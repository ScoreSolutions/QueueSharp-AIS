Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Partial Class ReportApp_repMonthlyReportManagement
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportData("application/vnd.xls", "Monthly_Report_Management_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls")
    End Sub

    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)
        Config.SaveTransLog("Export Data to " & _contentType, "ReportApp_repDailyReport.ExportData", Config.GetLoginHistoryID)
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

    Private Sub SetReportName()
        Dim NetworkType As String = Request("NetworkType") & ""
        Dim LocationCode As String = Request("LocationCode") & ""
        Dim Shop As String = Request("Shop") & ""
        If NetworkType = "" Or NetworkType = "Null" Then NetworkType = "(2G,3G,Other,All)"
        If LocationCode = "" Then LocationCode = "BKK & UPC"
        'If Shop <> "" Then LocationCode = Shop
        Dim ReportName As String = "Management report for " & NetworkType & " Customer of Shop " & LocationCode & ""
        lblReportName.Text = ReportName
    End Sub

    Private Sub ShowReport(ByVal ReportName As String)
        Dim DateFrom As String = Request("DateFrom") & ""
        Dim DateTo As String = Request("DateTo") & ""
        Dim NetworkType As String = Request("NetworkType") & ""
        Dim LocationCode As String = IIf(Request("LocationCode") & "" = "UPC", "RO", Request("LocationCode") & "")
        Dim ServiceType As String = Request("ServiceType") & ""
        Dim Shop As String = Request("Shop") & ""

        Try
            Dim tempData As DataTable = New DataTable()
            Dim con As SqlConnection = CenLinqDB.Common.Utilities.SqlDB.GetConnection
            Dim cmd As SqlCommand = New SqlCommand("SP_RepNetworkTypeAndServiceTypeMonthlyForVP_SVM", con)
            cmd.CommandTimeout = 1200
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom)
            cmd.Parameters.AddWithValue("@DateTo", DateTo)
            cmd.Parameters.AddWithValue("@NetworkType", NetworkType)
            cmd.Parameters.AddWithValue("@LocationCode", LocationCode)
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
            Engine.Common.FunctionEng.SaveErrorLog("ReportApp_repMonthlyReportManagement.ShowReport", "Exception : " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function getNumberDay() As String
        Dim NetWorkTypeColmnName As String = Request("NetworkType") & "-Shop " & Request("LocationCode")
        Dim TotalDay As Integer = 0
        Dim StartDate As DateTime = Nothing
        Dim EndDate As DateTime = Nothing
        Dim strStartDate As String = ""
        If Request("DateFrom") & "" <> "" AndAlso Request("DateTo") & "" <> "" Then
            TotalDay = (CDate(Request("DateTo")) - CDate(Request("DateFrom"))).Days
            StartDate = CDate(Request("DateFrom"))
            EndDate = CDate(Request("DateTo"))
            strStartDate = Replace(Request("DateFrom"), "-", "")
        End If

        Dim strHeader As New ArrayList
        strHeader.Add(NetWorkTypeColmnName)
        For i As Integer = 0 To TotalDay
            Dim day As String = DateAdd(DateInterval.Day, i, StartDate).Day
            Dim month As String = DateAdd(DateInterval.Day, i, StartDate).ToString("MMMM", New Globalization.CultureInfo("en-US"))
            Dim strdate As String = day & "-" & month
            strHeader.Add(strdate)
        Next
        strHeader.Add("Total")


        Return "width=" & (strHeader.Count * 100) + 100 & "px"
    End Function

    Private Sub RenderReportByDate(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim ret As New StringBuilder
            ret.Append("<table " & getNumberDay() & " border='1' cellpadding='0' cellspacing='0' class='mGrid' >")

            '### Header Colums Start ######
            Dim NetworkType As String = Request("NetworkType") & ""
            Dim ShowShop As String = IIf(Request("LocationCode").ToString = "", "All", Request("LocationCode"))
            If Request("Shop") & "" <> "" Then ShowShop = Request("Shop") & ""
            If NetworkType = "" Or NetworkType = "Null" Then NetworkType = "(2G,3G,Other,All)"
            Dim NetWorkTypeColmnName As String = NetworkType & "-Shop " & ShowShop
            Dim TotalDay As Integer = 0
            Dim StartDate As DateTime = Nothing
            Dim EndDate As DateTime = Nothing
            Dim strStartDate As String = ""
            If Request("DateFrom") & "" <> "" AndAlso Request("DateTo") & "" <> "" Then
                TotalDay = (CDate(Request("DateTo")) - CDate(Request("DateFrom"))).Days
                StartDate = CDate(Request("DateFrom"))
                EndDate = CDate(Request("DateTo"))
                strStartDate = Replace(Request("DateFrom"), "-", "")
            End If

            Dim strHeader As New ArrayList
            strHeader.Add(NetWorkTypeColmnName)
            For i As Integer = 0 To TotalDay
                Dim day As String = DateAdd(DateInterval.Day, i, StartDate).Day
                Dim month As String = DateAdd(DateInterval.Day, i, StartDate).ToString("MMMM", New Globalization.CultureInfo("en-US"))
                Dim strdate As String = day & "-" & month
                strHeader.Add(strdate)
            Next
            strHeader.Add("Total")

            ret.Append("    <tr style='background: yellowgreen repeat-x top;font-weight: bold;' >")
            For intHeader As Integer = 0 To strHeader.Count - 1
                ret.Append("        <td  align='center' style='color: #000000;' >" & strHeader(intHeader) & "</td>")
            Next
            ret.Append("    </tr>")
            '### Header Colums End ######


            '### Detail Colums Start ######
            '===### Start หา Service เพื่อไป Gen First Column
            Dim arrService As New ArrayList
            For cntData As Integer = 0 To dt.Rows.Count - 1
                Dim IsAdd As Boolean = True
                For cntTmp As Integer = 0 To arrService.Count - 1
                    If arrService(cntTmp) = dt.Rows(cntData).Item("item_name") & "" Then
                        IsAdd = False
                        Exit For
                    End If
                Next
                If IsAdd = True Then arrService.Add(dt.Rows(cntData).Item("item_name") & "")
            Next
            arrService.Add("All Service")

            If arrService.Count = 0 Then
                Exit Sub
            End If

            Dim arrServiceDetail As New ArrayList
            With arrServiceDetail
                .Add("Customer")
                .Add("Serve")
                .Add("Waiting Time with KPI")
                .Add("Missed Call")
                .Add("Cancelled")
                .Add("Total Abandon")
                .Add("%Achieve WT to Target")
                .Add("%Abandon")
                .Add("Average Waiting Time")
                .Add("Average Handling Time")
            End With
            '===### End หา Service เพื่อไป Gen First Column

            '===### Start ประกาศตัวแปร
            Dim arrSumCustomer As New ArrayList
            Dim arrSumServe As New ArrayList
            Dim arrSumWT_KPI As New ArrayList
            Dim arrSumMissed_Call As New ArrayList
            Dim arrSumCancelled As New ArrayList
            Dim arrSumTotalAbandon As New ArrayList
            Dim arrSumAchieveWT As New ArrayList
            'Dim arrSumMissedCall As New ArrayList
            Dim arrSumPerAbandon As New ArrayList
            Dim arrSumAverageWT As New ArrayList
            Dim arrSumAverageHT As New ArrayList

            Dim align As String = "right"
            '===### End ประกาศตัวแปร

            For cntMainService As Integer = 0 To arrService.Count - 1
                Dim CurrentService As String = arrService(cntMainService)
                ret.Append("    <tr style='background: yellowgreen;'>")
                ret.Append("        <td align='left' style='color: #000000;'>" & CurrentService & "</td>")
                ret.Append("    </tr>")

                Dim SumServ As Integer = 0
                Dim sumTotalAbandon As Integer = 0
                Dim sumCustomer As Integer = 0
                Dim SumTempServ As Integer = 0
                Dim sumTempTotalAbandon As Integer = 0
                Dim sumTempCustomer As Integer = 0

                For cntService As Integer = 0 To arrServiceDetail.Count - 1


                    '== ถ้าเป็น AllService บางชื่อ ServiceDetail เปลี่ยน
                    Dim ServiceDetailColName As String = arrServiceDetail(cntService)
                    If CurrentService = "All Service" Then
                        Select Case arrServiceDetail(cntService)
                            Case "Customer"
                                ServiceDetailColName = "All Customer"
                            Case "Serve"
                                ServiceDetailColName = "All Serve"
                            Case "Average Waiting Time"
                                ServiceDetailColName = "Over All Average Waiting Time"
                            Case "Average Handling Time"
                                ServiceDetailColName = "Over All Average Handling Time"
                            Case "Waiting Time with KPI"
                                ServiceDetailColName = "All Waiting Time with KPI"
                            Case "Missed Call"
                                ServiceDetailColName = "All Missed Call"
                            Case "Cancelled"
                                ServiceDetailColName = "All Cancelled"
                            Case "Total Abandon"
                                ServiceDetailColName = "Total Abandon"
                            Case "%Achieve WT to Target"
                                ServiceDetailColName = "% Over All Achieve WT to Target"
                            Case "%Abandon"
                                ServiceDetailColName = "% Over All Abandon"
                            Case Else
                                ServiceDetailColName = arrServiceDetail(cntService)
                        End Select
                    End If


                    ret.Append("    <tr style='background: #ffffff;'>")
                    ret.Append("        <td align='left' style='color: #000000;'>" & ServiceDetailColName & "</td>")

                    If arrServiceDetail(cntService) = "Average Waiting Time" Or _
                    arrServiceDetail(cntService) = "Average Handling Time" Then
                        align = "center"
                    Else
                        align = "right"
                    End If

                    Dim TotalRecord As Integer = 0
                    For cntDay As Integer = 0 To TotalDay

                        '===### Start หาข้อมูลจากข้อมูลทั้งหมดจากการคิวรี่ ตามวันที่และ service เพื่อเอาข้อมูลไปใส่ในแต่ละคอลัมน์
                        Dim dateForSearch As String = DateAdd(DateInterval.Day, cntDay, StartDate).ToString("yyyyMMdd")
                        Dim tmpDR() As DataRow
                        tmpDR = dt.Select(" service_date='" & dateForSearch & "' And item_name='" & CurrentService & "'")
                        Dim value As String = ""
                        '===### End  หาข้อมูลจากข้อมูลทั้งหมดจากการคิวรี่ ตามวันที่และ service

                        '===###  อ่านค่าใส่ตัวแปล Value เพื่อใส่ในแต่ละคอลัมน์ , TotalRecord เพื่อใส่ Total Column: SUM แกน x ,array : SUM แกน y
                        Select Case arrServiceDetail(cntService)
                            Case "Customer"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("customer") & ""

                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("customer"))
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumCustomer.Count > cntDay Then
                                    arrSumCustomer(cntDay) = CInt(arrSumCustomer(cntDay)) + value
                                Else
                                    arrSumCustomer.Add(value)
                                End If
                            Case "Serve"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("serve") & ""

                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("serve"))
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumServe.Count > cntDay Then
                                    arrSumServe(cntDay) = CInt(arrSumServe(cntDay)) + value
                                Else
                                    arrSumServe.Add(value)
                                End If
                            Case "%Achieve WT to Target"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("WT_With_KPI_Percen") & ""
                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("WT_With_KPI_Percen"))
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumAchieveWT.Count > cntDay Then
                                    arrSumAchieveWT(cntDay) = CInt(arrSumAchieveWT(cntDay)) + value
                                Else
                                    arrSumAchieveWT.Add(value)
                                End If
                                'Case "%Missed Call"
                                '    value = 0
                                '    If tmpDR.Length > 0 Then
                                '        value = tmpDR(0).Item("Misscall_Percen") & ""
                                '        Try
                                '            TotalRecord += CInt(tmpDR(0).Item("Misscall_Percen"))
                                '        Catch ex As Exception
                                '        End Try
                                '    End If

                                '    If arrSumMissedCall.Count > cntDay Then
                                '        arrSumMissedCall(cntDay) = CInt(arrSumMissedCall(cntDay)) + value
                                '    Else
                                '        arrSumMissedCall.Add(value)
                                '    End If
                            Case "Average Waiting Time"
                                value = 0
                                Dim sum_wt As Integer = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("AVG_WT") & ""
                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("SUM_WT"))
                                    Catch ex As Exception
                                    End Try

                                    If tmpDR(0).Item("SUM_WT") & "" <> "" Then
                                        sum_wt = CInt(tmpDR(0).Item("SUM_WT"))
                                    End If
                                End If

                                If arrSumAverageWT.Count > cntDay Then
                                    arrSumAverageWT(cntDay) = CInt(arrSumAverageWT(cntDay)) + sum_wt
                                Else
                                    arrSumAverageWT.Add(sum_wt)
                                End If
                            Case "Average Handling Time"
                                value = 0
                                Dim sum_ht As Integer = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("AVG_HT") & ""
                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("SUM_HT"))
                                    Catch ex As Exception
                                    End Try

                                    If tmpDR(0).Item("SUM_HT") & "" <> "" Then
                                        sum_ht = CInt(tmpDR(0).Item("SUM_HT"))
                                    End If
                                End If

                                If arrSumAverageHT.Count > cntDay Then
                                    arrSumAverageHT(cntDay) = CInt(arrSumAverageHT(cntDay)) + sum_ht
                                Else
                                    arrSumAverageHT.Add(sum_ht)
                                End If

                            Case "Waiting Time with KPI"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("WT_With_KPI") & ""
                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("WT_With_KPI"))
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumWT_KPI.Count > cntDay Then
                                    arrSumWT_KPI(cntDay) = CInt(arrSumWT_KPI(cntDay)) + value
                                Else
                                    arrSumWT_KPI.Add(value)
                                End If
                            Case "Missed Call"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("Misscall") & ""
                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("Misscall"))
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumMissed_Call.Count > cntDay Then
                                    arrSumMissed_Call(cntDay) = CInt(arrSumMissed_Call(cntDay)) + value
                                Else
                                    arrSumMissed_Call.Add(value)
                                End If
                            Case "Cancelled"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = tmpDR(0).Item("cancel") & ""
                                    Try
                                        TotalRecord += CInt(tmpDR(0).Item("cancel"))
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumCancelled.Count > cntDay Then
                                    arrSumCancelled(cntDay) = CInt(arrSumCancelled(cntDay)) + value
                                Else
                                    arrSumCancelled.Add(value)
                                End If
                            Case "Total Abandon"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = CInt(tmpDR(0).Item("cancel")) + CInt(tmpDR(0).Item("Misscall"))
                                    Try
                                        TotalRecord += value
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumTotalAbandon.Count > cntDay Then
                                    arrSumTotalAbandon(cntDay) = CInt(arrSumTotalAbandon(cntDay)) + value
                                Else
                                    arrSumTotalAbandon.Add(value)
                                End If
                            Case "%Abandon"
                                value = 0
                                If tmpDR.Length > 0 Then
                                    value = CInt((CInt(CInt(tmpDR(0).Item("cancel")) + CInt(tmpDR(0).Item("Misscall"))) / CInt(tmpDR(0).Item("customer"))) * 100)
                                    Try
                                        TotalRecord += value
                                    Catch ex As Exception
                                    End Try
                                End If

                                If arrSumPerAbandon.Count > cntDay Then
                                    arrSumPerAbandon(cntDay) = CInt(arrSumPerAbandon(cntDay)) + value
                                Else
                                    arrSumPerAbandon.Add(value)
                                End If
                        End Select
                        '===### End 

                        '==### Start คำนวนค่าเพื่อแสดงที่แต่ละ column #Not Total Column #SUM แกน y====
                        Dim strValue As String = value
                        If CurrentService = "All Service" Then
                            Select Case arrServiceDetail(cntService)
                                Case "Customer"
                                    strValue = arrSumCustomer(cntDay)
                                Case "Serve"
                                    strValue = arrSumServe(cntDay)
                                Case "%Achieve WT to Target"
                                    strValue = arrSumAchieveWT(cntDay)
                                    'Case "%Missed Call"
                                    '    strValue = arrSumMissedCall(cntDay)
                                Case "Average Waiting Time"
                                    value = CInt(arrSumAverageWT(cntDay)) / IIf(CInt(arrSumServe(cntDay)) = 0, 1, CInt(arrSumServe(cntDay)))
                                    strValue = Engine.Reports.ReportsENG.GetFormatTimeFromSec(value)
                                Case "Average Handling Time"
                                    value = CInt(arrSumAverageHT(cntDay)) / IIf(CInt(arrSumServe(cntDay)) = 0, 1, CInt(arrSumServe(cntDay)))
                                    strValue = Engine.Reports.ReportsENG.GetFormatTimeFromSec(value)
                                Case "Waiting Time with KPI"
                                    strValue = arrSumWT_KPI(cntDay)
                                Case "Missed Call"
                                    strValue = arrSumMissed_Call(cntDay)
                                Case "Cancelled"
                                    strValue = arrSumCancelled(cntDay)
                                Case "Total Abandon"
                                    strValue = CInt(arrSumMissed_Call(cntDay)) + CInt(arrSumCancelled(cntDay))
                                Case "%Abandon"
                                    strValue = CInt((CInt(arrSumMissed_Call(cntDay)) + CInt(arrSumCancelled(cntDay))) / CInt(arrSumCustomer(cntDay)) * 100)
                            End Select
                        End If
                        If strValue = "0" Or strValue = "00:00:00" Then strValue = ""
                        '==### End คำนวนค่าเพื่อแสดงที่แต่ละ column #Not Total Column====

                        ret.Append("        <td align='" & align & "' style='color: #000000;'>" & strValue & "</td>")


                    Next '=== End For TotalDay


                    '==### Start คำนวนค่าเพื่อแสดงที่ column Total ====
                    Dim strTotalRecord As String = TotalRecord.ToString
                    If CurrentService = "All Service" Then
                        '===คำนวน Total กรณีที่เป็น AllService
                        TotalRecord = 0
                        Select Case arrServiceDetail(cntService)
                            Case "Customer"
                                For cntarr As Integer = 0 To arrSumCustomer.Count - 1
                                    TotalRecord += arrSumCustomer(cntarr)
                                Next
                                sumTempCustomer = TotalRecord
                                strTotalRecord = TotalRecord.ToString
                            Case "Serve"
                                For cntarr As Integer = 0 To arrSumServe.Count - 1
                                    TotalRecord += arrSumServe(cntarr)
                                Next
                                strTotalRecord = TotalRecord.ToString

                                For cntarr As Integer = 0 To arrSumServe.Count - 1
                                    SumTempServ += arrSumServe(cntarr)
                                Next
                            Case "%Achieve WT to Target"
                                For cntarr As Integer = 0 To arrSumAchieveWT.Count - 1
                                    TotalRecord += arrSumAchieveWT(cntarr)
                                Next
                                strTotalRecord = TotalRecord.ToString
                                'Case "%Missed Call"
                                '    For cntarr As Integer = 0 To arrSumMissedCall.Count - 1
                                '        TotalRecord += arrSumMissedCall(cntarr)
                                '    Next
                                '    strTotalRecord = TotalRecord.ToString
                            Case "Average Waiting Time"
                                For cntarr As Integer = 0 To arrSumAverageWT.Count - 1
                                    TotalRecord += arrSumAverageWT(cntarr)
                                Next

                                TotalRecord = TotalRecord / IIf(SumTempServ = 0, 1, SumTempServ)
                                strTotalRecord = Engine.Reports.ReportsENG.GetFormatTimeFromSec(TotalRecord)
                                If strTotalRecord = "00:00:00" Then strTotalRecord = ""
                            Case "Average Handling Time"
                                For cntarr As Integer = 0 To arrSumAverageHT.Count - 1
                                    TotalRecord += arrSumAverageHT(cntarr)
                                Next

                                TotalRecord = TotalRecord / IIf(SumTempServ = 0, 1, SumTempServ)
                                strTotalRecord = Engine.Reports.ReportsENG.GetFormatTimeFromSec(TotalRecord)
                                If strTotalRecord = "00:00:00" Then strTotalRecord = ""

                            Case "Waiting Time with KPI"
                                For cntarr As Integer = 0 To arrSumWT_KPI.Count - 1
                                    TotalRecord += arrSumWT_KPI(cntarr)
                                Next
                                strTotalRecord = TotalRecord.ToString
                            Case "Missed Call"
                                For cntarr As Integer = 0 To arrSumMissed_Call.Count - 1
                                    TotalRecord += arrSumMissed_Call(cntarr)
                                Next
                                strTotalRecord = TotalRecord.ToString
                            Case "Cancelled"
                                For cntarr As Integer = 0 To arrSumCancelled.Count - 1
                                    TotalRecord += arrSumCancelled(cntarr)
                                Next
                                strTotalRecord = TotalRecord.ToString
                            Case "Total Abandon"
                                For cntarr As Integer = 0 To arrSumTotalAbandon.Count - 1
                                    TotalRecord += arrSumTotalAbandon(cntarr)
                                Next
                                sumTempTotalAbandon = TotalRecord
                                strTotalRecord = TotalRecord.ToString
                            Case "%Abandon"
                                Dim _total As Integer = CInt((sumTempTotalAbandon / sumTempCustomer) * 100)
                                strTotalRecord = _total.ToString
                        End Select

                    Else
                        '=== คำนวน Total กรณีอื่นๆ ,,,,และเป็น AVG_WT,AVG_HT ถ้าไม่ใช่ AVG_WT,AVG_HT ,,,,TotalRecord ใช้ค่าจากการคำนวนมาด้านบน
                        Select Case arrServiceDetail(cntService)
                            Case "Serve"
                                SumServ = TotalRecord
                            Case "Average Waiting Time"

                                TotalRecord = TotalRecord / IIf(SumServ = 0, 1, SumServ)
                                strTotalRecord = Engine.Reports.ReportsENG.GetFormatTimeFromSec(TotalRecord)
                                If strTotalRecord = "00:00:00" Then strTotalRecord = ""
                            Case "Average Handling Time"

                                TotalRecord = TotalRecord / IIf(SumServ = 0, 1, SumServ)
                                strTotalRecord = Engine.Reports.ReportsENG.GetFormatTimeFromSec(TotalRecord)
                                If strTotalRecord = "00:00:00" Then strTotalRecord = ""
                            Case "Total Abandon"
                                sumTotalAbandon = TotalRecord
                            Case "Customer"
                                sumCustomer = TotalRecord
                            Case "%Abandon"
                                strTotalRecord = CInt((sumTotalAbandon / sumCustomer) * 100)
                            Case Else
                                strTotalRecord = TotalRecord.ToString
                        End Select
                    End If
                    '==### End คำนวนค่าเพื่อแสดงที่ column Total ====

                    ret.Append("        <td align='" & align & "' style='color: #000000;'>" & strTotalRecord & "</td>")
                    ret.Append("    </tr>")

                Next '=== End For arrServiceDetail


            Next '=== End For Service
            '### Detail Colums End ######

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
