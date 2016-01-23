Imports CenLinqDB.TABLE
Imports CenParaDB.TABLE
Imports shT = ShLinqDB.TABLE
Imports shP = ShParaDB.TABLE
Imports ShParaDB.ShopWebService
Imports System.Web
Imports System.Net
Imports System.Text
Imports System.IO
Imports Engine.Common
Imports CenLinqDB.Common.Utilities
Imports Cen = CenLinqDB.TABLE
Imports CenParaDB.Common.Utilities
Imports System.Threading
Imports System.Windows.Forms

Public Class AppointmentGenSiebelENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox

    Public Sub SetTextLog(ByVal txtLog As TextBox)
        _TXT_LOG = txtLog
    End Sub

#Region "Create Siebel Activity"
    'Public Sub GenSiebelActivity()
    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    If trans.Trans IsNot Nothing Then
    '        Dim csLnq As New Cen.TbShopCenLinqDB
    '        Dim dt As DataTable = FunctionEng.GetActiveShop()
    '        If dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                Application.DoEvents()
    '                Dim dr As DataRow = dt.Rows(i)
    '                'ไปหาข้อมูลเกี่ยวกับการจองใน Shop
    '                Dim shLnq As TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))
    '                CreateSiebelActivity(trans, shLnq)
    '            Next
    '        End If
    '        trans.CommitTransaction()
    '    End If
    'End Sub

    'Private Sub CreateSiebelActivity(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    '    Dim retTrans As Boolean = False
    '    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
    '    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GetSiegelENG.CreateSiebelActivity")
    '    If shTrans.Trans IsNot Nothing Then
    '        Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
    '        Dim sqlRs As String = "select distinct CONVERT(date,app_date,120) app_date ,"
    '        sqlRs += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel"
    '        sqlRs += " from TB_APPOINTMENT_CUSTOMER "
    '        sqlRs += " where siebel_activity_id is null and siebel_desc is  null "
    '        sqlRs += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "

    '        Dim dt As DataTable = lnq.GetListBySql(sqlRs, shTrans.Trans)
    '        shTrans.CommitTransaction()
    '        If dt.Rows.Count > 0 Then
    '            'FunctionEng.SaveTransLog("GenSiebelENG.CreateSiebelActivity", "Start Create SiebelActivity For Shop " & shLnq.SHOP_NAME_EN)
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GetSiegelENG.CreateSiebelActivity")
    '                If shTrans.Trans IsNot Nothing Then
    '                    Dim re As Boolean = False
    '                    Dim ret As New SiebelResponsePara
    '                    Dim dr As DataRow = dt.Rows(i)
    '                    Dim SubCat As String = ""
    '                    If dr("appointment_channel") = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatID Then
    '                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatName
    '                    ElseIf dr("appointment_channel") = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatID Then
    '                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatName
    '                    ElseIf dr("appointment_channel") = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallInbound.CatID Then
    '                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallInbound.CatName
    '                    ElseIf dr("appointment_channel") = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatID Then
    '                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatName
    '                    End If

    '                    Dim AppDate As String = Convert.ToDateTime(dr("app_date")).ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US"))
    '                    Dim StartSlot As String = Convert.ToDateTime(dr("start_slot")).ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
    '                    Dim ServiceName As String = GetServiceName(AppDate, StartSlot, dr("customer_id"), shTrans)
    '                    Dim SiebelDesc As String = ""
    '                    SiebelDesc += " จองคิว รับบริการ" & vbNewLine & ServiceName & vbNewLine
    '                    SiebelDesc += " วันที่ " & Convert.ToDateTime(dr("start_slot")).ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น."
    '                    SiebelDesc += " สาขา" & shLnq.SHOP_NAME_TH

    '                    Dim InputPara As String = ""
    '                    InputPara += "SIEBEL_TYPE=Appointment"
    '                    InputPara += "&SIEBEL_ACTIVITYCATEGORY=Appointment"
    '                    InputPara += "&SIEBEL_ACTIVITYSUBCATEGORY=" & SubCat
    '                    InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
    '                    InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN
    '                    InputPara += "&SIEBEL_MOBILENO=" & dr("customer_id")

    '                    ret = CallWebService(InputPara)
    '                    If ret.RESULT = True Then
    '                        'Update To shop.TB_APPOINTMENT_CUSTOMER
    '                        Dim sqlU As String = "update tb_appointment_customer "
    '                        sqlU += " set siebel_activity_id='" & ret.ACTIVITY_ID & "', "
    '                        sqlU += " siebel_status = '" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN & "', "
    '                        sqlU += " siebel_desc = '" & SiebelDesc & "'"
    '                        sqlU += " where CONVERT(varchar(10),app_date,120) = '" & AppDate & "' and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "' and customer_id = '" & dr("customer_id") & "' "

    '                        re = lnq.UpdateBySql(sqlU, shTrans.Trans)
    '                        If re = False Then
    '                            _err = lnq.ErrorMessage
    '                            shTrans.RollbackTransaction()
    '                            FunctionEng.SaveErrorLog("GenSiebelENG.CreateSiebelActivity", lnq.ErrorMessage & " MobileNo:" & dr("customer_id"))
    '                            PrintLog("GenSiebelENG.CreateSiebelActivity : " & lnq.ErrorMessage & " MobileNo:" & dr("customer_id"))
    '                        Else
    '                            shTrans.CommitTransaction()
    '                        End If
    '                    Else
    '                        FunctionEng.SaveErrorLog("GenSiebelENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & dr("customer_id"))
    '                        PrintLog("GenSiebelENG.CreateSiebelActivity : " & lnq.ErrorMessage & " MobileNo:" & dr("customer_id"))

    '                        Dim sqlE As String = "update tb_appointment_customer "
    '                        sqlE += " set siebel_desc='GenSiebelENG.CreateSiebelActivity : " & lnq.ErrorMessage & " MobileNo:" & dr("customer_id") & "' "
    '                        sqlE += " where customer_id = '" & dr("customer_id") & "' and CONVERT(varchar(10),start_slot,120)= '" & Convert.ToDateTime(dr("start_slot")).ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "'"
    '                        'sqlE += " where id = '" & dr("id") & "'"
    '                        re = lnq.UpdateBySql(sqlE, shTrans.Trans)
    '                        If re = True Then
    '                            shTrans.CommitTransaction()
    '                        Else
    '                            shTrans.RollbackTransaction()
    '                        End If
    '                    End If
    '                Else
    '                    FunctionEng.SaveErrorLog("GenSiebelENG.CreateSiebelActivity", shTrans.ErrorMessage)
    '                    PrintLog("GenSiebelENG.CreateSiebelActivity : " & shTrans.ErrorMessage)
    '                End If
    '            Next
    '        End If
    '    End If
    'End Sub

    'Private Function CallWebService(ByVal InputPara As String) As SiebelResponsePara
    '    Dim ret As New SiebelResponsePara
    '    Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_url1") & InputPara
    '    Dim webRequest As WebRequest
    '    Dim webresponse As WebResponse
    '    webRequest = webRequest.Create(SIEBEL_URL1)
    '    ret = GetSiebelMsg(webRequest, webresponse, InputPara)

    '    If ret.RESULT = False Then
    '        FunctionEng.SaveErrorLog("GenSiebelENG.CallWebService", ret.ErrorMessage & " " & SIEBEL_URL1)
    '        PrintLog("GenSiebelENG.CallWebService " & " GenSiebelENG.CallWebService : " & ret.ErrorMessage & " " & SIEBEL_URL1)
    '        Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_url2") & InputPara
    '        webRequest = webRequest.Create(SIEBEL_URL2)
    '        ret = GetSiebelMsg(webRequest, webresponse, InputPara)

    '        If ret.RESULT = False Then
    '            _err = ret.ErrorMessage
    '            FunctionEng.SaveErrorLog("GenSiebelENG.CallWebService", "GenSiebelENG.CallWebService : " & ret.ErrorMessage & " " & SIEBEL_URL2)
    '            PrintLog("GenSiebelENG.CallWebService " & " GenSiebelENG.CallWebService : " & ret.ErrorMessage & " " & SIEBEL_URL2)
    '        End If
    '    End If

    '    Return ret
    'End Function

