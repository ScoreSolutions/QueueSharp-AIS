Imports System.Data
Imports Engine.Common
Partial Class TWCSIWebApp_twFilterTemplateForm
    Inherits System.Web.UI.Page

    Const SessSelectUser As String = "SessSelectUser"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Session.Remove("ShopFromMaster")
            btnAddLocation.Attributes.Add("onclick", "showModal('twSelectShop.aspx',770,620,0)")
            txtFilterName.Attributes.Add("onKeypress", "return clickButton(event,'" & btnSave.ClientID & "') ")

            Session.Remove(SessSelectUser)
            SetServiceList(0)
            SetCheckBokList()
            SetSegmentList()
            SetPeriodTime()
            chkAllSFF_CheckedChanged(sender, e)
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
                SetServiceList(vID)

                If Request("cmd") = "copy" Then
                    txtID.Text = "0"
                    txtFilterName.Text = txtFilterName.Text & "_Duplicate"
                End If
            End If
        End If

        If Session("SelectShopList") Then
            AddShop(Session("ShopList"))
            Session.Remove("ShopList")
            Session.Remove("SelectShopList")
        End If

    End Sub

#Region "Sub&Function"
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

    Protected Function CheckNull(ByVal objGrid As Object) As String
        If Object.ReferenceEquals(objGrid, DBNull.Value) Then
            Return "false"
        Else
            Return "true"
        End If
    End Function

    Private Sub SetServiceList(ByVal FilterID As Long)
        Dim engS As New Engine.CSI.FilterTemplateENG
        Dim dtS As DataTable = engS.GetTWFilterServiceList(txtID.Text)
        If dtS.Rows.Count > 0 Then
            gvService.DataSource = dtS
            gvService.DataBind()
            dtS = Nothing

            Dim lblTotTargetPer As Label = gvService.FooterRow.FindControl("lblTotTargetPer")
            For Each grv As GridViewRow In gvService.Rows
                Dim txtTargetPercent As UserControls_txtAutoComplete = grv.FindControl("txtTargetPercent")
                txtTargetPercent.Attributes.Add("onBlur", "CalTotTarget('" & lblTotTargetPer.ClientID & "')")
            Next
            txtPaymentPer.Attributes.Add("onBlur", "CalTotTarget('" & lblTotTargetPer.ClientID & "')")
            txtAllSFFPer.Attributes.Add("onBlur", "CalTotTarget('" & lblTotTargetPer.ClientID & "')")
        Else
            gvService.DataSource = Nothing
            gvService.DataBind()
        End If
    End Sub

    Private Sub SetCheckBokList()
        Dim eng As New Engine.Configuration.MasterENG

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

    Private Sub SetCategory()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetCategory("TWZ")

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
        dt = eng.GetSubCategory(category_name, "TWZ")

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
        dt = eng.GetSubCategory(category_name, "TWZ")

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
        dt = eng.GetSubCategory(category_name, "TWZ")

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
        dt = eng.GetSubCategory(category_name, "TWZ")

        Dim dr As DataRow = dt.NewRow
        dr("id") = ""
        dr("subcategory_name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        CmbSubCatGenActResult3UPC.SetItemList(dt, "subcategory_name", "id")

        dt.Dispose()

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

        ElseIf GetNationalityValue() = "" Then
            ret = False
            Config.SetAlert("กรุณาเลือก Nationality", Me)
        ElseIf CalPercent() = False And chkAllSFF.Checked = False Then
            ret = False
            Config.SetAlert("% Target ของทุกบริการรวมกัน ต้องเท่ากับ 100 %", Me)
        ElseIf chkAllSFF.Checked = True And txtAllSFFPer.Text = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ All SFF Order Type Percent", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtPeriodDateFrom.DateValue.Year = 1 Then
            ret = False
            Config.SetAlert("กรุณาระบุ Complete Date From", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtPeriodDateTo.DateValue.Year = 1 Then
            ret = False
            Config.SetAlert("กรุณาระบุ Complete Date To", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtPeriodDateFrom.DateValue.ToString("yyyyMMdd") > txtPeriodDateTo.DateValue.ToString("yyyyMMdd") Then
            ret = False
            Config.SetAlert("กรุณาระบุ Complete Date From ให้น้อยกว่า Complete Date To", Me, txtPeriodDateFrom.ClientID)
        ElseIf cmbTimeFrom.SelectedValue >= cmbTimeTo.SelectedValue Then
            ret = False
            Config.SetAlert("กรุณาระบุ Complete Time From ให้น้อยกว่า Complete Time To", Me, txtPeriodDateFrom.ClientID)
        ElseIf txtTemplateCode.Text.Trim = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Template Code", Me, txtTemplateCode.ClientID)
        ElseIf chkUnlimited.Checked = False And (txtTarget.Text.Trim = "" Or txtTarget.Text.Trim = "0") Then
            ret = False
            Config.SetAlert("กรุณาระบุ Target", Me, txtTarget.ClientID)
        ElseIf chkAllSFF.Checked = True Then
            Dim paymentPer As Integer = 0
            Dim SFFPer As Integer = 0
            If txtPaymentPer.Text <> "" Then
                paymentPer = txtPaymentPer.Text
            End If
            If txtAllSFFPer.Text <> "" Then
                SFFPer = txtAllSFFPer.Text
            End If
            If paymentPer + SFFPer <> 100 Then
                ret = False
                Config.SetAlert("Target ของทุกบริการรวมกัน ต้องเท่ากับ 100 %", Me, txtPaymentPer.ClientID)
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
            If eng.CheckDuplicateTwFilter(txtFilterName.Text, txtID.Text) = True Then
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
        p += CInt(IIf(txtPaymentPer.Text = "", 0, txtPaymentPer.Text))
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

    Private Function GetData() As CenParaDB.TABLE.TwFilterCenParaDB
        Dim para As New CenParaDB.TABLE.TwFilterCenParaDB
        para.ID = txtID.Text
        para.FILTER_NAME = txtFilterName.Text.Trim
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
        If rdiStatusActive.Checked = True Then
            para.ACTIVE_STATUS = "Y"
        ElseIf rdiStatusHold.Checked = True Then
            para.ACTIVE_STATUS = "N"
        End If
        para.NETWORK_TYPE = cmbNetworkType.SelectedValue

        If txtPaymentPer.Text.Trim = "" Then
            para.ORDER_PAYMENT_PER = 0
        Else
            para.ORDER_PAYMENT_PER = Convert.ToInt16(txtPaymentPer.Text)
        End If

        para.ORDER_SFF_PER = 0
        para.CHK_ORDER_SFF = "N"
        If chkAllSFF.Checked Then
            para.CHK_ORDER_SFF = "Y"
            para.ORDER_SFF_PER = txtAllSFFPer.Text
        End If

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

    Private Function GetSelectedShop() As String
        Dim ret As String = ""
        If GvLocation.Rows.Count > 0 Then
            For Each gv As GridViewRow In GvLocation.Rows
                Dim img As New ImageButton
                img = CType(gv.FindControl("imgDelLocation"), ImageButton)
                Dim id As String = img.CommandArgument
                If ret = "" Then
                    ret = id
                Else
                    ret += "," & id
                End If
            Next
        End If
        Return ret
    End Function

    Sub AddShop(ByVal NewData As DataTable)
        Dim dt As New DataTable
        With dt
            .Columns.Add("id")
            .Columns.Add("Location_code")
            .Columns.Add("Location_name_en")
            .Columns.Add("region_code")
            .Columns.Add("province_code")
            .Columns.Add("location_type")
            .Columns.Add("location_segment")
        End With

        Dim dr As DataRow
        For i As Integer = 0 To GvLocation.Rows.Count - 1
            Dim img As New ImageButton
            img = CType(GvLocation.Rows(i).FindControl("imgDelLocation"), ImageButton)
            Dim id As String = img.CommandArgument
            Dim location_code As String = GvLocation.Rows(i).Cells(2).Text
            Dim location_name As String = GvLocation.Rows(i).Cells(5).Text
            dr = dt.NewRow
            dr("id") = id
            dr("Location_code") = location_code
            dr("Location_name_en") = location_name
            dr("region_code") = GvLocation.Rows(i).Cells(4).Text
            dr("province_code") = GvLocation.Rows(i).Cells(3).Text
            dr("location_type") = GvLocation.Rows(i).Cells(6).Text
            dr("location_segment") = GvLocation.Rows(i).Cells(7).Text
            dt.Rows.Add(dr)
        Next

        For i As Integer = 0 To NewData.Rows.Count - 1
            dr = dt.NewRow
            dr("id") = NewData.Rows(i).Item("id") & ""
            dr("Location_code") = NewData.Rows(i).Item("Location_code") & ""
            dr("Location_name_en") = NewData.Rows(i).Item("Location_name_en") & ""
            dr("region_code") = NewData.Rows(i).Item("region_code") & ""
            dr("province_code") = NewData.Rows(i).Item("province_code") & ""
            dr("location_type") = NewData.Rows(i).Item("location_type") & ""
            dr("location_segment") = NewData.Rows(i).Item("location_segment") & ""
            dt.Rows.Add(dr)
        Next

        dt.Columns.Add("no")
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("no") = i + 1
        Next

        GvLocation.DataSource = dt
        GvLocation.DataBind()

        Session("ShopFromMaster") = dt
    End Sub


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
                Dim fPara As CenParaDB.TABLE.TwFilterCenParaDB = GetData()
                If eng.SaveTWFilterTemplate(UserName, fPara, trans) = True Then
                    eng.DeleteTWFilterService(trans)
                    eng.DeleteTWTempTarget(trans)

                    'Order Type
                    Dim ret As Boolean = True
                    For Each grv As GridViewRow In gvService.Rows
                        Dim tTxt As UserControls_txtAutoComplete = grv.Cells(2).FindControl("txtTargetPercent")
                        Dim para As New CenParaDB.TABLE.TwFilterOrderTypeCenParaDB
                        para.TW_SFF_ORDER_TYPE_ID = grv.Cells(1).Text
                        para.TW_FILTER_ID = eng.FilterTemplateID
                        If chkAllSFF.Checked = False Then
                            If tTxt.Text.Trim <> "" Then
                                para.TARGET_PERCENT = Convert.ToInt64(tTxt.Text)
                            End If
                        Else
                            para.TARGET_PERCENT = 0
                        End If

                        ret = eng.SaveTWFilterService(UserName, para, trans)
                        para = Nothing
                        If ret = False Then
                            Exit For
                        End If
                    Next
                    'Order Type Payment
                    Dim ppa As New CenParaDB.TABLE.TwFilterOrderTypeCenParaDB
                    ppa.TW_SFF_ORDER_TYPE_ID = 1   'Payment
                    ppa.TW_FILTER_ID = eng.FilterTemplateID
                    ppa.TARGET_PERCENT = fPara.ORDER_PAYMENT_PER
                    ret = eng.SaveTWFilterService(UserName, ppa, trans)
                    ppa = Nothing

                    If ret = True Then
                        'If ret = True Then
                        Dim ShopSelect As String = GetSelectedShop()
                        ret = eng.SaveSetTWFilterToShop(UserName, eng.FilterTemplateID, ShopSelect, trans)
                        If ret = True Then
                            trans.CommitTransaction()

                            FunctionEng.SaveTransLog("TWCSIWebApp_twFilterTemplateForm.btnSave_Click", "บันทึกข้อมูล Filter Template")
                            txtID.Text = eng.FilterTemplateID
                            FillData(txtID.Text)
                            Config.SetAlert("บันทึกข้อมูลเรียบร้อย", Me)
                        Else
                            trans.RollbackTransaction()
                            FunctionEng.SaveErrorLog("TWCSIWebApp_twFilterTemplateForm.SaveSetFilterToShop", eng.ErrorMessage)
                            Config.SetAlert(eng.ErrorMessage, Me)
                        End If
                        'Else
                        '    trans.RollbackTransaction()
                        '    FunctionEng.SaveErrorLog("TWCSIWebApp_twFilterTemplateForm.SaveFilterStaff", eng.ErrorMessage)
                        '    Config.SetAlert(eng.ErrorMessage, Me)
                        'End If

                        If ret = True Then
                            trans.CommitTransaction()
                        Else
                            trans.RollbackTransaction()
                        End If
                    Else
                        trans.RollbackTransaction()
                        FunctionEng.SaveErrorLog("TWCSIWebApp_twFilterTemplateForm.SaveFilterService", eng.ErrorMessage)
                        Config.SetAlert(eng.ErrorMessage, Me)
                    End If
                    fPara = Nothing
                Else
                    trans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("TWCSIWebApp_twFilterTemplateForm.SaveFilterTemplate", eng.ErrorMessage)
                    Config.SetAlert(eng.ErrorMessage, Me)
                End If
                eng = Nothing
            Else
                FunctionEng.SaveErrorLog("TWCSIWebApp_twFilterTemplateForm.btnSave_Click", trans.ErrorMessage)
            End If
        End If
    End Sub

    Private Sub FillData(ByVal vID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim eng As New Engine.CSI.FilterTemplateENG
        Dim para As CenParaDB.TABLE.TwFilterCenParaDB = eng.GetTWFilterTemplatePara(vID, trans)
        trans.CommitTransaction()
        If para.ID <> 0 Then
            txtID.Text = para.ID
            txtFilterName.Text = para.FILTER_NAME
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
            
            lnq = Nothing

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

            If para.ACTIVE_STATUS = "Y" Then
                rdiStatusActive.Checked = True
            Else
                rdiStatusHold.Checked = True
            End If

            txtPaymentPer.Text = para.ORDER_PAYMENT_PER
            txtAllSFFPer.Text = para.ORDER_SFF_PER
            chkAllSFF.Checked = IIf(para.CHK_ORDER_SFF = "N", False, True)
            If chkAllSFF.Checked = True Then
                txtAllSFFPer.Enabled = True
                gvService.Enabled = False
            Else
                txtAllSFFPer.Enabled = False
                gvService.Enabled = True
                txtAllSFFPer.Text = ""
            End If


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


            Dim dt As New DataTable
            Dim engSh As New Engine.Configuration.MasterENG
            dt = engSh.GetTWShopList(" and id in (select distinct tw_location_id from TW_FILTER_BRANCH where tw_filter_id='" & para.ID & "') ", "")
            Session("ShopFromMaster") = dt

            dt.Columns.Add("no")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = i + 1
            Next

            If dt.Rows.Count > 0 Then
                GvLocation.DataSource = dt
                GvLocation.DataBind()
            Else
                GvLocation.DataSource = Nothing
                GvLocation.DataBind()
            End If
            dt.Dispose()
            engSh = Nothing
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
        'zPop.Show()
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

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If gvService.Rows.Count > 0 Then
            Dim tot As Double = 0
            For Each grv As GridViewRow In gvService.Rows
                Dim tTxt As UserControls_txtAutoComplete = grv.Cells(2).FindControl("txtTargetPercent")
                If tTxt.Text.Trim <> "" Then
                    tot += Convert.ToDouble(tTxt.Text)
                End If
            Next
            Dim paymentper As Integer = 0
            If txtPaymentPer.Text <> "" Then
                paymentper = CInt(txtPaymentPer.Text)
            End If
            tot += paymentper

            Dim sffPer As Integer = 0
            If txtAllSFFPer.Text <> "" Then
                sffPer = CInt(txtAllSFFPer.Text)
            End If
            tot += sffPer

            Dim totLbl As Label = gvService.FooterRow.FindControl("lblTotTargetPer")
            totLbl.Text = tot & " %"
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If txtID.Text <> "0" Then
            Dim vID As Long = txtID.Text
            FillData(vID)
            SetServiceList(vID)
        Else
            SetServiceList(0)
            SetCheckBokList()
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

    Protected Sub imgDeleteLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim _id As String = sender.CommandArgument

        Dim dt As New DataTable
        With dt
            .Columns.Add("id")
            .Columns.Add("Location_code")
            .Columns.Add("Location_name_en")
            .Columns.Add("region_code")
            .Columns.Add("province_code")
            .Columns.Add("location_type")
            .Columns.Add("location_segment")
        End With

        Dim dr As DataRow
        For i As Integer = 0 To GvLocation.Rows.Count - 1
            Dim img As New ImageButton
            img = CType(GvLocation.Rows(i).FindControl("imgDelLocation"), ImageButton)
            Dim id As String = img.CommandArgument
            Dim location_code As String = GvLocation.Rows(i).Cells(2).Text
            Dim location_name As String = GvLocation.Rows(i).Cells(5).Text
            If _id <> id Then
                dr = dt.NewRow
                dr("id") = id
                dr("Location_code") = location_code
                dr("Location_name_en") = location_name
                dr("region_code") = GvLocation.Rows(i).Cells(4).Text
                dr("province_code") = GvLocation.Rows(i).Cells(3).Text
                dr("location_type") = GvLocation.Rows(i).Cells(6).Text
                dr("location_segment") = GvLocation.Rows(i).Cells(7).Text
                dt.Rows.Add(dr)
            End If
        Next

        dt.Columns.Add("no")
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("no") = i + 1
        Next

        GvLocation.DataSource = dt
        GvLocation.DataBind()

        Session("ShopFromMaster") = dt
    End Sub

    Protected Sub chkAllSFF_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllSFF.CheckedChanged
        If chkAllSFF.Checked Then
            txtAllSFFPer.Enabled = True
            gvService.Enabled = False
            For Each grv As GridViewRow In gvService.Rows
                Dim txt As UserControls_txtAutoComplete = grv.FindControl("txtTargetPercent")
                txt.Text = ""
            Next
        Else
            txtAllSFFPer.Text = ""
            txtAllSFFPer.Enabled = True = False
            gvService.Enabled = True
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
End Class
