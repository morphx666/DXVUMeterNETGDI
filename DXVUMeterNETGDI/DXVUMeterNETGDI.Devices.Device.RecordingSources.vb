Partial Public Class DXVUMeterNETGDI
    Partial Public Class DevicesCollection
        Partial Public Class Device
            ''' <summary>
            ''' Provides access to the recording sources of a device using the legacy mixer API provided by MixerProNET (for pre-Vista versions of Windows)
            ''' </summary>
            ''' <remarks>This class uses MixerProNET to gain access to the volume and selected state of all the recording sources in a line.
            ''' To have full control over all the controls in a mixer, please visit <see href="http://software.xfx.net/netcl/mxp/">MixerProNET's web site</see></remarks>
            Partial Public Class RecordingSourcesCollection
                Implements ICollection

                ''' <summary>
                ''' This class represents a line in a mixer.
                ''' Lines usually present the ability to control the audio level and the selected/unselected state
                ''' </summary>
                Public Class Line
                    Private mLine As CLine
                    Private mMuxSel As CCtrlItem
                    Private mVol As CControl

                    Friend Sub New(line As CLine, waveInLine As CLine)
                        mLine = line

                        Dim cs As Controls = mLine.ControlsByType(CControl.ControlTypeConstants.ctrltcVOLUME)
                        If cs.Count > 0 Then mVol = cs.Item(1)

                        Dim cc As Controls = waveInLine.ControlsByType(CControl.ControlTypeConstants.ctrltcMUX)
                        'If cc.Count > 0 Then mMuxSel = cc(1).CtrlItems(waveInLine.Parent.Lines.Count - mLine.Index + 1)
                        If cc.Count > 0 Then
                            Dim m As CMixer = waveInLine.Parent
                            Dim ln As Integer = 0
                            For Each sl As CLine In m.LinesByLineType(CLine.LineTypeConstants.ltcDestination)
                                ln += (m.LinesByConnection(sl.ID).Count + 1)
                                If sl.ID = waveInLine.ID Then Exit For
                            Next
                            mMuxSel = cc(1).CtrlItems(ln - mLine.Index + 1)
                        End If
                    End Sub

                    Friend ReadOnly Property theLine() As CLine
                        Get
                            Return mLine
                        End Get
                    End Property

                    ''' <summary>
                    ''' Returns the long name of the line
                    ''' </summary>
                    Public ReadOnly Property Name() As String
                        Get
                            Return mLine.LongName
                        End Get
                    End Property

                    ''' <summary>
                    ''' Sets or returns the recording volume of the line
                    ''' </summary>
                    ''' <remarks>The value must be between the <see cref="Line.Min">Line.Min</see> and <see cref="Line.Max">Line.Max</see> range</remarks>
                    Public Property Volume() As Integer
                        Get
                            If mVol IsNot Nothing Then
                                Return mVol.UniformValue
                            Else
                                Return -1
                            End If
                        End Get
                        Set(value As Integer)
                            If mVol IsNot Nothing Then
                                If value > mVol.Max Then value = mVol.Max
                                If value < mVol.Min Then value = mVol.Min
                                mVol.UniformValue = value
                            End If
                        End Set
                    End Property

                    ''' <summary>
                    ''' Sets or returns the selected recording source
                    ''' </summary>
                    ''' <value>Setting this property to True will cause the line to become the selected recording source</value>
                    ''' <returns>Returns True if the line is the selected recording source; otherwise will return false</returns>
                    Public Property Selected() As Boolean
                        Get
                            If mMuxSel IsNot Nothing Then Return (mMuxSel.UniformValue <> 0)
                        End Get
                        Set(value As Boolean)
                            If mMuxSel IsNot Nothing Then
                                If value Then
                                    mMuxSel.UniformValue = 1
                                Else
                                    mMuxSel.UniformValue = 0
                                End If
                            End If
                        End Set
                    End Property

                    ''' <summary>
                    ''' Indicates if the line supports changing its volume
                    ''' </summary>
                    ''' <returns>Returns True if the line supports changing or querying the volume control</returns>
                    Public ReadOnly Property HasVolume() As Boolean
                        Get
                            Return mVol IsNot Nothing
                        End Get
                    End Property

                    ''' <summary>
                    ''' Indicates if the line can be selected as a recording source
                    ''' </summary>
                    ''' <returns>Returns True if the line can be selected as a recording source</returns>
                    Public ReadOnly Property CanBeSelected() As Boolean
                        Get
                            Return mMuxSel IsNot Nothing
                        End Get
                    End Property

                    ''' <summary>
                    ''' Returns the minimum value supported by the volume control on this line
                    ''' </summary>
                    Public ReadOnly Property Min() As Integer
                        Get
                            If mVol IsNot Nothing Then
                                Return mVol.Min
                            Else
                                Return -1
                            End If
                        End Get
                    End Property

                    ''' <summary>
                    ''' Returns the maximum value supported by the volume control on this line
                    ''' </summary>
                    Public ReadOnly Property Max() As Integer
                        Get
                            If mVol IsNot Nothing Then
                                Return mVol.Max
                            Else
                                Return -1
                            End If
                        End Get
                    End Property

                    ''' <summary>
                    ''' Defines a binding between the volume control in the line and a <see cref="Control">control</see>
                    ''' </summary>
                    ''' <param name="Control">The control to be used to represent and/or change the volume. This is usually a <see cref="TrackBar">TrackBar</see></param>
                    ''' <param name="PropertyName">The name of the property that will be used to set and display the volume. When using a TrackBar this should be set to <see cref="TrackBar.Value">Value</see></param>
                    ''' <param name="EventName">The name of the event that the line will listen for to detect changes in the control. When using a TrackBar this should be set to <see cref="TrackBar.ValueChanged">ValueChanged</see></param>
                    ''' <remarks>It is very important to call <see cref="Line.RemoveBingings">RemoveBindings</see> when the binding are not going to be used or the control is going to be disposed.</remarks>
                    Public Sub SetVolumeBinding(Control As Control, Optional PropertyName As String = "Value", Optional EventName As String = "ValueChanged")
                        If mVol IsNot Nothing Then mVol.Binding.Define(Control, PropertyName, EventName)
                    End Sub

                    ''' <summary>
                    ''' Defines a binding between the select mutex control in the line and a <see cref="Control">control</see>
                    ''' </summary>
                    ''' <param name="Control">The control to be used to represent and/or change the selection state as a recording source for this line. This is usually a <see cref="CheckBox">CheckBox</see></param>
                    ''' <param name="PropertyName">The name of the property that will be used to set and display the selection state. When using a CheckBox this should be set to <see cref="CheckBox.Checked">Checked</see></param>
                    ''' <param name="EventName">The name of the event that the line will listen for to detect changes in the control. When using a CheckBox this should be set to <see cref="CheckBox.CheckStateChanged">CheckStateChanged</see></param>
                    ''' <remarks>It is very important to call <see cref="Line.RemoveBingings">RemoveBindings</see> when the binding are not going to be used or the control is going to be disposed.</remarks>
                    Public Sub SetSelectedBinding(Control As Control, Optional PropertyName As String = "Checked", Optional EventName As String = "CheckStateChanged")
                        If mMuxSel IsNot Nothing Then mMuxSel.Binding.Define(Control, PropertyName, EventName)
                    End Sub

                    ''' <summary>
                    ''' Removes any previously defined bindings.
                    ''' See <see cref="Line.SetVolumeBinding"></see> and <see cref="Line.SetSelectedBinding"></see> for information on how to define bindings.
                    ''' </summary>
                    Public Sub RemoveBingings()
                        If mVol IsNot Nothing Then mVol.Binding.Remove()
                        If mMuxSel IsNot Nothing Then mMuxSel.Binding.Remove()
                    End Sub

                    Public Overrides Function ToString() As String
                        Return Name
                    End Function
                End Class

                Private mCol As Collection = New Collection()
                Private mLine As CLine

                Friend Sub New(waveInLine As CLine)
                    mLine = waveInLine

                    For Each l As CLine In mLine.Parent.LinesByConnection(mLine.ID)
                        mCol.Add(New Line(l, mLine))
                    Next
                End Sub

                Public Function LoopbackLine() As Line
                    For Each ls As Line In mCol
                        If ls.theLine.ComponentType = CLine.ComponentTypeConstants.ctcSRC_WAVEOUT Then
                            Return ls
                        End If
                    Next

                    Return Nothing
                End Function

                Friend Sub New()
                End Sub

                Private Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo

                End Sub

                Public ReadOnly Property Count() As Integer Implements ICollection.Count
                    Get
                        Return mCol.Count
                    End Get
                End Property

                Private ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
                    Get

                    End Get
                End Property

                Private ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
                    Get
                        Return Me
                    End Get
                End Property

                Private Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                    Return mCol.GetEnumerator
                End Function
            End Class

            ''' <summary>
            ''' Provides access to the recording sources of a device using the new Core Audio API provided by MixerProNET (for Windows Vista and later)
            ''' </summary>
            ''' <remarks>This class uses MixerProNET to gain access to the volume and selected state of all the recording sources in a line.
            ''' To have full control over all the controls in a mixer, please visit <see href="http://software.xfx.net/netcl/mxp/">MixerProNET's web site</see></remarks>
            Partial Public Class RecordingSourcesCollection2
                Implements ICollection

                ''' <summary>
                ''' This class represents a line in a mixer.
                ''' Lines usually present the ability to control the audio level and the selected/unselected state
                ''' </summary>
                Public Class Line
                    Private mLine As CCoreAudio.CLine
                    Private mVol As CCoreAudio.CControl

                    Friend Sub New(line As CCoreAudio.CLine)
                        mLine = line
                        For Each ctrl In mLine.Controls
                            If ctrl.ControlVolume IsNot Nothing Then
                                mVol = ctrl
                                Exit For
                            End If
                        Next
                    End Sub

                    Friend ReadOnly Property theLine() As CCoreAudio.CLine
                        Get
                            Return mLine
                        End Get
                    End Property

                    ''' <summary>
                    ''' Returns the long name of the line
                    ''' </summary>
                    Public ReadOnly Property Name() As String
                        Get
                            Return mLine.Name
                        End Get
                    End Property

                    ''' <summary>
                    ''' Sets or returns the recording volume of the line
                    ''' </summary>
                    ''' <remarks>The value must be between the <see cref="Line.Min">Line.Min</see> and <see cref="Line.Max">Line.Max</see> range</remarks>
                    Public Property Volume() As Integer
                        Get
                            If HasVolume Then
                                Return mVol.ControlVolume.Volume
                            Else
                                Return -1
                            End If
                        End Get
                        Set(value As Integer)
                            If HasVolume Then
                                If value > mVol.ControlVolume.Max Then value = mVol.ControlVolume.Max
                                If value < mVol.ControlVolume.Min Then value = mVol.ControlVolume.Min
                                mVol.ControlVolume.Volume = value
                            End If
                        End Set
                    End Property

                    ''' <summary>
                    ''' Indicates if the line supports changing its volume
                    ''' </summary>
                    ''' <returns>Returns True if the line supports changing or querying the volume control</returns>
                    Public ReadOnly Property HasVolume() As Boolean
                        Get
                            Return mVol IsNot Nothing AndAlso mVol.ControlVolume IsNot Nothing
                        End Get
                    End Property

                    ''' <summary>
                    ''' Returns the minimum value supported by the volume control on this line
                    ''' </summary>
                    Public ReadOnly Property Min() As Integer
                        Get
                            If mVol IsNot Nothing Then
                                Return mVol.ControlVolume.Min
                            Else
                                Return -1
                            End If
                        End Get
                    End Property

                    ''' <summary>
                    ''' Returns the maximum value supported by the volume control on this line
                    ''' </summary>
                    Public ReadOnly Property Max() As Integer
                        Get
                            If HasVolume Then
                                Return mVol.ControlVolume.Max
                            Else
                                Return -1
                            End If
                        End Get
                    End Property

                    ''' <summary>
                    ''' Defines a binding between the volume control in the line and a <see cref="Control">control</see>
                    ''' </summary>
                    ''' <param name="Control">The control to be used to represent and/or change the volume. This is usually a <see cref="TrackBar">TrackBar</see></param>
                    ''' <param name="PropertyName">The name of the property that will be used to set and display the volume. When using a TrackBar this should be set to <see cref="TrackBar.Value">Value</see></param>
                    ''' <param name="EventName">The name of the event that the line will listen for to detect changes in the control. When using a TrackBar this should be set to <see cref="TrackBar.ValueChanged">ValueChanged</see></param>
                    ''' <remarks>It is very important to call <see cref="Line.RemoveBingings">RemoveBindings</see> when the binding are not going to be used or the control is going to be disposed.</remarks>
                    Public Sub SetVolumeBinding(Control As Control, Optional PropertyName As String = "Value", Optional EventName As String = "ValueChanged")
                        If HasVolume Then mVol.ControlVolume.Binding.Define(Control, PropertyName, EventName)
                    End Sub

                    ''' <summary>
                    ''' Removes any previously defined bindings.
                    ''' See <see cref="Line.SetVolumeBinding"></see> and <see cref="RecordingSourcesCollection.Line.SetSelectedBinding"></see> for information on how to define bindings.
                    ''' </summary>
                    Public Sub RemoveBingings()
                        If HasVolume Then mVol.ControlVolume.Binding.Remove()
                    End Sub

                    Public ReadOnly Property CoreAudioControl As CCoreAudio.CControl
                        Get
                            Return mVol
                        End Get
                    End Property

                    Public ReadOnly Property CoreAudioLine As CCoreAudio.CLine
                        Get
                            Return mLine
                        End Get
                    End Property

                    Public Overrides Function ToString() As String
                        Return Name
                    End Function
                End Class

                Private mCol As Collection = New Collection()
                Private mMixer As CCoreAudio.CMixer

                Friend Sub New(captureMixer As CCoreAudio.CMixer)
                    mMixer = captureMixer

                    For Each l As CCoreAudio.CLine In mMixer.Lines
                        mCol.Add(New Line(l))
                    Next
                End Sub

                Public Function LoopbackLine() As Line
                    'For Each ls As Line In mCol
                    '    If ls.theLine.ComponentType = CLine.ComponentTypeConstants.ctcSRC_WAVEOUT Then
                    '        Return ls
                    '    End If
                    'Next

                    Return Nothing
                End Function

                Friend Sub New()
                End Sub

                Private Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo

                End Sub

                Public ReadOnly Property Count() As Integer Implements ICollection.Count
                    Get
                        Return mCol.Count
                    End Get
                End Property

                Private ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
                    Get

                    End Get
                End Property

                Private ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
                    Get
                        Return Me
                    End Get
                End Property

                Private Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                    Return mCol.GetEnumerator
                End Function
            End Class
        End Class
    End Class
End Class
