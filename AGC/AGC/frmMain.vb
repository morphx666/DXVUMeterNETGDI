Imports NDXVUMeterNET

Public Class frmMain
    Private lastPeakAvg() As Single
    Private volTreshold As Single
    Private selLine As DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection.Line
    Private WithEvents Winamp As CWinamp
    Private IsRunning As Boolean
    Private tmrStatusBlink As System.Threading.Timer

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        dxvunCtrl.StopMonitoring()
        ResetStatusBlink()

        Do While dxvunCtrl.MonitoringState <> DXVUMeterNETGDI.MonitoringStateConstants.Idle
            Application.DoEvents()
        Loop
        dxvunCtrl.Dispose()
    End Sub

    Private Sub dxvunCtrl_ControlIsReady() Handles dxvunCtrl.ControlIsReady
        Dim i As Integer

        For Each device As DXVUMeterNETGDI.DevicesCollection.Device In dxvunCtrl.Devices
            i = cmbDevices.Items.Add(device)
            If device.Selected Then cmbDevices.SelectedIndex = i
        Next
        cmbDevices.SelectedIndex = 0
    End Sub

    Private Sub tbTreshold_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTreshold.Scroll
        SetTreshold()
    End Sub

    Private Sub SetTreshold()
        volTreshold = tbTreshold.Value / tbTreshold.Maximum
        pTreshold.Location = New Point(tbTreshold.Left - 5, dxvunCtrl.Top + dxvunCtrl.Height - volTreshold * (dxvunCtrl.Height - 2))
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbSens.Value = tbSens.Maximum - 50
        SetSensibility()

        tbTreshold.Value = 15
        SetTreshold()

        ' Lazyness!!!
        Label.CheckForIllegalCrossThreadCalls = False

        pProgress.Location = New Point(tbWAVol.Left, tbWAVol.Top - pProgress.Height - 5)
        pProgress.Width = 0

        tmrStatusBlink = New Threading.Timer(New Threading.TimerCallback(AddressOf BlinkStatus), Nothing, Threading.Timeout.Infinite, Threading.Timeout.Infinite)
        ResetStatusBlink()

        Winamp = New CWinamp

        ' Uncoment the following line and replace it with the one you received via email
        '   with your own registration information.
        'This will remove the DEMO message in DXVUMeterNET
        'DxvuMeterNET1.LicenseControl("XXXXXX", "XXX-XXX-XXX")
    End Sub

    Private Sub BlinkStatus(ByVal state As Object)
        If btnStatus.BackColor = Color.FromKnownColor(KnownColor.Control) Then
            btnStatus.BackColor = Color.Red
        Else
            btnStatus.BackColor = Color.FromKnownColor(KnownColor.Control)
        End If
    End Sub

    Private Sub ResetStatusBlink()
        tmrStatusBlink.Change(Threading.Timeout.Infinite, Threading.Timeout.Infinite)
        btnStatus.BackColor = Color.FromKnownColor(KnownColor.Control)
        btnStatus.Text = "Idle"
    End Sub

    Private Sub cmbDevices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDevices.SelectedIndexChanged
        Dim i As Integer
        Dim selDevice As DXVUMeterNETGDI.DevicesCollection.Device = CType(cmbDevices.SelectedItem, DXVUMeterNETGDI.DevicesCollection.Device)
        dxvunCtrl.Devices.SelectedDevice = selDevice

        cmbSources.Items.Clear()
        For Each srcLine As DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection.Line In selDevice.RecordingSources
            i = cmbSources.Items.Add(srcLine)
            If srcLine.Selected Then cmbSources.SelectedIndex = i
        Next
        If cmbSources.SelectedIndex = -1 AndAlso cmbSources.Items.Count > 0 Then
            cmbSources.SelectedIndex = 0
        End If

        Dim q As DXVUMeterNETGDI.DevicesCollection.Device.QualitiesCollection.Quality
        cmbCapabilities.Items.Clear()
        For Each q In selDevice.Qualities
            cmbCapabilities.Items.Add(q)
            If q.BitDepth = 16 And q.Channels = 2 And q.Frequency = 44100 Then
                cmbCapabilities.SelectedIndex = cmbCapabilities.Items.Count - 1
            End If
        Next
        If cmbCapabilities.SelectedIndex = -1 Then cmbCapabilities.SelectedIndex = cmbCapabilities.Items.Count - 1
    End Sub

    Private Sub cmbSources_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSources.SelectedIndexChanged
        Dim srcLine As DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection.Line = CType(cmbSources.SelectedItem, DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection.Line)

        SelectRecordingSource(srcLine)
    End Sub

    Private Sub SelectRecordingSource(ByVal srcLine As DXVUMeterNETGDI.DevicesCollection.Device.RecordingSourcesCollection.Line)
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
        If IsRunning Then Exit Sub

        If Winamp.IsRunning Then
            IsRunning = True

            cmbCapabilities.Enabled = False
            cmbDevices.Enabled = False
            cmbSources.Enabled = False
            btnStop.Enabled = True

            dxvunCtrl.StartMonitoring()
            Winamp.Volume = 0
            tmrStatusBlink.Change(0, 700)
        Else
            MsgBox("Winamp was not detected." + vbCrLf + "Please start Winamp before starting DXVUMeterNET AGC", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Unable to start")
        End If
    End Sub

    Private Sub Winamp_WinampStateChanged() Handles Winamp.RunningStateChanged
        If Winamp.IsRunning Then
            lblWAPlaying.Text = "Winamp detected! Press Start to begin..."
            btnStart.Image = My.Resources.wa_on
        Else
            StopWorking()
            lblWAPlaying.Text = "Winamp not detected..."
            btnStart.Image = My.Resources.wa_off
        End If
    End Sub

    Private Sub Winamp_TitleChanged(ByVal Title As String) Handles Winamp.TitleChanged
        lblWAPlaying.Text = Title
        ResetAverages()
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        StopWorking()
    End Sub

    Private Sub StopWorking()
        dxvunCtrl.StopMonitoring()

        pbVolUp.Visible = False
        pbVolDn.Visible = False

        cmbCapabilities.Enabled = True
        cmbDevices.Enabled = True
        cmbSources.Enabled = True
        btnStop.Enabled = False

        pProgress.Width = 0

        ResetStatusBlink()

        IsRunning = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub tbSens_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSens.Scroll
        SetSensibility()
    End Sub

    Private Sub SetSensibility()
        ReDim Preserve lastPeakAvg(tbSens.Maximum - tbSens.Value + 1)
    End Sub

    Private Sub tbWAVol_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbWAVol.ValueChanged
        ResetAverages()
    End Sub

    Private Sub ResetAverages()
        For i As Integer = lastPeakAvg.Length - 1 To 1 Step -1
            lastPeakAvg(i) = (lastPeakAvg(i) + lastPeakAvg(0)) / 2
        Next
    End Sub

    Private Sub dxvunCtrl_PeakValues(audioBuffer() As Byte, normalizedAudioBuffer() As Integer, maxPeak As NDXVUMeterNET.DXVUMeterNETGDI.Peak) Handles dxvunCtrl.PeakValues
        If Not Winamp.IsRunning Then Exit Sub

        Dim curPos As Integer = Winamp.GetCurPos
        Dim curLen As Integer = Winamp.GetCurLength
        Dim IgnoreAudio As Boolean = False

        If curLen > 0 Then
            ' Ignore audio between the first half and fifth second and also at the last twenty seconds
            ' This is to avoid the AGC from pumping up the volume on the fade-in and fade-out part of the audio
            If (curPos >= 500 And curPos <= 5000) Or (curLen - curPos < 20000) Then
                btnStatus.Text = "Ignoring"
                IgnoreAudio = True
            Else
                btnStatus.Text = "Working"
                IgnoreAudio = False
            End If
        End If

        If Not IgnoreAudio Then
            Dim cPeak As Single = (maxPeak.Left + maxPeak.Right) / 2
            Dim avg As Single
            For i As Integer = lastPeakAvg.Length - 1 To 1 Step -1
                avg += lastPeakAvg(i - 1)
                lastPeakAvg(i) = lastPeakAvg(i - 1)
            Next
            lastPeakAvg(0) = cPeak
            avg = (avg + cPeak) / lastPeakAvg.Length

            Dim cVol As Integer = Winamp.Volume

            If avg < volTreshold Then
                Winamp.Volume += CInt((volTreshold * 3 - avg * 3)) ^ 2
            ElseIf avg > volTreshold Then
                Winamp.Volume -= CInt((volTreshold * 5 - avg * 5)) ^ 2
            End If

            If Winamp.Volume > cVol Then
                pbVolUp.Visible = True
                pbVolDn.Visible = False
            ElseIf Winamp.Volume < cVol Then
                pbVolUp.Visible = False
                pbVolDn.Visible = True
            Else
                pbVolUp.Visible = False
                pbVolDn.Visible = False
            End If

            tbWAVol.Value = Winamp.Volume
        End If

        If curLen > 0 Then pProgress.Width = (curPos / curLen) * tbWAVol.Width
    End Sub
End Class
