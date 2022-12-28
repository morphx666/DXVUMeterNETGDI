Imports System.ComponentModel

<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
Module modColorSpaceFunctions
    Public Structure HSL
        Dim Hue As Integer
        Dim Saturation As Integer
        Dim Luminance As Integer
    End Structure

    Public Function RGBtoHSL(Red As Integer,
                             Green As Integer,
                             Blue As Integer) As HSL

        Dim pRed As Double
        Dim pGreen As Double
        Dim pBlue As Double
        Dim RetVal As HSL
        Dim pMax As Double
        Dim pMin As Double
        Dim pLum As Double
        Dim pSat As Double
        Dim pHue As Double

        pRed = Red / 255
        pGreen = Green / 255
        pBlue = Blue / 255

        If pRed > pGreen Then
            If pRed > pBlue Then
                pMax = pRed
            Else
                pMax = pBlue
            End If
        ElseIf pGreen > pBlue Then
            pMax = pGreen
        Else
            pMax = pBlue
        End If

        If pRed < pGreen Then
            If pRed < pBlue Then
                pMin = pRed
            Else
                pMin = pBlue
            End If
        ElseIf pGreen < pBlue Then
            pMin = pGreen
        Else
            pMin = pBlue
        End If

        pLum = (pMax + pMin) / 2

        If pMax = pMin Then
            pSat = 0
            pHue = 0
        Else
            If pLum < 0.5 Then
                pSat = (pMax - pMin) / (pMax + pMin)
            Else
                pSat = (pMax - pMin) / (2 - pMax - pMin)
            End If

            Select Case pMax
                Case pRed
                    pHue = (pGreen - pBlue) / (pMax - pMin)
                Case pGreen
                    pHue = 2 + (pBlue - pRed) / (pMax - pMin)
                Case pBlue
                    pHue = 4 + (pRed - pGreen) / (pMax - pMin)
            End Select
        End If

        RetVal.Hue = CInt(pHue * 239 / 6)
        If RetVal.Hue < 0 Then RetVal.Hue = RetVal.Hue + 240

        RetVal.Saturation = CInt(pSat * 239)
        RetVal.Luminance = CInt(pLum * 239)

        Return RetVal
    End Function

    Public Function HSLtoRGB(Hue As Integer,
                         Saturation As Integer,
                         Luminance As Integer) As Color

        Dim pHue As Double
        Dim pSat As Double
        Dim pLum As Double
        Dim pRed As Double
        Dim pGreen As Double
        Dim pBlue As Double
        Dim temp2 As Double
        Dim temp3() As Double
        Dim temp1 As Double
        Dim n As Integer

        ReDim temp3(0 To 2)

        pHue = Hue / 255
        pSat = Saturation / 255
        pLum = Luminance / 255

        If pSat = 0 Then
            pRed = pLum
            pGreen = pLum
            pBlue = pLum
        Else
            If pLum < 0.5 Then
                temp2 = pLum * (1 + pSat)
            Else
                temp2 = pLum + pSat - pLum * pSat
            End If
            temp1 = 2 * pLum - temp2

            temp3(0) = pHue + 1 / 3
            temp3(1) = pHue
            temp3(2) = pHue - 1 / 3

            For n = 0 To 2
                If temp3(n) < 0 Then temp3(n) = temp3(n) + 1
                If temp3(n) > 1 Then temp3(n) = temp3(n) - 1

                If 6 * temp3(n) < 1 Then
                    temp3(n) = temp1 + (temp2 - temp1) * 6 * temp3(n)
                Else
                    If 2 * temp3(n) < 1 Then
                        temp3(n) = temp2
                    Else
                        If 3 * temp3(n%) < 2 Then
                            temp3(n%) = temp1 + (temp2 - temp1) * ((2 / 3) - temp3(n%)) * 6
                        Else
                            temp3(n%) = temp1
                        End If
                    End If
                End If
            Next n%

            pRed = temp3(0)
            pGreen = temp3(1)
            pBlue = temp3(2)
        End If

        Return Color.FromArgb(255, CInt(pRed * 255), CInt(pGreen * 255), CInt(pBlue * 255))
    End Function
End Module