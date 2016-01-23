Imports System.Data
Partial Class ReportCriteria_frmMonthlySummaryReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim para As String = ""
        Dim ServiceType As String = ddlServiceType.ServiceTypeName
        If ServiceType = "All" Then ServiceType = ""


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
        If ctlByMonth1.YearFrom.ToString & ctlByMonth1.MonthFrom > ctlByMonth1.YearTo.ToString & ctlByMonth1.MonthTo Then
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OnClick", "<script>alert('กรุณาเลือก Month To มากกว่า  Month From!!')</script>", False)
            Exit Sub
        End If

        para += "?ReportName=ByMonth"
        If ddlNetworkServie.SelectedItem.Text = "All" Then
            para += "&ReportType=6"
            para += "&NetworkType=Null"

        Else
            para += "&ReportType=3"
            para += "&NetworkType=" & ddlNetworkServie.SelectedValue
        End If
        para += "&Location=" & rdbLocation.SelectedValue
        para += "&MonthFrom=" & ctlByMonth1.YearFrom & "-" & ctlByMonth1.MonthFrom & "-01"
        para += "&MonthTo=" & ctlByMonth1.YearTo & "-" & ctlByMonth1.MonthTo & "-" & Date.DaysInMonth(ctlByMonth1.YearTo, ctlByMonth1.MonthTo)
        para += "&rnd=" & DateTime.Now.Millisecond
        para += "&ServiceType=" & ServiceType

        Config.SaveTransLog("เรียกดูรายงาน : Appointment Monthly Summary Report เงื่อนไข :" & para, "AisReports.ReportCriteria_frmMonthlySummaryreport.btnSearch_Click", Config.GetLoginHistoryID)
        Dim scr As String = "window.open('../ReportApp/repMonthlySummaryReport.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub

End Class
