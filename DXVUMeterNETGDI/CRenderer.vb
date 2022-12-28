Imports System.Threading
Imports System.ComponentModel

<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
Public Class CRenderer
    Private parentControl As DXVUMeterNETGDI
    Private isRendererBusy As Boolean
    Private area As Rectangle
    Private isDisposed As Boolean
    Private bBitmap As Bitmap
    Private mKeepBackground As Boolean
    Private newDataAvailable As Boolean
    Private safeDelay As TimeSpan = New TimeSpan(0, 0, 0, 0, 10)
    Private mSmoothingMode As Drawing2D.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed

#Region "Graphic Object Classes"
    Private MustInherit Class GraphicObject
        Public Enum ObjectTypeConstants
            FilledRectangle = 0
            Rectangle = 1
            ArrayOfLines = 2
            StringData = 3
        End Enum

        Public MustOverride ReadOnly Property ObjectType() As ObjectTypeConstants
    End Class

    Private Class ColoredFilledRectangle
        Inherits GraphicObject

        Public rectangle As RectangleF
        Public color As SolidBrush

        Public Sub New(rectangle As RectangleF, color As SolidBrush)
            Me.rectangle = rectangle
            Me.color = color
        End Sub

        Public Overrides ReadOnly Property ObjectType() As ObjectTypeConstants
            Get
                Return ObjectTypeConstants.FilledRectangle
            End Get
        End Property
    End Class

    Private Class ColoredRectangle
        Inherits GraphicObject

        Public rectangle As Rectangle
        Public color As Pen

        Public Sub New(rectangle As Rectangle, color As Pen)
            Me.rectangle = rectangle
            Me.color = color
        End Sub

        Public Overrides ReadOnly Property ObjectType() As ObjectTypeConstants
            Get
                Return ObjectTypeConstants.Rectangle
            End Get
        End Property
    End Class

    Private Class ColoredArrayOfLines
        Inherits GraphicObject

        Public points() As PointF
        Public color As Pen
        Public closed As Boolean

        Public Sub New(points() As PointF, color As Pen, Optional closed As Boolean = False)
            Me.points = points
            Me.color = color
            Me.closed = closed
        End Sub

        Public Overrides ReadOnly Property ObjectType() As ObjectTypeConstants
            Get
                Return ObjectTypeConstants.ArrayOfLines
            End Get
        End Property
    End Class

    Private Class StringData
        Inherits GraphicObject

        Public text As String
        Public location As PointF
        Public color As SolidBrush
        Public font As Font

        Public Sub New(text As String, font As Font, color As SolidBrush, location As PointF)
            Me.text = text
            Me.font = font
            Me.location = location
            Me.color = color
            Me.font = font
        End Sub

        Public Overrides ReadOnly Property ObjectType() As ObjectTypeConstants
            Get
                Return ObjectTypeConstants.StringData
            End Get
        End Property
    End Class
