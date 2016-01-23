<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHQAgent
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHQAgent))
        Me.tmGenSiebel = New System.Windows.Forms.Timer(Me.components)
        Me.chkGenSiebel = New System.Windows.Forms.CheckBox
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.lblTime = New System.Windows.Forms.Label
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.chkBlackList = New System.Windows.Forms.CheckBox
        Me.tmBlacklist = New System.Windows.Forms.Timer(Me.components)
        Me.chkSendAppointmentNotify = New System.Windows.Forms.CheckBox
        Me.tmSendAppointmentNotify = New System.Windows.Forms.Timer(Me.components)
        Me.btnTest = New System.Windows.Forms.Button
        Me.dtpST = New System.Windows.Forms.DateTimePicker
        Me.dtpET = New System.Windows.Forms.DateTimePicker
        Me.cbT = New System.Windows.Forms.CheckBox
        Me.cbD = New System.Windows.Forms.CheckBox
        Me.cbM = New System.Windows.Forms.CheckBox
        Me.cbW = New System.Windows.Forms.CheckBox
        Me.cbY = New System.Windows.Forms.CheckBox
        Me.cbSeg = New System.Windows.Forms.CheckBox
        Me.cbNet = New System.Windows.Forms.CheckBox
        Me.cbWTsh = New System.Windows.Forms.CheckBox
        Me.cbWTsk = New System.Windows.Forms.CheckBox
        Me.cbWTst = New System.Windows.Forms.CheckBox
        Me.cbKPIsh = New System.Windows.Forms.CheckBox
        Me.cbKPIst = New System.Windows.Forms.CheckBox
        Me.cbPro = New System.Windows.Forms.CheckBox
        Me.cbCa = New System.Windows.Forms.CheckBox
        Me.cbSt = New System.Windows.Forms.CheckBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmbShop = New System.Windows.Forms.ComboBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.tmCSISurvey = New System.Windows.Forms.Timer(Me.components)
        Me.tmWorkForce = New System.Windows.Forms.Timer(Me.components)
        Me.tmArchMaster = New System.Windows.Forms.Timer(Me.components)
        Me.tmCutOffData = New System.Windows.Forms.Timer(Me.components)
        Me.tmClearCustomerInfo = New System.Windows.Forms.Timer(Me.components)
        Me.chkWorkforce = New System.Windows.Forms.CheckBox
        Me.chkCSI = New System.Windows.Forms.CheckBox
        Me.chkArchMaster = New System.Windows.Forms.CheckBox
        Me.chkCutOffData = New System.Windows.Forms.CheckBox
        Me.chkClearCustInfo = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmGenSiebel
        '
        Me.tmGenSiebel.Enabled = True
        Me.tmGenSiebel.Interval = 5000
        '
        'chkGenSiebel
        '
        Me.chkGenSiebel.AutoSize = True
        Me.chkGenSiebel.Location = New System.Drawing.Point(12, 12)
        Me.chkGenSiebel.Name = "chkGenSiebel"
        Me.chkGenSiebel.Size = New System.Drawing.Size(92, 17)
        Me.chkGenSiebel.TabIndex = 0
        Me.chkGenSiebel.Text = "Siebel Activity"
        Me.chkGenSiebel.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Location = New System.Drawing.Point(12, 64)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(457, 261)
        Me.txtLog.TabIndex = 2
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTime.Location = New System.Drawing.Point(239, 10)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(79, 20)
        Me.lblTime.TabIndex = 3
        Me.lblTime.Text = "00:00:00"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Q# Agent"
        Me.NotifyIcon1.Visible = True
        '
        'chkBlackList
        '
        Me.chkBlackList.AutoSize = True
        Me.chkBlackList.Location = New System.Drawing.Point(136, 41)
        Me.chkBlackList.Name = "chkBlackList"
        Me.chkBlackList.Size = New System.Drawing.Size(99, 17)
        Me.chkBlackList.TabIndex = 5
        Me.chkBlackList.Text = "Check Blacklist"
        Me.chkBlackList.UseVisualStyleBackColor = True
        '
        'tmBlacklist
        '
        Me.tmBlacklist.Enabled = True
        Me.tmBlacklist.Interval = 1000
        '
        'chkSendAppointmentNotify
        '
        Me.chkSendAppointmentNotify.AutoSize = True
        Me.chkSendAppointmentNotify.Location = New System.Drawing.Point(241, 40)
        Me.chkSendAppointmentNotify.Name = "chkSendAppointmentNotify"
        Me.chkSendAppointmentNotify.Size = New System.Drawing.Size(143, 17)
        Me.chkSendAppointmentNotify.TabIndex = 7
        Me.chkSendAppointmentNotify.Text = "Send Appointment Notify"
        Me.chkSendAppointmentNotify.UseVisualStyleBackColor = True
        '
        'tmSendAppointmentNotify
        '
        Me.tmSendAppointmentNotify.Enabled = True
        Me.tmSendAppointmentNotify.Interval = 60000
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(240, 37)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 8
        Me.btnTest.Text = "Add Data"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'dtpST
        '
        Me.dtpST.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpST.Location = New System.Drawing.Point(14, 11)
        Me.dtpST.Name = "dtpST"
        Me.dtpST.Size = New System.Drawing.Size(107, 20)
        Me.dtpST.TabIndex = 10
        '
        'dtpET
        '
        Me.dtpET.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpET.Location = New System.Drawing.Point(127, 10)
        Me.dtpET.Name = "dtpET"
        Me.dtpET.Size = New System.Drawing.Size(107, 20)
        Me.dtpET.TabIndex = 11
        '
        'cbT
        '
        Me.cbT.AutoSize = True
        Me.cbT.Location = New System.Drawing.Point(127, 303)
        Me.cbT.Name = "cbT"
        Me.cbT.Size = New System.Drawing.Size(33, 17)
        Me.cbT.TabIndex = 12
        Me.cbT.Text = "T"
        Me.cbT.UseVisualStyleBackColor = True
        '
        'cbD
        '
        Me.cbD.AutoSize = True
        Me.cbD.Location = New System.Drawing.Point(166, 303)
        Me.cbD.Name = "cbD"
        Me.cbD.Size = New System.Drawing.Size(34, 17)
        Me.cbD.TabIndex = 13
        Me.cbD.Text = "D"
        Me.cbD.UseVisualStyleBackColor = True
        '
        'cbM
        '
        Me.cbM.AutoSize = True
        Me.cbM.Location = New System.Drawing.Point(249, 303)
        Me.cbM.Name = "cbM"
        Me.cbM.Size = New System.Drawing.Size(35, 17)
        Me.cbM.TabIndex = 14
        Me.cbM.Text = "M"
        Me.cbM.UseVisualStyleBackColor = True
        '
        'cbW
        '
        Me.cbW.AutoSize = True
        Me.cbW.Location = New System.Drawing.Point(206, 303)
        Me.cbW.Name = "cbW"
        Me.cbW.Size = New System.Drawing.Size(37, 17)
        Me.cbW.TabIndex = 15
        Me.cbW.Text = "W"
        Me.cbW.UseVisualStyleBackColor = True
        Me.cbW.Visible = False
        '
        'cbY
        '
        Me.cbY.AutoSize = True
        Me.cbY.Location = New System.Drawing.Point(290, 303)
        Me.cbY.Name = "cbY"
        Me.cbY.Size = New System.Drawing.Size(33, 17)
        Me.cbY.TabIndex = 16
        Me.cbY.Text = "Y"
        Me.cbY.UseVisualStyleBackColor = True
        Me.cbY.Visible = False
        '
        'cbSeg
        '
        Me.cbSeg.AutoSize = True
        Me.cbSeg.Location = New System.Drawing.Point(14, 73)
        Me.cbSeg.Name = "cbSeg"
        Me.cbSeg.Size = New System.Drawing.Size(68, 17)
        Me.cbSeg.TabIndex = 17
        Me.cbSeg.Text = "Segment"
        Me.cbSeg.UseVisualStyleBackColor = True
        '
        'cbNet
        '
        Me.cbNet.AutoSize = True
        Me.cbNet.Location = New System.Drawing.Point(14, 96)
        Me.cbNet.Name = "cbNet"
        Me.cbNet.Size = New System.Drawing.Size(93, 17)
        Me.cbNet.TabIndex = 18
        Me.cbNet.Text = "Network Type"
        Me.cbNet.UseVisualStyleBackColor = True
        '
        'cbWTsh
        '
        Me.cbWTsh.AutoSize = True
        Me.cbWTsh.Location = New System.Drawing.Point(14, 119)
        Me.cbWTsh.Name = "cbWTsh"
        Me.cbWTsh.Size = New System.Drawing.Size(96, 17)
        Me.cbWTsh.TabIndex = 19
        Me.cbWTsh.Text = "WT - HT Shop"
        Me.cbWTsh.UseVisualStyleBackColor = True
        '
        'cbWTsk
        '
        Me.cbWTsk.AutoSize = True
        Me.cbWTsk.Location = New System.Drawing.Point(14, 303)
        Me.cbWTsk.Name = "cbWTsk"
        Me.cbWTsk.Size = New System.Drawing.Size(90, 17)
        Me.cbWTsk.TabIndex = 20
        Me.cbWTsk.Text = "WT - HT Skill"
        Me.cbWTsk.UseVisualStyleBackColor = True
        Me.cbWTsk.Visible = False
        '
        'cbWTst
        '
        Me.cbWTst.AutoSize = True
        Me.cbWTst.Location = New System.Drawing.Point(14, 142)
        Me.cbWTst.Name = "cbWTst"
        Me.cbWTst.Size = New System.Drawing.Size(93, 17)
        Me.cbWTst.TabIndex = 21
        Me.cbWTst.Text = "WT - HT Staff"
        Me.cbWTst.UseVisualStyleBackColor = True
        '
        'cbKPIsh
        '
        Me.cbKPIsh.AutoSize = True
        Me.cbKPIsh.Location = New System.Drawing.Point(14, 165)
        Me.cbKPIsh.Name = "cbKPIsh"
        Me.cbKPIsh.Size = New System.Drawing.Size(71, 17)
        Me.cbKPIsh.TabIndex = 22
        Me.cbKPIsh.Text = "KPI Shop"
        Me.cbKPIsh.UseVisualStyleBackColor = True
        '
        'cbKPIst
        '
        Me.cbKPIst.AutoSize = True
        Me.cbKPIst.Location = New System.Drawing.Point(14, 188)
        Me.cbKPIst.Name = "cbKPIst"
        Me.cbKPIst.Size = New System.Drawing.Size(68, 17)
        Me.cbKPIst.TabIndex = 23
        Me.cbKPIst.Text = "KPI Staff"
        Me.cbKPIst.UseVisualStyleBackColor = True
        '
        'cbPro
        '
        Me.cbPro.AutoSize = True
        Me.cbPro.Location = New System.Drawing.Point(14, 211)
        Me.cbPro.Name = "cbPro"
        Me.cbPro.Size = New System.Drawing.Size(81, 17)
        Me.cbPro.TabIndex = 24
        Me.cbPro.Text = "Productivity"
        Me.cbPro.UseVisualStyleBackColor = True
        '
        'cbCa
        '
        Me.cbCa.AutoSize = True
        Me.cbCa.Location = New System.Drawing.Point(15, 308)
        Me.cbCa.Name = "cbCa"
        Me.cbCa.Size = New System.Drawing.Size(67, 17)
        Me.cbCa.TabIndex = 25
        Me.cbCa.Text = "Capacity"
        Me.cbCa.UseVisualStyleBackColor = True
        Me.cbCa.Visible = False
        '
        'cbSt
        '
        Me.cbSt.AutoSize = True
        Me.cbSt.Location = New System.Drawing.Point(14, 234)
        Me.cbSt.Name = "cbSt"
        Me.cbSt.Size = New System.Drawing.Size(106, 17)
        Me.cbSt.TabIndex = 26
        Me.cbSt.Text = "Staff Attendance"
        Me.cbSt.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(127, 68)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(192, 197)
        Me.TextBox1.TabIndex = 27
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightGreen
        Me.Panel1.Controls.Add(Me.cmbShop)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.dtpST)
        Me.Panel1.Controls.Add(Me.btnTest)
        Me.Panel1.Controls.Add(Me.dtpET)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.cbT)
        Me.Panel1.Controls.Add(Me.cbSt)
        Me.Panel1.Controls.Add(Me.lblTime)
        Me.Panel1.Controls.Add(Me.cbD)
        Me.Panel1.Controls.Add(Me.cbCa)
        Me.Panel1.Controls.Add(Me.cbM)
        Me.Panel1.Controls.Add(Me.cbPro)
        Me.Panel1.Controls.Add(Me.cbW)
        Me.Panel1.Controls.Add(Me.cbKPIst)
        Me.Panel1.Controls.Add(Me.cbY)
        Me.Panel1.Controls.Add(Me.cbKPIsh)
        Me.Panel1.Controls.Add(Me.cbSeg)
        Me.Panel1.Controls.Add(Me.cbWTst)
        Me.Panel1.Controls.Add(Me.cbNet)
        Me.Panel1.Controls.Add(Me.cbWTsk)
        Me.Panel1.Controls.Add(Me.cbWTsh)
        Me.Panel1.Location = New System.Drawing.Point(478, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(328, 328)
        Me.Panel1.TabIndex = 30
        '
        'cmbShop
        '
        Me.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShop.FormattingEnabled = True
        Me.cmbShop.Location = New System.Drawing.Point(14, 39)
        Me.cmbShop.Name = "cmbShop"
        Me.cmbShop.Size = New System.Drawing.Size(220, 21)
        Me.cmbShop.TabIndex = 30
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(14, 271)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "Add WF"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(127, 271)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(196, 23)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "Clear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tmCSISurvey
        '
        Me.tmCSISurvey.Enabled = True
        Me.tmCSISurvey.Interval = 5000
        '
        'tmWorkForce
        '
        Me.tmWorkForce.Enabled = True
        Me.tmWorkForce.Interval = 5000
        '
        'tmArchMaster
        '
        Me.tmArchMaster.Enabled = True
        Me.tmArchMaster.Interval = 3600000
        '
        'tmCutOffData
        '
        Me.tmCutOffData.Enabled = True
        Me.tmCutOffData.Interval = 1000
        '
        'tmClearCustomerInfo
        '
        Me.tmClearCustomerInfo.Enabled = True
        Me.tmClearCustomerInfo.Interval = 3600000
        '
        'chkWorkforce
        '
        Me.chkWorkforce.AutoSize = True
        Me.chkWorkforce.Location = New System.Drawing.Point(390, 40)
        Me.chkWorkforce.Name = "chkWorkforce"
        Me.chkWorkforce.Size = New System.Drawing.Size(82, 17)
        Me.chkWorkforce.TabIndex = 35
        Me.chkWorkforce.Text = "Work Force"
        Me.chkWorkforce.UseVisualStyleBackColor = True
        '
        'chkCSI
        '
        Me.chkCSI.AutoSize = True
        Me.chkCSI.Location = New System.Drawing.Point(390, 11)
        Me.chkCSI.Name = "chkCSI"
        Me.chkCSI.Size = New System.Drawing.Size(79, 17)
        Me.chkCSI.TabIndex = 34
        Me.chkCSI.Text = "CSI Survey"
        Me.chkCSI.UseVisualStyleBackColor = True
        '
        'chkArchMaster
        '
        Me.chkArchMaster.AutoSize = True
        Me.chkArchMaster.Location = New System.Drawing.Point(241, 11)
        Me.chkArchMaster.Name = "chkArchMaster"
        Me.chkArchMaster.Size = New System.Drawing.Size(123, 17)
        Me.chkArchMaster.TabIndex = 33
        Me.chkArchMaster.Text = "Archive Master Data"
        Me.chkArchMaster.UseVisualStyleBackColor = True
        '
        'chkCutOffData
        '
        Me.chkCutOffData.AutoSize = True
        Me.chkCutOffData.Location = New System.Drawing.Point(136, 12)
        Me.chkCutOffData.Name = "chkCutOffData"
        Me.chkCutOffData.Size = New System.Drawing.Size(83, 17)
        Me.chkCutOffData.TabIndex = 32
        Me.chkCutOffData.Text = "Cut off Data"
        Me.chkCutOffData.UseVisualStyleBackColor = True
        '
        'chkClearCustInfo
        '
        Me.chkClearCustInfo.AutoSize = True
        Me.chkClearCustInfo.Location = New System.Drawing.Point(12, 41)
        Me.chkClearCustInfo.Name = "chkClearCustInfo"
        Me.chkClearCustInfo.Size = New System.Drawing.Size(118, 17)
        Me.chkClearCustInfo.TabIndex = 31
        Me.chkClearCustInfo.Text = "Clear Customer Info"
        Me.chkClearCustInfo.UseVisualStyleBackColor = True
        '
        'frmHQAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 337)
        Me.Controls.Add(Me.chkWorkforce)
        Me.Controls.Add(Me.chkCSI)
        Me.Controls.Add(Me.chkArchMaster)
        Me.Controls.Add(Me.chkCutOffData)
        Me.Controls.Add(Me.chkClearCustInfo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkSendAppointmentNotify)
        Me.Controls.Add(Me.chkBlackList)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.chkGenSiebel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHQAgent"
        Me.Text = "HQ Agent V0.0.4.13"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmGenSiebel As System.Windows.Forms.Timer
    Friend WithEvents chkGenSiebel As System.Windows.Forms.CheckBox
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents chkBlackList As System.Windows.Forms.CheckBox
    Friend WithEvents tmBlacklist As System.Windows.Forms.Timer
    Friend WithEvents chkSendAppointmentNotify As System.Windows.Forms.CheckBox
    Friend WithEvents tmSendAppointmentNotify As System.Windows.Forms.Timer
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents dtpST As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpET As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbT As System.Windows.Forms.CheckBox
    Friend WithEvents cbD As System.Windows.Forms.CheckBox
    Friend WithEvents cbM As System.Windows.Forms.CheckBox
    Friend WithEvents cbW As System.Windows.Forms.CheckBox
    Friend WithEvents cbY As System.Windows.Forms.CheckBox
    Friend WithEvents cbSeg As System.Windows.Forms.CheckBox
    Friend WithEvents cbNet As System.Windows.Forms.CheckBox
    Friend WithEvents cbWTsh As System.Windows.Forms.CheckBox
    Friend WithEvents cbWTsk As System.Windows.Forms.CheckBox
    Friend WithEvents cbWTst As System.Windows.Forms.CheckBox
    Friend WithEvents cbKPIsh As System.Windows.Forms.CheckBox
    Friend WithEvents cbKPIst As System.Windows.Forms.CheckBox
    Friend WithEvents cbPro As System.Windows.Forms.CheckBox
    Friend WithEvents cbCa As System.Windows.Forms.CheckBox
    Friend WithEvents cbSt As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cmbShop As System.Windows.Forms.ComboBox
    Friend WithEvents tmCSISurvey As System.Windows.Forms.Timer
    Friend WithEvents tmWorkForce As System.Windows.Forms.Timer
    Friend WithEvents tmArchMaster As System.Windows.Forms.Timer
    Friend WithEvents tmCutOffData As System.Windows.Forms.Timer
    Friend WithEvents tmClearCustomerInfo As System.Windows.Forms.Timer
    Friend WithEvents chkWorkforce As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSI As System.Windows.Forms.CheckBox
    Friend WithEvents chkArchMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chkCutOffData As System.Windows.Forms.CheckBox
    Friend WithEvents chkClearCustInfo As System.Windows.Forms.CheckBox
End Class
