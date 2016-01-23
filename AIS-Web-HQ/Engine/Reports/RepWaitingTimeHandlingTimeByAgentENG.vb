Imports CenParaDB.ReportCriteria
Imports Engine.Common

Namespace Reports
    Public Class RepWaitingTimeHandlingTimeByAgentENG
        
        Public Function GetReportDataTime(ByVal InputPara As WaitingTimeHandlingTimeByAgentPara) As DataTable
            Dim dt As New DataTable
            Dim StrTime As String = InputPara.TimePeroidFrom.ToString("HH:mm", New Globalization.CultureInfo("en-US"))
            Dim EndTime As String = InputPara.TimePeroidTo.ToString("HH:mm", New Globalization.CultureInfo("en-US"))

            Dim sql As String = ""
            sql += " select shop_id, shop_name_en, CONVERT(varchar(10),service_date,112) show_date, "
            sql += " user_id,user_code, staff_name, username,service_type, queue_no,mobile_no, service_date, awt, aht, act, cwt, cht, cct"
            sql += " from TB_REP_WT_HT_AGENT q"
            sql += " where CONVERT(varchar(10),service_date,120) between '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            sql += " and CONVERT(varchar(5), service_date,114) between '" & StrTime & "' and '" & EndTime & "' "
            sql += " and shop_id in (" & InputPara.ShopID & ")"
            sql += " order by shop_name_en,convert(varchar(10), service_date,120), staff_name, service_type"

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtAgentCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Function GetReportDataDate(ByVal InputPara As CustByWaitingTimePara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtStaffDayCenLinqDB
                Dim wh As String = "convert(varchar(8),service_date,112) between '" & InputPara.DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                wh += " and shop_id in (" & InputPara.ShopID & ")"
                dt = lnq.GetDataList(wh, "shop_name_en,service_date,staff_name,service_id", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Function GetReportDataWeek(ByVal InputPara As CustByWaitingTimePara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtStaffWeekCenLinqDB
                Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
                WhText += " and convert(varchar,show_year) +  STUFF('00',2-LEN(week_of_year)+1,LEN(week_of_year),week_of_year)  between '" & InputPara.YearFrom & InputPara.WeekInYearFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.WeekInYearTo.ToString.PadLeft(2, "0") & "'"

                dt = lnq.GetDataList(WhText, "shop_name_en,show_year,week_of_year,staff_name,service_id", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Function GetReportDataMonth(ByVal InputPara As CustByWaitingTimePara) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
                WhText += " and convert(varchar,show_year)+STUFF('00',2-LEN(month_no)+1,LEN(month_no),month_no) between '" & InputPara.YearFrom & InputPara.MonthFrom.ToString.PadLeft(2, "0") & "' and '" & InputPara.YearTo & InputPara.MonthTo.ToString.PadLeft(2, "0") & "'"

                Dim lnq As New CenLinqDB.TABLE.TbRepWtHtStaffMonthCenLinqDB
                dt = lnq.GetDataList(WhText, "shop_name_en,show_year,month_no,staff_name,service_id", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function
    End Class
End Namespace

