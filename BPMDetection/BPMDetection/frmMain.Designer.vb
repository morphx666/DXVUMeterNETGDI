<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnDetectBPM = New System.Windows.Forms.Button
        Me.lblStatus = New System.Windows.Forms.Label
        Me.dxvuCtrl = New NDXVUMeterNET.DXVUMeterNETGDI
        Me.SuspendLayout()
        '
        'btnDetectBPM
        '
        Me.btnDetectBPM.Location = New System.Drawing.Point(12, 168)
        Me.btnDetectBPM.Name = "btnDetectBPM"
        Me.btnDetectBPM.Size = New System.Drawing.Size(75, 23)
        Me.btnDetectBPM.TabIndex = 2
        Me.btnDetectBPM.Text = "Detect BPM"
        Me.btnDetectBPM.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(93, 173)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(60, 13)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "Status: Idle"
        '
        'dxvuCtrl
        '
        Me.dxvuCtrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dxvuCtrl.BackColor = System.Drawing.SystemColors.Control
        Me.dxvuCtrl.BitDepth = CType(16, Short)
        Me.dxvuCtrl.Channels = CType(2, Short)
        Me.dxvuCtrl.EnableRendering = True
        Me.dxvuCtrl.FFTDetectDTMF = False
        Me.dxvuCtrl.FFTHoldMaxPeaks = False
        Me.dxvuCtrl.FFTHoldMinPeaks = False
        Me.dxvuCtrl.FFTPeaksDecayDelay = 10
        Me.dxvuCtrl.FFTPeaksDecaySpeed = 20
        Me.dxvuCtrl.FFTRenderScales = NDXVUMeterNET.DXVUMeterNETGDI.FFTRenderScalesConstants.Both
        Me.dxvuCtrl.FFTScaleFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dxvuCtrl.FFTShowDecay = False
        Me.dxvuCtrl.FFTShowMinMaxRange = False
        Me.dxvuCtrl.FFTSize = NDXVUMeterNET.FFT.FFTSizeConstants.FFTs512
        Me.dxvuCtrl.FFTSmoothing = 0
        Me.dxvuCtrl.FFTStyle = NDXVUMeterNET.DXVUMeterNETGDI.FFTStyleConstants.Line
        Me.dxvuCtrl.FFTWindow = NDXVUMeterNET.FFT.FFTWindowConstants.None
        Me.dxvuCtrl.FFTXMax = 22050
        Me.dxvuCtrl.FFTXMin = 0
        Me.dxvuCtrl.FFTXScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTXScaleConstants.Normal
        Me.dxvuCtrl.FFTXZoom = False
        Me.dxvuCtrl.FFTXZoomWindowPos = 0
        Me.dxvuCtrl.FFTYScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTYScaleConstants.dB
        Me.dxvuCtrl.Frequency = 44100
        Me.dxvuCtrl.GreenOff = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.GreenOn = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.LinesThickness = 1
        Me.dxvuCtrl.Location = New System.Drawing.Point(12, 12)
        Me.dxvuCtrl.Name = "dxvuCtrl"
        Me.dxvuCtrl.NumVUs = CType(42, Short)
        Me.dxvuCtrl.Orientation = NDXVUMeterNET.DXVUMeterNETGDI.OrientationConstants.Horizontal
        Me.dxvuCtrl.PlaybackPosition = CType(0, Long)
        Me.dxvuCtrl.PlaybackTime = ""
        Me.dxvuCtrl.PlaybackVolume = CType(0, Short)
        Me.dxvuCtrl.RedOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.RedOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.Size = New System.Drawing.Size(540, 150)
        Me.dxvuCtrl.Style = NDXVUMeterNET.DXVUMeterNETGDI.StyleConstants.DigitalVU
        Me.dxvuCtrl.TabIndex = 0
        Me.dxvuCtrl.YellowOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.dxvuCtrl.YellowOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 482)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnDetectBPM)
        Me.Controls.Add(Me.dxvuCtrl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "BPM Detection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dxvuCtrl As NDXVUMeterNET.DXVUMeterNETGDI
    Friend WithEvents btnDetectBPM As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label

End Class
