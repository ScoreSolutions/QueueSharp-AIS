Imports QueueSharp.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG

Public Class frmAwaitingCustomer

    Private Sub AwaitingCustomer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Sub ShowData()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "exec SP_AwaitingCustomer"
        dt = getDataTable(sql)
        Grid.DataSource = dt
    End Sub

    Private Sub frmAwaitingCustomer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ShowData()
        Grid.AutoGenerateColumns = False
        GridWait.AutoGenerateColumns = False
        timerRefresh.Interval = AutoRefresh()
    End Sub

    Private Sub Grid_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GridWait.CellMouseClick
        If GridWait.Rows.Count > 0 Then
            If GridWait.SelectedRows(0).Cells(e.ColumnIndex).Value.ToString.ToUpper = "CANCEL QUEUE" Then
                timerRefresh.Enabled = False
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select * from tb_counter_queue where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString) & "' and status in (2,4)"
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    MessageBox.Show("This queue " & GridWait.SelectedRows(0).Cells("queue_no").Value.ToString & " is being served ", "Attention")
                    timerRefresh.Interval = AutoRefresh()
                    timerRefresh.Enabled = True
                    Exit Sub
                End If

                Dim f As New frmDialogYesNo
                f.Text = "Confirm"
                f.lblTxt.Text = "Do you want to cancel queue number " & GridWait.SelectedRows(0).Cells("queue_no").Value.ToString & "?"
                If f.ShowDialog = Windows.Forms.DialogResult.Yes Then
                    If CreateTransaction("frmAwaitingCustomer_Grid_CellMouseClick") = True Then
                        sql = "select y.id,y.item_name, x.customer_id, x.customertype_id, getdate() datenow from TB_COUNTER_QUEUE x left join TB_ITEM y on x.item_id = y.id where DATEDIFF(D,GETDATE(),service_date) = 0 and status =1 and queue_no = '" & FixDB(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString) & "' and customer_id = '" & FixDB(GridWait.SelectedRows(0).Cells("customer_id").Value.ToString) & "'"
                        dt = New DataTable
                        dt = getDataTable(sql, shTrans)
                        If dt.Rows.Count > 0 Then
                            Dim qItemID As Integer = 0
                            Dim IsRollBack As Boolean = False
                            Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)

                            sql = "update tb_counter_queue "
                            sql += " set status = 5,call_time = assign_time,start_time = assign_time,"
                            sql += " end_time = assign_time,user_id = " & myUser.user_id & ","
                            sql += " counter_id = " & myUser.counter_id & " "
                            sql += " where datediff(d,getdate(),service_date)=0 "
                            sql += " and queue_no = '" & FixDB(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString) & "' "
                            sql += " and customer_id = '" & FixDB(GridWait.SelectedRows(0).Cells("customer_id").Value.ToString) & "' "
                            sql += " and status=1"
                            If executeSQL(sql, shTrans, True) = True Then
                                Dim vDateNow As String = FixDateTime(Convert.ToDateTime(dt.Rows(0)("datenow")))
                                InsertLog(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString, GridWait.SelectedRows(0).Cells("customer_id").Value.ToString, myUser.user_id, 0, myUser.counter_id, 5, "Cancel Queue From frmAwaitingCustomer", shTrans, vDateNow, CurrDB)
                                qItemID = dt.Rows.Count
                            Else
                                IsRollBack = True
                            End If

                            'For i As Int32 = 0 To dt.Rows.Count - 1
                            '    sql = "update tb_counter_queue "
                            '    sql += " set status = 5,call_time = assign_time,start_time = assign_time,"
                            '    sql += " end_time = assign_time,user_id = " & myUser.user_id & ","
                            '    sql += " counter_id = " & myUser.counter_id & " "
                            '    sql += " where datediff(d,getdate(),service_date)=0 "
                            '    sql += " and queue_no = '" & FixDB(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString) & "' "
                            '    sql += " and customer_id = '" & FixDB(GridWait.SelectedRows(0).Cells("customer_id").Value.ToString) & "' "
                            '    sql += " and item_id = " & dt.Rows(i).Item("id").ToString
                            '    sql += " and status=1"
                            '    If executeSQL(sql, shTrans, True) = True Then
                            '        'If CurrDB = "MAIN" Then
                            '        '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                            '        'End If

                            '        Dim vDateNow As String = FixDateTime(Convert.ToDateTime(dt.Rows(i)("datenow")))
                            '        InsertLog(GridWait.SelectedRows(0).Cells("queue_no").Value.ToString, GridWait.SelectedRows(0).Cells("customer_id").Value.ToString, myUser.user_id, dt.Rows(i).Item("id").ToString, myUser.counter_id, 5, "Cancel Queue From frmAwaitingCustomer", shTrans, vDateNow, CurrDB)
                            '        qItemID += 1
                            '    Else
                            '        IsRollBack = True
                            '        Exit For
                            '    End If
                            'Next
                            If IsRollBack = False Then
                                CommitTransaction()

                                If dt.Rows(0)("customertype_id") = "3" Then
                                    'If dt.Rows.Count = qItemID Then
                                    'คืน Slot ก่อน
                                    UpdateAppointmentSlot(GridWait.SelectedRows(0).Cells("customer_id").Value.ToString, GridWait.SelectedRows(0).Cells("queue_no").Value.ToString)

                                    sql = "select appointment_job_id,customer_id,queue_no "
                                    sql += " from TB_APPOINTMENT_CUSTOMER "
                                    sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                                    sql += " and customer_id = '" & FixDB(GridWait.SelectedRows(0).Cells("customer_id").Value.ToString) & "' "
                                    sql += " and queue_no='" & GridWait.SelectedRows(0).Cells("queue_no").Value.ToString & "' "
                                    Dim aDt As New DataTable
                                    aDt = getDataTable(sql)
                                    If aDt.Rows.Count > 0 Then
                                        'Register แล้ว Cancel Queue  กรณีเป็นคิวจอง
                                        sql = "update TB_APPOINTMENT_CUSTOMER "
                                        sql += " set active_status = 6 "
                                        sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                                        sql += " and customer_id = '" & FixDB(GridWait.SelectedRows(0).Cells("customer_id").Value.ToString) & "' "
                                        sql += " and queue_no='" & GridWait.SelectedRows(0).Cells("queue_no").Value.ToString & "' "
                                        If executeSQL(sql) = True Then
                                            UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "6")

                                            'กรณีเลือก Cancel ทุก Service Update Siebel To Cancel กรณีเป็นคิวจอง
                                            SiebelUpdateToCancel(dt.Rows(0)("customer_id"), dt.Rows(0)("customertype_id"))
                                        End If
                                    End If
                                    aDt.Dispose()
                                    'End If
                                End If
                            Else
                                RollbackTransaction()
                            End If
                        Else
                            RollbackTransaction()
                            MessageBox.Show("This queue " & GridWait.SelectedRows(0).Cells("queue_no").Value.ToString & " is being served ", "Attention")
                        End If
                    End If

                    ShowData()
                    timerRefresh.Interval = AutoRefresh()
                    timerRefresh.Enabled = True
                End If
            ElseIf GridWait.SelectedRows(0).Cells(e.ColumnIndex).Value.ToString.ToUpper = "CANCEL SERVICE" Then
                timerRefresh.Enabled = False
                Dim f As New frmServiceCancel
                f.QueueNo = GridWait.SelectedRows(0).Cells("queue_no").Value.ToString
                f.CustomerID = GridWait.SelectedRows(0).Cells("customer_id").Value.ToString
                f.Flag = "Cancel Service From frmAwaitingCustomer"
                f.CountEnd = 1
                If f.ShowDialog = Windows.Forms.DialogResult.Yes Then
                    ShowData()
                    timerRefresh.Interval = AutoRefresh()
                    timerRefresh.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub Grid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.SelectionChanged
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select queue_no,customer_id from tb_counter_queue where datediff(d,getdate(),service_date)=0 and status = 1 and item_id = " & Grid.SelectedRows(0).Cells("id").Value
            dt = getDataTable(sql)
            GridWait.DataSource = dt
            If GridWait.RowCount > 0 Then
                For i As Int32 = 0 To GridWait.RowCount - 1
                    GridWait.Item("Cancel", i).Style.BackColor = Drawing.Color.MistyRose
                    GridWait.Item("Cancel", i).Style.SelectionBackColor = Drawing.Color.MistyRose
                    GridWait.Item("Cancel", i).Style.ForeColor = Drawing.Color.Black
                    GridWait.Item("Cancel", i).Style.SelectionForeColor = Drawing.Color.Black
                Next
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick
        'ShowData()
    End Sub
End Class