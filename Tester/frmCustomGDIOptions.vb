Imports NDXVUMeterNET

Public Class frmCustomGDIOptions
    Private IsALTDown As Boolean

    Friend Sub InitDlg()
        btnGreenOff.BackColor = CustomGDIColors.GreenOff
        btnGreenOn.BackColor = CustomGDIColors.GreenOn
        btnYellowOff.BackColor = CustomGDIColors.YellowOff
        btnYellowOn.BackColor = CustomGDIColors.YellowOn
        btnRedOff.BackColor = CustomGDIColors.RedOff
        btnRedOn.BackColor = CustomGDIColors.RedOn
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

    Private Sub frmCustomGDIOptions_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub frmCustomGDIOptions_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If IsALTDown Then
            Select Case e.Delta
                Case Is > 0
                    Me.Opacity = Math.Min(Me.Opacity + 0.1, 1)
                Case Is < 0
                    Me.Opacity = Math.Max(Me.Opacity - 0.1, 0.2)
            End Select
        End If
    End Sub

    Private Sub frmCustomGDIOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        IsALTDown = e.Alt
    End Sub

    Private Sub frmCustomGDIOptions_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        IsALTDown = Not e.Alt
    End Sub
End Class