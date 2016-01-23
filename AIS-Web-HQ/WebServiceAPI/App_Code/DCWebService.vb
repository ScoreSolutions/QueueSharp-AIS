Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Engine.GeteWay
Imports CenParaDB.TABLE
Imports CenParaDB.GateWay
Imports System.Collections.Specialized

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DCWebService
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetCustomerProfile(ByVal MobileNo As String) As TbCustomerCenParaDB
        Dim ret As New TbCustomerCenParaDB
        Dim eng As New GateWayServiceENG
        Dim SessionID As String = MobileNo & DateTime.Now.ToString("yyyyMMddHHmmss")
        ret = eng.GetCustomerProfile(MobileNo, SessionID)

        Return ret
    End Function

    '<WebMethod()> _
    'Public Function GetCampaignTest(ByVal MobileNo As String, ByVal AudienceIdValue As String) As TbCustomerCenParaDB
    '    Dim eng As New GateWayServiceENG
    '    Dim SessionID As String = MobileNo & DateTime.Now.ToString("yyyyMMddHHmmss")
    '    'Dim AudienceIdValue As String = "1-62139"
    '    Return eng.GetCampaignTest(AudienceIdValue, MobileNo, SessionID)
    'End Function

    <WebMethod()> _
    Public Function LDAPAuth(ByVal UserName As String, ByVal Pwd As String) As LDAPResponsePara
        Dim ret As New LDAPResponsePara
        Dim eng As New GateWayServiceENG
        ret = eng.LDAPAuth(UserName, Pwd)
        Return ret
    End Function

    <WebMethod()> _
    Public Function SendEMail(ByVal MailTo As String, ByVal Subject As String, ByVal MailMsg As String) As Boolean
        Dim eng As New GateWayServiceENG
        Return eng.SendEmail(MailTo, Subject, MailMsg)
    End Function

    <WebMethod()> _
    Public Function SendEMailAttFile(ByVal MailTo As String, ByVal Subject As String, ByVal MailMsg As String) As Boolean
        'http://stackoverflow.com/questions/566462/upload-files-with-httpwebrequest-multipart-form-data
        'http://stackoverflow.com/questions/8222092/sending-http-post-with-system-net-webclient

        Dim file() As String = {}
        MailMsg += "Image 1 : <IMG SRC='cid:ATTIMG1'  /> <br />"
        MailMsg += "Image 1 : <IMG SRC='cid:ATTIMG2'  /> <br />"
        Dim ret As Boolean = False
        Dim eng As New GateWayServiceENG
        ret = eng.SendEmailAttFile(MailTo, Subject, MailMsg, file)
        eng = Nothing
        Return ret

    End Function

    <WebMethod()> _
    Public Function SendSMS(ByVal MobileNo As String, ByVal Msg As String) As SMSResponsePara
        Dim eng As New GateWayServiceENG
        Dim p As New SMSResponsePara
        p = eng.SendSMS(MobileNo, Msg)
        eng = Nothing
        Return p
    End Function

    <WebMethod()> _
    Public Function CheckBlackList(ByVal MobileNo As String) As Boolean

    End Function

    <WebMethod()> _
    Public Function SendATSR(ByVal MobileNo As String, ByVal QueueID As String, ByVal ShopCode As String, ByVal TemplateID As String) As Boolean
        'QueueID Format = QueueNo + ShopAbb+yyyyMMddHHmmss  EX. A104PK25550326172423
        Dim eng As New GateWayServiceENG
        Dim ret As Boolean = eng.SendATSR(MobileNo, QueueID, ShopCode, TemplateID)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function GetATSRResult(ByVal QUNIQID As String) As ATSRResultPara
        Dim eng As New GateWayServiceENG
        Return eng.GetATSRSurveyResult(QUNIQID)
    End Function

    <WebMethod()> _
    Public Function SendEmailConfirm(ByVal MailTo As String, ByVal ServiceID As String, ByVal MobileNo As String, ByVal AppointmentTime As DateTime, ByVal PreLang As String, ByVal ShopID As String) As Boolean
        Dim eng As New Engine.Appointment.AppointmentENG
        Dim ret As Boolean = eng.SendEmailConfirm(MailTo, ServiceID, MobileNo, AppointmentTime, PreLang, ShopID)
        eng = Nothing
        Return ret
    End Function
    

End Class
