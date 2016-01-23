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
        Me.btnEN = New System.Windows.Forms.Button
        Me.btnTH = New System.Windows.Forms.Button
        Me.PanelMsg = New System.Windows.Forms.Panel
        Me.lblMsg = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnAppointment = New System.Windows.Forms.Button
        Me.btnMain = New System.Windows.Forms.Button
        Me.pb = New System.Windows.Forms.PictureBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.TimerCampaign = New System.Windows.Forms.Timer(Me.components)
        Me.lblCategory = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PanelMsg.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FLP
        '
        Me.FLP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FLP.AutoScroll = True
        Me.FLP.BackColor = System.Drawing.Color.Transparent
        Me.FLP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FLP.Location = New System.Drawing.Point(57, 337)
        Me.FLP.Margin = New System.Windows.Forms.Padding(0)
        Me.FLP.Name = "FLP"
        Me.FLP.Size = New System.Drawing.Size(679, 391)
        Me.FLP.TabIndex = 53
        '
        'TimerEnd
        '
        Me.TimerEnd.Interval = 3000
        '
        'pd
        '
        '
        'btnEN
        '
        Me.btnEN.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnEN.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.BackgroundImage = Global.Kiosk.My.Resources.Resources.EN
        Me.btnEN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEN.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.FlatAppearance.BorderSize = 0
        Me.btnEN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEN.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEN.Location = New System.Drawing.Point(668, 128)
        Me.btnEN.Name = "btnEN"
        Me.btnEN.Size = New System.Drawing.Size(50, 35)
        Me.btnEN.TabIndex = 59
        Me.btnEN.UseVisualStyleBackColor = False
        '
        'btnTH
        '
        Me.btnTH.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnTH.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.BackgroundImage = Global.Kiosk.My.Resources.Resources.TH
        Me.btnTH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTH.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.FlatAppearance.BorderSize = 0
        Me.btnTH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnTH.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTH.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnTH.Location = New System.Drawing.Point(612, 128)
        Me.btnTH.Name = "btnTH"
        Me.btnTH.Size = New System.Drawing.Size(50, 35)
        Me.btnTH.TabIndex = 58
        Me.btnTH.UseVisualStyleBackColor = False
        '
        'PanelMsg
        '
        Me.PanelMsg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelMsg.BackgroundImage = CType(resources.GetObject("PanelMsg.BackgroundImage"), System.Drawing.Image)
        Me.PanelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelMsg.Controls.Add(Me.lblMsg)
        Me.PanelMsg.Location = New System.Drawing.Point(244, 398)
        Me.PanelMsg.Name = "PanelMsg"
        Me.PanelMsg.Size = New System.Drawing.Size(339, 259)
        Me.PanelMsg.TabIndex = 0
        Me.PanelMsg.Visible = False
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(18, 12)
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
        Me.PictureBox3.Location = New System.Drawing.Point(604, 337)
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
        Me.PictureBox2.Location = New System.Drawing.Point(401, 337)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(10, 402)
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'btnAppointment
        '
        Me.btnAppointment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAppointment.BackColor = System.Drawing.Color.White
        Me.btnAppointment.BackgroundImage = Global.Kiosk.My.Resources.Resources.btnAppointment_TH
        Me.btnAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAppointment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnAppointment.FlatAppearance.BorderSize = 0
        Me.btnAppointment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnAppointment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppointment.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppointment.Location = New System.Drawing.Point(494, 838)
        Me.btnAppointment.Name = "btnAppointment"
        Me.btnAppointment.Size = New System.Drawing.Size(211, 63)
        Me.btnAppointment.TabIndex = 51
        Me.btnAppointment.UseVisualStyleBackColor = False
        '
        'btnMain
        '
        Me.btnMain.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMain.BackColor = System.Drawing.Color.White
        Me.btnMain.BackgroundImage = Global.Kiosk.My.Resources.Resources.btnBack_TH1
        Me.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.BorderSize = 0
        Me.btnMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnMain.Location = New System.Drawing.Point(494, 760)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(211, 63)
        Me.btnMain.TabIndex = 50
        Me.btnMain.UseVisualStyleBackColor = False
        '
        'pb
        '
        Me.pb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb.BackgroundImage = Global.Kiosk.My.Resources.Resources.ChooseService_EN
        Me.pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb.Location = New System.Drawing.Point(13, 0)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(768, 1300)
        Me.pb.TabIndex = 19
        Me.pb.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnOK.BackColor = System.Drawing.Color.White
        Me.btnOK.BackgroundImage = Global.Kiosk.My.Resources.Resources.btnOK_TH
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnOK.Location = New System.Drawing.Point(282, 760)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(194, 63)
        Me.btnOK.TabIndex = 49
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'TimerCampaign
        '
        Me.TimerCampaign.Interval = 2000
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.BackColor = System.Drawing.Color.White
        Me.lblCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCategory.ForeColor = System.Drawing.Color.White
        Me.lblCategory.Location = New System.Drawing.Point(66, 149)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(0, 37)
        Me.lblCategory.TabIndex = 61
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(68, 1007)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(660, 190)
        Me.PictureBox1.TabIndex = 52
        Me.PictureBox1.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(562, 37)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(189, 78)
        Me.PictureBox4.TabIndex = 62
        Me.PictureBox4.TabStop = False
        '
        'frmChooseService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 767)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.btnEN)
        Me.Controls.Add(Me.btnTH)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.PanelMsg)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.FLP)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnAppointment)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmChooseService"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmChooseService"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelMsg.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents TimerCampaign As System.Windows.Forms.Timer
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
End Class
