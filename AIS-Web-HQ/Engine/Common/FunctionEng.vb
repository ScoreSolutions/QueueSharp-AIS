Imports Cen = CenLinqDB.TABLE
Imports Sh = ShLinqDB.TABLE
Imports pSh = ShParaDB.TABLE
Imports CenLinqDB.Common.Utilities
Imports uSh = ShLinqDB.Common.Utilities
Imports System.Web
Imports CenLinqDB.TABLE
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Namespace Common
    Public Class FunctionEng
        Public Shared Function GetQisDBConfig(ByVal ParaName As String) As String
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New Cen.SysconfigCenLinqDB

            ret = lnq.ChkDataByCONFIG_NAME(ParaName, trans.Trans)
            trans.CommitTransaction()
            If ret = True Then
                Return lnq.CONFIG_VALUE
            Else
                Return ""
            End If
        End Function

        Public Shared Function GetShopConfig(ByVal ParaName As String, ByVal ShTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As Boolean = False
            Dim lnq As New ShLinqDB.TABLE.TbSettingShLinqDB
            ret = lnq.ChkDataByWhere("config_name = '" & ParaName & "'", ShTrans.Trans)
            If ret = True Then
                Return lnq.CONFIG_VALUE
            Else
                Return ""
            End If
        End Function

        Public Shared Function GetTbShopCenLinqDB(ByVal ShopID As Long) As TbShopCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            lnq.GetDataByPK(ShopID, trans.Trans)
            'lnq.SHOP_DB_SERVER = trans.conn.DataSource
            trans.CommitTransaction()

            Return lnq
        End Function

        Public Shared Function GetTbShopCenLinqDBByShopAbb(ByVal ShopAbb As String) As TbShopCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            lnq.ChkDataByWhere("shop_abb='" & ShopAbb & "'", trans.Trans)
            'lnq.SHOP_DB_SERVER = trans.conn.DataSource
            trans.CommitTransaction()

            Return lnq
        End Function

        Public Shared Function GetTbShopCenParaDB(ByVal ShopID As Long) As CenParaDB.TABLE.TbShopCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            Dim p As CenParaDB.TABLE.TbShopCenParaDB = lnq.GetParameter(ShopID, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return p
        End Function

        Public Shared Function GetShopRegion(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable

            Dim sql As String = "select s.*,r.id region_id, r.region_code,r.region_name_th, r.region_name_en, "
            sql += " r.location_group "
            sql += " from tb_shop s"
            sql += " inner join tb_region r on r.id=s.region_id"
            sql += " where s.id = '" & ShopID & "'"
            sql += " order by s.shop_name_en"
            dt = FunctionEng.ExecuteDataTable(sql)
            

            Return dt
        End Function

        Public Shared Function GetShopItemPara(ByVal ItemID As Integer, ByVal ShTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShLinqDB.TABLE.TbItemShLinqDB
            Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
            Return lnq.GetDataByPK(ItemID, ShTrans.Trans)
        End Function

        Public Shared Function GetShTransction(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As ShLinqDB.Common.Utilities.TransactionDB
            Dim retTrans As Boolean = False
            shTrans = New ShLinqDB.Common.Utilities.TransactionDB
            retTrans = shTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME)
            If retTrans = False Then
                FunctionEng.SaveErrorLog("FunctionENG.GetShTransction", "Connect to DR Site")
                retTrans = shTrans.CreateTransaction(shLnq.SHOP_DR_SERVER, shLnq.SHOP_DR_USERID, shLnq.SHOP_DR_PWD, shLnq.SHOP_DR_NAME)
            End If

            Return shTrans
        End Function

        Public Shared Function GetShTransction(ByVal ShopID As Long, ByVal ClassName As String) As ShLinqDB.Common.Utilities.TransactionDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB

            Dim retTrans As Boolean = False
            retTrans = shTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME)
            If retTrans = False Then
                FunctionEng.SaveErrorLog(ClassName, "Connect to DR Site : " & shTrans.ErrorMessage & " ### : " & shLnq.SHOP_ABB)
                retTrans = shTrans.CreateTransaction(shLnq.SHOP_DR_SERVER, shLnq.SHOP_DR_USERID, shLnq.SHOP_DR_PWD, shLnq.SHOP_DR_NAME)
            End If
            trans.CommitTransaction()
            shLnq = Nothing

            Return shTrans
        End Function

        Public Shared Function GetIPAddress() As String
            Dim oAddr As System.Net.IPAddress
            Dim sAddr As String
            With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
                oAddr = New System.Net.IPAddress(.AddressList(0).Address)
                sAddr = oAddr.ToString
            End With
            GetIPAddress = sAddr
        End Function

        Public Shared Function SaveTransLog(ByVal ClassName As String, ByVal transDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogTransCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = 0
            lnq.TRANS_DESC = transDesc
            lnq.TRANS_DATE = DateTime.Now

            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                SaveErrorLog(ClassName, lnq.ErrorMessage)
            End If
            lnq = Nothing

            Return ret
        End Function
        Public Shared Function SaveTransLog(ByVal LoginHisID As Long, ByVal ClassName As String, ByVal transDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogTransCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = LoginHisID
            lnq.TRANS_DESC = transDesc
            lnq.TRANS_DATE = DateTime.Now

            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                SaveErrorLog(LoginHisID, ClassName, lnq.ErrorMessage)
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Shared Function SaveErrorLog(ByVal ClassName As String, ByVal ErrorDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogErrorCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = 0
            lnq.ERROR_DESC = ErrorDesc
            lnq.ERROR_TIME = DateTime.Now
            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Shared Function SaveErrorLog(ByVal LoginHisID As Long, ByVal ClassName As String, ByVal ErrorDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogErrorCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = LoginHisID
            lnq.ERROR_DESC = ErrorDesc
            lnq.ERROR_TIME = DateTime.Now
            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing

            Return ret
        End Function
        Public Shared Function GetActiveShop() As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim sql As String = "select s.*, "
            sql += " r.location_group, r.region_name_en,r.region_name_th, r.region_code"
            sql += " from tb_shop s"
            sql += " inner join tb_region r on r.id=s.region_id"
            sql += " where s.active='Y'"
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim dt As DataTable = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function
        Public Shared Function GetActiveShopDDL() As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim sql As String = " select id, shop_code + ' ' + shop_name_en shop_name from tb_shop where active='Y' order by shop_code"
            Dim dt As DataTable = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Shared Function GetActiveSkill() As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("active='Y'", "", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Shared Function GetMonthNameEN(ByVal MonthNo As Integer) As String
            Dim month As String = ""
            Select Case MonthNo
                Case 1
                    month = "January"
                Case 2
                    month = "February"
                Case 3
                    month = "March"
                Case 4
                    month = "April"
                Case 5
                    month = "May"
                Case 6
                    month = "June"
                Case 7
                    month = "July"
                Case 8
                    month = "August"
                Case 9
                    month = "September"
                Case 10
                    month = "October"
                Case 11
                    month = "November"
                Case 12
                    month = "December"
            End Select
            Return month
        End Function

        Public Shared Function GetUploadPath() As String
            Dim fldPath As String = System.Configuration.ConfigurationSettings.AppSettings("UploadPath").ToString
            If System.IO.Directory.Exists(fldPath) = False Then
                System.IO.Directory.CreateDirectory(fldPath)
            End If
            Return fldPath
        End Function

        Public Shared Function GetFaultMngUploadPath() As String
            Dim fldPath As String = System.Configuration.ConfigurationSettings.AppSettings("FaultMngUploadPath").ToString
            If System.IO.Directory.Exists(fldPath) = False Then
                System.IO.Directory.CreateDirectory(fldPath)




            End If
            Return fldPath
        End Function
        Public Shared Function GetFaultMngMonitorPath() As String
            Dim fldPath As String = System.Configuration.ConfigurationSettings.AppSettings("FaultMngMonitorPath").ToString
            If System.IO.Directory.Exists(fldPath) = False Then
                System.IO.Directory.CreateDirectory(fldPath)
            End If
            Return fldPath
        End Function
        Public Shared Function GetFaultMngConfigPath() As String
            Dim fldPath As String = System.Configuration.ConfigurationSettings.AppSettings("FaultMngConfigPath").ToString
            If System.IO.Directory.Exists(fldPath) = False Then
                System.IO.Directory.CreateDirectory(fldPath)
            End If
            Return fldPath
        End Function

        Public Shared Function ExecuteShopSQL(ByVal Sql As String, ByVal ShopID As Long, ByVal ClassName As String) As DataTable
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = GetShTransction(ShopID, ClassName)
            Dim dt As New DataTable
            If shTrans IsNot Nothing Then
                dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(Sql, shTrans.Trans)
            End If
            shTrans.CommitTransaction()

            Return dt
        End Function
        Public Shared Function GetDatatable(ByVal Sql As String) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            If trans.Trans IsNot Nothing Then
                dt = SqlDB.ExecuteTable(Sql, trans.Trans)
            End If
            trans.CommitTransaction()

            Return dt
        End Function

        Public Shared Function ExecuteDataReader(ByVal sql As String) As SqlClient.SqlDataReader
            Dim dr As SqlClient.SqlDataReader
            dr = SqlDB.ExecuteReader(sql)
            Return dr
        End Function

        Public Shared Function ExecuteDataTable(ByVal sql As String) As DataTable
            Dim dt As New DataTable
            dt = SqlDB.ExecuteTable(sql)
            Return dt
        End Function

        Public Shared Function GetDateNowFromDB() As Date
            Dim ret As String
            Dim dt As DataTable = SqlDB.ExecuteTable("select getdate() date_now")
            ret = dt.Rows(0)("date_now")
            Return ret
        End Function

        Public Shared Function cStrToDate(ByVal StringDate As String) As Date 'Convert วันที่จาก yyyy-MM-dd เป็น Date
            Dim ret As New Date(1, 1, 1)
            If StringDate.Trim <> "" Then
                Dim vDate() As String = Split(StringDate, "-")
                ret = New Date(vDate(0), vDate(1), vDate(2))
            End If
            Return ret
        End Function

        Public Shared Function cStrToDate2(ByVal StringDate As String) As Date 'Convert วันที่จาก dd/MM/yyyy เป็น Date
            Dim ret As New Date(1, 1, 1)
            If StringDate.Trim <> "" Then
                Dim vDate() As String = Split(StringDate, "/")
                ret = New Date(vDate(2), vDate(1), vDate(0))
            End If
            Return ret
        End Function
        Public Shared Function cStrToDate3(ByVal StringDate As String) As Date 'Convert วันที่จาก yyyyMMdd เป็น Date
            Dim ret As New Date(1, 1, 1)
            If StringDate.Trim <> "" Then
                Dim yy As String = Left(StringDate, 4)
                Dim mm As String = StringDate.Substring(4, 2)
                Dim dd As String = Right(StringDate, 2)
                ret = New Date(yy, mm, dd)
            End If
            Return ret
        End Function

#Region " Encrypt/Decrypt "
        Public Shared Function EnCripPwd(ByVal passString As String) As String
            Return CenLinqDB.Common.Utilities.SqlDB.EnCripPwd(passString)
        End Function

        Public Shared Function DeCripPwd(ByVal passString As String) As String
            Return CenLinqDB.Common.Utilities.SqlDB.DeCripPwd(passString)
        End Function
#End Region

        '#Region " Encrypt/Decrypt "
        '        Private Shared mSEncryptionKey As String = "encryptstring"
        '        Private Shared mIV() As Byte = {&H25, &H29, &H93, &H27, &H52, &HFD, &HAE, &HBC}
        '        Private Shared mkey() As Byte = {}

        '        Public Shared Function EnCripPwd(ByVal pwd As String) As String
        '            Try
        '                mkey = System.Text.Encoding.UTF8.GetBytes(Left(mSEncryptionKey, 8))
        '                Dim des As New DESCryptoServiceProvider()
        '                ' convert our input string to a byte array
        '                Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(pwd)
        '                'now encrypt the bytearray
        '                Dim ms As New MemoryStream()
        '                Dim cs As New CryptoStream(ms, des.CreateEncryptor(mkey, mIV), CryptoStreamMode.Write)
        '                cs.Write(inputByteArray, 0, inputByteArray.Length)
        '                cs.FlushFinalBlock()
        '                ' now return the byte array as a "safe for XMLDOM" Base64 String
        '                Return Convert.ToBase64String(ms.ToArray())
        '            Catch e As Exception
        '                Return e.Message
        '            End Try
        '        End Function

        '        Public Shared Function DeCripPwd(ByVal pwd As String) As String
        '            Dim inputByteArray(pwd.Length) As Byte
        '            ' Note: The DES CryptoService only accepts certain key byte lengths
        '            ' We are going to make things easy by insisting on an 8 byte legal key length

        '            Try
        '                mkey = System.Text.Encoding.UTF8.GetBytes(Left(mSEncryptionKey, 8))
        '                Dim des As New DESCryptoServiceProvider()
        '                ' we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
        '                inputByteArray = Convert.FromBase64String(pwd)
        '                ' now decrypt the regular string
        '                Dim ms As New MemoryStream()
        '                Dim cs As New CryptoStream(ms, des.CreateDecryptor(mkey, mIV), CryptoStreamMode.Write)
        '                cs.Write(inputByteArray, 0, inputByteArray.Length)
        '                cs.FlushFinalBlock()
        '                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
        '                Return encoding.GetString(ms.ToArray())
        '            Catch e As Exception
        '                Return e.Message
        '            End Try
        '        End Function
        '#End Region
    End Class
End Namespace

