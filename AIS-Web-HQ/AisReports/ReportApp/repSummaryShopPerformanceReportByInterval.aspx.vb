Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repSummaryShopPerformanceReportByInterval
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "Summary_Shop_Performance_by_Interval_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repSummaryShopPerformanceByInterval.ExportData", Config.GetLoginHistoryID)
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
        If Request("ShopID") IsNot Nothing Then
            Title = Version
            ShowReport(Request("ShopID"))
        End If
    End Sub

    Private Sub ShowReport(ByVal ShopID As String)
        RenderReport(ShopID, Request("DateFrom"), Request("DateTo"), Request("IntervalMinute"), Request("NetworkType"), Request("Segment"), Request("ServiceID"))

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReport(ByVal ShopID As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal IntervalMinute As String, ByVal NetworkType As String, ByVal Segment As String, ByVal ServiceID As String)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepSummaryReportIntervalCenLinqDB

        Dim WhText As String = "shop_id in (" & ShopID & ")"
        WhText += " and convert(varchar(8),service_date,112) >= '" & DateFrom & "'"
        WhText += " and convert(varchar(8),service_date,112) <= '" & DateTo & "'"
        WhText += " and interval_minute='" & IntervalMinute & "'"
        WhText += " and network_type='" & NetworkType & "'"
        WhText += " and segment_type='" & Segment & "'"
        If ServiceID.Trim <> "0" Then
            WhText += "and item_id='" & ServiceID & "'"
        End If

        Dim dt As DataTable = lnq.GetDataList(WhText, "service_date,interval_minute,shop_location_group,item_id,network_type,segment_type,shop_name_en", trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder

            'Dim svDt As New DataTable   'Service ที่ดึงมาทั้งหมด
            'svDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy

            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Region</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Interval Time</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Network Type</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Customer Segment</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Service Type</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Regis</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Serve</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Missed</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Cancelled</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Total Abandon</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>% Abandon</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Waiting with KPI</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Served With KPI</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>% AWT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>% AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AWT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>No. of Counter</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>No. of Staff</td>")
            ret.Append("    </tr>")

            Dim dDt As New DataTable
            dDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "service_date", "show_date", "interval_minute", "shop_location_group", "item_id", "item_name_en", "item_name_th", "network_type", "segment_type")
            dDt.DefaultView.Sort = "shop_location_group,item_id,network_type,segment_type"
            dDt = dDt.DefaultView.ToTable
            If dDt.Rows.Count > 0 Then
                For Each dDr As DataRow In dDt.Rows
                    Dim vLocationGroup As String = dDr("shop_location_group")
                    If dDr("shop_location_group") = "RO" Then
                        vLocationGroup = "UPC"
                    End If

                    Dim wh As String = "interval_minute='" & dDr("interval_minute") & "'"
                    wh += " and show_date='" & dDr("show_date") & "'"
                    wh += " and shop_location_group='" & dDr("shop_location_group") & "'"
                    wh += " and item_id='" & dDr("item_id") & "'"
                    wh += " and network_type = '" & dDr("network_type") & "'"
                    wh += " and segment_type = '" & dDr("segment_type") & "'"
                    wh += " and shop_id='" & dDr("shop_id") & "'"

                    dt.DefaultView.RowFilter = wh
                    If dt.DefaultView.Count > 0 Then
                        dt.DefaultView.Sort = "show_time"
                        Dim vRegis As Long = 0
                        Dim vServe As Long = 0
                        Dim vMissed As Long = 0
                        Dim vCancel As Long = 0
                        Dim vWaitWithKPI As Long = 0
                        Dim vHandleWithKPI As Long = 0
                        Dim vSumWT As Long = 0
                        Dim vCountWT As Long = 0
                        Dim vSumHT As Long = 0
                        Dim vCountHT As Long = 0
                        Dim MaxCounter As Long = 0
                        Dim MaxStaff As Long = 0

                        For Each dr As DataRowView In dt.DefaultView
                            ret.Append("    <tr>")
                            ret.Append("        <td align='left'>&nbsp;" & dDr("shop_name_en") & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & vLocationGroup & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & dDr("show_date") & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & dr("show_time") & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & dDr("network_type") & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & dDr("segment_type") & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & dDr("item_name_en") & "</td>")
                            ret.Append("        <td align='right'>" & dr("regis") & "</td>")
                            ret.Append("        <td align='right'>" & dr("serve") & "</td>")
                            ret.Append("        <td align='right'>" & dr("missed_call") & "</td>")
                            ret.Append("        <td align='right'>" & dr("cancle") & "</td>")

                            Dim totAbandon As Long = Convert.ToInt64(dr("missed_call")) + Convert.ToInt64(dr("cancle"))
                            Dim pAbandon As Long = 0
                            If Convert.ToInt64(dr("regis")) > 0 Then
                                pAbandon = (totAbandon * 100) / Convert.ToInt64(dr("regis"))
                            End If
                            ret.Append("        <td align='right'>" & totAbandon & "</td>")
                            ret.Append("        <td align='right'>" & pAbandon & "</td>")

                            ret.Append("        <td align='right'>" & dr("wait_with_kpi") & "</td>")
                            ret.Append("        <td align='right'>" & dr("serve_with_kpi") & "</td>")

                            Dim pAWT As Long = 0
                            Dim pAHT As Long = 0
                            If Convert.ToInt64(dr("serve")) > 0 Then
                                pAWT = (Convert.ToInt64(dr("wait_with_kpi")) * 100) / Convert.ToInt64(dr("serve"))
                                pAHT = (Convert.ToInt64(dr("serve_with_kpi")) * 100) / Convert.ToInt64(dr("serve"))
                            End If

                            ret.Append("        <td align='right'>" & pAWT & "</td>")
                            ret.Append("        <td align='right'>" & pAHT & "</td>")
                            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("awt")) & "</td>")
                            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("aht")) & "</td>")
                            ret.Append("        <td align='right'>" & dr("no_counter") & "</td>")
                            ret.Append("        <td align='right'>" & dr("no_staff") & "</td>")
                            ret.Append("    </tr>")

                            vRegis += Convert.ToInt64(dr("regis"))
                            vServe += Convert.ToInt64(dr("serve"))
                            vMissed += Convert.ToInt64(dr("missed_call"))
                            vCancel += Convert.ToInt64(dr("cancle"))
                            vWaitWithKPI += Convert.ToInt64(dr("wait_with_kpi"))
                            vHandleWithKPI += Convert.ToInt64(dr("serve_with_kpi"))
                            vSumWT += Convert.ToInt64(dr("sum_wt"))
                            vCountWT += Convert.ToInt64(dr("count_wt"))
                            vSumHT += Convert.ToInt64(dr("sum_ht"))
                            vCountHT += Convert.ToInt64(dr("count_ht"))

                            If Convert.ToInt64(dr("no_counter")) > MaxCounter Then
                                MaxCounter = Convert.ToInt64(dr("no_counter"))
                            End If
                            If Convert.ToInt64(dr("no_staff")) > MaxStaff Then
                                MaxStaff = Convert.ToInt64(dr("no_staff"))
                            End If
                        Next

                        'Sub Total By Segment
                        ret.Append("    <tr style='background: #E4E4E4;'>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center'>&nbsp;" & vLocationGroup & "</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("show_date") & "</td>")
                        ret.Append("        <td align='center'>&nbsp;</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("network_type") & "</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("segment_type") & "</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("item_name_en") & "</td>")
                        ret.Append("        <td align='right'>" & vRegis & "</td>")
                        ret.Append("        <td align='right'>" & vServe & "</td>")
                        ret.Append("        <td align='right'>" & vMissed & "</td>")
                        ret.Append("        <td align='right'>" & vCancel & "</td>")
                        ret.Append("        <td align='right'>" & (vMissed + vCancel) & "</td>")

                        Dim totPAbandon As Long = 0
                        If vServe > 0 Then
                            totPAbandon = ((vMissed + vCancel) * 100) / vServe
                        End If
                        ret.Append("        <td align='right'>" & totPAbandon & "</td>")

                        ret.Append("        <td align='right'>" & vWaitWithKPI & "</td>")
                        ret.Append("        <td align='right'>" & vHandleWithKPI & "</td>")

                        Dim totPAWT As Long = 0
                        Dim totPAHT As Long = 0
                        If vServe > 0 Then
                            totPAWT = (vWaitWithKPI * 100) / vServe
                            totPAHT = (vHandleWithKPI * 100) / vServe
                        End If
                        ret.Append("        <td align='right'>" & totPAWT & "</td>")
                        ret.Append("        <td align='right'>" & totPAHT & "</td>")

                        Dim totAWT As String = "00:00:00"
                        If vCountWT > 0 Then
                            totAWT = ReportsENG.GetFormatTimeFromSec(vSumWT / vCountWT)
                        End If
                        ret.Append("        <td align='center'>" & totAWT & "</td>")

                        Dim totAHT As String = "00:00:00"
                        If vCountHT > 0 Then
                            totAHT = ReportsENG.GetFormatTimeFromSec(vSumHT / vCountHT)
                        End If
                        ret.Append("        <td align='center'>" & totAHT & "</td>")
                        ret.Append("        <td align='right'>" & MaxCounter & "</td>")
                        ret.Append("        <td align='right'>" & MaxStaff & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next
            End If
            dDt.Dispose()

            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
        lnq = Nothing
        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

End Class
