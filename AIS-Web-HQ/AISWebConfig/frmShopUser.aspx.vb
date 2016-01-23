Imports System
Imports System.Data
Imports Engine.Common
Imports System.Data.OleDb
Imports CenLinqDB.TABLE

Partial Class frmShopUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")
            txt_user_code.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_user_first_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_user_last_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_user_position.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txt_user_name.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")
            txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + cmd_save.ClientID + "') ")

            If Request("ShopID") Is Nothing Then
                Response.Redirect("frmShopSelectedShop.aspx?MenuID=" & Request("MenuID") & "&rnd=" & DateTime.Now.Millisecond)
            Else
                Dim sh As New CenParaDB.TABLE.TbShopCenParaDB
                Dim shEng As New Engine.Configuration.MasterENG
                sh = shEng.GetShopPara(Request("ShopID"))
                If sh.ID <> 0 Then
                    lblScreenName.Text = "Shop Setup >> User >> " & sh.SHOP_NAME_EN
                End If
                sh = Nothing
                shEng = Nothing

                lblShopID.Text = Request("ShopID")
                Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
                If uPara.LOGIN_HISTORY_ID = 0 Then
                    Session.RemoveAll()
                    Me.Response.Redirect("frmlogin.aspx")
                Else
                    txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))

                    BindTitle(lblShopID.Text)
                    BindGroup(lblShopID.Text)
                    BindSkill(lblShopID.Text)
                    SetDDLSearchShop(uPara)
                End If
                uPara = Nothing
                SearchData()
                Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopUser.aspx.Page_Load", Config.GetLoginHistoryID)
            End If
            txt_user_first_name.Attributes.Add("Style", "text-transform:uppercase")
            txt_user_last_name.Attributes.Add("Style", "text-transform:uppercase")
        End If
    End Sub

    Private Sub SetDDLSearchShop(ByVal uPara As CenParaDB.Common.UserProfilePara)
        Dim lEng As New Engine.Common.LoginENG
        Dim shDt As New DataTable
        shDt = lEng.GetShopListByUser(uPara.USERNAME)
        If shDt.Rows.Count > 0 Then
            shDt.DefaultView.Sort = "shop_name_en"
            shDt = shDt.DefaultView.ToTable
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

    Private Sub BindTitle(ByVal ShopID As String)
        Try
            Dim dt As New DataTable
            Dim mEng As New Engine.Configuration.ShopUserENG
            dt = mEng.GetShopTitleList(ShopID)
            If dt.Rows.Count > 0 Then
                cbo_titlenm.DataSource = dt
            Else
                dt.Columns.Add("id")
                dt.Columns.Add("title_name")

                Dim dr As DataRow = dt.NewRow
                dr("id") = "0"
                dr("title_name") = " -"
                dt.Rows.InsertAt(dr, 0)

                cbo_titlenm.DataSource = dt
            End If
            cbo_titlenm.DataTextField = "title_name"
            cbo_titlenm.DataValueField = "id"
            cbo_titlenm.DataBind()
            mEng = Nothing
            dt.Dispose()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindTitle Error !!,Please Check Function');", True)
        End Try
    End Sub
    Private Sub BindGroup(ByVal ShopID As String)
        Try
            Dim dt As New DataTable
            Dim mEng As New Engine.Configuration.ShopUserENG
            dt = mEng.GetShopGroupUserList(ShopID)
            If dt.Rows.Count > 0 Then
                cbo_titlenm.DataSource = dt
            Else
                dt.Columns.Add("id")
                dt.Columns.Add("group_name")

                Dim dr As DataRow = dt.NewRow
                dr("id") = "0"
                dr("group_name") = " -"
                dt.Rows.InsertAt(dr, 0)

                cbo_user_group.DataSource = dt
            End If
            cbo_user_group.DataSource = dt
            cbo_user_group.DataTextField = "group_name"
            cbo_user_group.DataValueField = "id"
            cbo_user_group.DataBind()
            mEng = Nothing
            dt.Dispose()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindGroup Error !!,Please Check Function');", True)
        End Try
    End Sub

    Private Sub BindSkill(ByVal ShopID As Long)
        Dim eng As New Engine.Configuration.ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetShopSkill(Request("ShopID"))
        If dt.Rows.Count > 0 Then
            dgvSkill.DataSource = dt
            dgvSkill.DataBind()
        Else
            dgvSkill.DataSource = Nothing
            dgvSkill.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub dgvdetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvdetail.PageIndexChanged
        dgvdetail.CurrentPageIndex = e.NewPageIndex
        SearchData()
    End Sub

    Protected Sub cmd_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        If txt_user_code.Text.Trim = "" Then
            Config.ShowAlert("Please input User Code.", Me, txt_user_code.ClientID)
            Exit Sub
        End If
        If txt_user_first_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Name.", Me, txt_user_first_name.ClientID)
            Exit Sub
        End If
        If txt_user_last_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Surname.", Me, txt_user_last_name.ClientID)
            Exit Sub
        End If
        If txt_user_position.Text.Trim = "" Then
            Config.ShowAlert("Please input Position.", Me, txt_user_position.ClientID)
            Exit Sub
        End If

        Dim i As Integer = 0
        For Each grv As DataGridItem In dgvSkill.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked = True Then
                i = 1
                Exit For
            End If
        Next
        If i = 0 Then
            Config.ShowAlert("Please select Skill.", Me)
            Exit Sub
        End If

        If txt_user_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Username.", Me, txt_user_name.ClientID)
            Exit Sub
        End If
        If txtPassword.Text.Trim = "" Then
            Config.ShowAlert("Please input Password.", Me, txtPassword.ClientID)
            Exit Sub
        End If

        Dim eng As New Engine.Configuration.ShopSettingENG
        If eng.CheckShopDataDuplicate(Request("ShopID"), "TB_USER", "user_code", txt_user_code.Text.Trim, txt_id.Text) = True Then
            Config.ShowAlert("The User Code already exists! Please re-enter the new one.", Me, txt_user_code.ClientID)
            eng = Nothing
            Exit Sub
        End If
        If eng.CheckShopDataDuplicate(Request("ShopID"), "TB_USER", "username", txt_user_name.Text.Trim, txt_id.Text) = True Then
            Config.ShowAlert("The Username already exists! Please re-enter the new one.", Me, txt_user_name.ClientID)
            eng = Nothing
            Exit Sub
        End If
        eng = Nothing

        If opt_schedule.Checked Then
            If txt_date.Text <> "" Then
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) < Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    Exit Sub
                End If
            End If
        End If

        If txt_user_name.Text.ToUpper <> "ADMIN" And cbConLDAP.Checked = True Then
            Dim ChkLDAP As CenParaDB.GateWay.LDAPResponsePara
            Dim gw As New Engine.GeteWay.GateWayServiceENG
            ChkLDAP = gw.LDAPAuth(txt_user_name.Text.Trim, txtPassword.Text.Trim)
            If ChkLDAP.RESULT = False Then
                If InStr(ChkLDAP.RESPONSE_MSG.ToUpper, "USER") > 0 Then
                    Config.ShowAlert("User not found in LDAP", Me)
                ElseIf InStr(ChkLDAP.RESPONSE_MSG.ToUpper, "PASSWORD") > 0 Then
                    Config.ShowAlert("Invalid LDAP password", Me)
                Else
                    Config.ShowAlert("Your username or password is incorrect.", Me)
                End If
                ChkLDAP = Nothing
                gw = Nothing
                Exit Sub
            End If
            ChkLDAP = Nothing
            gw = Nothing
        End If

        If opt_now.Checked = True Then
            If SaveDataNow() = True Then
                Config.ShowAlert("Save Complete", Me)
                ClearTextBox(lblShopID.Text)
            End If
        ElseIf opt_schedule.Checked = True Then
            DeleteCfg()
            If SaveDataSchedule() = True Then
                Config.ShowAlert("Save Complete", Me)
                ClearTextBox(lblShopID.Text)
            End If
        End If
        SearchData()
        Config.SaveTransLog("บันทึกข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopUser.aspx.cmd_save_Click", Config.GetLoginHistoryID)
    End Sub

    Private Function DeleteCfg() As Boolean
        Dim ret As Boolean = True

        Try
            Dim lnq As New TbCfgUserCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(" EVENT_STATUS=1 AND lower(UserName)='" & Trim(txt_user_name.Text).ToLower & "'", "", Nothing)
            If dt.Rows.Count = 0 Then
                Return True
            End If

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim cfg_user_id As String = dt.Rows(i).Item("ID").ToString
                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                eng.DeleteCfgUserSkill(cfg_user_id, trans)
                eng.DeleteCfgUser(cfg_user_id, trans)
            Next
            trans.CommitTransaction()
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

