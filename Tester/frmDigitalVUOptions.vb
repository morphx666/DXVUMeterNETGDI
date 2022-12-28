Imports NDXVUMeterNET

Public Class frmDigitalVUOptions
    Inherits Form

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
    Private components As ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As Label
    Friend WithEvents tbNumLeds As TrackBar
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnGreenOff As Button
    Friend WithEvents btnRedOff As Button
    Friend WithEvents btnRedOn As Button
    Friend WithEvents btnGreenOn As Button
    Friend WithEvents btnYellowOn As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btnYellowOff As Button
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New Label
        Me.tbNumLeds = New TrackBar
        Me.Label3 = New Label
        Me.btnGreenOff = New Button
        Me.Label4 = New Label
        Me.btnRedOff = New Button
        Me.btnRedOn = New Button
        Me.btnGreenOn = New Button
        Me.btnYellowOn = New Button
        Me.Label2 = New Label
        Me.btnYellowOff = New Button
        CType(Me.tbNumLeds, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New Point(14, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Size(86, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Number of Leds"
        Me.Label1.UseMnemonic = False
        '
        'tbNumLeds
        '
        Me.tbNumLeds.AutoSize = False
        Me.tbNumLeds.LargeChange = 4
        Me.tbNumLeds.Location = New Point(14, 26)
        Me.tbNumLeds.Maximum = 64
        Me.tbNumLeds.Minimum = 8
        Me.tbNumLeds.Name = "tbNumLeds"
        Me.tbNumLeds.Size = New Size(205, 31)
        Me.tbNumLeds.TabIndex = 1
        Me.tbNumLeds.TickFrequency = 4
        Me.tbNumLeds.Value = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New Point(14, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New Size(84, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Med Level Color"
        '
        'btnGreenOff
        '
        Me.btnGreenOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOff.Cursor = Cursors.Arrow
        Me.btnGreenOff.FlatStyle = FlatStyle.Flat
        Me.btnGreenOff.Location = New Point(118, 79)
        Me.btnGreenOff.Name = "btnGreenOff"
        Me.btnGreenOff.Size = New Size(16, 16)
        Me.btnGreenOff.TabIndex = 4
        Me.btnGreenOff.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New Point(14, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New Size(85, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "High Level Color"
        '
        'btnRedOff
        '
        Me.btnRedOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRedOff.Cursor = Cursors.Arrow
        Me.btnRedOff.FlatStyle = FlatStyle.Flat
        Me.btnRedOff.Location = New Point(118, 125)
        Me.btnRedOff.Name = "btnRedOff"
        Me.btnRedOff.Size = New Size(16, 16)
        Me.btnRedOff.TabIndex = 6
        Me.btnRedOff.UseVisualStyleBackColor = False
        '
        'btnRedOn
        '
        Me.btnRedOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRedOn.Cursor = Cursors.Arrow
        Me.btnRedOn.FlatStyle = FlatStyle.Flat
        Me.btnRedOn.Location = New Point(142, 125)
        Me.btnRedOn.Name = "btnRedOn"
        Me.btnRedOn.Size = New Size(16, 16)
        Me.btnRedOn.TabIndex = 10
        Me.btnRedOn.UseVisualStyleBackColor = False
        '
        'btnGreenOn
        '
        Me.btnGreenOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOn.Cursor = Cursors.Arrow
        Me.btnGreenOn.FlatStyle = FlatStyle.Flat
        Me.btnGreenOn.Location = New Point(142, 79)
        Me.btnGreenOn.Name = "btnGreenOn"
        Me.btnGreenOn.Size = New Size(16, 16)
        Me.btnGreenOn.TabIndex = 9
        Me.btnGreenOn.UseVisualStyleBackColor = False
        '
        'btnYellowOn
        '
        Me.btnYellowOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOn.Cursor = Cursors.Arrow
        Me.btnYellowOn.FlatStyle = FlatStyle.Flat
        Me.btnYellowOn.Location = New Point(142, 102)
        Me.btnYellowOn.Name = "btnYellowOn"
        Me.btnYellowOn.Size = New Size(16, 16)
        Me.btnYellowOn.TabIndex = 13
        Me.btnYellowOn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(14, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New Size(83, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Low Level Color"
        '
        'btnYellowOff
        '
        Me.btnYellowOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOff.Cursor = Cursors.Arrow
        Me.btnYellowOff.FlatStyle = FlatStyle.Flat
        Me.btnYellowOff.Location = New Point(118, 102)
        Me.btnYellowOff.Name = "btnYellowOff"
        Me.btnYellowOff.Size = New Size(16, 16)
        Me.btnYellowOff.TabIndex = 11
        Me.btnYellowOff.UseVisualStyleBackColor = False
        '
        'frmDigitalVUOptions
        '
        Me.AutoScaleBaseSize = New Size(5, 13)
        Me.ClientSize = New Size(224, 154)
        Me.Controls.Add(Me.btnYellowOn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnYellowOff)
        Me.Controls.Add(Me.btnRedOn)
        Me.Controls.Add(Me.btnGreenOn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnRedOff)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnGreenOff)
        Me.Controls.Add(Me.tbNumLeds)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDigitalVUOptions"
        Me.Text = "DigitalVU Options"
        CType(Me.tbNumLeds, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private IsALTDown As Boolean

    Private Sub tbNumLeds_MouseUp(sender As Object, e As MouseEventArgs) Handles tbNumLeds.MouseUp
        dxvuCtrl.NumVUs = tbNumLeds.Value
    End Sub

    Friend Sub InitDlg()
        tbNumLeds.Value = dxvuCtrl.NumVUs
        btnGreenOff.BackColor = dxvuCtrl.GreenOff
        btnGreenOn.BackColor = dxvuCtrl.GreenOn
        btnYellowOff.BackColor = dxvuCtrl.YellowOff
        btnYellowOn.BackColor = dxvuCtrl.YellowOn
        btnRedOff.BackColor = dxvuCtrl.RedOff
        btnRedOn.BackColor = dxvuCtrl.RedOn
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

    Private Sub btnGreenOff_Click(sender As Object, e As EventArgs) Handles btnGreenOff.Click
        ChangeColor(btnGreenOff)
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

    Private Sub btnRedOff_Click(sender As Object, e As EventArgs) Handles btnRedOff.Click
        ChangeColor(btnRedOff)
    End Sub

    Private Sub btnRedOn_Click(sender As Object, e As EventArgs) Handles btnRedOn.Click
        ChangeColor(btnRedOn)
    End Sub

    Friend Sub RestoreColors()
        With dxvuCtrl
            .YellowOff = btnYellowOff.BackColor
            .YellowOn = btnYellowOn.BackColor
            .GreenOff = btnGreenOff.BackColor
            .GreenOn = btnGreenOn.BackColor
            .RedOff = btnRedOff.BackColor
            .RedOn = btnRedOn.BackColor
        End With
    End Sub

    Private Sub frmDigitalVUOptions_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub frmDigitalVUOptions_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If IsALTDown Then
            Select Case e.Delta
                Case Is > 0
                    Me.Opacity = Math.Min(Me.Opacity + 0.1, 1)
                Case Is < 0
                    Me.Opacity = Math.Max(Me.Opacity - 0.1, 0.2)
            End Select
        End If
    End Sub

    Private Sub frmDigitalVUOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        IsALTDown = e.Alt
    End Sub

    Private Sub frmDigitalVUOptions_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        IsALTDown = Not e.Alt
    End Sub

    Private Sub tbNumLeds_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbNumLeds.MouseWheel
        If IsALTDown Then frmDigitalVUOptions_MouseWheel(Me, e)
    End Sub

    Private Sub tbNumLeds_Scroll(sender As Object, e As EventArgs) Handles tbNumLeds.Scroll

    End Sub
End Class
