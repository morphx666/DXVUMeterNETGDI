Imports NDXVUMeterNET
Imports System.ComponentModel

Public Class frmMain
    Inherits Form

    Dim IsChangingPos As Boolean
    Dim fftOptionsDlg As frmFFTOptions
    Dim digitalVUOptionsDlg As frmDigitalVUOptions
    Dim oscOptionsDlg As frmOscOptions
    Dim gdiOptionsDlg As frmCustomGDIOptions
    Dim mixerDialog As frmMixer
    Dim ctrlIsFullScreen As Boolean

    Dim lastPeak As DXVUMeterNETGDI.Peak

    'Dim msgFont As D3D.Font
    Dim msgSize As Size
    Friend WithEvents tbPBVol As TrackBar
    Friend WithEvents lblInfo As Label

    Friend WithEvents pCtrlBack As Panel

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        Try
            'This call is required by the Windows Form Designer.
            InitializeComponent()
        Catch ex As Exception
            MsgBox("Failed Component Initialization: " + ex.Message)
        End Try

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
    Friend WithEvents DxvuMeterNET1 As DXVUMeterNETGDI
    Friend WithEvents tbbSep04 As ToolBarButton
    Friend WithEvents tbbMixer As ToolBarButton
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbDevices As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbCapabilities As ComboBox
    Friend WithEvents cmbStyle As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnFFTOptions As Button
    Friend WithEvents tbControls As ToolBar
    Friend WithEvents ilIcons As ImageList
    Friend WithEvents tbbPower As ToolBarButton
    Friend WithEvents tbbSep01 As ToolBarButton
    Friend WithEvents tbbPlay As ToolBarButton
    Friend WithEvents tbbRec As ToolBarButton
    Friend WithEvents tbbStop As ToolBarButton
    Friend WithEvents tbbPause As ToolBarButton
    Friend WithEvents tbbSep02 As ToolBarButton
    Friend WithEvents tbbSep03 As ToolBarButton
    Friend WithEvents gbPlayback As GroupBox
    Friend WithEvents tbPlaybackPos As TrackBar
    Friend WithEvents lblPlayback As Label

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New Container()
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(frmMain))
        Me.Label1 = New Label()
        Me.cmbCapabilities = New ComboBox()
        Me.Label2 = New Label()
        Me.cmbStyle = New ComboBox()
        Me.Label3 = New Label()
        Me.btnFFTOptions = New Button()
        Me.tbControls = New ToolBar()
        Me.tbbPower = New ToolBarButton()
        Me.tbbSep01 = New ToolBarButton()
        Me.tbbPlay = New ToolBarButton()
        Me.tbbPause = New ToolBarButton()
        Me.tbbSep02 = New ToolBarButton()
        Me.tbbRec = New ToolBarButton()
        Me.tbbSep03 = New ToolBarButton()
        Me.tbbStop = New ToolBarButton()
        Me.tbbSep04 = New ToolBarButton()
        Me.tbbMixer = New ToolBarButton()
        Me.ilIcons = New ImageList(Me.components)
        Me.gbPlayback = New GroupBox()
        Me.tbPBVol = New TrackBar()
        Me.tbPlaybackPos = New TrackBar()
        Me.lblPlayback = New Label()
        Me.cmbDevices = New ComboBox()
        Me.DxvuMeterNET1 = New DXVUMeterNETGDI()
        Me.pCtrlBack = New Panel()
        Me.lblInfo = New Label()
        Me.gbPlayback.SuspendLayout()
        CType(Me.tbPBVol, ISupportInitialize).BeginInit()
        CType(Me.tbPlaybackPos, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New Point(7, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Size(222, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Devices"
        '
        'cmbCapabilities
        '
        Me.cmbCapabilities.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbCapabilities.Location = New Point(7, 120)
        Me.cmbCapabilities.Name = "cmbCapabilities"
        Me.cmbCapabilities.Size = New Size(221, 21)
        Me.cmbCapabilities.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.Location = New Point(7, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New Size(154, 14)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Supported Monitoring Quality"
        '
        'cmbStyle
        '
        Me.cmbStyle.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbStyle.Location = New Point(238, 80)
        Me.cmbStyle.Name = "cmbStyle"
        Me.cmbStyle.Size = New Size(131, 21)
        Me.cmbStyle.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.Location = New Point(238, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New Size(66, 14)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Style"
        '
        'btnFFTOptions
        '
        Me.btnFFTOptions.Location = New Point(238, 120)
        Me.btnFFTOptions.Name = "btnFFTOptions"
        Me.btnFFTOptions.Size = New Size(131, 21)
        Me.btnFFTOptions.TabIndex = 29
        Me.btnFFTOptions.Text = "FFT Options..."
        '
        'tbControls
        '
        Me.tbControls.Appearance = ToolBarAppearance.Flat
        Me.tbControls.BorderStyle = BorderStyle.Fixed3D
        Me.tbControls.Buttons.AddRange(New ToolBarButton() {Me.tbbPower, Me.tbbSep01, Me.tbbPlay, Me.tbbPause, Me.tbbSep02, Me.tbbRec, Me.tbbSep03, Me.tbbStop, Me.tbbSep04, Me.tbbMixer})
        Me.tbControls.Divider = False
        Me.tbControls.DropDownArrows = True
        Me.tbControls.ImageList = Me.ilIcons
        Me.tbControls.Location = New Point(0, 0)
        Me.tbControls.Name = "tbControls"
        Me.tbControls.ShowToolTips = True
        Me.tbControls.Size = New Size(672, 58)
        Me.tbControls.TabIndex = 33
        Me.tbControls.Wrappable = False
        '
        'tbbPower
        '
        Me.tbbPower.ImageIndex = 0
        Me.tbbPower.Name = "tbbPower"
        Me.tbbPower.Text = "Power"
        '
        'tbbSep01
        '
        Me.tbbSep01.Name = "tbbSep01"
        Me.tbbSep01.Style = ToolBarButtonStyle.Separator
        '
        'tbbPlay
        '
        Me.tbbPlay.ImageIndex = 2
        Me.tbbPlay.Name = "tbbPlay"
        Me.tbbPlay.Text = "Play"
        '
        'tbbPause
        '
        Me.tbbPause.ImageIndex = 6
        Me.tbbPause.Name = "tbbPause"
        Me.tbbPause.Style = ToolBarButtonStyle.ToggleButton
        Me.tbbPause.Text = "Pause"
        '
        'tbbSep02
        '
        Me.tbbSep02.Name = "tbbSep02"
        Me.tbbSep02.Style = ToolBarButtonStyle.Separator
        '
        'tbbRec
        '
        Me.tbbRec.ImageIndex = 4
        Me.tbbRec.Name = "tbbRec"
        Me.tbbRec.Text = "Record"
        '
        'tbbSep03
        '
        Me.tbbSep03.Name = "tbbSep03"
        Me.tbbSep03.Style = ToolBarButtonStyle.Separator
        '
        'tbbStop
        '
        Me.tbbStop.ImageIndex = 3
        Me.tbbStop.Name = "tbbStop"
        Me.tbbStop.Text = "Stop"
        '
        'tbbSep04
        '
        Me.tbbSep04.Name = "tbbSep04"
        Me.tbbSep04.Style = ToolBarButtonStyle.Separator
        '
        'tbbMixer
        '
        Me.tbbMixer.ImageIndex = 7
        Me.tbbMixer.Name = "tbbMixer"
        Me.tbbMixer.Text = "Mixer"
        '
        'ilIcons
        '
        Me.ilIcons.ImageStream = CType(resources.GetObject("ilIcons.ImageStream"), ImageListStreamer)
        Me.ilIcons.TransparentColor = Color.Transparent
        Me.ilIcons.Images.SetKeyName(0, "")
        Me.ilIcons.Images.SetKeyName(1, "")
        Me.ilIcons.Images.SetKeyName(2, "")
        Me.ilIcons.Images.SetKeyName(3, "")
        Me.ilIcons.Images.SetKeyName(4, "")
        Me.ilIcons.Images.SetKeyName(5, "")
        Me.ilIcons.Images.SetKeyName(6, "")
        Me.ilIcons.Images.SetKeyName(7, "icon32x32.ico")
        '
        'gbPlayback
        '
        Me.gbPlayback.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) _
            Or AnchorStyles.Right), AnchorStyles)
        Me.gbPlayback.Controls.Add(Me.tbPBVol)
        Me.gbPlayback.Controls.Add(Me.tbPlaybackPos)
        Me.gbPlayback.Controls.Add(Me.lblPlayback)
        Me.gbPlayback.Enabled = False
        Me.gbPlayback.Location = New Point(378, 64)
        Me.gbPlayback.Name = "gbPlayback"
        Me.gbPlayback.Size = New Size(286, 78)
        Me.gbPlayback.TabIndex = 36
        Me.gbPlayback.TabStop = False
        Me.gbPlayback.Text = "Playback"
        '
        'tbPBVol
        '
        Me.tbPBVol.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.tbPBVol.Location = New Point(232, 10)
        Me.tbPBVol.Maximum = 100
        Me.tbPBVol.Minimum = -100
        Me.tbPBVol.Name = "tbPBVol"
        Me.tbPBVol.Orientation = Orientation.Vertical
        Me.tbPBVol.Size = New Size(45, 66)
        Me.tbPBVol.TabIndex = 3
        Me.tbPBVol.TickFrequency = 50
        Me.tbPBVol.TickStyle = TickStyle.Both
        '
        'tbPlaybackPos
        '
        Me.tbPlaybackPos.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) _
            Or AnchorStyles.Right), AnchorStyles)
        Me.tbPlaybackPos.AutoSize = False
        Me.tbPlaybackPos.Location = New Point(4, 42)
        Me.tbPlaybackPos.Name = "tbPlaybackPos"
        Me.tbPlaybackPos.Size = New Size(222, 28)
        Me.tbPlaybackPos.TabIndex = 2
        '
        'lblPlayback
        '
        Me.lblPlayback.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) _
            Or AnchorStyles.Right), AnchorStyles)
        Me.lblPlayback.Font = New Font("Courier New", 20.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayback.Location = New Point(11, 18)
        Me.lblPlayback.Name = "lblPlayback"
        Me.lblPlayback.Size = New Size(270, 31)
        Me.lblPlayback.TabIndex = 0
        Me.lblPlayback.Text = "00:00:00"
        Me.lblPlayback.TextAlign = ContentAlignment.MiddleCenter
        Me.lblPlayback.UseMnemonic = False
        '
        'cmbDevices
        '
        Me.cmbDevices.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbDevices.Location = New Point(7, 80)
        Me.cmbDevices.Name = "cmbDevices"
        Me.cmbDevices.Size = New Size(221, 21)
        Me.cmbDevices.TabIndex = 7
        '
        'DxvuMeterNET1
        '
        Me.DxvuMeterNET1.BackColor = Color.Black
        Me.DxvuMeterNET1.BitDepth = CType(16, Short)
        Me.DxvuMeterNET1.Channels = CType(2, Short)
        Me.DxvuMeterNET1.EnableRendering = True
        Me.DxvuMeterNET1.FFTDetectDTMF = False
        Me.DxvuMeterNET1.FFTHighPrecisionMode = False
        Me.DxvuMeterNET1.FFTHistorySize = 4
        Me.DxvuMeterNET1.FFTHoldMaxPeaks = False
        Me.DxvuMeterNET1.FFTHoldMinPeaks = False
        Me.DxvuMeterNET1.FFTLineChannelMode = DXVUMeterNETGDI.FFTLineChannelModeConstants.Normal
        Me.DxvuMeterNET1.FFTNormalized = True
        Me.DxvuMeterNET1.FFTPeaksDecayDelay = 10
        Me.DxvuMeterNET1.FFTPeaksDecaySpeed = 20
        Me.DxvuMeterNET1.FFTPlotNoiseReduction = 0
        Me.DxvuMeterNET1.FFTRenderScales = DXVUMeterNETGDI.FFTRenderScalesConstants.Both
        Me.DxvuMeterNET1.FFTScaleFont = New Font("Tahoma", 8.0!)
        Me.DxvuMeterNET1.FFTShowDecay = False
        Me.DxvuMeterNET1.FFTShowMinMaxRange = False
        Me.DxvuMeterNET1.FFTSize = FFTSizeConstants.FFTs512
        Me.DxvuMeterNET1.FFTSmoothing = 0
        Me.DxvuMeterNET1.FFTStyle = DXVUMeterNETGDI.FFTStyleConstants.Line
        Me.DxvuMeterNET1.FFTWindow = FFTWindowConstants.None
        Me.DxvuMeterNET1.FFTXMax = 22050
        Me.DxvuMeterNET1.FFTXMin = 0
        Me.DxvuMeterNET1.FFTXScale = DXVUMeterNETGDI.FFTXScaleConstants.Normal
        Me.DxvuMeterNET1.FFTXZoom = False
        Me.DxvuMeterNET1.FFTXZoomWindowPos = 0
        Me.DxvuMeterNET1.FFTYScale = DXVUMeterNETGDI.FFTYScaleConstants.dB
        Me.DxvuMeterNET1.FFTYScaleMultiplier = 1.0R
        Me.DxvuMeterNET1.Frequency = 44100
        Me.DxvuMeterNET1.GreenOff = Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.GreenOn = Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.LeftChannelMute = False
        Me.DxvuMeterNET1.LinesThickness = 1
        Me.DxvuMeterNET1.Location = New Point(0, 159)
        Me.DxvuMeterNET1.Name = "DxvuMeterNET1"
        Me.DxvuMeterNET1.NoiseFilter = 0
        Me.DxvuMeterNET1.NumVUs = CType(42, Short)
        Me.DxvuMeterNET1.Orientation = DXVUMeterNETGDI.OrientationConstants.Horizontal
        Me.DxvuMeterNET1.Overlap = 0.0R
        Me.DxvuMeterNET1.PlaybackPosition = CType(0, Long)
        Me.DxvuMeterNET1.PlaybackTime = ""
        Me.DxvuMeterNET1.PlaybackVolume = CType(0, Short)
        Me.DxvuMeterNET1.RedOff = Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.RedOn = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.RightChannelMute = False
        Me.DxvuMeterNET1.Size = New Size(672, 230)
        Me.DxvuMeterNET1.Style = DXVUMeterNETGDI.StyleConstants.DigitalVU
        Me.DxvuMeterNET1.TabIndex = 0
        Me.DxvuMeterNET1.YellowOff = Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DxvuMeterNET1.YellowOn = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'pCtrlBack
        '
        Me.pCtrlBack.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) _
            Or AnchorStyles.Right), AnchorStyles)
        Me.pCtrlBack.BorderStyle = BorderStyle.Fixed3D
        Me.pCtrlBack.Location = New Point(0, 154)
        Me.pCtrlBack.Name = "pCtrlBack"
        Me.pCtrlBack.Size = New Size(673, 4)
        Me.pCtrlBack.TabIndex = 34
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.lblInfo.BackColor = SystemColors.Info
        Me.lblInfo.ForeColor = Color.Red
        Me.lblInfo.Location = New Point(290, 3)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New Size(380, 52)
        Me.lblInfo.TabIndex = 37
        Me.lblInfo.Text = resources.GetString("lblInfo.Text")
        Me.lblInfo.TextAlign = ContentAlignment.MiddleLeft
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New Size(5, 14)
        Me.ClientSize = New Size(672, 389)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.DxvuMeterNET1)
        Me.Controls.Add(Me.gbPlayback)
        Me.Controls.Add(Me.pCtrlBack)
        Me.Controls.Add(Me.tbControls)
        Me.Controls.Add(Me.btnFFTOptions)
        Me.Controls.Add(Me.cmbStyle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbCapabilities)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbDevices)
        Me.Controls.Add(Me.Label1)
        Me.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Me.MinimumSize = New Size(562, 239)
        Me.Name = "frmMain"
        Me.SizeGripStyle = SizeGripStyle.Hide
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "DXVU Meter Tester"
        Me.gbPlayback.ResumeLayout(False)
        Me.gbPlayback.PerformLayout()
        CType(Me.tbPBVol, ISupportInitialize).EndInit()
        CType(Me.tbPlaybackPos, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

#End Region

    Private Sub cmbCapabilities_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCapabilities.SelectedIndexChanged
        Dim q As DXVUMeterNETGDI.DevicesCollection.Device.QualitiesCollection.Quality = CType(cmbCapabilities.SelectedItem, DXVUMeterNETGDI.DevicesCollection.Device.QualitiesCollection.Quality)
        With DxvuMeterNET1
            .Frequency = q.Frequency
            .BitDepth = q.BitDepth
            .Channels = q.Channels
        End With

        fftOptionsDlg.SetFFTZoomParams()
    End Sub

    Private Sub cmbDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDevices.SelectedIndexChanged
        Dim d As DXVUMeterNETGDI.DevicesCollection.Device = cmbDevices.SelectedItem
        Dim q As DXVUMeterNETGDI.DevicesCollection.Device.QualitiesCollection.Quality

        DxvuMeterNET1.Devices.SelectedDevice = d

        cmbCapabilities.Items.Clear()
        For Each q In d.Qualities
            cmbCapabilities.Items.Add(q)
        Next

        For i As Integer = cmbCapabilities.Items.Count - 1 To 0 Step -1
            If cmbCapabilities.Items(i).BitDepth = 16 And cmbCapabilities.Items(i).Channels = 2 Then
                cmbCapabilities.SelectedIndex = i
                Exit For
            End If
        Next

        If cmbCapabilities.SelectedIndex = -1 Then cmbCapabilities.SelectedIndex = cmbCapabilities.Items.Count - 1
        mixerDialog.CreateMixer()
    End Sub

    Private Sub cmbStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStyle.SelectedIndexChanged
        DxvuMeterNET1.Style = CType(cmbStyle.SelectedItem, DXVUMeterNETGDI.StyleConstants)
        btnFFTOptions.Text = cmbStyle.Text + " Options..."

        Select Case DxvuMeterNET1.Style
            Case DXVUMeterNETGDI.StyleConstants.DigitalVU
                digitalVUOptionsDlg.RestoreColors()
                DxvuMeterNET1.BackColor = Me.BackColor
            Case DXVUMeterNETGDI.StyleConstants.Oscilloscope
                oscOptionsDlg.RestoreColors()
                DxvuMeterNET1.BackColor = Color.Black
            Case DXVUMeterNETGDI.StyleConstants.FFT
                fftOptionsDlg.RestoreColors()
                DxvuMeterNET1.BackColor = Color.Black
        End Select
    End Sub

    Private Sub btnFFTOptions_Click(sender As Object, e As EventArgs) Handles btnFFTOptions.Click
        Dim s As DXVUMeterNETGDI.StyleConstants = cmbStyle.SelectedItem
        Select Case s
            Case DXVUMeterNETGDI.StyleConstants.DigitalVU
                digitalVUOptionsDlg.Visible = Not digitalVUOptionsDlg.Visible
            Case DXVUMeterNETGDI.StyleConstants.FFT
                fftOptionsDlg.Visible = Not fftOptionsDlg.Visible
            Case DXVUMeterNETGDI.StyleConstants.Oscilloscope
                oscOptionsDlg.Visible = Not oscOptionsDlg.Visible
            Case DXVUMeterNETGDI.StyleConstants.UserPaintGDI
                gdiOptionsDlg.Visible = Not gdiOptionsDlg.Visible
        End Select
    End Sub

    Private Sub tbControls_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles tbControls.ButtonClick
        Dim pFileName As String = ""
        Dim tbb As ToolBarButton = e.Button
        Select Case tbb.Text
            Case "Power"
                Select Case DxvuMeterNET1.MonitoringState
                    Case DXVUMeterNETGDI.MonitoringStateConstants.Idle
                        DxvuMeterNET1.StartMonitoring()
                        If DxvuMeterNET1.State = DXVUMeterNETGDI.PlaybackStateConstants.Idle Then Exit Sub
                        e.Button.ImageIndex = 1
                    Case DXVUMeterNETGDI.MonitoringStateConstants.Monitoring
                        DxvuMeterNET1.StopMonitoring()
                        e.Button.ImageIndex = 0
                End Select
            Case "Play"
                Dim dlgFile As New OpenFileDialog
                With dlgFile
                    .AddExtension = True
                    .Filter = "WAV Files (*.wav)|*.wav"
                    .Title = "Select Media File"
                    If .ShowDialog = Windows.Forms.DialogResult.OK Then pFileName = .FileName
                End With

                DxvuMeterNET1.StartPlaying(pFileName)
                If DxvuMeterNET1.PlaybackState = DXVUMeterNETGDI.PlaybackStateConstants.Playing Then
                    With tbPlaybackPos
                        .Value = 0
                        .TickFrequency = 10
                        .Minimum = 0
                        .Maximum = 100
                    End With
                    gbPlayback.Text = "Playback"
                Else
                    MsgBox(String.Format("An error has occurred while trying to play the file '{0}'", pFileName), MsgBoxStyle.Information, "Error Playing File")
                End If
            Case "Pause"
                DxvuMeterNET1.PausePlaying()
            Case "Record"
                Dim dlgFile As New SaveFileDialog
                With dlgFile
                    .AddExtension = True
                    .Filter = "WAV Files (*.wav)|*.wav|MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*"
                    .Title = "Select Media File"
                    .OverwritePrompt = False
                    If .ShowDialog = Windows.Forms.DialogResult.OK Then
                        pFileName = .FileName

                        Dim DoOverwrite As Boolean = True
                        If pFileName.EndsWith(".mp3", StringComparison.CurrentCultureIgnoreCase) Then
                            If IO.File.Exists(.FileName) Then
                                If MsgBox("File already exists. Would you like to overwrite it?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Resume Recording") = MsgBoxResult.No Then Exit Sub
                            End If

                            Dim mp3Settings As DXVUMeterNETGDI.MP3EncoderConfiguration = New DXVUMeterNETGDI.MP3EncoderConfiguration With {
                                .BitRate = DXVUMeterNETGDI.MP3EncoderConfiguration.BitRateConstants.VBR_AverageBitRate,
                                .MaxBitrate = DXVUMeterNETGDI.MP3EncoderConfiguration.BitRateConstants.CBR320,
                                .AverageBitrate = DXVUMeterNETGDI.MP3EncoderConfiguration.BitRateConstants.CBR128
                            }

                            'MsgBox(DXVUMeterNETGDI.MP3EncoderConfiguration.EncoderVersion)

                            DxvuMeterNET1.StartRecording(pFileName, , DXVUMeterNETGDI.RecordingFormatConstants.MP3, mp3Settings)
                        Else
                            If IO.File.Exists(.FileName) Then
                                DoOverwrite = (MsgBox("Would you like to resume recording on the selected file or do you want to overwrite it?" + vbCrLf + "Answer Yes if you want to resume or No if you want to overwrite it", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Resume Recording") = MsgBoxResult.No)
                            End If

                            DxvuMeterNET1.StartRecording(pFileName, DoOverwrite, DXVUMeterNETGDI.RecordingFormatConstants.WAV)
                        End If

                        If dxvuCtrl.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Recording Then
                            gbPlayback.Text = "Recording"
                        Else
                            If MsgBox(String.Format("An error has occurred while trying to record.{0}If you selected to save the audio stream as an MP3 file make sure you have a valid LAME encoder available.{0}Also, make sure you select a sampling rate that is supported for your version of LAME. Usually, 44Khz 16bits Stereo should work on all versions.{0}{0}You can download the LAME encoder from http://www.free-codecs.com/Lame_Encoder_download.htm{0}{0}Would you like to download the encoder now?", vbCrLf), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Error Recording") = MsgBoxResult.Yes Then
                                Process.Start("http://www.free-codecs.com/Lame_Encoder_download.htm")
                                MsgBox(String.Format("Once you have downloaded the encoder, simply copy the 'lame_enc.dll' file to your '{0}\system32\' directory and DXVUMeterNET will then be able to record streams in MP3 format", Environ("windir")), MsgBoxStyle.Information, "Encoder Installation")
                            End If
                        End If
                    End If
                End With
            Case "Stop"
                If DxvuMeterNET1.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Recording Then DxvuMeterNET1.StopRecording()
                If DxvuMeterNET1.PlaybackState <> DXVUMeterNETGDI.PlaybackStateConstants.Idle Then DxvuMeterNET1.StopPlaying()
            Case "Mixer"
                mixerDialog.Visible = Not mixerDialog.Visible
        End Select

        UpdateControlsState()
    End Sub

    Private Sub UpdateControlsState()
        cmbCapabilities.Enabled = cmbCapabilities.Items.Count > 0 AndAlso DxvuMeterNET1.MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.Idle
        cmbDevices.Enabled = cmbDevices.Items.Count > 0 AndAlso DxvuMeterNET1.MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.Idle

        ' Power
        tbbPower.Enabled = DxvuMeterNET1.PlaybackState = DXVUMeterNETGDI.PlaybackStateConstants.Idle And
                                        DxvuMeterNET1.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Idle

        ' Play
        tbbPlay.Enabled = DxvuMeterNET1.MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.Monitoring And
                                        DxvuMeterNET1.PlaybackState = DXVUMeterNETGDI.PlaybackStateConstants.Idle And
                                        DxvuMeterNET1.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Idle

        ' Pause
        tbbPause.Enabled = DxvuMeterNET1.MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.Monitoring And
                                        DxvuMeterNET1.PlaybackState <> DXVUMeterNETGDI.PlaybackStateConstants.Idle
        tbbPause.Pushed = DxvuMeterNET1.PlaybackState = DXVUMeterNETGDI.PlaybackStateConstants.Paused

        ' Record
        tbbRec.Enabled = DxvuMeterNET1.MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.Monitoring And
                                        DxvuMeterNET1.PlaybackState = DXVUMeterNETGDI.PlaybackStateConstants.Idle And
                                        DxvuMeterNET1.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Idle

        ' Stop
        tbbStop.Enabled = DxvuMeterNET1.MonitoringState = DXVUMeterNETGDI.MonitoringStateConstants.Monitoring And
                                        (DxvuMeterNET1.PlaybackState <> DXVUMeterNETGDI.PlaybackStateConstants.Idle Or
                                        DxvuMeterNET1.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Recording)

        gbPlayback.Enabled = DxvuMeterNET1.PlaybackState <> DXVUMeterNETGDI.PlaybackStateConstants.Idle
    End Sub

    Private Sub DxvuMeterNET1_ControlIsReady() Handles DxvuMeterNET1.ControlIsReady
        Dim i As Integer

        dxvuCtrl = Me.DxvuMeterNET1

        ' Uncomment the following line and replace it with the one you received via email
        '   with your own registration information.
        'This will remove the DEMO message in DXVUMeterNET
        'DxvuMeterNET1.LicenseControl("LicenseCode", "SerialNumber or OrderNumber")

        mixerDialog = New frmMixer()

        DxvuMeterNET1.FFTPeaksDecayDelay = 8
        DxvuMeterNET1.FFTPeaksDecaySpeed = 10
        DxvuMeterNET1.FFTXScale = DXVUMeterNETGDI.FFTXScaleConstants.Logarithmic
        DxvuMeterNET1.FFTXZoom = True

        DxvuMeterNET1.InternalGain = 1.0

        fftOptionsDlg = New frmFFTOptions()
        fftOptionsDlg.InitFFTOptions()

        digitalVUOptionsDlg = New frmDigitalVUOptions
        digitalVUOptionsDlg.InitDlg()

        oscOptionsDlg = New frmOscOptions()
        oscOptionsDlg.InitDlg()

        With CustomGDIColors
            .GreenOff = DxvuMeterNET1.GreenOff
            .GreenOn = DxvuMeterNET1.GreenOn
            .YellowOff = DxvuMeterNET1.YellowOff
            .YellowOn = DxvuMeterNET1.YellowOn
            .RedOff = DxvuMeterNET1.RedOff
            .RedOn = DxvuMeterNET1.RedOn
        End With
        gdiOptionsDlg = New frmCustomGDIOptions()
        gdiOptionsDlg.InitDlg()

        For Each d As DXVUMeterNETGDI.DevicesCollection.Device In DxvuMeterNET1.Devices
            i = cmbDevices.Items.Add(d)
            If d.Selected Then cmbDevices.SelectedIndex = i
        Next
        If cmbDevices.Items.Count > 0 Then
            If cmbDevices.SelectedIndex = -1 Then cmbDevices.SelectedIndex = 0
        End If

        For Each k As Object In [Enum].GetValues(GetType(DXVUMeterNETGDI.StyleConstants))
            cmbStyle.Items.Add(k)
        Next
        cmbStyle.SelectedIndex = 0

        DxvuMeterNET1.FFTScaleFont = New Font("Consolas", 8, GraphicsUnit.Pixel)

        UpdateControlsState()
    End Sub

    Private Sub DxvuMeterNET1_PeakValues(AudioBuffer() As Byte, ByVal NormalizedAudioBuffer() As Integer, ByVal MaxPeak As DXVUMeterNETGDI.Peak) Handles DxvuMeterNET1.PeakValues
        lastPeak = MaxPeak

        Me.Invoke(New MethodInvoker(Sub()
                                        If DxvuMeterNET1.PlaybackState = DXVUMeterNETGDI.PlaybackStateConstants.Playing Then
                                            lblPlayback.Text = DxvuMeterNET1.PlaybackTime
                                            If Not IsChangingPos Then
                                                tbPlaybackPos.Value = (DxvuMeterNET1.PlaybackPosition / DxvuMeterNET1.PlaybackPositionTotal) * 100
                                            End If
                                        End If

                                        If DxvuMeterNET1.RecordingState = DXVUMeterNETGDI.RecordingStateConstants.Recording Then
                                            lblPlayback.Text = DxvuMeterNET1.RecordingTime
                                        End If
                                    End Sub))
    End Sub

    Private Sub tbPlaybackPos_MouseUp(sender As Object, e As MouseEventArgs) Handles tbPlaybackPos.MouseUp
        DxvuMeterNET1.PlaybackPosition = DxvuMeterNET1.PlaybackPositionTotal * (tbPlaybackPos.Value / 100)
        IsChangingPos = False
    End Sub

    Private Sub tbPlaybackPos_MouseDown(sender As Object, e As MouseEventArgs) Handles tbPlaybackPos.MouseDown
        IsChangingPos = True
    End Sub

    Private Sub DxvuMeterNET1_PlaybackStateChanged(ByVal OldState As DXVUMeterNETGDI.PlaybackStateConstants, ByVal NewState As DXVUMeterNETGDI.PlaybackStateConstants) Handles DxvuMeterNET1.PlaybackStateChanged
        If NewState = DXVUMeterNETGDI.PlaybackStateConstants.Idle Then UpdateControlsState()
    End Sub

    Private Sub DxvuMeterNET1_PaintGDI(graphics As Graphics) Handles DxvuMeterNET1.PaintGDI
        With graphics
            .Clear(Color.Black)

            .SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim b As Brush
            Const ToRad As Double = Math.PI / 180
            Dim w As Integer = DxvuMeterNET1.Width
            Dim w2 As Integer = w / 2
            Dim h As Integer = DxvuMeterNET1.Height
            Dim h2 As Integer = h / 2

            Dim pt(3) As Point

            Const startAngle As Integer = 180 - 10
            Const endAngle As Integer = 0 + 10

            Dim mp As Integer

            For a As Integer = startAngle To endAngle Step -4
                ' Left Channel
                mp = (startAngle - endAngle + 10) * (1 - IIf(DxvuMeterNET1.State = DXVUMeterNETGDI.PlaybackStateConstants.Idle, 0, lastPeak.Left))
                If a >= startAngle / 2 Then
                    b = New SolidBrush(IIf(mp < a, DxvuMeterNET1.GreenOn, DxvuMeterNET1.GreenOff))
                ElseIf a < startAngle / 2 And a > startAngle / 4 Then
                    b = New SolidBrush(IIf(mp <= a, DxvuMeterNET1.YellowOn, DxvuMeterNET1.YellowOff))
                Else
                    b = New SolidBrush(IIf(mp <= a, DxvuMeterNET1.RedOn, DxvuMeterNET1.RedOff))
                End If

                pt(0).X = w2 + w2 / 1.5 * Math.Cos(a * ToRad)
                pt(0).Y = h - h / 1.5 * Math.Sin(a * ToRad)

                pt(1).X = w2 + w2 / 1.2 * Math.Cos(a * ToRad)
                pt(1).Y = h - h / 1.2 * Math.Sin(a * ToRad)

                pt(2).X = w2 + w2 / 1.2 * Math.Cos((a - 2) * ToRad)
                pt(2).Y = h - h / 1.2 * Math.Sin((a - 2) * ToRad)

                pt(3).X = w2 + w2 / 1.5 * Math.Cos((a - 2) * ToRad)
                pt(3).Y = h - h / 1.5 * Math.Sin((a - 2) * ToRad)

                .FillPolygon(b, pt)

                ' Right Channel
                mp = (startAngle - endAngle + 10) * (1 - IIf(DxvuMeterNET1.State = DXVUMeterNETGDI.PlaybackStateConstants.Idle, 0, lastPeak.Right))
                If a >= startAngle / 2 Then
                    b = New SolidBrush(IIf(mp < a, DxvuMeterNET1.GreenOn, DxvuMeterNET1.GreenOff))
                ElseIf a < startAngle / 2 And a > startAngle / 4 Then
                    b = New SolidBrush(IIf(mp <= a, DxvuMeterNET1.YellowOn, DxvuMeterNET1.YellowOff))
                Else
                    b = New SolidBrush(IIf(mp <= a, DxvuMeterNET1.RedOn, DxvuMeterNET1.RedOff))
                End If

                pt(0).X = w2 + w2 / 2.1 * Math.Cos(a * ToRad)
                pt(0).Y = h - h / 2.1 * Math.Sin(a * ToRad)

                pt(1).X = w2 + w2 / 1.6 * Math.Cos(a * ToRad)
                pt(1).Y = h - h / 1.6 * Math.Sin(a * ToRad)

                pt(2).X = w2 + w2 / 1.6 * Math.Cos((a - 2) * ToRad)
                pt(2).Y = h - h / 1.6 * Math.Sin((a - 2) * ToRad)

                pt(3).X = w2 + w2 / 2.1 * Math.Cos((a - 2) * ToRad)
                pt(3).Y = h - h / 2.1 * Math.Sin((a - 2) * ToRad)

                .FillPolygon(b, pt)
            Next
        End With
    End Sub

    Private Sub DxvuMeterNET1_Error(ByVal Code As DXVUMeterNETGDI.ErrorConstants, ByVal Source As String, ByVal Message As String) Handles DxvuMeterNET1.Error
#If DEBUG Then
        MsgBox(Source + vbCrLf + Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error: " + Code.ToString)
        Debug.WriteLine(Message)
#End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Static WasMinimized As Boolean
        Static lastAnchor As AnchorStyles
        If Me.WindowState = FormWindowState.Minimized Then
            WasMinimized = True
            lastAnchor = DxvuMeterNET1.Anchor
            DxvuMeterNET1.Anchor = AnchorStyles.None
        Else
            If WasMinimized Then
                WasMinimized = False
                DxvuMeterNET1.Anchor = lastAnchor
            End If
        End If

        MyBase.OnResize(New EventArgs)
    End Sub

    Private tmrHideInfo As Threading.Timer
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "DXVU Meter " + DxvuMeterNET1.Version + " Tester"

        tmrHideInfo = New Threading.Timer(Sub() Me.Invoke(New MethodInvoker(Sub() lblInfo.Visible = False)),
                                          Nothing,
                                          30000,
                                          Threading.Timeout.Infinite)
        AddHandler lblInfo.Click, Sub() lblInfo.Visible = False
    End Sub

    Private Sub tbPBVol_MouseUp(sender As Object, e As MouseEventArgs) Handles tbPBVol.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then tbPBVol.Value = 0
    End Sub

    Private Sub tbPBVol_ValueChanged(sender As Object, e As EventArgs) Handles tbPBVol.ValueChanged
        dxvuCtrl.PlaybackVolume = tbPBVol.Value
    End Sub

    Private Sub DxvuMeterNET1_DoubleClick(sender As Object, e As EventArgs) Handles DxvuMeterNET1.DoubleClick
        ctrlIsFullScreen = Not ctrlIsFullScreen
        ResetCtrlSize(False)
    End Sub

    Private Sub UpdateFormBorder()
        Me.FormBorderStyle = If(Me.WindowState = FormWindowState.Maximized AndAlso ctrlIsFullScreen, FormBorderStyle.None, FormBorderStyle.Sizable)
    End Sub

    Private Sub frmMain_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        ResetCtrlSize(True)
    End Sub

    Private Sub ResetCtrlSize(ByVal force As Boolean)
        If dxvuCtrl Is Nothing Then Exit Sub
        Static lastState As Boolean

        If Not force Then
            If lastState = ctrlIsFullScreen Then Exit Sub
            lastState = ctrlIsFullScreen
        End If

        If ctrlIsFullScreen Then
            tbControls.Visible = False
            dxvuCtrl.Dock = DockStyle.Fill
        Else
            tbControls.Visible = True
            dxvuCtrl.Dock = DockStyle.None
            dxvuCtrl.Top = 159
            dxvuCtrl.Width = pCtrlBack.Width
            dxvuCtrl.Height = Me.DisplayRectangle.Height - dxvuCtrl.Top
        End If

        UpdateFormBorder()
    End Sub
End Class
