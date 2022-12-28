Imports NDXVUMeterNET
Imports System.Threading
Imports System.IO

' Most of the code implemented in the BPM detection was based on the article at:
' http://www.gamedev.net/reference/articles/article1952.asp

Public Class frmMain
    Private Const MAX_ENERGY As Double = 65536.0 * 65536.0
    Private bufferLeft() As Double
    Private bufferRight() As Double

    Private bufferLength As Integer = 32768 * 4 ' 44100 * 4
    Private srcBufferPos As Integer
    Private dstBufferPos As Integer

    Private tmpFFTLeft() As ComplexDouble
    Private tmpFFTRight() As ComplexDouble

    Private fftBuffer() As ComplexDouble

    Private fftSize As Integer = 512
    Private detectBPMFlag As Boolean
    Private mainOffset As Integer = 0

    Private bpmAlgoThread As Thread
    Private bpmAlgoWaiter As AutoResetEvent
    Private cancelThreads As Boolean

    Private bitmaps As Dictionary(Of String, Bitmap)

    Private Class impulseTrain
        Private mBeatsEnergy() As Double
        Private mBeats() As Double
        Private mEnergy As Double
        Private mPowerAtBeat As Double
        Private mBeatsPower() As Double
        Private mBufferLength As Integer
        Private mFFTSize As Integer
        Private mBPM As Integer

        Public Sub New(ByVal bpm As Integer, ByVal bufferLength As Integer, ByVal fftSize As Integer)
            ReDim mBeatsEnergy(bufferLength \ 2 - 1)
            ReDim mBeats(bufferLength \ 2 - 1)
            ReDim mBeatsPower(bufferLength \ 2 - 1)

            mBufferLength = bufferLength
            mFFTSize = fftSize
            mBPM = bpm

            GenImpulseTrain()
            ResetBeatsPower()
        End Sub

        Public ReadOnly Property BPM() As Integer
            Get
                Return mBPM
            End Get
        End Property

        Public ReadOnly Property Beats() As Double()
            Get
                Return mBeats
            End Get
        End Property

        Public ReadOnly Property BeatsEnergy() As Double()
            Get
                Return mBeatsEnergy
            End Get
        End Property

        Public Property Energy() As Double
            Get
                Return mEnergy
            End Get
            Set(ByVal value As Double)
                mEnergy = value
            End Set
        End Property

        Public ReadOnly Property PowerAtBeat(ByVal index As Integer) As Double
            Get
                Return mBeatsPower(index)
            End Get
        End Property

        Private Sub GenImpulseTrain()
            Dim dataLength As Integer = Beats.Length
            Dim samplingTime As Double = mBufferLength / 21845 '32768
            Dim interval As Integer = CInt(dataLength / (samplingTime * mBPM / 60))

            ' FIXME: This value is crucial and I think it should be (slighly) different for each BPM
            Dim pWidth As Integer = mFFTSize * 3

            Array.Clear(Beats, 0, mBeats.Length)
            Array.Clear(Beats, 0, mBeats.Length)
            For k As Integer = 0 To dataLength - 1 Step interval
                For j As Integer = Math.Max(k - pWidth, 0) To Math.Min(k + pWidth, dataLength - 1)
                    mBeats(j) = MAX_ENERGY
                Next
            Next
        End Sub

        Public Sub ResetBeatsPower()
            For i As Integer = 0 To mBeats.Length - 1
                mBeatsPower(i) = Math.Sqrt(Math.Abs(mBeats(i)) + Math.Abs(mBeats(i)))
            Next
        End Sub
    End Class
    Private impulses As Dictionary(Of Integer, impulseTrain)

    Private Delegate Sub UpdateStatusDel(ByVal status As String, ByVal isWorking As Boolean)

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        cancelThreads = True
        bpmAlgoWaiter.Set()

        ' With the latest version... this is no longer required!!!
        'With dxvuCtrl
        '    If .MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.mscMonitoring Then
        '        .StopMonitoring()
        '        Do While .MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.mscMonitoring
        '            Application.DoEvents()
        '        Loop
        '    End If
        'End With
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True

        ' Prevent resizing (for now) -- the code that resizes the bitmaps doesn't work well...
        'Me.MaximumSize = Me.Size
        'Me.MinimumSize = Me.Size
        'Me.MaximizeBox = False

        ' Initialize
        srcBufferPos = 0
        ReDim bufferLeft(bufferLength - 1)
        ReDim bufferRight(bufferLength - 1)

        ReDim tmpFFTLeft(fftSize - 1)
        ReDim tmpFFTRight(fftSize - 1)

        ReDim fftBuffer(bufferLength \ 2 - 1)

        bitmaps = New Dictionary(Of String, Bitmap)
        GenerateBeatImpulses(80, 160)

        bpmAlgoWaiter = New AutoResetEvent(False)
        bpmAlgoThread = New Thread(AddressOf bpmAlgoSub)
        bpmAlgoThread.Start()
    End Sub

    Private Sub dxvuCtrl_ControlIsReady() Handles dxvuCtrl.ControlIsReady
        With dxvuCtrl
            .BackColor = Color.Black

            .Frequency = 44100
            .BitDepth = 16
            .Channels = 1

            .FFTXMin = 20
            .FFTXMax = 4000
            .FFTXZoom = True
            .FFTXScale = DXVUMeterNETGDI.FFTXScaleConstants.Logarithmic
            .FFTYScale = DXVUMeterNETGDI.FFTYScaleConstants.Amplitude
            .FFTWindow = FFTWindowConstants.Hanning
            .FFTSize = FFTSizeConstants.FFTs1024
            .FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Spectrum
            .FFTSmoothing = 4

            .LinesThickness = 2

            .Style = DXVUMeterNETGDI.StyleConstants.FFT

            .StartMonitoring()
        End With
    End Sub

    Private Sub btnDetectBPM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetectBPM.Click
        bitmaps.Clear()
        'UpdateStatus("Recording Audio...", True)

        detectBPMFlag = True
    End Sub

    Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If bitmaps IsNot Nothing AndAlso bitmaps.Count > 0 Then
            Dim p As Point = New Point(dxvuCtrl.Left, btnDetectBPM.Bottom + 20)
            Dim g As Graphics = e.Graphics
            Dim isFirst As Boolean = True
            Dim n As Integer
            Dim f As Font = New Font(Me.Font, FontStyle.Bold)

            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            For Each bmp As KeyValuePair(Of String, Bitmap) In bitmaps
                If n > 0 AndAlso p.Y + bmp.Value.Height * 2 >= Me.Height Then
                    n = 0
                    p.Offset(bmp.Value.Width + 5, btnDetectBPM.Bottom + 20)
                End If

                If isFirst Then
                    'g.DrawImageUnscaled(bpm.Value,  CInt(x - mainOffset / (bufferLength \ 2) * bpm.Value.Width), y)
                    'g.FillRectangle(Brushes.White, x, y, bmp.Value.Width, bmp.Value.Height)

                    Dim offset As Integer = CInt(mainOffset / (bufferLength \ 2) * bmp.Value.Width)
                    Dim cBmp As Bitmap = CropImage(bmp.Value, offset)
                    g.DrawImageUnscaled(cBmp, p)
                    cBmp.Dispose()

                    isFirst = False
                Else
                    g.DrawImageUnscaled(bmp.Value, p)
                End If

                g.DrawString(bmp.Key, f, Brushes.White, p.X + 4, p.Y + 4)
                g.DrawString(bmp.Key, f, Brushes.Black, p.X + 2, p.Y + 2)
                g.DrawRectangle(Pens.Gray, p.X, p.Y, bmp.Value.Width - 1, bmp.Value.Height - 1)

                p.Offset(0, bmp.Value.Height + 5)
                n += 1
            Next

            f.Dispose()
        End If
    End Sub

    Private Sub UpdateStatus(ByVal status As String, ByVal isWorking As Boolean)
        lblStatus.Text = status
        btnDetectBPM.Enabled = Not isWorking
    End Sub

    ' FIXME: There are some things that can be optimized (for speed) here...
    Private Sub dxvuCtrl_PeakValues(audioBuffer() As Byte, normalizedAudioBuffer() As Integer, maxPeak As NDXVUMeterNET.DXVUMeterNETGDI.Peak) Handles dxvuCtrl.PeakValues
        If detectBPMFlag Then
            ' Always assuming 2 channels and 16 bits
            Dim w As Double = 1
            For p As Integer = 0 To normalizedAudioBuffer.Length - 2 Step 2
                ' This window filters most of the upper spectrum enhancing the lower frequencies
                w = FFT.ApplyWindow(CInt(p / normalizedAudioBuffer.Length * fftSize), FFTSizeConstants.FFTs2048, FFTWindowConstants.Hanning)

                bufferLeft(srcBufferPos) = w * normalizedAudioBuffer(p)
                srcBufferPos += 1

                If srcBufferPos Mod fftSize = 0 Then
                    Dim tmp(fftSize - 1) As Double
                    Array.Copy(bufferLeft, srcBufferPos - fftSize, tmp, 0, fftSize)

                    '---------------
                    Dim filtered(fftSize \ 2) As Double
                    FFT.SavitzkyGolay(filtered, fftSize \ 2 - 1, 16, 16, 1, 4)

                    Dim convoluted(bufferLength - 1) As Double
                    FFT.Convolute(tmp, fftSize \ 2, filtered, fftSize \ 2 - 1, 1, convoluted)
                    '---------------

                    FFT.FourierTransform(fftSize, convoluted, tmpFFTLeft, convoluted, tmpFFTRight, False)

                    Array.Copy(tmpFFTLeft, 0, fftBuffer, dstBufferPos, fftSize \ 2)

                    Me.Invoke(New UpdateStatusDel(AddressOf UpdateStatus), String.Format("Recording Audio: {0:0}%", srcBufferPos / bufferLength * 100), True)

                    dstBufferPos += fftSize \ 2
                End If

                If srcBufferPos >= bufferLength Then
                    detectBPMFlag = False
                    srcBufferPos = 0
                    dstBufferPos = 0

                    bpmAlgoWaiter.Set()
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub AddBitmap(ByVal caption As String, ByVal data() As ComplexDouble, Optional ByVal height As Integer = 60, Optional ByVal nStep As Integer = 1)
        Dim bmp As Bitmap = New Bitmap(dxvuCtrl.Width, height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Dim bufferLength As Integer = data.Length - 1
        Dim v As Double
        Dim p(bufferLength \ nStep - 1) As Point
        Dim k As Integer = 0

        g.Clear(Color.White)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        data(0) = ComplexDouble.FromDouble(0)
        Dim max As Double = data.Max(Of Double)(Function(m) (m.Power2Root))

        For i As Integer = 1 To bufferLength - nStep Step nStep
            v = data(i).Power2Root / max
            p(k) = New Point(CInt(i / bufferLength * bmp.Width), CInt(bmp.Height - v * bmp.Height))
            k += 1
        Next
        ReDim Preserve p(k - 1)

        g.DrawLines(Pens.Black, p)

        bitmaps.Add(caption, bmp)
        g.Dispose()
    End Sub

    ' FIXME: This routine performs three passes to detect the BPM but it does it over the same
    '   portion of sampled audio... useless!
    '   We should take smaller samples and perform three passes over each separate chunk.
    Private Sub bpmAlgoSub()
        Do
            bpmAlgoWaiter.WaitOne(Timeout.Infinite, False)
            If cancelThreads Then Exit Sub

            AddBitmap("Detected Beats", fftBuffer, 180, 32)

            Dim avgBPM As Integer = 0
            Dim detectedBPM As impulseTrain
            'For pass As Integer = 1 To 3
            '    Me.Invoke(New UpdateStatusDel(AddressOf UpdateStatus), String.Format("Calculating BPM: {0}/3", pass), True)

            '    detectedBPM = DoDetectBPM()
            '    avgBPM += detectedBPM.BPM
            'Next

            'avgBPM = avgBPM \ 3
            detectedBPM = DoDetectBPM()
            avgBPM = detectedBPM.BPM

            detectedBPM = impulses.Item(avgBPM)
            Me.Invoke(New UpdateStatusDel(AddressOf UpdateStatus), detectedBPM.BPM.ToString() + " BPM", False)
            AddBitmap("Best Match: " + detectedBPM.BPM.ToString(), ComplexDouble.FromDouble(detectedBPM.BeatsEnergy))
            Me.Invalidate()
        Loop
    End Sub

    ' This function compares the recorded audio with several pre-generated streams of audio (beats).
    '   Probably, the only portion of the code that requires some tweaking is the synch section.
    Private Function DoDetectBPM() As impulseTrain
        Dim maxPower As Double = fftBuffer.Max(Of Double)(Function(m) m.Power2Root)
        Dim bpms As Dictionary(Of Integer, Double) = New Dictionary(Of Integer, Double)
        Dim beatWave(bufferLength \ 2 - 1) As Double
        Dim threshold As Double

        For Each bpm As KeyValuePair(Of Integer, impulseTrain) In impulses
            ' This portion of code tries to detect where does the first beat of the recorded audio starts.
            '   This is the synch code which, most likely, needs to be enhanced.
            threshold = maxPower * 0.3
            For Me.mainOffset = 1 To bufferLength \ 2 - 1
                If fftBuffer(mainOffset).Power2Root > threshold Then Exit For
            Next
            threshold = maxPower * 0.04

            Dim e1 As Double
            Dim e2 As Double
            Dim E As Double

            ' Here we calculate the power sum between the train of impulses for various
            '   BPMs against the recorded audio. The train of impulses that provides the highest
            '   overall power sum should be one that corresponds to the recorded audio's BPM.
            bpm.Value.Energy = 0
            For k As Integer = mainOffset + 1 To bufferLength \ 2 - 1 - mainOffset
                e2 = bpm.Value.PowerAtBeat(k - mainOffset)
                If e2 > 0 Then
                    e1 = fftBuffer(k).Power2Root '/ maxPower * MAX_ENERGY
                    E = Math.Max(e1 * 2, MAX_ENERGY)
                    If e1 >= threshold Then
                        bpm.Value.Energy += E
                        bpm.Value.BeatsEnergy(k - mainOffset) = bpm.Value.Beats(k - mainOffset) + E
                    Else
                        bpm.Value.Energy -= E / 2
                    End If
                End If
            Next
            bpms.Add(bpm.Key, bpm.Value.Energy)
            'Enable for debugging purposes
            'AddBitmap(bpm.Key.ToString(), ComplexDouble.FromDouble(bpm.Value.BeatsEnergy))
        Next

        ' I love LINQ!!!
        ' Here's a one-liner to obtain the impulse from the impulses list with the maximum energy value.
        Dim maxBPM = (From i In impulses _
                    Where i.Value.Energy = impulses.Values.Max(Of Double)(Function(e) e.Energy) _
                    Select i.Value).First

        Return maxBPM
    End Function

    Private Sub GenerateBeatImpulses(ByVal fromBPM As Integer, ByVal toBPM As Integer)
        If impulses Is Nothing Then
            impulses = New Dictionary(Of Integer, impulseTrain)
        Else
            impulses.Clear()
        End If
        For bpm As Integer = fromBPM To toBPM
            Dim imp As impulseTrain = New impulseTrain(bpm, bufferLength, fftSize)
            Array.Copy(imp.Beats, imp.BeatsEnergy, imp.Beats.Length)
            imp.ResetBeatsPower()

            impulses.Add(bpm, imp)
        Next
    End Sub

    Private Function CropImage(ByVal bmp As Bitmap, ByVal offset As Integer) As Bitmap
        Dim cbmp As Bitmap = New Bitmap(bmp.Width, bmp.Height, Imaging.PixelFormat.Format24bppRgb)
        Dim sg As Graphics = Graphics.FromImage(cbmp)
        sg.Clear(Color.White)
        sg.DrawImage(bmp, New Rectangle(0, 0, bmp.Width - offset, bmp.Height), offset, 0, bmp.Width - offset, bmp.Height, GraphicsUnit.Pixel)

        Dim mm As MemoryStream = New MemoryStream()
        cbmp.Save(mm, System.Drawing.Imaging.ImageFormat.Bmp)

        Dim rimg As Bitmap = CType(Bitmap.FromStream(mm), Bitmap)

        mm.Dispose()
        cbmp.Dispose()
        sg.Dispose()

        Return rimg
    End Function

#Region "Not used in this version"
    Private originalSize As Size
    Private Sub frmMain_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        originalSize = Me.Size
    End Sub

    Private Sub frmMain_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        If bitmaps IsNot Nothing AndAlso bitmaps.Count > 0 Then
            Dim newSize As Size = Me.Size
            Dim widthFactor As Double = Me.Size.Width / originalSize.Width
            Dim heightFactor As Double = Me.Size.Height / originalSize.Height

            Dim newBitmaps As Dictionary(Of String, Bitmap) = New Dictionary(Of String, Bitmap)
            For Each bmp As KeyValuePair(Of String, Bitmap) In bitmaps
                newBitmaps.Add(bmp.Key, New Bitmap(bmp.Value, CInt(bmp.Value.Width * widthFactor), CInt(bmp.Value.Height * heightFactor)))
                bmp.Value.Dispose()
            Next
            bitmaps = newBitmaps

            Me.Invalidate()
        End If
    End Sub
#End Region
End Class
