Imports Engine.Configuration
Imports System.Data
Imports System.Drawing

Partial Class frmMSTSkill
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetServiceList()
            GetSkillList()
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_skill.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")

            Config.SaveTransLog("คลิกเมนู : Central Setup >> Skill", "AisWebConfig.frmMSTSkill.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub SetServiceList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetServiceActiveList("1=1")
        If dt.Rows.Count > 0 Then
            chkService.DataTextField = "item_name"
            chkService.DataValueField = "id"
            chkService.DataSource = dt
            chkService.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub GetSkillList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetSkillList("1=1")
        If dt.Rows.Count > 0 Then
            dgvSkillList.DataSource = dt
            dgvSkillList.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New CenParaDB.TABLE.TbSkillCenParaDB
            With p
                .ID = txt_id.Text
                .SKILL = txt_skill.Text.Trim
                If chk_appointment.Checked = True Then
                    .APPOINTMENT = "1"
                Else
                    .APPOINTMENT = "0"
                End If
                If chk_active.Checked = True Then
                    .ACTIVE_STATUS = "1"
                Else
                    .ACTIVE_STATUS = "0"
                End If
            End With

            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            Dim eng As New Engine.Configuration.MasterENG
            If eng.SaveSkill(uPara.USERNAME, p) = True Then
                eng.DeleteSkillItem(p.ID)
                For cnt As Integer = 0 To chkService.Items.Count - 1
                    If chkService.Items(cnt).Selected = True Then
                        Dim pItem As New CenParaDB.TABLE.TbSkillItemCenParaDB
                        With pItem
                            .ID = 0
                            .SKILL_ID = p.ID
                            .ITEM_ID = chkService.Items(cnt).Value
                        End With
                        eng.SaveSkillItem(uPara.USERNAME, pItem)
                    End If
                Next

                GetSkillList()


                Dim shDT As New DataTable
                shDT = Engine.Common.FunctionEng.GetActiveShopDDL
                If shDT.Rows.Count > 0 Then
                    For i As Integer = 0 To shDT.Rows.Count - 1
                        Dim ret As Boolean = False
                        Dim _shopid As String = shDT.Rows(i).Item("ID").ToString
                        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(_shopid)
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Engine.Common.FunctionEng.GetShTransction(_shopid, "frmMSTSkill.CmdSave_Click")
                        If shTrans.Trans IsNot Nothing Then
                            Dim shSkillLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                            shSkillLnq.ChkDataByWhere("master_skillid='" & p.ID & "'", shTrans.Trans)
                            If shSkillLnq.ID <> 0 Then
                                shSkillLnq.MASTER_SKILLID = p.ID
                                shSkillLnq.SKILL = p.SKILL
                                shSkillLnq.APPOINTMENT = p.APPOINTMENT
                                shSkillLnq.ACTIVE_STATUS = p.ACTIVE_STATUS
                                If shSkillLnq.ID <> 0 Then
                                    ret = shSkillLnq.UpdateByPK(uPara.USERNAME, shTrans.Trans)
                                End If

                                If ret Then
                                    Dim sql As String = "delete from tb_skill_item where skill_id='" & shSkillLnq.ID & "'"
                                    ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, shTrans.Trans)
                                    For cnt As Integer = 0 To chkService.Items.Count - 1
                                        If chkService.Items(cnt).Selected = True Then
                                            Dim ItemLnq As New ShLinqDB.TABLE.TbSkillItemShLinqDB
                                            With ItemLnq
                                                .ID = 0
                                                .SKILL_ID = p.ID
                                                .ITEM_ID = chkService.Items(cnt).Value
                                            End With
                                            ret = ItemLnq.InsertData(0, shTrans.Trans)
                                        End If
                                    Next
                                End If

                            End If
                        End If

                        If ret Then
                            shTrans.CommitTransaction()
                        Else
                            shTrans.RollbackTransaction()
                        End If
                    Next
                End If


                Config.ShowAlert("Save Complete.", Me)
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If

            eng = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Central Setup >> Skill", "AisWebConfig.frmMSTSkill.aspx.CmdSave_Click", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        Dim IsSelect As Boolean = False
        For i As Integer = 0 To chkService.Items.Count - 1
            If chkService.Items(i).Selected Then
                IsSelect = True
                Exit For
            End If
        Next
        If txt_skill.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input skill", Me, txt_skill.ClientID)
        ElseIf IsSelect = False Then
            ret = False
            Config.ShowAlert("Please input service", Me)
        Else
            Dim eng As New MasterENG
            If eng.CheckDuplicateSkill(txt_id.Text, txt_skill.Text) = True Then
                Config.ShowAlert("Skill is duplicate", Me, txt_skill.ClientID)
                ret = False
            End If
            eng = Nothing
        End If

        Return ret
    End Function


    Private Sub ClearData()
        txt_id.Text = "0"
        txt_skill.Text = ""
        chk_appointment.Checked = False
        chk_active.Checked = True
        lblNotFound.Visible = False
        dgvSkillList.Visible = True
        txt_search.Text = String.Empty
        SearchData()
        For i As Integer = 0 To chkService.Items.Count - 1
            chkService.Items(i).Selected = False
        Next
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Central Setup >> Skill", "AisWebConfig.frmMSTSkill.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : Central Setup >> Skill", "AisWebConfig.frmMSTSkill.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Sub SearchData()
        dgvSkillList.CurrentPageIndex = 0

        Dim eng As New MasterENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " skill like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.GetSkillList(wh)
        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvSkillList.Visible = False
        Else
            lblNotFound.Visible = False
            dgvSkillList.Visible = True
            dgvSkillList.DataSource = dt
            dgvSkillList.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub dgvSkillList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvSkillList.PageIndexChanged
        dgvSkillList.CurrentPageIndex = e.NewPageIndex
        GetSkillList()
    End Sub

    Protected Sub dgvSkillList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvSkillList.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim p As New CenParaDB.TABLE.TbSkillCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetSkillPara(lbl_id.Text)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txt_skill.Text = p.SKILL
                chk_appointment.Checked = IIf(p.APPOINTMENT = "1", True, False)
                chk_active.Checked = IIf(p.ACTIVE_STATUS = "1", True, False)
            End If

            Dim dtItem As New DataTable
            dtItem = eng.GetSkillItem("skill_id='" & p.ID & "'")
            If dtItem.Rows.Count > 0 Then
                For cnt As Integer = 0 To chkService.Items.Count - 1
                    Dim temp() As DataRow
                    temp = dtItem.Select("item_id = '" & chkService.Items(cnt).Value & "'")
                    If temp.Length > 0 Then
                        chkService.Items(cnt).Selected = True
                    Else
                        chkService.Items(cnt).Selected = False
                    End If
                Next
            End If
            'GetSkillList()

        End If
    End Sub
End Class
