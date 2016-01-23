Imports System.IO
Imports System.Data
Imports Engine.Reports

Partial Class ReportApp_repSummaryShopPerformanceReportByStaff
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
        RenderReport(ShopID, Request("DateFrom"), Request("UserCode"), Request("NetworkType"), Request("Segment"), Request("ServiceID"))

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReport(ByVal ShopID As String, ByVal vDate As String, ByVal UserCode As String, ByVal NetworkType As String, ByVal Segment As String, ByVal ServiceID As String)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbRepSummaryReportStaffCenLinqDB

        Dim WhText As String = "shop_id = '" & ShopID & "'"
        WhText += " and convert(varchar(8),service_date,112) between '" & vDate & "' and '" & vDate & "'"
        WhText += " and network_type='" & NetworkType & "'"
        WhText += " and segment_type='" & Segment & "'"
        If UserCode.Trim <> "0" Then
            WhText += " and emp_id='" & UserCode & "'"
        End If
        If ServiceID.Trim <> "0" Then
            WhText += "and item_id='" & ServiceID & "'"
        End If

        Dim dt As DataTable = lnq.GetDataList(WhText, "service_date,emp_id,staff_name,item_id,network_type,segment_type,shop_name_en", trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
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
            ret.Append("        <td align='center' style='color: #ffffff;'>% AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>AHT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Max HT</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Productivity</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>% Productivity</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Non-Productivity</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>% Non-Productivity</td>")
            ret.Append("        <td align='center' style='color: #ffffff;'>Total Time (Login - Logout)</td>")
            ret.Append("    </tr>")

            Dim dDt As New DataTable
            dDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "service_date", "show_date", "shop_location_group", "item_id", "item_name_en", "item_name_th", "network_type", "segment_type")
            dDt.DefaultView.Sort = "shop_location_group,shop_name_en,network_type,segment_type,item_id"
            dDt = dDt.DefaultView.ToTable
            If dDt.Rows.Count > 0 Then
                For Each dDr As DataRow In dDt.Rows
                    Dim wh As String = "show_date='" & dDr("show_date") & "'"
                    wh += " and shop_location_group='" & dDr("shop_location_group") & "'"
                    wh += " and item_id='" & dDr("item_id") & "'"
                    wh += " and network_type = '" & dDr("network_type") & "'"
                    wh += " and segment_type = '" & dDr("segment_type") & "'"
                    wh += " and shop_id='" & dDr("shop_id") & "'"

                    dt.DefaultView.RowFilter = wh
                    If dt.DefaultView.Count > 0 Then
                        dt.DefaultView.Sort = "staff_name"
                        Dim vRegis As Long = 0
                        Dim vServe As Long = 0
                        Dim vMissed As Long = 0
                        Dim vCancel As Long = 0
                        Dim vWaitWithKPI As Long = 0
                        Dim vHandleWithKPI As Long = 0
                        Dim vSumHT As Long = 0
                        Dim vCountHT As Long = 0
                        Dim MaxHT As Long = 0
                        Dim vProduct As Long = 0
                        Dim vNonProduct As Long = 0
                        Dim vTotalTime As Long = 0

                        For Each dr As DataRowView In dt.DefaultView
                            ret.Append("    <tr>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("staff_name") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dr("emp_id") & "</td>")
                            ret.Append("        <td align='left'>&nbsp;" & dDr("shop_name_en") & "</td>")
                            ret.Append("        <td align='center'>&nbsp;" & dDr("show_date") & "</td>")
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

                            Dim pAHT As Long = 0
                            If Convert.ToInt64(dr("serve")) > 0 Then
                                pAHT = (Convert.ToInt64(dr("serve_with_kpi")) * 100) / Convert.ToInt64(dr("serve"))
                            End If
                            ret.Append("        <td align='right'>" & pAHT & "</td>")
                            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("aht")) & "</td>")
                            ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("max_ht")) & "</td>")

                            Dim pProductivity As Integer = 0
                            Dim pNonProductivity As Integer = 0
                            Dim vTotTime As Long = ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                            If vTotTime > 0 Then
                                pProductivity = (ReportsENG.GetSecFromTimeFormat(dr("productivity")) * 100) / vTotTime
                                pNonProductivity = (ReportsENG.GetSecFromTimeFormat(dr("non_productivity")) * 100) / vTotTime
                            End If
                            ret.Append("        <td align='right'>" & dr("productivity") & "</td>")
                            ret.Append("        <td align='right'>" & pProductivity & "</td>")
                            ret.Append("        <td align='right'>" & dr("non_productivity") & "</td>")
                            ret.Append("        <td align='right'>" & pNonProductivity & "</td>")
                            ret.Append("        <td align='right'>" & dr("total_time") & "</td>")
                            ret.Append("    </tr>")

                            vRegis += Convert.ToInt64(dr("regis"))
                            vServe += Convert.ToInt64(dr("serve"))
                            vMissed += Convert.ToInt64(dr("missed_call"))
                            vCancel += Convert.ToInt64(dr("cancle"))
                            vWaitWithKPI += Convert.ToInt64(dr("wait_with_kpi"))
                            vHandleWithKPI += Convert.ToInt64(dr("serve_with_kpi"))
                            vSumHT += Convert.ToInt64(dr("sum_ht"))
                            vCountHT += Convert.ToInt64(dr("count_ht"))
                            If Convert.ToInt64(dr("max_ht")) > MaxHT Then
                                MaxHT = Convert.ToInt64(dr("max_ht"))
                            End If
                            vProduct += ReportsENG.GetSecFromTimeFormat(dr("productivity"))
                            vNonProduct += ReportsENG.GetSecFromTimeFormat(dr("non_productivity"))
                            vTotalTime += ReportsENG.GetSecFromTimeFormat(dr("total_time"))
                        Next

                        'Sub Total By Segment
                        ret.Append("    <tr style='background: #E4E4E4;'>")
                        ret.Append("        <td align='center'>&nbsp;Total</td>")
                        ret.Append("        <td align='center'>&nbsp;</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center'>&nbsp;" & dDr("show_date") & "</td>")
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

                        Dim totPAHT As Long = 0
                        If vServe > 0 Then
                            totPAHT = (vHandleWithKPI * 100) / vServe
                        End If
                        ret.Append("        <td align='right'>" & totPAHT & "</td>")

                        Dim totAHT As String = "00:00:00"
                        If vCountHT > 0 Then
                            totAHT = ReportsENG.GetFormatTimeFromSec(vSumHT / vCountHT)
                        End If
                        ret.Append("        <td align='center'>" & totAHT & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(MaxHT) & "</td>")

                        Dim SumPProduct As Long = 0
                        Dim SumPNonProduct As Long = 0
                        If vTotalTime > 0 Then
                            SumPProduct = (vProduct * 100) / vTotalTime
                            SumPNonProduct = (vNonProduct * 100) / vTotalTime
                        End If
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(vProduct) & "</td>")
                        ret.Append("        <td align='right'>" & SumPProduct & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(vNonProduct) & "</td>")
                        ret.Append("        <td align='right'>" & SumPNonProduct & "</td>")
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(vTotalTime) & "</td>")
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
