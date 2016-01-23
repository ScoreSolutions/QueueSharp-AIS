Imports System.Windows.Forms
Imports DirectX.Capture
Imports System.Drawing

Namespace Kiosk
    Public Class KioskRegisterENG : Inherits KioskBaseENG

        Public Structure Print
            Dim QueueNo As String
            Dim Mobile As String
            Dim WaitingTime As String
            Dim ListService As String
            Dim CountQueue As String
            Dim AppTime As String
            Dim AppDateNow As DateTime
            Dim HideWaitingTime As String
            Dim PrintTicketCoupon As String
        End Structure

        Public Sub ClickNumber(ByVal txtMobile As TextBox, ByVal TxtNo As String)
            CountVDO = 0
            If txtMobile.Text.Length = k_len Then Exit Sub
            txtMobile.Text = txtMobile.Text & TxtNo
        End Sub

        Public Sub DeleteQueueIfNoQueue(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim Sql As String = "delete from tb_counter_queue where datediff(d,getdate(),service_date)=0 and queue_no = ''"
            executeSQL(Sql, INIFileName, SoftwareName, FunctionName)
        End Sub

        Public Function CheckAppointmentCustomer(ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim Sql As String = "select count(1) as rec, start_slot from TB_APPOINTMENT_CUSTOMER where DATEDIFF(D,start_slot,GETDATE()) = 0 and customer_id = '" & MobileNo & "' and active_status = 1 group by start_slot"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function CheckAppointmentSlot(ByVal MobileNo As String, ByVal Rec As Integer, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = "declare @time as datetime;select @time = (select top 1 start_slot from TB_APPOINTMENT_CUSTOMER where DATEDIFF(D,app_date,GETDATE()) = 0 and customer_id = '" & MobileNo & "' and active_status = 1 group by start_slot) select top " & Rec & " id,in_use from TB_APPOINTMENT_SLOT where DATEDIFF(D,app_date,GETDATE()) = 0 and slot_time >= @time"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function GetAppointmentJobID(ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = "select appointment_job_id "
            Sql += "from TB_APPOINTMENT_CUSTOMER "
            Sql += " where DATEDIFF(D,start_slot,GETDATE()) = 0 and customer_id = '" & MobileNo & "' "
            Sql += " and active_status = 1"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Sub UpdateStatusAppointmentCustomer(ByVal MoblieNo As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim Sql As String = "update TB_APPOINTMENT_CUSTOMER set active_status = " & NewStatus & " where datediff(d,getdate(),start_slot)=0 and customer_id = '" & MoblieNo & "' and active_status = " & OldStatus
            executeSQL(Sql, INIFileName, SoftwareName, FunctionName)
        End Sub

        Public Function GetSiebelData(ByVal MobileNo As String, ByVal AppointmentStatus As String, ByVal SiebelStatus As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            'Update Siebel Activity to Cancelled (ไม่รู้จะทำไปทำไม เสียเวลาเปล่า)
            Dim sqlS As String = "select top 1 start_slot,CONVERT(varchar(5),start_slot,114) as app_time,item_id,item_name,item_name_th,siebel_activity_id, siebel_desc "
            sqlS += " from TB_APPOINTMENT_CUSTOMER "
            sqlS += " inner join TB_ITEM on TB_APPOINTMENT_CUSTOMER.item_id = TB_ITEM.id  "
            sqlS += " where DateDiff(D, GETDATE(), start_slot) = 0 And TB_ITEM.active_status = '1' "
            sqlS += " and TB_APPOINTMENT_CUSTOMER.active_status = " & AppointmentStatus & " "
            sqlS += " and customer_id = '" & MobileNo & "' "
            sqlS += " and siebel_status = '" & SiebelStatus & "'"
            sqlS += " order by item_order,item_name"
            Dim dtS As New DataTable
            dtS = getDataTable(sqlS, INIFileName, SoftwareName, FunctionName)

            Return dtS
        End Function

        Public Function GetTodayAppointment(ByVal MobileNo As String, ByVal k_late As Integer, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql = "select DATEDIFF(MINUTE ,GETDATE(),dateadd(minute," & k_late & ",ac.start_slot)) as regis_time,"
            Sql += " CONVERT(varchar(5),ac.start_slot,114) as app_time,ac.item_id,t.item_name,t.item_name_th,ac.active_status "
            Sql += " from TB_APPOINTMENT_CUSTOMER ac "
            Sql += " inner join TB_ITEM t on ac.item_id = t.id  "
            Sql += " where DATEDIFF(D,GETDATE(),ac.start_slot)=0 and t.active_status = 1 "
            Sql += " and ac.active_status in ('1','5') and ac.customer_id = '" & MobileNo & "' "
            Sql += " order by ac.active_status,ac.start_slot desc,t.item_wait,t.item_time,t.item_order"
            Dim dt As New DataTable
            'dt = getDataTable(Sql,inifilename , SoftwareName, "frmRegister_CheckTodayAppointment")
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function GetQueueToday(ByVal StartSlot As String, ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            'ถ้าวันนี้มีการจองแล้วสถานะเป็น No Show แล้ว ให้แสดง Popup แจ้งว่าคิวได้ถูกยกเลิกแล้ว แค่ครั้งแรกเท่านั้น
            'ครั้งต่อๆ ไปในวันนี้ถ้ามีกดอีก ก็ไม่ต้องแสดง Popup นี้อีก
            Dim qSql As String = "select top 1 id"
            qSql += " from TB_COUNTER_QUEUE q "
            qSql += " where CONVERT(varchar(5),q.service_date,114)>'" & StartSlot & "'"
            qSql += " and DATEDIFF(D,GETDATE(),q.service_date)=0"
            qSql += " and customer_id = '" & MobileNo & "'"
            Dim qDt As New DataTable
            qDt = getDataTable(qSql, INIFileName, SoftwareName, FunctionName)
            Return qDt
        End Function

        Public Function GetTextQueueFromCustomerType(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = "select top 1 id,txt_queue from TB_CUSTOMERTYPE where app = 1 and active_status = 1"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function GetDataAppointmentCustomer(ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = "select right('0' + convert(varchar(5),(DATEDIFF(MINUTE ,GETDATE(),start_slot))/60),2) + ':' + right('0' + convert(varchar(5),(DATEDIFF(MINUTE ,GETDATE(),start_slot))%60),2) as wait_time,"
            Sql += " CONVERT(varchar(5),start_slot,114) as app_time,item_id,item_name,item_name_th,"
            Sql += " case when DATEDIFF(MINUTE ,GETDATE(),start_slot) < 0 then 0 else DATEDIFF(MINUTE ,GETDATE(),start_slot) end as time, "
            Sql += " appointment_job_id, getdate() date_now,start_slot,disable_ticket_waiting_time"
            Sql += " from TB_APPOINTMENT_CUSTOMER "
            Sql += " inner join TB_ITEM on TB_APPOINTMENT_CUSTOMER.item_id = TB_ITEM.id  "
            Sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and TB_ITEM.active_status = 1 "
            Sql += " and TB_APPOINTMENT_CUSTOMER.active_status = 1 and customer_id = '" & MobileNo & "' "
            Sql += " order by item_wait,item_time,item_order"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function GetAppServiceQty(ByVal MobileNo As String, ByVal QueueNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = " select count(*) as Count_app FROM (select distinct(queue_no)  from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and status = 1 and customer_id <>'" & FixDB(MobileNo) & "' and queue_no <> '" & FixDB(QueueNo) & "'  and customertype_id in (SELECT id FROM TB_CUSTOMERTYPE WHERE  (app = 1)) ) as count_q"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Sub UpdateCounterQueueAppointment(ByVal AllService As String, ByVal MobileNo As String, ByVal QueueNo As String, ByVal CustomerSegment As String, ByVal CurrDB As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal ListItem As String)
            Dim Sql As String = "update TB_COUNTER_QUEUE set segment = '" & FixDB(CustomerSegment) & "',combo_item_all = '" & AllService & "' where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(MobileNo) & "' and queue_no = '" & FixDB(QueueNo) & "' and status = 1"
            executeSQL(Sql, INIFileName, SoftwareName, FunctionName)
            If CurrDB = "MAIN" Then
                Engine.Common.KioskENG.ExecuteSqlToDisplay(Sql, INIFileName)
            End If

            Sql = "update TB_APPOINTMENT_CUSTOMER set active_status = 2,queue_no = '" & FixDB(QueueNo) & "',service = '" & FixDB(ListItem) & "' where datediff(d,getdate(),start_slot)=0 and customer_id = '" & MobileNo & "' and active_status = 1"
            executeSQL(Sql, INIFileName, SoftwareName, FunctionName)
            If CurrDB = "MAIN" Then
                Engine.Common.KioskENG.ExecuteSqlToDisplay(Sql, INIFileName)
            End If
        End Sub

        Public Function GetDataAppointmentNoShow(ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = " select t.item_name,t.item_name_th,t.item_order,ac.start_slot "
            Sql += " from TB_APPOINTMENT_CUSTOMER ac "
            Sql += " inner join TB_ITEM t on ac.item_id = t.id  "
            Sql += " where DATEDIFF(D,GETDATE(),ac.start_slot)=0 and t.active_status = 1 "
            Sql += " and ac.active_status = '5' and ac.customer_id = '" & MobileNo & "' "
            Sql += " order by ac.start_slot desc, t.item_order, t.item_name"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function FindCustomerType(ByVal CustomerSegment As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Int32
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select customertype_id from TB_segment where active_status = 1 and rtrim(ltrim(segment)) = '" & CustomerSegment & "'"
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("customertype_id").ToString
            Else
                Return DefaultCustomerTypeID
            End If
        End Function

        Public Function FindQueueNoAppointment(ByVal mobile As String, ByVal ItemID As Int32, ByVal txtQueue As String, ByVal TypeID As Int32, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "declare @Min as int; select @Min = (select app_min_queue from TB_ITEM where id = " & ItemID & ");"
            sql &= "declare @Max as int; select @Max = (select app_max_queue from TB_ITEM where id = " & ItemID & ");"
            sql &= "select top 1 @Min as MinQ,@Max as MaxQ ,CONVERT(int,RIGHT(queue_no,3)) as Q "
            sql &= "from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id "
            sql &= "where(DateDiff(D, GETDATE(), service_date) = 0 And customertype_id = " & TypeID & ")"
            sql &= "and item_id = " & ItemID & " and CONVERT(int,RIGHT(queue_no,3)) >= @Min and CONVERT(int,RIGHT(queue_no,3)) <=  @Max"
            sql &= " order by service_date desc"
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            If dt.Rows.Count > 0 Then
                If CInt(dt.Rows(0).Item("Q")) + 1 <= dt.Rows(0).Item("MaxQ") Then
                    Return txtQueue & CInt(dt.Rows(0).Item("Q") + 1).ToString.PadLeft(3, "0")
                Else
                    Return txtQueue & dt.Rows(0).Item("MinQ").ToString.PadLeft(3, "0")
                End If
            End If

            dt = New DataTable
            sql = "select app_min_queue from TB_ITEM where id = " & ItemID
            dt = getDataTable(sql, INIFileName, SoftwareName, "frmRegister_FindQueueNoAppointment")
            Return txtQueue & dt.Rows(0).Item("app_min_queue").ToString.PadLeft(3, "0")
        End Function

        Public Function CheckCapturePaGroup(ByVal PaGroup As String, ByVal UrlCapture As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Boolean
            Dim ret As Boolean = False
            If PaGroup.Trim <> "" And UrlCapture.Trim <> "" Then
                If Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "IsPACapture", SoftwareName, FunctionName) = "Y" Then
                    ret = True
                End If
            Else
                ret = True
            End If

            Return ret
        End Function
        Public Function CheckDetectCamera() As Boolean
            Dim ret As Boolean = False
            Try
                Dim Dispositive As Filters = New Filters
                If Dispositive.VideoInputDevices.Count = 1 Then
                    ret = True
                End If
            Catch ex As Exception

            End Try

            Return ret
        End Function

        Public Sub PrintQueueAppointTicket(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal PrintQueue As Print)
            Dim fn5 As New Font("Tahoma", 5, FontStyle.Regular)
            Dim fn8 As New Font("Tahoma", 8, FontStyle.Regular)
            Dim fn10 As New Font("Tahoma", 10, FontStyle.Regular)
            Dim fn8b As New Font("Tahoma", 8, FontStyle.Bold)
            Dim fn9b As New Font("Tahoma", 9, FontStyle.Bold)
            Dim fn10b As New Font("Tahoma", 10, FontStyle.Bold)
            Dim fn12b As New Font("Tahoma", 12, FontStyle.Bold)
            Dim fn16b As New Font("Tahoma", 16, FontStyle.Bold)
            Dim fn14b As New Font("Tahoma", 14, FontStyle.Bold)
            Dim fn24b As New Font("Tahoma", 24, FontStyle.Bold)
            Dim fn36b As New Font("Tahoma", 36, FontStyle.Bold)
            Dim fn42b As New Font("Tahoma", 42, FontStyle.Bold)

            If Language = "TH" Then
                PrintImage(Image.FromFile("Logo_TH.bmp"), KioskBaseENG.Align.Right, e)
            Else
                PrintImage(Image.FromFile("Logo_EN.bmp"), KioskBaseENG.Align.Right, e)
            End If

            If Language = "TH" Then
                PrintText("ยินดีต้อนรับ", fn16b, KioskBaseENG.Align.Center, e)
            Else
                PrintText("Welcome", fn16b, KioskBaseENG.Align.Center, e)
            End If
            'PrintText(" ", fn5, Align.Center, e)

            Dim vDateNow As New Date(PrintQueue.AppDateNow.Year, PrintQueue.AppDateNow.Month, PrintQueue.AppDateNow.Day, PrintQueue.AppDateNow.Hour, PrintQueue.AppDateNow.Minute, PrintQueue.AppDateNow.Second, PrintQueue.AppDateNow.Millisecond)
            If Language = "TH" Then
                PrintText("วันที่: " & vDateNow.ToString("dd-MM-yy", New Globalization.CultureInfo("th-TH")) & " | เวลา: " & vDateNow.ToString("HH:mm ") & " น.", fn10b, KioskBaseENG.Align.Center, e)
                PrintText("เวลาที่นัด: " & PrintQueue.AppTime & " น.", fn10b, KioskBaseENG.Align.Center, e)
            Else
                PrintText("Date: " & vDateNow.ToString("dd-MM-yy", New Globalization.CultureInfo("en-US")) & " | Time: " & vDateNow.ToString("hh:mm tt"), fn10b, KioskBaseENG.Align.Center, e)
                PrintText("Appointment Time: " & CDate(PrintQueue.AppTime).ToString("hh:mm tt"), fn10b, KioskBaseENG.Align.Center, e)
            End If
            PrintText(" ", fn5, KioskBaseENG.Align.Center, e)
            'PrintText(PrintQueue.Mobile, fn14b, KioskBaseENG.Align.Center, e)
            'PrintText(" ", fn5, Align.Center, e)
            Dim Item() As String = PrintQueue.ListService.Split(",")
            Dim dt As New DataTable
            For i As Int32 = 0 To Item.Length - 1
                PrintText(Item(i), fn10, KioskBaseENG.Align.Center, e)
            Next
            PrintText(" ", fn5, KioskBaseENG.Align.Center, e)
            If Language = "TH" Then
                PrintText("หมายเลข", fn12b, KioskBaseENG.Align.Center, e)
            Else
                PrintText("Number", fn12b, KioskBaseENG.Align.Center, e)
            End If
            PrintText(PrintQueue.QueueNo, fn24b, KioskBaseENG.Align.Center, e)
            'PrintText(" ", fn5, Align.Center, e)
            If Language = "TH" Then
                PrintText("มีคิวรอก่อนหน้าคุณ " & PrintQueue.CountQueue & " ราย", fn8, KioskBaseENG.Align.Center, e)
            Else
                PrintText("Current waiting queues before you: " & PrintQueue.CountQueue & " queues ", fn8, KioskBaseENG.Align.Center, e)
            End If

            If PrintQueue.HideWaitingTime = "0" Then
                If PrintQueue.WaitingTime <> "" Then
                    If Language = "TH" Then
                        PrintText("เวลาที่รอรับบริการประมาณ " & PrintQueue.WaitingTime & " นาที", fn8, KioskBaseENG.Align.Center, e)
                    Else
                        PrintText("Your estimated waiting time: " & PrintQueue.WaitingTime & " minutes ", fn8, KioskBaseENG.Align.Center, e)
                    End If
                End If
            End If

            PrintText(" ", fn5, KioskBaseENG.Align.Center, e)
            If Language = "TH" Then
                If Campaign_Desc1_TH.Trim <> "" Or Campaign_Desc2_TH.Trim <> "" Then
                    PrintText(Campaign_TH, fn8, KioskBaseENG.Align.Center, e)
                    PrintText(Campaign_Desc1_TH, fn8, KioskBaseENG.Align.Center, e)
                    PrintText(Campaign_Desc2_TH, fn8, KioskBaseENG.Align.Center, e)
                End If
            Else
                If Campaign_Desc1_EN.Trim <> "" Or Campaign_Desc2_EN.Trim <> "" Then
                    PrintText(Campaign_EN, fn8, KioskBaseENG.Align.Center, e)
                    PrintText(Campaign_Desc1_EN, fn8, KioskBaseENG.Align.Center, e)
                    PrintText(Campaign_Desc2_EN, fn8, KioskBaseENG.Align.Center, e)
                End If
            End If
            If PrintQueue.PrintTicketCoupon = "Y" Then
                PrintImage(Image.FromFile("Footer.gif"), Align.Center, e)
            End If

            e.HasMorePages = False
        End Sub
    End Class
End Namespace

