Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class RepAverageServiceTimeComparingWithKPIENG : Inherits ReportsENG


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

        Public Sub ShopAVGCompareKPIProcessAllReport()
            ProcReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & "AverageServiceTimeComparingWidthKPIByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_MONTH where month_no='" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If sDt.Rows.Count > 0 Then
                            For Each sDr As DataRow In sDt.Rows
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByMonth")
                                Dim ServiceID As String = sDr("id")
                                Dim dt As DataTable = GetDataTableMonth(ServiceID, ServiceDate, shTrans)
                                shTrans.CommitTransaction()
                                If dt.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt.Rows
                                        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiMonthCenLinqDB
                                        lnq.SHOP_ID = shLnq.ID
                                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                        lnq.SERVICE_ID = ServiceID
                                        lnq.SERVICE_NAME_EN = sDr("item_name")
                                        lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                        lnq.MONTH_NO = ServiceDate.Month
                                        lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                        lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                        lnq.REGIS = Convert.ToInt64(dr("regis"))
                                        lnq.SERVED = Convert.ToInt64(dr("serve"))
                                        lnq.MISSED_CALL = Convert.ToInt64(dr("misscall"))
                                        lnq.CANCEL = Convert.ToInt64(dr("cancel"))
                                        lnq.WAIT_WITH_KPI = Convert.ToInt64(dr("wt_with_kpi"))
                                        lnq.SERVE_WITH_KPI = Convert.ToInt64(dr("ht_with_kpi"))
                                        lnq.NOT_CALL = Convert.ToInt64(dr("notcall"))
                                        lnq.NOT_CONFIRM = Convert.ToInt64(dr("notcon"))
                                        lnq.NOT_END = Convert.ToInt64(dr("notend"))
                                        lnq.MAX_WT = Convert.ToInt64(dr("max_wt"))
                                        lnq.MAX_HT = Convert.ToInt64(dr("max_ht"))

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByMonth", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                        End If
                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next
                                    dt.Dispose()
                                Else
                                    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiMonthCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.MONTH_NO = ServiceDate.Month
                                    lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                    lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.REGIS = 0
                                    lnq.SERVED = 0
                                    lnq.MISSED_CALL = 0
                                    lnq.CANCEL = 0
                                    lnq.WAIT_WITH_KPI = 0
                                    lnq.SERVE_WITH_KPI = 0
                                    lnq.NOT_CALL = 0
                                    lnq.NOT_CONFIRM = 0
                                    lnq.NOT_END = 0
                                    lnq.MAX_WT = 0
                                    lnq.MAX_HT = 0

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByMonth", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                    End If
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                End If
                            Next
                            sDt.Dispose()
                        End If
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shLnq.SHOP_ABB & " : " & shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & "AverageServiceTimeComparingWidthKPIByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_WEEK where week_of_year='" & WeekNo & "' and show_year='" & ServiceDate.Year & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
                Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If sDt.Rows.Count > 0 Then
                            For Each sDr As DataRow In sDt.Rows
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByWeek")
                                Dim ServiceID As String = sDr("id")
                                Dim dt As DataTable = GetDataTableWeek(ServiceID, FirstDay, LastDay, shTrans)
                                shTrans.CommitTransaction()
                                If dt.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt.Rows
                                        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiWeekCenLinqDB
                                        lnq.SHOP_ID = shLnq.ID
                                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                        lnq.SERVICE_ID = ServiceID
                                        lnq.SERVICE_NAME_EN = sDr("item_name")
                                        lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                        lnq.WEEK_OF_YEAR = WeekNo
                                        lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                        lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & " - " & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                        lnq.REGIS = Convert.ToInt64(dr("regis"))
                                        lnq.SERVED = Convert.ToInt64(dr("serve"))
                                        lnq.MISSED_CALL = Convert.ToInt64(dr("misscall"))
                                        lnq.CANCEL = Convert.ToInt64(dr("cancel"))
                                        lnq.WAIT_WITH_KPI = Convert.ToInt64(dr("wt_with_kpi"))
                                        lnq.SERVE_WITH_KPI = Convert.ToInt64(dr("ht_with_kpi"))
                                        lnq.NOT_CALL = Convert.ToInt64(dr("notcall"))
                                        lnq.NOT_CONFIRM = Convert.ToInt64(dr("notcon"))
                                        lnq.NOT_END = Convert.ToInt64(dr("notend"))
                                        lnq.MAX_WT = Convert.ToInt64(dr("max_wt"))
                                        lnq.MAX_HT = Convert.ToInt64(dr("max_ht"))

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByWeek", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                        End If
                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next
                                    dt.Dispose()
                                Else
                                    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiWeekCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.WEEK_OF_YEAR = WeekNo
                                    lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.REGIS = 0
                                    lnq.SERVED = 0
                                    lnq.MISSED_CALL = 0
                                    lnq.CANCEL = 0
                                    lnq.WAIT_WITH_KPI = 0
                                    lnq.SERVE_WITH_KPI = 0
                                    lnq.NOT_CALL = 0
                                    lnq.NOT_CONFIRM = 0
                                    lnq.NOT_END = 0
                                    lnq.MAX_WT = 0
                                    lnq.MAX_HT = 0

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByWeek", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                    End If
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                End If
                            Next
                            sDt.Dispose()
                        End If
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shLnq.SHOP_ABB & " : " & shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & "AverageServiceTimeComparingWidthKPIByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_DAY where convert(varchar(8),service_date,112)='" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If sDt.Rows.Count > 0 Then
                            For Each sDr As DataRow In sDt.Rows
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate")
                                Dim ServiceID As String = sDr("id")
                                Dim dt As DataTable = GetDataTableDate(ServiceID, ServiceDate, shTrans)
                                shTrans.CommitTransaction()
                                If dt.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt.Rows
                                        Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiDayCenLinqDB
                                        lnq.SHOP_ID = shLnq.ID
                                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                        lnq.SERVICE_ID = ServiceID
                                        lnq.SERVICE_NAME_EN = sDr("item_name")
                                        lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                        lnq.SERVICE_DATE = ServiceDate
                                        lnq.REGIS = Convert.ToInt64(dr("regis"))
                                        lnq.SERVED = Convert.ToInt64(dr("serve"))
                                        lnq.MISSED_CALL = Convert.ToInt64(dr("misscall"))
                                        lnq.CANCEL = Convert.ToInt64(dr("cancel"))
                                        lnq.WAIT_WITH_KPI = Convert.ToInt64(dr("wt_with_kpi"))
                                        lnq.SERVE_WITH_KPI = Convert.ToInt64(dr("ht_with_kpi"))
                                        lnq.NOT_CALL = Convert.ToInt64(dr("notcall"))
                                        lnq.NOT_CONFIRM = Convert.ToInt64(dr("notcon"))
                                        lnq.NOT_END = Convert.ToInt64(dr("notend"))
                                        lnq.MAX_WT = Convert.ToInt64(dr("max_wt"))
                                        lnq.MAX_HT = Convert.ToInt64(dr("max_ht"))

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                        End If

                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next
                                    dt.Dispose()
                                Else
                                    Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiDayCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.SERVICE_DATE = ServiceDate
                                    lnq.REGIS = 0
                                    lnq.SERVED = 0
                                    lnq.MISSED_CALL = 0
                                    lnq.CANCEL = 0
                                    lnq.WAIT_WITH_KPI = 0
                                    lnq.SERVE_WITH_KPI = 0
                                    lnq.NOT_CALL = 0
                                    lnq.NOT_CONFIRM = 0
                                    lnq.NOT_END = 0
                                    lnq.MAX_WT = 0
                                    lnq.MAX_HT = 0

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                    End If
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                End If
                            Next
                            sDt.Dispose()
                        End If
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shLnq.SHOP_ABB & " : " & shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.AverageServiceTimeComparingWidthKPIByTime, ServiceDate)
            If ProcID <> 0 Then
                'For Each drS As DataRow In dtS.Rows
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_TIME where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        trans.CommitTransaction()
                        If dtI.Rows.Count > 0 Then
                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByTime")
                            If shTrans.Trans IsNot Nothing Then
                                Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                                shTrans.CommitTransaction()
                                If sDt.Rows.Count > 0 Then
                                    For Each drI As DataRow In dtI.Rows
                                        Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))
                                        Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
                                        Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)
                                        Dim CurrTime As DateTime = StartTime
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByTime")
                                        If shTrans.Trans IsNot Nothing Then
                                            Do
                                                If CurrTime < EndTime Then
                                                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                                    If TimeTo > EndTime Then
                                                        TimeTo = EndTime
                                                    End If

                                                    For Each sDr As DataRow In sDt.Rows
                                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPIENG.ProcReportByTime")
                                                        Dim ServiceID As String = sDr("id")
                                                        Dim Service As String = sDr("item_name")
                                                        Dim dt As DataTable = GetDataTableTime(ServiceID, ServiceDate, CurrTime, TimeTo, shTrans)
                                                        shTrans.CommitTransaction()

                                                        If dt.Rows.Count > 0 Then
                                                            For Each dr As DataRow In dt.Rows
                                                                Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiTimeCenLinqDB
                                                                lnq.SERVICE_DATE = ServiceDate
                                                                lnq.SHOP_ID = shLnq.ID
                                                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                                lnq.INTERVAL_MINUTE = IntervalMinute
                                                                lnq.TIME_PRIOD_FROM = CurrTime
                                                                lnq.TIME_PRIOD_TO = TimeTo
                                                                lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")
                                                                lnq.SERVICE_NAME = Service
                                                                lnq.SERVICE_ID = ServiceID
                                                                lnq.REGIS = dr("regis")
                                                                lnq.SERVED = dr("serve")
                                                                lnq.MISSED_CALL = dr("misscall")
                                                                lnq.CANCEL = dr("cancel")
                                                                lnq.NOTCALL = dr("notcall")
                                                                lnq.NOTCON = dr("notcon")
                                                                lnq.NOTEND = dr("notend")
                                                                lnq.WAIT_WITH_KPI = dr("wt_with_kpi")
                                                                lnq.SERVE_WITH_KPI = dr("ht_with_kpi")
                                                                lnq.MAX_WT = dr("max_wt")
                                                                lnq.MAX_HT = dr("max_ht")
                                                                lnq.ACONT = dr("act")
                                                                lnq.SCONT = dr("sct")
                                                                lnq.CCONT = dr("cct")
                                                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                                Dim ret As Boolean = False
                                                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                                If ret = True Then
                                                                    trans.CommitTransaction()
                                                                Else
                                                                    trans.RollbackTransaction()
                                                                    FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                                                End If
                                                                Try
                                                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                                    Application.DoEvents()
                                                                Catch ex As Exception

                                                                End Try
                                                                
                                                            Next
                                                            dt.Dispose()
                                                            dt = Nothing
                                                        Else
                                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiTimeCenLinqDB
                                                            lnq.SERVICE_DATE = ServiceDate
                                                            lnq.SHOP_ID = shLnq.ID
                                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                            lnq.INTERVAL_MINUTE = IntervalMinute
                                                            lnq.TIME_PRIOD_FROM = CurrTime
                                                            lnq.TIME_PRIOD_TO = TimeTo
                                                            lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")
                                                            lnq.SERVICE_NAME = Service
                                                            lnq.SERVICE_ID = ServiceID
                                                            lnq.REGIS = 0
                                                            lnq.SERVED = 0
                                                            lnq.MISSED_CALL = 0
                                                            lnq.CANCEL = 0
                                                            lnq.NOTCALL = 0
                                                            lnq.NOTCON = 0
                                                            lnq.NOTEND = 0
                                                            lnq.WAIT_WITH_KPI = 0
                                                            lnq.SERVE_WITH_KPI = 0
                                                            lnq.MAX_WT = "0"
                                                            lnq.MAX_HT = "0"
                                                            lnq.ACONT = 0
                                                            lnq.SCONT = 0
                                                            lnq.CCONT = 0

                                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                            Dim ret As Boolean = False
                                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                            If ret = True Then
                                                                trans.CommitTransaction()
                                                            Else
                                                                trans.RollbackTransaction()
                                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                                            End If
                                                        End If
                                                        Try
                                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                            Application.DoEvents()
                                                        Catch ex As Exception

                                                        End Try
                                                    Next
                                                End If

                                                CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                            Loop While CurrTime <= EndTime
                                            shTrans.CommitTransaction()
                                        Else
                                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                                        End If
                                    Next
                                Else
                                    UpdateProcessError(ProcID, sLnq.ErrorMessage)
                                End If
                                sDt.Dispose()
                            Else
                                UpdateProcessError(ProcID, shTrans.ErrorMessage)
                            End If
                            dtI = Nothing
                        Else
                            UpdateProcessError(ProcID, lnqI.ErrorMessage)
                        End If
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                'Next
                UpdateProcessTime(ProcID)
            End If
            'End If
        End Sub

        'Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_WEEK where show_week = '" & DatePart(DateInterval.WeekOfYear, ServiceDate) & "' and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("AverageServiceTimeComparingWidthKPIByWeek", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim dt As DataTable = GetDataTableWeek(ServiceID, ServiceDate, shTrans)
        '                                    shTrans.CommitTransaction()

        '                                    If dt.Rows.Count > 0 Then
        '                                        For Each dr As DataRow In dt.Rows
        '                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiWeekCenLinqDB
        '                                            lnq.SHOP_ID = shLnq.ID
        '                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                                            lnq.SERVICE_ID = ServiceID
        '                                            lnq.SERVICE_NAME = sDr("item_name")
        '                                            lnq.SHOW_WEEK = DatePart(DateInterval.WeekOfYear, ServiceDate)
        '                                            lnq.SHOW_YEAR = ServiceDate.Year
        '                                            lnq.REGIS = dr("regis")
        '                                            lnq.SERVED = dr("serve")
        '                                            lnq.MISSED_CALL = dr("misscall")
        '                                            lnq.CANCEL = dr("cancel")
        '                                            lnq.NOTCALL = dr("not_call")
        '                                            lnq.NOTCON = dr("not_con")
        '                                            lnq.NOTEND = dr("not_end")
        '                                            lnq.WAIT_WITH_KPI = dr("wait_with_kpi")
        '                                            lnq.SERVE_WITH_KPI = dr("serve_with_kpi")
        '                                            lnq.PER_AWT_WITH_KPI = dr("wt_with_kpi_percen")
        '                                            lnq.PER_AWT_OVER_KPI = dr("wt_over_kpi_percen")
        '                                            lnq.PER_AHT_WITH_KPI = dr("ht_with_kpi_percen")
        '                                            lnq.PER_AHT_OVER_KPI = dr("ht_over_kpi_percen")
        '                                            lnq.PER_MISSED_CALL = dr("misscall_percen")
        '                                            'lnq.PER_CANCEL = dr("cancel_per")
        '                                            If dr("max_wt").ToString <> "" Then
        '                                                lnq.MAX_WT = dr("max_wt")
        '                                            Else
        '                                                lnq.MAX_WT = ""
        '                                            End If

        '                                            If dr("max_ht").ToString <> "" Then
        '                                                lnq.MAX_HT = dr("max_ht")
        '                                            Else
        '                                                lnq.MAX_HT = ""
        '                                            End If

        '                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                                            Dim ret As Boolean = False
        '                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                                            If ret = True Then
        '                                                trans.CommitTransaction()
        '                                            Else
        '                                                trans.RollbackTransaction()
        '                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate", lnq.ErrorMessage)
        '                                            End If
        '                                        Next
        '                                        dt.Dispose()
        '                                        dt = Nothing
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
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
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_YEAR where show_quarter=" & DatePart(DateInterval.Quarter, ServiceDate) & " and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("AverageServiceTimeComparingWidthKPIByYear", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim dt As DataTable = GetDataTableYear(ServiceID, ServiceDate, shTrans)
        '                                    shTrans.CommitTransaction()
        '                                    If dt.Rows.Count > 0 Then
        '                                        For Each dr As DataRow In dt.Rows
        '                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiYearCenLinqDB
        '                                            lnq.SHOP_ID = shLnq.ID
        '                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                                            lnq.SERVICE_ID = ServiceID
        '                                            lnq.SERVICE_NAME = sDr("item_name")
        '                                            lnq.SHOW_QUARTER = DatePart(DateInterval.Quarter, ServiceDate)
        '                                            lnq.SHOW_YEAR = ServiceDate.Year
        '                                            lnq.REGIS = dr("regis")
        '                                            lnq.SERVED = dr("serve")
        '                                            lnq.MISSED_CALL = dr("misscall")
        '                                            lnq.CANCEL = dr("cancel")
        '                                            lnq.NOTCALL = dr("not_call")
        '                                            lnq.NOTCON = dr("not_con")
        '                                            lnq.NOTEND = dr("not_end")
        '                                            lnq.WAIT_WITH_KPI = dr("wait_with_kpi")
        '                                            lnq.SERVE_WITH_KPI = dr("serve_with_kpi")
        '                                            lnq.PER_AWT_WITH_KPI = dr("wt_with_kpi_percen")
        '                                            lnq.PER_AWT_OVER_KPI = dr("wt_over_kpi_percen")
        '                                            lnq.PER_AHT_WITH_KPI = dr("ht_with_kpi_percen")
        '                                            lnq.PER_AHT_OVER_KPI = dr("ht_over_kpi_percen")
        '                                            lnq.PER_MISSED_CALL = dr("misscall_percen")
        '                                            'lnq.PER_CANCEL = dr("cancel_per")
        '                                            If dr("max_wt").ToString <> "" Then
        '                                                lnq.MAX_WT = dr("max_wt")
        '                                            Else
        '                                                lnq.MAX_WT = ""
        '                                            End If

        '                                            If dr("max_ht").ToString <> "" Then
        '                                                lnq.MAX_HT = dr("max_ht")
        '                                            Else
        '                                                lnq.MAX_HT = ""
        '                                            End If

        '                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                                            Dim ret As Boolean = False
        '                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                                            If ret = True Then
        '                                                trans.CommitTransaction()
        '                                            Else
        '                                                trans.RollbackTransaction()
        '                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate", lnq.ErrorMessage)
        '                                            End If
        '                                        Next
        '                                        dt.Dispose()
        '                                        dt = Nothing
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
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

        'Public Sub ProcReportByDate(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_DAY where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("AverageServiceTimeComparingWidthKPIByDate", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim dt As DataTable = GetDataTableDate(ServiceID, ServiceDate, shTrans)
        '                                    shTrans.CommitTransaction()

        '                                    If dt.Rows.Count > 0 Then
        '                                        For Each dr As DataRow In dt.Rows
        '                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiDayCenLinqDB
        '                                            lnq.SHOP_ID = shLnq.ID
        '                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                                            lnq.SERVICE_ID = ServiceID
        '                                            lnq.SERVICE_NAME = sDr("item_name")
        '                                            lnq.SERVICE_DATE = ServiceDate
        '                                            lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        '                                            lnq.SHOW_DAY = ServiceDate.ToString("dddd", New Globalization.CultureInfo("en-US"))
        '                                            lnq.SHOW_WEEK = DatePart(DateInterval.WeekOfYear, ServiceDate)
        '                                            lnq.SHOW_YEAR = ServiceDate.Year
        '                                            lnq.REGIS = dr("regis")
        '                                            lnq.SERVED = dr("serve")
        '                                            lnq.MISSED_CALL = dr("misscall")
        '                                            lnq.CANCEL = dr("cancel")
        '                                            lnq.NOTCALL = dr("not_call")
        '                                            lnq.NOTCON = dr("not_con")
        '                                            lnq.NOTEND = dr("not_end")
        '                                            lnq.WAIT_WITH_KPI = dr("wait_with_kpi")
        '                                            lnq.SERVE_WITH_KPI = dr("serve_with_kpi")
        '                                            lnq.PER_AWT_WITH_KPI = dr("wt_with_kpi_percen")
        '                                            lnq.PER_AWT_OVER_KPI = dr("wt_over_kpi_percen")
        '                                            lnq.PER_AHT_WITH_KPI = dr("ht_with_kpi_percen")
        '                                            lnq.PER_AHT_OVER_KPI = dr("ht_over_kpi_percen")
        '                                            lnq.PER_MISSED_CALL = dr("misscall_percen")
        '                                            'lnq.PER_CANCEL = dr("cancel_per")
        '                                            If dr("max_wt").ToString <> "" Then
        '                                                lnq.MAX_WT = dr("max_wt")
        '                                            Else
        '                                                lnq.MAX_WT = ""
        '                                            End If

        '                                            If dr("max_ht").ToString <> "" Then
        '                                                lnq.MAX_HT = dr("max_ht")
        '                                            Else
        '                                                lnq.MAX_HT = ""
        '                                            End If

        '                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                                            Dim ret As Boolean = False
        '                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                                            If ret = True Then
        '                                                trans.CommitTransaction()
        '                                            Else
        '                                                trans.RollbackTransaction()
        '                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIENG.ProcReportByDate", lnq.ErrorMessage)
        '                                            End If
        '                                        Next
        '                                        dt.Dispose()
        '                                        dt = Nothing
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
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

        Private Function GetDataTableTime(ByVal ServiceID As Integer, ByVal ServiceDate As DateTime, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable

            Dim StrTimeFrom As String = TimeFrom.ToString("HH:mm")
            Dim StrTimeTo As String = DateAdd(DateInterval.Minute, -1, TimeTo).ToString("HH:mm")
            Dim StrDate As String = ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = GetQuery(ServiceID)
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function

        Private Function GetDataTableMonth(ByVal ServiceID As Integer, ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery(ServiceID)
            sql += " and MM = " & ServiceDate.Month & vbNewLine
            sql += " and YY = " & ServiceDate.Year & " " & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function

        Private Function GetDataTableWeek(ByVal ServiceID As Integer, ByVal FirstDay As Date, ByVal LastDay As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery(ServiceID)
            If FirstDay.Month <> LastDay.Month Then
                sql += " and right('0'+LTRIM(str(MM)),2) + right('0'+LTRIM(str(DD)),2) between '" & FirstDay.ToString("MMdd", New Globalization.CultureInfo("en-US")) & "' and '" & LastDay.ToString("MMdd", New Globalization.CultureInfo("en-US")) & "'"
            Else
                sql += " and DD between " & FirstDay.Day & " and " & LastDay.Day
                sql += " and MM = " & FirstDay.Month & vbNewLine
            End If

            sql += " and YY = " & FirstDay.Year & " " & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function
        

        Private Function GetDataTableDate(ByVal ServiceID As Integer, ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery(ServiceID)
            sql += " and DD = " & ServiceDate.Day & vbNewLine
            sql += " and MM = " & ServiceDate.Month & vbNewLine
            sql += " and YY = " & ServiceDate.Year & " " & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function


        Private Function GetQuery(ByVal ServiceID As Long) As String
            Dim sql As String = ""
            sql = "select isnull(sum(regis),0) as regis,isnull(sum(serve),0) as serve,isnull(sum(misscall),0) as misscall,isnull(sum(cancel),0) as cancel,isnull(sum(notcall),0) as notcall,isnull(sum(notcon),0) as notcon,isnull(sum(notend),0) as notend,isnull(sum(wt_with_kpi),0) as wt_with_kpi,isnull(sum(ht_with_kpi),0) as ht_with_kpi,isnull(max(wt),0) as max_wt,isnull(max(ht),0) as max_ht,isnull(avg(ct),0) as act,isnull(sum(ct),0) as sct,isnull(sum(cct),0) as cct from (select staff,YY,MM,DD,[time],service_date,item_id,regis,serve,misscall,cancel,notcall,notcon,notend ,case when serve = 1 then wt_with_kpi else 0 end as wt_with_kpi,case when serve = 1 then ht_with_kpi else 0 end as ht_with_kpi,case when serve = 1 then wt else 0 end as wt,case when serve = 1 then ht else 0 end as ht,case when serve = 1 then ct else 0 end as ct,case when serve = 1 then cct else 0 end as cct from vw_report) as TB where item_id = " & ServiceID
            Return sql
        End Function
    End Class
End Namespace

