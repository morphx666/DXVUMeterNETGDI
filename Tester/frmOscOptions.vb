Imports NDXVUMeterNET

Public Class frmOscOptions
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
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnYellowOn As Button
    Friend WithEvents btnYellowOff As Button
    Friend WithEvents btnGreenOn As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents tbLinesThickness As TrackBar
    Friend WithEvents btnGreenOff As Button
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnYellowOn = New Button
        Me.Label3 = New Label
        Me.btnYellowOff = New Button
        Me.Label1 = New Label
        Me.Label4 = New Label
        Me.btnGreenOn = New Button
        Me.Label2 = New Label
        Me.btnGreenOff = New Button
        Me.Label5 = New Label
        Me.tbLinesThickness = New TrackBar
        CType(Me.tbLinesThickness, ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnYellowOn
        '
        Me.btnYellowOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOn.Cursor = Cursors.Arrow
        Me.btnYellowOn.FlatStyle = FlatStyle.Flat
        Me.btnYellowOn.Location = New Point(115, 87)
        Me.btnYellowOn.Name = "btnYellowOn"
        Me.btnYellowOn.Size = New Size(16, 16)
        Me.btnYellowOn.TabIndex = 18
        Me.btnYellowOn.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New Point(12, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New Size(71, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Right Ch Line"
        '
        'btnYellowOff
        '
        Me.btnYellowOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOff.Cursor = Cursors.Arrow
        Me.btnYellowOff.FlatStyle = FlatStyle.Flat
        Me.btnYellowOff.Location = New Point(115, 63)
        Me.btnYellowOff.Name = "btnYellowOff"
        Me.btnYellowOff.Size = New Size(16, 16)
        Me.btnYellowOff.TabIndex = 14
        Me.btnYellowOff.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New Point(12, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Size(80, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Right Ch Wave"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New Point(12, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New Size(73, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Left Ch Wave"
        '
        'btnGreenOn
        '
        Me.btnGreenOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOn.Cursor = Cursors.Arrow
        Me.btnGreenOn.FlatStyle = FlatStyle.Flat
        Me.btnGreenOn.Location = New Point(115, 35)
        Me.btnGreenOn.Name = "btnGreenOn"
        Me.btnGreenOn.Size = New Size(16, 16)
        Me.btnGreenOn.TabIndex = 27
        Me.btnGreenOn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(12, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New Size(64, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Left Ch Line"
        '
        'btnGreenOff
        '
        Me.btnGreenOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOff.Cursor = Cursors.Arrow
        Me.btnGreenOff.FlatStyle = FlatStyle.Flat
        Me.btnGreenOff.Location = New Point(115, 11)
        Me.btnGreenOff.Name = "btnGreenOff"
        Me.btnGreenOff.Size = New Size(16, 16)
        Me.btnGreenOff.TabIndex = 25
        Me.btnGreenOff.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New Point(12, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New Size(84, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Lines Thickness"
        '
        'tbLinesThickness
        '
        Me.tbLinesThickness.AutoSize = False
        Me.tbLinesThickness.Location = New Point(109, 114)
        Me.tbLinesThickness.Name = "tbLinesThickness"
        Me.tbLinesThickness.Size = New Size(103, 20)
        Me.tbLinesThickness.TabIndex = 30
        Me.tbLinesThickness.Value = 1
        '
        'frmOscOptions
        '
        Me.AutoScaleBaseSize = New Size(5, 13)
        Me.ClientSize = New Size(216, 145)
        Me.Controls.Add(Me.tbLinesThickness)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnGreenOn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnGreenOff)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnYellowOn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnYellowOff)
        Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmOscOptions"
        Me.Text = "Oscilloscope Options"
        CType(Me.tbLinesThickness, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private IsALTDown As Boolean

    Friend Sub InitDlg()
        btnGreenOff.BackColor = dxvuCtrl.GreenOff
        btnGreenOn.BackColor = dxvuCtrl.GreenOn
        btnYellowOff.BackColor = dxvuCtrl.YellowOff
        btnYellowOn.BackColor = dxvuCtrl.YellowOn
        tbLinesThickness.Value = dxvuCtrl.LinesThickness
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

    Friend Sub RestoreColors()
        With dxvuCtrl
            .YellowOff = btnYellowOff.BackColor
            .YellowOn = btnYellowOn.BackColor
            .GreenOff = btnGreenOff.BackColor
            .GreenOn = btnGreenOn.BackColor
            .LinesThickness = tbLinesThickness.Value
        End With
    End Sub

    Private Sub frmOscOptions_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub tbLinesWidth_ValueChanged(sender As Object, e As EventArgs) Handles tbLinesThickness.ValueChanged
        dxvuCtrl.LinesThickness = tbLinesThickness.Value
    End Sub

    Private Sub frmOscOptions_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If IsALTDown Then
            Select Case e.Delta
                Case Is > 0
                    Me.Opacity = Math.Min(Me.Opacity + 0.1, 1)
                Case Is < 0
                    Me.Opacity = Math.Max(Me.Opacity - 0.1, 0.2)
            End Select
        End If
    End Sub

    Private Sub frmOscOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        IsALTDown = e.Alt
    End Sub

    Private Sub frmOscOptions_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        IsALTDown = Not e.Alt
    End Sub
End Class
