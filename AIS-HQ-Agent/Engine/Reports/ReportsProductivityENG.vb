Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportsProductivityENG : Inherits ReportsENG
        Dim _ServiceDate As New DateTime(1, 1, 1)
        Dim _ShopID As Long = 0
        Dim _lblTime As Label

        Public WriteOnly Property ServiceDate()
            Set(ByVal value)
                _ServiceDate = value
            End Set
        End Property
        Public WriteOnly Property ShopID() As Long
            Set(ByVal value As Long)
                _ShopID = value
            End Set
        End Property
        Public WriteOnly Property lblTime() As Label
            Set(ByVal value As Label)
                _lblTime = value
            End Set
        End Property

        Public Sub ProductivityProcessAllReport()
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.ProductivityReportByDate, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_PRODUCTIVITY_DAY where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim stDT As DataTable = ReportsENG.GetUserByQTrans(ServiceDate, shLnq, trans)
                    If stDT.Rows.Count > 0 Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByDate")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For i As Integer = 0 To stDT.Rows.Count - 1
                                    Try
                                        Dim lnq As New CenLinqDB.TABLE.TbRepProductivityDayCenLinqDB
                                        lnq.SHOP_ID = shLnq.ID
                                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                        lnq.SERVICE_DATE = ServiceDate
                                        lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                        lnq.SHOW_DAY = ServiceDate.ToString("dddd", New Globalization.CultureInfo("en-US"))
                                        lnq.SHOW_WEEK = DatePart(DateInterval.WeekOfYear, ServiceDate)
                                        lnq.SHOW_YEAR = ServiceDate.Year
                                        lnq.USER_ID = stDT.Rows(i)("id")
                                        lnq.USER_CODE = stDT.Rows(i)("user_code").ToString
                                        lnq.USER_NAME = stDT.Rows(i)("username").ToString
                                        lnq.STAFF_NAME = stDT.Rows(i)("fname").ToString & " " & stDT.Rows(i)("lname").ToString
                                        lnq.PER_ACHIEVE_TARGET = 0

                                        For Each sDr As DataRow In sDt.Rows
                                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByDate")
                                            Dim ServiceID As Integer = sDr("id")
                                            Dim Target As Integer = GetTarget(sDr, "D", ServiceDate)
                                            Dim Actual As Integer = GetActual(sDr("id"), lnq.USER_ID, ServiceDate, "D", shTrans)
                                            Dim Percent As Integer = (Actual / Target) * 100
                                            Dim Aht As String = GetAHT(ServiceID, lnq.USER_ID, ServiceDate, "D", shTrans)
                                            Dim Sht As String = GetSHT(ServiceID, lnq.USER_ID, ServiceDate, "SHT", "D", shTrans)
                                            Dim Cht As String = GetSHT(ServiceID, lnq.USER_ID, ServiceDate, "CHT", "D", shTrans)
                                            Dim TotTarget As String = GetPrimaryTarget(ServiceDate, stDT.Rows(i)("id"), shTrans)

                                            shTrans.CommitTransaction()
                                            If lnq.DATA_COLUMN = "" Then
                                                lnq.DATA_COLUMN = "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT|SHT|CHT|TotTarget"
                                                lnq.DATA_VALUE = ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht & "|" & Sht & "|" & Cht & "|" & TotTarget
                                            Else
                                                lnq.DATA_COLUMN += "###" & "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT|SHT|CHT|TotTarget"
                                                lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht & "|" & Sht & "|" & Cht & "|" & TotTarget
                                            End If
                                        Next
                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsProductivityENG")
                                        End If
                                    Catch ex As Exception
                                        FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByDate", shLnq.SHOP_ABB & " Exception :" & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsProductivityENG")
                                    End Try
                                Next

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Else
                                UpdateProcessError(ProcID, sLnq.ErrorMessage)
                            End If
                        Else
                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                        End If
                    End If
                    stDT = Nothing
                End If
                UpdateProcessTime(ProcID)
            End If
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : ProductivityReportByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_PRODUCTIVITY_WEEK where week_of_year = '" & WeekNo & "' and show_year = '" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        'หา รายชื่อ Staff ของ Shop
                        Dim stLnq As New ShLinqDB.TABLE.TbUserShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByWeek")
                        Dim stDT As DataTable = stLnq.GetDataList("active_status = '1' and upper(username) <> 'ADMIN'", "fname, lname", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If stDT.Rows.Count > 0 Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByWeek")
                            If shTrans.Trans IsNot Nothing Then
                                Dim acDt As DataTable = GetActualByWeek(ServiceDate, shTrans)
                                Dim ahtDt As DataTable = GetAHTByWeek(ServiceDate, shTrans)
                                Dim shtDt As DataTable = GetSHTByWeek(ServiceDate, shTrans)
                                shTrans.CommitTransaction()

                                For i As Integer = 0 To stDT.Rows.Count - 1
                                    Try
                                        Dim lnq As New CenLinqDB.TABLE.TbRepProductivityWeekCenLinqDB
                                        lnq.SHOP_ID = shLnq.ID
                                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                        lnq.WEEK_OF_YEAR = WeekNo
                                        lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                        lnq.PERIOD_DATE = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & " - " & GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                        lnq.USER_ID = stDT.Rows(i)("id")
                                        lnq.USER_CODE = stDT.Rows(i)("user_code")
                                        lnq.USER_NAME = stDT.Rows(i)("username")
                                        lnq.STAFF_NAME = stDT.Rows(i)("fname") & " " & stDT.Rows(i)("lname")

                                        'หา Service ของ Shop
                                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByWeek")
                                        If shTrans.Trans IsNot Nothing Then
                                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                                            shTrans.CommitTransaction()
                                            If sDt.Rows.Count > 0 Then
                                                For Each sDr As DataRow In sDt.Rows
                                                    Dim ServiceID As Integer = sDr("id")
                                                    acDt.DefaultView.RowFilter = "user_id= '" & lnq.USER_ID & "' and item_id='" & ServiceID & "'"
                                                    ahtDt.DefaultView.RowFilter = "user_id= '" & lnq.USER_ID & "' and item_id='" & ServiceID & "'"
                                                    shtDt.DefaultView.RowFilter = "user_id= '" & lnq.USER_ID & "' and item_id='" & ServiceID & "'"

                                                    Dim Actual As Integer = 0 'GetActualByWeek(sDr("id"), lnq.USER_ID, ServiceDate, "W", shTrans)
                                                    If acDt.DefaultView.Count > 0 Then
                                                        Actual = acDt.DefaultView(0)("qty")
                                                        If Actual > 0 Then
                                                            lnq.TOTAL_ACTUAL += Actual
                                                        End If
                                                    End If

                                                    Dim Target As Integer = GetTarget(sDr, "W", ServiceDate)
                                                    If Actual > 0 Then
                                                        lnq.TOTAL_TARGET += Target
                                                    End If

                                                    Dim Percent As Integer = (Actual / Target) * 100
                                                    Dim Aht As String = 0 'GetAHTByWeek(ServiceID, lnq.USER_ID, ServiceDate, "W", shTrans)
                                                    If ahtDt.DefaultView.Count > 0 Then
                                                        Aht = ahtDt.DefaultView(0)("aht")
                                                    End If

                                                    Dim Sht As String = 0 'GetSHTByWeek(ServiceID, lnq.USER_ID, ServiceDate, "SHT", "W", shTrans)
                                                    Dim Cht As String = 0 'GetSHTByWeek(ServiceID, lnq.USER_ID, ServiceDate, "CHT", "W", shTrans)
                                                    If shtDt.DefaultView.Count > 0 Then
                                                        Sht = shtDt.DefaultView(0)("sht")
                                                        Cht = shtDt.DefaultView(0)("cht")
                                                    End If

                                                    'lnq.DATA_COLUMN = "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT|SHT|CHT|TotTarget"
                                                    Dim TmpValue As String = ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht & "|" & Sht & "|" & Cht
                                                    If lnq.DATA_VALUE = "" Then
                                                        lnq.DATA_VALUE = TmpValue
                                                    Else
                                                        lnq.DATA_VALUE += "###" & TmpValue
                                                    End If
                                                Next
                                                sDt.Dispose()
                                            End If
                                        End If

                                        'Total Column
                                        If lnq.TOTAL_TARGET > 0 Then
                                            lnq.TOTAL_PER_ACHIEVE_TARGET = (lnq.TOTAL_ACTUAL * 100) / lnq.TOTAL_TARGET
                                        End If

                                        shtDt.DefaultView.RowFilter = "user_id='" & stDT.Rows(i)("id") & "'"
                                        Dim SumHT As Long = 0
                                        Dim CountHT As Integer = 0
                                        For Each shtDV As DataRowView In shtDt.DefaultView
                                            SumHT += Convert.ToInt64(shtDV("sht"))
                                            CountHT += Convert.ToInt16(shtDV("cht"))
                                        Next
                                        lnq.TOTAL_AHT = 0
                                        If CountHT > 0 Then
                                            lnq.TOTAL_AHT = Math.Round(SumHT / CountHT)
                                        End If

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByWeek", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsProductivityENG")
                                        End If
                                        lnq = Nothing
                                    Catch ex As Exception
                                        FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByWeek", "Exception : " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsProductivityENG")
                                    End Try
                                Next
                                acDt.Dispose()
                                ahtDt.Dispose()
                                shtDt.Dispose()
                            End If
                        End If
                        stLnq = Nothing
                        stDT.Dispose()
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : ProductivityReportByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_PRODUCTIVITY_MONTH where month_no = '" & ServiceDate.Month & "' and show_year = '" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        'หา รายชื่อ Staff ของ Shop
                        Dim stLnq As New ShLinqDB.TABLE.TbUserShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByMonth")
                        Dim stDT As DataTable = stLnq.GetDataList("active_status = '1' and upper(username) <> 'ADMIN'", "fname, lname", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If stDT.Rows.Count > 0 Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByMonth")
                            If shTrans.Trans IsNot Nothing Then
                                Dim acDt As DataTable = GetActualByMonth(ServiceDate, shTrans)
                                Dim ahtDt As DataTable = GetAHTByMonth(ServiceDate, shTrans)
                                Dim shtDt As DataTable = GetSHTByMonth(ServiceDate, shTrans)
                                shTrans.CommitTransaction()
                                For i As Integer = 0 To stDT.Rows.Count - 1
                                    Try
                                        Dim lnq As New CenLinqDB.TABLE.TbRepProductivityMonthCenLinqDB
                                        lnq.SHOP_ID = shLnq.ID
                                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                        lnq.MONTH_NO = ServiceDate.Month
                                        lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                        lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                        lnq.USER_ID = stDT.Rows(i)("id")
                                        lnq.USER_CODE = stDT.Rows(i)("user_code")
                                        lnq.USER_NAME = stDT.Rows(i)("username")
                                        lnq.STAFF_NAME = stDT.Rows(i)("fname") & " " & stDT.Rows(i)("lname")
                                        'หา Service ของ Shop
                                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsProductivityENG.ProcReportByMonth")
                                        If shTrans.Trans IsNot Nothing Then
                                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                                            shTrans.CommitTransaction()
                                            If sDt.Rows.Count > 0 Then
                                                For Each sDr As DataRow In sDt.Rows
                                                    Dim ServiceID As Integer = sDr("id")
                                                    If acDt.Rows.Count > 0 Then
                                                        acDt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and user_id='" & lnq.USER_ID & "'"
                                                    End If

                                                    If ahtDt.Rows.Count > 0 Then
                                                        ahtDt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and user_id='" & lnq.USER_ID & "'"
                                                    End If

                                                    If shtDt.Rows.Count > 0 Then
                                                        shtDt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and user_id='" & lnq.USER_ID & "'"
                                                    End If

                                                    Dim Actual As Integer = 0 'GetActual(sDr("id"), lnq.USER_ID, ServiceDate, "W", shTrans)
                                                    If acDt.DefaultView.Count > 0 Then
                                                        Actual = acDt.DefaultView(0)("qty")
                                                        If Actual > 0 Then
                                                            lnq.TOTAL_ACTUAL += Actual
                                                        End If
                                                    End If

                                                    Dim Target As Integer = GetTarget(sDr, "M", ServiceDate)
                                                    If Actual > 0 Then
                                                        lnq.TOTAL_TARGET += Target
                                                    End If

                                                    Dim Percent As Integer = (Actual / Target) * 100
                                                    Dim Aht As String = 0 'GetAHT(ServiceID, lnq.USER_ID, ServiceDate, "W", shTrans)
                                                    If ahtDt.DefaultView.Count > 0 Then
                                                        Aht = ahtDt.DefaultView(0)("aht")
                                                    End If

                                                    Dim Sht As String = 0 'GetSHT(ServiceID, lnq.USER_ID, ServiceDate, "SHT", "W", shTrans)
                                                    Dim Cht As String = 0 'GetSHT(ServiceID, lnq.USER_ID, ServiceDate, "CHT", "W", shTrans)
                                                    If shtDt.DefaultView.Count > 0 Then
                                                        Sht = shtDt.DefaultView(0)("sht")
                                                        Cht = shtDt.DefaultView(0)("cht")
                                                    End If

                                                    Dim TmpValue As String = ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht & "|" & Sht & "|" & Cht
                                                    If lnq.DATA_VALUE = "" Then
                                                        lnq.DATA_VALUE = TmpValue
                                                    Else
                                                        lnq.DATA_VALUE += "###" & TmpValue
                                                    End If
                                                Next
                                                sDt.Dispose()
                                            End If
                                        End If

                                        'Total Column
                                        If lnq.TOTAL_TARGET > 0 Then
                                            lnq.TOTAL_PER_ACHIEVE_TARGET = (lnq.TOTAL_ACTUAL * 100) / lnq.TOTAL_TARGET
                                        End If

                                        If shtDt.Rows.Count > 0 Then
                                            shtDt.DefaultView.RowFilter = "user_id='" & stDT.Rows(i)("id") & "'"
                                            Dim SumHT As Long = 0
                                            Dim CountHT As Integer = 0
                                            For Each shtDV As DataRowView In shtDt.DefaultView
                                                SumHT += Convert.ToInt64(shtDV("sht"))
                                                CountHT += Convert.ToInt16(shtDV("cht"))
                                            Next
                                            lnq.TOTAL_AHT = 0
                                            If CountHT > 0 Then
                                                lnq.TOTAL_AHT = Math.Round(SumHT / CountHT)
                                            End If
                                        End If

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsProductivityENG")
                                        End If
                                        lnq = Nothing
                                    Catch ex As Exception
                                        FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByMonth", shLnq.SHOP_ABB & " Exception : " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsProductivityENG")
                                    End Try
                                Next
                                acDt.Dispose()
                                ahtDt.Dispose()
                                shtDt.Dispose()
                            Else
                                UpdateProcessError(ProcID, shTrans.ErrorMessage)
                            End If
                        End If
                        stLnq = Nothing
                        stDT.Dispose()
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
        End Sub

        'Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_PRODUCTIVITY_MONTH where month_no = '" & ServiceDate.Month & "' and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("ProductivityReportByMonth", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        'หา รายชื่อ Staff ของ Shop
        '                        Dim stLnq As New ShLinqDB.TABLE.TbUserShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        Dim stDT As DataTable = stLnq.GetDataList("active_status = '1' and upper(username) <> 'ADMIN' ", "fname, lname", shTrans.Trans)
        '                        shTrans.CommitTransaction()
        '                        If stDT.Rows.Count > 0 Then
        '                            For i As Integer = 0 To stDT.Rows.Count - 1
        '                                Dim lnq As New CenLinqDB.TABLE.TbRepProductivityMonthCenLinqDB
        '                                lnq.SHOP_ID = shLnq.ID
        '                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                                lnq.MONTH_NO = ServiceDate.Month
        '                                lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
        '                                lnq.SHOW_YEAR = ServiceDate.Year
        '                                lnq.USER_ID = stDT.Rows(i)("id")
        '                                lnq.USER_CODE = stDT.Rows(i)("user_code")
        '                                lnq.USER_NAME = stDT.Rows(i)("username")
        '                                lnq.STAFF_NAME = stDT.Rows(i)("fname") & " " & stDT.Rows(i)("lname")
        '                                lnq.PER_ACHIEVE_TARGET = 0
        '                                'หา Service ของ Shop
        '                                Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                If shTrans.Trans IsNot Nothing Then
        '                                    Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
        '                                    shTrans.CommitTransaction()
        '                                    If sDt.Rows.Count > 0 Then
        '                                        For Each sDr As DataRow In sDt.Rows
        '                                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                            Dim ServiceID As Integer = sDr("id")
        '                                            Dim Target As Integer = GetTarget(sDr)
        '                                            Dim Actual As Integer = GetActual(sDr("id"), lnq.USER_ID, ServiceDate, "M", shTrans)
        '                                            Dim Percent As Integer = (Actual / Target) * 100
        '                                            Dim Aht As String = GetAHT(ServiceID, lnq.USER_ID, ServiceDate, "M", shTrans)
        '                                            shTrans.CommitTransaction()
        '                                            If lnq.DATA_COLUMN = "" Then
        '                                                lnq.DATA_COLUMN = "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT"
        '                                                lnq.DATA_VALUE = ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht
        '                                            Else
        '                                                lnq.DATA_COLUMN += "###" & "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT"
        '                                                lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht
        '                                            End If
        '                                        Next
        '                                        sDt.Dispose()
        '                                        sDt = Nothing
        '                                    End If
        '                                End If

        '                                trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                                Dim ret As Boolean = False
        '                                ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                                If ret = True Then
        '                                    trans.CommitTransaction()
        '                                Else
        '                                    trans.RollbackTransaction()
        '                                    FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByMonth", lnq.ErrorMessage)
        '                                End If
        '                            Next
        '                            stDT.Dispose()
        '                            stDT = Nothing
        '                        End If
        '                    End If
        '                End If
        '            Next
        '            UpdateProcessTime(ProcID)
        '        End If
        '        dtS.Dispose()
        '        dtS = Nothing
        '    End If
        'End Sub

        'Public Sub ProcReportByYear(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_PRODUCTIVITY_YEAR where show_quarter=" & DatePart(DateInterval.Quarter, ServiceDate) & " and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("ProductivityReportByYear", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        'หา รายชื่อ Staff ของ Shop
        '                        Dim stLnq As New ShLinqDB.TABLE.TbUserShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        Dim stDT As DataTable = stLnq.GetDataList("active_status = '1' and upper(username) <> 'ADMIN'", "fname, lname", shTrans.Trans)
        '                        shTrans.CommitTransaction()
        '                        If stDT.Rows.Count > 0 Then
        '                            For i As Integer = 0 To stDT.Rows.Count - 1
        '                                Dim lnq As New CenLinqDB.TABLE.TbRepProductivityYearCenLinqDB
        '                                lnq.SHOP_ID = shLnq.ID
        '                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                                lnq.SHOW_QUARTER = DatePart(DateInterval.Quarter, ServiceDate)
        '                                lnq.SHOW_YEAR = ServiceDate.Year
        '                                lnq.USER_ID = stDT.Rows(i)("id")
        '                                lnq.USER_CODE = stDT.Rows(i)("user_code")
        '                                lnq.USER_NAME = stDT.Rows(i)("username")
        '                                lnq.STAFF_NAME = stDT.Rows(i)("fname") & " " & stDT.Rows(i)("lname")
        '                                lnq.PER_ACHIEVE_TARGET = 0
        '                                'หา Service ของ Shop
        '                                Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                If shTrans.Trans IsNot Nothing Then
        '                                    Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
        '                                    shTrans.CommitTransaction()
        '                                    If sDt.Rows.Count > 0 Then
        '                                        For Each sDr As DataRow In sDt.Rows
        '                                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                            Dim ServiceID As Integer = sDr("id")
        '                                            Dim Target As Integer = GetTarget(sDr)
        '                                            Dim Actual As Integer = GetActual(sDr("id"), lnq.USER_ID, ServiceDate, "M", shTrans)
        '                                            Dim Percent As Integer = (Actual / Target) * 100
        '                                            Dim Aht As String = GetAHT(ServiceID, lnq.USER_ID, ServiceDate, "M", shTrans)
        '                                            shTrans.CommitTransaction()
        '                                            If lnq.DATA_COLUMN = "" Then
        '                                                lnq.DATA_COLUMN = "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT"
        '                                                lnq.DATA_VALUE = ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht
        '                                            Else
        '                                                lnq.DATA_COLUMN += "###" & "ServiceID|ServiceNameEN|ServiceNameTH|Target|Actual|Percent|AHT|SHT|CHT"
        '                                                lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & Target & "|" & Actual & "|" & Percent & "|" & Aht
        '                                            End If
        '                                        Next
        '                                        sDt.Dispose()
        '                                        sDt = Nothing
        '                                    End If
        '                                End If

        '                                trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                                Dim ret As Boolean = False
        '                                ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                                If ret = True Then
        '                                    trans.CommitTransaction()
        '                                Else
        '                                    trans.RollbackTransaction()
        '                                    FunctionEng.SaveErrorLog("ReportsProductivityENG.ProcReportByYear", lnq.ErrorMessage)
        '                                End If
        '                            Next
        '                            stDT.Dispose()
        '                            stDT = Nothing
        '                        End If
        '                    End If
        '                End If
        '            Next
        '            UpdateProcessTime(ProcID)
        '        End If
        '        dtS.Dispose()
        '        dtS = Nothing
        '    End If
        'End Sub

        Private Function GetPrimaryTarget(ByVal ServiceDate As Date, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer
            Dim ret As Integer = 0
            Dim sql As String = "select ti.item_time"
            sql += " from TB_USER_SERVICE_SCHEDULE usc"
            sql += " inner join TB_ITEM ti on ti.id=usc.item_id"
            sql += " where CONVERT(varchar(8),usc.service_date,112)='" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            sql += " and usc.[user_id]='" & UserID & "' and usc.[priority]='1' and usc.active_status='1'"
            Dim lnq As New ShLinqDB.TABLE.TbUserServiceScheduleShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                If Convert.ToInt64(dt.Rows(0)("item_time")) <> 0 Then
                    ret = 450 / dt.Rows(0)("item_time")
                End If
            End If
            dt = Nothing
            lnq = Nothing

            Return ret
        End Function

        Private Function GetTarget(ByVal sDr As DataRow, ByVal rtType As String, ByVal ServiceDate As DateTime) As Integer
            'เวลาทำงานคือ 7.30 ชั่วโมง = 450 นาที
            If Convert.ToInt64(sDr("item_time")) <> 0 Then
                Dim DayMin As Int16 = 450
                Select Case rtType.ToUpper
                    Case "D"
                        Return Math.Round(Convert.ToDouble(DayMin / Convert.ToInt64(sDr("item_time"))), MidpointRounding.AwayFromZero)
                    Case "W"
                        Return Math.Round(Convert.ToDouble((DayMin * 7) / Convert.ToInt64(sDr("item_time"))))
                    Case "M"
                        Return Math.Round(Convert.ToDouble((DayMin * DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)) / Convert.ToInt64(sDr("item_time"))))
                End Select
            Else
                Return 0
            End If
        End Function

        Private Function GetActual(ByVal ServiceID As Integer, ByVal UserID As Integer, ByVal ServiceDate As Date, ByVal Para As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer
            Dim sql As String = ""
            sql += "select COUNT(id) id"
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where user_id=" & UserID & " and item_id=" & ServiceID & " and status=3 "
            Select Case Para.ToUpper
                Case "D"
                    sql += " and convert(varchar(8),service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                Case "W"
                    Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
                Case "M"
                    sql += " and convert(varchar(6),service_date,112) = '" & ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "' "
                    'Case "Y"
                    '    sql += " and year(service_date) = " & ServiceDate.Year & " and DATEPART(QUARTER,service_date) = " & DatePart(DateInterval.Quarter, ServiceDate)
            End Select


            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            Dim ret As Integer = 0
            If dt.Rows.Count > 0 Then
                If Convert.IsDBNull(dt.Rows(0)("id")) = False Then
                    ret = dt.Rows(0)("id")
                End If
                dt.Dispose()
                dt = Nothing
            End If
            lnq = Nothing

            Return ret
        End Function

        Private Function GetAHT(ByVal ServiceID As Integer, ByVal UserID As Integer, ByVal ServiceDate As Date, ByVal Para As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim sql As String = ""
            sql += " select avg(DATEDIFF(second, start_time,end_time)) aht "
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where user_id=" & UserID & " and item_id=" & ServiceID & " and status=3 "

            Select Case Para.ToUpper
                Case "D"
                    sql += " and convert(varchar(8),service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                Case "W"
                    Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "'"
                Case "M"
                    sql += " and month(service_date) = " & ServiceDate.Month & " and year(service_date)= " & ServiceDate.Year
                    'Case "Y"
                    '    sql += " and year(service_date) = " & ServiceDate.Year & " and DATEPART(QUARTER,service_date) = " & DatePart(DateInterval.Quarter, ServiceDate)
            End Select

            Dim ret As String = ""
            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                If Convert.IsDBNull(dt.Rows(0)("aht")) = False Then
                    ret = ReportsENG.GetFormatTimeFromSec(dt.Rows(0)("aht"))
                End If
                dt.Dispose()
                dt = Nothing
            End If
            lnq = Nothing

            Return ret
        End Function

        Private Function GetSHT(ByVal ServiceID As Integer, ByVal UserID As Integer, ByVal ServiceDate As Date, ByVal WT As String, ByVal Para As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Int32
            'sht = Service Handling Time
            'cht = Count Handling Time (จำนวนคิวที่มีการ END)
            Dim sql As String = ""
            sql += " select sum(DATEDIFF(second, start_time,end_time)) sht,count(1) as cht "
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where user_id=" & UserID & " and item_id=" & ServiceID & " and status=3 "

            Select Case Para.ToUpper
                Case "D"
                    sql += " and convert(varchar(8),service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                Case "W"
                    Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                    sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
                Case "M"
                    sql += " and month(service_date) = " & ServiceDate.Month & " and year(service_date)= " & ServiceDate.Year
                    'Case "Y"
                    '    sql += " and year(service_date) = " & ServiceDate.Year & " and DATEPART(QUARTER,service_date) = " & DatePart(DateInterval.Quarter, ServiceDate)
            End Select

            Dim ret As Int32 = 0
            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                If WT = "SHT" Then
                    If Convert.IsDBNull(dt.Rows(0)("sht")) = False Then
                        ret = dt.Rows(0)("sht")
                    End If
                Else
                    If Convert.IsDBNull(dt.Rows(0)("cht")) = False Then
                        ret = dt.Rows(0)("cht")
                    End If
                End If
                dt.Dispose()
                dt = Nothing
            End If
            lnq = Nothing

            Return ret
        End Function


#Region "By Week"
        Private Function GetActualByWeek(ByVal ServiceDate As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = ""
            sql += "select COUNT(id) qty, item_id, user_id"
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where status=3 "
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " group by item_id, user_id"

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetAHTByWeek(ByVal ServiceDate As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = ""
            sql += " select isnull(avg(DATEDIFF(second, start_time,end_time)),0) aht, "
            sql += " item_id, user_id "
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where  status=3 "
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "'"
            sql += " group by item_id, user_id"

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetSHTByWeek(ByVal ServiceDate As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            'sht = Service Handling Time
            'cht = Count Handling Time (จำนวนคิวที่มีการ END)

            Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = ""
            sql += " select sum(DATEDIFF(second, start_time,end_time)) sht,count(1) as cht, item_id, user_id "
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where status=3 "
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " group by item_id, user_id"

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function
#End Region

#Region "By Month"
        Private Function GetActualByMonth(ByVal ServiceDate As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
            Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

            Dim sql As String = ""
            sql += "select COUNT(id) qty, item_id,user_id"
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where status=3 "
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " group by item_id, user_id"

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetAHTByMonth(ByVal ServiceDate As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
            Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

            Dim sql As String = ""
            sql += " select avg(DATEDIFF(second, start_time,end_time)) aht, item_id, user_id "
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where  status=3 "
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " group by item_id, user_id"

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetSHTByMonth(ByVal ServiceDate As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            'sht = Service Handling Time
            'cht = Count Handling Time (จำนวนคิวที่มีการ END)

            Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
            Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

            Dim sql As String = ""
            sql += " select sum(DATEDIFF(second, start_time,end_time)) sht,count(1) as cht, item_id,user_id "
            sql += " from TB_COUNTER_QUEUE_HISTORY "
            sql += " where status=3 "
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " group by item_id, user_id"

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function
#End Region
    End Class
End Namespace

