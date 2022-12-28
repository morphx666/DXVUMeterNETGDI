Public Class DXVUMeterNET
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'DXVUMeterNET
        '
        Me.Name = "DXVUMeterNET"
        Me.Size = New System.Drawing.Size(284, 199)

    End Sub

#End Region

    Private pNumVUs As Short = 16
    Private VUs() As Panel

    Public Property NumVUs() As Integer
        Get
            Return pNumVUs
        End Get
        Set(ByVal Value As Integer)
            pNumVUs = Value
        End Set
    End Property

    Private Sub DXVUMeterNET_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RenderUI(True)
    End Sub

    Private Sub RenderUI(Optional ByVal CreateVUs As Boolean = False)
        Dim i As Integer
        Dim x As Integer = 0
        Dim y As Integer = 0

        If CreateVUs Then
            For i = 0 To UBound(VUs)
                VUs(i).Dispose()
            Next
            ReDim VUs(pNumVUs * 2 - 1)
            For i = 0 To pNumVUs * 2 - 1
                VUs(i) = New Panel
                VUs(i).Parent = Me
            Next
        End If

        For i = 0 To pNumVUs - 1
            With VUs(i)
                .Left = x
                .Top = y
                .Width = 4
                .Height = Height / 2 - 2
                .Visible = True
            End With
        Next
    End Sub
End Class