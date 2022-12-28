Partial Public Class DXVUMeterNETGDI
    ''' <summary>
    ''' Occurs every time a new chunk of audio is monitored
    ''' </summary>
    ''' <param name="audioBuffer">This array contains the raw values for the captured audio data</param>
    ''' <param name="normalizedAudioBuffer">This array contains the <see cref="DXVUMeterNETGDI.NormalizeBuffer">normalized</see> values for the captured audio data</param>
    ''' <param name="maxPeak">This parameter contains the maximum value available in the audioBuffer array</param>
    ''' <remarks>Use this event to obtain notifications whenever the control has finished capturing a new chunk of audio and you want to do something with this information, such as display your own custom interface or perform some additional operations over the captured audio data</remarks>
    Public Event PeakValues(audioBuffer() As Byte, normalizedAudioBuffer() As Integer, maxPeak As Peak)

    ''' <summary>Occurs every time the playback state of the control changes</summary>
    Public Event PlaybackStateChanged(oldState As PlaybackStateConstants, newState As PlaybackStateConstants)

    ''' <summary>Occurs every time the monitoring state of the control changes</summary>
    Public Event MonitoringStateChanged(oldState As MonitoringStateConstants, newState As MonitoringStateConstants)

    ''' <summary>Occurs every time the recording state of the control changes</summary>
    Public Event RecordingStateChanged(oldState As RecordingStateConstants, newState As RecordingStateConstants)

    ''' <summary>Occurs every time the control encounters a problem</summary>
    Public Event [Error](code As ErrorConstants, source As String, message As String)

    ''' <summary>Occurs every time a new FFT frame is calculated.
    ''' Note that the event will only be triggered if <see cref="DXVUMeterNETGDI.Style">Style</see> is set to <see cref="StyleConstants.FFT">FFT</see></summary>
    Public Event FFTFrame(fftLeft() As ComplexDouble, fftRight() As ComplexDouble)

    ''' <summary>
    ''' Occurs when a DTMF is detected
    ''' </summary>
    ''' <param name="tone">The tone that has been detected</param>
    Public Event DTMFToneDown(tone As DTMFToneConstants)

    ''' <summary>
    ''' Occurs when a previously detected DTMF is lost
    ''' </summary>
    Public Event DTMFToneUp()

    ''' <summary>
    ''' Occurs when the control has finished loading and initializing
    ''' </summary>
    ''' <remarks>It is only after this event has fired that interaction with the control should occur. Trying to use the control, in any way, before this event is triggered can cause unpredictable results</remarks>
    Public Event ControlIsReady()

    ''' <summary>
    ''' When the Style property is set to UserPaintGDI this event will be fired when the control needs to be painted
    ''' </summary>
    ''' <param name="graphics">The parameter is a reference to the <see cref="Graphics">graphics</see> object used to render the control</param>
    Public Event PaintGDI(graphics As Graphics)

    Private Enum PublicEventConstants
        PeakValues
        PlaybackStateChanged
        MonitoringStateChanged
        RecordingStateChanged
        [Error]
        InitDone
        DTMFToneDown
        DTMFToneUp
        ControlIsReady
        FFTFrame
    End Enum

    'Private Delegate Sub SafeRaiseEventDel(ByRef args() As Object)
    Private Sub SafeRaiseEventProc(ByRef args() As Object)
        Select Case CType(args(0), PublicEventConstants)
            Case PublicEventConstants.PeakValues
                RaiseEvent PeakValues(DirectCast(args(1), Byte()), DirectCast(args(2), Integer()), DirectCast(args(3), Peak))
            Case PublicEventConstants.DTMFToneDown
                RaiseEvent DTMFToneDown(DirectCast(args(1), DTMFToneConstants))
            Case PublicEventConstants.DTMFToneUp
                RaiseEvent DTMFToneUp()
            Case PublicEventConstants.PlaybackStateChanged
                RaiseEvent PlaybackStateChanged(DirectCast(args(1), PlaybackStateConstants), DirectCast(args(2), PlaybackStateConstants))
            Case PublicEventConstants.RecordingStateChanged
                RaiseEvent RecordingStateChanged(DirectCast(args(1), RecordingStateConstants), DirectCast(args(2), RecordingStateConstants))
            Case PublicEventConstants.MonitoringStateChanged
                RaiseEvent MonitoringStateChanged(DirectCast(args(1), MonitoringStateConstants), DirectCast(args(2), MonitoringStateConstants))
            Case PublicEventConstants.Error
                RaiseEvent [Error](DirectCast(args(1), ErrorConstants), DirectCast(args(2), String), DirectCast(args(3), String))
            Case PublicEventConstants.FFTFrame
                RaiseEvent FFTFrame(DirectCast(args(1), ComplexDouble()), DirectCast(args(2), ComplexDouble()))
        End Select
    End Sub
End Class
