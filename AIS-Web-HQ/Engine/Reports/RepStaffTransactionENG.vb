Imports CenParaDB.ReportCriteria
Imports Engine.Common

Namespace Reports
    Public Class RepStaffTransactionENG : Inherits ReportsENG
        Public Function GetQueueDataByShop(ByVal InputPara As StaffTransactionPara) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("shop_name_en")
            ret.Columns.Add("show_date")
            ret.Columns.Add("staff_name")
            ret.Columns.Add("emp_id")
            ret.Columns.Add("queue_no")
            ret.Columns.Add("mobile_no")
            ret.Columns.Add("mobile_segment")
            ret.Columns.Add("service_name_en")
            ret.Columns.Add("register_time", GetType(DateTime))
            ret.Columns.Add("call_time", GetType(DateTime))
            ret.Columns.Add("confirm_time", GetType(DateTime))
            ret.Columns.Add("ht")
            ret.Columns.Add("end_time", GetType(DateTime))
            ret.Columns.Add("wt")

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                For Each ShopID As String In Split(InputPara.SHOP_ID, ",")
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)

                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
                    If shTrans.Trans IsNot Nothing Then
                        Dim shQ As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
                        Dim sql As String = "select q.service_date, u.fname + ' ' + u.lname AS staff,"
                        sql += " u.user_code, q.queue_no, q.customer_id,q.segment, t.item_name, "
                        sql += " q.call_time, q.start_time, q.end_time, "
                        sql += " CASE WHEN isnull(DATEDIFF(SECOND, q.assign_time, q.call_time), 0) < 0 THEN 0 ELSE isnull(DATEDIFF(SECOND, q.assign_time, q.call_time), 0) END AS wt, "
                        sql += " CASE WHEN isnull(DATEDIFF(SECOND, q.assign_time, q.call_time), 0) < 0 THEN 0 ELSE isnull(DATEDIFF(SECOND, q.start_time, q.end_time), 0) END AS ht"
                        sql += " from TB_COUNTER_QUEUE_HISTORY q"
                        sql += " LEFT  JOIN TB_CUSTOMER c ON q.customer_id = c.mobile_no "
                        sql += " LEFT  JOIN dbo.TB_USER u ON q.user_id = u.id "
                        sql += " LEFT OUTER JOIN dbo.TB_ITEM t ON q.item_id = t.id"
                        sql += " where user_id <> '0' and UPPER(isnull(u.fname + ' ' + u.lname,'')) not like '%ADMIN%' "
                        sql += " and convert(varchar(10),q.service_date, 120) between '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' "
                        sql += " order by u.fname,q.service_date"

                        Dim dt As New DataTable '
                        dt = shQ.GetListBySql(sql, shTrans.Trans)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim tmp As DataRow = ret.NewRow
                                tmp("shop_name_en") = shLnq.SHOP_NAME_EN
                                tmp("show_date") = Convert.ToDateTime(dr("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                tmp("staff_name") = dr("staff")
                                tmp("emp_id") = dr("user_code")
                                tmp("queue_no") = dr("queue_no")
                                tmp("mobile_no") = dr("customer_id")
                                tmp("mobile_segment") = dr("segment")
                                tmp("service_name_en") = dr("item_name")
                                tmp("register_time") = dr("service_date")
                                tmp("call_time") = dr("call_time")
                                tmp("confirm_time") = dr("start_time")
                                tmp("ht") = dr("ht")
                                tmp("end_time") = dr("end_time")
                                tmp("wt") = dr("wt")
                                ret.Rows.Add(tmp)
                            Next
                        End If
                        dt.Dispose()
                        shQ = Nothing
                        shTrans.CommitTransaction()
                    End If
                Next
                trans.CommitTransaction()
            End If

            Return ret
        End Function
    End Class
End Namespace

