Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Partial Class ReportApp_repMonthlyReport
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "Monthly_Report_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls")
    End Sub

    Private Sub SetReportName()
        Dim NetworkType As String = Request("NetworkType") & ""
        Dim ReportType As String = Request("ReportType") & ""
        If NetworkType = "" Or NetworkType = "Null" Then NetworkType = "All"
        If ReportType = "2" Then ReportType = " All " Else ReportType = " by "
        Dim ReportName As String = "Monthly Performance Report of " & NetworkType & " Customer " & ReportType & " Service Type of Shop BKK and UPC"
        lblReportName.Text = ReportName
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repMonthlyReport.ExportData", Config.GetLoginHistoryID)
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
            SetReportName()
            ShowReport(Request("ReportName"))

        End If
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        Dim MonthFrom As String = Request("MonthFrom") & ""
        Dim MonthTo As String = Request("MonthTo") & ""
        Dim ReportType As String = Request("ReportType") & ""
        Dim NetworkType As String = Request("NetworkType") & ""
        Dim Location As String = Request("Location") & ""
        Dim ServiceType As String = Request("ServiceType") & ""
        Dim Shop As String = Request("Shop") & ""

        Try
            Dim tempData As DataTable = New DataTable()
            Dim con As SqlConnection = CenLinqDB.Common.Utilities.SqlDB.GetConnection
            Dim cmd As SqlCommand = New SqlCommand("SP_RepNetworkTypeAndServiceTypeMonthly", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 1200
            cmd.Parameters.AddWithValue("@MonthFrom", MonthFrom)
            cmd.Parameters.AddWithValue("@MonthTo", MonthTo)
            cmd.Parameters.AddWithValue("@ReportType", ReportType)
            cmd.Parameters.AddWithValue("@NetworkType", NetworkType)
            cmd.Parameters.AddWithValue("@Location", Location)
            cmd.Parameters.AddWithValue("@ServiceType", ServiceType)
            cmd.Parameters.AddWithValue("@Shop", Shop)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(tempData)
            con.Close()

            Dim txtData As String = ""
            If ReportName = "ByMonth" Then
                If tempData.Rows.Count > 0 Then
                    RenderReportByDate(tempData)
                End If
                tempData.Dispose()
            End If
        Catch ex As Exception
            lblerror.Text = ex.Message
            Engine.Common.FunctionEng.SaveErrorLog("ReportApp_repMonthlyReport.ShowReport", "Exception:" & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub



    Private Sub RenderReportByDate(ByVal dt As DataTable)
        'Dim ret As String = ""
        If dt.Rows.Count > 0 Then


            Dim ret As New StringBuilder
            'Start
            ret.Append("<table width=""2500px"" border='1' cellpadding='0' cellspacing='0' class='mGrid' >")


            '### Header Colums Start ######
            Dim strHeader As String() = { _
                                       "Network Type", _
                                       "Shop Name", _
                                       "Region", _
                                       "Month", _
                                       "Year", _
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
                                       "%Abandon", _
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

            '### Detail Colums Start ######


            Dim strKeepNetworkType As String = ""
            Dim strKeepLocation As String = ""
            Dim strKeepShop As String = ""
            Dim strKeepMonth_Name As String = ""
            Dim strKeepService_Year As String = ""
            Dim strServiceType As String = ""
            Dim strKeepNetworkType_Old As String = ""
            Dim strKeepShop_Old As String = ""
            Dim strKeepLocation_Old As String = ""
            Dim strKeepMonth_Name_Old As String = ""
            Dim strKeepService_Year_Old As String = ""
            Dim strServiceType_Old As String = ""

            Dim SumRegis As Integer
            Dim SumServe As Integer
            Dim SumMissCall As Integer
            Dim SumCancel As Integer
            Dim SumNotCall As Integer
            Dim SumNotConfirm As Integer
            Dim SumNotEnd As Integer
            Dim SumWT_With_KPI As Integer
            Dim SumHT_With_KPI As Integer
            Dim SumWT_With_KPI_Percen As Integer
            Dim SumWT_Over_KPI_Percen As Integer
            Dim SumHT_With_KPI_Percen As Integer
            Dim SumHT_Over_KPI_Percen As Integer
            Dim SumMisscall_percen As Integer
            Dim SumAVG_WT As Integer
            Dim SumAVG_HT As Integer
            Dim SumMAX_WT As Integer
            Dim SumMAX_HT As Integer

            Dim SumWT As Integer
            Dim SumHT As Integer
            Dim strSumWT As String
            Dim strSumHT As String


            For intData As Integer = 0 To dt.Rows.Count - 1
                '### Sum Start ######
                If strKeepNetworkType = "" Then
                    strKeepNetworkType = dt(intData)("network_type")
                    strKeepShop = dt(intData)("shop")
                    strKeepLocation = IIf(dt(intData)("location_code") & "" = "RO", "UPC", dt(intData)("location_code") & "")
                    strKeepMonth_Name = dt(intData)("Month_Name")
                    strKeepService_Year = dt(intData)("Service_Year")
                    strServiceType = dt(intData)("item_name")
                    ' ElseIf strKeepShop <> dt(intData)("Shop") Or strKeepMonth_Name <> dt(intData)("Month_Name") Or strKeepService_Year <> dt(intData)("Service_Year") Or strServiceType <> dt(intData)("item_name") Then
                ElseIf strKeepLocation <> IIf(dt(intData)("location_code") & "" = "RO", "UPC", dt(intData)("location_code") & "") Or strKeepMonth_Name <> dt(intData)("Month_Name") _
                Or strKeepService_Year <> dt(intData)("Service_Year") Or strServiceType <> dt(intData)("item_name") Then
                    'เก็บค่าเก่าไว้แสดง
                    strKeepNetworkType_Old = strKeepNetworkType
                    strKeepShop_Old = strKeepShop
                    strKeepLocation_Old = strKeepLocation
                    strKeepMonth_Name_Old = strKeepMonth_Name
                    strKeepService_Year_Old = strKeepService_Year
                    strServiceType_Old = strServiceType

                    'เก็บค่าใหม่ไว้เปรียบเทียบ
                    strKeepNetworkType = dt(intData)("network_type")
                    strKeepShop = dt(intData)("shop")
                    strKeepLocation = IIf(dt(intData)("location_code") & "" = "RO", "UPC", dt(intData)("location_code") & "")
                    strKeepMonth_Name = dt(intData)("Month_Name")
                    strKeepService_Year = dt(intData)("Service_Year")
                    strServiceType = dt(intData)("item_name")


                    Dim TotalAbandon As Integer = SumMissCall + SumCancel
                    Dim PerAbandon As Integer = (TotalAbandon / SumRegis) * 100
                    'Gen Sum
                    ret.Append("    <tr style='background: #E4E4E4;'>")
                    ret.Append("        <td align='left' >" & strKeepNetworkType_Old & "</td>")
                    ret.Append("        <td align='left' > Total (" & IIf(strServiceType_Old = "All", "All Service", strServiceType_Old) & ")</td>")
                    ret.Append("        <td align='center' >" & strKeepLocation_Old & "</td>")
                    ret.Append("        <td align='left' >" & strKeepMonth_Name_Old & "</td>")
                    ret.Append("        <td align='right' >" & strKeepService_Year_Old & "</td>")
                    ret.Append("        <td align='left' >" & strServiceType_Old & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumRegis, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumServe, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumMissCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumCancel, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(TotalAbandon, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumNotCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumNotConfirm, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumNotEnd, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumWT_With_KPI, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumHT_With_KPI, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(IIf(SumServe <> 0, (SumWT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(100 - IIf(SumServe <> 0, (SumWT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(IIf(SumServe <> 0, (SumHT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(100 - IIf(SumServe <> 0, (SumHT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(PerAbandon, "#,##0") & "%</td>")
                    'ret.Append("        <td align='right' >" & Format(IIf(SumRegis <> 0, (SumMissCall / SumRegis) * 100, 0), "#,##0") & "%</td>")
                    If (SumServe) <> 0 Then
                        ret.Append("        <td align='center' >" & Engine.Reports.ReportsENG.GetFormatTimeFromSec(SumWT / (SumServe)) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If SumServe <> 0 Then
                        ret.Append("        <td align='center' >" & Engine.Reports.ReportsENG.GetFormatTimeFromSec(SumHT / SumServe) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    ret.Append("        <td align='center' >" & IIf(strSumWT <> "", strSumWT, "00:00:00") & "</td>")
                    ret.Append("        <td align='center' >" & IIf(strSumHT <> "", strSumHT, "00:00:00") & "</td>")
                    ret.Append("    </tr>")

                    'Clear ค่า
                    SumRegis = 0
                    SumServe = 0
                    SumMissCall = 0
                    SumCancel = 0
                    SumNotCall = 0
                    SumNotConfirm = 0
                    SumNotEnd = 0
                    SumWT_With_KPI = 0
                    SumHT_With_KPI = 0
                    SumWT_With_KPI_Percen = 0
                    SumWT_Over_KPI_Percen = 0
                    SumHT_With_KPI_Percen = 0
                    SumHT_Over_KPI_Percen = 0
                    SumMisscall_percen = 0
                    SumAVG_WT = 0
                    SumAVG_HT = 0
                    SumMAX_WT = 0
                    SumMAX_HT = 0
                    SumWT = 0
                    SumHT = 0
                    strSumWT = ""
                    strSumHT = ""

                End If

                '### Sum End ######
                'เก็บค่ามากสุด  MAX_WT MAX_HT  ไว้แสดงตอน SUM
                If SumMAX_WT < Val(dt(intData)("MAX_WT").ToString.Replace(":", "")) Then
                    SumMAX_WT = Val(dt(intData)("MAX_WT").ToString.Replace(":", ""))
                    strSumWT = dt(intData)("MAX_WT").ToString
                End If

                If SumMAX_HT < Val(dt(intData)("MAX_HT").ToString.Replace(":", "")) Then
                    SumMAX_HT = Val(dt(intData)("MAX_HT").ToString.Replace(":", ""))
                    strSumHT = dt(intData)("MAX_HT").ToString
                End If

                Dim _totalabandon As Integer = CInt(dt(intData)("MissCall")) + CInt(dt(intData)("Cancel"))
                Dim _percent_abandon As Integer = (_totalabandon / CInt(dt(intData)("Regis"))) * 100
                'Gen Detail
                ret.Append("    <tr>")
                ret.Append("        <td align='left' >" & dt(intData)("network_type") & "</td>")
                ret.Append("        <td align='left' >" & dt(intData)("shop") & "</td>")
                ret.Append("        <td align='center' >" & IIf(dt(intData)("location_code") & "" = "RO", "UPC", dt(intData)("location_code") & "") & "</td>")
                ret.Append("        <td align='left' >" & dt(intData)("Month_Name") & "</td>")
                ret.Append("        <td align='right' >" & dt(intData)("service_year") & "</td>")
                ret.Append("        <td align='left' >" & dt(intData)("item_name") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("Regis"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("Serve"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("MissCall"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("Cancel"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & _totalabandon & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("NotCall"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("NotConfirm"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("NotEnd"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("WT_With_KPI"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("HT_With_KPI"), "#,##0") & "</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("WT_With_KPI_Percen"), "#,##0") & "%</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("WT_Over_KPI_Percen"), "#,##0") & "%</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("HT_With_KPI_Percen"), "#,##0") & "%</td>")
                ret.Append("        <td align='right' >" & Format(dt(intData)("HT_Over_KPI_Percen"), "#,##0") & "%</td>")
                ret.Append("        <td align='right' >" & Format(_percent_abandon, "#,##0") & "%</td>")
                'ret.Append("        <td align='right' >" & Format(dt(intData)("Misscall_percen"), "#,##0") & "%</td>")

                'Dim cal_wt As Integer = Val(dt(intData)("cal_wt") & "")
                Dim AVG_WT As String = dt(intData)("AVG_WT").ToString
                If AVG_WT <> "00:00:00" Then
                    ret.Append("        <td align='center' >" & AVG_WT & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If

                'Dim cal_ht As Integer = Val(dt(intData)("cal_ht") & "")
                Dim AVG_HT As String = dt(intData)("AVG_HT").ToString 'Engine.Reports.ReportsENG.GetFormatTimeFromSec(cal_ht)
                If AVG_HT <> "00:00:00" Then
                    ret.Append("        <td align='center' >" & AVG_HT & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If Val(dt(intData)("MAX_WT").ToString.Replace(":", "")) <> 0 Then
                    ret.Append("        <td align='center' >" & dt(intData)("MAX_WT") & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                If Val(dt(intData)("MAX_HT").ToString.Replace(":", "")) <> 0 Then
                    ret.Append("        <td align='center' >" & dt(intData)("MAX_HT") & "</td>")
                Else
                    ret.Append("        <td align='center' >00:00:00</td>")
                End If
                'ret.Append("        <td align='center' >" & IIf(Val(dt(intData)("AVG_WT") & "") = 0, "00:00:00", dt(intData)("AVG_WT")) & "</td>")
                'ret.Append("        <td align='center' >" & IIf(Val(dt(intData)("AVG_HT") & "") = 0, "00:00:00", dt(intData)("AVG_HT")) & "</td>")
                'ret.Append("        <td align='center' >" & IIf(Val(dt(intData)("MAX_WT") & "") = 0, "00:00:00", dt(intData)("MAX_WT")) & "</td>")
                'ret.Append("        <td align='center' >" & IIf(Val(dt(intData)("MAX_HT") & "") = 0, "00:00:00", dt(intData)("MAX_HT")) & "</td>")

                'เก็บค่าไว้ใช้ตอน Sum
                SumRegis += Val(dt(intData)("Regis") & "")
                SumServe += Val(dt(intData)("Serve") & "")
                SumMissCall += Val(dt(intData)("MissCall") & "")
                SumCancel += Val(dt(intData)("Cancel") & "")
                SumNotCall += Val(dt(intData)("NotCall") & "")
                SumNotConfirm += Val(dt(intData)("NotConfirm") & "")
                SumNotEnd += Val(dt(intData)("NotEnd") & "")
                SumWT_With_KPI += Val(dt(intData)("WT_With_KPI") & "")
                SumHT_With_KPI += Val(dt(intData)("HT_With_KPI") & "")
                SumWT_With_KPI_Percen += Val(dt(intData)("WT_With_KPI_Percen") & "")
                SumWT_Over_KPI_Percen += Val(dt(intData)("WT_Over_KPI_Percen") & "")
                SumHT_With_KPI_Percen += Val(dt(intData)("HT_With_KPI_Percen") & "")
                SumHT_Over_KPI_Percen += Val(dt(intData)("HT_Over_KPI_Percen") & "")
                SumMisscall_percen += Val(dt(intData)("Misscall_percen") & "")


                SumWT += Val(dt(intData)("SUM_WT") & "")
                SumHT += Val(dt(intData)("SUM_HT") & "")

                ret.Append("    </tr>")

                'ret.Append("    <tr>")
                'For intHeaderColums As Integer = 0 To strColumsName.Count - 1
                '    If strColumsName(intHeaderColums) = "Shop" Then
                '        ret.Append("        <td align='left'>" & dt(intData)(strColumsName(intHeaderColums)) & "-" & dt(intData)("location_code") & "</td>")
                '    ElseIf strColumsName(intHeaderColums).IndexOf("Percen") <> -1 Then
                '        ret.Append("        <td align='left'>" & dt(intData)(strColumsName(intHeaderColums)) & "%" & "</td>")
                '    Else
                '        ret.Append("        <td align='left'>" & dt(intData)(strColumsName(intHeaderColums)) & "</td>")
                '    End If
                'Next
                'ret.Append("    </tr>")


                '### Sum Row สุดท้าย ######
                If intData = dt.Rows.Count - 1 Then 'เก็บค่า  ไว้แสดงตอน SUM เมื่อถึง Record สุดท้าย
                    strKeepNetworkType_Old = dt(intData)("network_type")
                    strKeepShop_Old = dt(intData)("shop")
                    strKeepLocation_Old = IIf(dt(intData)("location_code") & "" = "RO", "UPC", dt(intData)("location_code") & "")
                    strKeepMonth_Name_Old = dt(intData)("Month_Name")
                    strKeepService_Year_Old = dt(intData)("Service_Year")
                    strServiceType_Old = dt(intData)("item_name")

                    Dim sumTotalAbandon As Integer = SumMissCall + SumCancel
                    Dim sumPerAbandon As Integer = (sumTotalAbandon / SumRegis) * 100

                    'Gen Sum
                    ret.Append("    <tr style='background: #E4E4E4;'>")
                    ret.Append("        <td align='left' >" & strKeepNetworkType_Old & "</td>")
                    ret.Append("        <td align='left' > Total (" & IIf(strServiceType_Old = "All", "All Service", strServiceType_Old) & ")</td>")
                    ret.Append("        <td align='center' >" & strKeepLocation_Old & "</td>")
                    ret.Append("        <td align='left' >" & strKeepMonth_Name_Old & "</td>")
                    ret.Append("        <td align='right' >" & strKeepService_Year_Old & "</td>")
                    ret.Append("        <td align='left' >" & strServiceType_Old & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumRegis, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumServe, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumMissCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumCancel, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(sumTotalAbandon, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumNotCall, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumNotConfirm, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumNotEnd, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumWT_With_KPI, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(SumHT_With_KPI, "#,##0") & "</td>")
                    ret.Append("        <td align='right' >" & Format(IIf(SumServe <> 0, (SumWT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(100 - IIf(SumServe <> 0, (SumWT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(IIf(SumServe <> 0, (SumHT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(100 - IIf(SumServe <> 0, (SumHT_With_KPI / SumServe) * 100, 0), "#,##0") & "%</td>")
                    ret.Append("        <td align='right' >" & Format(sumPerAbandon, "#,##0") & "%</td>")
                    'ret.Append("        <td align='right' >" & Format(IIf(SumRegis <> 0, (SumMissCall / SumRegis) * 100, 0), "#,##0") & "%</td>")
                    If (SumServe) <> 0 Then
                        ret.Append("        <td align='center' >" & Engine.Reports.ReportsENG.GetFormatTimeFromSec(SumWT / (SumServe)) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    If SumServe <> 0 Then
                        ret.Append("        <td align='center' >" & Engine.Reports.ReportsENG.GetFormatTimeFromSec(SumHT / SumServe) & "</td>")
                    Else
                        ret.Append("        <td align='center' >00:00:00</td>")
                    End If
                    ret.Append("        <td align='center' >" & IIf(strSumWT <> "", strSumWT, "00:00:00") & "</td>")
                    ret.Append("        <td align='center' >" & IIf(strSumHT <> "", strSumHT, "00:00:00") & "</td>")
                    ret.Append("    </tr>")
                End If

            Next
            '### Detail Colums End ######





            'End
            ret.Append("</table>")



            lblReportDesc.Text = ret.ToString
            ret = Nothing
        End If

        If lblReportDesc.Text.Trim <> "" Then
            'lblReportDesc.Text = ret
            lblerror.Visible = False
        End If
    End Sub

End Class
