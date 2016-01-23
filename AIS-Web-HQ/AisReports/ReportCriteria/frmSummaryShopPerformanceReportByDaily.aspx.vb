Imports System.Data

Partial Class ReportCriteria_frmSummaryShopPerformanceReportByDaily
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(uPara.USERNAME)
            For i As Integer = 0 To shDt.Rows.Count - 1
                shDt.Rows(i).Item("shop_name_en") = shDt.Rows(i).Item("shop_name_en") & "" & "&nbsp;"
            Next
            If shDt.Rows.Count > 0 Then
                chkShopId.DataTextField = "shop_name_en"
                chkShopId.DataValueField = "id"
                chkShopId.DataSource = shDt
                chkShopId.DataBind()
                shDt.Dispose()
            End If
            lEng = Nothing
            uPara = Nothing

            For i As Integer = 0 To chkShopId.Items.Count - 1
                chkShopId.Items(i).Selected = True
            Next
        End If
    End Sub

    'Protected Sub chkAllShop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllShop.CheckedChanged
    '    If chkAllShop.Checked = True Then
    '        For i As Integer = 0 To chkShopId.Items.Count - 1
    '            chkShopId.Items(i).Selected = True
    '        Next
    '    Else
    '        For i As Integer = 0 To chkShopId.Items.Count - 1
    '            chkShopId.Items(i).Selected = False
    '        Next
    '    End If
    'End Sub

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

            chkShopId.DataTextField = "shop_name_en"
            chkShopId.DataValueField = "id"
            chkShopId.DataSource = shDt
            chkShopId.DataBind()
            shDt.Dispose()

            For i As Integer = 0 To chkShopId.Items.Count - 1
                chkShopId.Items(i).Selected = True
            Next
        End If
        uPara = Nothing
        lEng = Nothing
        
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ShopID As String = GetShopID()
        If ShopID.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Shop ที่ต้องการแสดงรายงาน !!')</script>", False)
            Exit Sub
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

        Dim para As String = ""
        para += "?ShopID=" & ShopID
        para += "&DateFrom=" & ctlByDate1.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        para += "&DateTo=" & ctlByDate1.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        para += "&Day=" & rdiDay.SelectedValue
        para += "&NetworkType=" & rdiNetworkType.SelectedValue
        para += "&Segment=" & rdiSegment.SelectedValue
        para += "&ServiceID=" & ddlServiceType1.ServiceType
        para += "&rnd=" & DateTime.Now.Millisecond

        Config.SaveTransLog("เรียกดูรายงาน : Summary Shop Performance Report by Daily เงื่อนไข :" & para, "AisReports.ReportCriteria_frmSummaryShopPerformanceReportByDaily.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repSummaryShopPerformanceReportByDaily.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)

    End Sub

    Private Function GetShopID() As String
        Dim tmpID As String = ""
        For i As Integer = 0 To chkShopId.Items.Count - 1
            If chkShopId.Items(i).Selected = True Then
                If tmpID = "" Then
                    tmpID = chkShopId.Items(i).Value
                Else
                    tmpID += "," & chkShopId.Items(i).Value
                End If
            End If
        Next

        Return tmpID
    End Function
End Class
