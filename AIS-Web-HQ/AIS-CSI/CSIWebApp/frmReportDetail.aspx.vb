Imports System.Data
Imports System.Globalization
Imports System.IO
Imports OfficeOpenXml
Imports System.Drawing
Imports Engine.Common

Partial Class CSIWebApp_frmReportDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetCombo()
        End If

        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(Me.btnExportExcel)
    End Sub

    Private Sub SetCombo()
        cmbShopID.SetItemList(Engine.Common.FunctionEng.GetActiveShopDDL, "shop_name", "id")

        Dim sEng As New Engine.Configuration.MasterENG
        cmbServiceID.SetItemList(sEng.GetServiceActiveList("1=1"), "item_name", "id")
        sEng = Nothing

        cmbStatus.SetItemList("All", "0")
        cmbStatus.SetItemList("Complete", "2")
        cmbStatus.SetItemList("Incomplete", "1,3")

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

    Sub ClearForm()
        txtDateFrom.TxtBox.Text = ""
        txtDateTo.TxtBox.Text = ""
        cmbServiceID.SelectedValue = "0"
        cmbShopID.SelectedValue = "0"
        cmbShopUserID.SelectedValue = ""
        cmbStatus.SelectedValue = "0"
        cmbNetworkType.SelectedValue = ""
        ctlSelectFilterTemplate1.ClearSelectFilterID()
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
        If cmbStatus.SelectedValue <> "0" Then
            whText += " and fd.result_status = '" & cmbStatus.SelectedValue & "'"
        End If

        If cmbNetworkType.SelectedValue <> "" Then
            whText += " and fd.network_type = '" & cmbNetworkType.SelectedValue & "'"
        End If
        Return whText
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Validate() = False Then
            Exit Sub
        End If

        Dim eng As New Engine.CSI.CSIReportsENG
        Dim dt As New DataTable
        dt = eng.GetDetailDataList(GetWhText)
        If dt.Rows.Count > 5000 Then
            lblMessage.Text = "ไม่สามารถแสดงข้อมูลได้ ข้อมูลมีจำนวนเกิน 5000 เรคคอร์ด " & vbCrLf & "กรุณากดปุ่ม Export เพื่อตรวจสอบข้อมูล"
            Exit Sub
        End If

        Dim para As String = ""
        para += "?ReportName=Detail"
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
            End If
        End If
        If cmbServiceID.SelectedValue <> "0" Then
            para += "&ServiceID=" & cmbServiceID.SelectedValue
        End If

        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            para += "&TemplateID=" & SelectTemplate
        End If
        If cmbStatus.SelectedValue <> "0" Then
            para += "&Status=" & cmbStatus.SelectedValue
        End If
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

    Private Sub SearchDetailExcel()
        Try
            If Validate() = False Then
                Exit Sub
            End If

            Dim eng As New Engine.CSI.CSIReportsENG
            Dim dt As New DataTable
            dt = eng.GetDetailDataList(GetWhText)
            Dim countCriteria As Integer = 1

            If dt.Rows.Count > 0 Then
                'Credit EPPlus
                'http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html

                lblMessage.Text = ""
                Using ep As New ExcelPackage

                    '## Start Criterai ##
                    Dim ws As ExcelWorksheet = ep.Workbook.Worksheets.Add("DetailReport")
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
                    ws.Cells("B" & countCriteria & "").Value = cmbStatus.SelectedText
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
                    '## End Criterai ##

                    '## Start Header ##
                    ws.Cells("A" & countCriteria & "").Value = "Date"
                    ws.Cells("B" & countCriteria & "").Value = "End Service Time"
                    ws.Cells("C" & countCriteria & "").Value = "Sent Time"
                    ws.Cells("D" & countCriteria & "").Value = "ATSR Call Time"
                    ws.Cells("E" & countCriteria & "").Value = "Mobile No"
                    ws.Cells("F" & countCriteria & "").Value = "Result"
                    ws.Cells("G" & countCriteria & "").Value = "Location Code"
                    ws.Cells("H" & countCriteria & "").Value = "Location Name"
                    ws.Cells("I" & countCriteria & "").Value = "Name"
                    ws.Cells("J" & countCriteria & "").Value = "Service Type"
                    ws.Cells("K" & countCriteria & "").Value = "Username"
                    ws.Cells("L" & countCriteria & "").Value = "Staff Name"
                    ws.Cells("M" & countCriteria & "").Value = "Template"
                    ws.Cells("N" & countCriteria & "").Value = "NPS Score"
                    ws.Cells("O" & countCriteria & "").Value = "Network Type"

                    ' //Format the header 
                    Using RowHeader As ExcelRange = ws.Cells("A" & countCriteria & ":O" & countCriteria & "")
                        RowHeader.Style.Font.Bold = True
                        RowHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        RowHeader.Style.Fill.BackgroundColor.SetColor(Color.YellowGreen)
                        RowHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        RowHeader.Style.Font.Color.SetColor(Color.White)
                    End Using
                    '## End Header ##


                    '## Start Detail ##
                    Dim i As Long = 0
                    For i = 0 To dt.Rows.Count - 1
                        Dim dr As DataRow = dt.Rows(i)
                        Dim ExcelRow As Long = i + countCriteria + 1

                        If Convert.IsDBNull(dr("send_time")) = False Then
                            ws.Cells("A" & ExcelRow).Value = Convert.ToDateTime(dr("send_time")).ToString("dd/MM/yyyy", New CultureInfo("th-TH"))
                            ws.Cells("C" & ExcelRow).Value = Convert.ToDateTime(dr("send_time")).ToString("HH:mm:ss", New CultureInfo("th-TH"))
                        End If
                        If Convert.IsDBNull(dr("end_time")) = False Then
                            ws.Cells("B" & ExcelRow).Value = Convert.ToDateTime(dr("end_time")).ToString("HH:mm:ss")
                        End If
                        If Convert.IsDBNull(dr("atsr_call_time")) = False Then
                            ws.Cells("D" & ExcelRow).Value = Convert.ToDateTime(dr("atsr_call_time")).ToString("HH:mm:ss")
                        End If


                        ws.Cells("E" & ExcelRow).Value = dr("mobile_no")
                        ws.Cells("F" & ExcelRow).Value = dr("result")
                        ws.Cells("G" & ExcelRow).Value = dr("shop_code")
                        ws.Cells("H" & ExcelRow).Value = dr("shop_name_en")
                        ws.Cells("I" & ExcelRow).Value = dr("customer_name")
                        ws.Cells("J" & ExcelRow).Value = dr("item_name")
                        ws.Cells("K" & ExcelRow).Value = dr("username")
                        ws.Cells("L" & ExcelRow).Value = dr("staff_name")
                        ws.Cells("M" & ExcelRow).Value = dr("filter_name")

                        If Convert.ToInt16(dr("nps_score")) > -1 Then
                            ws.Cells("N" & ExcelRow).Value = dr("nps_score")
                        End If
                        ws.Cells("O" & ExcelRow).Value = dr("network_type")
                    Next
                    '## End Detail ##


                    Using RowContent As ExcelRange = ws.Cells("A" & countCriteria & ":O" & i + countCriteria)
                        RowContent.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        RowContent.AutoFitColumns()
                    End Using

                    '//Write it back to the client
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;  filename=Detail_Report_" & DateTime.Now.ToString("yyyyMMddhhmmssfff", New Globalization.CultureInfo("en-US")) & ".xlsx")
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

    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        SearchDetailExcel()
    End Sub

    Protected Sub btnSearch0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch0.Click
        ClearForm()
    End Sub
End Class
