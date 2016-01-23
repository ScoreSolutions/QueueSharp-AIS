Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Engine.Utilities

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CustomerProfile
    Inherits System.Web.Services.WebService

    '<WebMethod()> _
    'Public Function GetStartupPath() As String
    '    Return SqlDB.GetStartupPath
    'End Function

    <WebMethod()> _
    Public Function TestConnectDB(ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserID As String, ByVal Password As String) As Boolean
        Dim trans As New TransactionDB(False)
        Dim sql As String = "Data Source=" & ServerName & ";Initial Catalog=" & DatabaseName & ";User ID=" & UserID & ";Password=" & Password
        Dim ret As Boolean = trans.CreateTransaction(sql)
        If ret = True Then
            trans.CommitTransaction()
        Else
            trans.RollbackTransaction()
        End If

        Return ret
    End Function


    <WebMethod()> _
    Public Function ImportCustomerProfile(ByVal MobileNo As String, ByVal TitleName As String, ByVal FirstName As String, ByVal LastName As String, ByVal EMail As String, ByVal PreferLang As String, ByVal Segment As String, ByVal BirthDate As String, ByVal MobileStatus As String, ByVal Category As String, ByVal AccBalance As String, ByVal ContactClass As String, ByVal ServiceYear As String, ByVal ChumScore As String, ByVal CamCode As String, ByVal CamName As String, ByVal NetworkType As String) As ImportCustomerResponsePara
        Dim res As New ImportCustomerResponsePara
        
        Dim ret As Boolean = False
        Dim trans As New TransactionDB

        Dim uSql As String = "UPDATE TB_CUSTOMER SET "
        uSql += " UPDATE_BY = 'DailyUpdate', UPDATE_DATE = getdate(), "
        uSql += " TITLE_NAME = '" & TitleName & "', "
        uSql += " F_NAME = '" & FirstName & "', L_NAME = '" & LastName & "', EMAIL = '" & EMail & "', "
        uSql += " PREFER_LANG = '" & PreferLang & "', SEGMENT_LEVEL = '" & Segment & "', BIRTH_DATE = '" & BirthDate & "', MOBILE_STATUS = '" & MobileStatus & "', "
        uSql += " CATEGORY = '" & Category & "', ACCOUNT_BALANCE = '" & AccBalance & "', CONTACT_CLASS = '" & ContactClass & "', SERVICE_YEAR = '" & ServiceYear & "', "
        uSql += " CHUM_SCORE = '" & ChumScore & "', NETWORK_TYPE = '" & NetworkType & "'"
        uSql += " Where MOBILE_NO = '" & MobileNo & "' "


        Dim lnq As New TbCustomerCenLinqDB
        ret = lnq.UpdateBySql(uSql, trans.Trans)
        If ret = False Then
            lnq.MOBILE_NO = MobileNo
            lnq.TITLE_NAME = TitleName
            lnq.F_NAME = FirstName
            lnq.L_NAME = LastName
            lnq.EMAIL = EMail
            lnq.PREFER_LANG = PreferLang
            lnq.SEGMENT_LEVEL = Segment
            lnq.BIRTH_DATE = BirthDate
            lnq.MOBILE_STATUS = MobileStatus
            lnq.CATEGORY = Category
            lnq.ACCOUNT_BALANCE = AccBalance
            lnq.CONTACT_CLASS = ContactClass
            lnq.SERVICE_YEAR = ServiceYear
            lnq.CHUM_SCORE = ChumScore
            lnq.NETWORK_TYPE = NetworkType
            ret = lnq.InsertData("DailyInsert", trans.Trans)
        End If

        If ret = True Then
            trans.CommitTransaction()
            res.RESPONSE = True
        Else
            trans.RollbackTransaction()
            res.RESPONSE = False
            res.ErrorMessage = lnq.ErrorMessage
        End If
        Return res
    End Function

    <WebMethod()> _
    Public Function ImportCampaign(ByVal MobileNo As String, ByVal CamCode As String, ByVal CamName As String, ByVal CamNameEN As String, ByVal CamDesc As String) As ImportCustomerResponsePara
        Dim res As New ImportCustomerResponsePara

        Dim ret As Boolean = False
        Dim trans As New TransactionDB

        Dim uSql As String = "UPDATE TB_CUSTOMER SET "
        uSql += " UPDATE_BY = 'DailyUpdate', UPDATE_DATE = getdate(), "
        uSql += " CAMPAIGN_CODE = '" & CamCode & "', CAMPAIGN_NAME = '" & CamName & "', "
        uSql += " CAMPAIGN_NAME_EN = '" & CamNameEN & "', CAMPAIGN_DESC = '" & CamDesc & "' "
        uSql += " Where MOBILE_NO = '" & MobileNo & "' "

        Dim lnq As New TbCustomerCenLinqDB
        ret = lnq.UpdateBySql(uSql, trans.Trans)
        If ret = False Then
            trans.RollbackTransaction()
            res.RESPONSE = False
            res.ErrorMessage = lnq.ErrorMessage
        Else
            trans.CommitTransaction()
            res.RESPONSE = False
        End If

        Return res
    End Function
End Class