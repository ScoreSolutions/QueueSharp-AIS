Imports Engine.Configuration
Imports System.Data
Imports System.Drawing

Partial Class frmShopSegment
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            SetCustomerTypeList()
            SetSegmentList(uPara, True)
            SetDDLSearchShop(uPara)
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_Segment.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")

            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Shop Setup >> Segment", "AisWebConfig.frmShopSegment.aspx.Page_Load", Config.GetLoginHistoryID)
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

    Private Sub SetCustomerTypeList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetCustomerTypeList("active_status = 1 and app = 0 and def = 0")

        If dt.Rows.Count = 0 Then
            dt.Columns.Add("id")
            dt.Columns.Add("customertype_name")
        End If

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("customertype_name") = "-----Select-----"
        dt.Rows.InsertAt(dr, 0)
        ddlCustomerType.DataTextField = "customertype_name"
        ddlCustomerType.DataValueField = "id"
        ddlCustomerType.DataSource = dt
        ddlCustomerType.DataBind()

        dt.Dispose()
        eng = Nothing

    End Sub

    Private Sub SetSegmentList(ByVal uPara As CenParaDB.Common.UserProfilePara, ByVal IsClickSearch As Boolean)
        Dim eng As New ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " Segment like '%" & txt_search.Text.Trim & "%'"
        End If

        Dim searchShop As Long = 0
        If ddlSearchShop.SelectedValue <> "" Then
            searchShop = ddlSearchShop.SelectedValue
        End If

        dt = eng.SearchSegment(searchShop, wh, uPara.USERNAME)
        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvService.Visible = False
        Else
            lblNotFound.Visible = False
            dgvService.Visible = True
            dgvService.DataSource = dt
            dgvService.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
           
            Dim p As New ShParaDb.TABLE.TbSegmentShParaDB
            p.ID = hidSegmentID.Value
            p.CUSTOMERTYPE_ID = ddlCustomerType.SelectedValue
            p.SEGMENT = txt_Segment.Text.Trim
            'p.CREATE_BY = uPara.LOGIN_HISTORY_ID
            'p.CREATE_DATE = Date.Now
            'p.UPDATE_BY = uPara.LOGIN_HISTORY_ID
            'p.UPDATE_DATE = Date.Now

            If chk_active.Checked = True Then
                p.ACTIVE_STATUS = "1"
            Else
                p.ACTIVE_STATUS = "0"
            End If

            Dim eng As New Engine.Configuration.ShopServiceENG
            If eng.SaveShopSegment(p, uPara.LOGIN_HISTORY_ID, ctlBranchSelected1.CheckedNodes) = True Then
                Config.ShowAlert("Save Complete.", Me)
                ClearData()
                SetSegmentList(uPara, True)
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If
            eng = Nothing
            uPara = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopSegment.aspx.CmdSave_Click", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            ret = False
            Config.ShowAlert("Please select Shop", Me)
        ElseIf ddlCustomerType.SelectedValue = "0" Then
            ret = False
            Config.ShowAlert("Please select Customer Type", Me)
        ElseIf txt_Segment.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Segment", Me, txt_Segment.ClientID)
        Else
            For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
                Dim eng As New ShopEventScheduleSettingENG
                If eng.GetShopSegmentDup(c.Value, txt_Segment.Text.Trim, ddlCustomerType.SelectedValue, hidSegmentID.Value) = True Then
                    ret = False
                    Config.ShowAlert(eng.ErrMessage, Me, txt_Segment.ClientID)
                    eng = Nothing
                    Exit For
                End If
                eng = Nothing
            Next
        End If

        Return ret
    End Function


    Private Sub ClearData()
        'txt_id.Text = "0"
        'ctlBranchSelected1.
        ddlCustomerType.SelectedValue = "0"
        txt_Segment.Text = ""
        lblNotFound.Visible = False
        dgvService.Visible = True
        txt_search.Text = String.Empty
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetSegmentList(uPara, True)
        uPara = Nothing

        txt_Segment.Enabled = True
        hidSegmentID.Value = "0"
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvService.CurrentPageIndex = 0
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetSegmentList(uPara, True)
        uPara = Nothing
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub dgvService_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvService.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)

            Dim p As New ShParaDB.TABLE.TbSegmentShParaDB

            Dim eng As New Engine.Configuration.ShopServiceENG
            p = eng.GetSegmentItemPara(lbl_shop_id.Text, lbl_id.Text)
            If p.ID <> 0 Then
                ctlBranchSelected1.SelectShop = lbl_shop_id.Text
                ddlCustomerType.SelectedValue = p.CUSTOMERTYPE_ID
                txt_Segment.Text = p.SEGMENT
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                hidSegmentID.Value = lbl_id.Text
            End If
            p = Nothing
            eng = Nothing
        End If
    End Sub

    Protected Sub dgvService_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvService.PageIndexChanged
        dgvService.CurrentPageIndex = e.NewPageIndex
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetSegmentList(uPara, False)
        uPara = Nothing
    End Sub

End Class
