Imports System.Globalization
Imports Engine.GateWay
Imports CenParaDB.Common.Utilities
Imports Engine.Common
Imports System.Windows.Forms

Public Class AppointmentNotifyENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox
    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    Public Sub SetTextLog(ByVal txtLog As TextBox)
        _TXT_LOG = txtLog
    End Sub

    Dim _OldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If _OldLog <> LogDesc Then
            _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
            Application.DoEvents()
        End If
    End Sub


    Public Sub SendNotify(ByVal lblTime As Label)
        Try
            Dim dt As DataTable = FunctionEng.GetActiveShop()
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))
                    'กรณีจองข้ามวัน ให้มีการแจ้งเตือนล่วงหน้า 1 วัน (ตาม Config แล้วมันไป Config ที่ไหนวะ ???)
                    CreateNotifyJob(shLnq.ID, 24, 30, shLnq, lblTime)
                Next

                'เวลาที่ให้ทำการส่งข้อความคือ 08.00-17.00 น.
                For Each dr As DataRow In dt.Rows
                    'Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))
                    SendNotifyJob("11")
                    SendNotifyJob("12")
                    SendNotifyJob("21")
                    SendNotifyJob("22")
                Next
            Else
                'PrintLog("SendNotifyENG.SendNotify : " & "No Shop")
                FunctionEng.SaveErrorLog("AppointmentENG.SendNotify", "No Shop", Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("AppointmentENG.SendNotify", "Exception :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
        End Try
        
    End Sub

    Private Sub SendNotifyJob(ByVal JobType As String)
        'JobType
        '11=สำหรับการส่ง SMS ครั้งที่1
        '12=สำหรับการส่ง SMS ครั้งที่2
        '21=สำหรับการส่ง EMAIL ครั้งที่1
        '22=สำหรับการส่ง EMAIL ครั้งที่2
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim whText As String = ""
                Dim fldName As String = ""
                If JobType = "11" Then
                    'กรณีส่งล่วงหน้า 1 วัน ไม่ต้องส่ง SMS กรณีจองที่ Kiosk
                    whText = "sms_alert1 = 'N' and convert(varchar(16),sms_time1,120)<='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New CultureInfo("en-US")) & "' "
                    whText += " and appointment_channel not in ('" & Constant.TbAppointmentCustomer.AppointmentChannel.Kiosk & "')"
                    fldName = "sms_msg1"
                ElseIf JobType = "12" Then
                    whText = "sms_alert2 = 'N' and convert(varchar(16),sms_time2,120)<='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New CultureInfo("en-US")) & "' "
                    fldName = "sms_msg2"
                ElseIf JobType = "21" Then
                    whText = "email_alert1 = 'N' and convert(varchar(16),email_time1,120)<='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New CultureInfo("en-US")) & "' "
                    whText += " and appointment_channel not in ('" & Constant.TbAppointmentCustomer.AppointmentChannel.Kiosk & "')"
                    fldName = "email_msg1"
                ElseIf JobType = "22" Then
                    whText = "email_alert2 = 'N' and convert(varchar(16),email_time2,120)<='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New CultureInfo("en-US")) & "' "
                    whText += " and appointment_channel not in ('" & Constant.TbAppointmentCustomer.AppointmentChannel.Kiosk & "')"
                    fldName = "email_msg2"
                End If

                Dim lnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
                Dim dt As DataTable = lnq.GetDataList(whText, "", trans.Trans)
                trans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim aDr As DataRow = dt.Rows(i)
                        If CheckQueueRegister(aDr("mobile_no"), aDr("appointment_time"), aDr("shop_id"), aDr("id")) = True Then
                            Continue For
                        End If

                        Dim gwEng As New GateWayServiceENG
                        Select Case JobType
                            Case "11", "12"
                                Dim res As New CenParaDB.GateWay.SMSResponsePara
                                res = gwEng.SendSMS(aDr("mobile_no"), aDr(fldName))
                                If res.RESULT = True Then
                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim jLnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
                                    jLnq.GetDataByPK(aDr("id"), trans.Trans)
                                    If JobType = "11" Then
                                        jLnq.SMS_ALERT1 = "Y"
                                    ElseIf JobType = "12" Then
                                        jLnq.SMS_ALERT2 = "Y"
                                    End If
                                    Dim ret As Boolean = jLnq.UpdateByPK("SendNotifyENG.SendNotifyJob", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        _err = "Mobile No :" & aDr("mobile_no") & " " & jLnq.ErrorMessage
                                        FunctionEng.SaveErrorLog("AppointmentENG.SendNotifyJob", _err, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
                                        'PrintLog(_err)
                                    End If
                                    jLnq = Nothing
                                Else
                                    PrintLog("Mobile:" & aDr("mobile_no") & res.ERROR_RESPONSE)
                                End If
                            Case "21", "22"
                                If aDr("customer_email").ToString.Trim <> "" Then
                                    Dim resM As Boolean = gwEng.SendEmail(aDr("customer_email"), "Appointment Alert", aDr(fldName))
                                    If resM = True Then
                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim jLnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
                                        jLnq.GetDataByPK(aDr("id"), trans.Trans)
                                        If JobType = "21" Then
                                            jLnq.EMAIL_ALERT1 = "Y"
                                        ElseIf JobType = "22" Then
                                            jLnq.EMAIL_ALERT2 = "Y"
                                        End If

                                        Dim ret As Boolean = jLnq.UpdateByPK("SendNotifyENG.SendNotifyJob", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            _err = "Mobile No :" & aDr("mobile_no") & " eMail: " & aDr("customer_email").ToString & " " & jLnq.ErrorMessage
                                            FunctionEng.SaveErrorLog("AppointmentENG.SendNotifyJob", _err, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
                                            'PrintLog(_err)
                                        End If
                                        jLnq = Nothing
                                    Else
                                        'PrintLog()
                                        FunctionEng.SaveErrorLog("AppointmentENG.SendNotifyJob", "Cannot send e-mail to " & aDr("customer_email").ToString, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
                                    End If
                                End If
                        End Select
                        gwEng = Nothing
                    Next
                    dt.Dispose()
                End If
            Else
                'PrintLog("SendNotifyJob.SendNotifyENG :" & trans.ErrorMessage)
                FunctionEng.SaveErrorLog("AppointmentENG.SendNotifyJob", trans.ErrorMessage, "\ErrorLog\", "AppointmentNotifyENG.SendNotify")
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("AppointmentENG.SendNotifyJob", "Exception :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
        End Try
    End Sub

    Private Function CheckQueueRegister(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ShopID As Long, ByVal NotifyID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(ShopID, "SendNotifyENG.CheckQueueRegister")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                Dim sqlRs As String = "select top 1 id "
                sqlRs += " from TB_COUNTER_QUEUE "
                sqlRs += " where customer_id='" & MobileNo & "' "
                sqlRs += " and convert(varchar(16),hold,120)='" & StartSlot.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "'"

                Dim dtTmp As DataTable = lnq.GetListBySql(sqlRs, shTrans.Trans)
                shTrans.CommitTransaction()
                lnq = Nothing
                If dtTmp.Rows.Count > 0 Then
                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Dim nLnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
                    If nLnq.DeleteByPK(NotifyID, trans.Trans) = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                    End If
                    nLnq = Nothing

                    ret = True
                End If
                dtTmp.Dispose()
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("AppointmentENG.CheckQueueRegister", "Exception :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
        End Try
        Return ret
    End Function

    Private Function NotifyMsg1Day(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim ShopName As String = ""
        If InStr(PreferLang.ToUpper, "THAI") > 0 Then
            ShopName = FunctionEng.GetShopConfig("s_name_th", shLnq)
            ret += "พรุ่งนี้คุณมีนัดที่ " & ShopName
            ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น."
            ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shLnq)) & " นาที ขอบคุณค่ะ"
        Else
            ShopName = FunctionEng.GetShopConfig("s_name_en", shLnq)
            ret += "You have appointment at " & ShopName
            ret += " tomorrow at " & AppointmentTime.ToString("HH:mm")
            ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shLnq)) & " mins before the appointment time. Thank you."
        End If

        Return ret
    End Function

    Private Function NotifyMsg30Min(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim ShopName As String = ""
        If InStr(PreferLang.ToUpper, "THAI") > 0 Then
            ShopName = FunctionEng.GetShopConfig("s_name_th", shLnq)
            ret += "คุณมีนัดรับบริการที่ " & ShopName & " ในอีก 30 นาที"
            ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shLnq)) & " นาที ขอบคุณค่ะ"
        Else
            ShopName = FunctionEng.GetShopConfig("s_name_en", shLnq)
            ret += "You have an appointment at " & ShopName
            ret += " in 30-minute time."
            ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shLnq)) & " mins before the appointment. Thank you."
        End If

        Return ret
    End Function

    Private Function CreateNotifySMS1Day(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Return NotifyMsg1Day(AppointmentTime, PreferLang, shLnq)
    End Function

    Private Function CreateNotifySMS30Min(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Return NotifyMsg30Min(AppointmentTime, PreferLang, shLnq)
    End Function

    Private Sub CreateNotifyJob(ByVal ShopID As Long, ByVal BeforeHours1 As Double, ByVal BeforeHours2 As Double, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal lblTime As Label)
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim retTmp As Boolean = True
                Dim errTmp As String = ""
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq, "SendNotifyENG.CreateNotifyJob")
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                    Dim sqlRs As String = "select distinct CONVERT(date,app_date,120) app_date ,"
                    sqlRs += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel, customer_email"
                    sqlRs += " from TB_APPOINTMENT_CUSTOMER "
                    sqlRs += " where active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "
                    'sqlRs += " and appointment_channel not in ('" & Constant.TbAppointmentCustomer.AppointmentChannel.Kiosk & "')"

                    Dim dtTmp As DataTable = lnq.GetListBySql(sqlRs, shTrans.Trans)
                    lnq = Nothing
                    If dtTmp.Rows.Count > 0 Then
                        Dim tmpAppDate As Date = New Date(1, 1, 1)
                        Dim tmpMobileNO As String = ""
                        Dim tmpStartSlot As DateTime = New DateTime(1, 1, 1)

                        For Each drTmp As DataRow In dtTmp.Rows
                            If ChkNotifyJob(drTmp("customer_id"), Convert.ToDateTime(drTmp("start_slot")).ToString("yyyy-MM-dd HH:mm", New CultureInfo("en-US")), trans) = False Then
                                If Convert.ToDateTime(drTmp("app_date")) <> tmpAppDate And drTmp("customer_id") <> tmpMobileNO And Convert.ToDateTime(drTmp("start_slot")) <> tmpStartSlot Then
                                    Dim SmsTime() As String = Split(FunctionEng.GetQisDBConfig("AppointmentSMSNotifyTime"), "-")
                                    Dim SmsTimeFrom() As String = Split(SmsTime(0), ":")
                                    Dim SmsTimeTo() As String = Split(SmsTime(1), ":")

                                    Dim Time1 As DateTime = DateAdd(DateInterval.Hour, (0 - BeforeHours1), Convert.ToDateTime(drTmp("start_slot")))
                                    Dim Alert1 As String = "N"
                                    If Time1.ToString("HH:mm", New CultureInfo("en-US")) < SmsTime(0) Then
                                        Time1 = New DateTime(Time1.Year, Time1.Month, Time1.Day, CInt(SmsTimeFrom(0)), CInt(SmsTimeFrom(1)), 0)
                                        Alert1 = "Y"
                                    End If
                                    If Time1.ToString("HH:mm", New CultureInfo("en-US")) > SmsTime(1) Then
                                        Time1 = New DateTime(Time1.Year, Time1.Month, Time1.Day, CInt(SmsTimeTo(0)), CInt(SmsTimeTo(1)), 0)
                                        Alert1 = "Y"
                                    End If

                                    Dim Time2 As DateTime = DateAdd(DateInterval.Minute, (0 - BeforeHours2), Convert.ToDateTime(drTmp("start_slot")))
                                    Dim Alert2 As String = "N"
                                    If Time2.ToString("HH:mm", New CultureInfo("en-US")) < SmsTime(0) Then
                                        Time2 = New DateTime(Time2.Year, Time2.Month, Time2.Day, CInt(SmsTimeFrom(0)), CInt(SmsTimeFrom(1)), 0)
                                        Alert2 = "Y"
                                    End If
                                    If Time2.ToString("HH:mm", New CultureInfo("en-US")) > SmsTime(1) Then
                                        Time2 = New DateTime(Time2.Year, Time2.Month, Time2.Day, CInt(SmsTimeTo(0)), CInt(SmsTimeTo(1)), 0)
                                        Alert2 = "Y"
                                    End If

                                    Dim jLnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
                                    jLnq.SHOP_ID = shLnq.ID
                                    jLnq.MOBILE_NO = drTmp("customer_id")
                                    jLnq.APPOINTMENT_TIME = drTmp("start_slot")
                                    jLnq.APPOINTMENT_CHANNEL = drTmp("appointment_channel")
                                    jLnq.SMS_TIME1 = Time1
                                    If Convert.ToDateTime(drTmp("start_slot")).Date = Today.Date Then
                                        jLnq.SMS_ALERT1 = "Y"   'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง SMS ล่วงหน้า 1 วัน
                                    Else
                                        jLnq.SMS_ALERT1 = Alert1
                                    End If

                                    Dim cPara As New CenParaDB.TABLE.TbCustomerCenParaDB
                                    Dim gw As New GateWayServiceENG
                                    cPara = gw.GetCustomerProfile(drTmp("customer_id"))

                                    jLnq.SMS_MSG1 = CreateNotifySMS1Day(drTmp("start_slot"), cPara.PREFER_LANG.Trim, shLnq)
                                    jLnq.SMS_TIME2 = Time2
                                    jLnq.SMS_ALERT2 = Alert2
                                    jLnq.SMS_MSG2 = CreateNotifySMS30Min(drTmp("start_slot"), cPara.PREFER_LANG.Trim, shLnq)

                                    jLnq.CUSTOMER_EMAIL = drTmp("customer_email").ToString
                                    jLnq.EMAIL_TIME1 = Time1
                                    If Convert.ToDateTime(drTmp("start_slot")).Date = Today.Date Then
                                        jLnq.EMAIL_ALERT1 = "Y"     'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง Mail ล่วงหน้า 1 วัน
                                    Else
                                        'jLnq.EMAIL_ALERT1 = Alert1
                                        jLnq.EMAIL_ALERT1 = "Y"
                                    End If
                                    jLnq.EMAIL_MSG1 = CreateNotifyEMAIL1Day(drTmp("start_slot"), cPara.PREFER_LANG.Trim, shLnq)
                                    jLnq.EMAIL_TIME2 = Time2
                                    'jLnq.EMAIL_ALERT2 = Alert2
                                    jLnq.EMAIL_ALERT2 = "Y"
                                    jLnq.EMAIL_MSG2 = CreateNotifyEMAIL30Min(drTmp("start_slot"), cPara.PREFER_LANG.Trim, shLnq)

                                    retTmp = jLnq.InsertData("SendNotifyENG.CreateNotifyJob", trans.Trans)

                                    cPara = Nothing
                                    gw = Nothing
                                    If retTmp = False Then
                                        errTmp = "MobileNo : " & drTmp("customer_id") & " " & jLnq.ErrorMessage
                                        Exit For
                                    End If
                                End If
                                tmpAppDate = drTmp("app_date")
                                tmpMobileNO = drTmp("customer_id")
                                tmpStartSlot = drTmp("start_slot")
                            End If
                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                            Application.DoEvents()
                        Next
                        dtTmp.Dispose()
                    End If
                    shTrans.CommitTransaction()
                End If
                If retTmp = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("SendNotifyENG.CreateNotifyJob", errTmp, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
                    'PrintLog(errTmp)
                End If
            Else
                PrintLog("SendNotifyJob.CreateNotifyJob :" & trans.ErrorMessage)
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("SendNotifyENG.CreateNotifyJob", "Exception : " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "AppointmentNotifyENG")
        End Try
    End Sub

    Private Function ChkNotifyJob(ByVal MobileNo As String, ByVal StartSlot As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean = False
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("mobile_no='" & MobileNo & "' and convert(varchar(16),appointment_time,120) = '" & StartSlot & "' ", "", trans.Trans)
            If dt.Rows.Count > 0 Then
                ret = True
            End If
        Else
            PrintLog("SendNotifyJob.ChkNotifyJob :" & trans.ErrorMessage)
        End If
        Return ret
    End Function

    Private Function GetNotifyJobList(ByVal JobType As String, ByVal BeforeHour As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As String
        'JobType
        '11=สำหรับการส่ง SMS ครั้งที่1
        '12=สำหรับการส่ง SMS ครั้งที่2
        '21=สำหรับการส่ง EMAIL ครั้งที่1
        '22=สำหรับการส่ง EMAIL ครั้งที่2

        Dim ret As String = ""
        Dim TimeBefore As String = DateAdd(DateInterval.Hour, BeforeHour, DateTime.Now).ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
        Dim whText As String = ""
        If JobType = "11" Then
            whText = "sms_alert1 = 'N' and convert(varchar(16),sms_time1,120)<='" & TimeBefore & "' "
        ElseIf JobType = "12" Then
            whText = "sms_alert2 = 'N' and convert(varchar(16),sms_time2,120)<='" & TimeBefore & "' "
        ElseIf JobType = "21" Then
            whText = "email_alert1 = 'N' and convert(varchar(16),email_time1,120)<='" & TimeBefore & "' "
        ElseIf JobType = "22" Then
            whText = "email_alert2 = 'N' and convert(varchar(16),email_time2,120)<='" & TimeBefore & "' "
        End If

        Dim lnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(whText, "", trans.Trans)
        If dt.Rows.Count > 0 Then
            Dim tmp As String = ""
            ret += " and customer_id not in ("
            For Each dr As DataRow In dt.Rows
                If tmp = "" Then
                    tmp += "'" & dr("mobile_no") & "'"
                Else
                    tmp += "," & "'" & dr("mobile_no") & "'"
                End If
            Next
            ret += tmp & ")"
        End If
        Return ret
    End Function


    Private Function GetAppointmentData(ByVal ShopID As Long, ByVal BeforeHours As Double, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("app_date", GetType(Date))
        dt.Columns.Add("customer_id")
        dt.Columns.Add("start_slot", GetType(DateTime))
        dt.Columns.Add("appointment_channel")
        dt.Columns.Add("ServiceID")
        dt.Columns.Add("ServiceNameTH")
        dt.Columns.Add("ServiceNameEN")
        dt.Columns.Add("prefer_lang")
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq, "SendNotifyENG.GetAppointmentData")
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                    Dim ChkTime As DateTime = DateAdd(DateInterval.Hour, BeforeHours, DateTime.Now)
                    Dim dtTmp As DataTable = lnq.GetDataList("convert(varchar(16), start_slot,120)='" & ChkTime.ToString("yyyy-MM-dd HH:mm", New CultureInfo("en-US")) & "' and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'", "", shTrans.Trans)
                    If dtTmp.Rows.Count > 0 Then
                        Dim tmpAppDate As Date = New Date(1, 1, 1)
                        Dim tmpMobileNO As String = ""
                        Dim tmpStartSlot As DateTime = New DateTime(1, 1, 1)

                        For Each drTmp As DataRow In dtTmp.Rows
                            Dim lnqItem As ShParaDB.TABLE.TbItemShParaDB = FunctionEng.GetItem(drTmp("item_id"), shTrans)
                            If Convert.ToDateTime(drTmp("app_date")) <> tmpAppDate And drTmp("customer_id") <> tmpMobileNO And Convert.ToDateTime(drTmp("start_slot")) <> tmpStartSlot Then
                                Dim dr As DataRow = dt.NewRow
                                dr("app_date") = drTmp("app_date")
                                dr("customer_id") = drTmp("customer_id")
                                dr("start_slot") = drTmp("start_slot")
                                dr("appointment_channel") = drTmp("appointment_channel")
                                dr("ServiceID") = drTmp("item_id")
                                dr("ServiceNameTH") = lnqItem.ITEM_NAME_TH
                                dr("ServiceNameEN") = lnqItem.ITEM_NAME
                                dr("prefer_lang") = FunctionEng.GetQisDBCustomer(drTmp("customer_id"), trans).PREFER_LANG.Trim
                                dt.Rows.Add(dr)
                            Else
                                dt.Rows(dt.Rows.Count - 1)("ServiceID") += "," & drTmp("item_id")
                                dt.Rows(dt.Rows.Count - 1)("ServiceNameTH") += "," & lnqItem.ITEM_NAME_TH
                                dt.Rows(dt.Rows.Count - 1)("ServiceNameEN") += "," & lnqItem.ITEM_NAME
                            End If
                            tmpAppDate = drTmp("app_date")
                            tmpMobileNO = drTmp("customer_id")
                            tmpStartSlot = drTmp("start_slot")
                        Next
                    End If
                    shTrans.CommitTransaction()
                End If
                trans.CommitTransaction()
            Else
                PrintLog("SendNotifyJob.GetAppointmentData :" & trans.ErrorMessage)
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("AppointmentNotifyENG.GetAppointmentData", "Exception :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "GetAppointmentData")
        End Try
        Return dt
    End Function

    Private Function CreateNotifyEMAIL1Day(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Return NotifyMsg1Day(AppointmentTime, PreferLang, shLnq)
    End Function
    Private Function CreateNotifyEMAIL30Min(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Return NotifyMsg30Min(AppointmentTime, PreferLang, shLnq)
    End Function

End Class
