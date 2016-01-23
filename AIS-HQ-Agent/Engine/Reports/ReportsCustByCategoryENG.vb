Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities.Constant

Namespace Reports
    Public Class ReportsCustByCategoryENG : Inherits ReportsENG
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

        Public Sub CustByCategoryProcessAllReport()
            ProcReportByTime(_ServiceDate, _ShopID, _lblTime)
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerByCategoryByTime", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_category_time where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                'If IsUpdateSubCat = True Then
                '    UpdateCustomerData(ServiceDate, ServiceDate, ShopID, lblTime)
                'End If

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByTime")
                    If shTrans.Trans IsNot Nothing Then
                        'หา Interval Time
                        Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                        Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", trans.Trans)
                        lnqI = Nothing
                        trans.CommitTransaction()

                        If dtI.Rows.Count > 0 Then
                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByTime")
                            Dim sDt As New DataTable
                            If shTrans.Trans IsNot Nothing Then
                                Dim sSql As String = "select id, item_name, item_name_th "
                                sSql += " from TB_ITEM "
                                sSql += " where active_status='1'"
                                sSql += " order by id"
                                sDt = sLnq.GetListBySql(sSql, shTrans.Trans)
                                shTrans.CommitTransaction()
                            End If

                            For Each drI As DataRow In dtI.Rows
                                Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))
                                Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
                                Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)

                                Dim CurrTime As DateTime = StartTime
                                Do
                                    If CurrTime < EndTime Then
                                        Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                        If TimeTo > EndTime Then
                                            TimeTo = EndTime
                                        End If

                                        'หาข้อมูล Queue
                                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByTime")
                                        Dim dt As New DataTable
                                        dt = GetServiceTime(ServiceDate, CurrTime, TimeTo, shTrans)

                                        If dt.Rows.Count > 0 Then
                                            'Service
                                            If sDt.Rows.Count > 0 Then
                                                For Each sDr As DataRow In sDt.Rows
                                                    Dim ServiceID As String = sDr("id")
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustCategoryTimeCenLinqDB
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.SERVICE_ID = ServiceID
                                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")

                                                    'Category Residential, Sub Cat Thai Citizen
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.THA & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.THA_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.THA_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.THA_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.THA_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.THA_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.THA_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.THA_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category Residential, Sub Cat Foreigner
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.FORE & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.FOR_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.FOR_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.FOR_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.FOR_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.FOR_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.FOR_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.FOR_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category Business, Sub Cat Key Account
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.KEY & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.KEY_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.KEY_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.KEY_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.KEY_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.KEY_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.KEY_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.KEY_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category Business, Sub Cat SME
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.SME & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.SME_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.SME_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.SME_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.SME_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.SME_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.SME_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.SME_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category GovernmentAneNonProfit, Sub Cat Government
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.GOV & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.GOV_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.GOV_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.GOV_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.GOV_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.GOV_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.GOV_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.GOV_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category GovernmentAneNonProfit, Sub Cat State Enterprise
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.STA & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.STA_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.STA_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.STA_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.STA_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.STA_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.STA_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.STA_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category GovernmentAneNonProfit, Sub Cat Embassy
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.EMB & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.EMB_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.EMB_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.EMB_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.EMB_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.EMB_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.EMB_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.EMB_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category GovernmentAneNonProfit, Sub Cat Non Profit
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.NON & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.NON_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.NON_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.NON_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.NON_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.NON_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.NON_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.NON_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category Exclusive, Sub Cat Royal
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.ROY & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.ROY_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.ROY_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.ROY_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.ROY_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.ROY_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.ROY_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.ROY_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category Exclusive, Sub Cat TOT
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.TOT & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.TOT_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.TOT_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.TOT_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.TOT_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.TOT_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.TOT_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.TOT_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category In House, Sub Cat Pre-paid
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.PRE & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.PRE_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.PRE_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.PRE_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.PRE_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.PRE_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.PRE_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.PRE_NOT_END = dt.DefaultView(0)("notend")
                                                    End If

                                                    'Category In House, Sub Cat Pre-paid
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.AIS & "'"
                                                    If dt.DefaultView.Count > 0 Then
                                                        lnq.AIS_REGIS = dt.DefaultView(0)("regis")
                                                        lnq.AIS_SERVE = dt.DefaultView(0)("serve")
                                                        lnq.AIS_MISS_CALL = dt.DefaultView(0)("misscall")
                                                        lnq.AIS_CANCEL = dt.DefaultView(0)("cancel")
                                                        lnq.AIS_NOT_CALL = dt.DefaultView(0)("notcall")
                                                        lnq.AIS_NOT_CON = dt.DefaultView(0)("notcon")
                                                        lnq.AIS_NOT_END = dt.DefaultView(0)("notend")
                                                    End If


                                                    'No Category and No Sub Category
                                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and (isnull(category,'')= '' or isnull(corporate_type,'')='')"
                                                    If dt.DefaultView.Count > 0 Then
                                                        For Each dr As DataRowView In dt.DefaultView
                                                            lnq.NO_REGIS += dr("regis")
                                                            lnq.NO_SERVE += dr("serve")
                                                            lnq.NO_MISS_CALL += dr("misscall")
                                                            lnq.NO_CANCEL += dr("cancel")
                                                            lnq.NO_NOT_CALL += dr("notcall")
                                                            lnq.NO_NOT_CON += dr("notcon")
                                                            lnq.NO_NOT_END += dr("notend")
                                                        Next
                                                    End If

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcReportByTime", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsCustByCategoryENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByCategoryENG")
                                                    End If
                                                    lnq = Nothing

                                                    Try
                                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                        Application.DoEvents()
                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            End If
                                        Else
                                            If sDt.Rows.Count > 0 Then
                                                For Each sdr As DataRow In sDt.Rows
                                                    Dim ServiceID As String = sdr("id")
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustCategoryTimeCenLinqDB
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.SERVICE_ID = ServiceID
                                                    lnq.SERVICE_NAME_EN = sdr("item_name")
                                                    lnq.SERVICE_NAME_TH = sdr("item_name_th")
                                                    lnq.SERVICE_DATE = ServiceDate
                                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProcReportByTime", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsCustByCategoryENG.ProcReportByTime", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByCategoryENG")
                                                    End If
                                                    lnq = Nothing

                                                    Try
                                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                        Application.DoEvents()
                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            End If
                                        End If
                                        dt.Dispose()
                                    End If
                                    CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                Loop While CurrTime <= EndTime
                            Next
                            dtI.Dispose()
                            sDt.Dispose()
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
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_category_day where convert(varchar(8), service_date,112) = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()
                'If IsUpdateSubCat = True Then
                '    UpdateCustomerData(ServiceDate, ServiceDate, ShopID, lblTime)
                'End If

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Service ของ Shop
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)

                        'หาข้อมูล Queue
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByDate")
                        Dim dt As New DataTable
                        dt = GetCategoryByPeriodDate(ServiceDate, ServiceDate, shTrans)
                        shTrans.CommitTransaction()

                        If dt.Rows.Count > 0 Then


                            'Service
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    Dim ServiceID As String = sDr("id")
                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustCategoryDayCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.SERVICE_DATE = ServiceDate
                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))

                                    'Category Residential, Sub Cat Thai Citizen
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.THA & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.THA_REGIS = dt.DefaultView(0)("regis")
                                        lnq.THA_SERVE = dt.DefaultView(0)("serve")
                                        lnq.THA_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.THA_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.THA_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.THA_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.THA_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Residential, Sub Cat Foreigner
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.FORE & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.FOR_REGIS = dt.DefaultView(0)("regis")
                                        lnq.FOR_SERVE = dt.DefaultView(0)("serve")
                                        lnq.FOR_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.FOR_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.FOR_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.FOR_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.FOR_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Business, Sub Cat Key Account
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.KEY & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.KEY_REGIS = dt.DefaultView(0)("regis")
                                        lnq.KEY_SERVE = dt.DefaultView(0)("serve")
                                        lnq.KEY_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.KEY_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.KEY_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.KEY_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.KEY_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Business, Sub Cat SME
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.SME & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.SME_REGIS = dt.DefaultView(0)("regis")
                                        lnq.SME_SERVE = dt.DefaultView(0)("serve")
                                        lnq.SME_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.SME_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.SME_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.SME_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.SME_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat Government
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.GOV & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.GOV_REGIS = dt.DefaultView(0)("regis")
                                        lnq.GOV_SERVE = dt.DefaultView(0)("serve")
                                        lnq.GOV_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.GOV_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.GOV_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.GOV_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.GOV_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat State Enterprise
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.STA & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.STA_REGIS = dt.DefaultView(0)("regis")
                                        lnq.STA_SERVE = dt.DefaultView(0)("serve")
                                        lnq.STA_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.STA_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.STA_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.STA_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.STA_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat Embassy
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.EMB & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.EMB_REGIS = dt.DefaultView(0)("regis")
                                        lnq.EMB_SERVE = dt.DefaultView(0)("serve")
                                        lnq.EMB_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.EMB_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.EMB_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.EMB_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.EMB_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat Non Profit
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.NON & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.NON_REGIS = dt.DefaultView(0)("regis")
                                        lnq.NON_SERVE = dt.DefaultView(0)("serve")
                                        lnq.NON_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.NON_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.NON_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.NON_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.NON_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Exclusive, Sub Cat Royal
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.ROY & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.ROY_REGIS = dt.DefaultView(0)("regis")
                                        lnq.ROY_SERVE = dt.DefaultView(0)("serve")
                                        lnq.ROY_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.ROY_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.ROY_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.ROY_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.ROY_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Exclusive, Sub Cat TOT
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.TOT & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.TOT_REGIS = dt.DefaultView(0)("regis")
                                        lnq.TOT_SERVE = dt.DefaultView(0)("serve")
                                        lnq.TOT_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.TOT_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.TOT_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.TOT_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.TOT_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category In House, Sub Cat Pre-paid
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.PRE & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.PRE_REGIS = dt.DefaultView(0)("regis")
                                        lnq.PRE_SERVE = dt.DefaultView(0)("serve")
                                        lnq.PRE_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.PRE_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.PRE_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.PRE_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.PRE_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category In House, Sub Cat Pre-paid
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.AIS & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.AIS_REGIS = dt.DefaultView(0)("regis")
                                        lnq.AIS_SERVE = dt.DefaultView(0)("serve")
                                        lnq.AIS_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.AIS_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.AIS_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.AIS_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.AIS_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'No Category and No Sub Category
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and (isnull(category,'')= '' or isnull(corporate_type,'')='')"
                                    If dt.DefaultView.Count > 0 Then
                                        For Each dr As DataRowView In dt.DefaultView
                                            lnq.NO_REGIS += dr("regis")
                                            lnq.NO_SERVE += dr("serve")
                                            lnq.NO_MISS_CALL += dr("misscall")
                                            lnq.NO_CANCEL += dr("cancel")
                                            lnq.NO_NOT_CALL += dr("notcall")
                                            lnq.NO_NOT_CON += dr("notcon")
                                            lnq.NO_NOT_END += dr("notend")
                                        Next
                                    End If

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcReportByDay", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsCustByCategoryENG.ProcReportByDay", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByCategoryENG")
                                    End If
                                    lnq = Nothing

                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                Next
                            End If
                        End If
                        dt.Dispose()
                        sLnq = Nothing
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
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerByCategoryByWeek", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim c As New Globalization.CultureInfo("en-US")
                Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_category_week where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
                Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)

                'If IsUpdateSubCat = True Then
                '    UpdateCustomerData(FirstDay, LastDay, ShopID, lblTime)
                'End If

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Service ของ Shop
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByWeek")
                    If shTrans.Trans IsNot Nothing Then
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)

                        'หาข้อมูล Queue
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByWeek")
                        Dim dt As New DataTable
                        dt = GetCategoryByPeriodDate(FirstDay, LastDay, shTrans)
                        shTrans.CommitTransaction()

                        If dt.Rows.Count = 0 Then
                            Exit Sub
                        End If

                        'Service
                        If sDt.Rows.Count > 0 Then
                            For Each sDr As DataRow In sDt.Rows
                                Dim ServiceID As String = sDr("id")
                                Dim lnq As New CenLinqDB.TABLE.TbRepCustCategoryWeekCenLinqDB
                                lnq.SHOP_ID = shLnq.ID
                                lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                lnq.SERVICE_ID = ServiceID
                                lnq.SERVICE_NAME_EN = sDr("item_name")
                                lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                lnq.WEEK_OF_YEAR = WeekNo
                                lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                                lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "-" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                                'Category Residential, Sub Cat Thai Citizen
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.THA & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.THA_REGIS = dt.DefaultView(0)("regis")
                                    lnq.THA_SERVE = dt.DefaultView(0)("serve")
                                    lnq.THA_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.THA_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.THA_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.THA_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.THA_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category Residential, Sub Cat Foreigner
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.FORE & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.FOR_REGIS = dt.DefaultView(0)("regis")
                                    lnq.FOR_SERVE = dt.DefaultView(0)("serve")
                                    lnq.FOR_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.FOR_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.FOR_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.FOR_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.FOR_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category Business, Sub Cat Key Account
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.KEY & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.KEY_REGIS = dt.DefaultView(0)("regis")
                                    lnq.KEY_SERVE = dt.DefaultView(0)("serve")
                                    lnq.KEY_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.KEY_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.KEY_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.KEY_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.KEY_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category Business, Sub Cat SME
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.SME & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.SME_REGIS = dt.DefaultView(0)("regis")
                                    lnq.SME_SERVE = dt.DefaultView(0)("serve")
                                    lnq.SME_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.SME_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.SME_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.SME_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.SME_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category GovernmentAneNonProfit, Sub Cat Government
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.GOV & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.GOV_REGIS = dt.DefaultView(0)("regis")
                                    lnq.GOV_SERVE = dt.DefaultView(0)("serve")
                                    lnq.GOV_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.GOV_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.GOV_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.GOV_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.GOV_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category GovernmentAneNonProfit, Sub Cat State Enterprise
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.STA & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.STA_REGIS = dt.DefaultView(0)("regis")
                                    lnq.STA_SERVE = dt.DefaultView(0)("serve")
                                    lnq.STA_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.STA_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.STA_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.STA_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.STA_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category GovernmentAneNonProfit, Sub Cat Embassy
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.EMB & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.EMB_REGIS = dt.DefaultView(0)("regis")
                                    lnq.EMB_SERVE = dt.DefaultView(0)("serve")
                                    lnq.EMB_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.EMB_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.EMB_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.EMB_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.EMB_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category GovernmentAneNonProfit, Sub Cat Non Profit
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.NON & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.NON_REGIS = dt.DefaultView(0)("regis")
                                    lnq.NON_SERVE = dt.DefaultView(0)("serve")
                                    lnq.NON_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.NON_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.NON_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.NON_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.NON_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category Exclusive, Sub Cat Royal
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.ROY & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.ROY_REGIS = dt.DefaultView(0)("regis")
                                    lnq.ROY_SERVE = dt.DefaultView(0)("serve")
                                    lnq.ROY_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.ROY_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.ROY_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.ROY_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.ROY_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category Exclusive, Sub Cat TOT
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.TOT & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.TOT_REGIS = dt.DefaultView(0)("regis")
                                    lnq.TOT_SERVE = dt.DefaultView(0)("serve")
                                    lnq.TOT_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.TOT_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.TOT_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.TOT_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.TOT_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category In House, Sub Cat Pre-paid
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.PRE & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.PRE_REGIS = dt.DefaultView(0)("regis")
                                    lnq.PRE_SERVE = dt.DefaultView(0)("serve")
                                    lnq.PRE_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.PRE_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.PRE_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.PRE_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.PRE_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'Category In House, Sub Cat Pre-paid
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.AIS & "'"
                                If dt.DefaultView.Count > 0 Then
                                    lnq.AIS_REGIS = dt.DefaultView(0)("regis")
                                    lnq.AIS_SERVE = dt.DefaultView(0)("serve")
                                    lnq.AIS_MISS_CALL = dt.DefaultView(0)("misscall")
                                    lnq.AIS_CANCEL = dt.DefaultView(0)("cancel")
                                    lnq.AIS_NOT_CALL = dt.DefaultView(0)("notcall")
                                    lnq.AIS_NOT_CON = dt.DefaultView(0)("notcon")
                                    lnq.AIS_NOT_END = dt.DefaultView(0)("notend")
                                End If

                                'No Category and No Sub Category
                                dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and (isnull(category,'')= '' or isnull(corporate_type,'')='')"
                                If dt.DefaultView.Count > 0 Then
                                    For Each dr As DataRowView In dt.DefaultView
                                        lnq.NO_REGIS += dr("regis")
                                        lnq.NO_SERVE += dr("serve")
                                        lnq.NO_MISS_CALL += dr("misscall")
                                        lnq.NO_CANCEL += dr("cancel")
                                        lnq.NO_NOT_CALL += dr("notcall")
                                        lnq.NO_NOT_CON += dr("notcon")
                                        lnq.NO_NOT_END += dr("notend")
                                    Next
                                End If

                                trans = New CenLinqDB.Common.Utilities.TransactionDB
                                Dim ret As Boolean = False
                                ret = lnq.InsertData("ProcReportByWeek", trans.Trans)
                                If ret = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ReportsCustByCategoryENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByCategoryENG")
                                End If
                                lnq = Nothing

                                Try
                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                    Application.DoEvents()
                                Catch ex As Exception

                                End Try
                            Next
                        End If
                        dt.Dispose()
                        sDt.Dispose()
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
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : CustomerByCategoryByMonth", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from tb_rep_cust_category_month where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim FirstDay As New Date(ServiceDate.Year, ServiceDate.Month, 1)
                Dim LastDay As New Date(ServiceDate.Year, ServiceDate.Month, DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month))
                'If IsUpdateSubCat = True Then
                '    UpdateCustomerData(FirstDay, LastDay, ShopID, lblTime)
                'End If

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Service ของ Shop
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByMonth")
                    If shTrans.Trans IsNot Nothing Then
                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)

                        'หาข้อมูล Queue
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsCustByCategoryENG.ProcReportByMonth")
                        Dim dt As New DataTable
                        dt = GetCategoryByPeriodDate(FirstDay, LastDay, shTrans)
                        shTrans.CommitTransaction()

                        If dt.Rows.Count > 0 Then


                            'Service
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    Dim ServiceID As String = sDr("id")
                                    Dim lnq As New CenLinqDB.TABLE.TbRepCustCategoryMonthCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_ID = ServiceID
                                    lnq.SERVICE_NAME_EN = sDr("item_name")
                                    lnq.SERVICE_NAME_TH = sDr("item_name_th")
                                    lnq.MONTH_NO = ServiceDate.Month
                                    lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                                    lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))

                                    'Category Residential, Sub Cat Thai Citizen
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.THA & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.THA_REGIS = dt.DefaultView(0)("regis")
                                        lnq.THA_SERVE = dt.DefaultView(0)("serve")
                                        lnq.THA_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.THA_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.THA_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.THA_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.THA_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Residential, Sub Cat Foreigner
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Residential.Name & "' and corporate_type='" & TbCustomer.Category.Residential.SubCategory.FORE & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.FOR_REGIS = dt.DefaultView(0)("regis")
                                        lnq.FOR_SERVE = dt.DefaultView(0)("serve")
                                        lnq.FOR_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.FOR_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.FOR_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.FOR_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.FOR_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Business, Sub Cat Key Account
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.KEY & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.KEY_REGIS = dt.DefaultView(0)("regis")
                                        lnq.KEY_SERVE = dt.DefaultView(0)("serve")
                                        lnq.KEY_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.KEY_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.KEY_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.KEY_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.KEY_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Business, Sub Cat SME
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Business.Name & "' and corporate_type='" & TbCustomer.Category.Business.SubCategory.SME & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.SME_REGIS = dt.DefaultView(0)("regis")
                                        lnq.SME_SERVE = dt.DefaultView(0)("serve")
                                        lnq.SME_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.SME_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.SME_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.SME_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.SME_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat Government
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.GOV & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.GOV_REGIS = dt.DefaultView(0)("regis")
                                        lnq.GOV_SERVE = dt.DefaultView(0)("serve")
                                        lnq.GOV_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.GOV_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.GOV_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.GOV_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.GOV_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat State Enterprise
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.STA & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.STA_REGIS = dt.DefaultView(0)("regis")
                                        lnq.STA_SERVE = dt.DefaultView(0)("serve")
                                        lnq.STA_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.STA_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.STA_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.STA_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.STA_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat Embassy
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.EMB & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.EMB_REGIS = dt.DefaultView(0)("regis")
                                        lnq.EMB_SERVE = dt.DefaultView(0)("serve")
                                        lnq.EMB_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.EMB_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.EMB_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.EMB_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.EMB_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category GovernmentAneNonProfit, Sub Cat Non Profit
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.GovernmentAneNonProfit.Name & "' and corporate_type='" & TbCustomer.Category.GovernmentAneNonProfit.SubCategory.NON & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.NON_REGIS = dt.DefaultView(0)("regis")
                                        lnq.NON_SERVE = dt.DefaultView(0)("serve")
                                        lnq.NON_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.NON_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.NON_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.NON_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.NON_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Exclusive, Sub Cat Royal
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.ROY & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.ROY_REGIS = dt.DefaultView(0)("regis")
                                        lnq.ROY_SERVE = dt.DefaultView(0)("serve")
                                        lnq.ROY_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.ROY_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.ROY_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.ROY_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.ROY_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category Exclusive, Sub Cat TOT
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.Exclusive.Name & "' and corporate_type='" & TbCustomer.Category.Exclusive.SubCategory.TOT & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.TOT_REGIS = dt.DefaultView(0)("regis")
                                        lnq.TOT_SERVE = dt.DefaultView(0)("serve")
                                        lnq.TOT_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.TOT_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.TOT_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.TOT_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.TOT_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category In House, Sub Cat Pre-paid
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.PRE & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.PRE_REGIS = dt.DefaultView(0)("regis")
                                        lnq.PRE_SERVE = dt.DefaultView(0)("serve")
                                        lnq.PRE_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.PRE_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.PRE_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.PRE_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.PRE_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'Category In House, Sub Cat Pre-paid
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and category='" & TbCustomer.Category.InHouse.Name & "' and corporate_type='" & TbCustomer.Category.InHouse.SubCategory.AIS & "'"
                                    If dt.DefaultView.Count > 0 Then
                                        lnq.AIS_REGIS = dt.DefaultView(0)("regis")
                                        lnq.AIS_SERVE = dt.DefaultView(0)("serve")
                                        lnq.AIS_MISS_CALL = dt.DefaultView(0)("misscall")
                                        lnq.AIS_CANCEL = dt.DefaultView(0)("cancel")
                                        lnq.AIS_NOT_CALL = dt.DefaultView(0)("notcall")
                                        lnq.AIS_NOT_CON = dt.DefaultView(0)("notcon")
                                        lnq.AIS_NOT_END = dt.DefaultView(0)("notend")
                                    End If

                                    'No Category and No Sub Category
                                    dt.DefaultView.RowFilter = "item_id='" & ServiceID & "' and (isnull(category,'')= '' or isnull(corporate_type,'')='')"
                                    If dt.DefaultView.Count > 0 Then
                                        For Each dr As DataRowView In dt.DefaultView
                                            lnq.NO_REGIS += dr("regis")
                                            lnq.NO_SERVE += dr("serve")
                                            lnq.NO_MISS_CALL += dr("misscall")
                                            lnq.NO_CANCEL += dr("cancel")
                                            lnq.NO_NOT_CALL += dr("notcall")
                                            lnq.NO_NOT_CON += dr("notcon")
                                            lnq.NO_NOT_END += dr("notend")
                                        Next
                                    End If

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcReportByMonth", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsCustByCategoryENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsCustByCategoryENG")
                                    End If
                                    lnq = Nothing

                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                Next
                            End If
                        End If
                        dt.Dispose()
                        sDt.Dispose()
                        sLnq = Nothing
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetCategoryByPeriodDate(ByVal DateFrom As DateTime, ByVal DateTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = GetQuery()
            sql += " where convert(varchar(8), service_date,112) between '" & DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by q.item_id, isnull(c.category,''),isnull(c.corporate_type,'')"
            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
        

        Private Function GetServiceTime(ByVal ServiceDate As DateTime, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim StrTimeFrom As String = TimeFrom.ToString("HH:mm")
            Dim StrTimeTo As String = TimeTo.ToString("HH:mm")
            Dim sql As String = GetQuery()
            sql += " where YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & " "
            sql += " and convert(varchar(5), [time], 114) >= '" & StrTimeFrom & "' "
            sql += " and convert(varchar(5), [time], 114) < '" & StrTimeTo & "' "
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' "
            sql += " group by q.item_id, isnull(c.category,''),isnull(c.corporate_type,'')"

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function

        Private Function GetQuery() As String
            Dim sql As String = ""
            sql += "select q.item_id, isnull(c.category,'') category,isnull(c.corporate_type,'') corporate_type, isnull(SUM(q.regis),0) as regis,isnull(SUM(q.serve),0) as serve,isnull(SUM(q.misscall),0) as misscall ,isnull(SUM(q.cancel),0) as cancel,isnull(SUM(q.notcall),0) as notcall,isnull(SUM(q.notcon),0) as notcon ,isnull(SUM(q.notend),0) as notend "
            sql += " from VW_Report q"
            sql += " inner join TB_CUSTOMER c on c.mobile_no=q.customer_id"

            Return sql
        End Function
    End Class
End Namespace

