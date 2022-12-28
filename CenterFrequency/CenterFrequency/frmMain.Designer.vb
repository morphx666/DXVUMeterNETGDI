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
        Me.dxvuCtrl = New NDXVUMeterNET.DXVUMeterNETGDI
        Me.gbMonDev = New System.Windows.Forms.GroupBox
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbCapabilities = New System.Windows.Forms.ComboBox
        Me.tbVolume = New System.Windows.Forms.TrackBar
        Me.cmbSources = New System.Windows.Forms.ComboBox
        Me.cmbDevices = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pMarker = New System.Windows.Forms.Panel
        Me.lblFreq = New System.Windows.Forms.Label
        Me.lblInfo = New System.Windows.Forms.Label
        Me.chkLogXScale = New System.Windows.Forms.CheckBox
        Me.cmbFFTYScale = New System.Windows.Forms.ComboBox
        Me.gbMonDev.SuspendLayout()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dxvuCtrl
        '
        Me.dxvuCtrl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dxvuCtrl.BitDepth = CType(16, Short)
        Me.dxvuCtrl.Channels = CType(2, Short)
        Me.dxvuCtrl.EnableRendering = True
        Me.dxvuCtrl.FFTDetectDTMF = False
        Me.dxvuCtrl.FFTHoldMaxPeaks = False
        Me.dxvuCtrl.FFTHoldMinPeaks = False
        Me.dxvuCtrl.FFTXMax = 22050
        Me.dxvuCtrl.FFTXMin = 0
        Me.dxvuCtrl.FFTScaleFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dxvuCtrl.FFTShowDecay = False
        Me.dxvuCtrl.FFTShowMinMaxRange = False
        Me.dxvuCtrl.FFTSize = NDXVUMeterNET.FFT.FFTSizeConstants.FFTs512
        Me.dxvuCtrl.FFTSmoothing = 0
        Me.dxvuCtrl.FFTStyle = NDXVUMeterNET.DXVUMeterNETGDI.FFTStyleConstants.Line
        Me.dxvuCtrl.FFTWindow = NDXVUMeterNET.FFT.FFTWindowConstants.None
        Me.dxvuCtrl.FFTXScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTXScaleConstants.Normal
        Me.dxvuCtrl.FFTYScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTYScaleConstants.dB
        Me.dxvuCtrl.FFTXZoom = True
        Me.dxvuCtrl.FFTXZoomWindowPos = 0
        Me.dxvuCtrl.Frequency = 44100
        Me.dxvuCtrl.GreenOff = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.GreenOn = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.LinesThickness = 1
        Me.dxvuCtrl.Location = New System.Drawing.Point(12, 171)
        Me.dxvuCtrl.Name = "dxvuCtrl"
        Me.dxvuCtrl.NumVUs = CType(32, Short)
        Me.dxvuCtrl.Orientation = NDXVUMeterNET.DXVUMeterNETGDI.OrientationConstants.Horizontal
        Me.dxvuCtrl.PlaybackPosition = CType(0, Long)
        Me.dxvuCtrl.PlaybackTime = ""
        Me.dxvuCtrl.PlaybackVolume = CType(0, Short)
        Me.dxvuCtrl.RedOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.RedOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.Size = New System.Drawing.Size(584, 178)
        Me.dxvuCtrl.Style = NDXVUMeterNET.DXVUMeterNETGDI.StyleConstants.DigitalVU
        Me.dxvuCtrl.TabIndex = 0
        Me.dxvuCtrl.YellowOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.YellowOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'gbMonDev
        '
        Me.gbMonDev.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbMonDev.Controls.Add(Me.btnStop)
        Me.gbMonDev.Controls.Add(Me.btnStart)
        Me.gbMonDev.Controls.Add(Me.Label3)
        Me.gbMonDev.Controls.Add(Me.cmbCapabilities)
        Me.gbMonDev.Controls.Add(Me.tbVolume)
        Me.gbMonDev.Controls.Add(Me.cmbSources)
        Me.gbMonDev.Controls.Add(Me.cmbDevices)
        Me.gbMonDev.Controls.Add(Me.Label2)
        Me.gbMonDev.Controls.Add(Me.Label1)
        Me.gbMonDev.Location = New System.Drawing.Point(12, 12)
        Me.gbMonDev.Name = "gbMonDev"
        Me.gbMonDev.Size = New System.Drawing.Size(584, 135)
        Me.gbMonDev.TabIndex = 7
        Me.gbMonDev.TabStop = False
        Me.gbMonDev.Text = "Monitoring Device"
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStop.Location = New System.Drawing.Point(485, 96)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(93, 33)
        Me.btnStop.TabIndex = 8
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStart.Location = New System.Drawing.Point(485, 57)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(93, 33)
        Me.btnStart.TabIndex = 7
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 87)
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
        Me.tbVolume.Location = New System.Drawing.Point(63, 79)
        Me.tbVolume.Name = "tbVolume"
        Me.tbVolume.Size = New System.Drawing.Size(305, 45)
        Me.tbVolume.TabIndex = 4
        '
        'cmbSources
        '
        Me.cmbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSources.FormattingEnabled = True
        Me.cmbSources.Location = New System.Drawing.Point(63, 55)
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
        Me.Label2.Location = New System.Drawing.Point(12, 59)
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
        'pMarker
        '
        Me.pMarker.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pMarker.BackColor = System.Drawing.Color.Red
        Me.pMarker.Location = New System.Drawing.Point(296, 160)
        Me.pMarker.Name = "pMarker"
        Me.pMarker.Size = New System.Drawing.Size(3, 197)
        Me.pMarker.TabIndex = 8
        Me.pMarker.Visible = False
        '
        'lblFreq
        '
        Me.lblFreq.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblFreq.AutoSize = True
        Me.lblFreq.BackColor = System.Drawing.SystemColors.Info
        Me.lblFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFreq.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFreq.ForeColor = System.Drawing.SystemColors.InfoText
        Me.lblFreq.Location = New System.Drawing.Point(259, 360)
        Me.lblFreq.Name = "lblFreq"
        Me.lblFreq.Size = New System.Drawing.Size(71, 14)
        Me.lblFreq.TabIndex = 9
        Me.lblFreq.Text = "1.1 KHz @ 98%"
        Me.lblFreq.Visible = False
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(9, 376)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(141, 26)
        Me.lblInfo.TabIndex = 10
        Me.lblInfo.Text = "Center Frequency: 0 Hz" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Level: 0%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'chkLogXScale
        '
        Me.chkLogXScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkLogXScale.AutoSize = True
        Me.chkLogXScale.Checked = True
        Me.chkLogXScale.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLogXScale.Location = New System.Drawing.Point(12, 355)
        Me.chkLogXScale.Name = "chkLogXScale"
        Me.chkLogXScale.Size = New System.Drawing.Size(110, 17)
        Me.chkLogXScale.TabIndex = 11
        Me.chkLogXScale.Text = "Logarithmic Scale"
        Me.chkLogXScale.UseVisualStyleBackColor = True
        '
        'cmbFFTYScale
        '
        Me.cmbFFTYScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFFTYScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFFTYScale.Location = New System.Drawing.Point(122, 353)
        Me.cmbFFTYScale.Name = "cmbFFTYScale"
        Me.cmbFFTYScale.Size = New System.Drawing.Size(131, 20)
        Me.cmbFFTYScale.TabIndex = 52
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 411)
        Me.Controls.Add(Me.cmbFFTYScale)
        Me.Controls.Add(Me.chkLogXScale)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.lblFreq)
        Me.Controls.Add(Me.pMarker)
        Me.Controls.Add(Me.gbMonDev)
        Me.Controls.Add(Me.dxvuCtrl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Center Frequency"
        Me.gbMonDev.ResumeLayout(False)
        Me.gbMonDev.PerformLayout()
        CType(Me.tbVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dxvuCtrl As NDXVUMeterNET.DXVUMeterNETGDI
    Friend WithEvents gbMonDev As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCapabilities As System.Windows.Forms.ComboBox
    Friend WithEvents tbVolume As System.Windows.Forms.TrackBar
    Friend WithEvents cmbSources As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDevices As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents pMarker As System.Windows.Forms.Panel
    Friend WithEvents lblFreq As System.Windows.Forms.Label
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents chkLogXScale As System.Windows.Forms.CheckBox
    Friend WithEvents cmbFFTYScale As System.Windows.Forms.ComboBox

End Class
