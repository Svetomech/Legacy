<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnHid = New System.Windows.Forms.Button()
        Me.ntfDel = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.LBLstate = New System.Windows.Forms.Label()
        Me.tmrLBL = New System.Windows.Forms.Timer(Me.components)
        Me.logField = New System.Windows.Forms.TextBox()
        Me.TmrVanish = New System.Windows.Forms.Timer(Me.components)
        Me.btnSet = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(0, 6)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Cleaning"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnHid
        '
        Me.btnHid.Location = New System.Drawing.Point(197, 0)
        Me.btnHid.Name = "btnHid"
        Me.btnHid.Size = New System.Drawing.Size(87, 35)
        Me.btnHid.TabIndex = 2
        Me.btnHid.Text = "HIDE"
        Me.btnHid.UseVisualStyleBackColor = True
        '
        'ntfDel
        '
        '
        'LBLstate
        '
        Me.LBLstate.AutoSize = True
        Me.LBLstate.Font = New System.Drawing.Font("Meiryo UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LBLstate.Location = New System.Drawing.Point(12, 29)
        Me.LBLstate.Name = "LBLstate"
        Me.LBLstate.Size = New System.Drawing.Size(0, 20)
        Me.LBLstate.TabIndex = 3
        '
        'tmrLBL
        '
        Me.tmrLBL.Interval = 800
        '
        'logField
        '
        Me.logField.Location = New System.Drawing.Point(81, 0)
        Me.logField.Multiline = True
        Me.logField.Name = "logField"
        Me.logField.ReadOnly = True
        Me.logField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.logField.Size = New System.Drawing.Size(110, 58)
        Me.logField.TabIndex = 1
        Me.logField.TabStop = False
        '
        'TmrVanish
        '
        '
        'btnSet
        '
        Me.btnSet.Appearance = System.Windows.Forms.Appearance.Button
        Me.btnSet.AutoSize = True
        Me.btnSet.Location = New System.Drawing.Point(229, 35)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(55, 23)
        Me.btnSet.TabIndex = 3
        Me.btnSet.Text = "Settings"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 58)
        Me.Controls.Add(Me.btnSet)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnHid)
        Me.Controls.Add(Me.LBLstate)
        Me.Controls.Add(Me.logField)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.Opacity = 0.7R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnHid As System.Windows.Forms.Button
    Friend WithEvents ntfDel As System.Windows.Forms.NotifyIcon
    Friend WithEvents LBLstate As System.Windows.Forms.Label
    Friend WithEvents tmrLBL As System.Windows.Forms.Timer
    Friend WithEvents logField As System.Windows.Forms.TextBox
    Friend WithEvents TmrVanish As System.Windows.Forms.Timer
    Friend WithEvents btnSet As System.Windows.Forms.CheckBox

End Class
