<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStaffSMS
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
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.lblrow_id = New System.Windows.Forms.Label
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.lblcounter = New System.Windows.Forms.Label
        Me.ButtonAdd = New System.Windows.Forms.Button
        Me.gridSMS = New System.Windows.Forms.DataGridView
        Me.txtSMS = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.NUP = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonUpdate = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBoxButton.SuspendLayout()
        CType(Me.gridSMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBoxButton
        '
        Me.GroupBoxButton.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxButton.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.GroupBoxButton.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxButton.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxButton.BorderThickness = 1.0!
        Me.GroupBoxButton.Controls.Add(Me.btnCancel)
        Me.GroupBoxButton.Controls.Add(Me.btnAdd)
        Me.GroupBoxButton.Controls.Add(Me.btnSave)
        Me.GroupBoxButton.Controls.Add(Me.lblrow_id)
        Me.GroupBoxButton.Controls.Add(Me.ButtonCancel)
        Me.GroupBoxButton.Controls.Add(Me.lblcounter)
        Me.GroupBoxButton.Controls.Add(Me.ButtonAdd)
        Me.GroupBoxButton.Controls.Add(Me.gridSMS)
        Me.GroupBoxButton.Controls.Add(Me.txtSMS)
        Me.GroupBoxButton.Controls.Add(Me.Label3)
        Me.GroupBoxButton.Controls.Add(Me.NUP)
        Me.GroupBoxButton.Controls.Add(Me.Label1)
        Me.GroupBoxButton.Controls.Add(Me.ButtonUpdate)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, -2)
        Me.GroupBoxButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(448, 397)
        Me.GroupBoxButton.TabIndex = 23
        '
        'lblrow_id
        '
        Me.lblrow_id.AutoSize = True
        Me.lblrow_id.Location = New System.Drawing.Point(405, 28)
        Me.lblrow_id.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblrow_id.Name = "lblrow_id"
        Me.lblrow_id.Size = New System.Drawing.Size(0, 18)
        Me.lblrow_id.TabIndex = 12
        Me.lblrow_id.Visible = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(270, 166)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(132, 32)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        Me.ButtonCancel.Visible = False
        '
        'lblcounter
        '
        Me.lblcounter.AutoSize = True
        Me.lblcounter.Location = New System.Drawing.Point(228, 112)
        Me.lblcounter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblcounter.Name = "lblcounter"
        Me.lblcounter.Size = New System.Drawing.Size(0, 18)
        Me.lblcounter.TabIndex = 9
        Me.lblcounter.Visible = False
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(130, 166)
        Me.ButtonAdd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(132, 32)
        Me.ButtonAdd.TabIndex = 8
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'gridSMS
        '
        Me.gridSMS.AllowUserToAddRows = False
        Me.gridSMS.AllowUserToDeleteRows = False
        Me.gridSMS.AllowUserToResizeColumns = False
        Me.gridSMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridSMS.Location = New System.Drawing.Point(26, 291)
        Me.gridSMS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gridSMS.Name = "gridSMS"
        Me.gridSMS.RowHeadersVisible = False
        Me.gridSMS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridSMS.Size = New System.Drawing.Size(396, 84)
        Me.gridSMS.TabIndex = 7
        '
        'txtSMS
        '
        Me.txtSMS.Enabled = False
        Me.txtSMS.Location = New System.Drawing.Point(137, 56)
        Me.txtSMS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSMS.MaxLength = 10
        Me.txtSMS.Name = "txtSMS"
        Me.txtSMS.Size = New System.Drawing.Size(196, 24)
        Me.txtSMS.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 59)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 18)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Send SMS To :"
        '
        'NUP
        '
        Me.NUP.Enabled = False
        Me.NUP.Location = New System.Drawing.Point(270, 26)
        Me.NUP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.NUP.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NUP.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NUP.Name = "NUP"
        Me.NUP.Size = New System.Drawing.Size(63, 24)
        Me.NUP.TabIndex = 1
        Me.NUP.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(239, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Time More Than Standard Time   X"
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(130, 112)
        Me.ButtonUpdate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(132, 32)
        Me.ButtonUpdate.TabIndex = 10
        Me.ButtonUpdate.Text = "Update"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        Me.ButtonUpdate.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = Global.QueueSharp.My.Resources.Resources.Close
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(285, 244)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 40)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAdd.Image = Global.QueueSharp.My.Resources.Resources.Add
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(93, 244)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 40)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.Text = "Add"
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(189, 244)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 40)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmStaffSMS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(458, 399)
        Me.Controls.Add(Me.GroupBoxButton)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStaffSMS"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Long Waiting Alert"
        Me.GroupBoxButton.ResumeLayout(False)
        Me.GroupBoxButton.PerformLayout()
        CType(Me.gridSMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents NUP As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSMS As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents gridSMS As System.Windows.Forms.DataGridView
    Friend WithEvents lblcounter As System.Windows.Forms.Label
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents lblrow_id As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
