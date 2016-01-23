Imports QueueSharp.Org.Mentalis.Files

Public Class frmEndByService
    Dim mm As Int32 = 0
    Dim ss As Int32 = 0
    Dim TimeStamp As Int32 = 0
    Dim ServiceTime As Int32 = 0
    Dim clickX As Integer = 0
    Dim clickY As Integer = 0
    Public QueueNo As String
    Public CustomerID As String
    Dim dt_Button As New DataTable
    Dim tmp0 = frmDialogCustomerInfo.Width
    Dim dt_customerProfile As New DataTable
    Dim ServiceEnd As String = ""
    Dim isShown As Boolean = False
    Dim isSmall As Boolean = False
    Dim AgentAddService As Boolean = False
    Dim HeightNow As Int32 = 0
    Dim Filefrist As Boolean = True
    Dim ClickEndTime As DateTime
    Dim ChkDiffTimeCloseQM As Boolean = False


    Private Sub frmEndByService_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        StartTime = New DateTime(1, 1, 1)
        frmMain.Show()
    End Sub

    Private Sub frmEndByService_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Me.Left = ini.ReadInteger("E_LEFT")
        Me.Top = ini.ReadInteger("E_TOP")
        lblQueue.Text = QueueNo
        lblCustomerID.Text = CustomerID
        ClickEndTime = FindDateNow()
    End Sub

    

    Private Function DeleteQmTransferLog(ByVal Qid As String) As Boolean
        Dim sql As String = "delete from TB_QM_TRANSFER_LOG"
        sql += " where tb_counter_queue_id='" & Qid & "' and datediff(d,getdate(),service_date)=0"
        Return executeSQL(sql)
    End Function

    

    Sub ShowButton()
        Dim sql As String = ""
        Dim btn As New Button
        Dim lbl = New Label

        sql = "select item_id,item_name,1 as active,item_wait,item_time,item_order,TB_COUNTER_QUEUE.id,TB_COUNTER_QUEUE.start_time from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 2 and item_id = " & myUser.item_id & "" & vbCrLf
        sql &= "union all" & vbCrLf
        sql &= "select item_id,item_name,2 as active,item_wait,item_time,item_order,TB_COUNTER_QUEUE.id,TB_COUNTER_QUEUE.start_time  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 2 and item_id in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = " & myUser.user_id & ") and item_id <> " & myUser.item_id & " " & vbCrLf
        sql &= "union all" & vbCrLf
        sql &= "select item_id,item_name,3 as active,item_wait,item_time,item_order,TB_COUNTER_QUEUE.id,TB_COUNTER_QUEUE.start_time  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 2 and item_id not in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = " & myUser.user_id & ")" & vbCrLf
        sql &= "order by active asc,item_wait,item_time,item_order"

        dt_Button = getDataTable(sql)
        If dt_Button.Rows.Count > 0 Then
            Dim i As Int32 = 0
            For i = 0 To dt_Button.Rows.Count - 1
                Dim Active As Boolean = True
                If dt_Button.Rows(i).Item("active").ToString = "3" Then
                    Active = False
                Else

                End If
                btn = New Button
                AddBtn(btn, "txt_" & dt_Button.Rows(i).Item("item_id").ToString, dt_Button.Rows(i).Item("item_name").ToString, Active)
                btn = New Button
                AddBtn(btn, dt_Button.Rows(i).Item("item_id").ToString, "End", Active)
                Me.Height = Me.Height + 46
                QM.InsertQmTransferLog(7, dt_Button.Rows(i).Item("id").ToString)
            Next

            If myUser.item_id > 0 Then
                'ถ้าไม่ใช่คิวจอง ก็สามารถ Transfer กับ Add Service ได้
                If dt_Button.Rows.Count > 1 Then
                    btn = New Button
                    AddBtn(btn, "Transfer", "Transfer Queue")
                    Me.Height = Me.Height + 46
                End If
                btn = New Button
                ''******* POC รอบ2 ไม่ต้องแสดง ********
                If AgentAddService = True Then
                    AddBtn(btn, "Add", "Add Services", False)
                Else
                    AddBtn(btn, "Add", "Add Services")
                End If
                Me.Height = Me.Height + 46
                ''**********************************
            End If

            btn = New Button
            AddBtn(btn, "Cancel", "Cancel Services")
            Me.Height = Me.Height + 46

            StartTime = Convert.ToDateTime(dt_Button.Rows(0)("start_time"))
        End If
    End Sub

    Sub AddLabel(ByVal lbl As Label, ByVal id As Int32, ByVal name As String)
        Dim Font As Font = New Font("Angsana", 14, FontStyle.Bold, GraphicsUnit.Pixel)
        With lbl
            .Width = 190
            .Height = 40
            .Name = id
            .Text = name
            .Font = Font
            .AutoSize = False
            .FlatStyle = FlatStyle.Flat
            .BackColor = Color.LightSeaGreen
            .ForeColor = Color.White
        End With
        FLP.Controls.Add(lbl)
    End Sub

    Sub AddBtn(ByVal btn As Button, ByVal btn_id As String, ByVal Text As String, Optional ByVal Active As Boolean = True)
        Dim Font As Font = New Font("Angsana", 14, FontStyle.Bold, GraphicsUnit.Pixel)
        With btn
            .Width = 248
            .Height = 40
            .Name = btn_id
            .Text = Text
            .Font = Font
            .AutoSize = True
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 1
            If StringFromLeft(btn_id, 3) = "txt" Then
                If Active Then
                    .BackColor = Color.LightSeaGreen
                Else
                    .BackColor = Color.LightGray
                End If
                .ForeColor = Color.White
                .FlatAppearance.BorderColor = Color.White
                .Width = 170
                .Enabled = False
            Else
                Select Case btn_id.Trim
                    Case "Transfer"
                        .BackColor = Color.Orange
                        .ForeColor = Color.White
                        .FlatAppearance.BorderColor = Color.White
                    Case "Cancel"
                        .BackColor = Color.Red
                        .ForeColor = Color.White
                        .FlatAppearance.BorderColor = Color.White
                    Case "Add"
                        If Active Then
                            .BackColor = Color.RoyalBlue
                            .ForeColor = Color.White
                            .FlatAppearance.BorderColor = Color.White
                        Else
                            .BackColor = Color.LightGray
                            .ForeColor = Color.Black
                            .FlatAppearance.BorderColor = Color.White
                            .Enabled = False
                        End If
                    Case Else
                        If Active Then
                            .BackColor = Color.ForestGreen
                        Else
                            .BackColor = Color.LightGray
                            .Enabled = False
                            .Text = "None"
                        End If
                        .ForeColor = Color.White
                        .FlatAppearance.BorderColor = Color.White
                        .Width = 72
                End Select
            End If
        End With
        FLP.Controls.Add(btn)
        AddHandler btn.Click, AddressOf CheckEndService
    End Sub

    Private Sub CheckEndService(ByVal Sender As Object, ByVal e As EventArgs)

        Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
        Dim dDateNow As DateTime = FindDateNow()
        Dim vDateNow As String = FixDateTime(dDateNow)

        Dim btn As Button = Sender
        Select Case btn.Name
            Case "Transfer" 'Tranfer
                Dim num As Int32 = 0
                For i = 0 To dt_Button.Rows.Count - 1
                    For j As Int32 = 0 To FLP.Controls.Count - 1
                        If FLP.Controls(j).Name = dt_Button.Rows(i).Item("item_id").ToString Then
                            If FLP.Controls(j).Text <> "End" And FLP.Controls(j).Text <> "None" Then
                                num = num + 1
                            End If
                        End If
                    Next
                Next

                If num = 0 Then
                    showNotify("Attention", "Cannot Transfer Queue")
                    Exit Sub
                End If

                If ServiceEnd.Trim <> "" Then
                    Dim sql As String = ""
                    sql = "update tb_counter_queue "
                    sql += " set combo_item_end = '" & ServiceEnd & "' "
                    sql += " where datediff(d,getdate(),service_date)=0 and item_id in (" & ServiceEnd & ")"
                    executeSQL(sql)
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If
                End If
                InsertLog(lblQueue.Text, lblCustomerID.Text, myUser.user_id, 0, myUser.counter_id, 1, "Transfer", Nothing, vDateNow, CurrDB)

                'ถ้า Transfer
                ExitForm(True, CurrDB)
                Threading.Thread.Sleep(3000)
                QM.WriteTextQueueID("")
                QM.CloseQM()
            Case "Cancel" 'Cancel Service
                Dim f As New frmServiceCancel
                f.QueueNo = lblQueue.Text
                f.CustomerID = lblCustomerID.Text
                f.StatusCustomer = 2
                f.Flag = "frmEndByService"
                f.CountEnd = 2

                Dim num As Int32 = 0
                For i = 0 To dt_Button.Rows.Count - 1
                    For j As Int32 = 0 To FLP.Controls.Count - 1
                        If FLP.Controls(j).Name = dt_Button.Rows(i).Item("item_id").ToString Then
                            If FLP.Controls(j).Text <> "End" And FLP.Controls(j).Text <> "None" Then
                                num = num + 1
                            End If
                        End If
                    Next
                Next
                If num = 0 Then
                    f.FristEnd = True
                End If

                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    'Hold
                    ExitForm(False, CurrDB)
                ElseIf f.DialogResult = Windows.Forms.DialogResult.Yes Then
                    'Cancel
                    Dim sql As String = ""
                    Dim dt As New DataTable
                    sql = "select id,item_id,status from TB_COUNTER_QUEUE where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 5 order by status"
                    dt = getDataTable(sql)
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        Try
                            FLP.Controls("txt_" & dt.Rows(i).Item("item_id").ToString).BackColor = Color.Gray
                            FLP.Controls(dt.Rows(i).Item("item_id").ToString).BackColor = Color.Gray
                            FLP.Controls(dt.Rows(i).Item("item_id").ToString).ForeColor = Color.White
                            FLP.Controls(dt.Rows(i).Item("item_id").ToString).Enabled = False
                            FLP.Controls(dt.Rows(i).Item("item_id").ToString).Text = "Cancel"

                            DeleteQmTransferLog(dt.Rows(i).Item("id").ToString)
                        Catch ex As Exception : End Try
                    Next

                    ''Update assign_time ของ Service ที่เหลือ
                    'sql = "declare @EndTime as datetime;select @EndTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "')" & vbCrLf
                    'sql &= "update tb_counter_queue set assign_time = @EndTime,call_time = @EndTime,start_time = @EndTime where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 2"

                    'Update assign_time ของ Service ที่เหลือ โดยการเอาเวลาปัจจุบันไป Stamp
                    '  (จาก Defect ของ QSharp ข้อที่ 13 กับ 14 วันที่ 21/03/2013-29/03/2013)
                    'sql = "declare @EndTime as datetime;select @EndTime = (select getdate())" & vbCrLf
                    Dim EndTime As String = FindDateNow().ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))
                    sql = " update tb_counter_queue "
                    sql += " set assign_time = '" & EndTime & "',call_time = '" & EndTime & "',start_time = '" & EndTime & "' "
                    sql += " where datediff(d,getdate(),service_date)=0 "
                    sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 2"
                    executeSQL(sql)
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If

                    If CheckCloseFrom() Then
                        'ถ้า Cancel ทุก Service
                        QM.WriteTextQueueID("")
                        QM.CloseQM()
                        ExitForm(True, CurrDB)
                    End If
                End If
            Case "Add"
                Dim f As New frmAddServiceCustomer
                f.QueueNo = QueueNo
                f.Mobile = lblCustomerID.Text
                If f.ShowDialog = Windows.Forms.DialogResult.Yes Then
                    AgentAddService = True
                    If myUser.item_id > 0 Then
                        FLP.Controls.RemoveByKey("Transfer")
                        FLP.Controls.RemoveByKey("Add")
                    End If
                    FLP.Controls.RemoveByKey("Cancel")

                    Dim New_btn As Button
                    Dim sql As String = ""
                    Dim dt_add As New DataTable
                    sql = "select top 1 tb_counter_queue.item_id,item_name,isnull(SCH.item_id,0) as active "
                    sql += " from tb_counter_queue "
                    sql += " left join TB_ITEM on tb_counter_queue.item_id = TB_ITEM.id "
                    sql += " left join (select item_id "
                    sql += "            from TB_USER_SERVICE_SCHEDULE "
                    sql += "            where DATEDIFF(D,GETDATE(),service_date) = 0 "
                    sql += "            and active_status = 1 and user_id = " & myUser.user_id & ") as SCH on tb_counter_queue.item_id = SCH.item_id "
                    sql += " where DATEDIFF(D,GETDATE(),service_date)=0 "
                    sql += " and queue_no = '" & FixDB(QueueNo) & "' "
                    sql += " and customer_id = '" & FixDB(lblCustomerID.Text) & "' "
                    sql += " order by tb_counter_queue.id desc"
                    dt_add = getDataTable(sql)
                    If dt_add.Rows.Count > 0 Then
                        New_btn = New Button
                        Dim Active As Boolean = False
                        If dt_add.Rows(0).Item("active") > 0 Then
                            Active = True
                        End If
                        AddBtn(New_btn, "txt_" & dt_add.Rows(0).Item("item_id").ToString, dt_add.Rows(0).Item("item_name").ToString, Active)
                        New_btn = New Button

                        AddBtn(btn, dt_add.Rows(0).Item("item_id").ToString, "End", Active)
                        Me.Height = Me.Height + 46
                    End If

                    If myUser.item_id > 0 Then
                        New_btn = New Button
                        AddBtn(New_btn, "Transfer", "Transfer Queue")
                        ''******* POC รอบ2 ไม่ต้องแสดง ********
                        New_btn = New Button
                        AddBtn(New_btn, "Add", "Add Services", False)
                        ''**********************************
                        If dt_Button.Rows.Count = 1 Then
                            Me.Height = Me.Height + 46
                        End If
                    End If

                    New_btn = New Button
                    AddBtn(New_btn, "Cancel", "Cancel Services")
                    CheckService()
                End If
            Case Else
                If btn.BackColor = Color.ForestGreen Then
                    'End Service
                    '######################################
                    '######### กรณีกด End พร้อมกันหลาย Service ภายในเวลาที่กำหนด
                    '######### จะไม่ทำการอัด VDO ใน Service นั้น
                    '######### และจะไม่กด End ในคิวถัดไปได้
                    Dim cfgClickEndSec As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "QMClickEngSec", "QueueSharp", "frmEndByService_CheckEndService")
                    If cfgClickEndSec.Trim = "" Then
                        cfgClickEndSec = "3"
                    End If

                    ChkDiffTimeCloseQM = (DateDiff(DateInterval.Second, ClickEndTime, dDateNow) >= Convert.ToInt16(cfgClickEndSec))
                    If ChkDiffTimeCloseQM = False Then
                        Exit Sub
                    End If

                    'Disable Transfer Button
                    Dim tBtn As Button
                    Dim IsTransfer As Boolean = False
                    For i As Integer = 0 To FLP.Controls.Count - 1
                        If FLP.Controls(i).Name = "Transfer" Then
                            tBtn = FLP.Controls(i)
                            tBtn.Enabled = False
                            IsTransfer = True
                            'Exit For
                        End If
                        If FLP.Controls(i).Text = "End" Then
                            FLP.Controls(i).Enabled = False
                        End If
                    Next

                    Dim sql As String = ""
                    If TimeStamp = 0 Then
                        If Filefrist = True Then
                            btn.Text = lblTime.Text
                            TimeStamp = (mm * 60) + ss
                            Filefrist = False
                        Else
                            TimeStamp = (((mm * 60) + ss) - ServiceTime)
                            btn.Text = (TimeStamp \ 60).ToString.PadLeft(2, "0") & ":" & (TimeStamp Mod 60).ToString.PadLeft(2, "0")
                        End If
                    Else
                        'หาเวลา Start Time
                        TimeStamp = (((mm * 60) + ss) - ServiceTime)
                        btn.Text = (TimeStamp \ 60).ToString.PadLeft(2, "0") & ":" & (TimeStamp Mod 60).ToString.PadLeft(2, "0")
                    End If
                    ServiceTime = ServiceTime + TimeStamp

                    sql = "update TB_COUNTER_QUEUE "
                    'sql += " set status = 3,end_time = dateadd(SECOND," & TimeStamp & ",start_time),"
                    sql += " set status = 3,end_time = " & vDateNow & ","
                    sql += " [user_id] = " & myUser.user_id
                    sql += " where datediff(D,GETDATE(),service_date)=0 "
                    sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' "
                    sql += " and counter_id='" & myUser.counter_id & "' and status = 2 and item_id = " & btn.Name
                    executeSQL(sql)
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If

                    'Dim vDateNow As String = FixDateTime(FindDateNow)
                    InsertLog(QueueNo, CustomerID, myUser.user_id, btn.Name, myUser.counter_id, 3, "", Nothing, vDateNow, CurrDB)
                    sql = "declare @EndTime as datetime;select @EndTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "')" & vbCrLf
                    sql += " update tb_counter_queue "
                    sql += " set assign_time = @EndTime,call_time = @EndTime,start_time = @EndTime "
                    sql += " where datediff(d,getdate(),service_date)=0 "
                    sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' "
                    sql += " and counter_id = '" & myUser.counter_id & "' and status = 2"
                    executeSQL(sql)
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If

                    btn.BackColor = Color.Gray
                    btn.Enabled = False
                    FLP.Controls("txt_" & btn.Name).BackColor = Color.Gray


                    sql = "select id, customertype_id,datediff(s,start_time,getdate()) diff_time from tb_counter_queue where datediff(d,getdate(),service_date) = 0 and customer_id = '" & FixDB(CustomerID) & "' and queue_no = '" & FixDB(QueueNo) & "' and item_id = " & btn.Name
                    Dim dt As New DataTable
                    dt = getDataTable(sql)
                    If dt.Rows.Count > 0 Then
                        If ChkDiffTimeCloseQM = True Then
                            QM.WriteTextQueueID(dt.Rows(0).Item("id").ToString)
                            QM.CloseQM()
                            'Threading.Thread.Sleep(Convert.ToInt16(cfgClickEndSec) * 1000)
                        End If
                        ClickEndTime = dDateNow
                        '#############################


                        If ServiceEnd = "" Then
                            ServiceEnd = btn.Name
                        Else
                            ServiceEnd = ServiceEnd & "," & btn.Name
                        End If

                        If Convert.IsDBNull(dt.Rows(0)("customertype_id")) = False Then
                            If dt.Rows(0)("customertype_id") = "3" Then
                                If InStr(ServiceEnd, ",") = 0 Then
                                    'คืน Slot เมื่อมีการ End Service แรก
                                    UpdateAppointmentSlot(CustomerID, QueueNo)
                                End If


                                sql = "select appointment_job_id,customer_id,queue_no "
                                sql += " from TB_APPOINTMENT_CUSTOMER "
                                sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                                sql += " and customer_id = '" & FixDB(CustomerID) & "' "
                                sql += " and queue_no='" & FixDB(QueueNo) & "' "
                                Dim aDt As New DataTable
                                aDt = getDataTable(sql)
                                If aDt.Rows.Count > 0 Then
                                    'Update Appointment Job To DC
                                    UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "3")

                                    'Update Siebel when End Queue
                                    Dim dtS As DataTable = GetSiebelData(CustomerID, "2", "Registered")
                                    If dtS.Rows.Count > 0 Then
                                        Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                                        Dim EndDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                                        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                                        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                                          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                                          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                                          sslerror As System.Net.Security.SslPolicyErrors) True

                                        Try
                                            Dim ws As New ShopWebServiceMain.WebServiceAPI
                                            ws.Url = GetWebServiceURL()
                                            ws.SiebelUpdateToComplete(CustomerID, StartSlot, EndDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                                            ws = Nothing
                                        Catch ex As Exception
                                            Dim sqlLog As String = "insert into tb_error_log "
                                            sqlLog += " values(" & FindID("tb_error_log") & ", 1, getdate(),null,null,"
                                            sqlLog += " getdate(), 'QueueSharp.frmEndByService.CheckEndService : Don''t Update Activity to Complete Mobile No. " & FixDB(CustomerID) & ex.Message & "', null, '" & Engine.Common.FunctionEng.GetIPAddress & "', '" & getMyVersion() & "', newid())"
                                            executeSQL(sqlLog)

                                            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                                            ws.Url = GetWebServiceURL(False)
                                            ws.SiebelUpdateToComplete(CustomerID, StartSlot, EndDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                                            ws = Nothing
                                        End Try
                                    End If
                                End If
                                aDt.Dispose()
                            End If
                        End If
                    End If
                    dt.Dispose()

                    If CheckCloseFrom() Then
                        If ChkDiffTimeCloseQM = False Then
                            QM.WriteTextQueueID("")
                            QM.CloseQM()
                        End If

                        ExitForm(True, CurrDB)
                    Else
                        Threading.Thread.Sleep(1000)
                        QM.WriteTextQueueNo(DateTime.Now.ToString("yyyyMMddHHmmss") & "-" & QueueNo)
                        QM.StartQM(QueueNo)
                        If IsTransfer = True Then
                            tBtn.Enabled = True
                        End If

                        For i As Integer = 0 To FLP.Controls.Count - 1
                            If FLP.Controls(i).Text = "End" Then
                                FLP.Controls(i).Enabled = True
                            End If
                        Next
                    End If
                End If
        End Select
    End Sub

    Function FindServiceTime(ByVal Parameter As String)
        Dim ret(1) As String
        For i As Int32 = 0 To FLP.Controls.Count - 1
            If CInt(FLP.Controls(i).Name) > 0 And FLP.Controls(i).Text <> "End" Then
                Select Case Parameter
                    Case "Min"
                        If ret(0) = "" Then
                            ret(0) = FLP.Controls(i).Text
                            ret(1) = FLP.Controls(i).Name
                        Else
                            If CDate(ret(0)) < FLP.Controls(i).Text Then
                                ret(0) = FLP.Controls(i).Text
                                ret(1) = FLP.Controls(i).Name
                            End If
                        End If
                    Case "Max"
                        If ret(0) = "" Then
                            ret(0) = FLP.Controls(i).Text
                            ret(1) = FLP.Controls(i).Name
                        Else
                            If CDate(ret(0)) > FLP.Controls(i).Text Then
                                ret(0) = FLP.Controls(i).Text
                                ret(1) = FLP.Controls(i).Name
                            End If
                        End If
                End Select
            End If
        Next
        Return ret
    End Function

    Private Sub frmFloat_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, Gp.MouseMove, FLP.MouseMove, PanelCus.MouseMove, lblTime.MouseMove, lblTimeS.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left = Me.Left + (e.X - clickX)
            Me.Top = Me.Top + (e.Y - clickY)
        End If
    End Sub

    Private Sub frmFloat_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, Gp.MouseUp, FLP.MouseUp, PanelCus.MouseUp, lblTime.MouseUp, lblTimeS.MouseUp
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("E_LEFT", Me.Left)
        ini.Write("E_TOP", Me.Top)
    End Sub

    Private Sub frmFloat_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, Gp.MouseDown, FLP.MouseDown, PanelCus.MouseDown, lblTime.MouseDown, lblTimeS.MouseDown
        clickX = e.X
        clickY = e.Y
    End Sub

    Private Sub Down_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.MouseHover, Gp.MouseHover, FLP.MouseHover, PanelCus.MouseHover, lblTime.MouseHover, lblTimeS.MouseHover
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Me.Left = ini.ReadInteger("E_LEFT")
        Me.Top = ini.ReadInteger("E_TOP")
    End Sub

    Private Sub TimerCountServed_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCountServed.Tick
        'ss = ss + 1
        'If ss = 60 Then
        '    mm = mm + 1
        '    ss = 0
        'End If
        'lblTime.Text = CStr(mm).PadLeft(2, "0") & ":" & CStr(ss).PadLeft(2, "0")
        'lblTimeS.Text = CStr(mm).PadLeft(2, "0") & ":" & CStr(ss).PadLeft(2, "0")

        SetViewTime()
    End Sub

    Sub ExitForm(ByVal ShowMessage As Boolean, ByVal CurrDB As String)
        Dim sql As String = ""
        Dim dt As New DataTable
        Me.Close()

        If CreateTransaction("frmEndByService_ExitForm") = True Then
            Dim ret As Boolean = False

            'Update Status ของ Service ที่ไม่ได้ทำกับ User คนนี้
            sql = "declare @AssignTime as datetime;select @AssignTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "')" & vbCrLf
            sql &= "update TB_COUNTER_QUEUE set status = 1,assign_time = @AssignTime,start_time = NULL,end_time = NULL,[user_id] = 0,flag = '' where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 2"
            executeSQL(sql, shTrans, True)
            'If CurrDB = "MAIN" Then
            '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
            'End If

            sql = "update TB_COUNTER_QUEUE set flag = '' where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "'"
            ret = executeSQL(sql, shTrans, True)
            If ret = True Then
                'If CurrDB = "MAIN" Then
                '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                'End If

                ClearUnitDisplay(False, shTrans)
                ClearMainDisplay(myUser.counter_id, shTrans)
                CommitTransaction()
            Else
                RollbackTransaction()
            End If
            'QM.CloseQM()
            AgentAddService = False
        End If
    End Sub

    Function CheckCloseFrom() As Boolean
        Dim num As Int32 = 0
        For i = 0 To dt_Button.Rows.Count - 1
            For j As Int32 = 0 To FLP.Controls.Count - 1
                If FLP.Controls(j).Text = "End" Then
                    num = num + 1
                End If
            Next
        Next

        If num = 0 Then
            'คืน Slot
            'UpdateAppointmentSlot(CustomerID, QueueNo)

            'Register แล้ว Call > Confirm > End Queue
            Dim Sql As String = "update TB_APPOINTMENT_CUSTOMER "
            Sql += " set active_status = 3 "
            Sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
            Sql += " and customer_id = '" & FixDB(CustomerID) & "' and queue_no='" & QueueNo & "' "
            executeSQL(Sql)
            Return True
        End If
        Return False
    End Function

#Region "EndAllService"
    'Dim sql As String = ""
    'Dim dt As New DataTable
    'Dim dt_temp As New DataTable
    'Dim dt_ChoseItem As New DataTable
    'Dim dr As DataRow
    'With dt_ChoseItem
    '    .Columns.Add("item_id", GetType(System.String))
    '    .Columns.Add("avg_time", GetType(System.String))
    'End With

    ''หาค่า KPI ของแต่ละ Service
    'sql = "select tb1.user_id,tb1.item_id,item_name,isnull(avg_time,item_time * 60) as avg_time  from (select TB_USER.id as user_id,TB_ITEM.id as item_id,item_name,item_time from TB_USER cross join TB_ITEM where TB_USER.id = " & myUser.user_id & " and TB_ITEM.active_status = 1) as TB1 left join TB_AVG_USER_SERVICETIME as TB2 on TB1.user_id = tb2.user_id and tb1.item_id = tb2.item_id"
    'dt = getDataTable(sql)
    'If dt.Rows.Count > 0 Then
    '    Dim TotalAvgTime As Int32 = 0
    '    Dim TotalTime As Int32 = 0 'เวลาที่ใช้ในการให้บริการ
    '    TotalTime = (mm * 60) + ss
    '    For i = 0 To dt_Button.Rows.Count - 1
    '        For j As Int32 = 0 To FLP.Controls.Count - 1
    '            If FLP.Controls(j).Name.ToString = dt_Button.Rows(i).Item("item_id").ToString Then
    '                If FLP.Controls(j).Enabled = True Then
    '                    dt.DefaultView.RowFilter = "item_id = " & FLP.Controls(j).Name
    '                    dt_temp = dt.DefaultView.ToTable
    '                    dr = dt_ChoseItem.NewRow
    '                    dr("item_id") = dt_temp.Rows(0).Item("item_id").ToString
    '                    dr("avg_time") = dt_temp.Rows(0).Item("avg_time").ToString
    '                    dt_ChoseItem.Rows.Add(dr)
    '                    TotalAvgTime = TotalAvgTime + dt_temp.Rows(0).Item("avg_time")
    '                End If
    '            End If
    '        Next
    '    Next

    '    Dim AvgTime As Int32 = 0
    '    For x As Int32 = 0 To dt_ChoseItem.Rows.Count - 1
    '        AvgTime = AvgTime + (dt_ChoseItem.Rows(x).Item("avg_time") / TotalAvgTime) * TotalTime
    '        If x = 0 Then
    '            sql = "update TB_COUNTER_QUEUE set status = 3,end_time = DATEADD(ss," & AvgTime & ",start_time),[user_id] = " & myUser.user_id & " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & dt_ChoseItem.Rows(x).Item("item_id") & " and status = 2"
    '        Else
    '            sql = "declare @StartTime as datetime;select @StartTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 3);" & vbCrLf
    '            sql &= "update TB_COUNTER_QUEUE set status = 3,start_time = @StartTime,end_time = DATEADD(ss," & AvgTime & ",start_time),[user_id] = " & myUser.user_id & " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & dt_ChoseItem.Rows(x).Item("item_id") & " and status = 2"
    '        End If
    '        executeSQL(sql)
    '        InsertLog(FixDB(QueueNo), FixDB(CustomerID), myUser.user_id, dt_ChoseItem.Rows(x).Item("item_id"), myUser.counter_id, 3)
    '    Next
    'End If
#End Region

    Private Sub frmEndByService_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Height = 135
        FLP.Controls.Clear()
        ShowButton()
        Dim sql As String = ""
        Try
            QM.WriteTextQueueID("")
            QM.WriteTextQueueNo(DateTime.Now.ToString("yyyyMMddHHmmss") & "-" & QueueNo)
            QM.StartQM(QueueNo)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        SetViewTime()
        TimerCountServed.Enabled = True
    End Sub

    Public StartTime As New DateTime(1, 1, 1)
    Private Sub SetViewTime()
        'Dim dt_time As New DataTable
        'Dim Sql = "select start_time, datediff(second,start_time,getdate()) curr_time from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and status = 2 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "'"
        'dt_time = getDataTable(Sql)
        'If dt_time.Rows.Count > 0 Then
        '    'If StartTime.Year = 1 Then
        '    '    StartTime = Convert.ToDateTime(dt_time.Rows(0)("start_time"))
        '    'End If

        '    Dim UseTime As String = GetTimeFormat(dt_time.Rows(0)("curr_time"))
        '    mm = Split(UseTime, ":")(0) 'dt_time.Rows(0).Item("mm").ToString
        '    ss = Split(UseTime, ":")(1) 'dt_time.Rows(0).Item("ss").ToString
        '    dt_time.Dispose()
        'End If

        If StartTime.Year <> 1 Then
            Dim CurrTime As Integer = DateDiff(DateInterval.Second, StartTime, FindDateNow())
            Dim UseTime As String = GetTimeFormat(CurrTime)
            mm = Split(UseTime, ":")(0)
            ss = Split(UseTime, ":")(1)
        End If
        lblTime.Text = CStr(mm).PadLeft(2, "0") & ":" & CStr(ss).PadLeft(2, "0")
        lblTimeS.Text = CStr(mm).PadLeft(2, "0") & ":" & CStr(ss).PadLeft(2, "0")
    End Sub

    Private Function GetTimeFormat(ByVal TimeSec As Long) As String
        'แปลงเวลาจากวินาทีไปเป็น mm:ss
        Dim tMin As Integer = 0
        Dim tSec As Integer = 0

        tMin = Math.Floor(TimeSec / 60)
        tSec = TimeSec Mod 60
        Return tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
    End Function

    Private Sub frmEndByService_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        frmDialogCustomerInfo.Top = Me.Top + 5
        frmDialogCustomerInfo.Left = Me.Left - frmDialogCustomerInfo.Width + 2
    End Sub

    Private Sub btnCusInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCusInfo.Click
        If isShown = False Then
            frmDialogCustomerInfo.Visible = True
            btnCusInfo.Enabled = False
            Dim sql As String = ""
            Dim dt As New DataTable
            frmDialogCustomerInfo.lblCustomerID.Text = CustomerID
            frmDialogCustomerInfo.lblQ.Text = QueueNo

            sql = "exec SP_FindCustomerInfo '" & CustomerID.Replace("-", "") & "'"
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                frmDialogCustomerInfo.lblName.Text = dt.Rows(0).Item("Name").ToString
                frmDialogCustomerInfo.lblEmail.Text = dt.Rows(0).Item("Email").ToString
                frmDialogCustomerInfo.lblMobile_status.Text = dt.Rows(0).Item("Mobile_status").ToString
                frmDialogCustomerInfo.lblBirth_date.Text = dt.Rows(0).Item("Birth_date").ToString
                frmDialogCustomerInfo.lblCategory.Text = dt.Rows(0).Item("Category").ToString
                frmDialogCustomerInfo.lblAcc.Text = dt.Rows(0).Item("Acc").ToString
                frmDialogCustomerInfo.lblCon_class.Text = dt.Rows(0).Item("Con_class").ToString
                frmDialogCustomerInfo.lblService_year.Text = dt.Rows(0).Item("Service_year").ToString
                frmDialogCustomerInfo.lblChurn.Text = dt.Rows(0).Item("Churn").ToString
                frmDialogCustomerInfo.lblNetwork_type.Text = dt.Rows(0).Item("Network_type").ToString
                frmDialogCustomerInfo.lblSegment.Text = dt.Rows(0).Item("Segment_Level").ToString
                frmDialogCustomerInfo.lblPre_lang.Text = dt.Rows(0).Item("Pre_lang").ToString
                If InStr(dt.Rows(0).Item("Pre_lang").ToString.ToUpper, "THAI") > 0 Then
                    If dt.Rows(0).Item("Camp_name").ToString.Trim = "" And dt.Rows(0).Item("campaign_desc_th2").ToString.Trim = "" Then
                        frmDialogCustomerInfo.lblCamp_code.Text = ""
                        frmDialogCustomerInfo.lblCamp_name.Text = ""
                        frmDialogCustomerInfo.lblCamp_name1.Text = ""
                    Else
                        frmDialogCustomerInfo.lblCamp_code.Text = dt.Rows(0).Item("Camp_code").ToString
                        frmDialogCustomerInfo.lblCamp_name.Text = dt.Rows(0).Item("Camp_name").ToString
                        frmDialogCustomerInfo.lblCamp_name1.Text = dt.Rows(0).Item("campaign_desc_th2").ToString
                    End If
                Else
                    If dt.Rows(0).Item("campaign_desc").ToString.Trim = "" And dt.Rows(0).Item("campaign_desc_en2").ToString.Trim = "" Then
                        frmDialogCustomerInfo.lblCamp_code.Text = ""
                        frmDialogCustomerInfo.lblCamp_name.Text = ""
                        frmDialogCustomerInfo.lblCamp_name1.Text = ""
                    Else
                        frmDialogCustomerInfo.lblCamp_code.Text = dt.Rows(0).Item("campaign_name_en").ToString
                        frmDialogCustomerInfo.lblCamp_name.Text = dt.Rows(0).Item("campaign_desc").ToString
                        frmDialogCustomerInfo.lblCamp_name1.Text = dt.Rows(0).Item("campaign_desc_en2").ToString
                    End If
                End If

            Else
                frmDialogCustomerInfo.lblName.Text = "-"
                frmDialogCustomerInfo.lblEmail.Text = "-"
                frmDialogCustomerInfo.lblMobile_status.Text = "-"
                frmDialogCustomerInfo.lblBirth_date.Text = "-"
                frmDialogCustomerInfo.lblCategory.Text = "-"
                frmDialogCustomerInfo.lblAcc.Text = "-"
                frmDialogCustomerInfo.lblCon_class.Text = "-"
                frmDialogCustomerInfo.lblService_year.Text = "-"
                frmDialogCustomerInfo.lblChurn.Text = "-"
                frmDialogCustomerInfo.lblCamp_code.Text = "-"
                frmDialogCustomerInfo.lblCamp_name.Text = "-"
                frmDialogCustomerInfo.lblPre_lang.Text = "-"
                frmDialogCustomerInfo.lblNetwork_type.Text = "-"
                frmDialogCustomerInfo.lblSegment.Text = "-"
                frmDialogCustomerInfo.lblCamp_name1.Text = "-"
            End If
            
            frmDialogCustomerInfo.Show()
            frmDialogCustomerInfo.Width = 14
            frmDialogCustomerInfo.Left = Me.Left - 14 + 2
            frmDialogCustomerInfo.Top = Me.Top + 5
            Me.Activate()
            Dim tmp = 284
            frmDialogCustomerInfo.Width = 1
            frmDialogCustomerInfo.Owner = Me
            frmDialogCustomerInfo.Top = Me.Top + 5
            frmDialogCustomerInfo.Left = Me.Left - frmDialogCustomerInfo.Width + 2
            For i As Integer = 1 To tmp Step 5
                frmDialogCustomerInfo.Left -= 5
                frmDialogCustomerInfo.Width += 5
                Application.DoEvents()
            Next
            frmDialogCustomerInfo.Width = tmp
            btnCusInfo.Enabled = True
            isShown = True
            dt.Dispose()
        Else
            btnCusInfo.Enabled = False
            Me.TopMost = True
            Dim tmp As Integer = frmDialogCustomerInfo.Width
            Dim i As Integer = 0
            For i = tmp To 0 Step -1
                frmDialogCustomerInfo.Width = i
                frmDialogCustomerInfo.Left = Me.Left - frmDialogCustomerInfo.Width + 2
                Application.DoEvents()
            Next
            Me.Activate()
            frmDialogCustomerInfo.Visible = False
            btnCusInfo.Enabled = True
            isShown = False
        End If


    End Sub

    Private Sub btnTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTop.Click
        If isSmall = False Then
            HeightNow = Me.Height
            lblTimeS.Visible = True
            isSmall = True
            Me.Height = 72
        Else
            lblTimeS.Visible = False
            Me.Height = HeightNow
            isSmall = False
        End If
    End Sub

    Sub CheckService()
        Dim sql As String = ""

        sql = "select item_id,item_name,1 as active  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status in (2,3,5) and item_id = " & myUser.item_id & "" & vbCrLf
        sql &= "union all" & vbCrLf
        sql &= "select item_id,item_name,2 as active  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status in (2,3,5) and item_id in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = " & myUser.user_id & ") and item_id <> " & myUser.item_id & " " & vbCrLf
        sql &= "union all" & vbCrLf
        sql &= "select item_id,item_name,3 as active  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status in (2,3,5) and item_id not in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = " & myUser.user_id & ")" & vbCrLf
        sql &= "order by active asc"

        dt_Button = getDataTable(sql)
    End Sub

End Class
