<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEncryptUserPwdAtShop
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnDecrypt = New System.Windows.Forms.Button
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.txtDBName = New System.Windows.Forms.TextBox
        Me.txtServerIP = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExport = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.colStaffName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPosition = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colUserName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPassword = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sd = New System.Windows.Forms.SaveFileDialog
        Me.btnByUser = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnByUser)
        Me.Panel1.Controls.Add(Me.btnDecrypt)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.txtUserName)
        Me.Panel1.Controls.Add(Me.txtDBName)
        Me.Panel1.Controls.Add(Me.txtServerIP)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(615, 87)
        Me.Panel1.TabIndex = 0
        '
        'btnDecrypt
        '
        Me.btnDecrypt.Location = New System.Drawing.Point(84, 56)
        Me.btnDecrypt.Name = "btnDecrypt"
        Me.btnDecrypt.Size = New System.Drawing.Size(75, 23)
        Me.btnDecrypt.TabIndex = 8
        Me.btnDecrypt.Text = "Decrypt"
        Me.btnDecrypt.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(351, 30)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(195, 20)
        Me.txtPassword.TabIndex = 7
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(351, 7)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(195, 20)
        Me.txtUserName.TabIndex = 6
        '
        'txtDBName
        '
        Me.txtDBName.Location = New System.Drawing.Point(84, 30)
        Me.txtDBName.Name = "txtDBName"
        Me.txtDBName.Size = New System.Drawing.Size(195, 20)
        Me.txtDBName.TabIndex = 5
        '
        'txtServerIP
        '
        Me.txtServerIP.Location = New System.Drawing.Point(84, 7)
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.Size = New System.Drawing.Size(195, 20)
        Me.txtServerIP.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(285, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(285, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "User Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DB Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server IP"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(552, 440)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(75, 23)
        Me.btnExport.TabIndex = 9
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStaffName, Me.colPosition, Me.colUserName, Me.colPassword, Me.colStatus})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 105)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.Size = New System.Drawing.Size(615, 329)
        Me.DataGridView1.TabIndex = 10
        '
        'colStaffName
        '
        Me.colStaffName.DataPropertyName = "staff_name"
        Me.colStaffName.HeaderText = "Staff Name"
        Me.colStaffName.Name = "colStaffName"
        Me.colStaffName.ReadOnly = True
        Me.colStaffName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colStaffName.Width = 200
        '
        'colPosition
        '
        Me.colPosition.DataPropertyName = "position"
        Me.colPosition.HeaderText = "Position"
        Me.colPosition.Name = "colPosition"
        Me.colPosition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colUserName
        '
        Me.colUserName.DataPropertyName = "username"
        Me.colUserName.HeaderText = "User Name"
        Me.colUserName.Name = "colUserName"
        Me.colUserName.ReadOnly = True
        Me.colUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colPassword
        '
        Me.colPassword.DataPropertyName = "password"
        Me.colPassword.HeaderText = "Password"
        Me.colPassword.Name = "colPassword"
        Me.colPassword.ReadOnly = True
        Me.colPassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colStatus
        '
        Me.colStatus.DataPropertyName = "status"
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'btnByUser
        '
        Me.btnByUser.Location = New System.Drawing.Point(204, 56)
        Me.btnByUser.Name = "btnByUser"
        Me.btnByUser.Size = New System.Drawing.Size(75, 23)
        Me.btnByUser.TabIndex = 9
        Me.btnByUser.Text = "By User"
        Me.btnByUser.UseVisualStyleBackColor = True
        '
        'frmEncryptUserPwdAtShop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 475)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmEncryptUserPwdAtShop"
        Me.Text = "frmEncryptUserPwdAtShop"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtDBName As System.Windows.Forms.TextBox
    Friend WithEvents txtServerIP As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDecrypt As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents colStaffName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPosition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUserName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPassword As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnByUser As System.Windows.Forms.Button
End Class
