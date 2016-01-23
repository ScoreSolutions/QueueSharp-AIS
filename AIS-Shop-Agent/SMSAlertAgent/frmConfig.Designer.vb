<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btnSave = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.colID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colServiceName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colWaitingSec = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtSMSEng = New System.Windows.Forms.RichTextBox
        Me.txtSMSThai = New System.Windows.Forms.RichTextBox
        Me.btnSaveSMS = New System.Windows.Forms.Button
        Me.lstSMSParam = New System.Windows.Forms.ListBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 215)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 7)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(690, 270)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Controls.Add(Me.btnSave)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(682, 244)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Waiting Time"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colID, Me.colServiceName, Me.colWaitingSec})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(676, 206)
        Me.DataGridView1.TabIndex = 6
        '
        'colID
        '
        Me.colID.DataPropertyName = "id"
        Me.colID.HeaderText = "id"
        Me.colID.Name = "colID"
        Me.colID.Visible = False
        '
        'colServiceName
        '
        Me.colServiceName.DataPropertyName = "item_name"
        Me.colServiceName.HeaderText = "Service Name"
        Me.colServiceName.Name = "colServiceName"
        Me.colServiceName.ReadOnly = True
        Me.colServiceName.Width = 150
        '
        'colWaitingSec
        '
        Me.colWaitingSec.DataPropertyName = "waiting_sec"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colWaitingSec.DefaultCellStyle = DataGridViewCellStyle2
        Me.colWaitingSec.HeaderText = "Waiting Second"
        Me.colWaitingSec.Name = "colWaitingSec"
        Me.colWaitingSec.Width = 150
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.Gainsboro
        Me.TabPage2.Controls.Add(Me.txtSMSEng)
        Me.TabPage2.Controls.Add(Me.txtSMSThai)
        Me.TabPage2.Controls.Add(Me.btnSaveSMS)
        Me.TabPage2.Controls.Add(Me.lstSMSParam)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(682, 244)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "SMS Language"
        '
        'txtSMSEng
        '
        Me.txtSMSEng.EnableAutoDragDrop = True
        Me.txtSMSEng.Location = New System.Drawing.Point(399, 24)
        Me.txtSMSEng.Name = "txtSMSEng"
        Me.txtSMSEng.Size = New System.Drawing.Size(279, 184)
        Me.txtSMSEng.TabIndex = 6
        Me.txtSMSEng.Text = ""
        '
        'txtSMSThai
        '
        Me.txtSMSThai.EnableAutoDragDrop = True
        Me.txtSMSThai.Location = New System.Drawing.Point(3, 24)
        Me.txtSMSThai.Name = "txtSMSThai"
        Me.txtSMSThai.Size = New System.Drawing.Size(279, 184)
        Me.txtSMSThai.TabIndex = 5
        Me.txtSMSThai.Text = ""
        '
        'btnSaveSMS
        '
        Me.btnSaveSMS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSMS.Location = New System.Drawing.Point(6, 215)
        Me.btnSaveSMS.Name = "btnSaveSMS"
        Me.btnSaveSMS.Size = New System.Drawing.Size(112, 23)
        Me.btnSaveSMS.TabIndex = 4
        Me.btnSaveSMS.Text = "Save"
        Me.btnSaveSMS.UseVisualStyleBackColor = True
        '
        'lstSMSParam
        '
        Me.lstSMSParam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstSMSParam.FormattingEnabled = True
        Me.lstSMSParam.Location = New System.Drawing.Point(288, 24)
        Me.lstSMSParam.Name = "lstSMSParam"
        Me.lstSMSParam.Size = New System.Drawing.Size(105, 184)
        Me.lstSMSParam.TabIndex = 1
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 289)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmConfig"
        Me.Text = "Config SMS Alert"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents colID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colServiceName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWaitingSec As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lstSMSParam As System.Windows.Forms.ListBox
    Friend WithEvents btnSaveSMS As System.Windows.Forms.Button
    Friend WithEvents txtSMSEng As System.Windows.Forms.RichTextBox
    Friend WithEvents txtSMSThai As System.Windows.Forms.RichTextBox
End Class
