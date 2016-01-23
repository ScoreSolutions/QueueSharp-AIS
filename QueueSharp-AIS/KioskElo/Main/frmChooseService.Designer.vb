<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChooseService
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChooseService))
        Me.FLP = New System.Windows.Forms.FlowLayoutPanel
        Me.TimerEnd = New System.Windows.Forms.Timer(Me.components)
        Me.pd = New System.Drawing.Printing.PrintDocument
        Me.pbServQ = New System.Windows.Forms.PictureBox
        Me.pbWaitingTime = New System.Windows.Forms.PictureBox
        Me.pbTotalQ = New System.Windows.Forms.PictureBox
        Me.pbWaitService = New System.Windows.Forms.PictureBox
        Me.pbChoseService = New System.Windows.Forms.PictureBox
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.btnEN = New System.Windows.Forms.Button
        Me.btnTH = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.PanelMsg = New System.Windows.Forms.Panel
        Me.lblMsg = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnAppointment = New System.Windows.Forms.Button
        Me.btnMain = New System.Windows.Forms.Button
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.pb = New System.Windows.Forms.PictureBox
        Me.lblCategory = New System.Windows.Forms.Label
        Me.TimerCampaign = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pbServQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbWaitingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTotalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbWaitService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbChoseService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelMsg.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FLP
        '
        Me.FLP.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FLP.AutoScroll = True
        Me.FLP.BackColor = System.Drawing.Color.Transparent
        Me.FLP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FLP.Location = New System.Drawing.Point(29, 294)
        Me.FLP.Margin = New System.Windows.Forms.Padding(0)
        Me.FLP.Name = "FLP"
        Me.FLP.Size = New System.Drawing.Size(655, 391)
        Me.FLP.TabIndex = 53
        '
        'TimerEnd
        '
        Me.TimerEnd.Interval = 3000
        '
        'pd
        '
        '
        'pbServQ
        '
        Me.pbServQ.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pbServQ.BackgroundImage = Global.KioskElo.My.Resources.Resources._15
        Me.pbServQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbServQ.Location = New System.Drawing.Point(574, 244)
        Me.pbServQ.Name = "pbServQ"
        Me.pbServQ.Size = New System.Drawing.Size(101, 28)
        Me.pbServQ.TabIndex = 66
        Me.pbServQ.TabStop = False
        '
        'pbWaitingTime
        '
        Me.pbWaitingTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pbWaitingTime.BackgroundImage = Global.KioskElo.My.Resources.Resources._141
        Me.pbWaitingTime.Location = New System.Drawing.Point(480, 253)
        Me.pbWaitingTime.Name = "pbWaitingTime"
        Me.pbWaitingTime.Size = New System.Drawing.Size(71, 16)
        Me.pbWaitingTime.TabIndex = 65
        Me.pbWaitingTime.TabStop = False
        '
        'pbTotalQ
        '
        Me.pbTotalQ.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pbTotalQ.BackgroundImage = Global.KioskElo.My.Resources.Resources._131
        Me.pbTotalQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbTotalQ.Location = New System.Drawing.Point(365, 249)
        Me.pbTotalQ.Name = "pbTotalQ"
        Me.pbTotalQ.Size = New System.Drawing.Size(95, 23)
        Me.pbTotalQ.TabIndex = 64
        Me.pbTotalQ.TabStop = False
        '
        'pbWaitService
        '
        Me.pbWaitService.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pbWaitService.BackgroundImage = Global.KioskElo.My.Resources.Resources._121
        Me.pbWaitService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbWaitService.Location = New System.Drawing.Point(447, 193)
        Me.pbWaitService.Name = "pbWaitService"
        Me.pbWaitService.Size = New System.Drawing.Size(153, 27)
        Me.pbWaitService.TabIndex = 63
        Me.pbWaitService.TabStop = False
        '
        'pbChoseService
        '
        Me.pbChoseService.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pbChoseService.BackgroundImage = Global.KioskElo.My.Resources.Resources._111
        Me.pbChoseService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbChoseService.Location = New System.Drawing.Point(83, 214)
        Me.pbChoseService.Name = "pbChoseService"
        Me.pbChoseService.Size = New System.Drawing.Size(219, 33)
        Me.pbChoseService.TabIndex = 62
        Me.pbChoseService.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox6.Location = New System.Drawing.Point(1034, 70)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(320, 140)
        Me.PictureBox6.TabIndex = 59
        Me.PictureBox6.TabStop = False
        '
        'btnEN
        '
        Me.btnEN.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnEN.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.BackgroundImage = Global.KioskElo.My.Resources.Resources.EN
        Me.btnEN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEN.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.FlatAppearance.BorderSize = 0
        Me.btnEN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEN.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEN.Location = New System.Drawing.Point(121, 60)
        Me.btnEN.Name = "btnEN"
        Me.btnEN.Size = New System.Drawing.Size(50, 35)
        Me.btnEN.TabIndex = 59
        Me.btnEN.UseVisualStyleBackColor = False
        '
        'btnTH
        '
        Me.btnTH.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnTH.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.BackgroundImage = Global.KioskElo.My.Resources.Resources.TH
        Me.btnTH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTH.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.FlatAppearance.BorderSize = 0
        Me.btnTH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTH.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnTH.Location = New System.Drawing.Point(43, 60)
        Me.btnTH.Name = "btnTH"
        Me.btnTH.Size = New System.Drawing.Size(50, 35)
        Me.btnTH.TabIndex = 58
        Me.btnTH.UseVisualStyleBackColor = False
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
        Me.btnOK.Location = New System.Drawing.Point(1011, 409)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(162, 63)
        Me.btnOK.TabIndex = 49
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'PanelMsg
        '
        Me.PanelMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PanelMsg.BackgroundImage = CType(resources.GetObject("PanelMsg.BackgroundImage"), System.Drawing.Image)
        Me.PanelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelMsg.Controls.Add(Me.lblMsg)
        Me.PanelMsg.Location = New System.Drawing.Point(574, 294)
        Me.PanelMsg.Name = "PanelMsg"
        Me.PanelMsg.Size = New System.Drawing.Size(322, 259)
        Me.PanelMsg.TabIndex = 0
        Me.PanelMsg.Visible = False
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(10, 12)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(303, 235)
        Me.lblMsg.TabIndex = 0
        Me.lblMsg.Text = "[H]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[Y]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[S]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[X]"
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox3
        '
        Me.PictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(559, 288)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(10, 402)
        Me.PictureBox3.TabIndex = 54
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(356, 288)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(10, 402)
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'btnAppointment
        '
        Me.btnAppointment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAppointment.BackColor = System.Drawing.Color.White
        Me.btnAppointment.BackgroundImage = Global.KioskElo.My.Resources.Resources.Untitled_3
        Me.btnAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAppointment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnAppointment.FlatAppearance.BorderSize = 0
        Me.btnAppointment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnAppointment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppointment.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppointment.Location = New System.Drawing.Point(1011, 547)
        Me.btnAppointment.Name = "btnAppointment"
        Me.btnAppointment.Size = New System.Drawing.Size(162, 63)
        Me.btnAppointment.TabIndex = 51
        Me.btnAppointment.UseVisualStyleBackColor = False
        '
        'btnMain
        '
        Me.btnMain.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMain.BackColor = System.Drawing.Color.White
        Me.btnMain.BackgroundImage = Global.KioskElo.My.Resources.Resources._33
        Me.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.BorderSize = 0
        Me.btnMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnMain.Location = New System.Drawing.Point(1011, 478)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(162, 63)
        Me.btnMain.TabIndex = 50
        Me.btnMain.UseVisualStyleBackColor = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = Global.KioskElo.My.Resources.Resources._30
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(72, 176)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(612, 115)
        Me.PictureBox4.TabIndex = 60
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackgroundImage = Global.KioskElo.My.Resources.Resources._30
        Me.PictureBox5.Location = New System.Drawing.Point(29, 176)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(210, 115)
        Me.PictureBox5.TabIndex = 61
        Me.PictureBox5.TabStop = False
        '
        'pb
        '
        Me.pb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb.Location = New System.Drawing.Point(0, 0)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(1366, 768)
        Me.pb.TabIndex = 19
        Me.pb.TabStop = False
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.BackColor = System.Drawing.Color.White
        Me.lblCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(26, 132)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(0, 20)
        Me.lblCategory.TabIndex = 67
        '
        'TimerCampaign
        '
        Me.TimerCampaign.Enabled = True
        Me.TimerCampaign.Interval = 1000
        '
        'frmChooseService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.pbServQ)
        Me.Controls.Add(Me.pbWaitingTime)
        Me.Controls.Add(Me.pbTotalQ)
        Me.Controls.Add(Me.pbWaitService)
        Me.Controls.Add(Me.pbChoseService)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.btnEN)
        Me.Controls.Add(Me.btnTH)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.PanelMsg)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.FLP)
        Me.Controls.Add(Me.btnAppointment)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmChooseService"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmChooseService"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.pbServQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbWaitingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTotalQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbWaitService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbChoseService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelMsg.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnMain As System.Windows.Forms.Button
    Friend WithEvents btnAppointment As System.Windows.Forms.Button
    Friend WithEvents FLP As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pb As System.Windows.Forms.PictureBox
    Friend WithEvents PanelMsg As System.Windows.Forms.Panel
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents TimerEnd As System.Windows.Forms.Timer
    Friend WithEvents btnEN As System.Windows.Forms.Button
    Friend WithEvents btnTH As System.Windows.Forms.Button
    Friend WithEvents pd As System.Drawing.Printing.PrintDocument
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents pbChoseService As System.Windows.Forms.PictureBox
    Friend WithEvents pbWaitService As System.Windows.Forms.PictureBox
    Friend WithEvents pbTotalQ As System.Windows.Forms.PictureBox
    Friend WithEvents pbWaitingTime As System.Windows.Forms.PictureBox
    Friend WithEvents pbServQ As System.Windows.Forms.PictureBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents TimerCampaign As System.Windows.Forms.Timer
End Class
