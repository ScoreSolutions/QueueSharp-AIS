Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class frmMSTSoftwareSetupQSharp
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                ClearTextBox()
                'Binddata(Request("ShopID"))
                txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                txtRefreshDataSec.Attributes.Add("onKeypress", "return clickButton(event,'" + btn_save.ClientID + "') ")
            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub ClearTextBox()
        txtRefreshDataSec.Text = ""
        chkUseLdap.Checked = True
        opt_now.Checked = False
        opt_schedule.Checked = True
        txt_date.Text = Now.Date.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        ClearTextBox()
        Config.SaveTransLog("เคลียร์ข้อมูล : Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.btn_clear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        If ctlShopSelected1.SelectedShop.Rows.Count = 0 Then
            Config.ShowAlert("Please select Shop", Me)
            Exit Sub
        End If

        If opt_schedule.Checked Then
            If txt_date.Text <> "" Then
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) < Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    Exit Sub
                End If
            End If
        End If

        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim ret As Boolean = False
        If opt_now.Checked = True Then
            ret = SavedataNow()
        ElseIf opt_schedule.Checked = True Then
            DeleteCfg()
            ret = SaveDataSchedule(uPara)
        End If
        uPara = Nothing
        If ret = True Then
            Config.ShowAlert("Save Data Complete", Me)
        End If
        Config.SaveTransLog("บันทึกข้อมูล : Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.btn_save_Click", Config.GetLoginHistoryID)

    End Sub

    Private Function DeleteCfg() As Boolean
        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Try
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim shopid As String = dr("id")
                    Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                    eng.DeleteCfgQSharpSchedule(shopid, trans)
                Next
            End If

            trans.CommitTransaction()
        Catch ex As Exception
            trans.RollbackTransaction()
            ret = False
        End Try
        Return ret
    End Function

    Private Function SaveDataSchedule(ByVal uPara As CenParaDB.Common.UserProfilePara) As Boolean
        Dim ret As Boolean = False
        If Validation() = True Then
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim p As New CenParaDB.TABLE.TbCfgSwSchedQsharpCenParaDB
                    p.SHOP_ID = Convert.ToInt64(dr("id"))
                    p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
                    p.Q_REFRESH = txtRefreshDataSec.Text.Trim
                    If chkUseLdap.Checked = True Then
                        p.Q_CON_LDAP = "1"
                    Else
                        p.Q_CON_LDAP = "0"
                    End If
                    p.EVENT_STATUS = "1"

                    Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                    ret = eng.SaveCfgSchedQSharp(p, uPara.USERNAME)
                    eng = Nothing
                Next
            End If
            dt.Dispose()
        End If

        Return ret
    End Function

    Private Function SavedataNow() As Boolean
        Dim ret As Boolean = False
        If Validation() = True Then
            Dim ShowError As String = ""
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                Dim eng As New Engine.Configuration.ShopSettingENG

                For Each dr As DataRow In dt.Rows
                    Dim ShopID As String = dr("id")
                    ret = eng.SaveShopTbSetting(ShopID, txtRefreshDataSec.Text.Trim, "q_refresh")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                        Exit For
                    End If

                    Dim IsLDAP As String = ""
                    If chkUseLdap.Checked = True Then
                        IsLDAP = "1"
                    Else
                        IsLDAP = "0"
                    End If
                    ret = eng.SaveShopTbSetting(ShopID, IsLDAP, "q_con_ldap")
                    If ret = False Then
                        ShowError += eng.ErrorMessage
                    End If


                Next
                If ShowError.Trim <> "" Then
                    Config.ShowAlert(ShowError, Me)
                    ret = False
                End If
                eng = Nothing
            End If
        End If


        Return ret
    End Function

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If txtRefreshDataSec.Text.Trim = "" Then
            ret = False
            Config.ShowAlert("Please input Refresh Data every", Me, txtRefreshDataSec.ClientID)
        ElseIf ctlShopSelected1.SelectedShop.Rows.Count = 0 Then
            ret = False
            Config.ShowAlert("Please select shop", Me, txtRefreshDataSec.ClientID)
        ElseIf opt_schedule.Checked = True Then
            If txt_date.Text.Trim = "" Then
                ret = False
                Config.ShowAlert("Please select event date ", Me)
            Else
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) <= Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    Exit Function
                End If
            End If
        End If

        Return ret
    End Function

    Private Sub Binddata(ByVal ShopID As String)
        Dim eng As New Engine.Configuration.ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetTbSetting(ShopID)
        If dt.Rows.Count > 0 Then
            FillData(txtRefreshDataSec, "q_refresh", dt)

            dt.DefaultView.RowFilter = "config_name = 'q_con_ldap'"
            If dt.DefaultView.Count > 0 Then
                chkUseLdap.Checked = (dt.DefaultView(0)("config_value") = "1")
            Else
                Config.ShowAlert("Config value not found : q_con_ldap", Me)
            End If
        End If
        dt.Dispose()
        eng = Nothing

        Dim mEng As New Engine.Configuration.MasterENG
        ctlShopSelected1.ClearSelectedShop()
        ctlShopSelected1.SetSelectedShop(mEng.GetShopList("sh.id='" & ShopID & "'"))
        mEng = Nothing
    End Sub

    Private Sub FillData(ByVal txtBox As TextBox, ByVal ConfigName As String, ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.RowFilter = "config_name = '" & ConfigName & "'"
            If dt.DefaultView.Count > 0 Then
                txtBox.Text = dt.DefaultView(0)("config_value")
            Else
                Config.ShowAlert("Config value not found : " & ConfigName, Me)
            End If
        End If
    End Sub

    Private Function GetDataTabView(ByVal wh As String) As DataTable
        'Tab View
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("shop_size")
        dt.Columns.Add("shop_name")
        dt.Columns.Add("shop_code")
        dt.Columns.Add("refresh_data")

        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim lEng As New Engine.Common.LoginENG
        Dim shDt As New DataTable
        shDt = lEng.GetShopListByUser(uPara.USERNAME)
        If shDt.Rows.Count > 0 Then
            For Each shDr As DataRow In shDt.Rows
                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shDr("id"), "frmMSTSoftwareSetupQSharp_GetDataTabView")
                If shTrans.Trans IsNot Nothing Then
                    Dim RefreshData As String = Engine.Common.FunctionEng.GetShopConfig("q_refresh", shTrans)

                    Dim dr As DataRow = dt.NewRow
                    dr("id") = shDr("id")
                    dr("shop_size") = shDr("shop_size")
                    dr("shop_name") = shDr("shop_name_en")
                    dr("shop_code") = shDr("shop_code")
                    dr("refresh_data") = RefreshData
                    dt.Rows.Add(dr)
                End If
            Next
        End If

        If wh <> "" Then
            dt.DefaultView.RowFilter = wh
        End If
        shDt.Dispose()
        lEng = Nothing
        uPara = Nothing

        Return dt.DefaultView.ToTable
    End Function

    Private Sub BindTabView(ByVal wh As String)
        Dim dt As New DataTable
        dt = GetDataTabView(wh)
        If dt.DefaultView.Count > 0 Then
            dgvdetail.DataSource = dt.DefaultView.ToTable
            dgvdetail.DataBind()
        Else
            dgvdetail.DataSource = Nothing
            dgvdetail.DataBind()
            lblErrorMessage.Visible = True
        End If
        dt.Dispose()
    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        If TabContainer1.ActiveTabIndex = 1 Then
            BindTabView("1=1")
        End If
    End Sub

    Dim Valddshop_size As String = ""
    Dim Valddshop_name As String = ""
    Dim Valddrefresh_data As String = ""

