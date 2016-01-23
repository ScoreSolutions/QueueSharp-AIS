Imports QueueSharp.Org.Mentalis.Files

Public Class frmCompactView_M

    Dim clickX As Integer = 0
    Dim clickY As Integer = 0

    Private Sub frmFloat_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseHover, btnCall.MouseHover, btnEnd.MouseHover, btnCancel.MouseHover, lblStstus.MouseHover, lblRoom.MouseHover, btnMain.MouseHover, btnReSize.MouseHover, Pg.MouseHover, btnhold.MouseHover
        Me.Opacity = 1
        '------------
        FormFloatAllservice.Opacity = 1
        '------------
    End Sub

    Private Sub frmFloat_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        Me.Opacity = 0.7
        '------------
        FormFloatAllservice.Opacity = 0.7
        '------------
    End Sub

    Private Sub frmFloat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckCustomerServe()
        CheckHold()
    End Sub

   

    Public WriteOnly Property ReportText() As String
        Set(ByVal value As String)
            lblStstus.Text = value
        End Set
    End Property

    Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
        If lblStstus.Text.Trim = "" Then
            frmServiceQueue.btnCall_Click(frmServiceQueue.btnCall, New EventArgs)
            CheckCustomerServe()
        End If
    End Sub

    'Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
    '    frmServiceQueue.btnEnd_Click(frmServiceQueue.btnEnd, New EventArgs)
    '    CheckCustomerServe()
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        frmServiceQueue.btnCancel_Click(frmServiceQueue.btnCancel, New EventArgs)
        CheckCustomerServe()
    End Sub

    Private Sub btnhold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhold.Click
        'frmServiceQueue.btnHold_Click(frmServiceQueue.btnHold, New EventArgs)
        'CheckHold()
    End Sub

    Private Sub btnMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMain.Click
        frmMain.Show()
        Me.Close()
        FormFloatAllservice.Close()
    End Sub

    Private Sub frmFloat_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, Pg.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left = Me.Left + (e.X - clickX)
            Me.Top = Me.Top + (e.Y - clickY)
            '------------------
            FormFloatAllservice.Left = FormFloatAllservice.Left + (e.X - clickX)
            FormFloatAllservice.Top = FormFloatAllservice.Top + (e.Y - clickY)
            '------------------
        End If
    End Sub

    Private Sub frmFloat_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, Pg.MouseUp
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("LEFT", Me.Left)
        ini.Write("TOP", Me.Top)
    End Sub

    Private Sub frmFloat_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, Pg.MouseDown
        clickX = e.X
        clickY = e.Y
    End Sub

    Private Sub Down_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Down.MouseHover
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Me.Left = ini.ReadInteger("LEFT")
        Me.Top = ini.ReadInteger("TOP")
    End Sub

    Private Sub STimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STimer.Tick
        Dim counter As Integer
        For counter = 90 To 10 Step -5
            FormFloatAllservice.Opacity = counter / 100
            FormFloatAllservice.Refresh()
            Threading.Thread.Sleep(100)
        Next counter
        FormFloatAllservice.Close()
    End Sub

    Sub CheckHold()
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Me.Left = ini.ReadInteger("LEFT")
        Me.Top = ini.ReadInteger("TOP")

        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select available from TB_COUNTER where id =" & myUser.counter_id
        dt = getDataTable(sql)
        If dt.Rows(0).Item("available").ToString = "N" Then
            btnEnd.BackgroundImage = Global.QueueSharp.My.Resources.Resources.endQ_2
            btnCall.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_10_21
            Me.btnhold.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_2_13
            Me.btnCall.Enabled = False
            Me.btnCancel.Enabled = False
            Me.btnEnd.Enabled = False
        Else
            btnCall.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_10
            btnEnd.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_11
            Me.btnhold.BackgroundImage = Global.QueueSharp.My.Resources.Resources.QSharp_popup_13
            Me.btnCall.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnEnd.Enabled = True
            'CheckCustomerServe()
        End If
    End Sub

    Public Sub CheckCustomerServe()
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim QueueNo As String = ""
        lblRoom.Text = myUser.counter_name
        lblStstus.Text = ""

        sql = "select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and status = 2 and counter_id = " & myUser.counter_id & " order by start_time "
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                If QueueNo = "" Then
                    QueueNo = dt.Rows(i).Item("queue_no")
                Else
                    QueueNo = QueueNo & " ," & dt.Rows(i).Item("queue_no")
                End If
            Next
            lblStstus.Text = "กำลังรับบริการ หมายเลขคิว : " & QueueNo
        End If

    End Sub

    Private Sub frmFloat_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        frmServiceQueue.FloatHwnd = Me
    End Sub

    Private Sub frmFloat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmServiceQueue.FloatHwnd = Nothing
    End Sub

    Private Sub Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Down.Click
        FormFloatAllservice.Visible = Not FormFloatAllservice.Visible
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Me.Left = ini.ReadInteger("LEFT")
        Me.Top = ini.ReadInteger("TOP")
        FormFloatAllservice.SetDesktopLocation((Me.Left + Me.Width) - FormFloatAllservice.Width, Me.Top + Me.Height)
        FormFloatAllservice.Opacity = 1
    End Sub

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
End Class
