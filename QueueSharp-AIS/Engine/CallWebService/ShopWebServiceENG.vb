Imports ShLinqDB.Common.Utilities
Imports ShLinqDB.TABLE
Imports ShParaDb.TABLE
Imports ShParaDb.ShopWebService
Imports System.IO
Imports System.Net
Imports System.Text
Imports Engine.Common
Imports System.Globalization
Imports SHDocVw
Imports System.Threading
Imports System.Web
Imports ShParaDb.Common.Utilities

Namespace CallWebService
    Public Class ShopWebServiceENG
        Dim _err As String = ""
        Public INIFileName As String = "C:\Windows\QueueSharp.ini"  'Parth ที่ใช้เก็บไฟล์ .ini สำหรับการ Connect Database ที่ Shop

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

#Region "Customer Profile"
   
        Public Function GetCustomerProfileFromDC(ByVal MobileNo As String) As TbCustomerShParaDB
            'ถ้าดึงจาก GW ไม่ได้ ก็ให้ดึงจาก DB ของ Central แล้วค่อยมาดึงที่ Shop
            'สำหรับ Lab Test
            Dim ret As New TbCustomerShParaDB
            Dim retTrans As Boolean = False
            Dim trans As New TransactionDB(False)
            retTrans = trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainUsername & ";Password=" & SqlDB.HQMainPassword)
            If retTrans = False Then
                ret.RESPONSE_MSG = "Not Connect to Main Site"
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCustomerProfileFromDC", ret.RESPONSE_MSG)

                'ถ้ายังไม่ได้อีกก็ให้ Connect มาที่ Shop DB แทน
                retTrans = trans.CreateTransaction()
                Dim lnq As New TbCustomerShLinqDB
                If retTrans = True Then
                    ret = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                    ret.RESPONSE_MSG = lnq.ErrorMessage
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCustomerProfileFromDC", ret.RESPONSE_MSG)
                End If
            Else
                Dim dt As New DataTable
                dt = SqlDB.ExecuteTable("select * from tb_customer where mobile_no = '" & MobileNo & "'", trans.Trans)
                ret = BuldFromDT(dt)
                trans.CommitTransaction()
                If dt.Rows.Count = 0 Then
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCustomerProfileFromDC", ret.RESPONSE_MSG & " Mobile No : " & MobileNo)
                Else
                    UpdateCustomerProfile(ret)
                End If
            End If

            Return ret
        End Function

        Private Function BuldFromDT(ByVal dt As DataTable) As TbCustomerShParaDB
            Dim ret As New TbCustomerShParaDB
            If dt.Rows.Count > 0 Then
                Dim Rdr As DataRow = dt.Rows(0)
                If Convert.IsDBNull(Rdr("id")) = False Then ret.ID = Convert.ToInt64(Rdr("id"))
                If Convert.IsDBNull(Rdr("title_name")) = False Then ret.TITLE_NAME = Rdr("title_name").ToString()
                If Convert.IsDBNull(Rdr("f_name")) = False Then ret.F_NAME = Rdr("f_name").ToString()
                If Convert.IsDBNull(Rdr("l_name")) = False Then ret.L_NAME = Rdr("l_name").ToString()
                If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.MOBILE_NO = Rdr("mobile_no").ToString()
                If Convert.IsDBNull(Rdr("mobile_status")) = False Then ret.MOBILE_STATUS = Rdr("mobile_status").ToString()
                If Convert.IsDBNull(Rdr("email")) = False Then ret.EMAIL = Rdr("email").ToString()
                If Convert.IsDBNull(Rdr("segment_level")) = False Then ret.SEGMENT_LEVEL = Rdr("segment_level").ToString()
                If Convert.IsDBNull(Rdr("birth_date")) = False Then ret.BIRTH_DATE = Rdr("birth_date").ToString()
                If Convert.IsDBNull(Rdr("category")) = False Then ret.CATEGORY = Rdr("category").ToString()
                If Convert.IsDBNull(Rdr("account_balance")) = False Then ret.ACCOUNT_BALANCE = Rdr("account_balance").ToString()
                If Convert.IsDBNull(Rdr("contact_class")) = False Then ret.CONTACT_CLASS = Rdr("contact_class").ToString()
                If Convert.IsDBNull(Rdr("service_year")) = False Then ret.SERVICE_YEAR = Rdr("service_year").ToString()
                If Convert.IsDBNull(Rdr("chum_score")) = False Then ret.CHUM_SCORE = Rdr("chum_score").ToString()
                If Convert.IsDBNull(Rdr("prefer_lang")) = False Then ret.PREFER_LANG = Rdr("prefer_lang").ToString()
                If Convert.IsDBNull(Rdr("campaign_code")) = False Then ret.CAMPAIGN_CODE = Rdr("campaign_code").ToString()
                If Convert.IsDBNull(Rdr("campaign_name")) = False Then ret.CAMPAIGN_NAME = Rdr("campaign_name").ToString()
                If Convert.IsDBNull(Rdr("network_type")) = False Then ret.NETWORK_TYPE = Rdr("network_type").ToString()
                If Convert.IsDBNull(Rdr("create_date")) = False Then ret.CREATE_DATE = Convert.ToDateTime(Rdr("create_date"))
                If Convert.IsDBNull(Rdr("create_by")) = False Then ret.CREATE_BY = Rdr("create_by").ToString
                If Convert.IsDBNull(Rdr("update_date")) = False Then ret.UPDATE_DATE = Convert.ToDateTime(Rdr("update_date"))
                If Convert.IsDBNull(Rdr("update_by")) = False Then ret.UPDATE_BY = Rdr("update_by").ToString
                If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then ret.CAMPAIGN_NAME_EN = Rdr("campaign_name_en").ToString()
                If Convert.IsDBNull(Rdr("campaign_desc")) = False Then ret.CAMPAIGN_DESC = Rdr("campaign_desc").ToString()
                If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then ret.CAMPAIGN_DESC_TH2 = Rdr("campaign_desc_th2").ToString()
                If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then ret.CAMPAIGN_DESC_EN2 = Rdr("campaign_desc_en2").ToString()
                If Convert.IsDBNull(Rdr("corporate_type")) = False Then ret.CORPORATE_TYPE = Rdr("corporate_type").ToString()
                If Convert.IsDBNull(Rdr("pa_group")) = False Then ret.PA_GROUP = Rdr("pa_group").ToString()
                If Convert.IsDBNull(Rdr("url_capture")) = False Then ret.URL_CAPTURE = Rdr("url_capture").ToString()
                If Convert.IsDBNull(Rdr("url_capture_date")) = False Then ret.URL_CAPTURE_DATE = Rdr("url_capture_date").ToString()
                If Convert.IsDBNull(Rdr("contact_id")) = False Then ret.CONTACT_ID = Rdr("contact_id").ToString()
                If Convert.IsDBNull(Rdr("account_no")) = False Then ret.ACCOUNT_NO = Rdr("account_no").ToString()
                If Convert.IsDBNull(Rdr("billing_system")) = False Then ret.BILLING_SYSTEM = Rdr("billing_system").ToString()
                If Convert.IsDBNull(Rdr("brand_name")) = False Then ret.BRAND_NAME = Rdr("brand_name").ToString()


                FunctionEng.SaveShopTransLog("ShopWebServiceENG.BuldFromDT", "Get Customer Profile MobileNo = " & ret.MOBILE_NO)
            Else
                ret.RESPONSE_MSG = "No Data Found from DC"
            End If

            Return ret
        End Function

        Public Function GetCustomerProfileFormGW(ByVal MobileNo As String) As TbCustomerShParaDB
            Dim ret As New TbCustomerShParaDB
            'http://10.13.174.95/AISQ/interface/getmobileinfo.php?MOBILENO=0821156901&GETNEW=1&REQVAR=TITLE|NAME|CONTACT_EMAIL|LOCAL_LANGUAGE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|SUB_NETWORK_TYPE
            Dim ReqVar As String = "TITLE|NAME|CONTACT_EMAIL|NATIONALITY_CODE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|CHURN|SUB_NETWORK_TYPE|ASSET_ID|BILL_CYCLE|ACCOUNT_NO|GROUP_CODE|CRM_SEGMENT|CORPORATE_TYPE|ACCOUNT_NUM|PROVINCE_CODE|NETWORK_TYPE|ID_CARD_NO|PASSWORD|CA_BLACKLIST|CONTRACT_PHONE|PA_GROUP|PAYMENT_TYPE|ACCOUNT_NUM2|REGION_CODE|LOCAL_LANGUAGE|PP_COS_ID|TPURCode|CA_NAME|CA_ID_CARD_NO|CLV_SEGMENT|REMARK|TUX_CODE|DESCRIPTION|CREATE_DATE|CONTACT_DESC|BILL_DESC|BRAND_NAME"
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&GETNEW=1&REQVAR=" & ReqVar

            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"

            Dim URL1 As New System.Uri(FunctionEng.GetConfig("k_getCustomer_url1") & InputPara)
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webRequest.Timeout = 5000
                webresponse = webRequest.GetResponse()
                ret = GetProfileMsg(webresponse, webRequest, MobileNo, InputPara)
                FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCustomerProfileFromGW", URL1.AbsoluteUri)

            Catch ex As Exception
                Dim URL2 As New System.Uri(FunctionEng.GetConfig("k_getCustomer_url2") & InputPara)
                Try
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCustomerProfileFromGW", URL1.AbsoluteUri & " Exception : " & ex.Message)

                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webRequest.Timeout = 5000
                    webresponse = webRequest.GetResponse()
                    ret = GetProfileMsg(webresponse, webRequest, MobileNo, InputPara)
                    FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCustomerProfileFromGW", URL2.AbsoluteUri)

                Catch ex1 As Exception
                    ret.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCustomerProfileFromGW", URL2.AbsoluteUri & " Exception1 : " & ex1.Message)
                End Try
            End Try
            ini = Nothing

            Return ret
        End Function

        Private Function GetProfileMsg(ByVal webresponse As WebResponse, ByVal webRequest As WebRequest, ByVal MobileNo As String, ByVal InputPara As String) As TbCustomerShParaDB
            Dim ret As New TbCustomerShParaDB
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp() As String = Split(Stream.ReadLine(), "#")

                'TITLE|NAME|CONTACT_EMAIL|NATIONALITY_CODE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|CHURN|SUB_NETWORK_TYPE|ASSET_ID|BILL_CYCLE|ACCOUNT_NO|GROUP_CODE|CRM_SEGMENT|CORPORATE_TYPE|ACCOUNT_NUM|PROVINCE_CODE|NETWORK_TYPE|ID_CARD_NO|PASSWORD|CA_BLACKLIST|CONTRACT_PHONE|PA_GROUP|PAYMENT_TYPE|ACCOUNT_NUM2|REGION_CODE|LOCAL_LANGUAGE|PP_COS_ID|TPURCode|CA_NAME|CA_ID_CARD_NO|CLV_SEGMENT|REMARK|TUX_CODE|DESCRIPTION|CREATE_DATE|CONTACT_DESC|BILL_DESC|BRAND_NAME

                ret.MOBILE_NO = MobileNo
                ret.TITLE_NAME = tmp(0)
                If tmp(1).Trim <> "" Then
                    Dim CustName() As String = tmp(1).Split(" ")
                    ret.F_NAME = CustName(0)
                    If CustName.Length > 1 Then
                        ret.L_NAME = CustName(1)
                    End If
                End If
                ret.EMAIL = tmp(2)
                ret.PREFER_LANG = tmp(3)
                ret.SEGMENT_LEVEL = tmp(4)
                ret.BIRTH_DATE = tmp(5)
                ret.MOBILE_STATUS = tmp(6)
                ret.CHUM_SCORE = tmp(10)
                ret.ASSET_ID = tmp(12)
                ret.SERVICE_YEAR = CalServiceYear(tmp(9))
                ret.PA_GROUP = tmp(25)
                ret.URL_CAPTURE = tmp(37)
                ret.URL_CAPTURE_DATE = tmp(38)
                ret.CONTACT_ID = tmp(39)
                ret.ACCOUNT_NO = tmp(14)
                ret.BILLING_SYSTEM = tmp(40)
                ret.BRAND_NAME = tmp(41)

                Dim cDt As New DataTable
                cDt = MappingCustomerProfile(tmp(7), tmp(8), tmp(20), tmp(11), tmp(17))
                If cDt.Rows.Count > 0 Then
                    If Convert.IsDBNull(cDt.Rows(0)("category")) = False Then ret.CATEGORY = cDt.Rows(0)("category")
                    If Convert.IsDBNull(cDt.Rows(0)("contact_class")) = False Then ret.CONTACT_CLASS = cDt.Rows(0)("contact_class")
                    If Convert.IsDBNull(cDt.Rows(0)("network_type")) = False Then ret.NETWORK_TYPE = cDt.Rows(0)("network_type")
                    If Convert.IsDBNull(cDt.Rows(0)("corporate_type")) = False Then ret.CORPORATE_TYPE = cDt.Rows(0)("corporate_type")
                End If
                cDt.Dispose()

                If ret.NETWORK_TYPE.Trim = "" And ret.SEGMENT_LEVEL.Trim = "" Then
                    ret.RESPONSE_MSG = ""
                    DeleteDCCustomerProfile(ret.MOBILE_NO)
                    DeleteShopCustomerProfile(ret.MOBILE_NO)
                Else
                    UpdateCustomerProfile(ret)
                    For i As Integer = 13 To tmp.Length - 1
                        If ret.OTHER_INFO.Trim = "" Then
                            ret.OTHER_INFO = tmp(i)
                        Else
                            ret.OTHER_INFO += "#" & tmp(i)
                        End If
                    Next
                End If
            Else
                ret.RESPONSE_MSG = "ข้อมูลไม่ถูกต้อง"
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Sub DeleteDCCustomerProfile(ByVal MobileNo As String)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB()
            If trans.Trans IsNot Nothing Then
                Dim sql As String = "delete from tb_customer where mobile_no='" & MobileNo & "'"
                If CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans) > 0 Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If
        End Sub

        Private Sub DeleteShopCustomerProfile(ByVal MobileNo As String)
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans.CreateTransaction()
            If shTrans.Trans IsNot Nothing Then
                'Dim lnq As New ShLinqDB.TABLE.TbCustomerShLinqDB
                Dim sql As String = "delete from tb_customer where mobile_no='" & MobileNo & "'"
                If ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, shTrans.Trans) > 0 Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                End If
            End If
        End Sub

        Private Sub UpdateCustomerProfile(ByVal p As ShParaDb.TABLE.TbCustomerShParaDB)
            'If p.NETWORK_TYPE.Trim <> "" Or p.SEGMENT_LEVEL.Trim <> "" Then
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans.CreateTransaction()
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbCustomerShLinqDB
                lnq.ChkDataByMOBILE_NO(p.MOBILE_NO, shTrans.Trans)
                lnq.TITLE_NAME = p.TITLE_NAME
                lnq.F_NAME = p.F_NAME
                lnq.L_NAME = p.L_NAME
                lnq.MOBILE_STATUS = p.MOBILE_STATUS
                lnq.EMAIL = p.EMAIL
                lnq.SEGMENT_LEVEL = p.SEGMENT_LEVEL
                lnq.BIRTH_DATE = p.BIRTH_DATE
                lnq.CATEGORY = p.CATEGORY
                lnq.ACCOUNT_BALANCE = p.ACCOUNT_BALANCE
                lnq.CONTACT_CLASS = p.CONTACT_CLASS
                lnq.SERVICE_YEAR = p.SERVICE_YEAR
                lnq.CHUM_SCORE = p.CHUM_SCORE
                lnq.PREFER_LANG = p.PREFER_LANG
                lnq.CAMPAIGN_CODE = p.CAMPAIGN_CODE
                lnq.CAMPAIGN_NAME = p.CAMPAIGN_NAME
                lnq.NETWORK_TYPE = p.NETWORK_TYPE
                lnq.CAMPAIGN_NAME_EN = p.CAMPAIGN_NAME_EN
                lnq.CAMPAIGN_DESC = p.CAMPAIGN_DESC
                lnq.CAMPAIGN_DESC_TH2 = p.CAMPAIGN_DESC_TH2
                lnq.CAMPAIGN_DESC_EN2 = p.CAMPAIGN_DESC_EN2
                lnq.CORPORATE_TYPE = p.CORPORATE_TYPE
                lnq.PA_GROUP = p.PA_GROUP
                lnq.URL_CAPTURE = p.URL_CAPTURE
                lnq.BRAND_NAME = p.BRAND_NAME

                Dim strDate As String = ""
                If p.URL_CAPTURE_DATE <> "" Then
                    strDate = p.URL_CAPTURE_DATE.Substring(0, 10)
                    Dim arr As String() = strDate.Split("/")
                    strDate = arr(1) & "/" & arr(0) & "/" & arr(2)
                End If

                lnq.URL_CAPTURE_DATE = strDate
                lnq.CONTACT_ID = p.CONTACT_ID
                lnq.ACCOUNT_NO = p.ACCOUNT_NO
                lnq.BILLING_SYSTEM = p.BILLING_SYSTEM

                Dim ret As Boolean = False
                If lnq.ID <> 0 Then
                    ret = lnq.UpdateByPK("ShopWebServiceENG.UpdateCustomerProfile", shTrans.Trans)
                Else
                    lnq.MOBILE_NO = p.MOBILE_NO
                    ret = lnq.InsertData("ShopWebServiceENG.UpdateCustomerProfile", shTrans.Trans)
                End If
                If ret = True Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                End If
                lnq = Nothing
            End If
            p = Nothing
            'End If
        End Sub

        Private Function MappingCustomerProfile(ByVal vCategory As String, ByVal vContactClass As String, ByVal vNetworkType As String, ByVal vSubNetworkType As String, ByVal vCorporateType As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("category")
            ret.Columns.Add("corporate_type")
            ret.Columns.Add("network_type")
            ret.Columns.Add("contact_class")

            Dim trans As New ShLinqDB.Common.Utilities.TransactionDB(False)
            Dim retTrans As Boolean = False
            retTrans = trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainUsername & ";Password=" & SqlDB.HQMainPassword)
            If retTrans = False Then
                trans.CreateTransaction("Data Source=" & SqlDB.HQDRServe & ";Initial Catalog=" & SqlDB.HQDRDatabase & ";User ID=" & SqlDB.HQDRUsername & ";Password=" & SqlDB.HQDRPassword)
            End If

            If trans.Trans IsNot Nothing Then
                Dim drRet As DataRow = ret.NewRow
                Dim dt As New DataTable
                If vCategory.Trim <> "" Then
                    dt = SqlDB.ExecuteTable("select display_value from TB_CUSTOMER_MAPPING where field_name='GROUP_CODE' and mapping_code='" & vCategory & "'", trans.Trans)
                    If dt.Rows.Count > 0 Then
                        drRet("category") = dt.Rows(0)("display_value")
                    End If
                End If
                If vContactClass.Trim <> "" Then
                    dt = SqlDB.ExecuteTable("select display_value from TB_CUSTOMER_MAPPING where field_name='CUST_CLASS' and mapping_code='" & vContactClass & "'", trans.Trans)
                    If dt.Rows.Count > 0 Then
                        drRet("contact_class") = dt.Rows(0)("display_value")
                    End If
                End If
                If vNetworkType.Trim <> "" And vSubNetworkType.Trim <> "" Then
                    Dim lnq As New CenLinqDB.TABLE.TbCustomerNetworkTypeCenLinqDB
                    lnq.ChkDataByNETWORK_TYPE_SUB_NETWORK_TYPE(vNetworkType, vSubNetworkType, trans.Trans)
                    drRet("network_type") = lnq.DISPLAY_VALUE
                    lnq = Nothing
                End If
                If vCorporateType.Trim <> "" Then
                    Dim lnq As New CenLinqDB.TABLE.TbCustomerCorporateTypeCenLinqDB
                    lnq.ChkDataByCORPORATE_TYPE_CODE(vCorporateType, trans.Trans)
                    drRet("corporate_type") = lnq.CORPORATE_TYPE_NAME
                    lnq = Nothing
                End If
                ret.Rows.Add(drRet)

                trans.CommitTransaction()
            End If

            Return ret
        End Function

        'Private Function MappingCorporateType(ByVal CorporateTypeCode As String) As String
        '    Dim ret As String = ""
        '    If CorporateTypeCode.Trim <> "" Then
        '        Dim trans As New ShLinqDB.Common.Utilities.TransactionDB(False)
        '        Dim retTrans As Boolean = False
        '        retTrans = trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainUsername & ";Password=" & SqlDB.HQMainPassword)
        '        If retTrans = False Then
        '            trans.CreateTransaction("Data Source=" & SqlDB.HQDRServe & ";Initial Catalog=" & SqlDB.HQDRDatabase & ";User ID=" & SqlDB.HQDRUsername & ";Password=" & SqlDB.HQDRPassword)
        '            Dim lnq As New CenLinqDB.TABLE.TbCustomerCorporateTypeCenLinqDB
        '            lnq.ChkDataByCORPORATE_TYPE_CODE(CorporateTypeCode, trans.Trans)
        '            ret = lnq.CORPORATE_TYPE_NAME
        '            lnq = Nothing
        '        Else
        '            Dim lnq As New CenLinqDB.TABLE.TbCustomerCorporateTypeCenLinqDB
        '            lnq.ChkDataByCORPORATE_TYPE_CODE(CorporateTypeCode, trans.Trans)
        '            ret = lnq.CORPORATE_TYPE_NAME
        '            lnq = Nothing
        '        End If
        '        trans.CommitTransaction()
        '    End If
        '    Return ret
        'End Function

        'Private Function MappingNetworkType(ByVal NetworkType As String, ByVal SubNetworkType As String) As String
        '    Dim ret As String = ""
        '    If NetworkType.Trim <> "" And SubNetworkType.Trim <> "" Then
        '        Dim trans As New ShLinqDB.Common.Utilities.TransactionDB(False)
        '        Dim retTrans As Boolean = False
        '        retTrans = trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainUsername & ";Password=" & SqlDB.HQMainPassword)
        '        If retTrans = False Then
        '            trans.CreateTransaction("Data Source=" & SqlDB.HQDRServe & ";Initial Catalog=" & SqlDB.HQDRDatabase & ";User ID=" & SqlDB.HQDRUsername & ";Password=" & SqlDB.HQDRPassword)
        '            Dim lnq As New CenLinqDB.TABLE.TbCustomerNetworkTypeCenLinqDB
        '            lnq.ChkDataByNETWORK_TYPE_SUB_NETWORK_TYPE(NetworkType, SubNetworkType, trans.Trans)
        '            ret = lnq.DISPLAY_VALUE
        '            lnq = Nothing
        '        Else
        '            Dim lnq As New CenLinqDB.TABLE.TbCustomerNetworkTypeCenLinqDB
        '            lnq.ChkDataByNETWORK_TYPE_SUB_NETWORK_TYPE(NetworkType, SubNetworkType, trans.Trans)
        '            ret = lnq.DISPLAY_VALUE
        '            lnq = Nothing
        '        End If
        '        trans.CommitTransaction()
        '    End If
        '    Return ret
        'End Function

        'Private Function MappingCustomer(ByVal FieldName As String, ByVal vGroupCode As String) As String
        '    Dim ret As String = ""
        '    If vGroupCode.Trim <> "" Then
        '        Dim trans As New ShLinqDB.Common.Utilities.TransactionDB(False)
        '        Dim retTrans As Boolean = False
        '        retTrans = trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainUsername & ";Password=" & SqlDB.HQMainPassword)
        '        If retTrans = False Then
        '            trans.CreateTransaction("Data Source=" & SqlDB.HQDRServe & ";Initial Catalog=" & SqlDB.HQDRDatabase & ";User ID=" & SqlDB.HQDRUsername & ";Password=" & SqlDB.HQDRPassword)
        '            Dim dt As New DataTable
        '            dt = SqlDB.ExecuteTable("select display_value from TB_CUSTOMER_MAPPING where field_name='" & FieldName & "' and mapping_code='" & vGroupCode & "'", trans.Trans)
        '            If dt.Rows.Count > 0 Then
        '                ret = dt.Rows(0)("display_value")
        '                dt = Nothing
        '            End If
        '        Else
        '            Dim dt As New DataTable
        '            dt = SqlDB.ExecuteTable("select display_value from TB_CUSTOMER_MAPPING where field_name='" & FieldName & "' and mapping_code='" & vGroupCode & "'", trans.Trans)
        '            If dt.Rows.Count > 0 Then
        '                ret = dt.Rows(0)("display_value")
        '                dt = Nothing
        '            End If
        '        End If
        '        trans.CommitTransaction()

        '    End If
        '    Return ret
        'End Function

        Private Function CalServiceYear(ByVal RegisterDate As String) As String
            'RegisterDate Format = yyyyMMdd
            Dim ret As String = ""
            If RegisterDate.Trim <> "" Then
                Dim dd As String = Right(RegisterDate, 2)
                Dim mm As String = Mid(RegisterDate, 5, 2)
                Dim yy As Integer = Left(RegisterDate, 4)

                Dim RegDate As New Date(yy, mm, dd)
                Dim MonthDiff As Integer = DateDiff(DateInterval.Month, RegDate, Today)
                Dim YearQty As Int16 = MonthDiff \ 12
                Dim MonthQty As Int16 = (MonthDiff Mod 12) + 1
                If MonthQty >= 12 Then
                    MonthQty = 0
                    YearQty += 1
                End If

                ret = YearQty & " Year " & MonthQty & " Month"
            End If
            Return ret
        End Function

        Public Function GetArBalance(ByVal MobileNo As String) As TbCustomerShParaDB
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim para As New TbCustomerShParaDB
            Dim InputPara As String = "MOBILENO=" & MobileNo
            Dim URL1 As String = FunctionEng.GetConfig("getArBalanceURL1") & InputPara

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"
            Try
                'If Common.ShopConnectDBENG.PingServer(ini.ReadString("MainServer")) = True Then
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                para = GetArBalanceMsg(webresponse, para)
                FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetArBalance", URL1)
                'Else
                'If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
                '    para = GetArBalanceFromDisplayServer(MobileNo)
                'End If
                'End If
            Catch ex As Exception
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetArBalance", URL1 & " Exception : " & ex.Message)

                'If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
                para = GetArBalanceFromDisplayServer(MobileNo)
                'End If
            End Try
            ini = Nothing

            Return para
        End Function

        Private Function GetArBalanceFromDisplayServer(ByVal MobileNo As String) As TbCustomerShParaDB
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim para As New TbCustomerShParaDB
            Dim InputPara As String = "MOBILENO=" & MobileNo
            Dim URL2 As String = FunctionEng.GetConfig("getArBalanceURL2") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL2)
                webresponse = webRequest.GetResponse()
                para = GetArBalanceMsg(webresponse, para)
                FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetArBalance", URL2)
            Catch ex1 As Exception
                para.RESPONSE_MSG = ex1.Message
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetArBalance", URL2 & " Exception1 : " & ex1.Message)
            End Try
            Return para
        End Function

        Private Function GetArBalanceMsg(ByVal webresponse As WebResponse, ByVal para As TbCustomerShParaDB) As TbCustomerShParaDB
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    If IsNumeric(tmp) = True Then
                        para.ACCOUNT_BALANCE = Convert.ToDouble(tmp)
                    End If
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return para
        End Function
