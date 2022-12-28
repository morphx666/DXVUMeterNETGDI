Imports System.ComponentModel
Imports NDXVUMeterNET

Public Class frmFFTOptions
    Inherits Form

    Private isAltDown As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbFFTStyle As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbFFTWindow As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents chkFFTZoom As CheckBox
    Friend WithEvents chkFFTLogScale As CheckBox
    Friend WithEvents lblFFTWinPos As Label
    Friend WithEvents lblFFTMax As Label
    Friend WithEvents lblFFTMin As Label
    Friend WithEvents cmbFFTSize As ComboBox
    Friend WithEvents cmbFFTYScale As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents gbColors As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents chkShowMinMaxRange As CheckBox
    Friend WithEvents chkHoldMinPeaks As CheckBox
    Friend WithEvents chkHoldMaxPeaks As CheckBox
    Friend WithEvents btnYellowOn As Button
    Friend WithEvents btnYellowOff As Button
    Friend WithEvents btnGreenOn As Button
    Friend WithEvents btnGreenOff As Button
    Friend WithEvents chkShowDecay As CheckBox
    Friend WithEvents btnRedOn As Button
    Friend WithEvents btnRedOff As Button
    Friend WithEvents tbFFTSmoothing As TrackBar
    Friend WithEvents tbFFTZoomWinPos As TrackBar
    Friend WithEvents tbFFTMax As TrackBar
    Friend WithEvents tbFFTMin As TrackBar
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtDTMFTones As TextBox
    Friend WithEvents btnClear As Button
    Friend WithEvents chkDTMFDetect As CheckBox
    Friend WithEvents tbLinesThickness As TrackBar
    Friend WithEvents lblLinesThickness As Label
    Friend WithEvents lblWinPosVal As Label
    Friend WithEvents lblMaxFreqVal As Label
    Friend WithEvents lblMinFreqVal As Label
    Friend WithEvents lblLedBarsLeftMax As Label
    Friend WithEvents lblLedBarsRightMax As Label
    Friend WithEvents tbDecayDelay As TrackBar
    Friend WithEvents lblDelay As Label
    Friend WithEvents tbDecaySpeed As TrackBar
    Friend WithEvents Label9 As Label
    Friend WithEvents btnColors2 As Button
    Friend WithEvents btnColors1 As Button
    Friend WithEvents btnColors3 As Button
    Friend WithEvents cmbFFTLineStyleChannelMode As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents tbFFTHistorySize As TrackBar
    Friend WithEvents lblFFTAvg As Label
    Friend WithEvents tbOverlap As TrackBar
    Friend WithEvents lblOverlap As Label
    Friend WithEvents tbPlotNoiseReduction As TrackBar
    Friend WithEvents Label13 As Label
    Friend WithEvents lblSmoothingVal As Label
    Friend WithEvents lblOverlapVal As Label
    Friend WithEvents lblAvgVal As Label
    Friend WithEvents tbNoiseAtt As TrackBar
    Friend WithEvents lblNoiseAtt As Label
    Friend WithEvents lblNoiseAttValue As Label
    Friend WithEvents btnFilters As Button
    Friend WithEvents tbYScale As TrackBar
    Friend WithEvents Label11 As Label
    Friend WithEvents lblYScaleValue As Label
    Friend WithEvents chkNormalized As CheckBox
    Friend WithEvents chkHighPrecisionMode As CheckBox
    Friend WithEvents ttInfo As ToolTip
    Friend WithEvents lblFFTSmoothing As Label
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New Container()
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(frmFFTOptions))
        Me.GroupBox1 = New GroupBox()
        Me.chkHighPrecisionMode = New CheckBox()
        Me.chkNormalized = New CheckBox()
        Me.btnFilters = New Button()
        Me.cmbFFTLineStyleChannelMode = New ComboBox()
        Me.Label10 = New Label()
        Me.cmbFFTStyle = New ComboBox()
        Me.Label6 = New Label()
        Me.cmbFFTWindow = New ComboBox()
        Me.Label5 = New Label()
        Me.cmbFFTSize = New ComboBox()
        Me.Label4 = New Label()
        Me.GroupBox2 = New GroupBox()
        Me.tbYScale = New TrackBar()
        Me.Label11 = New Label()
        Me.lblYScaleValue = New Label()
        Me.tbNoiseAtt = New TrackBar()
        Me.lblNoiseAtt = New Label()
        Me.lblNoiseAttValue = New Label()
        Me.tbPlotNoiseReduction = New TrackBar()
        Me.Label13 = New Label()
        Me.lblFFTAvg = New Label()
        Me.tbOverlap = New TrackBar()
        Me.lblOverlap = New Label()
        Me.tbFFTHistorySize = New TrackBar()
        Me.lblOverlapVal = New Label()
        Me.lblAvgVal = New Label()
        Me.lblSmoothingVal = New Label()
        Me.lblWinPosVal = New Label()
        Me.lblMaxFreqVal = New Label()
        Me.lblMinFreqVal = New Label()
        Me.tbLinesThickness = New TrackBar()
        Me.lblLinesThickness = New Label()
        Me.tbFFTSmoothing = New TrackBar()
        Me.tbFFTZoomWinPos = New TrackBar()
        Me.tbFFTMax = New TrackBar()
        Me.tbFFTMin = New TrackBar()
        Me.chkShowDecay = New CheckBox()
        Me.lblFFTSmoothing = New Label()
        Me.cmbFFTYScale = New ComboBox()
        Me.Label1 = New Label()
        Me.lblFFTWinPos = New Label()
        Me.lblFFTMax = New Label()
        Me.lblFFTMin = New Label()
        Me.chkFFTZoom = New CheckBox()
        Me.chkFFTLogScale = New CheckBox()
        Me.gbColors = New GroupBox()
        Me.btnColors3 = New Button()
        Me.btnColors2 = New Button()
        Me.btnColors1 = New Button()
        Me.lblLedBarsLeftMax = New Label()
        Me.lblLedBarsRightMax = New Label()
        Me.btnRedOn = New Button()
        Me.btnRedOff = New Button()
        Me.btnYellowOn = New Button()
        Me.btnYellowOff = New Button()
        Me.btnGreenOn = New Button()
        Me.btnGreenOff = New Button()
        Me.Label2 = New Label()
        Me.Label3 = New Label()
        Me.Label7 = New Label()
        Me.Label8 = New Label()
        Me.GroupBox4 = New GroupBox()
        Me.tbDecaySpeed = New TrackBar()
        Me.Label9 = New Label()
        Me.tbDecayDelay = New TrackBar()
        Me.lblDelay = New Label()
        Me.chkShowMinMaxRange = New CheckBox()
        Me.chkHoldMinPeaks = New CheckBox()
        Me.chkHoldMaxPeaks = New CheckBox()
        Me.GroupBox5 = New GroupBox()
        Me.chkDTMFDetect = New CheckBox()
        Me.btnClear = New Button()
        Me.txtDTMFTones = New TextBox()
        Me.ttInfo = New ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.tbYScale, ISupportInitialize).BeginInit()
        CType(Me.tbNoiseAtt, ISupportInitialize).BeginInit()
        CType(Me.tbPlotNoiseReduction, ISupportInitialize).BeginInit()
        CType(Me.tbOverlap, ISupportInitialize).BeginInit()
        CType(Me.tbFFTHistorySize, ISupportInitialize).BeginInit()
        CType(Me.tbLinesThickness, ISupportInitialize).BeginInit()
        CType(Me.tbFFTSmoothing, ISupportInitialize).BeginInit()
        CType(Me.tbFFTZoomWinPos, ISupportInitialize).BeginInit()
        CType(Me.tbFFTMax, ISupportInitialize).BeginInit()
        CType(Me.tbFFTMin, ISupportInitialize).BeginInit()
        Me.gbColors.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.tbDecaySpeed, ISupportInitialize).BeginInit()
        CType(Me.tbDecayDelay, ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkHighPrecisionMode)
        Me.GroupBox1.Controls.Add(Me.chkNormalized)
        Me.GroupBox1.Controls.Add(Me.btnFilters)
        Me.GroupBox1.Controls.Add(Me.cmbFFTLineStyleChannelMode)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbFFTStyle)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbFFTWindow)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbFFTSize)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New Size(184, 321)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "FFT Calculation Options"
        '
        'chkHighPrecisionMode
        '
        Me.chkHighPrecisionMode.AutoSize = True
        Me.chkHighPrecisionMode.Location = New Point(13, 238)
        Me.chkHighPrecisionMode.Name = "chkHighPrecisionMode"
        Me.chkHighPrecisionMode.Size = New Size(121, 17)
        Me.chkHighPrecisionMode.TabIndex = 47
        Me.chkHighPrecisionMode.Text = "High Precision Mode"
        Me.ttInfo.SetToolTip(Me.chkHighPrecisionMode, resources.GetString("chkHighPrecisionMode.ToolTip"))
        Me.chkHighPrecisionMode.UseVisualStyleBackColor = True
        '
        'chkNormalized
        '
        Me.chkNormalized.AutoSize = True
        Me.chkNormalized.Checked = True
        Me.chkNormalized.CheckState = CheckState.Checked
        Me.chkNormalized.Location = New Point(14, 217)
        Me.chkNormalized.Name = "chkNormalized"
        Me.chkNormalized.Size = New Size(78, 17)
        Me.chkNormalized.TabIndex = 46
        Me.chkNormalized.Text = "Normalized"
        Me.ttInfo.SetToolTip(Me.chkNormalized, "Applies a standard normalization over the transform.")
        Me.chkNormalized.UseVisualStyleBackColor = True
        '
        'btnFilters
        '
        Me.btnFilters.Location = New Point(13, 289)
        Me.btnFilters.Name = "btnFilters"
        Me.btnFilters.Size = New Size(75, 23)
        Me.btnFilters.TabIndex = 45
        Me.btnFilters.Text = "Filters..."
        Me.ttInfo.SetToolTip(Me.btnFilters, "Provides the ability to apply various filters to the transform for visualization " &
        "purposes only." & ChrW(13) & ChrW(10) & "This feature is still in alpha and thus may not work as expected" &
        ".")
        Me.btnFilters.UseVisualStyleBackColor = True
        '
        'cmbFFTLineStyleChannelMode
        '
        Me.cmbFFTLineStyleChannelMode.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbFFTLineStyleChannelMode.Location = New Point(14, 181)
        Me.cmbFFTLineStyleChannelMode.Name = "cmbFFTLineStyleChannelMode"
        Me.cmbFFTLineStyleChannelMode.Size = New Size(156, 21)
        Me.cmbFFTLineStyleChannelMode.TabIndex = 44
        Me.ttInfo.SetToolTip(Me.cmbFFTLineStyleChannelMode, "When using stereo signals, this option defines how each channel is displayed.")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New Point(14, 165)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New Size(75, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Channel Mode"
        '
        'cmbFFTStyle
        '
        Me.cmbFFTStyle.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbFFTStyle.Location = New Point(14, 134)
        Me.cmbFFTStyle.Name = "cmbFFTStyle"
        Me.cmbFFTStyle.Size = New Size(156, 21)
        Me.cmbFFTStyle.TabIndex = 42
        Me.ttInfo.SetToolTip(Me.cmbFFTStyle, "Plot style used to display the transform.")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New Point(14, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New Size(52, 13)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "FFT Style"
        '
        'cmbFFTWindow
        '
        Me.cmbFFTWindow.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbFFTWindow.Location = New Point(14, 86)
        Me.cmbFFTWindow.Name = "cmbFFTWindow"
        Me.cmbFFTWindow.Size = New Size(156, 21)
        Me.cmbFFTWindow.TabIndex = 40
        Me.ttInfo.SetToolTip(Me.cmbFFTWindow, "Window to be applied to the transform, also known as the apodization function.")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New Point(14, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New Size(66, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "FFT Window"
        '
        'cmbFFTSize
        '
        Me.cmbFFTSize.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbFFTSize.Location = New Point(14, 38)
        Me.cmbFFTSize.Name = "cmbFFTSize"
        Me.cmbFFTSize.Size = New Size(156, 21)
        Me.cmbFFTSize.TabIndex = 38
        Me.ttInfo.SetToolTip(Me.cmbFFTSize, "Fast Fourier Size (Discrete Points).")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New Point(14, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New Size(47, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "FFT Size"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tbYScale)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.lblYScaleValue)
        Me.GroupBox2.Controls.Add(Me.tbNoiseAtt)
        Me.GroupBox2.Controls.Add(Me.lblNoiseAtt)
        Me.GroupBox2.Controls.Add(Me.lblNoiseAttValue)
        Me.GroupBox2.Controls.Add(Me.tbPlotNoiseReduction)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.lblFFTAvg)
        Me.GroupBox2.Controls.Add(Me.tbOverlap)
        Me.GroupBox2.Controls.Add(Me.lblOverlap)
        Me.GroupBox2.Controls.Add(Me.tbFFTHistorySize)
        Me.GroupBox2.Controls.Add(Me.lblOverlapVal)
        Me.GroupBox2.Controls.Add(Me.lblAvgVal)
        Me.GroupBox2.Controls.Add(Me.lblSmoothingVal)
        Me.GroupBox2.Controls.Add(Me.lblWinPosVal)
        Me.GroupBox2.Controls.Add(Me.lblMaxFreqVal)
        Me.GroupBox2.Controls.Add(Me.lblMinFreqVal)
        Me.GroupBox2.Controls.Add(Me.tbLinesThickness)
        Me.GroupBox2.Controls.Add(Me.lblLinesThickness)
        Me.GroupBox2.Controls.Add(Me.tbFFTSmoothing)
        Me.GroupBox2.Controls.Add(Me.tbFFTZoomWinPos)
        Me.GroupBox2.Controls.Add(Me.tbFFTMax)
        Me.GroupBox2.Controls.Add(Me.tbFFTMin)
        Me.GroupBox2.Controls.Add(Me.chkShowDecay)
        Me.GroupBox2.Controls.Add(Me.lblFFTSmoothing)
        Me.GroupBox2.Controls.Add(Me.cmbFFTYScale)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.lblFFTWinPos)
        Me.GroupBox2.Controls.Add(Me.lblFFTMax)
        Me.GroupBox2.Controls.Add(Me.lblFFTMin)
        Me.GroupBox2.Controls.Add(Me.chkFFTZoom)
        Me.GroupBox2.Controls.Add(Me.chkFFTLogScale)
        Me.GroupBox2.Location = New Point(200, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New Size(350, 321)
        Me.GroupBox2.TabIndex = 41
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "FFT Rendering Options"
        '
        'tbYScale
        '
        Me.tbYScale.AutoSize = False
        Me.tbYScale.LargeChange = 100
        Me.tbYScale.Location = New Point(79, 239)
        Me.tbYScale.Maximum = 1000
        Me.tbYScale.Minimum = 1
        Me.tbYScale.Name = "tbYScale"
        Me.tbYScale.Size = New Size(227, 21)
        Me.tbYScale.SmallChange = 10
        Me.tbYScale.TabIndex = 75
        Me.tbYScale.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbYScale, "Controls the gain of the vertical (amplitude) scale.")
        Me.tbYScale.Value = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Cursor = Cursors.Arrow
        Me.Label11.Location = New Point(15, 243)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New Size(41, 13)
        Me.Label11.TabIndex = 74
        Me.Label11.Text = "Y Scale"
        Me.Label11.UseMnemonic = False
        '
        'lblYScaleValue
        '
        Me.lblYScaleValue.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblYScaleValue.Location = New Point(303, 244)
        Me.lblYScaleValue.Name = "lblYScaleValue"
        Me.lblYScaleValue.Size = New Size(43, 11)
        Me.lblYScaleValue.TabIndex = 73
        Me.lblYScaleValue.Text = "0"
        Me.lblYScaleValue.TextAlign = ContentAlignment.MiddleRight
        '
        'tbNoiseAtt
        '
        Me.tbNoiseAtt.AutoSize = False
        Me.tbNoiseAtt.LargeChange = 1
        Me.tbNoiseAtt.Location = New Point(79, 212)
        Me.tbNoiseAtt.Name = "tbNoiseAtt"
        Me.tbNoiseAtt.Size = New Size(227, 21)
        Me.tbNoiseAtt.TabIndex = 72
        Me.tbNoiseAtt.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbNoiseAtt, "Tries to minimize small clicks from the sampled audio in order to provide a more " &
        "stable and cleaner transform.")
        '
        'lblNoiseAtt
        '
        Me.lblNoiseAtt.AutoSize = True
        Me.lblNoiseAtt.Cursor = Cursors.Arrow
        Me.lblNoiseAtt.Location = New Point(15, 216)
        Me.lblNoiseAtt.Name = "lblNoiseAtt"
        Me.lblNoiseAtt.Size = New Size(55, 13)
        Me.lblNoiseAtt.TabIndex = 71
        Me.lblNoiseAtt.Text = "Noise Att."
        Me.lblNoiseAtt.UseMnemonic = False
        '
        'lblNoiseAttValue
        '
        Me.lblNoiseAttValue.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoiseAttValue.Location = New Point(303, 217)
        Me.lblNoiseAttValue.Name = "lblNoiseAttValue"
        Me.lblNoiseAttValue.Size = New Size(43, 11)
        Me.lblNoiseAttValue.TabIndex = 70
        Me.lblNoiseAttValue.Text = "0"
        Me.lblNoiseAttValue.TextAlign = ContentAlignment.MiddleRight
        '
        'tbPlotNoiseReduction
        '
        Me.tbPlotNoiseReduction.AutoSize = False
        Me.tbPlotNoiseReduction.Location = New Point(202, 276)
        Me.tbPlotNoiseReduction.Name = "tbPlotNoiseReduction"
        Me.tbPlotNoiseReduction.Size = New Size(103, 20)
        Me.tbPlotNoiseReduction.TabIndex = 69
        Me.ttInfo.SetToolTip(Me.tbPlotNoiseReduction, "This feature tries to minimize the noise over higher frequencies, resulting in a " &
        "cleaner display of the transform curve.")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New Point(100, 280)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New Size(105, 13)
        Me.Label13.TabIndex = 68
        Me.Label13.Text = "Plot Noise Reduction"
        '
        'lblFFTAvg
        '
        Me.lblFFTAvg.AutoSize = True
        Me.lblFFTAvg.Cursor = Cursors.Arrow
        Me.lblFFTAvg.Location = New Point(15, 163)
        Me.lblFFTAvg.Name = "lblFFTAvg"
        Me.lblFFTAvg.Size = New Size(69, 13)
        Me.lblFFTAvg.TabIndex = 64
        Me.lblFFTAvg.Text = "FFT Average"
        Me.lblFFTAvg.UseMnemonic = False
        '
        'tbOverlap
        '
        Me.tbOverlap.AutoSize = False
        Me.tbOverlap.LargeChange = 10
        Me.tbOverlap.Location = New Point(79, 185)
        Me.tbOverlap.Maximum = 99
        Me.tbOverlap.Name = "tbOverlap"
        Me.tbOverlap.Size = New Size(227, 21)
        Me.tbOverlap.TabIndex = 67
        Me.tbOverlap.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbOverlap, "Applies an overlap over the sampled audio to minimize artifacts and provide bette" &
        "r stabilization of the resulting transform.")
        '
        'lblOverlap
        '
        Me.lblOverlap.AutoSize = True
        Me.lblOverlap.Cursor = Cursors.Arrow
        Me.lblOverlap.Location = New Point(15, 189)
        Me.lblOverlap.Name = "lblOverlap"
        Me.lblOverlap.Size = New Size(45, 13)
        Me.lblOverlap.TabIndex = 66
        Me.lblOverlap.Text = "Overlap"
        Me.lblOverlap.UseMnemonic = False
        '
        'tbFFTHistorySize
        '
        Me.tbFFTHistorySize.AutoSize = False
        Me.tbFFTHistorySize.LargeChange = 1
        Me.tbFFTHistorySize.Location = New Point(79, 159)
        Me.tbFFTHistorySize.Maximum = 20
        Me.tbFFTHistorySize.Minimum = 1
        Me.tbFFTHistorySize.Name = "tbFFTHistorySize"
        Me.tbFFTHistorySize.Size = New Size(227, 21)
        Me.tbFFTHistorySize.TabIndex = 65
        Me.tbFFTHistorySize.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbFFTHistorySize, "Applies a simple average between the current transform and previous ones, resulti" &
        "ng in much more stable transform display.")
        Me.tbFFTHistorySize.Value = 1
        '
        'lblOverlapVal
        '
        Me.lblOverlapVal.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverlapVal.Location = New Point(303, 190)
        Me.lblOverlapVal.Name = "lblOverlapVal"
        Me.lblOverlapVal.Size = New Size(43, 11)
        Me.lblOverlapVal.TabIndex = 63
        Me.lblOverlapVal.Text = "0"
        Me.lblOverlapVal.TextAlign = ContentAlignment.MiddleRight
        '
        'lblAvgVal
        '
        Me.lblAvgVal.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvgVal.Location = New Point(303, 164)
        Me.lblAvgVal.Name = "lblAvgVal"
        Me.lblAvgVal.Size = New Size(43, 11)
        Me.lblAvgVal.TabIndex = 63
        Me.lblAvgVal.Text = "0"
        Me.lblAvgVal.TextAlign = ContentAlignment.MiddleRight
        '
        'lblSmoothingVal
        '
        Me.lblSmoothingVal.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmoothingVal.Location = New Point(303, 138)
        Me.lblSmoothingVal.Name = "lblSmoothingVal"
        Me.lblSmoothingVal.Size = New Size(43, 11)
        Me.lblSmoothingVal.TabIndex = 63
        Me.lblSmoothingVal.Text = "0"
        Me.lblSmoothingVal.TextAlign = ContentAlignment.MiddleRight
        '
        'lblWinPosVal
        '
        Me.lblWinPosVal.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblWinPosVal.Location = New Point(303, 112)
        Me.lblWinPosVal.Name = "lblWinPosVal"
        Me.lblWinPosVal.Size = New Size(43, 11)
        Me.lblWinPosVal.TabIndex = 63
        Me.lblWinPosVal.Text = "22.0 KHz"
        Me.lblWinPosVal.TextAlign = ContentAlignment.MiddleRight
        '
        'lblMaxFreqVal
        '
        Me.lblMaxFreqVal.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxFreqVal.Location = New Point(303, 87)
        Me.lblMaxFreqVal.Name = "lblMaxFreqVal"
        Me.lblMaxFreqVal.Size = New Size(43, 11)
        Me.lblMaxFreqVal.TabIndex = 62
        Me.lblMaxFreqVal.Text = "22.0 KHz"
        Me.lblMaxFreqVal.TextAlign = ContentAlignment.MiddleRight
        '
        'lblMinFreqVal
        '
        Me.lblMinFreqVal.Font = New Font("Tahoma", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinFreqVal.Location = New Point(303, 61)
        Me.lblMinFreqVal.Name = "lblMinFreqVal"
        Me.lblMinFreqVal.Size = New Size(43, 11)
        Me.lblMinFreqVal.TabIndex = 61
        Me.lblMinFreqVal.Text = "22.0 KHz"
        Me.lblMinFreqVal.TextAlign = ContentAlignment.MiddleRight
        '
        'tbLinesThickness
        '
        Me.tbLinesThickness.AutoSize = False
        Me.tbLinesThickness.Location = New Point(203, 295)
        Me.tbLinesThickness.Name = "tbLinesThickness"
        Me.tbLinesThickness.Size = New Size(103, 20)
        Me.tbLinesThickness.TabIndex = 60
        Me.ttInfo.SetToolTip(Me.tbLinesThickness, "Controls the thickness of the lines used to display the transform curve")
        '
        'lblLinesThickness
        '
        Me.lblLinesThickness.AutoSize = True
        Me.lblLinesThickness.Location = New Point(125, 299)
        Me.lblLinesThickness.Name = "lblLinesThickness"
        Me.lblLinesThickness.Size = New Size(80, 13)
        Me.lblLinesThickness.TabIndex = 59
        Me.lblLinesThickness.Text = "Lines Thickness"
        '
        'tbFFTSmoothing
        '
        Me.tbFFTSmoothing.AutoSize = False
        Me.tbFFTSmoothing.LargeChange = 1
        Me.tbFFTSmoothing.Location = New Point(79, 133)
        Me.tbFFTSmoothing.Name = "tbFFTSmoothing"
        Me.tbFFTSmoothing.Size = New Size(227, 21)
        Me.tbFFTSmoothing.TabIndex = 58
        Me.tbFFTSmoothing.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbFFTSmoothing, resources.GetString("tbFFTSmoothing.ToolTip"))
        '
        'tbFFTZoomWinPos
        '
        Me.tbFFTZoomWinPos.AutoSize = False
        Me.tbFFTZoomWinPos.LargeChange = 100
        Me.tbFFTZoomWinPos.Location = New Point(79, 107)
        Me.tbFFTZoomWinPos.Name = "tbFFTZoomWinPos"
        Me.tbFFTZoomWinPos.Size = New Size(227, 21)
        Me.tbFFTZoomWinPos.SmallChange = 10
        Me.tbFFTZoomWinPos.TabIndex = 57
        Me.tbFFTZoomWinPos.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbFFTZoomWinPos, "When zooming is enabled and the whole spectrum doesn’t fit the available control’" &
        "s width, this slider can be used to navigate the complete transform.")
        '
        'tbFFTMax
        '
        Me.tbFFTMax.AutoSize = False
        Me.tbFFTMax.LargeChange = 100
        Me.tbFFTMax.Location = New Point(79, 82)
        Me.tbFFTMax.Name = "tbFFTMax"
        Me.tbFFTMax.Size = New Size(227, 21)
        Me.tbFFTMax.SmallChange = 10
        Me.tbFFTMax.TabIndex = 56
        Me.tbFFTMax.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbFFTMax, "Defines the highest frequency displayed on the control")
        '
        'tbFFTMin
        '
        Me.tbFFTMin.AutoSize = False
        Me.tbFFTMin.LargeChange = 100
        Me.tbFFTMin.Location = New Point(79, 56)
        Me.tbFFTMin.Name = "tbFFTMin"
        Me.tbFFTMin.Size = New Size(227, 21)
        Me.tbFFTMin.SmallChange = 10
        Me.tbFFTMin.TabIndex = 55
        Me.tbFFTMin.TickStyle = TickStyle.None
        Me.ttInfo.SetToolTip(Me.tbFFTMin, "Defines the lowerst frequency displayed on the control")
        '
        'chkShowDecay
        '
        Me.chkShowDecay.AutoSize = True
        Me.chkShowDecay.Location = New Point(16, 298)
        Me.chkShowDecay.Name = "chkShowDecay"
        Me.chkShowDecay.RightToLeft = RightToLeft.Yes
        Me.chkShowDecay.Size = New Size(85, 17)
        Me.chkShowDecay.TabIndex = 54
        Me.chkShowDecay.Text = "Show Decay"
        Me.ttInfo.SetToolTip(Me.chkShowDecay, "Not yet implemented." & ChrW(13) & ChrW(10) & "Provided just for backward compatibility with the DirectX v" &
        "ersion of the control.")
        Me.chkShowDecay.Visible = False
        '
        'lblFFTSmoothing
        '
        Me.lblFFTSmoothing.AutoSize = True
        Me.lblFFTSmoothing.Cursor = Cursors.Arrow
        Me.lblFFTSmoothing.Location = New Point(15, 137)
        Me.lblFFTSmoothing.Name = "lblFFTSmoothing"
        Me.lblFFTSmoothing.Size = New Size(57, 13)
        Me.lblFFTSmoothing.TabIndex = 53
        Me.lblFFTSmoothing.Text = "Smoothing"
        Me.lblFFTSmoothing.UseMnemonic = False
        '
        'cmbFFTYScale
        '
        Me.cmbFFTYScale.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbFFTYScale.Location = New Point(139, 30)
        Me.cmbFFTYScale.Name = "cmbFFTYScale"
        Me.cmbFFTYScale.Size = New Size(131, 21)
        Me.cmbFFTYScale.TabIndex = 51
        Me.ttInfo.SetToolTip(Me.cmbFFTYScale, "Selects the different vertical scale (amplitude) modes")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New Point(139, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Size(62, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "FFT Y Scale"
        '
        'lblFFTWinPos
        '
        Me.lblFFTWinPos.AutoSize = True
        Me.lblFFTWinPos.Cursor = Cursors.Arrow
        Me.lblFFTWinPos.Location = New Point(15, 111)
        Me.lblFFTWinPos.Name = "lblFFTWinPos"
        Me.lblFFTWinPos.Size = New Size(45, 13)
        Me.lblFFTWinPos.TabIndex = 48
        Me.lblFFTWinPos.Text = "Win Pos"
        Me.lblFFTWinPos.UseMnemonic = False
        '
        'lblFFTMax
        '
        Me.lblFFTMax.AutoSize = True
        Me.lblFFTMax.Cursor = Cursors.Arrow
        Me.lblFFTMax.Location = New Point(15, 86)
        Me.lblFFTMax.Name = "lblFFTMax"
        Me.lblFFTMax.Size = New Size(52, 13)
        Me.lblFFTMax.TabIndex = 47
        Me.lblFFTMax.Text = "Max Freq"
        Me.lblFFTMax.UseMnemonic = False
        '
        'lblFFTMin
        '
        Me.lblFFTMin.AutoSize = True
        Me.lblFFTMin.Cursor = Cursors.Arrow
        Me.lblFFTMin.Location = New Point(15, 60)
        Me.lblFFTMin.Name = "lblFFTMin"
        Me.lblFFTMin.Size = New Size(48, 13)
        Me.lblFFTMin.TabIndex = 46
        Me.lblFFTMin.Text = "Min Freq"
        Me.lblFFTMin.UseMnemonic = False
        '
        'chkFFTZoom
        '
        Me.chkFFTZoom.AutoSize = True
        Me.chkFFTZoom.Checked = True
        Me.chkFFTZoom.CheckState = CheckState.Checked
        Me.chkFFTZoom.FlatStyle = FlatStyle.System
        Me.chkFFTZoom.Location = New Point(16, 16)
        Me.chkFFTZoom.Name = "chkFFTZoom"
        Me.chkFFTZoom.Size = New Size(116, 18)
        Me.chkFFTZoom.TabIndex = 41
        Me.chkFFTZoom.Text = "FFT X Scale Zoom"
        Me.ttInfo.SetToolTip(Me.chkFFTZoom, "When enabled, the horizontal scale (frequency) will be zoomed so that it occupies" &
        " the whole width of the control")
        '
        'chkFFTLogScale
        '
        Me.chkFFTLogScale.AutoSize = True
        Me.chkFFTLogScale.Checked = True
        Me.chkFFTLogScale.CheckState = CheckState.Checked
        Me.chkFFTLogScale.FlatStyle = FlatStyle.System
        Me.chkFFTLogScale.Location = New Point(16, 33)
        Me.chkFFTLogScale.Name = "chkFFTLogScale"
        Me.chkFFTLogScale.Size = New Size(107, 18)
        Me.chkFFTLogScale.TabIndex = 40
        Me.chkFFTLogScale.Text = "FFT X Log Scale"
        Me.ttInfo.SetToolTip(Me.chkFFTLogScale, "When enabled, the horizontal scale (frequency) will be displayed using a logarith" &
        "mic scale")
        '
        'gbColors
        '
        Me.gbColors.Controls.Add(Me.btnColors3)
        Me.gbColors.Controls.Add(Me.btnColors2)
        Me.gbColors.Controls.Add(Me.btnColors1)
        Me.gbColors.Controls.Add(Me.lblLedBarsLeftMax)
        Me.gbColors.Controls.Add(Me.lblLedBarsRightMax)
        Me.gbColors.Controls.Add(Me.btnRedOn)
        Me.gbColors.Controls.Add(Me.btnRedOff)
        Me.gbColors.Controls.Add(Me.btnYellowOn)
        Me.gbColors.Controls.Add(Me.btnYellowOff)
        Me.gbColors.Controls.Add(Me.btnGreenOn)
        Me.gbColors.Controls.Add(Me.btnGreenOff)
        Me.gbColors.Controls.Add(Me.Label2)
        Me.gbColors.Controls.Add(Me.Label3)
        Me.gbColors.Controls.Add(Me.Label7)
        Me.gbColors.Controls.Add(Me.Label8)
        Me.gbColors.Location = New Point(8, 335)
        Me.gbColors.Name = "gbColors"
        Me.gbColors.Size = New Size(315, 137)
        Me.gbColors.TabIndex = 42
        Me.gbColors.TabStop = False
        Me.gbColors.Text = "Colors"
        '
        'btnColors3
        '
        Me.btnColors3.Location = New Point(224, 102)
        Me.btnColors3.Name = "btnColors3"
        Me.btnColors3.Size = New Size(75, 23)
        Me.btnColors3.TabIndex = 43
        Me.btnColors3.Text = "Mono"
        Me.btnColors3.UseVisualStyleBackColor = True
        '
        'btnColors2
        '
        Me.btnColors2.Location = New Point(116, 102)
        Me.btnColors2.Name = "btnColors2"
        Me.btnColors2.Size = New Size(75, 23)
        Me.btnColors2.TabIndex = 42
        Me.btnColors2.Text = "Modern"
        Me.btnColors2.UseVisualStyleBackColor = True
        '
        'btnColors1
        '
        Me.btnColors1.Location = New Point(8, 102)
        Me.btnColors1.Name = "btnColors1"
        Me.btnColors1.Size = New Size(75, 23)
        Me.btnColors1.TabIndex = 41
        Me.btnColors1.Text = "Default"
        Me.btnColors1.UseVisualStyleBackColor = True
        '
        'lblLedBarsLeftMax
        '
        Me.lblLedBarsLeftMax.AutoSize = True
        Me.lblLedBarsLeftMax.Location = New Point(230, 58)
        Me.lblLedBarsLeftMax.Name = "lblLedBarsLeftMax"
        Me.lblLedBarsLeftMax.Size = New Size(49, 13)
        Me.lblLedBarsLeftMax.TabIndex = 40
        Me.lblLedBarsLeftMax.Text = "Left Max"
        '
        'lblLedBarsRightMax
        '
        Me.lblLedBarsRightMax.AutoSize = True
        Me.lblLedBarsRightMax.Location = New Point(224, 27)
        Me.lblLedBarsRightMax.Name = "lblLedBarsRightMax"
        Me.lblLedBarsRightMax.Size = New Size(55, 13)
        Me.lblLedBarsRightMax.TabIndex = 39
        Me.lblLedBarsRightMax.Text = "Right Max"
        '
        'btnRedOn
        '
        Me.btnRedOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRedOn.Cursor = Cursors.Arrow
        Me.btnRedOn.FlatStyle = FlatStyle.Flat
        Me.btnRedOn.Location = New Point(283, 56)
        Me.btnRedOn.Name = "btnRedOn"
        Me.btnRedOn.Size = New Size(16, 16)
        Me.btnRedOn.TabIndex = 38
        Me.btnRedOn.UseVisualStyleBackColor = False
        '
        'btnRedOff
        '
        Me.btnRedOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRedOff.Cursor = Cursors.Arrow
        Me.btnRedOff.FlatStyle = FlatStyle.Flat
        Me.btnRedOff.Location = New Point(283, 25)
        Me.btnRedOff.Name = "btnRedOff"
        Me.btnRedOff.Size = New Size(16, 16)
        Me.btnRedOff.TabIndex = 37
        Me.btnRedOff.UseVisualStyleBackColor = False
        '
        'btnYellowOn
        '
        Me.btnYellowOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOn.Cursor = Cursors.Arrow
        Me.btnYellowOn.FlatStyle = FlatStyle.Flat
        Me.btnYellowOn.Location = New Point(195, 56)
        Me.btnYellowOn.Name = "btnYellowOn"
        Me.btnYellowOn.Size = New Size(16, 16)
        Me.btnYellowOn.TabIndex = 31
        Me.btnYellowOn.UseVisualStyleBackColor = False
        '
        'btnYellowOff
        '
        Me.btnYellowOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOff.Cursor = Cursors.Arrow
        Me.btnYellowOff.FlatStyle = FlatStyle.Flat
        Me.btnYellowOff.Location = New Point(195, 25)
        Me.btnYellowOff.Name = "btnYellowOff"
        Me.btnYellowOff.Size = New Size(16, 16)
        Me.btnYellowOff.TabIndex = 29
        Me.btnYellowOff.UseVisualStyleBackColor = False
        '
        'btnGreenOn
        '
        Me.btnGreenOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOn.Cursor = Cursors.Arrow
        Me.btnGreenOn.FlatStyle = FlatStyle.Flat
        Me.btnGreenOn.Location = New Point(85, 56)
        Me.btnGreenOn.Name = "btnGreenOn"
        Me.btnGreenOn.Size = New Size(16, 16)
        Me.btnGreenOn.TabIndex = 35
        Me.btnGreenOn.UseVisualStyleBackColor = False
        '
        'btnGreenOff
        '
        Me.btnGreenOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOff.Cursor = Cursors.Arrow
        Me.btnGreenOff.FlatStyle = FlatStyle.Flat
        Me.btnGreenOff.Location = New Point(85, 25)
        Me.btnGreenOff.Name = "btnGreenOff"
        Me.btnGreenOff.Size = New Size(16, 16)
        Me.btnGreenOff.TabIndex = 33
        Me.btnGreenOff.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(10, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New Size(73, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Left Ch Wave"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New Point(10, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New Size(74, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Left Ch (Hold)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New Point(115, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New Size(79, 13)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "Right Ch Wave"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New Point(115, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New Size(80, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Right Ch (Hold)"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.tbDecaySpeed)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.tbDecayDelay)
        Me.GroupBox4.Controls.Add(Me.lblDelay)
        Me.GroupBox4.Controls.Add(Me.chkShowMinMaxRange)
        Me.GroupBox4.Controls.Add(Me.chkHoldMinPeaks)
        Me.GroupBox4.Controls.Add(Me.chkHoldMaxPeaks)
        Me.GroupBox4.Location = New Point(329, 335)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New Size(221, 137)
        Me.GroupBox4.TabIndex = 43
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Hold Peaks"
        '
        'tbDecaySpeed
        '
        Me.tbDecaySpeed.AutoSize = False
        Me.tbDecaySpeed.Location = New Point(56, 108)
        Me.tbDecaySpeed.Maximum = 100
        Me.tbDecaySpeed.Name = "tbDecaySpeed"
        Me.tbDecaySpeed.Size = New Size(157, 20)
        Me.tbDecaySpeed.TabIndex = 63
        Me.tbDecaySpeed.TickFrequency = 10
        Me.ttInfo.SetToolTip(Me.tbDecaySpeed, "Controls the speed at which the highest and lowest values tend to collapse onto t" &
        "he current and latest transform's values (decay speed).")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New Point(16, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New Size(37, 13)
        Me.Label9.TabIndex = 62
        Me.Label9.Text = "Speed"
        '
        'tbDecayDelay
        '
        Me.tbDecayDelay.AutoSize = False
        Me.tbDecayDelay.Location = New Point(56, 82)
        Me.tbDecayDelay.Maximum = 100
        Me.tbDecayDelay.Name = "tbDecayDelay"
        Me.tbDecayDelay.Size = New Size(157, 20)
        Me.tbDecayDelay.TabIndex = 61
        Me.tbDecayDelay.TickFrequency = 10
        Me.ttInfo.SetToolTip(Me.tbDecayDelay, "Controls the time that the highest and lowest values will maintain their values.")
        '
        'lblDelay
        '
        Me.lblDelay.AutoSize = True
        Me.lblDelay.Location = New Point(16, 86)
        Me.lblDelay.Name = "lblDelay"
        Me.lblDelay.Size = New Size(34, 13)
        Me.lblDelay.TabIndex = 53
        Me.lblDelay.Text = "Delay"
        '
        'chkShowMinMaxRange
        '
        Me.chkShowMinMaxRange.AutoSize = True
        Me.chkShowMinMaxRange.Location = New Point(16, 60)
        Me.chkShowMinMaxRange.Name = "chkShowMinMaxRange"
        Me.chkShowMinMaxRange.Size = New Size(129, 17)
        Me.chkShowMinMaxRange.TabIndex = 52
        Me.chkShowMinMaxRange.Text = "Show Min-Max Range"
        Me.ttInfo.SetToolTip(Me.chkShowMinMaxRange, "When enabled, DXVUMeterNET will highlight the area between the history of highest" &
        " and lowest values." & ChrW(13) & ChrW(10))
        '
        'chkHoldMinPeaks
        '
        Me.chkHoldMinPeaks.AutoSize = True
        Me.chkHoldMinPeaks.Location = New Point(16, 41)
        Me.chkHoldMinPeaks.Name = "chkHoldMinPeaks"
        Me.chkHoldMinPeaks.Size = New Size(97, 17)
        Me.chkHoldMinPeaks.TabIndex = 51
        Me.chkHoldMinPeaks.Text = "Hold Min Peaks"
        Me.ttInfo.SetToolTip(Me.chkHoldMinPeaks, "When enabled, DXVUMeterNET will display a history of the transform's lowest value" &
        "s.")
        '
        'chkHoldMaxPeaks
        '
        Me.chkHoldMaxPeaks.AutoSize = True
        Me.chkHoldMaxPeaks.Location = New Point(16, 22)
        Me.chkHoldMaxPeaks.Name = "chkHoldMaxPeaks"
        Me.chkHoldMaxPeaks.Size = New Size(101, 17)
        Me.chkHoldMaxPeaks.TabIndex = 50
        Me.chkHoldMaxPeaks.Text = "Hold Max Peaks"
        Me.ttInfo.SetToolTip(Me.chkHoldMaxPeaks, "When enabled, DXVUMeterNET will display a history of the transform's highest valu" &
        "es.")
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkDTMFDetect)
        Me.GroupBox5.Controls.Add(Me.btnClear)
        Me.GroupBox5.Controls.Add(Me.txtDTMFTones)
        Me.GroupBox5.Location = New Point(8, 478)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New Size(542, 54)
        Me.GroupBox5.TabIndex = 44
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "DTMF Detection"
        '
        'chkDTMFDetect
        '
        Me.chkDTMFDetect.AutoSize = True
        Me.chkDTMFDetect.Location = New Point(6, 25)
        Me.chkDTMFDetect.Name = "chkDTMFDetect"
        Me.chkDTMFDetect.Size = New Size(15, 14)
        Me.chkDTMFDetect.TabIndex = 2
        Me.ttInfo.SetToolTip(Me.chkDTMFDetect, resources.GetString("chkDTMFDetect.ToolTip"))
        Me.chkDTMFDetect.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New Point(482, 22)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New Size(52, 21)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtDTMFTones
        '
        Me.txtDTMFTones.BackColor = SystemColors.Control
        Me.txtDTMFTones.Enabled = False
        Me.txtDTMFTones.Location = New Point(27, 22)
        Me.txtDTMFTones.Name = "txtDTMFTones"
        Me.txtDTMFTones.ReadOnly = True
        Me.txtDTMFTones.Size = New Size(449, 21)
        Me.txtDTMFTones.TabIndex = 0
        '
        'frmFFTOptions
        '
        Me.AutoScaleBaseSize = New Size(5, 14)
        Me.ClientSize = New Size(562, 544)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.gbColors)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFFTOptions"
        Me.Text = "FFT Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.tbYScale, ISupportInitialize).EndInit()
        CType(Me.tbNoiseAtt, ISupportInitialize).EndInit()
        CType(Me.tbPlotNoiseReduction, ISupportInitialize).EndInit()
        CType(Me.tbOverlap, ISupportInitialize).EndInit()
        CType(Me.tbFFTHistorySize, ISupportInitialize).EndInit()
        CType(Me.tbLinesThickness, ISupportInitialize).EndInit()
        CType(Me.tbFFTSmoothing, ISupportInitialize).EndInit()
        CType(Me.tbFFTZoomWinPos, ISupportInitialize).EndInit()
        CType(Me.tbFFTMax, ISupportInitialize).EndInit()
        CType(Me.tbFFTMin, ISupportInitialize).EndInit()
        Me.gbColors.ResumeLayout(False)
        Me.gbColors.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.tbDecaySpeed, ISupportInitialize).EndInit()
        CType(Me.tbDecayDelay, ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Friend Sub InitDlg()
        btnGreenOff.BackColor = dxvuCtrl.GreenOff
        btnGreenOn.BackColor = dxvuCtrl.GreenOn
        btnYellowOff.BackColor = dxvuCtrl.YellowOff
        btnYellowOn.BackColor = dxvuCtrl.YellowOn
        btnRedOff.BackColor = dxvuCtrl.RedOff
        btnRedOn.BackColor = dxvuCtrl.RedOn
        tbLinesThickness.Value = dxvuCtrl.LinesThickness
        tbDecayDelay.Value = dxvuCtrl.FFTPeaksDecayDelay
        tbDecaySpeed.Value = tbDecayDelay.Maximum - dxvuCtrl.FFTPeaksDecaySpeed
        tbLinesThickness.Value = 2
        tbFFTHistorySize.Value = 4
        tbOverlap.Value = 0
        tbFFTSmoothing.Value = 4
        tbPlotNoiseReduction.Value = 0
        tbNoiseAtt.Value = 0
        tbYScale.Value = 100
    End Sub

    Friend Sub RestoreColors()
        With dxvuCtrl
            .YellowOff = btnYellowOff.BackColor
            .YellowOn = btnYellowOn.BackColor
            .GreenOff = btnGreenOff.BackColor
            .GreenOn = btnGreenOn.BackColor
            .RedOff = btnRedOff.BackColor
            .RedOn = btnRedOn.BackColor
            .LinesThickness = tbLinesThickness.Value
            .FFTPeaksDecayDelay = tbDecayDelay.Value
            .FFTPeaksDecaySpeed = tbDecaySpeed.Maximum - tbDecaySpeed.Value
        End With
    End Sub

    Friend Sub InitFFTOptions()
        Dim k As Object

        cmbFFTStyle.Items.Clear()
        For Each k In [Enum].GetValues(GetType(DXVUMeterNETGDI.FFTStyleConstants))
            cmbFFTStyle.Items.Add(k)
        Next
        cmbFFTStyle.SelectedIndex = 0

        cmbFFTSize.Items.Clear()
        For Each k In [Enum].GetValues(GetType(FFTSizeConstants))
            cmbFFTSize.Items.Add(k)
        Next
        cmbFFTSize.SelectedIndex = 5

        cmbFFTWindow.Items.Clear()
        For Each k In [Enum].GetValues(GetType(FFTWindowConstants))
            cmbFFTWindow.Items.Add(k)
        Next
        cmbFFTWindow.SelectedIndex = 2

        cmbFFTYScale.Items.Clear()
        For Each k In [Enum].GetValues(GetType(DXVUMeterNETGDI.FFTYScaleConstants))
            cmbFFTYScale.Items.Add(k)
        Next
        cmbFFTYScale.SelectedIndex = 2

        cmbFFTLineStyleChannelMode.Items.Clear()
        For Each k In [Enum].GetValues(GetType(DXVUMeterNETGDI.FFTLineChannelModeConstants))
            cmbFFTLineStyleChannelMode.Items.Add(k)
        Next
        cmbFFTLineStyleChannelMode.SelectedIndex = 0

        SetFFTZoomParams(True)
        InitDlg()
    End Sub

    Private Sub cmbFFTSize_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbFFTSize.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub cmbFFTSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFFTSize.SelectedIndexChanged
        SetupFFTParams()
    End Sub

    Private Sub SetupFFTParams()
        dxvuCtrl.FFTSize = cmbFFTSize.SelectedItem
    End Sub

    Private Sub ApplyFFTSliders()
        If tbFFTMin.Value >= dxvuCtrl.Frequency / 2 Then tbFFTMin.Value = 0
        If tbFFTMax.Value > dxvuCtrl.Frequency Then tbFFTMin.Value = dxvuCtrl.Frequency / 2

        dxvuCtrl.FFTXMin = tbFFTMin.Value
        lblMinFreqVal.Text = HumanFreq(tbFFTMin.Value)

        dxvuCtrl.FFTXMax = tbFFTMax.Value
        lblMaxFreqVal.Text = HumanFreq(tbFFTMax.Value)

        dxvuCtrl.FFTXZoomWindowPos = tbFFTZoomWinPos.Value
        lblWinPosVal.Text = HumanFreq(tbFFTZoomWinPos.Value)

        dxvuCtrl.FFTSmoothing = tbFFTSmoothing.Value
        lblSmoothingVal.Text = tbFFTSmoothing.Value.ToString()

        dxvuCtrl.FFTHistorySize = tbFFTHistorySize.Value
        lblAvgVal.Text = tbFFTHistorySize.Value.ToString()

        dxvuCtrl.Overlap = tbOverlap.Value / 100
        lblOverlapVal.Text = tbOverlap.Value.ToString() + "%"

        dxvuCtrl.FFTPlotNoiseReduction = tbPlotNoiseReduction.Value

        dxvuCtrl.NoiseFilter = tbNoiseAtt.Value
        lblNoiseAttValue.Text = tbNoiseAtt.Value.ToString()

        dxvuCtrl.FFTYScaleMultiplier = tbYScale.Value / 100
        lblYScaleValue.Text = dxvuCtrl.FFTYScaleMultiplier.ToString("F2")
    End Sub

    Friend Sub SetFFTZoomParams(Optional ByVal ResetValues As Boolean = False)
        For i As Integer = 1 To 5
            With tbFFTMin
                .Minimum = 0
                .Maximum = dxvuCtrl.Frequency / 2
                If ResetValues Then .Value = .Minimum
                .SmallChange = 100
                .LargeChange = 1000
            End With

            With tbFFTMax
                .Minimum = 0
                .Maximum = dxvuCtrl.Frequency / 2
                If .Value < tbFFTMin.Value Then .Value = tbFFTMin.Value + 100
                If .Value > .Maximum Then .Value = .Maximum
                If ResetValues Then .Value = .Maximum
                .SmallChange = 100
                .LargeChange = 1000
            End With

            With tbFFTZoomWinPos
                .Minimum = 0
                .Maximum = dxvuCtrl.Frequency / 2 - tbFFTMax.Value
                Try
                    If .Value > 0 AndAlso .Value + .SmallChange > .Maximum Then .Value = .Value - .SmallChange
                Catch
                End Try
                .Enabled = tbFFTMax.Value < tbFFTMax.Maximum
                lblWinPosVal.Enabled = .Enabled
                .SmallChange = 100
                .LargeChange = 1000
            End With
            ApplyFFTSliders()
        Next i
    End Sub

    Private Sub cmbFFTWindow_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbFFTWindow.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub cmbFFTWindow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFFTWindow.SelectedIndexChanged
        dxvuCtrl.FFTWindow = cmbFFTWindow.SelectedItem
    End Sub

    Private Sub chkFFTLogScale_CheckedChanged(sender As Object, e As EventArgs) Handles chkFFTLogScale.CheckedChanged
        If Not dxvuCtrl Is Nothing Then
            If chkFFTLogScale.Checked Then
                dxvuCtrl.FFTXScale = DXVUMeterNETGDI.FFTXScaleConstants.Logarithmic
            Else
                dxvuCtrl.FFTXScale = DXVUMeterNETGDI.FFTXScaleConstants.Normal
            End If
        End If
    End Sub

    Private Sub sbFFTMin_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbFFTMin.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub sbFFTMin_ValueChanged(sender As Object, e As EventArgs) Handles tbFFTMin.ValueChanged
        ApplyFFTSliders()
        SetFFTZoomParams()
    End Sub

    Private Sub sbFFTMax_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbFFTMax.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub sbFFTMax_ValueChanged(sender As Object, e As EventArgs) Handles tbFFTMax.ValueChanged
        ApplyFFTSliders()
        SetFFTZoomParams()
    End Sub

    Private Sub chkFFTZoom_CheckedChanged(sender As Object, e As EventArgs) Handles chkFFTZoom.CheckedChanged
        dxvuCtrl.FFTXZoom = chkFFTZoom.Checked
    End Sub

    Private Sub sbFFTZoomWinPos_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbFFTZoomWinPos.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub sbFFTZoomWinPos_ValueChanged(sender As Object, e As EventArgs) Handles tbFFTZoomWinPos.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub cmbFFTStyle_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbFFTStyle.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub cmbFFTStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFFTStyle.SelectedIndexChanged
        dxvuCtrl.FFTStyle = cmbFFTStyle.SelectedItem

        If dxvuCtrl.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.TransferFunction Then
            dxvuCtrl.FFTLineChannelMode = DXVUMeterNETGDI.FFTLineChannelModeConstants.LeftOverRight
        Else
            dxvuCtrl.FFTLineChannelMode = cmbFFTLineStyleChannelMode.SelectedItem
        End If

        SetUpUI()
    End Sub

    Private Sub SetUpUI()
        With dxvuCtrl
            lblFFTSmoothing.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line Or .FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Filled)
            tbFFTSmoothing.Enabled = lblFFTSmoothing.Enabled
            chkShowDecay.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line)
            chkHoldMinPeaks.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line)
            chkShowMinMaxRange.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line)
            chkHoldMaxPeaks.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)
            chkFFTLogScale.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)
            tbDecayDelay.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum AndAlso (.FFTShowMinMaxRange OrElse .FFTHoldMaxPeaks OrElse .FFTHoldMinPeaks))
            tbDecaySpeed.Enabled = tbDecayDelay.Enabled

            btnGreenOff.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)
            btnGreenOn.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)

            btnRedOff.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.LedBars)
            btnRedOn.Enabled = btnRedOff.Enabled
            lblLedBarsLeftMax.Enabled = btnRedOff.Enabled
            lblLedBarsRightMax.Enabled = btnRedOff.Enabled

            btnYellowOff.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)
            btnYellowOn.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)

            tbLinesThickness.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line And Not .FFTShowDecay)
            lblLinesThickness.Enabled = tbLinesThickness.Enabled

            gbColors.Enabled = (.FFTStyle <> DXVUMeterNETGDI.FFTStyleConstants.Spectrum)
            btnRedOff.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.LedBars)
            btnRedOn.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.LedBars)

            cmbFFTLineStyleChannelMode.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line OrElse .FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Filled)

            tbPlotNoiseReduction.Enabled = (.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line OrElse .FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Filled)
        End With
    End Sub

    Private Sub frmFFTOptions_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub chkHoldMinPeaks_CheckedChanged(sender As Object, e As EventArgs) Handles chkHoldMinPeaks.CheckedChanged
        dxvuCtrl.FFTHoldMinPeaks = chkHoldMinPeaks.Checked
        SetUpUI()
    End Sub

    Private Sub cmbFFTYScale_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbFFTYScale.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub cmbFFTYScale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFFTYScale.SelectedIndexChanged
        dxvuCtrl.FFTYScale = cmbFFTYScale.SelectedItem
    End Sub

    Private Sub chkHoldMaxPeaks_CheckedChanged(sender As Object, e As EventArgs) Handles chkHoldMaxPeaks.CheckedChanged
        dxvuCtrl.FFTHoldMaxPeaks = chkHoldMaxPeaks.Checked
        SetUpUI()
    End Sub

    Private Sub chkShowMinMaxRange_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowMinMaxRange.CheckedChanged
        dxvuCtrl.FFTShowMinMaxRange = chkShowMinMaxRange.Checked
        SetUpUI()
    End Sub

    Private Sub btnGreenOff_Click(sender As Object, e As EventArgs) Handles btnGreenOff.Click
        ChangeColor(btnGreenOff)
    End Sub

    Private Sub ChangeColor(ByVal btn As Button)
        Dim dlgColor As New ColorDialog
        With dlgColor
            .AllowFullOpen = True
            .AnyColor = True
            .FullOpen = True
            .Color = btn.BackColor
            .ShowDialog()
            btn.BackColor = .Color
        End With

        Select Case btn.Name
            Case "btnYellowOff" : dxvuCtrl.YellowOff = btn.BackColor
            Case "btnYellowOn" : dxvuCtrl.YellowOn = btn.BackColor
            Case "btnGreenOff" : dxvuCtrl.GreenOff = btn.BackColor
            Case "btnGreenOn" : dxvuCtrl.GreenOn = btn.BackColor
            Case "btnRedOff" : dxvuCtrl.RedOff = btn.BackColor
            Case "btnRedOn" : dxvuCtrl.RedOn = btn.BackColor
        End Select
    End Sub

    Private Sub btnGreenOn_Click(sender As Object, e As EventArgs) Handles btnGreenOn.Click
        ChangeColor(btnGreenOn)
    End Sub

    Private Sub btnYellowOff_Click(sender As Object, e As EventArgs) Handles btnYellowOff.Click
        ChangeColor(btnYellowOff)
    End Sub

    Private Sub btnYellowOn_Click(sender As Object, e As EventArgs) Handles btnYellowOn.Click
        ChangeColor(btnYellowOn)
    End Sub

    Private Sub chkShowDecay_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDecay.CheckedChanged
        dxvuCtrl.FFTShowDecay = chkShowDecay.Checked
        SetUpUI()
    End Sub

    Private Sub btnRedOff_Click(sender As Object, e As EventArgs) Handles btnRedOff.Click
        ChangeColor(btnRedOff)
    End Sub

    Private Sub btnRedOn_Click(sender As Object, e As EventArgs) Handles btnRedOn.Click
        ChangeColor(btnRedOn)
    End Sub

    Private Sub sbFFTSmoothing_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbFFTSmoothing.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub sbFFTSmoothing_ValueChanged(sender As Object, e As EventArgs) Handles tbFFTSmoothing.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub DTMFToneDetected(ByVal tone As DXVUMeterNETGDI.DTMFToneConstants)
        Dim c As String = ""

        If tone <> DXVUMeterNETGDI.DTMFToneConstants.DTMFInvalid Then
            c = DXVUMeterNETGDI.DTMFToneToValue(tone)
        End If

        txtDTMFTones.Text += c
    End Sub

    Private Sub frmFFTOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        isAltDown = e.Alt
    End Sub

    Private Sub frmFFTOptions_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        isAltDown = e.Alt
    End Sub

    Private Sub frmFFTOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler dxvuCtrl.DTMFToneDown, AddressOf DTMFToneDetected
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtDTMFTones.Clear()
    End Sub

    Private Sub chkDTMFDetect_CheckedChanged(sender As Object, e As EventArgs) Handles chkDTMFDetect.CheckedChanged
        txtDTMFTones.Enabled = chkDTMFDetect.Checked
        txtDTMFTones.BackColor = IIf(chkDTMFDetect.Checked, Color.FromKnownColor(KnownColor.Window), Color.FromKnownColor(KnownColor.Control))
        dxvuCtrl.FFTDetectDTMF = chkDTMFDetect.Checked
    End Sub

    Private Sub tbLinesThickness_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbLinesThickness.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub tbLinesThickness_ValueChanged(sender As Object, e As EventArgs) Handles tbLinesThickness.ValueChanged
        dxvuCtrl.LinesThickness = tbLinesThickness.Value
    End Sub

    Private Sub tbDecayDelay_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbDecayDelay.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub tbDecayDelay_ValueChanged(sender As Object, e As EventArgs) Handles tbDecayDelay.ValueChanged
        If tbDecayDelay.Value = 100 Then
            dxvuCtrl.FFTPeaksDecayDelay = Integer.MaxValue
        Else
            dxvuCtrl.FFTPeaksDecayDelay = tbDecayDelay.Value
        End If
    End Sub

    Private Sub tbDecaySpeed_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbDecaySpeed.MouseWheel
        If isAltDown Then frmFFTOptions_MouseWheel(Me, e)
    End Sub

    Private Sub tbDecaySpeed_ValueChanged(sender As Object, e As EventArgs) Handles tbDecaySpeed.ValueChanged
        dxvuCtrl.FFTPeaksDecaySpeed = tbDecayDelay.Maximum - tbDecaySpeed.Value
    End Sub

    Private Sub btnColors1_Click(sender As Object, e As EventArgs) Handles btnColors1.Click
        btnGreenOff.BackColor = Color.FromArgb(255, 0, 128, 0)
        btnGreenOn.BackColor = Color.FromArgb(255, 0, 255, 0)

        btnYellowOff.BackColor = Color.FromArgb(255, 128, 128, 0)
        btnYellowOn.BackColor = Color.FromArgb(255, 255, 255, 0)

        btnRedOff.BackColor = Color.FromArgb(255, 128, 0, 0)
        btnRedOn.BackColor = Color.FromArgb(255, 255, 0, 0)

        RestoreColors()
    End Sub

    Private Sub btnColors2_Click(sender As Object, e As EventArgs) Handles btnColors2.Click
        btnGreenOff.BackColor = Color.FromArgb(255, 255, 0, 0)
        btnGreenOn.BackColor = Color.FromArgb(255, 255, 255, 0)

        btnYellowOff.BackColor = Color.FromArgb(255, 0, 115, 170)
        btnYellowOn.BackColor = Color.FromArgb(255, 0, 255, 255)

        chkHoldMaxPeaks.Checked = True
        chkHoldMinPeaks.Checked = True
        chkShowMinMaxRange.Checked = True

        tbLinesThickness.Value = 2
        tbFFTSmoothing.Value = 3
        chkFFTLogScale.Checked = True
        chkFFTZoom.Checked = True
        cmbFFTStyle.SelectedIndex = 0
        cmbFFTLineStyleChannelMode.SelectedIndex = 0

        RestoreColors()
    End Sub

    Private Sub btnColors3_Click(sender As Object, e As EventArgs) Handles btnColors3.Click
        btnGreenOff.BackColor = Color.FromArgb(255, 128, 128, 128)
        btnGreenOn.BackColor = Color.FromArgb(255, 255, 255, 255)

        btnYellowOff.BackColor = Color.FromArgb(255, 128, 128, 128)
        btnYellowOn.BackColor = Color.FromArgb(255, 255, 255, 255)

        RestoreColors()
    End Sub

    Private Sub frmFFTOptions_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If isAltDown Then
            Select Case e.Delta
                Case Is > 0
                    Me.Opacity = Math.Min(Me.Opacity + 0.1, 1)
                Case Is < 0
                    Me.Opacity = Math.Max(Me.Opacity - 0.1, 0.2)
            End Select
        End If
    End Sub

    Private Sub cmbFFTLineStyleChannelMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFFTLineStyleChannelMode.SelectedIndexChanged
        dxvuCtrl.FFTLineChannelMode = cmbFFTLineStyleChannelMode.SelectedItem
    End Sub

    Private Sub sbFFTHistorySize_ValueChanged(sender As Object, e As EventArgs) Handles tbFFTHistorySize.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub sbWAVHistorySize_ValueChanged(sender As Object, e As EventArgs) Handles tbOverlap.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub tbPlotNoiseReduction_ValueChanged(sender As Object, e As EventArgs) Handles tbPlotNoiseReduction.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub tbNoiseAtt_ValueChanged(sender As Object, e As EventArgs) Handles tbNoiseAtt.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub btnFilters_Click(sender As Object, e As EventArgs) Handles btnFilters.Click
        Using dlg As New frmFilters()
            dlg.ShowDialog(Me)
        End Using
    End Sub

    Private Sub tbYScale_ValueChanged(sender As Object, e As EventArgs) Handles tbYScale.ValueChanged
        ApplyFFTSliders()
    End Sub

    Private Sub chkNormalized_CheckedChanged(sender As Object, e As EventArgs) Handles chkNormalized.CheckedChanged
        dxvuCtrl.FFTNormalized = chkNormalized.Checked
    End Sub

    Private Sub chkHighPrecisionMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkHighPrecisionMode.CheckedChanged
        dxvuCtrl.FFTHighPrecisionMode = chkHighPrecisionMode.Checked
    End Sub
End Class
