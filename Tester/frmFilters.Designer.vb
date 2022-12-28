<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFilters
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <DebuggerNonUserCode()>
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
    Private components As ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ListViewFilters = New System.Windows.Forms.ListView()
        Me.chFilterType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chGain = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFrequency = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBandwidth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.cbFilterType = New System.Windows.Forms.ComboBox()
        Me.gbFilterProperties = New System.Windows.Forms.GroupBox()
        Me.lblFreqVal = New System.Windows.Forms.Label()
        Me.tbFrequency = New System.Windows.Forms.TrackBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbBandwidth = New System.Windows.Forms.TextBox()
        Me.tbGain = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbFilterProperties.SuspendLayout()
        CType(Me.tbFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvFilters
        '
        Me.ListViewFilters.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewFilters.CheckBoxes = True
        Me.ListViewFilters.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chFilterType, Me.chGain, Me.chFrequency, Me.chBandwidth})
        Me.ListViewFilters.FullRowSelect = True
        Me.ListViewFilters.GridLines = True
        Me.ListViewFilters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewFilters.HideSelection = False
        Me.ListViewFilters.Location = New System.Drawing.Point(12, 12)
        Me.ListViewFilters.MultiSelect = False
        Me.ListViewFilters.Name = "lvFilters"
        Me.ListViewFilters.Size = New System.Drawing.Size(327, 262)
        Me.ListViewFilters.TabIndex = 0
        Me.ListViewFilters.UseCompatibleStateImageBehavior = False
        Me.ListViewFilters.View = System.Windows.Forms.View.Details
        '
        'chFilterType
        '
        Me.chFilterType.Text = "Filter Type"
        '
        'chGain
        '
        Me.chGain.Text = "Gain"
        Me.chGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chFrequency
        '
        Me.chFrequency.Text = "Frequency"
        Me.chFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chBandwidth
        '
        Me.chBandwidth.Text = "Bandwidth"
        Me.chBandwidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnDelete
        '
        Me.ButtonDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDelete.Location = New System.Drawing.Point(264, 280)
        Me.ButtonDelete.Name = "btnDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(75, 23)
        Me.ButtonDelete.TabIndex = 1
        Me.ButtonDelete.Text = "Delete"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.ButtonAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd.Location = New System.Drawing.Point(183, 280)
        Me.ButtonAdd.Name = "btnAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAdd.TabIndex = 2
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'cbFilterType
        '
        Me.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterType.FormattingEnabled = True
        Me.cbFilterType.Location = New System.Drawing.Point(85, 19)
        Me.cbFilterType.Name = "cbFilterType"
        Me.cbFilterType.Size = New System.Drawing.Size(163, 21)
        Me.cbFilterType.TabIndex = 3
        '
        'gbFilterProperties
        '
        Me.gbFilterProperties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFilterProperties.Controls.Add(Me.lblFreqVal)
        Me.gbFilterProperties.Controls.Add(Me.tbFrequency)
        Me.gbFilterProperties.Controls.Add(Me.Label3)
        Me.gbFilterProperties.Controls.Add(Me.Label4)
        Me.gbFilterProperties.Controls.Add(Me.Label2)
        Me.gbFilterProperties.Controls.Add(Me.tbBandwidth)
        Me.gbFilterProperties.Controls.Add(Me.tbGain)
        Me.gbFilterProperties.Controls.Add(Me.Label1)
        Me.gbFilterProperties.Controls.Add(Me.cbFilterType)
        Me.gbFilterProperties.Location = New System.Drawing.Point(345, 12)
        Me.gbFilterProperties.Name = "gbFilterProperties"
        Me.gbFilterProperties.Size = New System.Drawing.Size(254, 262)
        Me.gbFilterProperties.TabIndex = 4
        Me.gbFilterProperties.TabStop = False
        Me.gbFilterProperties.Text = "Filter Properties"
        '
        'lblFreqVal
        '
        Me.lblFreqVal.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFreqVal.Location = New System.Drawing.Point(208, 75)
        Me.lblFreqVal.Name = "lblFreqVal"
        Me.lblFreqVal.Size = New System.Drawing.Size(43, 11)
        Me.lblFreqVal.TabIndex = 63
        Me.lblFreqVal.Text = "22.0 KHz"
        Me.lblFreqVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbFrequency
        '
        Me.tbFrequency.AutoSize = False
        Me.tbFrequency.LargeChange = 100
        Me.tbFrequency.Location = New System.Drawing.Point(85, 72)
        Me.tbFrequency.Name = "tbFrequency"
        Me.tbFrequency.Size = New System.Drawing.Size(125, 21)
        Me.tbFrequency.SmallChange = 10
        Me.tbFrequency.TabIndex = 57
        Me.tbFrequency.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Frequency"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Bandwidth"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Gain"
        '
        'tbBandwidth
        '
        Me.tbBandwidth.Location = New System.Drawing.Point(85, 99)
        Me.tbBandwidth.Name = "tbBandwidth"
        Me.tbBandwidth.Size = New System.Drawing.Size(163, 20)
        Me.tbBandwidth.TabIndex = 5
        Me.tbBandwidth.Text = "0"
        Me.tbBandwidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbGain
        '
        Me.tbGain.Location = New System.Drawing.Point(85, 46)
        Me.tbGain.Name = "tbGain"
        Me.tbGain.Size = New System.Drawing.Size(163, 20)
        Me.tbGain.TabIndex = 5
        Me.tbGain.Text = "0"
        Me.tbGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Type"
        '
        'frmFilters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 315)
        Me.Controls.Add(Me.gbFilterProperties)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ListViewFilters)
        Me.Name = "frmFilters"
        Me.Text = "Filters"
        Me.gbFilterProperties.ResumeLayout(False)
        Me.gbFilterProperties.PerformLayout()
        CType(Me.tbFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListViewFilters As ListView
    Friend WithEvents chFilterType As ColumnHeader
    Friend WithEvents chGain As ColumnHeader
    Friend WithEvents chFrequency As ColumnHeader
    Friend WithEvents chBandwidth As ColumnHeader
    Friend WithEvents ButtonDelete As Button
    Friend WithEvents ButtonAdd As Button
    Friend WithEvents cbFilterType As ComboBox
    Friend WithEvents gbFilterProperties As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents tbGain As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents tbFrequency As TrackBar
    Friend WithEvents lblFreqVal As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents tbBandwidth As TextBox
End Class
