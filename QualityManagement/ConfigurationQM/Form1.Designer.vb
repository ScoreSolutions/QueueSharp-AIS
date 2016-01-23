<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.panelAgentPreview = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboMICAgent = New System.Windows.Forms.ComboBox
        Me.progMICAgent = New System.Windows.Forms.ProgressBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboAgentInCam = New System.Windows.Forms.ComboBox
        Me.btnAgentPrv = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.panelCustomerPreview = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboMICCustomer = New System.Windows.Forms.ComboBox
        Me.cboCustomerInCam = New System.Windows.Forms.ComboBox
        Me.progMICCustomer = New System.Windows.Forms.ProgressBar
        Me.btnCustomerPrv = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.SaveExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.panelAgentPreview)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cboMICAgent)
        Me.GroupBox1.Controls.Add(Me.progMICAgent)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboAgentInCam)
        Me.GroupBox1.Controls.Add(Me.btnAgentPrv)
        Me.GroupBox1.Location = New System.Drawing.Point(-1, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(327, 365)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Agent"
        '
        'panelAgentPreview
        '
        Me.panelAgentPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelAgentPreview.Location = New System.Drawing.Point(4, 51)
        Me.panelAgentPreview.Name = "panelAgentPreview"
        Me.panelAgentPreview.Size = New System.Drawing.Size(320, 240)
        Me.panelAgentPreview.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 307)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Input MIC"
        '
        'cboMICAgent
        '
        Me.cboMICAgent.FormattingEnabled = True
        Me.cboMICAgent.Location = New System.Drawing.Point(86, 303)
        Me.cboMICAgent.Name = "cboMICAgent"
        Me.cboMICAgent.Size = New System.Drawing.Size(173, 21)
        Me.cboMICAgent.TabIndex = 6
        '
        'progMICAgent
        '
        Me.progMICAgent.Location = New System.Drawing.Point(26, 339)
        Me.progMICAgent.Maximum = 10
        Me.progMICAgent.Name = "progMICAgent"
        Me.progMICAgent.Size = New System.Drawing.Size(281, 23)
        Me.progMICAgent.Step = 1
        Me.progMICAgent.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Input Camera"
        '
        'cboAgentInCam
        '
        Me.cboAgentInCam.FormattingEnabled = True
        Me.cboAgentInCam.Location = New System.Drawing.Point(121, 21)
        Me.cboAgentInCam.Name = "cboAgentInCam"
        Me.cboAgentInCam.Size = New System.Drawing.Size(138, 21)
        Me.cboAgentInCam.TabIndex = 3
        '
        'btnAgentPrv
        '
        Me.btnAgentPrv.Location = New System.Drawing.Point(261, 20)
        Me.btnAgentPrv.Name = "btnAgentPrv"
        Me.btnAgentPrv.Size = New System.Drawing.Size(60, 23)
        Me.btnAgentPrv.TabIndex = 2
        Me.btnAgentPrv.Text = "Preview"
        Me.btnAgentPrv.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.panelCustomerPreview)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cboMICCustomer)
        Me.GroupBox2.Controls.Add(Me.cboCustomerInCam)
        Me.GroupBox2.Controls.Add(Me.progMICCustomer)
        Me.GroupBox2.Controls.Add(Me.btnCustomerPrv)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(328, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(329, 365)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Customer"
        '
        'panelCustomerPreview
        '
        Me.panelCustomerPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelCustomerPreview.Location = New System.Drawing.Point(6, 51)
        Me.panelCustomerPreview.Name = "panelCustomerPreview"
        Me.panelCustomerPreview.Size = New System.Drawing.Size(320, 240)
        Me.panelCustomerPreview.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 307)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Input MIC"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Input Camera"
        '
        'cboMICCustomer
        '
        Me.cboMICCustomer.FormattingEnabled = True
        Me.cboMICCustomer.Location = New System.Drawing.Point(89, 303)
        Me.cboMICCustomer.Name = "cboMICCustomer"
        Me.cboMICCustomer.Size = New System.Drawing.Size(171, 21)
        Me.cboMICCustomer.TabIndex = 10
        '
        'cboCustomerInCam
        '
        Me.cboCustomerInCam.FormattingEnabled = True
        Me.cboCustomerInCam.Location = New System.Drawing.Point(122, 21)
        Me.cboCustomerInCam.Name = "cboCustomerInCam"
        Me.cboCustomerInCam.Size = New System.Drawing.Size(138, 21)
        Me.cboCustomerInCam.TabIndex = 2
        '
        'progMICCustomer
        '
        Me.progMICCustomer.Location = New System.Drawing.Point(26, 339)
        Me.progMICCustomer.Maximum = 10
        Me.progMICCustomer.Name = "progMICCustomer"
        Me.progMICCustomer.Size = New System.Drawing.Size(281, 23)
        Me.progMICCustomer.Step = 1
        Me.progMICCustomer.TabIndex = 9
        '
        'btnCustomerPrv
        '
        Me.btnCustomerPrv.Location = New System.Drawing.Point(264, 20)
        Me.btnCustomerPrv.Name = "btnCustomerPrv"
        Me.btnCustomerPrv.Size = New System.Drawing.Size(60, 23)
        Me.btnCustomerPrv.TabIndex = 1
        Me.btnCustomerPrv.Text = "Preview"
        Me.btnCustomerPrv.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveExitToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(655, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SaveExitToolStripMenuItem
        '
        Me.SaveExitToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.SaveExitToolStripMenuItem.Name = "SaveExitToolStripMenuItem"
        Me.SaveExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.SaveExitToolStripMenuItem.Text = "File"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkRed
        Me.Panel1.Location = New System.Drawing.Point(109, 340)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(2, 21)
        Me.Panel1.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkRed
        Me.Panel2.Location = New System.Drawing.Point(107, 340)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(2, 21)
        Me.Panel2.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(78, 326)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Require Meter"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(76, 326)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Require Meter"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Highlight
        Me.ClientSize = New System.Drawing.Size(655, 389)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboAgentInCam As System.Windows.Forms.ComboBox
    Friend WithEvents btnAgentPrv As System.Windows.Forms.Button
    Friend WithEvents cboCustomerInCam As System.Windows.Forms.ComboBox
    Friend WithEvents btnCustomerPrv As System.Windows.Forms.Button
    Friend WithEvents cboMICAgent As System.Windows.Forms.ComboBox
    Friend WithEvents progMICAgent As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboMICCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents progMICCustomer As System.Windows.Forms.ProgressBar
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SaveExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panelAgentPreview As System.Windows.Forms.Panel
    Friend WithEvents panelCustomerPreview As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
