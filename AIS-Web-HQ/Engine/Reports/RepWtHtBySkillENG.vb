Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities

Namespace Reports
    Public Class RepWtHtBySkillENG
        Public Function GetReportByTime(ByVal InputPara As AverageServiceTimeWithKPIPara) As DataTable
            Dim ret As String = ""
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
            WhText += " and convert(varchar(8),service_date,112) between '" & InputPara.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and show_time in (" & InpTime & ") "

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillTimeCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,staff_name,service_date, skill_id ", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetReportByDate(ByVal InputPara As AverageServiceTimeWithKPIPara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillDateCenLinqDB
                Dim wh As String = "convert(varchar(8),service_date,112) between '" & InputPara.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                wh += " and shop_id in (" & InputPara.ShopID & ")"
                dt = lnq.GetDataList(wh, "shop_name_en,service_date,staff_name,skill_id", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function


        Public Function GetReportByWeek(ByVal InputPara As AverageServiceTimeWithKPIPara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillWeekCenLinqDB
                Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
                WhText += " and convert(varchar,show_year)+STUFF('00',2-LEN(week_of_year)+1,LEN(week_of_year),week_of_year) between '" & InputPara.YearFrom & InputPara.WeekInYearFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.WeekInYearTo.ToString.PadLeft(2, "0") & "'"
                'WhText += " and week_of_year between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

                dt = lnq.GetDataList(WhText, "shop_name_en,show_year,week_of_year,staff_name,skill_id", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function


        Public Function GetReportByMonth(ByVal InputPara As AverageServiceTimeWithKPIPara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtSkillMonthCenLinqDB
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

                dt = lnq.GetDataList(WhText, "shop_name_en,show_year,month_no,staff_name,skill_id", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        



    End Class
End Namespace

