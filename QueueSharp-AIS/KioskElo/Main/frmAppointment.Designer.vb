<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAppointment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAppointment))
        Me.FLP = New System.Windows.Forms.FlowLayoutPanel
        Me.PanelMsg = New System.Windows.Forms.Panel
        Me.lblFooter = New System.Windows.Forms.Label
        Me.lblMsg = New System.Windows.Forms.Label
        Me.TimerEnd = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.btnMain = New System.Windows.Forms.Button
        Me.btnP = New System.Windows.Forms.Button
        Me.pb = New System.Windows.Forms.PictureBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.PanelMsg.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FLP
        '
        Me.FLP.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FLP.AutoScroll = True
        Me.FLP.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.FLP.Location = New System.Drawing.Point(96, 86)
        Me.FLP.Name = "FLP"
        Me.FLP.Size = New System.Drawing.Size(669, 636)
        Me.FLP.TabIndex = 52
        '
        'PanelMsg
        '
        Me.PanelMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PanelMsg.BackgroundImage = CType(resources.GetObject("PanelMsg.BackgroundImage"), System.Drawing.Image)
        Me.PanelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelMsg.Controls.Add(Me.lblFooter)
        Me.PanelMsg.Controls.Add(Me.lblMsg)
        Me.PanelMsg.Location = New System.Drawing.Point(435, 86)
        Me.PanelMsg.Name = "PanelMsg"
        Me.PanelMsg.Size = New System.Drawing.Size(330, 363)
        Me.PanelMsg.TabIndex = 63
        Me.PanelMsg.Visible = False
        '
        'lblFooter
        '
        Me.lblFooter.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblFooter.BackColor = System.Drawing.Color.Transparent
        Me.lblFooter.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, CType(222, Byte))
        Me.lblFooter.Location = New System.Drawing.Point(9, 315)
        Me.lblFooter.Name = "lblFooter"
        Me.lblFooter.Size = New System.Drawing.Size(313, 39)
        Me.lblFooter.TabIndex = 1
        Me.lblFooter.Text = "Please arrive [Z] minutes before the appointment time." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Late arrival than the sch" & _
            "eduled time " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "will automatically cancel the appointment." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(14, 15)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(303, 291)
        Me.lblMsg.TabIndex = 0
        Me.lblMsg.Text = "[Y]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[S]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[X]"
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimerEnd
        '
        Me.TimerEnd.Interval = 5000
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox6.Location = New System.Drawing.Point(1042, -3)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(320, 140)
        Me.PictureBox6.TabIndex = 64
        Me.PictureBox6.TabStop = False
        '
        'btnMain
        '
        Me.btnMain.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.BackgroundImage = Global.KioskElo.My.Resources.Resources._33
        Me.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.BorderSize = 0
        Me.btnMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnMain.Location = New System.Drawing.Point(945, 572)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(162, 63)
        Me.btnMain.TabIndex = 54
        Me.btnMain.UseVisualStyleBackColor = False
        '
        'btnP
        '
        Me.btnP.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnP.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnP.BackgroundImage = Global.KioskElo.My.Resources.Resources.previous_01
        Me.btnP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnP.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnP.FlatAppearance.BorderSize = 0
        Me.btnP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnP.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnP.Location = New System.Drawing.Point(945, 501)
        Me.btnP.Name = "btnP"
        Me.btnP.Size = New System.Drawing.Size(162, 63)
        Me.btnP.TabIndex = 50
        Me.btnP.UseVisualStyleBackColor = False
        '
        'pb
        '
        Me.pb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb.Location = New System.Drawing.Point(20, -3)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(793, 1280)
        Me.pb.TabIndex = 20
        Me.pb.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnOK.BackColor = System.Drawing.Color.White
        Me.btnOK.BackgroundImage = Global.KioskElo.My.Resources.Resources.Untitled_11
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnOK.Location = New System.Drawing.Point(945, 428)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(162, 63)
        Me.btnOK.TabIndex = 65
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'frmAppointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.PanelMsg)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.FLP)
        Me.Controls.Add(Me.btnP)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmAppointment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmSuccess"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelMsg.ResumeLayout(False)
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pb As System.Windows.Forms.PictureBox
    Friend WithEvents btnP As System.Windows.Forms.Button
    Friend WithEvents FLP As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TimerEnd As System.Windows.Forms.Timer
    Friend WithEvents btnMain As System.Windows.Forms.Button
    Friend WithEvents PanelMsg As System.Windows.Forms.Panel
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblFooter As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
End Class
