Imports System.Math

'http://armitage.cns.montana.edu/svn/mien/mien/math/sigtools.py
Module modTF
    Private Structure SpectraResults
        Dim Pxx() As Double
        Dim Pyy() As Double
        Dim Pxy() As ComplexDouble

        Public Sub New(rPxx() As Double, rPyy() As Double, rPxy() As ComplexDouble)
            Me.Pxx = rPxx
            Me.Pyy = rPyy
            Me.Pxy = rPxy
        End Sub
    End Structure

    Public Function TransferFunction(inp() As ComplexDouble, out() As ComplexDouble, windowSize As FFTSizeConstants) As ComplexDouble()
        Dim sr As SpectraResults = GetSpectraHannFFT(inp, out, windowSize)

        Dim res(sr.Pxx.Length - 1) As ComplexDouble
        For i As Integer = 0 To res.Length - 1
            res(i) = sr.Pxy(i) / sr.Pxx(i)
        Next
        Return res
    End Function

    Public Function Coherence(inp() As ComplexDouble, out() As ComplexDouble, fs As Integer) As ComplexDouble()
        Dim nfft As Double = 2 ^ CInt(Log(fs) / Log(2))
        Do While nfft > inp.Length - 1
            nfft = nfft / 2
        Loop

        nfft *= 2
        Dim sr As SpectraResults = GetSpectraHannFFT(inp, out, CType(CInt(nfft), FFTSizeConstants))

        'freq = arange(Pxx.shape[0]).astype(Float64)*fs/(2*Pxx.shape[0]) ??????????????
        'Dim freq(sr.Pxx.Length - 1) As Double
        'For i As Integer = 0 To freq.Length - 1
        '    freq(i) = i * fs / (2 * sr.Pxx.Length)
        'Next

        'coh = abs(Pxy)**2/(Pxx*Pyy)
        Dim coh(sr.Pxx.Length - 1) As ComplexDouble
        For i As Integer = 0 To coh.Length - 1
            coh(i) = New ComplexDouble(sr.Pxy(i).Abs() ^ 2 / (sr.Pxx(i) * sr.Pyy(i)), 0)
        Next

        'For i As Integer = 0 To coh.Length - 1
        '    Debug.WriteLine(String.Format("{0} + {1}i", coh(i).R, coh(i).I))
        'Next

        Return coh

        'fcd = transpose(array([freq, coh])) ???????????
        'Dim fcd(sr.Pxx.Length - 1) As ComplexDouble
        'For i As Integer = 0 To coh.Length - 1
        '    fcd(i) = New FFT.ComplexDouble(coh(i), 0)
        'Next
        'Return fcd
    End Function

    Private Function GetSpectraHannFFT(inp() As ComplexDouble, out() As ComplexDouble, windowSize As FFTSizeConstants) As SpectraResults
        Dim nfft As Integer = CInt(windowSize)

        Dim powX(nfft - 1) As Double
        Dim powY(nfft - 1) As Double
        Dim cross(nfft - 1) As ComplexDouble

        ' INPUT ****************************************
        For k As Integer = 0 To nfft - 1
            powX(k) = (inp(k) * inp(k).Conjugate()).R
            powY(k) = (out(k) * out(k).Conjugate()).R
            cross(k) = out(k) * inp(k).Conjugate()
        Next

        'cross+=csec[:window+1] ???????
        ' OUPUT ****************************************

        Return New SpectraResults(powX, powY, cross)
    End Function

    'Public Function TransferFunction(inp() As Double, out() As Double, windowSize As DXVUMeterNETGDI.FFTSizeConstants, Optional over As Integer = 0) As ComplexDouble()
    '    Dim sr As SpectraResults = GetSpectraHann(inp, out, windowSize, over)

    '    Dim res(sr.Pxx.Length - 1) As FFT.ComplexDouble
    '    For i As Integer = 0 To res.Length - 1
    '        res(i) = sr.Pxy(i) / sr.Pxx(i) * 100000
    '    Next
    '    Return res
    'End Function

    'Public Function Coherence(inp() As Double, out() As Double, fs As Integer) As ComplexDouble()
    '    Dim nfft As Double = 2 ^ CInt(Log(fs) / Log(2))
    '    Do While nfft > inp.Length - 1
    '        nfft = nfft / 2
    '    Loop

    '    nfft *= 2
    '    Dim sr As SpectraResults = GetSpectraHann(inp, out, CType(CInt(nfft), DXVUMeterNETGDI.FFTSizeConstants))

    '    'freq = arange(Pxx.shape[0]).astype(Float64)*fs/(2*Pxx.shape[0]) ??????????????
    '    'Dim freq(sr.Pxx.Length - 1) As Double
    '    'For i As Integer = 0 To freq.Length - 1
    '    '    freq(i) = i * fs / (2 * sr.Pxx.Length)
    '    'Next

    '    'coh = abs(Pxy)**2/(Pxx*Pyy)
    '    Dim coh(sr.Pxx.Length - 1) As ComplexDouble
    '    For i As Integer = 0 To coh.Length - 1
    '        coh(i) = sr.Pxy(i) ^ 2 / (sr.Pxx(i) * sr.Pyy(i)) / 1000
    '    Next

    '    'fcd = transpose(array([freq, coh])) ???????????
    '    'Dim fcd(sr.Pxx.Length - 1)() As ComplexDouble
    '    'For i As Integer = 0 To coh.Length - 1
    '    '    ReDim fcd(i)(1)
    '    '    fcd(i)(0) = New FFT.ComplexDouble(freq(i), 0)
    '    '    fcd(i)(1) = coh(i)
    '    'Next
    '    'Return fcd

    '    Return coh
    '    'Stop
    'End Function

    'Private Function GetSpectraHannOriginal(inp() As Double, out() As Double, windowSize As DXVUMeterNETGDI.FFTSizeConstants, over As Integer) As SpectraResults
    '    Dim nfft As Integer = CInt(windowSize)
    '    Dim window As Integer = nfft \ 2
    '    Dim winstep As Integer = window - over
    '    Dim nwin As Integer = CInt((inp.Length - 1 - over) / winstep)
    '    If nwin < 1 Then
    '        nwin = 1
    '        window = inp.Length - 1
    '        over = 0
    '        Console.WriteLine("Warning: data is short. Using single window")
    '    End If

    '    Dim powX(window + 1) As Double
    '    Dim powXPos As Integer = 0
    '    Dim powY(window + 1) As Double
    '    Dim powYPos As Integer = 0
    '    Dim cross(window + 1) As ComplexDouble
    '    Dim crossPos As Integer = 0

    '    Dim inpsec() As Double
    '    Dim inpfftR() As Double
    '    Dim inpfftI() As Double

    '    Dim outsec() As Double
    '    Dim outfftR() As Double
    '    Dim outfftI() As Double

    '    Dim psec() As ComplexDouble
    '    Dim csec() As ComplexDouble

    '    For i As Integer = 0 To nwin - 1
    '        ' INPUT ****************************************
    '        ReDim inpsec(i * winstep + window - 1)
    '        Array.Copy(inp, i * winstep, inpsec, 0, i * winstep + window)

    '        For k As Integer = 0 To inpsec.Length - 1
    '            inpsec(k) = (inpsec(k) - inpsec.Average) * FFT.ApplyWindow(k, windowSize, DXVUMeterNETGDI.FFTWindowConstants.Hanning)
    '        Next

    '        ReDim inpfftR(inpsec.Length - 1)
    '        ReDim inpfftI(inpsec.Length - 1)
    '        FFT.FourierTransform(nfft, inpsec, inpfftR, inpfftI, False)

    '        ReDim psec(inpsec.Length - 1)
    '        For k As Integer = 0 To inpsec.Length - 1
    '            psec(k) = New ComplexDouble(inpfftR(k), inpfftI(k)) * New ComplexDouble(inpfftR(k), inpfftI(k)).Conjugate()
    '        Next

    '        For k As Integer = 0 To psec.Length - 1
    '            powX(powXPos) = psec(k).R
    '            powXPos += 1
    '        Next
    '        ' INPUT ****************************************

    '        ' OUPUT ****************************************
    '        ReDim outsec(i * winstep + window - 1)
    '        Array.Copy(out, i * winstep, outsec, 0, i * winstep + window)

    '        For k As Integer = 0 To outsec.Length - 1
    '            outsec(k) = (outsec(k) - outsec.Average) * FFT.ApplyWindow(k, windowSize, DXVUMeterNETGDI.FFTWindowConstants.Hanning)
    '        Next

    '        ReDim outfftR(outsec.Length - 1)
    '        ReDim outfftI(outsec.Length - 1)
    '        FFT.FourierTransform(nfft, outsec, outfftR, outfftI, False)

    '        ReDim csec(outsec.Length - 1)
    '        For k As Integer = 0 To outsec.Length - 1
    '            csec(k) = New ComplexDouble(outfftR(k), outfftI(k)) * New ComplexDouble(inpfftR(k), inpfftI(k)).Conjugate()
    '            psec(k) = New ComplexDouble(outfftR(k), outfftI(k)) * New ComplexDouble(outfftR(k), outfftI(k)).Conjugate()
    '        Next

    '        For k As Integer = 0 To psec.Length - 1
    '            powY(powYPos) = psec(k).R
    '            powYPos += 1
    '        Next

    '        'cross+=csec[:window+1] ???????
    '        For k As Integer = 0 To psec.Length - 1
    '            cross(crossPos) = csec(k)
    '            crossPos += 1
    '        Next
    '        ' OUPUT ****************************************
    '    Next

    '    Return New SpectraResults(powX, powY, cross)
    'End Function
End Module
