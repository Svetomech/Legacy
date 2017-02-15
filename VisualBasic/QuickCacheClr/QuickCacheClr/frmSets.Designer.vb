<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSets
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
        Me.trkOp = New System.Windows.Forms.TrackBar()
        Me.LBLop = New System.Windows.Forms.Label()
        Me.LBLspeed = New System.Windows.Forms.Label()
        Me.chkBaloon = New System.Windows.Forms.CheckBox()
        Me.chkAutorunAdmin = New System.Windows.Forms.CheckBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.LBLspdTip = New System.Windows.Forms.Label()
        Me.numSpd = New System.Windows.Forms.NumericUpDown()
        Me.btnService = New System.Windows.Forms.Button()
        Me.tmrDefs = New System.Windows.Forms.Timer(Me.components)
        CType(Me.trkOp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'trkOp
        '
        Me.trkOp.LargeChange = 10
        Me.trkOp.Location = New System.Drawing.Point(0, 0)
        Me.trkOp.Maximum = 100
        Me.trkOp.Minimum = 10
        Me.trkOp.Name = "trkOp"
        Me.trkOp.Size = New System.Drawing.Size(284, 45)
        Me.trkOp.SmallChange = 5
        Me.trkOp.TabIndex = 0
        Me.trkOp.TickFrequency = 10
        Me.trkOp.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.trkOp.Value = 70
        '
        'LBLop
        '
        Me.LBLop.AutoSize = True
        Me.LBLop.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LBLop.ForeColor = System.Drawing.Color.Blue
        Me.LBLop.Location = New System.Drawing.Point(208, 48)
        Me.LBLop.Name = "LBLop"
        Me.LBLop.Size = New System.Drawing.Size(64, 20)
        Me.LBLop.TabIndex = 1
        Me.LBLop.Text = "Visibility"
        '
        'LBLspeed
        '
        Me.LBLspeed.AutoSize = True
        Me.LBLspeed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LBLspeed.ForeColor = System.Drawing.Color.Red
        Me.LBLspeed.Location = New System.Drawing.Point(167, 137)
        Me.LBLspeed.Name = "LBLspeed"
        Me.LBLspeed.Size = New System.Drawing.Size(105, 20)
        Me.LBLspeed.TabIndex = 3
        Me.LBLspeed.Text = "Perfomance"
        '
        'chkBaloon
        '
        Me.chkBaloon.AutoSize = True
        Me.chkBaloon.Checked = True
        Me.chkBaloon.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBaloon.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkBaloon.ForeColor = System.Drawing.Color.Blue
        Me.chkBaloon.Location = New System.Drawing.Point(12, 79)
        Me.chkBaloon.Name = "chkBaloon"
        Me.chkBaloon.Size = New System.Drawing.Size(201, 24)
        Me.chkBaloon.TabIndex = 4
        Me.chkBaloon.Text = "Show onetime balloontip"
        Me.chkBaloon.UseVisualStyleBackColor = True
        '
        'chkAutorunAdmin
        '
        Me.chkAutorunAdmin.AutoSize = True
        Me.chkAutorunAdmin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkAutorunAdmin.ForeColor = System.Drawing.Color.Blue
        Me.chkAutorunAdmin.Location = New System.Drawing.Point(12, 51)
        Me.chkAutorunAdmin.Name = "chkAutorunAdmin"
        Me.chkAutorunAdmin.Size = New System.Drawing.Size(146, 24)
        Me.chkAutorunAdmin.TabIndex = 1
        Me.chkAutorunAdmin.Text = "Startup on logon"
        Me.chkAutorunAdmin.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Enabled = False
        Me.btnReset.Location = New System.Drawing.Point(12, 111)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(134, 23)
        Me.btnReset.TabIndex = 3
        Me.btnReset.Text = "Reset all to defaults"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'LBLspdTip
        '
        Me.LBLspdTip.AutoSize = True
        Me.LBLspdTip.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LBLspdTip.ForeColor = System.Drawing.Color.Red
        Me.LBLspdTip.Location = New System.Drawing.Point(196, 157)
        Me.LBLspdTip.Name = "LBLspdTip"
        Me.LBLspdTip.Size = New System.Drawing.Size(76, 9)
        Me.LBLspdTip.TabIndex = 7
        Me.LBLspdTip.Text = "(lower is better)"
        '
        'numSpd
        '
        Me.numSpd.Location = New System.Drawing.Point(171, 114)
        Me.numSpd.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numSpd.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.numSpd.Name = "numSpd"
        Me.numSpd.Size = New System.Drawing.Size(101, 20)
        Me.numSpd.TabIndex = 5
        Me.numSpd.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'btnService
        '
        Me.btnService.Location = New System.Drawing.Point(0, 143)
        Me.btnService.Name = "btnService"
        Me.btnService.Size = New System.Drawing.Size(75, 23)
        Me.btnService.TabIndex = 2
        Me.btnService.Text = "About"
        Me.btnService.UseVisualStyleBackColor = True
        '
        'tmrDefs
        '
        '
        'frmSets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 168)
        Me.Controls.Add(Me.btnService)
        Me.Controls.Add(Me.numSpd)
        Me.Controls.Add(Me.LBLspdTip)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.chkAutorunAdmin)
        Me.Controls.Add(Me.chkBaloon)
        Me.Controls.Add(Me.LBLspeed)
        Me.Controls.Add(Me.LBLop)
        Me.Controls.Add(Me.trkOp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSets"
        Me.Opacity = 0.7R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        CType(Me.trkOp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents trkOp As System.Windows.Forms.TrackBar
    Friend WithEvents LBLop As System.Windows.Forms.Label
    Friend WithEvents LBLspeed As System.Windows.Forms.Label
    Friend WithEvents chkBaloon As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutorunAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents LBLspdTip As System.Windows.Forms.Label
    Friend WithEvents numSpd As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnService As System.Windows.Forms.Button
    Friend WithEvents tmrDefs As System.Windows.Forms.Timer
End Class
