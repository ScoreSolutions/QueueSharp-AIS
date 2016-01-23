<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAisCSISendMail
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpST = New System.Windows.Forms.DateTimePicker
        Me.dtpET = New System.Windows.Forms.DateTimePicker
        Me.btnSendEmail = New System.Windows.Forms.Button
        Me.cmbShop = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblTime = New System.Windows.Forms.Label
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(194, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "To"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Date From : "
        '
        'dtpST
        '
        Me.dtpST.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpST.Location = New System.Drawing.Point(78, 39)
        Me.dtpST.Name = "dtpST"
        Me.dtpST.Size = New System.Drawing.Size(107, 20)
        Me.dtpST.TabIndex = 29
        '
        'dtpET
        '
        Me.dtpET.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpET.Location = New System.Drawing.Point(220, 39)
        Me.dtpET.Name = "dtpET"
        Me.dtpET.Size = New System.Drawing.Size(107, 20)
        Me.dtpET.TabIndex = 30
        '
        'btnSendEmail
        '
        Me.btnSendEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSendEmail.Location = New System.Drawing.Point(333, 12)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(83, 47)
        Me.btnSendEmail.TabIndex = 28
        Me.btnSendEmail.Text = "Send Email"
        Me.btnSendEmail.UseVisualStyleBackColor = True
        '
        'cmbShop
        '
        Me.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShop.FormattingEnabled = True
        Me.cmbShop.Location = New System.Drawing.Point(78, 12)
        Me.cmbShop.Name = "cmbShop"
        Me.cmbShop.Size = New System.Drawing.Size(249, 21)
        Me.cmbShop.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Shop : "
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTime.Location = New System.Drawing.Point(722, 16)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(79, 20)
        Me.lblTime.TabIndex = 25
        Me.lblTime.Text = "00:00:00"
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Enabled = False
        Me.txtLog.Location = New System.Drawing.Point(10, 68)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(786, 275)
        Me.txtLog.TabIndex = 24
        '
        'frmAisCSISendMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 355)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpST)
        Me.Controls.Add(Me.dtpET)
        Me.Controls.Add(Me.btnSendEmail)
        Me.Controls.Add(Me.cmbShop)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.txtLog)
        Me.Name = "frmAisCSISendMail"
        Me.Text = "AIS CSI Send E-Mail Agent"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpST As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpET As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSendEmail As System.Windows.Forms.Button
    Friend WithEvents cmbShop As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents txtLog As System.Windows.Forms.TextBox

End Class