#End Region

    Private graphicObjects1 As List(Of GraphicObject)
    Private graphicObjects2 As List(Of GraphicObject)

    Public Sub New(parentControl As DXVUMeterNETGDI)
        Me.parentControl = parentControl

        graphicObjects1 = New List(Of GraphicObject)
        graphicObjects2 = New List(Of GraphicObject)

        ' -----------------------
        DEMOFontL1 = New Font(parentControl.Font.FontFamily, 18, FontStyle.Bold)
        DEMOFontL2 = New Font(parentControl.Font.FontFamily, 8, FontStyle.Bold)

        DEMOFontSizeL1 = TextRenderer.MeasureText(DEMOMsgL1, DEMOFontL1)
        DEMOFontSizeL2 = TextRenderer.MeasureText(DEMOMsgL2, DEMOFontL2)
    End Sub

    Public Property KeepBackground() As Boolean
        Get
            Return mKeepBackground
        End Get
        Set(value As Boolean)
            mKeepBackground = value

            EraseBackground()
        End Set
    End Property

    Public Property SmoothingMode As Drawing2D.SmoothingMode
        Get
            Return mSmoothingMode
        End Get
        Set(value As Drawing2D.SmoothingMode)
            mSmoothingMode = value
        End Set
    End Property

    Public Sub EraseBackground()
        If bBitmap IsNot Nothing Then
            SafeWait()
            bBitmap.Dispose()
            bBitmap = Nothing
        End If
    End Sub

    Public Sub ScrollBackground(offset As Integer)
        bBitmap = bBitmap.Clone(New Rectangle(0 - offset, 0, bBitmap.Width - 2 * offset, bBitmap.Height), bBitmap.PixelFormat)
    End Sub

    Public Sub Render(g As Graphics)
        If Not isRendererBusy Then
            isRendererBusy = True

            Dim targetGraphics As Graphics

            If mKeepBackground Then
                If bBitmap Is Nothing Then bBitmap = New Bitmap(parentControl.Width, parentControl.Height, g)
                targetGraphics = Graphics.FromImage(bBitmap)
            Else
                targetGraphics = g
            End If

            Dim fr As ColoredFilledRectangle
            Dim cr As ColoredRectangle
            Dim cl As ColoredArrayOfLines
            Dim sd As StringData

            targetGraphics.SmoothingMode = mSmoothingMode

            For Each go As GraphicObject In graphicObjects2
                Try
                    Select Case go.ObjectType
                        Case GraphicObject.ObjectTypeConstants.FilledRectangle
                            fr = CType(go, ColoredFilledRectangle)
                            targetGraphics.FillRectangle(fr.color, fr.rectangle)
                        Case GraphicObject.ObjectTypeConstants.Rectangle
                            cr = CType(go, ColoredRectangle)
                            targetGraphics.DrawRectangle(cr.color, cr.rectangle)
                        Case GraphicObject.ObjectTypeConstants.ArrayOfLines
                            cl = CType(go, ColoredArrayOfLines)
                            If cl.closed Then
                                targetGraphics.FillPolygon(New SolidBrush(cl.color.Color), cl.points)
                            Else
                                targetGraphics.DrawLines(cl.color, cl.points)
                            End If
                        Case GraphicObject.ObjectTypeConstants.StringData
                            sd = CType(go, StringData)
                            targetGraphics.DrawString(sd.text, sd.font, sd.color, sd.location)
                    End Select
                Catch ex As Exception
                    Debug.WriteLine("Render: " + ex.Message)
                End Try
            Next

            newDataAvailable = False

            If mKeepBackground Then
                g.DrawImageUnscaled(bBitmap, 0, 0)
                targetGraphics.Dispose()
            End If

            DrawDEMOMsg(g)

            isRendererBusy = False
        End If
    End Sub

    Public Function SetColorAlpha(initColor As Color, a As Short) As Color
        If a > 100 Then
            a = 100
        ElseIf a < 0 Then
            a = 0
        End If
        Return Color.FromArgb(CInt(255 * (a / 100)), initColor)
    End Function

    Private Sub SafeWait()
        Do While isRendererBusy OrElse newDataAvailable
            Thread.Sleep(safeDelay)
        Loop
    End Sub

    Public Sub PrepareAddingObjects()
        graphicObjects1.Clear()
    End Sub

    Public Sub DoneAddingGraphicObjects()
        SafeWait()

        graphicObjects2.Clear()
        graphicObjects2 = graphicObjects1.ConvertAll(New Converter(Of GraphicObject, GraphicObject)(Function(g) (g)))

        newDataAvailable = True
        parentControl.RePaint()
        'parentControl.Invalidate()
    End Sub

    Public Sub AddFilledRectangle(r As RectangleF, color As SolidBrush)
        graphicObjects1.Add(New ColoredFilledRectangle(r, color))
    End Sub

    Public Sub AddFilledRectangle(r As RectangleF, color As Color)
        Me.AddFilledRectangle(r, New SolidBrush(color))
    End Sub

    Public Sub AddFilledRectangle(x As Double, y As Double, width As Double, height As Double, color As Color)
        Me.AddFilledRectangle(New RectangleF(CSng(x), CSng(y), CSng(width), CSng(height)), color)
    End Sub

    Public Sub AddRectangle(r As Rectangle, color As Pen)
        graphicObjects1.Add(New ColoredRectangle(r, color))
    End Sub

    Public Sub AddRectangle(r As Rectangle, color As Color, Optional width As Single = 1.0)
        Me.AddRectangle(r, New Pen(color, width))
    End Sub

    Public Sub AddRectangle(x As Double, y As Double, width As Double, height As Double, color As Color, Optional penWidth As Single = 1.0)
        Me.AddRectangle(New Rectangle(CInt(x), CInt(y), CInt(width), CInt(height)), New Pen(color, penWidth))
    End Sub

    Public Sub AddLines(points() As PointF, color As Pen, Optional closed As Boolean = False)
        graphicObjects1.Add(New ColoredArrayOfLines(points, color, closed))
    End Sub

    Public Sub AddLines(points() As PointF, color As Color, Optional penWidth As Single = 1.0, Optional closed As Boolean = False)
        Me.AddLines(points, New Pen(color, penWidth), closed)
    End Sub

    Public Sub AddLine(x1 As Double, y1 As Double, x2 As Double, y2 As Double, color As Color, Optional penWidth As Single = 1.0)
        Me.AddLines(New PointF() {New PointF(CSng(x1), CSng(y1)), New PointF(CSng(x2), CSng(y2))}, color, penWidth)
    End Sub

    Public Sub AddString(text As String, font As Font, color As Color, location As PointF)
        graphicObjects1.Add(New StringData(text, font, New SolidBrush(color), location))
    End Sub

    Public Sub AddString(text As String, font As Font, color As Color, x As Double, y As Double)
        Me.AddString(text, font, color, New PointF(CSng(x), CSng(y)))
    End Sub

