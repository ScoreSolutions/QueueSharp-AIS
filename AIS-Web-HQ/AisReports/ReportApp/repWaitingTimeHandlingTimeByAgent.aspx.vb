Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repWaitingTimeHandlingTimeByAgent
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "WaitingTimeAndHandlingTimeByStaffReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repWaitingTimeHandlingTimeByAgent.ExportData", Config.GetLoginHistoryID)
        Response.ClearHeaders()
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
        'Dim txtData As String = ""
        Select ReportName
            Case "ByTime"
                Dim para As New CenParaDB.ReportCriteria.WaitingTimeHandlingTimeByAgentPara
                para.ShopID = Request("ShopID")
                para.DateFrom = Request("DateFrom")
                para.DateTo = Request("DateTo")
                para.TimePeroidFrom = Request("TimeFrom")
                para.TimePeroidTo = Request("TimeTo")
                para.IntervalMinute = Request("Interval")
                RenderReportByTime(para)
                
            Case "ByWeek"
                Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
                para.ShopID = Request("ShopID")
                para.WeekInYearFrom = Request("WeekFrom")
                para.WeekInYearTo = Request("WeekTo")
                para.YearFrom = Request("YearFrom")
                para.YearTo = Request("YearTo")
                RenderReportByWeek(para)
            Case "ByDate"
                Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
                para.ShopID = Request("ShopID")
                para.DateFrom = Request("DateFrom")
                para.DateTo = Request("DateTo")
                RenderReportByDate(para)
            Case "ByMonth"
                Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
                para.ShopID = Request("ShopID")
                para.MonthFrom = Request("MonthFrom")
                para.MonthTo = Request("MonthTo")
                para.YearFrom = Request("YearFrom")
                para.YearTo = Request("YearTo")
                RenderReportByMonth(para)
        End Select

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal para As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim eng As New Engine.Reports.RepWaitingTimeHandlingTimeByAgentENG

        Dim dt As DataTable = eng.GetReportDataMonth(para)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        If dt.Rows.Count > 0 Then
            Dim GNumQ As Long = 0
            Dim GRegis As Long = 0
            Dim GServe As Long = 0
            Dim GMissCall As Long = 0
            Dim GCancel As Long = 0
            Dim GIncomplete As Long = 0
            Dim GSumWT As Long = 0
            Dim GCountWT As Long = 0
            Dim GSumHT As Long = 0
            Dim GCountHT As Long = 0
            Dim GSumConT As Long = 0
            Dim GCountConT As Long = 0

            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Service</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Num of Queue</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Missed Call</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AWT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AHT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AConT</td>" & vbNewLine)
            ret.Append("    </tr>" & vbNewLine)

            'Loop By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TNumQ As Long = 0
                Dim TRegis As Long = 0
                Dim TServe As Long = 0
                Dim TMissCall As Long = 0
                Dim TCancel As Long = 0
                Dim TIncomplete As Long = 0
                Dim TSumWT As Long = 0
                Dim TCountWT As Long = 0
                Dim TSumHT As Long = 0
                Dim TCountHT As Long = 0
                Dim TSumConT As Long = 0
                Dim TCountConT As Long = 0


                'Loop By Staff
                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "user_code", "username", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    Dim SNumQ As Long = 0
                    Dim SRegis As Long = 0
                    Dim SServe As Long = 0
                    Dim SMissCall As Long = 0
                    Dim SCancel As Long = 0
                    Dim SIncomplete As Long = 0
                    Dim SSumWT As Long = 0
                    Dim SCountWT As Long = 0
                    Dim SSumHT As Long = 0
                    Dim SCountHT As Long = 0
                    Dim SSumConT As Long = 0
                    Dim SCountConT As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and user_code='" & stDr("user_code") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    </tr>" & vbNewLine)
                        ret.Append("        <td align='left'>" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>" & dr("show_month") & "</td>")
                        ret.Append("        <td align='left'>" & dr("show_year") & "</td>")
                        ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                        ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                        ret.Append("        <td align='left'>" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("number_of_queue"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("regis"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("serve"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("miss_call"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("cancel"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("not_call") + dr("not_con") + dr("not_end"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_wt")) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_ht")) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_cont")) & "</td>")
                        ret.Append("    </tr>")

                        SNumQ += Convert.ToInt32(dr("number_of_queue"))
                        SRegis += Convert.ToInt32(dr("regis"))
                        SServe += Convert.ToInt32(dr("serve"))
                        SMissCall += Convert.ToInt32(dr("miss_call"))
                        SCancel += Convert.ToInt32(dr("cancel"))
                        SIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        SSumWT += Convert.ToInt32(dr("sum_wt"))
                        SCountWT += Convert.ToInt32(dr("count_wt"))
                        SSumHT += Convert.ToInt32(dr("sum_ht"))
                        SCountHT += Convert.ToInt32(dr("count_ht"))
                        SSumConT += Convert.ToInt32(dr("sum_cont"))
                        SCountConT += Convert.ToInt32(dr("count_cont"))

                        TNumQ += Convert.ToInt32(dr("number_of_queue"))
                        TRegis += Convert.ToInt32(dr("regis"))
                        TServe += Convert.ToInt32(dr("serve"))
                        TMissCall += Convert.ToInt32(dr("miss_call"))
                        TCancel += Convert.ToInt32(dr("cancel"))
                        TIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        TSumWT += Convert.ToInt32(dr("sum_wt"))
                        TCountWT += Convert.ToInt32(dr("count_wt"))
                        TSumHT += Convert.ToInt32(dr("sum_ht"))
                        TCountHT += Convert.ToInt32(dr("count_ht"))
                        TSumConT += Convert.ToInt32(dr("sum_cont"))
                        TCountConT += Convert.ToInt32(dr("count_cont"))

                        GNumQ += Convert.ToInt32(dr("number_of_queue"))
                        GRegis += Convert.ToInt32(dr("regis"))
                        GServe += Convert.ToInt32(dr("serve"))
                        GMissCall += Convert.ToInt32(dr("miss_call"))
                        GCancel += Convert.ToInt32(dr("cancel"))
                        GIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        GSumWT += Convert.ToInt32(dr("sum_wt"))
                        GCountWT += Convert.ToInt32(dr("count_wt"))
                        GSumHT += Convert.ToInt32(dr("sum_ht"))
                        GCountHT += Convert.ToInt32(dr("count_ht"))
                        GSumConT += Convert.ToInt32(dr("sum_cont"))
                        GCountConT += Convert.ToInt32(dr("count_cont"))
                    Next

                    'Sub Total
                    ret.Append("    <tr style='background: #E4E4E4'>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SNumQ, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SRegis, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SServe, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SMissCall, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SCancel, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SIncomplete, "#,##0") & "</td>")
                    If SCountWT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumWT / SCountWT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                    If SCountHT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumHT / SCountHT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                    If SCountConT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumConT / SCountConT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='right'>" & Format(TNumQ, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TRegis, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TServe, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TMissCall, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TCancel, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TIncomplete, "#,##0") & "</td>")
                If TCountWT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumWT / TCountWT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
                If TCountHT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumHT / TCountHT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
                If TCountConT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumConT / TCountConT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
            Next

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='right'>" & Format(GNumQ, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GRegis, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GServe, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GMissCall, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GCancel, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GIncomplete, "#,##0") & "</td>")
            If GCountWT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumWT / GCountWT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            If GCountHT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumHT / GCountHT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            If GCountConT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumConT / GCountConT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
    End Sub

    Private Sub RenderReportByWeek(ByVal para As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim eng As New Engine.Reports.RepWaitingTimeHandlingTimeByAgentENG

        Dim dt As DataTable = eng.GetReportDataWeek(para)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        If dt.Rows.Count > 0 Then
            Dim GNumQ As Long = 0
            Dim GRegis As Long = 0
            Dim GServe As Long = 0
            Dim GMissCall As Long = 0
            Dim GCancel As Long = 0
            Dim GIncomplete As Long = 0
            Dim GSumWT As Long = 0
            Dim GCountWT As Long = 0
            Dim GSumHT As Long = 0
            Dim GCountHT As Long = 0
            Dim GSumConT As Long = 0
            Dim GCountConT As Long = 0

            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Week</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Service</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Num of Queue</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Missed Call</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AWT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AHT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AConT</td>" & vbNewLine)
            ret.Append("    </tr>" & vbNewLine)

            'Loop By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TNumQ As Long = 0
                Dim TRegis As Long = 0
                Dim TServe As Long = 0
                Dim TMissCall As Long = 0
                Dim TCancel As Long = 0
                Dim TIncomplete As Long = 0
                Dim TSumWT As Long = 0
                Dim TCountWT As Long = 0
                Dim TSumHT As Long = 0
                Dim TCountHT As Long = 0
                Dim TSumConT As Long = 0
                Dim TCountConT As Long = 0


                'Loop By Staff
                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "user_code", "username", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    Dim SNumQ As Long = 0
                    Dim SRegis As Long = 0
                    Dim SServe As Long = 0
                    Dim SMissCall As Long = 0
                    Dim SCancel As Long = 0
                    Dim SIncomplete As Long = 0
                    Dim SSumWT As Long = 0
                    Dim SCountWT As Long = 0
                    Dim SSumHT As Long = 0
                    Dim SCountHT As Long = 0
                    Dim SSumConT As Long = 0
                    Dim SCountConT As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and user_code='" & stDr("user_code") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    </tr>" & vbNewLine)
                        ret.Append("        <td align='left'>" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>" & dr("week_of_year") & "</td>")
                        ret.Append("        <td align='left'>" & dr("show_year") & "</td>")
                        ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                        ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                        ret.Append("        <td align='left'>" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("number_of_queue"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("regis"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("serve"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("miss_call"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("cancel"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("not_call") + dr("not_con") + dr("not_end"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_wt")) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_ht")) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_cont")) & "</td>")
                        ret.Append("    </tr>")

                        SNumQ += Convert.ToInt32(dr("number_of_queue"))
                        SRegis += Convert.ToInt32(dr("regis"))
                        SServe += Convert.ToInt32(dr("serve"))
                        SMissCall += Convert.ToInt32(dr("miss_call"))
                        SCancel += Convert.ToInt32(dr("cancel"))
                        SIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        SSumWT += Convert.ToInt32(dr("sum_wt"))
                        SCountWT += Convert.ToInt32(dr("count_wt"))
                        SSumHT += Convert.ToInt32(dr("sum_ht"))
                        SCountHT += Convert.ToInt32(dr("count_ht"))
                        SSumConT += Convert.ToInt32(dr("sum_cont"))
                        SCountConT += Convert.ToInt32(dr("count_cont"))

                        TNumQ += Convert.ToInt32(dr("number_of_queue"))
                        TRegis += Convert.ToInt32(dr("regis"))
                        TServe += Convert.ToInt32(dr("serve"))
                        TMissCall += Convert.ToInt32(dr("miss_call"))
                        TCancel += Convert.ToInt32(dr("cancel"))
                        TIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        TSumWT += Convert.ToInt32(dr("sum_wt"))
                        TCountWT += Convert.ToInt32(dr("count_wt"))
                        TSumHT += Convert.ToInt32(dr("sum_ht"))
                        TCountHT += Convert.ToInt32(dr("count_ht"))
                        TSumConT += Convert.ToInt32(dr("sum_cont"))
                        TCountConT += Convert.ToInt32(dr("count_cont"))

                        GNumQ += Convert.ToInt32(dr("number_of_queue"))
                        GRegis += Convert.ToInt32(dr("regis"))
                        GServe += Convert.ToInt32(dr("serve"))
                        GMissCall += Convert.ToInt32(dr("miss_call"))
                        GCancel += Convert.ToInt32(dr("cancel"))
                        GIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        GSumWT += Convert.ToInt32(dr("sum_wt"))
                        GCountWT += Convert.ToInt32(dr("count_wt"))
                        GSumHT += Convert.ToInt32(dr("sum_ht"))
                        GCountHT += Convert.ToInt32(dr("count_ht"))
                        GSumConT += Convert.ToInt32(dr("sum_cont"))
                        GCountConT += Convert.ToInt32(dr("count_cont"))
                    Next

                    'Sub Total
                    ret.Append("    <tr style='background: #E4E4E4'>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SNumQ, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SRegis, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SServe, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SMissCall, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SCancel, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SIncomplete, "#,##0") & "</td>")
                    If SCountWT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumWT / SCountWT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                    If SCountHT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumHT / SCountHT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                    If SCountConT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumConT / SCountConT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='right'>" & Format(TNumQ, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TRegis, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TServe, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TMissCall, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TCancel, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TIncomplete, "#,##0") & "</td>")
                If TCountWT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumWT / TCountWT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
                If TCountHT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumHT / TCountHT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
                If TCountConT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumConT / TCountConT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
            Next

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='right'>" & Format(GNumQ, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GRegis, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GServe, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GMissCall, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GCancel, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GIncomplete, "#,##0") & "</td>")
            If GCountWT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumWT / GCountWT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            If GCountHT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumHT / GCountHT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            If GCountConT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumConT / GCountConT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
    End Sub

    Private Sub RenderReportByDate(ByVal para As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim eng As New Engine.Reports.RepWaitingTimeHandlingTimeByAgentENG

        Dim dt As DataTable = eng.GetReportDataDate(para)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        If dt.Rows.Count > 0 Then
            Dim GNumQ As Long = 0
            Dim GRegis As Long = 0
            Dim GServe As Long = 0
            Dim GMissCall As Long = 0
            Dim GCancel As Long = 0
            Dim GIncomplete As Long = 0
            Dim GSumWT As Long = 0
            Dim GCountWT As Long = 0
            Dim GSumHT As Long = 0
            Dim GCountHT As Long = 0
            Dim GSumConT As Long = 0
            Dim GCountConT As Long = 0

            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Service</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Num of Queue</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Missed Call</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AWT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AHT</td>" & vbNewLine)
            ret.Append("        <td align='center' style='color: #ffffff;' >AConT</td>" & vbNewLine)
            ret.Append("    </tr>" & vbNewLine)

            'Loop By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TNumQ As Long = 0
                Dim TRegis As Long = 0
                Dim TServe As Long = 0
                Dim TMissCall As Long = 0
                Dim TCancel As Long = 0
                Dim TIncomplete As Long = 0
                Dim TSumWT As Long = 0
                Dim TCountWT As Long = 0
                Dim TSumHT As Long = 0
                Dim TCountHT As Long = 0
                Dim TSumConT As Long = 0
                Dim TCountConT As Long = 0


                'Loop By Staff
                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "user_code", "username", "staff_name")
                For Each stDr As DataRow In stDt.Rows
                    Dim SNumQ As Long = 0
                    Dim SRegis As Long = 0
                    Dim SServe As Long = 0
                    Dim SMissCall As Long = 0
                    Dim SCancel As Long = 0
                    Dim SIncomplete As Long = 0
                    Dim SSumWT As Long = 0
                    Dim SCountWT As Long = 0
                    Dim SSumHT As Long = 0
                    Dim SCountHT As Long = 0
                    Dim SSumConT As Long = 0
                    Dim SCountConT As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "' and user_code='" & stDr("user_code") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    </tr>" & vbNewLine)
                        ret.Append("        <td align='left'>" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                        ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                        ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                        ret.Append("        <td align='left'>" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("number_of_queue"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("regis"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("serve"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("miss_call"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("cancel"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("not_call") + dr("not_con") + dr("not_end"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_wt")) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_ht")) & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("avg_cont")) & "</td>")
                        ret.Append("    </tr>")

                        SNumQ += Convert.ToInt32(dr("number_of_queue"))
                        SRegis += Convert.ToInt32(dr("regis"))
                        SServe += Convert.ToInt32(dr("serve"))
                        SMissCall += Convert.ToInt32(dr("miss_call"))
                        SCancel += Convert.ToInt32(dr("cancel"))
                        SIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        SSumWT += Convert.ToInt32(dr("sum_wt"))
                        SCountWT += Convert.ToInt32(dr("count_wt"))
                        SSumHT += Convert.ToInt32(dr("sum_ht"))
                        SCountHT += Convert.ToInt32(dr("count_ht"))
                        SSumConT += Convert.ToInt32(dr("sum_cont"))
                        SCountConT += Convert.ToInt32(dr("count_cont"))

                        TNumQ += Convert.ToInt32(dr("number_of_queue"))
                        TRegis += Convert.ToInt32(dr("regis"))
                        TServe += Convert.ToInt32(dr("serve"))
                        TMissCall += Convert.ToInt32(dr("miss_call"))
                        TCancel += Convert.ToInt32(dr("cancel"))
                        TIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        TSumWT += Convert.ToInt32(dr("sum_wt"))
                        TCountWT += Convert.ToInt32(dr("count_wt"))
                        TSumHT += Convert.ToInt32(dr("sum_ht"))
                        TCountHT += Convert.ToInt32(dr("count_ht"))
                        TSumConT += Convert.ToInt32(dr("sum_cont"))
                        TCountConT += Convert.ToInt32(dr("count_cont"))

                        GNumQ += Convert.ToInt32(dr("number_of_queue"))
                        GRegis += Convert.ToInt32(dr("regis"))
                        GServe += Convert.ToInt32(dr("serve"))
                        GMissCall += Convert.ToInt32(dr("miss_call"))
                        GCancel += Convert.ToInt32(dr("cancel"))
                        GIncomplete += Convert.ToInt32(dr("not_call")) + Convert.ToInt32(dr("not_con")) + Convert.ToInt32(dr("not_end"))
                        GSumWT += Convert.ToInt32(dr("sum_wt"))
                        GCountWT += Convert.ToInt32(dr("count_wt"))
                        GSumHT += Convert.ToInt32(dr("sum_ht"))
                        GCountHT += Convert.ToInt32(dr("count_ht"))
                        GSumConT += Convert.ToInt32(dr("sum_cont"))
                        GCountConT += Convert.ToInt32(dr("count_cont"))
                    Next

                    'Sub Total
                    ret.Append("    <tr style='background: #E4E4E4'>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='left' >Sub Total by " & stDr("staff_name") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SNumQ, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SRegis, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SServe, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SMissCall, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SCancel, "#,##0") & "</td>")
                    ret.Append("         <td align='right'>" & Format(SIncomplete, "#,##0") & "</td>")
                    If SCountWT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumWT / SCountWT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                    If SCountHT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumHT / SCountHT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                    If SCountConT > 0 Then
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(SSumConT / SCountConT) & "</td>")
                    Else
                        ret.Append("        <td align='right'>00:00:00</td>")
                    End If
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='left' >Total " & sDr("shop_name_en") & "</td>")
                ret.Append("         <td align='right'>" & Format(TNumQ, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TRegis, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TServe, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TMissCall, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TCancel, "#,##0") & "</td>")
                ret.Append("         <td align='right'>" & Format(TIncomplete, "#,##0") & "</td>")
                If TCountWT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumWT / TCountWT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
                If TCountHT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumHT / TCountHT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
                If TCountConT > 0 Then
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(TSumConT / TCountConT) & "</td>")
                Else
                    ret.Append("        <td align='right'>00:00:00</td>")
                End If
            Next

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='left' >Grand Total</td>")
            ret.Append("         <td align='right'>" & Format(GNumQ, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GRegis, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GServe, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GMissCall, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GCancel, "#,##0") & "</td>")
            ret.Append("         <td align='right'>" & Format(GIncomplete, "#,##0") & "</td>")
            If GCountWT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumWT / GCountWT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            If GCountHT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumHT / GCountHT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            If GCountConT > 0 Then
                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(GSumConT / GCountConT) & "</td>")
            Else
                ret.Append("        <td align='right'>00:00:00</td>")
            End If
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        dt.Dispose()
    End Sub

    Private Sub RenderReportByTime(ByVal para As CenParaDB.ReportCriteria.WaitingTimeHandlingTimeByAgentPara)
        Dim eng As New Engine.Reports.RepWaitingTimeHandlingTimeByAgentENG

        Dim dt As DataTable = eng.GetReportDataTime(para)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        If dt.Rows.Count > 0 Then
            Dim ShopID As Long = dt.Rows(0)("shop_id")
            Dim StaffName As String = dt.Rows(0)("staff_name")
            Dim ShopName As String = ""
            lblerror.Visible = False

            Dim TotAwt() As Long
            Dim TotAht() As Long
            Dim TotACt() As Long
            Dim RecTotAwt() As Long
            Dim RecTotAht() As Long
            Dim RecTotACt() As Long
            Dim CountRec As Int32 = 0

            Dim STotAwt() As Long
            Dim STotAht() As Long
            Dim STotACt() As Long
            Dim SRecTotAwt() As Long
            Dim SRecTotAht() As Long
            Dim SRecTotACt() As Long
            Dim SCountRec As Int32 = 0

            Dim GTotAwt() As Long
            Dim GTotAht() As Long
            Dim GTotACt() As Long
            Dim GRecTotAwt() As Long
            Dim GRecTotAht() As Long
            Dim GRecTotACt() As Long
            Dim GCountRec As Int32 = 0

            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >" & vbNewLine)
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Shop Name</td>" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Date</td>" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Staff Name</td>" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >EmpID</td>" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Service</td>" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Queue No</td>" & vbNewLine)
            ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Mobile No</td>" & vbNewLine)
            'Loop ตามเวลาเพื่อสร้างชื่อคอลัมน์
            Dim k As Int16 = 0
            Dim hd2 As String = ""   'ชื่อคอลัมน์ แถวที่ 2
            Dim StartTime As DateTime = para.TimePeroidFrom
            Dim EndTime As DateTime = para.TimePeroidTo
            Dim CurrTime As DateTime = StartTime
            Do
                If CurrTime < EndTime Then
                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
                    If TimeTo > EndTime Then
                        TimeTo = EndTime
                    End If

                    ret.Append("<td align='center' style='color: #ffffff;' colspan='3' >" & CurrTime.ToString("HH:mm") & "-" & TimeTo.ToString("HH:mm") & "</td>" & vbNewLine)
                    hd2 += "<td align='center' style='color: #ffffff;' >WT</td>" & vbNewLine
                    hd2 += "<td align='center' style='color: #ffffff;' >HT</td>" & vbNewLine
                    hd2 += "<td align='center' style='color: #ffffff;' >ConT</td>" & vbNewLine
                    k += 1
                End If
                CurrTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CurrTime)
            Loop While CurrTime < EndTime
            ret.Append("    </tr>" & vbNewLine)
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>" & vbNewLine)
            ret.Append(hd2)
            ret.Append("    </tr>" & vbNewLine)

            ReDim TotAwt(k)
            ReDim TotAht(k)
            ReDim TotACt(k)
            ReDim RecTotAwt(k)
            ReDim RecTotAht(k)
            ReDim RecTotACt(k)

            ReDim STotAwt(k)
            ReDim STotAht(k)
            ReDim STotACt(k)
            ReDim SRecTotAwt(k)
            ReDim SRecTotAht(k)
            ReDim SRecTotACt(k)

            ReDim GTotAwt(k)
            ReDim GTotAht(k)
            ReDim GTotACt(k)
            ReDim GRecTotAwt(k)
            ReDim GRecTotAht(k)
            ReDim GRecTotACt(k)

            'Data Row
            For i As Integer = 0 To dt.Rows.Count - 1
                Try


                    Dim dr As DataRow = dt.Rows(i)
                    ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                    ret.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                    ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                    ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                    ret.Append("        <td align='left'>" & dr("service_type") & "</td>")
                    ret.Append("        <td align='left'>" & dr("queue_no") & "</td>")
                    ret.Append("        <td align='left'>" & "&nbsp;" & dr("mobile_no") & "</td>")

                    Dim m As Integer = 0
                    Dim STime As DateTime = para.TimePeroidFrom
                    Dim ETime As DateTime = para.TimePeroidTo
                    Dim CTime As DateTime = STime

                    CountRec += 1
                    SCountRec += 1
                    GCountRec += 1
                    Do
                        If CTime < ETime Then
                            Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CTime)
                            If TimeTo > ETime Then
                                TimeTo = ETime
                            End If
                            Dim tmpTime As String = Convert.ToDateTime(dr("service_date")).ToString("HH:mm", New Globalization.CultureInfo("en-US"))
                            If CTime.ToString("HH:mm") < tmpTime And TimeTo.ToString("HH:mm") >= tmpTime Then
                                ret.Append("<td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("awt")) & "</td>")
                                ret.Append("<td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("aht")) & "</td>")
                                ret.Append("<td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("act")) & "</td>")

                                TotAwt(m) += Convert.ToInt64(dr("awt"))
                                TotAht(m) += Convert.ToInt64(dr("aht"))
                                TotACt(m) += Convert.ToInt64(dr("act"))
                                RecTotAwt(m) += Convert.ToInt64(dr("cwt"))
                                RecTotAht(m) += Convert.ToInt64(dr("cht"))
                                RecTotACt(m) += Convert.ToInt64(dr("cct"))

                                STotAwt(m) += Convert.ToInt64(dr("awt"))
                                STotAht(m) += Convert.ToInt64(dr("aht"))
                                STotACt(m) += Convert.ToInt64(dr("act"))
                                SRecTotAwt(m) += Convert.ToInt64(dr("cwt"))
                                SRecTotAht(m) += Convert.ToInt64(dr("cht"))
                                SRecTotACt(m) += Convert.ToInt64(dr("cct"))

                                GTotAwt(m) += Convert.ToInt64(dr("awt"))
                                GTotAht(m) += Convert.ToInt64(dr("aht"))
                                GTotACt(m) += Convert.ToInt64(dr("act"))
                                GRecTotAwt(m) += Convert.ToInt64(dr("cwt"))
                                GRecTotAht(m) += Convert.ToInt64(dr("cht"))
                                GRecTotACt(m) += Convert.ToInt64(dr("cct"))
                            Else
                                ret.Append("<td align='right' >00:00:00</td>")
                                ret.Append("<td align='right' >00:00:00</td>")
                                ret.Append("<td align='right' >00:00:00</td>")
                            End If
                        End If
                        m += 1
                        CTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CTime)
                    Loop While CTime < EndTime

                    ret.Append("    </tr>")

                    ShopID = dr("shop_id")
                    StaffName = dr("staff_name")


                    '************************ Sub Total **************************
                    Dim ChkSubTotal As Boolean = False
                    If dt.Rows.Count = i + 1 Then
                        ChkSubTotal = True
                    Else
                        If dt.Rows(i).Item("staff_name") <> dt.Rows(i + 1).Item("staff_name") Then
                            ChkSubTotal = True
                            ShopName = dt.Rows(i).Item("shop_name_en").ToString
                        End If
                    End If
                    If ChkSubTotal = True Then
                        ret.Append("    <tr style='background: #E4E4E4'>")
                        ret.Append("         <td align='center' colspan='5' >Sub Total</td>")
                        ret.Append("         <td align='right' >" & CountRec & "</td>")
                        ret.Append("         <td align='right' ></td>")
                        Dim n As Integer = 0
                        Dim SToime As DateTime = para.TimePeroidFrom
                        Dim EToime As DateTime = para.TimePeroidTo
                        Dim CToime As DateTime = SToime
                        Do
                            If CToime < EToime Then
                                Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, para.IntervalMinute, CToime)
                                If TimeTo > EToime Then
                                    TimeTo = EToime
                                End If

                                'AWT
                                If TotAwt(n) = 0 Then
                                    ret.Append("<td align='right' >" & "00:00:00" & "</td>")
                                Else
                                    ret.Append("<td align='right' >" & ReportsENG.GetFormatTimeFromSec(Math.Round(TotAwt(n) / RecTotAwt(n))) & "</td>")
                                End If

                                'AHT
                                If TotAht(n) = 0 Then
                                    ret.Append("<td align='right' >" & "00:00:00" & "</td>")
                                Else
                                    ret.Append("<td align='right' >" & ReportsENG.GetFormatTimeFromSec(Math.Round(TotAht(n) / RecTotAht(n))) & "</td>")
                                End If

                                'ConT
                                If TotACt(n) = 0 Then
                                    ret.Append("<td align='right' >" & "00:00:00" & "</td>")
                                Else
                                    ret.Append("<td align='right' >" & ReportsENG.GetFormatTimeFromSec(Math.Round(TotACt(n) / RecTotACt(n))) & "</td>")
                                End If
                            End If

                            TotAwt(n) = 0
                            TotAht(n) = 0
                            TotACt(n) = 0
                            RecTotAwt(n) = 0
                            RecTotAht(n) = 0
                            RecTotACt(n) = 0

                            n += 1
                            CToime = DateAdd(DateInterval.Minute, para.IntervalMinute, CToime)

                        Loop While CToime < EndTime
                        ret.Append("    </tr>")
                        CountRec = 0
                    End If
                    '**********************************************************
                    '************************* Total **************************
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
                        ret.Append("         <td align='center' colspan='5' >Total " & ShopName & "</td>")
                        ret.Append("         <td align='right'>" & SCountRec & "</td>")
                        ret.Append("         <td align='right'></td>")
                        For j As Int32 = 0 To SRecTotAwt.Length - 2
                            If STotAwt(j) = 0 Then
                                ret.Append("<td align='right'>" & "00:00:00" & "</td>")
                            Else
                                ret.Append("<td align='right'>" & ReportsENG.GetFormatTimeFromSec(Math.Round(STotAwt(j) / SRecTotAwt(j))) & "</td>")
                            End If
                            If STotAht(j) = 0 Then
                                ret.Append("<td align='right'>" & "00:00:00" & "</td>")
                            Else
                                ret.Append("<td align='right'>" & ReportsENG.GetFormatTimeFromSec(Math.Round(STotAht(j) / SRecTotAht(j))) & "</td>")
                            End If
                            If STotACt(j) = 0 Then
                                ret.Append("<td align='right'>" & "00:00:00" & "</td>")
                            Else
                                ret.Append("<td align='right'>" & ReportsENG.GetFormatTimeFromSec(Math.Round(STotACt(j) / SRecTotACt(j))) & "</td>")
                            End If

                            STotAwt(j) = 0
                            STotAht(j) = 0
                            STotACt(j) = 0
                            SRecTotAwt(j) = 0
                            SRecTotAht(j) = 0
                            SRecTotACt(j) = 0
                        Next
                        ret.Append("    </tr>")
                        SCountRec = 0
                    End If
                    '**********************************************************
                Catch ex As Exception

                End Try
            Next

            '************************ Grand Total **************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("         <td align='center' colspan='5' >Grand Total</td>")
            ret.Append("         <td align='right' >" & GCountRec & "</td>")
            ret.Append("         <td align='right' ></td>")
            For i As Int32 = 0 To GRecTotAwt.Length - 2
                If GTotAwt(i) = 0 Then
                    ret.Append("<td align='right' >" & "00:00:00" & "</td>")
                Else
                    ret.Append("<td align='right' >" & ReportsENG.GetFormatTimeFromSec(Math.Round(GTotAwt(i) / GRecTotAwt(i))) & "</td>")
                End If
                If GTotAht(i) = 0 Then
                    ret.Append("<td align='right' >" & "00:00:00" & "</td>")
                Else
                    ret.Append("<td align='right' >" & ReportsENG.GetFormatTimeFromSec(Math.Round(GTotAht(i) / GRecTotAht(i))) & "</td>")
                End If
                If GTotACt(i) = 0 Then
                    ret.Append("<td align='right' >" & "00:00:00" & "</td>")
                Else
                    ret.Append("<td align='right' >" & ReportsENG.GetFormatTimeFromSec(Math.Round(GTotACt(i) / GRecTotACt(i))) & "</td>")
                End If
            Next

            ret.Append("    </tr>")
            '**********************************************************
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        eng = Nothing
        dt.Dispose()
    End Sub

End Class
