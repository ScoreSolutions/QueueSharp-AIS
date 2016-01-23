Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities

Namespace Reports
    Public Class RepAverageServiceTimeComparingWithKPI
        Public Function GetReportByDate(ByVal InputPara As AverageServiceTimeWithKPIPara) As DataTable
            Dim ret As String = ""
            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar(10),service_date,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and convert(varchar(10),service_date,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        

    End Class
End Namespace

