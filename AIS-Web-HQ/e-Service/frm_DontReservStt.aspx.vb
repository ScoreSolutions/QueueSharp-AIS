Imports Engine.Appointment
Imports System
Imports System.Data
Imports System.Data.OleDb


Partial Class frm_DontReservStt
    Inherits System.Web.UI.Page
    Dim globalFunction As New globalFunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            binddata()
        End If
    End Sub
    Private Sub binddata()
        Try
            Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
            txtNetworkType.Text = cPara.NetworkType
            txtPreferLang.Text = cPara.PreferLang
            globalFunction.Network_Type = cPara.NetworkType
            globalFunction.Page_Name = AppRelativeVirtualPath.Replace("~/", "")


            Dim e As New Engine.Appointment.AppointmentENG
            Dim p As New CenParaDB.Appointment.AppointmentCheckBacklistResultPara
            p = e.CheckBacklist(cPara.MobileNo)
            If p.IsBackList = False Then
                If e.GetActiveAppointment(cPara.MobileNo) Then
                    Response.Redirect("frm_Appointment_list.aspx")
                Else
                    Dim strShopID As String = ""
                    Dim strShopName As String = ""
                    Dim strServiceID As String = ""
                    Dim strRegionID As String = ""
                    Dim pl As New CenParaDB.Appointment.LastShopRegisterPara
                    pl = e.GetLastShopRegister(cPara.MobileNo)
                    If pl.SHOP_ID <> 0 Then
                        strShopID = pl.SHOP_ID
                        If cPara.PreferLang = "Thai" Then
                            strShopName = pl.SHOP_NAME_TH
                        Else
                            strShopName = pl.SHOP_NAME_EN
                        End If
                        strRegionID = pl.REGION_ID

                        If strShopID <> "" Then
                            Session("shop_id") = strShopID & "|" & strShopName
                        End If
                        If strRegionID <> "" Then
                            Session("region_id") = strRegionID
                        End If
                        Session("serviceid") = pl.SERVICE_ID
                    End If
                    pl = Nothing

                    Response.Redirect("frm_Appointment_mnt.aspx")
                End If
            Else
                globalFunction.Type = "0"
                lbl_message1.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message1").ToString
                If cPara.PreferLang = "Thai" Then
                    lbl_message3.Text = p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo(cPara.CurrentLang))
                End If

                lbl_message2.Text = String.Format(globalFunction.GetText(cPara.PreferLang, "lbl_message2").ToString, p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo(cPara.CurrentLang)))
                lbl_message4.Text = globalFunction.GetText(cPara.PreferLang, "lbl_message4").ToString

                If cPara.PreferLang = "English" Then
                    lbl_message5.Text = "Thank you."
                End If
            End If
            p = Nothing
            e = Nothing

            globalFunction.Type = ""
            img_ok.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_ok")
            img_servicehistory.ImageUrl = globalFunction.GetText(cPara.PreferLang, "img_servicehistory")
            Session("form") = globalFunction.GetText(cPara.PreferLang, AppRelativeVirtualPath.Replace("~/", "")).ToString
            BindTooltip(cPara)
            cPara = Nothing
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            'Response.Redirect("frm_login.aspx")
        End Try

    End Sub

    Private Sub BindTooltip(ByVal cPara As CenParaDB.Appointment.CustomerWebAppointmentPara)
        If cPara.PreferLang.ToLower = "thai" Then
            img_ok.ToolTip = "ตกลง"
        Else
            img_ok.ToolTip = "Ok"
        End If
    End Sub



    Protected Sub img_ok_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ok.Click
        Try
            'Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
            'Dim eng As New Engine.Appointment.AppointmentENG
            'Dim pl As CenParaDB.Appointment.LastShopRegisterPara = eng.GetLastShopRegister(cPara.MobileNo)
            'Dim strShopID As String = pl.SHOP_ID.ToString
            'pl = Nothing
            'eng = Nothing
            'cPara = Nothing
            Response.Redirect("frm_history_appointment.aspx")

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
        End Try

    End Sub

    Protected Sub img_servicehistory_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_servicehistory.Click
        Response.Redirect("frm_history_appointment.aspx")
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin()
        MasterPageFile = cPara.MasterPage
        cPara = Nothing
    End Sub


End Class