#Region "TabView Filter Dropdownlist"
    Protected Sub ddshop_size_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                Valddshop_size = dd.SelectedValue.ToString
                BindTabView("shop_size = '" & dd.SelectedItem.Text & "'")
            Else
                Valddshop_size = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Shop Size : Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.ddshop_size_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try

    End Sub

    Protected Sub ddshop_name_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                Valddshop_name = dd.SelectedValue.ToString
                BindTabView("shop_name = '" & dd.SelectedItem.Text & "'")
            Else
                Valddshop_name = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Shop Name : Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.ddshop_name_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddrefresh_data_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dd As DropDownList = DirectCast(sender, DropDownList)
        If dd.SelectedItem.Text <> "All" Then
            Valddrefresh_data = dd.SelectedValue.ToString
            BindTabView("refresh_data = '" & dd.SelectedItem.Text & "'")
        Else
            Valddrefresh_data = ""
            BindTabView("")
        End If
        Config.SaveTransLog("ค้นหาข้อมูลตาม Refresh Data Every(Minute): Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.ddrefresh_data_SelectedIndexChanged", Config.GetLoginHistoryID)

    End Sub

    Protected Sub lbl_shop_Name_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            TabContainer1.ActiveTabIndex = "0"
            Dim lbl_shop_Name As LinkButton = DirectCast(sender, LinkButton)
            Dim gvr As DataGridItem = DirectCast(lbl_shop_Name.NamingContainer, DataGridItem)
            Dim lblID As Label = gvr.FindControl("lbl_id")
            Binddata(lblID.Text)
            Config.SaveTransLog("เลือกข้อมูล - " & lbl_shop_Name.Text & ": Software Setup >> Queue Sharp", "AisWebConfig.frmMSTSoftwareSetupQSharp.aspx.lbl_shop_Name_Click", Config.GetLoginHistoryID)

        Catch ex As Exception

        End Try
    End Sub
