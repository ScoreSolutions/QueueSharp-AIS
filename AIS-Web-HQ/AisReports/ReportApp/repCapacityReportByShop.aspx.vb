Imports System.IO
Imports System.Data

Partial Class ReportApp_repCapacityReportByShop
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "CapacityReportByShop_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repCapacityReportByShop.ExportData", Config.GetLoginHistoryID)
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
        If ReportName = "ByTime" Then
            Dim para As New CenParaDB.ReportCriteria.CapacityReportByShopPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            para.IntervalMinute = Request("Interval")
            para.TimePeroidFrom = Request("TimeFrom")
            para.TimePeroidTo = Request("TimeTo")
            Dim eng As New Engine.Reports.RepCapacityByShopENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByTime(para)
            If dt.Rows.Count > 0 Then
                RenderReportByTime(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
            lblHeader.Text = "by Shop by Time"
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.CapacityReportByShopPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepCapacityByShopENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByWeek(para)
            If dt.Rows.Count > 0 Then
                RenderReportByWeek(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
            lblHeader.Text = "by Shop by Week"
        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.CapacityReportByShopPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepCapacityByShopENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByDate(para)
            If dt.Rows.Count > 0 Then
                RenderReportByDate(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
            lblHeader.Text = "by Shop by Date"
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.CapacityReportByShopPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepCapacityByShopENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByMonth(para)
            If dt.Rows.Count > 0 Then
                RenderReportByMonth(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
            lblHeader.Text = "by Shop by Month"
        End If
        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If

    End Sub


    Private Sub RenderReportByTime(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Time</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Service Type</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Working Hour<br />(Hr/Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >KPI<br />(Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >No. of Counter</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Capacity<br />(Transactions)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Appointment</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Walk in</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Total To be<br />Served</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Capacity Used<br />(%)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected Open<br />Counter</td>")
            ret.Append("    </tr>")

            Dim GTotWh As Long = 0
            'Dim GTotKPI As Long = 0
            Dim GTotCounter As Long = 0
            Dim GTotCapacity As Long = 0
            Dim GTotAppointment As Long = 0
            Dim GTotExpt As Long = 0
            Dim GTotTotal As Long = 0
            Dim GTotExptOpenCounter As Long = 0

            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim ShopID As Long = sDr("shop_id")
                Dim TotWh As Long = 0
                'Dim TotKPI As Long = 0
                Dim TotCounter As Long = 0
                Dim TotCapacity As Long = 0
                Dim TotAppointment As Long = 0
                Dim TotExpt As Long = 0
                Dim TotTotal As Long = 0
                Dim TotExptOpenCounter As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & ShopID & "'"
                Dim dDt As New DataTable
                dDt = dt.DefaultView.ToTable(True, "show_date", "show_time")
                For Each dDr As DataRow In dDt.Rows
                    Dim STotWh As Long = 0
                    'Dim STotKPI As Long = 0
                    Dim STotCounter As Long = 0
                    Dim STotCapacity As Long = 0
                    Dim STotAppointment As Long = 0
                    Dim STotExpt As Long = 0
                    Dim STotTotal As Long = 0
                    Dim STotExptOpenCounter As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & ShopID & "' and show_date='" & dDr("show_date") & "' and show_time='" & dDr("show_time") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >" & dDr("show_date") & "</td>")
                        ret.Append("        <td align='center' >" & dDr("show_time") & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & dr("working_hour") & "</td>")
                        ret.Append("        <td align='right' >" & dr("kpi") & "</td>")
                        ret.Append("        <td align='right' >" & dr("no_counter") & "</td>")
                        ret.Append("        <td align='right' >" & dr("capacity_trans") & "</td>")
                        ret.Append("        <td align='right' >" & dr("appointment") & "</td>")
                        ret.Append("        <td align='right' >" & dr("expected_walk_in") & "</td>")
                        ret.Append("        <td align='right' >" & dr("total_to_be_serve") & "</td>")
                        ret.Append("        <td align='right' >" & dr("expected_capacity_used") & "%</td>")
                        ret.Append("        <td align='right' >" & dr("expected_open_counter") & "</td>")
                        ret.Append("    </tr>")

                        STotWh += dr("working_hour")
                        'STotKPI += dr("kpi")
                        STotCounter += dr("no_counter")
                        STotCapacity += dr("capacity_trans")
                        STotAppointment += dr("appointment")
                        STotExpt += dr("expected_walk_in")
                        STotTotal += dr("total_to_be_serve")
                        STotExptOpenCounter += dr("expected_open_counter")

                        TotWh += dr("working_hour")
                        'TotKPI += dr("kpi")
                        TotCounter += dr("no_counter")
                        TotCapacity += dr("capacity_trans")
                        TotAppointment += dr("appointment")
                        TotExpt += dr("expected_walk_in")
                        TotTotal += dr("total_to_be_serve")
                        TotExptOpenCounter += dr("expected_open_counter")

                        GTotWh += dr("working_hour")
                        'GTotKPI += dr("kpi")
                        GTotCounter += dr("no_counter")
                        GTotCapacity += dr("capacity_trans")
                        GTotAppointment += dr("appointment")
                        GTotExpt += dr("expected_walk_in")
                        GTotTotal += dr("total_to_be_serve")
                        GTotExptOpenCounter += dr("expected_open_counter")
                    Next

                    'Sub Total By Interval Time
                    ret.Append("    <tr style='background: #E4E4E4' >")
                    ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='Center' >" & dDr("show_date") & "</td>")
                    ret.Append("        <td align='Center' >" & dDr("show_time") & "</td>")
                    ret.Append("        <td align='Center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotWh, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotCounter, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCapacity, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotAppointment, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotExpt, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotTotal, "#,##0") & "</td>")
                    If STotCapacity = 0 Then
                        ret.Append("        <td align='right' >100%</td>")
                    Else
                        ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((STotTotal + STotAppointment) * 100) / STotCapacity), "#,##0") & "%</td>")
                    End If
                    ret.Append("        <td align='right' >" & Format(STotExptOpenCounter, "#,##0") & "</td>")
                    ret.Append("    </tr>")
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td colspan='3' align='Center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotWh, "#,##0") & "</td>")
                ret.Append("        <td align='right' >&nbsp;</td>")
                ret.Append("        <td align='right' >" & Format(TotCounter, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCapacity, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotAppointment, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotExpt, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotTotal, "#,##0") & "</td>")
                If TotCapacity = 0 Then
                    ret.Append("        <td align='right' >100%</td>")
                Else
                    ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((TotTotal + TotAppointment) * 100) / TotCapacity), "#,##0") & "%</td>")
                End If
                ret.Append("        <td align='right' >" & Format(TotExptOpenCounter, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='Center' >&nbsp;</td>")
            ret.Append("        <td colspan='3' align='Center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotWh, "#,##0") & "</td>")
            ret.Append("        <td align='right' >&nbsp;</td>")
            ret.Append("        <td align='right' >" & Format(GTotCounter, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCapacity, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotAppointment, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotExpt, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotTotal, "#,##0") & "</td>")
            If GTotCapacity = 0 Then
                ret.Append("        <td align='right' >100%</td>")
            Else
                ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((GTotTotal + GTotAppointment) * 100) / GTotCapacity), "#,##0") & "%</td>")
            End If
            ret.Append("        <td align='right' >" & Format(GTotExptOpenCounter, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Service Type</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Working Hour<br />(Hr/Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >KPI<br />(Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >No. of Counter</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Capacity<br />(Transactions)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Appointment</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Walk in</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Total To be<br />Served</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Capacity Used<br />(%)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected Open<br />Counter</td>")
            ret.Append("    </tr>")

            Dim GTotWh As Long = 0
            'Dim GTotKPI As Long = 0
            Dim GTotCounter As Long = 0
            Dim GTotCapacity As Long = 0
            Dim GTotAppointment As Long = 0
            Dim GTotExpt As Long = 0
            Dim GTotTotal As Long = 0
            Dim GTotExptOpenCounter As Long = 0

            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim ShopID As Long = sDr("shop_id")
                Dim TotWh As Long = 0
                'Dim TotKPI As Long = 0
                Dim TotCounter As Long = 0
                Dim TotCapacity As Long = 0
                Dim TotAppointment As Long = 0
                Dim TotExpt As Long = 0
                Dim TotTotal As Long = 0
                Dim TotExptOpenCounter As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & ShopID & "'"
                Dim dDt As New DataTable
                dDt = dt.DefaultView.ToTable(True, "show_date")
                For Each dDr As DataRow In dDt.Rows
                    Dim STotWh As Long = 0
                    'Dim STotKPI As Long = 0
                    Dim STotCounter As Long = 0
                    Dim STotCapacity As Long = 0
                    Dim STotAppointment As Long = 0
                    Dim STotExpt As Long = 0
                    Dim STotTotal As Long = 0
                    Dim STotExptOpenCounter As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & ShopID & "' and show_date='" & dDr("show_date") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >" & dDr("show_date") & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & dr("working_hour") & "</td>")
                        ret.Append("        <td align='right' >" & dr("kpi") & "</td>")
                        ret.Append("        <td align='right' >" & dr("no_counter") & "</td>")
                        ret.Append("        <td align='right' >" & dr("capacity_trans") & "</td>")
                        ret.Append("        <td align='right' >" & dr("appointment") & "</td>")
                        ret.Append("        <td align='right' >" & dr("expected_walk_in") & "</td>")
                        ret.Append("        <td align='right' >" & dr("total_to_be_serve") & "</td>")
                        ret.Append("        <td align='right' >" & dr("expected_capacity_used") & "%</td>")
                        ret.Append("        <td align='right' >" & dr("expected_open_counter") & "</td>")
                        ret.Append("    </tr>")

                        STotWh += dr("working_hour")
                        'STotKPI += dr("kpi")
                        STotCounter += dr("no_counter")
                        STotCapacity += dr("capacity_trans")
                        STotAppointment += dr("appointment")
                        STotExpt += dr("expected_walk_in")
                        STotTotal += dr("total_to_be_serve")
                        STotExptOpenCounter += dr("expected_open_counter")

                        TotWh += dr("working_hour")
                        'TotKPI += dr("kpi")
                        TotCounter += dr("no_counter")
                        TotCapacity += dr("capacity_trans")
                        TotAppointment += dr("appointment")
                        TotExpt += dr("expected_walk_in")
                        TotTotal += dr("total_to_be_serve")
                        TotExptOpenCounter += dr("expected_open_counter")

                        GTotWh += dr("working_hour")
                        'GTotKPI += dr("kpi")
                        GTotCounter += dr("no_counter")
                        GTotCapacity += dr("capacity_trans")
                        GTotAppointment += dr("appointment")
                        GTotExpt += dr("expected_walk_in")
                        GTotTotal += dr("total_to_be_serve")
                        GTotExptOpenCounter += dr("expected_open_counter")
                    Next

                    'Sub Total By Date
                    ret.Append("    <tr style='background: #E4E4E4' >")
                    ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='Center' >" & dDr("show_date") & "</td>")
                    ret.Append("        <td align='Center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotWh, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotCounter, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCapacity, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotAppointment, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotExpt, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotTotal, "#,##0") & "</td>")
                    If STotCapacity = 0 Then
                        ret.Append("        <td align='right' >100%</td>")
                    Else
                        ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((TotTotal + STotAppointment) * 100) / STotCapacity), "#,##0") & "%</td>")
                    End If

                    ret.Append("        <td align='right' >" & Format(STotExptOpenCounter, "#,##0") & "</td>")
                    ret.Append("    </tr>")
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td colspan='2' align='Center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotWh, "#,##0") & "</td>")
                ret.Append("        <td align='right' >&nbsp;</td>")
                ret.Append("        <td align='right' >" & Format(TotCounter, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCapacity, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotAppointment, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotExpt, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotTotal, "#,##0") & "</td>")
                If TotCapacity = 0 Then
                    ret.Append("        <td align='right' >100%</td>")
                Else
                    ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((TotTotal + TotAppointment) * 100) / TotCapacity), "#,##0") & "%</td>")
                End If
                ret.Append("        <td align='right' >" & Format(TotExptOpenCounter, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='Center' >&nbsp;</td>")
            ret.Append("        <td colspan='2' align='Center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotWh, "#,##0") & "</td>")
            ret.Append("        <td align='right' >&nbsp;</td>")
            ret.Append("        <td align='right' >" & Format(GTotCounter, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCapacity, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotAppointment, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotExpt, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotTotal, "#,##0") & "</td>")
            If GTotCapacity = 0 Then
                ret.Append("        <td align='right' >100%</td>")
            Else
                ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((GTotTotal + GTotAppointment) * 100) / GTotCapacity), "#,##0") & "%</td>")
            End If
            ret.Append("        <td align='right' >" & Format(GTotExptOpenCounter, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Week</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Service Type</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Working Hour<br />(Hr/Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >KPI<br />(Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >No. of Counter</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Capacity<br />(Transactions)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Appointment</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Walk in</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Total To be<br />Served</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Capacity Used<br />(%)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected Open<br />Counter</td>")
            ret.Append("    </tr>")

            Dim GTotWh As Long = 0
            'Dim GTotKPI As Long = 0
            Dim GTotCounter As Long = 0
            Dim GTotCapacity As Long = 0
            Dim GTotAppointment As Long = 0
            Dim GTotExpt As Long = 0
            Dim GTotTotal As Long = 0
            Dim GTotExptOpenCounter As Long = 0

            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim ShopID As Long = sDr("shop_id")
                Dim TotWh As Long = 0
                'Dim TotKPI As Long = 0
                Dim TotCounter As Long = 0
                Dim TotCapacity As Long = 0
                Dim TotAppointment As Long = 0
                Dim TotExpt As Long = 0
                Dim TotTotal As Long = 0
                Dim TotExptOpenCounter As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & ShopID & "'"
                Dim wDt As New DataTable
                wDt = dt.DefaultView.ToTable(True, "week_of_year", "show_year")
                For Each wDr As DataRow In wDt.Rows
                    Dim STotWh As Long = 0
                    'Dim STotKPI As Long = 0
                    Dim STotCounter As Long = 0
                    Dim STotCapacity As Long = 0
                    Dim STotAppointment As Long = 0
                    Dim STotExpt As Long = 0
                    Dim STotTotal As Long = 0
                    Dim STotExptOpenCounter As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & ShopID & "' and week_of_year='" & wDr("week_of_year") & "' and show_year='" & wDr("show_year") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >" & wDr("week_of_year") & "</td>")
                        ret.Append("        <td align='center' >" & wDr("show_year") & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(Convert.ToDouble(dr("working_hour")), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & dr("kpi") & "</td>")
                        ret.Append("        <td align='right' >" & dr("no_counter") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("capacity_trans"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("appointment"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("expected_walk_in"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("total_to_be_serve"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("expected_capacity_used"), "#,##0") & "%</td>")
                        ret.Append("        <td align='right' >" & Format(dr("expected_open_counter"), "#,##0") & "</td>")
                        ret.Append("    </tr>")

                        STotWh += dr("working_hour")
                        'STotKPI += dr("kpi")
                        STotCounter += dr("no_counter")
                        STotCapacity += dr("capacity_trans")
                        STotAppointment += dr("appointment")
                        STotExpt += dr("expected_walk_in")
                        STotTotal += dr("total_to_be_serve")
                        STotExptOpenCounter += dr("expected_open_counter")

                        TotWh += dr("working_hour")
                        'TotKPI += dr("kpi")
                        TotCounter += dr("no_counter")
                        TotCapacity += dr("capacity_trans")
                        TotAppointment += dr("appointment")
                        TotExpt += dr("expected_walk_in")
                        TotTotal += dr("total_to_be_serve")
                        TotExptOpenCounter += dr("expected_open_counter")

                        GTotWh += dr("working_hour")
                        'GTotKPI += dr("kpi")
                        GTotCounter += dr("no_counter")
                        GTotCapacity += dr("capacity_trans")
                        GTotAppointment += dr("appointment")
                        GTotExpt += dr("expected_walk_in")
                        GTotTotal += dr("total_to_be_serve")
                        GTotExptOpenCounter += dr("expected_open_counter")
                    Next

                    'Sub Total By Week
                    ret.Append("    <tr style='background: #E4E4E4' >")
                    ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='Center' >" & wDr("week_of_year") & "</td>")
                    ret.Append("        <td align='Center' >" & wDr("show_year") & "</td>")
                    ret.Append("        <td align='Center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotWh, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotCounter, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCapacity, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotAppointment, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotExpt, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotTotal, "#,##0") & "</td>")
                    If STotCapacity = 0 Then
                        ret.Append("        <td align='right' >100%</td>")
                    Else
                        ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((STotTotal + STotAppointment) * 100) / STotCapacity), "#,##0") & "%</td>")
                    End If

                    ret.Append("        <td align='right' >" & Format(STotExptOpenCounter, "#,##0") & "</td>")
                    ret.Append("    </tr>")
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td colspan='3' align='Center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotWh, "#,##0") & "</td>")
                ret.Append("        <td align='right' >&nbsp</td>")
                ret.Append("        <td align='right' >" & Format(TotCounter, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCapacity, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotAppointment, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotExpt, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotTotal, "#,##0") & "</td>")
                If TotCapacity = 0 Then
                    ret.Append("        <td align='right' >100%</td>")
                Else
                    ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((TotTotal + TotAppointment) * 100) / TotCapacity), "#,##0") & "%</td>")
                End If
                ret.Append("        <td align='right' >" & Format(TotExptOpenCounter, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='Center' >&nbsp;</td>")
            ret.Append("        <td colspan='3' align='Center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotWh, "#,##0") & "</td>")
            ret.Append("        <td align='right' >&nbsp;</td>")
            ret.Append("        <td align='right' >" & Format(GTotCounter, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCapacity, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotAppointment, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotExpt, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotTotal, "#,##0") & "</td>")
            If GTotCapacity = 0 Then
                ret.Append("        <td align='right' >100%</td>")
            Else
                ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((GTotTotal + GTotAppointment) * 100) / GTotCapacity), "#,##0") & "%</td>")
            End If
            ret.Append("        <td align='right' >" & Format(GTotExptOpenCounter, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;width:100px' >Shop Name</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Month</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Service Type</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Working Hour<br />(Hr/Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >KPI<br />(Min)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >No. of Counter</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Capacity<br />(Transactions)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Appointment</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Walk in</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Total To be<br />Served</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected<br />Capacity Used<br />(%)</td>")
            ret.Append("        <td align='center' valign='top' style='color: #ffffff;' >Expected Open<br />Counter</td>")
            ret.Append("    </tr>")

            Dim GTotWh As Long = 0
            'Dim GTotKPI As Long = 0
            Dim GTotCounter As Long = 0
            Dim GTotCapacity As Long = 0
            Dim GTotAppointment As Long = 0
            Dim GTotExpt As Long = 0
            Dim GTotTotal As Long = 0
            Dim GTotExptOpenCounter As Long = 0

            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim ShopID As Long = sDr("shop_id")
                Dim TotWh As Long = 0
                'Dim TotKPI As Long = 0
                Dim TotCounter As Long = 0
                Dim TotCapacity As Long = 0
                Dim TotAppointment As Long = 0
                Dim TotExpt As Long = 0
                Dim TotTotal As Long = 0
                Dim TotExptOpenCounter As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & ShopID & "'"
                Dim mDt As New DataTable
                mDt = dt.DefaultView.ToTable(True, "month_no", "show_year")
                For Each mDr As DataRow In mDt.Rows
                    Dim STotWh As Long = 0
                    'Dim STotKPI As Long = 0
                    Dim STotCounter As Long = 0
                    Dim STotCapacity As Long = 0
                    Dim STotAppointment As Long = 0
                    Dim STotExpt As Long = 0
                    Dim STotTotal As Long = 0
                    Dim STotExptOpenCounter As Long = 0

                    dt.DefaultView.RowFilter = "shop_id='" & ShopID & "' and month_no='" & mDr("month_no") & "' and show_year='" & mDr("show_year") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        ret.Append("    <tr >")
                        ret.Append("        <td align='left' >" & sDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='center' >" & mDr("month_no") & "</td>")
                        ret.Append("        <td align='center' >" & mDr("show_year") & "</td>")
                        ret.Append("        <td align='right' >&nbsp;" & dr("service_name_en") & "</td>")
                        ret.Append("        <td align='right' >" & Format(Convert.ToInt64(dr("working_hour")), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("kpi"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("no_counter"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("capacity_trans"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("appointment"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("expected_walk_in"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("total_to_be_serve"), "#,##0") & "</td>")
                        ret.Append("        <td align='right' >" & Format(dr("expected_capacity_used"), "#,##0") & "%</td>")
                        ret.Append("        <td align='right' >" & Format(dr("expected_open_counter"), "#,##0") & "</td>")
                        ret.Append("    </tr>")

                        STotWh += dr("working_hour")
                        'STotKPI += dr("kpi")
                        STotCounter += dr("no_counter")
                        STotCapacity += dr("capacity_trans")
                        STotAppointment += dr("appointment")
                        STotExpt += dr("expected_walk_in")
                        STotTotal += dr("total_to_be_serve")
                        STotExptOpenCounter += dr("expected_open_counter")

                        TotWh += dr("working_hour")
                        'TotKPI += dr("kpi")
                        TotCounter += dr("no_counter")
                        TotCapacity += dr("capacity_trans")
                        TotAppointment += dr("appointment")
                        TotExpt += dr("expected_walk_in")
                        TotTotal += dr("total_to_be_serve")
                        TotExptOpenCounter += dr("expected_open_counter")

                        GTotWh += dr("working_hour")
                        'GTotKPI += dr("kpi")
                        GTotCounter += dr("no_counter")
                        GTotCapacity += dr("capacity_trans")
                        GTotAppointment += dr("appointment")
                        GTotExpt += dr("expected_walk_in")
                        GTotTotal += dr("total_to_be_serve")
                        GTotExptOpenCounter += dr("expected_open_counter")
                    Next

                    'Sub Total By Month
                    ret.Append("    <tr style='background: #E4E4E4' >")
                    ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='Center' >" & mDr("month_no") & "</td>")
                    ret.Append("        <td align='Center' >" & mDr("show_year") & "</td>")
                    ret.Append("        <td align='Center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotWh, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >&nbsp;</td>")
                    ret.Append("        <td align='right' >" & Format(STotCounter, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotCapacity, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotAppointment, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotExpt, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(STotTotal, "#,##0") & "</td>")
                    If STotCapacity = 0 Then
                        ret.Append("        <td align='right' >100%</td>")
                    Else
                        ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((STotTotal + STotAppointment) * 100) / STotCapacity), "#,##0") & "%</td>")
                    End If

                    ret.Append("        <td align='right' >" & Format(STotExptOpenCounter, "#,##0") & "</td>")
                    ret.Append("    </tr>")
                Next

                'Total By Shop
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='Center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td colspan='3' align='Center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TotWh, "#,##0") & "</td>")
                ret.Append("        <td align='right' >&nbsp;</td>")
                ret.Append("        <td align='right' >" & Format(TotCounter, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotCapacity, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotAppointment, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotExpt, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TotTotal, "#,##0") & "</td>")
                If TotCapacity = 0 Then
                    ret.Append("        <td align='right' >100%</td>")
                Else
                    ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((TotTotal + TotAppointment) * 100) / TotCapacity), "#,##0") & "%</td>")
                End If
                ret.Append("        <td align='right' >" & Format(TotExptOpenCounter, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            'Grand Total
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='Center' >&nbsp;</td>")
            ret.Append("        <td colspan='3' align='Center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotWh, "#,##0") & "</td>")
            ret.Append("        <td align='right' >&nbsp;</td>")
            ret.Append("        <td align='right' >" & Format(GTotCounter, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotCapacity, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotAppointment, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotExpt, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GTotTotal, "#,##0") & "</td>")
            If GTotCapacity = 0 Then
                ret.Append("        <td align='right' >100%</td>")
            Else
                ret.Append("        <td align='right' >" & Format(Convert.ToInt64(((GTotTotal + GTotAppointment) * 100) / GTotCapacity), "#,##0") & "%</td>")
            End If
            ret.Append("        <td align='right' >" & Format(GTotExptOpenCounter, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If
    End Sub

End Class
