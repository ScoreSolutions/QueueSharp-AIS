<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAisLoadCubeReportAgent
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAisLoadCubeReportAgent))
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.lblTime = New System.Windows.Forms.Label
        Me.tmCutOffData = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.btnTest = New System.Windows.Forms.Button
        Me.dtpST = New System.Windows.Forms.DateTimePicker
        Me.dtpET = New System.Windows.Forms.DateTimePicker
        Me.cbT = New System.Windows.Forms.CheckBox
        Me.cbD = New System.Windows.Forms.CheckBox
        Me.cbM = New System.Windows.Forms.CheckBox
        Me.cbW = New System.Windows.Forms.CheckBox
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbSumStaff = New System.Windows.Forms.CheckBox
        Me.cbIntervalPerformance = New System.Windows.Forms.CheckBox
        Me.cbSumDairy = New System.Windows.Forms.CheckBox
        Me.cbCat = New System.Windows.Forms.CheckBox
        Me.cbAppointShop = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.tmTime = New System.Windows.Forms.Timer(Me.components)
        Me.lstShop = New System.Windows.Forms.ListBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Location = New System.Drawing.Point(4, 4)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(538, 299)
        Me.txtLog.TabIndex = 2
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTime.Location = New System.Drawing.Point(4, 11)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(79, 20)
        Me.lblTime.TabIndex = 3
        Me.lblTime.Text = "00:00:00"
        '
        'tmCutOffData
        '
        Me.tmCutOffData.Interval = 60000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "AIS Cut Off Data Agent"
        Me.NotifyIcon1.Visible = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(548, 308)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 8
        Me.btnTest.Text = "Add Data"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'dtpST
        '
        Me.dtpST.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpST.Location = New System.Drawing.Point(548, 14)
        Me.dtpST.Name = "dtpST"
        Me.dtpST.Size = New System.Drawing.Size(107, 20)
        Me.dtpST.TabIndex = 10
        '
        'dtpET
        '
        Me.dtpET.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpET.Location = New System.Drawing.Point(661, 14)
        Me.dtpET.Name = "dtpET"
        Me.dtpET.Size = New System.Drawing.Size(107, 20)
        Me.dtpET.TabIndex = 11
        '
        'cbT
        '
        Me.cbT.AutoSize = True
        Me.cbT.Location = New System.Drawing.Point(16, 305)
        Me.cbT.Name = "cbT"
        Me.cbT.Size = New System.Drawing.Size(33, 17)
        Me.cbT.TabIndex = 12
        Me.cbT.Text = "T"
        Me.cbT.UseVisualStyleBackColor = True
        '
        'cbD
        '
        Me.cbD.AutoSize = True
        Me.cbD.Location = New System.Drawing.Point(55, 305)
        Me.cbD.Name = "cbD"
        Me.cbD.Size = New System.Drawing.Size(34, 17)
        Me.cbD.TabIndex = 13
        Me.cbD.Text = "D"
        Me.cbD.UseVisualStyleBackColor = True
        '
        'cbM
        '
        Me.cbM.AutoSize = True
        Me.cbM.Location = New System.Drawing.Point(138, 305)
        Me.cbM.Name = "cbM"
        Me.cbM.Size = New System.Drawing.Size(35, 17)
        Me.cbM.TabIndex = 14
        Me.cbM.Text = "M"
        Me.cbM.UseVisualStyleBackColor = True
        '
        'cbW
        '
        Me.cbW.AutoSize = True
        Me.cbW.Location = New System.Drawing.Point(95, 305)
        Me.cbW.Name = "cbW"
        Me.cbW.Size = New System.Drawing.Size(37, 17)
        Me.cbW.TabIndex = 15
        Me.cbW.Text = "W"
        Me.cbW.UseVisualStyleBackColor = True
        '
        'cbSeg
        '
        Me.cbSeg.AutoSize = True
        Me.cbSeg.Location = New System.Drawing.Point(8, 73)
        Me.cbSeg.Name = "cbSeg"
        Me.cbSeg.Size = New System.Drawing.Size(68, 17)
        Me.cbSeg.TabIndex = 17
        Me.cbSeg.Text = "Segment"
        Me.cbSeg.UseVisualStyleBackColor = True
        '
        'cbNet
        '
        Me.cbNet.AutoSize = True
        Me.cbNet.Location = New System.Drawing.Point(8, 96)
        Me.cbNet.Name = "cbNet"
        Me.cbNet.Size = New System.Drawing.Size(93, 17)
        Me.cbNet.TabIndex = 18
        Me.cbNet.Text = "Network Type"
        Me.cbNet.UseVisualStyleBackColor = True
        '
        'cbWTsh
        '
        Me.cbWTsh.AutoSize = True
        Me.cbWTsh.Location = New System.Drawing.Point(8, 120)
        Me.cbWTsh.Name = "cbWTsh"
        Me.cbWTsh.Size = New System.Drawing.Size(96, 17)
        Me.cbWTsh.TabIndex = 19
        Me.cbWTsh.Text = "WT - HT Shop"
        Me.cbWTsh.UseVisualStyleBackColor = True
        '
        'cbWTsk
        '
        Me.cbWTsk.AutoSize = True
        Me.cbWTsk.Location = New System.Drawing.Point(137, 143)
        Me.cbWTsk.Name = "cbWTsk"
        Me.cbWTsk.Size = New System.Drawing.Size(90, 17)
        Me.cbWTsk.TabIndex = 20
        Me.cbWTsk.Text = "WT - HT Skill"
        Me.cbWTsk.UseVisualStyleBackColor = True
        '
        'cbWTst
        '
        Me.cbWTst.AutoSize = True
        Me.cbWTst.Location = New System.Drawing.Point(8, 143)
        Me.cbWTst.Name = "cbWTst"
        Me.cbWTst.Size = New System.Drawing.Size(93, 17)
        Me.cbWTst.TabIndex = 21
        Me.cbWTst.Text = "WT - HT Staff"
        Me.cbWTst.UseVisualStyleBackColor = True
        '
        'cbKPIsh
        '
        Me.cbKPIsh.AutoSize = True
        Me.cbKPIsh.Location = New System.Drawing.Point(8, 167)
        Me.cbKPIsh.Name = "cbKPIsh"
        Me.cbKPIsh.Size = New System.Drawing.Size(71, 17)
        Me.cbKPIsh.TabIndex = 22
        Me.cbKPIsh.Text = "KPI Shop"
        Me.cbKPIsh.UseVisualStyleBackColor = True
        '
        'cbKPIst
        '
        Me.cbKPIst.AutoSize = True
        Me.cbKPIst.Location = New System.Drawing.Point(8, 190)
        Me.cbKPIst.Name = "cbKPIst"
        Me.cbKPIst.Size = New System.Drawing.Size(68, 17)
        Me.cbKPIst.TabIndex = 23
        Me.cbKPIst.Text = "KPI Staff"
        Me.cbKPIst.UseVisualStyleBackColor = True
        '
        'cbPro
        '
        Me.cbPro.AutoSize = True
        Me.cbPro.Location = New System.Drawing.Point(8, 214)
        Me.cbPro.Name = "cbPro"
        Me.cbPro.Size = New System.Drawing.Size(81, 17)
        Me.cbPro.TabIndex = 24
        Me.cbPro.Text = "Productivity"
        Me.cbPro.UseVisualStyleBackColor = True
        '
        'cbCa
        '
        Me.cbCa.AutoSize = True
        Me.cbCa.Location = New System.Drawing.Point(137, 96)
        Me.cbCa.Name = "cbCa"
        Me.cbCa.Size = New System.Drawing.Size(67, 17)
        Me.cbCa.TabIndex = 25
        Me.cbCa.Text = "Capacity"
        Me.cbCa.UseVisualStyleBackColor = True
        '
        'cbSt
        '
        Me.cbSt.AutoSize = True
        Me.cbSt.Location = New System.Drawing.Point(8, 237)
        Me.cbSt.Name = "cbSt"
        Me.cbSt.Size = New System.Drawing.Size(106, 17)
        Me.cbSt.TabIndex = 26
        Me.cbSt.Text = "Staff Attendance"
        Me.cbSt.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightGreen
        Me.Panel1.Controls.Add(Me.cbSumStaff)
        Me.Panel1.Controls.Add(Me.cbIntervalPerformance)
        Me.Panel1.Controls.Add(Me.cbSumDairy)
        Me.Panel1.Controls.Add(Me.cbCat)
        Me.Panel1.Controls.Add(Me.cbAppointShop)
        Me.Panel1.Controls.Add(Me.cbT)
        Me.Panel1.Controls.Add(Me.cbSt)
        Me.Panel1.Controls.Add(Me.lblTime)
        Me.Panel1.Controls.Add(Me.cbD)
        Me.Panel1.Controls.Add(Me.cbCa)
        Me.Panel1.Controls.Add(Me.cbM)
        Me.Panel1.Controls.Add(Me.cbPro)
        Me.Panel1.Controls.Add(Me.cbW)
        Me.Panel1.Controls.Add(Me.cbKPIst)
        Me.Panel1.Controls.Add(Me.cbKPIsh)
        Me.Panel1.Controls.Add(Me.cbSeg)
        Me.Panel1.Controls.Add(Me.cbWTst)
        Me.Panel1.Controls.Add(Me.cbNet)
        Me.Panel1.Controls.Add(Me.cbWTsk)
        Me.Panel1.Controls.Add(Me.cbWTsh)
        Me.Panel1.Location = New System.Drawing.Point(776, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(264, 328)
        Me.Panel1.TabIndex = 30
        '
        'cbSumStaff
        '
        Me.cbSumStaff.AutoSize = True
        Me.cbSumStaff.Location = New System.Drawing.Point(137, 236)
        Me.cbSumStaff.Name = "cbSumStaff"
        Me.cbSumStaff.Size = New System.Drawing.Size(94, 17)
        Me.cbSumStaff.TabIndex = 36
        Me.cbSumStaff.Text = "Summary Staff"
        Me.cbSumStaff.UseVisualStyleBackColor = True
        Me.cbSumStaff.Visible = False
        '
        'cbIntervalPerformance
        '
        Me.cbIntervalPerformance.AutoSize = True
        Me.cbIntervalPerformance.Location = New System.Drawing.Point(137, 167)
        Me.cbIntervalPerformance.Name = "cbIntervalPerformance"
        Me.cbIntervalPerformance.Size = New System.Drawing.Size(124, 17)
        Me.cbIntervalPerformance.TabIndex = 35
        Me.cbIntervalPerformance.Text = "Interval Performance"
        Me.cbIntervalPerformance.UseVisualStyleBackColor = True
        '
        'cbSumDairy
        '
        Me.cbSumDairy.AutoSize = True
        Me.cbSumDairy.Location = New System.Drawing.Point(137, 213)
        Me.cbSumDairy.Name = "cbSumDairy"
        Me.cbSumDairy.Size = New System.Drawing.Size(95, 17)
        Me.cbSumDairy.TabIndex = 34
        Me.cbSumDairy.Text = "Summary Daily"
        Me.cbSumDairy.UseVisualStyleBackColor = True
        Me.cbSumDairy.Visible = False
        '
        'cbCat
        '
        Me.cbCat.AutoSize = True
        Me.cbCat.Location = New System.Drawing.Point(137, 120)
        Me.cbCat.Name = "cbCat"
        Me.cbCat.Size = New System.Drawing.Size(85, 17)
        Me.cbCat.TabIndex = 33
        Me.cbCat.Text = "Cat/Sub Cat"
        Me.cbCat.UseVisualStyleBackColor = True
        '
        'cbAppointShop
        '
        Me.cbAppointShop.AutoSize = True
        Me.cbAppointShop.Location = New System.Drawing.Point(137, 73)
        Me.cbAppointShop.Name = "cbAppointShop"
        Me.cbAppointShop.Size = New System.Drawing.Size(113, 17)
        Me.cbAppointShop.TabIndex = 31
        Me.cbAppointShop.Text = "Appointment Shop"
        Me.cbAppointShop.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(4, 309)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(196, 23)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "Clear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tmTime
        '
        Me.tmTime.Enabled = True
        Me.tmTime.Interval = 1000
        '
        'lstShop
        '
        Me.lstShop.FormattingEnabled = True
        Me.lstShop.Location = New System.Drawing.Point(548, 40)
        Me.lstShop.Name = "lstShop"
        Me.lstShop.ScrollAlwaysVisible = True
        Me.lstShop.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstShop.Size = New System.Drawing.Size(222, 264)
        Me.lstShop.TabIndex = 32
        '
        'frmAisLoadCubeReportAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 337)
        Me.Controls.Add(Me.lstShop)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dtpST)
        Me.Controls.Add(Me.dtpET)
        Me.Controls.Add(Me.btnTest)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAisLoadCubeReportAgent"
        Me.Text = "AIS Cut Off Data Agent V0.0.0.1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents dtpST As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpET As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbT As System.Windows.Forms.CheckBox
    Friend WithEvents cbD As System.Windows.Forms.CheckBox
    Friend WithEvents cbM As System.Windows.Forms.CheckBox
    Friend WithEvents cbW As System.Windows.Forms.CheckBox
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbAppointShop As System.Windows.Forms.CheckBox
    Friend WithEvents cbCat As System.Windows.Forms.CheckBox
    Friend WithEvents tmCutOffData As System.Windows.Forms.Timer
    Friend WithEvents tmTime As System.Windows.Forms.Timer
    Friend WithEvents cbSumDairy As System.Windows.Forms.CheckBox
    Friend WithEvents cbIntervalPerformance As System.Windows.Forms.CheckBox
    Friend WithEvents cbSumStaff As System.Windows.Forms.CheckBox
    Friend WithEvents lstShop As System.Windows.Forms.ListBox
End Class
