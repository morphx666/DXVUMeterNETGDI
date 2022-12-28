Imports System.Threading
Imports System.ComponentModel
Imports System.Math
Imports System.Runtime.InteropServices
Imports SlimDX.DirectSound
Imports SlimDX.Direct3D10

'The NDXVUMeterNET namespace encapsulates all the classes, structures, events and constants that make up the control.

''' <summary>
''' The DXVUMeterNETGDI class provides access to all the functionality provided by the control to monitor, playback and record audio.
''' </summary>
''' <remarks>
''' <p>DXVUMeterNETGDI is a .NET class library which can monitor any audio device configured as a recording source such as a Microphone, CD, etc... and display the monitored audio levels like a standard Digital VU Meter, as an Oscilloscope or even display the audio in the frequency domain using the Fast Fourier Transform).</p>
''' <p>DXVUMeterNETGDI is also able to save to a WAV file the audio data being monitored and since it uses a circular (or infinite) buffer there's no limitation on the amount of audio that can be captured. Playback of WAV and MP3 files is also supported.</p>
''' <p>The interface of the control is fully resizable in order to let it fit into any form design.</p>
''' <p>The DigitalVU mode supports vertical and horizontal placement of the bars and the FFT display mode implements many settings found only on professional applications which can be used to either beautify the display or perform professional audio analysis.</p>
''' <p>An application can use multiple instances of the control to display multiple versions of the same audio data but, since DXVUMeterNETGDI can monitor audio from different sound cards it is also possible to develop applications that incorporate multiple instances of the control where each instance monitors a different sound card.</p>
''' <p>DXVUMeterNETGDI uses GDI+ to render the audio data. Custom functions using GDI+ can also be designed to render your own audio display interface. This can be done by setting the <see cref="DXVUMeterNETGDI.Style">Style</see> property to <see cref="StyleConstants.UserPaintGDI">UserPaintGDI</see>.</p>
''' <p><b>Requirements</b></p>
''' <list type="bullet">
''' <item><description>Microsoft .NET 4.0</description></item>
''' <item><description>DirectX 9 or above</description></item>
''' <item><description>A version of Windows that supports the .NET 4.0 framework and Direct3D</description></item>
''' </list>
''' <p><b>Important considerations when using DXVUMeterNETGDI</b></p>
''' There are two things that you must keep in mind when using DXVUMeterNETGDI on a Form:
''' <list type="number">
''' <item><description>
''' Make sure you do not access any of DXVUMeterNETGDI's methods or properties before the <see cref="DXVUMeterNETGDI.ControlIsReady">ControlIsReady</see> event has been raised.
''' Trying to do so can cause unpredictable results.
''' When the control has raised the event, you can start working with DXVUMeterNETGDI. For example, you could call the <see cref="DXVUMeterNETGDI.StartMonitoring">StartMonitoring</see> method as shown in this code:
''' <code lang="vb.net">
''' Private Sub DxvuMeterNETGDI1_ControlIsReady() Handles DxvuMeterNETGDI1.ControlIsReady
'''     DxvuMeterNETGDI1.StartMonitoring()
''' End Sub
''' </code>
''' </description></item>
''' <item><description>
''' Make sure you stop DXVUMeterNETGDI before the form is closed. The best way to do this is as follows:
''' <code lang="vb.net">
''' Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
'''     DxvuMeterNETGDI1.StopMonitoring()
'''     Application.DoEvents()
''' End Sub
''' </code>
''' </description></item>
''' <item><description>
''' It is very important that you add the following code to the configuration section of the app.config file for your applications:
''' <code lang="xml">
''' <startup useLegacyV2RuntimeActivationPolicy="true">
'''     <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
''' </startup>
''' </code>
''' </description></item>
''' </list>
''' If you are using a registered version of DXVUMeterNETGDI, you should pass your registration information to the control from the <see cref="DXVUMeterNETGDI.ControlIsReady">ControlIsReady</see> event, using the <see cref="DXVUMeterNETGDI.LicenseControl">LicenseControl</see> method:
''' <code lang="vbnet">
''' Private Sub DxvuMeterNETGDI1_ControlIsReady() Handles DxvuMeterNETGDI1.ControlIsReady
'''     DXVUMeterNET1.LicenseControl("2J0N1X88022AE33FNED314159A653968", "DXVUNET-KP28-11AY-TJ2A")
''' End Sub
''' </code>
''' </remarks>
<ToolboxBitmap(GetType(DXVUMeterNETGDI))>
Public Class DXVUMeterNETGDI
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitControl()
        'Add any initialization after the InitializeComponent() call
        suspendThreads = True

        cursorPos = New CCursorPos(Me)
        pSaveFileName = New CWavFile(Me)
        pPlayFileName = New CWavFile(Me)
        PlaybackVolume = 0

        eventIdleRender = New AutoResetEvent(False)
        idleRenderThread = New Thread(AddressOf IdleRenderThreadSub)
        With idleRenderThread
            .Name = "dxvu_render_thread"
            '.SetApartmentState(ApartmentState.STA)
            '.IsBackground = True
            .Start()
        End With

        ' Create a notification event, for when the sound stops playing
        eventMonitorRender = New AutoResetEvent(False)

        ' Create thread that will be executed whenever a position notification event is thrown
        monitorRenderThread = New Thread(AddressOf MonitorThreadSub)
        With monitorRenderThread
            .Name = "dxvu_monitor_thread"
            .Start()
        End With

        ' Create thread that will be executed whenever the playback buffer is ready to accept new data
        eventPlaybackRender = New AutoResetEvent(False)
        playbackRenderThread = New Thread(AddressOf PlaybackThreadSub)
        With playbackRenderThread
            .Name = "dxvu_playback_thread"
            .Start()
        End With

        Devices = New DevicesCollection()
        Renderer = New CRenderer(Me)

        DefineColors()
        ResetGraphicsData()

        suspendThreads = False

        RaiseEvent ControlIsReady()
    End Sub

    Protected Overrides Sub Finalize()
        StopMonitoring()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' This subroutine must be called in order to "register" the control and prevent it from displaying the "DEMO" message.
    ''' </summary>
    ''' <param name="licenseKey">The provided license key</param>
    ''' <param name="serialNumber">The provided serial number</param>
    ''' <remarks>If this function is called with the incorrect parameters, the control may crash</remarks>
    Public Sub LicenseControl(Optional licenseKey As String = "", Optional serialNumber As String = "DEMO")
        'Dim password As String

        'Try
        '    If licenseKey = "" Then
        '        licenseKey = "08C12F36BD496DD94C51FAB8477CB72A"
        '        password = "DXVUNET-DEMO"
        '    Else
        '        password = "DXVUMeterNET::" + serialNumber
        '    End If
        '    Dim code As String = (New CCrypt).Decrypt(licenseKey, password, serialNumber)

        '    dynCompiler = New CDynCompile()
        '    dynCompiler.CompileCode(code)
        'Catch
        '    dynCompiler = Nothing
        '    Throw New Exception("Invalid License")
        'End Try
    End Sub

#Region " Private "

    'Private dynCompiler As CDynCompile

#Region " Private Variables "
    Private inMP3Mode As Boolean = False
    Private lameEnc As CLame
    Private savedBytes As Long
    Private isMono As Boolean = False

    Private mLeftChannelMute As Boolean = False
    Private mRightChannelMute As Boolean = False

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
    Protected Friend ReadOnly Property IsLicensed As Boolean
        Get
            Return True ' dynCompiler.ExecuteCode()
        End Get
    End Property

#Region " DigitalVUs "
    Private VUsReady As Boolean = False
    Private vuL(0) As Integer
    Private vuR(0) As Integer

    Private pNumVUs As Short = 32

    Private pOrientation As OrientationConstants = OrientationConstants.Horizontal

    Private ledSize As SizeF
#End Region

#Region " Colors "
    Private pGreenOn As Color = Color.FromArgb(0, &HFF, 0)
    Private pGreenOff As Color = Color.FromArgb(0, &H80, 0)
    Private pYellowOn As Color = Color.FromArgb(&HFF, &HFF, 0)
    Private pYellowOff As Color = Color.FromArgb(&H80, &H80, 0)
    Private pRedOn As Color = Color.FromArgb(&HFF, 0, 0)
    Private pRedOff As Color = Color.FromArgb(&H80, 0, 0)
#End Region

#Region " FFT "
    Private pFrequency As Integer = 44100
    Private pFrequency2 As Integer = pFrequency \ 2
    Private pFFTXMin As Integer = 0
    Private pFFTXMax As Integer = 22050
    Private pFFTXZoom As Boolean = False
    Private pFFTXZoomWindowPos As Integer = 0
    Private pFFTScaleFont As New Drawing.Font("Tahoma", 8)
    Private pFFTScaleFontSize As Size
    Private pFFTXScale As FFTXScaleConstants = FFTXScaleConstants.Normal
    Private pFFTYScale As FFTYScaleConstants = FFTYScaleConstants.dB

    Private fftWH As SizeF
    Private fft_w As Double
    Private fft_w2 As Double
    Private fft_w4 As Double
    Private fft_h As Double
    Private fft_h2 As Double
    Private fft_h4 As Double
    Private fft_h2h4 As Double

    Protected fftMin As Integer
    Protected fftMax As Integer

    Private pFFTStyle As FFTStyleConstants = FFTStyleConstants.Line
    Private pFFTLineChannelMode As FFTLineChannelModeConstants = FFTLineChannelModeConstants.Normal
    Private pFFTSize As FFTSizeConstants = FFTSizeConstants.FFTs512
    Private pFFTSize2 As Integer = pFFTSize \ 2
    Private pFFTWindow As FFTWindowConstants = FFTWindowConstants.None
    Private pFFTRenderScales As FFTRenderScalesConstants = FFTRenderScalesConstants.Both
    Private pFFT3DSize As Integer = 10


    Private windowSum As Double
    Private windowValues() As Double

    Private pFFTHoldMaxPeaks As Boolean = False
    Private pFFTHoldMinPeaks As Boolean = False
    Private pFFTShowMinMaxRange As Boolean = False
    Private pFFTSmoothing As Integer
    Private pFFTPlotNoiseReduction As Integer
    Private pFFTShowDecay As Boolean
    Private pFFTDetectDTMF As Boolean
    Private pFFTPeaksDecayDelay As Integer = 10
    Private pFFTPeaksDecaySpeed As Integer = 20
    Private pFFTNormalized As Boolean = True
    Private pFFTHighPrecisionMode As Boolean = False

    Private pFFTHistorySize As Integer = 4
    Private FFTHistL()() As Double
    Private FFTHistR()() As Double

    Private pOverlap As Double

    Private WAVHistL() As Double
    Private WAVHistR() As Double

    Private pFilters As New List(Of BiQuadFilter)
    Private pFFTYScaleMultiplier As Double = 1

    Private pCOHHistorySize As Integer = 20
    Private COHHistory()() As ComplexDouble

    Private pNoiseFilter As Integer = 0

    Private maxFFTL() As PointF
    Private maxFFTR() As PointF
    Private maxTimerFFTL() As Integer
    Private maxTimerFFTR() As Integer
    Private fftPL() As PointF
    Private fftPR() As PointF
    Private minTimerFFTL() As Integer
    Private minTimerFFTR() As Integer
    Private minFFTL() As PointF
    Private minFFTR() As PointF

    Private waveL() As Double
    Private waveR() As Double
    Private fftL() As ComplexDouble
    Private fftR() As ComplexDouble
    Private bufferIndex As Integer
    Private waveInIndex As Integer
    Private processedFFTSamples As Integer

    Private AvgL As Double
    Private AvgR As Double

    'Private FFTSmoothAttact As Integer
    'Private FFTSmoothDecay As Integer

    Private txtColor As Color = Color.White
    Private axisColor As Color = Color.FromArgb(100, 100, 100)
    Private xSpectrum As Double

    Private cursorPos As CCursorPos

    Private dtmfDetectedTone As DTMFToneConstants = DTMFToneConstants.DTMFInvalid
    Private tmrTriggerDTMF As Timer
#End Region

#Region " Monitoring/Playback "
    Private pBitDepth As Short = 16
    Private pChannels As Short = 2