#End Region
    Protected Sub dgvdetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvdetail.ItemCreated
        If e.Item.ItemType = DataControlRowType.Header Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(uPara.USERNAME)
            If shDt.Rows.Count > 0 Then
                ''Shop Size
                'Dim ddshop_size As DropDownList = DirectCast(e.Item.FindControl("ddshop_size"), DropDownList)
                'Dim dtshop_size As New DataTable
                'dtshop_size = shDt.DefaultView.ToTable(True, "shop_size")
                'If dtshop_size.Rows.Count > 0 Then
                '    Dim dr As DataRow = dtshop_size.NewRow
                '    dr("shop_size") = ""
                '    dtshop_size.Rows.InsertAt(dr, 0)
                '    ddshop_size.DataTextField = "shop_size"
                '    ddshop_size.DataSource = dtshop_size
                '    ddshop_size.DataBind()
                'End If
                'dtshop_size.Dispose()

                ''Shop Name
                'Dim ddshop_name As DropDownList = DirectCast(e.Item.FindControl("ddshop_name"), DropDownList)
                'Dim dtshop_name As New DataTable
                'dtshop_name = shDt.DefaultView.ToTable(True, "shop_name_en")
                'If dtshop_name.Rows.Count > 0 Then
                '    Dim dr As DataRow = dtshop_name.NewRow
                '    dr("shop_name_en") = ""
                '    dtshop_name.Rows.InsertAt(dr, 0)
                '    ddshop_name.DataTextField = "shop_name_en"
                '    ddshop_name.DataSource = dtshop_name
                '    ddshop_name.DataBind()
                'End If
                'dtshop_name.Dispose()

                'Dim ddrefresh_data As DropDownList = DirectCast(e.Item.FindControl("ddrefresh_data"), DropDownList)
                'Dim dtsrefresh As New DataTable
                'dtsrefresh = GetDataTabView("").DefaultView.ToTable("refresh_data")
                'If dtsrefresh.Rows.Count > 0 Then
                '    Dim dr As DataRow = dtsrefresh.NewRow
                '    dr("refresh_data") = ""
                '    dtsrefresh.Rows.InsertAt(dr, 0)
                '    ddrefresh_data.DataTextField = "refresh_data"
                '    ddrefresh_data.DataSource = dtsrefresh
                '    ddrefresh_data.DataBind()
                'End If

                'Shop Size
                BindDataToDropdownlist(shDt, DirectCast(e.Item.FindControl("ddshop_size"), DropDownList), "shop_size")
                If Valddshop_size <> "" Then
                    Dim ddl As New DropDownList
                    ddl = DirectCast(e.Item.FindControl("ddshop_size"), DropDownList)
                    If Not ddl Is Nothing Then
                        ddl.SelectedValue = Valddshop_size
                    End If
                End If

                'Shop Name
                BindDataToDropdownlist(shDt, DirectCast(e.Item.FindControl("ddshop_name"), DropDownList), "shop_name_en")
                If Valddshop_name <> "" Then
                    Dim ddl As New DropDownList
                    ddl = DirectCast(e.Item.FindControl("ddshop_name"), DropDownList)
                    If Not ddl Is Nothing Then
                        ddl.SelectedValue = Valddshop_name
                    End If
                End If


                Dim dt As New DataTable
                dt = GetDataTabView("")
                If dt.Rows.Count > 0 Then
                    BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddrefresh_data"), DropDownList), "refresh_data")
                    If Valddrefresh_data <> "" Then
                        Dim ddl As New DropDownList
                        ddl = DirectCast(e.Item.FindControl("ddrefresh_data"), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = Valddrefresh_data
                        End If
                    End If

                End If
                dt.Dispose()
            End If
        End If
    End Sub

    Private Sub BindDataToDropdownlist(ByVal dt As DataTable, ByVal ddl As DropDownList, ByVal DataTxtFld As String)
        Dim dtDDl As New DataTable
        dtDDl = dt.DefaultView.ToTable(True, DataTxtFld)
        dtDDl.DefaultView.Sort = DataTxtFld
        If dtDDl.Rows.Count > 0 Then
            Dim dr As DataRow = dtDDl.NewRow
            dr(DataTxtFld) = "All"
            dtDDl.Rows.InsertAt(dr, 0)
            ddl.DataTextField = DataTxtFld
            ddl.DataSource = dtDDl
            ddl.DataBind()
        End If
        dtDDl.Dispose()
    End Sub
End Class
