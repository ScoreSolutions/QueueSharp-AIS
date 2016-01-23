<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCounter
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCounter))
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.cbCounterManager = New System.Windows.Forms.CheckBox
        Me.cbBackOffice = New System.Windows.Forms.CheckBox
        Me.nudUnitDisplay = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbSpeed = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GridType = New System.Windows.Forms.DataGridView
        Me.customertype_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cb = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.customertype_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customertype_code = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label10 = New System.Windows.Forms.Label
        Me.GridTypeAllow = New System.Windows.Forms.DataGridView
        Me.customertype_id1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cb1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.cbQuick = New System.Windows.Forms.CheckBox
        Me.cbActive = New System.Windows.Forms.CheckBox
        Me.Grouper1 = New CodeVendor.Controls.Grouper
        Me.cbSearch = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cbCounterTablet = New System.Windows.Forms.CheckBox
        Me.counter_code = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.counter_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.active_status = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.quickservice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.update_status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.unitdisplay = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.speed_lane = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.back_office = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.counter_manager = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.counter_tablet = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxButton.SuspendLayout()
        CType(Me.nudUnitDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridTypeAllow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grouper1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtID.Location = New System.Drawing.Point(257, 20)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(56, 24)
        Me.txtID.TabIndex = 15
        Me.txtID.Visible = False
        '
        'txtName
        '
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtName.Location = New System.Drawing.Point(136, 50)
        Me.txtName.MaxLength = 50
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(243, 24)
        Me.txtName.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.Enabled = False
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtCode.Location = New System.Drawing.Point(136, 20)
        Me.txtCode.MaxLength = 20
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(127, 24)
        Me.txtCode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Counter Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Counter Code :"
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
        Me.Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.counter_code, Me.counter_name, Me.id, Me.active_status, Me.status, Me.quickservice, Me.update_status, Me.unitdisplay, Me.speed_lane, Me.back_office, Me.counter_manager, Me.counter_tablet})
        Me.Grid.Location = New System.Drawing.Point(9, 50)
        Me.Grid.MultiSelect = False
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.Size = New System.Drawing.Size(837, 279)
        Me.Grid.TabIndex = 5
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
        Me.GroupBoxButton.Controls.Add(Me.cbCounterTablet)
        Me.GroupBoxButton.Controls.Add(Me.cbCounterManager)
        Me.GroupBoxButton.Controls.Add(Me.cbBackOffice)
        Me.GroupBoxButton.Controls.Add(Me.nudUnitDisplay)
        Me.GroupBoxButton.Controls.Add(Me.Label5)
        Me.GroupBoxButton.Controls.Add(Me.cbSpeed)
        Me.GroupBoxButton.Controls.Add(Me.Label3)
        Me.GroupBoxButton.Controls.Add(Me.GridType)
        Me.GroupBoxButton.Controls.Add(Me.Label10)
        Me.GroupBoxButton.Controls.Add(Me.GridTypeAllow)
        Me.GroupBoxButton.Controls.Add(Me.btnCancel)
        Me.GroupBoxButton.Controls.Add(Me.btnAdd)
        Me.GroupBoxButton.Controls.Add(Me.btnSave)
        Me.GroupBoxButton.Controls.Add(Me.cbQuick)
        Me.GroupBoxButton.Controls.Add(Me.cbActive)
        Me.GroupBoxButton.Controls.Add(Me.txtID)
        Me.GroupBoxButton.Controls.Add(Me.txtCode)
        Me.GroupBoxButton.Controls.Add(Me.Label1)
        Me.GroupBoxButton.Controls.Add(Me.Label2)
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
        Me.GroupBoxButton.Size = New System.Drawing.Size(859, 263)
        Me.GroupBoxButton.TabIndex = 24
        '
        'cbCounterManager
        '
        Me.cbCounterManager.AutoSize = True
        Me.cbCounterManager.Enabled = False
        Me.cbCounterManager.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.cbCounterManager.FlatAppearance.BorderSize = 0
        Me.cbCounterManager.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.cbCounterManager.Location = New System.Drawing.Point(261, 140)
        Me.cbCounterManager.Name = "cbCounterManager"
        Me.cbCounterManager.Size = New System.Drawing.Size(142, 22)
        Me.cbCounterManager.TabIndex = 51
        Me.cbCounterManager.Text = "Counter Manager"
        Me.cbCounterManager.UseVisualStyleBackColor = True
        '
        'cbBackOffice
        '
        Me.cbBackOffice.AutoSize = True
        Me.cbBackOffice.Enabled = False
        Me.cbBackOffice.Location = New System.Drawing.Point(136, 140)
        Me.cbBackOffice.Name = "cbBackOffice"
        Me.cbBackOffice.Size = New System.Drawing.Size(101, 22)
        Me.cbBackOffice.TabIndex = 50
        Me.cbBackOffice.Text = "Back office"
        Me.cbBackOffice.UseVisualStyleBackColor = True
        '
        'nudUnitDisplay
        '
        Me.nudUnitDisplay.Enabled = False
        Me.nudUnitDisplay.Location = New System.Drawing.Point(135, 81)
        Me.nudUnitDisplay.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudUnitDisplay.Name = "nudUnitDisplay"
        Me.nudUnitDisplay.Size = New System.Drawing.Size(67, 24)
        Me.nudUnitDisplay.TabIndex = 49
        Me.nudUnitDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudUnitDisplay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(26, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 18)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Unitdisplay ID :"
        '
        'cbSpeed
        '
        Me.cbSpeed.AutoSize = True
        Me.cbSpeed.Enabled = False
        Me.cbSpeed.Location = New System.Drawing.Point(261, 112)
        Me.cbSpeed.Name = "cbSpeed"
        Me.cbSpeed.Size = New System.Drawing.Size(105, 22)
        Me.cbSpeed.TabIndex = 46
        Me.cbSpeed.Text = "Speed Lane"
        Me.cbSpeed.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(608, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(230, 18)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Allow Other  Customer if Available"
        '
        'GridType
        '
        Me.GridType.AllowUserToAddRows = False
        Me.GridType.AllowUserToDeleteRows = False
        Me.GridType.AllowUserToResizeColumns = False
        Me.GridType.AllowUserToResizeRows = False
        Me.GridType.BackgroundColor = System.Drawing.Color.LightGray
        Me.GridType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridType.ColumnHeadersVisible = False
        Me.GridType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.customertype_id, Me.cb, Me.customertype_name, Me.customertype_code})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridType.DefaultCellStyle = DataGridViewCellStyle2
        Me.GridType.Enabled = False
        Me.GridType.Location = New System.Drawing.Point(405, 41)
        Me.GridType.MultiSelect = False
        Me.GridType.Name = "GridType"
        Me.GridType.RowHeadersVisible = False
        Me.GridType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridType.Size = New System.Drawing.Size(198, 204)
        Me.GridType.TabIndex = 7
        '
        'customertype_id
        '
        Me.customertype_id.DataPropertyName = "id"
        Me.customertype_id.HeaderText = "customertype_id"
        Me.customertype_id.Name = "customertype_id"
        Me.customertype_id.Visible = False
        Me.customertype_id.Width = 5
        '
        'cb
        '
        Me.cb.HeaderText = "cb"
        Me.cb.Name = "cb"
        Me.cb.Width = 20
        '
        'customertype_name
        '
        Me.customertype_name.DataPropertyName = "customertype_name"
        Me.customertype_name.HeaderText = "customertype_name"
        Me.customertype_name.Name = "customertype_name"
        Me.customertype_name.ReadOnly = True
        Me.customertype_name.Width = 155
        '
        'customertype_code
        '
        Me.customertype_code.DataPropertyName = "customertype_code"
        Me.customertype_code.HeaderText = "customertype_code"
        Me.customertype_code.Name = "customertype_code"
        Me.customertype_code.Visible = False
        Me.customertype_code.Width = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label10.Location = New System.Drawing.Point(402, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(110, 18)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Customer Type"
        '
        'GridTypeAllow
        '
        Me.GridTypeAllow.AllowUserToAddRows = False
        Me.GridTypeAllow.AllowUserToDeleteRows = False
        Me.GridTypeAllow.AllowUserToResizeColumns = False
        Me.GridTypeAllow.AllowUserToResizeRows = False
        Me.GridTypeAllow.BackgroundColor = System.Drawing.Color.LightGray
        Me.GridTypeAllow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridTypeAllow.ColumnHeadersVisible = False
        Me.GridTypeAllow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.customertype_id1, Me.cb1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridTypeAllow.DefaultCellStyle = DataGridViewCellStyle3
        Me.GridTypeAllow.Enabled = False
        Me.GridTypeAllow.Location = New System.Drawing.Point(611, 42)
        Me.GridTypeAllow.MultiSelect = False
        Me.GridTypeAllow.Name = "GridTypeAllow"
        Me.GridTypeAllow.RowHeadersVisible = False
        Me.GridTypeAllow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridTypeAllow.Size = New System.Drawing.Size(198, 203)
        Me.GridTypeAllow.TabIndex = 9
        '
        'customertype_id1
        '
        Me.customertype_id1.DataPropertyName = "id"
        Me.customertype_id1.HeaderText = "customertype_id"
        Me.customertype_id1.Name = "customertype_id1"
        Me.customertype_id1.Visible = False
        Me.customertype_id1.Width = 5
        '
        'cb1
        '
        Me.cb1.FillWeight = 5.076142!
        Me.cb1.HeaderText = "cb"
        Me.cb1.Name = "cb1"
        Me.cb1.Width = 20
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "customertype_name"
        Me.DataGridViewTextBoxColumn2.FillWeight = 194.9239!
        Me.DataGridViewTextBoxColumn2.HeaderText = "customertype_name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 155
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "customertype_code"
        Me.DataGridViewTextBoxColumn3.HeaderText = "customertype_code"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = Global.QueueSharp.My.Resources.Resources.Close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(253, 206)
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
        Me.btnAdd.Location = New System.Drawing.Point(61, 206)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 40)
        Me.btnAdd.TabIndex = 10
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
        Me.btnSave.Location = New System.Drawing.Point(157, 206)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 40)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cbQuick
        '
        Me.cbQuick.AutoSize = True
        Me.cbQuick.Enabled = False
        Me.cbQuick.Location = New System.Drawing.Point(136, 112)
        Me.cbQuick.Name = "cbQuick"
        Me.cbQuick.Size = New System.Drawing.Size(119, 22)
        Me.cbQuick.TabIndex = 3
        Me.cbQuick.Text = "Quick Service"
        Me.cbQuick.UseVisualStyleBackColor = True
        '
        'cbActive
        '
        Me.cbActive.AutoSize = True
        Me.cbActive.Enabled = False
        Me.cbActive.Location = New System.Drawing.Point(261, 168)
        Me.cbActive.Name = "cbActive"
        Me.cbActive.Size = New System.Drawing.Size(66, 22)
        Me.cbActive.TabIndex = 6
        Me.cbActive.Text = "Active"
        Me.cbActive.UseVisualStyleBackColor = True
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
        Me.Grouper1.Controls.Add(Me.Label4)
        Me.Grouper1.Controls.Add(Me.txtSearch)
        Me.Grouper1.Controls.Add(Me.Grid)
        Me.Grouper1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Grouper1.GroupImage = Nothing
        Me.Grouper1.GroupTitle = ""
        Me.Grouper1.Location = New System.Drawing.Point(6, 253)
        Me.Grouper1.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Grouper1.Name = "Grouper1"
        Me.Grouper1.Padding = New System.Windows.Forms.Padding(20)
        Me.Grouper1.PaintGroupBox = False
        Me.Grouper1.RoundCorners = 10
        Me.Grouper1.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper1.ShadowControl = True
        Me.Grouper1.ShadowThickness = 3
        Me.Grouper1.Size = New System.Drawing.Size(859, 339)
        Me.Grouper1.TabIndex = 25
        '
        'cbSearch
        '
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Items.AddRange(New Object() {"All", "Active", "Inactive"})
        Me.cbSearch.Location = New System.Drawing.Point(320, 17)
        Me.cbSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(140, 26)
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
        Me.txtSearch.Location = New System.Drawing.Point(77, 18)
        Me.txtSearch.MaxLength = 100
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(237, 24)
        Me.txtSearch.TabIndex = 18
        '
        'cbCounterTablet
        '
        Me.cbCounterTablet.AutoSize = True
        Me.cbCounterTablet.Enabled = False
        Me.cbCounterTablet.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.cbCounterTablet.FlatAppearance.BorderSize = 0
        Me.cbCounterTablet.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.cbCounterTablet.Location = New System.Drawing.Point(136, 168)
        Me.cbCounterTablet.Name = "cbCounterTablet"
        Me.cbCounterTablet.Size = New System.Drawing.Size(124, 22)
        Me.cbCounterTablet.TabIndex = 52
        Me.cbCounterTablet.Text = "Counter Tablet"
        Me.cbCounterTablet.UseVisualStyleBackColor = True
        '
        'counter_code
        '
        Me.counter_code.DataPropertyName = "counter_code"
        Me.counter_code.FillWeight = 28.24859!
        Me.counter_code.HeaderText = "Counter Code"
        Me.counter_code.Name = "counter_code"
        Me.counter_code.ReadOnly = True
        Me.counter_code.Width = 140
        '
        'counter_name
        '
        Me.counter_name.DataPropertyName = "counter_name"
        Me.counter_name.FillWeight = 171.7514!
        Me.counter_name.HeaderText = "Counter Name"
        Me.counter_name.Name = "counter_name"
        Me.counter_name.ReadOnly = True
        Me.counter_name.Width = 320
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
        '
        'quickservice
        '
        Me.quickservice.DataPropertyName = "quickservice"
        Me.quickservice.HeaderText = "quickservice"
        Me.quickservice.Name = "quickservice"
        Me.quickservice.ReadOnly = True
        Me.quickservice.Visible = False
        '
        'update_status
        '
        Me.update_status.DataPropertyName = "update_status"
        Me.update_status.HeaderText = "update_status"
        Me.update_status.Name = "update_status"
        Me.update_status.ReadOnly = True
        Me.update_status.Visible = False
        '
        'unitdisplay
        '
        Me.unitdisplay.DataPropertyName = "unitdisplay"
        Me.unitdisplay.HeaderText = "unitdisplay"
        Me.unitdisplay.Name = "unitdisplay"
        Me.unitdisplay.ReadOnly = True
        Me.unitdisplay.Visible = False
        '
        'speed_lane
        '
        Me.speed_lane.DataPropertyName = "speed_lane"
        Me.speed_lane.HeaderText = "speed_lane"
        Me.speed_lane.Name = "speed_lane"
        Me.speed_lane.ReadOnly = True
        Me.speed_lane.Visible = False
        '
        'back_office
        '
        Me.back_office.DataPropertyName = "back_office"
        Me.back_office.HeaderText = "back_office"
        Me.back_office.Name = "back_office"
        Me.back_office.ReadOnly = True
        Me.back_office.Visible = False
        '
        'counter_manager
        '
        Me.counter_manager.DataPropertyName = "counter_manager"
        Me.counter_manager.HeaderText = "counter_manager"
        Me.counter_manager.Name = "counter_manager"
        Me.counter_manager.ReadOnly = True
        Me.counter_manager.Visible = False
        '
        'counter_tablet
        '
        Me.counter_tablet.DataPropertyName = "counter_tablet"
        Me.counter_tablet.HeaderText = "counter_tablet"
        Me.counter_tablet.Name = "counter_tablet"
        Me.counter_tablet.ReadOnly = True
        Me.counter_tablet.Visible = False
        '
        'frmCounter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(869, 594)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Controls.Add(Me.Grouper1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCounter"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Counter"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        CType(Me.nudUnitDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridTypeAllow, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cbActive As System.Windows.Forms.CheckBox
    Friend WithEvents cbQuick As System.Windows.Forms.CheckBox
    Friend WithEvents GridTypeAllow As System.Windows.Forms.DataGridView
    Friend WithEvents GridType As System.Windows.Forms.DataGridView
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents customertype_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cb As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents customertype_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_id1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cb1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cbSpeed As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nudUnitDisplay As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbBackOffice As System.Windows.Forms.CheckBox
    Friend WithEvents cbCounterManager As System.Windows.Forms.CheckBox
    Friend WithEvents cbCounterTablet As System.Windows.Forms.CheckBox
    Friend WithEvents counter_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents counter_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents active_status As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents quickservice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents update_status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents unitdisplay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents speed_lane As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents back_office As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents counter_manager As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents counter_tablet As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
