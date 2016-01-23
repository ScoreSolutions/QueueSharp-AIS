Imports System.Drawing

Namespace Kiosk
    Public Class KioskChooseServiceENG : Inherits KioskBaseENG
        Public Structure Print
            Dim QueueNo As String
            Dim Mobile As String
            Dim WaitingTime As String
            Dim ListService As String
            Dim CountQueue As String
            Dim HideWaiting As String
            Dim PrintTicketCoupon As String
        End Structure

        Public Function FindQueueNo(ByVal mobile As String, ByVal ItemID As Int32, ByVal TypeID As Int32, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As String
            Dim ret As String = ""
            Dim sql As String = ""
            Dim txtQueue As String = ""
            sql = "select txt_queue,count_queue_by_segment  from TB_ITEM where id = " & ItemID
            Dim dt As New DataTable
            dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
            txtQueue = dt.Rows(0).Item("txt_queue").ToString

            If txtQueue.Trim <> "" Then
                Dim CountQBySegment As String = dt.Rows(0)("count_queue_by_segment").ToString

                If CountQBySegment = "0" Then
                    sql = "declare @Min as int; select @Min = (select min_queue from TB_CUSTOMERTYPE  where id = " & TypeID & ");"
                    sql &= "declare @Max as int; select @Max = (select max_queue from TB_CUSTOMERTYPE  where id = " & TypeID & ");"
                Else
                    'กรณี Config ให้รันเลขคิวรวมกันทั้ง Mass และ Serenade โดยให้รันเป็นเลขของคิว Mass
                    'CustomerTypeID = 2  คือ Mass
                    sql = "declare @Min as int; select @Min = (select min_queue from TB_CUSTOMERTYPE  where id = 2);"
                    sql &= "declare @Max as int; select @Max = (select max_queue from TB_CUSTOMERTYPE  where id = 2);"
                End If

                sql &= "select top 1 @Min as MinQ,@Max as MaxQ ,CONVERT(int,RIGHT(queue_no,3)) as Q "
                sql &= " from TB_COUNTER_QUEUE "
                sql += " left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id "
                sql &= " where DateDiff(D, GETDATE(), service_date) = 0 "
                If CountQBySegment = "0" Then
                    sql += " and customertype_id = " & TypeID & " "
                Else
                    sql += " and customertype_id <>3"
                End If
                sql &= " and item_id = " & ItemID & " and LEFT(queue_no,1) = '" & txtQueue & "' and CONVERT(int,RIGHT(queue_no,3)) >= @Min and CONVERT(int,RIGHT(queue_no,3)) <=  @Max"
                sql &= " order by service_date desc"

                dt = New DataTable
                dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
                If dt.Rows.Count > 0 Then
                    If CInt(dt.Rows(0).Item("Q")) + 1 <= dt.Rows(0).Item("MaxQ") Then
                        ret = txtQueue & CInt(dt.Rows(0).Item("Q") + 1).ToString.PadLeft(3, "0")
                    Else
                        ret = txtQueue & dt.Rows(0).Item("MinQ").ToString.PadLeft(3, "0")
                    End If
                Else
                    dt = New DataTable
                    If CountQBySegment = "0" Then
                        sql = "select min_queue from TB_CUSTOMERTYPE  where id = " & TypeID
                    Else
                        sql = "select min_queue from TB_CUSTOMERTYPE  where id = 2"
                    End If
                    dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_FindQueueNo")
                    ret = txtQueue & dt.Rows(0).Item("min_queue").ToString.PadLeft(3, "0")
                End If
            End If

            Return ret
        End Function

        Public Sub UpdateQueueNo(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal vDateNow As String, ByVal CurrDB As String, ByVal MobileNo As String, ByVal AllService As String, ByVal Segment As String, ByVal QueueNo As String, ByVal ItemID As Integer)
            Dim sql As String = ""
            'Update Queue No.
            sql = "update TB_COUNTER_QUEUE set service_date = " & vDateNow & ",assign_time = " & vDateNow & ",segment = '" & FixDB(Segment) & "',combo_item_all = '" & AllService & "' where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(MobileNo) & "' and queue_no = '" & FixDB(QueueNo) & "' and status = 1"
            executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            'If CurrDB = "MAIN" Then
            '    Engine.Common.KioskENG.ExecuteSqlToDisplay(sql, INIFileName)
            'End If

            'Update Service
            sql = "update TB_COUNTER_QUEUE set flag = '1' where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(MobileNo) & "' and queue_no = '" & FixDB(QueueNo) & "' and item_id = " & ItemID
            executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            'If CurrDB = "MAIN" Then
            '    Engine.Common.KioskENG.ExecuteSqlToDisplay(sql, INIFileName)
            'End If
        End Sub

        Public Sub PrintQueueWalkInTicket(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal PrintQueue As Print)
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

            'Dim eng As New KioskRegisterENG
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
            Dim vDateNow As DateTime = FindDateNow(INIFileName, SoftwareName, "frmChooseService_pd.PrintPage")
            If Language = "TH" Then
                PrintText("วันที่: " & vDateNow.ToString("dd-MM-") & StringFromRight(IIf(vDateNow.Year < 2500, vDateNow.Year + 543, vDateNow.Year), 2) & " | เวลา: " & vDateNow.ToString("HH:mm") & " น.", fn10b, KioskBaseENG.Align.Center, e)
            Else
                PrintText("Date: " & vDateNow.ToString("dd-MM-") & StringFromRight(IIf(vDateNow.Year > 2500, vDateNow.Year - 543, vDateNow.Year), 2) & " | Time: " & vDateNow.ToString("hh:mm tt"), fn10b, KioskBaseENG.Align.Center, e)
            End If

            PrintText(" ", fn5, KioskRegisterENG.Align.Center, e)
            'PrintText(PrintQueue.Mobile, fn14b, KioskRegisterENG.Align.Center, e)
            ''PrintText(" ", fn5, Align.Center, e)
            Dim Item() As String = PrintQueue.ListService.Split(",")
            Dim dt As New DataTable
            For i As Int32 = 0 To Item.Length - 1
                PrintText(Item(i), fn10, KioskRegisterENG.Align.Center, e)
            Next
            PrintText(" ", fn5, KioskRegisterENG.Align.Center, e)
            If Language = "TH" Then
                PrintText("หมายเลข", fn12b, KioskRegisterENG.Align.Center, e)
            Else
                PrintText("Number", fn12b, KioskRegisterENG.Align.Center, e)
            End If
            PrintText(PrintQueue.QueueNo, fn24b, KioskRegisterENG.Align.Center, e)
            'PrintText(" ", fn5, Align.Center, e)
            If Language = "TH" Then
                PrintText("มีคิวรอก่อนหน้าคุณ " & PrintQueue.CountQueue & " ราย", fn8, KioskBaseENG.Align.Center, e)
            Else
                PrintText("Current waiting queues before you: " & PrintQueue.CountQueue & " queues ", fn8, KioskBaseENG.Align.Center, e)
            End If


            If PrintQueue.HideWaiting = "0" Then
                If PrintQueue.WaitingTime <> "" Then
                    If Language = "TH" Then
                        PrintText("เวลาที่รอรับบริการประมาณ " & PrintQueue.WaitingTime & " นาที", fn8, KioskRegisterENG.Align.Center, e)
                    Else
                        PrintText("Your estimated waiting time: " & PrintQueue.WaitingTime & " minutes ", fn8, KioskRegisterENG.Align.Center, e)
                    End If
                End If
            End If

            If Language = "TH" Then
                If Campaign_Desc1_TH.Trim <> "" Or Campaign_Desc2_TH.Trim <> "" Then
                    PrintText(" ", fn5, KioskRegisterENG.Align.Center, e)
                    PrintText(Campaign_TH, fn8, KioskRegisterENG.Align.Center, e)
                    PrintText(Campaign_Desc1_TH, fn8, KioskRegisterENG.Align.Center, e)
                    PrintText(Campaign_Desc2_TH, fn8, KioskRegisterENG.Align.Center, e)
                End If
            Else
                If Campaign_Desc1_EN.Trim <> "" Or Campaign_Desc2_EN.Trim <> "" Then
                    PrintText(" ", fn5, KioskRegisterENG.Align.Center, e)
                    PrintText(Campaign_EN, fn8, KioskRegisterENG.Align.Center, e)
                    PrintText(Campaign_Desc1_EN, fn8, KioskRegisterENG.Align.Center, e)
                    PrintText(Campaign_Desc2_EN, fn8, KioskRegisterENG.Align.Center, e)
                End If
            End If
            If PrintQueue.PrintTicketCoupon = "Y" Then
                PrintImage(Image.FromFile("Footer.gif"), Align.Center, e)
            End If



            e.HasMorePages = False
        End Sub

        Public Function CheckAppointmentSchedule(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select top 1 id from TB_APPOINTMENT_SCHEDULE where datediff(d,app_date,GETDATE()) = 0"
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function
        Public Function CheckQueueCustomerInShop(ByVal MobileNo As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql = "select top 1 id from tb_counter_queue where datediff(d,getdate(),service_date)=0 and status in (1,2,4) and customer_id = '" & FixDB(MobileNo) & "'"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function CheckServiceAppointment(ByVal serviceId As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = "select item_name,item_name_th  from TB_ITEM where active_status = 1 "
            Sql += " and id in (" & serviceId & ") "
            Sql += " and id not in (select item_id from TB_APPOINTMENT_ITEM where DATEDIFF(D,GETDATE(),app_date)=0)"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, "frmChooseService_btnAppointment_Click")
            Return dt
        End Function
    End Class
End Namespace

