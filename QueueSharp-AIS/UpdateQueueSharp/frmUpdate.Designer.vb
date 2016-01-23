<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdate))
        Me.BackgroundWorkerProgram = New System.ComponentModel.BackgroundWorker
        Me.PgLogo = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.btnINI = New System.Windows.Forms.Button
        Me.lnlSetting = New System.Windows.Forms.LinkLabel
        Me.gbProxy = New System.Windows.Forms.GroupBox
        Me.btnEXE = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.txtEXE = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtINI = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.sfdEXE = New System.Windows.Forms.SaveFileDialog
        Me.sfdINI = New System.Windows.Forms.SaveFileDialog
        Me.timerCheckProcess = New System.Windows.Forms.Timer(Me.components)
        Me.bw = New System.ComponentModel.BackgroundWorker
        CType(Me.PgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbProxy.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackgroundWorkerProgram
        '
        Me.BackgroundWorkerProgram.WorkerReportsProgress = True
        Me.BackgroundWorkerProgram.WorkerSupportsCancellation = True
        '
        'PgLogo
        '
        Me.PgLogo.BackgroundImage = CType(resources.GetObject("PgLogo.BackgroundImage"), System.Drawing.Image)
        Me.PgLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PgLogo.Location = New System.Drawing.Point(7, 8)
        Me.PgLogo.Name = "PgLogo"
        Me.PgLogo.Size = New System.Drawing.Size(138, 126)
        Me.PgLogo.TabIndex = 38
        Me.PgLogo.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Crimson
        Me.Label1.Location = New System.Drawing.Point(240, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(219, 19)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "ตรวจสอบการปรับปรุงเวอร์ชัน"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.Color.White
        Me.txtStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtStatus.ForeColor = System.Drawing.Color.DimGray
        Me.txtStatus.Location = New System.Drawing.Point(151, 31)
        Me.txtStatus.Multiline = True
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(308, 124)
        Me.txtStatus.TabIndex = 43
        Me.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnINI
        '
        Me.btnINI.Image = CType(resources.GetObject("btnINI.Image"), System.Drawing.Image)
        Me.btnINI.Location = New System.Drawing.Point(420, 15)
        Me.btnINI.Name = "btnINI"
        Me.btnINI.Size = New System.Drawing.Size(26, 23)
        Me.btnINI.TabIndex = 45
        Me.btnINI.UseVisualStyleBackColor = True
        '
        'lnlSetting
        '
        Me.lnlSetting.AutoSize = True
        Me.lnlSetting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lnlSetting.Location = New System.Drawing.Point(45, 141)
        Me.lnlSetting.Name = "lnlSetting"
        Me.lnlSetting.Size = New System.Drawing.Size(47, 14)
        Me.lnlSetting.TabIndex = 47
        Me.lnlSetting.TabStop = True
        Me.lnlSetting.Text = "Setting"
        '
        'gbProxy
        '
        Me.gbProxy.Controls.Add(Me.btnEXE)
        Me.gbProxy.Controls.Add(Me.btnApply)
        Me.gbProxy.Controls.Add(Me.txtEXE)
        Me.gbProxy.Controls.Add(Me.btnINI)
        Me.gbProxy.Controls.Add(Me.Label4)
        Me.gbProxy.Controls.Add(Me.txtINI)
        Me.gbProxy.Controls.Add(Me.Label2)
        Me.gbProxy.Location = New System.Drawing.Point(7, 155)
        Me.gbProxy.Name = "gbProxy"
        Me.gbProxy.Size = New System.Drawing.Size(452, 102)
        Me.gbProxy.TabIndex = 48
        Me.gbProxy.TabStop = False
        Me.gbProxy.Visible = False
        '
        'btnEXE
        '
        Me.btnEXE.Image = CType(resources.GetObject("btnEXE.Image"), System.Drawing.Image)
        Me.btnEXE.Location = New System.Drawing.Point(420, 44)
        Me.btnEXE.Name = "btnEXE"
        Me.btnEXE.Size = New System.Drawing.Size(26, 23)
        Me.btnEXE.TabIndex = 46
        Me.btnEXE.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnApply.Location = New System.Drawing.Point(347, 72)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(99, 21)
        Me.btnApply.TabIndex = 9
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'txtEXE
        '
        Me.txtEXE.BackColor = System.Drawing.Color.White
        Me.txtEXE.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtEXE.Location = New System.Drawing.Point(72, 45)
        Me.txtEXE.Name = "txtEXE"
        Me.txtEXE.ReadOnly = True
        Me.txtEXE.Size = New System.Drawing.Size(342, 21)
        Me.txtEXE.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Program"
        '
        'txtINI
        '
        Me.txtINI.BackColor = System.Drawing.Color.White
        Me.txtINI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtINI.Location = New System.Drawing.Point(72, 16)
        Me.txtINI.Name = "txtINI"
        Me.txtINI.ReadOnly = True
        Me.txtINI.Size = New System.Drawing.Size(342, 21)
        Me.txtINI.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Version"
        '
        'sfdEXE
        '
        Me.sfdEXE.OverwritePrompt = False
        '
        'sfdINI
        '
        Me.sfdINI.OverwritePrompt = False
        '
        'timerCheckProcess
        '
        '
        'bw
        '
        '
        'frmUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(468, 162)
        Me.Controls.Add(Me.lnlSetting)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PgLogo)
        Me.Controls.Add(Me.gbProxy)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Check for updater"
        Me.TopMost = True
        CType(Me.PgLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbProxy.ResumeLayout(False)
        Me.gbProxy.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BackgroundWorkerProgram As System.ComponentModel.BackgroundWorker
    Friend WithEvents PgLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnINI As System.Windows.Forms.Button
    Friend WithEvents lnlSetting As System.Windows.Forms.LinkLabel
    Friend WithEvents gbProxy As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEXE As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtINI As System.Windows.Forms.TextBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnEXE As System.Windows.Forms.Button
    Friend WithEvents sfdEXE As System.Windows.Forms.SaveFileDialog
    Friend WithEvents sfdINI As System.Windows.Forms.SaveFileDialog
    Friend WithEvents timerCheckProcess As System.Windows.Forms.Timer
    Friend WithEvents bw As System.ComponentModel.BackgroundWorker
End Class
