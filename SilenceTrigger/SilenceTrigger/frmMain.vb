Imports System.Threading
Imports System.ComponentModel
Imports NDXVUMeterNET.DXVUMeterNETGDI

Public Class frmMain
    Private selLine As DevicesCollection.Device.RecordingSourcesCollection.Line
    Private tmrDelay As Timer
    Private tmrDelayValue As Integer
    Private delaySetPoint As Integer
    Private volTreshold As Single
    Private DoTrigger As Boolean

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        DxvuMeterNET1.StopMonitoring()

        tmrDelay.Dispose()
        Do While bw.IsBusy And DxvuMeterNET1.MonitoringState <> MonitoringStateConstants.Idle
            Application.DoEvents()
        Loop
        DxvuMeterNET1.Dispose()
    End Sub

    Private Sub DxvuMeterNET1_ControlIsReady() Handles DxvuMeterNET1.ControlIsReady
        Dim i As Integer

        For Each device As DevicesCollection.Device In DxvuMeterNET1.Devices
            i = cmbDevices.Items.Add(device)
            If device.Selected Then cmbDevices.SelectedIndex = i
        Next
        cmbDevices.SelectedIndex = 0
    End Sub

    Private Sub DxvuMeterNET1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DxvuMeterNET1.DoubleClick
        If DxvuMeterNET1.Orientation = OrientationConstants.Horizontal Then
            DxvuMeterNET1.Orientation = OrientationConstants.Vertical
        Else
            DxvuMeterNET1.Orientation = OrientationConstants.Horizontal
        End If
    End Sub

    Private Sub cmbDevices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDevices.SelectedIndexChanged
        Dim i As Integer
        Dim selDevice As DevicesCollection.Device = CType(cmbDevices.SelectedItem, DevicesCollection.Device)
        DxvuMeterNET1.Devices.SelectedDevice = selDevice

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
            If q.BitDepth = 16 And q.Channels = 2 And q.Frequency = 44100 Then
                cmbCapabilities.SelectedIndex = cmbCapabilities.Items.Count - 1
            End If
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

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tmrDelay = New Timer(New TimerCallback(AddressOf CountDown), Nothing, Timeout.Infinite, 1)

        tbDelay.Value = 1200

        pTreshold.Size = New Size(tbTreshold.Width + DxvuMeterNET1.Width + 30, pTreshold.Height)
        pTreshold.SendToBack()
        tbTreshold.Value = 8
        SetTreshold()

        ' Uncoment the following line and replace it with the one you received via email
        '   with your own registration information.
        'This will remove the DEMO message in DXVUMeterNET
        'DxvuMeterNET1.LicenseControl("XXXXXX", "XXX-XXX-XXX")
    End Sub

    Private Sub tbTreshold_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTreshold.Scroll
        SetTreshold()
    End Sub

    Private Sub SetTreshold()
        volTreshold = tbTreshold.Value / tbTreshold.Maximum
        pTreshold.Location = New Point(tbTreshold.Left - 5, DxvuMeterNET1.Top + DxvuMeterNET1.Height - volTreshold * (DxvuMeterNET1.Height - 10))
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        SetupUI(False)

        ResetTimer()

        DxvuMeterNET1.StartMonitoring()
        tmrDelay.Change(0, 10)
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        SetupUI(True)

        tmrDelay.Change(Timeout.Infinite, Timeout.Infinite)
        DxvuMeterNET1.StopMonitoring()
    End Sub

    Private Sub SetupUI(ByVal State As Boolean)
        cmbDevices.Enabled = State
        cmbCapabilities.Enabled = State
        btnStart.Enabled = State
        btnStop.Enabled = Not State
        txtTrigger.Enabled = State
        btnBrowse.Enabled = State
    End Sub

    Private Sub tbDelay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbDelay.ValueChanged
        Dim s As Single = tbDelay.Value
        Dim u As String = ""

        If s < 1000 Then
            u = "ms"
        Else
            u = "s"
            s = s / 1000
        End If
        s = Math.Round(s, 2)

        lblDelay.Text = s.ToString + " " + u

        tmrDelayValue = tbDelay.Value
        delaySetPoint = tbDelay.Value
    End Sub

    Private Sub CountDown(ByVal state As Object)
        If tmrDelayValue > 0 Then
            tmrDelayValue -= 10
            If tmrDelayValue < 0 Then tmrDelayValue = 0
            If Not bw.IsBusy Then bw.RunWorkerAsync()
        Else
            If DoTrigger Then
                DoTrigger = False

                gbTrigger.BackColor = Color.Red

                If My.Computer.FileSystem.FileExists(txtTrigger.Text) Then
                    Dim startInfo As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo(txtTrigger.Text)
                    Dim pStart As New System.Diagnostics.Process

                    pStart.StartInfo = startInfo
                    pStart.Start()
                End If
            End If
        End If
    End Sub

    Private Sub ResetTimer()
        tmrDelayValue = delaySetPoint
        DoTrigger = True
        gbTrigger.BackColor = Color.FromKnownColor(KnownColor.Control)
    End Sub

    Private WithEvents bw As BackgroundWorker = New System.ComponentModel.BackgroundWorker
    Private Delegate Sub SetProgressDel()
    Private Sub bwEInit(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted
        Try
            Me.Invoke(New SetProgressDel(AddressOf SetProgressProc))
        Catch
            bw.RunWorkerAsync()
        End Try
    End Sub
    Private Sub SetProgressProc()
        pbDelay.Value = tmrDelayValue / delaySetPoint * 100
    End Sub

    Private Sub DxvuMeterNET1_PeakValues(AudioBuffer() As Byte, NormalizedAudioBuffer() As Integer, MaxPeak As Peak) Handles DxvuMeterNET1.PeakValues
        If (MaxPeak.Left + MaxPeak.Right) / 2 > volTreshold Then ResetTimer()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With ofdTrigger
            If .ShowDialog = Windows.Forms.DialogResult.OK Then txtTrigger.Text = .FileName
        End With
    End Sub

    Private Sub cmbCapabilities_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCapabilities.SelectedIndexChanged
        Dim q As DevicesCollection.Device.QualitiesCollection.Quality = CType(cmbCapabilities.SelectedItem, DevicesCollection.Device.QualitiesCollection.Quality)
        With DxvuMeterNET1
            .Frequency = q.Frequency
            .BitDepth = q.BitDepth
            .Channels = q.Channels
        End With
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
