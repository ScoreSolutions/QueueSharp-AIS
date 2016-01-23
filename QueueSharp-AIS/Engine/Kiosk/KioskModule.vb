Imports System.Data.SqlClient
Imports Engine.Common.ShopConnectDBENG
Imports System.Windows.Forms
Imports System.Drawing

Namespace Kiosk
    Public Module KioskModule
        Dim _err As String = ""
        Public CountVDO = 0
        Public MaxCountVDO As Int32 = 120
        Public Mobile As String = ""
        Public CustomerTypeID As Int32 = 0
        Public DefaultCustomerTypeID As Int32 = 0
        Public Campang As String = ""
        Public Campaign_TH As String = ""
        Public Campaign_EN As String = ""
        Public Campaign_Desc1_EN As String = ""
        Public Campaign_Desc2_EN As String = ""
        Public Campaign_Desc1_TH As String = ""
        Public Campaign_Desc2_TH As String = ""
        Public Contact_ID As String = ""
        Public AssetID As String = ""
        Public NetworkType As String = ""
        Public Segment As String = ""
        Public CustomerName As String = ""
        Public Appointment As Boolean = False
        Public CustomerApp As Boolean = False   'เป็นลูกค้าที่สามารถจองได้หรือไม่
        Public CustomerAIS As Boolean = False    'เป็นเบอร์ลูกค้า AIS หรือไม่
        Public SMECorporateType As String = ""
        Public Lang_TH As Boolean = True
        Public ConnecetionPrimaryDB As Boolean = True
        Public Const AppointmentChannel As String = "1" 'Kiosk = 1

        'Kiosk Capture
        Public CustomerImageID As Long = 0
        Public CustomerPAGroup As String = ""
        Public CustomerURLCapture As String = ""
        Public CustomerURLCaptureDate As String = ""
        Public CustomerBOS As String = ""
        Public CustomerMobileStatus As String = ""
        Public CustomerCategory As String = ""
        Public CustomerBrandName As String = ""

        'Kiosk Config
        Public k_before As Int32 = 0
        Public k_late As Int32 = 0
        Public k_before_app As Int32 = 0
        Public k_cancel As Int32 = 0
        Public k_disable As Int32 = 0
        Public k_serve As Int32 = 0
        Public k_refresh As Int32 = 0
        Public k_vdo As Int32 = 0
        Public k_len As Int32 = 0
        Public k_no_show As Int32 = 0
        Public k_not_booked As Int32 = 0
        Public k_nud_k_no_show_within As Int32 = 0
        Public k_mobile1 As String = ""
        Public k_mobile2 As String = ""
        Public k_mobile3 As String = ""
        Public k_mobile4 As String = ""
        Public k_max_appointment As Int32 = 0
        Public s_close As String = ""
        Public k_before_close As Int32 = 0
        Public q_con_ldap As Boolean = False
        Public m_webservice_url As String = ""
        Public d_webservice_url As String = ""
        Public ShowCustomerCapture As String = ""
        Public PrintTicketCoupon As String = ""

        'Kiosk Language
        Public Language As String = "TH"
        Public msgWarning As String = "ผิดพลาด"
        Public msgVerifyYourNumber As String = "กรุณาตรวจสอบหมายเลขโทรศัพท์"
        Public msgWrongNumberFormat As String = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง"
        Public msgQCancelled As String = "คิวนี้ได้ยกเลิกแล้ว" & vbCrLf & "กรุณากดเลือกทำรายการ" & vbCrLf & "เพื่อรับบริการอีกครั้ง"
        Public msgWantToBeServedNow As String = "ต้องการรับบริการทันทีหรือไม่?"
        Public itemname_field As String = "item_name_th"
        Public msgCancelAppointment As String = "ต้องการยกเลิก" & vbCrLf & "การจองคิวใช่หรือไม่ ?"
        Public msgCancelAppointmentCompleted1 As String = "ระบบได้ยกเลิกการจอง" & vbCrLf & "หมายเลข "
        Public msgCancelAppointmentCompleted2 As String = vbCrLf & vbCrLf & "เรียบร้อยแล้ว" & vbCrLf & "ขอบคุณค่ะ"
        Public msgGetTicketAndWaitForAppointment As String = "กรุณารับบัตรคิว" & vbCrLf & "และรอรับบริการประมาณ"
        Public msgAppointmentNotSupported As String = "ระบบไม่รองรับประเภทลูกค้านัด" & vbCrLf & "กรุณาติดต่อเจ้าหน้าที่"
        Public msgMaximum As String = "เลือกได้สูงสุด "
        Public msgService As String = " บริการ"
        Public msgHasAppointment As String = "มีนัดรับบริการ"
        Public msgDateon As String = "วันที่: "
        Public msgTimeAt As String = " เวลา: "
        Public msgAppTimeAt As String = " เวลา: "
        Public msgAtclock As String = " น."
        Public msgBefore As String = "ก่อน"
        Public msgCannotAppoinment As String = "ไม่สามารถทำรายการจองได้ "
        Public msgPleasmakeanew As String = " จะสามารถทำการจองได้อีกครั้ง "
        Public msgYouChooseTheService As String = "คุณเลือกบริการ"
        Public msgBookingNotAllowed As String = "ซึ่งระบบไม่ได้เปิดการจองไว้ในวันนี้"
        Public msgAppointmentAgain As String = " ในวันที่ "
        Public msgPleaseChooseOneService = "กรุณากดเลือกบริการ"
        Public msgOnward As String = " onwards"
        Public msgCannotBBooked As String = "ไม่สามารถทำการจองได้ "
        Public msgPlsWaitForAbout As String = "ประมาณ "
        Public msgMinute As String = " นาที"
        Public msgPlsChkQSetting As String = "กรุณาเช็คการตั้งค่าของหมายเลขคิว "
        Public msgOfService As String = "ของบริการ "
        Public msgAvailBookTime As String = "เวลาที่สามารถจองได้คือ "
        Public msgConfirm As String = "ยืนยัน"
        Public msgSureToBook As String = "ต้องการทำการจอง ใช่หรือไม่?"
        Public msgChooseAppTime As String = "กรุณาเลือกเวลาการจอง"
        Public msgAppTime As String = "ช่วงเวลาที่เลือกรับบริการ" & vbCrLf & "ไม่เพียงพอต่อบริการที่เลือก" & vbCrLf & vbCrLf & "กรุณาเลือกบริการใหม่"
        Public msgComeB4 As String = "กรุณามาก่อนเวลานัด "
        Public msgIfLate As String = "หากมาเกินเวลานัด" & vbCrLf & "คิวจะยกเลิกโดยอัติโนมัติ"
        Public msgBook As String = ""
        Public msgPleastaketicket As String = "กรุณารับบัตรคิว "
        '----
        Public msgYouwillservewith As String = ""
        Public msgYouwillbeservefirstwith As String = ""
        Public msgWithinabout As String = ""
        Public msgAbout As String = ""
        Public msgNotSufficientTime As String = ""
        Public msgPleaseSelectAppAgain As String = ""
        Public msgMaxServiceApp As String = "เลือกได้สูงสุด "
        Public msgHaveAppointment As String = "มีนัดรับบริการ"
        Public msgPleasArrive As String = "กรุณามาก่อนเวลานัด "
        Public msgMinuteBeforeApptime As String = " นาที"
        Public msgLateArriveThan As String = "Late arrival than the scheduled time"
        Public msgAutomaticCancelApp As String = "หากมาเกินเวลานัด คิวจะยกเลิกโดยอัตโนมัติ"
        Public msgMobile As String = ""
        Public msgDialogBtnOK As String = "ตกลง"

        Public msgDialogCaptureNotComplete As String = "ไม่สามารถบันทึกภาพได้!"

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property


        Public Conn As New SqlConnection
        Public Function CheckConn(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal SQL As String) As Boolean
            If CheckDBConn(Conn, INIFileName, SoftwareName, FunctionName, SQL) = False Then
                Return False
            End If
            Return True
        End Function


