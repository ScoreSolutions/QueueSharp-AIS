Imports Engine.Common
Imports System.Windows.Forms

Namespace Reports
    Public Class ReportsWaitingTimeHandlingTimeBySkillENG : Inherits ReportsENG

        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeBySkillByTime", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_TIME where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeBySkillENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        trans.CommitTransaction()
                        If dtI.Rows.Count > 0 Then
                            Dim qDt As New DataTable
                            qDt = GetQDataByDate(ServiceDate, shTrans)

                            For Each drI As DataRow In dtI.Rows
                                Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))
                                If qDt.Rows.Count > 0 Then
                                    Dim stDt As New DataTable
                                    stDt = qDt.DefaultView.ToTable(True, "user_code", "staff")
                                    For Each stDr As DataRow In stDt.Rows
                                        qDt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "'"
                                        For Each qDr As DataRowView In qDt.DefaultView
                                            Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillTimeCenLinqDB
                                            lnq.SERVICE_DATE = ServiceDate
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SERVICE_DATE = qDr("service_date")
                                            lnq.SHOW_DATE = lnq.SERVICE_DATE.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                            lnq.USER_CODE = IIf(Convert.IsDBNull(stDr("user_code")) = False, stDr("user_code"), "-")
                                            lnq.STAFF_NAME = IIf(Convert.IsDBNull(stDr("staff")) = False, stDr("staff"), "-")

                                            Dim IsAppoint As Boolean = IIf(qDr("customertype_id") = 3, True, False)
                                            Dim SkillDesc() As String = Split(GetSkillDesc(qDr("item_id"), IsAppoint, shTrans), "###")
                                            If SkillDesc.Length = 2 Then
                                                lnq.SKILL_ID = Convert.ToInt64(SkillDesc(0))
                                                lnq.SKILL_NAME = SkillDesc(1)
                                            Else
                                                lnq.SKILL_ID = 0
                                                lnq.SKILL_NAME = "-"
                                            End If
                                            lnq.QUEUE_NO = qDr("queue_no")
                                            lnq.MOBILE_NO = IIf(Convert.IsDBNull(qDr("customer_id")) = False, qDr("customer_id"), "")
                                            lnq.INTERVAL_MINUTE = IntervalMinute
                                            lnq.SHOW_TIME = GetQueueShowTime(IntervalMinute, ServiceDate, qDr("service_date"), shTrans) 'lnq.TIME_PERIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PERIOD_TO.ToString("HH:mm")
                                            lnq.WT = ReportsENG.GetFormatTimeFromSec(qDr("wt"))
                                            lnq.HT = ReportsENG.GetFormatTimeFromSec(qDr("ht"))
                                            lnq.CONT = ReportsENG.GetFormatTimeFromSec(qDr("ct"))

                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySKILLENG.ProcReportByTime", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                            End If
                                            lnq = Nothing

                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Next
                                        qDt.DefaultView.RowFilter = ""
                                    Next
                                    stDt.Dispose()
                                End If
                            Next
                            qDt.Dispose()
                        End If
                        lnqI = Nothing
                        dtI.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeBySkillByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_DATE where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeBySkillENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        Dim skDt As New DataTable
                        Dim skLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                        skDt = skLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        If skDt.Rows.Count > 0 Then
                            For Each skDr As DataRow In skDt.Rows
                                Dim qDt As New DataTable
                                qDt = GetSumDataByPeriod(ServiceDate, ServiceDate, skDr("id"), skDr("appointment"), shTrans) ' GetSumDataByDate(ServiceDate, skDr("id"), skDr("appointment"), shTrans)
                                If qDt.Rows.Count > 0 Then
                                    Dim stDT As New DataTable
                                    stDT = qDt.DefaultView.ToTable(True, "user_code", "staff")
                                    For Each stDr As DataRow In stDT.Rows
                                        qDt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "'"
                                        For Each dr As DataRowView In qDt.DefaultView
                                            Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillDateCenLinqDB
                                            Try
                                                lnq.SHOP_ID = ShopID
                                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                lnq.SERVICE_DATE = ServiceDate
                                                lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                                lnq.USER_CODE = IIf(Convert.IsDBNull(stDr("user_code")) = False, stDr("user_code"), "-")
                                                lnq.STAFF_NAME = IIf(Convert.IsDBNull(stDr("staff")) = False, stDr("staff"), "-")
                                                lnq.SKILL_ID = skDr("id")
                                                lnq.SKILL_NAME = skDr("skill")
                                                lnq.NUM_OF_QUEUE = dr("num_of_queue")
                                                lnq.REGIS = dr("regis")
                                                lnq.SERVE = dr("serve")
                                                lnq.MISS_CALL = dr("misscall")
                                                lnq.CANCEL = dr("cancel")
                                                lnq.NOT_CALL = dr("notcall")
                                                lnq.NOT_CON = dr("notcon")
                                                lnq.NOT_END = dr("notend")
                                                lnq.SUM_WT = dr("SWT")
                                                lnq.COUNT_WT = dr("CWT")
                                                If dr("CWT") > 0 Then lnq.AWT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SWT") / dr("CWT"))) Else lnq.AWT = "00:00:00"
                                                lnq.SUM_HT = dr("SHT")
                                                lnq.COUNT_HT = dr("CHT")
                                                If dr("CHT") > 0 Then lnq.AHT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SHT") / dr("CHT"))) Else lnq.AHT = "00:00:00"
                                                lnq.SUM_CONT = dr("SCT")
                                                lnq.COUNT_CONT = dr("CCT")
                                                If dr("CCT") > 0 Then lnq.ACONT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SCT") / dr("CCT"))) Else lnq.ACONT = "00:00:00"

                                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                Dim ret As Boolean = False
                                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                If ret = True Then
                                                    trans.CommitTransaction()
                                                Else
                                                    trans.RollbackTransaction()
                                                    FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySkillENG.ProcReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                                End If
                                                lnq = Nothing
                                            Catch ex As Exception
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySkillENG.ProcReportByDate", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                            End Try
                                            lnq = Nothing

                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Next
                                    Next
                                    stDT.Dispose()
                                End If
                                qDt.Dispose()
                            Next
                        End If
                        shTrans.CommitTransaction()
                        skDt.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeBySkillByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_WEEK where week_of_year ='" & WeekNo & "' and show_year='" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As DateTime = GetFirstDayOfWeek(ServiceDate)
                Dim LastDay As DateTime = GetLastDayOfWeek(ServiceDate)

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeBySkillENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        Dim skDt As New DataTable
                        Dim skLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                        skDt = skLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        If skDt.Rows.Count > 0 Then
                            For Each skDr As DataRow In skDt.Rows
                                Dim qDt As New DataTable
                                qDt = GetSumDataByPeriod(FirstDay, LastDay, skDr("id"), skDr("appointment"), shTrans)
                                If qDt.Rows.Count > 0 Then
                                    Dim stDT As New DataTable
                                    stDT = qDt.DefaultView.ToTable(True, "user_code", "staff")
                                    For Each stDr As DataRow In stDT.Rows
                                        qDt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "'"
                                        For Each dr As DataRowView In qDt.DefaultView
                                            Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillWeekCenLinqDB
                                            Try
                                                lnq.SHOP_ID = ShopID
                                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                lnq.WEEK_OF_YEAR = WeekNo
                                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                                lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "-" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                                lnq.USER_CODE = IIf(Convert.IsDBNull(stDr("user_code")) = False, stDr("user_code"), "-")
                                                lnq.STAFF_NAME = IIf(Convert.IsDBNull(stDr("staff")) = False, stDr("staff"), "-")
                                                lnq.SKILL_ID = skDr("id")
                                                lnq.SKILL_NAME = skDr("skill")
                                                lnq.NUM_OF_QUEUE = dr("num_of_queue")
                                                lnq.REGIS = dr("regis")
                                                lnq.SERVE = dr("serve")
                                                lnq.MISS_CALL = dr("misscall")
                                                lnq.CANCEL = dr("cancel")
                                                lnq.NOT_CALL = dr("notcall")
                                                lnq.NOT_CON = dr("notcon")
                                                lnq.NOT_END = dr("notend")
                                                lnq.SUM_WT = dr("SWT")
                                                lnq.COUNT_WT = dr("CWT")
                                                If dr("CWT") > 0 Then lnq.AWT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SWT") / dr("CWT"))) Else lnq.AWT = "00:00:00"
                                                lnq.SUM_HT = dr("SHT")
                                                lnq.COUNT_HT = dr("CHT")
                                                If dr("CHT") > 0 Then lnq.AHT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SHT") / dr("CHT"))) Else lnq.AHT = "00:00:00"
                                                lnq.SUM_CONT = dr("SCT")
                                                lnq.COUNT_CONT = dr("CCT")
                                                If dr("CCT") > 0 Then lnq.ACONT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SCT") / dr("CCT"))) Else lnq.ACONT = "00:00:00"

                                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                Dim ret As Boolean = False
                                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                If ret = True Then
                                                    trans.CommitTransaction()
                                                Else
                                                    trans.RollbackTransaction()
                                                    FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySkillENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                                End If
                                                lnq = Nothing
                                            Catch ex As Exception
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySkillENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                            End Try
                                            lnq = Nothing

                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Next
                                    Next
                                    stDT.Dispose()
                                End If
                                qDt.Dispose()
                            Next
                        End If
                        shTrans.CommitTransaction()
                        skDt.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeHandlingTimeBySkillByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_MONTH where month_no ='" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As New Date(ServiceDate.Year, ServiceDate.Month, 1)
                Dim LastDay As New Date(ServiceDate.Year, ServiceDate.Month, DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeBySkillENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        Dim skDt As New DataTable
                        Dim skLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                        skDt = skLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                        If skDt.Rows.Count > 0 Then
                            For Each skDr As DataRow In skDt.Rows
                                Dim qDt As New DataTable
                                qDt = GetSumDataByPeriod(FirstDay, LastDay, skDr("id"), skDr("appointment"), shTrans)
                                If qDt.Rows.Count > 0 Then
                                    Dim stDT As New DataTable
                                    stDT = qDt.DefaultView.ToTable(True, "user_code", "staff")
                                    For Each stDr As DataRow In stDT.Rows
                                        qDt.DefaultView.RowFilter = "user_code='" & stDr("user_code") & "'"
                                        For Each dr As DataRowView In qDt.DefaultView
                                            Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillMonthCenLinqDB
                                            Try
                                                lnq.SHOP_ID = ShopID
                                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                lnq.MONTH_NO = ServiceDate.Month
                                                lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                                lnq.USER_CODE = IIf(Convert.IsDBNull(stDr("user_code")) = False, stDr("user_code"), "-")
                                                lnq.STAFF_NAME = IIf(Convert.IsDBNull(stDr("staff")) = False, stDr("staff"), "-")
                                                lnq.SKILL_ID = skDr("id")
                                                lnq.SKILL_NAME = skDr("skill")
                                                lnq.NUM_OF_QUEUE = dr("num_of_queue")
                                                lnq.REGIS = dr("regis")
                                                lnq.SERVE = dr("serve")
                                                lnq.MISS_CALL = dr("misscall")
                                                lnq.CANCEL = dr("cancel")
                                                lnq.NOT_CALL = dr("notcall")
                                                lnq.NOT_CON = dr("notcon")
                                                lnq.NOT_END = dr("notend")
                                                lnq.SUM_WT = dr("SWT")
                                                lnq.COUNT_WT = dr("CWT")
                                                If dr("CWT") > 0 Then lnq.AWT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SWT") / dr("CWT"))) Else lnq.AWT = "00:00:00"
                                                lnq.SUM_HT = dr("SHT")
                                                lnq.COUNT_HT = dr("CHT")
                                                If dr("CHT") > 0 Then lnq.AHT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SHT") / dr("CHT"))) Else lnq.AHT = "00:00:00"
                                                lnq.SUM_CONT = dr("SCT")
                                                lnq.COUNT_CONT = dr("CCT")
                                                If dr("CCT") > 0 Then lnq.ACONT = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("SCT") / dr("CCT"))) Else lnq.ACONT = "00:00:00"

                                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                Dim ret As Boolean = False
                                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                If ret = True Then
                                                    trans.CommitTransaction()
                                                Else
                                                    trans.RollbackTransaction()
                                                    FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySkillENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                                End If
                                                lnq = Nothing
                                            Catch ex As Exception
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySkillENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHandlingTimeBySkillENG")
                                            End Try
                                            lnq = Nothing

                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Next
                                    Next
                                    stDT.Dispose()
                                End If
                                qDt.Dispose()
                            Next
                        End If
                        shTrans.CommitTransaction()
                        skDt.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetQueueShowTime(ByVal IntervalMinute As Integer, ByVal ServiceDate As DateTime, ByVal QueueServiceDateTime As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""

            Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
            Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)
            Dim CurrTime As DateTime = StartTime

            Do
                If CurrTime < EndTime Then
                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                    If TimeTo > EndTime Then
                        TimeTo = EndTime
                    End If
                    If CurrTime.ToString("HH:mm:ss") <= QueueServiceDateTime.ToString("HH:mm:ss") And QueueServiceDateTime.ToString("HH:mm:ss") < TimeTo.ToString("HH:mm:ss") Then
                        ret = CurrTime.ToString("HH:mm") & "-" & TimeTo.ToString("HH:mm")
                        Exit Do
                    End If
                End If
                CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
            Loop While CurrTime <= EndTime

            Return ret
        End Function

        Private Function GetQDataByDate(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql = GetQueueQuery()
            sql += " where  YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & " and UPPER(isnull(staff,'')) not like '%ADMIN%' "

            Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = hLnq.GetListBySql(sql, shTrans.Trans)
            hLnq = Nothing

            Return dt
        End Function

        Private Function GetQueueQuery() As String
            Dim sql As String = ""
            sql += "select queue_no,customer_id,item_id,item_name,service_date,user_code,staff,wt,ct,ht,cwt,cct,cht,customertype_id,customertype_name from VW_Report"
            Return sql
        End Function

        Private Function GetSkillDesc(ByVal ItemID As Long, ByVal IsAppoint As Boolean, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""
            Dim dt As New DataTable
            Dim sql As String = "select k.id, k.skill "
            sql += " from TB_SKILL_ITEM s "
            sql += " inner join TB_ITEM t on s.item_id = t.id "
            sql += " inner join TB_SKILL k on k.id=s.skill_id"
            sql += " where t.id = " & ItemID
            If IsAppoint = True Then
                sql += " and k.appointment=1"
            Else
                sql += " and k.appointment=0"
            End If
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0)("id") & "###" & dt.Rows(0)("skill")
            End If
            Return ret
        End Function

        'Public Sub ProcReportByDate(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("WaitingTimeHandlingTimeBySKILLByDate", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSKILLDayCenLinqDB
        '                        lnq.SHOP_ID = shLnq.ID
        '                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                        lnq.SERVICE_DATE = ServiceDate
        '                        lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        '                        lnq.SHOW_DAY = ServiceDate.ToString("dddd", New Globalization.CultureInfo("en-US"))
        '                        lnq.SHOW_WEEK = DatePart(DateInterval.WeekOfYear, ServiceDate)
        '                        lnq.SHOW_YEAR = ServiceDate.Year

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1' and appointment = 0", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim AWT As String = GetServiceDate(ServiceDate, ServiceID, "W", shTrans)
        '                                    Dim AHT As String = GetServiceDate(ServiceDate, ServiceID, "H", shTrans)
        '                                    Dim ACT As String = GetServiceDate(ServiceDate, ServiceID, "CT", shTrans)
        '                                    Dim TOTAL_REGIS As String = GetServiceDate(ServiceDate, ServiceID, "R", shTrans)
        '                                    Dim TOTAL_SERVED As String = GetServiceDate(ServiceDate, ServiceID, "S", shTrans)
        '                                    Dim TOTAL_MISSCALL As String = GetServiceDate(ServiceDate, ServiceID, "M", shTrans)
        '                                    Dim TOTAL_CANCEL As String = GetServiceDate(ServiceDate, ServiceID, "C", shTrans)
        '                                    Dim TOTAL_NOTCALL As String = GetServiceDate(ServiceDate, ServiceID, "CA", shTrans)
        '                                    Dim TOTAL_NOTCON As String = GetServiceDate(ServiceDate, ServiceID, "CO", shTrans)
        '                                    Dim TOTAL_NOTEND As String = GetServiceDate(ServiceDate, ServiceID, "EN", shTrans)

        '                                    shTrans.CommitTransaction()
        '                                    If lnq.DATA_COLUMN = "" Then
        '                                        lnq.DATA_COLUMN = "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE = ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    Else
        '                                        lnq.DATA_COLUMN += "###" & "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
        '                        End If

        '                        trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                        Dim ret As Boolean = False
        '                        ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                        If ret = True Then
        '                            trans.CommitTransaction()
        '                        Else
        '                            trans.RollbackTransaction()
        '                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySKILLENG.ProcReportByDate", lnq.ErrorMessage)
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

        'Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_WEEK where show_week = '" & DatePart(DateInterval.WeekOfYear, ServiceDate) & "' and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("WaitingTimeHandlingTimeBySKILLByWeek", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSKILLWeekCenLinqDB
        '                        lnq.SHOP_ID = shLnq.ID
        '                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                        lnq.SHOW_YEAR = ServiceDate.Year
        '                        lnq.SHOW_WEEK = DatePart(DateInterval.WeekOfYear, ServiceDate)

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1' and appointment = 0", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim AWT As String = GetServiceWeek(ServiceDate, ServiceID, "W", shTrans)
        '                                    Dim AHT As String = GetServiceWeek(ServiceDate, ServiceID, "H", shTrans)
        '                                    Dim ACT As String = GetServiceWeek(ServiceDate, ServiceID, "CT", shTrans)
        '                                    Dim TOTAL_REGIS As String = GetServiceWeek(ServiceDate, ServiceID, "R", shTrans)
        '                                    Dim TOTAL_SERVED As String = GetServiceWeek(ServiceDate, ServiceID, "S", shTrans)
        '                                    Dim TOTAL_MISSCALL As String = GetServiceWeek(ServiceDate, ServiceID, "M", shTrans)
        '                                    Dim TOTAL_CANCEL As String = GetServiceWeek(ServiceDate, ServiceID, "C", shTrans)
        '                                    Dim TOTAL_NOTCALL As String = GetServiceWeek(ServiceDate, ServiceID, "CA", shTrans)
        '                                    Dim TOTAL_NOTCON As String = GetServiceWeek(ServiceDate, ServiceID, "CO", shTrans)
        '                                    Dim TOTAL_NOTEND As String = GetServiceWeek(ServiceDate, ServiceID, "EN", shTrans)

        '                                    shTrans.CommitTransaction()
        '                                    If lnq.DATA_COLUMN = "" Then
        '                                        lnq.DATA_COLUMN = "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE = ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    Else
        '                                        lnq.DATA_COLUMN += "###" & "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
        '                        End If

        '                        trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                        Dim ret As Boolean = False
        '                        ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                        If ret = True Then
        '                            trans.CommitTransaction()
        '                        Else
        '                            trans.RollbackTransaction()
        '                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySKILLENG.ProcReportByWeek", lnq.ErrorMessage)
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

        'Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime)
        '    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_MONTH where month_no = '" & ServiceDate.Month & "' and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("WaitingTimeHandlingTimeBySKILLByMonth", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSKILLMonthCenLinqDB
        '                        lnq.SHOP_ID = shLnq.ID
        '                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                        lnq.MONTH_NO = ServiceDate.Month
        '                        lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
        '                        lnq.SHOW_YEAR = ServiceDate.Year

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1' and appointment = 0", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim AWT As String = GetServiceMonth(ServiceDate, ServiceID, "W", shTrans)
        '                                    Dim AHT As String = GetServiceMonth(ServiceDate, ServiceID, "H", shTrans)
        '                                    Dim ACT As String = GetServiceMonth(ServiceDate, ServiceID, "CT", shTrans)
        '                                    Dim TOTAL_REGIS As String = GetServiceMonth(ServiceDate, ServiceID, "R", shTrans)
        '                                    Dim TOTAL_SERVED As String = GetServiceMonth(ServiceDate, ServiceID, "S", shTrans)
        '                                    Dim TOTAL_MISSCALL As String = GetServiceMonth(ServiceDate, ServiceID, "M", shTrans)
        '                                    Dim TOTAL_CANCEL As String = GetServiceMonth(ServiceDate, ServiceID, "C", shTrans)
        '                                    Dim TOTAL_NOTCALL As String = GetServiceMonth(ServiceDate, ServiceID, "CA", shTrans)
        '                                    Dim TOTAL_NOTCON As String = GetServiceMonth(ServiceDate, ServiceID, "CO", shTrans)
        '                                    Dim TOTAL_NOTEND As String = GetServiceMonth(ServiceDate, ServiceID, "EN", shTrans)

        '                                    shTrans.CommitTransaction()
        '                                    If lnq.DATA_COLUMN = "" Then
        '                                        lnq.DATA_COLUMN = "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE = ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    Else
        '                                        lnq.DATA_COLUMN += "###" & "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
        '                        End If

        '                        trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                        Dim ret As Boolean = False
        '                        ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                        If ret = True Then
        '                            trans.CommitTransaction()
        '                        Else
        '                            trans.RollbackTransaction()
        '                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySKILLENG.ProcReportByMonth", lnq.ErrorMessage)
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
        '    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_SKILL_YEAR where show_quarter=" & DatePart(DateInterval.Quarter, ServiceDate) & " and show_year = '" & ServiceDate.Year & "'", dTrans.Trans)
        '    dTrans.CommitTransaction()

        '    'เริ่มที่ Shop ก่อน
        '    Dim dtS As DataTable = Common.FunctionEng.GetActiveShop()
        '    If dtS.Rows.Count > 0 Then
        '        Dim ProcID As Long = SaveProcessLog("WaitingTimeHandlingTimeBySKILLByMonth", ServiceDate)
        '        If ProcID <> 0 Then
        '            For Each drS As DataRow In dtS.Rows
        '                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '                If trans.Trans IsNot Nothing Then
        '                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(drS("id"))
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSKILLYearCenLinqDB
        '                        lnq.SHOP_ID = shLnq.ID
        '                        lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                        lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                        lnq.SHOW_QUARTER = DatePart(DateInterval.Quarter, ServiceDate)
        '                        lnq.SHOW_YEAR = ServiceDate.Year

        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
        '                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                        If shTrans.Trans IsNot Nothing Then
        '                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1' and appointment = 0", "id", shTrans.Trans)
        '                            shTrans.CommitTransaction()
        '                            If sDt.Rows.Count > 0 Then
        '                                For Each sDr As DataRow In sDt.Rows
        '                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
        '                                    Dim ServiceID As String = sDr("id")
        '                                    Dim AWT As String = GetServiceYear(ServiceDate, ServiceID, "W", shTrans)
        '                                    Dim AHT As String = GetServiceYear(ServiceDate, ServiceID, "H", shTrans)
        '                                    Dim ACT As String = GetServiceYear(ServiceDate, ServiceID, "CT", shTrans)
        '                                    Dim TOTAL_REGIS As String = GetServiceYear(ServiceDate, ServiceID, "R", shTrans)
        '                                    Dim TOTAL_SERVED As String = GetServiceYear(ServiceDate, ServiceID, "S", shTrans)
        '                                    Dim TOTAL_MISSCALL As String = GetServiceYear(ServiceDate, ServiceID, "M", shTrans)
        '                                    Dim TOTAL_CANCEL As String = GetServiceYear(ServiceDate, ServiceID, "C", shTrans)
        '                                    Dim TOTAL_NOTCALL As String = GetServiceYear(ServiceDate, ServiceID, "CA", shTrans)
        '                                    Dim TOTAL_NOTCON As String = GetServiceYear(ServiceDate, ServiceID, "CO", shTrans)
        '                                    Dim TOTAL_NOTEND As String = GetServiceYear(ServiceDate, ServiceID, "EN", shTrans)

        '                                    shTrans.CommitTransaction()
        '                                    If lnq.DATA_COLUMN = "" Then
        '                                        lnq.DATA_COLUMN = "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE = ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    Else
        '                                        lnq.DATA_COLUMN += "###" & "AWT|ACT|AHT|TOTAL_REGIS|TOTAL_SERVED|TOTAL_MISSCALL|TOTAL_CANCEL|TOTAL_CALL|TOTAL_CON|TOTAL_END"
        '                                        lnq.DATA_VALUE += "###" & ServiceID & "|" & sDr("Skill") & "|" & sDr("Skill") & "|" & AWT & "|" & ACT & "|" & AHT & "|" & TOTAL_REGIS & "|" & TOTAL_SERVED & "|" & TOTAL_MISSCALL & "|" & TOTAL_CANCEL & "|" & TOTAL_NOTCALL & "|" & TOTAL_NOTCON & "|" & TOTAL_NOTEND
        '                                    End If
        '                                Next
        '                                sDt.Dispose()
        '                                sDt = Nothing
        '                            End If
        '                        End If

        '                        trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                        Dim ret As Boolean = False
        '                        ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                        If ret = True Then
        '                            trans.CommitTransaction()
        '                        Else
        '                            trans.RollbackTransaction()
        '                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeBySKILLENG.ProcReportByYear", lnq.ErrorMessage)
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

        'Private Function GetServiceDate(ByVal ServiceDate As DateTime, ByVal SKillID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim sql As String = GetQuery(SKillID, TimeType, shTrans)
        '    sql += " and convert(varchar(10), cq.service_date, 120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' "

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

        'Private Function GetServiceTime(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal SKillID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim StrTimeFrom As String = TimeFrom.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
        '    Dim StrTimeTo As String = TimeTo.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))

        '    Dim sql As String = GetQuery(SKillID, TimeType, shTrans)
        '    sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
        '    sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "
        '    sql += " and UPPER(staff) not like '%ADMIN%' "

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

        'Private Function GetServiceWeek(ByVal ServiceDate As DateTime, ByVal SKillID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
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
        '    Dim sql As String = GetQuery(SKillID, TimeType, shTrans)
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

        'Private Function GetServiceMonth(ByVal ServiceDate As DateTime, ByVal SKillID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim sql As String = GetQuery(SKillID, TimeType, shTrans)
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

        'Private Function GetServiceYear(ByVal ServiceDate As DateTime, ByVal SKillID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
        '    Dim ret As String = ""
        '    'TimeType 
        '    '' W= Avg Waiting Time
        '    '' H= Avg Handling Time
        '    '' R= Total Regis
        '    '' S= Total Served
        '    '' M= Total Misscall
        '    '' C= Total Cancel
        '    '' I= Total Incomplete

        '    Dim sql As String = GetQuery(SKillID, TimeType, shTrans)
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

        
        

        Private Function GetSumDataByPeriod(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal SKillID As Long, ByVal IsAppoint As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql = GetSumQuery(SKillID, IsAppoint, shTrans)
            sql += " and convert(varchar(8),service_date,112) between '" & FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by user_code, staff"
            Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = hLnq.GetListBySql(sql, shTrans.Trans)
            hLnq = Nothing

            Return dt
        End Function

        'Private Function GetSumDataByDate(ByVal ServiceDate As DateTime, ByVal SKillID As Long, ByVal IsAppoint As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        '    Dim sql As String = ""
        '    sql = GetSumQuery(SKillID, IsAppoint, shTrans)
        '    sql += " and  YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
        '    sql += " group by user_code, staff"
        '    Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
        '    Dim dt As DataTable = hLnq.GetListBySql(sql, shTrans.Trans)
        '    hLnq = Nothing

        '    Return dt
        'End Function

        Private Function GetSumQuery(ByVal SKillID As Long, ByVal IsAppoint As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim sqlSkill As String = ""
            Dim Service As String = "0"
            Dim dt As New DataTable
            sqlSkill = "select si.item_id "
            sqlSkill += " from TB_SKILL_ITEM si "
            sqlSkill += " inner join TB_ITEM t on si.item_id = t.id "
            sqlSkill += " inner join TB_SKILL s on s.id=si.skill_id"
            sqlSkill += " where s.active_status = 1 And s.id = " & SKillID
            sqlSkill += " and s.appointment='" & IsAppoint & "'"
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sqlSkill, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    If Service = "0" Then
                        Service = dt.Rows(i).Item("item_id").ToString
                    Else
                        Service = Service & "," & dt.Rows(i).Item("item_id").ToString
                    End If
                Next
            End If

            Dim sql As String = ""
            sql += "select count(queue_no) num_of_queue, isnull(AVG(wt),0) as AWT"
            sql += " ,isnull(AVG(ct),0) as ACT"
            sql += " ,isnull(AVG(ht),0) as AHT  "
            sql += " ,isnull(SUM(wt),0) as SWT"
            sql += " ,isnull(SUM(ct),0) as SCT"
            sql += " ,isnull(SUM(ht),0) as SHT "
            sql += " ,isnull(SUM(cwt),0) as CWT "
            sql += " ,isnull(SUM(cct),0) as CCT "
            sql += " ,isnull(SUM(cht),0) as CHT "
            sql += " ,isnull(SUM(regis),0) as regis"
            sql += " ,isnull(SUM(serve),0) as serve"
            sql += " ,isnull(SUM(misscall),0) as misscall "
            sql += " ,isnull(SUM(cancel),0) as cancel"
            sql += " ,isnull(SUM(notcall),0) as notcall"
            sql += " ,isnull(SUM(notcon),0) as notcon "
            sql += " ,isnull(SUM(notend),0) as notend, user_code, staff"
            sql += " from VW_Report"
            sql += " where 1=1"
            sql += " and item_id  in (" & Service & ")"
            If IsAppoint = "1" Then
                sql += " and customertype_id='3'"
            Else
                sql += " and customertype_id<>'3'"
            End If


            Return sql
        End Function
    End Class
End Namespace

