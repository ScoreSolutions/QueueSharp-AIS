Imports System.IO
Imports System.Data

Partial Class ReportApp_repAvgServiceTimeKPIBySkill
    Inherits System.Web.UI.Page

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "Average_Service_Time_comparing_with_KPI_Report_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repAvgServiceTimeKPIBySkill.ExportData", Config.GetLoginHistoryID)
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
            Dim StartTime As DateTime = DateTime.Now
            ShowReport(Request("ReportName"))
        End If
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        Dim txtData As String = ""
        'If ReportName = "ByTime" Then
        '    Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
        '    para.ShopID = Request("ShopID")
        '    para.DateFrom = Request("DateFrom")
        '    para.DateTo = Request("DateTo")
        '    para.IntervalMinute = Request("Interval")
        '    para.TimePeroidFrom = Request("TimeFrom")
        '    para.TimePeroidTo = Request("TimeTo")
        '    Dim eng As New Engine.Reports.RepCustByWaitingTimeENG
        '    txtData = eng.RenderReportByTime(para)
        'ElseIf ReportName = "ByWeek" Then
        '    Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
        '    para.ShopID = Request("ShopID")
        '    para.WeekInYearFrom = Request("WeekFrom")
        '    para.WeekInYearTo = Request("WeekTo")
        '    para.YearFrom = Request("YearFrom")
        '    para.YearTo = Request("YearTo")
        '    Dim eng As New Engine.Reports.RepCustByWaitingTimeENG
        '    txtData = eng.RenderReportByWeek(para)
        'ElseIf ReportName = "ByDate" Then
        '    Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
        '    para.ShopID = Request("ShopID")
        '    para.DateFrom = Request("DateFrom")
        '    para.DateTo = Request("DateTo")
        '    Dim eng As New Engine.Reports.RepCustByWaitingTimeENG
        '    txtData = eng.RenderReportByDate(para)
        'ElseIf ReportName = "ByDay" Then
        '    Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
        '    para.ShopID = Request("ShopID")
        '    para.WeekInYearFrom = Request("WeekFrom")
        '    para.WeekInYearTo = Request("WeekTo")
        '    para.YearFrom = Request("YearFrom")
        '    para.YearTo = Request("YearTo")
        '    Dim eng As New Engine.Reports.RepCustByWaitingTimeENG
        '    txtData = eng.RenderReportByDay(para)
        'ElseIf ReportName = "ByMonth" Then
        '    Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
        '    para.ShopID = Request("ShopID")
        '    para.MonthFrom = Request("MonthFrom")
        '    para.MonthTo = Request("MonthTo")
        '    para.YearFrom = Request("YearFrom")
        '    para.YearTo = Request("YearTo")
        '    Dim eng As New Engine.Reports.RepCustByWaitingTimeENG
        '    txtData = eng.RenderReportByMonth(para)
        'ElseIf ReportName = "ByYear" Then
        '    Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
        '    para.ShopID = Request("ShopID")
        '    para.YearFrom = Request("YearFrom")
        '    para.YearTo = Request("YearTo")
        '    Dim eng As New Engine.Reports.RepCustByWaitingTimeENG
        '    txtData = eng.RenderReportByYear(para)
        'End If

        If ReportName = "ByTime" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            para.IntervalMinute = Request("Interval")
            para.TimePeroidFrom = Request("TimeFrom")
            para.TimePeroidTo = Request("TimeTo")
            'Dim eng As New Engine.Reports.RepWtHtBySkillENG
            'Dim dt As New DataTable
            'dt = eng.GetReportByTime(para)
            'If dt.Rows.Count > 0 Then
            '    RenderReportByTime(dt, para.IntervalMinute)
            'End If
            'dt.Dispose()
            'eng = Nothing
            'ElseIf ReportName = "ByDate" Then
            '    Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            '    para.ShopID = Request("ShopID")
            '    para.DateFrom = Request("DateFrom")
            '    para.DateTo = Request("DateTo")
            '    Dim eng As New Engine.Reports.RepAverageServiceTimeComparingWithKPI
            '    Dim dt As DataTable = eng.GetReportByDate(para)
            '    If dt.Rows.Count > 0 Then
            '        RenderReportByDate(dt)
            '    End If
            '    dt.Dispose()
            '    eng = Nothing
        End If
        If lblReportDesc.Text <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        Dim ret As String = ""
        If dt.Rows.Count > 0 Then
            ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
            'Header Row
            ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
            ret += "        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Service Type</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Date</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Year</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Regis</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Serve</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Missed Call</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Cancelled</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Incomplete</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Serve with KPI</td>"
            ret += "        <td align='center' style='color: #ffffff;' >%AWT within KPI</td>"
            ret += "        <td align='center' style='color: #ffffff;' >%AWT Over KPI</td>"
            ret += "        <td align='center' style='color: #ffffff;' >%AHT within KPI</td>"
            ret += "        <td align='center' style='color: #ffffff;' >%AHT Over KPI</td>"
            ret += "        <td align='center' style='color: #ffffff;' >%Missed Call</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Max WT</td>"
            ret += "        <td align='center' style='color: #ffffff;' >Max HT</td>"
            ret += "    </tr>"

            'For TotalRow
            Dim TotRegis As Long = 0
            Dim TotServe As Long = 0
            Dim TotMissedCall As Long = 0
            Dim TotCancelled As Long = 0
            Dim serve_with_kpi As Long = 0
            Dim per_awt_with_kpi As Long = 0
            Dim per_awt_over_kpi As Long = 0
            Dim per_aht_with_kpi As Long = 0
            Dim per_aht_over_kpi As Long = 0
            Dim per_missed_call As Long = 0
            Dim max_wt As Long = 0
            Dim max_ht As Long = 0

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                ret += "    <tr  >"
                ret += "        <td align='left' >" & dr("shop_name_en") & "</td>"
                ret += "        <td align='left' >" & dr("service_name") & "</td>"
                ret += "        <td align='center' >" & dr("show_date") & "</td>"
                ret += "        <td align='center' >" & dr("show_year") & "</td>"
                ret += "        <td align='right' >" & dr("regis") & "</td>"
                ret += "        <td align='right' >" & dr("served") & "</td>"
                ret += "        <td align='right' >" & dr("missed_call") & "</td>"
                ret += "        <td align='right' >" & dr("cancel") & "</td>"
                ret += "        <td align='right' >" & (dr("regis") - (dr("served") + dr("missed_call"))) & "</td>"
                ret += "        <td align='right' >" & dr("serve_with_kpi") & "</td>"
                ret += "        <td align='right' >" & dr("per_awt_with_kpi") & "%</td>"
                ret += "        <td align='right' >" & dr("per_awt_over_kpi") & "%</td>"
                ret += "        <td align='right' >" & dr("per_aht_with_kpi") & "%</td>"
                ret += "        <td align='right' >" & dr("per_aht_over_kpi") & "%</td>"
                ret += "        <td align='right' >" & dr("per_missed_call") & "%</td>"
                ret += "        <td align='right' >&nbsp;" & dr("max_wt") & "</td>"
                ret += "        <td align='right' >&nbsp;" & dr("max_ht") & "</td>"
                ret += "    </tr>"
                TotRegis += Convert.ToInt64(dr("regis"))
                TotServe += Convert.ToInt64(dr("served"))
                TotMissedCall += Convert.ToInt64(dr("missed_call"))
                TotCancelled += Convert.ToInt64(dr("cancel"))
                serve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))
                per_awt_with_kpi += Convert.ToInt64(dr("per_awt_with_kpi"))
                per_awt_over_kpi += Convert.ToInt64(dr("per_awt_over_kpi"))
                per_aht_with_kpi += Convert.ToInt64(dr("per_aht_with_kpi"))
                per_aht_over_kpi += Convert.ToInt64(dr("per_aht_over_kpi"))
                per_missed_call += Convert.ToInt64(dr("per_missed_call"))

                Dim MaxTime() As String = Split(dr("max_wt"), ":")
                If max_wt < (MaxTime(0) * 60) + MaxTime(1) Then
                    max_wt = (MaxTime(0) * 60) + MaxTime(1)
                End If

                Dim MaxTime2() As String = Split(dr("max_ht"), ":")
                If max_ht < (MaxTime2(0) * 60) + MaxTime2(1) Then
                    max_ht = (MaxTime2(0) * 60) + MaxTime2(1)
                End If
            Next
            ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
            ret += "        <td align='center' colspan='4'>Total</td>"
            ret += "        <td align='right' >" & TotRegis & "</td>"
            ret += "        <td align='right' >" & TotServe & "</td>"
            ret += "        <td align='right' >" & TotMissedCall & "</td>"
            ret += "        <td align='right' >" & TotCancelled & "</td>"
            ret += "        <td align='right' >" & TotRegis - (TotServe + TotMissedCall) & "</td>"
            ret += "        <td align='right' >" & serve_with_kpi & "</td>"
            ret += "        <td align='right' >" & Convert.ToInt16(per_awt_with_kpi / dt.Rows.Count) & "%</td>"
            ret += "        <td align='right' >" & Convert.ToInt16(per_awt_over_kpi / dt.Rows.Count) & "%</td>"
            ret += "        <td align='right' >" & Convert.ToInt16(per_aht_with_kpi / dt.Rows.Count) & "%</td>"
            ret += "        <td align='right' >" & Convert.ToInt16(per_aht_over_kpi / dt.Rows.Count) & "%</td>"
            ret += "        <td align='right' >" & Convert.ToInt16(per_missed_call / dt.Rows.Count) & "%</td>"

            Dim wMin As Integer = Math.Floor(max_wt / 60)
            Dim wSec As Integer = max_wt Mod 60
            ret += "        <td align='right' >&nbsp;" & wMin.ToString.PadLeft(2, "0") & ":" & wSec.ToString.PadLeft(2, "0") & "</td>"

            Dim wMin2 As Integer = Math.Floor(max_ht / 60)
            Dim wSec2 As Integer = max_ht Mod 60
            ret += "        <td align='right' >&nbsp;" & wMin2.ToString.PadLeft(2, "0") & ":" & wSec2.ToString.PadLeft(2, "0") & "</td>"

            ret += "    </tr>"
            ret += "</table>"
        End If

        If ret.Trim <> "" Then
            lblReportDesc.Text = ret
            lblerror.Visible = False
        End If
    End Sub
  
End Class
