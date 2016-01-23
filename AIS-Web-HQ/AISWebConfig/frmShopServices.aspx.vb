Imports Engine.Configuration
Imports System.Data
Imports System.Drawing

Partial Class frmShopServices
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ClearData()
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            SetDDLSearchShop(uPara)
            SetMasterItemList()
            SetServiceList(uPara, True)
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_appointment_queue_no_min.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_appointment_queue_no_max.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_queue.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            txt_color.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.Page_Load", Config.GetLoginHistoryID)
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
        dt = eng.GetServiceAllList("1=1 and  active_status=1")

        If dt.Rows.Count = 0 Then
            dt.Columns.Add("id")
            dt.Columns.Add("item_name")
        End If

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("item_name") = "-----Select-----"
        dt.Rows.InsertAt(dr, 0)
        ddlMasterItem.DataTextField = "item_name"
        ddlMasterItem.DataValueField = "id"
        ddlMasterItem.DataSource = dt
        ddlMasterItem.DataBind()

        dt.Dispose()
        eng = Nothing

    End Sub

    Private Sub SetServiceList(ByVal uPara As CenParaDB.Common.UserProfilePara, ByVal IsClickSearch As Boolean)
        Dim eng As New ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " item_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or item_name like '%" & txt_search.Text.Trim & "%'"
            wh += " or item_name_th like '%" & txt_search.Text.Trim & "%'"
        End If

        Dim searchShop As Long = 0
        If ddlSearchShop.SelectedValue <> "" Then
            searchShop = ddlSearchShop.SelectedValue
        End If

        dt = eng.SearchService(searchShop, wh, uPara.USERNAME)
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
            Dim p As New CenParaDB.TABLE.TbItemCenParaDB
            p.ID = ddlMasterItem.SelectedValue
            p.ITEM_CODE = txt_item_code.Text.Trim
            p.ITEM_NAME = txt_item_name_english.Text.Trim
            p.ITEM_NAME_TH = txt_item_name_thai.Text.Trim
            p.ITEM_TIME = txt_standard_handing_time.Text.Trim
            p.ITEM_WAIT = txt_standard_waiting_time.Text.Trim
            p.ITEM_ORDER = txt_item_order.Text.Trim
            p.TXT_QUEUE = txt_queue.Text.Trim
            p.TB_ITEM_GROUP_ID = hidItemGroupID.Value

            If txt_colorcode.Text.Trim <> "" Then
                Dim hexcolor As Color = System.Drawing.ColorTranslator.FromHtml("#" & txt_colorcode.Text)
                p.COLOR = hexcolor.ToArgb.ToString
            End If
            If chk_active.Checked = True Then
                p.ACTIVE_STATUS = "1"
            Else
                p.ACTIVE_STATUS = "0"
            End If

            If chk_vasily.Checked = True Then
                p.BRAND_NAME = "Y"
            Else
                p.BRAND_NAME = "N"
            End If

            p.APP_MIN_QUEUE = txt_appointment_queue_no_min.Text.Trim
            p.APP_MAX_QUEUE = txt_appointment_queue_no_max.Text.Trim

            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim eng As New Engine.Configuration.ShopServiceENG
            If eng.SaveShopItem(p, uPara.USERNAME, ctlBranchSelected1.CheckedNodes) = True Then
                Config.ShowAlert("Save Complete.", Me)
                ClearData()
                SetServiceList(uPara, True)
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If
            eng = Nothing
            uPara = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.CmdSave_Click", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            ret = False
            Config.ShowAlert("Please select Shop", Me)
        ElseIf ddlMasterItem.SelectedValue = "0" Then
            ret = False
            Config.ShowAlert("Please select Master Item", Me)
        ElseIf txt_item_code.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Item Code", Me, txt_item_code.ClientID)
        ElseIf txt_item_name_english.Text.Trim = "" Then
            Config.ShowAlert("Please input Item Name English", Me, txt_item_name_english.ClientID)
            ret = False
        ElseIf txt_item_name_thai.Text.Trim = "" Then
            Config.ShowAlert("Please input Item Name Thai", Me, txt_item_name_thai.ClientID)
            ret = False
        ElseIf txt_appointment_queue_no_min.Text.Trim = "" Then
            Config.ShowAlert("Please input Appointment Queue No Min", Me, txt_appointment_queue_no_min.ClientID)
            ret = False
        ElseIf txt_appointment_queue_no_max.Text.Trim = "" Then
            Config.ShowAlert("Please input Appointment Queue No Max", Me, txt_appointment_queue_no_max.ClientID)
            ret = False
        ElseIf txt_appointment_queue_no_min.Text >= txt_appointment_queue_no_max.Text Then
            Config.ShowAlert("Max Appointment Queue No < Min Appointment Queue No ,Please Try Again !!", Me, txt_appointment_queue_no_min.ClientID)
            ret = False
        ElseIf txt_item_order.Text.Trim = "" Then
            Config.ShowAlert("Please input Item Order", Me, txt_item_order.ClientID)
            ret = False
        ElseIf txt_item_order.Text < 0 Then
            Config.ShowAlert("Item Order < 0,Please Try Again !!", Me, txt_item_order.ClientID)
            ret = False
        ElseIf txt_queue.Text.Trim = "" Then
            Config.ShowAlert("Please input Text Queue", Me, txt_queue.ClientID)
            ret = False
        ElseIf txt_standard_handing_time.Text.Trim = "" Then
            Config.ShowAlert("Please input Standard Handling time", Me, txt_standard_handing_time.ClientID)
            ret = False
        ElseIf txt_standard_handing_time.Text < 0 Then
            Config.ShowAlert("Standard Handling time < 0,Please Try Again !!'", Me, txt_standard_handing_time.ClientID)
            ret = False
        ElseIf txt_standard_waiting_time.Text.Trim = "" Then
            Config.ShowAlert("Please input Standard Waiting time", Me, txt_standard_waiting_time.ClientID)
            ret = False
        ElseIf txt_standard_waiting_time.Text < 0 Then
            Config.ShowAlert("Standard Waiting time < 0,Please Try Again !!", Me, txt_standard_waiting_time.ClientID)
            ret = False
        End If

        Return ret
    End Function


    Private Sub ClearData()
        'txt_id.Text = "0"
        'ctlBranchSelected1.
        ddlMasterItem.SelectedValue = "0"
        txt_item_code.Text = ""
        txt_item_name_english.Text = ""
        txt_item_name_thai.Text = ""
        txt_standard_handing_time.Text = ""
        txt_standard_waiting_time.Text = ""
        txt_item_order.Text = ""
        txt_item_order2.Text = ""
        txt_queue.Text = ""
        txt_colorcode.Text = ""
        txt_color.BackColor = Color.White
        chk_active.Checked = True
        chk_vasily.Checked = True
        txt_appointment_queue_no_max.Text = ""
        txt_appointment_queue_no_min.Text = ""
        txt_item_code.Enabled = True
        lblNotFound.Visible = False
        dgvService.Visible = True
        txt_search.Text = String.Empty
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetServiceList(uPara, True)
        uPara = Nothing

        txt_item_order2.Visible = True
        txt_item_order.Visible = False
        ctlBranchSelected1.EnableControl = True
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        dgvService.CurrentPageIndex = 0
        SetServiceList(uPara, True)
        uPara = Nothing
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopServices.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub dgvService_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvService.EditCommand
        If e.CommandName = "Edit" Then
            txt_item_order2.Visible = False
            txt_item_order.Visible = True
            ctlBranchSelected1.EnableControl = False


            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)

            Dim p As New ShParaDB.TABLE.TbItemShParaDB
            Dim eng As New Engine.Configuration.ShopServiceENG
            p = eng.GetShopItemPara(lbl_shop_id.Text, lbl_id.Text)
            If p.ID <> 0 Then
                ctlBranchSelected1.SelectShop = lbl_shop_id.Text
                Try
                    ddlMasterItem.SelectedValue = p.MASTER_ITEMID
                Catch ex As Exception

                End Try
                txt_item_code.Text = p.ITEM_CODE
                txt_item_name_english.Text = p.ITEM_NAME
                txt_item_name_thai.Text = p.ITEM_NAME_TH
                txt_standard_handing_time.Text = p.ITEM_TIME
                txt_standard_waiting_time.Text = p.ITEM_WAIT
                txt_item_order.Text = p.ITEM_ORDER
                txt_queue.Text = p.TXT_QUEUE

                If p.COLOR.Value <> 0 Then
                    'Dim strcol As String = System.Drawing.ColorTranslator.ToHtml(System.Drawing.ColorTranslator.FromOle(p.COLOR))
                    'If strcol.Substring(0, 1) <> "#" Then
                    '    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    'Else
                    '    txt_colorcode.Text = strcol.Replace("#", "").Trim
                    'End If
                    'txt_color.BackColor = Color.FromArgb(p.COLOR)
                    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    Dim c As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)))
                    txt_color.BackColor = c
                Else
                    txt_colorcode.Text = ""
                    txt_color.BackColor = Color.White
                End If
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                chk_vasily.Checked = IIf(p.BRAND_NAME = "Y", True, False)
                txt_appointment_queue_no_max.Text = p.APP_MAX_QUEUE
                txt_appointment_queue_no_min.Text = p.APP_MIN_QUEUE
            End If
            p = Nothing
            eng = Nothing
        End If
    End Sub

    Protected Sub dgvService_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvService.PageIndexChanged
        dgvService.CurrentPageIndex = e.NewPageIndex
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetServiceList(uPara, False)
        uPara = Nothing
    End Sub

    Protected Sub ddlMasterItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMasterItem.SelectedIndexChanged
        If ddlMasterItem.SelectedValue <> "0" Then
           
            Dim p As New CenParaDB.TABLE.TbItemCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetServicePara(ddlMasterItem.SelectedValue)
            eng = Nothing

            If p.ID <> 0 Then
                txt_item_code.Text = p.ITEM_CODE
                txt_item_name_english.Text = p.ITEM_NAME
                txt_item_name_thai.Text = p.ITEM_NAME_TH
                txt_appointment_queue_no_min.Text = p.APP_MIN_QUEUE
                txt_appointment_queue_no_max.Text = p.APP_MAX_QUEUE
                txt_queue.Text = p.TXT_QUEUE
                txt_item_order.Text = p.ITEM_ORDER
                txt_item_order2.Text = p.ITEM_ORDER
                txt_standard_handing_time.Text = p.ITEM_TIME
                txt_standard_waiting_time.Text = p.ITEM_WAIT
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                chk_vasily.Checked = IIf(p.BRAND_NAME = "Y", True, False)
                hidItemGroupID.Value = p.TB_ITEM_GROUP_ID

                If p.COLOR.Value <> 0 Then
                    'Dim strcol As String = System.Drawing.ColorTranslator.ToHtml(System.Drawing.ColorTranslator.FromOle(p.COLOR))
                    'If strcol.Substring(0, 1) <> "#" Then
                    '    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    'Else
                    '    txt_colorcode.Text = strcol.Replace("#", "").Trim
                    'End If
                    'txt_color.BackColor = Color.FromArgb(p.COLOR)
                    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    Dim c As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)))
                    txt_color.BackColor = c
                Else
                    txt_colorcode.Text = ""
                    txt_color.BackColor = Color.White
                End If
            End If
            p = Nothing
            eng = Nothing
        Else
            ClearData()
        End If
    End Sub
End Class
