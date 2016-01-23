<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonitorStackServer
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.txtIPAddress = New System.Windows.Forms.TextBox
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnEmailDelete = New System.Windows.Forms.Button
        Me.dgEmail = New System.Windows.Forms.DataGridView
        Me.btnEmailAdd = New System.Windows.Forms.Button
        Me.txtEmailTo = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtMobileNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnSmsDelete = New System.Windows.Forms.Button
        Me.btnSmsAdd = New System.Windows.Forms.Button
        Me.dgSMS = New System.Windows.Forms.DataGridView
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.email_to = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sms_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sms_to = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgSMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "IP Address :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Location :"
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(93, 6)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(133, 20)
        Me.txtServerName.TabIndex = 3
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(93, 28)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.Size = New System.Drawing.Size(133, 20)
        Me.txtIPAddress.TabIndex = 4
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(93, 50)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(133, 20)
        Me.txtLocation.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(242, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 64)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(15, 76)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(302, 373)
        Me.txtDesc.TabIndex = 7
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10000
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnEmailDelete)
        Me.GroupBox1.Controls.Add(Me.dgEmail)
        Me.GroupBox1.Controls.Add(Me.btnEmailAdd)
        Me.GroupBox1.Controls.Add(Me.txtEmailTo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(338, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 440)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "E-Mail To"
        '
        'btnEmailDelete
        '
        Me.btnEmailDelete.Location = New System.Drawing.Point(129, 51)
        Me.btnEmailDelete.Name = "btnEmailDelete"
        Me.btnEmailDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnEmailDelete.TabIndex = 4
        Me.btnEmailDelete.Text = "Delete"
        Me.btnEmailDelete.UseVisualStyleBackColor = True
        '
        'dgEmail
        '
        Me.dgEmail.AllowUserToAddRows = False
        Me.dgEmail.AllowUserToDeleteRows = False
        Me.dgEmail.AllowUserToResizeColumns = False
        Me.dgEmail.AllowUserToResizeRows = False
        Me.dgEmail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgEmail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgEmail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.email_to})
        Me.dgEmail.Location = New System.Drawing.Point(6, 80)
        Me.dgEmail.Name = "dgEmail"
        Me.dgEmail.RowHeadersVisible = False
        Me.dgEmail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgEmail.Size = New System.Drawing.Size(268, 354)
        Me.dgEmail.TabIndex = 3
        '
        'btnEmailAdd
        '
        Me.btnEmailAdd.Location = New System.Drawing.Point(47, 51)
        Me.btnEmailAdd.Name = "btnEmailAdd"
        Me.btnEmailAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnEmailAdd.TabIndex = 2
        Me.btnEmailAdd.Text = "Add"
        Me.btnEmailAdd.UseVisualStyleBackColor = True
        '
        'txtEmailTo
        '
        Me.txtEmailTo.Location = New System.Drawing.Point(47, 24)
        Me.txtEmailTo.Name = "txtEmailTo"
        Me.txtEmailTo.Size = New System.Drawing.Size(227, 20)
        Me.txtEmailTo.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Email : "
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.dgSMS)
        Me.GroupBox2.Controls.Add(Me.btnSmsDelete)
        Me.GroupBox2.Controls.Add(Me.btnSmsAdd)
        Me.GroupBox2.Controls.Add(Me.txtMobileNo)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(624, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(196, 440)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SMS To"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.Location = New System.Drawing.Point(71, 24)
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Size = New System.Drawing.Size(114, 20)
        Me.txtMobileNo.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Mobile No : "
        '
        'btnSmsDelete
        '
        Me.btnSmsDelete.Location = New System.Drawing.Point(109, 51)
        Me.btnSmsDelete.Name = "btnSmsDelete"
        Me.btnSmsDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnSmsDelete.TabIndex = 8
        Me.btnSmsDelete.Text = "Delete"
        Me.btnSmsDelete.UseVisualStyleBackColor = True
        '
        'btnSmsAdd
        '
        Me.btnSmsAdd.Location = New System.Drawing.Point(27, 51)
        Me.btnSmsAdd.Name = "btnSmsAdd"
        Me.btnSmsAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnSmsAdd.TabIndex = 7
        Me.btnSmsAdd.Text = "Add"
        Me.btnSmsAdd.UseVisualStyleBackColor = True
        '
        'dgSMS
        '
        Me.dgSMS.AllowUserToAddRows = False
        Me.dgSMS.AllowUserToDeleteRows = False
        Me.dgSMS.AllowUserToResizeColumns = False
        Me.dgSMS.AllowUserToResizeRows = False
        Me.dgSMS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSMS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgSMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSMS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.sms_id, Me.sms_to})
        Me.dgSMS.Location = New System.Drawing.Point(6, 80)
        Me.dgSMS.Name = "dgSMS"
        Me.dgSMS.RowHeadersVisible = False
        Me.dgSMS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgSMS.Size = New System.Drawing.Size(184, 354)
        Me.dgSMS.TabIndex = 9
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.Visible = False
        '
        'email_to
        '
        Me.email_to.DataPropertyName = "email_to"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.email_to.DefaultCellStyle = DataGridViewCellStyle10
        Me.email_to.HeaderText = "E-Mail"
        Me.email_to.Name = "email_to"
        Me.email_to.Width = 240
        '
        'sms_id
        '
        Me.sms_id.DataPropertyName = "id"
        Me.sms_id.HeaderText = "id"
        Me.sms_id.Name = "sms_id"
        Me.sms_id.Visible = False
        '
        'sms_to
        '
        Me.sms_to.DataPropertyName = "sms_to"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.sms_to.DefaultCellStyle = DataGridViewCellStyle12
        Me.sms_to.HeaderText = "Mobile No"
        Me.sms_to.Name = "sms_to"
        Me.sms_to.Width = 160
        '
        'frmMonitorStackServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 461)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtIPAddress)
        Me.Controls.Add(Me.txtServerName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMonitorStackServer"
        Me.Text = "Monitor Stack Server"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgEmail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgSMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnEmailAdd As System.Windows.Forms.Button
    Friend WithEvents txtEmailTo As System.Windows.Forms.TextBox
    Friend WithEvents dgEmail As System.Windows.Forms.DataGridView
    Friend WithEvents btnEmailDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents email_to As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgSMS As System.Windows.Forms.DataGridView
    Friend WithEvents btnSmsDelete As System.Windows.Forms.Button
    Friend WithEvents btnSmsAdd As System.Windows.Forms.Button
    Friend WithEvents sms_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sms_to As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
