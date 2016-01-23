<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestWeb
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
        Me.WB = New System.Windows.Forms.WebBrowser
        Me.GroupBox1 = New CodeVendor.Controls.Grouper
        Me.SuspendLayout()
        '
        'WB
        '
        Me.WB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WB.Location = New System.Drawing.Point(-1, 51)
        Me.WB.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WB.Name = "WB"
        Me.WB.Size = New System.Drawing.Size(737, 429)
        Me.WB.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackgroundColor = System.Drawing.Color.White
        Me.GroupBox1.BackgroundGradientColor = System.Drawing.Color.Black
        Me.GroupBox1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical
        Me.GroupBox1.BorderColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BorderThickness = 1.0!
        Me.GroupBox1.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBox1.GroupImage = Nothing
        Me.GroupBox1.GroupTitle = ""
        Me.GroupBox1.Location = New System.Drawing.Point(-1, -12)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.MinimumSize = New System.Drawing.Size(2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(30, 28, 30, 28)
        Me.GroupBox1.PaintGroupBox = False
        Me.GroupBox1.RoundCorners = 1
        Me.GroupBox1.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBox1.ShadowControl = True
        Me.GroupBox1.ShadowThickness = 1
        Me.GroupBox1.Size = New System.Drawing.Size(816, 64)
        Me.GroupBox1.TabIndex = 9
        '
        'TestWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 480)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.WB)
        Me.Name = "TestWeb"
        Me.Text = "Web Site"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WB As System.Windows.Forms.WebBrowser
    Friend WithEvents GroupBox1 As CodeVendor.Controls.Grouper
End Class
