Imports System.Data
Imports Engine.Common

Partial Class TWCSIWebApp_twLocation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        If IsPostBack = False Then
            ClearControl()
            SetSegment()
            SetRegion()
            SetProvince("")
            SetLocationType()
            SetLocationList(" 1=1 ")
            btnUpload_Click(sender, e)
            txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
            txtLocation_Code.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSave.ClientID + "') ")
            txtLocation_Name.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSave.ClientID + "') ")
        End If
    End Sub

    Sub ClearControl()
        txtLocation_Code.Text = ""
        txtLocation_Name.Text = ""
        ddlSegment.SelectedIndex = 0
        ddlProvince.SelectedIndex = 0
        ddlRegion.SelectedIndex = 0
        ddlType.SelectedIndex = 0
        txtID.Text = 0
        txtLocation_Code.Visible = True
        lblDisplayLocatioCode.Visible = False
        lblNotFound.Visible = False
    End Sub

    Private Sub SetRegion()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWRegionAllList()

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        ddlRegion.DataTextField = "name"
        ddlRegion.DataValueField = "code"
        ddlRegion.DataSource = dt
        ddlRegion.DataBind()
        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetProvince(ByVal RegionCode As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWProvinceList(RegionCode)

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        ddlProvince.DataTextField = "name"
        ddlProvince.DataValueField = "code"
        ddlProvince.DataSource = dt
        ddlProvince.DataBind()
        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetLocationType()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWLocationTypeList

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        ddlType.DataTextField = "name"
        ddlType.DataValueField = "code"
        ddlType.DataSource = dt
        ddlType.DataBind()
        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetSegment()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWSegmentList
        If dt.Rows.Count = 0 Then
            dt.Columns.Add("code")
            dt.Columns.Add("name")
        End If

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "Please Select"
        dt.Rows.InsertAt(dr, 0)

        ddlSegment.DataTextField = "name"
        ddlSegment.DataValueField = "code"
        ddlSegment.DataSource = dt
        ddlSegment.DataBind()
        dt.Dispose()

        eng = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If VerifyData() Then
            Dim para As New CenParaDB.TABLE.TwLocationCenParaDB
            With para
                .ID = txtID.Text
                .LOCATION_CODE = Trim(txtLocation_Code.Text)
                .LOCATION_NAME_TH = Trim(txtLocation_Name.Text)
                .LOCATION_SEGMENT = ddlSegment.SelectedValue
                .PROVINCE_CODE = ddlProvince.SelectedValue
                .REGION_CODE = ddlRegion.SelectedValue
                .LOCATION_TYPE = ddlType.SelectedValue
                .ACTIVE = IIf(chkIsActive.Checked, "Y", "N")
            End With
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim UserName As String = Engine.Common.LoginENG.GetLogOnUser.USERNAME
            Dim eng As New Engine.CSI.CSIMasterENG

            Dim err As String = ""
            If eng.SaveTWLocation(UserName, para, trans) Then
                trans.CommitTransaction()
                SetLocationList(" 1=1 ")
                err = "บันทึกข้อมูลเรียบร้อย"
            Else
                trans.RollbackTransaction()
                err = eng.ErrorMessage
            End If
            FunctionEng.SaveErrorLog("TWCSIWebApp_twLocation.aspx.btnSave_Click", trans.ErrorMessage)
            ClearControl()
            Config.SetAlert(err, Me)
        End If
    End Sub

    Function VerifyData() As Boolean
        Dim ret As Boolean = True
        If Trim(txtLocation_Code.Text) = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Location Code", Me, txtLocation_Code.ClientID)
        End If
        If Trim(txtLocation_Name.Text) = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Location Name", Me, txtLocation_Name.ClientID)
        End If
        If ddlRegion.SelectedValue = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Region Code", Me, ddlRegion.ClientID)
        End If
        If ddlProvince.SelectedValue = "" Then
            ret = False
            Config.SetAlert("กรุณาระบุ Province Code", Me, ddlProvince.ClientID)
        End If
        

        Dim eng As New Engine.CSI.CSIMasterENG
        If eng.CheckDuplicateLocation(txtLocation_Code.Text, txtID.Text) = True Then
            ret = False
            Config.SetAlert("Location ซ้ำ", Me, txtLocation_Code.ClientID)
        End If
        eng = Nothing
        Return ret
    End Function

    Sub FillData(ByVal vID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim eng As New Engine.CSI.CSIMasterENG
        Dim para As CenParaDB.TABLE.TwLocationCenParaDB = eng.GetTWLocationPara(vID, trans)
        trans.CommitTransaction()
        If para.ID <> 0 Then
            txtID.Text = para.ID
            txtLocation_Code.Text = para.LOCATION_CODE
            lblDisplayLocatioCode.Text = para.LOCATION_CODE
            txtLocation_Name.Text = para.LOCATION_NAME_TH
            ddlSegment.SelectedValue = para.LOCATION_SEGMENT.ToUpper
            ddlRegion.SelectedValue = para.REGION_CODE
            SetProvince(ddlRegion.SelectedValue.Trim)
            ddlProvince.SelectedValue = para.PROVINCE_CODE
            ddlType.SelectedValue = para.LOCATION_TYPE
            chkIsActive.Checked = IIf(para.ACTIVE = "Y", True, False)
        End If
    End Sub

    Sub SetLocationList(ByVal wh As String)
        Dim eng As New Engine.CSI.CSIMasterENG
        Dim dt As New DataTable
        dt = eng.GetTWLocationListAll(wh)
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("no")
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("no") = i + 1
            Next

            PageControl1.SetMainGridView(gvLocationList)
            gvLocationList.PageSize = 20
            gvLocationList.DataSource = dt
            gvLocationList.DataBind()
            PageControl1.Update(dt.Rows.Count)
            Session("SearchFilterLocationList") = dt
            dt.Dispose()
            lblNotFound.Visible = False
        Else
            gvLocationList.DataSource = Nothing
            gvLocationList.DataBind()
            PageControl1.Visible = False
            Session("SearchFilterLocationList") = Nothing
            lblNotFound.Visible = True
        End If

    End Sub

    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        FillData(grv.Cells(7).Text)
        txtLocation_Code.Visible = False
        lblDisplayLocatioCode.Visible = True
        btnAddLocation_Click(sender, e)
    End Sub

    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim img As ImageButton = sender
        Dim grv As GridViewRow = img.Parent.Parent
        Dim eng As New Engine.CSI.CSIMasterENG
        If eng.DeleteTWLocation(grv.Cells(7).Text) = True Then
            SetLocationList(" 1=1 ")
        End If
        ClearControl()
        eng = Nothing
    End Sub

    Protected Sub PageControl1_PageChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles PageControl1.PageChange
        Dim dt As New DataTable
        dt = Session("SearchFilterLocationList")
        If dt.Rows.Count > 0 Then
            gvLocationList.PageIndex = PageControl1.SelectPageIndex
            PageControl1.SetMainGridView(gvLocationList)
            gvLocationList.DataSource = dt
            gvLocationList.DataBind()
            PageControl1.Update(dt.Rows.Count)
        End If
    End Sub

    Protected Sub gvLocationList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLocationList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim imgdel As New ImageButton
            Dim gvrow As GridViewRow = e.Row
            imgdel = gvrow.FindControl("imgDelete")

            Dim id As String = gvrow.Cells(7).Text
            If IsRefLocation(id) Then
                imgdel.Visible = False
            End If

        End If
    End Sub

    Protected Sub gvLocationList_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvLocationList.Sorting
        Dim dt As New DataTable
        dt = Session("SearchFilterList")
        If dt.Rows.Count > 0 Then
            gvLocationList.PageIndex = PageControl1.SelectPageIndex
            Config.GridViewSorting(gvLocationList, dt, txtSortDir, txtSortField, e, PageControl1.SelectPageIndex)
            PageControl1.SetMainGridView(gvLocationList)
            gvLocationList.DataSource = dt
            gvLocationList.DataBind()
            PageControl1.Update(dt.Rows.Count)
        End If
        dt = Nothing
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearControl()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim filter As String = "1=1"
        If Trim(txtSearch.Text) <> "" Then
            filter = " (replace(location_code,'_','9x9x9')  like replace('%" & Trim(txtSearch.Text) & "%','_','9x9x9') or replace(location_name_th,'_','9x9x9') like replace('%" & Trim(txtSearch.Text) & "%','_','9x9x9'))"
        End If
        SetLocationList(filter)
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        If ddlRegion.SelectedValue.Trim <> "" Then
            SetProvince(ddlRegion.SelectedValue.Trim)
        Else
            SetProvince("")
        End If
    End Sub

    Protected Sub ctlLocationList_Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlLocationList.Upload_Click
        If ctlLocationList.HasFile = False Then
            Config.SetAlert("Please select File", Me)
            Exit Sub
        End If


        Dim tmpFile As New CenParaDB.Common.TmpFileUploadPara
        tmpFile = ctlLocationList.TmpFileUploadPara

        Dim eng As New Engine.CSI.CSIMasterENG
        Dim uPara As New CenParaDB.Common.UserProfilePara
        uPara = Config.GetLogOnUser()

        If eng.SaveTWLocationFromFile(uPara.USERNAME, tmpFile) = True Then
            ctlLocationList.ClearFile()
            Config.SetAlert("บันทึกข้อมูลเรียบร้อย", Me)
            SetLocationList(" 1=1 ")
        Else
            Config.SetAlert(eng.ErrorMessage, Me)
        End If

        eng = Nothing

    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        btnUpload.BackColor = Drawing.Color.Gray
        btnAddLocation.BackColor = Drawing.Color.Empty
        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub btnAddLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLocation.Click
        btnUpload.BackColor = Drawing.Color.Empty
        btnAddLocation.BackColor = Drawing.Color.Gray
        MultiView1.ActiveViewIndex = 1
    End Sub

    Function IsRefLocation(ByVal locationID As String) As Boolean
       
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnqFb As New CenLinqDB.TABLE.TwFilterBranchCenLinqDB
        Dim dt As New DataTable
        dt = lnqFb.GetDataList("tw_location_id='" & locationID & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            Return True
        End If
        Dim lnqFd As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
        dt = New DataTable
        dt = lnqFd.GetDataList("tw_location_id='" & locationID & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            Return True
        End If

        Dim lnqTg As New CenLinqDB.TABLE.TwFilterTempTargetCenLinqDB
        dt = New DataTable
        dt = lnqTg.GetDataList("tw_location_id='" & locationID & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            Return True
        End If

        trans.CommitTransaction()
        lnqFb = Nothing
        lnqFd = Nothing
        lnqTg = Nothing
        Return False
    End Function

End Class
