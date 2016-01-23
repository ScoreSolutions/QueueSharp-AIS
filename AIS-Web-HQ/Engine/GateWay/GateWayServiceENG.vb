Imports pCen = CenParaDB.TABLE
Imports System.IO
Imports System.Net
Imports Engine.Common
Imports paCen = CenParaDB.GateWay
Imports CenParaDB.GateWay
Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE
Imports CenParaDB.Common.Utilities
Imports System.Text
Imports System.Collections.Specialized

Namespace GeteWay
    Public Class GateWayServiceENG

#Region "CustomerProfile"
        Public Function GetCustomerProfile(ByVal MobileNo As String, ByVal SessionID As String) As pCen.TbCustomerCenParaDB
            Dim para As New pCen.TbCustomerCenParaDB
            para = GetCustomerProfileFormGW(MobileNo, False, False, SessionID, "")
            If para.RESPONSE_MSG <> "" Then
                para = GetCustomerFromHQ(MobileNo)
            End If

            Return para
        End Function

        Public Function GetCustomerProfile(ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim para As New pCen.TbCustomerCenParaDB
            para = GetCustomerProfileFormGW(MobileNo, False, False, "", "")
            If para.RESPONSE_MSG <> "" Then
                para = GetCustomerFromHQ(MobileNo)
            End If

            Return para
        End Function

        Private Function GetCustomerProfileFormGW(ByVal MobileNo As String, ByVal IsArBalance As Boolean, ByVal IsCampaign As Boolean, ByVal SessionID As String, ByVal ShopCode As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB
            'http://10.13.174.95/AISQ/interface/getmobileinfo.php?MOBILENO=0821156901&GETNEW=1&REQVAR=TITLE|NAME|CONTACT_EMAIL|LOCAL_LANGUAGE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|SUB_NETWORK_TYPE
            Dim ReqVar As String = "TITLE|NAME|CONTACT_EMAIL|NATIONALITY_CODE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|CHURN|SUB_NETWORK_TYPE|ASSET_ID|BILL_CYCLE|ACCOUNT_NO|GROUP_CODE|CRM_SEGMENT|CORPORATE_TYPE|ACCOUNT_NUM|PROVINCE_CODE|NETWORK_TYPE|ID_CARD_NO|PASSWORD|CA_BLACKLIST|CONTRACT_PHONE|PA_GROUP|PAYMENT_TYPE|ACCOUNT_NUM2|REGION_CODE|LOCAL_LANGUAGE|PP_COS_ID|TPURCode|CA_NAME|CA_ID_CARD_NO|CLV_SEGMENT|REMARK|TUX_CODE"
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&GETNEW=1&REQVAR=" & ReqVar
            Dim URL1 As String = FunctionEng.GetQisDBConfig("CustomerProfileURL1") & InputPara

            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webRequest.Timeout = 10000
                webresponse = webRequest.GetResponse()
                ret = GetProfileMsg(webresponse, MobileNo)
                FunctionEng.SaveTransLog("GateWayServiceENG.GetCustomerProfileFromGW", URL1)

                If IsArBalance = True Then
                    ret = GetArBalance(ret)
                End If
                If IsCampaign = True Then
                    ret = GetCampaign(ret, SessionID, ShopCode)
                End If
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("CustomerProfileURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCustomerProfileFromGW", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webRequest.Timeout = 10000
                    webresponse = webRequest.GetResponse()
                    ret = GetProfileMsg(webresponse, MobileNo)
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCustomerProfileFromGW", URL2)

                    If IsArBalance = True Then
                        ret = GetArBalance(ret)
                    End If
                    If IsCampaign = True Then
                        ret = GetCampaign(ret, SessionID, ShopCode)
                    End If
                Catch ex1 As Exception
                    ret.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCustomerProfileFromGW", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function GetProfileMsg(ByVal webresponse As WebResponse, ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp() As String = Split(Stream.ReadLine(), "#")

                'TITLE|NAME|CONTACT_EMAIL|NATIONALITY_CODE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|CHURN|SUB_NETWORK_TYPE|ASSET_ID|BILL_CYCLE|ACCOUNT_NO|GROUP_CODE|CRM_SEGMENT|CORPORATE_TYPE|ACCOUNT_NUM|PROVINCE_CODE|NETWORK_TYPE|ID_CARD_NO|PASSWORD|CA_BLACKLIST|CONTRACT_PHONE|PA_GROUP|PAYMENT_TYPE|ACCOUNT_NUM2|REGION_CODE|LOCAL_LANGUAGE|PP_COS_ID|TPURCode|CA_NAME|CA_ID_CARD_NO|CLV_SEGMENT|REMARK|TUX_CODE
                ret.MOBILE_NO = MobileNo
                ret.TITLE_NAME = tmp(0)
                If tmp(1).Trim <> "" Then
                    ret.F_NAME = tmp(1).Split(" ")(0)
                    ret.L_NAME = tmp(1).Split(" ")(1)
                End If
                ret.EMAIL = tmp(2)
                ret.PREFER_LANG = tmp(3)
                ret.SEGMENT_LEVEL = tmp(4)
                ret.BIRTH_DATE = tmp(5)
                ret.MOBILE_STATUS = tmp(6)
                ret.CATEGORY = MappingCustomer("GROUP_CODE", tmp(7))
                ret.CONTACT_CLASS = MappingCustomer("CUST_CLASS", tmp(8))
                ret.SERVICE_YEAR = CalServiceYear(tmp(9))
                ret.CHUM_SCORE = tmp(10)
                ret.NETWORK_TYPE = MappingNetworkType(tmp(20), tmp(11))
                ret.ASSET_ID = tmp(12)
                ret.CORPORATE_TYPE = MappingCorporateType(tmp(17))

                UpdateCustomerProfile(ret)

                For i As Integer = 13 To tmp.Length - 1
                    If ret.OTHER_INFO.Trim = "" Then
                        ret.OTHER_INFO = tmp(i)
                    Else
                        ret.OTHER_INFO += "#" & tmp(i)
                    End If
                Next
            Else
                ret.RESPONSE_MSG = "ข้อมูลไม่ถูกต้อง"
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Sub UpdateCustomerProfile(ByVal p As pCen.TbCustomerCenParaDB)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerCenLinqDB
                lnq.ChkDataByMOBILE_NO(p.MOBILE_NO, trans.Trans)
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

                Dim ret As Boolean = False
                If lnq.ID <> 0 Then
                    ret = lnq.UpdateByPK("GateWayServiceENG.UpdateCustomerProfile", trans.Trans)
                Else
                    lnq.MOBILE_NO = p.MOBILE_NO
                    ret = lnq.InsertData("GateWayServiceENG.UpdateCustomerProfile", trans.Trans)
                End If
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
                lnq = Nothing
            End If
            p = Nothing
        End Sub

        Private Function MappingCorporateType(ByVal CorporateTypeCode As String) As String
            Dim ret As String = ""
            If CorporateTypeCode.Trim <> "" Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerCorporateTypeCenLinqDB
                lnq.ChkDataByCORPORATE_TYPE_CODE(CorporateTypeCode, Nothing)
                ret = lnq.CORPORATE_TYPE_NAME
                lnq = Nothing
            End If
            Return ret
        End Function

        Private Function MappingNetworkType(ByVal NetworkType As String, ByVal SubNetworkType As String) As String
            Dim ret As String = ""
            If NetworkType.Trim <> "" And SubNetworkType.Trim <> "" Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerNetworkTypeCenLinqDB
                lnq.ChkDataByNETWORK_TYPE_SUB_NETWORK_TYPE(NetworkType, SubNetworkType, Nothing)
                ret = lnq.DISPLAY_VALUE
                lnq = Nothing
            End If
            Return ret
        End Function

        Private Function MappingCustomer(ByVal FieldName As String, ByVal vGroupCode As String) As String
            Dim ret As String = ""
            If vGroupCode.Trim <> "" Then
                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim lnq As New CenLinqDB.TABLE.TbCustomerMappingLinq
                Dim dt As DataTable = lnq.GetDataList("field_name='" & FieldName & "' and mapping_code='" & vGroupCode & "'", "", trans.Trans)
                If dt.Rows.Count > 0 Then
                    ret = dt.Rows(0)("display_value")
                End If
            End If
            Return ret
        End Function

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
                Dim MonthQty As Int16 = (MonthDiff Mod 12)
                If CInt(dd) <> CInt(Date.Now.Day) Then
                    MonthQty += 1
                    If MonthQty >= 12 Then
                        YearQty += 1
                        MonthQty = 0
                    End If
                End If
                ret = YearQty & " Year " & MonthQty & " Month"
            End If
            Return ret
        End Function

        Private Function GetArBalance(ByVal para As pCen.TbCustomerCenParaDB) As pCen.TbCustomerCenParaDB
            Dim InputPara As String = "MOBILENO=" & para.MOBILE_NO
            Dim URL1 As String = FunctionEng.GetQisDBConfig("getArBalanceURL1") & InputPara

            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                para = GetArBalanceMsg(webresponse, para)
                FunctionEng.SaveTransLog("GateWayServiceENG.GetArBalance", URL1)
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("getArBalanceURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetArBalance", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    para = GetArBalanceMsg(webresponse, para)
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetArBalance", URL2)
                Catch ex1 As Exception
                    para.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetArBalance", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            Return para
        End Function

        Private Function GetArBalanceMsg(ByVal webresponse As WebResponse, ByVal para As pCen.TbCustomerCenParaDB) As pCen.TbCustomerCenParaDB
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

#Region "Get Customer Campaign"

        Public Function GetCampaignTest(ByVal AudienceIdValue As String, ByVal MobileNo As String, ByVal SessionID As String) As pCen.TbCustomerCenParaDB
            Dim para As New pCen.TbCustomerCenParaDB
            para.ASSET_ID = AudienceIdValue
            para.MOBILE_NO = MobileNo
            para.NETWORK_TYPE = "POST-PAID"

            Return GetCampaign(para, SessionID, "PK")
        End Function

        Public Function GetCampaign(ByVal para As pCen.TbCustomerCenParaDB, ByVal SessionID As String, ByVal ShopCode As String) As pCen.TbCustomerCenParaDB
            If GetCampaignStartSession(para, SessionID) = True Then
                Dim OfferMsg As String = ""
                Dim OfferResult() As String = GetCampaignOffer(SessionID)
                If OfferResult.Length > 0 Then
                    Dim campPara() As String = Split(OfferResult(0), "|")
                    para.CAMPAIGN_CODE = "สิทธิพิเศษ"
                    para.CAMPAIGN_NAME_EN = "Privilege"

                    Dim CampaignDescTh() As String = Split(campPara(2), "[LINEBREAK]")
                    If CampaignDescTh.Length >= 2 Then
                        para.CAMPAIGN_NAME = CampaignDescTh(0)
                        para.CAMPAIGN_DESC_TH2 = CampaignDescTh(1)
                    Else
                        para.CAMPAIGN_NAME = CampaignDescTh(0)
                    End If

                    Dim CampaignDescEng() As String = Split(campPara(1), "[LINEBREAK]")
                    If CampaignDescEng.Length >= 2 Then
                        para.CAMPAIGN_DESC = CampaignDescEng(0)
                        para.CAMPAIGN_DESC_EN2 = CampaignDescEng(1)
                    Else
                        para.CAMPAIGN_DESC = CampaignDescEng(0)
                    End If

                    For Each camp As String In OfferResult
                        If camp.Trim <> "" Then
                            Dim tmp() As String = Split(camp, "|")
                            If tmp.Length > 0 Then
                                GetCampaignPostEvent(SessionID, tmp(6), para.MOBILE_NO, tmp(8), tmp(7), tmp(5), tmp(1), ShopCode)
                            End If
                        End If
                    Next
                End If
            End If
            Return para
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
            InputPara += "USERDEFINEDFIELDS=" & ShopCode

            Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractPostEventURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                GetCampaignPostEventResult(webresponse)
                FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignPostEvent", URL1)
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractPostEventURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignPostEvent", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    GetCampaignPostEventResult(webresponse)
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignPostEvent", URL2)
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignPostEvent", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
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
                    End If
                Else
                    ret = False
                End If
            Else
                ret = False
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

            Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractGetOfferURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetCampaignOfferResult(webresponse)
                FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignOffer", URL1)
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractGetOfferURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignOffer", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetCampaignOfferResult(webresponse)
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignOffer", URL2)
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignOffer", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
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

        Private Function GetCampaignStartSession(ByVal para As pCen.TbCustomerCenParaDB, ByVal SessionID As String) As Boolean
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

            Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractStartSessionURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetCampaignStartSessionResult(webresponse)
                FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignStartSession", URL1)
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractStartSessionURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignStartSession", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetCampaignStartSessionResult(webresponse)
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignStartSession", URL2)
                Catch ex1 As Exception
                    para.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignStartSession", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
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
                    End If
                Else
                    ret = False
                End If
            Else
                ret = False
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function
#End Region

#Region "Get Customer From HQ DB"
        Private Function GetCustomerFromHQ(ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbCustomerCenLinqDB
            ret = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)
            If lnq.HaveData = True Then
                FunctionEng.SaveTransLog("MobileEng.GetCustomerFromHQ", "Get Data From HQ Mobile No : " & MobileNo)
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                ret.RESPONSE_MSG = lnq.ErrorMessage
                FunctionEng.SaveErrorLog("MobileEng.GetCustomerFromHQ", "Mobile No : " & MobileNo & "  " & lnq.ErrorMessage)
            End If

            Return ret
        End Function
#End Region
#End Region
#Region "LDAP"
        Public Function LDAPAuth(ByVal UserName As String, ByVal Pwd As String) As paCen.LDAPResponsePara
            Dim ret As New paCen.LDAPResponsePara
            'TQISAUTHEN    'for test
            Dim InputPara As String = "USERNAME=" & UserName & "&PWD=" & System.Web.HttpUtility.UrlEncode(Pwd) & "&PROJCODE=" & FunctionEng.GetQisDBConfig("q_ldap_proj_code")
            Dim LDAP_URL1 As String = FunctionEng.GetQisDBConfig("ldap_url1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(LDAP_URL1)
                webresponse = webRequest.GetResponse()
                ret = GetLDAPMsg(webresponse)
            Catch ex As Exception
                Try
                    Dim LDAP_URL2 As String = FunctionEng.GetQisDBConfig("ldap_url2") & InputPara
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(LDAP_URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetLDAPMsg(webresponse)
                Catch ex1 As Exception
                    ret.RESULT = False
                    ret.RESPONSE_MSG = ex1.Message
                End Try
            End Try
            Return ret
        End Function

        Private Function GetLDAPMsg(ByVal webresponse As WebResponse) As paCen.LDAPResponsePara
            Dim ret As New paCen.LDAPResponsePara
            Dim Stream As New StreamReader(webresponse.GetResponseStream())
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp = "AUTHENSUCCESS" Then
                    ret.RESULT = True
                Else
                    ret.RESULT = False
                    ret.RESPONSE_MSG = tmp
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function


#End Region

#Region "Send SMS"
        Public Function SendSMS(ByVal MobileNo As String, ByVal Msg As String) As SMSResponsePara
            Dim ret As New SMSResponsePara
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&SMSTXT=" & Msg
            Dim SMS_URL1 As String = FunctionEng.GetQisDBConfig("send_sms_url1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(SMS_URL1)
                ret = GetSMSMsg(webRequest, webresponse, InputPara)
                If ret.RESULT = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", ret.ERROR_RESPONSE & " URL :" & SMS_URL1)
                Else
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL1)
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL1 & " Exception :" & ex.Message)

                Dim SMS_URL2 As String = FunctionEng.GetQisDBConfig("send_sms_url2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(SMS_URL2)
                    ret = GetSMSMsg(webRequest, webresponse, InputPara)
                    If ret.RESULT = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", ret.ERROR_RESPONSE & " URL :" & SMS_URL2)
                    Else
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL2)
                    End If
                Catch ex1 As Exception
                    ret.ERROR_RESPONSE = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL2 & " Exception :" & ex1.Message)
                End Try

            End Try

            Return ret
        End Function

        Private Function GetSMSMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String) As SMSResponsePara
            Dim ret As New SMSResponsePara
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp = "DONE" Then
                        ret.RESULT = True
                    Else
                        ret.RESULT = False
                        ret.ERROR_RESPONSE = tmp
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret.RESULT = False
                ret.ERROR_RESPONSE = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region "ATSR Post Servey"
        Public Function SendATSR(ByVal MobileNo As String, ByVal QueueID As String, ByVal ShopCode As String, ByVal TemplateID As String) As Boolean
            Dim ret As Boolean = False
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&QUNIQID=" & QueueID & "&BRANCHCODE=" & ShopCode & "&TEMPLATEID=" & TemplateID
            Dim URL1 As String = FunctionEng.GetQisDBConfig("ATSR_AddSurveyURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = SendATSRMsg(webresponse)
                If ret = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", "URL :" & URL1)
                Else
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendATSR", URL1)
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", URL1 & " Exception :" & ex.Message)
                Dim URL2 As String = FunctionEng.GetQisDBConfig("ATSR_AddSurveyURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = SendATSRMsg(webresponse)
                    If ret = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", "URL :" & URL2)
                    Else
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendATSR", URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", URL2 & " Exception :" & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function SendATSRMsg(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim = "DONE" Then
                    ret = True
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Public Function GetATSRSurveyResult(ByVal QUNIQID As String) As ATSRResultPara
            Dim ret As New ATSRResultPara
            Dim InputPara As String = "QUNIQID=" & QUNIQID
            Dim URL1 As String = FunctionEng.GetQisDBConfig("ATSR_GetSurveyResultURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetATSRSurverResultMsg(webresponse, ret)
                If ret.RESULT <> "DONE" Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", "URL :" & URL1)
                Else
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetATSRSurveyResult", URL1)
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", URL1 & " Exception :" & ex.Message)
                Dim URL2 As String = FunctionEng.GetQisDBConfig("ATSR_GetSurveyResultURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetATSRSurverResultMsg(webresponse, ret)
                    If ret.RESULT <> "DONE" Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", "URL :" & URL2)
                    Else
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetATSRSurveyResult", URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", URL2 & " Exception :" & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function GetATSRSurverResultMsg(ByVal webresponse As WebResponse, ByVal ret As ATSRResultPara) As ATSRResultPara
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp() As String = Split(Stream.ReadLine(), "#")
                If tmp.Length > 0 Then
                    If tmp.Length = 2 Then
                        ret.RESULT = tmp(0)
                        ret.RESULT_STATE = tmp(1)
                    ElseIf tmp.Length > 0 Then
                        ret.RESULT = tmp(0)
                        ret.RESULT_STATE = tmp(1)
                        ret.RESULT_VALUE = tmp(2)
                        ret.ATSR_CALL_TIME = tmp(7)
                    End If
                Else
                    ret.ERROR_MESSAGE = "Fail No return data"
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function
#End Region

#Region "Send Email"
        Public Function SendEmail(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String) As Boolean
            'http://10.13.181.100/AISQ/interface/sendemail.php?ADDRTO=akkarawatp@scoresolutions.co.th&MAILSUB=Test From AIS ทดสอบการส่งเมล์&MAILMSG=Mail Message รายละเอียดอีเมล์
            Dim ret As Boolean = False

            Dim InputPara As String = "ADDRTO=" & EmailTo & "&MAILSUB=" & MailSubject & "&MAILMSG=" & msg
            Dim SMS_URL1 As String = FunctionEng.GetQisDBConfig("SendMailURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(SMS_URL1)
                ret = GetEmailMsg(webRequest, webresponse, InputPara)
                If ret = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", "Send Email Fail" & " URL :" & SMS_URL1)
                Else
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL1)
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL1 & " Exception :" & ex.Message)

                Dim SMS_URL2 As String = FunctionEng.GetQisDBConfig("SendMailURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(SMS_URL2)
                    ret = GetEmailMsg(webRequest, webresponse, InputPara)
                    If ret = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", "Send Email Fail" & " URL :" & SMS_URL2)
                    Else
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL2 & " Exception :" & ex1.Message)
                End Try
            End Try

            Return ret
        End Function
        Private Function GetEmailMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String) As Boolean
            Dim ret As Boolean = False
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"

            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp.Trim = "DONE" Then
                        ret = True
                    Else
                        ret = False
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret = False
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            Return ret
        End Function


        'Public Shared Sub UploadFilesToRemoteUrl(ByVal url As String, ByVal files As String(), ByVal nvc As NameValueCollection)
        '    Dim length As Long = 0
        '    Dim boundary As String = "----------------------------" & DateTime.Now.Ticks.ToString("x")

        '    Dim httpWebRequest2 As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        '    httpWebRequest2.ContentType = "multipart/form-data; boundary=" & boundary
        '    httpWebRequest2.Method = "POST"
        '    httpWebRequest2.KeepAlive = True
        '    httpWebRequest2.Credentials = System.Net.CredentialCache.DefaultCredentials

        '    Dim memStream As Stream = New System.IO.MemoryStream()
        '    Dim boundarybytes As Byte() = System.Text.Encoding.ASCII.GetBytes(vbCr & vbLf & "--" & boundary & vbCr & vbLf)
        '    Dim formdataTemplate As String = vbCr & vbLf & "--" & boundary & vbCr & vbLf & "Content-Disposition: form-data; name={0};" & vbCr & vbLf & vbCr & vbLf & "{1}"
        '    For Each key As String In nvc.Keys
        '        Dim formitem As String = String.Format(formdataTemplate, key, nvc(key))
        '        Dim formitembytes As Byte() = System.Text.Encoding.UTF8.GetBytes(formitem)
        '        memStream.Write(formitembytes, 0, formitembytes.Length)
        '    Next
        '    memStream.Write(boundarybytes, 0, boundarybytes.Length)

        '    Dim headerTemplate As String = "Content-Disposition: form-data; name={0}; filename={1}" & vbCr & vbLf & " Content-Type: application/octet-stream" & vbCr & vbLf & vbCr & vbLf
        '    For i As Integer = 0 To files.Length - 1
        '        Dim header As String = String.Format(headerTemplate, "ATTIMG" & (i + 1), files(i))
        '        Dim headerbytes As Byte() = System.Text.Encoding.UTF8.GetBytes(header)
        '        memStream.Write(headerbytes, 0, headerbytes.Length)
        '        Dim fileStream As New FileStream(files(i), FileMode.Open, FileAccess.Read)
        '        Dim buffer As Byte() = New Byte(fileStream.Length - 1) {}
        '        Dim bytesRead As Integer = 0
        '        Do
        '            bytesRead = fileStream.Read(buffer, 0, buffer.Length)
        '            memStream.Write(buffer, 0, bytesRead)
        '        Loop While bytesRead <> 0
        '        memStream.Write(boundarybytes, 0, boundarybytes.Length)
        '        fileStream.Close()
        '    Next

        '    httpWebRequest2.ContentLength = memStream.Length
        '    Dim requestStream As Stream = httpWebRequest2.GetRequestStream()
        '    memStream.Position = 0
        '    Dim tempBuffer As Byte() = New Byte(memStream.Length - 1) {}
        '    memStream.Read(tempBuffer, 0, tempBuffer.Length)
        '    memStream.Close()
        '    requestStream.Write(tempBuffer, 0, tempBuffer.Length)
        '    requestStream.Close()

        '    Dim webResponse2 As WebResponse = httpWebRequest2.GetResponse()
        '    Dim stream2 As Stream = webResponse2.GetResponseStream()
        '    Dim reader2 As New StreamReader(stream2)

        '    webResponse2.Close()
        '    httpWebRequest2 = Nothing
        '    webResponse2 = Nothing
        'End Sub


        Private Function BuiltMailAttFile(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String, ByVal MailURL As String, ByVal files() As String) As String
            Dim ret As String = ""
            Dim length As Long = 0
            Dim boundary As String = "----------------------------" & DateTime.Now.Ticks.ToString("x")

            Dim httpWebRequest2 As HttpWebRequest = DirectCast(WebRequest.Create(MailURL), HttpWebRequest)
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" & boundary
            httpWebRequest2.Method = "POST"
            httpWebRequest2.KeepAlive = True
            httpWebRequest2.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim memStream As Stream = New System.IO.MemoryStream()
            Dim boundarybytes As Byte() = System.Text.Encoding.ASCII.GetBytes(vbCr & vbLf & "--" & boundary & vbCr & vbLf)
            Dim formdataTemplate As String = vbCr & vbLf & "--" & boundary & vbCr & vbLf & "Content-Disposition: form-data; name={0};" & vbCr & vbLf & vbCr & vbLf & "{1}"

            Dim nvc As New NameValueCollection
            nvc.Add("ADDRTO", EmailTo)
            nvc.Add("MAILSUB", MailSubject)
            nvc.Add("MAILMSG", msg)
            For Each key As String In nvc.Keys
                
                'Dim formitem As String = String.Format(formdataTemplate, key, nvc(key))
                'Dim formitembytes As Byte() = System.Text.Encoding.UTF8.GetBytes(formitem)
                'memStream.Write(formitembytes, 0, formitembytes.Length)

                Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
                Dim formitem As String = String.Format(formdataTemplate, key, nvc(key))
                Dim formitembytes As Byte() = thaiEnc.GetBytes(formitem)
                memStream.Write(formitembytes, 0, formitembytes.Length)
            Next
            memStream.Write(boundarybytes, 0, boundarybytes.Length)

            Dim headerTemplate As String = "Content-Disposition: form-data; name={0}; filename={1}" & vbCr & vbLf & " Content-Type: application/octet-stream" & vbCr & vbLf & vbCr & vbLf
            For i As Integer = 0 To files.Length - 1
                Dim header As String = String.Format(headerTemplate, "ATTIMG" & (i + 1), files(i))
                Dim headerbytes As Byte() = System.Text.Encoding.UTF8.GetBytes(header)
                memStream.Write(headerbytes, 0, headerbytes.Length)
                Dim fileStream As New FileStream(files(i), FileMode.Open, FileAccess.Read)
                Dim buffer As Byte() = New Byte(fileStream.Length - 1) {}
                Dim bytesRead As Integer = 0
                Do
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                    memStream.Write(buffer, 0, bytesRead)
                Loop While bytesRead <> 0
                memStream.Write(boundarybytes, 0, boundarybytes.Length)
                fileStream.Close()
            Next

            httpWebRequest2.ContentLength = memStream.Length
            Dim requestStream As Stream = httpWebRequest2.GetRequestStream()
            memStream.Position = 0
            Dim tempBuffer As Byte() = New Byte(memStream.Length - 1) {}
            memStream.Read(tempBuffer, 0, tempBuffer.Length)
            memStream.Close()
            requestStream.Write(tempBuffer, 0, tempBuffer.Length)
            requestStream.Close()

            Dim webResponse2 As WebResponse = httpWebRequest2.GetResponse()
            Dim stream2 As Stream = webResponse2.GetResponseStream()
            Dim reader2 As New StreamReader(stream2)
            ret = reader2.ReadToEnd

            webResponse2.Close()
            httpWebRequest2 = Nothing
            webResponse2 = Nothing

            Return ret
        End Function

        Public Function SendEmailAttFile(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String, ByVal file() As String) As Boolean
            Dim ret As Boolean = False
            'Dim MAIL_URL1 As String = "http://10.13.174.95/AISQ/interface/sendemailembedimg.php"
            Dim MAIL_URL1 As String = FunctionEng.GetQisDBConfig("SendMailEmbdURL1") '"http://10.13.181.100/AISQ/interface/sendemailembedimg.php"
            Dim tmp As String = BuiltMailAttFile(EmailTo, MailSubject, msg, MAIL_URL1, file)
            If tmp.Trim = "DONE" Then
                ret = True
                FunctionEng.SaveTransLog("GateWayServiceENG.SendEmailAttFile", MAIL_URL1)
            Else
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmailAttFile", "Send Email Fail" & " URL :" & MAIL_URL1)

                Dim MAIL_URL2 As String = FunctionEng.GetQisDBConfig("SendMailEmbdURL2") '"http://10.13.181.100/AISQ/interface/sendemailembedimg.php"
                tmp = BuiltMailAttFile(EmailTo, MailSubject, msg, MAIL_URL2, file)
                If tmp.Trim = "DONE" Then
                    ret = True
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendEmailAttFile", MAIL_URL2)
                Else
                    ret = False
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmailAttFile", "Send Email Fail" & tmp & " URL :" & MAIL_URL1)
                End If
            End If
            Return ret
        End Function


        'Public Function SendEmailAttFile(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String) As Boolean
        '    Dim File1 As String = ""
        '    Dim File2 As String = ""
        '    Dim b1() As Byte = IO.File.ReadAllBytes("D:\Tmp\cheesy.gif")
        '    Dim b2() As Byte = IO.File.ReadAllBytes("D:\Tmp\Footer.gif")


        '    Dim ret As Boolean = False


        '    'Using wc As New System.Net.WebClient()
        '    '    wc.UploadFile("http://10.13.181.100/AISQ/interface/sendemailembedimg.php", "D:\Tmp\cheesy.gif")
        '    '    wc.UploadFile("http://10.13.181.100/AISQ/interface/sendemailembedimg.php", "D:\Tmp\Footer.gif")
        '    'End Using

        '    Dim InputPara As String = "ADDRTO=" & EmailTo & "&MAILSUB=" & MailSubject & "&MAILMSG=" & msg
        '    InputPara += "&ATTIMG1=" & Convert.ToBase64String(b1)
        '    InputPara += "&ATTIMG2=" & Convert.ToBase64String(b2)

        '    b1 = Nothing
        '    b2 = Nothing

        '    Dim SMS_URL1 As New StringBuilder
        '    SMS_URL1.Append("http://10.13.181.100/AISQ/interface/sendemailembedimg.php")
        '    'SMS_URL1.Append(InputPara) 'FunctionEng.GetQisDBConfig("SendMailURL1") & InputPara
        '    Try
        '        Dim req As WebRequest
        '        Dim webresponse As WebResponse
        '        req = WebRequest.Create(SMS_URL1.ToString)
        '        req.ContentType = "application/x-www-form-urlencoded"
        '        req.Method = "POST"

        '        Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
        '        Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
        '        Dim os As Stream = Nothing
        '        Try
        '            req.ContentLength = bytes.Length
        '            os = req.GetRequestStream()
        '            os.Write(bytes, 0, bytes.Length)

        '            webresponse = req.GetResponse()
        '            Dim Stream As New StreamReader(webresponse.GetResponseStream())
        '            If Stream.Peek <> -1 Then
        '                Dim tmp As String = Stream.ReadLine()
        '                If tmp.Trim = "DONE" Then
        '                    ret = True
        '                Else
        '                    ret = False
        '                End If
        '            End If
        '            Stream.Close()
        '            Stream = Nothing

        '        Catch ex As WebException
        '            ret = False
        '        Finally
        '            If os IsNot Nothing Then
        '                os.Close()
        '            End If
        '        End Try











        '        If ret = False Then
        '            FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", "Send Email Fail" & " URL :" & SMS_URL1.ToString)
        '        Else
        '            FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL1.ToString)
        '        End If
        '    Catch ex As Exception
        '        'FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL1 & " Exception :" & ex.Message)

        '        'Dim SMS_URL2 As String = FunctionEng.GetQisDBConfig("SendMailURL2") & InputPara
        '        'Try
        '        '    Dim webRequest As WebRequest
        '        '    Dim webresponse As WebResponse
        '        '    webRequest = webRequest.Create(SMS_URL2)
        '        '    ret = GetEmailMsg(webRequest, webresponse, InputPara)
        '        '    If ret = False Then
        '        '        FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", "Send Email Fail" & " URL :" & SMS_URL2)
        '        '    Else
        '        '        FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL2)
        '        '    End If
        '        'Catch ex1 As Exception
        '        '    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL2 & " Exception :" & ex1.Message)
        '        'End Try
        '    End Try

        '    Return ret
        'End Function
#End Region

#Region "Fault Managemnet"
        Public Function SendToSNMP(ByVal SysLocation As String, ByVal HostIP As String, ByVal HostName As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal Desc As String, ByVal FlagClear As String, ByVal AlarmMethod As String) As Boolean
            Dim ret As Boolean = False
            Dim InputPara As String = "Location=" & SysLocation.Trim & "&HostIP=" & HostIP.Trim & "&HostName=" & HostName.Trim & "&AlarmType=" & AlarmType.Trim & "&AlarmName=" & AlarmName.Trim & "&Severity=" & Severity.Trim & "&AlarmValue=" & AlarmValue.Trim & "&Desc=" & Desc.Trim & "&FlagClear=" & FlagClear.Trim & "&AlarmMethod=" & AlarmMethod.Trim
            Dim URL1 As String = FunctionEng.GetQisDBConfig("SNMP_ALARM_URL1") & InputPara
            Dim _ResErr As String = ""
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                ret = GetAlarmMsg(webRequest, webresponse, InputPara, _ResErr)
                If ret = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSMTP", _ResErr & " URL :" & URL1)
                Else
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendToSMTP", URL1)
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSMTP", URL1 & " Exception :" & ex.Message)
                _ResErr = ""
                Dim URL2 As String = FunctionEng.GetQisDBConfig("SNMP_ALARM_URL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    ret = GetAlarmMsg(webRequest, webresponse, InputPara, _ResErr)
                    If ret = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSNMP", _ResErr & " URL :" & URL2)
                    Else
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendToSNMP", URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSNMP", URL2 & " Exception :" & ex1.Message)
                End Try
            End Try

            Return ret
        End Function

        Private Function GetAlarmMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String, ByVal ErrDesc As String) As Boolean
            Dim ret As Boolean = False
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp.Trim = "DONE" Then
                        ret = True
                    Else
                        ret = False
                        ErrDesc = tmp
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret = False
                ErrDesc = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region "Siebel Activity"
        
        Public Function SiebelCreateActivity(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ItemName As String, ByVal AppointmentChannel As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim SubCat As String = ""
            If AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatName
            ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatName
            ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallInbound.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallInbound.CatName
            ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatName
            End If

            Dim sName() As String = Split(ItemName, ",")
            Dim tmpStr As String = ""
            For Each Str As String In sName
                If tmpStr = "" Then
                    tmpStr = "  - " & Str
                Else
                    tmpStr += vbNewLine & "  - " & Str
                End If
            Next

            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)

            Dim SiebelDesc As String = ""
            SiebelDesc += " จองคิว รับบริการ" & vbNewLine & tmpStr & vbNewLine
            SiebelDesc += " วันที่ " & StartSlot.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น."
            SiebelDesc += " สาขา" & FunctionEng.GetShopConfig("s_name_th", shTrans)

            Dim InputPara As String = ""
            InputPara += "SIEBEL_TYPE=Appointment"
            InputPara += "&SIEBEL_ACTIVITYCATEGORY=Appointment"
            InputPara += "&SIEBEL_ACTIVITYSUBCATEGORY=" & SubCat
            InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
            InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN
            InputPara += "&SIEBEL_MOBILENO=" & MobileNo

            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            ret = CallWebService(InputPara)
            If ret.RESULT = True Then
                'Update To shop.TB_APPOINTMENT_CUSTOMER
                Dim sqlU As String = "update tb_appointment_customer "
                sqlU += " set siebel_activity_id='" & ret.ACTIVITY_ID & "', "
                sqlU += " siebel_status = '" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN & "', "
                sqlU += " siebel_desc = '" & SiebelDesc & "'"
                sqlU += " where CONVERT(varchar(16),start_slot,120) = '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' "
                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"

                Dim re As Boolean = lnq.UpdateBySql(sqlU, shTrans.Trans)
                If re = False Then
                    ret.RESULT = False
                    ret.ACTIVITY_ID = ""
                    ret.ErrorMessage = lnq.ErrorMessage

                    shTrans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", lnq.ErrorMessage & " MobileNo:" & MobileNo)
                Else
                    shTrans.CommitTransaction()
                End If
            Else
                FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & MobileNo)

                Dim sqlE As String = "update tb_appointment_customer "
                sqlE += " set siebel_desc='GenSiebelENG.CreateSiebelActivity : " & ret.ErrorMessage & " MobileNo:" & MobileNo & "' "
                sqlE += " where customer_id = '" & MobileNo & "' and CONVERT(varchar(16),start_slot,120)= '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "'"
                Dim re As Boolean = lnq.UpdateBySql(sqlE, shTrans.Trans)
                If re = True Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                End If
            End If
            lnq = Nothing

            Return ret
        End Function


        Public Function SiebelUpdateDescription(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ItemName As String, ByVal AppointmentChannel As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal ActivityID As String) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            
            Dim sName() As String = Split(ItemName, ",")
            Dim tmpStr As String = ""
            For Each Str As String In sName
                If tmpStr = "" Then
                    tmpStr = "  - " & Str
                Else
                    tmpStr += vbNewLine & "  - " & Str
                End If
            Next

            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)

            Dim SiebelDesc As String = ""
            SiebelDesc += " จองคิว รับบริการ" & vbNewLine & tmpStr & vbNewLine
            SiebelDesc += " วันที่ " & StartSlot.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น."
            SiebelDesc += " สาขา" & FunctionEng.GetShopConfig("s_name_th", shTrans)

            Dim InputPara As String = ""
            InputPara += "SIEBEL_ACTID=" & ActivityID
            InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
            InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN

            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            ret = CallWebServiceUpdate(InputPara)
            If ret.RESULT = True Then
                'Update To shop.TB_APPOINTMENT_CUSTOMER
                Dim sqlU As String = "update tb_appointment_customer "
                sqlU += " set siebel_activity_id='" & ret.ACTIVITY_ID & "', "
                sqlU += " siebel_status = '" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN & "', "
                sqlU += " siebel_desc = '" & SiebelDesc & "'"
                sqlU += " where CONVERT(varchar(16),start_slot,120) = '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' "
                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"

                Dim re As Boolean = lnq.UpdateBySql(sqlU, shTrans.Trans)
                If re = False Then
                    ret.RESULT = False
                    ret.ACTIVITY_ID = ""
                    ret.ErrorMessage = lnq.ErrorMessage

                    shTrans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", lnq.ErrorMessage & " MobileNo:" & MobileNo)
                Else
                    shTrans.CommitTransaction()
                End If
            Else
                FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & MobileNo)

                Dim sqlE As String = "update tb_appointment_customer "
                sqlE += " set siebel_desc='GenSiebelENG.CreateSiebelActivity : " & ret.ErrorMessage & " MobileNo:" & MobileNo & "' "
                sqlE += " where customer_id = '" & MobileNo & "' and CONVERT(varchar(16),start_slot,120)= '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "'"
                Dim re As Boolean = lnq.UpdateBySql(sqlE, shTrans.Trans)
                If re = True Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                End If
            End If
            lnq = Nothing

            Return ret
        End Function

        Dim _err As String = ""
        Private Function CallWebService(ByVal InputPara As String) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_url1") & InputPara
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SIEBEL_URL1)
            ret = GetSiebelMsg(webRequest, webresponse, InputPara)

            If ret.RESULT = False Then
                FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebService", ret.ErrorMessage & " " & SIEBEL_URL1)
                Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_url2") & InputPara
                webRequest = webRequest.Create(SIEBEL_URL2)
                ret = GetSiebelMsg(webRequest, webresponse, InputPara)

                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebService", "GenSiebelENG.CallWebService : " & ret.ErrorMessage & " " & SIEBEL_URL2)
                End If
            End If
            Return ret
        End Function

        Private Function GetSiebelMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String, Optional ByVal GenType As String = "CREATE") As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If GenType = "UPDATE" Then
                        If tmp.IndexOf("SUCCESS") > -1 Then
                            ret.RESULT = True
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp
                        End If
                    Else
                        If tmp.IndexOf("SUCCESS-") > -1 Then
                            ret.RESULT = True
                            ret.ACTIVITY_ID = tmp.Replace("SUCCESS-", "")
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp
                        End If
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret.RESULT = False
                ret.ErrorMessage = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            Return ret
        End Function

        Public Function SiebelUpdateToCancel(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal CancelDate As DateTime, ByVal SiebelActivityID As String, ByVal SiebelDesc As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            SiebelDesc += CancelDate.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น." & vbNewLine
            SiebelDesc += "ลูกค้ายกเลิกการจอง"
            ret = UpdateSiebelDesc(SiebelDesc, Constant.TbAppointmentCustomer.Siebel.Status.CANCELLED, SiebelActivityID, MobileNo, shTrans)
            If ret.RESULT = False Then
                'shTrans.RollbackTransaction()
                FunctionEng.SaveErrorLog("ShopWebServiceENG.SiebelUpdateToRegister", "ShopWebServiceENG.SiebelUpdateToRegister : " & ret.ErrorMessage)
            Else
                ret.ACTIVITY_ID = SiebelActivityID
                'shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Private Function UpdateSiebelDesc(ByVal SiebelDesc As String, ByVal SiebelStatus As String, ByVal SiebelActivityID As String, ByVal MobileNo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As CenParaDB.Appointment.SiebelResponsePara
            Dim re As Boolean = False
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim InputPara As String = ""
            InputPara += "SIEBEL_ACTID=" & SiebelActivityID
            InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
            InputPara += "&SIEBEL_STATUS=" & SiebelStatus

            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            ret = CallWebServiceUpdate(InputPara)
            If ret.RESULT = True Then
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

        Private Function CallWebServiceUpdate(ByVal InputPara As String) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_update_url1") & InputPara
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SIEBEL_URL1)
            ret = GetSiebelMsg(webRequest, webresponse, InputPara, "UPDATE")

            If ret.RESULT = False Then
                FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebServiceUpdate", "ShopWebServiceENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL1)
                Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_update_url2") & InputPara
                webRequest = webRequest.Create(SIEBEL_URL2)
                ret = GetSiebelMsg(webRequest, webresponse, InputPara, "UPDATE")

                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebServiceUpdate", "ShopWebServiceENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL2)
                End If
            End If

            Return ret
        End Function
#End Region
    End Class
End Namespace
