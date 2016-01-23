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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAppointment))
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nudGap = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblShopOpen = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbCloseShop = New System.Windows.Forms.ComboBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.nud_k_no_show_within = New System.Windows.Forms.NumericUpDown
        Me.Label39 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.nud_k_no_show = New System.Windows.Forms.NumericUpDown
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.nud_k_not_book = New System.Windows.Forms.NumericUpDown
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblDayOfWeek = New System.Windows.Forms.Label
        Me.btnView = New System.Windows.Forms.Button
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblRecur = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.mc = New Calendar.Calendar
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.p_service = New System.Windows.Forms.Panel
        Me.FLP_Item = New System.Windows.Forms.FlowLayoutPanel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.p_capa = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnGen = New System.Windows.Forms.Button
        Me.cbSlot = New System.Windows.Forms.ComboBox
        Me.nudCounter = New System.Windows.Forms.NumericUpDown
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.CBend_time_h = New System.Windows.Forms.ComboBox
        Me.CBend_time_m = New System.Windows.Forms.ComboBox
        Me.CBstart_time_m = New System.Windows.Forms.ComboBox
        Me.CBstart_time_h = New System.Windows.Forms.ComboBox
        Me.p_week = New System.Windows.Forms.Panel
        Me.cbSun = New System.Windows.Forms.CheckBox
        Me.cbSat = New System.Windows.Forms.CheckBox
        Me.cbFri = New System.Windows.Forms.CheckBox
        Me.cbThu = New System.Windows.Forms.CheckBox
        Me.cbWed = New System.Windows.Forms.CheckBox
        Me.cbTue = New System.Windows.Forms.CheckBox
        Me.cbMon = New System.Windows.Forms.CheckBox
        Me.p_slot = New System.Windows.Forms.Panel
        Me.flpTime = New System.Windows.Forms.FlowLayoutPanel
        Me.p_date = New System.Windows.Forms.Panel
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.ColorPicker = New System.Windows.Forms.ColorDialog
        Me.GroupBoxButton.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.nudGap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.nud_k_no_show_within, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_no_show, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_k_not_book, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.p_service.SuspendLayout()
        Me.p_capa.SuspendLayout()
        CType(Me.nudCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.p_week.SuspendLayout()
        Me.p_slot.SuspendLayout()
        Me.p_date.SuspendLayout()
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
        Me.GroupBoxButton.Controls.Add(Me.Panel2)
        Me.GroupBoxButton.Controls.Add(Me.lblShopOpen)
        Me.GroupBoxButton.Controls.Add(Me.Label12)
        Me.GroupBoxButton.Controls.Add(Me.Label11)
        Me.GroupBoxButton.Controls.Add(Me.Panel1)
        Me.GroupBoxButton.Controls.Add(Me.Label17)
        Me.GroupBoxButton.Controls.Add(Me.lblDayOfWeek)
        Me.GroupBoxButton.Controls.Add(Me.btnView)
        Me.GroupBoxButton.Controls.Add(Me.Label18)
        Me.GroupBoxButton.Controls.Add(Me.Label6)
        Me.GroupBoxButton.Controls.Add(Me.lblRecur)
        Me.GroupBoxButton.Controls.Add(Me.Label7)
        Me.GroupBoxButton.Controls.Add(Me.mc)
        Me.GroupBoxButton.Controls.Add(Me.Label4)
        Me.GroupBoxButton.Controls.Add(Me.btnSave)
        Me.GroupBoxButton.Controls.Add(Me.p_service)
        Me.GroupBoxButton.Controls.Add(Me.Label9)
        Me.GroupBoxButton.Controls.Add(Me.Label5)
        Me.GroupBoxButton.Controls.Add(Me.p_capa)
        Me.GroupBoxButton.Controls.Add(Me.p_week)
        Me.GroupBoxButton.Controls.Add(Me.p_slot)
        Me.GroupBoxButton.Controls.Add(Me.p_date)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, -7)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(1, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(20)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(984, 600)
        Me.GroupBoxButton.TabIndex = 24
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.nudGap)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(966, 19)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(10, 26)
        Me.Panel2.TabIndex = 162
        Me.Panel2.Visible = False
        '
        'nudGap
        '
        Me.nudGap.Location = New System.Drawing.Point(131, 7)
        Me.nudGap.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.nudGap.Name = "nudGap"
        Me.nudGap.Size = New System.Drawing.Size(56, 24)
        Me.nudGap.TabIndex = 65
        Me.nudGap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudGap.Value = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nudGap.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 18)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Gap Time (min.) :"
        Me.Label1.Visible = False
        '
        'lblShopOpen
        '
        Me.lblShopOpen.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblShopOpen.AutoSize = True
        Me.lblShopOpen.Location = New System.Drawing.Point(711, 46)
        Me.lblShopOpen.Name = "lblShopOpen"
        Me.lblShopOpen.Size = New System.Drawing.Size(13, 18)
        Me.lblShopOpen.TabIndex = 161
        Me.lblShopOpen.Text = "-"
        Me.lblShopOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(540, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(165, 18)
        Me.Label12.TabIndex = 160
        Me.Label12.Text = "Shop Operation Hours :"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(43, 485)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(146, 18)
        Me.Label11.TabIndex = 159
        Me.Label11.Text = "Appointment Policies"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cbCloseShop)
        Me.Panel1.Controls.Add(Me.Label40)
        Me.Panel1.Controls.Add(Me.Label38)
        Me.Panel1.Controls.Add(Me.nud_k_no_show_within)
        Me.Panel1.Controls.Add(Me.Label39)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label41)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.nud_k_no_show)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.nud_k_not_book)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Location = New System.Drawing.Point(15, 496)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(951, 91)
        Me.Panel1.TabIndex = 75
        '
        'cbCloseShop
        '
        Me.cbCloseShop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbCloseShop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbCloseShop.BackColor = System.Drawing.Color.White
        Me.cbCloseShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCloseShop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCloseShop.FormattingEnabled = True
        Me.cbCloseShop.Items.AddRange(New Object() {"30", "60", "90", "120"})
        Me.cbCloseShop.Location = New System.Drawing.Point(273, 37)
        Me.cbCloseShop.Name = "cbCloseShop"
        Me.cbCloseShop.Size = New System.Drawing.Size(54, 26)
        Me.cbCloseShop.TabIndex = 155
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(331, 9)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(45, 18)
        Me.Label40.TabIndex = 158
        Me.Label40.Text = "within"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(333, 39)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(70, 18)
        Me.Label38.TabIndex = 154
        Me.Label38.Text = "minute(s)"
        '
        'nud_k_no_show_within
        '
        Me.nud_k_no_show_within.Enabled = False
        Me.nud_k_no_show_within.Location = New System.Drawing.Point(382, 7)
        Me.nud_k_no_show_within.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.nud_k_no_show_within.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_no_show_within.Name = "nud_k_no_show_within"
        Me.nud_k_no_show_within.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_no_show_within.TabIndex = 157
        Me.nud_k_no_show_within.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_no_show_within.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(22, 39)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(256, 18)
        Me.Label39.TabIndex = 153
        Me.Label39.Text = "2. Appointment before shop closed in "
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Button1.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(795, 43)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(148, 38)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = " Save Policies"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(444, 9)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(49, 18)
        Me.Label41.TabIndex = 156
        Me.Label41.Text = "day(s)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(290, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 18)
        Me.Label2.TabIndex = 141
        Me.Label2.Text = "times"
        '
        'nud_k_no_show
        '
        Me.nud_k_no_show.Enabled = False
        Me.nud_k_no_show.Location = New System.Drawing.Point(227, 7)
        Me.nud_k_no_show.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nud_k_no_show.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_no_show.Name = "nud_k_no_show"
        Me.nud_k_no_show.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_no_show.TabIndex = 139
        Me.nud_k_no_show.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_no_show.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(22, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(199, 18)
        Me.Label10.TabIndex = 140
        Me.Label10.Text = "1. If No show by appointment"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(493, 9)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(317, 18)
        Me.Label25.TabIndex = 144
        Me.Label25.Text = "then the customer cannot pre-book for the next"
        '
        'nud_k_not_book
        '
        Me.nud_k_not_book.Enabled = False
        Me.nud_k_not_book.Location = New System.Drawing.Point(824, 7)
        Me.nud_k_not_book.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.nud_k_not_book.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_k_not_book.Name = "nud_k_not_book"
        Me.nud_k_not_book.Size = New System.Drawing.Size(61, 24)
        Me.nud_k_not_book.TabIndex = 143
        Me.nud_k_not_book.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_k_not_book.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(891, 9)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(49, 18)
        Me.Label21.TabIndex = 142
        Me.Label21.Text = "day(s)"
        '
        'Label17
        '
        Me.Label17.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(526, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(179, 18)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "Your selected date range :"
        '
        'lblDayOfWeek
        '
        Me.lblDayOfWeek.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDayOfWeek.Location = New System.Drawing.Point(639, 75)
        Me.lblDayOfWeek.Name = "lblDayOfWeek"
        Me.lblDayOfWeek.Size = New System.Drawing.Size(320, 47)
        Me.lblDayOfWeek.TabIndex = 61
        Me.lblDayOfWeek.Text = "-"
        '
        'btnView
        '
        Me.btnView.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnView.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnView.Location = New System.Drawing.Point(289, 452)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(183, 31)
        Me.btnView.TabIndex = 73
        Me.btnView.Text = "View Slot(s) by Date"
        Me.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnView.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(540, 75)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 18)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "Recuring on  :"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 18)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Date Range"
        '
        'lblRecur
        '
        Me.lblRecur.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblRecur.AutoSize = True
        Me.lblRecur.Location = New System.Drawing.Point(711, 16)
        Me.lblRecur.Name = "lblRecur"
        Me.lblRecur.Size = New System.Drawing.Size(13, 18)
        Me.lblRecur.TabIndex = 64
        Me.lblRecur.Text = "-"
        Me.lblRecur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(509, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 18)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Appointment Slot(s)"
        '
        'mc
        '
        Me.mc.AcceptOnlyBoldedDates = False
        Me.mc.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.mc.DateValue = "28/8/2556"
        Me.mc.Enabled = False
        Me.mc.EndDate = New Date(2013, 8, 28, 0, 0, 0, 0)
        Me.mc.Location = New System.Drawing.Point(289, 288)
        Me.mc.Name = "mc"
        Me.mc.Size = New System.Drawing.Size(183, 169)
        Me.mc.StartDate = New Date(2013, 8, 28, 0, 0, 0, 0)
        Me.mc.TabIndex = 44
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(286, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 18)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "Service(s) Selection"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(823, 447)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 40)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "  Save Slot(s)"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'p_service
        '
        Me.p_service.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.p_service.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_service.Controls.Add(Me.FLP_Item)
        Me.p_service.Location = New System.Drawing.Point(264, 20)
        Me.p_service.Name = "p_service"
        Me.p_service.Size = New System.Drawing.Size(221, 260)
        Me.p_service.TabIndex = 62
        '
        'FLP_Item
        '
        Me.FLP_Item.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FLP_Item.AutoScroll = True
        Me.FLP_Item.BackColor = System.Drawing.Color.White
        Me.FLP_Item.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.FLP_Item.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLP_Item.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FLP_Item.Location = New System.Drawing.Point(4, 7)
        Me.FLP_Item.Margin = New System.Windows.Forms.Padding(0)
        Me.FLP_Item.Name = "FLP_Item"
        Me.FLP_Item.Size = New System.Drawing.Size(211, 251)
        Me.FLP_Item.TabIndex = 69
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(37, 267)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(114, 18)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "Slot Constraints"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(39, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 18)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Recurrence Day"
        '
        'p_capa
        '
        Me.p_capa.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.p_capa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_capa.Controls.Add(Me.Label3)
        Me.p_capa.Controls.Add(Me.btnGen)
        Me.p_capa.Controls.Add(Me.cbSlot)
        Me.p_capa.Controls.Add(Me.nudCounter)
        Me.p_capa.Controls.Add(Me.Label33)
        Me.p_capa.Controls.Add(Me.Label16)
        Me.p_capa.Controls.Add(Me.Label34)
        Me.p_capa.Controls.Add(Me.Label37)
        Me.p_capa.Controls.Add(Me.Label35)
        Me.p_capa.Controls.Add(Me.CBend_time_h)
        Me.p_capa.Controls.Add(Me.CBend_time_m)
        Me.p_capa.Controls.Add(Me.CBstart_time_m)
        Me.p_capa.Controls.Add(Me.CBstart_time_h)
        Me.p_capa.Location = New System.Drawing.Point(15, 278)
        Me.p_capa.Name = "p_capa"
        Me.p_capa.Size = New System.Drawing.Size(243, 205)
        Me.p_capa.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 18)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "No. of Counter(s) :"
        '
        'btnGen
        '
        Me.btnGen.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnGen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGen.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnGen.ForeColor = System.Drawing.Color.Navy
        Me.btnGen.Image = CType(resources.GetObject("btnGen.Image"), System.Drawing.Image)
        Me.btnGen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGen.Location = New System.Drawing.Point(28, 150)
        Me.btnGen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGen.Name = "btnGen"
        Me.btnGen.Size = New System.Drawing.Size(184, 44)
        Me.btnGen.TabIndex = 13
        Me.btnGen.Text = "  Generate Slot"
        Me.btnGen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnGen.UseVisualStyleBackColor = True
        '
        'cbSlot
        '
        Me.cbSlot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSlot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSlot.BackColor = System.Drawing.Color.White
        Me.cbSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbSlot.FormattingEnabled = True
        Me.cbSlot.Items.AddRange(New Object() {"30", "60", "90", "120"})
        Me.cbSlot.Location = New System.Drawing.Point(173, 117)
        Me.cbSlot.Name = "cbSlot"
        Me.cbSlot.Size = New System.Drawing.Size(54, 26)
        Me.cbSlot.TabIndex = 68
        '
        'nudCounter
        '
        Me.nudCounter.Location = New System.Drawing.Point(173, 85)
        Me.nudCounter.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudCounter.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCounter.Name = "nudCounter"
        Me.nudCounter.Size = New System.Drawing.Size(54, 24)
        Me.nudCounter.TabIndex = 13
        Me.nudCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudCounter.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(160, 21)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(12, 18)
        Me.Label33.TabIndex = 151
        Me.Label33.Text = ":"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(47, 120)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(125, 18)
        Me.Label16.TabIndex = 55
        Me.Label16.Text = "Slot Time (mins) :"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(17, 19)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(84, 18)
        Me.Label34.TabIndex = 149
        Me.Label34.Text = "Start Time :"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(17, 54)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(79, 18)
        Me.Label37.TabIndex = 150
        Me.Label37.Text = "End Time :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(160, 53)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(12, 18)
        Me.Label35.TabIndex = 152
        Me.Label35.Text = ":"
        '
        'CBend_time_h
        '
        Me.CBend_time_h.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBend_time_h.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBend_time_h.BackColor = System.Drawing.Color.White
        Me.CBend_time_h.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBend_time_h.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBend_time_h.FormattingEnabled = True
        Me.CBend_time_h.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.CBend_time_h.Location = New System.Drawing.Point(104, 50)
        Me.CBend_time_h.Name = "CBend_time_h"
        Me.CBend_time_h.Size = New System.Drawing.Size(54, 26)
        Me.CBend_time_h.TabIndex = 10
        '
        'CBend_time_m
        '
        Me.CBend_time_m.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBend_time_m.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBend_time_m.BackColor = System.Drawing.Color.White
        Me.CBend_time_m.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBend_time_m.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBend_time_m.FormattingEnabled = True
        Me.CBend_time_m.Items.AddRange(New Object() {"00", "30"})
        Me.CBend_time_m.Location = New System.Drawing.Point(173, 50)
        Me.CBend_time_m.Name = "CBend_time_m"
        Me.CBend_time_m.Size = New System.Drawing.Size(54, 26)
        Me.CBend_time_m.TabIndex = 11
        '
        'CBstart_time_m
        '
        Me.CBstart_time_m.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBstart_time_m.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBstart_time_m.BackColor = System.Drawing.Color.White
        Me.CBstart_time_m.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBstart_time_m.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBstart_time_m.FormattingEnabled = True
        Me.CBstart_time_m.Items.AddRange(New Object() {"00", "30"})
        Me.CBstart_time_m.Location = New System.Drawing.Point(173, 17)
        Me.CBstart_time_m.Name = "CBstart_time_m"
        Me.CBstart_time_m.Size = New System.Drawing.Size(54, 26)
        Me.CBstart_time_m.TabIndex = 9
        '
        'CBstart_time_h
        '
        Me.CBstart_time_h.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBstart_time_h.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBstart_time_h.BackColor = System.Drawing.Color.White
        Me.CBstart_time_h.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBstart_time_h.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBstart_time_h.FormattingEnabled = True
        Me.CBstart_time_h.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.CBstart_time_h.Location = New System.Drawing.Point(104, 17)
        Me.CBstart_time_h.Name = "CBstart_time_h"
        Me.CBstart_time_h.Size = New System.Drawing.Size(54, 26)
        Me.CBstart_time_h.TabIndex = 8
        '
        'p_week
        '
        Me.p_week.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.p_week.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_week.Controls.Add(Me.cbSun)
        Me.p_week.Controls.Add(Me.cbSat)
        Me.p_week.Controls.Add(Me.cbFri)
        Me.p_week.Controls.Add(Me.cbThu)
        Me.p_week.Controls.Add(Me.cbWed)
        Me.p_week.Controls.Add(Me.cbTue)
        Me.p_week.Controls.Add(Me.cbMon)
        Me.p_week.Location = New System.Drawing.Point(15, 130)
        Me.p_week.Name = "p_week"
        Me.p_week.Size = New System.Drawing.Size(243, 137)
        Me.p_week.TabIndex = 59
        '
        'cbSun
        '
        Me.cbSun.AutoSize = True
        Me.cbSun.Location = New System.Drawing.Point(149, 74)
        Me.cbSun.Name = "cbSun"
        Me.cbSun.Size = New System.Drawing.Size(76, 22)
        Me.cbSun.TabIndex = 6
        Me.cbSun.Text = "Sunday"
        Me.cbSun.UseVisualStyleBackColor = True
        '
        'cbSat
        '
        Me.cbSat.AutoSize = True
        Me.cbSat.Location = New System.Drawing.Point(149, 46)
        Me.cbSat.Name = "cbSat"
        Me.cbSat.Size = New System.Drawing.Size(85, 22)
        Me.cbSat.TabIndex = 5
        Me.cbSat.Text = "Saturday"
        Me.cbSat.UseVisualStyleBackColor = True
        '
        'cbFri
        '
        Me.cbFri.AutoSize = True
        Me.cbFri.Location = New System.Drawing.Point(149, 18)
        Me.cbFri.Name = "cbFri"
        Me.cbFri.Size = New System.Drawing.Size(67, 22)
        Me.cbFri.TabIndex = 4
        Me.cbFri.Text = "Friday"
        Me.cbFri.UseVisualStyleBackColor = True
        '
        'cbThu
        '
        Me.cbThu.AutoSize = True
        Me.cbThu.Location = New System.Drawing.Point(13, 102)
        Me.cbThu.Name = "cbThu"
        Me.cbThu.Size = New System.Drawing.Size(88, 22)
        Me.cbThu.TabIndex = 3
        Me.cbThu.Text = "Thursday"
        Me.cbThu.UseVisualStyleBackColor = True
        '
        'cbWed
        '
        Me.cbWed.AutoSize = True
        Me.cbWed.Location = New System.Drawing.Point(13, 74)
        Me.cbWed.Name = "cbWed"
        Me.cbWed.Size = New System.Drawing.Size(105, 22)
        Me.cbWed.TabIndex = 2
        Me.cbWed.Text = "Wednesday"
        Me.cbWed.UseVisualStyleBackColor = True
        '
        'cbTue
        '
        Me.cbTue.AutoSize = True
        Me.cbTue.Location = New System.Drawing.Point(13, 46)
        Me.cbTue.Name = "cbTue"
        Me.cbTue.Size = New System.Drawing.Size(83, 22)
        Me.cbTue.TabIndex = 1
        Me.cbTue.Text = "Tuesday"
        Me.cbTue.UseVisualStyleBackColor = True
        '
        'cbMon
        '
        Me.cbMon.AutoSize = True
        Me.cbMon.Location = New System.Drawing.Point(13, 18)
        Me.cbMon.Name = "cbMon"
        Me.cbMon.Size = New System.Drawing.Size(80, 22)
        Me.cbMon.TabIndex = 0
        Me.cbMon.Text = "Monday"
        Me.cbMon.UseVisualStyleBackColor = True
        '
        'p_slot
        '
        Me.p_slot.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.p_slot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_slot.Controls.Add(Me.flpTime)
        Me.p_slot.Location = New System.Drawing.Point(491, 135)
        Me.p_slot.Name = "p_slot"
        Me.p_slot.Size = New System.Drawing.Size(475, 304)
        Me.p_slot.TabIndex = 55
        '
        'flpTime
        '
        Me.flpTime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpTime.AutoScroll = True
        Me.flpTime.BackColor = System.Drawing.Color.White
        Me.flpTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.flpTime.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.flpTime.Location = New System.Drawing.Point(8, 15)
        Me.flpTime.Margin = New System.Windows.Forms.Padding(0)
        Me.flpTime.Name = "flpTime"
        Me.flpTime.Size = New System.Drawing.Size(456, 279)
        Me.flpTime.TabIndex = 14
        '
        'p_date
        '
        Me.p_date.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.p_date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_date.Controls.Add(Me.dtpEnd)
        Me.p_date.Controls.Add(Me.dtpStart)
        Me.p_date.Controls.Add(Me.Label8)
        Me.p_date.Location = New System.Drawing.Point(17, 20)
        Me.p_date.Name = "p_date"
        Me.p_date.Size = New System.Drawing.Size(242, 99)
        Me.p_date.TabIndex = 60
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(22, 63)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(200, 24)
        Me.dtpEnd.TabIndex = 1
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(20, 12)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(200, 24)
        Me.dtpStart.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(105, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 18)
        Me.Label8.TabIndex = 61
        Me.Label8.Text = "To"
        '
        'frmAppointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(994, 592)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAppointment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Appointment"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.nudGap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nud_k_no_show_within, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_no_show, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_k_not_book, System.ComponentModel.ISupportInitialize).EndInit()
        Me.p_service.ResumeLayout(False)
        Me.p_capa.ResumeLayout(False)
        Me.p_capa.PerformLayout()
        CType(Me.nudCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.p_week.ResumeLayout(False)
        Me.p_week.PerformLayout()
        Me.p_slot.ResumeLayout(False)
        Me.p_date.ResumeLayout(False)
        Me.p_date.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents ColorPicker As System.Windows.Forms.ColorDialog
    Friend WithEvents flpTime As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents p_slot As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnGen As System.Windows.Forms.Button
    Friend WithEvents p_week As System.Windows.Forms.Panel
    Friend WithEvents cbSun As System.Windows.Forms.CheckBox
    Friend WithEvents cbSat As System.Windows.Forms.CheckBox
    Friend WithEvents cbFri As System.Windows.Forms.CheckBox
    Friend WithEvents cbThu As System.Windows.Forms.CheckBox
    Friend WithEvents cbWed As System.Windows.Forms.CheckBox
    Friend WithEvents cbTue As System.Windows.Forms.CheckBox
    Friend WithEvents cbMon As System.Windows.Forms.CheckBox
    Friend WithEvents p_date As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents p_capa As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nudCounter As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudGap As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents FLP_Item As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cbSlot As System.Windows.Forms.ComboBox
    Friend WithEvents mc As Calendar.Calendar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents p_service As System.Windows.Forms.Panel
    Friend WithEvents CBstart_time_m As System.Windows.Forms.ComboBox
    Friend WithEvents CBstart_time_h As System.Windows.Forms.ComboBox
    Friend WithEvents lblRecur As System.Windows.Forms.Label
    Friend WithEvents CBend_time_m As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CBend_time_h As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblDayOfWeek As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbCloseShop As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents nud_k_no_show_within As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nud_k_no_show As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents nud_k_not_book As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblShopOpen As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