#Region " DirectSound "
    Private capBuf As CaptureBuffer
    Private bufCapDesc As CaptureBufferDescription
    Private dsCap As DirectSoundCapture
    Private rBuf() As Byte
    Private pBuf() As Byte

    Protected playBuf As SecondarySoundBuffer
    Private bufPlayDesc As SoundBufferDescription

    Private notifySize As Integer
    Private numberMonitoringNotifications As Integer = 2
    Private numberPlaybackNotifications As Integer = 8
    Private nextCaptureOffset As Integer
    Private nextPlaybackOffset As Integer
#End Region

    Private pSaveFileName As CWavFile
    Private pPlayFileName As CWavFile
    Private pMonitoringState As MonitoringStateConstants = MonitoringStateConstants.Idle
    Private pRecordingState As RecordingStateConstants = RecordingStateConstants.Idle
    Private pPlaybackState As PlaybackStateConstants = PlaybackStateConstants.Idle
    Private Structure PlaybackVolumeStruct
        Dim Volume As Short
        Dim VolSign As Integer
        Dim Amp As Double
    End Structure
    Private pPlaybackVolume As PlaybackVolumeStruct
#End Region

#Region " Threads "
    Private eventMonitorRender As AutoResetEvent
    Private abortMonitorRenderThread As Boolean
    Private monitorRenderThread As Thread
    Private monitorRenderCanAbort As Boolean = True

    Private eventPlaybackRender As AutoResetEvent
    Private abortPlaybackRenderThread As Boolean
    Private playbackRenderThread As Thread
    Private playbackRenderCanAbort As Boolean = True

    Private suspendThreads As Boolean = False

    Private eventIdleRender As AutoResetEvent
    Private abortIdleRenderThread As Boolean
    Private idleRenderThread As Thread
    Private idleRenderCanAbort As Boolean = True
#End Region

#Region " Rendering "
    Private pEnableRendering As Boolean = True
    Private pStyle As StyleConstants = StyleConstants.DigitalVU
    Private pLinesThickness As Integer = 1

    Private w As Double
    Private w2 As Double
    Private w4 As Double
    Private h As Double
    Private h2 As Double
    Private h4 As Double
    Private h2h4 As Double

    Private Renderer As CRenderer

    Private pIsPainting As Boolean
    Private pIsRendering As Boolean
#End Region
    Private plGray As Color = Color.LightGray

    Private Structure WAVEFileHeader
        Const dwRiff As Integer = &H46464952
        Const dwWave As Integer = &H45564157
        Const dwFormat As Integer = &H20746D66
        Const dwData As Integer = &H61746164
        Dim dwFileSize As Integer
        Dim dwFormatLength As Integer
        Dim wFormatTag As Short
        Dim nChannels As Short
        Dim nSamplesPerSec As Integer
        Dim nAvgBytesPerSec As Integer
        Dim nBlockAlign As Short
        Dim wBitsPerSample As Short
        Dim dwDataLength As Integer
    End Structure

    ''' <summary>
    ''' The audio channels supported by DXVUMeterNETGDI
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum FFTChannelConstants
        ''' <summary>The left audio channel.</summary>
        Left = 0
        ''' <summary>The right audio channel. Note that for mono audio streams, the right channel contains the same data as the left channel.</summary>
        Right = 1
    End Enum
#End Region

#Region " Private Methods "
#Region " FFT Subroutines "
    Private Sub InitFFT(Optional resetGraphics As Boolean = True)
        pFFTSize2 = pFFTSize \ 2

        UpdateWindow()

        If fftPL Is Nothing Then
            ReDim minFFTL(0)
            ReDim minFFTR(0)
            ReDim fftPL(0)
            ReDim fftPR(0)
            ReDim maxFFTL(0)
            ReDim maxFFTR(0)
        End If

        ReDim fftL(pFFTSize - 1)
        ReDim waveL(pFFTSize - 1)
        ReDim fftR(pFFTSize - 1)
        ReDim waveR(pFFTSize - 1)

        For i As Integer = 0 To pFFTSize - 1
            fftL(i) = New ComplexDouble()
            waveL(i) = 0
            fftR(i) = New ComplexDouble()
            waveR(i) = 0
        Next

        'SetFFTSmoothing()
        If resetGraphics Then ResetGraphicsData()
    End Sub

    Private Sub UpdateWindow()
        windowValues = GetWindowValues(pFFTSize, pFFTWindow)
        windowSum = GetWindowSum(pFFTSize, pFFTWindow)
    End Sub

    'Private Sub SetFFTSmoothing()
    '    If pFFTDetectDTMF Then
    '        FFTSmoothAttact = 1
    '        FFTSmoothDecay = 1
    '    Else
    '        FFTSmoothAttact = CInt(((8192 - pFFTSize2) / 4096) + 1)
    '        FFTSmoothDecay = CInt(((8192 - pFFTSize2) / 4096) + 1)
    '    End If
    'End Sub

    Private Function Interpolate(x0 As Double, y0 As Double, x1 As Double, y1 As Double, x As Double) As Double
        Return y0 + ((y1 - y0) / (x1 - x0)) * (x - x0)
    End Function

    Private Shared Function HumanFreq(f As Double, Optional IncludeUnit As Boolean = True) As String
        If f >= 1000 Then
            Dim fs As String = String.Format("{0:F1}", f / 1000)
            Return fs + "K" + If(IncludeUnit, "Hz", "")
        Else
            Dim fs As String = String.Format("{0:F1}", f)
            Return fs.ToString + If(IncludeUnit, "Hz", "")
        End If
    End Function

    Private Function X2Freq(x As Integer, min As Double, max As Double, w As Double) As Double
        w -= fftWH.Width
        If pFFTXScale = FFTXScaleConstants.Logarithmic Then
            If pFFTXZoom Then
                Return FFTIdx2Freq(CInt(10 ^ ((x - fftWH.Width) / w * Log10((max - min) - 1)) + min - 1))
            Else
                Return FFTIdx2Freq(CInt(10 ^ (((x - fftWH.Width) / w) * Log10(pFFTSize2 - 1)) - 1))
            End If
        Else
            If pFFTXZoom Then
                Return X2fftIdx(x, min, max, w) / pFFTSize2 * pFrequency2
            Else
                Return (x - fftWH.Width) / w * pFrequency2
            End If
        End If
    End Function

    Private Function TodB(v As Double, floor As Double, ceiling As Double, w As Double) As Double
        Return 10 ^ (v / (ceiling - floor) * Log10((fftMax - floor) - 1)) - 1
    End Function

    Private Function X2fftIdx(x As Double, min As Double, max As Double, w As Double) As Integer
        x -= fftWH.Width
        w -= fftWH.Width
        If pFFTXScale = FFTXScaleConstants.Logarithmic Then
            If pFFTXZoom Then
                Return CInt((10 ^ (x / w * Log10((max - min) - 1)) + min - 1))
            Else
                Return CInt((10 ^ (x / w * Log10(pFFTSize2 - 1)) - 1))
            End If
        Else
            If pFFTXZoom Then
                Return CInt(x / w * (max - min) + min)
            Else
                Return CInt(x / w * pFFTSize2)
            End If
        End If
    End Function

    ''' <summary>
    ''' Returns the frequency at the given FFT index
    ''' </summary>
    ''' <param name="fftIdx">The index for which we want to obtain the frequency for</param>
    ''' <returns>Returns the frequency for the given index</returns>
    Public Function FFTIdx2Freq(fftIdx As Integer) As Double
        Return fftIdx / (pFFTSize2 - 1) * pFrequency2 - pFFTXZoomWindowPos
    End Function

    ''' <summary>
    ''' Returns the FFT index for a given frequency
    ''' </summary>
    ''' <param name="freq">The frequency for which we want to obtain the index for</param>
    ''' <returns>Returns the index for the given frequency</returns>
    Public Function Freq2FFTIdx(freq As Double) As Integer
        Return Floor((freq + pFFTXZoomWindowPos) / pFrequency2 * (pFFTSize2 - 1))
    End Function

    ''' <summary>
    ''' Returns the "x" (horizontal) position within the control's graphic area given the FFT index
    ''' </summary>
    ''' <param name="fftIdx">The index for which we want to obtain the "x" value for</param>
    ''' <param name="min">The minimum Frequency Index</param>
    ''' <param name="max">The maximum Frequency Index</param>
    ''' <param name="w">The width of the control's area</param>
    ''' <returns>Returns the "x" position for the given index</returns>
    ''' <remarks></remarks>
    Public Function FFTIdx2X(fftIdx As Integer, min As Double, max As Double, w As Double) As Double
        w -= fftWH.Width
        If pFFTXScale = FFTXScaleConstants.Logarithmic Then
            If pFFTXZoom Then
                Return Log10((fftIdx - min) + 1) / Log10((max - min) - 1) * w + fftWH.Width
            Else
                Return Log10(fftIdx + 1) / Log10(pFFTSize2 - 1) * w + fftWH.Width
            End If
        Else
            If pFFTXZoom Then
                Return (fftIdx - min) / ((max - min) - 1) * w + fftWH.Width
            Else
                Return fftIdx / (pFFTSize2 - 1) * w + fftWH.Width
            End If
        End If
    End Function

    Private Function FFTIdx2Y(fftIdx As Integer, min As Double, max As Double, h As Double) As Double
        If pFFTXZoom Then
            Return (fftIdx - min) / ((max - min) - 1) * h
        Else
            Return fftIdx / (pFFTSize2 - 1) * h
        End If
    End Function

    Private Function Octave2FFTIdx(o As Double) As Integer
        Dim f As Double = 2 ^ o * CCursorPos.Note2Freq(0)
        If f >= pFrequency2 Then
            Return Freq2FFTIdx(pFrequency2) - 1
        Else
            Return Freq2FFTIdx(f)
        End If
    End Function

    ''' <summary>
    ''' Returns the power of the frequency response at a given FFT index
    ''' </summary>
    ''' <param name="fftIdx">The index at within the FFT transform</param>
    ''' <param name="channel">One of the values from the <see cref="FFTChannelConstants">FFTChannelConstants</see> enumerator</param>
    ''' <param name="bandWidth">if this value is larger than 0, the function will return the average of the adjacent bands</param>
    ''' <returns>Returns the power response for the given FFT index</returns>
    ''' <remarks>The power response is calculated using the following formula: (Real² + Imaginary²)</remarks>
    Public Function FFTPowerFromIndex(fftIdx As Integer, channel As FFTChannelConstants, Optional bandWidth As Integer = 0) As Double
        If pFFTNormalized Then Return FFTNormalizedPowerFromIndex(fftIdx, channel, bandWidth)

        Dim v As Double = 0

        If fftIdx < fftMin Then
            fftIdx = fftMin
        ElseIf fftIdx > fftMax Then
            fftIdx = fftMax - 1
        End If

        If bandWidth <= 0 Then
            If channel = FFTChannelConstants.Left Then
                v = fftL(fftIdx).Power()
            Else
                v = fftR(fftIdx).Power()
            End If
        Else
            For i As Integer = Max(fftMin, fftIdx - bandWidth) To Min(fftMax, fftIdx + bandWidth)
                If channel = FFTChannelConstants.Left Then
                    v += fftL(i).Power()
                Else
                    v += fftR(i).Power()
                End If
                v /= bandWidth * 2
            Next
        End If

        Return FFTApplyYScale(v, fftIdx)
    End Function

    ''' <summary>
    ''' Returns the normalized power of the frequency response at a given FFT index
    ''' </summary>
    ''' <param name="fftIdx">The index at within the FFT transform</param>
    ''' <param name="channel">One of the values from the <see cref="FFTChannelConstants">FFTChannelConstants</see> enumerator</param>
    ''' <param name="bandWidth">if this value is larger than 0, the function will return the average of the adjacent bands</param>
    ''' <returns>Returns the normalized power response for the given FFT index</returns>
    ''' <remarks>The normalized power response is calculated using the following formula: 2 * (Real² + Imaginary²) / WindowSum</remarks>
    Public Function FFTNormalizedPowerFromIndex(fftIdx As Integer, channel As FFTChannelConstants, Optional bandWidth As Integer = 0) As Double
        Dim v As Double = 0

        If fftIdx < fftMin Then
            fftIdx = fftMin
        ElseIf fftIdx > fftMax Then
            fftIdx = fftMax - 1
        End If

        If bandWidth <= 0 Then
            If channel = FFTChannelConstants.Left Then
                v = (fftL(fftIdx) / windowSum * 2).Power()
            Else
                v = (fftR(fftIdx) / windowSum * 2).Power()
            End If
        Else
            ' FIXME: This looks so wrong!
            '        See how this was implemented in FFTAverageFromIndex
            For i As Integer = Max(fftMin, fftIdx - bandWidth) To Min(fftMax, fftIdx + bandWidth)
                If channel = FFTChannelConstants.Left Then
                    v += (fftL(i) / windowSum * 2).Power()
                Else
                    v += (fftR(i) / windowSum * 2).Power()
                End If
                v /= bandWidth * 2
            Next
        End If

        Return FFTApplyYScale(v, fftIdx)
    End Function

    Private Function FFTPhaseFromIndex(fftidx As Integer, channel As FFTChannelConstants) As Double
        If fftidx = 0 Then Return 0

        Dim v As Double = Atan(FFTPowerFromIndex(fftidx, channel) / FFTPowerFromIndex(fftidx - 1, channel))

        If channel = FFTChannelConstants.Left Then
            v *= Sign(fftL(fftidx).R)
        Else
            v *= Sign(fftR(fftidx).R)
        End If

        Return v
    End Function

    Private Function FFTPhaseFromIndex2(fftidx As Integer, channel As FFTChannelConstants) As ComplexDouble
        Dim v As New ComplexDouble()
        If fftidx = 0 Then Return v

        v.R = FFTPowerFromIndex(fftidx, channel)
        If channel = FFTChannelConstants.Left Then
            v.I = Atan2(fftL(fftidx).I, fftL(fftidx).R)
        Else
            v.I = Atan2(fftR(fftidx).I, fftR(fftidx).R)
        End If

        Return v
    End Function

    Private Function FFTApplyYScale(value As Double, fftidx As Integer) As Double
        If pFFTNormalized Then
            Select Case pFFTYScale
                Case FFTYScaleConstants.Magnitude
                    value = Sqrt(value) / 1500
                Case FFTYScaleConstants.Amplitude
                    value = (2 * Sqrt(value) / pFFTSize) / 1.5
                Case FFTYScaleConstants.dB
                    value = Log10(value + 1) / 10
                    If value < 0 Then value = 0
                Case FFTYScaleConstants.PSDTimeInt
                    value = (2 * (fftidx + 1) * value) / (24000 * pFFTSize) / 2
            End Select
        Else
            Select Case pFFTYScale
                Case FFTYScaleConstants.Magnitude
                    value = Sqrt(value) / 1500000
                Case FFTYScaleConstants.Amplitude
                    value = (2 * Sqrt(value) / pFFTSize) / 2000
                Case FFTYScaleConstants.dB
                    value = Log10(value + 1) / 17
                    If value < 0 Then value = 0
                Case FFTYScaleConstants.PSDTimeInt
                    value = (2 * (fftidx + 1) * value) / (24000 * pFFTSize) / 10000000
            End Select
        End If

        value *= pFFTYScaleMultiplier
        If value > 1 Then value = 1

        Return value
    End Function

    ''' <summary>
    ''' Returns the average power at a given frequency
    ''' <seealso cref="FFTAverageFromFrequency">FFTAverageFromFrequency</seealso>
    ''' </summary>
    ''' <param name="fftidx">The index in the transform for which we want to obtain the power for</param>
    ''' <param name="channel">The channel (left/right)</param>
    ''' <param name="bandWidth">if this value is larger than 0, the function will return the average of the adjacent bands</param>
    ''' <remarks>The average is calculated by averaging the values in the FFT history. The size of the history can be adjusted through the <see cref="FFTHistorySize">FFTHistorySize</see> property.</remarks>
    ''' <returns>The average power level at the given FFT index</returns>
    Public Function FFTAverageFromIndex(fftidx As Integer, channel As FFTChannelConstants, Optional bandWidth As Integer = 0) As Double
        Dim v As Double = 0

        If fftidx < fftMin Then
            fftidx = fftMin
        ElseIf fftidx > fftMax Then
            fftidx = fftMax - 1
        End If

        If bandWidth <= 0 Then
            For h As Integer = 0 To pFFTHistorySize - 1
                If channel = FFTChannelConstants.Left Then
                    v += FFTHistL(h)(fftidx - fftMin)
                Else
                    v += FFTHistR(h)(fftidx - fftMin)
                End If
            Next
        Else
            bandWidth -= bandWidth Mod 2
            For i As Integer = -bandWidth / 2 To bandWidth / 2
                v += FFTAverageFromIndex(fftidx - fftMin + i, channel)
            Next

            v /= (bandWidth + 1)
        End If

        Return v / pFFTHistorySize
    End Function

    Public Function FFTValueFromIndex(fftidx As Integer, channel As FFTChannelConstants) As Double
        Dim v As Double = 0

        If fftidx < fftMin Then
            fftidx = fftMin
        ElseIf fftidx > fftMax Then
            fftidx = fftMax - 1
        End If

        If channel = FFTChannelConstants.Left Then
            v = fftL(fftidx).R
        Else
            v = fftR(fftidx).R
        End If

        Return FFTApplyYScale(v, fftidx)
    End Function

    ''' <summary>
    ''' Returns the average power at a given frequency
    ''' <seealso cref="FFTAverageFromIndex">FFTAverageFromFrequency</seealso>
    ''' </summary>
    ''' <param name="freq">The frequency for which we want to obtain the power for</param>
    ''' <param name="channel">The channel (left/right)</param>
    ''' <remarks>The average is calculated by averaging the values in the FFT history. The size of the history can be adjusted through the <see cref="FFTHistorySize">FFTHistorySize</see> property.</remarks>
    ''' <returns>The average power level at the given frequency</returns>
    Public Function FFTAverageFromFrequency(freq As Double, channel As FFTChannelConstants) As Double
        Return FFTAverageFromIndex(Freq2FFTIdx(freq), channel)
    End Function
