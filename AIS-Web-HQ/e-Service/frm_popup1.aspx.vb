
Partial Class frm_popup1
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

            Dim msg2 As String = ""
            Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(Convert.ToInt64(Session("shop_id").ToString.Split("|")(0)))
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(shLnq.ID, "WebAppoint.frm_popup1_bindObj")
            If shTrans IsNot Nothing Then
                msg2 = String.Format(globalFunction.GetText(cPara.PreferLang, "lbl_message2"), Engine.Common.FunctionEng.GetShopConfig("k_cancel", shTrans))
                shTrans.CommitTransaction()
            End If
            shLnq = Nothing
            lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1")
            lbl_message2.Text = msg2
            lbl_message3.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message3")
            lbl_message4.Text = String.Format(globalFunction.GetText(cPara.PreferLang, "lbl_message4"), Engine.Common.FunctionEng.GetQisDBConfig("MaxEditAppointmentHour"))
            lbl_message5.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message5")
            img_close.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_close")

            lbl_message2.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message3.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message4.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "title"))
            lbl_message5.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
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
        Response.Redirect("frm_Appointment_list.aspx")
    End Sub
End Class
