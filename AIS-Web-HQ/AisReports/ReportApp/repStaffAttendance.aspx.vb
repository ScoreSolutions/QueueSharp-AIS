Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repStaffAttendance
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "StaffAttendanceReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
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
            Dim para As New CenParaDB.ReportCriteria.StaffAttendancePara
            para.SHOP_ID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            RenderReportByDate(para)
            para = Nothing
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.StaffAttendancePara
            para.SHOP_ID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            RenderReportByWeek(para)
            para = Nothing
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.StaffAttendancePara
            para.SHOP_ID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            RenderReportByMonth(para)
            para = Nothing
        End If

        If lblReportDesc.Text <> "" Then
            lblerror.Visible = False
        End If
        
    End Sub
    Private Sub RenderReportByMonth(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
        Dim eng As New Engine.Reports.RepStaffAttendanceENG

        Dim dt As DataTable = eng.GetReportDataByMonth(para)
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
            ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            'ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-In</td>" & vbNewLine)
            'ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-Out</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Total Time</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Productivity(HR)</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Non-Productivity(HR)</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Estimated OT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Service Time</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Learning</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Stand by</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Brief</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Wrap up</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Consult</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Other Productivity</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Lunch</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Leave</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Change Counter</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Home</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Mini Break</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Rest Room</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Other Non-Productivity</td>")
            ret.Append("    </tr>")

            Dim GTTotTotalTime As Long = 0
            Dim GTTotProductivity As Long = 0
            Dim GTTotNonProductivity As Long = 0
            Dim GTTotE As Long = 0
            Dim GTTotS As Long = 0
            Dim GTTotLearning As Long = 0
            Dim GTTotStandBy As Long = 0
            Dim GTTotBrief As Long = 0
            Dim GTTotWarpUp As Long = 0
            Dim GTTotConsult As Long = 0
            Dim GTTotOtherProd As Long = 0
            Dim GTTotLunch As Long = 0
            Dim GTTotLeave As Long = 0
            Dim GTTotChangeCounter As Long = 0
            Dim GTTotHome As Long = 0
            Dim GTTotMiniBreak As Long = 0
            Dim GTTotRestRoom As Long = 0
            Dim GTTotOtherNonProd As Long = 0
            Dim GTTotWorkingDay As Long = 0

            Dim GTTotCount_productivity As Long = 0
            Dim GTTotCount_non_productivity As Long = 0
            Dim GTTotCount_est_ot As Long = 0
            Dim GTTotCount_prod_learning As Long = 0
            Dim GTTotCount_prod_stand_by As Long = 0
            Dim GTTotCount_prod_brief As Long = 0
            Dim GTTotCount_prod_warp_up As Long = 0
            Dim GTTotCount_prod_consult As Long = 0
            Dim GTTotCount_prod_other As Long = 0
            Dim GTTotCount_nprod_lunch As Long = 0
            Dim GTTotCount_nprod_leave As Long = 0
            Dim GTTotCount_nprod_change_counter As Long = 0
            Dim GTTotCount_nprod_home As Long = 0
            Dim GTTotCount_nprod_mini_break As Long = 0
            Dim GTTotCount_nprod_restroom As Long = 0
            Dim GTTotCount_nprod_other As Long = 0


            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TotTotalTime As Long = 0
                Dim TotProductivity As Long = 0
                Dim TotNonProductivity As Long = 0
                Dim TotE As Long = 0
                Dim TotS As Long = 0
                Dim TotLearning As Long = 0
                Dim TotStandBy As Long = 0
                Dim TotBrief As Long = 0
                Dim TotWarpUp As Long = 0
                Dim TotConsult As Long = 0
                Dim TotOtherProd As Long = 0
                Dim TotLunch As Long = 0
                Dim TotLeave As Long = 0
                Dim TotChangeCounter As Long = 0
                Dim TotHome As Long = 0
                Dim TotMiniBreak As Long = 0
                Dim TotRestRoom As Long = 0
                Dim TotOtherNonProd As Long = 0
                Dim TotWorkingDay As Long = 0

                Dim TotCount_productivity As Long = 0
                Dim TotCount_non_productivity As Long = 0
                Dim TotCount_est_ot As Long = 0
                Dim TotCount_prod_learning As Long = 0
                Dim TotCount_prod_stand_by As Long = 0
                Dim TotCount_prod_brief As Long = 0
                Dim TotCount_prod_warp_up As Long = 0
                Dim TotCount_prod_consult As Long = 0
                Dim TotCount_prod_other As Long = 0
                Dim TotCount_nprod_lunch As Long = 0
                Dim TotCount_nprod_leave As Long = 0
                Dim TotCount_nprod_change_counter As Long = 0
                Dim TotCount_nprod_home As Long = 0
                Dim TotCount_nprod_mini_break As Long = 0
                Dim TotCount_nprod_restroom As Long = 0
                Dim TotCount_nprod_other As Long = 0


                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim mDt As New DataTable
                mDt = dt.DefaultView.ToTable(True, "month_no", "show_month", "show_year")
                For Each mDr As DataRow In mDt.Rows
                    Dim STSumTotalTime As Long = 0
                    Dim STSumServiceTime As Long = 0
                    Dim STSumProductivity As Long = 0
                    Dim STSumNonProductivity As Long = 0
                    Dim STSumE As Long = 0
                    Dim STSumS As Long = 0
                    Dim STSumLeasrning As Long = 0
                    Dim STSumStandBy As Long = 0
                    Dim STSumBrief As Long = 0
                    Dim STSumWarpUp As Long = 0
                    Dim STSumConsult As Long = 0
                    Dim STSumOtherProd As Long = 0
                    Dim STSumLunch As Long = 0
                    Dim STSumLeave As Long = 0
                    Dim STSumChangeCounter As Long = 0
                    Dim STSumHome As Long = 0
                    Dim STSumMiniBreak As Long = 0
                    Dim STSumRestRoom As Long = 0
                    Dim STSumOtherNonProd As Long = 0
                    Dim STSumWorkingDay As Long = 0

                    Dim STCount_productivity As Long = 0
                    Dim STCount_non_productivity As Long = 0
                    Dim STCount_est_ot As Long = 0
                    Dim STCount_prod_learning As Long = 0
                    Dim STCount_prod_stand_by As Long = 0
                    Dim STCount_prod_brief As Long = 0
                    Dim STCount_prod_warp_up As Long = 0
                    Dim STCount_prod_consult As Long = 0
                    Dim STCount_prod_other As Long = 0
                    Dim STCount_nprod_lunch As Long = 0
                    Dim STCount_nprod_leave As Long = 0
                    Dim STCount_nprod_change_counter As Long = 0
                    Dim STCount_nprod_home As Long = 0
                    Dim STCount_nprod_mini_break As Long = 0
                    Dim STCount_nprod_restroom As Long = 0
                    Dim STCount_nprod_other As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and month_no='" & mDr("month_no") & "'"
                    Dim stDt As New DataTable
                    stDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "username", "staff_name")
                    For Each stDr As DataRow In stDt.Rows
                        dt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "' and month_no='" & mDr("month_no") & "' and show_year='" & mDr("show_year") & "' and shop_id='" & sDr("shop_id") & "'"
                        For Each dr As DataRowView In dt.DefaultView
                            ret.Append("    <tr>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("shop_name_en") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("show_month") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("show_year") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
                            'ret.Append("        <td align='right'>" & dr("log_in") & "</td>")
                            'ret.Append("        <td align='right'>" & dr("log_out") & "</td>")
                            ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
                            ret.Append("        <td align='right'>" & dr("productivity") & "</td>")
                            ret.Append("        <td align='right'> " & dr("non_productivity") & "</td>")

                            Dim drTotalTime As Long = 0
                            If Convert.IsDBNull(dr("total_time")) = False Then
                                drTotalTime = ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                                TotTotalTime += Convert.ToInt64(dr("sum_total_time"))
                                STSumTotalTime += Convert.ToInt64(dr("sum_total_time"))
                                GTTotTotalTime += Convert.ToInt64(dr("sum_total_time"))
                            End If

                            If Convert.IsDBNull(dr("working_day")) = False Then
                                TotWorkingDay += Convert.ToInt64(dr("working_day"))
                                STSumWorkingDay += Convert.ToInt64(dr("working_day"))
                                GTTotWorkingDay += Convert.ToInt64(dr("working_day"))
                            End If

                            If Convert.IsDBNull(dr("est_ot")) = False Then
                                ret.Append("    <td align='right'> " & dr("est_ot") & "</td>")
                                TotE += Convert.ToInt64(dr("sum_est_ot"))
                                STSumE += Convert.ToInt64(dr("sum_est_ot"))
                                GTTotE += Convert.ToInt64(dr("sum_est_ot"))
                            Else
                                ret.Append("    <td align='right'>00:00:00</td>")
                            End If

                            If Convert.IsDBNull(dr("service_time")) = False Then
                                ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
                                STSumServiceTime += Convert.ToInt64(dr("sum_service_time"))
                                TotS += Convert.ToInt64(dr("sum_service_time"))
                                GTTotS += Convert.ToInt64(dr("sum_service_time"))
                            Else
                                ret.Append("    <td align='right'>00:00:00</td>")
                            End If

                            'If drTotalTime > (450 * 60) Then
                            '    ret.Append("    <td align='right'> " & ReportsENG.GetFormatTimeFromSec(drTotalTime - (450 * 60)) & "</td>")

                            '    TotE += (drTotalTime - (450 * 60))
                            '    STSumE += (drTotalTime - (450 * 60))
                            '    GTTotE += (drTotalTime - (450 * 60))
                            'Else
                            '    ret.Append("    <td align='right'>00:00:00</td>")
                            'End If


                            'If Convert.IsDBNull(dr("service_time")) = False Then
                            '    ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
                            '    STSumServiceTime += Convert.ToInt64(dr("sum_service_time"))
                            '    TotS += Convert.ToInt64(dr("sum_service_time"))
                            '    GTTotS += Convert.ToInt64(dr("sum_service_time"))
                            'Else
                            '    ret.Append("    <td align='right'>00:00:00</td>")
                            'End If

                            If Convert.ToInt64(dr("sum_productivity")) > 0 Then
                                TotProductivity += Convert.ToInt64(dr("sum_productivity"))
                                STSumProductivity += Convert.ToInt64(dr("sum_productivity"))
                                GTTotProductivity += Convert.ToInt64(dr("sum_productivity"))
                            End If
                            If Convert.ToInt64(dr("sum_non_productivity")) > 0 Then
                                TotNonProductivity += Convert.ToInt64(dr("sum_non_productivity"))
                                STSumNonProductivity += Convert.ToInt64(dr("sum_non_productivity"))
                                GTTotNonProductivity += Convert.ToInt64(dr("sum_non_productivity"))
                            End If
                           

                            '#### Productivity
                            ret.Append("        <td align='right' >" & dr("prod_learning") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_learning")) > 0 Then
                                TotLearning += Convert.ToInt64(dr("sum_prod_learning"))
                                STSumLeasrning += Convert.ToInt64(dr("sum_prod_learning"))
                                GTTotLearning += Convert.ToInt64(dr("sum_prod_learning"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_stand_by") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_stand_by")) > 0 Then
                                TotStandBy += Convert.ToInt64(dr("sum_prod_stand_by"))
                                STSumStandBy += Convert.ToInt64(dr("sum_prod_stand_by"))
                                GTTotStandBy += Convert.ToInt64(dr("sum_prod_stand_by"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_brief") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_brief")) > 0 Then
                                TotBrief += Convert.ToInt64(dr("sum_prod_brief"))
                                STSumBrief += Convert.ToInt64(dr("sum_prod_brief"))
                                GTTotBrief += Convert.ToInt64(dr("sum_prod_brief"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_warp_up") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_warp_up")) > 0 Then
                                TotWarpUp += Convert.ToInt64(dr("sum_prod_warp_up"))
                                STSumWarpUp += Convert.ToInt64(dr("sum_prod_warp_up"))
                                GTTotWarpUp += Convert.ToInt64(dr("sum_prod_warp_up"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_consult") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_consult")) > 0 Then
                                TotConsult += Convert.ToInt64(dr("sum_prod_consult"))
                                STSumConsult += Convert.ToInt64(dr("sum_prod_consult"))
                                GTTotConsult += Convert.ToInt64(dr("sum_prod_consult"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_other") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_other")) > 0 Then
                                TotOtherProd += Convert.ToInt64(dr("sum_prod_other"))
                                STSumOtherProd += Convert.ToInt64(dr("sum_prod_other"))
                                GTTotOtherProd += Convert.ToInt64(dr("sum_prod_other"))
                            End If

                            '### Non Productivity
                            ret.Append("        <td align='right' >" & dr("nprod_lunch") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_lunch")) > 0 Then
                                TotLunch += Convert.ToInt64(dr("sum_nprod_lunch"))
                                STSumLunch += Convert.ToInt64(dr("sum_nprod_lunch"))
                                GTTotLunch += Convert.ToInt64(dr("sum_nprod_lunch"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_leave") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_leave")) > 0 Then
                                TotLeave += Convert.ToInt64(dr("sum_nprod_leave"))
                                STSumLeave += Convert.ToInt64(dr("sum_nprod_leave"))
                                GTTotLeave += Convert.ToInt64(dr("sum_nprod_leave"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_change_counter") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_change_counter")) > 0 Then
                                TotChangeCounter += Convert.ToInt64(dr("sum_nprod_change_counter"))
                                STSumChangeCounter += Convert.ToInt64(dr("sum_nprod_change_counter"))
                                GTTotChangeCounter += Convert.ToInt64(dr("sum_nprod_change_counter"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_home") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_home")) > 0 Then
                                TotHome += Convert.ToInt64(dr("sum_nprod_home"))
                                STSumHome += Convert.ToInt64(dr("sum_nprod_home"))
                                GTTotHome += Convert.ToInt64(dr("sum_nprod_home"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_mini_break") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_mini_break")) > 0 Then
                                TotMiniBreak += Convert.ToInt64(dr("sum_nprod_mini_break"))
                                STSumMiniBreak += Convert.ToInt64(dr("sum_nprod_mini_break"))
                                GTTotMiniBreak += Convert.ToInt64(dr("sum_nprod_mini_break"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_restroom") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_restroom")) > 0 Then
                                TotRestRoom += Convert.ToInt64(dr("sum_nprod_restroom"))
                                STSumRestRoom += Convert.ToInt64(dr("sum_nprod_restroom"))
                                GTTotRestRoom += Convert.ToInt64(dr("sum_nprod_restroom"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_other") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_other")) > 0 Then
                                TotOtherNonProd += Convert.ToInt64(dr("sum_nprod_other"))
                                STSumOtherNonProd += Convert.ToInt64(dr("sum_nprod_other"))
                                GTTotOtherNonProd += Convert.ToInt64(dr("sum_nprod_other"))
                            End If

                            '#### Sum CountAll
                            If Convert.ToInt64(dr("count_productivity")) > 0 Then
                                STCount_productivity += Convert.ToInt64(dr("count_productivity"))
                                TotCount_productivity += Convert.ToInt64(dr("count_productivity"))
                                GTTotCount_productivity += Convert.ToInt64(dr("count_productivity"))
                            End If

                            If Convert.ToInt64(dr("count_non_productivity")) > 0 Then
                                STCount_non_productivity += Convert.ToInt64(dr("count_non_productivity"))
                                TotCount_non_productivity += Convert.ToInt64(dr("count_non_productivity"))
                                GTTotCount_non_productivity += Convert.ToInt64(dr("count_non_productivity"))
                            End If

                            If Convert.ToInt64(dr("count_est_ot")) > 0 Then
                                STCount_est_ot += Convert.ToInt64(dr("count_est_ot"))
                                TotCount_est_ot += Convert.ToInt64(dr("count_est_ot"))
                                GTTotCount_est_ot += Convert.ToInt64(dr("count_est_ot"))
                            End If
                            If Convert.ToInt64(dr("count_prod_learning")) > 0 Then
                                STCount_prod_learning += Convert.ToInt64(dr("count_prod_learning"))
                                TotCount_prod_learning += Convert.ToInt64(dr("count_prod_learning"))
                                GTTotCount_prod_learning += Convert.ToInt64(dr("count_prod_learning"))
                            End If
                            If Convert.ToInt64(dr("count_prod_stand_by")) > 0 Then
                                STCount_prod_stand_by += Convert.ToInt64(dr("count_prod_stand_by"))
                                TotCount_prod_stand_by += Convert.ToInt64(dr("count_prod_stand_by"))
                                GTTotCount_prod_stand_by += Convert.ToInt64(dr("count_prod_stand_by"))
                            End If

                            If Convert.ToInt64(dr("count_prod_brief")) > 0 Then
                                STCount_prod_brief += Convert.ToInt64(dr("count_prod_brief"))
                                TotCount_prod_brief += Convert.ToInt64(dr("count_prod_brief"))
                                GTTotCount_prod_brief += Convert.ToInt64(dr("count_prod_brief"))
                            End If

                            If Convert.ToInt64(dr("count_prod_warp_up")) > 0 Then
                                STCount_prod_warp_up += Convert.ToInt64(dr("count_prod_warp_up"))
                                TotCount_prod_warp_up += Convert.ToInt64(dr("count_prod_warp_up"))
                                GTTotCount_prod_warp_up += Convert.ToInt64(dr("count_prod_warp_up"))
                            End If
                            If Convert.ToInt64(dr("count_prod_consult")) > 0 Then
                                STCount_prod_consult += Convert.ToInt64(dr("count_prod_consult"))
                                TotCount_prod_consult += Convert.ToInt64(dr("count_prod_consult"))
                                GTTotCount_prod_consult += Convert.ToInt64(dr("count_prod_consult"))
                            End If
                            If Convert.ToInt64(dr("count_prod_other")) > 0 Then
                                STCount_prod_other += Convert.ToInt64(dr("count_prod_other"))
                                TotCount_prod_other += Convert.ToInt64(dr("count_prod_other"))
                                GTTotCount_prod_other += Convert.ToInt64(dr("count_prod_other"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_lunch")) > 0 Then
                                STCount_nprod_lunch += Convert.ToInt64(dr("count_nprod_lunch"))
                                TotCount_nprod_lunch += Convert.ToInt64(dr("count_nprod_lunch"))
                                GTTotCount_nprod_lunch += Convert.ToInt64(dr("count_nprod_lunch"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_leave")) > 0 Then
                                STCount_nprod_leave += Convert.ToInt64(dr("count_nprod_leave"))
                                TotCount_nprod_leave += Convert.ToInt64(dr("count_nprod_leave"))
                                GTTotCount_nprod_leave += Convert.ToInt64(dr("count_nprod_leave"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_change_counter")) > 0 Then
                                STCount_nprod_change_counter += Convert.ToInt64(dr("count_nprod_change_counter"))
                                TotCount_nprod_change_counter += Convert.ToInt64(dr("count_nprod_change_counter"))
                                GTTotCount_nprod_change_counter += Convert.ToInt64(dr("count_nprod_change_counter"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_home")) > 0 Then
                                STCount_nprod_home += Convert.ToInt64(dr("count_nprod_home"))
                                TotCount_nprod_home += Convert.ToInt64(dr("count_nprod_home"))
                                GTTotCount_nprod_home += Convert.ToInt64(dr("count_nprod_home"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_mini_break")) > 0 Then
                                STCount_nprod_mini_break += Convert.ToInt64(dr("count_nprod_mini_break"))
                                TotCount_nprod_mini_break += Convert.ToInt64(dr("count_nprod_mini_break"))
                                GTTotCount_nprod_mini_break += Convert.ToInt64(dr("count_nprod_mini_break"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_restroom")) > 0 Then
                                STCount_nprod_restroom += Convert.ToInt64(dr("count_nprod_restroom"))
                                TotCount_nprod_restroom += Convert.ToInt64(dr("count_nprod_restroom"))
                                GTTotCount_nprod_restroom += Convert.ToInt64(dr("count_nprod_restroom"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_other")) > 0 Then
                                STCount_nprod_other += Convert.ToInt64(dr("count_nprod_other"))
                                TotCount_nprod_other += Convert.ToInt64(dr("count_nprod_other"))
                                GTTotCount_nprod_other += Convert.ToInt64(dr("count_nprod_other"))
                            End If
                            '#### End Sum CountnAll

                            ret.Append("    </tr>")
                        Next
                    Next
                    stDt.Dispose()

                    'Total By Month
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
                    'ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                    'ret.Append("        <td align='center' >" & mDr("show_month") & "</td>")
                    'ret.Append("        <td align='center' >" & mDr("show_year") & "</td>")
                    ret.Append("        <td align='center' colspan='3' >Sub Total </td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumTotalTime / IIf(STSumWorkingDay = 0, 1, STSumWorkingDay)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumProductivity / IIf(STCount_productivity = 0, 1, STCount_productivity)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumNonProductivity / IIf(STCount_non_productivity = 0, 1, STCount_non_productivity)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumE / IIf(STCount_est_ot = 0, 1, STCount_est_ot)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumServiceTime / IIf(STSumWorkingDay = 0, 1, STSumWorkingDay)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumLeasrning / IIf(STCount_prod_learning = 0, 1, STCount_prod_learning)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumStandBy / IIf(STCount_prod_stand_by = 0, 1, STCount_prod_stand_by)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumBrief / IIf(STCount_prod_brief = 0, 1, STCount_prod_brief)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumWarpUp / IIf(STCount_prod_warp_up = 0, 1, STCount_prod_warp_up)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumConsult / IIf(STCount_prod_consult = 0, 1, STCount_prod_consult)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumOtherProd / IIf(STCount_prod_other = 0, 1, STCount_prod_other)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumLunch / IIf(STCount_nprod_lunch = 0, 1, STCount_nprod_lunch)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumLeave / IIf(STCount_nprod_leave = 0, 1, STCount_nprod_leave)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumChangeCounter / IIf(STCount_nprod_change_counter = 0, 1, STCount_nprod_change_counter)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumHome / IIf(STCount_nprod_home = 0, 1, STCount_nprod_home)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumMiniBreak / IIf(STCount_nprod_mini_break = 0, 1, STCount_nprod_mini_break)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumRestRoom / IIf(STCount_nprod_restroom = 0, 1, STCount_nprod_restroom)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumOtherNonProd / IIf(STCount_nprod_other = 0, 1, STCount_nprod_other)) & "</td>")
                    ret.Append("    </tr>")
                Next
                mDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='3' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / IIf(TotWorkingDay = 0, 1, TotWorkingDay)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / IIf(TotCount_productivity = 0, 1, TotCount_productivity)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / IIf(TotCount_non_productivity = 0, 1, TotCount_non_productivity)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / IIf(TotCount_est_ot = 0, 1, TotCount_est_ot)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / IIf(TotWorkingDay = 0, 1, TotWorkingDay)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / IIf(TotCount_prod_learning = 0, 1, TotCount_prod_learning)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / IIf(TotCount_prod_stand_by = 0, 1, TotCount_prod_stand_by)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / IIf(TotCount_prod_brief = 0, 1, TotCount_prod_brief)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / IIf(TotCount_prod_warp_up = 0, 1, TotCount_prod_warp_up)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / IIf(TotCount_prod_consult = 0, 1, TotCount_prod_consult)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / IIf(TotCount_prod_other = 0, 1, TotCount_prod_other)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / IIf(TotCount_nprod_lunch = 0, 1, TotCount_nprod_lunch)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / IIf(TotCount_nprod_leave = 0, 1, TotCount_nprod_leave)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / IIf(TotCount_nprod_change_counter = 0, 1, TotCount_nprod_change_counter)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / IIf(TotCount_nprod_home = 0, 1, TotCount_nprod_home)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / IIf(TotCount_nprod_mini_break = 0, 1, TotCount_nprod_mini_break)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / IIf(TotCount_nprod_restroom = 0, 1, TotCount_nprod_restroom)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / IIf(TotCount_nprod_other = 0, 1, TotCount_nprod_other)) & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            '******************* Grand Total *******************
            ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='3' >Grand Total</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / IIf(GTTotWorkingDay = 0, 1, GTTotWorkingDay)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / IIf(GTTotCount_productivity = 0, 1, GTTotCount_productivity)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / IIf(GTTotCount_non_productivity = 0, 1, GTTotCount_non_productivity)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / IIf(GTTotCount_est_ot = 0, 1, GTTotCount_est_ot)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / IIf(GTTotWorkingDay = 0, 1, GTTotWorkingDay)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / IIf(GTTotCount_prod_learning = 0, 1, GTTotCount_prod_learning)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / IIf(GTTotCount_prod_stand_by = 0, 1, GTTotCount_prod_stand_by)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / IIf(GTTotCount_prod_brief = 0, 1, GTTotCount_prod_brief)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / IIf(GTTotCount_prod_warp_up = 0, 1, GTTotCount_prod_warp_up)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / IIf(GTTotCount_prod_consult = 0, 1, GTTotCount_prod_consult)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / IIf(GTTotCount_prod_other = 0, 1, GTTotCount_prod_other)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / IIf(GTTotCount_nprod_lunch = 0, 1, GTTotCount_nprod_lunch)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / IIf(GTTotCount_nprod_leave = 0, 1, GTTotCount_nprod_leave)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / IIf(GTTotCount_nprod_change_counter = 0, 1, GTTotCount_nprod_change_counter)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / IIf(GTTotCount_nprod_home = 0, 1, GTTotCount_nprod_home)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / IIf(GTTotCount_nprod_mini_break = 0, 1, GTTotCount_nprod_mini_break)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / IIf(GTTotCount_nprod_restroom = 0, 1, GTTotCount_nprod_restroom)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / IIf(GTTotCount_nprod_other = 0, 1, GTTotCount_nprod_other)) & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub RenderReportByWeek(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
        Dim eng As New Engine.Reports.RepStaffAttendanceENG

        Dim dt As DataTable = eng.GetReportDataByWeek(para)
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
            ret.Append("        <td align='center' style='color: #ffffff;' >Week No.</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            'ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-In</td>" & vbNewLine)
            'ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Log-Out</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Total Time</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Productivity(HR)</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Non-Productivity(HR)</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Estimated OT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AVG.Service Time</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Learning</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Stand by</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Brief</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Wrap up</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Productivity Consult</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Other Productivity</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Lunch</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Leave</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Change Counter</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Home</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Mini Break</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Non-Productivity Rest Room</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AVG.Other Non-Productivity</td>")
            ret.Append("    </tr>")

            Dim GTTotTotalTime As Long = 0
            Dim GTTotProductivity As Long = 0
            Dim GTTotNonProductivity As Long = 0
            Dim GTTotE As Long = 0
            Dim GTTotS As Long = 0
            Dim GTTotLearning As Long = 0
            Dim GTTotStandBy As Long = 0
            Dim GTTotBrief As Long = 0
            Dim GTTotWarpUp As Long = 0
            Dim GTTotConsult As Long = 0
            Dim GTTotOtherProd As Long = 0
            Dim GTTotLunch As Long = 0
            Dim GTTotLeave As Long = 0
            Dim GTTotChangeCounter As Long = 0
            Dim GTTotHome As Long = 0
            Dim GTTotMiniBreak As Long = 0
            Dim GTTotRestRoom As Long = 0
            Dim GTTotOtherNonProd As Long = 0
            Dim GTTotWorkingDay As Long = 0

            Dim GTTotCount_productivity As Long = 0
            Dim GTTotCount_non_productivity As Long = 0
            Dim GTTotCount_est_ot As Long = 0
            Dim GTTotCount_prod_learning As Long = 0
            Dim GTTotCount_prod_stand_by As Long = 0
            Dim GTTotCount_prod_brief As Long = 0
            Dim GTTotCount_prod_warp_up As Long = 0
            Dim GTTotCount_prod_consult As Long = 0
            Dim GTTotCount_prod_other As Long = 0
            Dim GTTotCount_nprod_lunch As Long = 0
            Dim GTTotCount_nprod_leave As Long = 0
            Dim GTTotCount_nprod_change_counter As Long = 0
            Dim GTTotCount_nprod_home As Long = 0
            Dim GTTotCount_nprod_mini_break As Long = 0
            Dim GTTotCount_nprod_restroom As Long = 0
            Dim GTTotCount_nprod_other As Long = 0

          

            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TotTotalTime As Long = 0
                Dim TotProductivity As Long = 0
                Dim TotNonProductivity As Long = 0
                Dim TotE As Long = 0
                Dim TotS As Long = 0
                Dim TotLearning As Long = 0
                Dim TotStandBy As Long = 0
                Dim TotBrief As Long = 0
                Dim TotWarpUp As Long = 0
                Dim TotConsult As Long = 0
                Dim TotOtherProd As Long = 0
                Dim TotLunch As Long = 0
                Dim TotLeave As Long = 0
                Dim TotChangeCounter As Long = 0
                Dim TotHome As Long = 0
                Dim TotMiniBreak As Long = 0
                Dim TotRestRoom As Long = 0
                Dim TotOtherNonProd As Long = 0
                Dim TotWorkingDay As Long = 0

                Dim TotCount_productivity As Long = 0
                Dim TotCount_non_productivity As Long = 0
                Dim TotCount_est_ot As Long = 0
                Dim TotCount_prod_learning As Long = 0
                Dim TotCount_prod_stand_by As Long = 0
                Dim TotCount_prod_brief As Long = 0
                Dim TotCount_prod_warp_up As Long = 0
                Dim TotCount_prod_consult As Long = 0
                Dim TotCount_prod_other As Long = 0
                Dim TotCount_nprod_lunch As Long = 0
                Dim TotCount_nprod_leave As Long = 0
                Dim TotCount_nprod_change_counter As Long = 0
                Dim TotCount_nprod_home As Long = 0
                Dim TotCount_nprod_mini_break As Long = 0
                Dim TotCount_nprod_restroom As Long = 0
                Dim TotCount_nprod_other As Long = 0


                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim wDt As New DataTable
                wDt = dt.DefaultView.ToTable(True, "week_of_year", "show_year")
                For Each wDr As DataRow In wDt.Rows
                    Dim STSumTotalTime As Long = 0
                    Dim STSumProductivity As Long = 0
                    Dim STSumNonProductivity As Long = 0
                    Dim STSumE As Long = 0
                    Dim STSumServiceTime As Long = 0
                    Dim STSumLeasrning As Long = 0
                    Dim STSumStandBy As Long = 0
                    Dim STSumBrief As Long = 0
                    Dim STSumWarpUp As Long = 0
                    Dim STSumConsult As Long = 0
                    Dim STSumOtherProd As Long = 0
                    Dim STSumLunch As Long = 0
                    Dim STSumLeave As Long = 0
                    Dim STSumChangeCounter As Long = 0
                    Dim STSumHome As Long = 0
                    Dim STSumMiniBreak As Long = 0
                    Dim STSumRestRoom As Long = 0
                    Dim STSumOtherNonProd As Long = 0
                    Dim STSumWorkingDay As Long = 0

                    Dim STCount_productivity As Long = 0
                    Dim STCount_non_productivity As Long = 0
                    Dim STCount_est_ot As Long = 0
                    Dim STCount_prod_learning As Long = 0
                    Dim STCount_prod_stand_by As Long = 0
                    Dim STCount_prod_brief As Long = 0
                    Dim STCount_prod_warp_up As Long = 0
                    Dim STCount_prod_consult As Long = 0
                    Dim STCount_prod_other As Long = 0
                    Dim STCount_nprod_lunch As Long = 0
                    Dim STCount_nprod_leave As Long = 0
                    Dim STCount_nprod_change_counter As Long = 0
                    Dim STCount_nprod_home As Long = 0
                    Dim STCount_nprod_mini_break As Long = 0
                    Dim STCount_nprod_restroom As Long = 0
                    Dim STCount_nprod_other As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and week_of_year='" & wDr("week_of_year") & "'"
                    Dim stDt As New DataTable
                    stDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "username", "staff_name")
                    For Each stDr As DataRow In stDt.Rows
                        dt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "' and week_of_year='" & wDr("week_of_year") & "' and show_year='" & wDr("show_year") & "' and shop_id='" & sDr("shop_id") & "'"
                        For Each dr As DataRowView In dt.DefaultView
                            ret.Append("    <tr>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("shop_name_en") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("week_of_year") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("show_year") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
                            'ret.Append("        <td align='right'>" & dr("log_in") & "</td>")
                            'ret.Append("        <td align='right'>" & dr("log_out") & "</td>")
                            ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
                            ret.Append("        <td align='right'>" & dr("productivity") & "</td>")
                            ret.Append("        <td align='right'> " & dr("non_productivity") & "</td>")


                            Dim drTotalTime As Long = 0
                            If Convert.IsDBNull(dr("total_time")) = False Then
                                drTotalTime = ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                                TotTotalTime += Convert.ToInt64(dr("sum_total_time"))
                                STSumTotalTime += Convert.ToInt64(dr("sum_total_time"))
                                GTTotTotalTime += Convert.ToInt64(dr("sum_total_time"))
                            End If

                            If Convert.IsDBNull(dr("working_day")) = False Then
                                TotWorkingDay += Convert.ToInt64(dr("working_day"))
                                STSumWorkingDay += Convert.ToInt64(dr("working_day"))
                                GTTotWorkingDay += Convert.ToInt64(dr("working_day"))
                            End If

                            If Convert.IsDBNull(dr("est_ot")) = False Then
                                ret.Append("    <td align='right'> " & dr("est_ot") & "</td>")
                                TotE += Convert.ToInt64(dr("sum_est_ot"))
                                STSumE += Convert.ToInt64(dr("sum_est_ot"))
                                GTTotE += Convert.ToInt64(dr("sum_est_ot"))
                            Else
                                ret.Append("    <td align='right'>00:00:00</td>")
                            End If

                            If Convert.IsDBNull(dr("service_time")) = False Then
                                ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
                                STSumServiceTime += Convert.ToInt64(dr("sum_service_time"))
                                TotS += Convert.ToInt64(dr("sum_service_time"))
                                GTTotS += Convert.ToInt64(dr("sum_service_time"))
                            Else
                                ret.Append("    <td align='right'>00:00:00</td>")
                            End If

                            If Convert.ToInt64(dr("sum_productivity")) > 0 Then
                                TotProductivity += Convert.ToInt64(dr("sum_productivity"))
                                STSumProductivity += Convert.ToInt64(dr("sum_productivity"))
                                GTTotProductivity += Convert.ToInt64(dr("sum_productivity"))
                            End If
                            If Convert.ToInt64(dr("sum_non_productivity")) > 0 Then
                                TotNonProductivity += Convert.ToInt64(dr("sum_non_productivity"))
                                STSumNonProductivity += Convert.ToInt64(dr("sum_non_productivity"))
                                GTTotNonProductivity += Convert.ToInt64(dr("sum_non_productivity"))
                            End If

                            '#### Productivity
                            ret.Append("        <td align='right' >" & dr("prod_learning") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_learning")) > 0 Then
                                TotLearning += Convert.ToInt64(dr("sum_prod_learning"))
                                STSumLeasrning += Convert.ToInt64(dr("sum_prod_learning"))
                                GTTotLearning += Convert.ToInt64(dr("sum_prod_learning"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_stand_by") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_stand_by")) > 0 Then
                                TotStandBy += Convert.ToInt64(dr("sum_prod_stand_by"))
                                STSumStandBy += Convert.ToInt64(dr("sum_prod_stand_by"))
                                GTTotStandBy += Convert.ToInt64(dr("sum_prod_stand_by"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_brief") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_brief")) > 0 Then
                                TotBrief += Convert.ToInt64(dr("sum_prod_brief"))
                                STSumBrief += Convert.ToInt64(dr("sum_prod_brief"))
                                GTTotBrief += Convert.ToInt64(dr("sum_prod_brief"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_warp_up") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_warp_up")) > 0 Then
                                TotWarpUp += Convert.ToInt64(dr("sum_prod_warp_up"))
                                STSumWarpUp += Convert.ToInt64(dr("sum_prod_warp_up"))
                                GTTotWarpUp += Convert.ToInt64(dr("sum_prod_warp_up"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_consult") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_consult")) > 0 Then
                                TotConsult += Convert.ToInt64(dr("sum_prod_consult"))
                                STSumConsult += Convert.ToInt64(dr("sum_prod_consult"))
                                GTTotConsult += Convert.ToInt64(dr("sum_prod_consult"))
                            End If

                            ret.Append("        <td align='right' >" & dr("prod_other") & "</td>")
                            If Convert.ToInt64(dr("sum_prod_other")) > 0 Then
                                TotOtherProd += Convert.ToInt64(dr("sum_prod_other"))
                                STSumOtherProd += Convert.ToInt64(dr("sum_prod_other"))
                                GTTotOtherProd += Convert.ToInt64(dr("sum_prod_other"))
                            End If

                            '### Non Productivity
                            ret.Append("        <td align='right' >" & dr("nprod_lunch") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_lunch")) > 0 Then
                                TotLunch += Convert.ToInt64(dr("sum_nprod_lunch"))
                                STSumLunch += Convert.ToInt64(dr("sum_nprod_lunch"))
                                GTTotLunch += Convert.ToInt64(dr("sum_nprod_lunch"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_leave") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_leave")) > 0 Then
                                TotLeave += Convert.ToInt64(dr("sum_nprod_leave"))
                                STSumLeave += Convert.ToInt64(dr("sum_nprod_leave"))
                                GTTotLeave += Convert.ToInt64(dr("sum_nprod_leave"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_change_counter") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_change_counter")) > 0 Then
                                TotChangeCounter += Convert.ToInt64(dr("sum_nprod_change_counter"))
                                STSumChangeCounter += Convert.ToInt64(dr("sum_nprod_change_counter"))
                                GTTotChangeCounter += Convert.ToInt64(dr("sum_nprod_change_counter"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_home") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_home")) > 0 Then
                                TotHome += Convert.ToInt64(dr("sum_nprod_home"))
                                STSumHome += Convert.ToInt64(dr("sum_nprod_home"))
                                GTTotHome += Convert.ToInt64(dr("sum_nprod_home"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_mini_break") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_mini_break")) > 0 Then
                                TotMiniBreak += Convert.ToInt64(dr("sum_nprod_mini_break"))
                                STSumMiniBreak += Convert.ToInt64(dr("sum_nprod_mini_break"))
                                GTTotMiniBreak += Convert.ToInt64(dr("sum_nprod_mini_break"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_restroom") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_restroom")) > 0 Then
                                TotRestRoom += Convert.ToInt64(dr("sum_nprod_restroom"))
                                STSumRestRoom += Convert.ToInt64(dr("sum_nprod_restroom"))
                                GTTotRestRoom += Convert.ToInt64(dr("sum_nprod_restroom"))
                            End If

                            ret.Append("        <td align='right' >" & dr("nprod_other") & "</td>")
                            If Convert.ToInt64(dr("sum_nprod_other")) > 0 Then
                                TotOtherNonProd += Convert.ToInt64(dr("sum_nprod_other"))
                                STSumOtherNonProd += Convert.ToInt64(dr("sum_nprod_other"))
                                GTTotOtherNonProd += Convert.ToInt64(dr("sum_nprod_other"))
                            End If


                            '#### Sum CoutAll
                            If Convert.ToInt64(dr("count_productivity")) > 0 Then
                                STCount_productivity += Convert.ToInt64(dr("count_productivity"))
                                TotCount_productivity += Convert.ToInt64(dr("count_productivity"))
                                GTTotCount_productivity += Convert.ToInt64(dr("count_productivity"))
                            End If

                            If Convert.ToInt64(dr("count_non_productivity")) > 0 Then
                                STCount_non_productivity += Convert.ToInt64(dr("count_non_productivity"))
                                TotCount_non_productivity += Convert.ToInt64(dr("count_non_productivity"))
                                GTTotCount_non_productivity += Convert.ToInt64(dr("count_non_productivity"))
                            End If

                            If Convert.ToInt64(dr("count_est_ot")) > 0 Then
                                STCount_est_ot += Convert.ToInt64(dr("count_est_ot"))
                                TotCount_est_ot += Convert.ToInt64(dr("count_est_ot"))
                                GTTotCount_est_ot += Convert.ToInt64(dr("count_est_ot"))
                            End If
                            If Convert.ToInt64(dr("count_prod_learning")) > 0 Then
                                STCount_prod_learning += Convert.ToInt64(dr("count_prod_learning"))
                                TotCount_prod_learning += Convert.ToInt64(dr("count_prod_learning"))
                                GTTotCount_prod_learning += Convert.ToInt64(dr("count_prod_learning"))
                            End If
                            If Convert.ToInt64(dr("count_prod_stand_by")) > 0 Then
                                STCount_prod_stand_by += Convert.ToInt64(dr("count_prod_stand_by"))
                                TotCount_prod_stand_by += Convert.ToInt64(dr("count_prod_stand_by"))
                                GTTotCount_prod_stand_by += Convert.ToInt64(dr("count_prod_stand_by"))
                            End If

                            If Convert.ToInt64(dr("count_prod_brief")) > 0 Then
                                STCount_prod_brief += Convert.ToInt64(dr("count_prod_brief"))
                                TotCount_prod_brief += Convert.ToInt64(dr("count_prod_brief"))
                                GTTotCount_prod_brief += Convert.ToInt64(dr("count_prod_brief"))
                            End If

                            If Convert.ToInt64(dr("count_prod_warp_up")) > 0 Then
                                STCount_prod_warp_up += Convert.ToInt64(dr("count_prod_warp_up"))
                                TotCount_prod_warp_up += Convert.ToInt64(dr("count_prod_warp_up"))
                                GTTotCount_prod_warp_up += Convert.ToInt64(dr("count_prod_warp_up"))
                            End If
                            If Convert.ToInt64(dr("count_prod_consult")) > 0 Then
                                STCount_prod_consult += Convert.ToInt64(dr("count_prod_consult"))
                                TotCount_prod_consult += Convert.ToInt64(dr("count_prod_consult"))
                                GTTotCount_prod_consult += Convert.ToInt64(dr("count_prod_consult"))
                            End If
                            If Convert.ToInt64(dr("count_prod_other")) > 0 Then
                                STCount_prod_other += Convert.ToInt64(dr("count_prod_other"))
                                TotCount_prod_other += Convert.ToInt64(dr("count_prod_other"))
                                GTTotCount_prod_other += Convert.ToInt64(dr("count_prod_other"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_lunch")) > 0 Then
                                STCount_nprod_lunch += Convert.ToInt64(dr("count_nprod_lunch"))
                                TotCount_nprod_lunch += Convert.ToInt64(dr("count_nprod_lunch"))
                                GTTotCount_nprod_lunch += Convert.ToInt64(dr("count_nprod_lunch"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_leave")) > 0 Then
                                STCount_nprod_leave += Convert.ToInt64(dr("count_nprod_leave"))
                                TotCount_nprod_leave += Convert.ToInt64(dr("count_nprod_leave"))
                                GTTotCount_nprod_leave += Convert.ToInt64(dr("count_nprod_leave"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_change_counter")) > 0 Then
                                STCount_nprod_change_counter += Convert.ToInt64(dr("count_nprod_change_counter"))
                                TotCount_nprod_change_counter += Convert.ToInt64(dr("count_nprod_change_counter"))
                                GTTotCount_nprod_change_counter += Convert.ToInt64(dr("count_nprod_change_counter"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_home")) > 0 Then
                                STCount_nprod_home += Convert.ToInt64(dr("count_nprod_home"))
                                TotCount_nprod_home += Convert.ToInt64(dr("count_nprod_home"))
                                GTTotCount_nprod_home += Convert.ToInt64(dr("count_nprod_home"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_mini_break")) > 0 Then
                                STCount_nprod_mini_break += Convert.ToInt64(dr("count_nprod_mini_break"))
                                TotCount_nprod_mini_break += Convert.ToInt64(dr("count_nprod_mini_break"))
                                GTTotCount_nprod_mini_break += Convert.ToInt64(dr("count_nprod_mini_break"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_restroom")) > 0 Then
                                STCount_nprod_restroom += Convert.ToInt64(dr("count_nprod_restroom"))
                                TotCount_nprod_restroom += Convert.ToInt64(dr("count_nprod_restroom"))
                                GTTotCount_nprod_restroom += Convert.ToInt64(dr("count_nprod_restroom"))
                            End If
                            If Convert.ToInt64(dr("count_nprod_other")) > 0 Then
                                STCount_nprod_other += Convert.ToInt64(dr("count_nprod_other"))
                                TotCount_nprod_other += Convert.ToInt64(dr("count_nprod_other"))
                                GTTotCount_nprod_other += Convert.ToInt64(dr("count_nprod_other"))
                            End If
                            '#### End Sum CountAll

                            ret.Append("    </tr>")
                        Next
                    Next
                    stDt.Dispose()

                    'Sub Total By Week
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
                    'ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                    'ret.Append("        <td align='center' >" & wDr("week_of_year") & "</td>")
                    'ret.Append("        <td align='center' >" & wDr("show_year") & "</td>")
                    ret.Append("        <td align='center' colspan='5' >Sub Total </td>")
                    'ret.Append("        <td align='center' >&nbsp;</td>")
                    'ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumTotalTime / IIf(STSumWorkingDay = 0, 1, STSumWorkingDay)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumProductivity / IIf(STCount_productivity = 0, 1, STCount_productivity)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumNonProductivity / IIf(STCount_non_productivity = 0, 1, STCount_non_productivity)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumE / IIf(STCount_est_ot = 0, 1, STCount_est_ot)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumServiceTime / IIf(STSumWorkingDay = 0, 1, STSumWorkingDay)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumLeasrning / IIf(STCount_prod_learning = 0, 1, STCount_prod_learning)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumStandBy / IIf(STCount_prod_stand_by = 0, 1, STCount_prod_stand_by)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumBrief / IIf(STCount_prod_brief = 0, 1, STCount_prod_brief)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumWarpUp / IIf(STCount_prod_warp_up = 0, 1, STCount_prod_warp_up)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumConsult / IIf(STCount_prod_consult = 0, 1, STCount_prod_consult)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumOtherProd / IIf(STCount_prod_other = 0, 1, STCount_prod_other)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumLunch / IIf(STCount_nprod_lunch = 0, 1, STCount_nprod_lunch)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumLeave / IIf(STCount_nprod_leave = 0, 1, STCount_nprod_leave)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumChangeCounter / IIf(STCount_nprod_change_counter = 0, 1, STCount_nprod_change_counter)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumHome / IIf(STCount_nprod_home = 0, 1, STCount_nprod_home)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumMiniBreak / IIf(STCount_nprod_mini_break = 0, 1, STCount_nprod_mini_break)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumRestRoom / IIf(STCount_nprod_restroom = 0, 1, STCount_nprod_restroom)) & "</td>")
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STSumOtherNonProd / IIf(STCount_nprod_other = 0, 1, STCount_nprod_other)) & "</td>")
                    ret.Append("    </tr>")
                Next
                wDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='5' >Total " & sDr("shop_name_en") & "</td>")
                'ret.Append("        <td align='center' >&nbsp;</td>")
                'ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / IIf(TotWorkingDay = 0, 1, TotWorkingDay)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / IIf(TotCount_productivity = 0, 1, TotCount_productivity)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / IIf(TotCount_non_productivity = 0, 1, TotCount_non_productivity)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / IIf(TotCount_est_ot = 0, 1, TotCount_est_ot)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / IIf(TotWorkingDay = 0, 1, TotWorkingDay)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / IIf(TotCount_prod_learning = 0, 1, TotCount_prod_learning)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / IIf(TotCount_prod_stand_by = 0, 1, TotCount_prod_stand_by)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / IIf(TotCount_prod_brief = 0, 1, TotCount_prod_brief)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / IIf(TotCount_prod_warp_up = 0, 1, TotCount_prod_warp_up)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / IIf(TotCount_prod_consult = 0, 1, TotCount_prod_consult)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / IIf(TotCount_prod_other = 0, 1, TotCount_prod_other)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / IIf(TotCount_nprod_lunch = 0, 1, TotCount_nprod_lunch)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / IIf(TotCount_nprod_leave = 0, 1, TotCount_nprod_leave)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / IIf(TotCount_nprod_change_counter = 0, 1, TotCount_nprod_change_counter)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / IIf(TotCount_nprod_home = 0, 1, TotCount_nprod_home)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / IIf(TotCount_nprod_mini_break = 0, 1, TotCount_nprod_mini_break)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / IIf(TotCount_nprod_restroom = 0, 1, TotCount_nprod_restroom)) & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / IIf(TotCount_nprod_other = 0, 1, TotCount_nprod_other)) & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            '******************* Grand Total *******************
            ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='5' >Grand Total</td>")
            'ret.Append("        <td align='center' >&nbsp;</td>")
            'ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / IIf(GTTotWorkingDay = 0, 1, GTTotWorkingDay)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / IIf(GTTotCount_productivity = 0, 1, GTTotCount_productivity)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / IIf(GTTotCount_non_productivity = 0, 1, GTTotCount_non_productivity)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / IIf(GTTotCount_est_ot = 0, 1, GTTotCount_est_ot)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / IIf(GTTotWorkingDay = 0, 1, GTTotWorkingDay)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / IIf(GTTotCount_prod_learning = 0, 1, GTTotCount_prod_learning)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / IIf(GTTotCount_prod_stand_by = 0, 1, GTTotCount_prod_stand_by)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / IIf(GTTotCount_prod_brief = 0, 1, GTTotCount_prod_brief)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / IIf(GTTotCount_prod_warp_up = 0, 1, GTTotCount_prod_warp_up)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / IIf(GTTotCount_prod_consult = 0, 1, GTTotCount_prod_consult)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / IIf(GTTotCount_prod_other = 0, 1, GTTotCount_prod_other)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / IIf(GTTotCount_nprod_lunch = 0, 1, GTTotCount_nprod_lunch)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / IIf(GTTotCount_nprod_leave = 0, 1, GTTotCount_nprod_leave)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / IIf(GTTotCount_nprod_change_counter = 0, 1, GTTotCount_nprod_change_counter)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / IIf(GTTotCount_nprod_home = 0, 1, GTTotCount_nprod_home)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / IIf(GTTotCount_nprod_mini_break = 0, 1, GTTotCount_nprod_mini_break)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / IIf(GTTotCount_nprod_restroom = 0, 1, GTTotCount_nprod_restroom)) & "</td>")
            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / IIf(GTTotCount_nprod_other = 0, 1, GTTotCount_nprod_other)) & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    'Private Sub RenderReportByDate(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
    '    Dim eng As New Engine.Reports.RepStaffAttendanceENG

    '    Dim dt As DataTable = eng.GetReportDataByDate(para)
    '    If dt.Rows.Count > 0 Then
    '        lblerror.Visible = False
    '        Dim ShopID As Long = 0
    '        Dim ShopIDGroup As Long = 0
    '        Dim DateGroup As String = ""

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

    '        Dim ret As New StringBuilder
    '        ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(i)
    '            If i = 0 Then
    '                'Header Row
    '                ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Log-In</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Log-Out</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Total Time</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Productivity</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Estimated OT</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Service Time</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Learning</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Stand by</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Brief</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Wrap up</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Consult</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;'  >Other Productivity(HR)</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Lunch</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Leave</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Change Counter</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Home</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Mini Break</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Rest Room</td>" & vbNewLine)
    '                ret.Append("        <td align='center' style='color: #ffffff;' >Other Non-Productivity(HR)</td>" & vbNewLine)
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
    '            ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
    '            ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
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
    '                End If
    '            End If

    '            If ChkSubTotal = True Then
    '                ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
    '                ret.Append("        <td align='center' colspan='4' >Sub Total</td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                If RecTotTotalTime = 0 Then
    '                    For k As Integer = 0 To 17
    '                        ret.Append("        <td align='right'>00:00:00</td>")
    '                    Next
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / RecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / RecTotTotalTime) & "</td>")
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
    '                ret.Append("        <td align='center' colspan='4' >Total " & dt.Rows(i).Item("Shop_name_en") & "</td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                ret.Append("        <td align='center' >&nbsp;</td>")
    '                If STRecTotTotalTime = 0 Then
    '                    For k As Integer = 0 To 17
    '                        ret.Append("        <td align='right'>00:00:00</td>")
    '                    Next
    '                Else
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotTotalTime / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotProductivity / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotNonProductivity / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotE / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotS / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeasrning / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotStandBy / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotBrief / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotWarpUp / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotConsult / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherProd / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLunch / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeave / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotChangeCounter / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotHome / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotMiniBreak / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotRestRoom / STRecTotTotalTime) & "</td>")
    '                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherNonProd / STRecTotTotalTime) & "</td>")
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
    '        ret.Append("        <td align='center' colspan='4' >Grand Total</td>")
    '        ret.Append("        <td align='center' >&nbsp;</td>")
    '        ret.Append("        <td align='center' >&nbsp;</td>")
    '        If GTRecTotTotalTime = 0 Then
    '            For i As Integer = 0 To 17
    '                ret.Append("        <td align='right'>00:00:00</td>")
    '            Next
    '        Else
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / GTRecTotTotalTime) & "</td>")
    '            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / GTRecTotTotalTime) & "</td>")
    '        End If
    '        ret.Append("    </tr>")
    '        ret.Append("</table>")
    '        lblReportDesc.Text = ret.ToString
    '        ret = Nothing
    '    End If
    '    dt.Dispose()
    '    eng = Nothing
    'End Sub

    'Code เดิมนะจ๊ะ

    Private Sub RenderReportByDate(ByVal para As CenParaDB.ReportCriteria.StaffAttendancePara)
        Dim eng As New Engine.Reports.RepStaffAttendanceENG

        Dim dt As DataTable = eng.GetReportDataByDate(para)
        If dt.Rows.Count > 0 Then
            lblerror.Visible = False
            'Dim RowProdTmp() As String = {}
            'Dim RowNonTmp() As String = {}
            Dim ShopID As Long = 0
            Dim ShopIDGroup As Long = 0
            Dim DateGroup As String = ""
            'Dim ShopName As String = ""

            Dim TotTotalTime As Long = 0
            Dim TotProductivity As Long = 0
            Dim TotNonProductivity As Long = 0
            Dim TotE As Long = 0
            Dim TotS As Long = 0
            Dim TotLearning As Long = 0
            Dim TotStandBy As Long = 0
            Dim TotBrief As Long = 0
            Dim TotWarpUp As Long = 0
            Dim TotConsult As Long = 0
            Dim TotOtherProd As Long = 0
            Dim TotLunch As Long = 0
            Dim TotLeave As Long = 0
            Dim TotChangeCounter As Long = 0
            Dim TotHome As Long = 0
            Dim TotMiniBreak As Long = 0
            Dim TotRestRoom As Long = 0
            Dim TotOtherNonProd As Long = 0

            Dim RecTotTotalTime As Long = 0
            Dim RecTotProductivity As Long = 0
            Dim RecTotNonProductivity As Long = 0
            Dim RecTotE As Long = 0
            Dim RecTotS As Long = 0
            Dim RecTotLearning As Long = 0
            Dim RecTotStandBy As Long = 0
            Dim RecTotBrief As Long = 0
            Dim RecTotWarpUp As Long = 0
            Dim RecTotConsult As Long = 0
            Dim RecTotOtherProd As Long = 0
            Dim RecTotLunch As Long = 0
            Dim RecTotLeave As Long = 0
            Dim RecTotChangeCounter As Long = 0
            Dim RecTotHome As Long = 0
            Dim RecTotMiniBreak As Long = 0
            Dim RecTotRestRoom As Long = 0
            Dim RecTotOtherNonProd As Long = 0

            Dim STTotTotalTime As Long = 0
            Dim STTotProductivity As Long = 0
            Dim STTotNonProductivity As Long = 0
            Dim STTotE As Long = 0
            Dim STTotS As Long = 0
            Dim STTotLeasrning As Long = 0
            Dim STTotStandBy As Long = 0
            Dim STTotBrief As Long = 0
            Dim STTotWarpUp As Long = 0
            Dim STTotConsult As Long = 0
            Dim STTotOtherProd As Long = 0
            Dim STTotLunch As Long = 0
            Dim STTotLeave As Long = 0
            Dim STTotChangeCounter As Long = 0
            Dim STTotHome As Long = 0
            Dim STTotMiniBreak As Long = 0
            Dim STTotRestRoom As Long = 0
            Dim STTotOtherNonProd As Long = 0

            Dim STRecTotTotalTime As Long = 0
            Dim STRecTotProductivity As Long = 0
            Dim STRecTotNonProductivity As Long = 0
            Dim STRecTotE As Long = 0
            Dim STRecTotS As Long = 0
            Dim STRecTotLearning As Long = 0
            Dim STRecTotStandBy As Long = 0
            Dim STRecTotBrief As Long = 0
            Dim STRecTotWarpUp As Long = 0
            Dim STRecTotConsult As Long = 0
            Dim STRecTotOtherProd As Long = 0
            Dim STRecTotLunch As Long = 0
            Dim STRecTotLeave As Long = 0
            Dim STRecTotChangeCounter As Long = 0
            Dim STRecTotHome As Long = 0
            Dim STRecTotMiniBreak As Long = 0
            Dim STRecTotRestRoom As Long = 0
            Dim STRecTotOtherNonProd As Long = 0

            Dim GTTotTotalTime As Long = 0
            Dim GTTotProductivity As Long = 0
            Dim GTTotNonProductivity As Long = 0
            Dim GTTotE As Long = 0
            Dim GTTotS As Long = 0
            Dim GTTotLearning As Long = 0
            Dim GTTotStandBy As Long = 0
            Dim GTTotBrief As Long = 0
            Dim GTTotWarpUp As Long = 0
            Dim GTTotConsult As Long = 0
            Dim GTTotOtherProd As Long = 0
            Dim GTTotLunch As Long = 0
            Dim GTTotLeave As Long = 0
            Dim GTTotChangeCounter As Long = 0
            Dim GTTotHome As Long = 0
            Dim GTTotMiniBreak As Long = 0
            Dim GTTotRestRoom As Long = 0
            Dim GTTotOtherNonProd As Long = 0

            Dim GTRecTotTotalTime As Long = 0
            Dim GTRecTotProductivity As Long = 0
            Dim GTRecTotNonProductivity As Long = 0
            Dim GTRecTotE As Long = 0
            Dim GTRecTotS As Long = 0
            Dim GTRecTotLearning As Long = 0
            Dim GTRecTotStandBy As Long = 0
            Dim GTRecTotBrief As Long = 0
            Dim GTRecTotWarpUp As Long = 0
            Dim GTRecTotConsult As Long = 0
            Dim GTRecTotOtherProd As Long = 0
            Dim GTRecTotLunch As Long = 0
            Dim GTRecTotLeave As Long = 0
            Dim GTRecTotChangeCounter As Long = 0
            Dim GTRecTotHome As Long = 0
            Dim GTRecTotMiniBreak As Long = 0
            Dim GTRecTotRestRoom As Long = 0
            Dim GTRecTotOtherNonProd As Long = 0

            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                If i = 0 Then
                    'Header Row
                    ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Log-In</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Log-Out</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Total Time</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Productivity</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Estimated OT</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Service Time</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Learning</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Stand by</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Brief</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Wrap up</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;'  >Productivity(HR) Consult</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;'  >Other Productivity(HR)</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Lunch</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Leave</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Change Counter</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Home</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Mini Break</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Non-Productivity(HR) Rest Room</td>" & vbNewLine)
                    ret.Append("        <td align='center' style='color: #ffffff;' >Other Non-Productivity(HR)</td>" & vbNewLine)
                    ret.Append("    </tr>")
                End If

                If ShopIDGroup = 0 Then
                    ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
                    ShopID = dt.Rows(i).Item("shop_id").ToString
                    DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
                End If

                '********************* File Data *************************
                ret.Append("    <tr>")
                ret.Append("        <td align='left'>&nbsp;" & dr("shop_name_en") & "</td>")
                ret.Append("        <td align='left'>&nbsp;" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                ret.Append("        <td align='left'>&nbsp;" & dr("user_code") & "</td>")
                ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
                ret.Append("        <td align='right'>" & dr("log_in") & "</td>")
                ret.Append("        <td align='right'>" & dr("log_out") & "</td>")
                ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(ReportsENG.GetSecFromTimeFormat(dr("productivity"))) & "</td>")
                ret.Append("        <td align='right'> " & dr("non_productivity") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
                    ret.Append("    <td align='right'> " & ReportsENG.GetFormatTimeFromSec(ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60)) & "</td>")
                    RecTotE += 1
                    STRecTotE += 1
                    GTRecTotE += 1
                Else
                    ret.Append("    <td align='right'>00:00:00</td>")
                End If
                ret.Append("        <td align='right'>" & dr("service_time") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > 0 Then
                    RecTotTotalTime += 1
                    STRecTotTotalTime += 1
                    GTRecTotTotalTime += 1
                End If
                If ReportsENG.GetSecFromTimeFormat(dr("productivity")) > 0 Then
                    RecTotProductivity += 1
                    STRecTotProductivity += 1
                    GTRecTotProductivity += 1
                End If
                If ReportsENG.GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
                    RecTotNonProductivity += 1
                    STRecTotNonProductivity += 1
                    GTRecTotNonProductivity += 1
                End If
                If ReportsENG.GetSecFromTimeFormat(dr("service_time")) > 0 Then
                    RecTotS += 1
                    STRecTotS += 1
                    GTRecTotS += 1
                End If


                '#### Productivity
                ret.Append("        <td align='right' >" & dr("prod_learning") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
                    RecTotLearning += 1
                    STRecTotLearning += 1
                    GTRecTotLearning += 1
                    TotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
                    STTotLeasrning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
                    GTTotLearning += ReportsENG.GetSecFromTimeFormat(dr("prod_learning"))
                End If

                ret.Append("        <td align='right' >" & dr("prod_stand_by") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
                    RecTotStandBy += 1
                    STRecTotStandBy += 1
                    GTRecTotStandBy += 1
                    TotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
                    STTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
                    GTTotStandBy += ReportsENG.GetSecFromTimeFormat(dr("prod_stand_by"))
                End If

                ret.Append("        <td align='right' >" & dr("prod_brief") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
                    RecTotBrief += 1
                    STRecTotBrief += 1
                    GTRecTotBrief += 1
                    TotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
                    STTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
                    GTTotBrief += ReportsENG.GetSecFromTimeFormat(dr("prod_brief"))
                End If

                ret.Append("        <td align='right' >" & dr("prod_warp_up") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
                    RecTotWarpUp += 1
                    STRecTotWarpUp += 1
                    GTRecTotWarpUp += 1
                    TotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
                    STTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
                    GTTotWarpUp += ReportsENG.GetSecFromTimeFormat(dr("prod_warp_up"))
                End If

                ret.Append("        <td align='right' >" & dr("prod_consult") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
                    RecTotConsult += 1
                    STRecTotConsult += 1
                    GTRecTotConsult += 1
                    TotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
                    STTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
                    GTTotConsult += ReportsENG.GetSecFromTimeFormat(dr("prod_consult"))
                End If

                ret.Append("        <td align='right' >" & dr("prod_other") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("prod_other")) > 0 Then
                    RecTotOtherProd += 1
                    STRecTotOtherProd += 1
                    GTRecTotOtherProd += 1
                    TotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
                    STTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
                    GTTotOtherProd += ReportsENG.GetSecFromTimeFormat(dr("prod_other"))
                End If


                '### Non Productivity
                ret.Append("        <td align='right' >" & dr("nprod_lunch") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
                    RecTotLunch += 1
                    STRecTotLunch += 1
                    GTRecTotLunch += 1
                    TotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
                    STTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
                    GTTotLunch += ReportsENG.GetSecFromTimeFormat(dr("nprod_lunch"))
                End If

                ret.Append("        <td align='right' >" & dr("nprod_leave") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
                    RecTotLeave += 1
                    STRecTotLeave += 1
                    GTRecTotLeave += 1
                    TotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
                    STTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
                    GTTotLeave += ReportsENG.GetSecFromTimeFormat(dr("nprod_leave"))
                End If

                ret.Append("        <td align='right' >" & dr("nprod_change_counter") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
                    RecTotChangeCounter += 1
                    STRecTotChangeCounter += 1
                    GTRecTotChangeCounter += 1
                    TotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
                    STTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
                    GTTotChangeCounter += ReportsENG.GetSecFromTimeFormat(dr("nprod_change_counter"))
                End If

                ret.Append("        <td align='right' >" & dr("nprod_home") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
                    RecTotHome += 1
                    STRecTotHome += 1
                    GTRecTotHome += 1
                    TotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
                    STTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
                    GTTotHome += ReportsENG.GetSecFromTimeFormat(dr("nprod_home"))
                End If

                ret.Append("        <td align='right' >" & dr("nprod_mini_break") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
                    RecTotMiniBreak += 1
                    STRecTotMiniBreak += 1
                    GTRecTotMiniBreak += 1
                    TotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
                    STTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
                    GTTotMiniBreak += ReportsENG.GetSecFromTimeFormat(dr("nprod_mini_break"))
                End If

                ret.Append("        <td align='right' >" & dr("nprod_restroom") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
                    RecTotRestRoom += 1
                    STRecTotRestRoom += 1
                    GTRecTotRestRoom += 1
                    TotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
                    STTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
                    GTTotRestRoom += ReportsENG.GetSecFromTimeFormat(dr("nprod_restroom"))
                End If

                ret.Append("        <td align='right' >" & dr("nprod_other") & "</td>")
                If ReportsENG.GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
                    RecTotOtherNonProd += 1
                    STRecTotOtherNonProd += 1
                    GTRecTotOtherNonProd += 1
                    TotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
                    STTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
                    GTTotOtherNonProd += ReportsENG.GetSecFromTimeFormat(dr("nprod_other"))
                End If
                ret.Append("    </tr>")

                If Convert.IsDBNull(dr("total_time")) = False Then
                    TotTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                    STTotTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                    GTTotTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                End If
                If ReportsENG.GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
                    TotE += (ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60))
                    STTotE += (ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60))
                    GTTotE += (ReportsENG.GetSecFromTimeFormat(dr("total_time")) - (450 * 60))
                End If

                TotS += ReportsENG.GetSecFromTimeFormat(dr("service_time"))
                TotProductivity += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
                TotNonProductivity += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))
                STTotS += ReportsENG.GetSecFromTimeFormat(dr("service_time"))
                STTotProductivity += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
                STTotNonProductivity += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))
                GTTotS += ReportsENG.GetSecFromTimeFormat(dr("service_time"))
                GTTotProductivity += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
                GTTotNonProductivity += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))

                '******************** Sub Total ********************
                Dim ChkSubTotal As Boolean = False
                If dt.Rows.Count = i + 1 Then
                    ChkSubTotal = True
                Else
                    If dt.Rows(i).Item("shop_id") <> dt.Rows(i + 1).Item("shop_id") Or CDate(dt.Rows(i).Item("service_date")).ToShortDateString <> CDate(dt.Rows(i + 1).Item("service_date")).ToShortDateString Then
                        ChkSubTotal = True
                        ShopIDGroup = dt.Rows(i).Item("shop_id")
                        DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
                        'ShopName = dt.Rows(i).Item("shop_name_en").ToString
                    End If
                End If

                If ChkSubTotal = True Then
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
                    ret.Append("        <td align='center' colspan='6' >Sub Total</td>")
                    If RecTotTotalTime = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotTotalTime / RecTotTotalTime) & "</td>")
                    End If
                    If RecTotProductivity = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotProductivity / RecTotProductivity) & "</td>")
                    End If
                    If RecTotNonProductivity = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotNonProductivity / RecTotNonProductivity) & "</td>")
                    End If
                    If RecTotE = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotE / RecTotE) & "</td>")
                    End If
                    If RecTotS = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotS / RecTotTotalTime) & "</td>")
                    End If
                    If RecTotLearning = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLearning / RecTotLearning) & "</td>")
                    End If
                    If RecTotStandBy = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotStandBy / RecTotStandBy) & "</td>")
                    End If
                    If RecTotBrief = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotBrief / RecTotBrief) & "</td>")
                    End If
                    If RecTotWarpUp = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotWarpUp / RecTotWarpUp) & "</td>")
                    End If
                    If RecTotConsult = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotConsult / RecTotConsult) & "</td>")
                    End If
                    If RecTotOtherProd = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherProd / RecTotOtherProd) & "</td>")
                    End If

                    If RecTotLunch = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLunch / RecTotLunch) & "</td>")
                    End If
                    If RecTotLeave = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotLeave / RecTotLeave) & "</td>")
                    End If
                    If RecTotChangeCounter = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotChangeCounter / RecTotChangeCounter) & "</td>")
                    End If
                    If RecTotHome = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotHome / RecTotHome) & "</td>")
                    End If
                    If RecTotMiniBreak = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotMiniBreak / RecTotMiniBreak) & "</td>")
                    End If
                    If RecTotRestRoom = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotRestRoom / RecTotRestRoom) & "</td>")
                    End If
                    If RecTotOtherNonProd = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TotOtherNonProd / RecTotOtherNonProd) & "</td>")
                    End If
                    ret.Append("    </tr>")

                    TotTotalTime = 0
                    TotProductivity = 0
                    TotNonProductivity = 0
                    TotE = 0
                    TotS = 0
                    TotLearning = 0
                    TotStandBy = 0
                    TotBrief = 0
                    TotWarpUp = 0
                    TotConsult = 0
                    TotOtherProd = 0
                    TotLunch = 0
                    TotLeave = 0
                    TotChangeCounter = 0
                    TotHome = 0
                    TotMiniBreak = 0
                    TotRestRoom = 0
                    TotOtherNonProd = 0

                    RecTotTotalTime = 0
                    RecTotProductivity = 0
                    RecTotNonProductivity = 0
                    RecTotE = 0
                    RecTotS = 0
                    RecTotLearning = 0
                    RecTotStandBy = 0
                    RecTotBrief = 0
                    RecTotWarpUp = 0
                    RecTotConsult = 0
                    RecTotOtherProd = 0
                    RecTotLunch = 0
                    RecTotLeave = 0
                    RecTotChangeCounter = 0
                    RecTotHome = 0
                    RecTotMiniBreak = 0
                    RecTotRestRoom = 0
                    RecTotOtherNonProd = 0
                End If
                '***************************************************
                '************************ Total ***************************
                Dim ChkTotal As Boolean = False
                If dt.Rows.Count = i + 1 Then
                    ChkTotal = True
                Else
                    If dt.Rows(i).Item("Shop_id") <> dt.Rows(i + 1).Item("Shop_id") Then
                        ChkTotal = True
                    End If
                End If
                If ChkTotal = True Then
                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' colspan='6' >Total " & dt.Rows(i).Item("Shop_name_en") & "</td>")
                    If STRecTotTotalTime = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotTotalTime / STRecTotTotalTime) & "</td>")

                    End If
                    If STRecTotProductivity = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotProductivity / STRecTotProductivity) & "</td>")
                    End If
                    If STRecTotNonProductivity = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotNonProductivity / STRecTotNonProductivity) & "</td>")
                    End If
                    If STRecTotE = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        'ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotE / STRecTotE) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotE / STRecTotE) & "</td>")
                    End If
                    If STRecTotS = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotS / STRecTotTotalTime) & "</td>")
                    End If
                    If STRecTotLearning = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeasrning / STRecTotLearning) & "</td>")
                    End If
                    If STRecTotStandBy = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotStandBy / STRecTotStandBy) & "</td>")
                    End If
                    If STRecTotBrief = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotBrief / STRecTotBrief) & "</td>")
                    End If
                    If STRecTotWarpUp = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotWarpUp / STRecTotWarpUp) & "</td>")
                    End If
                    If STRecTotConsult = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotConsult / STRecTotConsult) & "</td>")
                    End If
                    If STRecTotOtherProd = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherProd / STRecTotOtherProd) & "</td>")
                    End If

                    If STRecTotLunch = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLunch / STRecTotLunch) & "</td>")
                    End If
                    If STRecTotLeave = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotLeave / STRecTotLeave) & "</td>")
                    End If
                    If STRecTotChangeCounter = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotChangeCounter / STRecTotChangeCounter) & "</td>")
                    End If
                    If STRecTotHome = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotHome / STRecTotHome) & "</td>")
                    End If
                    If STRecTotMiniBreak = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotMiniBreak / STRecTotMiniBreak) & "</td>")
                    End If
                    If STRecTotRestRoom = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotRestRoom / STRecTotRestRoom) & "</td>")
                    End If
                    If STRecTotOtherNonProd = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(STTotOtherNonProd / STRecTotOtherNonProd) & "</td>")
                    End If
                    ret.Append("    </tr>")

                    STTotTotalTime = 0
                    STTotProductivity = 0
                    STTotNonProductivity = 0
                    STTotE = 0
                    STTotS = 0
                    STTotLeasrning = 0
                    STTotStandBy = 0
                    STTotBrief = 0
                    STTotWarpUp = 0
                    STTotConsult = 0
                    STTotOtherProd = 0
                    STTotLunch = 0
                    STTotLeave = 0
                    STTotChangeCounter = 0
                    STTotHome = 0
                    STTotMiniBreak = 0
                    STTotRestRoom = 0
                    STTotOtherNonProd = 0

                    STRecTotTotalTime = 0
                    STRecTotProductivity = 0
                    STRecTotNonProductivity = 0
                    STRecTotE = 0
                    STRecTotS = 0
                    STRecTotLearning = 0
                    STRecTotStandBy = 0
                    STRecTotBrief = 0
                    STRecTotWarpUp = 0
                    STRecTotConsult = 0
                    STRecTotOtherProd = 0
                    STRecTotLunch = 0
                    STRecTotLeave = 0
                    STRecTotChangeCounter = 0
                    STRecTotHome = 0
                    STRecTotMiniBreak = 0
                    STRecTotRestRoom = 0
                    STRecTotOtherNonProd = 0
                End If
                '***************************************************
            Next
            '******************* Grand Total *******************
            ret.Append("    <tr style='background: Orange; font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='6' >Grand Total</td>")
            If GTRecTotTotalTime = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotTotalTime / GTRecTotTotalTime) & "</td>")
            End If
            If GTRecTotProductivity = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotProductivity / GTRecTotProductivity) & "</td>")
            End If
            If GTRecTotNonProductivity = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotNonProductivity / GTRecTotNonProductivity) & "</td>")
            End If
            If GTRecTotE = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotE / GTRecTotE) & "</td>")
            End If
            If GTRecTotS = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotS / GTRecTotTotalTime) & "</td>")
            End If
            If GTRecTotLearning = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLearning / GTRecTotLearning) & "</td>")
            End If
            If GTRecTotStandBy = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotStandBy / GTRecTotStandBy) & "</td>")
            End If
            If GTRecTotBrief = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotBrief / GTRecTotBrief) & "</td>")
            End If
            If GTRecTotWarpUp = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotWarpUp / GTRecTotWarpUp) & "</td>")
            End If
            If GTRecTotConsult = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotConsult / GTRecTotConsult) & "</td>")
            End If
            If GTRecTotOtherProd = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherProd / GTRecTotOtherProd) & "</td>")
            End If

            If GTRecTotLunch = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLunch / GTRecTotLunch) & "</td>")
            End If
            If GTRecTotLeave = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotLeave / GTRecTotLeave) & "</td>")
            End If
            If GTRecTotChangeCounter = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotChangeCounter / GTRecTotChangeCounter) & "</td>")
            End If
            If GTRecTotHome = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotHome / GTRecTotHome) & "</td>")
            End If
            If GTRecTotMiniBreak = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotMiniBreak / GTRecTotMiniBreak) & "</td>")
            End If
            If GTRecTotRestRoom = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotRestRoom / GTRecTotRestRoom) & "</td>")
            End If
            If GTRecTotOtherNonProd = 0 Then
                ret.Append("        <td align='right'>00:00:00</td>")
            Else
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GTTotOtherNonProd / GTRecTotOtherNonProd) & "</td>")
            End If
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

End Class
