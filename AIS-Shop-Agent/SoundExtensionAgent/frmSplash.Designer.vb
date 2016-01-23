<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplash
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.lblStat = New System.Windows.Forms.Label
        Me.lblNotFound = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblStat
        '
        Me.lblStat.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblStat.Location = New System.Drawing.Point(0, 108)
        Me.lblStat.Name = "lblStat"
        Me.lblStat.Size = New System.Drawing.Size(263, 13)
        Me.lblStat.TabIndex = 0
        Me.lblStat.Text = "Checking File:"
        '
        'lblNotFound
        '
        Me.lblNotFound.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblNotFound.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblNotFound.Location = New System.Drawing.Point(0, 95)
        Me.lblNotFound.Name = "lblNotFound"
        Me.lblNotFound.Size = New System.Drawing.Size(263, 13)
        Me.lblNotFound.TabIndex = 1
        Me.lblNotFound.Text = "File Not Found [0]"
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(263, 121)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblNotFound)
        Me.Controls.Add(Me.lblStat)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSplash"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.SystemColors.Control
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblStat As System.Windows.Forms.Label
    Friend WithEvents lblNotFound As System.Windows.Forms.Label
End Class
