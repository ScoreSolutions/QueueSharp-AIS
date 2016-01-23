Imports System.Data
Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO
Imports Security.Security

Partial Class frmLogin
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Focus()
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        If txtUsername.Text.Trim = "" Then
            Master.showError("Please enter Username.")
            Exit Sub
        End If

        If txtPassword.Text.Trim = "" Then
            Master.showError("Please enter Password.")
            Exit Sub
        End If
        
        If txtUsername.Text.ToUpper <> "ADMIN" Then
            Try
                Dim lPara As New CenParaDB.GateWay.LDAPResponsePara
                Dim eng As New Engine.Common.LoginENG
                lPara = eng.ReportGetLDAP(txtUsername.Text.Trim, txtPassword.Text.Trim)

                If lPara.RESULT = True Then
                    'Create User Profile
                    CreateUserProfile(txtUsername.Text.Trim)
                Else
                    Master.showError(lPara.RESPONSE_MSG)
                    Exit Sub
                End If
            Catch ex As Exception
                Master.showError("User authentication FAILED.<br/><br/>" & ex.Message)
                Exit Sub
            End Try
        Else
            'Admin
            Dim eng As New Engine.Common.LoginENG
            'Dim pw As String = Encrypt(txtPassword.Text.Trim)
            Dim pw As String = txtPassword.Text.Trim
            If eng.CheckUserProfile(txtUsername.Text.Trim, pw) = True Then
                'Create User Profile
                CreateUserProfile(txtUsername.Text.Trim)
            Else
                Master.showError(eng.ErrorMessage)
            End If
        End If
    End Sub

    Private Sub CreateUserProfile(ByVal UserName As String)
        Dim uPara As New CenParaDB.Common.LoginSessionPara
        uPara.USERNAME = UserName

        Dim eng As New Engine.Common.LoginENG
        eng.SaveLoginHistory(UserName, Request, "QisReports")
        uPara.LOGIN_HISTORY_ID = eng.LOGIN_HISTORY_ID

        'Create LoginSession
        Dim sr As New XmlSerializer(GetType(CenParaDB.Common.LoginSessionPara))
        Dim st As New MemoryStream()
        sr.Serialize(st, uPara)
        Dim b() As Byte = st.GetBuffer()
        Dim SerialUserData As String = Convert.ToBase64String(b)
        HttpContext.Current.Response.Cookies.Clear()

        Dim fat As New FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddMinutes(30), False, SerialUserData)
        Dim ck As New System.Web.HttpCookie(".AIS-REPORTS")
        ck.Value = FormsAuthentication.Encrypt(fat)
        ck.Expires = fat.Expiration
        HttpContext.Current.Response.Cookies.Add(ck)
        Response.Redirect("ReportApp/frmWelcomePage.aspx")
    End Sub
End Class
