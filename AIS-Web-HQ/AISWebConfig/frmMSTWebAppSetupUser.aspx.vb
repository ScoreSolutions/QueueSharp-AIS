Imports System
Imports System.Data
Imports Engine.Common
Imports System.Data.OleDb

Partial Class frmMSTWebAppSetupUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                BindTitle()
                SetGridviewUserList()
                SetNonAuthrorizedShop()

                btnAddUser.Enabled = False
                btnDeleteUser.Enabled = False
                gvUserRight.Enabled = False

                txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
                txt_user_code.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
                txt_user_first_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
                txt_user_last_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
                txt_user_position.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
                txt_user_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")

            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Web Applicaiton Setup >> User", "AisWebConfig.frmMSTWebAppSetupUser.aspx.Page_Load", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Sub SetGridviewUserList()
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " fname like '%" & txt_search.Text.Trim & "%'"
            wh += " or lname like '%" & txt_search.Text.Trim & "%'"
            wh += " or user_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or position like '%" & txt_search.Text.Trim & "%'"
            wh += " or username like '%" & txt_search.Text.Trim & "%'"
        End If
        Dim eng As New Engine.Configuration.MasterENG
        Dim dt As New DataTable
        dt = eng.GetMasterUserList(wh)
        'If dt.Rows.Count > 0 Then
        dt.Columns.Add("staff_name")
        For i As Integer = 0 To dt.Rows.Count - 1
            Try
                Dim StaffName As String = ""
                If Convert.IsDBNull(dt.Rows(i)("fname")) = True And Convert.IsDBNull(dt.Rows(i)("lname")) = True Then
                    StaffName = dt.Rows(i)("username")
                Else
                    StaffName = dt.Rows(i)("fname").ToString & " " & dt.Rows(i)("lname").ToString
                End If

                dt.Rows(i)("staff_name") = StaffName
            Catch ex As Exception
                dt.Rows(i)("staff_name") = "No data found"
            End Try
        Next

        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvUserList.Visible = False
        Else
            lblNotFound.Visible = False
            dgvUserList.Visible = True
            dgvUserList.DataSource = dt
            dgvUserList.DataBind()
        End If

       
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub BindTitle()
        Try
            Dim dt As New DataTable
            Dim tEng As New Engine.Configuration.MasterENG
            dt = tEng.GetMasterTitleList("1=1")
            If dt.Rows.Count > 0 Then
                cbo_titlenm.DataSource = dt
            Else
                dt.Columns.Add("id")
                dt.Columns.Add("title_name")

                Dim dr As DataRow = dt.NewRow
                dr("id") = "0"
                dr("title_name") = " -"
                dt.Rows.InsertAt(dr, 0)

                cbo_titlenm.DataSource = dt
            End If
            cbo_titlenm.DataTextField = "title_name"
            cbo_titlenm.DataValueField = "id"
            cbo_titlenm.DataBind()
            tEng = Nothing
            dt.Dispose()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindTitle Error !!,Please Check Function');", True)
        End Try
    End Sub


    Protected Sub dgvUserList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvUserList.PageIndexChanged
        dgvUserList.CurrentPageIndex = e.NewPageIndex
        SetGridviewUserList()
    End Sub

    Protected Sub cmd_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        If Validation() = True Then
            Dim u As New CenParaDB.TABLE.TbUserCenParaDB
            u.ID = txt_id.Text.Trim
            u.TITLE_ID = cbo_titlenm.SelectedValue
            u.USER_CODE = txt_user_code.Text.Trim
            u.FNAME = txt_user_first_name.Text.Trim
            u.LNAME = txt_user_last_name.Text.Trim
            u.POSITION = txt_user_position.Text.Trim
            u.USERNAME = txt_user_name.Text.Trim
            If chk_active.Checked = True Then
                u.ACTIVE_STATUS = "1"
            Else
                u.ACTIVE_STATUS = "0"
            End If

            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim uEng As New Engine.Configuration.MasterENG
            Dim ret As Boolean = uEng.SaveMasterUser(uPara.USERNAME, u)
            If ret = True Then
                txt_id.Text = u.ID
                SetGridviewUserList()
                Config.ShowAlert("Save Complete", Me)
                'ClearTextBox()

                btnAddUser.Enabled = True
                btnDeleteUser.Enabled = True
                gvUserRight.Enabled = True

                SetAuthrorizedShop()
                SetNonAuthrorizedShop()
            Else
                Config.ShowAlert(uEng.ErrorMessage, Me)
            End If
            uPara = Nothing
            uEng = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Web Applicaiton Setup >> User", "AisWebConfig.frmMSTWebAppSetupUser.aspx.cmd_save_Click", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Sub SetAuthrorizedShop()
        Dim eng As New Engine.Configuration.MasterENG
        Dim dt As New DataTable
        dt = eng.GetShopList("sh.id in (select tb_shop_id from TB_USER_SHOP where tb_user_id='" & txt_id.Text.Trim & "')")
        If dt.Rows.Count > 0 Then
            gvUserLeft.DataSource = dt
            gvUserLeft.DataBind()
        Else
            gvUserLeft.DataSource = Nothing
            gvUserLeft.DataBind()
        End If
        eng = Nothing
        dt.Dispose()
    End Sub

    Private Sub SetNonAuthrorizedShop()
        Dim eng As New Engine.Configuration.MasterENG
        Dim dt As New DataTable
        dt = eng.GetShopList("sh.id not in (select tb_shop_id from TB_USER_SHOP where tb_user_id='" & txt_id.Text.Trim & "')")
        If dt.Rows.Count > 0 Then
            gvUserRight.DataSource = dt
            gvUserRight.DataBind()
        Else
            gvUserRight.DataSource = Nothing
            gvUserRight.DataBind()
        End If
        eng = Nothing
        dt.Dispose()
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If txt_user_code.Text.Trim = "" Then
            Config.ShowAlert("Please input User Code", Me, txt_user_code.ClientID)
            ret = False
        ElseIf txt_user_first_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Name", Me, txt_user_first_name.ClientID)
            ret = False
        ElseIf txt_user_last_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Surname", Me, txt_user_last_name.ClientID)
            ret = False
        ElseIf txt_user_position.Text.Trim = "" Then
            Config.ShowAlert("Please input Position", Me, txt_user_position.ClientID)
            ret = False
        ElseIf txt_user_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Username", Me, txt_user_name.ClientID)
            ret = False
        Else
            Dim eng As New Engine.Configuration.MasterENG
            If eng.CheckDuplicateUsername(txt_id.Text, txt_user_name.Text) = True Then
                Config.ShowAlert("Username is duplicate", Me, txt_user_name.ClientID)
                ret = False
            End If
            eng = Nothing
        End If

        Return ret
    End Function

    Private Sub ClearTextBox()
        txt_user_code.Text = ""
        txt_user_first_name.Text = ""
        txt_user_last_name.Text = ""
        txt_user_position.Text = ""
        txt_user_name.Text = ""
        txt_id.Text = "0"
        chk_active.Checked = True
        lblNotFound.Visible = False
        dgvUserList.Visible = True
        BindTitle()

        btnAddUser.Enabled = False
        btnDeleteUser.Enabled = False
        gvUserRight.Enabled = False
        SetAuthrorizedShop()
        SetNonAuthrorizedShop()
        txt_user_code.Enabled = True

        txt_search.Text = String.Empty
        SetGridviewUserList()
    End Sub

    Protected Sub dgvUserList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvUserList.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim p As New CenParaDB.TABLE.TbUserCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetMasterUserPara(lbl_id.Text)
            If p.ID <> 0 Then
                txt_user_code.Text = p.USER_CODE
                txt_user_first_name.Text = p.FNAME
                txt_user_last_name.Text = p.LNAME
                txt_user_position.Text = p.POSITION
                txt_user_name.Text = p.USERNAME
                txt_id.Text = p.ID
                If p.ACTIVE_STATUS = "1" Then
                    chk_active.Checked = True
                Else
                    chk_active.Checked = False
                End If

                btnAddUser.Enabled = True
                btnDeleteUser.Enabled = True
                gvUserRight.Enabled = True

                SetAuthrorizedShop()
                SetNonAuthrorizedShop()
            End If
            p = Nothing
            eng = Nothing
        End If
    End Sub

    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        ClearTextBox()
        Config.SaveTransLog("เคลียร์ข้อมูล : Web Applicaiton Setup >> User", "AisWebConfig.frmMSTWebAppSetupUser.aspx.cmd_clear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        SetGridviewUserList()
        Config.SaveTransLog("ค้นหาข้อมูล : Web Applicaiton Setup >> User", "AisWebConfig.frmMSTWebAppSetupUser.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim eng As New Engine.Configuration.RoleENG
        Dim ret As Boolean = False
        Dim _err As String = ""
        For Each grv As GridViewRow In gvUserRight.Rows
            Dim lbl_id As Label = grv.FindControl("lblID")
            Dim chk As CheckBox = DirectCast(grv.FindControl("ChkSelect"), CheckBox)
            If chk.Checked = True Then
                ret = eng.SaveUserShop(lbl_id.Text, txt_id.Text.Trim, uPara.USERNAME, trans)
                If ret = False Then
                    _err = eng.ErrorMessage
                    Exit For
                End If
            End If
        Next

        If ret = True Then
            trans.CommitTransaction()
        Else
            trans.RollbackTransaction()
            If _err.Trim <> "" Then
                Config.ShowAlert(_err, Me)
            End If
        End If
        uPara = Nothing
        eng = Nothing

        SetAuthrorizedShop()
        SetNonAuthrorizedShop()
    End Sub

    Protected Sub btnDeleteUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteUser.Click
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim eng As New Engine.Configuration.RoleENG
        Dim ret As Boolean = False
        Dim _err As String = ""
        For Each grv As GridViewRow In gvUserLeft.Rows
            Dim lbl_id As Label = grv.FindControl("lblID")
            Dim chk As CheckBox = DirectCast(grv.FindControl("ChkSelect"), CheckBox)
            If chk.Checked = True Then
                ret = eng.DeleteUserShopByID(txt_id.Text.Trim, lbl_id.Text)
                If ret = False Then
                    _err = eng.ErrorMessage
                    Exit For
                End If
            End If
        Next
        If ret = False Then
            If _err.Trim <> "" Then
                Config.ShowAlert(_err, Me)
            End If
        End If
        uPara = Nothing
        eng = Nothing

        SetAuthrorizedShop()
        SetNonAuthrorizedShop()
    End Sub
End Class
