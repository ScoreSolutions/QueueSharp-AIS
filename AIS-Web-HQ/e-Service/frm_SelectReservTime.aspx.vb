Imports Engine.Appointment
Imports System
Imports System.Data
Partial Class frm_SelectReservTime
    Inherits System.Web.UI.Page
    Dim globalFunction As New globalFunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
            bindObj(cPara)
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
            img_return.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_return")
            img_next.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_next")
            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString

            lbl_message1.Attributes.Add("style", globalFunction.GetStyle(cPara.NetworkType, "text"))

            If cPara.NetworkType = "pre" Then
                Panel5.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "post" Then
                Panel5.BackImageUrl = "~/image/gsm/bg_2.png"
            ElseIf cPara.NetworkType = "corp" Then
                Panel5.BackImageUrl = "~/image/corp/bg_2.png"
            ElseIf cPara.NetworkType = "3g" Then
                Panel5.BackImageUrl = "~/image/12call/bg_2.png"
            ElseIf cPara.NetworkType = "3gpost" Then
                Panel5.BackImageUrl = "~/image/3gpost/bg_2.png"
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class
