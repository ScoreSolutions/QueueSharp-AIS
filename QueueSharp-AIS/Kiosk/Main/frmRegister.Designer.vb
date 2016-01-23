<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegister))
        Me.TimerVDO = New System.Windows.Forms.Timer(Me.components)
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.pd = New System.Drawing.Printing.PrintDocument
        Me.TimerCheckCaptureFile = New System.Windows.Forms.Timer(Me.components)
        Me.btnEN = New System.Windows.Forms.Button
        Me.btnTH = New System.Windows.Forms.Button
        Me.PanelMsg = New System.Windows.Forms.Panel
        Me.btnAppOK = New System.Windows.Forms.Button
        Me.lblMsg = New System.Windows.Forms.Label
        Me.btnAppBack = New System.Windows.Forms.Button
        Me.btnAppCancel = New System.Windows.Forms.Button
        Me.btnAppNew = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnMain = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.btn0 = New System.Windows.Forms.Button
        Me.btn9 = New System.Windows.Forms.Button
        Me.btn8 = New System.Windows.Forms.Button
        Me.btn7 = New System.Windows.Forms.Button
        Me.btn6 = New System.Windows.Forms.Button
        Me.btn5 = New System.Windows.Forms.Button
        Me.btn4 = New System.Windows.Forms.Button
        Me.btn3 = New System.Windows.Forms.Button
        Me.btn2 = New System.Windows.Forms.Button
        Me.btn1 = New System.Windows.Forms.Button
        Me.pb = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PanelMsg.SuspendLayout()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerVDO
        '
        Me.TimerVDO.Interval = 1000
        '
        'txtMobile
        '
        Me.txtMobile.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtMobile.BackColor = System.Drawing.Color.White
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 50.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtMobile.ForeColor = System.Drawing.Color.Black
        Me.txtMobile.Location = New System.Drawing.Point(140, 302)
        Me.txtMobile.MaxLength = 10
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(530, 76)
        Me.txtMobile.TabIndex = 52
        Me.txtMobile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pd
        '
        '
        'TimerCheckCaptureFile
        '
        Me.TimerCheckCaptureFile.Enabled = True
        Me.TimerCheckCaptureFile.Interval = 600000
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
        Me.btnEN.Location = New System.Drawing.Point(635, 124)
        Me.btnEN.Name = "btnEN"
        Me.btnEN.Size = New System.Drawing.Size(50, 35)
        Me.btnEN.TabIndex = 57
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
        Me.btnTH.Location = New System.Drawing.Point(579, 124)
        Me.btnTH.Name = "btnTH"
        Me.btnTH.Size = New System.Drawing.Size(50, 35)
        Me.btnTH.TabIndex = 56
        Me.btnTH.UseVisualStyleBackColor = False
        '
        'PanelMsg
        '
        Me.PanelMsg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelMsg.BackgroundImage = Global.Kiosk.My.Resources.Resources.msg
        Me.PanelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelMsg.Controls.Add(Me.btnAppOK)
        Me.PanelMsg.Controls.Add(Me.lblMsg)
        Me.PanelMsg.Controls.Add(Me.btnAppBack)
        Me.PanelMsg.Controls.Add(Me.btnAppCancel)
        Me.PanelMsg.Controls.Add(Me.btnAppNew)
        Me.PanelMsg.Location = New System.Drawing.Point(222, 468)
        Me.PanelMsg.Name = "PanelMsg"
        Me.PanelMsg.Size = New System.Drawing.Size(363, 449)
        Me.PanelMsg.TabIndex = 53
        Me.PanelMsg.Visible = False
        '
        'btnAppOK
        '
        Me.btnAppOK.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAppOK.BackColor = System.Drawing.Color.Transparent
        Me.btnAppOK.FlatAppearance.BorderSize = 0
        Me.btnAppOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppOK.Location = New System.Drawing.Point(56, 380)
        Me.btnAppOK.Name = "btnAppOK"
        Me.btnAppOK.Size = New System.Drawing.Size(82, 60)
        Me.btnAppOK.TabIndex = 3
        Me.btnAppOK.Text = "ตกลง"
        Me.btnAppOK.UseVisualStyleBackColor = False
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(15, 14)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(333, 361)
        Me.lblMsg.TabIndex = 0
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAppBack
        '
        Me.btnAppBack.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAppBack.BackColor = System.Drawing.Color.Transparent
        Me.btnAppBack.FlatAppearance.BorderSize = 0
        Me.btnAppBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppBack.Location = New System.Drawing.Point(245, 380)
        Me.btnAppBack.Name = "btnAppBack"
        Me.btnAppBack.Size = New System.Drawing.Size(98, 60)
        Me.btnAppBack.TabIndex = 1
        Me.btnAppBack.Text = "หน้าหลัก"
        Me.btnAppBack.UseVisualStyleBackColor = False
        '
        'btnAppCancel
        '
        Me.btnAppCancel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAppCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnAppCancel.FlatAppearance.BorderSize = 0
        Me.btnAppCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppCancel.Location = New System.Drawing.Point(130, 380)
        Me.btnAppCancel.Name = "btnAppCancel"
        Me.btnAppCancel.Size = New System.Drawing.Size(125, 60)
        Me.btnAppCancel.TabIndex = 4
        Me.btnAppCancel.Text = "ยกเลิกการจอง"
        Me.btnAppCancel.UseVisualStyleBackColor = False
        '
        'btnAppNew
        '
        Me.btnAppNew.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAppNew.BackColor = System.Drawing.Color.Transparent
        Me.btnAppNew.FlatAppearance.BorderSize = 0
        Me.btnAppNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppNew.Location = New System.Drawing.Point(140, 380)
        Me.btnAppNew.Name = "btnAppNew"
        Me.btnAppNew.Size = New System.Drawing.Size(111, 60)
        Me.btnAppNew.TabIndex = 2
        Me.btnAppNew.Text = "รับคิวใหม่"
        Me.btnAppNew.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.BackgroundImage = Global.Kiosk.My.Resources.Resources.Close
        Me.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Location = New System.Drawing.Point(22, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(26, 23)
        Me.btnExit.TabIndex = 51
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnMain
        '
        Me.btnMain.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.BackgroundImage = Global.Kiosk.My.Resources.Resources.btmMain_TH
        Me.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.BorderSize = 0
        Me.btnMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnMain.Location = New System.Drawing.Point(483, 1145)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(250, 78)
        Me.btnMain.TabIndex = 50
        Me.btnMain.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEdit.BackgroundImage = Global.Kiosk.My.Resources.Resources.btnEdit_TH
        Me.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEdit.FlatAppearance.BorderSize = 0
        Me.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(279, 1145)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(194, 78)
        Me.btnEdit.TabIndex = 49
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.BackgroundImage = Global.Kiosk.My.Resources.Resources.btnOK_TH
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnOK.Location = New System.Drawing.Point(74, 1145)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(194, 78)
        Me.btnOK.TabIndex = 48
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btn0
        '
        Me.btn0.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn0.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn0.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn0.FlatAppearance.BorderSize = 0
        Me.btn0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn0.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn0.Image = CType(resources.GetObject("btn0.Image"), System.Drawing.Image)
        Me.btn0.Location = New System.Drawing.Point(188, 908)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(432, 123)
        Me.btn0.TabIndex = 47
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn9.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn9.FlatAppearance.BorderSize = 0
        Me.btn9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn9.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn9.Image = CType(resources.GetObject("btn9.Image"), System.Drawing.Image)
        Me.btn9.Location = New System.Drawing.Point(490, 755)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(128, 123)
        Me.btn9.TabIndex = 46
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn8.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn8.FlatAppearance.BorderSize = 0
        Me.btn8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn8.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn8.Image = CType(resources.GetObject("btn8.Image"), System.Drawing.Image)
        Me.btn8.Location = New System.Drawing.Point(339, 755)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(128, 123)
        Me.btn8.TabIndex = 45
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn7.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn7.FlatAppearance.BorderSize = 0
        Me.btn7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn7.Image = CType(resources.GetObject("btn7.Image"), System.Drawing.Image)
        Me.btn7.Location = New System.Drawing.Point(186, 755)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(128, 123)
        Me.btn7.TabIndex = 44
        Me.btn7.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn6.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn6.FlatAppearance.BorderSize = 0
        Me.btn6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn6.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn6.Image = CType(resources.GetObject("btn6.Image"), System.Drawing.Image)
        Me.btn6.Location = New System.Drawing.Point(490, 605)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(128, 123)
        Me.btn6.TabIndex = 43
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn5.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn5.FlatAppearance.BorderSize = 0
        Me.btn5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn5.Image = CType(resources.GetObject("btn5.Image"), System.Drawing.Image)
        Me.btn5.Location = New System.Drawing.Point(339, 605)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(128, 123)
        Me.btn5.TabIndex = 42
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn4.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn4.FlatAppearance.BorderSize = 0
        Me.btn4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn4.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn4.Image = CType(resources.GetObject("btn4.Image"), System.Drawing.Image)
        Me.btn4.Location = New System.Drawing.Point(186, 605)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(128, 123)
        Me.btn4.TabIndex = 41
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn3.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn3.FlatAppearance.BorderSize = 0
        Me.btn3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn3.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn3.Image = CType(resources.GetObject("btn3.Image"), System.Drawing.Image)
        Me.btn3.Location = New System.Drawing.Point(490, 457)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(128, 121)
        Me.btn3.TabIndex = 40
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn2.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn2.FlatAppearance.BorderSize = 0
        Me.btn2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn2.Image = CType(resources.GetObject("btn2.Image"), System.Drawing.Image)
        Me.btn2.Location = New System.Drawing.Point(339, 457)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(128, 121)
        Me.btn2.TabIndex = 39
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn1.FlatAppearance.BorderSize = 0
        Me.btn1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn1.Image = CType(resources.GetObject("btn1.Image"), System.Drawing.Image)
        Me.btn1.Location = New System.Drawing.Point(186, 457)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(128, 121)
        Me.btn1.TabIndex = 38
        Me.btn1.UseVisualStyleBackColor = False
        '
        'pb
        '
        Me.pb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb.BackgroundImage = Global.Kiosk.My.Resources.Resources.Kiosk_TH
        Me.pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb.Location = New System.Drawing.Point(13, 0)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(775, 1280)
        Me.pb.TabIndex = 18
        Me.pb.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(546, 33)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(191, 78)
        Me.PictureBox1.TabIndex = 61
        Me.PictureBox1.TabStop = False
        '
        'frmRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 780)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnEN)
        Me.Controls.Add(Me.btnTH)
        Me.Controls.Add(Me.PanelMsg)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btn0)
        Me.Controls.Add(Me.btn9)
        Me.Controls.Add(Me.btn8)
        Me.Controls.Add(Me.btn7)
        Me.Controls.Add(Me.btn6)
        Me.Controls.Add(Me.btn5)
        Me.Controls.Add(Me.btn4)
        Me.Controls.Add(Me.btn3)
        Me.Controls.Add(Me.btn2)
        Me.Controls.Add(Me.btn1)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSlot"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelMsg.ResumeLayout(False)
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerVDO As System.Windows.Forms.Timer
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents btn0 As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnMain As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents pb As System.Windows.Forms.PictureBox
    Friend WithEvents btnTH As System.Windows.Forms.Button
    Friend WithEvents btnEN As System.Windows.Forms.Button
    Friend WithEvents pd As System.Drawing.Printing.PrintDocument
    Friend WithEvents TimerCheckCaptureFile As System.Windows.Forms.Timer
    Friend WithEvents btnAppNew As System.Windows.Forms.Button
    Friend WithEvents btnAppCancel As System.Windows.Forms.Button
    Friend WithEvents btnAppBack As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents btnAppOK As System.Windows.Forms.Button
    Friend WithEvents PanelMsg As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
