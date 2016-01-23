Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities.Constant

Namespace Reports
    Public Class ReportsCustBySegmentENG : Inherits ReportsENG

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


        Public Sub SegmentProcessAllReport()
            ProcReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub


        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & ReportName.CustomerBySegmentByTime, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_segment_time where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        trans.CommitTransaction()
                        If dtI.Rows.Count > 0 Then
                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByTime")
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
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByTime")
                                        If shTrans.Trans IsNot Nothing Then
                                            Do
                                                If CurrTime < EndTime Then
                                                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                                    If TimeTo > EndTime Then
                                                        TimeTo = EndTime
                                                    End If
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentTimeCenLinqDB
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")

                                                    If sDt.Rows.Count > 0 Then
                                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByTime")
                                                        Dim dtSM As New DataTable
                                                        dtSM = GetServiceTime(ServiceDate, lnq.TIME_PRIOD_FROM, lnq.TIME_PRIOD_TO, shTrans)
                                                        shTrans.CommitTransaction()

                                                        For Each sDr As DataRow In sDt.Rows
                                                            Try
                                                                'shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByTime")
                                                                'If shTrans.Trans IsNot Nothing Then
                                                                Dim ServiceID As String = sDr("id")
                                                                Dim MASS_REGIS As Int32 = 0
                                                                Dim MASS_SERVED As Int32 = 0
                                                                Dim MASS_MISSED_CALL As Int32 = 0
                                                                Dim MASS_CANCEL As Int32 = 0
                                                                Dim MASS_NOTCALL As Int32 = 0
                                                                Dim MASS_NOTCON As Int32 = 0
                                                                Dim MASS_NOTEND As Int32 = 0
                                                                Dim SERENADE_REGIS As Int32 = 0
                                                                Dim SERENADE_SERVED As Int32 = 0
                                                                Dim SERENADE_MISSED_CALL As Int32 = 0
                                                                Dim SERENADE_CANCEL As Int32 = 0
                                                                Dim SERENADE_NOTCALL As Int32 = 0
                                                                Dim SERENADE_NOTCON As Int32 = 0
                                                                Dim SERENADE_NOTEND As Int32 = 0

                                                                If dtSM.Rows.Count > 0 Then
                                                                    dtSM.DefaultView.RowFilter = "customertype_name='Mass' and item_id='" & ServiceID & "'"
                                                                    If dtSM.DefaultView.Count > 0 Then
                                                                        MASS_REGIS = dtSM.DefaultView(0)("regis")
                                                                        MASS_SERVED = dtSM.DefaultView(0)("serve")
                                                                        MASS_MISSED_CALL = dtSM.DefaultView(0)("misscall")
                                                                        MASS_CANCEL = dtSM.DefaultView(0)("cancel")
                                                                        MASS_NOTCALL = dtSM.DefaultView(0)("notcall")
                                                                        MASS_NOTCON = dtSM.DefaultView(0)("notcon")
                                                                        MASS_NOTEND = dtSM.DefaultView(0)("notend")
                                                                    End If

                                                                    dtSM.DefaultView.RowFilter = "customertype_name='Serenade' and item_id='" & ServiceID & "'"
                                                                    If dtSM.DefaultView.Count > 0 Then
                                                                        SERENADE_REGIS = dtSM.DefaultView(0)("regis")
                                                                        SERENADE_SERVED = dtSM.DefaultView(0)("serve")
                                                                        SERENADE_MISSED_CALL = dtSM.DefaultView(0)("misscall")
                                                                        SERENADE_CANCEL = dtSM.DefaultView(0)("cancel")
                                                                        SERENADE_NOTCALL = dtSM.DefaultView(0)("notcall")
                                                                        SERENADE_NOTCON = dtSM.DefaultView(0)("notcon")
                                                                        SERENADE_NOTEND = dtSM.DefaultView(0)("notend")
                                                                    End If
                                                                    dtSM.DefaultView.RowFilter = ""
                                                                End If
                                                                'shTrans.CommitTransaction()

                                                                Dim tmpCol As String = "ServiceID|ServiceNameEN|ServiceNameTH|MASS_REGIS" & "|MASS_SERVED" & "|MASS_MISSED_CALL" & "|MASS_CANCEL" & "|MASS_NOTCALL" & "|MASS_NOTCON" & "|MASS_NOTEND" & "|SERENADE_REGIS" & "|SERENADE_SERVED" & "|SERENADE_MISSED_CALL" & "|SERENADE_CANCEL" & "|SERENADE_NOTCALL" & "|SERENADE_NOTCON" & "|SERENADE_NOTEND"
                                                                Dim tmpVal As String = sDr("id") & "|" & sDr("item_name") & "|" & sDr("item_name_th") & "|" & MASS_REGIS & "|" & MASS_SERVED & "|" & MASS_MISSED_CALL & "|" & MASS_CANCEL & "|" & MASS_NOTCALL & "|" & MASS_NOTCON & "|" & MASS_NOTEND & "|" & SERENADE_REGIS & "|" & SERENADE_SERVED & "|" & SERENADE_MISSED_CALL & "|" & SERENADE_CANCEL & "|" & SERENADE_NOTCALL & "|" & SERENADE_NOTCON & "|" & SERENADE_NOTEND
                                                                If lnq.DATA_COLUMN = "" Then
                                                                    lnq.DATA_COLUMN = tmpCol
                                                                    lnq.DATA_VALUE = tmpVal
                                                                Else
                                                                    lnq.DATA_COLUMN += "###" & tmpCol
                                                                    lnq.DATA_VALUE += "###" & tmpVal
                                                                End If
                                                                'End If
                                                                Try
                                                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                                    Application.DoEvents()
                                                                Catch ex As Exception

                                                                End Try
                                                            Catch ex As Exception
                                                                FunctionEng.SaveErrorLog("ReportsCustBySegmentENG.ProcReportByTime", " Exception :" & shLnq.SHOP_ABB & " " & ex.Message, Application.StartupPath & "\ErrorLog\", "ReportsCustBySegmentENG")
                                                            End Try
                                                        Next
                                                        dtSM.Dispose()
                                                    End If

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcReportByTime", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsCustBySegmentENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustBySegmentENG")
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
                UpdateProcessTime(ProcID)
            End If
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerBySegmentByDate", ServiceDate)
            If ProcID <> 0 Then
                Try

                
                    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_segment_day where convert(varchar(8), service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                    dTrans.CommitTransaction()

                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    If trans.Trans IsNot Nothing Then
                        'หา Service ของ Shop
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByDate")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                            Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)
                            If sDt.Rows.Count > 0 Then
                                Dim mDt As DataTable = GetSegmentDateQty(ServiceDate, shTrans)
                                shTrans.CommitTransaction()

                                For Each sDr As DataRow In sDt.Rows
                                    'shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByDate")
                                    Dim ServiceID As String = sDr("id")

                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentDayCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_DATE = ServiceDate
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")

                                    If mDt.Rows.Count > 0 Then
                                        mDt.DefaultView.RowFilter = "customertype_name='Mass' and item_id='" & ServiceID & "'"
                                        If mDt.DefaultView.Count > 0 Then
                                            lnq.MASS_REGIS = Convert.ToInt64(mDt.DefaultView(0)("regis"))
                                            lnq.MASS_SERVE = Convert.ToInt64(mDt.DefaultView(0)("serve"))
                                            lnq.MASS_MISS_CALL = Convert.ToInt64(mDt.DefaultView(0)("misscall"))
                                            lnq.MASS_CANCEL = Convert.ToInt64(mDt.DefaultView(0)("cancel"))
                                            lnq.MASS_INCOMPLETE = Convert.ToInt64(mDt.DefaultView(0)("notcall")) + Convert.ToInt64(mDt.DefaultView(0)("notcon")) + Convert.ToInt64(mDt.DefaultView(0)("notend"))
                                        End If

                                        mDt.DefaultView.RowFilter = "customertype_name='Serenade' and item_id='" & ServiceID & "'"
                                        If mDt.DefaultView.Count > 0 Then
                                            lnq.SERENADE_REGIS = Convert.ToInt64(mDt.DefaultView(0)("regis"))
                                            lnq.SERENADE_SERVE = Convert.ToInt64(mDt.DefaultView(0)("serve"))
                                            lnq.SERENADE_MISS_CALL = Convert.ToInt64(mDt.DefaultView(0)("misscall"))
                                            lnq.SERENADE_CANCEL = Convert.ToInt64(mDt.DefaultView(0)("cancel"))
                                            lnq.SERENADE_INCOMPLETE = Convert.ToInt64(mDt.DefaultView(0)("notcall")) + Convert.ToInt64(mDt.DefaultView(0)("notcon")) + Convert.ToInt64(mDt.DefaultView(0)("notend"))
                                        End If
                                        mDt.DefaultView.RowFilter = ""
                                    End If
                                    'shTrans.CommitTransaction()

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsCustBySegmentENG.ProcReportByDate", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustBySegmentENG")
                                    End If
                                    lnq = Nothing

                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                Next
                                sDt.Dispose()
                                mDt.Dispose()
                            End If
                            sLnq = Nothing
                        Else
                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                        End If
                    End If
                    UpdateProcessTime(ProcID)
                Catch ex As Exception
                    UpdateProcessError(ProcID, "Exception : " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerBySegmentByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_segment_week where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As String = GetFirstDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim LastDay As String = GetLastDayOfWeek(ServiceDate).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Service ของ Shop
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                        Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)
                        If sDt.Rows.Count > 0 Then
                            Dim mDt As DataTable = GetSegmentWeekQty(ServiceDate, FirstDay, LastDay, shTrans)
                            shTrans.CommitTransaction()

                            For Each sDr As DataRow In sDt.Rows
                                'shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByWeek")
                                Dim ServiceID As String = sDr("id")

                                Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentWeekCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.WEEK_OF_YEAR = WeekNo
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.PERIOD_DATE = FirstDay & " - " & LastDay
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = sDr("item_name")
                                lnq.SERVICE_NAME_TH = sDr("item_name_th")

                                If mDt.Rows.Count > 0 Then
                                    mDt.DefaultView.RowFilter = "customertype_name='Mass' and item_id='" & ServiceID & "'"
                                    If mDt.DefaultView.Count > 0 Then
                                        lnq.MASS_REGIS = Convert.ToInt64(mDt.DefaultView(0)("regis"))
                                        lnq.MASS_SERVE = Convert.ToInt64(mDt.DefaultView(0)("serve"))
                                        lnq.MASS_MISS_CALL = Convert.ToInt64(mDt.DefaultView(0)("misscall"))
                                        lnq.MASS_CANCEL = Convert.ToInt64(mDt.DefaultView(0)("cancel"))
                                        lnq.MASS_INCOMPLETE = Convert.ToInt64(mDt.DefaultView(0)("notcall")) + Convert.ToInt64(mDt.DefaultView(0)("notcon")) + Convert.ToInt64(mDt.DefaultView(0)("notend"))
                                    End If

                                    mDt.DefaultView.RowFilter = "customertype_name='Serenade' and item_id='" & ServiceID & "'"
                                    If mDt.DefaultView.Count > 0 Then
                                        lnq.SERENADE_REGIS = Convert.ToInt64(mDt.DefaultView(0)("regis"))
                                        lnq.SERENADE_SERVE = Convert.ToInt64(mDt.DefaultView(0)("serve"))
                                        lnq.SERENADE_MISS_CALL = Convert.ToInt64(mDt.DefaultView(0)("misscall"))
                                        lnq.SERENADE_CANCEL = Convert.ToInt64(mDt.DefaultView(0)("cancel"))
                                        lnq.SERENADE_INCOMPLETE = Convert.ToInt64(mDt.DefaultView(0)("notcall")) + Convert.ToInt64(mDt.DefaultView(0)("notcon")) + Convert.ToInt64(mDt.DefaultView(0)("notend"))
                                    End If
                                    mDt.DefaultView.RowFilter = ""
                                End If
                                'shTrans.CommitTransaction()

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCustBySegmentENG.ProcReportByMonth", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustBySegmentENG")
                                End If
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            mDt.Dispose()
                            sDt.Dispose()
                        End If
                        sLnq = Nothing
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
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerBySegmentByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_segment_month where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Service ของ Shop
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                        Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)

                        If sDt.Rows.Count > 0 Then
                            Dim mDt As DataTable = GetSegmentMonthQty(ServiceDate, shTrans)
                            shTrans.CommitTransaction()
                            For Each sDr As DataRow In sDt.Rows
                                'shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustBySegmentENG.ProcReportByMonth")
                                Dim ServiceID As String = sDr("id")
                                Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentMonthCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.MONTH_NO = ServiceDate.Month
                                lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = sDr("item_name")
                                lnq.SERVICE_NAME_TH = sDr("item_name_th")

                                If mDt.Rows.Count > 0 Then
                                    mDt.DefaultView.RowFilter = "customertype_name='MASS' and item_id='" & ServiceID & "'"
                                    If mDt.DefaultView.Count > 0 Then
                                        lnq.MASS_REGIS = Convert.ToInt64(mDt.DefaultView(0)("regis"))
                                        lnq.MASS_SERVE = Convert.ToInt64(mDt.DefaultView(0)("serve"))
                                        lnq.MASS_MISS_CALL = Convert.ToInt64(mDt.DefaultView(0)("misscall"))
                                        lnq.MASS_CANCEL = Convert.ToInt64(mDt.DefaultView(0)("cancel"))
                                        lnq.MASS_INCOMPLETE = Convert.ToInt64(mDt.DefaultView(0)("notcall")) + Convert.ToInt64(mDt.DefaultView(0)("notcon")) + Convert.ToInt64(mDt.DefaultView(0)("notend"))
                                    End If

                                    mDt.DefaultView.RowFilter = "customertype_name='Serenade' and item_id='" & ServiceID & "'"
                                    If mDt.DefaultView.Count > 0 Then
                                        lnq.SERENADE_REGIS = Convert.ToInt64(mDt.DefaultView(0)("regis"))
                                        lnq.SERENADE_SERVE = Convert.ToInt64(mDt.DefaultView(0)("serve"))
                                        lnq.SERENADE_MISS_CALL = Convert.ToInt64(mDt.DefaultView(0)("misscall"))
                                        lnq.SERENADE_CANCEL = Convert.ToInt64(mDt.DefaultView(0)("cancel"))
                                        lnq.SERENADE_INCOMPLETE = Convert.ToInt64(mDt.DefaultView(0)("notcall")) + Convert.ToInt64(mDt.DefaultView(0)("notcon")) + Convert.ToInt64(mDt.DefaultView(0)("notend"))
                                    End If
                                    mDt.DefaultView.RowFilter = ""
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcessReports", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCustBySegmentENG.ProcReportByMonth", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustBySegmentENG")
                                End If
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                            mDt.Dispose()
                            sDt.Dispose()
                        End If
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetSegmentDateQty(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim DateStr As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date,112) between '" & DateStr & "' and '" & DateStr & "' "    'ใช้คำสั่ง between จะคิวรี่ได้เร็วกว่าใช้ =
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by customertype_name,item_id"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
        Private Function GetSegmentWeekQty(ByVal ServiceDate As DateTime, ByVal FirstDay As String, ByVal LastDay As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            'sql += " and customertype_name = '" & Segment & "'" & vbNewLine
            sql += " group by customertype_name,item_id"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
        Private Function GetSegmentMonthQty(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim FirstDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & "01"
            Dim LastDay As String = ServiceDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US")) & DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month)

            Dim sql As String = GetQuery()
            sql += " and convert(varchar(8), service_date,112) between '" & FirstDay & "' and '" & LastDay & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by customertype_name,item_id"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function

        

        'Private Function GetServiceSegmentQty(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal ServiceID As Long, ByVal Segment As String, ByVal RepType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
        '    Dim ret As Long = 0
        '    'RepType 
        '    '' R=Register
        '    '' S=Served
        '    '' M=Missed
        '    '' C=CANCEL
        '    '' I=INCOMPLETE
        '    '' T=Total
        '    Dim StrTimeFrom As String = TimeFrom.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
        '    Dim StrTimeTo As String = TimeTo.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))

        '    Dim sql As String = GetQuery(ServiceID, Segment, RepType)
        '    sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
        '    sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "
        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '    End If
        '    dt.Dispose()
        '    dt = Nothing

        '    Return ret
        'End Function


        'Private Function GetSegmentYearQty(ByVal ServiceDate As DateTime, ByVal ServiceID As Int16, ByVal Segment As String, ByVal RepType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
        '    'RepType 
        '    '' R=Register
        '    '' S=Served
        '    '' M=Missed
        '    '' C=CANCEL
        '    '' I=INCOMPLETE
        '    '' T=Total
        '    Dim ret As Long = 0
        '    Dim sql As String = GetQuery(ServiceID, Segment, RepType)
        '    sql += " and year(cq.service_date) = " & ServiceDate.Year & " and DATEPART(QUARTER,cq.service_date) = " & DatePart(DateInterval.Quarter, ServiceDate)

        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '    End If
        '    dt.Dispose()
        '    dt = Nothing

        '    Return ret
        'End Function

        'Private Function GetSegmentMonthQty(ByVal ServiceDate As DateTime, ByVal ServiceID As Int16, ByVal Segment As String, ByVal RepType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
        '    'RepType 
        '    '' R=Register
        '    '' S=Served
        '    '' M=Missed
        '    '' C=CANCEL
        '    '' I=INCOMPLETE
        '    '' T=Total

        '    Dim ret As Long = 0
        '    Dim sql As String = GetQuery(ServiceID, Segment, RepType)
        '    sql += " and month(cq.service_date) = " & ServiceDate.Month & " and year(cq.service_date)= " & ServiceDate.Year
        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '    End If
        '    dt.Dispose()
        '    dt = Nothing

        '    Return ret
        'End Function

        'Private Function GetSegmentWeekQty(ByVal ServiceDate As DateTime, ByVal ServiceID As Int16, ByVal Segment As String, ByVal RepType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
        '    'RepType 
        '    '' R=Register
        '    '' S=Served
        '    '' M=Missed
        '    '' C=CANCEL
        '    '' I=INCOMPLETE
        '    '' T=Total

        '    Dim ret As Long = 0
        '    Dim ShowWeek As Int16 = DatePart(DateInterval.WeekOfYear, ServiceDate)
        '    Dim ShowYear As Int16 = ServiceDate.Year

        '    Dim sql As String = GetQuery(ServiceID, Segment, RepType)
        '    sql += " and datepart(week, cq.service_date) = " & ShowWeek
        '    sql += " and datepart(year, cq.service_date) = " & ShowYear
        '    Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        '    If dt.Rows.Count > 0 Then
        '        ret = Convert.ToInt64(dt.Rows(0)("qty"))
        '    End If
        '    dt.Dispose()
        '    dt = Nothing

        '    Return ret
        'End Function

        Private Function GetServiceTime(ByVal ServiceDate As DateTime, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim StrTimeFrom As String = TimeFrom.ToString("HH:mm")
            Dim StrTimeTo As String = DateAdd(DateInterval.Minute, -1, TimeTo).ToString("HH:mm")
            Dim StrDate As String = ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = GetQuery()
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by customertype_name,item_id"

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function

        Private Function GetQuery(ByVal ServiceID As Long) As String
            Dim sql As String = ""
            sql += "select customertype_name,isnull(SUM(regis),0) as regis,isnull(SUM(serve),0) as serve,isnull(SUM(misscall),0) as misscall ,isnull(SUM(cancel),0) as cancel,isnull(SUM(notcall),0) as notcall,isnull(SUM(notcon),0) as notcon ,isnull(SUM(notend),0) as notend from VW_Report"
            sql += " where item_id =" & ServiceID
            Return sql
        End Function

        Private Function GetQuery() As String
            Dim sql As String = ""
            sql += "select customertype_name,item_id,isnull(SUM(regis),0) as regis,"
            sql += " isnull(SUM(serve),0) as serve,isnull(SUM(misscall),0) as misscall ,"
            sql += " isnull(SUM(cancel),0) as cancel,isnull(SUM(notcall),0) as notcall,"
            sql += " isnull(SUM(notcon),0) as notcon ,isnull(SUM(notend),0) as notend "
            sql += " from VW_Report"
            sql += " where 1=1 "
            'sql += " where item_id =" & ServiceID
            Return sql
        End Function
    End Class
End Namespace

