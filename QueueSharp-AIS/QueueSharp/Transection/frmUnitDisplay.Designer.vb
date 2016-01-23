<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnitDisplay
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
        Me.tt = New System.Windows.Forms.Timer(Me.components)
        Me.ttt = New System.Windows.Forms.Timer(Me.components)
        Me.btnReCall = New System.Windows.Forms.Button
        Me.COM = New System.IO.Ports.SerialPort(Me.components)
        Me.txtLED = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'tt
        '
        Me.tt.Interval = 500
        '
        'ttt
        '
        Me.ttt.Interval = 1500
        '
        'btnReCall
        '
        Me.btnReCall.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnReCall.Location = New System.Drawing.Point(0, 47)
        Me.btnReCall.Name = "btnReCall"
        Me.btnReCall.Size = New System.Drawing.Size(292, 29)
        Me.btnReCall.TabIndex = 1
        Me.btnReCall.Text = "Re-Call"
        Me.btnReCall.UseVisualStyleBackColor = True
        '
        'txtLED
        '
        Me.txtLED.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLED.BackColor = System.Drawing.Color.Black
        Me.txtLED.ForeColor = System.Drawing.Color.Red
        Me.txtLED.Location = New System.Drawing.Point(78, 6)
        Me.txtLED.Multiline = True
        Me.txtLED.Name = "txtLED"
        Me.txtLED.ReadOnly = True
        Me.txtLED.Size = New System.Drawing.Size(136, 35)
        Me.txtLED.TabIndex = 2
        Me.txtLED.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmUnitDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(292, 76)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtLED)
        Me.Controls.Add(Me.btnReCall)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmUnitDisplay"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tt As System.Windows.Forms.Timer
    Friend WithEvents ttt As System.Windows.Forms.Timer
    Friend WithEvents btnReCall As System.Windows.Forms.Button
    Friend WithEvents COM As System.IO.Ports.SerialPort
    Friend WithEvents txtLED As System.Windows.Forms.TextBox
End Class
