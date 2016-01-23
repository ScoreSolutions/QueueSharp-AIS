Imports System
Imports System.Data
Imports System.Globalization
Imports System.Data.OleDb
Imports Engine.Appointment
Imports ShParaDB.TABLE

Partial Class frm_appointment_confirm
    Inherits System.Web.UI.Page
    Dim globalFunction As New globalFunction
    Dim AppointmentENG As New Engine.Appointment.AppointmentENG()
    Dim appdatetime As Array
    Dim CustomerID As String = String.Empty
    'Dim objConn As New OleDbConnection(ConfigurationManager.ConnectionStrings("QisDB").ConnectionString)



    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        MasterPageFile = cPara.MasterPage
        cPara = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            txtNetworkType.Text = cPara.NetworkType
            txtPreferLang.Text = cPara.PreferLang
            bindObj(cPara)
            BindTooltip(cPara)
            'img_confirm.Attributes.Add("onclick", "window.open('frm_popup1.aspx?', 'list','resizable=no,left=150,top=80,width=650,height=365,toolbar=no,scrollbars=yes,menubar=no,location=no');")
            If Not Session("Appointment") Is Nothing Then

                appdatetime = Session("Appointment").ToString.Split("|")
                Dim dDate As Date = globalFunction.cStrToDate(appdatetime(0))
                Dim dTime As String = appdatetime(1)
                lbl_appointment_day.Text = dDate.ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
                lbl_appointment_dt.Text = dDate.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo(cPara.CurrentLang))
                lbl_appointment_tm.Text = dTime
            End If

            If Not Session("shop_id") Is Nothing Then
                Dim arrshop As Array
                arrshop = Session("shop_id").ToString.Split("|")
                lbl_shop_nm.Text = arrshop(1)
            End If
            CustomerProfile()

            'If Session("service1")(0) IsNot Nothing And Session("service1")(0) <> "0" Then
            '    Dim service1 As Array = Session("service1").ToString.Split("|")
            '    lbl_service1.Text = service1(1)
            'Else
            '    lbl_service1.Text = ""
            'End If
            'If Session("service2")(0) IsNot Nothing And Session("service2")(0) <> "0" Then
            '    Dim service2 As Array = Session("service2").ToString.Split("|")
            '    lbl_service2.Text = service2(1)
            'Else
            '    lbl_service2.Text = ""
            'End If
            'If Session("service3") IsNot Nothing And Session("service3")(0) <> "0" Then
            '    Dim service3 As Array = Session("service3").ToString.Split("|")
            '    lbl_service3.Text = service3(1)
            'Else
            '    lbl_service3.Text = ""
            'End If

            'จัดเรียง Service 
            If Session("serviceitemtime") IsNot Nothing Then
                Dim strService As String = Session("serviceitemtime") & ",0"
                Dim strSplit As String() = strService.Split(",")
                For i As Integer = 0 To strSplit.Length - 1
                    If strSplit(i) <> "0" Then
                        If i = 0 Then
                            lbl_service1.Text = SetItem_Name(strSplit(0))
                        End If
                        If i = 1 Then
                            lbl_service2.Text = SetItem_Name(strSplit(1))
                        End If
                        If i = 2 Then
                            lbl_service3.Text = SetItem_Name(strSplit(2))
                        End If
                    End If
                Next
            End If

            cPara = Nothing
        End If
    End Sub

    Private Function SetItem_Name(ByVal id As String) As String
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
        Dim cEng As New Engine.Appointment.AppointmentENG
        Dim dt As DataTable = cEng.GetItemTime(id)
        Dim strService As String = ""
        If dt.Rows.Count <> 0 Then
            If cPara.PreferLang.ToLower = "thai" Then
                strService = dt(0)("item_name_th")
            Else
                strService = dt(0)("item_name")
            End If
        End If

        'If strService = "" Then
        '    strService = dt(i)("id")
        'Else
        '    strService &= "," & dt(i)("id")
        'End If



        cPara = Nothing
        cEng = Nothing

        Return strService
    End Function

    Private Sub bindObj(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
            globalFunction.Type = ""
            img_tab1.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab1")
            img_tab2.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab2")
            lbl_prefix.Text = globalFunction.GetText(cPara.PreferLang, "lbl_prefix")
            lbl_number.Text = globalFunction.GetText(cPara.PreferLang, "lbl_number")
            lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1")
            lbl_message2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message2")
            lbl_message3.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message3")
            lbl_message4.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message4")
            lbl_message5.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message5")
            lbl_message6.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message6")
            lbl_message7.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message7")
            lbl_message8.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message8")
            img_return.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_return")
            img_confirm.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_confirm")

            lbl_prefix.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_name.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_number.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_phone_number.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_message1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_shop_nm.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_message2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_appointment_day.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_message3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_appointment_dt.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_message4.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_appointment_tm.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_message5.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message6.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message7.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message8.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))

            lbl_service1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_service2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_service3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))

            If cPara.NetworkType = "pre" Then
                Panel7.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "post" Then
                Panel7.BackImageUrl = "~/image/gsm/bg_2.png"
            ElseIf cPara.NetworkType = "corp" Then
                Panel7.BackImageUrl = "~/image/corp/bg_2.png"
            ElseIf cPara.NetworkType = "3g" Then
                Panel7.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "3gpost" Then
                Panel7.BackImageUrl = "~/image/3gpost/bg_2.png"
            End If

            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('bindObj Error !!,Please Check Function');", True)
        End Try
    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_return.ToolTip = "ย้อนกลับ"
            img_confirm.ToolTip = "ยืนยันการนัด"
            txt_email.ToolTip = "อีเมล"
        Else
            img_return.ToolTip = "Back"
            img_confirm.ToolTip = "Confirm"
            txt_email.ToolTip = "E-Mail"
        End If
    End Sub

    Private Sub CustomerProfile()
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            Dim F_NAME As String = String.Empty
            Dim L_Name As String = String.Empty
            'Dim lang As String = String.Empty
            Dim cusInfo As New CenParaDB.TABLE.TbCustomerCenParaDB
            Dim cEng As New Engine.Appointment.AppointmentENG
            cusInfo = cEng.GetCustomerProfileFromDB(cPara.MobileNo) 'AppointmentENG.GetCustomerProfile(cPara.MobileNo, Session.SessionID())
            cEng = Nothing

            F_NAME = cusInfo.F_NAME.ToString
            L_Name = cusInfo.L_NAME.ToString
            Dim strLang As String = ""
            If cusInfo.PREFER_LANG = "Thai" Then
                strLang = "Thai"
            ElseIf cusInfo.PREFER_LANG = "Thailand" Then
                strLang = "Thai"
            ElseIf cusInfo.PREFER_LANG Is Nothing Then
                strLang = "Eng"
            ElseIf cusInfo.PREFER_LANG = "" Then
                strLang = "Eng"
            Else
                strLang = "Eng"
            End If

            Session("PREFER_LANG") = strLang
            CustomerID = cusInfo.MOBILE_NO
            lbl_prefix.Visible = True
            lbl_name.Visible = True
            lbl_phone_number.Text = cPara.MobileNo

            lbl_name.Text = F_NAME & " " & L_Name
            If cusInfo.TITLE_NAME.Trim <> "" Then
                lbl_name.Text = cusInfo.TITLE_NAME & " " & lbl_name.Text
            End If
            If lbl_name.Text.Length > 0 Then
                lbl_prefix.Visible = False
            Else
                lbl_prefix.Visible = True
            End If
            cPara = Nothing
        Catch ex As Exception
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindCustomer Error !!,Please Check Function');", True)
            lbl_prefix.Visible = False
            lbl_name.Visible = False
        End Try
    End Sub


    Protected Sub img_return_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_return.Click
        If Session("shop_id") Is Nothing Then
            Response.Redirect("frm_DontReservStt.aspx")
        Else
            Response.Redirect("frm_AddAppointment.aspx?shop_id=" & Session("shop_id"))
        End If

    End Sub

    Protected Sub img_confirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_confirm.Click
        'img_confirm.Visible = False
     Try



            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            Dim arrshop As Array
            Dim service1 As Array
            Dim service2 As Array
            Dim service3 As Array
            Dim strservice As String = String.Empty
            If txt_email.Text = "" Then
                If cPara.PreferLang.ToLower = "thai" Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('กรุณากรอกอีเมล !!!');", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Invalid E-Mail,Please Input E-Mail !!!');", True)
                End If
                Exit Sub
            End If

            arrshop = Session("shop_id").ToString.Split("|")
            If Not Session("service1") Is Nothing Then
                service1 = Session("service1").ToString.Split("|")
            Else
                service1 = Nothing
            End If
            If Not Session("service2") Is Nothing Then
                service2 = Session("service2").ToString.Split("|")
            Else
                service2 = Nothing
            End If
            If Not Session("service3") Is Nothing Then
                service3 = Session("service3").ToString.Split("|")
            Else
                service3 = Nothing
            End If

            Dim ItemID As String = String.Empty
            If Not service1 Is Nothing Then
                If service1(0) <> "0" Then
                    ItemID &= service1(0) & ","
                    strservice &= service1(0) & ","
                End If
            End If
            If Not service2 Is Nothing Then
                If service2(0) <> "0" Then
                    ItemID &= service2(0) & ","
                    strservice &= service2(0) & ","
                End If

            End If
            If Not service3 Is Nothing Then
                If service3(0) <> "0" Then
                    ItemID &= service3(0) & ","
                    strservice &= service3(0) & ","
                End If
            End If
            ItemID = ItemID.Substring(0, ItemID.Length - 1)
            strservice = strservice.Substring(0, strservice.Length - 1)

            Dim arrstrdt As Array
            arrstrdt = Session("Appointment").ToString.Split("|")
            Dim dDate As Date = globalFunction.cStrToDate(arrstrdt(0))
            Dim appdttime As New Date(dDate.Year, dDate.Month, dDate.Day, Split(arrstrdt(1), ":")(0), Split(arrstrdt(1), ":")(1), 0)
            Dim tmpItem() As String = Split(ItemID, ",")

            Dim AppointmentChannel As String = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.AppointmentChannel.WebAppoint
            Dim para1(tmpItem.Length - 1) As TbAppointmentCustomerShParaDB
            Dim i As Integer = 0
            For Each ServiceID As String In tmpItem
                '####### ตั้ม แก้ไขให้ถึงเฉพาะตัวเลข  31/05/2556#######
                Dim strShopIdFull As String = Session("shop_id") & ""
                Dim strShopIdArray As String() = strShopIdFull.Split("|")
                Dim strItemID As String
                If strShopIdArray.Length > 0 Then
                    strItemID = strShopIdArray(0)
                End If
                '############################################
                Dim eng As New AppointmentENG
                Dim tmpItemID As Long = eng.GetItemIDByMasterID(ServiceID, Convert.ToInt64(strItemID))
                eng = Nothing

                Dim para As New TbAppointmentCustomerShParaDB
                para.CAPACITY = 1
                para.APP_DATE = dDate
                para.START_SLOT = appdttime
                para.END_SLOT = appdttime
                para.ITEM_ID = tmpItemID
                para.CUSTOMER_ID = cPara.MobileNo
                para.APPOINTMENT_CHANNEL = AppointmentChannel
                para.ACTIVE_STATUS = 1
                para.CUSTOMER_EMAIL = txt_email.Text
                para1(i) = para
                i += 1
            Next
            Dim strmsg As String = String.Empty
            If Session("Edit") = "" Then
                strmsg = AppointmentENG.InsertAppointment(arrshop(0), para1, Session("PREFER_LANG"), AppointmentChannel).ErrorMessage
            Else
                strmsg = AppointmentENG.EditAppointment(arrshop(0), appdttime, appdttime, ItemID, cPara.MobileNo, Session("PREFER_LANG"), CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.AppointmentChannel.WebAppoint, txt_email.Text).ErrorMessage
            End If
            If strmsg = "" Then
                If AppointmentENG.SendEmailConfirm(txt_email.Text, strservice.ToString, cPara.MobileNo.ToString, appdttime, Session("PREFER_LANG"), arrshop(0)) = False Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Can not  Send E-mail to customer');", True)
                    'Exit Sub
                End If
                Response.Redirect("frm_popup1.aspx")
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Can not Appointment,Please Try Again !!!');", True)
                img_confirm.Visible = True
                Exit Sub
            End If

            Session.Remove("serviceid")
            Session.Remove("ArrService")
            Session.Remove("shop_id")
            Session.Remove("region_id")
            Session.Remove("Appointment")
            Session.Remove("service1")
            Session.Remove("service2")
            Session.Remove("service3")
            cPara = Nothing
            Response.Redirect("frm_popup1.aspx")
            'Response.Redirect("frm_DontReservStt.aspx")
        Catch ex As Exception
            'Response.Redirect("frm_popup1.aspx")
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            img_confirm.Visible = True
            Exit Sub
        End Try
        img_confirm.Visible = True
    End Sub

    Protected Sub img_tab2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab2.Click
        Response.Redirect("frm_history_appointment.aspx")
    End Sub
End Class
