Imports System
Imports System.Data
Imports CenLinqDB.TABLE

Partial Class frmShopHoldReason
    Inherits System.Web.UI.Page
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim version As String = System.Configuration.ConfigurationManager.AppSettings("version")
            Page.Title = version
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Me.Response.Redirect("frmlogin.aspx")
            Else
                txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))

                SetMasterHoldReason()
                SetDDLSearchShop()

                SearchData()
            End If
            uPara = Nothing

            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            Config.SaveTransLog("คลิกเมนู : Shop Setup >> Hold Reason", "AisWebConfig.frmShopHoldReason.aspx.Page_Load", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Sub SetDDLSearchShop()
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
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
        uPara = Nothing
    End Sub

    Private Sub SetMasterHoldReason()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetHoldReasonList("active_status='1'")
        dt.DefaultView.Sort = "name"
        dt = dt.DefaultView.ToTable

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("name") = "-----Select-----"
        dt.Rows.InsertAt(dr, 0)

        If dt.Rows.Count > 0 Then
            ddlMasterHoldReason.DataTextField = "name"
            ddlMasterHoldReason.DataValueField = "id"
            ddlMasterHoldReason.DataSource = dt
            ddlMasterHoldReason.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub SearchData()
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        Dim dt As New DataTable
        dt = eng.SearchReason(ddlSearchShop.SelectedValue, txt_search.Text.Trim, uPara.USERNAME, "HoldReason")
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

    Protected Sub dgvdetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvdetail.PageIndexChanged
        dgvdetail.CurrentPageIndex = e.NewPageIndex
        SearchData()
    End Sub
    Private Sub ClearTextBox_hold_reason()
        txt_hold_reason.Text = ""
        txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        chk_hold_reason_active.Checked = False
        chk_hold_reason_productive.Checked = False
        ddlMasterHoldReason.SelectedValue = "0"
        lblNotFound.Visible = False
        dgvdetail.Visible = True
        txt_search.Text = String.Empty
        SearchData()
        'TreeViewBranch.Enabled = True
        'For Each Node As TreeNode In TreeViewBranch.Nodes
        '    For Each Parent As TreeNode In Node.ChildNodes
        '        For Each Child As TreeNode In Parent.ChildNodes
        '            Child.Checked = False
        '        Next
        '        Parent.Checked = False
        '    Next
        '    Node.Checked = False
        'Next
    End Sub

    Protected Sub cmd_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        Dim flag As Boolean = False
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Please Select Shop!!');", True)
            Exit Sub
        End If
        If ddlMasterHoldReason.SelectedValue = "0" Then
            Config.ShowAlert("Please select Master Hold Reason", Me)
            Exit Sub
        End If
        If opt_now.Checked = False And opt_schedule.Checked = False Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Please Select Event !!');", True)
            Exit Sub
        End If
        If opt_schedule.Checked = True Then
            If txt_date.Text.Trim = "" Then
                Config.ShowAlert("Please select date", Me)
                Exit Sub
            ElseIf Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) < DateTime.Now.Date Then
                Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                Exit Sub
            End If
        End If
        
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        If opt_now.Checked = True Then
            If SaveDataEventNow(uPara) = True Then
                Config.ShowAlert("Save Complete", Me)
            End If
        ElseIf opt_schedule.Checked = True Then
            DeleteCfg()
            If SaveDataEventSchedule(uPara) = True Then
                Config.ShowAlert("Save Complete", Me)
            End If
        End If
        uPara = Nothing
        ClearTextBox_hold_reason()
        SearchData()
        Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Hold Reason", "AisWebConfig.frmShopHoldReason.aspx.cmd_save_Click", Config.GetLoginHistoryID)

        'txt_id.Text = ""
        'TreeViewBranch.Enabled = True
        'For Each Node As TreeNode In TreeViewBranch.Nodes
        '    For Each Parent As TreeNode In Node.ChildNodes
        '        For Each Child As TreeNode In Parent.ChildNodes
        '            Child.Checked = False
        '        Next
        '    Next
        'Next
        'Binddata()
    End Sub

    Private Function DeleteCfg() As Boolean
        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Try

            Dim lnq As New TbCfgHoldReasonCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(" EVENT_STATUS=1 AND lower(NAME)='" & Trim(txt_hold_reason.Text).ToLower & "'", "", trans.Trans)
            If dt.Rows.Count = 0 Then
                Return True
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim cfg_holdreason_id As String = dt.Rows(i).Item("ID").ToString
                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                eng.DeleteCfgHoldReason(cfg_holdreason_id, trans)
            Next

            trans.CommitTransaction()
        Catch ex As Exception
            trans.RollbackTransaction()
            ret = False
        End Try
        Return ret
    End Function

    Private Function SaveDataEventSchedule(ByVal uPara As CenParaDB.Common.UserProfilePara) As Boolean
        Dim ret As Boolean = True
        Dim _err As String = ""
        Dim chk As TreeNodeCollection = ctlBranchSelected1.CheckedNodes
        For Each c As TreeNode In chk
            Dim p As New CenParaDB.TABLE.TbCfgHoldReasonCenParaDB
            p.SHOP_ID = c.Value
            p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
            p.NAME = txt_hold_reason.Text.Trim
            p.MASTER_HOLDREASON_ID = ddlMasterHoldReason.SelectedValue

            If chk_hold_reason_productive.Checked = True Then
                p.PRODUCTIVE = 1
            Else
                p.PRODUCTIVE = 0
            End If
            If chk_hold_reason_active.Checked = True Then
                p.ACTIVE_STATUS = 1
            Else
                p.ACTIVE_STATUS = 0
            End If

            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            If eng.SaveCfgSchedHoldReason(p, uPara.USERNAME) = False Then
                _err = eng.ErrMessage
                Exit For
            End If
        Next
        If _err <> "" Then
            Config.ShowAlert(_err, Me)
            ret = False
        End If

        Return ret

    End Function

    Private Function SaveDataEventNow(ByVal uPara As CenParaDB.Common.UserProfilePara) As Boolean
        Dim ret As Boolean = True
        Dim _err As String = ""
        Dim chk As TreeNodeCollection = ctlBranchSelected1.CheckedNodes
        For Each c As TreeNode In chk
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "frmShopHoldReason_DataDataEventNow")
            If shTrans.Trans IsNot Nothing Then
                Dim p As New ShParaDB.TABLE.TbHoldReasonShParaDB
                p.MASTER_HOLDREASONID = ddlMasterHoldReason.SelectedValue
                p.NAME = txt_hold_reason.Text.Trim

                If chk_hold_reason_productive.Checked = True Then
                    p.PRODUCTIVE = 1
                Else
                    p.PRODUCTIVE = 0
                End If
                If chk_hold_reason_active.Checked = True Then
                    p.ACTIVE_STATUS = 1
                Else
                    p.ACTIVE_STATUS = 0
                End If

                Dim eng As New Engine.Configuration.ShopSettingENG
                If eng.SaveShopHoldReason(p, uPara.USERNAME, shTrans) = True Then
                    shTrans.CommitTransaction()
                    eng = Nothing
                Else
                    shTrans.RollbackTransaction()
                    _err = eng.ErrorMessage
                    eng = Nothing
                    Exit For
                End If
            End If
        Next

        If _err <> "" Then
            Config.ShowAlert(_err, Me)
            ret = False
        End If

        Return ret
    End Function

    'Protected Sub dgvdetail_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvdetail.EditCommand
    '    If e.CommandName = "Edit" Then
    '        ClearTextBox_hold_reason()
    '        ClearTextBox_logout_reason()
    '        Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
    '        Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
    '        Dim lbl_reason As Label = DirectCast(e.Item.FindControl("lbl_reason"), Label)
    '        Dim lbl_Type As Label = DirectCast(e.Item.FindControl("lbl_Type"), Label)
    '        Dim lbl_Productivity As Label = DirectCast(e.Item.FindControl("lbl_Productivity"), Label)
    '        Dim lbl_active_status As Label = DirectCast(e.Item.FindControl("lbl_active_status"), Label)

    '        'txt_id.Text = lbl_id.Text

    '        txt_hold_reason.Text = lbl_reason.Text
    '        If lbl_Productivity.Text = "1" Then
    '            chk_hold_reason_productive.Checked = True
    '        Else
    '            chk_hold_reason_productive.Checked = False
    '        End If

    '        If lbl_active_status.Text = "1" Then
    '            chk_hold_reason_active.Checked = True
    '        Else
    '            chk_hold_reason_active.Checked = False
    '        End If
    '        Shop_Config.shop_id = lbl_shop_id.Text
    '        mode = "Edit"

    '        'TreeViewBranch.Enabled = False
    '        'For Each Node As TreeNode In TreeViewBranch.Nodes
    '        '    For Each Parent As TreeNode In Node.ChildNodes
    '        '        For Each Child As TreeNode In Parent.ChildNodes
    '        '            Dim shop_code As String = Child.Value
    '        '            Dim strshop As Array = shop_code.Split(",")
    '        '            If lbl_shop_id.Text = strshop(0) Then
    '        '                Child.Checked = True
    '        '            End If
    '        '        Next
    '        '    Next
    '        'Next
    '    End If

    'End Sub
    'Protected Sub ChkActive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim gr As DataGridItem
    '    Dim chk As CheckBox = DirectCast(sender, CheckBox)


    '    Dim gvr As DataGridItem = DirectCast(chk.NamingContainer, DataGridItem)
    '    Dim itemindex As Integer = gvr.ItemIndex


    '    For Each gr In dgvdetail.Items
    '        If gr.ItemIndex = itemindex Then
    '            Dim lbl_shop_id As Label = gr.FindControl("lbl_shop_id")
    '            Dim lbl_id As Label = gr.FindControl("lbl_id")
    '            Dim lbl_Type As Label = gr.FindControl("lbl_Type")

    '            Dim lbl_active_status As Label = gr.FindControl("lbl_active_status")
    '            Shop_Config.shop_id = lbl_shop_id.Text
    '            Dim dt As New DataTable
    '            dt = Shop_Config.RetriveShop()
    '            Reason.id = lbl_id.Text
    '            Reason.update_by = Session("username").ToString
    '            Reason.update_date = Sqldb.GetDateTime_adddt
    '            If lbl_active_status.Text = "1" Then
    '                Reason.active_status = "0"
    '            Else
    '                Reason.active_status = "1"
    '            End If

    '            If lbl_Type.Text = "Hold reason" Then
    '                If Reason.UpdateActive_Hold_Reason(dt.Rows(0).Item("shop_code").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_name").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_userid").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_pwd").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_server").ToString.Trim) = False Then
    '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Update Active Status Fail. ,Please Try Again');", True)
    '                    Exit Sub
    '                End If
    '            ElseIf lbl_Type.Text = "Logout reason" Then

    '                If Reason.UpdateActive_Logout_Reason(dt.Rows(0).Item("shop_code").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_name").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_userid").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_pwd").ToString.Trim, _
    '                                                          dt.Rows(0).Item("shop_db_server").ToString.Trim) = False Then
    '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Update Active Status Fail. ,Please Try Again');", True)
    '                    Exit Sub
    '                End If
    '            End If

    '        End If
    '    Next
    '    SearchData()
    'End Sub

    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Hold Reason", "AisWebConfig.frmShopHoldReason.aspx.cmd_clear_Click", Config.GetLoginHistoryID)
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvdetail.CurrentPageIndex = 0
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Hold Reason", "AisWebConfig.frmShopHoldReason.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Private Sub FillInForm(ByVal HoldReasonID As Long)
        If HoldReasonID > 0 Then
            Dim eng As New Engine.Configuration.MasterENG
            Dim p As New CenParaDB.TABLE.TbHoldReasonCenParaDB
            p = eng.GetHoldReasonPara(HoldReasonID)
            If p.ID <> 0 Then
                txt_hold_reason.Text = p.NAME
                chk_hold_reason_productive.Checked = (p.PRODUCTIVE.Value = 1)
                chk_hold_reason_active.Checked = (p.ACTIVE_STATUS.Value = 1)
            Else
                txt_hold_reason.Text = ""
                chk_hold_reason_productive.Checked = False
                chk_hold_reason_active.Checked = False
            End If
            p = Nothing
            eng = Nothing
        Else
            txt_hold_reason.Text = ""
            chk_hold_reason_productive.Checked = False
            chk_hold_reason_active.Checked = False
        End If
    End Sub

    Protected Sub ddlMasterHoldReason_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMasterHoldReason.SelectedIndexChanged
        FillInForm(ddlMasterHoldReason.SelectedValue)
    End Sub
End Class
