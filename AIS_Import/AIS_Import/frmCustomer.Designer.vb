<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomer
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTextFileName = New System.Windows.Forms.TextBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.lblStatus = New System.Windows.Forms.Label
        Me.btnSimulateData = New System.Windows.Forms.Button
        Me.lblTime = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnUpBirthDate = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Text File Name:"
        '
        'txtTextFileName
        '
        Me.txtTextFileName.Location = New System.Drawing.Point(138, 15)
        Me.txtTextFileName.Name = "txtTextFileName"
        Me.txtTextFileName.Size = New System.Drawing.Size(512, 20)
        Me.txtTextFileName.TabIndex = 1
        Me.txtTextFileName.Text = "D:\My Documents\AIS-Q\Source\Data Test V02.xls"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(138, 41)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 2
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(220, 41)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(94, 23)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "Import to Table"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 99)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(316, 310)
        Me.DataGridView1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(138, 71)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(512, 23)
        Me.ProgressBar1.TabIndex = 5
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(328, 42)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblStatus.TabIndex = 6
        '
        'btnSimulateData
        '
        Me.btnSimulateData.Location = New System.Drawing.Point(560, 40)
        Me.btnSimulateData.Name = "btnSimulateData"
        Me.btnSimulateData.Size = New System.Drawing.Size(90, 23)
        Me.btnSimulateData.TabIndex = 7
        Me.btnSimulateData.Text = "SimulateData"
        Me.btnSimulateData.UseVisualStyleBackColor = True
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTime.Location = New System.Drawing.Point(747, 18)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(76, 29)
        Me.lblTime.TabIndex = 8
        Me.lblTime.Text = "00:00"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(334, 100)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(548, 309)
        Me.RichTextBox1.TabIndex = 9
        Me.RichTextBox1.Text = ""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(807, 71)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnUpBirthDate
        '
        Me.btnUpBirthDate.Location = New System.Drawing.Point(321, 39)
        Me.btnUpBirthDate.Name = "btnUpBirthDate"
        Me.btnUpBirthDate.Size = New System.Drawing.Size(98, 23)
        Me.btnUpBirthDate.TabIndex = 11
        Me.btnUpBirthDate.Text = "Up BirthDate"
        Me.btnUpBirthDate.UseVisualStyleBackColor = True
        '
        'frmCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 421)
        Me.Controls.Add(Me.btnUpBirthDate)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.btnSimulateData)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.txtTextFileName)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmCustomer"
        Me.Text = "frmCustomer"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTextFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnSimulateData As System.Windows.Forms.Button
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnUpBirthDate As System.Windows.Forms.Button
End Class