#End Region

#Region " Rendering "

#Region " Main Rendering "
    Friend Sub RePaint()
        'Using g As Graphics = Me.CreateGraphics()
        ' Me.InvokePaint(Me, New PaintEventArgs(g, Me.ClientRectangle))
        'End Using
        Me.Invalidate()
    End Sub

    Private Sub PreRender()
        Renderer.PrepareAddingObjects()

        Select Case pStyle
            Case StyleConstants.DigitalVU
            Case StyleConstants.Oscilloscope
                Renderer.AddLine(0, h4, w, h4, pGreenOff)
                Renderer.AddLine(0, h2h4, w, h2h4, pYellowOff)
            Case StyleConstants.FFT
                If pFFTStyle = FFTStyleConstants.Spectrum Then
                    RenderSpectrumScale()
                ElseIf pFFTRenderScales <> FFTRenderScalesConstants.None Then
                    RenderFFTScale()
                End If
            Case StyleConstants.UserPaintGDI
        End Select
    End Sub

    Private Sub RenderBuffer(buf() As Byte, isNew As Boolean)
        If buf Is Nothing Then Exit Sub

        Dim peak As Peak
        Dim fftStatus As PrepareFFTStatusConstants = PrepareFFTStatusConstants.Done

        pIsRendering = True

        Static nbuf() As Integer
        If isNew Then nbuf = NormalizeBuffer(buf)

        If pEnableRendering Then
            Select Case pStyle
                Case StyleConstants.DigitalVU
                    PreRender()
                    peak = RenderDigitalVUs(nbuf)
                    PostRender()
                Case StyleConstants.Oscilloscope
                    PreRender()
                    RenderOSC(nbuf)
                    PostRender()
                Case StyleConstants.FFT
                    fftStatus = PrepareFFT(nbuf)

                    If fftStatus = PrepareFFTStatusConstants.Done Then
                        If isNew Then PreRender()
                        Try
                            RenderFFT()
                        Catch ex As Exception
                            Diagnostics.Debug.WriteLine("RenderFFT: " + ex.Message)
                        End Try
                        If isNew Then PostRender()
                    End If
                Case StyleConstants.UserPaintGDI
                    peak = CalculateRMS(nbuf)
                    Me.Invalidate()
            End Select
        ElseIf nbuf.Length > 0 Then
            If isNew Then peak = CalculateRMS(nbuf)

            If pStyle = StyleConstants.FFT Then
                fftStatus = PrepareFFT(nbuf)
                If fftStatus = PrepareFFTStatusConstants.Done Then RenderFFT()
            End If
        End If

        pIsRendering = False

        If fftStatus = PrepareFFTStatusConstants.Done Then
            'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.PeakValues, buf, nbuf, peak}})
            RaiseEvent PeakValues(buf, nbuf, peak)
        End If
    End Sub

    Friend ReadOnly Property IsRendering() As Boolean
        Get
            Return pIsRendering
        End Get
    End Property

    Private Sub PostRender()
        Select Case pStyle
            Case StyleConstants.DigitalVU
            Case StyleConstants.Oscilloscope
                Renderer.AddLine(0, h2, w, h2, Color.LightGray)
            Case StyleConstants.FFT
                Select Case pFFTStyle
                    Case FFTStyleConstants.LedBars
                        For y As Integer = CInt(fftWH.Height + 2) To CInt(fft_h + fftWH.Height) Step 4
                            Renderer.AddLine(fftWH.Width, y, w, y, BackColor)
                        Next
                    Case FFTStyleConstants.Spectrum
                        If Not isMono Then Renderer.AddLine(fftWH.Width, h2, w, h2, plGray)
                        Renderer.AddLine(xSpectrum, 0, xSpectrum, h, plGray)
                End Select
                If cursorPos.CursorIsOnDisplay AndAlso pFFTStyle <> FFTStyleConstants.Spectrum Then
                    Renderer.AddLine(cursorPos.Position.X, 0, cursorPos.Position.X, h, Renderer.SetColorAlpha(Color.LightGray, 75))
                    Dim info As String = cursorPos.GetCursorPosInfo()
                    Renderer.AddString(info, pFFTScaleFont, Color.Black, cursorPos.PositionF.X + 1, cursorPos.PositionF.Y + 1)
                    Renderer.AddString(info, pFFTScaleFont, txtColor, cursorPos.PositionF.X, cursorPos.PositionF.Y)
                End If
            Case StyleConstants.UserPaintGDI
        End Select

        Renderer.DoneAddingGraphicObjects()
    End Sub

    Private Sub RenderOSC(buf() As Integer)
        Dim l As Double
        Dim r As Double
        Dim num As Integer = ((buf.Length - pChannels)) \ pChannels
        Dim pl(num - 1) As PointF
        Dim pr(num - 1) As PointF
        Dim k As Integer

        For i As Integer = 0 To buf.Length - pChannels - 1 Step pChannels
            l = buf(i) * h2 / 32768
            If pChannels = 1 Then
                r = l
            Else
                r = buf(i + 1) * h2 / 32768
            End If
            l += h4
            If l < 0 Then l = 0
            If l > h2 Then l = h2

            r += h2h4
            If r < h2 Then r = h2
            If r > h Then r = h

            pl(k).X = CSng(w * i / num)
            pl(k).Y = CSng(l)

            pr(k).X = pl(k).X
            pr(k).Y = CSng(r)

            k += 1
        Next

        Renderer.AddLines(pl, pGreenOn, pLinesThickness)
        Renderer.AddLines(pr, pYellowOn, pLinesThickness)
    End Sub


    Private Function RenderDigitalVUs(buf() As Integer) As Peak
        If Not VUsReady Then Exit Function

        Dim s As Single
        Dim ledL As RectangleF
        Dim ledR As RectangleF

        Dim p As Peak = CalculateRMS(buf)

        Select Case pOrientation
            Case OrientationConstants.Horizontal
                ledL = New RectangleF(New Point(0, 0), ledSize)
                ledR = New RectangleF(New Point(0, CInt(h2 + 1)), ledSize)
                s = CSng(w / pNumVUs)
            Case OrientationConstants.Vertical
                ledL = New RectangleF(New Point(0, CInt(h - s)), ledSize)
                ledR = New RectangleF(New Point(CInt(w2 + 1), CInt(h - s)), ledSize)
                s = CSng(h / pNumVUs)
        End Select

        p.Left = Int(p.Left * pNumVUs)
        p.Right = Int(p.Right * pNumVUs)

        For i As Integer = 0 To pNumVUs - 1
            If i <= pNumVUs / 2 - pNumVUs / 8 Then
                Renderer.AddFilledRectangle(ledL, If(p.Left > i, pGreenOn, pGreenOff))
                Renderer.AddFilledRectangle(ledR, If(p.Right > i, pGreenOn, pGreenOff))
            End If
            If i > pNumVUs / 2 - pNumVUs / 8 And i <= pNumVUs - pNumVUs / 4 Then
                Renderer.AddFilledRectangle(ledL, If(p.Left > i, pYellowOn, pYellowOff))
                Renderer.AddFilledRectangle(ledR, If(p.Right > i, pYellowOn, pYellowOff))
            End If
            If i > pNumVUs - pNumVUs / 4 Then
                Renderer.AddFilledRectangle(ledL, If(p.Left > i, pRedOn, pRedOff))
                Renderer.AddFilledRectangle(ledR, If(p.Right > i, pRedOn, pRedOff))
            End If

            Select Case pOrientation
                Case OrientationConstants.Horizontal
                    ledL.X += s
                    ledR.X += s
                Case OrientationConstants.Vertical
                    ledL.Y -= s
                    ledR.Y -= s
            End Select
        Next

        p.Left = p.Left / pNumVUs
        p.Right = p.Right / pNumVUs

        Return p
    End Function

    Private Enum PrepareFFTStatusConstants
        Done = 0
        BufferTooShort = 1
    End Enum

    Private Function PrepareFFT(buf() As Integer) As PrepareFFTStatusConstants
        Dim res As PrepareFFTStatusConstants

        Try
            If pOverlap > 0 AndAlso waveInIndex = 0 Then
                Dim overlap As Integer = pFFTSize * pOverlap
                Dim startIndex As Integer = pFFTSize - overlap
                Array.Copy(WAVHistL, startIndex, WAVHistL, 0, overlap)
                Array.Copy(WAVHistR, startIndex, WAVHistR, 0, overlap)
                waveInIndex = overlap
            End If

            Dim topBufferIndex = buf.Length - pChannels + 1
            Dim channelOffset = pChannels - 1

            Do
                If bufferIndex >= topBufferIndex Then
                    bufferIndex = 0
                    If waveInIndex >= pFFTSize Then
                        waveInIndex = 0
                        res = PrepareFFTStatusConstants.Done
                    Else
                        res = PrepareFFTStatusConstants.BufferTooShort
                    End If
                    Exit Do
                ElseIf waveInIndex >= pFFTSize Then
                    waveInIndex = 0
                    res = PrepareFFTStatusConstants.Done
                    Exit Do
                End If

                WAVHistL(waveInIndex) = buf(bufferIndex)
                WAVHistR(waveInIndex) = buf(bufferIndex + channelOffset)
                bufferIndex += pChannels
                waveInIndex += 1
            Loop
        Catch ex As Exception
            bufferIndex = 0
            waveInIndex = 0
            res = PrepareFFTStatusConstants.Done

            Diagnostics.Debug.WriteLine("PrepareFFT: " + ex.Message)
        End Try

        If res = PrepareFFTStatusConstants.Done AndAlso pFFTDetectDTMF Then DetectDTMF()

        Return res
    End Function

    Dim f As BiQuadFilter

    Private Sub RenderFFT()
        Dim p(pFFTSize2 - 1) As PointF

        For idx As Integer = 0 To pFFTSize - 1
            waveL(idx) = WAVHistL(idx) * windowValues(idx)
            waveR(idx) = WAVHistR(idx) * windowValues(idx)

            For Each filter In pFilters
                waveL(idx) = filter.ApplyFilter(waveL(idx))
                waveR(idx) = filter.ApplyFilter(waveR(idx)) ' FIXME: We should have two filters, one for each channel
            Next
        Next

        ' ALPHA CODE *******************
        'If pFilters.Count > 0 Then
        '    If f Is Nothing Then
        '        f = New BiQuadFilter(pFilters(0).FilterType, pFilters(0).Gain, pFilters(0).Frequency, pFilters(0).SamplingRate, pFilters(0).BandWidth)
        '    Else
        '        f.FilterType = pFilters(0).FilterType
        '        f.Gain = pFilters(0).Gain
        '        f.Frequency = pFilters(0).Frequency
        '        f.BandWidth = pFilters(0).BandWidth
        '    End If

        '    Dim df = 0.5 / (1 / pFFTSize) / (pFFTSize2 - 1)
        '    For idx2 As Integer = 0 To pFFTSize2 - 1
        '        Dim re As Double = 0
        '        Dim im As Double = 0

        '        For idx As Integer = 0 To pFFTSize - 1
        '            Dim fv As Double = idx2 * df
        '            Dim ir As Double = f.ApplyFilter(idx) * windowValues(idx)
        '            re += ir * Math.Cos(2 * Math.PI * fv * idx)
        '            im -= ir * Math.Sin(2 * Math.PI * fv * idx)
        '        Next
        '        p(idx2) = New PointF(FFTIdx2X(idx2, fftMin, fftMax, fft_w),
        '                             h2 - FFTIdx2Y(f.Gain * Math.Log10(re * re + im * im), fftMin, fftMax, fft_h2))
        '    Next
        '    Renderer.AddLines(p, Pens.White)
        'End If
        ' ALPHA CODE *******************

        FourierTransform(pFFTSize, waveL, fftL, waveR, fftR, False)

        'fftL = FFT.FFT1r(1, waveL, CInt(Log(pFFTSize, 2)))
        'fftR = FFT.FFT1r(1, waveR, CInt(Log(pFFTSize, 2)))

        If pFFTStyle = FFTStyleConstants.TransferFunction Then
            ' Transfer Function: Pxy/Pxx
            ' Where Pxy is the cross spectrum between signals x & y and Pxx is the power spectrum of signal x (see http://www.mathworks.com/help/toolbox/signal/tfestimate.html)
            ' See also: http://www.dsprelated.com/dspbooks/filters/Transfer_Function_Analysis.html
            ' Cross Spectrum: http://www.diracdelta.co.uk/science/source/c/r/cross%20spectrum/source.html
            ' Cross Correlation: http://www.diracdelta.co.uk/science/source/c/r/cross%20correlation/source.html

            Dim tf() As ComplexDouble = TransferFunction(fftL, fftR, pFFTSize)
            Dim ch() As ComplexDouble = Coherence(fftL, fftR, pFrequency)

            For h As Integer = 0 To pCOHHistorySize - 2
                For i As Integer = 0 To pFFTSize - 1
                    COHHistory(h)(i) = COHHistory(h + 1)(i)
                Next
            Next

            For i As Integer = 0 To pFFTSize - 1
                fftL(i) = tf(i) * 10000

                fftR(i) = New ComplexDouble()
                'COHHistory(COHHistory.Length - 1)(i) = (1 - ch(i)) * tf(i).Abs() ^ 2 * 1000000000000000
                COHHistory(COHHistory.Length - 1)(i) = ch(i) * tf(i).Abs() ^ 2
                For j As Integer = 0 To pCOHHistorySize - 1
                    fftR(i) += COHHistory(j)(i)
                Next
                fftR(i) /= (COHHistory.Length - 1)
            Next
        End If

        For h As Integer = 0 To pFFTHistorySize - 2
            For idx As Integer = 0 To (fftMax - fftMin) - 1
                FFTHistL(h)(idx) = FFTHistL(h + 1)(idx)
                FFTHistR(h)(idx) = FFTHistR(h + 1)(idx)
            Next
        Next

        If pEnableRendering Then
            Select Case pFFTStyle
                Case FFTStyleConstants.Bars, FFTStyleConstants.FilledBars, FFTStyleConstants.LedBars
                    RenderFFTBars()
                Case FFTStyleConstants.Spectrum
                    RenderFFTSpectrum()
                Case Else
                    RenderFFTLines()
            End Select
        Else
            For fftIdx As Integer = fftMin To fftMax - 1
                FFTHistL(pFFTHistorySize - 1)(fftIdx - fftMin) = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Left)
                FFTHistR(pFFTHistorySize - 1)(fftIdx - fftMin) = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Right)
            Next
        End If

        'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.FFTFrame, fftL, fftR}})
        RaiseEvent FFTFrame(fftL, fftR)
    End Sub

    Private dtmfLowFrequencies() As Integer = New Integer() {697, 770, 852, 941, 480, 440, 350}
    Private dtmfHighFrequencies() As Integer = New Integer() {1209, 1336, 1477, 1633, 620, 480, 440}

    Private Sub DetectDTMF()
        Dim tone As DTMFToneConstants = DTMFToneConstants.DTMFInvalid
        Dim avgLevel As Double

        Dim max As Double = Double.MinValue
        Dim min As Double = Double.MaxValue
        For h As Integer = 0 To pFFTHistorySize - 1
            avgLevel += FFTHistL(h).Average()
        Next
        avgLevel /= pFFTHistorySize

        Dim lf(6) As Double
        Dim hf(6) As Double

        For i As Integer = 0 To 6
            lf(i) = TestDTMFFreq(dtmfLowFrequencies(i), avgLevel)
            hf(i) = TestDTMFFreq(dtmfHighFrequencies(i), avgLevel)
        Next

        Dim maxLf = lf.Max()
        Dim maxHf = hf.Max()

        If Abs(maxLf - maxHf) > 0.1 OrElse maxLf <= avgLevel OrElse maxHf <= avgLevel Then Exit Sub

        Dim checkOtherFreqs = New Func(Of Integer, Integer, Boolean)(Function(testLow, testHi)
                                                                         For i As Integer = 0 To 6
                                                                             If lf(i) > lf(testLow) Then Return False
                                                                             If hf(i) > hf(testHi) Then Return False
                                                                         Next
                                                                         Return True
                                                                     End Function
        )

        Dim testTone As DTMFToneConstants
        For lfi As Integer = 0 To 6
            For hfi As Integer = 0 To 6
                If lf(lfi) = maxLf AndAlso hf(hfi) = maxHf Then 'AndAlso checkOtherFreqs(lfi, hfi) Then
