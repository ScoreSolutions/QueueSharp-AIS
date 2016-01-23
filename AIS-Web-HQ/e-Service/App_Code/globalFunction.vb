Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Net
Imports System.Drawing
Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.Data.SqlClient
Imports Engine.Common

Public Class globalFunction
    'Dim objConn As New OleDbConnection(ConfigurationManager.ConnectionStrings("QisDB").ConnectionString)
    Dim version As String = System.Configuration.ConfigurationManager.AppSettings("version")
    Public Type As String = ""
    Public Page_Name As String = ""
    Public Network_Type As String = ""

    Public Shared Function GetVersion() As String
        Return ConfigurationManager.AppSettings("Version").ToString()
    End Function

    Public Shared Function GetCustomerLogin() As CenParaDB.Appointment.CustomerWebAppointmentPara
        Dim tmp As New CenParaDB.Appointment.CustomerWebAppointmentPara
        Dim id As FormsIdentity = HttpContext.Current.User.Identity
        Dim tik As FormsAuthenticationTicket = id.Ticket
        Dim sr As New XmlSerializer(GetType(CenParaDB.Appointment.CustomerWebAppointmentPara))
        Using st As New MemoryStream(Convert.FromBase64String(tik.UserData))
            tmp = sr.Deserialize(st)
        End Using
        Return tmp
    End Function

    Public Function GetShop(ByVal strRegionID As String, ByVal strShopID As String) As DataTable
        Try
            Dim strSql As String = ""
            strSql = "Select * from ( " & _
                     "Select '0' as ID,'0000' as Shop_Code,'<-- กรุณาเลือก -->' as Shop_Name_Th,'<-- Select -->' as Shop_Name_En,'0' as Region_ID Union " & _
                     "Select ID,Shop_Code,Shop_Name_Th,Shop_Name_En,Region_ID From TB_Shop where active='Y' ) a Where 1=1"
            If strRegionID <> "" Then strSql &= " AND Region_ID in ('0','" & strRegionID & "')"
            If strShopID <> "" Then strSql &= " And ID in ('0'," & strShopID & ")"
            Return FunctionEng.GetDatatable(strSql)
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function

    Public Function GetRegion(ByVal strRegionID As String) As DataTable
        Try
            Dim strSql As String = ""
            Dim strWhere As String = ""
            If strRegionID <> "" Then strWhere = "And ID in ('0'," & strRegionID & ") "
            strSql = "select id,Region_Name_Th,Region_Name_En From ( " & _
                     "Select '0' as ID,'<-- กรุณาเลือก -->' as Region_Name_Th,'<-- Select -->' as Region_Name_En Union " & _
                     "Select ID,Region_Name_Th,Region_Name_En From TB_Region where active='Y' ) a Where 1=1 " & strWhere & _
                     "order by region_name_th"
            Return FunctionEng.GetDatatable(strSql)
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function

    Public Function GetService(ByVal NotInID As String) As DataTable
        Try
            'If objConn.State = ConnectionState.Closed Then objConn.Open()
            Dim strSql As String = ""

            strSql = "select id,Item_Name_th,Item_Name,Item_Order From (Select '0' as ID,'<-- กรุณาเลือก -->' as Item_Name_Th,'<-- Select -->' as Item_Name,'0' as Item_Order Union " & _
                     "Select ID,Item_Name_Th,Item_Name,Item_Order From TB_Item Where 1=1 And active_status= '1' and id not in (" & NotInID & ") ) a " & _
                     "order by item_order"
            Return FunctionEng.GetDatatable(strSql)
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function

    Public Function GetServiceAgent_ByDate(ByVal NotInID As String, ByVal strDateFrom As String, ByVal strDateTo As String) As DataTable
        'Try
        '    'If objConn.State = ConnectionState.Closed Then objConn.Open()
        '    Dim strSql As String = ""

        '    strSql = "select id"
        '    strSql &= ",Item_Name_th"
        '    strSql &= ",Item_Name"
        '    strSql &= ",Item_Order"
        '    strSql &= " From (Select '0' as ID,'<-- กรุณาเลือก -->' as Item_Name_Th,'<-- Select -->' as Item_Name,'0' as Item_Order"
        '    strSql &= "       Union"
        '    strSql &= "       Select  ITM.ID,ITM.Item_Name_Th,ITM.Item_Name,ITM.Item_Order"
        '    strSql &= "       From TB_Item ITM"
        '    strSql &= "       Inner Join  "
        '    strSql &= "       ("
        '    strSql &= "         select master_itemid,CONVERT(varchar,app_date,103) as app_date from TB_SHOP_SERVICE_APPOINTMENT "
        '    strSql &= "         where CONVERT(varchar(8),app_date,112) between '" & strDateFrom & "' and '" & strDateTo & "'"
        '    strSql &= "         group by master_itemid,CONVERT(varchar,app_date,103)"
        '    strSql &= "       ) as Table1"
        '    strSql &= "       ON Table1.master_itemid =ITM.id"
        '    strSql &= "       Where 1=1 "
        '    strSql &= "       And ITM.active_status= '1' "
        '    strSql &= "       and ITM.id not in (" & NotInID & ") ) a "
        '    strSql &= "       order by item_order"
        '    Return FunctionEng.GetDatatable(strSql)
        'Catch ex As Exception
        '    Return New DataTable
        'End Try

        Dim dt As New DataTable
        Dim eng As New Engine.Appointment.AppointmentENG
        dt = eng.GetServiceAgent_ByDate(NotInID, strDateFrom, strDateTo)
        eng = Nothing
        If dt.Rows.Count > 0 Then
            dt = dt.DefaultView.ToTable(True, "id", "item_name_th", "item_name", "item_order")
        End If

        'Select '0' as ID,'<-- กรุณาเลือก -->' as Item_Name_Th,'<-- Select -->' as Item_Name,'0' as Item_Order
        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("Item_Name_Th") = "<-- กรุณาเลือก -->"
        dr("Item_Name") = "<-- Select -->"
        dr("Item_Order") = "0"
        dt.Rows.InsertAt(dr, 0)

        Return dt
    End Function

    Public Function GetServiceAgent_ByDateAndShop(ByVal NotInID As String, ByVal strDateFrom As String, ByVal strDateTo As String, ByVal strShopId As String) As DataTable
        'Try
        '    'If objConn.State = ConnectionState.Closed Then objConn.Open()
        '    Dim strSql As String = ""

        '    strSql = "select id"
        '    strSql &= ",Item_Name_th"
        '    strSql &= ",Item_Name"
        '    strSql &= ",Item_Order"
        '    strSql &= " From (Select '0' as ID,'<-- กรุณาเลือก -->' as Item_Name_Th,'<-- Select -->' as Item_Name,'0' as Item_Order"
        '    strSql &= "       Union"
        '    strSql &= "       Select  ITM.ID,ITM.Item_Name_Th,ITM.Item_Name,ITM.Item_Order"
        '    strSql &= "       From TB_Item ITM"
        '    strSql &= "       Inner Join  "
        '    strSql &= "       ("
        '    strSql &= "         select master_itemid,CONVERT(varchar,app_date,103) as app_date from TB_SHOP_SERVICE_APPOINTMENT "
        '    strSql &= "         where CONVERT(varchar(8),app_date,112) between '" & strDateFrom & "' and '" & strDateTo & "' and shop_id=" & strShopId
        '    strSql &= "         group by master_itemid,CONVERT(varchar,app_date,103)"
        '    strSql &= "       ) as Table1"
        '    strSql &= "       ON Table1.master_itemid =ITM.id"
        '    strSql &= "       Where 1=1 "
        '    strSql &= "       And ITM.active_status= '1' "
        '    strSql &= "       and ITM.id not in (" & NotInID & ") ) a "
        '    strSql &= "       order by item_order"
        '    Return FunctionEng.GetDatatable(strSql)
        'Catch ex As Exception
        '    Return New DataTable
        'End Try

        Dim dt As New DataTable
        Dim eng As New Engine.Appointment.AppointmentENG
        dt = eng.GetServiceAgent_ByDateAndShop(NotInID, strDateFrom, strDateTo, strShopId)
        If dt.Rows.Count > 0 Then
            dt = dt.DefaultView.ToTable(True, "id", "item_name_th", "item_name", "item_order")
        End If

        Dim dr As DataRow = dt.NewRow
        dr("id") = "0"
        dr("Item_Name_Th") = "<-- กรุณาเลือก -->"
        dr("Item_Name") = "<-- Select -->"
        dr("Item_Order") = "0"
        dt.Rows.InsertAt(dr, 0)
        eng = Nothing
        Return dt
    End Function

    Public Function GetServiceByShop(ByVal NotInID As String, ByVal Master_ItemId As String) As DataTable
        Try
            'If objConn.State = ConnectionState.Closed Then objConn.Open()
            Dim strSql As String = ""

            strSql = "select id,Item_Name_th,Item_Name,Item_Order From (Select '0' as ID,'<-- กรุณาเลือก -->' as Item_Name_Th,'<-- Select -->' as Item_Name,'0' as Item_Order Union " & _
                     "Select ID,Item_Name_Th,Item_Name,Item_Order From TB_Item Where 1=1 And active_status= '1' and id not in (" & NotInID & ") and id in (" & Master_ItemId & ") ) a " & _
                     "order by item_order"
            Return FunctionEng.GetDatatable(strSql)
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function


    Public Function GetServiceByShopAgent_ByDate(ByVal NotInID As String, ByVal Master_ItemId As String, ByVal strDateFrom As String, ByVal strDateTo As String) As DataTable
        Try
            'If objConn.State = ConnectionState.Closed Then objConn.Open()
            Dim strSql As String = ""

            strSql = "select id"
            strSql &= ",Item_Name_th"
            strSql &= ",Item_Name"
            strSql &= ",Item_Order"
            strSql &= " From (Select '0' as ID,'<-- กรุณาเลือก -->' as Item_Name_Th,'<-- Select -->' as Item_Name,'0' as Item_Order"
            strSql &= "       Union"
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
            strSql &= "       and ITM.id not in (" & NotInID & ") and ITM.id in (" & Master_ItemId & ") ) a "
            strSql &= "       order by item_order"
            Return FunctionEng.GetDatatable(strSql)
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function

    Public Function GetText(ByVal lang As String, ByVal strobject As String) As String
        Try
            Dim strSql As String = ""
            Dim strWhere As String = ""
            Dim strValue As String = ""

            If Type.Length > 0 Then
                strWhere = "and type='" & Type.ToString.Trim & "' "
            End If
            If Network_Type.Length > 0 Then
                strWhere = "and cast(network_type as varchar(50))='" & Network_Type.ToString.Trim & "' "
            End If

            If lang = "Thai" Then
                strSql = "select txt_th from TB_CONFIGTXT_LANG " & _
                         "where ref_txt='" & strobject.ToString.Trim & "' " & _
                         "and page_name='" & Page_Name.ToString.Trim & "' " & strWhere
            Else
                strSql = "select txt_eng from TB_CONFIGTXT_LANG " & _
                         "where ref_txt='" & strobject.ToString.Trim & "' " & _
                         "and page_name='" & Page_Name.ToString.Trim & "' " & strWhere
            End If

            Dim Dt As New DataTable
            Dt = FunctionEng.GetDatatable(strSql)
            If Dt.Rows.Count = 0 Then
                strValue = ""
            Else
                strValue = Dt.Rows(0)(0)
            End If

            Return strValue
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetStyle(ByVal strNetworkType As String, ByVal strType As String) As String
        Dim strResult As String = ""
        Try
            Dim strSql As String = "Select txt_th From TB_CONFIGTXT_LANG Where ref_txt='" & strType & "' And network_type='" & strNetworkType & "' And Type='CSS'"
            Dim dt As DataTable = FunctionEng.GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                strResult = dt.Rows(0)("txt_th")
            End If
            dt.Dispose()

            Return strResult
        Catch ex As Exception
            Return ""
        Finally
            'objConn.Close()
        End Try
    End Function

    Public Function GetStyle(ByVal strNetworkType As String, ByVal strType As String,ByVal strLang As String) As String
        Dim strResult As String = ""
        Try
            Dim strSql As String = ""
            If strLang = "Thai" Then
                strSql = "Select txt_th From TB_CONFIGTXT_LANG Where ref_txt='" & strType & "' And network_type='" & strNetworkType & "' And Type='CSS'"
            Else
                strSql = "Select txt_en From TB_CONFIGTXT_LANG Where ref_txt='" & strType & "' And network_type='" & strNetworkType & "' And Type='CSS'"
            End If

            Dim dt As DataTable = FunctionEng.GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                strResult = dt.Rows(0)("txt_th")
            End If

            Return strResult
        Catch ex As Exception
            Return ""
        Finally

        End Try
    End Function

    Public Function GetStyleDropdown(ByVal strNetworkType As String, ByVal strType As String) As String
        Dim strResult As String = ""

        Try
            If strNetworkType.ToLower = "pre" Then
                If strType.ToLower = "font" Then strResult = "Tahoma, Geneva, Sans-Serif"
                If strType.ToLower = "color" Then strResult = "#5FAB2F"
                If strType.ToLower = "colorgrid" Then strResult = "95,171,47"
                If strType.ToLower = "size" Then strResult = "12"
            ElseIf strNetworkType.ToLower = "post" Then
                If strType.ToLower = "font" Then strResult = "Arail,Tahoma"
                If strType.ToLower = "color" Then strResult = "#333"
                If strType.ToLower = "colorgrid" Then strResult = "204,102,0"
                If strType.ToLower = "size" Then strResult = "12"
                'If strType.ToLower = "title" Then strResult = "font-family:Arail,Tahoma; color:#CC6600; font-weight:bold;"
                'If strType.ToLower = "text" Then strResult = "font-family:Arail, Tahoma; color:#333;"
                'If strType.ToLower = "text" Then strResult = "font-family:Arail, Tahoma; color:#faa81a;"
            ElseIf strNetworkType.ToLower = "corp" Then
                If strType.ToLower = "font" Then strResult = "Arail,Tahoma"
                If strType.ToLower = "color" Then strResult = "#126AC7"
                If strType.ToLower = "colorgrid" Then strResult = "18,106,199"
                If strType.ToLower = "size" Then strResult = "12"
                'If strType.ToLower = "title" Then strResult = "font-family:Arail, Tahoma;color:#333;font-weight:bold;"
                'If strType.ToLower = "text" Then strResult = "font-family:Arail, Tahoma;color:#126AC7;"
                'If strType.ToLower = "text" Then strResult = ""
            ElseIf strNetworkType.ToLower = "3g" Then
                If strType.ToLower = "font" Then strResult = "Tahoma, Geneva, Sans-Serif"
                If strType.ToLower = "color" Then strResult = "#5FAB2F"
                If strType.ToLower = "colorgrid" Then strResult = "95,171,47"
                If strType.ToLower = "size" Then strResult = "12"
            ElseIf strNetworkType.ToLower = "3gpost" Then
                If strType.ToLower = "font" Then strResult = "Tahoma, Geneva, Sans-Serif"
                If strType.ToLower = "color" Then strResult = "#c0d32c"
                If strType.ToLower = "colorgrid" Then strResult = "192,211,44"
                If strType.ToLower = "size" Then strResult = "12"
            End If
            Return strResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function Encrypt(ByVal textToEncrypt As String, ByVal key As String) As String
        Try
            Dim rijndaelCipher As New RijndaelManaged()
            rijndaelCipher.Mode = CipherMode.ECB
            rijndaelCipher.Padding = PaddingMode.PKCS7

            rijndaelCipher.KeySize = &H80
            rijndaelCipher.BlockSize = &H80
            rijndaelCipher.Key = Encoding.UTF8.GetBytes(key)

            Dim transform As ICryptoTransform = rijndaelCipher.CreateEncryptor()
            Dim plainText As Byte() = Encoding.UTF8.GetBytes(textToEncrypt)
            Dim encrypt__1 As Byte() = transform.TransformFinalBlock(plainText, 0, plainText.Length)

            Return Convert.ToBase64String(encrypt__1)
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Return ""
        End Try
    End Function

    Public Shared Function Decrypt(ByVal textToDecrypt As String, ByVal key As String) As String
        Try
            Dim rijndaelCipher As New RijndaelManaged()
            rijndaelCipher.Mode = CipherMode.ECB
            rijndaelCipher.Padding = PaddingMode.PKCS7

            rijndaelCipher.KeySize = &H80
            rijndaelCipher.BlockSize = &H80
            rijndaelCipher.Key = Encoding.UTF8.GetBytes(key)

            Dim transform As ICryptoTransform = rijndaelCipher.CreateDecryptor()
            Dim plainText As Byte() = Convert.FromBase64String(textToDecrypt)
            Dim decrypt__1 As Byte() = transform.TransformFinalBlock(plainText, 0, plainText.Length)

            Return Encoding.UTF8.GetString(decrypt__1)
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Return ""
        End Try
    End Function

    Public Function GetConnection(ByVal strServer As String, ByVal strDB As String, ByVal strUser As String, ByVal strPass As String) As OleDbConnection
        Try
            Dim Conn As String = ""
            Conn = "Provider=SQLOLEDB;Data Source=" & strServer & ";Initial Catalog=" & strDB & ";User Id=" & strUser & ";Password=" & strPass & ";"
            Dim objConn2 As New OleDbConnection(Conn)
            Return objConn2
        Catch ex As Exception
            Return New OleDbConnection
        End Try
    End Function

    'Public Function RetreiveSlotDate(ByVal shop_id As String, _
    '                 ByVal shop_db_name As String, _
    '                 ByVal shop_db_userid As String, _
    '                 ByVal shop_db_pwd As String, _
    '                 ByVal shop_db_server As String, ByVal slot_Date As Date, ByVal slot_time As Date) As Boolean
    '    Dim objConnShop As New OleDbConnection
    '    Try
    '        Dim strSql As String = ""
    '        Dim strDate As String = slot_Date.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & " " & slot_time.ToString("HH:mm") 'slot_Date.Year & "-" & slot_Date.Month & "-" & slot_Date.Day & " " & slot_time.ToString("HH:mm")
    '        Dim strLengthApp As String = GetQisDBConfig("Length_Of_Pre_Booking")
    '        Dim intPreBook As Integer = GetQisDBConfig("Pre_Booking_Period")
    '        strSql = "select * from TB_APPOINTMENT_SLOT where convert(varchar(16),slot_time,120)='" & strDate & "' " & _
    '                 "and isnull(in_use,0)<isnull(capacity,0) And app_date <= DateAdd(d," & strLengthApp & ",getdate()) "
    '        If objConn.State = ConnectionState.Closed Then objConn.Open()
    '        Dim objCmd As New OleDbCommand("Select Getdate()", objConn)
    '        Dim dDatenow As Date = objCmd.ExecuteScalar
    '        If CDate(slot_Date.Year & "/" & slot_Date.Month & "/" & slot_Date.Day) <= CDate(dDatenow.Year & "/" & dDatenow.Month & "/" & dDatenow.Day) Then strSql &= "and slot_time>DATEADD(HOUR," & intPreBook & ",GETDATE())"
    '        objConnShop = GetConnection(shop_db_server, shop_db_name, shop_db_userid, shop_db_pwd)
    '        If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '        Dim objAdapter As New OleDbDataAdapter(strSql, objConnShop)
    '        Dim Ds As New DataSet
    '        objAdapter.Fill(Ds, "Slot")
    '        If Ds.Tables("Slot").Rows.Count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '    Finally
    '        objConnShop.Close()
    '    End Try

    'End Function

    'Function RetriveSlot(ByVal shop_db_name As String, ByVal shop_db_userid As String, ByVal shop_db_pwd As String, _
    '             ByVal shop_db_server As String, ByVal selected_date As String, ByVal slot_time As String, ByVal service As Integer, ByVal IsEdit As String) As Boolean
    '    Dim ret As Boolean = False
    '    Dim strSql As String = ""
    '    Dim objConnShop As New OleDbConnection
    '    objConnShop = GetConnection(shop_db_server.Trim, shop_db_name.Trim, shop_db_userid.Trim, shop_db_pwd.Trim)
    '    If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '    Try
    '        strSql = "select slot "
    '        strSql += " from TB_APPOINTMENT_SCHEDULE "
    '        strSql += " where convert(varchar(10),app_date,120)='" & selected_date & "'"
    '        strSql += " and '" & slot_time & "' between CONVERT(varchar(5),start_time,114) and CONVERT(varchar(5),end_time,114)"
    '        Dim dt As New DataTable
    '        Dim objAdapter As New OleDbDataAdapter(strSql, objConnShop)
    '        objAdapter.Fill(dt)
    '        If dt.Rows.Count > 0 Then
    '            Dim Slot As Integer = Convert.ToInt32(dt.Rows(0)("slot"))
    '            strSql = "select id,in_use "
    '            strSql += " from TB_APPOINTMENT_SLOT "
    '            If IsEdit = "True" Then
    '                strSql += " where (in_use-1) < capacity "
    '            Else
    '                strSql += " where in_use < capacity "
    '            End If
    '            strSql += " and convert(varchar(10),app_date,120)='" & selected_date & "'"
    '            strSql += " and CONVERT(varchar(5),slot_time,114) between '" & slot_time & "' and '" & DateAdd(DateInterval.Minute, (service - 1) * Slot, CDate(slot_time)).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (service - 1) * Slot, CDate(slot_time)).Minute.ToString.PadLeft(2, "0") & "'"

    '            dt = New DataTable
    '            objAdapter = New OleDbDataAdapter(strSql, objConnShop)
    '            objAdapter.Fill(dt)
    '            If dt.Rows.Count = service Then
    '                ret = True
    '            End If
    '            dt.Dispose()
    '            objAdapter.Dispose()
    '        End If
    '    Catch ex As Exception
    '    Finally
    '        objConnShop.Close()
    '    End Try
    '    Return ret
    'End Function

    'Public Function AddTimeSlotCapacity(ByVal ChooseService As String, ByVal SlotDateTime As DateTime, ByVal ShopID As Long) As Boolean

    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    Dim AllService() As String = Split(ChooseService, ",")
    '    Dim CountService As Int32 = AllService.Length
    '    Dim objConnShop As New OleDbConnection

    '    Try
    '        Dim objCmd As New OleDbCommand("Select * From Tb_Shop Where ID='" & ShopID & "'", objConn)
    '        Dim objReader As OleDbDataReader = objCmd.ExecuteReader()
    '        If objReader.Read Then
    '            With objReader
    '                objConnShop = GetConnection(.Item("Shop_DB_server"), .Item("Shop_DB_Name"), .Item("Shop_DB_UserID"), .Item("Shop_DB_PWD"))
    '            End With
    '        End If
    '        objReader.Close()
    '    Catch ex As Exception
    '    End Try

    '    Dim objTrans As OleDbTransaction
    '    Try
    '        'Dim objCmd As New OleDbCommand("Select CONFIG_VALUE From TB_Setting Where Config_Name='k_before_close'", objConnShop)
    '        Dim objCmd As New OleDbCommand("Select CONFIG_VALUE From SYSCONFIG Where Config_Name='App_Before_Shop_Closed'", objConnShop)
    '        Dim kBeforeClose As Double = objCmd.ExecuteScalar
    '        Dim StrDateTime As String = SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
    '        Dim StrSql As String = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT " & _
    '                               "where DATEDIFF(d,app_date,'" & StrDateTime & "') = 0);select top 1 start_time,  case when " & _
    '                               "@EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & _
    '                               kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & _
    '                               StrDateTime & "',app_date)=0"
    '        Dim objAdapter As New OleDbDataAdapter(StrSql, objConnShop)
    '        Dim Ds As New DataSet
    '        objAdapter.Fill(Ds, "Slot")
    '        If Ds.Tables("Slot").Rows.Count > 0 Then
    '            Dim Slot As Long = Ds.Tables("Slot").Rows(0)("slot")
    '            StrSql = "Select * From TB_Appointment_Slot Where DATEDIFF(d,app_date,'" & StrDateTime & "') = 0 and in_use < capacity and CONVERT(varchar(5),slot_time,114) between '" & SlotDateTime.ToString("HH:mm") & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Minute.ToString.PadLeft(2, "0") & "'"
    '            objAdapter = New OleDbDataAdapter(StrSql, objConnShop)
    '            objAdapter.Fill(Ds, "SlotService")
    '            If Ds.Tables("SlotService").Rows.Count > 0 Then
    '                objTrans = objConnShop.BeginTransaction
    '                Try
    '                    objCmd = New OleDbCommand
    '                    objCmd.Connection = objConnShop
    '                    objCmd.Transaction = objTrans
    '                    For j As Int32 = 0 To Ds.Tables("SlotService").Rows.Count - 1
    '                        objCmd.CommandText = "Select IN_Use From TB_Appointment_Slot Where ID='" & Ds.Tables("SlotService").Rows(j).Item("ID") & "'"
    '                        Dim intInUser As Integer = CInt(objCmd.ExecuteScalar.ToString)
    '                        objCmd.CommandText = "Update TB_Appointment_Slot Set IN_Use='" & intInUser + 1 & "' Where ID='" & Ds.Tables("SlotService").Rows(j).Item("ID") & "'"
    '                        objCmd.ExecuteNonQuery()
    '                    Next
    '                    objTrans.Commit()
    '                Catch ex As Exception
    '                    objTrans.Rollback()
    '                End Try
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Public Function GetAppointmentHistory(ByVal MobileNo As String) As DataTable
    '    Dim DTable As New DataTable
    '    DTable.Columns.Add("mobile_no")
    '    DTable.Columns.Add("slot_date")
    '    DTable.Columns.Add("slot_time")
    '    DTable.Columns.Add("shop_id")
    '    DTable.Columns.Add("shop_name")
    '    DTable.Columns.Add("service_id")
    '    DTable.Columns.Add("service_name")
    '    DTable.Columns.Add("status")
    '    DTable.Columns.Add("slot_datetime", GetType(DateTime))

    '    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '    Dim Dt As New DataTable
    '    Dim objAdapter As New OleDbDataAdapter("Select * From TB_Shop Where active='Y'", objConn)
    '    Dim Ds As New DataSet
    '    Dim Drow As DataRow
    '    objAdapter.Fill(Ds, "Shop")
    '    Dt = Ds.Tables("Shop")

    '    If Dt.Rows.Count > 0 Then
    '        For Each dr As DataRow In Dt.Rows
    '            Try
    '                Dim objConnShop As OleDbConnection = GetConnection(dr.Item("Shop_DB_Server"), dr.Item("Shop_DB_Name"), dr.Item("Shop_DB_UserID"), dr.Item("Shop_DB_PWD"))
    '                If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '                Dim strSql As String = "select  ac.customer_id mobile_no, ac.start_slot , ac.item_id, s.item_name, " & _
    '                                      " case ac.active_status when '2' then 'Registered' when '3' then 'Complete' " & _
    '                                      " when '4' then 'Missed' when '5' then 'No Show' when '6' then 'Cancelled' " & _
    '                                      " end status from TB_APPOINTMENT_CUSTOMER ac inner join TB_ITEM s on s.id=ac.item_id " & _
    '                                      " where ac.customer_id = '" & MobileNo & "' and ac.active_status <> '1' order by ac.start_slot "
    '                Dim objCmd As New OleDbCommand(strSql, objConnShop)
    '                Dim objReader As OleDbDataReader = objCmd.ExecuteReader()
    '                Dim tmpStartSlot As DateTime = New DateTime(1, 1, 1)

    '                While objReader.Read
    '                    Dim tmpServiceID As String = ""
    '                    Dim tmpServiceName As String = ""
    '                    With objReader
    '                        tmpServiceID = .Item("item_id")
    '                        tmpServiceName = .Item("item_name")
    '                        If tmpStartSlot <> Convert.ToDateTime(.Item("start_slot")) Then
    '                            Drow = DTable.NewRow
    '                            Drow.Item("mobile_no") = .Item("mobile_no")
    '                            Drow.Item("slot_date") = Convert.ToDateTime(.Item("start_slot")).ToString("dd/MM/yy")
    '                            Drow.Item("slot_time") = Convert.ToDateTime(.Item("start_slot")).ToString("HH:mm")
    '                            Drow.Item("shop_id") = dr.Item("ID")
    '                            Drow.Item("shop_name") = dr.Item("shop_name_EN")
    '                            Drow.Item("service_id") = .Item("item_id")
    '                            Drow.Item("service_name") = .Item("item_name")
    '                            Drow.Item("status") = .Item("status")
    '                            Drow.Item("slot_datetime") = Convert.ToDateTime(.Item("start_slot"))
    '                            DTable.Rows.Add(Drow)

    '                            tmpStartSlot = .Item("start_slot")
    '                            tmpServiceID = .Item("item_id")
    '                            tmpServiceName = .Item("item_name")
    '                        Else
    '                            DTable.Rows(DTable.Rows.Count - 1)("service_id") += "," + tmpServiceID
    '                            DTable.Rows(DTable.Rows.Count - 1)("service_name") += "," + tmpServiceName
    '                        End If
    '                    End With
    '                End While
    '                objReader.Close()
    '                objConnShop.Close()
    '            Catch ex As OleDbException

    '            Catch ex As Exception

    '            End Try
    '        Next
    '    End If

    '    DTable.TableName = "AppointmentHistory"
    '    Return DTable
    'End Function


    'Public Function GetActiveAppointment(ByVal MobileNo As String) As Boolean
    '    Dim ret As Boolean = False
    '    Dim Dt As New DataTable
    '    Dt = FunctionEng.GetActiveShop()
    '    If Dt.Rows.Count > 0 Then
    '        Try
    '            For Each dr As DataRow In Dt.Rows
    '                Dim objConnShop As OleDbConnection = GetConnection(dr.Item("Shop_DB_Server"), dr.Item("Shop_DB_Name"), dr.Item("Shop_DB_UserID"), dr.Item("Shop_DB_PWD"))
    '                If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()

    '                Dim objCmd As New OleDbCommand("Select Count(*) From TB_Appointment_Customer Where CONVERT(varchar(16),start_slot,120) >= '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' and active_status = '1' ", objConnShop)
    '                If objCmd.ExecuteScalar > 0 Then ret = True
    '                If ret = True Then
    '                    Exit For
    '                End If
    '                objConnShop.Close()
    '            Next
    '        Catch ex As Exception

    '        End Try
    '    End If

    '    Return ret
    'End Function

    'Public Function GetAppointmentDesc(ByVal MobileNo As String) As DataTable
    '    Dim DTable As New DataTable
    '    DTable.Columns.Add("mobile_no")
    '    DTable.Columns.Add("slot_date")
    '    DTable.Columns.Add("slot_time")
    '    DTable.Columns.Add("shop_id")
    '    DTable.Columns.Add("shop_name")
    '    DTable.Columns.Add("service_id")
    '    DTable.Columns.Add("service_name")
    '    DTable.Columns.Add("status")

    '    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '    Dim Dt As New DataTable
    '    Dim objAdapter As New OleDbDataAdapter("Select * From TB_Shop Where active='Y'", objConn)
    '    Dim Ds As New DataSet
    '    Dim Drow As DataRow
    '    objAdapter.Fill(Ds, "Shop")
    '    Dt = Ds.Tables("Shop")

    '    If Dt.Rows.Count > 0 Then
    '        For Each dr As DataRow In Dt.Rows
    '            Dim objConnShop As OleDbConnection = GetConnection(dr.Item("Shop_DB_Server"), dr.Item("Shop_DB_Name"), dr.Item("Shop_DB_UserID"), dr.Item("Shop_DB_PWD"))
    '            If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '            Dim strSql As String = "select  ac.customer_id mobile_no, ac.start_slot, ac.item_id, s.item_name, " & _
    '                                   " case ac.active_status when '2' then 'Registered' when '3' then 'Complete' " & _
    '                                   " when '4' then 'Missed' when '5' then 'No Show' when '6' then 'Cancelled' " & _
    '                                   " end status from TB_APPOINTMENT_CUSTOMER ac  inner join TB_ITEM s on s.id=ac.item_id " & _
    '                                   " where CONVERT(varchar(16),ac.start_slot,120) >= '" & Today.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' " & _
    '                                   " and ac.customer_id = '" & MobileNo & "'  and ac.active_status = '1' "

    '            Dim objCmd As New OleDbCommand(strSql, objConnShop)
    '            Dim objReader As OleDbDataReader = objCmd.ExecuteReader()
    '            Dim tmpStartSlot As DateTime = New DateTime(1, 1, 1)

    '            While objReader.Read
    '                Dim tmpServiceID As String = ""
    '                Dim tmpServiceName As String = ""
    '                With objReader
    '                    tmpServiceID = .Item("item_id")
    '                    tmpServiceName = .Item("item_name")
    '                    If tmpStartSlot <> Convert.ToDateTime(.Item("start_slot")) Then
    '                        Drow = DTable.NewRow
    '                        Drow.Item("mobile_no") = .Item("mobile_no")
    '                        Drow.Item("slot_date") = Convert.ToDateTime(.Item("start_slot")).ToString("dd/MM/yy")
    '                        Drow.Item("slot_time") = Convert.ToDateTime(.Item("start_slot")).ToString("HH:mm")
    '                        Drow.Item("shop_id") = dr.Item("ID")
    '                        Drow.Item("shop_name") = dr.Item("shop_name_EN")
    '                        Drow.Item("service_id") = .Item("item_id")
    '                        Drow.Item("service_name") = .Item("item_name")
    '                        Drow.Item("status") = .Item("status")
    '                        DTable.Rows.Add(Drow)

    '                        tmpStartSlot = .Item("start_slot")
    '                        tmpServiceID = .Item("item_id")
    '                        tmpServiceName = .Item("item_name")
    '                    Else
    '                        DTable.Rows(DTable.Rows.Count - 1)("service_id") += "," + tmpServiceID
    '                        DTable.Rows(DTable.Rows.Count - 1)("service_name") += "," + tmpServiceName
    '                    End If
    '                End With
    '            End While
    '            objReader.Close()
    '            objConnShop.Close()
    '        Next
    '    End If

    '    DTable.TableName = "AppointmentDesc"
    '    Return DTable
    'End Function

    'Public Function RetriveItemByShop(ByVal Item_ID As String, ByVal shop_db_name As String, ByVal shop_db_userid As String, ByVal shop_db_pwd As String, ByVal shop_db_server As String) As DataTable
    '    Dim strSql As String = String.Empty
    '    Dim objConnShop As OleDbConnection = GetConnection(shop_db_server, shop_db_name, shop_db_userid, shop_db_pwd)
    '    If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '    Dim dt As New DataTable
    '    'strcommand = "select * from TB_ITEM where master_itemid in('" & master_itemid & "')"

    '    If Item_ID.Length > 0 Then
    '        'Dim objCmd As New OleDbCommand("Select Config_Value From SYSCONFIG Where Config_Name='Pre_Booking_Period'", objConn)
    '        Dim strDayBooking As String = GetQisDBConfig("Pre_Booking_Period")
    '        strSql = "select * from TB_ITEM inner join TB_APPOINTMENT_ITEM " & _
    '                 "on TB_ITEM.id=TB_APPOINTMENT_ITEM.item_id where TB_ITEM.active_status='1' " & _
    '                 "and convert(varchar(10),app_date,120)>=convert(varchar(10),GETDATE(),120) " & _
    '                 "and convert(varchar(10),app_date,120)<=convert(varchar(10),DATEADD(day," & strDayBooking & ",getdate()),120) " & _
    '                 "and master_itemid in(" & Item_ID & ")"
    '        Dim objAdapter As New OleDbDataAdapter(strSql, objConnShop)
    '        Dim Ds As New DataSet
    '        objAdapter.Fill(Ds, "Item")
    '        dt = Ds.Tables("Item")
    '    Else
    '        dt = Nothing
    '    End If

    '    Return dt
    'End Function

    Function RetriveItem_Applist(ByVal ItemID As String, ByVal shop_db_name As String, ByVal shop_db_userid As String, ByVal shop_db_pwd As String, ByVal shop_db_server As String) As DataTable
        Dim strSql As String = String.Empty
        Dim objConnShop As OleDbConnection = GetConnection(shop_db_server, shop_db_name, shop_db_userid, shop_db_pwd)
        If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
        Dim dt As New DataTable
        strSql = "select * from TB_ITEM where TB_ITEM.active_status='1' and id in(" & ItemID & ")"
        Dim objAdapter As New OleDbDataAdapter(strSql, objConnShop)
        Dim Ds As New DataSet
        objAdapter.Fill(Ds, "Item")
        dt = Ds.Tables("Item")
        Return dt
    End Function

    'Public Function GetQisDBConfig(ByVal ParaName As String) As String
    '    Dim ret As Boolean = False
    '    Dim strResult As String = ""
    '    Try
    '        If objConn.State = ConnectionState.Closed Then objConn.Open()
    '        Dim objCmd As New OleDbCommand("Select * From SysConfig Where Config_Name='" & ParaName & "'", objConn)
    '        Dim objReader As OleDbDataReader = objCmd.ExecuteReader()
    '        If objReader.Read Then
    '            ret = True
    '            strResult = objReader.Item("Config_Value")
    '        End If
    '        objReader.Close()
    '    Catch ex As Exception
    '    Finally
    '        objConn.Close()
    '    End Try

    '    If ret = True Then
    '        Return strResult
    '    Else
    '        Return ""
    '    End If
    'End Function

    'Public Function GetShopConfig(ByVal ParaName As String, ByVal objConnShop As OleDbConnection) As String
    '    Dim ret As Boolean = False
    '    Dim strResult As String = ""


    '    Try
    '        If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '        Dim objCmd As New OleDbCommand("Select * From TB_Setting Where Config_Name='" & ParaName & "'", objConnShop)
    '        Dim objReader As OleDbDataReader = objCmd.ExecuteReader()
    '        If objReader.Read Then
    '            ret = True
    '            strResult = objReader.Item("Config_Values")
    '        End If
    '        objReader.Close()
    '    Catch ex As Exception
    '    Finally
    '        objConn.Close()
    '    End Try

    '    If ret = True Then
    '        Return strResult
    '    Else
    '        Return ""
    '    End If
    'End Function

    'Public Function CancelAppointment(ByVal MobileNo As String) As Boolean
    '    Dim ret As Boolean = False

    '    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '    Dim Dt As New DataTable
    '    Dim objAdapter As New OleDbDataAdapter("Select * From TB_Shop Where active='Y'", objConn)
    '    Dim Ds As New DataSet
    '    objAdapter.Fill(Ds, "Shop")
    '    Dt = Ds.Tables("Shop")

    '    If Dt.Rows.Count > 0 Then
    '        For Each dr As DataRow In Dt.Rows
    '            Dim objConnShop As OleDbConnection = GetConnection(dr.Item("Shop_DB_Server"), dr.Item("Shop_DB_Name"), dr.Item("Shop_DB_UserID"), dr.Item("Shop_DB_PWD"))
    '            If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '            Dim strSql As String = "select * From TB_Appointment_Customer Where 1=1 "
    '            Dim CancelHour As Long = GetQisDBConfig("MaxEditAppointmentHour")
    '            Dim CancelBefore As DateTime = DateAdd(DateInterval.Hour, CancelHour, DateTime.Now)
    '            Dim whText As String = " And CONVERT(varchar(16),start_slot,120) >= '" & CancelBefore.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' " & _
    '                                   " and active_status = '1' and customer_id = '" & MobileNo & "' "
    '            objAdapter = New OleDbDataAdapter(strSql & whText, objConnShop)
    '            objAdapter.Fill(Ds, "Data")
    '            If Ds.Tables("Data").Rows.Count > 0 Then
    '                With Ds.Tables("Data").Rows(0)
    '                    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '                    Dim objTrans As OleDbTransaction = objConn.BeginTransaction
    '                    Dim objCmd As New OleDbCommand
    '                    Try
    '                        Dim StartSlot As String = .Item("START_SLOT").ToString("yyyy-MM-dd HH:mm") ', New Globalization.CultureInfo("en-US"))
    '                        strSql = " delete from tb_notify_joblist where shop_id='" & dr.Item("ID") & "' and mobile_no = '" & MobileNo & "' " & _
    '                                 " and convert(varchar(16),appointment_time, 120) = '" & StartSlot & "'"
    '                        objCmd.Connection = objConn
    '                        objCmd.Transaction = objTrans
    '                        objCmd.CommandText = strSql
    '                        objCmd.ExecuteNonQuery()

    '                        strSql = "update tb_appointment_customer set active_status = '6' where customer_id = '" & MobileNo & "' " & _
    '                                 "and CONVERT(varchar(16),start_slot,120) = '" & StartSlot & "'"
    '                        objCmd.CommandText = strSql
    '                        objCmd.ExecuteNonQuery()
    '                        objTrans.Commit()

    '                        Dim ChooseService = GetChooseService(MobileNo, StartSlot, "1", objConnShop)
    '                        If ClearTimeSlot(StartSlot, dr.Item("ID"), ChooseService, objConnShop) = True Then
    '                            Exit For
    '                        Else
    '                            ret = False
    '                        End If

    '                    Catch ex As Exception
    '                        objTrans.Rollback()
    '                    End Try
    '                End With
    '            End If
    '        Next
    '    End If

    '    Return ret
    'End Function

    'Public Function CreateTimeSlot(ByVal ShopID As Long) As DataTable
    '    Dim ret As New DataTable
    '    ret.Columns.Add("ShowDate", GetType(Date))
    '    ret.Columns.Add("ShowTime")
    '    ret.Columns.Add("status")

    '    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '    Dim Dt As New DataTable
    '    Dim objAdapter As New OleDbDataAdapter("Select * From TB_Shop Where active='Y' ID='" & ShopID & "'", objConn)
    '    Dim Ds As New DataSet
    '    objAdapter.Fill(Ds, "Shop")
    '    Dt = Ds.Tables("Shop")

    '    If Dt.Rows.Count > 0 Then
    '        Dim sAppointmentDate As Double = 7  'By Config
    '        Dim sDate As DateTime = DateTime.Now
    '        Dim eDate As DateTime = DateAdd(DateInterval.Day, sAppointmentDate, sDate)
    '        Dim objConnShop As OleDbConnection = GetConnection(Dt.Rows(0).Item("Shop_DB_Server"), Dt.Rows(0).Item("Shop_DB_Name"), Dt.Rows(0).Item("Shop_DB_UserID"), Dt.Rows(0).Item("Shop_DB_PWD"))
    '        If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '        If objConn.State = ConnectionState.Closed Then objConn.Open()
    '        'Dim objCmd As New OleDbCommand("Select Config_Value From SYSCONFIG Where Config_Name ='App_Before_Shop_Closed'", objConn)
    '        Dim kBeforeClose As Double = GetQisDBConfig("App_Before_Shop_Closed") 'GetShopConfig("k_before_close", objConnShop)

    '        Do
    '            Dim strDate As String = sDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
    '            Dim strSql As String = ""
    '            ''******************* Create Slot *********************

    '            strSql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & strDate & "') = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & strDate & "',app_date)=0"
    '            objAdapter = New OleDbDataAdapter(strSql, objConnShop)
    '            objAdapter.Fill(Ds, "Slot")
    '            Dt = Ds.Tables("Slot")

    '            'Gen Slot
    '            If Dt.Rows.Count > 0 Then
    '                If objConn.State = ConnectionState.Closed Then objConn.Open()
    '                Dim objCmd As New OleDbCommand("Select GetDate()", objConn)

    '                Dim date_now As String = objCmd.ExecuteScalar
    '                'Loop in dataTable For add slot
    '                Dim StartTime As Date = CDate(Dt.Rows(0).Item("start_time").ToString).ToShortTimeString
    '                Dim EndTime As Date = CDate(Dt.Rows(0).Item("end_time").ToString).ToShortTimeString
    '                Dim SlotTime As Date = StartTime
    '                Dim Slot As Integer = Dt.Rows(0).Item("slot")
    '                'Add slot
    '                Do
    '                    If CDate(CDate(SlotTime).ToShortTimeString) >= CDate(CDate(date_now).ToShortTimeString) Then
    '                        Dim dr As DataRow = ret.NewRow
    '                        dr("ShowDate") = sDate
    '                        dr("ShowTime") = SlotTime.ToString("HH:mm")
    '                        ret.Rows.Add(dr)
    '                    End If
    '                    SlotTime = DateAdd(DateInterval.Minute, Slot, SlotTime)
    '                Loop While CDate(SlotTime) <= CDate(EndTime)
    '            End If

    '            Dim kBeforeApp As Double = GetQisDBConfig("Pre_Booking_Period") * 60 'GetShopConfig("k_before_app", objConnShop)
    '            Dim kBeforeAppDay As Double = GetQisDBConfig("Length_Of_Pre_Booking")
    '            'ถ้าเป็นวันที่ปัจจุบัน
    '            If strDate = DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) Then
    '                strSql = "select app_date, convert(varchar(5),slot_time,114) as time,case when in_use = capacity then 1 else 0 end as status from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & strDate & "') = 0 and slot_time > DATEADD(MINUTE," & kBeforeApp & ",'" & sDate.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "') And app_date <= DateAdd(day," & kBeforeAppDay & "getdate()"
    '            Else
    '                strSql = "select app_date, convert(varchar(5),slot_time,114) as time,case when in_use = capacity then 1 else 0 end as status from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & strDate & "') = 0 "
    '            End If
    '            objAdapter = New OleDbDataAdapter(strSql, objConnShop)
    '            objAdapter.Fill(Ds, "ShopSlot")

    '            Dt = Ds.Tables("ShopSlot")
    '            If Dt.Rows.Count > 0 Then
    '                For i As Int32 = 0 To Dt.Rows.Count - 1
    '                    If Dt.Rows(i).Item("status") = 0 Then
    '                        For j As Integer = 0 To ret.Rows.Count - 1
    '                            Dim dr As DataRow = ret.Rows(j)
    '                            If dr("ShowTime") = Dt.Rows(i)("time") And Convert.ToDateTime(dr("ShowDate")).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) = Convert.ToDateTime(Dt.Rows(i)("app_date")).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) Then
    '                                ret.Rows(j)("status") = "ว่าง"
    '                            End If
    '                        Next
    '                    End If
    '                Next
    '            End If
    '            sDate = DateAdd(DateInterval.Day, 1, sDate)
    '        Loop While sDate <= eDate

    '    End If

    '    ret.TableName = "CreateTimeSlot"
    '    Return ret
    'End Function

    'Private Function ClearTimeSlot(ByVal SlotDateTime As DateTime, ByVal ShopID As Long, ByVal ChooseService As String, ByVal objConnShop As OleDbConnection) As Boolean
    '    Dim ret As Boolean = False

    '    Dim AllService() As String = Split(ChooseService, ",")
    '    Dim CountService As Int32 = AllService.Length
    '    Dim Ds As New DataSet
    '    If objConnShop IsNot Nothing Then
    '        If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '        Dim kBeforeClose As Double = GetShopConfig("k_before_close", objConnShop)
    '        Dim strSql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "') = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & kBeforeClose & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "',app_date)=0"
    '        Dim objAdapter As New OleDbDataAdapter(strSql, objConnShop)
    '        objAdapter.Fill(Ds, "Schedule")
    '        Dim slDT As DataTable = Ds.Tables("Schedule")
    '        If slDT.Rows.Count > 0 Then
    '            Dim Slot As Long = slDT.Rows(0)("slot")

    '            strSql = "Select * From TB_Appointment_Slot Where DATEDIFF(d,app_date,'" & SlotDateTime.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "') = 0 and CONVERT(varchar(5),slot_time,114) between '" & SlotDateTime.ToString("HH:mm") & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, SlotDateTime).Minute.ToString.PadLeft(2, "0") & "'"
    '            objAdapter = New OleDbDataAdapter(strSql, objConnShop)
    '            objAdapter.Fill(Ds, "Data")
    '            Dim dt As DataTable = Ds.Tables("Data")
    '            If dt.Rows.Count > 0 Then
    '                For Each dr As DataRow In dt.Rows
    '                    strSql = "Update TB_Appointment_Slot Set In_Use=In_Use-1 Where Id='" & dr.Item("id") & "'"
    '                    Dim objCmd As New OleDbCommand(strSql, objConnShop)
    '                    objCmd.ExecuteNonQuery()
    '                Next
    '            End If
    '        End If
    '    End If
    '    objConnShop.Close()
    '    Return ret
    'End Function

    'Private Function GetChooseService(ByVal MobileNo As String, ByVal StartDateTime As String, ByVal ActiveStatus As String, ByVal objConnShop As OleDbConnection) As String
    '    Dim ret As String = ""

    '    If objConnShop IsNot Nothing Then
    '        Dim strSql As String = "Select * From TB_Appointment_Customer Where CONVERT(varchar(16),start_slot,120) = '" & StartDateTime & "' " & _
    '                               " and active_status = '" & ActiveStatus & "' and customer_id = '" & MobileNo & "' "
    '        Dim Ds As New DataSet
    '        If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '        Dim objAdapter As New OleDbDataAdapter(strSql, objConnShop)
    '        objAdapter.Fill(Ds, "Cus")
    '        Dim dt As DataTable = Ds.Tables("Cus")
    '        If dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                If ret = "" Then
    '                    ret = dr("item_id")
    '                Else
    '                    ret += "," & dr("item_id")
    '                End If
    '            Next
    '        End If
    '    End If
    '    objConnShop.Close()
    '    Return ret
    'End Function

    'Public Function SendEmailConfirm(ByVal MailTo As String, ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As Boolean
    '    Return SendEmail(MailTo, "Confirm Appointment", CreateMailConfirm(ServiceID, MobileNo, AppointmentTime, PreLang, ShopID))
    'End Function

    'Private Function CreateMailConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As String
    '    'Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
    '    'Dim trans As New ShLinqDB.Common.Utilities.TransactionDB
    '    'trans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME)
    '    'Dim lnq As New ShLinqDB.TABLE.TbCustomerShLinqDB

    '    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '    Dim Dt As New DataTable
    '    Dim objAdapter As New OleDbDataAdapter("Select * From TB_Shop Where active='Y' ID='" & ShopID & "'", objConn)
    '    Dim Ds As New DataSet
    '    objAdapter.Fill(Ds, "Shop")
    '    Dt = Ds.Tables("Shop")
    '    Dim objConnShop As OleDbConnection = GetConnection(Dt.Rows(0).Item("Shop_DB_Server"), Dt.Rows(0).Item("Shop_DB_Name"), Dt.Rows(0).Item("Shop_DB_UserID"), Dt.Rows(0).Item("Shop_DB_PWD"))
    '    Dim ServiceName As String = ""
    '    If Dt.Rows.Count > 0 Then
    '        Dim sAppointmentDate As Double = 7  'By Config
    '        Dim sDate As DateTime = DateTime.Now
    '        Dim eDate As DateTime = DateAdd(DateInterval.Day, sAppointmentDate, sDate)

    '        Try
    '            If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()
    '            Dim SerIDList() As String = Split(ServiceID, ",")
    '            For Each sID As Long In SerIDList
    '                'Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
    '                objAdapter = New OleDbDataAdapter("Select * From TB_Item Where ID='" & sID & "'", objConnShop)
    '                objAdapter.Fill(Ds, "Item")

    '                If Ds.Tables("Item").Rows.Count <> 0 Then
    '                    If PreLang.ToUpper().IndexOf("ENG") > -1 Then
    '                        If ServiceName = "" Then
    '                            ServiceName += Ds.Tables("Item").Rows(0).Item("ITEM_NAME")
    '                        Else
    '                            ServiceName += "," & Ds.Tables("Item").Rows(0).Item("ITEM_NAME")
    '                        End If
    '                    Else
    '                        If ServiceName = "" Then
    '                            ServiceName += Ds.Tables("Item").Rows(0).Item("ITEM_NAME_TH")
    '                        Else
    '                            ServiceName += "," & Ds.Tables("Item").Rows(0).Item("ITEM_NAME_TH")
    '                        End If
    '                    End If
    '                End If
    '                Ds.Tables("Item").Dispose()

    '            Next

    '        Catch ex As Exception
    '        Finally
    '            objConnShop.Close()
    '        End Try
    '    End If



    '    Dim ret As String = ""
    '    Dim ShopName As String = ""
    '    'If PreLang.ToUpper().IndexOf("ENG") > -1 Then
    '    '    'datetime.Now.ToString("ddd dMMMyy",New CultureInfo("en-US"))
    '    '    ShopName = FunctionEng.GetShopConfig("s_name_en", trans)
    '    '    ret += "You have an appointment for " & ServiceName
    '    '    ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy, hh:mmtt", New System.Globalization.CultureInfo("en-US")) & ","
    '    '    ret += " To change/cancel,please make changes " & (Convert.ToInt64(FunctionEng.GetShopConfig("k_cancel", trans)) / 60) & "hr in advance."
    '    'Else
    '    '    ShopName = FunctionEng.GetShopConfig("s_name_th", trans)
    '    '    ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " " & AppointmentTime.ToString("dddd dMMMyy", New System.Globalization.CultureInfo("th-TH"))
    '    '    ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. หากเปลี่ยน/ยกเลิก กรุณาแจ้งAIS"
    '    '    ret += " ก่อนถึงเวลานัด" & (Convert.ToInt64(FunctionEng.GetShopConfig("k_cancel", trans)) / 60) & "ชม."
    '    'End If


    '    If PreLang.ToUpper().IndexOf("ENG") > -1 Then
    '        Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(GetQisDBConfig("Cancel_Before_Reserv") * 60), AppointmentTime).ToString("HH:mmtt")
    '        ShopName = GetShopConfig("s_name_en", objConnShop)
    '        ret += "You have an appointment for " & ServiceName
    '        ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy, hh:mmtt", New System.Globalization.CultureInfo("en-US")) & ","
    '        ret += " To change/cancel,please make changes " & TimeBefore & " in advance."
    '    Else
    '        Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(GetQisDBConfig("Cancel_Before_Reserv") * 60), AppointmentTime).ToString("HH:mm")
    '        ShopName = GetShopConfig("s_name_th", objConnShop)
    '        ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " " & AppointmentTime.ToString("dddd dMMMyy", New System.Globalization.CultureInfo("th-TH"))
    '        ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. หากเปลี่ยน/ยกเลิก กรุณาแจ้งAIS"
    '        ret += " ภายในเวลา" & TimeBefore & "น."
    '    End If
    '    'trans.CommitTransaction()

    '    Return ret
    'End Function

    'Public Function SendSMSConfirm(ByVal MobileNo As String, ByVal ServiceID As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As GateWay.SMSResponsePara

    '    Return SendSMS(MobileNo, CreateSMSConfirm(ServiceID, MobileNo, AppointmentTime, PreLang, ShopID))
    'End Function

    'Private Function CreateSMSConfirm(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As String
    '    If objConn.State = ConnectionState.Closed Then objConn.Open()
    '    Dim Dt As New DataTable
    '    Dim objAdapter As New OleDbDataAdapter("Select * From TB_Shop Where active='Y' ID='" & ShopID & "'", objConn)
    '    Dim Ds As New DataSet
    '    objAdapter.Fill(Ds, "Shop")
    '    Dt = Ds.Tables("Shop")
    '    Dim objConnShop As OleDbConnection = GetConnection(Dt.Rows(0).Item("Shop_DB_Server"), Dt.Rows(0).Item("Shop_DB_Name"), Dt.Rows(0).Item("Shop_DB_UserID"), Dt.Rows(0).Item("Shop_DB_PWD"))

    '    Dim ServiceName As String = ""
    '    Dim SerIDList() As String = Split(ServiceID, ",")
    '    If objConnShop.State = ConnectionState.Closed Then objConnShop.Open()

    '    Try
    '        For Each sID As Long In SerIDList
    '            'Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
    '            objAdapter = New OleDbDataAdapter("Select * From TB_Item Where ID='" & sID & "'", objConnShop)
    '            objAdapter.Fill(Ds, "Item")
    '            If Ds.Tables("Item").Rows.Count <> 0 Then
    '                If PreLang.ToUpper().IndexOf("THAI") > -1 Then
    '                    If ServiceName = "" Then
    '                        ServiceName += Ds.Tables("Item").Rows(0).Item("ITEM_NAME_TH")
    '                    Else
    '                        ServiceName += "," & Ds.Tables("Item").Rows(0).Item("ITEM_NAME_TH")
    '                    End If
    '                Else
    '                    If ServiceName = "" Then
    '                        ServiceName += Ds.Tables("Item").Rows(0).Item("ITEM_NAME")
    '                    Else
    '                        ServiceName += "," & Ds.Tables("Item").Rows(0).Item("ITEM_NAME")
    '                    End If
    '                End If
    '            End If
    '            Ds.Tables("Item").Dispose()
    '        Next

    '    Catch ex As Exception
    '    Finally
    '        objConnShop.Close()
    '    End Try

    '    Dim ret As String = ""
    '    Dim ShopName As String = ""
    '    If PreLang.ToUpper().IndexOf("THAI") > -1 Then
    '        Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(GetShopConfig("k_cancel", objConnShop)), AppointmentTime).ToString("HH:mm")
    '        ShopName = GetShopConfig("s_name_th", objConnShop)
    '        ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " " & AppointmentTime.ToString("dddd dMMMyy", New System.Globalization.CultureInfo("th-TH"))
    '        ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. หากเปลี่ยน/ยกเลิก กรุณาแจ้งAIS"
    '        ret += " ภายในเวลา" & TimeBefore & "น."
    '    Else
    '        Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(GetShopConfig("k_cancel", objConnShop)), AppointmentTime).ToString("HH:mmtt")
    '        ShopName = GetShopConfig("s_name_en", objConnShop)
    '        ret += "You have an appointment for " & ServiceName
    '        ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy, hh:mmtt", New System.Globalization.CultureInfo("en-US")) & ","
    '        ret += " To change/cancel,please make changes " & TimeBefore & " in advance."
    '    End If
    '    'trans.CommitTransaction()

    '    Return ret
    'End Function

    Public Function SaveTransLog(ByVal ClassName As String, ByVal transDesc As String) As Boolean
        'If objConn.State = ConnectionState.Closed Then objConn.Open()
        'Dim objTrans As OleDbTransaction = objConn.BeginTransaction
        'Try
        '    Dim strSql As String = "Insert Into LOG_TRANS (LOGIN_HIS_ID,TRANS_DESC,TRANS_DATE,CREATE_BY,CREATE_DATE) Values (" & _
        '                           "'0','" & transDesc & "','" & DateTime.Now & "','" & ClassName & "','" & DateTime.Now & "')"
        '    Dim objCmd As New OleDbCommand
        '    objCmd.Connection = objConn
        '    objCmd.Transaction = objTrans
        '    objCmd.CommandText = strSql
        '    objCmd.ExecuteNonQuery()
        '    objTrans.Commit()
        '    Return True
        'Catch ex As Exception
        '    objTrans.Rollback()
        '    Return False
        'Finally
        '    objConn.Close()
        'End Try
        FunctionEng.SaveTransLog(ClassName, transDesc)

    End Function

    Public Function SaveErrorLog(ByVal ClassName As String, ByVal ErrorDesc As String) As Boolean
        'If objConn.State = ConnectionState.Closed Then objConn.Open()
        'Dim objTrans As OleDbTransaction = objConn.BeginTransaction
        'Try
        '    Dim strSql As String = "Insert Into LOG_ERROR (LOGIN_HIS_ID,ERROR_DESC,ERROR_time,CREATE_BY,CREATE_DATE) Values (" & _
        '                           "'0','" & ErrorDesc & "','" & DateTime.Now & "','" & ClassName & "','" & DateTime.Now & "')"
        '    Dim objCmd As New OleDbCommand
        '    objCmd.Connection = objConn
        '    objCmd.Transaction = objTrans
        '    objCmd.CommandText = strSql
        '    objCmd.ExecuteNonQuery()
        '    objTrans.Commit()
        '    Return True
        'Catch ex As Exception
        '    objTrans.Rollback()
        '    Return False
        'Finally
        '    objConn.Close()
        'End Try
        FunctionEng.SaveErrorLog(ClassName, ErrorDesc)
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
            Return y.ToString & "-" & m.ToString.PadLeft(2, "0") & "-" & d.ToString.PadLeft(2, "0")
        Else
            Return ""
        End If
    End Function

    Public Function cStrToDate(ByVal StringDate As String) As Date 'Convert วันที่จาก yyyy-MM-dd เป็น Date
        Dim ret As New Date(1, 1, 1)
        If StringDate.Trim <> "" Then
            Dim vDate() As String = Split(StringDate, "-")
            ret = New Date(vDate(0), vDate(1), vDate(2))
        End If
        Return ret
    End Function

    Public Function cStrToDateTime(ByVal StrDate As String, ByVal StrTime As String) As DateTime 'Convert วันที่จาก yyyy-MM-dd HH:mm เป็น DateTime
        Dim ret As New Date(1, 1, 1)
        If StrDate.Trim <> "" Then
            Dim vDate() As String = Split(StrDate, "-")
            If StrTime.Trim <> "" Then
                Dim vTime() As String = Split(StrTime, ":")
                ret = New Date(vDate(0), vDate(1), vDate(2), vTime(0), vTime(1), 0)
            Else
                ret = New Date(vDate(0), vDate(1), vDate(2))
            End If
        End If
        Return ret
    End Function
End Class
