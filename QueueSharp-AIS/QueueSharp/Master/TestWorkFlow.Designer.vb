<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestWorkFlow
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
        Me.monthView1 = New System.Windows.Forms.Calendar.MonthView
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'monthView1
        '
        Me.monthView1.DaySelectedColor = System.Drawing.Color.Empty
        Me.monthView1.DaySelectedTextColor = System.Drawing.Color.Black
        Me.monthView1.ItemPadding = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.monthView1.Location = New System.Drawing.Point(0, 0)
        Me.monthView1.MaxSelectionCount = 35
        Me.monthView1.Name = "monthView1"
        Me.monthView1.Size = New System.Drawing.Size(196, 153)
        Me.monthView1.TabIndex = 5
        Me.monthView1.Text = "monthView1"
        Me.monthView1.TodayBorderColor = System.Drawing.Color.White
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(55, 194)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TestWorkFlow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(254, 246)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.monthView1)
        Me.Name = "TestWorkFlow"
        Me.Text = "TestWorkFlow"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents monthView1 As System.Windows.Forms.Calendar.MonthView
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
