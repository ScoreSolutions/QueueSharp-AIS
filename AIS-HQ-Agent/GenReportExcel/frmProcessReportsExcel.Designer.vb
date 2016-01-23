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
        Me.Button1 = New System.Windows.Forms.Button
        Me.dtDateFrom = New System.Windows.Forms.DateTimePicker
        Me.dtDateTo = New System.Windows.Forms.DateTimePicker
        Me.rdiDaily = New System.Windows.Forms.RadioButton
        Me.rdiAccumulate = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMailContent = New System.Windows.Forms.TextBox
        Me.txtCC = New System.Windows.Forms.TextBox
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkSendEmail = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(225, 363)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dtDateFrom
        '
        Me.dtDateFrom.Location = New System.Drawing.Point(28, 12)
        Me.dtDateFrom.Name = "dtDateFrom"
        Me.dtDateFrom.Size = New System.Drawing.Size(144, 20)
        Me.dtDateFrom.TabIndex = 1
        '
        'dtDateTo
        '
        Me.dtDateTo.Location = New System.Drawing.Point(215, 12)
        Me.dtDateTo.Name = "dtDateTo"
        Me.dtDateTo.Size = New System.Drawing.Size(144, 20)
        Me.dtDateTo.TabIndex = 2
        '
        'rdiDaily
        '
        Me.rdiDaily.AutoSize = True
        Me.rdiDaily.Checked = True
        Me.rdiDaily.Location = New System.Drawing.Point(28, 38)
        Me.rdiDaily.Name = "rdiDaily"
        Me.rdiDaily.Size = New System.Drawing.Size(48, 17)
        Me.rdiDaily.TabIndex = 3
        Me.rdiDaily.TabStop = True
        Me.rdiDaily.Text = "Daily"
        Me.rdiDaily.UseVisualStyleBackColor = True
        '
        'rdiAccumulate
        '
        Me.rdiAccumulate.AutoSize = True
        Me.rdiAccumulate.Location = New System.Drawing.Point(124, 38)
        Me.rdiAccumulate.Name = "rdiAccumulate"
        Me.rdiAccumulate.Size = New System.Drawing.Size(81, 17)
        Me.rdiAccumulate.TabIndex = 4
        Me.rdiAccumulate.Text = "Accumulate"
        Me.rdiAccumulate.UseVisualStyleBackColor = True
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
        Me.GroupBox1.Location = New System.Drawing.Point(5, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(520, 276)
        Me.GroupBox1.TabIndex = 5
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
        Me.txtSubject.Text = "AIS-QIS>Daily Report"
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
        'chkSendEmail
        '
        Me.chkSendEmail.AutoSize = True
        Me.chkSendEmail.Location = New System.Drawing.Point(124, 62)
        Me.chkSendEmail.Name = "chkSendEmail"
        Me.chkSendEmail.Size = New System.Drawing.Size(79, 17)
        Me.chkSendEmail.TabIndex = 6
        Me.chkSendEmail.Text = "Send Email"
        Me.chkSendEmail.UseVisualStyleBackColor = True
        '
        'frmProcessReportsExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 393)
        Me.Controls.Add(Me.chkSendEmail)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.rdiAccumulate)
        Me.Controls.Add(Me.rdiDaily)
        Me.Controls.Add(Me.dtDateTo)
        Me.Controls.Add(Me.dtDateFrom)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmProcessReportsExcel"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dtDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdiDaily As System.Windows.Forms.RadioButton
    Friend WithEvents rdiAccumulate As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMailContent As System.Windows.Forms.TextBox
    Friend WithEvents txtCC As System.Windows.Forms.TextBox
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSendEmail As System.Windows.Forms.CheckBox

End Class
