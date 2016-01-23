<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceCancel
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.RadioHold = New System.Windows.Forms.RadioButton
        Me.RadioCancelService = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New CodeVendor.Controls.Grouper
        Me.btnOK = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbSelectAll = New System.Windows.Forms.CheckBox
        Me.GridItem = New System.Windows.Forms.DataGridView
        Me.item_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cb = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.item_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtCancelAll = New System.Windows.Forms.TextBox
        Me.txtHoldTime = New System.Windows.Forms.TextBox
        Me.btn15M = New System.Windows.Forms.Button
        Me.btn10M = New System.Windows.Forms.Button
        Me.btn5M = New System.Windows.Forms.Button
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GridItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioHold
        '
        Me.RadioHold.AutoSize = True
        Me.RadioHold.Checked = True
        Me.RadioHold.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RadioHold.Enabled = False
        Me.RadioHold.ForeColor = System.Drawing.Color.Black
        Me.RadioHold.Location = New System.Drawing.Point(26, 26)
        Me.RadioHold.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioHold.Name = "RadioHold"
        Me.RadioHold.Size = New System.Drawing.Size(128, 22)
        Me.RadioHold.TabIndex = 0
        Me.RadioHold.TabStop = True
        Me.RadioHold.Text = "Hold this queue"
        Me.RadioHold.UseVisualStyleBackColor = True
        Me.RadioHold.Visible = False
        '
        'RadioCancelService
        '
        Me.RadioCancelService.AutoSize = True
        Me.RadioCancelService.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RadioCancelService.ForeColor = System.Drawing.Color.Black
        Me.RadioCancelService.Location = New System.Drawing.Point(26, 70)
        Me.RadioCancelService.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioCancelService.Name = "RadioCancelService"
        Me.RadioCancelService.Size = New System.Drawing.Size(165, 22)
        Me.RadioCancelService.TabIndex = 2
        Me.RadioCancelService.Text = "Cancel This Services"
        Me.RadioCancelService.UseVisualStyleBackColor = True
        Me.RadioCancelService.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundColor = System.Drawing.Color.White
        Me.GroupBox1.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBox1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBox1.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBox1.BorderThickness = 1.0!
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.GridItem)
        Me.GroupBox1.Controls.Add(Me.txtCancelAll)
        Me.GroupBox1.Controls.Add(Me.txtHoldTime)
        Me.GroupBox1.Controls.Add(Me.btn15M)
        Me.GroupBox1.Controls.Add(Me.btn10M)
        Me.GroupBox1.Controls.Add(Me.btn5M)
        Me.GroupBox1.Controls.Add(Me.RadioCancelService)
        Me.GroupBox1.Controls.Add(Me.RadioHold)
        Me.GroupBox1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBox1.ForeColor = System.Drawing.Color.DimGray
        Me.GroupBox1.GroupImage = Nothing
        Me.GroupBox1.GroupTitle = ""
        Me.GroupBox1.Location = New System.Drawing.Point(6, -2)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.MinimumSize = New System.Drawing.Size(2, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBox1.PaintGroupBox = False
        Me.GroupBox1.RoundCorners = 10
        Me.GroupBox1.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBox1.ShadowControl = True
        Me.GroupBox1.ShadowThickness = 3
        Me.GroupBox1.Size = New System.Drawing.Size(519, 277)
        Me.GroupBox1.TabIndex = 34
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnOK.ForeColor = System.Drawing.Color.Navy
        Me.btnOK.Image = Global.QueueSharp.My.Resources.Resources.scroll_ok
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOK.Location = New System.Drawing.Point(146, 218)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(227, 50)
        Me.btnOK.TabIndex = 43
        Me.btnOK.Text = "       Cancel Service"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(238, 22)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(266, 26)
        Me.Panel2.TabIndex = 42
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 18)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Remark"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cbSelectAll)
        Me.Panel1.Location = New System.Drawing.Point(13, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(219, 26)
        Me.Panel1.TabIndex = 41
        '
        'cbSelectAll
        '
        Me.cbSelectAll.AutoSize = True
        Me.cbSelectAll.BackColor = System.Drawing.Color.Transparent
        Me.cbSelectAll.ForeColor = System.Drawing.Color.Black
        Me.cbSelectAll.Location = New System.Drawing.Point(4, 2)
        Me.cbSelectAll.Name = "cbSelectAll"
        Me.cbSelectAll.Size = New System.Drawing.Size(87, 22)
        Me.cbSelectAll.TabIndex = 39
        Me.cbSelectAll.Text = "Select All"
        Me.cbSelectAll.UseVisualStyleBackColor = False
        '
        'GridItem
        '
        Me.GridItem.AllowUserToAddRows = False
        Me.GridItem.AllowUserToDeleteRows = False
        Me.GridItem.AllowUserToResizeColumns = False
        Me.GridItem.AllowUserToResizeRows = False
        Me.GridItem.BackgroundColor = System.Drawing.Color.White
        Me.GridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridItem.ColumnHeadersVisible = False
        Me.GridItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.item_id, Me.cb, Me.item_name})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridItem.DefaultCellStyle = DataGridViewCellStyle1
        Me.GridItem.GridColor = System.Drawing.Color.White
        Me.GridItem.Location = New System.Drawing.Point(13, 46)
        Me.GridItem.MultiSelect = False
        Me.GridItem.Name = "GridItem"
        Me.GridItem.RowHeadersVisible = False
        Me.GridItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridItem.Size = New System.Drawing.Size(219, 166)
        Me.GridItem.TabIndex = 38
        '
        'item_id
        '
        Me.item_id.DataPropertyName = "id"
        Me.item_id.HeaderText = "item_id"
        Me.item_id.Name = "item_id"
        Me.item_id.Visible = False
        Me.item_id.Width = 5
        '
        'cb
        '
        Me.cb.HeaderText = "cb"
        Me.cb.Name = "cb"
        Me.cb.Width = 20
        '
        'item_name
        '
        Me.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.item_name.DataPropertyName = "item_name"
        Me.item_name.HeaderText = "item_name"
        Me.item_name.Name = "item_name"
        Me.item_name.ReadOnly = True
        '
        'txtCancelAll
        '
        Me.txtCancelAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelAll.Location = New System.Drawing.Point(238, 46)
        Me.txtCancelAll.MaxLength = 500
        Me.txtCancelAll.Multiline = True
        Me.txtCancelAll.Name = "txtCancelAll"
        Me.txtCancelAll.Size = New System.Drawing.Size(266, 166)
        Me.txtCancelAll.TabIndex = 18
        '
        'txtHoldTime
        '
        Me.txtHoldTime.Enabled = False
        Me.txtHoldTime.Location = New System.Drawing.Point(451, 24)
        Me.txtHoldTime.Name = "txtHoldTime"
        Me.txtHoldTime.Size = New System.Drawing.Size(30, 24)
        Me.txtHoldTime.TabIndex = 16
        Me.txtHoldTime.Visible = False
        '
        'btn15M
        '
        Me.btn15M.BackColor = System.Drawing.Color.Gainsboro
        Me.btn15M.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn15M.Enabled = False
        Me.btn15M.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btn15M.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn15M.ForeColor = System.Drawing.Color.DimGray
        Me.btn15M.Location = New System.Drawing.Point(346, 21)
        Me.btn15M.Name = "btn15M"
        Me.btn15M.Size = New System.Drawing.Size(85, 34)
        Me.btn15M.TabIndex = 11
        Me.btn15M.Tag = "15"
        Me.btn15M.Text = "15 Minute"
        Me.btn15M.UseVisualStyleBackColor = False
        Me.btn15M.Visible = False
        '
        'btn10M
        '
        Me.btn10M.BackColor = System.Drawing.Color.Gainsboro
        Me.btn10M.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn10M.Enabled = False
        Me.btn10M.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btn10M.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn10M.ForeColor = System.Drawing.Color.DimGray
        Me.btn10M.Location = New System.Drawing.Point(255, 21)
        Me.btn10M.Name = "btn10M"
        Me.btn10M.Size = New System.Drawing.Size(85, 34)
        Me.btn10M.TabIndex = 10
        Me.btn10M.Tag = "10"
        Me.btn10M.Text = "10 Minute"
        Me.btn10M.UseVisualStyleBackColor = False
        Me.btn10M.Visible = False
        '
        'btn5M
        '
        Me.btn5M.BackColor = System.Drawing.Color.Red
        Me.btn5M.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn5M.Enabled = False
        Me.btn5M.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btn5M.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn5M.ForeColor = System.Drawing.Color.White
        Me.btn5M.Location = New System.Drawing.Point(164, 21)
        Me.btn5M.Name = "btn5M"
        Me.btn5M.Size = New System.Drawing.Size(85, 34)
        Me.btn5M.TabIndex = 9
        Me.btn5M.Tag = "5"
        Me.btn5M.Text = "5 Minute"
        Me.btn5M.UseVisualStyleBackColor = False
        Me.btn5M.Visible = False
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'frmServiceCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(529, 279)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmServiceCancel"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cancel Service"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadioHold As System.Windows.Forms.RadioButton
    Friend WithEvents RadioCancelService As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As CodeVendor.Controls.Grouper
    Friend WithEvents btn15M As System.Windows.Forms.Button
    Friend WithEvents btn10M As System.Windows.Forms.Button
    Friend WithEvents btn5M As System.Windows.Forms.Button
    Friend WithEvents txtHoldTime As System.Windows.Forms.TextBox
    Friend WithEvents txtCancelAll As System.Windows.Forms.TextBox
    Friend WithEvents GridItem As System.Windows.Forms.DataGridView
    Friend WithEvents cbSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents item_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cb As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents item_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Timer As System.Windows.Forms.Timer
End Class
