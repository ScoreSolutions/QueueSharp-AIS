Imports System.Data
Imports System.Globalization
Imports System.IO
Imports OfficeOpenXml
Imports System.Drawing
Imports Engine.Common

Partial Class CSIWebApp_frmReportCSIByAgent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request("ForShop") IsNot Nothing Then
                If Request("MenuID") IsNot Nothing Then
                    Dim UserName As String = Config.GetLogOnUser.USERNAME
                    SetCombo(UserName)
                    lblReportName.Text = "CSI By Agent Report for Shop"
                Else
                    Response.Redirect("../CSIWebApp/frmWelcomePage.aspx?NoAuthen=Y")
                End If
            Else
                SetCombo()
            End If
        End If

        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(Me.btnExportExcel)
    End Sub

    Private Sub SetCombo(Optional ByVal UserName As String = "")
        If UserName = "" Then
            cmbShopID.SetItemList(Engine.Common.FunctionEng.GetActiveShopDDL, "shop_name", "id")
        Else
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
        End If


        Dim sEng As New Engine.Configuration.MasterENG
        cmbServiceID.SetItemList(sEng.GetServiceActiveList("1=1"), "item_name", "id")
        sEng = Nothing

        cmbNetworkType.SetItemList("All", "")
        cmbNetworkType.SetItemList("2G", "2G")
        cmbNetworkType.SetItemList("3G", "3G")
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

        'Dim eng As New Engine.CSI.CSIReportsENG
        'Dim dt As New DataTable
        'dt = eng.GetCSIByAgentDataList(whText)


        Dim para As String = ""
        para += "?ReportName=CSI_BY_AGENT"
        para += "&rnd=" & DateTime.Now.Millisecond
        If Request("ForShop") IsNot Nothing Then
            para += "&ForShop=Y"
        End If

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
        Else
            If Request("ForShop") IsNot Nothing Then
                Dim UserName As String = Config.GetLogOnUser.USERNAME
                para += "&username=" & UserName
            End If
        End If
        If cmbServiceID.SelectedValue <> "0" Then
            para += "&ServiceID=" & cmbServiceID.SelectedValue
        End If

        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
        If SelectTemplate <> "" Then
            para += "&TemplateID=" & SelectTemplate
        End If
        'If cmbStatus.SelectedValue <> "0" Then
        para += "&Status=2" 'Complete
        'End If
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

    Protected Sub btnSearch0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch0.Click
        ClearForm()
    End Sub

    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        SearchDetailExcel()
    End Sub

    Sub SetHeadExcelStyle(ByVal ExcelRowHeader As ExcelRange, ByVal ws As ExcelWorksheet)
        Using RowHeader As ExcelRange = ExcelRowHeader
            RowHeader.Style.Font.Bold = True
            RowHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            RowHeader.Style.Fill.BackgroundColor.SetColor(Color.Yellow)
            RowHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            RowHeader.Style.Font.Color.SetColor(Color.Black)
        End Using
    End Sub

    Private Sub SearchDetailExcel()
        Try
            If Validate() = False Then
                Exit Sub
            End If

            Dim sEng As New Engine.Configuration.MasterENG
            Dim sDt As New DataTable
            Dim sWh As String = " 1 = 1 "
            If cmbServiceID.SelectedValue <> "0" Then
                sWh += " and id= '" & cmbServiceID.SelectedValue & "'"
            End If

            sDt = sEng.GetServiceActiveList(sWh)
            Dim shWh As String = " 1=1 "
            If cmbShopID.SelectedValue <> "0" Then
                shWh += " and sh.id = '" & cmbShopID.SelectedValue & "'"
            Else
                If Request("ForShop") IsNot Nothing Then
                    Dim tmpDt As New DataTable
                    Dim eng As New Engine.Common.LoginENG
                    tmpDt = eng.GetShopListByUser(Config.GetLogOnUser.USERNAME)
                    If tmpDt.Rows.Count > 0 Then
                        Dim tmp As String = ""
                        For Each shDr As DataRow In tmpDt.Rows
                            If tmp = "" Then
                                tmp = "'" & shDr("id") & "'"
                            Else
                                tmp += "," & "'" & shDr("id") & "'"
                            End If
                        Next
                        shWh += " and sh.id in (" & tmp & ")"
                    End If
                    tmpDt.Dispose()
                    eng = Nothing
                End If
            End If
            Dim shDt As New DataTable
            Dim shEng As New Engine.Configuration.MasterENG
            shDt = shEng.GetShopList(shWh)

            Dim shopeng As New Engine.Configuration.ShopUserENG
            Dim shUserDT As New DataTable
            For j As Integer = 0 To shDt.Rows.Count - 1
                Try
                    Dim tmp As New DataTable
                    tmp = shopeng.GetShopAllUser(shDt.Rows(j)("id"))
                    tmp.DefaultView.RowFilter = "username<>'admin'"
                    If tmp.DefaultView.Count > 0 Then
                        tmp = tmp.DefaultView.ToTable.Copy
                    End If
                    shUserDT.Merge(tmp)
                    tmp.Dispose()
                Catch ex As Exception

                End Try
            Next
            If sDt.Rows.Count > 0 Then
                Dim RowId As Integer = 1
                lblMessage.Text = ""
                Using ep As New ExcelPackage
                    '## Start Criteria ##
                    Dim ws As ExcelWorksheet
                    If Request("ForShop") IsNot Nothing Then
                        ws = ep.Workbook.Worksheets.Add("CSIByAgentReportForShop")
                    Else
                        ws = ep.Workbook.Worksheets.Add("CSIByAgentReport")
                    End If

                    ws.Cells("A" & RowId & "").Value = "Date Between : "
                    ws.Cells("C" & RowId & "").Value = txtDateFrom.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH")) & " - " & txtDateTo.DateValue.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                    RowId += 1

                    If cmbShopID.SelectedValue <> "0" Then
                        ws.Cells("A" & RowId & "").Value = "Location : "
                        ws.Cells("C" & RowId & "").Value = cmbShopID.SelectedText
                        RowId += 1
                    End If

                    If cmbServiceID.SelectedValue <> "0" Then
                        ws.Cells("A" & RowId & "").Value = "Service : "
                        ws.Cells("C" & RowId & "").Value = cmbServiceID.SelectedText
                        RowId += 1
                    End If

                    If cmbShopUserID.SelectedValue <> "" Then
                        ws.Cells("A" & RowId & "").Value = "Agent : "
                        ws.Cells("C" & RowId & "").Value = cmbShopUserID.SelectedText
                        RowId += 1
                    End If

                    ws.Cells("A" & RowId & "").Value = "Status : "
                    ws.Cells("C" & RowId & "").Value = lblStatus.Text
                    RowId += 1

                    If cmbNetworkType.SelectedValue <> "" Then
                        ws.Cells("A" & RowId & "").Value = "Network Type : "
                        ws.Cells("C" & RowId & "").Value = cmbNetworkType.SelectedValue
                        RowId += 1
                    End If

                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Try
                        Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
                        If SelectTemplate <> "" Then
                            ws.Cells("A" & RowId & "").Value = "Template :"
                            Dim f As Integer = 0
                            For Each fID As String In SelectTemplate.Split(",")
                                Dim fPara As New CenParaDB.TABLE.TbFilterCenParaDB
                                Dim fEng As New Engine.CSI.FilterTemplateENG
                                fPara = fEng.GetFilterTemplatePara(Convert.ToInt64(fID), trans)

                                ws.Cells("C" & RowId & "").Value = fPara.FILTER_NAME
                                RowId += 1
                            Next
                        End If
                        trans.CommitTransaction()
                    Catch ex As Exception
                        trans.RollbackTransaction()
                    End Try

                    'แสดง Segment ให้เพิ่ม Code ตรงนี้
                    Dim Segment As String = ""
                    If Segment <> "" Then
                        ws.Cells("A" & RowId & "").Value = "Segment : "
                        ws.Cells("C" & RowId & "").Value = Segment
                        RowId += 1
                    End If

                    For cnt As Integer = 1 To RowId - 1
                        ws.Cells("A" & cnt).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    Next
                    RowId += 1
                    '## End Criteria ##

                    Dim MonthHeaderRow = RowId
                    '## Start Header ##
                    Dim ServQty As Long = sDt.Rows.Count    'จำนวน Service +2(คือ Overall และ Customer Service)
                    'สร้างชื่อคอลัมน์ก่อน
                    Dim DateFrom As Date = FunctionEng.cStrToDate3(txtDateFrom.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")))
                    Dim DateTo As Date = FunctionEng.cStrToDate3(txtDateTo.DateValue.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")))
                    Dim CurrMonth As Date = DateFrom

                    ws.Cells("A" & (MonthHeaderRow + 2) & "").Value = "Location Code"
                    ws.Cells("B" & (MonthHeaderRow + 2) & "").Value = "Location Name"
                    ws.Cells("C" & (MonthHeaderRow + 2) & "").Value = "Username"
                    ws.Cells("D" & (MonthHeaderRow + 2) & "").Value = "Staff Name"
                    ws.Cells("E" & (MonthHeaderRow + 2) & "").Value = "Segment"

                    'Month Row
                    ws.Cells(MonthHeaderRow, 1, MonthHeaderRow, 5).Merge = True
                    ws.Cells(MonthHeaderRow, 1, MonthHeaderRow, 5).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    ws.Cells(MonthHeaderRow, 1, MonthHeaderRow, 5).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    ws.Cells(MonthHeaderRow, 1, MonthHeaderRow, 5).Style.Fill.BackgroundColor.SetColor(Color.Gray)


                    'Service Row
                    Dim ServiceHeaderRow As Integer = MonthHeaderRow + 1
                    ws.Cells(ServiceHeaderRow, 1, ServiceHeaderRow, 5).Merge = True
                    
                    Dim ColID As Integer = 6   'ข้อมูล Service เริ่มที่คอลัมน์ที่ ColID
                    Do
                        Dim StartDate As Date = IIf(CurrMonth = DateFrom, DateFrom, New Date(CurrMonth.Year, CurrMonth.Month, 1))
                        Dim EndDate As Date = IIf(CurrMonth.ToString("MMyyyy") = DateTo.ToString("MMyyyy"), DateTo, New Date(CurrMonth.Year, CurrMonth.Month, CurrMonth.DaysInMonth(CurrMonth.Year, CurrMonth.Month)))

                        ws.Cells(MonthHeaderRow, ColID).Value = CurrMonth.ToString("MMMM", New CultureInfo("en-US")) & " " & CurrMonth.ToString("yyyy", New CultureInfo("th-TH")) 'September 2557
                        Dim mRgn As ExcelRange = ws.Cells(MonthHeaderRow, ColID, MonthHeaderRow, (ServQty * 7) + ColID - 1)
                        mRgn.Merge = True
                        mRgn.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        mRgn.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        mRgn.Style.Fill.BackgroundColor.SetColor(Color.Gray)


                        For Each sDr As DataRow In sDt.Rows
                            ws.Cells(ServiceHeaderRow, ColID).Value = sDr("item_name")
                            ws.Cells(ServiceHeaderRow, ColID, ServiceHeaderRow, 6 + ColID).Merge = True
                            ws.Cells(ServiceHeaderRow, ColID, ServiceHeaderRow, 6 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            
                            ws.Cells(MonthHeaderRow + 2, ColID).Value = "ดีมาก"
                            ws.Cells(MonthHeaderRow + 2, ColID, MonthHeaderRow + 2, 1 + ColID).Merge = True
                            ws.Cells(MonthHeaderRow + 2, ColID, MonthHeaderRow + 2, 1 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                            ws.Cells(MonthHeaderRow + 2, ColID + 2).Value = "ดี"
                            ws.Cells(MonthHeaderRow + 2, ColID + 2, MonthHeaderRow + 2, 3 + ColID).Merge = True
                            ws.Cells(MonthHeaderRow + 2, ColID + 2, MonthHeaderRow + 2, 3 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                            ws.Cells(MonthHeaderRow + 2, ColID + 4).Value = "ควรปรับปรุง"
                            ws.Cells(MonthHeaderRow + 2, ColID + 4, MonthHeaderRow + 2, 5 + ColID).Merge = True
                            ws.Cells(MonthHeaderRow + 2, ColID + 4, MonthHeaderRow + 2, 5 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                            ws.Cells(MonthHeaderRow + 2, ColID + 6).Value = "%CSI " & sDr("item_name")
                            ws.Cells(MonthHeaderRow + 2, ColID + 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                            ColID += 7
                        Next
                        CurrMonth = DateAdd(DateInterval.Month, 1, CurrMonth)
                    Loop While CurrMonth.ToString("yyyyMM") <= DateTo.ToString("yyyyMM")

                    'Overall  ####################
                    ws.Cells(MonthHeaderRow, ColID).Value = ""
                    Dim omRgn As ExcelRange = ws.Cells(MonthHeaderRow, ColID, MonthHeaderRow, ColID + 6)
                    omRgn.Merge = True
                    omRgn.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    omRgn.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    omRgn.Style.Fill.BackgroundColor.SetColor(Color.Gray)

                    ws.Cells(MonthHeaderRow + 1, ColID).Value = "Overall"
                    ws.Cells(MonthHeaderRow + 1, ColID, MonthHeaderRow + 1, 6 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 1, ColID, MonthHeaderRow + 1, 6 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID).Value = "ดีมาก"
                    ws.Cells(MonthHeaderRow + 2, ColID, MonthHeaderRow + 2, 1 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 2, ColID, MonthHeaderRow + 2, 1 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID + 2).Value = "ดี"
                    ws.Cells(MonthHeaderRow + 2, ColID + 2, MonthHeaderRow + 2, 3 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 2, ColID + 2, MonthHeaderRow + 2, 3 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID + 4).Value = "ควรปรับปรุง"
                    ws.Cells(MonthHeaderRow + 2, ColID + 4, MonthHeaderRow + 2, 5 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 2, ColID + 4, MonthHeaderRow + 2, 5 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID + 6).Value = "%CSI Overall"
                    ws.Cells(MonthHeaderRow + 2, ColID + 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    ColID += 7
                    'Overall ##############################

                    'Customer Service (Include SIM Change & Upgrade to AIS 3G) ##########################
                    ws.Cells(MonthHeaderRow, ColID).Value = ""
                    Dim cmRgn As ExcelRange = ws.Cells(MonthHeaderRow, ColID, MonthHeaderRow, ColID + 6)
                    cmRgn.Merge = True
                    cmRgn.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    cmRgn.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    cmRgn.Style.Fill.BackgroundColor.SetColor(Color.Gray)

                    ws.Cells(MonthHeaderRow + 1, ColID).Value = "Customer Service" & vbNewLine & "(Include SIM Change & Upgrade to AIS 3G)"
                    ws.Cells(MonthHeaderRow + 1, ColID, MonthHeaderRow + 1, 6 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 1, ColID, MonthHeaderRow + 1, 6 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID).Value = "ดีมาก"
                    ws.Cells(MonthHeaderRow + 2, ColID, MonthHeaderRow + 2, 1 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 2, ColID, MonthHeaderRow + 2, 1 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID + 2).Value = "ดี"
                    ws.Cells(MonthHeaderRow + 2, ColID + 2, MonthHeaderRow + 2, 3 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 2, ColID + 2, MonthHeaderRow + 2, 3 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID + 4).Value = "ควรปรับปรุง"
                    ws.Cells(MonthHeaderRow + 2, ColID + 4, MonthHeaderRow + 2, 5 + ColID).Merge = True
                    ws.Cells(MonthHeaderRow + 2, ColID + 4, MonthHeaderRow + 2, 5 + ColID).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                    ws.Cells(MonthHeaderRow + 2, ColID + 6).Value = "%CSI Customer Service"
                    ws.Cells(MonthHeaderRow + 2, ColID + 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    ColID += 7
                    'Customer Service (Include SIM Change & Upgrade to AIS 3G) ##########################

                    'Set Style For Service Header Role
                    ws.Cells(ServiceHeaderRow, 1, ServiceHeaderRow + 1, ColID - 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    ws.Cells(ServiceHeaderRow, 1, ServiceHeaderRow + 1, ColID - 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("yellowgreen"))

                    'Set Border For Header Row
                    Using HRng As ExcelRange = ws.Cells(MonthHeaderRow, 1, MonthHeaderRow + 2, ColID - 1)
                        HRng.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        HRng.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        HRng.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        HRng.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                    End Using
                    RowId = MonthHeaderRow + 3
                    ''## End Header ##


                    '## Start DataRow ##
                    Dim reDt As New DataTable
                    shDt.DefaultView.Sort = "region_code,shop_name_en"
                    reDt = shDt.DefaultView.ToTable(True, "region_id", "region_code").Copy
                    If reDt.Rows.Count > 0 Then
                        Dim GTotVeryGood(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                        Dim GTotGood(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                        Dim GTotAdjust(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                        Dim GTotTotal(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                        '### ตั้มเพิ่ม ไว้เก็บค่า Grand Total ######
                        'Customer Service
                        Dim GTotVeryGood_Customer_Service As Double
                        Dim GTotGood_Customer_Service As Double
                        Dim GTotAdjust_Customer_Service As Double
                        Dim GTotTotal_Customer_Service As Double
                        'Overall
                        Dim GTotVeryGood_Overall As Double
                        Dim GTotGood_Overall As Double
                        Dim GTotAdjust_Overall As Double
                        Dim GTotTotal_Overall As Double
                        '##################################

                        For Each reDr As DataRow In reDt.Rows     'Loop by Region
                            Dim TotVeryGood(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                            Dim TotGood(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                            Dim TotAdjust(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                            Dim TotTotal(sDt.Rows.Count * (DateDiff(DateInterval.Month, DateFrom, DateTo) + 1)) As Double
                            '### ตั้มเพิ่ม ไว้เก็บค่า Total และ Clear ค่า ######
                            'Customer Service
                            Dim TotVeryGood_Customer_Service As Double = 0
                            Dim TotGood_Customer_Service As Double = 0
                            Dim TotAdjust_Customer_Service As Double = 0
                            Dim TotTotal_Customer_Service As Double = 0
                            'Over all
                            Dim TotVeryGood_Overall As Double = 0
                            Dim TotGood_Overall As Double = 0
                            Dim TotAdjust_Overall As Double = 0
                            Dim TotTotal_Overall As Double = 0

                            'Customer Service
                            Dim vVeryGood_Customer_Service As Double = 0
                            Dim vGood_Customer_Service As Double = 0
                            Dim vAdjust_Customer_Service As Double = 0
                            Dim vTotal_Customer_Service As Double = 0
                            'Overall
                            Dim vVeryGood_Overall As Double = 0
                            Dim vGood_Overall As Double = 0
                            Dim vAdjust_Overall As Double = 0
                            Dim vTotal_Overall As Double = 0
                            '##################################


                            shDt.DefaultView.RowFilter = "region_id='" & reDr("region_id") & "'"
                            For Each shDr As DataRowView In shDt.DefaultView
                                Dim wh As String = " convert(varchar(8),fd.send_time, 112) between '" & DateFrom.ToString("yyyyMMdd", New CultureInfo("en-US")) & "' and '" & DateTo.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
                                If cmbServiceID.SelectedValue <> "0" Then
                                    wh += " and fd.tb_item_id = '" & cmbServiceID.SelectedValue & "'"
                                End If

                                wh += " and fd.shop_id = '" & shDr("id") & "'"

                                Dim SelectTemplate As String = ctlSelectFilterTemplate1.GetSelectedFilterID
                                If SelectTemplate <> "" Then
                                    wh += " and fd.tb_filter_id in (" & SelectTemplate & ")"
                                End If
                                If cmbShopUserID.SelectedValue <> "" Then
                                    wh += " and fd.username='" & cmbShopUserID.SelectedValue & "'"
                                End If
                                
                                wh += " and fd.result_status in (2)"
                                If cmbNetworkType.SelectedValue <> "" Then
                                    wh += " and fd.network_type ='" & cmbNetworkType.SelectedValue & "'"
                                End If

                                'เพิ่มเงื่อนไข Segment ตรงนี้                                
                                If Segment.Trim <> "" Then
                                    wh += " and fd.segment = '" & Segment & "'"
                                End If


                                Dim userDT As New DataTable
                                Dim UserEng As New Engine.CSI.CSIReportsENG
                                userDT = UserEng.GetUserCSIByAgentList(wh)

                                If userDT.Rows.Count > 0 Then
                                    For Each uDr As DataRow In userDT.Rows
                                        ws.Cells("A" & RowId).Value = shDr("shop_code")
                                        ws.Cells("B" & RowId).Value = shDr("shop_name_en")
                                        ws.Cells("C" & RowId).Value = uDr("username")
                                        ws.Cells("D" & RowId).Value = uDr("staff_name")
                                        ws.Cells("E" & RowId).Value = uDr("segment")

                                        ColID = 5
                                        Dim s As Integer = 0
                                        Dim tt As Integer = 0
                                        CurrMonth = DateFrom
                                        Do
                                            Dim StartDate As Date = IIf(CurrMonth = DateFrom, DateFrom, New Date(CurrMonth.Year, CurrMonth.Month, 1))
                                            Dim EndDate As Date = IIf(CurrMonth.ToString("MMyyyy") = DateTo.ToString("MMyyyy"), DateTo, New Date(CurrMonth.Year, CurrMonth.Month, CurrMonth.DaysInMonth(CurrMonth.Year, CurrMonth.Month)))
                                            For Each sDr As DataRow In sDt.Rows
                                                Dim whText As String = " convert(varchar(8),fd.send_time, 112) between '" & StartDate.ToString("yyyyMMdd", New CultureInfo("en-US")) & "' and '" & EndDate.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
                                                whText += " and fd.tb_item_id = '" & sDr("id") & "'"
                                                whText += " and fd.shop_id = '" & shDr("id") & "'"
                                                If SelectTemplate <> "" Then
                                                    whText += " and fd.tb_filter_id in (" & SelectTemplate & ")"
                                                End If
                                                whText += " and fd.username='" & uDr("username") & "'"
                                                If Convert.IsDBNull(uDr("segment")) = False Then
                                                    whText += " and fd.segment = '" & uDr("segment") & "'"
                                                Else
                                                    whText += " and fd.segment is null"
                                                End If

                                                whText += " and fd.result_status in (2)"

                                                If cmbNetworkType.SelectedValue <> "" Then
                                                    whText += " and fd.network_type ='" & cmbNetworkType.SelectedValue & "'"
                                                End If

                                                Dim eng As New Engine.CSI.CSIReportsENG
                                                Dim dt As New DataTable
                                                dt = eng.GetCSIByAgentDataList(whText)

                                                If dt.Rows.Count > 0 Then
                                                    Dim tmpShop As String = ""
                                                    Dim dr As DataRow = dt.Rows(0)
                                                    Dim vVeryGood As Double = Convert.ToDouble(dr("result_very_good"))
                                                    Dim vGood As Double = Convert.ToDouble(dr("result_good"))
                                                    Dim vAdjust As Double = Convert.ToDouble(dr("result_adjust"))
                                                    Dim vTotal As Double = vVeryGood + vGood + vAdjust

                                                    '### ตั้มเพิ่ม ไว้เก็บค่า Detail ไว้แสดง ######
                                                    'Customer Service
                                                    If sDr("id") = 6 Or sDr("id") = 8 Or sDr("id") = 2 Then '2 Customer Service,6 SimChage,8 Upgrad To 3g
                                                        vVeryGood_Customer_Service += Convert.ToDouble(dr("result_very_good"))
                                                        vGood_Customer_Service += Convert.ToDouble(dr("result_good"))
                                                        vAdjust_Customer_Service += Convert.ToDouble(dr("result_adjust"))
                                                        vTotal_Customer_Service += Convert.ToDouble(dr("result_very_good")) + Convert.ToDouble(dr("result_good")) + Convert.ToDouble(dr("result_adjust"))
                                                    End If
                                                    'Overall
                                                    vVeryGood_Overall += Convert.ToDouble(dr("result_very_good"))
                                                    vGood_Overall += Convert.ToDouble(dr("result_good"))
                                                    vAdjust_Overall += Convert.ToDouble(dr("result_adjust"))
                                                    vTotal_Overall += Convert.ToDouble(dr("result_very_good")) + Convert.ToDouble(dr("result_good")) + Convert.ToDouble(dr("result_adjust"))
                                                    '### ตั้มเพิ่ม ไว้เก็บค่า Detail ไว้แสดง ######

                                                    Dim vVeryGoodP As Double = 0
                                                    Dim vGoodP As Double = 0
                                                    Dim vAdjustP As Double = 0
                                                    Dim vTotalP As Double = 0
                                                    If vTotal <> 0 Then
                                                        vVeryGoodP = ((vVeryGood * 100) / vTotal)
                                                        vGoodP = ((vGood * 100) / vTotal)
                                                        vAdjustP = ((vAdjust * 100) / vTotal)
                                                        vTotalP = (((vVeryGood + (vGood / 2)) * 100) / vTotal)
                                                    End If
                                                    ws.Cells(RowId, ColID + 1).Value = vVeryGood
                                                    ws.Cells(RowId, ColID + 2).Value = Math.Round(vVeryGoodP) & "%"
                                                    ws.Cells(RowId, ColID + 3).Value = vGood
                                                    ws.Cells(RowId, ColID + 4).Value = Math.Round(vGoodP) & "%"
                                                    ws.Cells(RowId, ColID + 5).Value = vAdjust
                                                    ws.Cells(RowId, ColID + 6).Value = Math.Round(vAdjustP) & "%"
                                                    ws.Cells(RowId, ColID + 7).Value = Math.Round(vTotalP) & "%"

                                                    TotVeryGood(s) += vVeryGood
                                                    TotGood(s) += vGood
                                                    TotAdjust(s) += vAdjust
                                                    TotTotal(s) += vTotal

                                                    GTotVeryGood(tt) += vVeryGood
                                                    GTotGood(tt) += vGood
                                                    GTotAdjust(tt) += vAdjust
                                                    GTotTotal(tt) += vTotal
                                                    'Next
                                                    dt.Dispose()
                                                End If
                                                ColID += 7
                                                s += 1
                                                tt += 1
                                            Next
                                            CurrMonth = DateAdd(DateInterval.Month, 1, CurrMonth)
                                        Loop While CurrMonth.ToString("yyyyMM") <= DateTo.ToString("yyyyMM")


                                        '############# Overall
                                        Dim vVeryGoodP_Overall As Double = 0
                                        Dim vGoodP_Overall As Double = 0
                                        Dim vAdjustP_Overall As Double = 0
                                        Dim vTotalP_Overall As Double = 0

                                        If vTotal_Overall <> 0 Then
                                            vVeryGoodP_Overall = ((vVeryGood_Overall * 100) / vTotal_Overall)
                                            vGoodP_Overall = ((vGood_Overall * 100) / vTotal_Overall)
                                            vAdjustP_Overall = ((vAdjust_Overall * 100) / vTotal_Overall)
                                            vTotalP_Overall = (((vVeryGood_Overall + (vGood_Overall / 2)) * 100) / vTotal_Overall)
                                        End If

                                        If vTotal_Overall <> 0 Then
                                            ws.Cells(RowId, ColID + 1).Value = vVeryGood_Overall
                                            ws.Cells(RowId, ColID + 2).Value = Math.Round(vVeryGoodP_Overall) & "%"
                                            ws.Cells(RowId, ColID + 3).Value = vGood_Overall
                                            ws.Cells(RowId, ColID + 4).Value = Math.Round(vGoodP_Overall) & "%"
                                            ws.Cells(RowId, ColID + 5).Value = vAdjust_Overall
                                            ws.Cells(RowId, ColID + 6).Value = Math.Round(vAdjustP_Overall) & "%"
                                            ws.Cells(RowId, ColID + 7).Value = Math.Round(vTotalP_Overall) & "%"

                                            TotVeryGood_Overall += vVeryGood_Overall
                                            TotGood_Overall += vGood_Overall
                                            TotAdjust_Overall += vAdjust_Overall
                                            TotTotal_Overall += vTotal_Overall

                                            GTotVeryGood_Overall += vVeryGood_Overall
                                            GTotGood_Overall += vGood_Overall
                                            GTotAdjust_Overall += vAdjust_Overall
                                            GTotTotal_Overall += vTotal_Overall
                                            '## Clear ค่า ###
                                            vVeryGood_Overall = 0
                                            vGood_Overall = 0
                                            vAdjust_Overall = 0
                                            vTotal_Overall = 0
                                            '##############
                                        End If
                                        ColID += 7
                                        '########## End Overall

                                        '#########customer service
                                        Dim vVeryGoodP_Customer_Service As Double = 0
                                        Dim vGoodP_Customer_Service As Double = 0
                                        Dim vAdjustP_Customer_Service As Double = 0
                                        Dim vTotalP_Customer_Service As Double = 0

                                        If vTotal_Customer_Service <> 0 Then
                                            vVeryGoodP_Customer_Service = ((vVeryGood_Customer_Service * 100) / vTotal_Customer_Service)
                                            vGoodP_Customer_Service = ((vGood_Customer_Service * 100) / vTotal_Customer_Service)
                                            vAdjustP_Customer_Service = ((vAdjust_Customer_Service * 100) / vTotal_Customer_Service)
                                            vTotalP_Customer_Service = (((vVeryGood_Customer_Service + (vGood_Customer_Service / 2)) * 100) / vTotal_Customer_Service)
                                        End If

                                        If vTotal_Customer_Service <> 0 Then
                                            ws.Cells(RowId, ColID + 1).Value = vVeryGood_Customer_Service
                                            ws.Cells(RowId, ColID + 2).Value = Math.Round(vVeryGoodP_Customer_Service) & "%"
                                            ws.Cells(RowId, ColID + 3).Value = vGood_Customer_Service
                                            ws.Cells(RowId, ColID + 4).Value = Math.Round(vGoodP_Customer_Service) & "%"
                                            ws.Cells(RowId, ColID + 5).Value = vAdjust_Customer_Service
                                            ws.Cells(RowId, ColID + 6).Value = Math.Round(vAdjustP_Customer_Service) & "%"
                                            ws.Cells(RowId, ColID + 7).Value = Math.Round(vTotalP_Customer_Service) & "%"

                                            TotVeryGood_Customer_Service += vVeryGood_Customer_Service
                                            TotGood_Customer_Service += vGood_Customer_Service
                                            TotAdjust_Customer_Service += vAdjust_Customer_Service
                                            TotTotal_Customer_Service += vTotal_Customer_Service

                                            GTotVeryGood_Customer_Service += vVeryGood_Customer_Service
                                            GTotGood_Customer_Service += vGood_Customer_Service
                                            GTotAdjust_Customer_Service += vAdjust_Customer_Service
                                            GTotTotal_Customer_Service += vTotal_Customer_Service
                                            '## Clear ค่า ###
                                            vVeryGood_Customer_Service = 0
                                            vGood_Customer_Service = 0
                                            vAdjust_Customer_Service = 0
                                            vTotal_Customer_Service = 0
                                            '##############
                                            'Else
                                            '    ws.Cells(RowId, ColID + 1).Value = ""
                                            '    ws.Cells(RowId, ColID + 2).Value = ""
                                            '    ws.Cells(RowId, ColID + 3).Value = ""
                                            '    ws.Cells(RowId, ColID + 4).Value = ""
                                            '    ws.Cells(RowId, ColID + 5).Value = ""
                                            '    ws.Cells(RowId, ColID + 6).Value = ""
                                            '    ws.Cells(RowId, ColID + 7).Value = ""
                                        End If
                                        '#########customer service
                                        RowId += 1
                                    Next
                                Else
                                    ws.Cells("A" & RowId).Value = shDr("shop_code")
                                    ws.Cells("B" & RowId).Value = shDr("shop_name_en")
                                    RowId += 1
                                End If
                                shDt.DefaultView.RowFilter = ""
                            Next


                            '#### Total by Region
                            ws.Cells(RowId, 1, RowId, 4).Value = "Total " & reDr("region_code")
                            ws.Cells(RowId, 1, RowId, 4).Merge = True
                            ColID = 5
                            Dim i As Integer = 0
                            CurrMonth = DateFrom
                            Do
                                Dim StartDate As Date = IIf(CurrMonth = DateFrom, DateFrom, New Date(CurrMonth.Year, CurrMonth.Month, 1))
                                Dim EndDate As Date = IIf(CurrMonth.ToString("MMyyyy") = DateTo.ToString("MMyyyy"), DateTo, New Date(CurrMonth.Year, CurrMonth.Month, CurrMonth.DaysInMonth(CurrMonth.Year, CurrMonth.Month)))
                                For Each sDr As DataRow In sDt.Rows
                                    Dim TotVeryGoodP As Double = 0
                                    Dim TotGoodP As Double = 0
                                    Dim TotAdjustP As Double = 0
                                    Dim TotTotalP As Double = 0
                                    If TotTotal(i) <> 0 Then
                                        TotVeryGoodP = (TotVeryGood(i) * 100) / TotTotal(i)
                                        TotGoodP = (TotGood(i) * 100) / TotTotal(i)
                                        TotAdjustP = (TotAdjust(i) * 100) / TotTotal(i)
                                        TotTotalP = (((TotVeryGood(i) + (TotGood(i) / 2)) * 100) / TotTotal(i))
                                    End If
                                    ws.Cells(RowId, ColID + 1).Value = TotVeryGood(i)
                                    ws.Cells(RowId, ColID + 2).Value = Math.Round(TotVeryGoodP) & "%"
                                    ws.Cells(RowId, ColID + 3).Value = TotGood(i)
                                    ws.Cells(RowId, ColID + 4).Value = Math.Round(TotGoodP) & "%"
                                    ws.Cells(RowId, ColID + 5).Value = TotAdjust(i)
                                    ws.Cells(RowId, ColID + 6).Value = Math.Round(TotAdjustP) & "%"
                                    ws.Cells(RowId, ColID + 7).Value = Math.Round(TotTotalP) & "%"
                                    ColID += 7

                                    i += 1
                                Next
                                CurrMonth = DateAdd(DateInterval.Month, 1, CurrMonth)
                            Loop While CurrMonth.ToString("yyyyMM") <= DateTo.ToString("yyyyMM")


                            'Total Overall
                            Dim TotVeryGoodP_Overall As Double = 0
                            Dim TotGoodP_Overall As Double = 0
                            Dim TotAdjustP_Overall As Double = 0
                            Dim TotTotalP_Overall As Double = 0
                            If TotTotal_Overall <> 0 Then
                                TotVeryGoodP_Overall = (TotVeryGood_Overall * 100) / TotTotal_Overall
                                TotGoodP_Overall = (TotGood_Overall * 100) / TotTotal_Overall
                                TotAdjustP_Overall = (TotAdjust_Overall * 100) / TotTotal_Overall
                                TotTotalP_Overall = (((TotVeryGood_Overall + (TotGood_Overall / 2)) * 100) / TotTotal_Overall)
                            End If
                            ws.Cells(RowId, ColID + 1).Value = TotVeryGood_Overall
                            ws.Cells(RowId, ColID + 2).Value = Math.Round(TotVeryGoodP_Overall) & "%"
                            ws.Cells(RowId, ColID + 3).Value = TotGood_Overall
                            ws.Cells(RowId, ColID + 4).Value = Math.Round(TotGoodP_Overall) & "%"
                            ws.Cells(RowId, ColID + 5).Value = TotAdjust_Overall
                            ws.Cells(RowId, ColID + 6).Value = Math.Round(TotAdjustP_Overall) & "%"
                            ws.Cells(RowId, ColID + 7).Value = Math.Round(TotTotalP_Overall) & "%"
                            ColID += 7


                            'Total Customer Service
                            Dim TotVeryGoodP_Customer_Service As Double = 0
                            Dim TotGoodP_Customer_Service As Double = 0
                            Dim TotAdjustP_Customer_Service As Double = 0
                            Dim TotTotalP_Customer_Service As Double = 0
                            If TotTotal_Customer_Service <> 0 Then
                                TotVeryGoodP_Customer_Service = (TotVeryGood_Customer_Service * 100) / TotTotal_Customer_Service
                                TotGoodP_Customer_Service = (TotGood_Customer_Service * 100) / TotTotal_Customer_Service
                                TotAdjustP_Customer_Service = (TotAdjust_Customer_Service * 100) / TotTotal_Customer_Service
                                TotTotalP_Customer_Service = (((TotVeryGood_Customer_Service + (TotGood_Customer_Service / 2)) * 100) / TotTotal_Customer_Service)
                            End If
                            ws.Cells(RowId, ColID + 1).Value = TotVeryGood_Customer_Service
                            ws.Cells(RowId, ColID + 2).Value = Math.Round(TotVeryGoodP_Customer_Service) & "%"
                            ws.Cells(RowId, ColID + 3).Value = TotGood_Customer_Service
                            ws.Cells(RowId, ColID + 4).Value = Math.Round(TotGoodP_Customer_Service) & "%"
                            ws.Cells(RowId, ColID + 5).Value = TotAdjust_Customer_Service
                            ws.Cells(RowId, ColID + 6).Value = Math.Round(TotAdjustP_Customer_Service) & "%"
                            ws.Cells(RowId, ColID + 7).Value = Math.Round(TotTotalP_Customer_Service) & "%"
                            'ColID += 7
                            '############################
                            ws.Cells(RowId, 1, RowId, ColID + 7).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            ws.Cells(RowId, 1, RowId, ColID + 7).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            ws.Cells(RowId, 1, RowId, ColID + 7).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("yellowgreen"))
                            RowId += 1
                        Next

                        '#### Grand Total
                        ws.Cells(RowId, 1, RowId, 4).Value = "Grand Total"
                        ws.Cells(RowId, 1, RowId, 4).Merge = True
                        ColID = 5
                        CurrMonth = DateFrom
                        Dim m As Integer = 0
                        Do
                            Dim StartDate As Date = IIf(CurrMonth = DateFrom, DateFrom, New Date(CurrMonth.Year, CurrMonth.Month, 1))
                            Dim EndDate As Date = IIf(CurrMonth.ToString("MMyyyy") = DateTo.ToString("MMyyyy"), DateTo, New Date(CurrMonth.Year, CurrMonth.Month, CurrMonth.DaysInMonth(CurrMonth.Year, CurrMonth.Month)))
                            For Each sDr As DataRow In sDt.Rows
                                Dim GTotVeryGoodP As Double = 0
                                Dim GTotGoodP As Double = 0
                                Dim GTotAdjustP As Double = 0
                                Dim GTotTotalP As Double = 0
                                If GTotTotal(m) <> 0 Then
                                    GTotVeryGoodP = (GTotVeryGood(m) * 100) / GTotTotal(m)
                                    GTotGoodP = (GTotGood(m) * 100) / GTotTotal(m)
                                    GTotAdjustP = (GTotAdjust(m) * 100) / GTotTotal(m)
                                    GTotTotalP = (((GTotVeryGood(m) + (GTotGood(m) / 2)) * 100) / GTotTotal(m))
                                End If
                                ws.Cells(RowId, ColID + 1).Value = GTotVeryGood(m)
                                ws.Cells(RowId, ColID + 2).Value = Math.Round(GTotVeryGoodP) & "%"
                                ws.Cells(RowId, ColID + 3).Value = GTotGood(m)
                                ws.Cells(RowId, ColID + 4).Value = Math.Round(GTotGoodP) & "%"
                                ws.Cells(RowId, ColID + 5).Value = GTotAdjust(m)
                                ws.Cells(RowId, ColID + 6).Value = Math.Round(GTotAdjustP) & "%"
                                ws.Cells(RowId, ColID + 7).Value = Math.Round(GTotTotalP) & "%"
                                ColID += 7
                                m += 1
                            Next
                            CurrMonth = DateAdd(DateInterval.Month, 1, CurrMonth)
                        Loop While CurrMonth.ToString("yyyyMM") <= DateTo.ToString("yyyyMM")

                        'Overall Grand Total
                        Dim GTotVeryGoodP_Overall As Double = 0
                        Dim GTotGoodP_Overall As Double = 0
                        Dim GTotAdjustP_Overall As Double = 0
                        Dim GTotTotalP_Overall As Double = 0
                        If GTotTotal_Overall <> 0 Then
                            GTotVeryGoodP_Overall = (GTotVeryGood_Overall * 100) / GTotTotal_Overall
                            GTotGoodP_Overall = (GTotGood_Overall * 100) / GTotTotal_Overall
                            GTotAdjustP_Overall = (GTotAdjust_Overall * 100) / GTotTotal_Overall
                            GTotTotalP_Overall = (((GTotVeryGood_Overall + (GTotGood_Overall / 2)) * 100) / GTotTotal_Overall)
                        End If
                        ws.Cells(RowId, ColID + 1).Value = GTotVeryGood_Overall
                        ws.Cells(RowId, ColID + 2).Value = Math.Round(GTotVeryGoodP_Overall) & "%"
                        ws.Cells(RowId, ColID + 3).Value = GTotGood_Overall
                        ws.Cells(RowId, ColID + 4).Value = Math.Round(GTotGoodP_Overall) & "%"
                        ws.Cells(RowId, ColID + 5).Value = GTotAdjust_Overall
                        ws.Cells(RowId, ColID + 6).Value = Math.Round(GTotAdjustP_Overall) & "%"
                        ws.Cells(RowId, ColID + 7).Value = Math.Round(GTotTotalP_Overall) & "%"
                        ColID += 7

                        'Customer Service Grand Total
                        Dim GTotVeryGoodP_Customer_Service As Double = 0
                        Dim GTotGoodP_Customer_Service As Double = 0
                        Dim GTotAdjustP_Customer_Service As Double = 0
                        Dim GTotTotalP_Customer_Service As Double = 0
                        If GTotTotal_Customer_Service <> 0 Then
                            GTotVeryGoodP_Customer_Service = (GTotVeryGood_Customer_Service * 100) / GTotTotal_Customer_Service
                            GTotGoodP_Customer_Service = (GTotGood_Customer_Service * 100) / GTotTotal_Customer_Service
                            GTotAdjustP_Customer_Service = (GTotAdjust_Customer_Service * 100) / GTotTotal_Customer_Service
                            GTotTotalP_Customer_Service = (((GTotVeryGood_Customer_Service + (GTotGood_Customer_Service / 2)) * 100) / GTotTotal_Customer_Service)
                        End If
                        ws.Cells(RowId, ColID + 1).Value = GTotVeryGood_Customer_Service
                        ws.Cells(RowId, ColID + 2).Value = Math.Round(GTotVeryGoodP_Customer_Service) & "%"
                        ws.Cells(RowId, ColID + 3).Value = GTotGood_Customer_Service
                        ws.Cells(RowId, ColID + 4).Value = Math.Round(GTotGoodP_Customer_Service) & "%"
                        ws.Cells(RowId, ColID + 5).Value = GTotAdjust_Customer_Service
                        ws.Cells(RowId, ColID + 6).Value = Math.Round(GTotAdjustP_Customer_Service) & "%"
                        ws.Cells(RowId, ColID + 7).Value = Math.Round(GTotTotalP_Customer_Service) & "%"
                        '######################################
                        ws.Cells(RowId, 1, RowId, ColID + 7).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        ws.Cells(RowId, 1, RowId, ColID + 7).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        ws.Cells(RowId, 1, RowId, ColID + 7).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("Orange"))
                    End If
                    '## End Datarow


                    Using RowContent As ExcelRange = ws.Cells(ServiceHeaderRow + 1, 1, RowId, ColID + 7)
                        RowContent.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        RowContent.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        RowContent.AutoFitColumns()
                    End Using

                    '//Write it back to the client
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;  filename=" & Replace(lblReportName.Text, " ", "_") & "_" & DateTime.Now.ToString("yyyyMMddhhmmssfff") & ".xlsx")
                    Response.BinaryWrite(ep.GetAsByteArray())
                    Response.End()
                    Response.Flush()
                End Using
            Else
                lblMessage.Text = "Not Found"
                Exit Sub
            End If
            sDt.Dispose()
            
        Catch ex As Exception
            lblMessage.Text = ex.ToString()
        End Try

    End Sub

End Class
