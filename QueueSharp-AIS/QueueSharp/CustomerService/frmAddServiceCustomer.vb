Imports System.Data.SqlClient
Imports Engine.Common.ShopConnectDBENG

Public Class frmAddServiceCustomer

    Public QueueNo As String = ""
    Public Mobile As String = ""

    Private Sub frmAddServiceCustomer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub frmEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,item_name  from TB_ITEM where active_status = 1 and id not in (select item_id from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and status in (1,2,3,4,5) and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "') order by item_order"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                AddService(dt.Rows(i).Item("item_name").ToString, dt.Rows(i).Item("id").ToString)
            Next
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim ItemID As Int32 = 0
        For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            If FLP_Item.Controls(i).BackColor = Color.RoyalBlue Then
                ItemID = FLP_Item.Controls(i).Name
            End If
        Next

        If ItemID = 0 Then
            MessageBox.Show("You need to select one service to click OK button.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select top 1 * from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "' order by service_date  desc"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim CurrDB As String = CheckCurrentDB(INIFileName)
            Dim vDateNow As String = FixDateTime(FindDateNow)

            If CreateTransaction("frmAddServiceCustomer_btnAdd_Click") = True Then
                Dim vQid As Long = FindID("TB_COUNTER_QUEUE", shTrans)
                sql = " declare @ServiceDate as datetime;select @ServiceDate = (select MAX(service_date) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "' and status = 2)" & vbCrLf
                sql &= " declare @AssignTime as datetime;select @AssignTime = (select MAX(assign_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "' and status = 2)" & vbCrLf
                sql &= " declare @CallTime as datetime;select @CallTime = (select MAX(call_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "' and status = 2)" & vbCrLf
                sql &= " declare @StartTime as datetime;select @StartTime = (select MAX(start_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "' and status = 2)" & vbCrLf
                sql &= " insert into TB_counter_queue(id,queue_no,customer_id,customertype_id,"
                sql &= " item_id,customer_name,segment,counter_id,service_date,status,user_id,assign_time,call_time,start_time,network_type,add_service,call_auto_force) "
                sql &= " values('" & vQid & "','" & FixDB(QueueNo) & "','" & FixDB(Mobile) & "'," & dt.Rows(0).Item("customertype_id").ToString & ","
                sql &= ItemID & ",'','" & dr("segment").ToString & "'," & myUser.counter_id & ",@ServiceDate,2," & myUser.user_id & ",@AssignTime,@CallTime,@StartTime,'" & dr("network_type").ToString & "','1','" & dr("call_auto_force").ToString & "')"


                If executeSQL(sql, shTrans, True) = True Then
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If

                    InsertLog(QueueNo, Mobile, myUser.user_id, ItemID, myUser.counter_id, 2, "Add", shTrans, vDateNow, CurrDB)

                    Dim combo As String = dt.Rows(0).Item("combo_item_all").ToString & "," & ItemID.ToString
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set combo_item_all = '" & combo & "' "
                    sql += " where DATEDIFF(D,GETDATE(),service_date)=0 "
                    sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(Mobile) & "'"
                    If executeSQL(sql, shTrans, True) = False Then
                        RollbackTransaction()
                        SaveQueryErrorLog("Cannon Execute SQL", sql)
                        MessageBox.Show("The selected service already exists! Please select the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        'If CurrDB = "MAIN" Then
                        '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                        'End If
                        CommitTransaction()
                        QM.InsertQmTransferLog(7, vQid)
                        Me.DialogResult = Windows.Forms.DialogResult.Yes
                    End If
                Else
                    RollbackTransaction()
                    SaveQueryErrorLog("Cannon Execute SQL", sql)
                    MessageBox.Show("The selected service already exists! Please select the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                SaveQueryErrorLog("Cannon Create Transaction", "")
                MessageBox.Show("The selected service already exists! Please select the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            MessageBox.Show("The selected service already exists! Please select the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    Sub AddService(ByVal ItemName As String, ByVal ItemId As Int32)
        Dim lbl As New Label
        'Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        With lbl
            .AutoSize = False
            .Width = 240
            .Height = 30
            .Tag = ItemId.ToString
            .Name = ItemId.ToString
            .Text = ItemName
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
            .BackColor = Color.White
            '.Font = Font
        End With
        FLP_Item.Controls.Add(lbl)
        AddHandler lbl.Click, AddressOf CheckStatusItem
    End Sub

    Private Sub CheckStatusItem(ByVal Sender As Object, ByVal e As EventArgs)
        For i As Int32 = 0 To FLP_Item.Controls.Count - 1
            FLP_Item.Controls(i).BackColor = Color.White
            FLP_Item.Controls(i).ForeColor = Color.Black
        Next
        Dim btn As Label = Sender
        If btn.BackColor = Color.White Then
            btn.BackColor = Color.RoyalBlue
            btn.ForeColor = Color.White
        Else
            btn.BackColor = Color.White
            btn.ForeColor = Color.Black
        End If
    End Sub
End Class