'Imports Microsoft.Office.Interop
Imports System.Data
Imports System.IO
Imports System.IO.Packaging
Imports System
Imports System.Windows.Forms
Imports OfficeOpenXml

Public Class frmProcessReportsExcel
    Dim IniFileName As String = Application.StartupPath & "\GenReportExcel.ini"

    Private Sub CreateExcelFile(ByVal excelFile As String, ByVal TmpDt As System.Data.DataTable)
        'Generate Excel File
        If TmpDt.Rows.Count > 0 Then
            Dim dDt As New System.Data.DataTable
            dDt = TmpDt.DefaultView.ToTable(True, "Date")

            'Loop ตามวันที่
            For Each dDr As DataRow In dDt.Rows
                Using ep As New ExcelPackage
                    TmpDt.DefaultView.RowFilter = "Date='" & dDr("Date") & "'"
                    Dim sDt As New System.Data.DataTable
                    sDt = TmpDt.DefaultView.ToTable(True, "item_name", "Date")
                    'Loop ตาม Service
                    For Each sDr As DataRow In sDt.Rows
                        Dim sh As ExcelWorksheet = ep.Workbook.Worksheets.Add(Replace(sDr("item_name"), "/", ""))
                        sh.TabColor = System.Drawing.ColorTranslator.FromWin32(RGB(255, 255, 0))

                        sh.Cells("A1").Value = "Shop Name"
                        sh.Cells("B1").Value = "Region Code"
                        sh.Cells("C1").Value = "Date"
                        sh.Cells("D1").Value = "Service Type"
                        sh.Cells("E1").Value = "Regis"
                        sh.Cells("F1").Value = "Serve"
                        sh.Cells("G1").Value = "Missed Call"
                        sh.Cells("H1").Value = "Cancelled"
                        sh.Cells("I1").Value = "InComplete"
                        sh.Cells("J1").Value = "Waiting Time With KPI"
                        sh.Cells("K1").Value = "Serve With KPI"
                        sh.Cells("L1").Value = " %Achieve WT to Target"
                        sh.Cells("M1").Value = " %Achieve WT over Target"
                        sh.Cells("N1").Value = " %Achieve HT to Target"
                        sh.Cells("O1").Value = " %Achieve HT over Target"
                        sh.Cells("P1").Value = "Missed Call Percent"
                        sh.Cells("Q1").Value = "Average Handling Time"
                        sh.Cells("R1").Value = "Average Waiting Time"
                        sh.Cells("S1").Value = "Max WT HH:MM:SS"
                        sh.Cells("T1").Value = "Max HT HH:MM:SS"

                        Dim totRegis As Long = 0
                        Dim totServe As Long = 0
                        Dim totMisscall As Long = 0
                        Dim totCancel As Long = 0
                        Dim totIncomplete As Long = 0
                        Dim totWTKpi As Long = 0
                        Dim totHTKpi As Long = 0
                        Dim sumWT As Long = 0
                        Dim sumHT As Long = 0
                        Dim MaxWT As Long = 0
                        Dim MaxHT As Long = 0
                        Dim CountWT As Long = 0

                        TmpDt.DefaultView.RowFilter = "Date='" & dDr("Date") & "' and item_name='" & sDr("item_name") & "'"
                        TmpDt.DefaultView.Sort = "shop_code, shop"
                        Dim i As Integer = 2
                        For Each dr As DataRowView In TmpDt.DefaultView
                            sh.Cells("A" & i).Value = dr("Shop")
                            sh.Cells("B" & i).Value = dr("shop_code")
                            sh.Cells("C" & i).Value = dr("Date")
                            sh.Cells("D" & i).Value = dr("item_name")
                            sh.Cells("E" & i).Value = dr("Regis")
                            sh.Cells("F" & i).Value = dr("Serve")
                            sh.Cells("G" & i).Value = dr("Misscall")
                            sh.Cells("H" & i).Value = dr("Cancel")
                            sh.Cells("I" & i).Value = dr("InComplete")
                            sh.Cells("J" & i).Value = dr("WT_With_KPI")
                            sh.Cells("K" & i).Value = dr("HT_With_KPI")
                            If Convert.ToInt64(dr("Serve")) > 0 Then
                                sh.Cells("L" & i).Value = dr("WT_With_KPI_Percen")
                                sh.Cells("M" & i).Value = dr("WT_Over_KPI_Percen")
                                sh.Cells("N" & i).Value = dr("HT_With_KPI_Percen")
                                sh.Cells("O" & i).Value = dr("HT_Over_KPI_Percen")
                            Else
                                sh.Cells("L" & i).Value = "0"
                                sh.Cells("M" & i).Value = "0"
                                sh.Cells("N" & i).Value = "0"
                                sh.Cells("O" & i).Value = "0"
                            End If
                            sh.Cells("P" & i).Value = dr("Misscall_Percen")
                            If Convert.IsDBNull(dr("AVG_HT")) = False Then
                                sh.Cells("Q" & i).Value = dr("AVG_HT")
                            End If
                            If Convert.IsDBNull(dr("AVG_WT")) = False Then
                                sh.Cells("R" & i).Value = dr("AVG_WT")
                            End If
                            If Convert.IsDBNull(dr("MAX_WT")) = False Then
                                sh.Cells("S" & i).Value = dr("MAX_WT")
                            End If
                            If Convert.IsDBNull(dr("MAX_HT")) = False Then
                                sh.Cells("T" & i).Value = dr("MAX_HT")
                            End If

                            totRegis += Convert.ToInt64(dr("Regis"))
                            totServe += Convert.ToInt64(dr("Serve"))
                            totMisscall += Convert.ToInt64(dr("Misscall"))
                            totCancel += Convert.ToInt64(dr("Cancel"))
                            totIncomplete += Convert.ToInt64(dr("InComplete"))
                            totWTKpi += Convert.ToInt64(dr("WT_With_KPI"))
                            totHTKpi += Convert.ToInt64(dr("HT_With_KPI"))
                            If Convert.IsDBNull(dr("SUM_WT")) = False Then
                                sumWT += Convert.ToInt64(dr("SUM_WT"))
                                CountWT += (Convert.ToInt64(dr("Serve")) + Convert.ToInt64(dr("Cancel")))
                            End If
                            If Convert.IsDBNull(dr("SUM_HT")) = False Then
                                sumHT += Convert.ToInt64(dr("SUM_HT"))
                            End If

                            If Convert.IsDBNull(dr("MAX_WT")) = False Then
                                If Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_WT")) > MaxWT Then
                                    MaxWT = Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_WT"))
                                End If
                            End If
                            If Convert.IsDBNull(dr("MAX_HT")) = False Then
                                If Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_HT")) > MaxHT Then
                                    MaxHT = Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_HT"))
                                End If
                            End If
                            i += 1
                        Next

                        ''== Start SubTotal
                        SetRowTotalStyle(sh, i, "T")

                        sh.Cells("A" & i).Value = "Total"
                        sh.Cells("B" & i).Value = ""
                        sh.Cells("C" & i).Value = sDr("Date")
                        sh.Cells("D" & i).Value = sDr("item_name")
                        sh.Cells("E" & i).Value = totRegis
                        sh.Cells("F" & i).Value = totServe
                        sh.Cells("G" & i).Value = totMisscall
                        sh.Cells("H" & i).Value = totCancel
                        sh.Cells("I" & i).Value = totIncomplete
                        sh.Cells("J" & i).Value = totWTKpi
                        sh.Cells("K" & i).Value = totHTKpi
                        If totServe > 0 Then
                            sh.Cells("L" & i).Value = Convert.ToInt64((totWTKpi * 100) / totServe)
                            sh.Cells("M" & i).Value = Convert.ToInt64(100 - ((totWTKpi * 100) / totServe))
                            sh.Cells("N" & i).Value = Convert.ToInt64((totHTKpi * 100) / totServe)
                            sh.Cells("O" & i).Value = Convert.ToInt64(100 - ((totHTKpi * 100) / totServe))
                            sh.Cells("Q" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Math.Floor(sumHT / totServe))
                        Else
                            sh.Cells("L" & i).Value = "0"
                            sh.Cells("M" & i).Value = "0"
                            sh.Cells("N" & i).Value = "0"
                            sh.Cells("O" & i).Value = "0"
                            sh.Cells("Q" & i).Value = "00:00:00"
                        End If
                        If CountWT > 0 Then
                            sh.Cells("R" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Math.Floor(sumWT / CountWT))
                        Else
                            sh.Cells("R" & i).Value = "00:00:00"
                        End If

                        sh.Cells("P" & i).Value = Convert.ToInt64((totMisscall * 100) / totRegis)
                        If MaxWT > 0 Then
                            sh.Cells("S" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(MaxWT)
                        Else
                            sh.Cells("S" & i).Value = "00:00:00"
                        End If
                        If MaxHT > 0 Then
                            sh.Cells("T" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(MaxHT)
                        Else
                            sh.Cells("T" & i).Value = "00:00:00"
                        End If

                        Using shRange As ExcelRange = sh.Cells("A1:T" & i)
                            shRange.AutoFitColumns()

                            shRange.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                            shRange.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                            shRange.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                            shRange.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        End Using
                    Next
                    If rdiDaily.Checked = True Then
                        CreateSummarySheet(ep, TmpDt, dDr("Date"))
                    End If

                    Dim FilePath As String = "D:\ReportExcel"
                    If Directory.Exists(FilePath) = False Then
                        Directory.CreateDirectory(FilePath)
                    End If
                    Dim FileName As String = FilePath & "\" & excelFile & dDr("Date") & ".xlsx"
                    If File.Exists(FileName) = True Then
                        File.Delete(FileName)
                    End If
                    Dim b() As Byte = ep.GetAsByteArray
                    File.WriteAllBytes(FileName, b)
                End Using
            Next
        End If
    End Sub

    Sub SetRowTotalStyle(ByVal sh As ExcelWorksheet, ByVal RowIndex As Integer, ByVal LastCol As String)
        Using RowTotalStyle As ExcelRange = sh.Cells("A" & RowIndex & ":" & LastCol & RowIndex)
            RowTotalStyle.Style.Font.Bold = True
            RowTotalStyle.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            RowTotalStyle.Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        End Using
    End Sub


    'Private Sub CreateExcelFile(ByVal excelFile As String, ByVal TmpDt As System.Data.DataTable)
    '    'Generate Excel File
    '    If TmpDt.Rows.Count > 0 Then
    '        Dim chkApp As New Excel.Application
    '        Dim dDt As New System.Data.DataTable
    '        dDt = TmpDt.DefaultView.ToTable(True, "Date")

    '        'Loop ตามวันที่
    '        For Each dDr As DataRow In dDt.Rows
    '            Dim dailyReportBook As Excel.Workbook = chkApp.Workbooks.Add

    '            TmpDt.DefaultView.RowFilter = "Date='" & dDr("Date") & "'"
    '            Dim sDt As New System.Data.DataTable
    '            sDt = TmpDt.DefaultView.ToTable(True, "item_name", "Date")
    '            'Loop ตาม Service
    '            Dim sh As Excel.Worksheet
    '            For Each sDr As DataRow In sDt.Rows
    '                sh = dailyReportBook.Worksheets.Add
    '                sh.Name = Replace(sDr("item_name"), "/", "")
    '                sh.Tab.Color = RGB(255, 255, 0)

    '                sh.Range("A1").Value = "Shop Name"
    '                sh.Range("B1").Value = "Region Code"
    '                sh.Range("C1").Value = "Date"
    '                sh.Range("D1").Value = "Service Type"
    '                sh.Range("E1").Value = "Regis"
    '                sh.Range("F1").Value = "Serve"
    '                sh.Range("G1").Value = "Missed Call"
    '                sh.Range("H1").Value = "Cancelled"
    '                sh.Range("I1").Value = "InComplete"
    '                sh.Range("J1").Value = "Waiting Time With KPI"
    '                sh.Range("K1").Value = "Serve With KPI"
    '                sh.Range("L1").Value = " %Achieve WT to Target"
    '                sh.Range("M1").Value = " %Achieve WT over Target"
    '                sh.Range("N1").Value = " %Achieve HT to Target"
    '                sh.Range("O1").Value = " %Achieve HT over Target"
    '                sh.Range("P1").Value = "Missed Call Percent"
    '                sh.Range("Q1").Value = "Average Handling Time"
    '                sh.Range("R1").Value = "Average Waiting Time"
    '                sh.Range("S1").Value = "Max WT HH:MM:SS"
    '                sh.Range("T1").Value = "Max HT HH:MM:SS"

    '                Dim totRegis As Long = 0
    '                Dim totServe As Long = 0
    '                Dim totMisscall As Long = 0
    '                Dim totCancel As Long = 0
    '                Dim totIncomplete As Long = 0
    '                Dim totWTKpi As Long = 0
    '                Dim totHTKpi As Long = 0
    '                Dim sumWT As Long = 0
    '                Dim sumHT As Long = 0
    '                Dim MaxWT As Long = 0
    '                Dim MaxHT As Long = 0
    '                Dim CountWT As Long = 0

    '                TmpDt.DefaultView.RowFilter = "Date='" & dDr("Date") & "' and item_name='" & sDr("item_name") & "'"
    '                TmpDt.DefaultView.Sort = "shop_code, shop"
    '                Dim i As Integer = 2
    '                For Each dr As DataRowView In TmpDt.DefaultView
    '                    sh.Range("A" & i).Value = dr("Shop")
    '                    sh.Range("B" & i).Value = dr("shop_code")
    '                    sh.Range("C" & i).Value = dr("Date")
    '                    sh.Range("D" & i).Value = dr("item_name")
    '                    sh.Range("E" & i).Value = dr("Regis")
    '                    sh.Range("F" & i).Value = dr("Serve")
    '                    sh.Range("G" & i).Value = dr("Misscall")
    '                    sh.Range("H" & i).Value = dr("Cancel")
    '                    sh.Range("I" & i).Value = dr("InComplete")
    '                    sh.Range("J" & i).Value = dr("WT_With_KPI")
    '                    sh.Range("K" & i).Value = dr("HT_With_KPI")
    '                    If Convert.ToInt64(dr("Serve")) > 0 Then
    '                        sh.Range("L" & i).Value = dr("WT_With_KPI_Percen")
    '                        sh.Range("M" & i).Value = dr("WT_Over_KPI_Percen")
    '                        sh.Range("N" & i).Value = dr("HT_With_KPI_Percen")
    '                        sh.Range("O" & i).Value = dr("HT_Over_KPI_Percen")
    '                    Else
    '                        sh.Range("L" & i).Value = "0"
    '                        sh.Range("M" & i).Value = "0"
    '                        sh.Range("N" & i).Value = "0"
    '                        sh.Range("O" & i).Value = "0"
    '                    End If
    '                    sh.Range("P" & i).Value = dr("Misscall_Percen")
    '                    If Convert.IsDBNull(dr("AVG_HT")) = False Then
    '                        'If dr("AVG_HT").ToString() <> "00:00:00" Then
    '                        sh.Range("Q" & i).Value = dr("AVG_HT")
    '                        'End If
    '                    End If
    '                    If Convert.IsDBNull(dr("AVG_WT")) = False Then
    '                        'If dr("AVG_WT").ToString() <> "00:00:00" Then
    '                        sh.Range("R" & i).Value = dr("AVG_WT")
    '                        'End If
    '                    End If
    '                    If Convert.IsDBNull(dr("MAX_WT")) = False Then
    '                        'If dr("MAX_WT").ToString() <> "00:00:00" Then
    '                        sh.Range("S" & i).Value = dr("MAX_WT")
    '                        'End If
    '                    End If
    '                    If Convert.IsDBNull(dr("MAX_HT")) = False Then
    '                        'If dr("MAX_HT").ToString() <> "00:00:00" Then
    '                        sh.Range("T" & i).Value = dr("MAX_HT")
    '                        'End If
    '                    End If

    '                    totRegis += Convert.ToInt64(dr("Regis"))
    '                    totServe += Convert.ToInt64(dr("Serve"))
    '                    totMisscall += Convert.ToInt64(dr("Misscall"))
    '                    totCancel += Convert.ToInt64(dr("Cancel"))
    '                    totIncomplete += Convert.ToInt64(dr("InComplete"))
    '                    totWTKpi += Convert.ToInt64(dr("WT_With_KPI"))
    '                    totHTKpi += Convert.ToInt64(dr("HT_With_KPI"))
    '                    If Convert.IsDBNull(dr("SUM_WT")) = False Then
    '                        sumWT += Convert.ToInt64(dr("SUM_WT"))
    '                        CountWT += (Convert.ToInt64(dr("Serve")) + Convert.ToInt64(dr("Cancel")))
    '                    End If
    '                    If Convert.IsDBNull(dr("SUM_HT")) = False Then
    '                        sumHT += Convert.ToInt64(dr("SUM_HT"))
    '                    End If

    '                    If Convert.IsDBNull(dr("MAX_WT")) = False Then
    '                        If Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_WT")) > MaxWT Then
    '                            MaxWT = Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_WT"))
    '                        End If
    '                    End If
    '                    If Convert.IsDBNull(dr("MAX_HT")) = False Then
    '                        If Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_HT")) > MaxHT Then
    '                            MaxHT = Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_HT"))
    '                        End If
    '                    End If
    '                    i += 1
    '                Next

    '                sh.Range("A" & i, "T" & i).Font.Bold = True
    '                sh.Range("A" & i, "T" & i).Borders.Weight = 2

    '                sh.Range("A" & i).Value = "Total"
    '                sh.Range("B" & i).Value = ""
    '                sh.Range("C" & i).Value = sDr("Date")
    '                sh.Range("D" & i).Value = sDr("item_name")
    '                sh.Range("E" & i).Value = totRegis
    '                sh.Range("F" & i).Value = totServe
    '                sh.Range("G" & i).Value = totMisscall
    '                sh.Range("H" & i).Value = totCancel
    '                sh.Range("I" & i).Value = totIncomplete
    '                sh.Range("J" & i).Value = totWTKpi
    '                sh.Range("K" & i).Value = totHTKpi
    '                If totServe > 0 Then
    '                    sh.Range("L" & i).Value = Convert.ToInt64((totWTKpi * 100) / totServe)
    '                    sh.Range("M" & i).Value = Convert.ToInt64(100 - ((totWTKpi * 100) / totServe))
    '                    sh.Range("N" & i).Value = Convert.ToInt64((totHTKpi * 100) / totServe)
    '                    sh.Range("O" & i).Value = Convert.ToInt64(100 - ((totHTKpi * 100) / totServe))
    '                    sh.Range("Q" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Math.Floor(sumHT / totServe))
    '                Else
    '                    sh.Range("L" & i).Value = "0"
    '                    sh.Range("M" & i).Value = "0"
    '                    sh.Range("N" & i).Value = "0"
    '                    sh.Range("O" & i).Value = "0"
    '                    sh.Range("Q" & i).Value = "00:00:00"
    '                End If
    '                If CountWT > 0 Then
    '                    sh.Range("R" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Math.Floor(sumWT / CountWT))
    '                Else
    '                    sh.Range("R" & i).Value = "00:00:00"
    '                End If

    '                sh.Range("P" & i).Value = Convert.ToInt64((totMisscall * 100) / totRegis)
    '                If MaxWT > 0 Then
    '                    sh.Range("S" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(MaxWT)
    '                Else
    '                    sh.Range("S" & i).Value = "00:00:00"
    '                End If
    '                If MaxHT > 0 Then
    '                    sh.Range("T" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(MaxHT)
    '                Else
    '                    sh.Range("T" & i).Value = "00:00:00"
    '                End If
    '                sh.Columns.AutoFit()
    '            Next
    '            If rdiDaily.Checked = True Then
    '                CreateSummarySheet(dailyReportBook, TmpDt, dDr("Date"))
    '            End If

    '            Dim FilePath As String = "D:\ReportExcel"
    '            If Directory.Exists(FilePath) = False Then
    '                Directory.CreateDirectory(FilePath)
    '            End If
    '            Dim FileName As String = FilePath & "\" & excelFile & dDr("Date") & ".xlsx"
    '            If File.Exists(FileName) = True Then
    '                File.Delete(FileName)
    '            End If
    '            dailyReportBook.SaveAs(FileName)
    '            dailyReportBook.Close()
    '        Next
    '        chkApp = Nothing
    '    End If
    'End Sub

    Private Function SetNullToZero(ByVal TmpDt As System.Data.DataTable) As System.Data.DataTable
        If TmpDt.Rows.Count > 0 Then
            For i As Integer = 0 To TmpDt.Rows.Count - 1
                If Convert.IsDBNull(TmpDt.Rows(i)("Regis")) = True Then
                    TmpDt.Rows(i)("Regis") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("Serve")) = True Then
                    TmpDt.Rows(i)("Serve") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("Misscall")) = True Then
                    TmpDt.Rows(i)("Misscall") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("cancel")) = True Then
                    TmpDt.Rows(i)("cancel") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("incomplete")) = True Then
                    TmpDt.Rows(i)("incomplete") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("wt_with_kpi")) = True Then
                    TmpDt.Rows(i)("wt_with_kpi") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("ht_with_kpi")) = True Then
                    TmpDt.Rows(i)("ht_with_kpi") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("sum_wt")) = True Then
                    TmpDt.Rows(i)("sum_wt") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("sum_ht")) = True Then
                    TmpDt.Rows(i)("sum_ht") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("WT_With_KPI_Percen")) = True Then
                    TmpDt.Rows(i)("WT_With_KPI_Percen") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("WT_Over_KPI_Percen")) = True Then
                    TmpDt.Rows(i)("WT_Over_KPI_Percen") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("HT_With_KPI_Percen")) = True Then
                    TmpDt.Rows(i)("HT_With_KPI_Percen") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("HT_Over_KPI_Percen")) = True Then
                    TmpDt.Rows(i)("HT_Over_KPI_Percen") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("AVG_HT")) = True Then
                    TmpDt.Rows(i)("AVG_HT") = "00:00:00"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("AVG_WT")) = True Then
                    TmpDt.Rows(i)("AVG_WT") = "00:00:00"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("Misscall_Percen")) = True Then
                    TmpDt.Rows(i)("Misscall_Percen") = "0"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("max_wt")) = True Then
                    TmpDt.Rows(i)("max_wt") = "00:00:00"
                End If
                If Convert.IsDBNull(TmpDt.Rows(i)("max_ht")) = True Then
                    TmpDt.Rows(i)("max_ht") = "00:00:00"
                End If
            Next
        End If
        Return TmpDt
    End Function

    Private Sub CreateSummarySheet(ByVal dailyReportBook As ExcelPackage, ByVal TmpDt As System.Data.DataTable, ByVal drDate As String)

        'TmpDt.DefaultView.RowFilter = "Date='" & drDate & "' "
        'TmpDt.DefaultView.Sort = "shop"

        Dim dtSum As New System.Data.DataTable
        dtSum = TmpDt.Clone
        If dtSum.Columns.Contains("item_name") = True Then
            dtSum.Columns.Remove("item_name")
        End If
        If dtSum.Columns.Contains("master_itemid") = True Then
            dtSum.Columns.Remove("master_itemid")
        End If

        TmpDt.DefaultView.RowFilter = ""
        Dim shDt As New System.Data.DataTable
        shDt = TmpDt.DefaultView.ToTable(True, "shop", "shop_code")
        For Each shDr As DataRow In shDt.Rows
            TmpDt.DefaultView.RowFilter = " Shop='" & shDr("Shop") & "' and date='" & drDate & "'"
            If TmpDt.DefaultView.Count > 0 Then
                Dim drSum As DataRow = dtSum.NewRow
                drSum("Shop") = shDr("Shop")
                drSum("shop_code") = shDr("shop_code")
                drSum("Date") = drDate
                drSum("Regis") = TmpDt.Compute("sum(regis)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("Serve") = TmpDt.Compute("sum(Serve)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("Misscall") = TmpDt.Compute("sum(Misscall)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("cancel") = TmpDt.Compute("sum(cancel)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("incomplete") = TmpDt.Compute("sum(incomplete)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("wt_with_kpi") = TmpDt.Compute("sum(wt_with_kpi)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("ht_with_kpi") = TmpDt.Compute("sum(ht_with_kpi)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("sum_wt") = TmpDt.Compute("sum(sum_wt)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("sum_ht") = TmpDt.Compute("sum(sum_ht)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                If Convert.ToInt64(drSum("Serve")) > 0 Then
                    drSum("WT_With_KPI_Percen") = (Convert.ToInt64(drSum("wt_with_kpi")) * 100) / Convert.ToInt64(drSum("Serve"))
                    drSum("WT_Over_KPI_Percen") = 100 - Convert.ToInt64(drSum("wt_with_kpi_percen"))
                    drSum("HT_With_KPI_Percen") = (Convert.ToInt64(drSum("ht_with_kpi")) * 100) / Convert.ToInt64(drSum("Serve"))
                    drSum("HT_Over_KPI_Percen") = 100 - Convert.ToInt64(drSum("HT_With_KPI_Percen"))
                    drSum("AVG_HT") = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(drSum("sum_ht")) / Convert.ToInt64(drSum("Serve")))
                Else
                    drSum("WT_With_KPI_Percen") = "0"
                    drSum("WT_Over_KPI_Percen") = "0"
                    drSum("HT_With_KPI_Percen") = "0"
                    drSum("HT_Over_KPI_Percen") = "0"
                    drSum("AVG_HT") = "0:00:00"
                End If
                If Convert.ToInt64(drSum("cancel")) + Convert.ToInt64(drSum("Serve")) > 0 Then
                    drSum("AVG_WT") = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(drSum("sum_wt")) / (Convert.ToInt64(drSum("cancel")) + Convert.ToInt64(drSum("Serve"))))
                Else
                    drSum("AVG_WT") = "0:00:00"
                End If
                If Convert.ToInt64(drSum("Regis")) > 0 Then
                    drSum("Misscall_Percen") = (Convert.ToInt64(drSum("Misscall")) * 100) / Convert.ToInt64(drSum("Regis"))
                End If
                drSum("max_wt") = TmpDt.Compute("max(max_wt)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")
                drSum("max_ht") = TmpDt.Compute("max(max_ht)", " Shop='" & shDr("Shop") & "' and date='" & drDate & "'")

                dtSum.Rows.Add(drSum)
            End If
            TmpDt.DefaultView.RowFilter = ""
        Next
        shDt.Dispose()

        'Summary Sheet
        Dim sh As ExcelWorksheet = dailyReportBook.Workbook.Worksheets.Add("Summary")
        sh.TabColor = System.Drawing.ColorTranslator.FromWin32(RGB(0, 255, 0))

        sh.Cells("A1").Value = "Shop Name"
        sh.Cells("B1").Value = "Shop Code"
        sh.Cells("C1").Value = "Date"
        sh.Cells("D1").Value = "Service Type"
        sh.Cells("E1").Value = "Regis"
        sh.Cells("F1").Value = "Serve"
        sh.Cells("G1").Value = "Missed Call"
        sh.Cells("H1").Value = "Cancelled"
        sh.Cells("I1").Value = "InComplete"
        sh.Cells("J1").Value = "Waiting Time With KPI"
        sh.Cells("K1").Value = "Serve With KPI"
        sh.Cells("L1").Value = " %Achieve WT to Target"
        sh.Cells("M1").Value = " %Achieve WT over Target"
        sh.Cells("N1").Value = " %Achieve HT to Target"
        sh.Cells("O1").Value = " %Achieve HT over Target"
        sh.Cells("P1").Value = "Missed Call Percent"
        sh.Cells("Q1").Value = "Average Handling Time"
        sh.Cells("R1").Value = "Average Waiting Time"
        sh.Cells("S1").Value = "Max WT HH:MM:SS"
        sh.Cells("T1").Value = "Max HT HH:MM:SS"

        Dim totRegis As Long = 0
        Dim totServe As Long = 0
        Dim totMisscall As Long = 0
        Dim totCancel As Long = 0
        Dim totIncomplete As Long = 0
        Dim totWTKpi As Long = 0
        Dim totHTKpi As Long = 0
        Dim sumWT As Long = 0
        Dim sumHT As Long = 0
        Dim CountWT As Integer = 0
        Dim MaxWT As Long = 0
        Dim MaxHT As Long = 0

        Dim i As Integer = 2
        For Each dr As DataRow In dtSum.Rows
            sh.Cells("A" & i).Value = dr("Shop")
            sh.Cells("B" & i).Value = dr("Shop_code")
            sh.Cells("C" & i).Value = dr("Date")
            sh.Cells("D" & i).Value = "All"
            sh.Cells("E" & i).Value = dr("Regis")
            sh.Cells("F" & i).Value = dr("Serve")
            sh.Cells("G" & i).Value = dr("Misscall")
            sh.Cells("H" & i).Value = dr("Cancel")
            sh.Cells("I" & i).Value = dr("InComplete")
            sh.Cells("J" & i).Value = dr("WT_With_KPI")
            sh.Cells("K" & i).Value = dr("HT_With_KPI")
            sh.Cells("L" & i).Value = dr("WT_With_KPI_Percen")
            sh.Cells("M" & i).Value = dr("WT_Over_KPI_Percen")
            sh.Cells("N" & i).Value = dr("HT_With_KPI_Percen")
            sh.Cells("O" & i).Value = dr("HT_Over_KPI_Percen")
            sh.Cells("P" & i).Value = dr("Misscall_Percen")
            sh.Cells("Q" & i).Value = dr("AVG_HT")
            sh.Cells("R" & i).Value = dr("AVG_WT")

            If Convert.IsDBNull(dr("MAX_WT")) = False Then
                'If dr("MAX_WT").ToString() <> "0:00:00" Then
                sh.Cells("S" & i).Value = dr("MAX_WT")
                'End If
            End If
            If Convert.IsDBNull(dr("MAX_HT")) = False Then
                'If dr("MAX_HT").ToString() <> "0:00:00" Then
                sh.Cells("T" & i).Value = dr("MAX_HT")
                'End If
            End If

            totRegis += Convert.ToInt64(dr("Regis"))
            totServe += Convert.ToInt64(dr("Serve"))
            totMisscall += Convert.ToInt64(dr("Misscall"))
            totCancel += Convert.ToInt64(dr("Cancel"))
            totIncomplete += Convert.ToInt64(dr("InComplete"))
            totWTKpi += Convert.ToInt64(dr("WT_With_KPI"))
            totHTKpi += Convert.ToInt64(dr("HT_With_KPI"))
            If Convert.IsDBNull(dr("SUM_WT")) = False Then
                sumWT += Convert.ToInt64(dr("SUM_WT"))
                CountWT += (Convert.ToInt64(dr("Serve")) + Convert.ToInt64(dr("Cancel")))
            End If
            If Convert.IsDBNull(dr("SUM_HT")) = False Then
                sumHT += Convert.ToInt64(dr("SUM_HT"))
            End If

            If Convert.IsDBNull(dr("MAX_WT")) = False Then
                If Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_WT")) > MaxWT Then
                    MaxWT = Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_WT"))
                End If
            End If
            If Convert.IsDBNull(dr("MAX_HT")) = False Then
                If Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_HT")) > MaxHT Then
                    MaxHT = Engine.Reports.ReportsENG.GetSecFromTimeFormat(dr("MAX_HT"))
                End If
            End If
            i += 1
        Next

        SetRowTotalStyle(sh, i, "T")

        sh.Cells("A" & i).Value = "Total"
        sh.Cells("B" & i).Value = ""
        sh.Cells("C" & i).Value = drDate
        sh.Cells("D" & i).Value = "All"
        sh.Cells("E" & i).Value = totRegis
        sh.Cells("F" & i).Value = totServe
        sh.Cells("G" & i).Value = totMisscall
        sh.Cells("H" & i).Value = totCancel
        sh.Cells("I" & i).Value = totIncomplete
        sh.Cells("J" & i).Value = totWTKpi
        sh.Cells("K" & i).Value = totHTKpi
        If totServe > 0 Then
            sh.Cells("L" & i).Value = Convert.ToInt64((totWTKpi * 100) / totServe)
            sh.Cells("M" & i).Value = Convert.ToInt64(100 - ((totWTKpi * 100) / totServe))
            sh.Cells("N" & i).Value = Convert.ToInt64((totHTKpi * 100) / totServe)
            sh.Cells("O" & i).Value = Convert.ToInt64(100 - ((totHTKpi * 100) / totServe))
            sh.Cells("Q" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Math.Floor(sumHT / totServe))
        Else
            sh.Cells("L" & i).Value = "0"
            sh.Cells("M" & i).Value = "0"
            sh.Cells("N" & i).Value = "0"
            sh.Cells("O" & i).Value = "0"
            sh.Cells("Q" & i).Value = "0:00:00"
        End If
        If CountWT > 0 Then
            sh.Cells("R" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(Math.Floor(sumWT / CountWT))
        End If

        sh.Cells("P" & i).Value = Convert.ToInt64((totMisscall * 100) / totRegis)
        If MaxWT > 0 Then
            sh.Cells("S" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(MaxWT)
        Else
            sh.Cells("S" & i).Value = "0:00:00"
        End If
        If MaxHT > 0 Then
            sh.Cells("T" & i).Value = Engine.Reports.ReportsENG.GetFormatTimeFromSec(MaxHT)
        Else
            sh.Cells("T" & i).Value = "0:00:00"
        End If

        Using shRange As ExcelRange = sh.Cells("A1:T" & i)
            shRange.AutoFitColumns()

            shRange.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
            shRange.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
            shRange.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
            shRange.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
        End Using
    End Sub


    'This sub demonstrates adding files to a zip
    Private Function AddFileToZip(ByVal filePath As String, ByVal ZipFileName As String, Optional ByVal uri As String = "") As ArchiveFile

        'ArchiveFile is a custom class that stores the File Name, Type, Modified, Uri,
        '  and gets the correct system icon.
        Dim archFile As New ArchiveFile(filePath)

        'Open the zip file if it exists, else create a new one
        Dim zip As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)

        'If no Uri was provided, then create one from the existing file path
        '   An optional route would be to just use the file name as the Uri, but then
        '   it will extract to the root directory. 
        If uri <> "" Then
            'Change all backward slashes to forward slashes
            uri = uri.Replace("\", "/")
        Else
            'Uri was not provided, so use the name of the file:
            uri = String.Concat("/", IO.Path.GetFileName(filePath))

            'Spaces cannot appear in the file name, so replace them with underscores.
            uri = uri.Replace(" ", "_")
        End If

        Dim partUri As New Uri(uri, UriKind.Relative)
        Dim contentType As String = Net.Mime.MediaTypeNames.Application.Zip   'constant: "application/zip"

        'The PackagePart contains the information:
        '   Where to extract the file when it's extracted (partUri)
        '   The type of content stream (MIME type) - (contentType)
        '   The type of compression to use (CompressionOption.Normal)
        Dim pkgPart As PackagePart = zip.CreatePart(partUri, contentType, CompressionOption.Normal)

        'Read all of the bytes from the file to add to the zip file
        Dim bites As Byte() = File.ReadAllBytes(filePath)

        'Compress and write the bytes to the zip file
        pkgPart.Package.PackageProperties.Modified = archFile.Modified
        pkgPart.GetStream().Write(bites, 0, bites.Length)

        'store the Uri in the Custom ArchiveFile
        archFile.Uri = uri

        zip.Close()  'Close the zip file

        Return archFile

    End Function

    Public Sub GetDataAccumAllShop(ByVal ExcelFile As String, ByVal vDateFrom As String, ByVal vDateTo As String)
        'vDate Format = YYYY-MM-DD
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

        Dim shSql As String = "select s.id,s.shop_db_name,s.shop_db_userid, s.shop_db_server, s.shop_db_pwd, "
        shSql += " s.shop_name_en, r.location_group "
        shSql += " from TB_SHOP s"
        shSql += " inner join TB_REGION r on r.id=s.region_id"
        shSql += " where s.active='Y'"
        shSql += " order by r.location_group, s.shop_name_en"

        Dim shDt As New System.Data.DataTable
        shDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(shSql)
        If shDt.Rows.Count > 0 Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim TmpDt As New System.Data.DataTable
            For Each shDr As DataRow In shDt.Rows
                Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
                shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(shDr("id"))
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq, "archMasterENG.GetDataAccumAllShop")
                If shTrans.Trans IsNot Nothing Then
                    'If shTrans.CreateTransaction(shDr("shop_db_server"), shDr("shop_db_userid"), shDr("shop_db_pwd"), shDr("shop_db_name")) = True Then
                    Dim sql As String = "declare @ST as datetime; " & vbNewLine
                    sql += " declare @ET as datetime; " & vbNewLine
                    sql += " declare @STT as varchar(5); select @STT = '08:00';" & vbNewLine
                    sql += " declare @ETT as varchar(5); select @ETT = '22:00';" & vbNewLine
                    sql += " set @ST = '" & vDateFrom & "'" & vbNewLine
                    sql += " set @ET = '" & vDateTo & "'" & vbNewLine
                    sql += " declare @ShopName varchar(255);set @ShopName='" & shDr("SHOP_NAME_EN") & "'" & vbNewLine
                    sql += " declare @ShopCode varchar(255);set @ShopCode='" & shDr("location_group") & "'" & vbNewLine
                    sql += " select a.Shop, a.shop_code, convert(varchar(8),@ST,112) + ' To ' + convert(varchar(8),@ET,112) as [date],a.item_name,a.master_itemid," & vbNewLine
                    sql += " SUM(a.Regis) regis, SUM(a.serve) serve, SUM(a.Misscall) misscall," & vbNewLine
                    sql += " SUM(a.Cancel) cancel, SUM(a.Incomplete) incomplete,SUM(a.WT_With_KPI) WT_With_KPI," & vbNewLine
                    sql += " SUM(a.HT_With_KPI) HT_With_KPI, " & vbNewLine
                    sql += " case when SUM(a.serve) = 0 then 0 else " & vbNewLine
                    sql += " 	convert(int,round(((SUM(a.WT_With_KPI) * 100) / cast(SUM(a.serve) as money)),0)) end as WT_With_KPI_Percen," & vbNewLine
                    sql += " case when SUM(a.serve) = 0 then 0 else " & vbNewLine
                    sql += " 	convert(int,round((((SUM(a.serve) - SUM(a.WT_With_KPI)) * 100) / cast(SUM(a.serve) as money)),0)) end as WT_Over_KPI_Percen," & vbNewLine
                    sql += " case when SUM(a.serve) = 0 then 0 else " & vbNewLine
                    sql += " 	convert(int,round(((SUM(a.HT_With_KPI) * 100) / cast(SUM(a.serve) as money)),0)) end as HT_With_KPI_Percen," & vbNewLine
                    sql += " case when SUM(a.serve) = 0 then 0 else " & vbNewLine
                    sql += " 	convert(int,round((((SUM(a.serve) - SUM(a.HT_With_KPI)) * 100) / cast(SUM(a.serve) as money)),0)) end as HT_Over_KPI_Percen," & vbNewLine
                    sql += " case when SUM(a.serve) = 0 then 0 else " & vbNewLine
                    sql += " 	convert(int,round(((SUM(a.Misscall) * 100) / cast(SUM(a.Regis) as money)),0)) end as Misscall_Percen," & vbNewLine
                    sql += " MAX(a.MAX_HT) MAX_HT, MAX(a.MAX_WT) MAX_WT," & vbNewLine
                    sql += " case when SUM(a.serve)+ISNULL(SUM(a.cancel),0) = 0 then 0 else SUM(a.SUM_WT)/(SUM(a.serve)+ISNULL(SUM(a.cancel),0)) end AVG_WT," & vbNewLine
                    sql += " case when SUM(a.serve) = 0 then 0 else SUM(a.SUM_HT)/SUM(a.serve) end AVG_HT1," & vbNewLine
                    sql += " SUM(a.SUM_WT) SUM_WT, SUM(a.SUM_HT) SUM_HT"
                    sql += " from (" & vbNewLine
                    sql += LoadSqlFromTextFile()
                    sql += " ) a"
                    sql += " group by a.Shop,a.shop_code, a.item_name, a.master_itemid"

                    Dim dt As New System.Data.DataTable
                    dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
                    If dt.Rows.Count > 0 Then   'ข้อมูลที่จะได้คือ 1 Shop มีหลาย Service
                        'dt.Columns.Add("avg_wt")
                        dt.Columns.Add("avg_ht")
                        For i As Integer = 0 To dt.Rows.Count - 1
                            'If Convert.IsDBNull(dt.Rows(i)("avg_wt1")) = False Then
                            '    If Convert.ToInt64(dt.Rows(i)("avg_wt1")) > 0 Then
                            '        dt.Rows(i)("avg_wt") = Engine.Reports.ReportsENG.GetFormatTimeFromSec(dt.Rows(i)("avg_wt1"))
                            '    End If
                            'End If
                            If Convert.IsDBNull(dt.Rows(i)("avg_ht1")) = False Then
                                If Convert.ToInt64(dt.Rows(i)("avg_ht1")) > 0 Then
                                    dt.Rows(i)("avg_ht") = Engine.Reports.ReportsENG.GetFormatTimeFromSec(dt.Rows(i)("avg_ht1"))
                                End If
                            End If
                        Next
                        TmpDt = InsertToTmpDt(dt, TmpDt)
                    End If
                    dt.Dispose()
                    shTrans.CommitTransaction()
                End If
                shLnq = Nothing
            Next
            shDt.Dispose()
            trans.CommitTransaction()

            If TmpDt.Rows.Count > 0 Then
                TmpDt = SetNullToZero(TmpDt)
                MappingItemName(TmpDt)
                CreateExcelFile(ExcelFile, TmpDt)
            End If
            TmpDt.Dispose()
        End If
    End Sub

    Public Sub GetDataAllShop(ByVal excelFile As String, ByVal vDateFrom As String, ByVal vDateTo As String)
        'vDate Format = YYYY-MM-DD
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")


        Dim shSql As String = "select s.id,s.shop_db_name,s.shop_db_userid, s.shop_db_server, s.shop_db_pwd, "
        shSql += " s.shop_name_en, r.location_group "
        shSql += " from TB_SHOP s"
        shSql += " inner join TB_REGION r on r.id=s.region_id"
        shSql += " where s.active='Y'"
        shSql += " order by r.location_group, s.shop_name_en"

        Dim shDt As New System.Data.DataTable
        shDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(shSql)
        'MessageBox.Show(shDt.Rows.Count)

        If shDt.Rows.Count > 0 Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim TmpDt As New System.Data.DataTable

            For Each shDr As DataRow In shDt.Rows
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(shDr("id"), "frmProcessReportsExcel.GetDataAllShop")
                If shTrans.Trans IsNot Nothing Then
                    'If shTrans.CreateTransaction(shDr("shop_db_server"), shDr("shop_db_userid"), shDr("shop_db_pwd"), shDr("shop_db_name")) = True Then
                    Dim sql As String = "declare @ST as datetime; " & vbNewLine
                    sql += " declare @ET as datetime; " & vbNewLine
                    sql += " declare @STT as varchar(5); select @STT = '08:00';" & vbNewLine
                    sql += " declare @ETT as varchar(5); select @ETT = '22:00';" & vbNewLine
                    sql += " set @ST = '" & vDateFrom & "'" & vbNewLine
                    sql += " set @ET = '" & vDateTo & "'" & vbNewLine
                    sql += " declare @ShopName varchar(255);set @ShopName='" & shDr("SHOP_NAME_EN") & "'" & vbNewLine
                    sql += " declare @ShopCode varchar(255);set @ShopCode='" & shDr("location_group") & "'"
                    sql += LoadSqlFromTextFile()
                    sql += " order by tb1.item_order"

                    Dim dt As New System.Data.DataTable
                    dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
                    If dt.Rows.Count > 0 Then   'ข้อมูลที่จะได้คือ 1 Shop มีหลาย Service ใน 1 วัน
                        TmpDt = InsertToTmpDt(dt, TmpDt)
                    End If
                    dt.Dispose()
                    shTrans.CommitTransaction()
                End If
            Next
            shDt.Dispose()

            If TmpDt.Rows.Count > 0 Then
                TmpDt = SetNullToZero(TmpDt)
                MappingItemName(TmpDt)
                CreateExcelFile(excelFile, TmpDt)
            End If
            TmpDt.Dispose()
        End If
    End Sub


    Private Sub MappingItemName(ByVal TmpDt As System.Data.DataTable)
        Try
            Dim mItemDT As New System.Data.DataTable
            mItemDT = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable("select id, item_name from tb_item")
            If mItemDT.Rows.Count > 0 Then
                If TmpDt.Columns.Contains("master_itemid") = True Then
                    If TmpDt.Rows.Count > 0 Then
                        For i As Integer = 0 To TmpDt.Rows.Count - 1
                            If Convert.IsDBNull(TmpDt.Rows(i)("master_itemid")) = False Then
                                mItemDT.DefaultView.RowFilter = "id='" & TmpDt.Rows(i)("master_itemid") & "'"
                                If mItemDT.DefaultView.Count > 0 Then
                                    TmpDt.Rows(i)("item_name") = mItemDT.DefaultView(0)("item_name")
                                End If
                                mItemDT.DefaultView.RowFilter = ""
                            End If
                        Next
                    End If
                End If
            End If
            mItemDT.Dispose()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function InsertToTmpDt(ByVal dt As System.Data.DataTable, ByVal TmpDt As System.Data.DataTable) As System.Data.DataTable
        If TmpDt.Rows.Count = 0 Then
            TmpDt = dt.Clone
            If TmpDt.Columns.Contains("AVG_WT") = True Then
                TmpDt.Columns("AVG_WT").DataType = GetType(String)
            End If
        End If
        For Each dr As DataRow In dt.Rows
            Dim TmpDr As DataRow = TmpDt.NewRow
            TmpDr("Shop") = dr("Shop")
            TmpDr("shop_code") = dr("shop_code")
            TmpDr("Date") = dr("Date")
            TmpDr("item_name") = dr("item_name")
            TmpDr("Regis") = dr("Regis")
            TmpDr("Serve") = dr("Serve")
            TmpDr("Misscall") = dr("Misscall")
            TmpDr("Cancel") = dr("Cancel")
            TmpDr("InComplete") = dr("InComplete")
            TmpDr("WT_With_KPI") = dr("WT_With_KPI")
            TmpDr("HT_With_KPI") = dr("HT_With_KPI")
            TmpDr("WT_With_KPI_Percen") = dr("WT_With_KPI_Percen")
            TmpDr("WT_Over_KPI_Percen") = dr("WT_Over_KPI_Percen")
            TmpDr("HT_With_KPI_Percen") = dr("HT_With_KPI_Percen")
            TmpDr("HT_Over_KPI_Percen") = dr("HT_Over_KPI_Percen")
            TmpDr("Misscall_Percen") = dr("Misscall_Percen")
            If Convert.IsDBNull(dr("AVG_WT")) = False Then
                TmpDr("AVG_WT") = Engine.Reports.ReportsENG.GetFormatTimeFromSec(dr("AVG_WT"))
            End If
            TmpDr("AVG_HT") = dr("AVG_HT")
            TmpDr("MAX_WT") = dr("MAX_WT")
            TmpDr("MAX_HT") = dr("MAX_HT")
            TmpDr("master_itemid") = dr("master_itemid")
            TmpDr("SUM_WT") = dr("SUM_WT")
            TmpDr("SUM_HT") = dr("SUM_HT")
            TmpDt.Rows.Add(TmpDr)
        Next

        Return TmpDt
    End Function

    Private Function LoadSqlFromTextFile() As String
        Dim ret As String = ""
        Dim myReader As StreamReader
        Dim SqlScript As String = System.Windows.Forms.Application.StartupPath & "\SqlScript.txt"
        myReader = New StreamReader(SqlScript, System.Text.UnicodeEncoding.Default)
        While myReader.Peek <> -1
            ret = myReader.ReadToEnd
        End While

        myReader.Close()
        myReader = Nothing

        Return ret
    End Function

    Private Function SendSqlEmail(ByVal MailAttFile As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim sql As String = ""
            sql = " declare @p_subject as varchar(300); set @p_subject='" & txtSubject.Text.Trim & "'"
            sql += " declare @p_recipients as varchar(300);set @p_recipients='" & txtTo.Text.Trim & "'"
            sql += " declare @p_cc as varchar(300);set @p_cc='" & txtCC.Text.Trim & "'"
            sql += " declare @MailAttFile varchar(255); set @MailAttFile = '" & MailAttFile & "';"
            sql += " EXEC msdb.dbo.sp_send_dbmail @profile_name='QIS System',"
            sql += " @recipients=@p_recipients,@copy_recipients=@p_cc,"
            sql += " @subject=@p_subject,@body='" & txtMailContent.Text.Trim & "',@file_attachments = @MailAttFile"

            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql)
            ret = True
        Catch ex As Exception
            ret = False
        End Try
        
        Return ret
    End Function

    Private Function ValidateEmail() As Boolean
        Dim ret As Boolean = True
        If txtSubject.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Subject")
            ret = False
        ElseIf txtTo.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Mail To")
            ret = False
        ElseIf txtMailContent.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Mail Content")
            ret = False
        End If

        Return ret
    End Function

    Private Sub ProcReports()
        Button1.Enabled = False
        If chkSendEmail.Checked = True Then
            If ValidateEmail() = False Then
                Button1.Enabled = True
                Exit Sub
            End If
        End If

        Dim DateFrom As String = dtDateFrom.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        Dim DateTo As String = dtDateTo.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        If rdiDaily.Checked = True Then
            GetDataAllShop("AIS_Report_Daily_", DateFrom, DateTo)

            'Zip File
            Dim ZipFileName As String = "D:\ReportExcel\AIS_Report_Daily_" & DateTo & ".zip"
            If File.Exists(ZipFileName) = True Then
                File.Delete(ZipFileName)
            End If

            Dim CurrDate As Date = dtDateFrom.Value
            Do
                Dim DateStr As String = CurrDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                If File.Exists("D:\ReportExcel\AIS_Report_Daily_" & DateStr & ".xlsx") = True Then
                    AddFileToZip("D:\ReportExcel\AIS_Report_Daily_" & DateStr & ".xlsx", ZipFileName)
                End If

                Dim FileName As New ArrayList
                'Open the zip file
                Dim zip As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                For Each pkgPart As PackagePart In zip.GetParts()
                    FileName.Add(pkgPart.Uri.ToString())
                Next
                zip.Close()

                'Open the zip file
                Dim zip2 As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                For Each Str As String In FileName
                    If IO.Path.GetExtension(Str) = ".psmdcp" OrElse Str.IndexOf("_rels") > -1 Then
                        Dim partUri As New Uri(Str, System.UriKind.Relative)
                        zip2.DeletePart(partUri)
                    End If
                Next
                zip2.Close()

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate <= dtDateTo.Value
            If chkSendEmail.Checked = True Then
                SendSqlEmail(ZipFileName)
            End If
        ElseIf rdiAccumulate.Checked = True Then
            GetDataAccumAllShop("AIS_Report_Accumulate_", DateFrom, DateTo)

            'Zip File
            Dim DateStr As String = dtDateTo.Value.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim ZipFileName As String = "D:\ReportExcel\AIS_Report_Accumulate_" & DateStr & ".zip"
            If File.Exists(ZipFileName) = True Then
                File.Delete(ZipFileName)
            End If

            Dim dFrom As String = dtDateFrom.Value.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim dTo As String = dtDateTo.Value.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            AddFileToZip("D:\ReportExcel\AIS_Report_Accumulate_" & dFrom & " To " & dTo & ".xlsx", ZipFileName)
            Dim FileName As New ArrayList
            'Open the zip file
            Dim zip As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            For Each pkgPart As PackagePart In zip.GetParts()
                FileName.Add(pkgPart.Uri.ToString())
            Next
            zip.Close()

            'Open the zip file
            Dim zip2 As Package = ZipPackage.Open(ZipFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            For Each Str As String In FileName
                If IO.Path.GetExtension(Str) = ".psmdcp" OrElse Str.IndexOf("_rels") > -1 Then
                    Dim partUri As New Uri(Str, System.UriKind.Relative)
                    zip2.DeletePart(partUri)
                End If
            Next
            zip2.Close()

            If chkSendEmail.Checked = True Then
                SendSqlEmail(ZipFileName)
            End If
        End If

        Button1.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ProcReports()

        MessageBox.Show("Success!!!!")

        Dim ini As New IniReader(IniFileName)
        ini.Section = "Mail Setting"
        ini.Write("MailSubject", txtSubject.Text.Trim)
        ini.Write("MailTo", txtTo.Text.Trim)
        ini.Write("MailCC", txtCC.Text.Trim)
        ini.Write("MailContent", txtMailContent.Text.Trim)
        ini = Nothing
    End Sub

    Private Sub frmProcessReportsExcel_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim ini As New IniReader(IniFileName)
        ini.Section = "Mail Setting"
        txtSubject.Text = ini.ReadString("MailSubject")
        txtTo.Text = ini.ReadString("MailTo")
        txtCC.Text = ini.ReadString("MailCC")
        txtMailContent.Text = ini.ReadString("MailContent")
        ini = Nothing

        If My.Application.CommandLineArgs.Count > 0 Then
            Dim ReportDate As DateTime = DateAdd(DateInterval.Day, -1, DateTime.Now)
            chkSendEmail.Checked = True
            dtDateFrom.Value = New Date(ReportDate.Year, ReportDate.Month, 1)
            dtDateTo.Value = ReportDate

            txtSubject.Text = "AIS-Q>Daily Report " & dtDateTo.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            'txtTo.Text = "akkarawat@scoresolutions.co.th;nattapol@scoresolutions.co.th"
            'txtCC.Text = "machadit@scoresolutions.co.th;akkarawat@scoresolutions.co.th"
            txtTo.Text = "boonyakn@ais.co.th;wipawees@ais.co.th;nattapol@scoresolutions.co.th;machadit@scoresolutions.co.th;suchart@scoresolutions.co.th"

            txtMailContent.Text = "Dear All" & vbNewLine & vbNewLine
            txtMailContent.Text += " QIS Summary Daily Report." & vbNewLine & vbNewLine & vbNewLine
            txtMailContent.Text += "Best Regards," & vbNewLine
            txtMailContent.Text += "Shop Technical Support" & vbNewLine
            txtMailContent.Text += "[Auto Mail]"

            rdiDaily.Checked = True
            ProcReports()
            rdiAccumulate.Checked = True
            ProcReports()

            Application.Exit()
        End If
    End Sub
End Class
