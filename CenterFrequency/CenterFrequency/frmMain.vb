Imports NDXVUMeterNET.DXVUMeterNETGDI
Imports System.Threading

Public Class frmMain
    Private selLine As DevicesCollection.Device.RecordingSourcesCollection.Line
    Private tmrResetFreq As Timer
    Private tmrDelayResize As Timer
    Private maxFreqVal As Double = 0
    Private fftIdxMin As Integer
    Private fftIdxMax As Integer

    Private setCenterFreqThread As Thread
    Private setCenterFreqEvent As AutoResetEvent
    Private setPointFreq As Integer

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        tmrResetFreq.Dispose()
        setCenterFreqThread.Abort()
        tmrDelayResize.Dispose()
        tmrDelayResize = Nothing

        Application.DoEvents()

        dxvuCtrl.StopMonitoring()
    End Sub

    Private Sub DXVUMeterNET_IsReady() Handles dxvuCtrl.ControlIsReady
        Dim i As Integer

        With dxvuCtrl
            .Style = StyleConstants.FFT
            .FFTSize = NDXVUMeterNET.FFT.FFTSizeConstants.FFTs1024
            .FFTXScale = FFTXScaleConstants.Logarithmic
            .FFTWindow = NDXVUMeterNET.FFT.FFTWindowConstants.Hanning
            .FFTSmoothing = 4
            .LinesThickness = 2

            .FFTXMin = 0
            .FFTXMax = 15000
            .FFTXZoom = True

            .BackColor = Color.Black
        End With

        For Each device As DevicesCollection.Device In dxvuCtrl.Devices
            i = cmbDevices.Items.Add(device)
            If device.Selected Then cmbDevices.SelectedIndex = i
        Next
        cmbDevices.SelectedIndex = 0

        cmbFFTYScale.Items.Clear()
        For Each k As Object In FFTYScaleConstants.GetValues(GetType(FFTYScaleConstants))
            cmbFFTYScale.Items.Add(k)
        Next
        cmbFFTYScale.SelectedIndex = 3
    End Sub

    Private Sub cmbDevices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDevices.SelectedIndexChanged
        Dim i As Integer
        Dim selDevice As DevicesCollection.Device = CType(cmbDevices.SelectedItem, DevicesCollection.Device)
        dxvuCtrl.Devices.SelectedDevice = selDevice

        cmbSources.Items.Clear()
        For Each srcLine As DevicesCollection.Device.RecordingSourcesCollection.Line In selDevice.RecordingSources
            i = cmbSources.Items.Add(srcLine)
            If srcLine.Selected Then cmbSources.SelectedIndex = i
        Next
        If cmbSources.SelectedIndex = -1 AndAlso cmbSources.Items.Count > 0 Then cmbSources.SelectedIndex = 0
        If cmbSources.Items.Count = 0 Then
            cmbSources.Enabled = False
            tbVolume.Enabled = False
        End If

        Dim q As DevicesCollection.Device.QualitiesCollection.Quality
        cmbCapabilities.Items.Clear()
        For Each q In selDevice.Qualities
            cmbCapabilities.Items.Add(q)
            If q.BitDepth = 16 And q.Channels = 1 And q.Frequency = 44100 Then cmbCapabilities.SelectedIndex = cmbCapabilities.Items.Count - 1
        Next
        If cmbCapabilities.SelectedIndex = -1 Then cmbCapabilities.SelectedIndex = cmbCapabilities.Items.Count - 1
    End Sub

    Private Sub cmbSources_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSources.SelectedIndexChanged
        Dim srcLine As DevicesCollection.Device.RecordingSourcesCollection.Line = CType(cmbSources.SelectedItem, DevicesCollection.Device.RecordingSourcesCollection.Line)

        SelectRecordingSource(srcLine)
    End Sub

    Private Sub SelectRecordingSource(ByVal srcLine As DevicesCollection.Device.RecordingSourcesCollection.Line)
        If selLine IsNot Nothing Then selLine.RemoveBingings()

        With srcLine
            If .HasVolume Then
                tbVolume.Enabled = True
                tbVolume.Minimum = .Min
                tbVolume.Maximum = .Max
                tbVolume.LargeChange = (.Max - .Min) / 10
                tbVolume.TickFrequency = tbVolume.LargeChange
                srcLine.SetVolumeBinding(tbVolume)
                srcLine.Selected = True
            Else
                tbVolume.Enabled = False
            End If
        End With

        selLine = srcLine
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        dxvuCtrl.StartMonitoring()

        If dxvuCtrl.MonitoringState = MonitoringStateConstants.Monitoring Then
            cmbCapabilities.Enabled = False
            cmbDevices.Enabled = False
            btnStop.Enabled = True
            btnStart.Enabled = False

            With dxvuCtrl
                fftIdxMin = .Freq2FFTIdx(.FFTXMin)
                fftIdxMax = .Freq2FFTIdx(.FFTXMax)
            End With

            pMarker.Visible = True
            lblFreq.Visible = True
            lblFreq.BringToFront()

            tmrResetFreq.Change(500, Timeout.Infinite)
        End If
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        If dxvuCtrl.MonitoringState = MonitoringStateConstants.Monitoring Then
            dxvuCtrl.StopMonitoring()

            cmbCapabilities.Enabled = True
            cmbDevices.Enabled = True
            btnStop.Enabled = False
            btnStart.Enabled = True

            pMarker.Visible = False
            lblFreq.Visible = False

            tmrResetFreq.Change(Timeout.Infinite, Timeout.Infinite)
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnStop.Enabled = False
        tmrResetFreq = New Timer(New TimerCallback(AddressOf DoResetFreq), Nothing, Timeout.Infinite, Timeout.Infinite)
        tmrDelayResize = New Timer(New TimerCallback(AddressOf DelayResize), Nothing, Timeout.Infinite, Timeout.Infinite)

        ' Laziness at its best!
        Panel.CheckForIllegalCrossThreadCalls = False

        setCenterFreqEvent = New AutoResetEvent(False)
        setCenterFreqThread = New Thread(AddressOf setCenterFreqSub)
        setCenterFreqThread.Start()

        ' Uncomment the following line and replace it with the one you received via email
        '   with your own registration information.
        'This will remove the DEMO message in DXVUMeterNET
        'DxvuMeterNET1.LicenseControl("XXXXXX", "XXX-XXX-XXX")
    End Sub

    Private Sub DoResetFreq(ByVal state As Object)
        maxFreqVal = 0
    End Sub

    Private Sub cmbCapabilities_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCapabilities.SelectedIndexChanged
        Dim q As DevicesCollection.Device.QualitiesCollection.Quality = CType(cmbCapabilities.SelectedItem, DevicesCollection.Device.QualitiesCollection.Quality)
        With dxvuCtrl
            .Frequency = q.Frequency
            .BitDepth = q.BitDepth
            .Channels = q.Channels
        End With
    End Sub

    Private Sub dxvuCtrl_PeakValues(AudioBuffer() As Byte, NormalizedAudioBuffer() As Integer, MaxPeak As Peak) Handles dxvuCtrl.PeakValues
        Dim maxFreq As Integer = -1
        Dim tmpFreqVal As Single
        Dim newMaxFreqVal As Double = maxFreqVal
        With dxvuCtrl
            For f As Integer = .FFTXMin To .FFTXMax
                tmpFreqVal = .FFTAverageFromFrequency(f, FFTChannelConstants.Left)
                If tmpFreqVal > newMaxFreqVal Then
                    newMaxFreqVal = tmpFreqVal
                    maxFreq = f
                End If
            Next

            If newMaxFreqVal > maxFreqVal AndAlso maxFreq <> -1 Then
                tmrResetFreq.Change(200, Timeout.Infinite)
                maxFreqVal = newMaxFreqVal

                Dim mf As String = HumanFreq(maxFreq)
                Dim mv As String = Int(maxFreqVal * 150 - 150).ToString + "dB (" + Int(maxFreqVal * 100).ToString + "%)"

                If .FFTXScale = FFTXScaleConstants.Normal Then
                    setPointFreq = .Left + maxFreq / (.FFTXMax - .FFTXMin) * (.Width - 28) + 29
                Else
                    setPointFreq = .Left + .FFTIdx2X(.Freq2FFTIdx(maxFreq), fftIdxMin, fftIdxMax, .Width - 28)
                End If

                lblFreq.Text = String.Format("{0} @ {1}", mf, mv)
                lblFreq.Top = .Top + (1 - maxFreqVal) * (.Height - 27)

                lblInfo.Text = String.Format("Center Frequency: {0}{1}Level: {2}", mf, vbCrLf, mv)
            End If
        End With
    End Sub

    Private Sub setCenterFreqSub()
        Do While setCenterFreqThread.ThreadState = ThreadState.Running
            setCenterFreqEvent.WaitOne(25, False)

            If setPointFreq <> pMarker.Left Then
                Dim dif As Integer = setPointFreq - pMarker.Left
                If dif <> 0 Then
                    pMarker.Left += dif / 2

                    If Math.Abs(dif) <= 10 Then pMarker.Left = setPointFreq

                    lblFreq.Left = pMarker.Left - lblFreq.Width / 2
                End If
            End If
        Loop
    End Sub

    Private Function HumanFreq(ByVal f As Single) As String
        If f > 1000 Then
            Return Math.Round(f / 1000, 1).ToString + " KHz"
        Else
            Return Math.Round(f, 1).ToString + " Hz"
        End If
    End Function

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If tmrDelayResize IsNot Nothing Then
            tmrDelayResize.Change(1000, 0)
        Else
            DelayResize(Nothing)
        End If
    End Sub

    Private Sub DelayResize(ByVal state As Object)
        pMarker.Top = dxvuCtrl.Top - 10
        pMarker.Height = dxvuCtrl.Height + 10 * 2

        lblInfo.Top = chkLogXScale.Bottom + 5
        cmbFFTYScale.Top = chkLogXScale.Top - 3
    End Sub

    Private Sub chlLogXScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLogXScale.CheckedChanged
        If chkLogXScale.Checked Then
            dxvuCtrl.FFTXScale = FFTXScaleConstants.Logarithmic
        Else
            dxvuCtrl.FFTXScale = FFTXScaleConstants.Normal
        End If
    End Sub

    Private Sub cmbFFTYScale_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFFTYScale.SelectedIndexChanged
        dxvuCtrl.FFTYScale = cmbFFTYScale.SelectedItem
    End Sub
End Class
