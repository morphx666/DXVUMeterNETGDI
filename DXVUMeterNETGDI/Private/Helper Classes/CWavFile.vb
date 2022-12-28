Imports System.Math
Imports SlimDX.Multimedia

Partial Public Class DXVUMeterNETGDI
    Private Class CWavFile
        Public File As IO.FileStream
        Public Header As WAVEFileHeader
        Private mParent As DXVUMeterNETGDI

        Public Sub New(Parent As DXVUMeterNETGDI)
            mParent = Parent
        End Sub

        Public Sub Open(FilePath As String)
            Dim Exists As Boolean = IO.File.Exists(FilePath)
            If Exists Then
                If File IsNot Nothing Then File.Close()
                File = New IO.FileStream(FilePath, IO.FileMode.OpenOrCreate)
                ReadHeader()
            End If
        End Sub

        Public Sub Create(FilePath As String)
            Dim Exists As Boolean = IO.File.Exists(FilePath)
            File = New IO.FileStream(FilePath, IO.FileMode.OpenOrCreate)
            If Not Exists Then WriteHeader(False)
        End Sub

        Public ReadOnly Property IsValid() As Boolean
            Get
                Return File.CanWrite AndAlso File.CanSeek AndAlso File.CanRead
            End Get
        End Property

        Public Sub Write(b() As Byte)
            File.Write(b, 0, b.Length)
        End Sub

        Public Sub Write(b As Long)
            File.Write(BitConverter.GetBytes(b), 0, 8)
        End Sub

        Public Sub Write(b As Integer)
            File.Write(BitConverter.GetBytes(b), 0, 4)
        End Sub

        Public Sub Write(b As Short)
            File.Write(BitConverter.GetBytes(b), 0, 2)
        End Sub

        Public Sub WriteHeader(Close As Boolean)
            Dim curPos As Long = File.Position
            Dim headerLength As Integer = Len(Header)

            If File.Length < headerLength Then File.SetLength(headerLength)

            If Close Then
                Do While (File.Length Mod mParent.capBuf.Format.BlockAlignment) > 0
                    Write(New Byte() {0})
                Loop
            End If

            File.Seek(0, IO.SeekOrigin.Begin)

            With Header
                .dwFileSize = CInt(File.Length - 8)
                .dwFormatLength = 16
                .wFormatTag = CShort(mParent.capBuf.Format.FormatTag)
                .nChannels = mParent.capBuf.Format.Channels
                .nSamplesPerSec = mParent.capBuf.Format.SamplesPerSecond
                .nAvgBytesPerSec = mParent.capBuf.Format.AverageBytesPerSecond
                .nBlockAlign = mParent.capBuf.Format.BlockAlignment
                .wBitsPerSample = mParent.capBuf.Format.BitsPerSample
                '.dwDataLength = curPos - headerLength - 2
                .dwDataLength = .dwFileSize - headerLength - 8

                Write(WAVEFileHeader.dwRiff)
                Write(.dwFileSize)
                Write(WAVEFileHeader.dwWave)
                Write(WAVEFileHeader.dwFormat)
                Write(.dwFormatLength)
                Write(.wFormatTag)
                Write(.nChannels)
                Write(.nSamplesPerSec)
                Write(.nAvgBytesPerSec)
                Write(.nBlockAlign)
                Write(.wBitsPerSample)
                Write(WAVEFileHeader.dwData)
                Write(.dwDataLength)
            End With

            If Close Then File.Close()
        End Sub

        Private Sub Read(ByRef var As Integer)
            Dim b(3) As Byte
            File.Read(b, 0, 4)
            'Array.Reverse(b)
            var = BitConverter.ToInt32(b, 0)
        End Sub

        Private Sub Read(ByRef var As Short)
            Dim b(1) As Byte
            File.Read(b, 0, 2)
            'Array.Reverse(b)
            var = BitConverter.ToInt16(b, 0)
        End Sub

        Public Function ReadHeader() As WAVEFileHeader
            File.Seek(16 - 4, IO.SeekOrigin.Begin)

            With Header
                'Read(.dwRiff)
                Read(.dwFileSize)
                'Read(.dwWave)
                'Read(.dwFormat)
                Read(.dwFormatLength)
                Read(.wFormatTag)
                Read(.nChannels)
                Read(.nSamplesPerSec)
                Read(.nAvgBytesPerSec)
                Read(.nBlockAlign)
                Read(.wBitsPerSample)
                'Read(.dwData)
                Read(.dwDataLength)
            End With

            Return Header
        End Function

        Public Function GetD9Header() As WaveFormat
            Dim h As New WaveFormat()

            With h
                .Channels = Header.nChannels
                .SamplesPerSecond = Header.nSamplesPerSec
                .BitsPerSample = Header.wBitsPerSample
                .BlockAlignment = CShort(.Channels * .BitsPerSample / 8)
                .AverageBytesPerSecond = .SamplesPerSecond * .BlockAlignment
                .FormatTag = WaveFormatTag.Pcm
            End With

            Return h
        End Function

        Public Function ReadAudioData(n As Integer) As Byte()
            Dim b(n - 1) As Byte
            File.Read(b, 0, n)
            Return b
        End Function
    End Class
End Class
