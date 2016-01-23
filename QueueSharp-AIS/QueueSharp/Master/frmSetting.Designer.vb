<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetting))
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.Label13 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.nud_k_appointment = New System.Windows.Forms.NumericUpDown
        Me.txtMobile4 = New System.Windows.Forms.TextBox
        Me.txtMobile3 = New System.Windows.Forms.TextBox
        Me.txtMobile2 = New System.Windows.Forms.TextBox
        Me.txtMobile1 = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.nud_k_len = New System.Windows.Forms.NumericUpDown
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.nud_k_Cancel = New System.Windows.Forms.NumericUpDown
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.nud_k_vdo = New System.Windows.Forms.NumericUpDown
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.nud_k_refresh = New System.Windows.Forms.NumericUpDown
        Me.nud_k_before_app = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.nud_k_serve = New System.Windows.Forms.NumericUpDown
        Me.nud_k_late = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.nud_k_before = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.nud_k_disable = New System.Windows.Forms.NumericUpDown
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBoxButton.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.nud_k_appointment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_len, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_Cancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_vdo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_refresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_before_app, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_serve, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_late, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_before, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_disable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBoxButton
        '
        Me.GroupBoxButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxButton.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxButton.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBoxButton.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxButton.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxButton.BorderThickness = 1.0!
        Me.GroupBoxButton.Controls.Add(Me.Label13)
        Me.GroupBoxButton.Controls.Add(Me.Panel1)
        Me.GroupBoxButton.Controls.Add(Me.btnSave)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, -2)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(1, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(20)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(452, 440)
        Me.GroupBoxButton.TabIndex = 24
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(432, 24)
        Me.Label13.TabIndex = 68
        Me.Label13.Text = "Kiosk"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.nud_k_appointment)
        Me.Panel1.Controls.Add(Me.txtMobile4)
        Me.Panel1.Controls.Add(Me.txtMobile3)
        Me.Panel1.Controls.Add(Me.txtMobile2)
        Me.Panel1.Controls.Add(Me.txtMobile1)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.nud_k_len)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.nud_k_Cancel)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.nud_k_vdo)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.nud_k_refresh)
        Me.Panel1.Controls.Add(Me.nud_k_before_app)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.nud_k_serve)
        Me.Panel1.Controls.Add(Me.nud_k_late)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.nud_k_before)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.nud_k_disable)
        Me.Panel1.Location = New System.Drawing.Point(8, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(432, 332)
        Me.Panel1.TabIndex = 65
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(331, 68)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(57, 18)
        Me.Label31.TabIndex = 98
        Me.Label31.Text = "Service"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(8, 68)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(253, 18)
        Me.Label32.TabIndex = 97
        Me.Label32.Text = "Max Services Appointment per Time :"
        '
        'nud_k_appointment
        '
        Me.nud_k_appointment.Location = New System.Drawing.Point(263, 66)
        Me.nud_k_appointment.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nud_k_appointment.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_appointment.Name = "nud_k_appointment"
        Me.nud_k_appointment.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_appointment.TabIndex = 96
        Me.nud_k_appointment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_appointment.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'txtMobile4
        '
        Me.txtMobile4.BackColor = System.Drawing.Color.White
        Me.txtMobile4.Location = New System.Drawing.Point(369, 296)
        Me.txtMobile4.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMobile4.MaxLength = 2
        Me.txtMobile4.Name = "txtMobile4"
        Me.txtMobile4.Size = New System.Drawing.Size(30, 24)
        Me.txtMobile4.TabIndex = 94
        Me.txtMobile4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMobile3
        '
        Me.txtMobile3.BackColor = System.Drawing.Color.White
        Me.txtMobile3.Location = New System.Drawing.Point(334, 296)
        Me.txtMobile3.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMobile3.MaxLength = 2
        Me.txtMobile3.Name = "txtMobile3"
        Me.txtMobile3.Size = New System.Drawing.Size(30, 24)
        Me.txtMobile3.TabIndex = 95
        Me.txtMobile3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMobile2
        '
        Me.txtMobile2.BackColor = System.Drawing.Color.White
        Me.txtMobile2.Location = New System.Drawing.Point(299, 296)
        Me.txtMobile2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMobile2.MaxLength = 2
        Me.txtMobile2.Name = "txtMobile2"
        Me.txtMobile2.Size = New System.Drawing.Size(30, 24)
        Me.txtMobile2.TabIndex = 94
        Me.txtMobile2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMobile1
        '
        Me.txtMobile1.BackColor = System.Drawing.Color.White
        Me.txtMobile1.Location = New System.Drawing.Point(264, 296)
        Me.txtMobile1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMobile1.MaxLength = 2
        Me.txtMobile1.Name = "txtMobile1"
        Me.txtMobile1.Size = New System.Drawing.Size(30, 24)
        Me.txtMobile1.TabIndex = 93
        Me.txtMobile1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(86, 299)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(167, 18)
        Me.Label30.TabIndex = 92
        Me.Label30.Text = "Allowable Mobile Prefix :"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(330, 271)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(37, 18)
        Me.Label28.TabIndex = 91
        Me.Label28.Text = "Digit"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(104, 271)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(153, 18)
        Me.Label29.TabIndex = 90
        Me.Label29.Text = "Length of Mobile No. :"
        '
        'nud_k_len
        '
        Me.nud_k_len.Location = New System.Drawing.Point(263, 269)
        Me.nud_k_len.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nud_k_len.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_len.Name = "nud_k_len"
        Me.nud_k_len.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_len.TabIndex = 89
        Me.nud_k_len.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_len.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(330, 243)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(52, 18)
        Me.Label19.TabIndex = 86
        Me.Label19.Text = "Minute"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(330, 125)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(52, 18)
        Me.Label27.TabIndex = 85
        Me.Label27.Text = "Minute"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(330, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 18)
        Me.Label11.TabIndex = 84
        Me.Label11.Text = "Minute"
        '
        'nud_k_Cancel
        '
        Me.nud_k_Cancel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.nud_k_Cancel.Increment = New Decimal(New Integer() {15, 0, 0, 0})
        Me.nud_k_Cancel.Location = New System.Drawing.Point(263, 123)
        Me.nud_k_Cancel.Maximum = New Decimal(New Integer() {900, 0, 0, 0})
        Me.nud_k_Cancel.Name = "nud_k_Cancel"
        Me.nud_k_Cancel.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_Cancel.TabIndex = 81
        Me.nud_k_Cancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_Cancel.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(25, 125)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(228, 18)
        Me.Label26.TabIndex = 82
        Me.Label26.Text = "Cancel before Reservation Time :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(128, 243)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(129, 18)
        Me.Label20.TabIndex = 73
        Me.Label20.Text = "Show VDO every :"
        '
        'nud_k_vdo
        '
        Me.nud_k_vdo.Location = New System.Drawing.Point(263, 241)
        Me.nud_k_vdo.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.nud_k_vdo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_vdo.Name = "nud_k_vdo"
        Me.nud_k_vdo.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_vdo.TabIndex = 72
        Me.nud_k_vdo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_vdo.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(331, 215)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(59, 18)
        Me.Label14.TabIndex = 71
        Me.Label14.Text = "Second"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(118, 215)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(139, 18)
        Me.Label15.TabIndex = 70
        Me.Label15.Text = "Refresh W.T every :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 18)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Soonest Arrival for Appointment :"
        '
        'nud_k_refresh
        '
        Me.nud_k_refresh.Location = New System.Drawing.Point(263, 213)
        Me.nud_k_refresh.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nud_k_refresh.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_refresh.Name = "nud_k_refresh"
        Me.nud_k_refresh.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_refresh.TabIndex = 69
        Me.nud_k_refresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_refresh.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nud_k_before_app
        '
        Me.nud_k_before_app.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.nud_k_before_app.Increment = New Decimal(New Integer() {15, 0, 0, 0})
        Me.nud_k_before_app.Location = New System.Drawing.Point(263, 94)
        Me.nud_k_before_app.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nud_k_before_app.Name = "nud_k_before_app"
        Me.nud_k_before_app.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_before_app.TabIndex = 4
        Me.nud_k_before_app.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_before_app.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(114, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 18)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Pre-booking Period :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(208, 18)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Latest Arrival for Appointment :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(331, 185)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 18)
        Me.Label8.TabIndex = 61
        Me.Label8.Text = "Service"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(331, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 18)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Minute"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 185)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(252, 18)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "Maximun Services chosen per Time :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(331, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 18)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Minute"
        '
        'nud_k_serve
        '
        Me.nud_k_serve.Location = New System.Drawing.Point(263, 183)
        Me.nud_k_serve.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nud_k_serve.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_serve.Name = "nud_k_serve"
        Me.nud_k_serve.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_serve.TabIndex = 59
        Me.nud_k_serve.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_serve.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'nud_k_late
        '
        Me.nud_k_late.Location = New System.Drawing.Point(263, 37)
        Me.nud_k_late.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nud_k_late.Name = "nud_k_late"
        Me.nud_k_late.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_late.TabIndex = 1
        Me.nud_k_late.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(331, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 18)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Minute"
        '
        'nud_k_before
        '
        Me.nud_k_before.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.nud_k_before.Location = New System.Drawing.Point(263, 7)
        Me.nud_k_before.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nud_k_before.Name = "nud_k_before"
        Me.nud_k_before.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_before.TabIndex = 0
        Me.nud_k_before.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_before.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(45, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(199, 18)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "No Print if W.T is more than :"
        '
        'nud_k_disable
        '
        Me.nud_k_disable.Location = New System.Drawing.Point(263, 153)
        Me.nud_k_disable.Maximum = New Decimal(New Integer() {240, 0, 0, 0})
        Me.nud_k_disable.Name = "nud_k_disable"
        Me.nud_k_disable.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_disable.TabIndex = 56
        Me.nud_k_disable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_disable.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(340, 385)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 45)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "   Save"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(462, 442)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetting"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Setting"
        Me.GroupBoxButton.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nud_k_appointment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_len, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_Cancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_vdo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_refresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_before_app, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_serve, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_late, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_before, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_disable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents nud_k_late As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nud_k_before As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nud_k_before_app As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nud_k_disable As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nud_k_serve As System.Windows.Forms.NumericUpDown
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents nud_k_refresh As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents nud_k_vdo As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents nud_k_Cancel As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents nud_k_len As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtMobile1 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtMobile4 As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile2 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents nud_k_appointment As System.Windows.Forms.NumericUpDown
End Class
