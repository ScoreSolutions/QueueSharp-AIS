Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities.Constant
Namespace Reports
    Public Class ReportsAppointmentByServiceENG : Inherits ReportsENG
        Public Sub ProcessReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : AppointmentReportByServiceByDate", ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_APPOINTMENT_SERVICE_DATE where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportAppointmentByServiceENG.ProcessReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        'หาคิวที่ได้ทำรายการจอง
                        Dim dt As New DataTable
                        dt = GetAppointmentDataByPeriodDate(ServiceDate, ServiceDate, shTrans)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim lnq As New CenLinqDB.TABLE.TbRepAppointmentServiceDateCenLinqDB
                                Try
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.TRANSACTION_DATE = dr("transaction_date")
                                    lnq.APPOINTMENT_DATE = dr("appointment_date")
                                    lnq.APPOINTMENT_CHANNEL = Convert.ToChar(dr("appointment_channel"))
                                    lnq.MOBILE_NO = dr("mobile_no")
                                    lnq.SEGMENT = IIf(IsDBNull(dr("segment")) = False, dr("segment"), GetMobileSegment(lnq.MOBILE_NO, shTrans))
                                    lnq.USER_CODE = IIf(IsDBNull(dr("user_code")) = False, dr("user_code"), "-")
                                    lnq.STAFF_NAME = IIf(IsDBNull(dr("staff_name")) = False, dr("staff_name"), "-")
                                    lnq.SERVICE_ID = dr("service_id")
                                    lnq.SERVICE_NAME_EN = dr("service_name_en")
                                    lnq.SERVICE_NAME_TH = dr("service_name_th")
                                    lnq.APPOINTMENT_STATUS = GetAppointmentStatus(dr("appointment_status"))

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcReportByDate", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsAppointmentByServiceENG.ProcReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentReportByShopServiceENG")
                                    End If
                                Catch ex As Exception
                                    FunctionEng.SaveErrorLog("ReportsAppointmentByServiceENG.ProcessReportDate", shLnq.SHOP_ABB & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsAppointmentReportByShopServiceENG")
                                End Try
                                lnq = Nothing

                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                Application.DoEvents()
                            Next
                        End If
                        dt.Dispose()
                    End If
                Else
                    UpdateProcessError(ProcID, trans.ErrorMessage)
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetMobileSegment(ByVal MobileNo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
            Dim ret As String = "Mass"
            Dim lnq As New ShLinqDB.TABLE.TbSegmentShLinqDB
            Dim dt As New DataTable

            Dim cPara As New CenParaDB.TABLE.TbCustomerCenParaDB
            Dim gw As New Engine.GateWay.GateWayServiceENG
            cPara = gw.GetCustomerProfile(MobileNo)
            gw = Nothing

            Dim sql As String = "select ct.customertype_name "
            sql += " from tb_customertype ct "
            sql += " inner join tb_segment s on s.customertype_id=ct.id"
            sql += " where s.active_status = 1 and rtrim(ltrim(s.segment)) = '" & cPara.SEGMENT_LEVEL & "'"

            dt = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0)("customertype_name")
            End If

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


        Private Function GetAppointmentDataByPeriodDate(ByVal SlotDateFrom As DateTime, ByVal SlotDateTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
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
            sql += " where CONVERT(varchar(8),start_slot,112) between '" & SlotDateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & SlotDateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' "

            dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            Return dt
        End Function
    End Class
End Namespace

