Imports System.Math

Partial Public Class DXVUMeterNETGDI
    Private Class CCursorPos
        Private cInfo As String
        Private cPos As Point
        Private IsValid As Boolean
        Private IsOver As Boolean
        Private mParent As DXVUMeterNETGDI

        Public Sub New(parent As DXVUMeterNETGDI)
            mParent = parent
        End Sub

        Public ReadOnly Property PositionF() As PointF
            Get
                Dim s As SizeF = TextRenderer.MeasureText(cInfo, mParent.pFFTScaleFont)
                Dim x As Integer = cPos.X + 10
                Dim y As Integer = CInt(cPos.Y - s.Height)

                If x + s.Width > mParent.fft_w + mParent.fftWH.Width Then x = CInt(mParent.fft_w - s.Width + mParent.fftWH.Width)
                If y < mParent.fftWH.Height Then y = CInt(mParent.fftWH.Height)

                Return New PointF(x, y)
            End Get
        End Property

        Public Property Position() As Point
            Get
                Return cPos
            End Get
            Set(Value As Point)
                cPos = Value

                Dim f As Double = mParent.X2Freq(Value.X, mParent.fftMin, mParent.fftMax, mParent.fft_w)
                cInfo = HumanFreq(f) & " " & Freq2Note(f)
            End Set
        End Property

        Public Function GetCursorPosInfo() As String
            Return cInfo
        End Function

        Public Property CursorIsOver() As Boolean
            Get
                Return IsOver
            End Get
            Set(Value As Boolean)
                IsOver = Value
            End Set
        End Property

        Public ReadOnly Property CursorIsOnDisplay() As Boolean
            Get
                Return IsOver And (cPos.X >= mParent.fftWH.Width And cPos.X <= mParent.Width) And (cPos.Y >= mParent.fftWH.Height And cPos.Y <= mParent.Height)
            End Get
        End Property

        Shared Function Freq2Note(f As Double) As String
            If f >= 16 Then
                Dim Notes() As String = {"C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"}
                Dim n As Double = Abs(Log(f) / Log(2 ^ (1 / 12)))
                Dim nf As Integer = CInt(n) Mod 12
                Dim o As Integer = CInt(Log(f / Note2Freq(nf), 2))
                Dim ns As String = Notes(nf)
                If ns.IndexOf("#") = -1 Then ns += " "

                Return ns + o.ToString
            Else
                Return ""
            End If
        End Function

        Shared Function Note2Freq(n As Integer) As Double
            Return 16.35 * 2 ^ (n / 12)
        End Function
    End Class
End Class
