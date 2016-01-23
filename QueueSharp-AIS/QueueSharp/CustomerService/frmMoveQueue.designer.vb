<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMoveQueue
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TimerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.Group1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbItem = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.ButRefresh = New System.Windows.Forms.Button
        Me.CheckTimer = New System.Windows.Forms.CheckBox
        Me.DomainUp = New System.Windows.Forms.NumericUpDown
        Me.Label10 = New System.Windows.Forms.Label
        Me.tlp = New System.Windows.Forms.TableLayoutPanel
        Me.Group1.SuspendLayout()
        CType(Me.DomainUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerRefresh
        '
        '
        'Group1
        '
        Me.Group1.AutoScroll = True
        Me.Group1.Controls.Add(Me.Label1)
        Me.Group1.Controls.Add(Me.cbItem)
        Me.Group1.Controls.Add(Me.Button1)
        Me.Group1.Controls.Add(Me.ButRefresh)
        Me.Group1.Controls.Add(Me.CheckTimer)
        Me.Group1.Controls.Add(Me.DomainUp)
        Me.Group1.Controls.Add(Me.Label10)
        Me.Group1.Controls.Add(Me.tlp)
        Me.Group1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Group1.Location = New System.Drawing.Point(0, 0)
        Me.Group1.Name = "Group1"
        Me.Group1.Size = New System.Drawing.Size(618, 448)
        Me.Group1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 18)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "บริการ"
        '
        'cbItem
        '
        Me.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.cbItem.FormattingEnabled = True
        Me.cbItem.Location = New System.Drawing.Point(51, 7)
        Me.cbItem.Name = "cbItem"
        Me.cbItem.Size = New System.Drawing.Size(139, 26)
        Me.cbItem.TabIndex = 32
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.AutoSize = True
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Button1.Location = New System.Drawing.Point(528, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(67, 28)
        Me.Button1.TabIndex = 31
        Me.Button1.Tag = "EXP"
        Me.Button1.Text = "Expand"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ButRefresh
        '
        Me.ButRefresh.BackColor = System.Drawing.Color.White
        Me.ButRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ButRefresh.Location = New System.Drawing.Point(196, 5)
        Me.ButRefresh.Name = "ButRefresh"
        Me.ButRefresh.Size = New System.Drawing.Size(75, 28)
        Me.ButRefresh.TabIndex = 30
        Me.ButRefresh.Text = "Refresh"
        Me.ButRefresh.UseVisualStyleBackColor = False
        '
        'CheckTimer
        '
        Me.CheckTimer.AutoSize = True
        Me.CheckTimer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.CheckTimer.Location = New System.Drawing.Point(281, 9)
        Me.CheckTimer.Name = "CheckTimer"
        Me.CheckTimer.Size = New System.Drawing.Size(135, 22)
        Me.CheckTimer.TabIndex = 29
        Me.CheckTimer.Text = "ปรับปรุงข้อมูล ทุกๆ"
        Me.CheckTimer.UseVisualStyleBackColor = True
        '
        'DomainUp
        '
        Me.DomainUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.DomainUp.Location = New System.Drawing.Point(418, 8)
        Me.DomainUp.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.DomainUp.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.DomainUp.Name = "DomainUp"
        Me.DomainUp.Size = New System.Drawing.Size(41, 24)
        Me.DomainUp.TabIndex = 28
        Me.DomainUp.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label10.Location = New System.Drawing.Point(461, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 18)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "วินาที"
        '
        'tlp
        '
        Me.tlp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlp.ColumnCount = 3
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.tlp.Location = New System.Drawing.Point(6, 38)
        Me.tlp.Name = "tlp"
        Me.tlp.RowCount = 1
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlp.Size = New System.Drawing.Size(589, 547)
        Me.tlp.TabIndex = 26
        '
        'frmMoveQueue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(618, 448)
        Me.Controls.Add(Me.Group1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MinimumSize = New System.Drawing.Size(397, 448)
        Me.Name = "frmMoveQueue"
        Me.Text = "Doctor Rooms"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        CType(Me.DomainUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TimerRefresh As System.Windows.Forms.Timer
    Friend WithEvents Group1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ButRefresh As System.Windows.Forms.Button
    Friend WithEvents CheckTimer As System.Windows.Forms.CheckBox
    Friend WithEvents DomainUp As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tlp As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbItem As System.Windows.Forms.ComboBox
End Class
