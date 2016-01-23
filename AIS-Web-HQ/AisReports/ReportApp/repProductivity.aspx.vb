Imports System.IO
Imports System.Data
Imports Engine.Common
Imports Engine.Reports
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser

Partial Class ReportApp_repProductivity
    Inherits System.Web.UI.Page

    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "ProductivityReport_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
    End Sub
    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repProductivity.ExportData", Config.GetLoginHistoryID)
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
        If ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim dt As New DataTable
            Dim eng As New Engine.Reports.RepProductivityENG
            dt = eng.GetReportByWeek(para)
            If dt.Rows.Count > 0 Then
                RenderReportByWeek(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByDate" Then
            Dim para As New CenParaDB.ReportCriteria.ProductivityPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepProductivityENG
            Dim dt As New DataTable
            dt = eng.GetRportDataByDate(para)
            If dt.Rows.Count > 0 Then
                RenderReportByDate(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.CustBySegmentPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim dt As New DataTable
            Dim eng As New Engine.Reports.RepProductivityENG
            dt = eng.GetReportByMonth(para)
            If dt.Rows.Count > 0 Then
                RenderReportByMonth(dt)
            Else
                btnExport.Visible = False
            End If
            dt.Dispose()
            eng = Nothing
            para = Nothing
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th").Copy
            If shDt.Rows.Count > 0 Then
                Dim GTarget() As Long
                Dim GActual() As Long
                Dim GAHT() As Long
                Dim GCount() As Integer
                Dim GTotTarget As Long = 0
                Dim GTotActure As Long = 0
                Dim GTotAHT As Long = 0
                Dim GTotCount As Integer = 0

                Dim maxService As Integer = 0

                Dim ret As New StringBuilder
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                For Each shDr As DataRow In shDt.Rows
                    'Header Row
                    ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")

                    'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
                    Dim sDt As New DataTable
                    sDt = ReportsENG.GetServiceAtShop(shDr("shop_id"))
                    If sDt.Rows.Count > 0 Then
                        For Each sDr As DataRow In sDt.Rows
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " Target</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " Actual</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " % Achieve To Target</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " AHT</td>")
                        Next
                    End If
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total Target</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total Actual</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total %  Achieve To Target</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total AHT</td>")
                    ret.Append("    </tr>")

                    maxService = sDt.Rows.Count - 1
                    ReDim GTarget(maxService)
                    ReDim GActual(maxService)
                    ReDim GAHT(maxService)
                    ReDim GCount(maxService)

                    Dim TTarget(sDt.Rows.Count - 1) As Long
                    Dim TActual(sDt.Rows.Count - 1) As Long
                    Dim TAHT(sDt.Rows.Count - 1) As Long
                    Dim TCount(sDt.Rows.Count - 1) As Integer
                    Dim TTotTarget As Long = 0
                    Dim TTotActure As Long = 0
                    Dim TTotAHT As Long = 0
                    Dim TTotCount As Integer = 0

                    'Data Row By Shop
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    dt.DefaultView.Sort = "show_year,month_no "

                    Dim mDt As New DataTable
                    mDt = dt.DefaultView.ToTable(True, "month_no", "show_month", "show_year")
                    If mDt.Rows.Count > 0 Then
                        For Each mDr As DataRow In mDt.Rows
                            Dim STarget(sDt.Rows.Count - 1) As Long
                            Dim SActual(sDt.Rows.Count - 1) As Long
                            Dim SAHT(sDt.Rows.Count - 1) As Long
                            Dim SCount(sDt.Rows.Count - 1) As Integer
                            Dim STotTarget As Long = 0
                            Dim STotActure As Long = 0
                            Dim STotAHT As Long = 0
                            Dim STotCount As Integer = 0

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and month_no='" & mDr("month_no") & "' and show_year='" & mDr("show_year") & "'"
                            For Each dr As DataRowView In dt.DefaultView
                                ret.Append("    <tr>")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>" & dr("show_month") & "</td>")
                                ret.Append("        <td align='left'>" & dr("show_year") & "</td>")
                                ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                                ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                                If sDt.Rows.Count > 0 Then
                                    Dim RowTmp As String() = Split(dr("data_value"), "###")
                                    Dim i As Integer = 0
                                    For Each sRow As String In RowTmp
                                        Dim DataRow As String() = Split(sRow, "|")
                                        sDt.DefaultView.RowFilter = "id='" & DataRow(0) & "'"

                                        If sDt.DefaultView.Count > 0 Then
                                            ret.Append("        <td align='right'>" & DataRow(3) & "</td>")
                                            ret.Append("        <td align='right'>" & DataRow(4) & "</td>")
                                            ret.Append("        <td align='right'>" & DataRow(5) & "%</td>")
                                            If DataRow(6).Trim = "" Then
                                                ret.Append("        <td align='right'>00:00:00</td>")
                                            Else
                                                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(DataRow(6)) & "</td>")
                                            End If

                                            If Convert.ToInt64(DataRow(4)) > 0 Then
                                                STarget(i) += Convert.ToInt64(DataRow(3))
                                                SActual(i) += Convert.ToInt64(DataRow(4))
                                                SAHT(i) += Convert.ToInt64(DataRow(6))
                                                SCount(i) += 1
                                                STotAHT += Convert.ToInt64(DataRow(7))
                                                STotCount += Convert.ToInt64(DataRow(8))

                                                TTarget(i) += Convert.ToInt64(DataRow(3))
                                                TActual(i) += Convert.ToInt64(DataRow(4))
                                                TAHT(i) += Convert.ToInt64(DataRow(6))
                                                TCount(i) += 1
                                                TTotAHT += Convert.ToInt64(DataRow(7))
                                                TTotCount += Convert.ToInt64(DataRow(8))

                                                GTarget(i) += Convert.ToInt64(DataRow(3))
                                                GActual(i) += Convert.ToInt64(DataRow(4))
                                                GAHT(i) += Convert.ToInt64(DataRow(6))
                                                GCount(i) += 1
                                                GTotAHT += Convert.ToInt64(DataRow(7))
                                                GTotCount += Convert.ToInt64(DataRow(8))
                                            End If
                                            'Else
                                            '    ret.Append("        <td align='right'>&nbsp;</td>")
                                            '    ret.Append("        <td align='right'>&nbsp;</td>")
                                            '    ret.Append("        <td align='right'>&nbsp;</td>")
                                            '    ret.Append("        <td align='right'>00:00:00</td>")
                                        End If
                                        i += 1
                                    Next
                                End If

                                'Total Column
                                ret.Append("        <td align='right'>" & dr("total_target") & "</td>")
                                ret.Append("        <td align='right'>" & dr("total_actual") & "</td>")
                                ret.Append("        <td align='right'>" & dr("total_per_achieve_target") & "%</td>")
                                If Convert.ToInt32(dr("total_aht")) = 0 Then
                                    ret.Append("        <td align='right'>00:00:00</td>")
                                Else
                                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("total_aht")) & "</td>")
                                End If
                                ret.Append("    </tr>")

                                If Convert.ToInt64(dr("total_actual")) > 0 Then
                                    STotTarget += Convert.ToInt64(dr("total_target"))
                                    STotActure += Convert.ToInt64(dr("total_actual"))

                                    TTotTarget += Convert.ToInt64(dr("total_target"))
                                    TTotActure += Convert.ToInt64(dr("total_actual"))

                                    GTotTarget += Convert.ToInt64(dr("total_target"))
                                    GTotActure += Convert.ToInt64(dr("total_actual"))
                                End If
                            Next

                            'Sub Total By Month
                            ret.Append("    <tr style='background: #E4E4E4;'>")
                            ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                            ret.Append("        <td align='center' >" & mDr("show_month") & "</td>")
                            ret.Append("        <td align='center' >" & mDr("show_year") & "</td>")
                            ret.Append("        <td align='center' >Sub Total</td>")
                            ret.Append("        <td align='center' >&nbsp;</td>")

                            Dim tI As Integer = 0
                            For Each sDr As DataRow In sDt.Rows
                                ret.Append("        <td  align='right' >" & Format(STarget(tI), "#,##0") & "</td>")
                                ret.Append("        <td  align='right' >" & Format(SActual(tI), "#,##0") & "</td>")

                                If STarget(tI) > 0 Then
                                    ret.Append("        <td  align='right' >" & Format((SActual(tI) / STarget(tI) * 100), "#,##0") & "%</td>")
                                Else
                                    ret.Append("        <td  align='right' >0%</td>")
                                End If
                                If SCount(tI) > 0 Then
                                    ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(SAHT(tI) / SCount(tI)) & "</td>")
                                Else
                                    ret.Append("        <td  align='right' >00:00:00</td>")
                                End If
                                tI += 1
                            Next
                            ret.Append("        <td  align='right' >" & Format(STotTarget, "#,##0") & "</td>")
                            ret.Append("        <td  align='right' >" & Format(STotActure, "#,##0") & "</td>")
                            If STotTarget > 0 Then
                                ret.Append("        <td  align='right' >" & Format((STotActure / STotTarget) * 100, "#,##0") & "%</td>")
                            Else
                                ret.Append("        <td  align='right' >0%</td>")
                            End If
                            If STotCount > 0 Then
                                ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(STotAHT / STotCount) & "</td>")
                            Else
                                ret.Append("        <td  align='right' >00:00:00</td>")
                            End If
                        Next
                    End If
                    'Sub Total By Shop
                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' colspan='5' >Total " & shDr("shop_name_en") & "</td>")

                    Dim sI As Integer = 0
                    For Each sDr As DataRow In sDt.Rows
                        ret.Append("        <td  align='right' >" & Format(TTarget(sI), "#,##0") & "</td>")
                        ret.Append("        <td  align='right' >" & Format(TActual(sI), "#,##0") & "</td>")

                        If TTarget(sI) > 0 Then
                            ret.Append("        <td  align='right' >" & Format((TActual(sI) / TTarget(sI) * 100), "#,##0") & "%</td>")
                        Else
                            ret.Append("        <td  align='right' >0%</td>")
                        End If
                        If TCount(sI) > 0 Then
                            ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(TAHT(sI) / TCount(sI)) & "</td>")
                        Else
                            ret.Append("        <td  align='right' >00:00:00</td>")
                        End If
                        sI += 1
                    Next
                    ret.Append("        <td  align='right' >" & Format(TTotTarget, "#,##0") & "</td>")
                    ret.Append("        <td  align='right' >" & Format(TTotActure, "#,##0") & "</td>")
                    If TTotTarget > 0 Then
                        ret.Append("        <td  align='right' >" & Format((TTotActure / TTotTarget) * 100, "#,##0") & "%</td>")
                    Else
                        ret.Append("        <td  align='right' >0%</td>")
                    End If
                    If TTotCount > 0 Then
                        ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(TTotAHT / TTotCount) & "</td>")
                    Else
                        ret.Append("        <td  align='right' >00:00:00</td>")
                    End If
                Next

                'Grand Total
                ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='5' >Grand Total</td>")

                For i As Integer = 0 To maxService
                    ret.Append("        <td  align='right' >" & Format(GTarget(i), "#,##0") & "</td>")
                    ret.Append("        <td  align='right' >" & Format(GActual(i), "#,##0") & "</td>")

                    If GTarget(i) > 0 Then
                        ret.Append("        <td  align='right' >" & Format((GActual(i) / GTarget(i) * 100), "#,##0") & "%</td>")
                    Else
                        ret.Append("        <td  align='right' >0%</td>")
                    End If
                    If GCount(i) > 0 Then
                        ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(GAHT(i) / GCount(i)) & "</td>")
                    Else
                        ret.Append("        <td  align='right' >00:00:00</td>")
                    End If
                Next
                ret.Append("        <td  align='right' >" & Format(GTotTarget, "#,##0") & "</td>")
                ret.Append("        <td  align='right' >" & Format(GTotActure, "#,##0") & "</td>")
                If GTotTarget > 0 Then
                    ret.Append("        <td  align='right' >" & Format((GTotActure / GTotTarget) * 100, "#,##0") & "%</td>")
                Else
                    ret.Append("        <td  align='right' >0%</td>")
                End If
                If GTotCount > 0 Then
                    ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(GTotAHT / GTotCount) & "</td>")
                Else
                    ret.Append("        <td  align='right' >00:00:00</td>")
                End If
                ret.Append("</table>")
                lblReportDesc.Text = ret.ToString
                ret = Nothing
            End If

            If lblReportDesc.Text.Trim <> "" Then
                lblerror.Visible = False
            End If
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th").Copy
            If shDt.Rows.Count > 0 Then
                Dim GTarget() As Long
                Dim GActual() As Long
                Dim GAHT() As Long
                Dim GCount() As Integer
                Dim GTotTarget As Long = 0
                Dim GTotActure As Long = 0
                Dim GTotAHT As Long = 0
                Dim GTotCount As Integer = 0

                Dim maxService As Integer = 0

                Dim ret As New StringBuilder
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                For Each shDr As DataRow In shDt.Rows
                    'Header Row
                    ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Week No.</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")

                    'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
                    Dim sDt As New DataTable
                    sDt = ReportsENG.GetServiceAtShop(shDr("shop_id"))
                    If sDt.Rows.Count > 0 Then
                        For Each sDr As DataRow In sDt.Rows
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " Target</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " Actual</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " % Achieve To Target</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " AHT</td>")
                        Next
                    End If
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total Target</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total Actual</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total %  Achieve To Target</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total AHT</td>")
                    ret.Append("    </tr>")

                    maxService = sDt.Rows.Count - 1
                    ReDim GTarget(maxService)
                    ReDim GActual(maxService)
                    ReDim GAHT(maxService)
                    ReDim GCount(maxService)

                    Dim TTarget(sDt.Rows.Count - 1) As Long
                    Dim TActual(sDt.Rows.Count - 1) As Long
                    Dim TAHT(sDt.Rows.Count - 1) As Long
                    Dim TCount(sDt.Rows.Count - 1) As Integer
                    Dim TTotTarget As Long = 0
                    Dim TTotActure As Long = 0
                    Dim TTotAHT As Long = 0
                    Dim TTotCount As Integer = 0


                    'Data Row By Shop
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    Dim wDt As New DataTable
                    wDt = dt.DefaultView.ToTable(True, "week_of_year", "show_year")
                    If wDt.Rows.Count > 0 Then
                        For Each wDr As DataRow In wDt.Rows
                            Dim STarget(sDt.Rows.Count - 1) As Long
                            Dim SActual(sDt.Rows.Count - 1) As Long
                            Dim SAHT(sDt.Rows.Count - 1) As Long
                            Dim SCount(sDt.Rows.Count - 1) As Integer
                            Dim STotTarget As Long = 0
                            Dim STotActure As Long = 0
                            Dim STotAHT As Long = 0
                            Dim STotCount As Integer = 0

                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and week_of_year='" & wDr("week_of_year") & "' and show_year='" & wDr("show_year") & "'"
                            For Each dr As DataRowView In dt.DefaultView
                                ret.Append("    <tr>")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>" & dr("week_of_year") & "</td>")
                                ret.Append("        <td align='left'>" & dr("show_year") & "</td>")
                                ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                                ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                                If sDt.Rows.Count > 0 Then
                                    Dim RowTmp As String() = Split(dr("data_value"), "###")
                                    Dim i As Integer = 0
                                    For Each sRow As String In RowTmp
                                        Dim DataRow As String() = Split(sRow, "|")
                                        sDt.DefaultView.RowFilter = "id='" & DataRow(0) & "'"

                                        If sDt.DefaultView.Count > 0 Then
                                            ret.Append("        <td align='right'>" & DataRow(3) & "</td>")
                                            ret.Append("        <td align='right'>" & DataRow(4) & "</td>")
                                            ret.Append("        <td align='right'>" & DataRow(5) & "%</td>")
                                            If DataRow(6).Trim = "" Then
                                                ret.Append("        <td align='right'>00:00:00</td>")
                                            Else
                                                ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(DataRow(6)) & "</td>")
                                            End If

                                            If Convert.ToInt64(DataRow(4)) > 0 Then
                                                STarget(i) += Convert.ToInt64(DataRow(3))
                                                SActual(i) += Convert.ToInt64(DataRow(4))
                                                SAHT(i) += Convert.ToInt64(DataRow(6))
                                                SCount(i) += 1
                                                STotAHT += Convert.ToInt64(DataRow(7))
                                                STotCount += Convert.ToInt64(DataRow(8))

                                                TTarget(i) += Convert.ToInt64(DataRow(3))
                                                TActual(i) += Convert.ToInt64(DataRow(4))
                                                TAHT(i) += Convert.ToInt64(DataRow(6))
                                                TCount(i) += 1
                                                TTotAHT += Convert.ToInt64(DataRow(7))
                                                TTotCount += Convert.ToInt64(DataRow(8))

                                                GTarget(i) += Convert.ToInt64(DataRow(3))
                                                GActual(i) += Convert.ToInt64(DataRow(4))
                                                GAHT(i) += Convert.ToInt64(DataRow(6))
                                                GCount(i) += 1
                                                GTotAHT += Convert.ToInt64(DataRow(7))
                                                GTotCount += Convert.ToInt64(DataRow(8))
                                            End If
                                        End If
                                        i += 1
                                    Next
                                End If

                                'Total Column
                                ret.Append("        <td align='right'>" & dr("total_target") & "</td>")
                                ret.Append("        <td align='right'>" & dr("total_actual") & "</td>")
                                ret.Append("        <td align='right'>" & dr("total_per_achieve_target") & "%</td>")
                                If Convert.ToInt32(dr("total_aht")) = 0 Then
                                    ret.Append("        <td align='right'>00:00:00</td>")
                                Else
                                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(dr("total_aht")) & "</td>")
                                End If
                                ret.Append("    </tr>")

                                If Convert.ToInt64(dr("total_actual")) > 0 Then
                                    STotTarget += Convert.ToInt64(dr("total_target"))
                                    STotActure += Convert.ToInt64(dr("total_actual"))

                                    TTotTarget += Convert.ToInt64(dr("total_target"))
                                    TTotActure += Convert.ToInt64(dr("total_actual"))
                                    
                                    GTotTarget += Convert.ToInt64(dr("total_target"))
                                    GTotActure += Convert.ToInt64(dr("total_actual"))
                                End If
                            Next

                            'Sub Total By Week
                            ret.Append("    <tr style='background: #E4E4E4;'>")
                            ret.Append("        <td align='center' >" & shDr("shop_name_en") & "</td>")
                            ret.Append("        <td align='center' >" & wDr("week_of_year") & "</td>")
                            ret.Append("        <td align='center' >" & wDr("show_year") & "</td>")
                            ret.Append("        <td align='center' >Sub Total</td>")
                            ret.Append("        <td align='center' >&nbsp;</td>")

                            Dim tI As Integer = 0
                            For Each sDr As DataRow In sDt.Rows
                                ret.Append("        <td  align='right' >" & Format(STarget(tI), "#,##0") & "</td>")
                                ret.Append("        <td  align='right' >" & Format(SActual(tI), "#,##0") & "</td>")

                                If STarget(tI) > 0 Then
                                    ret.Append("        <td  align='right' >" & Format((SActual(tI) / STarget(tI) * 100), "#,##0") & "%</td>")
                                Else
                                    ret.Append("        <td  align='right' >0%</td>")
                                End If
                                If SCount(tI) > 0 Then
                                    ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(SAHT(tI) / SCount(tI)) & "</td>")
                                Else
                                    ret.Append("        <td  align='right' >00:00:00</td>")
                                End If
                                tI += 1
                            Next
                            ret.Append("        <td  align='right' >" & Format(STotTarget, "#,##0") & "</td>")
                            ret.Append("        <td  align='right' >" & Format(STotActure, "#,##0") & "</td>")
                            If STotTarget > 0 Then
                                ret.Append("        <td  align='right' >" & Format((STotActure / STotTarget) * 100, "#,##0") & "%</td>")
                            Else
                                ret.Append("        <td  align='right' >0%</td>")
                            End If
                            If STotCount > 0 Then
                                ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(STotAHT / STotCount) & "</td>")
                            Else
                                ret.Append("        <td  align='right' >00:00:00</td>")
                            End If
                        Next
                    End If
                    'Sub Total By Shop
                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' colspan='5' >Total " & shDr("shop_name_en") & "</td>")

                    Dim sI As Integer = 0
                    For Each sDr As DataRow In sDt.Rows
                        ret.Append("        <td  align='right' >" & Format(TTarget(sI), "#,##0") & "</td>")
                        ret.Append("        <td  align='right' >" & Format(TActual(sI), "#,##0") & "</td>")

                        If TTarget(sI) > 0 Then
                            ret.Append("        <td  align='right' >" & Format((TActual(sI) / TTarget(sI) * 100), "#,##0") & "%</td>")
                        Else
                            ret.Append("        <td  align='right' >0%</td>")
                        End If
                        If TCount(sI) > 0 Then
                            ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(TAHT(sI) / TCount(sI)) & "</td>")
                        Else
                            ret.Append("        <td  align='right' >00:00:00</td>")
                        End If
                        sI += 1
                    Next
                    ret.Append("        <td  align='right' >" & Format(TTotTarget, "#,##0") & "</td>")
                    ret.Append("        <td  align='right' >" & Format(TTotActure, "#,##0") & "</td>")
                    If TTotTarget > 0 Then
                        ret.Append("        <td  align='right' >" & Format((TTotActure / TTotTarget) * 100, "#,##0") & "%</td>")
                    Else
                        ret.Append("        <td  align='right' >0%</td>")
                    End If
                    If TTotCount > 0 Then
                        ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(TTotAHT / TTotCount) & "</td>")
                    Else
                        ret.Append("        <td  align='right' >00:00:00</td>")
                    End If
                Next

                'Grand Total
                ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' colspan='5' >Grand Total</td>")

                For i As Integer = 0 To maxService
                    ret.Append("        <td  align='right' >" & Format(GTarget(i), "#,##0") & "</td>")
                    ret.Append("        <td  align='right' >" & Format(GActual(i), "#,##0") & "</td>")

                    If GTarget(i) > 0 Then
                        ret.Append("        <td  align='right' >" & Format((GActual(i) / GTarget(i) * 100), "#,##0") & "%</td>")
                    Else
                        ret.Append("        <td  align='right' >0%</td>")
                    End If
                    If GCount(i) > 0 Then
                        ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(GAHT(i) / GCount(i)) & "</td>")
                    Else
                        ret.Append("        <td  align='right' >00:00:00</td>")
                    End If
                Next
                ret.Append("        <td  align='right' >" & Format(GTotTarget, "#,##0") & "</td>")
                ret.Append("        <td  align='right' >" & Format(GTotActure, "#,##0") & "</td>")
                If GTotTarget > 0 Then
                    ret.Append("        <td  align='right' >" & Format((GTotActure / GTotTarget) * 100, "#,##0") & "%</td>")
                Else
                    ret.Append("        <td  align='right' >0%</td>")
                End If
                If GTotCount > 0 Then
                    ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(GTotAHT / GTotCount) & "</td>")
                Else
                    ret.Append("        <td  align='right' >00:00:00</td>")
                End If
                ret.Append("</table>")
                lblReportDesc.Text = ret.ToString
                ret = Nothing
            End If

            If lblReportDesc.Text.Trim <> "" Then
                lblerror.Visible = False
            End If
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        'Dim ret As String = ""
        If dt.Rows.Count > 0 Then
            Dim shDt As New DataTable
            shDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th").Copy
            If shDt.Rows.Count > 0 Then
                Dim GTarget() As Long
                Dim GActual() As Long
                Dim GAHT() As Long
                Dim GSumHT() As Long
                Dim GCount() As Integer
                Dim GTotTarget As Long = 0
                Dim GTotActual As Long = 0
                Dim GTotAHT As Long = 0
                Dim GTotCount As Integer = 0
                Dim GTotSumHT As Long = 0
                Dim maxService As Integer = 0

                Dim sDt As New DataTable
                'จำนวน Service ที่แสดงตรงชื่อคอลัมน์
                'sDt = ReportsENG.GetServiceAtShop(shDt.Rows(0)("shop_id"))
                sDt = ReportsENG.GetDataServiceList(shDt)
                If sDt.Rows.Count > 0 Then
                    maxService = sDt.Rows.Count - 1
                    ReDim GTarget(maxService)
                    ReDim GActual(maxService)
                    ReDim GAHT(maxService)
                    ReDim GSumHT(maxService)
                    ReDim GCount(maxService)
                End If

                Dim ret As New StringBuilder
                ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
                For Each shDr As DataRow In shDt.Rows
                    'Header Row
                    ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >Staff Name</td>")
                    ret.Append("        <td align='center' style='color: #ffffff;' >EmpID</td>")
                    If sDt.Rows.Count > 0 Then
                        For Each sDr As DataRow In sDt.Rows
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " Target</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " Actual</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " % Achieve To Target</td>")
                            ret.Append("        <td  align='center' style='color: #ffffff;' >" & sDr("item_name") & " AHT</td>")
                        Next
                    End If
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total Target</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total Actual</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total %  Achieve To Target</td>")
                    ret.Append("        <td  align='center' style='color: #ffffff;' >Total AHT</td>")
                    ret.Append("    </tr>")

                    

                    Dim TTarget(maxService) As Long
                    Dim TActual(maxService) As Long
                    Dim TAHT(maxService) As Long
                    Dim TSumHT(maxService) As Long
                    Dim TCount(maxService) As Integer
                    Dim TTotTarget As Long = 0
                    Dim TTotActual As Long = 0
                    Dim TTotAHT As Long = 0
                    Dim TTotCount As Integer = 0
                    Dim TTotSumHT As Long = 0

                    'Data Row By Shop
                    dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "'"
                    dt.DefaultView.Sort = "service_date, staff_name"

                    Dim dDt As New DataTable
                    dDt = dt.DefaultView.ToTable(True, "show_date", "show_year")
                    If dDt.Rows.Count > 0 Then
                        For Each dDr As DataRow In dDt.Rows
                            Dim STarget(maxService) As Long
                            Dim SActual(maxService) As Long
                            Dim SAHT(maxService) As Long
                            Dim SCount(maxService) As Integer
                            Dim SSumHT(maxService) As Long

                            Dim STotTarget As Long = 0
                            Dim STotActual As Long = 0
                            Dim STotAHT As Long = 0
                            Dim STotCount As Integer = 0
                            Dim STotSumHT As Long = 0


                            dt.DefaultView.RowFilter = "shop_id='" & shDr("shop_id") & "' and show_date='" & dDr("show_date") & "' and show_year='" & dDr("show_year") & "'"
                            For Each dr As DataRowView In dt.DefaultView
                                ret.Append("    <tr>")
                                ret.Append("        <td align='left'>" & shDr("shop_name_en") & "</td>")
                                ret.Append("        <td align='left'>" & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & "</td>")
                                ret.Append("        <td align='left'>" & dr("staff_name") & "</td>")
                                ret.Append("        <td align='left'>" & dr("user_code") & "</td>")
                                If sDt.Rows.Count > 0 Then
                                    Dim RowTmp = Split(dr("data_value"), "###")
                                    Dim i As Integer = 0
                                    Dim TotTarget As Long = 0
                                    Dim TotActual As Long = 0
                                    Dim TotAht As Long = 0
                                    Dim TotCount As Integer = 0
                                    Dim TotHT As Long = 0
                                    Dim TotSumCount As Integer = 0

                                    For Each sRow As String In RowTmp
                                        Dim DataRow As String() = Split(sRow, "|")
                                        sDt.DefaultView.RowFilter = "id='" & DataRow(0) & "'"

                                        If sDt.DefaultView.Count > 0 Then
                                            ret.Append("        <td align='right'>" & DataRow(3) & "</td>")
                                            ret.Append("        <td align='right'>" & DataRow(4) & "</td>")
                                            ret.Append("        <td align='right'>" & DataRow(5) & "%</td>")
                                            If DataRow(6).Trim = "" Then
                                                ret.Append("        <td align='right'>00:00:00</td>")
                                            Else
                                                ret.Append("        <td align='right'>" & DataRow(6) & "</td>")
                                            End If

                                            If Convert.ToInt64(DataRow(4)) > 0 Then
                                                If DataRow.Length = 10 Then
                                                    TotTarget = Convert.ToInt64(DataRow(9))
                                                End If
                                                TotActual += Convert.ToInt64(DataRow(4))
                                                TotAht += Convert.ToInt64(ReportsENG.GetSecFromTimeFormat(DataRow(6)))
                                                TotHT += Convert.ToInt64(DataRow(7))
                                                TotCount += 1
                                                TotSumCount += Convert.ToInt64(DataRow(8))


                                                STarget(i) += Convert.ToInt64(DataRow(3))
                                                SActual(i) += Convert.ToInt64(DataRow(4))
                                                SAHT(i) += Convert.ToInt64(ReportsENG.GetSecFromTimeFormat(DataRow(6)))
                                                SCount(i) += Convert.ToInt64(DataRow(8))
                                                SSumHT(i) += Convert.ToInt64(DataRow(7))

                                                TTarget(i) += Convert.ToInt64(DataRow(3))
                                                TActual(i) += Convert.ToInt64(DataRow(4))
                                                TAHT(i) += Convert.ToInt64(ReportsENG.GetSecFromTimeFormat(DataRow(6)))
                                                TCount(i) += Convert.ToInt64(DataRow(8))
                                                TSumHT(i) += Convert.ToInt64(DataRow(7))

                                                GTarget(i) += Convert.ToInt64(DataRow(3))
                                                GActual(i) += Convert.ToInt64(DataRow(4))
                                                GAHT(i) += Convert.ToInt64(ReportsENG.GetSecFromTimeFormat(DataRow(6)))
                                                GCount(i) += Convert.ToInt64(DataRow(8))
                                                GSumHT(i) += Convert.ToInt64(DataRow(7))
                                            End If
                                        End If
                                        i += 1
                                    Next
                                    ret.Append("        <td align='right'>" & TotTarget & "</td>")
                                    ret.Append("        <td align='right'>" & TotActual & "</td>")
                                    If TotTarget > 0 Then
                                        ret.Append("        <td align='right'>" & Format(((TotActual / TotTarget) * 100), "##0") & "%</td>")
                                    Else
                                        ret.Append("        <td align='right'>0%</td>")
                                    End If
                                    If TotCount > 0 Then
                                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(Format(TotAht / TotCount, "##0")) & "</td>")
                                    Else
                                        ret.Append("        <td align='right'>00:00:00</td>")
                                    End If

                                    STotTarget += TotTarget
                                    STotActual += TotActual
                                    STotAHT += TotAht
                                    STotCount += TotSumCount
                                    STotSumHT += TotHT
                                End If
                                ret.Append("    </tr>")
                            Next

                            '******************** Sub Total By Date ********************
                            '#E4E4E4
                            ret.Append("    <tr style='background: #E4E4E4;'>")
                            ret.Append("        <td colspan='4' align='center' >Sub Total</td>")

                            Dim sI As Integer = 0
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    ret.Append("        <td  align='right' >" & Format(STarget(sI), "#,##0") & "</td>")
                                    ret.Append("        <td  align='right' >" & Format(SActual(sI), "#,##0") & "</td>")

                                    If STarget(sI) > 0 Then
                                        ret.Append("        <td  align='right' >" & Format((SActual(sI) * 100) / STarget(sI), "##0") & "%</td>")
                                    Else
                                        ret.Append("        <td  align='right' >0%</td>")
                                    End If
                                    If SCount(sI) > 0 Then
                                        ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(SSumHT(sI) / SCount(sI)) & "</td>")
                                    Else
                                        ret.Append("        <td  align='right' >00:00:00</td>")
                                    End If
                                    sI += 1
                                Next
                                ret.Append("        <td align='right'>" & Format(STotTarget, "#,##0") & "</td>")
                                ret.Append("        <td align='right'>" & Format(STotActual, "#,##0") & "</td>")
                                If STotTarget = 0 Then
                                    ret.Append("        <td  align='right' >0%</td>")
                                Else
                                    ret.Append("        <td align='right'>" & Format(((STotActual * 100) / STotTarget), "##0") & "%</td>")
                                End If
                                If STotCount = 0 Then
                                    ret.Append("        <td  align='right' >00:00:00</td>")
                                Else
                                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(Format(STotSumHT / STotCount, "##0")) & "</td>")
                                End If
                                ret.Append("    </tr>")
                            End If

                            TTotTarget += STotTarget
                            TTotActual += STotActual
                            TTotAHT += STotAHT
                            TTotCount += STotCount
                            TTotSumHT += STotSumHT
                        Next
                    End If
                    dDt.Dispose()

                    '************************************************************
                    '************************ Total By Shop ***************************
                    ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                    ret.Append("        <td align='center' colspan='4' >Total " & shDr("shop_name_en") & "</td>")
                    Dim d As Integer = 0
                    For Each sDr As DataRow In sDt.Rows
                        ret.Append("        <td  align='right' >" & Format(CDbl(TTarget(d)), "#,##0") & "</td>")
                        ret.Append("        <td  align='right' >" & Format(CDbl(TActual(d)), "#,##0") & "</td>")
                        If TTarget(d) = 0 Then
                            ret.Append("        <td  align='right' >0%</td>")
                        Else
                            ret.Append("        <td  align='right' >" & Format((TActual(d) * 100) / TTarget(d), "##0") & "%</td>")
                        End If
                        If TCount(d) = 0 Then
                            ret.Append("        <td  align='right' >00:00:00</td>")
                        Else
                            ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(Format(TSumHT(d) / TCount(d), "##0")) & "</td>")
                        End If
                        d += 1
                    Next
                    ret.Append("        <td align='right'>" & Format(TTotTarget, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(TTotActual, "#,##0") & "</td>")
                    If TTotTarget = 0 Then
                        ret.Append("        <td align='right'>0%</td>")
                    Else
                        ret.Append("        <td align='right'>" & Format((TTotActual * 100) / TTotTarget, "##0") & "%</td>")
                    End If
                    If TTotCount = 0 Then
                        ret.Append("        <td align='right'>00:00:00</td>")
                    Else
                        ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(Format(TTotSumHT / TTotCount, "##0")) & "</td>")
                    End If
                    ret.Append("    </tr>")
                    '************************************************************
                    GTotTarget += TTotTarget
                    GTotActual += TTotActual
                    GTotAHT += TTotAHT
                    GTotCount += TTotCount
                    GTotSumHT += TTotSumHT
                Next
                '****************************************************************
                '************************************************************
                '************************ Grand Total ***************************
                ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;'>")
                ret.Append("        <td align='center' colspan='4' >Grand Total</td>")
                Dim k As Integer = 0
                For Each sDr As DataRow In sDt.Rows
                    ret.Append("        <td  align='right' >" & Format(CDbl(GTarget(k)), "#,##0") & "</td>")
                    ret.Append("        <td  align='right' >" & Format(CDbl(GActual(k)), "#,##0") & "</td>")
                    If GTarget(k) = 0 Then
                        ret.Append("        <td  align='right' >0%</td>")
                    Else
                        ret.Append("        <td  align='right' >" & Format((GActual(k) * 100) / GTarget(k), "##0") & "%</td>")
                    End If
                    If GCount(k) = 0 Then
                        ret.Append("        <td  align='right' >00:00:00</td>")
                    Else
                        ret.Append("        <td  align='right' >" & ReportsENG.GetFormatTimeFromSec(Format(GSumHT(k) / GCount(k), "##0")) & "</td>")
                    End If
                    k += 1
                Next
                ret.Append("        <td align='right'>" & Format(GTotTarget, "#,##0") & "</td>")
                ret.Append("        <td align='right'>" & Format(GTotActual, "#,##0") & "</td>")
                If GTotTarget = 0 Then
                    ret.Append("        <td align='right'>0%</td>")
                Else
                    ret.Append("        <td align='right'>" & Format((gTotActual * 100) / GTotTarget, "##0") & "%</td>")
                End If
                If GTotCount = 0 Then
                    ret.Append("        <td align='right'>00:00:00</td>")
                Else
                    ret.Append("        <td align='right'>" & ReportsENG.GetFormatTimeFromSec(Format(gTotSumHT / GTotCount, "##0")) & "</td>")
                End If
                ret.Append("    </tr>")
                '************************************************************
                ret.Append("</table>")
                lblReportDesc.Text = ret.ToString
                ret = Nothing
            End If
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    'Function GetAHT(ByVal ShopID As Int32, ByVal UserID As Int32, ByVal ServiceID As Int32, ByVal Showdate As String) As String
    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Dim lnq As New CenLinqDB.TABLE.TbRepProductivityDayCenLinqDB
    '    Dim dt As New DataTable
    '    Dim sql As String = ""
    '    Dim ret As Int32 = 0
    '    sql = "select * from TB_REP_WT_HT_SHOP_DAY where 1 = 1 "
    '    dt = lnq.GetListBySql(sql, Nothing)

    '    If ShopID > 0 Then
    '        sql += " and shop_id = " & ShopID
    '    End If
    '    If Showdate <> "" Then
    '        sql += " and show_date = '" & Showdate & "'"
    '    End If
    '    dt = lnq.GetListBySql(sql, Nothing)
    '    If dt.Rows.Count > 0 Then
    '        Dim RowTmp() As String = Split(dt.Rows(0).Item("data_value"), "###")
    '        For i As Int32 = 0 To RowTmp.Length - 1
    '            Dim tmp() As String = Split(RowTmp(i), "|")
    '            If tmp(0) = ServiceID Then
    '                Return (tmp(5))
    '            End If
    '        Next
    '    End If
    '    Return ret
    'End Function

  
End Class
