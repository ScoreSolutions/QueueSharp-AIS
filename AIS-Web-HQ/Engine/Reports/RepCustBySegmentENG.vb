Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities
Namespace Reports
    Public Class RepCustBySegmentENG
        Private Function GetServiceSegmentQty(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal ServiceID As Long, ByVal Segment As String, ByVal RepType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0
            'RepType 
            '' R=Register
            '' S=Served
            '' M=Missed
            '' T=Total
            Dim StrTimeFrom As String = TimeFrom.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim StrTimeTo As String = TimeTo.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim sql As String = ""
            'sql += "select count(cq.id) qty "
            'sql += " from tb_counter_queue_history cq "
            'sql += " left join tb_customertype ct on ct.id=cq.customertype_id "
            'sql += " where cq.item_id =" & ServiceID & " and ct.customertype_name = '" & Segment & "'"

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
                sql += " where 1 = 1 "
            Else
                sql += "select count(cq.id) qty "
                sql += " from tb_counter_queue_history cq "
                sql += " left join tb_customertype ct on ct.id=cq.customertype_id "
                sql += " where cq.item_id =" & ServiceID & " and ct.customertype_name = '" & Segment & "'"
            End If

            sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "

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

        Public Function GetReportByWeek(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar,show_year) +   right('0'+ convert(varchar,week_of_year),2)  between '" & InputPara.YearFrom & Right("0" & InputPara.WeekInYearFrom.ToString, 2) & "' and '" & InputPara.YearTo & Right("0" & InputPara.WeekInYearTo.ToString, 2) & "'"

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentWeekCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en, show_year,week_of_year", trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetReportByTime(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
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


            Dim Sql As String = "SELECT [service_date],[shop_id],[shop_name_th],[shop_name_en],[interval_minute],"
            Sql += " [time_priod_from],[time_priod_to],[show_time],[data_column],[data_value], "
            Sql += " convert(varchar(8),service_date,112) ShowDate"
            Sql += " from [TB_REP_CUST_SEGMENT_TIME]"
            Sql += " where " & WhText
            Sql += " order by shop_name_en,service_date, show_time"

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentTimeCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date, show_time", trans.Trans)
            Dim dt As DataTable = lnq.GetListBySql(Sql, trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetReportByDate(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar(8),service_date,112) >= '" & InputPara.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and convert(varchar(8),service_date,112) <= '" & InputPara.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetReportByDay(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            Dim ConvertDay As String = InputPara.DayInWeek
            ConvertDay = ConvertDay.Replace("1", "'Sunday'")
            ConvertDay = ConvertDay.Replace("2", "'Monday'")
            ConvertDay = ConvertDay.Replace("3", "'Tuesday'")
            ConvertDay = ConvertDay.Replace("4", "'Wednesday'")
            ConvertDay = ConvertDay.Replace("5", "'Thursday'")
            ConvertDay = ConvertDay.Replace("6", "'Friday'")
            ConvertDay = ConvertDay.Replace("7", "'Saturday'")

            WhText += " and show_day in (" & ConvertDay & ")"
            WhText += " and show_year between " & InputPara.YearFrom & " and " & InputPara.YearTo
            WhText += " and show_week between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetReportByMonth(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
            'Dim MonthPara As String = ""
            'For i As Integer = InputPara.MonthFrom To InputPara.MonthTo
            '    If MonthPara = "" Then
            '        MonthPara = i
            '    Else
            '        MonthPara += "," & i
            '    End If
            'Next

            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar,show_year)+STUFF('00',2-LEN(month_no)+1,LEN(month_no),month_no) between '" & InputPara.YearFrom & InputPara.MonthFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.MonthTo.ToString.PadLeft(2, "0") & "'"
            'WhText += " and month_no in (" & MonthPara & ") "
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCustSegmentMonthCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, month_no", trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetReportByYear(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
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

