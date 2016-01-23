<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerType))
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.lblQueue = New System.Windows.Forms.Label
        Me.txtQueue = New System.Windows.Forms.TextBox
        Me.cbDef = New System.Windows.Forms.CheckBox
        Me.cbApp = New System.Windows.Forms.CheckBox
        Me.txtMax = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtMin = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblColor = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbActive = New System.Windows.Forms.CheckBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Grouper1 = New CodeVendor.Controls.Grouper
        Me.cbSearch = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.ColorPicker = New System.Windows.Forms.ColorDialog
        Me.cmbMasterCustomertype = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.customertype_code = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customertype_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txt_queue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.min_queue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.max_queue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.color = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lated_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.before_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.app = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.def = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.active_status = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.master_customertypeid = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxButton.SuspendLayout()
        Me.Grouper1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtID.Location = New System.Drawing.Point(316, 49)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(56, 24)
        Me.txtID.TabIndex = 15
        Me.txtID.Visible = False
        '
        'txtName
        '
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtName.Location = New System.Drawing.Point(179, 77)
        Me.txtName.MaxLength = 50
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(294, 24)
        Me.txtName.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.Enabled = False
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtCode.Location = New System.Drawing.Point(179, 49)
        Me.txtCode.MaxLength = 20
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(131, 24)
        Me.txtCode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(116, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(122, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Code :"
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
        Me.Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.customertype_code, Me.customertype_name, Me.txt_queue, Me.min_queue, Me.max_queue, Me.id, Me.color, Me.lated_time, Me.before_time, Me.app, Me.def, Me.active_status, Me.status, Me.master_customertypeid})
        Me.Grid.Location = New System.Drawing.Point(9, 52)
        Me.Grid.MultiSelect = False
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.Size = New System.Drawing.Size(551, 213)
        Me.Grid.TabIndex = 11
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
        Me.GroupBoxButton.Controls.Add(Me.Label5)
        Me.GroupBoxButton.Controls.Add(Me.cmbMasterCustomertype)
        Me.GroupBoxButton.Controls.Add(Me.lblQueue)
        Me.GroupBoxButton.Controls.Add(Me.txtQueue)
        Me.GroupBoxButton.Controls.Add(Me.cbDef)
        Me.GroupBoxButton.Controls.Add(Me.cbApp)
        Me.GroupBoxButton.Controls.Add(Me.txtMax)
        Me.GroupBoxButton.Controls.Add(Me.Label9)
        Me.GroupBoxButton.Controls.Add(Me.txtMin)
        Me.GroupBoxButton.Controls.Add(Me.Label10)
        Me.GroupBoxButton.Controls.Add(Me.lblColor)
        Me.GroupBoxButton.Controls.Add(Me.Label4)
        Me.GroupBoxButton.Controls.Add(Me.cbActive)
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
        Me.GroupBoxButton.Size = New System.Drawing.Size(573, 279)
        Me.GroupBoxButton.TabIndex = 24
        '
        'lblQueue
        '
        Me.lblQueue.AutoSize = True
        Me.lblQueue.Enabled = False
        Me.lblQueue.Location = New System.Drawing.Point(294, 159)
        Me.lblQueue.Name = "lblQueue"
        Me.lblQueue.Size = New System.Drawing.Size(92, 18)
        Me.lblQueue.TabIndex = 59
        Me.lblQueue.Text = "Text Queue :"
        '
        'txtQueue
        '
        Me.txtQueue.Enabled = False
        Me.txtQueue.Location = New System.Drawing.Point(391, 156)
        Me.txtQueue.MaxLength = 1
        Me.txtQueue.Name = "txtQueue"
        Me.txtQueue.Size = New System.Drawing.Size(56, 24)
        Me.txtQueue.TabIndex = 58
        Me.txtQueue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbDef
        '
        Me.cbDef.AutoSize = True
        Me.cbDef.Enabled = False
        Me.cbDef.Location = New System.Drawing.Point(179, 182)
        Me.cbDef.Name = "cbDef"
        Me.cbDef.Size = New System.Drawing.Size(179, 22)
        Me.cbDef.TabIndex = 57
        Me.cbDef.Text = "Default Customer Type"
        Me.cbDef.UseVisualStyleBackColor = True
        '
        'cbApp
        '
        Me.cbApp.AutoSize = True
        Me.cbApp.Enabled = False
        Me.cbApp.Location = New System.Drawing.Point(179, 159)
        Me.cbApp.Name = "cbApp"
        Me.cbApp.Size = New System.Drawing.Size(109, 22)
        Me.cbApp.TabIndex = 56
        Me.cbApp.Text = "Appointment"
        Me.cbApp.UseVisualStyleBackColor = True
        '
        'txtMax
        '
        Me.txtMax.Enabled = False
        Me.txtMax.Location = New System.Drawing.Point(266, 105)
        Me.txtMax.MaxLength = 3
        Me.txtMax.Name = "txtMax"
        Me.txtMax.Size = New System.Drawing.Size(63, 24)
        Me.txtMax.TabIndex = 53
        Me.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(247, 108)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(13, 18)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "-"
        '
        'txtMin
        '
        Me.txtMin.Enabled = False
        Me.txtMin.Location = New System.Drawing.Point(179, 105)
        Me.txtMin.MaxLength = 3
        Me.txtMin.Name = "txtMin"
        Me.txtMin.Size = New System.Drawing.Size(63, 24)
        Me.txtMin.TabIndex = 52
        Me.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(88, 108)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 18)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Queue No :"
        '
        'lblColor
        '
        Me.lblColor.BackColor = System.Drawing.Color.White
        Me.lblColor.Location = New System.Drawing.Point(180, 134)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(50, 20)
        Me.lblColor.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(120, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 18)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Color :"
        '
        'cbActive
        '
        Me.cbActive.AutoSize = True
        Me.cbActive.Enabled = False
        Me.cbActive.Location = New System.Drawing.Point(179, 205)
        Me.cbActive.Name = "cbActive"
        Me.cbActive.Size = New System.Drawing.Size(66, 22)
        Me.cbActive.TabIndex = 7
        Me.cbActive.Text = "Active"
        Me.cbActive.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = Global.QueueSharp.My.Resources.Resources.Close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(371, 228)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 40)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAdd.Image = Global.QueueSharp.My.Resources.Resources.Add
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(179, 228)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 40)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "Add"
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(275, 228)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 40)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
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
        Me.Grouper1.Controls.Add(Me.cbSearch)
        Me.Grouper1.Controls.Add(Me.Label3)
        Me.Grouper1.Controls.Add(Me.txtSearch)
        Me.Grouper1.Controls.Add(Me.Grid)
        Me.Grouper1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Grouper1.GroupImage = Nothing
        Me.Grouper1.GroupTitle = ""
        Me.Grouper1.Location = New System.Drawing.Point(6, 270)
        Me.Grouper1.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Grouper1.Name = "Grouper1"
        Me.Grouper1.Padding = New System.Windows.Forms.Padding(20)
        Me.Grouper1.PaintGroupBox = False
        Me.Grouper1.RoundCorners = 10
        Me.Grouper1.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper1.ShadowControl = True
        Me.Grouper1.ShadowThickness = 3
        Me.Grouper1.Size = New System.Drawing.Size(573, 277)
        Me.Grouper1.TabIndex = 25
        '
        'cbSearch
        '
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Items.AddRange(New Object() {"All", "Active", "Inactive"})
        Me.cbSearch.Location = New System.Drawing.Point(318, 18)
        Me.cbSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(140, 26)
        Me.cbSearch.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 18)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Search :"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(75, 19)
        Me.txtSearch.MaxLength = 100
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(237, 24)
        Me.txtSearch.TabIndex = 9
        '
        'ColorPicker
        '
        Me.ColorPicker.AllowFullOpen = False
        '
        'cmbMasterCustomertype
        '
        Me.cmbMasterCustomertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMasterCustomertype.Enabled = False
        Me.cmbMasterCustomertype.FormattingEnabled = True
        Me.cmbMasterCustomertype.Location = New System.Drawing.Point(179, 19)
        Me.cmbMasterCustomertype.Name = "cmbMasterCustomertype"
        Me.cmbMasterCustomertype.Size = New System.Drawing.Size(294, 26)
        Me.cmbMasterCustomertype.TabIndex = 60
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(168, 18)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Master Customer Type :"
        '
        'customertype_code
        '
        Me.customertype_code.DataPropertyName = "customertype_code"
        Me.customertype_code.FillWeight = 5.076141!
        Me.customertype_code.HeaderText = "Code"
        Me.customertype_code.Name = "customertype_code"
        Me.customertype_code.ReadOnly = True
        Me.customertype_code.Width = 140
        '
        'customertype_name
        '
        Me.customertype_name.DataPropertyName = "customertype_name"
        Me.customertype_name.FillWeight = 194.9239!
        Me.customertype_name.HeaderText = "Name"
        Me.customertype_name.Name = "customertype_name"
        Me.customertype_name.ReadOnly = True
        Me.customertype_name.Width = 305
        '
        'txt_queue
        '
        Me.txt_queue.DataPropertyName = "txt_queue"
        Me.txt_queue.HeaderText = "txt_queue"
        Me.txt_queue.Name = "txt_queue"
        Me.txt_queue.ReadOnly = True
        Me.txt_queue.Visible = False
        '
        'min_queue
        '
        Me.min_queue.DataPropertyName = "min_queue"
        Me.min_queue.HeaderText = "min_queue"
        Me.min_queue.Name = "min_queue"
        Me.min_queue.ReadOnly = True
        Me.min_queue.Visible = False
        '
        'max_queue
        '
        Me.max_queue.DataPropertyName = "max_queue"
        Me.max_queue.HeaderText = "max_queue"
        Me.max_queue.Name = "max_queue"
        Me.max_queue.ReadOnly = True
        Me.max_queue.Visible = False
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'color
        '
        Me.color.DataPropertyName = "color"
        Me.color.HeaderText = "color"
        Me.color.Name = "color"
        Me.color.ReadOnly = True
        Me.color.Visible = False
        '
        'lated_time
        '
        Me.lated_time.DataPropertyName = "lated_time"
        Me.lated_time.HeaderText = "lated_time"
        Me.lated_time.Name = "lated_time"
        Me.lated_time.ReadOnly = True
        Me.lated_time.Visible = False
        '
        'before_time
        '
        Me.before_time.DataPropertyName = "before_time"
        Me.before_time.HeaderText = "before_time"
        Me.before_time.Name = "before_time"
        Me.before_time.ReadOnly = True
        Me.before_time.Visible = False
        '
        'app
        '
        Me.app.DataPropertyName = "app"
        Me.app.HeaderText = "app"
        Me.app.Name = "app"
        Me.app.ReadOnly = True
        Me.app.Visible = False
        '
        'def
        '
        Me.def.DataPropertyName = "def"
        Me.def.HeaderText = "def"
        Me.def.Name = "def"
        Me.def.ReadOnly = True
        Me.def.Visible = False
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
        Me.active_status.Width = 80
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.status.DefaultCellStyle = DataGridViewCellStyle1
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Width = 80
        '
        'master_customertypeid
        '
        Me.master_customertypeid.DataPropertyName = "master_customertypeid"
        Me.master_customertypeid.HeaderText = "master_customertypeid"
        Me.master_customertypeid.Name = "master_customertypeid"
        Me.master_customertypeid.ReadOnly = True
        Me.master_customertypeid.Visible = False
        '
        'frmCustomerType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(583, 550)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Controls.Add(Me.Grouper1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCustomerType"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Type"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        Me.Grouper1.ResumeLayout(False)
        Me.Grouper1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper1 As CodeVendor.Controls.Grouper
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cbActive As System.Windows.Forms.CheckBox
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents txtMax As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtMin As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbApp As System.Windows.Forms.CheckBox
    Friend WithEvents cbDef As System.Windows.Forms.CheckBox
    Friend WithEvents lblQueue As System.Windows.Forms.Label
    Friend WithEvents txtQueue As System.Windows.Forms.TextBox
    Friend WithEvents ColorPicker As System.Windows.Forms.ColorDialog
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbMasterCustomertype As System.Windows.Forms.ComboBox
    Friend WithEvents customertype_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txt_queue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents min_queue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents max_queue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lated_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents before_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents app As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents def As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents active_status As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents master_customertypeid As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