#If NET40 = True Then
                    ' For .NET 4
                    If [Enum].TryParse(lfi * 7 + hfi, testTone) Then
                        tone = testTone
                        Exit For
                    End If
#Else
                    ' For .NET 3.5
                    If [Enum].IsDefined(GetType(DTMFToneConstants), lfi * 7 + hfi) Then
                        testTone = CType([Enum].Parse(GetType(DTMFToneConstants), lfi * 7 + hfi), DTMFToneConstants)
                        Exit For
                    End If
#End If
                End If
            Next
            If tone <> DTMFToneConstants.DTMFInvalid Then Exit For
        Next

        If dtmfDetectedTone <> tone Then
            dtmfDetectedTone = tone

            If tmrTriggerDTMF IsNot Nothing Then tmrTriggerDTMF.Dispose()

            If tone = DTMFToneConstants.DTMFInvalid Then
                'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.DTMFToneUp}})
                RaiseEvent DTMFToneUp()
            Else
                tmrTriggerDTMF = New Timer(New TimerCallback(AddressOf TriggerDTMFEventDown), Nothing, 70, Timeout.Infinite)
            End If
        End If
    End Sub

    Private Function TestDTMFFreq(f As Integer, l As Double) As Double
        Dim fl = f - f * 0.1
        Dim fr = f + f * 0.1

        Dim fv As Double
        Dim vc As Integer

        Dim ft As Double
        Dim tc As Integer

        For i = fl To fr
            If Abs(i - f) <= 20 Then
                fv += FFTAverageFromFrequency(i, FFTChannelConstants.Left)
                vc += 1
            ElseIf Abs(i - f) > 20 Then
                ft += FFTAverageFromFrequency(i, FFTChannelConstants.Left) * 0.9
                tc += 1
            End If
        Next
        fv /= vc
        ft /= tc

        If fv > ft AndAlso fv > l Then
            Return fv - l
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' This function converts a <see cref="DTMFToneConstants">tone</see> into its corresponding string value.
    ''' <seealso cref="DetectDTMF">DetectDTMF</seealso>
    ''' <seealso cref="DTMFToneConstants">DTMFToneConstants</seealso>
    ''' </summary>
    ''' <param name="tone">One of the possible values from the <see cref="DTMFToneConstants">DTMFToneConstants</see> enumeration</param>
    ''' <returns>Returns a string representing the DTMF tone</returns>
    Public Shared Function DTMFToneToValue(tone As DTMFToneConstants) As String
        Select Case tone
            Case DTMFToneConstants.DTMF00 : Return "0"
            Case DTMFToneConstants.DTMF01 : Return "1"
            Case DTMFToneConstants.DTMF02 : Return "2"
            Case DTMFToneConstants.DTMF03 : Return "3"
            Case DTMFToneConstants.DTMF04 : Return "4"
            Case DTMFToneConstants.DTMF05 : Return "5"
            Case DTMFToneConstants.DTMF06 : Return "6"
            Case DTMFToneConstants.DTMF07 : Return "7"
            Case DTMFToneConstants.DTMF08 : Return "8"
            Case DTMFToneConstants.DTMF09 : Return "9"
            Case DTMFToneConstants.DTMFA : Return "A"
            Case DTMFToneConstants.DTMFB : Return "B"
            Case DTMFToneConstants.DTMFC : Return "C"
            Case DTMFToneConstants.DTMFD : Return "D"
            Case DTMFToneConstants.DTMFPound : Return "#"
            Case DTMFToneConstants.DTMFStar : Return "*"
            Case DTMFToneConstants.DTMFDialTone : Return "Dial Tone"
            Case DTMFToneConstants.DTMFBusyTone : Return "Busy Tone"
            Case DTMFToneConstants.DTMFRingbackTone : Return "Ringback Tone"
            Case DTMFToneConstants.DTMFInvalid : Return "Invalid"
            Case Else : Return ""
        End Select
    End Function

    Private Sub TriggerDTMFEventDown(state As Object)
        'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.DTMFToneDown, dtmfDetectedTone}})
        RaiseEvent DTMFToneDown(dtmfDetectedTone)
    End Sub
#End Region

#Region " FFT "
    Private Sub RenderBSplineCurve(fromPoint As Integer, pl() As PointF, pr() As PointF, ByRef k As Integer)
        Dim ts As Double

        Dim x(4) As Double
        Dim yl(4) As Double
        Dim yr(4) As Double
        Dim i As Integer
        Dim c As Integer = 0

        If pFFTXScale = FFTXScaleConstants.Logarithmic Then
            c = 74 * 0
        End If

        For i = 0 To 3
            ' TODO: fix problem with log scale/zoom min/max adjustment
            x(i) = FFTIdx2X(fromPoint + i, fftMin, fftMax, fft_w + c) - c

            yl(i) = FFTAverageFromIndex(fromPoint + i, FFTChannelConstants.Left)
            yr(i) = FFTAverageFromIndex(fromPoint + i, FFTChannelConstants.Right)
        Next

        Dim a(3) As Double
        Dim bl(3) As Double
        Dim br(3) As Double

        a(0) = (-x(0) + 3 * x(1) - 3 * x(2) + x(3)) / 6.0
        a(1) = (3 * x(0) - 6 * x(1) + 3 * x(2)) / 6.0
        a(2) = (-3 * x(0) + 3 * x(2)) / 6.0
        a(3) = (x(0) + 4 * x(1) + x(2)) / 6.0

        bl(0) = (-yl(0) + 3 * yl(1) - 3 * yl(2) + yl(3)) / 6.0
        bl(1) = (3 * yl(0) - 6 * yl(1) + 3 * yl(2)) / 6.0
        bl(2) = (-3 * yl(0) + 3 * yl(2)) / 6.0
        bl(3) = (yl(0) + 4 * yl(1) + yl(2)) / 6.0

        br(0) = (-yr(0) + 3 * yr(1) - 3 * yr(2) + yr(3)) / 6.0
        br(1) = (3 * yr(0) - 6 * yr(1) + 3 * yr(2)) / 6.0
        br(2) = (-3 * yr(0) + 3 * yr(2)) / 6.0
        br(3) = (yr(0) + 4 * yr(1) + yr(2)) / 6.0

        pl(k).X = CSng(a(3))
        pl(k).Y = CSng(fft_h2 - bl(3) * fft_h2 + fftWH.Height)
        pr(k).X = pl(k).X
        pr(k).Y = CSng(fft_h - br(3) * fft_h2 + fftWH.Height)

        k += 1

        For i = 1 To pFFTSmoothing - 1
            ts = i / pFFTSmoothing

            yl(4) = bl(3) + ts * (bl(2) + ts * (bl(1) + ts * bl(0)))
            yr(4) = br(3) + ts * (br(2) + ts * (br(1) + ts * br(0)))
            x(4) = a(3) + ts * (a(2) + ts * (a(1) + ts * a(0)))

            pl(k).X = CSng(x(4))
            pl(k).Y = CSng(fft_h2 - yl(4) * fft_h2 + fftWH.Height)
            pr(k).X = pl(k).X
            pr(k).Y = CSng(fft_h - yr(4) * fft_h2 + fftWH.Height)

            k += 1
        Next
    End Sub

    Private Sub RenderSpectrumScale()
        Dim txt As String
        Dim p1 As New PointF(0, 0)
        Dim ly As Single = -999
        Dim sh As Integer = If(isMono, h, h2)

        If pFFTRenderScales = FFTRenderScalesConstants.Both Or pFFTRenderScales = FFTRenderScalesConstants.Vertical Then
            For fftidx As Integer = fftMin To fftMax - 1
                p1.Y = CSng(FFTIdx2Y(fftidx, fftMin, fftMax, sh))
                If p1.Y - ly > pFFTScaleFont.Height * 1.5 Then
                    txt = CStr(Round(FFTIdx2Freq(fftidx) / 1000, 1))
                    If txt.IndexOf(".") = -1 Then txt += ".0"
                    txt += "Khz"

                    Renderer.AddString(txt, pFFTScaleFont, plGray, fftWH.Width - pFFTScaleFontSize.Width / 2 * txt.Length, fft_h2 - p1.Y - pFFTScaleFontSize.Height / 2)

                    If pChannels = 2 Then
                        If fft_h - p1.Y - pFFTScaleFontSize.Height > fft_h2 + pFFTScaleFontSize.Height Then
                            Renderer.AddString(txt, pFFTScaleFont, plGray, fftWH.Width - pFFTScaleFontSize.Width / 2 * txt.Length, fft_h - p1.Y - pFFTScaleFontSize.Height)
                        End If
                    End If

                    ly = p1.Y
                End If
            Next
        End If

        Renderer.AddLine(fftWH.Width + 4, 0, fftWH.Width + 4, h, plGray)
    End Sub

    Private Sub RenderFFTScale(Optional renderImage As Boolean = False)
        Dim fftIdx As Integer
        Dim p1 As New PointF(0, 0)
        Dim f As Integer
        Dim txt As String
        Dim smin As Double = fftMin
        Dim smax As Double = fftMax
        Dim lx As Double
        Dim ly As Double = -999
        Dim scaleStep As Integer
        Dim fftH2 As Integer = CInt(fftWH.Height / 2)
        Dim c As Color
        Dim drawGridLines As Boolean = (pFFTStyle <> FFTStyleConstants.LedBars)
        Dim fullScale As Boolean = isMono

        If (pFFTStyle = FFTStyleConstants.Line OrElse pFFTStyle = FFTStyleConstants.Filled) AndAlso pFFTLineChannelMode <> FFTLineChannelModeConstants.Normal Then fullScale = True

        If pFFTRenderScales = FFTRenderScalesConstants.Both OrElse pFFTRenderScales = FFTRenderScalesConstants.Vertical Then
            ' Render dB scale (y)
            Dim sh As Integer = CInt(If(fullScale, h, h2))
            Dim yMin As Integer
            Dim yMax As Integer
            Dim yUnit As String

            If (pFFTStyle = FFTStyleConstants.Line OrElse pFFTStyle = FFTStyleConstants.Filled) AndAlso pFFTLineChannelMode = FFTLineChannelModeConstants.Phase Then
                yMin = 0
                yMax = 180
                yUnit = "°"
            Else
                yMin = -150
                yMax = 0
                yUnit = "dB"
            End If

            For y As Integer = yMin To yMax Step CInt(fftWH.Height / If(isMono, 2, 1))
                p1.Y = CSng(Abs(yMax - y) / Abs(yMax - yMin) * sh + fftWH.Height)

                If p1.Y > sh - fftWH.Height Then Continue For
                If Abs(p1.Y - ly) > fftWH.Height * 2 Then
                    ly = p1.Y

                    If drawGridLines Then
                        If y Mod 2 = 0 Then
                            c = Renderer.SetColorAlpha(axisColor, 85)
                        Else
                            c = Renderer.SetColorAlpha(axisColor, 65)
                        End If
                        Renderer.AddLine(fftWH.Width, p1.Y, w, p1.Y, c)
                    End If

                    txt = y.ToString + yUnit
                    Renderer.AddString(txt, pFFTScaleFont, plGray, fftWH.Width - pFFTScaleFontSize.Width / 2 * txt.Length - 2, p1.Y - fftH2)
                    If Not fullScale Then
                        p1.Y = CSng(p1.Y - fftWH.Height + h2)
                        Renderer.AddString(txt, pFFTScaleFont, plGray, fftWH.Width - pFFTScaleFontSize.Width / 2 * txt.Length - 2, p1.Y - fftH2)
                        Renderer.AddLine(fftWH.Width, p1.Y, w, p1.Y, c)
                    End If
                End If
            Next
            Renderer.AddLine(fftWH.Width, h - fftWH.Height, w, h - fftWH.Height, axisColor)
            If Not fullScale Then Renderer.AddLine(fftWH.Width, h2, w, h2, plGray)
        End If

        If pFFTRenderScales = FFTRenderScalesConstants.Both Or pFFTRenderScales = FFTRenderScalesConstants.Horizontal Then
            ' Render Hz scale (x)
            If Not pFFTXZoom Then
                smin = 0
                smax = pFFTSize2
            End If
            lx = -999
            scaleStep = CInt(1 / fft_w * (smax - smin) * If(pFFTXScale = FFTXScaleConstants.Logarithmic, 10, 50))
            If scaleStep < 1 Then scaleStep = 1
            For fftIdx = CInt(smin) To CInt(smax - 1) Step scaleStep
                p1.X = CSng(FFTIdx2X(fftIdx, fftMin, fftMax, fft_w))

                f = CInt(FFTIdx2Freq(fftIdx))
                txt = HumanFreq(f)

                If fftIdx > If(pFFTXZoom, smin, 0) Then p1.X = CSng(p1.X - pFFTScaleFontSize.Width * txt.Length / 2)
                If p1.X - lx > 40 Or pFFTXScale = FFTXScaleConstants.Normal Then
                    If drawGridLines Then Renderer.AddLine(p1.X, fftWH.Height, p1.X, fft_h + fftWH.Height, axisColor)

                    c = Renderer.SetColorAlpha(plGray, If(fftIdx Mod 2 = 0, 95, 75))
                    Renderer.AddString(txt, pFFTScaleFont, c, p1.X, 0)
                    Renderer.AddString(txt, pFFTScaleFont, c, p1.X, CSng(h - pFFTScaleFontSize.Height))
                    lx = p1.X
                End If
            Next fftIdx
        End If
    End Sub

    Private Sub RenderFFTSpectrum()
        Dim yl As Double
        Dim yr As Double
        Dim lc As Color
        Dim rc As Color
        Dim c As Double
        Dim mp As Double = FFTIdx2Y(pFFTSize2, fftMin, fftMax, fft_h2)
        Dim ys As Double = FFTIdx2Y(fftMin + 1, fftMin, fftMax, fft_h2) - FFTIdx2Y(fftMin, fftMin, fftMax, fft_h2)

        For fftIdx As Integer = fftMin To fftMax - 1
            c = FFTIdx2Y(fftIdx, fftMin, fftMax, fft_h2)
            yl = fft_h2 - c
            yr = fft_h - c

            FFTHistL(pFFTHistorySize - 1)(fftIdx - fftMin) = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Left)
            FFTHistR(pFFTHistorySize - 1)(fftIdx - fftMin) = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Right)

            AvgL = FFTAverageFromIndex(fftIdx, FFTChannelConstants.Left)
            AvgR = FFTAverageFromIndex(fftIdx, FFTChannelConstants.Right)

            lc = Color.FromArgb(255, HSLtoRGB(CInt(Abs(255 - 360 * AvgL)), 255, CInt(AvgL * 255)))
            rc = Color.FromArgb(255, HSLtoRGB(CInt(Abs(255 - 360 * AvgR)), 255, CInt(AvgR * 255)))

            Renderer.AddFilledRectangle(xSpectrum - 1, yl, 1, ys, lc)
            Renderer.AddFilledRectangle(xSpectrum - 1, yr, 1, ys, rc)

            ' What's this???
            'If Not pFFTXZoom AndAlso max < pFFTSize2 Then
            '    Renderer.AddLines(New PointF() {New PointF(CSng(xSpectrum), CSng(mp)), New PointF(CSng(xSpectrum), 0)}, Me.BackColor)
            '    Renderer.AddLines(New PointF() {New PointF(CSng(xSpectrum), CSng(fft_h2 + mp)), New PointF(CSng(xSpectrum), CSng(fft_h2))}, Me.BackColor)
            'End If
        Next fftIdx

        xSpectrum += 1
        If xSpectrum > w Then xSpectrum = fftWH.Width + 4 + 2
        'If xSpectrum > w Then
        '    xSpectrum -= 1
        '    Renderer.ScrollBackground(1)
        'End If
    End Sub

    Private Function Octave2FFTIdx(o As Single) As Integer
        Dim f As Single = CSng(2 ^ o * CCursorPos.Note2Freq(0))
        If f >= pFrequency2 Then
            Return Freq2FFTIdx(pFrequency2) - 1
        Else
            Return Freq2FFTIdx(f)
        End If
    End Function

    Private forceResetFFTArrays As Boolean

    Private Function UpdateFFTPointsArrays() As Integer
        Static lastNumOfPoints As Integer
        Dim numOfPoints As Integer

        If pFFTSmoothing > 0 Then
            numOfPoints = (fftMax - fftMin - 3) * pFFTSmoothing - (pFFTSmoothing - 1) - If(fftMin = 0, 1, 2)
        Else
            numOfPoints = fftMax - fftMin - 1
        End If
        If pFFTStyle = FFTStyleConstants.Filled Then numOfPoints += 2

        If lastNumOfPoints <> numOfPoints OrElse forceResetFFTArrays Then
            If numOfPoints < 0 Then Exit Function
            forceResetFFTArrays = False

            ReDim maxFFTL(numOfPoints)
            ReDim maxFFTR(numOfPoints)
            ReDim maxTimerFFTL(numOfPoints)
            ReDim maxTimerFFTR(numOfPoints)
            ReDim Preserve fftPL(numOfPoints)
            ReDim Preserve fftPR(numOfPoints)
            ReDim minFFTL(numOfPoints)
            ReDim minFFTR(numOfPoints)
            ReDim minTimerFFTL(numOfPoints)
            ReDim minTimerFFTR(numOfPoints)

            If lastNumOfPoints > 0 Then
                For i = lastNumOfPoints To numOfPoints
                    fftPL(i) = fftPL(lastNumOfPoints - 1)
                    fftPR(i) = fftPR(lastNumOfPoints - 1)
                Next
                fftPL(numOfPoints - 1).Y = fft_h2
                fftPL(numOfPoints) = New Point(fftPL(0).X, fft_h2)
                fftPR(numOfPoints - 1).Y = fft_h2
                fftPR(numOfPoints) = New Point(fftPR(0).X, fft_h2)
            End If

            For i As Integer = 0 To numOfPoints
                maxFFTL(i) = fftPL(i)
                maxFFTR(i) = fftPR(i)
                maxTimerFFTL(i) = pFFTPeaksDecayDelay
                maxTimerFFTR(i) = pFFTPeaksDecayDelay
                minFFTL(i) = fftPL(i)
                minFFTR(i) = fftPR(i)
                minTimerFFTL(i) = 0
                minTimerFFTR(i) = 0
            Next

            lastNumOfPoints = numOfPoints
        End If

        Return numOfPoints
    End Function

    Private Sub RenderFFTLines()
        Dim numOfPoints As Integer = UpdateFFTPointsArrays()
        Dim currentPoint As Integer
        Dim l As Double
        Dim r As Double

        For fftIdx As Integer = fftMin To fftMax - 1
            If pFFTLineChannelMode = FFTLineChannelModeConstants.Phase Then
                l = FFTPhaseFromIndex(fftIdx, FFTChannelConstants.Left) * 180 / PI
                r = FFTPhaseFromIndex(fftIdx, FFTChannelConstants.Right) * 180 / PI

                If l < 0 Then l = Abs(l) + 180
                If r < 0 Then r = Abs(r) + 180

                FFTHistL(pFFTHistorySize - 1)(fftIdx) = Abs(l - r) / 180
            Else
                If pFFTStyle = FFTStyleConstants.TransferFunction Then
                    l = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Left)
                    r = (1 - FFTPowerFromIndex(fftIdx, FFTChannelConstants.Right))
                Else
                    l = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Left)
                    r = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Right)
                End If

                FFTHistL(pFFTHistorySize - 1)(fftIdx - fftMin) = l
                FFTHistR(pFFTHistorySize - 1)(fftIdx - fftMin) = r
            End If

            If pFFTSmoothing > 0 Then
                If fftIdx - fftMin >= 4 Then
                    RenderBSplineCurve(fftIdx - 4, fftPL, fftPR, currentPoint)
                    Continue For
                ElseIf fftIdx > 0 Then
                    Continue For
                End If
            End If

            fftPL(currentPoint).X = CSng(FFTIdx2X(fftIdx, fftMin, fftMax, fft_w))
            fftPR(currentPoint).X = fftPL(currentPoint).X
            fftPL(currentPoint).Y = CSng(fft_h2 - FFTAverageFromIndex(fftIdx, FFTChannelConstants.Left) * fft_h2 + fftWH.Height)
            fftPR(currentPoint).Y = CSng(fft_h - FFTAverageFromIndex(fftIdx, FFTChannelConstants.Right) * fft_h2 + fftWH.Height)

            currentPoint += 1
        Next

        If pFFTPlotNoiseReduction > 0 Then
            Dim npL(fftPL.Length - 1) As PointF
            Dim npR(fftPR.Length - 1) As PointF

            Array.Copy(fftPL, npL, fftPL.Length)
            Array.Copy(fftPR, npR, fftPR.Length)

            Dim len2 As Integer = pFFTPlotNoiseReduction * 2 + 1
            Dim avgL As Double
            Dim avgR As Double
            For i As Integer = 0 To numOfPoints - pFFTPlotNoiseReduction
                avgL = 0
                avgR = 0

                For k = i - pFFTPlotNoiseReduction To i + pFFTPlotNoiseReduction
                    avgL += If(k < 0, fftPL(0).Y, fftPL(k).Y)
                    avgR += If(k < 0, fftPR(0).Y, fftPR(k).Y)
                Next

                npL(i).Y = avgL / len2
                npR(i).Y = avgR / len2
            Next
            fftPL = npL
            fftPR = npR

            'Dim len2 As Integer = pFFTPlotNoiseReduction * 2 + 1
            'Dim avgL As Double
            'Dim avgR As Double
            'For i As Integer = 1 To numOfPoints - pFFTPlotNoiseReduction
            '    avgL = 0
            '    avgR = 0

            '    For k = i - pFFTPlotNoiseReduction To i + pFFTPlotNoiseReduction
            '        avgL += If(k < 0, fftPL(0).Y, fftPL(k).Y)
            '        avgR += If(k < 0, fftPR(0).Y, fftPR(k).Y)
            '    Next

            '    fftPL(i).Y = avgL / len2
            '    fftPR(i).Y = avgR / len2
            'Next
        End If

        If pFFTHoldMinPeaks OrElse pFFTHoldMaxPeaks OrElse pFFTShowMinMaxRange Then
            For i As Integer = 0 To numOfPoints
                If pFFTHoldMaxPeaks OrElse pFFTShowMinMaxRange Then
                    maxFFTL(i).X = fftPL(i).X
                    If fftPL(i).Y < maxFFTL(i).Y Then
                        maxFFTL(i).Y = fftPL(i).Y
                        maxTimerFFTL(i) = pFFTPeaksDecayDelay
                    ElseIf maxTimerFFTL(i) <= 0 Then
                        maxFFTL(i).Y += CSng(Sqrt(fftPL(i).Y - maxFFTL(i).Y) / pFFTPeaksDecaySpeed)
                    Else
                        maxTimerFFTL(i) -= 1
                    End If

                    maxFFTR(i).X = fftPR(i).X
                    If fftPR(i).Y < maxFFTR(i).Y Then
                        maxFFTR(i).Y = fftPR(i).Y
                        maxTimerFFTR(i) = pFFTPeaksDecayDelay
                    ElseIf maxTimerFFTR(i) <= 0 Then
                        maxFFTR(i).Y += CSng(Sqrt(fftPR(i).Y - maxFFTR(i).Y) / pFFTPeaksDecaySpeed)
                    Else
                        maxTimerFFTR(i) -= 1
                    End If
                End If

                If pFFTHoldMinPeaks OrElse pFFTShowMinMaxRange Then
                    minFFTL(i).X = fftPL(i).X
                    If fftPL(i).Y > minFFTL(i).Y Then
                        minFFTL(i).Y = fftPL(i).Y
                        minTimerFFTL(i) = pFFTPeaksDecayDelay
                    ElseIf minTimerFFTL(i) <= 0 Then
                        minFFTL(i).Y -= CSng(Sqrt(minFFTL(i).Y - fftPL(i).Y) / pFFTPeaksDecaySpeed)
                    Else
                        minTimerFFTL(i) -= 1
                    End If

                    minFFTR(i).X = fftPR(i).X
                    If fftPR(i).Y > minFFTR(i).Y Then
                        minFFTR(i).Y = fftPR(i).Y
                        minTimerFFTR(i) = pFFTPeaksDecayDelay
                    ElseIf minTimerFFTR(i) <= 0 Then
                        minFFTR(i).Y -= CSng(Sqrt(minFFTR(i).Y - fftPR(i).Y) / pFFTPeaksDecaySpeed)
                    Else
                        minTimerFFTR(i) -= 1
                    End If
                End If
            Next
        End If

        Dim closed As Boolean
        If pFFTStyle = FFTStyleConstants.Filled Then
            fftPL(currentPoint).X = fftPL(currentPoint - 1).X
            fftPL(currentPoint).Y = CSng(fft_h2 + fftWH.Height)
            fftPL(currentPoint + 1).X = fftPL(0).X
            fftPL(currentPoint + 1).Y = fftPL(currentPoint).Y

            fftPR(currentPoint).X = fftPR(currentPoint - 1).X
            fftPR(currentPoint).Y = CSng(fft_h + fftWH.Height)
            fftPR(currentPoint + 1).X = fftPR(0).X
            fftPR(currentPoint + 1).Y = fftPR(currentPoint).Y

            closed = True
        Else
            closed = False
        End If

        Dim cMode As FFTLineChannelModeConstants = If(pChannels = 1, FFTLineChannelModeConstants.Normal, pFFTLineChannelMode)
        Select Case cMode
            Case FFTLineChannelModeConstants.Normal, FFTLineChannelModeConstants.RightOverLeft
                RenderFFTLinesHelper(numOfPoints, fftPL, fftPR, minFFTL, minFFTR, maxFFTL, maxFFTR, False, pChannels, closed)
            Case FFTLineChannelModeConstants.LeftOverRight
                RenderFFTLinesHelper(numOfPoints, fftPR, fftPL, minFFTR, minFFTL, maxFFTR, maxFFTL, True, pChannels, closed)
            Case FFTLineChannelModeConstants.Phase
                RenderFFTLinesHelper(numOfPoints, fftPL, fftPR, minFFTL, minFFTR, maxFFTL, maxFFTR, False, 1, closed)
        End Select
    End Sub

    Private Sub RenderFFTLinesHelper(numOfPoints As Integer, l() As PointF, r() As PointF, minL() As PointF, minR() As PointF, maxL() As PointF, maxR() As PointF, switchColors As Boolean, nChannels As Integer, closed As Boolean)
        Dim go As Color = If(switchColors, pYellowOn, pGreenOn)
        Dim gf As Color = If(switchColors, pYellowOff, pGreenOff)
        Dim yo As Color = If(switchColors, pGreenOn, pYellowOn)
        Dim yf As Color = If(switchColors, pGreenOff, pYellowOff)

        If pFFTHoldMinPeaks AndAlso pLinesThickness > 0 Then
            Renderer.AddLines(minL, gf, pLinesThickness, closed)
            If nChannels > 1 Then Renderer.AddLines(minR, yf, pLinesThickness, closed)
        End If

        If pFFTHoldMaxPeaks AndAlso pLinesThickness > 0 Then
            Renderer.AddLines(maxL, gf, pLinesThickness, closed)
            If nChannels > 1 Then Renderer.AddLines(maxR, yf, pLinesThickness, closed)
        End If

        If pFFTShowMinMaxRange AndAlso pFFTStyle = FFTStyleConstants.Line Then
            Dim ptsL(numOfPoints * 2 + 1) As PointF
            Dim ptsR(numOfPoints * 2 + 1) As PointF
            Dim tmp() As PointF

            Array.Copy(minL, ptsL, numOfPoints + 1)
            tmp = CType(maxL.Clone(), PointF())
            Array.Reverse(tmp)
            Array.Copy(tmp, 0, ptsL, numOfPoints + 1, numOfPoints + 1)
            Renderer.AddLines(ptsL, Color.FromArgb(128, gf), pLinesThickness, True)

            If nChannels > 1 Then
                Array.Copy(minR, ptsR, numOfPoints + 1)
                tmp = CType(maxR.Clone(), PointF())
                Array.Reverse(tmp)
                Array.Copy(tmp, 0, ptsR, numOfPoints + 1, numOfPoints + 1)
                Renderer.AddLines(ptsR, Color.FromArgb(128, yf), pLinesThickness, True)
            End If
        End If

        If pLinesThickness > 0 Then
            Renderer.AddLines(l, go, pLinesThickness, closed)
            If nChannels > 1 Then Renderer.AddLines(r, yo, pLinesThickness, closed)
        End If
    End Sub

    Private Sub RenderFFTBars()
        Dim fftIdx As Integer
        Dim lastfftIdx As Integer
        Dim resetInterpolation As Boolean
        Dim x As Double
        Dim lx As Double

        Dim xmin As Double
        Dim xmax As Double

        Dim barWidth As Double = fft_w / (If(pFFTXZoom, (fftMax - fftMin), pFFTSize2) / If(pFFTStyle = FFTStyleConstants.LedBars, 8, 6))
        Dim barSep As Double = barWidth + 2

        If pFFTXZoom Then
            xmin = fftWH.Width
            xmax = fft_w '- fftWH.Width
        Else
            xmin = (pFFTXMin + pFFTXZoomWindowPos) / pFrequency2 * fft_w + fftWH.Width
            xmax = (pFFTXMax + pFFTXZoomWindowPos) / pFrequency2 * fft_w '- fftWH.Width
        End If

        resetInterpolation = True
        lastfftIdx = X2fftIdx(xmin, fftMin, fftMax, fft_w)
        FFTHistL(pFFTHistorySize - 1)(lastfftIdx) = FFTPowerFromIndex(lastfftIdx, FFTChannelConstants.Left)
        FFTHistR(pFFTHistorySize - 1)(lastfftIdx) = FFTPowerFromIndex(lastfftIdx, FFTChannelConstants.Right)

        For x = xmin To xmax - barWidth Step barSep
            fftIdx = X2fftIdx(x, fftMin, fftMax, fft_w)
            If fftIdx <> lastfftIdx Then
                FFTHistL(pFFTHistorySize - 1)(fftIdx - fftMin) = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Left)
                FFTHistR(pFFTHistorySize - 1)(fftIdx - fftMin) = FFTPowerFromIndex(fftIdx, FFTChannelConstants.Right)

                lastfftIdx = fftIdx
                lx = x
                resetInterpolation = True
            End If

            AvgL = FFTAverageFromIndex(fftIdx, FFTChannelConstants.Left)
            AvgR = FFTAverageFromIndex(fftIdx, FFTChannelConstants.Right)

            Select Case pFFTStyle
                Case FFTStyleConstants.Bars
                    Renderer.AddRectangle(x, fft_h2 - AvgL * fft_h2 + fftWH.Height, barWidth, AvgL * fft_h2, pGreenOn)
                    If pChannels > 1 Then Renderer.AddRectangle(x, fft_h - AvgR * fft_h2 + fftWH.Height, barWidth, AvgR * fft_h2, pYellowOn)
                Case FFTStyleConstants.FilledBars
                    Renderer.AddFilledRectangle(x, fft_h2 - AvgL * fft_h2 + fftWH.Height, barWidth, AvgL * fft_h2, pGreenOn)
                    If pChannels > 1 Then Renderer.AddFilledRectangle(x, fft_h - AvgR * fft_h2 + fftWH.Height, barWidth, AvgR * fft_h2, pYellowOn)
                Case FFTStyleConstants.LedBars
                    For h As Double = fft_h2 + fftWH.Height To fft_h2 - AvgL * fft_h2 Step -4
                        Renderer.AddFilledRectangle(x, h, barWidth, 2, GetFFTLedColor(h))
                    Next

                    If pChannels > 1 Then
                        For h As Double = fft_h + fftWH.Height To fft_h - AvgR * fft_h2 + fftWH.Height Step -4
                            Renderer.AddFilledRectangle(x, h, barWidth, 2, GetFFTLedColor(h - fft_h2))
                        Next
                    End If
            End Select
        Next x
    End Sub

    Private Function GetFFTLedColor(h As Double) As Color
        If h <= fft_h4 / 2 Then Return pRedOn
        If h <= fft_h2 - fft_h4 Then Return pYellowOn
        Return pGreenOn
    End Function
#End Region

#End Region

#Region " Events "
    Private Sub DXVUMeterNETGDI_HandleCreated(sender As Object, e As EventArgs) Handles MyBase.HandleCreated
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        'Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.DoubleBuffered = True

        LicenseControl()
        InitControl()

        If Me.FindForm IsNot Nothing Then AddHandler CType(Me.FindForm, Form).FormClosing, AddressOf DoSafeClose
    End Sub

    Private Sub DXVUMeterNETGDI_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Try
            If pStyle = StyleConstants.UserPaintGDI Then
                RaiseEvent PaintGDI(e.Graphics)
            Else
                Renderer.Render(e.Graphics)
            End If
        Catch
        End Try

        If Not Me.Enabled Then
            Using b = New SolidBrush(Color.FromArgb(128, Color.White))
                e.Graphics.FillRectangle(b, Me.DisplayRectangle)
            End Using
        End If
    End Sub

    Private Sub DXVUMeterNETGDI_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResetGraphicsData()
    End Sub
#Region " Mouse "
    Private Sub DXVUMeterNETGDI_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        cursorPos.CursorIsOver = (pStyle = StyleConstants.FFT) AndAlso (pFFTStyle <> FFTStyleConstants.Spectrum)
        If cursorPos.CursorIsOver Then cursorPos.Position = New Point(e.X, e.Y)
    End Sub

    Private Sub DXVUMeterNETGDI_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        cursorPos.CursorIsOver = False
    End Sub

    Private Sub DXVUMeterNETGDI_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If cursorPos.CursorIsOver Then cursorPos.Position = New Point(e.X, e.Y)
    End Sub
#End Region
#End Region

#Region " States Handling "
    Private Sub ChangePlaybackState(newState As PlaybackStateConstants)
        Dim oState As PlaybackStateConstants = pPlaybackState
        pPlaybackState = newState

        If Not isClosing Then
            'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.PlaybackStateChanged, oState, NewState}})
            RaiseEvent PlaybackStateChanged(oState, newState)
        End If
    End Sub

    Private Sub ChangeRecordingState(newState As RecordingStateConstants)
        Dim oState As RecordingStateConstants = pRecordingState
        pRecordingState = newState

        If Not isClosing Then
            'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.RecordingStateChanged, oState, NewState}})
            RaiseEvent RecordingStateChanged(oState, newState)
        End If
    End Sub

    Private Sub ChangeMonitoringState(newState As MonitoringStateConstants)
        Dim oState As MonitoringStateConstants = pMonitoringState
        pMonitoringState = newState

        If Not isClosing Then
            'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.MonitoringStateChanged, oState, NewState}})
            RaiseEvent MonitoringStateChanged(oState, newState)
        End If
    End Sub
#End Region

#Region " Playback/Monitoring Handling "
    Private Function ReadCapturedData() As Boolean
        Dim lockSize As Integer

        lockSize = capBuf.CurrentReadPosition - nextCaptureOffset
        If lockSize < 0 Then lockSize += bufCapDesc.BufferBytes

        ' Block align lock size so that we always write on a boundary
        lockSize -= lockSize Mod notifySize
        If lockSize = 0 Then Return False

        ' Read the capture buffer
        ReDim rBuf(lockSize - 1)
        capBuf.Read(rBuf, 0, lockSize, nextCaptureOffset)

        If pRecordingState = RecordingStateConstants.Recording Then
            SyncLock pSaveFileName
                savedBytes += rBuf.Length
                If inMP3Mode Then
                    Dim b() As Byte = lameEnc.Encode(rBuf)
                    If b IsNot Nothing Then pSaveFileName.Write(b)
                Else
                    pSaveFileName.Write(rBuf)
                End If
            End SyncLock
        End If

        nextCaptureOffset += rBuf.Length
        nextCaptureOffset = nextCaptureOffset Mod bufCapDesc.BufferBytes  ' Circular buffer

        Return True
    End Function

    Private Sub PlaybackData()
        Dim lockSize As Integer

        lockSize = playBuf.CurrentWritePosition - nextPlaybackOffset
        If lockSize < 0 Then lockSize += bufPlayDesc.SizeInBytes

        ' Block align lock size so that we always read on a boundary
        lockSize -= lockSize Mod notifySize
        If lockSize = 0 Then Return

        pBuf = pPlayFileName.ReadAudioData(lockSize)
        ApplyPlaybackVolume(pBuf)

        playBuf.Write(pBuf, nextPlaybackOffset, LockFlags.None)
        'If pRenderExclusively Then RenderBuffer(buf)

        nextPlaybackOffset += pBuf.Length
        nextPlaybackOffset = nextPlaybackOffset Mod bufPlayDesc.SizeInBytes ' Circular buffer

        If pPlayFileName.File.Position = pPlayFileName.File.Length Then StopPlaying()
    End Sub

    Private Sub ApplyPlaybackVolume(b() As Byte)
        If pPlaybackVolume.Volume = 0 Then Exit Sub

        If playBuf.Format.BitsPerSample = 8 Then
            b = Array.ConvertAll(b, AddressOf AmpByteSub)
        Else
            Dim nb() As Integer = NormalizeBuffer(b)
            nb = Array.ConvertAll(nb, AddressOf AmpIntSub)

            For i As Integer = 0 To nb.Length - 2
                Array.Copy(BitConverter.GetBytes(nb(i)), 0, b, i * 2, 2)
            Next i
        End If
    End Sub

    Private Function AmpByteSub(b As Byte) As Byte
        Dim i As Double = 128 - b
        i = Min(Max(i + pPlaybackVolume.VolSign * (i * pPlaybackVolume.Amp), -128), 127)
        Return CByte(i + 128)
    End Function

    Private Function AmpIntSub(i As Integer) As Integer
        Return CInt(Min(Max(i + pPlaybackVolume.VolSign * (i * pPlaybackVolume.Amp), -32768), 32767))
    End Function

    Private Sub ResetMonitor()
        If monitorRenderThread IsNot Nothing AndAlso capBuf IsNot Nothing AndAlso capBuf.Capturing Then capBuf.Stop()
        ChangeMonitoringState(MonitoringStateConstants.Idle)
    End Sub

    Private Sub IdleRenderThreadSub()
        Do
            eventIdleRender.WaitOne(500, False)
            If abortIdleRenderThread Then Exit Do

            If pMonitoringState = MonitoringStateConstants.Idle Then
                idleRenderCanAbort = False

                If Not suspendThreads Then
                    If pEnableRendering Then
                        PreRender()

                        If pStyle = StyleConstants.DigitalVU Then RenderDigitalVUs(Nothing)

                        PostRender()
                    End If
                End If

                idleRenderCanAbort = True
            End If
        Loop Until abortIdleRenderThread
        idleRenderCanAbort = True
    End Sub

    Public Delegate Function CustomBufferProvider() As Byte()
    Private mUserProvidedBuffer As Boolean
    Private userBufferProvider As CustomBufferProvider

    ''' <summary>
    ''' This is an advanced feature that allows you to use your own buffer provider.
    ''' When a custom provider is set, DXVUMeterNETGDI will query the provider to obtain the buffer data, instead of using its internal audio monitoring routines.
    ''' This can be useful if your application is gathering the buffer data from a non standard source or if, for example, you want to plot some data that is not directly provided by a sound card.
    ''' </summary>
    ''' <param name="enabled">Use to control the state of the provider. If set to False, DXVUMeterNETGDI will use its internal provider.</param>
    ''' <param name="userFunction">This is a pointer to the function, provided by the user, from which DXVUMeterNETGDI is expected to receive the buffer data.</param>
    ''' <remarks>
    ''' The userFunction will be queried at specific intervals, based on the various parameters that control the amount of data that should be retrieved from the buffer.
    ''' These parameters include:
    ''' <list type="bullet">
    ''' <item><description><see cref="Frequency">Frequency</see></description></item>
    ''' <item><description><see cref="BitDepth">BitDepth</see></description></item>
    ''' </list>
    ''' </remarks>
    Public Sub SetCustomBufferProvider(enabled As Boolean, Optional userFunction As CustomBufferProvider = Nothing)
        mUserProvidedBuffer = enabled
        userBufferProvider = userFunction
    End Sub

    Private Sub MonitorThreadSub()
        Dim status As Boolean = True

        Do
            'If pMonitoringState = MonitoringStateConstants.mscMonitoring then
            ' the eventMonitorRender will only be fired when a position is reached
            ' So... sit here until this happens...

            If Not mUserProvidedBuffer Then eventMonitorRender.WaitOne(Timeout.Infinite, False)

            If pMonitoringState = MonitoringStateConstants.Monitoring AndAlso (abortMonitorRenderThread = False) Then
                monitorRenderCanAbort = False

                If Not suspendThreads Then
                    If mUserProvidedBuffer Then
                        rBuf = userBufferProvider.Invoke()
                        status = True
                    Else
                        status = ReadCapturedData()
                    End If

                    If pFFTHighPrecisionMode Then
                        Do
                            If status Then RenderBuffer(rBuf, bufferIndex = 0)
                        Loop While status AndAlso bufferIndex <> 0
                    Else
                        If status Then RenderBuffer(rBuf, True)
                    End If
                End If

                monitorRenderCanAbort = True
            Else
                Thread.Sleep(10)
            End If
        Loop Until abortMonitorRenderThread

        Diagnostics.Debug.WriteLine("Monitor Render Thread Aborted")
    End Sub

    Private Sub PlaybackThreadSub()
        Do
            ' The eventHnd will only be fired when a position is reached
            ' So... sit here until this happens...

            eventPlaybackRender.WaitOne(Timeout.Infinite, False)
            If abortPlaybackRenderThread Then Exit Do

            If pPlaybackState = PlaybackStateConstants.Playing Then
                playbackRenderCanAbort = False

                If Not suspendThreads Then PlaybackData()

                playbackRenderCanAbort = True
            End If
        Loop Until abortPlaybackRenderThread
        playbackRenderCanAbort = True
    End Sub
