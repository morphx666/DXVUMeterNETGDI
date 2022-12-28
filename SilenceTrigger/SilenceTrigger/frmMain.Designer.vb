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
        Me.gbMonDev = New System.Windows.Forms.GroupBox()
        Me.cmbCapabilities = New System.Windows.Forms.ComboBox()
        Me.tbVolume = New System.Windows.Forms.TrackBar()
        Me.cmbSources = New System.Windows.Forms.ComboBox()
        Me.cmbDevices = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DxvuMeterNET1 = New NDXVUMeterNET.DXVUMeterNETGDI()
        Me.tbTreshold = New System.Windows.Forms.TrackBar()
        Me.pTreshold = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pbDelay = New System.Windows.Forms.ProgressBar()
        Me.lblDelay = New System.Windows.Forms.Label()
        Me.tbDelay = New System.Windows.Forms.TrackBar()
        Me.gbTrigger = New System.Windows.Forms.GroupBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtTrigger = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.ofdTrigger = New System.Windows.Forms.OpenFileDialog()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.gbMonDev.SuspendLayout()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.tbDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTrigger.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbMonDev
        '
        Me.gbMonDev.Controls.Add(Me.cmbCapabilities)
        Me.gbMonDev.Controls.Add(Me.tbVolume)
        Me.gbMonDev.Controls.Add(Me.cmbSources)
        Me.gbMonDev.Controls.Add(Me.cmbDevices)
        Me.gbMonDev.Controls.Add(Me.Label2)
        Me.gbMonDev.Controls.Add(Me.Label1)
        Me.gbMonDev.Location = New System.Drawing.Point(182, 12)
        Me.gbMonDev.Name = "gbMonDev"
        Me.gbMonDev.Size = New System.Drawing.Size(368, 135)
        Me.gbMonDev.TabIndex = 0
        Me.gbMonDev.TabStop = False
        Me.gbMonDev.Text = "Monitoring Device"
        '
        'cmbCapabilities
        '
        Me.cmbCapabilities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCapabilities.FormattingEnabled = True
        Me.cmbCapabilities.Location = New System.Drawing.Point(223, 27)
        Me.cmbCapabilities.Name = "cmbCapabilities"
        Me.cmbCapabilities.Size = New System.Drawing.Size(121, 21)
        Me.cmbCapabilities.TabIndex = 5
        '
        'tbVolume
        '
        Me.tbVolume.Location = New System.Drawing.Point(12, 85)
        Me.tbVolume.Name = "tbVolume"
        Me.tbVolume.Size = New System.Drawing.Size(350, 45)
        Me.tbVolume.TabIndex = 4
        '
        'cmbSources
        '
        Me.cmbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSources.FormattingEnabled = True
        Me.cmbSources.Location = New System.Drawing.Point(63, 58)
        Me.cmbSources.Name = "cmbSources"
        Me.cmbSources.Size = New System.Drawing.Size(281, 21)
        Me.cmbSources.TabIndex = 3
        '
        'cmbDevices
        '
        Me.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDevices.FormattingEnabled = True
        Me.cmbDevices.Location = New System.Drawing.Point(63, 27)
        Me.cmbDevices.Name = "cmbDevices"
        Me.cmbDevices.Size = New System.Drawing.Size(154, 21)
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
        'DxvuMeterNET1
        '
        Me.DxvuMeterNET1.BitDepth = CType(16, Short)
        Me.DxvuMeterNET1.Channels = CType(2, Short)
        Me.DxvuMeterNET1.EnableRendering = True
        Me.DxvuMeterNET1.FFTDetectDTMF = False
        Me.DxvuMeterNET1.FFTHistorySize = 4
        Me.DxvuMeterNET1.FFTHoldMaxPeaks = False
        Me.DxvuMeterNET1.FFTHoldMinPeaks = False
        Me.DxvuMeterNET1.FFTLineChannelMode = NDXVUMeterNET.DXVUMeterNETGDI.FFTLineChannelModeConstants.Normal
        Me.DxvuMeterNET1.FFTPeaksDecayDelay = 10
        Me.DxvuMeterNET1.FFTPeaksDecaySpeed = 20
        Me.DxvuMeterNET1.FFTPlotNoiseReduction = 0
        Me.DxvuMeterNET1.FFTRenderScales = NDXVUMeterNET.DXVUMeterNETGDI.FFTRenderScalesConstants.Both
        Me.DxvuMeterNET1.FFTScaleFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DxvuMeterNET1.FFTShowDecay = False
        Me.DxvuMeterNET1.FFTShowMinMaxRange = False
        Me.DxvuMeterNET1.FFTSize = NDXVUMeterNET.DXVUMeterNETGDI.FFTSizeConstants.FFTs512
        Me.DxvuMeterNET1.FFTSmoothing = 0
        Me.DxvuMeterNET1.FFTStyle = NDXVUMeterNET.DXVUMeterNETGDI.FFTStyleConstants.Line
        Me.DxvuMeterNET1.FFTWindow = NDXVUMeterNET.DXVUMeterNETGDI.FFTWindowConstants.None
        Me.DxvuMeterNET1.FFTXMax = 22050
        Me.DxvuMeterNET1.FFTXMin = 0
        Me.DxvuMeterNET1.FFTXScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTXScaleConstants.Normal
        Me.DxvuMeterNET1.FFTXZoom = False
        Me.DxvuMeterNET1.FFTXZoomWindowPos = 0
        Me.DxvuMeterNET1.FFTYScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTYScaleConstants.dB
        Me.DxvuMeterNET1.Frequency = 44100
        Me.DxvuMeterNET1.GreenOff = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.GreenOn = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.LinesThickness = 1
        Me.DxvuMeterNET1.Location = New System.Drawing.Point(68, 6)
        Me.DxvuMeterNET1.Name = "DxvuMeterNET1"
        Me.DxvuMeterNET1.NumVUs = CType(48, Short)
        Me.DxvuMeterNET1.Orientation = NDXVUMeterNET.DXVUMeterNETGDI.OrientationConstants.Vertical
        Me.DxvuMeterNET1.PlaybackPosition = CType(0, Long)
        Me.DxvuMeterNET1.PlaybackTime = ""
        Me.DxvuMeterNET1.PlaybackVolume = CType(0, Short)
        Me.DxvuMeterNET1.RedOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.RedOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.Size = New System.Drawing.Size(86, 385)
        Me.DxvuMeterNET1.Style = NDXVUMeterNET.DXVUMeterNETGDI.StyleConstants.DigitalVU
        Me.DxvuMeterNET1.TabIndex = 1
        Me.DxvuMeterNET1.YellowOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.YellowOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'tbTreshold
        '
        Me.tbTreshold.AutoSize = False
        Me.tbTreshold.LargeChange = 2
        Me.tbTreshold.Location = New System.Drawing.Point(20, 4)
        Me.tbTreshold.Maximum = 47
        Me.tbTreshold.Minimum = 1
        Me.tbTreshold.Name = "tbTreshold"
        Me.tbTreshold.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbTreshold.Size = New System.Drawing.Size(33, 395)
        Me.tbTreshold.TabIndex = 2
        Me.tbTreshold.Value = 1
        '
        'pTreshold
        '
        Me.pTreshold.BackColor = System.Drawing.Color.Red
        Me.pTreshold.Location = New System.Drawing.Point(144, 289)
        Me.pTreshold.Name = "pTreshold"
        Me.pTreshold.Size = New System.Drawing.Size(100, 6)
        Me.pTreshold.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pbDelay)
        Me.GroupBox1.Controls.Add(Me.lblDelay)
        Me.GroupBox1.Controls.Add(Me.tbDelay)
        Me.GroupBox1.Location = New System.Drawing.Point(182, 153)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(368, 84)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Delay"
        '
        'pbDelay
        '
        Me.pbDelay.Location = New System.Drawing.Point(12, 60)
        Me.pbDelay.Name = "pbDelay"
        Me.pbDelay.Size = New System.Drawing.Size(300, 14)
        Me.pbDelay.TabIndex = 7
        Me.pbDelay.Value = 100
        '
        'lblDelay
        '
        Me.lblDelay.AutoSize = True
        Me.lblDelay.Location = New System.Drawing.Point(317, 24)
        Me.lblDelay.Name = "lblDelay"
        Me.lblDelay.Size = New System.Drawing.Size(39, 13)
        Me.lblDelay.TabIndex = 6
        Me.lblDelay.Text = "99.9 m"
        Me.lblDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbDelay
        '
        Me.tbDelay.AutoSize = False
        Me.tbDelay.LargeChange = 100
        Me.tbDelay.Location = New System.Drawing.Point(12, 20)
        Me.tbDelay.Maximum = 10000
        Me.tbDelay.Minimum = 10
        Me.tbDelay.Name = "tbDelay"
        Me.tbDelay.Size = New System.Drawing.Size(302, 32)
        Me.tbDelay.TabIndex = 5
        Me.tbDelay.TickFrequency = 1000
        Me.tbDelay.Value = 10
        '
        'gbTrigger
        '
        Me.gbTrigger.Controls.Add(Me.btnBrowse)
        Me.gbTrigger.Controls.Add(Me.txtTrigger)
        Me.gbTrigger.Location = New System.Drawing.Point(182, 243)
        Me.gbTrigger.Name = "gbTrigger"
        Me.gbTrigger.Size = New System.Drawing.Size(368, 71)
        Me.gbTrigger.TabIndex = 5
        Me.gbTrigger.TabStop = False
        Me.gbTrigger.Text = "Trigger"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(310, 29)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(34, 20)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtTrigger
        '
        Me.txtTrigger.BackColor = System.Drawing.SystemColors.Window
        Me.txtTrigger.Location = New System.Drawing.Point(15, 29)
        Me.txtTrigger.Name = "txtTrigger"
        Me.txtTrigger.ReadOnly = True
        Me.txtTrigger.Size = New System.Drawing.Size(289, 20)
        Me.txtTrigger.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(182, 360)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(72, 30)
        Me.btnStart.TabIndex = 6
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(260, 360)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(72, 30)
        Me.btnStop.TabIndex = 7
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'ofdTrigger
        '
        Me.ofdTrigger.DefaultExt = "exe"
        Me.ofdTrigger.Filter = "Application (*.exe)|*.exe"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(478, 360)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 30)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 407)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.gbTrigger)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pTreshold)
        Me.Controls.Add(Me.tbTreshold)
        Me.Controls.Add(Me.DxvuMeterNET1)
        Me.Controls.Add(Me.gbMonDev)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Silence Trigger"
        Me.gbMonDev.ResumeLayout(False)
        Me.gbMonDev.PerformLayout()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTreshold, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.tbDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTrigger.ResumeLayout(False)
        Me.gbTrigger.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbMonDev As System.Windows.Forms.GroupBox
    Friend WithEvents DxvuMeterNET1 As NDXVUMeterNET.DXVUMeterNETGDI
    Friend WithEvents tbTreshold As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbVolume As System.Windows.Forms.TrackBar
    Friend WithEvents cmbSources As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDevices As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbCapabilities As System.Windows.Forms.ComboBox
    Friend WithEvents pTreshold As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDelay As System.Windows.Forms.Label
    Friend WithEvents tbDelay As System.Windows.Forms.TrackBar
    Friend WithEvents pbDelay As System.Windows.Forms.ProgressBar
    Friend WithEvents gbTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtTrigger As System.Windows.Forms.TextBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents ofdTrigger As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
