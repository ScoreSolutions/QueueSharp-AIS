Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities.Constant

Namespace Reports
    Public Class ReportsAppointmentByShopENG : Inherits ReportsENG
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

        Public Sub ShopAppointmentProcessAllReport()
            ProcessReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcessReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcessReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcessReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcessReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AppointmentReportByShopByTime", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_APPOINTMENT_SHOP_TIME where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportAppointmentByShopENG.ProcessReportBytime")
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
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportAppointmentByShopENG.ProcessReportBytime")
                                If shTrans.Trans IsNot Nothing Then
                                    Do
                                        If CurrTime < EndTime Then
                                            Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                            If TimeTo > EndTime Then
                                                TimeTo = EndTime
                                            End If

                                            'หาคิวที่ได้ทำรายการจอง
                                            Dim dt As New DataTable
                                            dt = GetAppointmentDataByTime(CurrTime, TimeTo, shTrans)
                                            If dt.Rows.Count > 0 Then
                                                Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopTimeCenLinqDB
                                                Try
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.SHOW_TIME = CurrTime.ToString("HH:mm") & "-" & TimeTo.ToString("HH:mm")
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Kiosk & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.TOTAL += dt.DefaultView(0)("total")
                                                        lnq.KIOSK_SHOW = dt.DefaultView(0)("show")
                                                        lnq.KIOSK_NOSHOW = dt.DefaultView(0)("noshow")
                                                    End If

                                                    dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.WebAppoint & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.TOTAL += dt.DefaultView(0)("total")
                                                        lnq.WEB_SHOW = dt.DefaultView(0)("show")
                                                        lnq.WEB_NOSHOW = dt.DefaultView(0)("noshow")
                                                    End If

                                                    dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Mobile & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.TOTAL += dt.DefaultView(0)("total")
                                                        lnq.MOBILE_SHOW = dt.DefaultView(0)("show")
                                                        lnq.MOBILE_NOSHOW = dt.DefaultView(0)("noshow")
                                                    End If

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcReportByTime", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                                                    End If
                                                Catch ex As Exception
                                                    FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcessReportTime", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                                                End Try
                                                lnq = Nothing

                                                Try
                                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                    Application.DoEvents()
                                                Catch ex As Exception

                                                End Try
                                            End If
                                            dt.Dispose()
                                        End If
                                        CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                    Loop While CurrTime <= EndTime
                                    shTrans.CommitTransaction()
                                Else
                                    UpdateProcessError(ProcID, shTrans.ErrorMessage)
                                End If
                            Next
                        End If
                        dtI.Dispose()
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub


        Public Sub ProcessReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AppointmentReportByShopByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_APPOINTMENT_SHOP_DATE where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportAppointmentByShopENG.ProcessReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        'หาคิวที่ได้ทำรายการจอง
                        Dim dt As New DataTable
                        dt = GetAppointmentDataByPeriodDate(ServiceDate, ServiceDate, shTrans)
                        If dt.Rows.Count > 0 Then

                            Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopDateCenLinqDB
                            Try
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.SERVICE_DATE = ServiceDate
                                lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Kiosk & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.KIOSK_SHOW = dt.DefaultView(0)("show")
                                    lnq.KIOSK_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.WebAppoint & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.WEB_SHOW = dt.DefaultView(0)("show")
                                    lnq.WEB_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Mobile & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.MOBILE_SHOW = dt.DefaultView(0)("show")
                                    lnq.MOBILE_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcReportByDate", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                                End If
                            Catch ex As Exception
                                FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcessReportDate", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                            End Try
                            lnq = Nothing

                            Try
                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                Application.DoEvents()
                            Catch ex As Exception

                            End Try
                        End If
                        dt.Dispose()
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcessReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AppointmentReportByShopByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_APPOINTMENT_SHOP_WEEK where week_of_year='" & WeekNo & "' and show_year='" & ServiceDate.Year & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
                Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportAppointmentByShopENG.ProcessReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        'หาคิวที่ได้ทำรายการจอง
                        Dim dt As New DataTable
                        dt = GetAppointmentDataByPeriodDate(FirstDay, LastDay, shTrans)
                        If dt.Rows.Count > 0 Then

                            Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopWeekCenLinqDB
                            Try
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.WEEK_OF_YEAR = WeekNo
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "-" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Kiosk & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.KIOSK_SHOW = dt.DefaultView(0)("show")
                                    lnq.KIOSK_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.WebAppoint & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.WEB_SHOW = dt.DefaultView(0)("show")
                                    lnq.WEB_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Mobile & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.MOBILE_SHOW = dt.DefaultView(0)("show")
                                    lnq.MOBILE_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcReportByWeek", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                                End If
                            Catch ex As Exception
                                FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcessReportWeek", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                            End Try
                            lnq = Nothing

                            Try
                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                Application.DoEvents()
                            Catch ex As Exception

                            End Try
                        End If
                        dt.Dispose()
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcessReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AppointmentReportByShopByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_APPOINTMENT_SHOP_MONTH where month_no='" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As New Date(ServiceDate.Year, ServiceDate.Month, 1)
                Dim LastDay As New Date(ServiceDate.Year, ServiceDate.Month, DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportAppointmentByShopENG.ProcessReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        'หาคิวที่ได้ทำรายการจอง
                        Dim dt As New DataTable
                        dt = GetAppointmentDataByPeriodDate(FirstDay, LastDay, shTrans)
                        If dt.Rows.Count > 0 Then
                            Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopMonthCenLinqDB
                            Try
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.MONTH_NO = ServiceDate.Month
                                lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Kiosk & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.KIOSK_SHOW = dt.DefaultView(0)("show")
                                    lnq.KIOSK_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.WebAppoint & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.WEB_SHOW = dt.DefaultView(0)("show")
                                    lnq.WEB_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                dt.DefaultView.RowFilter = "appointment_channel='" & TbAppointmentCustomer.AppointmentChannel.Mobile & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOTAL += dt.DefaultView(0)("total")
                                    lnq.MOBILE_SHOW = dt.DefaultView(0)("show")
                                    lnq.MOBILE_NOSHOW = dt.DefaultView(0)("noshow")
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcReportByMonth", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                                End If
                            Catch ex As Exception
                                FunctionEng.SaveErrorLog("ReportsAppointmentByShopENG.ProcessReportMonth", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentByShopENG")
                            End Try
                            lnq = Nothing

                            Try
                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                Application.DoEvents()
                            Catch ex As Exception

                            End Try
                        End If
                        dt.Dispose()
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub


        Private Function GetAppointmentDataByTime(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
            Dim dt As New DataTable
            Dim sql As String = " select COUNT(ac.id) total,"
            sql += " sum(case ac.active_status "
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' then 1"
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.EndQueue & "' then 1"
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.Missed & "' then 1"
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.Cancel & "' then 1"
            sql += " 	else 0 end) show,"
            sql += " sum(case ac.active_status when '" & TbAppointmentCustomer.ActiveStatus.NoShow & "' then 1 else 0 end) noshow,"
            sql += " ac.appointment_channel"
            sql += " from tb_appointment_customer ac"
            sql += " where CONVERT(varchar(16),start_slot,120) >= '" & TimeFrom.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            sql += " and CONVERT(varchar(16),start_slot,120) < '" & TimeTo.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            sql += " group by ac.appointment_channel"
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function

        Private Function GetAppointmentDataByPeriodDate(ByVal ServiceDateFrom As DateTime, ByVal ServiceDateTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
            Dim dt As New DataTable
            Dim sql As String = " select COUNT(ac.id) total,"
            sql += " sum(case ac.active_status "
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' then 1"
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.EndQueue & "' then 1"
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.Missed & "' then 1"
            sql += " 	when '" & TbAppointmentCustomer.ActiveStatus.Cancel & "' then 1"
            sql += " 	else 0 end) show,"
            sql += " sum(case ac.active_status when '" & TbAppointmentCustomer.ActiveStatus.NoShow & "' then 1 else 0 end) noshow,"
            sql += " ac.appointment_channel"
            sql += " from tb_appointment_customer ac"
            sql += " where CONVERT(varchar(8),start_slot,112) between '" & ServiceDateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & ServiceDateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' "
            sql += " group by ac.appointment_channel"
            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
    End Class
End Namespace

