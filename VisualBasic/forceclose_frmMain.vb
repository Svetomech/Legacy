Imports System.IO

Public Class frmMain
    ''Important declarations
    '
    Dim sw As StreamWriter
    Dim sr As StreamReader

    Dim WithEvents wc As New System.Net.WebClient
    Private Sub wc_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        ''later - add notification, etc. to update system [*]
    End Sub
    Private Sub wc_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        ''later - add prograss bar to update system [*]
    End Sub

    'some global variables
    Dim structurePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\" & CompanyName & "\" & ProductName & "\" 'where to save cache (settings) file
    Dim delayedExit As Byte = 1 '0/1, 1 - if user changed any setting (process#x included) then it asks if he wants to save everything or not, 0 - no question appears, program quits immidiately
    Dim balloonShow As Byte = 1 '0/1/2, 1 - shows 1st baloontip when hiding, 2 - shows 2nd baloontip when hiding, 0 - no baloontips at all
    Dim msgboxDeactive As Byte = 1 'fix4 - 0/1, 1 - form hides itself when loosing focus, 0 - always show
    Dim Op As Byte '0/1, 1 - program thinks it is non-transparent, 0 - program thinks it is transparent | will have no effect if you define it here
    Dim OpInterval As Decimal = 0.05
    Dim TmInterval As Decimal = 10
    Dim savedLaunch As Byte = 0 '0/1, 1 - , 0 - 

    Dim i As Integer = 0 '0-2147483647, it is responsible for timer counter (form & tray icon), you can define first parameter here (in seconds)
    ''

    ''FIXES:
    '
    'fix1 - NtfIco under <= WindowsXP
    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        NtfIco.Dispose()
    End Sub

    'fix2 - no exit apart from taskkiller and program method
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If wasChangedTxtProc1 = 0 And wasChangedTxtProc2 = 0 And wasChangedTxtProc3 = 0 And wasChangedTxtProc4 = 0 And wasChangedTxtProc5 = 0 Then
            'Exit Sub - it should be like this, but I can't use it!
            '           (getting some strange error when I close 
            '           the program not in IDE - dunno why, may 
            '           be somehow connected with Deactivate
            '           method) I managed a workaround, below
            delayedExit = 0
            Application.Exit()
            'NOT SURE when there will be more setting, coming with update system [*]
        End If

        If delayedExit = 1 Then
            e.Cancel = True
        Else
            Exit Sub
        End If

        'if need to be placed here [*] - was changed
        'If TxtProc1.Text != "" And TxtProc2.Text != "" And TxtProc3.Text != "" And TxtProc4.Text != "" And TxtProc5.Text != "" Then Exit Sub
        Dim SoN As DialogResult = MessageBox.Show("Save data (e.g. processes' names)?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If SoN = DialogResult.No Then
            delayedExit = 0
            Application.Exit()
        Else
            FileOpen(1, structurePath & "cache.dat", OpenMode.Append)
            FileClose(1)
            sw = New StreamWriter(structurePath & "cache.dat")
            ''if [*] - != ""
            sw.WriteLine(TxtProc1.Text)
            sw.WriteLine(TxtProc2.Text)
            sw.WriteLine(TxtProc3.Text)
            sw.WriteLine(TxtProc4.Text)
            sw.WriteLine(TxtProc5.Text)
            ''

            'sw.WriteLine("") - exit after processes are killed / or not exit [*]
            'sw.WriteLine("") - timeout to be driven to stop-state (update ntfico also) [*]
            'sw.WriteLine("") - form border style [*]
            'sw.WriteLine("") - taskbar icon show [*]
            sw.Close()

            delayedExit = 0
            Application.Exit()
        End If
    End Sub

    '!fix 3 - to NOT waste CPU on calculating, /writing/ NOTHING
    Dim wasChangedTxtProc1 As Byte = 0
    Dim wasChangedTxtProc2 As Byte = 0
    Dim wasChangedTxtProc3 As Byte = 0
    Dim wasChangedTxtProc4 As Byte = 0
    Dim wasChangedTxtProc5 As Byte = 0
    'Dim wasChanged"" As Byte = 0 - exit after processes are killed / or not exit [*]
    'Dim wasChanged"" As Byte = 0 - timeout to be driven to stop-state (update ntfico also) [*]
    'Dim wasChanged"" As Byte = 0 - form border style [*]
    'Dim wasChanged"" As Byte = 0 - taskbar icon show [*]
    ''

    ''Form->TrayIcon | TrayIcon->Form manipulating
    '
    'fade declaration
    Private Sub TmrVanish_Tick(sender As Object, e As EventArgs) Handles TmrVanish.Tick
        If Op = 1 Then
            Me.Opacity = Opacity - OpInterval
            If Opacity <= 0 Then
                Me.Enabled = False
                TmrVanish.Stop()
            End If
        End If
        If Op = 0 Then
            Me.Opacity = Opacity + OpInterval
            Me.Enabled = True
            If Opacity >= 1 Then
                TmrVanish.Stop()
            End If
        End If
    End Sub

    'if user deactivates the program window then 'fade' it
    Private Sub frmMain_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        '!fix4
        If msgboxDeactive = 0 Then Exit Sub

        'shows the Dropbox-like tray notification | why hard-coded "Stopped"? 
        '  because when it's working it's updating tray notification automatically 
        '  so there 's no need to check program state here
        NtfIco.Text = ProductName & " " & ProductVersion & Chr(13) & "Stopped"

        'fade
        Me.Op = 1
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        NtfIco.Visible = True
        If balloonShow = 0 Then
            Exit Sub
        End If
        If balloonShow = 1 Then
            NtfIco.ShowBalloonTip(2, "It is still running, click once to maximize", "Just click here already :)", ToolTipIcon.Info)
            balloonShow = 2
            Exit Sub
        End If
        If balloonShow = 2 Then
            NtfIco.ShowBalloonTip(2, "", "I hope you understood what's what", ToolTipIcon.Info)
            balloonShow = 0
            Exit Sub
        End If
    End Sub
    Private Sub NtfIco_MouseClick(sender As Object, e As MouseEventArgs) Handles NtfIco.MouseClick
        NtfIco.Visible = False

        'show again
        Me.Op = 0
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        Me.Show() : Me.Activate()
    End Sub
    Private Sub NtfIco_BallonTipClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NtfIco.BalloonTipClicked
        NtfIco.Visible = False

        'show again
        Me.Op = 0
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        Me.Show() : Me.Activate()
    End Sub
    ''
    Private Sub frmMain_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        frmProps.Location = New Point((Me.Location.X + Me.Width), Me.Location.Y)
    End Sub
    ''Resources - loading...
    '
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'gui
        Me.Icon = My.Resources.temp_icon
        Me.Text = ProductName
        NtfIco.Icon = My.Resources.temp_icon
        'frmBorderStyle.Dispose()
        'tskIcoVisible.Dispose()
        msgboxDeactive = 0 'fix4 copypaste
        frmProps.Show()
        msgboxDeactive = 1 'fix4 copypaste

        'fs
        IO.Directory.CreateDirectory(structurePath)
        If IO.Directory.Exists("svt_structure") = True Then My.Computer.FileSystem.MoveDirectory("svt_structure", structurePath, True)

        'data read
        If My.Computer.FileSystem.FileExists(structurePath & "cache.dat") = True Then
            sr = New StreamReader(structurePath & "cache.dat")
            TxtProc1.Text = sr.ReadLine()
            TxtProc2.Text = sr.ReadLine()
            TxtProc3.Text = sr.ReadLine()
            TxtProc4.Text = sr.ReadLine()
            TxtProc5.Text = sr.ReadLine()

            '"".Text = sr.ReadLine() - exit after processes are killed / or not exit [*]
            '"".Text = sr.ReadLine() - timeout to be driven to stop-state (update ntfico also) [*]
            '"".Text = sr.ReadLine() - form border style [*]
            '"".Text = sr.ReadLine() - taskbar icon show [*]
            sr.Close()
        End If
    End Sub
    ''

    ''
    '
    Private Sub BtnStrt_killing_Click(sender As Object, e As EventArgs) Handles BtnStrt_killing.Click
        If TxtProc1.Text = "" And TxtProc2.Text = "" And TxtProc3.Text = "" And TxtProc4.Text = "" And TxtProc5.Text = "" Then
            msgboxDeactive = 0 'fix4
            MessageBox.Show("You must define one process at least!", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            msgboxDeactive = 1 'fix4
        Else
            If TmrCnt.Enabled = False Then
                TxtProc1.Enabled = False
                TxtProc2.Enabled = False
                TxtProc3.Enabled = False
                TxtProc4.Enabled = False
                TxtProc5.Enabled = False
                LBLproc1.Enabled = False
                LBLproc2.Enabled = False
                LBLproc3.Enabled = False
                LBLproc4.Enabled = False
                LBLproc5.Enabled = False

                TmrAct.Enabled = True
                TxtWrk.Enabled = True
                TxtWrk.Text = "0 s"
                TmrCnt.Enabled = True
                BtnStrt_killing.Text = "STOP"
            Else
                TxtProc1.Enabled = True
                TxtProc2.Enabled = True
                TxtProc3.Enabled = True
                TxtProc4.Enabled = True
                TxtProc5.Enabled = True
                LBLproc1.Enabled = True
                LBLproc2.Enabled = True
                LBLproc3.Enabled = True
                LBLproc4.Enabled = True
                LBLproc5.Enabled = True

                TmrAct.Enabled = False
                TxtWrk.Enabled = False
                i = 0
                TmrCnt.Enabled = False
                BtnStrt_killing.Text = "LAUNCH"
            End If
        End If
    End Sub

    Private Sub TmrAct_Tick(sender As Object, e As EventArgs) Handles TmrAct.Tick
        'For Each selProcess As Process In Process.GetProcesses
        '    If selProcess.ProcessName = TxtProc1.Text Then
        '        selProcess.Kill()
        '        Exit For
        '    End If
        '    If selProcess.ProcessName = TxtProc2.Text Then
        '        selProcess.Kill()
        '        Exit For
        '    End If
        '    If selProcess.ProcessName = TxtProc3.Text Then
        '        selProcess.Kill()
        '        Exit For
        '    End If
        '    If selProcess.ProcessName = TxtProc4.Text Then
        '        selProcess.Kill()
        '        Exit For
        '    End If
        '    If selProcess.ProcessName = TxtProc5.Text Then
        '        selProcess.Kill()
        '        Exit For
        '    End If
        'Next

        For Each currentprocesses As Process In Process.GetProcesses
            If currentprocesses.ProcessName = TxtProc1.Text Or currentprocesses.ProcessName = TxtProc2.Text Or currentprocesses.ProcessName = TxtProc3.Text Or currentprocesses.ProcessName = TxtProc4.Text Or currentprocesses.ProcessName = TxtProc5.Text Then
                Process.GetProcessesByName(currentprocesses.ProcessName)(0).Kill()
                ''Process.GetProcessesByName(currentprocesses.ProcessName)(0).CloseMainWindow()
            End If
        Next
    End Sub

    Private Sub TmrCnt_Tick(sender As Object, e As EventArgs) Handles TmrCnt.Tick
        i = i + 1
        TxtWrk.Text = i & " s"
        NtfIco.Text = ProductName & " " & ProductVersion & Chr(13) & "Launched [" & i & "s" & "]"
    End Sub
    ''

    ''Some magic
    '
    'hides the labels
    Private Sub LBLproc1_MouseEnter(sender As Object, e As EventArgs) Handles TxtProc1.MouseEnter, LBLproc1.MouseEnter
        LBLproc1.Visible = False
    End Sub
    Private Sub LBLproc2_MouseEnter(sender As Object, e As EventArgs) Handles TxtProc2.MouseEnter, LBLproc2.MouseEnter
        LBLproc2.Visible = False
    End Sub
    Private Sub LBLproc3_MouseEnter(sender As Object, e As EventArgs) Handles TxtProc3.MouseEnter, LBLproc3.MouseEnter
        LBLproc3.Visible = False
    End Sub
    Private Sub LBLproc4_MouseEnter(sender As Object, e As EventArgs) Handles TxtProc4.MouseEnter, LBLproc4.MouseEnter
        LBLproc4.Visible = False
    End Sub
    Private Sub LBLproc5_MouseEnter(sender As Object, e As EventArgs) Handles TxtProc5.MouseEnter, LBLproc5.MouseEnter
        LBLproc5.Visible = False
    End Sub

    'hides the labels (2)
    Private Sub TxtProc1_TextChanged(sender As Object, e As EventArgs) Handles TxtProc1.TextChanged
        wasChangedTxtProc1 = 1

        If TxtProc1.Text = "" Then
            LBLproc1.Visible = True
        Else
            LBLproc1.Visible = False
        End If
    End Sub
    Private Sub TxtProc2_TextChanged(sender As Object, e As EventArgs) Handles TxtProc2.TextChanged
        wasChangedTxtProc2 = 1

        If TxtProc2.Text = "" Then
            LBLproc2.Visible = True
        Else
            LBLproc2.Visible = False
        End If
    End Sub
    Private Sub TxtProc3_TextChanged(sender As Object, e As EventArgs) Handles TxtProc3.TextChanged
        wasChangedTxtProc3 = 1

        If TxtProc3.Text = "" Then
            LBLproc3.Visible = True
        Else
            LBLproc3.Visible = False
        End If
    End Sub
    Private Sub TxtProc4_TextChanged(sender As Object, e As EventArgs) Handles TxtProc4.TextChanged
        wasChangedTxtProc4 = 1

        If TxtProc4.Text = "" Then
            LBLproc4.Visible = True
        Else
            LBLproc4.Visible = False
        End If
    End Sub
    Private Sub TxtProc5_TextChanged(sender As Object, e As EventArgs) Handles TxtProc5.TextChanged
        wasChangedTxtProc5 = 1

        If TxtProc5.Text = "" Then
            LBLproc5.Visible = True
        Else
            LBLproc5.Visible = False
        End If
    End Sub

    'shows the labels
    Private Sub LBLproc1_MouseLeave(sender As Object, e As EventArgs) Handles TxtProc1.MouseLeave
        If TxtProc1.Text = "" Then LBLproc1.Visible = True
    End Sub
    Private Sub LBLproc2_MouseLeave(sender As Object, e As EventArgs) Handles TxtProc2.MouseLeave
        If TxtProc2.Text = "" Then LBLproc2.Visible = True
    End Sub
    Private Sub LBLproc3_MouseLeave(sender As Object, e As EventArgs) Handles TxtProc3.MouseLeave
        If TxtProc3.Text = "" Then LBLproc3.Visible = True
    End Sub
    Private Sub LBLproc4_MouseLeave(sender As Object, e As EventArgs) Handles TxtProc4.MouseLeave
        If TxtProc4.Text = "" Then LBLproc4.Visible = True
    End Sub
    Private Sub LBLproc5_MouseLeave(sender As Object, e As EventArgs) Handles TxtProc5.MouseLeave
        If TxtProc5.Text = "" Then LBLproc5.Visible = True
    End Sub
    ''

    ''
    '
    Private Sub frmBorderStyle_CheckedChanged(sender As Object, e As EventArgs) Handles frmBorderStyle.CheckedChanged
        'wasChanged"" = 1 - form border style [*]

        If Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            ShowIcon = True
            Me.Text = "NA-F"
        Else
            Me.Text = ProductName
            ShowIcon = False
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        End If
    End Sub

    '
    Private Sub tskIcoVisible_CheckedChanged(sender As Object, e As EventArgs) Handles tskIcoVisible.CheckedChanged
        'wasChanged"" = 1 - taskbar icon show [*]
        msgboxDeactive = 0 'fix4 copypaste
        If Me.ShowInTaskbar = False Then
            ShowIcon = True
            ShowInTaskbar = True
        Else
            ShowIcon = False
            ShowInTaskbar = False
        End If
        msgboxDeactive = 1 'fix4 copypaste
    End Sub
    ''
End Class
