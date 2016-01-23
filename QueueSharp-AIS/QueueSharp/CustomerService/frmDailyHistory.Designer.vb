<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDailyHistory
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
        Me.lblServe = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.cbUser = New System.Windows.Forms.ComboBox
        Me.Counter_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.xxx = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Segment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.service = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Waiting_T = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.served_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TotalWaiting = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.row = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colNetworkType = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.lblServe)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.Label6)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.Label4)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.Grid)
        Me.GroupBoxViewCustomerEnd.Controls.Add(Me.cbUser)
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
        'lblServe
        '
        Me.lblServe.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblServe.Location = New System.Drawing.Point(178, 467)
        Me.lblServe.Name = "lblServe"
        Me.lblServe.Size = New System.Drawing.Size(52, 18)
        Me.lblServe.TabIndex = 38
        Me.lblServe.Text = "0"
        Me.lblServe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 467)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 18)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Total customer served."
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(236, 467)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 18)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Queue"
        '
        'Grid
        '
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToDeleteRows = False
        Me.Grid.AllowUserToResizeColumns = False
        Me.Grid.AllowUserToResizeRows = False
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Counter_name, Me.DataGridViewTextBoxColumn1, Me.CusName, Me.xxx, Me.DataGridViewTextBoxColumn3, Me.Segment, Me.service, Me.Waiting_T, Me.served_time, Me.Status_name, Me.TotalWaiting, Me.row, Me.colNetworkType})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grid.DefaultCellStyle = DataGridViewCellStyle1
        Me.Grid.Location = New System.Drawing.Point(15, 57)
        Me.Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.MultiSelect = False
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.ShowCellErrors = False
        Me.Grid.ShowCellToolTips = False
        Me.Grid.ShowEditingIcon = False
        Me.Grid.Size = New System.Drawing.Size(940, 405)
        Me.Grid.TabIndex = 32
        '
        'cbUser
        '
        Me.cbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUser.FormattingEnabled = True
        Me.cbUser.Location = New System.Drawing.Point(15, 23)
        Me.cbUser.Margin = New System.Windows.Forms.Padding(4)
        Me.cbUser.Name = "cbUser"
        Me.cbUser.Size = New System.Drawing.Size(276, 26)
        Me.cbUser.TabIndex = 31
        '
        'Counter_name
        '
        Me.Counter_name.DataPropertyName = "Counter_name"
        Me.Counter_name.HeaderText = "Counter"
        Me.Counter_name.Name = "Counter_name"
        Me.Counter_name.ReadOnly = True
        Me.Counter_name.Width = 110
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
        Me.xxx.Width = 105
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "customertype_name"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Customer Type"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 200
        '
        'Segment
        '
        Me.Segment.DataPropertyName = "Segment"
        Me.Segment.HeaderText = "Segment"
        Me.Segment.Name = "Segment"
        Me.Segment.ReadOnly = True
        Me.Segment.Width = 110
        '
        'service
        '
        Me.service.DataPropertyName = "service"
        Me.service.HeaderText = "Service"
        Me.service.Name = "service"
        Me.service.ReadOnly = True
        Me.service.Width = 150
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
        Me.served_time.Width = 130
        '
        'Status_name
        '
        Me.Status_name.DataPropertyName = "Status_name"
        Me.Status_name.HeaderText = "Status"
        Me.Status_name.Name = "Status_name"
        Me.Status_name.ReadOnly = True
        Me.Status_name.Width = 117
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
        Me.row.DataPropertyName = "row"
        Me.row.HeaderText = "row"
        Me.row.Name = "row"
        Me.row.ReadOnly = True
        Me.row.Visible = False
        '
        'colNetworkType
        '
        Me.colNetworkType.DataPropertyName = "network_type"
        Me.colNetworkType.HeaderText = "Network Type"
        Me.colNetworkType.Name = "colNetworkType"
        Me.colNetworkType.ReadOnly = True
        Me.colNetworkType.Width = 150
        '
        'frmDailyHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(984, 501)
        Me.Controls.Add(Me.GroupBoxViewCustomerEnd)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmDailyHistory"
        Me.ShowInTaskbar = False
        Me.Text = "Daily History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBoxViewCustomerEnd.ResumeLayout(False)
        Me.GroupBoxViewCustomerEnd.PerformLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxViewCustomerEnd As CodeVendor.Controls.Grouper
    Friend WithEvents cbUser As System.Windows.Forms.ComboBox
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents lblServe As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Counter_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents xxx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Segment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents service As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Waiting_T As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents served_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalWaiting As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents row As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetworkType As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
