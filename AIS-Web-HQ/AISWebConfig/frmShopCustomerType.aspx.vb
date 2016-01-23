Imports Engine.Configuration
Imports System.Data
Imports System.Drawing
Partial Class frmShopCustomerType
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim sh As New CenParaDB.TABLE.TbShopCenParaDB
            'Dim shEng As New Engine.Configuration.MasterENG
            'sh = shEng.GetShopPara(Request("ShopID"))
            'If sh.ID <> 0 Then
            '    lblScreenName.Text = "Shop Setup >> Customer Type >> " & sh.SHOP_NAME_EN
            'End If
            'sh = Nothing
            'shEng = Nothing

            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                'Code at Form_Load
                SetDDLSearchShop(uPara)
                SetMasterCustomerList()
                SetCustomerTypeList(uPara, True)

                'txt_queue_no_max.Attributes.Add("onKeyPress", "return ChkMinusInt(event,'" & txt_queue_no_max.ClientID & "')")
                txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
                txt_code.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
                txt_name.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
                txt_queue.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
                txt_color.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSave.ClientID + "') ")
            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopCustomerType.aspx.Page_Load", Config.GetLoginHistoryID)
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

    Private Sub SetMasterCustomerList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetCustomerTypeList("active_status = 1")

        If dt.Rows.Count = 0 Then
            dt.Columns.Add("id")
            dt.Columns.Add("customertype_name")
        End If

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("customertype_name") = "-----Select-----"
        dt.Rows.InsertAt(dr, 0)
        ddlMasterCustomerType.DataTextField = "customertype_name"
        ddlMasterCustomerType.DataValueField = "id"
        ddlMasterCustomerType.DataSource = dt
        ddlMasterCustomerType.DataBind()

        dt.Dispose()
        eng = Nothing

    End Sub

    Private Sub SetCustomerTypeList(ByVal uPara As CenParaDB.Common.UserProfilePara, ByVal IsClickSearch As Boolean)
        Dim eng As New ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " customertype_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or customertype_name like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.SearchCustomerType(ddlSearchShop.SelectedValue, wh, uPara.USERNAME)
        If dt.Rows.Count = 0 Then
            lblNotFound.Visible = True
            dgvCustomerType.Visible = False
        Else
            lblNotFound.Visible = False
            dgvCustomerType.Visible = True
            dgvCustomerType.DataSource = dt
            dgvCustomerType.DataBind()
        End If

        dt.Dispose()
        eng = Nothing
    End Sub

    Private Function CheckCustomerType_Valid(ByVal strCase As String) As String
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser

        Dim strReturnValue As String = ""
        Dim strReturnValueTemp As String = ""
        Dim eng As New ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = ""
        Select Case strCase.ToUpper
            Case "DEF"
                wh = "DEF = 1 and Id <>'" & hidShopCustomerTypeID.Value & "'"
                strReturnValueTemp = "The Default Customer Type already exists for other customer Type "
            Case "APP"
                wh = "APP = 1 and Id <>'" & hidShopCustomerTypeID.Value & "'"
                strReturnValueTemp = "The Customer Type Appointment already exists for other customer Type "

        End Select
        For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
            dt = eng.SearchCustomerType(c.Value, wh, uPara.USERNAME)
            If dt.Rows.Count > 0 Then
                strReturnValue = strReturnValueTemp
                Exit For
            End If
        Next

        dt.Dispose()
        eng = Nothing
        uPara = Nothing


        Return strReturnValue
    End Function

    Protected Sub ddlMasterCustomerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMasterCustomerType.SelectedIndexChanged
        If ddlMasterCustomerType.SelectedValue <> "0" Then
            Dim p As New CenParaDB.TABLE.TbCustomertypeCenParaDB
            Dim eng As New Engine.Configuration.MasterENG
            p = eng.GetCustomerTypePara(ddlMasterCustomerType.SelectedValue)
            eng = Nothing

            If p.ID <> 0 Then
                txt_code.Text = p.CUSTOMERTYPE_CODE
                txt_name.Text = p.CUSTOMERTYPE_NAME
                
                txt_queue.Text = p.TXT_QUEUE
                chk_IsAppointment.Checked = IIf(p.APP.Value = "1", True, False)
                chk_IsDefaultCustomerType.Checked = IIf(p.DEF.Value = "1", True, False)
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                txt_queue_no_min.Text = ""
                txt_queue_no_max.Text = ""
                txt_colorcode.Text = ""
                txt_color.BackColor = Color.White

                'If hidShopCustomerTypeID.Value <> "0" Then
                '    txt_queue_no_min.Text = ""
                '    txt_queue_no_max.Text = ""
                '    txt_colorcode.Text = ""
                '    txt_color.BackColor = Color.White
                'Else
                '    txt_queue_no_min.Text = p.MAX_QUEUE
                '    txt_queue_no_max.Text = p.MAX_QUEUE
                '    If p.COLOR.Value <> 0 Then
                '        Dim strcol As String = System.Drawing.ColorTranslator.ToHtml(System.Drawing.ColorTranslator.FromOle(p.COLOR))
                '        If strcol.Substring(0, 1) <> "#" Then
                '            txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                '        Else
                '            txt_colorcode.Text = strcol.Replace("#", "").Trim
                '        End If
                '        txt_color.BackColor = Color.FromArgb(p.COLOR)
                '    Else
                '        txt_colorcode.Text = ""
                '        txt_color.BackColor = Color.White
                '    End If
                'End If
                
                If ddlMasterCustomerType.SelectedItem.ToString.ToLower = "appointment" Then
                    txt_queue_no_min.Enabled = False
                    txt_queue_no_max.Enabled = False
                Else
                    txt_queue_no_min.Enabled = True
                    txt_queue_no_max.Enabled = True
                End If
                
            End If
            p = Nothing
            eng = Nothing
        Else
            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        ddlMasterCustomerType.Enabled = True
        ddlMasterCustomerType.SelectedIndex = 0
        txt_queue_no_min.Text = ""
        txt_queue_no_max.Text = ""
        txt_code.Text = ""
        txt_name.Text = ""
        txt_colorcode.Text = ""
        txt_color.BackColor = Color.White
        chk_IsAppointment.Checked = False
        chk_IsDefaultCustomerType.Checked = False
        chk_active.Checked = True
        txt_queue.Text = ""
        lblNotFound.Visible = False
        dgvCustomerType.Visible = True

        txt_search.Text = String.Empty
        dgvCustomerType.CurrentPageIndex = 0
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetCustomerTypeList(uPara, True)
        uPara = Nothing

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New CenParaDB.TABLE.TbCustomertypeCenParaDB
            p.ID = ddlMasterCustomerType.SelectedValue
            p.CUSTOMERTYPE_CODE = txt_code.Text.Trim
            p.CUSTOMERTYPE_NAME = txt_name.Text.Trim

            'p.DEF = IIf(chk_IsDefaultCustomerType.Checked = True, "1", "0")
            'p.APP = IIf(chk_IsAppointment.Checked = True, "1", "0")
            p.TXT_QUEUE = txt_queue.Text.Trim
            If txt_colorcode.Text.Trim <> "" Then
                Dim hexcolor As Color = System.Drawing.ColorTranslator.FromHtml("#" & txt_colorcode.Text)
                p.COLOR = hexcolor.ToArgb.ToString
            End If

            If chk_IsDefaultCustomerType.Checked = True Then
                p.DEF = "1"
            Else
                p.DEF = "0"
            End If

            If chk_IsAppointment.Checked = True Then
                p.APP = "1"
            Else
                p.APP = "0"
            End If

            If chk_active.Checked = True Then
                p.ACTIVE_STATUS = "1"
            Else
                p.ACTIVE_STATUS = "0"
            End If

            p.MIN_QUEUE = txt_queue_no_min.Text.Trim
            p.MAX_QUEUE = txt_queue_no_max.Text.Trim

            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim eng As New Engine.Configuration.ShopCustomerTypeENG
            If eng.SaveCustomerType(p, uPara.USERNAME, ctlBranchSelected1.CheckedNodes) = True Then
                Config.ShowAlert("Save Complete.", Me)
                ClearData()
                SetCustomerTypeList(uPara, True)
            Else
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If
            eng = Nothing
            uPara = Nothing
            Config.SaveTransLog("บันทึกข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopCustomerType.aspx.CmdSave_Click", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If ctlBranchSelected1.CheckedNodes.Count = 0 Then
            ret = False
            Config.ShowAlert("Please select Shop", Me)
        ElseIf ddlMasterCustomerType.SelectedValue = "0" Then
            ret = False
            Config.ShowAlert("Please select Master Customer Type", Me)
            'ElseIf txt_queue_no_min.Text.Trim = "" Then
            '    Config.ShowAlert("Please input Queue No Min", Me, txt_queue_no_min.ClientID)
            '    ret = False
            'ElseIf txt_queue_no_max.Text.Trim = "" Then
            '    Config.ShowAlert("Please input Queue No Max", Me, txt_queue_no_max.ClientID)
            '    ret = False
        ElseIf ddlMasterCustomerType.SelectedItem.ToString.ToLower <> "appointment" Then
            If txt_queue_no_min.Text.Trim = "" Then
                Config.ShowAlert("Please input Queue No Min", Me, txt_queue_no_min.ClientID)
                ret = False
            ElseIf txt_queue_no_max.Text.Trim = "" Then
                Config.ShowAlert("Please input Queue No Max", Me, txt_queue_no_max.ClientID)
                ret = False
            ElseIf CInt(txt_queue_no_min.Text) >= CInt(txt_queue_no_max.Text) Then
                Config.ShowAlert("Max Queue No < Min Queue No ,Please Try Again !!", Me, txt_queue_no_min.ClientID)
                ret = False
            End If
            Else

                If chk_IsAppointment.Checked Then
                    Dim strTempAPP As String = CheckCustomerType_Valid("APP")
                    If strTempAPP <> "" Then
                        Config.ShowAlert(strTempAPP, Me)
                        ret = False
                    End If
                End If

                If chk_IsDefaultCustomerType.Checked Then
                    Dim strTempDEF As String = CheckCustomerType_Valid("DEF")
                    If strTempDEF <> "" Then
                        Config.ShowAlert(strTempDEF, Me)
                        ret = False
                    End If
                End If

                For Each c As TreeNode In ctlBranchSelected1.CheckedNodes
                    Dim eng As New Engine.Configuration.ShopCustomerTypeENG
                    If eng.CheckDuplicateCustomerType(c.Value, hidShopCustomerTypeID.Value, txt_name.Text, txt_code.Text) = True Then
                        ret = False
                        Config.ShowAlert(eng.ErrorMessage, Me)
                        Exit For
                    End If
                Next
            End If

            Return ret
    End Function

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopCustomerType.aspx.CmdClear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvCustomerType.CurrentPageIndex = 0
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetCustomerTypeList(uPara, True)
        uPara = Nothing
        Config.SaveTransLog("ค้นหาข้อมูล : Shop Setup >> Service", "AisWebConfig.frmShopCustomerType.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub dgvCustomerType_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvCustomerType.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
            Dim lbl_item_name As Label = DirectCast(e.Item.FindControl("lbl_item_name"), Label)
            Dim p As New ShParaDB.TABLE.TbCustomertypeShParaDB
            Dim eng As New Engine.Configuration.ShopCustomerTypeENG
            p = eng.GetShopCustomerTypePara(lbl_shop_id.Text, lbl_id.Text)
            If p.ID <> 0 Then
                ctlBranchSelected1.SelectShop = lbl_shop_id.Text
                Try
                    ddlMasterCustomerType.SelectedValue = p.MASTER_CUSTOMERTYPEID
                Catch ex As Exception
                    ddlMasterCustomerType.SelectedValue = "0"
                End Try

                hidShopCustomerTypeID.Value = lbl_id.Text
                txt_code.Text = p.CUSTOMERTYPE_CODE
                txt_name.Text = p.CUSTOMERTYPE_NAME
                If Asc(p.TXT_QUEUE.Value.ToString) <> 0 Then
                    txt_queue.Text = p.TXT_QUEUE.Value.ToString.Trim
                End If


                If p.COLOR.Value <> 0 Then
                    txt_colorcode.Text = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)).Replace("#", "").Trim
                    Dim c As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(p.COLOR)))
                    txt_color.BackColor = c
                Else
                    txt_colorcode.Text = ""
                    txt_color.BackColor = Color.White
                End If
                chk_active.Checked = IIf(p.ACTIVE_STATUS.Value = "1", True, False)
                chk_IsAppointment.Checked = IIf(p.APP.Value = "1", True, False)
                chk_IsDefaultCustomerType.Checked = IIf(p.DEF.Value = "1", True, False)
                txt_queue_no_max.Text = p.MAX_QUEUE
                txt_queue_no_min.Text = p.MIN_QUEUE
            End If
            p = Nothing
            eng = Nothing

            ddlMasterCustomerType.Enabled = True
            txt_queue_no_max.Enabled = True
            txt_queue_no_min.Enabled = True

            'ถ้าเป็น appointment Q จะถูกซ่อน
            If lbl_item_name.Text.ToLower = "appointment" Then
                txt_queue_no_max.Enabled = False
                txt_queue_no_min.Enabled = False
            End If
        End If
    End Sub

    Protected Sub dgvCustomerType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvCustomerType.PageIndexChanged
        dgvCustomerType.CurrentPageIndex = e.NewPageIndex
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        SetCustomerTypeList(uPara, False)
        uPara = Nothing
    End Sub
End Class
