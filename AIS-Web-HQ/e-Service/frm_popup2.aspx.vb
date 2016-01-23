Partial Class frm_popup2
    Inherits System.Web.UI.Page
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
            cPara = Nothing
        End If
    End Sub
    Private Sub bindObj(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        Try
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")
            globalFunction.Type = ""
            lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1")
            lbl_message2.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message2")
            img_close.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_close")

            lbl_message1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('bindObj Error !!,Please Check Function');", True)
        End Try

    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_close.ToolTip = "ปิดหน้าต่าง"
        Else
            img_close.ToolTip = "Close Window"
        End If
    End Sub

    Protected Sub img_close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_close.Click
        'Session.RemoveAll()
        Session("serviceid") = ""
        Session("shop_id") = ""
        Session("region_id") = ""
        Session("Appointment") = ""
        Session("service1") = ""
        Session("service2") = ""
        Session("service3") = ""
        Response.Redirect("frm_Appointment_mnt.aspx")
    End Sub
End Class
