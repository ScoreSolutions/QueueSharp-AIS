<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAisEmpProfileAgent
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAisEmpProfileAgent))
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.lblTime = New System.Windows.Forms.Label
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.tmTime = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbShop = New System.Windows.Forms.ComboBox
        Me.btnUpdateData = New System.Windows.Forms.Button
        Me.btnTestCall = New System.Windows.Forms.Button
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Location = New System.Drawing.Point(12, 34)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(777, 310)
        Me.txtLog.TabIndex = 4
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTime.Location = New System.Drawing.Point(706, 5)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(79, 20)
        Me.lblTime.TabIndex = 5
        Me.lblTime.Text = "00:00:00"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "AIS Update Employee Profile Agent V0.0.0.1"
        Me.NotifyIcon1.Visible = True
        '
        'tmTime
        '
        Me.tmTime.Enabled = True
        Me.tmTime.Interval = 1000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Shop : "
        '
        'cmbShop
        '
        Me.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShop.FormattingEnabled = True
        Me.cmbShop.Location = New System.Drawing.Point(59, 7)
        Me.cmbShop.Name = "cmbShop"
        Me.cmbShop.Size = New System.Drawing.Size(309, 21)
        Me.cmbShop.TabIndex = 7
        '
        'btnUpdateData
        '
        Me.btnUpdateData.Location = New System.Drawing.Point(375, 7)
        Me.btnUpdateData.Name = "btnUpdateData"
        Me.btnUpdateData.Size = New System.Drawing.Size(83, 23)
        Me.btnUpdateData.TabIndex = 8
        Me.btnUpdateData.Text = "Update Data"
        Me.btnUpdateData.UseVisualStyleBackColor = True
        '
        'btnTestCall
        '
        Me.btnTestCall.Location = New System.Drawing.Point(142, 348)
        Me.btnTestCall.Name = "btnTestCall"
        Me.btnTestCall.Size = New System.Drawing.Size(75, 23)
        Me.btnTestCall.TabIndex = 9
        Me.btnTestCall.Text = "Test Call"
        Me.btnTestCall.UseVisualStyleBackColor = True
        Me.btnTestCall.Visible = False
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(12, 350)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(124, 20)
        Me.txtUserName.TabIndex = 10
        Me.txtUserName.Visible = False
        '
        'frmAisEmpProfileAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 380)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.btnTestCall)
        Me.Controls.Add(Me.btnUpdateData)
        Me.Controls.Add(Me.cmbShop)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.lblTime)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAisEmpProfileAgent"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents tmTime As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbShop As System.Windows.Forms.ComboBox
    Friend WithEvents btnUpdateData As System.Windows.Forms.Button
    Friend WithEvents btnTestCall As System.Windows.Forms.Button
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox

End Class
