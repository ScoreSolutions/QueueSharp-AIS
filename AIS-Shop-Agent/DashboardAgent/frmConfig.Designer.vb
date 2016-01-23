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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gbMainServer = New System.Windows.Forms.GroupBox
        Me.btmMainTestConnect = New System.Windows.Forms.Button
        Me.txtMainPass = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMainUser = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtMainDB = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtMainServer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnDisplayTestConnect = New System.Windows.Forms.Button
        Me.txtDisplayPass = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDisplayUser = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDisplayDBName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDisplayServer = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.numRefleshEvery = New System.Windows.Forms.NumericUpDown
        Me.Label11 = New System.Windows.Forms.Label
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gbMainServer.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numRefleshEvery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 12)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbMainServer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(737, 155)
        Me.SplitContainer1.SplitterDistance = 365
        Me.SplitContainer1.TabIndex = 2
        '
        'gbMainServer
        '
        Me.gbMainServer.Controls.Add(Me.btmMainTestConnect)
        Me.gbMainServer.Controls.Add(Me.txtMainPass)
        Me.gbMainServer.Controls.Add(Me.Label4)
        Me.gbMainServer.Controls.Add(Me.txtMainUser)
        Me.gbMainServer.Controls.Add(Me.Label3)
        Me.gbMainServer.Controls.Add(Me.txtMainDB)
        Me.gbMainServer.Controls.Add(Me.Label2)
        Me.gbMainServer.Controls.Add(Me.txtMainServer)
        Me.gbMainServer.Controls.Add(Me.Label1)
        Me.gbMainServer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainServer.Location = New System.Drawing.Point(0, 0)
        Me.gbMainServer.Name = "gbMainServer"
        Me.gbMainServer.Size = New System.Drawing.Size(365, 155)
        Me.gbMainServer.TabIndex = 1
        Me.gbMainServer.TabStop = False
        Me.gbMainServer.Text = "Main Server Database"
        '
        'btmMainTestConnect
        '
        Me.btmMainTestConnect.Location = New System.Drawing.Point(85, 123)
        Me.btmMainTestConnect.Name = "btmMainTestConnect"
        Me.btmMainTestConnect.Size = New System.Drawing.Size(107, 23)
        Me.btmMainTestConnect.TabIndex = 16
        Me.btmMainTestConnect.Text = "Test Connected"
        Me.btmMainTestConnect.UseVisualStyleBackColor = True
        '
        'txtMainPass
        '
        Me.txtMainPass.Location = New System.Drawing.Point(85, 97)
        Me.txtMainPass.Name = "txtMainPass"
        Me.txtMainPass.Size = New System.Drawing.Size(175, 20)
        Me.txtMainPass.TabIndex = 15
        Me.txtMainPass.Text = "1234"
        Me.txtMainPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMainPass.UseSystemPasswordChar = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Password"
        '
        'txtMainUser
        '
        Me.txtMainUser.Location = New System.Drawing.Point(85, 71)
        Me.txtMainUser.Name = "txtMainUser"
        Me.txtMainUser.Size = New System.Drawing.Size(175, 20)
        Me.txtMainUser.TabIndex = 13
        Me.txtMainUser.Text = "sa"
        Me.txtMainUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "User"
        '
        'txtMainDB
        '
        Me.txtMainDB.Location = New System.Drawing.Point(85, 45)
        Me.txtMainDB.Name = "txtMainDB"
        Me.txtMainDB.Size = New System.Drawing.Size(175, 20)
        Me.txtMainDB.TabIndex = 11
        Me.txtMainDB.Text = "Qisdb_PK"
        Me.txtMainDB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "DB Name"
        '
        'txtMainServer
        '
        Me.txtMainServer.Location = New System.Drawing.Point(85, 19)
        Me.txtMainServer.Name = "txtMainServer"
        Me.txtMainServer.Size = New System.Drawing.Size(175, 20)
        Me.txtMainServer.TabIndex = 9
        Me.txtMainServer.Text = "./LOCAL"
        Me.txtMainServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Main Server"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnDisplayTestConnect)
        Me.GroupBox2.Controls.Add(Me.txtDisplayPass)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtDisplayUser)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtDisplayDBName)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtDisplayServer)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(368, 155)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Display Server Database"
        '
        'btnDisplayTestConnect
        '
        Me.btnDisplayTestConnect.Location = New System.Drawing.Point(93, 123)
        Me.btnDisplayTestConnect.Name = "btnDisplayTestConnect"
        Me.btnDisplayTestConnect.Size = New System.Drawing.Size(107, 23)
        Me.btnDisplayTestConnect.TabIndex = 16
        Me.btnDisplayTestConnect.Text = "Test Connected"
        Me.btnDisplayTestConnect.UseVisualStyleBackColor = True
        '
        'txtDisplayPass
        '
        Me.txtDisplayPass.Location = New System.Drawing.Point(93, 97)
        Me.txtDisplayPass.Name = "txtDisplayPass"
        Me.txtDisplayPass.Size = New System.Drawing.Size(156, 20)
        Me.txtDisplayPass.TabIndex = 15
        Me.txtDisplayPass.Text = "1234"
        Me.txtDisplayPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDisplayPass.UseSystemPasswordChar = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Password"
        '
        'txtDisplayUser
        '
        Me.txtDisplayUser.Location = New System.Drawing.Point(93, 71)
        Me.txtDisplayUser.Name = "txtDisplayUser"
        Me.txtDisplayUser.Size = New System.Drawing.Size(156, 20)
        Me.txtDisplayUser.TabIndex = 13
        Me.txtDisplayUser.Text = "sa"
        Me.txtDisplayUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "User"
        '
        'txtDisplayDBName
        '
        Me.txtDisplayDBName.Location = New System.Drawing.Point(93, 45)
        Me.txtDisplayDBName.Name = "txtDisplayDBName"
        Me.txtDisplayDBName.Size = New System.Drawing.Size(156, 20)
        Me.txtDisplayDBName.TabIndex = 11
        Me.txtDisplayDBName.Text = "Qisdb_PK"
        Me.txtDisplayDBName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "DB Name"
        '
        'txtDisplayServer
        '
        Me.txtDisplayServer.Location = New System.Drawing.Point(93, 19)
        Me.txtDisplayServer.Name = "txtDisplayServer"
        Me.txtDisplayServer.Size = New System.Drawing.Size(156, 20)
        Me.txtDisplayServer.TabIndex = 9
        Me.txtDisplayServer.Text = "./LOCAL"
        Me.txtDisplayServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Display Server"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(278, 173)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 60)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.numRefleshEvery)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 173)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 60)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "XML Flash"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(198, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Minute(s)"
        '
        'numRefleshEvery
        '
        Me.numRefleshEvery.Location = New System.Drawing.Point(140, 23)
        Me.numRefleshEvery.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numRefleshEvery.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numRefleshEvery.Name = "numRefleshEvery"
        Me.numRefleshEvery.Size = New System.Drawing.Size(52, 20)
        Me.numRefleshEvery.TabIndex = 18
        Me.numRefleshEvery.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numRefleshEvery.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(24, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(102, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Refresh Flash Every"
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(761, 247)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmConfig"
        Me.Text = "Config Dashboard"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.gbMainServer.ResumeLayout(False)
        Me.gbMainServer.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numRefleshEvery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gbMainServer As System.Windows.Forms.GroupBox
    Friend WithEvents txtMainPass As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMainUser As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMainDB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMainServer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDisplayPass As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDisplayUser As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDisplayDBName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDisplayServer As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btmMainTestConnect As System.Windows.Forms.Button
    Friend WithEvents btnDisplayTestConnect As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents numRefleshEvery As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
