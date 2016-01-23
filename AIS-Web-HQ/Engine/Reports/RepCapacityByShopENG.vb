Imports Engine.Common

Namespace Reports
    Public Class RepCapacityByShopENG

        Public Function GetReportDataByDate(ByVal InputPara As CenParaDB.ReportCriteria.CapacityReportByShopPara) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("shop_id in (" & InputPara.ShopID & ") and convert(varchar(10),service_date,120) between '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'", "shop_name_en, service_date,service_id", trans.Trans)
            Return dt
        End Function
        Public Function GetReportDataByTime(ByVal InputPara As CenParaDB.ReportCriteria.CapacityReportByShopPara) As DataTable
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
            Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopTimeCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en, service_date, show_time,service_id", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetReportDataByWeek(ByVal InputPara As CenParaDB.ReportCriteria.CapacityReportByShopPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar,show_year)+STUFF('00',2-LEN(week_of_year)+1,LEN(week_of_year),week_of_year) between '" & InputPara.YearFrom & InputPara.WeekInYearFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.WeekInYearTo.ToString.PadLeft(2, "0") & "'"
            'WhText += " and week_of_year between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopWeekCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, week_of_year,service_id", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetReportDataByMonth(ByVal InputPara As CenParaDB.ReportCriteria.CapacityReportByShopPara) As DataTable
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
            Dim lnq As New CenLinqDB.TABLE.TbRepCapacityShopMonthCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, month_no,service_id", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
    End Class
End Namespace

