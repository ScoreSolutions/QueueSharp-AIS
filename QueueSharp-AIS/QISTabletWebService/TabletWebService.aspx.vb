Imports Engine.Common
Imports Newtonsoft.Json
Imports Engine.CallWebService.TabletWebServiceENG

Partial Public Class TabletWebService
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request("MethodName") IsNot Nothing Then
                Select Case Request("MethodName")
                    Case "CounterList"
                        Response.Write(GetTabletCounterList())

                    Case "LoginCounterRefreshSec"
                        Response.Write(GetLoginCounterRefreshSec())

                    Case "Login"
                        Dim UserName As String = Request("Username")
                        Dim PassWD As String = Request("PassWD")
                        Dim CounterID As String = Request("CounterID")
                        Response.Write(JsonConvert.SerializeObject(TabletLogin(UserName, PassWD, CounterID, Request.UserHostAddress), Formatting.Indented))

                    Case "GetReasonList"
                        Dim ReasonType As String = Request("reason_type")
                        Response.Write(GetReasonList(ReasonType))

                    Case "HoldCounter"
                        Dim ReasonID As String = Request("ReasonID")
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Response.Write(HoldCounter(CounterID, UserID, ReasonID))

                    Case "UnholdCounter"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Response.Write(UnholdCounter(CounterID, UserID))

                    Case "Logout"
                        Dim ReasonID As String = Request("ReasonID")
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim ItemID As String = Request("ItemID")
                        Response.Write(TabletLogout(ReasonID, UserID, CounterID, ItemID, "2", Request.UserHostAddress))

                    Case "ShowItemList"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim AssignAppointment As String = Request("AssignAppointment")
                        Response.Write(JsonConvert.SerializeObject(ShowItemList(UserID, CounterID, AssignAppointment), Formatting.Indented))

                    Case "UpdateUserServiceItem"
                        Dim UserID As String = Request("UserID")
                        Dim UserItemID As String = Request("UserItemID")
                        Response.Write(UpdateUserServiceItem(UserItemID, UserID))

                    Case "ShowCustomerWait"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim UserItemID As String = Request("UserItemID")
                        Dim AssignAppointment As String = Request("AssignAppointment")
                        Response.Write(JsonConvert.SerializeObject(ShowCustomerWait(UserID, CounterID, UserItemID, AssignAppointment), Formatting.Indented))

                    Case "GetQueueRefreshSec"
                        Response.Write(GetQueueRefreshSec())

                    Case "CallNextQueue"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim AssignAppointment As String = Request("AssignAppointment")
                        Response.Write(JsonConvert.SerializeObject(CallNextQueue(UserID, CounterID, AssignAppointment), Formatting.Indented))

                    Case "CheckTimeForce"
                        Response.Write(CheckTimeForce())

                    Case "CallAutoForce"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim AssignAppointment As String = Request("AssignAppointment")
                        Response.Write(JsonConvert.SerializeObject(CallAutoForce(UserID, CounterID, AssignAppointment), Formatting.Indented))

                    Case "CallPickupQueue"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim AssignAppointment As String = Request("AssignAppointment")
                        Dim QueueNo As String = Request("QueueNo")
                        Response.Write(JsonConvert.SerializeObject(CallPickupQueue(UserID, CounterID, AssignAppointment, QueueNo), Formatting.Indented))

                    Case "CallQuickService"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim AssignAppointment As String = Request("AssignAppointment")
                        Dim QueueNo As String = Request("QueueNo")
                        Response.Write(JsonConvert.SerializeObject(CallQuickService(UserID, CounterID, AssignAppointment, QueueNo), Formatting.Indented))


                    Case "CloseCallingQueue"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Dim TimeHold As String = Request("TimeHold")
                        Response.Write(CloseCallingQueue(UserID, CounterID, QueueNo, MobileNo, TimeHold))

                    Case "MissedCall"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Response.Write(MissedCallQueue(QueueNo, MobileNo, UserID, CounterID))

                    Case "ConfirmQueue"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Response.Write(JsonConvert.SerializeObject(ConfirmQueue(UserID, CounterID, QueueNo, MobileNo)))

                    Case "EndService"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Dim EndItemID As String = Request("EndItemID")
                        Dim IsFirstItem As String = Request("IsFirstItem")
                        Response.Write(EndService(UserID, CounterID, QueueNo, MobileNo, EndItemID, IsFirstItem))

                    Case "EndQueueLastService"
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Response.Write(EndQueueLastService(CounterID, QueueNo, MobileNo))

                    Case "TransferQueue"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Response.Write(TransferQueue(UserID, CounterID, MobileNo, QueueNo))

                    Case "GetServiceCancel"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")

                        Response.Write(JsonConvert.SerializeObject(GetServiceCancel(UserID, CounterID, QueueNo, MobileNo)))
                    Case "CancelService"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Dim ListItemID As String = Request("ListItemID")
                        Dim Remarks As String = Request("Remarks")
                        Response.Write(CancelService(UserID, CounterID, MobileNo, QueueNo, ListItemID, Remarks))

                    Case "GetServiceAdd"
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Response.Write(JsonConvert.SerializeObject(GetServiceAdd(QueueNo, MobileNo)))

                    Case "AddService"
                        Dim UserID As String = Request("UserID")
                        Dim CounterID As String = Request("CounterID")
                        Dim QueueNo As String = Request("QueueNo")
                        Dim MobileNo As String = Request("MobileNo")
                        Dim AddItemID As String = Request("AddItemID")
                        Response.Write(JsonConvert.SerializeObject(AddServiceItem(UserID, CounterID, QueueNo, MobileNo, AddItemID)))

                    Case "GetDateNow"
                        Response.Write(GetDateNow())
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Function UnholdCounter(ByVal CounterID As String, ByVal UserID As String) As String
    '    Dim ret As String = ""
    '    Try
    '        ret = Engine.CallWebService.TabletWebServiceENG.
    '    Catch ex As Exception
    '        ret = "false|" & ex.Message
    '    End Try
    '    Return ret
    'End Function

    'Private Function HoldCounter(ByVal CounterID As String, ByVal UserID As String, ByVal ReasonID As String) As String
    '    Dim ret As String = ""
    '    Try
    '        ret = Engine.CallWebService.TabletWebServiceENG.HoldCounter(CounterID, UserID, ReasonID)
    '    Catch ex As Exception
    '        ret = "false|" & ex.Message
    '    End Try
    '    Return ret
    'End Function

    'Private Function Logout(ByVal ReasonID As String, ByVal UserID As String, ByVal CounterID As String, ByVal ItemID As String) As String
    '    Dim ret As String = ""
    '    Try
    '        ret = Engine.CallWebService.TabletWebServiceENG.TabletLogout(ReasonID, UserID, CounterID, ItemID, "2", Request.UserHostAddress)
    '    Catch ex As Exception
    '        ret = "false|" & ex.Message
    '    End Try
    '    Return ret
    'End Function
    'Private Function Login(ByVal Username As String, ByVal Passwd As String, ByVal CounterID As String) As String
    '    Dim ret As New ShParaDb.Common.TabletLogonUserPara
    '    Try
    '        ret = TabletLogin(Username, Passwd, CounterID, Request.UserHostAddress)
    '    Catch ex As Exception
    '        ret.RETURN_RESULT = "false|" & ex.Message
    '    End Try
    '    Return JsonConvert.SerializeObject(ret, Formatting.Indented)
    'End Function


    Private Function GetReasonList(ByVal ReasonType As String) As String
        Dim ret As String = ""
        Try
            Dim dt As DataTable = Engine.CallWebService.TabletWebServiceENG.GetReasonList(ReasonType)
            If dt.Rows.Count > 0 Then
                ret = JsonConvert.SerializeObject(dt, Formatting.Indented)
            End If
        Catch ex As Exception
            ret = ""
        End Try

        Return ret
    End Function





    Private Function GetLoginCounterRefreshSec() As String
        Dim ret As String = ""
        Try
            ret = Engine.Common.FunctionEng.GetConfig("LoginCounterRefreshSec")
            If ret = "" Then
                ret = "60"
            End If
        Catch ex As Exception
            ret = "1"
        End Try
        Return ret
    End Function

    Private Function GetTabletCounterList() As String
        Dim ret As String = ""
        Try
            Dim Sql As String = "select id,counter_name  "
            Sql += " from TB_counter "
            Sql += " where active_status = 1  and id not in (select distinct isnull(counter_id,0) from tb_user) "
            Sql += " and counter_tablet = 1"
            Sql += " order by counter_code"
            Dim dt As DataTable = FunctionEng.GetShopDataTable(Sql)

            If dt.Rows.Count > 0 Then
                ret = JsonConvert.SerializeObject(dt, Formatting.Indented)
            End If
            dt.Dispose()
        Catch ex As Exception
            ret = ""
        End Try
        Return ret
    End Function



End Class