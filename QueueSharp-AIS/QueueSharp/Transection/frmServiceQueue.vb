Imports System.Data
Imports System.Drawing
Imports QueueSharp.Org.Mentalis.Files
Imports System.IO

Public Class frmServiceQueue

    Dim IconCount(50) As String 'เป็นตัวแปรที่เก็บจำนวน Column ที่แสดง Icon 
    Dim GV_Height As Int32
    Dim ShowItemDisplay(20) As String

    Private Sub frmServiceQueue_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        FormServiceShow = False
        frmMain.btnNotifier.Visible = False
    End Sub

    Private Sub frmServiceFIFO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Q
                btnCall.PerformClick()
            Case Keys.C
                btnCancel.PerformClick()
        End Select
    End Sub

#Region "Sub Show Customer Wait in room and Show Customer End Service in room"
    Sub showcustomerwait()
        Dim dt As New DataTable
        Dim sql As String = ""
        If myUser.item_id = 0 And CounterFunction.SpeedLane = 1 Then
            'ห้อง SpeedLane แล้วเลือก Service Appointment
            sql = "exec SP_ShowCustomerAppWait " & myUser.counter_id & "," & 0
        ElseIf myUser.item_id > 0 And CounterFunction.SpeedLane = 1 And myUser.assign_appointment = 1 Then
            'ห้อง SpeedLane แล้วเลือก Service ที่ไม่ใช่ Appointment
            sql = "exec SP_ShowCustomerAppWait " & myUser.counter_id & "," & myUser.item_id
        Else
            'กรณีปกติ
            sql = "exec SP_ShowCustomerWait " & myUser.counter_id & "," & myUser.item_id
        End If

        dt = getDataTable(sql)
        GridWait.DataSource = dt
        Dim App As Boolean = False
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("hold").ToString.Trim <> "-" Then
                    App = True
                End If
            Next

            If App = True Then
                GridWait.Columns("hold").Visible = True
            Else
                If GridWait.Columns.Contains("hold") = True Then
                    GridWait.Columns("hold").Visible = False
                End If
            End If
        End If

        lblCountCustomer.Text = "No. Waiting Customers : " & dt.Rows.Count
        FormatGridWait()
        avgServiceTime()
        ShowDisplayService()
        Application.DoEvents()
        dt.Dispose()
    End Sub

    Sub ShowDisplayService()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "exec SP_DisplayCustomerWait"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            If FLPAllService.Controls.Count <> dt.Rows.Count Then
                FLPAllService.Controls.Clear()
                Dim Font As Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.Pixel)
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim lbl As New Label
                    With lbl
                        .Width = 140
                        .Height = 90
                        .Font = Font
                        .AutoSize = False
                        .FlatStyle = FlatStyle.Flat
                        .ForeColor = Drawing.Color.Black
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BorderStyle = BorderStyle.FixedSingle
                    End With
                    FLPAllService.Controls.Add(lbl)
                Next
            End If

            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim txt As String = ""
                txt = dt.Rows(i).Item("customertype_name").ToString & vbCrLf
                txt &= dt.Rows(i).Item("item_name").ToString & vbCrLf
                txt &= "No. serving " & dt.Rows(i).Item("count_queue_serve").ToString & vbCrLf
                txt &= "No. wait to serve " & dt.Rows(i).Item("count_queue_wait").ToString & vbCrLf
                txt &= "Max WT. " & dt.Rows(i).Item("max_wait").ToString & vbCrLf
                Dim lbl As New Label
                lbl = FLPAllService.Controls(i)
                With lbl
                    .Name = dt.Rows(i).Item("customertype_id").ToString & "_" & dt.Rows(i).Item("item_id").ToString
                    .Text = txt
                    If dt.Rows(i).Item("color").ToString = "" Then
                        .BackColor = Drawing.Color.White
                    Else
                        .BackColor = Drawing.Color.FromArgb(CInt(dt.Rows(i).Item("color").ToString))
                    End If
                End With
            Next
        End If
        dt.Dispose()
    End Sub

    Sub showcustomerend()
        Dim dt As New DataTable
        Dim sql As String = ""
        sql = "exec SP_ShowCustomerEnd " & myUser.user_id
        dt = getDataTable(sql)
        GridEnd.DataSource = dt

        dt = New DataTable
        sql = "select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and status in (3,5,8) and TB_COUNTER_QUEUE.user_id = " & myUser.user_id & " group by queue_no"
        dt = getDataTable(sql)
        lblServe.Text = dt.Rows.Count.ToString
        dt.Dispose()
    End Sub

