Partial Public Class DXVUMeterNETGDI
    Partial Public Class DevicesCollection
        Partial Public Class Device
            Partial Public Class QualitiesCollection
                ''' <summary>Provides access to the device's recording parameters</summary>
                Public Class Quality
                    Dim pFrequency As Integer
                    Dim pBitDepth As Integer
                    Dim pChannels As Integer
                    Dim pToString As String

#Region " Public Properties "
                    ''' <summary>Returns the supported frequency</summary>
                    Public ReadOnly Property Frequency() As Integer
                        Get
                            Return pFrequency
                        End Get
                    End Property

                    ''' <summary>Returns the supported quality or resolution</summary>
                    Public ReadOnly Property BitDepth() As Integer
                        Get
                            Return pBitDepth
                        End Get
                    End Property

                    ''' <summary>Returns the supported number of channels</summary>
                    Public ReadOnly Property Channels() As Integer
                        Get
                            Return pChannels
                        End Get
                    End Property

                    ''' <summary>Returns a formatted representation of the supported parameters</summary>
                    Public Overrides Function ToString() As String
                        Return pToString
                    End Function
#End Region

                    Friend Sub New(Frequency As Integer, BitDepth As Short, Channels As Short, qstr As String)
                        pFrequency = Frequency
                        pBitDepth = BitDepth
                        pChannels = Channels
                        pToString = qstr
                    End Sub

                    Protected Overrides Sub Finalize()
                        pToString = Nothing
                        MyBase.Finalize()
                    End Sub
                End Class
            End Class
        End Class
    End Class
End Class
