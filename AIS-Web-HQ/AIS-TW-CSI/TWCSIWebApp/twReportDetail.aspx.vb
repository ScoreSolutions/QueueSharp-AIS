Imports System.Data
Imports System.Globalization
Imports System.IO
Imports OfficeOpenXml
Imports System.Drawing
Imports Engine.Common

Partial Class TWCSIWebApp_twReportDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetCombo()
        End If

        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(Me.btnExportExcel)
    End Sub

    Private Sub SetCombo()
        Dim sEng As New Engine.Configuration.MasterENG
        cmbRegion.SetItemList(sEng.GetTWRegionAllList(), "name", "code")
        cmbSFFOrderTypeID.SetItemList(sEng.GetTWSffOrderTypeList("1=1"), "order_type_name", "id")
        sEng = Nothing

        cmbStatus.SetItemList("All", "0")
        cmbStatus.SetItemList("Complete", "2")
        cmbStatus.SetItemList("Incomplete", "1,3")

        cmbNetworkType.SetItemList("All", "")
        cmbNetworkType.SetItemList("2G", "2G")
        cmbNetworkType.SetItemList("3G", "3G")
    End Sub

    Sub ClearForm()
        txtDateFrom.TxtBox.Text = ""
        txtDateTo.TxtBox.Text = ""
        cmbSFFOrderTypeID.SelectedValue = "0"
        cmbStatus.SelectedValue = "0"
        cmbNetworkType.SelectedValue = ""
        cmbRegion.SelectedValue = "0"
        cmbProvince.SelectedValue = "0"
        cmbLocationCode.SelectedValue = "0"
        ctlSelectFilterTemplate1.ClearSelectFilterID()
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
        dt = eng.GetTWDetailDataList(GetWhText)
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
        If cmbRegion.SelectedValue <> "0" Then
            para += "&RegionCode=" & cmbRegion.SelectedValue

            If cmbProvince.SelectedValue <> "0" Then
                para += "&ProvinceCode=" & cmbProvince.SelectedValue

                If cmbLocationCode.SelectedValue <> "0" Then
                    para += "&LocationCode=" & cmbLocationCode.SelectedValue
                End If
            End If
        End If
        If cmbSFFOrderTypeID.SelectedValue <> "0" Then
            para += "&OrderTypeID=" & cmbSFFOrderTypeID.SelectedValue
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

        Dim scr As String = "window.open('../TWCSIWebApp/twReportPreview.aspx" & para & "', '_blank', 'height=650,left=600,location=no,menubar=no,toolbar=no,status=yes,resizable=yes,scrollbars=yes', true);"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "ShowReport", scr, True)

    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Dim sEng As New Engine.Configuration.MasterENG
        cmbProvince.SetItemList(sEng.GetTWProvinceList(cmbRegion.SelectedValue), "name", "code")

        Dim dt As New DataTable
        dt = sEng.GetTWShopList(" and province_code='" & cmbProvince.SelectedValue & "'", "")
        dt.DefaultView.Sort = "location_code"
        dt = dt.DefaultView.ToTable
        cmbLocationCode.SetItemList(dt, "location_name_ddl", "location_code")
        sEng = Nothing
        dt.Dispose()
    End Sub

    Protected Sub cmbProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvince.SelectedIndexChanged
        Dim sEng As New Engine.Configuration.MasterENG

        Dim dt As New DataTable
        dt = sEng.GetTWShopList(" and province_code='" & cmbProvince.SelectedValue & "'", "")
        dt.DefaultView.Sort = "location_code"
        dt = dt.DefaultView.ToTable
        cmbLocationCode.SetItemList(dt, "location_name_ddl", "location_code")
        sEng = Nothing
        dt.Dispose()
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

    Private Function GetWhText() As String
        Dim whText As String = "1=1"
        If txtDateFrom.DateValue.Year <> 1 Then
            whText += " and convert(varchar(8),fd.send_time,112)>='" & txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        End If
        If txtDateTo.DateValue.Year <> 1 Then
            whText += " and convert(varchar(8),fd.send_time,112)<='" & txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
        End If
        If cmbRegion.SelectedValue <> "0" Then
            whText += " and L.region_code='" & cmbRegion.SelectedValue & "'"
        End If
        If cmbProvince.SelectedValue <> "0" Then
            whText += " and L.province_code='" & cmbProvince.SelectedValue & "'"
        End If
        If cmbLocationCode.SelectedValue <> "0" Then
            whText += " and L.location_code='" & cmbLocationCode.SelectedValue & "'"
        End If
        If cmbSFFOrderTypeID.SelectedValue <> "0" Then
            whText += " and fd.order_type_name = (select top 1 order_type_name from tw_sff_order_type where id='" & cmbSFFOrderTypeID.SelectedValue & "')"
        End If

        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            whText += " and fd.tw_filter_id in (" & SelectTemplate & ")"
        End If

        If cmbStatus.SelectedValue <> "0" Then
            'Dim tmpSt() As String = Split(cmbStatus.SelectedValue, ",")
            'Dim tmp As String = ""
            'For Each t As String In tmpSt
            '    If tmp = "" Then
            '        tmp = "'" & t & "'"
            '    Else
            '        tmp += ",'" & t & "'"
            '    End If
            'Next
            whText += " and fd.result_status ='" & cmbStatus.SelectedValue & "'"
        End If
        If cmbNetworkType.SelectedValue <> "" Then
            whText += " and fd.network_type = '" & cmbNetworkType.SelectedValue & "'"
        End If
        Return whText
    End Function

    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        SearchDetailExcel()
    End Sub

    Private Sub SearchDetailExcel()
        Try
            If Validate() = False Then
                Exit Sub
            End If

            Dim eng As New Engine.CSI.CSIReportsENG
            Dim dt As New DataTable
            dt = eng.GetTWDetailDataList(GetWhText)
            Dim countCriteria As Integer = 1

            If dt.Rows.Count > 0 Then
                'Credit EPPlus
                'http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html

                lblMessage.Text = ""
                Using ep As New ExcelPackage

                    '## Start Criterai ##
                    Dim ws As ExcelWorksheet = ep.Workbook.Worksheets.Add("DetailReport")
                    ws.Cells("A" & countCriteria & "").Value = "Date Between : "
                    ws.Cells("B" & countCriteria & "").Value = txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & " - " & txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    countCriteria += 1

                    If cmbSFFOrderTypeID.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "SFFOrderType : "
                        ws.Cells("B" & countCriteria & "").Value = cmbSFFOrderTypeID.SelectedText
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

                    If cmbRegion.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Region : "
                        ws.Cells("B" & countCriteria & "").Value = cmbRegion.SelectedText
                        countCriteria += 1
                    End If

                    If cmbProvince.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Province : "
                        ws.Cells("B" & countCriteria & "").Value = cmbProvince.SelectedText
                        countCriteria += 1
                    End If

                    If cmbLocationCode.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Location : "
                        ws.Cells("B" & countCriteria & "").Value = cmbLocationCode.SelectedText
                        countCriteria += 1
                    End If

                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Try
                        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
                        If SelectTemplate <> "" Then
                            ws.Cells("A" & countCriteria & "").Value = "Template :"
                            Dim f As Integer = 0
                            For Each fID As String In SelectTemplate.Split(",")
                                Dim fPara As New CenParaDB.TABLE.TwFilterCenParaDB
                                Dim fEng As New Engine.CSI.FilterTemplateENG
                                fPara = fEng.GetTWFilterTemplatePara(Convert.ToInt64(fID), trans)

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
                    ws.Cells("A" & countCriteria & "").Value = "Complete Datetime"
                    ws.Cells("B" & countCriteria & "").Value = "Sent Datetime"
                    ws.Cells("C" & countCriteria & "").Value = "ATSR Call Time"
                    ws.Cells("D" & countCriteria & "").Value = "Mobile No"
                    ws.Cells("E" & countCriteria & "").Value = "Result"
                    ws.Cells("F" & countCriteria & "").Value = "Region"
                    ws.Cells("G" & countCriteria & "").Value = "Province"
                    ws.Cells("H" & countCriteria & "").Value = "Location Code"
                    ws.Cells("I" & countCriteria & "").Value = "Location Name"
                    ws.Cells("J" & countCriteria & "").Value = "Name"
                    ws.Cells("K" & countCriteria & "").Value = "Order Type"
                    ws.Cells("L" & countCriteria & "").Value = "Template"
                    ws.Cells("M" & countCriteria & "").Value = "Network Type"
                    ws.Cells("N" & countCriteria & "").Value = "NPS Score"

                    ' //Format the header 
                    Using RowHeader As ExcelRange = ws.Cells("A" & countCriteria & ":N" & countCriteria & "")
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

                        If Convert.IsDBNull(dr("end_time")) = False Then
                            ws.Cells("A" & ExcelRow).Value = Convert.ToDateTime(dr("end_time")).ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("th-TH"))
                        Else
                            ws.Cells("A" & ExcelRow).Value = ""
                        End If

                        If Convert.IsDBNull(dr("send_time")) = False Then
                            ws.Cells("B" & ExcelRow).Value = Convert.ToDateTime(dr("send_time")).ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("th-TH"))
                        Else
                            ws.Cells("B" & ExcelRow).Value = ""
                        End If

                        If Convert.IsDBNull(dr("atsr_call_time")) = False Then
                            ws.Cells("C" & ExcelRow).Value = Convert.ToDateTime(dr("atsr_call_time")).ToString("dd/MM/yyyy HH:mm:ss", New CultureInfo("th-TH"))
                        Else
                            ws.Cells("C" & ExcelRow).Value = ""
                        End If

                        ws.Cells("D" & ExcelRow & "").Value = dr("mobile_no")
                        ws.Cells("E" & ExcelRow & "").Value = dr("result")
                        ws.Cells("F" & ExcelRow & "").Value = dr("region_code")
                        ws.Cells("G" & ExcelRow & "").Value = dr("province_code")
                        ws.Cells("H" & ExcelRow & "").Value = dr("location_code")
                        ws.Cells("I" & ExcelRow & "").Value = dr("location_name_th")
                        ws.Cells("J" & ExcelRow & "").Value = dr("customer_name")
                        ws.Cells("K" & ExcelRow & "").Value = dr("order_type_name")
                        ws.Cells("L" & ExcelRow & "").Value = dr("filter_name")
                        ws.Cells("M" & ExcelRow & "").Value = dr("network_type")
                        If Convert.ToInt64(dr("nps_score")) <= -1 Then
                            ws.Cells("N" & ExcelRow & "").Value = ""
                        Else
                            ws.Cells("N" & ExcelRow & "").Value = dr("nps_score")
                        End If
                    Next
                    '## End Detail ##


                    Using RowContent As ExcelRange = ws.Cells("A" & countCriteria & ":N" & i + countCriteria)
                        RowContent.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        RowContent.AutoFitColumns()
                    End Using

                    '//Write it back to the client
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;  filename=" & DateTime.Now.ToString("yyyyMMddhhmmssfff") & ".xlsx")
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
