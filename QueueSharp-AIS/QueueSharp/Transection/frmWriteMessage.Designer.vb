<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWriteMessage
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Grouper1 = New CodeVendor.Controls.Grouper
        Me.Grouper4 = New CodeVendor.Controls.Grouper
        Me.GridCounter = New System.Windows.Forms.DataGridView
        Me.counter_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cb = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.counter_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Grouper2 = New CodeVendor.Controls.Grouper
        Me.btnSave = New System.Windows.Forms.Button
        Me.txtMsg = New System.Windows.Forms.TextBox
        Me.Grouper1.SuspendLayout()
        Me.Grouper4.SuspendLayout()
        CType(Me.GridCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grouper2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grouper1
        '
        Me.Grouper1.BackgroundColor = System.Drawing.Color.White
        Me.Grouper1.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.Grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper1.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper1.BorderThickness = 1.0!
        Me.Grouper1.Controls.Add(Me.Grouper4)
        Me.Grouper1.Controls.Add(Me.Grouper2)
        Me.Grouper1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper1.GroupImage = Nothing
        Me.Grouper1.GroupTitle = ""
        Me.Grouper1.Location = New System.Drawing.Point(9, -3)
        Me.Grouper1.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper1.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper1.Name = "Grouper1"
        Me.Grouper1.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper1.PaintGroupBox = False
        Me.Grouper1.RoundCorners = 10
        Me.Grouper1.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper1.ShadowControl = True
        Me.Grouper1.ShadowThickness = 3
        Me.Grouper1.Size = New System.Drawing.Size(630, 300)
        Me.Grouper1.TabIndex = 24
        '
        'Grouper4
        '
        Me.Grouper4.BackgroundColor = System.Drawing.Color.White
        Me.Grouper4.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.Grouper4.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper4.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper4.BorderThickness = 1.0!
        Me.Grouper4.Controls.Add(Me.GridCounter)
        Me.Grouper4.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper4.GroupImage = Nothing
        Me.Grouper4.GroupTitle = ""
        Me.Grouper4.Location = New System.Drawing.Point(401, 13)
        Me.Grouper4.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper4.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper4.Name = "Grouper4"
        Me.Grouper4.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper4.PaintGroupBox = False
        Me.Grouper4.RoundCorners = 10
        Me.Grouper4.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper4.ShadowControl = True
        Me.Grouper4.ShadowThickness = 3
        Me.Grouper4.Size = New System.Drawing.Size(214, 269)
        Me.Grouper4.TabIndex = 26
        '
        'GridCounter
        '
        Me.GridCounter.AllowUserToAddRows = False
        Me.GridCounter.AllowUserToDeleteRows = False
        Me.GridCounter.AllowUserToResizeColumns = False
        Me.GridCounter.AllowUserToResizeRows = False
        Me.GridCounter.BackgroundColor = System.Drawing.Color.LightGray
        Me.GridCounter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridCounter.ColumnHeadersVisible = False
        Me.GridCounter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.counter_id, Me.cb, Me.counter_name})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridCounter.DefaultCellStyle = DataGridViewCellStyle4
        Me.GridCounter.Location = New System.Drawing.Point(14, 23)
        Me.GridCounter.MultiSelect = False
        Me.GridCounter.Name = "GridCounter"
        Me.GridCounter.RowHeadersVisible = False
        Me.GridCounter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridCounter.Size = New System.Drawing.Size(182, 231)
        Me.GridCounter.TabIndex = 7
        '
        'counter_id
        '
        Me.counter_id.DataPropertyName = "id"
        Me.counter_id.HeaderText = "counter_id"
        Me.counter_id.Name = "counter_id"
        Me.counter_id.Visible = False
        Me.counter_id.Width = 5
        '
        'cb
        '
        Me.cb.HeaderText = "cb"
        Me.cb.Name = "cb"
        Me.cb.Width = 20
        '
        'counter_name
        '
        Me.counter_name.DataPropertyName = "counter_name"
        Me.counter_name.HeaderText = "counter_name"
        Me.counter_name.Name = "counter_name"
        Me.counter_name.ReadOnly = True
        Me.counter_name.Width = 158
        '
        'Grouper2
        '
        Me.Grouper2.BackgroundColor = System.Drawing.Color.White
        Me.Grouper2.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.Grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper2.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper2.BorderThickness = 1.0!
        Me.Grouper2.Controls.Add(Me.btnSave)
        Me.Grouper2.Controls.Add(Me.txtMsg)
        Me.Grouper2.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper2.GroupImage = Nothing
        Me.Grouper2.GroupTitle = ""
        Me.Grouper2.Location = New System.Drawing.Point(13, 13)
        Me.Grouper2.Margin = New System.Windows.Forms.Padding(4)
        Me.Grouper2.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Grouper2.Name = "Grouper2"
        Me.Grouper2.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Grouper2.PaintGroupBox = False
        Me.Grouper2.RoundCorners = 10
        Me.Grouper2.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper2.ShadowControl = True
        Me.Grouper2.ShadowThickness = 3
        Me.Grouper2.Size = New System.Drawing.Size(380, 269)
        Me.Grouper2.TabIndex = 24
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(253, 222)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 32)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "ส่งข้อความ"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtMsg
        '
        Me.txtMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMsg.Location = New System.Drawing.Point(14, 23)
        Me.txtMsg.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMsg.MaxLength = 1000
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.Size = New System.Drawing.Size(351, 190)
        Me.txtMsg.TabIndex = 0
        '
        'frmWriteMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(647, 304)
        Me.Controls.Add(Me.Grouper1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWriteMessage"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Message"
        Me.Grouper1.ResumeLayout(False)
        Me.Grouper4.ResumeLayout(False)
        CType(Me.GridCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Grouper2.ResumeLayout(False)
        Me.Grouper2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grouper1 As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper4 As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper2 As CodeVendor.Controls.Grouper
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents GridCounter As System.Windows.Forms.DataGridView
    Friend WithEvents counter_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cb As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents counter_name As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