#End Region

#Region " Support Functions "
    Private Sub ResetGraphicsData()
        If Not VUsReady Or Renderer Is Nothing Then Exit Sub

        w = Me.DisplayRectangle.Width
        h = Me.DisplayRectangle.Height

        If pStyle = StyleConstants.FFT Then
            If pFFTStyle = FFTStyleConstants.Spectrum Then
                Renderer.KeepBackground = True
                Renderer.EraseBackground()
            Else
                Renderer.KeepBackground = False
            End If
        Else
            Renderer.KeepBackground = False
        End If

        w2 = w / 2
        w4 = w / 4

        h2 = h / 2
        h4 = h / 4
        h2h4 = h2 + h4

        forceResetFFTArrays = True
        If pStyle = StyleConstants.FFT Then
            pFFTScaleFontSize = TextRenderer.MeasureText("X", pFFTScaleFont)
            If pFFTRenderScales = FFTRenderScalesConstants.None Then
                fftWH = Size.Empty
            Else
                Select Case pFFTStyle
                    Case FFTStyleConstants.Spectrum
                        fftWH = TextRenderer.MeasureText("XX.XXKhz", pFFTScaleFont)
                    Case Else
                        fftWH = TextRenderer.MeasureText(" -XXXdB", pFFTScaleFont)
                End Select
                fftWH.Width = CSng(fftWH.Width - fftWH.Width * 0.2)
            End If

            fftMin = CInt((pFFTXMin + pFFTXZoomWindowPos) / pFrequency2 * pFFTSize2)
            fftMax = CInt((pFFTXMax + pFFTXZoomWindowPos) / pFrequency2 * pFFTSize2)
            'If pFFTSize2 <> pFFTSize \ 2 Then InitFFT(False)

            If bufCapDesc.Format IsNot Nothing AndAlso bufCapDesc.Format.Channels = 2 AndAlso pFFTLineChannelMode = FFTLineChannelModeConstants.Normal Then
                fft_w = w '- fftWH.Width
                fft_h = h - 2 * If(pFFTStyle = FFTStyleConstants.Spectrum, 0, fftWH.Height) - 1

                fft_w2 = fft_w / 2
                fft_w4 = fft_w / 4

                fft_h2 = fft_h / 2
                fft_h4 = fft_h / 4
                fft_h2h4 = fft_h2 + fft_h4
            Else
                fft_w = w '- fftWH.Width
                fft_h = h - 2 * If(pFFTStyle = FFTStyleConstants.Spectrum, 0, fftWH.Height) - 1

                fft_w2 = fft_w / 2
                fft_w4 = fft_w / 4

                fft_h2 = fft_h
                fft_h4 = fft_h / 2
                fft_h2h4 = fft_h2 + fft_h4
            End If

            If pFFTStyle = FFTStyleConstants.Spectrum Then xSpectrum = fftWH.Width + 4

            ReDim Preserve FFTHistL(pFFTHistorySize - 1)
            ReDim Preserve FFTHistR(pFFTHistorySize - 1)
            For i As Integer = 0 To pFFTHistorySize - 1
                ReDim Preserve FFTHistL(i)((fftMax - fftMin) - 1)
                ReDim Preserve FFTHistR(i)((fftMax - fftMin) - 1)
            Next

            If waveL IsNot Nothing Then
                ReDim Preserve WAVHistL(pFFTSize - 1)
                ReDim Preserve WAVHistR(pFFTSize - 1)
            End If

            pCOHHistorySize = pFFTHistorySize
            ReDim Preserve COHHistory(pCOHHistorySize - 1)
            For i As Integer = 0 To pCOHHistorySize - 1
                ReDim Preserve COHHistory(i)(pFFTSize - 1)
                For j As Integer = 0 To pFFTSize - 1
                    COHHistory(i)(j) = New ComplexDouble()
                Next
            Next

            pFilters.ForEach(Sub(f) f.SamplingRate = pFrequency)
        End If

        bufferIndex = 0
        waveInIndex = 0

        ResetLedsData()
    End Sub

    Private Sub DefineColors()
        If vuL.Length <> pNumVUs Then
            ReDim vuL(pNumVUs - 1)
            ReDim vuR(pNumVUs - 1)
        End If

        'If Renderer IsNot Nothing Then Renderer.setColors(pGreenOff, pGreenOn, pYellowOff, pYellowOn, pRedOff, pRedOn)

        VUsReady = True
    End Sub

    Private Function Time2Str(s As Double) As String
        Dim h As Integer = CInt(s / 3600)
        Dim m As Integer = CInt(Abs((h * 3600 - s) / 60))
        s = CInt(Abs(s - (h * 3600 + m * 60)))

        Return If(h.ToString.Length = 1, "0", "") + h.ToString + ":" +
                If(m.ToString.Length = 1, "0", "") + m.ToString + ":" +
                If(s.ToString.Length = 1, "0", "") + s.ToString
    End Function

    Private Function CalculateRMS(buf() As Integer) As Peak
        Dim p As Peak

        Try
            Dim i As Integer
            Dim nStep As Integer = pChannels * 128
            Const nAvg As Integer = 4

            If buf IsNot Nothing Then
                With p
                    ' Calculate Average
                    For i = 0 To buf.Length - pChannels Step nStep
                        .Left = .Left + (buf(i) / 30) ^ 2
                        If pChannels = 2 Then
                            .Right = .Right + (buf(i + 1) / 30) ^ 2
                        Else
                            .Right = .Left
                        End If
                    Next

                    .Left = .Left / (buf.Length / nStep)
                    .Right = .Right / (buf.Length / nStep)

                    ' Apply compensation
                    vuL(0) = CInt(.Left)
                    vuR(0) = CInt(.Right)
                    For i = nAvg To 1 Step -1
                        vuL(i) = vuL(i - 1)
                        vuR(i) = vuR(i - 1)
                        .Left = .Left + vuL(i)
                        .Right = .Right + vuR(i)
                    Next i

                    ' Calculate Power
                    .Left = 0.8 * (Sqrt(.Left) / 128) / nAvg : If .Left > 1 Then .Left = 1
                    .Right = 0.8 * (Sqrt(.Right) / 128) / nAvg : If .Right > 1 Then .Right = 1
                End With
            End If
        Catch
            Stop
        End Try

        Return p
    End Function

    Private Sub LogMessage(code As ErrorConstants, src As String, msg As String)
        Dim s As String = New String("-"c, 20)

        Diagnostics.Debug.WriteLine(s)
        Diagnostics.Debug.WriteLine(src)
        Diagnostics.Debug.WriteLine(msg)
        Diagnostics.Debug.WriteLine(s)

        'Me.Invoke(New SafeRaiseEventDel(AddressOf SafeRaiseEventProc), New Object() {New Object() {PublicEventConstants.Error, code, src, msg}})
        RaiseEvent [Error](code, src, msg)
    End Sub

    Private Sub ResetLedsData()
        Select Case pOrientation
            Case OrientationConstants.Horizontal
                ledSize = New SizeF(CSng(w / pNumVUs - 2), CSng(h2 - 1))
            Case OrientationConstants.Vertical
                ledSize = New SizeF(CSng(w2 - 1), CSng(h / pNumVUs - 2))
        End Select
    End Sub
