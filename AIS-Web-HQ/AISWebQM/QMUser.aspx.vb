Imports System.Data
Partial Class QMUser
    Inherits System.Web.UI.Page
    Dim utl As New utils
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not utl.CurrentUserIsAdmin(Session("MyUser")) Then
            Response.Redirect("NotAllow.aspx")
        End If

        Dim LoginHisID As Long = DirectCast(Session("MyUser"), utils.User).login_history_id
        Engine.Common.FunctionEng.SaveTransLog(LoginHisID, "QM.QMUser.Load", "คลิกเมนู Manage QM User")
        lblInfo.Visible = False
        If Not IsPostBack Then
            BindGrid()
        End If
        Title = Version

        txtUsername.Attributes.Add("onkeypress", "return clickButton(event,'" + btnCheck.ClientID + "') ")
    End Sub

    Sub BindGrid()
        gvShop.DataSource = utl.GetShopList()
        gvShop.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtUsername.Text.Trim = "" Then
            Master.showError("Username is required")
            lblInfo.Visible = False
            Exit Sub
        End If

        Dim usr As utils.User = CType(Session("MyUser"), utils.User)
        Dim tmp As String = GenAllowShopList()
        Dim adm As Int16 = 0
        If chkAdmin.Checked Then
            adm = 1
        End If
        Dim tmpPass As String = ""
        Try
            If txtPassword.Text.Trim <> "" Then
                tmpPass = " ,password='" & txtPassword.Text.Trim & "' "
            End If
            Dim sql As String = "use " & Master.Conn.Database & ";if exists(select username from TB_USER where username='" & txtUsername.Text.Trim & "') begin update TB_USER set is_admin='" & adm & "'" & tmpPass & ", view_others_vdo='" & tmp & "',update_by='" & usr.user_id & "' where username='" & txtUsername.Text.Trim & "' end else begin insert into TB_USER (create_by,username,password,active_status,view_others_vdo,is_admin) values('" & usr.user_id & "','" & txtUsername.Text.Trim & "','" & txtPassword.Text.Trim & "','1','" & tmp & "','" & adm & "') end"
            utl.executeSQL(sql, Master.Conn)

            Dim uDt As New DataTable
            uDt = utl.getDataTable("select view_others_vdo from tb_user where username = '" & txtUsername.Text.Trim & "'", Master.Conn)
            If uDt.Rows.Count > 0 Then
                If Convert.IsDBNull(uDt.Rows(0)("view_others_vdo")) = False Then usr.view_others_vdo = uDt.Rows(0)("view_others_vdo").ToString
                Session("MyUser") = usr
            End If

            Master.showError("User has been updated successfully", False)
        Catch ex As Exception
            Master.showError(ex.Message)
        End Try

    End Sub

    Function GenAllowShopList() As String
        Dim tmp As String = ""
        With gvShop
            For i As Integer = 0 To gvShop.Rows.Count - 1
                Dim chk As CheckBox = CType(.Rows(i).FindControl("chkviewOthers"), CheckBox)
                If chk.Checked Then
                    Dim val As String
                    val = CType(.Rows(i).FindControl("lblShop"), Label).Text
                    If tmp.Length = 0 Then
                        tmp = val
                    Else
                        tmp &= "," & val
                    End If
                End If
            Next
        End With
        Return tmp
    End Function

    Protected Sub btnCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        If txtUsername.Text.Trim = "" Then
            Master.showError("Username is required")
            lblInfo.Visible = False
            Exit Sub
        End If
        Dim dt As New DataTable
        dt = utl.GetUserInfo(txtUsername.Text.Trim, Master.Conn)
        If dt.Rows.Count = 0 Then

            lblInfo.Text = "This is a NEW User."
            lblInfo.Visible = True
            btnSave.Text = "Save"
            Dim chk As New CheckBox
            chkAdmin.Checked = False
            For i As Integer = 0 To gvShop.Rows.Count - 1
                chk = New CheckBox
                chk = CType(gvShop.Rows(i).FindControl("chkviewOthers"), CheckBox)
                chk.Checked = False
            Next
        Else
            If dt.Rows(0)("is_admin").ToString = "1" Then
                chkAdmin.Checked = True
            Else
                chkAdmin.Checked = False
            End If
            lblInfo.Text = "This is an EXISTING User."
            lblInfo.Visible = True
            btnSave.Text = "Update"

            Dim tmpVDO() As String
            tmpVDO = Split(dt.Rows(0)("view_others_vdo").ToString, ",")

            Dim chk As New CheckBox
            For i As Integer = 0 To gvShop.Rows.Count - 1
                chk = New CheckBox
                chk = CType(gvShop.Rows(i).FindControl("chkviewOthers"), CheckBox)
                If Array.LastIndexOf(tmpVDO, CType(gvShop.Rows(i).FindControl("lblShop"), Label).Text) >= 0 Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If
            Next

        End If
    End Sub
End Class