#Region "Manage Query"

        Public Function getDataTable(ByVal SQL As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Try
                If CheckConn(INIFileName, SoftwareName, FunctionName, SQL) = True Then
                    da = New SqlDataAdapter(SQL, Conn)
                    da.Fill(dt)
                    Conn.Close()
                    Return dt
                End If
            Catch ex As Exception
                'showFormError(ex.Message & Environment.NewLine & Environment.NewLine & SQL)
                Return dt
            End Try
            Return New DataTable
        End Function

        Public Sub executeSQL(ByVal SQL As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FuncationName As String)
            If SQL.Trim <> "" Then
                Dim cmd As New SqlCommand(SQL)
                Try
                    If CheckConn(INIFileName, SoftwareName, FuncationName, SQL) = True Then
                        cmd.Connection = Conn
                        cmd.ExecuteNonQuery()
                        Conn.Close()
                    End If
                Catch ex As Exception
                    Engine.Common.FunctionEng.SaveShopErrorLog(SoftwareName, ex.Message & Environment.NewLine & Environment.NewLine & SQL)
                End Try
            End If
        End Sub

#End Region


#Region "Convert Data"
        Public Function FixDB(ByVal TXT As String) As String 'Replace text จาก ' เป็น ''
            If IsDBNull(TXT) = True Then
                Return ""
            ElseIf TXT = Nothing Then
                Return ""
            Else
                Return Trim(TXT.ToString.Replace("'", "''"))
            End If
        End Function

        Function FixMoney(ByVal MyMoney As Double) As String 'Convert ตัวเลข เป็น เลขทศนิยม 2 ตำแหน่ง
            Dim Money As String = ""
            Money = Format(MyMoney, "#,###.##")
            Return Money
        End Function

        Public Function FixDate(ByVal StringDate As String) As String 'Convert วันที่ให้เป็น YYYYMMDD
            Dim d As String = ""
            Dim m As String = ""
            Dim y As String = ""
            If IsDate(StringDate) Then
                Dim dmy As Date = CDate(StringDate)
                d = dmy.Day
                m = dmy.Month
                y = dmy.Year
                If y > 2500 Then
                    y = y - 543
                End If
                Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
            Else
                Return ""
            End If
        End Function
        Public Function FixDateTime(ByVal DateTimeIn As DateTime) As String 'Convert จาก DataRow Datetime ให้เป็น yyyy-MM-dd HH:mm:ss.fff  ใช้สำหรับ Insert หรือ Update ลงฐานข้อมูล
            Dim ret As String = ""
            ret = "'" & DateTimeIn.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
            Return ret
        End Function

#End Region


        Public Function CheckUpdateConfig(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Boolean
            Dim ret As Boolean = False
            If CheckConn(INIFileName, SoftwareName, FunctionName, "") = True Then
                Try
                    Dim sql As String = ""
                    Dim dt As New DataTable
                    '************** Check Configuration ***************
                    sql = "select * from tb_setting"
                    dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
                    If dt.Rows.Count > 0 Then
                        For i As Int32 = 0 To dt.Rows.Count - 1
                            Select Case dt.Rows(i).Item("config_name").ToString.Trim
                                Case "k_before" 'มาก่อนเวลา
                                    k_before = dt.Rows(i).Item("config_value").ToString
                                Case "k_late" 'มาสาย
                                    k_late = dt.Rows(i).Item("config_value").ToString
                                Case "k_before_app" 'จองก่อนเวลา
                                    k_before_app = dt.Rows(i).Item("config_value").ToString
                                Case "k_cancel" 'ยกเลิกการนัด
                                    k_cancel = dt.Rows(i).Item("config_value").ToString
                                Case "k_disable" 'ซ่อนเวลารอถ้ามากเกินไป
                                    k_disable = dt.Rows(i).Item("config_value").ToString
                                Case "k_serve" 'ลูกค้ารับบริการได้กี่รายการ
                                    k_serve = dt.Rows(i).Item("config_value").ToString
                                Case "k_refresh" 'Refresh
                                    k_refresh = dt.Rows(i).Item("config_value").ToString
                                Case "k_vdo" 'Show VDO
                                    k_vdo = dt.Rows(i).Item("config_value").ToString
                                Case "k_len" 'Show VDO
                                    k_len = dt.Rows(i).Item("config_value").ToString
                                Case "k_no_show" 'No Show
                                    k_no_show = dt.Rows(i).Item("config_value").ToString
                                Case "k_not_booked" 'Not Booked
                                    k_not_booked = dt.Rows(i).Item("config_value").ToString
                                Case "k_mobile1" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 1
                                    k_mobile1 = dt.Rows(i).Item("config_value").ToString
                                Case "k_mobile2" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 2
                                    k_mobile2 = dt.Rows(i).Item("config_value").ToString
                                Case "k_mobile3" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 3
                                    k_mobile3 = dt.Rows(i).Item("config_value").ToString
                                Case "k_mobile4" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 4
                                    k_mobile4 = dt.Rows(i).Item("config_value").ToString
                                Case "q_con_ldap" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 4
                                    If dt.Rows(i).Item("config_value").ToString = "1" Then
                                        q_con_ldap = True
                                    End If
                                Case "k_max_appointment" 'จองได้สูงสุดกี่บริการ
                                    k_max_appointment = dt.Rows(i).Item("config_value").ToString
                                Case "s_close"
                                    s_close = dt.Rows(i).Item("config_value").ToString
                                Case "k_before_close"
                                    k_before_close = dt.Rows(i).Item("config_value").ToString
                                Case "nud_k_no_show_within" 'ถ้าจองแล้วไม่มาตามนัดครบจำนวนครั้งใน X วัน 
                                    k_nud_k_no_show_within = dt.Rows(i).Item("config_value")
                                Case "m_webservice_url" 'Main Server WebService URL
                                    m_webservice_url = dt.Rows(i).Item("config_value").ToString
                                Case "d_webservice_url" 'Display Server WebService URL
                                    d_webservice_url = dt.Rows(i).Item("config_value").ToString
                                Case "ShowCustomerCapture"
                                    ShowCustomerCapture = dt.Rows(i)("config_value").ToString
                                Case "PrintTicketCoupon"
                                    PrintTicketCoupon = dt.Rows(i)("config_value").ToString

                            End Select
                        Next
                    End If

                    '*********** Check Default CustomerType ***********
                    sql = "select id from TB_CUSTOMERTYPE where active_status = 1 and def = 1"
                    dt = New DataTable
                    dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
                    If dt.Rows.Count > 0 Then
                        DefaultCustomerTypeID = dt.Rows(0).Item("id").ToString
                    Else
                        If Language = "TH" Then
                            'ShowDialogBox("กรุณาเช็คการตั้งค่าพื้นฐานประเภทลูกค้า" & vbCrLf & "ในระบบคิว", msgWarning)
                            _err = "กรุณาเช็คการตั้งค่าพื้นฐานประเภทลูกค้า" & vbCrLf & "ในระบบคิว"
                        Else
                            'ShowDialogBox("Please Verify Customer's Mandatory Settings", msgWarning)
                            _err = "Please Verify Customer's Mandatory Settings"
                        End If
                    End If
                    '**************************************************

                    ret = True
                Catch ex As Exception
                    _err = "KioskENG.CheckUpdateConfig Exception : " & ex.Message
                End Try
            Else
                'ShowDialogBox("ไม่สามารถเชื่อต่อกับฐานข้อมูลของระบบ" & vbCrLf & "กรุณาติดต่อเจ้าหน้าที่", msgWarning)
                _err = "ไม่สามารถเชื่อต่อกับฐานข้อมูลของระบบ" & vbCrLf & "กรุณาติดต่อเจ้าหน้าที่"
            End If
            Return ret
        End Function

#Region "Insert Service & Log"
        Public Sub InsertServiceAppointment(ByVal AppTime As String, ByVal CustomerID As String, ByVal CustomerTypeID As Integer, ByVal ItemID As Integer, ByVal CustomerName As String, ByVal Segment As String, ByVal vDateNow As String, ByVal QueueNo As String, ByVal UserID As Int32, ByVal NetworkType As String, ByVal CurrDB As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim CurrDate As String = FindDateNow(INIFileName, SoftwareName, FunctionName).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = ""
            sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_COUNTER_QUEUE);"
            sql &= " insert into TB_counter_queue(id,queue_no,customer_id,customertype_id,item_id,hold,segment,counter_id,service_date,status,user_id,assign_time,network_type) "
            sql &= " values(@ID,'" & FixDB(QueueNo) & "','" & FixDB(Mobile) & "'," & CustomerTypeID & "," & ItemID & ",'" & CurrDate & " " & AppTime & ":00.000','" & Segment & "',0," & vDateNow & ",1,0, " & vDateNow & ",'" & NetworkType & "')"
            executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            InsertLog(QueueNo, CustomerID, UserID, ItemID, 0, 1, "", vDateNow, CurrDB, INIFileName, SoftwareName, FunctionName)
            'If CurrDB = "MAIN" Then
            '    Dim dt As New DataTable
            '    dt = getDataTable("select top 1 id,rowguid from TB_COUNTER_QUEUE where queue_no='" & QueueNo & "' and customer_id='" & Mobile & "' and item_id='" & ItemID & "' and DATEDIFF(D,GETDATE(),service_date) = 0", INIFileName, SoftwareName, FunctionName)
            '    If dt.Rows.Count > 0 Then
            '        Dim dr As DataRow = dt.Rows(0)
            '        sql = " insert into TB_counter_queue(id,queue_no,customer_id,customertype_id,item_id,hold,segment,counter_id,service_date,status,user_id,rowguid,assign_time,network_type,assign_time) "
            '        sql &= " values('" & dr("id") & "','" & FixDB(QueueNo) & "','" & FixDB(Mobile) & "'," & CustomerTypeID & "," & ItemID & ",'" & FixDate(FindDateNow(INIFileName, SoftwareName, FunctionName)) & " " & AppTime & "','" & Segment & "',0," & vDateNow & ",1,0,'" & dr("rowguid").ToString & "'," & vDateNow & ",'" & NetworkType & "'," & vDateNow & ")"

            '        Engine.Common.KioskENG.ExecuteSqlToDisplay(sql, INIFileName)
            '        dt.Dispose()
            '    End If
            'End If
        End Sub

        Sub InsertService(ByVal CustomerID As String, ByVal CustomerTypeID As Integer, ByVal ItemID As Integer, ByVal CustomerName As String, ByVal Segment As String, ByVal QueueNo As String, ByVal UserID As Int32, ByVal NetworkType As String, ByVal vDateNow As String, ByVal CurrDB As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)

            Dim sql As String = ""
            'Dim  = FixDateTime(FindDateNow)
            sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_COUNTER_QUEUE);"
            sql &= " insert into TB_counter_queue(id,queue_no,customer_id,customertype_id,item_id,segment,counter_id,service_date,status,user_id, network_type, assign_time) "
            sql &= " values(@ID,'" & FixDB(QueueNo) & "','" & FixDB(Mobile) & "'," & CustomerTypeID & "," & ItemID & ",'" & Segment & "',0," & vDateNow & ",1,0,'" & NetworkType & "'," & vDateNow & ")"
            executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            InsertLog(QueueNo, CustomerID, UserID, ItemID, 0, 1, "", vDateNow, CurrDB, INIFileName, SoftwareName, FunctionName)
            'If CurrDB = "MAIN" Then
            '    Dim dt As New DataTable
            '    dt = getDataTable("select top 1 id, rowguid from TB_COUNTER_QUEUE where queue_no='" & QueueNo & "' and customer_id='" & Mobile & "' and item_id='" & ItemID & "' and DATEDIFF(D,GETDATE(),service_date) = 0", INIFileName, SoftwareName, FunctionName)
            '    If dt.Rows.Count > 0 Then
            '        Dim dr As DataRow = dt.Rows(0)
            '        sql = " insert into TB_counter_queue(id,queue_no,customer_id,customertype_id,item_id,segment,counter_id,service_date,status,user_id,rowguid, network_type, assign_time) "
            '        sql &= " values('" & dr("id") & "','" & FixDB(QueueNo) & "','" & FixDB(Mobile) & "'," & CustomerTypeID & "," & ItemID & ",'" & Segment & "',0," & vDateNow & ",1,0,'" & dr("rowguid").ToString & "','" & NetworkType & "'," & vDateNow & ")"

            '        Engine.Common.KioskENG.ExecuteSqlToDisplay(sql, INIFileName)
            '        dt = Nothing
            '    End If
            'End If
        End Sub

        'Insert Log กรณีที่มีการเปลี่ยนแปลงสถานะของลูกค้า
        Sub InsertLog(ByVal QueueNo As String, ByVal CustomerID As String, ByVal UserID As Integer, ByVal ItemID As Integer, ByVal CounterID As Integer, ByVal Status As Integer, ByVal Flag As String, ByVal vDateNow As String, ByVal CurrDB As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim sql As String = ""
            Dim dt As New DataTable
            Dim RowID As Int32 = 0
            Dim vQID As Int32 = 0
            sql = "select top 1 id from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and customer_id = '" & CustomerID & "' and queue_no = '" & QueueNo & "' and item_id = " & ItemID & " order by service_date desc"
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            If dt.Rows.Count > 0 Then
                vQID = dt.Rows(0).Item("id")
            End If

            Dim vID As Long = FindID("TB_LOGFILE", INIFileName, SoftwareName, FunctionName)
            sql = " insert into TB_LOGFILE(id,cq_id,log_date,queue_no,customer_id,user_id,item_id,counter_id,status,flag) "
            sql += " values('" & vID & "','" & vQID & "'," & vDateNow & ",'" & FixDB(QueueNo) & "','" & FixDB(CustomerID) & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & FixDB(Flag) & "')"
            executeSQL(sql, INIFileName, SoftwareName, FunctionName)

            'If CurrDB = "MAIN" Then
            '    Dim Ldt As New DataTable
            '    Ldt = getDataTable("select top 1 id, rowguid from TB_LOGFILE where id='" & vID & "'", INIFileName, SoftwareName, FunctionName)
            '    If Ldt.Rows.Count > 0 Then
            '        Dim dr As DataRow = Ldt.Rows(0)
            '        sql = " insert into TB_LOGFILE(id,cq_id,log_date,queue_no,customer_id,user_id,item_id,counter_id,status,flag,rowguid) "
            '        sql += " values('" & vID & "','" & vQID & "'," & vDateNow & ",'" & FixDB(QueueNo) & "','" & FixDB(CustomerID) & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & FixDB(Flag) & "','" & dr("rowguid").ToString & "')"

            '        Engine.Common.KioskENG.ExecuteSqlToDisplay(sql, INIFileName)
            '        dt = Nothing
            '    End If
            '    Ldt.Dispose()
            'End If
        End Sub
#End Region

#Region "GenQueueNo"
        Public Function GenQueueNo(ByVal ItemId As String, ByVal CustomerTypeID As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As String
            Dim QueueNo As String = ""
            Dim sql As String = ""
            Dim dt As New DataTable

            sql = "declare @Item as int; select @Item = " & ItemId & ";declare @Min as varChar(3); select @Min = (select min_queue from TB_CUSTOMERTYPE where id = " & CustomerTypeID & ");declare @Max as varChar(3); select @Max = (select max_queue from TB_CUSTOMERTYPE where id = " & CustomerTypeID & ");declare @Q as Char;select @Q = (select txt_queue from TB_ITEM where id = @Item); select MAX(queue_no) as queue_no,@Q as txt_queue,@Min as min_queue,@Max as max_queue from (select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and item_id = @Item  and queue_no <> '' and convert(int,(right(queue_no,3))) between @Min and @Max and left(queue_no,1) = @Q group by queue_no) as xxx "
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            If dt.Rows(0).Item("queue_no").ToString = "" Then
                QueueNo = dt.Rows(0).Item("txt_queue").ToString & dt.Rows(0).Item("min_queue").ToString.PadLeft(3, "0")
            Else
                Dim Q As Int32
                If CInt(StringFromRight(dt.Rows(0).Item("queue_no").ToString, 3)) + 1 > CInt(dt.Rows(0).Item("max_queue")) Then
                    Q = CInt(dt.Rows(0).Item("min_queue"))
                Else
                    Q = CInt(StringFromRight(dt.Rows(0).Item("queue_no").ToString, 3)) + 1
                End If
                QueueNo = dt.Rows(0).Item("txt_queue").ToString & Q.ToString.PadLeft(3, "0")
            End If
            Return QueueNo
        End Function

        Public Function StringFromLeft(ByVal strTmp As String, ByVal strLength As Integer) As String
            If (strLength > 0 And strTmp.Length >= strLength) Then
                Return strTmp.Substring(0, strLength)
            Else
                Return strTmp
            End If
        End Function

        Public Function StringFromRight(ByVal strTmp As String, ByVal strLength As Integer) As String
            If (strLength > 0 And strTmp.Length >= strLength) Then
                Return strTmp.Substring(strTmp.Length - strLength, strLength)
            Else
                Return strTmp
            End If
        End Function
#End Region

        'หา rowID ของ Tabel
        Public Function FindID(ByVal TableName As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As String
            Dim id As String = ""
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select isnull(MAX(id + 1),1) as id from " & FixDB(TableName)
                dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
                If dt.Rows.Count > 0 Then
                    id = dt.Rows(0).Item("id").ToString
                End If
                Return id
            Catch ex As Exception
                'showFormError(ex.Message & Environment.NewLine & Environment.NewLine)
            End Try
            Return id
        End Function

        Public Function FindDateNow(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Date
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select GETDATE() as DateNow"
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            Dim ret As Date = dt.Rows(0).Item("DateNow")
            dt.Dispose()
            Return ret
        End Function

        Public Function CheckServerIP(ByVal INIFileName As String) As Boolean
            'ตรวจสอบว่า IP ของเครือง MainServer กับ DisplayServer จะต้องไม่ใช่ IP เดียวกัน
            Dim ret As Boolean = False
            Dim ini As New Engine.Common.IniReader(INIFileName)
            ini.Section = "Setting"
            If ini.ReadString("Server") = ini.ReadString("Server1") Then
                ret = True
            End If
            ini = Nothing
            Return ret
        End Function

        Public Function GetSpece(ByVal txt As String, ByVal len As Int32) As String
            Dim T_Spece As String = ""
            Dim L_Spece As Int32
            If txt.Length < len Then
                L_Spece = (len - txt.Length) / 2
                For i As Int32 = 1 To L_Spece
                    T_Spece = T_Spece + " "
                Next
                Return T_Spece & txt
            End If
            Return txt
        End Function

        Public Function GetWebServiceURL(ByVal MainServer As Boolean, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As String
            Dim ret As String = ""
            Dim sql As String = ""
            Dim dt As New DataTable

            If MainServer = True Then
                sql = "select config_value from tb_setting where config_name = 'm_webservice_url'"
            Else
                sql = "select config_value from tb_setting where config_name = 'd_webservice_url'"
            End If
            dt = getDataTable(sql, INIFileName, SoftwareName, FunctionName)
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0).Item("config_value").ToString
            End If
            dt.Dispose()

            Return ret
        End Function

        Public Function GetStrToDateTime(ByVal DateIn As String) As DateTime
            'Convert ข้อมูลวันที่จาก String ในรูปแบบ YYYY-MM-DD HH:mm:ss.fff  ให้เป็นข้อมูล Datetime
            If DateIn.Trim <> "" Then
                Dim yy As Integer = DateIn.Substring(0, 4)
                Dim Mo As Integer = DateIn.Substring(5, 2)
                Dim dd As Integer = DateIn.Substring(8, 2)
                Dim HH As Integer = DateIn.Substring(11, 2)
                Dim mi As Integer = DateIn.Substring(14, 2)
                Dim ss As Integer = DateIn.Substring(17, 2)
                Dim ff As Integer = DateIn.Substring(20, 3)
                Return New Date(yy, Mo, dd, HH, mi, ss, ff)
            Else
                Return New Date(1, 1, 1)
            End If
        End Function
    End Module
End Namespace

