Imports System.Runtime.InteropServices
Imports SlimDX.Multimedia
Imports SlimDX.DirectSound

Partial Public Class DXVUMeterNETGDI
    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Public Shared Function GetDesktopWindow() As IntPtr
    End Function

    ''' <summary>
    ''' Starts recording so the monitored audio will be saved into the file passed as a parameter
    ''' NOTE: DXVUMeterNETGDI uses the LAME encoder to encode the PCM audio into MP3 format.
    ''' Please refer to http://www.mp3dev.org/ for more information about the LAME encoder.
    ''' </summary>
    ''' <param name="fileName">The file name where the audio will be saved</param>
    ''' <param name="overwrite">If set to True and the file pointed by fileName exists, the file will be overwritten, otherwise, the new captured audio will be appended.</param>
    ''' <param name="recordingFormat">The audio format for the recorded file.</param>
    ''' <param name="mp3EncoderConfig">If recordingFormat is set to MP3, then this parameter specifies the format of the MP3 file</param>
    ''' <remarks></remarks>
    Public Sub StartRecording(fileName As String, Optional overwrite As Boolean = False, Optional recordingFormat As RecordingFormatConstants = RecordingFormatConstants.WAV, Optional mp3EncoderConfig As MP3EncoderConfiguration = Nothing)
        savedBytes = 0
        If recordingFormat = RecordingFormatConstants.MP3 Then
            StartRecordingMP3(fileName, mp3EncoderConfig)
        Else
            If pMonitoringState = MonitoringStateConstants.Monitoring And pPlaybackState = PlaybackStateConstants.Idle Then
                Dim Exists As Boolean = IO.File.Exists(fileName)
                If Not Exists Or overwrite Then
                    IO.File.Delete(fileName)
                    pSaveFileName.Create(fileName)
                Else
                    pSaveFileName.Open(fileName)
                    pSaveFileName.File.Seek(0, IO.SeekOrigin.End)
                End If

                inMP3Mode = False
                If pSaveFileName.IsValid Then ChangeRecordingState(RecordingStateConstants.Recording)
            End If
        End If
    End Sub

    Private Sub StartRecordingMP3(FileName As String, MP3EncoderConfig As MP3EncoderConfiguration)
        If pMonitoringState = MonitoringStateConstants.Monitoring And pPlaybackState = PlaybackStateConstants.Idle Then
            Dim Exists As Boolean = IO.File.Exists(FileName)
            If Exists Then IO.File.Delete(FileName)
            pSaveFileName.Create(FileName)
            pSaveFileName.File.Seek(0, IO.SeekOrigin.Begin)

            Try
                Dim h As WaveFormat = pSaveFileName.GetD9Header
                Dim f As CLame.WaveFormat = New CLame.WaveFormat(h.SamplesPerSecond, h.BitsPerSample, h.Channels)

                lameEnc = New CLame(f, MP3EncoderConfig)

                inMP3Mode = True
                If pSaveFileName.IsValid Then ChangeRecordingState(RecordingStateConstants.Recording)
            Catch ex As Exception
                RaiseEvent Error(ErrorConstants.MP3EncoderEx, ex.Source, ex.Message)
                pSaveFileName.File.Close()
                Exit Sub
            End Try
        End If
    End Sub

    ''' <summary>Stops recording</summary>
    Public Sub StopRecording()
        If pRecordingState = RecordingStateConstants.Recording Then
            ChangeRecordingState(RecordingStateConstants.Idle)

            SyncLock pSaveFileName
                If inMP3Mode Then
                    Dim b() As Byte = lameEnc.Close()
                    If b IsNot Nothing Then pSaveFileName.Write(b)
                    pSaveFileName.File.Close()
                Else
                    pSaveFileName.WriteHeader(True)
                End If
            End SyncLock
        End If
    End Sub

    ''' <summary>Pauses the playback</summary>
    Public Sub PausePlaying()
        Select Case pPlaybackState
            Case PlaybackStateConstants.Playing
                playBuf.Stop()
                ChangePlaybackState(PlaybackStateConstants.Paused)
            Case PlaybackStateConstants.Paused
                playBuf.Play(0, PlayFlags.Looping)
                ChangePlaybackState(PlaybackStateConstants.Playing)
        End Select
    End Sub

    ''' <summary>
    ''' Starts playing the file passed as a parameter
    ''' </summary>
    ''' <param name="fileName">The file name to be played</param>
    Public Sub StartPlaying(fileName As String)
        If MonitoringState = MonitoringStateConstants.Monitoring And pRecordingState = RecordingStateConstants.Idle Then
            If IO.File.Exists(fileName) Then
                pPlayFileName.Open(fileName)

                If pPlayFileName.IsValid Then
                    Try
                        'pRenderExclusively = RenderExclusively

                        'If pRenderExclusively Then MonitorRenderThread.Suspend()

                        ' Define the capture format
                        Dim f As WaveFormat = pPlayFileName.GetD9Header

                        ' Define the size of the notification chunks
                        notifySize = If(1024 > f.AverageBytesPerSecond / 32, 1024, f.AverageBytesPerSecond \ 32)
                        notifySize -= notifySize Mod f.BlockAlignment

                        ' Create a buffer description object
                        bufPlayDesc = New SoundBufferDescription()
                        With bufPlayDesc
                            .Format = f
                            .Flags = BufferFlags.ControlPositionNotify Or BufferFlags.GetCurrentPosition2 Or BufferFlags.GlobalFocus Or BufferFlags.Static
                            .SizeInBytes = notifySize * numberPlaybackNotifications
                        End With

                        ' Create the playback buffer object
                        Dim audioDev As DirectSound = New DirectSound(Devices.SelectedDevice.GUID)
                        Dim windowHandle As IntPtr = GetDesktopWindow() ' Me.Handle
                        If windowHandle = 0 Then windowHandle = Me.Handle
                        audioDev.SetCooperativeLevel(windowHandle, CooperativeLevel.Priority)
                        playBuf = New SecondarySoundBuffer(audioDev, bufPlayDesc)

                        ' Define the notification events
                        Dim np(numberPlaybackNotifications - 1) As NotificationPosition

                        For i As Integer = 0 To numberPlaybackNotifications - 1
                            np(i) = New NotificationPosition() With {
                                .Offset = (notifySize * i) + notifySize - 1,
                                .Event = eventPlaybackRender
                            }
                        Next
                        playBuf.SetNotificationPositions(np)

                        nextPlaybackOffset = 0
                        'IdleRenderThread.Suspend()
                        playBuf.Play(0, PlayFlags.Looping)
                        ChangePlaybackState(PlaybackStateConstants.Playing)
                    Catch e As Exception
                        LogMessage(ErrorConstants.StartPlayingEx, e.StackTrace, e.Message)
                    End Try
                Else
                    pPlayFileName.File.Close()
                End If
            End If
        End If
    End Sub

    ''' <summary>Stops playing</summary>
    Public Sub StopPlaying()
        'Try
        If pPlaybackState <> PlaybackStateConstants.Idle Then
            If playbackRenderThread IsNot Nothing Then
                pPlayFileName.File.Close()
                playBuf.Stop()

                ChangePlaybackState(PlaybackStateConstants.Idle)
                'If pRenderExclusively Then MonitorRenderThread.Resume()
            End If
        End If
        'Catch e As System.Exception
        '    LogMessage(ErrorConstants.StopPlayingEx, e.StackTrace, e.Message)
        'End Try
    End Sub

    ''' <summary>Starts monitoring and rendering the monitored audio</summary>
    Public Sub StartMonitoring()
        Try
            ' Define the capture format

            Dim f As New WaveFormat()
            With f
                .Channels = pChannels
                .SamplesPerSecond = pFrequency
                .BitsPerSample = pBitDepth
                .BlockAlignment = CShort(.Channels * .BitsPerSample / 8)
                .AverageBytesPerSecond = .SamplesPerSecond * .BlockAlignment
                .FormatTag = WaveFormatTag.Pcm
            End With

            CleanStuff()

            isMono = (f.Channels = 1)

            notifySize = ToNearestPowerOfTwo(If(1024 > f.AverageBytesPerSecond \ 32, 1024, f.AverageBytesPerSecond \ 32))
            notifySize -= notifySize Mod f.BlockAlignment

            ' Create a buffer description object
            bufCapDesc = New CaptureBufferDescription()
            With bufCapDesc
                .Format = f
                .BufferBytes = notifySize * numberMonitoringNotifications
            End With

            ' Create the capture buffer object
            If capBuf IsNot Nothing Then
                If capBuf.Capturing Then capBuf.Stop()
                capBuf.Dispose()
            End If
            If dsCap IsNot Nothing Then dsCap.Dispose()

            Dim success As Boolean
            Do
                Try
                    success = True
                    dsCap = New DirectSoundCapture(Devices.SelectedDevice.GUID)
                    capBuf = New CaptureBuffer(dsCap, bufCapDesc)
                Catch
                    success = False
                    Application.DoEvents()
                End Try
            Loop Until success

            ' Define the notification events
            Dim np(numberMonitoringNotifications - 1) As NotificationPosition

            For i As Integer = 0 To numberMonitoringNotifications - 1
                np(i) = New NotificationPosition() With {
                    .Offset = (notifySize * i) + notifySize - 1,
                    .Event = eventMonitorRender
                }
            Next
            capBuf.SetNotificationPositions(np)

            nextCaptureOffset = 0
            InitFFT()
            capBuf.Start(True)

            ChangeMonitoringState(MonitoringStateConstants.Monitoring)
        Catch e As Exception
            ResetMonitor()
            LogMessage(ErrorConstants.StartMonitoringEx, e.StackTrace, e.Message)
        End Try
    End Sub

    ''' <summary>Stops monitoring and rendering</summary>
    Public Sub StopMonitoring()
        StopRecording()
        If pMonitoringState = MonitoringStateConstants.Monitoring Then ResetMonitor()
    End Sub

    ''' <summary>
    ''' Converts an array of bytes into an array of integers with values ranging from -32768 to 32767
    ''' This function is used, internally by the control, but you may also use it when handling the <see cref="DXVUMeterNETGDI.PeakValues">PeakValues</see> event.
    ''' </summary>
    ''' <param name="source">Takes an array of bytes</param>
    ''' <returns>Returns an array of integers</returns>
    ''' <remarks>The conversion from bytes to integers is dependant on the currently selected <see cref="DevicesCollection.Device.QualitiesCollection.Quality">quality</see> (ampling frequency, number of chanels,
    ''' and bit depth.</remarks>
    Public Function NormalizeBuffer(source() As Byte) As Integer()
        Dim dataSize As Integer = pBitDepth \ 8
        Dim bb(source.Length \ dataSize - 1) As Integer
        Dim cycleStep As Short = If(pBitDepth = 8, pChannels, CShort(pChannels * 2))
        Dim rtChOffset As Integer = If(pChannels = 1, 0, dataSize)
        Dim tmpB(dataSize - 1) As Byte
        Dim bbStep As Integer = 0

        Dim noiseFilter = Function(v As Double)
                              If pNoiseFilter > 0 AndAlso v <= pNoiseFilter Then
                                  Return 0
                              Else
                                  Return v
                              End If
                          End Function

        For i As Integer = 0 To source.Length - rtChOffset - 1 Step cycleStep
            Select Case pBitDepth
                Case 8
                    If Not mLeftChannelMute Then
                        bb(bbStep) += noiseFilter((128 - source(i)) * 256) * mInternalGain
                    End If

                    If pChannels = 2 AndAlso Not mRightChannelMute Then
                        bb(bbStep + 1) = noiseFilter(128 - source(i + rtChOffset) * 256) * mInternalGain
                    End If
                Case 16
                    If Not mLeftChannelMute Then
                        Array.Copy(source, i, tmpB, 0, dataSize)
                        bb(bbStep) = noiseFilter(BitConverter.ToInt16(tmpB, 0)) * mInternalGain
                    End If

                    If pChannels = 2 AndAlso Not mRightChannelMute Then
                        Array.Copy(source, i + rtChOffset, tmpB, 0, dataSize)
                        bb(bbStep + 1) = noiseFilter(BitConverter.ToInt16(tmpB, 0)) * mInternalGain
                    End If
            End Select

            bbStep += pChannels
        Next

        Return bb
    End Function

    Private Sub CleanStuff()
        If WAVHistL IsNot Nothing Then
            Array.Clear(WAVHistL, 0, WAVHistL.Length)
            Array.Clear(WAVHistR, 0, WAVHistR.Length)
        End If

        If fftL IsNot Nothing Then
            For i As Integer = 0 To pFFTSize - 1
                fftL(i) = New ComplexDouble()
                fftR(i) = New ComplexDouble()
            Next
        End If

        If waveL IsNot Nothing Then
            Array.Clear(waveL, 0, waveL.Length)
            Array.Clear(waveR, 0, waveR.Length)
        End If

        For h As Integer = 0 To pFFTHistorySize - 2
            For idx As Integer = 0 To (fftMax - fftMin) - 1
                FFTHistL(h)(idx) = 0
                FFTHistR(h)(idx) = 0
            Next
        Next

        waveInIndex = 0
        bufferIndex = 0
        processedFFTSamples = 0
    End Sub
End Class