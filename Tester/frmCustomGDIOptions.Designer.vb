<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCustomGDIOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnYellowOn = New Button
        Me.Label2 = New Label
        Me.btnYellowOff = New Button
        Me.btnRedOn = New Button
        Me.btnGreenOn = New Button
        Me.Label4 = New Label
        Me.btnRedOff = New Button
        Me.Label3 = New Label
        Me.btnGreenOff = New Button
        Me.SuspendLayout()
        '
        'btnYellowOn
        '
        Me.btnYellowOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOn.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnYellowOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnYellowOn.Location = New System.Drawing.Point(146, 35)
        Me.btnYellowOn.Name = "btnYellowOn"
        Me.btnYellowOn.Size = New System.Drawing.Size(16, 16)
        Me.btnYellowOn.TabIndex = 22
        Me.btnYellowOn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Low Level Color"
        '
        'btnYellowOff
        '
        Me.btnYellowOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYellowOff.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnYellowOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnYellowOff.Location = New System.Drawing.Point(122, 35)
        Me.btnYellowOff.Name = "btnYellowOff"
        Me.btnYellowOff.Size = New System.Drawing.Size(16, 16)
        Me.btnYellowOff.TabIndex = 20
        Me.btnYellowOff.UseVisualStyleBackColor = False
        '
        'btnRedOn
        '
        Me.btnRedOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRedOn.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnRedOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRedOn.Location = New System.Drawing.Point(146, 58)
        Me.btnRedOn.Name = "btnRedOn"
        Me.btnRedOn.Size = New System.Drawing.Size(16, 16)
        Me.btnRedOn.TabIndex = 19
        Me.btnRedOn.UseVisualStyleBackColor = False
        '
        'btnGreenOn
        '
        Me.btnGreenOn.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOn.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnGreenOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGreenOn.Location = New System.Drawing.Point(146, 12)
        Me.btnGreenOn.Name = "btnGreenOn"
        Me.btnGreenOn.Size = New System.Drawing.Size(16, 16)
        Me.btnGreenOn.TabIndex = 18
        Me.btnGreenOn.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "High Level Color"
        '
        'btnRedOff
        '
        Me.btnRedOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRedOff.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnRedOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRedOff.Location = New System.Drawing.Point(122, 58)
        Me.btnRedOff.Name = "btnRedOff"
        Me.btnRedOff.Size = New System.Drawing.Size(16, 16)
        Me.btnRedOff.TabIndex = 16
        Me.btnRedOff.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Med Level Color"
        '
        'btnGreenOff
        '
        Me.btnGreenOff.BackColor = Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnGreenOff.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnGreenOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGreenOff.Location = New System.Drawing.Point(122, 12)
        Me.btnGreenOff.Name = "btnGreenOff"
        Me.btnGreenOff.Size = New System.Drawing.Size(16, 16)
        Me.btnGreenOff.TabIndex = 14
        Me.btnGreenOff.UseVisualStyleBackColor = False
        '
        'frmCustomGDIOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(184, 85)
        Me.Controls.Add(Me.btnYellowOn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnYellowOff)
        Me.Controls.Add(Me.btnRedOn)
        Me.Controls.Add(Me.btnGreenOn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnRedOff)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnGreenOff)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmCustomGDIOptions"
        Me.Text = "CustomGDI Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnYellowOn As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btnYellowOff As Button
    Friend WithEvents btnRedOn As Button
    Friend WithEvents btnGreenOn As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents btnRedOff As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnGreenOff As Button
End Class
