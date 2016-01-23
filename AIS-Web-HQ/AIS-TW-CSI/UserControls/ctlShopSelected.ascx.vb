Imports System.Data

Partial Class UserControls_ctlShopSelected
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property HeaderLeft() As String
        Set(ByVal value As String)
            lblHeaderLeft.Text = value
        End Set
    End Property
    Public WriteOnly Property HeaderRight() As String
        Set(ByVal value As String)
            lblHeaderRight.Text = value
        End Set
    End Property
    Public ReadOnly Property SelectedShop() As DataTable
        Get
            Dim dt As New DataTable
            dt = GetAllShopFromGridview(gvShopLeft)
            Return dt
        End Get
    End Property

    Public Sub SetEditShop(ByVal FilterID As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWShopList(" id in (select distinct tw_location_id from TW_FILTER_BRANCH where tw_filter_id='" & FilterID & "') ", "")

        'Dim dtLeft As New DataTable
        'dtLeft = GetAllShopFromGridview(gvShopLeft)

        'For i As Integer = dt.Rows.Count - 1 To 0 Step -1
        '    dtLeft.DefaultView.RowFilter = "id='" & dt.Rows(i)("id") & "'"
        '    If dtLeft.DefaultView.Count > 0 Then
        '        dt.Rows.RemoveAt(i)
        '    End If
        'Next

        If dt.Rows.Count > 0 Then
            gvShopLeft.DataSource = dt
            gvShopLeft.DataBind()
        Else
            gvShopLeft.DataSource = Nothing
            gvShopLeft.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Function GetSelectedShop(ByVal gv As GridView) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("Location_code")
        dt.Columns.Add("Location_name_en")

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
                dt.Rows.Add(dr)
            End If
        Next

        Return dt
    End Function

    Private Function GetAllShopFromGridview(ByVal gv As GridView) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("Location_code")
        dt.Columns.Add("Location_name_en")
        For Each grv As GridViewRow In gv.Rows
            Dim lbl_id As Label = grv.FindControl("lblID")
            Dim lblLocationName As Label = grv.FindControl("lblLocationName")
            Dim lblLocationCode As Label = grv.FindControl("lblLocationCode")
            Dim dr As DataRow = dt.NewRow
            dr("id") = lbl_id.Text
            dr("Location_code") = lblLocationCode.Text
            dr("Location_name_en") = lblLocationName.Text
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Public Sub ClearSelectedShop()
        SetLeftShop(New DataTable)
    End Sub

    Public Sub SetSelectedShop(ByVal ShopDT As DataTable)
        SetLeftShop(ShopDT)

        Dim dt As New DataTable
        dt = GetAllShopFromGridview(gvShopLeft)
        If dt.Rows.Count > 0 Then
            Dim wh As String = ""
            For Each dr As DataRow In dt.Rows
                If wh = "" Then
                    wh = dr("id")
                Else
                    wh += "," & dr("id")
                End If
            Next
            If wh <> "" Then
                SetRightShop("id not in (" & wh & ") and tw_region_id='" & ddlRegion.SelectedValue & "'")
            End If
        End If
        dt.Dispose()
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        SetSelectedShop(GetSelectedShop(gvShopRight))
        'SetLeftShop(GetSelectedShop(gvShopRight))

        'Dim dt As New DataTable
        'dt = GetAllShopFromGridview(gvShopLeft)
        'If dt.Rows.Count > 0 Then
        '    Dim wh As String = ""
        '    For Each dr As DataRow In dt.Rows
        '        If wh = "" Then
        '            wh = dr("id")
        '        Else
        '            wh += "," & dr("id")
        '        End If
        '    Next
        '    If wh <> "" Then
        '        SetRightShop("sh.id not in (" & wh & ")")
        '    End If
        'End If
        'dt.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetRegion()
            SetRightShop(" region_code='" & ddlRegion.SelectedValue & "'")
        End If
    End Sub

    Private Sub SetLeftShop(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim TmpDt As New DataTable
            TmpDt = GetAllShopFromGridview(gvShopLeft)
            If TmpDt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim TmpDr As DataRow = TmpDt.NewRow
                    TmpDr("id") = dr("id")
                    TmpDr("Location_code") = dr("Location_code")
                    TmpDr("Location_name_en") = dr("Location_name_en")
                    TmpDt.Rows.Add(TmpDr)
                Next
            Else
                TmpDt = dt
            End If

            If TmpDt.Rows.Count > 0 Then
                gvShopLeft.DataSource = TmpDt
                gvShopLeft.DataBind()
            Else
                gvShopLeft.DataSource = Nothing
                gvShopLeft.DataBind()
            End If
            TmpDt.Dispose()
        Else
            gvShopLeft.DataSource = Nothing
            gvShopLeft.DataBind()
        End If
    End Sub

    Private Sub SetRightShop(ByVal wh As String)
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWShopList(wh, "")

        Dim dtLeft As New DataTable
        dtLeft = GetAllShopFromGridview(gvShopLeft)

        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            dtLeft.DefaultView.RowFilter = "id='" & dt.Rows(i)("id") & "'"
            If dtLeft.DefaultView.Count > 0 Then
                dt.Rows.RemoveAt(i)
            End If
        Next

        If dt.Rows.Count > 0 Then
            gvShopRight.DataSource = dt
            gvShopRight.DataBind()
        Else
            gvShopRight.DataSource = Nothing
            gvShopRight.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
       
    End Sub

    Private Sub SetRegion()
        Dim dt As New DataTable
        Dim eng As New Engine.Configuration.MasterENG
        dt = eng.GetTWRegionAllList()

        Dim dr As DataRow = dt.NewRow
        dr("code") = ""
        dr("name") = "-------Select-------"

        dt.Rows.InsertAt(dr, 0)
        ddlRegion.DataSource = dt
        ddlRegion.DataBind()
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub btnDeleteUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteUser.Click
        Dim sDt As New DataTable
        sDt = GetSelectedShop(gvShopLeft)

        'All at Selected Shop
        Dim dt As New DataTable
        dt = GetAllShopFromGridview(gvShopLeft)

        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            sDt.DefaultView.RowFilter = "id='" & dt.Rows(i)("id") & "'"
            If sDt.DefaultView.Count > 0 Then
                dt.Rows.RemoveAt(i)
            End If
        Next

        Dim wh As String = ""
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If wh = "" Then
                    wh = dr("id")
                Else
                    wh += "," & dr("id")
                End If
            Next

            gvShopLeft.DataSource = dt
            gvShopLeft.DataBind()
        Else
            gvShopLeft.DataSource = Nothing
            gvShopLeft.DataBind()
        End If
        sDt.Dispose()
        dt.Dispose()

        If wh <> "" Then
            SetRightShop("id not in (" & wh & ") and region_code='" & ddlRegion.SelectedValue & "'")
        Else
            SetRightShop("1=1 and region_code='" & ddlRegion.SelectedValue & "'")
        End If
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        'ClearSelectedShop()
        SetRightShop("region_code='" & ddlRegion.SelectedValue & "'")
    End Sub

End Class
