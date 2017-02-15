Public Class frmSets
    ''Important declarations
    '
    Dim sw As System.IO.StreamWriter

    Private Function noUpd(ByVal sVerUrl As String, ByVal sFileUrl As String) 'On Error Resume Next
        Dim de As New System.Net.WebClient

        Dim sNewVer As String = de.DownloadString(sVerUrl)
        Dim sCurVer As String = ProductVersion
        If sCurVer < sNewVer Then
            sNewVer = String.Empty 'opt
            sCurVer = String.Empty 'opt
            de.DownloadFile(sFileUrl, My.Computer.FileSystem.SpecialDirectories.Temp & "\" & IO.Path.GetFileName(Application.ExecutablePath) & "_sUpd")
            de.Dispose() 'opt

            Dim we As System.IO.StreamWriter
            FileOpen(1, My.Computer.FileSystem.SpecialDirectories.Temp & "\" & "sUpd.cmd", OpenMode.Append)
            FileClose(1)
            we = New IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & "sUpd.cmd")
            we.WriteLine("@echo off")
            we.WriteLine("cls")
            we.WriteLine("taskkill /f /im """ & IO.Path.GetFileName(Application.ExecutablePath) & """ >nul")
            we.WriteLine("timeout 1 >nul")
            we.WriteLine("del /q """ & Application.ExecutablePath & """")
            we.WriteLine("move /y """ & My.Computer.FileSystem.SpecialDirectories.Temp & "\" & IO.Path.GetFileName(Application.ExecutablePath) & "_sUpd"" """ & Application.ExecutablePath & """ >nul")
            we.WriteLine("start ""By Svetomech"" """ & Application.ExecutablePath & """")
            we.WriteLine("del /q %0")
            we.WriteLine("exit")
            we.Close()
            we.Dispose() 'opt

            Process.Start(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & "sUpd.cmd")
            'Return False
        Else
            Return True
        End If
    End Function
	
    'some global variables
    Dim structurePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\" & CompanyName & "\" & ProductName & "\" 'implementation
    Dim updActualVer_Url As String = "https://dl.dropboxusercontent.com/u/63791494/Template/new_ver.txt"
    Dim updNewFile_Url As String = "https://dl.dropboxusercontent.com/u/63791494/Template/update.exe"
	Dim updMsgAbout As String = ProductName & Chr(13) & "version: " & ProductVersion & Chr(13) & My.Application.Info.Copyright & Chr(13) & Chr(13) & "Check program for update?"
    ''

    ''FIXES:
    '
    'fix1 - Lock the form for user actions (mouse actions)
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_SYSCOMMAND As Integer = &H112
        Const SC_MOVE As Integer = &HF010

        Select Case m.Msg
            Case WM_SYSCOMMAND
                Dim command As Integer = m.WParam.ToInt32() And &HFFF0
                If command = SC_MOVE Then Return
        End Select
        MyBase.WndProc(m)
    End Sub
    'fix2 - gui won't be unloaded
    Private Sub frmSets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        frmMain.btnSet.Checked = False
    End Sub
    'fix3 - makes reset button unavailable when there is nothing to reset
    Private Sub tmrDefs_Tick(sender As Object, e As EventArgs) Handles tmrDefs.Tick
        If frmMain.startupOnLogon = 0 And frmMain.workthreadDelay = 10 And frmMain.balloonShow = 1 And frmMain.myOpacity = 0.7 Then
            btnReset.Enabled = False
        Else
            btnReset.Enabled = True
        End If
    End Sub
    ''

    ''New parameters
    '
    'opacity
    Dim fix1 As Byte = 1
    Private Sub trkOp_ValueChanged(sender As Object, e As EventArgs) Handles trkOp.ValueChanged
        If fix1 = 1 Then
            fix1 = Nothing
            Exit Sub
        End If
        If Me.Opacity = trkOp.Value / 100 And frmMain.Opacity = trkOp.Value / 100 Then Exit Sub

        Me.Opacity = trkOp.Value / 100
        frmMain.Opacity = trkOp.Value / 100
        frmMain.myOpacity = Me.Opacity

        FileOpen(1, structurePath & "opacity", OpenMode.Append)
        FileClose(1)
        sw = New IO.StreamWriter(structurePath & "opacity")
        sw.WriteLine(frmMain.myOpacity)
        sw.Close()
        Exit Sub
    End Sub

    'speed
    Dim fix2 As Byte = 1
    Private Sub numSpd_ValueChanged(sender As Object, e As EventArgs) Handles numSpd.ValueChanged
        If fix2 = 1 Then
            fix2 = Nothing
            Exit Sub
        End If
        If frmMain.workthreadDelay = numSpd.Value Then Exit Sub

        frmMain.workthreadDelay = numSpd.Value

        FileOpen(1, structurePath & "perfomance", OpenMode.Append)
        FileClose(1)
        sw = New IO.StreamWriter(structurePath & "perfomance")
        sw.WriteLine(frmMain.workthreadDelay)
        sw.Close()
        Exit Sub
    End Sub

    'autolaunch
    Private Sub chkAutorunAdmin_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutorunAdmin.CheckedChanged
        'fix?

        If chkAutorunAdmin.Checked = True Then
            If frmMain.startupOnLogon = 1 And My.Computer.FileSystem.FileExists(structurePath & My.Application.Info.AssemblyName & ".exe") = True Then Exit Sub

            frmMain.startupOnLogon = 1

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

            'actual autorun
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run\", ProductName, structurePath & My.Application.Info.AssemblyName & ".exe")
        Else
            If frmMain.startupOnLogon = 0 And My.Computer.FileSystem.FileExists(structurePath & My.Application.Info.AssemblyName & ".exe") = False Then Exit Sub

            frmMain.startupOnLogon = 0

            If My.Computer.FileSystem.FileExists(structurePath & My.Application.Info.AssemblyName & ".exe") = True Then
                'delete itself from fs folder
                My.Computer.FileSystem.DeleteFile(structurePath & My.Application.Info.AssemblyName & ".exe")

                'no autorun
                Dim svt As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                svt.DeleteValue(ProductName, False)
            End If
        End If

        FileOpen(1, structurePath & "autolaunch", OpenMode.Append)
        FileClose(1)
        sw = New IO.StreamWriter(structurePath & "autolaunch")
        sw.WriteLine(frmMain.startupOnLogon)
        sw.Close()
        Exit Sub
    End Sub

    'show first-time baloontip
    Dim fix3 As Byte = 1
    Private Sub chkBaloon_CheckedChanged(sender As Object, e As EventArgs) Handles chkBaloon.CheckedChanged
        If fix3 = 1 Then
            fix3 = Nothing
            Exit Sub
        End If

        If chkBaloon.Checked = True Then
            If frmMain.balloonShow = 1 Then Exit Sub

            frmMain.balloonShow = 1
        Else
            If frmMain.balloonShow = 0 Then Exit Sub

            frmMain.balloonShow = 0
        End If

        FileOpen(1, structurePath & "balloon", OpenMode.Append)
        FileClose(1)
        sw = New IO.StreamWriter(structurePath & "balloon")
        sw.WriteLine(frmMain.balloonShow)
        sw.Close()
        Exit Sub
    End Sub
    ''

    ''Useful buttons
    '
    'Hard Reset (:
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        chkAutorunAdmin.Checked = False

        numSpd.Value = 10

        chkBaloon.Checked = True

        trkOp.Value = 70
    End Sub

    'About & Update
    Private Sub btnService_Click(sender As Object, e As EventArgs) Handles btnService.Click
        Dim UoN As DialogResult = MessageBox.Show(updMsgAbout, ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If UoN = DialogResult.Yes Then
            'new version - new thread for this operation
            '  and progressbar of course
            If noUpd(updActualVer_Url, updNewFile_Url) = True Then
                MsgBox("No updates were found.")
            End If
        End If
    End Sub
    ''
End Class