Imports System
Imports System.Data
Imports CenLinqDB.TABLE

Partial Class frmShopLogoutReason
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

                SetMasterLogoutReason()
                SetDDLSearchShop()

                SearchData()
            End If
            uPara = Nothing

            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            Config.SaveTransLog("คลิกเมนู : Shop Setup >> Logout Reason", "AisWebConfig.frmShopLogoutReason.aspx.Page_Load", Config.GetLoginHistoryID)

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

    Private Sub SetMasterLogoutReason()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetLogoutReasonList("active_status='1'")
        dt.DefaultView.Sort = "name"
        dt = dt.DefaultView.ToTable

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("name") = "-----Select-----"
        dt.Rows.InsertAt(dr, 0)

        If dt.Rows.Count > 0 Then
            ddlMasterLogoutReason.DataTextField = "name"
            ddlMasterLogoutReason.DataValueField = "id"
            ddlMasterLogoutReason.DataSource = dt
            ddlMasterLogoutReason.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub


    Private Sub SearchData()

        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        Dim dt As New DataTable
        dt = eng.SearchReason(ddlSearchShop.SelectedValue, txt_search.Text.Trim, uPara.USERNAME, "LogoutReason")
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
    Private Sub ClearTextBox_Logout_reason()
        txt_logout_reason.Text = ""
        txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        chk_logout_reason_active.Checked = False
        chk_logout_reason_productive.Checked = False
        ddlMasterLogoutReason.SelectedValue = "0"
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
        If ddlMasterLogoutReason.SelectedValue = "0" Then
            Config.ShowAlert("Please select Master Logout Reason", Me)
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

        ClearTextBox_Logout_reason()
        SearchData()
        Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Logout Reason", "AisWebConfig.frmShopLogoutReason.aspx.cmd_save_Click", Config.GetLoginHistoryID)
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

            Dim lnq As New TbCfgLogoutReasonCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(" EVENT_STATUS=1 AND lower(NAME)='" & Trim(txt_logout_reason.Text).ToLower & "'", "", trans.Trans)
            If dt.Rows.Count = 0 Then
                Return True
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim cfg_logoutreason_id As String = dt.Rows(i).Item("ID").ToString
                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                eng.DeleteCfgLogoutReason(cfg_logoutreason_id, trans)
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
            Dim p As New CenParaDB.TABLE.TbCfgLogoutReasonCenParaDB
            p.SHOP_ID = c.Value
            p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
            p.NAME = txt_logout_reason.Text.Trim
            p.MASTER_logoutREASON_ID = ddlMasterLogoutReason.SelectedValue

            If chk_logout_reason_productive.Checked = True Then
                p.PRODUCTIVE = 1
            Else
                p.PRODUCTIVE = 0
            End If
            If chk_logout_reason_active.Checked = True Then
                p.ACTIVE_STATUS = 1
            Else
                p.ACTIVE_STATUS = 0
            End If

            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            If eng.SaveCfgSchedlogoutReason(p, uPara.USERNAME) = False Then
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
            shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "frmShopLogoutReason_SaveDataEventNow")
            If shTrans.Trans IsNot Nothing Then
                Dim p As New ShParaDB.TABLE.TbLogoutReasonShParaDB
                p.MASTER_LOGOUTREASONID = ddlMasterLogoutReason.SelectedValue
                p.NAME = txt_logout_reason.Text.Trim

                If chk_logout_reason_productive.Checked = True Then
                    p.PRODUCTIVE = 1
                Else
                    p.PRODUCTIVE = 0
                End If
                If chk_logout_reason_active.Checked = True Then
                    p.ACTIVE_STATUS = 1
                Else
                    p.ACTIVE_STATUS = 0
                End If

                Dim eng As New Engine.Configuration.ShopSettingENG
                If eng.SaveShopLogoutReason(p, uPara.USER_PARA.ID, shTrans) = True Then
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

    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Logout Reason", "AisWebConfig.frmShopLogoutReason.aspx.cmd_clear_Click", Config.GetLoginHistoryID)
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvdetail.CurrentPageIndex = 0
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Logout Reason", "AisWebConfig.frmShopLogoutReason.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Private Sub FillInForm(ByVal LogoutReasonID As Long)
        If LogoutReasonID > 0 Then
            Dim eng As New Engine.Configuration.MasterENG
            Dim p As New CenParaDB.TABLE.TbLogoutReasonCenParaDB
            p = eng.GetLogoutReasonPara(LogoutReasonID)
            If p.ID <> 0 Then
                txt_logout_reason.Text = p.NAME
                chk_logout_reason_productive.Checked = (p.PRODUCTIVE.Value = 1)
                chk_logout_reason_active.Checked = (p.ACTIVE_STATUS.Value = 1)
            Else
                txt_logout_reason.Text = ""
                chk_logout_reason_productive.Checked = False
                chk_logout_reason_active.Checked = False
            End If
            p = Nothing
            eng = Nothing
        Else
            txt_logout_reason.Text = ""
            chk_logout_reason_productive.Checked = False
            chk_logout_reason_active.Checked = False
        End If
    End Sub

    Protected Sub ddlMasterLogoutReason_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMasterLogoutReason.SelectedIndexChanged
        FillInForm(ddlMasterLogoutReason.SelectedValue)
    End Sub
End Class
