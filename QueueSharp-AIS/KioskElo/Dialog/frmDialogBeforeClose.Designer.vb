<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDialogBeforeClose
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
        Me.TimerEnd = New System.Windows.Forms.Timer(Me.components)
        Me.lblTxt = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TimerEnd
        '
        Me.TimerEnd.Enabled = True
        Me.TimerEnd.Interval = 3000
        '
        'lblTxt
        '
        Me.lblTxt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTxt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTxt.Location = New System.Drawing.Point(12, 13)
        Me.lblTxt.Name = "lblTxt"
        Me.lblTxt.Size = New System.Drawing.Size(304, 254)
        Me.lblTxt.TabIndex = 1
        Me.lblTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmDialogBeforeClose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.KioskElo.My.Resources.Resources.msg
        Me.ClientSize = New System.Drawing.Size(330, 280)
        Me.Controls.Add(Me.lblTxt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDialogBeforeClose"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmDialogBeforeCloseForm"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TimerEnd As System.Windows.Forms.Timer
    Friend WithEvents lblTxt As System.Windows.Forms.Label
End Class
