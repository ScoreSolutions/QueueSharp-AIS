Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO

Partial Class frm_login
    Inherits System.Web.UI.Page
    Dim strurl As String = String.Empty
    Dim version As String = System.Configuration.ConfigurationManager.AppSettings("version")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.RemoveAll()
            'Dim dt As New DataTable
            'Dim dr As DataRow
            'dt.Columns.Add("lang")
            'dr = dt.NewRow
            'dr(0) = "Thai"
            'dt.Rows.Add(dr)
            'dr = dt.NewRow
            'dr(0) = "English"
            'dt.Rows.Add(dr)
            'dtplang.DataTextField = "lang"
            'dtplang.DataSource = dt
            'dtplang.DataBind()
            Page.Title = version
            If Request.UrlReferrer IsNot Nothing Then ViewState("PreviousPage") = Request.UrlReferrer
        End If

        If Request("mobileparam") IsNot Nothing And Request("mobileparam") <> "" Then
            strurl = Request("mobileparam").ToString().Replace(" ", "+")
            Login()
            Exit Sub
        Else
            If Request.QueryString("mobileparam") IsNot Nothing Then
                strurl = Request.QueryString("mobileparam").ToString().Replace(" ", "+")
                Login()
                Exit Sub
            End If
            If Request("mobileNo") IsNot Nothing And Request("type") IsNot Nothing And Request("lang") IsNot Nothing And Request("mobileNo") <> "mobileNo" And Request("type") <> "network Type" Then
                strurl = globalFunction.Encrypt("mobileNo=" & Request("mobileNo") & "&channel=QIS&lang=" & Request("lang") & "&networktype=" & Request("type"), "QISIntegrateTest")
                Login()
                Exit Sub
            End If
        End If
    End Sub

    Sub Login()
        If strurl <> "" Then
            Dim strdecrypt As String = globalFunction.Decrypt(strurl, "QISIntegrateTest")
            'mobileNo=0801060179&channel=QIS&lang=th&networktype=corp
            If strdecrypt = "" Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Invalid Parameter,Please input parameter');", True)
                If ViewState("PreviousPage") IsNot Nothing Then
                    Dim strUrlPrevious As String = ViewState("PreviousPage").ToString()
                    ViewState.Remove("PreviousPage")
                    Response.Redirect(strUrlPrevious)
                    Exit Sub
                End If
            End If

            Dim para As Array
            para = strdecrypt.Split("&")
            If UBound(para) > 0 Then
                Dim cPara As New CenParaDB.Appointment.CustomerWebAppointmentPara
                cPara.MobileNo = para(0).ToString.Replace("mobileNo=", "").ToString
                If para(2).ToString.Replace("lang=", "").ToString = "th" Then
                    cPara.PreferLang = "Thai"
                    cPara.CurrentLang = "th-TH"
                Else
                    cPara.PreferLang = "English"
                    cPara.CurrentLang = "en-US"
                End If
                '####Para#####
                'postpaid2g()
                'prepaid2g()
                'corporate2g()
                'postpaid3g()
                'prepaid3g()
                'corporate3g()

                If para(3).ToString.Replace("networktype=", "").ToLower = "postpaid2g" Then
                    cPara.MasterPage = "~/Gsm.master"
                    cPara.NetworkType = "post"
                ElseIf para(3).ToString.Replace("networktype=", "").ToLower = "corporate2g" Then
                    cPara.MasterPage = "~/corp.master"
                    cPara.NetworkType = "corp"
                ElseIf para(3).ToString.Replace("networktype=", "").ToLower = "prepaid2g" Then
                    cPara.MasterPage = "~/e_Service_MasterPage.master"
                    cPara.NetworkType = "pre"
                ElseIf para(3).ToString.Replace("networktype=", "").ToLower = "prepaid3g" Then
                    cPara.MasterPage = "~/e_Service_MasterPage.master"
                    cPara.NetworkType = "3g"
                ElseIf para(3).ToString.Replace("networktype=", "").ToLower = "postpaid3g" Then
                    cPara.MasterPage = "~/e_Service_MasterPage.master"
                    cPara.NetworkType = "3gpost"
                ElseIf para(3).ToString.Replace("networktype=", "").ToLower = "corporate3g" Then
                    cPara.MasterPage = "~/e_Service_MasterPage.master"
                    cPara.NetworkType = "3gpost"
                Else
                    cPara.MasterPage = "~/e_Service_MasterPage.master"
                    cPara.NetworkType = "pre"
                End If

                'Create LoginSession
                Dim SerialUserData As String = ""
                Dim sr As New XmlSerializer(GetType(CenParaDB.Appointment.CustomerWebAppointmentPara))
                Dim st As New MemoryStream()
                sr.Serialize(st, cPara)
                Dim b() As Byte = st.GetBuffer()
                SerialUserData = Convert.ToBase64String(b)

                HttpContext.Current.Response.Cookies.Clear()
                Dim fat As New FormsAuthenticationTicket(1, cPara.MobileNo, DateTime.Now, DateTime.Now.AddMinutes(60), False, SerialUserData.ToString)
                Dim ck As New System.Web.HttpCookie(".QIS-WebAppoint")
                ck.Value = FormsAuthentication.Encrypt(fat)
                ck.Expires = fat.Expiration
                HttpContext.Current.Response.Cookies.Add(ck)
                Dim cEng As New Engine.Appointment.AppointmentENG
                cEng.GetCustomerProfile(cPara.MobileNo, Session.SessionID())  'สำหรับทำการ Update ข้อมูลลง TB_CUSTOMER
                cEng = Nothing
            End If

            Response.Redirect("frm_DontReservStt.aspx?")
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Invalid Parameter,Please input parameter');", True)
            Exit Sub
        End If

        'Else

        'Session.RemoveAll()
        'cPara.MobileNo = txt_tel.Text.Trim
        'Session("lang") = dtplang.SelectedValue
        'If Session("lang") = "Thai" Then
        '    Session("currentlang") = "th-TH"

        'Else
        '    Session("currentlang") = "en-US"

        'End If

        'Response.Redirect("frm_DontReservStt.aspx")

        ' End If

    End Sub



End Class
