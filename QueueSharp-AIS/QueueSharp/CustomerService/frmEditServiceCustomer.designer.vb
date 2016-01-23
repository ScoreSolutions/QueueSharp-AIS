<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditServiceCustomer
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
        Me.GridCustomer = New System.Windows.Forms.DataGridView
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.GroupBox1 = New CodeVendor.Controls.Grouper
        Me.TextFilter = New System.Windows.Forms.TextBox
        Me.Grouper1 = New CodeVendor.Controls.Grouper
        Me.GridItem = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.queue_no = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customer_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customertype_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.customertype_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.status_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.item_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.GridCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxButton.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Grouper1.SuspendLayout()
        CType(Me.GridItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.GridCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.queue_no, Me.customer_id, Me.customertype_name, Me.customertype_id})
        Me.GridCustomer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridCustomer.Location = New System.Drawing.Point(12, 23)
        Me.GridCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.GridCustomer.MultiSelect = False
        Me.GridCustomer.Name = "GridCustomer"
        Me.GridCustomer.RowHeadersVisible = False
        Me.GridCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridCustomer.Size = New System.Drawing.Size(444, 587)
        Me.GridCustomer.TabIndex = 0
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
        Me.GroupBoxButton.Controls.Add(Me.GridCustomer)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, 61)
        Me.GroupBoxButton.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(472, 626)
        Me.GroupBoxButton.TabIndex = 23
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackgroundColor = System.Drawing.Color.White
        Me.GroupBox1.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBox1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBox1.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBox1.BorderThickness = 1.0!
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextFilter)
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
        Me.GroupBox1.Size = New System.Drawing.Size(961, 66)
        Me.GroupBox1.TabIndex = 24
        '
        'TextFilter
        '
        Me.TextFilter.Location = New System.Drawing.Point(216, 22)
        Me.TextFilter.Margin = New System.Windows.Forms.Padding(4)
        Me.TextFilter.MaxLength = 100
        Me.TextFilter.Name = "TextFilter"
        Me.TextFilter.Size = New System.Drawing.Size(248, 24)
        Me.TextFilter.TabIndex = 0
        '
        'Grouper1
        '
        Me.Grouper1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper1.BackgroundColor = System.Drawing.Color.White
        Me.Grouper1.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.Grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper1.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper1.BorderThickness = 1.0!
        Me.Grouper1.Controls.Add(Me.GridItem)
        Me.Grouper1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper1.GroupImage = Nothing
        Me.Grouper1.GroupTitle = ""
        Me.Grouper1.Location = New System.Drawing.Point(486, 61)
        Me.Grouper1.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper1.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper1.Name = "Grouper1"
        Me.Grouper1.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper1.PaintGroupBox = False
        Me.Grouper1.RoundCorners = 10
        Me.Grouper1.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper1.ShadowControl = True
        Me.Grouper1.ShadowThickness = 3
        Me.Grouper1.Size = New System.Drawing.Size(481, 626)
        Me.Grouper1.TabIndex = 25
        '
        'GridItem
        '
        Me.GridItem.AllowUserToAddRows = False
        Me.GridItem.AllowUserToDeleteRows = False
        Me.GridItem.AllowUserToOrderColumns = True
        Me.GridItem.AllowUserToResizeRows = False
        Me.GridItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.item_name, Me.status_name, Me.DataGridViewTextBoxColumn2, Me.status, Me.item_id, Me.id})
        Me.GridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridItem.Location = New System.Drawing.Point(13, 23)
        Me.GridItem.Margin = New System.Windows.Forms.Padding(4)
        Me.GridItem.MultiSelect = False
        Me.GridItem.Name = "GridItem"
        Me.GridItem.RowHeadersVisible = False
        Me.GridItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridItem.Size = New System.Drawing.Size(452, 587)
        Me.GridItem.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 18)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "( Queue No.  or  Mobile No. )"
        '
        'queue_no
        '
        Me.queue_no.DataPropertyName = "queue_no"
        Me.queue_no.HeaderText = "Queue No"
        Me.queue_no.Name = "queue_no"
        Me.queue_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.queue_no.Width = 110
        '
        'customer_id
        '
        Me.customer_id.DataPropertyName = "customer_id"
        Me.customer_id.HeaderText = "Mobile No"
        Me.customer_id.Name = "customer_id"
        Me.customer_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customer_id.Width = 130
        '
        'customertype_name
        '
        Me.customertype_name.DataPropertyName = "customertype_name"
        Me.customertype_name.HeaderText = "Customer Type"
        Me.customertype_name.Name = "customertype_name"
        Me.customertype_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customertype_name.Width = 200
        '
        'customertype_id
        '
        Me.customertype_id.DataPropertyName = "customertype_id"
        Me.customertype_id.HeaderText = "customertype_id"
        Me.customertype_id.Name = "customertype_id"
        Me.customertype_id.Visible = False
        Me.customertype_id.Width = 142
        '
        'item_name
        '
        Me.item_name.DataPropertyName = "item_name"
        Me.item_name.HeaderText = "Service"
        Me.item_name.Name = "item_name"
        Me.item_name.Width = 200
        '
        'status_name
        '
        Me.status_name.DataPropertyName = "status_name"
        Me.status_name.HeaderText = "Status"
        Me.status_name.Name = "status_name"
        Me.status_name.Width = 200
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "queue_no"
        Me.DataGridViewTextBoxColumn2.HeaderText = "queue_no"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 140
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        Me.status.HeaderText = "status"
        Me.status.Name = "status"
        Me.status.Visible = False
        '
        'item_id
        '
        Me.item_id.DataPropertyName = "item_id"
        Me.item_id.HeaderText = "item_id"
        Me.item_id.Name = "item_id"
        Me.item_id.Visible = False
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.Visible = False
        Me.id.Width = 142
        '
        'frmEditServiceCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(971, 691)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.Controls.Add(Me.Grouper1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEditServiceCustomer"
        Me.Text = "Edit Customer Service"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Grouper1.ResumeLayout(False)
        CType(Me.GridItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridCustomer As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents GroupBox1 As CodeVendor.Controls.Grouper
    Friend WithEvents TextFilter As System.Windows.Forms.TextBox
    Friend WithEvents Grouper1 As CodeVendor.Controls.Grouper
    Friend WithEvents GridItem As System.Windows.Forms.DataGridView
    Friend WithEvents queue_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customertype_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents item_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
