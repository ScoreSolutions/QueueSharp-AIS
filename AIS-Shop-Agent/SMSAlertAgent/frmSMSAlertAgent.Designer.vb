<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSMSAlertAgent
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSMSAlertAgent))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tmTime = New System.Windows.Forms.Timer(Me.components)
        Me.lblTime = New System.Windows.Forms.Label
        Me.dgServiceList = New System.Windows.Forms.DataGridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.tmAlert = New System.Windows.Forms.Timer(Me.components)
        Me.lblMinuteConfig = New System.Windows.Forms.Label
        Me.btnConfig = New System.Windows.Forms.Button
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colServiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colServiceName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colAvgHT = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgServiceList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmTime
        '
        Me.tmTime.Enabled = True
        '
        'lblTime
        '
        Me.lblTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(376, 6)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTime.Size = New System.Drawing.Size(39, 13)
        Me.lblTime.TabIndex = 0
        Me.lblTime.Text = "Label1"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgServiceList
        '
        Me.dgServiceList.AllowUserToAddRows = False
        Me.dgServiceList.AllowUserToDeleteRows = False
        Me.dgServiceList.AllowUserToResizeColumns = False
        Me.dgServiceList.AllowUserToResizeRows = False
        Me.dgServiceList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgServiceList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgServiceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgServiceList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colServiceDate, Me.colServiceName, Me.colAvgHT})
        Me.dgServiceList.Location = New System.Drawing.Point(12, 25)
        Me.dgServiceList.Name = "dgServiceList"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgServiceList.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgServiceList.RowHeadersVisible = False
        Me.dgServiceList.Size = New System.Drawing.Size(485, 249)
        Me.dgServiceList.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(385, 186)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'tmAlert
        '
        Me.tmAlert.Interval = 1000
        '
        'lblMinuteConfig
        '
        Me.lblMinuteConfig.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinuteConfig.AutoSize = True
        Me.lblMinuteConfig.Location = New System.Drawing.Point(6, 6)
        Me.lblMinuteConfig.Name = "lblMinuteConfig"
        Me.lblMinuteConfig.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMinuteConfig.Size = New System.Drawing.Size(0, 13)
        Me.lblMinuteConfig.TabIndex = 3
        Me.lblMinuteConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMinuteConfig.Visible = False
        '
        'btnConfig
        '
        Me.btnConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfig.Location = New System.Drawing.Point(422, 277)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(75, 23)
        Me.btnConfig.TabIndex = 4
        Me.btnConfig.Text = "Config"
        Me.btnConfig.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "SMS Notify Agent"
        Me.NotifyIcon1.Visible = True
        '
        'colID
        '
        Me.colID.DataPropertyName = "item_id"
        Me.colID.HeaderText = "item_id"
        Me.colID.Name = "colID"
        Me.colID.Visible = False
        '
        'colServiceDate
        '
        Me.colServiceDate.DataPropertyName = "service_date"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colServiceDate.DefaultCellStyle = DataGridViewCellStyle2
        Me.colServiceDate.HeaderText = "Last Month"
        Me.colServiceDate.Name = "colServiceDate"
        '
        'colServiceName
        '
        Me.colServiceName.DataPropertyName = "item_name"
        Me.colServiceName.HeaderText = "Service Name"
        Me.colServiceName.Name = "colServiceName"
        Me.colServiceName.Width = 150
        '
        'colAvgHT
        '
        Me.colAvgHT.DataPropertyName = "avg_ht"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colAvgHT.DefaultCellStyle = DataGridViewCellStyle3
        Me.colAvgHT.HeaderText = "AVG HT"
        Me.colAvgHT.Name = "colAvgHT"
        '
        'frmSMSAlertAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 302)
        Me.Controls.Add(Me.btnConfig)
        Me.Controls.Add(Me.lblMinuteConfig)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dgServiceList)
        Me.Controls.Add(Me.lblTime)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSMSAlertAgent"
        Me.Text = "SMS Alert Agent"
        CType(Me.dgServiceList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmTime As System.Windows.Forms.Timer
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents dgServiceList As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents tmAlert As System.Windows.Forms.Timer
    Friend WithEvents lblMinuteConfig As System.Windows.Forms.Label
    Friend WithEvents btnConfig As System.Windows.Forms.Button
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents colID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colServiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colServiceName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAvgHT As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
