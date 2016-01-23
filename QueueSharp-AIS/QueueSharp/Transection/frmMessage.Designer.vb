<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMessage
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
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboSnooze = New System.Windows.Forms.ComboBox
        Me.ButSnooze = New System.Windows.Forms.Button
        Me.ButDismiss = New System.Windows.Forms.Button
        Me.TextMessage = New System.Windows.Forms.TextBox
        Me.GroupBoxButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxButton
        '
        Me.GroupBoxButton.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxButton.BackgroundGradientColor = System.Drawing.Color.Orange
        Me.GroupBoxButton.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxButton.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxButton.BorderThickness = 1.0!
        Me.GroupBoxButton.Controls.Add(Me.Label2)
        Me.GroupBoxButton.Controls.Add(Me.ComboSnooze)
        Me.GroupBoxButton.Controls.Add(Me.ButSnooze)
        Me.GroupBoxButton.Controls.Add(Me.ButDismiss)
        Me.GroupBoxButton.Controls.Add(Me.TextMessage)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(9, -1)
        Me.GroupBoxButton.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(408, 251)
        Me.GroupBoxButton.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(333, 208)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "นาที"
        '
        'ComboSnooze
        '
        Me.ComboSnooze.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboSnooze.FormattingEnabled = True
        Me.ComboSnooze.Location = New System.Drawing.Point(257, 205)
        Me.ComboSnooze.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboSnooze.Name = "ComboSnooze"
        Me.ComboSnooze.Size = New System.Drawing.Size(68, 26)
        Me.ComboSnooze.TabIndex = 1
        '
        'ButSnooze
        '
        Me.ButSnooze.Location = New System.Drawing.Point(137, 202)
        Me.ButSnooze.Margin = New System.Windows.Forms.Padding(4)
        Me.ButSnooze.Name = "ButSnooze"
        Me.ButSnooze.Size = New System.Drawing.Size(112, 32)
        Me.ButSnooze.TabIndex = 0
        Me.ButSnooze.Text = "เลื่อน >>"
        Me.ButSnooze.UseVisualStyleBackColor = True
        '
        'ButDismiss
        '
        Me.ButDismiss.Location = New System.Drawing.Point(14, 202)
        Me.ButDismiss.Margin = New System.Windows.Forms.Padding(4)
        Me.ButDismiss.Name = "ButDismiss"
        Me.ButDismiss.Size = New System.Drawing.Size(112, 32)
        Me.ButDismiss.TabIndex = 2
        Me.ButDismiss.Text = "ยกเลิกข้อความ"
        Me.ButDismiss.UseVisualStyleBackColor = True
        '
        'TextMessage
        '
        Me.TextMessage.BackColor = System.Drawing.Color.White
        Me.TextMessage.Enabled = False
        Me.TextMessage.ForeColor = System.Drawing.Color.Navy
        Me.TextMessage.Location = New System.Drawing.Point(14, 24)
        Me.TextMessage.Margin = New System.Windows.Forms.Padding(4)
        Me.TextMessage.MaxLength = 200
        Me.TextMessage.Multiline = True
        Me.TextMessage.Name = "TextMessage"
        Me.TextMessage.Size = New System.Drawing.Size(379, 167)
        Me.TextMessage.TabIndex = 0
        '
        'frmMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(425, 257)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBoxButton)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMessage"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กล่องข้อความ"
        Me.TopMost = True
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents ButDismiss As System.Windows.Forms.Button
    Friend WithEvents TextMessage As System.Windows.Forms.TextBox
    Friend WithEvents ComboSnooze As System.Windows.Forms.ComboBox
    Friend WithEvents ButSnooze As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
