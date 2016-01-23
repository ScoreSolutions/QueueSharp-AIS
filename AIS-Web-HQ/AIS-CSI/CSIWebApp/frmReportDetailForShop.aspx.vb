Imports System.Data
Imports System.Globalization
Imports System.IO
Imports OfficeOpenXml
Imports System.Drawing
Imports Engine.Common

Partial Class CSIWebApp_frmReportDetailForShop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim UserName As String = Config.GetLogOnUser.USERNAME
            If UserName <> "" Then
                If Request("MenuID") IsNot Nothing Then
                    If UserName <> "admin" Then
                        If Engine.Common.MenuENG.CheckMenuByUserName(Request("MenuID"), Request.AppRelativeCurrentExecutionFilePath, UserName) = False Then
                            Response.Redirect("../CSIWebApp/frmWelcomePage.aspx?NoAuthen=Y")
                        Else
                            SetCombo(UserName)
                        End If
                    Else
                        SetCombo(UserName)
                    End If
                Else
                    Response.Redirect("../CSIWebApp/frmWelcomePage.aspx?NoAuthen=Y")
                End If
            Else
                Session.RemoveAll()
                Session.Abandon()
                Response.Redirect("../frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
            End If
            
        End If

        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(Me.btnExportExcel)
    End Sub

    Private Sub SetCombo(ByVal UserName As String)
        If UserName <> "admin" Then
            Dim dt As New DataTable
            Dim eng As New Engine.Common.LoginENG
            dt = eng.GetShopListByUser(UserName)
            If dt.Rows.Count > 0 Then
                Dim SelShopID As String = "0"
                If dt.Rows.Count = 1 Then
                    SelShopID = dt.Rows(0)("id")
                End If
                cmbShopID.SetItemList(dt, "shop_name_en", "id")

                If SelShopID > "0" Then
                    cmbShopID.SelectedValue = SelShopID
                    cmbShopID_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
            dt.Dispose()
        Else
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.GetActiveShopDDL
            If dt.Rows.Count > 0 Then
                Dim SelShopID As String = "0"
                If dt.Rows.Count = 1 Then
                    SelShopID = dt.Rows(0)("id")
                End If
                cmbShopID.SetItemList(dt, "shop_name", "id")

                If SelShopID > "0" Then
                    cmbShopID.SelectedValue = SelShopID
                    cmbShopID_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
            dt.Dispose()
        End If


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
        Else
            Dim shDt As New DataTable
            Dim eng As New Engine.Common.LoginENG
            shDt = eng.GetShopListByUser(Config.GetLogOnUser.USERNAME)
            If shDt.Rows.Count > 0 Then
                Dim tmp As String = ""
                For Each shDr As DataRow In shDt.Rows
                    If tmp = "" Then
                        tmp = "'" & shDr("id") & "'"
                    Else
                        tmp += "," & "'" & shDr("id") & "'"
                    End If
                Next
                whText += " and s.id in (" & tmp & ")"
            End If
            shDt.Dispose()
            eng = Nothing
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
        para += "?ReportName=DetailForShop"
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
        Else
            Dim UserName As String = Config.GetLogOnUser.USERNAME
            para += "&username=" & UserName
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
            Try
                Dim eng As New Engine.Configuration.ShopUserENG
                Dim dt As New DataTable
                dt = eng.GetShopAllUserNotAdmin(cmbShopID.SelectedValue)
                dt.DefaultView.RowFilter = "username<>'admin'"

                dt = dt.DefaultView.ToTable
                cmbShopUserID.SetItemList(dt, "fullname", "username")
                dt.Dispose()
                eng = Nothing
            Catch ex As Exception

            End Try
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
                    Dim ws As ExcelWorksheet = ep.Workbook.Worksheets.Add("Detail_Report_for_Shop_" & DateTime.Now.ToString("yyyyMMddhhmmssfff"))
                    ws.Cells("A" & countCriteria & "").Value = "Date Between : "
                    ws.Cells("C" & countCriteria & "").Value = txtDateFrom.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & txtDateTo.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                    countCriteria += 1

                    If cmbShopID.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Location : "
                        ws.Cells("C" & countCriteria & "").Value = cmbShopID.SelectedText
                        countCriteria += 1
                    End If

                    If cmbServiceID.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Service : "
                        ws.Cells("C" & countCriteria & "").Value = cmbServiceID.SelectedText
                        countCriteria += 1
                    End If

                    If cmbShopUserID.SelectedValue <> "" Then
                        ws.Cells("A" & countCriteria & "").Value = "Agent : "
                        ws.Cells("C" & countCriteria & "").Value = cmbShopUserID.SelectedText
                        countCriteria += 1
                    End If

                    If cmbStatus.SelectedValue <> "0" Then
                        ws.Cells("A" & countCriteria & "").Value = "Status : "
                        ws.Cells("C" & countCriteria & "").Value = cmbStatus.SelectedText
                        countCriteria += 1
                    End If
                    
                    If cmbNetworkType.SelectedValue <> "" Then
                        ws.Cells("A" & countCriteria & "").Value = "Network Type : "
                        ws.Cells("C" & countCriteria & "").Value = cmbNetworkType.SelectedValue
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

                                ws.Cells("C" & countCriteria & "").Value = fPara.FILTER_NAME
                                countCriteria += 1
                            Next
                        End If
                        trans.CommitTransaction()
                    Catch ex As Exception
                        trans.RollbackTransaction()
                    End Try

                    For cnt As Integer = 1 To countCriteria - 1
                        'ws.Cells(cnt, 3, cnt, 4).Merge = True
                        ws.Cells("A" & cnt).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    Next

                    countCriteria += 1
                    '## End Criterai ##

                    '## Start Header ##
                    ws.Cells("A" & countCriteria & "").Value = "Date"
                    ws.Cells("B" & countCriteria & "").Value = "Result"
                    ws.Cells("C" & countCriteria & "").Value = "Location Code"
                    ws.Cells("D" & countCriteria & "").Value = "Location Name"
                    ws.Cells("E" & countCriteria & "").Value = "Service Type"
                    ws.Cells("F" & countCriteria & "").Value = "Username"
                    ws.Cells("G" & countCriteria & "").Value = "Staff Name"
                    ws.Cells("H" & countCriteria & "").Value = "NPS Score"
                    ws.Cells("I" & countCriteria & "").Value = "Network Type"

                    ' //Format the header 
                    Using RowHeader As ExcelRange = ws.Cells("A" & countCriteria & ":I" & countCriteria & "")
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
                        End If

                        ws.Cells("B" & ExcelRow).Value = dr("result")
                        ws.Cells("C" & ExcelRow).Value = dr("shop_code")
                        ws.Cells("D" & ExcelRow).Value = dr("shop_name_en")
                        ws.Cells("E" & ExcelRow).Value = dr("item_name")
                        ws.Cells("F" & ExcelRow).Value = dr("username")
                        ws.Cells("G" & ExcelRow).Value = dr("staff_name")

                        If Convert.ToInt16(dr("nps_score")) > -1 Then
                            ws.Cells("H" & ExcelRow).Value = dr("nps_score")
                        End If
                        ws.Cells("I" & ExcelRow).Value = dr("network_type")
                    Next
                    '## End Detail ##


                    Using RowContent As ExcelRange = ws.Cells("A" & countCriteria & ":I" & i + countCriteria)
                        RowContent.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        RowContent.AutoFitColumns()
                    End Using

                    '//Write it back to the client
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;  filename=Detail_Report_for_Shop_" & DateTime.Now.ToString("yyyyMMddhhmmssfff") & ".xlsx")
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
