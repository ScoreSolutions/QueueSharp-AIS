<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVDO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVDO))
        Me.pb1 = New System.Windows.Forms.PictureBox
        Me.VDO = New AxWMPLib.AxWindowsMediaPlayer
        Me.pb = New System.Windows.Forms.PictureBox
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VDO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pb1
        '
        Me.pb1.BackColor = System.Drawing.Color.Black
        Me.pb1.BackgroundImage = CType(resources.GetObject("pb1.BackgroundImage"), System.Drawing.Image)
        Me.pb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pb1.Location = New System.Drawing.Point(0, 489)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(800, 295)
        Me.pb1.TabIndex = 14
        Me.pb1.TabStop = False
        '
        'VDO
        '
        Me.VDO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VDO.Enabled = True
        Me.VDO.Location = New System.Drawing.Point(0, 232)
        Me.VDO.Name = "VDO"
        Me.VDO.OcxState = CType(resources.GetObject("VDO.OcxState"), System.Windows.Forms.AxHost.State)
        Me.VDO.Size = New System.Drawing.Size(799, 556)
        Me.VDO.TabIndex = 2
        '
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(22, -12)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(751, 1300)
        Me.pb.TabIndex = 20
        Me.pb.TabStop = False
        '
        'frmVDO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 784)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.VDO)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmVDO"
        Me.Text = "frmVDO"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VDO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pb1 As System.Windows.Forms.PictureBox
    Friend WithEvents VDO As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents pb As System.Windows.Forms.PictureBox
End Class
