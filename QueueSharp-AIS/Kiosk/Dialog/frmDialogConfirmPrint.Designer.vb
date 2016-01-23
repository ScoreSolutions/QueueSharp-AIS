<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDialogConfirmPrint
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
        Me.PanelMsg = New System.Windows.Forms.Panel
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnSms = New System.Windows.Forms.Button
        Me.lblMsg = New System.Windows.Forms.Label
        Me.btnPrintAndSMS = New System.Windows.Forms.Button
        Me.PanelMsg.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelMsg
        '
        Me.PanelMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PanelMsg.BackgroundImage = Global.Kiosk.My.Resources.Resources.msg
        Me.PanelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelMsg.Controls.Add(Me.btnPrint)
        Me.PanelMsg.Controls.Add(Me.btnSms)
        Me.PanelMsg.Controls.Add(Me.lblMsg)
        Me.PanelMsg.Controls.Add(Me.btnPrintAndSMS)
        Me.PanelMsg.Location = New System.Drawing.Point(0, 2)
        Me.PanelMsg.Name = "PanelMsg"
        Me.PanelMsg.Size = New System.Drawing.Size(400, 359)
        Me.PanelMsg.TabIndex = 54
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Transparent
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(89, 304)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(82, 52)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnSms
        '
        Me.btnSms.BackColor = System.Drawing.Color.Transparent
        Me.btnSms.FlatAppearance.BorderSize = 0
        Me.btnSms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSms.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSms.Location = New System.Drawing.Point(167, 304)
        Me.btnSms.Name = "btnSms"
        Me.btnSms.Size = New System.Drawing.Size(99, 52)
        Me.btnSms.TabIndex = 4
        Me.btnSms.Text = "SMS"
        Me.btnSms.UseVisualStyleBackColor = False
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(17, 19)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(367, 282)
        Me.lblMsg.TabIndex = 0
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnPrintAndSMS
        '
        Me.btnPrintAndSMS.BackColor = System.Drawing.Color.Transparent
        Me.btnPrintAndSMS.FlatAppearance.BorderSize = 0
        Me.btnPrintAndSMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintAndSMS.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPrintAndSMS.Location = New System.Drawing.Point(264, 304)
        Me.btnPrintAndSMS.Name = "btnPrintAndSMS"
        Me.btnPrintAndSMS.Size = New System.Drawing.Size(131, 52)
        Me.btnPrintAndSMS.TabIndex = 1
        Me.btnPrintAndSMS.Text = "Print && SMS"
        Me.btnPrintAndSMS.UseVisualStyleBackColor = False
        '
        'frmDialogConfirmPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 362)
        Me.Controls.Add(Me.PanelMsg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDialogConfirmPrint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.PanelMsg.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelMsg As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents btnPrintAndSMS As System.Windows.Forms.Button
    Friend WithEvents btnSms As System.Windows.Forms.Button
End Class
