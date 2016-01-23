Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportsWaitingTimeHandlingTimeByShopENG : Inherits ReportsENG

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

        Public Sub ShopWaitingTimeHandlingTimeProcessAllReport()
            ProcReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.WaitingTimeHandlingTimeByShopByTime, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SHOP_TIME where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        trans.CommitTransaction()
                        If dtI.Rows.Count > 0 Then

                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByTime")
                            If shTrans.Trans IsNot Nothing Then
                                Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                                shTrans.CommitTransaction()
                                If sDt.Rows.Count > 0 Then
                                    For Each drI As DataRow In dtI.Rows
                                        Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))
                                        Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
                                        Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)
                                        Dim CurrTime As DateTime = StartTime
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByTime")
                                        If shTrans.Trans IsNot Nothing Then
                                            Do
                                                If CurrTime < EndTime Then
                                                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                                    If TimeTo > EndTime Then
                                                        TimeTo = EndTime
                                                    End If
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopTimeCenLinqDB
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")

                                                    For Each sDr As DataRow In sDt.Rows
                                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByTime")
                                                        Dim ServiceID As String = sDr("id")
                                                        Dim AWT As Int32 = 0
                                                        Dim AHT As Int32 = 0
                                                        Dim ACT As Int32 = 0
                                                        Dim TOTAL_REGIS As Int32 = 0
                                                        Dim TOTAL_SERVED As Int32 = 0
                                                        Dim TOTAL_MISSCALL As Int32 = 0
                                                        Dim TOTAL_CANCEL As Int32 = 0
                                                        Dim TOTAL_NOTCALL As Int32 = 0
                                                        Dim TOTAL_NOTCON As Int32 = 0
                                                        Dim TOTAL_NOTEND As Int32 = 0
                                                        Dim SWT As Int32 = 0
                                                        Dim SHT As Int32 = 0
                                                        Dim SCT As Int32 = 0
                                                        Dim CWT As Int32 = 0
                                                        Dim CHT As Int32 = 0
                                                        Dim CCT As Int32 = 0

                                                        Dim dt_Data As New DataTable
                                                        dt_Data = GetServiceTime(ServiceDate, lnq.TIME_PRIOD_FROM, lnq.TIME_PRIOD_TO, ServiceID, shTrans)
                                                        If dt_Data.Rows.Count > 0 Then
                                                            TOTAL_REGIS = dt_Data.Rows(0).Item("regis").ToString
                                                            TOTAL_SERVED = dt_Data.Rows(0).Item("serve").ToString
                                                            TOTAL_MISSCALL = dt_Data.Rows(0).Item("misscall").ToString
                                                            TOTAL_CANCEL = dt_Data.Rows(0).Item("cancel").ToString
                                                            TOTAL_NOTCALL = dt_Data.Rows(0).Item("notcall").ToString
                                                            TOTAL_NOTCON = dt_Data.Rows(0).Item("notcon").ToString
                                                            TOTAL_NOTEND = dt_Data.Rows(0).Item("notend").ToString
                                                            AWT = dt_Data.Rows(0).Item("awt").ToString
                                                            AHT = dt_Data.Rows(0).Item("aht").ToString
                                                            ACT = dt_Data.Rows(0).Item("act").ToString
                                                            SWT = dt_Data.Rows(0).Item("swt").ToString
                                                            SHT = dt_Data.Rows(0).Item("sht").ToString
                                                            SCT = dt_Data.Rows(0).Item("sct").ToString
                                                            CWT = dt_Data.Rows(0).Item("cwt").ToString
                                                            CHT = dt_Data.Rows(0).Item("cht").ToString
                                                            CCT = dt_Data.Rows(0).Item("cct").ToString
                                                        End If
                                                        dt_Data = Nothing

                                                        shTrans.CommitTransaction()
                                                        If lnq.DATA_COLUMN = "" Then
                                                            lnq.DATA_COLUMN = "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
                                                            lnq.DATA_VALUE = ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND & "|" & SWT & "|" & SCT & "|" & SHT & "|" & CWT & "|" & CCT & "|" & CHT
                                                        Else
                                                            lnq.DATA_COLUMN += "###" & "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
                                                            lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND & "|" & SWT & "|" & SCT & "|" & SHT & "|" & CWT & "|" & CCT & "|" & CHT
                                                        End If
                                                        Try
                                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                            Application.DoEvents()
                                                        Catch ex As Exception

                                                        End Try
                                                    Next

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByShopENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByShopENG")
                                                    End If
                                                End If
                                                CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                            Loop While CurrTime <= EndTime
                                            shTrans.CommitTransaction()
                                        Else
                                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                                        End If
                                    Next
                                    sDt.Dispose()
                                Else
                                    UpdateProcessError(ProcID, sLnq.ErrorMessage)
                                End If
                            Else
                                UpdateProcessError(ProcID, shTrans.ErrorMessage)
                            End If
                            dtI.Dispose()
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
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            'เริ่มที่ Shop ก่อน
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeByShopByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SHOP_DAY where shop_id = '" & ShopID & "' and convert(varchar(8),service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' ", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If sDt.Rows.Count > 0 Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByDate")
                            Dim dt As New DataTable
                            dt = GetServiceDate(ServiceDate, shTrans)
                            If dt.Rows.Count = 0 Then
                                Exit Sub
                            End If

                            shTrans.CommitTransaction()
                            For Each sDr As DataRow In sDt.Rows
                                Dim ServiceID As String = sDr("id")

                                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopDayCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.SERVICE_DATE = ServiceDate
                                lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = sDr("item_name")
                                lnq.SERVICE_NAME_TH = sDr("item_name_th")

                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.REGIS = Convert.ToInt64(dt.DefaultView(0)("regis"))
                                    lnq.SERVE = Convert.ToInt64(dt.DefaultView(0)("serve"))
                                    lnq.MISSED_CALL = Convert.ToInt64(dt.DefaultView(0)("misscall"))
                                    lnq.CANCLE = Convert.ToInt64(dt.DefaultView(0)("cancel"))
                                    lnq.INCOMPLETE = Convert.ToInt64(dt.DefaultView(0)("notcall")) + Convert.ToInt64(dt.DefaultView(0)("notcon")) + Convert.ToInt64(dt.DefaultView(0)("notend"))

                                    If dt.DefaultView(0)("CWT") > 0 Then lnq.AWT = dt.DefaultView(0)("SWT") / dt.DefaultView(0)("CWT")
                                    If dt.DefaultView(0)("CHT") > 0 Then lnq.AHT = dt.DefaultView(0)("SHT") / dt.DefaultView(0)("CHT")
                                    If dt.DefaultView(0)("CCT") > 0 Then lnq.ACT = dt.DefaultView(0)("SCT") / dt.DefaultView(0)("CCT")
                                    lnq.COUNT_WT = dt.DefaultView(0)("CWT")
                                    lnq.COUNT_HT = dt.DefaultView(0)("CHT")
                                    lnq.COUNT_CT = dt.DefaultView(0)("CCT")
                                    lnq.SUM_WT = dt.DefaultView(0)("SWT")
                                    lnq.SUM_HT = dt.DefaultView(0)("SHT")
                                    lnq.SUM_CT = dt.DefaultView(0)("SCT")
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByShopENG.ProcReportByDate", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByShopENG")
                                End If
                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                                lnq = Nothing
                            Next
                            dt.Dispose()
                            sDt.Dispose()
                        End If
                    Else
                        UpdateProcessError(ProcID, shLnq.SHOP_ABB & " " & shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            'เริ่มที่ Shop ก่อน
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeByShopByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SHOP_WEEK where shop_id = '" & ShopID & "' and week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' ", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If sDt.Rows.Count > 0 Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByWeek")
                            Dim dt As New DataTable
                            dt = GetServiceWeek(FirstDay, LastDay, shTrans)
                            If dt.Rows.Count = 0 Then
                                Exit Sub
                            End If

                            shTrans.CommitTransaction()
                            For Each sDr As DataRow In sDt.Rows
                                Dim ServiceID As String = sDr("id")

                                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopWeekCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.WEEK_OF_YEAR = WeekNo
                                lnq.PERIOD_DATE = FirstDay & " - " & LastDay
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = sDr("item_name")
                                lnq.SERVICE_NAME_TH = sDr("item_name_th")

                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.REGIS = Convert.ToInt64(dt.DefaultView(0)("regis"))
                                    lnq.SERVE = Convert.ToInt64(dt.DefaultView(0)("serve"))
                                    lnq.MISSED_CALL = Convert.ToInt64(dt.DefaultView(0)("misscall"))
                                    lnq.CANCLE = Convert.ToInt64(dt.DefaultView(0)("cancel"))
                                    lnq.INCOMPLETE = Convert.ToInt64(dt.DefaultView(0)("notcall")) + Convert.ToInt64(dt.DefaultView(0)("notcon")) + Convert.ToInt64(dt.DefaultView(0)("notend"))

                                    If dt.DefaultView(0)("CWT") > 0 Then lnq.AWT = dt.DefaultView(0)("SWT") / dt.DefaultView(0)("CWT")
                                    If dt.DefaultView(0)("CHT") > 0 Then lnq.AHT = dt.DefaultView(0)("SHT") / dt.DefaultView(0)("CHT")
                                    If dt.DefaultView(0)("CCT") > 0 Then lnq.ACT = dt.DefaultView(0)("SCT") / dt.DefaultView(0)("CCT")
                                    lnq.COUNT_WT = dt.DefaultView(0)("CWT")
                                    lnq.COUNT_HT = dt.DefaultView(0)("CHT")
                                    lnq.COUNT_CT = dt.DefaultView(0)("CCT")
                                    lnq.SUM_WT = dt.DefaultView(0)("SWT")
                                    lnq.SUM_HT = dt.DefaultView(0)("SHT")
                                    lnq.SUM_CT = dt.DefaultView(0)("SCT")
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByShopENG.ProcReportByWeek", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByShopENG")
                                End If
                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                                lnq = Nothing
                            Next
                            dt.Dispose()
                            sDt.Dispose()
                        End If
                    Else
                        UpdateProcessError(ProcID, shLnq.SHOP_ABB & " " & shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            'เริ่มที่ Shop ก่อน
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeByShopByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SHOP_MONTH where shop_id = '" & ShopID & "' and month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        shTrans.CommitTransaction()
                        If sDt.Rows.Count > 0 Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByShopENG.ProcReportByMonth")
                            Dim dt As New DataTable
                            dt = GetServiceMonth(ServiceDate, shTrans)
                            shTrans.CommitTransaction()
                            If dt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    Dim ServiceID As String = sDr("id")

                                    Dim lnq As New CenLinqDB.TABLE.TbRepWtHtShopMonthCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.MONTH_NO = ServiceDate.Month
                                    lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                    lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")

                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.REGIS = Convert.ToInt64(dt.DefaultView(0)("regis"))
                                        lnq.SERVE = Convert.ToInt64(dt.DefaultView(0)("serve"))
                                        lnq.MISSED_CALL = Convert.ToInt64(dt.DefaultView(0)("misscall"))
                                        lnq.CANCLE = Convert.ToInt64(dt.DefaultView(0)("cancel"))
                                        lnq.INCOMPLETE = Convert.ToInt64(dt.DefaultView(0)("notcall")) + Convert.ToInt64(dt.DefaultView(0)("notcon")) + Convert.ToInt64(dt.DefaultView(0)("notend"))

                                        If dt.DefaultView(0)("CWT") > 0 Then lnq.AWT = dt.DefaultView(0)("SWT") / dt.DefaultView(0)("CWT")
                                        If dt.DefaultView(0)("CHT") > 0 Then lnq.AHT = dt.DefaultView(0)("SHT") / dt.DefaultView(0)("CHT")
                                        If dt.DefaultView(0)("CCT") > 0 Then lnq.ACT = dt.DefaultView(0)("SCT") / dt.DefaultView(0)("CCT")
                                        lnq.COUNT_WT = dt.DefaultView(0)("CWT")
                                        lnq.COUNT_HT = dt.DefaultView(0)("CHT")
                                        lnq.COUNT_CT = dt.DefaultView(0)("CCT")
                                        lnq.SUM_WT = dt.DefaultView(0)("SWT")
                                        lnq.SUM_HT = dt.DefaultView(0)("SHT")
                                        lnq.SUM_CT = dt.DefaultView(0)("SCT")
                                    End If

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByShopENG.ProcReportByMonth", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByShopENG")
                                    End If
                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                    lnq = Nothing
                                Next
                            End If
                            dt.Dispose()
                            sDt.Dispose()
                        End If
                    Else
                        UpdateProcessError(ProcID, shLnq.SHOP_ABB & " " & shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetServiceMonth(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
            Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date, 112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by item_id"
            Dim dt As New DataTable
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetServiceWeek(ByVal FirstDay As String, ByVal LastDay As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date, 112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by item_id"
            Dim dt As New DataTable
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetServiceDate(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim DateSer As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date, 112) between '" & DateSer & "' and '" & DateSer & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by item_id"
            Dim dt As New DataTable
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        Private Function GetServiceTime(ByVal ServiceDate As DateTime, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal ServiceID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable

            Dim StrTimeFrom As String = TimeFrom.ToString("HH:mm")
            Dim StrTimeTo As String = DateAdd(DateInterval.Minute, -1, TimeTo).ToString("HH:mm")
            Dim StrDate As String = ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = GetQuery(ServiceID)
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by item_id"
            Dim dt As New DataTable
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt Is Nothing Then
                dt = New DataTable
            End If

            Return dt
        End Function

        'Private Function GetServiceWeek(ByVal ServiceDate As DateTime, ByVal ServiceID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim ShowWeek As Int16 = DatePart(DateInterval.WeekOfYear, ServiceDate)
        '    Dim ShowYear As Int16 = ServiceDate.Year
        '    Dim sql As String = GetQuery(ServiceID, TimeType)
        '    sql += " and datepart(week, cq.service_date) = " & ShowWeek
        '    sql += " and datepart(year, cq.service_date) = " & ShowYear

        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        If TimeType = "R" Or TimeType = "S" Or TimeType = "M" Or TimeType = "C" Or TimeType = "CA" Or TimeType = "CO" Or TimeType = "EN" Then
        '            ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '        Else
        '            Dim tSec As Long = 0
        '            If TimeType = "W" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumAWT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumAWT"))
        '                End If
        '            ElseIf TimeType = "H" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumAHT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumAHT"))
        '                End If
        '            ElseIf TimeType = "CT" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumACT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumACT"))
        '                End If
        '            End If
        '            'Dim cMin As Integer = Math.Floor(tSec / 60)
        '            'Dim cSec As Integer = tSec Mod 60

        '            ret = ReportsENG.GetFormatTimeFromSec(tSec) 'cMin.ToString & ":" & cSec.ToString.PadLeft(2, "0")
        '        End If
        '        dt.Dispose()
        '        dt = Nothing
        '    End If

        '    Return ret
        'End Function

        'Private Function GetServiceMonth(ByVal ServiceDate As DateTime, ByVal ServiceID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim sql As String = GetQuery(ServiceID, TimeType)
        '    sql += " and month(cq.service_date) = " & ServiceDate.Month & " and year(cq.service_date)= " & ServiceDate.Year

        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        If TimeType = "R" Or TimeType = "S" Or TimeType = "M" Or TimeType = "C" Or TimeType = "CA" Or TimeType = "CO" Or TimeType = "EN" Then
        '            ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '        Else
        '            Dim tSec As Long = 0
        '            If TimeType = "W" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumAWT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumAWT"))
        '                End If
        '            ElseIf TimeType = "H" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumAHT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumAHT"))
        '                End If
        '            ElseIf TimeType = "CT" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumACT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumACT"))
        '                End If
        '            End If
        '            'Dim cMin As Integer = Math.Floor(tSec / 60)
        '            'Dim cSec As Integer = tSec Mod 60

        '            ret = ReportsENG.GetFormatTimeFromSec(tSec) 'cMin.ToString & ":" & cSec.ToString.PadLeft(2, "0")
        '        End If
        '        dt.Dispose()
        '        dt = Nothing
        '    End If

        '    Return ret
        'End Function

        'Private Function GetServiceYear(ByVal ServiceDate As DateTime, ByVal ServiceID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim sql As String = GetQuery(ServiceID, TimeType)
        '    sql += " and year(cq.service_date) = " & ServiceDate.Year & " and DATEPART(QUARTER,cq.service_date) = " & DatePart(DateInterval.Quarter, ServiceDate)

        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        If TimeType = "R" Or TimeType = "S" Or TimeType = "M" Or TimeType = "C" Or TimeType = "CA" Or TimeType = "CO" Or TimeType = "EN" Then
        '            ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '        Else
        '            Dim tSec As Long = 0
        '            If TimeType = "W" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumAWT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumAWT"))
        '                End If
        '            ElseIf TimeType = "H" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumAHT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumAHT"))
        '                End If
        '            ElseIf TimeType = "CT" Then
        '                If Convert.IsDBNull(dt.Rows(0)("sumACT")) = False Then
        '                    tSec = Convert.ToInt64(dt.Rows(0)("sumACT"))
        '                End If
        '            End If
        '            'Dim cMin As Integer = Math.Floor(tSec / 60)
        '            'Dim cSec As Integer = tSec Mod 60

        '            ret = ReportsENG.GetFormatTimeFromSec(tSec) 'cMin.ToString & ":" & cSec.ToString.PadLeft(2, "0")
        '        End If
        '        dt.Dispose()
        '        dt = Nothing
        '    End If

        '    Return ret
        'End Function

        Private Function GetQuery(Optional ByVal ServiceID As Long = 0) As String
            Dim sql As String = ""
            sql += "select isnull(AVG(wt),0) as AWT"
            sql += " ,isnull(AVG(ct),0) as ACT"
            sql += " ,isnull(AVG(ht),0) as AHT  "
            sql += " ,isnull(SUM(wt),0) as SWT"
            sql += " ,isnull(SUM(ct),0) as SCT"
            sql += " ,isnull(SUM(ht),0) as SHT "
            sql += " ,isnull(SUM(cwt),0) as CWT"
            sql += " ,isnull(SUM(cct),0) as CCT"
            sql += " ,isnull(SUM(cht),0) as CHT "
            sql += " ,isnull(SUM(regis),0) as regis"
            sql += " ,isnull(SUM(serve),0) as serve"
            sql += " ,isnull(SUM(misscall),0) as misscall "
            sql += " ,isnull(SUM(cancel),0) as cancel"
            sql += " ,isnull(SUM(notcall),0) as notcall"
            sql += " ,isnull(SUM(notcon),0) as notcon "
            sql += " ,isnull(SUM(notend),0) as notend, item_id"
            sql += " from VW_Report"
            sql += " where 1=1"
            If ServiceID > 0 Then
                sql += " and item_id =" & ServiceID
            End If

            Return sql
        End Function
    End Class
End Namespace

