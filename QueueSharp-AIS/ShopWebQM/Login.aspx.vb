Imports System.Data
Imports Engine.Common

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Focus()

        lblServerPath.Text = Server.MapPath(ConfigurationManager.AppSettings("TempVDOPath"))
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim utl As New utils
        If txtUsername.Text.Trim = "" Then
            Master.showError("Please enter Username.")
            'txtUsername.Focus()
            Exit Sub
        End If

        If txtPassword.Text.Trim = "" Then
            Master.showError("Please enter Password.")
            'txtPassword.Focus()
            Exit Sub
        End If


        Dim sql, tmpsql As String
        tmpsql = ""
        Dim pw As String = txtPassword.Text.Trim
        If txtUsername.Text.ToUpper <> "ADMIN" Then
            Try
                'Dim res As utils.FunctionReturn = utl.CheckLDAPLogin(txtUsername.Text, txtPassword.Text)
                Dim res As New ShParaDb.ShopWebService.LDAPResponsePara
                Dim eng As New Engine.CallWebService.ShopWebServiceENG
                res = eng.LDAPAuth(txtUsername.Text, txtPassword.Text)
                eng = Nothing
                If res.RESULT = False Then
                    'Login failed
                    Master.showError("User authentication FAILED.")
                    Exit Sub
                End If
            Catch ex As Exception
                Master.showError("User authentication FAILED. \n" & ex.Message)
                Exit Sub
            End Try
        Else
            tmpsql = " and [password] = '" & Engine.Common.FunctionEng.EnCripPwd(pw) & "' "
        End If

        Dim dt As New DataTable
        sql = "select x.id,isnull(fname,'') + ' ' + isnull(lname,'') as fullname,isnull(user_code,'') as user_code,username," & vbNewLine
        sql += " (select top 1 config_value from tb_setting where config_name='s_name_en') shop_name_en," & vbNewLine
        sql += " (select top 1 config_value from tb_setting where config_name='s_name_th') shop_name_th," & vbNewLine
        sql += " (select top 1 config_value from tb_setting where config_name='ShopAbbCode') shop_abb," & vbNewLine
        sql += " (select top 1 config_value from tb_setting where config_name='QMSplitFolder') QMSplitFolder " & vbNewLine
        sql += " from TB_USER x " & vbNewLine
        sql += " where username = '" & txtUsername.Text & "' " & tmpsql & "  and active_status = 1"
        dt = Engine.Common.FunctionEng.GetShopDataTable(sql)
        If dt.Rows.Count > 0 Then
            Dim LoginHisID As Long = Engine.Common.FunctionEng.SaveQisDBLoginHistory(txtUsername.Text.Trim, Request, "ShopWebQM", dt.Rows(0)("shop_abb"))

            Dim mUser As New utils.User
            With mUser
                .username = txtUsername.Text
                .user_id = dt.Rows(0).Item("id").ToString
                .user_code = dt.Rows(0).Item("user_code").ToString
                .fulllname = dt.Rows(0).Item("fullname").ToString
                .ip_address = Request.UserHostAddress
                .login_history_id = LoginHisID
                .ShopAbb = dt.Rows(0)("shop_abb")
                .ShopNameEN = dt.Rows(0)("shop_name_en")
                .ShopNameTH = dt.Rows(0)("shop_name_th")
                .QMSplitFolder = dt.Rows(0)("QMSplitFolder")
                .TempVDOPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempVDOPath") & "/" & txtUsername.Text)
            End With
            Session("MyUser") = mUser

            Try
                utl.DeleteVDOofUser(mUser.TempVDOPath)
            Catch ex As Exception
            End Try
            FunctionEng.ExecuteShopNonQuery("update TB_User set password='" & Engine.Common.FunctionEng.EnCripPwd(pw) & "' where id='" & mUser.user_id & "' ")
            Response.Redirect("logbyshop.aspx?rnd=" & DateTime.Now.Millisecond)
        Else
            Master.showError("This user has not been configured to use the QM System.\n Please contact your System Aministrator.")
            Session("MyUser") = Nothing
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        lblConnInfo.Text = Master.Conn.DataSource
    End Sub
End Class
