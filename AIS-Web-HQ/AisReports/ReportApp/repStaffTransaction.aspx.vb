Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repStaffTransaction
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "StaffTransactionReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repStaffAttendance.ExportData", Config.GetLoginHistoryID)
        Response.ClearContent()
        Response.Charset = "utf-8"
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = _contentType
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        frm.Attributes("runat") = "server"
        frm.Controls.Add(lblReportDesc)
        lblReportDesc.RenderControl(htw)

        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("ReportName") IsNot Nothing Then
            Title = Version
            ShowReport(Request("ReportName"))
        End If
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        If ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.StaffTransactionPara
            para.SHOP_ID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            RenderReportByDate(para)
            para = Nothing
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.StaffTransactionPara
            para.SHOP_ID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            'RenderReportByWeek(para)
            para = Nothing
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.StaffTransactionPara
            para.SHOP_ID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            'RenderReportByMonth(para)
            para = Nothing
        End If

        If lblReportDesc.Text <> "" Then
            lblerror.Visible = False
        End If

    End Sub

    Private Sub RenderReportByDate(ByVal para As CenParaDB.ReportCriteria.StaffTransactionPara)
        Dim eng As New Engine.Reports.RepStaffTransactionENG
        Dim dt As DataTable = eng.GetQueueDataByShop(para)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Queue No.</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Mobile No</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Mobile Segment</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Service Type</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Transaction </br>(Time)</br> Register</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Transaction </br>(Time)Call</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Transaction </br>(Time)</br> Comfirm</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;'>Transaction </br>(Time) HT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Transaction </br>(Time) End</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Transaction </br>(Time) WT</td>")
            ret.Append("    </tr>")

            Dim GCountQueue As Long = 0
            Dim GCountRegis As Long = 0
            Dim GCountCall As Long = 0
            Dim GCountConfirm As Long = 0
            Dim GCountEnd As Long = 0
            Dim GSumHT As Long = 0
            Dim GSumWT As Long = 0

            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_name_en")
            For Each sDr As DataRow In sDt.Rows
                Dim TCountQueue As Long = 0
                Dim TCountRegis As Long = 0
                Dim TCountCall As Long = 0
                Dim TCountConfirm As Long = 0
                Dim TCountEnd As Long = 0
                Dim TSumHT As Long = 0
                Dim TSumWT As Long = 0

                dt.DefaultView.RowFilter = "shop_name_en='" & sDr("shop_name_en") & "'"

                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "show_date", "emp_id", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    Dim CountQueue As Long = 0
                    Dim CountRegis As Long = 0
                    Dim CountCall As Long = 0
                    Dim CountConfirm As Long = 0
                    Dim CountEnd As Long = 0
                    Dim SumHT As Long = 0
                    Dim SumWT As Long = 0

                    dt.DefaultView.RowFilter = "shop_name_en='" & sDr("shop_name_en") & "' and show_date='" & stDr("show_date") & "' and emp_id='" & stDr("emp_id") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr>")
                        ret.Append("        <td align='left'>&nbsp;" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & stDr("show_date") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & stDr("staff_name") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & stDr("emp_id") & "</td>")
                        ret.Append("        <td align='center'>" & dr("queue_no") & "</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dr("mobile_no") & "</td>")
                        ret.Append("        <td align='center'>" & dr("mobile_segment") & "</td>")
                        ret.Append("        <td align='left'>" & dr("service_name_en") & "</td>")
                        If Convert.IsDBNull(dr("register_time")) = False Then
                            ret.Append("        <td align='right'> " & Convert.ToDateTime(dr("register_time")).ToString("HH:mm:ss") & "</td>")
                            CountRegis += 1
                            TCountRegis += 1
                            GCountRegis += 1
                        Else
                            ret.Append("        <td align='right'>&nbsp;</td>")
                        End If
                        If Convert.IsDBNull(dr("call_time")) = False Then
                            ret.Append("        <td align='right'> " & Convert.ToDateTime(dr("call_time")).ToString("HH:mm:ss") & "</td>")
                            CountCall += 1
                            TCountCall += 1
                            GCountCall += 1
                        Else
                            ret.Append("        <td align='right'>&nbsp;</td>")
                        End If
                        If Convert.IsDBNull(dr("confirm_time")) = False Then
                            ret.Append("        <td align='right'> " & Convert.ToDateTime(dr("confirm_time")).ToString("HH:mm:ss") & "</td>")
                            CountConfirm += 1
                            TCountConfirm += 1
                            GCountConfirm += 1
                        Else
                            ret.Append("        <td align='right'>&nbsp;</td>")
                        End If
                        If Convert.IsDBNull(dr("HT")) = False Then
                            ret.Append("        <td align='right'> " & ReportsENG.GetFormatTimeFromSec(dr("HT")) & "</td>")
                            SumHT += Convert.ToInt32(dr("HT"))
                            TSumHT += Convert.ToInt32(dr("HT"))
                            GSumHT += Convert.ToInt32(dr("HT"))
                        Else
                            ret.Append("        <td align='right'>&nbsp;</td>")
                        End If
                        If Convert.IsDBNull(dr("end_time")) = False Then
                            ret.Append("        <td align='right'> " & Convert.ToDateTime(dr("end_time")).ToString("HH:mm:ss") & "</td>")
                            CountEnd += 1
                            TCountEnd += 1
                            GCountEnd += 1
                        Else
                            ret.Append("        <td align='right'>&nbsp;</td>")
                        End If
                        If Convert.IsDBNull(dr("WT")) = False Then
                            ret.Append("        <td align='right'> " & ReportsENG.GetFormatTimeFromSec(dr("WT")) & "</td>")
                            SumWT += Convert.ToInt32(dr("WT"))
                            TSumWT += Convert.ToInt32(dr("WT"))
                            GSumWT += Convert.ToInt32(dr("WT"))
                        Else
                            ret.Append("        <td align='right'>&nbsp;</td>")
                        End If
                        ret.Append("    </tr>")
                        CountQueue += 1
                        TCountQueue += 1
                        GCountQueue += 1
                    Next

                    'Total By Staff
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
                    ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_date") & "</td>")
                    ret.Append("        <td align='left' >Sub Total By " & stDr("staff_name") & "</td>")
                    ret.Append("        <td align='left'>&nbsp;" & stDr("emp_id") & "</td>")
                    ret.Append("        <td align='right' >" & Format(CountQueue, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(CountRegis, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(CountCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(CountConfirm, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & ReportsENG.GetFormatTimeFromSec(SumHT) & "</td>")
                    ret.Append("        <td align='right' >" & Format(CountEnd, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & ReportsENG.GetFormatTimeFromSec(SumWT) & "</td>")
                    ret.Append("    </tr>")
                Next
                stDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >Total สาขา " & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='right' >" & Format(TCountQueue, "#,##0") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='right' >" & Format(TCountRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TCountCall, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TCountConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & ReportsENG.GetFormatTimeFromSec(TSumHT) & "</td>")
                ret.Append("        <td align='right' >" & Format(TCountEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & ReportsENG.GetFormatTimeFromSec(TSumWT) & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            ''******************* Grand Total *******************
            ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='left'>&nbsp;</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='right' >" & Format(GCountQueue, "#,##0") & "</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='right' >" & Format(GCountRegis, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GCountCall, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GCountConfirm, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & ReportsENG.GetFormatTimeFromSec(GSumHT) & "</td>")
            ret.Append("        <td align='right' >" & Format(GCountEnd, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & ReportsENG.GetFormatTimeFromSec(GSumWT) & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    'Private Sub RenderReportByMonth(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
    '    Dim eng As New Engine.Reports.RepStaffAttendanceENG

    '    Dim dt As DataTable = eng.GetReportDataByMonth(para)
    '    If dt.Rows.Count > 0 Then
    '        Dim ret As New StringBuilder
    '        ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
    '        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-In</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-Out</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Total Time</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Productivity(HR)</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Non-Productivity(HR)</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Estimated OT</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Service Time</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Learning</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Stand by</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Brief</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Wrap up</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Consult</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Other Productivity</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Lunch</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Leave</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Change Counter</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Home</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Mini Break</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Rest Room</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Other Non-Productivity</td>")
    '        ret.Append("    </tr>")

    '        Dim GTTotTotalTime As Long = 0
    '        Dim GTTotProductivity As Long = 0
    '        Dim GTTotNonProductivity As Long = 0
    '        Dim GTTotE As Long = 0
    '        Dim GTTotS As Long = 0
    '        Dim GTTotLearning As Long = 0
    '        Dim GTTotStandBy As Long = 0
    '        Dim GTTotBrief As Long = 0
    '        Dim GTTotWarpUp As Long = 0
    '        Dim GTTotConsult As Long = 0
    '        Dim GTTotOtherProd As Long = 0
    '        Dim GTTotLunch As Long = 0
    '        Dim GTTotLeave As Long = 0
    '        Dim GTTotChangeCounter As Long = 0
    '        Dim GTTotHome As Long = 0
    '        Dim GTTotMiniBreak As Long = 0
    '        Dim GTTotRestRoom As Long = 0
    '        Dim GTTotOtherNonProd As Long = 0

    '        Dim GTRecTotTotalTime As Long = 0
    '        Dim GTRecTotProductivity As Long = 0
    '        Dim GTRecTotNonProductivity As Long = 0
    '        Dim GTRecTotE As Long = 0
    '        Dim GTRecTotS As Long = 0
    '        Dim GTRecTotLearning As Long = 0
    '        Dim GTRecTotStandBy As Long = 0
    '        Dim GTRecTotBrief As Long = 0
    '        Dim GTRecTotWarpUp As Long = 0
    '        Dim GTRecTotConsult As Long = 0
    '        Dim GTRecTotOtherProd As Long = 0
    '        Dim GTRecTotLunch As Long = 0
    '        Dim GTRecTotLeave As Long = 0
    '        Dim GTRecTotChangeCounter As Long = 0
    '        Dim GTRecTotHome As Long = 0
    '        Dim GTRecTotMiniBreak As Long = 0
    '        Dim GTRecTotRestRoom As Long = 0
    '        Dim GTRecTotOtherNonProd As Long = 0

    '        Dim sDt As New DataTable
    '        sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
    '        For Each sDr As DataRow In sDt.Rows
    '            Dim TotTotalTime As Long = 0
    '            Dim TotProductivity As Long = 0
    '            Dim TotNonProductivity As Long = 0
    '            Dim TotE As Long = 0
    '            Dim TotS As Long = 0
    '            Dim TotLearning As Long = 0
    '            Dim TotStandBy As Long = 0
    '            Dim TotBrief As Long = 0
    '            Dim TotWarpUp As Long = 0
    '            Dim TotConsult As Long = 0
    '            Dim TotOtherProd As Long = 0
    '            Dim TotLunch As Long = 0
    '            Dim TotLeave As Long = 0
    '            Dim TotChangeCounter As Long = 0
    '            Dim TotHome As Long = 0
    '            Dim TotMiniBreak As Long = 0
    '            Dim TotRestRoom As Long = 0
    '            Dim TotOtherNonProd As Long = 0

    '            Dim RecTotTotalTime As Long = 0
    '            Dim RecTotProductivity As Long = 0
    '            Dim RecTotNonProductivity As Long = 0
    '            Dim RecTotE As Long = 0
    '            Dim RecTotS As Long = 0
    '            Dim RecTotLearning As Long = 0
    '            Dim RecTotStandBy As Long = 0
    '            Dim RecTotBrief As Long = 0
    '            Dim RecTotWarpUp As Long = 0
    '            Dim RecTotConsult As Long = 0
    '            Dim RecTotOtherProd As Long = 0
    '            Dim RecTotLunch As Long = 0
    '            Dim RecTotLeave As Long = 0
    '            Dim RecTotChangeCounter As Long = 0
    '            Dim RecTotHome As Long = 0
    '            Dim RecTotMiniBreak As Long = 0
    '            Dim RecTotRestRoom As Long = 0
    '            Dim RecTotOtherNonProd As Long = 0

    '            dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
    '            Dim mDt As New DataTable
    '            mDt = dt.DefaultView.ToTable(True, "month_no", "show_month", "show_year")
    '            For Each mDr As DataRow In mDt.Rows
    '                Dim STTotTotalTime As Long = 0
    '                Dim STTotProductivity As Long = 0
    '                Dim STTotNonProductivity As Long = 0
    '                Dim STTotE As Long = 0
    '                Dim STTotS As Long = 0
    '                Dim STTotLeasrning As Long = 0
    '                Dim STTotStandBy As Long = 0
    '                Dim STTotBrief As Long = 0
    '                Dim STTotWarpUp As Long = 0
    '                Dim STTotConsult As Long = 0
    '                Dim STTotOtherProd As Long = 0
    '                Dim STTotLunch As Long = 0
    '                Dim STTotLeave As Long = 0
    '                Dim STTotChangeCounter As Long = 0
    '                Dim STTotHome As Long = 0
    '                Dim STTotMiniBreak As Long = 0
    '                Dim STTotRestRoom As Long = 0
    '                Dim STTotOtherNonProd As Long = 0

    '                Dim STRecTotTotalTime As Long = 0
    '                Dim STRecTotProductivity As Long = 0
    '                Dim STRecTotNonProductivity As Long = 0
    '                Dim STRecTotE As Long = 0
    '                Dim STRecTotS As Long = 0
    '                Dim STRecTotLearning As Long = 0
    '                Dim STRecTotStandBy As Long = 0
    '                Dim STRecTotBrief As Long = 0
    '                Dim STRecTotWarpUp As Long = 0
    '                Dim STRecTotConsult As Long = 0
    '                Dim STRecTotOtherProd As Long = 0
    '                Dim STRecTotLunch As Long = 0
    '                Dim STRecTotLeave As Long = 0
    '                Dim STRecTotChangeCounter As Long = 0
    '                Dim STRecTotHome As Long = 0
    '                Dim STRecTotMiniBreak As Long = 0
    '                Dim STRecTotRestRoom As Long = 0
    '                Dim STRecTotOtherNonProd As Long = 0

    '                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and month_no='" & mDr("month_no") & "'"
    '                Dim stDt As New DataTable
    '                stDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "username", "staff_name")
    '                For Each stDr As DataRow In stDt.Rows
    '                    dt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "' and month_no='" & mDr("month_no") & "' and show_year='" & mDr("show_year") & "' and shop_id='" & sDr("shop_id") & "'"
    '                    For Each dr As DataRowView In dt.DefaultView
    '                        ret.Append("    <tr>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("shop_name_en") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("show_month") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("show_year") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("log_in") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("log_out") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("productivity") & "</td>")
    '                        ret.Append("        <td align='right'> " & dr("non_productivity") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
    '                            ret.Append("    <td align='right'> " & ReportsENG.GetFormatTimeFromSec(ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)) & "</td>")
    '                            RecTotE += 1
    '                            STRecTotE += 1
    '                            GTRecTotE += 1
    '                        Else
    '                            ret.Append("    <td align='right'>00:00:00</td>")
    '                        End If
    '                        ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > 0 Then
    '                            RecTotTotalTime += 1
    '                            STRecTotTotalTime += 1
    '                            GTRecTotTotalTime += 1
    '                        End If
    '                        If ReportsENG.GetSecFromTimeFormat(dr("productivity")) > 0 Then
    '                            RecTotProductivity += 1
    '                            STRecTotProductivity += 1
    '                            GTRecTotProductivity += 1
    '                        End If
    '                        If ReportsENG.GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
    '                            RecTotNonProductivity += 1
    '                            STRecTotNonProductivity += 1
    '                            GTRecTotNonProductivity += 1
    '                        End If
    '                        If ReportsENG.GetSecFromTimeFormat(dr("service_time")) > 0 Then
    '                            RecTotS += 1
    '                            STRecTotS += 1
    '                            GTRecTotS += 1
    '                        End If


    '                        '#### Productivity
    '                        ret.Append("        <td align='right' >" & dr("prod_learning") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
    '                            RecTotLearning += 1
    '                            STRecTotLearning += 1
    '                            GTRecTotLearning += 1
    '                            TotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                            STTotLeasrning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                            GTTotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_stand_by") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
    '                            RecTotStandBy += 1
    '                            STRecTotStandBy += 1
    '                            GTRecTotStandBy += 1
    '                            TotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                            STTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                            GTTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_brief") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
    '                            RecTotBrief += 1
    '                            STRecTotBrief += 1
    '                            GTRecTotBrief += 1
    '                            TotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                            STTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                            GTTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_warp_up") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
    '                            RecTotWarpUp += 1
    '                            STRecTotWarpUp += 1
    '                            GTRecTotWarpUp += 1
    '                            TotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                            STTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                            GTTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_consult") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
    '                            RecTotConsult += 1
    '                            STRecTotConsult += 1
    '                            GTRecTotConsult += 1
    '                            TotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                            STTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                            GTTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_other") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_other")) > 0 Then
    '                            RecTotOtherProd += 1
    '                            STRecTotOtherProd += 1
    '                            GTRecTotOtherProd += 1
    '                            TotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                            STTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                            GTTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                        End If


    '                        '### Non Productivity
    '                        ret.Append("        <td align='right' >" & dr("nprod_lunch") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
    '                            RecTotLunch += 1
    '                            STRecTotLunch += 1
    '                            GTRecTotLunch += 1
    '                            TotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                            STTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                            GTTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_leave") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
    '                            RecTotLeave += 1
    '                            STRecTotLeave += 1
    '                            GTRecTotLeave += 1
    '                            TotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                            STTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                            GTTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_change_counter") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
    '                            RecTotChangeCounter += 1
    '                            STRecTotChangeCounter += 1
    '                            GTRecTotChangeCounter += 1
    '                            TotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                            STTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                            GTTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_home") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
    '                            RecTotHome += 1
    '                            STRecTotHome += 1
    '                            GTRecTotHome += 1
    '                            TotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                            STTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                            GTTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_mini_break") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
    '                            RecTotMiniBreak += 1
    '                            STRecTotMiniBreak += 1
    '                            GTRecTotMiniBreak += 1
    '                            TotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                            STTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                            GTTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_restroom") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
    '                            RecTotRestRoom += 1
    '                            STRecTotRestRoom += 1
    '                            GTRecTotRestRoom += 1
    '                            TotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                            STTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                            GTTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_other") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
    '                            RecTotOtherNonProd += 1
    '                            STRecTotOtherNonProd += 1
    '                            GTRecTotOtherNonProd += 1
    '                            TotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                            STTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                            GTTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                        End If
    '                        ret.Append("    </tr>")
    '                    Next
    '                Next
    '                stDt.Dispose()

    '                'Total By Week
    '                ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
    '                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
    '                ret.Append("        <td align='center' >" & mDr("show_month") & "</td>")
    '                ret.Append("        <td align='center' >" & mDr("show_year") & "</td>")
    '                ret.Append("        <td align='center' colspan='2' >Sub Total </td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                If STRecTotTotalTime = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotTotalTime / STRecTotTotalTime) & "</td>")
    '                End If
    '                If STRecTotProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotProductivity / STRecTotProductivity) & "</td>")
    '                End If
    '                If STRecTotNonProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotNonProductivity / STRecTotNonProductivity) & "</td>")
    '                End If
    '                If STRecTotE = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotE / STRecTotE) & "</td>")
    '                End If
    '                If STRecTotS = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotS / STRecTotS) & "</td>")
    '                End If
    '                If STRecTotLearning = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeasrning / STRecTotLearning) & "</td>")
    '                End If
    '                If STRecTotStandBy = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotStandBy / STRecTotStandBy) & "</td>")
    '                End If
    '                If STRecTotBrief = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotBrief / STRecTotBrief) & "</td>")
    '                End If
    '                If STRecTotWarpUp = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotWarpUp / STRecTotWarpUp) & "</td>")
    '                End If
    '                If STRecTotConsult = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotConsult / STRecTotConsult) & "</td>")
    '                End If
    '                If STRecTotOtherProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherProd / STRecTotOtherProd) & "</td>")
    '                End If

    '                If STRecTotLunch = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLunch / STRecTotLunch) & "</td>")
    '                End If
    '                If STRecTotLeave = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeave / STRecTotLeave) & "</td>")
    '                End If
    '                If STRecTotChangeCounter = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotChangeCounter / STRecTotChangeCounter) & "</td>")
    '                End If
    '                If STRecTotHome = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotHome / STRecTotHome) & "</td>")
    '                End If
    '                If STRecTotMiniBreak = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotMiniBreak / STRecTotMiniBreak) & "</td>")
    '                End If
    '                If STRecTotRestRoom = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotRestRoom / STRecTotRestRoom) & "</td>")
    '                End If
    '                If STRecTotOtherNonProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherNonProd / STRecTotOtherNonProd) & "</td>")
    '                End If
    '                ret.Append("    </tr>")
    '            Next
    '            mDt.Dispose()

    '            'Total By Shop
    '            ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
    '            ret.Append("        <td align='center' colspan='5' >Total " & sDr("shop_name_en") & "</td>")
    '            ret.Append("        <td align='center' >&nbsp;</td>")
    '            ret.Append("        <td align='center' >&nbsp;</td>")
    '            If RecTotTotalTime = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / RecTotTotalTime) & "</td>")
    '            End If
    '            If RecTotProductivity = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / RecTotProductivity) & "</td>")
    '            End If
    '            If RecTotNonProductivity = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / RecTotNonProductivity) & "</td>")
    '            End If
    '            If RecTotE = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / RecTotE) & "</td>")
    '            End If
    '            If RecTotS = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / RecTotS) & "</td>")
    '            End If
    '            If RecTotLearning = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / RecTotLearning) & "</td>")
    '            End If
    '            If RecTotStandBy = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / RecTotStandBy) & "</td>")
    '            End If
    '            If RecTotBrief = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / RecTotBrief) & "</td>")
    '            End If
    '            If RecTotWarpUp = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / RecTotWarpUp) & "</td>")
    '            End If
    '            If RecTotConsult = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / RecTotConsult) & "</td>")
    '            End If
    '            If RecTotOtherProd = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / RecTotOtherProd) & "</td>")
    '            End If

    '            If RecTotLunch = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / RecTotLunch) & "</td>")
    '            End If
    '            If RecTotLeave = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / RecTotLeave) & "</td>")
    '            End If
    '            If RecTotChangeCounter = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / RecTotChangeCounter) & "</td>")
    '            End If
    '            If RecTotHome = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / RecTotHome) & "</td>")
    '            End If
    '            If RecTotMiniBreak = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / RecTotMiniBreak) & "</td>")
    '            End If
    '            If RecTotRestRoom = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / RecTotRestRoom) & "</td>")
    '            End If
    '            If RecTotOtherNonProd = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / RecTotOtherNonProd) & "</td>")
    '            End If
    '            ret.Append("    </tr>")
    '        Next
    '        sDt.Dispose()

    '        '******************* Grand Total *******************
    '        ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
    '        ret.Append("        <td align='center' colspan='5' >Grand Total</td>")
    '        ret.Append("        <td align='center' >&nbsp;</td>")
    '        ret.Append("        <td align='center' >&nbsp;</td>")
    '        If GTRecTotTotalTime = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / GTRecTotTotalTime) & "</td>")
    '        End If
    '        If GTRecTotProductivity = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / GTRecTotProductivity) & "</td>")
    '        End If
    '        If GTRecTotNonProductivity = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / GTRecTotNonProductivity) & "</td>")
    '        End If
    '        If GTRecTotE = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / GTRecTotE) & "</td>")
    '        End If
    '        If GTRecTotS = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / GTRecTotS) & "</td>")
    '        End If
    '        If GTRecTotLearning = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / GTRecTotLearning) & "</td>")
    '        End If
    '        If GTRecTotStandBy = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / GTRecTotStandBy) & "</td>")
    '        End If
    '        If GTRecTotBrief = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / GTRecTotBrief) & "</td>")
    '        End If
    '        If GTRecTotWarpUp = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / GTRecTotWarpUp) & "</td>")
    '        End If
    '        If GTRecTotConsult = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / GTRecTotConsult) & "</td>")
    '        End If
    '        If GTRecTotOtherProd = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / GTRecTotOtherProd) & "</td>")
    '        End If

    '        If GTRecTotLunch = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / GTRecTotLunch) & "</td>")
    '        End If
    '        If GTRecTotLeave = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / GTRecTotLeave) & "</td>")
    '        End If
    '        If GTRecTotChangeCounter = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / GTRecTotChangeCounter) & "</td>")
    '        End If
    '        If GTRecTotHome = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / GTRecTotHome) & "</td>")
    '        End If
    '        If GTRecTotMiniBreak = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / GTRecTotMiniBreak) & "</td>")
    '        End If
    '        If GTRecTotRestRoom = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / GTRecTotRestRoom) & "</td>")
    '        End If
    '        If GTRecTotOtherNonProd = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / GTRecTotOtherNonProd) & "</td>")
    '        End If
    '        ret.Append("    </tr>")

    '        ret.Append("</table>")
    '        lblReportDesc.Text = ret.ToString
    '        ret = Nothing
    '    End If
    '    dt.Dispose()
    '    eng = Nothing
    'End Sub

    'Private Sub RenderReportByWeek(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
    '    Dim eng As New Engine.Reports.RepStaffAttendanceENG

    '    Dim dt As DataTable = eng.GetReportDataByWeek(para)
    '    If dt.Rows.Count > 0 Then
    '        Dim ret As New StringBuilder
    '        ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
    '        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Week No.</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-In</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-Out</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Total Time</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Productivity(HR)</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Non-Productivity(HR)</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Estimated OT</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Service Time</td>" & vbNewLine)
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Learning</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Stand by</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Brief</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Wrap up</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Productivity Consult</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Other Productivity</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Lunch</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Leave</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Change Counter</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Home</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Mini Break</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity Rest Room</td>")
    '        ret.Append("        <td align='center' style='color: #ffffff;'>Other Non-Productivity</td>")
    '        ret.Append("    </tr>")

    '        Dim GTTotTotalTime As Long = 0
    '        Dim GTTotProductivity As Long = 0
    '        Dim GTTotNonProductivity As Long = 0
    '        Dim GTTotE As Long = 0
    '        Dim GTTotS As Long = 0
    '        Dim GTTotLearning As Long = 0
    '        Dim GTTotStandBy As Long = 0
    '        Dim GTTotBrief As Long = 0
    '        Dim GTTotWarpUp As Long = 0
    '        Dim GTTotConsult As Long = 0
    '        Dim GTTotOtherProd As Long = 0
    '        Dim GTTotLunch As Long = 0
    '        Dim GTTotLeave As Long = 0
    '        Dim GTTotChangeCounter As Long = 0
    '        Dim GTTotHome As Long = 0
    '        Dim GTTotMiniBreak As Long = 0
    '        Dim GTTotRestRoom As Long = 0
    '        Dim GTTotOtherNonProd As Long = 0

    '        Dim GTRecTotTotalTime As Long = 0
    '        Dim GTRecTotProductivity As Long = 0
    '        Dim GTRecTotNonProductivity As Long = 0
    '        Dim GTRecTotE As Long = 0
    '        Dim GTRecTotS As Long = 0
    '        Dim GTRecTotLearning As Long = 0
    '        Dim GTRecTotStandBy As Long = 0
    '        Dim GTRecTotBrief As Long = 0
    '        Dim GTRecTotWarpUp As Long = 0
    '        Dim GTRecTotConsult As Long = 0
    '        Dim GTRecTotOtherProd As Long = 0
    '        Dim GTRecTotLunch As Long = 0
    '        Dim GTRecTotLeave As Long = 0
    '        Dim GTRecTotChangeCounter As Long = 0
    '        Dim GTRecTotHome As Long = 0
    '        Dim GTRecTotMiniBreak As Long = 0
    '        Dim GTRecTotRestRoom As Long = 0
    '        Dim GTRecTotOtherNonProd As Long = 0

    '        Dim sDt As New DataTable
    '        sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
    '        For Each sDr As DataRow In sDt.Rows
    '            Dim TotTotalTime As Long = 0
    '            Dim TotProductivity As Long = 0
    '            Dim TotNonProductivity As Long = 0
    '            Dim TotE As Long = 0
    '            Dim TotS As Long = 0
    '            Dim TotLearning As Long = 0
    '            Dim TotStandBy As Long = 0
    '            Dim TotBrief As Long = 0
    '            Dim TotWarpUp As Long = 0
    '            Dim TotConsult As Long = 0
    '            Dim TotOtherProd As Long = 0
    '            Dim TotLunch As Long = 0
    '            Dim TotLeave As Long = 0
    '            Dim TotChangeCounter As Long = 0
    '            Dim TotHome As Long = 0
    '            Dim TotMiniBreak As Long = 0
    '            Dim TotRestRoom As Long = 0
    '            Dim TotOtherNonProd As Long = 0

    '            Dim RecTotTotalTime As Long = 0
    '            Dim RecTotProductivity As Long = 0
    '            Dim RecTotNonProductivity As Long = 0
    '            Dim RecTotE As Long = 0
    '            Dim RecTotS As Long = 0
    '            Dim RecTotLearning As Long = 0
    '            Dim RecTotStandBy As Long = 0
    '            Dim RecTotBrief As Long = 0
    '            Dim RecTotWarpUp As Long = 0
    '            Dim RecTotConsult As Long = 0
    '            Dim RecTotOtherProd As Long = 0
    '            Dim RecTotLunch As Long = 0
    '            Dim RecTotLeave As Long = 0
    '            Dim RecTotChangeCounter As Long = 0
    '            Dim RecTotHome As Long = 0
    '            Dim RecTotMiniBreak As Long = 0
    '            Dim RecTotRestRoom As Long = 0
    '            Dim RecTotOtherNonProd As Long = 0

    '            dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
    '            Dim wDt As New DataTable
    '            wDt = dt.DefaultView.ToTable(True, "week_of_year", "show_year")
    '            For Each wDr As DataRow In wDt.Rows
    '                Dim STTotTotalTime As Long = 0
    '                Dim STTotProductivity As Long = 0
    '                Dim STTotNonProductivity As Long = 0
    '                Dim STTotE As Long = 0
    '                Dim STTotS As Long = 0
    '                Dim STTotLeasrning As Long = 0
    '                Dim STTotStandBy As Long = 0
    '                Dim STTotBrief As Long = 0
    '                Dim STTotWarpUp As Long = 0
    '                Dim STTotConsult As Long = 0
    '                Dim STTotOtherProd As Long = 0
    '                Dim STTotLunch As Long = 0
    '                Dim STTotLeave As Long = 0
    '                Dim STTotChangeCounter As Long = 0
    '                Dim STTotHome As Long = 0
    '                Dim STTotMiniBreak As Long = 0
    '                Dim STTotRestRoom As Long = 0
    '                Dim STTotOtherNonProd As Long = 0

    '                Dim STRecTotTotalTime As Long = 0
    '                Dim STRecTotProductivity As Long = 0
    '                Dim STRecTotNonProductivity As Long = 0
    '                Dim STRecTotE As Long = 0
    '                Dim STRecTotS As Long = 0
    '                Dim STRecTotLearning As Long = 0
    '                Dim STRecTotStandBy As Long = 0
    '                Dim STRecTotBrief As Long = 0
    '                Dim STRecTotWarpUp As Long = 0
    '                Dim STRecTotConsult As Long = 0
    '                Dim STRecTotOtherProd As Long = 0
    '                Dim STRecTotLunch As Long = 0
    '                Dim STRecTotLeave As Long = 0
    '                Dim STRecTotChangeCounter As Long = 0
    '                Dim STRecTotHome As Long = 0
    '                Dim STRecTotMiniBreak As Long = 0
    '                Dim STRecTotRestRoom As Long = 0
    '                Dim STRecTotOtherNonProd As Long = 0

    '                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and week_of_year='" & wDr("week_of_year") & "'"
    '                Dim stDt As New DataTable
    '                stDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "username", "staff_name")
    '                For Each stDr As DataRow In stDt.Rows
    '                    dt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "' and week_of_year='" & wDr("week_of_year") & "' and show_year='" & wDr("show_year") & "' and shop_id='" & sDr("shop_id") & "'"
    '                    For Each dr As DataRowView In dt.DefaultView
    '                        ret.Append("    <tr>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("shop_name_en") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("week_of_year") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("show_year") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
    '                        ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("log_in") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("log_out") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
    '                        ret.Append("        <td align='right'>" & dr("productivity") & "</td>")
    '                        ret.Append("        <td align='right'> " & dr("non_productivity") & "</td>")
    '                        If Convert.IsDBNull(dr("total_time")) = False Then
    '                            If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
    '                                ret.Append("    <td align='right'> " & ReportsENG.GetFormatTimeFromSec(ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)) & "</td>")
    '                                RecTotE += 1
    '                                STRecTotE += 1
    '                                GTRecTotE += 1
    '                            Else
    '                                ret.Append("    <td align='right'>00:00:00</td>")
    '                            End If
    '                        Else
    '                            ret.Append("    <td align='right'>00:00:00</td>")
    '                        End If

    '                        ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
    '                        If Convert.IsDBNull(dr("total_time")) = False Then
    '                            If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > 0 Then
    '                                RecTotTotalTime += 1
    '                                STRecTotTotalTime += 1
    '                                GTRecTotTotalTime += 1
    '                            End If
    '                        End If
    '                        If ReportsENG.GetSecFromTimeFormat(dr("productivity")) > 0 Then
    '                            RecTotProductivity += 1
    '                            STRecTotProductivity += 1
    '                            GTRecTotProductivity += 1
    '                        End If
    '                        If ReportsENG.GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
    '                            RecTotNonProductivity += 1
    '                            STRecTotNonProductivity += 1
    '                            GTRecTotNonProductivity += 1
    '                        End If
    '                        If ReportsENG.GetSecFromTimeFormat(dr("service_time")) > 0 Then
    '                            RecTotS += 1
    '                            STRecTotS += 1
    '                            GTRecTotS += 1
    '                        End If


    '                        '#### Productivity
    '                        ret.Append("        <td align='right' >" & dr("prod_learning") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
    '                            RecTotLearning += 1
    '                            STRecTotLearning += 1
    '                            GTRecTotLearning += 1
    '                            TotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                            STTotLeasrning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                            GTTotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_stand_by") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
    '                            RecTotStandBy += 1
    '                            STRecTotStandBy += 1
    '                            GTRecTotStandBy += 1
    '                            TotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                            STTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                            GTTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_brief") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
    '                            RecTotBrief += 1
    '                            STRecTotBrief += 1
    '                            GTRecTotBrief += 1
    '                            TotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                            STTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                            GTTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_warp_up") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
    '                            RecTotWarpUp += 1
    '                            STRecTotWarpUp += 1
    '                            GTRecTotWarpUp += 1
    '                            TotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                            STTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                            GTTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_consult") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
    '                            RecTotConsult += 1
    '                            STRecTotConsult += 1
    '                            GTRecTotConsult += 1
    '                            TotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                            STTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                            GTTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("prod_other") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("prod_other")) > 0 Then
    '                            RecTotOtherProd += 1
    '                            STRecTotOtherProd += 1
    '                            GTRecTotOtherProd += 1
    '                            TotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                            STTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                            GTTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                        End If


    '                        '### Non Productivity
    '                        ret.Append("        <td align='right' >" & dr("nprod_lunch") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
    '                            RecTotLunch += 1
    '                            STRecTotLunch += 1
    '                            GTRecTotLunch += 1
    '                            TotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                            STTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                            GTTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_leave") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
    '                            RecTotLeave += 1
    '                            STRecTotLeave += 1
    '                            GTRecTotLeave += 1
    '                            TotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                            STTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                            GTTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_change_counter") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
    '                            RecTotChangeCounter += 1
    '                            STRecTotChangeCounter += 1
    '                            GTRecTotChangeCounter += 1
    '                            TotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                            STTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                            GTTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_home") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
    '                            RecTotHome += 1
    '                            STRecTotHome += 1
    '                            GTRecTotHome += 1
    '                            TotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                            STTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                            GTTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_mini_break") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
    '                            RecTotMiniBreak += 1
    '                            STRecTotMiniBreak += 1
    '                            GTRecTotMiniBreak += 1
    '                            TotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                            STTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                            GTTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_restroom") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
    '                            RecTotRestRoom += 1
    '                            STRecTotRestRoom += 1
    '                            GTRecTotRestRoom += 1
    '                            TotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                            STTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                            GTTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                        End If

    '                        ret.Append("        <td align='right' >" & dr("nprod_other") & "</td>")
    '                        If ReportsENG.GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
    '                            RecTotOtherNonProd += 1
    '                            STRecTotOtherNonProd += 1
    '                            GTRecTotOtherNonProd += 1
    '                            TotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                            STTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                            GTTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                        End If
    '                        ret.Append("    </tr>")
    '                    Next
    '                Next
    '                stDt.Dispose()

    '                'Total By Week
    '                ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
    '                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
    '                ret.Append("        <td align='center' >" & wDr("week_of_year") & "</td>")
    '                ret.Append("        <td align='center' >" & wDr("show_year") & "</td>")
    '                ret.Append("        <td align='center' colspan='2' >Sub Total </td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                If STRecTotTotalTime = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotTotalTime / STRecTotTotalTime) & "</td>")
    '                End If
    '                If STRecTotProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotProductivity / STRecTotProductivity) & "</td>")
    '                End If
    '                If STRecTotNonProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotNonProductivity / STRecTotNonProductivity) & "</td>")
    '                End If
    '                If STRecTotE = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotE / STRecTotE) & "</td>")
    '                End If
    '                If STRecTotS = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotS / STRecTotS) & "</td>")
    '                End If
    '                If STRecTotLearning = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeasrning / STRecTotLearning) & "</td>")
    '                End If
    '                If STRecTotStandBy = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotStandBy / STRecTotStandBy) & "</td>")
    '                End If
    '                If STRecTotBrief = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotBrief / STRecTotBrief) & "</td>")
    '                End If
    '                If STRecTotWarpUp = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotWarpUp / STRecTotWarpUp) & "</td>")
    '                End If
    '                If STRecTotConsult = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotConsult / STRecTotConsult) & "</td>")
    '                End If
    '                If STRecTotOtherProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherProd / STRecTotOtherProd) & "</td>")
    '                End If

    '                If STRecTotLunch = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLunch / STRecTotLunch) & "</td>")
    '                End If
    '                If STRecTotLeave = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeave / STRecTotLeave) & "</td>")
    '                End If
    '                If STRecTotChangeCounter = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotChangeCounter / STRecTotChangeCounter) & "</td>")
    '                End If
    '                If STRecTotHome = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotHome / STRecTotHome) & "</td>")
    '                End If
    '                If STRecTotMiniBreak = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotMiniBreak / STRecTotMiniBreak) & "</td>")
    '                End If
    '                If STRecTotRestRoom = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotRestRoom / STRecTotRestRoom) & "</td>")
    '                End If
    '                If STRecTotOtherNonProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherNonProd / STRecTotOtherNonProd) & "</td>")
    '                End If
    '                ret.Append("    </tr>")
    '            Next
    '            wDt.Dispose()

    '            'Total By Shop
    '            ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
    '            ret.Append("        <td align='center' colspan='5' >Total " & sDr("shop_name_en") & "</td>")
    '            ret.Append("        <td align='center' >&nbsp;</td>")
    '            ret.Append("        <td align='center' >&nbsp;</td>")
    '            If RecTotTotalTime = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / RecTotTotalTime) & "</td>")
    '            End If
    '            If RecTotProductivity = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / RecTotProductivity) & "</td>")
    '            End If
    '            If RecTotNonProductivity = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / RecTotNonProductivity) & "</td>")
    '            End If
    '            If RecTotE = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / RecTotE) & "</td>")
    '            End If
    '            If RecTotS = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / RecTotS) & "</td>")
    '            End If
    '            If RecTotLearning = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / RecTotLearning) & "</td>")
    '            End If
    '            If RecTotStandBy = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / RecTotStandBy) & "</td>")
    '            End If
    '            If RecTotBrief = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / RecTotBrief) & "</td>")
    '            End If
    '            If RecTotWarpUp = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / RecTotWarpUp) & "</td>")
    '            End If
    '            If RecTotConsult = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / RecTotConsult) & "</td>")
    '            End If
    '            If RecTotOtherProd = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / RecTotOtherProd) & "</td>")
    '            End If

    '            If RecTotLunch = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / RecTotLunch) & "</td>")
    '            End If
    '            If RecTotLeave = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / RecTotLeave) & "</td>")
    '            End If
    '            If RecTotChangeCounter = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / RecTotChangeCounter) & "</td>")
    '            End If
    '            If RecTotHome = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / RecTotHome) & "</td>")
    '            End If
    '            If RecTotMiniBreak = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / RecTotMiniBreak) & "</td>")
    '            End If
    '            If RecTotRestRoom = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / RecTotRestRoom) & "</td>")
    '            End If
    '            If RecTotOtherNonProd = 0 Then
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Else
    '                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / RecTotOtherNonProd) & "</td>")
    '            End If
    '            ret.Append("    </tr>")
    '        Next
    '        sDt.Dispose()

    '        '******************* Grand Total *******************
    '        ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
    '        ret.Append("        <td align='center' colspan='5' >Grand Total</td>")
    '        ret.Append("        <td align='center' >&nbsp;</td>")
    '        ret.Append("        <td align='center' >&nbsp;</td>")
    '        If GTRecTotTotalTime = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / GTRecTotTotalTime) & "</td>")
    '        End If
    '        If GTRecTotProductivity = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / GTRecTotProductivity) & "</td>")
    '        End If
    '        If GTRecTotNonProductivity = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / GTRecTotNonProductivity) & "</td>")
    '        End If
    '        If GTRecTotE = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / GTRecTotE) & "</td>")
    '        End If
    '        If GTRecTotS = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / GTRecTotS) & "</td>")
    '        End If
    '        If GTRecTotLearning = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / GTRecTotLearning) & "</td>")
    '        End If
    '        If GTRecTotStandBy = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / GTRecTotStandBy) & "</td>")
    '        End If
    '        If GTRecTotBrief = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / GTRecTotBrief) & "</td>")
    '        End If
    '        If GTRecTotWarpUp = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / GTRecTotWarpUp) & "</td>")
    '        End If
    '        If GTRecTotConsult = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / GTRecTotConsult) & "</td>")
    '        End If
    '        If GTRecTotOtherProd = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / GTRecTotOtherProd) & "</td>")
    '        End If

    '        If GTRecTotLunch = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / GTRecTotLunch) & "</td>")
    '        End If
    '        If GTRecTotLeave = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / GTRecTotLeave) & "</td>")
    '        End If
    '        If GTRecTotChangeCounter = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / GTRecTotChangeCounter) & "</td>")
    '        End If
    '        If GTRecTotHome = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / GTRecTotHome) & "</td>")
    '        End If
    '        If GTRecTotMiniBreak = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / GTRecTotMiniBreak) & "</td>")
    '        End If
    '        If GTRecTotRestRoom = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / GTRecTotRestRoom) & "</td>")
    '        End If
    '        If GTRecTotOtherNonProd = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / GTRecTotOtherNonProd) & "</td>")
    '        End If
    '        ret.Append("    </tr>")

    '        ret.Append("</table>")
    '        lblReportDesc.Text = ret.ToString
    '        ret = Nothing
    '    End If
    '    dt.Dispose()
    '    eng = Nothing
    'End Sub

    'Private Sub RenderReportByDate(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
    '    Dim eng As New Engine.Reports.RepStaffAttendanceENG

    '    Dim dt As DataTable = eng.GetReportDataByDate(para)
    '    If dt.Rows.Count > 0 Then
    '        lblerror.Visible = False
    '        'Dim RowProdTmp() As String = {}
    '        'Dim RowNonTmp() As String = {}
    '        Dim ShopID As Long = 0
    '        Dim ShopIDGroup As Long = 0
    '        Dim DateGroup As String = ""
    '        Dim ShopName As String = ""

    '        Dim TotTotalTime As Long = 0
    '        Dim TotProductivity As Long = 0
    '        Dim TotNonProductivity As Long = 0
    '        Dim TotE As Long = 0
    '        Dim TotS As Long = 0
    '        Dim TotLearning As Long = 0
    '        Dim TotStandBy As Long = 0
    '        Dim TotBrief As Long = 0
    '        Dim TotWarpUp As Long = 0
    '        Dim TotConsult As Long = 0
    '        Dim TotOtherProd As Long = 0
    '        Dim TotLunch As Long = 0
    '        Dim TotLeave As Long = 0
    '        Dim TotChangeCounter As Long = 0
    '        Dim TotHome As Long = 0
    '        Dim TotMiniBreak As Long = 0
    '        Dim TotRestRoom As Long = 0
    '        Dim TotOtherNonProd As Long = 0

    '        Dim RecTotTotalTime As Long = 0
    '        Dim RecTotProductivity As Long = 0
    '        Dim RecTotNonProductivity As Long = 0
    '        Dim RecTotE As Long = 0
    '        Dim RecTotS As Long = 0
    '        Dim RecTotLearning As Long = 0
    '        Dim RecTotStandBy As Long = 0
    '        Dim RecTotBrief As Long = 0
    '        Dim RecTotWarpUp As Long = 0
    '        Dim RecTotConsult As Long = 0
    '        Dim RecTotOtherProd As Long = 0
    '        Dim RecTotLunch As Long = 0
    '        Dim RecTotLeave As Long = 0
    '        Dim RecTotChangeCounter As Long = 0
    '        Dim RecTotHome As Long = 0
    '        Dim RecTotMiniBreak As Long = 0
    '        Dim RecTotRestRoom As Long = 0
    '        Dim RecTotOtherNonProd As Long = 0

    '        Dim STTotTotalTime As Long = 0
    '        Dim STTotProductivity As Long = 0
    '        Dim STTotNonProductivity As Long = 0
    '        Dim STTotE As Long = 0
    '        Dim STTotS As Long = 0
    '        Dim STTotLeasrning As Long = 0
    '        Dim STTotStandBy As Long = 0
    '        Dim STTotBrief As Long = 0
    '        Dim STTotWarpUp As Long = 0
    '        Dim STTotConsult As Long = 0
    '        Dim STTotOtherProd As Long = 0
    '        Dim STTotLunch As Long = 0
    '        Dim STTotLeave As Long = 0
    '        Dim STTotChangeCounter As Long = 0
    '        Dim STTotHome As Long = 0
    '        Dim STTotMiniBreak As Long = 0
    '        Dim STTotRestRoom As Long = 0
    '        Dim STTotOtherNonProd As Long = 0

    '        Dim STRecTotTotalTime As Long = 0
    '        Dim STRecTotProductivity As Long = 0
    '        Dim STRecTotNonProductivity As Long = 0
    '        Dim STRecTotE As Long = 0
    '        Dim STRecTotS As Long = 0
    '        Dim STRecTotLearning As Long = 0
    '        Dim STRecTotStandBy As Long = 0
    '        Dim STRecTotBrief As Long = 0
    '        Dim STRecTotWarpUp As Long = 0
    '        Dim STRecTotConsult As Long = 0
    '        Dim STRecTotOtherProd As Long = 0
    '        Dim STRecTotLunch As Long = 0
    '        Dim STRecTotLeave As Long = 0
    '        Dim STRecTotChangeCounter As Long = 0
    '        Dim STRecTotHome As Long = 0
    '        Dim STRecTotMiniBreak As Long = 0
    '        Dim STRecTotRestRoom As Long = 0
    '        Dim STRecTotOtherNonProd As Long = 0

    '        Dim GTTotTotalTime As Long = 0
    '        Dim GTTotProductivity As Long = 0
    '        Dim GTTotNonProductivity As Long = 0
    '        Dim GTTotE As Long = 0
    '        Dim GTTotS As Long = 0
    '        Dim GTTotLearning As Long = 0
    '        Dim GTTotStandBy As Long = 0
    '        Dim GTTotBrief As Long = 0
    '        Dim GTTotWarpUp As Long = 0
    '        Dim GTTotConsult As Long = 0
    '        Dim GTTotOtherProd As Long = 0
    '        Dim GTTotLunch As Long = 0
    '        Dim GTTotLeave As Long = 0
    '        Dim GTTotChangeCounter As Long = 0
    '        Dim GTTotHome As Long = 0
    '        Dim GTTotMiniBreak As Long = 0
    '        Dim GTTotRestRoom As Long = 0
    '        Dim GTTotOtherNonProd As Long = 0

    '        Dim GTRecTotTotalTime As Long = 0
    '        Dim GTRecTotProductivity As Long = 0
    '        Dim GTRecTotNonProductivity As Long = 0
    '        Dim GTRecTotE As Long = 0
    '        Dim GTRecTotS As Long = 0
    '        Dim GTRecTotLearning As Long = 0
    '        Dim GTRecTotStandBy As Long = 0
    '        Dim GTRecTotBrief As Long = 0
    '        Dim GTRecTotWarpUp As Long = 0
    '        Dim GTRecTotConsult As Long = 0
    '        Dim GTRecTotOtherProd As Long = 0
    '        Dim GTRecTotLunch As Long = 0
    '        Dim GTRecTotLeave As Long = 0
    '        Dim GTRecTotChangeCounter As Long = 0
    '        Dim GTRecTotHome As Long = 0
    '        Dim GTRecTotMiniBreak As Long = 0
    '        Dim GTRecTotRestRoom As Long = 0
    '        Dim GTRecTotOtherNonProd As Long = 0

    '        'Dim TotProductivityMin() As String = {}
    '        'Dim TotNonProductivityMin() As String = {}
    '        'Dim RecTotProductivityMin() As String = {}
    '        'Dim RecTotNonProductivityMin() As String = {}
    '        'Dim STTotProductivityMin() As String = {}
    '        'Dim STTotNonProductivityMin() As String = {}
    '        'Dim STRecTotProductivityMin() As String = {}
    '        'Dim STRecTotNonProductivityMin() As String = {}
    '        'Dim GTTotProductivityMin() As String = {}
    '        'Dim GTTotNonProductivityMin() As String = {}
    '        'Dim GTRecTotProductivityMin() As String = {}
    '        'Dim GTRecTotNonProductivityMin() As String = {}

    '        Dim ret As New StringBuilder
    '        ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(i)
    '            If i = 0 Then
    '                'Header Row
    '                ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Date</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Log-In</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Log-Out</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Total Time</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Productivity</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Non-Productivity</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Estimated OT</td>" & vbNewLine)
    '                ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Service Time</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' colspan='6' >Productivity</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Non-Productivity</td>" & vbNewLine)
    '                ret.Append("    </tr>")
    '                ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Learning</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Stand by</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Brief</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Wrap up</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Consult</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Other Productivity</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Lunch</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Leave</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Change Counter</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Home</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Mini Break</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Rest Room</td>")
    '                ret.Append("        <td align='center' style='color: #ffffff;'>Other Non-Productivity</td>")
    '                ret.Append("    </tr>")
    '            End If

    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '                ShopID = dt.Rows(i).Item("shop_id").ToString
    '                DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
    '            End If

    '            '********************* File Data *************************
    '            ret.Append("    <tr>")
    '            ret.Append("        <td align='left'>&nbsp;" & dr("shop_name_en") & "</td>")
    '            ret.Append("        <td align='left'>&nbsp;" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
    '            ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
    '            ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
    '            ret.Append("        <td align='right'>" & dr("log_in") & "</td>")
    '            ret.Append("        <td align='right'>" & dr("log_out") & "</td>")
    '            ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(ReportsENG.GetSecFromTimeFormat(dr("productivity"))) & "</td>")
    '            ret.Append("        <td align='right'> " & dr("non_productivity") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
    '                ret.Append("    <td align='right'> " & ReportsENG.GetFormatTimeFromSec(ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)) & "</td>")
    '                RecTotE += 1
    '                STRecTotE += 1
    '                GTRecTotE += 1
    '            Else
    '                ret.Append("    <td align='right'>00:00:00</td>")
    '            End If
    '            ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > 0 Then
    '                RecTotTotalTime += 1
    '                STRecTotTotalTime += 1
    '                GTRecTotTotalTime += 1
    '            End If
    '            If ReportsENG.GetSecFromTimeFormat(dr("productivity")) > 0 Then
    '                RecTotProductivity += 1
    '                STRecTotProductivity += 1
    '                GTRecTotProductivity += 1
    '            End If
    '            If ReportsENG.GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
    '                RecTotNonProductivity += 1
    '                STRecTotNonProductivity += 1
    '                GTRecTotNonProductivity += 1
    '            End If
    '            If ReportsENG.GetSecFromTimeFormat(dr("service_time")) > 0 Then
    '                RecTotS += 1
    '                STRecTotS += 1
    '                GTRecTotS += 1
    '            End If


    '            '#### Productivity
    '            ret.Append("        <td align='right' >" & dr("prod_learning") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
    '                RecTotLearning += 1
    '                STRecTotLearning += 1
    '                GTRecTotLearning += 1
    '                TotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                STTotLeasrning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '                GTTotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("prod_stand_by") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
    '                RecTotStandBy += 1
    '                STRecTotStandBy += 1
    '                GTRecTotStandBy += 1
    '                TotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                STTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '                GTTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("prod_brief") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
    '                RecTotBrief += 1
    '                STRecTotBrief += 1
    '                GTRecTotBrief += 1
    '                TotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                STTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '                GTTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("prod_warp_up") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
    '                RecTotWarpUp += 1
    '                STRecTotWarpUp += 1
    '                GTRecTotWarpUp += 1
    '                TotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                STTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '                GTTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("prod_consult") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
    '                RecTotConsult += 1
    '                STRecTotConsult += 1
    '                GTRecTotConsult += 1
    '                TotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                STTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '                GTTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("prod_other") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("prod_other")) > 0 Then
    '                RecTotOtherProd += 1
    '                STRecTotOtherProd += 1
    '                GTRecTotOtherProd += 1
    '                TotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                STTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '                GTTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
    '            End If


    '            '### Non Productivity
    '            ret.Append("        <td align='right' >" & dr("nprod_lunch") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
    '                RecTotLunch += 1
    '                STRecTotLunch += 1
    '                GTRecTotLunch += 1
    '                TotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                STTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '                GTTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("nprod_leave") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
    '                RecTotLeave += 1
    '                STRecTotLeave += 1
    '                GTRecTotLeave += 1
    '                TotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                STTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '                GTTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("nprod_change_counter") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
    '                RecTotChangeCounter += 1
    '                STRecTotChangeCounter += 1
    '                GTRecTotChangeCounter += 1
    '                TotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                STTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                GTTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("nprod_home") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
    '                RecTotHome += 1
    '                STRecTotHome += 1
    '                GTRecTotHome += 1
    '                TotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                STTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '                GTTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("nprod_mini_break") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
    '                RecTotMiniBreak += 1
    '                STRecTotMiniBreak += 1
    '                GTRecTotMiniBreak += 1
    '                TotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                STTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                GTTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("nprod_restroom") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
    '                RecTotRestRoom += 1
    '                STRecTotRestRoom += 1
    '                GTRecTotRestRoom += 1
    '                TotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                STTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '                GTTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
    '            End If

    '            ret.Append("        <td align='right' >" & dr("nprod_other") & "</td>")
    '            If ReportsENG.GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
    '                RecTotOtherNonProd += 1
    '                STRecTotOtherNonProd += 1
    '                GTRecTotOtherNonProd += 1
    '                TotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                STTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '                GTTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
    '            End If
    '            ret.Append("    </tr>")

    '            If Convert.IsDBNull(dr("total_time")) = False Then
    '                TotTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
    '                STTotTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
    '                GTTotTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
    '            End If
    '            If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
    '                TotE += ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)
    '                STTotE += ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)
    '                GTTotE += ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)
    '            End If

    '            TotS += ReportsENG.GetSecFromTimeFormat(dr("service_time"))
    '            TotProductivity += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
    '            TotNonProductivity += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))
    '            STTotS += ReportsENG.GetSecFromTimeFormat(dr("service_time"))
    '            STTotProductivity += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
    '            STTotNonProductivity += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))
    '            GTTotS += ReportsENG.GetSecFromTimeFormat(dr("service_time"))
    '            GTTotProductivity += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
    '            GTTotNonProductivity += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))

    '            '******************** Sub Total ********************
    '            Dim ChkSubTotal As Boolean = False
    '            If dt.Rows.Count = i + 1 Then
    '                ChkSubTotal = True
    '            Else
    '                If dt.Rows(i).Item("shop_id") <> dt.Rows(i + 1).Item("shop_id") Or CDate(dt.Rows(i).Item("service_date")).ToShortDateString <> CDate(dt.Rows(i + 1).Item("service_date")).ToShortDateString Then
    '                    ChkSubTotal = True
    '                    ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                    DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
    '                    ShopName = dt.Rows(i).Item("shop_name_en").ToString
    '                End If
    '            End If

    '            If ChkSubTotal = True Then
    '                ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
    '                ret.Append("        <td align='center' colspan='6' >Sub Total</td>")
    '                If RecTotTotalTime = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / RecTotTotalTime) & "</td>")
    '                End If
    '                If RecTotProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / RecTotProductivity) & "</td>")
    '                End If
    '                If RecTotNonProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / RecTotNonProductivity) & "</td>")
    '                End If
    '                If RecTotE = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / RecTotE) & "</td>")
    '                End If
    '                If RecTotS = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / RecTotS) & "</td>")
    '                End If
    '                If RecTotLearning = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / RecTotLearning) & "</td>")
    '                End If
    '                If RecTotStandBy = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / RecTotStandBy) & "</td>")
    '                End If
    '                If RecTotBrief = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / RecTotBrief) & "</td>")
    '                End If
    '                If RecTotWarpUp = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / RecTotWarpUp) & "</td>")
    '                End If
    '                If RecTotConsult = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / RecTotConsult) & "</td>")
    '                End If
    '                If RecTotOtherProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / RecTotOtherProd) & "</td>")
    '                End If

    '                If RecTotLunch = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / RecTotLunch) & "</td>")
    '                End If
    '                If RecTotLeave = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / RecTotLeave) & "</td>")
    '                End If
    '                If RecTotChangeCounter = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / RecTotChangeCounter) & "</td>")
    '                End If
    '                If RecTotHome = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / RecTotHome) & "</td>")
    '                End If
    '                If RecTotMiniBreak = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / RecTotMiniBreak) & "</td>")
    '                End If
    '                If RecTotRestRoom = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / RecTotRestRoom) & "</td>")
    '                End If
    '                If RecTotOtherNonProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / RecTotOtherNonProd) & "</td>")
    '                End If
    '                ret.Append("    </tr>")

    '                TotTotalTime = 0
    '                TotProductivity = 0
    '                TotNonProductivity = 0
    '                TotE = 0
    '                TotS = 0
    '                TotLearning = 0
    '                TotStandBy = 0
    '                TotBrief = 0
    '                TotWarpUp = 0
    '                TotConsult = 0
    '                TotOtherProd = 0
    '                TotLunch = 0
    '                TotLeave = 0
    '                TotChangeCounter = 0
    '                TotHome = 0
    '                TotMiniBreak = 0
    '                TotRestRoom = 0
    '                TotOtherNonProd = 0

    '                RecTotTotalTime = 0
    '                RecTotProductivity = 0
    '                RecTotNonProductivity = 0
    '                RecTotE = 0
    '                RecTotS = 0
    '                RecTotLearning = 0
    '                RecTotStandBy = 0
    '                RecTotBrief = 0
    '                RecTotWarpUp = 0
    '                RecTotConsult = 0
    '                RecTotOtherProd = 0
    '                RecTotLunch = 0
    '                RecTotLeave = 0
    '                RecTotChangeCounter = 0
    '                RecTotHome = 0
    '                RecTotMiniBreak = 0
    '                RecTotRestRoom = 0
    '                RecTotOtherNonProd = 0
    '            End If
    '            '***************************************************
    '            '************************ Total ***************************
    '            Dim ChkTotal As Boolean = False
    '            If dt.Rows.Count = i + 1 Then
    '                ChkTotal = True
    '            Else
    '                If dt.Rows(i).Item("Shop_id") <> dt.Rows(i + 1).Item("Shop_id") Then
    '                    ChkTotal = True
    '                End If
    '            End If
    '            If ChkTotal = True Then
    '                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
    '                ret.Append("        <td align='center' colspan='6' >Total " & ShopName & "</td>")
    '                If STRecTotTotalTime = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotTotalTime / STRecTotTotalTime) & "</td>")
    '                End If
    '                If STRecTotProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotProductivity / STRecTotProductivity) & "</td>")
    '                End If
    '                If STRecTotNonProductivity = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotNonProductivity / STRecTotNonProductivity) & "</td>")
    '                End If
    '                If STRecTotE = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotE / STRecTotE) & "</td>")
    '                End If
    '                If STRecTotS = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotS / STRecTotS) & "</td>")
    '                End If
    '                If STRecTotLearning = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeasrning / STRecTotLearning) & "</td>")
    '                End If
    '                If STRecTotStandBy = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotStandBy / STRecTotStandBy) & "</td>")
    '                End If
    '                If STRecTotBrief = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotBrief / STRecTotBrief) & "</td>")
    '                End If
    '                If STRecTotWarpUp = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotWarpUp / STRecTotWarpUp) & "</td>")
    '                End If
    '                If STRecTotConsult = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotConsult / STRecTotConsult) & "</td>")
    '                End If
    '                If STRecTotOtherProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherProd / STRecTotOtherProd) & "</td>")
    '                End If

    '                If STRecTotLunch = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLunch / STRecTotLunch) & "</td>")
    '                End If
    '                If STRecTotLeave = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeave / STRecTotLeave) & "</td>")
    '                End If
    '                If STRecTotChangeCounter = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotChangeCounter / STRecTotChangeCounter) & "</td>")
    '                End If
    '                If STRecTotHome = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotHome / STRecTotHome) & "</td>")
    '                End If
    '                If STRecTotMiniBreak = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotMiniBreak / STRecTotMiniBreak) & "</td>")
    '                End If
    '                If STRecTotRestRoom = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotRestRoom / STRecTotRestRoom) & "</td>")
    '                End If
    '                If STRecTotOtherNonProd = 0 Then
    '                    ret.Append("        <td align='right'>00:00:00</td>")
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherNonProd / STRecTotOtherNonProd) & "</td>")
    '                End If
    '                ret.Append("    </tr>")

    '                STTotTotalTime = 0
    '                STTotProductivity = 0
    '                STTotNonProductivity = 0
    '                STTotE = 0
    '                STTotS = 0
    '                STTotLeasrning = 0
    '                STTotStandBy = 0
    '                STTotBrief = 0
    '                STTotWarpUp = 0
    '                STTotConsult = 0
    '                STTotOtherProd = 0
    '                STTotLunch = 0
    '                STTotLeave = 0
    '                STTotChangeCounter = 0
    '                STTotHome = 0
    '                STTotMiniBreak = 0
    '                STTotRestRoom = 0
    '                STTotOtherNonProd = 0

    '                STRecTotTotalTime = 0
    '                STRecTotProductivity = 0
    '                STRecTotNonProductivity = 0
    '                STRecTotE = 0
    '                STRecTotS = 0
    '                STRecTotLearning = 0
    '                STRecTotStandBy = 0
    '                STRecTotBrief = 0
    '                STRecTotWarpUp = 0
    '                STRecTotConsult = 0
    '                STRecTotOtherProd = 0
    '                STRecTotLunch = 0
    '                STRecTotLeave = 0
    '                STRecTotChangeCounter = 0
    '                STRecTotHome = 0
    '                STRecTotMiniBreak = 0
    '                STRecTotRestRoom = 0
    '                STRecTotOtherNonProd = 0
    '            End If
    '            '***************************************************
    '        Next
    '        '******************* Grand Total *******************
    '        ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
    '        ret.Append("        <td align='center' colspan='6' >Grand Total</td>")
    '        If GTRecTotTotalTime = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / GTRecTotTotalTime) & "</td>")
    '        End If
    '        If GTRecTotProductivity = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / GTRecTotProductivity) & "</td>")
    '        End If
    '        If GTRecTotNonProductivity = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / GTRecTotNonProductivity) & "</td>")
    '        End If
    '        If GTRecTotE = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / GTRecTotE) & "</td>")
    '        End If
    '        If GTRecTotS = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / GTRecTotS) & "</td>")
    '        End If
    '        If GTRecTotLearning = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / GTRecTotLearning) & "</td>")
    '        End If
    '        If GTRecTotStandBy = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / GTRecTotStandBy) & "</td>")
    '        End If
    '        If GTRecTotBrief = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / GTRecTotBrief) & "</td>")
    '        End If
    '        If GTRecTotWarpUp = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / GTRecTotWarpUp) & "</td>")
    '        End If
    '        If GTRecTotConsult = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / GTRecTotConsult) & "</td>")
    '        End If
    '        If GTRecTotOtherProd = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / GTRecTotOtherProd) & "</td>")
    '        End If

    '        If GTRecTotLunch = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / GTRecTotLunch) & "</td>")
    '        End If
    '        If GTRecTotLeave = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / GTRecTotLeave) & "</td>")
    '        End If
    '        If GTRecTotChangeCounter = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / GTRecTotChangeCounter) & "</td>")
    '        End If
    '        If GTRecTotHome = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / GTRecTotHome) & "</td>")
    '        End If
    '        If GTRecTotMiniBreak = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / GTRecTotMiniBreak) & "</td>")
    '        End If
    '        If GTRecTotRestRoom = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / GTRecTotRestRoom) & "</td>")
    '        End If
    '        If GTRecTotOtherNonProd = 0 Then
    '            ret.Append("        <td align='right'>00:00:00</td>")
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / GTRecTotOtherNonProd) & "</td>")
    '        End If
    '        ret.Append("    </tr>")
    '        ret.Append("</table>")
    '        lblReportDesc.Text = ret.ToString
    '        ret = Nothing
    '    End If
    '    dt.Dispose()
    '    eng = Nothing
    'End Sub

  
End Class
