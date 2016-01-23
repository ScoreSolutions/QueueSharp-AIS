<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAppointmentCustomer
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
        Me.GroupBoxViewCustomerEnd = New CodeVendor.Controls.Grouper
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.customer_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Segment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Service = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.app_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colNetworkType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBoxViewCustomerEnd.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.Grid)
        Me.GroupBoxViewCustomerEnd.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxViewCustomerEnd.GroupImage = Nothing
        Me.GroupBoxViewCustomerEnd.GroupTitle = ""
        Me.GroupBoxViewCustomerEnd.Location = New System.Drawing.Point(6, -2)
        Me.GroupBoxViewCustomerEnd.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBoxViewCustomerEnd.MinimumSize = New System.Drawing.Size(3, 1)
        Me.GroupBoxViewCustomerEnd.Name = "GroupBoxViewCustomerEnd"
        Me.GroupBoxViewCustomerEnd.Padding = New System.Windows.Forms.Padding(45, 39, 45, 39)
        Me.GroupBoxViewCustomerEnd.PaintGroupBox = False
        Me.GroupBoxViewCustomerEnd.RoundCorners = 10
        Me.GroupBoxViewCustomerEnd.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxViewCustomerEnd.ShadowControl = True
        Me.GroupBoxViewCustomerEnd.ShadowThickness = 3
        Me.GroupBoxViewCustomerEnd.Size = New System.Drawing.Size(973, 499)
        Me.GroupBoxViewCustomerEnd.TabIndex = 25
        '
        'Grid
        '
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToDeleteRows = False
        Me.Grid.AllowUserToResizeRows = False
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.customer_id, Me.DataGridViewTextBoxColumn1, Me.Segment, Me.Service, Me.app_time, Me.colNetworkType, Me.Status_name})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grid.DefaultCellStyle = DataGridViewCellStyle1
        Me.Grid.Location = New System.Drawing.Point(14, 26)
        Me.Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.MultiSelect = False
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.ShowCellErrors = False
        Me.Grid.ShowCellToolTips = False
        Me.Grid.ShowEditingIcon = False
        Me.Grid.Size = New System.Drawing.Size(940, 455)
        Me.Grid.TabIndex = 32
        '
        'customer_id
        '
        Me.customer_id.DataPropertyName = "customer_id"
        Me.customer_id.HeaderText = "Mobile No"
        Me.customer_id.Name = "customer_id"
        Me.customer_id.ReadOnly = True
        Me.customer_id.Width = 120
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "queue_no"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Queue No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'Segment
        '
        Me.Segment.DataPropertyName = "Segment"
        Me.Segment.HeaderText = "Segment"
        Me.Segment.Name = "Segment"
        Me.Segment.ReadOnly = True
        Me.Segment.Width = 150
        '
        'Service
        '
        Me.Service.DataPropertyName = "Service"
        Me.Service.HeaderText = "Service"
        Me.Service.Name = "Service"
        Me.Service.ReadOnly = True
        Me.Service.Width = 400
        '
        'app_time
        '
        Me.app_time.DataPropertyName = "app_time"
        Me.app_time.HeaderText = "Appointment Time"
        Me.app_time.Name = "app_time"
        Me.app_time.ReadOnly = True
        Me.app_time.Width = 150
        '
        'colNetworkType
        '
        Me.colNetworkType.DataPropertyName = "network_type"
        Me.colNetworkType.HeaderText = "Network Type"
        Me.colNetworkType.Name = "colNetworkType"
        Me.colNetworkType.ReadOnly = True
        Me.colNetworkType.Width = 150
        '
        'Status_name
        '
        Me.Status_name.DataPropertyName = "status_name"
        Me.Status_name.HeaderText = "Status"
        Me.Status_name.Name = "Status_name"
        Me.Status_name.ReadOnly = True
        Me.Status_name.Width = 150
        '
        'frmAppointmentCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(984, 501)
        Me.Controls.Add(Me.GroupBoxViewCustomerEnd)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAppointmentCustomer"
        Me.ShowInTaskbar = False
        Me.Text = "Appointment Customer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBoxViewCustomerEnd.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxViewCustomerEnd As CodeVendor.Controls.Grouper
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents customer_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Segment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Service As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents app_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetworkType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status_name As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
