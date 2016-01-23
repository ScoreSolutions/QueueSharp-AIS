<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchedulText
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBoxButton = New CodeVendor.Controls.Grouper
        Me.Grouper2 = New CodeVendor.Controls.Grouper
        Me.btnSave_M = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txt_Sh_Fri_M = New System.Windows.Forms.TextBox
        Me.txt_Sh_Thr_M = New System.Windows.Forms.TextBox
        Me.txt_Sh_Mon_M = New System.Windows.Forms.TextBox
        Me.txt_Sh_Thu_M = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_Sh_Wen_M = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Grouper3 = New CodeVendor.Controls.Grouper
        Me.Grouper5 = New CodeVendor.Controls.Grouper
        Me.btnSave_A = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txt_Sh_Fri_A = New System.Windows.Forms.TextBox
        Me.txt_Sh_Thr_A = New System.Windows.Forms.TextBox
        Me.txt_Sh_Mon_A = New System.Windows.Forms.TextBox
        Me.txt_Sh_Thu_A = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txt_Sh_Wen_A = New System.Windows.Forms.TextBox
        Me.SFD_M = New System.Windows.Forms.SaveFileDialog
        Me.SFD_A = New System.Windows.Forms.SaveFileDialog
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBoxButton.SuspendLayout()
        Me.Grouper2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Grouper3.SuspendLayout()
        Me.Grouper5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(986, 572)
        Me.TabControl1.TabIndex = 14
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.AliceBlue
        Me.TabPage1.Controls.Add(Me.GroupBoxButton)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(978, 541)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ข้อความวิ่งช่วงเช้า"
        '
        'GroupBoxButton
        '
        Me.GroupBoxButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxButton.BackgroundColor = System.Drawing.Color.White
        Me.GroupBoxButton.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.GroupBoxButton.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.GroupBoxButton.BorderColor = System.Drawing.Color.SteelBlue
        Me.GroupBoxButton.BorderThickness = 1.0!
        Me.GroupBoxButton.Controls.Add(Me.Grouper2)
        Me.GroupBoxButton.CustomGroupBoxColor = System.Drawing.Color.White
        Me.GroupBoxButton.GroupImage = Nothing
        Me.GroupBoxButton.GroupTitle = ""
        Me.GroupBoxButton.Location = New System.Drawing.Point(6, -4)
        Me.GroupBoxButton.MinimumSize = New System.Drawing.Size(1, 1)
        Me.GroupBoxButton.Name = "GroupBoxButton"
        Me.GroupBoxButton.Padding = New System.Windows.Forms.Padding(20)
        Me.GroupBoxButton.PaintGroupBox = False
        Me.GroupBoxButton.RoundCorners = 10
        Me.GroupBoxButton.ShadowColor = System.Drawing.Color.DarkGray
        Me.GroupBoxButton.ShadowControl = True
        Me.GroupBoxButton.ShadowThickness = 3
        Me.GroupBoxButton.Size = New System.Drawing.Size(966, 539)
        Me.GroupBoxButton.TabIndex = 24
        '
        'Grouper2
        '
        Me.Grouper2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper2.BackgroundColor = System.Drawing.Color.White
        Me.Grouper2.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.Grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper2.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper2.BorderThickness = 1.0!
        Me.Grouper2.Controls.Add(Me.btnSave_M)
        Me.Grouper2.Controls.Add(Me.Label6)
        Me.Grouper2.Controls.Add(Me.Label5)
        Me.Grouper2.Controls.Add(Me.Label2)
        Me.Grouper2.Controls.Add(Me.txt_Sh_Fri_M)
        Me.Grouper2.Controls.Add(Me.txt_Sh_Thr_M)
        Me.Grouper2.Controls.Add(Me.txt_Sh_Mon_M)
        Me.Grouper2.Controls.Add(Me.txt_Sh_Thu_M)
        Me.Grouper2.Controls.Add(Me.Label3)
        Me.Grouper2.Controls.Add(Me.Label4)
        Me.Grouper2.Controls.Add(Me.txt_Sh_Wen_M)
        Me.Grouper2.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper2.GroupImage = Nothing
        Me.Grouper2.GroupTitle = ""
        Me.Grouper2.Location = New System.Drawing.Point(115, 39)
        Me.Grouper2.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Grouper2.Name = "Grouper2"
        Me.Grouper2.Padding = New System.Windows.Forms.Padding(20)
        Me.Grouper2.PaintGroupBox = False
        Me.Grouper2.RoundCorners = 10
        Me.Grouper2.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper2.ShadowControl = True
        Me.Grouper2.ShadowThickness = 3
        Me.Grouper2.Size = New System.Drawing.Size(737, 461)
        Me.Grouper2.TabIndex = 25
        '
        'btnSave_M
        '
        Me.btnSave_M.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave_M.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave_M.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave_M.Location = New System.Drawing.Point(620, 402)
        Me.btnSave_M.Name = "btnSave_M"
        Me.btnSave_M.Size = New System.Drawing.Size(94, 40)
        Me.btnSave_M.TabIndex = 33
        Me.btnSave_M.Text = "บันทึก"
        Me.btnSave_M.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave_M.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(41, 327)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 18)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "วันศุกร์ :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 254)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 18)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "วันพฤหัสบดี :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 181)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 18)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "วันพุธ :"
        '
        'txt_Sh_Fri_M
        '
        Me.txt_Sh_Fri_M.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Fri_M.Location = New System.Drawing.Point(108, 324)
        Me.txt_Sh_Fri_M.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Fri_M.Multiline = True
        Me.txt_Sh_Fri_M.Name = "txt_Sh_Fri_M"
        Me.txt_Sh_Fri_M.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Fri_M.TabIndex = 24
        '
        'txt_Sh_Thr_M
        '
        Me.txt_Sh_Thr_M.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Thr_M.Location = New System.Drawing.Point(108, 251)
        Me.txt_Sh_Thr_M.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Thr_M.Multiline = True
        Me.txt_Sh_Thr_M.Name = "txt_Sh_Thr_M"
        Me.txt_Sh_Thr_M.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Thr_M.TabIndex = 29
        '
        'txt_Sh_Mon_M
        '
        Me.txt_Sh_Mon_M.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Mon_M.Location = New System.Drawing.Point(108, 32)
        Me.txt_Sh_Mon_M.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Mon_M.Multiline = True
        Me.txt_Sh_Mon_M.Name = "txt_Sh_Mon_M"
        Me.txt_Sh_Mon_M.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Mon_M.TabIndex = 25
        '
        'txt_Sh_Thu_M
        '
        Me.txt_Sh_Thu_M.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Thu_M.Location = New System.Drawing.Point(108, 105)
        Me.txt_Sh_Thu_M.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Thu_M.Multiline = True
        Me.txt_Sh_Thu_M.Name = "txt_Sh_Thu_M"
        Me.txt_Sh_Thu_M.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Thu_M.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 18)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "วันจันทร์ :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 18)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "วันอังคาร :"
        '
        'txt_Sh_Wen_M
        '
        Me.txt_Sh_Wen_M.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Wen_M.Location = New System.Drawing.Point(108, 178)
        Me.txt_Sh_Wen_M.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Wen_M.Multiline = True
        Me.txt_Sh_Wen_M.Name = "txt_Sh_Wen_M"
        Me.txt_Sh_Wen_M.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Wen_M.TabIndex = 26
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.AliceBlue
        Me.TabPage2.Controls.Add(Me.Grouper3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(978, 541)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "ข้อความวิ่งช่วงบ่าย"
        '
        'Grouper3
        '
        Me.Grouper3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper3.BackgroundColor = System.Drawing.Color.White
        Me.Grouper3.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.Grouper3.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper3.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper3.BorderThickness = 1.0!
        Me.Grouper3.Controls.Add(Me.Grouper5)
        Me.Grouper3.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper3.GroupImage = Nothing
        Me.Grouper3.GroupTitle = ""
        Me.Grouper3.Location = New System.Drawing.Point(6, -4)
        Me.Grouper3.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Grouper3.Name = "Grouper3"
        Me.Grouper3.Padding = New System.Windows.Forms.Padding(20)
        Me.Grouper3.PaintGroupBox = False
        Me.Grouper3.RoundCorners = 10
        Me.Grouper3.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper3.ShadowControl = True
        Me.Grouper3.ShadowThickness = 3
        Me.Grouper3.Size = New System.Drawing.Size(966, 539)
        Me.Grouper3.TabIndex = 25
        '
        'Grouper5
        '
        Me.Grouper5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grouper5.BackgroundColor = System.Drawing.Color.White
        Me.Grouper5.BackgroundGradientColor = System.Drawing.Color.SkyBlue
        Me.Grouper5.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.ForwardDiagonal
        Me.Grouper5.BorderColor = System.Drawing.Color.SteelBlue
        Me.Grouper5.BorderThickness = 1.0!
        Me.Grouper5.Controls.Add(Me.btnSave_A)
        Me.Grouper5.Controls.Add(Me.Label8)
        Me.Grouper5.Controls.Add(Me.Label9)
        Me.Grouper5.Controls.Add(Me.Label10)
        Me.Grouper5.Controls.Add(Me.txt_Sh_Fri_A)
        Me.Grouper5.Controls.Add(Me.txt_Sh_Thr_A)
        Me.Grouper5.Controls.Add(Me.txt_Sh_Mon_A)
        Me.Grouper5.Controls.Add(Me.txt_Sh_Thu_A)
        Me.Grouper5.Controls.Add(Me.Label11)
        Me.Grouper5.Controls.Add(Me.Label12)
        Me.Grouper5.Controls.Add(Me.txt_Sh_Wen_A)
        Me.Grouper5.CustomGroupBoxColor = System.Drawing.Color.White
        Me.Grouper5.GroupImage = Nothing
        Me.Grouper5.GroupTitle = ""
        Me.Grouper5.Location = New System.Drawing.Point(115, 39)
        Me.Grouper5.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Grouper5.Name = "Grouper5"
        Me.Grouper5.Padding = New System.Windows.Forms.Padding(20)
        Me.Grouper5.PaintGroupBox = False
        Me.Grouper5.RoundCorners = 10
        Me.Grouper5.ShadowColor = System.Drawing.Color.DarkGray
        Me.Grouper5.ShadowControl = True
        Me.Grouper5.ShadowThickness = 3
        Me.Grouper5.Size = New System.Drawing.Size(737, 461)
        Me.Grouper5.TabIndex = 25
        '
        'btnSave_A
        '
        Me.btnSave_A.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave_A.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave_A.Image = Global.QueueSharp.My.Resources.Resources.Save
        Me.btnSave_A.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave_A.Location = New System.Drawing.Point(620, 402)
        Me.btnSave_A.Name = "btnSave_A"
        Me.btnSave_A.Size = New System.Drawing.Size(94, 40)
        Me.btnSave_A.TabIndex = 33
        Me.btnSave_A.Text = "บันทึก"
        Me.btnSave_A.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave_A.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(41, 327)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 18)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "วันศุกร์ :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 254)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 18)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "วันพฤหัสบดี :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(46, 181)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 18)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "วันพุธ :"
        '
        'txt_Sh_Fri_A
        '
        Me.txt_Sh_Fri_A.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Fri_A.Location = New System.Drawing.Point(108, 324)
        Me.txt_Sh_Fri_A.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Fri_A.Multiline = True
        Me.txt_Sh_Fri_A.Name = "txt_Sh_Fri_A"
        Me.txt_Sh_Fri_A.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Fri_A.TabIndex = 24
        '
        'txt_Sh_Thr_A
        '
        Me.txt_Sh_Thr_A.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Thr_A.Location = New System.Drawing.Point(108, 251)
        Me.txt_Sh_Thr_A.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Thr_A.Multiline = True
        Me.txt_Sh_Thr_A.Name = "txt_Sh_Thr_A"
        Me.txt_Sh_Thr_A.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Thr_A.TabIndex = 29
        '
        'txt_Sh_Mon_A
        '
        Me.txt_Sh_Mon_A.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Mon_A.Location = New System.Drawing.Point(108, 32)
        Me.txt_Sh_Mon_A.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Mon_A.Multiline = True
        Me.txt_Sh_Mon_A.Name = "txt_Sh_Mon_A"
        Me.txt_Sh_Mon_A.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Mon_A.TabIndex = 25
        '
        'txt_Sh_Thu_A
        '
        Me.txt_Sh_Thu_A.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Thu_A.Location = New System.Drawing.Point(108, 105)
        Me.txt_Sh_Thu_A.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Thu_A.Multiline = True
        Me.txt_Sh_Thu_A.Name = "txt_Sh_Thu_A"
        Me.txt_Sh_Thu_A.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Thu_A.TabIndex = 28
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(32, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 18)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "วันจันทร์ :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(29, 108)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 18)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "วันอังคาร :"
        '
        'txt_Sh_Wen_A
        '
        Me.txt_Sh_Wen_A.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sh_Wen_A.Location = New System.Drawing.Point(108, 178)
        Me.txt_Sh_Wen_A.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Sh_Wen_A.Multiline = True
        Me.txt_Sh_Wen_A.Name = "txt_Sh_Wen_A"
        Me.txt_Sh_Wen_A.Size = New System.Drawing.Size(605, 65)
        Me.txt_Sh_Wen_A.TabIndex = 26
        '
        'frmSchedulText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(986, 572)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSchedulText"
        Me.ShowIcon = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBoxButton.ResumeLayout(False)
        Me.Grouper2.ResumeLayout(False)
        Me.Grouper2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Grouper3.ResumeLayout(False)
        Me.Grouper5.ResumeLayout(False)
        Me.Grouper5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBoxButton As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper2 As CodeVendor.Controls.Grouper
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_Sh_Fri_M As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sh_Thr_M As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sh_Mon_M As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sh_Thu_M As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_Sh_Wen_M As System.Windows.Forms.TextBox
    Friend WithEvents btnSave_M As System.Windows.Forms.Button
    Friend WithEvents SFD_M As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Grouper3 As CodeVendor.Controls.Grouper
    Friend WithEvents Grouper5 As CodeVendor.Controls.Grouper
    Friend WithEvents btnSave_A As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_Sh_Fri_A As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sh_Thr_A As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sh_Mon_A As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sh_Thu_A As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_Sh_Wen_A As System.Windows.Forms.TextBox
    Friend WithEvents SFD_A As System.Windows.Forms.SaveFileDialog

End Class
