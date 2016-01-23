Imports System.IO
Imports System.Data

Partial Class ReportApp_repCustomerByNetwork
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/ms-excel", "CustomerContactByNetworkReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
        'ExportData("application/vnd.ms-excel", "Customer_Contact_by_Network_Report.xlsx")

    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repCustomerByNetwork.ExportData", Config.GetLoginHistoryID)
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
            Dim para As New CenParaDB.ReportCriteria.CustByNetworkTypePara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            para.IntervalMinute = Request("Interval")
            para.TimePeroidFrom = Request("TimeFrom")
            para.TimePeroidTo = Request("TimeTo")
            Dim eng As New Engine.Reports.RepCustByNetworkTypeENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByTime(para)
            If dt.Rows.Count = 0 Then
                btnExport.Visible = False
            Else
                btnExport.Visible = True
            End If
            RenderReportByTime(dt)
            eng = Nothing
            para = Nothing
            dt.Dispose()

        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.CustByNetworkTypePara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepCustByNetworkTypeENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByDate(para)
            If dt.Rows.Count = 0 Then
                btnExport.Visible = False
            Else
                btnExport.Visible = True
            End If
            RenderReportByDate(dt)
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.CustByNetworkTypePara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepCustByNetworkTypeENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByWeek(para)
            If dt.Rows.Count = 0 Then
                btnExport.Visible = False
            Else
                btnExport.Visible = True
            End If
            RenderReportByWeek(dt)
            eng = Nothing
            dt.Dispose()
            para = Nothing
        
        ElseIf ReportName = "ByDay" Then
            'Dim para As New CenParaDB.ReportCriteria.CustByNetworkTypePara
            'para.ShopID = Request("ShopID")
            'para.DayInWeek = Request("Day")
            'para.WeekInYearFrom = Request("WeekFrom")
            'para.WeekInYearTo = Request("WeekTo")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'Dim eng As New Engine.Reports.RepCustByNetworkTypeENG
            'RenderReportByDay(eng.GetReportDataByDay(para))
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.CustByNetworkTypePara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepCustByNetworkTypeENG
            Dim dt As New DataTable
            dt = eng.GetReportDataByMonth(para)
            If dt.Rows.Count = 0 Then
                btnExport.Visible = False
            Else
                btnExport.Visible = True
            End If
            RenderReportByMonth(dt)
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByYear" Then
            'Dim para As New CenParaDB.ReportCriteria.CustByNetworkTypePara
            'para.ShopID = Request("ShopID")
            'para.YearFrom = Request("YearFrom")
            'para.YearTo = Request("YearTo")
            'Dim eng As New Engine.Reports.RepCustByNetworkTypeENG
            'RenderReportByYear(eng.GetReportDataByYear(para))
        End If
    End Sub

    Private Sub RenderReportByTime(ByVal dt As DataTable)
        'Dim ret As String = ""
        Dim retGrandTotal As String = ""
        Dim RowTmp() As String = {}
        Dim ret_sub(10) As String
        If dt.Rows.Count > 0 Then
            Dim Header As Int32 = 0
            Dim TotMassRegis() As Double
            Dim TotMassServed() As Double
            Dim TotMassMissedCall() As Double
            Dim TotMassCancelled() As Double
            Dim TotMassIncomplete() As Double
            Dim TotSerenadeRegis() As Double
            Dim TotSerenadeServed() As Double
            Dim TotSerenadeMissedCall() As Double
            Dim TotSerenadeCancelled() As Double
            Dim TotSerenadeIncomplete() As Double
            Dim TotNonRegis() As Double
            Dim TotNonServed() As Double
            Dim TotNonMissedCall() As Double
            Dim TotNonCancelled() As Double
            Dim TotNonIncomplete() As Double
            '++ 
            Dim TotAIS3GRegis() As Double
            Dim TotAIS3GServed() As Double
            Dim TotAIS3GMissedCall() As Double
            Dim TotAIS3GCancelled() As Double
            Dim TotAIS3GIncomplete() As Double
            Dim TotOTC3GRegis() As Double
            Dim TotOTC3GServed() As Double
            Dim TotOTC3GMissedCall() As Double
            Dim TotOTC3GCancelled() As Double
            Dim TotOTC3GIncomplete() As Double


            Dim STotMassRegis() As Double
            Dim STotMassServed() As Double
            Dim STotMassMissedCall() As Double
            Dim STotMassCancelled() As Double
            Dim STotMassIncomplete() As Double
            Dim STotSerenadeRegis() As Double
            Dim STotSerenadeServed() As Double
            Dim STotSerenadeMissedCall() As Double
            Dim STotSerenadeCancelled() As Double
            Dim STotSerenadeIncomplete() As Double
            Dim STotNonRegis() As Double
            Dim STotNonServed() As Double
            Dim STotNonMissedCall() As Double
            Dim STotNonCancelled() As Double
            Dim STotNonIncomplete() As Double
            '++
            Dim STotAIS3GRegis() As Double
            Dim STotAIS3GServed() As Double
            Dim STotAIS3GMissedCall() As Double
            Dim STotAIS3GCancelled() As Double
            Dim STotAIS3GIncomplete() As Double
            Dim STotOTC3GRegis() As Double
            Dim STotOTC3GServed() As Double
            Dim STotOTC3GMissedCall() As Double
            Dim STotOTC3GCancelled() As Double
            Dim STotOTC3GIncomplete() As Double

            Dim GTotMassRegis() As Double
            Dim GTotMassServed() As Double
            Dim GTotMassMissedCall() As Double
            Dim GTotMassCancelled() As Double
            Dim GTotMassIncomplete() As Double
            Dim GTotSerenadeRegis() As Double
            Dim GTotSerenadeServed() As Double
            Dim GTotSerenadeMissedCall() As Double
            Dim GTotSerenadeCancelled() As Double
            Dim GTotSerenadeIncomplete() As Double
            Dim GTotNonRegis() As Double
            Dim GTotNonServed() As Double
            Dim GTotNonMissedCall() As Double
            Dim GTotNonCancelled() As Double
            Dim GTotNonIncomplete() As Double
            '++
            Dim GTotAIS3GRegis() As Double
            Dim GTotAIS3GServed() As Double
            Dim GTotAIS3GMissedCall() As Double
            Dim GTotAIS3GCancelled() As Double
            Dim GTotAIS3GIncomplete() As Double
            Dim GTotOTC3GRegis() As Double
            Dim GTotOTC3GServed() As Double
            Dim GTotOTC3GMissedCall() As Double
            Dim GTotOTC3GCancelled() As Double
            Dim GTotOTC3GIncomplete() As Double

            Dim AGTotMassRegis As Double
            Dim AGTotMassServed As Double
            Dim AGTotMassMissedCall As Double
            Dim AGTotMassCancelled As Double
            Dim AGTotMassIncomplete As Double
            Dim AGTotSerenadeRegis As Double
            Dim AGTotSerenadeServed As Double
            Dim AGTotSerenadeMissedCall As Double
            Dim AGTotSerenadeCancelled As Double
            Dim AGTotSerenadeIncomplete As Double
            Dim AGTotNonRegis As Double
            Dim AGTotNonServed As Double
            Dim AGTotNonMissedCall As Double
            Dim AGTotNonCancelled As Double
            Dim AGTotNonIncomplete As Double
            '++
            Dim AGTotAIS3GRegis As Double
            Dim AGTotAIS3GServed As Double
            Dim AGTotAIS3GMissedCall As Double
            Dim AGTotAIS3GCancelled As Double
            Dim AGTotAIS3GIncomplete As Double
            Dim AGTotOTC3GRegis As Double
            Dim AGTotOTC3GServed As Double
            Dim AGTotOTC3GMissedCall As Double
            Dim AGTotOTC3GCancelled As Double
            Dim AGTotOTC3GIncomplete As Double
            

            Dim ShopIDGroup As Int32 = 0
            Dim DateGroup As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                RowTmp = Split(dr("data_value"), "###")

                If ShopIDGroup = 0 Then
                    ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
                    DateGroup = CDate(dt.Rows(i).Item("service_date").ToString).ToShortDateString

                    ReDim TotMassRegis(RowTmp.Length)
                    ReDim TotMassServed(RowTmp.Length)
                    ReDim TotMassMissedCall(RowTmp.Length)
                    ReDim TotMassCancelled(RowTmp.Length)
                    ReDim TotMassIncomplete(RowTmp.Length)
                    ReDim TotSerenadeRegis(RowTmp.Length)
                    ReDim TotSerenadeServed(RowTmp.Length)
                    ReDim TotSerenadeMissedCall(RowTmp.Length)
                    ReDim TotSerenadeCancelled(RowTmp.Length)
                    ReDim TotSerenadeIncomplete(RowTmp.Length)
                    ReDim TotNonRegis(RowTmp.Length)
                    ReDim TotNonServed(RowTmp.Length)
                    ReDim TotNonMissedCall(RowTmp.Length)
                    ReDim TotNonCancelled(RowTmp.Length)
                    ReDim TotNonIncomplete(RowTmp.Length)
                    ReDim TotAIS3GRegis(RowTmp.Length)
                    ReDim TotAIS3GServed(RowTmp.Length)
                    ReDim TotAIS3GMissedCall(RowTmp.Length)
                    ReDim TotAIS3GCancelled(RowTmp.Length)
                    ReDim TotAIS3GIncomplete(RowTmp.Length)
                    ReDim TotOTC3GRegis(RowTmp.Length)
                    ReDim TotOTC3GServed(RowTmp.Length)
                    ReDim TotOTC3GMissedCall(RowTmp.Length)
                    ReDim TotOTC3GCancelled(RowTmp.Length)
                    ReDim TotOTC3GIncomplete(RowTmp.Length)

                    ReDim STotMassRegis(RowTmp.Length)
                    ReDim STotMassServed(RowTmp.Length)
                    ReDim STotMassMissedCall(RowTmp.Length)
                    ReDim STotMassCancelled(RowTmp.Length)
                    ReDim STotMassIncomplete(RowTmp.Length)
                    ReDim STotSerenadeRegis(RowTmp.Length)
                    ReDim STotSerenadeServed(RowTmp.Length)
                    ReDim STotSerenadeMissedCall(RowTmp.Length)
                    ReDim STotSerenadeCancelled(RowTmp.Length)
                    ReDim STotSerenadeIncomplete(RowTmp.Length)
                    ReDim STotNonRegis(RowTmp.Length)
                    ReDim STotNonServed(RowTmp.Length)
                    ReDim STotNonMissedCall(RowTmp.Length)
                    ReDim STotNonCancelled(RowTmp.Length)
                    ReDim STotNonIncomplete(RowTmp.Length)
                    ReDim STotAIS3GRegis(RowTmp.Length)
                    ReDim STotAIS3GServed(RowTmp.Length)
                    ReDim STotAIS3GMissedCall(RowTmp.Length)
                    ReDim STotAIS3GCancelled(RowTmp.Length)
                    ReDim STotAIS3GIncomplete(RowTmp.Length)
                    ReDim STotOTC3GRegis(RowTmp.Length)
                    ReDim STotOTC3GServed(RowTmp.Length)
                    ReDim STotOTC3GMissedCall(RowTmp.Length)
                    ReDim STotOTC3GCancelled(RowTmp.Length)
                    ReDim STotOTC3GIncomplete(RowTmp.Length)

                    ReDim GTotMassRegis(RowTmp.Length)
                    ReDim GTotMassServed(RowTmp.Length)
                    ReDim GTotMassMissedCall(RowTmp.Length)
                    ReDim GTotMassCancelled(RowTmp.Length)
                    ReDim GTotMassIncomplete(RowTmp.Length)
                    ReDim GTotSerenadeRegis(RowTmp.Length)
                    ReDim GTotSerenadeServed(RowTmp.Length)
                    ReDim GTotSerenadeMissedCall(RowTmp.Length)
                    ReDim GTotSerenadeCancelled(RowTmp.Length)
                    ReDim GTotSerenadeIncomplete(RowTmp.Length)
                    ReDim GTotNonRegis(RowTmp.Length)
                    ReDim GTotNonServed(RowTmp.Length)
                    ReDim GTotNonMissedCall(RowTmp.Length)
                    ReDim GTotNonCancelled(RowTmp.Length)
                    ReDim GTotNonIncomplete(RowTmp.Length)
                    ReDim GTotAIS3GRegis(RowTmp.Length)
                    ReDim GTotAIS3GServed(RowTmp.Length)
                    ReDim GTotAIS3GMissedCall(RowTmp.Length)
                    ReDim GTotAIS3GCancelled(RowTmp.Length)
                    ReDim GTotAIS3GIncomplete(RowTmp.Length)
                    ReDim GTotOTC3GRegis(RowTmp.Length)
                    ReDim GTotOTC3GServed(RowTmp.Length)
                    ReDim GTotOTC3GMissedCall(RowTmp.Length)
                    ReDim GTotOTC3GCancelled(RowTmp.Length)
                    ReDim GTotOTC3GIncomplete(RowTmp.Length)
                End If

                If i = 0 Then
                    '******************** Header Row *********************
                    Dim r As Int32 = 0
                    For Each ServTmp As String In RowTmp
                        lblReportDesc.Text = ""
                        Dim tmp() As String = Split(ServTmp, "|")

                        lblReportDesc.Text = "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
                        lblReportDesc.Text += "        <td rowspan='3' align='center' style='color: #ffffff;'>Date</td>"
                        lblReportDesc.Text += "        <td rowspan='3' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
                        lblReportDesc.Text += "        <td rowspan='3' align='center' style='color: #ffffff;width:100px' >Time</td>"
                        lblReportDesc.Text += "        <td colspan='30' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>"
                        lblReportDesc.Text += "    </tr>"
                        lblReportDesc.Text += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
                        lblReportDesc.Text += "        <td colspan='5' align='center' style='color: #ffffff;'>GSM</td>"
                        lblReportDesc.Text += "        <td colspan='5' align='center' style='color: #ffffff;'>One-2-Call</td>"
                        lblReportDesc.Text += "        <td colspan='5' align='center' style='color: #ffffff;'>Non Mobile</td>"
                        lblReportDesc.Text += "        <td colspan='5' align='center' style='color: #ffffff;'>AIS3G</td>"
                        lblReportDesc.Text += "        <td colspan='5' align='center' style='color: #ffffff;'>OTC3G</td>"
                        lblReportDesc.Text += "    </tr>"
                        lblReportDesc.Text += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>"
                        lblReportDesc.Text += "        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>"
                        lblReportDesc.Text += "        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>"
                        lblReportDesc.Text += "        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>"
                        lblReportDesc.Text += "        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Regis</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Serve</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Missed call</td>"
                        lblReportDesc.Text += "        <td rowspan='1' align='center' style='color: #ffffff;' >Cancel</td>"
                        lblReportDesc.Text += "        <td colspan='1' align='center' style='color: #ffffff;' >Incomplete</td>"
                        lblReportDesc.Text += "    </tr>"

                        ret_sub(r) += lblReportDesc.Text
                        r += 1
                        '***********************************************
                    Next
                End If

                RowTmp = Split(dr("data_value"), "###")
                Dim k As Integer = 0
                For Each ServTmp As String In RowTmp
                    lblReportDesc.Text = ""
                    Dim tmp() As String = Split(ServTmp, "|")
                    '******************** Data Row ******************
                    Dim MassRegis As Int32 = Convert.ToDouble(tmp(3))
                    Dim MassServed As Int32 = Convert.ToDouble(tmp(4))
                    Dim MassMissedCall As Int32 = Convert.ToDouble(tmp(5))
                    Dim MassCancelled As Int32 = Convert.ToDouble(tmp(6))
                    Dim MassIncomplete As Int32 = Convert.ToDouble(CInt(tmp(7)) + CInt(tmp(8)) + CInt(tmp(9)))

                    Dim SerenadeRegis As Int32 = Convert.ToDouble(tmp(10))
                    Dim SerenadeServed As Int32 = Convert.ToDouble(tmp(11))
                    Dim SerenadeMissedCall As Int32 = Convert.ToDouble(tmp(12))
                    Dim SerenadeCancelled As Int32 = Convert.ToDouble(tmp(13))
                    Dim SerenadeIncomplete As Int32 = Convert.ToDouble(CInt(tmp(14)) + CInt(tmp(15)) + CInt(tmp(16)))

                    Dim NonRegis As Int32 = Convert.ToDouble(tmp(17))
                    Dim NonServed As Int32 = Convert.ToDouble(tmp(18))
                    Dim NonMissedCall As Int32 = Convert.ToDouble(tmp(19))
                    Dim NonCancelled As Int32 = Convert.ToDouble(tmp(20))
                    Dim NonIncomplete As Int32 = Convert.ToDouble(CInt(tmp(21)) + CInt(tmp(22)) + CInt(tmp(23)))

                    'AIS 3G
                    Dim AIS3GRegis As Int32 = 0
                    Dim AIS3GServed As Int32 = 0
                    Dim AIS3GMissedCall As Int32 = 0
                    Dim AIS3GCancelled As Int32 = 0
                    Dim AIS3GIncomplete As Int32 = 0

                    'AIS 3G One-2-Call
                    Dim OTC3GRegis As Int32 = 0
                    Dim OTC3GServed As Int32 = 0
                    Dim OTC3GMissedCall As Int32 = 0
                    Dim OTC3GCancelled As Int32 = 0
                    Dim OTC3GIncomplete As Int32 = 0
                    If tmp.Length = 38 Then
                        AIS3GRegis = Convert.ToDouble(tmp(24))
                        AIS3GServed = Convert.ToDouble(tmp(25))
                        AIS3GMissedCall = Convert.ToDouble(tmp(26))
                        AIS3GCancelled = Convert.ToDouble(tmp(27))
                        AIS3GIncomplete = Convert.ToDouble(CInt(tmp(28)) + CInt(tmp(29)) + CInt(tmp(30)))

                        OTC3GRegis = Convert.ToDouble(tmp(31))
                        OTC3GServed = Convert.ToDouble(tmp(32))
                        OTC3GMissedCall = Convert.ToDouble(tmp(33))
                        OTC3GCancelled = Convert.ToDouble(tmp(34))
                        OTC3GIncomplete = Convert.ToDouble(CInt(tmp(35)) + CInt(tmp(36)) + CInt(tmp(37)))
                    End If

                    Dim dbRetSub As New StringBuilder
                    dbRetSub.Append("    <tr>")
                    dbRetSub.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                    dbRetSub.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                    dbRetSub.Append("        <td align='left'>" & dr("show_time") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(MassRegis, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(MassServed, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(MassMissedCall, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(MassCancelled, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(MassIncomplete, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(SerenadeRegis, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(SerenadeServed, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(SerenadeMissedCall, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(SerenadeCancelled, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(SerenadeIncomplete, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(NonRegis, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(NonServed, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(NonMissedCall, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(NonCancelled, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(NonIncomplete, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(AIS3GRegis, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(AIS3GServed, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(AIS3GMissedCall, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(AIS3GCancelled, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(AIS3GIncomplete, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(OTC3GRegis, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(OTC3GServed, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(OTC3GMissedCall, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(OTC3GCancelled, "###,##0") & "</td>")
                    dbRetSub.Append("        <td align='right'>" & Format(OTC3GIncomplete, "###,##0") & "</td>")

                    TotMassRegis(k) += Convert.ToDouble(tmp(3))
                    TotMassServed(k) += Convert.ToDouble(tmp(4))
                    TotMassMissedCall(k) += Convert.ToDouble(tmp(5))
                    TotMassCancelled(k) += Convert.ToDouble(tmp(6))
                    TotMassIncomplete(k) += Convert.ToDouble(CInt(tmp(7)) + CInt(tmp(8)) + CInt(tmp(9)))
                    TotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
                    TotSerenadeServed(k) += Convert.ToDouble(tmp(11))
                    TotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
                    TotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
                    TotSerenadeIncomplete(k) += Convert.ToDouble(CInt(tmp(14)) + CInt(tmp(15)) + CInt(tmp(16)))
                    TotNonRegis(k) += Convert.ToDouble(tmp(17))
                    TotNonServed(k) += Convert.ToDouble(tmp(18))
                    TotNonMissedCall(k) += Convert.ToDouble(tmp(19))
                    TotNonCancelled(k) += Convert.ToDouble(tmp(20))
                    TotNonIncomplete(k) += Convert.ToDouble(CInt(tmp(21)) + CInt(tmp(22)) + CInt(tmp(23)))
                    '++
                    TotAIS3GRegis(k) += Convert.ToDouble(tmp(24))
                    TotAIS3GServed(k) += Convert.ToDouble(tmp(25))
                    TotAIS3GMissedCall(k) += Convert.ToDouble(tmp(26))
                    TotAIS3GCancelled(k) += Convert.ToDouble(tmp(27))
                    TotAIS3GIncomplete(k) += Convert.ToDouble(CInt(tmp(28)) + CInt(tmp(29)) + CInt(tmp(30)))
                    TotOTC3GRegis(k) += Convert.ToDouble(tmp(31))
                    TotOTC3GServed(k) += Convert.ToDouble(tmp(32))
                    TotOTC3GMissedCall(k) += Convert.ToDouble(tmp(33))
                    TotOTC3GCancelled(k) += Convert.ToDouble(tmp(34))
                    TotOTC3GIncomplete(k) += Convert.ToDouble(CInt(tmp(35)) + CInt(tmp(36)) + CInt(tmp(37)))

                    STotMassRegis(k) += Convert.ToDouble(tmp(3))
                    STotMassServed(k) += Convert.ToDouble(tmp(4))
                    STotMassMissedCall(k) += Convert.ToDouble(tmp(5))
                    STotMassCancelled(k) += Convert.ToDouble(tmp(6))
                    STotMassIncomplete(k) += Convert.ToDouble(CInt(tmp(7)) + CInt(tmp(8)) + CInt(tmp(9)))
                    STotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
                    STotSerenadeServed(k) += Convert.ToDouble(tmp(11))
                    STotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
                    STotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
                    STotSerenadeIncomplete(k) += Convert.ToDouble(CInt(tmp(14)) + CInt(tmp(15)) + CInt(tmp(16)))
                    STotNonRegis(k) += Convert.ToDouble(tmp(17))
                    STotNonServed(k) += Convert.ToDouble(tmp(18))
                    STotNonMissedCall(k) += Convert.ToDouble(tmp(19))
                    STotNonCancelled(k) += Convert.ToDouble(tmp(20))
                    STotNonIncomplete(k) += Convert.ToDouble(CInt(tmp(21)) + CInt(tmp(22)) + CInt(tmp(23)))
                    '++
                    STotAIS3GRegis(k) += Convert.ToDouble(tmp(24))
                    STotAIS3GServed(k) += Convert.ToDouble(tmp(25))
                    STotAIS3GMissedCall(k) += Convert.ToDouble(tmp(26))
                    STotAIS3GCancelled(k) += Convert.ToDouble(tmp(27))
                    STotAIS3GIncomplete(k) += Convert.ToDouble(CInt(tmp(28)) + CInt(tmp(29)) + CInt(tmp(30)))
                    STotOTC3GRegis(k) += Convert.ToDouble(tmp(31))
                    STotOTC3GServed(k) += Convert.ToDouble(tmp(32))
                    STotOTC3GMissedCall(k) += Convert.ToDouble(tmp(33))
                    STotOTC3GCancelled(k) += Convert.ToDouble(tmp(34))
                    STotOTC3GIncomplete(k) += Convert.ToDouble(CInt(tmp(35)) + CInt(tmp(36)) + CInt(tmp(37)))


                    GTotMassRegis(k) += Convert.ToDouble(tmp(3))
                    GTotMassServed(k) += Convert.ToDouble(tmp(4))
                    GTotMassMissedCall(k) += Convert.ToDouble(tmp(5))
                    GTotMassCancelled(k) += Convert.ToDouble(tmp(6))
                    GTotMassIncomplete(k) += Convert.ToDouble(CInt(tmp(7)) + CInt(tmp(8)) + CInt(tmp(9)))
                    GTotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
                    GTotSerenadeServed(k) += Convert.ToDouble(tmp(11))
                    GTotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
                    GTotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
                    GTotSerenadeIncomplete(k) += Convert.ToDouble(CInt(tmp(14)) + CInt(tmp(15)) + CInt(tmp(16)))
                    GTotNonRegis(k) += Convert.ToDouble(tmp(17))
                    GTotNonServed(k) += Convert.ToDouble(tmp(18))
                    GTotNonMissedCall(k) += Convert.ToDouble(tmp(19))
                    GTotNonCancelled(k) += Convert.ToDouble(tmp(20))
                    GTotNonIncomplete(k) += Convert.ToDouble(CInt(tmp(21)) + CInt(tmp(22)) + CInt(tmp(23)))
                    '++
                    GTotAIS3GRegis(k) += Convert.ToDouble(tmp(24))
                    GTotAIS3GServed(k) += Convert.ToDouble(tmp(25))
                    GTotAIS3GMissedCall(k) += Convert.ToDouble(tmp(26))
                    GTotAIS3GCancelled(k) += Convert.ToDouble(tmp(27))
                    GTotAIS3GIncomplete(k) += Convert.ToDouble(CInt(tmp(28)) + CInt(tmp(29)) + CInt(tmp(30)))
                    GTotOTC3GRegis(k) += Convert.ToDouble(tmp(31))
                    GTotOTC3GServed(k) += Convert.ToDouble(tmp(32))
                    GTotOTC3GMissedCall(k) += Convert.ToDouble(tmp(33))
                    GTotOTC3GCancelled(k) += Convert.ToDouble(tmp(34))
                    GTotOTC3GIncomplete(k) += Convert.ToDouble(CInt(tmp(35)) + CInt(tmp(36)) + CInt(tmp(37)))


                    AGTotMassRegis += Convert.ToDouble(tmp(3))
                    AGTotMassServed += Convert.ToDouble(tmp(4))
                    AGTotMassMissedCall += Convert.ToDouble(tmp(5))
                    AGTotMassCancelled += Convert.ToDouble(tmp(6))
                    AGTotMassIncomplete += Convert.ToDouble(CInt(tmp(7)) + CInt(tmp(8)) + CInt(tmp(9)))
                    AGTotSerenadeRegis += Convert.ToDouble(tmp(10))
                    AGTotSerenadeServed += Convert.ToDouble(tmp(11))
                    AGTotSerenadeMissedCall += Convert.ToDouble(tmp(12))
                    AGTotSerenadeCancelled += Convert.ToDouble(tmp(13))
                    AGTotSerenadeIncomplete += Convert.ToDouble(CInt(tmp(14)) + CInt(tmp(15)) + CInt(tmp(16)))
                    AGTotNonRegis += Convert.ToDouble(tmp(17))
                    AGTotNonServed += Convert.ToDouble(tmp(18))
                    AGTotNonMissedCall += Convert.ToDouble(tmp(19))
                    AGTotNonCancelled += Convert.ToDouble(tmp(20))
                    AGTotNonIncomplete += Convert.ToDouble(CInt(tmp(21)) + CInt(tmp(22)) + CInt(tmp(23)))
                    AGTotAIS3GRegis += Convert.ToDouble(tmp(24))
                    AGTotAIS3GServed += Convert.ToDouble(tmp(25))
                    AGTotAIS3GMissedCall += Convert.ToDouble(tmp(26))
                    AGTotAIS3GCancelled += Convert.ToDouble(tmp(27))
                    AGTotAIS3GIncomplete += Convert.ToDouble(CInt(tmp(28)) + CInt(tmp(29)) + CInt(tmp(30)))
                    AGTotOTC3GRegis += Convert.ToDouble(tmp(31))
                    AGTotOTC3GServed += Convert.ToDouble(tmp(32))
                    AGTotOTC3GMissedCall += Convert.ToDouble(tmp(33))
                    AGTotOTC3GCancelled += Convert.ToDouble(tmp(34))
                    AGTotOTC3GIncomplete += Convert.ToDouble(CInt(tmp(35)) + CInt(tmp(36)) + CInt(tmp(37)))

                    dbRetSub.Append("    </tr>")
                    ret_sub(k) += dbRetSub.ToString()
                    k += 1
                    dbRetSub = Nothing
                    '***********************************************
                Next

                '******************* Sub Total ******************
                Dim ChkSubTotal As Boolean = False
                If dt.Rows.Count = i + 1 Then
                    ChkSubTotal = True
                Else
                    If dt.Rows(i).Item("shop_id") <> dt.Rows(i + 1).Item("shop_id") Or CDate(dt.Rows(i).Item("service_date")).ToShortDateString <> CDate(dt.Rows(i + 1).Item("service_date")).ToShortDateString Then
                        ChkSubTotal = True
                        ShopIDGroup = dt.Rows(i + 1).Item("shop_id")
                        DateGroup = CDate(dt.Rows(i + 1).Item("service_date").ToString).ToShortDateString
                    End If
                End If
                If ChkSubTotal = True Then
                    Dim l As Integer = 0
                    For Each ServTmp As String In RowTmp

                        Dim sbTmp As New StringBuilder
                        sbTmp.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                        sbTmp.Append("        <td align='center' colspan='3'>Sub Total</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotMassRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotMassServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotMassMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotMassCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotMassIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotSerenadeRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotSerenadeServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotSerenadeMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotSerenadeCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotSerenadeIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotNonRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotNonServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotNonMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotNonCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotNonIncomplete(l), "###,##0") & "</td>")

                        sbTmp.Append("        <td align='right'>" & Format(TotAIS3GRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotAIS3GServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotAIS3GMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotAIS3GCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotAIS3GIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotOTC3GRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotOTC3GServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotOTC3GMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotOTC3GCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(TotOTC3GIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("    </tr>")

                        ret_sub(l) += sbTmp.ToString
                        l += 1
                        sbTmp = Nothing
                    Next

                    ReDim TotMassRegis(RowTmp.Length)
                    ReDim TotMassServed(RowTmp.Length)
                    ReDim TotMassMissedCall(RowTmp.Length)
                    ReDim TotMassCancelled(RowTmp.Length)
                    ReDim TotMassIncomplete(RowTmp.Length)
                    ReDim TotSerenadeRegis(RowTmp.Length)
                    ReDim TotSerenadeServed(RowTmp.Length)
                    ReDim TotSerenadeMissedCall(RowTmp.Length)
                    ReDim TotSerenadeCancelled(RowTmp.Length)
                    ReDim TotSerenadeIncomplete(RowTmp.Length)
                    ReDim TotNonRegis(RowTmp.Length)
                    ReDim TotNonServed(RowTmp.Length)
                    ReDim TotNonMissedCall(RowTmp.Length)
                    ReDim TotNonCancelled(RowTmp.Length)
                    ReDim TotNonIncomplete(RowTmp.Length)
                    ReDim TotAIS3GRegis(RowTmp.Length)
                    ReDim TotAIS3GServed(RowTmp.Length)
                    ReDim TotAIS3GMissedCall(RowTmp.Length)
                    ReDim TotAIS3GCancelled(RowTmp.Length)
                    ReDim TotAIS3GIncomplete(RowTmp.Length)
                    ReDim TotOTC3GRegis(RowTmp.Length)
                    ReDim TotOTC3GServed(RowTmp.Length)
                    ReDim TotOTC3GMissedCall(RowTmp.Length)
                    ReDim TotOTC3GCancelled(RowTmp.Length)
                    ReDim TotOTC3GIncomplete(RowTmp.Length)
                End If

                '******************* Total ******************
                Dim ChkTotal As Boolean = False
                If dt.Rows.Count = i + 1 Then
                    ChkTotal = True
                Else
                    If dt.Rows(i).Item("shop_id") <> dt.Rows(i + 1).Item("shop_id") Then
                        ChkTotal = True
                    End If
                End If
                If ChkTotal = True Then
                    Dim l As Integer = 0
                    For Each ServTmp As String In RowTmp
                        Dim sbTmp As New StringBuilder
                        sbTmp.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                        sbTmp.Append("        <td align='center' colspan='3'>Total " & dt.Rows(i).Item("shop_name_en").ToString & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotMassRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotMassServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotMassMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotMassCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotMassIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotSerenadeRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotSerenadeServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotSerenadeMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotSerenadeCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotSerenadeIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotNonRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotNonServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotNonMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotNonCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotNonIncomplete(l), "###,##0") & "</td>")

                        sbTmp.Append("        <td align='right'>" & Format(STotAIS3GRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotAIS3GServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotAIS3GMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotAIS3GCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotAIS3GIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotOTC3GRegis(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotOTC3GServed(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotOTC3GMissedCall(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotOTC3GCancelled(l), "###,##0") & "</td>")
                        sbTmp.Append("        <td align='right'>" & Format(STotOTC3GIncomplete(l), "###,##0") & "</td>")
                        sbTmp.Append("    </tr>")
                        ret_sub(l) += sbTmp.ToString
                        l += 1
                        sbTmp = Nothing
                    Next

                    ReDim STotMassRegis(RowTmp.Length)
                    ReDim STotMassServed(RowTmp.Length)
                    ReDim STotMassMissedCall(RowTmp.Length)
                    ReDim STotMassCancelled(RowTmp.Length)
                    ReDim STotMassIncomplete(RowTmp.Length)
                    ReDim STotSerenadeRegis(RowTmp.Length)
                    ReDim STotSerenadeServed(RowTmp.Length)
                    ReDim STotSerenadeMissedCall(RowTmp.Length)
                    ReDim STotSerenadeCancelled(RowTmp.Length)
                    ReDim STotSerenadeIncomplete(RowTmp.Length)
                    ReDim STotNonRegis(RowTmp.Length)
                    ReDim STotNonServed(RowTmp.Length)
                    ReDim STotNonMissedCall(RowTmp.Length)
                    ReDim STotNonCancelled(RowTmp.Length)
                    ReDim STotNonIncomplete(RowTmp.Length)
                    ReDim STotAIS3GRegis(RowTmp.Length)
                    ReDim STotAIS3GServed(RowTmp.Length)
                    ReDim STotAIS3GMissedCall(RowTmp.Length)
                    ReDim STotAIS3GCancelled(RowTmp.Length)
                    ReDim STotAIS3GIncomplete(RowTmp.Length)
                    ReDim STotOTC3GRegis(RowTmp.Length)
                    ReDim STotOTC3GServed(RowTmp.Length)
                    ReDim STotOTC3GMissedCall(RowTmp.Length)
                    ReDim STotOTC3GCancelled(RowTmp.Length)
                    ReDim STotOTC3GIncomplete(RowTmp.Length)
                End If
            Next

            '******************* Grand Total ******************
            retGrandTotal += "    <tr style='background: orange repeat-x top;font-weight: bold;'>"
            retGrandTotal += "        <td align='center' colspan='3'>Grand Total</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotMassRegis, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotMassServed, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotMassMissedCall, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotMassCancelled, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotMassIncomplete, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotSerenadeRegis, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotSerenadeServed, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotSerenadeMissedCall, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotSerenadeCancelled, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotSerenadeIncomplete, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotNonRegis, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotNonServed, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotNonMissedCall, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotNonCancelled, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotNonIncomplete, "###,##0") & "</td>"

            retGrandTotal += "        <td align='right'>" & Format(AGTotAIS3GRegis, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotAIS3GServed, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotAIS3GMissedCall, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotAIS3GCancelled, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotAIS3GIncomplete, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotOTC3GRegis, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotOTC3GServed, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotOTC3GMissedCall, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotOTC3GCancelled, "###,##0") & "</td>"
            retGrandTotal += "        <td align='right'>" & Format(AGTotOTC3GIncomplete, "###,##0") & "</td>"
            retGrandTotal += "    </tr>"
            '***************************************************
        End If

        If ret_sub(0) <> "" Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            For n As Int32 = 0 To ret_sub.Length - 1
                ret.Append(ret_sub(n))
            Next
            ret.Append(retGrandTotal)
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If

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
                Dim G_GSMRegis As Int32 = 0
                Dim G_GSMServe As Int32 = 0
                Dim G_GSMMissCall As Int32 = 0
                Dim G_GSMCancel As Int32 = 0
                Dim G_GSMNotCall As Int32 = 0
                Dim G_GSMNotConfirm As Int32 = 0
                Dim G_GSMNotEnd As Int32 = 0
                Dim G_OTCRegis As Int32 = 0
                Dim G_OTCServe As Int32 = 0
                Dim G_OTCMissCall As Int32 = 0
                Dim G_OTCCancel As Int32 = 0
                Dim G_OTCNotCall As Int32 = 0
                Dim G_OTCNotConfirm As Int32 = 0
                Dim G_OTCNotEnd As Int32 = 0
                Dim G_NonRegis As Int32 = 0
                Dim G_NonServe As Int32 = 0
                Dim G_NonMissCall As Int32 = 0
                Dim G_NonCancel As Int32 = 0
                Dim G_NonNotCall As Int32 = 0
                Dim G_NonNotConfirm As Int32 = 0
                Dim G_NonNotEnd As Int32 = 0
                '++
                Dim G_AIS3GRegis As Int32 = 0
                Dim G_AIS3GServed As Int32 = 0
                Dim G_AIS3GMisscall As Int32 = 0
                Dim G_AIS3GCancel As Int32 = 0
                Dim G_AIS3GNotcall As Int32 = 0
                Dim G_AIS3GNotConfirm As Int32 = 0
                Dim G_AIS3GNotEnd As Int32 = 0
                Dim G_OTC3GRegis As Int32 = 0
                Dim G_OTC3GServed As Int32 = 0
                Dim G_OTC3GMisscall As Int32 = 0
                Dim G_OTC3GCancel As Int32 = 0
                Dim G_OTC3GNotcall As Int32 = 0
                Dim G_OTC3GNotConfirm As Int32 = 0
                Dim G_OTC3GNotEnd As Int32 = 0


                For Each shDr As DataRow In shDt.Rows
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim sDt As New DataTable   'Service
                    sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy
                    If sDt.Rows.Count > 0 Then
                        Dim T_GSMRegis As Int32 = 0
                        Dim T_GSMServe As Int32 = 0
                        Dim T_GSMMissCall As Int32 = 0
                        Dim T_GSMCancel As Int32 = 0
                        Dim T_GSMNotCall As Int32 = 0
                        Dim T_GSMNotConfirm As Int32 = 0
                        Dim T_GSMNotEnd As Int32 = 0
                        Dim T_OTCRegis As Int32 = 0
                        Dim T_OTCServe As Int32 = 0
                        Dim T_OTCMissCall As Int32 = 0
                        Dim T_OTCCancel As Int32 = 0
                        Dim T_OTCNotCall As Int32 = 0
                        Dim T_OTCNotConfirm As Int32 = 0
                        Dim T_OTCNotEnd As Int32 = 0
                        Dim T_NonRegis As Int32 = 0
                        Dim T_NonServe As Int32 = 0
                        Dim T_NonMissCall As Int32 = 0
                        Dim T_NonCancel As Int32 = 0
                        Dim T_NonNotCall As Int32 = 0
                        Dim T_NonNotConfirm As Int32 = 0
                        Dim T_NonNotEnd As Int32 = 0
                        '++
                        Dim T_AIS3GRegis As Int32 = 0
                        Dim T_AIS3GServed As Int32 = 0
                        Dim T_AIS3GMisscall As Int32 = 0
                        Dim T_AIS3GCancel As Int32 = 0
                        Dim T_AIS3GNotcall As Int32 = 0
                        Dim T_AIS3GNotConfirm As Int32 = 0
                        Dim T_AIS3GNotEnd As Int32 = 0
                        Dim T_OTC3GRegis As Int32 = 0
                        Dim T_OTC3GServed As Int32 = 0
                        Dim T_OTC3GMisscall As Int32 = 0
                        Dim T_OTC3GCancel As Int32 = 0
                        Dim T_OTC3GNotcall As Int32 = 0
                        Dim T_OTC3GNotConfirm As Int32 = 0
                        Dim T_OTC3GNotEnd As Int32 = 0


                        For Each sDr As DataRow In sDt.Rows
                            '******************** Header Row *********************
                            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Date</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not End</td>")
                            '++
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not End</td>")
                            ret.Append("    </tr>")

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "'"
                            If dt.DefaultView.Count > 0 Then
                                Dim GSMRegis As Int32 = 0
                                Dim GSMServe As Int32 = 0
                                Dim GSMMissCall As Int32 = 0
                                Dim GSMCancel As Int32 = 0
                                Dim GSMNotCall As Int32 = 0
                                Dim GSMNotConfirm As Int32 = 0
                                Dim GSMNotEnd As Int32 = 0
                                Dim OTCRegis As Int32 = 0
                                Dim OTCServe As Int32 = 0
                                Dim OTCMissCall As Int32 = 0
                                Dim OTCCancel As Int32 = 0
                                Dim OTCNotCall As Int32 = 0
                                Dim OTCNotConfirm As Int32 = 0
                                Dim OTCNotEnd As Int32 = 0
                                Dim NonRegis As Int32 = 0
                                Dim NonServe As Int32 = 0
                                Dim NonMissCall As Int32 = 0
                                Dim NonCancel As Int32 = 0
                                Dim NonNotCall As Int32 = 0
                                Dim NonNotConfirm As Int32 = 0
                                Dim NonNotEnd As Int32 = 0
                                '++
                                Dim AIS3GRegis As Int32 = 0
                                Dim AIS3GServe As Int32 = 0
                                Dim AIS3GMissCall As Int32 = 0
                                Dim AIS3GCancel As Int32 = 0
                                Dim AIS3GNotCall As Int32 = 0
                                Dim AIS3GNotConfirm As Int32 = 0
                                Dim AIS3GNotEnd As Int32 = 0
                                Dim OTC3GRegis As Int32 = 0
                                Dim OTC3GServe As Int32 = 0
                                Dim OTC3GMissCall As Int32 = 0
                                Dim OTC3GCancel As Int32 = 0
                                Dim OTC3GNotCall As Int32 = 0
                                Dim OTC3GNotConfirm As Int32 = 0
                                Dim OTC3GNotEnd As Int32 = 0

                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                                    ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notend"), "#,##0") & "</td>")
                                    '++
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notend"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    'Sub Total
                                    GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    NonRegis += Convert.ToInt32(dr("non_regis"))
                                    NonServe += Convert.ToInt32(dr("non_serve"))
                                    NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    AIS3GServe += Convert.ToInt32(dr("ais3g_serve"))
                                    AIS3GMissCall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    AIS3GNotCall += Convert.ToInt32(dr("ais3g_notcall"))
                                    AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    OTC3GServe += Convert.ToInt32(dr("otc3g_serve"))
                                    OTC3GMissCall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    OTC3GNotCall += Convert.ToInt32(dr("otc3g_notcall"))
                                    OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))

                                    'Total Shop
                                    T_GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    T_GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    T_GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    T_GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    T_GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    T_GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    T_GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    T_OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    T_OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    T_OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    T_OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    T_OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    T_OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    T_OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    T_NonRegis += Convert.ToInt32(dr("non_regis"))
                                    T_NonServe += Convert.ToInt32(dr("non_serve"))
                                    T_NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    T_NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    T_NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    T_NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    T_NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    T_AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    T_AIS3GServed += Convert.ToInt32(dr("ais3g_serve"))
                                    T_AIS3GMisscall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    T_AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    T_AIS3GNotcall += Convert.ToInt32(dr("ais3g_notcall"))
                                    T_AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    T_AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    T_OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    T_OTC3GServed += Convert.ToInt32(dr("otc3g_serve"))
                                    T_OTC3GMisscall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    T_OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    T_OTC3GNotcall += Convert.ToInt32(dr("otc3g_notcall"))
                                    T_OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    T_OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))

                                    'Total Service
                                    G_GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    G_GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    G_GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    G_GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    G_GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    G_GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    G_GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    G_OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    G_OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    G_OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    G_OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    G_OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    G_OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    G_OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    G_NonRegis += Convert.ToInt32(dr("non_regis"))
                                    G_NonServe += Convert.ToInt32(dr("non_serve"))
                                    G_NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    G_NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    G_NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    G_NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    G_NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    G_AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    G_AIS3GServed += Convert.ToInt32(dr("ais3g_serve"))
                                    G_AIS3GMisscall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    G_AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    G_AIS3GNotcall += Convert.ToInt32(dr("ais3g_notcall"))
                                    G_AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    G_AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    G_OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    G_OTC3GServed += Convert.ToInt32(dr("otc3g_serve"))
                                    G_OTC3GMisscall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    G_OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    G_OTC3GNotcall += Convert.ToInt32(dr("otc3g_notcall"))
                                    G_OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    G_OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))
                                Next

                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                                ret.Append("        <td align='left'>Sub Total</td>")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotEnd, "#,##0") & "</td>")
                                '++
                                ret.Append("        <td align='right'>" & Format(AIS3GRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotEnd, "#,##0") & "</td>")
                                ret.Append("    </tr>")
                            End If
                            dt.DefaultView.RowFilter = ""
                        Next
                        'Total by Shop
                        ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='left'>Total</td>")
                        ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotEnd, "#,##0") & "</td>")
                        '++
                        ret.Append("        <td align='right'>" & Format(T_AIS3GRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GMisscall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotcall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GMisscall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotcall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotEnd, "#,##0") & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left'>Grand Total</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotEnd, "#,##0") & "</td>")
                '++
                ret.Append("        <td align='right'>" & Format(G_AIS3GRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GMisscall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotcall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GMisscall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotcall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotEnd, "#,##0") & "</td>")
                ret.Append("    </tr>")
            End If
            shDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")

            Dim shDt As New DataTable  'Shop
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            If shDt.Rows.Count > 0 Then
                Dim G_GSMRegis As Int32 = 0
                Dim G_GSMServe As Int32 = 0
                Dim G_GSMMissCall As Int32 = 0
                Dim G_GSMCancel As Int32 = 0
                Dim G_GSMNotCall As Int32 = 0
                Dim G_GSMNotConfirm As Int32 = 0
                Dim G_GSMNotEnd As Int32 = 0
                Dim G_OTCRegis As Int32 = 0
                Dim G_OTCServe As Int32 = 0
                Dim G_OTCMissCall As Int32 = 0
                Dim G_OTCCancel As Int32 = 0
                Dim G_OTCNotCall As Int32 = 0
                Dim G_OTCNotConfirm As Int32 = 0
                Dim G_OTCNotEnd As Int32 = 0
                Dim G_NonRegis As Int32 = 0
                Dim G_NonServe As Int32 = 0
                Dim G_NonMissCall As Int32 = 0
                Dim G_NonCancel As Int32 = 0
                Dim G_NonNotCall As Int32 = 0
                Dim G_NonNotConfirm As Int32 = 0
                Dim G_NonNotEnd As Int32 = 0
                '++
                Dim G_AIS3GRegis As Int32 = 0
                Dim G_AIS3GServed As Int32 = 0
                Dim G_AIS3GMisscall As Int32 = 0
                Dim G_AIS3GCancel As Int32 = 0
                Dim G_AIS3GNotcall As Int32 = 0
                Dim G_AIS3GNotConfirm As Int32 = 0
                Dim G_AIS3GNotEnd As Int32 = 0
                Dim G_OTC3GRegis As Int32 = 0
                Dim G_OTC3GServed As Int32 = 0
                Dim G_OTC3GMisscall As Int32 = 0
                Dim G_OTC3GCancel As Int32 = 0
                Dim G_OTC3GNotcall As Int32 = 0
                Dim G_OTC3GNotConfirm As Int32 = 0
                Dim G_OTC3GNotEnd As Int32 = 0

                For Each shDr As DataRow In shDt.Rows
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim sDt As New DataTable   'Service
                    sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy
                    If sDt.Rows.Count > 0 Then
                        Dim T_GSMRegis As Int32 = 0
                        Dim T_GSMServe As Int32 = 0
                        Dim T_GSMMissCall As Int32 = 0
                        Dim T_GSMCancel As Int32 = 0
                        Dim T_GSMNotCall As Int32 = 0
                        Dim T_GSMNotConfirm As Int32 = 0
                        Dim T_GSMNotEnd As Int32 = 0
                        Dim T_OTCRegis As Int32 = 0
                        Dim T_OTCServe As Int32 = 0
                        Dim T_OTCMissCall As Int32 = 0
                        Dim T_OTCCancel As Int32 = 0
                        Dim T_OTCNotCall As Int32 = 0
                        Dim T_OTCNotConfirm As Int32 = 0
                        Dim T_OTCNotEnd As Int32 = 0
                        Dim T_NonRegis As Int32 = 0
                        Dim T_NonServe As Int32 = 0
                        Dim T_NonMissCall As Int32 = 0
                        Dim T_NonCancel As Int32 = 0
                        Dim T_NonNotCall As Int32 = 0
                        Dim T_NonNotConfirm As Int32 = 0
                        Dim T_NonNotEnd As Int32 = 0
                        '++
                        Dim T_AIS3GRegis As Int32 = 0
                        Dim T_AIS3GServed As Int32 = 0
                        Dim T_AIS3GMisscall As Int32 = 0
                        Dim T_AIS3GCancel As Int32 = 0
                        Dim T_AIS3GNotcall As Int32 = 0
                        Dim T_AIS3GNotConfirm As Int32 = 0
                        Dim T_AIS3GNotEnd As Int32 = 0
                        Dim T_OTC3GRegis As Int32 = 0
                        Dim T_OTC3GServed As Int32 = 0
                        Dim T_OTC3GMisscall As Int32 = 0
                        Dim T_OTC3GCancel As Int32 = 0
                        Dim T_OTC3GNotcall As Int32 = 0
                        Dim T_OTC3GNotConfirm As Int32 = 0
                        Dim T_OTC3GNotEnd As Int32 = 0

                        For Each sDr As DataRow In sDt.Rows
                            '******************** Header Row *********************
                            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Week No.</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Year</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not End</td>")
                            '++
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not End</td>")
                            ret.Append("    </tr>")

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "'"
                            If dt.DefaultView.Count > 0 Then
                                Dim GSMRegis As Int32 = 0
                                Dim GSMServe As Int32 = 0
                                Dim GSMMissCall As Int32 = 0
                                Dim GSMCancel As Int32 = 0
                                Dim GSMNotCall As Int32 = 0
                                Dim GSMNotConfirm As Int32 = 0
                                Dim GSMNotEnd As Int32 = 0
                                Dim OTCRegis As Int32 = 0
                                Dim OTCServe As Int32 = 0
                                Dim OTCMissCall As Int32 = 0
                                Dim OTCCancel As Int32 = 0
                                Dim OTCNotCall As Int32 = 0
                                Dim OTCNotConfirm As Int32 = 0
                                Dim OTCNotEnd As Int32 = 0
                                Dim NonRegis As Int32 = 0
                                Dim NonServe As Int32 = 0
                                Dim NonMissCall As Int32 = 0
                                Dim NonCancel As Int32 = 0
                                Dim NonNotCall As Int32 = 0
                                Dim NonNotConfirm As Int32 = 0
                                Dim NonNotEnd As Int32 = 0
                                '++
                                Dim AIS3GRegis As Int32 = 0
                                Dim AIS3GServe As Int32 = 0
                                Dim AIS3GMissCall As Int32 = 0
                                Dim AIS3GCancel As Int32 = 0
                                Dim AIS3GNotCall As Int32 = 0
                                Dim AIS3GNotConfirm As Int32 = 0
                                Dim AIS3GNotEnd As Int32 = 0
                                Dim OTC3GRegis As Int32 = 0
                                Dim OTC3GServe As Int32 = 0
                                Dim OTC3GMissCall As Int32 = 0
                                Dim OTC3GCancel As Int32 = 0
                                Dim OTC3GNotCall As Int32 = 0
                                Dim OTC3GNotConfirm As Int32 = 0
                                Dim OTC3GNotEnd As Int32 = 0

                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='left'>" & dr("week_of_year") & "</td>")
                                    ret.Append("        <td align='left'>" & dr("show_year") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notend"), "#,##0") & "</td>")
                                    '++
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notend"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    'Sub Total
                                    GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    NonRegis += Convert.ToInt32(dr("non_regis"))
                                    NonServe += Convert.ToInt32(dr("non_serve"))
                                    NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    AIS3GServe += Convert.ToInt32(dr("ais3g_serve"))
                                    AIS3GMissCall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    AIS3GNotCall += Convert.ToInt32(dr("ais3g_notcall"))
                                    AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    OTC3GServe += Convert.ToInt32(dr("otc3g_serve"))
                                    OTC3GMissCall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    OTC3GNotCall += Convert.ToInt32(dr("otc3g_notcall"))
                                    OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))

                                    'Total Shop
                                    T_GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    T_GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    T_GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    T_GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    T_GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    T_GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    T_GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    T_OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    T_OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    T_OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    T_OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    T_OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    T_OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    T_OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    T_NonRegis += Convert.ToInt32(dr("non_regis"))
                                    T_NonServe += Convert.ToInt32(dr("non_serve"))
                                    T_NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    T_NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    T_NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    T_NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    T_NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    T_AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    T_AIS3GServed += Convert.ToInt32(dr("ais3g_serve"))
                                    T_AIS3GMisscall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    T_AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    T_AIS3GNotcall += Convert.ToInt32(dr("ais3g_notcall"))
                                    T_AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    T_AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    T_OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    T_OTC3GServed += Convert.ToInt32(dr("otc3g_serve"))
                                    T_OTC3GMisscall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    T_OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    T_OTC3GNotcall += Convert.ToInt32(dr("otc3g_notcall"))
                                    T_OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    T_OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))

                                    'Total Service
                                    G_GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    G_GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    G_GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    G_GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    G_GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    G_GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    G_GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    G_OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    G_OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    G_OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    G_OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    G_OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    G_OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    G_OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    G_NonRegis += Convert.ToInt32(dr("non_regis"))
                                    G_NonServe += Convert.ToInt32(dr("non_serve"))
                                    G_NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    G_NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    G_NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    G_NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    G_NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    G_AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    G_AIS3GServed += Convert.ToInt32(dr("ais3g_serve"))
                                    G_AIS3GMisscall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    G_AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    G_AIS3GNotcall += Convert.ToInt32(dr("ais3g_notcall"))
                                    G_AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    G_AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    G_OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    G_OTC3GServed += Convert.ToInt32(dr("otc3g_serve"))
                                    G_OTC3GMisscall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    G_OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    G_OTC3GNotcall += Convert.ToInt32(dr("otc3g_notcall"))
                                    G_OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    G_OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))
                                Next

                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>Sub Total</td>")
                                ret.Append("        <td align='left'>&nbsp;</td>")
                                ret.Append("        <td align='right'>" & Format(GSMRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotEnd, "#,##0") & "</td>")
                                '++
                                ret.Append("        <td align='right'>" & Format(AIS3GRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotEnd, "#,##0") & "</td>")
                                ret.Append("    </tr>")
                            End If
                            dt.DefaultView.RowFilter = ""
                        Next
                        'Total by Shop
                        ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>Total</td>")
                        ret.Append("        <td align='left'>&nbsp;</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotEnd, "#,##0") & "</td>")
                        '++
                        ret.Append("        <td align='right'>" & Format(T_AIS3GRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GMisscall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotcall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GMisscall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotcall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotEnd, "#,##0") & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left'>Grand Total</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotEnd, "#,##0") & "</td>")
                '++
                ret.Append("        <td align='right'>" & Format(G_AIS3GRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GMisscall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotcall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GMisscall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotcall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotEnd, "#,##0") & "</td>")
                ret.Append("    </tr>")
            End If
            shDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        Dim ret As New StringBuilder
        If dt.Rows.Count > 0 Then
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")

            Dim shDt As New DataTable  'Shop
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
            If shDt.Rows.Count > 0 Then
                Dim G_GSMRegis As Int32 = 0
                Dim G_GSMServe As Int32 = 0
                Dim G_GSMMissCall As Int32 = 0
                Dim G_GSMCancel As Int32 = 0
                Dim G_GSMNotCall As Int32 = 0
                Dim G_GSMNotConfirm As Int32 = 0
                Dim G_GSMNotEnd As Int32 = 0
                Dim G_OTCRegis As Int32 = 0
                Dim G_OTCServe As Int32 = 0
                Dim G_OTCMissCall As Int32 = 0
                Dim G_OTCCancel As Int32 = 0
                Dim G_OTCNotCall As Int32 = 0
                Dim G_OTCNotConfirm As Int32 = 0
                Dim G_OTCNotEnd As Int32 = 0
                Dim G_NonRegis As Int32 = 0
                Dim G_NonServe As Int32 = 0
                Dim G_NonMissCall As Int32 = 0
                Dim G_NonCancel As Int32 = 0
                Dim G_NonNotCall As Int32 = 0
                Dim G_NonNotConfirm As Int32 = 0
                Dim G_NonNotEnd As Int32 = 0
                '++
                Dim G_AIS3GRegis As Int32 = 0
                Dim G_AIS3GServed As Int32 = 0
                Dim G_AIS3GMisscall As Int32 = 0
                Dim G_AIS3GCancel As Int32 = 0
                Dim G_AIS3GNotcall As Int32 = 0
                Dim G_AIS3GNotConfirm As Int32 = 0
                Dim G_AIS3GNotEnd As Int32 = 0
                Dim G_OTC3GRegis As Int32 = 0
                Dim G_OTC3GServed As Int32 = 0
                Dim G_OTC3GMisscall As Int32 = 0
                Dim G_OTC3GCancel As Int32 = 0
                Dim G_OTC3GNotcall As Int32 = 0
                Dim G_OTC3GNotConfirm As Int32 = 0
                Dim G_OTC3GNotEnd As Int32 = 0

                For Each shDr As DataRow In shDt.Rows
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim sDt As New DataTable   'Service
                    sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th").Copy
                    If sDt.Rows.Count > 0 Then
                        Dim T_GSMRegis As Int32 = 0
                        Dim T_GSMServe As Int32 = 0
                        Dim T_GSMMissCall As Int32 = 0
                        Dim T_GSMCancel As Int32 = 0
                        Dim T_GSMNotCall As Int32 = 0
                        Dim T_GSMNotConfirm As Int32 = 0
                        Dim T_GSMNotEnd As Int32 = 0
                        Dim T_OTCRegis As Int32 = 0
                        Dim T_OTCServe As Int32 = 0
                        Dim T_OTCMissCall As Int32 = 0
                        Dim T_OTCCancel As Int32 = 0
                        Dim T_OTCNotCall As Int32 = 0
                        Dim T_OTCNotConfirm As Int32 = 0
                        Dim T_OTCNotEnd As Int32 = 0
                        Dim T_NonRegis As Int32 = 0
                        Dim T_NonServe As Int32 = 0
                        Dim T_NonMissCall As Int32 = 0
                        Dim T_NonCancel As Int32 = 0
                        Dim T_NonNotCall As Int32 = 0
                        Dim T_NonNotConfirm As Int32 = 0
                        Dim T_NonNotEnd As Int32 = 0
                        '++
                        Dim T_AIS3GRegis As Int32 = 0
                        Dim T_AIS3GServed As Int32 = 0
                        Dim T_AIS3GMisscall As Int32 = 0
                        Dim T_AIS3GCancel As Int32 = 0
                        Dim T_AIS3GNotcall As Int32 = 0
                        Dim T_AIS3GNotConfirm As Int32 = 0
                        Dim T_AIS3GNotEnd As Int32 = 0
                        Dim T_OTC3GRegis As Int32 = 0
                        Dim T_OTC3GServed As Int32 = 0
                        Dim T_OTC3GMisscall As Int32 = 0
                        Dim T_OTC3GCancel As Int32 = 0
                        Dim T_OTC3GNotcall As Int32 = 0
                        Dim T_OTC3GNotConfirm As Int32 = 0
                        Dim T_OTC3GNotEnd As Int32 = 0

                        For Each sDr As DataRow In sDt.Rows
                            '******************** Header Row *********************
                            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                            ret.Append("        <td align='center' style='color: #ffffff;width:100px' >Shop Name</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Month</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;'>Year</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " GSM Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " 1-2-Call Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " Non Mobile Not End</td>")
                            '++
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " AIS3G Not End</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >" & sDr("service_name_en") & " OTC3G Not End</td>")
                            ret.Append("    </tr>")

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "'"
                            If dt.DefaultView.Count > 0 Then
                                Dim GSMRegis As Int32 = 0
                                Dim GSMServe As Int32 = 0
                                Dim GSMMissCall As Int32 = 0
                                Dim GSMCancel As Int32 = 0
                                Dim GSMNotCall As Int32 = 0
                                Dim GSMNotConfirm As Int32 = 0
                                Dim GSMNotEnd As Int32 = 0
                                Dim OTCRegis As Int32 = 0
                                Dim OTCServe As Int32 = 0
                                Dim OTCMissCall As Int32 = 0
                                Dim OTCCancel As Int32 = 0
                                Dim OTCNotCall As Int32 = 0
                                Dim OTCNotConfirm As Int32 = 0
                                Dim OTCNotEnd As Int32 = 0
                                Dim NonRegis As Int32 = 0
                                Dim NonServe As Int32 = 0
                                Dim NonMissCall As Int32 = 0
                                Dim NonCancel As Int32 = 0
                                Dim NonNotCall As Int32 = 0
                                Dim NonNotConfirm As Int32 = 0
                                Dim NonNotEnd As Int32 = 0
                                '++
                                Dim AIS3GRegis As Int32 = 0
                                Dim AIS3GServe As Int32 = 0
                                Dim AIS3GMissCall As Int32 = 0
                                Dim AIS3GCancel As Int32 = 0
                                Dim AIS3GNotCall As Int32 = 0
                                Dim AIS3GNotConfirm As Int32 = 0
                                Dim AIS3GNotEnd As Int32 = 0
                                Dim OTC3GRegis As Int32 = 0
                                Dim OTC3GServe As Int32 = 0
                                Dim OTC3GMissCall As Int32 = 0
                                Dim OTC3GCancel As Int32 = 0
                                Dim OTC3GNotCall As Int32 = 0
                                Dim OTC3GNotConfirm As Int32 = 0
                                Dim OTC3GNotEnd As Int32 = 0

                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='left'>" & dr("show_month") & "</td>")
                                    ret.Append("        <td align='left'>" & dr("show_year") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gsm_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_notend"), "#,##0") & "</td>")
                                    '++
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais3g_notend"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notcall"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notcon"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("otc3g_notend"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    'Sub Total
                                    GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    NonRegis += Convert.ToInt32(dr("non_regis"))
                                    NonServe += Convert.ToInt32(dr("non_serve"))
                                    NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    AIS3GServe += Convert.ToInt32(dr("ais3g_serve"))
                                    AIS3GMissCall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    AIS3GNotCall += Convert.ToInt32(dr("ais3g_notcall"))
                                    AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    OTC3GServe += Convert.ToInt32(dr("otc3g_serve"))
                                    OTC3GMissCall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    OTC3GNotCall += Convert.ToInt32(dr("otc3g_notcall"))
                                    OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))

                                    'Total Shop
                                    T_GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    T_GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    T_GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    T_GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    T_GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    T_GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    T_GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    T_OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    T_OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    T_OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    T_OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    T_OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    T_OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    T_OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    T_NonRegis += Convert.ToInt32(dr("non_regis"))
                                    T_NonServe += Convert.ToInt32(dr("non_serve"))
                                    T_NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    T_NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    T_NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    T_NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    T_NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    T_AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    T_AIS3GServed += Convert.ToInt32(dr("ais3g_serve"))
                                    T_AIS3GMisscall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    T_AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    T_AIS3GNotcall += Convert.ToInt32(dr("ais3g_notcall"))
                                    T_AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    T_AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    T_OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    T_OTC3GServed += Convert.ToInt32(dr("otc3g_serve"))
                                    T_OTC3GMisscall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    T_OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    T_OTC3GNotcall += Convert.ToInt32(dr("otc3g_notcall"))
                                    T_OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    T_OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))

                                    'Total Service
                                    G_GSMRegis += Convert.ToInt32(dr("gsm_regis"))
                                    G_GSMServe += Convert.ToInt32(dr("gsm_serve"))
                                    G_GSMMissCall += Convert.ToInt32(dr("gsm_miss_call"))
                                    G_GSMCancel += Convert.ToInt32(dr("gsm_cancel"))
                                    G_GSMNotCall += Convert.ToInt32(dr("gsm_notcall"))
                                    G_GSMNotConfirm += Convert.ToInt32(dr("gsm_notcon"))
                                    G_GSMNotEnd += Convert.ToInt32(dr("gsm_notend"))
                                    G_OTCRegis += Convert.ToInt32(dr("otc_regis"))
                                    G_OTCServe += Convert.ToInt32(dr("otc_serve"))
                                    G_OTCMissCall += Convert.ToInt32(dr("otc_miss_call"))
                                    G_OTCCancel += Convert.ToInt32(dr("otc_cancel"))
                                    G_OTCNotCall += Convert.ToInt32(dr("otc_notcall"))
                                    G_OTCNotConfirm += Convert.ToInt32(dr("otc_notcon"))
                                    G_OTCNotEnd += Convert.ToInt32(dr("otc_notend"))
                                    G_NonRegis += Convert.ToInt32(dr("non_regis"))
                                    G_NonServe += Convert.ToInt32(dr("non_serve"))
                                    G_NonMissCall += Convert.ToInt32(dr("non_miss_call"))
                                    G_NonCancel += Convert.ToInt32(dr("non_cancel"))
                                    G_NonNotCall += Convert.ToInt32(dr("non_notcall"))
                                    G_NonNotConfirm += Convert.ToInt32(dr("non_notcon"))
                                    G_NonNotEnd += Convert.ToInt32(dr("non_notend"))
                                    '++
                                    G_AIS3GRegis += Convert.ToInt32(dr("ais3g_regis"))
                                    G_AIS3GServed += Convert.ToInt32(dr("ais3g_serve"))
                                    G_AIS3GMisscall += Convert.ToInt32(dr("ais3g_miss_call"))
                                    G_AIS3GCancel += Convert.ToInt32(dr("ais3g_cancel"))
                                    G_AIS3GNotcall += Convert.ToInt32(dr("ais3g_notcall"))
                                    G_AIS3GNotConfirm += Convert.ToInt32(dr("ais3g_notcon"))
                                    G_AIS3GNotEnd += Convert.ToInt32(dr("ais3g_notend"))
                                    G_OTC3GRegis += Convert.ToInt32(dr("otc3g_regis"))
                                    G_OTC3GServed += Convert.ToInt32(dr("otc3g_serve"))
                                    G_OTC3GMisscall += Convert.ToInt32(dr("otc3g_miss_call"))
                                    G_OTC3GCancel += Convert.ToInt32(dr("otc3g_cancel"))
                                    G_OTC3GNotcall += Convert.ToInt32(dr("otc3g_notcall"))
                                    G_OTC3GNotConfirm += Convert.ToInt32(dr("otc3g_notcon"))
                                    G_OTC3GNotEnd += Convert.ToInt32(dr("otc3g_notend"))
                                Next

                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;' >")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>Sub Total</td>")
                                ret.Append("        <td align='left'>&nbsp;</td>")
                                ret.Append("        <td align='right'>" & Format(GSMRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(GSMNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTCNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(NonNotEnd, "#,##0") & "</td>")
                                '++
                                ret.Append("        <td align='right'>" & Format(AIS3GRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(AIS3GNotEnd, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GRegis, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GServe, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GMissCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GCancel, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotCall, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotConfirm, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(OTC3GNotEnd, "#,##0") & "</td>")
                                ret.Append("    </tr>")
                            End If
                            dt.DefaultView.RowFilter = ""
                        Next
                        'Total by Shop
                        ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>Total</td>")
                        ret.Append("        <td align='left'>&nbsp;</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_GSMNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTCNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonServe, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonMissCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotCall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_NonNotEnd, "#,##0") & "</td>")
                        '++
                        ret.Append("        <td align='right'>" & Format(T_AIS3GRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GMisscall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotcall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_AIS3GNotEnd, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GRegis, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GServed, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GMisscall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GCancel, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotcall, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotConfirm, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(T_OTC3GNotEnd, "#,##0") & "</td>")
                        ret.Append("    </tr>")
                    End If
                Next

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='left'>Grand Total</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='left'>&nbsp;</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_GSMNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTCNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonServe, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonMissCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotCall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_NonNotEnd, "#,##0") & "</td>")
                '++
                ret.Append("        <td align='right'>" & Format(G_AIS3GRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GMisscall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotcall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_AIS3GNotEnd, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GRegis, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GServed, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GMisscall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GCancel, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotcall, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotConfirm, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(G_OTC3GNotEnd, "#,##0") & "</td>")
                ret.Append("    </tr>")
            End If
            shDt.Dispose()
            ret.Append("</table>")

            lblReportDesc.Text = ret.ToString
            lblerror.Visible = False
        End If
    End Sub
    'Private Sub RenderReportByDay(ByVal dt As DataTable)
    '    Dim ret As String = ""
    '    If dt.Rows.Count > 0 Then
    '        Dim RowTmp() As String = {}
    '        Dim ShopIDGroup As Int32 = 0
    '        Dim TotMassRegis() As Double
    '        Dim TotMassServed() As Double
    '        Dim TotMassMissedCall() As Double
    '        Dim TotMassCancelled() As Double
    '        Dim TotMassNotCall() As Double
    '        Dim TotMassNotConfirm() As Double
    '        Dim TotMassNotEnd() As Double
    '        Dim TotSerenadeRegis() As Double
    '        Dim TotSerenadeServed() As Double
    '        Dim TotSerenadeMissedCall() As Double
    '        Dim TotSerenadeCancelled() As Double
    '        Dim TotSerenadeNotCall() As Double
    '        Dim TotSerenadeNotConfirm() As Double
    '        Dim TotSerenadeNotEnd() As Double
    '        Dim TotNonRegis() As Double
    '        Dim TotNonServed() As Double
    '        Dim TotNonMissedCall() As Double
    '        Dim TotNonCancelled() As Double
    '        Dim TotNonNotCall() As Double
    '        Dim TotNonNotConfirm() As Double
    '        Dim TotNonNotEnd() As Double

    '        Dim GTotMassRegis() As Double
    '        Dim GTotMassServed() As Double
    '        Dim GTotMassMissedCall() As Double
    '        Dim GTotMassCancelled() As Double
    '        Dim GTotMassNotCall() As Double
    '        Dim GTotMassNotConfirm() As Double
    '        Dim GTotMassNotEnd() As Double
    '        Dim GTotSerenadeRegis() As Double
    '        Dim GTotSerenadeServed() As Double
    '        Dim GTotSerenadeMissedCall() As Double
    '        Dim GTotSerenadeCancelled() As Double
    '        Dim GTotSerenadeNotCall() As Double
    '        Dim GTotSerenadeNotConfirm() As Double
    '        Dim GTotSerenadeNotEnd() As Double
    '        Dim GTotNonRegis() As Double
    '        Dim GTotNonServed() As Double
    '        Dim GTotNonMissedCall() As Double
    '        Dim GTotNonCancelled() As Double
    '        Dim GTotNonNotCall() As Double
    '        Dim GTotNonNotConfirm() As Double
    '        Dim GTotNonNotEnd() As Double

    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(i)
    '            If i = 0 Then
    '                'Header Row
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Day</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Week</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Year</td>"

    '                'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
    '                RowTmp = Split(dr("data_value"), "###")
    '                For Each ServTmp As String In RowTmp
    '                    Dim tmp() As String = Split(ServTmp, "|")
    '                    ret += "        <td colspan='14' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>"
    '                Next
    '                'ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Total</td>"
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)

    '                ReDim GTotMassRegis(RowTmp.Length)
    '                ReDim GTotMassServed(RowTmp.Length)
    '                ReDim GTotMassMissedCall(RowTmp.Length)
    '                ReDim GTotMassCancelled(RowTmp.Length)
    '                ReDim GTotMassNotCall(RowTmp.Length)
    '                ReDim GTotMassNotConfirm(RowTmp.Length)
    '                ReDim GTotMassNotEnd(RowTmp.Length)
    '                ReDim GTotSerenadeRegis(RowTmp.Length)
    '                ReDim GTotSerenadeServed(RowTmp.Length)
    '                ReDim GTotSerenadeMissedCall(RowTmp.Length)
    '                ReDim GTotSerenadeCancelled(RowTmp.Length)
    '                ReDim GTotSerenadeNotCall(RowTmp.Length)
    '                ReDim GTotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim GTotSerenadeNotEnd(RowTmp.Length)

    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>GSM</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>1-2-Call</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>Non Mobile</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                Next

    '                ret += "    </tr>"
    '            End If
    '            '******************* Sub Total ******************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='4'>Sub Total</td>"
    '                Dim o As Integer = 0
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='right'>" & TotMassRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotEnd(o) & "</td>"
    '                    o += 1
    '                Next
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)

    '            End If
    '            '***********************************************

    '            'Data Row
    '            ret += "    <tr>"
    '            ret += "        <td align='left'>" & dr("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dr("show_day") & "</td>"
    '            ret += "        <td align='left'>" & "W" & dr("show_week") & "</td>"
    '            ret += "        <td align='left'>" & dr("show_year") & "</td>"

    '            Dim k As Integer = 0
    '            Dim TotalCal As Long = 0
    '            RowTmp = Split(dr("data_value"), "###")
    '            For Each ServTmp As String In RowTmp
    '                Dim tmp() As String = Split(ServTmp, "|")
    '                'Dim MassIncomp As Double = Convert.ToInt64(tmp(3)) - (Convert.ToInt64(tmp(4)) + Convert.ToInt64(tmp(5)))
    '                'Dim SerenadeIncomp As Double = Convert.ToInt64(tmp(6)) - (Convert.ToInt64(tmp(7)) + Convert.ToInt64(tmp(8)))

    '                ret += "        <td align='right'>" & tmp(3) & "</td>"
    '                ret += "        <td align='right'>" & tmp(4) & "</td>"
    '                ret += "        <td align='right'>" & tmp(5) & "</td>"
    '                ret += "        <td align='right'>" & tmp(6) & "</td>"
    '                ret += "        <td align='right'>" & tmp(7) & "</td>"
    '                ret += "        <td align='right'>" & tmp(8) & "</td>"
    '                ret += "        <td align='right'>" & tmp(9) & "</td>"
    '                ret += "        <td align='right'>" & tmp(10) & "</td>"
    '                ret += "        <td align='right'>" & tmp(11) & "</td>"
    '                ret += "        <td align='right'>" & tmp(12) & "</td>"
    '                ret += "        <td align='right'>" & tmp(13) & "</td>"
    '                ret += "        <td align='right'>" & tmp(14) & "</td>"
    '                ret += "        <td align='right'>" & tmp(15) & "</td>"
    '                ret += "        <td align='right'>" & tmp(16) & "</td>"
    '                ret += "        <td align='right'>" & tmp(17) & "</td>"
    '                ret += "        <td align='right'>" & tmp(18) & "</td>"
    '                ret += "        <td align='right'>" & tmp(19) & "</td>"
    '                ret += "        <td align='right'>" & tmp(20) & "</td>"
    '                ret += "        <td align='right'>" & tmp(21) & "</td>"
    '                ret += "        <td align='right'>" & tmp(22) & "</td>"
    '                ret += "        <td align='right'>" & tmp(23) & "</td>"

    '                TotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                TotMassServed(k) += Convert.ToDouble(tmp(4))
    '                TotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                TotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                TotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                TotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                TotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                TotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                TotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                TotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                TotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                TotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                TotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                TotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                TotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                TotNonServed(k) += Convert.ToDouble(tmp(18))
    '                TotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                TotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                TotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                TotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                TotNonNotEnd(k) += Convert.ToDouble(tmp(23))

    '                GTotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                GTotMassServed(k) += Convert.ToDouble(tmp(4))
    '                GTotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                GTotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                GTotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                GTotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                GTotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                GTotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                GTotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                GTotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                GTotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                GTotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                GTotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                GTotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                GTotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                GTotNonServed(k) += Convert.ToDouble(tmp(18))
    '                GTotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                GTotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                GTotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                GTotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                GTotNonNotEnd(k) += Convert.ToDouble(tmp(23))
    '                k += 1
    '            Next
    '            'ret += "        <td align='right'>" & TotalCal & "</td>"
    '            ret += "    </tr>"
    '        Next

    '        '******************** Total ********************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '        Dim n As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & TotMassRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotEnd(n) & "</td>"
    '            n += 1
    '        Next
    '        ReDim TotMassRegis(RowTmp.Length)
    '        ReDim TotMassServed(RowTmp.Length)
    '        ReDim TotMassMissedCall(RowTmp.Length)
    '        ReDim TotMassCancelled(RowTmp.Length)
    '        ReDim TotMassNotCall(RowTmp.Length)
    '        ReDim TotMassNotConfirm(RowTmp.Length)
    '        ReDim TotMassNotEnd(RowTmp.Length)
    '        ReDim TotSerenadeRegis(RowTmp.Length)
    '        ReDim TotSerenadeServed(RowTmp.Length)
    '        ReDim TotSerenadeMissedCall(RowTmp.Length)
    '        ReDim TotSerenadeCancelled(RowTmp.Length)
    '        ReDim TotSerenadeNotCall(RowTmp.Length)
    '        ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '        ReDim TotSerenadeNotEnd(RowTmp.Length)
    '        ReDim TotNonRegis(RowTmp.Length)
    '        ReDim TotNonServed(RowTmp.Length)
    '        ReDim TotNonMissedCall(RowTmp.Length)
    '        ReDim TotNonCancelled(RowTmp.Length)
    '        ReDim TotNonNotCall(RowTmp.Length)
    '        ReDim TotNonNotConfirm(RowTmp.Length)
    '        ReDim TotNonNotEnd(RowTmp.Length)

    '        ret += "    </tr>"
    '        '***********************************************
    '        '***************** Grand Total *****************
    '        Dim GrandTot As Long = 0
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Total</td>"
    '        Dim m As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & GTotMassRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotEnd(m) & "</td>"
    '            GrandTot += GTotMassRegis(m) + GTotSerenadeRegis(m) + GTotNonRegis(m)
    '            m += 1
    '        Next
    '        ret += "    </tr>"
    '        '***********************************************
    '        ret += "</table>"

    '        dt.Dispose()
    '        dt = Nothing
    '    End If

    '    If ret.Trim <> "" Then
    '        lblReportDesc.Text = ret
    '        lblerror.Visible = False
    '    End If
    'End Sub
    'Private Sub RenderReportByWeek(ByVal dt As DataTable)
    '    Dim ret As String = ""
    '    If dt.Rows.Count > 0 Then
    '        Dim RowTmp() As String = {}

    '        Dim TotMassRegis() As Double
    '        Dim TotMassServed() As Double
    '        Dim TotMassMissedCall() As Double
    '        Dim TotMassCancelled() As Double
    '        Dim TotMassNotCall() As Double
    '        Dim TotMassNotConfirm() As Double
    '        Dim TotMassNotEnd() As Double
    '        Dim TotSerenadeRegis() As Double
    '        Dim TotSerenadeServed() As Double
    '        Dim TotSerenadeMissedCall() As Double
    '        Dim TotSerenadeCancelled() As Double
    '        Dim TotSerenadeNotCall() As Double
    '        Dim TotSerenadeNotConfirm() As Double
    '        Dim TotSerenadeNotEnd() As Double
    '        Dim TotNonRegis() As Double
    '        Dim TotNonServed() As Double
    '        Dim TotNonMissedCall() As Double
    '        Dim TotNonCancelled() As Double
    '        Dim TotNonNotCall() As Double
    '        Dim TotNonNotConfirm() As Double
    '        Dim TotNonNotEnd() As Double

    '        Dim GTotMassRegis() As Double
    '        Dim GTotMassServed() As Double
    '        Dim GTotMassMissedCall() As Double
    '        Dim GTotMassCancelled() As Double
    '        Dim GTotMassNotCall() As Double
    '        Dim GTotMassNotConfirm() As Double
    '        Dim GTotMassNotEnd() As Double
    '        Dim GTotSerenadeRegis() As Double
    '        Dim GTotSerenadeServed() As Double
    '        Dim GTotSerenadeMissedCall() As Double
    '        Dim GTotSerenadeCancelled() As Double
    '        Dim GTotSerenadeNotCall() As Double
    '        Dim GTotSerenadeNotConfirm() As Double
    '        Dim GTotSerenadeNotEnd() As Double
    '        Dim GTotNonRegis() As Double
    '        Dim GTotNonServed() As Double
    '        Dim GTotNonMissedCall() As Double
    '        Dim GTotNonCancelled() As Double
    '        Dim GTotNonNotCall() As Double
    '        Dim GTotNonNotConfirm() As Double
    '        Dim GTotNonNotEnd() As Double

    '        Dim ShopIDGroup As Int32 = 0

    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(i)
    '            If i = 0 Then
    '                'Header Row
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Week</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Year</td>"

    '                'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
    '                RowTmp = Split(dr("data_value"), "###")
    '                For Each ServTmp As String In RowTmp
    '                    Dim tmp() As String = Split(ServTmp, "|")
    '                    ret += "        <td colspan='21' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>"
    '                Next
    '                'ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Total</td>"
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)
    '                ReDim TotNonRegis(RowTmp.Length)
    '                ReDim TotNonServed(RowTmp.Length)
    '                ReDim TotNonMissedCall(RowTmp.Length)
    '                ReDim TotNonCancelled(RowTmp.Length)
    '                ReDim TotNonNotCall(RowTmp.Length)
    '                ReDim TotNonNotConfirm(RowTmp.Length)
    '                ReDim TotNonNotEnd(RowTmp.Length)

    '                ReDim GTotMassRegis(RowTmp.Length)
    '                ReDim GTotMassServed(RowTmp.Length)
    '                ReDim GTotMassMissedCall(RowTmp.Length)
    '                ReDim GTotMassCancelled(RowTmp.Length)
    '                ReDim GTotMassNotCall(RowTmp.Length)
    '                ReDim GTotMassNotConfirm(RowTmp.Length)
    '                ReDim GTotMassNotEnd(RowTmp.Length)
    '                ReDim GTotSerenadeRegis(RowTmp.Length)
    '                ReDim GTotSerenadeServed(RowTmp.Length)
    '                ReDim GTotSerenadeMissedCall(RowTmp.Length)
    '                ReDim GTotSerenadeCancelled(RowTmp.Length)
    '                ReDim GTotSerenadeNotCall(RowTmp.Length)
    '                ReDim GTotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim GTotSerenadeNotEnd(RowTmp.Length)
    '                ReDim GTotNonRegis(RowTmp.Length)
    '                ReDim GTotNonServed(RowTmp.Length)
    '                ReDim GTotNonMissedCall(RowTmp.Length)
    '                ReDim GTotNonCancelled(RowTmp.Length)
    '                ReDim GTotNonNotCall(RowTmp.Length)
    '                ReDim GTotNonNotConfirm(RowTmp.Length)
    '                ReDim GTotNonNotEnd(RowTmp.Length)

    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>GSM</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>1-2-Call</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>Non Mobile</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                Next
    '                ret += "    </tr>"
    '            End If
    '            '******************* Sub Total ******************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '                Dim o As Integer = 0
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='right'>" & TotMassRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotEnd(o) & "</td>"
    '                    o += 1
    '                Next
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)
    '                ReDim TotNonRegis(RowTmp.Length)
    '                ReDim TotNonServed(RowTmp.Length)
    '                ReDim TotNonMissedCall(RowTmp.Length)
    '                ReDim TotNonCancelled(RowTmp.Length)
    '                ReDim TotNonNotCall(RowTmp.Length)
    '                ReDim TotNonNotConfirm(RowTmp.Length)
    '                ReDim TotNonNotEnd(RowTmp.Length)

    '            End If
    '            '***********************************************
    '            'Data Row
    '            ret += "    <tr>"
    '            ret += "        <td align='left'>" & dr("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & "W" & dr("show_week") & "</td>"
    '            ret += "        <td align='left'>" & dr("show_year") & "</td>"

    '            Dim k As Integer = 0
    '            Dim TotalCal As Long = 0
    '            RowTmp = Split(dr("data_value"), "###")
    '            For Each ServTmp As String In RowTmp
    '                Dim tmp() As String = Split(ServTmp, "|")
    '                'Dim MassIncomp As Double = Convert.ToInt64(tmp(3)) - (Convert.ToInt64(tmp(4)) + Convert.ToInt64(tmp(5)))
    '                'Dim SerenadeIncomp As Double = Convert.ToInt64(tmp(6)) - (Convert.ToInt64(tmp(7)) + Convert.ToInt64(tmp(8)))

    '                ret += "        <td align='right'>" & tmp(3) & "</td>"
    '                ret += "        <td align='right'>" & tmp(4) & "</td>"
    '                ret += "        <td align='right'>" & tmp(5) & "</td>"
    '                ret += "        <td align='right'>" & tmp(6) & "</td>"
    '                ret += "        <td align='right'>" & tmp(7) & "</td>"
    '                ret += "        <td align='right'>" & tmp(8) & "</td>"
    '                ret += "        <td align='right'>" & tmp(9) & "</td>"
    '                ret += "        <td align='right'>" & tmp(10) & "</td>"
    '                ret += "        <td align='right'>" & tmp(11) & "</td>"
    '                ret += "        <td align='right'>" & tmp(12) & "</td>"
    '                ret += "        <td align='right'>" & tmp(13) & "</td>"
    '                ret += "        <td align='right'>" & tmp(14) & "</td>"
    '                ret += "        <td align='right'>" & tmp(15) & "</td>"
    '                ret += "        <td align='right'>" & tmp(16) & "</td>"
    '                ret += "        <td align='right'>" & tmp(17) & "</td>"
    '                ret += "        <td align='right'>" & tmp(18) & "</td>"
    '                ret += "        <td align='right'>" & tmp(19) & "</td>"
    '                ret += "        <td align='right'>" & tmp(20) & "</td>"
    '                ret += "        <td align='right'>" & tmp(21) & "</td>"
    '                ret += "        <td align='right'>" & tmp(22) & "</td>"
    '                ret += "        <td align='right'>" & tmp(23) & "</td>"

    '                TotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                TotMassServed(k) += Convert.ToDouble(tmp(4))
    '                TotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                TotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                TotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                TotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                TotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                TotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                TotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                TotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                TotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                TotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                TotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                TotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                TotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                TotNonServed(k) += Convert.ToDouble(tmp(18))
    '                TotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                TotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                TotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                TotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                TotNonNotEnd(k) += Convert.ToDouble(tmp(23))

    '                GTotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                GTotMassServed(k) += Convert.ToDouble(tmp(4))
    '                GTotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                GTotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                GTotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                GTotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                GTotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                GTotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                GTotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                GTotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                GTotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                GTotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                GTotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                GTotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                GTotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                GTotNonServed(k) += Convert.ToDouble(tmp(18))
    '                GTotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                GTotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                GTotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                GTotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                GTotNonNotEnd(k) += Convert.ToDouble(tmp(23))
    '                k += 1
    '            Next
    '            'ret += "        <td align='right'>" & TotalCal & "</td>"
    '            ret += "    </tr>"
    '        Next

    '        '******************** Total ********************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '        Dim n As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & TotMassRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotEnd(n) & "</td>"
    '            n += 1
    '        Next
    '        ReDim TotMassRegis(RowTmp.Length)
    '        ReDim TotMassServed(RowTmp.Length)
    '        ReDim TotMassMissedCall(RowTmp.Length)
    '        ReDim TotMassCancelled(RowTmp.Length)
    '        ReDim TotMassNotCall(RowTmp.Length)
    '        ReDim TotMassNotConfirm(RowTmp.Length)
    '        ReDim TotMassNotEnd(RowTmp.Length)
    '        ReDim TotSerenadeRegis(RowTmp.Length)
    '        ReDim TotSerenadeServed(RowTmp.Length)
    '        ReDim TotSerenadeMissedCall(RowTmp.Length)
    '        ReDim TotSerenadeCancelled(RowTmp.Length)
    '        ReDim TotSerenadeNotCall(RowTmp.Length)
    '        ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '        ReDim TotSerenadeNotEnd(RowTmp.Length)
    '        ReDim TotNonRegis(RowTmp.Length)
    '        ReDim TotNonServed(RowTmp.Length)
    '        ReDim TotNonMissedCall(RowTmp.Length)
    '        ReDim TotNonCancelled(RowTmp.Length)
    '        ReDim TotNonNotCall(RowTmp.Length)
    '        ReDim TotNonNotConfirm(RowTmp.Length)
    '        ReDim TotNonNotEnd(RowTmp.Length)

    '        ret += "    </tr>"
    '        '***********************************************
    '        '***************** Grand Total *****************
    '        Dim GrandTot As Long = 0
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Total</td>"
    '        Dim m As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & GTotMassRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotEnd(m) & "</td>"
    '            GrandTot += GTotMassRegis(m) + GTotSerenadeRegis(m) + GTotNonRegis(m)
    '            m += 1
    '        Next
    '        ret += "    </tr>"
    '        '***********************************************
    '        ret += "</table>"

    '        dt.Dispose()
    '        dt = Nothing
    '    End If

    '    If ret.Trim <> "" Then
    '        lblReportDesc.Text = ret
    '        lblerror.Visible = False
    '    End If
    'End Sub
    'Private Sub RenderReportByMonth(ByVal dt As DataTable)
    '    Dim ret As String = ""
    '    If dt.Rows.Count > 0 Then
    '        Dim RowTmp() As String = {}

    '        Dim TotMassRegis() As Double
    '        Dim TotMassServed() As Double
    '        Dim TotMassMissedCall() As Double
    '        Dim TotMassCancelled() As Double
    '        Dim TotMassNotCall() As Double
    '        Dim TotMassNotConfirm() As Double
    '        Dim TotMassNotEnd() As Double
    '        Dim TotSerenadeRegis() As Double
    '        Dim TotSerenadeServed() As Double
    '        Dim TotSerenadeMissedCall() As Double
    '        Dim TotSerenadeCancelled() As Double
    '        Dim TotSerenadeNotCall() As Double
    '        Dim TotSerenadeNotConfirm() As Double
    '        Dim TotSerenadeNotEnd() As Double
    '        Dim TotNonRegis() As Double
    '        Dim TotNonServed() As Double
    '        Dim TotNonMissedCall() As Double
    '        Dim TotNonCancelled() As Double
    '        Dim TotNonNotCall() As Double
    '        Dim TotNonNotConfirm() As Double
    '        Dim TotNonNotEnd() As Double

    '        Dim GTotMassRegis() As Double
    '        Dim GTotMassServed() As Double
    '        Dim GTotMassMissedCall() As Double
    '        Dim GTotMassCancelled() As Double
    '        Dim GTotMassNotCall() As Double
    '        Dim GTotMassNotConfirm() As Double
    '        Dim GTotMassNotEnd() As Double
    '        Dim GTotSerenadeRegis() As Double
    '        Dim GTotSerenadeServed() As Double
    '        Dim GTotSerenadeMissedCall() As Double
    '        Dim GTotSerenadeCancelled() As Double
    '        Dim GTotSerenadeNotCall() As Double
    '        Dim GTotSerenadeNotConfirm() As Double
    '        Dim GTotSerenadeNotEnd() As Double
    '        Dim GTotNonRegis() As Double
    '        Dim GTotNonServed() As Double
    '        Dim GTotNonMissedCall() As Double
    '        Dim GTotNonCancelled() As Double
    '        Dim GTotNonNotCall() As Double
    '        Dim GTotNonNotConfirm() As Double
    '        Dim GTotNonNotEnd() As Double

    '        Dim ShopIDGroup As Int32 = 0

    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(i)
    '            If i = 0 Then
    '                'Header Row
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Month</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Year</td>"

    '                'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
    '                RowTmp = Split(dr("data_value"), "###")
    '                For Each ServTmp As String In RowTmp
    '                    Dim tmp() As String = Split(ServTmp, "|")
    '                    ret += "        <td colspan='21' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>"
    '                Next
    '                'ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Total</td>"
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)
    '                ReDim TotNonRegis(RowTmp.Length)
    '                ReDim TotNonServed(RowTmp.Length)
    '                ReDim TotNonMissedCall(RowTmp.Length)
    '                ReDim TotNonCancelled(RowTmp.Length)
    '                ReDim TotNonNotCall(RowTmp.Length)
    '                ReDim TotNonNotConfirm(RowTmp.Length)
    '                ReDim TotNonNotEnd(RowTmp.Length)

    '                ReDim GTotMassRegis(RowTmp.Length)
    '                ReDim GTotMassServed(RowTmp.Length)
    '                ReDim GTotMassMissedCall(RowTmp.Length)
    '                ReDim GTotMassCancelled(RowTmp.Length)
    '                ReDim GTotMassNotCall(RowTmp.Length)
    '                ReDim GTotMassNotConfirm(RowTmp.Length)
    '                ReDim GTotMassNotEnd(RowTmp.Length)
    '                ReDim GTotSerenadeRegis(RowTmp.Length)
    '                ReDim GTotSerenadeServed(RowTmp.Length)
    '                ReDim GTotSerenadeMissedCall(RowTmp.Length)
    '                ReDim GTotSerenadeCancelled(RowTmp.Length)
    '                ReDim GTotSerenadeNotCall(RowTmp.Length)
    '                ReDim GTotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim GTotSerenadeNotEnd(RowTmp.Length)
    '                ReDim GTotNonRegis(RowTmp.Length)
    '                ReDim GTotNonServed(RowTmp.Length)
    '                ReDim GTotNonMissedCall(RowTmp.Length)
    '                ReDim GTotNonCancelled(RowTmp.Length)
    '                ReDim GTotNonNotCall(RowTmp.Length)
    '                ReDim GTotNonNotConfirm(RowTmp.Length)
    '                ReDim GTotNonNotEnd(RowTmp.Length)

    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>GSM</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>1-2-Call</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>Non Mobile</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                Next
    '                ret += "    </tr>"
    '            End If
    '            '******************* Sub Total ******************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '                Dim o As Integer = 0
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='right'>" & TotMassRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotEnd(o) & "</td>"
    '                    o += 1
    '                Next
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)
    '                ReDim TotNonRegis(RowTmp.Length)
    '                ReDim TotNonServed(RowTmp.Length)
    '                ReDim TotNonMissedCall(RowTmp.Length)
    '                ReDim TotNonCancelled(RowTmp.Length)
    '                ReDim TotNonNotCall(RowTmp.Length)
    '                ReDim TotNonNotConfirm(RowTmp.Length)
    '                ReDim TotNonNotEnd(RowTmp.Length)

    '            End If
    '            '***********************************************
    '            'Data Row
    '            ret += "    <tr>"
    '            ret += "        <td align='left'>" & dr("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dr("show_month") & "</td>"
    '            ret += "        <td align='left'>" & dr("show_year") & "</td>"

    '            Dim k As Integer = 0
    '            Dim TotalCal As Long = 0
    '            RowTmp = Split(dr("data_value"), "###")
    '            For Each ServTmp As String In RowTmp
    '                Dim tmp() As String = Split(ServTmp, "|")
    '                'Dim MassIncomp As Double = Convert.ToInt64(tmp(3)) - (Convert.ToInt64(tmp(4)) + Convert.ToInt64(tmp(5)))
    '                'Dim SerenadeIncomp As Double = Convert.ToInt64(tmp(6)) - (Convert.ToInt64(tmp(7)) + Convert.ToInt64(tmp(8)))

    '                ret += "        <td align='right'>" & tmp(3) & "</td>"
    '                ret += "        <td align='right'>" & tmp(4) & "</td>"
    '                ret += "        <td align='right'>" & tmp(5) & "</td>"
    '                ret += "        <td align='right'>" & tmp(6) & "</td>"
    '                ret += "        <td align='right'>" & tmp(7) & "</td>"
    '                ret += "        <td align='right'>" & tmp(8) & "</td>"
    '                ret += "        <td align='right'>" & tmp(9) & "</td>"
    '                ret += "        <td align='right'>" & tmp(10) & "</td>"
    '                ret += "        <td align='right'>" & tmp(11) & "</td>"
    '                ret += "        <td align='right'>" & tmp(12) & "</td>"
    '                ret += "        <td align='right'>" & tmp(13) & "</td>"
    '                ret += "        <td align='right'>" & tmp(14) & "</td>"
    '                ret += "        <td align='right'>" & tmp(15) & "</td>"
    '                ret += "        <td align='right'>" & tmp(16) & "</td>"
    '                ret += "        <td align='right'>" & tmp(17) & "</td>"
    '                ret += "        <td align='right'>" & tmp(18) & "</td>"
    '                ret += "        <td align='right'>" & tmp(19) & "</td>"
    '                ret += "        <td align='right'>" & tmp(20) & "</td>"
    '                ret += "        <td align='right'>" & tmp(21) & "</td>"
    '                ret += "        <td align='right'>" & tmp(22) & "</td>"
    '                ret += "        <td align='right'>" & tmp(23) & "</td>"

    '                TotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                TotMassServed(k) += Convert.ToDouble(tmp(4))
    '                TotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                TotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                TotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                TotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                TotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                TotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                TotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                TotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                TotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                TotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                TotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                TotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                TotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                TotNonServed(k) += Convert.ToDouble(tmp(18))
    '                TotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                TotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                TotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                TotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                TotNonNotEnd(k) += Convert.ToDouble(tmp(23))

    '                GTotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                GTotMassServed(k) += Convert.ToDouble(tmp(4))
    '                GTotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                GTotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                GTotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                GTotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                GTotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                GTotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                GTotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                GTotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                GTotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                GTotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                GTotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                GTotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                GTotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                GTotNonServed(k) += Convert.ToDouble(tmp(18))
    '                GTotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                GTotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                GTotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                GTotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                GTotNonNotEnd(k) += Convert.ToDouble(tmp(23))
    '                k += 1
    '            Next
    '            'ret += "        <td align='right'>" & TotalCal & "</td>"
    '            ret += "    </tr>"
    '        Next

    '        '******************** Total ********************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '        Dim n As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & TotMassRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotEnd(n) & "</td>"
    '            n += 1
    '        Next
    '        ReDim TotMassRegis(RowTmp.Length)
    '        ReDim TotMassServed(RowTmp.Length)
    '        ReDim TotMassMissedCall(RowTmp.Length)
    '        ReDim TotMassCancelled(RowTmp.Length)
    '        ReDim TotMassNotCall(RowTmp.Length)
    '        ReDim TotMassNotConfirm(RowTmp.Length)
    '        ReDim TotMassNotEnd(RowTmp.Length)
    '        ReDim TotSerenadeRegis(RowTmp.Length)
    '        ReDim TotSerenadeServed(RowTmp.Length)
    '        ReDim TotSerenadeMissedCall(RowTmp.Length)
    '        ReDim TotSerenadeCancelled(RowTmp.Length)
    '        ReDim TotSerenadeNotCall(RowTmp.Length)
    '        ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '        ReDim TotSerenadeNotEnd(RowTmp.Length)
    '        ReDim TotNonRegis(RowTmp.Length)
    '        ReDim TotNonServed(RowTmp.Length)
    '        ReDim TotNonMissedCall(RowTmp.Length)
    '        ReDim TotNonCancelled(RowTmp.Length)
    '        ReDim TotNonNotCall(RowTmp.Length)
    '        ReDim TotNonNotConfirm(RowTmp.Length)
    '        ReDim TotNonNotEnd(RowTmp.Length)

    '        ret += "    </tr>"
    '        '***********************************************
    '        '***************** Grand Total *****************
    '        Dim GrandTot As Long = 0
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Total</td>"
    '        Dim m As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & GTotMassRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotEnd(m) & "</td>"
    '            GrandTot += GTotMassRegis(m) + GTotSerenadeRegis(m) + GTotNonRegis(m)
    '            m += 1
    '        Next
    '        ret += "    </tr>"
    '        '***********************************************
    '        ret += "</table>"

    '        dt.Dispose()
    '        dt = Nothing
    '    End If

    '    If ret.Trim <> "" Then
    '        lblReportDesc.Text = ret
    '        lblerror.Visible = False
    '    End If
    'End Sub
    'Private Sub RenderReportByYear(ByVal dt As DataTable)
    '    Dim ret As String = ""
    '    If dt.Rows.Count > 0 Then
    '        Dim RowTmp() As String = {}

    '        Dim TotMassRegis() As Double
    '        Dim TotMassServed() As Double
    '        Dim TotMassMissedCall() As Double
    '        Dim TotMassCancelled() As Double
    '        Dim TotMassNotCall() As Double
    '        Dim TotMassNotConfirm() As Double
    '        Dim TotMassNotEnd() As Double
    '        Dim TotSerenadeRegis() As Double
    '        Dim TotSerenadeServed() As Double
    '        Dim TotSerenadeMissedCall() As Double
    '        Dim TotSerenadeCancelled() As Double
    '        Dim TotSerenadeNotCall() As Double
    '        Dim TotSerenadeNotConfirm() As Double
    '        Dim TotSerenadeNotEnd() As Double
    '        Dim TotNonRegis() As Double
    '        Dim TotNonServed() As Double
    '        Dim TotNonMissedCall() As Double
    '        Dim TotNonCancelled() As Double
    '        Dim TotNonNotCall() As Double
    '        Dim TotNonNotConfirm() As Double
    '        Dim TotNonNotEnd() As Double

    '        Dim GTotMassRegis() As Double
    '        Dim GTotMassServed() As Double
    '        Dim GTotMassMissedCall() As Double
    '        Dim GTotMassCancelled() As Double
    '        Dim GTotMassNotCall() As Double
    '        Dim GTotMassNotConfirm() As Double
    '        Dim GTotMassNotEnd() As Double
    '        Dim GTotSerenadeRegis() As Double
    '        Dim GTotSerenadeServed() As Double
    '        Dim GTotSerenadeMissedCall() As Double
    '        Dim GTotSerenadeCancelled() As Double
    '        Dim GTotSerenadeNotCall() As Double
    '        Dim GTotSerenadeNotConfirm() As Double
    '        Dim GTotSerenadeNotEnd() As Double
    '        Dim GTotNonRegis() As Double
    '        Dim GTotNonServed() As Double
    '        Dim GTotNonMissedCall() As Double
    '        Dim GTotNonCancelled() As Double
    '        Dim GTotNonNotCall() As Double
    '        Dim GTotNonNotConfirm() As Double
    '        Dim GTotNonNotEnd() As Double


    '        Dim ShopIDGroup As Int32 = 0

    '        ret += "<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >"
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dr As DataRow = dt.Rows(i)
    '            If i = 0 Then
    '                'Header Row
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Shop Name</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Year</td>"
    '                ret += "        <td rowspan='4' align='center' style='color: #ffffff;' >Quarter</td>"

    '                'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
    '                RowTmp = Split(dr("data_value"), "###")
    '                For Each ServTmp As String In RowTmp
    '                    Dim tmp() As String = Split(ServTmp, "|")
    '                    ret += "        <td colspan='21' align='center' style='color: #ffffff;'>" & tmp(1) & "</td>"
    '                Next
    '                'ret += "        <td rowspan='4' align='center' style='color: #ffffff;width:100px' >Total</td>"
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)
    '                ReDim TotNonRegis(RowTmp.Length)
    '                ReDim TotNonServed(RowTmp.Length)
    '                ReDim TotNonMissedCall(RowTmp.Length)
    '                ReDim TotNonCancelled(RowTmp.Length)
    '                ReDim TotNonNotCall(RowTmp.Length)
    '                ReDim TotNonNotConfirm(RowTmp.Length)
    '                ReDim TotNonNotEnd(RowTmp.Length)

    '                ReDim GTotMassRegis(RowTmp.Length)
    '                ReDim GTotMassServed(RowTmp.Length)
    '                ReDim GTotMassMissedCall(RowTmp.Length)
    '                ReDim GTotMassCancelled(RowTmp.Length)
    '                ReDim GTotMassNotCall(RowTmp.Length)
    '                ReDim GTotMassNotConfirm(RowTmp.Length)
    '                ReDim GTotMassNotEnd(RowTmp.Length)
    '                ReDim GTotSerenadeRegis(RowTmp.Length)
    '                ReDim GTotSerenadeServed(RowTmp.Length)
    '                ReDim GTotSerenadeMissedCall(RowTmp.Length)
    '                ReDim GTotSerenadeCancelled(RowTmp.Length)
    '                ReDim GTotSerenadeNotCall(RowTmp.Length)
    '                ReDim GTotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim GTotSerenadeNotEnd(RowTmp.Length)
    '                ReDim GTotNonRegis(RowTmp.Length)
    '                ReDim GTotNonServed(RowTmp.Length)
    '                ReDim GTotNonMissedCall(RowTmp.Length)
    '                ReDim GTotNonCancelled(RowTmp.Length)
    '                ReDim GTotNonNotCall(RowTmp.Length)
    '                ReDim GTotNonNotConfirm(RowTmp.Length)
    '                ReDim GTotNonNotEnd(RowTmp.Length)

    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>GSM</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>1-2-Call</td>"
    '                    ret += "        <td colspan='7' align='center' style='color: #ffffff;'>Non Mobile</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Regis</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Serve</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Missed call</td>"
    '                    ret += "        <td rowspan='2' align='center' style='color: #ffffff;' >Cancel</td>"
    '                    ret += "        <td colspan='3' align='center' style='color: #ffffff;' >Incomplete</td>"
    '                Next
    '                ret += "    </tr>"
    '                ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Call</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not Confirm</td>"
    '                    ret += "        <td align='center' style='color: #ffffff;' >Not End</td>"
    '                Next
    '                ret += "    </tr>"
    '            End If
    '            '******************* Sub Total ******************
    '            If ShopIDGroup = 0 Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id").ToString
    '            End If
    '            If ShopIDGroup <> dt.Rows(i).Item("shop_id") Then
    '                ShopIDGroup = dt.Rows(i).Item("shop_id")
    '                ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '                ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '                Dim o As Integer = 0
    '                For Each ServTmp As String In RowTmp
    '                    ret += "        <td align='right'>" & TotMassRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotMassNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotSerenadeNotEnd(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonRegis(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonServed(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonMissedCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonCancelled(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotCall(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotConfirm(o) & "</td>"
    '                    ret += "        <td align='right'>" & TotNonNotEnd(o) & "</td>"
    '                    o += 1
    '                Next
    '                ret += "    </tr>"

    '                ReDim TotMassRegis(RowTmp.Length)
    '                ReDim TotMassServed(RowTmp.Length)
    '                ReDim TotMassMissedCall(RowTmp.Length)
    '                ReDim TotMassCancelled(RowTmp.Length)
    '                ReDim TotMassNotCall(RowTmp.Length)
    '                ReDim TotMassNotConfirm(RowTmp.Length)
    '                ReDim TotMassNotEnd(RowTmp.Length)
    '                ReDim TotSerenadeRegis(RowTmp.Length)
    '                ReDim TotSerenadeServed(RowTmp.Length)
    '                ReDim TotSerenadeMissedCall(RowTmp.Length)
    '                ReDim TotSerenadeCancelled(RowTmp.Length)
    '                ReDim TotSerenadeNotCall(RowTmp.Length)
    '                ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '                ReDim TotSerenadeNotEnd(RowTmp.Length)
    '                ReDim TotNonRegis(RowTmp.Length)
    '                ReDim TotNonServed(RowTmp.Length)
    '                ReDim TotNonMissedCall(RowTmp.Length)
    '                ReDim TotNonCancelled(RowTmp.Length)
    '                ReDim TotNonNotCall(RowTmp.Length)
    '                ReDim TotNonNotConfirm(RowTmp.Length)
    '                ReDim TotNonNotEnd(RowTmp.Length)

    '            End If
    '            '***********************************************
    '            'Data Row
    '            ret += "    <tr>"
    '            ret += "        <td align='left'>" & dr("shop_name_en") & "</td>"
    '            ret += "        <td align='left'>" & dr("show_year") & "</td>"
    '            ret += "        <td align='left'>" & "Q" & dr("show_quarter") & "</td>"

    '            Dim k As Integer = 0
    '            Dim TotalCal As Long = 0
    '            RowTmp = Split(dr("data_value"), "###")
    '            For Each ServTmp As String In RowTmp
    '                Dim tmp() As String = Split(ServTmp, "|")
    '                'Dim MassIncomp As Double = Convert.ToInt64(tmp(3)) - (Convert.ToInt64(tmp(4)) + Convert.ToInt64(tmp(5)))
    '                'Dim SerenadeIncomp As Double = Convert.ToInt64(tmp(6)) - (Convert.ToInt64(tmp(7)) + Convert.ToInt64(tmp(8)))

    '                ret += "        <td align='right'>" & tmp(3) & "</td>"
    '                ret += "        <td align='right'>" & tmp(4) & "</td>"
    '                ret += "        <td align='right'>" & tmp(5) & "</td>"
    '                ret += "        <td align='right'>" & tmp(6) & "</td>"
    '                ret += "        <td align='right'>" & tmp(7) & "</td>"
    '                ret += "        <td align='right'>" & tmp(8) & "</td>"
    '                ret += "        <td align='right'>" & tmp(9) & "</td>"
    '                ret += "        <td align='right'>" & tmp(10) & "</td>"
    '                ret += "        <td align='right'>" & tmp(11) & "</td>"
    '                ret += "        <td align='right'>" & tmp(12) & "</td>"
    '                ret += "        <td align='right'>" & tmp(13) & "</td>"
    '                ret += "        <td align='right'>" & tmp(14) & "</td>"
    '                ret += "        <td align='right'>" & tmp(15) & "</td>"
    '                ret += "        <td align='right'>" & tmp(16) & "</td>"
    '                ret += "        <td align='right'>" & tmp(17) & "</td>"
    '                ret += "        <td align='right'>" & tmp(18) & "</td>"
    '                ret += "        <td align='right'>" & tmp(19) & "</td>"
    '                ret += "        <td align='right'>" & tmp(20) & "</td>"
    '                ret += "        <td align='right'>" & tmp(21) & "</td>"
    '                ret += "        <td align='right'>" & tmp(22) & "</td>"
    '                ret += "        <td align='right'>" & tmp(23) & "</td>"

    '                TotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                TotMassServed(k) += Convert.ToDouble(tmp(4))
    '                TotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                TotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                TotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                TotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                TotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                TotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                TotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                TotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                TotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                TotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                TotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                TotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                TotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                TotNonServed(k) += Convert.ToDouble(tmp(18))
    '                TotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                TotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                TotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                TotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                TotNonNotEnd(k) += Convert.ToDouble(tmp(23))

    '                GTotMassRegis(k) += Convert.ToDouble(tmp(3))
    '                GTotMassServed(k) += Convert.ToDouble(tmp(4))
    '                GTotMassMissedCall(k) += Convert.ToDouble(tmp(5))
    '                GTotMassCancelled(k) += Convert.ToDouble(tmp(6))
    '                GTotMassNotCall(k) += Convert.ToDouble(tmp(7))
    '                GTotMassNotConfirm(k) += Convert.ToDouble(tmp(8))
    '                GTotMassNotEnd(k) += Convert.ToDouble(tmp(9))
    '                GTotSerenadeRegis(k) += Convert.ToDouble(tmp(10))
    '                GTotSerenadeServed(k) += Convert.ToDouble(tmp(11))
    '                GTotSerenadeMissedCall(k) += Convert.ToDouble(tmp(12))
    '                GTotSerenadeCancelled(k) += Convert.ToDouble(tmp(13))
    '                GTotSerenadeNotCall(k) += Convert.ToDouble(tmp(14))
    '                GTotSerenadeNotConfirm(k) += Convert.ToDouble(tmp(15))
    '                GTotSerenadeNotEnd(k) += Convert.ToDouble(tmp(16))
    '                GTotNonRegis(k) += Convert.ToDouble(tmp(17))
    '                GTotNonServed(k) += Convert.ToDouble(tmp(18))
    '                GTotNonMissedCall(k) += Convert.ToDouble(tmp(19))
    '                GTotNonCancelled(k) += Convert.ToDouble(tmp(20))
    '                GTotNonNotCall(k) += Convert.ToDouble(tmp(21))
    '                GTotNonNotConfirm(k) += Convert.ToDouble(tmp(22))
    '                GTotNonNotEnd(k) += Convert.ToDouble(tmp(23))
    '                k += 1
    '            Next
    '            'ret += "        <td align='right'>" & TotalCal & "</td>"
    '            ret += "    </tr>"
    '        Next

    '        '******************** Total ********************
    '        ret += "    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Sub Total</td>"
    '        Dim n As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & TotMassRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotMassNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotSerenadeNotEnd(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonRegis(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonServed(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonMissedCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonCancelled(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotCall(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotConfirm(n) & "</td>"
    '            ret += "        <td align='right'>" & TotNonNotEnd(n) & "</td>"
    '            n += 1
    '        Next
    '        ReDim TotMassRegis(RowTmp.Length)
    '        ReDim TotMassServed(RowTmp.Length)
    '        ReDim TotMassMissedCall(RowTmp.Length)
    '        ReDim TotMassCancelled(RowTmp.Length)
    '        ReDim TotMassNotCall(RowTmp.Length)
    '        ReDim TotMassNotConfirm(RowTmp.Length)
    '        ReDim TotMassNotEnd(RowTmp.Length)
    '        ReDim TotSerenadeRegis(RowTmp.Length)
    '        ReDim TotSerenadeServed(RowTmp.Length)
    '        ReDim TotSerenadeMissedCall(RowTmp.Length)
    '        ReDim TotSerenadeCancelled(RowTmp.Length)
    '        ReDim TotSerenadeNotCall(RowTmp.Length)
    '        ReDim TotSerenadeNotConfirm(RowTmp.Length)
    '        ReDim TotSerenadeNotEnd(RowTmp.Length)
    '        ReDim TotNonRegis(RowTmp.Length)
    '        ReDim TotNonServed(RowTmp.Length)
    '        ReDim TotNonMissedCall(RowTmp.Length)
    '        ReDim TotNonCancelled(RowTmp.Length)
    '        ReDim TotNonNotCall(RowTmp.Length)
    '        ReDim TotNonNotConfirm(RowTmp.Length)
    '        ReDim TotNonNotEnd(RowTmp.Length)

    '        ret += "    </tr>"
    '        '***********************************************
    '        '***************** Grand Total *****************
    '        Dim GrandTot As Long = 0
    '        ret += "    <tr style='background: yellowgreen repeat-x top;font-weight: bold;'>"
    '        ret += "        <td align='center' colspan='3'>Total</td>"
    '        Dim m As Integer = 0
    '        For Each ServTmp As String In RowTmp
    '            ret += "        <td align='right'>" & GTotMassRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotMassNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotSerenadeNotEnd(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonRegis(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonServed(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonMissedCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonCancelled(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotCall(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotConfirm(m) & "</td>"
    '            ret += "        <td align='right'>" & GTotNonNotEnd(m) & "</td>"
    '            GrandTot += GTotMassRegis(m) + GTotSerenadeRegis(m) + GTotNonRegis(m)
    '            m += 1
    '        Next
    '        ret += "    </tr>"
    '        '***********************************************
    '        ret += "</table>"

    '        dt.Dispose()
    '        dt = Nothing
    '    End If

    '    If ret.Trim <> "" Then
    '        lblReportDesc.Text = ret
    '        lblerror.Visible = False
    '    End If
    'End Sub

   
End Class
