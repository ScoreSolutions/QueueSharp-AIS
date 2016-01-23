Imports System.Data
Partial Class ReportCriteria_frmDailyReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim para As String = ""
        Dim ServiceType As String = ""
        If rdbSeviceType.SelectedValue = "1" Then
            ServiceType = ddlServiceType.ServiceTypeName
            If ServiceType = "All" Then ServiceType = ""
        End If

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

        para += "?ReportName=ByDate"
        If ddlNetworkServie.SelectedItem.Text = "All" And rdbSeviceType.SelectedValue = 1 Then
            para += "&ReportType=4"
            para += "&NetworkType=" & ddlNetworkServie.SelectedValue
        ElseIf ddlNetworkServie.SelectedItem.Text = "All" And rdbSeviceType.SelectedValue = 2 Then
            para += "&ReportType=5"
            para += "&NetworkType=" & ddlNetworkServie.SelectedValue
        Else
            para += "&ReportType=" & rdbSeviceType.SelectedValue
            para += "&NetworkType=" & ddlNetworkServie.SelectedValue
        End If
        para += "&Location=" & rdbLocation.SelectedValue
        para += "&DateFrom=" & ctlByDate1.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        para += "&DateTo=" & ctlByDate1.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        para += "&rnd=" & DateTime.Now.Millisecond
        para += "&ServiceType=" & ServiceType
        para += "&Shop=" & Shop

        Config.SaveTransLog("เรียกดูรายงาน : Daily Report เงื่อนไข :" & para, "AisReports.ReportCriteria_frmDailyReport.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repDailyReport.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
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

    Protected Sub rdbSeviceType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSeviceType.SelectedIndexChanged
        If rdbSeviceType.SelectedValue = "2" Then
            ddlServiceType.Visible = False
        Else
            ddlServiceType.Visible = True
        End If
    End Sub
End Class
