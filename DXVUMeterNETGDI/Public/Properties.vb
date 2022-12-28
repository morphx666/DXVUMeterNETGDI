Imports System.ComponentModel

Partial Public Class DXVUMeterNETGDI
    ''' <summary>Collection of devices, that can record audio, currently available on the system</summary>
    Public Devices As DevicesCollection

    ''' <summary>Enables or disables the internal rendering engine</summary>
    ''' <remarks>
    ''' When set to False the control will still calculate the RMS value of the audio being monitored so the <see cref="PeakValues">PeakValues</see> will still return valid data.
    ''' </remarks>
    <Category("Behavior")>
    Public Property EnableRendering() As Boolean
        Get
            Return pEnableRendering
        End Get
        Set(Value As Boolean)
            pEnableRendering = Value
        End Set
    End Property

    ''' <summary>Sets or gets the number of notifications thrown by the monitoring engine</summary>
    ''' <remarks>
    ''' Do not use unless you know what you're doing!
    ''' </remarks>
    <Category("Behavior")>
    Public Property MonitoringNotifications() As Integer
        Get
            Return numberMonitoringNotifications
        End Get
        Set(Value As Integer)
            numberMonitoringNotifications = Value
        End Set
    End Property

#Region " FFT Specific "
    ''' <summary>
    ''' Controls the minimum audio value that should be accepted as valid
    ''' </summary>
    <Category("FFT")>
    Public Property NoiseFilter As Integer
        Get
            Return pNoiseFilter
        End Get
        Set(value As Integer)
            pNoiseFilter = value
        End Set
    End Property

    ''' <summary>
    ''' Sets or retrieves the font used in the FFT scale
    ''' <seealso cref="Style"></seealso>
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTScaleFont() As Font
        Get
            Return pFFTScaleFont
        End Get
        Set(Value As Font)
            If Value IsNot Nothing Then
                pFFTScaleFont = Value

                ResetGraphicsData()
            End If
        End Set
    End Property

    ''' <summary>When enabled, the FFT will hold the highest peaks for a few milliseconds
    ''' This setting is valid for all FFTPStyle values
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTHoldMaxPeaks() As Boolean
        Get
            Return pFFTHoldMaxPeaks
        End Get
        Set(Value As Boolean)
            forceResetFFTArrays = True
            pFFTHoldMaxPeaks = Value
        End Set
    End Property

    ''' <summary>
    ''' When enabled, the the FFT will leave a small trail behind as it changes
    ''' This setting is only valid for the FFTStyleConstants.Line FFTPStyle
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTShowDecay() As Boolean
        Get
            Return pFFTShowDecay
        End Get
        Set(Value As Boolean)
            pFFTShowDecay = Value
        End Set
    End Property

    ''' <summary>
    ''' This option controls the smoothing ammount applied to the FFT waveform
    ''' This setting is only valid for the FFTStyleConstants.Line and FFTStyleConstants.Filled styles
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTSmoothing() As Integer
        Get
            Return pFFTSmoothing
        End Get
        Set(value As Integer)
            pFFTSmoothing = value
        End Set
    End Property

    ''' <summary>
    ''' This option can be useful to reduce the visual noise that can be generated
    ''' when rendering a zoomeded plot in log scale
    ''' This setting is only valid for the FFTStyleConstants.Line and FFTStyleConstants.Filled styles
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTPlotNoiseReduction() As Integer
        Get
            Return pFFTPlotNoiseReduction
        End Get
        Set(value As Integer)
            pFFTPlotNoiseReduction = value
        End Set
    End Property

    ''' <summary>
    ''' When enabled, the FFT will hold the lowest peaks for a few milliseconds
    ''' This setting is only valid for the FFTStyleConstants.Line FFTPStyle
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTHoldMinPeaks() As Boolean
        Get
            Return pFFTHoldMinPeaks
        End Get
        Set(Value As Boolean)
            forceResetFFTArrays = True
            pFFTHoldMinPeaks = Value
        End Set
    End Property

    ''' <summary>
    ''' When enabled, the FFT will display a shaded area showing the valleys and peaks
    ''' This setting is only valid for the FFTStyleConstants.Line FFTPStyle
    ''' <seealso cref="FFTStyle"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTShowMinMaxRange() As Boolean
        Get
            Return pFFTShowMinMaxRange
        End Get
        Set(Value As Boolean)
            forceResetFFTArrays = True
            pFFTShowMinMaxRange = Value
        End Set
    End Property

    ''' <summary>Sets or returns the style used to render the Y (level) scale</summary>
    <Category("FFT")>
    Public Property FFTYScale() As FFTYScaleConstants
        Get
            Return pFFTYScale
        End Get
        Set(Value As FFTYScaleConstants)
            pFFTYScale = Value
        End Set
    End Property

    ''' <summary>Sets or returns the every time</summary>
    <Category("FFT")>
    Public Property FFTStyle() As FFTStyleConstants
        Get
            Return pFFTStyle
        End Get
        Set(Value As FFTStyleConstants)
            If Value <> pFFTStyle Then
                pFFTStyle = Value

                ResetGraphicsData()
                DefineColors()
            End If
        End Set
    End Property

    ''' <summary>Sets or returns the style used to render Fast Fourier Transform when in Line mode</summary>
    <Category("FFT")>
    Public Property FFTLineChannelMode() As FFTLineChannelModeConstants
        Get
            Return pFFTLineChannelMode
        End Get
        Set(value As FFTLineChannelModeConstants)
            If value <> pFFTLineChannelMode Then
                pFFTLineChannelMode = value
                ResetGraphicsData()
            End If
        End Set
    End Property

    ''' <summary>Sets or returns the size of the data history used to render the FFT</summary>
    <Category("FFT")>
    Public Property FFTHistorySize() As Integer
        Get
            Return pFFTHistorySize
        End Get
        Set(value As Integer)
            If value <> pFFTHistorySize Then
                If value <= 0 Then value = 1
                pFFTHistorySize = value
                ResetGraphicsData()
            End If
        End Set
    End Property

    ''' <summary>Contains the list of filters applied to the audio data after applying the selected Window and before generating the FFT</summary>
    <Category("FFT")>
    Public ReadOnly Property Filters() As List(Of BiQuadFilter)
        Get
            Return pFilters
        End Get
    End Property

    ''' <summary>Sets or returns the size of the input data (audio signal) history used to generate the FFT</summary>
    <Category("FFT")>
    <Obsolete("This property is no longer used nor supported. Instead, use the new Overlap property.")>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property WAVHistorySize() As Integer
        Get
            Return 0
        End Get
        Set(value As Integer)
        End Set
    End Property

    ''' <summary>Sets or returns the audio overlap amount in percentage, from 0 (0%) to 0.99 (99%)</summary>
    <Category("FFT")>
    Public Property Overlap() As Double
        Get
            Return pOverlap
        End Get
        Set(value As Double)
            If value < 0 Then
                value = 0
            ElseIf value > 1 Then
                value = 1
            End If
            pOverlap = value
        End Set
    End Property

    ''' <summary>
    ''' Sets or returns the minimum frequency to be displayed
    ''' <seealso cref="FFTXMax"></seealso>
    ''' <seealso cref="FFTXZoom"></seealso>
    ''' <seealso cref="FFTXZoomWindowPos"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTXMin() As Integer
        Get
            Return pFFTXMin
        End Get
        Set(value As Integer)
            If value <> pFFTXMin Then
                If value < 0 Then value = 0
                If value > pFFTXMax Then value = pFFTXMax
                pFFTXMin = value
                ResetGraphicsData()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets or returns the maximum frequency to be displayed
    ''' <seealso cref="FFTXMin"></seealso>
    ''' <seealso cref="FFTXZoom"></seealso>
    ''' <seealso cref="FFTXZoomWindowPos"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTXMax() As Integer
        Get
            Return pFFTXMax
        End Get
        Set(Value As Integer)
            If Value <> pFFTXMax Then
                If Value > pFrequency2 Then Value = pFrequency2
                If Value < pFFTXMin Then Value = pFFTXMin
                pFFTXMax = Value
                ResetGraphicsData()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Allows adjusting the starting frequency of the FFT render without affecting the <see cref="FFTXMin">FFTMin</see> and <see cref="FFTXMin">FFTMax</see> properties
    ''' <seealso cref="FFTXMax"></seealso>
    ''' <seealso cref="FFTXZoom"></seealso>
    ''' <seealso cref="FFTXZoomWindowPos"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTXZoomWindowPos() As Integer
        Get
            Return pFFTXZoomWindowPos
        End Get
        Set(Value As Integer)
            If Value <> pFFTXZoomWindowPos Then
                If pFFTXZoomWindowPos < 0 Then pFFTXZoomWindowPos = 0
                If pFFTXZoomWindowPos + (pFFTXMax - pFFTXMin) > pFrequency2 Then pFFTXZoomWindowPos = pFrequency2 - pFFTXMax
                pFFTXZoomWindowPos = Value
                ResetGraphicsData()
            End If
        End Set
    End Property

    ''' <summary>
    ''' When enabled, the rendered FFT range will occupy the whole width of the control
    ''' <seealso cref="FFTXMin"></seealso>
    ''' <seealso cref="FFTXMax"></seealso>
    ''' <seealso cref="FFTXZoomWindowPos"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTXZoom() As Boolean
        Get
            Return pFFTXZoom
        End Get
        Set(Value As Boolean)
            pFFTXZoom = Value
        End Set
    End Property

    ''' <summary>Sets or returns the style used to render the X (time) scale</summary>
    <Category("FFT")>
    Public Property FFTXScale() As FFTXScaleConstants
        Get
            Return pFFTXScale
        End Get
        Set(Value As FFTXScaleConstants)
            pFFTXScale = Value
        End Set
    End Property

    ''' <summary>
    ''' Sets or returns which scales to render while in FFT mode
    ''' <seealso cref="DXVUMeterNETGDI.Style"></seealso>
    ''' </summary>
    <Category("FFT")>
    Public Property FFTRenderScales() As FFTRenderScalesConstants
        Get
            Return pFFTRenderScales
        End Get
        Set(value As FFTRenderScalesConstants)
            pFFTRenderScales = value
        End Set
    End Property

    ''' <summary>Sets or returns the windowing type used to render the Fast Fourier Transform</summary>
    <Category("FFT")>
    Public Property FFTWindow() As FFTWindowConstants
        Get
            Return pFFTWindow
        End Get
        Set(Value As FFTWindowConstants)
            pFFTWindow = Value
            UpdateWindow()
        End Set
    End Property

    ''' <summary>Sets or returns the size (bands) of the Fast Fourier Transform</summary>
    <Category("FFT")>
    Public Property FFTSize() As FFTSizeConstants
        Get
            Return pFFTSize
        End Get
        Set(Value As FFTSizeConstants)
            If Value <> pFFTSize Then
                pFFTSize = Value
                InitFFT()
            End If
        End Set
    End Property

    ''' <summary>Sets or returns a multiplier used to scale the FFT</summary>
    <Category("FFT")>
    Public Property FFTYScaleMultiplier() As Double
        Get
            Return pFFTYScaleMultiplier
        End Get
        Set(Value As Double)
            If Value <> pFFTSize Then
                pFFTYScaleMultiplier = Value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Controls the decay delay for the peaks
    ''' </summary>
    <Category("FFT")>
    Public Property FFTPeaksDecayDelay() As Integer
        Get
            Return pFFTPeaksDecayDelay
        End Get
        Set(value As Integer)
            pFFTPeaksDecayDelay = value
        End Set
    End Property

    ''' <summary>
    ''' Controls the decay speed for the peaks
    ''' </summary>
    <Category("FFT")>
    Public Property FFTPeaksDecaySpeed() As Integer
        Get
            Return pFFTPeaksDecaySpeed
        End Get
        Set(value As Integer)
            pFFTPeaksDecaySpeed = value
        End Set
    End Property

    <Category("FFT")>
    Public Property FFTNormalized As Boolean
        Get
            Return pFFTNormalized
        End Get
        Set(value As Boolean)
            pFFTNormalized = value
        End Set
    End Property

    <Category("FFT")>
    Public Property FFTHighPrecisionMode As Boolean
        Get
            Return pFFTHighPrecisionMode
        End Get
        Set(value As Boolean)
            pFFTHighPrecisionMode = value
        End Set
    End Property

    ''' <summary>When enabled and when the <see cref="Style">Style</see> property is set to FFT, the control will raise a <see cref="DTMFToneDown">DTMFToneDown</see> event when a DTMF tone is detected</summary>
    <Category("FFT")>
    Public Property FFTDetectDTMF() As Boolean
        Get
            Return pFFTDetectDTMF
        End Get
        Set(value As Boolean)
            pFFTDetectDTMF = value
            'SetFFTSmoothing()
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Sets or returns the thickness of the lines used to render the <see cref="StyleConstants">Oscilloscope</see> and the 
    ''' <see cref="StyleConstants">FFT</see> when using the <see cref="FFTStyleConstants">Line</see> style
    ''' </summary>
    ''' <value>Thickness of the lines is meassured in pixels</value>
    ''' <returns>Returns the thickness of the lines</returns>
    ''' <remarks>Set the property to 0 to hide the lines</remarks>
    <Category("Appearance")>
    Public Property LinesThickness() As Integer
        Get
            Return pLinesThickness
        End Get
        Set(value As Integer)
            pLinesThickness = value
        End Set
    End Property

    ''' <summary>This property sets or returns the playback position within the file</summary>
    <Browsable(False)>
    Public Property PlaybackPosition() As Long
        Get
            If pPlaybackState <> PlaybackStateConstants.Idle Then
                Return pPlayFileName.File.Seek(0, IO.SeekOrigin.Current) - 40
            End If
        End Get
        Set(Value As Long)
            If pPlaybackState <> PlaybackStateConstants.Idle Then
                Value += Value Mod (pPlayFileName.Header.nChannels * If(pPlayFileName.Header.wBitsPerSample = 16, 2, 1))
                pPlayFileName.File.Seek(Value + 40, IO.SeekOrigin.Begin)
            End If
        End Set
    End Property

    ''' <summary>This property can be used to adjust the volume of the Playback without affecting the Main/Wave output volumes of the sound card</summary>
    ''' <remarks>The property accepts any value from -100 to 100 where the value indicates the ammount of attenuation (negative values) or amplicfication (positive values) that will be applied.</remarks>
    <Browsable(False)>
    Public Property PlaybackVolume() As Short
        Get
            Return pPlaybackVolume.Volume
        End Get
        Set(value As Short)
            If value < -100 Then value = -100
            If value > 100 Then value = 100
            With pPlaybackVolume
                .Volume = value
                .VolSign = Math.Sign(.Volume)
                .Amp = Math.Abs(.Volume / 100)
            End With
        End Set
    End Property

    Private mInternalGain As Double = 1
    Public Property InternalGain As Double
        Get
            Return mInternalGain
        End Get
        Set(value As Double)
            mInternalGain = value
            If mInternalGain < 0 Then mInternalGain = 0
            If mInternalGain > 10 Then mInternalGain = 10
        End Set
    End Property

    ''' <summary>This property returns the total size of the audio data in the file</summary>
    <Browsable(False)>
    Public ReadOnly Property PlaybackPositionTotal() As Long
        Get
            If pPlaybackState <> PlaybackStateConstants.Idle Then
                Return pPlayFileName.File.Length - 40
            End If
        End Get
    End Property

    ''' <summary>This property sets or returns the current playback position within the file in hh:mm:ss format</summary>
    <Browsable(False)>
    Public Property PlaybackTime() As String
        Get
            If pPlaybackState <> PlaybackStateConstants.Idle Then
                Return Time2Str((PlaybackPosition / (pPlayFileName.Header.nChannels * If(pPlayFileName.Header.wBitsPerSample = 16, 2, 1)) / pPlayFileName.Header.nSamplesPerSec))
            Else
                Return ""
            End If
        End Get
        Set(Value As String)
            If pPlaybackState <> PlaybackStateConstants.Idle Then
                Dim h As Integer = CInt(Value.Split(CChar(":"))(0))
                Dim m As Integer = CInt(Value.Split(CChar(":"))(1))
                Dim s As Integer = CInt(Value.Split(CChar(":"))(2))

                Dim t As Long = h * 3600 + m * 60 + s

                PlaybackPosition = t * pPlayFileName.Header.nSamplesPerSec * (pPlayFileName.Header.nChannels * If(pPlayFileName.Header.wBitsPerSample = 16, 2, 1))
            End If
        End Set
    End Property

    ''' <summary>Returns the total recording time in hh:mm:ss format</summary>
    <Browsable(False)>
    Public ReadOnly Property RecordingTime() As String
        Get
            If pSaveFileName.File IsNot Nothing Then
                Return Time2Str((savedBytes / (pSaveFileName.Header.nChannels * If(pSaveFileName.Header.wBitsPerSample = 16, 2, 1))) / pSaveFileName.Header.nSamplesPerSec)
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>Returns the total playback time in hh:mm:ss format</summary>
    <Browsable(False)>
    Public ReadOnly Property PlaybackTimeTotal() As String
        Get
            If pPlaybackState <> PlaybackStateConstants.Idle Then
                Return Time2Str((PlaybackPositionTotal / (pPlayFileName.Header.nChannels * If(pPlayFileName.Header.wBitsPerSample = 16, 2, 1))) / pPlayFileName.Header.nSamplesPerSec)
            Else
                Return ""
            End If
        End Get
    End Property

    ''' <summary>Sets or returns the rendering style that will be used to represent the audio being monitored</summary>
    ''' <remarks>
    ''' </remarks>
    <Category("Appearance")>
    Public Property Style() As StyleConstants
        Get
            Return pStyle
        End Get
        Set(value As StyleConstants)
            pStyle = value
            ResetGraphicsData()
        End Set
    End Property

    ''' <summary>Returns the monitoring state of the control</summary>
    <Browsable(False)>
    Public ReadOnly Property MonitoringState() As MonitoringStateConstants
        Get
            Return pMonitoringState
        End Get
    End Property

    ''' <summary>Returns the recording state of the control</summary>
    <Browsable(False)>
    Public ReadOnly Property RecordingState() As RecordingStateConstants
        Get
            Return pRecordingState
        End Get
    End Property

    ''' <summary>Returns the playback state of the control</summary>
    <Browsable(False)>
    Public ReadOnly Property PlaybackState() As PlaybackStateConstants
        Get
            Return pPlaybackState
        End Get
    End Property

    ''' <summary>Returns the state of the control</summary>
    <Browsable(False)>
    Public ReadOnly Property State() As PlaybackStateConstants
        Get
            Return CType(pMonitoringState Or pPlaybackState Or pRecordingState, PlaybackStateConstants)
        End Get
    End Property

    ''' <summary>Sets or returns the frequency at which the audio will be monitored</summary>
    ''' <remarks>You should check each one of the <see cref="DevicesCollection.Device.Qualities">Qualities</see> for the selected <see cref="DevicesCollection.Device">Device</see> to ensure that the provided Frequency is valid and supported by the device.</remarks>
    <Browsable(False)>
    Public Property Frequency() As Integer
        Get
            Return pFrequency
        End Get
        Set(Value As Integer)
            pFrequency = Value
            pFrequency2 = pFrequency \ 2
            If pFFTXMax > pFrequency2 Then pFFTXMax = pFrequency2
        End Set
    End Property

    ''' <summary>Sets or returns the number of chanels to monitor (1 for mono and 2 for stereo)</summary>
    ''' <remarks>You should check each one of the <see cref="DevicesCollection.Device.Qualities">Qualities</see> for the selected <see cref="DevicesCollection.Device">Device</see> to ensure that the provided number of Channels is valid and supported by the device.</remarks>
    <Browsable(False)>
    Public Property Channels() As Short
        Get
            Return pChannels
        End Get
        Set(Value As Short)
            pChannels = Value
        End Set
    End Property

    ''' <summary>Sets or returns the quality at which the audio will be monitored</summary>
    ''' <remarks>You should check each one of the <see cref="DevicesCollection.Device.Qualities">Qualities</see> for the selected <see cref="DevicesCollection.Device">Device</see> to ensure that the provided BitDepth is valid and supported by the device.</remarks>
    <Browsable(False)>
    Public Property BitDepth() As Short
        Get
            Return pBitDepth
        End Get
        Set(Value As Short)
            pBitDepth = Value
        End Set
    End Property

    ''' <summary>Sets or returns the orientation of the control when the <see cref="Style">Style</see> property is set to <see cref="StyleConstants.DigitalVU">DigitalVU</see></summary>
    <Category("Appearance")>
    Public Property Orientation() As OrientationConstants
        Get
            Return pOrientation
        End Get
        Set(Value As OrientationConstants)
            If pOrientation <> Value Then
                pOrientation = Value
                ResetLedsData()
            End If
        End Set
    End Property

    ''' <summary>Sets or returns the number of leds that will be rendered when the <see cref="Style">Style</see> property is set to <see cref="StyleConstants.DigitalVU">DigitalVU</see></summary>
    <Category("Appearance")>
    Public Property NumVUs() As Short
        Get
            Return pNumVUs
        End Get
        Set(Value As Short)
            If Value < 1 Then Value = 1
            If Value <> pNumVUs Then
                pNumVUs = Value

                ResetLedsData()
                DefineColors()
            End If
        End Set
    End Property

    ''' <summary>Sets or returns the active middle-level color of the monitored audio</summary>
    <Category("Appearance")>
    Public Property YellowOn() As Color
        Get
            Return pYellowOn
        End Get
        Set(Value As Color)
            pYellowOn = Value
            DefineColors()
        End Set
    End Property

    ''' <summary>Sets or returns the inactive middle-level color of the monitored audio</summary>
    <Category("Appearance")>
    Public Property YellowOff() As Color
        Get
            Return pYellowOff
        End Get
        Set(Value As Color)
            pYellowOff = Value
            DefineColors()
        End Set
    End Property

    ''' <summary>Sets or returns the active low-level color of the monitored audio</summary>
    <Category("Appearance")>
    Public Property GreenOn() As Color
        Get
            Return pGreenOn
        End Get
        Set(Value As Color)
            pGreenOn = Value
            DefineColors()
        End Set
    End Property

    ''' <summary>Sets or returns the inactive low-level color of the monitored audio</summary>
    <Category("Appearance")>
    Public Property GreenOff() As Color
        Get
            Return pGreenOff
        End Get
        Set(Value As Color)
            pGreenOff = Value
            DefineColors()
        End Set
    End Property

    ''' <summary>Sets or returns the active high-level color of the monitored audio</summary>
    <Category("Appearance")>
    Public Property RedOn() As Color
        Get
            Return pRedOn
        End Get
        Set(Value As Color)
            pRedOn = Value
            DefineColors()
        End Set
    End Property

    ''' <summary>Sets or returns the inactive high-level color of the monitored audio</summary>
    <Category("Appearance")>
    Public Property RedOff() As Color
        Get
            Return pRedOff
        End Get
        Set(Value As Color)
            pRedOff = Value
            DefineColors()
        End Set
    End Property

    ''' <summary>Returns the control's version number</summary>
    Public ReadOnly Property Version() As String
        Get
            Return FileVersionInfo.GetVersionInfo(GetType(DXVUMeterNETGDI).Assembly.Location).FileVersion.ToString
            'Return GetType(NDXVUMeterNET.DXVUMeterNET).Assembly.GetName.Version.ToString
        End Get
    End Property

    ''' <summary>If set to true, the control will ignore the audio being monitored through the left channel</summary>
    <Category("Behavior")>
    Public Property LeftChannelMute() As Boolean
        Get
            Return mLeftChannelMute
        End Get
        Set(Value As Boolean)
            mLeftChannelMute = Value
        End Set
    End Property

    ''' <summary>If set to true, the control will ignore the audio being monitored through the right channel</summary>
    <Category("Behavior")>
    Public Property RightChannelMute() As Boolean
        Get
            Return mRightChannelMute
        End Get
        Set(Value As Boolean)
            mRightChannelMute = Value
        End Set
    End Property

    Public ReadOnly Property CaptureBufferLenght As Integer
        Get
            Return bufCapDesc.BufferBytes
        End Get
    End Property

    Public ReadOnly Property UsesCustomBuffer As Boolean
        Get
            Return mUserProvidedBuffer
        End Get
    End Property
End Class
