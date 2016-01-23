Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports Engine.Appointment
Imports Engine.Common
Imports pSh = ShParaDB.TABLE
Imports Cen = CenParaDB.TABLE
Imports cSh = ShParaDB.Common
Imports CenParaDB.Appointment
Imports Newtonsoft.Json

Partial Class AppointmentService
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.AddHeader(""

        Select Case Request("MethodName")
            Case "MinAppointmentHour"
                'ระยะเวลาสำหรับการจองล่วงหน้าอย่างน้อย X ชั่วโมง
                Response.Write(JsonConvert.SerializeObject(FunctionEng.GetQisDBConfig("MinAppointmentHour"), Formatting.Indented))
            Case "MaxEditAppointment"
                'ระยะเวลาการแก้ไขหรือยกเลิกข้อมูลการจองล่วงหน้าได้ไม่น้อยกว่า X ชั่วโมง
                Response.Write(JsonConvert.SerializeObject(FunctionEng.GetQisDBConfig("MaxEditAppointmentHour"), Formatting.Indented))
            Case "MaxAppointmentDay"
                'ระยะเวลาสำหรับจองล่วงหน้าได้ไม่เกิน X วัน
                Response.Write(JsonConvert.SerializeObject(FunctionEng.GetQisDBConfig("MaxAppointmentDay"), Formatting.Indented))
            Case "MaxAppointmentService"
                'จำนวนบริการที่สามารถจองล่วงหน้าได้
                Response.Write(JsonConvert.SerializeObject(FunctionEng.GetQisDBConfig("MaxAppointmentService"), Formatting.Indented))
            Case "CheckBackList"
                Dim eng As New AppointmentENG
                Dim p As New AppointmentCheckBacklistResultPara
                p = eng.CheckBacklist(Request("MobileNo"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(p, Formatting.Indented))
                'Response.Write(p.IsBackList)
            Case "GetRegionAllList"
                'รายชื่อภาค
                Dim dt As New DataTable
                Dim eng As New AppointmentENG
                dt = eng.GetRegionAllList
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "GetShopAllList"
                'รายชื่อชอปทั้งหมด
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.GetShopAllList
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "GetServiceAtShop"
                'รายชื่อ Service ที่มีใน Shop
                Dim eng As New AppointmentENG
                Dim ret As String = eng.GetServiceAtShop(Request("ShopID"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(ret, Formatting.Indented))
            Case "GetShopByRegion"
                'ดึงรายชื่อ Shop ตามภูมิภาค
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.GetShopByRegion(Request("RegionID"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "GetShopByService"
                'ดึงรายชื่อ Shop ตาม Service
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.GetShopByService(Request("MasterServiceID"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "GetMasterServiceList"
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.GetMasterServiceList()
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "GetServiceList"
                Dim eng As New AppointmentENG
                Dim dt As New DataTable

                Dim MaxAppointmentDay As String = FunctionEng.GetQisDBConfig("MaxAppointmentDay")
                If MaxAppointmentDay.Trim = "" Then
                    MaxAppointmentDay = "7"
                End If

                Dim DateFrom As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim DateTo As String = DateAdd(DateInterval.Day, Convert.ToInt16(MaxAppointmentDay), DateTime.Now).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

                If Request("ShopID") Is Nothing Then
                    dt = eng.GetServiceAgent_ByDate("0", DateFrom, DateTo)
                Else
                    dt = eng.GetServiceAgent_ByDateAndShop("0", DateFrom, DateTo, Request("ShopID"))
                End If
                If dt.Rows.Count > 0 Then
                    dt = dt.DefaultView.ToTable(True, "id", "item_name_th", "item_name", "item_order")
                End If

                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "YouHaveAppointment"
                'ตรวจสอบว่าผู้ใช้มีข้อมูลการจองล่วงหน้าอยู่แล้วหรือไม่
                Dim eng As New AppointmentENG
                Dim ret As Boolean = eng.GetActiveAppointment(Request("MobileNo"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(ret, Formatting.Indented))
            Case "InsertAppointment"
                'บันทึกขัอมูลการจองล่วงหน้า

                Dim StartTime As String = Request("StartTime")
                Dim yy As Integer = StartTime.Substring(0, 4)
                Dim Mo As Integer = StartTime.Substring(5, 2)
                Dim dd As Integer = StartTime.Substring(8, 2)
                Dim HH As Integer = StartTime.Substring(11, 2)
                Dim mi As Integer = StartTime.Substring(14, 2)

                Dim MobileNo As String = Request("CustomerID")
                Dim AppointmentChannel As String = Request("AppointmentChannel") 'CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.AppointmentChannel.Mobile
                Dim tmpItem() As String = Split(Request("ItemID"), ",")
                Dim para1(tmpItem.Length - 1) As pSh.TbAppointmentCustomerShParaDB
                Dim i As Integer = 0
                For Each ServiceID As String In tmpItem
                    Dim para As New pSh.TbAppointmentCustomerShParaDB
                    para.CAPACITY = 1
                    para.APP_DATE = DateTime.Now
                    para.START_SLOT = New DateTime(yy, Mo, dd, HH, mi, 0)
                    'para.END_SLOT = EndTime
                    para.ITEM_ID = ServiceID
                    para.CUSTOMER_ID = MobileNo
                    para.APPOINTMENT_CHANNEL = AppointmentChannel
                    If Request("EMail") IsNot Nothing Then
                        para.CUSTOMER_EMAIL = Request("EMail")
                    End If
                    para.ACTIVE_STATUS = 1
                    para1(i) = para
                    para = Nothing
                    i += 1
                Next

                Dim eng As New AppointmentENG
                Dim p As New CenParaDB.Appointment.InsertAppointmentResultPara
                p = eng.InsertAppointment(Request("ShopID"), para1, Request("PreferLang"), AppointmentChannel, True)
                para1 = Nothing
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(p, Formatting.Indented))
            Case "GetCustomerProfile"
                Dim eng As New AppointmentENG
                Dim p As New Cen.TbCustomerCenParaDB
                Dim SessionID As String = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                p = eng.GetCustomerProfile(Request("MobileNo"), SessionID)
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(p, Formatting.Indented))
            Case "GetAppointmentDesc"
                'ดึงข้อมูลรายละเอียดการจองที่ยังไม่ถึงเวลานัด
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.GetAppointmentDesc(Request("MobileNo"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "GetLastShopRegister"
                'ดึงข้อมูล Shop ล่าสุดที่ลูกค้าเข้ามา Register
                Dim eng As New AppointmentENG
                Dim p As New CenParaDB.Appointment.LastShopRegisterPara
                p = eng.GetLastShopRegister(Request("MoblieNo"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(p, Formatting.Indented))
            Case "GetAppointmentHistory"
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.GetAppointmentHistory(Request("MobileNo"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented))
            Case "CancelAppointment"
                Dim eng As New AppointmentENG
                Dim ret As Boolean = eng.CancelAppointment(Request("MobileNo"))
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(ret, Formatting.Indented))
            Case "EditAppointment"
                Dim StartTime As String = Request("StartTime")
                Dim yy As Integer = StartTime.Substring(0, 4)
                Dim Mo As Integer = StartTime.Substring(5, 2)
                Dim dd As Integer = StartTime.Substring(8, 2)
                Dim HH As Integer = StartTime.Substring(11, 2)
                Dim mi As Integer = StartTime.Substring(14, 2)

                Dim eMail As String = ""
                If Request("EMail") IsNot Nothing Then
                    eMail = Request("EMail")
                End If

                Dim ret As New EditAppointmentResultPara
                Dim eng As New AppointmentENG
                ret = eng.EditAppointment(Request("ShopID"), New DateTime(yy, Mo, dd, HH, mi, 0), New DateTime(yy, Mo, dd, HH, mi, 0), Request("ItemID"), Request("CustomerID"), Request("PreferLang"), Request("AppointmentChannel"), eMail)
                eng = Nothing
                Response.Write(JsonConvert.SerializeObject(ret, Formatting.Indented))
            Case "CreateTimeSlot"
                Dim eng As New AppointmentENG
                Dim dt As New DataTable
                dt = eng.CreateTimeSlot(Request("ShopID"), Request("ServiceID"))
                eng = Nothing

                Dim ret As New DataTable
                ret.Columns.Add("ShowDate")
                ret.Columns.Add("StartTime")
                ret.Columns.Add("EndTime")
                ret.Columns.Add("slotTime", GetType(DataTable))

                'Built Time Slot For Mobile
                If dt.Rows.Count > 0 Then
                    Dim dDt As New DataTable
                    dDt = dt.DefaultView.ToTable(True, "ShowDate", "SlotMinute")
                    If dDt.Rows.Count > 0 Then
                        For Each dDr As DataRow In dDt.Rows
                            Dim rDr As DataRow = ret.NewRow
                            rDr("ShowDate") = dDr("ShowDate")
                            'rDr("SlotMinute") = dDr("SlotMinute")

                            dt.DefaultView.RowFilter = "ShowDate='" & dDr("ShowDate") & "' and SlotMinute='" & dDr("SlotMinute") & "' "
                            If dt.DefaultView.Count > 0 Then
                                Dim tmp As New DataTable
                                tmp.Columns.Add("start")
                                tmp.Columns.Add("end")
                                tmp.Columns.Add("status")

                                Dim k As Integer = 0
                                For Each dr As DataRowView In dt.DefaultView
                                    Dim tmpDr As DataRow = tmp.NewRow
                                    tmpDr("start") = dr("ShowTime")
                                    tmpDr("end") = DateAdd(DateInterval.Minute, dr("SlotMinute"), CDate(dr("ShowTime"))).ToString("HH:mm")
                                    If dr("status").ToString = "ว่าง" Then
                                        tmpDr("status") = "1"
                                    Else
                                        tmpDr("status") = "0"
                                    End If
                                    tmp.Rows.Add(tmpDr)

                                    If k = 0 Then
                                        rDr("StartTime") = tmpDr("start")
                                    ElseIf k = (dt.DefaultView.Count - 1) Then
                                        rDr("EndTime") = tmpDr("start")
                                    End If
                                    k += 1
                                Next
                                rDr("slotTime") = tmp
                            End If
                            ret.Rows.Add(rDr)
                            dt.DefaultView.RowFilter = ""
                        Next
                    End If
                    dDt.Dispose()
                End If
                dt.Dispose()

                Response.Write(JsonConvert.SerializeObject(ret, Formatting.Indented))
            Case "CheckSlotCapacity"
                Dim SelectTime As String = Request("SelectTime")
                Dim yy As Integer = SelectTime.Substring(0, 4)
                Dim Mo As Integer = SelectTime.Substring(5, 2)
                Dim dd As Integer = SelectTime.Substring(8, 2)
                Dim HH As Integer = SelectTime.Substring(11, 2)
                Dim mi As Integer = SelectTime.Substring(14, 2)

                Dim ret As Boolean = False
                Dim Eng As New AppointmentENG
                If Request("ShopidOld") Is Nothing Then
                    ret = Eng.CheckSlotCapacity(Request("ShopID"), New DateTime(yy, Mo, dd, HH, mi, 0), Request("MobileNo"), Request("SelServQty"))
                Else
                    ret = Eng.CheckSlotCapacity(Request("ShopID"), New DateTime(yy, Mo, dd, HH, mi, 0), Request("MobileNo"), Request("SelServQty"), Request("ShopidOld"))
                End If
                Eng = Nothing

                Response.Write(JsonConvert.SerializeObject(ret, Formatting.Indented))
        End Select
    End Sub
End Class