#Region "Save Data Schedule"

    Private Function SaveDataSchedule() As Boolean
        Dim ret As Boolean = False
        Dim p As New CenParaDB.TABLE.TbCfgUserCenParaDB
        p.SHOP_ID = lblShopID.Text
        p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
        p.TITLE_ID = cbo_titlenm.SelectedValue
        p.USER_CODE = txt_user_code.Text.Trim
        p.FNAME = txt_user_first_name.Text.Trim.ToUpper
        p.LNAME = txt_user_last_name.Text.Trim.ToUpper
        p.POSITION = txt_user_position.Text.Trim
        p.GROUP_ID = cbo_user_group.SelectedValue
        p.USERNAME = txt_user_name.Text.Trim
        p.PASSWORD = Engine.Common.FunctionEng.EnCripPwd(txtPassword.Text.Trim)
        If chk_active.Checked = True Then
            p.ACTIVE_STATUS = 1
        Else
            p.ACTIVE_STATUS = 0
        End If
        p.EVENT_STATUS = "1"

        Dim Trans As New CenLinqDB.Common.Utilities.TransactionDB
        If Trans.Trans IsNot Nothing Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            ret = eng.SaveCfgUser(p, uPara.USERNAME, Trans)
            If ret = False Then
                Config.ShowAlert(eng.ErrMessage, Me)
            Else
                ret = SaveSKILLSchedule(p.ID, uPara.USERNAME, Trans)
            End If
            eng = Nothing
        Else
            Config.ShowAlert(Trans.ErrorMessage, Me)
            ret = False
        End If
        If ret = True Then
            Trans.CommitTransaction()
        Else
            Trans.RollbackTransaction()
        End If

        Return ret
    End Function

    Private Function SaveSKILLSchedule(ByVal TbCfgUserID As Long, ByVal LoginName As String, ByVal Trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean = False

        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        If eng.DeleteCfgUserSkill(TbCfgUserID, Trans) = True Then
            For Each grv As DataGridItem In dgvSkill.Items
                Dim chk As CheckBox = grv.FindControl("chk")
                If chk.Checked = True Then
                    Dim lbl_id As Label = grv.FindControl("lbl_id")
                    Dim p As New CenParaDB.TABLE.TbCfgUserSkillCenParaDB
                    p.TB_CFG_USER_ID = TbCfgUserID
                    p.SKILL_ID = lbl_id.Text

                    ret = eng.SaveCfgUserSkill(p, LoginName, Trans)
                    If ret = False Then
                        Config.ShowAlert(eng.ErrMessage, Me)
                        eng = Nothing
                        Exit For
                    End If

                End If
            Next
        Else
            Config.ShowAlert(eng.ErrMessage, Me)
            ret = False
        End If
        eng = Nothing

        Return ret
    End Function
#End Region

#Region "Save Data Now"
    Private Function SaveDataNow() As Boolean
        Dim ret As Boolean = False
        Dim p As New ShParaDB.TABLE.TbUserShParaDB
        p.ID = txt_id.Text
        p.TITLE_ID = cbo_titlenm.SelectedValue
        p.USER_CODE = txt_user_code.Text.Trim
        p.FNAME = txt_user_first_name.Text.Trim.ToUpper
        p.LNAME = txt_user_last_name.Text.Trim.ToUpper
        p.POSITION = txt_user_position.Text.Trim
        p.GROUP_ID = cbo_user_group.SelectedValue
        p.USERNAME = txt_user_name.Text.Trim
        p.PASSWORD = Engine.Common.FunctionEng.EnCripPwd(txtPassword.Text.Trim)
        If chk_active.Checked = True Then
            p.ACTIVE_STATUS = 1
        Else
            p.ACTIVE_STATUS = 0
        End If

        Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(Request("ShopID"), "frmShopUser_SaveDataNow")
        If shTrans.Trans IsNot Nothing Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim eng As New Engine.Configuration.ShopSettingENG
            ret = eng.SaveShopUser(p, uPara.USERNAME, shTrans)
            If ret = False Then
                Config.ShowAlert(eng.ErrorMessage, Me)
            Else

                ret = SaveSKILLNow(p.ID, uPara.USERNAME, shTrans)
            End If
            eng = Nothing
        Else
            Config.ShowAlert(shTrans.ErrorMessage, Me)
            ret = False
        End If
        If ret = True Then
            txt_id.Text = p.ID
            shTrans.CommitTransaction()
        Else
            shTrans.RollbackTransaction()
        End If

        Return ret
    End Function
    Private Function SaveSKILLNow(ByVal UserID As Integer, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean = False
        Dim eng As New Engine.Configuration.ShopSettingENG
        ret = eng.DeleteUserSkill(UserID, shTrans)
        If ret = False Then
            Config.ShowAlert(eng.ErrorMessage, Me)
            eng = Nothing
        End If


        For Each grv As DataGridItem In dgvSkill.Items
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked = True Then
                Dim lbl_id As Label = grv.FindControl("lbl_id")
                Dim p As New ShParaDB.TABLE.TbUserSkillShParaDB
                p.USER_ID = UserID
                p.SKILL_ID = lbl_id.Text
                eng = New Engine.Configuration.ShopSettingENG
                ret = eng.SaveShopUserSkill(p, LoginName, shTrans)
                If ret = False Then
                    Config.ShowAlert(eng.ErrorMessage, Me)
                    eng = Nothing
                    Exit For
                End If
                eng = Nothing
            End If
        Next

        Return ret
    End Function
#End Region
    Private Sub ClearTextBox(ByVal ShopID As String)
        txt_id.Text = "0"
        'lblShopID.Text = ""
        txt_user_code.Text = ""
        txt_user_first_name.Text = ""
        txt_user_last_name.Text = ""
        txt_user_position.Text = ""
        txt_user_name.Text = ""
        txt_user_position.Text = ""
        chk_active.Checked = True
        lblNotFound.Visible = False
        dgvdetail.Visible = True
        If ShopID = "" Then ShopID = 0
        BindTitle(ShopID)
        BindGroup(ShopID)
        BindSkill(ShopID)
        SearchData()
        txt_date.Text = Now.Date.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        txt_search.Text = String.Empty
        SearchData()
    End Sub

    

    Protected Sub dgvdetail_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvdetail.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_shop_id As Label = DirectCast(e.Item.FindControl("lbl_shop_id"), Label)
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)

            Dim p As New ShParaDB.TABLE.TbUserShParaDB
            Dim eng As New Engine.Configuration.ShopUserENG
            p = eng.GetShopUserPara(lbl_shop_id.Text, lbl_id.Text)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txt_user_code.Text = p.USER_CODE
                txt_user_first_name.Text = p.FNAME
                txt_user_last_name.Text = p.LNAME
                Try
                    cbo_titlenm.SelectedValue = p.TITLE_ID
                Catch ex As Exception
                End Try

                cbo_user_group.SelectedValue = p.GROUP_ID
                txt_user_name.Text = p.USERNAME
                chk_active.Checked = (p.ACTIVE_STATUS = 1)
                lbl_shop_id.Text = lbl_shop_id.Text
                txt_user_position.Text = p.POSITION

                Dim sDt As New DataTable
                sDt = eng.GetShopUserSkill(lbl_shop_id.Text, lbl_id.Text)
                If sDt.Rows.Count > 0 Then
                    For Each sGrv As DataGridItem In dgvSkill.Items
                        Dim chk As CheckBox = sGrv.FindControl("chk")
                        Dim lbl_skill_id As Label = sGrv.FindControl("lbl_id")
                        sDt.DefaultView.RowFilter = "skill_id='" & lbl_skill_id.Text & "'"
                        If sDt.DefaultView.Count > 0 Then
                            chk.Checked = True
                        Else
                            chk.Checked = False
                        End If
                    Next
                End If
                sDt.Dispose()
            End If
        End If
    End Sub
    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        ClearTextBox(lblShopID.Text)
        'Config.SaveTransLog("เคลียร์ข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopUser.aspx.cmd_clear_Click", Config.GetLoginHistoryID)
    End Sub
    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvdetail.CurrentPageIndex = 0
        SearchData()
        Config.SaveTransLog("ค้นหาข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopUser.aspx.CmdSearch_Click", Config.GetLoginHistoryID)
    End Sub

    Private Sub SearchData()
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        Dim dt As New DataTable
        Dim wh As String = "u.user_code like '%" & txt_search.Text.Trim & _
        "%' or u.fname + ' ' + u.lname like '%" & txt_search.Text.Trim & "%' or position like '%" & txt_search.Text.Trim & _
        "%' or username like '%" & txt_search.Text.Trim & "%'"
        dt = eng.SearchUser(Request("ShopID"), wh, uPara.USERNAME)
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

    Protected Sub btnLDAP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLDAP.Click
        If txt_user_name.Text.Trim = "" Then
            Config.ShowAlert("Please input Username.", Me, txt_user_name.ClientID)
            Exit Sub
        End If
        If txtPassword.Text.Trim = "" Then
            Config.ShowAlert("Please input Password.", Me, txtPassword.ClientID)
            Exit Sub
        End If

        If txt_user_name.Text.Trim <> "" And txtPassword.Text.Trim <> "" Then

            Try
                'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                  Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                  chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                  sslerror As System.Net.Security.SslPolicyErrors) True

                Dim GateWay As New Engine.GeteWay.GateWayServiceENG
                Dim LDAP As New CenParaDB.GateWay.LDAPResponsePara
                LDAP = GateWay.LDAPAuth(txt_user_name.Text, txtPassword.Text)

                If LDAP.RESULT = True Then
                    Config.ShowAlert("Congratulation! Your username and password have existed in the LDAP.", Me, txt_user_name.ClientID)
                    Exit Sub
                Else
                    If InStr(LDAP.RESPONSE_MSG.ToUpper, "USER") > 0 Then
                        Config.ShowAlert("User not found", Me, txt_user_name.ClientID)
                    ElseIf InStr(LDAP.RESPONSE_MSG.ToUpper, "PASSWORD") > 0 Then
                        Config.ShowAlert("Invalid password", Me, txt_user_name.ClientID)
                    Else
                        Config.ShowAlert("Your username or password is incorrect.", Me, txt_user_name.ClientID)
                    End If
                End If
               
            Catch ex As Exception
                'MessageBox.Show("Cannot access LDAP", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try

        End If
    End Sub
End Class
