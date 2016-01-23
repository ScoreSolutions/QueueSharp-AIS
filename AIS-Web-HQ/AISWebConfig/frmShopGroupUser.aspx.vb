Imports System.Data

Partial Class frmShopGroupUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim sh As New CenParaDB.TABLE.TbShopCenParaDB
            Dim shEng As New Engine.Configuration.MasterENG
            sh = shEng.GetShopPara(Request("ShopID"))
            If sh.ID <> 0 Then
                lblScreenName.Text = "Shop Setup >> Group User"
            End If
            sh = Nothing
            shEng = Nothing

            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                'Code at Form_Load
                SetGridMenu()
                SetDDLSearchShop(uPara)
                SetGroupUserList(uPara, True)

                txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
                txtGroupCode.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdsave.ClientID + "') ")
                txtGroupName.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdsave.ClientID + "') ")
            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopGroupUser.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub SetDDLSearchShop(ByVal uPara As CenParaDB.Common.UserProfilePara)
        Dim lEng As New Engine.Common.LoginENG
        Dim shDt As New DataTable
        shDt = lEng.GetShopListByUser(uPara.USERNAME)
        If shDt.Rows.Count > 0 Then
            shDt.DefaultView.Sort = "shop_name_en"
            shDt = shDt.DefaultView.ToTable
            'Else
            '    shDt.Columns.Add("id")
            '    shDt.Columns.Add("shop_name_en")
        End If

        Dim dr As DataRow = shDt.NewRow
        dr("id") = "0"
        dr("shop_name_en") = " All Shop "
        shDt.Rows.InsertAt(dr, 0)

        ddlSearchShop.DataTextField = "shop_name_en"
        ddlSearchShop.DataValueField = "id"
        ddlSearchShop.DataSource = shDt
        ddlSearchShop.DataBind()

        shDt.Dispose()
        lEng = Nothing
    End Sub

    Private Sub SetGridMenu()
        Dim eng As New Engine.Configuration.MasterENG
        Dim dt As New DataTable
        dt = eng.GetMasterShopMenu
        dgvMenu.DataSource = dt
        dgvMenu.DataBind()
    End Sub

    Private Sub SetGroupUserList(ByVal uPara As CenParaDB.Common.UserProfilePara, ByVal IsClickSearch As Boolean)
        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " group_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or group_name like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.SearchGroupUser(ddlSearchShop.SelectedValue, wh, uPara.USERNAME)
        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvGroupUser.Visible = False
        Else
            lblNotFound.Visible = False
            dgvGroupUser.Visible = True
            dgvGroupUser.DataSource = dt
            dgvGroupUser.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub
    'sql = "select id,group_code,group_name,active_status,case when active_status = 1 then 'Active' else 'Inactive' end as status from TB_groupuser order by group_code"
    'sql = "select id,menu_type,menu_name from TB_menu order by menu_type,menu_name"

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvGroupUser.CurrentPageIndex = 0
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetGroupUserList(uPara, True)
        uPara = Nothing
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Group User", "AisWebConfig.frmShopGroupUser.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New ShParaDB.TABLE.TbGroupuserShParaDB
            p.ID = hidGroiupUserID.Value
            p.GROUP_CODE = txtGroupCode.Text.Trim
            p.GROUP_NAME = txtGroupName.Text.Trim

            If chk_active.Checked = True Then
                p.ACTIVE_STATUS = "1"
            Else
                p.ACTIVE_STATUS = "0"
            End If
            
            Dim ret As Boolean = False
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "frmShopGroupUser_cmd_save.Click")

                If shTrans.Trans IsNot Nothing Then
                    Dim eng As New Engine.Configuration.ShopMenuENG
                    Dim gId As Long = eng.SaveGroupUser(p, GetSelectMenu, uPara, shTrans)
                    If gId > 0 Then
                        shTrans.CommitTransaction()
                        hidGroiupUserID.Value = gId
                        SetGroupUserList(uPara, True)
                        Config.ShowAlert("Save Complete.", Me)
                    Else
                        shTrans.RollbackTransaction()
                        Config.ShowAlert(eng.ErrorMessage, Me)
                    End If
                    eng = Nothing
                End If
            Next

            uPara = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.CmdSave_Click", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Function GetSelectMenu() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("MenuID")
        For Each grv As DataGridItem In dgvMenu.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked = True Then
                Dim dr As DataRow = dt.NewRow

                Dim lbl_id As Label = grv.FindControl("lbl_id")
                dr("MenuID") = lbl_id.Text
                dt.Rows.Add(dr)
            End If
        Next

        Return dt
    End Function

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            ret = False
            Config.ShowAlert("Please select Shop", Me)
        ElseIf txtGroupCode.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Group Code", Me, txtGroupCode.ClientID)
        ElseIf txtGroupName.Text.Trim = "" Then
            Config.ShowAlert("Please input Group Name", Me, txtGroupName.ClientID)
            ret = False
        ElseIf CheckSelectMenu() = False Then
            Config.ShowAlert("Please select the menu", Me, txtGroupName.ClientID)
            ret = False
        Else
            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
                If eng.GetShopGroupUserDup(c.Value, txtGroupCode.Text.Trim, txtGroupName.Text.Trim, hidGroiupUserID.Value) = True Then
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(c.Value)
                    Config.ShowAlert(eng.ErrMessage & " on Shop " & shLnq.SHOP_NAME_EN, Me)
                    ret = False
                    eng = Nothing
                    shLnq = Nothing
                    Exit For
                End If
            Next
            eng = Nothing
        End If

        Return ret
    End Function

    Private Function CheckSelectMenu() As Boolean
        Dim valueReturn As Boolean = False
        If dgvMenu.Items.Count = 0 Then
            Return valueReturn
        End If
        For i As Integer = 0 To dgvMenu.Items.Count - 1
            Dim chk As CheckBox = dgvMenu.Items.Item(i).FindControl("chk")
            If chk.Checked = True Then
                valueReturn = True
                Exit For
            End If
        Next

        Return valueReturn
    End Function


    Private Sub ClearData()
        hidGroiupUserID.Value = 0
        txtGroupCode.Text = ""
        txtGroupName.Text = ""
        chk_active.Checked = True
        lblNotFound.Visible = False
        dgvGroupUser.Visible = True
        For Each grv As DataGridItem In dgvMenu.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            chk.Checked = False
        Next

        txt_search.Text = String.Empty
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetGroupUserList(uPara, True)
        uPara = Nothing
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Group User", "AisWebConfig.frmShopGroupUser.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub dgvGroupUser_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvGroupUser.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
            Dim lbl_group_code As Label = DirectCast(e.Item.FindControl("lbl_group_code"), Label)
            Dim lbl_group_name As Label = DirectCast(e.Item.FindControl("lbl_group_name"), Label)

            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(lbl_shop_id.Text, "ShopEventScheduleSettingENG.GetShopGroupUserPara")

            Dim p As New ShParaDB.TABLE.TbGroupuserShParaDB
            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            p = eng.GetShopGroupUserPara(lbl_id.Text, shTrans)
            If p.ID <> 0 Then
                ctlBranchSelected1.SelectShop = lbl_shop_id.Text

                hidGroiupUserID.Value = p.ID
                txtGroupCode.Text = p.GROUP_CODE
                txtGroupName.Text = p.GROUP_NAME
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)

                SetGroupUserMenu(lbl_id.Text, shTrans)
            End If
            p = Nothing
            eng = Nothing
        End If
    End Sub

    Private Sub SetGroupUserMenu(ByVal vID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
        Dim dt As New DataTable
        If shTrans.Trans IsNot Nothing Then
            Dim lnq As New ShLinqDB.TABLE.TbGroupuserMenuShLinqDB
            dt = lnq.GetDataList("group_id='" & vID & "'", "", shTrans.Trans)
            lnq = Nothing

            If dt.Rows.Count > 0 Then
                For Each grv As DataGridItem In dgvMenu.Items
                    Dim lbl_id As Label = grv.FindControl("lbl_id")
                    Dim chk As CheckBox = grv.FindControl("chk")
                    dt.DefaultView.RowFilter = "menu_id='" & lbl_id.Text & "'"
                    If dt.DefaultView.Count > 0 Then
                        chk.Checked = True
                    Else
                        chk.Checked = False
                    End If
                Next
            End If
            dt.Dispose()
        End If
    End Sub

    Protected Sub dgvGroupUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvGroupUser.PageIndexChanged
        dgvGroupUser.CurrentPageIndex = e.NewPageIndex
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetGroupUserList(uPara, True)
        uPara = Nothing
    End Sub
End Class
