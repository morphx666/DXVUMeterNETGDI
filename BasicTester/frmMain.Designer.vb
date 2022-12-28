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
        Me.DxvuMeterNETGDI1 = New NDXVUMeterNET.DXVUMeterNETGDI
        Me.SuspendLayout()
        '
        'DxvuMeterNETGDI1
        '
        Me.DxvuMeterNETGDI1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DxvuMeterNETGDI1.BitDepth = CType(16, Short)
        Me.DxvuMeterNETGDI1.Channels = CType(2, Short)
        Me.DxvuMeterNETGDI1.EnableRendering = True
        Me.DxvuMeterNETGDI1.FFTDetectDTMF = False
        Me.DxvuMeterNETGDI1.FFTHistorySize = 4
        Me.DxvuMeterNETGDI1.FFTHoldMaxPeaks = False
        Me.DxvuMeterNETGDI1.FFTHoldMinPeaks = False
        Me.DxvuMeterNETGDI1.FFTLineChannelMode = NDXVUMeterNET.DXVUMeterNETGDI.FFTLineChannelModeConstants.Normal
        Me.DxvuMeterNETGDI1.FFTPeaksDecayDelay = 10
        Me.DxvuMeterNETGDI1.FFTPeaksDecaySpeed = 20
        Me.DxvuMeterNETGDI1.FFTPlotNoiseReduction = 0
        Me.DxvuMeterNETGDI1.FFTRenderScales = NDXVUMeterNET.DXVUMeterNETGDI.FFTRenderScalesConstants.Both
        Me.DxvuMeterNETGDI1.FFTScaleFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DxvuMeterNETGDI1.FFTShowDecay = False
        Me.DxvuMeterNETGDI1.FFTShowMinMaxRange = False
        Me.DxvuMeterNETGDI1.FFTSize = NDXVUMeterNET.FFT.FFTSizeConstants.FFTs512
        Me.DxvuMeterNETGDI1.FFTSmoothing = 0
        Me.DxvuMeterNETGDI1.FFTStyle = NDXVUMeterNET.DXVUMeterNETGDI.FFTStyleConstants.Line
        Me.DxvuMeterNETGDI1.FFTWindow = NDXVUMeterNET.FFT.FFTWindowConstants.None
        Me.DxvuMeterNETGDI1.FFTXMax = 22050
        Me.DxvuMeterNETGDI1.FFTXMin = 0
        Me.DxvuMeterNETGDI1.FFTXScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTXScaleConstants.Normal
        Me.DxvuMeterNETGDI1.FFTXZoom = False
        Me.DxvuMeterNETGDI1.FFTXZoomWindowPos = 0
        Me.DxvuMeterNETGDI1.FFTYScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTYScaleConstants.dB
        Me.DxvuMeterNETGDI1.Frequency = 44100
        Me.DxvuMeterNETGDI1.GreenOff = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNETGDI1.GreenOn = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNETGDI1.LinesThickness = 1
        Me.DxvuMeterNETGDI1.Location = New System.Drawing.Point(12, 12)
        Me.DxvuMeterNETGDI1.Name = "DxvuMeterNETGDI1"
        Me.DxvuMeterNETGDI1.NumVUs = CType(32, Short)
        Me.DxvuMeterNETGDI1.Orientation = NDXVUMeterNET.DXVUMeterNETGDI.OrientationConstants.Horizontal
        Me.DxvuMeterNETGDI1.PlaybackPosition = CType(0, Long)
        Me.DxvuMeterNETGDI1.PlaybackTime = ""
        Me.DxvuMeterNETGDI1.PlaybackVolume = CType(0, Short)
        Me.DxvuMeterNETGDI1.RedOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNETGDI1.RedOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNETGDI1.Size = New System.Drawing.Size(480, 240)
        Me.DxvuMeterNETGDI1.Style = NDXVUMeterNET.DXVUMeterNETGDI.StyleConstants.DigitalVU
        Me.DxvuMeterNETGDI1.TabIndex = 0
        Me.DxvuMeterNETGDI1.YellowOff = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNETGDI1.YellowOn = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 264)
        Me.Controls.Add(Me.DxvuMeterNETGDI1)
        Me.Name = "frmMain"
        Me.Text = "Basic Tester"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DxvuMeterNETGDI1 As NDXVUMeterNET.DXVUMeterNETGDI

End Class
