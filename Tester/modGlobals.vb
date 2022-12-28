Module modGlobals
    Public Structure CustomGDIColorsDef
        Dim GreenOff As Color
        Dim GreenOn As Color
        Dim YellowOff As Color
        Dim YellowOn As Color
        Dim RedOff As Color
        Dim RedOn As Color
    End Structure
    Public CustomGDIColors As CustomGDIColorsDef

    Public WithEvents dxvuCtrl As NDXVUMeterNET.DXVUMeterNETGDI

    Public Function HumanFreq(f As Integer) As String
        If f >= 1000 Then
            Dim fs As String = Math.Round(f / 1000, 1).ToString
            If fs.IndexOf(".") = -1 Then fs += ".0"
            Return fs + " Khz"
        Else
            Return Math.Round(f, 1).ToString + ".0 Hz"
        End If
    End Function
End Module
