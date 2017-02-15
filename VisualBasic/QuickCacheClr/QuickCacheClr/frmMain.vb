Public Class frmMain
    ''Important declarations
    '
    Dim sr As System.IO.StreamReader

    'some global variables
    Dim structurePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\" & CompanyName & "\" & ProductName & "\" 'where to save cache (settings) file
    Public startupOnLogon As Byte = 0 '0/1, 1 - the program will be running quietly in background after system restart, 0 - will not
    Public workthreadDelay As Integer = 10 '[*]2-1000 (higher[to Int32Max] or lower[from 0] values are possible but not recommended), how frequently the program checks temp folders; in milliseconds
    Public balloonShow As Byte = 1 '0/1, 1 - shows the first time baloontip when hiding, 0 - no baloontip
    Dim balloonText As String = "If any cache file is found there will be notification about its deleting so you can cancell it by clicking that notification and pressing ""Stop"" button. You will only have short amount of time to do it though."

    Dim TmInterval As Decimal = 10 'fading animation property
    Dim OpInterval As Decimal = 0.05 'fading animation property
    Dim Op As Byte '0/1, 1 - program thinks it is non-transparent, 0 - program thinks it is transparent | will have no effect if you define it here
    Public myOpacity = 0.7 '0-1 (e.g. 0.01 = 1%, 0.3 = 30% & so on), same here. Sort of a fix (for HIDE button), but ACTUALLY program supposed transparency by default | may affect somehow if you define it here, but there could be problems

    Dim borderCross As Byte 'borderline state of a district where program 'lives' | will have no effect if you define it here
    ''

    ''FIXES:
    '
    'fix1 - NtfIco under <= WindowsXP
    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ntfDel.Dispose()
    End Sub
    ''

    ''Form->TrayIcon | TrayIcon->Form manipulating
    '
    'yeah, the program lives here, in WorkingArea
    '  this function allows to detect 
    '  when it tries to escape
    Private Sub frmMain_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        If Me.Location.X > Screen.PrimaryScreen.WorkingArea.Width - Me.Width Or Me.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - Me.Height Then
            borderCross = 1
        Else
            borderCross = 0
        End If

        'User isn't able to summon the form when it can't be seen
        If Me.Location.X >= Screen.PrimaryScreen.WorkingArea.Width - Me.Width Then
            btnSet.Enabled = False
        Else
            btnSet.Enabled = True
        End If

        'Settings form relocating
        If frmSets.Visible = True And frmSets.Location <> New Point((Me.Location.X + Me.Width), Me.Location.Y) Then frmSets.Location = New Point((Me.Location.X + Me.Width), Me.Location.Y)
    End Sub

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
            If Opacity >= myOpacity Then
                TmrVanish.Stop()
            End If
        End If
    End Sub

    'if user hides the program window then:
    Private Sub btnHid_Click(sender As Object, e As EventArgs) Handles btnHid.Click
        'Settings form closing
        If frmSets.Visible = True Then btnSet.Checked = False

        'to the right screen corner #MyDesign
        Dim x1, y1, x2, y2 As Integer
        x1 = Me.Location.X
        y1 = Me.Location.Y
        x2 = Screen.PrimaryScreen.WorkingArea.Width - Me.Width
        y2 = Screen.PrimaryScreen.WorkingArea.Height - Me.Height
        Do Until x1 = x2 And y1 = y2
            If x1 <> x2 Then
                If borderCross = 0 Then
                    x1 = x1 + 1
                Else
                    x1 = x1 - 1
                End If
            End If
            If y1 <> y2 Then
                If borderCross = 0 Then
                    y1 = y1 + 1
                Else
                    y1 = y1 - 1
                End If
            End If
            Me.Location = New Point(x1, y1)
        Loop

        'fade
        Me.Op = 1
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        'show onetime balloontip
        ntfDel.Visible = True
        If balloonShow = 1 Then
            ntfDel.ShowBalloonTip(2, "", balloonText, ToolTipIcon.Info)
        End If
        frmSets.chkBaloon.Checked = False
    End Sub

    'form appearing
    Private Sub ntfDel_MouseClick(sender As Object, e As MouseEventArgs) Handles ntfDel.MouseClick
        ntfDel.Visible = False

        'show again
        Me.Op = 0
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        Me.Show() : Me.Activate()
    End Sub
    Private Sub ntfDel_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ntfDel.MouseDoubleClick
        ntfDel.Visible = False

        'show again
        Me.Op = 0
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        Me.Show() : Me.Activate()
    End Sub
    Private Sub ntfDel_BallonTipClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ntfDel.BalloonTipClicked
        ntfDel.Visible = False

        'show again
        Me.Op = 0
        TmrVanish.Interval = TmInterval
        TmrVanish.Start()

        Me.Show() : Me.Activate()
    End Sub
    ''

    ''Main function
    '
    'IsBackgroundFalse and workdelay = тот же таймер, только круче, живучий
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        'work manager
        If IsNothing(Twork) Then
            'START thread
            Twork = New Threading.Thread(AddressOf Work)
            Twork.IsBackground = True
            Twork.Start() 'END START
            tmrLBL.Start()
            logField.Text = "Started." & vbCrLf
            btnStart.Text = "Stop"
        Else
            'STOP thread
            stopTwork = True
            waitTwork.Set()
            Twork.Join()
            Debug.WriteLine("end") 'END STOP
            tmrLBL.Stop()
            logField.AppendText("Done." & vbCrLf)
            btnStart.Text = "Cleaning"
            LBLstate.Text = ""
            Me.Text = "[   ] " & ProductName
        End If
    End Sub
    Dim Twork As Threading.Thread
    Dim waitTwork As New Threading.AutoResetEvent(False)
    Dim stopTwork As Boolean
    Sub Work()
        'the work itself
        Do

            'your code here

            waitTwork.WaitOne(workthreadDelay) ' = Thread.Sleep(workthreadDelay)
            If stopTwork Then
                Twork = Nothing
                stopTwork = Nothing
                Exit Do
            End If
        Loop
    End Sub
    ''

    ''miscellaneous
    '
    'title and label update (as if working)
    Private Sub tmrLBL_Tick(sender As Object, e As EventArgs) Handles tmrLBL.Tick
        If LBLstate.Text = "" Then
            LBLstate.Text = "."
            Me.Text = "[.  ] " & ProductName
            Exit Sub
        End If
        If LBLstate.Text = "." Then
            LBLstate.Text = ".."
            Me.Text = "[.. ] " & ProductName
            Exit Sub
        End If
        If LBLstate.Text = ".." Then
            LBLstate.Text = "..."
            Me.Text = "[...] " & ProductName
            Exit Sub
        End If
        If LBLstate.Text = "..." Then
            LBLstate.Text = ""
            Me.Text = "[   ] " & ProductName
            Exit Sub
        End If
    End Sub
    ''

    ''Resources - loading...
    '
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'gui
        Me.Text = "[   ] " & ProductName
        frmSets.Text = "[Settings] " & ProductName
        ntfDel.Icon = My.Resources.unnecessary
        ntfDel.Text = ProductName & Chr(13) & "Log is empty"

        'fs
        IO.Directory.CreateDirectory(structurePath)
        If IO.Directory.Exists("svt_structure") = True Then My.Computer.FileSystem.MoveDirectory("svt_structure", structurePath, True)

        'data read
        If My.Computer.FileSystem.FileExists(structurePath & "opacity") = True Then
            sr = New IO.StreamReader(structurePath & "opacity")
            myOpacity = sr.ReadLine()
            sr.Close()
            frmSets.trkOp.Value = myOpacity * 100
        End If
        If My.Computer.FileSystem.FileExists(structurePath & "balloon") = True Then
            sr = New IO.StreamReader(structurePath & "balloon")
            balloonShow = sr.ReadLine()
            sr.Close()
            If balloonShow = 1 Then
                frmSets.chkBaloon.Checked = True
            Else
                frmSets.chkBaloon.Checked = False
            End If
        End If
        If My.Computer.FileSystem.FileExists(structurePath & "perfomance") = True Then
            sr = New IO.StreamReader(structurePath & "perfomance")
            workthreadDelay = sr.ReadLine()
            sr.Close()
            frmSets.numSpd.Value = workthreadDelay
        End If
        If My.Computer.FileSystem.FileExists(structurePath & "autolaunch") = True Then
            sr = New IO.StreamReader(structurePath & "autolaunch")
            startupOnLogon = sr.ReadLine()
            sr.Close()
            If startupOnLogon = 1 Then
                If Application.ExecutablePath = structurePath & My.Application.Info.AssemblyName & ".exe" Then
                    'START thread
                    Twork = New Threading.Thread(AddressOf Work)
                    Twork.IsBackground = False
                    Twork.Start() 'END START
                    Application.Exit()

                    'COPYPASTE THIS IN YOUR CODE IF & WHEN YOU WANT TO REALLY EXIT APPLICATION
                    'stopTwork = True
                Else
                    frmSets.chkAutorunAdmin.Checked = True
                End If
            Else
                frmSets.chkAutorunAdmin.Checked = False
            End If
        End If
    End Sub
    ''

    ''Program settings
    '
    'form
    Private Sub btnSet_CheckedChanged(sender As Object, e As EventArgs) Handles btnSet.CheckedChanged
        If frmSets.Visible = False Then
            If frmSets.tmrDefs.Enabled = False Then frmSets.tmrDefs.Start()
            frmSets.Show() : frmSets.Activate()
            If frmSets.Location <> New Point((Me.Location.X + Me.Width), Me.Location.Y) Then frmSets.Location = New Point((Me.Location.X + Me.Width), Me.Location.Y)
        Else
            frmSets.Hide()
            If frmSets.tmrDefs.Enabled = True Then frmSets.tmrDefs.Stop()
        End If
    End Sub
    ''
End Class
