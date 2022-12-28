Public Class frmMain

    Private Sub DxvuMeterNETGDI1_ControlIsReady() Handles DxvuMeterNETGDI1.ControlIsReady
        DxvuMeterNETGDI1.FFTScaleFont = New Font("Monotype.com", 8, GraphicsUnit.Pixel)

        DxvuMeterNETGDI1.StartMonitoring()
        DxvuMeterNETGDI1.BackColor = Color.Black
        DxvuMeterNETGDI1.Style = NDXVUMeterNET.DXVUMeterNETGDI.StyleConstants.FFT
        DxvuMeterNETGDI1.LinesThickness = 2

        DxvuMeterNETGDI1.FFTSmoothing = 4
        DxvuMeterNETGDI1.FFTXScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTXScaleConstants.Logarithmic
        DxvuMeterNETGDI1.FFTXZoom = True
        DxvuMeterNETGDI1.FFTXMax = 16000
        DxvuMeterNETGDI1.FFTYScale = NDXVUMeterNET.DXVUMeterNETGDI.FFTYScaleConstants.Magnitude
        DxvuMeterNETGDI1.FFTStyle = NDXVUMeterNET.DXVUMeterNETGDI.FFTStyleConstants.Line
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        DxvuMeterNETGDI1.StopMonitoring()
    End Sub

    Private Sub DxvuMeterNETGDI1_PeakValues(audioBuffer() As Byte, normalizedAudioBuffer() As Integer, maxPeak As NDXVUMeterNET.DXVUMeterNETGDI.Peak) Handles DxvuMeterNETGDI1.PeakValues
        Debug.WriteLine(normalizedAudioBuffer(0))
    End Sub
End Class
