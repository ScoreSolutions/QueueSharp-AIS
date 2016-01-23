Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportsCustByNetworkTypeENG : Inherits ReportsENG
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


        Public Sub NetworkTypeProcessAllReport()
            ProcReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcReportByDay(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub


        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.CustomerByNetworkTypeByTime, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_network_time where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        trans.CommitTransaction()
                        If dtI.Rows.Count > 0 Then
                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByTime")
                            If shTrans.Trans IsNot Nothing Then
                                Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                                Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)
                                shTrans.CommitTransaction()
                                If sDt.Rows.Count > 0 Then

                                    For Each drI As DataRow In dtI.Rows
                                        Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))

                                        'Loop ตามเวลาที่ เปิด ปิด Shop
                                        Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
                                        Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)
                                        Dim CurrTime As DateTime = StartTime
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByTime")
                                        If shTrans.Trans IsNot Nothing Then
                                            Do
                                                If CurrTime < EndTime Then
                                                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                                    If TimeTo > EndTime Then
                                                        TimeTo = EndTime
                                                    End If
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkTimeCenLinqDB
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")

                                                    If sDt.Rows.Count > 0 Then
                                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByTime")
                                                        Dim dtN As New DataTable
                                                        dtN = GetServiceTime(ServiceDate, lnq.TIME_PRIOD_FROM, lnq.TIME_PRIOD_TO, shTrans)
                                                        shTrans.CommitTransaction()
                                                        For Each sDr As DataRow In sDt.Rows
                                                            'shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByTime")
                                                            Dim ServiceID As String = sDr("id")
                                                            Dim GSM_REGIS As Int32 = 0
                                                            Dim GSM_SERVED As Int32 = 0
                                                            Dim GSM_MISSED_CALL As Int32 = 0
                                                            Dim GSM_CANCEL As Int32 = 0
                                                            Dim GSM_NOTCALL As Int32 = 0
                                                            Dim GSM_NOTCON As Int32 = 0
                                                            Dim GSM_NOTEND As Int32 = 0

                                                            Dim OTC_REGIS As Int32 = 0
                                                            Dim OTC_SERVED As Int32 = 0
                                                            Dim OTC_MISSED_CALL As Int32 = 0
                                                            Dim OTC_CANCEL As Int32 = 0
                                                            Dim OTC_NOTCALL As Int32 = 0
                                                            Dim OTC_NOTCON As Int32 = 0
                                                            Dim OTC_NOTEND As Int32 = 0

                                                            Dim AIS3G_REGIS As Int32 = 0
                                                            Dim AIS3G_SERVED As Int32 = 0
                                                            Dim AIS3G_MISSED_CALL As Int32 = 0
                                                            Dim AIS3G_CANCEL As Int32 = 0
                                                            Dim AIS3G_NOTCALL As Int32 = 0
                                                            Dim AIS3G_NOTCON As Int32 = 0
                                                            Dim AIS3G_NOTEND As Int32 = 0

                                                            Dim OTC3G_REGIS As Int32 = 0
                                                            Dim OTC3G_SERVED As Int32 = 0
                                                            Dim OTC3G_MISSED_CALL As Int32 = 0
                                                            Dim OTC3G_CANCEL As Int32 = 0
                                                            Dim OTC3G_NOTCALL As Int32 = 0
                                                            Dim OTC3G_NOTCON As Int32 = 0
                                                            Dim OTC3G_NOTEND As Int32 = 0

                                                            Dim NON_REGIS As Int32 = 0
                                                            Dim NON_SERVED As Int32 = 0
                                                            Dim NON_MISSED_CALL As Int32 = 0
                                                            Dim NON_CANCEL As Int32 = 0
                                                            Dim NON_NOTCALL As Int32 = 0
                                                            Dim NON_NOTCON As Int32 = 0
                                                            Dim NON_NOTEND As Int32 = 0

                                                            If dtN.Rows.Count > 0 Then
                                                                dtN.DefaultView.RowFilter = "NetworkType in ('Post-paid','GSM Advance','GSM1800') and item_id='" & ServiceID & "'"
                                                                If dtN.DefaultView.Count > 0 Then
                                                                    For Each drN As DataRowView In dtN.DefaultView
                                                                        GSM_REGIS += drN("regis")
                                                                        GSM_SERVED += drN("serve")
                                                                        GSM_MISSED_CALL += drN("misscall")
                                                                        GSM_CANCEL += drN("cancel")
                                                                        GSM_NOTCALL += drN("notcall")
                                                                        GSM_NOTCON += drN("notcon")
                                                                        GSM_NOTEND += drN("notend")
                                                                    Next
                                                                End If

                                                                dtN.DefaultView.RowFilter = "NetworkType in ('One-2-Call','Pre-paid','OTC') and item_id='" & ServiceID & "'"
                                                                If dtN.DefaultView.Count > 0 Then
                                                                    For Each drN As DataRowView In dtN.DefaultView
                                                                        OTC_REGIS += drN("regis")
                                                                        OTC_SERVED += drN("serve")
                                                                        OTC_MISSED_CALL += drN("misscall")
                                                                        OTC_CANCEL += drN("cancel")
                                                                        OTC_NOTCALL += drN("notcall")
                                                                        OTC_NOTCON += drN("notcon")
                                                                        OTC_NOTEND += drN("notend")
                                                                    Next
                                                                End If

                                                                dtN.DefaultView.RowFilter = "NetworkType='Non Mobile' and item_id='" & ServiceID & "'"
                                                                If dtN.DefaultView.Count > 0 Then
                                                                    NON_REGIS = dtN.DefaultView(0)("regis")
                                                                    NON_SERVED = dtN.DefaultView(0)("serve")
                                                                    NON_MISSED_CALL = dtN.DefaultView(0)("misscall")
                                                                    NON_CANCEL = dtN.DefaultView(0)("cancel")
                                                                    NON_NOTCALL = dtN.DefaultView(0)("notcall")
                                                                    NON_NOTCON = dtN.DefaultView(0)("notcon")
                                                                    NON_NOTEND = dtN.DefaultView(0)("notend")
                                                                End If

                                                                dtN.DefaultView.RowFilter = "NetworkType='AIS 3G' and item_id='" & ServiceID & "'"
                                                                If dtN.DefaultView.Count > 0 Then
                                                                    AIS3G_REGIS = dtN.DefaultView(0)("regis")
                                                                    AIS3G_SERVED = dtN.DefaultView(0)("serve")
                                                                    AIS3G_MISSED_CALL = dtN.DefaultView(0)("misscall")
                                                                    AIS3G_CANCEL = dtN.DefaultView(0)("cancel")
                                                                    AIS3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                                                    AIS3G_NOTCON = dtN.DefaultView(0)("notcon")
                                                                    AIS3G_NOTEND = dtN.DefaultView(0)("notend")
                                                                End If

                                                                dtN.DefaultView.RowFilter = "NetworkType='AIS 3G One-2-Call' and item_id='" & ServiceID & "'"
                                                                If dtN.DefaultView.Count > 0 Then
                                                                    OTC3G_REGIS = dtN.DefaultView(0)("regis")
                                                                    OTC3G_SERVED = dtN.DefaultView(0)("serve")
                                                                    OTC3G_MISSED_CALL = dtN.DefaultView(0)("misscall")
                                                                    OTC3G_CANCEL = dtN.DefaultView(0)("cancel")
                                                                    OTC3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                                                    OTC3G_NOTCON = dtN.DefaultView(0)("notcon")
                                                                    OTC3G_NOTEND = dtN.DefaultView(0)("notend")
                                                                End If
                                                                dtN.DefaultView.RowFilter = ""
                                                            End If

                                                            'shTrans.CommitTransaction()
                                                            Dim tmpValue As String = sDr("id") & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & GSM_REGIS & "|" & GSM_SERVED & "|" & GSM_MISSED_CALL & "|" & GSM_CANCEL & "|" & GSM_NOTCALL & "|" & GSM_NOTCON & "|" & GSM_NOTEND & "|" & OTC_REGIS & "|" & OTC_SERVED & "|" & OTC_MISSED_CALL & "|" & OTC_CANCEL & "|" & OTC_NOTCALL & "|" & OTC_NOTCON & "|" & OTC_NOTEND & "|" & NON_REGIS & "|" & NON_SERVED & "|" & NON_MISSED_CALL & "|" & NON_CANCEL & "|" & NON_NOTCALL & "|" & NON_NOTCON & "|" & NON_NOTEND & "|" & AIS3G_REGIS & "|" & AIS3G_SERVED & "|" & AIS3G_MISSED_CALL & "|" & AIS3G_CANCEL & "|" & AIS3G_NOTCALL & "|" & AIS3G_NOTCON & "|" & AIS3G_NOTEND & "|" & OTC3G_REGIS & "|" & OTC3G_SERVED & "|" & OTC3G_MISSED_CALL & "|" & OTC3G_CANCEL & "|" & OTC3G_NOTCALL & "|" & OTC3G_NOTCON & "|" & OTC3G_NOTEND
                                                            If lnq.DATA_VALUE = "" Then
                                                                lnq.DATA_VALUE = tmpValue
                                                            Else
                                                                lnq.DATA_VALUE += "###" & tmpValue 'sDr("id") & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & GSM_REGIS & "|" & GSM_SERVED & "|" & GSM_MISSED_CALL & "|" & GSM_CANCEL & "|" & GSM_NOTCALL & "|" & GSM_NOTCON & "|" & GSM_NOTEND & "|" & OTC_REGIS & "|" & OTC_SERVED & "|" & OTC_MISSED_CALL & "|" & OTC_CANCEL & "|" & OTC_NOTCALL & "|" & OTC_NOTCON & "|" & OTC_NOTEND & "|" & NON_REGIS & "|" & NON_SERVED & "|" & NON_MISSED_CALL & "|" & NON_CANCEL & "|" & NON_NOTCALL & "|" & NON_NOTCON & "|" & NON_NOTEND
                                                            End If
                                                        Next
                                                        dtN.Dispose()
                                                    End If

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsCustByNetworkTypeENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByNetworkTypeENG")
                                                    End If
                                                End If
                                                CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)

                                                Try
                                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                    Application.DoEvents()
                                                Catch ex As Exception

                                                End Try
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
                        End If
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
        End Sub

        Public Sub ProcReportByDay(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerByNetworkTypeByDay", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_network_day where convert(varchar(8), service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByDay")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByDay")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                            Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)
                            If sDt.Rows.Count > 0 Then
                                Dim dtN As New DataTable
                                dtN = GetServiceDate(ServiceDate, shTrans)
                                shTrans.CommitTransaction()
                                For Each sDr As DataRow In sDt.Rows
                                    Dim ServiceID As String = sDr("id")

                                    'shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByDay")
                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkDayCenLinqDB
                                    lnq.SERVICE_DATE = ServiceDate
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_DATE = ServiceDate
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.SERVICE_NAME_EN = sDr("item_name")

                                    If dtN.Rows.Count > 0 Then
                                        dtN.DefaultView.RowFilter = "NetworkType in ('Post-paid','GSM Advance','GSM1800') and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            For Each drN As DataRowView In dtN.DefaultView
                                                lnq.GSM_REGIS += drN("regis")
                                                lnq.GSM_SERVE += drN("serve")
                                                lnq.GSM_MISS_CALL += drN("misscall")
                                                lnq.GSM_CANCEL += drN("cancel")
                                                lnq.GSM_NOTCALL += drN("notcall")
                                                lnq.GSM_NOTCON += drN("notcon")
                                                lnq.GSM_NOTEND += drN("notend")
                                            Next
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType in ('One-2-Call','Pre-paid','OTC') and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            For Each drN As DataRowView In dtN.DefaultView
                                                lnq.OTC_REGIS += drN("regis")
                                                lnq.OTC_SERVE += drN("serve")
                                                lnq.OTC_MISS_CALL += drN("misscall")
                                                lnq.OTC_CANCEL += drN("cancel")
                                                lnq.OTC_NOTCALL += drN("notcall")
                                                lnq.OTC_NOTCON += drN("notcon")
                                                lnq.OTC_NOTEND += drN("notend")
                                            Next
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='Non Mobile' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.NON_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.NON_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.NON_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.NON_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.NON_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.NON_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.NON_NOTEND = dtN.DefaultView(0)("notend")
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='AIS 3G' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.AIS3G_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.AIS3G_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.AIS3G_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.AIS3G_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.AIS3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.AIS3G_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.AIS3G_NOTEND = dtN.DefaultView(0)("notend")
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='AIS 3G One-2-Call' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.OTC3G_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.OTC3G_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.OTC3G_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.OTC3G_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.OTC3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.OTC3G_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.OTC3G_NOTEND = dtN.DefaultView(0)("notend")
                                        End If
                                    End If

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsCustByNetworkTypeENG.ProcReportByDay", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByNetworkTypeENG")
                                    End If
                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                Next
                                sDt.Dispose()
                            End If
                        Else
                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                        End If
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerByNetworkTypeByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_network_week where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByWeek")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                            Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)

                            If sDt.Rows.Count > 0 Then
                                Dim dtN As New DataTable
                                dtN = GetServiceWeek(ServiceDate, FirstDay, LastDay, shTrans)
                                shTrans.CommitTransaction()
                                For Each sDr As DataRow In sDt.Rows
                                    Dim ServiceID As String = sDr("id")

                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkWeekCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.WEEK_OF_YEAR = WeekNo
                                    lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.PERIOD_DATE = FirstDay & " - " & LastDay
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.SERVICE_NAME_EN = sDr("item_name")

                                    If dtN.Rows.Count > 0 Then
                                        dtN.DefaultView.RowFilter = "NetworkType in ('Post-paid','GSM Advance','GSM1800') and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            For Each drN As DataRowView In dtN.DefaultView
                                                lnq.GSM_REGIS += drN("regis")
                                                lnq.GSM_SERVE += drN("serve")
                                                lnq.GSM_MISS_CALL += drN("misscall")
                                                lnq.GSM_CANCEL += drN("cancel")
                                                lnq.GSM_NOTCALL += drN("notcall")
                                                lnq.GSM_NOTCON += drN("notcon")
                                                lnq.GSM_NOTEND += drN("notend")
                                            Next
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType in ('One-2-Call','Pre-paid','OTC') and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            For Each drN As DataRowView In dtN.DefaultView
                                                lnq.OTC_REGIS += drN("regis")
                                                lnq.OTC_SERVE += drN("serve")
                                                lnq.OTC_MISS_CALL += drN("misscall")
                                                lnq.OTC_CANCEL += drN("cancel")
                                                lnq.OTC_NOTCALL += drN("notcall")
                                                lnq.OTC_NOTCON += drN("notcon")
                                                lnq.OTC_NOTEND += drN("notend")
                                            Next
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='Non Mobile' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.NON_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.NON_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.NON_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.NON_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.NON_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.NON_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.NON_NOTEND = dtN.DefaultView(0)("notend")
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='AIS 3G' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.AIS3G_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.AIS3G_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.AIS3G_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.AIS3G_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.AIS3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.AIS3G_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.AIS3G_NOTEND = dtN.DefaultView(0)("notend")
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='AIS 3G One-2-Call' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.OTC3G_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.OTC3G_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.OTC3G_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.OTC3G_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.OTC3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.OTC3G_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.OTC3G_NOTEND = dtN.DefaultView(0)("notend")
                                        End If
                                        dtN.DefaultView.RowFilter = ""
                                    End If

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsCustByNetworkTypeENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByNetworkTypeENG")
                                    End If
                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                Next
                                sDt.Dispose()
                            End If
                        Else
                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                        End If
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerByNetworkTypeByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_network_month where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Service ของ Shop
                    Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB

                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByNetworkTypeENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                        Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)

                        If sDt.Rows.Count > 0 Then
                            Dim dtN As New DataTable
                            dtN = GetServiceMonth(ServiceDate, shTrans)
                            shTrans.CommitTransaction()
                            For Each sDr As DataRow In sDt.Rows
                                Try
                                    Dim ServiceID As String = sDr("id")
                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkMonthCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.MONTH_NO = ServiceDate.Month
                                    lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                    lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.SERVICE_NAME_EN = sDr("item_name")

                                    If dtN.Rows.Count > 0 Then
                                        dtN.DefaultView.RowFilter = "NetworkType in ('Post-paid','GSM Advance','GSM1800') and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            For Each drN As DataRowView In dtN.DefaultView
                                                lnq.GSM_REGIS += drN("regis")
                                                lnq.GSM_SERVE += drN("serve")
                                                lnq.GSM_MISS_CALL += drN("misscall")
                                                lnq.GSM_CANCEL += drN("cancel")
                                                lnq.GSM_NOTCALL += drN("notcall")
                                                lnq.GSM_NOTCON += drN("notcon")
                                                lnq.GSM_NOTEND += drN("notend")
                                            Next
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType in ('One-2-Call','Pre-paid','OTC') and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            For Each drN As DataRowView In dtN.DefaultView
                                                lnq.OTC_REGIS += drN("regis")
                                                lnq.OTC_SERVE += drN("serve")
                                                lnq.OTC_MISS_CALL += drN("misscall")
                                                lnq.OTC_CANCEL += drN("cancel")
                                                lnq.OTC_NOTCALL += drN("notcall")
                                                lnq.OTC_NOTCON += drN("notcon")
                                                lnq.OTC_NOTEND += drN("notend")
                                            Next
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='Non Mobile' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.NON_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.NON_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.NON_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.NON_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.NON_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.NON_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.NON_NOTEND = dtN.DefaultView(0)("notend")
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='AIS 3G' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.AIS3G_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.AIS3G_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.AIS3G_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.AIS3G_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.AIS3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.AIS3G_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.AIS3G_NOTEND = dtN.DefaultView(0)("notend")
                                        End If

                                        dtN.DefaultView.RowFilter = "NetworkType='AIS 3G One-2-Call' and item_id='" & ServiceID & "'"
                                        If dtN.DefaultView.Count > 0 Then
                                            lnq.OTC3G_REGIS = dtN.DefaultView(0)("regis")
                                            lnq.OTC3G_SERVE = dtN.DefaultView(0)("serve")
                                            lnq.OTC3G_MISS_CALL = dtN.DefaultView(0)("misscall")
                                            lnq.OTC3G_CANCEL = dtN.DefaultView(0)("cancel")
                                            lnq.OTC3G_NOTCALL = dtN.DefaultView(0)("notcall")
                                            lnq.OTC3G_NOTCON = dtN.DefaultView(0)("notcon")
                                            lnq.OTC3G_NOTEND = dtN.DefaultView(0)("notend")
                                        End If
                                        dtN.DefaultView.RowFilter = ""
                                    End If

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsCustByNetworkTypeENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByNetworkTypeENG")
                                    End If
                                Catch ex As Exception
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCustByNetworkTypeENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & ex.Message, Application.StartupPath & "\ErrorLog\", "ReportsCustByNetworkTypeENG")
                                End Try

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            sDt.Dispose()
                        End If
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                    sLnq = Nothing
                    'Else
                    '    UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    'End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetServiceTime(ByVal ServiceDate As DateTime, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim StrTimeFrom As String = TimeFrom.ToString("HH:mm")
            Dim StrTimeTo As String = DateAdd(DateInterval.Minute, -1, TimeTo).ToString("HH:mm")
            Dim StrDate As String = ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = GetQuery()
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by NetworkType,item_id"

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function


        Private Function GetServiceDate(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim DateStr As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date,112) between '" & DateStr & "' and '" & DateStr & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by NetworkType,item_id"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
        Private Function GetServiceWeek(ByVal ServiceDate As DateTime, ByVal FirstDay As String, ByVal LastDay As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by NetworkType,item_id"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
        Private Function GetServiceMonth(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
            Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by NetworkType,item_id"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function

        Private Function GetQuery(ByVal ServiceID As Long) As String
            Dim sql As String = ""
            sql += "select isnull(NetworkType,'Non Mobile') NetworkType ,isnull(SUM(regis),0) as regis,isnull(SUM(serve),0) as serve,isnull(SUM(misscall),0) as misscall ,isnull(SUM(cancel),0) as cancel,isnull(SUM(notcall),0) as notcall,isnull(SUM(notcon),0) as notcon ,isnull(SUM(notend),0) as notend from VW_Report"
            sql += " where item_id =" & ServiceID
            Return sql
        End Function
        Private Function GetQuery() As String
            Dim sql As String = ""
            sql += "select isnull(NetworkType,'Non Mobile') NetworkType ,item_id ,isnull(SUM(regis),0) as regis,isnull(SUM(serve),0) as serve,isnull(SUM(misscall),0) as misscall ,isnull(SUM(cancel),0) as cancel,isnull(SUM(notcall),0) as notcall,isnull(SUM(notcon),0) as notcon ,isnull(SUM(notend),0) as notend from VW_Report"
            sql += " where 1=1"
            Return sql
        End Function
    End Class

End Namespace