#End Region

#Region "Update Siebel Activity"
    Public Sub UpdateSiebelActivity(ByVal lblTime As Label)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim csLnq As New Cen.TbShopCenLinqDB
            Dim dt As DataTable = FunctionEng.GetActiveShop()
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Application.DoEvents()
                    Dim dr As DataRow = dt.Rows(i)
                    'ไปหาข้อมูลเกี่ยวกับการจองใน Shop
                    Dim shLnq As TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))
                    'UpdateSiebel(trans, shLnq, lblTime)

                    Dim retTrans As Boolean = False
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GetSiegelENG.UpdateSiebelActivity")
                    If shTrans.Trans IsNot Nothing Then
                        SiebelUpdateToNoShow(shTrans, trans, shLnq, lblTime)
                    End If
                    shLnq = Nothing
                Next
            End If
            trans.CommitTransaction()
        End If
    End Sub

    'Private Delegate Sub DegSiebelUpdateToRegister(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    'Private Sub SiebelUpdateToRegister(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    '    Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB

    '    'Dim whText As String = "siebel_status = '" & Constant.Siebel.Status.OPEN & "' " ' and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"
    '    'Dim dt As DataTable = lnq.GetDataList(whText, "customer_id, id", shTrans.Trans)

    '    Dim whText As String = "select distinct CONVERT(date,app_date,120) app_date ,siebel_activity_id,"
    '    whText += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel, siebel_desc"
    '    whText += " from TB_APPOINTMENT_CUSTOMER "
    '    whText += " where siebel_status = '" & Constant.Siebel.Status.OPEN & "' and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' "
    '    whText += " and CONVERT(varchar(10),start_slot,120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "'"
    '    Dim dt As DataTable = lnq.GetListBySql(whText, shTrans.Trans)
    '    shTrans.CommitTransaction()
    '    If dt.Rows.Count > 0 Then
    '        'FunctionEng.SaveTransLog("GenSiebelENG.SiebelUpdateToRegister", "Start Update SiebelActivity on Shop " & shLnq.SHOP_NAME_EN)
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '            If shTrans.Trans IsNot Nothing Then
    '                Dim ret As New SiebelResponsePara
    '                Dim dr As DataRow = dt.Rows(i)

    '                'ถ้ายังไม่ถึงเวลานัด ให้ตรวจสอบว่าว่าลูกค้ามา Register แล้วหรือยัง
    '                Dim rLnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
    '                Dim whR As String = "customer_id = '" & dr("customer_id") & "' "
    '                whR += " and CONVERT(varchar(10), service_date,120) = '" & Convert.ToDateTime(dr("start_slot")).ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "' "
    '                whR += " and customertype_id = '3' "
    '                Dim dtR As DataTable = rLnq.GetDataList(whR, "", shTrans.Trans)
    '                shTrans.CommitTransaction()
    '                If dtR.Rows.Count > 0 Then
    '                    For j As Integer = 0 To dtR.Rows.Count - 1
    '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '                        If shTrans.Trans IsNot Nothing Then
    '                            Dim SiebelDesc As String = dr("siebel_desc") & vbNewLine & vbNewLine
    '                            SiebelDesc += Convert.ToDateTime(dtR.Rows(j)("service_date")).ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
    '                            SiebelDesc += "ลูกค้ามาลงทะเบียนรับบัตรคิว"

    '                            ret = UpdateSiebelDesc(SiebelDesc, dr, shTrans, lnq, Constant.Siebel.Status.REGISTERED)
    '                            If ret.RESULT = False Then
    '                                shTrans.RollbackTransaction()
    '                                FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
    '                                PrintLog("GenSiebelENG.SiebelUpdateToRegister  GenSiebelENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
    '                            Else
    '                                shTrans.CommitTransaction()
    '                            End If
    '                        Else
    '                            FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage & " MobileNo :" & dr("customer_id"))
    '                            PrintLog("GenSiebelENG.SiebelUpdateToRegister GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage & " MobileNo :" & dr("customer_id"))
    '                        End If
    '                    Next
    '                End If
    '            Else
    '                FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '                PrintLog("GenSiebelENG.SiebelUpdateToRegister GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '            End If
    '        Next
    '    End If
    'End Sub

    'Private Delegate Sub DegSiebelUpdateToCompleted(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    'Private Sub SiebelUpdateToCompleted(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    '    'กรณีจองแล้ว Register แล้ว และ End Queue แล้ว
    '    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '    If shTrans.Trans IsNot Nothing Then
    '        Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
    '        'Dim whText = "siebel_status = '" & Constant.Siebel.Status.REGISTERED & "' " 'and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "'"
    '        'Dim dt As DataTable = lnq.GetDataList(whText, "customer_id, id", shTrans.Trans)

    '        Dim whText As String = "select distinct CONVERT(date,app_date,120) app_date ,siebel_activity_id,"
    '        whText += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel, siebel_desc"
    '        whText += " from TB_APPOINTMENT_CUSTOMER "
    '        whText += " where siebel_status = '" & Constant.Siebel.Status.REGISTERED & "' "
    '        whText += " and CONVERT(varchar(10),start_slot,120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "' "

    '        Dim dt As DataTable = lnq.GetListBySql(whText, shTrans.Trans)
    '        shTrans.CommitTransaction()
    '        If dt.Rows.Count > 0 Then
    '            'FunctionEng.SaveTransLog("GenSiebelENG.SiebelUpdateToCompleted", "Start Update SiebelActivity on Shop " & shLnq.SHOP_NAME_EN)
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '                If shTrans.Trans IsNot Nothing Then
    '                    Dim ret As New SiebelResponsePara
    '                    Dim dr As DataRow = dt.Rows(i)

    '                    Dim eLnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
    '                    Dim whE As String = "customer_id = '" & dr("customer_id") & "' "
    '                    whE += " and call_time is not null and start_time is not null and end_time is not null "
    '                    whE += " and CONVERT(varchar(10),service_date,120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "' "
    '                    whE += " and customertype_id = '3' "
    '                    Dim dtE As DataTable = eLnq.GetDataList(whE, "", shTrans.Trans)
    '                    shTrans.CommitTransaction()
    '                    If dtE.Rows.Count > 0 Then
    '                        For j As Integer = 0 To dtE.Rows.Count - 1
    '                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '                            If shTrans.Trans IsNot Nothing Then
    '                                Dim SiebelDesc As String = dr("siebel_desc") & vbNewLine & vbNewLine
    '                                SiebelDesc += Convert.ToDateTime(dtE.Rows(j)("end_time")).ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
    '                                SiebelDesc += "ลูกค้าได้รับบริการเรียบร้อย"

    '                                ret = UpdateSiebelDesc(SiebelDesc, dr, shTrans, lnq, Constant.Siebel.Status.COMPLETED)
    '                                If ret.RESULT = False Then
    '                                    shTrans.RollbackTransaction()
    '                                    FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
    '                                    PrintLog("GenSiebelENG.SiebelUpdateToRegister GenSiebelENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
    '                                Else
    '                                    shTrans.CommitTransaction()
    '                                End If
    '                            Else
    '                                FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '                                PrintLog("GenSiebelENG.SiebelUpdateToRegister GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '                            End If
    '                        Next
    '                    End If
    '                Else
    '                    FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '                    PrintLog("GenSiebelENG.SiebelUpdateToRegister GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '                End If
    '            Next
    '        End If
    '    Else
    '        'FunctionEng.SaveTransLog("GenSiebelENG.SiebelUpdateToRegister", "GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '        PrintLog("GenSiebelENG.SiebelUpdateToRegister GenSiebelENG.SiebelUpdateToRegister : " & shTrans.ErrorMessage)
    '    End If
    'End Sub

    'Private Delegate Sub DegSiebelUpdateToMissed(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    'Private Sub SiebelUpdateToMissed(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    '    'กรณี Missed (คือจองมาแล้ว แล้วก็มา Register แล้ว แต่ตอน Call แล้วไม่มา) TB_COUNTER_QUEUE.status = 
    '    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '    If shTrans.Trans IsNot Nothing Then
    '        Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
    '        'Dim whText As String = "siebel_status = '" & Constant.Siebel.Status.REGISTERED & "' "
    '        'Dim dt As DataTable = lnq.GetDataList(whText, "customer_id, id", shTrans.Trans)

    '        Dim whText As String = "select distinct CONVERT(date,app_date,120) app_date ,siebel_activity_id,"
    '        whText += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel, siebel_desc"
    '        whText += " from TB_APPOINTMENT_CUSTOMER "
    '        whText += " where siebel_status = '" & Constant.Siebel.Status.REGISTERED & "' "
    '        whText += " and CONVERT(varchar(10),start_slot,120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "'"
    '        Dim dt As DataTable = lnq.GetListBySql(whText, shTrans.Trans)
    '        shTrans.CommitTransaction()
    '        If dt.Rows.Count > 0 Then
    '            'FunctionEng.SaveTransLog("GenSiebelENG.SiebelUpdateToMissed", "Start Update SiebelActivity on Shop " & shLnq.SHOP_NAME_EN)
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '                If shTrans.Trans IsNot Nothing Then
    '                    Dim ret As New SiebelResponsePara
    '                    Dim dr As DataRow = dt.Rows(i)

    '                    Dim cLnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
    '                    Dim whE As String = "customer_id = '" & dr("customer_id") & "' "
    '                    whE += " and call_time is not null and start_time is not null and end_time is null "
    '                    whE += " and status = '" & Constant.TbCounterQueue.Status.MissedCall & "' "  'ตรวจสอบสถานะอีกที
    '                    whE += " and and CONVERT(varchar(10),service_date,120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "' "
    '                    whE += " and customertype_id = '3' "
    '                    Dim dtC As DataTable = cLnq.GetDataList(whE, "", shTrans.Trans)
    '                    shTrans.CommitTransaction()
    '                    If dtC.Rows.Count > 0 Then
    '                        For j As Integer = 0 To dtC.Rows.Count - 1
    '                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '                            If shTrans.Trans IsNot Nothing Then
    '                                Dim SiebelDesc As String = dr("siebel_desc") & vbNewLine & vbNewLine
    '                                SiebelDesc += Convert.ToDateTime(dtC.Rows(j)("service_date")).ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
    '                                SiebelDesc += "ลูกค้าไม่รอรับบริการ"

    '                                ret = UpdateSiebelDesc(SiebelDesc, dr, shTrans, lnq, Constant.Siebel.Status.MISSED)
    '                                If ret.RESULT = False Then
    '                                    shTrans.RollbackTransaction()
    '                                    FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToMissed", "GenSiebelENG.SiebelUpdateToMissed : " & ret.ErrorMessage)
    '                                    PrintLog("GenSiebelENG.SiebelUpdateToMissed GenSiebelENG.SiebelUpdateToMissed : " & ret.ErrorMessage)
    '                                Else
    '                                    shTrans.CommitTransaction()
    '                                End If
    '                            Else
    '                                FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToMissed", "GenSiebelENG.SiebelUpdateToMissed : " & shTrans.ErrorMessage)
    '                                PrintLog("GenSiebelENG.SiebelUpdateToMissed GenSiebelENG.SiebelUpdateToMissed : " & shTrans.ErrorMessage)
    '                            End If
    '                        Next
    '                    End If
    '                Else
    '                    FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToMissed", "GenSiebelENG.SiebelUpdateToMissed : " & shTrans.ErrorMessage)
    '                    PrintLog("GenSiebelENG.SiebelUpdateToMissed GenSiebelENG.SiebelUpdateToMissed : " & shTrans.ErrorMessage)
    '                End If
    '            Next
    '        End If
    '    Else
    '        FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToMissed", "GenSiebelENG.SiebelUpdateToMissed : " & shTrans.ErrorMessage)
    '        PrintLog("GenSiebelENG.SiebelUpdateToMissed GenSiebelENG.SiebelUpdateToMissed : " & shTrans.ErrorMessage)
    '    End If
    'End Sub

    'Private Delegate Sub DegSiebelUpdateToCancel(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    'Private Sub SiebelUpdateToCancel(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB)
    '    'กรณี ยกเลิกการจอง Cancel (คือจองไปแล้ว ก็ยกเลิกก่อนจะถึงเวลา Register)
    '    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '    If shTrans.Trans IsNot Nothing Then
    '        Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
    '        'Dim whText As String = "siebel_status = '" & Constant.Siebel.Status.OPEN & " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "'"
    '        'Dim dt As DataTable = lnq.GetDataList(whText, "customer_id, id", shTrans.Trans)

    '        Dim whText As String = "select distinct app_date, siebel_activity_id,"
    '        whText += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel, siebel_desc"
    '        whText += " from TB_APPOINTMENT_CUSTOMER "
    '        whText += " where siebel_status = '" & Constant.Siebel.Status.OPEN & "' and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "' "
    '        whText += " and CONVERT(varchar(10),start_slot,120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "'"
    '        Dim dt As DataTable = lnq.GetListBySql(whText, shTrans.Trans)

    '        shTrans.CommitTransaction()
    '        If dt.Rows.Count > 0 Then
    '            'FunctionEng.SaveTransLog("GenSiebelENG.SiebelUpdateToCancel", "Start Update SiebelActivity on Shop " & shLnq.SHOP_NAME_EN)
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '                If shTrans.Trans IsNot Nothing Then
    '                    Dim ret As New SiebelResponsePara
    '                    Dim dr As DataRow = dt.Rows(i)

    '                    Dim SiebelDesc As String = dr("siebel_desc") & vbNewLine & vbNewLine
    '                    SiebelDesc += Convert.ToDateTime(dr("app_date")).ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
    '                    SiebelDesc += "ลูกค้ายกเลิกการจอง"

    '                    ret = UpdateSiebelDesc(SiebelDesc, dr, shTrans, lnq, Constant.Siebel.Status.CANCELLED)
    '                    If ret.RESULT = False Then
    '                        shTrans.RollbackTransaction()
    '                        FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToCancel", "GenSiebelENG.SiebelUpdateToCancel : " & ret.ErrorMessage)
    '                        PrintLog("GenSiebelENG.SiebelUpdateToCancel GenSiebelENG.SiebelUpdateToCancel : " & ret.ErrorMessage)
    '                    Else
    '                        shTrans.CommitTransaction()
    '                    End If
    '                Else
    '                    FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToCancel", "GenSiebelENG.SiebelUpdateToCancel : " & shTrans.ErrorMessage)
    '                    PrintLog("GenSiebelENG.SiebelUpdateToCancel GenSiebelENG.SiebelUpdateToCancel : " & shTrans.ErrorMessage)
    '                End If
    '            Next
    '        End If
    '    Else
    '        FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToCancel", "GenSiebelENG.SiebelUpdateToCancel : " & shTrans.ErrorMessage)
    '        PrintLog("GenSiebelENG.SiebelUpdateToCancel GenSiebelENG.SiebelUpdateToCancel : " & shTrans.ErrorMessage)
    '    End If
    'End Sub

    Private Sub SiebelUpdateToNoShow(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB, ByVal lblTime As Label)
        'กรณี No Show (คือจองไปแล้ว ไม่มาตามวันที่นัด ต้องรอให้ข้ามวันก่อนแล้วค่อย Update Siebel)
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GetSiegelENG.SiebelUpdateToNoShow")
        If shTrans.Trans IsNot Nothing Then
            Dim kLate As String = FunctionEng.GetShopConfig("k_late", shLnq)
            If kLate.Trim <> "" Then
                Dim jDt As New DataTable
                Dim jLnq As New TbAppointmentJobCenLinqDB
                Dim jWh As String = "((dateadd(minute," & (Convert.ToInt16(kLate) + 1) & ",start_slot) < getdate() "
                jWh += " and active_status='" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "')  "
                jWh += " or (convert(varchar(8),start_slot,112) <= convert(varchar(8),getdate()-1,112)  and active_status='" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "'))"
                jWh += " and shop_abb='" & shLnq.SHOP_ABB & "'"
                jDt = jLnq.GetDataList(jWh, "", trans.Trans)
                If jDt.Rows.Count > 0 Then
                    Dim vDateNow As DateTime = DateTime.Now
                    For Each jDr As DataRow In jDt.Rows
                        'Save Appointment Job To No Show
                        Dim pJob As New CenParaDB.Appointment.SaveAppointmentJobPara
                        Dim pPara As New CenParaDB.TABLE.TbAppointmentJobCenParaDB
                        If Convert.IsDBNull(jDr("id")) = False Then pPara.ID = jDr("id")
                        pPara.SHOP_ABB = shLnq.SHOP_ABB  'FunctionEng.GetShopConfig("ShopAbbCode", shLnq)
                        pPara.MOBILE_NO = jDr("mobile_no")
                        pPara.APP_DATE = vDateNow
                        pPara.START_SLOT = Convert.ToDateTime(jDr("start_slot"))
                        pPara.APPOINTMENT_CHANNEL = jDr("appointment_channel")
                        If jDr("active_status").ToString = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment Then
                            pPara.ACTIVE_STATUS = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.NoShow
                        ElseIf jDr("active_status").ToString = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk Then
                            pPara.ACTIVE_STATUS = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.Cancel
                        End If

                        pJob = SaveAppointmentJob(pPara)
                        pPara = Nothing
                    Next
                End If
                jDt.Dispose()


                Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                Dim whText As String = "select distinct CONVERT(date,app_date,120) app_date ,siebel_activity_id," & vbNewLine
                whText += " customer_id,CONVERT(datetime,start_slot,120) start_slot, appointment_channel, siebel_desc,customer_id, " & vbNewLine
                whText += " appointment_job_id, active_status " & vbNewLine
                whText += " from TB_APPOINTMENT_CUSTOMER " & vbNewLine
                whText += " where (active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'" & vbNewLine
                whText += " and dateadd(minute," & Convert.ToInt16(kLate) & ",start_slot) < getdate()) " & vbNewLine
                whText += " or (active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' and convert(varchar(8),start_slot,112) <= convert(varchar(8),getdate()-1,112))" & vbNewLine
                Dim dt As DataTable = lnq.GetListBySql(whText, shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim dr As DataRow = dt.Rows(i)
                        ' Update Siebel To NoShow
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GenSiebelENG.SiebelUpdateToNoShow")
                        If shTrans.Trans IsNot Nothing Then
                            Dim ret As New SiebelResponsePara

                            Dim SiebelDesc As String = dr("siebel_desc") & vbNewLine & vbNewLine
                            If dr("active_status").ToString = Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment Then
                                SiebelDesc += Convert.ToDateTime(dr("start_slot")).ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
                                SiebelDesc += "ลูกค้าไม่มารับบริการ"
                            ElseIf dr("active_status").ToString = Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk Then
                                SiebelDesc += DateTime.Now.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
                                SiebelDesc += "ลูกค้ายกเลิกการจอง"
                            End If

                            ret = UpdateSiebelDesc(SiebelDesc, dr, shTrans, lnq, Constant.TbAppointmentCustomer.Siebel.Status.NO_SHOW, Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment, shLnq)
                            If ret.RESULT = False Then
                                'Update Siebel จาก WebService ไม่ได้ ก็ให้เก็บ LOG เอาไว้
                                FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToNoShow", "GenSiebelENG.SiebelUpdateToNoShow : " & ret.ErrorMessage, Application.StartupPath & "\ErrorLog\", "AppointmentGenSiebelENG")
                                'PrintLog("GenSiebelENG.SiebelUpdateToNoShow GenSiebelENG.SiebelUpdateToNoShow : " & ret.ErrorMessage)
                            End If

                            'Update active_status to NoShow
                            Dim AppDate As String = Convert.ToDateTime(dr("app_date")).ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US"))
                            Dim StartSlot As String = Convert.ToDateTime(dr("start_slot")).ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
                            Dim sqlU As String = ""
                            If dr("active_status").ToString = Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment Then
                                sqlU = "update tb_appointment_customer "
                                sqlU += " set active_status='" & Constant.TbAppointmentCustomer.ActiveStatus.NoShow & "', "
                                sqlU += " update_by='0',update_date=getdate()"
                                sqlU += " where CONVERT(varchar(10),app_date,120) = '" & AppDate & "' and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "'"
                                sqlU += " and customer_id = '" & dr("customer_id") & "' "
                                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"
                            ElseIf dr("active_status").ToString = Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk Then
                                sqlU = "update tb_appointment_customer "
                                sqlU += " set active_status='" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "', "
                                sqlU += " update_by='0',update_date=getdate()"
                                sqlU += " where CONVERT(varchar(10),app_date,120) = '" & AppDate & "' and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "'"
                                sqlU += " and customer_id = '" & dr("customer_id") & "' "
                                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "'"
                            End If

                            If lnq.UpdateBySql(sqlU, shTrans.Trans) = True Then
                                shTrans.CommitTransaction()
                            Else
                                shTrans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToNoShow", shTrans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "AppointmentGenSiebelENG")
                            End If
                        End If
                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                        Application.DoEvents()
                    Next
                End If
                dt.Dispose()
            End If
        Else
            FunctionEng.SaveErrorLog("GenSiebelENG.SiebelUpdateToNoShow", "GenSiebelENG.SiebelUpdateToNoShow : " & shTrans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "AppointmentGenSiebelENG")
            'PrintLog("GenSiebelENG.SiebelUpdateToNoShow : SHOP Name " & shLnq.SHOP_NAME_EN & " ShopID: " & shLnq.SHOP_DB_NAME & "####" & shTrans.ErrorMessage)
        End If

    End Sub

    'Private Sub UpdateSiebel(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As TbShopCenLinqDB, ByVal lblTime As Label)
    '    Dim retTrans As Boolean = False
    '    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
    '    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
    '    If shTrans.Trans IsNot Nothing Then
    '        SiebelUpdateToNoShow(shTrans, trans, shLnq, lblTime)
    '    End If
    'End Sub

    Private Function UpdateSiebelDesc(ByVal SiebelDesc As String, ByVal dr As DataRow, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal lnq As ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB, ByVal SiebelStatus As String, ByVal ActiveStatus As String, ByVal shLnq As TbShopCenLinqDB) As SiebelResponsePara
        Dim re As Boolean = False
        Dim ret As New SiebelResponsePara
        Dim InputPara As String = ""
        InputPara += "SIEBEL_ACTID=" & dr("siebel_activity_id")
        InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
        InputPara += "&SIEBEL_STATUS=" & SiebelStatus

        ret = CallWebServiceUpdate(InputPara)
        If ret.RESULT = True Then
            'Update To shop.TB_APPOINTMENT_CUSTOMER
            Dim AppDate As String = Convert.ToDateTime(dr("app_date")).ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US"))
            Dim StartSlot As String = Convert.ToDateTime(dr("start_slot")).ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))

            Dim sqlU As String = "update tb_appointment_customer "
            sqlU += " set siebel_status = '" & SiebelStatus & "', "
            sqlU += " siebel_desc = '" & SiebelDesc & "' "
            sqlU += " where CONVERT(varchar(10),app_date,120) = '" & AppDate & "' and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "' and customer_id = '" & dr("customer_id") & "' "
            sqlU += " and active_status='" & ActiveStatus & "'"

            re = lnq.UpdateBySql(sqlU, shTrans.Trans)
            If re = False Then
                ret.ErrorMessage = lnq.ErrorMessage
                ret.RESULT = False
            Else
                ret.RESULT = True
            End If
        End If

        Return ret
    End Function

    Public Function SaveAppointmentJob(ByVal p As CenParaDB.TABLE.TbAppointmentJobCenParaDB) As CenParaDB.Appointment.SaveAppointmentJobPara
        Dim ret As New CenParaDB.Appointment.SaveAppointmentJobPara
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbAppointmentJobCenLinqDB
        If p.ID <> 0 Then
            lnq.GetDataByPK(p.ID, trans.Trans)
        End If

        lnq.SHOP_ABB = p.SHOP_ABB
        lnq.MOBILE_NO = p.MOBILE_NO
        lnq.APP_DATE = p.APP_DATE
        lnq.START_SLOT = p.START_SLOT
        lnq.APPOINTMENT_CHANNEL = p.APPOINTMENT_CHANNEL
        lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

        Dim re As Boolean = False
        If lnq.ID <> 0 Then
            re = lnq.UpdateByPK("GenSiebelENG.SaveAppointmentJob", trans.Trans)
        Else
            re = lnq.InsertData("GenSiebelENG.SaveAppointmentJob", trans.Trans)
            If re = True Then
                p.ID = lnq.ID
            End If
        End If
        ret.SaveResult = re
        ret.AppointmentJobPara = p

        If re = False Then
            trans.RollbackTransaction()
            ret.ErrorMessage = lnq.ErrorMessage
        Else
            trans.CommitTransaction()
        End If

        Return ret
    End Function


    Private Function CallWebServiceUpdate(ByVal InputPara As String) As SiebelResponsePara
        Dim ret As New SiebelResponsePara
        Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_update_url1") & InputPara
        Dim webRequest As WebRequest
        Dim webresponse As WebResponse
        webRequest = webRequest.Create(SIEBEL_URL1)
        ret = GetSiebelMsg(webRequest, webresponse, InputPara, "UPDATE")

        If ret.RESULT = False Then
            FunctionEng.SaveErrorLog("GenSiebelENG.CallWebServiceUpdate", "GenSiebelENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL1, Application.StartupPath & "\ErrorLog\", "AppointmentGenSiebelENG")
            Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_update_url2") & InputPara
            webRequest = webRequest.Create(SIEBEL_URL2)
            ret = GetSiebelMsg(webRequest, webresponse, InputPara, "UPDATE")

            If ret.RESULT = False Then
                _err = ret.ErrorMessage
                FunctionEng.SaveErrorLog("GenSiebelENG.CallWebServiceUpdate", "GenSiebelENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL2, Application.StartupPath & "\ErrorLog\", "AppointmentGenSiebelENG")
                'PrintLog("GenSiebelENG.CallWebServiceUpdate GenSiebelENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL2)
            End If
        End If

        Return ret
    End Function
#End Region

#Region "Common Function"
    Private Function GetServiceName(ByVal AppDate As String, ByVal StartSlot As String, ByVal MobileNo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        'AppDate = yyyy-MM-dd
        'StartSlot = yyyy-MM-dd HH:mm

        Dim ret As String = ""
        Dim aLnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
        Dim dt As DataTable = aLnq.GetDataList("CONVERT(varchar(10),app_date,120) = '" & AppDate & "' and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "' and customer_id = '" & MobileNo & "' ", "", shTrans.Trans)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                lnq.GetDataByPK(dr("item_id"), shTrans.Trans)
                If lnq.ITEM_NAME_TH.Trim <> "" Then
                    ret += "  - " & lnq.ITEM_NAME_TH & vbNewLine
                End If
            Next
            dt = Nothing
        End If
        aLnq = Nothing

        Return ret
    End Function



    Private Function GetSiebelMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String, Optional ByVal GenType As String = "CREATE") As SiebelResponsePara
        Dim ret As New SiebelResponsePara
        webrequest.ContentType = "application/x-www-form-urlencoded"
        webrequest.Method = "POST"
        Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
        Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
        Dim os As Stream = Nothing

        Try
            webrequest.ContentLength = bytes.Length
            os = webrequest.GetRequestStream()
            os.Write(bytes, 0, bytes.Length)
            webresponse = webrequest.GetResponse()
            Dim Stream As New StreamReader(webresponse.GetResponseStream())
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If GenType = "UPDATE" Then
                    If tmp.IndexOf("SUCCESS") > -1 Then
                        ret.RESULT = True
                    Else
                        ret.RESULT = False
                        ret.ErrorMessage = tmp
                    End If
                Else
                    If tmp.IndexOf("SUCCESS-") > -1 Then
                        ret.RESULT = True
                        ret.ACTIVITY_ID = tmp.Replace("SUCCESS-", "")
                    Else
                        ret.RESULT = False
                        ret.ErrorMessage = tmp
                    End If
                End If
            End If
            Stream.Close()
            Stream = Nothing

        Catch ex As WebException
            ret.RESULT = False
            ret.ErrorMessage = ex.Message
        Finally
            If os IsNot Nothing Then
                os.Close()
            End If
        End Try
        Return ret
    End Function

    'Dim _OldLog As String = ""
    'Private Sub PrintLog(ByVal LogDesc As String)
    '    If _OldLog <> LogDesc Then
    '        _TXT_LOG.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
    '        _OldLog = LogDesc
    '        Application.DoEvents()
    '    End If
    'End Sub
#End Region
End Class
