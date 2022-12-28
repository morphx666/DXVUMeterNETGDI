Imports System.Threading
Imports System.ComponentModel

Public Class CWinamp
    Private Const WM_USER = &H400
    Private Const WM_COPYDATA = &H4A
    Private Const WM_COMMAND = &H111
    Private Const PROCESS_VM_READ = &H10

    Private Const IPC_GETVERSION = 0
    Private Const IPC_GETREGISTEREDVERSION = 770
    Private Const IPC_PLAYFILE = 100
    Private Const IPC_ENQUEUEFILE = 100
    Private Const IPC_DELETE = 101
    Private Const IPC_DELETE_INT = 1101
    Private Const IPC_STARTPLAY = 102
    Private Const IPC_CHDIR = 103
    Private Const IPC_ISPLAYING = 104
    Private Const IPC_GETOUTPUTTIME = 105
    Private Const IPC_JUMPTOTIME = 106
    Private Const IPC_GETMODULENAME = 109
    Private Const IPC_EX_ISRIGHTEXE = 666
    Private Const IPC_WRITEPLAYLIST = 120
    Private Const IPC_SETPLAYLISTPOS = 121
    Private Const IPC_SETVOLUME = 122
    Private Const IPC_SETPANNING = 123
    Private Const IPC_GETLISTLENGTH = 124
    Private Const IPC_GETLISTPOS = 125
    Private Const IPC_GETINFO = 126
    Private Const IPC_GETEQDATA = 127
    Private Const IPC_SETEQDATA = 128
    Private Const IPC_ADDBOOKMARK = 129
    Private Const IPC_INSTALLPLUGIN = 130
    Private Const IPC_RESTARTWINAMP = 135
    Private Const IPC_ISFULLSTOP = 400
    Private Const IPC_INETAVAILABLE = 242
    Private Const IPC_UPDTITLE = 243
    Private Const IPC_REFRESHPLCACHE = 247
    Private Const IPC_GET_SHUFFLE = 250
    Private Const IPC_GET_REPEAT = 251
    Private Const IPC_SET_SHUFFLE = 252
    Private Const IPC_SET_REPEAT = 253
    Private Const IPC_ENABLEDISABLE_ALL_WINDOWS = 259
    Private Const IPC_GETWND = 260
    Private Const IPC_GETWND_EQ = 0
    Private Const IPC_GETWND_PE = 1
    Private Const IPC_GETWND_MB = 2
    Private Const IPC_GETWND_VIDEO = 3
    Private Const IPC_ISWNDVISIBLE = 261
    Private Const IPC_SETSKIN = 200
    Private Const IPC_GETSKIN = 201
    Private Const IPC_EXECPLUG = 202
    Private Const IPC_GETPLAYLISTFILE = 211
    Private Const IPC_GETPLAYLISTTITLE = 212
    Private Const IPC_GETHTTPGETTER = 240
    Private Const IPC_MBOPEN = 241
    Private Const IPC_CHANGECURRENTFILE = 245
    Private Const IPC_GETMBURL = 246
    Private Const IPC_MBBLOCK = 248
    Private Const IPC_MBOPENREAL = 249
    Private Const IPC_ADJUST_OPTIONSMENUPOS = 280
    Private Const IPC_GET_HMENU = 281
    Private Const IPC_GET_EXTENDED_FILE_INFO = 290
    Private Const IPC_GET_EXTENDED_FILE_INFO_HOOKABLE = 296
    Private Const IPC_GET_EXTLIST = 292
    Private Const IPC_INFOBOX = 293
    Private Const IPC_SET_EXTENDED_FILE_INFO = 294
    Private Const IPC_WRITE_EXTENDED_FILE_INFO = 295
    Private Const IPC_FORMAT_TITLE = 297
    Private Const IPC_GETUNCOMPRESSINTERFACE = 331
    Private Const IPC_ADD_PREFS_DLG = 332
    Private Const IPC_REMOVE_PREFS_DLG = 333
    Private Const IPC_OPENPREFSTOPAGE = 380
    Private Const IPC_GETINIFILE = 334
    Private Const IPC_GETINIDIRECTORY = 335
    Private Const IPC_SPAWNBUTTONPOPUP = 361
    Private Const IPC_OPENURLBOX = 360
    Private Const IPC_OPENFILEBOX = 362
    Private Const IPC_OPENDIRBOX = 363
    Private Const IPC_SETDIALOGBOXPARENT = 364
    Private Const IPC_GET_GENSKINBITMAP = 503
    Private Const IPC_GET_EMBEDIF = 505
    Private Const EMBED_FLAGS_NORESIZE = 1
    Private Const EMBED_FLAGS_NOTRANSPARENCY = 2
    Private Const IPC_EMBED_ENUM = 532
    Private Const IPC_EMBED_ISVALID = 533
    Private Const IPC_CONVERTFILE = 506
    Private Const IPC_CONVERTFILE_END = 507
    Private Const IPC_CONVERT_CONFIG = 508
    Private Const IPC_CONVERT_CONFIG_END = 509
    Private Const IPC_CONVERT_CONFIG_ENUMFMTS = 510
    Private Const IPC_BURN_CD = 511
    Private Const IPC_CONVERT_SET_PRIORITY = 512
    Private Const IPC_HOOK_TITLES = 850
    Private Const IPC_GETSADATAFUNC = 800
    Private Const IPC_ISMAINWNDVISIBLE = 900
    Private Const IPC_SETPLEDITCOLORS = 920
    Private Const IPC_SPAWNEQPRESETMENU = 933
    Private Const IPC_SPAWNFILEMENU = 934
    Private Const IPC_SPAWNOPTIONSMENU = 935
    Private Const IPC_SPAWNWINDOWSMENU = 936
    Private Const IPC_SPAWNHELPMENU = 937
    Private Const IPC_SPAWNPLAYMENU = 938
    Private Const IPC_SPAWNPEFILEMENU = 939
    Private Const IPC_SPAWNPEPLAYLISTMENU = 940
    Private Const IPC_SPAWNPESORTMENU = 941
    Private Const IPC_SPAWNPEHELPMENU = 942
    Private Const IPC_SPAWNMLFILEMENU = 943
    Private Const IPC_SPAWNMLVIEWMENU = 944
    Private Const IPC_SPAWNMLHELPMENU = 945
    Private Const IPC_SPAWNPELISTOFPLAYLISTS = 946
    Private Const WM_WA_SYSTRAY = WM_USER + 1
    Private Const WM_WA_MPEG_EOF = WM_USER + 2
    Private Const WINAMP_OPTIONS_EQ = 40036
    Private Const WINAMP_OPTIONS_PLEDIT = 40040
    Private Const WINAMP_VOLUMEUP = 40058
    Private Const WINAMP_VOLUMEDOWN = 40059
    Private Const WINAMP_FFWD5S = 40060
    Private Const WINAMP_REW5S = 40061
    Private Const WINAMP_BUTTON1 = 40044
    Private Const WINAMP_BUTTON2 = 40045
    Private Const WINAMP_BUTTON3 = 40046
    Private Const WINAMP_BUTTON4 = 40047
    Private Const WINAMP_BUTTON5 = 40048
    Private Const WINAMP_BUTTON1_SHIFT = 40144
    Private Const WINAMP_BUTTON2_SHIFT = 40145
    Private Const WINAMP_BUTTON3_SHIFT = 40146
    Private Const WINAMP_BUTTON4_SHIFT = 40147
    Private Const WINAMP_BUTTON5_SHIFT = 40148
    Private Const WINAMP_BUTTON1_CTRL = 40154
    Private Const WINAMP_BUTTON2_CTRL = 40155
    Private Const WINAMP_BUTTON3_CTRL = 40156
    Private Const WINAMP_BUTTON4_CTRL = 40157
    Private Const WINAMP_BUTTON5_CTRL = 40158
    Private Const WINAMP_FILE_PLAY = 40029
    Private Const WINAMP_FILE_DIR = 40187
    Private Const WINAMP_OPTIONS_PREFS = 40012
    Private Const WINAMP_OPTIONS_AOT = 40019
    Private Const WINAMP_HELP_ABOUT = 40041
    Private Const ID_MAIN_PLAY_AUDIOCD1 = 40323
    Private Const ID_MAIN_PLAY_AUDIOCD2 = 40323
    Private Const ID_MAIN_PLAY_AUDIOCD3 = 40323
    Private Const ID_MAIN_PLAY_AUDIOCD4 = 40323

    Private Structure COPYDATASTRUCT
        Dim dwData As Integer
        Dim cbData As Integer
        Dim lpData As String
    End Structure

    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Private mWinamp As System.Diagnostics.Process
    Private tmrFindWinamp As Timer
    Private mVolume As Integer

    Public Event RunningStateChanged()
    Public Event TitleChanged(ByVal Title As String)

    Public Sub New()
        tmrFindWinamp = New Timer(New TimerCallback(AddressOf FindWinamp), Nothing, 500, 500)
    End Sub

    Public ReadOnly Property IsRunning() As Boolean
        Get
            Return (mWinamp IsNot Nothing)
        End Get
    End Property

    Private Sub FindWinamp(ByVal state As Object)
        Static IsBusy As Boolean
        If IsBusy Then Exit Sub
        IsBusy = True

        Dim lastState As Boolean = (mWinamp Is Nothing)
        Dim ps() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcessesByName("winamp")

        Select Case ps.Length
            Case 0
                mWinamp = Nothing
            Case 1
                mWinamp = ps(0)
            Case Else
                mWinamp = Nothing
        End Select

        If lastState <> (mWinamp Is Nothing) Then
            RaiseEvent RunningStateChanged()
        Else
            If IsRunning Then
                Static lastTitle As String
                Dim cTitle As String = GetCurTitle
                If lastTitle <> cTitle Then
                    lastTitle = cTitle
                    RaiseEvent TitleChanged(cTitle)
                End If
            End If
        End If

        IsBusy = False
    End Sub

    Public ReadOnly Property GetCurTitle() As String
        Get
            If IsRunning() Then
                Dim wTitle As String = mWinamp.MainWindowTitle
                If wTitle <> "" Then
                    If Left(wTitle, "7") = "Winamp " Then
                        wTitle = "No Track is Playing"
                    Else
                        If InStr(wTitle, "***") Then
                            wTitle = Trim(Split(wTitle, "***")(1) + Split(wTitle, "***")(0))
                            wTitle = Mid(wTitle, InStr(wTitle, " ") + 1)
                            If InStr(wTitle, " - Win") Then wTitle = Left(wTitle, InStr(wTitle, " - Win") - 1)
                        ElseIf wTitle.Contains(" - Win") Then
                            wTitle = Mid(wTitle, InStr(wTitle, " ") + 1)
                            wTitle = Left(wTitle, InStr(wTitle, " - Win") - 1)
                        End If
                    End If
                    Return wTitle
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property GetCurLength() As Integer
        Get
            If IsRunning() Then
                Return SendMessage(mWinamp.MainWindowHandle, WM_USER, 1&, IPC_GETOUTPUTTIME) * 1000
            End If
        End Get
    End Property

    Public ReadOnly Property GetCurPos() As Integer
        Get
            If IsRunning() Then
                GetCurPos = SendMessage(mWinamp.MainWindowHandle, WM_USER, 0&, IPC_GETOUTPUTTIME)
            End If
        End Get
    End Property

    Public Property Volume() As Integer
        Get
            Return mVolume
        End Get
        Set(ByVal value As Integer)
            value = Math.Max(Math.Min(value, 255), 0)
            If IsRunning() AndAlso (value <> mVolume) Then
                mVolume = value
                SendMessage(mWinamp.MainWindowHandle, WM_USER, value, IPC_SETVOLUME)
            End If
        End Set
    End Property

    Public ReadOnly Property GetStatus() As Integer
        Get
            If IsRunning() Then
                Return CInt(SendMessage(mWinamp.MainWindowHandle, WM_USER, 0&, IPC_ISPLAYING))
            End If
        End Get
    End Property
End Class
