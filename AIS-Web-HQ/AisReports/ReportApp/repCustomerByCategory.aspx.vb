Imports System.IO
Imports System.Data
Imports Engine.Common

Partial Class ReportApp_repCustomerByCategory
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "CustomerContactByCategorySubCategoryReport_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".xls")
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
            Try
                Dim para As New CenParaDB.ReportCriteria.CustByCategoryPara
                para.ShopID = Request("ShopID")
                para.DateFrom = Request("DateFrom")
                para.DateTo = Request("DateTo")
                para.IntervalMinute = Request("Interval")
                para.TimePeroidFrom = Request("TimeFrom")
                para.TimePeroidTo = Request("TimeTo")
                Dim eng As New Engine.Reports.RepCustByCategoryENG
                Dim dt As New DataTable
                dt = eng.GetReportByTime(para)
                If dt.Rows.Count > 0 Then
                    RenderReportByTime(dt)
                Else
                    btnExport.Visible = False
                End If
                dt.Dispose()
                eng = Nothing
                para = Nothing
            Catch ex As Exception

            End Try
        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.CustByCategoryPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepCustByCategoryENG
            Dim dt As New DataTable
            dt = eng.GetReportByDate(para)
            If dt.Rows.Count > 0 Then
                RenderReportByDate(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.CustByCategoryPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim eng As New Engine.Reports.RepCustByCategoryENG
            Dim dt As New DataTable
            dt = eng.GetReportByWeek(para)
            If dt.Rows.Count > 0 Then
                RenderReportByWeek(dt)
            Else
                btnExport.Visible = False
            End If
            eng = Nothing
            dt.Dispose()
            para = Nothing
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.CustByCategoryPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim dt As New DataTable
            Dim eng As New Engine.Reports.RepCustByCategoryENG
            dt = eng.GetReportByMonth(para)
            If dt.Rows.Count > 0 Then
                RenderReportByMonth(dt)
            Else
                btnExport.Visible = False
            End If
            eng = Nothing
            dt.Dispose()
        End If
    End Sub

    Private Sub RenderReportByTime(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Try
                Dim GRegis(12) As Int32
                Dim GServe(12) As Int32
                Dim GMissCall(12) As Int32
                Dim GCancel(12) As Int32
                Dim GNotCall(12) As Int32
                Dim GNotCon(12) As Int32
                Dim GNotEnd(12) As Int32

                Dim ret As New StringBuilder
                'Header Row
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                Dim sDt As New DataTable
                sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th")
                For Each sDr As DataRow In sDt.Rows
                    Try
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' rowspan='4' >Date</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;width:100px' rowspan='4' >Shop Name</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;width:100px' rowspan='4' >Time</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='84' >" & sDr("service_name_en") & "</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Residential</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Business</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='28' >Government & Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Exclusive</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >In House</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Thai Citizen</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Foreigner</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Key Account</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >SME</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Government</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >State Enterprise</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Embassy</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Royal</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >TOT</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Pre-paid</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >AIS</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        For h As Integer = 1 To 12
                            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not End</td>")
                        Next
                        ret.Append("    </tr>")

                        'Data Row By Shop
                        dt.DefaultView.RowFilter = "service_id='" & sDr("service_id") & "'"
                        Dim shDt As New DataTable
                        shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
                        For Each shDr As DataRow In shDt.Rows
                            Dim TRegis(12) As Int32
                            Dim TServe(12) As Int32
                            Dim TMissCall(12) As Int32
                            Dim TCancel(12) As Int32
                            Dim TNotCall(12) As Int32
                            Dim TNotCon(12) As Int32
                            Dim TNotEnd(12) As Int32

                            'Data By Date
                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                            Dim dDt As New DataTable
                            dDt = dt.DefaultView.ToTable(True, "show_date")
                            For Each dDr As DataRow In dDt.Rows
                                Dim SRegis(12) As Int32
                                Dim SServe(12) As Int32
                                Dim SMissCall(12) As Int32
                                Dim SCancel(12) As Int32
                                Dim SNotCall(12) As Int32
                                Dim SNotCon(12) As Int32
                                Dim SNotEnd(12) As Int32

                                dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "' and show_date='" & dDr("show_date") & "'"
                                For Each dr As DataRowView In dt.DefaultView
                                    ret.Append("    <tr>")
                                    ret.Append("        <td align='left'>" & dr("show_date") & "</td>")
                                    ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                                    ret.Append("        <td align='left'>" & dr("show_time") & "</td>")
                                    'Thai Citizen
                                    ret.Append("        <td align='right'>" & Format(dr("tha_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tha_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tha_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tha_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tha_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tha_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tha_not_end"), "#,##0") & "</td>")
                                    SRegis(1) += Convert.ToInt32(dr("tha_regis"))
                                    SServe(1) += Convert.ToInt32(dr("tha_serve"))
                                    SMissCall(1) += Convert.ToInt32(dr("tha_miss_call"))
                                    SCancel(1) += Convert.ToInt32(dr("tha_cancel"))
                                    SNotCall(1) += Convert.ToInt32(dr("tha_not_call"))
                                    SNotCon(1) += Convert.ToInt32(dr("tha_not_con"))
                                    SNotEnd(1) += Convert.ToInt32(dr("tha_not_end"))

                                    'Foreigner
                                    ret.Append("        <td align='right'>" & Format(dr("for_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("for_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("for_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("for_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("for_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("for_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("for_not_end"), "#,##0") & "</td>")
                                    SRegis(2) += Convert.ToInt32(dr("for_regis"))
                                    SServe(2) += Convert.ToInt32(dr("for_serve"))
                                    SMissCall(2) += Convert.ToInt32(dr("for_miss_call"))
                                    SCancel(2) += Convert.ToInt32(dr("for_cancel"))
                                    SNotCall(2) += Convert.ToInt32(dr("for_not_call"))
                                    SNotCon(2) += Convert.ToInt32(dr("for_not_con"))
                                    SNotEnd(2) += Convert.ToInt32(dr("for_not_end"))

                                    'Key Account
                                    ret.Append("        <td align='right'>" & Format(dr("key_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("key_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("key_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("key_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("key_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("key_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("key_not_end"), "#,##0") & "</td>")
                                    SRegis(3) += Convert.ToInt32(dr("key_regis"))
                                    SServe(3) += Convert.ToInt32(dr("key_serve"))
                                    SMissCall(3) += Convert.ToInt32(dr("key_miss_call"))
                                    SCancel(3) += Convert.ToInt32(dr("key_cancel"))
                                    SNotCall(3) += Convert.ToInt32(dr("key_not_call"))
                                    SNotCon(3) += Convert.ToInt32(dr("key_not_con"))
                                    SNotEnd(3) += Convert.ToInt32(dr("key_not_end"))

                                    'SME
                                    ret.Append("        <td align='right'>" & Format(dr("sme_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sme_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sme_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sme_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sme_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sme_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sme_not_end"), "#,##0") & "</td>")
                                    SRegis(4) += Convert.ToInt32(dr("sme_regis"))
                                    SServe(4) += Convert.ToInt32(dr("sme_serve"))
                                    SMissCall(4) += Convert.ToInt32(dr("sme_miss_call"))
                                    SCancel(4) += Convert.ToInt32(dr("sme_cancel"))
                                    SNotCall(4) += Convert.ToInt32(dr("sme_not_call"))
                                    SNotCon(4) += Convert.ToInt32(dr("sme_not_con"))
                                    SNotEnd(4) += Convert.ToInt32(dr("sme_not_end"))

                                    'Government
                                    ret.Append("        <td align='right'>" & Format(dr("gov_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gov_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gov_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gov_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gov_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gov_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("gov_not_end"), "#,##0") & "</td>")
                                    SRegis(5) += Convert.ToInt32(dr("gov_regis"))
                                    SServe(5) += Convert.ToInt32(dr("gov_serve"))
                                    SMissCall(5) += Convert.ToInt32(dr("gov_miss_call"))
                                    SCancel(5) += Convert.ToInt32(dr("gov_cancel"))
                                    SNotCall(5) += Convert.ToInt32(dr("gov_not_call"))
                                    SNotCon(5) += Convert.ToInt32(dr("gov_not_con"))
                                    SNotEnd(5) += Convert.ToInt32(dr("gov_not_end"))

                                    'State Enterprise
                                    ret.Append("        <td align='right'>" & Format(dr("sta_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sta_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sta_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sta_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sta_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sta_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("sta_not_end"), "#,##0") & "</td>")
                                    SRegis(6) += Convert.ToInt32(dr("sta_regis"))
                                    SServe(6) += Convert.ToInt32(dr("sta_serve"))
                                    SMissCall(6) += Convert.ToInt32(dr("sta_miss_call"))
                                    SCancel(6) += Convert.ToInt32(dr("sta_cancel"))
                                    SNotCall(6) += Convert.ToInt32(dr("sta_not_call"))
                                    SNotCon(6) += Convert.ToInt32(dr("sta_not_con"))
                                    SNotEnd(6) += Convert.ToInt32(dr("sta_not_end"))

                                    'Embassy
                                    ret.Append("        <td align='right'>" & Format(dr("emb_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("emb_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("emb_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("emb_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("emb_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("emb_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("emb_not_end"), "#,##0") & "</td>")
                                    SRegis(7) += Convert.ToInt32(dr("emb_regis"))
                                    SServe(7) += Convert.ToInt32(dr("emb_serve"))
                                    SMissCall(7) += Convert.ToInt32(dr("emb_miss_call"))
                                    SCancel(7) += Convert.ToInt32(dr("emb_cancel"))
                                    SNotCall(7) += Convert.ToInt32(dr("emb_not_call"))
                                    SNotCon(7) += Convert.ToInt32(dr("emb_not_con"))
                                    SNotEnd(7) += Convert.ToInt32(dr("emb_not_end"))

                                    'Non Profit
                                    ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("non_not_end"), "#,##0") & "</td>")
                                    SRegis(8) += Convert.ToInt32(dr("non_regis"))
                                    SServe(8) += Convert.ToInt32(dr("non_serve"))
                                    SMissCall(8) += Convert.ToInt32(dr("non_miss_call"))
                                    SCancel(8) += Convert.ToInt32(dr("non_cancel"))
                                    SNotCall(8) += Convert.ToInt32(dr("non_not_call"))
                                    SNotCon(8) += Convert.ToInt32(dr("non_not_con"))
                                    SNotEnd(8) += Convert.ToInt32(dr("non_not_end"))

                                    'Royal
                                    ret.Append("        <td align='right'>" & Format(dr("roy_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("roy_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("roy_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("roy_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("roy_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("roy_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("roy_not_end"), "#,##0") & "</td>")
                                    SRegis(9) += Convert.ToInt32(dr("roy_regis"))
                                    SServe(9) += Convert.ToInt32(dr("roy_serve"))
                                    SMissCall(9) += Convert.ToInt32(dr("roy_miss_call"))
                                    SCancel(9) += Convert.ToInt32(dr("roy_cancel"))
                                    SNotCall(9) += Convert.ToInt32(dr("roy_not_call"))
                                    SNotCon(9) += Convert.ToInt32(dr("roy_not_con"))
                                    SNotEnd(9) += Convert.ToInt32(dr("roy_not_end"))

                                    'TOT
                                    ret.Append("        <td align='right'>" & Format(dr("tot_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tot_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tot_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tot_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tot_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tot_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("tot_not_end"), "#,##0") & "</td>")
                                    SRegis(10) += Convert.ToInt32(dr("tot_regis"))
                                    SServe(10) += Convert.ToInt32(dr("tot_serve"))
                                    SMissCall(10) += Convert.ToInt32(dr("tot_miss_call"))
                                    SCancel(10) += Convert.ToInt32(dr("tot_cancel"))
                                    SNotCall(10) += Convert.ToInt32(dr("tot_not_call"))
                                    SNotCon(10) += Convert.ToInt32(dr("tot_not_con"))
                                    SNotEnd(10) += Convert.ToInt32(dr("tot_not_end"))

                                    'Pre-paid
                                    ret.Append("        <td align='right'>" & Format(dr("pre_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("pre_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("pre_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("pre_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("pre_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("pre_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("pre_not_end"), "#,##0") & "</td>")
                                    SRegis(11) += Convert.ToInt32(dr("pre_regis"))
                                    SServe(11) += Convert.ToInt32(dr("pre_serve"))
                                    SMissCall(11) += Convert.ToInt32(dr("pre_miss_call"))
                                    SCancel(11) += Convert.ToInt32(dr("pre_cancel"))
                                    SNotCall(11) += Convert.ToInt32(dr("pre_not_call"))
                                    SNotCon(11) += Convert.ToInt32(dr("pre_not_con"))
                                    SNotEnd(11) += Convert.ToInt32(dr("pre_not_end"))

                                    'AIS
                                    ret.Append("        <td align='right'>" & Format(dr("ais_regis"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais_serve"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais_miss_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais_cancel"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais_not_call"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais_not_con"), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(dr("ais_not_end"), "#,##0") & "</td>")
                                    ret.Append("    </tr>")
                                    SRegis(12) += Convert.ToInt32(dr("ais_regis"))
                                    SServe(12) += Convert.ToInt32(dr("ais_serve"))
                                    SMissCall(12) += Convert.ToInt32(dr("ais_miss_call"))
                                    SCancel(12) += Convert.ToInt32(dr("ais_cancel"))
                                    SNotCall(12) += Convert.ToInt32(dr("ais_not_call"))
                                    SNotCon(12) += Convert.ToInt32(dr("ais_not_con"))
                                    SNotEnd(12) += Convert.ToInt32(dr("ais_not_end"))
                                Next

                                'SubTotal By Date
                                ret.Append("    <tr style='background: #E4E4E4 repeat-x top;font-weight: bold;'>")
                                ret.Append("        <td align='center' >" & dDr("show_date") & "</td>")
                                ret.Append("        <td align='center' colspan='2'>Sub Total</td>")
                                For i As Integer = 1 To 12
                                    ret.Append("        <td align='right'>" & Format(SRegis(i), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(SServe(i), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(SMissCall(i), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(SCancel(i), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(SNotCall(i), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(SNotCon(i), "#,##0") & "</td>")
                                    ret.Append("        <td align='right'>" & Format(SNotEnd(i), "#,##0") & "</td>")

                                    TRegis(i) += SRegis(i)
                                    TServe(i) += SServe(i)
                                    TMissCall(i) += SMissCall(i)
                                    TCancel(i) += SCancel(i)
                                    TNotCall(i) += SNotCall(i)
                                    TNotCon(i) += SNotCon(i)
                                    TNotEnd(i) += SNotEnd(i)
                                Next
                                ret.Append("    </tr>")
                            Next
                            dDt.Dispose()


                            'Total By Shop 1 Service
                            ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                            ret.Append("        <td align='center' colspan='3'>Total " & shDr("shop_name_en").ToString & "</td>")
                            For i As Integer = 1 To 12
                                ret.Append("        <td align='right'>" & Format(TRegis(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TServe(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TMissCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TCancel(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCon(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotEnd(i), "#,##0") & "</td>")

                                GRegis(i) += TRegis(i)
                                GServe(i) += TServe(i)
                                GMissCall(i) += TMissCall(i)
                                GCancel(i) += TCancel(i)
                                GNotCall(i) += TNotCall(i)
                                GNotCon(i) += TNotCon(i)
                                GNotEnd(i) += TNotEnd(i)
                            Next
                            ret.Append("    </tr>")
                        Next
                        shDt.Dispose()
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("1. ReportApp_repCustomerByCategory.RenderReportByTime", ex.Message & vbNewLine & ex.StackTrace)
                        lblerror.Text = ex.Message & vbNewLine & ex.StackTrace
                    End Try
                Next
                sDt.Dispose()

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='3'>Grand Total</td>")
                For i As Integer = 1 To 12
                    ret.Append("        <td align='right'>" & Format(GRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCon(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotEnd(i), "#,##0") & "</td>")
                Next
                ret.Append("    </tr>")
                ret.Append("</table>")

                lblReportDesc.Text = ret.ToString
            Catch ex As Exception
                FunctionEng.SaveErrorLog("2. ReportApp_repCustomerByCategory.RenderReportByTime", ex.Message & vbNewLine & ex.StackTrace)
                lblerror.Text = ex.Message & vbNewLine & ex.StackTrace
            End Try
        Else
            btnExport.Visible = False
        End If
        dt.Dispose()

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Try
                Dim GRegis(12) As Int32
                Dim GServe(12) As Int32
                Dim GMissCall(12) As Int32
                Dim GCancel(12) As Int32
                Dim GNotCall(12) As Int32
                Dim GNotCon(12) As Int32
                Dim GNotEnd(12) As Int32


                Dim ret As New StringBuilder
                'Header Row
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                Dim sDt As New DataTable
                sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th")
                For Each sDr As DataRow In sDt.Rows
                    Try
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' rowspan='4' >Date</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;width:100px' rowspan='4' >Shop Name</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='84' >" & sDr("service_name_en") & "</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Residential</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Business</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='28' >Government & Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Exclusive</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >In House</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Thai Citizen</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Foreigner</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Key Account</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >SME</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Government</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >State Enterprise</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Embassy</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Royal</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >TOT</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Pre-paid</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >AIS</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        For h As Integer = 1 To 12
                            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not End</td>")
                        Next
                        ret.Append("    </tr>")

                        'Data Row By Shop
                        dt.DefaultView.RowFilter = "service_id='" & sDr("service_id") & "'"
                        Dim shDt As New DataTable
                        shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
                        For Each shDr As DataRow In shDt.Rows
                            Dim TRegis(12) As Int32
                            Dim TServe(12) As Int32
                            Dim TMissCall(12) As Int32
                            Dim TCancel(12) As Int32
                            Dim TNotCall(12) As Int32
                            Dim TNotCon(12) As Int32
                            Dim TNotEnd(12) As Int32

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "' "
                            For Each dr As DataRowView In dt.DefaultView
                                ret.Append("    <tr>")
                                ret.Append("        <td align='left'>" & dr("show_date") & "</td>")
                                ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                                'Thai Citizen
                                ret.Append("        <td align='right'>" & Format(dr("tha_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_end"), "#,##0") & "</td>")
                                TRegis(1) += Convert.ToInt32(dr("tha_regis"))
                                TServe(1) += Convert.ToInt32(dr("tha_serve"))
                                TMissCall(1) += Convert.ToInt32(dr("tha_miss_call"))
                                TCancel(1) += Convert.ToInt32(dr("tha_cancel"))
                                TNotCall(1) += Convert.ToInt32(dr("tha_not_call"))
                                TNotCon(1) += Convert.ToInt32(dr("tha_not_con"))
                                TNotEnd(1) += Convert.ToInt32(dr("tha_not_end"))

                                'Foreigner
                                ret.Append("        <td align='right'>" & Format(dr("for_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_end"), "#,##0") & "</td>")
                                TRegis(2) += Convert.ToInt32(dr("for_regis"))
                                TServe(2) += Convert.ToInt32(dr("for_serve"))
                                TMissCall(2) += Convert.ToInt32(dr("for_miss_call"))
                                TCancel(2) += Convert.ToInt32(dr("for_cancel"))
                                TNotCall(2) += Convert.ToInt32(dr("for_not_call"))
                                TNotCon(2) += Convert.ToInt32(dr("for_not_con"))
                                TNotEnd(2) += Convert.ToInt32(dr("for_not_end"))

                                'Key Account
                                ret.Append("        <td align='right'>" & Format(dr("key_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_end"), "#,##0") & "</td>")
                                TRegis(3) += Convert.ToInt32(dr("key_regis"))
                                TServe(3) += Convert.ToInt32(dr("key_serve"))
                                TMissCall(3) += Convert.ToInt32(dr("key_miss_call"))
                                TCancel(3) += Convert.ToInt32(dr("key_cancel"))
                                TNotCall(3) += Convert.ToInt32(dr("key_not_call"))
                                TNotCon(3) += Convert.ToInt32(dr("key_not_con"))
                                TNotEnd(3) += Convert.ToInt32(dr("key_not_end"))

                                'SME
                                ret.Append("        <td align='right'>" & Format(dr("sme_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_end"), "#,##0") & "</td>")
                                TRegis(4) += Convert.ToInt32(dr("sme_regis"))
                                TServe(4) += Convert.ToInt32(dr("sme_serve"))
                                TMissCall(4) += Convert.ToInt32(dr("sme_miss_call"))
                                TCancel(4) += Convert.ToInt32(dr("sme_cancel"))
                                TNotCall(4) += Convert.ToInt32(dr("sme_not_call"))
                                TNotCon(4) += Convert.ToInt32(dr("sme_not_con"))
                                TNotEnd(4) += Convert.ToInt32(dr("sme_not_end"))

                                'Government
                                ret.Append("        <td align='right'>" & Format(dr("gov_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_end"), "#,##0") & "</td>")
                                TRegis(5) += Convert.ToInt32(dr("gov_regis"))
                                TServe(5) += Convert.ToInt32(dr("gov_serve"))
                                TMissCall(5) += Convert.ToInt32(dr("gov_miss_call"))
                                TCancel(5) += Convert.ToInt32(dr("gov_cancel"))
                                TNotCall(5) += Convert.ToInt32(dr("gov_not_call"))
                                TNotCon(5) += Convert.ToInt32(dr("gov_not_con"))
                                TNotEnd(5) += Convert.ToInt32(dr("gov_not_end"))

                                'State Enterprise
                                ret.Append("        <td align='right'>" & Format(dr("sta_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_end"), "#,##0") & "</td>")
                                TRegis(6) += Convert.ToInt32(dr("sta_regis"))
                                TServe(6) += Convert.ToInt32(dr("sta_serve"))
                                TMissCall(6) += Convert.ToInt32(dr("sta_miss_call"))
                                TCancel(6) += Convert.ToInt32(dr("sta_cancel"))
                                TNotCall(6) += Convert.ToInt32(dr("sta_not_call"))
                                TNotCon(6) += Convert.ToInt32(dr("sta_not_con"))
                                TNotEnd(6) += Convert.ToInt32(dr("sta_not_end"))

                                'Embassy
                                ret.Append("        <td align='right'>" & Format(dr("emb_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_end"), "#,##0") & "</td>")
                                TRegis(7) += Convert.ToInt32(dr("emb_regis"))
                                TServe(7) += Convert.ToInt32(dr("emb_serve"))
                                TMissCall(7) += Convert.ToInt32(dr("emb_miss_call"))
                                TCancel(7) += Convert.ToInt32(dr("emb_cancel"))
                                TNotCall(7) += Convert.ToInt32(dr("emb_not_call"))
                                TNotCon(7) += Convert.ToInt32(dr("emb_not_con"))
                                TNotEnd(7) += Convert.ToInt32(dr("emb_not_end"))

                                'Non Profit
                                ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_end"), "#,##0") & "</td>")
                                TRegis(8) += Convert.ToInt32(dr("non_regis"))
                                TServe(8) += Convert.ToInt32(dr("non_serve"))
                                TMissCall(8) += Convert.ToInt32(dr("non_miss_call"))
                                TCancel(8) += Convert.ToInt32(dr("non_cancel"))
                                TNotCall(8) += Convert.ToInt32(dr("non_not_call"))
                                TNotCon(8) += Convert.ToInt32(dr("non_not_con"))
                                TNotEnd(8) += Convert.ToInt32(dr("non_not_end"))

                                'Royal
                                ret.Append("        <td align='right'>" & Format(dr("roy_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_end"), "#,##0") & "</td>")
                                TRegis(9) += Convert.ToInt32(dr("roy_regis"))
                                TServe(9) += Convert.ToInt32(dr("roy_serve"))
                                TMissCall(9) += Convert.ToInt32(dr("roy_miss_call"))
                                TCancel(9) += Convert.ToInt32(dr("roy_cancel"))
                                TNotCall(9) += Convert.ToInt32(dr("roy_not_call"))
                                TNotCon(9) += Convert.ToInt32(dr("roy_not_con"))
                                TNotEnd(9) += Convert.ToInt32(dr("roy_not_end"))

                                'TOT
                                ret.Append("        <td align='right'>" & Format(dr("tot_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_end"), "#,##0") & "</td>")
                                TRegis(10) += Convert.ToInt32(dr("tot_regis"))
                                TServe(10) += Convert.ToInt32(dr("tot_serve"))
                                TMissCall(10) += Convert.ToInt32(dr("tot_miss_call"))
                                TCancel(10) += Convert.ToInt32(dr("tot_cancel"))
                                TNotCall(10) += Convert.ToInt32(dr("tot_not_call"))
                                TNotCon(10) += Convert.ToInt32(dr("tot_not_con"))
                                TNotEnd(10) += Convert.ToInt32(dr("tot_not_end"))

                                'Pre-paid
                                ret.Append("        <td align='right'>" & Format(dr("pre_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_end"), "#,##0") & "</td>")
                                TRegis(11) += Convert.ToInt32(dr("pre_regis"))
                                TServe(11) += Convert.ToInt32(dr("pre_serve"))
                                TMissCall(11) += Convert.ToInt32(dr("pre_miss_call"))
                                TCancel(11) += Convert.ToInt32(dr("pre_cancel"))
                                TNotCall(11) += Convert.ToInt32(dr("pre_not_call"))
                                TNotCon(11) += Convert.ToInt32(dr("pre_not_con"))
                                TNotEnd(11) += Convert.ToInt32(dr("pre_not_end"))

                                'AIS
                                ret.Append("        <td align='right'>" & Format(dr("ais_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_end"), "#,##0") & "</td>")
                                ret.Append("    </tr>")
                                TRegis(12) += Convert.ToInt32(dr("ais_regis"))
                                TServe(12) += Convert.ToInt32(dr("ais_serve"))
                                TMissCall(12) += Convert.ToInt32(dr("ais_miss_call"))
                                TCancel(12) += Convert.ToInt32(dr("ais_cancel"))
                                TNotCall(12) += Convert.ToInt32(dr("ais_not_call"))
                                TNotCon(12) += Convert.ToInt32(dr("ais_not_con"))
                                TNotEnd(12) += Convert.ToInt32(dr("ais_not_end"))
                            Next



                            'Total By Shop 1 Service
                            ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                            ret.Append("        <td align='center' colspan='2'>Total " & shDr("shop_name_en").ToString & "</td>")
                            For i As Integer = 1 To 12
                                ret.Append("        <td align='right'>" & Format(TRegis(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TServe(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TMissCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TCancel(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCon(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotEnd(i), "#,##0") & "</td>")

                                GRegis(i) += TRegis(i)
                                GServe(i) += TServe(i)
                                GMissCall(i) += TMissCall(i)
                                GCancel(i) += TCancel(i)
                                GNotCall(i) += TNotCall(i)
                                GNotCon(i) += TNotCon(i)
                                GNotEnd(i) += TNotEnd(i)
                            Next
                            ret.Append("    </tr>")
                        Next
                        shDt.Dispose()
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("1. ReportApp_repCustomerByCategory.RenderReportByDate", ex.Message & vbNewLine & ex.StackTrace)
                        lblerror.text = ex.Message & vbNewLine & ex.StackTrace
                    End Try
                Next
                sDt.Dispose()

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='2'>Grand Total</td>")
                For i As Integer = 1 To 12
                    ret.Append("        <td align='right'>" & Format(GRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCon(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotEnd(i), "#,##0") & "</td>")
                Next
                ret.Append("    </tr>")
                ret.Append("</table>")

                lblReportDesc.Text = ret.ToString
                lblerror.Visible = False
            Catch ex As Exception
                FunctionEng.SaveErrorLog("2. ReportApp_repCustomerByCategory.RenderReportByDate", ex.Message & vbNewLine & ex.StackTrace)
                lblerror.text = ex.Message & vbNewLine & ex.StackTrace
            End Try
        Else
            btnExport.Visible = False
        End If
        dt.Dispose()

    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Try
                Dim GRegis(12) As Int32
                Dim GServe(12) As Int32
                Dim GMissCall(12) As Int32
                Dim GCancel(12) As Int32
                Dim GNotCall(12) As Int32
                Dim GNotCon(12) As Int32
                Dim GNotEnd(12) As Int32


                Dim ret As New StringBuilder
                'Header Row
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                Dim sDt As New DataTable
                sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th")
                For Each sDr As DataRow In sDt.Rows
                    Try
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;width:100px' rowspan='4' >Shop Name</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' rowspan='4' >Week No.</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' rowspan='4' >Year</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='84' >" & sDr("service_name_en") & "</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Residential</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Business</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='28' >Government & Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Exclusive</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >In House</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Thai Citizen</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Foreigner</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Key Account</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >SME</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Government</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >State Enterprise</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Embassy</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Royal</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >TOT</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Pre-paid</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >AIS</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        For h As Integer = 1 To 12
                            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not End</td>")
                        Next
                        ret.Append("    </tr>")

                        'Data Row By Shop
                        dt.DefaultView.RowFilter = "service_id='" & sDr("service_id") & "'"
                        Dim shDt As New DataTable
                        shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
                        For Each shDr As DataRow In shDt.Rows
                            Dim TRegis(12) As Int32
                            Dim TServe(12) As Int32
                            Dim TMissCall(12) As Int32
                            Dim TCancel(12) As Int32
                            Dim TNotCall(12) As Int32
                            Dim TNotCon(12) As Int32
                            Dim TNotEnd(12) As Int32

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "' "
                            For Each dr As DataRowView In dt.DefaultView
                                ret.Append("    <tr>")
                                ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                                ret.Append("        <td align='center'>" & dr("week_of_year") & "</td>")
                                ret.Append("        <td align='center'>" & dr("show_year") & "</td>")

                                'Thai Citizen
                                ret.Append("        <td align='right'>" & Format(dr("tha_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_end"), "#,##0") & "</td>")
                                TRegis(1) += Convert.ToInt32(dr("tha_regis"))
                                TServe(1) += Convert.ToInt32(dr("tha_serve"))
                                TMissCall(1) += Convert.ToInt32(dr("tha_miss_call"))
                                TCancel(1) += Convert.ToInt32(dr("tha_cancel"))
                                TNotCall(1) += Convert.ToInt32(dr("tha_not_call"))
                                TNotCon(1) += Convert.ToInt32(dr("tha_not_con"))
                                TNotEnd(1) += Convert.ToInt32(dr("tha_not_end"))

                                'Foreigner
                                ret.Append("        <td align='right'>" & Format(dr("for_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_end"), "#,##0") & "</td>")
                                TRegis(2) += Convert.ToInt32(dr("for_regis"))
                                TServe(2) += Convert.ToInt32(dr("for_serve"))
                                TMissCall(2) += Convert.ToInt32(dr("for_miss_call"))
                                TCancel(2) += Convert.ToInt32(dr("for_cancel"))
                                TNotCall(2) += Convert.ToInt32(dr("for_not_call"))
                                TNotCon(2) += Convert.ToInt32(dr("for_not_con"))
                                TNotEnd(2) += Convert.ToInt32(dr("for_not_end"))

                                'Key Account
                                ret.Append("        <td align='right'>" & Format(dr("key_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_end"), "#,##0") & "</td>")
                                TRegis(3) += Convert.ToInt32(dr("key_regis"))
                                TServe(3) += Convert.ToInt32(dr("key_serve"))
                                TMissCall(3) += Convert.ToInt32(dr("key_miss_call"))
                                TCancel(3) += Convert.ToInt32(dr("key_cancel"))
                                TNotCall(3) += Convert.ToInt32(dr("key_not_call"))
                                TNotCon(3) += Convert.ToInt32(dr("key_not_con"))
                                TNotEnd(3) += Convert.ToInt32(dr("key_not_end"))

                                'SME
                                ret.Append("        <td align='right'>" & Format(dr("sme_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_end"), "#,##0") & "</td>")
                                TRegis(4) += Convert.ToInt32(dr("sme_regis"))
                                TServe(4) += Convert.ToInt32(dr("sme_serve"))
                                TMissCall(4) += Convert.ToInt32(dr("sme_miss_call"))
                                TCancel(4) += Convert.ToInt32(dr("sme_cancel"))
                                TNotCall(4) += Convert.ToInt32(dr("sme_not_call"))
                                TNotCon(4) += Convert.ToInt32(dr("sme_not_con"))
                                TNotEnd(4) += Convert.ToInt32(dr("sme_not_end"))

                                'Government
                                ret.Append("        <td align='right'>" & Format(dr("gov_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_end"), "#,##0") & "</td>")
                                TRegis(5) += Convert.ToInt32(dr("gov_regis"))
                                TServe(5) += Convert.ToInt32(dr("gov_serve"))
                                TMissCall(5) += Convert.ToInt32(dr("gov_miss_call"))
                                TCancel(5) += Convert.ToInt32(dr("gov_cancel"))
                                TNotCall(5) += Convert.ToInt32(dr("gov_not_call"))
                                TNotCon(5) += Convert.ToInt32(dr("gov_not_con"))
                                TNotEnd(5) += Convert.ToInt32(dr("gov_not_end"))

                                'State Enterprise
                                ret.Append("        <td align='right'>" & Format(dr("sta_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_end"), "#,##0") & "</td>")
                                TRegis(6) += Convert.ToInt32(dr("sta_regis"))
                                TServe(6) += Convert.ToInt32(dr("sta_serve"))
                                TMissCall(6) += Convert.ToInt32(dr("sta_miss_call"))
                                TCancel(6) += Convert.ToInt32(dr("sta_cancel"))
                                TNotCall(6) += Convert.ToInt32(dr("sta_not_call"))
                                TNotCon(6) += Convert.ToInt32(dr("sta_not_con"))
                                TNotEnd(6) += Convert.ToInt32(dr("sta_not_end"))

                                'Embassy
                                ret.Append("        <td align='right'>" & Format(dr("emb_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_end"), "#,##0") & "</td>")
                                TRegis(7) += Convert.ToInt32(dr("emb_regis"))
                                TServe(7) += Convert.ToInt32(dr("emb_serve"))
                                TMissCall(7) += Convert.ToInt32(dr("emb_miss_call"))
                                TCancel(7) += Convert.ToInt32(dr("emb_cancel"))
                                TNotCall(7) += Convert.ToInt32(dr("emb_not_call"))
                                TNotCon(7) += Convert.ToInt32(dr("emb_not_con"))
                                TNotEnd(7) += Convert.ToInt32(dr("emb_not_end"))

                                'Non Profit
                                ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_end"), "#,##0") & "</td>")
                                TRegis(8) += Convert.ToInt32(dr("non_regis"))
                                TServe(8) += Convert.ToInt32(dr("non_serve"))
                                TMissCall(8) += Convert.ToInt32(dr("non_miss_call"))
                                TCancel(8) += Convert.ToInt32(dr("non_cancel"))
                                TNotCall(8) += Convert.ToInt32(dr("non_not_call"))
                                TNotCon(8) += Convert.ToInt32(dr("non_not_con"))
                                TNotEnd(8) += Convert.ToInt32(dr("non_not_end"))

                                'Royal
                                ret.Append("        <td align='right'>" & Format(dr("roy_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_end"), "#,##0") & "</td>")
                                TRegis(9) += Convert.ToInt32(dr("roy_regis"))
                                TServe(9) += Convert.ToInt32(dr("roy_serve"))
                                TMissCall(9) += Convert.ToInt32(dr("roy_miss_call"))
                                TCancel(9) += Convert.ToInt32(dr("roy_cancel"))
                                TNotCall(9) += Convert.ToInt32(dr("roy_not_call"))
                                TNotCon(9) += Convert.ToInt32(dr("roy_not_con"))
                                TNotEnd(9) += Convert.ToInt32(dr("roy_not_end"))

                                'TOT
                                ret.Append("        <td align='right'>" & Format(dr("tot_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_end"), "#,##0") & "</td>")
                                TRegis(10) += Convert.ToInt32(dr("tot_regis"))
                                TServe(10) += Convert.ToInt32(dr("tot_serve"))
                                TMissCall(10) += Convert.ToInt32(dr("tot_miss_call"))
                                TCancel(10) += Convert.ToInt32(dr("tot_cancel"))
                                TNotCall(10) += Convert.ToInt32(dr("tot_not_call"))
                                TNotCon(10) += Convert.ToInt32(dr("tot_not_con"))
                                TNotEnd(10) += Convert.ToInt32(dr("tot_not_end"))

                                'Pre-paid
                                ret.Append("        <td align='right'>" & Format(dr("pre_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_end"), "#,##0") & "</td>")
                                TRegis(11) += Convert.ToInt32(dr("pre_regis"))
                                TServe(11) += Convert.ToInt32(dr("pre_serve"))
                                TMissCall(11) += Convert.ToInt32(dr("pre_miss_call"))
                                TCancel(11) += Convert.ToInt32(dr("pre_cancel"))
                                TNotCall(11) += Convert.ToInt32(dr("pre_not_call"))
                                TNotCon(11) += Convert.ToInt32(dr("pre_not_con"))
                                TNotEnd(11) += Convert.ToInt32(dr("pre_not_end"))

                                'AIS
                                ret.Append("        <td align='right'>" & Format(dr("ais_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_end"), "#,##0") & "</td>")
                                ret.Append("    </tr>")
                                TRegis(12) += Convert.ToInt32(dr("ais_regis"))
                                TServe(12) += Convert.ToInt32(dr("ais_serve"))
                                TMissCall(12) += Convert.ToInt32(dr("ais_miss_call"))
                                TCancel(12) += Convert.ToInt32(dr("ais_cancel"))
                                TNotCall(12) += Convert.ToInt32(dr("ais_not_call"))
                                TNotCon(12) += Convert.ToInt32(dr("ais_not_con"))
                                TNotEnd(12) += Convert.ToInt32(dr("ais_not_end"))
                            Next



                            'Total By Shop 1 Service
                            ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                            ret.Append("        <td align='center' colspan='3'>Total " & shDr("shop_name_en").ToString & "</td>")
                            For i As Integer = 1 To 12
                                ret.Append("        <td align='right'>" & Format(TRegis(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TServe(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TMissCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TCancel(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCon(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotEnd(i), "#,##0") & "</td>")

                                GRegis(i) += TRegis(i)
                                GServe(i) += TServe(i)
                                GMissCall(i) += TMissCall(i)
                                GCancel(i) += TCancel(i)
                                GNotCall(i) += TNotCall(i)
                                GNotCon(i) += TNotCon(i)
                                GNotEnd(i) += TNotEnd(i)
                            Next
                            ret.Append("    </tr>")
                        Next
                        shDt.Dispose()
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("1. ReportApp_repCustomerByCategory.RenderReportByWeek", ex.Message & vbNewLine & ex.StackTrace)
                        lblerror.text = ex.Message & vbNewLine & ex.StackTrace
                    End Try
                Next
                sDt.Dispose()

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='3'>Grand Total</td>")
                For i As Integer = 1 To 12
                    ret.Append("        <td align='right'>" & Format(GRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCon(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotEnd(i), "#,##0") & "</td>")
                Next
                ret.Append("    </tr>")
                ret.Append("</table>")

                lblReportDesc.Text = ret.ToString
                lblerror.Visible = False
            Catch ex As Exception
                FunctionEng.SaveErrorLog("2. ReportApp_repCustomerByCategory.RenderReportByWeek", ex.Message & vbNewLine & ex.StackTrace)
                lblerror.text = ex.Message & vbNewLine & ex.StackTrace
            End Try
        Else
            btnExport.Visible = False
        End If
        dt.Dispose()
    End Sub


    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Try
                Dim GRegis(12) As Int32
                Dim GServe(12) As Int32
                Dim GMissCall(12) As Int32
                Dim GCancel(12) As Int32
                Dim GNotCall(12) As Int32
                Dim GNotCon(12) As Int32
                Dim GNotEnd(12) As Int32


                Dim ret As New StringBuilder
                'Header Row
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                Dim sDt As New DataTable
                sDt = dt.DefaultView.ToTable(True, "service_id", "service_name_en", "service_name_th")
                For Each sDr As DataRow In sDt.Rows
                    Try
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;width:100px' rowspan='4' >Shop Name</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' rowspan='4' >Month</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' rowspan='4' >Year</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='84' >" & sDr("service_name_en") & "</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Residential</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Business</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='28' >Government & Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >Exclusive</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='14' >In House</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Thai Citizen</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Foreigner</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Key Account</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >SME</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Government</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >State Enterprise</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Embassy</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Non Profit</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Royal</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >TOT</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >Pre-paid</td>")
                        ret.Append("        <td align='center' style='color: #ffffff;' colspan='7' >AIS</td>")
                        ret.Append("    </tr>")
                        ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                        For h As Integer = 1 To 12
                            ret.Append("        <td align='center' style='color: #ffffff;' >Regis</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Serve</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Missed call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Cancel</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Call</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not Confirm</td>")
                            ret.Append("        <td align='center' style='color: #ffffff;' >Not End</td>")
                        Next
                        ret.Append("    </tr>")

                        'Data Row By Shop
                        dt.DefaultView.RowFilter = "service_id='" & sDr("service_id") & "'"
                        Dim shDt As New DataTable
                        shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_th", "shop_name_en").Copy
                        For Each shDr As DataRow In shDt.Rows
                            Dim TRegis(12) As Int32
                            Dim TServe(12) As Int32
                            Dim TMissCall(12) As Int32
                            Dim TCancel(12) As Int32
                            Dim TNotCall(12) As Int32
                            Dim TNotCon(12) As Int32
                            Dim TNotEnd(12) As Int32

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and service_id='" & sDr("service_id") & "' "
                            For Each dr As DataRowView In dt.DefaultView
                                ret.Append("    <tr>")
                                ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                                ret.Append("        <td align='center'>" & dr("show_month") & "</td>")
                                ret.Append("        <td align='center'>" & dr("show_year") & "</td>")

                                'Thai Citizen
                                ret.Append("        <td align='right'>" & Format(dr("tha_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tha_not_end"), "#,##0") & "</td>")
                                TRegis(1) += Convert.ToInt32(dr("tha_regis"))
                                TServe(1) += Convert.ToInt32(dr("tha_serve"))
                                TMissCall(1) += Convert.ToInt32(dr("tha_miss_call"))
                                TCancel(1) += Convert.ToInt32(dr("tha_cancel"))
                                TNotCall(1) += Convert.ToInt32(dr("tha_not_call"))
                                TNotCon(1) += Convert.ToInt32(dr("tha_not_con"))
                                TNotEnd(1) += Convert.ToInt32(dr("tha_not_end"))

                                'Foreigner
                                ret.Append("        <td align='right'>" & Format(dr("for_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("for_not_end"), "#,##0") & "</td>")
                                TRegis(2) += Convert.ToInt32(dr("for_regis"))
                                TServe(2) += Convert.ToInt32(dr("for_serve"))
                                TMissCall(2) += Convert.ToInt32(dr("for_miss_call"))
                                TCancel(2) += Convert.ToInt32(dr("for_cancel"))
                                TNotCall(2) += Convert.ToInt32(dr("for_not_call"))
                                TNotCon(2) += Convert.ToInt32(dr("for_not_con"))
                                TNotEnd(2) += Convert.ToInt32(dr("for_not_end"))

                                'Key Account
                                ret.Append("        <td align='right'>" & Format(dr("key_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("key_not_end"), "#,##0") & "</td>")
                                TRegis(3) += Convert.ToInt32(dr("key_regis"))
                                TServe(3) += Convert.ToInt32(dr("key_serve"))
                                TMissCall(3) += Convert.ToInt32(dr("key_miss_call"))
                                TCancel(3) += Convert.ToInt32(dr("key_cancel"))
                                TNotCall(3) += Convert.ToInt32(dr("key_not_call"))
                                TNotCon(3) += Convert.ToInt32(dr("key_not_con"))
                                TNotEnd(3) += Convert.ToInt32(dr("key_not_end"))

                                'SME
                                ret.Append("        <td align='right'>" & Format(dr("sme_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sme_not_end"), "#,##0") & "</td>")
                                TRegis(4) += Convert.ToInt32(dr("sme_regis"))
                                TServe(4) += Convert.ToInt32(dr("sme_serve"))
                                TMissCall(4) += Convert.ToInt32(dr("sme_miss_call"))
                                TCancel(4) += Convert.ToInt32(dr("sme_cancel"))
                                TNotCall(4) += Convert.ToInt32(dr("sme_not_call"))
                                TNotCon(4) += Convert.ToInt32(dr("sme_not_con"))
                                TNotEnd(4) += Convert.ToInt32(dr("sme_not_end"))

                                'Government
                                ret.Append("        <td align='right'>" & Format(dr("gov_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("gov_not_end"), "#,##0") & "</td>")
                                TRegis(5) += Convert.ToInt32(dr("gov_regis"))
                                TServe(5) += Convert.ToInt32(dr("gov_serve"))
                                TMissCall(5) += Convert.ToInt32(dr("gov_miss_call"))
                                TCancel(5) += Convert.ToInt32(dr("gov_cancel"))
                                TNotCall(5) += Convert.ToInt32(dr("gov_not_call"))
                                TNotCon(5) += Convert.ToInt32(dr("gov_not_con"))
                                TNotEnd(5) += Convert.ToInt32(dr("gov_not_end"))

                                'State Enterprise
                                ret.Append("        <td align='right'>" & Format(dr("sta_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("sta_not_end"), "#,##0") & "</td>")
                                TRegis(6) += Convert.ToInt32(dr("sta_regis"))
                                TServe(6) += Convert.ToInt32(dr("sta_serve"))
                                TMissCall(6) += Convert.ToInt32(dr("sta_miss_call"))
                                TCancel(6) += Convert.ToInt32(dr("sta_cancel"))
                                TNotCall(6) += Convert.ToInt32(dr("sta_not_call"))
                                TNotCon(6) += Convert.ToInt32(dr("sta_not_con"))
                                TNotEnd(6) += Convert.ToInt32(dr("sta_not_end"))

                                'Embassy
                                ret.Append("        <td align='right'>" & Format(dr("emb_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("emb_not_end"), "#,##0") & "</td>")
                                TRegis(7) += Convert.ToInt32(dr("emb_regis"))
                                TServe(7) += Convert.ToInt32(dr("emb_serve"))
                                TMissCall(7) += Convert.ToInt32(dr("emb_miss_call"))
                                TCancel(7) += Convert.ToInt32(dr("emb_cancel"))
                                TNotCall(7) += Convert.ToInt32(dr("emb_not_call"))
                                TNotCon(7) += Convert.ToInt32(dr("emb_not_con"))
                                TNotEnd(7) += Convert.ToInt32(dr("emb_not_end"))

                                'Non Profit
                                ret.Append("        <td align='right'>" & Format(dr("non_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("non_not_end"), "#,##0") & "</td>")
                                TRegis(8) += Convert.ToInt32(dr("non_regis"))
                                TServe(8) += Convert.ToInt32(dr("non_serve"))
                                TMissCall(8) += Convert.ToInt32(dr("non_miss_call"))
                                TCancel(8) += Convert.ToInt32(dr("non_cancel"))
                                TNotCall(8) += Convert.ToInt32(dr("non_not_call"))
                                TNotCon(8) += Convert.ToInt32(dr("non_not_con"))
                                TNotEnd(8) += Convert.ToInt32(dr("non_not_end"))

                                'Royal
                                ret.Append("        <td align='right'>" & Format(dr("roy_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("roy_not_end"), "#,##0") & "</td>")
                                TRegis(9) += Convert.ToInt32(dr("roy_regis"))
                                TServe(9) += Convert.ToInt32(dr("roy_serve"))
                                TMissCall(9) += Convert.ToInt32(dr("roy_miss_call"))
                                TCancel(9) += Convert.ToInt32(dr("roy_cancel"))
                                TNotCall(9) += Convert.ToInt32(dr("roy_not_call"))
                                TNotCon(9) += Convert.ToInt32(dr("roy_not_con"))
                                TNotEnd(9) += Convert.ToInt32(dr("roy_not_end"))

                                'TOT
                                ret.Append("        <td align='right'>" & Format(dr("tot_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("tot_not_end"), "#,##0") & "</td>")
                                TRegis(10) += Convert.ToInt32(dr("tot_regis"))
                                TServe(10) += Convert.ToInt32(dr("tot_serve"))
                                TMissCall(10) += Convert.ToInt32(dr("tot_miss_call"))
                                TCancel(10) += Convert.ToInt32(dr("tot_cancel"))
                                TNotCall(10) += Convert.ToInt32(dr("tot_not_call"))
                                TNotCon(10) += Convert.ToInt32(dr("tot_not_con"))
                                TNotEnd(10) += Convert.ToInt32(dr("tot_not_end"))

                                'Pre-paid
                                ret.Append("        <td align='right'>" & Format(dr("pre_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("pre_not_end"), "#,##0") & "</td>")
                                TRegis(11) += Convert.ToInt32(dr("pre_regis"))
                                TServe(11) += Convert.ToInt32(dr("pre_serve"))
                                TMissCall(11) += Convert.ToInt32(dr("pre_miss_call"))
                                TCancel(11) += Convert.ToInt32(dr("pre_cancel"))
                                TNotCall(11) += Convert.ToInt32(dr("pre_not_call"))
                                TNotCon(11) += Convert.ToInt32(dr("pre_not_con"))
                                TNotEnd(11) += Convert.ToInt32(dr("pre_not_end"))

                                'AIS
                                ret.Append("        <td align='right'>" & Format(dr("ais_regis"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_serve"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_miss_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_cancel"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_call"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_con"), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(dr("ais_not_end"), "#,##0") & "</td>")
                                ret.Append("    </tr>")
                                TRegis(12) += Convert.ToInt32(dr("ais_regis"))
                                TServe(12) += Convert.ToInt32(dr("ais_serve"))
                                TMissCall(12) += Convert.ToInt32(dr("ais_miss_call"))
                                TCancel(12) += Convert.ToInt32(dr("ais_cancel"))
                                TNotCall(12) += Convert.ToInt32(dr("ais_not_call"))
                                TNotCon(12) += Convert.ToInt32(dr("ais_not_con"))
                                TNotEnd(12) += Convert.ToInt32(dr("ais_not_end"))
                            Next



                            'Total By Shop 1 Service
                            ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;'>")
                            ret.Append("        <td align='center' colspan='3'>Total " & shDr("shop_name_en").ToString & "</td>")
                            For i As Integer = 1 To 12
                                ret.Append("        <td align='right'>" & Format(TRegis(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TServe(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TMissCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TCancel(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCall(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotCon(i), "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(TNotEnd(i), "#,##0") & "</td>")

                                GRegis(i) += TRegis(i)
                                GServe(i) += TServe(i)
                                GMissCall(i) += TMissCall(i)
                                GCancel(i) += TCancel(i)
                                GNotCall(i) += TNotCall(i)
                                GNotCon(i) += TNotCon(i)
                                GNotEnd(i) += TNotEnd(i)
                            Next
                            ret.Append("    </tr>")
                        Next
                        shDt.Dispose()
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("1. ReportApp_repCustomerByCategory.RenderReportByMonth", ex.Message & vbNewLine & ex.StackTrace)
                        lblerror.text = ex.Message & vbNewLine & ex.StackTrace
                    End Try
                Next
                sDt.Dispose()

                'Grand Total All Service
                ret.Append("    <tr style='background: orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='3'>Grand Total</td>")
                For i As Integer = 1 To 12
                    ret.Append("        <td align='right'>" & Format(GRegis(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GServe(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GMissCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GCancel(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCall(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotCon(i), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(GNotEnd(i), "#,##0") & "</td>")
                Next
                ret.Append("    </tr>")
                ret.Append("</table>")

                lblReportDesc.Text = ret.ToString
                lblerror.Visible = False
            Catch ex As Exception
                FunctionEng.SaveErrorLog("2. ReportApp_repCustomerByCategory.RenderReportByMonth", ex.Message & vbNewLine & ex.StackTrace)
                lblerror.text = ex.Message & vbNewLine & ex.StackTrace
            End Try
        Else
            btnExport.Visible = False
        End If
        dt.Dispose()
    End Sub

  
End Class
