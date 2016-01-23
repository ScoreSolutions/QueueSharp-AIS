Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class RepAverageServiceTimeComparingWithKPI_byStaffENG : Inherits ReportsENG

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

        Public Sub AgentAVGCompareKPIProcessAllReport()
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.AverageServiceTimeComparingWidthKPIStaffByDate, ServiceDate)
            If ProcID <> 0 Then
                'For Each drS As DataRow In dtS.Rows
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByDate")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByDate")
                                    Dim ServiceID As String = sDr("id")
                                    Dim dt As DataTable = GetDataTableDate(ServiceID, ServiceDate, shTrans)
                                    shTrans.CommitTransaction()

                                    If dt.Rows.Count > 0 Then
                                        For Each dr As DataRow In dt.Rows
                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffDayCenLinqDB
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SERVICE_ID = ServiceID
                                            lnq.SERVICE_NAME = sDr("item_name")
                                            lnq.SERVICE_DATE = ServiceDate
                                            lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                            lnq.SHOW_DAY = ServiceDate.ToString("dddd", New Globalization.CultureInfo("en-US"))
                                            lnq.SHOW_WEEK = DatePart(DateInterval.WeekOfYear, ServiceDate)
                                            lnq.SHOW_YEAR = ServiceDate.Year
                                            lnq.USER_ID = 0
                                            lnq.USER_CODE = IIf(Convert.IsDBNull(dr("user_code")) = False, dr("user_code"), "-")
                                            lnq.USER_NAME = ""
                                            lnq.STAFF_NAME = IIf(Convert.IsDBNull(dr("staff")) = False, dr("staff"), "-")
                                            lnq.REGIS = dr("regis")
                                            lnq.SERVED = dr("serve")
                                            lnq.MISSED_CALL = dr("misscall")
                                            lnq.CANCEL = dr("cancel")
                                            lnq.NOTCALL = dr("notcall")
                                            lnq.NOTCON = dr("notcon")
                                            lnq.NOTEND = dr("notend")
                                            lnq.WAIT_WITH_KPI = dr("wt_with_kpi")
                                            lnq.SERVE_WITH_KPI = dr("ht_with_kpi")
                                            lnq.ACONT = dr("act")
                                            lnq.SCONT = dr("sct")
                                            lnq.CCONT = dr("cct")
                                            lnq.MAX_WT = dr("max_wt")
                                            lnq.MAX_HT = dr("max_ht")
                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIStaffENG.ProcReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                            End If
                                            Try
                                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                Application.DoEvents()
                                            Catch ex As Exception

                                            End Try
                                        Next
                                        dt.Dispose()
                                    End If
                                Next
                                sDt.Dispose()
                            End If
                        End If
                        sLnq = Nothing
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AverageServiceTimeComparingWidthKPIStaffByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_STAFF_WEEK where week_of_year='" & WeekNo & "' and show_year='" & ServiceDate.Year & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
                Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByWeek")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByWeek")
                                    Dim ServiceID As String = sDr("id")
                                    Dim dt As DataTable = GetDataTableWeek(ServiceID, FirstDay, LastDay, shTrans)
                                    shTrans.CommitTransaction()

                                    If dt.Rows.Count > 0 Then
                                        For Each dr As DataRow In dt.Rows
                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffWeekCenLinqDB
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SERVICE_ID = ServiceID
                                            lnq.SERVICE_NAME_EN = sDr("item_name")
                                            lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                            lnq.WEEK_OF_YEAR = WeekNo
                                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                            lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & " - " & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                            lnq.USER_ID = 0
                                            lnq.USER_CODE = IIf(Convert.IsDBNull(dr("user_code")) = False, dr("user_code"), "-")
                                            lnq.USER_NAME = ""
                                            lnq.STAFF_NAME = IIf(Convert.IsDBNull(dr("staff")) = False, dr("staff"), "-")
                                            lnq.REGIS = dr("regis")
                                            lnq.SERVED = dr("serve")
                                            lnq.MISSED_CALL = dr("misscall")
                                            lnq.CANCEL = dr("cancel")
                                            lnq.NOT_CALL = dr("notcall")
                                            lnq.NOT_CON = dr("notcon")
                                            lnq.NOT_END = dr("notend")
                                            lnq.WAIT_WITH_KPI = dr("wt_with_kpi")
                                            lnq.SERVE_WITH_KPI = dr("ht_with_kpi")
                                            lnq.ACONT = dr("act")
                                            lnq.SCONT = dr("sct")
                                            lnq.CCONT = dr("cct")
                                            lnq.MAX_WT = dr("max_wt")
                                            lnq.MAX_HT = dr("max_ht")
                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIStaffENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPIENG")
                                            End If
                                            Try
                                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                Application.DoEvents()
                                            Catch ex As Exception

                                            End Try
                                        Next
                                        dt.Dispose()
                                    End If
                                Next
                                sDt.Dispose()
                            End If
                        End If
                        sLnq = Nothing
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AverageServiceTimeComparingWidthKPIStaffByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_AVG_SERVICE_TIME_KPI_STAFF_MONTH where month_no='" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByMonth")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "RepAverageServiceTimeComparingWithKPI_byStaffENG.ProcReportByMonth")
                                    Dim ServiceID As String = sDr("id")
                                    Dim dt As DataTable = GetDataTableMonth(ServiceID, ServiceDate, shTrans)
                                    shTrans.CommitTransaction()

                                    If dt.Rows.Count > 0 Then
                                        For Each dr As DataRow In dt.Rows
                                            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffMonthCenLinqDB
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SERVICE_ID = ServiceID
                                            lnq.SERVICE_NAME_EN = sDr("item_name")
                                            lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                            lnq.MONTH_NO = ServiceDate.Month
                                            lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                            lnq.USER_ID = 0
                                            lnq.USER_CODE = IIf(Convert.IsDBNull(dr("user_code")) = False, dr("user_code"), "-")
                                            lnq.USER_NAME = ""
                                            lnq.STAFF_NAME = IIf(Convert.IsDBNull(dr("staff")) = False, dr("staff"), "-")
                                            lnq.REGIS = dr("regis")
                                            lnq.SERVED = dr("serve")
                                            lnq.MISSED_CALL = dr("misscall")
                                            lnq.CANCEL = dr("cancel")
                                            lnq.NOT_CALL = dr("notcall")
                                            lnq.NOT_CON = dr("notcon")
                                            lnq.NOT_END = dr("notend")
                                            lnq.WAIT_WITH_KPI = dr("wt_with_kpi")
                                            lnq.SERVE_WITH_KPI = dr("ht_with_kpi")
                                            lnq.ACONT = dr("act")
                                            lnq.SCONT = dr("sct")
                                            lnq.CCONT = dr("cct")
                                            lnq.MAX_WT = dr("max_wt")
                                            lnq.MAX_HT = dr("max_ht")
                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("RepAverageServiceTimeComparingWithKPIStaffENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "RepAverageServiceTimeComparingWighKPI_byStaffENG")
                                            End If
                                            Try
                                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                Application.DoEvents()
                                            Catch ex As Exception

                                            End Try
                                        Next
                                        dt.Dispose()
                                    End If
                                Next
                                sDt.Dispose()
                            End If
                        End If
                        sLnq = Nothing
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        'Private Function GetDataTableTime(ByVal ServiceID As Integer, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        '    Dim Condition As String = ""
        '    Condition = " convert(varchar(16), service_date,120) >= '" & TimeFrom.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
        '    Condition &= " and convert(varchar(16), service_date,120) < '" & TimeTo.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "'"
        '    Condition += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
        '    Dim sql As String = GetQuery(ServiceID)
        '    sql = sql.Replace("[SCORE]", Condition)
        '    Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        'End Function

        Private Function GetDataTableDate(ByVal ServiceID As Integer, ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery(ServiceID)
            sql += " and YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & " "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by user_code,staff"
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function

        Private Function GetDataTableWeek(ByVal ServiceID As Integer, ByVal FirstDay As Date, ByVal LastDay As Date, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            'Dim CalDate As Date = DateAdd(DateInterval.Day, -1, ServiceDate)
            'Dim FirstDay As Date = DateAdd(DateInterval.Day, 1 - CalDate.DayOfWeek, CalDate)
            'If FirstDay < New Date(ServiceDate.Year, 1, 1) Then
            '    FirstDay = New Date(ServiceDate.Year, 1, 1)
            'End If

            'Dim LastDay As Date = DateAdd(DateInterval.Day, 7 - CalDate.DayOfWeek, CalDate)
            'If LastDay > New Date(ServiceDate.Year, 12, 31) Then
            '    LastDay = New Date(ServiceDate.Year, 12, 31)
            'End If

            Dim sql As String = GetQuery(ServiceID)
            If FirstDay.Month <> LastDay.Month Then
                sql += " and right('0'+LTRIM(str(MM)),2) + right('0'+LTRIM(str(DD)),2) between '" & FirstDay.ToString("MMdd", New Globalization.CultureInfo("en-US")) & "' and '" & LastDay.ToString("MMdd", New Globalization.CultureInfo("en-US")) & "'"
            Else
                sql += " and DD between " & FirstDay.Day & " and " & LastDay.Day
                sql += " and MM = " & FirstDay.Month & vbNewLine
            End If

            sql += " and YY = " & FirstDay.Year & " " & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by user_code,staff"
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function


        Private Function GetDataTableMonth(ByVal ServiceID As Integer, ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery(ServiceID)
            sql += " and MM = " & ServiceDate.Month & vbNewLine
            sql += " and YY = " & ServiceDate.Year & " " & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by user_code,staff"
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function

        Private Function GetQuery(ByVal ServiceID As Long) As String
            Dim sql As String = ""
            sql = "select isnull(sum(regis),0) as regis,isnull(sum(serve),0) as serve,isnull(sum(misscall),0) as misscall,isnull(sum(cancel),0) as cancel,isnull(sum(notcall),0) as notcall,isnull(sum(notcon),0) as notcon,isnull(sum(notend),0) as notend,isnull(sum(wt_with_kpi),0) as wt_with_kpi,isnull(sum(ht_with_kpi),0) as ht_with_kpi,isnull(max(wt),0) as max_wt,isnull(max(ht),0) as max_ht,isnull(avg(ct),0) as act,isnull(sum(ct),0) as sct,isnull(sum(cct),0) as cct,user_code,staff from (select YY,MM,DD,[time],item_id,regis,serve,misscall,cancel,notcall,notcon,notend ,case when serve = 1 then wt_with_kpi else 0 end as wt_with_kpi,case when serve = 1 then ht_with_kpi else 0 end as ht_with_kpi,case when serve = 1 then wt else 0 end as wt,case when serve = 1 then ht else 0 end as ht,case when serve = 1 then ct else 0 end as ct,case when serve = 1 then cct else 0 end as cct,user_code,staff from vw_report) as TB where item_id = " & ServiceID
            Return sql
        End Function
        
    End Class
End Namespace

