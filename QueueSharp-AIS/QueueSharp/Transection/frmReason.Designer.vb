<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReason
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
        Me.GroupBoxHistory = New CodeVendor.Controls.Grouper
        Me.btnOK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbReason = New System.Windows.Forms.ComboBox
        Me.GroupBoxHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxHistory
        '
        Me.GroupBoxHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxHistory.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxHistory.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise
        Me.GroupBoxHistory.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxHistory.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxHistory.BorderThickness = 1.0!
        Me.GroupBoxHistory.Controls.Add(Me.btnOK)
        Me.GroupBoxHistory.Controls.Add(Me.Label1)
        Me.GroupBoxHistory.Controls.Add(Me.cbReason)
        Me.GroupBoxHistory.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxHistory.GroupImage = Nothing
        Me.GroupBoxHistory.GroupTitle = ""
        Me.GroupBoxHistory.Location = New System.Drawing.Point(9, -3)
        Me.GroupBoxHistory.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBoxHistory.MinimumSize = New System.Drawing.Size(3, 1)
        Me.GroupBoxHistory.Name = "GroupBoxHistory"
        Me.GroupBoxHistory.Padding = New System.Windows.Forms.Padding(45, 39, 45, 39)
        Me.GroupBoxHistory.PaintGroupBox = False
        Me.GroupBoxHistory.RoundCorners = 10
        Me.GroupBoxHistory.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxHistory.ShadowControl = True
        Me.GroupBoxHistory.ShadowThickness = 3
        Me.GroupBoxHistory.Size = New System.Drawing.Size(337, 132)
        Me.GroupBoxHistory.TabIndex = 24
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnOK.ForeColor = System.Drawing.Color.Navy
        Me.btnOK.Image = Global.QueueSharp.My.Resources.Resources.check
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOK.Location = New System.Drawing.Point(216, 70)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(103, 42)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "   OK"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 18)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Reason :"
        '
        'cbReason
        '
        Me.cbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReason.FormattingEnabled = True
        Me.cbReason.Location = New System.Drawing.Point(83, 30)
        Me.cbReason.Margin = New System.Windows.Forms.Padding(4)
        Me.cbReason.Name = "cbReason"
        Me.cbReason.Size = New System.Drawing.Size(236, 26)
        Me.cbReason.TabIndex = 0
        '
        'frmReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(352, 135)
        Me.Controls.Add(Me.GroupBoxHistory)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReason"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmReason"
        Me.TopMost = True
        Me.GroupBoxHistory.ResumeLayout(False)
        Me.GroupBoxHistory.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxHistory As CodeVendor.Controls.Grouper
    Friend WithEvents cbReason As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
End Class
