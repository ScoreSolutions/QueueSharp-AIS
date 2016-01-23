Imports System.Data
Imports Engine.Common
Partial Class CSIWebApp_frmFilterTemplateForm
    Inherits System.Web.UI.Page

    Const SessSelectUser As String = "SessSelectUser"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Session.Remove(SessSelectUser)
            txtFilterName.Attributes.Add("onKeypress", "return clickButton(event,'" & btnSave.ClientID & "')")
            
            SetServiceList(0)
            SetShopList(0)
            SetCheckBokList()
            SetSegmentList()
            SetPeriodTime()
            SetCategory()

            CmbCatGenActAllSurvey_SelectedIndexChanged(sender, e)
            CmbCatGenActAllSurveyUPC_SelectedIndexChanged(sender, e)
            CmbCatGenActResult3_SelectedIndexChanged(sender, e)
            CmbCatGenActResult3UPC_SelectedIndexChanged(sender, e)
            chkGenActAllSurvey_CheckedChanged(sender, e)
            chkGenActResult3_CheckedChanged(sender, e)

            If Request("id") IsNot Nothing Then
                Dim vID As Long = Request("id")
                FillData(vID)
                SetUserList(vID)
                SetShopList(vID)
                SetServiceList(vID)
                If Request("cmd") = "copy" Then
                    txtID.Text = "0"
                    txtFilterName.Text = txtFilterName.Text & "_Duplicate"
                End If
            End If
        End If
    End Sub