#End Region
#End Region

#End Region

    Private isClosing As Boolean = False
    Private Sub DoSafeClose(sender As Object, e As EventArgs)
        isClosing = True
        Dim n As Integer = 0

        If playbackRenderThread IsNot Nothing Then
            StopPlaying()
            abortPlaybackRenderThread = True
            n = 0
            Do
                n += 1
                Application.DoEvents()
                eventPlaybackRender.Set()
            Loop Until playbackRenderCanAbort OrElse n >= 100
            If n >= 0 Then playbackRenderThread.Abort()
            playbackRenderThread = Nothing
            eventPlaybackRender.Close()
            eventPlaybackRender = Nothing
        End If

        If monitorRenderThread IsNot Nothing Then
            StopMonitoring()

            abortMonitorRenderThread = True
            n = 0
            Do
                n += 1
                Application.DoEvents()
                eventMonitorRender.Set()
            Loop Until monitorRenderCanAbort OrElse n >= 100
            If n >= 0 Then monitorRenderThread.Abort()
            monitorRenderThread = Nothing
            eventMonitorRender.Close()
            eventMonitorRender = Nothing
        End If



        If idleRenderThread IsNot Nothing Then
            abortIdleRenderThread = True
            n = 0
            Do
                n += 1
                Application.DoEvents()
                eventIdleRender.Set()
            Loop Until idleRenderCanAbort OrElse n >= 100
            If n >= 0 Then idleRenderThread.Abort()
            idleRenderThread = Nothing
            eventIdleRender.Close()
            eventIdleRender = Nothing
        End If

        If Renderer IsNot Nothing Then
            Renderer.Dispose()
            Renderer = Nothing
        End If

        cursorPos = Nothing
        pSaveFileName = Nothing
        pPlayFileName = Nothing

        For Each d In Me.Devices
            d.Dispose()
        Next
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class