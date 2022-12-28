Imports System
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization
Imports System.ComponentModel
Imports NDXVUMeterNET.DXVUMeterNETGDI.MP3EncoderConfiguration
Imports SlimDX.Multimedia

Partial Public Class DXVUMeterNETGDI
    ''' <summary>
    ''' This class provides configuration options to be used with the internal MP3 encoder
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MP3EncoderConfiguration
        ''' <summary>
        ''' Defines the possible enconding bit rates
        ''' </summary>
        ''' <remarks>
        ''' Constants that start with the CBR prefix indicate "Constant Bit Rates"
        ''' Constants that start with the VBR prefix indicate "Variable Bit Rates".
        ''' See the <see cref="VBRQualityConstants">VBRQualityConstants</see> enumeration for a list of possible qualities that can be used on variable bit rate encoded files
        ''' </remarks>
        Public Enum BitRateConstants
            CBR8 = 8
            CBR16 = 16
            CBR24 = 24
            CBR32 = 32
            CBR40 = 40
            CBR48 = 48
            CBR56 = 56
            CBR64 = 64
            CBR80 = 80
            CBR96 = 96
            CBR112 = 112
            CBR128 = 128
            CBR144 = 144
            CBR160 = 160
            CBR192 = 192
            CBR224 = 224
            CBR256 = 256
            CBR320 = 320
            VBR_Default = -1
            VBR_AverageBitRate = -2
            VBR_MTRH = -3
            VBR_Old = -4
            VBR_New = -5
        End Enum

        ''' <summary>
        ''' Defines a set of built-in encoding presets
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum PresetConstants
            NoPreset = -1
            NormalQuality = 0
            LowQuality = 1
            HighQuality = 2
            VoiceQuality = 3
            R3Mix = 4
            VeyHighQuality = 5
            Standard = 6
            FastStandard = 7
            Extreme = 8
            FastExtreme = 9
            Insane = 10
            VariableBitRate = 11
            ConstantBitRate = 12
            Medium = 13
            FastMedium = 14
            Phone = 1000
            ShortWave = 2000
            AM = 3000
            FM = 4000
            Voice = 5000
            Radio = 6000
            Tape = 7000
            Hifi = 8000
            CD = 9000
            Studio = 10000
        End Enum

        ''' <summary>
        ''' Defines the quality of variable bit rate encoded file
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum VBRQualityConstants
            Quality0 = 0 'Max
            Quality1 = 1
            Quality2 = 2
            Quality3 = 3
            Quality4 = 4
            Quality5 = 5
            Quality6 = 6
            Quality7 = 7
            Quality8 = 8
            Quality9 = 9 'Min
        End Enum

        Private mBitRate As BitRateConstants = BitRateConstants.CBR128
        Private mPreset As PresetConstants = PresetConstants.NormalQuality
        Private mMaxBitrate As BitRateConstants = BitRateConstants.CBR128
        Private mAverageBitrate As BitRateConstants = BitRateConstants.CBR128
        Private mVBRQuality As VBRQualityConstants = VBRQualityConstants.Quality0

        Public Sub New()

        End Sub

        Public Sub New(BitRate As BitRateConstants)
            mBitRate = BitRate
        End Sub

        ''' <summary>Gets or sets the encoding BitRate</summary>
        Public Property BitRate() As BitRateConstants
            Get
                Return mBitRate
            End Get
            Set(value As BitRateConstants)
                mBitRate = value
            End Set
        End Property

        ''' <summary>Gets or sets the preset to be used to encode the file</summary>
        Public Property Preset() As PresetConstants
            Get
                Return mPreset
            End Get
            Set(value As PresetConstants)
                mPreset = value
            End Set
        End Property

        ''' <summary>Gets or sets the maximum encoding BitRate</summary>
        Public Property MaxBitrate() As BitRateConstants
            Get
                Return mMaxBitrate
            End Get
            Set(value As BitRateConstants)
                mMaxBitrate = value
            End Set
        End Property

        ''' <summary>Gets or sets the average encoding BitRate</summary>
        Public Property AverageBitrate() As BitRateConstants
            Get
                Return mAverageBitrate
            End Get
            Set(value As BitRateConstants)
                mAverageBitrate = value
            End Set
        End Property

        ''' <summary>Gets or sets the variable bit rate quality</summary>
        Public Property VBRQuality() As VBRQualityConstants
            Get
                Return mVBRQuality
            End Get
            Set(value As VBRQualityConstants)
                mVBRQuality = value
            End Set
        End Property

        ''' <summary>Gets the version of the LAME encoder found on the system</summary>
        Public Shared ReadOnly Property EncoderVersion() As String
            Get
                Return CLame.EncoderVersion
            End Get
        End Property
    End Class

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
    Private Class CLame
        Private mInputSamples As UInteger
        Private mOutBufferSize As UInteger
        Private mhLameStream As UInteger

        Private mInBufferPos As Integer
        Private mInBuffer() As Byte
        Private mOutBuffer() As Byte

        Private lConfig As BE_CONFIG

        Public Sub New()

        End Sub

        Public Sub New(Format As WaveFormat, Optional EncoderConfig As MP3EncoderConfiguration = Nothing)
            lConfig = New BE_CONFIG(Format)

            If EncoderConfig IsNot Nothing Then
                Select Case EncoderConfig.BitRate
                    Case BitRateConstants.VBR_Default, BitRateConstants.VBR_AverageBitRate, BitRateConstants.VBR_MTRH, BitRateConstants.VBR_New, BitRateConstants.VBR_Old
                        lConfig.format.lhv1.bEnableVBR = 1
                        lConfig.format.lhv1.dwMaxBitrate = CUInt(EncoderConfig.MaxBitrate)
                        lConfig.format.lhv1.dwVbrAbr_bps = CUInt(EncoderConfig.AverageBitrate)
                        lConfig.format.lhv1.dwBitrate = CUInt(EncoderConfig.MaxBitrate)
                        lConfig.format.lhv1.nVBRQuality = EncoderConfig.VBRQuality
                        Select Case EncoderConfig.BitRate
                            Case BitRateConstants.VBR_Default
                                lConfig.format.lhv1.nVbrMethod = VBRMETHOD.VBR_METHOD_DEFAULT
                            Case BitRateConstants.VBR_AverageBitRate
                                lConfig.format.lhv1.nVbrMethod = VBRMETHOD.VBR_METHOD_ABR
                            Case BitRateConstants.VBR_MTRH
                                lConfig.format.lhv1.nVbrMethod = VBRMETHOD.VBR_METHOD_MTRH
                            Case BitRateConstants.VBR_New
                                lConfig.format.lhv1.nVbrMethod = VBRMETHOD.VBR_METHOD_NEW
                            Case BitRateConstants.VBR_Old
                                lConfig.format.lhv1.nVbrMethod = VBRMETHOD.VBR_METHOD_OLD
                        End Select
                    Case Else
                        lConfig.format.lhv1.bEnableVBR = 0
                End Select
                lConfig.format.lhv1.nPreset = CType(EncoderConfig.Preset, LAME_QUALITY_PRESET)
            End If

            If Format.wBitsPerSample = 8 Then
                Throw New ArgumentOutOfRangeException("Only 16 bits formats are supported")
            End If

            Select Case Format.nChannels
                Case 1
                    lConfig.format.lhv1.nMode = MpegMode.MONO
                Case 2
                    lConfig.format.lhv1.nMode = MpegMode.STEREO
            End Select

            Select Case Format.nSamplesPerSec
                Case 16000, 22050, 24000
                    lConfig.format.lhv1.dwMpegVersion = LHV1.MPEG2
                Case 32000, 44100, 48000
                    lConfig.format.lhv1.dwMpegVersion = LHV1.MPEG1
            End Select

            Select Case lConfig.format.lhv1.dwBitrate
                Case 192, 224, 256, 320
                    If lConfig.format.lhv1.dwMpegVersion <> LHV1.MPEG1 Then
                        Throw New ArgumentOutOfRangeException(String.Format("BitRate of {0} bps is not compatible with input format of {1} KHz", lConfig.format.lhv1.dwBitrate, Format.nSamplesPerSec))
                    End If
                Case 8, 16, 24, 144
                    If lConfig.format.lhv1.dwMpegVersion <> LHV1.MPEG2 Then
                        Throw New ArgumentOutOfRangeException(String.Format("BitRate of {0} bps is not compatible with input format of {1} KHz", lConfig.format.lhv1.dwBitrate, Format.nSamplesPerSec))
                    End If
            End Select

            InitLame()
        End Sub

        Public Sub New(LameConfig As BE_CONFIG)
            lConfig = LameConfig
            InitLame()
        End Sub

        Private Sub InitLame()
            Dim LameResult As UInt32 = Lame_encDll.beInitStream(lConfig, mInputSamples, mOutBufferSize, mhLameStream)
            If LameResult = Lame_encDll.BE_ERR_SUCCESSFUL Then
                ReDim mInBuffer(CInt(mInputSamples * 2))
                ReDim mOutBuffer(CInt(mOutBufferSize))
            Else
                Throw New ApplicationException(String.Format("Error {0}:" + vbCrLf + "MP3 Encoder failed to initialize", LameResult))
            End If
        End Sub

        Public Function Encode(b() As Byte) As Byte()
            Dim ToCopy As Integer = 0
            Dim EncodedSize As System.UInt32 = 0
            Dim LameResult As System.UInt32
            Dim count As Integer = b.Length
            Dim index As Integer = 0
            Dim outBuf() As Byte

            While count > 0
                If mInBufferPos > 0 Then
                    ToCopy = Math.Min(count, mInBuffer.Length - mInBufferPos)
                    Buffer.BlockCopy(b, index, mInBuffer, mInBufferPos, ToCopy)
                    mInBufferPos += ToCopy
                    index += ToCopy
                    count -= ToCopy
                    If mInBufferPos >= mInBuffer.Length Then
                        mInBufferPos = 0
                        LameResult = Lame_encDll.EncodeChunk(mhLameStream, mInBuffer, mOutBuffer, EncodedSize)
                        If LameResult = Lame_encDll.BE_ERR_SUCCESSFUL Then
                            If EncodedSize > 0 Then
                                ReDim outBuf(CInt(EncodedSize - 1))
                                Buffer.BlockCopy(mOutBuffer, 0, outBuf, 0, CInt(EncodedSize))
                                Return outBuf
                            End If
                        Else
                            Throw New ApplicationException(String.Format("ERROR {0}:" + vbCrLf + "Encoder(1) failed", LameResult))
                        End If
                    End If
                Else
                    If count >= mInBuffer.Length Then
                        LameResult = Lame_encDll.EncodeChunk(mhLameStream, b, index, CUInt(b.Length), mOutBuffer, EncodedSize)
                        If LameResult = Lame_encDll.BE_ERR_SUCCESSFUL Then
                            If EncodedSize > 0 Then
                                ReDim outBuf(CInt(EncodedSize - 1))
                                Buffer.BlockCopy(mOutBuffer, 0, outBuf, 0, CInt(EncodedSize))
                                Return outBuf
                            End If
                        Else
                            Throw New ApplicationException(String.Format("ERROR {0}:" + vbCrLf + "Encoder(2) failed", LameResult))
                        End If
                        count -= mInBuffer.Length
                        index += mInBuffer.Length
                    Else
                        Buffer.BlockCopy(b, index, mInBuffer, 0, count)
                        mInBufferPos = count
                        index += count
                        count = 0
                    End If
                End If
            End While

            Return Nothing
        End Function

        Public Function Close() As Byte()
            Dim outBuf() As Byte = Nothing

            Try
                Dim EncodedSize As System.UInt32 = 0
                If mInBufferPos > 0 Then
                    If Lame_encDll.EncodeChunk(mhLameStream, mInBuffer, 0, CType(mInBufferPos, System.UInt32), mOutBuffer, EncodedSize) = Lame_encDll.BE_ERR_SUCCESSFUL Then
                        If EncodedSize > 0 Then
                            ReDim outBuf(CInt(EncodedSize - 1))
                            Buffer.BlockCopy(mOutBuffer, 0, outBuf, 0, CInt(EncodedSize))
                        End If
                    End If
                End If
                EncodedSize = 0
                If Lame_encDll.beDeinitStream(mhLameStream, mOutBuffer, EncodedSize) = Lame_encDll.BE_ERR_SUCCESSFUL Then
                    If EncodedSize > 0 Then
                        Dim offset As Integer = 0
                        If outBuf Is Nothing Then
                            ReDim outBuf(CInt(EncodedSize - 1))
                        Else
                            offset = outBuf.Length
                            ReDim Preserve outBuf(CInt(outBuf.Length + EncodedSize - 1))
                        End If
                        Buffer.BlockCopy(mOutBuffer, 0, outBuf, offset, CInt(EncodedSize))
                    End If
                End If
            Finally
                Lame_encDll.beCloseStream(mhLameStream)
            End Try

            If outBuf Is Nothing Then
                Return Nothing
            Else
                Return outBuf
            End If
        End Function

        Public Shared Function EncoderVersion() As String
            Dim ver As BE_VERSION = New BE_VERSION
            Lame_encDll.beVersion(ver)

            Return "LAME Encoder " & ver.byMajorVersion & "." & ver.byMinorVersion & vbCrLf & ver.zHomepage
        End Function

        Public Enum VBRMETHOD
            VBR_METHOD_NONE = -1
            VBR_METHOD_DEFAULT = 0
            VBR_METHOD_OLD = 1
            VBR_METHOD_NEW = 2
            VBR_METHOD_MTRH = 3
            VBR_METHOD_ABR = 4
        End Enum

        Public Enum MpegMode
            STEREO = 0
            JOINT_STEREO
            DUAL_CHANNEL
            MONO
            NOT_SET
            MAX_INDICATOR
        End Enum

        Public Enum LAME_QUALITY_PRESET
            LQP_NOPRESET = -1
            LQP_NORMAL_QUALITY = 0
            LQP_LOW_QUALITY = 1
            LQP_HIGH_QUALITY = 2
            LQP_VOICE_QUALITY = 3
            LQP_R3MIX = 4
            LQP_VERYHIGH_QUALITY = 5
            LQP_STANDARD = 6
            LQP_FAST_STANDARD = 7
            LQP_EXTREME = 8
            LQP_FAST_EXTREME = 9
            LQP_INSANE = 10
            LQP_ABR = 11
            LQP_CBR = 12
            LQP_MEDIUM = 13
            LQP_FAST_MEDIUM = 14
            LQP_PHONE = 1000
            LQP_SW = 2000
            LQP_AM = 3000
            LQP_FM = 4000
            LQP_VOICE = 5000
            LQP_RADIO = 6000
            LQP_TAPE = 7000
            LQP_HIFI = 8000
            LQP_CD = 9000
            LQP_STUDIO = 10000
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Public Class WaveFormat
            Public wFormatTag As Short
            Public nChannels As Short
            Public nSamplesPerSec As Integer
            Public nAvgBytesPerSec As Integer
            Public nBlockAlign As Short
            Public wBitsPerSample As Short
            Public cbSize As Short

            Public Sub New(rate As Integer, bits As Integer, channels As Integer)
                wFormatTag = CType(WaveFormatTag.Pcm, Short)
                nChannels = CType(channels, Short)
                nSamplesPerSec = rate
                wBitsPerSample = CType(bits, Short)
                cbSize = 0
                nBlockAlign = CType((channels * (bits / 8)), Short)
                nAvgBytesPerSec = nSamplesPerSec * nBlockAlign
            End Sub
        End Class

        <StructLayout(LayoutKind.Sequential), Serializable()>
        Public Structure MP3
            Public dwSampleRate As System.UInt32
            Public byMode As Byte
            Public wBitrate As System.UInt16
            Public bPrivate As Integer
            Public bCRC As Integer
            Public bCopyright As Integer
            Public bOriginal As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential, Size:=327), Serializable()>
        Public Structure LHV1
            Public Const MPEG1 As System.UInt32 = 1
            Public Const MPEG2 As System.UInt32 = 0
            Public dwStructVersion As System.UInt32
            Public dwStructSize As System.UInt32
            Public dwSampleRate As System.UInt32
            Public dwReSampleRate As System.UInt32
            Public nMode As MpegMode
            Public dwBitrate As System.UInt32
            Public dwMaxBitrate As System.UInt32
            Public nPreset As LAME_QUALITY_PRESET
            Public dwMpegVersion As System.UInt32
            Public dwPsyModel As System.UInt32
            Public dwEmphasis As System.UInt32
            Public bPrivate As Integer
            Public bCRC As Integer
            Public bCopyright As Integer
            Public bOriginal As Integer
            Public bWriteVBRHeader As Integer
            Public bEnableVBR As Integer
            Public nVBRQuality As Integer
            Public dwVbrAbr_bps As System.UInt32
            Public nVbrMethod As VBRMETHOD
            Public bNoRes As Integer
            Public bStrictIso As Integer
            Public nQuality As System.UInt16

            Public Sub New(format As WaveFormat, MpeBitRate As System.UInt32)
                If Not (format.wFormatTag = CType(WaveFormatTag.Pcm, Short)) Then
                    Throw New ArgumentOutOfRangeException("format", "Only PCM format supported")
                End If
                If Not (format.wBitsPerSample = 16) Then
                    Throw New ArgumentOutOfRangeException("format", "Only 16 bits samples supported")
                End If
                dwStructVersion = 1
                dwStructSize = CType(Marshal.SizeOf(GetType(BE_CONFIG)), System.UInt32)
                Select Case format.nSamplesPerSec
                    Case 16000, 22050, 24000
                        dwMpegVersion = MPEG2
                        ' break
                    Case 32000, 44100, 48000
                        dwMpegVersion = MPEG1
                        ' break
                    Case Else
                        Throw New ArgumentOutOfRangeException("format", "Unsupported sample rate")
                End Select
                dwSampleRate = CType(format.nSamplesPerSec, System.UInt32)
                dwReSampleRate = 0
                Select Case format.nChannels
                    Case 1
                        nMode = MpegMode.MONO
                        ' break
                    Case 2
                        nMode = MpegMode.STEREO
                        ' break
                    Case Else
                        Throw New ArgumentOutOfRangeException("format", "Invalid number of channels")
                End Select
                Select Case MpeBitRate
                    Case 32, 40, 48, 56, 64, 80, 96, 112, 128, 160
                        ' break
                    Case 192, 224, 256, 320
                        If Not (dwMpegVersion = MPEG1) Then
                            Throw New ArgumentOutOfRangeException("MpsBitRate", "Bit rate not compatible with input format")
                        End If
                        ' break
                    Case 8, 16, 24, 144
                        If Not (dwMpegVersion = MPEG2) Then
                            Throw New ArgumentOutOfRangeException("MpsBitRate", "Bit rate not compatible with input format")
                        End If
                        ' break
                    Case Else
                        Throw New ArgumentOutOfRangeException("MpsBitRate", "Unsupported bit rate")
                End Select
                dwBitrate = MpeBitRate
                nPreset = LAME_QUALITY_PRESET.LQP_NORMAL_QUALITY
                dwPsyModel = 0
                dwEmphasis = 0
                bOriginal = 1
                bWriteVBRHeader = 0
                bNoRes = 0
                bCopyright = 0
                bCRC = 0
                bEnableVBR = 0
                bPrivate = 0
                bStrictIso = 0
                dwMaxBitrate = 0
                dwVbrAbr_bps = 0
                nQuality = 0
                nVbrMethod = VBRMETHOD.VBR_METHOD_NONE
                nVBRQuality = 0
            End Sub
        End Structure

        <StructLayout(LayoutKind.Sequential), Serializable()>
        Public Structure ACC
            Public dwSampleRate As System.UInt32
            Public byMode As Byte
            Public wBitrate As System.UInt16
            Public byEncodingMethod As Byte
        End Structure

        <StructLayout(LayoutKind.Explicit), Serializable()>
        Public Class Format
            <FieldOffset(0)>
            Public mp3 As MP3
            <FieldOffset(0)>
            Public lhv1 As LHV1
            <FieldOffset(0)>
            Public acc As ACC

            Public Sub New(format As WaveFormat, MpeBitRate As System.UInt32)
                lhv1 = New LHV1(format, MpeBitRate)
            End Sub
        End Class

        <StructLayout(LayoutKind.Sequential), Serializable()>
        Public Class BE_CONFIG
            Public Const BE_CONFIG_MP3 As System.UInt32 = 0
            Public Const BE_CONFIG_LAME As System.UInt32 = 256
            Public dwConfig As System.UInt32
            Public format As Format

            Public Sub New(format As WaveFormat, MpeBitRate As System.UInt32)
                Me.dwConfig = BE_CONFIG_LAME
                Me.format = New Format(format, MpeBitRate)
            End Sub

            Public Sub New(format As WaveFormat)
                MyClass.New(format, 128)
            End Sub
        End Class

        <StructLayout(LayoutKind.Sequential)>
        Public Class BE_VERSION
            Public Const BE_MAX_HOMEPAGE As System.UInt32 = 256
            Public byDLLMajorVersion As Byte
            Public byDLLMinorVersion As Byte
            Public byMajorVersion As Byte
            Public byMinorVersion As Byte
            Public byDay As Byte
            Public byMonth As Byte
            Public wYear As System.UInt16
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=257)> Public zHomepage As String
            Public byAlphaLevel As Byte
            Public byBetaLevel As Byte
            Public byMMXEnabled As Byte
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=125)> Public btReserved As Byte()

            Public Sub New()
                btReserved = New Byte(125) {}
            End Sub
        End Class

        Public Class Lame_encDll
            Public Const BE_ERR_SUCCESSFUL As System.UInt32 = 0
            Public Const BE_ERR_INVALID_FORMAT As System.UInt32 = 1
            Public Const BE_ERR_INVALID_FORMAT_PARAMETERS As System.UInt32 = 2
            Public Const BE_ERR_NO_MORE_HANDLES As System.UInt32 = 3
            Public Const BE_ERR_INVALID_HANDLE As System.UInt32 = 4

            <DllImport("lame_enc.dll")>
            Public Shared Function beInitStream(pbeConfig As BE_CONFIG, ByRef dwSamples As System.UInt32, ByRef dwBufferSize As System.UInt32, ByRef phbeStream As System.UInt32) As System.UInt32
            End Function

            <DllImport("lame_enc.dll")>
            Public Shared Function beEncodeChunk(hbeStream As System.UInt32, nSamples As System.UInt32, pInSamples As Short(), <[In](), Out()> pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
            End Function

            <DllImport("lame_enc.dll")>
            Protected Shared Function beEncodeChunk(hbeStream As System.UInt32, nSamples As System.UInt32, pSamples As IntPtr, <[In](), Out()> pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
            End Function

            Public Shared Function EncodeChunk(hbeStream As System.UInt32, buffer As Byte(), index As Integer, nBytes As System.UInt32, pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
                Dim res As System.UInt32
                Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
                Try
                    Dim ptr As IntPtr = CType((handle.AddrOfPinnedObject.ToInt32 + index), IntPtr)
                    res = beEncodeChunk(hbeStream, CUInt(nBytes / 2), ptr, pOutput, pdwOutput)

                    'Dim b() As Short = Byte2Short(buffer, 16, 2)
                    'res = beEncodeChunk(hbeStream, b.Length,  b, pOutput, pdwOutput)
                Catch
                Finally
                    'handle.Free()
                End Try
                Return res
            End Function

            'Private Shared Function Byte2Short(src() As Byte, pBitDepth As Short, pChannels As Short) As Short()
            '    Dim dataSize As Integer = pBitDepth / 8
            '    Dim bb(src.Length / dataSize) As Short
            '    Dim cycleStep As Integer = IIf(pBitDepth = 8, pChannels, pChannels * 2)
            '    Dim rtChOffset As Integer = IIf(pChannels = 1, 0, dataSize)
            '    Dim tmpB(dataSize) As Byte
            '    Dim bbStep As Integer = 0

            '    For i As Integer = 0 To src.Length - cycleStep Step cycleStep
            '        Select Case pBitDepth
            '            Case 8
            '                bb(bbStep) = (128 - src(i)) * 256

            '                If pChannels = 2 Then
            '                    bb(bbStep + 1) = (128 - src(i + rtChOffset)) * 256
            '                    bbStep += 2
            '                Else
            '                    bbStep += 1
            '                End If
            '            Case 16
            '                Array.Copy(src, i, tmpB, 0, dataSize)
            '                bb(bbStep) = System.BitConverter.ToInt16(tmpB, 0)

            '                If pChannels = 2 Then
            '                    Array.Copy(src, i + rtChOffset, tmpB, 0, dataSize)
            '                    bb(bbStep + 1) = System.BitConverter.ToInt16(tmpB, 0)
            '                    bbStep += 2
            '                Else
            '                    bbStep += 1
            '                End If
            '        End Select
            '    Next i

            '    Return bb
            'End Function

            Public Shared Function EncodeChunk(hbeStream As System.UInt32, buffer As Byte(), pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
                Return EncodeChunk(hbeStream, buffer, 0, CType(buffer.Length, System.UInt32), pOutput, pdwOutput)
            End Function

            <DllImport("lame_enc.dll")>
            Public Shared Function beDeinitStream(hbeStream As System.UInt32, <[In](), Out()> pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
            End Function

            <DllImport("lame_enc.dll")>
            Public Shared Function beCloseStream(hbeStream As System.UInt32) As System.UInt32
            End Function

            <DllImport("lame_enc.dll")>
            Public Shared Sub beVersion(<Out()> pbeVersion As BE_VERSION)
            End Sub

            <DllImport("lame_enc.dll")>
            Public Shared Sub beWriteVBRHeader(pszMP3FileName As String)
            End Sub

            <DllImport("lame_enc.dll")>
            Public Shared Function beEncodeChunkFloatS16NI(hbeStream As System.UInt32, nSamples As System.UInt32, <[In]()> buffer_l As Double(), <[In]()> buffer_r As Double(), <[In](), Out()> pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
            End Function

            <DllImport("lame_enc.dll")>
            Public Shared Function beFlushNoGap(hbeStream As System.UInt32, <[In](), Out()>
            pOutput As Byte(), ByRef pdwOutput As System.UInt32) As System.UInt32
            End Function

            <DllImport("lame_enc.dll")>
            Public Shared Function beWriteInfoTag(hbeStream As System.UInt32, lpszFileName As String) As System.UInt32
            End Function
        End Class
    End Class
End Class