Imports SlimDX.DirectSound

Partial Public Class DXVUMeterNETGDI
    Partial Public Class DevicesCollection
        ''' <summary>Represents a device (sound card)</summary>
        Partial Public Class Device
            Implements IDisposable

            Private pName As String
            Private pDescription As String
            Private pGUID As Guid
            Private pSelected As Boolean
            Private pParent As DevicesCollection
            Private mxp As CMixerPro = New CMixerPro()
            Private selMixer As CMixer
            Private cAudio As CCoreAudio

            ''' <summary>Collection of all the devices supported recording parameters</summary>
            Public Qualities As New QualitiesCollection()

            ''' <summary>Sets or returns the currently selected device</summary>
            Public Property Selected() As Boolean
                Get
                    Return pSelected
                End Get
                Set(value As Boolean)
                    Dim d As Device
                    If value = False Then
                        For Each d In pParent
                            d.pSelected = d.IsDefault
                        Next
                    Else
                        For Each d In pParent
                            d.pSelected = False
                        Next
                        pSelected = True
                    End If
                End Set
            End Property

            Public RecordingSources As RecordingSourcesCollection
            Public RecordingSources2 As RecordingSourcesCollection2

            ''' <summary>Returns true if the device is the system's default recording device</summary>
            Public ReadOnly Property IsDefault() As Boolean
                Get
                    Return pGUID.ToString = "00000000-0000-0000-0000-000000000000"
                End Get
            End Property

            ''' <summary>Returns the name of the device</summary>
            Public ReadOnly Property Name() As String
                Get
                    Return pName
                End Get
            End Property

            ''' <summary>Returns the description of the device</summary>
            Public ReadOnly Property Description() As String
                Get
                    Return pDescription
                End Get
            End Property

            ''' <summary>Returns the GUI of the device</summary>
            Public ReadOnly Property GUID() As Guid
                Get
                    Return pGUID
                End Get
            End Property

            Public Overrides Function ToString() As String
                Return pDescription
            End Function

            Friend Sub New(name As String, description As String, GUI As Guid, parent As DevicesCollection)
                pName = name
                pDescription = description
                pGUID = GUI
                pSelected = IsDefault
                pParent = parent

                Dim c As DirectSoundCapture = New DirectSoundCapture(pGUID)

                Try
                    If c.Capabilities.Format11KhzMono16Bit Then Qualities.Add(New QualitiesCollection.Quality(11100, 16, 1, "11Khz Mono 16bit"))
                    If c.Capabilities.Format11KhzMono8Bit Then Qualities.Add(New QualitiesCollection.Quality(11100, 8, 1, "11Khz Mono 8bit"))
                    If c.Capabilities.Format11KhzStereo16Bit Then Qualities.Add(New QualitiesCollection.Quality(11100, 16, 2, "11Khz Stereo 16bit"))
                    If c.Capabilities.Format11KhzStereo8Bit Then Qualities.Add(New QualitiesCollection.Quality(11100, 8, 2, "11Khz Stereo 8bit"))

                    If c.Capabilities.Format22KhzMono16Bit Then Qualities.Add(New QualitiesCollection.Quality(22000, 16, 1, "22Khz Mono 16bit"))
                    If c.Capabilities.Format22KhzMono8Bit Then Qualities.Add(New QualitiesCollection.Quality(22000, 8, 1, "22Khz Mono 8bit"))
                    If c.Capabilities.Format22KhzStereo16Bit Then Qualities.Add(New QualitiesCollection.Quality(22000, 16, 2, "22Khz Stereo 16bit"))
                    If c.Capabilities.Format22KhzStereo8Bit Then Qualities.Add(New QualitiesCollection.Quality(22000, 8, 2, "22Khz Stereo 8bit"))

                    If c.Capabilities.Format44KhzMono16Bit Then Qualities.Add(New QualitiesCollection.Quality(44100, 16, 1, "44Khz Mono 16bit"))
                    If c.Capabilities.Format44KhzMono8Bit Then Qualities.Add(New QualitiesCollection.Quality(44100, 8, 1, "44Khz Mono 8bit"))
                    If c.Capabilities.Format44KhzStereo16Bit Then Qualities.Add(New QualitiesCollection.Quality(44100, 16, 2, "44Khz Stereo 16bit"))
                    If c.Capabilities.Format44KhzStereo8Bit Then Qualities.Add(New QualitiesCollection.Quality(44100, 8, 2, "44Khz Stereo 8bit"))

                    If c.Capabilities.Format48KhzMono16Bit Then Qualities.Add(New QualitiesCollection.Quality(48000, 16, 1, "48Khz Mono 16bit"))
                    If c.Capabilities.Format48KhzMono8Bit Then Qualities.Add(New QualitiesCollection.Quality(48000, 8, 1, "48Khz Mono 8bit"))
                    If c.Capabilities.Format48KhzStereo16Bit Then Qualities.Add(New QualitiesCollection.Quality(48000, 16, 2, "48Khz Stereo 16bit"))
                    If c.Capabilities.Format48KhzStereo8Bit Then Qualities.Add(New QualitiesCollection.Quality(48000, 8, 2, "48Khz Stereo 8bit"))

                    If c.Capabilities.Format96KhzMono16Bit Then Qualities.Add(New QualitiesCollection.Quality(96000, 16, 1, "96Khz Mono 16bit"))
                    If c.Capabilities.Format96KhzMono8Bit Then Qualities.Add(New QualitiesCollection.Quality(96000, 8, 1, "96Khz Mono 8bit"))
                    If c.Capabilities.Format96KhzStereo16Bit Then Qualities.Add(New QualitiesCollection.Quality(96000, 16, 2, "96Khz Stereo 16bit"))
                    If c.Capabilities.Format96KhzStereo8Bit Then Qualities.Add(New QualitiesCollection.Quality(96000, 8, 2, "96Khz Stereo 8bit"))

                    If CCoreAudio.RequiresCoreAudio Then
                        cAudio = New CCoreAudio(mxp)
                        For Each mixer In cAudio.Mixers
                            If mixer.Enabled AndAlso mixer.DataFlow = CoreAudio.DataFlow.Capture Then
                                RecordingSources2 = New RecordingSourcesCollection2(mixer)
                                Exit For
                            End If
                        Next
                        RecordingSources = New RecordingSourcesCollection()
                    Else
                        Dim waveIn As Lines
                        If pGUID.ToString = "00000000-0000-0000-0000-000000000000" Then
                            selMixer = Nothing
                            For Each m As CMixer In mxp.Mixers
                                waveIn = m.LinesByComponentType(CLine.ComponentTypeConstants.ctcDST_WAVEIN)
                                If waveIn.Count > 0 Then
                                    selMixer = m
                                    Exit For
                                End If
                            Next
                            If selMixer Is Nothing Then selMixer = mxp.Mixers(1)
                        Else
                            For Each Me.selMixer In mxp.Mixers
                                If selMixer.DeviceName = pDescription Then Exit For
                            Next
                        End If

                        waveIn = selMixer.LinesByComponentType(CLine.ComponentTypeConstants.ctcDST_WAVEIN)
                        If waveIn.Count > 0 Then
                            RecordingSources = New RecordingSourcesCollection(waveIn.Item(1))
                        Else
                            RecordingSources = New RecordingSourcesCollection()
                        End If
                        RecordingSources2 = New RecordingSourcesCollection2()
                    End If
                Catch
                    RecordingSources = New RecordingSourcesCollection()
                    RecordingSources2 = New RecordingSourcesCollection2()
                Finally
                    If c IsNot Nothing Then c.Dispose()
                End Try
            End Sub

            Protected Overrides Sub Finalize()
                Dispose(False)
                MyBase.Finalize()
            End Sub

#Region "IDisposable Support"
            Private disposedValue As Boolean ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: dispose managed state (managed objects).
                        pName = Nothing
                        pDescription = Nothing
                        pGUID = Nothing
                        pSelected = Nothing
                        pParent = Nothing
                        Qualities = Nothing
                    End If

                    ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                    ' TODO: set large fields to null.
                    If cAudio IsNot Nothing Then
                        cAudio.Dispose()
                        cAudio = Nothing
                    End If
                End If
                Me.disposedValue = True
            End Sub

            ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
            'Protected Overrides Sub Finalize()
            '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            '    Dispose(False)
            '    MyBase.Finalize()
            'End Sub

            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region
        End Class
    End Class
End Class