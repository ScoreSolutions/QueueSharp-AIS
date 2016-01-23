Imports System
Imports System.Data
Imports MycustomDG
Imports Microsoft.VisualBasic
Imports System.Web.UI.ClientScriptManager
Imports System.Web.UI
Imports System.Data.OleDb
Imports CenLinqDB.TABLE

Partial Class frmShopCounter
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_counter_code.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_counter_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_unit_display_id.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_date.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            If Request("ShopID") Is Nothing Then
                Response.Redirect("frmShopSelectedShop.aspx?MenuID=" & Request("MenuID") & "&rnd=" & DateTime.Now.Millisecond)
            Else
                Dim sh As New CenParaDB.TABLE.TbShopCenParaDB
                Dim shEng As New Engine.Configuration.MasterENG
                sh = shEng.GetShopPara(Request("ShopID"))
                If sh.ID <> 0 Then
                    lblScreenName.Text = "Shop Setup >> Counter >> " & sh.SHOP_NAME_EN
                End If
                sh = Nothing
                shEng = Nothing



                Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
                If uPara.LOGIN_HISTORY_ID = 0 Then
                    Session.RemoveAll()
                    Me.Response.Redirect("frmlogin.aspx")
                Else
                    'Code at Form_Load
                    txtShopID.Text = Request("ShopID")
                    Dim ctDt As New DataTable
                    Dim eng As New Engine.Configuration.ShopCounterENG
                    ctDt = eng.GetCustomerType(Request("ShopID"))
                    If ctDt.Rows.Count > 0 Then
                        SetRdiCustomerType(ctDt)
                        SetRdiOtherCustomerType(ctDt)
                    End If
                    ctDt.Dispose()

                    'SetDDLSearchShop(uPara)

                    txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                End If
                uPara = Nothing
                Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopCounter.aspx.Page_Load", Config.GetLoginHistoryID)

                SearchData()
            End If
        End If
    End Sub

    Private Sub SetRdiCustomerType(ByVal ctDt As DataTable)
        opt_custype.DataTextField = "customertype_name"
        opt_custype.DataValueField = "id"
        opt_custype.DataSource = ctDt
        opt_custype.DataBind()
    End Sub

    Private Sub SetRdiOtherCustomerType(ByVal ctDt As DataTable)
        chk_allow.DataTextField = "customertype_name"
        chk_allow.DataValueField = "id"
        chk_allow.DataSource = ctDt
        chk_allow.DataBind()
    End Sub

    'Private Sub SetDDLSearchShop(ByVal uPara As CenParaDB.Common.UserProfilePara)
    '    Dim lEng As New Engine.Common.LoginENG
    '    Dim shDt As New DataTable
    '    shDt = lEng.GetShopListByUser(uPara.USERNAME)
    '    If shDt.Rows.Count > 0 Then
    '        shDt.DefaultView.Sort = "shop_name_en"
    '        shDt = shDt.DefaultView.ToTable
    '    End If

    '    Dim dr As DataRow = shDt.NewRow
    '    dr("id") = "0"
    '    dr("shop_name_en") = " All Shop "
    '    shDt.Rows.InsertAt(dr, 0)

    '    ddlSearchShop.DataTextField = "shop_name_en"
    '    ddlSearchShop.DataValueField = "id"
    '    ddlSearchShop.DataSource = shDt
    '    ddlSearchShop.DataBind()

    '    shDt.Dispose()
    '    lEng = Nothing
    'End Sub

    Protected Sub cmd_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        If Validation() = True Then
            If opt_now.Checked = True Then
                If SaveDataNow() = True Then
                    Config.ShowAlert("Save Complete", Me)
                End If
            ElseIf opt_schedule.Checked = True Then
                DeleteCfg()
                If SaveDataSchedule() = True Then
                    Config.ShowAlert("Save Complete", Me)
                End If
            End If

            dgvdetail.CurrentPageIndex = 0
            SearchData()
            Config.SaveTransLog("บันทึกข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopCounter.aspx.cmd_save_Click", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Function DeleteCfg() As Boolean
        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Try

            Dim lnq As New TbCfgCounterCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(" EVENT_STATUS=1 and SHOP_ID='" & txtShopID.Text & "'", "", trans.Trans)
            If dt.Rows.Count > 0 Then
                dt.CaseSensitive = False
                dt.DefaultView.RowFilter = "COUNTER_NAME='" & Trim(txt_counter_name.Text).ToLower & "'"
                If dt.DefaultView.Count = 0 Then
                    dt.DefaultView.RowFilter = "COUNTER_CODE='" & Trim(txt_counter_code.Text).ToLower & "'"
                    If dt.DefaultView.Count = 0 Then
                        dt.DefaultView.RowFilter = "unitdisplay='" & txt_unit_display_id.Text & "'"
                        If dt.DefaultView.Count = 0 Then
                            Return True
                        End If
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim cfg_counter_id As String = dt.Rows(i).Item("ID").ToString
                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                eng.DeleteCfgCounterCustomerAllow(cfg_counter_id, trans)
                eng.DeleteCfgCounterCustomerType(cfg_counter_id, trans)
                eng.DeleteCfgCounter(cfg_counter_id, trans)
            Next

            trans.CommitTransaction()
        Catch ex As Exception
            trans.RollbackTransaction()
            ret = False
        End Try
        Return ret
    End Function

    Private Function SaveDataSchedule() As Boolean
     
        Dim ret As Boolean = False
        Dim p As New CenParaDB.TABLE.TbCfgCounterCenParaDB
        p.SHOP_ID = txtShopID.Text
        p.COUNTER_CODE = txt_counter_code.Text.Trim
        p.COUNTER_NAME = txt_counter_name.Text.Trim
        p.UNITDISPLAY = txt_unit_display_id.Text
        p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
        p.REF_SHOP_COUNTER_ID = txt_id.Text
        p.EVENT_STATUS = "1"
        If chk_Active.Checked = True Then
            p.ACTIVE_STATUS = 1
        Else
            p.ACTIVE_STATUS = 0
        End If
        If chk_Quick_Services.Checked = True Then
            p.QUICKSERVICE = 1
        Else
            p.QUICKSERVICE = 0
        End If
        If chk_Speed_Lane.Checked = True Then
            p.SPEED_LANE = 1
        Else
            p.SPEED_LANE = 0
        End If
        If chk_Back_Office.Checked = True Then
            p.BACK_OFFICE = 1
        Else
            p.BACK_OFFICE = 0
        End If
        If chk_Counter_Manager.Checked = True Then
            p.COUNTER_MANAGER = 1
        Else
            p.COUNTER_MANAGER = 0
        End If

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            p.ID = eng.SaveCfgCounter(p, uPara.USERNAME, trans)
            If p.ID > 0 Then
                Dim pCt As New CenParaDB.TABLE.TbCfgCounterCustomerTypeCenParaDB
                pCt.TB_CFG_COUNTER_ID = p.ID
                pCt.TB_CUSTOMERTYPE_ID = opt_custype.SelectedValue
                ret = eng.SaveCfgCounterCustomertype(pCt, uPara.USERNAME, trans)

                If ret = True Then
                    If eng.DeleteCfgCounterCustomerAllow(p.ID, trans) = True Then
                        For Each chk As ListItem In chk_allow.Items
                            If chk.Selected = True Then
                                Dim pCa As New CenParaDB.TABLE.TbCfgCounterCusttypeAllowCenParaDB
                                pCa.TB_CFG_COUNTER_ID = p.ID
                                pCa.TB_CUSTOMERTYPE_ID = chk.Value

                                ret = eng.SaveCfgCounterCusttypeAllow(pCa, uPara.USERNAME, trans)
                                If ret = False Then
                                    Config.ShowAlert(eng.ErrMessage, Me)
                                    Engine.Common.FunctionEng.SaveErrorLog("frmShopCounter.SaveDataSchedule", eng.ErrMessage)
                                    Return False
                                End If
                            End If
                        Next

                        If ret = True Then
                            trans.CommitTransaction()
                            ClearForm()
                        Else
                            trans.RollbackTransaction()
                            Return False
                        End If
                    Else
                        trans.RollbackTransaction()
                        Config.ShowAlert(eng.ErrMessage, Me)
                        Engine.Common.FunctionEng.SaveErrorLog("frmShopCounter.SaveDataSchedule", eng.ErrMessage)
                        Return False
                    End If
                Else
                    trans.RollbackTransaction()
                    Config.ShowAlert(eng.ErrMessage, Me)
                    Engine.Common.FunctionEng.SaveErrorLog("frmShopCounter.SaveDataSchedule", eng.ErrMessage)
                    Return False
                End If
            Else
                trans.RollbackTransaction()
                Config.ShowAlert(eng.ErrMessage, Me)
                Engine.Common.FunctionEng.SaveErrorLog("frmShopCounter.SaveSataSchedule", eng.ErrMessage)
                Return False
            End If
            eng = Nothing
            uPara = Nothing
        End If
        Return ret
    End Function

    Private Function SaveDataNow() As Boolean
        Dim ret As Boolean = False
        Dim p As New ShParaDB.TABLE.TbCounterShParaDB
        p.ID = txt_id.Text
        p.COUNTER_CODE = txt_counter_code.Text.Trim
        p.COUNTER_NAME = txt_counter_name.Text.Trim
        p.UNITDISPLAY = txt_unit_display_id.Text
        If chk_Active.Checked = True Then
            p.ACTIVE_STATUS = 1
        Else
            p.ACTIVE_STATUS = 0
        End If
        If chk_Quick_Services.Checked = True Then
            p.QUICKSERVICE = 1
        Else
            p.QUICKSERVICE = 0
        End If
        If chk_Speed_Lane.Checked = True Then
            p.SPEED_LANE = 1
        Else
            p.SPEED_LANE = 0
        End If
        If chk_Back_Office.Checked = True Then
            p.BACK_OFFICE = 1
        Else
            p.BACK_OFFICE = 0
        End If
        If chk_Counter_Manager.Checked = True Then
            p.COUNTER_MANAGER = 1
        Else
            p.COUNTER_MANAGER = 0
        End If

        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = Engine.Common.FunctionEng.GetShTransction(txtShopID.Text, "frmShopCounter_SaveDataNow")
        If shTrans.Trans IsNot Nothing Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim eng As New Engine.Configuration.ShopCounterENG
            p.ID = eng.SaveShopCounter(p, uPara.USERNAME, txtShopID.Text, shTrans)
            If p.ID > 0 Then
                eng.DeleteCounterCustomerType(p.ID, shTrans)

                Dim pCt As New ShParaDB.TABLE.TbCounterCustomertypeShParaDB
                pCt.COUNTER_ID = p.ID
                pCt.CUSTOMERTYPE_ID = opt_custype.SelectedValue
                ret = eng.SaveCounterCustomerType(pCt, uPara.USERNAME, shTrans)

                If ret = True Then
                    eng.DeleteCounterCustomerTypeAllow(p.ID, shTrans)
                    For Each chk As ListItem In chk_allow.Items
                        If chk.Selected = True Then
                            Dim pCa As New ShParaDB.TABLE.TbCounterCustomertypeAllowShParaDB
                            pCa.COUNTER_ID = p.ID
                            pCa.CUSTOMERTYPE_ID = chk.Value

                            ret = eng.SaveCounterCustomerTypeAllow(pCa, uPara.USERNAME, shTrans)
                            If ret = False Then
                                Config.ShowAlert(eng.ErrorMessage, Me)
                                Exit For
                            End If
                        End If
                    Next

                    If ret = True Then
                        shTrans.CommitTransaction()
                        txt_id.Text = p.ID
                    Else
                        shTrans.RollbackTransaction()
                    End If
                Else
                    shTrans.RollbackTransaction()
                    Config.ShowAlert(eng.ErrorMessage, Me)
                End If
            Else
                shTrans.RollbackTransaction()
                Config.ShowAlert(eng.ErrorMessage, Me)
            End If
            eng = Nothing
            uPara = Nothing
        Else
            Config.ShowAlert(shTrans.ErrorMessage, Me)
        End If

        Return ret
    End Function

    Private Function Validation() As Boolean
        If txt_counter_code.Text.Trim = "" Then
            Config.ShowAlert("Please input counter code", Me, txt_counter_code.ClientID)
            Return False
        End If
        If txt_counter_name.Text.Trim = "" Then
            Config.ShowAlert("Please input counter name", Me, txt_counter_code.ClientID)
            Return False
        End If

        Dim ctSel As Boolean = False
        Dim ctSelVal As String = "0"
        For Each rdi As ListItem In opt_custype.Items
            If rdi.Selected = True Then
                ctSel = True
                ctSelVal = rdi.Value
            End If
        Next
        If ctSel = False Then
            Config.ShowAlert("Please select customer type", Me)
            Return False
        End If

        Dim chkSel As Boolean = False
        For Each chk As ListItem In chk_allow.Items
            If chk.Selected = True Then
                If chk.Value = ctSelVal Then
                    Config.ShowAlert("Customer Type Allow cannot be the same as the Customer Type.", Me)
                    Return False
                End If
            End If
        Next

        If opt_schedule.Checked Then
            If txt_date.Text <> "" Then
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) < Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    Return False
                End If
            End If
        End If

        'เช็คค่าซ้ำด้วย
        Dim eng As New Engine.Configuration.ShopCounterENG
        If eng.CheckDupByCounterCode(txt_counter_code.Text.Trim, txt_id.Text, txtShopID.Text) = True Then
            Config.ShowAlert("The Counter Code already exists! Please re-enter the new one.", Me)
            eng = Nothing
            Return False
        End If
        If eng.CheckDupByCounterName(txt_counter_name.Text.Trim, txt_id.Text, txtShopID.Text) = True Then
            Config.ShowAlert("The Counter Name already exists! Please re-enter the new one.", Me)
            eng = Nothing
            Return False
        End If
        If eng.CheckDupByCounterUnitDisplayID(txt_unit_display_id.Text.Trim, txt_id.Text, txtShopID.Text) = True Then
            Config.ShowAlert("The Unit Display ID already exists! Please re-enter the new one.", Me)
            eng = Nothing
            Return False
        End If


        Return True
    End Function

    Private Sub ClearForm()
        txt_id.Text = "0"
        txt_counter_code.Text = ""
        txt_counter_name.Text = ""
        txt_unit_display_id.Text = "0"
        chk_Quick_Services.Checked = False
        chk_Speed_Lane.Checked = False
        chk_Back_Office.Checked = False
        chk_Counter_Manager.Checked = False
        chk_Active.Checked = True
        lblNotFound.Visible = False
        dgvdetail.Visible =True

        For Each rdi As ListItem In opt_custype.Items
            rdi.Selected = False
        Next
        For Each chk As ListItem In chk_allow.Items
            chk.Selected = False
        Next
        txt_date.Text = Now.Date.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        opt_schedule.Checked = True
        txt_search.Text = String.Empty
        SearchData()
    End Sub

    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        ClearForm()
        Config.SaveTransLog("เคลียร์ข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopCounter.aspx.cmd_clear_Click", Config.GetLoginHistoryID)

    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvdetail.CurrentPageIndex = 0
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopCounter.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Private Sub SearchData()
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        Dim wh As String = ""
        If txt_search.Text.Trim <> "" Then
            wh = " counter_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or counter_name like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.SearchCounter(txtShopID.Text, wh, uPara.USERNAME)

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
        uPara = Nothing
    End Sub

    Protected Sub dgvdetail_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvdetail.EditCommand
        If e.CommandName = "Edit" Then
            ClearForm()
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)

            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(lbl_shop_id.Text, "frmShopCounter_dgvdetail.EditCommand")
            Dim p As New ShParaDB.TABLE.TbCounterShParaDB
            Dim eng As New Engine.Configuration.ShopCounterENG
            p = eng.GetShopCounterPara(lbl_id.Text, lbl_shop_id.Text, shTrans)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txt_counter_code.Text = p.COUNTER_CODE
                txt_counter_name.Text = p.COUNTER_NAME
                chk_Quick_Services.Checked = (p.QUICKSERVICE = 1)
                chk_Speed_Lane.Checked = (p.SPEED_LANE = 1)
                txt_unit_display_id.Text = p.UNITDISPLAY
                chk_Back_Office.Checked = (p.BACK_OFFICE = 1)
                chk_Counter_Manager.Checked = (p.COUNTER_MANAGER = 1)
                chk_Active.Checked = (p.ACTIVE_STATUS = 1)
                lbl_shop_id.Text = lbl_shop_id.Text

                Dim cDt As New DataTable
                cDt = eng.GetShopCounterCustomerType(txt_id.Text, shTrans)
                If cDt.Rows.Count > 0 Then
                    For Each rdi As ListItem In opt_custype.Items
                        cDt.DefaultView.RowFilter = "customertype_id='" & rdi.Value & "'"
                        If cDt.DefaultView.Count > 0 Then
                            rdi.Selected = True
                            Exit For
                        End If
                    Next
                End If
                cDt.Dispose()


                Dim caDt As New DataTable
                caDt = eng.GetShopCounterCustomerTypeAllow(txt_id.Text, shTrans)
                If caDt.Rows.Count > 0 Then
                    For Each chk As ListItem In chk_allow.Items
                        caDt.DefaultView.RowFilter = "customertype_id='" & chk.Value & "'"
                        If caDt.DefaultView.Count > 0 Then
                            chk.Selected = True
                        End If
                    Next
                End If
                caDt.Dispose()
            End If
            p = Nothing
            eng = Nothing
            shTrans.CommitTransaction()
        End If
    End Sub

    Protected Sub dgvdetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvdetail.PageIndexChanged
        dgvdetail.CurrentPageIndex = e.NewPageIndex
        SearchData()
    End Sub

End Class
