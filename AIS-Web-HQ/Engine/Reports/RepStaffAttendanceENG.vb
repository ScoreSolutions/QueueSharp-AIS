Imports CenParaDB.ReportCriteria
Imports Engine.Common

Namespace Reports
    Public Class RepStaffAttendanceENG
        Public Function GetReportDataByDate(ByVal InputPara As StaffAttendancePara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim whText As String = " shop_id in (" & InputPara.SHOP_ID & ")"
                whText += " and UPPER(username) <> 'ADMIN'"
                whText += " and convert(varchar(10),service_date, 120) between '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' "
                Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceCenLinqDB
                dt = lnq.GetDataList(whText, "shop_name_en,service_date,staff_name", trans.Trans)
                trans.CommitTransaction()
            End If

            Return dt
        End Function

        Public Function GetReportDataByWeek(ByVal InputPara As StaffAttendancePara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim WhText As String = "shop_id in (" & InputPara.SHOP_ID & ")"
                WhText += " and convert(varchar,show_year) +  STUFF('00',2-LEN(week_of_year)+1,LEN(week_of_year),week_of_year)  between '" & InputPara.YearFrom & InputPara.WeekInYearFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.WeekInYearTo.ToString.PadLeft(2, "0") & "'"

                Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceWeekCenLinqDB
                dt = lnq.GetDataList(WhText, "shop_name_en,show_year,week_of_year,staff_name", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Function GetReportDataByMonth(ByVal InputPara As StaffAttendancePara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
               
                Dim WhText As String = "shop_id in (" & InputPara.SHOP_ID & ")"
                WhText += " and convert(varchar,show_year)+STUFF('00',2-LEN(month_no)+1,LEN(month_no),month_no) between '" & InputPara.YearFrom & InputPara.MonthFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.MonthTo.ToString.PadLeft(2, "0") & "'"

                Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceMonthCenLinqDB
                dt = lnq.GetDataList(WhText, "shop_name_en,show_year,month_no,staff_name", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function
    End Class
End Namespace

