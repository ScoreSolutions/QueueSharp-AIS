<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEndByService
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
        Me.TimerCountServed = New System.Windows.Forms.Timer(Me.components)
        Me.lblTime = New System.Windows.Forms.Label
        Me.FLP = New System.Windows.Forms.FlowLayoutPanel
        Me.Gp = New CodeVendor.Controls.Grouper
        Me.lblTimeS = New System.Windows.Forms.Label
        Me.btnCusInfo = New System.Windows.Forms.Button
        Me.PanelCus = New System.Windows.Forms.Panel
        Me.lblQueue = New System.Windows.Forms.Label
        Me.lblCustomerID = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnTop = New System.Windows.Forms.Button
        Me.Gp.SuspendLayout()
        Me.PanelCus.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimerCountServed
        '
        Me.TimerCountServed.Interval = 900
        '
        'lblTime
        '
        Me.lblTime.BackColor = System.Drawing.Color.Black
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.Red
        Me.lblTime.Location = New System.Drawing.Point(12, 78)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(247, 55)
        Me.lblTime.TabIndex = 3
        Me.lblTime.Text = "00:00"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLP
        '
        Me.FLP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FLP.BackColor = System.Drawing.Color.Transparent
        Me.FLP.Location = New System.Drawing.Point(9, 136)
        Me.FLP.Name = "FLP"
        Me.FLP.Size = New System.Drawing.Size(264, 306)
        Me.FLP.TabIndex = 4
        '
        'Gp
        '
        Me.Gp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Gp.BackgroundColor = System.Drawing.Color.White
        Me.Gp.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.Gp.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Gp.BorderColor = System.Drawing.Color.SteelBlue
        Me.Gp.BorderThickness = 1.0!
        Me.Gp.Controls.Add(Me.lblTimeS)
        Me.Gp.Controls.Add(Me.btnCusInfo)
        Me.Gp.Controls.Add(Me.PanelCus)
        Me.Gp.Controls.Add(Me.lblTime)
        Me.Gp.Controls.Add(Me.btnTop)
        Me.Gp.Controls.Add(Me.FLP)
        Me.Gp.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Gp.GroupImage = Nothing
        Me.Gp.GroupTitle = ""
        Me.Gp.Location = New System.Drawing.Point(2, -7)
        Me.Gp.Margin = New System.Windows.Forms.Padding(4)
        Me.Gp.MinimumSize = New System.Drawing.Size(2, 1)
        Me.Gp.Name = "Gp"
        Me.Gp.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.Gp.PaintGroupBox = False
        Me.Gp.RoundCorners = 10
        Me.Gp.ShadowColor = System.Drawing.Color.DarkGray
        Me.Gp.ShadowControl = True
        Me.Gp.ShadowThickness = 1
        Me.Gp.Size = New System.Drawing.Size(273, 141)
        Me.Gp.TabIndex = 24
        '
        'lblTimeS
        '
        Me.lblTimeS.BackColor = System.Drawing.Color.Black
        Me.lblTimeS.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTimeS.ForeColor = System.Drawing.Color.Red
        Me.lblTimeS.Location = New System.Drawing.Point(7, 16)
        Me.lblTimeS.Name = "lblTimeS"
        Me.lblTimeS.Size = New System.Drawing.Size(233, 55)
        Me.lblTimeS.TabIndex = 12
        Me.lblTimeS.Text = "00:00"
        Me.lblTimeS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTimeS.Visible = False
        '
        'btnCusInfo
        '
        Me.btnCusInfo.BackgroundImage = Global.QueueSharp.My.Resources.Resources.CustomerInfo
        Me.btnCusInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCusInfo.FlatAppearance.BorderSize = 0
        Me.btnCusInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCusInfo.Location = New System.Drawing.Point(243, 46)
        Me.btnCusInfo.Name = "btnCusInfo"
        Me.btnCusInfo.Size = New System.Drawing.Size(20, 20)
        Me.btnCusInfo.TabIndex = 11
        Me.btnCusInfo.UseVisualStyleBackColor = True
        '
        'PanelCus
        '
        Me.PanelCus.BackColor = System.Drawing.Color.White
        Me.PanelCus.Controls.Add(Me.lblQueue)
        Me.PanelCus.Controls.Add(Me.lblCustomerID)
        Me.PanelCus.Controls.Add(Me.Label10)
        Me.PanelCus.Controls.Add(Me.Label1)
        Me.PanelCus.Location = New System.Drawing.Point(12, 23)
        Me.PanelCus.Name = "PanelCus"
        Me.PanelCus.Size = New System.Drawing.Size(227, 46)
        Me.PanelCus.TabIndex = 9
        '
        'lblQueue
        '
        Me.lblQueue.AutoSize = True
        Me.lblQueue.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblQueue.ForeColor = System.Drawing.Color.SeaGreen
        Me.lblQueue.Location = New System.Drawing.Point(101, 5)
        Me.lblQueue.Name = "lblQueue"
        Me.lblQueue.Size = New System.Drawing.Size(44, 17)
        Me.lblQueue.TabIndex = 7
        Me.lblQueue.Text = "1001"
        '
        'lblCustomerID
        '
        Me.lblCustomerID.AutoSize = True
        Me.lblCustomerID.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomerID.ForeColor = System.Drawing.Color.SeaGreen
        Me.lblCustomerID.Location = New System.Drawing.Point(101, 24)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(98, 17)
        Me.lblCustomerID.TabIndex = 8
        Me.lblCustomerID.Text = "0809127127"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label10.Location = New System.Drawing.Point(7, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 17)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Queue No :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label1.Location = New System.Drawing.Point(7, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Mobile No :"
        '
        'btnTop
        '
        Me.btnTop.BackgroundImage = Global.QueueSharp.My.Resources.Resources.Top
        Me.btnTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTop.FlatAppearance.BorderSize = 0
        Me.btnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTop.Location = New System.Drawing.Point(243, 21)
        Me.btnTop.Name = "btnTop"
        Me.btnTop.Size = New System.Drawing.Size(20, 20)
        Me.btnTop.TabIndex = 10
        Me.btnTop.UseVisualStyleBackColor = True
        '
        'frmEndByService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Magenta
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(277, 135)
        Me.ControlBox = False
        Me.Controls.Add(Me.Gp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmEndByService"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.Gp.ResumeLayout(False)
        Me.PanelCus.ResumeLayout(False)
        Me.PanelCus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TimerCountServed As System.Windows.Forms.Timer
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents FLP As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Gp As CodeVendor.Controls.Grouper
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblQueue As System.Windows.Forms.Label
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents PanelCus As System.Windows.Forms.Panel
    Friend WithEvents btnTop As System.Windows.Forms.Button
    Friend WithEvents btnCusInfo As System.Windows.Forms.Button
    Friend WithEvents lblTimeS As System.Windows.Forms.Label

End Class
