Imports System.Data
Partial Class TWCSIWebApp_twSelectShop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetRegion()
            SetProvince("")
            SetSegment()
            SetShop(" and region_code='" & ddlRegion.SelectedValue & "'")
            lblerror.Visible = False
        End If
    End Sub

    Private Sub SetRegion()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWRegionAllList()

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "-------All-------"
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
        If dt.Rows.Count <> 1 Then
            Dim dr As DataRow = dt.NewRow
            dr("code") = ""
            dr("name") = "-------All-------"
            dt.Rows.InsertAt(dr, 0)
        End If
        
        ddlProvince.DataTextField = "name"
        ddlProvince.DataValueField = "code"
        ddlProvince.DataSource = dt
        ddlProvince.DataBind()
        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetSegment()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWLocationTypeList

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "-------All-------"
        dt.Rows.InsertAt(dr, 0)

        ddlType.DataTextField = "name"
        ddlType.DataValueField = "code"
        ddlType.DataSource = dt
        ddlType.DataBind()
        dt.Dispose()

        eng = Nothing
    End Sub

    Private Sub SetShop(ByVal wh As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWShopList(wh, txtLocationName.Text.Trim)

        If Not Session("ShopFromMaster") Is Nothing Then
            Dim dtSh As New DataTable
            dtSh = CType(Session("ShopFromMaster"), DataTable)
            If Not dtSh Is Nothing And dtSh.Rows.Count > 0 Then
                For i As Integer = 0 To dtSh.Rows.Count - 1
                    Dim tempDR() As DataRow = dt.Select(" ID ='" & Replace(dtSh.Rows(i).Item("ID").ToString, "&nbsp;", "") & "'")
                    If tempDR.Length > 0 Then
                        dt.Rows.Remove(tempDR(0))
                    End If
                Next
            End If
        End If

        dt.Columns.Add("no")
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("no") = i + 1
        Next

        If dt.Rows.Count > 0 Then
            gvShop.DataSource = dt
            gvShop.DataBind()
            lblerror.Visible = False
        Else
            gvShop.DataSource = Nothing
            gvShop.DataBind()
            lblerror.Visible = True
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        If ddlRegion.SelectedValue.Trim <> "" Then
            SetProvince(ddlRegion.SelectedValue.Trim)
        Else
            SetProvince("")
        End If
    End Sub

    Private Function GetSelectedShop(ByVal gv As GridView) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("Location_code")
        dt.Columns.Add("Location_name_en")
        dt.Columns.Add("region_code")
        dt.Columns.Add("province_code")
        dt.Columns.Add("location_type")
        dt.Columns.Add("location_Segment")

        For Each grv As GridViewRow In gv.Rows
            Dim chk As CheckBox = DirectCast(grv.FindControl("ChkSelect"), CheckBox)
            If chk.Checked = True Then
                Dim lbl_id As Label = grv.FindControl("lblID")
                Dim lblShopName As Label = grv.FindControl("lblLocationName")
                Dim lblShopCode As Label = grv.FindControl("lblLocationCode")
                Dim dr As DataRow = dt.NewRow
                dr("id") = lbl_id.Text
                dr("Location_code") = lblShopCode.Text
                dr("Location_name_en") = lblShopName.Text
                dr("region_code") = DirectCast(grv.FindControl("lblRegionCode"), Label).Text
                dr("province_code") = DirectCast(grv.FindControl("lblProvinceCode"), Label).Text
                dr("location_type") = DirectCast(grv.FindControl("lblLocationType"), Label).Text
                dr("location_Segment") = DirectCast(grv.FindControl("lblLocationSegment"), Label).Text
                dt.Rows.Add(dr)
            End If
        Next

        Return dt
    End Function

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Dim dt As New DataTable
        dt = GetSelectedShop(gvShop)
        If dt Is Nothing Then
            Config.SetAlert("กรุณาเลือก Location", Me)
            Exit Sub
        End If
        If dt.Rows.Count > 0 Then
            Session("SelectShopList") = True
            Session("ShopList") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), Guid.NewGuid().ToString(), "window.close();", True)
        Else
            Config.SetAlert("กรุณาเลือก Location", Me)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim filter As String = ""
        If ddlRegion.SelectedValue <> "" Then
            filter &= " and region_code='" & ddlRegion.SelectedValue & "'"
        End If

        If ddlProvince.SelectedValue <> "" Then
            filter &= " and province_code = '" & ddlProvince.SelectedValue & "'"
        End If

        If ddlType.SelectedValue <> "" Then
            filter &= " and location_type = '" & ddlType.SelectedValue & "'"
        End If

        If Trim(txtLocationCode.Text) <> "" Then
            filter &= " and location_code like '%" & Trim(txtLocationCode.Text) & "%'"
        End If

        'If Trim(txtLocationName.Text) <> "" Then
        '    filter &= " and (location_name_en like '%" & Trim(txtLocationName.Text) & "%' "
        '    filter &= " or location_name_th like '%" & Trim(txtLocationName.Text) & "%' )"
        'End If

        SetShop(filter)
    End Sub

    Protected Sub ChkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = sender
        For Each grv As GridViewRow In gvShop.Rows
            Dim chk As CheckBox = grv.FindControl("ChkSelect")
            chk.Checked = chkAll.Checked
        Next
    End Sub
End Class