#End Region

    'หา สถานะของลูกค้าที่ยังคงค้างอยู่ที่ห้อง
    Sub FindQueueService()
        Dim dt As New DataTable
        Dim sql As String = ""
        sql = "exec SP_UnfinishedCustomer " & myUser.counter_id
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Select Case dt.Rows(0).Item("status").ToString
                Case "2"
                    frmEndByService.QueueNo = dt.Rows(0).Item("queue_no").ToString
                    frmEndByService.CustomerID = dt.Rows(0).Item("customer_id").ToString
                    frmEndByService.Show()
                    'FloatHwnd = frmEndByService
                    'AddHandler FloatHwnd.FormClosed, AddressOf CloseFormFloat
                    frmMain.CheckCam = False
                    frmMain.Hide()
                Case "4"
                    Dim f As New frmServiceConfirm
                    f.QueueNo = dt.Rows(0).Item("queue_no").ToString
                    f.CustomerID = dt.Rows(0).Item("customer_id").ToString
                    f.CustomerName = dt.Rows(0).Item("customer_name").ToString
                    f.CustomerTypeID = dt.Rows(0).Item("customertype_id").ToString
                    f.CustomerTypeName = dt.Rows(0).Item("customertype_name").ToString
                    If f.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                        If CheckCustomerStatus(dt.Rows(0).Item("queue_no").ToString, dt.Rows(0).Item("customer_id").ToString, 4) = True Then
                            UpdateQueueStatus(1, myUser.counter_id, 0, dt.Rows(0).Item("queue_no").ToString, dt.Rows(0).Item("customer_id").ToString)
                        Else
                            MessageBox.Show("The information has already been updated by the other user.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    End If
                    showcustomerwait()
                    FindQueueService()
            End Select
        Else
            txtDisplayQueue.Text = ""
            lblQueue.Text = "-"
            lblCustomerID.Text = "-"
            lbltype.Text = "-"
            FLP.Enabled = True
        End If
        dt.Dispose()
    End Sub

    Private Sub CamButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCam.MouseDown
        btnCam.Top += 3
        btnCam.Left += 3
    End Sub

    Private Sub CamButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCam.MouseUp
        btnCam.Top -= 3
        btnCam.Left -= 3
    End Sub

    Private Sub InteractButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCall.MouseDown
        btnCall.Top += 3
        btnCall.Left += 3
    End Sub

    Private Sub InteractButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCall.MouseUp
        btnCall.Top -= 3
        btnCall.Left -= 3
    End Sub

    Public Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
        If txtDisplayQueue.Text <> "" Then Exit Sub
        btnCall.Enabled = False
        GridWait.Enabled = False
        frmMain.timerCheckForce.Enabled = False
        frmMain.timerForce.Enabled = False
        frmMain.BtnLogout.Enabled = False

        Dim dt As New DataTable
        Dim sql As String = ""
        Dim Chk As Boolean = False
        If myUser.item_id = 0 And CounterFunction.SpeedLane = 1 Then
            'ห้อง SpeedLane แล้วเลือก Service Appointment
            sql = "exec SP_ShowCustomerAppWait " & myUser.counter_id & "," & 0
        ElseIf myUser.item_id > 0 And CounterFunction.SpeedLane = 1 And myUser.assign_appointment = 1 Then
            'ห้อง SpeedLane แล้วเลือก Service ที่ไม่ใช่ Appointment
            sql = "exec SP_ShowCustomerAppWait " & myUser.counter_id & "," & myUser.item_id
        Else
            'กรณีปกติ
            sql = "exec SP_ShowCustomerWait " & myUser.counter_id & "," & myUser.item_id
        End If
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
            For i As Int32 = 0 To dt.Rows.Count - 1
                If CInt(dt.Rows(i).Item("ck_hold").ToString) > 0 Or CInt(dt.Rows(i).Item("status").ToString) = 8 Then
                    btnCall.Enabled = True
                    GridWait.Enabled = True
                    frmMain.BtnLogout.Enabled = True
                    Exit Sub
                End If

                Dim Old_Status As Int32 = dt.Rows(i).Item("status").ToString
                Dim time_hold As String = dt.Rows(i).Item("hold").ToString
                Dim t_hold As Int32 = 0
                If Old_Status = 6 Then
                    t_hold = DateDiff(DateInterval.Minute, CDate(FindDateNow.Hour & ":" & FindDateNow.Minute), CDate(time_hold))
                End If

                Dim DateNow As String = FindDateNow().ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))
                If CreateTransaction("frmServiceQueue_btnCall_Click") = True Then
                    If UpdateCallStatus(myUser.counter_id, dt.Rows(i).Item("queue_no").ToString, dt.Rows(i).Item("customer_id").ToString, DateNow, shTrans) = True Then
                        'UnitDisplay
                        CallUnitDisplay(dt.Rows(i).Item("queue_no").ToString, shTrans)
                        CallSpeaker(dt.Rows(i).Item("queue_no").ToString, shTrans)
                        CommitTransaction()

                        showcustomerwait()
                        Dim f As New frmServiceConfirm
                        f.QueueNo = dt.Rows(i).Item("queue_no").ToString
                        f.CustomerTypeID = dt.Rows(i).Item("customertype_id").ToString
                        f.CustomerTypeName = dt.Rows(i).Item("customertype_name").ToString
                        f.CustomerID = dt.Rows(i).Item("customer_id").ToString
                        f.CustomerName = dt.Rows(i).Item("customer_name").ToString
                        frmMain.BtnLogout.Enabled = True

                        If f.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                            'ปิด Dialog ไปเลย
                            CurrDB = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
                            Select Case Old_Status
                                Case 8
                                    UpdateQueueStatus(8, 0, 0, dt.Rows(i).Item("queue_no").ToString, dt.Rows(i).Item("customer_id").ToString)
                                Case 6
                                    UpdateQueueStatus(6, 0, t_hold, dt.Rows(i).Item("queue_no").ToString, dt.Rows(i).Item("customer_id").ToString)
                                Case Else
                                    UpdateQueueStatus(1, 0, 0, dt.Rows(i).Item("queue_no").ToString, dt.Rows(i).Item("customer_id").ToString)
                                    If time_hold <> "-" Then
                                        Dim vDateNow As String = FindDateNow(shTrans).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                                        sql = "update tb_counter_queue set hold = '" & vDateNow & " " & time_hold & "' "
                                        sql += " where datediff(d,getdate(),service_date)=0 "
                                        sql += " and queue_no = '" & dt.Rows(i).Item("queue_no").ToString & "' "
                                        sql += " and customer_id = '" & dt.Rows(i).Item("customer_id").ToString & "' and status = 1"
                                        executeSQL(sql)
                                        'If CurrDB = "MAIN" Then
                                        '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                                        'End If
                                    End If
                            End Select
                            'UnitDisplay
                            ClearUnitDisplay(False)
                        ElseIf f.DialogResult = Windows.Forms.DialogResult.OK Then
                            'Missed Call
                            If CreateTransaction("frmServiceQueue_btnCall_Click") = True Then
                                ClearUnitDisplay(False, shTrans)
                                ClearMainDisplay(myUser.counter_id, shTrans)
                                CommitTransaction()
                            End If
                        ElseIf f.DialogResult = Windows.Forms.DialogResult.Yes Then
                            'UnitDisplay
                            ServeUnitDisplay(f.QueueNo)
                            Dim ff As New frmEndByService
                            ff.QueueNo = dt.Rows(i).Item("queue_no").ToString
                            ff.CustomerID = dt.Rows(i).Item("customer_id").ToString
                            frmMain.Hide()
                            ff.ShowDialog()
                            QM.CloseQM()
                            frmMain.Show()
                        End If
                        showcustomerwait()
                        FindQueueService()
                        Exit For
                    Else
                        RollbackTransaction()
                        If AutoForceQueue = False Then
                            MessageBox.Show("The information has already been updated by the other user.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            'showcustomerwait()
                            'Exit For
                        End If
                        showcustomerwait()
                    End If
                    'Else
                    '    CommitTransaction()
                    '    showcustomerwait()
                    'End If
                Else
                    showcustomerwait()
                End If
            Next
            frmMain.timerCheckForce.Enabled = True
        End If
        dt.Dispose()
        btnCall.Enabled = True
        GridWait.Enabled = True
        frmMain.BtnLogout.Enabled = True
    End Sub

    Private Sub ButRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshWait.Click
        showcustomerwait()
    End Sub

    Private Sub ButRefresh1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showcustomerend()
    End Sub

    'Private Sub CheckTimer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckTimerWait.CheckedChanged
    '    checkAutoRefresh()
    'End Sub

    'Function checkAutoRefresh() As Boolean
    '    If CheckTimerWait.Checked Then
    '        timerRefreshWait.Interval = CInt(nudWait.Value) * 1000
    '        timerRefreshWait.Enabled = True
    '        nudWait.Enabled = True
    '        btnRefreshWait.Enabled = False
    '    Else
    '        timerRefreshWait.Enabled = False
    '        nudWait.Enabled = False
    '        btnRefreshWait.Enabled = True
    '    End If
    'End Function


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabMessagePage.SelectedIndexChanged
        Select Case TabMessagePage.SelectedTab.Name.ToUpper
            Case "TabServicePage".ToUpper
                showcustomerwait()
                'CheckTimerWait.Checked = True
                'timerRefreshWait.Enabled = True
            Case "TabViewPage".ToUpper
                showcustomerend()
                'timerRefreshWait.Enabled = False
        End Select
    End Sub

    Private Sub frmServiceFIFO_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
        GridItem.Columns("item_id").Visible = False
    End Sub

    Private Sub frmServiceFIFO_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        FormServiceShow = True
        frmMain.timerCheckForce.Enabled = True
        GV_Height = GroupBoxView.Height
        CheckTimerWait.Checked = True
        CheckCounterFunction()
        lblRoomName.Text = myUser.counter_name
        ShowItem()
        FindQueueService()
        frmMain.btnNotifier.Visible = True
        CheckHold(False)
        CheckQuickService()
        avgServiceTime()
        GridWait.AutoGenerateColumns = False
        GridEnd.AutoGenerateColumns = False
        timerRefreshWait.Interval = AutoRefresh()

        If AutoForceQueue = True Then
            btnCall.PerformClick()
            AutoForceQueue = False
        End If


        showcustomerwait()
        btnCall.Enabled = True
        GridWait.Enabled = True
    End Sub

    Sub ShowItem()

        Dim dt_item As New DataTable
        Dim dt_app As New DataTable
        Dim sql As String = ""

        sql = "select * from TB_APPOINTMENT_USER where DATEDIFF(D,GETDATE(),app_date )=0 and user_id = " & myUser.user_id
        dt_app = getDataTable(sql)
        If dt_app.Rows.Count > 0 Then
            myUser.assign_appointment = 1
        Else
            myUser.assign_appointment = 0
        End If

        If myUser.assign_appointment = 1 And CounterFunction.SpeedLane = 1 Then
            sql = "select 0 as id,'Appointment' as item_name,0 as ord,0 as priority union all select tb_item.id,item_name,1 as ord,priority from TB_USER_SERVICE_SCHEDULE left join tb_item on TB_USER_SERVICE_SCHEDULE.item_id = tb_item.id  where datediff(D,GETDATE(),service_date)=0 and user_id = " & myUser.user_id & " and TB_USER_SERVICE_SCHEDULE.active_status = 1 and tb_item.id in (select item_id from TB_USER_SKILL left join TB_SKILL_ITEM on TB_USER_SKILL.skill_id = TB_SKILL_ITEM.skill_id where user_id = " & myUser.user_id & ") order by ord,priority desc"
        Else
            sql = "select tb_item.id,item_name,1 as ord  from TB_USER_SERVICE_SCHEDULE left join tb_item on TB_USER_SERVICE_SCHEDULE.item_id = tb_item.id  where datediff(D,GETDATE(),service_date)=0 and user_id = " & myUser.user_id & " and TB_USER_SERVICE_SCHEDULE.active_status = 1 and tb_item.id in (select item_id from TB_USER_SKILL left join TB_SKILL_ITEM on TB_USER_SKILL.skill_id = TB_SKILL_ITEM.skill_id where user_id = " & myUser.user_id & ") order by priority desc"
        End If

        dt_item = getDataTable(sql)
        If dt_item.Rows.Count > 0 Then
            With cbItem
                .DataSource = dt_item
                .ValueMember = "id"
                .DisplayMember = "item_name"
            End With

            myUser.item_id = dt_item.Rows(0).Item("id").ToString
            'If CounterFunction.SpeedLane = 1 Then
            '    'ห้องที่เป็น SpeedLane
            '    cbItem.SelectedIndex = 0
            'Else
            If myUser.item_id > 0 Then
                cbItem.SelectedValue = myUser.item_id
                sql = "update tb_user set item_id = " & myUser.item_id & " where id = " & myUser.user_id
                executeSQL(sql)
            Else
                cbItem.SelectedIndex = 0
                myUser.item_id = cbItem.SelectedValue
            End If
            'End If
        Else
            myUser.item_id = 999
        End If

    End Sub

    Function CheckUserServiceItem(ByVal ItemID As Int32) As Boolean
        'เช็คว่าวันนี้ User ทำ Service อะไรได้บ้าง
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and user_id = " & myUser.user_id & " and item_id = " & ItemID & " and active_status = 1"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Sub CheckItemDisplay(ByVal Sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = Sender
        Dim sql As String = ""
        Dim dt As New DataTable
        If chk.Checked = True Then
            sql = "select id from TB_USER_SERVICE_LOG where DATEDIFF(D,GETDATE(),service_date)=0 and item_id = " & chk.Name & " and [USER_ID] = " & myUser.user_id
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                sql = "update TB_USER_SERVICE_LOG set active_status = 1 where DATEDIFF(D,GETDATE(),service_date)=0and item_id = " & chk.Name & " and [USER_ID] = " & myUser.user_id
                executeSQL(sql)
            Else
                sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_USER_SERVICE_LOG); insert into TB_USER_SERVICE_LOG(id,service_date,[user_id],item_id,active_status) values(@ID,GETDATE()," & myUser.user_id & "," & chk.Name & ",1)"
                executeSQL(sql)
            End If
        Else
            sql = "update TB_USER_SERVICE_LOG set active_status = 0 where DATEDIFF(D,GETDATE(),service_date)=0 and item_id = " & chk.Name & " and [USER_ID] = " & myUser.user_id
            executeSQL(sql)
        End If
        showcustomerwait()
    End Sub

    Private Sub GroupBoxView_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GroupBoxView.KeyPress
        If UCase(e.KeyChar) = "Q"c Then
            btnCall.PerformClick()
        ElseIf UCase(e.KeyChar) = "C"c Then
            btnCancel.PerformClick()
        End If
    End Sub

    Private Sub txtQueue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQueue.KeyPress
        If e.KeyChar = Chr(13) Then
            If txtQueue.Text <> "" Then
                QuickService(txtQueue.Text)
                txtQueue.Text = ""
            End If
        End If
    End Sub

    Public Sub GridWait_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GridWait.CellMouseClick
        If GridWait.Rows.Count > 0 Then
            If GridWait.SelectedRows(0).Cells(e.ColumnIndex).Value.ToString.ToUpper = "CANCEL" Then
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select queue_no,status from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and queue_no  = '" & FixDB(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString) & "' and customer_id = '" & GridWait.SelectedRows(0).Cells("customer_id").Value.ToString & "' and status in (1,6,8) group by queue_no,status"
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    Dim f As New frmServiceCancel
                    f.CustomerTypeID = GridWait.SelectedRows(0).Cells("customertype_id").Value.ToString
                    f.QueueNo = GridWait.SelectedRows(0).Cells("queue_no").Value.ToString
                    f.CustomerID = GridWait.SelectedRows(0).Cells("customer_id").Value.ToString
                    f.StatusCustomer = dt.Rows(0).Item("status").ToString
                    f.ShowDialog()
                    showcustomerwait()
                    FindQueueService()

                Else
                    MessageBox.Show("The information has already been updated by the other user.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End If
    End Sub

    Public Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If txtDisplayQueue.Text <> "" Then
            If CheckCustomerStatus(lblQueue.Text, lblCustomerID.Text, 2) = True Then
                Dim f As New frmServiceCancel
                f.CustomerTypeID = txtCustomerTypeID.Text
                f.QueueNo = lblQueue.Text
                f.CustomerID = lblCustomerID.Text
                f.StatusCustomer = 2
                If f.ShowDialog = Windows.Forms.DialogResult.Yes Then
                    showcustomerwait()
                    FindQueueService()
                End If
            Else
                MessageBox.Show("The information has already been updated by the other user.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Function CheckCustomerUnfinished() As Boolean
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "exec SP_UnfinishedCustomer " & myUser.counter_id
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            MessageBox.Show("กรุณาจบการบริการของลูกค้า ก่อนเรียกคิวถัดไปมารับบริการ", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return True
        End If
        Return False
    End Function

    Sub CheckQuickService()
        If CounterFunction.QuickService = 1 Then
            GroupBoxView.Height = GV_Height
            GroupBoxView.Height = GroupBoxView.Height - 40
            pnQuickService.Visible = True
            txtQueue.Focus()
        Else
            GroupBoxView.Height = GV_Height
            pnQuickService.Visible = False
        End If
    End Sub

    Private Sub QuickService(ByVal Queue As String)
        Dim dt As New DataTable
        dt = GridWait.DataSource
        Dim dt_tmp As New DataTable
        If dt.Rows.Count > 0 Then
            dt.DefaultView.RowFilter = "queue_no = '" & FixDB(Queue) & "'"
            If dt.DefaultView.Count > 0 Then
                dt_tmp = dt.DefaultView.ToTable
                UpdateQueueStatus(2, myUser.counter_id, 0, dt_tmp.Rows(0).Item("queue_no").ToString, dt_tmp.Rows(0).Item("customer_id").ToString)
                FindQueueService()
                showcustomerwait()
            End If
            dt.DefaultView.RowFilter = ""
        End If
    End Sub

    'Private Sub timerRefreshWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefreshWait.Tick
    '    If TabMessagePage.SelectedTab.Name <> "TabServicePage" Then Exit Sub
    '    showcustomerwait()
    'End Sub

    Private Sub FormatGridWait()

        'Dim sql As String = ""
        'Dim dt As New DataTable
        'sql = "select queue_no,item_id,[status] from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 "
        'dt = getDataTable(sql)

        For i As Integer = 0 To GridWait.Rows.Count - 1
            With GridWait.Rows(i).DefaultCellStyle
                If GridWait.Rows(i).Cells("color").Value.ToString <> "" And IsNumeric(GridWait.Rows(i).Cells("color").Value.ToString) Then
                    Dim Color As Color = Drawing.Color.FromArgb(CInt(GridWait.Rows(i).Cells("color").Value.ToString))
                    .ForeColor = Drawing.Color.White
                    .SelectionForeColor = Drawing.Color.White
                    .BackColor = Color
                    .SelectionBackColor = Color
                Else
                    Dim Color As Color = Drawing.Color.White
                    .ForeColor = Drawing.Color.Black
                    .SelectionForeColor = Drawing.Color.Black
                    .BackColor = Color
                    .SelectionBackColor = Color
                End If
            End With

            GridWait.Item("Cancel", i).Style.BackColor = Drawing.Color.MistyRose
            GridWait.Item("Cancel", i).Style.SelectionBackColor = Drawing.Color.MistyRose
            GridWait.Item("Cancel", i).Style.ForeColor = Drawing.Color.Black
            GridWait.Item("Cancel", i).Style.SelectionForeColor = Drawing.Color.Black
        Next
        'dt.Dispose()
    End Sub

    Public Sub btnHold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtDisplayQueue.Text.Trim <> "" Then
            MessageBox.Show("Please serve queue before hold.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        CheckHold(True)
    End Sub

    Sub CheckHold(ByVal buthold_Click As Boolean)
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select available from TB_COUNTER where id =" & myUser.counter_id
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("available") = 1 Then
                If buthold_Click = True Then
                    frmMain.timerCheckForce.Enabled = False
                    frmMain.timerForce.Enabled = False
                    Dim f As New frmReason
                    f.Reason = 1
                    If f.ShowDialog = Windows.Forms.DialogResult.Yes Then
                        HoldRoom()
                    Else
                        frmMain.timerCheckForce.Enabled = True
                    End If
                Else
                    UnholdRoom()
                End If
            Else
                If buthold_Click = True Then
                    LogHoldRoom(2)
                    UnholdRoom()
                Else
                    HoldRoom()
                End If

            End If
        End If
    End Sub

    Sub HoldRoom()
        frmMain.timerCheckForce.Enabled = False
        frmMain.timerForce.Enabled = False
        pb1.Visible = False
        pb2.Visible = True
        lblRoomName.ForeColor = Drawing.Color.Red
        btnCall.Enabled = False
        GridWait.Enabled = False
        FLP.Enabled = False
        GridItem.Enabled = False
        btnMessage.Enabled = False
        btnExpand.Enabled = False
        cbItem.Enabled = False

        frmMain.Rbbn.Enabled = False
    End Sub

    Sub UnholdRoom()
        frmMain.timerCheckForce.Enabled = True
        pb2.Visible = False
        pb1.Visible = True
        lblRoomName.ForeColor = Drawing.Color.RoyalBlue
        btnCall.Enabled = True
        GridWait.Enabled = True
        FLP.Enabled = True
        GridItem.Enabled = True
        btnMessage.Enabled = True
        btnExpand.Enabled = True
        cbItem.Enabled = True

        frmMain.Rbbn.Enabled = True
    End Sub

    'Sub AddIcon()
    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    sql = "select id,item_icon from TB_ITEM where item_icon is not null order by id"
    '    dt = getDataTable(sql)
    '    If dt.Rows.Count > 0 Then
    '        For i As Int32 = 0 To dt.Rows.Count - 1
    '            Dim stream As MemoryStream
    '            Dim B() As Byte = dt.Rows(i).Item("item_icon")
    '            stream = New MemoryStream(B)
    '            Dim img As New DataGridViewImageColumn()
    '            Dim inImg As Image = Image.FromStream(stream)
    '            img.Image = inImg
    '            img.HeaderText = ""
    '            img.Name = dt.Rows(i).Item("id").ToString
    '            GridWait.Columns.Add(img)
    '            IconCount(i) = dt.Rows(i).Item("id").ToString
    '        Next
    '    End If
    'End Sub

    Public FloatHwnd As Form = Nothing
    Private Sub btnCompactView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompactView.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        If ini.ReadString("compactsite") = "" Then
            ini.Write("compactsite", "M")
            frmCompactView_M.Show()
            FloatHwnd = frmCompactView_M
        Else
            Select Case ini.ReadString("compactsite").ToString.ToUpper
                Case "B"
                    frmCompactView_B.Show()
                    FloatHwnd = frmCompactView_B
                Case "M"
                    frmCompactView_M.Show()
                    FloatHwnd = frmCompactView_M
                Case "S"
                    frmCompactView_S.Show()
                    FloatHwnd = frmCompactView_S
            End Select
        End If
        AddHandler FloatHwnd.FormClosed, AddressOf CloseFormFloat
        frmMain.Hide()
    End Sub

    Public Sub CloseFormFloat(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)
        FloatHwnd = Nothing
        CheckHold(False)
        showcustomerwait()
        FindQueueService()
    End Sub

    'Private Sub btnMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMessage.Click
    '    TimerMessageCounter.Enabled = False
    '    TimerMessageCounterColor.Enabled = False
    '    frmMain.TimerBeep.Enabled = False
    '    If MsgShow = False Then
    '        Dim f As New frmWriteMessage
    '        f.ShowDialog()
    '    Else
    '        Dim f As New frmMessageCounter
    '        f.ShowDialog()
    '        btnMessage.BackColor = Drawing.Color.White
    '        btnMessage.ForeColor = Drawing.Color.Black
    '    End If
    '    TimerMessageCounter.Enabled = True
    '    TimerMessageCounterColor.Enabled = True
    'End Sub

    '#Region "TabMsg"
    '    Private Sub Grid_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
    '        If Grid.Rows.Count > 0 Then
    '            lblQueue_msg.Text = Grid.SelectedRows(0).Cells("queue_no_msg").Value.ToString
    '            lblCustomerID_msg.Text = Grid.SelectedRows(0).Cells("customer_id_msg").Value.ToString
    '            txtMsg.Focus()
    '        End If
    '    End Sub

    '    Sub AddTime()
    '        Dim j As String = ""
    '        For i As Int32 = 0 To 23
    '            j = CStr(i)
    '            ComboH.Items.Add(j.ToString.PadLeft(2, CChar("0")))
    '        Next

    '        For i As Int32 = 0 To 60
    '            If i > 59 Then
    '                Exit For
    '            End If
    '            j = CStr(i)
    '            i = i + 5
    '            If CInt(j) < 10 Then
    '                j = "0" & j
    '            End If
    '            i = i - 1
    '            ComboM.Items.Add(j)
    '        Next
    '    End Sub

    '    Sub findtime()
    '        Dim mm As String = FindDateNow.Hour.ToString.PadLeft(2, CChar("0"))
    '        Dim nn As String = FindDateNow.Minute.ToString.PadLeft(2, CChar("0"))
    '        Dim n As Int32 = CInt(nn) Mod 5
    '        If n <> 0 Then
    '            n = (5 - n) + CInt(nn)
    '            nn = CStr(n).ToString.PadLeft(2, CChar("0"))
    '            If nn = "60" Then
    '                nn = "00"
    '                mm = CStr(CInt(mm) + 1)
    '            End If
    '        End If
    '        Try
    '            ComboH.SelectedIndex = ComboH.FindString(mm)
    '            ComboM.SelectedIndex = ComboM.FindString(nn)
    '        Catch ex As Exception : End Try
    '    End Sub

    '    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '        If lblQueue_msg.Text = "-" Then
    '            MessageBox.Show("กรุณาระบุลูกค้า ที่ต้องการส่งข้อความ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Exit Sub
    '        End If

    '        If txtMsg.Text.Trim = "" Then
    '            MessageBox.Show("กรุณากรอกข้อความที่ต้องการจะส่ง !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Exit Sub
    '        End If

    '        Dim dt_tmp As New DataTable
    '        Dim time_select As String = ComboH.SelectedItem.ToString & ":" & ComboM.SelectedItem.ToString
    '        Dim time_now As String = FindDateNow.ToShortTimeString
    '        dt_tmp = getDataTable("select datediff(n,'" & time_select & "','" & time_now & "') as dd")
    '        If dt_tmp.Rows.Count > 0 Then
    '            If CInt(dt_tmp.Rows(0)("dd").ToString) > 0 Then
    '                MessageBox.Show("ไม่สามารถระบุเวลา ย้อนหลังได้  !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If
    '        End If

    '        Dim sql As String = ""
    '        sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_message);insert into TB_message(id,queue_no,customer_id,message,message_date,message_status,create_by,create_date) values(@ID,'" & FixDB(lblQueue_msg.Text) & "','" & FixDB(lblCustomerID_msg.Text) & "','" & FixDB(txtMsg.Text) & "','" & FixDate(FindDateNow) & " " & time_select & "',1," & myUser.user_id & ",getdate())"
    '        executeSQL(sql)
    '        MessageBox.Show("ข้อความถูกส่งเรียบร้อยแล้ว ", "ส่งข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        txtSearch.Text = ""
    '        lblCustomerID_msg.Text = ""
    '        lblQueue_msg.Text = "-"
    '    End Sub

    '    Private Sub TextSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
    '        If Asc(e.KeyChar) = 13 Then
    '            Grid_CellMouseDoubleClick(Nothing, Nothing)
    '        End If
    '    End Sub

    '#End Region

    'Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    sql = "select * from TB_MESSAGE_COUNTER where message_status = 1 and DATEDIFF(D,GETDATE(),create_date)=0 and counter_id = " & myUser.counter_id & " order by create_date asc"
    '    dt = getDataTable(sql)
    '    If dt.Rows.Count > 0 Then
    '        MsgShow = True
    '    End If
    'End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If MsgShow = True Then

    '        If btnMessage.BackColor = Drawing.Color.White Then
    '            btnMessage.BackColor = Drawing.Color.Yellow
    '            btnMessage.ForeColor = Drawing.Color.Red
    '        ElseIf btnMessage.BackColor = Drawing.Color.Yellow Then
    '            btnMessage.BackColor = Drawing.Color.Blue
    '            btnMessage.ForeColor = Drawing.Color.Yellow
    '        ElseIf btnMessage.BackColor = Drawing.Color.Blue Then
    '            btnMessage.BackColor = Drawing.Color.Yellow
    '            btnMessage.ForeColor = Drawing.Color.Red
    '        End If
    '        frmMain.TimerBeep.Enabled = True
    '    End If
    'End Sub

    Sub CheckCounterFunction()
        If myUser.counter_id > 0 Then
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select quickservice,speed_lane from TB_COUNTER where id = " & myUser.counter_id
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                CounterFunction.QuickService = dt.Rows(0).Item("quickservice")
                CounterFunction.SpeedLane = dt.Rows(0).Item("speed_lane")
                'CounterFunction.Beep = dt.Rows(0).Item("beep")
                'CounterFunction.ReturnCase = dt.Rows(0).Item("return_case")
                'CounterFunction.Swap = dt.Rows(0).Item("auto_swap")
            End If
        End If
    End Sub

    Private Sub btnExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpand.Click
        If btnExpand.Tag.ToString = "EXP" Then
            Me.WindowState = FormWindowState.Maximized
            btnExpand.Text = "      Window"
            btnExpand.Tag = "WND"
            Me.MdiParent = Nothing
        Else
            Me.WindowState = FormWindowState.Maximized
            btnExpand.Text = "      Expand"
            btnExpand.Tag = "EXP"
            Me.MdiParent = frmMain
        End If

        If CounterFunction.QuickService = 1 Then
            GV_Height = GroupBoxView.Height + 40
        Else
            GV_Height = GroupBoxView.Height
        End If
    End Sub

    Private Sub GridWait_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GridWait.CellMouseDoubleClick
        If txtDisplayQueue.Text <> "" Then Exit Sub
        Try
            btnCall.Enabled = False
            GridWait.Enabled = False
            frmMain.timerCheckForce.Enabled = False
            frmMain.timerForce.Enabled = False
            frmMain.BtnLogout.Enabled = False

            Dim sql As String = ""
            Dim Old_Status As Int32 = GridWait.SelectedRows(0).Cells("status").Value.ToString
            Dim time_hold As String = GridWait.SelectedRows(0).Cells("hold").Value.ToString
            Dim QueueNo As String = GridWait.SelectedRows(0).Cells("queue_no").Value.ToString
            Dim CustomerTypeID As Int32 = GridWait.SelectedRows(0).Cells("customertype_id").Value.ToString
            Dim CustomerTypeName As String = GridWait.SelectedRows(0).Cells("customertype_name").Value.ToString
            Dim CustomerID As String = GridWait.SelectedRows(0).Cells("customer_id").Value.ToString
            Dim CustomerName As String = GridWait.SelectedRows(0).Cells("customer_name").Value.ToString
            Dim t_hold As Int32 = 0
            If Old_Status = 6 Then
                t_hold = DateDiff(DateInterval.Minute, CDate(FindDateNow.Hour & ":" & FindDateNow.Minute), CDate(time_hold))
            End If

            Dim DateNow As String = FindDateNow().ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))
            If CreateTransaction("frmServiceQueue_GridWait_CellMouseDoubleClick") = True Then
                If UpdateCallStatus(myUser.counter_id, QueueNo, CustomerID, DateNow, shTrans) = True Then
                    'UnitDisplay
                    CallUnitDisplay(QueueNo, shTrans)
                    CallSpeaker(QueueNo, shTrans)
                    CommitTransaction()

                    showcustomerwait()
                    Dim f As New frmServiceConfirm
                    f.QueueNo = QueueNo
                    f.CustomerTypeID = CustomerTypeID
                    f.CustomerTypeName = CustomerTypeName
                    f.CustomerID = CustomerID
                    f.CustomerName = CustomerName
                    frmMain.BtnLogout.Enabled = True

                    If f.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                        Select Case Old_Status
                            Case 8
                                UpdateQueueStatus(8, 0, 0, QueueNo, CustomerID)
                            Case 6
                                UpdateQueueStatus(6, 0, t_hold, QueueNo, CustomerID)
                            Case Else
                                UpdateQueueStatus(1, 0, 0, QueueNo, CustomerID)
                                If time_hold <> "-" Then
                                    Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
                                    DateNow = FixDate(FindDateNow)
                                    sql = "update tb_counter_queue set hold = '" & DateNow & " " & time_hold & "' where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status = 1"
                                    If executeSQL(sql) = True Then
                                        'If CurrDB = "MAIN" Then
                                        '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                                        'End If
                                    End If
                                End If
                        End Select

                        'UnitDisplay
                        ClearUnitDisplay(False)
                    ElseIf f.DialogResult = Windows.Forms.DialogResult.OK Then
                        'Hold
                        If CreateTransaction("frmServiceQueue_GridWait_CellMouseDoubleClick") = True Then
                            ClearUnitDisplay(False, shTrans)
                            ClearMainDisplay(myUser.counter_id, shTrans)
                            CommitTransaction()
                        End If
                    ElseIf f.DialogResult = Windows.Forms.DialogResult.Yes Then
                        'UnitDisplay
                        ServeUnitDisplay(f.QueueNo)
                        Dim ff As New frmEndByService
                        ff.QueueNo = QueueNo
                        ff.CustomerID = CustomerID
                        frmMain.Hide()
                        ff.ShowDialog()
                        QM.CloseQM()
                        frmMain.Show()
                    End If
                    showcustomerwait()
                    FindQueueService()
                Else
                    RollbackTransaction()

                    MessageBox.Show("The information has already been updated by the other user.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    showcustomerwait()
                    FindQueueService()
                End If
            Else
                showcustomerwait()
            End If
        Catch ex As Exception : End Try

        frmMain.timerCheckForce.Enabled = True
        frmMain.BtnLogout.Enabled = True
        btnCall.Enabled = True
        GridWait.Enabled = True
    End Sub

    Private Sub pb2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb2.Click
        CheckHold(True)
        'UnitDisplay
        ClearUnitDisplay(False)
        Dim sql As String = ""
        sql = "update TB_user set counter_id = 0 ,item_id = 0 where id =" & myUser.user_id
        executeSQL(sql)
        Me.Close()
        frmMain.TimerCheckHoldRoom.Enabled = True
        frmMain.TimerCheckStatus.Enabled = False
    End Sub

    Private Sub pb1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb1.Click
        CheckHold(True)
        'UnitDisplay
        PauseUnitDisplay()
        QM.CloseQMSchedule()
    End Sub

    Sub avgServiceTime()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select TB_ITEM.id,item_name ," & vbNewLine
        sql += " case when avg(DATEDIFF(SS,start_time,end_time)) IS null " & vbNewLine
        sql += " then '-' " & vbNewLine
        sql += " else case when avg(DATEDIFF(SS,start_time,end_time))/ 60 > 9 " & vbNewLine
        sql += "    then CONVERT(varchar(10),avg(DATEDIFF(SS,start_time,end_time)) / 60) " & vbNewLine
        sql += "    else right(('0' + CONVERT(varchar(10),avg(DATEDIFF(SS,start_time,end_time)) / 60)),2) " & vbNewLine
        sql += "    end + ':' + right(('0' + convert(varchar(10),avg(DATEDIFF(SS,start_time,end_time)) % 60)),2) " & vbNewLine
        sql += " end as servicetime ," & vbNewLine
        sql += " count(1) as serve " & vbNewLine
        sql += " from TB_COUNTER_QUEUE " & vbNewLine
        sql += " left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id " & vbNewLine
        sql += " where DATEDIFF(D,GETDATE(),service_date)=0 and status = 3 " & vbNewLine
        sql += " and user_id = " & myUser.user_id & vbNewLine
        sql += " group by TB_ITEM.id,item_name " & vbNewLine
        sql += " order by item_name" & vbNewLine
        dt = getDataTable(sql)
        GridItem.Columns("item_id").Visible = False
        GridItem.AutoGenerateColumns = False
        GridItem.DataSource = dt
    End Sub

    Private Sub cbItem_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbItem.SelectionChangeCommitted
        myUser.item_id = cbItem.SelectedValue
        Dim sql As String = ""
        sql = "update tb_user set item_id = " & myUser.item_id & " where id = " & myUser.user_id
        executeSQL(sql)
        showcustomerwait()
        frmMain.timerCheckForce.Enabled = False
        frmMain.timerForce.Enabled = False
        frmMain.timerCheckForce.Enabled = True
    End Sub
  
    Private Sub timerRefreshWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefreshWait.Tick
        showcustomerwait()
    End Sub

    Private Sub btnCam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCam.Click
        If System.IO.File.Exists(Application.StartupPath & "\QMPreview.exe") Then
            Dim proc As New Process()
            proc.StartInfo.FileName = Application.StartupPath & "\QMPreview.exe"
            proc.StartInfo.Arguments = ""
            proc.Start()
        Else
            MessageBox.Show("find not found QMPreview.exe", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class