Imports System.Data

Partial Class ReportCriteria_frmAvgServiceTimeKPIByShop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim dt As DataTable = Engine.Reports.ReportsENG.getShopList()
            Dim dt As DataTable = Engine.Reports.ReportsENG.GetShopListByUser()
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i).Item("shop_name_en") = dt.Rows(i).Item("shop_name_en") & "" & "&nbsp;"
            Next
            If dt.Rows.Count > 0 Then
                chkShopId.DataTextField = "shop_name_en"
                chkShopId.DataValueField = "id"
                chkShopId.DataSource = dt
                chkShopId.DataBind()
            End If
        End If
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

    Private Sub chkAllShop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllShop.CheckedChanged
        If chkAllShop.Checked = True Then
            For i As Integer = 0 To chkShopId.Items.Count - 1
                chkShopId.Items(i).Selected = True
            Next
        Else
            For i As Integer = 0 To chkShopId.Items.Count - 1
                chkShopId.Items(i).Selected = False
            Next
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim para As String = ""
        Dim ShopID As String = GetShopID()
        If ShopID.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Shop ที่ต้องการแสดงรายงาน !!')</script>", False)
            Exit Sub
        End If

        If rdiReportBy.SelectedValue = "Time" Then
            If ctlByTime1.IntervalMinute = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Interval Time!!')</script>", False)
                Exit Sub
            End If
            If ctlByTime1.TimeFrom >= ctlByTime1.TimeTo Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือกPeriod To มากกว่า Period From!!')</script>", False)
                Exit Sub
            End If
            If ctlByTime1.DateFrom.Year = "1" Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date From !!')</script>", False)
                Exit Sub
            End If

            If ctlByTime1.DateTo.Year = "1" Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date To !!')</script>", False)
                Exit Sub
            End If
            If ctlByTime1.DateFrom > ctlByTime1.DateTo Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Date From มากกว่า Date To !!" & ctlByTime1.DateFrom & "')</script>", False)
                Exit Sub
            End If
            para += "?ReportName=ByTime"
            para += "&ShopID=" & ShopID
            para += "&DateFrom=" & ctlByTime1.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("th-TH"))
            para += "&DateTo=" & ctlByTime1.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("th-TH"))
            para += "&Interval=" & ctlByTime1.IntervalMinute
            para += "&TimeFrom=" & ctlByTime1.TimeFrom
            para += "&TimeTo=" & ctlByTime1.TimeTo
            para += "&rnd=" & DateTime.Now.Millisecond
        ElseIf rdiReportBy.SelectedValue = "Week" Then
            If ctlByWeek1.YearFrom = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year From!!')</script>", False)
                Exit Sub
            End If
            If ctlByWeek1.YearTo = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To!!')</script>", False)
                Exit Sub
            End If
            If ctlByWeek1.YearTo < ctlByWeek1.YearFrom Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To มากกว่า  Year From!!')</script>", False)
                Exit Sub
            End If

            If ctlByWeek1.WeekFrom = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Week From!!')</script>", False)
                Exit Sub
            End If
            If ctlByWeek1.WeekTo = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Week To!!')</script>", False)
                Exit Sub
            End If
            If (ctlByWeek1.WeekTo < ctlByWeek1.WeekFrom) And (ctlByWeek1.YearTo = ctlByWeek1.YearFrom) Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Week To มากกว่า  Week From!!')</script>", False)
                Exit Sub
            End If

            para += "?ReportName=ByWeek"
            para += "&ShopID=" & ShopID
            para += "&WeekFrom=" & ctlByWeek1.WeekFrom
            para += "&WeekTo=" & ctlByWeek1.WeekTo
            para += "&YearFrom=" & ctlByWeek1.YearFrom
            para += "&YearTo=" & ctlByWeek1.YearTo
            para += "&rnd" & DateTime.Now.Millisecond
        ElseIf rdiReportBy.SelectedValue = "Date" Then
            If ctlByDate1.DateFrom.Year = "1" Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date From !!')</script>", False)
                Exit Sub
            End If

            If ctlByDate1.DateTo.Year = "1" Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Date To !!')</script>", False)
                Exit Sub
            End If
            If ctlByDate1.DateFrom > ctlByDate1.DateTo Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('คุณเลือก Date From มากกว่า Date To !!" & ctlByDate1.DateFrom & "')</script>", False)
                Exit Sub
            End If
            para += "?ReportName=ByDate"
            para += "&ShopID=" & ShopID
            para += "&DateFrom=" & ctlByDate1.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("th-TH"))
            para += "&DateTo=" & ctlByDate1.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("th-TH"))
            para += "&rnd=" & DateTime.Now.Millisecond

            'ElseIf rdiReportBy.SelectedValue = "Day" Then
            '    If ctlByDay1.Days = "" Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Day!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByDay1.WeekFrom = 0 Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Week From!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByDay1.WeekTo = 0 Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Week To!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByDay1.WeekTo < ctlByDay1.WeekFrom Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Week To มากกว่า  Week From!!')</script>", False)
            '        Exit Sub
            '    End If

            '    If ctlByDay1.YearFrom = 0 Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year From!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByDay1.YearTo = 0 Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByDay1.YearTo < ctlByDay1.YearFrom Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To มากกว่า  Year From!!')</script>", False)
            '        Exit Sub
            '    End If
            '    para += "?ReportName=ByDay"
            '    para += "&ShopID=" & ShopID
            '    para += "&Day=" & ctlByDay1.Days
            '    para += "&WeekFrom=" & ctlByDay1.WeekFrom
            '    para += "&WeekTo=" & ctlByDay1.WeekTo
            '    para += "&YearFrom=" & ctlByDay1.YearFrom
            '    para += "&YearTo=" & ctlByDay1.YearTo
            '    para += "&rnd" & DateTime.Now.Millisecond

        ElseIf rdiReportBy.SelectedValue = "Month" Then
            If ctlByMonth1.YearFrom = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year From!!')</script>", False)
                Exit Sub
            End If
            If ctlByMonth1.YearTo = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To!!')</script>", False)
                Exit Sub
            End If
            If ctlByMonth1.YearTo < ctlByMonth1.YearFrom Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To มากกว่า  Year From!!')</script>", False)
                Exit Sub
            End If

            If ctlByMonth1.MonthFrom = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Month From!!')</script>", False)
                Exit Sub
            End If
            If ctlByMonth1.MonthTo = 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Month To!!')</script>", False)
                Exit Sub
            End If
            If (ctlByMonth1.MonthFrom > ctlByMonth1.MonthTo) And (ctlByMonth1.YearTo = ctlByMonth1.YearFrom) Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Month To มากกว่า  Month From!!')</script>", False)
                Exit Sub
            End If
            para += "?ReportName=ByMonth"
            para += "&ShopID=" & ShopID
            para += "&MonthFrom=" & ctlByMonth1.MonthFrom
            para += "&MonthTo=" & ctlByMonth1.MonthTo
            para += "&YearFrom=" & ctlByMonth1.YearFrom
            para += "&YearTo=" & ctlByMonth1.YearTo
            para += "&rnd" & DateTime.Now.Millisecond

            'ElseIf rdiReportBy.SelectedValue = "Year" Then

            '    If ctlByYear1.YearFrom = 0 Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year From!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByYear1.YearTo = 0 Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To!!')</script>", False)
            '        Exit Sub
            '    End If
            '    If ctlByYear1.YearTo < ctlByYear1.YearFrom Then
            '        ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาระบุ Year To มากกว่า  Year From!!')</script>", False)
            '        Exit Sub
            '    End If

            '    para += "?ReportName=ByYear"
            '    para += "&ShopID=" & ShopID
            '    para += "&YearFrom=" & ctlByYear1.YearFrom
            '    para += "&YearTo=" & ctlByYear1.YearTo
            '    para += "&rnd" & DateTime.Now.Millisecond

        End If

        Config.SaveTransLog("เรียกดูรายงาน : % Customers Served by Shop เงื่อนไข :" & para, "AisReports.ReportCriteria_frmAvgServiceTimeKPIByShop.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repAvgServiceTimeKPIByShop.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub

    Protected Sub rdiReportBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiReportBy.SelectedIndexChanged

        ctlByTime1.Visible = False
        ctlByWeek1.Visible = False
        ctlByDate1.Visible = False
        'ctlByDay1.Visible = False
        ctlByMonth1.Visible = False
        'ctlByYear1.Visible = False

        If rdiReportBy.SelectedValue = "Time" Then
            ctlByTime1.Visible = True
        ElseIf rdiReportBy.SelectedValue = "Week" Then
            ctlByWeek1.Visible = True
        ElseIf rdiReportBy.SelectedValue = "Date" Then
            ctlByDate1.Visible = True
            'ElseIf rdiReportBy.SelectedValue = "Day" Then
            '    ctlByDay1.Visible = True
        ElseIf rdiReportBy.SelectedValue = "Month" Then
            ctlByMonth1.Visible = True
            'ElseIf rdiReportBy.SelectedValue = "Year" Then
            '    ctlByYear1.Visible = True
        End If
    End Sub

End Class
