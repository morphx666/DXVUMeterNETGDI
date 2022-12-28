' https://github.com/wooters/miniDSP/blob/master/biquad.c

Public Class BiQuadFilter
    Implements ICloneable

    Public Enum FiltersTypes
        ''' <summary>Low Pass Filter</summary>
        LowPass
        ''' <summary>High Pass Filter</summary>
        HighPass
        ''' <summary>Band Pass Filter</summary>
        BandPass
        ''' <summary>Notch Filter</summary>
        Notch
        ''' <summary>Peaking Band Filter</summary>
        PeakingBand
        ''' <summary>Low Shelf Filter</summary>
        LowShelf
        ''' <summary>High Shelf Filter</summary>
        HighShelf
    End Enum

    Private Structure BiQuad
        Public a0 As Double
        Public a1 As Double
        Public a2 As Double
        Public a3 As Double
        Public a4 As Double

        Public x1 As Double
        Public x2 As Double

        Public y1 As Double
        Public y2 As Double
    End Structure

    Private mBiQuad As New BiQuad()
    Private mFilterType As FiltersTypes
    Private mFrequency As Double
    Private mSamplingRate As Integer
    Private mBandWidth As Double
    Private mGain As Double
    Private mEnabled As Boolean

    Public Sub New()
        mEnabled = True
    End Sub

    Public Sub New(filterType As FiltersTypes, gain As Double, frequency As Double, samplingRate As Integer, bandWidth As Double)
        Me.New()

        mFilterType = filterType
        mFrequency = frequency
        mSamplingRate = samplingRate
        mBandWidth = bandWidth
        mGain = gain

        InitiaizeFilter()
    End Sub

    Public Property Enabled As Boolean
        Get
            Return mEnabled
        End Get
        Set(value As Boolean)
            mEnabled = value
        End Set
    End Property

    Public Property FilterType As FiltersTypes
        Get
            Return mFilterType
        End Get
        Set(value As FiltersTypes)
            mFilterType = value
            InitiaizeFilter()
        End Set
    End Property

    Public Property Gain As Double
        Get
            Return mGain
        End Get
        Set(value As Double)
            mGain = value
            InitiaizeFilter()
        End Set
    End Property

    Public Property Frequency As Double
        Get
            Return mFrequency
        End Get
        Set(value As Double)
            mFrequency = value
            InitiaizeFilter()
        End Set
    End Property

    Public Property SamplingRate As Integer
        Get
            Return mSamplingRate
        End Get
        Set(value As Integer)
            mSamplingRate = value
            InitiaizeFilter()
        End Set
    End Property

    Public Property BandWidth As Double
        Get
            Return mBandWidth
        End Get
        Set(value As Double)
            mBandWidth = value
            InitiaizeFilter()
        End Set
    End Property

    Public Function ApplyFilter(value As Double) As Double
        If mEnabled Then
            Dim result As Double = mBiQuad.a0 * value +
                                   mBiQuad.a1 * mBiQuad.x1 +
                                   mBiQuad.a2 * mBiQuad.x2 -
                                   mBiQuad.a3 * mBiQuad.y1 -
                                   mBiQuad.a4 * mBiQuad.y2

            ' shift x1 to x2, sample to x1
            mBiQuad.x2 = mBiQuad.x1
            mBiQuad.x1 = value

            ' shift y1 to y2, result to y1
            mBiQuad.y2 = mBiQuad.y1
            mBiQuad.y1 = result

            Return result
        Else
            Return value
        End If
    End Function

    Private Sub InitiaizeFilter()
        Dim A As Double = Math.Pow(10, mGain / 40)
        Dim omega As Double = 2 * Math.PI * (mFrequency / 2) / mSamplingRate
        Dim sn As Double = Math.Sin(omega)
        Dim cs As Double = Math.Cos(omega)
        Dim alpha As Double = sn * Math.Sinh(Math.Log(2) / 2 * mBandWidth * (omega / sn))
        Dim beta As Double = Math.Sqrt(2 * A)

        Dim a0 As Double
        Dim a1 As Double
        Dim a2 As Double

        Dim b0 As Double
        Dim b1 As Double
        Dim b2 As Double

        Select Case FilterType
            Case FiltersTypes.LowPass
                b0 = (1 - cs) / 2
                b1 = 1 - cs
                b2 = (1 - cs) / 2
                a0 = 1 + alpha
                a1 = -2 * cs
                a2 = 1 - alpha

            Case FiltersTypes.HighPass
                b0 = (1 + cs) / 2
                b1 = -(1 + cs)
                b2 = (1 + cs) / 2
                a0 = 1 + alpha
                a1 = -2 * cs
                a2 = 1 - alpha

            Case FiltersTypes.BandPass
                b0 = alpha
                b1 = 0
                b2 = -alpha
                a0 = 1 + alpha
                a1 = -2 * cs
                a2 = 1 - alpha

            Case FiltersTypes.Notch
                b0 = 1
                b1 = -2 * cs
                b2 = 1
                a0 = 1 + alpha
                a1 = -2 * cs
                a2 = 1 - alpha

            Case FiltersTypes.PeakingBand
                b0 = 1 + (alpha * A)
                b1 = -2 * cs
                b2 = 1 - (alpha * A)
                a0 = 1 + (alpha / A)
                a1 = -2 * cs
                a2 = 1 - (alpha / A)

            Case FiltersTypes.LowShelf
                b0 = A * ((A + 1) - (A - 1) * cs + beta * sn)
                b1 = 2 * A * ((A - 1) - (A + 1) * cs)
                b2 = A * ((A + 1) - (A - 1) * cs - beta * sn)
                a0 = (A + 1) + (A - 1) * cs + beta * sn
                a1 = -2 * ((A - 1) + (A + 1) * cs)
                a2 = (A + 1) + (A - 1) * cs - beta * sn

            Case FiltersTypes.HighShelf
                b0 = A * ((A + 1) + (A - 1) * cs + beta * sn)
                b1 = -2 * A * ((A - 1) + (A + 1) * cs)
                b2 = A * ((A + 1) + (A - 1) * cs - beta * sn)
                a0 = (A + 1) - (A - 1) * cs + beta * sn
                a1 = 2 * ((A - 1) - (A + 1) * cs)
                a2 = (A + 1) - (A - 1) * cs - beta * sn
        End Select

        mBiQuad.a0 = b0 / a0
        mBiQuad.a1 = b1 / a0
        mBiQuad.a2 = b2 / a0
        mBiQuad.a3 = a1 / a0
        mBiQuad.a4 = a2 / a0

        mBiQuad.x1 = 0
        mBiQuad.x2 = 0
        mBiQuad.y1 = 0
        mBiQuad.y2 = 0
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return New BiQuadFilter(mFilterType, mGain, mFrequency, mSamplingRate, mBandWidth)
    End Function
End Class