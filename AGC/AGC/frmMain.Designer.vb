<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.dxvunCtrl = New NDXVUMeterNET.DXVUMeterNETGDI
        Me.pTreshold = New System.Windows.Forms.Panel
        Me.tbTreshold = New System.Windows.Forms.TrackBar
        Me.gbMonDev = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbCapabilities = New System.Windows.Forms.ComboBox
        Me.tbVolume = New System.Windows.Forms.TrackBar
        Me.cmbSources = New System.Windows.Forms.ComboBox
        Me.cmbDevices = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.pbVolDn = New System.Windows.Forms.PictureBox
        Me.pbVolUp = New System.Windows.Forms.PictureBox
        Me.pProgress = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.tbWAVol = New System.Windows.Forms.TrackBar
        Me.lblWAPlaying = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnStatus = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbSens = New System.Windows.Forms.TrackBar
        Me.btnStart = New System.Windows.Forms.Button
        CType(Me.tbTreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMonDev.SuspendLayout()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbVolDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbVolUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbWAVol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.tbSens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dxvunCtrl
        '
        Me.dxvunCtrl.BitDepth = CType(16, Short)
        Me.dxvunCtrl.Channels = CType(2, Short)
        Me.dxvunCtrl.EnableRendering = True
        Me.dxvunCtrl.FFTDetectDTMF = False
        Me.dxvunCtrl.FFTHoldMaxPeaks = False
        Me.dxvunCtrl.FFTHoldMinPeaks = False
        Me.dxvunCtrl.FFTXMax = 22050
        Me.dxvunCtrl.FFTXMin = 0
        Me.dxvunCtrl.FFTScaleFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dxvunCtrl.FFTShowDecay = False
        Me.dxvunCtrl.FFTShowMinMaxRange = False
        Me.dxvunCtrl.FFTSize = NDXVUMeterNET.FFT.FFTSizeConstants.FFTs512
        Me.dxvunCtrl.FFTSmoothing = 0
        Me.dxvunCtrl.FFTStyle = NDXVUMeterNET.DXVUMeterNETGDI.FFTStyleConstants.Line
        Me.dxvunCtrl.FFTWindow = NDXVUMeterNET.FFT.FFTWindowConstants.None
        Me.dxvunCtrl.FFTXScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTXScaleConstants.Normal
        Me.dxvunCtrl.FFTYScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTYScaleConstants.dB
        Me.dxvunCtrl.FFTXZoom = False
        Me.dxvunCtrl.FFTXZoomWindowPos = 0
        Me.dxvunCtrl.Frequency = 44100
        Me.dxvunCtrl.GreenOff = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvunCtrl.GreenOn = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvunCtrl.LinesThickness = 1
        Me.dxvunCtrl.Location = New System.Drawing.Point(57, 20)
        Me.dxvunCtrl.Name = "dxvunCtrl"
        Me.dxvunCtrl.NumVUs = CType(48, Short)
        Me.dxvunCtrl.Orientation = NDXVUMeterNET.DXVUMeterNETGDI.OrientationConstants.Vertical
        Me.dxvunCtrl.PlaybackPosition = CType(0, Long)
        Me.dxvunCtrl.PlaybackTime = ""
        Me.dxvunCtrl.PlaybackVolume = CType(0, Short)
        Me.dxvunCtrl.RedOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvunCtrl.RedOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvunCtrl.Size = New System.Drawing.Size(79, 378)
        Me.dxvunCtrl.Style = NDXVUMeterNET.DXVUMeterNETGDI.StyleConstants.DigitalVU
        Me.dxvunCtrl.TabIndex = 0
        Me.dxvunCtrl.YellowOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvunCtrl.YellowOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'pTreshold
        '
        Me.pTreshold.BackColor = System.Drawing.Color.Red
        Me.pTreshold.Location = New System.Drawing.Point(93, 281)
        Me.pTreshold.Name = "pTreshold"
        Me.pTreshold.Size = New System.Drawing.Size(140, 6)
        Me.pTreshold.TabIndex = 5
        '
        'tbTreshold
        '
        Me.tbTreshold.AutoSize = False
        Me.tbTreshold.LargeChange = 2
        Me.tbTreshold.Location = New System.Drawing.Point(12, 12)
        Me.tbTreshold.Maximum = 47
        Me.tbTreshold.Minimum = 1
        Me.tbTreshold.Name = "tbTreshold"
        Me.tbTreshold.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbTreshold.Size = New System.Drawing.Size(33, 395)
        Me.tbTreshold.TabIndex = 4
        Me.tbTreshold.Value = 1
        '
        'gbMonDev
        '
        Me.gbMonDev.Controls.Add(Me.Label3)
        Me.gbMonDev.Controls.Add(Me.cmbCapabilities)
        Me.gbMonDev.Controls.Add(Me.tbVolume)
        Me.gbMonDev.Controls.Add(Me.cmbSources)
        Me.gbMonDev.Controls.Add(Me.cmbDevices)
        Me.gbMonDev.Controls.Add(Me.Label2)
        Me.gbMonDev.Controls.Add(Me.Label1)
        Me.gbMonDev.Location = New System.Drawing.Point(158, 20)
        Me.gbMonDev.Name = "gbMonDev"
        Me.gbMonDev.Size = New System.Drawing.Size(387, 135)
        Me.gbMonDev.TabIndex = 6
        Me.gbMonDev.TabStop = False
        Me.gbMonDev.Text = "Monitoring Device"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 29)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Monitor Volume"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCapabilities
        '
        Me.cmbCapabilities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCapabilities.FormattingEnabled = True
        Me.cmbCapabilities.Location = New System.Drawing.Point(247, 27)
        Me.cmbCapabilities.Name = "cmbCapabilities"
        Me.cmbCapabilities.Size = New System.Drawing.Size(121, 21)
        Me.cmbCapabilities.TabIndex = 5
        '
        'tbVolume
        '
        Me.tbVolume.Location = New System.Drawing.Point(63, 85)
        Me.tbVolume.Name = "tbVolume"
        Me.tbVolume.Size = New System.Drawing.Size(318, 45)
        Me.tbVolume.TabIndex = 4
        '
        'cmbSources
        '
        Me.cmbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSources.FormattingEnabled = True
        Me.cmbSources.Location = New System.Drawing.Point(63, 58)
        Me.cmbSources.Name = "cmbSources"
        Me.cmbSources.Size = New System.Drawing.Size(305, 21)
        Me.cmbSources.TabIndex = 3
        '
        'cmbDevices
        '
        Me.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDevices.FormattingEnabled = True
        Me.cmbDevices.Location = New System.Drawing.Point(63, 27)
        Me.cmbDevices.Name = "cmbDevices"
        Me.cmbDevices.Size = New System.Drawing.Size(178, 21)
        Me.cmbDevices.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Source"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Device"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pbVolDn)
        Me.GroupBox1.Controls.Add(Me.pbVolUp)
        Me.GroupBox1.Controls.Add(Me.pProgress)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.tbWAVol)
        Me.GroupBox1.Controls.Add(Me.lblWAPlaying)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(158, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(387, 101)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Winamp"
        '
        'pbVolDn
        '
        Me.pbVolDn.BackgroundImage = Global.AGC.My.Resources.Resources.adn_b
        Me.pbVolDn.Location = New System.Drawing.Point(372, 67)
        Me.pbVolDn.Name = "pbVolDn"
        Me.pbVolDn.Size = New System.Drawing.Size(8, 8)
        Me.pbVolDn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbVolDn.TabIndex = 11
        Me.pbVolDn.TabStop = False
        Me.pbVolDn.Visible = False
        '
        'pbVolUp
        '
        Me.pbVolUp.BackgroundImage = Global.AGC.My.Resources.Resources.aup_b
        Me.pbVolUp.Location = New System.Drawing.Point(372, 53)
        Me.pbVolUp.Name = "pbVolUp"
        Me.pbVolUp.Size = New System.Drawing.Size(8, 8)
        Me.pbVolUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbVolUp.TabIndex = 10
        Me.pbVolUp.TabStop = False
        Me.pbVolUp.Visible = False
        '
        'pProgress
        '
        Me.pProgress.BackColor = System.Drawing.Color.GreenYellow
        Me.pProgress.Location = New System.Drawing.Point(73, 30)
        Me.pProgress.Name = "pProgress"
        Me.pProgress.Size = New System.Drawing.Size(97, 15)
        Me.pProgress.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(7, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 29)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Output Volume"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbWAVol
        '
        Me.tbWAVol.Enabled = False
        Me.tbWAVol.LargeChange = 10
        Me.tbWAVol.Location = New System.Drawing.Point(63, 53)
        Me.tbWAVol.Maximum = 255
        Me.tbWAVol.Name = "tbWAVol"
        Me.tbWAVol.Size = New System.Drawing.Size(305, 45)
        Me.tbWAVol.TabIndex = 7
        Me.tbWAVol.TickFrequency = 10
        '
        'lblWAPlaying
        '
        Me.lblWAPlaying.AutoSize = True
        Me.lblWAPlaying.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWAPlaying.Location = New System.Drawing.Point(60, 16)
        Me.lblWAPlaying.Name = "lblWAPlaying"
        Me.lblWAPlaying.Size = New System.Drawing.Size(140, 13)
        Me.lblWAPlaying.TabIndex = 1
        Me.lblWAPlaying.Text = "Winamp not detected..."
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(7, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Playing"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStop.Location = New System.Drawing.Point(247, 346)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(81, 51)
        Me.btnStop.TabIndex = 9
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(464, 346)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(81, 51)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnStatus)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.tbSens)
        Me.GroupBox2.Location = New System.Drawing.Point(158, 268)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(387, 72)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Automatic Gain Control"
        '
        'btnStatus
        '
        Me.btnStatus.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.btnStatus.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.btnStatus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStatus.Location = New System.Drawing.Point(316, 17)
        Me.btnStatus.Name = "btnStatus"
        Me.btnStatus.Size = New System.Drawing.Size(64, 46)
        Me.btnStatus.TabIndex = 14
        Me.btnStatus.Text = "Status"
        Me.btnStatus.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(7, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 29)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Sensitivity"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbSens
        '
        Me.tbSens.LargeChange = 10
        Me.tbSens.Location = New System.Drawing.Point(63, 19)
        Me.tbSens.Maximum = 100
        Me.tbSens.Minimum = 1
        Me.tbSens.Name = "tbSens"
        Me.tbSens.Size = New System.Drawing.Size(247, 45)
        Me.tbSens.TabIndex = 12
        Me.tbSens.TickFrequency = 10
        Me.tbSens.Value = 10
        '
        'btnStart
        '
        Me.btnStart.Image = Global.AGC.My.Resources.Resources.wa_off
        Me.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStart.Location = New System.Drawing.Point(158, 346)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(81, 51)
        Me.btnStart.TabIndex = 7
        Me.btnStart.Text = "Start"
        Me.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 411)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.gbMonDev)
        Me.Controls.Add(Me.tbTreshold)
        Me.Controls.Add(Me.dxvunCtrl)
        Me.Controls.Add(Me.pTreshold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DXVUMeterNET AGC"
        CType(Me.tbTreshold, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMonDev.ResumeLayout(False)
        Me.gbMonDev.PerformLayout()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pbVolDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbVolUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbWAVol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.tbSens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dxvunCtrl As NDXVUMeterNET.DXVUMeterNETGDI
    Friend WithEvents pTreshold As System.Windows.Forms.Panel
    Friend WithEvents tbTreshold As System.Windows.Forms.TrackBar
    Friend WithEvents gbMonDev As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCapabilities As System.Windows.Forms.ComboBox
    Friend WithEvents tbVolume As System.Windows.Forms.TrackBar
    Friend WithEvents cmbSources As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDevices As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblWAPlaying As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbWAVol As System.Windows.Forms.TrackBar
    Friend WithEvents pProgress As System.Windows.Forms.Panel
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbSens As System.Windows.Forms.TrackBar
    Friend WithEvents btnStatus As System.Windows.Forms.Button
    Friend WithEvents pbVolUp As System.Windows.Forms.PictureBox
    Friend WithEvents pbVolDn As System.Windows.Forms.PictureBox

End Class