#Region "Sub&Function"

    Private Sub SetCategory()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetCategory("AIS")

        Dim dr As DataRow = dt.NewRow
        dr("id") = ""
        dr("category_name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        CmbCatGenActAllSurvey.SetItemList(dt, "category_name", "id")
        CmbCatGenActAllSurveyUPC.SetItemList(dt, "category_name", "id")
        CmbCatGenActResult3.SetItemList(dt, "category_name", "id")
        CmbCatGenActResult3UPC.SetItemList(dt, "category_name", "id")

        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetSubCatGenActAllSurvey(ByVal category_name As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetSubCategory(category_name, "AIS")

        Dim dr As DataRow = dt.NewRow
        dr("id") = ""
        dr("subcategory_name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        CmbSubCatGenActAllSurvey.SetItemList(dt, "subcategory_name", "id")

        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetSubCatGenActAllSurveyUPC(ByVal category_name As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetSubCategory(category_name, "AIS")

        Dim dr As DataRow = dt.NewRow
        dr("id") = ""
        dr("subcategory_name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        CmbSubCatGenActAllSurveyUPC.SetItemList(dt, "subcategory_name", "id")

        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetSubCatGenActResult3(ByVal category_name As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetSubCategory(category_name, "AIS")

        Dim dr As DataRow = dt.NewRow
        dr("id") = ""
        dr("subcategory_name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        CmbSubCatGenActResult3.SetItemList(dt, "subcategory_name", "id")

        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetSubCatGenActResult3UPC(ByVal category_name As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetSubCategory(category_name, "AIS")

        Dim dr As DataRow = dt.NewRow
        dr("id") = ""
        dr("subcategory_name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        CmbSubCatGenActResult3UPC.SetItemList(dt, "subcategory_name", "id")

        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetPeriodTime()
        Dim dt As New DataTable
        dt.Columns.Add("value")
        dt.Columns.Add("display")
        For i As Integer = 8 To 20
            Dim dr As DataRow = dt.NewRow
            dr("value") = i.ToString.PadLeft(2, "0") & ":00"
            dr("display") = i.ToString.PadLeft(2, "0") & ":00"
            dt.Rows.Add(dr)
        Next

        cmbTimeFrom.SetItemList(dt, "display", "value")
        cmbTimeTo.SetItemList(dt, "display", "value")
        dt = Nothing
    End Sub

    Private Sub SetUserList(ByVal FilterID As Long)
        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim dt As DataTable = eng.GetFilterUser(FilterID)
        If dt.Rows.Count > 0 Then
            Session(SessSelectUser) = dt
            gvUserName.DataSource = dt
            gvUserName.DataBind()
        Else
            gvUserName.DataSource = Nothing
            gvUserName.DataBind()
            Session.Remove(SessSelectUser)
        End If
    End Sub
    Protected Function CheckNull(ByVal objGrid As Object) As String
        If Object.ReferenceEquals(objGrid, DBNull.Value) Then
            Return "false"
        Else
            Return "true"
        End If
    End Function

    Private Sub SetServiceList(ByVal FilterID As Long)
        Dim engS As New Engine.CSI.FilterTemplateENG
        Dim dtS As DataTable = engS.GetFilterServiceList(txtID.Text)
        If dtS.Rows.Count > 0 Then
            gvService.DataSource = dtS
            gvService.DataBind()
            dtS = Nothing

            Dim lblTotTargetPer As Label = gvService.FooterRow.FindControl("lblTotTargetPer")
            For Each grv As GridViewRow In gvService.Rows
                Dim txtTargetPercent As UserControls_txtAutoComplete = grv.FindControl("txtTargetPercent")
                txtTargetPercent.Attributes.Add("onBlur", "CalTotTarget('" & lblTotTargetPer.ClientID & "')")
            Next
        Else
            gvService.DataSource = Nothing
            gvService.DataBind()
        End If
    End Sub

    Private Sub SetCheckBokList()
        Dim eng As New Engine.Configuration.MasterENG
        Dim dtT As DataTable = eng.GetCategoryList
        If dtT.Rows.Count > 0 Then
            cmbCategory.SetItemList(dtT, "display", "value")
            cmbCategory.SelectedValue = FunctionEng.GetQisDBConfig("CSI_DF_CATEGORY")
        End If
        dtT = Nothing

        Dim dtC As DataTable = eng.GetContactClass
        If dtC.Rows.Count > 0 Then
            cmbContactClass.SetItemList(dtC, "display", "value")
            cmbContactClass.SelectedValue = FunctionEng.GetQisDBConfig("CSI_DF_CONTACT_CLASS")
        End If
        dtC = Nothing


        Dim dtN As New DataTable
        dtN.Columns.Add("Type", GetType(String))
        dtN.Columns.Add("Value", GetType(String))
        Dim dr As DataRow

        dr = dtN.NewRow
        dr("Type") = "All"
        dr("Value") = "All"
        dtN.Rows.Add(dr)

        dr = dtN.NewRow
        dr("Type") = "2G"
        dr("Value") = "2G"
        dtN.Rows.Add(dr)

        dr = dtN.NewRow
        dr("Type") = "3G"
        dr("Value") = "3G"
        dtN.Rows.Add(dr)
        cmbNetworkType.SetItemList(dtN, "Type", "Value")


        eng = Nothing
    End Sub

    Function GetNationalityValue() As String
        Dim value As String = ""
        For i As Integer = 0 To chkNationality.Items.Count - 1
            If chkNationality.Items(i).Selected Then
                value &= chkNationality.Items(i).Value & ","
            End If
            If i = chkNationality.Items.Count - 1 And value <> "" Then
                value = value.Substring(0, value.Length - 1)
            End If
        Next
        Return value
    End Function

    Private Function Valid() As Boolean
        Dim ret As Boolean = True
        If txtFilterName.Text.Trim = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Filter Name", Me, txtFilterName.ClientID)
        ElseIf cmbContactClass.SelectedValue = "0" Then
            ret = False
            Config.SetAlert("กรุณาเลือก Contact Class", Me, cmbContactClass.ClientID)
        ElseIf cmbCategory.SelectedValue = "0" Then
            ret = False
            Config.SetAlert("กรุณาเลือก Customer Type", Me, cmbCategory.ClientID)
        ElseIf GetNationalityValue() = "" Then
            ret = False
            Config.SetAlert("กรุณาเลือก Nationality", Me)
        ElseIf CalPercent() = False Then
            ret = False
            Config.SetAlert("% Target ของทุกบริการรวมกัน ต้องเท่ากับ 100 %", Me)
        ElseIf txtPeriodDateFrom.DateValue.Year = 1 Then
            ret = False
            Config.SetAlert("กรุณาระบุ Period Date From", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtPeriodDateTo.DateValue.Year = 1 Then
            ret = False
            Config.SetAlert("กรุณาระบุ Period Date To", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtPeriodDateFrom.DateValue.ToString("yyyyMMdd") > txtPeriodDateTo.DateValue.ToString("yyyyMMdd") Then
            ret = False
            Config.SetAlert("กรุณาระบุ Period Date From ให้น้อยกว่า Period Date To", Me, txtPeriodDateFrom.ClientID)
        ElseIf cmbTimeFrom.SelectedValue >= cmbTimeTo.SelectedValue Then
            ret = False
            Config.SetAlert("กรุณาระบุ Period Time From ให้น้อยกว่า Period Time To", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtTemplateCode.Text.Trim = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Template Code", Me, txtTemplateCode.ClientID)
        ElseIf chkUnlimited.Checked = False And (txtTarget.Text.Trim = "" Or txtTarget.Text.Trim = "0") Then
            ret = False
            Config.SetAlert("กรุณาระบุ Target", Me, txtTarget.ClientID)
        ElseIf rdiAfterEndQueue.Checked = True And txtAfterEndMin.Text.Trim = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุเวลาหลังจบบริการ", Me, txtAfterEndMin.ClientID)
        ElseIf rdiEveryTime.Checked = True And txtEveryMin.Text.Trim = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุเวลาทุกๆ กี่นาที", Me, txtEveryMin.ClientID)
        End If


        If ret = True Then
            Dim ShopSelect As New DataTable
            ShopSelect = GetSelectedShopDT()
            If ShopSelect.Rows.Count = 0 Then
                Config.SetAlert("กรุณาเลือก Shop", Me)
            End If

            Dim tmpdr() As DataRow
            tmpdr = ShopSelect.Select("ShTemplateCode = ''")
            If tmpdr.Length > 0 Then
                Config.SetAlert("กรุณาระบุ Template Code by shop", Me)
            End If
        End If


        If ret = True Then
            If chkGenActAllSurvey.Checked = True Then
                If CmbCatGenActAllSurvey.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Category", Me, CmbCatGenActAllSurvey.ClientID)
                ElseIf CmbSubCatGenActAllSurvey.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Sub Category", Me, CmbSubCatGenActAllSurvey.ClientID)
                ElseIf CmbCatGenActAllSurveyUPC.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Category UPC", Me, CmbCatGenActAllSurveyUPC.ClientID)
                ElseIf CmbSubCatGenActAllSurveyUPC.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Sub Category UPC", Me, CmbSubCatGenActAllSurveyUPC.ClientID)
                End If
            End If
        End If

        If ret = True Then
            If chkGenActResult3.Checked = True Then
                If CmbCatGenActResult3.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Category", Me, CmbCatGenActResult3.ClientID)
                ElseIf CmbSubCatGenActResult3.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Sub Category", Me, CmbSubCatGenActResult3.ClientID)
                ElseIf CmbCatGenActResult3UPC.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Category UPC", Me, CmbCatGenActResult3UPC.ClientID)
                ElseIf CmbSubCatGenActResult3UPC.SelectedValue = "" Then
                    ret = False
                    Config.SetAlert("กรุณาระบุ Sub Category UPC", Me, CmbSubCatGenActResult3UPC.ClientID)
                End If
            End If
        End If

        If ret = True Then
            Dim eng As New Engine.CSI.FilterTemplateENG
            If eng.CheckDuplicateFilter(txtFilterName.Text, txtID.Text) = True Then
                ret = False
                Config.SetAlert("Filter Name ซ้ำ", Me, txtFilterName.ClientID)
            End If
            eng = Nothing
        End If

        Return ret
    End Function

    Private Function CalPercent() As Boolean
        Dim ret As Boolean = True
        Dim p As Integer = 0
        For Each grv As GridViewRow In gvService.Rows
            Dim txtTargetPercent As UserControls_txtAutoComplete = grv.FindControl("txtTargetPercent")
            If txtTargetPercent.Text.Trim <> "" Then
                p += Convert.ToInt16(txtTargetPercent.Text)
            End If
        Next

        If p <> 100 Then
            ret = False
        End If
        Return ret
    End Function

    Private Function GetSelectedSegment() As String
        Dim ret As String = "0"
        For Each grv As GridViewRow In gvSegment.Rows
            Dim chk As CheckBox = grv.FindControl("chk")
            If chk.Checked = True Then
                If ret = "0" Then
                    ret = grv.Cells(1).Text
                Else
                    ret += "," & grv.Cells(1).Text
                End If
            End If
        Next
        Return ret
    End Function

    Private Function GetData() As CenParaDB.TABLE.TbFilterCenParaDB
        Dim para As New CenParaDB.TABLE.TbFilterCenParaDB
        para.ID = txtID.Text
        para.FILTER_NAME = txtFilterName.Text.Trim
        para.CATEGORY = cmbCategory.SelectedValue
        para.CONTACT_CLASS = cmbContactClass.SelectedValue
        para.NATIONALITY = GetNationalityValue()
        para.SEGMENT = GetSelectedSegment()
        para.PREIOD_DATEFROM = txtPeriodDateFrom.DateValue
        para.PREIOD_DATETO = txtPeriodDateTo.DateValue
        para.PREIOD_TIMEFROM = cmbTimeFrom.SelectedValue
        para.PREIOD_TIMETO = cmbTimeTo.SelectedValue
        para.SCHEDULETYPEDAY = rdiScheduleTypeDay.SelectedValue
        para.SCHEDULEMONDAY = "N"
        para.SCHEDULETUEDAY = "N"
        para.SCHEDULEWEDDAY = "N"
        para.SCHEDULETHUDAY = "N"
        para.SCHEDULEFRIDAY = "N"
        para.SCHEDULESATDAY = "N"
        para.SCHEDULESUNDAY = "N"
        If rdiScheduleTypeDay.SelectedValue = "1" Then
            For Each chkSchDay As ListItem In chkScheduleDay.Items
                If chkSchDay.Selected Then
                    Select Case chkSchDay.Value
                        Case "MON"
                            para.SCHEDULEMONDAY = "Y"
                        Case "TUE"
                            para.SCHEDULETUEDAY = "Y"
                        Case "WED"
                            para.SCHEDULEWEDDAY = "Y"
                        Case "THU"
                            para.SCHEDULETHUDAY = "Y"
                        Case "FRI"
                            para.SCHEDULEFRIDAY = "Y"
                        Case "SAT"
                            para.SCHEDULESATDAY = "Y"
                        Case "SUN"
                            para.SCHEDULESUNDAY = "Y"
                    End Select
                End If
            Next
        End If

        If chkUnlimited.Checked = False Then
            para.TARGET = txtTarget.Text
            para.TARGET_UNLIMITED = "N"
            para.CAL_TARGET = "N"
        Else
            para.TARGET_UNLIMITED = "Y"
            para.CAL_TARGET = "Y"
        End If

        para.TEMPLATE_CODE = txtTemplateCode.Text
        If rdiAfterEndQueue.Checked = True Then
            para.DURATION_TYPE = "0"
            para.DURATION_AFTER_MIN = txtAfterEndMin.Text.Trim
        ElseIf rdiEveryTime.Checked = True Then
            para.DURATION_TYPE = "1"
            para.DURATION_EVERY_MIN = txtEveryMin.Text.Trim
        End If
        If rdiStatusActive.Checked = True Then
            para.ACTIVE_STATUS = "Y"
        ElseIf rdiStatusHold.Checked = True Then
            para.ACTIVE_STATUS = "N"
        End If
        para.NETWORK_TYPE = cmbNetworkType.SelectedValue
        para.BLANK_CATEGORY = IIf(chkISBlankCategory.Checked, "Y", "N")
        para.BLANK_CONTACT_CLASS = IIf(chkISBlankContactClass.Checked, "Y", "N")
        para.SURVEY_ALL_SHOP = rdiSurveyAllShop.SelectedValue

        'Auto Gen Activity
        If chkGenActAllSurvey.Checked = True Then
            para.GEN_ACT_ALL_SURVEY = "Y"
            para.CATEGORY_GEN_ACT_ALL_SURVEY = CmbCatGenActAllSurvey.SelectedValue
            para.SUBCATEGORY_GEN_ACT_ALL_SURVEY = CmbSubCatGenActAllSurvey.SelectedValue
            para.CAT_GEN_ACT_ALL_SURVEY_UPC = CmbCatGenActAllSurveyUPC.SelectedValue
            para.SUBCAT_GEN_ACT_ALL_SURVEY_UPC = CmbSubCatGenActAllSurveyUPC.SelectedValue
        Else
            para.GEN_ACT_ALL_SURVEY = "N"
            para.CATEGORY_GEN_ACT_ALL_SURVEY = ""
            para.SUBCATEGORY_GEN_ACT_ALL_SURVEY = ""
            para.CAT_GEN_ACT_ALL_SURVEY_UPC = ""
            para.SUBCAT_GEN_ACT_ALL_SURVEY_UPC = ""
        End If
        para.OWNER_GEN_ACT_ALL_SURVEY = txtOwnerGenActAllSurvey.Text

        If chkGenActResult3.Checked = True Then
            para.GEN_ACT_RESULT3 = "Y"
            para.CATEGORY_GEN_ACT_RESULT3 = CmbCatGenActResult3.SelectedValue
            para.SUBCATEGORY_GEN_ACT_RESULT3 = CmbSubCatGenActResult3.SelectedValue
            para.CAT_GEN_ACT_RESULT3_UPC = CmbCatGenActResult3UPC.SelectedValue
            para.SUBCAT_GEN_ACT_RESULT3_UPC = CmbSubCatGenActResult3UPC.SelectedValue
        Else
            para.GEN_ACT_RESULT3 = "N"
            para.CATEGORY_GEN_ACT_RESULT3 = ""
            para.SUBCATEGORY_GEN_ACT_RESULT3 = ""
            para.CAT_GEN_ACT_RESULT3_UPC = ""
            para.SUBCAT_GEN_ACT_RESULT3_UPC = ""
        End If
        para.OWNER_GEN_ACT_RESULT3 = txtOwnerGenActResult3.Text

        Return para
    End Function

    Private Sub SetSegmentList()
        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim dt As DataTable = eng.GetSegmentList
        If dt.Rows.Count > 0 Then
            gvSegment.DataSource = dt
            gvSegment.DataBind()
            dt = Nothing
        Else
            gvSegment.DataSource = Nothing
            gvSegment.DataBind()
        End If
        eng = Nothing
    End Sub


    Private Sub SetShopList(ByVal FilterID As Long)
        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim dt As DataTable = eng.GetShopList()
        If dt.Rows.Count > 0 Then
            gvShopList.DataSource = dt
            gvShopList.DataBind()
            dt = Nothing
        Else
            gvShopList.DataSource = Nothing
            gvShopList.DataBind()
        End If

        'Dim eng As New FilterTemplateENG
        Dim dtS As DataTable = eng.GetFilterShopList(FilterID)
        If dtS.Rows.Count > 0 Then
            For Each gv As GridViewRow In gvShopList.Rows
                dtS.DefaultView.RowFilter = "tb_shop_id = '" & gv.Cells(1).Text & "'"
                If dtS.DefaultView.Count > 0 Then
                    Dim chk As CheckBox = gv.Cells(0).FindControl("chk")
                    chk.Checked = True

                    Dim txt As UserControls_txtAutoComplete = CType(gv.Cells(4).FindControl("txtShTemplateCode"), UserControls_txtAutoComplete)
                    txt.Text = dtS.DefaultView(0)("sh_template_code")
                End If

            Next
            dtS = Nothing
        End If
        rdiSurveyAllShop_SelectedIndexChanged(Nothing, Nothing)
        eng = Nothing
    End Sub

    Private Function GetSelectedShop() As String
        Dim ret As String = ""

        If gvShopList.Rows.Count > 0 Then
            For Each gv As GridViewRow In gvShopList.Rows
                Dim chk As CheckBox = gv.Cells(0).FindControl("chk")
                If rdiSurveyAllShop.SelectedValue = "N" Then
                    If chk.Checked Then
                        If ret = "" Then
                            ret = gv.Cells(1).Text
                        Else
                            ret += "," & gv.Cells(1).Text
                        End If
                    End If
                Else
                    If ret = "" Then
                        ret = gv.Cells(1).Text
                    Else
                        ret += "," & gv.Cells(1).Text
                    End If

                End If
            Next
        End If

        Return ret
    End Function

    Private Function GetSelectedShopDT() As DataTable
        Dim ret As String = ""
        Dim dt As New DataTable
        dt.Columns.Add("ShopID")
        dt.Columns.Add("ShTemplateCode")
        Dim dr As DataRow

        If gvShopList.Rows.Count > 0 Then
            For Each gv As GridViewRow In gvShopList.Rows
                Dim chk As CheckBox = gv.Cells(0).FindControl("chk")
                If rdiSurveyAllShop.SelectedValue = "N" Then
                    If chk.Checked Then
                        dr = dt.NewRow
                        dr("ShopID") = gv.Cells(1).Text

                        Dim txt As UserControls_txtAutoComplete = CType(gv.Cells(4).FindControl("txtShTemplateCode"), UserControls_txtAutoComplete)
                        dr("ShTemplateCode") = txt.Text
                        dt.Rows.Add(dr)
                    End If
                Else
                    dr = dt.NewRow
                    dr("ShopID") = gv.Cells(1).Text

                    Dim txt As UserControls_txtAutoComplete = CType(gv.Cells(4).FindControl("txtShTemplateCode"), UserControls_txtAutoComplete)
                    dr("ShTemplateCode") = txt.Text
                    dt.Rows.Add(dr)
                End If
            Next
        End If

        Return dt
    End Function

    Private Function GetSelectedService() As String
        Dim ret As String = ""
        If gvService.Rows.Count > 0 Then
            For Each gv As GridViewRow In gvService.Rows
                Dim chk As CheckBox = gv.Cells(3).FindControl("chk")
                If chk.Checked Then
                    If ret = "" Then
                        ret = gv.Cells(1).Text
                    Else
                        ret += "," & gv.Cells(1).Text
                    End If
                End If
            Next
        End If

        Return ret
    End Function
#End Region


    Protected Sub rdiScheduleTypeDay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiScheduleTypeDay.SelectedIndexChanged
        If rdiScheduleTypeDay.SelectedValue = "0" Then
            chkScheduleDay.Enabled = False
            For Each chkSchDay As ListItem In chkScheduleDay.Items
                chkSchDay.Selected = False
            Next
        Else
            chkScheduleDay.Enabled = True
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Valid() = True Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim UserName As String = Engine.Common.LoginENG.GetLogOnUser.USERNAME
                Dim eng As New Engine.CSI.FilterTemplateENG
                Dim fPara As CenParaDB.TABLE.TbFilterCenParaDB = GetData()
                If eng.SaveFilterTemplate(UserName, fPara, trans) = True Then
                    eng.DeleteFilterService(trans)
                    eng.DeleteFilterStaff(trans)
                    eng.DeleteTempTarget(trans)

                    Dim ret As Boolean = True
                    For Each grv As GridViewRow In gvService.Rows
                        Dim tTxt As UserControls_txtAutoComplete = grv.Cells(2).FindControl("txtTargetPercent")
                        Dim chk As CheckBox = grv.Cells(3).FindControl("chk")

                        Dim para As New CenParaDB.TABLE.TbFilterServiceCenParaDB
                        para.TB_ITEM_ID = grv.Cells(1).Text
                        para.TB_FILTER_ID = eng.FilterTemplateID
                        para.CHK = IIf(chk.Checked = True, "Y", "N")
                        If tTxt.Text.Trim <> "" Then
                            para.TARGET_PERCENT = Convert.ToInt64(tTxt.Text)
                        End If

                        ret = eng.SaveFilterService(UserName, para, trans)
                        If ret = False Then
                            Exit For
                        End If
                    Next

                    If ret = True Then
                        Dim uDt As DataTable = Session(SessSelectUser)
                        If uDt IsNot Nothing Then
                            For Each dr As DataRow In uDt.Rows
                                Dim para As New CenParaDB.TABLE.TbFilterStaffCenParaDB
                                para.TB_FILTER_ID = eng.FilterTemplateID
                                para.SHOP_ID = dr("shop_id")
                                para.USERNAME = dr("username")
                                ret = eng.SaveFilterStaff(UserName, para, trans)
                                If ret = False Then
                                    Exit For
                                End If
                            Next
                        End If

                        If ret = True Then
                            Dim ShopSelect As New DataTable
                            ShopSelect = GetSelectedShopDT()
                            ret = eng.SaveSetFilterToShop(UserName, eng.FilterTemplateID, ShopSelect, trans)
                            If ret = True Then
                                trans.CommitTransaction()

                                FunctionEng.SaveTransLog("CSIWebApp_frmFilterTemplateForm.btnSave_Click", "บันทึกข้อมูล Filter Template")
                                txtID.Text = eng.FilterTemplateID
                                FillData(txtID.Text)
                                SetUserList(txtID.Text)
                                SetShopList(txtID.Text)
                                Config.SetAlert("บันทึกข้อมูลเรียบร้อย", Me)
                            Else
                                trans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("CSIWebApp_frmFilterTemplateForm.SaveSetFilterToShop", eng.ErrorMessage)
                                Config.SetAlert(eng.ErrorMessage, Me)
                            End If
                        Else
                            trans.RollbackTransaction()
                            FunctionEng.SaveErrorLog("CSIWebApp_frmFilterTemplateForm.SaveFilterStaff", eng.ErrorMessage)
                            Config.SetAlert(eng.ErrorMessage, Me)
                        End If

                        If ret = True Then
                            trans.CommitTransaction()
                        Else
                            trans.RollbackTransaction()
                        End If
                    Else
                        trans.RollbackTransaction()
                        FunctionEng.SaveErrorLog("CSIWebApp_frmFilterTemplateForm.SaveFilterService", eng.ErrorMessage)
                        Config.SetAlert(eng.ErrorMessage, Me)
                    End If
                    fPara = Nothing
                Else
                    trans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("CSIWebApp_frmFilterTemplateForm.SaveFilterTemplate", eng.ErrorMessage)
                    Config.SetAlert(eng.ErrorMessage, Me)
                End If
                eng = Nothing
            Else
                FunctionEng.SaveErrorLog("CSIWebApp_frmFilterTemplateForm.btnSave_Click", trans.ErrorMessage)
            End If
        End If
    End Sub

    Private Sub FillData(ByVal vID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim para As CenParaDB.TABLE.TbFilterCenParaDB = eng.GetFilterTemplatePara(vID, trans)
        trans.CommitTransaction()
        If para.ID <> 0 Then
            txtID.Text = para.ID
            txtFilterName.Text = para.FILTER_NAME
            cmbCategory.SelectedValue = para.CATEGORY
            cmbContactClass.SelectedValue = para.CONTACT_CLASS
            cmbNetworkType.SelectedValue = para.NETWORK_TYPE

            If para.NATIONALITY <> "" Then
                Dim value As String() = para.NATIONALITY.Split(",")
                For i As Integer = 0 To chkNationality.Items.Count - 1
                    Dim national As String = chkNationality.Items(i).Value
                    For Each a As String In value
                        If national = a Then
                            chkNationality.Items(i).Selected = True
                        End If
                    Next
                Next
            End If

            trans.CreateTransaction()
            Dim lnq As New CenLinqDB.TABLE.TbFilterServiceCenLinqDB
            Dim dv As DataView = lnq.GetDataList("tb_filter_id = '" & vID & "'", "", trans.Trans).DefaultView
            trans.CommitTransaction()
            For Each grv As GridViewRow In gvService.Rows
                Dim sChk As CheckBox = grv.Cells(3).FindControl("chk")
                dv.RowFilter = "tb_item_id = " & grv.Cells(1).Text
                If dv.Count > 0 Then
                    sChk.Checked = IIf(dv.Item(0)("chk").ToString = "Y", True, False)
                End If
            Next
            lnq = Nothing

            'cmbSegment.SelectedValue = para.SEGMENT
            'Segment
            If para.SEGMENT <> "0" Then
                Dim seg() As String = Split(para.SEGMENT, ",")
                If seg.Length > 0 Then
                    For Each s As String In seg
                        For Each grv As GridViewRow In gvSegment.Rows
                            If grv.Cells(1).Text.Trim = s Then
                                Dim chk As CheckBox = grv.FindControl("chk")
                                chk.Checked = True
                            End If
                        Next
                    Next
                End If
            End If



            txtPeriodDateFrom.DateValue = para.PREIOD_DATEFROM
            txtPeriodDateTo.DateValue = para.PREIOD_DATETO
            cmbTimeFrom.SelectedValue = para.PREIOD_TIMEFROM
            cmbTimeTo.SelectedValue = para.PREIOD_TIMETO
            rdiScheduleTypeDay.SelectedValue = para.SCHEDULETYPEDAY
            If rdiScheduleTypeDay.SelectedValue = "1" Then
                chkScheduleDay.Enabled = True
            Else
                chkScheduleDay.Enabled = False
            End If
            For Each chkSchDay As ListItem In chkScheduleDay.Items
                Select Case chkSchDay.Value
                    Case "MON"
                        chkSchDay.Selected = (para.SCHEDULEMONDAY = "Y")
                    Case "TUE"
                        chkSchDay.Selected = (para.SCHEDULETUEDAY = "Y")
                    Case "WED"
                        chkSchDay.Selected = (para.SCHEDULEWEDDAY = "Y")
                    Case "THU"
                        chkSchDay.Selected = (para.SCHEDULETHUDAY = "Y")
                    Case "FRI"
                        chkSchDay.Selected = (para.SCHEDULEFRIDAY = "Y")
                    Case "SAT"
                        chkSchDay.Selected = (para.SCHEDULESATDAY = "Y")
                    Case "SUN"
                        chkSchDay.Selected = (para.SCHEDULESUNDAY = "Y")
                End Select
            Next

            If para.TARGET_UNLIMITED = "Y" Then
                chkUnlimited.Checked = True
                txtTarget.Text = ""
                txtTarget.Enabled = False
            Else
                chkUnlimited.Checked = False
                txtTarget.Text = para.TARGET
                txtTarget.Enabled = True
            End If
            txtTemplateCode.Text = para.TEMPLATE_CODE

            txtAfterEndMin.Enabled = False
            txtEveryMin.Enabled = False
            If para.DURATION_TYPE = "0" Then
                rdiAfterEndQueue.Checked = True
                txtAfterEndMin.Text = para.DURATION_AFTER_MIN
                txtAfterEndMin.Enabled = True
            Else
                rdiEveryTime.Checked = True
                txtEveryMin.Text = para.DURATION_EVERY_MIN
                txtEveryMin.Enabled = True
            End If

            If para.ACTIVE_STATUS = "Y" Then
                rdiStatusActive.Checked = True
            Else
                rdiStatusHold.Checked = True
            End If

            chkISBlankCategory.Checked = IIf(para.BLANK_CATEGORY = "Y", True, False)
            chkISBlankContactClass.Checked = IIf(para.BLANK_CONTACT_CLASS = "Y", True, False)
            rdiSurveyAllShop.SelectedValue = para.SURVEY_ALL_SHOP
            rdiSurveyAllShop_SelectedIndexChanged(Nothing, Nothing)

            'Auto Generate Siebel Activity
            chkGenActAllSurvey.Checked = (para.GEN_ACT_ALL_SURVEY = "Y")
            If chkGenActAllSurvey.Checked = True Then
                CmbCatGenActAllSurvey.SelectedValue = para.CATEGORY_GEN_ACT_ALL_SURVEY
                CmbCatGenActAllSurvey_SelectedIndexChanged(Nothing, Nothing)
                CmbSubCatGenActAllSurvey.SelectedValue = para.SUBCATEGORY_GEN_ACT_ALL_SURVEY

                CmbCatGenActAllSurveyUPC.SelectedValue = para.CAT_GEN_ACT_ALL_SURVEY_UPC
                CmbCatGenActAllSurveyUPC_SelectedIndexChanged(Nothing, Nothing)
                CmbSubCatGenActAllSurveyUPC.SelectedValue = para.SUBCAT_GEN_ACT_ALL_SURVEY_UPC

                CmbCatGenActAllSurvey.Enabled = True
                CmbCatGenActAllSurveyUPC.Enabled = True
                CmbSubCatGenActAllSurvey.Enabled = True
                CmbSubCatGenActAllSurveyUPC.Enabled = True
            Else
                CmbCatGenActAllSurvey.Enabled = False
                CmbCatGenActAllSurveyUPC.Enabled = False
                CmbSubCatGenActAllSurvey.Enabled = False
                CmbSubCatGenActAllSurveyUPC.Enabled = False

                CmbCatGenActAllSurvey.SelectedValue = ""
                CmbCatGenActAllSurveyUPC.SelectedValue = ""
                CmbCatGenActAllSurvey_SelectedIndexChanged(Nothing, Nothing)
                CmbCatGenActAllSurveyUPC_SelectedIndexChanged(Nothing, Nothing)
            End If

            chkGenActResult3.Checked = (para.GEN_ACT_RESULT3 = "Y")
            If chkGenActResult3.Checked = True Then
                CmbCatGenActResult3.SelectedValue = para.CATEGORY_GEN_ACT_RESULT3
                CmbCatGenActResult3_SelectedIndexChanged(Nothing, Nothing)
                CmbSubCatGenActResult3.SelectedValue = para.SUBCATEGORY_GEN_ACT_RESULT3

                CmbCatGenActResult3UPC.SelectedValue = para.CAT_GEN_ACT_RESULT3_UPC
                CmbCatGenActResult3UPC_SelectedIndexChanged(Nothing, Nothing)
                CmbSubCatGenActResult3UPC.SelectedValue = para.SUBCAT_GEN_ACT_RESULT3_UPC

                CmbCatGenActResult3.Enabled = True
                CmbCatGenActResult3UPC.Enabled = True
                CmbSubCatGenActResult3.Enabled = True
                CmbSubCatGenActResult3UPC.Enabled = True
            Else
                CmbCatGenActResult3.Enabled = False
                CmbCatGenActResult3UPC.Enabled = False
                CmbSubCatGenActResult3.Enabled = False
                CmbSubCatGenActResult3UPC.Enabled = False

                CmbCatGenActResult3.SelectedValue = ""
                CmbCatGenActResult3UPC.SelectedValue = ""
                CmbCatGenActResult3_SelectedIndexChanged(Nothing, Nothing)
                CmbCatGenActResult3UPC_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If

    End Sub

    Protected Sub chkUnlimited_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUnlimited.CheckedChanged
        If chkUnlimited.Checked = True Then
            txtTarget.Enabled = False
            txtTarget.Text = ""
        Else
            txtTarget.Enabled = True
        End If
    End Sub

    Protected Sub chkHUser_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).Cells(0).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
    End Sub

    Protected Sub chkHService_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).Cells(0).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
    End Sub

    Protected Sub chkH_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).Cells(0).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
    End Sub

    Protected Sub chkSearchUserList_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).Cells(0).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
        zPop.Show()
    End Sub

    Protected Sub chkSegment_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkH As CheckBox = sender
        Dim grv As GridViewRow = chkH.Parent.Parent
        Dim gv As GridView = grv.Parent.Parent
        For i As Integer = 0 To gv.Rows.Count - 1
            Dim chk As CheckBox = gv.Rows(i).FindControl("chk")
            chk.Checked = chkH.Checked
        Next
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim eng As New Engine.Configuration.MasterENG
        Dim ShopList As String = GetSelectedShop()

        Dim Wh As String = " shop_id in (" & ShopList & ") "
        If cmbSearchShop.SelectedValue <> "0" Then
            Wh += " and shop_id = " & cmbSearchShop.SelectedValue
        End If
        If txtSearchStaffName.Text.Trim <> "" Then
            Wh += " and staff_name like '%" & txtSearchStaffName.Text.Trim & "%'"
        End If

        Dim tmpUser As String = ""
        'Dim tmpShopID As String = ""
        For Each gv As GridViewRow In gvUserName.Rows
            Dim chk As CheckBox = gv.Cells(0).FindControl("chk")
            If tmpUser.Trim = "" Then
                tmpUser = gv.Cells(6).Text & "|" & gv.Cells(3).Text
            Else
                tmpUser += "###" & gv.Cells(6).Text & "|" & gv.Cells(3).Text
            End If
        Next
        'If tmpUser.Trim <> "" Then
        '    Wh += " and username not in (" & tmpUser & ")"
        'End If

        Dim tmpItem As String = ""
        For Each gv As GridViewRow In gvService.Rows
            Dim chk As CheckBox = gv.FindControl("chk")
            If chk.Checked = True Then
                If tmpItem.Trim = "" Then
                    tmpItem = "'" & gv.Cells(1).Text & "'"
                Else
                    tmpItem += ",'" & gv.Cells(1).Text & "'"
                End If
            End If
        Next

        Dim dt As DataTable = eng.GetUserList(Wh, tmpItem, ShopList)
        If dt.Rows.Count > 0 Then
            If tmpUser.Trim <> "" Then
                For Each u As String In Split(tmpUser, "###")
                    For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                        If dt.Rows(i)("shop_id") = Split(u, "|")(1) And dt.Rows(i)("username") = Split(u, "|")(0) Then
                            dt.Rows.RemoveAt(i)
                        End If
                    Next
                Next
            End If


            dt.Columns.Add("no")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = i + 1
            Next
            gvSearchStaffList.DataSource = dt
            gvSearchStaffList.DataBind()
            btnSelect.Visible = True
        Else
            gvSearchStaffList.DataSource = Nothing
            gvSearchStaffList.DataBind()
            btnSelect.Visible = False
        End If
        zPop.Show()
    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim dt As DataTable = Session(SessSelectUser)
        If dt Is Nothing Then
            dt = New DataTable
            dt.Columns.Add("shop_id")
            dt.Columns.Add("shop_name")
            dt.Columns.Add("shop_abb")
            dt.Columns.Add("user_id")
            dt.Columns.Add("username")
            dt.Columns.Add("staff_name")
            dt.Columns.Add("fname")
            dt.Columns.Add("lname")
            dt.Columns.Add("UQID")
        End If

        Dim IsSelect As Boolean = False
        For Each grv As GridViewRow In gvSearchStaffList.Rows
            Dim chk As CheckBox = grv.Cells(0).FindControl("chk")
            If chk.Checked Then
                Dim dr As DataRow = dt.NewRow
                'gv.Cells(4).Text
                dr("shop_id") = grv.Cells(4).Text
                dr("shop_name") = grv.Cells(5).Text
                dr("shop_abb") = grv.Cells(2).Text
                dr("user_id") = grv.Cells(6).Text
                dr("username") = grv.Cells(7).Text
                dr("staff_name") = grv.Cells(3).Text
                dr("fname") = grv.Cells(8).Text
                dr("lname") = grv.Cells(9).Text
                dr("UQID") = grv.Cells(4).Text & grv.Cells(6).Text
                dt.Rows.Add(dr)
                IsSelect = True
            End If
        Next
        If IsSelect = False Then
            Config.SetAlert("กรุณาเลือก Agent ", Me)
            zPop.Show()
            Exit Sub
        End If

        gvSearchStaffList.DataSource = Nothing
        gvSearchStaffList.DataBind()

        If dt.Rows.Count > 0 Then
            Session(SessSelectUser) = dt
            gvUserName.DataSource = dt
            gvUserName.DataBind()
        Else
            gvUserName.DataSource = Nothing
            gvUserName.DataBind()
            Session(SessSelectUser) = Nothing
        End If
    End Sub

    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable = Session(SessSelectUser)
        Dim UQID As String = sender.CommandArgument

        Dim dv As DataView = dt.DefaultView
        dv.RowFilter = "UQID not in ('" & UQID & "')"
        dt = dv.ToTable

        If dt.Rows.Count > 0 Then
            Session(SessSelectUser) = dt
            gvUserName.DataSource = dt
            gvUserName.DataBind()
        Else
            gvUserName.DataSource = Nothing
            gvUserName.DataBind()
            Session.Remove(SessSelectUser)
        End If
    End Sub

    Protected Sub btnUSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUSearch.Click
        Dim dtSh As New DataTable
        dtSh.Columns.Add("id")
        dtSh.Columns.Add("shop_name_en")

        Dim shSel As String = GetSelectedShop()
        If shSel.Trim <> "" Then
            Dim svSel As String = GetSelectedService()
            If svSel.Trim <> "" Then
                For Each sh As String In Split(shSel, ",")
                    Dim eng As New Engine.Configuration.MasterENG
                    Dim shP As New CenParaDB.TABLE.TbShopCenParaDB
                    shP = eng.GetShopPara(Convert.ToInt64(sh))

                    Dim dr As DataRow = dtSh.NewRow
                    dr("id") = sh
                    dr("shop_name_en") = shP.SHOP_NAME_EN
                    dtSh.Rows.Add(dr)

                    eng = Nothing
                    shP = Nothing
                Next
                If dtSh.Rows.Count > 0 Then
                    cmbSearchShop.SetItemList(dtSh, "shop_name_en", "id")
                End If
                dtSh = Nothing

                txtSearchStaffName.Text = ""
                cmbSearchShop.SelectedValue = "0"
                gvSearchStaffList.DataSource = Nothing
                gvSearchStaffList.DataBind()
                zPop.Show()
            Else
                Config.SetAlert("Please Select Service", Me)
            End If
        Else
            Config.SetAlert("Please Select Location", Me)
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClosePopSearch()
    End Sub

    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        ClosePopSearch()
    End Sub

    Private Sub ClosePopSearch()
        cmbSearchShop.SelectedValue = "0"
        txtSearchStaffName.Text = ""
        gvSearchStaffList.DataSource = Nothing
        gvSearchStaffList.DataBind()
    End Sub

    Protected Sub rdiAfterEndQueue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiAfterEndQueue.CheckedChanged
        If rdiAfterEndQueue.Checked = True Then
            txtAfterEndMin.Enabled = True
            txtEveryMin.Enabled = False
            txtEveryMin.Text = ""
        End If
    End Sub

    Protected Sub rdiEveryTime_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiEveryTime.CheckedChanged
        If rdiEveryTime.Checked = True Then
            txtEveryMin.Enabled = True
            txtAfterEndMin.Enabled = False
            txtAfterEndMin.Text = ""
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If gvService.Rows.Count > 0 Then
            Dim tot As Double = 0
            For Each grv As GridViewRow In gvService.Rows
                Dim tTxt As UserControls_txtAutoComplete = grv.Cells(2).FindControl("txtTargetPercent")
                If tTxt.Text.Trim <> "" Then
                    tot += Convert.ToDouble(tTxt.Text)
                End If
            Next
            Dim totLbl As Label = gvService.FooterRow.FindControl("lblTotTargetPer")
            totLbl.Text = tot & " %"
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If txtID.Text <> "0" Then
            Dim vID As Long = txtID.Text
            FillData(vID)
            SetUserList(vID)
            SetShopList(vID)
            SetServiceList(vID)
        Else
            SetServiceList(0)
            SetShopList(0)
            SetCheckBokList()
            SetUserList(0)
            SetSegmentList()

            txtFilterName.Text = ""
            txtPeriodDateFrom.DateValue = New Date(1, 1, 1)
            txtPeriodDateTo.DateValue = New Date(1, 1, 1)
            SetPeriodTime()
            rdiScheduleTypeDay.SelectedValue = "0"
            chkScheduleDay.Enabled = False

            For Each chkSchDay As ListItem In chkScheduleDay.Items
                Select Case chkSchDay.Value
                    Case "MON"
                        chkSchDay.Selected = False
                    Case "TUE"
                        chkSchDay.Selected = False
                    Case "WED"
                        chkSchDay.Selected = False
                    Case "THU"
                        chkSchDay.Selected = False
                    Case "FRI"
                        chkSchDay.Selected = False
                    Case "SAT"
                        chkSchDay.Selected = False
                    Case "SUN"
                        chkSchDay.Selected = False
                End Select
            Next

            chkUnlimited.Checked = False
            txtTarget.Text = ""
            txtTarget.Enabled = True
            txtTemplateCode.Text = ""


            rdiAfterEndQueue.Checked = True
            txtAfterEndMin.Enabled = True
            txtAfterEndMin.Text = ""

            rdiEveryTime.Checked = False
            txtEveryMin.Enabled = False
            txtEveryMin.Text = ""

            'Auto Generate Siebel Activity
            CmbCatGenActAllSurvey_SelectedIndexChanged(sender, e)
            CmbCatGenActAllSurveyUPC_SelectedIndexChanged(sender, e)
            CmbCatGenActResult3_SelectedIndexChanged(sender, e)
            CmbCatGenActResult3UPC_SelectedIndexChanged(sender, e)
            chkGenActAllSurvey.Checked = False
            chkGenActResult3.Checked = False
            chkGenActAllSurvey_CheckedChanged(sender, e)
            chkGenActResult3_CheckedChanged(sender, e)

            rdiStatusActive.Checked = True

        End If
    End Sub

    Protected Sub rdiSurveyAllShop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiSurveyAllShop.SelectedIndexChanged
        If rdiSurveyAllShop.SelectedValue = "Y" Then
            For Each grv As GridViewRow In gvShopList.Rows
                Dim chk As CheckBox = grv.FindControl("chk")
                chk.Checked = False
                chk.Enabled = False
            Next
            Dim chkH As CheckBox = gvShopList.HeaderRow.FindControl("chkH")
            chkH.Checked = False
            chkH.Enabled = False
        Else
            For Each grv As GridViewRow In gvShopList.Rows
                Dim chk As CheckBox = grv.FindControl("chk")
                chk.Enabled = True
            Next
            Dim chkH As CheckBox = gvShopList.HeaderRow.FindControl("chkH")
            chkH.Enabled = True
        End If
    End Sub

    Protected Sub chkGenActAllSurvey_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGenActAllSurvey.CheckedChanged
        If chkGenActAllSurvey.Checked = True Then
            CmbCatGenActAllSurvey.Enabled = True
            CmbCatGenActAllSurveyUPC.Enabled = True
            CmbSubCatGenActAllSurvey.Enabled = True
            CmbSubCatGenActAllSurveyUPC.Enabled = True
        Else
            CmbCatGenActAllSurvey.Enabled = False
            CmbCatGenActAllSurveyUPC.Enabled = False
            CmbSubCatGenActAllSurvey.Enabled = False
            CmbSubCatGenActAllSurveyUPC.Enabled = False

            CmbCatGenActAllSurvey.SelectedValue = ""
            CmbCatGenActAllSurveyUPC.SelectedValue = ""
            CmbSubCatGenActAllSurvey.SelectedValue = ""
            CmbSubCatGenActAllSurveyUPC.SelectedValue = ""
        End If
    End Sub

    Protected Sub chkGenActResult3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGenActResult3.CheckedChanged
        If chkGenActResult3.Checked = True Then
            CmbCatGenActResult3.Enabled = True
            CmbCatGenActResult3UPC.Enabled = True
            CmbSubCatGenActResult3.Enabled = True
            CmbSubCatGenActResult3UPC.Enabled = True
        Else
            CmbCatGenActResult3.Enabled = False
            CmbCatGenActResult3UPC.Enabled = False
            CmbSubCatGenActResult3.Enabled = False
            CmbSubCatGenActResult3UPC.Enabled = False

            CmbCatGenActResult3.SelectedValue = ""
            CmbCatGenActResult3UPC.SelectedValue = ""
            CmbSubCatGenActResult3.SelectedValue = ""
            CmbSubCatGenActResult3UPC.SelectedValue = ""
        End If
    End Sub

    Protected Sub CmbCatGenActAllSurvey_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCatGenActAllSurvey.SelectedIndexChanged
        SetSubCatGenActAllSurvey(CmbCatGenActAllSurvey.SelectedValue)
    End Sub

    Protected Sub CmbCatGenActAllSurveyUPC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCatGenActAllSurveyUPC.SelectedIndexChanged
        SetSubCatGenActAllSurveyUPC(CmbCatGenActAllSurveyUPC.SelectedValue)
    End Sub

    Protected Sub CmbCatGenActResult3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCatGenActResult3.SelectedIndexChanged
        SetSubCatGenActResult3(CmbCatGenActResult3.SelectedValue)
    End Sub

    Protected Sub CmbCatGenActResult3UPC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCatGenActResult3UPC.SelectedIndexChanged
        SetSubCatGenActResult3UPC(CmbCatGenActResult3UPC.SelectedValue)
    End Sub

    Protected Sub txtTemplateCode_TextChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemplateCode.TextChange
        For Each gv As GridViewRow In gvShopList.Rows
            Dim txt As UserControls_txtAutoComplete = CType(gv.Cells(4).FindControl("txtShTemplateCode"), UserControls_txtAutoComplete)
            If txt.Text = "" Then
                txt.Text = txtTemplateCode.Text
            End If
        Next
    End Sub
End Class
