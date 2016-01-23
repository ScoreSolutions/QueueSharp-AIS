Imports Engine.Common
Imports System.Windows.Forms

Namespace Reports
    Public Class ReportsCapacityReportByShopENG : Inherits ReportsENG

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

        Public Sub CapacityReportProcessAllReport()
            ProcReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CapacityReportByShopByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_CAPACITY_SHOP_MONTH where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As New Date(ServiceDate.Year, ServiceDate.Month, 1)
                Dim LastDay As New Date(ServiceDate.Year, ServiceDate.Month, DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCapacityReportByShopENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetDataByMonth(FirstDay, LastDay, shTrans)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim ServiceID As String = dr("id")
                                Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopMonthCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.MONTH_NO = ServiceDate.Month
                                lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = dr("item_name")
                                lnq.SERVICE_NAME_TH = dr("item_name_th")
                                lnq.WORKING_HOUR = GetWorkingHour(DateDiff(DateInterval.Day, FirstDay, LastDay) + 1)
                                lnq.KPI = dr("kpi")
                                lnq.NO_COUNTER = GetCounterQty(FirstDay, LastDay, ServiceID, shTrans)
                                If dr("kpi") > 0 Then lnq.CAPACITY_TRANS = Math.Round((lnq.WORKING_HOUR * lnq.NO_COUNTER) / dr("kpi"))
                                lnq.APPOINTMENT = GetAppointmentDataByDate(FirstDay, LastDay, ServiceID, shTrans).Rows.Count
                                lnq.EXPECTED_WALK_IN = dr("expt")
                                lnq.TOTAL_TO_BE_SERVE = Convert.ToInt64(dr("tot"))
                                If lnq.CAPACITY_TRANS = 0 Then
                                    lnq.EXPECTED_CAPACITY_USED = 0
                                    lnq.EXPECTED_OPEN_COUNTER = 0
                                Else
                                    lnq.EXPECTED_CAPACITY_USED = (lnq.TOTAL_TO_BE_SERVE / lnq.CAPACITY_TRANS) * 100
                                    lnq.EXPECTED_OPEN_COUNTER = Math.Round((lnq.TOTAL_TO_BE_SERVE - lnq.CAPACITY_TRANS) / lnq.CAPACITY_TRANS, MidpointRounding.AwayFromZero)
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCapacityReportByShopENG.ProcReportByMonth", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCapacityReportByShopENG")
                                End If
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            dt.Dispose()
                        End If
                        shTrans.CommitTransaction()
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CapacityReportByShopByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_CAPACITY_SHOP_WEEK where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
                FirstDay = New Date(FirstDay.Year, FirstDay.Month, FirstDay.Day)

                Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)
                LastDay = New Date(LastDay.Year, LastDay.Month, LastDay.Day)

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCapacityReportByShopENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetDataByWeek(FirstDay, LastDay, shTrans)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim ServiceID As String = dr("id")
                                Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopWeekCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.WEEK_OF_YEAR = WeekNo
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "-" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = dr("item_name")
                                lnq.SERVICE_NAME_TH = dr("item_name_th")
                                lnq.WORKING_HOUR = GetWorkingHour(DateDiff(DateInterval.Day, FirstDay, LastDay) + 1)
                                lnq.KPI = dr("kpi")
                                lnq.NO_COUNTER = GetCounterQty(FirstDay, LastDay, ServiceID, shTrans)
                                If dr("kpi") > 0 Then lnq.CAPACITY_TRANS = Math.Round((lnq.WORKING_HOUR * lnq.NO_COUNTER) / dr("kpi"))
                                lnq.APPOINTMENT = GetAppointmentDataByDate(FirstDay, LastDay, ServiceID, shTrans).Rows.Count
                                lnq.EXPECTED_WALK_IN = dr("expt")
                                lnq.TOTAL_TO_BE_SERVE = Convert.ToInt64(dr("tot"))
                                If lnq.CAPACITY_TRANS = 0 Then
                                    lnq.EXPECTED_CAPACITY_USED = 0
                                    lnq.EXPECTED_OPEN_COUNTER = 0
                                Else
                                    lnq.EXPECTED_CAPACITY_USED = (lnq.TOTAL_TO_BE_SERVE / lnq.CAPACITY_TRANS) * 100
                                    lnq.EXPECTED_OPEN_COUNTER = Math.Round((lnq.TOTAL_TO_BE_SERVE - lnq.CAPACITY_TRANS) / lnq.CAPACITY_TRANS, MidpointRounding.AwayFromZero)
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCapacityReportByShopENG.ProcReportByWeek", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCapacityReportByShopENG")
                                End If
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            dt.Dispose()
                        End If
                        shTrans.CommitTransaction()
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CapacityReportByShopByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_CAPACITY_SHOP_DAY where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCapacityReportByShopENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetDataByDate(ServiceDate, shTrans)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim ServiceID As String = dr("id")
                                Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopDayCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.SERVICE_DATE = ServiceDate
                                lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = dr("item_name")
                                lnq.SERVICE_NAME_TH = dr("item_name_th")
                                lnq.WORKING_HOUR = GetWorkingHour(1)
                                lnq.KPI = dr("kpi")
                                lnq.NO_COUNTER = GetCounterQty(ServiceDate, ServiceDate, ServiceID, shTrans)
                                If dr("kpi") > 0 Then lnq.CAPACITY_TRANS = Math.Round((lnq.WORKING_HOUR * lnq.NO_COUNTER) / dr("kpi"), MidpointRounding.AwayFromZero)
                                lnq.APPOINTMENT = GetAppointmentDataByDate(ServiceDate, ServiceDate, ServiceID, shTrans).Rows.Count
                                lnq.EXPECTED_WALK_IN = dr("expt")
                                lnq.TOTAL_TO_BE_SERVE = Convert.ToInt64(dr("tot"))
                                If lnq.CAPACITY_TRANS = 0 Then
                                    lnq.EXPECTED_CAPACITY_USED = 0
                                    lnq.EXPECTED_OPEN_COUNTER = 0
                                Else
                                    lnq.EXPECTED_CAPACITY_USED = (lnq.TOTAL_TO_BE_SERVE / lnq.CAPACITY_TRANS) * 100
                                    lnq.EXPECTED_OPEN_COUNTER = Math.Round((lnq.TOTAL_TO_BE_SERVE - lnq.CAPACITY_TRANS) / lnq.CAPACITY_TRANS, MidpointRounding.AwayFromZero)
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCapacityReportByShopENG.ProcReportByDate", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCapacityReportByShopENG")
                                End If
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            dt.Dispose()
                        End If
                        shTrans.CommitTransaction()
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)

            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CapacityReportByShopByTime", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_CAPACITY_SHOP_TIME where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCapacityReportByShopENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        trans.CommitTransaction()

                        If dtI.Rows.Count > 0 Then
                            For Each drI As DataRow In dtI.Rows
                                Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))

                                'Loop ตามเวลาที่ เปิด ปิด Shop
                                Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
                                Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)
                                Dim CurrTime As DateTime = StartTime
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCapacityReportByShopENG.ProcReportByTime")
                                If shTrans.Trans IsNot Nothing Then
                                    Do
                                        If CurrTime < EndTime Then
                                            Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                            If TimeTo > EndTime Then
                                                TimeTo = EndTime
                                            End If

                                            Dim dt As DataTable = GetDataByTime(ServiceDate, CurrTime.ToString("HH:mm"), TimeTo.ToString("HH:mm"), shTrans)
                                            If dt.Rows.Count > 0 Then
                                                For Each dr As DataRow In dt.Rows
                                                    Dim ServiceID As String = dr("id")
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopTimeCenLinqDB

                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                                    lnq.SERVICE_ID = ServiceID
                                                    lnq.SERVICE_NAME_EN = dr("item_name")
                                                    lnq.SERVICE_NAME_TH = dr("item_name_th")
                                                    lnq.WORKING_HOUR = IntervalMinute
                                                    lnq.KPI = dr("kpi")
                                                    lnq.NO_COUNTER = GetCounterQty(ServiceDate, ServiceDate, ServiceID, shTrans)
                                                    If dr("kpi") > 0 Then lnq.CAPACITY_TRANS = Math.Round((lnq.WORKING_HOUR * lnq.NO_COUNTER) / dr("kpi"))
                                                    lnq.APPOINTMENT = GetAppointmentDataByTime(CurrTime, TimeTo, ServiceID, shTrans).Rows.Count
                                                    lnq.EXPECTED_WALK_IN = dr("expt")
                                                    lnq.TOTAL_TO_BE_SERVE = Convert.ToInt64(dr("tot"))
                                                    If lnq.CAPACITY_TRANS = 0 Then
                                                        lnq.EXPECTED_CAPACITY_USED = 0
                                                        lnq.EXPECTED_OPEN_COUNTER = 0
                                                    Else
                                                        lnq.EXPECTED_CAPACITY_USED = (lnq.TOTAL_TO_BE_SERVE / lnq.CAPACITY_TRANS) * 100
                                                        lnq.EXPECTED_OPEN_COUNTER = Math.Round((lnq.TOTAL_TO_BE_SERVE - lnq.CAPACITY_TRANS) / lnq.CAPACITY_TRANS, MidpointRounding.AwayFromZero)
                                                    End If

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsCapacityReportByShopENG.ProcReportByTime", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCapacityReportByShopENG")
                                                    End If
                                                    lnq = Nothing

                                                    Try
                                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                        Application.DoEvents()
                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                                dt.Dispose()
                                            End If
                                        End If
                                        CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                    Loop While CurrTime <= EndTime
                                    shTrans.CommitTransaction()
                                End If
                            Next
                            dtI.Dispose()
                            dtI = Nothing
                        End If
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetCounterQty(ByVal ServiceDateFrom As DateTime, ByVal ServiceDateTo As DateTime, ByVal ServiceID As Integer, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer
            Dim DateFrom As String = ServiceDateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim DateTo As String = ServiceDateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = ""
            sql += " select COUNT(1) as qty from TB_USER_SERVICE_SCHEDULE "
            sql += " where item_id = " & ServiceID & " and priority = 1 "
            sql += " and CONVERT(varchar(8),service_date,112) between '" & DateFrom & "' and '" & DateTo & "'" & vbNewLine

            Dim ret As Integer = 0
            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                If Convert.IsDBNull(dt.Rows(0)("qty")) = False Then
                    ret = dt.Rows(0)("qty")
                End If
                dt.Dispose()
            End If

            Return ret
        End Function

        Private Function GetDataByDate(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""

            sql += " select tb_item.id,item_name,item_name_th,item_time as kpi" & vbNewLine
            sql += " ,isnull(expt,0) as expt,isnull(tot,0) tot from tb_item " & vbNewLine
            sql += " Left Join" & vbNewLine
            sql += " (" & vbNewLine
            sql += " select item_id,COUNT(1) as expt from" & vbNewLine
            sql += " TB_COUNTER_QUEUE_HISTORY " & vbNewLine
            sql += " where CONVERT(varchar(8),service_date,112) = '" & ServiceDate.AddDays(-1).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' " & vbNewLine
            sql += " group by item_id " & vbNewLine
            sql += " ) as expt on tb_item.id = expt.item_id" & vbNewLine
            sql += " Left Join" & vbNewLine
            sql += " (" & vbNewLine
            sql += " select item_id,COUNT(1) as tot from" & vbNewLine
            sql += " TB_COUNTER_QUEUE_HISTORY where status = 3" & vbNewLine
            sql += " and CONVERT(varchar(8),service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' " & vbNewLine
            sql += " group by item_id " & vbNewLine
            sql += " ) as tot on tb_item.id = tot.item_id" & vbNewLine
            sql += " where active_status = 1 " & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            Return dt
        End Function

        Private Function GetDataByWeek(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""

            sql += " select tb_item.id,item_name,item_name_th,item_time as kpi" & vbNewLine
            sql += " ,isnull(expt,0) as expt,isnull(tot,0) tot from tb_item " & vbNewLine
            sql += " Left Join" & vbNewLine
            sql += " (" & vbNewLine
            sql += " select item_id,COUNT(1) as expt from" & vbNewLine
            sql += " TB_COUNTER_QUEUE_HISTORY " & vbNewLine
            sql += " where CONVERT(varchar(8),service_date,112) between '" & FirstDay.AddDays(-7).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & LastDay.AddDays(-7).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
            sql += " group by item_id " & vbNewLine
            sql += " ) as expt on tb_item.id = expt.item_id" & vbNewLine
            sql += " Left Join" & vbNewLine
            sql += " (" & vbNewLine
            sql += " select item_id,COUNT(1) as tot from" & vbNewLine
            sql += " TB_COUNTER_QUEUE_HISTORY where status = 3" & vbNewLine
            sql += " and CONVERT(varchar(8),service_date,112) between '" & FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
            sql += " group by item_id " & vbNewLine
            sql += " ) as tot on tb_item.id = tot.item_id" & vbNewLine
            sql += " where active_status = 1 " & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            Return dt
        End Function

        Private Function GetDataByMonth(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim LastMonth As Date = FirstDay.AddMonths(-1)
            Dim LastMonthFirstDay As String = LastMonth.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim LastMonthLastDay As New Date(LastMonth.Year, LastMonth.Month, DateTime.DaysInMonth(LastMonth.Year, LastMonth.Month))

            'Dim sql As String = ""
            'sql += " select tb_item.id,item_name,item_name_th,item_time as kpi" & vbNewLine
            'sql += " ,isnull(expt,0) as expt,isnull(tot,0) tot from tb_item " & vbNewLine
            'sql += " Left Join" & vbNewLine
            'sql += " (" & vbNewLine
            'sql += " select item_id,COUNT(1) as expt from" & vbNewLine
            'sql += " TB_COUNTER_QUEUE_HISTORY " & vbNewLine
            'sql += " where CONVERT(varchar(8),service_date,112) >= '" & LastMonthFirstDay & "' and CONVERT(varchar(8),service_date,112) <= '" & LastMonthLastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
            'sql += " group by item_id " & vbNewLine
            'sql += " ) as expt on tb_item.id = expt.item_id" & vbNewLine
            'sql += " Left Join" & vbNewLine
            'sql += " (" & vbNewLine
            'sql += " select item_id,COUNT(1) as tot from" & vbNewLine
            'sql += " TB_COUNTER_QUEUE_HISTORY where status = 3" & vbNewLine
            'sql += " and CONVERT(varchar(8),service_date,112) >= '" & FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and CONVERT(varchar(8),service_date,112) <= '" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
            'sql += " group by item_id " & vbNewLine
            'sql += " ) as tot on tb_item.id = tot.item_id" & vbNewLine
            'sql += " where active_status = 1 " & vbNewLine



            Dim sql As String = ""
            sql += " select tb_item.id,item_name,item_name_th,item_time as kpi" & vbNewLine
            sql += " from tb_item " & vbNewLine
            sql += " where active_status = 1 " & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                dt.Columns.Add("expt", GetType(Long))
                dt.Columns.Add("tot", GetType(Long))

                sql = " select item_id,COUNT(1) as expt from" & vbNewLine
                sql += " TB_COUNTER_QUEUE_HISTORY " & vbNewLine
                sql += " where CONVERT(varchar(8),service_date,112) >= '" & LastMonthFirstDay & "' and CONVERT(varchar(8),service_date,112) <= '" & LastMonthLastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
                sql += " group by item_id " & vbNewLine
                Dim dtExpt As New DataTable
                dtExpt = lnq.GetListBySql(sql, shTrans.Trans)

                sql = " select item_id,COUNT(1) as tot from" & vbNewLine
                sql += " TB_COUNTER_QUEUE_HISTORY where status = 3" & vbNewLine
                sql += " and CONVERT(varchar(8),service_date,112) >= '" & FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and CONVERT(varchar(8),service_date,112) <= '" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
                sql += " group by item_id " & vbNewLine
                Dim dtTot As New DataTable
                dtTot = lnq.GetListBySql(sql, shTrans.Trans)

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)

                    If dtExpt.Rows.Count > 0 Then
                        dtExpt.DefaultView.RowFilter = "item_id='" & dr("id") & "'"
                        If dtExpt.DefaultView.Count > 0 Then
                            dt.Rows(i)("expt") = dtExpt.DefaultView(0)("expt")
                        Else
                            dt.Rows(i)("expt") = 0
                        End If
                        dtExpt.DefaultView.RowFilter = ""
                    Else
                        dt.Rows(i)("expt") = 0
                    End If
                    
                    If dtTot.Rows.Count > 0 Then
                        dtTot.DefaultView.RowFilter = "item_id='" & dr("id") & "'"
                        If dtTot.DefaultView.Count > 0 Then
                            dt.Rows(i)("tot") = dtTot.DefaultView(0)("tot")
                        Else
                            dt.Rows(i)("tot") = 0
                        End If
                        dtTot.DefaultView.RowFilter = ""
                    Else
                        dt.Rows(i)("tot") = 0
                    End If
                Next
                dtExpt.Dispose()
                dtTot.Dispose()
            End If


            lnq = Nothing
            Return dt
        End Function

        Private Function GetDataByTime(ByVal ServiceDate As DateTime, ByVal TimeFrom As String, ByVal TimeTo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql += " declare @Date as varchar(10); select @Date = '" & ServiceDate.AddDays(-1).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
            sql += " select tb_item.id,item_name,item_name_th,item_time as kpi" & vbNewLine
            sql += " ,isnull(expt,0) as expt, isnull(tot,0) tot from tb_item " & vbNewLine
            sql += " Left Join" & vbNewLine
            sql += " (" & vbNewLine
            sql += " select item_id,COUNT(1) as expt from" & vbNewLine
            sql += " TB_COUNTER_QUEUE_HISTORY " & vbNewLine
            sql += " where CONVERT(varchar(10),service_date,120) = @Date" & vbNewLine
            sql += " and convert(varchar(5), service_date, 114) >= '" & TimeFrom & "' "
            sql += " and convert(varchar(5), service_date, 114) < '" & TimeTo & "' "
            sql += " group by item_id " & vbNewLine
            sql += " ) as expt on tb_item.id = expt.item_id" & vbNewLine

            sql += " Left Join" & vbNewLine
            sql += " (" & vbNewLine
            sql += " select item_id,COUNT(1) as tot from" & vbNewLine
            sql += " TB_COUNTER_QUEUE_HISTORY where status = 3" & vbNewLine
            sql += " and CONVERT(varchar(10),service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
            sql += " and convert(varchar(5), service_date, 114) >= '" & TimeFrom & "' "
            sql += " and convert(varchar(5), service_date, 114) < '" & TimeTo & "' "
            sql += " group by item_id " & vbNewLine
            sql += " ) as tot on tb_item.id = tot.item_id" & vbNewLine

            sql += " where active_status = 1 " & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            lnq = Nothing
            Return dt
        End Function

        Private Function GetAppointmentDataByDate(ByVal SlotDateFrom As DateTime, ByVal SlotDateTo As DateTime, ByVal ServiceID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim dt As New DataTable

            Dim sql As String = " select ac.id, ac.app_date transaction_date, ac.start_slot appointment_date, ac.customer_id mobile_no,"
            sql += " ac.appointment_channel,ac.active_status appointment_status"
            sql += " from TB_APPOINTMENT_CUSTOMER ac"
            sql += " where CONVERT(varchar(10),ac.start_slot,120) between '" & SlotDateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & SlotDateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' "
            sql += " and ac.item_id='" & ServiceID & "'"
            'sql += " and ac.active_status='" & CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "'"

            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function

        Private Function GetAppointmentDataByTime(ByVal SlotDateFrom As DateTime, ByVal SlotDateTo As DateTime, ByVal ServiceID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim dt As New DataTable

            Dim sql As String = " select ac.id, ac.app_date transaction_date, ac.start_slot appointment_date, ac.customer_id mobile_no,"
            sql += " ac.appointment_channel,ac.active_status appointment_status"
            sql += " from TB_APPOINTMENT_CUSTOMER ac"
            sql += " where CONVERT(varchar(16),ac.start_slot,120) between '" & SlotDateFrom.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' and '" & SlotDateTo.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            sql += " and ac.item_id='" & ServiceID & "'"
            'sql += " and ac.active_status='" & CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "'"

            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
        
     
        Function GetWorkingHour(ByVal DayQty As Integer) As Int32
            Return 450 * DayQty
        End Function
    End Class
End Namespace

