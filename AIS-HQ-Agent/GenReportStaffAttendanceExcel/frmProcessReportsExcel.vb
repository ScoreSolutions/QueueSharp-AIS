Imports OfficeOpenXml
Imports System.Data
Imports System.IO
Imports System.Windows.Forms

Public Class frmProcessReportsExcel
    Dim IniFileName As String = Application.StartupPath & "\config.ini"

    Private Sub frmProcessReportsExcel_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim ini As New IniReader(IniFileName)
        ini.Section = "MailSetting"
        txtSubject.Text = ini.ReadString("MailSubject") & "_" & dtDateTo.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        txtTo.Text = ini.ReadString("MailTo")
        txtCC.Text = ini.ReadString("MailCC")
        ini = Nothing
        Dim line As String = ""
        Try
            Using sr As StreamReader = New StreamReader("MailContent.txt")
                line = sr.ReadToEnd
            End Using
        Catch
        End Try

        txtMailContent.Text = line
        FillWithMonths(chkMonth, Today.Month - 1)
        txtYear.Text = Today.ToString("yyyy")
        rdDialy_CheckedChanged(sender, e)

        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                Dim ReportDate As DateTime = DateAdd(DateInterval.Day, -1, DateTime.Now)
                dtDateFrom.Value = ReportDate 'New Date(ReportDate.Year, ReportDate.Month, 1)
                dtDateTo.Value = ReportDate
                chkSendEmail.Checked = True

                txtSubject.Text = "Ad hoc Staff Attendance Report_" & ReportDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                'txtTo.Text = "suchart@scoresolutions.co.th"

                txtMailContent.Text = "Dear All" & vbNewLine & vbNewLine
                txtMailContent.Text += " QIS Staff Attendance Daily Report." & ReportDate.ToString("yyyy-MM-dd") & vbNewLine & vbNewLine & vbNewLine
                txtMailContent.Text += "Best Regards," & vbNewLine
                txtMailContent.Text += "[Auto Mail]"

                ProcReportsDialy()
                If Now.Day = 1 Then
                    txtSubject.Text = "Ad hoc Staff Attendance Report By Month_" & ReportDate.ToString("MM-yyyy", New Globalization.CultureInfo("en-US"))
                    txtMailContent.Text = "Dear All" & vbNewLine & vbNewLine
                    txtMailContent.Text += " QIS Staff Attendance Monthly Report." & ReportDate.ToString("MMM yyyy") & vbNewLine & vbNewLine & vbNewLine
                    txtMailContent.Text += "Best Regards," & vbNewLine
                    txtMailContent.Text += "[Auto Mail]"

                    rdMonth.Checked = True
                    chkMonth.SelectedIndex = ReportDate.Month - 1
                    txtYear.Text = ReportDate.Year + 543
                    ProcReportsMonth()
                End If

                Application.Exit()
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
            Engine.Common.FunctionEng.SaveErrorLog("frmProcessReportsExcel.Shown", "Exception :" & ex.Message & vbNewLine & ex.StackTrace, "\ErrorLog\", "GenReportStaffAttendanceExcel")
        End Try

    End Sub

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

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Me.Enabled = False
        If chkSendEmail.Checked = True Then
            If ValidateEmail() = False Then
                Me.Enabled = True
                Exit Sub
            End If
        End If


        If rdDialy.Checked Then
            ProcReportsDialy()
        Else
            ProcReportsMonth()
        End If

        Dim subject() As String
        subject = txtSubject.Text.Split("_")
        Dim subjName As String = ""
        If subject.Length > 0 Then
            subjName = subject(0)
        End If

        Dim ini As New IniReader(IniFileName)
        ini.Section = "MailSetting"
        ini.Write("MailSubject", subjName)
        ini.Write("MailTo", txtTo.Text.Trim)
        ini.Write("MailCC", txtCC.Text.Trim)
        ini = Nothing

        Dim FILE_NAME As String = Application.StartupPath & "\MailContent.txt"
        If System.IO.File.Exists(FILE_NAME) = True Then
            System.IO.File.Delete(Application.StartupPath & "\MailContent.txt")
        End If
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
        objWriter.Write(txtMailContent.Text)
        objWriter.Close()

        Me.Enabled = True
        MessageBox.Show("Success!!!!")
    End Sub

    Private Sub rdDialy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdDialy.CheckedChanged
        SetEnable(rdDialy.Checked)

        Dim subject() As String
        subject = txtSubject.Text.Split("_")
        Dim subjName As String = ""
        If subject.Length > 0 Then
            subjName = subject(0)
        End If
        txtSubject.Text = subjName & "_" & dtDateTo.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
    End Sub

    Private Sub rdMonth_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdMonth.CheckedChanged
        SetEnable(Not rdMonth.Checked)

        Dim subject() As String
        subject = txtSubject.Text.Split("_")
        Dim subjName As String = ""
        If subject.Length > 0 Then
            subjName = subject(0)
        End If
        txtSubject.Text = subjName & "_" & dtDateTo.Value.ToString("MM-yyyy", New Globalization.CultureInfo("en-US"))
    End Sub

    Private Sub chkMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMonth.SelectedIndexChanged
        Dim subject() As String
        subject = txtSubject.Text.Split("_")
        Dim subjName As String = ""
        If subject.Length > 0 Then
            subjName = subject(0)
        End If
        txtSubject.Text = subjName & "_" & dtDateTo.Value.ToString("MM-yyyy", New Globalization.CultureInfo("en-US"))
    End Sub

#Region "__Sub&Function"

    Sub SetEnable(ByVal IsEnable As Boolean)
        dtDateFrom.Enabled = IsEnable
        dtDateTo.Enabled = IsEnable
        chkMonth.Enabled = Not IsEnable
        txtYear.Enabled = Not IsEnable
        chkSendEmail.Enabled = IsEnable
    End Sub

    Private Sub ProcReportsDialy()

        Dim DateFrom As String = dtDateFrom.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        Dim DateTo As String = dtDateTo.Value.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))

        Dim CurrDate As Date = dtDateFrom.Value
        Do
            Dim ExcelFile As String = "Ad_hoc_Staff_Attendace_" & CurrDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            Dim shdt As New DataTable
            '==GetShop
            shdt = GetDataAllShop()

            ''==LoadCube
            LoadCubeByDate(shdt, CurrDate)

            '==GetData&CreateExcel
            Dim para As New CenParaDB.ReportCriteria.StaffAttendancePara
            para.DateFrom = CurrDate
            para.DateTo = CurrDate
            Dim dt As DataTable = GetReportDataByDate(para)
            CreateExcelFileDaily(ExcelFile, dt, CurrDate)
            dt.Dispose()

            CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
        Loop While CurrDate <= dtDateTo.Value

    End Sub

    Private Sub ProcReportsMonth()
        If Trim(txtYear.Text) = "" AndAlso CInt(txtYear.Text) > Today.Year Then
            MessageBox.Show("กรุณาเลือกปีให้ถูกต้อง")
            Exit Sub
        End If

        Dim CurrMonth As String = chkMonth.SelectedIndex + 1
        Dim CurrYear As String = CInt(txtYear.Text) - 543
        Dim ExcelFile As String = "Ad_hoc_Staff_Attendace_By_Month_" & CurrYear & "_" & CurrMonth.PadLeft(2, "0")

        ''==GetShop
        Dim shdt As New DataTable
        shdt = GetDataAllShop()

        'Load Cube
        Dim vDate As New Date(CurrYear, CurrMonth, 1)
        LoadCubeByMonth(shdt, vDate)

        ''==GetData&CreateExcel
        Dim para As New CenParaDB.ReportCriteria.StaffAttendancePara
        para.YearFrom = CurrYear
        para.MonthFrom = CurrMonth
        Dim dt As DataTable = GetReportDataByMonth(para)
        CreateExcelFileMonth(ExcelFile, dt)
        dt.Dispose()
    End Sub

    Function GetDataAllShop() As DataTable
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

        Dim shSql As String = "select s.id,s.shop_db_name,s.shop_db_userid, s.shop_db_server, s.shop_db_pwd, "
        shSql += " s.shop_name_en, r.location_group "
        shSql += " from TB_SHOP s"
        shSql += " inner join TB_REGION r on r.id=s.region_id"
        shSql += " where s.active='Y'"
        shSql += " order by  s.shop_name_en desc"
        Dim shDt As New System.Data.DataTable
        shDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(shSql)
        Return shDt
    End Function

    Sub LoadCubeByDate(ByVal shDt As DataTable, ByVal service_date As Date)
        For Each sDr As DataRow In shDt.Rows
            Dim eng As New Engine.Reports.ReportsStaffAttendanceENG
            eng.ProcReportByDate(service_date, sDr("ID"), lblTime)
            eng = Nothing
        Next
    End Sub

    Sub LoadCubeByMonth(ByVal shDt As DataTable, ByVal service_date As Date)
        For Each sDr As DataRow In shDt.Rows
            Dim eng As New Engine.Reports.ReportsStaffAttendanceENG
            eng.ProcReportByMonth(service_date, sDr("ID"), lblTime)
            eng = Nothing
        Next
    End Sub

    Function GetReportDataByDate(ByVal InputPara As CenParaDB.ReportCriteria.StaffAttendancePara) As DataTable
        Dim dt As New DataTable
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim sql As String = "select s.shop_abb,rg.region_code, r.* "
            sql += " from tb_rep_staff_attendance r"
            sql += " inner join tb_shop s on s.id=r.shop_id"
            sql += " left join tb_region rg on s.region_id = rg.id "
            sql += " where UPPER(r.username) <> 'ADMIN'"
            sql += " and convert(varchar(10),r.service_date, 120) between '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' "
            sql += " order by s.shop_abb desc, r.staff_name,username"
            Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceCenLinqDB
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
        End If

        Return dt
    End Function

    Public Function GetReportDataByMonth(ByVal InputPara As CenParaDB.ReportCriteria.StaffAttendancePara) As DataTable
        Dim dt As New DataTable
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim sql As String = ""
            sql += " select r.shop_id,r.shop_name_en, s.shop_abb,r.user_code,r.username,r.staff_name,re.region_code," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.log_in))) log_in," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.log_out))) log_out," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.total_time))) total_time, " & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.productivity))) productivity, " & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.non_productivity))) non_productivity," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(case when dbo.fn_getSecFromTimeFormat(r.total_time)>(450*60) then dbo.fn_getSecFromTimeFormat(r.total_time)-(450*60) else 0 end)) est_ot," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.service_time))) service_time," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.prod_learning))) prod_learning," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.prod_stand_by))) prod_stand_by," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.prod_brief))) prod_brief," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.prod_warp_up))) prod_warp_up," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.prod_consult))) prod_consult," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.prod_other))) prod_other," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_lunch))) nprod_lunch," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_leave))) nprod_leave," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_change_counter))) nprod_change_counter," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_home))) nprod_home," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_mini_break))) nprod_mini_break," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_restroom))) nprod_restroom," & vbNewLine
            sql += " dbo.fn_getTimeFormatFromSec(sum(dbo.fn_getSecFromTimeFormat(r.nprod_other))) nprod_other" & vbNewLine
            sql += " from TB_REP_STAFF_ATTENDANCE r" & vbNewLine
            sql += " inner join TB_SHOP s on s.id=r.shop_id" & vbNewLine
            sql += " inner join TB_REGION re on re.id=s.region_id" & vbNewLine
            sql += " where convert(varchar(6),r.service_date,112) = '" & InputPara.YearFrom & InputPara.MonthFrom.ToString.PadLeft(2, "0") & "'" & vbNewLine
            sql += " group by r.shop_id,r.shop_name_en, s.shop_abb,r.user_code,r.username,r.staff_name,re.region_code" & vbNewLine
            sql += " order by s.shop_abb desc, r.staff_name"
            Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceMonthCenLinqDB
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
        End If

        Return dt
    End Function

    Sub SetCellsColor(ByVal sh As ExcelWorksheet, ByVal row As Integer, ByVal column As Integer)
        Using CellsColor As ExcelRange = sh.Cells(row, column)
            CellsColor.Style.Font.Bold = True
            CellsColor.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            CellsColor.Style.Fill.BackgroundColor.SetColor(Color.YellowGreen)
            CellsColor.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            CellsColor.Style.Font.Color.SetColor(Color.White)
        End Using
    End Sub

    Sub SetRowTotalStyle(ByVal sh As ExcelWorksheet, ByVal RowIndex As Integer, ByVal LastCol As String)
        Using RowTotalStyle As ExcelRange = sh.Cells("A" & RowIndex & ":" & LastCol & RowIndex)
            RowTotalStyle.Style.Font.Bold = True
            RowTotalStyle.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            RowTotalStyle.Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        End Using
    End Sub


    Private Sub CreateExcelFileDaily(ByVal excelFile As String, ByVal TmpDt As System.Data.DataTable, ByVal ServiceDate As DateTime)
        'Generate Excel File
        If TmpDt.Rows.Count > 0 Then
            Dim sDt As New System.Data.DataTable
            sDt = TmpDt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "Service_Date", "shop_abb")
            Using ep As New ExcelPackage
                For Each sDr As DataRow In sDt.Rows
                    Try
                        Dim TotTotalTime As Long = 0
                        Dim TotProductivity As Long = 0
                        Dim TotNonProductivity As Long = 0
                        Dim TotE As Long = 0
                        Dim TotS As Long = 0
                        Dim TotLearning As Long = 0
                        Dim TotStandBy As Long = 0
                        Dim TotBrief As Long = 0
                        Dim TotWarpUp As Long = 0
                        Dim TotConsult As Long = 0
                        Dim TotOtherProd As Long = 0
                        Dim TotLunch As Long = 0
                        Dim TotLeave As Long = 0
                        Dim TotChangeCounter As Long = 0
                        Dim TotHome As Long = 0
                        Dim TotMiniBreak As Long = 0
                        Dim TotRestRoom As Long = 0
                        Dim TotOtherNonProd As Long = 0

                        Dim sh As ExcelWorksheet = ep.Workbook.Worksheets.Add(Replace(sDr("shop_abb"), "/", ""))
                        sh.TabColor = System.Drawing.ColorTranslator.FromWin32(RGB(29, 213, 23))
                        SetCellsColor(sh, 1, 1)
                        sh.Cells("A1").Value = "Region"

                        SetCellsColor(sh, 1, 2)
                        sh.Cells("B1").Value = "Shop Name"

                        SetCellsColor(sh, 1, 3)
                        sh.Cells("C1").Value = "Date"

                        SetCellsColor(sh, 1, 4)
                        sh.Cells("D1").Value = "EmpID"

                        SetCellsColor(sh, 1, 5)
                        sh.Cells("E1").Value = "User Name"

                        SetCellsColor(sh, 1, 6)
                        sh.Cells("F1").Value = "Staff Name"

                        SetCellsColor(sh, 1, 7)
                        sh.Cells("G1").Value = "Log-In"

                        SetCellsColor(sh, 1, 8)
                        sh.Cells("H1").Value = "Log-Out"

                        SetCellsColor(sh, 1, 9)
                        sh.Cells("I1").Value = "Total Time"

                        SetCellsColor(sh, 1, 10)
                        sh.Cells("J1").Value = "Productivity"

                        SetCellsColor(sh, 1, 11)
                        sh.Cells("K1").Value = "Non Productivity"

                        SetCellsColor(sh, 1, 12)
                        sh.Cells("L1").Value = "Estimated OT"

                        SetCellsColor(sh, 1, 13)
                        sh.Cells("M1").Value = "Service Time"

                        SetCellsColor(sh, 1, 14)
                        sh.Cells("N1").Value = "Productivity Learning"

                        SetCellsColor(sh, 1, 15)
                        sh.Cells("O1").Value = "Productivity Stand by"

                        SetCellsColor(sh, 1, 16)
                        sh.Cells("P1").Value = "Productivity Brief"

                        SetCellsColor(sh, 1, 17)
                        sh.Cells("Q1").Value = "Productivity Wrap up"

                        SetCellsColor(sh, 1, 18)
                        sh.Cells("R1").Value = "Productivity Consult"

                        SetCellsColor(sh, 1, 19)
                        sh.Cells("S1").Value = "Other Productivity"

                        SetCellsColor(sh, 1, 20)
                        sh.Cells("T1").Value = "Non-Productivity Lunch"

                        SetCellsColor(sh, 1, 21)
                        sh.Cells("U1").Value = "Non-Productivity Leave"

                        SetCellsColor(sh, 1, 22)
                        sh.Cells("V1").Value = "Non-Productivity Change Counter"

                        SetCellsColor(sh, 1, 23)
                        sh.Cells("W1").Value = "Non-Productivity Home"

                        SetCellsColor(sh, 1, 24)
                        sh.Cells("X1").Value = "Non-Productivity Mini Break"

                        SetCellsColor(sh, 1, 25)
                        sh.Cells("Y1").Value = "Non-Productivity Rest Room"

                        SetCellsColor(sh, 1, 26)
                        sh.Cells("Z1").Value = "Other Non-Productivity"

                        SetCellsColor(sh, 1, 27)
                        sh.Cells("AA1").Value = "% Productivity"

                        SetCellsColor(sh, 1, 28)
                        sh.Cells("AB1").Value = "% Non-productive"

                        TmpDt.DefaultView.RowFilter = "service_date='" & sDr("service_date") & "' and shop_id='" & sDr("shop_id") & "'"
                        TmpDt.DefaultView.Sort = "shop_name_en,service_date,staff_name"
                        Dim i As Integer = 2
                        For Each dr As DataRowView In TmpDt.DefaultView
                            Try
                                sh.Cells("A" & i).Value = dr("Region_Code")
                                sh.Cells("B" & i).Value = dr("Shop_Name_EN")
                                sh.Cells("C" & i).Value = Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                sh.Cells("D" & i).Value = dr("user_code")
                                sh.Cells("D" & i).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                                sh.Cells("E" & i).Value = dr("username")
                                sh.Cells("F" & i).Value = dr("staff_name")
                                sh.Cells("G" & i).Value = dr("log_in")
                                sh.Cells("H" & i).Value = dr("log_out")
                                sh.Cells("I" & i).Value = dr("total_time")
                                sh.Cells("J" & i).Value = GetFormatTimeFromSec(GetSecFromTimeFormat(dr("productivity")))
                                sh.Cells("K" & i).Value = dr("non_productivity")
                                If GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
                                    sh.Cells("L" & i).Value = GetFormatTimeFromSec(GetSecFromTimeFormat(dr("total_time")) - (450 * 60))
                                Else
                                    sh.Cells("L" & i).Value = "00:00:00"
                                End If

                                sh.Cells("M" & i).Value = dr("service_time")
                                sh.Cells("N" & i).Value = dr("prod_learning")
                                If GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
                                    TotLearning += GetSecFromTimeFormat(dr("prod_learning"))
                                End If

                                sh.Cells("O" & i).Value = dr("prod_stand_by")
                                If GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
                                    TotStandBy += GetSecFromTimeFormat(dr("prod_stand_by"))
                                End If

                                sh.Cells("P" & i).Value = dr("prod_brief")
                                If GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
                                    TotBrief += GetSecFromTimeFormat(dr("prod_brief"))
                                End If

                                sh.Cells("Q" & i).Value = dr("prod_warp_up")
                                If GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
                                    TotWarpUp += GetSecFromTimeFormat(dr("prod_warp_up"))
                                End If

                                sh.Cells("R" & i).Value = dr("prod_consult")
                                If GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
                                    TotConsult += GetSecFromTimeFormat(dr("prod_consult"))
                                End If

                                sh.Cells("S" & i).Value = dr("prod_other")
                                If GetSecFromTimeFormat(dr("prod_other")) > 0 Then
                                    TotOtherProd += GetSecFromTimeFormat(dr("prod_other"))
                                End If

                                sh.Cells("T" & i).Value = dr("nprod_lunch")
                                If GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
                                    TotLunch += GetSecFromTimeFormat(dr("nprod_lunch"))
                                End If

                                sh.Cells("U" & i).Value = dr("nprod_leave")
                                If GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
                                    TotLeave += GetSecFromTimeFormat(dr("nprod_leave"))
                                End If

                                sh.Cells("V" & i).Value = dr("nprod_change_counter")
                                If GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
                                    TotChangeCounter += GetSecFromTimeFormat(dr("nprod_change_counter"))
                                End If

                                sh.Cells("W" & i).Value = dr("nprod_home")
                                If GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
                                    TotHome += GetSecFromTimeFormat(dr("nprod_home"))
                                End If

                                sh.Cells("X" & i).Value = dr("nprod_mini_break")
                                If GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
                                    TotMiniBreak += GetSecFromTimeFormat(dr("nprod_mini_break"))
                                End If

                                sh.Cells("Y" & i).Value = dr("nprod_restroom")
                                If GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
                                    TotRestRoom += GetSecFromTimeFormat(dr("nprod_restroom"))
                                End If

                                sh.Cells("Z" & i).Value = dr("nprod_other")
                                If GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
                                    TotOtherNonProd += GetSecFromTimeFormat(dr("nprod_other"))
                                End If

                                Dim _total_time As Integer = GetSecFromTimeFormat(dr("total_time"))
                                If _total_time = 0 Then
                                    _total_time = 1
                                End If
                                If GetSecFromTimeFormat(dr("productivity")) > 0 Then
                                    sh.Cells("AA" & i).Value = CInt((GetSecFromTimeFormat(dr("productivity")) / _total_time) * 100) & "%"
                                Else
                                    sh.Cells("AA" & i).Value = "0%"
                                End If

                                If GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
                                    sh.Cells("AB" & i).Value = CInt((GetSecFromTimeFormat(dr("non_productivity")) / _total_time) * 100) & "%"
                                Else
                                    sh.Cells("AB" & i).Value = "0%"
                                End If

                                If Convert.IsDBNull(dr("total_time")) = False Then
                                    TotTotalTime += GetSecFromTimeFormat(dr("total_time"))
                                End If
                                If GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
                                    TotE += GetSecFromTimeFormat(dr("total_time")) - (450 * 60)
                                End If

                                TotS += GetSecFromTimeFormat(dr("service_time"))
                                TotProductivity += GetSecFromTimeFormat(dr("productivity"))
                                TotNonProductivity += GetSecFromTimeFormat(dr("non_productivity"))
                            Catch ex As Exception
                                Engine.Common.FunctionEng.CreateLogFile("Exception 2 :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "frmProcessReportsExcel.CreateExcelFileDaily")
                            End Try
                            i += 1
                        Next

                        ''== Start SubTotal
                        SetRowTotalStyle(sh, i, "AB")
                        If TmpDt.Rows.Count > 0 Then
                            sh.Cells("A" & i & ":H" & i).Merge = True
                            sh.Cells("A" & i).Value = "Sub Total"
                            sh.Cells("A" & i).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

                            sh.Cells("B" & i).Value = sDr("Shop_name_en")
                            sh.Cells("C" & i).Value = " " & ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                            If TotTotalTime = 0 Then
                                sh.Cells("I" & i).Value = "00:00:00"
                            Else
                                sh.Cells("I" & i).Value = GetFormatTimeFromSec(TotTotalTime).ToString
                            End If
                            If TotProductivity = 0 Then
                                sh.Cells("J" & i).Value = "00:00:00"
                            Else
                                sh.Cells("J" & i).Value = GetFormatTimeFromSec(TotProductivity).ToString
                            End If
                            If TotNonProductivity = 0 Then
                                sh.Cells("K" & i).Value = "00:00:00"
                            Else
                                sh.Cells("K" & i).Value = GetFormatTimeFromSec(TotNonProductivity).ToString
                            End If
                            If TotE = 0 Then
                                sh.Cells("L" & i).Value = "00:00:00"
                            Else
                                sh.Cells("L" & i).Value = GetFormatTimeFromSec(TotE).ToString
                            End If
                            If TotS = 0 Then
                                sh.Cells("M" & i).Value = "00:00:00"
                            Else
                                sh.Cells("M" & i).Value = GetFormatTimeFromSec(TotS).ToString
                            End If
                            If TotLearning = 0 Then
                                sh.Cells("N" & i).Value = "00:00:00"
                            Else
                                sh.Cells("N" & i).Value = GetFormatTimeFromSec(TotLearning).ToString
                            End If
                            If TotStandBy = 0 Then
                                sh.Cells("O" & i).Value = "00:00:00"
                            Else
                                sh.Cells("O" & i).Value = GetFormatTimeFromSec(TotStandBy).ToString
                            End If
                            If TotBrief = 0 Then
                                sh.Cells("P" & i).Value = "00:00:00"
                            Else
                                sh.Cells("P" & i).Value = GetFormatTimeFromSec(TotBrief).ToString
                            End If
                            If TotWarpUp = 0 Then
                                sh.Cells("Q" & i).Value = "00:00:00"
                            Else
                                sh.Cells("Q" & i).Value = GetFormatTimeFromSec(TotWarpUp).ToString
                            End If
                            If TotConsult = 0 Then
                                sh.Cells("R" & i).Value = "00:00:00"
                            Else
                                sh.Cells("R" & i).Value = GetFormatTimeFromSec(TotConsult).ToString
                            End If
                            If TotOtherProd = 0 Then
                                sh.Cells("S" & i).Value = "00:00:00"
                            Else
                                sh.Cells("S" & i).Value = GetFormatTimeFromSec(TotOtherProd).ToString
                            End If
                            If TotLunch = 0 Then
                                sh.Cells("T" & i).Value = "00:00:00"
                            Else
                                sh.Cells("T" & i).Value = GetFormatTimeFromSec(TotLunch).ToString
                            End If
                            If TotLeave = 0 Then
                                sh.Cells("U" & i).Value = "00:00:00"
                            Else
                                sh.Cells("U" & i).Value = GetFormatTimeFromSec(TotLeave).ToString
                            End If
                            If TotChangeCounter = 0 Then
                                sh.Cells("V" & i).Value = "00:00:00"
                            Else
                                sh.Cells("V" & i).Value = GetFormatTimeFromSec(TotChangeCounter).ToString
                            End If
                            If TotHome = 0 Then
                                sh.Cells("W" & i).Value = "00:00:00"
                            Else
                                sh.Cells("W" & i).Value = GetFormatTimeFromSec(TotHome).ToString
                            End If
                            If TotMiniBreak = 0 Then
                                sh.Cells("X" & i).Value = "00:00:00"
                            Else
                                sh.Cells("X" & i).Value = GetFormatTimeFromSec(TotMiniBreak).ToString
                            End If
                            If TotRestRoom = 0 Then
                                sh.Cells("Y" & i).Value = "00:00:00"
                            Else
                                sh.Cells("Y" & i).Value = GetFormatTimeFromSec(TotRestRoom).ToString
                            End If
                            If TotOtherNonProd = 0 Then
                                sh.Cells("Z" & i).Value = "00:00:00"
                            Else
                                sh.Cells("Z" & i).Value = GetFormatTimeFromSec(TotOtherNonProd).ToString
                            End If

                            If TotTotalTime = 0 Then
                                sh.Cells("AA" & i).Value = "0%"
                                sh.Cells("AB" & i).Value = "0%"
                            Else
                                sh.Cells("AA" & i).Value = CInt((TotProductivity / TotTotalTime) * 100).ToString & "%"
                                sh.Cells("AB" & i).Value = CInt((TotNonProductivity / TotTotalTime) * 100).ToString & "%"
                            End If
                        End If
                        '== End SubTotal

                        Using shRange As ExcelRange = sh.Cells("A1:AB" & i)
                            shRange.AutoFitColumns()

                            shRange.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                            shRange.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                            shRange.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                            shRange.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        End Using
                    Catch ex As Exception
                        Engine.Common.FunctionEng.CreateLogFile("Exception 2 :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "frmProcessReportsExcel.CreateExcelFileDaily")
                    End Try
                Next

                Dim vMonth As String = ServiceDate.ToString("yyyy") & "\" & ServiceDate.ToString("yyyyMM")
                Dim FilePath As String = "D:\ReportExcel\" & vMonth
                If Directory.Exists(FilePath) = False Then
                    Directory.CreateDirectory(FilePath)
                End If

                Dim FileName As String = FilePath & "\" & excelFile & ".xlsx"
                If File.Exists(FileName) = True Then
                    File.Delete(FileName)
                End If

                Dim b() As Byte = ep.GetAsByteArray
                File.WriteAllBytes(FileName, b)

                If chkSendEmail.Checked = True Then
                    If File.Exists(FileName) = True Then
                        SendSqlEmail(FileName)
                    End If
                End If
            End Using
        End If
    End Sub

    'Private Sub CreateExcelFileDaily(ByVal excelFile As String, ByVal TmpDt As System.Data.DataTable, ByVal ServiceDate As DateTime)
    '    'Generate Excel File
    '    If TmpDt.Rows.Count > 0 Then
    '        'Dim chkApp As New Excel.Application
    '        'Dim dailyReportBook As Excel.Workbook = chkApp.Workbooks.Add
    '        Dim sDt As New System.Data.DataTable
    '        sDt = TmpDt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "Service_Date", "shop_abb")

    '        'Dim style As Excel.Style = dailyReportBook.Application.ActiveWorkbook.Styles.Add("NewStyle")
    '        'Dim styleT As Excel.Style = dailyReportBook.Application.ActiveWorkbook.Styles.Add("TotalStyle")
    '        'Dim sh As Excel.Worksheet

    '        Using ep As New ExcelPackage
    '            '## Start Criterai ##


    '            For Each sDr As DataRow In sDt.Rows

    '                Dim TotTotalTime As Long = 0
    '                Dim TotProductivity As Long = 0
    '                Dim TotNonProductivity As Long = 0
    '                Dim TotE As Long = 0
    '                Dim TotS As Long = 0
    '                Dim TotLearning As Long = 0
    '                Dim TotStandBy As Long = 0
    '                Dim TotBrief As Long = 0
    '                Dim TotWarpUp As Long = 0
    '                Dim TotConsult As Long = 0
    '                Dim TotOtherProd As Long = 0
    '                Dim TotLunch As Long = 0
    '                Dim TotLeave As Long = 0
    '                Dim TotChangeCounter As Long = 0
    '                Dim TotHome As Long = 0
    '                Dim TotMiniBreak As Long = 0
    '                Dim TotRestRoom As Long = 0
    '                Dim TotOtherNonProd As Long = 0

    '                Dim sh As ExcelWorksheet = ep.Workbook.Worksheets.Add("")

    '                sh = dailyReportBook.Worksheets.Add
    '                sh.Name = Replace(sDr("shop_abb"), "/", "")
    '                sh.Tab.Color = RGB(29, 213, 23)

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 1)
    '                sh.Range("A1").Value = "Region"
    '                sh.Range("A1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 2)
    '                sh.Range("B1").Value = "Shop Name"
    '                sh.Range("B1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 3)
    '                sh.Range("C1").Value = "Date"
    '                sh.Range("C1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 4)
    '                sh.Range("D1").Value = "EmpID"
    '                sh.Range("D1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 5)
    '                sh.Range("E1").Value = "User Name"
    '                sh.Range("E1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 6)
    '                sh.Range("F1").Value = "Staff Name"
    '                sh.Range("F1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 7)
    '                sh.Range("G1").Value = "Log-In"
    '                sh.Range("G1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 8)
    '                sh.Range("H1").Value = "Log-Out"
    '                sh.Range("H1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 9)
    '                sh.Range("I1").Value = "Total Time"
    '                sh.Range("I1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 10)
    '                sh.Range("J1").Value = "Productivity"
    '                sh.Range("J1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 11)
    '                sh.Range("K1").Value = "Non Productivity"
    '                sh.Range("K1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 12)
    '                sh.Range("L1").Value = "Estimated OT"
    '                sh.Range("L1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 13)
    '                sh.Range("M1").Value = "Service Time"
    '                sh.Range("M1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 14)
    '                sh.Range("N1").Value = "Productivity Learning"
    '                sh.Range("N1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 15)
    '                sh.Range("O1").Value = "Productivity Stand by"
    '                sh.Range("O1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 16)
    '                sh.Range("P1").Value = "Productivity Brief"
    '                sh.Range("P1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 17)
    '                sh.Range("Q1").Value = "Productivity Wrap up"
    '                sh.Range("Q1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 18)
    '                sh.Range("R1").Value = "Productivity Consult"
    '                sh.Range("R1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 19)
    '                sh.Range("S1").Value = "Other Productivity"
    '                sh.Range("S1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 20)
    '                sh.Range("T1").Value = "Non-Productivity Lunch"
    '                sh.Range("T1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 21)
    '                sh.Range("U1").Value = "Non-Productivity Leave"
    '                sh.Range("U1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 22)
    '                sh.Range("V1").Value = "Non-Productivity Change Counter"
    '                sh.Range("V1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 23)
    '                sh.Range("W1").Value = "Non-Productivity Home"
    '                sh.Range("W1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 24)
    '                sh.Range("X1").Value = "Non-Productivity Mini Break"
    '                sh.Range("X1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 25)
    '                sh.Range("Y1").Value = "Non-Productivity Rest Room"
    '                sh.Range("Y1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 26)
    '                sh.Range("Z1").Value = "Other Non-Productivity"
    '                sh.Range("Z1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 27)
    '                sh.Range("AA1").Value = "% Productivity"
    '                sh.Range("AA1").Borders.Weight = 2

    '                SetCellsColor(Style, dailyReportBook, sh, 1, 28)
    '                sh.Range("AB1").Value = "% Non-productive"
    '                sh.Range("AB1").Borders.Weight = 2


    '                TmpDt.DefaultView.RowFilter = "service_date='" & sDr("service_date") & "' and shop_id='" & sDr("shop_id") & "'"
    '                TmpDt.DefaultView.Sort = "shop_name_en,service_date,staff_name"
    '                Dim i As Integer = 2
    '                For Each dr As DataRowView In TmpDt.DefaultView
    '                    sh.Range("A" & i).Value = dr("Region_Code")
    '                    sh.Range("B" & i).Value = dr("Shop_Name_EN")
    '                    sh.Range("C" & i).Value = " " & Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
    '                    sh.Range("D" & i).Value = dr("user_code")
    '                    sh.Range("D" & i).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
    '                    sh.Range("E" & i).Value = dr("username")
    '                    sh.Range("F" & i).Value = dr("staff_name")
    '                    sh.Range("G" & i).Value = dr("log_in")
    '                    sh.Range("H" & i).Value = dr("log_out")
    '                    sh.Range("I" & i).Value = dr("total_time")
    '                    sh.Range("J" & i).Value = GetFormatTimeFromSec(GetSecFromTimeFormat(dr("productivity")))
    '                    sh.Range("K" & i).Value = dr("non_productivity")
    '                    If GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
    '                        sh.Range("L" & i).Value = GetFormatTimeFromSec(GetSecFromTimeFormat(dr("total_time")) - (450 * 60))
    '                    Else
    '                        sh.Range("L" & i).Value = "00:00:00"
    '                    End If

    '                    sh.Range("M" & i).Value = dr("service_time")
    '                    sh.Range("N" & i).Value = dr("prod_learning")
    '                    If GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
    '                        TotLearning += GetSecFromTimeFormat(dr("prod_learning"))
    '                    End If

    '                    sh.Range("O" & i).Value = dr("prod_stand_by")
    '                    If GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
    '                        TotStandBy += GetSecFromTimeFormat(dr("prod_stand_by"))
    '                    End If

    '                    sh.Range("P" & i).Value = dr("prod_brief")
    '                    If GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
    '                        TotBrief += GetSecFromTimeFormat(dr("prod_brief"))
    '                    End If

    '                    sh.Range("Q" & i).Value = dr("prod_warp_up")
    '                    If GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
    '                        TotWarpUp += GetSecFromTimeFormat(dr("prod_warp_up"))
    '                    End If

    '                    sh.Range("R" & i).Value = dr("prod_consult")
    '                    If GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
    '                        TotConsult += GetSecFromTimeFormat(dr("prod_consult"))
    '                    End If

    '                    sh.Range("S" & i).Value = dr("prod_other")
    '                    If GetSecFromTimeFormat(dr("prod_other")) > 0 Then
    '                        TotOtherProd += GetSecFromTimeFormat(dr("prod_other"))
    '                    End If

    '                    sh.Range("T" & i).Value = dr("nprod_lunch")
    '                    If GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
    '                        TotLunch += GetSecFromTimeFormat(dr("nprod_lunch"))
    '                    End If

    '                    sh.Range("U" & i).Value = dr("nprod_leave")
    '                    If GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
    '                        TotLeave += GetSecFromTimeFormat(dr("nprod_leave"))
    '                    End If

    '                    sh.Range("V" & i).Value = dr("nprod_change_counter")
    '                    If GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
    '                        TotChangeCounter += GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                    End If

    '                    sh.Range("W" & i).Value = dr("nprod_home")
    '                    If GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
    '                        TotHome += GetSecFromTimeFormat(dr("nprod_home"))
    '                    End If

    '                    sh.Range("X" & i).Value = dr("nprod_mini_break")
    '                    If GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
    '                        TotMiniBreak += GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                    End If

    '                    sh.Range("Y" & i).Value = dr("nprod_restroom")
    '                    If GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
    '                        TotRestRoom += GetSecFromTimeFormat(dr("nprod_restroom"))
    '                    End If

    '                    sh.Range("Z" & i).Value = dr("nprod_other")
    '                    If GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
    '                        TotOtherNonProd += GetSecFromTimeFormat(dr("nprod_other"))
    '                    End If

    '                    Dim _total_time As Integer = GetSecFromTimeFormat(dr("total_time"))
    '                    If _total_time = 0 Then
    '                        _total_time = 1
    '                    End If
    '                    If GetSecFromTimeFormat(dr("productivity")) > 0 Then
    '                        sh.Range("AA" & i).Value = CInt((GetSecFromTimeFormat(dr("productivity")) / _total_time) * 100) & "%"
    '                    Else
    '                        sh.Range("AA" & i).Value = "0%"
    '                    End If

    '                    If GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
    '                        sh.Range("AB" & i).Value = CInt((GetSecFromTimeFormat(dr("non_productivity")) / _total_time) * 100) & "%"
    '                    Else
    '                        sh.Range("AB" & i).Value = "0%"
    '                    End If

    '                    If Convert.IsDBNull(dr("total_time")) = False Then
    '                        TotTotalTime += GetSecFromTimeFormat(dr("total_time"))
    '                    End If
    '                    If GetSecFromTimeFormat(dr("total_time")) > (450 * 60) Then
    '                        TotE += GetSecFromTimeFormat(dr("total_time")) - (450 * 60)
    '                    End If

    '                    TotS += GetSecFromTimeFormat(dr("service_time"))
    '                    TotProductivity += GetSecFromTimeFormat(dr("productivity"))
    '                    TotNonProductivity += GetSecFromTimeFormat(dr("non_productivity"))


    '                    SetDataRowStyle(dailyReportBook, sh, i, 27)
    '                    i += 1
    '                Next
    '                '== Start SubTotal
    '                SetRowTotalStyle(styleT, dailyReportBook, sh, i, 27, "AB")
    '                If TmpDt.Rows.Count > 0 Then
    '                    ' sh.Range("A" & i, "H" & i).Merge()
    '                    sh.Range("A" & i).Value = "Sub Total"
    '                    sh.Range("A" & i).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

    '                    sh.Range("B" & i).Value = sDr("Shop_name_en")
    '                    sh.Range("C" & i).Value = " " & ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
    '                    If TotTotalTime = 0 Then
    '                        sh.Range("I" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("I" & i).Value = GetFormatTimeFromSec(TotTotalTime).ToString
    '                    End If
    '                    If TotProductivity = 0 Then
    '                        sh.Range("J" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("J" & i).Value = GetFormatTimeFromSec(TotProductivity).ToString
    '                    End If
    '                    If TotNonProductivity = 0 Then
    '                        sh.Range("K" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("K" & i).Value = GetFormatTimeFromSec(TotNonProductivity).ToString
    '                    End If
    '                    If TotE = 0 Then
    '                        sh.Range("L" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("L" & i).Value = GetFormatTimeFromSec(TotE).ToString
    '                    End If
    '                    If TotS = 0 Then
    '                        sh.Range("M" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("M" & i).Value = GetFormatTimeFromSec(TotS).ToString
    '                    End If
    '                    If TotLearning = 0 Then
    '                        sh.Range("N" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("N" & i).Value = GetFormatTimeFromSec(TotLearning).ToString
    '                    End If
    '                    If TotStandBy = 0 Then
    '                        sh.Range("O" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("O" & i).Value = GetFormatTimeFromSec(TotStandBy).ToString
    '                    End If
    '                    If TotBrief = 0 Then
    '                        sh.Range("P" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("P" & i).Value = GetFormatTimeFromSec(TotBrief).ToString
    '                    End If
    '                    If TotWarpUp = 0 Then
    '                        sh.Range("Q" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("Q" & i).Value = GetFormatTimeFromSec(TotWarpUp).ToString
    '                    End If
    '                    If TotConsult = 0 Then
    '                        sh.Range("R" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("R" & i).Value = GetFormatTimeFromSec(TotConsult).ToString
    '                    End If
    '                    If TotOtherProd = 0 Then
    '                        sh.Range("S" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("S" & i).Value = GetFormatTimeFromSec(TotOtherProd).ToString
    '                    End If
    '                    If TotLunch = 0 Then
    '                        sh.Range("T" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("T" & i).Value = GetFormatTimeFromSec(TotLunch).ToString
    '                    End If
    '                    If TotLeave = 0 Then
    '                        sh.Range("U" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("U" & i).Value = GetFormatTimeFromSec(TotLeave).ToString
    '                    End If
    '                    If TotChangeCounter = 0 Then
    '                        sh.Range("V" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("V" & i).Value = GetFormatTimeFromSec(TotChangeCounter).ToString
    '                    End If
    '                    If TotHome = 0 Then
    '                        sh.Range("W" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("W" & i).Value = GetFormatTimeFromSec(TotHome).ToString
    '                    End If
    '                    If TotMiniBreak = 0 Then
    '                        sh.Range("X" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("X" & i).Value = GetFormatTimeFromSec(TotMiniBreak).ToString
    '                    End If
    '                    If TotRestRoom = 0 Then
    '                        sh.Range("Y" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("Y" & i).Value = GetFormatTimeFromSec(TotRestRoom).ToString
    '                    End If
    '                    If TotOtherNonProd = 0 Then
    '                        sh.Range("Z" & i).Value = "00:00:00"
    '                    Else
    '                        sh.Range("Z" & i).Value = GetFormatTimeFromSec(TotOtherNonProd).ToString
    '                    End If

    '                    If TotTotalTime = 0 Then
    '                        sh.Range("AA" & i).Value = "0%"
    '                        sh.Range("AB" & i).Value = "0%"
    '                    Else
    '                        sh.Range("AA" & i).Value = CInt((TotProductivity / TotTotalTime) * 100).ToString & "%"
    '                        sh.Range("AB" & i).Value = CInt((TotNonProductivity / TotTotalTime) * 100).ToString & "%"
    '                    End If
    '                End If
    '                '== End SubTotal
    '                sh.Columns.AutoFit()
    '            Next
    '        End Using

    '        Dim FilePath As String = "D:\ReportExcel"
    '        If Directory.Exists(FilePath) = False Then
    '            Directory.CreateDirectory(FilePath)
    '        End If

    '        Dim FileName As String = FilePath & "\" & excelFile & ".xlsx"
    '        If File.Exists(FileName) = True Then
    '            File.Delete(FileName)
    '        End If


    '        dailyReportBook.SaveAs(FileName)
    '        dailyReportBook.Close()
    '        chkApp = Nothing
    '        SendSqlEmail(FileName)
    '    End If
    'End Sub

    Private Sub CreateExcelFileMonth(ByVal excelFile As String, ByVal TmpDt As System.Data.DataTable)
        'Generate Excel File
        If TmpDt.Rows.Count > 0 Then
            Dim sDt As New System.Data.DataTable
            sDt = TmpDt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_abb", "region_code")
            Using ep As New ExcelPackage
                For Each sDr As DataRow In sDt.Rows
                    Dim sh As ExcelWorksheet = ep.Workbook.Worksheets.Add(Replace(sDr("shop_abb"), "/", ""))
                    sh.TabColor = System.Drawing.ColorTranslator.FromWin32(RGB(29, 213, 23))

                    SetCellsColor(sh, 1, 2)
                    sh.Cells("A1").Value = "Shop Name"

                    SetCellsColor(sh, 1, 1)
                    sh.Cells("B1").Value = "Region"

                    SetCellsColor(sh, 1, 3)
                    sh.Cells("C1").Value = "Month"

                    SetCellsColor(sh, 1, 4)
                    sh.Cells("D1").Value = "Year"

                    SetCellsColor(sh, 1, 5)
                    sh.Cells("E1").Value = "EmpID"

                    SetCellsColor(sh, 1, 6)
                    sh.Cells("F1").Value = "User Name"

                    SetCellsColor(sh, 1, 7)
                    sh.Cells("G1").Value = "Staff Name"

                    SetCellsColor(sh, 1, 8)
                    sh.Cells("H1").Value = "Total Log-In"

                    SetCellsColor(sh, 1, 9)
                    sh.Cells("I1").Value = "Total Log-Out"

                    SetCellsColor(sh, 1, 10)
                    sh.Cells("J1").Value = "Total Time"

                    SetCellsColor(sh, 1, 11)
                    sh.Cells("K1").Value = "Productivity(HR)"

                    SetCellsColor(sh, 1, 12)
                    sh.Cells("L1").Value = "Non-Productivity(HR)"

                    SetCellsColor(sh, 1, 13)
                    sh.Cells("M1").Value = "Estimated OT"

                    SetCellsColor(sh, 1, 14)
                    sh.Cells("N1").Value = "Service Time"

                    SetCellsColor(sh, 1, 15)
                    sh.Cells("O1").Value = "Productivity Learning"

                    SetCellsColor(sh, 1, 16)
                    sh.Cells("P1").Value = "Productivity Stand by"

                    SetCellsColor(sh, 1, 17)
                    sh.Cells("Q1").Value = "Productivity Brief"

                    SetCellsColor(sh, 1, 18)
                    sh.Cells("R1").Value = "Productivity Wrap up"

                    SetCellsColor(sh, 1, 19)
                    sh.Cells("S1").Value = "Productivity Consult"

                    SetCellsColor(sh, 1, 20)
                    sh.Cells("T1").Value = "Other Productivity"

                    SetCellsColor(sh, 1, 21)
                    sh.Cells("U1").Value = "Non-Productivity Lunch"

                    SetCellsColor(sh, 1, 22)
                    sh.Cells("V1").Value = "Non-Productivity Leave"

                    SetCellsColor(sh, 1, 23)
                    sh.Cells("W1").Value = "Non-Productivity Change Counter"

                    SetCellsColor(sh, 1, 24)
                    sh.Cells("X1").Value = "Non-Productivity Home"

                    SetCellsColor(sh, 1, 25)
                    sh.Cells("Y1").Value = "Non-Productivity Mini Break"

                    SetCellsColor(sh, 1, 26)
                    sh.Cells("Z1").Value = "Non-Productivity Rest Room"

                    SetCellsColor(sh, 1, 27)
                    sh.Cells("AA1").Value = "Other Non-Productivity"

                    SetCellsColor(sh, 1, 28)
                    sh.Cells("AB1").Value = "% Productivity"

                    SetCellsColor(sh, 1, 29)
                    sh.Cells("AC1").Value = "% Non-productive"

                    Dim TotTotalTime As Long = 0
                    Dim TotProductivity As Long = 0
                    Dim TotNonProductivity As Long = 0
                    Dim TotE As Long = 0
                    Dim TotS As Long = 0
                    Dim TotLearning As Long = 0
                    Dim TotStandBy As Long = 0
                    Dim TotBrief As Long = 0
                    Dim TotWarpUp As Long = 0
                    Dim TotConsult As Long = 0
                    Dim TotOtherProd As Long = 0
                    Dim TotLunch As Long = 0
                    Dim TotLeave As Long = 0
                    Dim TotChangeCounter As Long = 0
                    Dim TotHome As Long = 0
                    Dim TotMiniBreak As Long = 0
                    Dim TotRestRoom As Long = 0
                    Dim TotOtherNonProd As Long = 0
                    Dim TotWorkingDay As Long = 0
                    Dim TotProductivityPer As Long = 0
                    Dim TotNonProductivityPer As Long = 0
                    Dim TotLogin As Long = 0
                    Dim TotLogout As Long = 0

                    Dim i As Integer = 2
                    TmpDt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
                    For Each dr As DataRowView In TmpDt.DefaultView
                        sh.Cells("A" & i).Value = dr("Shop_Name_EN")
                        sh.Cells("B" & i).Value = dr("Region_Code")
                        sh.Cells("C" & i).Value = Engine.Common.FunctionEng.GetMonthName(chkMonth.SelectedIndex + 1)
                        sh.Cells("D" & i).Value = CInt(txtYear.Text) - 543
                        sh.Cells("E" & i).Value = dr("user_code")
                        sh.Cells("F" & i).Value = dr("username")
                        sh.Cells("G" & i).Value = dr("staff_name")
                        sh.Cells("H" & i).Value = dr("log_in")
                        If GetSecFromTimeFormat(dr("log_in")) > 0 Then
                            TotLogin += GetSecFromTimeFormat(dr("log_in"))
                        End If

                        sh.Cells("I" & i).Value = dr("log_out")
                        If GetSecFromTimeFormat(dr("log_out")) > 0 Then
                            TotLogout += GetSecFromTimeFormat(dr("log_out"))
                        End If
                        sh.Cells("J" & i).Value = dr("total_time")
                        sh.Cells("K" & i).Value = dr("productivity")
                        sh.Cells("L" & i).Value = dr("non_productivity")

                        If Convert.IsDBNull(dr("total_time")) = False Then
                            TotTotalTime += GetSecFromTimeFormat(dr("total_time"))
                        End If

                        If Convert.IsDBNull(dr("est_ot")) = False Then
                            TotE += GetSecFromTimeFormat(dr("est_ot"))
                            sh.Cells("M" & i).Value = GetFormatTimeFromSec(TotE)
                        Else
                            sh.Cells("M" & i).Value = "00:00:00"
                        End If

                        If Convert.IsDBNull(dr("service_time")) = False Then
                            sh.Cells("N" & i).Value = dr("service_time")
                            TotS += GetSecFromTimeFormat(dr("service_time"))
                        Else
                            sh.Cells("N" & i).Value = "00:00:00"
                        End If

                        If GetSecFromTimeFormat(dr("productivity")) > 0 Then
                            TotProductivity += GetSecFromTimeFormat(dr("productivity"))
                        End If
                        If GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
                            TotNonProductivity += GetSecFromTimeFormat(dr("non_productivity"))
                        End If

                        '#### Productivity
                        sh.Cells("O" & i).Value = dr("prod_learning")
                        If GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
                            TotLearning += GetSecFromTimeFormat(dr("prod_learning"))
                        End If

                        sh.Cells("P" & i).Value = dr("prod_stand_by")
                        If GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
                            TotStandBy += GetSecFromTimeFormat(dr("prod_stand_by"))
                        End If

                        sh.Cells("Q" & i).Value = dr("prod_brief")
                        If GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
                            TotBrief += GetSecFromTimeFormat(dr("prod_brief"))
                        End If

                        sh.Cells("R" & i).Value = dr("prod_warp_up")
                        If GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
                            TotWarpUp += GetSecFromTimeFormat(dr("prod_warp_up"))
                        End If

                        sh.Cells("S" & i).Value = dr("prod_consult")
                        If GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
                            TotConsult += GetSecFromTimeFormat(dr("prod_consult"))
                        End If

                        sh.Cells("T" & i).Value = dr("prod_other")
                        If GetSecFromTimeFormat(dr("prod_other")) > 0 Then
                            TotOtherProd += GetSecFromTimeFormat(dr("prod_other"))
                        End If

                        '### Non Productivity
                        sh.Cells("U" & i).Value = dr("nprod_lunch")
                        If GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
                            TotLunch += GetSecFromTimeFormat(dr("nprod_lunch"))
                        End If

                        sh.Cells("V" & i).Value = dr("nprod_leave")
                        If GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
                            TotLeave += GetSecFromTimeFormat(dr("nprod_leave"))
                        End If

                        sh.Cells("W" & i).Value = dr("nprod_change_counter")
                        If GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
                            TotChangeCounter += GetSecFromTimeFormat(dr("nprod_change_counter"))
                        End If

                        sh.Cells("X" & i).Value = dr("nprod_home")
                        If GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
                            TotHome += GetSecFromTimeFormat(dr("nprod_home"))
                        End If

                        sh.Cells("Y" & i).Value = dr("nprod_mini_break")
                        If GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
                            TotMiniBreak += GetSecFromTimeFormat(dr("nprod_mini_break"))
                        End If

                        sh.Cells("Z" & i).Value = dr("nprod_restroom")
                        If GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
                            TotRestRoom += GetSecFromTimeFormat(dr("nprod_restroom"))
                        End If

                        sh.Cells("AA" & i).Value = dr("nprod_other")
                        If GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
                            TotOtherNonProd += GetSecFromTimeFormat(dr("nprod_other"))
                        End If

                        Dim _total_time As Integer = GetSecFromTimeFormat(dr("total_time"))
                        If _total_time = 0 Then
                            _total_time = 1
                        End If
                        If _total_time > 0 Then
                            sh.Cells("AB" & i).Value = CInt((GetSecFromTimeFormat(dr("productivity")) / _total_time) * 100) & "%"
                            TotProductivityPer += GetSecFromTimeFormat(dr("productivity"))
                        Else
                            sh.Cells("AB" & i).Value = "0%"
                        End If

                        If _total_time > 0 Then
                            sh.Cells("AC" & i).Value = CInt((GetSecFromTimeFormat(dr("non_productivity")) / _total_time) * 100) & "%"
                            TotNonProductivityPer += GetSecFromTimeFormat(dr("non_productivity"))
                        Else
                            sh.Cells("AC" & i).Value = "0%"
                        End If
                        i += 1
                    Next

                    SetRowTotalStyle(sh, i, "AC")
                    sh.Cells("A" & i).Value = sDr("shop_name_en")
                    sh.Cells("B" & i).Value = sDr("Region_Code")
                    sh.Cells("C" & i).Value = Engine.Common.FunctionEng.GetMonthName(chkMonth.SelectedIndex + 1)
                    sh.Cells("D" & i).Value = CInt(txtYear.Text) - 543
                    sh.Cells("G" & i).Value = "Sub Total "
                    sh.Cells("G" & i).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    sh.Cells("H" & i).Value = GetFormatTimeFromSec(TotLogin)
                    sh.Cells("I" & i).Value = GetFormatTimeFromSec(TotLogout)
                    sh.Cells("J" & i).Value = GetFormatTimeFromSec(TotTotalTime)
                    sh.Cells("K" & i).Value = GetFormatTimeFromSec(TotProductivity)
                    sh.Cells("L" & i).Value = GetFormatTimeFromSec(TotNonProductivity)
                    sh.Cells("M" & i).Value = GetFormatTimeFromSec(TotE)
                    sh.Cells("N" & i).Value = GetFormatTimeFromSec(TotS)
                    sh.Cells("O" & i).Value = GetFormatTimeFromSec(TotLearning)
                    sh.Cells("P" & i).Value = GetFormatTimeFromSec(TotStandBy)
                    sh.Cells("Q" & i).Value = GetFormatTimeFromSec(TotBrief)
                    sh.Cells("R" & i).Value = GetFormatTimeFromSec(TotWarpUp)
                    sh.Cells("S" & i).Value = GetFormatTimeFromSec(TotConsult)
                    sh.Cells("T" & i).Value = GetFormatTimeFromSec(TotOtherProd)
                    sh.Cells("U" & i).Value = GetFormatTimeFromSec(TotLunch)
                    sh.Cells("V" & i).Value = GetFormatTimeFromSec(TotLeave)
                    sh.Cells("W" & i).Value = GetFormatTimeFromSec(TotChangeCounter)
                    sh.Cells("X" & i).Value = GetFormatTimeFromSec(TotHome)
                    sh.Cells("Y" & i).Value = GetFormatTimeFromSec(TotMiniBreak)
                    sh.Cells("Z" & i).Value = GetFormatTimeFromSec(TotRestRoom)
                    sh.Cells("AA" & i).Value = GetFormatTimeFromSec(TotOtherNonProd)
                    If TotProductivity = 0 Then
                        sh.Cells("AB" & i).Value = "0%"
                    Else
                        sh.Cells("AB" & i).Value = CInt((TotProductivityPer / TotTotalTime) * 100).ToString & "%"
                    End If
                    If TotNonProductivity = 0 Then
                        sh.Cells("AC" & i).Value = "0%"
                    Else
                        sh.Cells("AC" & i).Value = CInt((TotNonProductivityPer / TotTotalTime) * 100).ToString & "%"
                    End If

                    Using shRange As ExcelRange = sh.Cells("A1:AC" & i)
                        shRange.AutoFitColumns()

                        shRange.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        shRange.Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                        shRange.Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        shRange.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                    End Using
                Next

                Dim vMonth As String = (CInt(txtYear.Text) - 543).ToString & "\" & (CInt(txtYear.Text) - 543).ToString & "" & (chkMonth.SelectedIndex + 1).ToString.PadLeft(2, "0")
                Dim FilePath As String = "D:\ReportExcel\" & vMonth
                If Directory.Exists(FilePath) = False Then
                    Directory.CreateDirectory(FilePath)
                End If

                Dim FileName As String = FilePath & "\" & excelFile & ".xlsx"
                If File.Exists(FileName) = True Then
                    File.Delete(FileName)
                End If

                Dim b() As Byte = ep.GetAsByteArray
                File.WriteAllBytes(FileName, b)

                If chkSendEmail.Checked = True Then
                    If File.Exists(FileName) = True Then
                        SendSqlEmail(FileName)
                    End If
                End If
            End Using
        End If
    End Sub

    'Private Sub CreateExcelFileMonth(ByVal excelFile As String, ByVal TmpDt As System.Data.DataTable)
    '    'Generate Excel File
    '    If TmpDt.Rows.Count > 0 Then
    '        Dim chkApp As New Excel.Application
    '        Dim dailyReportBook As Excel.Workbook = chkApp.Workbooks.Add
    '        Dim sDt As New System.Data.DataTable
    '        sDt = TmpDt.DefaultView.ToTable(True, "shop_id", "shop_name_en", "shop_abb", "region_code")

    '        Dim style As Excel.Style = dailyReportBook.Application.ActiveWorkbook.Styles.Add("NewStyle")
    '        Dim styleT As Excel.Style = dailyReportBook.Application.ActiveWorkbook.Styles.Add("TotalStyle")
    '        Dim sh As Excel.Worksheet

    '        For Each sDr As DataRow In sDt.Rows
    '            sh = dailyReportBook.Worksheets.Add
    '            sh.Name = Replace(sDr("shop_abb"), "/", "")
    '            sh.Tab.Color = RGB(29, 213, 23)

    '            SetCellsColor(style, dailyReportBook, sh, 1, 2)
    '            sh.Range("A1").Value = "Shop Name"
    '            sh.Range("A1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 1)
    '            sh.Range("B1").Value = "Region"
    '            sh.Range("B1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 3)
    '            sh.Range("C1").Value = "Month"
    '            sh.Range("C1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 4)
    '            sh.Range("D1").Value = "Year"
    '            sh.Range("D1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 5)
    '            sh.Range("E1").Value = "EmpID"
    '            sh.Range("E1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 6)
    '            sh.Range("F1").Value = "User Name"
    '            sh.Range("F1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 7)
    '            sh.Range("G1").Value = "Staff Name"
    '            sh.Range("G1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 8)
    '            sh.Range("H1").Value = "Total Log-In"
    '            sh.Range("H1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 9)
    '            sh.Range("I1").Value = "Total Log-Out"
    '            sh.Range("I1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 10)
    '            sh.Range("J1").Value = "Total Time"
    '            sh.Range("J1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 11)
    '            sh.Range("K1").Value = "Productivity(HR)"
    '            sh.Range("K1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 12)
    '            sh.Range("L1").Value = "Non-Productivity(HR)"
    '            sh.Range("L1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 13)
    '            sh.Range("M1").Value = "Estimated OT"
    '            sh.Range("M1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 14)
    '            sh.Range("N1").Value = "Service Time"
    '            sh.Range("N1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 15)
    '            sh.Range("O1").Value = "Productivity Learning"
    '            sh.Range("O1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 16)
    '            sh.Range("P1").Value = "Productivity Stand by"
    '            sh.Range("P1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 17)
    '            sh.Range("Q1").Value = "Productivity Brief"
    '            sh.Range("Q1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 18)
    '            sh.Range("R1").Value = "Productivity Wrap up"
    '            sh.Range("R1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 19)
    '            sh.Range("S1").Value = "Productivity Consult"
    '            sh.Range("S1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 20)
    '            sh.Range("T1").Value = "Other Productivity"
    '            sh.Range("T1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 21)
    '            sh.Range("U1").Value = "Non-Productivity Lunch"
    '            sh.Range("U1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 22)
    '            sh.Range("V1").Value = "Non-Productivity Leave"
    '            sh.Range("V1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 23)
    '            sh.Range("W1").Value = "Non-Productivity Change Counter"
    '            sh.Range("W1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 24)
    '            sh.Range("X1").Value = "Non-Productivity Home"
    '            sh.Range("X1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 25)
    '            sh.Range("Y1").Value = "Non-Productivity Mini Break"
    '            sh.Range("Y1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 26)
    '            sh.Range("Z1").Value = "Non-Productivity Rest Room"
    '            sh.Range("Z1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 27)
    '            sh.Range("AA1").Value = "Other Non-Productivity"
    '            sh.Range("AA1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 28)
    '            sh.Range("AB1").Value = "% Productivity"
    '            sh.Range("AB1").Borders.Weight = 2

    '            SetCellsColor(style, dailyReportBook, sh, 1, 29)
    '            sh.Range("AC1").Value = "% Non-productive"
    '            sh.Range("AC1").Borders.Weight = 2

    '            Dim TotTotalTime As Long = 0
    '            Dim TotProductivity As Long = 0
    '            Dim TotNonProductivity As Long = 0
    '            Dim TotE As Long = 0
    '            Dim TotS As Long = 0
    '            Dim TotLearning As Long = 0
    '            Dim TotStandBy As Long = 0
    '            Dim TotBrief As Long = 0
    '            Dim TotWarpUp As Long = 0
    '            Dim TotConsult As Long = 0
    '            Dim TotOtherProd As Long = 0
    '            Dim TotLunch As Long = 0
    '            Dim TotLeave As Long = 0
    '            Dim TotChangeCounter As Long = 0
    '            Dim TotHome As Long = 0
    '            Dim TotMiniBreak As Long = 0
    '            Dim TotRestRoom As Long = 0
    '            Dim TotOtherNonProd As Long = 0
    '            Dim TotWorkingDay As Long = 0
    '            Dim TotProductivityPer As Long = 0
    '            Dim TotNonProductivityPer As Long = 0
    '            Dim TotLogin As Long = 0
    '            Dim TotLogout As Long = 0

    '            Dim i As Integer = 2
    '            TmpDt.DefaultView.RowFilter = "shop_id='" & sDr("shop_id") & "'"
    '            For Each dr As DataRowView In TmpDt.DefaultView
    '                sh.Range("A" & i).Value = dr("Shop_Name_EN")
    '                sh.Range("B" & i).Value = dr("Region_Code")
    '                sh.Range("C" & i).Value = Engine.Common.FunctionEng.GetMonthName(chkMonth.SelectedIndex + 1)
    '                sh.Range("D" & i).Value = CInt(txtYear.Text) - 543
    '                sh.Range("E" & i).Value = dr("user_code")
    '                sh.Range("F" & i).Value = dr("username")
    '                sh.Range("G" & i).Value = dr("staff_name")
    '                sh.Range("H" & i).Value = dr("log_in")
    '                If GetSecFromTimeFormat(dr("log_in")) > 0 Then
    '                    TotLogin += GetSecFromTimeFormat(dr("log_in"))
    '                End If

    '                sh.Range("I" & i).Value = dr("log_out")
    '                If GetSecFromTimeFormat(dr("log_out")) > 0 Then
    '                    TotLogout += GetSecFromTimeFormat(dr("log_out"))
    '                End If
    '                sh.Range("J" & i).Value = dr("total_time")
    '                sh.Range("K" & i).Value = dr("productivity")
    '                sh.Range("L" & i).Value = dr("non_productivity")

    '                'Dim drTotalTime As Long = 0
    '                If Convert.IsDBNull(dr("total_time")) = False Then
    '                    TotTotalTime += GetSecFromTimeFormat(dr("total_time"))
    '                End If

    '                If Convert.IsDBNull(dr("est_ot")) = False Then
    '                    TotE += GetSecFromTimeFormat(dr("est_ot"))
    '                    sh.Range("M" & i).Value = GetFormatTimeFromSec(TotE)
    '                Else
    '                    sh.Range("M" & i).Value = "00:00:00"
    '                End If

    '                If Convert.IsDBNull(dr("service_time")) = False Then
    '                    sh.Range("N" & i).Value = dr("service_time")
    '                    TotS += GetSecFromTimeFormat(dr("service_time"))
    '                Else
    '                    sh.Range("N" & i).Value = "00:00:00"
    '                End If

    '                If GetSecFromTimeFormat(dr("productivity")) > 0 Then
    '                    TotProductivity += GetSecFromTimeFormat(dr("productivity"))
    '                End If
    '                If GetSecFromTimeFormat(dr("non_productivity")) > 0 Then
    '                    TotNonProductivity += GetSecFromTimeFormat(dr("non_productivity"))
    '                End If

    '                '#### Productivity
    '                sh.Range("O" & i).Value = dr("prod_learning")
    '                If GetSecFromTimeFormat(dr("prod_learning")) > 0 Then
    '                    TotLearning += GetSecFromTimeFormat(dr("prod_learning"))
    '                End If

    '                sh.Range("P" & i).Value = dr("prod_stand_by")
    '                If GetSecFromTimeFormat(dr("prod_stand_by")) > 0 Then
    '                    TotStandBy += GetSecFromTimeFormat(dr("prod_stand_by"))
    '                End If

    '                sh.Range("Q" & i).Value = dr("prod_brief")
    '                If GetSecFromTimeFormat(dr("prod_brief")) > 0 Then
    '                    TotBrief += GetSecFromTimeFormat(dr("prod_brief"))
    '                End If

    '                sh.Range("R" & i).Value = dr("prod_warp_up")
    '                If GetSecFromTimeFormat(dr("prod_warp_up")) > 0 Then
    '                    TotWarpUp += GetSecFromTimeFormat(dr("prod_warp_up"))
    '                End If

    '                sh.Range("S" & i).Value = dr("prod_consult")
    '                If GetSecFromTimeFormat(dr("prod_consult")) > 0 Then
    '                    TotConsult += GetSecFromTimeFormat(dr("prod_consult"))
    '                End If

    '                sh.Range("T" & i).Value = dr("prod_other")
    '                If GetSecFromTimeFormat(dr("prod_other")) > 0 Then
    '                    TotOtherProd += GetSecFromTimeFormat(dr("prod_other"))
    '                End If

    '                '### Non Productivity
    '                sh.Range("U" & i).Value = dr("nprod_lunch")
    '                If GetSecFromTimeFormat(dr("nprod_lunch")) > 0 Then
    '                    TotLunch += GetSecFromTimeFormat(dr("nprod_lunch"))
    '                End If

    '                sh.Range("V" & i).Value = dr("nprod_leave")
    '                If GetSecFromTimeFormat(dr("nprod_leave")) > 0 Then
    '                    TotLeave += GetSecFromTimeFormat(dr("nprod_leave"))
    '                End If

    '                sh.Range("W" & i).Value = dr("nprod_change_counter")
    '                If GetSecFromTimeFormat(dr("nprod_change_counter")) > 0 Then
    '                    TotChangeCounter += GetSecFromTimeFormat(dr("nprod_change_counter"))
    '                End If

    '                sh.Range("X" & i).Value = dr("nprod_home")
    '                If GetSecFromTimeFormat(dr("nprod_home")) > 0 Then
    '                    TotHome += GetSecFromTimeFormat(dr("nprod_home"))
    '                End If

    '                sh.Range("Y" & i).Value = dr("nprod_mini_break")
    '                If GetSecFromTimeFormat(dr("nprod_mini_break")) > 0 Then
    '                    TotMiniBreak += GetSecFromTimeFormat(dr("nprod_mini_break"))
    '                End If

    '                sh.Range("Z" & i).Value = dr("nprod_restroom")
    '                If GetSecFromTimeFormat(dr("nprod_restroom")) > 0 Then
    '                    TotRestRoom += GetSecFromTimeFormat(dr("nprod_restroom"))
    '                End If

    '                sh.Range("AA" & i).Value = dr("nprod_other")
    '                If GetSecFromTimeFormat(dr("nprod_other")) > 0 Then
    '                    TotOtherNonProd += GetSecFromTimeFormat(dr("nprod_other"))
    '                End If

    '                Dim _total_time As Integer = GetSecFromTimeFormat(dr("total_time"))
    '                If _total_time = 0 Then
    '                    _total_time = 1
    '                End If
    '                If _total_time > 0 Then
    '                    sh.Range("AB" & i).Value = CInt((GetSecFromTimeFormat(dr("productivity")) / _total_time) * 100) & "%"
    '                    TotProductivityPer += GetSecFromTimeFormat(dr("productivity"))
    '                Else
    '                    sh.Range("AB" & i).Value = "0%"
    '                End If

    '                If _total_time > 0 Then
    '                    sh.Range("AC" & i).Value = CInt((GetSecFromTimeFormat(dr("non_productivity")) / _total_time) * 100) & "%"
    '                    TotNonProductivityPer += GetSecFromTimeFormat(dr("non_productivity"))
    '                Else
    '                    sh.Range("AC" & i).Value = "0%"
    '                End If
    '                SetDataRowStyle(dailyReportBook, sh, i, 28)
    '                i += 1
    '            Next

    '            SetRowTotalStyle(styleT, dailyReportBook, sh, i, 28, "AC")
    '            sh.Range("A" & i).Value = sDr("shop_name_en")
    '            sh.Range("B" & i).Value = sDr("Region_Code")
    '            sh.Range("C" & i).Value = Engine.Common.FunctionEng.GetMonthName(chkMonth.SelectedIndex + 1)
    '            sh.Range("D" & i).Value = CInt(txtYear.Text) - 543
    '            sh.Range("G" & i).Value = "Sub Total "
    '            sh.Range("G" & i).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
    '            sh.Range("H" & i).Value = GetFormatTimeFromSec(TotLogin)
    '            sh.Range("I" & i).Value = GetFormatTimeFromSec(TotLogout)
    '            sh.Range("J" & i).Value = GetFormatTimeFromSec(TotTotalTime)
    '            sh.Range("K" & i).Value = GetFormatTimeFromSec(TotProductivity)
    '            sh.Range("L" & i).Value = GetFormatTimeFromSec(TotNonProductivity)
    '            sh.Range("M" & i).Value = GetFormatTimeFromSec(TotE)
    '            sh.Range("N" & i).Value = GetFormatTimeFromSec(TotS)
    '            sh.Range("O" & i).Value = GetFormatTimeFromSec(TotLearning)
    '            sh.Range("P" & i).Value = GetFormatTimeFromSec(TotStandBy)
    '            sh.Range("Q" & i).Value = GetFormatTimeFromSec(TotBrief)
    '            sh.Range("R" & i).Value = GetFormatTimeFromSec(TotWarpUp)
    '            sh.Range("S" & i).Value = GetFormatTimeFromSec(TotConsult)
    '            sh.Range("T" & i).Value = GetFormatTimeFromSec(TotOtherProd)
    '            sh.Range("U" & i).Value = GetFormatTimeFromSec(TotLunch)
    '            sh.Range("V" & i).Value = GetFormatTimeFromSec(TotLeave)
    '            sh.Range("W" & i).Value = GetFormatTimeFromSec(TotChangeCounter)
    '            sh.Range("X" & i).Value = GetFormatTimeFromSec(TotHome)
    '            sh.Range("Y" & i).Value = GetFormatTimeFromSec(TotMiniBreak)
    '            sh.Range("Z" & i).Value = GetFormatTimeFromSec(TotRestRoom)
    '            sh.Range("AA" & i).Value = GetFormatTimeFromSec(TotOtherNonProd)
    '            If TotProductivity = 0 Then
    '                sh.Range("AB" & i).Value = "0%"
    '            Else
    '                sh.Range("AB" & i).Value = CInt((TotProductivityPer / TotTotalTime) * 100).ToString & "%"
    '            End If
    '            If TotNonProductivity = 0 Then
    '                sh.Range("AC" & i).Value = "0%"
    '            Else
    '                sh.Range("AC" & i).Value = CInt((TotNonProductivityPer / TotTotalTime) * 100).ToString & "%"
    '            End If
    '            sh.Columns.AutoFit()
    '        Next

    '        Dim FilePath As String = "D:\ReportExcel"
    '        If Directory.Exists(FilePath) = False Then
    '            Directory.CreateDirectory(FilePath)
    '        End If

    '        Dim FileName As String = FilePath & "\" & excelFile & ".xlsx"
    '        If File.Exists(FileName) = True Then
    '            File.Delete(FileName)
    '        End If

    '        dailyReportBook.SaveAs(FileName)
    '        dailyReportBook.Close()
    '        chkApp = Nothing
    '        SendSqlEmail(FileName)
    '    End If
    'End Sub

    Function GetSecFromTimeFormat(ByVal TimeFormat As String) As Integer
        'แปลงเวลาในรูปแบบ HH:mm:ss ไปเป็นวินาที

        Dim ret As Int32 = 0
        If TimeFormat.Trim <> "" Then
            Dim tmp() As String = Split(TimeFormat, ":")
            Dim TimeSec As Integer = 0
            If Convert.ToInt64(tmp(0)) > 0 Then
                TimeSec += (Convert.ToInt64(tmp(0)) * 60 * 60)
            End If
            If Convert.ToInt64(tmp(1)) > 0 Then
                TimeSec += (Convert.ToInt64(tmp(1)) * 60)
            End If
            ret = TimeSec + Convert.ToInt32(tmp(2))
        End If
        Return ret
    End Function

    Function GetFormatTimeFromSec(ByVal TimeSec As Integer) As String
        'แปลงเวลาจากวินาทีไปเป็น HH:mm:ss
        Dim tHour As Integer = 0
        Dim tMin As Integer = 0
        Dim tSec As Integer = 0
        If TimeSec >= 3600 Then
            tHour = Math.Floor(TimeSec / 3600) 'ชม.
            tMin = Math.Floor((TimeSec - (tHour * 3600)) / 60) ' นาที
            tSec = (TimeSec - (tHour * 3600)) Mod 60
        Else
            tMin = Math.Floor(TimeSec / 60)
            tSec = TimeSec Mod 60
        End If

        Return tHour.ToString.PadLeft(2, "0") & ":" & tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
    End Function



    Sub FillWithMonths(ByVal ComboBox As ComboBox, ByVal indexofMonth As Integer)
        'Dim usEnglish As New Globalization.CultureInfo("en-US")
        'Dim englishInfo As Globalization.DateTimeFormatInfo = usEnglish.DateTimeFormat

        ComboBox.DataSource = _
        ( _
            From MonthIndex In Enumerable.Range(1, 12) _
            Select MonthName = DateAndTime.MonthName(MonthIndex) _
        ).ToArray

        ComboBox.SelectedIndex = indexofMonth
    End Sub

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
#End Region '"__Sub&Function"


   
End Class
