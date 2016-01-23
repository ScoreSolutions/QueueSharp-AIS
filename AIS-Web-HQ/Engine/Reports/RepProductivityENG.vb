Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities

Namespace Reports
    Public Class RepProductivityENG
        
        Public Function GetRportDataByDate(ByVal InputPara As CenParaDB.ReportCriteria.ProductivityPara) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepProductivityDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("shop_id in (" & InputPara.ShopID & ") and convert(varchar(10),service_date,120) between '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'", "shop_name_en,service_date,staff_name", trans.Trans)
            Return dt
        End Function

        Public Function GetReportByWeek(ByVal InputPara As CenParaDB.ReportCriteria.CustBySegmentPara) As DataTable
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar,show_year)+STUFF('00',2-LEN(week_of_year)+1,LEN(week_of_year),week_of_year) between '" & InputPara.YearFrom & InputPara.WeekInYearFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.WeekInYearTo.ToString.PadLeft(2, "0") & "'"
            'WhText += " and week_of_year between " & InputPara.WeekInYearFrom & " and " & InputPara.WeekInYearTo

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepProductivityWeekCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year, week_of_year", trans.Trans)
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
            Dim lnq As New CenLinqDB.TABLE.TbRepProductivityMonthCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,show_year,month_no", trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function
    End Class
End Namespace

