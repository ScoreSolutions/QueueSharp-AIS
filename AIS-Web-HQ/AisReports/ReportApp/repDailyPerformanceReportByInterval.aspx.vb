Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports Engine.Reports

Partial Class ReportApp_repDailyPerformanceReportByInterval
    Inherits System.Web.UI.Page

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "Daily_Interval_Report_by_Network_and_Service_Type_of_Shop_BKK_and_UPC_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repDailyPerformanceReportByInterval.ExportData", Config.GetLoginHistoryID)
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
        SetReportName()
        If Request("ReportName") IsNot Nothing Then
            ShowReport(Request("ReportName"))
        End If
    End Sub

    Private Sub SetReportName()
        Dim NetworkType As String = Request("NetworkType") & ""
        Dim ServiceType As String = Request("ServiceType") & ""
        If NetworkType = "" Or NetworkType = "Null" Then NetworkType = "All"
        If ServiceType = "2" Then ServiceType = " All " Else ServiceType = " by "
        Dim ReportName As String = "Daily Interval Report of " & NetworkType & " Customer " & ServiceType & " Service Type for Shop BKK and UPC"
        lblReportName.Text = ReportName
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        Dim DateFrom As String = Request("DateFrom") & ""
        Dim DateTo As String = Request("DateTo") & ""
        Dim NetworkType As String = Request("NetworkType") & ""

        Dim ServiceType As String = Request("ServiceType") & ""
        Dim ServiceID As String = Request("ServiceID")

        Dim Location As String = Request("Location") & ""
        Dim ShopName As String = Request("Shop") & ""

        Dim Segment As String = Request("Segment") & ""

        Dim IntervalMinute As Double = Convert.ToDouble(Request("IntervalMinute"))
        Dim TimeFrom As String = Request("TimeFrom") & ""
        Dim TimeTo As String = Request("TimeTo") & ""

        Dim StrTime As DateTime = Date.ParseExact(TimeFrom, "HH:mm", Nothing)
        Dim EndTime As DateTime = Date.ParseExact(TimeTo, "HH:mm", Nothing)
        Dim CurrTime As DateTime = StrTime
        Dim InpTime As String = ""
        Do
            If CurrTime < EndTime Then
                Dim tmp As String = "'" & CurrTime.ToString("HH:mm") & "-" & DateAdd(DateInterval.Minute, IntervalMinute, CurrTime).ToString("HH:mm") & "'"
                If InpTime = "" Then
                    InpTime = tmp
                Else
                    InpTime += "," & tmp
                End If
            End If
            CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
        Loop While CurrTime <= EndTime

        Dim WhText As String = " interval_minute = '" & IntervalMinute & "'"
        WhText += " and convert(varchar(8),service_date,112) >= '" & DateFrom & "'"
        WhText += " and convert(varchar(8),service_date,112) <= '" & DateTo & "'"
        WhText += " and show_time in (" & InpTime & ") "
        If NetworkType.ToUpper <> "ALL" Then
            WhText += " and upper(network_type)='" & NetworkType.ToUpper & "'"
        Else
            WhText += " and network_type='All'"
        End If
        If ServiceType = "1" Then   'By Service
            If ServiceID <> "0" Then
                WhText += " and service_id='" & ServiceID & "'"
            Else
                WhText += " and service_id<>0"  'แสดงข้อมูลทุก Service
            End If
        Else
            WhText += " and service_id=0"  'แสดงข้อมูล Service=All
        End If
        If Location.Trim <> "" Then
            WhText += " and region_name = '" & Location & "'"

            If ShopName.Trim <> "" Then
                WhText += " and shop_name_en='" & ShopName & "'"
            End If
        End If

        If Segment.Trim.ToUpper <> "ALL" Then
            WhText += " and upper(customertype_name) = '" & Segment.ToUpper & "'"
        End If

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepIntervalPerformanceTimeCenLinqDB
        Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date, show_time", trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count = 0 Then
            btnExport.Visible = False
        Else
            RenderReportByDate(dt, Split(InpTime, ","))
            btnExport.Visible = True
        End If
    End Sub



    Private Sub RenderReportByDate(ByVal dt As DataTable, ByVal InpTime() As String)
        'Dim ret As String = ""
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder

            ret.Append("<table width=""2500px"" border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            '### Header Colums Start ######
            Dim strHeader As String() = { _
                                       "Network Type", _
                                       "Shop Name", _
                                       "Region", _
                                       "Date", _
                                       "Interval Time", _
                                       "Customer Segment", _
                                       "Service Type", _
                                       "Regis", _
                                       "Serve", _
                                       "Missed Call", _
                                       "Cancelled", _
                                       "Total Abandon", _
                                       "Not Call", _
                                       "Not Confirm", _
                                       "Not End", _
                                       "Waiting Time With KPI", _
                                       "Serve With KPI", _
                                       "% Achive WT to Target", _
                                       "% Achive WT over Target", _
                                       "% Achive HT to Target", _
                                       "% Achive HT over Target", _
                                       "% Abandon", _
                                       "Average Waiting Time", _
                                       "Average Handling Time", _
                                       "Max WT HH:MM:SS", _
                                       "Max HT HH:MM:SS"}
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            For intHeader As Integer = 0 To strHeader.Count - 1
                ret.Append("        <td  align='center' style='color: #ffffff;' >" & strHeader(intHeader) & "</td>")
            Next
            ret.Append("    </tr>")
            '### Header Colums End ######



            Dim dateDt As New DataTable
            dateDt = dt.DefaultView.ToTable(True, "service_date").Copy
            If dateDt.Rows.Count > 0 Then
                'วันที่
                For Each dateDr As DataRow In dateDt.Rows
                    Dim ServiceDate As String = Convert.ToDateTime(dateDr("service_date")).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                    Dim sDt As New DataTable
                    sDt = dt.DefaultView.ToTable(True, "service_name").Copy
                    If sDt.Rows.Count > 0 Then
                        'Loop by Service
                        For Each sDr As DataRow In sDt.Rows
                            Dim nDt As New DataTable
                            nDt = dt.DefaultView.ToTable(True, "network_type").Copy
                            If nDt.Rows.Count > 0 Then
                                'Loop By Network Type
                                For Each nDr As DataRow In nDt.Rows
                                    Dim rDt As New DataTable
                                    rDt = dt.DefaultView.ToTable(True, "region_name")
                                    If rDt.Rows.Count > 0 Then
                                        For Each rDr As DataRow In rDt.Rows
                                            Dim shDt As New DataTable
                                            shDt = dt.DefaultView.ToTable(True, "region_name", "shop_name_en").Copy
                                            'shDt.DefaultView.RowFilter = "region_name='" & rDr("region_name") & "'"
                                            shDt.DefaultView.Sort = "shop_name_en"
                                            If shDt.DefaultView.Count > 0 Then
                                                For Each shDr As DataRow In shDt.Rows
                                                    If shDr("region_name") <> rDr("region_name") Then
                                                        Continue For
                                                    End If
                                                    Dim segDt As New DataTable
                                                    segDt = dt.DefaultView.ToTable(True, "customertype_name").Copy
                                                    If segDt.Rows.Count > 0 Then
                                                        'For Grand Total by Segment
                                                        Dim GRegis As Int16 = 0
                                                        Dim GServe As Int16 = 0
                                                        Dim GMissCall As Int16 = 0
                                                        Dim GCancel As Int16 = 0
                                                        Dim GNotCall As Int16 = 0
                                                        Dim GNotCon As Int16 = 0
                                                        Dim GNotEnd As Int16 = 0
                                                        Dim GWaitWithKPI As Integer = 0
                                                        Dim GServeWithKPI As Integer = 0

                                                        Dim GWt As Long = 0
                                                        Dim GCountWt As Integer = 0
                                                        Dim GHt As Long = 0
                                                        Dim GCountHt As Integer = 0
                                                        Dim GMaxWt As Integer = 0
                                                        Dim GMaxHt As Integer = 0

                                                        Dim TotStartTime As String = ""
                                                        Dim TotEndTime As String = ""

                                                        For Each segDr As DataRow In segDt.Rows



                                                            'For Total Row
                                                            Dim TotRegis As Int16 = 0
                                                            Dim TotServe As Int16 = 0
                                                            Dim TotMissCall As Int16 = 0
                                                            Dim TotCancel As Int16 = 0
                                                            Dim TotNotCall As Int16 = 0
                                                            Dim TotNotCon As Int16 = 0
                                                            Dim TotNotEnd As Int16 = 0
                                                            Dim TotWaitWithKPI As Integer = 0
                                                            Dim TotServeWithKPI As Integer = 0

                                                            Dim TotWt As Long = 0
                                                            Dim TotCountWt As Integer = 0
                                                            Dim TotHt As Long = 0
                                                            Dim TotCountHt As Integer = 0
                                                            Dim TotMaxWt As Integer = 0
                                                            Dim TotMaxHt As Integer = 0

                                                            For Each tm As String In InpTime
                                                                Dim fRow As String = "network_type='" & nDr("network_type") & "'"
                                                                fRow += " and shop_name_en = '" & shDr("shop_name_en") & "'"
                                                                fRow += " and region_name = '" & rDr("region_name") & "'"
                                                                fRow += " and service_date = '" & dateDr("service_date") & "'"
                                                                fRow += " and customertype_name='" & segDr("customertype_name") & "'"
                                                                fRow += " and service_name = '" & sDr("service_name") & "'"
                                                                fRow += " and show_time = " & tm & ""
                                                                dt.DefaultView.RowFilter = fRow

                                                                If TotStartTime = "" Then
                                                                    TotStartTime = Replace(Split(tm, "-")(0), "'", "")
                                                                End If
                                                                TotEndTime = Replace(Split(tm, "-")(1), "'", "")

                                                                If dt.DefaultView.Count > 0 Then
                                                                    For Each drv As DataRowView In dt.DefaultView

                                                                        Dim _totalabandon As Integer = CInt(drv("missed_call")) + CInt(drv("Cancel"))
                                                                        Dim _percent_abandon As Integer = (_totalabandon / CInt(drv("Regis"))) * 100

                                                                        ret.Append("    <tr>")
                                                                        ret.Append("        <td align='left' >" & nDr("network_type") & "</td>")
                                                                        ret.Append("        <td align='left' >" & shDr("shop_name_en") & "</td>")
                                                                        ret.Append("        <td align='center' >" & rDr("region_name") & "</td>")
                                                                        ret.Append("        <td align='right' >" & ServiceDate & "</td>")
                                                                        ret.Append("        <td align='left' >" & tm.Replace("'", "") & "</td>")
                                                                        ret.Append("        <td align='left' >" & segDr("customertype_name") & "</td>")
                                                                        ret.Append("        <td align='left' >" & sDr("service_name") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("Regis"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("served"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("missed_call"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("Cancel"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & _totalabandon & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("not_call"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("not_con"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("not_end"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("wait_with_kpi"), "#,##0") & "</td>")
                                                                        ret.Append("        <td align='right' >" & Format(drv("serve_with_kpi"), "#,##0") & "</td>")

                                                                        Dim perWt1 As String = "0.00"
                                                                        Dim perWt2 As String = "0.00"
                                                                        Dim perHt1 As String = "0.00"
                                                                        Dim perHt2 As String = "0.00"

                                                                        If drv("served") > 0 Then
                                                                            perWt1 = Format((drv("wait_with_kpi") / drv("served")) * 100, "##0.00")
                                                                            perWt2 = Format(100 - (drv("wait_with_kpi") / drv("served")) * 100, "##0.00")
                                                                            perHt1 = Format((drv("serve_with_kpi") / drv("served")) * 100, "##0.00")
                                                                            perHt2 = Format(100 - (drv("serve_with_kpi") / drv("served")) * 100, "##0.00")
                                                                        End If

                                                                        Dim perAbd As String = "0.00"
                                                                        If drv("regis") > 0 Then
                                                                            perAbd = Format((_totalabandon / drv("regis")) * 100, "##0.00")
                                                                        End If

                                                                        ret.Append("        <td align='right' >" & perWt1 & "%</td>")
                                                                        ret.Append("        <td align='right' >" & perWt2 & "%</td>")
                                                                        ret.Append("        <td align='right' >" & perHt1 & "%</td>")
                                                                        ret.Append("        <td align='right' >" & perHt2 & "%</td>")
                                                                        ret.Append("        <td align='right' >" & perAbd & "%</td>")

                                                                        Dim cal_wt As Double = (Val(drv("sum_wt")) / IIf(Val(drv("served")) = 0.0, 1, Val(drv("served"))))
                                                                        Dim AVG_WT As String = Engine.Reports.ReportsENG.GetFormatTimeFromSec(cal_wt)
                                                                        If AVG_WT <> "00:00:00" Then
                                                                            ret.Append("        <td align='center' >" & AVG_WT & "</td>")
                                                                        Else
                                                                            ret.Append("        <td align='center' >00:00:00</td>")
                                                                        End If

                                                                        Dim cal_ht As Integer = (Val(drv("sum_ht")) / IIf(Val(drv("served")) = 0.0, 1, Val(drv("served"))))
                                                                        Dim AVG_HT As String = Engine.Reports.ReportsENG.GetFormatTimeFromSec(cal_ht)
                                                                        If AVG_HT <> "00:00:00" Then
                                                                            ret.Append("        <td align='center' >" & AVG_HT & "</td>")
                                                                        Else
                                                                            ret.Append("        <td align='center' >00:00:00</td>")
                                                                        End If
                                                                        If Convert.ToInt64(drv("MAX_WT")) <> 0 Then
                                                                            ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(drv("MAX_WT")) & "</td>")
                                                                        Else
                                                                            ret.Append("        <td align='center' >00:00:00</td>")
                                                                        End If
                                                                        If Convert.ToInt64(drv("MAX_HT")) <> 0 Then
                                                                            ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(drv("MAX_HT")) & "</td>")
                                                                        Else
                                                                            ret.Append("        <td align='center' >00:00:00</td>")
                                                                        End If
                                                                        ret.Append("    </tr>")

                                                                      
                                                                        'For Total Row
                                                                        TotRegis += Convert.ToInt16(drv("regis"))
                                                                        TotServe += Convert.ToInt16(drv("served"))
                                                                        TotMissCall += Convert.ToInt16(drv("missed_call"))
                                                                        TotCancel += Convert.ToInt16(drv("cancel"))
                                                                        TotNotCall += Convert.ToInt16(drv("not_call"))
                                                                        TotNotCon += Convert.ToInt16(drv("not_con"))
                                                                        TotNotEnd += Convert.ToInt16(drv("not_end"))
                                                                        TotWaitWithKPI += Convert.ToInt32(drv("wait_with_kpi"))
                                                                        TotServeWithKPI += Convert.ToInt32(drv("serve_with_kpi"))
                                                                        TotWt += Convert.ToInt64(drv("sum_wt"))
                                                                        TotCountWt += Convert.ToInt16(drv("count_wt"))
                                                                        TotHt += Convert.ToInt64(drv("sum_ht"))
                                                                        TotCountHt += Convert.ToInt64(drv("count_ht"))
                                                                        If TotMaxWt < Convert.ToInt64(drv("max_wt")) Then
                                                                            TotMaxWt = Convert.ToInt64(drv("max_wt"))
                                                                        End If
                                                                        If TotMaxHt < Convert.ToInt64(drv("max_ht")) Then
                                                                            TotMaxHt = Convert.ToInt64(drv("max_ht"))
                                                                        End If


                                                                        'For Grand Total Row
                                                                        GRegis += Convert.ToInt16(drv("regis"))
                                                                        GServe += Convert.ToInt16(drv("served"))
                                                                        GMissCall += Convert.ToInt16(drv("missed_call"))
                                                                        GCancel += Convert.ToInt16(drv("cancel"))
                                                                        GNotCall += Convert.ToInt16(drv("not_call"))
                                                                        GNotCon += Convert.ToInt16(drv("not_con"))
                                                                        GNotEnd += Convert.ToInt16(drv("not_end"))
                                                                        GWaitWithKPI += Convert.ToInt32(drv("wait_with_kpi"))
                                                                        GServeWithKPI += Convert.ToInt32(drv("serve_with_kpi"))
                                                                        GWt += Convert.ToInt64(drv("sum_wt"))
                                                                        GCountWt += Convert.ToInt16(drv("count_wt"))
                                                                        GHt += Convert.ToInt64(drv("sum_ht"))
                                                                        GCountHt += Convert.ToInt64(drv("count_ht"))
                                                                        If GMaxWt < Convert.ToInt64(drv("max_wt")) Then
                                                                            GMaxWt = Convert.ToInt64(drv("max_wt"))
                                                                        End If
                                                                        If GMaxHt < Convert.ToInt64(drv("max_ht")) Then
                                                                            GMaxHt = Convert.ToInt64(drv("max_ht"))
                                                                        End If
                                                                    Next
                                                                Else
                                                                    'ถ้าม่มีข้อมูลให้แสดงแถวว่าง
                                                                    ret.Append("    <tr>")
                                                                    ret.Append("        <td align='left' >" & nDr("network_type") & "</td>")
                                                                    ret.Append("        <td align='left' >" & shDr("shop_name_en") & "</td>")
                                                                    ret.Append("        <td align='center' >" & rDr("region_name") & "</td>")
                                                                    ret.Append("        <td align='right' >" & ServiceDate & "</td>")
                                                                    ret.Append("        <td align='left' >" & tm.Replace("'", "") & "</td>")
                                                                    ret.Append("        <td align='left' >" & segDr("customertype_name") & "</td>")
                                                                    ret.Append("        <td align='left' >" & sDr("service_name") & "</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")
                                                                    ret.Append("        <td align='right' >0</td>")

                                                                    ret.Append("        <td align='right' >0.00%</td>")
                                                                    ret.Append("        <td align='right' >0.00%</td>")
                                                                    ret.Append("        <td align='right' >0.00%</td>")
                                                                    ret.Append("        <td align='right' >0.00%</td>")
                                                                    ret.Append("        <td align='right' >0.00%</td>")

                                                                    ret.Append("        <td align='center' >00:00:00</td>")
                                                                    ret.Append("        <td align='center' >00:00:00</td>")
                                                                    ret.Append("        <td align='center' >00:00:00</td>")
                                                                    ret.Append("        <td align='center' >00:00:00</td>")
                                                                    ret.Append("    </tr>")
                                                                End If
                                                                dt.DefaultView.RowFilter = ""
                                                            Next

                                                            'Total Row
                                                            ret.Append("    <tr style='background: #E4E4E4;'>")
                                                            ret.Append("        <td align='left' >" & nDr("network_type") & "</td>")
                                                            ret.Append("        <td align='left' > Total</td>")
                                                            ret.Append("        <td align='center' >" & rDr("region_name") & "</td>")
                                                            ret.Append("        <td align='right' >" & ServiceDate & "</td>")
                                                            ret.Append("        <td align='left' >" & TotStartTime & "-" & TotEndTime & "</td>")
                                                            ret.Append("        <td align='left' >Total " & segDr("customertype_name") & "</td>")
                                                            ret.Append("        <td align='left' >" & sDr("service_name") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotRegis, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotServe, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotMissCall, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotCancel, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format((TotMissCall + TotCancel), "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotNotCall, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotNotCon, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotNotEnd, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotWaitWithKPI, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(TotServeWithKPI, "#,##0") & "</td>")
                                                            ret.Append("        <td align='right' >" & Format(IIf(TotServe <> 0, (TotWaitWithKPI / TotServe) * 100, 0), "##0.00") & "%</td>")
                                                            ret.Append("        <td align='right' >" & Format(100 - IIf(TotServe <> 0, (TotServeWithKPI / TotServe) * 100, 0), "##0.00") & "%</td>")
                                                            ret.Append("        <td align='right' >" & Format(IIf(TotServe <> 0, (TotServeWithKPI / TotServe) * 100, 0), "##0.00") & "%</td>")
                                                            ret.Append("        <td align='right' >" & Format(100 - IIf(TotServe <> 0, (TotServeWithKPI / TotServe) * 100, 0), "##0.00") & "%</td>")
                                                            ret.Append("        <td align='right' >" & Format(IIf(TotRegis <> 0, ((TotMissCall + TotCancel) / TotRegis), 0), "##0.00") & "%</td>")

                                                            If TotCountWt <> 0 Then
                                                                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TotWt / TotCountWt) & "</td>")
                                                            Else
                                                                ret.Append("        <td align='center' >00:00:00</td>")
                                                            End If
                                                            If TotCountHt <> 0 Then
                                                                ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TotHt / TotCountHt) & "</td>")
                                                            Else
                                                                ret.Append("        <td align='center' >00:00:00</td>")
                                                            End If
                                                            ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TotMaxWt) & "</td>")
                                                            ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(TotMaxHt) & "</td>")
                                                            ret.Append("    </tr>")
                                                        Next


                                                        'Grand Total Row
                                                        ret.Append("    <tr style='background: #E4E4E4;'>")
                                                        ret.Append("        <td align='left' >" & nDr("network_type") & "</td>")
                                                        ret.Append("        <td align='left' >Grand Total</td>")
                                                        ret.Append("        <td align='center' >" & rDr("region_name") & "</td>")
                                                        ret.Append("        <td align='right' >" & ServiceDate & "</td>")
                                                        ret.Append("        <td align='left' >" & TotStartTime & "-" & TotEndTime & "</td>")
                                                        ret.Append("        <td align='left' >Total All Segment</td>")
                                                        ret.Append("        <td align='left' >" & sDr("service_name") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GRegis, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GServe, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GMissCall, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GCancel, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format((GMissCall + GCancel), "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GNotCall, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GNotCon, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GNotEnd, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GWaitWithKPI, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(GServeWithKPI, "#,##0") & "</td>")
                                                        ret.Append("        <td align='right' >" & Format(IIf(GServe <> 0, (GWaitWithKPI / GServe) * 100, 0), "##0.00") & "%</td>")
                                                        ret.Append("        <td align='right' >" & Format(100 - IIf(GServe <> 0, (GServeWithKPI / GServe) * 100, 0), "##0.00") & "%</td>")
                                                        ret.Append("        <td align='right' >" & Format(IIf(GServe <> 0, (GServeWithKPI / GServe) * 100, 0), "##0.00") & "%</td>")
                                                        ret.Append("        <td align='right' >" & Format(100 - IIf(GServe <> 0, (GServeWithKPI / GServe) * 100, 0), "##0.00") & "%</td>")
                                                        ret.Append("        <td align='right' >" & Format(IIf(GRegis <> 0, ((GMissCall + GCancel) / GRegis), 0), "##0.00") & "%</td>")

                                                        If GCountWt <> 0 Then
                                                            ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GWt / GCountWt) & "</td>")
                                                        Else
                                                            ret.Append("        <td align='center' >00:00:00</td>")
                                                        End If
                                                        If GCountHt <> 0 Then
                                                            ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GHt / GCountHt) & "</td>")
                                                        Else
                                                            ret.Append("        <td align='center' >00:00:00</td>")
                                                        End If
                                                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GMaxWt) & "</td>")
                                                        ret.Append("        <td align='center' >" & ReportsENG.GetFormatTimeFromSec(GMaxHt) & "</td>")
                                                        ret.Append("    </tr>")
                                                        shDt.Dispose()
                                                    End If
                                                    segDt.Dispose()
                                                Next
                                            End If
                                            shDt.Dispose()
                                        Next
                                    End If
                                    rDt.Dispose()
                                Next
                            End If
                            nDt.Dispose()
                        Next
                    End If
                    sDt.Dispose()
                Next
            End If
            dateDt.Dispose()












            'End
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

End Class

