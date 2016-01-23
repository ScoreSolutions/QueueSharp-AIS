Imports Engine.Appointment
Imports System
Imports System.Data
Imports System.Globalization
Partial Class frm_delete_appointment
    Inherits System.Web.UI.Page
    Dim AppointmentENG As New Engine.Appointment.AppointmentENG
    Dim globalFunction As New globalFunction

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
            If Not Request.QueryString("shop_id").ToString() Is Nothing Then
                Session("shop_id") = Request.QueryString("shop_id").ToString()
                Dim arrshop As Array
                arrshop = Session("shop_id").ToString.Split("|")
                lbl_shopnm.Text = arrshop(1)
                Dim arrservice As Array
                arrservice = Request.QueryString("service").ToString().Split(",")
                Dim strservice As String = String.Empty
                For i As Integer = 0 To UBound(arrservice)
                    If arrservice(i) <> "" Then
                        strservice = strservice & BindServiceAll1(arrservice(i)) & ","
                    End If
                Next
                lbl_service.Text = strservice.Substring(0, strservice.Length - 1)

                Dim appdt As Date = Request.QueryString("appdt").ToString()
                lbl_day.Text = appdt.ToString("dddd", CultureInfo.GetCultureInfo(cPara.CurrentLang))
                lbl_date.Text = appdt.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo(cPara.CurrentLang))
                lbl_time.Text = appdt.ToString("HH.mm", CultureInfo.GetCultureInfo(cPara.CurrentLang))
            End If
            cPara = Nothing
        End If
    End Sub
    Private Sub bindObj(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
            globalFunction.Type = ""
            img_tab1.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab1")
            img_tab2.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_tab2")
            lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1")
            lbl_message2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message2")
            lbl_message3.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message3")
            lbl_message4.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message4")
            lbl_message5.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message5")
            lbl_message6.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message6")
            lbl_message7.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message7")
            img_return_no_arrow.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_return_no_arrow")
            img_confirm_reject.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_confirm_reject")
            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString

            lbl_message1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_message2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message4.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message5.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message6.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message7.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))

            lbl_service.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_shopnm.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_day.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_date.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
            lbl_time.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))

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
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('bindObj Error !!,Please Check Function');", True)
        End Try

    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_tab1.ToolTip = "นัดรับบริการ"
            img_tab2.ToolTip = "ประวัติการรับบริการ"
            img_return_no_arrow.ToolTip = "ย้อนกลับ"
            img_confirm_reject.ToolTip = "ยืนยันการยกเลิก"
        Else
            img_tab1.ToolTip = "Appointment"
            img_tab2.ToolTip = "History"
            img_return_no_arrow.ToolTip = "Back"
            img_confirm_reject.ToolTip = "Confirm"
        End If
    End Sub

    Protected Sub img_return_no_arrow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_return_no_arrow.Click
        Response.Redirect("frm_Appointment_list.aspx")

    End Sub

    Protected Sub img_confirm_reject_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_confirm_reject.Click
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            If AppointmentENG.CancelAppointment(cPara.MobileNo) = False Then

                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Can not Delete Appointment');", True)
                Exit Sub
            Else
                Session.Remove("serviceid")
                Session.Remove("ArrService")
                Session.Remove("shop_id")
                Session.Remove("region_id")
                Session.Remove("Appointment")
                Session.Remove("service1")
                Session.Remove("service2")
                Session.Remove("service3")
                Session.Remove("Edit")
                cPara = Nothing
                Response.Redirect("frm_popup2.aspx")
            End If
        Catch ex As Exception

        End Try
       
    End Sub

    Protected Sub img_tab1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab1.Click
        Response.Redirect("frm_Appointment_mnt.aspx")
    End Sub

    Protected Sub img_tab2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_tab2.Click
        'Response.Redirect("frm_Appointment_list.aspx")
        Response.Redirect("frm_history_appointment.aspx")
    End Sub

    Function BindServiceAll1(ByVal itemcd As String) As String
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            Dim dt As New DataTable
            dt = globalFunction.GetService("'0'")

            If dt.Rows.Count > 0 Then
                Dim Arr As Array = dt.Select("ID='" & itemcd & "'")
                If cPara.PreferLang = "Thai" Then
                    Return Arr(0)(1)
                Else
                    Return Arr(0)(2)
                End If
            End If
            cPara = Nothing
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindServiceAll1 Error !!,Please Check Function');", True)
        End Try
        Return ""
    End Function
End Class
