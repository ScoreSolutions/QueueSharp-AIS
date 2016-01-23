Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities

Namespace Reports
    Public Class RepWaitingTimeHandlingTimeByShopENG
        'Public Function GetReportByTime(ByVal InputPara As CustByWaitingTimePara) As DataTable
        '    Dim ret As New DataTable
        '    'Create DataTable
        '    ret.Columns.Add("Date")
        '    ret.Columns.Add("Shop Name")
        '    ret.Columns.Add("Time")

        '    'วนลูปตาม Shop ก่อน
        '    Dim ShopID() As String = Split(InputPara.ShopID, ",")
        '    Dim cenTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '    If cenTrans.Trans IsNot Nothing Then
        '        For Each shID As String In ShopID
        '            If shID.Trim <> "" Then
        '                Dim ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(Convert.ToInt64(shID))
        '                If ShopLnq.ID <> 0 Then
        '                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '                    shTrans = FunctionEng.GetShTransction(shTrans, cenTrans, ShopLnq)
        '                    If shTrans.Trans IsNot Nothing Then
        '                        'หา Service ของ Shop
        '                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '                        Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
        '                        If sDt.Rows.Count > 0 Then
        '                            For Each sDr As DataRow In sDt.Rows
        '                                If ret.Columns.Contains(sDr("item_name") & " AWT") = False Then
        '                                    ret.Columns.Add(sDr("item_name") & " AWT")
        '                                End If
        '                                If ret.Columns.Contains(sDr("item_name") & " AHT") = False Then
        '                                    ret.Columns.Add(sDr("item_name") & " AHT")
        '                                End If
        '                                If ret.Columns.Contains(sDr("item_name") & " Total Regis") = False Then
        '                                    ret.Columns.Add(sDr("item_name") & " Total Regis", GetType(Long))
        '                                End If
        '                                If ret.Columns.Contains(sDr("item_name") & " Total Served") = False Then
        '                                    ret.Columns.Add(sDr("item_name") & " Total Served", GetType(Long))
        '                                End If
        '                            Next
        '                        End If
        '                        'จากนั้นก็ลูปตามวันที่
        '                        Dim CurrDate As Date = InputPara.DateFrom
        '                        Do
        '                            Dim DateDisplay As String = CurrDate.ToString("dd/MM/yyyy", New System.Globalization.CultureInfo("en-US"))

        '                            'หาหน่วยของเวลาด้วยนะ
        '                            Dim sTime() As String = Split(InputPara.TimePeroidFrom, ":")
        '                            Dim sHour As Integer = sTime(0)
        '                            Dim sMin As Integer = sTime(1)
        '                            Dim CurrTime As DateTime = New DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, sHour, sMin, 0)

        '                            Dim eTime() As String = Split(InputPara.TimePeroidTo, ":")
        '                            Dim eHour As Integer = eTime(0)
        '                            Dim eMin As Integer = eTime(1)
        '                            Dim EndTime As DateTime = New DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, eHour, eMin, 0)
        '                            Do
        '                                If shTrans.Trans IsNot Nothing Then
        '                                    Dim dr As DataRow = ret.NewRow
        '                                    Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
        '                                    If TimeTo > EndTime Then
        '                                        TimeTo = EndTime
        '                                    End If

        '                                    dr("Date") = DateDisplay
        '                                    dr("Shop Name") = ShopLnq.SHOP_NAME_TH
        '                                    dr("Time") = CurrTime.ToString("HH:mm") & " - " & TimeTo.ToString("HH:mm")

        '                                    For Each sDr As DataRow In sDt.Rows
        '                                        dr(sDr("item_name") & " AWT") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "W", shTrans)
        '                                        dr(sDr("item_name") & " AHT") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "H", shTrans)
        '                                        dr(sDr("item_name") & " Total Regis") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "R", shTrans)
        '                                        dr(sDr("item_name") & " Total Served") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "S", shTrans)
        '                                    Next
        '                                    ret.Rows.Add(dr)
        '                                End If
        '                                CurrTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
        '                            Loop While CurrTime <= EndTime

        '                            CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
        '                        Loop While CurrDate <= InputPara.DateTo
        '                        shTrans.CommitTransaction()
        '                    Else
        '                        FunctionEng.SaveErrorLog("RepCustByWaitingTimeENG.GetReportByTime", shTrans.ErrorMessage & " Shop ID = " & ShopLnq.ID)
        '                    End If
        '                    shTrans.CommitTransaction()
        '                End If
        '            End If
        '        Next
        '        ret.TableName = "RepCustByWaitingTimeByTime"
        '        'ReportsENG.SetDataToTable(ret, InputPara.ProcessUser, cenTrans)
        '    End If
        '    Return ret
        'End Function
        Private Function GetServiceTime(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal ServiceID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""
            'TimeType 
            '' W= Avg Waiting Time
            '' H= Avg Handling Time
            '' R= Total Regis
            '' S= Total Served
            Dim StrTimeFrom As String = TimeFrom.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim StrTimeTo As String = TimeTo.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim sql As String = ""
            sql += "select count(cq.id) qty, AVG(DATEDIFF(SECOND, assign_time,start_time)) sumAWT, "
            sql += " AVG(DATEDIFF(SECOND, start_time,end_time)) sumAHT"
            sql += " from tb_counter_queue_history cq "
            sql += " where cq.item_id =" & ServiceID

            sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "

            If TimeType = "S" Then
                sql += " and status = 3 "
            End If

            'If TimeType = "W" Or TimeType = "R" Then
            '    sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "
            'ElseIf TimeType = "H" Or TimeType = "S" Then
            '    sql += " and convert(varchar(16), cq.start_time, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.start_time, 120) < '" & StrTimeTo & "' "
            'End If

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                If TimeType = "R" Or TimeType = "S" Then
                    ret = Convert.ToInt64(dt.Rows(0)("qty"))
                Else
                    Dim tSec As Long = 0
                    If TimeType = "W" Then
                        If Convert.IsDBNull(dt.Rows(0)("sumAWT")) = False Then
                            tSec = Convert.ToInt64(dt.Rows(0)("sumAWT"))
                        End If
                    ElseIf TimeType = "H" Then
                        If Convert.IsDBNull(dt.Rows(0)("sumAHT")) = False Then
                            tSec = Convert.ToInt64(dt.Rows(0)("sumAHT"))
                        End If
                    End If
                    Dim cMin As Integer = Math.Floor(tSec / 60)
                    Dim cSec As Integer = tSec Mod 60

                    ret = cMin.ToString & ":" & cSec.ToString.PadLeft(2, "0")
                End If
            End If

            Return ret
        End Function


        Public Function GetReportByTime(ByVal InputPara As CustByWaitingTimePara) As DataTable
            Dim ret As New DataTable
            'Create DataTable
            ret.Columns.Add("Date")
            ret.Columns.Add("Shop Name")
            ret.Columns.Add("Time")
            ret.Columns.Add("Total", GetType(Integer))

            Dim cenTrans As New CenLinqDB.Common.Utilities.TransactionDB
            If cenTrans.Trans IsNot Nothing Then
                'วนลูปตาม Shop ก่อน
                Dim ShopID() As String = Split(InputPara.ShopID, ",")
                For Each shID As String In ShopID
                    If shID.Trim <> "" Then
                        Dim ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(Convert.ToInt64(shID))
                        If ShopLnq.ID <> 0 Then
                            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                            shTrans = FunctionEng.GetShTransction(shTrans, cenTrans, ShopLnq)
                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                            If sDt.Rows.Count > 0 Then
                                For Each sDr As DataRow In sDt.Rows
                                    If ret.Columns.Contains("[" & sDr("item_name") & " GSM Registration]") = False Then
                                        ret.Columns.Add("[" & sDr("item_name") & " GSM Registration]", GetType(Integer))
                                    End If
                                    If ret.Columns.Contains("[" & sDr("item_name") & " GSM Served]") = False Then
                                        ret.Columns.Add("[" & sDr("item_name") & " GSM Served]", GetType(Integer))
                                    End If
                                    If ret.Columns.Contains("[" & sDr("item_name") & " GSM Missed Call]") = False Then
                                        ret.Columns.Add("[" & sDr("item_name") & " GSM Missed Call]", GetType(Integer))
                                    End If
                                    If ret.Columns.Contains("[" & sDr("item_name") & " 1-2-Call Registration]") = False Then
                                        ret.Columns.Add("[" & sDr("item_name") & " 1-2-Call Registration]", GetType(Integer))
                                    End If
                                    If ret.Columns.Contains("[" & sDr("item_name") & " 1-2-Call Served]") = False Then
                                        ret.Columns.Add("[" & sDr("item_name") & " 1-2-Call Served]", GetType(Integer))
                                    End If
                                    If ret.Columns.Contains("[" & sDr("item_name") & " 1-2-Call Missed Call]") = False Then
                                        ret.Columns.Add("[" & sDr("item_name") & " 1-2-Call Missed Call]", GetType(Integer))
                                    End If
                                Next
                            End If

                            'จากนั้นก็ลูปตามวันที่
                            Dim CurrDate As Date = InputPara.DateFrom
                            Do
                                Dim DateDisplay As String = CurrDate.ToString("dd/MM/yyyy", New System.Globalization.CultureInfo("en-US"))

                                'หาหน่วยของเวลาด้วยนะ
                                Dim sTime() As String = Split(InputPara.TimePeroidFrom, ":")
                                Dim sHour As Integer = sTime(0)
                                Dim sMin As Integer = sTime(1)
                                Dim CurrTime As DateTime = New DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, sHour, sMin, 0)

                                Dim eTime() As String = Split(InputPara.TimePeroidTo, ":")
                                Dim eHour As Integer = eTime(0)
                                Dim eMin As Integer = eTime(1)
                                Dim EndTime As DateTime = New DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, eHour, eMin, 0)
                                Do
                                    If CurrTime < EndTime Then
                                        shTrans = FunctionEng.GetShTransction(shTrans, cenTrans, ShopLnq)
                                        If shTrans.Trans IsNot Nothing Then
                                            Dim dr As DataRow = ret.NewRow
                                            Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
                                            If TimeTo > EndTime Then
                                                TimeTo = EndTime
                                            End If

                                            dr("Date") = DateDisplay
                                            dr("Shop Name") = ShopLnq.SHOP_NAME_EN
                                            dr("Time") = CurrTime.ToString("HH:mm") & " - " & TimeTo.ToString("HH:mm")
                                            Dim Total As Integer = 0
                                            For Each sDr As DataRow In sDt.Rows
                                                'dr("[" & sDr("item_name") & " GSM Registration]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), "'GSM','POST'", "R", shTrans)
                                                'dr("[" & sDr("item_name") & " GSM Served]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), "'GSM','POST'", "R", shTrans)
                                                'dr("[" & sDr("item_name") & " GSM Missed Call]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), "'GSM','POST'", "R", shTrans)
                                                'dr("[" & sDr("item_name") & " 1-2-Call Registration]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), "'PRE','1 2 Call'", "S", shTrans)
                                                'dr("[" & sDr("item_name") & " 1-2-Call Served]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), "12Call", "S", shTrans)
                                                'dr("[" & sDr("item_name") & " 1-2-Call Missed Call]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), "12Call", "S", shTrans)

                                                'Total += dr("[" & sDr("item_name") & " GSM Registration]") + dr("[" & sDr("item_name") & " GSM Served]") + dr("[" & sDr("item_name") & " GSM Missed Call]") + dr("[" & sDr("item_name") & " 1-2-Call Registration]") + dr("[" & sDr("item_name") & " 1-2-Call Served]") + dr("[" & sDr("item_name") & " 1-2-Call Missed Call]")
                                                dr("[" & sDr("item_name") & " GSM Registration]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), " (c.network_type like '%GSM%' or c.network_type like '%POST%') ", "R", shTrans)
                                                dr("[" & sDr("item_name") & " GSM Served]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), " (c.network_type like '%GSM%' or c.network_type like '%POST%') ", "R", shTrans)
                                                dr("[" & sDr("item_name") & " GSM Missed Call]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), " (c.network_type like '%GSM%' or c.network_type like '%POST%') ", "R", shTrans)

                                                dr("[" & sDr("item_name") & " 1-2-Call Registration]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), " (c.network_type like '%OTC%' or c.network_type like '%PRE%') ", "S", shTrans)
                                                dr("[" & sDr("item_name") & " 1-2-Call Served]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), " (c.network_type like '%OTC%' or c.network_type like '%PRE%') ", "S", shTrans)
                                                dr("[" & sDr("item_name") & " 1-2-Call Missed Call]") = GetServiceNetworkQty(CurrTime, TimeTo, sDr("id"), " (c.network_type like '%OTC%' or c.network_type like '%PRE%') ", "S", shTrans)

                                            Next
                                            dr("Total") = GetServiceNetworkQty(CurrTime, TimeTo, 0, "", "T", shTrans)
                                            ret.Rows.Add(dr)
                                        End If
                                    End If
                                    CurrTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
                                Loop While CurrTime <= EndTime

                                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                            Loop While CurrDate <= InputPara.DateTo
                            shTrans.CommitTransaction()
                        End If
                    End If
                Next
            End If
            ret.TableName = "RepCustByNetworkTypeByTime"
            Return ret
        End Function
        Private Function GetServiceNetworkQty(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal ServiceID As Long, ByVal NetworkType As String, ByVal RepType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0
            'RepType 
            '' R=Register
            '' S=Served
            '' M=Missed
            'Dim NwType As String = ""
            'If NetworkType.ToUpper.IndexOf("POST") > -1 Then
            '    NwType=
            'ElseIf NetworkType.ToUpper.IndexOf("GSM") > -1 Then
            'ElseIf NetworkType.ToUpper.IndexOf("PRE") > -1 Then
            'ElseIf NetworkType.ToUpper.IndexOf("1 2 Call") > -1 Then


            'End If

            Dim StrTimeFrom As String = TimeFrom.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim StrTimeTo As String = TimeTo.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim sql As String = ""

            'sql += "select count(cq.id) qty "
            'sql += " from tb_counter_queue_history cq "
            'sql += " left join tb_customertype ct on ct.id=cq.customertype_id "
            'sql += " left join tb_customer c on c.mobile_no=cq.customer_id"
            'sql += " where cq.item_id =" & ServiceID & " and c.network_type in (" & NetworkType & ") "

            'If RepType = "R" Then
            '    sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "
            'ElseIf RepType = "S" Then
            '    sql += " and convert(varchar(16), cq.start_time, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.start_time, 120) < '" & StrTimeTo & "' "
            'ElseIf RepType = "M" Then
            '    sql += " and convert(varchar(16), cq.end_time, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.end_time, 120) < '" & StrTimeTo & "' "
            '    sql += " and cq.status = '" & Constant.TbCounterQueue.Status.MissedCall & "' "
            'End If

            If RepType = "T" Then
                sql += "select count(distinct queue_no) qty "
                sql += " from tb_counter_queue_history cq "
                sql += " left join tb_customertype ct on ct.id=cq.customertype_id "
                sql += " left join tb_customer c on c.mobile_no=cq.customer_id"
                sql += " where 1 = 1 "
            Else
                sql += "select count(cq.id) qty "
                sql += " from tb_counter_queue_history cq "
                sql += " left join tb_customertype ct on ct.id=cq.customertype_id "
                sql += " left join tb_customer c on c.mobile_no=cq.customer_id"
                sql += " where cq.item_id =" & ServiceID & " and " & NetworkType
            End If

            'sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            'sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "

            sql += " and convert(int,convert(varchar(8),service_date,112)) = " & FixDate(StrTimeFrom) & " and convert(varchar(5),service_date,114) >= '" & TimeFrom.Hour.ToString.PadLeft(2, "0") & ":" & TimeFrom.Minute.ToString.PadLeft(2, "0") & "' and convert(varchar(5),service_date,114) < '" & TimeTo.Hour.ToString.PadLeft(2, "0") & ":" & TimeTo.Minute.ToString.PadLeft(2, "0") & "'"

            If RepType = "S" Then
                sql += " and status = 3 "
            ElseIf RepType = "M" Then
                sql += " and status = 8 "
            End If

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                ret = Convert.ToInt64(dt.Rows(0)("qty"))
            End If

            Return ret
        End Function
        Public Function FixDate(ByVal StringDate As String) As String 'Convert วันที่ให้เป็น YYYYMMDD
            Dim d As String = ""
            Dim m As String = ""
            Dim y As String = ""
            If IsDate(StringDate) Then
                Dim dmy As Date = CDate(StringDate)
                d = dmy.Day
                m = dmy.Month
                y = dmy.Year
                If y > 2500 Then
                    y = y - 543
                End If
                Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
            Else
                Return ""
            End If
        End Function
        Public Function GetReportDataByTime(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara) As DataTable
            Dim StrTime As DateTime = Date.ParseExact(InputPara.TimePeroidFrom, "HH:mm", Nothing)
            Dim EndTime As DateTime = Date.ParseExact(InputPara.TimePeroidTo, "HH:mm", Nothing)
            Dim CurrTime As DateTime = StrTime
            Dim InpTime As String = ""
            Do
                If CurrTime < EndTime Then
                    Dim tmp As String = "'" & CurrTime.ToString("HH:mm") & "-" & DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime).ToString("HH:mm") & "'"
                    If InpTime = "" Then
                        InpTime = tmp
                    Else
                        InpTime += "," & tmp
                    End If
                End If
                CurrTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
            Loop While CurrTime <= EndTime

            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and interval_minute = '" & InputPara.IntervalMinute & "'"
            WhText += " and convert(varchar(10),time_priod_from,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and convert(varchar(10),time_priod_to,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and show_time in (" & InpTime & ") "
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkTimeCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en, service_date, show_time", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetReportDataByDate(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar(10),service_date,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and convert(varchar(10),service_date,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetReportDataByDay(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
            WhText += " and show_week between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetReportDataByWeek(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
            WhText += " and show_week between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustNetworkWeekCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, show_week", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetReportDataByMonth(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara) As DataTable
            Dim MonthPara As String = ""
            For i As Integer = InputPara.MonthFrom To InputPara.MonthTo
                If MonthPara = "" Then
                    MonthPara = i
                Else
                    MonthPara += "," & i
                End If
            Next
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
            WhText += " and month_no in (" & MonthPara & ") "
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentMonthCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, month_no", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetReportDataByYear(ByVal InputPara As CenParaDB.ReportCriteria.CustByWaitingTimePara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentYearCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, show_quarter", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function


    End Class
End Namespace

