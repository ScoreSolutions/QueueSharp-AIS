Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repAvgServiceTimeKPIByShop
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "CustomersServedByShop_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repAvgServiceTimeKPIByShop.ExportData", Config.GetLoginHistoryID)
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
            Dim StartTime As DateTime = DateTime.Now
            ShowReport(Request("ReportName"))
        End If
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        'Dim txtData As String = ""
        If ReportName = "ByTime" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            para.IntervalMinute = Request("Interval")
            para.TimePeroidFrom = Request("TimeFrom")
            para.TimePeroidTo = Request("TimeTo")
            Dim eng As New Engine.Reports.RepAverageServiceTimeComparingWithKPI
            RenderReportByTime(para)
            
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepAverageServiceTimeComparingWithKPI
            RenderReportByWeek(para)
        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepAverageServiceTimeComparingWithKPI
            RenderReportByDate(para)
        ElseIf ReportName = "ByDay" Then
            'Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            'para.ShopID = Request("ShopID")
            'para.DayInWeek = Request("Day")
            'para.WeekInYearFrom = Request("WeekFrom")
            'para.WeekInYearTo = Request("WeekTo")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'txtData = RenderReportByDay(para)
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            RenderReportByMonth(para)
        ElseIf ReportName = "ByYear" Then
            'Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
            'para.ShopID = Request("ShopID")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'txtData = RenderReportByYear(para)
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara)
        'Dim MonthPara As String = ""
        'For i As Integer = InputPara.MonthFrom To InputPara.MonthTo
        '    If MonthPara = "" Then
        '        MonthPara = i
        '    Else
        '        MonthPara += "," & i
        '    End If
        'Next
        Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
        WhText += " and convert(varchar,show_year)+convert(varchar,month_no) between '" & InputPara.YearFrom & InputPara.MonthFrom & "' and '" & InputPara.YearTo & InputPara.MonthTo & "'"

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiMonthCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, month_no", trans.Trans)
        trans.CommitTransaction()
        lnq = Nothing
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If

        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td  align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Month</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Regis</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")

            'Grand Total
            Dim GTotRegis As Long = 0
            Dim GTotServe As Long = 0
            Dim GTotMissedCall As Long = 0
            Dim GTotCancelled As Long = 0
            Dim GTotIncomplete As Long = 0
            Dim Gwt_with_kpi As Long = 0
            Dim Gserve_with_kpi As Long = 0
            Dim Gmax_wt As Long = 0
            Dim Gmax_ht As Long = 0
            Dim Gx As Integer = 0
            Dim Gy As Integer = 0

            'Loop ตาม Shop
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            For Each shDr As DataRow In shDt.Rows
                Dim TotRegis As Long = 0
                Dim TotServe As Long = 0
                Dim TotMissedCall As Long = 0
                Dim TotCancelled As Long = 0
                Dim TotIncomplete As Long = 0
                Dim Twt_with_kpi As Long = 0
                Dim Tserve_with_kpi As Long = 0
                Dim Tmax_wt As Long = 0
                Dim Tmax_ht As Long = 0
                Dim Tx As Integer = 0
                Dim Ty As Integer = 0

                dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "'"
                Dim mDt As New DataTable
                mDt = dt.DefaultView.ToTable(True, "month_no", "show_month", "show_year")
                For Each mDr As DataRow In mDt.Rows
                    Dim STotRegis As Long = 0
                    Dim STotServe As Long = 0
                    Dim STotMissedCall As Long = 0
                    Dim STotCancelled As Long = 0
                    Dim STotIncomplete As Long = 0
                    Dim STwt_with_kpi As Long = 0
                    Dim STserve_with_kpi As Long = 0
                    Dim STmax_wt As Long = 0
                    Dim STmax_ht As Long = 0
                    Dim STx As Integer = 0
                    Dim STy As Integer = 0

                    dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "' and month_no='" & mDr("month_no") & "' and show_year='" & mDr("show_year") & "'"
                    Dim dv As DataView = dt.DefaultView
                    For Each dr As DataRowView In dv
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left'>&nbsp;" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & mDr("show_month") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & mDr("show_year") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("regis"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("missed_call"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("cancel"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("served"), "#,##0") & "</td>")
                        Dim Incomplete As Long = CDbl(dr("not_call")) + CDbl(dr("not_confirm")) + CDbl(dr("not_end"))
                        ret.Append("        <td align='right' >" & Format(Incomplete, "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("wait_with_kpi"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("serve_with_kpi"), "#,##0") & "</td>")

                        Dim perWt1 As String = "0.00"
                        Dim perWt2 As String = "0.00"
                        Dim perHt1 As String = "0.00"
                        Dim perHt2 As String = "0.00"
                        Dim perMs As String = "0.00"
                        If Convert.ToInt64(dr("served")) > 0 Then
                            perWt1 = Format((dr("wait_with_kpi") / dr("served")) * 100, "0.00")
                            perWt2 = Format(100 - (dr("wait_with_kpi") / dr("served")) * 100, "0.00")
                            perHt1 = Format((dr("serve_with_kpi") / dr("served")) * 100, "0.00")
                            perHt2 = Format(100 - (dr("serve_with_kpi") / dr("served")) * 100, "0.00")
                        End If
                        If Convert.ToInt64(dr("regis")) > 0 Then
                            perMs = Format((dr("missed_call") / dr("regis")) * 100, "0.00")
                        End If
                        ret.Append("        <td align='right' >&nbsp;" & perWt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perWt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perMs & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dr("max_wt")) & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dr("max_ht")) & "</td>")
                        ret.Append("    </tr>")

                        TotRegis += Convert.ToInt64(dr("regis"))
                        TotServe += Convert.ToInt64(dr("served"))
                        TotMissedCall += Convert.ToInt64(dr("missed_call"))
                        TotCancelled += Convert.ToInt64(dr("cancel"))
                        TotIncomplete += Incomplete
                        Twt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Tserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        STotRegis += Convert.ToInt64(dr("regis"))
                        STotServe += Convert.ToInt64(dr("served"))
                        STotMissedCall += Convert.ToInt64(dr("missed_call"))
                        STotCancelled += Convert.ToInt64(dr("cancel"))
                        STotIncomplete += Incomplete
                        STwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        STserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        GTotRegis += Convert.ToInt64(dr("regis"))
                        GTotServe += Convert.ToInt64(dr("served"))
                        GTotMissedCall += Convert.ToInt64(dr("missed_call"))
                        GTotCancelled += Convert.ToInt64(dr("cancel"))
                        GTotIncomplete += Incomplete
                        Gwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Gserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        If dr("max_wt") > 0 Then
                            Tmax_wt += Convert.ToInt64(dr("max_wt"))
                            Tx += 1

                            STmax_wt += Convert.ToInt64(dr("max_wt"))
                            STx += 1

                            Gmax_wt += Convert.ToInt64(dr("max_wt"))
                            Gx += 1
                        End If
                        If dr("max_ht") > 0 Then
                            Tmax_ht += Convert.ToInt64(dr("max_ht"))
                            Ty += 1

                            STmax_ht += Convert.ToInt64(dr("max_ht"))
                            STy += 1

                            Gmax_ht += Convert.ToInt64(dr("max_ht"))
                            Gy += 1
                        End If
                    Next
                    dv = Nothing
                    dt.DefaultView.RowFilter = ""

                    'Sub Total By Month
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & mDr("show_month") & "</td>")
                    ret.Append("        <td align='center' >" & mDr("show_year") & "</td>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STwt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STserve_with_kpi, "###,##0") & "</td>")

                    Dim S1wt1 As String = "0.00"
                    Dim S1wt2 As String = "0.00"
                    Dim S1ht1 As String = "0.00"
                    Dim S1ht2 As String = "0.00"
                    Dim S1mc As String = "0.00"
                    If TotServe > 0 Then
                        S1wt1 = Format((STwt_with_kpi / STotServe) * 100, "##0.00")
                        S1wt2 = Format(100 - ((STwt_with_kpi / STotServe) * 100), "##0.00")
                        S1ht1 = Format((STserve_with_kpi / STotServe) * 100, "##0.00")
                        S1ht2 = Format(100 - ((STserve_with_kpi / STotServe) * 100), "##0.00")
                    End If

                    ' % Missed Call
                    If TotRegis > 0 Then
                        S1mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                    End If
                    ret.Append("        <td align='right' >&nbsp;" & S1wt1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1wt2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1mc & "%</td>")

                    '****************** AGV WT ******************
                    Dim StAwt As Integer = 0
                    If STx <> 0 Then StAwt = (Format(STmax_wt / STx, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(StAwt) & "</td>")
                    '****************** AGV HT ******************
                    Dim stAht As Integer = 0
                    If STy <> 0 Then stAht = (Format(STmax_ht / STy, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(stAht) & "</td>")
                    '********************************************
                    ret.Append("    </tr>")
                Next
                mDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                Dim T1wt1 As String = "0.00"
                Dim T1wt2 As String = "0.00"
                Dim T1ht1 As String = "0.00"
                Dim T1ht2 As String = "0.00"
                Dim T1mc As String = "0.00"
                If TotServe > 0 Then
                    T1wt1 = Format((Twt_with_kpi / TotServe) * 100, "##0.00")
                    T1wt2 = Format(100 - ((Twt_with_kpi / TotServe) * 100), "##0.00")
                    T1ht1 = Format((Tserve_with_kpi / TotServe) * 100, "##0.00")
                    T1ht2 = Format(100 - ((Tserve_with_kpi / TotServe) * 100), "##0.00")
                End If

                ' % Missed Call
                If TotRegis > 0 Then
                    T1mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                End If
                ret.Append("        <td align='right' >&nbsp;" & T1wt1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1wt2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1ht1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1ht2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1mc & "%</td>")

                '****************** AGV WT ******************
                Dim ttAwt As Integer = 0
                If Tx <> 0 Then ttAwt = (Format(Tmax_wt / Tx, "##0"))
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                '****************** AGV HT ******************
                Dim ttAht As Integer = 0
                If Ty <> 0 Then ttAht = (Format(Tmax_ht / Ty, "##0"))
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                '********************************************
                ret.Append("    </tr>")
            Next
            shDt.Dispose()

            '******************** Grand Total ************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "#,##0") & "</td>")

            Dim Gwt1 As String = "0.00"
            Dim Gwt2 As String = "0.00"
            Dim Ght1 As String = "0.00"
            Dim Ght2 As String = "0.00"
            Dim Gmc As String = "0.00"
            If GTotServe > 0 Then
                Gwt1 = Format((Gwt_with_kpi / GTotServe) * 100, "##0.00")
                Gwt2 = Format(100 - ((Gwt_with_kpi / GTotServe) * 100), "##0.00")
                Ght1 = Format((Gserve_with_kpi / GTotServe) * 100, "##0.00")
                Ght2 = Format(100 - ((Gserve_with_kpi / GTotServe) * 100), "##0.00")
            End If

            ' %Missed Call
            If GTotRegis > 0 Then
                Gmc = Format((GTotMissedCall / GTotRegis) * 100, "##0.00")
            End If
            ret.Append("        <td align='right' >&nbsp;" & Gwt1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gwt2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gmc & "%</td>")

            '****************** AGV WT ******************
            Dim GAwt As Integer = 0
            If Gx <> 0 Then GAwt = (Format(Gmax_wt / Gx, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>")

            '****************** AGV HT ******************
            Dim GAht As Integer = 0
            If Gy <> 0 Then GAht = (Format(Gmax_ht / Gy, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>")
            '********************************************
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
            dt.Dispose()
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara)
        Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
        WhText += " and convert(varchar,show_year) +  right('0'+ convert(varchar,week_of_year),2)  between '" & InputPara.YearFrom & Right("0" & InputPara.WeekInYearFrom, 2) & "' and '" & InputPara.YearTo & Right("0" & InputPara.WeekInYearTo, 2) & "'"

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiWeekCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, week_of_year", trans.Trans)
        trans.CommitTransaction()
        lnq = Nothing
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If

        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td  align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Week No.</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Regis</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")

            'Grand Total
            Dim GTotRegis As Long = 0
            Dim GTotServe As Long = 0
            Dim GTotMissedCall As Long = 0
            Dim GTotCancelled As Long = 0
            Dim GTotIncomplete As Long = 0
            Dim Gwt_with_kpi As Long = 0
            Dim Gserve_with_kpi As Long = 0
            Dim Gmax_wt As Long = 0
            Dim Gmax_ht As Long = 0
            Dim Gx As Integer = 0
            Dim Gy As Integer = 0

            'Loop ตาม Shop
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            For Each shDr As DataRow In shDt.Rows
                Dim TotRegis As Long = 0
                Dim TotServe As Long = 0
                Dim TotMissedCall As Long = 0
                Dim TotCancelled As Long = 0
                Dim TotIncomplete As Long = 0
                Dim Twt_with_kpi As Long = 0
                Dim Tserve_with_kpi As Long = 0
                Dim Tmax_wt As Long = 0
                Dim Tmax_ht As Long = 0
                Dim Tx As Integer = 0
                Dim Ty As Integer = 0

                dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "'"
                Dim wDt As New DataTable
                wDt = dt.DefaultView.ToTable(True, "week_of_year", "show_year")
                For Each wDr As DataRow In wDt.Rows
                    Dim STotRegis As Long = 0
                    Dim STotServe As Long = 0
                    Dim STotMissedCall As Long = 0
                    Dim STotCancelled As Long = 0
                    Dim STotIncomplete As Long = 0
                    Dim STwt_with_kpi As Long = 0
                    Dim STserve_with_kpi As Long = 0
                    Dim STmax_wt As Long = 0
                    Dim STmax_ht As Long = 0
                    Dim STx As Integer = 0
                    Dim STy As Integer = 0

                    dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "' and week_of_year='" & wDr("week_of_year") & "' and show_year='" & wDr("show_year") & "'"
                    Dim dv As DataView = dt.DefaultView
                    For Each dr As DataRowView In dv
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left'>&nbsp;" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & wDr("week_of_year") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & wDr("show_year") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("regis"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("missed_call"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("cancel"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("served"), "#,##0") & "</td>")
                        Dim Incomplete As Long = CDbl(dr("not_call")) + CDbl(dr("not_confirm")) + CDbl(dr("not_end"))
                        ret.Append("        <td align='right' >" & Format(Incomplete, "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("wait_with_kpi"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("serve_with_kpi"), "#,##0") & "</td>")

                        Dim perWt1 As String = "0.00"
                        Dim perWt2 As String = "0.00"
                        Dim perHt1 As String = "0.00"
                        Dim perHt2 As String = "0.00"
                        Dim perMs As String = "0.00"
                        If Convert.ToInt64(dr("served")) > 0 Then
                            perWt1 = Format((dr("wait_with_kpi") / dr("served")) * 100, "0.00")
                            perWt2 = Format(100 - (dr("wait_with_kpi") / dr("served")) * 100, "0.00")
                            perHt1 = Format((dr("serve_with_kpi") / dr("served")) * 100, "0.00")
                            perHt2 = Format(100 - (dr("serve_with_kpi") / dr("served")) * 100, "0.00")
                        End If
                        If Convert.ToInt64(dr("regis")) > 0 Then
                            perMs = Format((dr("missed_call") / dr("regis")) * 100, "0.00")
                        End If
                        ret.Append("        <td align='right' >&nbsp;" & perWt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perWt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perMs & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dr("max_wt")) & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dr("max_ht")) & "</td>")
                        ret.Append("    </tr>")

                        TotRegis += Convert.ToInt64(dr("regis"))
                        TotServe += Convert.ToInt64(dr("served"))
                        TotMissedCall += Convert.ToInt64(dr("missed_call"))
                        TotCancelled += Convert.ToInt64(dr("cancel"))
                        TotIncomplete += Incomplete
                        Twt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Tserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        STotRegis += Convert.ToInt64(dr("regis"))
                        STotServe += Convert.ToInt64(dr("served"))
                        STotMissedCall += Convert.ToInt64(dr("missed_call"))
                        STotCancelled += Convert.ToInt64(dr("cancel"))
                        STotIncomplete += Incomplete
                        STwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        STserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        GTotRegis += Convert.ToInt64(dr("regis"))
                        GTotServe += Convert.ToInt64(dr("served"))
                        GTotMissedCall += Convert.ToInt64(dr("missed_call"))
                        GTotCancelled += Convert.ToInt64(dr("cancel"))
                        GTotIncomplete += Incomplete
                        Gwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Gserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        If dr("max_wt") > 0 Then
                            Tmax_wt += Convert.ToInt64(dr("max_wt"))
                            Tx += 1

                            STmax_wt += Convert.ToInt64(dr("max_wt"))
                            STx += 1

                            Gmax_wt += Convert.ToInt64(dr("max_wt"))
                            Gx += 1
                        End If
                        If dr("max_ht") > 0 Then
                            Tmax_ht += Convert.ToInt64(dr("max_ht"))
                            Ty += 1

                            STmax_ht += Convert.ToInt64(dr("max_ht"))
                            STy += 1

                            Gmax_ht += Convert.ToInt64(dr("max_ht"))
                            Gy += 1
                        End If
                    Next
                    dv = Nothing
                    dt.DefaultView.RowFilter = ""

                    'Sub Total By Month
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & wDr("week_of_year") & "</td>")
                    ret.Append("        <td align='center' >" & wDr("show_year") & "</td>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STwt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STserve_with_kpi, "###,##0") & "</td>")

                    Dim S1wt1 As String = "0.00"
                    Dim S1wt2 As String = "0.00"
                    Dim S1ht1 As String = "0.00"
                    Dim S1ht2 As String = "0.00"
                    Dim S1mc As String = "0.00"
                    If TotServe > 0 Then
                        S1wt1 = Format((STwt_with_kpi / STotServe) * 100, "##0.00")
                        S1wt2 = Format(100 - ((STwt_with_kpi / STotServe) * 100), "##0.00")
                        S1ht1 = Format((STserve_with_kpi / STotServe) * 100, "##0.00")
                        S1ht2 = Format(100 - ((STserve_with_kpi / STotServe) * 100), "##0.00")
                    End If

                    ' % Missed Call
                    If TotRegis > 0 Then
                        S1mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                    End If
                    ret.Append("        <td align='right' >&nbsp;" & S1wt1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1wt2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1mc & "%</td>")

                    '****************** AGV WT ******************
                    Dim StAwt As Integer = 0
                    If STx <> 0 Then StAwt = (Format(STmax_wt / STx, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(StAwt) & "</td>")
                    '****************** AGV HT ******************
                    Dim stAht As Integer = 0
                    If STy <> 0 Then stAht = (Format(STmax_ht / STy, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(stAht) & "</td>")
                    '********************************************
                    ret.Append("    </tr>")
                Next
                wDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                Dim T1wt1 As String = "0.00"
                Dim T1wt2 As String = "0.00"
                Dim T1ht1 As String = "0.00"
                Dim T1ht2 As String = "0.00"
                Dim T1mc As String = "0.00"
                If TotServe > 0 Then
                    T1wt1 = Format((Twt_with_kpi / TotServe) * 100, "##0.00")
                    T1wt2 = Format(100 - ((Twt_with_kpi / TotServe) * 100), "##0.00")
                    T1ht1 = Format((Tserve_with_kpi / TotServe) * 100, "##0.00")
                    T1ht2 = Format(100 - ((Tserve_with_kpi / TotServe) * 100), "##0.00")
                End If

                ' % Missed Call
                If TotRegis > 0 Then
                    T1mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                End If
                ret.Append("        <td align='right' >&nbsp;" & T1wt1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1wt2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1ht1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1ht2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1mc & "%</td>")

                '****************** AGV WT ******************
                Dim ttAwt As Integer = 0
                If Tx <> 0 Then ttAwt = (Format(Tmax_wt / Tx, "##0"))
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                '****************** AGV HT ******************
                Dim ttAht As Integer = 0
                If Ty <> 0 Then ttAht = (Format(Tmax_ht / Ty, "##0"))
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                '********************************************
                ret.Append("    </tr>")
            Next
            shDt.Dispose()

            '******************** Grand Total ************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "#,##0") & "</td>")

            Dim Gwt1 As String = "0.00"
            Dim Gwt2 As String = "0.00"
            Dim Ght1 As String = "0.00"
            Dim Ght2 As String = "0.00"
            Dim Gmc As String = "0.00"
            If GTotServe > 0 Then
                Gwt1 = Format((Gwt_with_kpi / GTotServe) * 100, "##0.00")
                Gwt2 = Format(100 - ((Gwt_with_kpi / GTotServe) * 100), "##0.00")
                Ght1 = Format((Gserve_with_kpi / GTotServe) * 100, "##0.00")
                Ght2 = Format(100 - ((Gserve_with_kpi / GTotServe) * 100), "##0.00")
            End If

            ' %Missed Call
            If GTotRegis > 0 Then
                Gmc = Format((GTotMissedCall / GTotRegis) * 100, "##0.00")
            End If
            ret.Append("        <td align='right' >&nbsp;" & Gwt1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gwt2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gmc & "%</td>")

            '****************** AGV WT ******************
            Dim GAwt As Integer = 0
            If Gx <> 0 Then GAwt = (Format(Gmax_wt / Gx, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>")

            '****************** AGV HT ******************
            Dim GAht As Integer = 0
            If Gy <> 0 Then GAht = (Format(Gmax_ht / Gy, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>")
            '********************************************
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
            dt.Dispose()
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara)
        Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
        WhText += " and convert(varchar(8),service_date,112) >= '" & InputPara.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        WhText += " and convert(varchar(8),service_date,112) <= '" & InputPara.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiDayCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date, service_id", trans.Trans)
        trans.CommitTransaction()
        lnq = Nothing
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If

        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td  align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Regis</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td  align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")

            'Grand Total
            Dim GTotRegis As Long = 0
            Dim GTotServe As Long = 0
            Dim GTotMissedCall As Long = 0
            Dim GTotCancelled As Long = 0
            Dim GTotIncomplete As Long = 0
            Dim Gwt_with_kpi As Long = 0
            Dim Gserve_with_kpi As Long = 0
            Dim Gmax_wt As Long = 0
            Dim Gmax_ht As Long = 0
            Dim Gx As Integer = 0
            Dim Gy As Integer = 0

            'Loop ตาม Shop
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            For Each shDr As DataRow In shDt.Rows
                Dim TotRegis As Long = 0
                Dim TotServe As Long = 0
                Dim TotMissedCall As Long = 0
                Dim TotCancelled As Long = 0
                Dim TotIncomplete As Long = 0
                Dim Twt_with_kpi As Long = 0
                Dim Tserve_with_kpi As Long = 0
                Dim Tmax_wt As Long = 0
                Dim Tmax_ht As Long = 0
                Dim Tx As Integer = 0
                Dim Ty As Integer = 0

                dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "'"
                Dim dDt As New DataTable
                dDt = dt.DefaultView.ToTable(True, "service_date")
                For Each dDr As DataRow In dDt.Rows
                    Dim STotRegis As Long = 0
                    Dim STotServe As Long = 0
                    Dim STotMissedCall As Long = 0
                    Dim STotCancelled As Long = 0
                    Dim STotIncomplete As Long = 0
                    Dim STwt_with_kpi As Long = 0
                    Dim STserve_with_kpi As Long = 0
                    Dim STmax_wt As Long = 0
                    Dim STmax_ht As Long = 0
                    Dim STx As Integer = 0
                    Dim STy As Integer = 0

                    dt.DefaultView.RowFilter = "shop_id = '" & shDr("shop_id") & "' and service_date='" & dDr("service_date") & "' "
                    Dim dv As DataView = dt.DefaultView
                    For Each dr As DataRowView In dv
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left'>&nbsp;" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & Convert.ToDateTime(dDr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                        ret.Append("        <td align='left'>&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("regis"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("missed_call"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("cancel"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("served"), "#,##0") & "</td>")
                        Dim Incomplete As Long = CDbl(dr("not_call")) + CDbl(dr("not_confirm")) + CDbl(dr("not_end"))
                        ret.Append("        <td align='right' >" & Format(Incomplete, "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("wait_with_kpi"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("serve_with_kpi"), "#,##0") & "</td>")

                        Dim perWt1 As String = "0.00"
                        Dim perWt2 As String = "0.00"
                        Dim perHt1 As String = "0.00"
                        Dim perHt2 As String = "0.00"
                        Dim perMs As String = "0.00"
                        If Convert.ToInt64(dr("served")) > 0 Then
                            perWt1 = Format((dr("wait_with_kpi") / dr("served")) * 100, "0.00")
                            perWt2 = Format(100 - (dr("wait_with_kpi") / dr("served")) * 100, "0.00")
                            perHt1 = Format((dr("serve_with_kpi") / dr("served")) * 100, "0.00")
                            perHt2 = Format(100 - (dr("serve_with_kpi") / dr("served")) * 100, "0.00")
                        End If
                        If Convert.ToInt64(dr("regis")) > 0 Then
                            perMs = Format((dr("missed_call") / dr("regis")) * 100, "0.00")
                        End If
                        ret.Append("        <td align='right' >&nbsp;" & perWt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perWt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perMs & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dr("max_wt")) & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dr("max_ht")) & "</td>")
                        ret.Append("    </tr>")

                        TotRegis += Convert.ToInt64(dr("regis"))
                        TotServe += Convert.ToInt64(dr("served"))
                        TotMissedCall += Convert.ToInt64(dr("missed_call"))
                        TotCancelled += Convert.ToInt64(dr("cancel"))
                        TotIncomplete += Incomplete
                        Twt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Tserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        STotRegis += Convert.ToInt64(dr("regis"))
                        STotServe += Convert.ToInt64(dr("served"))
                        STotMissedCall += Convert.ToInt64(dr("missed_call"))
                        STotCancelled += Convert.ToInt64(dr("cancel"))
                        STotIncomplete += Incomplete
                        STwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        STserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        GTotRegis += Convert.ToInt64(dr("regis"))
                        GTotServe += Convert.ToInt64(dr("served"))
                        GTotMissedCall += Convert.ToInt64(dr("missed_call"))
                        GTotCancelled += Convert.ToInt64(dr("cancel"))
                        GTotIncomplete += Incomplete
                        Gwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Gserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))

                        If dr("max_wt") > 0 Then
                            Tmax_wt += Convert.ToInt64(dr("max_wt"))
                            Tx += 1

                            STmax_wt += Convert.ToInt64(dr("max_wt"))
                            STx += 1

                            Gmax_wt += Convert.ToInt64(dr("max_wt"))
                            Gx += 1
                        End If
                        If dr("max_ht") > 0 Then
                            Tmax_ht += Convert.ToInt64(dr("max_ht"))
                            Ty += 1

                            STmax_ht += Convert.ToInt64(dr("max_ht"))
                            STy += 1

                            Gmax_ht += Convert.ToInt64(dr("max_ht"))
                            Gy += 1
                        End If
                    Next
                    dv = Nothing
                    dt.DefaultView.RowFilter = ""

                    'Sub Total By Month
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & Convert.ToDateTime(dDr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STwt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STserve_with_kpi, "###,##0") & "</td>")

                    Dim S1wt1 As String = "0.00"
                    Dim S1wt2 As String = "0.00"
                    Dim S1ht1 As String = "0.00"
                    Dim S1ht2 As String = "0.00"
                    Dim S1mc As String = "0.00"
                    If TotServe > 0 Then
                        S1wt1 = Format((STwt_with_kpi / STotServe) * 100, "##0.00")
                        S1wt2 = Format(100 - ((STwt_with_kpi / STotServe) * 100), "##0.00")
                        S1ht1 = Format((STserve_with_kpi / STotServe) * 100, "##0.00")
                        S1ht2 = Format(100 - ((STserve_with_kpi / STotServe) * 100), "##0.00")
                    End If

                    ' % Missed Call
                    If TotRegis > 0 Then
                        S1mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                    End If
                    ret.Append("        <td align='right' >&nbsp;" & S1wt1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1wt2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1mc & "%</td>")

                    '****************** AGV WT ******************
                    Dim StAwt As Integer = 0
                    If STx <> 0 Then StAwt = (Format(STmax_wt / STx, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(StAwt) & "</td>")
                    '****************** AGV HT ******************
                    Dim stAht As Integer = 0
                    If STy <> 0 Then stAht = (Format(STmax_ht / STy, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(stAht) & "</td>")
                    '********************************************
                    ret.Append("    </tr>")
                Next
                dDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                Dim T1wt1 As String = "0.00"
                Dim T1wt2 As String = "0.00"
                Dim T1ht1 As String = "0.00"
                Dim T1ht2 As String = "0.00"
                Dim T1mc As String = "0.00"
                If TotServe > 0 Then
                    T1wt1 = Format((Twt_with_kpi / TotServe) * 100, "##0.00")
                    T1wt2 = Format(100 - ((Twt_with_kpi / TotServe) * 100), "##0.00")
                    T1ht1 = Format((Tserve_with_kpi / TotServe) * 100, "##0.00")
                    T1ht2 = Format(100 - ((Tserve_with_kpi / TotServe) * 100), "##0.00")
                End If

                ' % Missed Call
                If TotRegis > 0 Then
                    T1mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                End If
                ret.Append("        <td align='right' >&nbsp;" & T1wt1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1wt2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1ht1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1ht2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & T1mc & "%</td>")

                '****************** AGV WT ******************
                Dim ttAwt As Integer = 0
                If Tx <> 0 Then ttAwt = (Format(Tmax_wt / Tx, "##0"))
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                '****************** AGV HT ******************
                Dim ttAht As Integer = 0
                If Ty <> 0 Then ttAht = (Format(Tmax_ht / Ty, "##0"))
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                '********************************************
                ret.Append("    </tr>")
            Next
            shDt.Dispose()

            '******************** Grand Total ************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "#,##0") & "</td>")

            Dim Gwt1 As String = "0.00"
            Dim Gwt2 As String = "0.00"
            Dim Ght1 As String = "0.00"
            Dim Ght2 As String = "0.00"
            Dim Gmc As String = "0.00"
            If GTotServe > 0 Then
                Gwt1 = Format((Gwt_with_kpi / GTotServe) * 100, "##0.00")
                Gwt2 = Format(100 - ((Gwt_with_kpi / GTotServe) * 100), "##0.00")
                Ght1 = Format((Gserve_with_kpi / GTotServe) * 100, "##0.00")
                Ght2 = Format(100 - ((Gserve_with_kpi / GTotServe) * 100), "##0.00")
            End If

            ' %Missed Call
            If GTotRegis > 0 Then
                Gmc = Format((GTotMissedCall / GTotRegis) * 100, "##0.00")
            End If
            ret.Append("        <td align='right' >&nbsp;" & Gwt1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gwt2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gmc & "%</td>")

            '****************** AGV WT ******************
            Dim GAwt As Integer = 0
            If Gx <> 0 Then GAwt = (Format(Gmax_wt / Gx, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>")

            '****************** AGV HT ******************
            Dim GAht As Integer = 0
            If Gy <> 0 Then GAht = (Format(Gmax_ht / Gy, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>")
            '********************************************
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
            dt.Dispose()
        End If
    End Sub

    Private Sub RenderReportByTime(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara)
        Dim ret As New StringBuilder
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
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiTimeCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date, show_time", trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            btnExport.Visible = True
        End If

        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Time</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
            'ret.Append("        <td align='center' style='color: #ffffff;' >Abandon</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")
            'ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>")
            'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>")
            'ret.Append("    </tr>")

            'For TotalRow
            Dim TotRegis As Long = 0
            Dim TotServe As Long = 0
            Dim TotMissedCall As Long = 0
            Dim TotCancelled As Long = 0
            Dim TotIncomplete As Long = 0
            Dim Twt_with_kpi As Long = 0
            Dim Tserve_with_kpi As Long = 0
            Dim Tmax_wt As Long = 0
            Dim Tmax_ht As Long = 0
            Dim Tx As Integer = 0
            Dim Ty As Integer = 0

            Dim STotRegis As Long = 0
            Dim STotServe As Long = 0
            Dim STotMissedCall As Long = 0
            Dim STotCancelled As Long = 0
            Dim STotIncomplete As Long = 0
            Dim Swt_with_kpi As Long = 0
            Dim Sserve_with_kpi As Long = 0
            Dim Smax_wt As Long = 0
            Dim Smax_ht As Long = 0
            Dim Sx As Integer = 0
            Dim Sy As Integer = 0

            Dim GTotRegis As Long = 0
            Dim GTotServe As Long = 0
            Dim GTotMissedCall As Long = 0
            Dim GTotCancelled As Long = 0
            Dim GTotIncomplete As Long = 0
            Dim Gwt_with_kpi As Long = 0
            Dim Gserve_with_kpi As Long = 0
            Dim Gmax_wt As Long = 0
            Dim Gmax_ht As Long = 0
            Dim Gx As Integer = 0
            Dim Gy As Integer = 0

            Dim ShopIDGroup As Int32 = 0
            Dim ShopID As Int32 = 0
            Dim DateGroup As String = ""
            Dim ShopName As String = ""

            For i As Integer = 0 To dt.Rows.Count - 1
                If ShopIDGroup = 0 Then
                    ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
                    ShopID = dt.Rows(i).Item("shop_id").ToString
                    DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
                End If
                '********************* File Data *************************
                ret.Append("    <tr  >")
                ret.Append("        <td align='left' >&nbsp;" & dt.Rows(i).Item("shop_name_en") & "</td>")
                ret.Append("        <td align='left'>&nbsp;" & Convert.ToDateTime(dt.Rows(i).Item("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                ret.Append("        <td align='left'>&nbsp;" & dt.Rows(i).Item("show_time") & "</td>")
                ret.Append("        <td align='left' >&nbsp;" & dt.Rows(i).Item("service_name") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("regis"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("missed_call"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("cancel"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("served"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("wait_with_kpi"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("serve_with_kpi"), "###,##0") & "</td>")

                Dim perWt1 As String = "0.00"
                Dim perWt2 As String = "0.00"
                Dim perHt1 As String = "0.00"
                Dim perHt2 As String = "0.00"
                Dim perMs As String = "0.00"

                If dt.Rows(i).Item("served") > 0 Then
                    perWt1 = Format((dt.Rows(i).Item("wait_with_kpi") / dt.Rows(i).Item("served")) * 100, "##0.00")
                    perWt2 = Format(100 - (dt.Rows(i).Item("wait_with_kpi") / dt.Rows(i).Item("served")) * 100, "##0.00")
                    perHt1 = Format((dt.Rows(i).Item("serve_with_kpi") / dt.Rows(i).Item("served")) * 100, "##0.00")
                    perHt2 = Format(100 - (dt.Rows(i).Item("serve_with_kpi") / dt.Rows(i).Item("served")) * 100, "##0.00")
                End If

                If dt.Rows(i).Item("regis") > 0 Then
                    perMs = Format((dt.Rows(i).Item("missed_call") / dt.Rows(i).Item("regis")) * 100, "##0.00")
                End If

                ret.Append("        <td align='right' >&nbsp;" & perWt1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & perWt2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & perHt1 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & perHt2 & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & perMs & "%</td>")
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dt.Rows(i).Item("max_wt")) & "</td>")
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(dt.Rows(i).Item("max_ht")) & "</td>")
                ret.Append("    </tr>")

                TotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
                TotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
                TotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
                TotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
                TotIncomplete += Convert.ToInt64(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")))
                Twt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
                Tserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
                Tmax_wt += Convert.ToInt64(dt.Rows(i).Item("max_wt"))
                Tmax_ht += Convert.ToInt64(dt.Rows(i).Item("max_ht"))

                STotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
                STotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
                STotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
                STotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
                STotIncomplete += Convert.ToInt64(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")))
                Swt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
                Sserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
                Smax_wt += Convert.ToInt64(dt.Rows(i).Item("max_wt"))
                Smax_ht += Convert.ToInt64(dt.Rows(i).Item("max_ht"))

                GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
                GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
                GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
                GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
                GTotIncomplete += Convert.ToInt64(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")))
                Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
                Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
                Gmax_wt += Convert.ToInt64(dt.Rows(i).Item("max_wt"))
                Gmax_ht += Convert.ToInt64(dt.Rows(i).Item("max_ht"))

                If dt.Rows(i).Item("max_wt") > 0 Then
                    Tx += 1
                    Sx += 1
                    Gx += 1
                End If
                If dt.Rows(i).Item("max_ht") > 0 Then
                    Ty += 1
                    Sy += 1
                    Gy += 1
                End If

                '********************* Sub Total *************************
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
                    ret.Append("        <td align='center' >" & dt.Rows(i).Item("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                    Dim wt1 As String = "0.00"
                    Dim wt2 As String = "0.00"
                    Dim ht1 As String = "0.00"
                    Dim ht2 As String = "0.00"
                    Dim mc As String = "0.00"

                    If TotServe > 0 Then
                        wt1 = Format((Twt_with_kpi / TotServe) * 100, "##0.00")
                        wt2 = Format(100 - ((Twt_with_kpi / TotServe) * 100), "##0.00")
                        ht1 = Format((Tserve_with_kpi / TotServe) * 100, "##0.00")
                        ht2 = Format(100 - ((Tserve_with_kpi / TotServe) * 100), "##0.00")
                    End If

                    '% Missed Call
                    If TotRegis > 0 Then
                        mc = Format((TotMissedCall / TotRegis) * 100, "##0.00")
                    End If
                    ret.Append("        <td align='right' >&nbsp;" & wt1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & wt2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & ht1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & ht2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & mc & "%</td>")
                    '****************** AGV WT ******************
                    Dim SAwt As Integer = 0
                    If Tx <> 0 Then SAwt = (Format(Tmax_wt / Tx, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>")

                    '****************** AGV HT ******************
                    Dim SAht As Integer = 0
                    If Ty <> 0 Then SAht = (Format(Tmax_ht / Ty, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>")
                    '********************************************
                    ret.Append("    </tr>")

                    TotRegis = 0
                    TotServe = 0
                    TotMissedCall = 0
                    TotCancelled = 0
                    TotIncomplete = 0
                    Twt_with_kpi = 0
                    Tserve_with_kpi = 0
                    Tmax_wt = 0
                    Tmax_ht = 0
                    Tx = 0
                    Ty = 0
                End If
                '*********************************************************
                '************************ Total ***************************
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
                    ret.Append("        <td align='center' >" & dt.Rows(i).Item("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >Total</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Swt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Sserve_with_kpi, "###,##0") & "</td>")

                    Dim S1wt1 As String = "0.00"
                    Dim S1wt2 As String = "0.00"
                    Dim S1ht1 As String = "0.00"
                    Dim S1ht2 As String = "0.00"
                    Dim S1mc As String = "0.00"

                    If STotServe > 0 Then
                        S1wt1 = Format((Swt_with_kpi / STotServe) * 100, "##0.00")
                        S1wt2 = Format(100 - ((Swt_with_kpi / STotServe) * 100), "##0.00")
                        S1ht1 = Format((Sserve_with_kpi / STotServe) * 100, "##0.00")
                        S1ht2 = Format(100 - ((Sserve_with_kpi / STotServe) * 100), "##0.00")
                    End If

                    ' % Missed Call
                    If STotRegis > 0 Then
                        S1mc = Format((STotMissedCall / STotRegis) * 100, "##0.00")
                    End If
                    ret.Append("        <td align='right' >&nbsp;" & S1wt1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1wt2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1mc & "%</td>")

                    '****************** AGV WT ******************
                    Dim ttAwt As Integer = 0
                    If Sx <> 0 Then ttAwt = (Format(Smax_wt / Sx, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                    '****************** AGV HT ******************

                    Dim ttAht As Integer = 0
                    If Sy <> 0 Then ttAht = (Format(Smax_ht / Sy, "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                    '********************************************
                    ret.Append("    </tr>")

                    STotRegis = 0
                    STotServe = 0
                    STotMissedCall = 0
                    STotCancelled = 0
                    STotIncomplete = 0
                    Swt_with_kpi = 0
                    Sserve_with_kpi = 0
                    Smax_wt = 0
                    Smax_ht = 0
                    Sx = 0
                    Sy = 0
                End If
                '*********************************************************
            Next

            '******************** Grand Total ************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='4'>Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "###,##0") & "</td>")

            Dim Gwt1 As String = "0.00"
            Dim Gwt2 As String = "0.00"
            Dim Ght1 As String = "0.00"
            Dim Ght2 As String = "0.00"
            Dim Gmc As String = "0.00"

            If GTotServe > 0 Then
                Gwt1 = Format((Gwt_with_kpi / GTotServe) * 100, "##0.00")
                Gwt2 = Format(100 - ((Gwt_with_kpi / GTotServe) * 100), "##0.00")
                Ght1 = Format((Gserve_with_kpi / GTotServe) * 100, "##0.00")
                Ght2 = Format(100 - ((Gserve_with_kpi / GTotServe) * 100), "##0.00")
            End If

            ' %Missed Call
            If GTotRegis > 0 Then
                Gmc = Format((GTotMissedCall / GTotRegis) * 100, "##0.00")
            End If
            ret.Append("        <td align='right' >&nbsp;" & Gwt1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gwt2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght1 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Ght2 & "%</td>")
            ret.Append("        <td align='right' >&nbsp;" & Gmc & "%</td>")

            '****************** AGV WT ******************
            Dim GAwt As Integer = 0
            If Gx <> 0 Then GAwt = (Format(Gmax_wt / Gx, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>")

            '****************** AGV HT ******************
            Dim GAht As Integer = 0
            If Gy <> 0 Then GAht = (Format(Gmax_ht / Gy, "##0"))
            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>")
            '********************************************
            ret.Append("    </tr>")
            '*********************************************************
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
        End If
    End Sub

    'Private Function RenderReportByDay(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara) As String
    '    Dim ret As String = ""

    '    Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
    '    Dim ConvertDay As String = InputPara.DayInWeek
    '    ConvertDay = ConvertDay.Replace("1", "'Sunday'")
    '    ConvertDay = ConvertDay.Replace("2", "'Monday'")
    '    ConvertDay = ConvertDay.Replace("3", "'Tuesday'")
    '    ConvertDay = ConvertDay.Replace("4", "'Wednesday'")
    '    ConvertDay = ConvertDay.Replace("5", "'Thursday'")
    '    ConvertDay = ConvertDay.Replace("6", "'Friday'")
    '    ConvertDay = ConvertDay.Replace("7", "'Saturday'")

    '    WhText += " and show_day in (" & ConvertDay & ")"
    '    WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
    '    WhText += " and show_week between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiDayCenLinqDB
    '    Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
    '    trans.CommitTransaction()

    '    If dt.Rows.Count > 0 Then
    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        'Header Row
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Day</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Week</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Year</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Service</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '        ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Waiting Time With KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve with KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Missed Call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>"
    '        ret += "    </tr>"
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '        ret += "    </tr>"
    '        'For TotalRow
    '        Dim TotRegis As Long = 0
    '        Dim TotServe As Long = 0
    '        Dim TotMissedCall As Long = 0
    '        Dim TotCancelled As Long = 0
    '        Dim TotNotCall As Long = 0
    '        Dim TotNotCon As Long = 0
    '        Dim TotNotEnd As Long = 0
    '        Dim wt_with_kpi As Long = 0
    '        Dim serve_with_kpi As Long = 0
    '        Dim per_awt_with_kpi As Long = 0
    '        Dim per_awt_over_kpi As Long = 0
    '        Dim per_aht_with_kpi As Long = 0
    '        Dim per_aht_over_kpi As Long = 0
    '        Dim per_missed_call As Long = 0
    '        Dim max_wt As Long = 0
    '        Dim max_ht As Long = 0
    '        Dim x As Integer = 0
    '        Dim y As Integer = 0

    '        Dim GTotRegis As Long = 0
    '        Dim GTotServe As Long = 0
    '        Dim GTotMissedCall As Long = 0
    '        Dim GTotCancelled As Long = 0
    '        Dim GTotNotCall As Long = 0
    '        Dim GTotNotCon As Long = 0
    '        Dim GTotNotEnd As Long = 0
    '        Dim Gwt_with_kpi As Long = 0
    '        Dim Gserve_with_kpi As Long = 0
    '        Dim Gper_awt_with_kpi As Long = 0
    '        Dim Gper_awt_over_kpi As Long = 0
    '        Dim Gper_aht_with_kpi As Long = 0
    '        Dim Gper_aht_over_kpi As Long = 0
    '        Dim Gper_missed_call As Long = 0
    '        Dim Gmax_wt As Long = 0
    '        Dim Gmax_ht As Long = 0
    '        Dim Gx As Integer = 0
    '        Dim Gy As Integer = 0

    '        Dim ShopIDGroup As Int32 = 0

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            '********************* Sub Total *************************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='5'>Sub Total</td>"
    '                ret += "        <td align='right' >" & TotRegis & "</td>"
    '                ret += "        <td align='right' >" & TotServe & "</td>"
    '                ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '                ret += "        <td align='right' >" & TotCancelled & "</td>"
    '                ret += "        <td align='right' >" & TotNotCall & "</td>"
    '                ret += "        <td align='right' >" & TotNotCon & "</td>"
    '                ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '                ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '                ret += "        <td align='right' >" & serve_with_kpi & "</td>"

    '                Dim wt1 As Int32 = 0
    '                Dim wt2 As Int32 = 0
    '                Dim ht1 As Int32 = 0
    '                Dim ht2 As Int32 = 0
    '                Dim mc As Int32 = 0
    '                If TotRegis > 0 Then
    '                    wt1 = Convert.ToInt16((wt_with_kpi / TotRegis) * 100)
    '                    wt2 = Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100))
    '                    mc = Convert.ToInt16((TotMissedCall / TotRegis) * 100)
    '                End If
    '                If TotServe > 0 Then
    '                    ht1 = Convert.ToInt16((serve_with_kpi / TotServe) * 100)
    '                    ht2 = Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100))
    '                End If
    '                ret += "        <td align='right' >" & wt1 & "%</td>"
    '                ret += "        <td align='right' >" & wt2 & "%</td>"
    '                ret += "        <td align='right' >" & ht1 & "%</td>"
    '                ret += "        <td align='right' >" & ht2 & "%</td>"
    '                ret += "        <td align='right' >" & mc & "%</td>"
    '                '****************** AGV WT ******************
    '                Dim SAwt As Integer = (max_wt / x)
    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>"

    '                '****************** AGV HT ******************
    '                Dim SAht As Integer = (max_ht / y)

    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>"
    '                '********************************************
    '                ret += "    </tr>"

    '                TotRegis = 0
    '                TotServe = 0
    '                TotMissedCall = 0
    '                TotCancelled = 0
    '                TotNotCall = 0
    '                TotNotCon = 0
    '                TotNotEnd = 0
    '                wt_with_kpi = 0
    '                serve_with_kpi = 0
    '                per_awt_with_kpi = 0
    '                per_awt_over_kpi = 0
    '                per_aht_with_kpi = 0
    '                per_aht_over_kpi = 0
    '                per_missed_call = 0
    '                max_wt = 0
    '                max_ht = 0
    '                x = 0
    '                y = 0
    '            End If
    '            '*********************************************************
    '            '********************* File Data *************************
    '            ret += "    <tr  >"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_day") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_week") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_year") & "</td>"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("service_name") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("regis") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("served") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("missed_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("cancel") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_con") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_end") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("wait_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("serve_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_missed_call") & "%</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_wt") & "</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_ht") & "</td>"
    '            ret += "    </tr>"

    '            TotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            TotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            TotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            TotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            TotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            TotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            TotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            wt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            serve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            per_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            per_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            per_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            per_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            per_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            GTotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            GTotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            GTotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            Gper_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            Gper_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            Gper_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            Gper_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            Gper_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            If dt.Rows(i).Item("max_wt").ToString.Trim <> "" Then
    '                Dim MaxTime() As String = Split(dt.Rows(i).Item("max_wt"), ":")
    '                max_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                Gmax_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                If dt.Rows(i).Item("max_wt") <> "00:00:00" Then
    '                    x = x + 1
    '                    Gx = x + 1
    '                End If
    '            End If

    '            If dt.Rows(i).Item("max_ht").ToString.Trim <> "" Then
    '                Dim MaxTime2() As String = Split(dt.Rows(i).Item("max_ht"), ":")
    '                max_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                Gmax_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                If dt.Rows(i).Item("max_ht") <> "00:00:00" Then
    '                    y = y + 1
    '                    Gy = y + 1
    '                End If
    '            End If
    '        Next
    '        '*********************************************************
    '        '********************* Sub Total *************************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='5'>Sub Total</td>"
    '        ret += "        <td align='right' >" & TotRegis & "</td>"
    '        ret += "        <td align='right' >" & TotServe & "</td>"
    '        ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & TotCancelled & "</td>"
    '        ret += "        <td align='right' >" & TotNotCall & "</td>"
    '        ret += "        <td align='right' >" & TotNotCon & "</td>"
    '        ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & serve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((wt_with_kpi / TotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((serve_with_kpi / TotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((TotMissedCall / TotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim Awt As Integer = (max_wt / x)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Awt) & "</td>"

    '        '****************** AGV HT ******************
    '        Dim Aht As Integer = (max_ht / y)

    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Aht) & "</td>"
    '        '********************************************
    '        ret += "    </tr>"
    '        ''*********************************************************

    '        '******************** Grand Total ************************
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='5'>Total</td>"
    '        ret += "        <td align='right' >" & GTotRegis & "</td>"
    '        ret += "        <td align='right' >" & GTotServe & "</td>"
    '        ret += "        <td align='right' >" & GTotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & GTotCancelled & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCall & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCon & "</td>"
    '        ret += "        <td align='right' >" & GTotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & Gwt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Gserve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gwt_with_kpi / GTotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gwt_with_kpi / GTotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gserve_with_kpi / GTotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gserve_with_kpi / GTotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((GTotMissedCall / GTotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim GAwt As Integer = (Gmax_wt / Gx)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>"
    '        '****************** AGV HT ******************
    '        Dim GAht As Integer = (Gmax_ht / Gy)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>"
    '        '********************************************

    '        ret += "    </tr>"
    '        '*********************************************************
    '        ret += "</table>"
    '    End If

    '    Return ret
    'End Function

    'Private Function RenderReportByWeek(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara) As String
    '    Dim ret As String = ""

    '    Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
    '    WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
    '    WhText += " and show_week between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiWeekCenLinqDB
    '    Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year,show_week", trans.Trans)
    '    trans.CommitTransaction()

    '    If dt.Rows.Count > 0 Then
    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        'Header Row
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '        ret += "        <td rowspan='2'align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '        ret += "        <td rowspan='2'align='center' style='color: #ffffff;' >Week</td>"
    '        ret += "        <td rowspan='2'align='center' style='color: #ffffff;' >Year</td>"
    '        ret += "        <td rowspan='2'align='center' style='color: #ffffff;' >Service</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '        ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Waiting Time With KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve with KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Missed Call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>"
    '        ret += "    </tr>"

    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '        ret += "    </tr>"

    '        'For TotalRow
    '        Dim TotRegis As Long = 0
    '        Dim TotServe As Long = 0
    '        Dim TotMissedCall As Long = 0
    '        Dim TotCancelled As Long = 0
    '        Dim TotNotCall As Long = 0
    '        Dim TotNotCon As Long = 0
    '        Dim TotNotEnd As Long = 0
    '        Dim wt_with_kpi As Long = 0
    '        Dim serve_with_kpi As Long = 0
    '        Dim per_awt_with_kpi As Long = 0
    '        Dim per_awt_over_kpi As Long = 0
    '        Dim per_aht_with_kpi As Long = 0
    '        Dim per_aht_over_kpi As Long = 0
    '        Dim per_missed_call As Long = 0
    '        Dim max_wt As Long = 0
    '        Dim max_ht As Long = 0
    '        Dim x As Integer = 0
    '        Dim y As Integer = 0

    '        Dim GTotRegis As Long = 0
    '        Dim GTotServe As Long = 0
    '        Dim GTotMissedCall As Long = 0
    '        Dim GTotCancelled As Long = 0
    '        Dim GTotNotCall As Long = 0
    '        Dim GTotNotCon As Long = 0
    '        Dim GTotNotEnd As Long = 0
    '        Dim Gwt_with_kpi As Long = 0
    '        Dim Gserve_with_kpi As Long = 0
    '        Dim Gper_awt_with_kpi As Long = 0
    '        Dim Gper_awt_over_kpi As Long = 0
    '        Dim Gper_aht_with_kpi As Long = 0
    '        Dim Gper_aht_over_kpi As Long = 0
    '        Dim Gper_missed_call As Long = 0
    '        Dim Gmax_wt As Long = 0
    '        Dim Gmax_ht As Long = 0
    '        Dim Gx As Integer = 0
    '        Dim Gy As Integer = 0

    '        Dim ShopIDGroup As Int32 = 0

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            '********************* Sub Total *************************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '                ret += "        <td align='right' >" & TotRegis & "</td>"
    '                ret += "        <td align='right' >" & TotServe & "</td>"
    '                ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '                ret += "        <td align='right' >" & TotCancelled & "</td>"
    '                ret += "        <td align='right' >" & TotNotCall & "</td>"
    '                ret += "        <td align='right' >" & TotNotCon & "</td>"
    '                ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '                ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '                ret += "        <td align='right' >" & serve_with_kpi & "</td>"

    '                Dim wt1 As Int32 = 0
    '                Dim wt2 As Int32 = 0
    '                Dim ht1 As Int32 = 0
    '                Dim ht2 As Int32 = 0
    '                Dim mc As Int32 = 0
    '                If TotRegis > 0 Then
    '                    wt1 = Convert.ToInt16((wt_with_kpi / TotRegis) * 100)
    '                    wt2 = Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100))
    '                    mc = Convert.ToInt16((TotMissedCall / TotRegis) * 100)
    '                End If
    '                If TotServe > 0 Then
    '                    ht1 = Convert.ToInt16((serve_with_kpi / TotServe) * 100)
    '                    ht2 = Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100))
    '                End If
    '                ret += "        <td align='right' >" & wt1 & "%</td>"
    '                ret += "        <td align='right' >" & wt2 & "%</td>"
    '                ret += "        <td align='right' >" & ht1 & "%</td>"
    '                ret += "        <td align='right' >" & ht2 & "%</td>"
    '                ret += "        <td align='right' >" & mc & "%</td>"
    '                '****************** AGV WT ******************
    '                Dim SAwt As Integer = (max_wt / x)
    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>"

    '                '****************** AGV HT ******************
    '                Dim SAht As Integer = (max_ht / y)

    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>"
    '                '********************************************
    '                ret += "    </tr>"

    '                TotRegis = 0
    '                TotServe = 0
    '                TotMissedCall = 0
    '                TotCancelled = 0
    '                TotNotCall = 0
    '                TotNotCon = 0
    '                TotNotEnd = 0
    '                wt_with_kpi = 0
    '                serve_with_kpi = 0
    '                per_awt_with_kpi = 0
    '                per_awt_over_kpi = 0
    '                per_aht_with_kpi = 0
    '                per_aht_over_kpi = 0
    '                per_missed_call = 0
    '                max_wt = 0
    '                max_ht = 0
    '                x = 0
    '                y = 0
    '            End If
    '            '*********************************************************
    '            '********************* File Data *************************
    '            ret += "    <tr  >"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & "W" & dt.Rows(i).Item("show_week") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_year") & "</td>"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("service_name") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("regis") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("served") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("missed_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("cancel") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_con") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_end") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("wait_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("serve_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_missed_call") & "%</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_wt") & "</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_ht") & "</td>"
    '            ret += "    </tr>"

    '            TotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            TotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            TotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            TotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            TotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            TotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            TotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            wt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            serve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            per_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            per_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            per_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            per_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            per_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            GTotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            GTotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            GTotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            Gper_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            Gper_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            Gper_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            Gper_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            Gper_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            If dt.Rows(i).Item("max_wt").ToString.Trim <> "" Then
    '                Dim MaxTime() As String = Split(dt.Rows(i).Item("max_wt"), ":")
    '                max_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                Gmax_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                If dt.Rows(i).Item("max_wt") <> "00:00:00" Then
    '                    x = x + 1
    '                    Gx = x + 1
    '                End If
    '            End If

    '            If dt.Rows(i).Item("max_ht").ToString.Trim <> "" Then
    '                Dim MaxTime2() As String = Split(dt.Rows(i).Item("max_ht"), ":")
    '                max_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                Gmax_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                If dt.Rows(i).Item("max_ht") <> "00:00:00" Then
    '                    y = y + 1
    '                    Gy = y + 1
    '                End If
    '            End If
    '        Next
    '        '*********************************************************
    '        '********************* Sub Total *************************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '        ret += "        <td align='right' >" & TotRegis & "</td>"
    '        ret += "        <td align='right' >" & TotServe & "</td>"
    '        ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & TotCancelled & "</td>"
    '        ret += "        <td align='right' >" & TotNotCall & "</td>"
    '        ret += "        <td align='right' >" & TotNotCon & "</td>"
    '        ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & serve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((wt_with_kpi / TotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((serve_with_kpi / TotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((TotMissedCall / TotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim Awt As Integer = (max_wt / x)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Awt) & "</td>"

    '        '****************** AGV HT ******************
    '        Dim Aht As Integer = (max_ht / y)

    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Aht) & "</td>"
    '        '********************************************
    '        ret += "    </tr>"
    '        ''*********************************************************

    '        '******************** Grand Total ************************
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Total</td>"
    '        ret += "        <td align='right' >" & GTotRegis & "</td>"
    '        ret += "        <td align='right' >" & GTotServe & "</td>"
    '        ret += "        <td align='right' >" & GTotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & GTotCancelled & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCall & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCon & "</td>"
    '        ret += "        <td align='right' >" & GTotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & Gwt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Gserve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gwt_with_kpi / GTotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gwt_with_kpi / GTotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gserve_with_kpi / GTotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gserve_with_kpi / GTotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((GTotMissedCall / GTotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim GAwt As Integer = (Gmax_wt / Gx)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>"
    '        '****************** AGV HT ******************
    '        Dim GAht As Integer = (Gmax_ht / Gy)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>"
    '        '********************************************

    '        ret += "    </tr>"
    '        '*********************************************************
    '        ret += "</table>"
    '    End If

    '    Return ret
    'End Function

    'Private Function RenderReportByMonth(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara) As String
    '    Dim ret As String = ""

    '    Dim MonthPara As String = ""
    '    For i As Integer = InputPara.MonthFrom To InputPara.MonthTo
    '        If MonthPara = "" Then
    '            MonthPara = i
    '        Else
    '            MonthPara += "," & i
    '        End If
    '    Next

    '    Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
    '    WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
    '    WhText += " and month_no in (" & MonthPara & ") "

    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiMonthCenLinqDB
    '    Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year,month_no", trans.Trans)
    '    trans.CommitTransaction()

    '    If dt.Rows.Count > 0 Then
    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        'Header Row
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Month</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Year</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Service</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '        ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Waiting Time With KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve with KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Missed Call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>"
    '        ret += "    </tr>"

    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '        ret += "    </tr>"

    '        'For TotalRow
    '        Dim TotRegis As Long = 0
    '        Dim TotServe As Long = 0
    '        Dim TotMissedCall As Long = 0
    '        Dim TotCancelled As Long = 0
    '        Dim TotNotCall As Long = 0
    '        Dim TotNotCon As Long = 0
    '        Dim TotNotEnd As Long = 0
    '        Dim wt_with_kpi As Long = 0
    '        Dim serve_with_kpi As Long = 0
    '        Dim per_awt_with_kpi As Long = 0
    '        Dim per_awt_over_kpi As Long = 0
    '        Dim per_aht_with_kpi As Long = 0
    '        Dim per_aht_over_kpi As Long = 0
    '        Dim per_missed_call As Long = 0
    '        Dim max_wt As Long = 0
    '        Dim max_ht As Long = 0
    '        Dim x As Integer = 0
    '        Dim y As Integer = 0

    '        Dim GTotRegis As Long = 0
    '        Dim GTotServe As Long = 0
    '        Dim GTotMissedCall As Long = 0
    '        Dim GTotCancelled As Long = 0
    '        Dim GTotNotCall As Long = 0
    '        Dim GTotNotCon As Long = 0
    '        Dim GTotNotEnd As Long = 0
    '        Dim Gwt_with_kpi As Long = 0
    '        Dim Gserve_with_kpi As Long = 0
    '        Dim Gper_awt_with_kpi As Long = 0
    '        Dim Gper_awt_over_kpi As Long = 0
    '        Dim Gper_aht_with_kpi As Long = 0
    '        Dim Gper_aht_over_kpi As Long = 0
    '        Dim Gper_missed_call As Long = 0
    '        Dim Gmax_wt As Long = 0
    '        Dim Gmax_ht As Long = 0
    '        Dim Gx As Integer = 0
    '        Dim Gy As Integer = 0

    '        Dim ShopIDGroup As Int32 = 0

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            '********************* Sub Total *************************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '                ret += "        <td align='right' >" & TotRegis & "</td>"
    '                ret += "        <td align='right' >" & TotServe & "</td>"
    '                ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '                ret += "        <td align='right' >" & TotCancelled & "</td>"
    '                ret += "        <td align='right' >" & TotNotCall & "</td>"
    '                ret += "        <td align='right' >" & TotNotCon & "</td>"
    '                ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '                ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '                ret += "        <td align='right' >" & serve_with_kpi & "</td>"

    '                Dim wt1 As Int32 = 0
    '                Dim wt2 As Int32 = 0
    '                Dim ht1 As Int32 = 0
    '                Dim ht2 As Int32 = 0
    '                Dim mc As Int32 = 0
    '                If TotRegis > 0 Then
    '                    wt1 = Convert.ToInt16((wt_with_kpi / TotRegis) * 100)
    '                    wt2 = Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100))
    '                    mc = Convert.ToInt16((TotMissedCall / TotRegis) * 100)
    '                End If
    '                If TotServe > 0 Then
    '                    ht1 = Convert.ToInt16((serve_with_kpi / TotServe) * 100)
    '                    ht2 = Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100))
    '                End If
    '                ret += "        <td align='right' >" & wt1 & "%</td>"
    '                ret += "        <td align='right' >" & wt2 & "%</td>"
    '                ret += "        <td align='right' >" & ht1 & "%</td>"
    '                ret += "        <td align='right' >" & ht2 & "%</td>"
    '                ret += "        <td align='right' >" & mc & "%</td>"
    '                '****************** AGV WT ******************
    '                Dim SAwt As Integer = (max_wt / x)
    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>"

    '                '****************** AGV HT ******************
    '                Dim SAht As Integer = (max_ht / y)

    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>"
    '                '********************************************
    '                ret += "    </tr>"

    '                TotRegis = 0
    '                TotServe = 0
    '                TotMissedCall = 0
    '                TotCancelled = 0
    '                TotNotCall = 0
    '                TotNotCon = 0
    '                TotNotEnd = 0
    '                wt_with_kpi = 0
    '                serve_with_kpi = 0
    '                per_awt_with_kpi = 0
    '                per_awt_over_kpi = 0
    '                per_aht_with_kpi = 0
    '                per_aht_over_kpi = 0
    '                per_missed_call = 0
    '                max_wt = 0
    '                max_ht = 0
    '                x = 0
    '                y = 0
    '            End If
    '            '*********************************************************
    '            '********************* File Data *************************
    '            ret += "    <tr  >"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_month") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_year") & "</td>"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("service_name") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("regis") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("served") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("missed_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("cancel") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_con") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_end") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("wait_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("serve_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_missed_call") & "%</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_wt") & "</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_ht") & "</td>"
    '            ret += "    </tr>"

    '            TotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            TotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            TotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            TotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            TotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            TotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            TotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            wt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            serve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            per_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            per_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            per_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            per_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            per_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            GTotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            GTotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            GTotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            Gper_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            Gper_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            Gper_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            Gper_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            Gper_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            If dt.Rows(i).Item("max_wt").ToString.Trim <> "" Then
    '                Dim MaxTime() As String = Split(dt.Rows(i).Item("max_wt"), ":")
    '                max_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                Gmax_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                If dt.Rows(i).Item("max_wt") <> "00:00:00" Then
    '                    x = x + 1
    '                    Gx = x + 1
    '                End If
    '            End If

    '            If dt.Rows(i).Item("max_ht").ToString.Trim <> "" Then
    '                Dim MaxTime2() As String = Split(dt.Rows(i).Item("max_ht"), ":")
    '                max_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                Gmax_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                If dt.Rows(i).Item("max_ht") <> "00:00:00" Then
    '                    y = y + 1
    '                    Gy = y + 1
    '                End If
    '            End If
    '        Next
    '        '*********************************************************
    '        '********************* Sub Total *************************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '        ret += "        <td align='right' >" & TotRegis & "</td>"
    '        ret += "        <td align='right' >" & TotServe & "</td>"
    '        ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & TotCancelled & "</td>"
    '        ret += "        <td align='right' >" & TotNotCall & "</td>"
    '        ret += "        <td align='right' >" & TotNotCon & "</td>"
    '        ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & serve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((wt_with_kpi / TotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((serve_with_kpi / TotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((TotMissedCall / TotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim Awt As Integer = (max_wt / x)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Awt) & "</td>"

    '        '****************** AGV HT ******************
    '        Dim Aht As Integer = (max_ht / y)

    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Aht) & "</td>"
    '        '********************************************
    '        ret += "    </tr>"
    '        ''*********************************************************

    '        '******************** Grand Total ************************
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Total</td>"
    '        ret += "        <td align='right' >" & GTotRegis & "</td>"
    '        ret += "        <td align='right' >" & GTotServe & "</td>"
    '        ret += "        <td align='right' >" & GTotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & GTotCancelled & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCall & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCon & "</td>"
    '        ret += "        <td align='right' >" & GTotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & Gwt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Gserve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gwt_with_kpi / GTotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gwt_with_kpi / GTotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gserve_with_kpi / GTotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gserve_with_kpi / GTotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((GTotMissedCall / GTotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim GAwt As Integer = (Gmax_wt / Gx)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>"
    '        '****************** AGV HT ******************
    '        Dim GAht As Integer = (Gmax_ht / Gy)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>"
    '        '********************************************

    '        ret += "    </tr>"
    '        '*********************************************************
    '        ret += "</table>"
    '    End If

    '    Return ret
    'End Function

    'Private Function RenderReportByYear(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara) As String
    '    Dim ret As String = ""

    '    Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
    '    WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiYearCenLinqDB
    '    Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year,show_quarter", trans.Trans)
    '    trans.CommitTransaction()

    '    If dt.Rows.Count > 0 Then
    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        'Header Row
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Year</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Quarter</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Service</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '        ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Waiting Time With KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve with KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Missed Call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>"
    '        ret += "    </tr>"
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '        ret += "    </tr>"
    '        'For TotalRow
    '        Dim TotRegis As Long = 0
    '        Dim TotServe As Long = 0
    '        Dim TotMissedCall As Long = 0
    '        Dim TotCancelled As Long = 0
    '        Dim TotNotCall As Long = 0
    '        Dim TotNotCon As Long = 0
    '        Dim TotNotEnd As Long = 0
    '        Dim wt_with_kpi As Long = 0
    '        Dim serve_with_kpi As Long = 0
    '        Dim per_awt_with_kpi As Long = 0
    '        Dim per_awt_over_kpi As Long = 0
    '        Dim per_aht_with_kpi As Long = 0
    '        Dim per_aht_over_kpi As Long = 0
    '        Dim per_missed_call As Long = 0
    '        Dim max_wt As Long = 0
    '        Dim max_ht As Long = 0
    '        Dim x As Integer = 0
    '        Dim y As Integer = 0

    '        Dim GTotRegis As Long = 0
    '        Dim GTotServe As Long = 0
    '        Dim GTotMissedCall As Long = 0
    '        Dim GTotCancelled As Long = 0
    '        Dim GTotNotCall As Long = 0
    '        Dim GTotNotCon As Long = 0
    '        Dim GTotNotEnd As Long = 0
    '        Dim Gwt_with_kpi As Long = 0
    '        Dim Gserve_with_kpi As Long = 0
    '        Dim Gper_awt_with_kpi As Long = 0
    '        Dim Gper_awt_over_kpi As Long = 0
    '        Dim Gper_aht_with_kpi As Long = 0
    '        Dim Gper_aht_over_kpi As Long = 0
    '        Dim Gper_missed_call As Long = 0
    '        Dim Gmax_wt As Long = 0
    '        Dim Gmax_ht As Long = 0
    '        Dim Gx As Integer = 0
    '        Dim Gy As Integer = 0

    '        Dim ShopIDGroup As Int32 = 0

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            '********************* Sub Total *************************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '                ret += "        <td align='right' >" & TotRegis & "</td>"
    '                ret += "        <td align='right' >" & TotServe & "</td>"
    '                ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '                ret += "        <td align='right' >" & TotCancelled & "</td>"
    '                ret += "        <td align='right' >" & TotNotCall & "</td>"
    '                ret += "        <td align='right' >" & TotNotCon & "</td>"
    '                ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '                ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '                ret += "        <td align='right' >" & serve_with_kpi & "</td>"

    '                Dim wt1 As Int32 = 0
    '                Dim wt2 As Int32 = 0
    '                Dim ht1 As Int32 = 0
    '                Dim ht2 As Int32 = 0
    '                Dim mc As Int32 = 0
    '                If TotRegis > 0 Then
    '                    wt1 = Convert.ToInt16((wt_with_kpi / TotRegis) * 100)
    '                    wt2 = Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100))
    '                    mc = Convert.ToInt16((TotMissedCall / TotRegis) * 100)
    '                End If
    '                If TotServe > 0 Then
    '                    ht1 = Convert.ToInt16((serve_with_kpi / TotServe) * 100)
    '                    ht2 = Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100))
    '                End If
    '                ret += "        <td align='right' >" & wt1 & "%</td>"
    '                ret += "        <td align='right' >" & wt2 & "%</td>"
    '                ret += "        <td align='right' >" & ht1 & "%</td>"
    '                ret += "        <td align='right' >" & ht2 & "%</td>"
    '                ret += "        <td align='right' >" & mc & "%</td>"
    '                '****************** AGV WT ******************
    '                Dim SAwt As Integer = (max_wt / x)
    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>"

    '                '****************** AGV HT ******************
    '                Dim SAht As Integer = (max_ht / y)

    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>"
    '                '********************************************
    '                ret += "    </tr>"

    '                TotRegis = 0
    '                TotServe = 0
    '                TotMissedCall = 0
    '                TotCancelled = 0
    '                TotNotCall = 0
    '                TotNotCon = 0
    '                TotNotEnd = 0
    '                wt_with_kpi = 0
    '                serve_with_kpi = 0
    '                per_awt_with_kpi = 0
    '                per_awt_over_kpi = 0
    '                per_aht_with_kpi = 0
    '                per_aht_over_kpi = 0
    '                per_missed_call = 0
    '                max_wt = 0
    '                max_ht = 0
    '                x = 0
    '                y = 0
    '            End If
    '            '*********************************************************
    '            '********************* File Data *************************
    '            ret += "    <tr  >"
    '            ret += "        <td aalign='left' >" & dt.Rows(i).Item("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_year") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_quarter") & "</td>"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("service_name") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("regis") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("served") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("missed_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("cancel") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_con") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_end") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("wait_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("serve_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_missed_call") & "%</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_wt") & "</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_ht") & "</td>"
    '            ret += "    </tr>"

    '            TotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            TotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            TotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            TotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            TotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            TotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            TotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            wt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            serve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            per_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            per_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            per_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            per_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            per_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            GTotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            GTotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            GTotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            Gper_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            Gper_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            Gper_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            Gper_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            Gper_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            If dt.Rows(i).Item("max_wt").ToString.Trim <> "" Then
    '                Dim MaxTime() As String = Split(dt.Rows(i).Item("max_wt"), ":")
    '                max_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                Gmax_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                If dt.Rows(i).Item("max_wt") <> "00:00:00" Then
    '                    x = x + 1
    '                    Gx = x + 1
    '                End If
    '            End If

    '            If dt.Rows(i).Item("max_ht").ToString.Trim <> "" Then
    '                Dim MaxTime2() As String = Split(dt.Rows(i).Item("max_ht"), ":")
    '                max_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                Gmax_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                If dt.Rows(i).Item("max_ht") <> "00:00:00" Then
    '                    y = y + 1
    '                    Gy = y + 1
    '                End If
    '            End If
    '        Next
    '        '*********************************************************
    '        '********************* Sub Total *************************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '        ret += "        <td align='right' >" & TotRegis & "</td>"
    '        ret += "        <td align='right' >" & TotServe & "</td>"
    '        ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & TotCancelled & "</td>"
    '        ret += "        <td align='right' >" & TotNotCall & "</td>"
    '        ret += "        <td align='right' >" & TotNotCon & "</td>"
    '        ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & serve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((wt_with_kpi / TotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((serve_with_kpi / TotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((TotMissedCall / TotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim Awt As Integer = (max_wt / x)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Awt) & "</td>"

    '        '****************** AGV HT ******************
    '        Dim Aht As Integer = (max_ht / y)

    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Aht) & "</td>"
    '        '********************************************
    '        ret += "    </tr>"
    '        ''*********************************************************

    '        '******************** Grand Total ************************
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Total</td>"
    '        ret += "        <td align='right' >" & GTotRegis & "</td>"
    '        ret += "        <td align='right' >" & GTotServe & "</td>"
    '        ret += "        <td align='right' >" & GTotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & GTotCancelled & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCall & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCon & "</td>"
    '        ret += "        <td align='right' >" & GTotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & Gwt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Gserve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gwt_with_kpi / GTotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gwt_with_kpi / GTotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gserve_with_kpi / GTotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gserve_with_kpi / GTotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((GTotMissedCall / GTotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim GAwt As Integer = (Gmax_wt / Gx)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>"
    '        '****************** AGV HT ******************
    '        Dim GAht As Integer = (Gmax_ht / Gy)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>"
    '        '********************************************

    '        ret += "    </tr>"
    '        '*********************************************************
    '        ret += "</table>"
    '    End If

    '    Return ret
    'End Function

    'Private Function RenderReportByTime(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara) As String
    '    Dim ret As String = ""

    '    Dim StrTime As DateTime = Date.ParseExact(InputPara.TimePeroidFrom, "HH:mm", Nothing)
    '    Dim EndTime As DateTime = Date.ParseExact(InputPara.TimePeroidTo, "HH:mm", Nothing)
    '    Dim CurrTime As DateTime = StrTime
    '    Dim InpTime As String = ""
    '    Do
    '        If CurrTime < EndTime Then
    '            Dim tmp As String = "'" & CurrTime.ToString("HH:mm") & "-" & DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime).ToString("HH:mm") & "'"
    '            If InpTime = "" Then
    '                InpTime = tmp
    '            Else
    '                InpTime += "," & tmp
    '            End If
    '        End If
    '        CurrTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
    '    Loop While CurrTime <= EndTime

    '    Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
    '    WhText += " and interval_minute = '" & InputPara.IntervalMinute & "'"
    '    WhText += " and convert(varchar(10),time_priod_from,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
    '    WhText += " and convert(varchar(10),time_priod_to,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
    '    WhText += " and show_time in (" & InpTime & ") "
    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiTimeCenLinqDB
    '    Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date, show_time", trans.Trans)
    '    trans.CommitTransaction()

    '    If dt.Rows.Count > 0 Then
    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        'Header Row
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Date</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Time</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Service</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '        ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Waiting Time With KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve with KPI</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve WT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT to Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Achieve HT over Target</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >%Missed Call</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>"
    '        ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>"
    '        ret += "    </tr>"
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '        ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '        ret += "    </tr>"
    '        'For TotalRow
    '        Dim TotRegis As Long = 0
    '        Dim TotServe As Long = 0
    '        Dim TotMissedCall As Long = 0
    '        Dim TotCancelled As Long = 0
    '        Dim TotNotCall As Long = 0
    '        Dim TotNotCon As Long = 0
    '        Dim TotNotEnd As Long = 0
    '        Dim wt_with_kpi As Long = 0
    '        Dim serve_with_kpi As Long = 0
    '        Dim per_awt_with_kpi As Long = 0
    '        Dim per_awt_over_kpi As Long = 0
    '        Dim per_aht_with_kpi As Long = 0
    '        Dim per_aht_over_kpi As Long = 0
    '        Dim per_missed_call As Long = 0
    '        Dim max_wt As Long = 0
    '        Dim max_ht As Long = 0
    '        Dim x As Integer = 0
    '        Dim y As Integer = 0

    '        Dim GTotRegis As Long = 0
    '        Dim GTotServe As Long = 0
    '        Dim GTotMissedCall As Long = 0
    '        Dim GTotCancelled As Long = 0
    '        Dim GTotNotCall As Long = 0
    '        Dim GTotNotCon As Long = 0
    '        Dim GTotNotEnd As Long = 0
    '        Dim Gwt_with_kpi As Long = 0
    '        Dim Gserve_with_kpi As Long = 0
    '        Dim Gper_awt_with_kpi As Long = 0
    '        Dim Gper_awt_over_kpi As Long = 0
    '        Dim Gper_aht_with_kpi As Long = 0
    '        Dim Gper_aht_over_kpi As Long = 0
    '        Dim Gper_missed_call As Long = 0
    '        Dim Gmax_wt As Long = 0
    '        Dim Gmax_ht As Long = 0
    '        Dim Gx As Integer = 0
    '        Dim Gy As Integer = 0

    '        Dim ShopIDGroup As Int32 = 0
    '        Dim DateGroup As String = ""

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            '********************* Sub Total *************************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '                DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Or DateGroup <> CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '                ret += "        <td align='right' >" & TotRegis & "</td>"
    '                ret += "        <td align='right' >" & TotServe & "</td>"
    '                ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '                ret += "        <td align='right' >" & TotCancelled & "</td>"
    '                ret += "        <td align='right' >" & TotNotCall & "</td>"
    '                ret += "        <td align='right' >" & TotNotCon & "</td>"
    '                ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '                ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '                ret += "        <td align='right' >" & serve_with_kpi & "</td>"

    '                Dim wt1 As Int32 = 0
    '                Dim wt2 As Int32 = 0
    '                Dim ht1 As Int32 = 0
    '                Dim ht2 As Int32 = 0
    '                Dim mc As Int32 = 0
    '                If TotRegis > 0 Then
    '                    wt1 = Convert.ToInt16((wt_with_kpi / TotRegis) * 100)
    '                    wt2 = Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100))
    '                    mc = Convert.ToInt16((TotMissedCall / TotRegis) * 100)
    '                End If
    '                If TotServe > 0 Then
    '                    ht1 = Convert.ToInt16((serve_with_kpi / TotServe) * 100)
    '                    ht2 = Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100))
    '                End If
    '                ret += "        <td align='right' >" & wt1 & "%</td>"
    '                ret += "        <td align='right' >" & wt2 & "%</td>"
    '                ret += "        <td align='right' >" & ht1 & "%</td>"
    '                ret += "        <td align='right' >" & ht2 & "%</td>"
    '                ret += "        <td align='right' >" & mc & "%</td>"
    '                '****************** AGV WT ******************
    '                Dim SAwt As Integer = (max_wt / x)
    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>"

    '                '****************** AGV HT ******************
    '                Dim SAht As Integer = (max_ht / y)

    '                ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>"
    '                '********************************************
    '                ret += "    </tr>"

    '                TotRegis = 0
    '                TotServe = 0
    '                TotMissedCall = 0
    '                TotCancelled = 0
    '                TotNotCall = 0
    '                TotNotCon = 0
    '                TotNotEnd = 0
    '                wt_with_kpi = 0
    '                serve_with_kpi = 0
    '                per_awt_with_kpi = 0
    '                per_awt_over_kpi = 0
    '                per_aht_with_kpi = 0
    '                per_aht_over_kpi = 0
    '                per_missed_call = 0
    '                max_wt = 0
    '                max_ht = 0
    '                x = 0
    '                y = 0
    '            End If
    '            '*********************************************************
    '            '********************* File Data *************************
    '            ret += "    <tr  >"
    '            ret += "        <td align='left'>" & Convert.ToDateTime(dt.Rows(i).Item("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dt.Rows(i).Item("show_time") & "</td>"
    '            ret += "        <td align='left' >" & dt.Rows(i).Item("service_name") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("regis") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("served") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("missed_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("cancel") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_call") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_con") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("not_end") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("wait_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("serve_with_kpi") & "</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_awt_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_with_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_aht_over_kpi") & "%</td>"
    '            ret += "        <td align='right' >" & dt.Rows(i).Item("per_missed_call") & "%</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_wt") & "</td>"
    '            ret += "        <td align='right' >&nbsp;" & dt.Rows(i).Item("max_ht") & "</td>"
    '            ret += "    </tr>"

    '            TotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            TotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            TotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            TotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            TotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            TotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            TotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            wt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            serve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            per_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            per_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            per_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            per_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            per_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
    '            GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
    '            GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
    '            GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
    '            GTotNotCall += Convert.ToInt64(dt.Rows(i).Item("not_call"))
    '            GTotNotCon += Convert.ToInt64(dt.Rows(i).Item("not_con"))
    '            GTotNotEnd += Convert.ToInt64(dt.Rows(i).Item("not_end"))
    '            Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
    '            Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
    '            Gper_awt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_with_kpi"))
    '            Gper_awt_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_awt_over_kpi"))
    '            Gper_aht_with_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_with_kpi"))
    '            Gper_aht_over_kpi += Convert.ToInt64(dt.Rows(i).Item("per_aht_over_kpi"))
    '            Gper_missed_call += Convert.ToInt64(dt.Rows(i).Item("per_missed_call"))

    '            If dt.Rows(i).Item("max_wt").ToString.Trim <> "" Then
    '                Dim MaxTime() As String = Split(dt.Rows(i).Item("max_wt"), ":")
    '                max_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                Gmax_wt += (MaxTime(0) * 3600) + (MaxTime(1) * 60) + MaxTime(2)
    '                If dt.Rows(i).Item("max_wt") <> "00:00:00" Then
    '                    x = x + 1
    '                    Gx = x + 1
    '                End If
    '            End If

    '            If dt.Rows(i).Item("max_ht").ToString.Trim <> "" Then
    '                Dim MaxTime2() As String = Split(dt.Rows(i).Item("max_ht"), ":")
    '                max_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                Gmax_ht += (MaxTime2(0) * 3600) + (MaxTime2(1) * 60) + MaxTime2(2)
    '                If dt.Rows(i).Item("max_ht") <> "00:00:00" Then
    '                    y = y + 1
    '                    Gy = y + 1
    '                End If
    '            End If
    '        Next
    '        '*********************************************************
    '        '********************* Sub Total *************************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '        ret += "        <td align='right' >" & TotRegis & "</td>"
    '        ret += "        <td align='right' >" & TotServe & "</td>"
    '        ret += "        <td align='right' >" & TotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & TotCancelled & "</td>"
    '        ret += "        <td align='right' >" & TotNotCall & "</td>"
    '        ret += "        <td align='right' >" & TotNotCon & "</td>"
    '        ret += "        <td align='right' >" & TotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & wt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & serve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((wt_with_kpi / TotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((wt_with_kpi / TotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((serve_with_kpi / TotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((serve_with_kpi / TotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((TotMissedCall / TotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim Awt As Integer = (max_wt / x)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Awt) & "</td>"

    '        '****************** AGV HT ******************
    '        Dim Aht As Integer = (max_ht / y)

    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Aht) & "</td>"
    '        '********************************************
    '        ret += "    </tr>"
    '        ''*********************************************************

    '        '******************** Grand Total ************************
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='4'>Grand Total</td>"
    '        ret += "        <td align='right' >" & GTotRegis & "</td>"
    '        ret += "        <td align='right' >" & GTotServe & "</td>"
    '        ret += "        <td align='right' >" & GTotMissedCall & "</td>"
    '        ret += "        <td align='right' >" & GTotCancelled & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCall & "</td>"
    '        ret += "        <td align='right' >" & GTotNotCon & "</td>"
    '        ret += "        <td align='right' >" & GTotNotEnd & "</td>"
    '        ret += "        <td align='right' >" & Gwt_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Gserve_with_kpi & "</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gwt_with_kpi / GTotRegis) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gwt_with_kpi / GTotRegis) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((Gserve_with_kpi / GTotServe) * 100) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16(100 - ((Gserve_with_kpi / GTotServe) * 100)) & "%</td>"
    '        ret += "        <td align='right' >" & Convert.ToInt16((GTotMissedCall / GTotRegis) * 100) & "%</td>"
    '        '****************** AGV WT ******************
    '        Dim GAwt As Integer = (Gmax_wt / Gx)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAwt) & "</td>"
    '        '****************** AGV HT ******************
    '        Dim GAht As Integer = (Gmax_ht / Gy)
    '        ret += "        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(GAht) & "</td>"
    '        '********************************************

    '        ret += "    </tr>"
    '        '*********************************************************
    '        ret += "</table>"
    '    End If

    '    Return ret
    'End Function

 
End Class

