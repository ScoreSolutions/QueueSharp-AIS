Imports CenParaDB.ReportCriteria
Imports Engine.Common
Namespace Reports
    Public Class RepAppointmentReportByShopENG
        Public Function GetReportDataTime(ByVal InputPara As AppointmentReportByShopPara) As DataTable
            Dim dt As New DataTable

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
            WhText += " and convert(varchar(8),time_priod_from,112) >= '" & InputPara.DateFrom & "'"
            WhText += " and convert(varchar(8),time_priod_to,112) <= '" & InputPara.DateTo & "'"
            WhText += " and show_time in (" & InpTime & ") "
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopTimeCenLinqDB
                dt = lnq.GetDataList(WhText, "shop_name_en,service_date,show_time", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function


        Public Function GetReportByDate(ByVal InputPara As AppointmentReportByShopPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar(8),service_date,112) >= '" & InputPara.DateFrom & "'"
            WhText += " and convert(varchar(8),service_date,112) <= '" & InputPara.DateTo & "'"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopDateCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetReportByWeek(ByVal InputPara As AppointmentReportByShopPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar,show_year)+ right('0'+ convert(varchar,week_of_year),2)  between '" & InputPara.YearFrom & Right("0" & InputPara.WeekInYearFrom.ToString, 2) & "' and '" & InputPara.YearTo & Right("0" & InputPara.WeekInYearTo.ToString, 2) & "'"
            'WhText += " and week_of_year between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopWeekCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en, show_year, week_of_year", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetReportByMonth(ByVal InputPara As AppointmentReportByShopPara) As DataTable
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
            Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentShopMonthCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en, show_year, month_no", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
    End Class
End Namespace

