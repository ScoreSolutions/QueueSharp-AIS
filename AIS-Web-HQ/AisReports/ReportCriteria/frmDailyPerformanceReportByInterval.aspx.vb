Imports System.Data

Partial Class ReportCriteria_frmDailyPerformanceReportByInterval
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            'Dim lEng As New Engine.Common.LoginENG
            'Dim shDt As New DataTable
            'shDt = lEng.GetShopListByUser(uPara.USERNAME)
            'For i As Integer = 0 To shDt.Rows.Count - 1
            '    shDt.Rows(i).Item("shop_name_en") = shDt.Rows(i).Item("shop_name_en") & "" & "&nbsp;"
            'Next
            'If shDt.Rows.Count > 0 Then
            '    chkShopId.DataTextField = "shop_name_en"
            '    chkShopId.DataValueField = "id"
            '    chkShopId.DataSource = shDt
            '    chkShopId.DataBind()
            '    shDt.Dispose()
            'End If
            'lEng = Nothing
            'uPara = Nothing

            'For i As Integer = 0 To chkShopId.Items.Count - 1
            '    chkShopId.Items(i).Selected = True
            'Next
        End If
    End Sub


    'Protected Sub rdiLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiLocation.SelectedIndexChanged
    '    Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
    '    Dim lEng As New Engine.Common.LoginENG
    '    Dim shDt As New DataTable
    '    shDt = lEng.GetShopListByUser(uPara.USERNAME)
    '    If shDt.Rows.Count > 0 Then
    '        If rdiLocation.SelectedValue <> "ALL" Then
    '            If rdiLocation.SelectedValue = "BKK" Then
    '                shDt.DefaultView.RowFilter = "location_group='BKK'"
    '            ElseIf rdiLocation.SelectedValue = "UPC" Then
    '                shDt.DefaultView.RowFilter = "location_group='RO'"
    '            End If
    '            shDt = shDt.DefaultView.ToTable
    '        End If

    '        chkShopId.DataTextField = "shop_name_en"
    '        chkShopId.DataValueField = "id"
    '        chkShopId.DataSource = shDt
    '        chkShopId.DataBind()
    '        shDt.Dispose()

    '        For i As Integer = 0 To chkShopId.Items.Count - 1
    '            chkShopId.Items(i).Selected = True
    '        Next
    '    End If
    '    uPara = Nothing
    '    lEng = Nothing

    'End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Dim ShopID As String = GetShopID()
        'If ShopID.Trim = "" Then
        '    ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Shop ที่ต้องการแสดงรายงาน !!')</script>", False)
        '    Exit Sub
        'End If

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
        If ctlByTime1.TimeFrom >= ctlByTime1.TimeTo Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Time Period From มากกว่า Time Period To !!')</script>", False)
            Exit Sub
        End If

        Dim ServiceType As String = ""
        If rdbSeviceType.SelectedValue = "1" Then
            ServiceType = ddlServiceType1.ServiceType
            If ServiceType = "All" Then ServiceType = ""
        End If

        Dim Shop As String = ""
        If rdbLocation.SelectedValue <> "" Then
            Shop = ddlShop.Shop
            If Shop = "All" Then Shop = ""
        End If


        Dim para As String = "?ReportName=ByTime"
        para += "&DateFrom=" & ctlByDate1.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        para += "&DateTo=" & ctlByDate1.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

        para += "&NetworkType=" & ddlNetworkServie.SelectedValue
        para += "&ServiceType=" & rdbSeviceType.SelectedValue
        para += "&ServiceID=" & ServiceType

        para += "&Location=" & rdbLocation.SelectedValue
        para += "&Shop=" & Shop

        para += "&Segment=" & ddlSegment.SelectedValue

        para += "&IntervalMinute=" & ctlByTime1.IntervalMinute
        para += "&TimeFrom=" & ctlByTime1.TimeFrom
        para += "&TimeTo=" & ctlByTime1.TimeTo


        para += "&rnd=" & DateTime.Now.Millisecond

        Config.SaveTransLog("เรียกดูรายงาน : Daily Interval Report by Network and Service Type of Shop BKK and UPC เงื่อนไข :" & para, "AisReports.ReportCriteria_frmDailyPerformanceReportByInterval.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repDailyPerformanceReportByInterval.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
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
            ddlServiceType1.Visible = False
        Else
            ddlServiceType1.Visible = True
        End If
    End Sub
End Class
