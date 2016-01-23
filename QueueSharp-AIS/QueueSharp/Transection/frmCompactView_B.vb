Imports QueueSharp.Org.Mentalis.Files

Public Class frmCompactView_B

    Dim clickX As Integer = 0
    Dim clickY As Integer = 0
    Dim sql As String = ""
    Dim ds As New DataSet

    '#Region "Transparency Cotnrol"

    '    Private Sub frmFloat_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick

    '    End Sub

    '    Private Sub frmFloat_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    '        clickX = e.X
    '        clickY = e.Y
    '    End Sub
    '    Private Sub frmFloat_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseHover, btnCall.MouseHover, btnEnd.MouseHover, btnCancel.MouseHover
    '        Me.Opacity = 1
    '        '------------
    '        'FormFloatAllservice.Opacity = 1
    '        '------------
    '    End Sub

    '    Private Sub frmFloat_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
    '        Me.Opacity = 0.7
    '        '------------
    '        'FormFloatAllservice.Opacity = 0.7
    '        '------------
    '    End Sub
    '#End Region

    '    Private Sub frmFloat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '        showdata()
    '        CheckCustomerServe()
    '    End Sub
    '    Private Sub frmFloat_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    '        If e.Button = Windows.Forms.MouseButtons.Left Then
    '            Me.Left = Me.Left + (e.X - clickX)
    '            Me.Top = Me.Top + (e.Y - clickY)
    '            '------------------
    '            FormFloatAllservice.Left = FormFloatAllservice.Left + (e.X - clickX)
    '            FormFloatAllservice.Top = FormFloatAllservice.Top + (e.Y - clickY)
    '            '------------------
    '        End If
    '    End Sub

    '    Private Sub frmFloat_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
    '        Dim ini As New IniReader(INIFileName)
    '        ini.Section = "SETTING"
    '        ini.Write("LEFT", Me.Left)
    '        ini.Write("TOP", Me.Top)
    '    End Sub

    '    Public WriteOnly Property ReportText() As String
    '        Set(ByVal value As String)
    '            lblNextroom.Text = value
    '        End Set
    '    End Property

    '    Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
    '        frmServiceFIFO.butnext_Click(frmServiceFIFO.butnext, New EventArgs)
    '    End Sub

    '    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
    '        frmServiceFIFO.butservice_Click(frmServiceFIFO.butservice, New EventArgs)
    '    End Sub

    '    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '        frmServiceFIFO.butcancel_Click(frmServiceFIFO.butcancel, New EventArgs)
    '    End Sub

    '    Private Sub btnhold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhold.Click
    '        lblNextroom.Text = ""
    '        frmServiceFIFO.buthold_Click(frmServiceFIFO.buthold, New EventArgs)
    '        showdata()
    '    End Sub

    '    Private Sub btnMain_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMain.Click
    '        frmMain.Show()
    '        Me.Close()
    '        FormFloatAllservice.Close()
    '    End Sub
    '    Private Sub Down_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Down.MouseHover
    '        Dim ini As New IniReader(INIFileName)
    '        ini.Section = "SETTING"
    '        Me.Left = ini.ReadInteger("LEFT")
    '        Me.Top = ini.ReadInteger("TOP")
    '        'FormFloatAllservice.SetDesktopLocation(Me.Left + 328, Me.Top + 155)
    '        'FormFloatAllservice.Show()
    '        'FormFloatAllservice.Opacity = 1
    '    End Sub

    '    Private Sub Down_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Down.MouseLeave
    '        'STimer.Interval = 10 * 1000
    '        'STimer.Enabled = True
    '    End Sub

    '    Private Sub STimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STimer.Tick
    '        Dim counter As Integer
    '        For counter = 90 To 10 Step -5
    '            FormFloatAllservice.Opacity = counter / 100
    '            FormFloatAllservice.Refresh()
    '            Threading.Thread.Sleep(100)
    '        Next counter
    '        FormFloatAllservice.Close()
    '    End Sub

    '    Private Sub Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Down.Click
    '        FormFloatAllservice.Visible = Not FormFloatAllservice.Visible
    '        Dim ini As New IniReader(INIFileName)
    '        ini.Section = "SETTING"
    '        Me.Left = ini.ReadInteger("LEFT")
    '        Me.Top = ini.ReadInteger("TOP")
    '        FormFloatAllservice.SetDesktopLocation(Me.Left + 328, Me.Top + 155)
    '        'FormFloatAllservice.Show()
    '        FormFloatAllservice.Opacity = 1
    '        showdata()

    '    End Sub

    '    Sub showdata()
    '        Dim ini As New IniReader(INIFileName)
    '        ini.Section = "SETTING"
    '        Me.Left = ini.ReadInteger("LEFT")
    '        Me.Top = ini.ReadInteger("TOP")

    '        lblroom.Text = myUser.counter_name

    '        sql = "select status from TB_service_counter where counter_id = '" & myUser.counter_id & "' and status = 1"
    '        ds = getDataset(sql)
    '        If ds.Tables(0).Rows.Count = 0 Then
    '            btnEnd.BackgroundImage = Global.QueueSharp.My.Resources.Resources.endTB_2
    '            btnCall.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_10_21
    '            Me.btnhold.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_2_13
    '            Me.btnCall.Enabled = False
    '            Me.btnCancel.Enabled = False
    '            Me.btnEnd.Enabled = False
    '        Else
    '            btnCall.Image = Global.QueueSharp.My.Resources.Resources.QSharp_popup_10
    '            btnEnd.Image = Global.QueueSharp.My.Resources.Resources.QSharp_popup_11
    '            Me.btnhold.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_13
    '            Me.btnCall.Enabled = True
    '            Me.btnCancel.Enabled = True
    '            Me.btnEnd.Enabled = True
    '            CheckCustomerServe()
    '        End If

    '    End Sub

    '    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '        Dim site As String = ""
    '        Dim ini As New IniReader(INIFileName)
    '        ini.Section = "SETTING"
    '        site = ini.ReadString("compactsite")
    '        Select Case site.ToUpper
    '            Case "B"
    '                Me.Close()
    '                FormFloatAllservice.Close()
    '                Dim f As New frmCompactView_M
    '                f.Show()
    '                ini.Write("compactsite", "M")
    '            Case "M"
    '                Me.Close()
    '                FormFloatAllservice.Close()
    '                Dim f As New frmCompactView_S
    '                f.Show()
    '                ini.Write("compactsite", "S")
    '            Case "S"
    '                Me.Close()
    '                FormFloatAllservice.Close()
    '                Dim f As New frmCompactView_B
    '                f.Show()
    '                ini.Write("compactsite", "B")
    '        End Select
    '    End Sub


    '    Sub CheckCustomerServe()
    '        lblNextroom.Text = ""
    '        Dim txtq As String = ""
    '        sql = "select queue_no from TB_counter_queue where datediff(d,getdate(),service_date)=0 and counter_id ='" & myUser.counter_id & "' and status = '2' order by start_time desc"
    '        ds = getDataset(sql)
    '        If ds.Tables(0).Rows.Count = 1 Then
    '            txtq = ds.Tables(0).Rows(0)("queue_no").ToString
    '            sql = "select c.prename_desc + ' ' + b.f_name + ' ' + b.l_name as fullname from TB_counter_queue a inner join TB_customer b on a.customer_id = b.customer_id left join TB_prename c on b.prename_id = c.prename_id where datediff(d,getdate(),service_date)=0 and a.queue_no ='" & FixDB(txtq) & "'"
    '            ds = getDataset(sql)
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                lblNextroom.Text = "Being Serviced Queue No.: " & txtq & vbCrLf & "Name: " & ds.Tables(0).Rows(0)("fullname").ToString
    '            Else
    '                lblNextroom.Text = "Being Serviced Queue No.: " & txtq
    '            End If
    '        ElseIf ds.Tables(0).Rows.Count > 1 Then
    '            For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
    '                If txtq = "" Then
    '                    txtq = ds.Tables(0).Rows(i)("queue_no").ToString
    '                Else
    '                    txtq = txtq & ", " & ds.Tables(0).Rows(i)("queue_no").ToString
    '                End If
    '            Next
    '            lblNextroom.Text = "Being Serviced Queue No.: " & txtq
    '        End If
    '    End Sub

    '#Region "T Add "

    '    Private Sub frmFloat_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '        frmMain.FloatHwnd = Me
    '    End Sub

    '    Private Sub frmFloat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '        frmMain.FloatHwnd = Nothing
    '    End Sub

    '#End Region



End Class
