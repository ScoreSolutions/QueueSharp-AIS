Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports CenParaDB.Common.Utilities.Constant

Namespace Reports
    Public Class RepAppointmentReportByServiceENG
        Public Function GetReportByDate(ByVal InputPara As AppointmentReportByServicePara) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("shop_id")
            ret.Columns.Add("shop_name_th")
            ret.Columns.Add("shop_name_en")
            ret.Columns.Add("transaction_date", GetType(DateTime))
            ret.Columns.Add("appointment_date", GetType(DateTime))
            ret.Columns.Add("appointment_channel")
            ret.Columns.Add("mobile_no")
            ret.Columns.Add("segment")
            ret.Columns.Add("user_code")
            ret.Columns.Add("staff_name")
            ret.Columns.Add("service_id")
            ret.Columns.Add("service_name_en")
            ret.Columns.Add("service_name_th")
            ret.Columns.Add("appointment_status")

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                For Each sh As String In Split(InputPara.ShopID, ",")
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(sh)
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
                    If shTrans.Trans IsNot Nothing Then
                        Dim aLnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                        Dim dt As New DataTable
                        Dim sql As String = " select ac.app_date transaction_date, ac.start_slot appointment_date, ac.customer_id mobile_no,"
                        sql += " cq.segment, u.user_code, u.fname + ' ' + u.lname staff_name,"
                        sql += " ac.item_id service_id, t.item_name service_name_en, t.item_name_th service_name_th,"
                        sql += " ac.appointment_channel,ac.active_status appointment_status"
                        sql += " from TB_APPOINTMENT_CUSTOMER ac"
                        sql += " left join TB_COUNTER_QUEUE_HISTORY cq on cq.customer_id=ac.customer_id and cq.queue_no=ac.queue_no"
                        sql += "        and CONVERT(varchar(16),cq.hold,120)=CONVERT(varchar(16),ac.start_slot,120)"
                        sql += "        and cq.item_id=ac.item_id"
                        sql += " left join TB_USER u on u.id=cq.user_id"
                        sql += " inner join TB_ITEM t on t.id=ac.item_id"
                        sql += " where CONVERT(varchar(8),start_slot,112) between '" & InputPara.DateFrom & "' and '" & InputPara.DateTo & "' "

                        dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
                        shTrans.CommitTransaction()

                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim rDr As DataRow = ret.NewRow
                                rDr("shop_id") = shLnq.ID
                                rDr("shop_name_th") = shLnq.SHOP_NAME_TH
                                rDr("shop_name_en") = shLnq.SHOP_NAME_EN
                                rDr("transaction_date") = dr("transaction_date")
                                rDr("appointment_date") = dr("appointment_date")
                                rDr("appointment_channel") = dr("appointment_channel")
                                rDr("mobile_no") = dr("mobile_no")
                                rDr("segment") = GetMobileSegment(dr("mobile_no"), shTrans)
                                rDr("user_code") = dr("user_code")
                                rDr("staff_name") = dr("staff_name")
                                rDr("service_id") = dr("service_id")
                                rDr("service_name_en") = dr("service_name_en")
                                rDr("service_name_th") = dr("service_name_th")
                                rDr("appointment_status") = GetAppointmentStatus(dr("appointment_status"))

                                ret.Rows.Add(rDr)
                            Next
                            dt.Dispose()
                        End If
                    End If
                    shLnq = Nothing
                Next
            End If
            trans.CommitTransaction()
            


            'Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            'WhText += " and convert(varchar(8),appointment_date,112) >= '" & InputPara.DateFrom & "'"
            'WhText += " and convert(varchar(8),appointment_date,112) <= '" & InputPara.DateTo & "'"
            'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            'Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentServiceDateCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,appointment_date", trans.Trans)
            'trans.CommitTransaction()
            'lnq = Nothing

            Return ret
        End Function

        Private Function GetMobileSegment(ByVal MobileNo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = "Mass"
            Dim lnq As New ShLinqDB.TABLE.TbSegmentShLinqDB
            Dim dt As New DataTable

            Dim cPara As New CenParaDB.TABLE.TbCustomerCenParaDB
            Dim gw As New Engine.GeteWay.GateWayServiceENG
            cPara = gw.GetCustomerProfile(MobileNo)
            gw = Nothing

            If cPara.SEGMENT_LEVEL.Trim <> "" Then
                Dim sql As String = "select ct.customertype_name "
                sql += " from tb_customertype ct "
                sql += " inner join tb_segment s on s.customertype_id=ct.id"
                sql += " where s.active_status = 1 and rtrim(ltrim(s.segment)) = '" & cPara.SEGMENT_LEVEL & "'"

                dt = lnq.GetListBySql(sql, shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    ret = dt.Rows(0)("customertype_name")
                End If
            End If
            dt.Dispose()
            lnq = Nothing
            

            Return ret
        End Function

        Private Function GetAppointmentStatus(ByVal StatusCode As String) As String
            Dim ret As String = ""
            Select Case StatusCode
                Case TbAppointmentCustomer.ActiveStatus.ConfirmAppointment
                    ret = "Comfirm Appointment"
                Case TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk
                    ret = "Registered"
                Case TbAppointmentCustomer.ActiveStatus.EndQueue
                    ret = "Complete"
                Case TbAppointmentCustomer.ActiveStatus.Missed
                    ret = "Missed Call"
                Case TbAppointmentCustomer.ActiveStatus.Cancel
                    ret = "Cancel"
                Case TbAppointmentCustomer.ActiveStatus.NoShow
                    ret = "No Show"
            End Select

            Return ret
        End Function
    End Class
End Namespace

