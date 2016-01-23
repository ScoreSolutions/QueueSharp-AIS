<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompactView_B
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblroom = New System.Windows.Forms.Label
        Me.btnMain = New System.Windows.Forms.Button
        Me.lblNextroom = New System.Windows.Forms.Label
        Me.btnCall = New System.Windows.Forms.Button
        Me.btnEnd = New System.Windows.Forms.Button
        Me.btnhold = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Down = New System.Windows.Forms.Button
        Me.STimer = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_09_2
        Me.PictureBox1.Location = New System.Drawing.Point(7, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(78, 74)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'lblroom
        '
        Me.lblroom.BackColor = System.Drawing.Color.Transparent
        Me.lblroom.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblroom.ForeColor = System.Drawing.Color.Black
        Me.lblroom.Location = New System.Drawing.Point(17, 95)
        Me.lblroom.Name = "lblroom"
        Me.lblroom.Size = New System.Drawing.Size(74, 41)
        Me.lblroom.TabIndex = 7
        '
        'btnMain
        '
        Me.btnMain.AllowDrop = True
        Me.btnMain.FlatAppearance.BorderSize = 0
        Me.btnMain.Image = Global.QueueSharp.My.Resources.Resources.QSharp_popup_14_2
        Me.btnMain.Location = New System.Drawing.Point(582, 14)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(38, 34)
        Me.btnMain.TabIndex = 8
        Me.btnMain.UseVisualStyleBackColor = True
        '
        'lblNextroom
        '
        Me.lblNextroom.BackColor = System.Drawing.Color.Transparent
        Me.lblNextroom.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblNextroom.ForeColor = System.Drawing.Color.Black
        Me.lblNextroom.Location = New System.Drawing.Point(119, 94)
        Me.lblNextroom.Name = "lblNextroom"
        Me.lblNextroom.Size = New System.Drawing.Size(490, 41)
        Me.lblNextroom.TabIndex = 4
        Me.lblNextroom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCall
        '
        Me.btnCall.AllowDrop = True
        Me.btnCall.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_10
        Me.btnCall.Location = New System.Drawing.Point(87, 12)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(123, 74)
        Me.btnCall.TabIndex = 1
        Me.btnCall.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.AllowDrop = True
        Me.btnEnd.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_11
        Me.btnEnd.Location = New System.Drawing.Point(209, 12)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(123, 74)
        Me.btnEnd.TabIndex = 2
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnhold
        '
        Me.btnhold.AllowDrop = True
        Me.btnhold.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_13
        Me.btnhold.Location = New System.Drawing.Point(458, 12)
        Me.btnhold.Name = "btnhold"
        Me.btnhold.Size = New System.Drawing.Size(123, 74)
        Me.btnhold.TabIndex = 5
        Me.btnhold.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.AllowDrop = True
        Me.btnCancel.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_12
        Me.btnCancel.Location = New System.Drawing.Point(333, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(123, 74)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Down
        '
        Me.Down.BackColor = System.Drawing.Color.Silver
        Me.Down.Location = New System.Drawing.Point(585, 137)
        Me.Down.Name = "Down"
        Me.Down.Size = New System.Drawing.Size(27, 15)
        Me.Down.TabIndex = 10
        Me.Down.Text = "="
        Me.Down.UseVisualStyleBackColor = False
        '
        'STimer
        '
        '
        'Button1
        '
        Me.Button1.AllowDrop = True
        Me.Button1.BackColor = System.Drawing.Color.Silver
        Me.Button1.BackgroundImage = Global.QueueSharp.My.Resources.Resources.breakpoint_into
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.Location = New System.Drawing.Point(588, 54)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(27, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmFloat
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Magenta
        Me.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_BG
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(631, 160)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Down)
        Me.Controls.Add(Me.lblroom)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblNextroom)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.btnCall)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnhold)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmFloat"
        Me.Opacity = 0.7
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Magenta
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCall As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblNextroom As System.Windows.Forms.Label
    Friend WithEvents btnhold As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblroom As System.Windows.Forms.Label
    Friend WithEvents btnMain As System.Windows.Forms.Button
    Friend WithEvents Down As System.Windows.Forms.Button
    Friend WithEvents STimer As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
