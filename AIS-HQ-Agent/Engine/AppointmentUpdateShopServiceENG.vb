Imports System.Data
Imports CenLinqDB.TABLE
Imports System.Windows.Forms
Imports CenParaDB.Appointment
Imports CenParaDB.Common.Utilities
Imports Cen = CenLinqDB.TABLE
Imports uSh = ShLinqDB.Common.Utilities
Imports cSh = ShParaDB.Common
Imports Engine.Common
Imports Engine.GateWay
Imports CenLinqDB.Common.Utilities
Imports Sh = ShLinqDB.TABLE
Imports pSh = ShParaDB.TABLE
Imports System.Globalization

Public Class AppointmentUpdateShopServiceENG
    Dim _err As String = ""

    Public Sub UpdateShopAppointmentService(ByVal lblTime As Label)
        Try
            Dim sDt As DataTable = Engine.Common.FunctionEng.GetActiveShop()
            If sDt.Rows.Count > 0 Then
                Dim MaxAppointmentDay As String = Engine.Common.FunctionEng.GetQisDBConfig("MaxAppointmentDay")
                If MaxAppointmentDay.Trim = "" Then
                    MaxAppointmentDay = "7"
                End If
                Dim vDateFrom As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim vDateTo As String = DateAdd(DateInterval.Day, Convert.ToInt16(MaxAppointmentDay), DateTime.Now).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim dDateFrom As New Date(Today.Year, Today.Month, Today.Day)

                For Each sDr As DataRow In sDt.Rows
                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Dim dSql As String = "delete from tb_shop_service_appointment  "
                    dSql += " where shop_id='" & sDr("id") & "'"
                    dSql += " and convert(varchar(8),app_date,112) between '" & vDateFrom & "' and '" & vDateTo & "'"
                    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
                    trans.CommitTransaction()

                    Dim shTran As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(sDr("id"), "Engine.UpdateShopAppointServiceENG_UpdateShopAppointmentService")
                    If shTran.Trans IsNot Nothing Then
                        Dim sql As String = "select distinct i.master_itemid,CONVERT(varchar(8),sc.app_date,112) app_date " & vbNewLine
                        sql += " from TB_APPOINTMENT_SCHEDULE sc " & vbNewLine
                        sql += " inner join TB_APPOINTMENT_ITEM ai on CONVERT(varchar(8),ai.app_date,112)=CONVERT(varchar(8),sc.app_date,112) " & vbNewLine
                        sql += " inner join TB_ITEM i on i.id=ai.item_id " & vbNewLine
                        sql += " where CONVERT(varchar(8),sc.app_date,112) between '" & vDateFrom & "' and '" & vDateTo & "'"
                        sql += " and i.active_status='1'"

                        Dim slDt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTran.Trans)
                        shTran.CommitTransaction()
                        If slDt.Rows.Count > 0 Then
                            Dim CurrDate As Date = dDateFrom
                            Do
                                slDt.DefaultView.RowFilter = "app_date='" & CurrDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                                For Each slDr As DataRowView In slDt.DefaultView
                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    If trans.Trans IsNot Nothing Then
                                        Dim lnq As New TbShopServiceAppointmentCenLinqDB
                                        lnq.ChkDataByAPP_DATE_MASTER_ITEMID_SHOP_ID(CurrDate, slDr("master_itemid").ToString, sDr("id"), trans.Trans)
                                        lnq.SHOP_ID = sDr("id")
                                        If Convert.IsDBNull(slDr("master_itemid")) = False Then lnq.MASTER_ITEMID = slDr("master_itemid")
                                        lnq.APP_DATE = CurrDate

                                        Dim ret As Boolean = False
                                        If lnq.ID > 0 Then
                                            ret = lnq.UpdateByPK("UpdateShopAppointmentService", trans.Trans)
                                        Else
                                            ret = lnq.InsertData("UpdateShopAppointmentService", trans.Trans)
                                        End If

                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                        End If
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    End If
                                Next
                                slDt.DefaultView.RowFilter = ""
                                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                            Loop While CurrDate <= DateAdd(DateInterval.Day, Convert.ToInt16(MaxAppointmentDay), DateTime.Now).Date
                        End If
                        slDt.Dispose()
                    End If
                Next
            End If
            sDt.Dispose()
        Catch ex As Exception

        End Try

    End Sub
    '#Region "Edit Appointment"
    '    Public Sub SetEditAppointmentJob()
    '        Try
    '            Dim lnq As New TbAppointmentEditJobCenLinqDB
    '            Dim dt As New DataTable
    '            dt = lnq.GetDataList("IsDeleteOldData='N'", "", Nothing)
    '            If dt.Rows.Count > 0 Then
    '                For Each dr As DataRow In dt.Rows







    '                    Dim ShopID As String = dr("tb_shop_id").ToString
    '                    Dim StartTime As DateTime = Convert.ToDateTime(dr("start_slot"))
    '                    Dim ItemID As String = dr("service_id").ToString
    '                    Dim MobileNo As String = dr("mobile_no").ToString
    '                    Dim AppointmentChannel As String = dr("appointment_channel").ToString
    '                    Dim Email As String = ""
    '                    If Convert.IsDBNull(dr("customer_email")) = False Then
    '                        Email = dr("customer_email").ToString
    '                    End If
    '                    Dim PreferLang As String = ""
    '                    If Convert.IsDBNull(dr("prefer_lang")) = False Then
    '                        PreferLang = dr("prefer_lang").ToString
    '                    End If


    '                    Dim tmpItem() As String = Split(ItemID, ",")
    '                    Dim para1(tmpItem.Length - 1) As pSh.TbAppointmentCustomerShParaDB
    '                    Dim i As Integer = 0
    '                    For Each ServiceID As String In tmpItem
    '                        Dim para As New pSh.TbAppointmentCustomerShParaDB
    '                        para.CAPACITY = 1
    '                        para.APP_DATE = DateTime.Now
    '                        para.START_SLOT = StartTime
    '                        para.ITEM_ID = ServiceID
    '                        para.CUSTOMER_ID = MobileNo
    '                        para.APPOINTMENT_CHANNEL = AppointmentChannel
    '                        para.CUSTOMER_EMAIL = Email
    '                        para.ACTIVE_STATUS = 1
    '                        para1(i) = para
    '                        para = Nothing
    '                        i += 1
    '                    Next

    '                    Dim ret As InsertAppointmentResultPara = InsertAppointment(ShopID, para1, PreferLang, AppointmentChannel, False) 'EditAppointment(ShopID, StartTime, ItemID, MobileNo, PreferLang, AppointmentChannal, Email)
    '                    If ret.RESPONSE = True Then

    '                    End If
    '                Next

    '            End If
    '            dt.Dispose()
    '            lnq = Nothing
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Function EditAppointment(ByVal JobIdOld As Long) As EditAppointmentResultPara
    '        'การแก้ไขคือการลบขัอมูลการจองล่วงหน้าเดิมก่อน แล้วค่อย Insert ใหม่
    '        Dim ret As New EditAppointmentResultPara
    '        Dim CurrentSiebelAct As String = GetActiveSiebelActivity(CustomerID)

    '        Dim eng As New AppointmentUpdateShopServiceENG
    '        If eng.DeleteAppointment(CustomerID) = True Then
    '            Dim tmpRet As New InsertAppointmentResultPara
    '            Dim ItemName As String = ""
    '            Dim tmpItem() As String = Split(ItemID, ",")
    '            Dim para1(tmpItem.Length - 1) As pSh.TbAppointmentCustomerShParaDB
    '            Dim i As Integer = 0
    '            For Each ServiceID As String In tmpItem
    '                Dim para As New pSh.TbAppointmentCustomerShParaDB
    '                para.CAPACITY = 1
    '                para.APP_DATE = DateTime.Now
    '                para.START_SLOT = StartTime
    '                para.ITEM_ID = ServiceID
    '                para.CUSTOMER_ID = CustomerID
    '                para.APPOINTMENT_CHANNEL = AppointmentChannal
    '                para.ACTIVE_STATUS = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment
    '                para.CUSTOMER_EMAIL = CustomerEmail
    '                para1(i) = para


    '                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShopID, "AppointmentENG.EditAppointment")
    '                Dim iLnq As New Sh.TbItemShLinqDB
    '                iLnq.GetDataByPK(ServiceID, shTrans.Trans)
    '                If iLnq.ID > 0 Then
    '                    If ItemName = "" Then
    '                        ItemName = iLnq.ITEM_NAME_TH
    '                    Else
    '                        ItemName += "," & iLnq.ITEM_NAME_TH
    '                    End If
    '                End If
    '                shTrans.CommitTransaction()
    '                iLnq = Nothing

    '                i += 1
    '            Next
    '            tmpRet = eng.InsertAppointment(ShopID, para1, PreferLang, AppointmentChannal, False)
    '            ret.RESPONSE = tmpRet.RESPONSE
    '            ret.ErrorMessage = tmpRet.ErrorMessage

    '            Dim OldSiebel() As String = Split(CurrentSiebelAct, "##")
    '            If OldSiebel.Length = 3 Then
    '                ''Update Siebel Activiry ให้เป็นค่าเดิม
    '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)

    '                Dim gw As New GateWay.GateWayServiceAppointmentENG
    '                gw.SiebelUpdateDescription(CustomerID, StartTime, ItemName, AppointmentChannal, shLnq, trans, OldSiebel(0))
    '                gw = Nothing
    '                shLnq = Nothing
    '                trans.CommitTransaction()
    '            End If
    '        Else
    '            ret.RESPONSE = False
    '            ret.ErrorMessage = "Cannon Delete Appointment Data"
    '        End If

    '        Return ret
    '    End Function

    '    Private Function SaveAppointmentJob(ByVal p As CenParaDB.TABLE.TbAppointmentJobCenParaDB) As CenParaDB.Appointment.SaveAppointmentJobPara
    '        Dim ret As New CenParaDB.Appointment.SaveAppointmentJobPara
    '        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '        Dim lnq As New CenLinqDB.TABLE.TbAppointmentJobCenLinqDB
    '        If p.ID <> 0 Then
    '            lnq.GetDataByPK(p.ID, trans.Trans)
    '        End If

    '        lnq.SHOP_ABB = p.SHOP_ABB
    '        lnq.MOBILE_NO = p.MOBILE_NO
    '        lnq.APP_DATE = p.APP_DATE
    '        lnq.START_SLOT = p.START_SLOT
    '        lnq.APPOINTMENT_CHANNEL = p.APPOINTMENT_CHANNEL
    '        lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

    '        Dim re As Boolean = False
    '        If lnq.ID <> 0 Then
    '            re = lnq.UpdateByPK("AppointmentENG.SaveAppointmentJob", trans.Trans)
    '        Else
    '            re = lnq.InsertData("AppointmentENG.SaveAppointmentJob", trans.Trans)
    '            If re = True Then
    '                p.ID = lnq.ID
    '            End If
    '        End If
    '        ret.SaveResult = re
    '        ret.AppointmentJobPara = p

    '        If re = False Then
    '            trans.RollbackTransaction()
    '            ret.ErrorMessage = lnq.ErrorMessage
    '        Else
    '            trans.CommitTransaction()
    '        End If

    '        Return ret
    '    End Function

    '    Private Function ValidInsert(ByVal para As pSh.TbAppointmentCustomerShParaDB) As Boolean
    '        Dim ret As Boolean = True
    '        If para.APP_DATE.Value.Year = 1 Then
    '            _err = "Please Select Reservation Date"
    '            ret = False
    '        ElseIf para.CUSTOMER_ID.Trim = "" Then
    '            _err = "Please Input Mobile No"
    '            ret = False
    '        ElseIf para.START_SLOT.Value.Year = 1 Then
    '            _err = "Please Select Start Time"
    '            ret = False
    '        ElseIf para.APPOINTMENT_CHANNEL.ToString.Trim = "" Then
    '            _err = "Please Input Appointment Channel"
    '            ret = False
    '        ElseIf para.APPOINTMENT_CHANNEL.ToString > "3" Or para.APPOINTMENT_CHANNEL.ToString <= "0" Then
    '            _err = "Invalid Appointment Channel"
    '            ret = False
    '        End If

    '        Return ret
    '    End Function

    '    Private Function InsertAppointment(ByVal ShopID As String, ByVal paraList() As pSh.TbAppointmentCustomerShParaDB, ByVal PreferLang As String, ByVal AppointmentChannel As String, Optional ByVal IsCreateActivity As Boolean = True) As InsertAppointmentResultPara
    '        Dim resPara As New InsertAppointmentResultPara
    '        Dim MobileNo As String = paraList(0).CUSTOMER_ID
    '        Dim StartTime As DateTime = paraList(0).START_SLOT.Value
    '        Dim lnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(ShopID)

    '        'Save Appointment Job
    '        Dim pJob As New CenParaDB.Appointment.SaveAppointmentJobPara
    '        Dim pPara As New CenParaDB.TABLE.TbAppointmentJobCenParaDB
    '        pPara.SHOP_ABB = lnq.SHOP_ABB
    '        pPara.MOBILE_NO = MobileNo
    '        pPara.APP_DATE = DateTime.Now
    '        pPara.START_SLOT = StartTime
    '        pPara.APPOINTMENT_CHANNEL = AppointmentChannel
    '        pPara.ACTIVE_STATUS = Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment

    '        pJob = SaveAppointmentJob(pPara)
    '        pPara = Nothing

    '        If pJob.SaveResult = True Then
    '            Dim ServiceID As String = ""
    '            Dim ServiceName As String = ""
    '            Dim AppDate As DateTime = DateTime.Now
    '            For Each para In paraList
    '                If ValidInsert(para) Then
    '                    MobileNo = para.CUSTOMER_ID
    '                    StartTime = para.START_SLOT.Value
    '                    'Insert Appointment At shop
    '                    Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(lnq.ID, "Enging.AppointmentENG.InsertAppointment")
    '                    Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
    '                    shLinq.APP_DATE = AppDate
    '                    shLinq.CAPACITY = para.CAPACITY
    '                    shLinq.ITEM_ID = para.ITEM_ID
    '                    shLinq.CUSTOMER_ID = para.CUSTOMER_ID
    '                    shLinq.START_SLOT = para.START_SLOT
    '                    shLinq.ACTIVE_STATUS = para.ACTIVE_STATUS
    '                    shLinq.APPOINTMENT_CHANNEL = para.APPOINTMENT_CHANNEL
    '                    shLinq.CUSTOMER_EMAIL = para.CUSTOMER_EMAIL
    '                    shLinq.APPOINTMENT_JOB_ID = pJob.AppointmentJobPara.ID

    '                    Dim ret As Boolean = shLinq.InsertData(para.CUSTOMER_ID, shTrans.Trans)
    '                    resPara.RESPONSE = ret
    '                    If ret = False Then
    '                        resPara.ErrorMessage = shLinq.ErrorMessage
    '                        shTrans.RollbackTransaction()
    '                    Else
    '                        resPara.ErrorMessage = ""

    '                        If ServiceID = "" Then
    '                            ServiceID = para.ITEM_ID
    '                        Else
    '                            ServiceID += "," & para.ITEM_ID
    '                        End If

    '                        Dim shILnq As New ShLinqDB.TABLE.TbItemShLinqDB
    '                        shILnq = FunctionEng.GetShopItemPara(para.ITEM_ID, shTrans)
    '                        If ServiceName = "" Then
    '                            ServiceName = shILnq.ITEM_NAME
    '                        Else
    '                            ServiceName += ", " & shILnq.ITEM_NAME
    '                        End If
    '                        shTrans.CommitTransaction()

    '                        shILnq = Nothing
    '                    End If
    '                Else
    '                    resPara.RESPONSE = False
    '                    resPara.ErrorMessage = _err
    '                End If
    '            Next

    '            If resPara.ErrorMessage = "" Then
    '                AddTimeSlotCapacity(ServiceID, StartTime, ShopID)
    '                SendSMSConfirm(MobileNo, ServiceID, paraList(0).START_SLOT.Value, PreferLang, lnq)
    '                CreateAppointmentNotifyJoblist(ShopID, MobileNo, StartTime, AppointmentChannel, PreferLang, paraList(0).CUSTOMER_EMAIL)
    '                If paraList(0).CUSTOMER_EMAIL.Trim <> "" Then
    '                    SendEmailConfirm(paraList(0).CUSTOMER_EMAIL.Trim, ServiceID, MobileNo, StartTime, PreferLang, ShopID)
    '                End If


    '                If IsCreateActivity = True Then
    '                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '                    Dim gw As New GateWayServiceAppointmentENG
    '                    gw.SiebelCreateActivity(MobileNo, paraList(0).START_SLOT.Value, ServiceName, paraList(0).APPOINTMENT_CHANNEL, lnq, trans)
    '                    gw = Nothing
    '                    trans.CommitTransaction()
    '                End If
    '            End If
    '        Else
    '            resPara.RESPONSE = False
    '            resPara.ErrorMessage = pJob.ErrorMessage
    '        End If

    '        Return resPara
    '    End Function

    '    Private Function SendEmailConfirm(ByVal MailTo As String, ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As Boolean
    '        Dim gw As New GateWayServiceAppointmentENG
    '        Dim PicturePath As String = Application.StartupPath & "\MailPicture"
    '        Dim file() As String = {PicturePath & "\logo.gif", PicturePath & "\bg_leave.jpg", PicturePath & "\aunjai_shop.png"}
    '        Dim ret As Boolean = gw.SendEmailAttFile(MailTo, "Confirm Appointment", CreateMailConfirm(ServiceID, MobileNo, AppointmentTime, PreLang, ShopID), file)
    '        gw = Nothing
    '        Return ret
    '    End Function

    '    Private Function CreateMailConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As String
    '        Dim ret As String = ""

    '        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
    '        Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(shLnq.ID, "Engine.AppointmentENG.CreateMailConfirm")

    '        Dim SlotDT As New DataTable
    '        Dim SlotLnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
    '        SlotDT = SlotLnq.GetDataList("convert(varchar(8),app_date,112)='" & AppointmentTime.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'", "", shTrans.Trans)
    '        If SlotDT.Rows.Count > 0 Then
    '            Dim lnq As New CenLinqDB.TABLE.TbCustomerCenLinqDB
    '            Dim cPara As New CenParaDB.TABLE.TbCustomerCenParaDB
    '            cPara = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)

    '            Dim ServiceName As String = ""
    '            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
    '            Dim sDt As New DataTable
    '            sDt = sLnq.GetDataList("master_itemid in (" & ServiceID & ")", "item_time,item_wait,item_order", shTrans.Trans)
    '            Dim i As Integer = 0
    '            For Each sDr As DataRow In sDt.Rows
    '                If InStr(PreLang.ToUpper(), "THAI") > 0 Then
    '                    If ServiceName = "" Then
    '                        ServiceName = (i + 1).ToString & "." & sDr("ITEM_NAME_TH").ToString
    '                    Else
    '                        ServiceName += ", " & (i + 1).ToString & "." & sDr("ITEM_NAME_TH").ToString
    '                    End If
    '                Else
    '                    If ServiceName = "" Then
    '                        ServiceName += (i + 1).ToString & "." & sDr("ITEM_NAME").ToString
    '                    Else
    '                        ServiceName += ", " & (i + 1).ToString & "." & sDr("ITEM_NAME").ToString
    '                    End If
    '                End If
    '                i += 1
    '            Next
    '            sLnq = Nothing
    '            sDt.Dispose()

    '            If ServiceName.IndexOf("2.") = -1 Then
    '                ServiceName = ServiceName.Replace("1.", "")
    '            End If
    '            'Config Information
    '            Dim kBefore As Integer = Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans))
    '            Dim NoShowQty As Integer = FunctionEng.GetQisDBConfig("AppointmentNoShowQty")  'ผิดนัดเป็นจำนวนกี่ครั้ง
    '            'Dim WithinDay As Integer = FunctionEng.GetQisDBConfig("AppointmentWithinDay")  'ภายในกี่วัน
    '            Dim NoBookDay As Integer = FunctionEng.GetQisDBConfig("NoBookDay") 'จะถูกระงับการจองไปนานกี่วัน


    '            Dim CustomerName As String = ""
    '            Dim txtMobile As String = ""
    '            Dim TxtAppoontment As String = ""
    '            Dim lblDate As String = ""
    '            Dim txtDate As String = ""
    '            Dim lblTime As String = ""
    '            Dim txtTime As String = ""
    '            Dim lblShopName As String = ""
    '            Dim txtShopName As String = ""
    '            Dim lblServiceName As String = ""
    '            Dim txtServiceName As String = ServiceName
    '            Dim lblRegister As String = ""
    '            Dim lblRedIssue As String = ""
    '            Dim lblNoShow As String = ""
    '            If InStr(PreLang.ToUpper(), "THAI") > 0 Then
    '                If cPara.F_NAME <> "" And cPara.L_NAME <> "" Then
    '                    CustomerName = "เรียนคุณ " & cPara.F_NAME & " " & cPara.L_NAME
    '                End If
    '                txtMobile = "ผู้ใช้บริการโทรศัพท์หมายเลข " & MobileNo
    '                TxtAppoontment = "คุณมีรายการนัดหมายล่วงหน้า เพื่อติดต่อรับบริการจาก AIS Shop โดยมีรายละเอียดดังนี้:"
    '                lblDate = "วัน "
    '                txtDate = AppointmentTime.ToString("dddd", New Globalization.CultureInfo("th-TH")) & " ที่  " & AppointmentTime.ToString("dd MMMM yyyy", New Globalization.CultureInfo("th-TH"))
    '                lblTime = "เวลา"
    '                txtTime = AppointmentTime.ToString("HH:mm") & " - " & DateAdd(DateInterval.Minute, Convert.ToInt64(SlotDT.Rows(0)("slot")) * i, AppointmentTime).ToString("HH:mm") & "น."
    '                lblShopName = "สถานที่"
    '                txtShopName = shLnq.SHOP_NAME_TH 'FunctionEng.GetShopConfig("s_name_th", shTrans)
    '                lblServiceName = "บริการ"
    '                lblRegister = "กรุณาเช็คอิน รับคิวนัดหมายล่วงหน้า ที่ AIS Shop ที่ระบุ ก่อนถึงเวลานัด " & kBefore & " นาที"
    '                lblRedIssue = "หากไม่มาติดต่อขอรับบริการภายในเวลาที่นัดหมาย บริษัทขอสงวนสิทธิ์ยกเลิกการนัด<br />โดยไม่ต้องแจ้งให้ทราบล่วงหน้า ผู้ใช้บริการต้องกดบัตรคิวใหม่เพื่อรับบริการ"
    '                lblNoShow = "*กรณีไม่มาติดต่อขอรับบริการหลังจากมีการนัดหมายล่วงหน้าเกิน " & NoShowQty & " ครั้ง คุณจะไม่สามารถทำการนัดหมายล่วงหน้าได้อีก<br />ภายในระยะเวลา " & NoBookDay & " วัน นับจากวันที่ที่มีการนัดหมายครั้งล่าสุด*"
    '            Else
    '                If cPara.F_NAME <> "" And cPara.L_NAME <> "" Then
    '                    CustomerName = "Dear, " & cPara.F_NAME & " " & cPara.L_NAME
    '                End If
    '                txtMobile = "Mobile Number :  " & MobileNo
    '                TxtAppoontment = " Appointment Reminder with AIS Shop "
    '                lblDate = "Day "
    '                txtDate = AppointmentTime.ToString("dddd", New Globalization.CultureInfo("en-US")) & " (" & AppointmentTime.ToString("MMMM dd", New Globalization.CultureInfo("en-US")) & "," & AppointmentTime.ToString("yyyy", New Globalization.CultureInfo("en-US")) & ")"
    '                lblTime = "Time"
    '                txtTime = AppointmentTime.ToString("HH:mmtt") & " - " & DateAdd(DateInterval.Minute, Convert.ToInt64(SlotDT.Rows(0)("slot")) * i, AppointmentTime).ToString("HH:mmtt")
    '                lblShopName = "Shop Name"
    '                txtShopName = shLnq.SHOP_NAME_EN ' FunctionEng.GetShopConfig("s_name_en", shTrans)
    '                lblServiceName = "Service"
    '                'lblRegister = "กรุณาเช็คอิน รับคิวนัดหมายล่วงหน้า ที่ AIS Shop ที่ระบุ ก่อนถึงเวลานัด " & kCancel
    '                'lblRedIssue = "หากไม่มาติดต่อขอรับบริการภายในเวลาที่นัดหมาย บริษัทขอสงวนสิทธิ์ยกเลิกการนัด<br />โดยไม่ต้องแจ้งให้ทราบล่วงหน้า ผู้ใช้บริการต้องกดบัตรคิวใหม่เพื่อรับบริการ"
    '                'lblNoShow = "*กรณีไม่มาติดต่อขอรับบริการหลังจากมีการนัดหมายล่วงหน้าเกิน " & NoShowQty & " ครั้ง คุณจะไม่สามารถทำการนัดหมายล่วงหน้าได้อีก<br />ภายในระยะเวลา " & NoBookDay & " วัน นับจากวันที่ที่มีการนัดหมายครั้งล่าสุด*"
    '                lblRegister = "Please arrive at AIS Shop " & kBefore & " minutes before your appointment time."
    '                lblRedIssue = "In case you do not show up by the appointment date and time,<br />we reserve the right to skip your queue without prior notice,<br/>and you can take a new queue ticket at AIS Shop."
    '                lblNoShow = "* In case you do not show up after making appointment, you will not be able<br />to make appointment for your next visit."
    '            End If

    '            ret += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>"
    '            ret += "    <tr>"
    '            ret += "        <td width='90%' alight='left'>" & CustomerName & "</td>"
    '            ret += "        <td width='10%' alight='right'><IMG SRC='cid:ATTIMG1' width='138' height='62' /></td>"
    '            ret += "    </tr>"
    '            ret += "    <tr>"
    '            ret += "        <td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtMobile & "</td>"
    '            ret += "    </tr>"
    '            ret += "    <tr><td colspan='2'>&nbsp;</td></tr>"
    '            ret += "    <tr>"
    '            ret += "        <td colspan='2'>" & TxtAppoontment & "</td>"
    '            ret += "    </tr>"
    '            ret += "    <tr>"
    '            ret += "        <td colspan='2'>"
    '            ret += "            <table border='0' cellpadding='0' cellspacing='0' width='100%'>"
    '            ret += "                <tr>"
    '            ret += "                    <td width='20%'>" & lblDate & "</td>"
    '            ret += "                    <td width='80%'>: " & txtDate & "</td>"
    '            ret += "                </tr>"
    '            ret += "                <tr>"
    '            ret += "                    <td >" & lblTime & "</td>"
    '            ret += "                    <td >: " & txtTime & "</td>"
    '            ret += "                </tr>"
    '            ret += "                <tr>"
    '            ret += "                    <td >" & lblShopName & "</td>"
    '            ret += "                    <td >: " & txtShopName & "</td>"
    '            ret += "                </tr>"
    '            ret += "                <tr>"
    '            ret += "                    <td >" & lblServiceName & "</td>"
    '            ret += "                    <td >: " & ServiceName & "</td>"
    '            ret += "                </tr>"
    '            ret += "            </table>"
    '            ret += "        </td>"
    '            ret += "    </tr>"
    '            ret += "    <tr><td colspan='2'>&nbsp;</td></tr>"
    '            ret += "    <tr>"
    '            ret += "        <td colspan='2'>"
    '            ret += "            <table border='0' cellpadding='0' cellspacing='0' width='100%' height='150' background=" & Chr(34) & "cid:ATTIMG2" & Chr(34) & " style='background-repeat:no-repeat;background-size:273px 150px;'  >"
    '            ret += "                <tr>"
    '            ret += "                    <td colspan='2'>"
    '            ret += "                        <font color='red' size='5px'><u><i>" & lblRegister & "</i></u></font>"
    '            ret += "                    </td>"
    '            ret += "                </tr>"
    '            ret += "                <tr>"
    '            ret += "                    <td width='80%' valign='top' >"
    '            ret += "                        <font color='red'>" & lblRedIssue & "</font>"
    '            ret += "                    </td>"
    '            ret += "                    <td width='20%' ><IMG SRC='cid:ATTIMG3'  height='150' /></td>"
    '            ret += "                </tr>"
    '            ret += "                <tr>"
    '            ret += "                    <td colspan='2'>" & lblNoShow & "</td>"
    '            ret += "                </tr>"
    '            ret += "            </table>"
    '            ret += "        </td>"
    '            ret += "    </tr>"
    '            ret += "</table>"

    '            shTrans.CommitTransaction()
    '            lnq = Nothing
    '            cPara = Nothing
    '        End If

    '        Return ret
    '    End Function

    '    Private Sub CreateAppointmentNotifyJoblist(ByVal ShopID As Long, ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal AppointmentChannel As String, ByVal PreferLang As String, ByVal CustomerEmail As String)
    '        Dim SmsTime() As String = Split(Engine.Common.FunctionEng.GetQisDBConfig("AppointmentSMSNotifyTime"), "-")
    '        Dim SmsTimeFrom() As String = Split(SmsTime(0), ":")
    '        Dim SmsTimeTo() As String = Split(SmsTime(1), ":")
    '        Dim BeforeHours1 As Integer = 24    'ล่วงหน้า 24 ชั่วโมง
    '        Dim BeforeHours2 As Integer = 30    'ล่วงหน้า 30 นาที

    '        Dim Time1 As DateTime = DateAdd(DateInterval.Hour, (0 - BeforeHours1), StartSlot)
    '        Dim Alert1 As String = "N"
    '        If Time1.ToString("HH:mm", New CultureInfo("en-US")) < SmsTime(0) Then
    '            Time1 = New DateTime(Time1.Year, Time1.Month, Time1.Day, CInt(SmsTimeFrom(0)), CInt(SmsTimeFrom(1)), 0)
    '            Alert1 = "Y"
    '        End If
    '        If Time1.ToString("HH:mm", New CultureInfo("en-US")) > SmsTime(1) Then
    '            Time1 = New DateTime(Time1.Year, Time1.Month, Time1.Day, CInt(SmsTimeTo(0)), CInt(SmsTimeTo(1)), 0)
    '            Alert1 = "Y"
    '        End If

    '        Dim Time2 As DateTime = DateAdd(DateInterval.Minute, (0 - BeforeHours2), StartSlot)
    '        Dim Alert2 As String = "N"
    '        If Time2.ToString("HH:mm", New CultureInfo("en-US")) < SmsTime(0) Then
    '            Time2 = New DateTime(Time2.Year, Time2.Month, Time2.Day, CInt(SmsTimeFrom(0)), CInt(SmsTimeFrom(1)), 0)
    '            Alert2 = "Y"
    '        End If
    '        If Time2.ToString("HH:mm", New CultureInfo("en-US")) > SmsTime(1) Then
    '            Time2 = New DateTime(Time2.Year, Time2.Month, Time2.Day, CInt(SmsTimeTo(0)), CInt(SmsTimeTo(1)), 0)
    '            Alert2 = "Y"
    '        End If

    '        Dim jLnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
    '        jLnq.SHOP_ID = ShopID
    '        jLnq.MOBILE_NO = MobileNo
    '        jLnq.APPOINTMENT_TIME = StartSlot
    '        jLnq.APPOINTMENT_CHANNEL = AppointmentChannel
    '        jLnq.SMS_TIME1 = Time1
    '        If StartSlot.Date = Today.Date Then
    '            jLnq.SMS_ALERT1 = "Y"   'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง SMS ล่วงหน้า 1 วัน
    '        Else
    '            jLnq.SMS_ALERT1 = Alert1
    '        End If

    '        Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
    '        Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShopID, "Appointment.CreateAppointmentNotifyJoblist")
    '        jLnq.SMS_MSG1 = CreateNotifyMsg1Day(StartSlot, PreferLang.Trim, shTrans, shLnq)
    '        jLnq.SMS_TIME2 = Time2
    '        jLnq.SMS_ALERT2 = Alert2
    '        jLnq.SMS_MSG2 = CreateNotifyMsg30Min(StartSlot, PreferLang.Trim, shTrans, shLnq)

    '        If CustomerEmail.Trim <> "" Then
    '            jLnq.CUSTOMER_EMAIL = CustomerEmail
    '            jLnq.EMAIL_TIME1 = Time1
    '            If StartSlot.Date = Today.Date Then
    '                jLnq.EMAIL_ALERT1 = "Y"     'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง Mail ล่วงหน้า 1 วัน
    '            Else
    '                'jLnq.EMAIL_ALERT1 = Alert1
    '                jLnq.EMAIL_ALERT1 = "Y"
    '            End If
    '            jLnq.EMAIL_MSG1 = CreateNotifyMsg1Day(StartSlot, PreferLang.Trim, shTrans, shLnq)
    '            jLnq.EMAIL_TIME2 = Time2
    '            'jLnq.EMAIL_ALERT2 = Alert2
    '            jLnq.EMAIL_ALERT2 = "Y"
    '            jLnq.EMAIL_MSG2 = CreateNotifyMsg30Min(StartSlot, PreferLang.Trim, shTrans, shLnq)
    '        End If

    '        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '        If trans.Trans IsNot Nothing Then
    '            If jLnq.InsertData("AppoinementENG.CreateNotifyJob", trans.Trans) = True Then
    '                trans.CommitTransaction()
    '            Else
    '                trans.RollbackTransaction()
    '                FunctionEng.SaveErrorLog("AppointmentENG.CreateAppointmentNotifyJoblist", jLnq.ErrorMessage)
    '            End If
    '        End If
    '    End Sub

    '    Private Function CreateNotifyMsg30Min(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As Cen.TbShopCenLinqDB) As String
    '        Dim ret As String = ""
    '        Dim ShopName As String = ""
    '        If InStr(PreferLang.ToUpper, "THAI") > 0 Then
    '            ShopName = FunctionEng.GetShopConfig("s_name_th", shTrans)
    '            ret += "คุณมีนัดรับบริการที่ " & ShopName & " ในอีก 30 นาที"
    '            ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " นาที ขอบคุณค่ะ"
    '        Else
    '            ShopName = FunctionEng.GetShopConfig("s_name_en", shTrans)
    '            ret += "You have an appointment at " & ShopName
    '            ret += " in 30-minute time."
    '            ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " mins before the appointment. Thank you."
    '        End If

    '        Return ret
    '    End Function

    '    Private Function CreateNotifyMsg1Day(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As Cen.TbShopCenLinqDB) As String
    '        Dim ret As String = ""
    '        Dim ShopName As String = ""
    '        If InStr(PreferLang.ToUpper, "THAI") > 0 Then
    '            ShopName = shLnq.SHOP_NAME_TH 'FunctionEng.GetShopConfig("s_name_th", shTrans)
    '            ret += "พรุ่งนี้คุณมีนัดที่ " & ShopName
    '            ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น."
    '            ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " นาที ขอบคุณค่ะ"
    '        Else
    '            ShopName = shLnq.SHOP_NAME_EN 'FunctionEng.GetShopConfig("s_name_en", shTrans)
    '            ret += "You have appointment at " & ShopName
    '            ret += " tomorrow at " & AppointmentTime.ToString("HH:mm")
    '            ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " mins before the appointment time. Thank you."
    '        End If

    '        Return ret
    '    End Function

    '    Private Function SendSMSConfirm(ByVal MobileNo As String, ByVal ServiceID As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As CenParaDB.GateWay.SMSResponsePara
    '        Dim gw As New GateWayServiceENG
    '        Dim ret As New CenParaDB.GateWay.SMSResponsePara
    '        ret = gw.SendSMS(MobileNo, CreateSMSConfirm(ServiceID, MobileNo, AppointmentTime, PreLang, ShLnq))
    '        gw = Nothing
    '        Return ret
    '    End Function

    '    Private Function CreateSMSConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
    '        Dim trans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShLnq.ID, "Engine.AppointmentUpdateServiceENG.CreateSMSConfirm")
    '        Dim ServiceName As String = ""
    '        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
    '        Dim sDt As New DataTable
    '        sDt = sLnq.GetDataList("id in (" & ServiceID & ")", "item_time,item_wait,item_order", trans.Trans)
    '        For Each sDr As DataRow In sDt.Rows
    '            If InStr(PreLang.ToUpper(), "THAI") > 0 Then
    '                If ServiceName = "" Then
    '                    ServiceName += sDr("ITEM_NAME_TH").ToString
    '                Else
    '                    ServiceName += "," & sDr("ITEM_NAME_TH").ToString
    '                End If
    '            Else
    '                If ServiceName = "" Then
    '                    ServiceName += sDr("ITEM_NAME").ToString
    '                Else
    '                    ServiceName += "," & sDr("ITEM_NAME").ToString
    '                End If
    '            End If
    '        Next
    '        sLnq = Nothing
    '        sDt.Dispose()

    '        Dim ret As String = ""
    '        Dim ShopName As String = ""
    '        Dim TimeBefore As String = DateAdd(DateInterval.Hour, 0 - Convert.ToDouble(FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")), AppointmentTime).ToString("HH:mm")
    '        If InStr(PreLang.ToUpper(), "THAI") > 0 Then
    '            ShopName = ShLnq.SHOP_NAME_TH 'FunctionEng.GetShopConfig("s_name_th", trans)
    '            ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " " & AppointmentTime.ToString("dddd dMMMyy", New System.Globalization.CultureInfo("th-TH"))
    '            ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. หากเปลี่ยน/ยกเลิก กรุณาแจ้ง"
    '            ret += "ภายในเวลา" & TimeBefore & "น."
    '        Else
    '            'Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(FunctionEng.GetShopConfig("k_cancel", trans)), AppointmentTime).ToString("HH:mmtt")
    '            ShopName = ShLnq.SHOP_NAME_EN 'FunctionEng.GetShopConfig("s_name_en", trans)
    '            ret += "You have an appointment for " & ServiceName
    '            ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy, HH:mm", New System.Globalization.CultureInfo("en-US")) & "."
    '            ret += " To change/cancel,please make changes " & TimeBefore & " in advance."
    '        End If
    '        trans.CommitTransaction()

    '        Return ret
    '    End Function

    '    Private Function AddTimeSlotCapacity(ByVal ChooseService As String, ByVal SlotDateTime As DateTime, ByVal ShopID As Long) As Boolean
    '        Dim res As Boolean = False
    '        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '        If trans.Trans IsNot Nothing Then
    '            Dim sql As String = ""
    '            Dim dt As New DataTable
    '            Dim AllService() As String = Split(ChooseService, ",")
    '            Dim CountService As Int32 = AllService.Length
    '            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
    '            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
    '            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "AppointmentUpdateShopServiceENG.AddTimeSlotCapacity")
    '            If shTrans.Trans IsNot Nothing Then
    '                Dim kBeforeClose As Double = FunctionEng.GetShopConfig("k_before_close", shTrans)
    '                Dim StrDateTime As String = SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
    '                Dim slSql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & StrDateTime & "') = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & StrDateTime & "',app_date)=0"
    '                Dim slLnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
    '                Dim slDT As DataTable = slLnq.GetListBySql(slSql, shTrans.Trans)
    '                If slDT.Rows.Count > 0 Then
    '                    Dim Slot As Long = slDT.Rows(0)("slot")
    '                    Dim lnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
    '                    Dim whText As String = "DATEDIFF(d,app_date,'" & StrDateTime & "') = 0 and in_use < capacity and CONVERT(varchar(5),slot_time,114) between '" & SlotDateTime.ToString("HH:mm") & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Minute.ToString.PadLeft(2, "0") & "'"
    '                    dt = lnq.GetDataList(whText, "", shTrans.Trans)
    '                    If dt.Rows.Count = CountService Then
    '                        'Dim ret As Boolean = False
    '                        For j As Int32 = 0 To dt.Rows.Count - 1
    '                            Dim asLnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
    '                            asLnq.GetDataByPK(dt.Rows(j).Item("id"), shTrans.Trans)
    '                            asLnq.IN_USE = asLnq.IN_USE + 1
    '                            res = asLnq.UpdateByPK(0, shTrans.Trans)
    '                            If res = False Then
    '                                Exit For
    '                            End If
    '                        Next
    '                    Else
    '                        res = False
    '                    End If
    '                    If res = True Then
    '                        shTrans.CommitTransaction()
    '                    Else
    '                        shTrans.RollbackTransaction()
    '                    End If
    '                End If
    '            End If
    '        End If

    '        Return res
    '    End Function

    '    Private Function GetActiveSiebelActivity(ByVal MobileNo As String) As String
    '        Dim ret As String = ""

    '        Dim trans As New TransactionDB
    '        Dim lnq As New Cen.TbShopCenLinqDB
    '        Dim dt As DataTable = lnq.GetDataList("active='Y'", "", trans.Trans)
    '        trans.CommitTransaction()
    '        If dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))
    '                Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmnetENG.GetActiveSiebelActivity")
    '                'shTrans.CreateTransaction(cenShLnq.SHOP_DB_SERVER, cenShLnq.SHOP_DB_USERID, cenShLnq.SHOP_DB_PWD, cenShLnq.SHOP_DB_NAME)
    '                If shTrans.Trans IsNot Nothing Then
    '                    Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
    '                    Dim sqlRs As String = "select distinct  ac.customer_id mobile_no, ac.siebel_activity_id, ac.siebel_status, ac.siebel_desc"
    '                    sqlRs += " from TB_APPOINTMENT_CUSTOMER ac "
    '                    sqlRs += " inner join TB_ITEM s on s.id=ac.item_id"
    '                    sqlRs += " where CONVERT(varchar(16),ac.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
    '                    sqlRs += " and ac.customer_id = '" & MobileNo & "' "
    '                    sqlRs += " and ac.active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "

    '                    Dim tmp As New DataTable
    '                    tmp = shLinq.GetListBySql(sqlRs, shTrans.Trans)
    '                    shTrans.CommitTransaction()
    '                    If tmp.Rows.Count > 0 Then
    '                        With tmp.Rows(0)
    '                            If Convert.IsDBNull(.Item("siebel_activity_id")) = False Then ret = .Item("siebel_activity_id")
    '                            If Convert.IsDBNull(.Item("siebel_status")) = False Then ret += "##" & .Item("siebel_status")
    '                            If Convert.IsDBNull(.Item("siebel_desc")) = False Then ret += "##" & .Item("siebel_desc")
    '                        End With
    '                    End If
    '                    tmp.Dispose()
    '                    shLinq = Nothing
    '                End If
    '                cenShLnq = Nothing
    '            Next
    '        End If
    '        dt.Dispose()
    '        lnq = Nothing

    '        Return ret
    '    End Function

    '    Private Function DeleteAppointment(ByVal MobileNo As String) As Boolean
    '        Dim ret As Boolean = False
    '        Dim sql As String = "select distinct aj.shop_abb "
    '        sql += " from TB_APPOINTMENT_JOB aj"
    '        sql += " where CONVERT(varchar(16),aj.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
    '        sql += " and aj.mobile_no = '" & MobileNo & "' "
    '        sql += " and aj.active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "

    '        Dim dt As New DataTable
    '        Dim lnq As New Cen.TbAppointmentJobCenLinqDB
    '        dt = lnq.GetListBySql(sql, Nothing)
    '        If dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDBByShopAbb(dr("shop_abb"))

    '                Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.DeleteAppointment")
    '                Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB

    '                Dim CancelHour As Long = FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")
    '                Dim CancelBefore As DateTime = DateAdd(DateInterval.Hour, CancelHour, DateTime.Now)
    '                Dim whText As String = "CONVERT(varchar(16),start_slot,120) >= '" & CancelBefore.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
    '                whText += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "
    '                whText += " and customer_id = '" & MobileNo & "' "

    '                ret = shLinq.ChkDataByWhere(whText, shTrans.Trans)
    '                If ret = True Then
    '                    Dim StartSlot As String = shLinq.START_SLOT.Value.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US"))

    '                    Dim trans As New TransactionDB
    '                    'Delete Notify ก่อน
    '                    Dim DelWh As String = ""
    '                    DelWh += " delete from tb_notify_joblist "
    '                    DelWh += " where shop_id='" & cenShLnq.ID & "' "
    '                    DelWh += " and mobile_no = '" & MobileNo & "'"
    '                    DelWh += " and convert(varchar(16),appointment_time, 120) = '" & StartSlot & "'"
    '                    SqlDB.ExecuteNonQuery(DelWh, trans.Trans)

    '                    Dim ChooseService As String = GetChooseService(MobileNo, StartSlot, Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment, shTrans)
    '                    Dim uSql As String = "delete from tb_appointment_customer "
    '                    uSql += " where customer_id = '" & MobileNo & "' "
    '                    uSql += " and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "' "
    '                    ret = (SqlDB.ExecuteNonQuery(uSql, shTrans.Trans) > 0)

    '                    If shLinq.APPOINTMENT_JOB_ID > 0 Then
    '                        Dim dWh As String = "delete from tb_appointment_job where id='" & shLinq.APPOINTMENT_JOB_ID & "'"
    '                        SqlDB.ExecuteNonQuery(dWh, trans.Trans)
    '                        trans.CommitTransaction()
    '                    End If

    '                    If ret = True Then
    '                        If ClearTimeSlot(shLinq.START_SLOT.Value, cenShLnq.ID, ChooseService, shTrans) = True Then
    '                            shTrans.CommitTransaction()
    '                            Exit For
    '                        Else
    '                            ret = False
    '                            shTrans.RollbackTransaction()
    '                        End If
    '                    Else
    '                        shTrans.RollbackTransaction()
    '                    End If
    '                End If
    '            Next
    '        End If

    '        Return ret
    '    End Function


    '    Private Function ClearTimeSlot(ByVal SlotDateTime As DateTime, ByVal ShopID As Long, ByVal ChooseService As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
    '        Dim ret As Boolean = False

    '        Dim AllService() As String = Split(ChooseService, ",")
    '        Dim CountService As Int32 = AllService.Length
    '        If shTrans.Trans IsNot Nothing Then
    '            Dim kBeforeClose As Double = FunctionEng.GetShopConfig("k_before_close", shTrans)
    '            Dim slSql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "') = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "',app_date)=0"
    '            Dim slLnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
    '            Dim slDT As DataTable = slLnq.GetListBySql(slSql, shTrans.Trans)
    '            If slDT.Rows.Count > 0 Then
    '                Dim Slot As Long = slDT.Rows(0)("slot")
    '                Dim lnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
    '                Dim whText As String = "DATEDIFF(d,app_date,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "') = 0 and CONVERT(varchar(5),slot_time,114) between '" & SlotDateTime.ToString("HH:mm") & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Minute.ToString.PadLeft(2, "0") & "'"
    '                Dim dt As DataTable = lnq.GetDataList(whText, "", shTrans.Trans)
    '                If dt.Rows.Count > 0 Then
    '                    For Each dr As DataRow In dt.Rows
    '                        lnq = New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
    '                        lnq.GetDataByPK(dr("id"), shTrans.Trans)
    '                        lnq.IN_USE = lnq.IN_USE - 1
    '                        ret = lnq.UpdateByPK(0, shTrans.Trans)
    '                    Next
    '                End If
    '            End If
    '        End If

    '        Return ret
    '    End Function

    '    Private Function GetChooseService(ByVal MobileNo As String, ByVal StartDateTime As String, ByVal ActiveStatus As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
    '        Dim ret As String = ""

    '        If shTrans.Trans IsNot Nothing Then
    '            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
    '            Dim whText As String = "CONVERT(varchar(16),start_slot,120) = '" & StartDateTime & "' "
    '            whText += " and active_status = '" & ActiveStatus & "' "
    '            whText += " and customer_id = '" & MobileNo & "' "
    '            Dim dt As DataTable = lnq.GetDataList(whText, "", shTrans.Trans)
    '            If dt.Rows.Count > 0 Then
    '                For Each dr As DataRow In dt.Rows
    '                    If ret = "" Then
    '                        ret = dr("item_id")
    '                    Else
    '                        ret += "," & dr("item_id")
    '                    End If
    '                Next
    '            End If
    '        End If

    '        Return ret
    '    End Function

    '#End Region




End Class
