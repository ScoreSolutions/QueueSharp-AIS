Imports System.Data
'Imports Security.Security

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Focus()

        'lblServerPath.Text = ConfigurationManager.AppSettings("tempvdo_path") ' Server.MapPath(ConfigurationManager.AppSettings("tempvdo_path"))
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
                Dim res As New CenParaDB.GateWay.LDAPResponsePara
                res = utl.CheckLDAPLogin(txtUsername.Text, txtPassword.Text)
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
            tmpsql = " and [password] = '" & utl.FixDB(pw) & "' "
        End If

        Dim dt As New DataTable
        sql = "select x.id,isnull(fname,'') + ' ' + isnull(lname,'') as fullname,isnull(user_code,'') as user_code,username,isnull(is_admin,'0') as is_admin,isnull(view_others_vdo,'') as view_others_vdo from TB_USER x where username = '" & utl.FixDB(txtUsername.Text) & "' " & tmpsql & "  and active_status = 1"
        dt = utl.GetDatatable(sql)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim eng As New Engine.Common.LoginENG
            eng.SaveLoginHistory(txtUsername.Text, Request, "QisQM")

            Dim mUser As New utils.User
            With mUser
                .username = txtUsername.Text
                .user_id = dt.Rows(0).Item("id").ToString
                .user_code = dt.Rows(0).Item("user_code").ToString
                .fulllname = dt.Rows(0).Item("fullname").ToString
                .counter_id = ""
                .counter_name = "QM"
                .ip_address = ""
                .is_admin = IIf(dt.Rows(0).Item("is_admin").ToString = "1", True, False)
                .view_others_vdo = dt.Rows(0).Item("view_others_vdo").ToString
                .login_history_id = eng.LOGIN_HISTORY_ID
            End With
            eng = Nothing
            Session("MyUser") = mUser
            

            'Try
            '    utl.DeleteVDOofUser(mUser.username, ConfigurationManager.AppSettings("tempvdo_path"))
            '    'utl.DeleteVDOofUser(mUser.username, ConfigurationManager.AppSettings("QMTempFolder") & mUser.username)
            'Catch ex As Exception
            'End Try
            utl.executeSQL("use " & Master.Conn.Database & ";update TB_User set password='" & pw & "' where id='" & mUser.user_id & "' ", Master.Conn)
            Response.Redirect("default.aspx")
        Else
            Master.showError("This user has not been configured to use the QM System.\n Please contact your System Aministrator.")
            Session("MyUser") = Nothing
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        lblConnInfo.Text = Master.Conn.DataSource
    End Sub
End Class
