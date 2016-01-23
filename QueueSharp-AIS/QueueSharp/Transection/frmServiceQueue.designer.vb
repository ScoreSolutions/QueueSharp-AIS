<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceQueue
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServiceQueue))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.nudWait = New System.Windows.Forms.NumericUpDown
        Me.Label04 = New System.Windows.Forms.Label
        Me.CheckTimerWait = New System.Windows.Forms.CheckBox
        Me.btnRefreshWait = New System.Windows.Forms.Button
        Me.lbltype = New System.Windows.Forms.Label
        Me.lblQueue = New System.Windows.Forms.Label
        Me.Label03 = New System.Windows.Forms.Label
        Me.Label01 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtDisplayQueue = New System.Windows.Forms.TextBox
        Me.TabMessagePage = New System.Windows.Forms.TabControl
        Me.TabServicePage = New System.Windows.Forms.TabPage
        Me.GroupBoxHistory = New CodeVendor.Controls.Grouper
        Me.GridItem = New System.Windows.Forms.DataGridView
        Me.item_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ServiceTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.serve = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCam = New System.Windows.Forms.Button
        Me.cbItem = New System.Windows.Forms.ComboBox
        Me.pb1 = New System.Windows.Forms.PictureBox
        Me.lblRoomName = New System.Windows.Forms.Label
        Me.pb2 = New System.Windows.Forms.PictureBox
        Me.pnQuickService = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtQueue = New System.Windows.Forms.TextBox
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.lblCountCustomer = New System.Windows.Forms.Label
        Me.btnCall = New System.Windows.Forms.Button
        Me.GroupBoxView = New CodeVendor.Controls.Grouper
        Me.GridWait = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnMessage = New System.Windows.Forms.Button
        Me.btnCompactView = New System.Windows.Forms.Button
        Me.btnExpand = New System.Windows.Forms.Button
        Me.lblCustomerName = New System.Windows.Forms.Label
        Me.lblCustomerID = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCustomerTypeID = New System.Windows.Forms.TextBox
        Me.FLP = New System.Windows.Forms.FlowLayoutPanel
        Me.TabViewPage = New System.Windows.Forms.TabPage
        Me.GroupBoxViewCustomerEnd = New CodeVendor.Controls.Grouper
        Me.lblServe = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GridEnd = New System.Windows.Forms.DataGridView
        Me.Use_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.xxx = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Segment_ = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.service = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Waiting_T = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.served_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.status_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TotalWaiting = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.row = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GridEnd_colNetwork = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.timerRefreshWait = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn
        Me.Grouper2 = New CodeVendor.Controls.Grouper
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtMsg = New System.Windows.Forms.TextBox
        Me.ComboH = New System.Windows.Forms.ComboBox
        Me.ComboM = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblQueue_msg = New System.Windows.Forms.Label
        Me.lblCustomerID_msg = New System.Windows.Forms.Label
        Me.Grouper4 = New CodeVendor.Controls.Grouper
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.customertype_name_msg = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customer_id_msg = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.queue_no_msg = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FLPAllService = New System.Windows.Forms.FlowLayoutPanel
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn
        Me.queue_no = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customer_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customer_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Segment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customertype_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.hold = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.totaltime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ck_hold = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ck_misscall = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.register = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customertype_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.serve_custype = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.serve_custype_allow = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.color = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.priority_value = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Cancel = New System.Windows.Forms.DataGridViewButtonColumn
        Me.colNetworkType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMigrateToBos = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Brand_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.nudWait, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabMessagePage.SuspendLayout()
        Me.TabServicePage.SuspendLayout()
        Me.GroupBoxHistory.SuspendLayout()
        CType(Me.GridItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnQuickService.SuspendLayout()
        Me.GroupBoxButton.SuspendLayout()
        Me.GroupBoxView.SuspendLayout()
        CType(Me.GridWait, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TabViewPage.SuspendLayout()
        Me.GroupBoxViewCustomerEnd.SuspendLayout()
        CType(Me.GridEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nudWait
        '
        Me.nudWait.Location = New System.Drawing.Point(380, 13)
        Me.nudWait.Margin = New System.Windows.Forms.Padding(4)
        Me.nudWait.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.nudWait.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudWait.Name = "nudWait"
        Me.nudWait.Size = New System.Drawing.Size(62, 24)
        Me.nudWait.TabIndex = 5
        Me.nudWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudWait.Value = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudWait.Visible = False
        '
        'Label04
        '
        Me.Label04.AutoSize = True
        Me.Label04.Location = New System.Drawing.Point(448, 15)
        Me.Label04.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label04.Name = "Label04"
        Me.Label04.Size = New System.Drawing.Size(65, 18)
        Me.Label04.TabIndex = 4
        Me.Label04.Text = "seconds"
        Me.Label04.Visible = False
        '
        'CheckTimerWait
        '
        Me.CheckTimerWait.AutoSize = True
        Me.CheckTimerWait.Location = New System.Drawing.Point(234, 14)
        Me.CheckTimerWait.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckTimerWait.Name = "CheckTimerWait"
        Me.CheckTimerWait.Size = New System.Drawing.Size(145, 22)
        Me.CheckTimerWait.TabIndex = 7
        Me.CheckTimerWait.Text = "Update data every"
        Me.CheckTimerWait.UseVisualStyleBackColor = True
        Me.CheckTimerWait.Visible = False
        '
        'btnRefreshWait
        '
        Me.btnRefreshWait.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshWait.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnRefreshWait.Location = New System.Drawing.Point(114, 9)
        Me.btnRefreshWait.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefreshWait.Name = "btnRefreshWait"
        Me.btnRefreshWait.Size = New System.Drawing.Size(112, 32)
        Me.btnRefreshWait.TabIndex = 10
        Me.btnRefreshWait.Text = "Refresh"
        Me.btnRefreshWait.UseVisualStyleBackColor = True
        Me.btnRefreshWait.Visible = False
        '
        'lbltype
        '
        Me.lbltype.AutoSize = True
        Me.lbltype.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.lbltype.Location = New System.Drawing.Point(396, 83)
        Me.lbltype.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(15, 20)
        Me.lbltype.TabIndex = 33
        Me.lbltype.Text = "-"
        Me.lbltype.Visible = False
        '
        'lblQueue
        '
        Me.lblQueue.AutoSize = True
        Me.lblQueue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.lblQueue.Location = New System.Drawing.Point(385, 83)
        Me.lblQueue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQueue.Name = "lblQueue"
        Me.lblQueue.Size = New System.Drawing.Size(15, 20)
        Me.lblQueue.TabIndex = 30
        Me.lblQueue.Text = "-"
        Me.lblQueue.Visible = False
        '
        'Label03
        '
        Me.Label03.AutoSize = True
        Me.Label03.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.Label03.Location = New System.Drawing.Point(385, 83)
        Me.Label03.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label03.Name = "Label03"
        Me.Label03.Size = New System.Drawing.Size(139, 20)
        Me.Label03.TabIndex = 29
        Me.Label03.Text = "Customer Type :"
        Me.Label03.Visible = False
        '
        'Label01
        '
        Me.Label01.AutoSize = True
        Me.Label01.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.Label01.Location = New System.Drawing.Point(384, 83)
        Me.Label01.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label01.Name = "Label01"
        Me.Label01.Size = New System.Drawing.Size(99, 20)
        Me.Label01.TabIndex = 26
        Me.Label01.Text = "Queue No :"
        Me.Label01.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.DarkRed
        Me.btnCancel.Location = New System.Drawing.Point(287, 45)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 47)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel Service" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'txtDisplayQueue
        '
        Me.txtDisplayQueue.BackColor = System.Drawing.SystemColors.MenuText
        Me.txtDisplayQueue.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtDisplayQueue.ForeColor = System.Drawing.Color.Lime
        Me.txtDisplayQueue.Location = New System.Drawing.Point(114, 29)
        Me.txtDisplayQueue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDisplayQueue.MaxLength = 10000
        Me.txtDisplayQueue.Name = "txtDisplayQueue"
        Me.txtDisplayQueue.ReadOnly = True
        Me.txtDisplayQueue.Size = New System.Drawing.Size(165, 62)
        Me.txtDisplayQueue.TabIndex = 8
        Me.txtDisplayQueue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtDisplayQueue.Visible = False
        '
        'TabMessagePage
        '
        Me.TabMessagePage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabMessagePage.Controls.Add(Me.TabServicePage)
        Me.TabMessagePage.Controls.Add(Me.TabViewPage)
        Me.TabMessagePage.Location = New System.Drawing.Point(0, 0)
        Me.TabMessagePage.Margin = New System.Windows.Forms.Padding(4)
        Me.TabMessagePage.Name = "TabMessagePage"
        Me.TabMessagePage.SelectedIndex = 0
        Me.TabMessagePage.Size = New System.Drawing.Size(800, 441)
        Me.TabMessagePage.TabIndex = 3
        '
        'TabServicePage
        '
        Me.TabServicePage.BackColor = System.Drawing.Color.AliceBlue
        Me.TabServicePage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabServicePage.Controls.Add(Me.GroupBoxHistory)
        Me.TabServicePage.Controls.Add(Me.Panel1)
        Me.TabServicePage.Controls.Add(Me.pnQuickService)
        Me.TabServicePage.Controls.Add(Me.GroupBoxButton)
        Me.TabServicePage.Controls.Add(Me.GroupBoxView)
        Me.TabServicePage.Location = New System.Drawing.Point(4, 27)
        Me.TabServicePage.Margin = New System.Windows.Forms.Padding(4)
        Me.TabServicePage.Name = "TabServicePage"
        Me.TabServicePage.Padding = New System.Windows.Forms.Padding(4)
        Me.TabServicePage.Size = New System.Drawing.Size(792, 410)
        Me.TabServicePage.TabIndex = 0
        Me.TabServicePage.Text = "Service"
        Me.TabServicePage.UseVisualStyleBackColor = True
        '
        'GroupBoxHistory
        '
        Me.GroupBoxHistory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxHistory.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxHistory.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.GroupBoxHistory.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxHistory.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxHistory.BorderThickness = 1.0!
        Me.GroupBoxHistory.Controls.Add(Me.GridItem)
        Me.GroupBoxHistory.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxHistory.GroupImage = Nothing
        Me.GroupBoxHistory.GroupTitle = ""
        Me.GroupBoxHistory.Location = New System.Drawing.Point(324, -2)
        Me.GroupBoxHistory.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxHistory.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxHistory.Name = "GroupBoxHistory"
        Me.GroupBoxHistory.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxHistory.PaintGroupBox = False
        Me.GroupBoxHistory.RoundCorners = 10
        Me.GroupBoxHistory.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxHistory.ShadowControl = True
        Me.GroupBoxHistory.ShadowThickness = 3
        Me.GroupBoxHistory.Size = New System.Drawing.Size(107, 155)
        Me.GroupBoxHistory.TabIndex = 23
        '
        'GridItem
        '
        Me.GridItem.AllowUserToAddRows = False
        Me.GridItem.AllowUserToDeleteRows = False
        Me.GridItem.AllowUserToResizeColumns = False
        Me.GridItem.AllowUserToResizeRows = False
        Me.GridItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GridItem.BackgroundColor = System.Drawing.Color.White
        Me.GridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridItem.ColumnHeadersVisible = False
        Me.GridItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.item_id, Me.item_name, Me.ServiceTime, Me.serve})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridItem.DefaultCellStyle = DataGridViewCellStyle1
        Me.GridItem.Location = New System.Drawing.Point(15, 23)
        Me.GridItem.MultiSelect = False
        Me.GridItem.Name = "GridItem"
        Me.GridItem.RowHeadersVisible = False
        Me.GridItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridItem.Size = New System.Drawing.Size(74, 117)
        Me.GridItem.TabIndex = 38
        '
        'item_id
        '
        Me.item_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.item_id.DataPropertyName = "id"
        Me.item_id.HeaderText = "item_id"
        Me.item_id.MinimumWidth = 2
        Me.item_id.Name = "item_id"
        Me.item_id.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.item_id.Visible = False
        Me.item_id.Width = 2
        '
        'item_name
        '
        Me.item_name.DataPropertyName = "item_name"
        Me.item_name.HeaderText = "item_name"
        Me.item_name.Name = "item_name"
        Me.item_name.ReadOnly = True
        '
        'ServiceTime
        '
        Me.ServiceTime.DataPropertyName = "ServiceTime"
        Me.ServiceTime.HeaderText = "ServiceTime"
        Me.ServiceTime.Name = "ServiceTime"
        '
        'serve
        '
        Me.serve.DataPropertyName = "serve"
        Me.serve.HeaderText = "serve"
        Me.serve.Name = "serve"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.btnCam)
        Me.Panel1.Controls.Add(Me.cbItem)
        Me.Panel1.Controls.Add(Me.pb1)
        Me.Panel1.Controls.Add(Me.lblRoomName)
        Me.Panel1.Controls.Add(Me.pb2)
        Me.Panel1.Location = New System.Drawing.Point(438, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(352, 150)
        Me.Panel1.TabIndex = 45
        '
        'btnCam
        '
        Me.btnCam.FlatAppearance.BorderSize = 0
        Me.btnCam.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnCam.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCam.Image = Global.QueueSharp.My.Resources.Resources.webcam
        Me.btnCam.Location = New System.Drawing.Point(76, 101)
        Me.btnCam.Name = "btnCam"
        Me.btnCam.Size = New System.Drawing.Size(64, 54)
        Me.btnCam.TabIndex = 46
        Me.btnCam.UseVisualStyleBackColor = True
        '
        'cbItem
        '
        Me.cbItem.BackColor = System.Drawing.Color.RoyalBlue
        Me.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbItem.ForeColor = System.Drawing.Color.White
        Me.cbItem.FormattingEnabled = True
        Me.cbItem.Location = New System.Drawing.Point(7, 73)
        Me.cbItem.Name = "cbItem"
        Me.cbItem.Size = New System.Drawing.Size(214, 26)
        Me.cbItem.TabIndex = 45
        '
        'pb1
        '
        Me.pb1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb1.BackColor = System.Drawing.Color.Transparent
        Me.pb1.BackgroundImage = CType(resources.GetObject("pb1.BackgroundImage"), System.Drawing.Image)
        Me.pb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb1.Location = New System.Drawing.Point(227, 15)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(120, 120)
        Me.pb1.TabIndex = 43
        Me.pb1.TabStop = False
        '
        'lblRoomName
        '
        Me.lblRoomName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblRoomName.BackColor = System.Drawing.Color.PowderBlue
        Me.lblRoomName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoomName.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblRoomName.Location = New System.Drawing.Point(7, 14)
        Me.lblRoomName.Name = "lblRoomName"
        Me.lblRoomName.Size = New System.Drawing.Size(214, 54)
        Me.lblRoomName.TabIndex = 42
        Me.lblRoomName.Text = "Counter 01"
        Me.lblRoomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pb2
        '
        Me.pb2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb2.BackColor = System.Drawing.Color.Transparent
        Me.pb2.BackgroundImage = CType(resources.GetObject("pb2.BackgroundImage"), System.Drawing.Image)
        Me.pb2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb2.Location = New System.Drawing.Point(227, 15)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(120, 120)
        Me.pb2.TabIndex = 44
        Me.pb2.TabStop = False
        Me.pb2.Visible = False
        '
        'pnQuickService
        '
        Me.pnQuickService.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnQuickService.BackColor = System.Drawing.Color.Transparent
        Me.pnQuickService.Controls.Add(Me.Label2)
        Me.pnQuickService.Controls.Add(Me.txtQueue)
        Me.pnQuickService.Location = New System.Drawing.Point(546, 369)
        Me.pnQuickService.Margin = New System.Windows.Forms.Padding(4)
        Me.pnQuickService.Name = "pnQuickService"
        Me.pnQuickService.Size = New System.Drawing.Size(229, 37)
        Me.pnQuickService.TabIndex = 38
        Me.pnQuickService.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 18)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Queue No"
        '
        'txtQueue
        '
        Me.txtQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQueue.Location = New System.Drawing.Point(88, 6)
        Me.txtQueue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQueue.Name = "txtQueue"
        Me.txtQueue.Size = New System.Drawing.Size(135, 24)
        Me.txtQueue.TabIndex = 3
        '
        'GroupBoxButton
        '
        Me.GroupBoxButton.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxButton.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.GroupBoxButton.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxButton.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxButton.BorderThickness = 1.0!
        Me.GroupBoxButton.Controls.Add(Me.lblCountCustomer)
        Me.GroupBoxButton.Controls.Add(Me.btnCall)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, -2)
        Me.GroupBoxButton.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(312, 155)
        Me.GroupBoxButton.TabIndex = 22
        '
        'lblCountCustomer
        '
        Me.lblCountCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCountCustomer.BackColor = System.Drawing.Color.White
        Me.lblCountCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCountCustomer.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblCountCustomer.Location = New System.Drawing.Point(12, 118)
        Me.lblCountCustomer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCountCustomer.Name = "lblCountCustomer"
        Me.lblCountCustomer.Size = New System.Drawing.Size(284, 26)
        Me.lblCountCustomer.TabIndex = 11
        Me.lblCountCustomer.Text = "Customer Wait  0"
        Me.lblCountCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCall
        '
        Me.btnCall.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCall.BackColor = System.Drawing.Color.White
        Me.btnCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCall.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCall.Enabled = False
        Me.btnCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue
        Me.btnCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCall.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCall.ForeColor = System.Drawing.Color.DarkGreen
        Me.btnCall.Image = CType(resources.GetObject("btnCall.Image"), System.Drawing.Image)
        Me.btnCall.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnCall.Location = New System.Drawing.Point(13, 23)
        Me.btnCall.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(283, 85)
        Me.btnCall.TabIndex = 0
        Me.btnCall.Text = "  Call Next Queue (Q)"
        Me.btnCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCall.UseVisualStyleBackColor = False
        '
        'GroupBoxView
        '
        Me.GroupBoxView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxView.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxView.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBoxView.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxView.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxView.BorderThickness = 1.0!
        Me.GroupBoxView.Controls.Add(Me.GridWait)
        Me.GroupBoxView.Controls.Add(Me.Panel2)
        Me.GroupBoxView.Controls.Add(Me.FLP)
        Me.GroupBoxView.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxView.GroupImage = Nothing
        Me.GroupBoxView.GroupTitle = ""
        Me.GroupBoxView.Location = New System.Drawing.Point(6, 149)
        Me.GroupBoxView.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxView.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxView.Name = "GroupBoxView"
        Me.GroupBoxView.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxView.PaintGroupBox = False
        Me.GroupBoxView.RoundCorners = 10
        Me.GroupBoxView.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxView.ShadowControl = True
        Me.GroupBoxView.ShadowThickness = 3
        Me.GroupBoxView.Size = New System.Drawing.Size(782, 255)
        Me.GroupBoxView.TabIndex = 25
        '
        'GridWait
        '
        Me.GridWait.AllowUserToAddRows = False
        Me.GridWait.AllowUserToDeleteRows = False
        Me.GridWait.AllowUserToResizeRows = False
        Me.GridWait.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridWait.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridWait.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.queue_no, Me.customer_id, Me.customer_name, Me.Segment, Me.customertype_name, Me.hold, Me.time, Me.totaltime, Me.ck_hold, Me.ck_misscall, Me.register, Me.customertype_id, Me.serve_custype, Me.serve_custype_allow, Me.color, Me.priority_value, Me.status, Me.Cancel, Me.colNetworkType, Me.colMigrateToBos, Me.Brand_Name})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridWait.DefaultCellStyle = DataGridViewCellStyle3
        Me.GridWait.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridWait.Enabled = False
        Me.GridWait.Location = New System.Drawing.Point(10, 22)
        Me.GridWait.Margin = New System.Windows.Forms.Padding(4)
        Me.GridWait.MultiSelect = False
        Me.GridWait.Name = "GridWait"
        Me.GridWait.ReadOnly = True
        Me.GridWait.RowHeadersVisible = False
        Me.GridWait.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GridWait.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridWait.ShowCellErrors = False
        Me.GridWait.ShowCellToolTips = False
        Me.GridWait.ShowEditingIcon = False
        Me.GridWait.Size = New System.Drawing.Size(758, 220)
        Me.GridWait.TabIndex = 17
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnMessage)
        Me.Panel2.Controls.Add(Me.btnCompactView)
        Me.Panel2.Controls.Add(Me.btnExpand)
        Me.Panel2.Controls.Add(Me.lblCustomerName)
        Me.Panel2.Controls.Add(Me.lblCustomerID)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.lbltype)
        Me.Panel2.Controls.Add(Me.txtDisplayQueue)
        Me.Panel2.Controls.Add(Me.btnRefreshWait)
        Me.Panel2.Controls.Add(Me.Label01)
        Me.Panel2.Controls.Add(Me.Label04)
        Me.Panel2.Controls.Add(Me.CheckTimerWait)
        Me.Panel2.Controls.Add(Me.lblQueue)
        Me.Panel2.Controls.Add(Me.nudWait)
        Me.Panel2.Controls.Add(Me.txtCustomerTypeID)
        Me.Panel2.Controls.Add(Me.Label03)
        Me.Panel2.Location = New System.Drawing.Point(20, 40)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(484, 168)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'btnMessage
        '
        Me.btnMessage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnMessage.BackColor = System.Drawing.Color.LightCyan
        Me.btnMessage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMessage.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnMessage.ForeColor = System.Drawing.Color.Black
        Me.btnMessage.Image = CType(resources.GetObject("btnMessage.Image"), System.Drawing.Image)
        Me.btnMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMessage.Location = New System.Drawing.Point(-42, 3)
        Me.btnMessage.Name = "btnMessage"
        Me.btnMessage.Size = New System.Drawing.Size(104, 43)
        Me.btnMessage.TabIndex = 40
        Me.btnMessage.Text = "      Message"
        Me.btnMessage.UseVisualStyleBackColor = False
        '
        'btnCompactView
        '
        Me.btnCompactView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCompactView.BackColor = System.Drawing.Color.SkyBlue
        Me.btnCompactView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCompactView.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnCompactView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCompactView.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCompactView.ForeColor = System.Drawing.Color.DimGray
        Me.btnCompactView.Location = New System.Drawing.Point(331, 18)
        Me.btnCompactView.Name = "btnCompactView"
        Me.btnCompactView.Size = New System.Drawing.Size(80, 62)
        Me.btnCompactView.TabIndex = 39
        Me.btnCompactView.Text = "Compact View"
        Me.btnCompactView.UseVisualStyleBackColor = False
        '
        'btnExpand
        '
        Me.btnExpand.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnExpand.BackColor = System.Drawing.Color.LightCyan
        Me.btnExpand.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExpand.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExpand.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExpand.ForeColor = System.Drawing.Color.Black
        Me.btnExpand.Image = CType(resources.GetObject("btnExpand.Image"), System.Drawing.Image)
        Me.btnExpand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExpand.Location = New System.Drawing.Point(-42, 49)
        Me.btnExpand.Name = "btnExpand"
        Me.btnExpand.Size = New System.Drawing.Size(104, 43)
        Me.btnExpand.TabIndex = 41
        Me.btnExpand.Tag = "EXP"
        Me.btnExpand.Text = "      Expand"
        Me.btnExpand.UseVisualStyleBackColor = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.lblCustomerName.Location = New System.Drawing.Point(396, 87)
        Me.lblCustomerName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(15, 20)
        Me.lblCustomerName.TabIndex = 39
        Me.lblCustomerName.Text = "-"
        Me.lblCustomerName.Visible = False
        '
        'lblCustomerID
        '
        Me.lblCustomerID.AutoSize = True
        Me.lblCustomerID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.lblCustomerID.Location = New System.Drawing.Point(396, 87)
        Me.lblCustomerID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(15, 20)
        Me.lblCustomerID.TabIndex = 37
        Me.lblCustomerID.Text = "-"
        Me.lblCustomerID.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.Label3.Location = New System.Drawing.Point(385, 83)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 20)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Mobile No :"
        Me.Label3.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte), True)
        Me.Label5.Location = New System.Drawing.Point(377, 49)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 20)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Name :"
        Me.Label5.Visible = False
        '
        'txtCustomerTypeID
        '
        Me.txtCustomerTypeID.Location = New System.Drawing.Point(388, 83)
        Me.txtCustomerTypeID.Name = "txtCustomerTypeID"
        Me.txtCustomerTypeID.Size = New System.Drawing.Size(51, 24)
        Me.txtCustomerTypeID.TabIndex = 35
        Me.txtCustomerTypeID.Visible = False
        '
        'FLP
        '
        Me.FLP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FLP.BackColor = System.Drawing.Color.White
        Me.FLP.Location = New System.Drawing.Point(451, 22)
        Me.FLP.Name = "FLP"
        Me.FLP.Size = New System.Drawing.Size(174, 136)
        Me.FLP.TabIndex = 16
        '
        'TabViewPage
        '
        Me.TabViewPage.BackColor = System.Drawing.Color.AliceBlue
        Me.TabViewPage.Controls.Add(Me.GroupBoxViewCustomerEnd)
        Me.TabViewPage.Location = New System.Drawing.Point(4, 27)
        Me.TabViewPage.Margin = New System.Windows.Forms.Padding(4)
        Me.TabViewPage.Name = "TabViewPage"
        Me.TabViewPage.Size = New System.Drawing.Size(792, 410)
        Me.TabViewPage.TabIndex = 2
        Me.TabViewPage.Text = "View"
        Me.TabViewPage.UseVisualStyleBackColor = True
        '
        'GroupBoxViewCustomerEnd
        '
        Me.GroupBoxViewCustomerEnd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxViewCustomerEnd.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxViewCustomerEnd.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBoxViewCustomerEnd.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxViewCustomerEnd.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxViewCustomerEnd.BorderThickness = 1.0!
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.lblServe)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.Label6)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.Label4)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.GridEnd)
        Me.GroupBoxViewCustomerEnd.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxViewCustomerEnd.GroupImage = Nothing
        Me.GroupBoxViewCustomerEnd.GroupTitle = ""
        Me.GroupBoxViewCustomerEnd.Location = New System.Drawing.Point(6, -2)
        Me.GroupBoxViewCustomerEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxViewCustomerEnd.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxViewCustomerEnd.Name = "GroupBoxViewCustomerEnd"
        Me.GroupBoxViewCustomerEnd.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxViewCustomerEnd.PaintGroupBox = False
        Me.GroupBoxViewCustomerEnd.RoundCorners = 10
        Me.GroupBoxViewCustomerEnd.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxViewCustomerEnd.ShadowControl = True
        Me.GroupBoxViewCustomerEnd.ShadowThickness = 3
        Me.GroupBoxViewCustomerEnd.Size = New System.Drawing.Size(782, 408)
        Me.GroupBoxViewCustomerEnd.TabIndex = 24
        '
        'lblServe
        '
        Me.lblServe.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblServe.Location = New System.Drawing.Point(176, 376)
        Me.lblServe.Name = "lblServe"
        Me.lblServe.Size = New System.Drawing.Size(52, 18)
        Me.lblServe.TabIndex = 35
        Me.lblServe.Text = "0"
        Me.lblServe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 376)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 18)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Total customer served."
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(234, 376)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 18)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Queue"
        '
        'GridEnd
        '
        Me.GridEnd.AllowUserToAddRows = False
        Me.GridEnd.AllowUserToDeleteRows = False
        Me.GridEnd.AllowUserToResizeRows = False
        Me.GridEnd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridEnd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridEnd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Use_Name, Me.DataGridViewTextBoxColumn1, Me.CusName, Me.xxx, Me.DataGridViewTextBoxColumn3, Me.Segment_, Me.service, Me.Waiting_T, Me.served_time, Me.status_name, Me.TotalWaiting, Me.row, Me.GridEnd_colNetwork})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridEnd.DefaultCellStyle = DataGridViewCellStyle4
        Me.GridEnd.Location = New System.Drawing.Point(13, 25)
        Me.GridEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.GridEnd.MultiSelect = False
        Me.GridEnd.Name = "GridEnd"
        Me.GridEnd.ReadOnly = True
        Me.GridEnd.RowHeadersVisible = False
        Me.GridEnd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GridEnd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridEnd.ShowCellErrors = False
        Me.GridEnd.ShowCellToolTips = False
        Me.GridEnd.ShowEditingIcon = False
        Me.GridEnd.Size = New System.Drawing.Size(751, 344)
        Me.GridEnd.TabIndex = 18
        '
        'Use_Name
        '
        Me.Use_Name.DataPropertyName = "UserName"
        Me.Use_Name.HeaderText = "User Name"
        Me.Use_Name.Name = "Use_Name"
        Me.Use_Name.ReadOnly = True
        Me.Use_Name.Visible = False
        Me.Use_Name.Width = 200
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "queue_no"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Queue No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 105
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "customer_name"
        Me.CusName.HeaderText = "Name"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        Me.CusName.Visible = False
        Me.CusName.Width = 250
        '
        'xxx
        '
        Me.xxx.DataPropertyName = "customer_id"
        Me.xxx.HeaderText = "Mobile No"
        Me.xxx.Name = "xxx"
        Me.xxx.ReadOnly = True
        Me.xxx.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "customertype_name"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Customer Type"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 170
        '
        'Segment_
        '
        Me.Segment_.DataPropertyName = "Segment"
        Me.Segment_.HeaderText = "Segment"
        Me.Segment_.Name = "Segment_"
        Me.Segment_.ReadOnly = True
        Me.Segment_.Width = 150
        '
        'service
        '
        Me.service.DataPropertyName = "service"
        Me.service.HeaderText = "Service"
        Me.service.Name = "service"
        Me.service.ReadOnly = True
        Me.service.Width = 170
        '
        'Waiting_T
        '
        Me.Waiting_T.DataPropertyName = "Waiting_T"
        Me.Waiting_T.HeaderText = "Waiting Time"
        Me.Waiting_T.Name = "Waiting_T"
        Me.Waiting_T.ReadOnly = True
        Me.Waiting_T.Width = 120
        '
        'served_time
        '
        Me.served_time.DataPropertyName = "served_time"
        Me.served_time.HeaderText = "Handling Time"
        Me.served_time.Name = "served_time"
        Me.served_time.ReadOnly = True
        Me.served_time.Width = 125
        '
        'status_name
        '
        Me.status_name.DataPropertyName = "status_name"
        Me.status_name.HeaderText = "Status"
        Me.status_name.Name = "status_name"
        Me.status_name.ReadOnly = True
        '
        'TotalWaiting
        '
        Me.TotalWaiting.DataPropertyName = "TotalWaiting"
        Me.TotalWaiting.HeaderText = "TotalWaiting"
        Me.TotalWaiting.Name = "TotalWaiting"
        Me.TotalWaiting.ReadOnly = True
        '
        'row
        '
        Me.row.HeaderText = "row"
        Me.row.Name = "row"
        Me.row.ReadOnly = True
        Me.row.Visible = False
        '
        'GridEnd_colNetwork
        '
        Me.GridEnd_colNetwork.DataPropertyName = "network_type"
        Me.GridEnd_colNetwork.HeaderText = "Network Type"
        Me.GridEnd_colNetwork.Name = "GridEnd_colNetwork"
        Me.GridEnd_colNetwork.ReadOnly = True
        Me.GridEnd_colNetwork.Width = 150
        '
        'timerRefreshWait
        '
        Me.timerRefreshWait.Enabled = True
        Me.timerRefreshWait.Interval = 10000
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        '
        'Grouper2
        '
        Me.Grouper2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper2.BackgroundColor = System.Drawing.Color.White
        Me.Grouper2.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.Grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper2.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper2.BorderThickness = 1.0!
        Me.Grouper2.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper2.GroupImage = Nothing
        Me.Grouper2.GroupTitle = ""
        Me.Grouper2.Location = New System.Drawing.Point(14, 250)
        Me.Grouper2.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper2.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper2.Name = "Grouper2"
        Me.Grouper2.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper2.PaintGroupBox = False
        Me.Grouper2.RoundCorners = 10
        Me.Grouper2.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper2.ShadowControl = True
        Me.Grouper2.ShadowThickness = 3
        Me.Grouper2.Size = New System.Drawing.Size(841, 235)
        Me.Grouper2.TabIndex = 24
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 196)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 18)
        Me.Label8.TabIndex = 3
        '
        'txtMsg
        '
        Me.txtMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMsg.Location = New System.Drawing.Point(20, 51)
        Me.txtMsg.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMsg.MaxLength = 200
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMsg.Size = New System.Drawing.Size(800, 132)
        Me.txtMsg.TabIndex = 0
        '
        'ComboH
        '
        Me.ComboH.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboH.FormattingEnabled = True
        Me.ComboH.Location = New System.Drawing.Point(61, 193)
        Me.ComboH.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboH.Name = "ComboH"
        Me.ComboH.Size = New System.Drawing.Size(70, 21)
        Me.ComboH.TabIndex = 1
        '
        'ComboM
        '
        Me.ComboM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboM.FormattingEnabled = True
        Me.ComboM.Location = New System.Drawing.Point(151, 193)
        Me.ComboM.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboM.Name = "ComboM"
        Me.ComboM.Size = New System.Drawing.Size(70, 21)
        Me.ComboM.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(136, 196)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(12, 18)
        Me.Label9.TabIndex = 4
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(242, 189)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 32)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "ส่งข้อความ"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 21)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 18)
        Me.Label10.TabIndex = 7
        '
        'lblQueue_msg
        '
        Me.lblQueue_msg.AutoSize = True
        Me.lblQueue_msg.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblQueue_msg.Location = New System.Drawing.Point(117, 21)
        Me.lblQueue_msg.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQueue_msg.Name = "lblQueue_msg"
        Me.lblQueue_msg.Size = New System.Drawing.Size(14, 18)
        Me.lblQueue_msg.TabIndex = 8
        '
        'lblCustomerID_msg
        '
        Me.lblCustomerID_msg.AutoSize = True
        Me.lblCustomerID_msg.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomerID_msg.Location = New System.Drawing.Point(466, 21)
        Me.lblCustomerID_msg.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerID_msg.Name = "lblCustomerID_msg"
        Me.lblCustomerID_msg.Size = New System.Drawing.Size(0, 18)
        Me.lblCustomerID_msg.TabIndex = 9
        Me.lblCustomerID_msg.Visible = False
        '
        'Grouper4
        '
        Me.Grouper4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper4.BackgroundColor = System.Drawing.Color.White
        Me.Grouper4.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.Grouper4.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper4.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper4.BorderThickness = 1.0!
        Me.Grouper4.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper4.GroupImage = Nothing
        Me.Grouper4.GroupTitle = ""
        Me.Grouper4.Location = New System.Drawing.Point(14, 16)
        Me.Grouper4.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper4.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper4.Name = "Grouper4"
        Me.Grouper4.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper4.PaintGroupBox = False
        Me.Grouper4.RoundCorners = 10
        Me.Grouper4.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper4.ShadowControl = True
        Me.Grouper4.ShadowThickness = 3
        Me.Grouper4.Size = New System.Drawing.Size(841, 236)
        Me.Grouper4.TabIndex = 26
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(265, 18)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(555, 20)
        Me.txtSearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 18)
        Me.Label1.TabIndex = 11
        '
        'customertype_name_msg
        '
        Me.customertype_name_msg.DataPropertyName = "customertype_name"
        Me.customertype_name_msg.HeaderText = "ประเภทลูกค้า"
        Me.customertype_name_msg.Name = "customertype_name_msg"
        Me.customertype_name_msg.ReadOnly = True
        Me.customertype_name_msg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customertype_name_msg.Width = 200
        '
        'customer_id_msg
        '
        Me.customer_id_msg.DataPropertyName = "customer_id"
        Me.customer_id_msg.HeaderText = "รหัสประจำตัวลูกค้า"
        Me.customer_id_msg.Name = "customer_id_msg"
        Me.customer_id_msg.ReadOnly = True
        Me.customer_id_msg.Width = 140
        '
        'queue_no_msg
        '
        Me.queue_no_msg.DataPropertyName = "queue_no"
        Me.queue_no_msg.HeaderText = "หมายเลขคิว"
        Me.queue_no_msg.Name = "queue_no_msg"
        Me.queue_no_msg.ReadOnly = True
        Me.queue_no_msg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.queue_no_msg.Width = 120
        '
        'FLPAllService
        '
        Me.FLPAllService.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FLPAllService.AutoScroll = True
        Me.FLPAllService.Location = New System.Drawing.Point(4, 442)
        Me.FLPAllService.Name = "FLPAllService"
        Me.FLPAllService.Size = New System.Drawing.Size(792, 90)
        Me.FLPAllService.TabIndex = 53
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.HeaderText = ""
        Me.DataGridViewImageColumn2.Image = CType(resources.GetObject("DataGridViewImageColumn2.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ReadOnly = True
        '
        'queue_no
        '
        Me.queue_no.DataPropertyName = "queue_no"
        Me.queue_no.FillWeight = 71.75085!
        Me.queue_no.HeaderText = "Queue No"
        Me.queue_no.Name = "queue_no"
        Me.queue_no.ReadOnly = True
        Me.queue_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.queue_no.Width = 150
        '
        'customer_id
        '
        Me.customer_id.DataPropertyName = "customer_id"
        Me.customer_id.FillWeight = 75.32797!
        Me.customer_id.HeaderText = "Mobile No"
        Me.customer_id.Name = "customer_id"
        Me.customer_id.ReadOnly = True
        Me.customer_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customer_id.Width = 157
        '
        'customer_name
        '
        Me.customer_name.DataPropertyName = "customer_name"
        Me.customer_name.HeaderText = "Name"
        Me.customer_name.Name = "customer_name"
        Me.customer_name.ReadOnly = True
        Me.customer_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customer_name.Visible = False
        '
        'Segment
        '
        Me.Segment.DataPropertyName = "Segment"
        Me.Segment.FillWeight = 78.1944!
        Me.Segment.HeaderText = "Segment"
        Me.Segment.Name = "Segment"
        Me.Segment.ReadOnly = True
        Me.Segment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Segment.Width = 164
        '
        'customertype_name
        '
        Me.customertype_name.DataPropertyName = "customertype_name"
        Me.customertype_name.FillWeight = 42.04738!
        Me.customertype_name.HeaderText = "Customer Type"
        Me.customertype_name.Name = "customertype_name"
        Me.customertype_name.ReadOnly = True
        Me.customertype_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customertype_name.Visible = False
        Me.customertype_name.Width = 12
        '
        'hold
        '
        Me.hold.DataPropertyName = "hold"
        Me.hold.FillWeight = 53.66088!
        Me.hold.HeaderText = "App. Time"
        Me.hold.Name = "hold"
        Me.hold.ReadOnly = True
        Me.hold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.hold.Visible = False
        Me.hold.Width = 112
        '
        'time
        '
        Me.time.DataPropertyName = "time"
        Me.time.FillWeight = 48.97653!
        Me.time.HeaderText = "Waiting Time"
        Me.time.Name = "time"
        Me.time.ReadOnly = True
        Me.time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.time.Width = 103
        '
        'totaltime
        '
        Me.totaltime.DataPropertyName = "totaltime"
        Me.totaltime.HeaderText = "Total Waiting Time"
        Me.totaltime.Name = "totaltime"
        Me.totaltime.ReadOnly = True
        Me.totaltime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.totaltime.Width = 150
        '
        'ck_hold
        '
        Me.ck_hold.DataPropertyName = "ck_hold"
        Me.ck_hold.HeaderText = "ck_hold"
        Me.ck_hold.Name = "ck_hold"
        Me.ck_hold.ReadOnly = True
        Me.ck_hold.Visible = False
        '
        'ck_misscall
        '
        Me.ck_misscall.DataPropertyName = "ck_misscall"
        Me.ck_misscall.HeaderText = "ck_misscall"
        Me.ck_misscall.Name = "ck_misscall"
        Me.ck_misscall.ReadOnly = True
        Me.ck_misscall.Visible = False
        '
        'register
        '
        Me.register.DataPropertyName = "register"
        Me.register.HeaderText = "register"
        Me.register.Name = "register"
        Me.register.ReadOnly = True
        Me.register.Visible = False
        '
        'customertype_id
        '
        Me.customertype_id.DataPropertyName = "customertype_id"
        Me.customertype_id.HeaderText = "customertype_id"
        Me.customertype_id.Name = "customertype_id"
        Me.customertype_id.ReadOnly = True
        Me.customertype_id.Visible = False
        '
        'serve_custype
        '
        Me.serve_custype.DataPropertyName = "serve_custype"
        Me.serve_custype.HeaderText = "serve_custype"
        Me.serve_custype.Name = "serve_custype"
        Me.serve_custype.ReadOnly = True
        Me.serve_custype.Visible = False
        '
        'serve_custype_allow
        '
        Me.serve_custype_allow.DataPropertyName = "serve_custype_allow"
        Me.serve_custype_allow.HeaderText = "serve_custype_allow"
        Me.serve_custype_allow.Name = "serve_custype_allow"
        Me.serve_custype_allow.ReadOnly = True
        Me.serve_custype_allow.Visible = False
        '
        'color
        '
        Me.color.DataPropertyName = "color"
        Me.color.HeaderText = "color"
        Me.color.Name = "color"
        Me.color.ReadOnly = True
        Me.color.Visible = False
        '
        'priority_value
        '
        Me.priority_value.DataPropertyName = "priority_value"
        Me.priority_value.HeaderText = "priority_value"
        Me.priority_value.Name = "priority_value"
        Me.priority_value.ReadOnly = True
        Me.priority_value.Visible = False
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        Me.status.HeaderText = "status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Visible = False
        '
        'Cancel
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.MistyRose
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MistyRose
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.Cancel.DefaultCellStyle = DataGridViewCellStyle2
        Me.Cancel.FillWeight = 33.23533!
        Me.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel.HeaderText = ""
        Me.Cancel.Name = "Cancel"
        Me.Cancel.ReadOnly = True
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseColumnTextForButtonValue = True
        Me.Cancel.Visible = False
        Me.Cancel.Width = 69
        '
        'colNetworkType
        '
        Me.colNetworkType.DataPropertyName = "network_type"
        Me.colNetworkType.HeaderText = "Network Type"
        Me.colNetworkType.Name = "colNetworkType"
        Me.colNetworkType.ReadOnly = True
        Me.colNetworkType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNetworkType.Width = 150
        '
        'colMigrateToBos
        '
        Me.colMigrateToBos.DataPropertyName = "billing_system"
        Me.colMigrateToBos.HeaderText = "Migrate to BOS"
        Me.colMigrateToBos.Name = "colMigrateToBos"
        Me.colMigrateToBos.ReadOnly = True
        Me.colMigrateToBos.Width = 150
        '
        'Brand_Name
        '
        Me.Brand_Name.DataPropertyName = "Brand_Name"
        Me.Brand_Name.HeaderText = "Project"
        Me.Brand_Name.Name = "Brand_Name"
        Me.Brand_Name.ReadOnly = True
        '
        'frmServiceQueue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 532)
        Me.ControlBox = False
        Me.Controls.Add(Me.FLPAllService)
        Me.Controls.Add(Me.TabMessagePage)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmServiceQueue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Service"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.nudWait, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabMessagePage.ResumeLayout(False)
        Me.TabServicePage.ResumeLayout(False)
        Me.GroupBoxHistory.ResumeLayout(False)
        CType(Me.GridItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnQuickService.ResumeLayout(False)
        Me.pnQuickService.PerformLayout()
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxView.ResumeLayout(False)
        CType(Me.GridWait, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabViewPage.ResumeLayout(False)
        Me.GroupBoxViewCustomerEnd.ResumeLayout(False)
        Me.GroupBoxViewCustomerEnd.PerformLayout()
        CType(Me.GridEnd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbltype As System.Windows.Forms.Label
    Friend WithEvents lblQueue As System.Windows.Forms.Label
    Friend WithEvents Label03 As System.Windows.Forms.Label
    Friend WithEvents Label01 As System.Windows.Forms.Label
    Public WithEvents btnCall As System.Windows.Forms.Button
    Public WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtDisplayQueue As System.Windows.Forms.TextBox
    Friend WithEvents TabMessagePage As System.Windows.Forms.TabControl
    Friend WithEvents TabServicePage As System.Windows.Forms.TabPage
    Friend WithEvents TabViewPage As System.Windows.Forms.TabPage
    Friend WithEvents btnRefreshWait As System.Windows.Forms.Button
    Friend WithEvents Label04 As System.Windows.Forms.Label
    Friend WithEvents CheckTimerWait As System.Windows.Forms.CheckBox
    Friend WithEvents nudWait As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBoxHistory As CodeVendor.Controls.Grouper
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents GroupBoxViewCustomerEnd As CodeVendor.Controls.Grouper
    Friend WithEvents GroupBoxView As CodeVendor.Controls.Grouper
    Friend WithEvents timerRefreshWait As System.Windows.Forms.Timer
    Friend WithEvents GridEnd As System.Windows.Forms.DataGridView
    Friend WithEvents txtCustomerTypeID As System.Windows.Forms.TextBox
    Friend WithEvents lblCountCustomer As System.Windows.Forms.Label
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents pnQuickService As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtQueue As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCompactView As System.Windows.Forms.Button
    Friend WithEvents btnMessage As System.Windows.Forms.Button
    Friend WithEvents Grouper2 As CodeVendor.Controls.Grouper
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents ComboH As System.Windows.Forms.ComboBox
    Friend WithEvents ComboM As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblQueue_msg As System.Windows.Forms.Label
    Friend WithEvents lblCustomerID_msg As System.Windows.Forms.Label
    Friend WithEvents Grouper4 As CodeVendor.Controls.Grouper
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents customertype_name_msg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_id_msg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents queue_no_msg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExpand As System.Windows.Forms.Button
    Friend WithEvents FLP As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GridWait As System.Windows.Forms.DataGridView
    Friend WithEvents GridItem As System.Windows.Forms.DataGridView
    Friend WithEvents lblRoomName As System.Windows.Forms.Label
    Friend WithEvents pb1 As System.Windows.Forms.PictureBox
    Friend WithEvents pb2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbItem As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents FLPAllService As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblServe As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnCam As System.Windows.Forms.Button
    Friend WithEvents Use_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents xxx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Segment_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents service As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Waiting_T As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents served_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalWaiting As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents row As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GridEnd_colNetwork As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServiceTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serve As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents queue_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Segment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hold As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents totaltime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ck_hold As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ck_misscall As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents register As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serve_custype As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serve_custype_allow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents priority_value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cancel As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents colNetworkType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMigrateToBos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Brand_Name As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
