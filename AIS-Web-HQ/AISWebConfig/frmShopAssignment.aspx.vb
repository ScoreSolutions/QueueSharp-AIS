Imports System
Imports System.Data
Imports MycustomDG
Imports Microsoft.VisualBasic
Imports System.Web.UI.ClientScriptManager
Imports System.Web.UI
Imports System.Data.OleDb

Partial Class frmShopAssignment
    Inherits System.Web.UI.Page

    Protected Sub chk_secondary_service_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim indx As DataGridItem = CType(CType(sender, CheckBoxList).Parent.Parent, DataGridItem)

            Dim dgi As DataGridItem
            For Each dgi In GrdDynamic.Items
                If dgi.ItemIndex = indx.ItemIndex Then
                    Dim cbo_primary As DropDownList = dgi.FindControl("cbo_primary")
                    Dim chk_secondary_service As CheckBoxList = dgi.FindControl("chk_secondary_service")
                    If cbo_primary.SelectedValue = chk_secondary_service.SelectedValue Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Service Primary Selected,Please select again.');", True)
                        chk_secondary_service.Items(cbo_primary.SelectedIndex).Selected = False
                    End If

                End If

            Next

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub cbo_primary_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim indx As DataGridItem = CType(CType(sender, DropDownList).Parent.Parent, DataGridItem)

            Dim dgi As DataGridItem
            For Each dgi In GrdDynamic.Items
                If dgi.ItemIndex = indx.ItemIndex Then
                    Dim cbo_primary As DropDownList = dgi.FindControl("cbo_primary")
                    Dim chk_secondary_service As CheckBoxList = dgi.FindControl("chk_secondary_service")
                    If cbo_primary.Text <> "" Then
                        For i As Integer = 0 To chk_secondary_service.Items.Count - 1
                            If cbo_primary.SelectedValue = chk_secondary_service.Items(i).Value Then
                                chk_secondary_service.Items(i).Selected = False
                                chk_secondary_service.Items(i).Enabled = False
                            Else
                                chk_secondary_service.Items(i).Enabled = True
                            End If
                        Next
                    Else
                        For i As Integer = 0 To chk_secondary_service.Items.Count - 1
                            chk_secondary_service.Items(i).Enabled = True
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub btn_Appointment_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btn As Button = DirectCast(sender, Button)
            Dim gvr As DataGridItem = DirectCast(btn.NamingContainer, DataGridItem)
            Dim itemindex As Integer = gvr.ItemIndex
            Dim gr As DataGridItem
            For Each gr In GrdDynamic.Items
                If gr.ItemIndex = itemindex Then
                    Dim btn_Appointment As Button = gr.FindControl("btn_Appointment")
                    If btn_Appointment.BackColor = Drawing.Color.White Then
                        btn_Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#0066CC")
                    Else
                        btn_Appointment.BackColor = Drawing.Color.White
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request("ShopID") Is Nothing Then
                Response.Redirect("frmShopSelectedShop.aspx?MenuID=" & Request("MenuID") & "&rnd=" & DateTime.Now.Millisecond)
            Else
                Dim sh As New CenParaDB.TABLE.TbShopCenParaDB
                Dim shEng As New Engine.Configuration.MasterENG
                sh = shEng.GetShopPara(Request("ShopID"))
                If sh.ID <> 0 Then
                    lblScreenName.Text = "Shop Setup >> Assignment >> " & sh.SHOP_NAME_EN
                End If
                sh = Nothing
                shEng = Nothing

                Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
                If uPara.LOGIN_HISTORY_ID = 0 Then
                    Session.RemoveAll()
                    Me.Response.Redirect("frmlogin.aspx")
                Else
                    'Code at Form_Load
                    InitialData()
                End If
                uPara = Nothing
            End If
            Config.SaveTransLog("คลิกเมนู : " & lblScreenName.Text, "AisWebConfig.frmShopAssignment.aspx.Page_Load", Config.GetLoginHistoryID)

            cmd_save.Visible = True
            cmd_clear.Visible = True
        End If
    End Sub


    Dim ShopUserSkillDT As New DataTable
    Dim ShopUserSkillAppDT As New DataTable

    Private Sub BindService(ByVal ShopID As Long)
        Dim ShopServiceDT As New DataTable
        Dim eng As New Engine.Configuration.ShopSettingENG
        ShopServiceDT = eng.GetShopUserService(ShopID)
        If ShopServiceDT.Rows.Count > 0 Then
            dgvservice.DataSource = ShopServiceDT
            dgvservice.DataBind()
        Else
            dgvservice.DataSource = Nothing
            dgvservice.DataBind()
        End If
        eng = Nothing
        ShopServiceDT.Dispose()
    End Sub

    Private Sub BindUser(ByVal ShopID As Long)
        Dim eng As New Engine.Configuration.ShopUserENG
        Dim dt As New DataTable
        dt = eng.GetShopUser(ShopID)
        If dt.Rows.Count > 0 Then
            GrdDynamic.DataSource = dt
            GrdDynamic.DataBind()

            InitialBindUser()
        Else
            GrdDynamic.DataSource = Nothing
            GrdDynamic.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub InitialBindUser()
        For Each grv As DataGridItem In GrdDynamic.Items
            Dim lbl_id As Label = grv.FindControl("lbl_userid")

            'Appointment Button
            Dim lbl_Appointment As Label = grv.FindControl("lbl_Appointment")
            Dim btn_Appointment As Button = grv.FindControl("btn_Appointment")

            ShopUserSkillAppDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text
            If ShopUserSkillAppDT.DefaultView.Count = 0 Then
                btn_Appointment.BackColor = Drawing.Color.Gray
                btn_Appointment.Text = ""
                btn_Appointment.Enabled = False
            Else
                btn_Appointment.Enabled = True
                btn_Appointment.BackColor = Drawing.Color.White
            End If
            ShopUserSkillAppDT.DefaultView.RowFilter = ""

            Dim cbo_primary As DropDownList = grv.FindControl("cbo_primary")
            Dim chk_secondary_service As CheckBoxList = grv.FindControl("chk_secondary_service")

            ShopUserSkillDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text
            If ShopUserSkillDT.DefaultView.Count > 0 Then
                cbo_primary.DataTextField = "item_code"
                cbo_primary.DataValueField = "item_id"
                cbo_primary.DataSource = ShopUserSkillDT.DefaultView
                cbo_primary.DataBind()

                chk_secondary_service.DataTextField = "item_code"
                chk_secondary_service.DataValueField = "item_id"
                chk_secondary_service.DataSource = ShopUserSkillDT.DefaultView
                chk_secondary_service.DataBind()

                For i As Integer = 0 To chk_secondary_service.Items.Count - 1
                    chk_secondary_service.Items(i).Selected = True
                    If chk_secondary_service.Items(i).Value = cbo_primary.SelectedValue Then
                        chk_secondary_service.Items(i).Enabled = False
                        chk_secondary_service.Items(i).Selected = False
                    End If
                Next
            Else
                cbo_primary.DataSource = Nothing
                cbo_primary.DataBind()

                chk_secondary_service.DataSource = Nothing
                chk_secondary_service.DataBind()
            End If
        Next
    End Sub

    Protected Sub cmd_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = Engine.Common.FunctionEng.GetShTransction(Request("ShopID"), "frmShopAssignment_cmd_save.Click")
        Try
            If txtAssignDateFrom.Text.Trim = "" Then
                Config.ShowAlert("Please select date from", Me)
                Exit Sub
            End If
            If txtAssignDateTo.Text.Trim = "" Then
                Config.ShowAlert("Please select date to", Me)
                Exit Sub
            End If
            If Engine.Common.FunctionEng.cStrToDate2(txtAssignDateFrom.Text) > Engine.Common.FunctionEng.cStrToDate2(txtAssignDateTo.Text) Then
                Config.ShowAlert("Date From not Over Date To,Please Select Again !!", Me)
                Exit Sub
            End If
            If Engine.Common.FunctionEng.cStrToDate2(txtAssignDateFrom.Text) < Today Then
                Config.ShowAlert("Date From not less than Today,Please Select Again !!", Me)
                Exit Sub
            End If

            Dim DateFrom As Date = Engine.Common.FunctionEng.cStrToDate2(txtAssignDateFrom.Text)
            Dim DateTo As Date = Engine.Common.FunctionEng.cStrToDate2(txtAssignDateTo.Text)
            Dim CurrDate As Date = DateFrom


            If shTrans.Trans IsNot Nothing Then
                Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
                Dim ret As Boolean = True
                Dim eng As New Engine.Configuration.ShopSettingENG
                Dim strDateFrom As String = DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim strDateTo As String = DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                eng.DeleteShopAssignment(strDateFrom, strDateTo, shTrans)

                Do
                    For Each grv As DataGridItem In GrdDynamic.Items
                        Dim lbl_id As Label = grv.FindControl("lbl_userid")
                        Dim cbo_primary As DropDownList = grv.FindControl("cbo_primary")
                        Dim chk_secondary_service As CheckBoxList = grv.FindControl("chk_secondary_service")


                        'Primary Service
                        Dim primary As Long = 0
                        If cbo_primary.SelectedValue <> "" Then
                            primary = cbo_primary.SelectedValue
                        End If
                        Dim PriPam As New ShParaDB.TABLE.TbUserServiceScheduleShParaDB
                        PriPam.SERVICE_DATE = CurrDate
                        PriPam.USER_ID = lbl_id.Text
                        PriPam.ITEM_ID = primary
                        PriPam.PRIORITY = 1
                        PriPam.ACTIVE_STATUS = 1
                        ret = eng.SaveShopUserServiceSchedule(PriPam, uPara.USER_PARA.ID, shTrans)
                        PriPam = Nothing
                        If ret = False Then
                            Config.ShowAlert(eng.ErrorMessage, Me)
                            Exit For
                        End If

                        'Secondary Service
                        For i As Integer = 0 To chk_secondary_service.Items.Count - 1
                            If chk_secondary_service.Items(i).Selected = True Then
                                Dim SecPam As New ShParaDB.TABLE.TbUserServiceScheduleShParaDB
                                SecPam.SERVICE_DATE = CurrDate
                                SecPam.USER_ID = lbl_id.Text
                                SecPam.ITEM_ID = chk_secondary_service.Items(i).Value
                                SecPam.PRIORITY = 0
                                SecPam.ACTIVE_STATUS = 1
                                ret = eng.SaveShopUserServiceSchedule(SecPam, uPara.USER_PARA.ID, shTrans)
                                SecPam = Nothing
                                If ret = False Then
                                    Config.ShowAlert(eng.ErrorMessage, Me)
                                    Exit For
                                End If
                            End If
                        Next
                        If ret = False Then
                            Exit For
                        End If

                        'Appointment User
                        Dim btn_Appointment As Button = grv.FindControl("btn_Appointment")
                        If btn_Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#0066CC") Then
                            Dim appU As New ShParaDB.TABLE.TbAppointmentUserShParaDB
                            appU.APP_DATE = CurrDate
                            appU.USER_ID = lbl_id.Text
                            ret = eng.SaveShopAppointmentUser(appU, uPara.USER_PARA.ID, shTrans)
                            appU = Nothing
                            If ret = False Then
                                Config.ShowAlert(eng.ErrorMessage, Me)
                                Exit For
                            End If
                        End If
                    Next

                    If ret = False Then
                        Exit Do
                    End If
                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                Loop While CurrDate <= DateTo
                uPara = Nothing

                If ret = True Then
                    shTrans.CommitTransaction()
                    Config.ShowAlert("Save Complete", Me)
                Else
                    shTrans.RollbackTransaction()
                End If
            Else
                Config.ShowAlert(shTrans.ErrorMessage, Me)
            End If

            Config.SaveTransLog("บันทึกข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopAssignment.aspx.cmd_save_Click", Config.GetLoginHistoryID)
        Catch ex As Exception
            shTrans.RollbackTransaction()
            Config.ShowAlert(ex.Message.ToString, Me)
        End Try
    End Sub
    Private Sub BindDataByDate(ByVal vDate As Date)
        Dim eng As New Engine.Configuration.ShopSettingENG
        ShopUserSkillDT = eng.GetShopUserSkill(Request("ShopID"))   'ข้อมูล Skill ที่มี ของพนักงาน

        Dim SkillDT As DataTable = eng.GetShopUserByDate(Request("ShopID"), vDate)
        Dim AppDT As DataTable = eng.GetShopUserAppointByDate(Request("ShopID"), vDate)

        Dim uDt As New DataTable
        uDt = SkillDT.DefaultView.ToTable(True, "id", "fullname")
        If uDt.Rows.Count > 0 Then
            GrdDynamic.DataSource = uDt
            GrdDynamic.DataBind()


            Dim TmpUserSkill As New DataTable
            TmpUserSkill = eng.GetShopUserSkillAppoint(Request("ShopID"))
            For Each grv As DataGridItem In GrdDynamic.Items
                Dim lbl_id As Label = grv.FindControl("lbl_userid")

                'Appointment Skill
                Dim lbl_Appointment As Label = grv.FindControl("lbl_Appointment")
                Dim btn_Appointment As Button = grv.FindControl("btn_Appointment")

                AppDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text
                If AppDT.DefaultView.Count = 0 Then
                    'ถ้าววันที่เลือกไม่ได้กำหนด Skill Appointment ให้ตรวจสอบว่า User นี้ มี Skill Appointment หรือไม่
                    TmpUserSkill.DefaultView.RowFilter = "user_id=" & lbl_id.Text
                    If TmpUserSkill.DefaultView.Count = 0 Then
                        btn_Appointment.BackColor = Drawing.Color.Gray
                        btn_Appointment.Text = ""
                        btn_Appointment.Enabled = False
                    Else
                        btn_Appointment.Enabled = True
                        btn_Appointment.BackColor = Drawing.Color.White
                    End If
                    TmpUserSkill.DefaultView.RowFilter = ""
                Else
                    btn_Appointment.Enabled = True
                    btn_Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#0066CC")
                End If
                AppDT.DefaultView.RowFilter = ""

                'Primary and Secondary Service
                Dim cbo_primary As DropDownList = grv.FindControl("cbo_primary")
                Dim chk_secondary_service As CheckBoxList = grv.FindControl("chk_secondary_service")

                ShopUserSkillDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text
                If ShopUserSkillDT.DefaultView.Count > 0 Then
                    cbo_primary.DataTextField = "item_code"
                    cbo_primary.DataValueField = "item_id"
                    cbo_primary.DataSource = ShopUserSkillDT.DefaultView
                    cbo_primary.DataBind()

                    SkillDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text & " and priority=1"
                    If SkillDT.DefaultView.Count > 0 Then
                        cbo_primary.SelectedValue = SkillDT.DefaultView(0)("item_id")
                    End If

                    SkillDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text
                    chk_secondary_service.DataTextField = "item_code"
                    chk_secondary_service.DataValueField = "item_id"
                    chk_secondary_service.DataSource = ShopUserSkillDT.DefaultView
                    chk_secondary_service.DataBind()

                    For i As Integer = 0 To chk_secondary_service.Items.Count - 1
                        SkillDT.DefaultView.RowFilter = "user_id=" & lbl_id.Text & " and priority=0 and item_id=" & chk_secondary_service.Items(i).Value
                        If SkillDT.DefaultView.Count > 0 Then
                            chk_secondary_service.Items(i).Selected = True
                        End If

                        If chk_secondary_service.Items(i).Value = cbo_primary.SelectedValue Then
                            chk_secondary_service.Items(i).Enabled = False
                            chk_secondary_service.Items(i).Selected = False
                        End If
                    Next
                Else
                    cbo_primary.DataSource = Nothing
                    cbo_primary.DataBind()

                    chk_secondary_service.DataSource = Nothing
                    chk_secondary_service.DataBind()
                End If
                SkillDT.DefaultView.RowFilter = ""
            Next
            TmpUserSkill.Dispose()

            cmd_save.Visible = True
            cmd_clear.Visible = True
        Else
            InitialData()
        End If
        uDt.Dispose()
        eng = Nothing


        txtAssignDateFrom.Text = vDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        txtAssignDateTo.Text = vDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
    End Sub
    Private Sub InitialData()
        'cldViewEdit.SelectedDate = Today
        Dim eng As New Engine.Configuration.ShopSettingENG
        ShopUserSkillDT = eng.GetShopUserSkill(Request("ShopID"))
        ShopUserSkillAppDT = eng.GetShopUserSkillAppoint(Request("ShopID"))
        eng = Nothing

        BindService(Request("ShopID"))
        BindUser(Request("ShopID"))

        txtAssignDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        txtAssignDateTo.Text = New Date(Now.Year, Now.Month, DateTime.DaysInMonth(Now.Year, Now.Month)).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
    End Sub

    Protected Sub cmd_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        InitialData()
        Config.SaveTransLog("เคลียร์ข้อมูล : " & lblScreenName.Text, "AisWebConfig.frmShopAssignment.aspx.cmd_clear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub cldViewEdit_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cldViewEdit.SelectionChanged
        BindDataByDate(cldViewEdit.SelectedDate)
    End Sub


    
End Class

