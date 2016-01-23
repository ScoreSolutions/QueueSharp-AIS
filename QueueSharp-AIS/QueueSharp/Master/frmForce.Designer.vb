<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmForce
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmForce))
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label34 = New System.Windows.Forms.Label
        Me.btnGen = New System.Windows.Forms.Button
        Me.CBstart_time_h = New System.Windows.Forms.ComboBox
        Me.cbSlot = New System.Windows.Forms.ComboBox
        Me.CBstart_time_m = New System.Windows.Forms.ComboBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.CBend_time_m = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.CBend_time_h = New System.Windows.Forms.ComboBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.btnView = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.mc = New Calendar.Calendar
        Me.btnSave = New System.Windows.Forms.Button
        Me.p_service = New System.Windows.Forms.Panel
        Me.lblShopOpen = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblDayOfWeek = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblRecur = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.p_capa = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.nudWait = New System.Windows.Forms.NumericUpDown
        Me.Label9 = New System.Windows.Forms.Label
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
        Me.Panel1.SuspendLayout()
        Me.p_service.SuspendLayout()
        Me.p_capa.SuspendLayout()
        CType(Me.nudWait, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBoxButton.Controls.Add(Me.Label3)
        Me.GroupBoxButton.Controls.Add(Me.Panel1)
        Me.GroupBoxButton.Controls.Add(Me.btnView)
        Me.GroupBoxButton.Controls.Add(Me.Label6)
        Me.GroupBoxButton.Controls.Add(Me.Label7)
        Me.GroupBoxButton.Controls.Add(Me.mc)
        Me.GroupBoxButton.Controls.Add(Me.btnSave)
        Me.GroupBoxButton.Controls.Add(Me.p_service)
        Me.GroupBoxButton.Controls.Add(Me.Label5)
        Me.GroupBoxButton.Controls.Add(Me.p_capa)
        Me.GroupBoxButton.Controls.Add(Me.p_week)
        Me.GroupBoxButton.Controls.Add(Me.p_slot)
        Me.GroupBoxButton.Controls.Add(Me.p_date)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, -3)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(1, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(20)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(921, 502)
        Me.GroupBoxButton.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 274)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 18)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Auto Force"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label34)
        Me.Panel1.Controls.Add(Me.btnGen)
        Me.Panel1.Controls.Add(Me.CBstart_time_h)
        Me.Panel1.Controls.Add(Me.cbSlot)
        Me.Panel1.Controls.Add(Me.CBstart_time_m)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.CBend_time_m)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.CBend_time_h)
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.Label37)
        Me.Panel1.Location = New System.Drawing.Point(835, 272)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(63, 18)
        Me.Panel1.TabIndex = 74
        Me.Panel1.Visible = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(-4, 16)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(84, 18)
        Me.Label34.TabIndex = 149
        Me.Label34.Text = "Start Time :"
        '
        'btnGen
        '
        Me.btnGen.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnGen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGen.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnGen.ForeColor = System.Drawing.Color.Navy
        Me.btnGen.Image = CType(resources.GetObject("btnGen.Image"), System.Drawing.Image)
        Me.btnGen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGen.Location = New System.Drawing.Point(-49, 118)
        Me.btnGen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGen.Name = "btnGen"
        Me.btnGen.Size = New System.Drawing.Size(184, 44)
        Me.btnGen.TabIndex = 13
        Me.btnGen.Text = "  Generate Slot"
        Me.btnGen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnGen.UseVisualStyleBackColor = True
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
        Me.CBstart_time_h.Location = New System.Drawing.Point(83, 14)
        Me.CBstart_time_h.Name = "CBstart_time_h"
        Me.CBstart_time_h.Size = New System.Drawing.Size(54, 26)
        Me.CBstart_time_h.TabIndex = 8
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
        Me.cbSlot.Location = New System.Drawing.Point(152, 79)
        Me.cbSlot.Name = "cbSlot"
        Me.cbSlot.Size = New System.Drawing.Size(54, 26)
        Me.cbSlot.TabIndex = 68
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
        Me.CBstart_time_m.Location = New System.Drawing.Point(152, 14)
        Me.CBstart_time_m.Name = "CBstart_time_m"
        Me.CBstart_time_m.Size = New System.Drawing.Size(54, 26)
        Me.CBstart_time_m.TabIndex = 9
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(139, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(12, 18)
        Me.Label33.TabIndex = 151
        Me.Label33.Text = ":"
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
        Me.CBend_time_m.Location = New System.Drawing.Point(152, 47)
        Me.CBend_time_m.Name = "CBend_time_m"
        Me.CBend_time_m.Size = New System.Drawing.Size(54, 26)
        Me.CBend_time_m.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(26, 82)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(125, 18)
        Me.Label16.TabIndex = 55
        Me.Label16.Text = "Slot Time (mins) :"
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
        Me.CBend_time_h.Location = New System.Drawing.Point(83, 47)
        Me.CBend_time_h.Name = "CBend_time_h"
        Me.CBend_time_h.Size = New System.Drawing.Size(54, 26)
        Me.CBend_time_h.TabIndex = 10
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(139, 50)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(12, 18)
        Me.Label35.TabIndex = 152
        Me.Label35.Text = ":"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(-4, 51)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(79, 18)
        Me.Label37.TabIndex = 150
        Me.Label37.Text = "End Time :"
        '
        'btnView
        '
        Me.btnView.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnView.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnView.Location = New System.Drawing.Point(729, 181)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(180, 30)
        Me.btnView.TabIndex = 73
        Me.btnView.Text = "View Slot(s) by Date"
        Me.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnView.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 18)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Date Range"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(284, 148)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 18)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Force Time Slot(s)"
        '
        'mc
        '
        Me.mc.AcceptOnlyBoldedDates = False
        Me.mc.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.mc.DateValue = "6/19/2012"
        Me.mc.Enabled = False
        Me.mc.EndDate = New Date(2012, 6, 19, 0, 0, 0, 0)
        Me.mc.Location = New System.Drawing.Point(728, 16)
        Me.mc.Name = "mc"
        Me.mc.Size = New System.Drawing.Size(183, 169)
        Me.mc.StartDate = New Date(2012, 6, 19, 0, 0, 0, 0)
        Me.mc.TabIndex = 44
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(578, 443)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 40)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "  Save Slot(s)"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'p_service
        '
        Me.p_service.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_service.Controls.Add(Me.lblShopOpen)
        Me.p_service.Controls.Add(Me.Label17)
        Me.p_service.Controls.Add(Me.lblDayOfWeek)
        Me.p_service.Controls.Add(Me.Label18)
        Me.p_service.Controls.Add(Me.Label12)
        Me.p_service.Controls.Add(Me.lblRecur)
        Me.p_service.Location = New System.Drawing.Point(269, 20)
        Me.p_service.Name = "p_service"
        Me.p_service.Size = New System.Drawing.Size(452, 127)
        Me.p_service.TabIndex = 62
        '
        'lblShopOpen
        '
        Me.lblShopOpen.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblShopOpen.AutoSize = True
        Me.lblShopOpen.Location = New System.Drawing.Point(192, 37)
        Me.lblShopOpen.Name = "lblShopOpen"
        Me.lblShopOpen.Size = New System.Drawing.Size(13, 18)
        Me.lblShopOpen.TabIndex = 161
        Me.lblShopOpen.Text = "-"
        Me.lblShopOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(7, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(179, 18)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "Your selected date range :"
        '
        'lblDayOfWeek
        '
        Me.lblDayOfWeek.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDayOfWeek.Location = New System.Drawing.Point(120, 66)
        Me.lblDayOfWeek.Name = "lblDayOfWeek"
        Me.lblDayOfWeek.Size = New System.Drawing.Size(320, 50)
        Me.lblDayOfWeek.TabIndex = 61
        Me.lblDayOfWeek.Text = "-"
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(21, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 18)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "Recuring on  :"
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(165, 18)
        Me.Label12.TabIndex = 160
        Me.Label12.Text = "Shop Operation Hours :"
        '
        'lblRecur
        '
        Me.lblRecur.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblRecur.AutoSize = True
        Me.lblRecur.Location = New System.Drawing.Point(192, 7)
        Me.lblRecur.Name = "lblRecur"
        Me.lblRecur.Size = New System.Drawing.Size(13, 18)
        Me.lblRecur.TabIndex = 64
        Me.lblRecur.Text = "-"
        Me.lblRecur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 18)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Recurrence Day"
        '
        'p_capa
        '
        Me.p_capa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_capa.Controls.Add(Me.Label1)
        Me.p_capa.Controls.Add(Me.nudWait)
        Me.p_capa.Controls.Add(Me.Label9)
        Me.p_capa.Location = New System.Drawing.Point(13, 283)
        Me.p_capa.Name = "p_capa"
        Me.p_capa.Size = New System.Drawing.Size(243, 76)
        Me.p_capa.TabIndex = 62
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 18)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "Next queue :"
        '
        'nudWait
        '
        Me.nudWait.Location = New System.Drawing.Point(99, 29)
        Me.nudWait.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudWait.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudWait.Name = "nudWait"
        Me.nudWait.Size = New System.Drawing.Size(54, 24)
        Me.nudWait.TabIndex = 13
        Me.nudWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudWait.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(164, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 18)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "Seconds"
        '
        'p_week
        '
        Me.p_week.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_week.Controls.Add(Me.cbSun)
        Me.p_week.Controls.Add(Me.cbSat)
        Me.p_week.Controls.Add(Me.cbFri)
        Me.p_week.Controls.Add(Me.cbThu)
        Me.p_week.Controls.Add(Me.cbWed)
        Me.p_week.Controls.Add(Me.cbTue)
        Me.p_week.Controls.Add(Me.cbMon)
        Me.p_week.Location = New System.Drawing.Point(13, 135)
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
        Me.p_slot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_slot.Controls.Add(Me.flpTime)
        Me.p_slot.Location = New System.Drawing.Point(269, 158)
        Me.p_slot.Name = "p_slot"
        Me.p_slot.Size = New System.Drawing.Size(452, 276)
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
        Me.flpTime.Size = New System.Drawing.Size(433, 251)
        Me.flpTime.TabIndex = 14
        '
        'p_date
        '
        Me.p_date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_date.Controls.Add(Me.dtpEnd)
        Me.p_date.Controls.Add(Me.dtpStart)
        Me.p_date.Controls.Add(Me.Label8)
        Me.p_date.Location = New System.Drawing.Point(13, 25)
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
        'frmForce
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(931, 502)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmForce"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Force - UnForce"
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.p_service.ResumeLayout(False)
        Me.p_service.PerformLayout()
        Me.p_capa.ResumeLayout(False)
        Me.p_capa.PerformLayout()
        CType(Me.nudWait, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents nudWait As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbSlot As System.Windows.Forms.ComboBox
    Friend WithEvents mc As Calendar.Calendar
    Friend WithEvents lblRecur As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblDayOfWeek As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents lblShopOpen As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents p_service As System.Windows.Forms.Panel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents CBend_time_h As System.Windows.Forms.ComboBox
    Friend WithEvents CBend_time_m As System.Windows.Forms.ComboBox
    Friend WithEvents CBstart_time_m As System.Windows.Forms.ComboBox
    Friend WithEvents CBstart_time_h As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
