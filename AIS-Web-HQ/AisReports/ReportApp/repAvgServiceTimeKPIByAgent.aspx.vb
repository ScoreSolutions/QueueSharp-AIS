Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repAvgServiceTimeKPIByAgent
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "CustomersServedByStaff_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repAvgServiceTimeKPIByAgent.ExportData", Config.GetLoginHistoryID)
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
        If ReportName = "ByTime" Then
            'Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara
            'para.ShopID = Request("ShopID")
            'para.DateFrom = Request("DateFrom")
            'para.DateTo = Request("DateTo")
            'para.IntervalMinute = Request("Interval")
            'para.TimePeroidFrom = Request("TimeFrom")
            'para.TimePeroidTo = Request("TimeTo")
            'txtData = RenderReportByTime(para)
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            RenderReportByWeek(para)
        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            RenderReportByDate(para)
        ElseIf ReportName = "ByDay" Then
            'Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara
            'para.ShopID = Request("ShopID")
            'para.DayInWeek = Request("Day")
            'para.WeekInYearFrom = Request("WeekFrom")
            'para.WeekInYearTo = Request("WeekTo")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'txtData = RenderReportByDay(para)
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            RenderReportByMonth(para)
        ElseIf ReportName = "ByYear" Then
            'Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara
            'para.ShopID = Request("ShopID")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'txtData = RenderReportByYear(para)
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If

    End Sub

    Private Sub RenderReportByDate(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara)
        Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
        WhText += " and convert(varchar(10),service_date,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
        WhText += " and convert(varchar(10),service_date,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffDayCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date,staff_name,service_id", trans.Trans)
        trans.CommitTransaction()
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
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Call</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >AvgConT</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")
           

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
            Dim TotSct As Long = 0
            Dim TotCct As Long = 0

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
            Dim STotSct As Long = 0
            Dim STotCct As Long = 0

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
            Dim GTotSct As Long = 0
            Dim GTotCct As Long = 0

            Dim StaffName As String = ""
            Dim ShopID As Int32 = 0
            Dim ShopName As String = ""

            For i As Integer = 0 To dt.Rows.Count - 1
                '********************* File Data *************************
                ret.Append("    <tr  >")
                ret.Append("        <td align='left' >&nbsp;" & dt.Rows(i).Item("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;" & dt.Rows(i).Item("show_date") & "</td>")
                ret.Append("        <td align='left' >&nbsp;" & dt.Rows(i).Item("staff_name") & "</td>")
                ret.Append("        <td align='left' >&nbsp;" & dt.Rows(i).Item("user_code") & "</td>")
                ret.Append("        <td align='left' >&nbsp;" & dt.Rows(i).Item("service_name") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("regis"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("served"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("missed_call"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("cancel"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("wait_with_kpi"), "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt.Rows(i).Item("serve_with_kpi"), "###,##0") & "</td>")
                If dt.Rows(i).Item("ccont") = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(dt.Rows(i).Item("scont") / dt.Rows(i).Item("ccont"), "##0")) & "</td>")
                End If

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
                TotSct += Convert.ToInt64(dt.Rows(i).Item("scont"))
                TotCct += Convert.ToInt64(dt.Rows(i).Item("ccont"))
                Tmax_wt += Convert.ToInt64(dt.Rows(i).Item("max_wt"))
                Tmax_ht += Convert.ToInt64(dt.Rows(i).Item("max_ht"))

                STotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
                STotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
                STotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
                STotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
                STotIncomplete += Convert.ToInt64(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")))
                Swt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
                Sserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
                STotSct += Convert.ToInt64(dt.Rows(i).Item("scont"))
                STotCct += Convert.ToInt64(dt.Rows(i).Item("ccont"))
                Smax_wt += Convert.ToInt64(dt.Rows(i).Item("max_wt"))
                Smax_ht += Convert.ToInt64(dt.Rows(i).Item("max_ht"))

                GTotRegis += Convert.ToInt64(dt.Rows(i).Item("regis"))
                GTotServe += Convert.ToInt64(dt.Rows(i).Item("served"))
                GTotMissedCall += Convert.ToInt64(dt.Rows(i).Item("missed_call"))
                GTotCancelled += Convert.ToInt64(dt.Rows(i).Item("cancel"))
                GTotIncomplete += Convert.ToInt64(CDbl(dt.Rows(i).Item("not_call")) + CDbl(dt.Rows(i).Item("not_con")) + CDbl(dt.Rows(i).Item("not_end")))
                Gwt_with_kpi += Convert.ToInt64(dt.Rows(i).Item("wait_with_kpi"))
                Gserve_with_kpi += Convert.ToInt64(dt.Rows(i).Item("serve_with_kpi"))
                GTotSct += Convert.ToInt64(dt.Rows(i).Item("scont"))
                GTotCct += Convert.ToInt64(dt.Rows(i).Item("ccont"))
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
                '*********************************************************
                '********************* Sub Total *************************
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
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                    ret.Append("        <td align='center' colspan='5'>Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                    If TotCct = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(TotSct / TotCct, "##0")) & "</td>")
                    End If

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
                    If Tx = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        Dim SAwt As Integer = Format((Tmax_wt / Tx), "##0")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>")
                    End If
                    'ret += "        <td align='right' >&nbsp;" & max_wt & "</td>"
                    '****************** AGV HT ******************
                    If Ty = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        Dim SAht As Integer = Format((Tmax_ht / Ty), "##0")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>")
                    End If
                    'ret += "        <td align='right' >&nbsp;" & max_ht & "</td>"
                    '********************************************
                    ret.Append("    </tr>")

                    TotRegis = 0
                    TotServe = 0
                    TotMissedCall = 0
                    TotCancelled = 0
                    TotIncomplete = 0
                    TotSct = 0
                    TotCct = 0
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
                    If dt.Rows(i).Item("Shop_id") <> dt.Rows(i + 1).Item("Shop_id") Then
                        ChkTotal = True
                    End If
                End If
                If ChkTotal = True Then
                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' colspan='5' >Total " & ShopName & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Swt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Sserve_with_kpi, "###,##0") & "</td>")
                    If STotCct = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(STotSct / STotCct, "##0")) & "</td>")
                    End If

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

                    ' %Missed Call
                    If STotRegis > 0 Then
                        S1mc = Format((STotMissedCall / STotRegis) * 100, "##0.00")
                    End If
                    ret.Append("        <td align='right' >&nbsp;" & S1wt1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1wt2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht1 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1ht2 & "%</td>")
                    ret.Append("        <td align='right' >&nbsp;" & S1mc & "%</td>")
                    '****************** AGV WT ******************
                    Dim ttAwt As Integer = (Format(Smax_wt / IIf(Sx = 0, 1, Sx), "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                    '****************** AGV HT ******************
                    Dim ttAht As Integer = (Format(Smax_ht / IIf(Sy = 0, 1, Sy), "##0"))
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                    '********************************************
                    ret.Append("    </tr>")
                    STotRegis = 0
                    STotServe = 0
                    STotMissedCall = 0
                    STotCancelled = 0
                    STotIncomplete = 0
                    STotSct = 0
                    STotCct = 0
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
            ret.Append("        <td align='center' colspan='5'>Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "###,##0") & "</td>")
            If GTotCct = 0 Then
                ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
            Else
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(GTotSct / GTotCct, "##0")) & "</td>")
            End If
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

            ' % Missed Call
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
            ret = Nothing
        End If

        'Return ret
    End Sub

    Private Sub RenderReportByWeek(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara)
        Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
        WhText += " and convert(varchar,show_year) +  right('0'+ convert(varchar,week_of_year),2)  between '" & InputPara.YearFrom & Right("0" & InputPara.WeekInYearFrom, 2) & "' and '" & InputPara.YearTo & Right("0" & InputPara.WeekInYearTo, 2) & "'"


        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffWeekCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year,week_of_year", trans.Trans)
        trans.CommitTransaction()
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
            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Week No.</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AvgConT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")

            'For Grand Total
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
            Dim GTotSct As Long = 0
            Dim GTotCct As Long = 0

            'Loop By Shop
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th").Copy
            For Each shDr As DataRow In shDt.Rows
                'For Total By Shop
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
                Dim TotSct As Long = 0
                Dim TotCct As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                dt.DefaultView.Sort = "staff_name"
                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "user_name", "staff_name", "week_of_year", "show_year").Copy
                For Each stDr As DataRow In stDt.Rows
                   
                   
                    dt.DefaultView.RowFilter = " shop_id='" & shDr("shop_id") & "' and staff_name='" & stDr("staff_name") & "' and show_year  ='" & stDr("show_year") & "' and week_of_year='" & stDr("week_of_year") & "'"
                    dt.DefaultView.Sort = "week_of_year"
                    'Loop By Week
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
                    Dim STotSct As Long = 0
                    Dim STotCct As Long = 0


                    For Each ddr As DataRowView In dt.DefaultView
                        '********************* File Data *************************
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left' >&nbsp;" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & stDr("week_of_year") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & stDr("show_year") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;" & ddr("staff_name") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;" & ddr("user_code") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;" & ddr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(ddr("regis"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(ddr("served"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(ddr("missed_call"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(ddr("cancel"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(CDbl(ddr("not_call")) + CDbl(ddr("not_con")) + CDbl(ddr("not_end")), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(ddr("wait_with_kpi"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(ddr("serve_with_kpi"), "###,##0") & "</td>")
                        If ddr("ccont") = 0 Then
                            ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(ddr("scont") / ddr("ccont"), "##0")) & "</td>")
                        End If

                        Dim perWt1 As String = "0.00"
                        Dim perWt2 As String = "0.00"
                        Dim perHt1 As String = "0.00"
                        Dim perHt2 As String = "0.00"
                        Dim perMs As String = "0.00"

                        If ddr("served") > 0 Then
                            perWt1 = Format((ddr("wait_with_kpi") / ddr("served")) * 100, "##0.00")
                            perWt2 = Format(100 - (ddr("wait_with_kpi") / ddr("served")) * 100, "##0.00")
                            perHt1 = Format((ddr("serve_with_kpi") / ddr("served")) * 100, "##0.00")
                            perHt2 = Format(100 - (ddr("serve_with_kpi") / ddr("served")) * 100, "##0.00")
                        End If

                        If ddr("regis") > 0 Then
                            perMs = Format((ddr("missed_call") / ddr("regis")) * 100, "##0.00")
                        End If

                        ret.Append("        <td align='right' >&nbsp;" & perWt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perWt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt1 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perHt2 & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & perMs & "%</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ddr("max_wt")) & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ddr("max_ht")) & "</td>")
                        ret.Append("    </tr>")

                        TotRegis += Convert.ToInt64(ddr("regis"))
                        TotServe += Convert.ToInt64(ddr("served"))
                        TotMissedCall += Convert.ToInt64(ddr("missed_call"))
                        TotCancelled += Convert.ToInt64(ddr("cancel"))
                        TotIncomplete += Convert.ToInt64(CDbl(ddr("not_call")) + CDbl(ddr("not_con")) + CDbl(ddr("not_end")))
                        Twt_with_kpi += Convert.ToInt64(ddr("wait_with_kpi"))
                        Tserve_with_kpi += Convert.ToInt64(ddr("serve_with_kpi"))
                        TotSct += Convert.ToInt64(ddr("scont"))
                        TotCct += Convert.ToInt64(ddr("ccont"))
                        Tmax_wt += Convert.ToInt64(ddr("max_wt"))
                        Tmax_ht += Convert.ToInt64(ddr("max_ht"))

                        STotRegis += Convert.ToInt64(ddr("regis"))
                        STotServe += Convert.ToInt64(ddr("served"))
                        STotMissedCall += Convert.ToInt64(ddr("missed_call"))
                        STotCancelled += Convert.ToInt64(ddr("cancel"))
                        STotIncomplete += Convert.ToInt64(CDbl(ddr("not_call")) + CDbl(ddr("not_con")) + CDbl(ddr("not_end")))
                        Swt_with_kpi += Convert.ToInt64(ddr("wait_with_kpi"))
                        Sserve_with_kpi += Convert.ToInt64(ddr("serve_with_kpi"))
                        STotSct += Convert.ToInt64(ddr("scont"))
                        STotCct += Convert.ToInt64(ddr("ccont"))
                        Smax_wt += Convert.ToInt64(ddr("max_wt"))
                        Smax_ht += Convert.ToInt64(ddr("max_ht"))

                        GTotRegis += Convert.ToInt64(ddr("regis"))
                        GTotServe += Convert.ToInt64(ddr("served"))
                        GTotMissedCall += Convert.ToInt64(ddr("missed_call"))
                        GTotCancelled += Convert.ToInt64(ddr("cancel"))
                        GTotIncomplete += Convert.ToInt64(CDbl(ddr("not_call")) + CDbl(ddr("not_con")) + CDbl(ddr("not_end")))
                        Gwt_with_kpi += Convert.ToInt64(ddr("wait_with_kpi"))
                        Gserve_with_kpi += Convert.ToInt64(ddr("serve_with_kpi"))
                        GTotSct += Convert.ToInt64(ddr("scont"))
                        GTotCct += Convert.ToInt64(ddr("ccont"))
                        Gmax_wt += Convert.ToInt64(ddr("max_wt"))
                        Gmax_ht += Convert.ToInt64(ddr("max_ht"))

                        If ddr("max_wt") > 0 Then
                            Tx += 1
                            Sx += 1
                            Gx += 1
                        End If
                        If ddr("max_ht") > 0 Then
                            Ty += 1
                            Sy += 1
                            Gy += 1
                        End If
                    Next


                    'Sub Total By week
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='center' >" & stDr("week_of_year") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_year") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("staff_name") & "</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Swt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Sserve_with_kpi, "###,##0") & "</td>")
                    If STotCct = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(STotSct / STotCct, "##0")) & "</td>")
                    End If

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

                    ' %Missed Call
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
                    If Sx = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ttAwt = (Format(Smax_wt / Sx, "##0"))
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                    End If

                    '****************** AGV HT ******************
                    Dim ttAht As Integer = 0
                    If Sy = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ttAht = (Format(Smax_ht / Sy, "##0"))
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                    End If

                    '********************************************
                    ret.Append("    </tr>")


                Next
                stDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='6'>Total " & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                If TotCct = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(TotSct / TotCct, "##0")) & "</td>")
                End If

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
                If Tx = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    Dim SAwt As Integer = Format((Tmax_wt / Tx), "##0")
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>")
                End If
                '****************** AGV HT ******************
                If Ty = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    Dim SAht As Integer = Format((Tmax_ht / Ty), "##0")
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>")
                End If
                '********************************************
                ret.Append("    </tr>")
            Next
            shDt.Dispose()

            '******************** Grand Total ************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='6'>Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "###,##0") & "</td>")
            If GTotCct = 0 Then
                ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
            Else
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(GTotSct / GTotCct, "##0")) & "</td>")
            End If
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

            ' % Missed Call
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
            ret = Nothing
        End If
        dt.Dispose()
    End Sub

    Private Sub RenderReportByMonth(ByVal InputPara As CenParaDB.ReportCriteria.AverageServiceTimeWithKPI_byStaffPara)
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
        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffMonthCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year,month_no,service_id", trans.Trans)
        trans.CommitTransaction()
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
            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Service</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Abandon Missed call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Abandon Cancel</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Incomplete</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customers served with AWT Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customers served with AHT Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >AvgConT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AWT to Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AWT over Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AHT to Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Achieve AHT over Target</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >%Missed Call</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Max WT HH:MM:SS</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Max HT HH:MM:SS</td>")
            ret.Append("    </tr>")

            'For Grand Total
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
            Dim GTotSct As Long = 0
            Dim GTotCct As Long = 0

            'Loop By Shop
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th").Copy
            For Each shDr As DataRow In shDt.Rows
                'For Total By Shop
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
                Dim TotSct As Long = 0
                Dim TotCct As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                dt.DefaultView.Sort = "staff_name"
                Dim stDt As New DataTable
                stDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "user_name", "staff_name", "show_year", "show_month").Copy
                For Each stDr As DataRow In stDt.Rows

                    'Loop By Staff
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
                    Dim STotSct As Long = 0
                    Dim STotCct As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and staff_name='" & stDr("staff_name") & "' and show_year ='" & stDr("show_year") & "' and show_month='" & stDr("show_month") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        '********************* File Data *************************
                        ret.Append("    <tr  >")
                        ret.Append("        <td align='left' >&nbsp;" & dr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("show_month") & "</td>")
                        ret.Append("        <td align='center' >&nbsp;" & dr("show_year") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;" & dr("staff_name") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;" & dr("user_code") & "</td>")
                        ret.Append("        <td align='left' >&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("regis"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("served"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("missed_call"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("cancel"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(CDbl(dr("not_call")) + CDbl(dr("not_con")) + CDbl(dr("not_end")), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("wait_with_kpi"), "###,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("serve_with_kpi"), "###,##0") & "</td>")
                        If dr("ccont") = 0 Then
                            ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                        Else
                            ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(dr("scont") / dr("ccont"), "##0")) & "</td>")
                        End If

                        Dim perWt1 As String = "0.00"
                        Dim perWt2 As String = "0.00"
                        Dim perHt1 As String = "0.00"
                        Dim perHt2 As String = "0.00"
                        Dim perMs As String = "0.00"

                        If dr("served") > 0 Then
                            perWt1 = Format((dr("wait_with_kpi") / dr("served")) * 100, "##0.00")
                            perWt2 = Format(100 - (dr("wait_with_kpi") / dr("served")) * 100, "##0.00")
                            perHt1 = Format((dr("serve_with_kpi") / dr("served")) * 100, "##0.00")
                            perHt2 = Format(100 - (dr("serve_with_kpi") / dr("served")) * 100, "##0.00")
                        End If

                        If dr("regis") > 0 Then
                            perMs = Format((dr("missed_call") / dr("regis")) * 100, "##0.00")
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
                        TotIncomplete += Convert.ToInt64(CDbl(dr("not_call")) + CDbl(dr("not_con")) + CDbl(dr("not_end")))
                        Twt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Tserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))
                        TotSct += Convert.ToInt64(dr("scont"))
                        TotCct += Convert.ToInt64(dr("ccont"))
                        Tmax_wt += Convert.ToInt64(dr("max_wt"))
                        Tmax_ht += Convert.ToInt64(dr("max_ht"))

                        STotRegis += Convert.ToInt64(dr("regis"))
                        STotServe += Convert.ToInt64(dr("served"))
                        STotMissedCall += Convert.ToInt64(dr("missed_call"))
                        STotCancelled += Convert.ToInt64(dr("cancel"))
                        STotIncomplete += Convert.ToInt64(CDbl(dr("not_call")) + CDbl(dr("not_con")) + CDbl(dr("not_end")))
                        Swt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Sserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))
                        STotSct += Convert.ToInt64(dr("scont"))
                        STotCct += Convert.ToInt64(dr("ccont"))
                        Smax_wt += Convert.ToInt64(dr("max_wt"))
                        Smax_ht += Convert.ToInt64(dr("max_ht"))

                        GTotRegis += Convert.ToInt64(dr("regis"))
                        GTotServe += Convert.ToInt64(dr("served"))
                        GTotMissedCall += Convert.ToInt64(dr("missed_call"))
                        GTotCancelled += Convert.ToInt64(dr("cancel"))
                        GTotIncomplete += Convert.ToInt64(CDbl(dr("not_call")) + CDbl(dr("not_con")) + CDbl(dr("not_end")))
                        Gwt_with_kpi += Convert.ToInt64(dr("wait_with_kpi"))
                        Gserve_with_kpi += Convert.ToInt64(dr("serve_with_kpi"))
                        GTotSct += Convert.ToInt64(dr("scont"))
                        GTotCct += Convert.ToInt64(dr("ccont"))
                        Gmax_wt += Convert.ToInt64(dr("max_wt"))
                        Gmax_ht += Convert.ToInt64(dr("max_ht"))

                        If dr("max_wt") > 0 Then
                            Tx += 1
                            Sx += 1
                            Gx += 1
                        End If
                        If dr("max_ht") > 0 Then
                            Ty += 1
                            Sy += 1
                            Gy += 1
                        End If
                    Next

                    'Sub Total By Staff
                    ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='center' >" & stDr("show_month") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("show_year") & "</td>")
                    ret.Append("        <td align='center' >" & stDr("staff_name") & "</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='center' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotRegis, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotServe, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotMissedCall, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCancelled, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotIncomplete, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Swt_with_kpi, "###,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(Sserve_with_kpi, "###,##0") & "</td>")
                    If STotCct = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(STotSct / STotCct, "##0")) & "</td>")
                    End If

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

                    ' %Missed Call
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
                    If Sx = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ttAwt = (Format(Smax_wt / Sx, "##0"))
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAwt) & "</td>")
                    End If

                    '****************** AGV HT ******************
                    Dim ttAht As Integer = 0
                    If Sy = 0 Then
                        ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                    Else
                        ttAht = (Format(Smax_ht / Sy, "##0"))
                        ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(ttAht) & "</td>")
                    End If

                    '********************************************
                    ret.Append("    </tr>")
                Next
                stDt.Dispose()

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='6'>Total " & shDr("shop_name_en") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotRegis, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotServe, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotMissedCall, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCancelled, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotIncomplete, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Twt_with_kpi, "###,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(Tserve_with_kpi, "###,##0") & "</td>")

                If TotCct = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(TotSct / TotCct, "##0")) & "</td>")
                End If

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
                If Tx = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    Dim SAwt As Integer = Format((Tmax_wt / Tx), "##0")
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAwt) & "</td>")
                End If
                '****************** AGV HT ******************
                If Ty = 0 Then
                    ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
                Else
                    Dim SAht As Integer = Format((Tmax_ht / Ty), "##0")
                    ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(SAht) & "</td>")
                End If
                '********************************************
                ret.Append("    </tr>")
            Next
            shDt.Dispose()

            '******************** Grand Total ************************
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='6'>Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotRegis, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotServe, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotMissedCall, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCancelled, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotIncomplete, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gwt_with_kpi, "###,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(Gserve_with_kpi, "###,##0") & "</td>")
            If GTotCct = 0 Then
                ret.Append("        <td align='right' >&nbsp;00:00:00</td>")
            Else
                ret.Append("        <td align='right' >&nbsp;" & ReportsENG.GetFormatTimeFromSec(Format(GTotSct / GTotCct, "##0")) & "</td>")
            End If
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

            ' % Missed Call
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
            ret = Nothing
        End If
        dt.Dispose()
    End Sub

End Class
