Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repWaitingTimeHandlingTimeBySkill
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "WaitingTimeAndHandlingTimeBySkillReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repWaitingTimeHandlingTimeBySkill.ExportData", Config.GetLoginHistoryID)
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
        Dim txtData As String = ""
        Select Case ReportName
            Case "ByTime"
                Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
                para.ShopID = Request("ShopID")
                para.DateFrom = Request("DateFrom")
                para.DateTo = Request("DateTo")
                para.IntervalMinute = Request("Interval")
                para.TimePeroidFrom = Request("TimeFrom")
                para.TimePeroidTo = Request("TimeTo")
                Dim eng As New Engine.Reports.RepWtHtBySkillENG
                Dim dt As New DataTable
                dt = eng.GetReportByTime(para)
                If dt.Rows.Count > 0 Then
                    RenderReportByTime(dt, para)
                Else
                    btnExport.Visible = False
                End If
                dt.Dispose()
                eng = Nothing
                para = Nothing
            Case "ByWeek"
                Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
                para.ShopID = Request("ShopID")
                para.WeekInYearFrom = Request("WeekFrom")
                para.WeekInYearTo = Request("WeekTo")
                para.YearFrom = Request("YearFrom")
                para.YearTo = Request("YearTo")
                Dim dt As New DataTable
                Dim eng As New Engine.Reports.RepWtHtBySkillENG
                dt = eng.GetReportByweek(para)
                If dt.Rows.Count > 0 Then
                    RenderReportByWeek(dt)
                Else
                    btnExport.Visible = False
                End If
                dt.Dispose()
                eng = Nothing
                para = Nothing
            Case "ByDate"
                Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
                para.ShopID = Request("ShopID")
                para.DateFrom = Request("DateFrom")
                para.DateTo = Request("DateTo")
                Dim dt As New DataTable
                Dim eng As New Engine.Reports.RepWtHtBySkillENG
                dt = eng.GetReportByDate(para)
                If dt.Rows.Count > 0 Then
                    RenderReportByDate(dt)
                Else
                    btnExport.Visible = False
                End If
                dt.Dispose()
                eng = Nothing
                para = Nothing
            Case "ByMonth"
                Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
                para.ShopID = Request("ShopID")
                para.MonthFrom = Request("MonthFrom")
                para.MonthTo = Request("MonthTo")
                para.YearFrom = Request("YearFrom")
                para.YearTo = Request("YearTo")
                Dim dt As New DataTable
                Dim eng As New Engine.Reports.RepWtHtBySkillENG
                dt = eng.GetReportByMonth(para)
                If dt.Rows.Count > 0 Then
                    RenderReportByMonth(dt)
                Else
                    btnExport.Visible = False
                End If
                dt.Dispose()
                eng = Nothing
                para = Nothing
        End Select

        If lblReportDesc.Text <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Skill</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Num of Queue</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Missed Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AWT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AConT</td>")
            ret.Append("    </tr>")

            Dim GCountQ As Integer = 0
            Dim GRegis As Integer = 0
            Dim GServe As Integer = 0
            Dim GMissCall As Integer = 0
            Dim GCancel As Integer = 0
            Dim GIncomplete As Integer = 0
            Dim GSumWT As Long = 0
            Dim GCountWT As Integer = 0
            Dim GSumHT As Long = 0
            Dim GCountHT As Integer = 0
            Dim GSumConT As Long = 0
            Dim GCountConT As Integer = 0

            'DataRow
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TCountQ As Integer = 0
                Dim TRegis As Integer = 0
                Dim TServe As Integer = 0
                Dim TMissCall As Integer = 0
                Dim TCancel As Integer = 0
                Dim TIncomplete As Integer = 0
                Dim TSumWT As Long = 0
                Dim TCountWT As Integer = 0
                Dim TSumHT As Long = 0
                Dim TCountHT As Integer = 0
                Dim TSumConT As Long = 0
                Dim TCountConT As Integer = 0

                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                dt.DefaultView.Sort = "show_year,month_no,staff_name"
                stDt = dt.DefaultView.ToTable(True, "show_year", "month_no", "show_month", "user_code", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    Dim CountQ As Integer = 0
                    Dim Regis As Integer = 0
                    Dim Serve As Integer = 0
                    Dim MissCall As Integer = 0
                    Dim Cancel As Integer = 0
                    Dim Incomplete As Integer = 0
                    Dim SumWT As Long = 0
                    Dim CountWT As Integer = 0
                    Dim SumHT As Long = 0
                    Dim CountHT As Integer = 0
                    Dim SumConT As Long = 0
                    Dim CountConT As Integer = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and show_year='" & stDr("show_year") & "' and month_no='" & stDr("month_no") & "' and user_code='" & stDr("user_code") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >" & stDr("show_month") & "</td>")
                        ret.Append("        <td align='center' >" & stDr("show_year") & "</td>")
                        ret.Append("        <td align='left' >" & stDr("staff_name") & "</td>")
                        ret.Append("        <td align='left' >" & stDr("user_code") & "</td>")
                        ret.Append("        <td align='left' >" & dr("skill_name") & "</td>")
                        ret.Append("        <td align='center' >" & dr("num_of_queue") & "</td>")
                        ret.Append("        <td align='center' >" & dr("regis") & "</td>")
                        ret.Append("        <td align='center' >" & dr("serve") & "</td>")
                        ret.Append("        <td align='center' >" & dr("miss_call") & "</td>")
                        ret.Append("        <td align='center' >" & dr("cancel") & "</td>")
                        ret.Append("        <td align='center' >" & (dr("not_call") + dr("not_con") + dr("not_end")) & "</td>")
                        ret.Append("        <td align='center' >" & dr("awt") & "</td>")
                        ret.Append("        <td align='center' >" & dr("aht") & "</td>")
                        ret.Append("        <td align='center' >" & dr("acont") & "</td>")
                        ret.Append("    </tr>")
                        CountQ += Convert.ToInt32(dr("num_of_queue"))
                        Regis += Convert.ToInt32(dr("regis"))
                        Serve += Convert.ToInt32(dr("serve"))
                        MissCall += Convert.ToInt32(dr("miss_call"))
                        Cancel += Convert.ToInt32(dr("cancel"))
                        Incomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        SumWT += Convert.ToInt64(dr("sum_wt"))
                        CountWT += Convert.ToInt64(dr("count_wt"))
                        SumHT += Convert.ToInt64(dr("sum_ht"))
                        CountHT += Convert.ToInt64(dr("count_ht"))
                        SumConT += Convert.ToInt64(dr("sum_cont"))
                        CountConT += Convert.ToInt64(dr("count_cont"))

                        TCountQ += Convert.ToInt32(dr("num_of_queue"))
                        TRegis += Convert.ToInt32(dr("regis"))
                        TServe += Convert.ToInt32(dr("serve"))
                        TMissCall += Convert.ToInt32(dr("miss_call"))
                        TCancel += Convert.ToInt32(dr("cancel"))
                        TIncomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        TSumWT += Convert.ToInt64(dr("sum_wt"))
                        TCountWT += Convert.ToInt64(dr("count_wt"))
                        TSumHT += Convert.ToInt64(dr("sum_ht"))
                        TCountHT += Convert.ToInt64(dr("count_ht"))
                        TSumConT += Convert.ToInt64(dr("sum_cont"))
                        TCountConT += Convert.ToInt64(dr("count_cont"))

                        GCountQ += Convert.ToInt32(dr("num_of_queue"))
                        GRegis += Convert.ToInt32(dr("regis"))
                        GServe += Convert.ToInt32(dr("serve"))
                        GMissCall += Convert.ToInt32(dr("miss_call"))
                        GCancel += Convert.ToInt32(dr("cancel"))
                        GIncomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        GSumWT += Convert.ToInt64(dr("sum_wt"))
                        GCountWT += Convert.ToInt64(dr("count_wt"))
                        GSumHT += Convert.ToInt64(dr("sum_ht"))
                        GCountHT += Convert.ToInt64(dr("count_ht"))
                        GSumConT += Convert.ToInt64(dr("sum_cont"))
                        GCountConT += Convert.ToInt64(dr("count_cont"))
                    Next

                    'Sub Total Row
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;' >")
                    ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_month") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_year") & "</td>")
                    ret.Append("        <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("        <td align='left' >&nbsp;</td>")
                    ret.Append("        <td align='left' >&nbsp;</td>")
                    ret.Append("        <td align='center' >" & Format(CountQ, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Regis, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Serve, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(MissCall, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Cancel, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Incomplete, "#,##0") & "</td>")
                    If CountWT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumWT / CountWT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If CountHT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumHT / CountHT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If CountConT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumConT / CountConT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    ret.Append("    </tr>")
                    'Next
                Next

                'Total Row by Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='left' >Total </td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='center' >" & Format(TCountQ, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TRegis, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TServe, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TCancel, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TIncomplete, "#,##0") & "</td>")
                If TCountWT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumWT / TCountWT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If TCountHT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumHT / TCountHT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If TCountConT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumConT / TCountConT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange; font-weight: bold;' >")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='left' >Grand Total</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >" & Format(GCountQ, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GRegis, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GServe, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMissCall, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GCancel, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GIncomplete, "#,##0") & "</td>")
            If GCountWT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumWT / GCountWT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            If GCountHT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumHT / GCountHT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            If GCountConT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumConT / GCountConT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Week</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Skill</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Num of Queue</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Missed Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AWT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AConT</td>")
            ret.Append("    </tr>")

            Dim GCountQ As Integer = 0
            Dim GRegis As Integer = 0
            Dim GServe As Integer = 0
            Dim GMissCall As Integer = 0
            Dim GCancel As Integer = 0
            Dim GIncomplete As Integer = 0
            Dim GSumWT As Long = 0
            Dim GCountWT As Integer = 0
            Dim GSumHT As Long = 0
            Dim GCountHT As Integer = 0
            Dim GSumConT As Long = 0
            Dim GCountConT As Integer = 0

            'DataRow
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TCountQ As Integer = 0
                Dim TRegis As Integer = 0
                Dim TServe As Integer = 0
                Dim TMissCall As Integer = 0
                Dim TCancel As Integer = 0
                Dim TIncomplete As Integer = 0
                Dim TSumWT As Long = 0
                Dim TCountWT As Integer = 0
                Dim TSumHT As Long = 0
                Dim TCountHT As Integer = 0
                Dim TSumConT As Long = 0
                Dim TCountConT As Integer = 0

                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                dt.DefaultView.Sort = "show_year,week_of_year,staff_name"
                stDt = dt.DefaultView.ToTable(True, "show_year", "week_of_year", "user_code", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    Dim CountQ As Integer = 0
                    Dim Regis As Integer = 0
                    Dim Serve As Integer = 0
                    Dim MissCall As Integer = 0
                    Dim Cancel As Integer = 0
                    Dim Incomplete As Integer = 0
                    Dim SumWT As Long = 0
                    Dim CountWT As Integer = 0
                    Dim SumHT As Long = 0
                    Dim CountHT As Integer = 0
                    Dim SumConT As Long = 0
                    Dim CountConT As Integer = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and show_year='" & stDr("show_year") & "' and week_of_year='" & stDr("week_of_year") & "' and user_code='" & stDr("user_code") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >" & stDr("week_of_year") & "</td>")
                        ret.Append("        <td align='center' >" & stDr("show_year") & "</td>")
                        ret.Append("        <td align='left' >" & stDr("staff_name") & "</td>")
                        ret.Append("        <td align='left' >" & stDr("user_code") & "</td>")
                        ret.Append("        <td align='left' >" & dr("skill_name") & "</td>")
                        ret.Append("        <td align='center' >" & dr("num_of_queue") & "</td>")
                        ret.Append("        <td align='center' >" & dr("regis") & "</td>")
                        ret.Append("        <td align='center' >" & dr("serve") & "</td>")
                        ret.Append("        <td align='center' >" & dr("miss_call") & "</td>")
                        ret.Append("        <td align='center' >" & dr("cancel") & "</td>")
                        ret.Append("        <td align='center' >" & (dr("not_call") + dr("not_con") + dr("not_end")) & "</td>")
                        ret.Append("        <td align='center' >" & dr("awt") & "</td>")
                        ret.Append("        <td align='center' >" & dr("aht") & "</td>")
                        ret.Append("        <td align='center' >" & dr("acont") & "</td>")
                        ret.Append("    </tr>")
                        CountQ += Convert.ToInt32(dr("num_of_queue"))
                        Regis += Convert.ToInt32(dr("regis"))
                        Serve += Convert.ToInt32(dr("serve"))
                        MissCall += Convert.ToInt32(dr("miss_call"))
                        Cancel += Convert.ToInt32(dr("cancel"))
                        Incomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        SumWT += Convert.ToInt64(dr("sum_wt"))
                        CountWT += Convert.ToInt64(dr("count_wt"))
                        SumHT += Convert.ToInt64(dr("sum_ht"))
                        CountHT += Convert.ToInt64(dr("count_ht"))
                        SumConT += Convert.ToInt64(dr("sum_cont"))
                        CountConT += Convert.ToInt64(dr("count_cont"))

                        TCountQ += Convert.ToInt32(dr("num_of_queue"))
                        TRegis += Convert.ToInt32(dr("regis"))
                        TServe += Convert.ToInt32(dr("serve"))
                        TMissCall += Convert.ToInt32(dr("miss_call"))
                        TCancel += Convert.ToInt32(dr("cancel"))
                        TIncomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        TSumWT += Convert.ToInt64(dr("sum_wt"))
                        TCountWT += Convert.ToInt64(dr("count_wt"))
                        TSumHT += Convert.ToInt64(dr("sum_ht"))
                        TCountHT += Convert.ToInt64(dr("count_ht"))
                        TSumConT += Convert.ToInt64(dr("sum_cont"))
                        TCountConT += Convert.ToInt64(dr("count_cont"))

                        GCountQ += Convert.ToInt32(dr("num_of_queue"))
                        GRegis += Convert.ToInt32(dr("regis"))
                        GServe += Convert.ToInt32(dr("serve"))
                        GMissCall += Convert.ToInt32(dr("miss_call"))
                        GCancel += Convert.ToInt32(dr("cancel"))
                        GIncomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        GSumWT += Convert.ToInt64(dr("sum_wt"))
                        GCountWT += Convert.ToInt64(dr("count_wt"))
                        GSumHT += Convert.ToInt64(dr("sum_ht"))
                        GCountHT += Convert.ToInt64(dr("count_ht"))
                        GSumConT += Convert.ToInt64(dr("sum_cont"))
                        GCountConT += Convert.ToInt64(dr("count_cont"))
                    Next

                    'Sub Total Row
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;' >")
                    ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("week_of_year") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_year") & "</td>")
                    ret.Append("        <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("        <td align='left' >&nbsp;</td>")
                    ret.Append("        <td align='left' >&nbsp;</td>")
                    ret.Append("        <td align='center' >" & Format(CountQ, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Regis, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Serve, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(MissCall, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Cancel, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Incomplete, "#,##0") & "</td>")
                    If CountWT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumWT / CountWT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If CountHT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumHT / CountHT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If CountConT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumConT / CountConT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    ret.Append("    </tr>")
                    'Next
                Next

                'Total Row by Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='left' >Total </td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='center' >" & Format(TCountQ, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TRegis, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TServe, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TCancel, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TIncomplete, "#,##0") & "</td>")
                If TCountWT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumWT / TCountWT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If TCountHT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumHT / TCountHT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If TCountConT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumConT / TCountConT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                ret.Append("    </tr>")
            Next
            sDt.Dispose()


            'Grand Total
            ret.Append("    <tr style='background: Orange; font-weight: bold;' >")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='left' >Grand Total</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >" & Format(GCountQ, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GRegis, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GServe, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMissCall, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GCancel, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GIncomplete, "#,##0") & "</td>")
            If GCountWT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumWT / GCountWT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            If GCountHT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumHT / GCountHT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            If GCountConT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumConT / GCountConT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Skill</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Num of Queue</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Missed Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AWT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AConT</td>")
            ret.Append("    </tr>")

            Dim GCountQ As Integer = 0
            Dim GRegis As Integer = 0
            Dim GServe As Integer = 0
            Dim GMissCall As Integer = 0
            Dim GCancel As Integer = 0
            Dim GIncomplete As Integer = 0
            Dim GSumWT As Long = 0
            Dim GCountWT As Integer = 0
            Dim GSumHT As Long = 0
            Dim GCountHT As Integer = 0
            Dim GSumConT As Long = 0
            Dim GCountConT As Integer = 0

            'DataRow
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TCountQ As Integer = 0
                Dim TRegis As Integer = 0
                Dim TServe As Integer = 0
                Dim TMissCall As Integer = 0
                Dim TCancel As Integer = 0
                Dim TIncomplete As Integer = 0
                Dim TSumWT As Long = 0
                Dim TCountWT As Integer = 0
                Dim TSumHT As Long = 0
                Dim TCountHT As Integer = 0
                Dim TSumConT As Long = 0
                Dim TCountConT As Integer = 0

                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                dt.DefaultView.Sort = "service_date,staff_name"
                stDt = dt.DefaultView.ToTable(True, "show_date", "user_code", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    'dt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "'"
                    'Dim dDt As New DataTable
                    'dDt = dt.DefaultView.ToTable(True, "show_date")
                    'For Each dDr As DataRow In dDt.Rows
                    Dim CountQ As Integer = 0
                    Dim Regis As Integer = 0
                    Dim Serve As Integer = 0
                    Dim MissCall As Integer = 0
                    Dim Cancel As Integer = 0
                    Dim Incomplete As Integer = 0
                    Dim SumWT As Long = 0
                    Dim CountWT As Integer = 0
                    Dim SumHT As Long = 0
                    Dim CountHT As Integer = 0
                    Dim SumConT As Long = 0
                    Dim CountConT As Integer = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and show_date='" & stDr("show_date") & "' and user_code='" & stDr("user_code") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & stDr("show_date") & "</td>")
                        ret.Append("        <td align='left' >" & stDr("staff_name") & "</td>")
                        ret.Append("        <td align='left' >" & stDr("user_code") & "</td>")
                        ret.Append("        <td align='left' >" & dr("skill_name") & "</td>")
                        ret.Append("        <td align='center' >" & dr("num_of_queue") & "</td>")
                        ret.Append("        <td align='center' >" & dr("regis") & "</td>")
                        ret.Append("        <td align='center' >" & dr("serve") & "</td>")
                        ret.Append("        <td align='center' >" & dr("miss_call") & "</td>")
                        ret.Append("        <td align='center' >" & dr("cancel") & "</td>")
                        ret.Append("        <td align='center' >" & (dr("not_call") + dr("not_con") + dr("not_end")) & "</td>")
                        ret.Append("        <td align='center' >" & dr("awt") & "</td>")
                        ret.Append("        <td align='center' >" & dr("aht") & "</td>")
                        ret.Append("        <td align='center' >" & dr("acont") & "</td>")
                        ret.Append("    </tr>")
                        CountQ += Convert.ToInt32(dr("num_of_queue"))
                        Regis += Convert.ToInt32(dr("regis"))
                        Serve += Convert.ToInt32(dr("serve"))
                        MissCall += Convert.ToInt32(dr("miss_call"))
                        Cancel += Convert.ToInt32(dr("cancel"))
                        Incomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        SumWT += Convert.ToInt64(dr("sum_wt"))
                        CountWT += Convert.ToInt64(dr("count_wt"))
                        SumHT += Convert.ToInt64(dr("sum_ht"))
                        CountHT += Convert.ToInt64(dr("count_ht"))
                        SumConT += Convert.ToInt64(dr("sum_cont"))
                        CountConT += Convert.ToInt64(dr("count_cont"))

                        TCountQ += Convert.ToInt32(dr("num_of_queue"))
                        TRegis += Convert.ToInt32(dr("regis"))
                        TServe += Convert.ToInt32(dr("serve"))
                        TMissCall += Convert.ToInt32(dr("miss_call"))
                        TCancel += Convert.ToInt32(dr("cancel"))
                        TIncomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        TSumWT += Convert.ToInt64(dr("sum_wt"))
                        TCountWT += Convert.ToInt64(dr("count_wt"))
                        TSumHT += Convert.ToInt64(dr("sum_ht"))
                        TCountHT += Convert.ToInt64(dr("count_ht"))
                        TSumConT += Convert.ToInt64(dr("sum_cont"))
                        TCountConT += Convert.ToInt64(dr("count_cont"))

                        GCountQ += Convert.ToInt32(dr("num_of_queue"))
                        GRegis += Convert.ToInt32(dr("regis"))
                        GServe += Convert.ToInt32(dr("serve"))
                        GMissCall += Convert.ToInt32(dr("miss_call"))
                        GCancel += Convert.ToInt32(dr("cancel"))
                        GIncomplete += (Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end")))
                        GSumWT += Convert.ToInt64(dr("sum_wt"))
                        GCountWT += Convert.ToInt64(dr("count_wt"))
                        GSumHT += Convert.ToInt64(dr("sum_ht"))
                        GCountHT += Convert.ToInt64(dr("count_ht"))
                        GSumConT += Convert.ToInt64(dr("sum_cont"))
                        GCountConT += Convert.ToInt64(dr("count_cont"))
                    Next

                    'Sub Total Row
                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;' >")
                    ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_date") & "</td>")
                    ret.Append("        <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("        <td align='left' >&nbsp;</td>")
                    ret.Append("        <td align='left' >&nbsp;</td>")
                    ret.Append("        <td align='center' >" & Format(CountQ, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Regis, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Serve, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(MissCall, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Cancel, "#,##0") & "</td>")
                    ret.Append("        <td align='center' >" & Format(Incomplete, "#,##0") & "</td>")
                    If CountWT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumWT / CountWT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If CountHT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumHT / CountHT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If CountConT > 0 Then
                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(SumConT / CountConT) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    ret.Append("    </tr>")
                    'Next
                Next

                'Total Row by Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='left' >Total </td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='center' >" & Format(TCountQ, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TRegis, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TServe, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TCancel, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TIncomplete, "#,##0") & "</td>")
                If TCountWT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumWT / TCountWT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If TCountHT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumHT / TCountHT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If TCountConT > 0 Then
                    ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TSumConT / TCountConT) & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                ret.Append("    </tr>")
            Next
            sDt.Dispose()


            'Grand Total
            ret.Append("    <tr style='background: Orange; font-weight: bold;' >")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='left' >Grand Total</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >" & Format(GCountQ, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GRegis, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GServe, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMissCall, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GCancel, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GIncomplete, "#,##0") & "</td>")
            If GCountWT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumWT / GCountWT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            If GCountHT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumHT / GCountHT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            If GCountConT > 0 Then
                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GSumConT / GCountConT) & "</td>")
            Else
                ret.Append("        <td align='center' >00:00:00</td>")
            End If
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

    Private Sub RenderReportByTime(ByVal dt As DataTable, ByVal para As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;' >Skill</td>")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;' >Queue No.</td>")
            ret.Append("        <td align='center' rowspan='2' style='color: #ffffff;' >Mobile No.</td>")

            Dim TmpRow As String = ""
            Dim StartTime As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Split(para.TimePeroidFrom, ":")(0), Split(para.TimePeroidFrom, ":")(1), 0)
            Dim EndTime As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Split(para.TimePeroidTo, ":")(0), Split(para.TimePeroidTo, ":")(1), 0)
            Dim CurrTime As DateTime = StartTime
            Dim c As Integer = 0
            Do
                If CurrTime < EndTime Then
                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                    If TimeTo > EndTime Then
                        TimeTo = EndTime
                    End If
                    ret.Append("        <td align='center' colspan='3' style='color: #ffffff;' >" & CurrTime.ToString("HH:mm") & "-" & TimeTo.ToString("HH:mm") & "</td>")

                    TmpRow += "        <td align='center' style='color: #ffffff;' >WT</td>"
                    TmpRow += "        <td align='center' style='color: #ffffff;' >HT</td>"
                    TmpRow += "        <td align='center' style='color: #ffffff;' >ConT</td>"
                End If
                c += 1
                CurrTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
            Loop While CurrTime <= EndTime
            ret.Append("    </tr>")
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append(TmpRow)
            ret.Append("    </tr>")

            Dim GCountQ As Integer = 0
            Dim GSumWT(c) As Long
            Dim GSumHT(c) As Long
            Dim GSumConT(c) As Long
            Dim CGSumWT(c) As Long
            Dim CGSumHT(c) As Long
            Dim CGSumConT(c) As Long


            'DataRow
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TCountQ As Integer = 0
                Dim TSumWT(c) As Long
                Dim TSumHT(c) As Long
                Dim TSumConT(c) As Long
                Dim CTSumWT(c) As Long
                Dim CTSumHT(c) As Long
                Dim CTSumConT(c) As Long

                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "user_code", "staff_name")
                For Each stDr As DataRow In stDt.Rows

                    dt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "'"
                    Dim dDt As New DataTable
                    dDt = dt.DefaultView.ToTable(True, "show_date")
                    For Each dDr As DataRow In dDt.Rows
                        Dim CountQ As Integer = 0
                        Dim SumWT(c) As Long
                        Dim SumHT(c) As Long
                        Dim SumConT(c) As Long
                        Dim CSumWT(c) As Long
                        Dim CSumHT(c) As Long
                        Dim CSumConT(c) As Long

                        dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and show_date='" & dDr("show_date") & "' and user_code='" & stDr("user_code") & "'"
                        For Each dr As DataRowView In dt.DefaultView
                            ret.Append("    <tr  >")
                            ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                            ret.Append("        <td align='center' >&nbsp;" & dDr("show_date") & "</td>")
                            ret.Append("        <td align='left' >" & stDr("staff_name") & "</td>")
                            ret.Append("        <td align='left' >" & stDr("user_code") & "</td>")
                            ret.Append("        <td align='left' >" & dr("skill_name") & "</td>")
                            ret.Append("        <td align='center' >" & dr("queue_no") & "</td>")
                            ret.Append("        <td align='center' >&nbsp;" & dr("mobile_no") & "</td>")

                            Dim j As Integer = 0
                            CurrTime = StartTime
                            Do
                                If CurrTime < EndTime Then
                                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                                    If TimeTo > EndTime Then
                                        TimeTo = EndTime
                                    End If
                                    If CurrTime.ToString("HH:mm") & "-" & TimeTo.ToString("HH:mm") = dr("show_time") Then
                                        ret.Append("        <td align='center'  >" & dr("wt") & "</td>")
                                        ret.Append("        <td align='center'  >" & dr("ht") & "</td>")
                                        ret.Append("        <td align='center'  >" & dr("cont") & "</td>")

                                        If Convert.ToString(dr("wt")) <> "00:00:00" Then
                                            SumWT(j) += ReportsENG.GetSecFromTimeFormat(dr("wt"))
                                            TSumWT(j) += ReportsENG.GetSecFromTimeFormat(dr("wt"))
                                            GSumWT(j) += ReportsENG.GetSecFromTimeFormat(dr("wt"))

                                            CSumWT(j) += 1
                                            CTSumWT(j) += 1
                                            CGSumWT(j) += 1
                                        End If
                                        If Convert.ToString(dr("ht")) <> "00:00:00" Then
                                            SumHT(j) += ReportsENG.GetSecFromTimeFormat(dr("ht"))
                                            TSumHT(j) += ReportsENG.GetSecFromTimeFormat(dr("ht"))
                                            GSumHT(j) += ReportsENG.GetSecFromTimeFormat(dr("ht"))

                                            CSumHT(j) += 1
                                            CTSumHT(j) += 1
                                            CGSumHT(j) += 1
                                        End If
                                        If Convert.ToString(dr("cont")) <> "00:00:00" Then
                                            SumConT(j) += ReportsENG.GetSecFromTimeFormat(dr("cont"))
                                            TSumConT(j) += ReportsENG.GetSecFromTimeFormat(dr("cont"))
                                            GSumConT(j) += ReportsENG.GetSecFromTimeFormat(dr("cont"))

                                            CSumConT(j) += 1
                                            CTSumConT(j) += 1
                                            CGSumConT(j) += 1
                                        End If
                                    Else
                                        ret.Append("        <td align='center'  >00:00:00</td>")
                                        ret.Append("        <td align='center'  >00:00:00</td>")
                                        ret.Append("        <td align='center'  >00:00:00</td>")
                                    End If
                                End If
                                j += 1
                                CurrTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                            Loop While CurrTime <= EndTime
                            ret.Append("    </tr>")
                            CountQ += 1
                            TCountQ += 1
                            GCountQ += 1
                        Next

                        'Sub Total Row
                        ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;' >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dDr("show_date") & "</td>")
                        ret.Append("        <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;</td>")
                        ret.Append("        <td align='left' >&nbsp;</td>")
                        ret.Append("        <td align='center' >" & Format(CountQ, "#,##0") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;</td>")

                        Dim st As Integer = 0
                        CurrTime = StartTime
                        Do
                            If CurrTime < EndTime Then
                                Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                                If TimeTo > EndTime Then
                                    TimeTo = EndTime
                                End If
                                If CSumWT(st) > 0 Then
                                    ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(SumWT(st) / CSumWT(st), MidpointRounding.AwayFromZero)) & "</td>")
                                Else
                                    ret.Append("        <td align='center'  >&nbsp;00:00:00</td>")
                                End If
                                If CSumHT(st) > 0 Then
                                    ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(SumHT(st) / CSumHT(st), MidpointRounding.AwayFromZero)) & "</td>")
                                Else
                                    ret.Append("        <td align='center'  >&nbsp;00:00:00</td>")
                                End If
                                If CSumConT(st) > 0 Then
                                    ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(SumConT(st) / CSumConT(st), MidpointRounding.AwayFromZero)) & "</td>")
                                Else
                                    ret.Append("        <td align='center'  >&nbsp;00:00:00</td>")
                                End If
                            End If
                            st += 1
                            CurrTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                        Loop While CurrTime <= EndTime
                        ret.Append("    </tr>")
                    Next
                Next

                'Total Row by Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='left' >Total</td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='left' >&nbsp;</td>")
                ret.Append("        <td align='center' >" & Format(TCountQ, "#,##0") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")

                Dim tt As Integer = 0
                CurrTime = StartTime
                Do
                    If CurrTime < EndTime Then
                        Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                        If TimeTo > EndTime Then
                            TimeTo = EndTime
                        End If
                        If CTSumWT(tt) > 0 Then
                            ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(TSumWT(tt) / CTSumWT(tt), MidpointRounding.AwayFromZero)) & "</td>")
                        Else
                            ret.Append("        <td align='center'  >&nbsp;00:00:00:</td>")
                        End If
                        If CTSumHT(tt) > 0 Then
                            ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(TSumHT(tt) / CTSumHT(tt), MidpointRounding.AwayFromZero)) & "</td>")
                        Else
                            ret.Append("        <td align='center'  >&nbsp;00:00:00:</td>")
                        End If
                        If CTSumConT(tt) > 0 Then
                            ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(TSumConT(tt) / CTSumConT(tt), MidpointRounding.AwayFromZero)) & "</td>")
                        Else
                            ret.Append("        <td align='center'  >&nbsp;00:00:00:</td>")
                        End If
                    End If
                    tt += 1
                    CurrTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                Loop While CurrTime <= EndTime
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange; font-weight: bold;' >")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='left' >Grand Total</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='left' >&nbsp;</td>")
            ret.Append("        <td align='center' >" & Format(GCountQ, "#,##0") & "</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")

            Dim gt As Integer = 0
            CurrTime = StartTime
            Do
                If CurrTime < EndTime Then
                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                    If TimeTo > EndTime Then
                        TimeTo = EndTime
                    End If
                    If CGSumWT(gt) > 0 Then
                        ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(GSumWT(gt) / CGSumWT(gt), MidpointRounding.AwayFromZero)) & "</td>")
                    Else
                        ret.Append("        <td align='center'  >&nbsp;00:00:00</td>")
                    End If
                    If CGSumHT(gt) > 0 Then
                        ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(GSumHT(gt) / CGSumHT(gt), MidpointRounding.AwayFromZero)) & "</td>")
                    Else
                        ret.Append("        <td align='center'  >&nbsp;00:00:00</td>")
                    End If
                    If CGSumConT(gt) > 0 Then
                        ret.Append("        <td align='center'  >" & ReportsENG.GetFormatTimeFromSec(Math.Round(GSumConT(gt) / CGSumConT(gt), MidpointRounding.AwayFromZero)) & "</td>")
                    Else
                        ret.Append("        <td align='center'  >&nbsp;00:00:00</td>")
                    End If
                End If
                gt += 1
                CurrTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
            Loop While CurrTime <= EndTime
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

End Class