#End Region

#Region "Get Customer Campaign"
        Private Function CheckSubNetworkType(ByVal DisplayNetworkType) As String
            Dim lnq As New CenLinqDB.TABLE.TbCustomerNetworkTypeCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList("display_value='" & DisplayNetworkType & "'", "", Nothing)
            lnq = Nothing

            Dim ret As String = ""
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0)("sub_network_type")
            End If
            Return ret
        End Function


        Public Function GetCampaignExecbatch(ByVal MobileNo As String, ByVal SessionID As String, ByVal AssetID As String, ByVal NetworkType As String) As TbCustomerShParaDB
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim para As New TbCustomerShParaDB
            para.MOBILE_NO = MobileNo
            para.ASSET_ID = AssetID
            para.NETWORK_TYPE = NetworkType

            Dim SubNetworkType As String = CheckSubNetworkType(NetworkType)
            Dim InputPara As String = "SESSIONID=" & SessionID
            If SubNetworkType.ToUpper.IndexOf("POST") > -1 Then
                InputPara += "&CMDLIST=SUBSCRIPTION_IDENTIFIER|" & AssetID & "|string|SUBSCRIPTION_IDENTIFIER|false||MOBILE_NO|" & MobileNo & "|String||Touchpoint_Interact|startSession|1|false%5eSUBSCRIPTION_IDENTIFIER|" & AssetID & "|string||false|||||Queue||getOffers|3|false%5eSUBSCRIPTION_IDENTIFIER|" & AssetID & "|string|SUBSCRIPTION_IDENTIFIER|false|offerView||||||postEvent|1|false"
            Else
                InputPara += "&CMDLIST=MOBILE_NO|" & MobileNo & "|string|MOBILE_NO|false||MOBILE_NO|" & MobileNo & "|String||Touchpoint_Prepaid|startSession|1|false%5eSUBSCRIPTION_IDENTIFIER|" & MobileNo & "|string||false|||||QueuePP||getOffers|3|false%5eMOBILE_NO|" & MobileNo & "|string|MOBILE_NO|false|offerView||||||postEvent|1|false"
            End If

            Dim ret() As String = {}
            Dim URL1 As String = FunctionEng.GetConfig("InteractExecbatchURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()

                ret = GetCampaignExecbatchResult(webresponse)
            Catch ex As Exception
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignExecbatch", URL1 & " Exception : " & ex.Message)

                Dim URL2 As String = FunctionEng.GetConfig("InteractExecbatchURL2") & InputPara
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL2)
                webresponse = webRequest.GetResponse()

                ret = GetCampaignExecbatchResult(webresponse)
            End Try

            If ret.Length = 9 Then
                'เหมา เหมา 9 บาท/วัน (ใช้ได้15 นาที)| | |000002369|
                '2GLVHR_MaoMao_9B15m_INOUT|97|a88d3.942.22cb2a3.fffffffff04e1223|1-24157539437|1-24157539431#

                If ret(1).Trim <> "" Then
                    para.CAMPAIGN_NAME_EN = "Privilege"

                    Dim Campaign() As String = Split(ret(1), "[LINEBREAK]")
                    para.CAMPAIGN_DESC = Campaign(0)
                    If Campaign.Length > 1 Then
                        para.CAMPAIGN_DESC_EN2 = Campaign(1)
                    End If
                End If

                If ret(2).Trim <> "" Then
                    para.CAMPAIGN_CODE = "สิทธิพิเศษ"

                    Dim Campaign() As String = Split(ret(2), "[LINEBREAK]")
                    para.CAMPAIGN_NAME = Campaign(0)
                    If Campaign.Length > 1 Then
                        para.CAMPAIGN_DESC_TH2 = Campaign(1)
                    End If
                End If
            End If

            Return para
        End Function

        Private Function GetCampaignExecbatchResult(ByVal webresponse As WebResponse) As String()
            Dim ret() As String = {}
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    ret = Split(tmp, "|")
                End If
            Else
                _err = "Cannot Get Campaign interact_execbatch"
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function



        Public Function GetCampaign(ByVal MobileNo As String, ByVal SessionID As String, ByVal AssetID As String, ByVal NetworkType As String) As TbCustomerShParaDB
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim para As New TbCustomerShParaDB
            para.MOBILE_NO = MobileNo
            para.ASSET_ID = AssetID
            para.NETWORK_TYPE = NetworkType
            Dim ShopAbbCode As String = FunctionEng.GetConfig("ShopAbbCode")
            If GetCampaignStartSession(para, SessionID) = True Then
                Dim OfferMsg As String = ""
                Dim OfferResult() As String = GetCampaignOffer(SessionID)
                If OfferResult.Length > 0 Then
                    Dim campPara() As String = Split(OfferResult(0), "|")
                    If campPara.Length > 2 Then
                        Dim CampaignDescTh() As String = Split(campPara(2), "[LINEBREAK]")
                        If CampaignDescTh.Length >= 2 Then
                            para.CAMPAIGN_CODE = "สิทธิพิเศษ"
                            para.CAMPAIGN_NAME = CampaignDescTh(0)
                            para.CAMPAIGN_DESC_TH2 = CampaignDescTh(1)
                        Else
                            para.CAMPAIGN_NAME = CampaignDescTh(0)
                        End If

                        Dim CampaignDescEng() As String = Split(campPara(1), "[LINEBREAK]")
                        If CampaignDescEng.Length >= 2 Then
                            para.CAMPAIGN_NAME_EN = "Privilege"
                            para.CAMPAIGN_DESC = CampaignDescEng(0)
                            para.CAMPAIGN_DESC_EN2 = CampaignDescEng(1)
                        Else
                            para.CAMPAIGN_DESC = CampaignDescEng(0)
                        End If

                        For Each camp As String In OfferResult
                            If camp.Trim <> "" Then
                                Dim tmp() As String = Split(camp, "|")
                                If tmp.Length > 0 Then
                                    GetCampaignPostEvent(SessionID, tmp(6), para.MOBILE_NO, tmp(8), tmp(7), tmp(5), tmp(1), ShopAbbCode)
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            GetCampaignEndSession(SessionID)

            Return para
        End Function

        Private Sub GetCampaignEndSession(ByVal SessionID As String)
            Dim InputPara As String = "SESSIONID=" & SessionID
            Dim URL1 As String = FunctionEng.GetConfig("InteractEndSessionURL1") & InputPara

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"

            'If Common.ShopConnectDBENG.PingServer(ini.ReadString("MainServer")) = True Then
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                If GetCampaignEndSessionResult(webresponse) = True Then
                    FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignEndSession", URL1)
                Else
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignEndSession", URL1 & " Error:" & _err)
                End If
            Catch ex As Exception
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignEndSession", URL1 & " Exception : " & ex.Message)

                Dim URL2 As String = FunctionEng.GetConfig("InteractEndSessionURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    If GetCampaignEndSessionResult(webresponse) = True Then
                        FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignEndSession", URL2)
                    Else
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignEndSession", URL2 & " Error:" & _err)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignEndSession", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            'Else
            'If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
            '    Dim URL2 As String = FunctionEng.GetConfig("InteractEndSessionURL2") & InputPara
            '    Try
            '        Dim webRequest As WebRequest
            '        Dim webresponse As WebResponse
            '        webRequest = webRequest.Create(URL2)
            '        webresponse = webRequest.GetResponse()
            '        If GetCampaignEndSessionResult(webresponse) = True Then
            '            FunctionEng.SaveTransLog("ShopWebServiceENG.GetCampaignEndSession", URL2)
            '        Else
            '            FunctionEng.SaveErrorLog("ShopWebServiceENG.GetCampaignEndSession", URL2 & " Error:" & _err)
            '        End If
            '    Catch ex1 As Exception
            '        FunctionEng.SaveErrorLog("ShopWebServiceENG.GetCampaignEndSession", URL2 & " Exception1 : " & ex1.Message)
            '    End Try
            'End If
            'End If
            ini = Nothing
        End Sub

        Private Function GetCampaignEndSessionResult(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    Dim tmpRes() As String = Split(tmp, "|")
                    If tmpRes(0).Trim = "" Then
                        ret = True
                    Else
                        ret = False
                        _err = tmp
                    End If
                Else
                    _err = tmp
                    ret = False
                End If
            Else
                ret = False
                _err = "Cannon End Session"
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Sub GetCampaignPostEvent(ByVal SessionID As String, ByVal TreamentCode As String, ByVal MobileNo As String, ByVal ParentCode As String, ByVal ChildCode As String, ByVal Score As String, ByVal OfferCode As String, ByVal ShopCode As String)
            Dim InputPara As String = "SESSIONID=" & SessionID
            InputPara += "&EVENTNAME=offerView"
            InputPara += "&TREATMENTCODE=" & TreamentCode
            InputPara += "&MOBILENO=" & MobileNo
            InputPara += "&CHILDCODE=" & ChildCode
            InputPara += "&SCORE=" & Score
            InputPara += "&OFFERCODE=" & OfferCode
            InputPara += "&CHANNELCODE=QueueSystem"
            InputPara += "&USERDEFINEDFIELDS=" & ShopCode

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"
            'If Common.ShopConnectDBENG.PingServer(ini.ReadString("MainServer")) = True Then
            Dim URL1 As String = FunctionEng.GetConfig("InteractPostEventURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                If GetCampaignPostEventResult(webresponse) = True Then
                    FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignPostEvent", URL1)
                Else
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignPostEvent", URL1 & " Error :" & _err)
                End If
            Catch ex As Exception
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignPostEvent", URL1 & " Exception : " & ex.Message)

                Dim URL2 As String = FunctionEng.GetConfig("InteractPostEventURL2") & InputPara
                Try

                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    If GetCampaignPostEventResult(webresponse) = True Then
                        FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignPostEvent", URL2)
                    Else
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignPostEvent", URL2 & " Error :" & _err)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignPostEvent", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            'Else
            'If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
            '    Dim URL2 As String = FunctionEng.GetConfig("InteractPostEventURL2") & InputPara
            '    Try
            '        Dim webRequest As WebRequest
            '        Dim webresponse As WebResponse
            '        webRequest = webRequest.Create(URL2)
            '        webresponse = webRequest.GetResponse()
            '        If GetCampaignPostEventResult(webresponse) = True Then
            '            FunctionEng.SaveTransLog("ShopWebServiceENG.GetCampaignPostEvent", URL2)
            '        Else
            '            FunctionEng.SaveErrorLog("ShopWebServiceENG.GetCampaignPostEvent", URL2 & " Error :" & _err)
            '        End If
            '    Catch ex1 As Exception
            '        FunctionEng.SaveErrorLog("ShopWebServiceENG.GetCampaignPostEvent", URL2 & " Exception1 : " & ex1.Message)
            '    End Try
            'End If
            'End If
            ini = Nothing
        End Sub

        Private Function GetCampaignPostEventResult(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    Dim tmpRes() As String = Split(tmp, "|")
                    If tmpRes(0).Trim = "" Then
                        ret = True
                    Else
                        ret = False
                        _err = tmp
                    End If
                Else
                    ret = False
                    _err = tmp
                End If
            Else
                ret = False
                _err = "Cannot Post Event"
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Function GetCampaignOffer(ByVal SessionID As String) As String()
            Dim ret() As String = {}
            Dim InputPara As String = "SESSIONID=" & SessionID
            InputPara += "&IPOINT=Recommend"
            InputPara += "&NUMBERREQUESTED=3"

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"
            'If Common.ShopConnectDBENG.PingServer(ini.ReadString("MainServer")) = True Then
            Dim URL1 As String = FunctionEng.GetConfig("InteractGetOfferURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetCampaignOfferResult(webresponse)
                FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignOffer", URL1)
            Catch ex As Exception
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignOffer", URL1 & " Exception : " & ex.Message)

                'If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
                Dim URL2 As String = FunctionEng.GetConfig("InteractGetOfferURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetCampaignOfferResult(webresponse)
                    FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignOffer", URL2)
                Catch ex1 As Exception
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignOffer", URL2 & " Exception1 : " & ex1.Message)
                End Try
                'End If
            End Try
            'End If
            ini = Nothing

            Return ret
        End Function

        Private Function GetCampaignOfferResult(ByVal webresponse As WebResponse) As String()
            Dim ret() As String = {}
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    ret = Split(tmp, "#")
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Function GetCampaignStartSession(ByVal para As TbCustomerShParaDB, ByVal SessionID As String) As Boolean
            Dim ret As Boolean = False
            Dim AudienceIdValue As String = ""
            If para.NETWORK_TYPE.ToUpper.IndexOf("POST") > -1 Then
                AudienceIdValue = para.ASSET_ID
            Else
                AudienceIdValue = para.MOBILE_NO
            End If
            Dim InputPara As String = "SESSIONID=" & SessionID & "&RELYONEXISTINGSESSION=false"
            InputPara += "&INTERACTIVECHANNEL=Touchpoint_Interact"
            InputPara += "&AUDIENCEID_NAME=SUBSCRIPTION_IDENTIFIER"
            InputPara += "&AUDIENCEID_VALUE=" & AudienceIdValue
            InputPara += "&AUDIENCEID_DATATYPE=string"
            InputPara += "&AUDIENCELEVEL=SUBSCRIPTION_IDENTIFIER"
            InputPara += "&PARAM1_NAME=MOBILE_NO"
            InputPara += "&PARAM1_VALUE=" & para.MOBILE_NO
            InputPara += "&PARAM1_DATATYPE=string"
            InputPara += "&PARAM2_NAME=MOBILE_STATUS"
            InputPara += "&PARAM2_VALUE=Active"
            InputPara += "&PARAM2_DATATYPE=string"
            InputPara += "&DEBUGFLAG=true"

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"

            'If Common.ShopConnectDBENG.PingServer(ini.ReadString("MainServer")) = True Then
            Dim URL1 As String = FunctionEng.GetConfig("InteractStartSessionURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetCampaignStartSessionResult(webresponse)
                If ret = True Then
                    FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignStartSession", URL1)
                Else
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignStartSession", URL1 & " False :" & _err)
                End If
            Catch ex As Exception
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignStartSession", URL1 & " Exception : " & ex.Message)

                'If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
                Dim URL2 As String = FunctionEng.GetConfig("InteractStartSessionURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetCampaignStartSessionResult(webresponse)
                    If ret = True Then
                        FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetCampaignStartSession", URL2)
                    Else
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignStartSession", URL2 & " False :" & _err)
                    End If
                Catch ex1 As Exception
                    para.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetCampaignStartSession", URL2 & " Exception1 : " & ex1.Message)
                End Try
                'End If
            End Try

            ini = Nothing
            Return ret
        End Function

        Private Function GetCampaignStartSessionResult(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    Dim tmpRes() As String = Split(tmp, "|")
                    If tmpRes(0).Trim = "" Then
                        ret = True
                    Else
                        _err = tmp
                        ret = False
                    End If
                Else
                    _err = tmp
                    ret = False
                End If
            Else
                _err = "Cannot Start Session"
                ret = False
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function
#End Region

#Region "LDAP"
        Public Function LDAPAuth(ByVal UserName As String, ByVal Pwd As String) As LDAPResponsePara
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ret As New LDAPResponsePara
            'TQISAUTHEN    'for test
            Dim InputPara As String = "USERNAME=" & UserName & "&PWD=" & Pwd & "&PROJCODE=" & FunctionEng.GetConfig("q_ldap_proj_code")
            Dim LDAP_URL1 As New System.Uri(FunctionEng.GetConfig("q_ldap_url1") & InputPara)

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"
            ret = GetLDAPMsg(InputPara, ini.ReadString("MainServer"), LDAP_URL1)
            If ret.IS_CALL_GW2 = True Then
                Dim LDAP_URL2 As New System.Uri(FunctionEng.GetConfig("q_ldap_url2") & InputPara)
                ret = GetLDAPMsg(InputPara, ini.ReadString("DisplayServer"), LDAP_URL2)
            End If
            ini = Nothing

            Return ret
        End Function

        Private Function GetLDAPMsg(ByVal InputPara As String, ByVal ShopServer As String, ByVal LDAP_URL As System.Uri) As LDAPResponsePara
            Dim ret As New LDAPResponsePara
            'If Common.ShopConnectDBENG.PingServer(ShopServer) = True Then
            'If Common.ShopConnectDBENG.PingServer(LDAP_URL.Host) = True Then
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(LDAP_URL)
            webRequest.ContentType = "application/x-www-form-urlencoded"
            webRequest.Method = "POST"
            Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webRequest.ContentLength = bytes.Length
                os = webRequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webRequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp.IndexOf("AUTHENSUCCESS") > -1 Then
                        ret.RESULT = True
                    Else
                        ret.RESULT = False
                        ret.RESPONSE_MSG = tmp
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetLDAPMsg", tmp & " Data : " & InputPara)
                    End If
                    ret.IS_CALL_GW2 = False
                End If
            Catch ex As WebException
                ret.RESULT = False
                ret.RESPONSE_MSG = ex.Message
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetLDAPMsg", ex.Message)
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            'Else
            'ret.RESULT = False
            'ret.RESPONSE_MSG = "Connon ping to server " & LDAP_URL.AbsoluteUri
            'End If
            'Else
            'ret.RESULT = False
            'ret.RESPONSE_MSG = "Connon ping to server " & ShopServer
            'End If

            Return ret
        End Function



        'Public Function LDAPAuth(ByVal UserName As String, ByVal Pwd As String) As LDAPResponsePara
        '    'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        '    System.Net.ServicePointManager.ServerCertificateValidationCallback = _
        '      Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
        '      chain As System.Security.Cryptography.X509Certificates.X509Chain, _
        '      sslerror As System.Net.Security.SslPolicyErrors) True

        '    Dim ret As New LDAPResponsePara
        '    'TQISAUTHEN    'for test
        '    Dim InputPara As String = "USERNAME=" & UserName & "&PWD=" & Pwd & "&PROJCODE=" & FunctionEng.GetConfig("q_ldap_proj_code")
        '    Dim LDAP_URL1 As New System.Uri(FunctionEng.GetConfig("q_ldap_url1") & InputPara)

        '    Dim ini As New Common.IniReader(INIFileName)
        '    ini.Section = "Setting"
        '    If Common.ShopConnectDBENG.PingServer(ini.ReadString("MainServer")) = True Then
        '        Try
        '            Dim webRequest As WebRequest
        '            Dim webresponse As WebResponse
        '            If Common.ShopConnectDBENG.PingServer(LDAP_URL1.Host) = True Then
        '                webRequest = webRequest.Create(LDAP_URL1)
        '                ret = GetLDAPMsg(InputPara, webresponse, webRequest)
        '            Else
        '                Dim LDAP_URL2 As New System.Uri(FunctionEng.GetConfig("q_ldap_url2") & InputPara)
        '                If Common.ShopConnectDBENG.PingServer(LDAP_URL2.Host) = True Then
        '                    webRequest = webRequest.Create(LDAP_URL2)
        '                    ret = GetLDAPMsg(InputPara, webresponse, webRequest)
        '                Else
        '                    ret.RESULT = False
        '                    ret.RESPONSE_MSG = "Unable to connect server : " & LDAP_URL2.AbsoluteUri
        '                End If
        '            End If
        '        Catch ex As Exception
        '            If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
        '                Dim LDAP_URL2 As New System.Uri(FunctionEng.GetConfig("q_ldap_url2") & InputPara)
        '                Try
        '                    Dim webRequest As WebRequest
        '                    Dim webresponse As WebResponse
        '                    If Common.ShopConnectDBENG.PingServer(LDAP_URL2.Host) = True Then
        '                        webRequest = webRequest.Create(LDAP_URL2)
        '                        ret = GetLDAPMsg(InputPara, webresponse, webRequest)
        '                    Else
        '                        ret.RESULT = False
        '                        ret.RESPONSE_MSG = "Unable to connect server : " & LDAP_URL2.AbsoluteUri
        '                    End If
        '                Catch ex1 As Exception
        '                    ret.RESULT = False
        '                    ret.RESPONSE_MSG = ex1.Message
        '                End Try
        '            End If
        '        End Try
        '    Else
        '        If Common.ShopConnectDBENG.PingServer(ini.ReadString("DisplayServer")) = True Then
        '            Dim LDAP_URL2 As New System.Uri(FunctionEng.GetConfig("q_ldap_url2") & InputPara)
        '            Try
        '                Dim webRequest As WebRequest
        '                Dim webresponse As WebResponse
        '                If Common.ShopConnectDBENG.PingServer(LDAP_URL2.Host) = True Then
        '                    webRequest = webRequest.Create(LDAP_URL2)
        '                    ret = GetLDAPMsg(InputPara, webresponse, webRequest)
        '                Else
        '                    ret.RESULT = False
        '                    ret.RESPONSE_MSG = "Unable to connect server : " & LDAP_URL2.AbsoluteUri
        '                End If
        '            Catch ex1 As Exception
        '                ret.RESULT = False
        '                ret.RESPONSE_MSG = ex1.Message
        '            End Try
        '        End If
        '    End If
        '    ini = Nothing

        '    Return ret
        'End Function

        'Private Function GetLDAPMsg(ByVal InputPara As String, ByVal ShopServer As String, ByVal LDAP_URL As System.Uri) As LDAPResponsePara
        '    Dim ret As New LDAPResponsePara

        '    Dim webRequest As WebRequest
        '    Dim webresponse As WebResponse
        '    webRequest = webRequest.Create(LDAP_URL)
        '    webRequest.ContentType = "application/x-www-form-urlencoded"
        '    webRequest.Method = "POST"
        '    Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
        '    Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
        '    Dim os As Stream = Nothing

        '    Try
        '        webRequest.ContentLength = bytes.Length
        '        os = webRequest.GetRequestStream()
        '        os.Write(bytes, 0, bytes.Length)
        '        webresponse = webRequest.GetResponse()
        '        Dim Stream As New StreamReader(webresponse.GetResponseStream())
        '        If Stream.Peek <> -1 Then
        '            Dim tmp As String = Stream.ReadLine()
        '            If tmp.IndexOf("AUTHENSUCCESS") > -1 Then
        '                ret.RESULT = True
        '                'FunctionEng.SaveTransLog("ShopWebServiceENG.GetLDAPMsg", InputPara)
        '            Else
        '                ret.RESULT = False
        '                ret.RESPONSE_MSG = tmp
        '                FunctionEng.SaveErrorLog("ShopWebServiceENG.GetLDAPMsg", tmp & " Data : " & InputPara)
        '            End If
        '        End If
        '    Catch ex As WebException
        '        ret.RESULT = False
        '        ret.RESPONSE_MSG = ex.Message
        '        FunctionEng.SaveErrorLog("ShopWebServiceENG.GetLDAPMsg", ex.Message)
        '    Finally
        '        If os IsNot Nothing Then
        '            os.Close()
        '        End If
        '    End Try

        '    Return ret
        'End Function
#End Region

#Region "Shop Send SMS"
        Public Function ShopSendSMS(ByVal MobileNo As String, ByVal ServiceID As String, ByVal AppointmentTime As DateTime) As SMSResponsePara
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ret As New SMSResponsePara
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&SMSTXT=" & CreateSMSMsg(ServiceID, MobileNo, AppointmentTime)

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"
            ret = GetSMSMsg(InputPara, New System.Uri(FunctionEng.GetConfig("k_send_sms_url1") & InputPara), ini.ReadString("MainServer"))
            If ret.CALL_GW2 = True Then
                ret = GetSMSMsg(InputPara, New System.Uri(FunctionEng.GetConfig("k_send_sms_url2") & InputPara), ini.ReadString("DisplayServer"))
            End If
            ini = Nothing
            Return ret
        End Function

        Private Function CreateSMSMsg(ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime) As String
            Dim trans As New TransactionDB
            Dim lnq As New TbCustomerShLinqDB
            Dim Cpara As TbCustomerShParaDB = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)

            Dim ServiceName As String = ""
            Dim sLnq As New TbItemShLinqDB
            Dim sDt As New DataTable
            sDt = sLnq.GetDataList("id in (" & ServiceID & ")", "item_wait,item_time,item_order ", trans.Trans)
            If sDt.Rows.Count > 0 Then
                For Each sDr As DataRow In sDt.Rows
                    If InStr(Cpara.PREFER_LANG.ToUpper, "THAI") > 0 Then
                        If ServiceName = "" Then
                            ServiceName += sDr("item_name_th")
                        Else
                            ServiceName += "," & sDr("item_name_th")
                        End If
                    Else
                        If ServiceName = "" Then
                            ServiceName += sDr("item_name")
                        Else
                            ServiceName += "," & sDr("item_name")
                        End If
                    End If
                Next
            End If
            trans.CommitTransaction()

            'Dim SerIDList() As String = Split(ServiceID, ",")
            'For Each sID As Long In SerIDList
            '    Dim sLnq As New TbItemShLinqDB
            '    sLnq.GetDataByPK(Convert.ToInt64(sID), trans.Trans)
            '    If sLnq.ID <> 0 Then
            '        If InStr(Cpara.PREFER_LANG.ToUpper, "THAI") > 0 Then
            '            If ServiceName = "" Then
            '                ServiceName += sLnq.ITEM_NAME_TH
            '            Else
            '                ServiceName += "," & sLnq.ITEM_NAME_TH
            '            End If
            '        Else
            '            If ServiceName = "" Then
            '                ServiceName += sLnq.ITEM_NAME
            '            Else
            '                ServiceName += "," & sLnq.ITEM_NAME
            '            End If
            '        End If
            '    End If
            'Next
            'trans.CommitTransaction()

            Dim ret As String = ""
            Dim ShopName As String = ""
            'Dim TimeBefore As String = Convert.ToDouble(FunctionEng.GetConfig("k_cancel")) / 60
            Dim TimeBefore As String = DateAdd(DateInterval.Minute, 0 - Convert.ToInt64(FunctionEng.GetConfig("k_cancel")), AppointmentTime).ToString("HH:mm")
            If InStr(Cpara.PREFER_LANG.ToUpper, "THAI") > 0 Then

                ShopName = FunctionEng.GetConfig("s_name_th")
                ret += "คุณได้จองคิว " & ServiceName & " ที่ " & ShopName & " วันนี้"
                ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น. กรณีเปลี่ยน/ยกเลิก กรุณาแจ้งAIS"
                'ret += " ก่อนถึงเวลานัด" & TimeBefore & "ชม."
                ret += " ภายในเวลา " & TimeBefore & "น."
            Else
                ShopName = FunctionEng.GetConfig("s_name_en")
                ret += "You have an appointment for " & ServiceName
                ret += " at " & ShopName & " on " & AppointmentTime.ToString("ddd dMMMyy", New CultureInfo("en-US")) & ", " & AppointmentTime.ToString("HH:mm") & "."
                'ret += " To change/cancel,please make changes " & TimeBefore & "hr in advance."
                ret += " To change/cancel,please make changes " & TimeBefore & " in advance."
            End If

            Return ret
        End Function

        Public Function SendSMS(ByVal MobileNo As String, ByVal Msg As String) As SMSResponsePara
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ret As New SMSResponsePara
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&SMSTXT=" & Msg
            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"
            ret = GetSMSMsg(InputPara, New System.Uri(FunctionEng.GetConfig("k_send_sms_url1") & InputPara), ini.ReadString("MainServer"))
            If ret.CALL_GW2 = True Then
                ret = GetSMSMsg(InputPara, New System.Uri(FunctionEng.GetConfig("k_send_sms_url2") & InputPara), ini.ReadString("DisplayServer"))
            End If
            ini = Nothing

            Return ret
        End Function

        Private Function GetSMSMsg(ByVal InputPara As String, ByVal SMS_URL As System.Uri, ByVal ShopServer As String) As SMSResponsePara
            Dim ret As New SMSResponsePara

            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SMS_URL)
            webRequest.ContentType = "application/x-www-form-urlencoded"
            webRequest.Method = "POST"
            Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webRequest.ContentLength = bytes.Length
                os = webRequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webRequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp = "DONE" Then
                        ret.RESULT = True
                        FunctionEng.SaveShopTransLog("ShopWebServiceENG.GetSMSMsg", SMS_URL.OriginalString)
                    Else
                        ret.RESULT = False
                        ret.ERROR_RESPONSE = tmp
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.GetSMSMsg", tmp + " Data : " & SMS_URL.OriginalString)
                    End If
                    ret.CALL_GW2 = False
                End If
                Stream.Close()
                Stream = Nothing
            Catch ex As WebException
                ret.RESULT = False
                ret.ERROR_RESPONSE = ex.Message & " URL :" & SMS_URL.OriginalString
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try

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
                re = lnq.UpdateByPK("ShopWebServiceENG.SaveAppointmentJob", trans.Trans)
            Else
                re = lnq.InsertData("ShopWebServiceENG.SaveAppointmentJob", trans.Trans)
                If re = True Then
                    p.ID = lnq.ID
                End If
            End If
            ret.SaveResult = re
            ret.AppointmentJobPara = p

            If re = False Then
                trans.RollbackTransaction()
                ret.ErrorMessage = lnq.ErrorMessage
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SaveAppointmentJob", ret.ErrorMessage)
            Else
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function DeleteSMSCancelAppointment(ByVal MobileNo As String, ByVal StartSlot As String) As Boolean
            Dim ret As Boolean = False
            Dim ShopAbbCode As String = Engine.Common.FunctionEng.GetConfig("ShopAbbCode")
            If ShopAbbCode.Trim <> "" Then
                Try
                    'Delete Notify ก่อน
                    Dim DelWh As String = ""
                    DelWh += " delete from tb_notify_joblist "
                    DelWh += " where shop_id=(select top 1 id from tb_shop where shop_abb='" & ShopAbbCode & "') "
                    DelWh += " and mobile_no = '" & MobileNo & "'"
                    DelWh += " and convert(varchar(16),appointment_time, 120) = '" & StartSlot & "'"
                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    ret = (SqlDB.ExecuteNonQuery(DelWh, trans.Trans) > 0)
                    trans.CommitTransaction()
                Catch ex As Exception
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.DeleteSMSCancelAppointment", "Exception : " & ex.Message)
                End Try
            Else
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.DeleteSMSCancelAppointment", "Cannot get TB_SETTING ShopAbbCode")
            End If

            Return ret
        End Function
#End Region

#Region "Check Black List"
        Public Function CheckBlackList(ByVal MobileNo As String) As CenParaDB.Appointment.AppointmentCheckBacklistResultPara
            Dim ret As New CenParaDB.Appointment.AppointmentCheckBacklistResultPara
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim sql As String = "select top 1 id, end_date "
                sql += " from tb_appointment_blacklist "
                sql += " where customer_id = '" & MobileNo & "' "
                sql += " and '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "' between convert(varchar(10),start_date,120) and convert(varchar(10), end_date,120)"
                Dim dt As DataTable = SqlDB.ExecuteTable(sql, trans.Trans)
                If dt.Rows.Count > 0 Then
                    ret.IsBackList = True
                    ret.NewAppointmentDate = DateAdd(DateInterval.Day, 1, Convert.ToDateTime(dt.Rows(0)("end_date")))
                    ret.MSG = "Backlist"
                End If
                dt.Dispose()
            End If

            Return ret
        End Function
#End Region

#Region "Siebel Activity"
        Public Function SiebelCreateActivityConfirm(ByVal MobileNo As String) As ShParaDb.ShopWebService.SiebelResponsePara
            Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara

            Dim InputPara As String = ""
            InputPara += "SIEBEL_TYPE=Walk In"
            InputPara += "&SIEBEL_ACTIVITYCATEGORY=N/A"
            InputPara += "&SIEBEL_ACTIVITYSUBCATEGORY=N/A"
            InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN
            InputPara += "&SIEBEL_MOBILENO=" & MobileNo

            ret = CallWebService(InputPara, ret)
            If ret.RESULT = False Then
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelCreateActivityConfirm", ret.ErrorMessage & " MobileNo:" & MobileNo)
            End If

            Return ret
        End Function

        Private Sub CreateAppointmentNotifyJoblist(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ShopAbbCode As String, ByVal AppointmentChannel As String)
            Dim shDt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                shDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable("select top 1 id from tb_shop where shop_abb='" & ShopAbbCode & "'", trans.Trans)
                If shDt.Rows.Count > 0 Then
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
                    jLnq.SHOP_ID = Convert.ToInt16(shDt.Rows(0)("id"))
                    jLnq.MOBILE_NO = MobileNo
                    jLnq.APPOINTMENT_TIME = StartSlot
                    jLnq.APPOINTMENT_CHANNEL = AppointmentChannel
                    jLnq.SMS_TIME1 = Time1
                    If StartSlot.Date = Today.Date Then
                        jLnq.SMS_ALERT1 = "Y"   'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง SMS ล่วงหน้า 1 วัน
                    Else
                        jLnq.SMS_ALERT1 = Alert1
                    End If

                    Dim CustData As New ShParaDb.TABLE.TbCustomerShParaDB
                    CustData = GetCustomerProfileFormGW(MobileNo)

                    jLnq.SMS_MSG1 = CreateNotifyMsg1Day(StartSlot, CustData.PREFER_LANG.Trim)
                    jLnq.SMS_TIME2 = Time2
                    jLnq.SMS_ALERT2 = Alert2
                    jLnq.SMS_MSG2 = CreateNotifyMsg30Min(StartSlot, CustData.PREFER_LANG.Trim)

                    '####### จองที่ Kiosk ไม่มีการระบุ Email อยู่แล้ว ###########
                    '#################################################

                    'jLnq.CUSTOMER_EMAIL = drTmp("customer_email").ToString
                    'jLnq.EMAIL_TIME1 = Time1
                    'If Convert.ToDateTime(drTmp("start_slot")).Date = Today.Date Then
                    '    jLnq.EMAIL_ALERT1 = "Y"     'ถ้าเป็นการจองในวันเดียวกันก็ไม่ต้องส่ง Mail ล่วงหน้า 1 วัน
                    'Else
                    '    jLnq.EMAIL_ALERT1 = Alert1
                    'End If
                    'jLnq.EMAIL_MSG1 = CreateNotifyEMAIL1Day(drTmp("start_slot"), cPara.PREFER_LANG.Trim, shLnq)
                    'jLnq.EMAIL_TIME2 = Time2
                    'jLnq.EMAIL_ALERT2 = Alert2
                    'jLnq.EMAIL_MSG2 = CreateNotifyEMAIL30Min(drTmp("start_slot"), cPara.PREFER_LANG.Trim, shLnq)

                    If jLnq.InsertData("SendNotifyENG.CreateNotifyJob", trans.Trans) = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CreateAppointmentNotifyJoblist", jLnq.ErrorMessage)
                    End If
                    CustData = Nothing
                Else
                    trans.CommitTransaction()
                End If
                shDt.Dispose()
            End If
        End Sub

        Private Function CreateNotifyMsg30Min(ByVal AppointmentTime As DateTime, ByVal PreferLang As String) As String
            Dim ret As String = ""
            Dim ShopName As String = ""
            If InStr(PreferLang.ToUpper, "THAI") > 0 Then
                ShopName = FunctionEng.GetConfig("s_name_th")
                ret += "คุณมีนัดรับบริการที่ " & ShopName & " ในอีก 30 นาที"
                ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetConfig("k_before")) & " นาที ขอบคุณค่ะ"
            Else
                ShopName = FunctionEng.GetConfig("s_name_en")
                ret += "You have an appointment at " & ShopName
                ret += " in 30-minute time."
                ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetConfig("k_before")) & " mins before the appointment. Thank you."
            End If

            Return ret
        End Function

        Private Function CreateNotifyMsg1Day(ByVal AppointmentTime As DateTime, ByVal PreferLang As String) As String
            Dim ret As String = ""
            Dim ShopName As String = ""
            If InStr(PreferLang.ToUpper, "THAI") > 0 Then
                ShopName = FunctionEng.GetConfig("s_name_th")
                ret += "พรุ่งนี้คุณมีนัดที่ " & ShopName
                ret += " เวลา" & AppointmentTime.ToString("HH:mm") & "น."
                ret += " กรุณามาถึงก่อนเวลานัด " & Convert.ToInt64(FunctionEng.GetConfig("k_before")) & " นาที ขอบคุณค่ะ"
            Else
                ShopName = FunctionEng.GetConfig("s_name_en")
                ret += "You have appointment at " & ShopName
                ret += " tomorrow at " & AppointmentTime.ToString("HH:mm")
                ret += " Please arrive " & Convert.ToInt64(FunctionEng.GetConfig("k_before")) & " mins before the appointment time. Thank you."
            End If

            Return ret
        End Function

        Public Function SiebelCreateActivity(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ItemName As String, ByVal AppointmentChannel As String) As ShParaDb.ShopWebService.SiebelResponsePara
            Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara
            Dim ShopAbbCode As String = FunctionEng.GetConfig("ShopAbbCode")
            'Save Appointment Job
            Dim pJob As New CenParaDB.Appointment.SaveAppointmentJobPara
            Dim pPara As New CenParaDB.TABLE.TbAppointmentJobCenParaDB
            pPara.SHOP_ABB = ShopAbbCode
            pPara.MOBILE_NO = MobileNo
            pPara.APP_DATE = DateTime.Now
            pPara.START_SLOT = StartSlot
            pPara.APPOINTMENT_CHANNEL = AppointmentChannel
            pPara.ACTIVE_STATUS = Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment

            Dim jLnq As New CenLinqDB.TABLE.TbAppointmentJobCenLinqDB
            If jLnq.ChkDataByACTIVE_STATUS_MOBILE_NO_START_SLOT(Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment, pPara.MOBILE_NO, pPara.START_SLOT, Nothing) = False Then
                pJob = SaveAppointmentJob(pPara)
            Else
                pJob.AppointmentJobPara = jLnq.GetParameter(jLnq.ID, Nothing)
                pJob.SaveResult = True
            End If
            jLnq = Nothing
            pPara = Nothing

            CreateAppointmentNotifyJoblist(MobileNo, StartSlot, ShopAbbCode, AppointmentChannel)


            If pJob.SaveResult = True Then
                ret.APPOINTMENT_JOB_ID = pJob.AppointmentJobPara.ID

                Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
                Dim shTrans As New TransactionDB
                Dim re As Boolean = False
                'Update JobID to TB_APPOINTMENT_CUSTOMER
                Dim sqlU As String = "update tb_appointment_customer "
                sqlU += " set appointment_job_id='" & pJob.AppointmentJobPara.ID & "'"
                sqlU += " where CONVERT(varchar(16),start_slot,120) = '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' "
                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"
                re = lnq.UpdateBySql(sqlU, shTrans.Trans)

                If re = True Then
                    shTrans.CommitTransaction()

                    Dim SubCat As String = ""
                    If AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatID Then
                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatName
                    ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatID Then
                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatName
                    ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallCenter.CatID Then
                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallCenter.CatName
                    ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatID Then
                        SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatName
                    End If

                    Dim tmpStr As String
                    tmpStr = vbTab & Chr(45) & " " & Replace(ItemName, ",", vbNewLine & vbTab & Chr(45) & " ")

                    Dim SiebelDesc As New StringBuilder
                    SiebelDesc.Append("จองคิว รับบริการ")
                    SiebelDesc.Append(tmpStr)
                    SiebelDesc.Append(vbNewLine & vbNewLine & " วันที่ " & StartSlot.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น. ")
                    SiebelDesc.Append(" สาขา" & FunctionEng.GetConfig("s_name_th"))

                    Dim InputPara As String = ""
                    InputPara += "SIEBEL_TYPE=Appointment"
                    InputPara += "&SIEBEL_ACTIVITYCATEGORY=Appointment"
                    InputPara += "&SIEBEL_ACTIVITYSUBCATEGORY=" & SubCat
                    InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc.ToString
                    InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN
                    InputPara += "&SIEBEL_MOBILENO=" & MobileNo

                    ret = CallWebService(InputPara, ret)
                    If ret.RESULT = True Then
                        'Update To shop.TB_APPOINTMENT_CUSTOMER
                        sqlU = "update tb_appointment_customer "
                        sqlU += " set siebel_activity_id='" & ret.ACTIVITY_ID & "', "
                        sqlU += " siebel_status = '" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN & "', "
                        sqlU += " siebel_desc = '" & SiebelDesc.ToString & "'"
                        sqlU += " where CONVERT(varchar(16),start_slot,120) = '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' "
                        sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"
                        shTrans = New TransactionDB
                        re = lnq.UpdateBySql(sqlU, shTrans.Trans)
                        If re = False Then
                            ret.RESULT = False
                            ret.ACTIVITY_ID = ""
                            ret.ErrorMessage = "Cannot Update tb_appointment_customer ### " & sqlU & " ### " & lnq.ErrorMessage

                            shTrans.RollbackTransaction()
                            FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & MobileNo)
                        Else
                            shTrans.CommitTransaction()
                        End If
                    Else
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & MobileNo)

                        Dim sqlE As String = "update tb_appointment_customer "
                        sqlE += " set siebel_desc='GenSiebelENG.CreateSiebelActivity : " & ret.ErrorMessage & " MobileNo:" & MobileNo & "' "
                        sqlE += " where customer_id = '" & MobileNo & "' and CONVERT(varchar(16),start_slot,120)= '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "'"
                        re = lnq.UpdateBySql(sqlE, shTrans.Trans)
                        If re = True Then
                            shTrans.CommitTransaction()
                        Else
                            shTrans.RollbackTransaction()
                        End If
                    End If
                Else
                    shTrans.RollbackTransaction()

                    Dim ErrMsg As String = "Cannon Update appointment_job_id :" & sqlU & "  ### " & lnq.ErrorMessage
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelCreateActivity", ErrMsg)
                    ret.RESULT = False
                    ret.ACTIVITY_ID = ""
                    ret.ErrorMessage = ErrMsg
                End If
            Else
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelCreateActivity", pJob.ErrorMessage)
                ret.RESULT = False
                ret.ACTIVITY_ID = ""
                ret.ErrorMessage = pJob.ErrorMessage
            End If

            Return ret
        End Function

        Private Function CallWebService(ByVal InputPara As String, ByVal ret As SiebelResponsePara) As SiebelResponsePara
            'Dim ret As SiebelResponsePara

            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"

            ret = GetSiebelMsg(InputPara, New System.Uri(FunctionEng.GetConfig("seibel_url1") & InputPara), ini.ReadString("MainServer"), "CREATE", ret)
            If ret.CALL_GW2 = True Then
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CallWebService", ret.ErrorMessage & " ### " & InputPara)

                ret = GetSiebelMsg(InputPara, New System.Uri(FunctionEng.GetConfig("seibel_url2") & InputPara), ini.ReadString("DisplayServer"), "CREATE", ret)
                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CallWebService", "GenSiebelENG.CallWebService : " & ret.ErrorMessage)
                End If
            End If
            ini = Nothing
            Return ret
        End Function

        Private Function GetSiebelMsg(ByVal InputPara As String, ByVal SIEBEL_URL As System.Uri, ByVal ShopServer As String, ByVal GenType As String, ByVal ret As SiebelResponsePara) As SiebelResponsePara
            'Dim ret As SiebelResponsePara

            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            'If Common.ShopConnectDBENG.PingServer(ShopServer) = True Then
            'If Common.ShopConnectDBENG.PingServer(SIEBEL_URL.Host) = True Then
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SIEBEL_URL)
            webRequest.ContentType = "application/x-www-form-urlencoded"
            webRequest.Method = "POST"
            Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webRequest.ContentLength = bytes.Length
                os = webRequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webRequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If GenType = "UPDATE" Then
                        If tmp.IndexOf("SUCCESS") > -1 Then
                            ret.RESULT = True
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp & " URL:" & SIEBEL_URL.OriginalString
                        End If
                    Else
                        If tmp.IndexOf("SUCCESS-") > -1 Then
                            ret.RESULT = True
                            ret.ACTIVITY_ID = tmp.Replace("SUCCESS-", "")
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp & " URL:" & SIEBEL_URL.OriginalString
                        End If
                    End If
                    ret.CALL_GW2 = False
                Else
                    ret.RESULT = False
                    ret.ErrorMessage = "Error Call Webservice URL :" & SIEBEL_URL.OriginalString
                End If
                Stream.Close()
                Stream = Nothing
            Catch ex As WebException
                ret.RESULT = False
                ret.ErrorMessage = ex.Message & " URL:" & SIEBEL_URL.OriginalString
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            'Else
            'ret.RESULT = False
            'ret.ErrorMessage = "Connon ping to server " & SIEBEL_URL.OriginalString
            'End If
            'Else
            'ret.RESULT = False
            'ret.ErrorMessage = "Connon ping to server " & ShopServer
            'End If

            Return ret
        End Function

        Public Function SiebelUpdateToRegister(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ServiceDate As DateTime, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As SiebelResponsePara
            Dim shTrans As New TransactionDB
            Dim ret As New SiebelResponsePara
            SiebelDesc += ServiceDate.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
            SiebelDesc += vbNewLine & vbNewLine & "ลูกค้ามาลงทะเบียนรับบัตรคิว"
            ret = UpdateSiebelDesc(SiebelDesc, Constant.TbAppointmentCustomer.Siebel.Status.REGISTERED, SiebelActivityID, MobileNo, shTrans)
            If ret.RESULT = False Then
                shTrans.RollbackTransaction()
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelUpdateToRegister", "ShopWebServiceENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
            Else
                ret.ACTIVITY_ID = SiebelActivityID
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function SiebelUpdateToComplete(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal EndDate As DateTime, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As SiebelResponsePara
            Dim shTrans As New TransactionDB
            Dim ret As New SiebelResponsePara
            SiebelDesc += EndDate.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
            SiebelDesc += vbNewLine & vbNewLine & "ลูกค้าได้รับบริการเรียบร้อย"
            ret = UpdateSiebelDesc(SiebelDesc, Constant.TbAppointmentCustomer.Siebel.Status.COMPLETED, SiebelActivityID, MobileNo, shTrans)
            If ret.RESULT = False Then
                shTrans.RollbackTransaction()
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelUpdateToRegister", "ShopWebServiceENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
            Else
                ret.ACTIVITY_ID = SiebelActivityID
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function SiebelUpdateToMissed(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal MissedDate As DateTime, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As SiebelResponsePara
            Dim shTrans As New TransactionDB
            Dim ret As New SiebelResponsePara
            SiebelDesc += MissedDate.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
            SiebelDesc += "ลูกค้าไม่รอรับบริการ"
            ret = UpdateSiebelDesc(SiebelDesc, Constant.TbAppointmentCustomer.Siebel.Status.MISSED, SiebelActivityID, MobileNo, shTrans)
            If ret.RESULT = False Then
                shTrans.RollbackTransaction()
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelUpdateToRegister", "ShopWebServiceENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
            Else
                ret.ACTIVITY_ID = SiebelActivityID
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function SiebelUpdateToCancel(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal CancelDate As DateTime, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As SiebelResponsePara
            Dim shTrans As New TransactionDB
            Dim ret As New SiebelResponsePara
            SiebelDesc += CancelDate.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
            SiebelDesc += vbNewLine & vbNewLine & "ลูกค้ายกเลิกการจอง"
            ret = UpdateSiebelDesc(SiebelDesc, Constant.TbAppointmentCustomer.Siebel.Status.CANCELLED, SiebelActivityID, MobileNo, shTrans)
            If ret.RESULT = False Then
                shTrans.RollbackTransaction()
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SiebelUpdateToCancel", "ShopWebServiceENG.SiebelUpdateToCancel : " & ret.ErrorMessage)
            Else
                ret.ACTIVITY_ID = SiebelActivityID
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Private Function UpdateSiebelDesc(ByVal SiebelDesc As String, ByVal SiebelStatus As String, ByVal SiebelActivityID As String, ByVal MobileNo As String, ByVal shTrans As TransactionDB) As SiebelResponsePara
            Dim re As Boolean = False
            Dim ret As New SiebelResponsePara
            Dim InputPara As String = ""
            InputPara += "SIEBEL_ACTID=" & SiebelActivityID
            InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
            InputPara += "&SIEBEL_STATUS=" & SiebelStatus

            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            ret = CallWebServiceUpdate(InputPara)
            If ret.RESULT = True Then
                'Update To shop.TB_APPOINTMENT_CUSTOMER
                Dim sqlU As String = "update tb_appointment_customer "
                sqlU += " set siebel_status = '" & SiebelStatus & "', "
                sqlU += " siebel_desc = '" & SiebelDesc & "' "
                sqlU += " where siebel_activity_id = '" & SiebelActivityID & "' and customer_id = '" & MobileNo & "' "

                re = lnq.UpdateBySql(sqlU, shTrans.Trans)
                If re = False Then
                    ret.ErrorMessage = _err
                    ret.RESULT = False
                Else
                    ret.RESULT = True
                End If
            End If
            lnq = Nothing

            Return ret
        End Function

        Private Function CallWebServiceUpdate(ByVal InputPara As String) As SiebelResponsePara
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ret As New SiebelResponsePara
            Dim ini As New Common.IniReader(INIFileName)
            ini.Section = "Setting"

            ret = GetSiebelMsg(InputPara, New System.Uri(FunctionEng.GetConfig("seibel_update_url1") & InputPara), ini.ReadString("MainServer"), "UPDATE", ret)
            If ret.CALL_GW2 = True Then
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CallWebServiceUpdate", "ShopWebServiceENG.CallWebServiceUpdate : " & ret.ErrorMessage)

                ret = GetSiebelMsg(InputPara, New System.Uri(FunctionEng.GetConfig("seibel_update_url2") & InputPara), ini.ReadString("DisplayServer"), "UPDATE", ret)
                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.CallWebServiceUpdate", "ShopWebServiceENG.CallWebServiceUpdate : " & ret.ErrorMessage)
                End If
            End If

            Return ret
        End Function

        Public Function UpdateAppointmentJobStatus(ByVal AppointmentJobID As Long, ByVal ActiveStatus As String) As CenParaDB.Appointment.SaveAppointmentJobPara
            'Save Appointment Job
            Dim pJob As New CenParaDB.Appointment.SaveAppointmentJobPara
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbAppointmentJobCenLinqDB
            lnq.GetDataByPK(AppointmentJobID, trans.Trans)
            If lnq.ID > 0 Then
                Dim pPara As New CenParaDB.TABLE.TbAppointmentJobCenParaDB
                pPara = lnq.GetParameter(lnq.ID, trans.Trans)
                pPara.ACTIVE_STATUS = ActiveStatus
                trans.CommitTransaction()

                pJob = SaveAppointmentJob(pPara)
                pPara = Nothing
            Else
                trans.RollbackTransaction()
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.UpdateAppointmentJobStatus", "ShopWebServiceENG.UpdateAppointmentJobStatus : " & pJob.ErrorMessage)
            End If

            Return pJob
        End Function

#End Region

        Public Function CheckActiveAppointment(ByVal Mobile As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbAppointmentJobCenLinqDB
            Dim sql As String = "select top 1 id, convert(varchar(5),start_slot,114) appointment_time"
            sql += " from tb_appointment_job "
            sql += " where active_status in ('" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "','" & Constant.TbAppointmentCustomer.ActiveStatus.RegisterAtKiosk & "') "
            sql += " and mobile_no='" & Mobile & "'"
            'sql += " and convert(varchar(8),start_slot,112) = convert(varchar(8),getdate(),112)"
            'sql += " and convert(varchar(5),start_slot,114) >= CONVERT(varchar(5),getdate(),114)"

            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            dt.DefaultView.RowFilter = "appointment_time >= '" & DateTime.Now.ToString("HH:mm") & "'"
            If dt.DefaultView.Count > 0 Then
                ret = True
            Else
                'กรณีไม่เจออาจจะเป็นเพราะมีการ Regis มาแล้วและเวลาปัจจุบันได้ล่วงเลยเวลา start_slot มาแล้ว
                'ให้ตรวจสอบสถานะของการจอง
                If dt.Rows.Count > 0 Then
                    ret = True
                End If
            End If
            dt.Dispose()
            lnq = Nothing
            trans.CommitTransaction()

            Return ret
        End Function



#Region "Image Capture File"
        Public Function SendImageFile(ByVal FileByte() As Byte, ByVal MobileNo As String, ByVal QueueNo As String, ByVal AssignTime As DateTime, ByVal CaptureTime As DateTime) As Long
            Dim ret As Long = 0
            Try
                Dim shTrans As New TransactionDB
                Dim cLnq As New ShLinqDB.TABLE.TbCustomerShLinqDB
                cLnq.ChkDataByMOBILE_NO(MobileNo, shTrans.Trans)
                If cLnq.ID <> 0 Then
                    Dim CustCapturePath As String = FunctionEng.GetConfig("CustomerPicturePath")  ' "D:\Customer Picture\data\"
                    Dim Fld As String = CustCapturePath & DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "\"
                    If Directory.Exists(Fld) = False Then
                        Directory.CreateDirectory(Fld)
                    End If

                    'Dim vDateNow As DateTime = DateTime.Now
                    Dim ShopAbb As String = FunctionEng.GetConfig("ShopAbbCode")
                    Dim PicFileName As String = cLnq.MOBILE_NO & "_" & cLnq.CONTACT_ID & ".jpg"
                    Dim fs As FileStream
                    If File.Exists(Fld & PicFileName) = True Then
                        File.Delete(Fld & PicFileName)
                    End If

                    fs = New FileStream(Fld & PicFileName, FileMode.CreateNew)
                    fs.Write(FileByte, 0, FileByte.Length)
                    fs.Close()

                    Dim p As New TbCustomerImageShLinqDB
                    p.CAPTURE_TIME = CaptureTime
                    p.MOBILE_NO = MobileNo
                    p.QUEUE_NO = QueueNo
                    If AssignTime.Year <> 1 Then
                        p.ASSIGN_TIME = AssignTime
                    End If
                    p.CONTACTID = cLnq.CONTACT_ID
                    p.CUSTOMER_FIRST_NAME = cLnq.F_NAME
                    p.CUSTOMER_LAST_NAME = cLnq.L_NAME
                    p.CUSTOMER_STATUS = cLnq.MOBILE_STATUS
                    p.LAST_CAPTURE_DATE = cLnq.URL_CAPTURE_DATE
                    p.NETWORK_TYPE = cLnq.NETWORK_TYPE
                    p.ACCOUNT_NO = cLnq.ACCOUNT_NO
                    p.WEBSERVICE_HOST_IP = FunctionEng.GetIPAddress

                    If p.InsertData("Kiosk", shTrans.Trans) Then
                        Dim tmpRet As Boolean = CreateIndexFile(ShopAbb, Fld, shTrans)
                        If tmpRet = True Then
                            shTrans.CommitTransaction()
                            ret = p.ID
                        Else
                            shTrans.RollbackTransaction()

                            If File.Exists(Fld & PicFileName) = True Then
                                File.Delete(Fld & PicFileName)
                            End If
                        End If
                    Else
                        shTrans.RollbackTransaction()
                        FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SendImageFile", p.ErrorMessage)

                        If File.Exists(Fld & PicFileName) = True Then
                            File.Delete(Fld & PicFileName)
                        End If
                    End If
                    p = Nothing
                Else
                    FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SendImageFile", shTrans.ErrorMessage)
                End If
            Catch ex As Exception
                ret = 0
                FunctionEng.SaveShopErrorLog("ShopWebServiceENG.SendImageFile", "Exception : " & ex.Message)
            End Try

            Return ret
        End Function

        Public Function LoadImageFile(ByVal MobileNo As String) As CaptureImageResponsePara
            Dim ret As New CaptureImageResponsePara

            Dim lnq As New TbCustomerImageShLinqDB
            Dim dt As New DataTable

            Dim vDateNow As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim wh As String = " mobile_no='" & MobileNo & "'"
            wh += " and convert(varchar(8),capture_time,112) between '" & vDateNow & "' and '" & vDateNow & "'"
            dt = lnq.GetDataList(wh, "", Nothing)
            If dt.Rows.Count > 0 Then
                Dim shTrans As New TransactionDB
                Dim cLnq As New ShLinqDB.TABLE.TbCustomerShLinqDB
                cLnq.ChkDataByMOBILE_NO(MobileNo, shTrans.Trans)
                If cLnq.ID <> 0 Then
                    Dim CustCapturePath As String = FunctionEng.GetConfig("CustomerPicturePath")  ' "D:\Customer Picture\data\"
                    Dim Fld As String = CustCapturePath & DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "\"
                    Dim ShopAbb As String = FunctionEng.GetConfig("ShopAbbCode")
                    Dim PicFileName As String = cLnq.MOBILE_NO & "_" & cLnq.CONTACT_ID & ".jpg"

                    Dim FileByte() As Byte = Nothing
                    If File.Exists(Fld & PicFileName) = True Then
                        FileByte = File.ReadAllBytes(Fld & PicFileName)
                        ret.CaptureResult = True
                        ret.CaptureImage = FileByte
                    End If
                End If
                cLnq = Nothing
                shTrans.CommitTransaction()
            End If
            dt.Dispose()
            lnq = Nothing

            Return ret
        End Function

        Public Sub UpdateCustCaptureIndexFile()
            Dim CustCapturePath As String = FunctionEng.GetConfig("CustomerPicturePath")  ' "D:\Customer Picture\data\"
            Dim Fld As String = CustCapturePath & DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "\"
            Dim ShopAbb As String = FunctionEng.GetConfig("ShopAbbCode")
            If ShopAbb.Trim <> "" Then
                Try
                    Dim shTrans As New TransactionDB
                    If CreateIndexFile(ShopAbb, Fld, shTrans) = True Then
                        shTrans.CommitTransaction()
                    Else
                        shTrans.RollbackTransaction()
                    End If
                Catch ex As Exception
                    FunctionEng.SaveShopErrorLog("ShopWebServiceEng.UpdateCustCaptureIndexFile", "Exception :" & ex.Message)
                End Try
            End If
        End Sub

        Private Function CreateIndexFile(ByVal ShopAbb As String, ByVal FolderPath As String, ByVal shTrans As TransactionDB) As Boolean
            Dim ret As Boolean = False
            Try
                Dim StrDateNow As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim FileName As String = FolderPath & ShopAbb & "_" & StrDateNow & ".DAT"
                If File.Exists(FileName) Then
                    File.Delete(FileName)
                End If

                Dim dt As New DataTable
                Dim lnq As New TbCustomerImageShLinqDB
                dt = lnq.GetDataList("convert(varchar(8),capture_time,112)='" & StrDateNow & "'", "capture_time", shTrans.Trans)
                If dt.Rows.Count > 0 Then
                    Dim IndexText As String = "01|" & ShopAbb & "_" & StrDateNow & vbCrLf
                    For Each dr As DataRow In dt.Rows
                        Dim p As New TbCustomerImageShParaDB
                        p = lnq.GetParameter(dr("id"), shTrans.Trans)

                        IndexText += "02|" & ShopAbb & "|"
                        IndexText += p.QUEUE_NO & "|"
                        If p.ASSIGN_TIME.Value.Year <> 1 Then
                            IndexText += p.ASSIGN_TIME.Value.ToString("yyyyMMdd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "|"
                        Else
                            IndexText += "|"
                        End If
                        IndexText += p.MOBILE_NO & "|"
                        IndexText += p.CONTACTID & "|"
                        IndexText += p.CUSTOMER_FIRST_NAME & "|"
                        IndexText += p.CUSTOMER_LAST_NAME & "|"
                        IndexText += p.CUSTOMER_STATUS & "|"
                        IndexText += p.LAST_CAPTURE_DATE & "|"
                        IndexText += p.NETWORK_TYPE & "|"
                        IndexText += p.ACCOUNT_NO & vbCrLf

                        p = Nothing
                    Next
                    IndexText += "09|" & dt.Rows.Count

                    Dim objWriter As New System.IO.StreamWriter(FileName, True)
                    objWriter.WriteLine(IndexText)
                    objWriter.Close()
                    ret = True
                End If
                lnq = Nothing
                dt.Dispose()
            Catch ex As Exception
                ret = False
                FunctionEng.SaveShopErrorLog("ShopWebServiceEng.CreateIndexFile", "Exception :" & ex.Message)
            End Try

            Return ret
        End Function

        Private Function SetQueryText(ByVal data As Object) As String
            If Convert.IsDBNull(data) = False Then
                Return data.ToString
            End If
            Return ""
        End Function
#End Region

        Public Function CreateFailOverDBLog(ByVal ClientIP As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal ActiveDB As String, ByVal SQL As String) As String
            Dim ret As String = ""
            Try
                Dim Fld As String = "C:\FailOverLog"

                If Directory.Exists(Fld) = False Then
                    Directory.CreateDirectory(Fld)
                End If

                Dim LogText As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                LogText += " Software Name=" & SoftwareName
                LogText += " ClientIP=" & ClientIP
                LogText += " Function Name=" & FunctionName
                LogText += " Active DB = " & ActiveDB & vbCrLf
                If SQL.Trim <> "" Then
                    LogText += " SQL=" & SQL & vbCrLf & vbCrLf
                End If

                Dim FileName As String = Fld & "\FailOverLog_" & DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & ".log"
                Dim objWriter As New System.IO.StreamWriter(FileName, True)
                objWriter.WriteLine(LogText)
                objWriter.Close()
            Catch ex As Exception
                ret = ex.Message
            End Try

            Return ret
        End Function
    End Class
End Namespace

