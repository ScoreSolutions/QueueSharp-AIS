Imports System
Imports System.Data
Imports System.Data.OleDb
Imports Engine.Configuration

Partial Class frmShopSkill
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            SetDDLSearchShop(uPara)
            SetMasterItemList()
            SetItemList()
            SetskillList(uPara, True)
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Shop Setup >> Skill", "AisWebConfig.frmShopSkill.aspx.Page_Load", Config.GetLoginHistoryID)
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

    Private Sub SetMasterItemList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetSkillList("1=1")

        If dt.Rows.Count = 0 Then
            dt.Columns.Add("id")
            dt.Columns.Add("skill")
        End If

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("skill") = "-----Select-----"
        dt.Rows.InsertAt(dr, 0)
        ddlMasterSkill.DataTextField = "skill"
        ddlMasterSkill.DataValueField = "id"
        ddlMasterSkill.DataSource = dt
        ddlMasterSkill.DataBind()

        dt.Dispose()
        eng = Nothing

    End Sub

    Private Sub SetItemList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetServiceActiveList("1=1")
        If dt.Rows.Count > 0 Then
            dgvSkillItem.DataSource = dt
            dgvSkillItem.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub SetSkillItemList(ByVal ShopID As String, ByVal SkillID As Long)
        Dim eng As New MasterENG
        Dim dt As New DataTable
        Dim engSh As New ShopSkillENG

        If ShopID <> "" Then
            dt = engSh.GetSkillItem(ShopID, SkillID)
        Else
            dt = eng.GetSkillItem(SkillID)
        End If
        If dt.Rows.Count > 0 Then
            For Each grv As DataGridItem In dgvSkillItem.Items
                Dim chk As CheckBox = grv.FindControl("chk")
                chk.Checked = False
                Dim lbl_item_id As Label = grv.FindControl("lbl_id")
                dt.DefaultView.RowFilter = "master_itemid='" & lbl_item_id.Text & "'"
                If dt.DefaultView.Count > 0 Then
                    chk.Checked = True
                End If
            Next
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub SetSkillList(ByVal uPara As CenParaDB.Common.UserProfilePara, ByVal IsClickSearch As Boolean)
        Dim eng As New ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " skill like '%" & txt_search.Text.Trim & "%'"

        End If
        dt = eng.SearchSkill(ddlSearchShop.SelectedValue, wh, uPara.USERNAME)
        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvdetail.Visible = False
        Else
            lblNotFound.Visible = False
            dgvdetail.Visible = True
            dgvdetail.DataSource = dt
            dgvdetail.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub ddlMasterSkill_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMasterSkill.SelectedIndexChanged
        If ddlMasterSkill.SelectedValue <> "0" Then
            Dim p As New CenParaDB.TABLE.TbSkillCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetSkillPara(ddlMasterSkill.SelectedValue)
            eng = Nothing

            If p.ID <> 0 Then
                txt_skill.Text = p.SKILL
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                chk_appointment.Checked = IIf(p.APPOINTMENT.Value = "1", True, False)

                SetSkillItemList("", p.ID)
            End If
            p = Nothing
            eng = Nothing
        Else
            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        ddlMasterSkill.SelectedValue = "0"
        txt_skill.Text = ""
        chk_active.Checked = False
        chk_appointment.Checked = False
        lblNotFound.Visible = False
        dgvdetail.Visible = True
        For Each grv As DataGridItem In dgvSkillItem.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            chk.Checked = False
        Next
        ctlBranchSelected1.ClearAllShop()

        txt_search.Text = String.Empty
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetSkillList(uPara, True)
        uPara = Nothing
    End Sub

    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Skill", "AisWebConfig.frmShopSkill.aspx.cmd_clear_Click", Config.GetLoginHistoryID)

    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvdetail.CurrentPageIndex = 0
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetSkillList(uPara, True)
        uPara = Nothing
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Skill", "AisWebConfig.frmShopSkill.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            ret = False
            Config.ShowAlert("Please select Shop", Me)
        ElseIf ddlMasterSkill.SelectedValue = "0" Then
            ret = False
            Config.ShowAlert("Please select Master Skill", Me)
        ElseIf txt_skill.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Skill", Me, txt_skill.ClientID)
        End If

        ret = False
        For Each grv As DataGridItem In dgvSkillItem.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked = True Then
                ret = True
                Exit For
            End If
        Next
        If ret = False Then
            Config.ShowAlert("Please select Service", Me)
        End If

        Return ret
    End Function

    Protected Sub dgvdetail_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvdetail.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)

            Dim p As New ShParaDB.TABLE.TbSkillShParaDB
            Dim eng As New Engine.Configuration.ShopSkillENG
            p = eng.GetShopskillPara(lbl_shop_id.Text, lbl_id.Text)
            If p.ID <> 0 Then
                ctlBranchSelected1.SelectShop = lbl_shop_id.Text
                If p.MASTER_SKILLID <> "" Then
                    Try
                        ddlMasterSkill.SelectedValue = p.MASTER_SKILLID
                    Catch ex As Exception

                    End Try
                End If
                chk_appointment.Checked = IIf(p.APPOINTMENT = "1", True, False)
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                txt_skill.Text = p.SKILL
                SetSkillItemList(lbl_shop_id.Text, lbl_id.Text)
            End If
            p = Nothing
            eng = Nothing

        End If
    End Sub

    Protected Sub cmd_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        If Validation() = True Then
            Dim p As New CenParaDB.TABLE.TbSkillCenParaDB
            p.ID = ddlMasterSkill.SelectedValue
            p.SKILL = txt_skill.Text.Trim
            If chk_appointment.Checked = True Then
                p.APPOINTMENT = "1"
            Else
                p.APPOINTMENT = "0"
            End If
            If chk_active.Checked = True Then
                p.ACTIVE_STATUS = "1"
            Else
                p.ACTIVE_STATUS = "0"
            End If

            Dim ret As Boolean = False
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "frmShopSkill_cmd_save.Click")

                If shTrans.Trans IsNot Nothing Then
                    Dim eng As New Engine.Configuration.ShopSkillENG
                    Dim SID As Long = eng.SaveShopSkill(p, uPara.USERNAME, c.Value, shTrans)
                    If SID > 0 Then

                        Dim sql As String = "delete from tb_skill_item where skill_id='" & SID & "'"
                        ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, shTrans.Trans)

                        For Each grv As DataGridItem In dgvSkillItem.Items
                            Dim lbl_item_id As Label = grv.FindControl("lbl_id")
                            Dim chk As New CheckBox
                            chk = CType(grv.FindControl("chk"), CheckBox)
                            If chk.Checked Then
                                Dim ishLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                                ishLnq.ChkDataByWhere("master_itemid='" & lbl_item_id.Text & "'", shTrans.Trans)
                                If ishLnq.ID <> 0 Then
                                    Dim siP As New ShParaDB.TABLE.TbSkillItemShParaDB
                                    siP.SKILL_ID = SID
                                    siP.ITEM_ID = ishLnq.ID

                                    ret = eng.SaveShopSkillItem(SID, siP, uPara.USER_PARA.ID, shTrans)
                                    If ret = False Then
                                        Config.ShowAlert(eng.ErrorMessage, Me)
                                        Exit For
                                    End If
                                    'Else
                                    '    ret = False
                                    '    Config.ShowAlert(ishLnq.ErrorMessage, Me)
                                    '    Exit For
                                End If
                            End If
                        Next
                    Else
                        shTrans.RollbackTransaction()
                        Config.ShowAlert(eng.ErrorMessage, Me)
                    End If
                    eng = Nothing

                    If ret = False Then
                        shTrans.RollbackTransaction()
                        Exit For
                    Else
                        shTrans.CommitTransaction()
                    End If
                End If
            Next
            If ret = True Then
                Config.ShowAlert("Save Complete.", Me)
                ClearData()
                SetSkillList(uPara, True)
                Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Skill", "AisWebConfig.frmShopSkill.aspx.cmd_save_Click", Config.GetLoginHistoryID)
            End If
            uPara = Nothing

        End If
    End Sub

    Protected Sub dgvdetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvdetail.PageIndexChanged
        dgvdetail.CurrentPageIndex = e.NewPageIndex
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetSkillList(uPara, True)
        uPara = Nothing
    End Sub
End Class
