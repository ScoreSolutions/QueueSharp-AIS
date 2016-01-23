<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDialogAppointment
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
        Me.btnAppOK = New System.Windows.Forms.Button
        Me.btnAppCancel = New System.Windows.Forms.Button
        Me.lblMsg = New System.Windows.Forms.Label
        Me.btnAppBack = New System.Windows.Forms.Button
        Me.btnAppNew = New System.Windows.Forms.Button
        Me.PanelMsg.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelMsg
        '
        Me.PanelMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PanelMsg.BackgroundImage = Global.Kiosk.My.Resources.Resources.msg
        Me.PanelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelMsg.Controls.Add(Me.btnAppOK)
        Me.PanelMsg.Controls.Add(Me.btnAppCancel)
        Me.PanelMsg.Controls.Add(Me.lblMsg)
        Me.PanelMsg.Controls.Add(Me.btnAppBack)
        Me.PanelMsg.Controls.Add(Me.btnAppNew)
        Me.PanelMsg.Location = New System.Drawing.Point(0, 0)
        Me.PanelMsg.Name = "PanelMsg"
        Me.PanelMsg.Size = New System.Drawing.Size(400, 445)
        Me.PanelMsg.TabIndex = 54
        Me.PanelMsg.Visible = False
        '
        'btnAppOK
        '
        Me.btnAppOK.BackColor = System.Drawing.Color.Transparent
        Me.btnAppOK.FlatAppearance.BorderSize = 0
        Me.btnAppOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppOK.Location = New System.Drawing.Point(89, 379)
        Me.btnAppOK.Name = "btnAppOK"
        Me.btnAppOK.Size = New System.Drawing.Size(82, 60)
        Me.btnAppOK.TabIndex = 3
        Me.btnAppOK.Text = "ตกลง"
        Me.btnAppOK.UseVisualStyleBackColor = False
        '
        'btnAppCancel
        '
        Me.btnAppCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnAppCancel.FlatAppearance.BorderSize = 0
        Me.btnAppCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppCancel.Location = New System.Drawing.Point(170, 378)
        Me.btnAppCancel.Name = "btnAppCancel"
        Me.btnAppCancel.Size = New System.Drawing.Size(125, 60)
        Me.btnAppCancel.TabIndex = 4
        Me.btnAppCancel.Text = "ยกเลิกการจอง"
        Me.btnAppCancel.UseVisualStyleBackColor = False
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(17, 14)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(367, 361)
        Me.lblMsg.TabIndex = 0
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAppBack
        '
        Me.btnAppBack.BackColor = System.Drawing.Color.Transparent
        Me.btnAppBack.FlatAppearance.BorderSize = 0
        Me.btnAppBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppBack.Location = New System.Drawing.Point(285, 378)
        Me.btnAppBack.Name = "btnAppBack"
        Me.btnAppBack.Size = New System.Drawing.Size(112, 60)
        Me.btnAppBack.TabIndex = 1
        Me.btnAppBack.Text = "หน้าหลัก"
        Me.btnAppBack.UseVisualStyleBackColor = False
        '
        'btnAppNew
        '
        Me.btnAppNew.BackColor = System.Drawing.Color.Transparent
        Me.btnAppNew.FlatAppearance.BorderSize = 0
        Me.btnAppNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAppNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAppNew.Location = New System.Drawing.Point(173, 379)
        Me.btnAppNew.Name = "btnAppNew"
        Me.btnAppNew.Size = New System.Drawing.Size(111, 60)
        Me.btnAppNew.TabIndex = 2
        Me.btnAppNew.Text = "รับคิวใหม่"
        Me.btnAppNew.UseVisualStyleBackColor = False
        '
        'frmDialogAppointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 445)
        Me.Controls.Add(Me.PanelMsg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDialogAppointment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.PanelMsg.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelMsg As System.Windows.Forms.Panel
    Friend WithEvents btnAppOK As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents btnAppBack As System.Windows.Forms.Button
    Friend WithEvents btnAppCancel As System.Windows.Forms.Button
    Friend WithEvents btnAppNew As System.Windows.Forms.Button
End Class
