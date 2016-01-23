<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompactView_M
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
        Me.Pg = New System.Windows.Forms.PictureBox
        Me.lblRoom = New System.Windows.Forms.Label
        Me.btnMain = New System.Windows.Forms.Button
        Me.lblStstus = New System.Windows.Forms.Label
        Me.btnCall = New System.Windows.Forms.Button
        Me.btnEnd = New System.Windows.Forms.Button
        Me.btnhold = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Down = New System.Windows.Forms.Button
        Me.STimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnReSize = New System.Windows.Forms.Button
        CType(Me.Pg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pg
        '
        Me.Pg.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_09_21
        Me.Pg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Pg.Location = New System.Drawing.Point(7, 8)
        Me.Pg.Name = "Pg"
        Me.Pg.Size = New System.Drawing.Size(54, 52)
        Me.Pg.TabIndex = 6
        Me.Pg.TabStop = False
        '
        'lblRoom
        '
        Me.lblRoom.BackColor = System.Drawing.Color.DimGray
        Me.lblRoom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRoom.ForeColor = System.Drawing.Color.White
        Me.lblRoom.Location = New System.Drawing.Point(12, 64)
        Me.lblRoom.Name = "lblRoom"
        Me.lblRoom.Size = New System.Drawing.Size(110, 33)
        Me.lblRoom.TabIndex = 7
        Me.lblRoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMain
        '
        Me.btnMain.AllowDrop = True
        Me.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMain.FlatAppearance.BorderSize = 0
        Me.btnMain.Image = Global.QueueSharp.My.Resources.Resources.QSharp_popup_14_21
        Me.btnMain.Location = New System.Drawing.Point(403, 20)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(27, 26)
        Me.btnMain.TabIndex = 8
        Me.btnMain.UseVisualStyleBackColor = True
        '
        'lblStstus
        '
        Me.lblStstus.BackColor = System.Drawing.Color.Transparent
        Me.lblStstus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblStstus.ForeColor = System.Drawing.Color.Black
        Me.lblStstus.Location = New System.Drawing.Point(138, 65)
        Me.lblStstus.Name = "lblStstus"
        Me.lblStstus.Size = New System.Drawing.Size(290, 31)
        Me.lblStstus.TabIndex = 4
        Me.lblStstus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCall
        '
        Me.btnCall.AllowDrop = True
        Me.btnCall.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_10
        Me.btnCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCall.Location = New System.Drawing.Point(63, 8)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(85, 52)
        Me.btnCall.TabIndex = 1
        Me.btnCall.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.AllowDrop = True
        Me.btnEnd.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_11
        Me.btnEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnd.Location = New System.Drawing.Point(147, 8)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(85, 52)
        Me.btnEnd.TabIndex = 2
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnhold
        '
        Me.btnhold.AllowDrop = True
        Me.btnhold.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_13
        Me.btnhold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnhold.Location = New System.Drawing.Point(315, 8)
        Me.btnhold.Name = "btnhold"
        Me.btnhold.Size = New System.Drawing.Size(85, 52)
        Me.btnhold.TabIndex = 5
        Me.btnhold.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.AllowDrop = True
        Me.btnCancel.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_12
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.Location = New System.Drawing.Point(231, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 52)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Down
        '
        Me.Down.BackColor = System.Drawing.Color.Silver
        Me.Down.Location = New System.Drawing.Point(411, 93)
        Me.Down.Name = "Down"
        Me.Down.Size = New System.Drawing.Size(18, 15)
        Me.Down.TabIndex = 10
        Me.Down.Text = "="
        Me.Down.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Down.UseVisualStyleBackColor = False
        '
        'STimer
        '
        '
        'btnReSize
        '
        Me.btnReSize.AllowDrop = True
        Me.btnReSize.BackColor = System.Drawing.Color.Silver
        Me.btnReSize.BackgroundImage = Global.QueueSharp.My.Resources.Resources.breakpoint_into
        Me.btnReSize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReSize.FlatAppearance.BorderSize = 0
        Me.btnReSize.Location = New System.Drawing.Point(405, 37)
        Me.btnReSize.Name = "btnReSize"
        Me.btnReSize.Size = New System.Drawing.Size(22, 20)
        Me.btnReSize.TabIndex = 12
        Me.btnReSize.UseVisualStyleBackColor = False
        Me.btnReSize.Visible = False
        '
        'frmCompactView_M
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Magenta
        Me.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_BG1
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(439, 110)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnReSize)
        Me.Controls.Add(Me.Down)
        Me.Controls.Add(Me.lblRoom)
        Me.Controls.Add(Me.Pg)
        Me.Controls.Add(Me.lblStstus)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.btnCall)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnhold)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCompactView_M"
        Me.Opacity = 0.7
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Magenta
        CType(Me.Pg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCall As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblStstus As System.Windows.Forms.Label
    Friend WithEvents btnhold As System.Windows.Forms.Button
    Friend WithEvents Pg As System.Windows.Forms.PictureBox
    Friend WithEvents lblRoom As System.Windows.Forms.Label
    Friend WithEvents btnMain As System.Windows.Forms.Button
    Friend WithEvents Down As System.Windows.Forms.Button
    Friend WithEvents STimer As System.Windows.Forms.Timer
    Friend WithEvents btnReSize As System.Windows.Forms.Button

End Class