#Region "DEMO Stuff"
    Private DEMOFontL1 As Font
    Private DEMOFontSizeL1 As Size
    Private DEMOMsgL1 As String = "DXVUMeterNET(GDI) DEMO Version"
    Private DEMOFontL2 As Font
    Private DEMOFontSizeL2 As Size
    Private DEMOMsgL2 As String = "Visit http://software.xfx.net/netcl/dxvunet/ for more information"

    Private Sub DrawDEMOMsg(g As Graphics)
        If Not parentControl.IsLicensed Then
            Static alpha As Short = 99
            Static dir As Short = 2

            alpha += dir
            If alpha >= 400 Then dir = -4
            If alpha <= -800 Then dir = 2

            If alpha > 0 Then
                Dim x As Integer = CInt((parentControl.Width - DEMOFontSizeL1.Width) / 2)
                Dim y As Integer = CInt((parentControl.Height / 2) - DEMOFontSizeL1.Height + 4)
                Dim DEMOBrush1 As SolidBrush = New SolidBrush(SetColorAlpha(Color.Black, alpha))
                Dim DEMOBrush2 As SolidBrush = New SolidBrush(SetColorAlpha(Color.White, alpha))

                g.DrawString(DEMOMsgL1, DEMOFontL1, DEMOBrush1, x + 1, y + 1)
                g.DrawString(DEMOMsgL1, DEMOFontL1, DEMOBrush2, x, y)

                x = (parentControl.Width - DEMOFontSizeL2.Width) \ 2
                y += DEMOFontSizeL1.Height - DEMOFontSizeL2.Height \ 2 + 2
                g.DrawString(DEMOMsgL2, DEMOFontL2, DEMOBrush1, x + 1, y + 1)
                g.DrawString(DEMOMsgL2, DEMOFontL2, DEMOBrush2, x, y)

                DEMOBrush1.Dispose()
                DEMOBrush2.Dispose()
            End If
        End If
    End Sub
#End Region

    Public Sub Dispose()
        DEMOFontL1.Dispose()
        DEMOFontL2.Dispose()

        isDisposed = True
    End Sub
End Class