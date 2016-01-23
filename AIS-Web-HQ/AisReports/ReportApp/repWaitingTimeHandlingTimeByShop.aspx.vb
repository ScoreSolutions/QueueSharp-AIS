Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repWaitingTimeHandlingTimeByShop
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "WaitingTimeAndHandlingTimeByShopReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repWaitingTimeHandlingTimeByShop.ExportData", Config.GetLoginHistoryID)
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
        Select Case ReportName
            Case "ByTime"
                'Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
                'para.ShopID = Request("ShopID")
                'para.DateFrom = Request("DateFrom")
                'para.DateTo = Request("DateTo")
                'para.IntervalMinute = Request("Interval")
                'para.TimePeroidFrom = Request("TimeFrom")
                'para.TimePeroidTo = Request("TimeTo")
                'txtData = RenderReportByTime(para)

                Dim para As New CenParaDB.ReportCriteria.CustByWaitingTimePara
                para.ShopID = Request("ShopID")
                para.DateFrom = Request("DateFrom")
                para.DateTo = Request("DateTo")
                para.IntervalMinute = Request("Interval")
                para.TimePeroidFrom = Request("TimeFrom")
                para.TimePeroidTo = Request("TimeTo")
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

    Private Sub RenderReportByDate(ByVal para As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopDayCenLinqDB

        Dim WhText As String = "shop_id in (" & para.ShopID & ")"
        WhText += " and convert(varchar(8),service_date,112) >= '" & para.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        WhText += " and convert(varchar(8),service_date,112) <= '" & para.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date,service_id", trans.Trans)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder

            Dim svDt As New DataTable   'Service ที่ดึงมาทั้งหมด
            svDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy

            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")

            Dim tmpCol As String = ""
            For Each svDr As DataRow In svDt.Rows
                'ret.Append("        <td colspan='8' align='center' style='color: #ffffff;' >" & svDr("service_name") & "</td>")

                'Second Row Header
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Regis</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Serve</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Missed call</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Cancel</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Incomplete</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AWT</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AHT</td>"
                tmpCol += "        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AvgConT</td>"
            Next
            'ret.Append("    </tr>")
            'ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
            ret.Append(tmpCol)
            ret.Append("    </tr>")


            Dim GTRegis(svDt.Rows.Count) As Long
            Dim GTServe(svDt.Rows.Count) As Long
            Dim GTMissCall(svDt.Rows.Count) As Long
            Dim GTCancel(svDt.Rows.Count) As Long
            Dim GTIncomplete(svDt.Rows.Count) As Long
            Dim GTCntAwt(svDt.Rows.Count) As Long
            Dim GTSumAwt(svDt.Rows.Count) As Long
            Dim GTCntAht(svDt.Rows.Count) As Long
            Dim GTSumAht(svDt.Rows.Count) As Long
            Dim GTCntAct(svDt.Rows.Count) As Long
            Dim GTSumAct(svDt.Rows.Count) As Long

            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            For Each shDr As DataRow In shDt.Rows
                Dim TotRegis(svDt.Rows.Count) As Integer
                Dim TotServe(svDt.Rows.Count) As Integer
                Dim TotMissCall(svDt.Rows.Count) As Integer
                Dim TotCancel(svDt.Rows.Count) As Integer
                Dim TotIncomplete(svDt.Rows.Count) As Integer
                Dim TotCntAwt(svDt.Rows.Count) As Long
                Dim TotSumAwt(svDt.Rows.Count) As Long
                Dim TotCntAht(svDt.Rows.Count) As Long
                Dim TotSumAht(svDt.Rows.Count) As Long
                Dim TotCntAct(svDt.Rows.Count) As Long
                Dim TotSumAct(svDt.Rows.Count) As Long

                Dim dDt As New DataTable
                dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                dDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en", "show_date", "service_date")
                For Each dDr As DataRow In dDt.Rows
                    ret.Append("    <tr>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='right'>&nbsp;" & Convert.ToDateTime(dDr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                    For i As Integer = 0 To svDt.Rows.Count - 1
                        dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "' and service_id = '" & svDt.Rows(i)("service_id") & "' and show_date='" & dDr("show_date") & "'"
                        Dim dv As DataView = dt.DefaultView
                        If dv.Count > 0 Then
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("regis")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("serve")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("missed_call")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("cancle")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("incomplete")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("awt")) & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("aht")) & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("act")) & "</td>")

                            TotRegis(i) += Convert.ToInt64(dv(0)("regis"))
                            TotServe(i) += Convert.ToInt64(dv(0)("serve"))
                            TotMissCall(i) += Convert.ToInt64(dv(0)("missed_call"))
                            TotCancel(i) += Convert.ToInt64(dv(0)("cancle"))
                            TotIncomplete(i) += Convert.ToInt64(dv(0)("incomplete"))
                            TotCntAwt(i) += Convert.ToInt64(dv(0)("count_wt"))
                            TotSumAwt(i) += Convert.ToInt64(dv(0)("sum_wt"))
                            TotCntAht(i) += Convert.ToInt64(dv(0)("count_ht"))
                            TotSumAht(i) += Convert.ToInt64(dv(0)("sum_ht"))
                            TotCntAct(i) += Convert.ToInt64(dv(0)("count_ct"))
                            TotSumAct(i) += Convert.ToInt64(dv(0)("sum_ct"))
                        End If
                        dv = Nothing
                        dt.DefaultView.RowFilter = ""
                    Next
                    ret.Append("    </tr>")
                Next
                dDt = Nothing

                'Total Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='2'>Total " & shDr("shop_name_en") & "</td>")
                For i As Integer = 0 To svDt.Rows.Count - 1
                    ret.Append("        <td align='right'>" & Format(TotRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotIncomplete(i), "#,##0") & "</td>")
                    If TotCntAwt(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAwt(i) / TotCntAwt(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If
                    If TotCntAht(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAht(i) / TotCntAht(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If
                    If TotCntAct(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAct(i) / TotCntAct(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If

                    GTRegis(i) += TotRegis(i)
                    GTServe(i) += TotServe(i)
                    GTMissCall(i) += TotMissCall(i)
                    GTCancel(i) += TotCancel(i)
                    GTIncomplete(i) += TotIncomplete(i)
                    GTCntAwt(i) += TotCntAwt(i)
                    GTSumAwt(i) += TotSumAwt(i)
                    GTCntAht(i) += TotCntAht(i)
                    GTSumAht(i) += TotSumAht(i)
                    GTCntAct(i) += TotCntAct(i)
                    GTSumAct(i) += TotSumAct(i)
                Next
                ret.Append("    </tr>")
            Next

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' colspan='2'>Grand Total</td>")
            For i As Integer = 0 To svDt.Rows.Count - 1
                ret.Append("        <td align='right'>" & Format(GTRegis(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTServe(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTMissCall(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTCancel(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTIncomplete(i), "#,##0") & "</td>")

                If GTCntAwt(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAwt(i) / GTCntAwt(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
                If GTCntAht(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAht(i) / GTCntAht(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
                If GTCntAct(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAct(i) / GTCntAct(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
            Next
            ret.Append("    </tr>")

            svDt = Nothing
            shDt = Nothing
            ret.Append("</table>")
            dt.Dispose()

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        lnq = Nothing
        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal para As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopWeekCenLinqDB

        Dim WhText As String = "shop_id in (" & para.ShopID & ")"
        WhText += " and convert(varchar,show_year) + right('0'+ convert(varchar,week_of_year),2)  between '" & para.YearFrom & Right("0" & para.WeekInYearFrom, 2) & "' and '" & para.YearTo & Right("0" & para.WeekInYearTo, 2) & "'"
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, week_of_year,service_id", trans.Trans)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder

            Dim svDt As New DataTable   'Service ที่ดึงมาทั้งหมด
            svDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy

            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Week No.</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            For Each svDr As DataRow In svDt.Rows
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Regis</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Serve</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Missed call</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Cancel</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Incomplete</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AWT</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AHT</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AvgConT</td>")
            Next
            ret.Append("    </tr>")

            Dim GTRegis(svDt.Rows.Count) As Long
            Dim GTServe(svDt.Rows.Count) As Long
            Dim GTMissCall(svDt.Rows.Count) As Long
            Dim GTCancel(svDt.Rows.Count) As Long
            Dim GTIncomplete(svDt.Rows.Count) As Long
            Dim GTCntAwt(svDt.Rows.Count) As Long
            Dim GTSumAwt(svDt.Rows.Count) As Long
            Dim GTCntAht(svDt.Rows.Count) As Long
            Dim GTSumAht(svDt.Rows.Count) As Long
            Dim GTCntAct(svDt.Rows.Count) As Long
            Dim GTSumAct(svDt.Rows.Count) As Long

            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            For Each shDr As DataRow In shDt.Rows
                Dim TotRegis(svDt.Rows.Count) As Integer
                Dim TotServe(svDt.Rows.Count) As Integer
                Dim TotMissCall(svDt.Rows.Count) As Integer
                Dim TotCancel(svDt.Rows.Count) As Integer
                Dim TotIncomplete(svDt.Rows.Count) As Integer
                Dim TotCntAwt(svDt.Rows.Count) As Long
                Dim TotSumAwt(svDt.Rows.Count) As Long
                Dim TotCntAht(svDt.Rows.Count) As Long
                Dim TotSumAht(svDt.Rows.Count) As Long
                Dim TotCntAct(svDt.Rows.Count) As Long
                Dim TotSumAct(svDt.Rows.Count) As Long

                Dim dDt As New DataTable
                dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                dDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en", "week_of_year", "show_year")
                For Each dDr As DataRow In dDt.Rows
                    ret.Append("    <tr>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("week_of_year") & "</td>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("show_year") & "</td>")
                    For i As Integer = 0 To svDt.Rows.Count - 1
                        dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "' and service_id = '" & svDt.Rows(i)("service_id") & "' and week_of_year='" & dDr("week_of_year") & "' and show_year='" & dDr("show_year") & "'"
                        Dim dv As DataView = dt.DefaultView
                        If dv.Count > 0 Then
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("regis")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("serve")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("missed_call")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("cancle")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("incomplete")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("awt")) & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("aht")) & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("act")) & "</td>")

                            TotRegis(i) += Convert.ToInt64(dv(0)("regis"))
                            TotServe(i) += Convert.ToInt64(dv(0)("serve"))
                            TotMissCall(i) += Convert.ToInt64(dv(0)("missed_call"))
                            TotCancel(i) += Convert.ToInt64(dv(0)("cancle"))
                            TotIncomplete(i) += Convert.ToInt64(dv(0)("incomplete"))
                            TotCntAwt(i) += Convert.ToInt64(dv(0)("count_wt"))
                            TotSumAwt(i) += Convert.ToInt64(dv(0)("sum_wt"))
                            TotCntAht(i) += Convert.ToInt64(dv(0)("count_ht"))
                            TotSumAht(i) += Convert.ToInt64(dv(0)("sum_ht"))
                            TotCntAct(i) += Convert.ToInt64(dv(0)("count_ct"))
                            TotSumAct(i) += Convert.ToInt64(dv(0)("sum_ct"))
                        End If
                        dv = Nothing
                        dt.DefaultView.RowFilter = ""
                    Next
                    ret.Append("    </tr>")
                Next
                dDt = Nothing

                'Total Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                For i As Integer = 0 To svDt.Rows.Count - 1
                    ret.Append("        <td align='right'>" & Format(TotRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotIncomplete(i), "#,##0") & "</td>")
                    If TotCntAwt(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAwt(i) / TotCntAwt(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If
                    If TotCntAht(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAht(i) / TotCntAht(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If
                    If TotCntAct(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAct(i) / TotCntAct(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If

                    GTRegis(i) += TotRegis(i)
                    GTServe(i) += TotServe(i)
                    GTMissCall(i) += TotMissCall(i)
                    GTCancel(i) += TotCancel(i)
                    GTIncomplete(i) += TotIncomplete(i)
                    GTCntAwt(i) += TotCntAwt(i)
                    GTSumAwt(i) += TotSumAwt(i)
                    GTCntAht(i) += TotCntAht(i)
                    GTSumAht(i) += TotSumAht(i)
                    GTCntAct(i) += TotCntAct(i)
                    GTSumAct(i) += TotSumAct(i)
                Next
                ret.Append("    </tr>")
            Next

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            For i As Integer = 0 To svDt.Rows.Count - 1
                ret.Append("        <td align='right'>" & Format(GTRegis(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTServe(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTMissCall(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTCancel(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTIncomplete(i), "#,##0") & "</td>")

                If GTCntAwt(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAwt(i) / GTCntAwt(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
                If GTCntAht(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAht(i) / GTCntAht(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
                If GTCntAct(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAct(i) / GTCntAct(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
            Next
            ret.Append("    </tr>")
            ret.Append("</table>")

            svDt.Dispose()
            shDt.Dispose()
            dt.Dispose()

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        lnq = Nothing
        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal para As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopMonthCenLinqDB

        Dim WhText As String = "shop_id in (" & para.ShopID & ")"
        WhText += " and convert(varchar,show_year)+convert(varchar,month_no) between '" & para.YearFrom & para.MonthFrom & "' and '" & para.YearTo & para.MonthTo & "'"
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, month_no, show_month,service_id", trans.Trans)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder

            Dim svDt As New DataTable   'Service ที่ดึงมาทั้งหมด
            svDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy

            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            For Each svDr As DataRow In svDt.Rows
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Regis</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Serve</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Missed call</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Cancel</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " Incomplete</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AWT</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AHT</td>")
                ret.Append("        <td align='center' style='color: #ffffff;'>" & svDr("service_name_en") & " AvgConT</td>")
            Next
            ret.Append("    </tr>")

            Dim GTRegis(svDt.Rows.Count) As Long
            Dim GTServe(svDt.Rows.Count) As Long
            Dim GTMissCall(svDt.Rows.Count) As Long
            Dim GTCancel(svDt.Rows.Count) As Long
            Dim GTIncomplete(svDt.Rows.Count) As Long
            Dim GTCntAwt(svDt.Rows.Count) As Long
            Dim GTSumAwt(svDt.Rows.Count) As Long
            Dim GTCntAht(svDt.Rows.Count) As Long
            Dim GTSumAht(svDt.Rows.Count) As Long
            Dim GTCntAct(svDt.Rows.Count) As Long
            Dim GTSumAct(svDt.Rows.Count) As Long

            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            For Each shDr As DataRow In shDt.Rows
                Dim TotRegis(svDt.Rows.Count) As Integer
                Dim TotServe(svDt.Rows.Count) As Integer
                Dim TotMissCall(svDt.Rows.Count) As Integer
                Dim TotCancel(svDt.Rows.Count) As Integer
                Dim TotIncomplete(svDt.Rows.Count) As Integer
                Dim TotCntAwt(svDt.Rows.Count) As Long
                Dim TotSumAwt(svDt.Rows.Count) As Long
                Dim TotCntAht(svDt.Rows.Count) As Long
                Dim TotSumAht(svDt.Rows.Count) As Long
                Dim TotCntAct(svDt.Rows.Count) As Long
                Dim TotSumAct(svDt.Rows.Count) As Long

                Dim dDt As New DataTable
                dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                dDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en", "month_no", "show_month", "show_year")
                For Each dDr As DataRow In dDt.Rows
                    ret.Append("    <tr>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("show_month") & "</td>")
                    ret.Append("        <td align='right'>&nbsp;" & dDr("show_year") & "</td>")
                    For i As Integer = 0 To svDt.Rows.Count - 1
                        dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "' and service_id = '" & svDt.Rows(i)("service_id") & "' and month_no='" & dDr("month_no") & "' and show_year='" & dDr("show_year") & "'"
                        Dim dv As DataView = dt.DefaultView
                        If dv.Count > 0 Then
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("regis")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("serve")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("missed_call")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("cancle")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(Convert.ToInt64(dv(0)("incomplete")), "###,##0") & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("awt")) & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("aht")) & "</td>")
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(dv(0)("act")) & "</td>")

                            TotRegis(i) += Convert.ToInt64(dv(0)("regis"))
                            TotServe(i) += Convert.ToInt64(dv(0)("serve"))
                            TotMissCall(i) += Convert.ToInt64(dv(0)("missed_call"))
                            TotCancel(i) += Convert.ToInt64(dv(0)("cancle"))
                            TotIncomplete(i) += Convert.ToInt64(dv(0)("incomplete"))
                            TotCntAwt(i) += Convert.ToInt64(dv(0)("count_wt"))
                            TotSumAwt(i) += Convert.ToInt64(dv(0)("sum_wt"))
                            TotCntAht(i) += Convert.ToInt64(dv(0)("count_ht"))
                            TotSumAht(i) += Convert.ToInt64(dv(0)("sum_ht"))
                            TotCntAct(i) += Convert.ToInt64(dv(0)("count_ct"))
                            TotSumAct(i) += Convert.ToInt64(dv(0)("sum_ct"))
                        End If
                        dv = Nothing
                        dt.DefaultView.RowFilter = ""
                    Next
                    ret.Append("    </tr>")
                Next
                dDt = Nothing

                'Total Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                For i As Integer = 0 To svDt.Rows.Count - 1
                    ret.Append("        <td align='right'>" & Format(TotRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TotIncomplete(i), "#,##0") & "</td>")
                    If TotCntAwt(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAwt(i) / TotCntAwt(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If
                    If TotCntAht(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAht(i) / TotCntAht(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If
                    If TotCntAct(i) > 0 Then
                        ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(TotSumAct(i) / TotCntAct(i)) & "</td>")
                    Else
                        ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                    End If

                    GTRegis(i) += TotRegis(i)
                    GTServe(i) += TotServe(i)
                    GTMissCall(i) += TotMissCall(i)
                    GTCancel(i) += TotCancel(i)
                    GTIncomplete(i) += TotIncomplete(i)
                    GTCntAwt(i) += TotCntAwt(i)
                    GTSumAwt(i) += TotSumAwt(i)
                    GTCntAht(i) += TotCntAht(i)
                    GTSumAht(i) += TotSumAht(i)
                    GTCntAct(i) += TotCntAct(i)
                    GTSumAct(i) += TotSumAct(i)
                Next
                ret.Append("    </tr>")
            Next

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            For i As Integer = 0 To svDt.Rows.Count - 1
                ret.Append("        <td align='right'>" & Format(GTRegis(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTServe(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTMissCall(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTCancel(i), "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTIncomplete(i), "#,##0") & "</td>")

                If GTCntAwt(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAwt(i) / GTCntAwt(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
                If GTCntAht(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAht(i) / GTCntAht(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
                If GTCntAct(i) > 0 Then
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(GTSumAct(i) / GTCntAct(i)) & "</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                End If
            Next
            ret.Append("    </tr>")
            ret.Append("</table>")

            svDt.Dispose()
            shDt.Dispose()
            dt.Dispose()

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        lnq = Nothing
        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByTime(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara)
        Dim StrTime As DateTime = Date.ParseExact(InputPara.TimePeroidFrom, "HH:mm", Nothing)
        Dim EndTime As DateTime = Date.ParseExact(InputPara.TimePeroidTo, "HH:mm", Nothing)
        Dim CurrTime As DateTime = StrTime
        Dim InpTime As String = ""
        Do
            If CurrTime < EndTime Then
                Dim tmp As String = "'" & CurrTime.ToString("HH:mm") & "-" & DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime).ToString("HH:mm") & "'"
                If InpTime = "" Then
                    InpTime = tmp
                Else
                    InpTime += "," & tmp
                End If
            End If
            CurrTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
        Loop While CurrTime <= EndTime

        Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
        WhText += " and interval_minute = '" & InputPara.IntervalMinute & "'"
        WhText += " and convert(varchar(10),time_priod_from,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
        WhText += " and convert(varchar(10),time_priod_to,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
        WhText += " and show_time in (" & InpTime & ") "
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopTimeCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date, show_time,data_value", trans.Trans)
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If
        trans.CommitTransaction()

        If dt.Rows.Count > 0 Then
            Dim RowTmp() As String = {}

            'For TotalRow
            Dim TotSWT() As Double = {}
            Dim TotSHT() As Double = {}
            Dim TotSCT() As Double = {}
            Dim TotCWT() As Double = {}
            Dim TotCHT() As Double = {}
            Dim TotCCT() As Double = {}
            Dim TotRegis() As Double = {}
            Dim TotServed() As Double = {}
            Dim TotMissedcall() As Double = {}
            Dim TotCancelled() As Double = {}
            Dim TotIncomplete() As Double = {}

            Dim STotSWT() As Double = {}
            Dim STotSHT() As Double = {}
            Dim STotSCT() As Double = {}
            Dim STotCWT() As Double = {}
            Dim STotCHT() As Double = {}
            Dim STotCCT() As Double = {}
            Dim STotRegis() As Double = {}
            Dim STotServed() As Double = {}
            Dim STotMissedcall() As Double = {}
            Dim STotCancelled() As Double = {}
            Dim STotIncomplete() As Double = {}

            Dim GTotSWT() As Double = {}
            Dim GTotSHT() As Double = {}
            Dim GTotSCT() As Double = {}
            Dim GTotCWT() As Double = {}
            Dim GTotCHT() As Double = {}
            Dim GTotCCT() As Double = {}
            Dim GTotRegis() As Double = {}
            Dim GTotServed() As Double = {}
            Dim GTotMissedcall() As Double = {}
            Dim GTotCancelled() As Double = {}
            Dim GTotIncomplete() As Double = {}

            Dim ShopIDGroup As Int32 = 0
            Dim ShopID As Int32 = 0
            Dim ShopName As String = ""
            Dim DateGroup As String = ""
            Dim StrDate As String = ""
            Dim StrShop As String = ""

            Dim ret As New StringBuilder

            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                If i = 0 Then
                    'Header Row
                    ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Shop Name</td>")
                    ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Date</td>")
                    ret.Append("        <td rowspan='2' align='center' style='color: #ffffff;' >Time</td>")
                    'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
                    RowTmp = Split(dr("data_value"), "###")
                    For Each ServTmp As String In RowTmp
                        Dim tmp() As String = Split(ServTmp, "|")
                        ret.Append("        <td colspan='8' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>")
                    Next
                    ret.Append("    </tr>")

                    ReDim TotRegis(RowTmp.Length)
                    ReDim TotServed(RowTmp.Length)
                    ReDim TotMissedcall(RowTmp.Length)
                    ReDim TotCancelled(RowTmp.Length)
                    ReDim TotIncomplete(RowTmp.Length)
                    ReDim TotSWT(RowTmp.Length)
                    ReDim TotSHT(RowTmp.Length)
                    ReDim TotSCT(RowTmp.Length)
                    ReDim TotCWT(RowTmp.Length)
                    ReDim TotCHT(RowTmp.Length)
                    ReDim TotCCT(RowTmp.Length)

                    ReDim STotRegis(RowTmp.Length)
                    ReDim STotServed(RowTmp.Length)
                    ReDim STotMissedcall(RowTmp.Length)
                    ReDim STotCancelled(RowTmp.Length)
                    ReDim STotIncomplete(RowTmp.Length)
                    ReDim STotSWT(RowTmp.Length)
                    ReDim STotSHT(RowTmp.Length)
                    ReDim STotSCT(RowTmp.Length)
                    ReDim STotCWT(RowTmp.Length)
                    ReDim STotCHT(RowTmp.Length)
                    ReDim STotCCT(RowTmp.Length)

                    ReDim GTotRegis(RowTmp.Length)
                    ReDim GTotServed(RowTmp.Length)
                    ReDim GTotMissedcall(RowTmp.Length)
                    ReDim GTotCancelled(RowTmp.Length)
                    ReDim GTotIncomplete(RowTmp.Length)
                    ReDim GTotSWT(RowTmp.Length)
                    ReDim GTotSHT(RowTmp.Length)
                    ReDim GTotSCT(RowTmp.Length)
                    ReDim GTotCWT(RowTmp.Length)
                    ReDim GTotCHT(RowTmp.Length)
                    ReDim GTotCCT(RowTmp.Length)

                    ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
                    For Each ServTmp As String In RowTmp
                        ret.Append("        <td align='center' style='color: #ffffff;'>Regis</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>Serve</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>Missed call</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>Cancel</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>Incomplete</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>AWT</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>AHT</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;'>AvgConT</td>")
                    Next
                    ret.Append("    </tr>")
                End If

                If ShopIDGroup = 0 Then
                    ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
                    ShopID = dt.Rows(i).Item("shop_id").ToString
                    DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
                End If

                '***************** File Data *******************
                ret.Append("    <tr>")
                ret.Append("        <td align='center'>&nbsp;" & dr("shop_name_en") & "</td>")
                ret.Append("        <td align='left'>&nbsp;" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                ret.Append("        <td align='center'>&nbsp;" & dr("show_time").ToString & "</td>")

                StrDate = Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                StrShop = dr("shop_name_en")
                Dim k As Integer = 0
                Dim TotalCal As Long = 0
                RowTmp = Split(dr("data_value"), "###")

                Dim regis, serve, cancel, misscall, incomplete, awt, aht, act, swt, sht, sct, cwt, cht, cct As Int32
                For Each ServTmp As String In RowTmp
                    Dim tmp() As String = Split(ServTmp, "|")
                    'ServiceID|item_name|item_name_th|AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_NOTCALL|TOTAL_NOTCON|TOTAL_NOTEND|SWT|SCT|SHT|CWT|CCT|CHT
                    regis = tmp(6)
                    serve = tmp(7)
                    misscall = tmp(8)
                    cancel = tmp(9)
                    incomplete = CInt(tmp(10)) + CInt(tmp(11)) + CInt(tmp(12))
                    awt = tmp(3)
                    act = tmp(4)
                    aht = tmp(5)
                    swt = tmp(13)
                    sct = tmp(14)
                    sht = tmp(15)
                    cwt = tmp(16)
                    cct = tmp(17)
                    cht = tmp(18)

                    ret.Append("        <td align='right'>" & Format(regis, "###,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(serve, "###,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(misscall, "###,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(cancel, "###,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(incomplete, "###,##0") & "</td>")

                    'AWT
                    If cwt = 0 Then
                        ret.Append("<td align='right'>&nbsp;00:00:00</td>")
                    Else
                        ret.Append("<td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(swt / cwt, "##0")) & "</td>")
                    End If

                    If cht = 0 Then
                        ret.Append("<td align='right'>&nbsp;00:00:00</td>")
                        ret.Append("<td align='right'>&nbsp;00:00:00</td>")
                    Else
                        'AHT
                        ret.Append("<td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(sht / cht, "##0")) & "</td>")

                        'AvgConT
                        ret.Append("<td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(sct / cht, "##0")) & "</td>")
                    End If

                    TotRegis(k) += Convert.ToDouble(regis)
                    TotServed(k) += Convert.ToDouble(serve)
                    TotMissedcall(k) += Convert.ToDouble(misscall)
                    TotCancelled(k) += Convert.ToDouble(cancel)
                    TotIncomplete(k) += Convert.ToDouble(incomplete)
                    TotSWT(k) += Convert.ToDouble(swt)
                    TotSCT(k) += Convert.ToDouble(sct)
                    TotSHT(k) += Convert.ToDouble(sht)
                    TotCWT(k) += Convert.ToDouble(cwt)
                    TotCCT(k) += Convert.ToDouble(cct)
                    TotCHT(k) += Convert.ToDouble(cht)

                    STotRegis(k) += Convert.ToDouble(regis)
                    STotServed(k) += Convert.ToDouble(serve)
                    STotMissedcall(k) += Convert.ToDouble(misscall)
                    STotCancelled(k) += Convert.ToDouble(cancel)
                    STotIncomplete(k) += Convert.ToDouble(incomplete)
                    STotSWT(k) += Convert.ToDouble(swt)
                    STotSCT(k) += Convert.ToDouble(sct)
                    STotSHT(k) += Convert.ToDouble(sht)
                    STotCWT(k) += Convert.ToDouble(cwt)
                    STotCCT(k) += Convert.ToDouble(cct)
                    STotCHT(k) += Convert.ToDouble(cht)

                    GTotRegis(k) += Convert.ToDouble(regis)
                    GTotServed(k) += Convert.ToDouble(serve)
                    GTotMissedcall(k) += Convert.ToDouble(misscall)
                    GTotCancelled(k) += Convert.ToDouble(cancel)
                    GTotIncomplete(k) += Convert.ToDouble(incomplete)
                    GTotSWT(k) += Convert.ToDouble(swt)
                    GTotSCT(k) += Convert.ToDouble(sct)
                    GTotSHT(k) += Convert.ToDouble(sht)
                    GTotCWT(k) += Convert.ToDouble(cwt)
                    GTotCCT(k) += Convert.ToDouble(cct)
                    GTotCHT(k) += Convert.ToDouble(cht)

                    k += 1
                Next
                ret.Append("    </tr>")
                '***********************************************

                '******************* Sub Total *****************
                Dim ChkSubTotal As Boolean = False
                If dt.Rows.Count = i + 1 Then
                    ChkSubTotal = True
                Else
                    If dt.Rows(i).Item("shop_id") <> dt.Rows(i + 1).Item("shop_id") Or CDate(dt.Rows(i).Item("service_date")).ToShortDateString <> CDate(dt.Rows(i + 1).Item("service_date")).ToShortDateString Then
                        ChkSubTotal = True
                        ShopIDGroup = dt.Rows(i).Item("shop_id")
                        DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
                        ShopName = dt.Rows(i).Item("shop_name_en").ToString
                    End If
                End If

                If ChkSubTotal = True Then
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                    ret.Append("        <td align='center'>&nbsp;" & StrShop & "</td>")
                    ret.Append("        <td align='left'>&nbsp;" & StrDate & "</td>")
                    ret.Append("        <td align='center'>Sub Total</td>")
                    Dim n As Integer = 0
                    For Each ServTmp As String In RowTmp
                        ret.Append("        <td align='right'>" & Format(TotRegis(n), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TotServed(n), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TotMissedcall(n), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TotCancelled(n), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TotIncomplete(n), "###,##0") & "</td>")
                        If TotCWT(n) = 0 Then
                            ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(TotSWT(n) / TotCWT(n))) & "</td>")
                        End If

                        If TotCHT(n) = 0 Then
                            ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(TotSHT(n) / TotCHT(n))) & "</td>")
                        End If
                        If TotCCT(n) = 0 Then
                            ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(TotSCT(n) / TotCCT(n))) & "</td>")
                        End If
                        n += 1
                    Next
                    ret.Append("    </tr>")

                    ReDim TotRegis(RowTmp.Length)
                    ReDim TotServed(RowTmp.Length)
                    ReDim TotMissedcall(RowTmp.Length)
                    ReDim TotCancelled(RowTmp.Length)
                    ReDim TotIncomplete(RowTmp.Length)
                    ReDim TotSWT(RowTmp.Length)
                    ReDim TotSHT(RowTmp.Length)
                    ReDim TotSCT(RowTmp.Length)
                    ReDim TotCWT(RowTmp.Length)
                    ReDim TotCHT(RowTmp.Length)
                    ReDim TotCCT(RowTmp.Length)
                End If
                '***********************************************

                '******************** Total ********************
                Dim ChkTotal As Boolean = False
                If dt.Rows.Count = i + 1 Then
                    ChkTotal = True
                Else
                    If dt.Rows(i).Item("shop_id") <> dt.Rows(i + 1).Item("shop_id") Then
                        ChkTotal = True
                    End If
                End If
                If ChkTotal = True Then
                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                    ret.Append("        <td align='center' colspan='3'>Total " & StrShop & "</td>")
                    Dim x As Integer = 0
                    For Each ServTmp As String In RowTmp
                        ret.Append("        <td align='right'>" & Format(STotRegis(x), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(STotServed(x), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(STotMissedcall(x), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(STotCancelled(x), "###,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(STotIncomplete(x), "###,##0") & "</td>")
                        If STotCWT(x) = 0 Then
                            ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(STotSWT(x) / STotCWT(x))) & "</td>")
                        End If
                        If STotCHT(x) = 0 Then
                            ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(STotSHT(x) / STotCHT(x))) & "</td>")
                        End If
                        If STotCCT(x) = 0 Then
                            ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(STotSCT(x) / STotCCT(x))) & "</td>")
                        End If
                        x += 1
                    Next
                    ret.Append("    </tr>")

                    ReDim STotRegis(RowTmp.Length)
                    ReDim STotServed(RowTmp.Length)
                    ReDim STotMissedcall(RowTmp.Length)
                    ReDim STotCancelled(RowTmp.Length)
                    ReDim STotIncomplete(RowTmp.Length)
                    ReDim STotSWT(RowTmp.Length)
                    ReDim STotSHT(RowTmp.Length)
                    ReDim STotSCT(RowTmp.Length)
                    ReDim STotCWT(RowTmp.Length)
                    ReDim STotCHT(RowTmp.Length)
                    ReDim STotCCT(RowTmp.Length)
                End If
                '***********************************************
            Next

            '***************** Grand Total *****************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='3'>Grand Total</td>")
            Dim m As Integer = 0
            For Each ServTmp As String In RowTmp
                ret.Append("        <td align='right'>" & Format(GTotRegis(m), "###,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTotServed(m), "###,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTotMissedcall(m), "###,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTotCancelled(m), "###,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTotIncomplete(m), "###,##0") & "</td>")
                If GTotCWT(m) = 0 Then
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(GTotSWT(m) / GTotCWT(m))) & "</td>")
                End If
                If GTotCHT(m) = 0 Then
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(GTotSHT(m) / GTotCHT(m))) & "</td>")
                End If
                If GTotCCT(m) = 0 Then
                    ret.Append("        <td align='right'>&nbsp;00:00:00</td>")
                Else
                    ret.Append("        <td align='right'>&nbsp;" & ReportsENG.GetFormatTimeFromSec(Math.Round(GTotSCT(m) / GTotCCT(m))) & "</td>")
                End If
                m += 1
            Next
            ret.Append("    </tr>")
            '***********************************************
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

   
End Class
