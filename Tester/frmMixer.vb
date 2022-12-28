Imports NDXVUMeterNET
Imports legacy = NDXVUMeterNET.DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection
Imports coreAudio = NDXVUMeterNET.DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection2

Public Class frmMixer
    Private IsALTDown As Boolean

    Friend Sub CreateMixer()
        Dim p As Point = New Point(20, 0)
        If Not Me.Visible Then Exit Sub

        While Me.Controls.Count > 0
            Me.Controls.Remove(Me.Controls(0))
        End While

        If dxvuCtrl.Devices.SelectedDevice.RecordingSources2.Count > 0 Then
            p = CreateCoreAudioMixer(p)
        ElseIf dxvuCtrl.Devices.SelectedDevice.RecordingSources.Count > 0 Then
            p = CreateLegacyMixer(p)
        Else
            Dim lbl As Label = New Label
            lbl.Location = New Point(10, 10)
            lbl.Text = "The selected sound card does not have recording sources"
            lbl.AutoSize = True

            Me.Controls.Add(lbl)

            Me.Size = New Size(lbl.Right + 20, lbl.Bottom + 30)
        End If
        Me.Text = dxvuCtrl.Devices.SelectedDevice.Description + " Recording Mixer"

        Dim sepH As Label = New Label
        Me.Controls.Add(sepH)
        With sepH
            .Visible = False
            .AutoSize = False
            .Text = ""
            .Location = New Point(5, 30)
            .Size = New Size(p.X - 30, 2)
            .FlatStyle = FlatStyle.Flat
            .BorderStyle = BorderStyle.Fixed3D
            .BringToFront()
            .Visible = True
        End With
    End Sub

    Private Function CreateCoreAudioMixer(ByVal p As Point) As Point
        Dim loopBackLine As coreAudio.Line
        loopBackLine = dxvuCtrl.Devices.SelectedDevice.RecordingSources2.LoopbackLine

        For Each recLine As coreAudio.Line In dxvuCtrl.Devices.SelectedDevice.RecordingSources2
            Dim maxBoottom As Integer = 0

            Dim lbl As Label = New Label
            Me.Controls.Add(lbl)
            With lbl
                .Visible = False
                .Text = recLine.Name
                .AutoSize = True
                .Location = New Point(p.X - 12, 10)
                .Visible = True
            End With
            maxBoottom = Math.Max(maxBoottom, lbl.Bottom)

            Dim tb As TrackBar = New TrackBar
            If recLine.HasVolume Then
                Me.Controls.Add(tb)
                With tb
                    .Visible = False
                    .Orientation = Orientation.Vertical
                    .Minimum = recLine.Min
                    .Maximum = recLine.Max
                    .SmallChange = 1
                    .LargeChange = (.Maximum - .Minimum) / 10
                    .TickFrequency = .LargeChange
                    .TickStyle = TickStyle.Both
                    .Location = New Point(p.X + 100 / 2 - .Width / 2 - 20, lbl.Bottom + 12)
                    .Height = 150
                    .Visible = True
                End With
                recLine.SetVolumeBinding(tb)
                maxBoottom = Math.Max(maxBoottom, tb.Bottom)
            Else
                tb.Dispose()
            End If

            'Dim chk As CheckBox = New CheckBox
            'If recLine.CanBeSelected Then
            '    Me.Controls.Add(chk)
            '    With chk
            '        .Visible = False
            '        .Text = "Select"
            '        .AutoSize = True
            '        .Location = New Point(lbl.Left, maxBoottom + 4)
            '        .Visible = True
            '    End With
            '    recLine.SetSelectedBinding(chk)
            '    maxBoottom = Math.Max(maxBoottom, chk.Bottom)
            'Else
            '    chk.Dispose()
            'End If

            If loopBackLine IsNot Nothing AndAlso recLine.Name = loopBackLine.Name Then
                Dim backHL As Panel = New Panel
                Me.Controls.Add(backHL)
                With backHL
                    .Location = New Point(p.X - 20 + 2, 0)
                    .Size = New Size(100 - 4, Me.Height)
                    .BackColor = Color.LightPink
                    lbl.BackColor = Color.LightPink
                    'If recLine.CanBeSelected Then chk.BackColor = Color.LightPink
                    If recLine.HasVolume Then tb.BackColor = Color.LightPink
                    .SendToBack()
                    .Visible = True
                End With
            End If

            Dim sep As Label = New Label
            Me.Controls.Add(sep)
            With sep
                .Visible = False
                .AutoSize = False
                .Text = ""
                .Location = New Point(p.X + 100 - 20, lbl.Bottom + 8)
                .Size = New Size(2, maxBoottom - 25)
                .FlatStyle = FlatStyle.Flat
                .BorderStyle = BorderStyle.Fixed3D
                .Visible = True
            End With

            p.X += 100
            p.Y = Math.Max(maxBoottom, p.Y)
        Next

        Me.Controls.Remove(Me.Controls(Me.Controls.Count - 1))

        Me.Size = New Size(p.X - 15, p.Y + 32)
    End Function

    Private Function CreateLegacyMixer(ByVal p As Point) As Point
        Dim loopBackLine As legacy.Line
        loopBackLine = dxvuCtrl.Devices.SelectedDevice.RecordingSources.LoopbackLine

        For Each recLine As legacy.Line In dxvuCtrl.Devices.SelectedDevice.RecordingSources
            Dim maxBoottom As Integer = 0

            Dim lbl As Label = New Label
            Me.Controls.Add(lbl)
            With lbl
                .Visible = False
                .Text = recLine.Name
                .AutoSize = True
                .Location = New Point(p.X - 12, 10)
                .Visible = True
            End With
            maxBoottom = Math.Max(maxBoottom, lbl.Bottom)

            Dim tb As TrackBar = New TrackBar
            If recLine.HasVolume Then
                Me.Controls.Add(tb)
                With tb
                    .Visible = False
                    .Orientation = Orientation.Vertical
                    .Minimum = recLine.Min
                    .Maximum = recLine.Max
                    .SmallChange = 1
                    .LargeChange = (.Maximum - .Minimum) / 10
                    .TickFrequency = .LargeChange
                    .TickStyle = TickStyle.Both
                    .Location = New Point(p.X + 100 / 2 - .Width / 2 - 20, lbl.Bottom + 12)
                    .Height = 150
                    .Visible = True
                End With
                recLine.SetVolumeBinding(tb)
                maxBoottom = Math.Max(maxBoottom, tb.Bottom)
            Else
                tb.Dispose()
            End If

            Dim chk As CheckBox = New CheckBox
            If recLine.CanBeSelected Then
                Me.Controls.Add(chk)
                With chk
                    .Visible = False
                    .Text = "Select"
                    .AutoSize = True
                    .Location = New Point(lbl.Left, maxBoottom + 4)
                    .Visible = True
                End With
                recLine.SetSelectedBinding(chk)
                maxBoottom = Math.Max(maxBoottom, chk.Bottom)
            Else
                chk.Dispose()
            End If

            If loopBackLine IsNot Nothing AndAlso recLine.Name = loopBackLine.Name Then
                Dim backHL As Panel = New Panel
                Me.Controls.Add(backHL)
                With backHL
                    .Location = New Point(p.X - 20 + 2, 0)
                    .Size = New Size(100 - 4, Me.Height)
                    .BackColor = Color.LightPink
                    lbl.BackColor = Color.LightPink
                    If recLine.CanBeSelected Then chk.BackColor = Color.LightPink
                    If recLine.HasVolume Then tb.BackColor = Color.LightPink
                    .SendToBack()
                    .Visible = True
                End With
            End If

            Dim sep As Label = New Label
            Me.Controls.Add(sep)
            With sep
                .Visible = False
                .AutoSize = False
                .Text = ""
                .Location = New Point(p.X + 100 - 20, lbl.Bottom + 8)
                .Size = New Size(2, maxBoottom - 25)
                .FlatStyle = FlatStyle.Flat
                .BorderStyle = BorderStyle.Fixed3D
                .Visible = True
            End With

            p.X += 100
            p.Y = Math.Max(maxBoottom, p.Y)
        Next

        Me.Controls.Remove(Me.Controls(Me.Controls.Count - 1))

        Me.Size = New Size(p.X - 15, p.Y + 32)
    End Function

    Private Sub frmMixer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub frmMixer_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then CreateMixer()
    End Sub

    Private Sub frmMixer_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If IsALTDown Then
            Select Case e.Delta
                Case Is > 0
                    Me.Opacity = Math.Min(Me.Opacity + 0.1, 1)
                Case Is < 0
                    Me.Opacity = Math.Max(Me.Opacity - 0.1, 0.2)
            End Select
        End If
    End Sub

    Private Sub frmMixer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        IsALTDown = e.Alt
    End Sub

    Private Sub frmMixer_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        IsALTDown = Not e.Alt
    End Sub
End Class