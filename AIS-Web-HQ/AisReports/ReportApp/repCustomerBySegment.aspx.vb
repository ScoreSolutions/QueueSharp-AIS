Imports System.IO
Imports System.Data

Partial Class ReportApp_repCustomerBySegment
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "CustomerContactBySegmentReport_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repCustomerBySegment.ExportData", Config.GetLoginHistoryID)
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
            Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            para.IntervalMinute = Request("Interval")
            para.TimePeroidFrom = Request("TimeFrom")
            para.TimePeroidTo = Request("TimeTo")
            Dim eng As New Engine.Reports.RepCustBySegmentENG
            Dim dt As New DataTable
            dt = eng.GetReportByTime(para)
            RenderReportByTime(dt)
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepCustBySegmentENG
            Dim dt As New DataTable
            dt = eng.GetReportByDate(para)
            RenderReportByDate(dt)
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepCustBySegmentENG
            Dim dt As New DataTable
            dt = eng.GetReportByWeek(para)
            RenderReportByWeek(dt)
            eng = Nothing
            dt.Dispose()


        ElseIf ReportName = "ByDay" Then
            'Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            'para.ShopID = Request("ShopID")
            'para.DayInWeek = Request("Day")
            'para.WeekInYearFrom = Request("WeekFrom")
            'para.WeekInYearTo = Request("WeekTo")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'Dim eng As New Engine.Reports.RepCustBySegmentENG
            'RenderReportByDay(eng.RenderReportByDay(para))
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim dt As New DataTable
            Dim eng As New Engine.Reports.RepCustBySegmentENG
            dt = eng.GetReportByMonth(para)
            RenderReportByMonth(dt)
            eng = Nothing
            dt.Dispose()
        ElseIf ReportName = "ByYear" Then
            'Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            'para.ShopID = Request("ShopID")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'Dim eng As New Engine.Reports.RepCustBySegmentENG
            'RenderReportByYear(eng.RenderReportByYear(para))
        End If
    End Sub

    Private Sub RenderReportByTime(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim GTotMassRegis As Double = 0
            Dim GTotMassServed As Double = 0
            Dim GTotMassMissedCall As Double = 0
            Dim GTotMassCancelled As Double = 0
            Dim GTotMassIncomplete As Double = 0
            Dim GTotSerenadeRegis As Double = 0
            Dim GTotSerenadeServed As Double = 0
            Dim GTotSerenadeMissedCall As Double = 0
            Dim GTotSerenadeCancelled As Double = 0
            Dim GTotSerenadeIncomplete As Double = 0

            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            Dim RowTmp() As String = Split(dt.Rows(0)("data_value"), "###")
            For Each ServTmp As String In RowTmp
                Dim tmp() As String = Split(ServTmp, "|")
                'ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                'ret.Append("        <td rowspan='3' align='center' style='color: #ffffff;'>Date</td>")
                'ret.Append("        <td rowspan='3' align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                'ret.Append("        <td rowspan='3' align='center' style='color: #ffffff;width:100px' >Time</td>")
                'ret.Append("        <td colspan='10' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>")
                'ret.Append("    </tr>")
                'ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
                'ret.Append("        <td colspan='5' align='center' style='color: #ffffff;'>Mass</td>")
                'ret.Append("        <td colspan='5' align='center' style='color: #ffffff;'>Serenade</td>")
                'ret.Append("    </tr>")
                'ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>")
                'ret.Append("        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>")
                'ret.Append("        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>")
                'ret.Append("        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>")
                'ret.Append("    </tr>")

                ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' style='color: #ffffff;'>Date</td>")
                ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Time</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Mass Regis</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Mass Serve</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Mass Missed call</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Mass Cancel</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Mass Incomplete</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Serenade Regis</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Serenade Serve</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Serenade Missed call</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Serenade Cancel</td>")
                ret.Append("        <td align='center' style='color: #ffffff;' >" & tmp(1) & " Serenade Incomplete</td>")
                ret.Append("    </tr>")

                Dim shDt As New DataTable
                shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
                For Each shDr As DataRow In shDt.Rows
                    Dim STotMassRegis As Double = 0
                    Dim STotMassServed As Double = 0
                    Dim STotMassMissedCall As Double = 0
                    Dim STotMassCancelled As Double = 0
                    Dim STotMassIncomplete As Double = 0
                    Dim STotSerenadeRegis As Double = 0
                    Dim STotSerenadeServed As Double = 0
                    Dim STotSerenadeMissedCall As Double = 0
                    Dim STotSerenadeCancelled As Double = 0
                    Dim STotSerenadeIncomplete As Double = 0

                    Dim dateDt As New DataTable
                    dateDt = dt.DefaultView.ToTable(True, "ShowDate")
                    If dateDt.Rows.Count > 0 Then
                        For Each dateDr As DataRow In dateDt.Rows
                            dt.DefaultView.RowFilter = "ShowDate = '" & dateDr("ShowDate") & "' and shop_id = '" & shDr("shop_id") & "'"
                            Dim vDt As New DataTable
                            vDt = dt.DefaultView.ToTable.Copy
                            dt.DefaultView.RowFilter = ""

                            Dim TotMassRegis As Double = 0
                            Dim TotMassServed As Double = 0
                            Dim TotMassMissedCall As Double = 0
                            Dim TotMassCancelled As Double = 0
                            Dim TotMassIncomplete As Double = 0
                            Dim TotSerenadeRegis As Double = 0
                            Dim TotSerenadeServed As Double = 0
                            Dim TotSerenadeMissedCall As Double = 0
                            Dim TotSerenadeCancelled As Double = 0
                            Dim TotSerenadeIncomplete As Double = 0
                            For Each dr As DataRow In vDt.Rows
                                Dim vRow() As String = Split(dr("data_value"), "###")
                                For Each dRow As String In vRow
                                    Dim vData() As String = Split(dRow, "|")
                                    Dim ItemID As String = vData(0)
                                    If ItemID = tmp(0) Then
                                        Dim MassRegis As Int32 = Convert.ToDouble(vData(3))
                                        Dim MassServe As Int32 = Convert.ToDouble(vData(4))
                                        Dim MassMissCall As Int32 = Convert.ToDouble(vData(5))
                                        Dim MassCancel As Int32 = Convert.ToDouble(vData(6))
                                        Dim MassIncomplete As Int32 = Convert.ToDouble(CInt(vData(7)) + CInt(vData(8)) + CInt(vData(9)))
                                        Dim SerenadeRegis As Int32 = Convert.ToDouble(vData(10))
                                        Dim SerenadeServed As Int32 = Convert.ToDouble(vData(11))
                                        Dim SerenadeMissedCall As Int32 = Convert.ToDouble(vData(12))
                                        Dim SerenadeCancelled As Int32 = Convert.ToDouble(vData(13))
                                        Dim SerenadeIncomplete As Int32 = Convert.ToDouble(CInt(vData(14)) + CInt(vData(15)) + CInt(vData(16)))

                                        ret.Append("    <tr>")
                                        ret.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                                        ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                                        ret.Append("        <td align='left'>" & dr("show_time") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(MassRegis, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(MassServe, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(MassMissCall, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(MassCancel, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(MassIncomplete, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(SerenadeRegis, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(SerenadeServed, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(SerenadeMissedCall, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(SerenadeCancelled, "#,##0") & "</td>")
                                        ret.Append("        <td align='right'>" & Format(SerenadeIncomplete, "#,##0") & "</td>")
                                        ret.Append("    </tr>")

                                        TotMassRegis += MassRegis
                                        TotMassServed += MassServe
                                        TotMassMissedCall += MassMissCall
                                        TotMassCancelled += MassCancel
                                        TotMassIncomplete += MassIncomplete
                                        TotSerenadeRegis += SerenadeRegis
                                        TotSerenadeServed += SerenadeServed
                                        TotSerenadeMissedCall += SerenadeMissedCall
                                        TotSerenadeCancelled += SerenadeCancelled
                                        TotSerenadeIncomplete += SerenadeIncomplete
                                    End If
                                Next
                            Next
                            vDt.Dispose()

                            'SubTotal By Date
                            ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                            ret.Append("        <td align='center' colspan='3'>Sub Total</td>")
                            ret.Append("        <td align='right'>" & Format(TotMassRegis, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotMassServed, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotMassMissedCall, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotMassCancelled, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotMassIncomplete, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotSerenadeRegis, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotSerenadeServed, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotSerenadeMissedCall, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotSerenadeCancelled, "#,##0") & "</td>")
                            ret.Append("        <td align='right'>" & Format(TotSerenadeIncomplete, "#,##0") & "</td>")
                            ret.Append("    </tr>")

                            STotMassRegis += TotMassRegis
                            STotMassServed += TotMassServed
                            STotMassMissedCall += TotMassMissedCall
                            STotMassCancelled += TotMassCancelled
                            STotMassIncomplete += TotMassIncomplete
                            STotSerenadeRegis += TotSerenadeRegis
                            STotSerenadeServed += TotSerenadeServed
                            STotSerenadeMissedCall += TotSerenadeMissedCall
                            STotSerenadeCancelled += TotSerenadeCancelled
                            STotSerenadeIncomplete += TotSerenadeIncomplete
                        Next
                    End If
                    dateDt.Dispose()

                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                    ret.Append("        <td align='center' colspan='3'>Total " & shDr("shop_name_en").ToString & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotMassRegis, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotMassServed, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotMassMissedCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotMassCancelled, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotMassIncomplete, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotSerenadeRegis, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotSerenadeServed, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotSerenadeMissedCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotSerenadeCancelled, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(STotSerenadeIncomplete, "#,##0") & "</td>")
                    ret.Append("    </tr>")

                    GTotMassRegis += STotMassRegis
                    GTotMassServed += STotMassServed
                    GTotMassMissedCall += STotMassMissedCall
                    GTotMassCancelled += STotMassCancelled
                    GTotMassIncomplete += STotMassIncomplete
                    GTotSerenadeRegis += STotSerenadeRegis
                    GTotSerenadeServed += STotSerenadeServed
                    GTotSerenadeMissedCall += STotSerenadeMissedCall
                    GTotSerenadeCancelled += STotSerenadeCancelled
                    GTotSerenadeIncomplete += STotSerenadeIncomplete
                Next
                shDt.Dispose()
            Next
            '        '******************* Grand Total ******************
            ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;'>")
            ret.Append("        <td align='center' colspan='3'>Grand Total</td>")
            ret.Append("        <td align='right'>" & Format(GTotMassRegis, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotMassServed, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotMassMissedCall, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotMassCancelled, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotMassIncomplete, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotSerenadeRegis, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotSerenadeServed, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotSerenadeMissedCall, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotSerenadeCancelled, "#,##0") & "</td>")
            ret.Append("        <td align='right'>" & Format(GTotSerenadeIncomplete, "#,##0") & "</td>")
            ret.Append("    </tr>")
            '        '***************************************************
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
        Else
            btnExport.Visible = False
        End If
        dt = Nothing

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")

            Dim shDt As New DataTable  'Shop
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            If shDt.Rows.Count > 0 Then
                Dim GMassRegis As Int32 = 0
                Dim GMassServe As Int32 = 0
                Dim GMassMissCall As Int32 = 0
                Dim GMassCancel As Int32 = 0
                Dim GMassIncomplete As Int32 = 0
                Dim GSerenadeRegis As Int32 = 0
                Dim GSerenadeServed As Int32 = 0
                Dim GSerenadeMissedCall As Int32 = 0
                Dim GSerenadeCancelled As Int32 = 0
                Dim GSerenadeIncomplete As Int32 = 0
                For Each shDr As DataRow In shDt.Rows
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim sDt As New DataTable   'Service
                    sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy
                    If sDt.Rows.Count > 0 Then
                        Dim TMassRegis As Int32 = 0
                        Dim TMassServe As Int32 = 0
                        Dim TMassMissCall As Int32 = 0
                        Dim TMassCancel As Int32 = 0
                        Dim TMassIncomplete As Int32 = 0
                        Dim TSerenadeRegis As Int32 = 0
                        Dim TSerenadeServed As Int32 = 0
                        Dim TSerenadeMissedCall As Int32 = 0
                        Dim TSerenadeCancelled As Int32 = 0
                        Dim TSerenadeIncomplete As Int32 = 0

                        For Each sDr As DataRow In sDt.Rows
                            '******************** Header Row *********************
                            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Date</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Incomplete</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Incomplete</td>")
                            ret.Append("    </tr>")

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "'"
                            If dt.DefaultView.Count > 0 Then
                                Dim MassRegis As Int32 = 0
                                Dim MassServe As Int32 = 0
                                Dim MassMissCall As Int32 = 0
                                Dim MassCancel As Int32 = 0
                                Dim MassIncomplete As Int32 = 0
                                Dim SerenadeRegis As Int32 = 0
                                Dim SerenadeServed As Int32 = 0
                                Dim SerenadeMissedCall As Int32 = 0
                                Dim SerenadeCancelled As Int32 = 0
                                Dim SerenadeIncomplete As Int32 = 0
                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                                    ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_incomplete"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_incomplete"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    'Sub Total
                                    MassRegis += Convert.ToInt32(dr("mass_regis"))
                                    MassServe += Convert.ToInt32(dr("mass_serve"))
                                    MassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    MassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    MassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    SerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    SerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    SerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    SerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    SerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))

                                    'Total Shop
                                    TMassRegis += Convert.ToInt32(dr("mass_regis"))
                                    TMassServe += Convert.ToInt32(dr("mass_serve"))
                                    TMassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    TMassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    TMassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    TSerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    TSerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    TSerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    TSerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    TSerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))

                                    'Total Service
                                    GMassRegis += Convert.ToInt32(dr("mass_regis"))
                                    GMassServe += Convert.ToInt32(dr("mass_serve"))
                                    GMassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    GMassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    GMassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    GSerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    GSerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    GSerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    GSerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    GSerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))
                                Next

                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                                ret.Append("        <td align='left'>Sub Total</td>")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassIncomplete, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeServed, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeMissedCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeCancelled, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeIncomplete, "#,##0") & "</td>")
                                ret.Append("    </tr>")
                            End If
                            dt.DefaultView.RowFilter = ""
                        Next
                        'Total by Shop
                        ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='left'>Total</td>")
                        ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassIncomplete, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeMissedCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeCancelled, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeIncomplete, "#,##0") & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left'>Grand Total</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='right'>" & Format(GMassRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassIncomplete, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeMissedCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeCancelled, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeIncomplete, "#,##0") & "</td>")
                ret.Append("    </tr>")
            End If
            shDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            lblerror.Visible = False
        Else
            btnExport.Visible = False
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            Dim shDt As New DataTable  'Shop
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            If shDt.Rows.Count > 0 Then
                Dim GMassRegis As Int32 = 0
                Dim GMassServe As Int32 = 0
                Dim GMassMissCall As Int32 = 0
                Dim GMassCancel As Int32 = 0
                Dim GMassIncomplete As Int32 = 0
                Dim GSerenadeRegis As Int32 = 0
                Dim GSerenadeServed As Int32 = 0
                Dim GSerenadeMissedCall As Int32 = 0
                Dim GSerenadeCancelled As Int32 = 0
                Dim GSerenadeIncomplete As Int32 = 0
                For Each shDr As DataRow In shDt.Rows
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim sDt As New DataTable   'Service
                    sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy
                    If sDt.Rows.Count > 0 Then
                        Dim TMassRegis As Int32 = 0
                        Dim TMassServe As Int32 = 0
                        Dim TMassMissCall As Int32 = 0
                        Dim TMassCancel As Int32 = 0
                        Dim TMassIncomplete As Int32 = 0
                        Dim TSerenadeRegis As Int32 = 0
                        Dim TSerenadeServed As Int32 = 0
                        Dim TSerenadeMissedCall As Int32 = 0
                        Dim TSerenadeCancelled As Int32 = 0
                        Dim TSerenadeIncomplete As Int32 = 0

                        For Each sDr As DataRow In sDt.Rows
                            '******************** Header Row *********************
                            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Week No.</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Incomplete</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Incomplete</td>")
                            ret.Append("    </tr>")

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "'"
                            If dt.DefaultView.Count > 0 Then
                                Dim MassRegis As Int32 = 0
                                Dim MassServe As Int32 = 0
                                Dim MassMissCall As Int32 = 0
                                Dim MassCancel As Int32 = 0
                                Dim MassIncomplete As Int32 = 0
                                Dim SerenadeRegis As Int32 = 0
                                Dim SerenadeServed As Int32 = 0
                                Dim SerenadeMissedCall As Int32 = 0
                                Dim SerenadeCancelled As Int32 = 0
                                Dim SerenadeIncomplete As Int32 = 0
                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='center'>&nbsp;" & dr("week_of_year") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_incomplete"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_incomplete"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    'Sub Total
                                    MassRegis += Convert.ToInt32(dr("mass_regis"))
                                    MassServe += Convert.ToInt32(dr("mass_serve"))
                                    MassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    MassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    MassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    SerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    SerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    SerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    SerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    SerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))

                                    'Total Shop
                                    TMassRegis += Convert.ToInt32(dr("mass_regis"))
                                    TMassServe += Convert.ToInt32(dr("mass_serve"))
                                    TMassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    TMassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    TMassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    TSerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    TSerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    TSerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    TSerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    TSerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))

                                    'Total Service
                                    GMassRegis += Convert.ToInt32(dr("mass_regis"))
                                    GMassServe += Convert.ToInt32(dr("mass_serve"))
                                    GMassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    GMassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    GMassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    GSerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    GSerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    GSerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    GSerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    GSerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))
                                Next

                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>Sub Total</td>")
                                ret.Append("        <td align='right'>" & Format(MassRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassIncomplete, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeServed, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeMissedCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeCancelled, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeIncomplete, "#,##0") & "</td>")
                                ret.Append("    </tr>")
                            End If
                            dt.DefaultView.RowFilter = ""
                        Next
                        'Total by Shop
                        ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>Total</td>")
                        ret.Append("        <td align='right'>" & Format(TMassRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassIncomplete, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeMissedCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeCancelled, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeIncomplete, "#,##0") & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left'>Grand Total</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='right'>" & Format(GMassRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassIncomplete, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeMissedCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeCancelled, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeIncomplete, "#,##0") & "</td>")
                ret.Append("    </tr>")
            End If
            shDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            lblerror.Visible = False
        Else
            btnExport.Visible = False
        End If
    End Sub


    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            Dim shDt As New DataTable  'Shop
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            If shDt.Rows.Count > 0 Then
                Dim GMassRegis As Int32 = 0
                Dim GMassServe As Int32 = 0
                Dim GMassMissCall As Int32 = 0
                Dim GMassCancel As Int32 = 0
                Dim GMassIncomplete As Int32 = 0
                Dim GSerenadeRegis As Int32 = 0
                Dim GSerenadeServed As Int32 = 0
                Dim GSerenadeMissedCall As Int32 = 0
                Dim GSerenadeCancelled As Int32 = 0
                Dim GSerenadeIncomplete As Int32 = 0
                For Each shDr As DataRow In shDt.Rows
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim sDt As New DataTable   'Service
                    sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy
                    If sDt.Rows.Count > 0 Then
                        Dim TMassRegis As Int32 = 0
                        Dim TMassServe As Int32 = 0
                        Dim TMassMissCall As Int32 = 0
                        Dim TMassCancel As Int32 = 0
                        Dim TMassIncomplete As Int32 = 0
                        Dim TSerenadeRegis As Int32 = 0
                        Dim TSerenadeServed As Int32 = 0
                        Dim TSerenadeMissedCall As Int32 = 0
                        Dim TSerenadeCancelled As Int32 = 0
                        Dim TSerenadeIncomplete As Int32 = 0

                        For Each sDr As DataRow In sDt.Rows
                            '******************** Header Row *********************
                            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Month</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Mass Incomplete</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Serenade Incomplete</td>")
                            ret.Append("    </tr>")

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "'"
                            If dt.DefaultView.Count > 0 Then
                                Dim MassRegis As Int32 = 0
                                Dim MassServe As Int32 = 0
                                Dim MassMissCall As Int32 = 0
                                Dim MassCancel As Int32 = 0
                                Dim MassIncomplete As Int32 = 0
                                Dim SerenadeRegis As Int32 = 0
                                Dim SerenadeServed As Int32 = 0
                                Dim SerenadeMissedCall As Int32 = 0
                                Dim SerenadeCancelled As Int32 = 0
                                Dim SerenadeIncomplete As Int32 = 0
                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='center'>&nbsp;" & dr("show_month") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("mass_incomplete"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("serenade_incomplete"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    'Sub Total
                                    MassRegis += Convert.ToInt32(dr("mass_regis"))
                                    MassServe += Convert.ToInt32(dr("mass_serve"))
                                    MassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    MassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    MassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    SerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    SerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    SerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    SerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    SerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))

                                    'Total Shop
                                    TMassRegis += Convert.ToInt32(dr("mass_regis"))
                                    TMassServe += Convert.ToInt32(dr("mass_serve"))
                                    TMassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    TMassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    TMassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    TSerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    TSerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    TSerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    TSerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    TSerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))

                                    'Total Service
                                    GMassRegis += Convert.ToInt32(dr("mass_regis"))
                                    GMassServe += Convert.ToInt32(dr("mass_serve"))
                                    GMassMissCall += Convert.ToInt32(dr("mass_miss_call"))
                                    GMassCancel += Convert.ToInt32(dr("mass_cancel"))
                                    GMassIncomplete += Convert.ToInt32(dr("mass_incomplete"))
                                    GSerenadeRegis += Convert.ToInt32(dr("serenade_regis"))
                                    GSerenadeServed += Convert.ToInt32(dr("serenade_serve"))
                                    GSerenadeMissedCall += Convert.ToInt32(dr("serenade_miss_call"))
                                    GSerenadeCancelled += Convert.ToInt32(dr("serenade_cancel"))
                                    GSerenadeIncomplete += Convert.ToInt32(dr("serenade_incomplete"))
                                Next

                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>Sub Total</td>")
                                ret.Append("        <td align='right'>" & Format(MassRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(MassIncomplete, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeServed, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeMissedCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeCancelled, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(SerenadeIncomplete, "#,##0") & "</td>")
                                ret.Append("    </tr>")
                            End If
                            dt.DefaultView.RowFilter = ""
                        Next
                        'Total by Shop
                        ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>Total</td>")
                        ret.Append("        <td align='right'>" & Format(TMassRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TMassIncomplete, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeMissedCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeCancelled, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(TSerenadeIncomplete, "#,##0") & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left'>Grand Total</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='right'>" & Format(GMassRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GMassIncomplete, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeMissedCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeCancelled, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GSerenadeIncomplete, "#,##0") & "</td>")
                ret.Append("    </tr>")
            End If
            shDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            lblerror.Visible = False
        Else
            btnExport.Visible = False
        End If
    End Sub

   
End Class
