Imports System.IO
Imports QMTransferLog.Org.Mentalis.Files

Public Class frmQMTransferLog

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            Dim INIFileName As String = Application.StartupPath & "\Config.ini"
            Dim Path As String
            Dim ini As New IniReader(INIFileName)
            Path = ini.ReadString("Setting", "Path")
            If Directory.Exists(Path) Then
                Dim files() As String = IO.Directory.GetFiles(Path, "*.flv")
                If files.Length > 0 Then
                    ProgressBar1.Maximum = files.Length
                    ProgressBar1.Value = 0

                    For Each f As String In IO.Directory.GetFiles(Path, "*.flv")
                        Dim file As New FileInfo(f)
                        Dim str() As String = file.Name.Split(".")

                        Dim _servicedate As String = ""
                        Dim _qid As String = ""
                        If str(0).Length > 5 Then _servicedate = str(0).Substring(0, 6)
                        If str(0).Length > 6 Then _qid = str(0).Substring(6)

                        'เช็คกับ TB_QM_TransferLog(by service_date,tb_counter_queue_id)
                        If IsExistData(_servicedate, _qid) Then
                            'ถ้ามี Update Status = 4
                            UpdateQmTransferLog(4, _servicedate, _qid)

                        Else
                            'ถ้าไม่มี Insert Data โดยเอาข้อมูลจาก
                            'Query Table TB_Counter_Queue_History ถ้าไม่พบข้อมูลให้หาจาก  TB_Counter_Queue(by service_date,tb_counter_queue_id)
                            Dim dt As New DataTable
                            dt = GetQueueInfo(_servicedate, _qid)
                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                InsertTransferLog(_qid, dt)
                            End If
                        End If

                        ProgressBar1.Value += 1
                        Application.DoEvents()
                    Next
                End If
            End If
            MessageBox.Show("Success!!")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Function GetQueueInfo(ByVal ServiceDate As String, ByVal Qid As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim db As New DBClass
            Dim Sql As String = "select Service_Date,queue_no,customer_id,item_id,item_name as item_name_en,"
            Sql += " counter_id,counter_name,status  from TB_Counter_Queue_History "
            Sql += " where RIGHT(CONVERT(VARCHAR(10),service_date,112),6)='" & ServiceDate & "'"
            Sql += " and tb_counter_queue_id='" & Qid & "'"


            Dim ds As DataSet = db.SqlGet(Sql, "TB_Counter_Queue")
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                dt = ds.Tables(0)
            Else
                Sql = "select Service_Date,queue_no,customer_id,item_id,item_name as item_name_en,"
                Sql += " counter_id,counter_name,status  from TB_Counter_Queue  TQ "
                Sql += " left join TB_ITEM TI ON TQ.item_id=TI.id"
                Sql += " left Join TB_COUNTER TC ON TQ.counter_id = TC.id"
                Sql += " where RIGHT(CONVERT(VARCHAR(10),TQ.service_date,112),6)='" & ServiceDate & "'"
                Sql += " and TQ.id='" & Qid & "'"
                ds = New DataSet
                ds = db.SqlGet(Sql, "TB_Counter_Queue")
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If
        Catch ex As Exception

        End Try
        
        Return dt
    End Function

    Private Function IsExistData(ByVal ServiceDate As String, ByVal Qid As String) As Boolean
        Try
            Dim db As New DBClass
            Dim Sql As String = "select  top 1 id from TB_QM_TRANSFER_LOG "
            Sql += " where RIGHT(CONVERT(VARCHAR(10),service_date,112),6)='" & ServiceDate & "'"
            Sql += " and tb_counter_queue_id='" & Qid & "'"

            Dim ds As DataSet = db.SqlGet(Sql, "TB_QM_TRANSFER_LOG")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
        
        Return False
    End Function

    Private Function UpdateQmTransferLog(ByVal vStatus As String, ByVal ServiceDate As String, ByVal Qid As String) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        Dim db As New DBClass

        sql = "update TB_QM_TRANSFER_LOG"
        sql += " set update_date=getdate(), update_by='QMTransferLog', status='" & vStatus & "'"
        sql += ",transfer_date=getdate() "
        sql += " where tb_counter_queue_id='" & Qid & "' and RIGHT(CONVERT(VARCHAR(10),service_date,112),6)='" & ServiceDate & "'"
        ret = (db.SqlExecute(sql) > 0)
        db = Nothing

        Return ret
    End Function

    Private Function InsertTransferLog(ByVal Qid As String, ByVal dt As DataTable) As Boolean
        Dim ret As Boolean = False
        Dim db As New DBClass
        Dim ip As String = Engine.Common.FunctionEng.GetIPAddress
        Dim sql As String = "INSERT INTO [TB_QM_TRANSFER_LOG]"
        sql += "([id]"
        sql += " ,[create_by]"
        sql += ",[create_date]"
        sql += ",[service_date]"
        sql += ",[transfer_date]"
        sql += ",[tb_counter_queue_id]"
        sql += ",[queue_no]"
        sql += ",[mobile_no]"
        sql += ",[item_id]"
        sql += ",[item_name_en]"
        sql += ",[counter_id]"
        sql += ",[counter_name]"
        sql += ",[status],[ip_address])"
        sql += "VALUES"
        sql += "(isnull((select max(id) +1 from TB_QM_TRANSFER_LOG),1)"
        sql += ",'QMTransferLog'"
        sql += ",GETDATE()"
        sql += ",'" & Convert.ToDateTime(dt.Rows(0).Item("Service_Date")).ToString("yyyy-MM-dd hh:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
        sql += ",GETDATE()"
        sql += ",'" & Qid & "'"
        sql += ",'" & dt.Rows(0).Item("queue_no").ToString & "'"
        sql += ",'" & dt.Rows(0).Item("customer_id").ToString & "'"
        sql += " ,'" & dt.Rows(0).Item("item_id").ToString & "'"
        sql += ",'" & dt.Rows(0).Item("item_name_en").ToString & "'"
        sql += ",'" & dt.Rows(0).Item("counter_id").ToString & "'"
        sql += ",'" & dt.Rows(0).Item("counter_name").ToString & "'"
        sql += ",'4','" & ip & "')"
        ret = (db.SqlExecute(sql) > 0)
        db = Nothing

        Return ret
    End Function



End Class
