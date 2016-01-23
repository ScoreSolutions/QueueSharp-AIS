Imports System.Data

Partial Class ReportCriteria_frmSummaryShopPerformanceReportByStaff
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(uPara.USERNAME)
            For i As Integer = 0 To shDt.Rows.Count - 1
                shDt.Rows(i).Item("shop_name_en") = shDt.Rows(i).Item("shop_name_en")
            Next
            If shDt.Rows.Count > 0 Then
                cmbShop.SetItemList(shDt, "shop_name_en", "id")
                shDt.Dispose()
            End If
            lEng = Nothing
            uPara = Nothing
        End If
    End Sub

    Protected Sub rdiLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiLocation.SelectedIndexChanged
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim lEng As New Engine.Common.LoginENG
        Dim shDt As New DataTable
        shDt = lEng.GetShopListByUser(uPara.USERNAME)
        If shDt.Rows.Count > 0 Then
            If rdiLocation.SelectedValue <> "ALL" Then
                If rdiLocation.SelectedValue = "BKK" Then
                    shDt.DefaultView.RowFilter = "location_group='BKK'"
                ElseIf rdiLocation.SelectedValue = "UPC" Then
                    shDt.DefaultView.RowFilter = "location_group='RO'"
                End If
                shDt = shDt.DefaultView.ToTable
            End If

            cmbShop.SetItemList(shDt, "shop_name_en", "id")
            shDt.Dispose()
        End If
        uPara = Nothing
        lEng = Nothing

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If cmbShop.SelectedValue = "0" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Shop ที่ต้องการแสดงรายงาน !!')</script>", False)
            Exit Sub
        End If

        If txtDate.DateValue.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date !!')</script>", False)
            Exit Sub
        End If

        Dim para As String = ""
        para += "?ShopID=" & cmbShop.SelectedValue
        para += "&DateFrom=" & txtDate.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        para += "&UserCode=" & cmbStaffName.SelectedValue
        para += "&NetworkType=" & rdiNetworkType.SelectedValue
        para += "&Segment=" & rdiSegment.SelectedValue
        para += "&ServiceID=" & ddlServiceType1.ServiceType
        para += "&rnd=" & DateTime.Now.Millisecond

        Config.SaveTransLog("เรียกดูรายงาน : Summary Shop Performance Report by Staff เงื่อนไข :" & para, "AisReports.ReportCriteria_frmSummaryShopPerformanceReportByStaff.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repSummaryShopPerformanceReportByStaff.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)

    End Sub


    Private Sub SetCmbStaff(ByVal vShopID As Long)
        Dim eng As New Engine.Configuration.ShopUserENG
        Dim dt As New DataTable
        dt = eng.GetShopUser(vShopID)
        If dt.Rows.Count > 0 Then
            cmbStaffName.SetItemList(dt, "fullname2", "user_code")
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub cmbShop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShop.SelectedIndexChanged
        If cmbShop.SelectedValue <> "0" Then
            SetCmbStaff(cmbShop.SelectedValue)
        End If
    End Sub
End Class
