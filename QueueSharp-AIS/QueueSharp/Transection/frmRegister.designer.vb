<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
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
        Me.TabView = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New CodeVendor.Controls.Grouper
        Me.GridCustomer = New System.Windows.Forms.DataGridView
        Me.Queue_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customer_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CustomerType_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.wait_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ordertime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DomainUp = New System.Windows.Forms.NumericUpDown
        Me.ButRefresh = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.CheckTimerView = New System.Windows.Forms.CheckBox
        Me.TabQueue = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New CodeVendor.Controls.Grouper
        Me.Grouper5 = New CodeVendor.Controls.Grouper
        Me.FLPService = New System.Windows.Forms.FlowLayoutPanel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtType = New System.Windows.Forms.TextBox
        Me.Grouper1 = New CodeVendor.Controls.Grouper
        Me.Label3 = New System.Windows.Forms.Label
        Me.pgPrint = New System.Windows.Forms.PictureBox
        Me.ButRePrint = New System.Windows.Forms.Button
        Me.ButOK = New System.Windows.Forms.Button
        Me.CheckPrint = New System.Windows.Forms.CheckBox
        Me.txtMessage = New System.Windows.Forms.Label
        Me.ButtonClear = New System.Windows.Forms.Button
        Me.txtCustomerID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTypeId = New System.Windows.Forms.TextBox
        Me.FLPType = New System.Windows.Forms.FlowLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblType = New System.Windows.Forms.Label
        Me.TabRegister = New System.Windows.Forms.TabControl
        Me.timerRefreshView = New System.Windows.Forms.Timer(Me.components)
        Me.TabView.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GridCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DomainUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabQueue.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Grouper5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Grouper1.SuspendLayout()
        CType(Me.pgPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabRegister.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabView
        '
        Me.TabView.BackColor = System.Drawing.Color.AliceBlue
        Me.TabView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabView.Controls.Add(Me.GroupBox3)
        Me.TabView.Location = New System.Drawing.Point(4, 27)
        Me.TabView.Margin = New System.Windows.Forms.Padding(4)
        Me.TabView.Name = "TabView"
        Me.TabView.Size = New System.Drawing.Size(975, 495)
        Me.TabView.TabIndex = 2
        Me.TabView.Text = "View"
        Me.TabView.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackgroundColor = System.Drawing.Color.White
        Me.GroupBox3.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBox3.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBox3.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBox3.BorderThickness = 1.0!
        Me.GroupBox3.Controls.Add(Me.GridCustomer)
        Me.GroupBox3.Controls.Add(Me.DomainUp)
        Me.GroupBox3.Controls.Add(Me.ButRefresh)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.CheckTimerView)
        Me.GroupBox3.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBox3.GroupImage = Nothing
        Me.GroupBox3.GroupTitle = ""
        Me.GroupBox3.Location = New System.Drawing.Point(6, -2)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBox3.PaintGroupBox = False
        Me.GroupBox3.RoundCorners = 10
        Me.GroupBox3.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBox3.ShadowControl = True
        Me.GroupBox3.ShadowThickness = 3
        Me.GroupBox3.Size = New System.Drawing.Size(965, 493)
        Me.GroupBox3.TabIndex = 34
        '
        'GridCustomer
        '
        Me.GridCustomer.AllowUserToAddRows = False
        Me.GridCustomer.AllowUserToDeleteRows = False
        Me.GridCustomer.AllowUserToOrderColumns = True
        Me.GridCustomer.AllowUserToResizeRows = False
        Me.GridCustomer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Queue_No, Me.customer_id, Me.CustomerType_Name, Me.Time, Me.wait_time, Me.ordertime})
        Me.GridCustomer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridCustomer.Location = New System.Drawing.Point(10, 62)
        Me.GridCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.GridCustomer.MultiSelect = False
        Me.GridCustomer.Name = "GridCustomer"
        Me.GridCustomer.ReadOnly = True
        Me.GridCustomer.RowHeadersVisible = False
        Me.GridCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridCustomer.Size = New System.Drawing.Size(940, 416)
        Me.GridCustomer.TabIndex = 1
        '
        'Queue_No
        '
        Me.Queue_No.DataPropertyName = "Queue_No"
        Me.Queue_No.HeaderText = "Queue No"
        Me.Queue_No.Name = "Queue_No"
        Me.Queue_No.ReadOnly = True
        Me.Queue_No.Width = 120
        '
        'customer_id
        '
        Me.customer_id.DataPropertyName = "customer_id"
        Me.customer_id.HeaderText = "Mobile No"
        Me.customer_id.Name = "customer_id"
        Me.customer_id.ReadOnly = True
        Me.customer_id.Width = 150
        '
        'CustomerType_Name
        '
        Me.CustomerType_Name.DataPropertyName = "CustomerType_Name"
        Me.CustomerType_Name.HeaderText = "Customer Type"
        Me.CustomerType_Name.Name = "CustomerType_Name"
        Me.CustomerType_Name.ReadOnly = True
        Me.CustomerType_Name.Width = 250
        '
        'Time
        '
        Me.Time.DataPropertyName = "Time"
        Me.Time.HeaderText = "Registered Time"
        Me.Time.Name = "Time"
        Me.Time.ReadOnly = True
        Me.Time.Width = 150
        '
        'wait_time
        '
        Me.wait_time.DataPropertyName = "wait_time"
        Me.wait_time.HeaderText = "Total Waiting Time"
        Me.wait_time.Name = "wait_time"
        Me.wait_time.ReadOnly = True
        Me.wait_time.Width = 180
        '
        'ordertime
        '
        Me.ordertime.DataPropertyName = "ordertime"
        Me.ordertime.HeaderText = "ordertime"
        Me.ordertime.Name = "ordertime"
        Me.ordertime.ReadOnly = True
        Me.ordertime.Visible = False
        '
        'DomainUp
        '
        Me.DomainUp.Location = New System.Drawing.Point(279, 26)
        Me.DomainUp.Margin = New System.Windows.Forms.Padding(4)
        Me.DomainUp.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.DomainUp.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.DomainUp.Name = "DomainUp"
        Me.DomainUp.Size = New System.Drawing.Size(62, 24)
        Me.DomainUp.TabIndex = 22
        Me.DomainUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DomainUp.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'ButRefresh
        '
        Me.ButRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ButRefresh.Location = New System.Drawing.Point(10, 22)
        Me.ButRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.ButRefresh.Name = "ButRefresh"
        Me.ButRefresh.Size = New System.Drawing.Size(112, 32)
        Me.ButRefresh.TabIndex = 22
        Me.ButRefresh.Text = "Refresh"
        Me.ButRefresh.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(349, 28)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 18)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "seconds"
        '
        'CheckTimerView
        '
        Me.CheckTimerView.AutoSize = True
        Me.CheckTimerView.Location = New System.Drawing.Point(132, 26)
        Me.CheckTimerView.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckTimerView.Name = "CheckTimerView"
        Me.CheckTimerView.Size = New System.Drawing.Size(145, 22)
        Me.CheckTimerView.TabIndex = 22
        Me.CheckTimerView.Text = "Update data every"
        Me.CheckTimerView.UseVisualStyleBackColor = True
        '
        'TabQueue
        '
        Me.TabQueue.BackColor = System.Drawing.Color.AliceBlue
        Me.TabQueue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabQueue.Controls.Add(Me.GroupBox1)
        Me.TabQueue.Location = New System.Drawing.Point(4, 27)
        Me.TabQueue.Margin = New System.Windows.Forms.Padding(4)
        Me.TabQueue.Name = "TabQueue"
        Me.TabQueue.Padding = New System.Windows.Forms.Padding(4)
        Me.TabQueue.Size = New System.Drawing.Size(975, 495)
        Me.TabQueue.TabIndex = 0
        Me.TabQueue.Text = "Register"
        Me.TabQueue.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackgroundColor = System.Drawing.Color.White
        Me.GroupBox1.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBox1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBox1.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBox1.BorderThickness = 1.0!
        Me.GroupBox1.Controls.Add(Me.Grouper5)
        Me.GroupBox1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBox1.GroupImage = Nothing
        Me.GroupBox1.GroupTitle = ""
        Me.GroupBox1.Location = New System.Drawing.Point(6, -2)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBox1.PaintGroupBox = False
        Me.GroupBox1.RoundCorners = 10
        Me.GroupBox1.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBox1.ShadowControl = True
        Me.GroupBox1.ShadowThickness = 3
        Me.GroupBox1.Size = New System.Drawing.Size(965, 488)
        Me.GroupBox1.TabIndex = 100
        '
        'Grouper5
        '
        Me.Grouper5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper5.BackgroundColor = System.Drawing.Color.White
        Me.Grouper5.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.Grouper5.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper5.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper5.BorderThickness = 1.0!
        Me.Grouper5.Controls.Add(Me.FLPService)
        Me.Grouper5.Controls.Add(Me.Panel2)
        Me.Grouper5.Controls.Add(Me.txtType)
        Me.Grouper5.Controls.Add(Me.Grouper1)
        Me.Grouper5.Controls.Add(Me.ButtonClear)
        Me.Grouper5.Controls.Add(Me.txtCustomerID)
        Me.Grouper5.Controls.Add(Me.Label1)
        Me.Grouper5.Controls.Add(Me.txtTypeId)
        Me.Grouper5.Controls.Add(Me.FLPType)
        Me.Grouper5.Controls.Add(Me.Panel1)
        Me.Grouper5.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper5.GroupImage = Nothing
        Me.Grouper5.GroupTitle = ""
        Me.Grouper5.Location = New System.Drawing.Point(15, 15)
        Me.Grouper5.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper5.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper5.Name = "Grouper5"
        Me.Grouper5.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper5.PaintGroupBox = False
        Me.Grouper5.RoundCorners = 10
        Me.Grouper5.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper5.ShadowControl = True
        Me.Grouper5.ShadowThickness = 3
        Me.Grouper5.Size = New System.Drawing.Size(933, 456)
        Me.Grouper5.TabIndex = 100
        '
        'FLPService
        '
        Me.FLPService.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FLPService.AutoScroll = True
        Me.FLPService.AutoSize = True
        Me.FLPService.BackColor = System.Drawing.Color.White
        Me.FLPService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FLPService.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLPService.Location = New System.Drawing.Point(273, 100)
        Me.FLPService.Margin = New System.Windows.Forms.Padding(4)
        Me.FLPService.Name = "FLPService"
        Me.FLPService.Size = New System.Drawing.Size(250, 331)
        Me.FLPService.TabIndex = 80
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(273, 66)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(250, 40)
        Me.Panel2.TabIndex = 81
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(84, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 24)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Service"
        '
        'txtType
        '
        Me.txtType.BackColor = System.Drawing.Color.White
        Me.txtType.Enabled = False
        Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.Location = New System.Drawing.Point(191, 24)
        Me.txtType.Margin = New System.Windows.Forms.Padding(4)
        Me.txtType.MaxLength = 50
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.Size = New System.Drawing.Size(68, 29)
        Me.txtType.TabIndex = 76
        Me.txtType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtType.Visible = False
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
        Me.Grouper1.Controls.Add(Me.Label3)
        Me.Grouper1.Controls.Add(Me.pgPrint)
        Me.Grouper1.Controls.Add(Me.ButRePrint)
        Me.Grouper1.Controls.Add(Me.ButOK)
        Me.Grouper1.Controls.Add(Me.CheckPrint)
        Me.Grouper1.Controls.Add(Me.txtMessage)
        Me.Grouper1.CustomGroupBoxColor = System.Drawing.Color.Transparent
        Me.Grouper1.GroupImage = Nothing
        Me.Grouper1.GroupTitle = ""
        Me.Grouper1.Location = New System.Drawing.Point(540, 15)
        Me.Grouper1.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper1.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper1.Name = "Grouper1"
        Me.Grouper1.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper1.PaintGroupBox = False
        Me.Grouper1.RoundCorners = 10
        Me.Grouper1.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper1.ShadowControl = True
        Me.Grouper1.ShadowThickness = 3
        Me.Grouper1.Size = New System.Drawing.Size(376, 416)
        Me.Grouper1.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(61, 28)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(184, 24)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "Print Queue Ticket"
        '
        'pgPrint
        '
        Me.pgPrint.BackgroundImage = Global.QueueSharp.My.Resources.Resources.printer
        Me.pgPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pgPrint.Location = New System.Drawing.Point(29, 26)
        Me.pgPrint.Name = "pgPrint"
        Me.pgPrint.Size = New System.Drawing.Size(26, 27)
        Me.pgPrint.TabIndex = 65
        Me.pgPrint.TabStop = False
        '
        'ButRePrint
        '
        Me.ButRePrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButRePrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButRePrint.Location = New System.Drawing.Point(189, 60)
        Me.ButRePrint.Margin = New System.Windows.Forms.Padding(4)
        Me.ButRePrint.Name = "ButRePrint"
        Me.ButRePrint.Size = New System.Drawing.Size(173, 78)
        Me.ButRePrint.TabIndex = 64
        Me.ButRePrint.Text = "Reprint"
        Me.ButRePrint.UseVisualStyleBackColor = True
        '
        'ButOK
        '
        Me.ButOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButOK.Location = New System.Drawing.Point(10, 60)
        Me.ButOK.Margin = New System.Windows.Forms.Padding(4)
        Me.ButOK.Name = "ButOK"
        Me.ButOK.Size = New System.Drawing.Size(173, 78)
        Me.ButOK.TabIndex = 60
        Me.ButOK.Text = "Register"
        Me.ButOK.UseVisualStyleBackColor = True
        '
        'CheckPrint
        '
        Me.CheckPrint.AutoSize = True
        Me.CheckPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckPrint.Location = New System.Drawing.Point(8, 33)
        Me.CheckPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckPrint.Name = "CheckPrint"
        Me.CheckPrint.Size = New System.Drawing.Size(15, 14)
        Me.CheckPrint.TabIndex = 62
        Me.CheckPrint.UseVisualStyleBackColor = True
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.BackColor = System.Drawing.Color.White
        Me.txtMessage.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtMessage.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.txtMessage.Location = New System.Drawing.Point(11, 151)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(352, 251)
        Me.txtMessage.TabIndex = 0
        Me.txtMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonClear
        '
        Me.ButtonClear.BackgroundImage = Global.QueueSharp.My.Resources.Resources._3
        Me.ButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonClear.FlatAppearance.BorderSize = 0
        Me.ButtonClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClear.Location = New System.Drawing.Point(395, 24)
        Me.ButtonClear.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(60, 29)
        Me.ButtonClear.TabIndex = 61
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'txtCustomerID
        '
        Me.txtCustomerID.BackColor = System.Drawing.Color.White
        Me.txtCustomerID.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerID.Location = New System.Drawing.Point(191, 24)
        Me.txtCustomerID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerID.MaxLength = 10
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(196, 29)
        Me.txtCustomerID.TabIndex = 79
        Me.txtCustomerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(59, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 24)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Mobile No :"
        '
        'txtTypeId
        '
        Me.txtTypeId.BackColor = System.Drawing.Color.White
        Me.txtTypeId.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTypeId.Location = New System.Drawing.Point(192, 24)
        Me.txtTypeId.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTypeId.MaxLength = 50
        Me.txtTypeId.Name = "txtTypeId"
        Me.txtTypeId.ReadOnly = True
        Me.txtTypeId.Size = New System.Drawing.Size(37, 29)
        Me.txtTypeId.TabIndex = 77
        Me.txtTypeId.Visible = False
        '
        'FLPType
        '
        Me.FLPType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FLPType.AutoScroll = True
        Me.FLPType.AutoSize = True
        Me.FLPType.BackColor = System.Drawing.Color.White
        Me.FLPType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FLPType.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLPType.Location = New System.Drawing.Point(14, 100)
        Me.FLPType.Margin = New System.Windows.Forms.Padding(4)
        Me.FLPType.Name = "FLPType"
        Me.FLPType.Size = New System.Drawing.Size(250, 331)
        Me.FLPType.TabIndex = 75
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkBlue
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblType)
        Me.Panel1.Location = New System.Drawing.Point(14, 66)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 40)
        Me.Panel1.TabIndex = 76
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.Color.White
        Me.lblType.Location = New System.Drawing.Point(48, 5)
        Me.lblType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(152, 24)
        Me.lblType.TabIndex = 56
        Me.lblType.Text = "Customer Type"
        '
        'TabRegister
        '
        Me.TabRegister.Controls.Add(Me.TabQueue)
        Me.TabRegister.Controls.Add(Me.TabView)
        Me.TabRegister.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabRegister.Location = New System.Drawing.Point(0, 0)
        Me.TabRegister.Margin = New System.Windows.Forms.Padding(4)
        Me.TabRegister.Name = "TabRegister"
        Me.TabRegister.SelectedIndex = 0
        Me.TabRegister.Size = New System.Drawing.Size(983, 526)
        Me.TabRegister.TabIndex = 100
        '
        'timerRefreshView
        '
        '
        'frmRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(983, 526)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabRegister)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmRegister"
        Me.Text = "Register"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabView.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.GridCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DomainUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabQueue.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Grouper5.ResumeLayout(False)
        Me.Grouper5.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Grouper1.ResumeLayout(False)
        Me.Grouper1.PerformLayout()
        CType(Me.pgPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabRegister.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabView As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As CodeVendor.Controls.Grouper
    Friend WithEvents GridCustomer As System.Windows.Forms.DataGridView
    Friend WithEvents DomainUp As System.Windows.Forms.NumericUpDown
    Friend WithEvents ButRefresh As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckTimerView As System.Windows.Forms.CheckBox
    Friend WithEvents TabQueue As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper1 As CodeVendor.Controls.Grouper
    Friend WithEvents TabRegister As System.Windows.Forms.TabControl
    Friend WithEvents Grouper5 As CodeVendor.Controls.Grouper
    Friend WithEvents timerRefreshView As System.Windows.Forms.Timer
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents FLPType As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtTypeId As System.Windows.Forms.TextBox
    Friend WithEvents txtMessage As System.Windows.Forms.Label
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
    Friend WithEvents ButRePrint As System.Windows.Forms.Button
    Friend WithEvents ButOK As System.Windows.Forms.Button
    Friend WithEvents CheckPrint As System.Windows.Forms.CheckBox
    Friend WithEvents pgPrint As System.Windows.Forms.PictureBox
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FLPService As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Queue_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomerType_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents wait_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ordertime As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
