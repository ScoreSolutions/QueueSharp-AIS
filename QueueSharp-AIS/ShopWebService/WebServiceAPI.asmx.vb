Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Engine.CallWebService
Imports Engine.Common
Imports ShParaDb.TABLE
Imports ShParaDb.ShopWebService
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebServiceAPI
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetCustomerProfile(ByVal MobileNo As String) As TbCustomerShParaDB
        Dim StartTime As DateTime = DateTime.Now
        Dim ret As New TbCustomerShParaDB
        Dim eng As New Engine.CallWebService.ShopWebServiceENG
        ret = eng.GetCustomerProfileFormGW(MobileNo)
        If ret.RESPONSE_MSG <> "" Then
            ret = eng.GetCustomerProfileFromDC(MobileNo)
        End If
        eng = Nothing
        ret.OTHER_INFO += "#Request Time : " & FunctionEng.GetDiffMillisec(StartTime) & "ms" '(DateTime.Now.Millisecond - StartTime.Millisecond).ToString

        Return ret
    End Function

    <WebMethod()> _
    Public Function GetArBalance(ByVal MobileNo As String) As TbCustomerShParaDB
        Dim ret As New TbCustomerShParaDB
        Dim eng As New Engine.CallWebService.ShopWebServiceENG
        ret = eng.GetArBalance(MobileNo)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function GetCampaign(ByVal MobileNo As String, ByVal AssetID As String, ByVal NetworkType As String) As TbCustomerShParaDB
        Dim ret As New TbCustomerShParaDB
        Dim eng As New Engine.CallWebService.ShopWebServiceENG
        Dim SessionID As String = MobileNo & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        ret = eng.GetCampaignExecbatch(MobileNo, SessionID, AssetID, NetworkType)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function LDAPAuth(ByVal UserName As String, ByVal Pwd As String) As LDAPResponsePara
        Dim ret As New LDAPResponsePara
        Dim eng As New ShopWebServiceENG
        ret = eng.LDAPAuth(UserName, Pwd)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function ShopSendSMS(ByVal MobileNo As String, ByVal ServiceID As String, ByVal AppointmentTime As String) As SMSResponsePara
        Dim eng As New ShopWebServiceENG
        Dim yy As Integer = AppointmentTime.Substring(0, 4) - 543
        Dim Mo As Integer = AppointmentTime.Substring(5, 2)
        Dim dd As Integer = AppointmentTime.Substring(8, 2)
        Dim HH As Integer = AppointmentTime.Substring(11, 2)
        Dim mi As Integer = AppointmentTime.Substring(14, 2)
        Dim ret As New SMSResponsePara
        ret = eng.ShopSendSMS(MobileNo, ServiceID, New DateTime(yy, Mo, dd, HH, mi, 0))
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function CheckActiveAppointment(ByVal MobileNo As String) As Boolean
        Dim ret As Boolean = False
        Dim eng As New ShopWebServiceENG
        ret = eng.CheckActiveAppointment(MobileNo)
        eng = Nothing

        Return ret
    End Function

    <WebMethod()> _
    Public Function DeleteSMSCancelAppointment(ByVal MobileNo As String, ByVal StartSlot As String) As Boolean
        Dim eng As New ShopWebServiceENG
        Dim ret As Boolean = eng.DeleteSMSCancelAppointment(MobileNo, StartSlot)
        eng = Nothing

        Return ret
    End Function

    <WebMethod()> _
    Public Function CheckBlackList(ByVal MobileNo As String) As CenParaDB.Appointment.AppointmentCheckBacklistResultPara
        Dim eng As New ShopWebServiceENG
        Dim ret As New CenParaDB.Appointment.AppointmentCheckBacklistResultPara
        ret = eng.CheckBlackList(MobileNo)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function SiebelCreateActivityConfirm(ByVal MobileNo As String) As ShParaDb.ShopWebService.SiebelResponsePara
        Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara
        Dim eng As New ShopWebServiceENG
        ret = eng.SiebelCreateActivityConfirm(MobileNo)
        eng = Nothing

        Return ret
    End Function

    <WebMethod()> _
    Public Function SiebelCreateActivity(ByVal MobileNo As String, ByVal StartSlot As String, ByVal ItemName As String, ByVal AppointmentChannel As String) As ShParaDb.ShopWebService.SiebelResponsePara
        Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara

        Dim yy As Integer = StartSlot.Substring(0, 4) - 543
        Dim Mo As Integer = StartSlot.Substring(5, 2)
        Dim dd As Integer = StartSlot.Substring(8, 2)
        Dim HH As Integer = StartSlot.Substring(11, 2)
        Dim mi As Integer = StartSlot.Substring(14, 2)
        Dim eng As New ShopWebServiceENG

        ret = eng.SiebelCreateActivity(MobileNo, New DateTime(yy, Mo, dd, HH, mi, 0), ItemName, AppointmentChannel)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function SiebelUpdateToRegister(ByVal MobileNo As String, ByVal StartSlot As String, ByVal ServiceDate As String, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As ShParaDb.ShopWebService.SiebelResponsePara
        Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara
        Dim eng As New ShopWebServiceENG

        Dim Slyy As Integer = StartSlot.Substring(0, 4) - 543
        Dim SlMo As Integer = StartSlot.Substring(5, 2)
        Dim Sldd As Integer = StartSlot.Substring(8, 2)
        Dim SlHH As Integer = StartSlot.Substring(11, 2)
        Dim Slmi As Integer = StartSlot.Substring(14, 2)

        Dim Seyy As Integer = ServiceDate.Substring(0, 4) - 543
        Dim SeMo As Integer = ServiceDate.Substring(5, 2)
        Dim Sedd As Integer = ServiceDate.Substring(8, 2)
        Dim SeHH As Integer = ServiceDate.Substring(11, 2)
        Dim Semi As Integer = ServiceDate.Substring(14, 2)

        ret = eng.SiebelUpdateToRegister(MobileNo, New DateTime(Slyy, SlMo, Sldd, SlHH, Slmi, 0), New DateTime(Seyy, SeMo, Sedd, SeHH, Semi, 0), SiebelActivityID, SiebelDesc)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function SiebelUpdateToComplete(ByVal MobileNo As String, ByVal StartSlot As String, ByVal EndDate As String, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As ShParaDb.ShopWebService.SiebelResponsePara
        Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara
        Dim eng As New ShopWebServiceENG

        Dim Slyy As Integer = StartSlot.Substring(0, 4) - 543
        Dim SlMo As Integer = StartSlot.Substring(5, 2)
        Dim Sldd As Integer = StartSlot.Substring(8, 2)
        Dim SlHH As Integer = StartSlot.Substring(11, 2)
        Dim Slmi As Integer = StartSlot.Substring(14, 2)

        Dim Eyy As Integer = EndDate.Substring(0, 4) - 543
        Dim EMo As Integer = EndDate.Substring(5, 2)
        Dim Edd As Integer = EndDate.Substring(8, 2)
        Dim EHH As Integer = EndDate.Substring(11, 2)
        Dim Emi As Integer = EndDate.Substring(14, 2)

        ret = eng.SiebelUpdateToComplete(MobileNo, New DateTime(Slyy, SlMo, Sldd, SlHH, Slmi, 0), New DateTime(Eyy, EMo, Edd, EHH, Emi, 0), SiebelActivityID, SiebelDesc)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function SiebelUpdateToMissed(ByVal MobileNo As String, ByVal StartSlot As String, ByVal MissedDate As String, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As ShParaDb.ShopWebService.SiebelResponsePara
        Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara
        Dim eng As New ShopWebServiceENG

        Dim Slyy As Integer = StartSlot.Substring(0, 4) - 543
        Dim SlMo As Integer = StartSlot.Substring(5, 2)
        Dim Sldd As Integer = StartSlot.Substring(8, 2)
        Dim SlHH As Integer = StartSlot.Substring(11, 2)
        Dim Slmi As Integer = StartSlot.Substring(14, 2)

        Dim Myy As Integer = MissedDate.Substring(0, 4) - 543
        Dim MMo As Integer = MissedDate.Substring(5, 2)
        Dim Mdd As Integer = MissedDate.Substring(8, 2)
        Dim MHH As Integer = MissedDate.Substring(11, 2)
        Dim Mmi As Integer = MissedDate.Substring(14, 2)

        ret = eng.SiebelUpdateToMissed(MobileNo, New DateTime(Slyy, SlMo, Sldd, SlHH, Slmi, 0), New DateTime(Myy, MMo, Mdd, MHH, Mmi, 0), SiebelActivityID, SiebelDesc)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function SiebelUpdateToCancel(ByVal MobileNo As String, ByVal StartSlot As String, ByVal CancelDate As String, ByVal SiebelActivityID As String, ByVal SiebelDesc As String) As ShParaDb.ShopWebService.SiebelResponsePara
        Dim ret As New ShParaDb.ShopWebService.SiebelResponsePara
        Dim eng As New ShopWebServiceENG

        Dim Slyy As Integer = StartSlot.Substring(0, 4) - 543
        Dim SlMo As Integer = StartSlot.Substring(5, 2)
        Dim Sldd As Integer = StartSlot.Substring(8, 2)
        Dim SlHH As Integer = StartSlot.Substring(11, 2)
        Dim Slmi As Integer = StartSlot.Substring(14, 2)

        Dim Cyy As Integer = CancelDate.Substring(0, 4) - 543
        Dim CMo As Integer = CancelDate.Substring(5, 2)
        Dim Cdd As Integer = CancelDate.Substring(8, 2)
        Dim CHH As Integer = CancelDate.Substring(11, 2)
        Dim Cmi As Integer = CancelDate.Substring(14, 2)

        ret = eng.SiebelUpdateToCancel(MobileNo, New DateTime(Slyy, SlMo, Sldd, SlHH, Slmi, 0), New DateTime(Cyy, CMo, Cdd, CHH, Cmi, 0), SiebelActivityID, SiebelDesc)
        eng = Nothing
        Return ret
    End Function
    <WebMethod()> _
    Public Function SendSMS(ByVal Msg As String, ByVal MobileNo As String) As SMSResponsePara
        Dim eng As New ShopWebServiceENG

        Dim ret As New SMSResponsePara
        ret = eng.SendSMS(MobileNo, Msg)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function UpdateAppointmentJobStatus(ByVal AppointJobID As Long, ByVal ActiveStatus As String) As CenParaDB.Appointment.SaveAppointmentJobPara
        Dim ret As New CenParaDB.Appointment.SaveAppointmentJobPara
        Dim eng As New ShopWebServiceENG
        ret = eng.UpdateAppointmentJobStatus(AppointJobID, ActiveStatus)
        eng = Nothing
        Return ret
    End Function

#Region "Master WebService"
    <WebMethod()> _
    Public Function GetMasterItemList() As DataTable
        Return DcMasterENG.GetMasterItemList("active_status='1'", "item_order")
    End Function

    <WebMethod()> _
    Public Function GetMasterItemByID(ByVal MasterID As String) As CenParaDB.TABLE.TbItemCenParaDB
        Return DcMasterENG.GetMasterItemByID(MasterID)
    End Function

    <WebMethod()> _
    Public Function GetMasterCustomerTypeList() As DataTable
        Return DcMasterENG.GetMasterCustomerTypeList("active_status='1'", "priority_value")
    End Function

    <WebMethod()> _
    Public Function GetMasterCustomerTypeByID(ByVal MasterID As String) As CenParaDB.TABLE.TbCustomertypeCenParaDB
        Return DcMasterENG.GetMasterCustomerTypeByID(MasterID)
    End Function

    <WebMethod()> _
    Public Function GetMasterGroupUserList() As DataTable
        Return DcMasterENG.GetMasterGroupUserList("active_status='1'", "group_name")
    End Function

    <WebMethod()> _
    Public Function GetMasterGroupUserByID(ByVal MasterID As String) As CenParaDB.TABLE.TbGroupuserCenParaDB
        Return DcMasterENG.GetMasterGroupUserByID(MasterID)
    End Function

    <WebMethod()> _
    Public Function GetMasterSkillList() As DataTable
        Return DcMasterENG.GetMasterSkillList("active_status='1'", "skill")
    End Function

    <WebMethod()> _
    Public Function GetMasterSkillByID(ByVal MasterID As String) As CenParaDB.TABLE.TbSkillCenParaDB
        Return DcMasterENG.GetMasterSkillByID(MasterID)
    End Function

    <WebMethod()> _
    Public Function GetMasterSkillItemList(ByVal MasterID As String) As DataTable
        Return DcMasterENG.GetMasterSkillItemList(MasterID)
    End Function

    <WebMethod()> _
    Public Function GetMasterHoldReasonList() As DataTable
        Return DcMasterENG.GetMasterHoldReasonList("active_status='1'", "name")
    End Function

    <WebMethod()> _
    Public Function GetMasterHoldReasonByID(ByVal MasterID As String) As CenParaDB.TABLE.TbHoldReasonCenParaDB
        Return DcMasterENG.GetMasterHoldReasonByID(MasterID)
    End Function

    <WebMethod()> _
    Public Function GetMasterLogoutReasonList() As DataTable
        Return DcMasterENG.GetMasterLogoutReasonList("active_status='1'", "name")
    End Function

    <WebMethod()> _
    Public Function GetMasterLogoutReasonByID(ByVal MasterID As String) As CenParaDB.TABLE.TbLogoutReasonCenParaDB
        Return DcMasterENG.GetMasterLogoutReasonByID(MasterID)
    End Function

    <WebMethod()> _
    Public Function LoadImageFromWebConfig(ByVal EventDate As String, ByVal TargetType As String) As DataTable
        Dim ShopAbb As String = Engine.Common.FunctionEng.GetConfig("ShopAbbCode")
        Dim dt As New DataTable
        Try
            Dim ws As New EquipmentWS.EquipmentFileService
            ws.Url = Engine.Common.FunctionEng.GetConfig("EquipmentURL1")
            ws.Timeout = 2400000
            dt = ws.GetFileFromDC(EventDate, ShopAbb, TargetType)
            ws = Nothing
        Catch ex As Exception
            dt = New DataTable
        End Try
        dt.TableName = "GetFileFromDC"

        Return dt
    End Function

    <WebMethod()> _
    Public Function UpdateTransferStatus(ByVal FileScheduleID As String) As Boolean
        Dim ws As New EquipmentWS.EquipmentFileService
        ws.Url = Engine.Common.FunctionEng.GetConfig("EquipmentURL1")
        Dim ret As Boolean = ws.UpdateTransferStatus(FileScheduleID)
        If ret = False Then
            Engine.Common.FunctionEng.SaveShopErrorLog("WebServiceAPI.UpdateTransferStatus", ws.ErrorMessage)
        End If
        ws = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Function GetAppointmentPolicy() As String
        Return DcMasterENG.GetAppointmentPolicy()
    End Function

#End Region


#Region "Image Capture File"
    <WebMethod()> _
    Public Function SendCaptureImageFile(ByVal FileByte() As Byte, ByVal MobileNo As String, ByVal QueueNo As String, ByVal AssignTime As DateTime, ByVal CaptureTime As DateTime) As Long
        Dim ret As Long = 0
        Try
            Dim eng As New ShopWebServiceENG
            ret = eng.SendImageFile(FileByte, MobileNo, QueueNo, AssignTime, CaptureTime)
            eng = Nothing
        Catch ex As Exception
            ret = 0
        End Try
        Return ret
    End Function


    <WebMethod()> _
    Public Function LoadCaptureImageFile(ByVal MobileNo As String) As CaptureImageResponsePara
        Dim eng As New ShopWebServiceENG
        Dim ret As New CaptureImageResponsePara
        ret = eng.LoadImageFile(MobileNo)
        eng = Nothing
        Return ret
    End Function

    <WebMethod()> _
    Public Sub UpdateCustCaptureIndexFile()
        Dim eng As New ShopWebServiceENG
        eng.UpdateCustCaptureIndexFile()
        eng = Nothing
    End Sub
#End Region

#Region "Fail Over DB Log"
    <WebMethod()> _
    Public Sub CreateFailOverDBLog(ByVal ClinetIP As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal ActiveDB As String, ByVal SQL As String)
        Dim eng As New ShopWebServiceENG
        eng.CreateFailOverDBLog(ClinetIP, SoftwareName, FunctionName, ActiveDB, SQL)
        eng = Nothing
    End Sub
#End Region

End Class