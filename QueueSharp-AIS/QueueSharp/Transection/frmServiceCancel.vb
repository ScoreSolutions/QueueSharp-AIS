Imports System.Data
Imports System.Data.SqlClient
Imports QueueSharp.Org.Mentalis.Files

Public Class frmServiceCancel

    Public CustomerID As String = ""
    Public CustomerTypeID As String = ""
    Public QueueNo As String = ""
    Public StatusCustomer As Int32 = 0
    Public Flag As String = ""
    Public CountEnd As Int32 = 0
    Public ServiceTime As Int32 = 0
    Public FristEnd As Boolean = False

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim sql As String = ""
        Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
        Dim vDateNow As String = FixDateTime(FindDateNow)
        Select Case True
            Case RadioHold.Checked 'Hold
                If CheckHold(QueueNo, CustomerID) = False Then
                    UpdateQueueStatus(6, myUser.counter_id, txtHoldTime.Text, QueueNo, CustomerID, , txtCancelAll.Text.Trim)
                End If
                Me.DialogResult = Windows.Forms.DialogResult.OK

            Case RadioCancelService.Checked 'ยกเลิกบริการเฉพาะบริการนี้
                If txtCancelAll.Text.Trim = "" Then
                    MessageBox.Show("Please Enter Remark.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim ret As Boolean = False
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    If GridItem.Rows(i).Cells("cb").Value = True Then
                        ret = True
                        Exit For
                    End If
                Next
                'ถ้าไม่มีการเลือก Service
                If ret = False Then
                    MessageBox.Show("Please select Service", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                '####################################################
                '####### frmAwaiting Customer   กด Cancel Service
                '####################################################
                If CountEnd = 1 Then
                    For i As Int32 = 0 To GridItem.Rows.Count - 1
                        If GridItem.Rows(i).Cells("cb").Value = True Then
                            ret = True
                            sql = "update tb_counter_queue set status = 5,assign_time = service_date,call_time = service_date,start_time = service_date,end_time = service_date,user_id = " & myUser.user_id & ",counter_id = " & myUser.counter_id & " where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & GridItem.Rows(i).Cells("item_id").Value.ToString
                            executeSQL(sql)
                            'If CurrDB = "MAIN" Then
                            '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                            'End If
                            InsertLog(QueueNo, CustomerID, myUser.user_id, GridItem.Rows(i).Cells("item_id").Value.ToString, myUser.counter_id, 5, Flag & " Remarks:" & txtCancelAll.Text, Nothing, vDateNow, CurrDB)
                        End If
                    Next

                    Dim dt As New DataTable
                    sql = "select * from tb_counter_queue where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' "
                    dt = getDataTable(sql)
                    If dt.Rows.Count > 0 Then
                        dt.DefaultView.RowFilter = " status in (1,2,4)"
                        If dt.DefaultView.Count = 0 Then
                            'จะต้อง Cancel ทุก Service ถึงจะคืน Slot
                            UpdateAppointmentSlot(CustomerID, QueueNo)
                        End If

                        dt.DefaultView.RowFilter = " status = 5"
                        If dt.DefaultView.Count = dt.Rows.Count Then
                            sql = "select appointment_job_id "
                            sql += " from TB_APPOINTMENT_CUSTOMER "
                            sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                            sql += " and customer_id = '" & FixDB(CustomerID) & "' "
                            sql += " and queue_no='" & FixDB(QueueNo) & "' "
                            Dim aDt As New DataTable
                            aDt = getDataTable(sql)
                            If aDt.Rows.Count > 0 Then
                                sql = "update TB_APPOINTMENT_CUSTOMER "
                                sql += " set active_status = 6 "   'Cancel Queue
                                sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
                                sql += " and customer_id = '" & FixDB(CustomerID) & "' "
                                sql += " and queue_no = '" & FixDB(QueueNo) & "'"
                                executeSQL(sql)

                                'Update Appointment Job To DC
                                UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "6")

                                'Update Siebel To Cancel กรณีเป็นคิวจอง  กด Cancel ทุก Service จากหน้าจอ Awaiting Customer
                                SiebelUpdateToCancel(FixDB(CustomerID), dt.DefaultView(0)("customertype_id"))
                            End If
                            aDt.Dispose()
                        End If
                    End If
                    dt.Dispose()
                Else

                    '#########################################################
                    '######## frmEndByService กด Cancel จากหน้า Service Time
                    '###########################################################
                    Dim FristRows As Int32 = 0
                    For i As Int32 = 0 To GridItem.Rows.Count - 1
                        If GridItem.Rows(i).Cells("cb").Value = True Then
                            If FristEnd = True And FristRows = 0 Then
                                'Cancel Service แรก
                                sql = "declare @CallTime as datetime;select @CallTime = (select MAX(call_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "')" & vbCrLf
                                sql &= "update tb_counter_queue set status = 5,start_time = @CallTime,end_time = @CallTime,user_id = " & myUser.user_id & ",counter_id = " & myUser.counter_id & " where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & GridItem.Rows(i).Cells("item_id").Value.ToString
                                executeSQL(sql)
                                'If CurrDB = "MAIN" Then
                                '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                                'End If
                            Else
                                sql = "declare @EndTime as datetime;select @EndTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "')" & vbCrLf
                                sql &= "update tb_counter_queue set status = 5,assign_time = @EndTime,call_time = @EndTime,start_time = @EndTime,end_time = @EndTime where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & GridItem.Rows(i).Cells("item_id").Value.ToString
                                executeSQL(sql)
                                'If CurrDB = "MAIN" Then
                                '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                                'End If
                            End If
                            InsertLog(QueueNo, CustomerID, myUser.user_id, GridItem.Rows(i).Cells("item_id").Value.ToString, myUser.counter_id, 5, "Cancel Service From frmEndByService Remarks:" & txtCancelAll.Text, Nothing, vDateNow, CurrDB)

                            FristRows += 1
                        End If
                    Next

                    sql = "select customertype_id, status from tb_counter_queue where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "'"
                    Dim dt As New DataTable
                    dt = getDataTable(sql)
                    If dt.Rows.Count > 0 Then
                        'ถ้าเป็นคิวจอง
                        If dt.Rows(0)("customertype_id") = "3" Then
                            dt.DefaultView.RowFilter = " status = 5 "
                            If dt.Rows.Count = dt.DefaultView.Count Then
                                'คืน Slot ก่อน
                                UpdateAppointmentSlot(CustomerID, QueueNo)

                                sql = "select appointment_job_id "
                                sql += " from TB_APPOINTMENT_CUSTOMER "
                                sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                                sql += " and customer_id = '" & FixDB(CustomerID) & "' "
                                sql += " and queue_no='" & FixDB(QueueNo) & "' "
                                Dim aDt As New DataTable
                                aDt = getDataTable(sql)
                                If aDt.Rows.Count > 0 Then
                                    sql = "update TB_APPOINTMENT_CUSTOMER "
                                    sql += " set active_status = 6 "   'Cancel Queue
                                    sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
                                    sql += " and customer_id = '" & FixDB(CustomerID) & "' "
                                    sql += " and queue_no = '" & FixDB(QueueNo) & "'"
                                    executeSQL(sql)

                                    'Update Appointment Job To DC
                                    UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "6")

                                    'Update Siebel To Cancel กรณีเป็นคิวจอง กด Cancel  ทุก Service จากหน้าจอ Service Time
                                    SiebelUpdateToCancel(FixDB(CustomerID), dt.Rows(0)("customertype_id"))
                                End If
                                aDt.Dispose()
                            End If
                        End If
                    End If
                    dt.Dispose()
                End If

                Me.DialogResult = Windows.Forms.DialogResult.Yes
        End Select

        Me.Close()
    End Sub

    Private Sub frmServiceCancel_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then
            If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        End If
    End Sub

    Private Sub btn5M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5M.Click
        btn5M.BackColor = Color.Red
        btn10M.BackColor = Color.Gainsboro
        btn15M.BackColor = Color.Gainsboro
        btn5M.ForeColor = Color.White
        btn10M.ForeColor = Color.DimGray
        btn15M.ForeColor = Color.DimGray
        txtHoldTime.Text = "5"
    End Sub

    Private Sub btn10M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn10M.Click
        btn5M.BackColor = Color.Gainsboro
        btn10M.BackColor = Color.Red
        btn15M.BackColor = Color.Gainsboro
        btn5M.ForeColor = Color.DimGray
        btn10M.ForeColor = Color.White
        btn15M.ForeColor = Color.DimGray
        txtHoldTime.Text = "10"
    End Sub

    Private Sub btn15M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn15M.Click
        btn5M.BackColor = Color.Gainsboro
        btn10M.BackColor = Color.Gainsboro
        btn15M.BackColor = Color.Red
        btn5M.ForeColor = Color.DimGray
        btn10M.ForeColor = Color.DimGray
        btn15M.ForeColor = Color.White
        txtHoldTime.Text = "15"
    End Sub

    Private Sub frmServiceCancel_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'RadioHold.Checked = True
        RadioCancelService.Checked = True
        txtHoldTime.Text = "5"
        Dim sql As String = ""
        Dim dt As New DataTable
        dt = New DataTable
        sql = "select y.id,y.item_name from TB_COUNTER_QUEUE x left join TB_ITEM y on x.item_id = y.id where DATEDIFF(D,GETDATE(),service_date) = 0 and status in (1,2,4,6,8) and item_id = " & myUser.item_id & " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' union all select y.id,y.item_name from TB_COUNTER_QUEUE x left join TB_ITEM y on x.item_id = y.id where DATEDIFF(D,GETDATE(),service_date) = 0 and status in (1,2,4,6,8) and item_id <> " & myUser.item_id & " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "'"
        dt = getDataTable(Sql)
        GridItem.DataSource = dt
    End Sub

    Private Sub Radiowait_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioHold.CheckedChanged
        If RadioHold.Checked = True Then
            btn5M.Enabled = True
            btn10M.Enabled = True
            btn15M.Enabled = True
        Else
            btn5M.Enabled = False
            btn10M.Enabled = False
            btn15M.Enabled = False
        End If
    End Sub

    Private Sub RadioSer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioCancelService.CheckedChanged
        If RadioCancelService.Checked = True Then
            GridItem.Enabled = True
            GridItem.BackgroundColor = Color.White
            txtCancelAll.Enabled = True
            cbSelectAll.Enabled = True
        Else
            GridItem.Enabled = False
            GridItem.BackgroundColor = Color.WhiteSmoke
            txtCancelAll.Enabled = False
            cbSelectAll.Enabled = False
            If GridItem.Rows.Count > 0 Then
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = False
                Next
            End If
        End If
    End Sub

    Private Sub cbSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelectAll.CheckedChanged
        If cbSelectAll.Checked Then
            If GridItem.Rows.Count > 0 Then
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = True
                Next
            End If
        Else
            If GridItem.Rows.Count > 0 Then
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = False
                Next
            End If
        End If
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ServiceTime += 1
    End Sub
End Class