Imports System.Data
Imports System.Globalization
Imports System.IO
Imports OfficeOpenXml
Imports System.Drawing

Partial Class CSIWebApp_frmReportNPS_SCORE_ByAgent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetCombo()
        End If
        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(Me.btnExportExcel)
    End Sub

    Sub ClearForm()
        txtDateFrom.TxtBox.Text = ""
        txtDateTo.TxtBox.Text = ""
        cmbServiceID.SelectedValue = "0"
        cmbShopID.SelectedValue = "0"
        cmbShopUserID.SelectedValue = ""
        cmbNetworkType.SelectedValue = ""
        ctlSelectFilterTemplate1.ClearSelectFilterID()
    End Sub


    Private Sub SetCombo()
        cmbShopID.SetItemList(Engine.Common.FunctionEng.GetActiveShopDDL, "shop_name", "id")

        Dim sEng As New Engine.Configuration.MasterENG
        cmbServiceID.SetItemList(sEng.GetServiceActiveList("1=1"), "item_name", "id")
        sEng = Nothing

        cmbNetworkType.SetItemList("All", "")
        cmbNetworkType.SetItemList("2G", "2G")
        cmbNetworkType.SetItemList("3G", "3G")
    End Sub

    Function Validate() As Boolean
        lblMessage.Text = ""
        If txtDateFrom.DateValue.Year = "1" Then
            lblMessage.Text = "กรุณาเลือก Date from !!!"
            Return False
        End If
        If txtDateTo.DateValue.Year = "1" Then
            lblMessage.Text = "กรุณาเลือก Date to !!!"
            Return False
        End If

        If txtDateFrom.DateValue > txtDateTo.DateValue Then
            lblMessage.Text = "คุณเลือก Date from มากกว่า Date to !!!"
            Return False
        End If
        Return True
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
      
        If Validate() = False Then
            Exit Sub
        End If

        Dim eng As New Engine.CSI.CSIReportsENG
        Dim dt As New DataTable
        dt = eng.GetNPSSCOREByAgentDataList(GetWhText)
        If dt.Rows.Count > 5000 Then
            lblMessage.Text = "ไม่สามารถแสดงข้อมูลได้ ข้อมูลมีจำนวนเกิน 5000 เรคคอร์ด " & vbCrLf & "กรุณากดปุ่ม Export เพื่อตรวจสอบข้อมูล"
            Exit Sub
        End If

        Dim para As String = ""
        para += "?ReportName=NPSSCORE_BY_AGENT"
        para += "&rnd=" & DateTime.Now.Millisecond

        If txtDateFrom.DateValue.Year <> 1 Then
            para += "&DateFrom=" & txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
        If txtDateTo.DateValue.Year <> 1 Then
            para += "&DateTo=" & txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
        If cmbShopID.SelectedValue <> "0" Then
            para += "&ShopID=" & cmbShopID.SelectedValue
            If cmbShopUserID.SelectedValue <> "" Then
                para += "&ShopUserName=" & cmbShopUserID.SelectedValue
                para += "&ShopUserNameFullName=" & cmbShopUserID.SelectedText
            End If
        End If
        If cmbServiceID.SelectedValue <> "0" Then
            para += "&ServiceID=" & cmbServiceID.SelectedValue
        End If

        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            para += "&TemplateID=" & SelectTemplate
        End If

        para += "&Status=2" 'Complete

        If cmbNetworkType.SelectedValue <> "" Then
            para += "&NetworkType=" & cmbNetworkType.SelectedValue
        End If

        Dim scr As String = "window.open('../CSIWebApp/frmReportPreview.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)
    End Sub

    Protected Sub cmbShopID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShopID.SelectedIndexChanged
        If cmbShopID.SelectedValue <> "0" Then
            Dim eng As New Engine.Configuration.ShopUserENG
            Dim dt As New DataTable
            dt = eng.GetShopAllUserNotAdmin(cmbShopID.SelectedValue)
            cmbShopUserID.SetItemList(dt, "fullname", "username")
            dt.Dispose()
            eng = Nothing
        Else
            cmbShopUserID.ClearCombo()
            cmbShopUserID.SetItemList("Select All", "")
        End If
    End Sub


    Protected Sub ctlSelectFilterTemplate1_Search(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlSelectFilterTemplate1.Search
        If Validate() = False Then
            Exit Sub
        End If

        If txtDateFrom.DateValue.Year <> 1 Then
            ctlSelectFilterTemplate1.DateFrom = txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
        If txtDateTo.DateValue.Year <> 1 Then
            ctlSelectFilterTemplate1.DateTo = txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        End If
    End Sub

    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        SearchDetailExcel()
    End Sub

    Private Function GetWhText() As String
        Dim whText As String = "1=1"
        If txtDateFrom.DateValue.Year <> 1 Then
            whText += " and convert(varchar(8),fd.send_time,112)>='" & txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        End If
        If txtDateTo.DateValue.Year <> 1 Then
            whText += " and convert(varchar(8),fd.send_time,112)<='" & txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        End If
        If cmbShopID.SelectedValue <> "0" Then
            whText += " and s.id='" & cmbShopID.SelectedValue & "'"

            If cmbShopUserID.SelectedValue <> "" Then
                whText += " and fd.username = '" & cmbShopUserID.SelectedValue & "'"
            End If
        End If

        If cmbServiceID.SelectedValue <> "0" Then
            whText += " and fd.tb_item_id = '" & cmbServiceID.SelectedValue & "'"
        End If

        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            whText += " and fd.tb_filter_id in (" & SelectTemplate & ")"
        End If
       
        whText += " and fd.result_status in (2)"
        If cmbNetworkType.SelectedValue <> "" Then
            whText += " and fd.network_type = '" & cmbNetworkType.SelectedValue & "'"
        End If
        Return whText
    End Function

    Sub SetHeadExcelStyle(ByVal ExcelRowHeader As ExcelRange, ByVal ws As ExcelWorksheet)
        Using RowHeader As ExcelRange = ExcelRowHeader
            RowHeader.Style.Font.Bold = True
            RowHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            RowHeader.Style.Fill.BackgroundColor.SetColor(Color.Yellow)
            RowHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            RowHeader.Style.Font.Color.SetColor(Color.Black)
        End Using
    End Sub

    Sub SetRowStyle(ByVal ExcelRowHeader As ExcelRange, ByVal ws As ExcelWorksheet, ByVal RowColor As Color)
        Using RowHeader As ExcelRange = ExcelRowHeader
            RowHeader.Style.Font.Bold = True
            RowHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            RowHeader.Style.Fill.BackgroundColor.SetColor(RowColor)
            RowHeader.Style.Font.Color.SetColor(Color.Black)
        End Using
    End Sub

    Private Sub SearchDetailExcel()
        Try
            If Validate() = False Then
                Exit Sub
            End If

            Dim eng As New Engine.CSI.CSIReportsENG
            Dim dt As New DataTable
            dt = eng.GetNPSSCOREByAgentDataList(GetWhText)
            Dim countCriteria As Integer = 1

            If dt.Rows.Count > 0 Then
                'Credit EPPlus
                'http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html

                lblMessage.Text = ""
                Using ep As New ExcelPackage

                    '## Start Criteria ##
                    Dim ws As ExcelWorksheet = ep.Workbook.Worksheets.Add("NPSScoreByAgentReport")
                    ws.Cells("A" & countCriteria & "").Value = "Date Between : "
                    ws.Cells("B" & countCriteria & "").Value = txtDateFrom.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & txtDateTo.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                    countCriteria += 1

                    If cmbShopID.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Location : "
                        ws.Cells("B" & countCriteria & "").Value = cmbShopID.SelectedText
                        countCriteria += 1
                    End If

                    If cmbServiceID.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Service : "
                        ws.Cells("B" & countCriteria & "").Value = cmbServiceID.SelectedText
                        countCriteria += 1
                    End If

                    If cmbShopUserID.SelectedValue <> "" Then
                        ws.Cells("A" & countCriteria & "").Value = "Agent : "
                        ws.Cells("B" & countCriteria & "").Value = cmbShopUserID.SelectedText
                        countCriteria += 1
                    End If

                    ws.Cells("A" & countCriteria & "").Value = "Status : "
                    ws.Cells("B" & countCriteria & "").Value = lblStatus.Text
                    countCriteria += 1

                    If cmbNetworkType.SelectedValue <> "" Then
                        ws.Cells("A" & countCriteria & "").Value = "Network Type : "
                        ws.Cells("B" & countCriteria & "").Value = cmbNetworkType.SelectedValue
                        countCriteria += 1
                    End If

                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Try
                        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
                        If SelectTemplate <> "" Then
                            ws.Cells("A" & countCriteria & "").Value = "Template :"
                            Dim f As Integer = 0
                            For Each fID As String In SelectTemplate.Split(",")
                                Dim fPara As New CenParaDB.TABLE.TbFilterCenParaDB
                                Dim fEng As New Engine.CSI.FilterTemplateENG
                                fPara = fEng.GetFilterTemplatePara(Convert.ToInt64(fID), trans)

                                ws.Cells("B" & countCriteria & "").Value = fPara.FILTER_NAME
                                countCriteria += 1
                            Next
                        End If
                        trans.CommitTransaction()
                    Catch ex As Exception
                        trans.RollbackTransaction()
                    End Try

                    For cnt As Integer = 1 To countCriteria - 1
                        ws.Cells(cnt, 2, cnt, 3).Merge = True
                        ws.Cells("A" & cnt).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                    Next
                    countCriteria += 1
                    '## End Criteria ##


                    '## Start Header ##
                    ws.Cells("A" & countCriteria & "").Value = "Location"
                    ws.Cells("B" & countCriteria & "").Value = "AIS Shop"
                    ws.Cells("C" & countCriteria & "").Value = "Username"
                    ws.Cells("D" & countCriteria & "").Value = "Staff Name"

                    For cnt As Integer = 1 To 4
                        ws.Cells(countCriteria, cnt, countCriteria + 1, cnt).Merge = True
                    Next

                    ws.Cells(countCriteria, 5, countCriteria, 26).Merge = True
                    ws.Cells("E" & countCriteria).Value = "NPS Score"
                    countCriteria += 1

                    ws.Cells("E" & countCriteria & "").Value = "0"
                    ws.Cells("F" & countCriteria & "").Value = "1"
                    ws.Cells("G" & countCriteria & "").Value = "2"
                    ws.Cells("H" & countCriteria & "").Value = "3"
                    ws.Cells("I" & countCriteria & "").Value = "4"
                    ws.Cells("J" & countCriteria & "").Value = "5"
                    ws.Cells("K" & countCriteria & "").Value = "6"
                    ws.Cells("L" & countCriteria & "").Value = "0-6"
                    ws.Cells("M" & countCriteria & "").Value = "%"
                    ws.Cells("N" & countCriteria & "").Value = "%0-6"
                    ws.Cells("O" & countCriteria & "").Value = "7"
                    ws.Cells("P" & countCriteria & "").Value = "8"
                    ws.Cells("Q" & countCriteria & "").Value = "7-8"
                    ws.Cells("R" & countCriteria & "").Value = "%"
                    ws.Cells("Q" & countCriteria & "").Value = "%7-8"
                    ws.Cells("T" & countCriteria & "").Value = "9"
                    ws.Cells("U" & countCriteria & "").Value = "10"
                    ws.Cells("V" & countCriteria & "").Value = "9-10"
                    ws.Cells("W" & countCriteria & "").Value = "%"
                    ws.Cells("X" & countCriteria & "").Value = "%9-10"
                    ws.Cells("Y" & countCriteria & "").Value = "Grand Total"
                    ws.Cells("Z" & countCriteria & "").Value = "NPS"

                    SetHeadExcelStyle(ws.Cells("A" & countCriteria - 1 & ":Z" & countCriteria - 1 & ""), ws)
                    SetHeadExcelStyle(ws.Cells("A" & countCriteria & ":Z" & countCriteria & ""), ws)
                    '## End Header ##


                    '## Start Detail ##
                    Dim _all_sum0 As Integer = 0
                    Dim _all_sum1 As Integer = 0
                    Dim _all_sum2 As Integer = 0
                    Dim _all_sum3 As Integer = 0
                    Dim _all_sum4 As Integer = 0
                    Dim _all_sum5 As Integer = 0
                    Dim _all_sum6 As Integer = 0
                    Dim _all_sum7 As Integer = 0
                    Dim _all_sum8 As Integer = 0
                    Dim _all_sum9 As Integer = 0
                    Dim _all_sum10 As Integer = 0
                    Dim cntDetail As Integer = 0
                    Dim ExcelRow As Long = cntDetail + countCriteria + 1

                    Dim rDt As New DataTable
                    dt.DefaultView.Sort = "region_name_en,shop_name_en,username"
                    rDt = dt.DefaultView.ToTable(True, "region_id", "region_code", "region_name_en").Copy
                    If rDt.Rows.Count > 0 Then
                        For Each rDr As DataRow In rDt.Rows
                            If rDr("region_id") Is DBNull.Value Then
                                Continue For
                            End If
                            dt.DefaultView.RowFilter = "region_id='" & rDr("region_id") & "'"

                            Dim _sum0 As Integer = 0
                            Dim _sum1 As Integer = 0
                            Dim _sum2 As Integer = 0
                            Dim _sum3 As Integer = 0
                            Dim _sum4 As Integer = 0
                            Dim _sum5 As Integer = 0
                            Dim _sum6 As Integer = 0
                            Dim _sum7 As Integer = 0
                            Dim _sum8 As Integer = 0
                            Dim _sum9 As Integer = 0
                            Dim _sum10 As Integer = 0


                            If dt.DefaultView.Count > 0 Then
                                For Each dr As DataRowView In dt.DefaultView
                                    Dim grandTo As Integer = CInt(dr("GrandTo"))
                                    Dim sum0_6 As Integer = CInt(dr("score_0")) + CInt(dr("score_1")) + _
                                    CInt(dr("score_2")) + CInt(dr("score_3")) + CInt(dr("score_4")) + _
                                    CInt(dr("score_5")) + CInt(dr("score_6"))
                                    Dim percent0_6 As Double = Math.Round((sum0_6 / grandTo) * 100, 2)
                                    Dim round0_6 As Integer = Math.Round(percent0_6)

                                    Dim sum7_8 As Integer = CInt(dr("score_7")) + CInt(dr("score_8"))
                                    Dim percent7_8 As Double = Math.Round((sum7_8 / grandTo) * 100, 2)
                                    Dim round7_8 As Integer = Math.Round(percent7_8)

                                    Dim sum9_10 As Integer = CInt(dr("score_9")) + CInt(dr("score_10"))
                                    Dim percent9_10 As Double = Math.Round((sum9_10 / grandTo) * 100, 2)
                                    Dim round9_10 As Integer = Math.Round(percent9_10)
                                    Dim NPS As Integer = round9_10 - round0_6

                                    _sum0 += CInt(dr("score_0"))
                                    _sum1 += CInt(dr("score_1"))
                                    _sum2 += CInt(dr("score_2"))
                                    _sum3 += CInt(dr("score_3"))
                                    _sum4 += CInt(dr("score_4"))
                                    _sum5 += CInt(dr("score_5"))
                                    _sum6 += CInt(dr("score_6"))
                                    _sum7 += CInt(dr("score_7"))
                                    _sum8 += CInt(dr("score_8"))
                                    _sum9 += CInt(dr("score_9"))
                                    _sum10 += CInt(dr("score_10"))

                                    ExcelRow = cntDetail + countCriteria + 1
                                    ws.Cells("A" & ExcelRow & "").Value = dr("shop_code").ToString
                                    ws.Cells("B" & ExcelRow & "").Value = dr("shop_name_en").ToString
                                    ws.Cells("C" & ExcelRow & "").Value = dr("username").ToString
                                    ws.Cells("D" & ExcelRow & "").Value = dr("staff_name").ToString
                                    ws.Cells("E" & ExcelRow & "").Value = dr("score_0").ToString
                                    ws.Cells("F" & ExcelRow & "").Value = dr("score_1").ToString
                                    ws.Cells("G" & ExcelRow & "").Value = dr("score_2").ToString
                                    ws.Cells("H" & ExcelRow & "").Value = dr("score_3").ToString
                                    ws.Cells("I" & ExcelRow & "").Value = dr("score_4").ToString
                                    ws.Cells("J" & ExcelRow & "").Value = dr("score_5").ToString
                                    ws.Cells("K" & ExcelRow & "").Value = dr("score_6").ToString
                                    ws.Cells("L" & ExcelRow & "").Value = sum0_6
                                    ws.Cells("M" & ExcelRow & "").Value = percent0_6.ToString("0.00")
                                    ws.Cells("N" & ExcelRow & "").Value = round0_6
                                    ws.Cells("O" & ExcelRow & "").Value = dr("score_7").ToString
                                    ws.Cells("P" & ExcelRow & "").Value = dr("score_8").ToString
                                    ws.Cells("Q" & ExcelRow & "").Value = sum7_8
                                    ws.Cells("R" & ExcelRow & "").Value = percent7_8.ToString("0.00")
                                    ws.Cells("S" & ExcelRow & "").Value = round7_8
                                    ws.Cells("T" & ExcelRow & "").Value = dr("score_9").ToString
                                    ws.Cells("U" & ExcelRow & "").Value = dr("score_10").ToString
                                    ws.Cells("V" & ExcelRow & "").Value = sum9_10
                                    ws.Cells("W" & ExcelRow & "").Value = percent9_10.ToString("0.00")
                                    ws.Cells("X" & ExcelRow & "").Value = round9_10
                                    ws.Cells("Y" & ExcelRow & "").Value = grandTo
                                    ws.Cells("Z" & ExcelRow & "").Value = NPS
                                    ws.Cells("A" & ExcelRow & ":D" & ExcelRow & "").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                                    ws.Cells("E" & ExcelRow & ":Z" & ExcelRow & "").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                                    cntDetail += 1
                                Next
                            End If
                            dt.DefaultView.RowFilter = ""


                            Dim _grandTo As Integer = _sum0 + _sum1 + _sum2 + _sum3 + _sum4 + _sum5 + _sum6 + _sum7 + _sum8 + _sum9 + _sum10
                            Dim _sum0_6 As Integer = _sum0 + _sum1 + _sum2 + _sum3 + _sum4 + _sum5 + _sum6
                            Dim _sum7_8 As Integer = _sum7 + _sum8
                            Dim _sum9_10 As Integer = _sum9 + _sum10
                            Dim _percent0_6 As Double = Math.Round((_sum0_6 / _grandTo) * 100, 2)
                            Dim _percent7_8 As Double = Math.Round((_sum7_8 / _grandTo) * 100, 2)
                            Dim _percent9_10 As Double = Math.Round((_sum9_10 / _grandTo) * 100, 2)
                            Dim _round0_6 As Integer = Math.Round(_percent0_6)
                            Dim _round7_8 As Integer = Math.Round(_percent7_8)
                            Dim _round9_10 As Integer = Math.Round(_percent9_10)
                            Dim _nps As Integer = _round9_10 - _round0_6

                            _all_sum0 += _sum0
                            _all_sum1 += _sum1
                            _all_sum2 += _sum2
                            _all_sum3 += _sum3
                            _all_sum4 += _sum4
                            _all_sum5 += _sum5
                            _all_sum6 += _sum6
                            _all_sum7 += _sum7
                            _all_sum8 += _sum8
                            _all_sum9 += _sum9
                            _all_sum10 += _sum10

                            ExcelRow = cntDetail + countCriteria + 1
                            ws.Cells(ExcelRow, 1, ExcelRow, 4).Merge = True 'A-D
                            ws.Cells("A" & ExcelRow & "").Value = rDr("region_code")

                            ws.Cells("E" & ExcelRow & "").Value = _sum0
                            ws.Cells("F" & ExcelRow & "").Value = _sum1
                            ws.Cells("G" & ExcelRow & "").Value = _sum2
                            ws.Cells("H" & ExcelRow & "").Value = _sum3
                            ws.Cells("I" & ExcelRow & "").Value = _sum4
                            ws.Cells("J" & ExcelRow & "").Value = _sum5
                            ws.Cells("K" & ExcelRow & "").Value = _sum6
                            ws.Cells("L" & ExcelRow & "").Value = _sum0_6
                            ws.Cells("M" & ExcelRow & "").Value = _percent0_6.ToString("0.00")
                            ws.Cells("N" & ExcelRow & "").Value = _round0_6
                            ws.Cells("O" & ExcelRow & "").Value = _sum7
                            ws.Cells("P" & ExcelRow & "").Value = _sum8
                            ws.Cells("Q" & ExcelRow & "").Value = _sum7_8
                            ws.Cells("R" & ExcelRow & "").Value = _percent7_8.ToString("0.00")
                            ws.Cells("S" & ExcelRow & "").Value = _round7_8
                            ws.Cells("T" & ExcelRow & "").Value = _sum9
                            ws.Cells("U" & ExcelRow & "").Value = _sum10
                            ws.Cells("V" & ExcelRow & "").Value = _sum9_10
                            ws.Cells("W" & ExcelRow & "").Value = _percent9_10.ToString("0.00")
                            ws.Cells("X" & ExcelRow & "").Value = _round9_10
                            ws.Cells("Y" & ExcelRow & "").Value = _grandTo
                            ws.Cells("Z" & ExcelRow & "").Value = _nps

                            SetRowStyle(ws.Cells("A" & ExcelRow & ":Z" & ExcelRow & ""), ws, Color.YellowGreen)
                            ws.Cells("A" & ExcelRow & ":D" & ExcelRow & "").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            ws.Cells("E" & ExcelRow & ":Z" & ExcelRow & "").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right

                            cntDetail += 1
                        Next
                    End If


                    Dim _all_grandTo As Integer = _all_sum0 + _all_sum1 + _all_sum2 + _all_sum3 + _all_sum4 + _
                    _all_sum5 + _all_sum6 + _all_sum7 + _all_sum8 + _all_sum9 + _all_sum10
                    Dim _all_sum0_6 As Integer = _all_sum0 + _all_sum1 + _all_sum2 + _all_sum3 + _all_sum4 + _all_sum5 + _all_sum6
                    Dim _all_sum7_8 As Integer = _all_sum7 + _all_sum8
                    Dim _all_sum9_10 As Integer = _all_sum9 + _all_sum10
                    Dim _all_percent0_6 As Double = Math.Round((_all_sum0_6 / _all_grandTo) * 100, 2)
                    Dim _all_percent7_8 As Double = Math.Round((_all_sum7_8 / _all_grandTo) * 100, 2)
                    Dim _all_percent9_10 As Double = Math.Round((_all_sum9_10 / _all_grandTo) * 100, 2)
                    Dim _all_round0_6 As Integer = Math.Round(_all_percent0_6)
                    Dim _all_round7_8 As Integer = Math.Round(_all_percent7_8)
                    Dim _all_round9_10 As Integer = Math.Round(_all_percent9_10)
                    Dim _all_nps As Integer = _all_round9_10 - _all_round0_6

                    ExcelRow = cntDetail + countCriteria + 1
                    ws.Cells(ExcelRow, 1, ExcelRow, 4).Merge = True 'A-D
                    ws.Cells("A" & ExcelRow & "").Value = "Overall Retail Shop"

                    ws.Cells("E" & ExcelRow & "").Value = _all_sum0
                    ws.Cells("F" & ExcelRow & "").Value = _all_sum1
                    ws.Cells("G" & ExcelRow & "").Value = _all_sum2
                    ws.Cells("H" & ExcelRow & "").Value = _all_sum3
                    ws.Cells("I" & ExcelRow & "").Value = _all_sum4
                    ws.Cells("J" & ExcelRow & "").Value = _all_sum5
                    ws.Cells("K" & ExcelRow & "").Value = _all_sum6
                    ws.Cells("L" & ExcelRow & "").Value = _all_sum0_6
                    ws.Cells("M" & ExcelRow & "").Value = _all_percent0_6.ToString("0.00")
                    ws.Cells("N" & ExcelRow & "").Value = _all_round0_6
                    ws.Cells("O" & ExcelRow & "").Value = _all_sum7
                    ws.Cells("P" & ExcelRow & "").Value = _all_sum8
                    ws.Cells("Q" & ExcelRow & "").Value = _all_sum7_8
                    ws.Cells("R" & ExcelRow & "").Value = _all_percent7_8.ToString("0.00")
                    ws.Cells("S" & ExcelRow & "").Value = _all_round7_8
                    ws.Cells("T" & ExcelRow & "").Value = _all_sum9
                    ws.Cells("U" & ExcelRow & "").Value = _all_sum10
                    ws.Cells("V" & ExcelRow & "").Value = _all_sum9_10
                    ws.Cells("W" & ExcelRow & "").Value = _all_percent9_10.ToString("0.00")
                    ws.Cells("X" & ExcelRow & "").Value = _all_round9_10
                    ws.Cells("Y" & ExcelRow & "").Value = _all_grandTo
                    ws.Cells("Z" & ExcelRow & "").Value = _all_nps
                    SetRowStyle(ws.Cells("A" & ExcelRow & ":Z" & ExcelRow & ""), ws, Color.BlueViolet)
                    ws.Cells("A" & ExcelRow & "").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    ws.Cells("E" & ExcelRow & ":Z" & ExcelRow & "").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right

                    rDt.Dispose()

                    Using RowContent As ExcelRange = ws.Cells("A" & countCriteria - 1 & ":Z" & cntDetail + countCriteria + 1)
                        RowContent.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        RowContent.AutoFitColumns()
                    End Using

                    '//Write it back to the client
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;  filename=NPS_Score_By_Agent_Report_" & DateTime.Now.ToString("yyyyMMddhhmmssfff", New Globalization.CultureInfo("en-US")) & ".xlsx")
                    Response.BinaryWrite(ep.GetAsByteArray())
                    Response.End()
                    Response.Flush()

                End Using
            Else
                lblMessage.Text = "Not Found"
                Exit Sub
            End If
            dt.Dispose()
        Catch ex As Exception
            lblMessage.Text = ex.ToString()
        End Try
    End Sub

    Protected Sub btnSearch0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch0.Click
        ClearForm()
    End Sub
End Class
