Imports Engine.Configuration
Imports System.Data
Imports System.Drawing
Imports Engine

Partial Class frmMSTWebAppSetupRole
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetPropertyUser()
            SetPropertyProgram()
            GetRoleList()
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_rolename.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            Config.SaveTransLog("คลิกเมนู : Web Applicaiton Setup >> Role", "AisWebConfig.frmMSTWebAppSetupRole.aspx.Page_Load", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Sub SetPropertyUser()
        UserExist.Header = "Authorized User"
        UserExist.IsUserExist = True
        UserExist.RoleID = txt_id.Text

        UserNotExist.Header = "No Authorized User"
        UserNotExist.IsUserExist = False
        UserNotExist.RoleID = txt_id.Text

    End Sub

    Private Sub SetPropertyProgram()

        ProgramIDExist.Header = "Assigned Program"
        ProgramIDExist.IsProgramIDExist = True
        ProgramIDExist.RoleID = txt_id.Text

        ProgramIDNotExist.Header = "Unassigned Program"
        ProgramIDNotExist.IsProgramIDExist = False
        ProgramIDNotExist.RoleID = txt_id.Text
    End Sub

    Private Sub GetRoleList()
        Dim eng As New RoleENG
        Dim dt As New DataTable
        dt = eng.GetRoleList("1=1")
        If dt.Rows.Count > 0 Then
            dgvRoleList.DataSource = dt
            dgvRoleList.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New CenParaDB.TABLE.TbRoleCenParaDB
            With p
                .ID = txt_id.Text
                .ROLE_NAME = txt_rolename.Text.Trim
                .ROLE_DESC = txt_description.Text.Trim
                If chk_active.Checked = True Then
                    .ACTIVE = "Y"
                Else
                    .ACTIVE = "N"
                End If
            End With

            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            Dim eng As New Engine.Configuration.RoleENG
            If eng.SaveRole(uPara.USERNAME, p) = True Then
                txt_id.Text = p.ID
                GetRoleList()
                TabContainer1.Enabled = True
                Config.ShowAlert("Save Complete.", Me)
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If

            eng = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Web Applicaiton Setup >> Role", "AisWebConfig.frmMSTWebAppSetupRole.aspx.CmdSave_Click", Config.GetLoginHistoryID)
        End If


    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True

        If txt_rolename.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Role", Me, txt_rolename.ClientID)

        Else
            Dim eng As New RoleENG
            If eng.CheckDuplicateRole(txt_id.Text, txt_rolename.Text) = True Then
                Config.ShowAlert("Role is duplicate", Me, txt_rolename.ClientID)
                ret = False
            End If
            eng = Nothing
        End If

        Return ret
    End Function


    Private Sub ClearData()
        txt_id.Text = "0"
        txt_rolename.Text = ""
        txt_description.Text = ""
        chk_active.Checked = True
        TabContainer1.Enabled = False
        SetPropertyUser()
        SetPropertyProgram()
        UserExist.SetDataList()
        UserNotExist.SetDataList()
        ProgramIDExist.SetDataList()
        ProgramIDNotExist.SetDataList()
        chk_active.Checked = True
        lblNotFound.Visible = False
        dgvRoleList.Visible = True
        txt_search.Text = String.Empty
        SearchData()
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Web Applicaiton Setup >> Role", "AisWebConfig.frmMSTWebAppSetupRole.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : Web Applicaiton Setup >> Role", "AisWebConfig.frmMSTWebAppSetupRole.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Sub SearchData()
        Dim eng As New RoleENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " role_name like '%" & txt_search.Text.Trim & "%'"
            wh &= " or role_desc like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.GetRoleList(wh)
        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvRoleList.Visible = False
        Else
            lblNotFound.Visible = False
            dgvRoleList.Visible = True
            dgvRoleList.DataSource = dt
            dgvRoleList.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub dgvRoleList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvRoleList.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim p As New CenParaDB.TABLE.TbRoleCenParaDB
            Dim eng As New Engine.Configuration.RoleENG
            p = eng.GetRolePara(lbl_id.Text)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txt_rolename.Text = p.ROLE_NAME
                txt_description.Text = p.ROLE_DESC
                chk_active.Checked = IIf(p.ACTIVE = "Y", True, False)
            End If

            TabContainer1.Enabled = True
            SetPropertyUser()
            SetPropertyProgram()
            UserExist.SetDataList()
            UserNotExist.SetDataList()
            ProgramIDExist.SetDataList()
            ProgramIDNotExist.SetDataList()
            GetRoleList()

        End If
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Dim eng As New RoleENG
        Try
            Dim dt As New DataTable
            dt = UserNotExist.dtSelectList
            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            For cnt As Integer = 0 To dt.Rows.Count - 1
                Dim pItem As New CenParaDB.TABLE.TbRoleUserCenParaDB
                With pItem
                    .TB_ROLE_ID = txt_id.Text
                    .TB_USER_ID = dt.Rows(cnt).Item("id").ToString
                End With
                eng.SaveRoleUser(uPara.USERNAME, pItem)
            Next
            dt.Dispose()
            eng = Nothing
        Catch ex As Exception
            Config.ShowAlert(eng.ErrorMessage, Me)
        End Try
        SetPropertyUser()
        UserExist.SetDataList()
        UserNotExist.SetDataList()

    End Sub

    Protected Sub btnDeleteUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteUser.Click

        Dim eng As New RoleENG
        Try
            Dim dt As New DataTable
            dt = UserExist.dtSelectList
            For cnt As Integer = 0 To dt.Rows.Count - 1
                eng.DeleteRoleUserByID(dt.Rows(cnt).Item("id").ToString, txt_id.Text)
            Next
            dt.Dispose()
            eng = Nothing
        Catch ex As Exception
            Config.ShowAlert(eng.ErrorMessage, Me)
        End Try

        SetPropertyUser()
        UserExist.SetDataList()
        UserNotExist.SetDataList()

    End Sub

    Protected Sub btnAddProgram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProgram.Click
        Dim eng As New RoleENG
        Try
            Dim dt As New DataTable
            dt = ProgramIDNotExist.dtSelectList
            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            For cnt As Integer = 0 To dt.Rows.Count - 1
                Dim pItem As New CenParaDB.TABLE.TbRoleSysmenuCenParaDB
                With pItem
                    .TB_ROLE_ID = txt_id.Text
                    .SYSMENU_ID = dt.Rows(cnt).Item("id").ToString
                End With
                eng.SaveRoleProgramID(uPara.USERNAME, pItem)
            Next
            dt.Dispose()
            eng = Nothing
        Catch ex As Exception
            Config.ShowAlert(eng.ErrorMessage, Me)
        End Try

        SetPropertyProgram()
        ProgramIDExist.SetDataList()
        ProgramIDNotExist.SetDataList()
    End Sub

    Protected Sub btnDeleteProgram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteProgram.Click
        Dim eng As New RoleENG
        Try
            Dim dt As New DataTable
            dt = ProgramIDExist.dtSelectList
            For cnt As Integer = 0 To dt.Rows.Count - 1
                eng.DeleteRoleProgramByID(dt.Rows(cnt).Item("id").ToString, txt_id.Text)
            Next
            dt.Dispose()
            eng = Nothing
        Catch ex As Exception
            Config.ShowAlert(eng.ErrorMessage, Me)
        End Try

        SetPropertyProgram()
        ProgramIDExist.SetDataList()
        ProgramIDNotExist.SetDataList()
    End Sub

    Protected Sub dgvRoleList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvRoleList.PageIndexChanged
        dgvRoleList.CurrentPageIndex = e.NewPageIndex
        GetRoleList()
    End Sub
End Class
