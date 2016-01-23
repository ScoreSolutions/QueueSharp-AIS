Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities
Namespace Reports
    Public Class ReportsWaitingTimeHandlingTimeByAgentENG : Inherits ReportsENG

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

        Public Sub AgentWaitingTimeHandlingTimeProcessAllReport()
            ProcessReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcessReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcessReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcessReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcessReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.WaitingTimeAndHandlingTimeReportByAgentByTime, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_AGENT where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetQueueDataByTime(ServiceDate, shTrans)
                        If dt IsNot Nothing Then
                            If dt.Rows.Count = 0 Then
                                Exit Sub
                            End If
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtAgentCenLinqDB
                                Try
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.USER_ID = 0
                                    lnq.USER_CODE = IIf(IsDBNull(dt.Rows(i)("user_code")) = False, dt.Rows(i)("user_code"), "-")
                                    lnq.USERNAME = "-"
                                    lnq.STAFF_NAME = IIf(IsDBNull(dt.Rows(i)("staff")) = False, dt.Rows(i)("staff"), "-")
                                    lnq.SERVICE_DATE = Convert.ToDateTime(dt.Rows(i)("service_date"))
                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.SERVICE_ID = dt.Rows(i)("item_id")
                                    lnq.SERVICE_TYPE = dt.Rows(i)("item_name")
                                    lnq.QUEUE_NO = dt.Rows(i)("queue_no")
                                    lnq.MOBILE_NO = dt.Rows(i)("customer_id")
                                    If IsDBNull(dt.Rows(i)("wt")) = False Then lnq.AWT = Convert.ToInt64(dt.Rows(i)("wt"))
                                    If IsDBNull(dt.Rows(i)("ht")) = False Then lnq.AHT = Convert.ToInt64(dt.Rows(i)("ht"))
                                    If IsDBNull(dt.Rows(i)("ct")) = False Then lnq.ACT = Convert.ToInt64(dt.Rows(i)("ct"))
                                    If IsDBNull(dt.Rows(i)("cwt")) = False Then lnq.CWT = Convert.ToInt64(dt.Rows(i)("cwt"))
                                    If IsDBNull(dt.Rows(i)("cht")) = False Then lnq.CHT = Convert.ToInt64(dt.Rows(i)("cht"))
                                    If IsDBNull(dt.Rows(i)("cct")) = False Then lnq.CCT = Convert.ToInt64(dt.Rows(i)("cct"))

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                    End If
                                Catch ex As Exception
                                    FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportTime", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                End Try
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            dt = Nothing
                        End If
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
        End Sub

        Public Sub ProcessReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeAndHandlingTimeReportByStaffByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_STAFF_DAY where convert(varchar(8), service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        Dim strServiceDate As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                        Dim dt As DataTable = GetQueueDataByPeriodDate(strServiceDate, strServiceDate, shTrans)
                        shTrans.CommitTransaction()
                        If dt.Rows.Count > 0 Then
                            Dim sDt As New DataTable
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByDate")
                            sDt = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    dt.DefaultView.RowFilter = "item_id='" & sDr("id") & "'"
                                    Dim drv As DataView = dt.DefaultView
                                    For i As Integer = 0 To drv.Count - 1
                                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtStaffDayCenLinqDB
                                        Try
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.USER_CODE = IIf(IsDBNull(drv(i)("user_code")) = False, drv(i)("user_code"), "-")
                                            lnq.USERNAME = "-"
                                            lnq.STAFF_NAME = IIf(IsDBNull(drv(i)("staff")) = False, drv(i)("staff"), "-")
                                            lnq.SERVICE_DATE = ServiceDate
                                            lnq.SERVICE_ID = sDr("id")
                                            lnq.SERVICE_NAME_EN = sDr("item_name")
                                            lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                            lnq.NUMBER_OF_QUEUE = drv(i)("num_of_queue")
                                            lnq.REGIS = drv(i)("regis")
                                            lnq.SERVE = drv(i)("serve")
                                            lnq.MISS_CALL = drv(i)("misscall")
                                            lnq.CANCEL = drv(i)("cancel")
                                            lnq.NOT_CALL = drv(i)("notcall")
                                            lnq.NOT_CON = drv(i)("notcon")
                                            lnq.NOT_END = drv(i)("notend")
                                            lnq.SUM_WT = drv(i)("SWT")
                                            lnq.COUNT_WT = drv(i)("CWT")
                                            If drv(i)("CWT") > 0 Then lnq.AVG_WT = drv(i)("SWT") / drv(i)("CWT")
                                            lnq.SUM_HT = drv(i)("SHT")
                                            lnq.COUNT_HT = drv(i)("CHT")
                                            If drv(i)("CHT") > 0 Then lnq.AVG_HT = drv(i)("SHT") / drv(i)("CHT")
                                            lnq.SUM_CONT = dt.Rows(i)("SCT")
                                            lnq.COUNT_CONT = dt.Rows(i)("CCT")
                                            If drv(i)("CCT") > 0 Then lnq.AVG_CONT = drv(i)("SCT") / drv(i)("CCT")

                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                            End If
                                            lnq = Nothing
                                        Catch ex As Exception
                                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportByDate", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                        End Try
                                        lnq = Nothing

                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next
                                Next
                                sDt.Dispose()
                            End If
                        End If
                        dt.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcessReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeAndHandlingTimeReportByStaffByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_STAFF_WEEK where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetQueueDataByPeriodDate(FirstDay, LastDay, shTrans)
                        shTrans.CommitTransaction()
                        If dt.Rows.Count > 0 Then
                            Dim sDt As New DataTable
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByWeek")
                            sDt = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    dt.DefaultView.RowFilter = "item_id='" & sDr("id") & "'"
                                    Dim drv As DataView = dt.DefaultView
                                    For i As Integer = 0 To drv.Count - 1
                                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtStaffWeekCenLinqDB
                                        Try
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.USER_CODE = IIf(IsDBNull(drv(i)("user_code")) = False, drv(i)("user_code"), "-")
                                            lnq.USERNAME = "-"
                                            lnq.STAFF_NAME = IIf(IsDBNull(drv(i)("staff")) = False, drv(i)("staff"), "-")
                                            lnq.WEEK_OF_YEAR = WeekNo
                                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                            lnq.PERIOD_DATE = FirstDay & "-" & LastDay
                                            lnq.SERVICE_ID = sDr("id")
                                            lnq.SERVICE_NAME_EN = sDr("item_name")
                                            lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                            lnq.NUMBER_OF_QUEUE = drv(i)("num_of_queue")
                                            lnq.REGIS = drv(i)("regis")
                                            lnq.SERVE = drv(i)("serve")
                                            lnq.MISS_CALL = drv(i)("misscall")
                                            lnq.CANCEL = drv(i)("cancel")
                                            lnq.NOT_CALL = drv(i)("notcall")
                                            lnq.NOT_CON = drv(i)("notcon")
                                            lnq.NOT_END = drv(i)("notend")
                                            lnq.SUM_WT = drv(i)("SWT")
                                            lnq.COUNT_WT = drv(i)("CWT")
                                            If drv(i)("CWT") > 0 Then lnq.AVG_WT = drv(i)("SWT") / drv(i)("CWT")
                                            lnq.SUM_HT = drv(i)("SHT")
                                            lnq.COUNT_HT = drv(i)("CHT")
                                            If drv(i)("CHT") > 0 Then lnq.AVG_HT = drv(i)("SHT") / drv(i)("CHT")
                                            lnq.SUM_CONT = dt.Rows(i)("SCT")
                                            lnq.COUNT_CONT = dt.Rows(i)("CCT")
                                            If drv(i)("CCT") > 0 Then lnq.AVG_CONT = drv(i)("SCT") / drv(i)("CCT")

                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                            End If
                                            lnq = Nothing
                                        Catch ex As Exception
                                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportByWeek", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                        End Try
                                        lnq = Nothing

                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next
                                Next
                                sDt.Dispose()
                            End If
                        End If
                        dt.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcessReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : WaitingTimeAndHandlingTimeReportByStaffByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_WT_HT_STAFF_MONTH where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
                Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetQueueDataByPeriodDate(FirstDay, LastDay, shTrans)
                        shTrans.CommitTransaction()
                        If dt.Rows.Count > 0 Then
                            Dim sDt As New DataTable
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsWaitintTimeHandlingTimeByAgentENG.ProcessReportByMonth")
                            sDt = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            shTrans.CommitTransaction()
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    dt.DefaultView.RowFilter = "item_id='" & sDr("id") & "'"
                                    Dim drv As DataView = dt.DefaultView
                                    For i As Integer = 0 To drv.Count - 1
                                        Dim lnq As New CenLinqDB.TABLE.TbRepWtHtStaffMonthCenLinqDB
                                        Try
                                            lnq.SHOP_ID = shLnq.ID
                                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                            lnq.USER_CODE = IIf(IsDBNull(drv(i)("user_code")) = False, drv(i)("user_code"), "-")
                                            lnq.USERNAME = "-"
                                            lnq.STAFF_NAME = IIf(IsDBNull(drv(i)("staff")) = False, drv(i)("staff"), "-")
                                            lnq.MONTH_NO = ServiceDate.Month
                                            lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                            lnq.SERVICE_ID = sDr("id")
                                            lnq.SERVICE_NAME_EN = sDr("item_name")
                                            lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                            lnq.NUMBER_OF_QUEUE = drv(i)("num_of_queue")
                                            lnq.REGIS = drv(i)("regis")
                                            lnq.SERVE = drv(i)("serve")
                                            lnq.MISS_CALL = drv(i)("misscall")
                                            lnq.CANCEL = drv(i)("cancel")
                                            lnq.NOT_CALL = drv(i)("notcall")
                                            lnq.NOT_CON = drv(i)("notcon")
                                            lnq.NOT_END = drv(i)("notend")
                                            lnq.SUM_WT = drv(i)("SWT")
                                            lnq.COUNT_WT = drv(i)("CWT")
                                            If drv(i)("CWT") > 0 Then lnq.AVG_WT = drv(i)("SWT") / drv(i)("CWT")
                                            lnq.SUM_HT = drv(i)("SHT")
                                            lnq.COUNT_HT = drv(i)("CHT")
                                            If drv(i)("CHT") > 0 Then lnq.AVG_HT = drv(i)("SHT") / drv(i)("CHT")
                                            lnq.SUM_CONT = dt.Rows(i)("SCT")
                                            lnq.COUNT_CONT = dt.Rows(i)("CCT")
                                            If drv(i)("CCT") > 0 Then lnq.AVG_CONT = drv(i)("SCT") / drv(i)("CCT")

                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim ret As Boolean = False
                                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                                            If ret = True Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                            End If
                                            lnq = Nothing
                                        Catch ex As Exception
                                            FunctionEng.SaveErrorLog("ReportsWaitingTimeHandlingTimeByAgentENG.ProcessReportByMonth", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsWaitingTimeHadlingTimeByAgentENG")
                                        End Try
                                        lnq = Nothing

                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next
                                Next
                                sDt.Dispose()
                            End If
                        End If
                        dt.Dispose()
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetQuery() As String
            Dim sql As String = ""
            sql += "select queue_no,customer_id,item_id,item_name,service_date,user_code,staff,wt,ct,ht,cwt,cct,cht from VW_Report"
            Return sql
        End Function

        Private Function GetQueryForSum() As String
            Dim sql As String = ""
            sql += "select count(queue_no) num_of_queue, isnull(AVG(wt),0) as AWT"
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
            sql += " ,isnull(SUM(notend),0) as notend, item_id, user_code,staff"
            sql += " from VW_Report"
            sql += " where 1=1"

            Return sql
        End Function

        Private Function GetQueueDataByPeriodDate(ByVal FirstDay As String, ByVal LastDay As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql = GetQueryForSum()
            sql += " and convert(varchar(8), service_date, 112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by item_id, user_code,staff"

            Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As New DataTable
            dt = hLnq.GetListBySql(sql, shTrans.Trans)
            hLnq = Nothing

            Return dt
        End Function

        'Private Function GetQueueDataByMonth(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        '    Dim sql As String = ""
        '    sql = GetQueryForSum()
        '    sql += " and convert(varchar(6), service_date, 112) = '" & ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "' "
        '    sql += " group by item_id, user_code,staff"

        '    Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
        '    Dim dt As New DataTable
        '    dt = hLnq.GetListBySql(sql, shTrans.Trans)
        '    hLnq = Nothing

        '    Return dt
        'End Function

        'Private Function GetQueueDataByWeek(ByVal FirstDay As String, ByVal LastDay As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        '    Dim sql As String = ""
        '    sql = GetQueryForSum()
        '    sql += " and convert(varchar(8), service_date, 112) between '" & FirstDay & "' and '" & LastDay & "' "
        '    sql += " group by item_id, user_code,staff"

        '    Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
        '    Dim dt As New DataTable
        '    dt = hLnq.GetListBySql(sql, shTrans.Trans)
        '    hLnq = Nothing

        '    Return dt
        'End Function

        'Private Function GetQueueDataByDay(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        '    Dim sql As String = ""
        '    sql = GetQueryForSum()
        '    sql += " and  YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
        '    sql += " group by item_id, user_code,staff"

        '    Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
        '    Dim dt As New DataTable
        '    dt = hLnq.GetListBySql(sql, shTrans.Trans)
        '    hLnq = Nothing

        '    Return dt
        'End Function

        Private Function GetQueueDataByTime(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql = GetQuery()
            sql += " where  YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & " and UPPER(isnull(staff,'')) not like '%ADMIN%' "

            Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim dt As DataTable = hLnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                hLnq = Nothing
                Return dt
            Else
                Return Nothing
            End If
        End Function

    End Class
End Namespace

