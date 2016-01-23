<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestSetDateModify
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtDateTime = New System.Windows.Forms.TextBox
        Me.lstFile = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(286, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtDateTime
        '
        Me.txtDateTime.Location = New System.Drawing.Point(26, 12)
        Me.txtDateTime.Name = "txtDateTime"
        Me.txtDateTime.Size = New System.Drawing.Size(187, 20)
        Me.txtDateTime.TabIndex = 1
        '
        'lstFile
        '
        Me.lstFile.FormattingEnabled = True
        Me.lstFile.Location = New System.Drawing.Point(26, 38)
        Me.lstFile.Name = "lstFile"
        Me.lstFile.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFile.Size = New System.Drawing.Size(740, 407)
        Me.lstFile.TabIndex = 2
        '
        'frmTestSetDateModify
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(778, 456)
        Me.Controls.Add(Me.lstFile)
        Me.Controls.Add(Me.txtDateTime)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmTestSetDateModify"
        Me.Text = "frmTestSetDateModify"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtDateTime As System.Windows.Forms.TextBox
    Friend WithEvents lstFile As System.Windows.Forms.ListBox
End Class
