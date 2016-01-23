<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmService
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmService))
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.item_code = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_name_th = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_order = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TB_type_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_wait = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txt_queue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.color = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.q_type_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.itemtype_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.active_status = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.app_min_queue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.app_max_queue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_url = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.master_itemid = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.cmbRefMasterItem = New System.Windows.Forms.ComboBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtUrl = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtItemOrder = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMax = New System.Windows.Forms.TextBox
        Me.nudWait = New System.Windows.Forms.NumericUpDown
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMin = New System.Windows.Forms.TextBox
        Me.lblColor = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtName_th = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbQueue = New System.Windows.Forms.ComboBox
        Me.pg = New System.Windows.Forms.PictureBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnClearIcon = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtQueue = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.nudTime = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbInActive = New System.Windows.Forms.CheckBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Grouper1 = New CodeVendor.Controls.Grouper
        Me.btnOrder = New System.Windows.Forms.Button
        Me.cbSearch = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.ColorPicker = New System.Windows.Forms.ColorDialog
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxButton.SuspendLayout()
        CType(Me.nudWait, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.pg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grouper1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtID.Location = New System.Drawing.Point(303, 54)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(56, 24)
        Me.txtID.TabIndex = 15
        Me.txtID.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Enabled = False
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtCode.Location = New System.Drawing.Point(190, 54)
        Me.txtCode.MaxLength = 20
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(169, 24)
        Me.txtCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(99, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Item Code :"
        '
        'Grid
        '
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToDeleteRows = False
        Me.Grid.AllowUserToOrderColumns = True
        Me.Grid.AllowUserToResizeColumns = False
        Me.Grid.AllowUserToResizeRows = False
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.item_code, Me.item_name, Me.item_name_th, Me.item_order, Me.TB_type_name, Me.item_time, Me.item_wait, Me.txt_queue, Me.color, Me.q_type_id, Me.itemtype_id, Me.id, Me.active_status, Me.status, Me.app_min_queue, Me.app_max_queue, Me.item_url, Me.master_itemid})
        Me.Grid.Location = New System.Drawing.Point(9, 49)
        Me.Grid.MultiSelect = False
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.Size = New System.Drawing.Size(844, 324)
        Me.Grid.TabIndex = 5
        '
        'item_code
        '
        Me.item_code.DataPropertyName = "item_code"
        Me.item_code.FillWeight = 28.24859!
        Me.item_code.HeaderText = "Code"
        Me.item_code.Name = "item_code"
        Me.item_code.ReadOnly = True
        Me.item_code.Width = 70
        '
        'item_name
        '
        Me.item_name.DataPropertyName = "item_name"
        Me.item_name.FillWeight = 171.7514!
        Me.item_name.HeaderText = "Name (EN)"
        Me.item_name.Name = "item_name"
        Me.item_name.ReadOnly = True
        Me.item_name.Width = 185
        '
        'item_name_th
        '
        Me.item_name_th.DataPropertyName = "item_name_th"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.item_name_th.DefaultCellStyle = DataGridViewCellStyle1
        Me.item_name_th.HeaderText = "Name (TH)"
        Me.item_name_th.Name = "item_name_th"
        Me.item_name_th.ReadOnly = True
        Me.item_name_th.Width = 185
        '
        'item_order
        '
        Me.item_order.DataPropertyName = "item_order"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.item_order.DefaultCellStyle = DataGridViewCellStyle2
        Me.item_order.HeaderText = "Order"
        Me.item_order.Name = "item_order"
        Me.item_order.ReadOnly = True
        Me.item_order.Width = 70
        '
        'TB_type_name
        '
        Me.TB_type_name.DataPropertyName = "TB_type_name"
        Me.TB_type_name.HeaderText = "Queue Type"
        Me.TB_type_name.Name = "TB_type_name"
        Me.TB_type_name.ReadOnly = True
        Me.TB_type_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TB_type_name.Visible = False
        '
        'item_time
        '
        Me.item_time.DataPropertyName = "item_time"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.NullValue = Nothing
        Me.item_time.DefaultCellStyle = DataGridViewCellStyle3
        Me.item_time.HeaderText = "Std. H.T"
        Me.item_time.Name = "item_time"
        Me.item_time.ReadOnly = True
        Me.item_time.Width = 85
        '
        'item_wait
        '
        Me.item_wait.DataPropertyName = "item_wait"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.item_wait.DefaultCellStyle = DataGridViewCellStyle4
        Me.item_wait.HeaderText = "Std. W.T"
        Me.item_wait.Name = "item_wait"
        Me.item_wait.ReadOnly = True
        Me.item_wait.Width = 90
        '
        'txt_queue
        '
        Me.txt_queue.DataPropertyName = "txt_queue"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.txt_queue.DefaultCellStyle = DataGridViewCellStyle5
        Me.txt_queue.HeaderText = "Text"
        Me.txt_queue.Name = "txt_queue"
        Me.txt_queue.ReadOnly = True
        Me.txt_queue.Width = 60
        '
        'color
        '
        Me.color.DataPropertyName = "color"
        Me.color.HeaderText = "color"
        Me.color.Name = "color"
        Me.color.ReadOnly = True
        Me.color.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.color.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.color.Visible = False
        Me.color.Width = 50
        '
        'q_type_id
        '
        Me.q_type_id.DataPropertyName = "q_type_id"
        Me.q_type_id.HeaderText = "q_type_id"
        Me.q_type_id.Name = "q_type_id"
        Me.q_type_id.ReadOnly = True
        Me.q_type_id.Visible = False
        '
        'itemtype_id
        '
        Me.itemtype_id.DataPropertyName = "itemtype_id"
        Me.itemtype_id.HeaderText = "itemtype_id"
        Me.itemtype_id.Name = "itemtype_id"
        Me.itemtype_id.ReadOnly = True
        Me.itemtype_id.Visible = False
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'active_status
        '
        Me.active_status.DataPropertyName = "active_status"
        Me.active_status.HeaderText = "Active"
        Me.active_status.Name = "active_status"
        Me.active_status.ReadOnly = True
        Me.active_status.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.active_status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.active_status.Visible = False
        Me.active_status.Width = 50
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.status.DefaultCellStyle = DataGridViewCellStyle6
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Width = 80
        '
        'app_min_queue
        '
        Me.app_min_queue.DataPropertyName = "app_min_queue"
        Me.app_min_queue.HeaderText = "app_min_queue"
        Me.app_min_queue.Name = "app_min_queue"
        Me.app_min_queue.ReadOnly = True
        Me.app_min_queue.Visible = False
        '
        'app_max_queue
        '
        Me.app_max_queue.DataPropertyName = "app_max_queue"
        Me.app_max_queue.HeaderText = "app_max_queue"
        Me.app_max_queue.Name = "app_max_queue"
        Me.app_max_queue.ReadOnly = True
        Me.app_max_queue.Visible = False
        '
        'item_url
        '
        Me.item_url.DataPropertyName = "item_url"
        Me.item_url.HeaderText = "item_url"
        Me.item_url.Name = "item_url"
        Me.item_url.ReadOnly = True
        Me.item_url.Visible = False
        '
        'master_itemid
        '
        Me.master_itemid.DataPropertyName = "master_itemid"
        Me.master_itemid.HeaderText = "master_itemid"
        Me.master_itemid.Name = "master_itemid"
        Me.master_itemid.ReadOnly = True
        Me.master_itemid.Visible = False
        '
        'GroupBoxButton
        '
        Me.GroupBoxButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxButton.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxButton.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBoxButton.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxButton.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxButton.BorderThickness = 1.0!
        Me.GroupBoxButton.Controls.Add(Me.cmbRefMasterItem)
        Me.GroupBoxButton.Controls.Add(Me.Label20)
        Me.GroupBoxButton.Controls.Add(Me.Label19)
        Me.GroupBoxButton.Controls.Add(Me.Label18)
        Me.GroupBoxButton.Controls.Add(Me.txtUrl)
        Me.GroupBoxButton.Controls.Add(Me.Label17)
        Me.GroupBoxButton.Controls.Add(Me.txtItemOrder)
        Me.GroupBoxButton.Controls.Add(Me.Label15)
        Me.GroupBoxButton.Controls.Add(Me.Label16)
        Me.GroupBoxButton.Controls.Add(Me.Label13)
        Me.GroupBoxButton.Controls.Add(Me.txtMax)
        Me.GroupBoxButton.Controls.Add(Me.nudWait)
        Me.GroupBoxButton.Controls.Add(Me.Label14)
        Me.GroupBoxButton.Controls.Add(Me.txtMin)
        Me.GroupBoxButton.Controls.Add(Me.lblColor)
        Me.GroupBoxButton.Controls.Add(Me.Label12)
        Me.GroupBoxButton.Controls.Add(Me.Label11)
        Me.GroupBoxButton.Controls.Add(Me.txtName_th)
        Me.GroupBoxButton.Controls.Add(Me.Panel1)
        Me.GroupBoxButton.Controls.Add(Me.Label8)
        Me.GroupBoxButton.Controls.Add(Me.txtQueue)
        Me.GroupBoxButton.Controls.Add(Me.Label6)
        Me.GroupBoxButton.Controls.Add(Me.nudTime)
        Me.GroupBoxButton.Controls.Add(Me.Label7)
        Me.GroupBoxButton.Controls.Add(Me.cbInActive)
        Me.GroupBoxButton.Controls.Add(Me.txtID)
        Me.GroupBoxButton.Controls.Add(Me.btnCancel)
        Me.GroupBoxButton.Controls.Add(Me.txtCode)
        Me.GroupBoxButton.Controls.Add(Me.btnAdd)
        Me.GroupBoxButton.Controls.Add(Me.Label1)
        Me.GroupBoxButton.Controls.Add(Me.Label2)
        Me.GroupBoxButton.Controls.Add(Me.btnSave)
        Me.GroupBoxButton.Controls.Add(Me.txtName)
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
        Me.GroupBoxButton.Size = New System.Drawing.Size(866, 313)
        Me.GroupBoxButton.TabIndex = 24
        '
        'cmbRefMasterItem
        '
        Me.cmbRefMasterItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRefMasterItem.Enabled = False
        Me.cmbRefMasterItem.FormattingEnabled = True
        Me.cmbRefMasterItem.Location = New System.Drawing.Point(190, 25)
        Me.cmbRefMasterItem.Name = "cmbRefMasterItem"
        Me.cmbRefMasterItem.Size = New System.Drawing.Size(254, 26)
        Me.cmbRefMasterItem.TabIndex = 69
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label20.Location = New System.Drawing.Point(89, 28)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(94, 18)
        Me.Label20.TabIndex = 68
        Me.Label20.Text = "Master Item :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(148, 318)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(123, 18)
        Me.Label19.TabIndex = 67
        Me.Label19.Text = "XXX = Mobile No"
        Me.Label19.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(32, 318)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 18)
        Me.Label18.TabIndex = 66
        Me.Label18.Text = "URL :"
        Me.Label18.Visible = False
        '
        'txtUrl
        '
        Me.txtUrl.Enabled = False
        Me.txtUrl.Location = New System.Drawing.Point(84, 306)
        Me.txtUrl.MaxLength = 500
        Me.txtUrl.Multiline = True
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(53, 57)
        Me.txtUrl.TabIndex = 65
        Me.txtUrl.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(99, 165)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(86, 18)
        Me.Label17.TabIndex = 64
        Me.Label17.Text = "Item Order :"
        '
        'txtItemOrder
        '
        Me.txtItemOrder.Enabled = False
        Me.txtItemOrder.Location = New System.Drawing.Point(190, 162)
        Me.txtItemOrder.MaxLength = 3
        Me.txtItemOrder.Name = "txtItemOrder"
        Me.txtItemOrder.Size = New System.Drawing.Size(63, 24)
        Me.txtItemOrder.TabIndex = 63
        Me.txtItemOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(258, 111)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(13, 18)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "-"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 111)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(170, 18)
        Me.Label16.TabIndex = 61
        Me.Label16.Text = "Appointment Queue No :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label13.Location = New System.Drawing.Point(673, 140)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 18)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "Minute"
        '
        'txtMax
        '
        Me.txtMax.Enabled = False
        Me.txtMax.Location = New System.Drawing.Point(276, 108)
        Me.txtMax.MaxLength = 3
        Me.txtMax.Name = "txtMax"
        Me.txtMax.Size = New System.Drawing.Size(63, 24)
        Me.txtMax.TabIndex = 8
        Me.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'nudWait
        '
        Me.nudWait.Enabled = False
        Me.nudWait.Location = New System.Drawing.Point(589, 138)
        Me.nudWait.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nudWait.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudWait.Name = "nudWait"
        Me.nudWait.Size = New System.Drawing.Size(78, 24)
        Me.nudWait.TabIndex = 4
        Me.nudWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudWait.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label14.Location = New System.Drawing.Point(418, 140)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(169, 18)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Standard Waiting  Time :"
        '
        'txtMin
        '
        Me.txtMin.Enabled = False
        Me.txtMin.Location = New System.Drawing.Point(190, 108)
        Me.txtMin.MaxLength = 3
        Me.txtMin.Name = "txtMin"
        Me.txtMin.Size = New System.Drawing.Size(63, 24)
        Me.txtMin.TabIndex = 7
        Me.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblColor
        '
        Me.lblColor.BackColor = System.Drawing.Color.White
        Me.lblColor.Location = New System.Drawing.Point(191, 190)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(62, 24)
        Me.lblColor.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(130, 193)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 18)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "Color :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label11.Location = New System.Drawing.Point(452, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(135, 18)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Item Name In Thai :"
        '
        'txtName_th
        '
        Me.txtName_th.Enabled = False
        Me.txtName_th.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtName_th.Location = New System.Drawing.Point(589, 81)
        Me.txtName_th.MaxLength = 50
        Me.txtName_th.Name = "txtName_th"
        Me.txtName_th.Size = New System.Drawing.Size(254, 24)
        Me.txtName_th.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cbQueue)
        Me.Panel1.Controls.Add(Me.pg)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.btnClearIcon)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Location = New System.Drawing.Point(23, 238)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(164, 62)
        Me.Panel1.TabIndex = 51
        Me.Panel1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(-3, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 18)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Icon 16x16 :"
        Me.Label5.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 18)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Queue Type :"
        Me.Label3.Visible = False
        '
        'cbQueue
        '
        Me.cbQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbQueue.Enabled = False
        Me.cbQueue.FormattingEnabled = True
        Me.cbQueue.Location = New System.Drawing.Point(12, 8)
        Me.cbQueue.Name = "cbQueue"
        Me.cbQueue.Size = New System.Drawing.Size(169, 26)
        Me.cbQueue.TabIndex = 2
        '
        'pg
        '
        Me.pg.BackColor = System.Drawing.Color.White
        Me.pg.Enabled = False
        Me.pg.Location = New System.Drawing.Point(178, 33)
        Me.pg.Name = "pg"
        Me.pg.Size = New System.Drawing.Size(16, 16)
        Me.pg.TabIndex = 41
        Me.pg.TabStop = False
        Me.pg.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(180, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(13, 18)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "-"
        Me.Label9.Visible = False
        '
        'btnClearIcon
        '
        Me.btnClearIcon.BackgroundImage = CType(resources.GetObject("btnClearIcon.BackgroundImage"), System.Drawing.Image)
        Me.btnClearIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearIcon.Enabled = False
        Me.btnClearIcon.Location = New System.Drawing.Point(200, 31)
        Me.btnClearIcon.Name = "btnClearIcon"
        Me.btnClearIcon.Size = New System.Drawing.Size(20, 20)
        Me.btnClearIcon.TabIndex = 42
        Me.btnClearIcon.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClearIcon.UseVisualStyleBackColor = True
        Me.btnClearIcon.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(28, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 18)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Queue No :"
        Me.Label10.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(92, 135)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 18)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Text Queue :"
        '
        'txtQueue
        '
        Me.txtQueue.Enabled = False
        Me.txtQueue.Location = New System.Drawing.Point(190, 135)
        Me.txtQueue.MaxLength = 1
        Me.txtQueue.Name = "txtQueue"
        Me.txtQueue.Size = New System.Drawing.Size(63, 24)
        Me.txtQueue.TabIndex = 6
        Me.txtQueue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label6.Location = New System.Drawing.Point(673, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 18)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Minute"
        '
        'nudTime
        '
        Me.nudTime.Enabled = False
        Me.nudTime.Location = New System.Drawing.Point(589, 112)
        Me.nudTime.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nudTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudTime.Name = "nudTime"
        Me.nudTime.Size = New System.Drawing.Size(78, 24)
        Me.nudTime.TabIndex = 3
        Me.nudTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudTime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label7.Location = New System.Drawing.Point(414, 114)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(173, 18)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Standard Handling Time :"
        '
        'cbInActive
        '
        Me.cbInActive.AutoSize = True
        Me.cbInActive.Enabled = False
        Me.cbInActive.Location = New System.Drawing.Point(190, 217)
        Me.cbInActive.Name = "cbInActive"
        Me.cbInActive.Size = New System.Drawing.Size(66, 22)
        Me.cbInActive.TabIndex = 9
        Me.cbInActive.Text = "Active"
        Me.cbInActive.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = Global.QueueSharp.My.Resources.Resources.Close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(484, 258)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 40)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAdd.Image = Global.QueueSharp.My.Resources.Resources.Add
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(292, 258)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 40)
        Me.btnAdd.TabIndex = 10
        Me.btnAdd.Text = "Add"
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Item Name In English:"
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(388, 258)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 40)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtName.Location = New System.Drawing.Point(190, 81)
        Me.txtName.MaxLength = 50
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(254, 24)
        Me.txtName.TabIndex = 1
        '
        'Grouper1
        '
        Me.Grouper1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper1.BackgroundColor = System.Drawing.Color.White
        Me.Grouper1.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.Grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper1.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper1.BorderThickness = 1.0!
        Me.Grouper1.Controls.Add(Me.btnOrder)
        Me.Grouper1.Controls.Add(Me.cbSearch)
        Me.Grouper1.Controls.Add(Me.Label4)
        Me.Grouper1.Controls.Add(Me.txtSearch)
        Me.Grouper1.Controls.Add(Me.Grid)
        Me.Grouper1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Grouper1.GroupImage = Nothing
        Me.Grouper1.GroupTitle = ""
        Me.Grouper1.Location = New System.Drawing.Point(6, 304)
        Me.Grouper1.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Grouper1.Name = "Grouper1"
        Me.Grouper1.Padding = New System.Windows.Forms.Padding(20)
        Me.Grouper1.PaintGroupBox = False
        Me.Grouper1.RoundCorners = 10
        Me.Grouper1.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper1.ShadowControl = True
        Me.Grouper1.ShadowThickness = 3
        Me.Grouper1.Size = New System.Drawing.Size(866, 383)
        Me.Grouper1.TabIndex = 25
        '
        'btnOrder
        '
        Me.btnOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOrder.Location = New System.Drawing.Point(460, 17)
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Size = New System.Drawing.Size(102, 26)
        Me.btnOrder.TabIndex = 56
        Me.btnOrder.Text = "ReOrder"
        Me.btnOrder.UseVisualStyleBackColor = True
        '
        'cbSearch
        '
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Items.AddRange(New Object() {"All", "Active", "Inactive"})
        Me.cbSearch.Location = New System.Drawing.Point(331, 18)
        Me.cbSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(105, 26)
        Me.cbSearch.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 18)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Search :"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(74, 18)
        Me.txtSearch.MaxLength = 100
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(250, 24)
        Me.txtSearch.TabIndex = 18
        '
        'frmService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(877, 689)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Controls.Add(Me.Grouper1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmService"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Service"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        CType(Me.nudWait, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Grouper1.ResumeLayout(False)
        Me.Grouper1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper1 As CodeVendor.Controls.Grouper
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cbInActive As System.Windows.Forms.CheckBox
    Friend WithEvents cbQueue As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents nudTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pg As System.Windows.Forms.PictureBox
    Friend WithEvents btnClearIcon As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtQueue As System.Windows.Forms.TextBox
    Friend WithEvents txtMax As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtMin As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtName_th As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ColorPicker As System.Windows.Forms.ColorDialog
    Friend WithEvents btnOrder As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents nudWait As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtItemOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbRefMasterItem As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents item_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_th As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_order As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TB_type_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_wait As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txt_queue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents q_type_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itemtype_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents active_status As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents app_min_queue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents app_max_queue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_url As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents master_itemid As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
