Imports System.IO
Imports System.Data
Imports Engine.Common
Imports Engine.Reports

Partial Class ReportApp_repAppointmentReportByShop
    Inherits System.Web.UI.Page

    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "AppointmentReportByShop_" & Now.ToString("yyyyMMddHHmmssfff") & ".xls")
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
        If ReportName = "ByTime" Then
            Dim para As New CenParaDB.ReportCriteria.AppointmentReportByShopPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            para.IntervalMinute = Request("Interval")
            para.TimePeroidFrom = Request("TimeFrom")
            para.TimePeroidTo = Request("TimeTo")
            Dim eng As New Engine.Reports.RepAppointmentReportByShopENG
            Dim dt As New DataTable
            dt = eng.GetReportDataTime(para)
            If dt.Rows.Count > 0 Then
                RenderReportByTime(dt)
            Else
                btnExport.Visible = False
            End If

        ElseIf ReportName = "ByWeek" Then
            Dim para As New CenParaDB.ReportCriteria.AppointmentReportByShopPara
            para.ShopID = Request("ShopID")
            para.WeekInYearFrom = Request("WeekFrom")
            para.WeekInYearTo = Request("WeekTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim dt As New DataTable
            Dim eng As New Engine.Reports.RepAppointmentReportByShopENG
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
            Dim para As New CenParaDB.ReportCriteria.AppointmentReportByShopPara
            para.ShopID = Request("ShopID")
            para.DateFrom = Request("DateFrom")
            para.DateTo = Request("DateTo")
            Dim eng As New Engine.Reports.RepAppointmentReportByShopENG
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
        ElseIf ReportName = "ByMonth" Then
            Dim para As New CenParaDB.ReportCriteria.AppointmentReportByShopPara
            para.ShopID = Request("ShopID")
            para.MonthFrom = Request("MonthFrom")
            para.MonthTo = Request("MonthTo")
            para.YearFrom = Request("YearFrom")
            para.YearTo = Request("YearTo")
            Dim dt As New DataTable
            Dim eng As New Engine.Reports.RepAppointmentReportByShopENG
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

    Private Sub RenderReportByTime(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Time</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Total</td>")
            ret.Append("    </tr>")

            Dim GTotal As Long = 0
            Dim GKioskShow As Long = 0
            Dim GKioskNoShow As Long = 0
            Dim GKioskTotal As Long = 0
            Dim GWebShow As Long = 0
            Dim GWebNoShow As Long = 0
            Dim GWebTotal As Long = 0
            Dim GMobileShow As Long = 0
            Dim GMobileNoShow As Long = 0
            Dim GMobileTotal As Long = 0

            'Data Row By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TTotal As Long = 0
                Dim TKioskShow As Long = 0
                Dim TKioskNoShow As Long = 0
                Dim TKioskTotal As Long = 0
                Dim TWebShow As Long = 0
                Dim TWebNoShow As Long = 0
                Dim TWebTotal As Long = 0
                Dim TMobileShow As Long = 0
                Dim TMobileNoShow As Long = 0
                Dim TMobileTotal As Long = 0

                dt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                Dim dDt As New DataTable
                dDt = dt.DefaultView.ToTable(True, "show_date")
                For Each dDr As DataRow In dDt.Rows
                    Dim STotal As Long = 0
                    Dim SKioskShow As Long = 0
                    Dim SKioskNoShow As Long = 0
                    Dim SKioskTotal As Long = 0
                    Dim SWebShow As Long = 0
                    Dim SWebNoShow As Long = 0
                    Dim SWebTotal As Long = 0
                    Dim SMobileShow As Long = 0
                    Dim SMobileNoShow As Long = 0
                    Dim SMobileTotal As Long = 0

                    dt.DefaultView.RowFilter = "show_date='" & dDr("show_date") & "' and shop_id='" & sDr("shop_id") & "'"
                    For Each dr As DataRowView In dt.DefaultView
                        Dim KioskTotal As Long = dr("kiosk_show") + dr("kiosk_noshow")
                        Dim KioskShowP As Long = 0
                        Dim KioskNoShowP As Long = 0
                        If KioskTotal > 0 Then
                            KioskShowP = (dr("kiosk_show") * 100) / KioskTotal
                            KioskNoShowP = (dr("kiosk_noshow") * 100) / KioskTotal
                        End If

                        Dim WebTotal As Long = dr("web_show") + dr("web_noshow")
                        Dim WebShowP As Long = 0
                        Dim WebNoShowP As Long = 0
                        If WebTotal > 0 Then
                            WebShowP = (dr("web_show") * 100) / WebTotal
                            WebNoShowP = (dr("web_noshow") * 100) / WebTotal
                        End If

                        Dim MobileTotal As Long = dr("mobile_show") + dr("mobile_noshow")
                        Dim MobileShowP As Long = 0
                        Dim MobileNoShowP As Long = 0
                        If MobileTotal > 0 Then
                            MobileShowP = (dr("mobile_show") * 100) / MobileTotal
                            MobileNoShowP = (dr("mobile_noshow") * 100) / MobileTotal
                        End If

                        ret.Append("    <tr>")
                        ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                        ret.Append("        <td align='left'>" & dr("show_date") & "</td>")
                        ret.Append("        <td align='left'>" & dr("show_time") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("total"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("kiosk_show"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(KioskShowP, "##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("kiosk_noshow"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(KioskNoShowP, "##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(KioskTotal, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("web_show"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(WebShowP, "##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("web_noshow"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(WebNoShowP, "##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(WebTotal, "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("mobile_show"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(MobileShowP, "##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(dr("mobile_noshow"), "#,##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(MobileNoShowP, "##0") & "</td>")
                        ret.Append("        <td align='right'>" & Format(MobileTotal, "#,##0") & "</td>")
                        ret.Append("    </tr>")

                        STotal += Convert.ToInt64(dr("total"))
                        SKioskShow += Convert.ToInt64(dr("kiosk_show"))
                        SKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                        SKioskTotal += KioskTotal
                        SWebShow += Convert.ToInt64(dr("web_show"))
                        SWebNoShow += Convert.ToInt64(dr("web_noshow"))
                        SWebTotal += WebTotal
                        SMobileShow += Convert.ToInt64(dr("mobile_show"))
                        SMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                        SMobileTotal += MobileTotal

                        TTotal += Convert.ToInt64(dr("total"))
                        TKioskShow += Convert.ToInt64(dr("kiosk_show"))
                        TKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                        TKioskTotal += KioskTotal
                        TWebShow += Convert.ToInt64(dr("web_show"))
                        TWebNoShow += Convert.ToInt64(dr("web_noshow"))
                        TWebTotal += WebTotal
                        TMobileShow += Convert.ToInt64(dr("mobile_show"))
                        TMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                        TMobileTotal += MobileTotal

                        GTotal += Convert.ToInt64(dr("total"))
                        GKioskShow += Convert.ToInt64(dr("kiosk_show"))
                        GKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                        GKioskTotal += KioskTotal
                        GWebShow += Convert.ToInt64(dr("web_show"))
                        GWebNoShow += Convert.ToInt64(dr("web_noshow"))
                        GWebTotal += WebTotal
                        GMobileShow += Convert.ToInt64(dr("mobile_show"))
                        GMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                        GMobileTotal += MobileTotal
                    Next

                    ''Sub Total By Date
                    Dim SKioskShowP As Long = 0
                    Dim SKioskNoShowP As Long = 0
                    If SKioskTotal > 0 Then
                        SKioskShowP = (SKioskShow * 100) / SKioskTotal
                        SKioskNoShowP = (SKioskNoShow * 100) / SKioskTotal
                    End If

                    Dim SWebShowP As Long = 0
                    Dim SWebNoShowP As Long = 0
                    If SWebTotal > 0 Then
                        SWebShowP = (SWebShow * 100) / SWebTotal
                        SWebNoShowP = (SWebNoShow * 100) / SWebTotal
                    End If

                    Dim SMobileShowP As Long = 0
                    Dim SMobileNoShowP As Long = 0
                    If SMobileTotal > 0 Then
                        SMobileShowP = (SMobileShow * 100) / SMobileTotal
                        SMobileNoShowP = (SMobileNoShow * 100) / SMobileTotal
                    End If

                    ret.Append("    <tr style='background: #E4E4E4; font-weight: bold;'>")
                    ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center' >" & dDr("show_date") & "</td>")
                    ret.Append("        <td align='center' >Sub Total</td>")
                    ret.Append("        <td align='right' >" & Format(STotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SKioskShow, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SKioskShowP, "##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SKioskNoShow, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SKioskNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SKioskTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SWebShow, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SWebShowP, "##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SWebNoShow, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SWebNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SWebTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SMobileShow, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SMobileShowP, "##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SMobileNoShow, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SMobileNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SMobileTotal, "#,##0") & "</td>")
                    ret.Append("    </tr>")
                Next
                dDt.Dispose()

                ' Total By Shop
                Dim TKioskShowP As Long = 0
                Dim TKioskNoShowP As Long = 0
                If TKioskTotal > 0 Then
                    TKioskShowP = (TKioskShow * 100) / TKioskTotal
                    TKioskNoShowP = (TKioskNoShow * 100) / TKioskTotal
                End If

                Dim TWebShowP As Long = 0
                Dim TWebNoShowP As Long = 0
                If TWebTotal > 0 Then
                    TWebShowP = (TWebShow * 100) / TWebTotal
                    TWebNoShowP = (TWebNoShow * 100) / TWebTotal
                End If

                Dim TMobileShowP As Long = 0
                Dim TMobileNoShowP As Long = 0
                If TMobileTotal > 0 Then
                    TMobileShowP = (TMobileShow * 100) / TMobileTotal
                    TMobileNoShowP = (TMobileNoShow * 100) / TMobileTotal
                End If
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='right' >" & Format(TTotal, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TKioskShow, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TKioskShowP, "##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TKioskNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TKioskNoShowP, "##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TKioskTotal, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TWebShow, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TWebShowP, "##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TWebNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TWebNoShowP, "##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TWebTotal, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TMobileShow, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TMobileShowP, "##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TMobileNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TMobileNoShowP, "##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(TMobileTotal, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            ''Grand Total
            Dim GKioskShowP As Long = 0
            Dim GKioskNoShowP As Long = 0
            If GKioskTotal > 0 Then
                GKioskShowP = (GKioskShow * 100) / GKioskTotal
                GKioskNoShowP = (GKioskNoShow * 100) / GKioskTotal
            End If

            Dim GWebShowP As Long = 0
            Dim GWebNoShowP As Long = 0
            If GWebTotal > 0 Then
                GWebShowP = (GWebShow * 100) / GWebTotal
                GWebNoShowP = (GWebNoShow * 100) / GWebTotal
            End If

            Dim GMobileShowP As Long = 0
            Dim GMobileNoShowP As Long = 0
            If GMobileTotal > 0 Then
                GMobileShowP = (GMobileShow * 100) / GMobileTotal
                GMobileNoShowP = (GMobileNoShow * 100) / GMobileTotal
            End If
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='right' >" & Format(GTotal, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GKioskShow, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GKioskShowP, "##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GKioskNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GKioskNoShowP, "##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GKioskTotal, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GWebShow, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GWebShowP, "##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GWebNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GWebNoShowP, "##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GWebTotal, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GMobileShow, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GMobileShowP, "##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GMobileNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GMobileNoShowP, "##0") & "</td>")
            ret.Append("        <td align='right' >" & Format(GMobileTotal, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        Else
            btnExport.Visible = False
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Date</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Total</td>")
            ret.Append("    </tr>")

            Dim GTotal As Long = 0
            Dim GKioskShow As Long = 0
            Dim GKioskNoShow As Long = 0
            Dim GKioskTotal As Long = 0
            Dim GWebShow As Long = 0
            Dim GWebNoShow As Long = 0
            Dim GWebTotal As Long = 0
            Dim GMobileShow As Long = 0
            Dim GMobileNoShow As Long = 0
            Dim GMobileTotal As Long = 0

            'Data Row By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TTotal As Long = 0
                Dim TKioskShow As Long = 0
                Dim TKioskNoShow As Long = 0
                Dim TKioskTotal As Long = 0
                Dim TWebShow As Long = 0
                Dim TWebNoShow As Long = 0
                Dim TWebTotal As Long = 0
                Dim TMobileShow As Long = 0
                Dim TMobileNoShow As Long = 0
                Dim TMobileTotal As Long = 0

                dt.DefaultView.RowFilter = " shop_id='" & sDr("shop_id") & "'"
                For Each dr As DataRowView In dt.DefaultView
                    Dim KioskTotal As Long = dr("kiosk_show") + dr("kiosk_noshow")
                    Dim KioskShowP As Long = 0
                    Dim KioskNoShowP As Long = 0
                    If KioskTotal > 0 Then
                        KioskShowP = (dr("kiosk_show") * 100) / KioskTotal
                        KioskNoShowP = (dr("kiosk_noshow") * 100) / KioskTotal
                    End If

                    Dim WebTotal As Long = dr("web_show") + dr("web_noshow")
                    Dim WebShowP As Long = 0
                    Dim WebNoShowP As Long = 0
                    If WebTotal > 0 Then
                        WebShowP = (dr("web_show") * 100) / WebTotal
                        WebNoShowP = (dr("web_noshow") * 100) / WebTotal
                    End If

                    Dim MobileTotal As Long = dr("mobile_show") + dr("mobile_noshow")
                    Dim MobileShowP As Long = 0
                    Dim MobileNoShowP As Long = 0
                    If MobileTotal > 0 Then
                        MobileShowP = (dr("mobile_show") * 100) / MobileTotal
                        MobileNoShowP = (dr("mobile_noshow") * 100) / MobileTotal
                    End If

                    ret.Append("    <tr>")
                    ret.Append("        <td align='left'>" & dr("shop_name_en") & "</td>")
                    ret.Append("        <td align='left'>" & dr("show_date") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("total"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("kiosk_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("kiosk_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("web_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("web_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("mobile_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("mobile_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileTotal, "#,##0") & "</td>")
                    ret.Append("    </tr>")

                    TTotal += Convert.ToInt64(dr("total"))
                    TKioskShow += Convert.ToInt64(dr("kiosk_show"))
                    TKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                    TKioskTotal += KioskTotal
                    TWebShow += Convert.ToInt64(dr("web_show"))
                    TWebNoShow += Convert.ToInt64(dr("web_noshow"))
                    TWebTotal += WebTotal
                    TMobileShow += Convert.ToInt64(dr("mobile_show"))
                    TMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                    TMobileTotal += MobileTotal

                    GTotal += Convert.ToInt64(dr("total"))
                    GKioskShow += Convert.ToInt64(dr("kiosk_show"))
                    GKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                    GKioskTotal += KioskTotal
                    GWebShow += Convert.ToInt64(dr("web_show"))
                    GWebNoShow += Convert.ToInt64(dr("web_noshow"))
                    GWebTotal += WebTotal
                    GMobileShow += Convert.ToInt64(dr("mobile_show"))
                    GMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                    GMobileTotal += MobileTotal
                Next

                ' Total By Shop
                Dim TKioskShowP As Long = 0
                Dim TKioskNoShowP As Long = 0
                If TKioskTotal > 0 Then
                    TKioskShowP = (TKioskShow * 100) / TKioskTotal
                    TKioskNoShowP = (TKioskNoShow * 100) / TKioskTotal
                End If

                Dim TWebShowP As Long = 0
                Dim TWebNoShowP As Long = 0
                If TWebTotal > 0 Then
                    TWebShowP = (TWebShow * 100) / TWebTotal
                    TWebNoShowP = (TWebNoShow * 100) / TWebTotal
                End If

                Dim TMobileShowP As Long = 0
                Dim TMobileNoShowP As Long = 0
                If TMobileTotal > 0 Then
                    TMobileShowP = (TMobileShow * 100) / TMobileTotal
                    TMobileNoShowP = (TMobileNoShow * 100) / TMobileTotal
                End If
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='center' >" & Format(TTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileTotal, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            ''Grand Total
            Dim GKioskShowP As Long = 0
            Dim GKioskNoShowP As Long = 0
            If GKioskTotal > 0 Then
                GKioskShowP = (GKioskShow * 100) / GKioskTotal
                GKioskNoShowP = (GKioskNoShow * 100) / GKioskTotal
            End If

            Dim GWebShowP As Long = 0
            Dim GWebNoShowP As Long = 0
            If GWebTotal > 0 Then
                GWebShowP = (GWebShow * 100) / GWebTotal
                GWebNoShowP = (GWebNoShow * 100) / GWebTotal
            End If

            Dim GMobileShowP As Long = 0
            Dim GMobileNoShowP As Long = 0
            If GMobileTotal > 0 Then
                GMobileShowP = (GMobileShow * 100) / GMobileTotal
                GMobileNoShowP = (GMobileNoShow * 100) / GMobileTotal
            End If
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >" & Format(GTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileTotal, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        Else
            btnExport.Visible = False
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByWeek(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Week No.</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Total</td>")
            ret.Append("    </tr>")

            Dim GTotal As Long = 0
            Dim GKioskShow As Long = 0
            Dim GKioskNoShow As Long = 0
            Dim GKioskTotal As Long = 0
            Dim GWebShow As Long = 0
            Dim GWebNoShow As Long = 0
            Dim GWebTotal As Long = 0
            Dim GMobileShow As Long = 0
            Dim GMobileNoShow As Long = 0
            Dim GMobileTotal As Long = 0

            'Data Row By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TTotal As Long = 0
                Dim TKioskShow As Long = 0
                Dim TKioskNoShow As Long = 0
                Dim TKioskTotal As Long = 0
                Dim TWebShow As Long = 0
                Dim TWebNoShow As Long = 0
                Dim TWebTotal As Long = 0
                Dim TMobileShow As Long = 0
                Dim TMobileNoShow As Long = 0
                Dim TMobileTotal As Long = 0

                dt.DefaultView.RowFilter = " shop_id='" & sDr("shop_id") & "'"
                For Each dr As DataRowView In dt.DefaultView
                    Dim KioskTotal As Long = dr("kiosk_show") + dr("kiosk_noshow")
                    Dim KioskShowP As Long = 0
                    Dim KioskNoShowP As Long = 0
                    If KioskTotal > 0 Then
                        KioskShowP = (dr("kiosk_show") * 100) / KioskTotal
                        KioskNoShowP = (dr("kiosk_noshow") * 100) / KioskTotal
                    End If

                    Dim WebTotal As Long = dr("web_show") + dr("web_noshow")
                    Dim WebShowP As Long = 0
                    Dim WebNoShowP As Long = 0
                    If WebTotal > 0 Then
                        WebShowP = (dr("web_show") * 100) / WebTotal
                        WebNoShowP = (dr("web_noshow") * 100) / WebTotal
                    End If

                    Dim MobileTotal As Long = dr("mobile_show") + dr("mobile_noshow")
                    Dim MobileShowP As Long = 0
                    Dim MobileNoShowP As Long = 0
                    If MobileTotal > 0 Then
                        MobileShowP = (dr("mobile_show") * 100) / MobileTotal
                        MobileNoShowP = (dr("mobile_noshow") * 100) / MobileTotal
                    End If

                    ret.Append("    <tr>")
                    ret.Append("        <td align='center'>" & dr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center'>" & dr("week_of_year") & "</td>")
                    ret.Append("        <td align='center'>" & dr("show_year") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("total"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("kiosk_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("kiosk_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("web_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("web_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("mobile_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("mobile_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileTotal, "#,##0") & "</td>")
                    ret.Append("    </tr>")

                    TTotal += Convert.ToInt64(dr("total"))
                    TKioskShow += Convert.ToInt64(dr("kiosk_show"))
                    TKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                    TKioskTotal += KioskTotal
                    TWebShow += Convert.ToInt64(dr("web_show"))
                    TWebNoShow += Convert.ToInt64(dr("web_noshow"))
                    TWebTotal += WebTotal
                    TMobileShow += Convert.ToInt64(dr("mobile_show"))
                    TMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                    TMobileTotal += MobileTotal

                    GTotal += Convert.ToInt64(dr("total"))
                    GKioskShow += Convert.ToInt64(dr("kiosk_show"))
                    GKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                    GKioskTotal += KioskTotal
                    GWebShow += Convert.ToInt64(dr("web_show"))
                    GWebNoShow += Convert.ToInt64(dr("web_noshow"))
                    GWebTotal += WebTotal
                    GMobileShow += Convert.ToInt64(dr("mobile_show"))
                    GMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                    GMobileTotal += MobileTotal
                Next

                ' Total By Shop
                Dim TKioskShowP As Long = 0
                Dim TKioskNoShowP As Long = 0
                If TKioskTotal > 0 Then
                    TKioskShowP = (TKioskShow * 100) / TKioskTotal
                    TKioskNoShowP = (TKioskNoShow * 100) / TKioskTotal
                End If

                Dim TWebShowP As Long = 0
                Dim TWebNoShowP As Long = 0
                If TWebTotal > 0 Then
                    TWebShowP = (TWebShow * 100) / TWebTotal
                    TWebNoShowP = (TWebNoShow * 100) / TWebTotal
                End If

                Dim TMobileShowP As Long = 0
                Dim TMobileNoShowP As Long = 0
                If TMobileTotal > 0 Then
                    TMobileShowP = (TMobileShow * 100) / TMobileTotal
                    TMobileNoShowP = (TMobileNoShow * 100) / TMobileTotal
                End If
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='center' >" & Format(TTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileTotal, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            ''Grand Total
            Dim GKioskShowP As Long = 0
            Dim GKioskNoShowP As Long = 0
            If GKioskTotal > 0 Then
                GKioskShowP = (GKioskShow * 100) / GKioskTotal
                GKioskNoShowP = (GKioskNoShow * 100) / GKioskTotal
            End If

            Dim GWebShowP As Long = 0
            Dim GWebNoShowP As Long = 0
            If GWebTotal > 0 Then
                GWebShowP = (GWebShow * 100) / GWebTotal
                GWebNoShowP = (GWebNoShow * 100) / GWebTotal
            End If

            Dim GMobileShowP As Long = 0
            Dim GMobileNoShowP As Long = 0
            If GMobileTotal > 0 Then
                GMobileShowP = (GMobileShow * 100) / GMobileTotal
                GMobileNoShowP = (GMobileNoShow * 100) / GMobileTotal
            End If
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='center' >" & Format(GTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileTotal, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        Else
            btnExport.Visible = False
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

    Private Sub RenderReportByMonth(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table border='1' cellpadding='0' cellspacing='0' class='mGrid' >")
            'Header Row
            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' style='color: #ffffff;' >Shop Name</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Month</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Year</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Kiosk Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Web Site Total</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) %No Show</td>")
            ret.Append("        <td align='center' style='color: #ffffff;' >Appointment Channels Mobile(Smart Phone) Total</td>")
            ret.Append("    </tr>")

            Dim GTotal As Long = 0
            Dim GKioskShow As Long = 0
            Dim GKioskNoShow As Long = 0
            Dim GKioskTotal As Long = 0
            Dim GWebShow As Long = 0
            Dim GWebNoShow As Long = 0
            Dim GWebTotal As Long = 0
            Dim GMobileShow As Long = 0
            Dim GMobileNoShow As Long = 0
            Dim GMobileTotal As Long = 0

            'Data Row By Shop
            Dim sDt As New DataTable
            sDt = dt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_name_th")
            For Each sDr As DataRow In sDt.Rows
                Dim TTotal As Long = 0
                Dim TKioskShow As Long = 0
                Dim TKioskNoShow As Long = 0
                Dim TKioskTotal As Long = 0
                Dim TWebShow As Long = 0
                Dim TWebNoShow As Long = 0
                Dim TWebTotal As Long = 0
                Dim TMobileShow As Long = 0
                Dim TMobileNoShow As Long = 0
                Dim TMobileTotal As Long = 0

                dt.DefaultView.RowFilter = " shop_id='" & sDr("shop_id") & "'"
                For Each dr As DataRowView In dt.DefaultView
                    Dim KioskTotal As Long = dr("kiosk_show") + dr("kiosk_noshow")
                    Dim KioskShowP As Long = 0
                    Dim KioskNoShowP As Long = 0
                    If KioskTotal > 0 Then
                        KioskShowP = (dr("kiosk_show") * 100) / KioskTotal
                        KioskNoShowP = (dr("kiosk_noshow") * 100) / KioskTotal
                    End If

                    Dim WebTotal As Long = dr("web_show") + dr("web_noshow")
                    Dim WebShowP As Long = 0
                    Dim WebNoShowP As Long = 0
                    If WebTotal > 0 Then
                        WebShowP = (dr("web_show") * 100) / WebTotal
                        WebNoShowP = (dr("web_noshow") * 100) / WebTotal
                    End If

                    Dim MobileTotal As Long = dr("mobile_show") + dr("mobile_noshow")
                    Dim MobileShowP As Long = 0
                    Dim MobileNoShowP As Long = 0
                    If MobileTotal > 0 Then
                        MobileShowP = (dr("mobile_show") * 100) / MobileTotal
                        MobileNoShowP = (dr("mobile_noshow") * 100) / MobileTotal
                    End If

                    ret.Append("    <tr>")
                    ret.Append("        <td align='center'>" & dr("shop_name_en") & "</td>")
                    ret.Append("        <td align='center'>" & dr("show_month") & "</td>")
                    ret.Append("        <td align='center'>" & dr("show_year") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("total"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("kiosk_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("kiosk_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(KioskTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("web_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("web_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(WebTotal, "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("mobile_show"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(dr("mobile_noshow"), "#,##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileNoShowP, "##0") & "</td>")
                    ret.Append("        <td align='right'>" & Format(MobileTotal, "#,##0") & "</td>")
                    ret.Append("    </tr>")

                    TTotal += Convert.ToInt64(dr("total"))
                    TKioskShow += Convert.ToInt64(dr("kiosk_show"))
                    TKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                    TKioskTotal += KioskTotal
                    TWebShow += Convert.ToInt64(dr("web_show"))
                    TWebNoShow += Convert.ToInt64(dr("web_noshow"))
                    TWebTotal += WebTotal
                    TMobileShow += Convert.ToInt64(dr("mobile_show"))
                    TMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                    TMobileTotal += MobileTotal

                    GTotal += Convert.ToInt64(dr("total"))
                    GKioskShow += Convert.ToInt64(dr("kiosk_show"))
                    GKioskNoShow += Convert.ToInt64(dr("kiosk_noshow"))
                    GKioskTotal += KioskTotal
                    GWebShow += Convert.ToInt64(dr("web_show"))
                    GWebNoShow += Convert.ToInt64(dr("web_noshow"))
                    GWebTotal += WebTotal
                    GMobileShow += Convert.ToInt64(dr("mobile_show"))
                    GMobileNoShow += Convert.ToInt64(dr("mobile_noshow"))
                    GMobileTotal += MobileTotal
                Next

                ' Total By Shop
                Dim TKioskShowP As Long = 0
                Dim TKioskNoShowP As Long = 0
                If TKioskTotal > 0 Then
                    TKioskShowP = (TKioskShow * 100) / TKioskTotal
                    TKioskNoShowP = (TKioskNoShow * 100) / TKioskTotal
                End If

                Dim TWebShowP As Long = 0
                Dim TWebNoShowP As Long = 0
                If TWebTotal > 0 Then
                    TWebShowP = (TWebShow * 100) / TWebTotal
                    TWebNoShowP = (TWebNoShow * 100) / TWebTotal
                End If

                Dim TMobileShowP As Long = 0
                Dim TMobileNoShowP As Long = 0
                If TMobileTotal > 0 Then
                    TMobileShowP = (TMobileShow * 100) / TMobileTotal
                    TMobileNoShowP = (TMobileNoShow * 100) / TMobileTotal
                End If
                ret.Append("    <tr style='background: pink repeat-x top;font-weight: bold;' >")
                ret.Append("        <td align='center' >" & sDr("shop_name_en") & "</td>")
                ret.Append("        <td align='center' >&nbsp;</td>")
                ret.Append("        <td align='center' >Total</td>")
                ret.Append("        <td align='center' >" & Format(TTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TKioskTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TWebTotal, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileNoShow, "#,##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileNoShowP, "##0") & "</td>")
                ret.Append("        <td align='center' >" & Format(TMobileTotal, "#,##0") & "</td>")
                ret.Append("    </tr>")
            Next
            sDt.Dispose()

            ''Grand Total
            Dim GKioskShowP As Long = 0
            Dim GKioskNoShowP As Long = 0
            If GKioskTotal > 0 Then
                GKioskShowP = (GKioskShow * 100) / GKioskTotal
                GKioskNoShowP = (GKioskNoShow * 100) / GKioskTotal
            End If

            Dim GWebShowP As Long = 0
            Dim GWebNoShowP As Long = 0
            If GWebTotal > 0 Then
                GWebShowP = (GWebShow * 100) / GWebTotal
                GWebNoShowP = (GWebNoShow * 100) / GWebTotal
            End If

            Dim GMobileShowP As Long = 0
            Dim GMobileNoShowP As Long = 0
            If GMobileTotal > 0 Then
                GMobileShowP = (GMobileShow * 100) / GMobileTotal
                GMobileNoShowP = (GMobileNoShow * 100) / GMobileTotal
            End If
            ret.Append("    <tr style='background: Orange repeat-x top;font-weight: bold;' >")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >&nbsp;</td>")
            ret.Append("        <td align='center' >Grand Total</td>")
            ret.Append("        <td align='center' >" & Format(GTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GKioskTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GWebTotal, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileNoShow, "#,##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileNoShowP, "##0") & "</td>")
            ret.Append("        <td align='center' >" & Format(GMobileTotal, "#,##0") & "</td>")
            ret.Append("    </tr>")
            ret.Append("</table>")
            lblReportDesc.Text = ret.ToString
            ret = Nothing
        Else
            btnExport.Visible = False
        End If

        If lblReportDesc.Text.Trim <> "" Then
            lblerror.Visible = False
        End If
    End Sub

End Class
