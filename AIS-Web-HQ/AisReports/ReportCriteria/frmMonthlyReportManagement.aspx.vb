Imports System.Data
Partial Class ReportCriteria_frmMonthlyReportManagement
    Inherits System.Web.UI.Page

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim para As String = ""
        Dim ServiceType As String = ddlServiceType.ServiceTypeName
        If ServiceType = "All" Then ServiceType = ""

        Dim Shop As String = ""
        If rdbLocation.SelectedValue <> "" Then
            Shop = ddlShop.Shop
            If Shop = "All" Then Shop = ""
        End If

        If ctlByDate1.DateFrom.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date From !!')</script>", False)
            Exit Sub
        End If

        If ctlByDate1.DateTo.Year = "1" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date To !!')</script>", False)
            Exit Sub
        End If
        If ctlByDate1.DateFrom > ctlByDate1.DateTo Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Date From มากกว่า Date To !!')</script>", False)
            Exit Sub
        End If

        para += "?ReportName=ByMonth"
        para += "&DateFrom=" & ctlByDate1.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        para += "&DateTo=" & ctlByDate1.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        para += "&NetworkType=" & ddlNetworkServie.SelectedValue
        para += "&LocationCode=" & rdbLocation.SelectedValue
        para += "&ServiceType=" & ServiceType
        para += "&Shop=" & Shop

        Config.SaveTransLog("เรียกดูรายงาน : Monthly Report Management report for CCO & VP-SVM Network Type (Other) Shop-UPC เงื่อนไข :" & para, "AisReports.ReportCriteria_frmMonthlyReportManagement.aspx.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repMonthlyReportManagement.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub

    Protected Sub rdbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbLocation.SelectedIndexChanged
        If rdbLocation.SelectedValue = "" Then
            ddlShop.Visible = False
        Else
            ddlShop.Visible = True
            ddlShop.Region = rdbLocation.SelectedValue
            ddlShop.SetItemList()
        End If

    End Sub
End Class
