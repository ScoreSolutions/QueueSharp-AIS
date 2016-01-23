<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessReportsExcel
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMailContent = New System.Windows.Forms.TextBox
        Me.txtCC = New System.Windows.Forms.TextBox
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtDateFrom = New System.Windows.Forms.DateTimePicker
        Me.btnStart = New System.Windows.Forms.Button
        Me.lblTime = New System.Windows.Forms.Label
        Me.rdDialy = New System.Windows.Forms.RadioButton
        Me.rdMonth = New System.Windows.Forms.RadioButton
        Me.chkMonth = New System.Windows.Forms.ComboBox
        Me.txtYear = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkSendEmail = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtMailContent)
        Me.GroupBox1.Controls.Add(Me.txtCC)
        Me.GroupBox1.Controls.Add(Me.txtTo)
        Me.GroupBox1.Controls.Add(Me.txtSubject)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(520, 259)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Send Email"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Mail Content"
        '
        'txtMailContent
        '
        Me.txtMailContent.Location = New System.Drawing.Point(6, 107)
        Me.txtMailContent.Multiline = True
        Me.txtMailContent.Name = "txtMailContent"
        Me.txtMailContent.Size = New System.Drawing.Size(508, 161)
        Me.txtMailContent.TabIndex = 6
        '
        'txtCC
        '
        Me.txtCC.Location = New System.Drawing.Point(67, 65)
        Me.txtCC.Name = "txtCC"
        Me.txtCC.Size = New System.Drawing.Size(447, 20)
        Me.txtCC.TabIndex = 5
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(67, 43)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(447, 20)
        Me.txtTo.TabIndex = 4
        Me.txtTo.Text = "rosaratj@ais.co.th; yanitav@ais.co.th; nattapol@scoresolutions.co.th"
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(67, 20)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(447, 20)
        Me.txtSubject.TabIndex = 3
        Me.txtSubject.Text = "Ad hoc Staff Attendance Report"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Subject :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "CC :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "To :"
        '
        'dtDateTo
        '
        Me.dtDateTo.Location = New System.Drawing.Point(262, 9)
        Me.dtDateTo.Name = "dtDateTo"
        Me.dtDateTo.Size = New System.Drawing.Size(144, 20)
        Me.dtDateTo.TabIndex = 8
        '
        'dtDateFrom
        '
        Me.dtDateFrom.Location = New System.Drawing.Point(75, 9)
        Me.dtDateFrom.Name = "dtDateFrom"
        Me.dtDateFrom.Size = New System.Drawing.Size(144, 20)
        Me.dtDateFrom.TabIndex = 7
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(226, 360)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 9
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(440, 13)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(0, 13)
        Me.lblTime.TabIndex = 10
        '
        'rdDialy
        '
        Me.rdDialy.AutoSize = True
        Me.rdDialy.Checked = True
        Me.rdDialy.Location = New System.Drawing.Point(21, 12)
        Me.rdDialy.Name = "rdDialy"
        Me.rdDialy.Size = New System.Drawing.Size(48, 17)
        Me.rdDialy.TabIndex = 11
        Me.rdDialy.TabStop = True
        Me.rdDialy.Text = "Dialy"
        Me.rdDialy.UseVisualStyleBackColor = True
        '
        'rdMonth
        '
        Me.rdMonth.AutoSize = True
        Me.rdMonth.Location = New System.Drawing.Point(21, 46)
        Me.rdMonth.Name = "rdMonth"
        Me.rdMonth.Size = New System.Drawing.Size(55, 17)
        Me.rdMonth.TabIndex = 12
        Me.rdMonth.TabStop = True
        Me.rdMonth.Text = "Month"
        Me.rdMonth.UseVisualStyleBackColor = True
        '
        'chkMonth
        '
        Me.chkMonth.FormattingEnabled = True
        Me.chkMonth.Location = New System.Drawing.Point(75, 42)
        Me.chkMonth.Name = "chkMonth"
        Me.chkMonth.Size = New System.Drawing.Size(144, 21)
        Me.chkMonth.TabIndex = 13
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(262, 42)
        Me.txtYear.MaxLength = 4
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(100, 20)
        Me.txtYear.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(229, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Year"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(368, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = " พ.ศ."
        '
        'chkSendEmail
        '
        Me.chkSendEmail.AutoSize = True
        Me.chkSendEmail.Location = New System.Drawing.Point(75, 73)
        Me.chkSendEmail.Name = "chkSendEmail"
        Me.chkSendEmail.Size = New System.Drawing.Size(79, 17)
        Me.chkSendEmail.TabIndex = 17
        Me.chkSendEmail.Text = "Send Email"
        Me.chkSendEmail.UseVisualStyleBackColor = True
        '
        'frmProcessReportsExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 392)
        Me.Controls.Add(Me.chkSendEmail)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.chkMonth)
        Me.Controls.Add(Me.rdMonth)
        Me.Controls.Add(Me.rdDialy)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.dtDateTo)
        Me.Controls.Add(Me.dtDateFrom)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmProcessReportsExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMailContent As System.Windows.Forms.TextBox
    Friend WithEvents txtCC As System.Windows.Forms.TextBox
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents rdDialy As System.Windows.Forms.RadioButton
    Friend WithEvents rdMonth As System.Windows.Forms.RadioButton
    Friend WithEvents chkMonth As System.Windows.Forms.ComboBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkSendEmail As System.Windows.Forms.CheckBox

End Class
