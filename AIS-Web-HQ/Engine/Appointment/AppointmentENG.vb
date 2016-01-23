Imports System.Data
Imports System.Globalization
Imports Cen = CenLinqDB.TABLE
Imports Sh = ShLinqDB.TABLE
Imports pSh = ShParaDB.TABLE
Imports pCen = CenParaDB.TABLE
Imports CenLinqDB.Common.Utilities
Imports uSh = ShLinqDB.Common.Utilities
Imports cSh = ShParaDB.Common
Imports Engine.GeteWay
Imports CenParaDB.Common.Utilities
Imports Engine.Common
Imports CenParaDB.Appointment
Imports CenParaDB.GateWay

Namespace Appointment
    Public Class AppointmentENG
        Dim _err As String = ""

        Public Function MinAppointmentHour(ByVal ShopID As String) As String
            'ระยะเวลาสำหรับการจองล่วงหน้าอย่างน้อย X ชั่วโมง
            Return FunctionEng.GetQisDBConfig("MinAppointmentHour")
        End Function

        Public Function MaxEditAppointmentHour(ByVal ShopID As String) As String
            'ระยะเวลาการแก้ไขหรือยกเลิกข้อมูลการจองล่วงหน้าได้ไม่น้อยกว่า X ชั่วโมง
            Return FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")
        End Function

        Public Function MaxAppointmentDay(ByVal ShopID As String) As String
            'ระยะเวลาสำหรับจองล่วงหน้าได้ไม่เกิน X วัน
            Return FunctionEng.GetQisDBConfig("MaxAppointmentDay")
        End Function

        Public Function MaxAppointmentService(ByVal ShopID As String) As String
            'จำนวนบริการที่สามารถจองล่วงหน้าได้
            Return FunctionEng.GetQisDBConfig("MaxAppointmentService")
        End Function


        Public Function GetRegionAllList() As DataTable
            Dim e As New Configuration.MasterENG
            Dim dt As New DataTable
            dt = e.GetRegionAllList("region_name_th")
            e = Nothing
            Return dt
        End Function

        Public Function GetMasterServiceList() As DataTable
            Dim e As New Configuration.MasterENG
            Dim dt As New DataTable
            dt = e.GetServiceActiveList("1=1")
            e = Nothing
            dt.TableName = "MasterServiceList"
            Return dt
        End Function

        Public Function GetShopByRegion(ByVal RegionID As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            dt = lnq.GetDataList("active = 'Y' and region_id = '" & RegionID & "' ", "", trans.Trans)
            dt.TableName = "GetShopByRegion"
            trans.CommitTransaction()

            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If
        End Function

        Public Function GetShopByService(ByVal MasterItemID As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("id")
            ret.Columns.Add("shop_code")
            ret.Columns.Add("shop_name_th")
            ret.Columns.Add("shop_name_en")
            ret.Columns.Add("region_id")
            ret.Columns.Add("region_name_th")
            ret.Columns.Add("region_name_en")
            ret.Columns.Add("building_id")
            ret.Columns.Add("building_name_th")
            ret.Columns.Add("building_name_en")
            Dim iCount As Integer = 0
            For Each m As String In Split(MasterItemID, ",")
                If m.Trim <> "'0'" Then
                    iCount += 1
                End If
            Next

            Dim DateFrom As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim DateTo As String = DateAdd(DateInterval.Day, Convert.ToInt16(FunctionEng.GetQisDBConfig("MaxAppointmentDay")), DateTime.Now).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = "select distinct s.id,s.shop_code, s.shop_name_th,s.shop_name_en,s.region_id,isnull(s.building_id,0) building_id,"
            sql += " r.region_name_th, r.region_name_en, isnull(b.building_name_th,'') building_name_th, isnull(b.building_name_en,'') building_name_en,"
            sql += " ss.master_itemid"
            sql += " from tb_shop s"
            sql += " inner join tb_region r on r.id=s.region_id"
            sql += " left join tb_building b on b.id=s.building_id"
            sql += " inner join TB_SHOP_SERVICE_APPOINTMENT ss on s.id=ss.shop_id"
            sql += " where s.active='Y'"
            If MasterItemID <> "'0','0','0'" Then
                sql += " and ss.master_itemid in (" & MasterItemID & ")"
            End If
            sql += " and convert(varchar(8),ss.app_date,112) between '" & DateFrom & "' and '" & DateTo & "'"
            Dim shL As New Cen.TbShopCenLinqDB

            Dim sh As DataTable = shL.GetListBySql(sql, Nothing) 'FunctionEng.GetActiveShop()
            shL = Nothing

            ret = sh.Clone
            If sh.Rows.Count > 0 Then
                Dim tmpSh As New DataTable
                tmpSh = sh.DefaultView.ToTable(True, "id", "shop_code", "shop_name_th", "shop_name_en", "region_id", "building_id", "region_name_th", "region_name_en", "building_name_th", "building_name_en").Copy
                If tmpSh.Rows.Count > 0 Then
                    For Each tmpSr As DataRow In tmpSh.Rows
                        sh.DefaultView.RowFilter = "id='" & tmpSr("id") & "'"
                        If MasterItemID = "'0','0','0'" Then
                            iCount = sh.DefaultView.Count
                        End If

                        If sh.DefaultView.Count = iCount Then
                            Dim dr As DataRow = ret.NewRow
                            dr("id") = sh.DefaultView(0)("id")
                            dr("shop_code") = sh.DefaultView(0)("shop_code")
                            dr("shop_name_th") = sh.DefaultView(0)("shop_name_th")
                            dr("shop_name_en") = sh.DefaultView(0)("shop_name_en")
                            dr("region_id") = sh.DefaultView(0)("region_id")
                            dr("region_name_th") = sh.DefaultView(0)("region_name_th")
                            dr("region_name_en") = sh.DefaultView(0)("region_name_en")
                            dr("building_id") = sh.DefaultView(0)("building_id")
                            dr("building_name_th") = sh.DefaultView(0)("building_name_th")
                            dr("building_name_en") = sh.DefaultView(0)("building_name_en")
                            ret.Rows.Add(dr)
                        End If
                    Next
                End If
                tmpSh.Dispose()
            End If
            sh.Dispose()
            ret.TableName = "GetShopByService"
            Return ret
        End Function

        Public Function GetShopByService_ByDate(ByVal MasterItemID As String, ByVal strDate As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("id")
            ret.Columns.Add("shop_code")
            ret.Columns.Add("shop_name_th")
            ret.Columns.Add("shop_name_en")
            ret.Columns.Add("region_id")
            ret.Columns.Add("region_name_th")
            ret.Columns.Add("region_name_en")
            ret.Columns.Add("building_id")
            ret.Columns.Add("building_name_th")
            ret.Columns.Add("building_name_en")
            Dim iCount As Integer = 0
            For Each m As String In Split(MasterItemID, ",")
                If m.Trim <> "'0'" Then
                    iCount += 1
                End If
            Next

            Dim sql As String = "select s.id,s.shop_code, s.shop_name_th,s.shop_name_en,s.region_id,isnull(s.building_id,0) building_id,"
            sql += " r.region_name_th, r.region_name_en, isnull(b.building_name_th,'') building_name_th, isnull(b.building_name_en,'') building_name_en"
            sql += " from tb_shop s"
            sql += " inner join tb_region r on r.id=s.region_id"
            sql += " left join tb_building b on b.id=s.building_id"
            sql += " Inner Join  "
            sql += "       ("
            sql += "         select shop_id,CONVERT(varchar,app_date,103) as app_date,COUNT(master_itemid) as CountService from TB_SHOP_SERVICE_APPOINTMENT "
            sql += "         where CONVERT(varchar,app_date,103)= '" & strDate & "' and master_itemid in (" & MasterItemID & ")"
            sql += "         group by shop_id,CONVERT(varchar,app_date,103)"
            sql += "       ) as Table1"
            sql += " ON Table1.shop_id =s.id"
            sql += " where s.active='Y' and CountService=" & iCount
            Dim shL As New Cen.TbShopCenLinqDB

            Dim sh As DataTable = shL.GetListBySql(sql, Nothing) 'FunctionEng.GetActiveShop()
            shL = Nothing
            ret = sh.Clone()
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            For Each sr As DataRow In sh.Rows
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB

                Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
                shLnq = FunctionEng.GetTbShopCenLinqDB(sr("id"))
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)

                Dim shItem As New ShLinqDB.TABLE.TbItemShLinqDB
                Dim iDt As New DataTable
                '####### ตั้มแก้ให้ดึง ตั้งหมด ถ้ายังไม่มีการเลือก Service ######
                If MasterItemID = "'0','0','0'" Then
                    iDt = shItem.GetDataList("active_status='1'", "", shTrans.Trans)
                    iCount = iDt.Rows.Count
                Else
                    iDt = shItem.GetDataList("master_itemid in (" & MasterItemID & ") and active_status='1'", "", shTrans.Trans)
                End If
                If iDt.Rows.Count > 0 And iDt.Rows.Count = iCount Then
                    Dim dr As DataRow = ret.NewRow
                    dr("id") = sr("id")
                    dr("shop_code") = sr("shop_code")
                    dr("shop_name_th") = sr("shop_name_th")
                    dr("shop_name_en") = sr("shop_name_en")
                    dr("region_id") = sr("region_id")
                    dr("region_name_th") = sr("region_name_th")
                    dr("region_name_en") = sr("region_name_en")
                    dr("building_id") = sr("building_id")
                    dr("building_name_th") = sr("building_name_th")
                    dr("building_name_en") = sr("building_name_en")
                    ret.Rows.Add(dr)
                End If
                shTrans.CommitTransaction()
                iDt.Dispose()
            Next
            trans.CommitTransaction()
            ret.TableName = "GetShopByService"
            Return ret
        End Function

        Public Function GetItemIDByMasterID(ByVal vMasterID As String, ByVal ShopID As Long) As Long
            Dim ret As Long = 0
            Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            shLnq = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
            If shTrans.Trans IsNot Nothing Then
                Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                Dim dt As New DataTable
                dt = sLnq.GetDataList("master_itemid='" & vMasterID & "'", "", shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    ret = Convert.ToInt64(dt.Rows(0)("id"))
                End If
                dt.Dispose()
                sLnq = Nothing
            End If
            trans.CommitTransaction()
            shLnq = Nothing

            Return ret
        End Function

        Public Function GetServiceAtShop(ByVal ShopID As String) As String
            Dim ret As String = ""
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(shLnq.ID, "Engine.AppointmentENG.GetServiceAtShop")
            'shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME)
            Dim dt As New DataTable

            If shTrans.Trans IsNot Nothing Then
                Dim sql As String = "select id, item_code, item_name, item_name_th "
                sql += " from tb_item "
                sql += " where active_status= '1' "
                sql += " order by item_order "

                Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                dt = lnq.GetListBySql(sql, shTrans.Trans)
                shTrans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim tmp As String = dr("id") & "|" & dr("item_code") & "|" & dr("item_name") & "|" & dr("item_name_th")
                        If ret = "" Then
                            ret = tmp
                        Else
                            ret += "#" & tmp
                        End If
                    Next
                End If
            End If

            Return ret
        End Function

        Public Function GetMaster_ItemidServiceByShop(ByVal ShopID As String, ByVal Master_Itemid As String) As String
            Dim ret As String = ""
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(Val(ShopID & ""))
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(shLnq.ID, "Engine.AppointmentENG.GetMaster_ItemidServiceByShop")
            'shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME)
            Dim dt As New DataTable

            If shTrans.Trans IsNot Nothing Then
                Dim sql As String = "select id,master_itemid, item_code, item_name, item_name_th "
                sql += " from tb_item "
                sql += " where active_status= '1' and master_itemid in(" & Master_Itemid & ") and master_itemid is not null"
                sql += " order by item_order "

                Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                dt = lnq.GetListBySql(sql, shTrans.Trans)
                shTrans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim tmp As String = dr("master_itemid")
                        If ret = "" Then
                            ret = tmp
                        Else
                            ret += "," & tmp
                        End If
                    Next
                End If
            End If

            Return ret
        End Function

        Public Function GetServiceAgent_ByDate(ByVal NotInID As String, ByVal strDateFrom As String, ByVal strDateTo As String) As DataTable
            Try
                'If objConn.State = ConnectionState.Closed Then objConn.Open()
                Dim strSql As String = ""

                strSql = "select id"
                strSql &= ",Item_Name_th"
                strSql &= ",Item_Name"
                strSql &= ",Item_Order"
                strSql &= " From (
                strSql &= "       Select  ITM.ID,ITM.Item_Name_Th,ITM.Item_Name,ITM.Item_Order"
                strSql &= "       From TB_Item ITM"
                strSql &= "       Inner Join  "
                strSql &= "       ("
                strSql &= "         select master_itemid,CONVERT(varchar,app_date,103) as app_date from TB_SHOP_SERVICE_APPOINTMENT "
                strSql &= "         where CONVERT(varchar(8),app_date,112) between '" & strDateFrom & "' and '" & strDateTo & "'"
                strSql &= "         group by master_itemid,CONVERT(varchar,app_date,103)"
                strSql &= "       ) as Table1"
                strSql &= "       ON Table1.master_itemid =ITM.id"
                strSql &= "       Where 1=1 "
                strSql &= "       And ITM.active_status= '1' "
                strSql &= "       and ITM.id not in (" & NotInID & ") ) a "
                strSql &= "       order by item_order"
                Return FunctionEng.GetDatatable(strSql)
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function

        Public Function GetServiceAgent_ByDateAndShop(ByVal NotInID As String, ByVal strDateFrom As String, ByVal strDateTo As String, ByVal strShopId As String) As DataTable
            Try
                'If objConn.State = ConnectionState.Closed Then objConn.Open()
                Dim strSql As String = ""

                strSql = "select id"
                strSql &= ",Item_Name_th"
                strSql &= ",Item_Name"
                strSql &= ",Item_Order"
                strSql &= " From (
                strSql &= "       Select  ITM.ID,ITM.Item_Name_Th,ITM.Item_Name,ITM.Item_Order"
                strSql &= "       From TB_Item ITM"
                strSql &= "       Inner Join  "
                strSql &= "       ("
                strSql &= "         select master_itemid,CONVERT(varchar,app_date,103) as app_date from TB_SHOP_SERVICE_APPOINTMENT "
                strSql &= "         where CONVERT(varchar(8),app_date,112) between '" & strDateFrom & "' and '" & strDateTo & "' and shop_id=" & strShopId
                strSql &= "         group by master_itemid,CONVERT(varchar,app_date,103)"
                strSql &= "       ) as Table1"
                strSql &= "       ON Table1.master_itemid =ITM.id"
                strSql &= "       Where 1=1 "
                strSql &= "       And ITM.active_status= '1' "
                strSql &= "       and ITM.id not in (" & NotInID & ") ) a "
                strSql &= "       order by item_order"
                Return FunctionEng.GetDatatable(strSql)
            Catch ex As Exception
                Return New DataTable
            End Try
        End Function
      

        Public Function GetShopAllList() As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            dt = lnq.GetDataList("active = 'Y'", "", trans.Trans)
            dt.TableName = "GetShopAllList"
            trans.CommitTransaction()

            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        End Function


        Private Function ValidInsert(ByVal para As pSh.TbAppointmentCustomerShParaDB) As Boolean
            Dim ret As Boolean = True
            If para.APP_DATE.Value.Year = 1 Then
                _err = "Please Select Reservation Date"
                ret = False
            ElseIf para.CUSTOMER_ID.Trim = "" Then
                _err = "Please Input Mobile No"
                ret = False
            ElseIf para.START_SLOT.Value.Year = 1 Then
                _err = "Please Select Start Time"
                ret = False
            ElseIf para.APPOINTMENT_CHANNEL.ToString.Trim = "" Then
                _err = "Please Input Appointment Channel"
                ret = False
            ElseIf para.APPOINTMENT_CHANNEL.ToString > "3" Or para.APPOINTMENT_CHANNEL.ToString <= "0" Then
                _err = "Invalid Appointment Channel"
                ret = False
            End If

            Return ret
        End Function

        Private Sub CreateAppointmentNotifyJoblist(ByVal ShopID As Long, ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal AppointmentChannel As String, ByVal PreferLang As String, ByVal CustomerEmail As String)
            Dim SmsTime() As String = Split(Engine.Common.FunctionEng.GetQisDBConfig("AppointmentSMSNotifyTime"), "-")
            Dim SmsTimeFrom() As String = Split(SmsTime(0), ":")
            Dim SmsTimeTo() As String = Split(SmsTime(1), ":")
            Dim BeforeHours1 As Integer = 24    'ล่วงหน้า 24 ชั่วโมง
            Dim BeforeHours2 As Integer = 30    'ล่วงหน้า 30 นาที

            Dim Time1 As DateTime = DateAdd(DateInterval.Hour, (0 - BeforeHours1), StartSlot)
            Dim Alert1 As String = "N"
            If Time1.ToString("HH:mm", New CultureInfo("en-US")) < SmsTime(0) Then
                Time1 = New DateTime(Time1.Year, Time1.Month, Time1.Day, CInt(SmsTimeFrom(0)), CInt(SmsTimeFrom(1)), 0)
                Alert1 = "Y"
            End If
            If Time1.ToString("HH:mm", New CultureInfo("en-US")) > SmsTime(1) Then
                Time1 = New DateTime(Time1.Year, Time1.Month, Time1.Day, CInt(SmsTimeTo(0)), CInt(SmsTimeTo(1)), 0)
                Alert1 = "Y"
            End If

            Dim Time2 As DateTime = DateAdd(DateInterval.Minute, (0 - BeforeHours2), StartSlot)
            Dim Alert2 As String = "N"
            If Time2.ToString("HH:mm", New CultureInfo("en-US")) < SmsTime(0) Then
                Time2 = New DateTime(Time2.Year, Time2.Month, Time2.Day, CInt(SmsTimeFrom(0)), CInt(SmsTimeFrom(1)), 0)
                Alert2 = "Y"
            End If
            If Time2.ToString("HH:mm", New CultureInfo("en-US")) > SmsTime(1) Then
                Time2 = New DateTime(Time2.Year, Time2.Month, Time2.Day, CInt(SmsTimeTo(0)), CInt(SmsTimeTo(1)), 0)
                Alert2 = "Y"
            End If

            Dim jLnq As New CenLinqDB.TABLE.TbNotifyJoblistCenLinqDB
            jLnq.SHOP_ID = ShopID
            jLnq.MOBILE_NO = MobileNo
            jLnq.APPOINTMENT_TIME = StartSlot
            jLnq.APPOINTMENT_CHANNEL = AppointmentChannel
            jLnq.SMS_TIME1 = Time1
            If StartSlot.Date = Today.Date Then
                jLnq.SMS_ALERT1 = "Y"   'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง SMS ล่วงหน้า 1 วัน
            Else
                jLnq.SMS_ALERT1 = Alert1
            End If

            Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShopID, "Appointment.CreateAppointmentNotifyJoblist")
            jLnq.SMS_MSG1 = CreateNotifyMsg1Day(StartSlot, PreferLang.Trim, shTrans, shLnq)
            jLnq.SMS_TIME2 = Time2
            jLnq.SMS_ALERT2 = Alert2
            jLnq.SMS_MSG2 = CreateNotifyMsg30Min(StartSlot, PreferLang.Trim, shTrans, shLnq)

            If CustomerEmail.Trim <> "" Then
                jLnq.CUSTOMER_EMAIL = CustomerEmail
                jLnq.EMAIL_TIME1 = Time1
                If StartSlot.Date = Today.Date Then
                    jLnq.EMAIL_ALERT1 = "Y"     'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง Mail ล่วงหน้า 1 วัน
                Else
                    'jLnq.EMAIL_ALERT1 = Alert1
                    jLnq.EMAIL_ALERT1 = "Y"
                End If
                jLnq.EMAIL_MSG1 = CreateNotifyMsg1Day(StartSlot, PreferLang.Trim, shTrans, shLnq)
                jLnq.EMAIL_TIME2 = Time2
                'jLnq.EMAIL_ALERT2 = Alert2
                jLnq.EMAIL_ALERT2 = "Y"
                jLnq.EMAIL_MSG2 = CreateNotifyMsg30Min(StartSlot, PreferLang.Trim, shTrans, shLnq)
            End If

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                If jLnq.InsertData("AppoinementENG.CreateNotifyJob", trans.Trans) = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("AppointmentENG.CreateAppointmentNotifyJoblist", jLnq.ErrorMessage)
                End If
            End If
        End Sub

        Private Function CreateNotifyMsg30Min(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As Cen.TbShopCenLinqDB) As String
            Dim ret As String = ""
            Dim ShopName As String = ""
            If InStr(PreferLang.ToUpper, "THAI") > 0 Then
                ShopName = FunctionEng.GetShopConfig("s_name_th", shTrans)
                ret += "คุณมีนัดรับบริการที่ " & ShopName & " ในอีก 30 นาที"
                ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " นาที ขอบคุณค่ะ"
            Else
                ShopName = FunctionEng.GetShopConfig("s_name_en", shTrans)
                ret += "You have an appointment at " & ShopName
                ret += " in 30-minute time."
                ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " mins before the appointment. Thank you."
            End If

            Return ret
        End Function

        Private Function CreateNotifyMsg1Day(ByVal AppointmentTime As DateTime, ByVal PreferLang As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As Cen.TbShopCenLinqDB) As String
            Dim ret As String = ""
            Dim ShopName As String = ""
            If InStr(PreferLang.ToUpper, "THAI") > 0 Then
                ShopName = shLnq.SHOP_NAME_TH 'FunctionEng.GetShopConfig("s_name_th", shTrans)
                ret += "พรุ่งนี้คุณมีนัดที่ " & ShopName
                ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น."
                ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " นาที ขอบคุณค่ะ"
            Else
                ShopName = shLnq.SHOP_NAME_EN 'FunctionEng.GetShopConfig("s_name_en", shTrans)
                ret += "You have appointment at " & ShopName
                ret += " tomorrow at " & AppointmentTime.ToString("HH:mm")
                ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans)) & " mins before the appointment time. Thank you."
            End If

            Return ret
        End Function

        Public Function InsertAppointment(ByVal ShopID As String, ByVal paraList() As pSh.TbAppointmentCustomerShParaDB, ByVal PreferLang As String, ByVal AppointmentChannel As String, Optional ByVal IsCreateActivity As Boolean = True) As InsertAppointmentResultPara
            Dim resPara As New InsertAppointmentResultPara
            Dim MobileNo As String = paraList(0).CUSTOMER_ID
            Dim StartTime As DateTime = paraList(0).START_SLOT.Value
            Dim lnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(ShopID)

            'Save Appointment Job
            Dim pJob As New CenParaDB.Appointment.SaveAppointmentJobPara
            Dim pPara As New CenParaDB.TABLE.TbAppointmentJobCenParaDB
            pPara.SHOP_ABB = lnq.SHOP_ABB
            pPara.MOBILE_NO = MobileNo
            pPara.APP_DATE = DateTime.Now
            pPara.START_SLOT = StartTime
            pPara.APPOINTMENT_CHANNEL = AppointmentChannel
            pPara.ACTIVE_STATUS = Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment

            Dim ap As New Appointment.AppointmentENG
            pJob = ap.SaveAppointmentJob(pPara)
            pPara = Nothing
            ap = Nothing

            If pJob.SaveResult = True Then
                Dim ServiceID As String = ""
                Dim ServiceName As String = ""
                Dim AppDate As DateTime = DateTime.Now
                For Each para In paraList
                    If ValidInsert(para) Then
                        MobileNo = para.CUSTOMER_ID
                        StartTime = para.START_SLOT.Value
                        'Insert Appointment At shop
                        Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(lnq.ID, "Enging.AppointmentENG.InsertAppointment")
                        'shTrans.CreateTransaction(lnq.SHOP_DB_SERVER, lnq.SHOP_DB_USERID, lnq.SHOP_DB_PWD, lnq.SHOP_DB_NAME)
                        Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
                        shLinq.APP_DATE = AppDate
                        shLinq.CAPACITY = para.CAPACITY
                        shLinq.ITEM_ID = para.ITEM_ID
                        shLinq.CUSTOMER_ID = para.CUSTOMER_ID
                        shLinq.START_SLOT = para.START_SLOT
                        'shLinq.END_SLOT = para.END_SLOT
                        shLinq.ACTIVE_STATUS = para.ACTIVE_STATUS
                        shLinq.APPOINTMENT_CHANNEL = para.APPOINTMENT_CHANNEL
                        shLinq.CUSTOMER_EMAIL = para.CUSTOMER_EMAIL
                        shLinq.APPOINTMENT_JOB_ID = pJob.AppointmentJobPara.ID

                        Dim ret As Boolean = shLinq.InsertData(para.CUSTOMER_ID, shTrans.Trans)
                        resPara.RESPONSE = ret
                        If ret = False Then
                            resPara.ErrorMessage = shLinq.ErrorMessage
                            shTrans.RollbackTransaction()
                        Else
                            resPara.ErrorMessage = ""

                            If ServiceID = "" Then
                                ServiceID = para.ITEM_ID
                            Else
                                ServiceID += "," & para.ITEM_ID
                            End If

                            Dim shILnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            Dim shDt As New DataTable
                            shDt = shILnq.GetListBySql("select id, item_name from tb_item where id='" & para.ITEM_ID & "'", shTrans.Trans)
                            If shDt.Rows.Count > 0 Then
                                If ServiceName = "" Then
                                    ServiceName = shDt.Rows(0)("item_name")
                                Else
                                    ServiceName += ", " & shDt.Rows(0)("item_name")
                                End If
                            End If
                            shDt.Dispose()
                            shTrans.CommitTransaction()

                            shILnq = Nothing
                        End If
                    Else
                        resPara.RESPONSE = False
                        resPara.ErrorMessage = _err
                    End If
                Next

                If resPara.ErrorMessage = "" Then
                    AddTimeSlotCapacity(ServiceID, StartTime, ShopID)
                    SendSMSConfirm(MobileNo, ServiceID, paraList(0).START_SLOT.Value, PreferLang, lnq)
                    CreateAppointmentNotifyJoblist(ShopID, MobileNo, StartTime, AppointmentChannel, PreferLang, paraList(0).CUSTOMER_EMAIL)
                    If paraList(0).CUSTOMER_EMAIL.Trim <> "" Then
                        SendEmailConfirm(paraList(0).CUSTOMER_EMAIL.Trim, ServiceID, MobileNo, StartTime, PreferLang, ShopID)
                    End If


                    If IsCreateActivity = True Then
                        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                        Dim gw As New GateWayServiceENG
                        gw.SiebelCreateActivity(MobileNo, paraList(0).START_SLOT.Value, ServiceName, paraList(0).APPOINTMENT_CHANNEL, lnq, trans)
                        gw = Nothing
                        trans.CommitTransaction()
                    End If
                End If
            Else
                resPara.RESPONSE = False
                resPara.ErrorMessage = pJob.ErrorMessage
            End If

            Return resPara
        End Function

        Private Function AddTimeSlotCapacity(ByVal ChooseService As String, ByVal SlotDateTime As DateTime, ByVal ShopID As Long) As Boolean
            Dim res As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim sql As String = ""
                Dim dt As New DataTable
                Dim AllService() As String = Split(ChooseService, ",")
                Dim CountService As Int32 = AllService.Length
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
                If shTrans.Trans IsNot Nothing Then
                    Dim kBeforeClose As Double = FunctionEng.GetShopConfig("k_before_close", shTrans)
                    Dim StrDateTime As String = SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                    Dim slSql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & StrDateTime & "') = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & StrDateTime & "',app_date)=0"
                    Dim slLnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
                    Dim slDT As DataTable = slLnq.GetListBySql(slSql, shTrans.Trans)
                    If slDT.Rows.Count > 0 Then
                        Dim Slot As Long = slDT.Rows(0)("slot")
                        Dim lnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                        Dim whText As String = "DATEDIFF(d,app_date,'" & StrDateTime & "') = 0 and in_use < capacity and CONVERT(varchar(5),slot_time,114) between '" & SlotDateTime.ToString("HH:mm") & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Minute.ToString.PadLeft(2, "0") & "'"
                        dt = lnq.GetDataList(whText, "", shTrans.Trans)
                        If dt.Rows.Count = CountService Then
                            'Dim ret As Boolean = False
                            For j As Int32 = 0 To dt.Rows.Count - 1
                                Dim asLnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                                asLnq.GetDataByPK(dt.Rows(j).Item("id"), shTrans.Trans)
                                asLnq.IN_USE = asLnq.IN_USE + 1
                                res = asLnq.UpdateByPK(0, shTrans.Trans)
                                If res = False Then
                                    Exit For
                                End If
                            Next
                        Else
                            res = False
                        End If
                        If res = True Then
                            shTrans.CommitTransaction()
                        Else
                            shTrans.RollbackTransaction()
                        End If
                    End If
                End If
            End If

            Return res
        End Function

        Private Function SendSMSConfirm(ByVal MobileNo As String, ByVal ServiceID As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As SMSResponsePara
            Dim gw As New GateWayServiceENG
            Dim ret As New SMSResponsePara
            ret = gw.SendSMS(MobileNo, CreateSMSConfirm(ServiceID, MobileNo, AppointmentTime, PreLang, ShLnq))
            gw = Nothing
            Return ret
        End Function

        Private Function CreateSMSConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
            Dim trans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShLnq.ID, "Engine.AppointmentENG.CreateSMSConfirm")
            'trans.CreateTransaction(ShLnq.SHOP_DB_SERVER, ShLnq.SHOP_DB_USERID, ShLnq.SHOP_DB_PWD, ShLnq.SHOP_DB_NAME)
            Dim ServiceName As String = ""
            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
            Dim sDt As New DataTable
            sDt = sLnq.GetDataList("id in (" & ServiceID & ")", "item_time,item_wait,item_order", trans.Trans)
            For Each sDr As DataRow In sDt.Rows
                If InStr(PreLang.ToUpper(), "THAI") > 0 Then
                    If ServiceName = "" Then
                        ServiceName += sDr("ITEM_NAME_TH").ToString
                    Else
                        ServiceName += "," & sDr("ITEM_NAME_TH").ToString
                    End If
                Else
                    If ServiceName = "" Then
                        ServiceName += sDr("ITEM_NAME").ToString
                    Else
                        ServiceName += "," & sDr("ITEM_NAME").ToString
                    End If
                End If
            Next
            sLnq = Nothing
            sDt.Dispose()

            Dim ret As String = ""
            Dim ShopName As String = ""
            Dim TimeBefore As String = DateAdd(DateInterval.Hour, 0 - Convert.ToDouble(FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")), AppointmentTime).ToString("HH:mm")
            If InStr(PreLang.ToUpper(), "THAI") > 0 Then
                ShopName = ShLnq.SHOP_NAME_TH 'FunctionEng.GetShopConfig("s_name_th", trans)
                ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " " & AppointmentTime.ToString("dddd dMMMyy", New System.Globalization.CultureInfo("th-TH"))
                ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. หากเปลี่ยน/ยกเลิก กรุณาแจ้ง"
                ret += "ภายในเวลา" & TimeBefore & "น."
            Else
                'Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(FunctionEng.GetShopConfig("k_cancel", trans)), AppointmentTime).ToString("HH:mmtt")
                ShopName = ShLnq.SHOP_NAME_EN 'FunctionEng.GetShopConfig("s_name_en", trans)
                ret += "You have an appointment for " & ServiceName
                ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy, HH:mm", New System.Globalization.CultureInfo("en-US")) & "."
                ret += " To change/cancel,please make changes " & TimeBefore & " in advance."
            End If
            trans.CommitTransaction()

            Return ret
        End Function

        Public Function SendEmailConfirm(ByVal MailTo As String, ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As Boolean
            Dim gw As New GateWayServiceENG
            Dim PicturePath As String = FunctionEng.GetQisDBConfig("AppointMailPicturePath") '"D:\Application\MailPicture"
            If PicturePath.Trim = "" Then
                PicturePath = "D:\Application\MailPicture"
            End If
            Dim file() As String = {PicturePath & "\logo.gif", PicturePath & "\bg_leave.jpg", PicturePath & "\aunjai_shop.png"}

            Dim ret As Boolean = False
            Dim MailContent As String = CreateMailConfirm(ServiceID, MobileNo, AppointmentTime, PreLang, ShopID)
            If MailContent.Trim <> "" Then
                ret = gw.SendEmailAttFile(MailTo, "Confirm Appointment", MailContent, file)
            End If
            gw = Nothing
            Return ret
        End Function

        Private Function CreateMailConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As String
            Dim ret As String = ""

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(shLnq.ID, "Engine.AppointmentENG.CreateMailConfirm")

            Dim SlotDT As New DataTable
            Dim SlotLnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
            SlotDT = SlotLnq.GetDataList("convert(varchar(8),app_date,112)='" & AppointmentTime.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'", "", shTrans.Trans)
            If SlotDT.Rows.Count > 0 Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerCenLinqDB
                Dim cPara As New CenParaDB.TABLE.TbCustomerCenParaDB
                cPara = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)

                Dim ServiceName As String = ""
                Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                Dim sDt As New DataTable
                Dim sSql As String = "select item_name_th,item_name,item_time,item_wait,item_order "
                sSql += " from tb_item"
                sSql += " where master_itemid in (" & ServiceID & ")"
                sSql += " order by item_time,item_wait,item_order"
                sDt = sLnq.GetListBySql(sSql, shTrans.Trans) 'sLnq.GetDataList("master_itemid in (" & ServiceID & ")", "item_time,item_wait,item_order", shTrans.Trans)
                Dim i As Integer = 0
                For Each sDr As DataRow In sDt.Rows
                    If InStr(PreLang.ToUpper(), "THAI") > 0 Then
                        If ServiceName = "" Then
                            ServiceName = (i + 1).ToString & "." & sDr("ITEM_NAME_TH").ToString
                        Else
                            ServiceName += ", " & (i + 1).ToString & "." & sDr("ITEM_NAME_TH").ToString
                        End If
                    Else
                        If ServiceName = "" Then
                            ServiceName += (i + 1).ToString & "." & sDr("ITEM_NAME").ToString
                        Else
                            ServiceName += ", " & (i + 1).ToString & "." & sDr("ITEM_NAME").ToString
                        End If
                    End If
                    i += 1
                Next
                sLnq = Nothing
                sDt.Dispose()

                If ServiceName.IndexOf("2.") = -1 Then
                    ServiceName = ServiceName.Replace("1.", "")
                End If
                'Config Information
                Dim kBefore As Integer = Convert.ToInt64(FunctionEng.GetShopConfig("k_before", shTrans))
                Dim NoShowQty As Integer = FunctionEng.GetQisDBConfig("AppointmentNoShowQty")  'ผิดนัดเป็นจำนวนกี่ครั้ง
                'Dim WithinDay As Integer = FunctionEng.GetQisDBConfig("AppointmentWithinDay")  'ภายในกี่วัน
                Dim NoBookDay As Integer = FunctionEng.GetQisDBConfig("NoBookDay") 'จะถูกระงับการจองไปนานกี่วัน

                Dim CustomerName As String = ""
                Dim txtMobile As String = ""
                Dim TxtAppoontment As String = ""
                Dim lblDate As String = ""
                Dim txtDate As String = ""
                Dim lblTime As String = ""
                Dim txtTime As String = ""
                Dim lblShopName As String = ""
                Dim txtShopName As String = ""
                Dim lblServiceName As String = ""
                Dim lblRegister As String = ""
                Dim lblRedIssue As String = ""
                Dim lblNoShow As String = ""
                If InStr(PreLang.ToUpper(), "THAI") > 0 Then
                    If cPara.F_NAME <> "" And cPara.L_NAME <> "" Then
                        CustomerName = "เรียนคุณ " & cPara.F_NAME & " " & cPara.L_NAME
                    End If
                    txtMobile = "ผู้ใช้บริการโทรศัพท์หมายเลข " & MobileNo
                    TxtAppoontment = "คุณมีรายการนัดหมายล่วงหน้า เพื่อติดต่อรับบริการจาก AIS Shop โดยมีรายละเอียดดังนี้:"
                    lblDate = "วัน "
                    txtDate = AppointmentTime.ToString("dddd", New Globalization.CultureInfo("th-TH")) & " ที่  " & AppointmentTime.ToString("dd MMMM yyyy", New Globalization.CultureInfo("th-TH"))
                    lblTime = "เวลา"
                    txtTime = AppointmentTime.ToString("HH:mm") & " - " & DateAdd(DateInterval.Minute, Convert.ToInt64(SlotDT.Rows(0)("slot")) * i, AppointmentTime).ToString("HH:mm") & "น."
                    lblShopName = "สถานที่"
                    txtShopName = shLnq.SHOP_NAME_TH 'FunctionEng.GetShopConfig("s_name_th", shTrans)
                    lblServiceName = "บริการ"
                    lblRegister = "กรุณาเช็คอิน รับคิวนัดหมายล่วงหน้า ที่ AIS Shop ที่ระบุ ก่อนถึงเวลานัด " & kBefore & " นาที"
                    lblRedIssue = "หากไม่มาติดต่อขอรับบริการภายในเวลาที่นัดหมาย บริษัทขอสงวนสิทธิ์ยกเลิกการนัด<br />โดยไม่ต้องแจ้งให้ทราบล่วงหน้า ผู้ใช้บริการต้องกดบัตรคิวใหม่เพื่อรับบริการ"
                    lblNoShow = "*กรณีไม่มาติดต่อขอรับบริการหลังจากมีการนัดหมายล่วงหน้าเกิน " & NoShowQty & " ครั้ง คุณจะไม่สามารถทำการนัดหมายล่วงหน้าได้อีก<br />ภายในระยะเวลา " & NoBookDay & " วัน นับจากวันที่ที่มีการนัดหมายครั้งล่าสุด*"
                Else
                    If cPara.F_NAME <> "" And cPara.L_NAME <> "" Then
                        CustomerName = "Dear, " & cPara.F_NAME & " " & cPara.L_NAME
                    End If
                    txtMobile = "Mobile Number :  " & MobileNo
                    TxtAppoontment = " Appointment Reminder with AIS Shop "
                    lblDate = "Day "
                    txtDate = AppointmentTime.ToString("dddd", New Globalization.CultureInfo("en-US")) & " (" & AppointmentTime.ToString("MMMM dd", New Globalization.CultureInfo("en-US")) & "," & AppointmentTime.ToString("yyyy", New Globalization.CultureInfo("en-US")) & ")"
                    lblTime = "Time"
                    txtTime = AppointmentTime.ToString("HH:mmtt") & " - " & DateAdd(DateInterval.Minute, Convert.ToInt64(SlotDT.Rows(0)("slot")) * i, AppointmentTime).ToString("HH:mmtt")
                    lblShopName = "Shop Name"
                    txtShopName = shLnq.SHOP_NAME_EN ' FunctionEng.GetShopConfig("s_name_en", shTrans)
                    lblServiceName = "Service"
                    'lblRegister = "กรุณาเช็คอิน รับคิวนัดหมายล่วงหน้า ที่ AIS Shop ที่ระบุ ก่อนถึงเวลานัด " & kCancel
                    'lblRedIssue = "หากไม่มาติดต่อขอรับบริการภายในเวลาที่นัดหมาย บริษัทขอสงวนสิทธิ์ยกเลิกการนัด<br />โดยไม่ต้องแจ้งให้ทราบล่วงหน้า ผู้ใช้บริการต้องกดบัตรคิวใหม่เพื่อรับบริการ"
                    'lblNoShow = "*กรณีไม่มาติดต่อขอรับบริการหลังจากมีการนัดหมายล่วงหน้าเกิน " & NoShowQty & " ครั้ง คุณจะไม่สามารถทำการนัดหมายล่วงหน้าได้อีก<br />ภายในระยะเวลา " & NoBookDay & " วัน นับจากวันที่ที่มีการนัดหมายครั้งล่าสุด*"
                    lblRegister = "Please arrive at AIS Shop " & kBefore & " minutes before your appointment time."
                    lblRedIssue = "In case you do not show up by the appointment date and time,<br />we reserve the right to skip your queue without prior notice,<br/>and you can take a new queue ticket at AIS Shop."
                    lblNoShow = "* In case you do not show up after making appointment, you will not be able<br />to make appointment for your next visit."
                End If

                ret += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>"
                ret += "    <tr>"
                ret += "        <td width='90%' alight='left'>" & CustomerName & "</td>"
                ret += "        <td width='10%' alight='right'><IMG SRC='cid:ATTIMG1' width='138' height='62' /></td>"
                ret += "    </tr>"
                ret += "    <tr>"
                ret += "        <td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtMobile & "</td>"
                ret += "    </tr>"
                ret += "    <tr><td colspan='2'>&nbsp;</td></tr>"
                ret += "    <tr>"
                ret += "        <td colspan='2'>" & TxtAppoontment & "</td>"
                ret += "    </tr>"
                ret += "    <tr>"
                ret += "        <td colspan='2'>"
                ret += "            <table border='0' cellpadding='0' cellspacing='0' width='100%'>"
                ret += "                <tr>"
                ret += "                    <td width='20%'>" & lblDate & "</td>"
                ret += "                    <td width='80%'>: " & txtDate & "</td>"
                ret += "                </tr>"
                ret += "                <tr>"
                ret += "                    <td >" & lblTime & "</td>"
                ret += "                    <td >: " & txtTime & "</td>"
                ret += "                </tr>"
                ret += "                <tr>"
                ret += "                    <td >" & lblShopName & "</td>"
                ret += "                    <td >: " & txtShopName & "</td>"
                ret += "                </tr>"
                ret += "                <tr>"
                ret += "                    <td >" & lblServiceName & "</td>"
                ret += "                    <td >: " & ServiceName & "</td>"
                ret += "                </tr>"
                ret += "            </table>"
                ret += "        </td>"
                ret += "    </tr>"
                ret += "    <tr><td colspan='2'>&nbsp;</td></tr>"
                ret += "    <tr>"
                ret += "        <td colspan='2'>"
                ret += "            <table border='0' cellpadding='0' cellspacing='0' width='100%' height='150' background=" & Chr(34) & "cid:ATTIMG2" & Chr(34) & " style='background-repeat:no-repeat;background-size:273px 150px;'  >"
                ret += "                <tr>"
                ret += "                    <td colspan='2'>"
                ret += "                        <font color='red' size='5px'><u><i>" & lblRegister & "</i></u></font>"
                ret += "                    </td>"
                ret += "                </tr>"
                ret += "                <tr>"
                ret += "                    <td width='80%' valign='top' >"
                ret += "                        <font color='red'>" & lblRedIssue & "</font>"
                ret += "                    </td>"
                ret += "                    <td width='20%' ><IMG SRC='cid:ATTIMG3'  height='150' /></td>"
                ret += "                </tr>"
                ret += "                <tr>"
                ret += "                    <td colspan='2'>" & lblNoShow & "</td>"
                ret += "                </tr>"
                ret += "            </table>"
                ret += "        </td>"
                ret += "    </tr>"
                ret += "</table>"

                shTrans.CommitTransaction()
                lnq = Nothing
                cPara = Nothing
            End If

            Return ret
        End Function

        'Private Function CreateMailConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As String
        '    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        '    Dim trans As New ShLinqDB.Common.Utilities.TransactionDB
        '    trans.CreateTransaction(ShLnq.SHOP_DB_SERVER, ShLnq.SHOP_DB_USERID, ShLnq.SHOP_DB_PWD, ShLnq.SHOP_DB_NAME)
        '    Dim lnq As New ShLinqDB.TABLE.TbCustomerShLinqDB

        '    Dim ServiceName As String = ""
        '    Dim SerIDList() As String = Split(ServiceID, ",")
        '    For Each sID As Long In SerIDList
        '        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
        '        sLnq.GetDataByPK(Convert.ToInt64(sID), trans.Trans)
        '        If sLnq.ID <> 0 Then
        '            If InStr(PreLang.ToUpper(), "THAI") > 0 Then
        '                If ServiceName = "" Then
        '                    ServiceName += sLnq.ITEM_NAME_TH
        '                Else
        '                    ServiceName += "," & sLnq.ITEM_NAME_TH
        '                End If
        '            Else
        '                If ServiceName = "" Then
        '                    ServiceName += sLnq.ITEM_NAME
        '                Else
        '                    ServiceName += "," & sLnq.ITEM_NAME
        '                End If
        '            End If
        '        End If
        '    Next
        '    Dim ret As String = ""
        '    Dim ShopName As String = ""

        '    If InStr(PreLang.ToUpper(), "THAI") > 0 Then
        '        Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(FunctionEng.GetShopConfig("k_cancel", trans)), AppointmentTime).ToString("HH:mm")
        '        ShopName = FunctionEng.GetShopConfig("s_name_th", trans)
        '        ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " " & AppointmentTime.ToString("dddd dMMMyy", New System.Globalization.CultureInfo("th-TH"))
        '        ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. หากเปลี่ยน/ยกเลิก กรุณาแจ้งAIS"
        '        ret += " ภายในเวลา" & TimeBefore & "น."
        '    Else
        '        Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(FunctionEng.GetShopConfig("k_cancel", trans)), AppointmentTime).ToString("HH:mmtt")
        '        ShopName = FunctionEng.GetShopConfig("s_name_en", trans)
        '        ret += "You have an appointment for " & ServiceName
        '        ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy, hh:mmtt", New System.Globalization.CultureInfo("en-US")) & ","
        '        ret += " To change/cancel,please make changes " & TimeBefore & " in advance."
        '    End If
        '    trans.CommitTransaction()

        '    Return ret
        'End Function

        Public Function GetCustomerProfile(ByVal MobileNo As String, ByVal SessionID As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB

            Dim gw As New GateWayServiceENG
            ret = gw.GetCustomerProfile(MobileNo, SessionID)
            gw = Nothing
            Return ret
        End Function

        Public Function GetCustomerProfileFromDB(ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim lnq As New Cen.TbCustomerCenLinqDB
            Dim p As New pCen.TbCustomerCenParaDB
            p = lnq.GetParameterByMobileNo(MobileNo, Nothing)
            lnq = Nothing
            Return p
        End Function


        Public Function GetActiveAppointment(ByVal MobileNo As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Try
                Dim lnq As New Cen.TbAppointmentJobCenLinqDB
                Dim dt As New DataTable
                Dim wh As String = "mobile_no='" & MobileNo & "' "
                wh += " and active_status in ('" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "','" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "') "
                wh += " and CONVERT(varchar(16),start_slot,120) >= '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
                dt = lnq.GetDataList(wh, "", trans.Trans)
                If dt.Rows.Count > 0 Then
                    ret = True
                End If
                dt.Dispose()
                lnq = Nothing
            Catch ex As Exception
                ret = False
                trans.RollbackTransaction()
            End Try

            Return ret
        End Function

        'Public Function GetActiveAppointment(ByVal MobileNo As String) As Boolean
        '    Dim ret As Boolean = False
        '    Dim trans As New TransactionDB
        '    Dim lnq As New Cen.TbShopCenLinqDB
        '    Dim dt As DataTable = lnq.GetDataList("active='Y'", "", trans.Trans)
        '    trans.CommitTransaction()
        '    If dt.Rows.Count > 0 Then
        '        For Each dr As DataRow In dt.Rows
        '            Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))
        '            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.GetActiveAppointment")
        '            'shTrans.CreateTransaction(cenShLnq.SHOP_DB_SERVER, cenShLnq.SHOP_DB_USERID, cenShLnq.SHOP_DB_PWD, cenShLnq.SHOP_DB_NAME)
        '            If shTrans.Trans IsNot Nothing Then
        '                Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
        '                Dim wh As String = "CONVERT(varchar(16),start_slot,120) >= '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
        '                wh += " and customer_id = '" & MobileNo & "' "
        '                wh += " and active_status in ('" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "','" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "') "
        '                ret = shLinq.ChkDataByWhere(wh, shTrans.Trans)
        '                shLinq = Nothing
        '                If ret = True Then
        '                    Exit For
        '                End If
        '            End If
        '            cenShLnq = Nothing
        '        Next
        '    End If
        '    lnq = Nothing
        '    dt.Dispose()

        '    Return ret
        'End Function

        Private Function BuiltAppointmentList(ByVal dt As DataTable, ByVal sqlRs As String, ByVal shLinq As Sh.TbAppointmentCustomerShLinqDB, ByVal shTrans As uSh.TransactionDB, ByVal cenShLnq As Cen.TbShopCenLinqDB) As DataTable
            Dim tmpDT As DataTable = shLinq.GetListBySql(sqlRs, shTrans.Trans)
            shTrans.CommitTransaction()
            If tmpDT.Rows.Count > 0 Then
                Dim tmpStartSlot As DateTime = New DateTime(1, 1, 1)
                Dim tmpServiceID As String = ""
                Dim tmpServiceName As String = ""
                Dim tmpServiceName_Th As String = ""
                Dim tmpAppDate As DateTime = New DateTime(1, 1, 1)

                For i As Integer = 0 To tmpDT.Rows.Count - 1
                    Dim tmpDr As DataRow = tmpDT.Rows(i)
                    If Convert.IsDBNull(tmpDr("master_itemid")) = False Then tmpServiceID = tmpDr("master_itemid") Else Continue For
                    tmpServiceName = tmpDr("item_name")
                    tmpServiceName_Th = tmpDr("item_name_th")
                    If tmpAppDate.ToString <> Convert.ToDateTime(tmpDr("app_date")).ToString Then
                        Dim retDr As DataRow = dt.NewRow
                        retDr("mobile_no") = tmpDr("mobile_no")
                        retDr("slot_date") = Convert.ToDateTime(tmpDr("start_slot")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                        retDr("slot_time") = Convert.ToDateTime(tmpDr("start_slot")).ToString("HH:mm")
                        retDr("shop_id") = cenShLnq.ID
                        retDr("shop_name_en") = cenShLnq.SHOP_NAME_EN
                        retDr("shop_name_th") = cenShLnq.SHOP_NAME_TH
                        retDr("service_id") = tmpDr("master_itemid")
                        retDr("service_name_en") = tmpDr("item_name")
                        retDr("service_name_th") = tmpDr("item_name_th")
                        retDr("status") = tmpDr("status")
                        retDr("slot_datetime") = Convert.ToDateTime(tmpDr("start_slot"))
                        dt.Rows.Add(retDr)

                        tmpStartSlot = tmpDr("start_slot")
                        tmpServiceID = tmpDr("master_itemid")
                        tmpServiceName = tmpDr("item_name")
                        tmpAppDate = tmpDr("app_date")
                    Else
                        dt.Rows(dt.Rows.Count - 1)("service_id") += "," + tmpServiceID
                        dt.Rows(dt.Rows.Count - 1)("service_name_en") += "," + tmpServiceName
                        dt.Rows(dt.Rows.Count - 1)("service_name_th") += "," + tmpServiceName_th
                    End If
                Next
            End If

            Return dt
        End Function

        Public Function GetAppointmentHistory(ByVal MobileNo As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("mobile_no")
            ret.Columns.Add("slot_date")
            ret.Columns.Add("slot_time")
            ret.Columns.Add("shop_id")
            ret.Columns.Add("shop_name_en")
            ret.Columns.Add("shop_name_th")
            ret.Columns.Add("service_id")
            ret.Columns.Add("service_name_en")
            ret.Columns.Add("service_name_th")
            ret.Columns.Add("status")
            ret.Columns.Add("slot_datetime", GetType(DateTime))

            Try
                Dim sql As String = "select distinct aj.shop_abb "
                sql += " from TB_APPOINTMENT_JOB aj"
                sql += " where aj.mobile_no='" & MobileNo & "' and aj.active_status <> '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"

                Dim dt As New DataTable
                Dim lnq As New Cen.TbAppointmentJobCenLinqDB
                dt = lnq.GetListBySql(sql, Nothing)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim sLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDBByShopAbb(dr("shop_abb"))
                        Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(sLnq.ID, "AppointmentENG.GetAppointmentHistory")
                        If shTrans.Trans IsNot Nothing Then
                            Dim scLinq As New Sh.TbAppointmentCustomerShLinqDB
                            Dim sqlRs As String = "select distinct  ac.customer_id mobile_no, ac.start_slot , s.master_itemid, s.item_name, s.item_name_th, "
                            sqlRs += " case ac.active_status when '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.REGISTERED & "' "
                            sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.COMPLETED & "' "
                            sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Missed & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.MISSED & "' "
                            sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.NoShow & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.NO_SHOW & "' "
                            sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.CANCELLED & "' "
                            sqlRs += " end status, ac.app_date,s.item_time,s.item_wait,s.item_order"
                            sqlRs += " from TB_APPOINTMENT_CUSTOMER ac "
                            sqlRs += " inner join TB_ITEM s on s.id=ac.item_id "
                            sqlRs += " where ac.customer_id = '" & MobileNo & "' and ac.active_status <> '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"
                            sqlRs += " order by ac.start_slot,s.item_time,s.item_wait,s.item_order "

                            ret = BuiltAppointmentList(ret, sqlRs, scLinq, shTrans, sLnq)
                        End If
                        sLnq = Nothing
                    Next
                End If
                dt.Dispose()


            Catch ex As Exception
                ret = New DataTable
            End Try

            ret.TableName = "AppointmentHistory"
            Return ret
        End Function

        'Public Function GetAppointmentHistory(ByVal MobileNo As String) As DataTable
        '    Dim ret As New DataTable
        '    ret.Columns.Add("mobile_no")
        '    ret.Columns.Add("slot_date")
        '    ret.Columns.Add("slot_time")
        '    ret.Columns.Add("shop_id")
        '    ret.Columns.Add("shop_name_en")
        '    ret.Columns.Add("shop_name_th")
        '    ret.Columns.Add("service_id")
        '    ret.Columns.Add("service_name_en")
        '    ret.Columns.Add("service_name_th")
        '    ret.Columns.Add("status")
        '    ret.Columns.Add("slot_datetime", GetType(DateTime))

        '    Dim trans As New TransactionDB
        '    Dim lnq As New Cen.TbShopCenLinqDB
        '    Dim dt As DataTable = lnq.GetDataList("Active='Y'", "", trans.Trans)
        '    trans.CommitTransaction()
        '    If dt.Rows.Count > 0 Then
        '        For Each dr As DataRow In dt.Rows
        '            Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))

        '            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.GetAppointmentHistory")
        '            'shTrans.CreateTransaction(cenShLnq.SHOP_DB_SERVER, cenShLnq.SHOP_DB_USERID, cenShLnq.SHOP_DB_PWD, cenShLnq.SHOP_DB_NAME)
        '            If shTrans.Trans IsNot Nothing Then
        '                Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
        '                Dim sqlRs As String = "select distinct  ac.customer_id mobile_no, ac.start_slot , s.master_itemid, s.item_name, s.item_name_th, "
        '                sqlRs += " case ac.active_status when '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.REGISTERED & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.COMPLETED & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Missed & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.MISSED & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.NoShow & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.NO_SHOW & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.CANCELLED & "' "
        '                sqlRs += " end status, ac.app_date,s.item_time,s.item_wait,s.item_order"
        '                sqlRs += " from TB_APPOINTMENT_CUSTOMER ac "
        '                sqlRs += " inner join TB_ITEM s on s.id=ac.item_id "
        '                sqlRs += " where ac.customer_id = '" & MobileNo & "' and ac.active_status <> '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"
        '                sqlRs += " order by ac.start_slot,s.item_time,s.item_wait,s.item_order "


        '                ret = BuiltAppointmentList(ret, sqlRs, shLinq, shTrans, cenShLnq)
        '            End If
        '            cenShLnq = Nothing
        '        Next
        '    End If
        '    dt.Dispose()

        '    ret.TableName = "AppointmentHistory"
        '    Return ret
        'End Function

        

        Public Function GetAppointmentDesc(ByVal MobileNo As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("mobile_no")
            ret.Columns.Add("slot_date")
            ret.Columns.Add("slot_time")
            ret.Columns.Add("shop_id")
            ret.Columns.Add("shop_name_en")
            ret.Columns.Add("shop_name_th")
            ret.Columns.Add("service_id")
            ret.Columns.Add("service_name_en")
            ret.Columns.Add("service_name_th")
            ret.Columns.Add("status")
            ret.Columns.Add("slot_datetime", GetType(DateTime))

            Dim sql As String = "select distinct aj.shop_abb "
            sql += " from TB_APPOINTMENT_JOB aj"
            sql += " where CONVERT(varchar(16),aj.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            sql += " and aj.mobile_no = '" & MobileNo & "' "
            sql += " and aj.active_status in ('" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "','" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "') "

            Dim dt As New DataTable
            Dim lnq As New Cen.TbAppointmentJobCenLinqDB
            dt = lnq.GetListBySql(sql, Nothing)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDBByShopAbb(dr("shop_abb"))
                    Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.GetAppointmentDesc")
                    If shTrans.Trans IsNot Nothing Then
                        Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
                        Dim sqlRs As String = "select distinct ac.customer_id mobile_no, ac.start_slot, s.master_itemid, s.item_name, s.item_name_th, "
                        sqlRs += " case ac.active_status when '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.REGISTERED & "' "
                        sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.COMPLETED & "' "
                        sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Missed & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.MISSED & "' "
                        sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.NoShow & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.NO_SHOW & "' "
                        sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.CANCELLED & "' "
                        sqlRs += " end status, ac.app_date, s.item_time,s.item_wait,s.item_order"
                        sqlRs += " from TB_APPOINTMENT_CUSTOMER ac "
                        sqlRs += " inner join TB_ITEM s on s.id=ac.item_id"
                        sqlRs += " where CONVERT(varchar(16),ac.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
                        sqlRs += " and ac.customer_id = '" & MobileNo & "' "
                        sqlRs += " and ac.active_status in ('" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "','" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "') "
                        sqlRs += " order by s.item_time,s.item_wait,s.item_order"

                        ret = BuiltAppointmentList(ret, sqlRs, shLinq, shTrans, cenShLnq)
                    End If
                    cenShLnq = Nothing
                Next
            End If

            ret.TableName = "AppointmentDesc"
            Return ret
        End Function


        'Public Function GetAppointmentDesc(ByVal MobileNo As String) As DataTable
        '    Dim ret As New DataTable
        '    ret.Columns.Add("mobile_no")
        '    ret.Columns.Add("slot_date")
        '    ret.Columns.Add("slot_time")
        '    ret.Columns.Add("shop_id")
        '    ret.Columns.Add("shop_name_en")
        '    ret.Columns.Add("shop_name_th")
        '    ret.Columns.Add("service_id")
        '    ret.Columns.Add("service_name_en")
        '    ret.Columns.Add("service_name_th")
        '    ret.Columns.Add("status")
        '    ret.Columns.Add("slot_datetime", GetType(DateTime))

        '    Dim trans As New TransactionDB
        '    Dim lnq As New Cen.TbShopCenLinqDB
        '    Dim dt As DataTable = lnq.GetDataList("active='Y'", "", trans.Trans)
        '    trans.CommitTransaction()
        '    If dt.Rows.Count > 0 Then
        '        For Each dr As DataRow In dt.Rows
        '            Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))
        '            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.GetAppointmentDesc")
        '            'shTrans.CreateTransaction(cenShLnq.SHOP_DB_SERVER, cenShLnq.SHOP_DB_USERID, cenShLnq.SHOP_DB_PWD, cenShLnq.SHOP_DB_NAME)
        '            If shTrans.Trans IsNot Nothing Then
        '                Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
        '                Dim sqlRs As String = "select distinct ac.customer_id mobile_no, ac.start_slot, s.master_itemid, s.item_name, s.item_name_th, "
        '                sqlRs += " case ac.active_status when '" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.REGISTERED & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.COMPLETED & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Missed & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.MISSED & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.NoShow & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.NO_SHOW & "' "
        '                sqlRs += " when '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "' then '" & Constant.TbAppointmentCustomer.Siebel.Status.CANCELLED & "' "
        '                sqlRs += " end status, ac.app_date, s.item_time,s.item_wait,s.item_order"
        '                sqlRs += " from TB_APPOINTMENT_CUSTOMER ac "
        '                sqlRs += " inner join TB_ITEM s on s.id=ac.item_id"
        '                sqlRs += " where CONVERT(varchar(16),ac.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
        '                sqlRs += " and ac.customer_id = '" & MobileNo & "' "
        '                sqlRs += " and ac.active_status in ('" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "','" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "') "
        '                sqlRs += " order by s.item_time,s.item_wait,s.item_order"

        '                ret = BuiltAppointmentList(ret, sqlRs, shLinq, shTrans, cenShLnq)
        '            End If
        '            cenShLnq = Nothing
        '        Next
        '    End If

        '    ret.TableName = "AppointmentDesc"
        '    Return ret
        'End Function

        Public Function GetLastShopRegister(ByVal MobileNo As String) As CenParaDB.Appointment.LastShopRegisterPara
            Dim ret As New CenParaDB.Appointment.LastShopRegisterPara
            Dim cSql As String = "select distinct aj.shop_abb, aj.start_slot  "
            cSql += " from TB_APPOINTMENT_JOB aj"
            cSql += " where CONVERT(varchar(16),aj.start_slot,120) <= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            cSql += " and aj.mobile_no = '" & MobileNo & "' "
            cSql += " and aj.active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.EndQueue & "' "
            cSql += " order by aj.start_slot desc"

            Dim dt As New DataTable
            Dim lnq As New Cen.TbAppointmentJobCenLinqDB
            dt = lnq.GetListBySql(cSql, Nothing)
            If dt.Rows.Count > 0 Then
                Dim rDT As New DataTable
                rDT.Columns.Add("shop_id")
                rDT.Columns.Add("service_date", GetType(DateTime))
                rDT.Columns.Add("shop_name_th")
                rDT.Columns.Add("shop_name_en")
                rDT.Columns.Add("region_id")
                rDT.Columns.Add("service_id")
                rDT.Columns.Add("cdate")

                Dim dr As DataRow = dt.Rows(0)
                Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDBByShopAbb(dr("shop_abb"))

                Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.GetLastShopRegister")
                Dim shLinq As New Sh.TbCounterQueueShLinqDB

                Dim Sql As String = "select q.service_date, isnull(t.master_itemid,0) item_id, convert(varchar(19),q.service_date,120) cdate " & vbNewLine
                Sql += " from tb_counter_queue q " & vbNewLine
                Sql += " inner join tb_item t on t.id=q.item_id"
                Sql += " where q.customer_id = '" & MobileNo & "' and q.status='3'" & vbNewLine
                Sql += " and convert(varchar(10), q.service_date,120) <= '" & Today.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'  " & vbNewLine
                Sql += " union all" & vbNewLine
                Sql += " select q.service_date,isnull(t.master_itemid,0) item_id, convert(varchar(19),q.service_date,120) cdate " & vbNewLine
                Sql += " from tb_counter_queue_history q " & vbNewLine
                Sql += " inner join tb_item t on t.id=q.item_id"
                Sql += " where q.customer_id = '" & MobileNo & "' and q.status='3'" & vbNewLine
                Sql += " and convert(varchar(10), q.service_date,120) < '" & Today.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'  " & vbNewLine
                Sql += " order by service_date desc" & vbNewLine

                Dim dd As DataTable = shLinq.GetListBySql(Sql, shTrans.Trans)
                shTrans.CommitTransaction()
                If dd.Rows.Count > 0 Then
                    'หาข้อมูลของวันนี้ที่มีอยู่แล้วทุก Shop
                    Dim rDr As DataRow = rDT.NewRow
                    rDr("shop_id") = cenShLnq.ID
                    rDr("SERVICE_DATE") = dd.Rows(0)("service_date")
                    rDr("SHOP_NAME_TH") = cenShLnq.SHOP_NAME_TH
                    rDr("SHOP_NAME_EN") = cenShLnq.SHOP_NAME_EN
                    rDr("REGION_ID") = cenShLnq.REGION_ID
                    rDr("cdate") = Convert.ToDateTime(dd.Rows(0)("service_date")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US"))

                    dd.DefaultView.RowFilter = "cdate = '" & rDr("cdate") & "'"
                    If dd.DefaultView.Count > 0 Then
                        Dim ServiceID As String = ""
                        For Each ddr As DataRowView In dd.DefaultView
                            ServiceID += ddr("item_id") & "|"
                        Next
                        rDr("service_id") = ServiceID
                    End If
                    rDT.Rows.Add(rDr)
                    dd.Dispose()
                End If
                'Next

                If rDT.Rows.Count > 0 Then
                    rDT.DefaultView.Sort = "service_date desc"
                    ret.SHOP_ID = rDT.DefaultView(0)("shop_id")
                    ret.REGION_ID = rDT.DefaultView(0)("region_id")
                    ret.SERVICE_DATE = rDT.DefaultView(0)("service_date")
                    ret.SHOP_NAME_EN = rDT.DefaultView(0)("shop_name_en")
                    ret.SHOP_NAME_TH = rDT.DefaultView(0)("shop_name_th")
                    ret.SERVICE_ID = rDT.DefaultView(0)("service_id")
                End If
                rDT.Dispose()
                dt.Dispose()
            End If

            Return ret
        End Function

        Public Function SaveAppointmentJob(ByVal p As CenParaDB.TABLE.TbAppointmentJobCenParaDB) As CenParaDB.Appointment.SaveAppointmentJobPara
            Dim ret As New CenParaDB.Appointment.SaveAppointmentJobPara
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbAppointmentJobCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If

            lnq.SHOP_ABB = p.SHOP_ABB
            lnq.MOBILE_NO = p.MOBILE_NO
            lnq.APP_DATE = p.APP_DATE
            lnq.START_SLOT = p.START_SLOT
            lnq.APPOINTMENT_CHANNEL = p.APPOINTMENT_CHANNEL
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

            Dim re As Boolean = False
            If lnq.ID <> 0 Then
                re = lnq.UpdateByPK("AppointmentENG.SaveAppointmentJob", trans.Trans)
            Else
                re = lnq.InsertData("AppointmentENG.SaveAppointmentJob", trans.Trans)
                If re = True Then
                    p.ID = lnq.ID
                End If
            End If
            ret.SaveResult = re
            ret.AppointmentJobPara = p

            If re = False Then
                trans.RollbackTransaction()
                ret.ErrorMessage = lnq.ErrorMessage
            Else
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function CancelAppointment(ByVal MobileNo As String) As Boolean
            Dim ret As Boolean = False
            Try
                Dim CancelHour As String = FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")
                If CancelHour.Trim = "" Then
                    CancelHour = "6"
                End If
                Dim CancelBefore As DateTime = DateAdd(DateInterval.Hour, Convert.ToInt64(CancelHour), DateTime.Now)
                Dim whText As String = "CONVERT(varchar(16),start_slot,120) >= '" & CancelBefore.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
                whText += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "
                whText += " and mobile_no = '" & MobileNo & "' "

                Dim lnq As New Cen.TbAppointmentJobCenLinqDB
                ret = lnq.ChkDataByWhere(whText, Nothing)
                If ret = True Then
                    Dim JobID As Long = lnq.ID

                    Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDBByShopAbb(lnq.SHOP_ABB)
                    Dim StartSlot As String = lnq.START_SLOT.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US"))
                    Dim trans As New TransactionDB

                    'Delete Notify ก่อน
                    Dim DelWh As String = ""
                    DelWh += " delete from tb_notify_joblist "
                    DelWh += " where shop_id='" & shLnq.ID & "' "
                    DelWh += " and mobile_no = '" & MobileNo & "'"
                    DelWh += " and convert(varchar(16),appointment_time, 120) = '" & StartSlot & "'"
                    SqlDB.ExecuteNonQuery(DelWh, trans.Trans)

                    lnq.GetDataByPK(JobID, trans.Trans)
                    If lnq.ID > 0 Then
                        lnq.ACTIVE_STATUS = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.Cancel

                        ret = lnq.UpdateByPK("AppointmentENG.CancelAppointment", trans.Trans)
                        If ret = True Then
                            'Connect ไปยัง DB Shop เพื่อไป Update TB_APPOINTMENT_CUSTOMER และ คืน Slot
                            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(shLnq.ID, "Engine.AppointmentENG.CancelAppointment")
                            Dim AppointInfo() As String = GetShopAppointInfo(MobileNo, StartSlot, Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment, shTrans)
                            Dim uSql As String = "update tb_appointment_customer "
                            uSql += " set active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "'"
                            uSql += " where appointment_job_id='" & JobID & "'"

                            Dim acLnq As New Sh.TbAppointmentCustomerShLinqDB
                            ret = acLnq.UpdateBySql(uSql, shTrans.Trans)
                            If ret = True Then
                                'คืน Slot
                                If ClearTimeSlot(lnq.START_SLOT, shLnq.ID, AppointInfo(0), shTrans) = True Then
                                    Dim gw As New GateWayServiceENG
                                    gw.SiebelUpdateToCancel(MobileNo, lnq.START_SLOT, DateTime.Now, AppointInfo(1), AppointInfo(2), shLnq, shTrans)
                                    gw = Nothing

                                    shTrans.CommitTransaction()
                                    trans.CommitTransaction()
                                Else
                                    ret = False
                                    shTrans.RollbackTransaction()
                                    trans.RollbackTransaction()
                                End If
                            Else
                                shTrans.RollbackTransaction()
                                trans.RollbackTransaction()
                            End If
                        Else
                            ret = False
                            trans.RollbackTransaction()
                        End If
                    End If
                End If

                lnq = Nothing
            Catch ex As Exception
                ret = False
            End Try

            Return ret
        End Function

        'Public Function CancelAppointment(ByVal MobileNo As String) As Boolean
        '    Dim ret As Boolean = False
        '    Dim trans As New TransactionDB
        '    Dim lnq As New Cen.TbShopCenLinqDB
        '    Dim dt As DataTable = lnq.GetDataList("active='Y'", "", trans.Trans)
        '    trans.CommitTransaction()
        '    If dt.Rows.Count > 0 Then
        '        For Each dr As DataRow In dt.Rows
        '            Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDB(dr("id"))

        '            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.CancelAppointment")
        '            'shTrans.CreateTransaction(cenShLnq.SHOP_DB_SERVER, cenShLnq.SHOP_DB_USERID, cenShLnq.SHOP_DB_PWD, cenShLnq.SHOP_DB_NAME)
        '            Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB

        '            Dim CancelHour As Long = FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")
        '            Dim CancelBefore As DateTime = DateAdd(DateInterval.Hour, CancelHour, DateTime.Now)
        '            Dim whText As String = "CONVERT(varchar(16),start_slot,120) >= '" & CancelBefore.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
        '            whText += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "
        '            whText += " and customer_id = '" & MobileNo & "' "

        '            ret = shLinq.ChkDataByWhere(whText, shTrans.Trans)
        '            If ret = True Then
        '                Dim StartSlot As String = shLinq.START_SLOT.Value.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US"))
        '                'Delete Notify ก่อน
        '                Dim DelWh As String = ""
        '                DelWh += " delete from tb_notify_joblist "
        '                DelWh += " where shop_id='" & cenShLnq.ID & "' "
        '                DelWh += " and mobile_no = '" & MobileNo & "'"
        '                DelWh += " and convert(varchar(16),appointment_time, 120) = '" & StartSlot & "'"
        '                trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                SqlDB.ExecuteNonQuery(DelWh, trans.Trans)
        '                trans.CommitTransaction()

        '                'Update Appointment Job
        '                Dim pJob As New CenParaDB.Appointment.SaveAppointmentJobPara
        '                Dim pPara As New CenParaDB.TABLE.TbAppointmentJobCenParaDB
        '                If Convert.IsDBNull(shLinq.APPOINTMENT_JOB_ID) = False Then pPara.ID = shLinq.APPOINTMENT_JOB_ID
        '                pPara.SHOP_ABB = FunctionEng.GetShopConfig("ShopAbbCode", shTrans)
        '                pPara.MOBILE_NO = MobileNo
        '                pPara.APP_DATE = shLinq.APP_DATE
        '                pPara.START_SLOT = shLinq.START_SLOT
        '                pPara.APPOINTMENT_CHANNEL = shLinq.APPOINTMENT_CHANNEL
        '                pPara.ACTIVE_STATUS = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.Cancel
        '                pJob = SaveAppointmentJob(pPara)
        '                'pPara = Nothing

        '                Dim ChooseService = GetChooseService(MobileNo, StartSlot, Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment, shTrans)
        '                Dim uSql As String = "update tb_appointment_customer "
        '                uSql += " set active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.Cancel & "'"
        '                uSql += " where appointment_job_id='" & pPara.ID & "'"
        '                ret = shLinq.UpdateBySql(uSql, shTrans.Trans)
        '                If ret = True Then
        '                    'คืน Slot
        '                    If ClearTimeSlot(shLinq.START_SLOT.Value, cenShLnq.ID, ChooseService, shTrans) = True Then
        '                        trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                        Dim gw As New GateWayServiceENG
        '                        gw.SiebelUpdateToCancel(MobileNo, shLinq.START_SLOT.Value, DateTime.Now, shLinq.SIEBEL_ACTIVITY_ID, shLinq.SIEBEL_DESC, cenShLnq, shTrans)
        '                        gw = Nothing

        '                        shTrans.CommitTransaction()
        '                        trans.CommitTransaction()

        '                        Exit For
        '                    Else
        '                        ret = False
        '                        shTrans.RollbackTransaction()
        '                    End If

        '                Else
        '                    shTrans.RollbackTransaction()
        '                End If
        '            End If
        '        Next
        '    End If

        '    Return ret
        'End Function

        Public Function CheckSlotCapacity(ByVal ShopID As Long, ByVal SelectTime As DateTime, ByVal MobileNo As String, ByVal SelServQty As Integer, Optional ByVal ShopidOld As Integer = 0) As Boolean
            Dim ret As Boolean = False
            Dim SelDate As String = SelectTime.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim SelTime As String = SelectTime.ToString("HH:mm")

            Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            shLnq = FunctionEng.GetTbShopCenLinqDB(ShopID)

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB

            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
            If shTrans.Trans IsNot Nothing Then
                Dim whSlot = " convert(varchar(8),app_date,112)='" & SelDate & "'"
                whSlot += " and '" & SelTime & "' between CONVERT(varchar(5),start_time,114) and CONVERT(varchar(5),end_time,114)"

                Dim dt As New DataTable
                Dim lnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
                dt = lnq.GetDataList(whSlot, "", shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    Dim Slot As Integer = Convert.ToInt32(dt.Rows(0)("slot"))
                    If GetActiveAppointment(MobileNo) = True And ShopidOld = 0 Then '##ตั้มเพิ่ม  ShopidOld = 0 ให้รูว่า Edit มีการเปลี่ยน Shop 0=ไม่มีการเปลี่ยน  ถ้า >0 มีการเปลี่ยน Shop ##
                        Dim TmpQty As Integer = 0

                        'กรณีทำการแก้ไขข้อมูลการจอง ให้ตรวจสอบข้อมูลที่จองอยู่แล้ว
                        Dim AppDt As New DataTable
                        AppDt = GetAppointmentDesc(MobileNo)
                        If AppDt.Rows.Count > 0 Then
                            'หา Slot ใหม่ที่เลือก
                            Dim strSql = "select id,in_use, capacity, convert(varchar(16),slot_time,120) slot_time "
                            strSql += " from TB_APPOINTMENT_SLOT "
                            strSql += " where convert(varchar(8),app_date,112)='" & SelDate & "'"
                            strSql += " and CONVERT(varchar(5),slot_time,114) between '" & SelTime & "' and '" & DateAdd(DateInterval.Minute, (SelServQty - 1) * Slot, CDate(SelTime)).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (SelServQty - 1) * Slot, CDate(SelTime)).Minute.ToString.PadLeft(2, "0") & "'"

                            Dim nLnq = New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                            Dim nDt As New DataTable
                            nDt = nLnq.GetListBySql(strSql, shTrans.Trans)
                            If nDt.Rows.Count > 0 Then

                                'หา Slot เดิม
                                Dim ServQty As Integer = Split(AppDt.Rows(0)("service_id"), ",").Length
                                Dim CurrAppTime As DateTime = Convert.ToDateTime(AppDt.Rows(0)("slot_datetime"))
                                Dim TmpDate As String = CurrAppTime.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                Dim TmpTime As String = CurrAppTime.ToString("HH:mm")
                                strSql = "select id,in_use, capacity, convert(varchar(16),slot_time,120) slot_time "
                                strSql += " from TB_APPOINTMENT_SLOT "
                                strSql += " where convert(varchar(8),app_date,112)='" & TmpDate & "'"
                                strSql += " and CONVERT(varchar(5),slot_time,114) between '" & TmpTime & "' and '" & DateAdd(DateInterval.Minute, (ServQty - 1) * Slot, CDate(TmpTime)).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (ServQty - 1) * Slot, CDate(TmpTime)).Minute.ToString.PadLeft(2, "0") & "'"
                                strSql += " and (in_use - 1) < capacity"

                                Dim aLnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                                Dim aDt As New DataTable
                                aDt = aLnq.GetListBySql(strSql, shTrans.Trans)

                                For Each nDr As DataRow In nDt.Rows
                                    If aDt.Rows.Count > 0 Then
                                        'เทียบกันเพื่อหาว่า Slot ใหม่ซ้ำกับ Slot เก่าอยู่แล้วหรือไม่
                                        aDt.DefaultView.RowFilter = "slot_time='" & nDr("slot_time") & "'"
                                        If aDt.DefaultView.Count > 0 Then
                                            TmpQty += 1
                                        Else
                                            'ถ้าไม่ซ้ำ ให้ตรวจสอบว่า Slot ใหม่นี้ว่างอยู่หรือไม่
                                            If nDr("in_use") < nDr("capacity") Then
                                                TmpQty += 1
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        If TmpQty = SelServQty Then
                            ret = True
                        End If
                    Else
                        'กรณีจองใหม่
                        Dim strSql = "select id "
                        strSql += " from TB_APPOINTMENT_SLOT "
                        strSql += " where in_use < capacity "
                        strSql += " and convert(varchar(8),app_date,112)='" & SelDate & "'"
                        strSql += " and CONVERT(varchar(5),slot_time,114) between '" & SelTime & "' and '" & DateAdd(DateInterval.Minute, (SelServQty - 1) * Slot, CDate(SelTime)).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (SelServQty - 1) * Slot, CDate(SelTime)).Minute.ToString.PadLeft(2, "0") & "'"

                        Dim aLnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                        Dim aDt As New DataTable
                        aDt = aLnq.GetListBySql(strSql, shTrans.Trans)
                        If aDt.Rows.Count = SelServQty Then
                            ret = True
                        End If
                        aDt.Dispose()
                        aLnq = Nothing
                    End If
                End If
                dt.Dispose()
                lnq = Nothing
            End If
            Return ret
        End Function

        Private Function GetActiveSiebelActivity(ByVal ShopAbb As String, ByVal MobileNo As String) As String
            Dim ret As String = ""

            Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDBByShopAbb(ShopAbb)
            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmnetENG.GetActiveSiebelActivity")
            If shTrans.Trans IsNot Nothing Then
                Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB
                Dim sqlRs As String = "select distinct  ac.customer_id mobile_no, ac.siebel_activity_id, ac.siebel_status, ac.siebel_desc"
                sqlRs += " from TB_APPOINTMENT_CUSTOMER ac "
                sqlRs += " inner join TB_ITEM s on s.id=ac.item_id"
                sqlRs += " where CONVERT(varchar(16),ac.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
                sqlRs += " and ac.customer_id = '" & MobileNo & "' "
                sqlRs += " and ac.active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "

                Dim tmp As New DataTable
                tmp = shLinq.GetListBySql(sqlRs, shTrans.Trans)
                shTrans.CommitTransaction()
                If tmp.Rows.Count > 0 Then
                    With tmp.Rows(0)
                        If Convert.IsDBNull(.Item("siebel_activity_id")) = False Then ret = .Item("siebel_activity_id")
                        If Convert.IsDBNull(.Item("siebel_status")) = False Then ret += "##" & .Item("siebel_status")
                        If Convert.IsDBNull(.Item("siebel_desc")) = False Then ret += "##" & .Item("siebel_desc")
                    End With
                End If
                tmp.Dispose()
                shLinq = Nothing
            End If
            cenShLnq = Nothing

            Return ret
        End Function

        'Public Function EditAppointment(ByVal ShopID As String, ByVal StartTime As DateTime, ByVal EndTime As DateTime, ByVal ItemID As String, ByVal MobileNo As String, ByVal PreferLang As String, ByVal AppointmentChannal As Char, ByVal CustomerEmail As String) As EditAppointmentResultPara
        '    'การแก้ไขคือการลบขัอมูลการจองล่วงหน้าเดิมก่อน แล้วค่อย Insert ใหม่
        '    Dim ret As New EditAppointmentResultPara
        '    Try
        '        Dim sql As String = "select distinct aj.id "
        '        sql += " from TB_APPOINTMENT_JOB aj"
        '        sql += " where CONVERT(varchar(16),aj.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
        '        sql += " and aj.mobile_no = '" & MobileNo & "' "
        '        sql += " and aj.active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "
        '        sql += " order by aj.id desc"

        '        Dim dt As New DataTable
        '        Dim jobLnq As New Cen.TbAppointmentJobCenLinqDB
        '        dt = jobLnq.GetListBySql(sql, Nothing)
        '        If dt.Rows.Count > 0 Then
        '            Dim lnq As New Cen.TbAppointmentEditJobCenLinqDB
        '            lnq.TB_APPOINTMENT_JOB_ID_OLD = Convert.ToInt64(dt.Rows(0)("id"))
        '            lnq.MOBILE_NO = MobileNo
        '            lnq.TB_SHOP_ID = ShopID
        '            lnq.SERVICE_ID = ItemID
        '            lnq.APP_DATE = DateTime.Now
        '            lnq.START_SLOT = StartTime
        '            lnq.APPOINTMENT_CHANNEL = AppointmentChannal
        '            lnq.CUSTOMER_EMAIL = CustomerEmail
        '            lnq.PREFER_LANG = PreferLang

        '            Dim trans As New TransactionDB
        '            If lnq.InsertData(MobileNo, trans.Trans) = True Then
        '                trans.CommitTransaction()
        '                ret.RESPONSE = True
        '            Else
        '                trans.RollbackTransaction()
        '                ret.RESPONSE = False
        '                ret.ErrorMessage = lnq.ErrorMessage
        '            End If
        '        Else
        '            ret.RESPONSE = False
        '            ret.ErrorMessage = "No Appointment Job Mobile no:" & MobileNo & " ShopID:" & ShopID & " ActiveStatus :" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment
        '        End If
        '        dt.Dispose()
        '        jobLnq = Nothing
        '    Catch ex As Exception
        '        ret.RESPONSE = False
        '        ret.ErrorMessage = "Exception : AppointmentENG.EditAppointment " & ex.Message & vbNewLine & ex.StackTrace
        '    End Try

        '    Return ret
        'End Function

        Public Function EditAppointment(ByVal ShopID As String, ByVal StartTime As DateTime, ByVal EndTime As DateTime, ByVal ItemID As String, ByVal CustomerID As String, ByVal PreferLang As String, ByVal AppointmentChannal As Char, ByVal CustomerEmail As String) As EditAppointmentResultPara
            'การแก้ไขคือการลบขัอมูลการจองล่วงหน้าเดิมก่อน แล้วค่อย Insert ใหม่
            Dim ret As New EditAppointmentResultPara

            Dim sql As String = "select distinct aj.mobile_no,aj.shop_abb "
            sql += " from TB_APPOINTMENT_JOB aj"
            sql += " where CONVERT(varchar(16),aj.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            sql += " and aj.mobile_no = '" & CustomerID & "' "
            sql += " and aj.active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "

            Dim dt As New DataTable
            Dim lnq As New Cen.TbAppointmentJobCenLinqDB
            dt = lnq.GetListBySql(sql, Nothing)
            If dt.Rows.Count > 0 Then
                Dim MobileNo As String = dt.Rows(0)("mobile_no").ToString
                Dim ShopAbb As String = dt.Rows(0)("shop_abb").ToString
                Dim CurrentSiebelAct As String = GetActiveSiebelActivity(ShopAbb, MobileNo)

                Dim eng As New AppointmentENG
                If eng.DeleteAppointment(ShopAbb, MobileNo) = True Then
                    Dim tmpRet As New InsertAppointmentResultPara
                    Dim ItemName As String = ""
                    Dim tmpItem() As String = Split(ItemID, ",")
                    Dim para1(tmpItem.Length - 1) As pSh.TbAppointmentCustomerShParaDB
                    Dim i As Integer = 0
                    For Each ServiceID As String In tmpItem
                        Dim para As New pSh.TbAppointmentCustomerShParaDB
                        para.CAPACITY = 1
                        para.APP_DATE = DateTime.Now
                        para.START_SLOT = StartTime
                        para.ITEM_ID = ServiceID
                        para.CUSTOMER_ID = CustomerID
                        para.APPOINTMENT_CHANNEL = AppointmentChannal
                        para.ACTIVE_STATUS = CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment
                        para.CUSTOMER_EMAIL = CustomerEmail
                        para1(i) = para


                        Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShopID, "AppointmentENG.EditAppointment")
                        Dim iLnq As New Sh.TbItemShLinqDB
                        iLnq.GetDataByPK(ServiceID, shTrans.Trans)
                        If iLnq.ID > 0 Then
                            If ItemName = "" Then
                                ItemName = iLnq.ITEM_NAME_TH
                            Else
                                ItemName += "," & iLnq.ITEM_NAME_TH
                            End If
                        End If
                        shTrans.CommitTransaction()
                        iLnq = Nothing

                        i += 1
                    Next
                    tmpRet = eng.InsertAppointment(ShopID, para1, PreferLang, AppointmentChannal, False)
                    ret.RESPONSE = tmpRet.RESPONSE
                    ret.ErrorMessage = tmpRet.ErrorMessage

                    Dim OldSiebel() As String = Split(CurrentSiebelAct, "##")
                    If OldSiebel.Length = 3 Then
                        ''Update Siebel Activiry ให้เป็นค่าเดิม
                        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)

                        Dim gw As New GateWayServiceENG
                        gw.SiebelUpdateDescription(CustomerID, StartTime, ItemName, AppointmentChannal, shLnq, trans, OldSiebel(0))
                        gw = Nothing
                        shLnq = Nothing
                        trans.CommitTransaction()
                    End If
                Else
                    ret.RESPONSE = False
                    ret.ErrorMessage = "Cannon Delete Appointment Data"
                End If
            Else
                ret.RESPONSE = False
                ret.ErrorMessage = "No Active Appointment Data"
            End If

            Return ret
        End Function

        Private Function GetChooseService(ByVal MobileNo As String, ByVal StartDateTime As String, ByVal ActiveStatus As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""

            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                Dim whText As String = "CONVERT(varchar(16),start_slot,120) = '" & StartDateTime & "' "
                whText += " and active_status = '" & ActiveStatus & "' "
                whText += " and customer_id = '" & MobileNo & "' "
                Dim dt As DataTable = lnq.GetDataList(whText, "", shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If ret = "" Then
                            ret = dr("item_id")
                        Else
                            ret += "," & dr("item_id")
                        End If
                    Next
                End If
            End If

            Return ret
        End Function

        Private Function GetShopAppointInfo(ByVal MobileNo As String, ByVal StartDateTime As String, ByVal ActiveStatus As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String()
            Dim ret As String = ""

            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                Dim whText As String = "CONVERT(varchar(16),start_slot,120) = '" & StartDateTime & "' "
                whText += " and active_status = '" & ActiveStatus & "' "
                whText += " and customer_id = '" & MobileNo & "' "
                Dim dt As DataTable = lnq.GetDataList(whText, "", shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If ret = "" Then
                            ret = dr("item_id")
                        Else
                            ret += "," & dr("item_id")
                        End If
                    Next

                    ret += "##" & dt.Rows(0)("siebel_activity_id") & "##" & dt.Rows(0)("siebel_desc")
                End If
            End If

            Return Split(ret, "##")
        End Function

        Public Function DeleteAppointment(ByVal ShopAbb As String, ByVal MobileNo As String) As Boolean
            Dim ret As Boolean = False
            
            Dim cenShLnq As Cen.TbShopCenLinqDB = Common.FunctionEng.GetTbShopCenLinqDBByShopAbb(ShopAbb)
            Dim shTrans As uSh.TransactionDB = FunctionEng.GetShTransction(cenShLnq.ID, "Engine.AppointmentENG.DeleteAppointment")
            Dim shLinq As New Sh.TbAppointmentCustomerShLinqDB

            Dim CancelHour As Long = FunctionEng.GetQisDBConfig("MaxEditAppointmentHour")
            Dim CancelBefore As DateTime = DateAdd(DateInterval.Hour, CancelHour, DateTime.Now)
            Dim whText As String = "CONVERT(varchar(16),start_slot,120) >= '" & CancelBefore.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' "
            whText += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "' "
            whText += " and customer_id = '" & MobileNo & "' "

            ret = shLinq.ChkDataByWhere(whText, shTrans.Trans)
            If ret = True Then
                Dim StartSlot As String = shLinq.START_SLOT.Value.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US"))

                Dim trans As New TransactionDB
                'Delete Notify ก่อน
                Dim DelWh As String = ""
                DelWh += " delete from tb_notify_joblist "
                DelWh += " where shop_id='" & cenShLnq.ID & "' "
                DelWh += " and mobile_no = '" & MobileNo & "'"
                DelWh += " and convert(varchar(16),appointment_time, 120) = '" & StartSlot & "'"
                SqlDB.ExecuteNonQuery(DelWh, trans.Trans)

                Dim ChooseService As String = GetChooseService(MobileNo, StartSlot, Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment, shTrans)
                Dim uSql As String = "delete from tb_appointment_customer "
                uSql += " where customer_id = '" & MobileNo & "' "
                uSql += " and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "' "
                ret = (SqlDB.ExecuteNonQuery(uSql, shTrans.Trans) > 0)

                If shLinq.APPOINTMENT_JOB_ID > 0 Then
                    Dim dWh As String = "delete from tb_appointment_job where id='" & shLinq.APPOINTMENT_JOB_ID & "'"
                    SqlDB.ExecuteNonQuery(dWh, trans.Trans)
                    trans.CommitTransaction()
                End If

                If ret = True Then
                    If ClearTimeSlot(shLinq.START_SLOT.Value, cenShLnq.ID, ChooseService, shTrans) = True Then
                        shTrans.CommitTransaction()
                    Else
                        ret = False
                        shTrans.RollbackTransaction()
                    End If
                Else
                    shTrans.RollbackTransaction()
                End If
            End If

            Return ret
        End Function

        Public Function CreateTimeSlot(ByVal ShopID As Long, ByVal ServiceID As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("ShowDate")
            ret.Columns.Add("ShowTime")
            ret.Columns.Add("status")
            ret.Columns.Add("SlotMinute")

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
                If shTrans.Trans IsNot Nothing Then
                    Dim sAppointmentDate As Double = FunctionEng.GetQisDBConfig("MaxAppointmentDay")  'By Config
                    Dim sDate As DateTime = DateTime.Now
                    Dim eDate As DateTime = DateAdd(DateInterval.Day, sAppointmentDate, sDate)
                    Dim kBeforeClose As Double = FunctionEng.GetShopConfig("k_before_close", shTrans)

                    Do
                        Dim sql As String = ""
                        Dim strDate As String = sDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))

                        '***************** ตรวจสอบว่าในวันนั้นมีเปิด Service ครบตามที่ระบุมาหรือไม่ ****************
                        Dim chk As Boolean = True
                        sql = " select t.master_itemid"
                        sql += " from TB_APPOINTMENT_ITEM ai"
                        sql += " inner join TB_ITEM t on t.id=ai.item_id"
                        sql += " where convert(varchar(10),app_date,120)='" & strDate & "'"
                        Dim iLnq As New ShLinqDB.TABLE.TbAppointmentItemShLinqDB
                        Dim iDt As New DataTable
                        iDt = iLnq.GetListBySql(sql, shTrans.Trans)
                        For Each s As String In Split(ServiceID, ",")
                            iDt.DefaultView.RowFilter = "master_itemid = '" & s & "'"
                            If iDt.DefaultView.Count = 0 Then
                                'ถ้าเปิด Service ไม่ครบตามที่ระบุก็ไม่ต้อง Gen Slot เลย จะได้จองไม่ได้
                                chk = False
                                Exit For
                            End If
                        Next
                        iLnq = Nothing
                        iDt.Dispose()

                        If chk = True Then
                            ''******************* Create Slot *********************
                            Dim lnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                            sql = "declare @EndSlot as datetime; " & vbNewLine
                            sql += " select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & strDate & "') = 0);" & vbNewLine
                            sql += " select top 1 start_time, " & vbNewLine
                            sql += " case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ," & vbNewLine
                            sql += " slot " & vbNewLine
                            sql += " from TB_APPOINTMENT_SCHEDULE " & vbNewLine
                            sql += " where DATEDIFF(D,'" & strDate & "',app_date)=0"
                            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)

                            'Gen Slot
                            If dt.Rows.Count > 0 Then
                                Dim date_now As Date = CenLinqDB.Common.Utilities.SqlDB.GetDateNowFromDB(trans.Trans)
                                'Loop in dataTable For add slot
                                Dim StartTime As Date = Convert.ToDateTime(dt.Rows(0).Item("start_time"))
                                Dim EndTime As Date = Convert.ToDateTime(dt.Rows(0).Item("end_time"))
                                Dim SlotTime As Date = StartTime
                                Dim Slot As Integer = dt.Rows(0).Item("slot")
                                'Add slot
                                Do
                                    If SlotTime.ToString("yyyy-MM-dd HH:mm") >= date_now.ToString("yyyy-MM-dd HH:mm") Then
                                        Dim dr As DataRow = ret.NewRow
                                        dr("ShowDate") = sDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                                        dr("ShowTime") = SlotTime.ToString("HH:mm")
                                        dr("SlotMinute") = Slot
                                        ret.Rows.Add(dr)
                                    End If
                                    SlotTime = DateAdd(DateInterval.Minute, Slot, SlotTime)
                                Loop While CDate(SlotTime) <= CDate(EndTime)
                            End If

                            'ถ้าเป็นวันที่ปัจจุบัน
                            If strDate = DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) Then
                                Dim kBeforeApp As Double = FunctionEng.GetQisDBConfig("MinAppointmentHour")  'FunctionEng.GetShopConfig("k_before_app", shTrans)

                                sql = "select app_date, convert(varchar(5),slot_time,114) as time, "
                                sql += " case when in_use = capacity then 1 else 0 end as status "
                                sql += " from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & strDate & "') = 0 "
                                sql += " and convert(varchar(8),app_date,112) in (select CONVERT(varchar(8),app_date,112) from TB_APPOINTMENT_ITEM where item_id in (" & ServiceID & "))"
                                sql += " and slot_time > DATEADD(HOUR," & kBeforeApp & ",'" & sDate.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "')"
                            Else
                                sql = "select app_date, convert(varchar(5),slot_time,114) as time, "
                                sql += " case when in_use = capacity then 1 else 0 end as status "
                                sql += " from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & strDate & "') = 0 "
                                sql += " and convert(varchar(8),app_date,112) in (select CONVERT(varchar(8),app_date,112) from TB_APPOINTMENT_ITEM where item_id in (" & ServiceID & "))"
                            End If

                            dt = lnq.GetListBySql(sql, shTrans.Trans)
                            If dt.Rows.Count > 0 Then
                                For i As Int32 = 0 To dt.Rows.Count - 1
                                    If dt.Rows(i).Item("status") = 0 Then
                                        For j As Integer = 0 To ret.Rows.Count - 1
                                            Dim dr As DataRow = ret.Rows(j)
                                            If dr("ShowTime") = dt.Rows(i)("time") And dr("ShowDate") = Convert.ToDateTime(dt.Rows(i)("app_date")).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) Then
                                                ret.Rows(j)("status") = "ว่าง"
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        End If
                        sDate = DateAdd(DateInterval.Day, 1, sDate)
                    Loop While sDate < eDate

                    shTrans.CommitTransaction()
                End If
                trans.CommitTransaction()
            End If

            ret.TableName = "CreateTimeSlot"
            Return ret
        End Function

        Private Function ClearTimeSlot(ByVal SlotDateTime As DateTime, ByVal ShopID As Long, ByVal ChooseService As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False

            Dim AllService() As String = Split(ChooseService, ",")
            Dim CountService As Int32 = AllService.Length
            If shTrans.Trans IsNot Nothing Then
                Dim kBeforeClose As Double = FunctionEng.GetShopConfig("k_before_close", shTrans)
                Dim slSql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "') = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "',app_date)=0"
                Dim slLnq As New ShLinqDB.TABLE.TbAppointmentScheduleShLinqDB
                Dim slDT As DataTable = slLnq.GetListBySql(slSql, shTrans.Trans)
                If slDT.Rows.Count > 0 Then
                    Dim Slot As Long = slDT.Rows(0)("slot")
                    Dim lnq As New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                    Dim whText As String = "DATEDIFF(d,app_date,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "') = 0 and CONVERT(varchar(5),slot_time,114) between '" & SlotDateTime.ToString("HH:mm") & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Minute.ToString.PadLeft(2, "0") & "'"
                    Dim dt As DataTable = lnq.GetDataList(whText, "", shTrans.Trans)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            lnq = New ShLinqDB.TABLE.TbAppointmentSlotShLinqDB
                            lnq.GetDataByPK(dr("id"), shTrans.Trans)
                            lnq.IN_USE = lnq.IN_USE - 1
                            ret = lnq.UpdateByPK(0, shTrans.Trans)
                        Next
                    End If
                End If
            End If

            Return ret
        End Function

        Public Function CheckBacklist(ByVal MobileNo As String) As AppointmentCheckBacklistResultPara
            Dim para As New AppointmentCheckBacklistResultPara
            Dim lnq As New CenLinqDB.TABLE.TbAppointmentBlacklistCenLinqDB
            Dim whText As String = "customer_id = '" & MobileNo & "' "
            whText += " and convert(varchar(10),getdate(), 120) between convert(varchar(10),start_date, 120) and convert(varchar(10),end_date, 120)"

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As DataTable = lnq.GetDataList(whText, "", Trans.Trans)
            If dt.Rows.Count > 0 Then
                para.IsBackList = True
                para.NewAppointmentDate = DateAdd(DateInterval.Day, 1, Convert.ToDateTime(dt.Rows(0)("end_date")))
                para.MSG = "Backlist"
                dt = Nothing
            End If
            lnq = Nothing
            trans.CommitTransaction()

            Return para
        End Function

#Region "Get Set Action On Mobile"
        Public Function CreateMobileAction(ByVal p As CenParaDB.TABLE.TbMobileAppointActCenParaDB) As String
            Dim lnq As New CenLinqDB.TABLE.TbMobileAppointActCenLinqDB
            lnq.MOBILE_NO = p.MOBILE_NO
            lnq.APP_DATE = DateTime.Now
            lnq.ACTION_STATUS = "1"

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim ret As String = "0"
            If lnq.InsertData(p.MOBILE_NO, trans.Trans) = True Then
                trans.CommitTransaction()
                ret = lnq.ID
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing
            Return ret
        End Function

        Public Function UpdateMobileAction(ByVal p As CenParaDB.TABLE.TbMobileAppointActCenParaDB) As Boolean
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbMobileAppointActCenLinqDB
            If p.ID > 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If

            lnq.SERVICE_ID = p.SERVICE_ID
            lnq.SHOP_ID = p.SHOP_ID
            lnq.APPOINTMENT_TIME = p.APPOINTMENT_TIME

            Dim ret As Boolean = False
            ret = lnq.UpdateByPK(lnq.MOBILE_NO, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing

            Return ret
        End Function

        Private Function UpdateActionConfirm(ByVal MobileActionID As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New CenLinqDB.TABLE.TbMobileAppointActCenLinqDB
            
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            lnq.GetDataByPK(MobileActionID, trans.Trans)
            If lnq.ID > 0 Then
                lnq.ACTION_STATUS = "2"
                ret = lnq.UpdateByPK(lnq.MOBILE_NO, trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If

            lnq = Nothing

            Return ret
        End Function

        Public Function GetMobileAction(ByVal ActionID As String) As MobileActionResultPara
            Dim p As New MobileActionResultPara
            Dim lnq As New CenLinqDB.TABLE.TbMobileAppointActCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            lnq.GetDataByPK(ActionID, trans.Trans)
            trans.CommitTransaction()
            If lnq.ID > 0 Then
                p.ActionID = lnq.ID
                p.ShopID = lnq.SHOP_ID
                p.ServiceID = lnq.SERVICE_ID
                p.AppointmentTime = lnq.APPOINTMENT_TIME
            End If
            lnq = Nothing

            Return p
        End Function
#End Region

#Region "Get Item Time"
        Public Function GetItemTime(ByVal id As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New Cen.TbItemCenLinqDB
            dt = lnq.GetDataList("id in(" & id & ")", "item_time", trans.Trans)
            dt.TableName = "GetItemTime"
            trans.CommitTransaction()

            Return dt

        End Function

#End Region

    End Class
End Namespace

