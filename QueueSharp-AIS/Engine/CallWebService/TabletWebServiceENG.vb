Imports System.Data.SqlClient
Imports ShLinqDB.Common.Utilities
Imports ShLinqDB.TABLE
Imports ShParaDb.TABLE
Imports Engine.Common.FunctionEng
Imports ShParaDb.TabletWebService


Namespace CallWebService
    Public Class TabletWebServiceENG
        Dim _err As String = ""
        Private Shared INIFileName As String = "C:\Windows\QueueSharp.ini"  'Parth ที่ใช้เก็บไฟล์ .ini สำหรับการ Connect Database ที่ Shop

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

#Region "Login"
        Public Shared Function TabletLogin(ByVal Username As String, ByVal Passwd As String, ByVal CounterID As String, ByVal IPAddress As String) As TabletLogonUserPara
            Dim ret As New TabletLogonUserPara
            Try
                ret = ValidationLogin(Username, Passwd, CounterID)
                If ret.RETURN_RESULT = "true" Then
                    Dim sql = ""
                    Dim dt As New DataTable
                    'Encrypt
                    Dim PW As String = Engine.Common.FunctionEng.EnCripPwd(Passwd.Trim)
                    '*****************************

                    sql = "select x.id,title_name + ' ' + fname + ' ' + lname as fullname,user_code,group_id from TB_USER x left join TB_TITLE y on x.title_id = y.id where username = '" & Username.Trim & "' and [password] = '" & PW.Trim & "' and active_status = 1"
                    dt = GetShopDataTable(sql)
                    If dt.Rows.Count > 0 Then
                        Dim dt_tmp As New DataTable
                        sql = "select * from TB_GROUPUSER where active_status = 1 and id = " & dt.Rows(0).Item("group_id").ToString
                        dt_tmp = GetShopDataTable(sql)
                        If dt_tmp.Rows.Count = 0 Then
                            ret.RETURN_RESULT = "false|" & "Group User can't active"
                            Return ret
                        End If
                        dt_tmp.Dispose()


                        Dim trans As New TransactionDB
                        Dim uLnq As New TbUserShLinqDB

                        sql = "update TB_USER "
                        sql += " set check_update = GETDATE(),counter_id = '" & CounterID & "' "
                        sql += " where id = '" & dt.Rows(0).Item("id").ToString & "' and (counter_id=0 or counter_id is null)"
                        If uLnq.UpdateBySql(sql, trans.Trans) = True Then
                            Dim cDt = New DataTable
                            sql = "select counter_id from tb_user "
                            sql += " where counter_id = '" & CounterID & "'"
                            sql += " and id<>'" & dt.Rows(0).Item("id").ToString & "'"
                            cDt = GetShopDataTable(sql, trans)
                            If cDt.Rows.Count > 0 Then
                                trans.RollbackTransaction()
                                ret.RETURN_RESULT = "false|Counter has been updated by the other user"
                            Else
                                ret.RETURN_RESULT = "true"
                                ret.USERNAME = Username
                                ret.USER_ID = dt.Rows(0).Item("id").ToString
                                ret.USER_CODE = dt.Rows(0).Item("user_code").ToString
                                ret.FULLNAME = dt.Rows(0).Item("fullname").ToString
                                ret.COUNTER_ID = CounterID

                                sql = "select counter_name from tb_counter where id='" & CounterID & "'"
                                dt = New DataTable
                                dt = GetShopDataTable(sql)
                                If dt.Rows.Count > 0 Then
                                    ret.COUNTER_NAME = dt.Rows(0)("counter_name")
                                End If
                                ret.IP_ADDRESS = IPAddress

                                sql = "select item_id from TB_USER_SERVICE_SCHEDULE where datediff(D,GETDATE(),service_date)=0 and user_id = '" & ret.USER_ID & "' and priority = 1 and item_id in (select item_id from TB_USER_SKILL left join TB_SKILL_ITEM on TB_USER_SKILL.skill_id = TB_SKILL_ITEM.skill_id where user_id = '" & ret.USER_ID & "')"
                                dt = New DataTable
                                dt = GetShopDataTable(sql)
                                If dt.Rows.Count > 0 Then
                                    ret.ITEM_ID = dt.Rows(0).Item("item_id").ToString
                                Else
                                    ret.ITEM_ID = 0
                                End If

                                sql = "select * from TB_APPOINTMENT_USER where DATEDIFF(D,GETDATE(),app_date )=0 and user_id = '" & ret.USER_ID & "'"
                                dt = New DataTable
                                dt = GetShopDataTable(sql)
                                If dt.Rows.Count > 0 Then
                                    ret.ASSIGN_APPOINTMENT = 1
                                Else
                                    ret.ASSIGN_APPOINTMENT = 0
                                End If
                                dt.Dispose()

                                UpdateCounterStatus(CounterID, True, trans)
                                LogLogin(ret, 1, trans)
                                ClearUnitDisplay(ret.COUNTER_ID, False, trans)

                                trans.CommitTransaction()
                            End If
                            cDt.Dispose()
                        Else
                            trans.RollbackTransaction()
                            ret.RETURN_RESULT = "false|Counter has been updated by the other user"
                        End If
                        uLnq = Nothing

                        Return ret
                    End If
                    dt.Dispose()
                End If
            Catch ex As Exception
                ret.RETURN_RESULT = "false|" & ex.Message
            End Try
            Return ret
        End Function

        'บันทึกประวัติการ Login
        Private Shared Sub LogLogin(ByVal lPara As TabletLogonUserPara, ByVal Action As Int32, ByVal trans As TransactionDB, Optional ByVal ReasonID As Int32 = 0, Optional ByVal Productive As Int32 = 0)
            If lPara.COUNTER_ID <> "" Then
                Dim sql As String = ""
                sql = "exec SP_LOGIN " & lPara.USER_ID & "," & lPara.COUNTER_ID & "," & ReasonID & "," & Productive & "," & Action & "," & lPara.ITEM_ID & ", '" & lPara.IP_ADDRESS & "', 'TABLET'"
                ExecuteShopNonQuery(sql, trans)
            End If
        End Sub

        'Update สถานะของห้องว่า มีการใช้งานหรือไม่
        Private Shared Sub UpdateCounterStatus(ByVal CounterID As Integer, ByVal Status As Boolean, ByVal trans As TransactionDB)
            If CounterID <> 0 Then
                Dim available As Int32 = 0
                If Status = True Then
                    available = 1
                End If

                Dim sql As String = ""
                sql = "update TB_counter set available = " & available & " where id = " & CounterID
                ExecuteShopNonQuery(sql, trans)
            End If
        End Sub

        Private Shared Function QueueCheckLDAP() As Boolean
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select config_name,config_value from TB_SETTING where config_name = 'q_con_ldap'"
            dt = GetShopDataTable(sql)
            If dt.Rows(0).Item("config_value").ToString = "1" Then
                Return True
            End If
            Return False
        End Function

        Private Shared Function ValidationLogin(ByVal UserName As String, ByVal PassWd As String, ByVal CounterID As String) As TabletLogonUserPara
            Dim ret As New TabletLogonUserPara
            Try
                If UserName.Trim = "" Then
                    ret.RETURN_RESULT = "false|Please enter Username"
                    Return ret
                End If

                If PassWd.Trim = "" Then
                    ret.RETURN_RESULT = "false|Please enter Password"
                    Return ret
                End If

                If CounterID = "0" Or CounterID = "" Then
                    ret.RETURN_RESULT = "false|Please select Counter"
                    Return ret
                End If

                'Encrypt
                Dim PW As String = Engine.Common.FunctionEng.EnCripPwd(PassWd)
                '*****************************
                If UserName.ToUpper <> "ADMIN" Then
                    If QueueCheckLDAP() = True Then
                        Try
                            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                              sslerror As System.Net.Security.SslPolicyErrors) True

                            Dim Sql As String = ""
                            Dim LDAP As New ShParaDb.ShopWebService.LDAPResponsePara
                            Dim ws As New ShopWebServiceENG
                            LDAP = ws.LDAPAuth(UserName, PassWd)

                            If LDAP.RESULT = True Then
                                Dim trans As New TransactionDB
                                Dim uLnq As New ShLinqDB.TABLE.TbUserShLinqDB
                                If uLnq.ChkDataByWhere("username = '" & UserName & "'", trans.Trans) = True Then
                                    uLnq.PASSWORD = PW

                                    If uLnq.UpdateByPK(UserName, trans.Trans) = True Then
                                        trans.CommitTransaction()
                                        ret.RETURN_RESULT = "true"
                                    Else
                                        trans.RollbackTransaction()
                                        ret.RETURN_RESULT = "false|" & uLnq.ErrorMessage
                                    End If
                                Else
                                    trans.RollbackTransaction()
                                    ret.RETURN_RESULT = "false|" & uLnq.ErrorMessage
                                End If
                            Else
                                If InStr(LDAP.RESPONSE_MSG, "User") > 0 Then
                                    ret.RETURN_RESULT = "false|User not found"
                                ElseIf InStr(LDAP.RESPONSE_MSG, "Password") > 0 Then
                                    ret.RETURN_RESULT = "false|Invalid password"
                                Else
                                    ret.RETURN_RESULT = "false|Your username or password is incorrect."
                                End If
                            End If
                        Catch ex As Exception
                            'LDAP_CONNECT = False
                            Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                            If lnq.ChkDataByWhere("username='" & UserName & "' and password = '" & PW & "'", Nothing) = False Then
                                ret.RETURN_RESULT = "false|" & lnq.ErrorMessage
                            End If
                        End Try
                    End If
                Else
                    Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                    If lnq.ChkDataByWhere("username='" & UserName & "' and password = '" & PW & "'", Nothing) = False Then
                        ret.RETURN_RESULT = "false|Invalid password"
                    Else
                        lnq.PASSWORD = PW

                        Dim trans As New TransactionDB
                        If lnq.UpdateByPK(lnq.ID, trans.Trans) = True Then
                            trans.CommitTransaction()
                            ret.RETURN_RESULT = "true"
                        Else
                            trans.RollbackTransaction()
                            ret.RETURN_RESULT = "false|" & lnq.ErrorMessage
                        End If
                    End If
                    lnq = Nothing
                End If

            Catch ex As Exception
                ret.RETURN_RESULT = "false|" & ex.Message
            End Try

            Return ret
        End Function
#End Region

#Region "Logout & Hold"
        Public Shared Function TabletLogout(ByVal ReasonID As String, ByVal UserID As String, ByVal CounterID As String, ByVal ItemID As String, ByVal Action As String, ByVal IPAddress As String) As String
            Dim ret As String = ""
            Try
                Dim iPara As New TabletLogonUserPara
                iPara.USER_ID = UserID
                iPara.ITEM_ID = ItemID
                iPara.COUNTER_ID = CounterID
                iPara.IP_ADDRESS = IPAddress

                Dim trans As New TransactionDB
                Dim sql As String = ""
                sql = " select productive from TB_LOGOUT_REASON where id='" & ReasonID & "'"
                Dim dt As DataTable = GetShopDataTable(sql, trans)
                If dt.Rows.Count > 0 Then
                    LogLogin(iPara, Action, trans, ReasonID, dt.Rows(0)("productive"))

                    sql = "update TB_user set counter_id = 0 ,item_id = 0, ip_address=null where id = '" & UserID & "'"
                    If ExecuteShopNonQuery(sql, trans) = True Then
                        trans.CommitTransaction()
                        ret = "true"
                    Else
                        trans.RollbackTransaction()
                        ret = "false|SQL = " & sql
                    End If
                Else
                    trans.RollbackTransaction()
                    ret = "false|Not found Logout Reason"
                End If
                dt.Dispose()
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function GetReasonList(ByVal ReasonType As Integer) As DataTable
            Dim ret As New DataTable
            Try
                Dim TB As String = ""
                Dim vType As String = ""

                Select Case ReasonType
                    Case 1
                        TB = "TB_HOLD_REASON"
                        vType = "HOLD REASON"
                    Case 2
                        TB = "TB_LOGOUT_REASON"
                        vType = "LOGOUT REASON"
                End Select

                Dim Sql As String = "select id,name,productive from " & TB & " where active_status = 1 order by name"
                ret = GetShopDataTable(Sql)
                If ret.Rows.Count > 0 Then
                    ret.Columns.Add("reason_type")
                    For i As Integer = 0 To ret.Rows.Count - 1
                        ret.Rows(i)("reason_type") = vType
                    Next
                End If
            Catch ex As Exception
                ret = New DataTable
            End Try
            Return ret
        End Function

        Public Shared Function UnholdCounter(ByVal CounterID As String, ByVal UserID As String) As String
            Dim ret As String = ""
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select available from TB_COUNTER where id = '" & CounterID & "'"
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("available") = 0 Then
                        Dim trans As New TransactionDB

                        'Action  1 = Holdroom   2 = UnHoldroom
                        sql = "exec SP_HoldRoom " & UserID & "," & CounterID & ",0,0,2"   'Unhold
                        ExecuteShopNonQuery(sql, trans)

                        'Dim iPara As New TabletLogonUserPara
                        'iPara.USER_ID = UserID
                        'iPara.COUNTER_ID = CounterID
                        ClearUnitDisplay(CounterID, False, trans)

                        sql = "update TB_user set counter_id = 0 ,item_id = 0 where id = '" & UserID & "'"
                        ExecuteShopNonQuery(sql, trans)

                        trans.CommitTransaction()
                        ret = "true"
                    Else
                        ret = "false|Counter not available"
                    End If
                Else
                    ret = "false|Counter not found"
                End If
                dt.Dispose()
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function HoldCounter(ByVal CounterID As String, ByVal UserID As String, ByVal ReasonID As String) As String
            Dim ret As String = ""
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select available from TB_COUNTER where id = '" & CounterID & "'"
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("available") = 1 Then
                        sql = " select productive from TB_HOLD_REASON where id='" & ReasonID & "'"
                        Dim pDt As DataTable = GetShopDataTable(sql)
                        If pDt.Rows.Count > 0 Then
                            If CounterID <> "0" Or CounterID <> "" Then
                                'Action  1 = Holdroom   2 = UnHoldroom
                                sql = "exec SP_HoldRoom " & UserID & "," & CounterID & "," & ReasonID & "," & pDt.Rows(0)("productive") & ",1" ' Hold
                                ExecuteShopNonQuery(sql)

                                Dim iPara As New TabletLogonUserPara
                                iPara.USER_ID = UserID
                                iPara.COUNTER_ID = CounterID

                                Dim trans As New TransactionDB
                                PauseUnitDisplay(iPara, trans)
                                trans.CommitTransaction()

                                ret = "true"
                            End If
                        End If
                        pDt.Dispose()
                    End If
                End If
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function
#End Region

#Region "Service Queue"
        Public Shared Function ShowItemList(ByVal UserID As String, ByVal CounterID As String, ByVal AssignAppointment As String) As DataTable
            Dim ret As New DataTable
            Try
                Dim sql As String = "select quickservice,speed_lane from TB_COUNTER where id = '" & CounterID & "'"
                Dim cDt As DataTable = GetShopDataTable(sql)
                If cDt.Rows.Count > 0 Then
                    If AssignAppointment = "1" And Convert.ToInt16(cDt.Rows(0)("speed_lane")) = 1 Then
                        sql = "select 0 as id,'Appointment' as item_name,0 as ord,0 as priority "
                        sql += " union all "
                        sql += " select tb_item.id,item_name,1 as ord,priority "
                        sql += " from TB_USER_SERVICE_SCHEDULE "
                        sql += " left join tb_item on TB_USER_SERVICE_SCHEDULE.item_id = tb_item.id  "
                        sql += " where datediff(D,GETDATE(),service_date)=0 "
                        sql += " and user_id = '" & UserID & "' "
                        sql += " and TB_USER_SERVICE_SCHEDULE.active_status = 1 "
                        sql += " and tb_item.id in (select item_id "
                        sql += "                    from TB_USER_SKILL "
                        sql += "                    left join TB_SKILL_ITEM on TB_USER_SKILL.skill_id = TB_SKILL_ITEM.skill_id "
                        sql += "                    where user_id = '" & UserID & "') "
                        sql += " order by ord,priority desc"
                    Else
                        sql = "select tb_item.id,item_name,1 as ord,priority  "
                        sql += " from TB_USER_SERVICE_SCHEDULE "
                        sql += " left join tb_item on TB_USER_SERVICE_SCHEDULE.item_id = tb_item.id  "
                        sql += " where datediff(D,GETDATE(),service_date)=0 "
                        sql += " and user_id = '" & UserID & "' "
                        sql += " and TB_USER_SERVICE_SCHEDULE.active_status = 1 "
                        sql += " and tb_item.id in (select item_id "
                        sql += "                    from TB_USER_SKILL "
                        sql += "                    left join TB_SKILL_ITEM on TB_USER_SKILL.skill_id = TB_SKILL_ITEM.skill_id "
                        sql += "                    where user_id = '" & UserID & "') "
                        sql += " order by priority desc"
                    End If

                    ret = GetShopDataTable(sql)
                End If
                cDt.Dispose()

            Catch ex As Exception
                ret = New DataTable
            End Try

            Return ret
        End Function

        Public Shared Function ShowCustomerWait(ByVal UserID As String, ByVal CounterID As String, ByVal UserItemID As String, ByVal AssignAppointment As String) As DataSet
            Dim ret As New DataSet
            Try
                Dim wDt As DataTable = GetCustomerWait(CounterID, UserItemID, AssignAppointment, "")
                If wDt IsNot Nothing Then
                    wDt.TableName = "ShowCustomerWait"
                    ret.Tables.Add(wDt)
                End If

                Dim stDt As DataTable = avgServiceTime(UserID)
                If stDt IsNot Nothing Then
                    stDt.TableName = "avgServiceTime"
                    ret.Tables.Add(stDt)
                End If

                Dim dpDt As DataTable = ShowDisplayService()
                If dpDt IsNot Nothing Then
                    dpDt.TableName = "ShowDisplayService"
                    ret.Tables.Add(dpDt)
                End If
            Catch ex As Exception
                ret = New DataSet
            End Try
            Return ret
        End Function

        Private Shared Function GetCustomerWait(ByVal CounterID As String, ByVal UserItemID As String, ByVal AssignAppointment As String, ByVal QueueNo As String) As DataTable
            Dim dt As New DataTable
            Try
                Dim SpeedLane As String = "0"
                Dim sql As String = "select speed_lane from TB_COUNTER where id = '" & CounterID & "'"
                Dim cDt As DataTable = GetShopDataTable(sql)
                If cDt.Rows.Count > 0 Then
                    SpeedLane = cDt.Rows(0)("speed_lane")
                End If
                cDt.Dispose()

                If UserItemID = "0" And SpeedLane = "1" Then
                    'ห้อง SpeedLane แล้วเลือก Service Appointment
                    sql = "exec SP_ShowCustomerAppWait " & CounterID & "," & 0
                ElseIf UserItemID > "0" And SpeedLane = "1" And AssignAppointment = "1" Then
                    'ห้อง SpeedLane แล้วเลือก Service ที่ไม่ใช่ Appointment
                    sql = "exec SP_ShowCustomerAppWait " & CounterID & "," & UserItemID
                Else
                    'กรณีปกติ
                    sql = "exec SP_ShowCustomerWait " & CounterID & "," & UserItemID
                End If

                dt = GetShopDataTable(sql)
                If QueueNo.Trim <> "" Then
                    If dt.Rows.Count > 0 Then
                        dt.DefaultView.RowFilter = "queue_no='" & QueueNo & "'"
                        dt = dt.DefaultView.ToTable.Copy
                    End If
                End If
            Catch ex As Exception
                dt = New DataTable
            End Try
            Return dt
        End Function

        Private Shared Function avgServiceTime(ByVal UserID As String) As DataTable
            Dim sql As String = ""
            Dim dt As New DataTable
            Try
                sql = "select TB_ITEM.id,item_name ," & vbNewLine
                sql += " case when avg(DATEDIFF(SS,start_time,end_time)) IS null " & vbNewLine
                sql += " then '-' " & vbNewLine
                sql += " else case when avg(DATEDIFF(SS,start_time,end_time))/ 60 > 9 " & vbNewLine
                sql += "    then CONVERT(varchar(10),avg(DATEDIFF(SS,start_time,end_time)) / 60) " & vbNewLine
                sql += "    else right(('0' + CONVERT(varchar(10),avg(DATEDIFF(SS,start_time,end_time)) / 60)),2) " & vbNewLine
                sql += "    end + ':' + right(('0' + convert(varchar(10),avg(DATEDIFF(SS,start_time,end_time)) % 60)),2) " & vbNewLine
                sql += " end as servicetime ," & vbNewLine
                sql += " count(1) as serve " & vbNewLine
                sql += " from TB_COUNTER_QUEUE " & vbNewLine
                sql += " left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id " & vbNewLine
                sql += " where DATEDIFF(D,GETDATE(),service_date)=0 and status = 3 " & vbNewLine
                sql += " and user_id = '" & UserID & "'" & vbNewLine
                sql += " group by TB_ITEM.id,item_name " & vbNewLine
                sql += " order by item_name" & vbNewLine
                dt = GetShopDataTable(sql)
            Catch ex As Exception
                dt = New DataTable
            End Try
            Return dt
            'GridItem.Columns("item_id").Visible = False
            'GridItem.AutoGenerateColumns = False
            'GridItem.DataSource = dt
        End Function

        Private Shared Function ShowDisplayService() As DataTable
            Dim sql As String = ""
            Dim dt As New DataTable
            Try
                sql = "exec SP_DisplayCustomerWait"
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    dt.Columns.Add("show_text")

                    For i As Int32 = 0 To dt.Rows.Count - 1
                        Dim txt As String = ""
                        txt = dt.Rows(i).Item("customertype_name").ToString & vbCrLf
                        txt &= dt.Rows(i).Item("item_name").ToString & vbCrLf
                        txt &= "No. serving " & dt.Rows(i).Item("count_queue_serve").ToString & vbCrLf
                        txt &= "No. wait to serve " & dt.Rows(i).Item("count_queue_wait").ToString & vbCrLf
                        txt &= "Max WT. " & dt.Rows(i).Item("max_wait").ToString & vbCrLf

                        dt.Rows(i)("show_text") = txt
                    Next
                End If
            Catch ex As Exception
                dt = New DataTable
            End Try
            
            Return dt
        End Function

        Public Shared Function GetQueueRefreshSec() As String
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select config_name,config_value from TB_SETTING where config_name = 'q_refresh'"
            dt = GetShopDataTable(sql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("config_value")
            Else
                Return 10
            End If
        End Function
        Public Shared Function UpdateUserServiceItem(ByVal UserItemID As String, ByVal UserID As String) As String
            Dim ret As String = ""
            Try
                Dim sql As String = ""
                sql = "update tb_user set item_id = '" & UserItemID & "' where id = '" & UserID & "'"
                ret = ExecuteShopNonQuery(sql).ToString.ToLower
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

#End Region
#Region "Call Confirm End Queue"
        Public Shared Function CallAutoForce(ByVal UserID As String, ByVal CounterID As String, ByVal AssignAppointment As String) As TabletCallingQueuePara
            Return CallQueue(UserID, CounterID, AssignAppointment, True)
        End Function

        Public Shared Function CheckTimeForce() As String
            Dim ret As String = ""
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select wait "
                sql += " from TB_FORCE_SCHEDULE "
                sql += " where DATEDIFF(D,force_date,GETDATE()) = 0 "
                sql += " and GETDATE() between start_time and dateadd(minute,slot,end_time)"
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    ret = "true|" & dt.Rows(0)("wait").ToString
                Else
                    ret = "false|No force time"
                End If
                dt.Dispose()
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function CallNextQueue(ByVal UserID As String, ByVal CounterID As String, ByVal AssignAppointment As String) As TabletCallingQueuePara
            Return CallQueue(UserID, CounterID, AssignAppointment, False)
        End Function
        Public Shared Function CallPickupQueue(ByVal UserID As String, ByVal CounterID As String, ByVal AssignAppointment As String, ByVal QueueNo As String) As TabletCallingQueuePara
            Return CallQueue(UserID, CounterID, AssignAppointment, False, QueueNo)
        End Function

        Private Shared Function GetCurrentUserItemID(ByVal UserID As String) As String
            Dim ret As String = ""
            Dim sql As String = "select item_id from tb_user where id='" & UserID & "'"
            Try
                Dim dt As DataTable = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    ret = dt.Rows(0)("item_id")
                End If
                dt.Dispose()
            Catch ex As Exception

            End Try

            Return ret
        End Function
        Private Shared Function CallQueue(ByVal UserID As String, ByVal CounterID As String, ByVal AssignAppointment As String, ByVal AutoForce As Boolean, Optional ByVal QueueNo As String = "")
            Dim ret As New TabletCallingQueuePara
            Try
                Dim UserItemID As String = GetCurrentUserItemID(UserID)

                Dim dt As DataTable = GetCustomerWait(CounterID, UserItemID, AssignAppointment, QueueNo)
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        If CInt(dt.Rows(i).Item("ck_hold").ToString) > 0 Or CInt(dt.Rows(i).Item("status").ToString) = 8 Then
                            'ถ้าเป็นคิวจอง แต่ยังไม่ถึงเวลาที่นัด ก็ยังเรียกคิวนี้ไม่ได้
                            ret.ErrorMessage = "Not appointment time"

                            Return ret
                        End If

                        Dim shTrans As New TransactionDB
                        Dim DateNow As String = SqlDB.GetDateNowFromDB(shTrans.Trans).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))

                        If UpdateCallStatus(UserID, CounterID, dt.Rows(i).Item("queue_no").ToString, dt.Rows(i).Item("customer_id").ToString, UserItemID, DateNow, AutoForce, shTrans) = True Then
                            CallUnitDisplay(CounterID, dt.Rows(i).Item("queue_no").ToString, shTrans)
                            CallSpeaker(CounterID, dt.Rows(i).Item("queue_no").ToString, shTrans)
                            shTrans.CommitTransaction()

                            ret.ReturnResult = "true"
                            ret.QueueNo = dt.Rows(i).Item("queue_no").ToString
                            ret.CustomerTypeID = dt.Rows(i).Item("customertype_id").ToString
                            ret.CustomerTypeName = dt.Rows(i).Item("customertype_name").ToString
                            ret.MobileNo = dt.Rows(i).Item("customer_id").ToString
                            ret.CustomerName = dt.Rows(i).Item("customer_name").ToString
                            ret.TimeHold = dt.Rows(i)("hold")

                            If ret.MobileNo <> "" Then
                                Dim Sql As String = "exec SP_FindCustomerInfo '" & dt.Rows(i).Item("customer_id").ToString.Replace("-", "") & "'"
                                Dim cDt As DataTable = GetShopDataTable(Sql)
                                If cDt.Rows.Count > 0 Then
                                    ret.CustomerEmail = cDt.Rows(0).Item("Email").ToString
                                    ret.MobileStatus = cDt.Rows(0).Item("Mobile_status").ToString
                                    ret.BirthDate = cDt.Rows(0).Item("Birth_date").ToString
                                    ret.Catagory = cDt.Rows(0).Item("Category").ToString
                                    ret.AccBalance = cDt.Rows(0).Item("Acc").ToString
                                    ret.ConClass = cDt.Rows(0).Item("Con_class").ToString
                                    ret.ServiceYear = cDt.Rows(0).Item("Service_year").ToString
                                    ret.Churn = cDt.Rows(0).Item("Churn").ToString
                                    ret.NetworkType = cDt.Rows(0).Item("Network_type").ToString
                                    ret.SegmentLevel = cDt.Rows(0).Item("Segment_Level").ToString
                                    ret.PreLang = cDt.Rows(0).Item("Pre_lang").ToString
                                    ret.BillingSystem = IIf(cDt.Rows(0).Item("Billing_System").ToString = "BOS", "Yes", "No")
                                    ret.BrandName = cDt.Rows(0).Item("Brand_Name").ToString
                                    If InStr(ret.PreLang.ToUpper, "THAI") > 0 Then
                                        If cDt.Rows(0).Item("Camp_name").ToString.Trim = "" And cDt.Rows(0).Item("campaign_desc_th2").ToString.Trim = "" Then

                                        Else
                                            ret.CampaignCode = cDt.Rows(0).Item("Camp_code").ToString
                                            ret.CampaignName = cDt.Rows(0).Item("Camp_name").ToString
                                            ret.CampaignDesc = cDt.Rows(0).Item("campaign_desc_th2").ToString
                                        End If
                                    Else
                                        If cDt.Rows(0).Item("campaign_desc").ToString.Trim = "" And cDt.Rows(0).Item("campaign_desc_en2").ToString.Trim = "" Then

                                        Else
                                            ret.CampaignCode = cDt.Rows(0).Item("campaign_name_en").ToString
                                            ret.CampaignName = cDt.Rows(0).Item("campaign_desc").ToString
                                            ret.CampaignDesc = cDt.Rows(0).Item("campaign_desc_en2").ToString
                                        End If
                                    End If

                                    Dim cImg As New ShParaDb.ShopWebService.CaptureImageResponsePara
                                    Dim shWs As New CallWebService.ShopWebServiceENG
                                    cImg = shWs.LoadImageFile(ret.MobileNo)
                                    If cImg.CaptureResult = False Then
                                        If cDt.Rows(0).Item("url_capture").ToString.Trim <> "" Then
                                            'Convert Image to Base64String
                                            Dim imag As System.Drawing.Image = GetImageFromURL(UserID, cDt.Rows(0).Item("url_capture").ToString.Trim)
                                            If imag IsNot Nothing Then
                                                ret.ImageBase64String = ImageURLToBase64String(imag, System.Drawing.Imaging.ImageFormat.Jpeg)
                                            End If
                                        End If
                                    Else
                                        'Convert Byte to Base64String
                                        Dim bipimag As New IO.MemoryStream(cImg.CaptureImage)
                                        Dim imag As System.Drawing.Image = New System.Drawing.Bitmap(bipimag)
                                        If imag IsNot Nothing Then
                                            ret.ImageBase64String = ImageURLToBase64String(imag, System.Drawing.Imaging.ImageFormat.Jpeg)
                                        End If
                                        bipimag.Close()
                                    End If
                                End If
                                cDt.Dispose()
                            End If

                            Return ret
                        Else
                            shTrans.RollbackTransaction()
                            'If AutoForceQueue = False Then
                            ret.ReturnResult = "false"
                            ret.ErrorMessage = "The information has already been updated by the other user."
                            Return ret
                            'End If
                        End If
                    Next
                Else
                    ret.ReturnResult = "false"
                    ret.ErrorMessage = "No Queue Waiting for this Service"
                End If
                dt.Dispose()
            Catch ex As Exception
                ret.ReturnResult = "false"
                ret.ErrorMessage = ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function CallQuickService(ByVal UserID As String, ByVal CounterID As String, ByVal AssignAppointment As String, ByVal QueueNo As String) As TabletConfirmQueue
            Dim ret As New TabletConfirmQueue
            Try
                Dim UserItemID As String = GetCurrentUserItemID(UserID)

                Dim dt As DataTable = GetCustomerWait(CounterID, UserItemID, AssignAppointment, QueueNo)
                If dt.Rows.Count > 0 Then
                    Dim MobileNo As String = ""
                    If Convert.IsDBNull(dt.Rows(0)("customer_id")) = False Then
                        MobileNo = dt.Rows(0)("customer_id")
                    End If

                    Dim shTrans As New TransactionDB
                    If UpdateQueueStatus(UserID, 2, CounterID, 0, QueueNo, MobileNo, shTrans, UserItemID) = True Then
                        ServeUnitDisplay(CounterID, QueueNo, shTrans)

                        shTrans.CommitTransaction()
                        ret.ReturnResult = "true"
                        ret.QueueNo = QueueNo
                        ret.MobileNo = MobileNo
                        ret.QueueServiceList = GetConfirmQueueServiceList(UserID, QueueNo, MobileNo, UserItemID)
                        If ret.QueueServiceList.Rows.Count > 0 Then
                            ret.StartTime = Convert.ToDateTime(ret.QueueServiceList.Rows(0)("start_time")).ToString("yyyy-MM-dd hh:mm:ss.fff", New Globalization.CultureInfo("en-US"))
                        End If

                        If UserItemID > "0" Then
                            ret.IsTransferQueue = "true"
                            ret.IsAddService = "true"
                        End If
                    Else
                        shTrans.RollbackTransaction()
                        ret.ReturnResult = "false"
                        ret.ErrorMessage = "Database Connection Fail!!! Please try again."
                    End If
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function


        Private Shared Function GetImageFromURL(ByVal UserID As String, ByVal ImageUrl As String) As System.Drawing.Image
            Dim ret As System.Drawing.Image
            Try
                'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                  Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                  chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                  sslerror As System.Net.Security.SslPolicyErrors) True

                Dim dt As New DataTable
                dt = GetShopDataTable("select username, password from tb_user where id='" & UserID & "'")
                If dt.Rows.Count > 0 Then
                    Dim req As System.Net.HttpWebRequest = System.Net.WebRequest.Create(ImageUrl)
                    req.Method = "GET"
                    req.ContentType = "image/jpeg"
                    req.UserAgent = "Mozilla/4.0+(compatible;+MSIE+5.01;+Windows+NT+5.0"
                    req.Credentials = New System.Net.NetworkCredential(dt.Rows(0)("username"), Engine.Common.FunctionEng.DeCripPwd(dt.Rows(0)("password")))

                    Dim res As System.Net.WebResponse = req.GetResponse
                    ret = System.Drawing.Image.FromStream(res.GetResponseStream())
                End If
                dt.Dispose()
            Catch ex As Exception
                ret = Nothing
            End Try

            Return ret
        End Function

        Public Shared Function ImageURLToBase64String(ByVal image As System.Drawing.Image, ByVal imageFormat As System.Drawing.Imaging.ImageFormat) As String
            Dim result As String = ""
            Using memStream As New IO.MemoryStream
                Try
                    image.Save(memStream, imageFormat)
                    result = Convert.ToBase64String(memStream.ToArray())
                    memStream.Close()
                Catch ex As Exception
                    result = ""
                End Try
            End Using
            Return result
        End Function

        Private Shared Function UpdateCallStatus(ByVal UserID As String, ByVal CounterID As Integer, ByVal QueueNo As String, ByVal MobileNo As String, ByVal ItemID As String, ByVal DateNow As String, ByVal AutoForceQueue As Boolean, ByVal shTrans As TransactionDB) As Boolean
            Dim ret As Boolean = False

            Dim Status As Integer = 4
            Dim Sql As String = "update TB_COUNTER_QUEUE "
            Sql += " set status = " & Status & ",call_time = '" & DateNow & "',[user_id] = '" & UserID & "',counter_id = '" & CounterID & "' "
            If AutoForceQueue = True Then
                Sql += ",call_auto_force='1'"
            End If
            Sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' "
            Sql += " and customer_id = '" & MobileNo & "' and status = 1"
            ret = ExecuteShopNonQuery(Sql, shTrans)

            If ret = True Then
                ret = InsertMainDisplay(UserID, QueueNo, CounterID, DateNow, Status, shTrans)
                If ret = True Then
                    InsertLog(QueueNo, MobileNo, UserID, 0, CounterID, Status, "", shTrans, "'" & DateNow & "'")


                    'If uDt.Rows.Count > 0 Then
                    If ItemID = "0" Then
                        'Appointment 
                        Dim dt As New DataTable
                        Sql = "select top 1 q.item_id "
                        Sql += " from TB_COUNTER_QUEUE q "
                        Sql += " left join TB_ITEM t on q.item_id = t.id "
                        Sql += " where datediff(d,getdate(),q.service_date)=0 "
                        Sql += " and q.queue_no = '" & QueueNo & "' and q.customer_id = '" & MobileNo & "' "
                        Sql += " and q.status = " & Status
                        Sql += " order by t.item_wait,t.item_time,t.item_order"
                        dt = GetShopDataTable(Sql, shTrans)
                        If dt.Rows.Count > 0 Then
                            Sql = "update tb_counter_queue "
                            Sql += " set flag = 'MainDisplay' "
                            Sql += " where datediff(d,getdate(),service_date)=0 "
                            Sql += " and queue_no = '" & QueueNo & "' "
                            Sql += " and customer_id = '" & MobileNo & "' "
                            Sql += " and item_id = " & dt.Rows(0).Item("item_id").ToString
                            Sql += " and status = " & Status
                            ret = ExecuteShopNonQuery(Sql, shTrans)
                        End If
                        dt.Dispose()
                    Else
                        Sql = "update tb_counter_queue "
                        Sql += " set flag = 'MainDisplay' where datediff(d,getdate(),service_date)=0 "
                        Sql += " and queue_no = '" & QueueNo & "' "
                        Sql += " and customer_id = '" & MobileNo & "' "
                        Sql += " and item_id = '" & ItemID & "'"
                        Sql += " and status = " & Status
                        ret = ExecuteShopNonQuery(Sql, shTrans)
                    End If
                End If
            End If

            Return ret
        End Function

        'Update Status ของคิว
        Private Shared Function UpdateQueueStatus(ByVal UserID As String, ByVal Status As Integer, ByVal CounterID As Integer, ByVal TimeHold As Integer, ByVal QueueNo As String, ByVal CustomerID As String, ByVal _Trans As TransactionDB, Optional ByVal ItemID As Int32 = 0, Optional ByVal Flag As String = "") As Boolean
            Dim ret As Boolean = False
            Dim sql As String = ""
            Dim DateNow As String = SqlDB.GetDateNowFromDB(_Trans.Trans).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))

            Select Case Status
                Case 1
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "' ,counter_id = 0,[user_id] = 0"
                    sql += " ,call_time = null,start_time = Null,call_auto_force='0'"
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (2,4,6,8)"
                Case 2
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "', start_time = '" & DateNow & "',[user_id] = '" & UserID & "',counter_id = '" & CounterID & "'"
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (1,4)"
                Case 3
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "',end_time = '" & DateNow & "',[user_id] = '" & UserID & "',counter_id = '" & CounterID & "'"
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = '" & ItemID & "'"
                Case 4
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "',call_time = '" & DateNow & "',[user_id] = '" & UserID & "',counter_id = '" & CounterID & "' "
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (1)"
                Case 5
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "',end_time = '" & DateNow & "',[user_id] = '" & UserID & "',counter_id = '" & CounterID & "'"
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = '" & ItemID & "'"
                Case 6
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "',hold = DATEADD(minute," & TimeHold & ",GETDATE()),[user_id] = '" & UserID & "',counter_id = '" & CounterID & "'"
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (1,2,4,8) "
                Case 8
                    sql = "update TB_COUNTER_QUEUE "
                    sql += " set status = '" & Status & "',start_time = '" & DateNow & "',[user_id] = '" & UserID & "',hold = null "
                    sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (4)"
            End Select
            ret = ExecuteShopNonQuery(sql, _Trans)

            If ret = True Then
                If Status = 1 Then
                    ClearMainDisplay(CounterID, _Trans)

                    If ItemID = 0 Then
                        sql = "update tb_counter_queue set flag = '' where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status = 1"
                    Else
                        sql = "update tb_counter_queue set flag = '' where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = '" & ItemID & "'"
                    End If
                    ret = ExecuteShopNonQuery(sql, _Trans)
                ElseIf Status = 2 Or Status = 4 Then
                    ret = InsertMainDisplay(UserID, QueueNo, CounterID, DateNow, Status, _Trans)
                    If ret = True Then
                        If ItemID = 0 Then
                            'Appointment
                            Dim dt As New DataTable
                            sql = "select top 1 TB_COUNTER_QUEUE.item_id from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status = '" & Status & "' order by item_wait,item_time,item_order"
                            dt = GetShopDataTable(sql, _Trans)
                            If dt.Rows.Count > 0 Then
                                sql = "update tb_counter_queue set flag = 'MainDisplay' where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = " & dt.Rows(0).Item("item_id").ToString
                                ret = ExecuteShopNonQuery(sql, _Trans)
                            End If
                            dt.Dispose()
                        Else
                            sql = "update tb_counter_queue set flag = 'MainDisplay' where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = '" & ItemID & "'"
                            ret = ExecuteShopNonQuery(sql, _Trans)
                        End If
                    End If
                End If
            End If
            If ret = True Then
                InsertLog(QueueNo, CustomerID, UserID, 0, CounterID, Status, Flag, _Trans, "'" & DateNow & "'")
            End If

            Return ret
        End Function

        Public Shared Function CloseCallingQueue(ByVal UserID As String, ByVal CounterID As String, ByVal QueueNo As String, ByVal MobileNo As String, ByVal TimeHold As String) As String
            Dim ret As String = ""
            Try
                Dim sTrans As New TransactionDB
                Dim sql As String = ""
                sql = "delete from TB_speaker where datediff(d,getdate(),call_date)=0 and queue_no ='" & QueueNo & "' and counter_id = '" & CounterID & "'"
                ExecuteShopNonQuery(sql, sTrans)

                If UpdateQueueStatus(UserID, 1, CounterID, 0, QueueNo, MobileNo, sTrans) = True Then
                    ret = "true"
                    If TimeHold <> "-" Then
                        Dim vDateNow As String = SqlDB.GetDateNowFromDB(sTrans.Trans).ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
                        sql = "update tb_counter_queue set hold = '" & vDateNow & " " & TimeHold & "' "
                        sql += " where datediff(d,getdate(),service_date)=0 "
                        sql += " and queue_no = '" & QueueNo & "' "
                        sql += " and customer_id = '" & MobileNo & "' and status = 1"
                        ret = ExecuteShopNonQuery(sql, sTrans).ToString.ToLower
                    ElseIf TimeHold.Trim = "" Then
                        ret = "false|Incorrect TimeHold"
                    End If

                    If ret = "true" Then
                        ClearUnitDisplay(CounterID, False, sTrans)

                        sTrans.CommitTransaction()
                    Else
                        sTrans.RollbackTransaction()
                    End If
                Else
                    sTrans.RollbackTransaction()
                    ret = "false|Update Queue Status Fail!"
                End If
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function MissedCallQueue(ByVal QueueNo As String, ByVal MobileNo As String, ByVal UserID As String, ByVal CounterID As String) As String
            Dim ret As String = ""
            Try
                If CheckCustomerStatus(QueueNo, MobileNo, 4) = True Then
                    Dim sql As String = ""
                    Dim dt As New DataTable
                    sql = "select item_id,customertype_id,getdate() datenow "
                    sql += " from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 "
                    sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' "
                    sql += " and status = 4 "
                    dt = GetShopDataTable(sql)
                    If dt.Rows.Count > 0 Then
                        Dim vDateNow As String = "'" & Convert.ToDateTime(dt.Rows(0)("datenow")).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"

                        Dim _err As String = ""
                        sql = "update tb_counter_queue "
                        sql += " set status = 8,call_time = assign_time,start_time = assign_time,end_time = assign_time,"
                        sql += " user_id = '" & UserID & "',counter_id = '" & CounterID & "'"
                        sql += " where datediff(d,getdate(),service_date)=0 "
                        sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' "
                        sql += " and status = 4"
                        Dim re As Boolean = ExecuteShopNonQuery(sql)
                        If re = True Then
                            InsertLog(QueueNo, MobileNo, UserID, 0, CounterID, 8, "", Nothing, vDateNow)

                            If dt.Rows(0)("customertype_id") = "3" Then
                                'ถ้าเป็นคิวจอง
                                'คืน Slot
                                UpdateAppointmentSlot(MobileNo, QueueNo)

                                sql = "select appointment_job_id,customer_id,queue_no "
                                sql += " from TB_APPOINTMENT_CUSTOMER "
                                sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                                sql += " and customer_id = '" & MobileNo & "' "
                                sql += " and queue_no='" & QueueNo & "' "
                                Dim aDt As New DataTable
                                aDt = GetShopDataTable(sql)
                                If aDt.Rows.Count > 0 Then
                                    'Register แล้ว MissCall
                                    sql = "update TB_APPOINTMENT_CUSTOMER "
                                    sql += " set active_status = 4 "
                                    sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
                                    sql += " and customer_id = '" & MobileNo & "' and queue_no='" & QueueNo & "' "
                                    ExecuteShopNonQuery(sql)

                                    'Update Appointment Job To DC
                                    'ถ้าต้องการให้ระบบนำไปคิด Backlist ให้กำหนด status=5 แต่ถ้าไม่คิด Backlist ให้กำหนดเป็น 4
                                    UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "4")

                                    'Update Siebel when Missed Call
                                    Dim dtS As DataTable = GetSiebelData(MobileNo, "4", "Registered")
                                    If dtS.Rows.Count > 0 Then
                                        Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                                        Dim CancelDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                                        Try
                                            Dim ws As New ShopWebServiceENG
                                            ws.SiebelUpdateToMissed(MobileNo, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                                            ws = Nothing
                                        Catch ex As Exception
                                            re = False
                                            _err = "TabletWebServiceENG.MissedCallQueue Exception : " & ex.Message & vbNewLine & " Don''t  Update Activity to Missed Call Mobile No.  "
                                            Engine.Common.FunctionEng.SaveShopErrorLog("TabletWebServiceENG.MissedCallQueue", _err)
                                        End Try
                                    Else
                                        re = False
                                    End If
                                Else
                                    re = False
                                    _err = "No data foune in TB_APPOINTMENT_CUSTOMER  ##SQL=" & sql
                                End If
                                aDt.Dispose()
                            End If
                        Else
                            re = False
                            _err = "Error Update TB_COUNTER_QUEUE   ### SQL=" & sql
                        End If
                        If re = True Then
                            ret = "true"
                        Else
                            ret = "false|" & _err
                        End If
                    End If
                Else
                    ret = "false|The information has already been updated by the other user."
                End If
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Private Shared Function GetConfirmQueueServiceList(ByVal UserID As String, ByVal QueueNo As String, ByVal MobileNo As String, ByVal ItemID As String) As DataTable
            Dim ret As New DataTable
            Try
                Dim Sql As String = "select item_id,item_name,1 as active,item_wait,item_time,item_order,TB_COUNTER_QUEUE.id,TB_COUNTER_QUEUE.start_time, 'true' active_service from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2 and item_id = '" & ItemID & "'" & vbCrLf
                Sql &= "union all" & vbCrLf
                Sql &= "select item_id,item_name,2 as active,item_wait,item_time,item_order,TB_COUNTER_QUEUE.id,TB_COUNTER_QUEUE.start_time, 'true' active_service  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2 and item_id in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = '" & UserID & "') and item_id <> '" & ItemID & "' " & vbCrLf
                Sql &= "union all" & vbCrLf
                Sql &= "select item_id,item_name,3 as active,item_wait,item_time,item_order,TB_COUNTER_QUEUE.id,TB_COUNTER_QUEUE.start_time, 'false' active_service  from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2 and item_id not in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = '" & UserID & "')" & vbCrLf
                Sql &= "order by active asc,item_wait,item_time,item_order"

                ret = GetShopDataTable(Sql)
            Catch ex As Exception
                ret = New DataTable
            End Try
            Return ret
        End Function

        Public Shared Function ConfirmQueue(ByVal UserID As String, ByVal CounterID As String, ByVal QueueNo As String, ByVal MobileNo As String) As TabletConfirmQueue
            Dim ret As New TabletConfirmQueue
            Try
                If CheckCustomerStatus(QueueNo, MobileNo, 4) = True Then
                    Dim ItemID As String = GetCurrentUserItemID(UserID)

                    Dim shTrans As New TransactionDB
                    If UpdateQueueStatus(UserID, 2, CounterID, 0, QueueNo, MobileNo, shTrans, ItemID) = True Then
                        ServeUnitDisplay(CounterID, QueueNo, shTrans)

                        shTrans.CommitTransaction()
                        ret.ReturnResult = "true"
                        ret.QueueNo = QueueNo
                        ret.MobileNo = MobileNo
                        ret.QueueServiceList = GetConfirmQueueServiceList(UserID, QueueNo, MobileNo, ItemID)
                        If ret.QueueServiceList.Rows.Count > 0 Then
                            ret.StartTime = Convert.ToDateTime(ret.QueueServiceList.Rows(0)("start_time")).ToString("yyyy-MM-dd hh:mm:ss.fff", New Globalization.CultureInfo("en-US"))
                        End If

                        If ItemID > "0" Then
                            ret.IsTransferQueue = "true"
                            ret.IsAddService = "true"
                        End If

                        'สำหรับ Tablet ไม่ต้องแสดง SiebelPOP
                        'Try
                        '    If Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "UseSiebelPop", "QueueSharp", "TabletWebService.ConfirmQueue") = "Y" Then
                        '        Dim SiebelPopPath As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "SiebelPopPath", "QueueSharp", "TabletWebService.ConfirmQueue")
                        '        If SiebelPopPath.Trim <> "" Then
                        '            If Replace(Trim(lblNetwork_type.Text), "-", "") <> "" Then
                        '                Dim strPathExe As String = SiebelPopPath & "SiebelPop.exe "
                        '                Dim process As New Process
                        '                process.StartInfo.FileName = strPathExe
                        '                process.StartInfo.Arguments = Trim(lblCustomerID.Text) & ",Walk In"
                        '                process.Start()
                        '            End If
                        '        End If
                        '    End If
                        'Catch ex As Exception

                        'End Try
                    Else
                        shTrans.RollbackTransaction()
                        ret.ReturnResult = "false"
                        ret.ErrorMessage = "Database Connection Fail!!! Please try again."
                    End If
                End If
            Catch ex As Exception
                ret.ReturnResult = "false"
                ret.ErrorMessage = ex.Message
            End Try
            Return ret
        End Function

        Private Shared Function CalServeTime(ByVal TimeSec As Integer) As String
            Dim ret As String = ""
            Try
                Dim TimeM As String = (TimeSec \ 60).ToString.PadLeft(2, "0")
                Dim TimeS As String = (TimeSec Mod 60).ToString.PadLeft(2, "0")
                ret = TimeM & ":" & TimeS
            Catch ex As Exception
                ret = ""
            End Try

            Return ret
        End Function

        Public Shared Function GetServiceCancel(ByVal UserID As String, ByVal CounterID As String, ByVal QueueNo As String, ByVal MobileNo As String) As DataTable
            Dim ret As New DataTable
            Try
                Dim UserItemID As String = GetCurrentUserItemID(UserID)
                Dim Sql As String = "select y.id,y.item_name "
                Sql += " from TB_COUNTER_QUEUE x "
                Sql += " left join TB_ITEM y on x.item_id = y.id "
                Sql += " where DATEDIFF(D,GETDATE(),service_date) = 0 "
                Sql += " and status in (1,2,4,6,8) and item_id = '" & UserItemID & "' "
                Sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' "
                Sql += " union all "
                Sql += " select y.id,y.item_name "
                Sql += " from TB_COUNTER_QUEUE x "
                Sql += " left join TB_ITEM y on x.item_id = y.id "
                Sql += " where DATEDIFF(D,GETDATE(),service_date) = 0 "
                Sql += " and status in (1,2,4,6,8) and item_id <> '" & UserItemID & "' "
                Sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "'"
                ret = GetShopDataTable(Sql)
            Catch ex As Exception
                ret = New DataTable
            End Try
            Return ret
        End Function

        Public Shared Function CancelService(ByVal UserID As String, ByVal CounterID As String, ByVal MobileNo As String, ByVal QueueNo As String, ByVal ListItemID As String, ByVal Remarks As String) As String
            Dim ret As String = ""
            Try
                If Split(ListItemID, ",").Length = 0 Then
                    'ไม่เลือก Service
                    Return "false|Please select Service"
                End If

                If Remarks.Trim = "" Then
                    'ไม่กรอก Remarks
                    Return "false|Please Enter Remark."
                End If

                Dim dDataNow As DateTime = SqlDB.GetDateNowFromDB(Nothing)
                Dim vDateNow As String = "'" & dDataNow.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
                Dim shTrans As New TransactionDB
                Dim sql As String = ""
                '1. ตรวจสอบว่ามีการ End ไปแล้วอย่างน้อย 1 Service
                Dim FirstEnd As Boolean = CheckEndService(CounterID, MobileNo, QueueNo)
                For Each ItemID As String In Split(ListItemID, ",")
                    If FirstEnd = False Then
                        'Cancel Service แรก
                        '2. กรณียังไม่มีการ End เลย ให้ดึงเวลาจากเวลาที่ Call
                        sql = "declare @CallTime as datetime;select @CallTime = (select MAX(call_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "')" & vbCrLf
                        sql &= "update tb_counter_queue set status = 5,start_time = @CallTime,end_time = @CallTime,user_id = '" & UserID & "',counter_id = '" & CounterID & "' "
                        sql += " where datediff(d,getdate(),service_date)=0 "
                        sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and item_id = " & ItemID
                        ExecuteShopNonQuery(sql, shTrans)
                    Else
                        '3. กรณีที่มีการ End ไปแล้ว ให้ดึงเวลาจาก Service ล่าสุดที่เพิ่ง End
                        sql = "declare @EndTime as datetime;select @EndTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "')" & vbCrLf
                        sql &= "update tb_counter_queue set status = 5,assign_time = @EndTime,call_time = @EndTime,start_time = @EndTime,end_time = @EndTime where datediff(d,getdate(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and item_id = '" & ItemID & "'"
                        ExecuteShopNonQuery(sql, shTrans)
                    End If

                    '4. Insert Log
                    InsertLog(QueueNo, MobileNo, UserID, ItemID, CounterID, 5, "Cancel Serving Service From Tablet Remarks:" & Remarks, shTrans, vDateNow)
                Next
                shTrans.CommitTransaction()

                '5. ตรวจสอบคิวจอง ถ้าเป็นคิวจองให้ทำการคืน Slot
                sql = "select customertype_id, status "
                sql += " from tb_counter_queue "
                sql += " where DATEDIFF(d,GETDATE(),service_date)=0 "
                sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "'"
                Dim dt As New DataTable
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("customertype_id") = "3" Then
                        dt.DefaultView.RowFilter = " status = 5 "
                        If dt.Rows.Count = dt.DefaultView.Count Then
                            'คืน Slot ก่อน
                            UpdateAppointmentSlot(MobileNo, QueueNo)

                            sql = "select appointment_job_id "
                            sql += " from TB_APPOINTMENT_CUSTOMER "
                            sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                            sql += " and customer_id = '" & MobileNo & "' "
                            sql += " and queue_no='" & QueueNo & "' "
                            Dim aDt As New DataTable
                            aDt = GetShopDataTable(sql)
                            If aDt.Rows.Count > 0 Then
                                sql = "update TB_APPOINTMENT_CUSTOMER "
                                sql += " set active_status = 6 "   'Cancel Queue
                                sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
                                sql += " and customer_id = '" & MobileNo & "' "
                                sql += " and queue_no = '" & QueueNo & "'"
                                shTrans = New TransactionDB
                                ExecuteShopNonQuery(sql, shTrans)
                                shTrans.CommitTransaction()

                                'Update Appointment Job To DC
                                UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "6")

                                'Update Siebel To Cancel กรณีเป็นคิวจอง กด Cancel  ทุก Service จากหน้าจอ Service Time
                                SiebelUpdateToCancel(MobileNo, dt.Rows(0)("customertype_id"))
                            End If
                            aDt.Dispose()
                        End If
                    End If
                End If
                dt.Dispose()

                '6. Update เวลา assign_time, call_time, start_time ของ Service ที่เหลือ
                vDateNow = dDataNow.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))
                sql = " update tb_counter_queue "
                sql += " set assign_time = '" & vDateNow & "',call_time = '" & vDateNow & "',start_time = '" & vDateNow & "' "
                sql += " where datediff(d,getdate(),service_date)=0 "
                sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2"
                shTrans = New TransactionDB
                ExecuteShopNonQuery(sql, shTrans)
                shTrans.CommitTransaction()

                '7. Update TB_APPOINTMENT_CUSTOMER.active_status=3 กรณีเป็นคิวจอง เพื่อ Stamp ว่าลูกค้าได้มารับบริการแล้ว
                If FirstEnd = False Then
                    sql = "update TB_APPOINTMENT_CUSTOMER "
                    sql += " set active_status = 3 "
                    sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
                    sql += " and customer_id = '" & MobileNo & "' and queue_no='" & QueueNo & "' "

                    shTrans = New TransactionDB
                    ExecuteShopNonQuery(sql, shTrans)
                    shTrans.CommitTransaction()
                End If

                '8. 'Update Status ของ Service ที่ไม่ได้ทำกับ User คนนี้  (EndQueueLastService)
                ret = EndQueueLastService(CounterID, QueueNo, MobileNo)
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function TransferQueue(ByVal UserID As String, ByVal CounterID As String, ByVal MobileNo As String, ByVal QueueNo As String) As String
            Dim ret As String = ""
            Try
                '1. ตรวจสอบว่ามีการ End แล้วอย่างน้อย 1 Service ที่ Counter นี้
                If CheckEndService(CounterID, MobileNo, QueueNo) = True Then

                    Dim vDateNow As String = "'" & SqlDB.GetDateNowFromDB(Nothing).ToString("yyyy-MM-dd hh:mm:ss.fff") & "'"
                    '2. Insert Log ว่า Transfer
                    Dim shTrans As New TransactionDB
                    InsertLog(QueueNo, MobileNo, UserID, 0, CounterID, 1, "Transfer", shTrans, vDateNow)
                    shTrans.CommitTransaction()

                    '3. 'Update Status ของ Service ที่ไม่ได้ทำกับ User คนนี้  (EndQueueLastService)
                    ret = EndQueueLastService(CounterID, QueueNo, MobileNo)
                Else
                    ret = "false|Cannot Transfer Queue"
                End If
            Catch ex As Exception
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function GetServiceAdd(ByVal QueueNo As String, ByVal MobileNo As String) As DataTable
            Dim ret As New DataTable
            Try
                Dim sql As String = "select id,item_name  "
                sql += " from TB_ITEM "
                sql += " where active_status = 1 "
                sql += " and id not in (select item_id "
                sql += "                from TB_COUNTER_QUEUE "
                sql += "                where DATEDIFF(D,GETDATE(),service_date)=0 "
                sql += "                and status in (1,2,3,4,5) "
                sql += "                and queue_no = '" & QueueNo & "' "
                sql += "                and customer_id = '" & MobileNo & "') "
                sql += " order by item_order"

                ret = GetShopDataTable(sql)
            Catch ex As Exception
                ret = New DataTable
            End Try
            Return ret
        End Function

        Public Shared Function AddServiceItem(ByVal UserID As String, ByVal CounterID As String, ByVal QueueNo As String, ByVal MobileNo As String, ByVal AddItemID As String) As TabletAddServicePara
            Dim ret As New TabletAddServicePara
            Try
                If AddItemID = "" Then
                    ret.ReturnResult = "false"
                    ret.ErrorMessage = "You need to select one service to click OK button."
                End If

                '1. ตรวจสอบ Service ที่เลือก ว่าเป็น Service ที่ไม่เคยมีอยู่ในคิวนี้มาก่อน
                Dim sql As String = "select top 1 * "
                sql += " from TB_COUNTER_QUEUE "
                sql += " where DATEDIFF(D,GETDATE(),service_date)=0 "
                sql += " and queue_no = '" & QueueNo & "' "
                sql += " and customer_id = '" & MobileNo & "' "
                sql += " order by service_date  desc"
                Dim dt As DataTable = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    Dim dr As DataRow = dt.Rows(0)
                    Dim vDateNow As String = "'" & SqlDB.GetDateNowFromDB(Nothing).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"

                    Dim shTrans As New TransactionDB
                    '2. Insert ข้อมูลใน TB_COUNTER_QUEUE พร้อม Flag add_service =1
                    sql = " declare @ServiceDate as datetime;select @ServiceDate = (select MAX(service_date) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2)" & vbCrLf
                    sql &= " declare @AssignTime as datetime;select @AssignTime = (select MAX(assign_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2)" & vbCrLf
                    sql &= " declare @CallTime as datetime;select @CallTime = (select MAX(call_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2)" & vbCrLf
                    sql &= " declare @StartTime as datetime;select @StartTime = (select MAX(start_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2)" & vbCrLf
                    sql &= " insert into TB_counter_queue(id,queue_no,customer_id,customertype_id,"
                    sql &= " item_id,customer_name,segment,counter_id,service_date,status,user_id,assign_time,call_time,start_time,network_type,add_service,call_auto_force) "
                    sql &= " values((select max(id)+1 from TB_COUNTER_QUEUE),'" & QueueNo & "','" & MobileNo & "'," & dr("customertype_id").ToString & ","
                    sql &= AddItemID & ",'','" & dr("segment").ToString & "','" & CounterID & "',@ServiceDate,2," & UserID & ",@AssignTime,@CallTime,@StartTime,'" & dr("network_type").ToString & "','1','" & dr("call_auto_force").ToString & "')"

                    If ExecuteShopNonQuery(sql, shTrans) = True Then

                        '3. Insert Log
                        InsertLog(QueueNo, MobileNo, UserID, AddItemID, CounterID, 2, "Add", shTrans, vDateNow)

                        '4. Update TB_COUNTER_QUEUE.combo_item_all
                        Dim combo As String = dt.Rows(0).Item("combo_item_all").ToString & "," & AddItemID
                        sql = "update TB_COUNTER_QUEUE "
                        sql += " set combo_item_all = '" & combo & "' "
                        sql += " where DATEDIFF(D,GETDATE(),service_date)=0 "
                        sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "'"

                        If ExecuteShopNonQuery(sql, shTrans) = True Then
                            shTrans.CommitTransaction()

                            sql = "select top 1 tb_counter_queue.item_id,item_name,isnull(SCH.item_id,0) as service_active "
                            sql += " from tb_counter_queue "
                            sql += " left join TB_ITEM on tb_counter_queue.item_id = TB_ITEM.id "
                            sql += " left join (select item_id "
                            sql += "            from TB_USER_SERVICE_SCHEDULE "
                            sql += "            where DATEDIFF(D,GETDATE(),service_date) = 0 "
                            sql += "            and active_status = 1 and user_id = '" & UserID & "') as SCH on tb_counter_queue.item_id = SCH.item_id "
                            sql += " where DATEDIFF(D,GETDATE(),service_date)=0 "
                            sql += " and queue_no = '" & QueueNo & "' "
                            sql += " and customer_id = '" & MobileNo & "' "
                            sql += " order by tb_counter_queue.id desc"

                            Dim tmpDt As DataTable = GetShopDataTable(sql)
                            If tmpDt.Rows.Count > 0 Then
                                ret.ReturnResult = "true"
                                ret.QueueNo = QueueNo
                                ret.MobileNo = MobileNo
                                ret.AddItemID = tmpDt.Rows(0)("item_id")
                                ret.AddItemName = tmpDt.Rows(0)("item_name")

                                If Convert.ToInt16(tmpDt.Rows(0)("service_active")) > 0 Then
                                    ret.ServiceActive = 1
                                Else
                                    ret.ServiceActive = 0
                                End If
                            End If
                            tmpDt.Dispose()
                        Else
                            shTrans.RollbackTransaction()
                            ret.ReturnResult = "false"
                            ret.ErrorMessage = "The selected service already exists! Please select the new one."
                        End If
                    Else
                        shTrans.RollbackTransaction()
                        ret.ReturnResult = "false"
                        ret.ErrorMessage = "The selected service already exists! Please select the new one."
                    End If
                Else
                    ret.ReturnResult = "false"
                    ret.ErrorMessage = "The selected service already exists! Please select the new one."
                End If
                dt.Dispose()
                
            Catch ex As Exception
                ret.ReturnResult = "false"
                ret.ErrorMessage = "Exception : " & ex.Message
            End Try
            Return ret
        End Function

        Private Shared Function CheckEndService(ByVal CounterID As String, ByVal MobileNo As String, ByVal QueueNo As String) As Boolean
            Dim ret As Boolean = False
            Try
                Dim sql As String = "select id"
                sql += " from TB_COUNTER_QUEUE "
                sql += " where datediff(D,GETDATE(),service_date)=0 "
                sql += " and queue_no = '" & QueueNo & "' "
                sql += " and counter_id = '" & CounterID & "'"
                sql += " and customer_id = '" & MobileNo & "' and status = 3"
                Dim dt As DataTable = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    ret = True
                End If
                dt.Dispose()
            Catch ex As Exception
                ret = False
            End Try
            Return ret
        End Function

        Public Shared Function EndService(ByVal UserID As String, ByVal CounterID As String, ByVal QueueNo As String, ByVal MobileNo As String, ByVal EndItemID As String, ByVal IsFirstItem As String) As String
            Dim ret As String = ""
            Dim shTrans As New TransactionDB
            Try
                Dim vDateNow As String = "'" & SqlDB.GetDateNowFromDB(shTrans.Trans).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
                Dim sql As String = "update TB_COUNTER_QUEUE "
                'sql += " set status = 3,end_time = dateadd(SECOND," & TimeStamp & ",start_time),"
                sql += " set status = 3,end_time = " & vDateNow & ","
                sql += " [user_id] = '" & UserID & "'"
                sql += " where datediff(D,GETDATE(),service_date)=0 "
                sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' "
                sql += " and counter_id='" & CounterID & "' and status = 2 and item_id = '" & EndItemID & "'"
                ExecuteShopNonQuery(sql, shTrans)

                InsertLog(QueueNo, MobileNo, UserID, EndItemID, CounterID, 3, "", shTrans, vDateNow)
                sql = "declare @EndTime as datetime;select @EndTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "')" & vbCrLf
                sql += " update tb_counter_queue "
                sql += " set assign_time = @EndTime,call_time = @EndTime,start_time = @EndTime "
                sql += " where datediff(d,getdate(),service_date)=0 "
                sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' "
                sql += " and counter_id = '" & CounterID & "' and status = 2"
                ExecuteShopNonQuery(sql, shTrans)

                shTrans.CommitTransaction()


                sql = "select id, customertype_id,datediff(s,start_time,getdate()) diff_time,datediff(s,start_time,end_time) ht "
                sql += " from tb_counter_queue "
                sql += " where datediff(d,getdate(),service_date) = 0 "
                sql += " and customer_id = '" & MobileNo & "' "
                sql += " and queue_no = '" & QueueNo & "' and item_id = '" & EndItemID & "'"
                Dim dt As New DataTable
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    ret = "true|" & CalServeTime(dt.Rows(0)("ht"))

                    If Convert.IsDBNull(dt.Rows(0)("customertype_id")) = False Then
                        If dt.Rows(0)("customertype_id") = "3" Then
                            If IsFirstItem = "true" Then
                                'คืน Slot เมื่อมีการ End Service แรก
                                UpdateAppointmentSlot(MobileNo, QueueNo)
                            End If

                            sql = "select appointment_job_id,customer_id,queue_no "
                            sql += " from TB_APPOINTMENT_CUSTOMER "
                            sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                            sql += " and customer_id = '" & MobileNo & "' "
                            sql += " and queue_no='" & QueueNo & "' "
                            Dim aDt As New DataTable
                            aDt = GetShopDataTable(sql)
                            If aDt.Rows.Count > 0 Then
                                'Update Appointment Job To DC
                                UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "3")

                                'Update Siebel when End Queue
                                Dim dtS As DataTable = GetSiebelData(MobileNo, "2", "Registered")
                                If dtS.Rows.Count > 0 Then
                                    Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                                    Dim EndDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                                    'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                                    System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                                      Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                                      chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                                      sslerror As System.Net.Security.SslPolicyErrors) True
                                    Try
                                        Dim ws As New ShopWebServiceENG
                                        ws.SiebelUpdateToComplete(MobileNo, StartSlot, EndDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                                        ws = Nothing
                                    Catch ex As Exception
                                        Engine.Common.FunctionEng.SaveShopErrorLog("TabletWebServiceENG.EndService", "Don''t Update Activity to Complete Mobile No. " & MobileNo & ex.Message)
                                        ret = "false|" & "QueueSharp.frmEndByService.CheckEndService : Don''t Update Activity to Complete Mobile No. " & MobileNo & ex.Message
                                    End Try
                                End If
                            End If
                            aDt.Dispose()
                        End If
                    End If
                End If
                dt.Dispose()
            Catch ex As Exception
                shTrans.RollbackTransaction()
                ret = "false|" & ex.Message
            End Try
            Return ret
        End Function

        Public Shared Function EndQueueLastService(ByVal CounterID As String, ByVal QueueNo As String, ByVal MobileNo As String) As String
            Dim ret As String = ""
            Dim shTrans As New TransactionDB
            Try
                'Update Status ของ Service ที่ไม่ได้ทำกับ User คนนี้
                Dim Sql As String = "declare @AssignTime as datetime;select @AssignTime = (select MAX(end_time) from TB_COUNTER_QUEUE where DATEDIFF(d,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "')" & vbCrLf
                Sql &= "update TB_COUNTER_QUEUE set status = 1,assign_time = @AssignTime,start_time = NULL,end_time = NULL,[user_id] = 0,flag = '' where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "' and status = 2"
                ExecuteShopNonQuery(Sql, shTrans)

                Sql = "update TB_COUNTER_QUEUE set flag = '' where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "'"
                ret = ExecuteShopNonQuery(Sql, shTrans)
                If ret = True Then
                    ClearUnitDisplay(CounterID, False, shTrans)
                    ClearMainDisplay(CounterID, shTrans)
                    shTrans.CommitTransaction()
                    ret = "true"
                Else
                    shTrans.RollbackTransaction()
                    ret = "false|" & "Error " & Sql
                End If
            Catch ex As Exception
                shTrans.RollbackTransaction()
                ret = "false|" & ex.Message
            End Try

            Return ret

        End Function

        'Public Shared Function TransferQueue()
        '    Dim ret As String = ""
        '    Try
        '        Dim num As Int32 = 0
        '        For i = 0 To dt_Button.Rows.Count - 1
        '            For j As Int32 = 0 To FLP.Controls.Count - 1
        '                If FLP.Controls(j).Name = dt_Button.Rows(i).Item("item_id").ToString Then
        '                    If FLP.Controls(j).Text <> "End" And FLP.Controls(j).Text <> "None" Then
        '                        num = num + 1
        '                    End If
        '                End If
        '            Next
        '        Next

        '        If num = 0 Then
        '            showNotify("Attention", "Cannot Transfer Queue")
        '            Exit Function
        '        End If

        '        If ServiceEnd.Trim <> "" Then
        '            Dim sql As String = ""
        '            sql = "update tb_counter_queue "
        '            sql += " set combo_item_end = '" & ServiceEnd & "' "
        '            sql += " where datediff(d,getdate(),service_date)=0 and item_id in (" & ServiceEnd & ")"
        '            executeSQL(sql)
        '            'If CurrDB = "MAIN" Then
        '            '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
        '            'End If
        '        End If
        '        InsertLog(lblQueue.Text, lblCustomerID.Text, myUser.user_id, 0, myUser.counter_id, 1, "Transfer", Nothing, vDateNow, CurrDB)

        '        'ถ้า Transfer
        '        ExitForm(True, CurrDB)
        '        Threading.Thread.Sleep(3000)
        '        QM.WriteTextQueueID("")
        '        QM.CloseQM()
        '    Catch ex As Exception

        '    End Try
        '    Return ret
        'End Function

        Public Shared Function GetDateNow() As String
            Dim ret As String
            Try
                ret = SqlDB.GetDateNowFromDB(Nothing).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))
            Catch ex As Exception
                ret = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))
            End Try
            Return ret
        End Function

        Private Shared Function GetSiebelData(ByVal MobileNo As String, ByVal AppointmentStatus As String, ByVal SiebelStatus As String) As DataTable
            'Update Siebel Activity  (ไม่รู้จะทำไปทำไม เสียเวลาเปล่า)
            Dim sqlS As String = "select top 1 start_slot,CONVERT(varchar(5),start_slot,114) as app_time,item_id,item_name,item_name_th,siebel_activity_id, siebel_desc "
            sqlS += " from TB_APPOINTMENT_CUSTOMER "
            sqlS += " inner join TB_ITEM on TB_APPOINTMENT_CUSTOMER.item_id = TB_ITEM.id  "
            sqlS += " where DateDiff(D, GETDATE(), app_date) = 0 And TB_ITEM.active_status = '1' "
            sqlS += " and TB_APPOINTMENT_CUSTOMER.active_status = " & AppointmentStatus & " "
            sqlS += " and customer_id = '" & MobileNo & "' "
            sqlS += " and siebel_status = '" & SiebelStatus & "'"
            sqlS += " order by item_wait,item_time,item_order"
            Dim dtS As New DataTable
            dtS = GetShopDataTable(sqlS)

            Return dtS
        End Function

        'Update status ของ QisDB.TB_APPOINTMENT_JOB
        Private Shared Sub UpdateAppointmentJob(ByVal AppointJobID As Long, ByVal ActiveStatus As String)
            Try
                Dim ws As New ShopWebServiceENG
                ws.UpdateAppointmentJobStatus(AppointJobID, ActiveStatus)
                ws = Nothing
            Catch ex As Exception
                Engine.Common.FunctionEng.SaveShopErrorLog("TabletWebServiceENG.UpdateAppointmentJob", "TabletWebServiceENG.UpdateAppointmentJob : " & ex.Message)
            End Try
        End Sub

        'เช็คสถานะของลูกค้าว่าถูก Update ไปก่อนหน้านี้แล้วหรือยัง
        Private Shared Function CheckCustomerStatus(ByVal QueueNo As String, ByVal MobileNo As String, ByVal Status As Integer) As Boolean
            Dim sql As String = ""
            Dim dt As New DataTable
            '@QueueNo as int,
            '@CustomerID as varchar,
            '@Status as int
            sql = "exec SP_CheckCustomerStatus '" & QueueNo & "','" & MobileNo & "'," & Status
            dt = GetShopDataTable(sql)
            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If

        End Function

        Private Shared Sub SiebelUpdateToCancel(ByVal MobileNo As String, ByVal CustomerTypeID As String)
            'Update Siebel To Cancel กรณีเป็นคิวจอง
            If MobileNo.Trim <> "" And CustomerTypeID = "3" Then
                Dim dtS As DataTable = GetSiebelData(MobileNo, "6", "Registered")
                If dtS.Rows.Count > 0 Then

                    'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                      Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                      chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                      sslerror As System.Net.Security.SslPolicyErrors) True

                    Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                    Dim CancelDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                    Try
                        Dim ws As New ShopWebServiceENG
                        ws.SiebelUpdateToCancel(MobileNo, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                        ws = Nothing
                    Catch ex As Exception
                        Engine.Common.FunctionEng.SaveShopErrorLog("TabletWebServiceENG.SiebelUpdateToCancel", "Exception : " & ex.Message & "   Don''t Update Siebel Activity To Cancel Mobile No. " & MobileNo & "'")
                    End Try
                End If
                dtS.Dispose()
            End If
        End Sub

        Private Shared Sub UpdateAppointmentSlot(ByVal MobileNo As String, ByVal QueueNo As String)
            Dim sql As String = ""
            Dim dt As New DataTable
            Try
                sql = "select hold "
                sql += " from TB_COUNTER_QUEUE "
                sql += " left join TB_CUSTOMERTYPE on TB_COUNTER_QUEUE.customertype_id = TB_CUSTOMERTYPE.id "
                sql += " where DATEDIFF(D,GETDATE(),service_date) = 0 "
                sql += " and TB_CUSTOMERTYPE.app = 1 "
                sql += " and queue_no = '" & QueueNo & "' and customer_id = '" & MobileNo & "'"
                dt = GetShopDataTable(sql)
                If dt.Rows.Count > 0 Then
                    Dim Hold As DateTime = Convert.ToDateTime(dt.Rows(0).Item("hold"))
                    sql = "declare @Slot as int; " & vbNewLine
                    sql += " select @Slot = (select top 1 slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,GETDATE(),start_slot)=0); " & vbNewLine
                    sql += " select *,@Slot as slot  " & vbNewLine
                    sql += " from TB_APPOINTMENT_CUSTOMER " & vbNewLine
                    sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 " & vbNewLine
                    sql += " and customer_id = '" & MobileNo & "'"
                    dt = GetShopDataTable(sql)
                    If dt.Rows.Count > 0 Then
                        For i As Int32 = 1 To dt.Rows.Count
                            If i > 1 Then
                                Hold = DateAdd(DateInterval.Minute, CInt(dt.Rows(i - 1).Item("slot")), Hold)
                            End If
                            sql = "update TB_APPOINTMENT_SLOT "
                            sql += " set in_use = in_use - 1 "
                            sql += " where DATEDIFF(D,GETDATE(),app_date)=0 "
                            sql += " and CONVERT(varchar(5),slot_time,114) = '" & Hold.ToString("HH:mm") & "'"
                            ExecuteShopNonQuery(sql)
                        Next
                    End If
                End If
                dt.Dispose()
            Catch ex As Exception : End Try

            dt.Dispose()
        End Sub

        'Insert Log กรณีที่มีการเปลี่ยนแปลงสถานะของคิว
        Private Shared Sub InsertLog(ByVal QueueNo As String, ByVal CustomerID As String, ByVal UserID As Integer, ByVal ItemID As Integer, ByVal CounterID As Integer, ByVal Status As Integer, ByVal Flag As String, ByVal _Trans As TransactionDB, ByVal vDateNow As String)
            Dim sql As String = ""
            Dim dt As New DataTable
            Dim RowID As Int32 = 0
            Dim vQID As Int32 = 0
            sql = "select top 1 id from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and customer_id = '" & CustomerID & "' and queue_no = '" & QueueNo & "' and item_id = " & ItemID
            dt = GetShopDataTable(sql, _Trans)
            If dt.Rows.Count > 0 Then
                vQID = dt.Rows(0).Item("id")
            End If
            dt.Dispose()

            sql = " insert into TB_LOGFILE(id,cq_id,log_date,queue_no,customer_id,user_id,item_id,counter_id,status,flag) "
            sql += " values((select isnull(max(id),0) +1 from TB_LOGFILE ),'" & vQID & "'," & vDateNow & ",'" & QueueNo & "','" & CustomerID & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & Flag & "')"
            ExecuteShopNonQuery(sql, _Trans)
        End Sub
#End Region

#Region "Main Display"
        Private Shared Function ClearMainDisplay(ByVal CounterID As Integer, ByVal _Trans As TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim sql As String = "delete from TB_MAINDISPLAY "
            sql += " where DATEDIFF(D,GETDATE(),order_time)=0 "
            sql += " and counter_code=(select counter_code from tb_counter where id='" & CounterID & "')"
            Return ExecuteShopNonQuery(sql, _Trans)
        End Function

        Private Shared Function InsertMainDisplay(ByVal UserID As String, ByVal QueueNo As String, ByVal CounterID As Integer, ByVal StartTime As String, ByVal StatusID As Integer, ByVal _Trans As TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim sql As String = ""
            ClearMainDisplay(CounterID, _Trans)

            sql = "insert into TB_MAINDISPLAY(create_by,create_date,queue_no,counter_code,order_time,status_id) "
            sql += " values ('" & UserID & "',getdate(),'" & QueueNo & "',"
            sql += "(select counter_code from tb_counter where id='" & CounterID & "'),'" & StartTime & "','" & StatusID & "')"
            If _Trans Is Nothing Then
                _Trans = New TransactionDB

                ret = ExecuteShopNonQuery(sql, _Trans)
                If ret = True Then
                    _Trans.CommitTransaction()
                Else
                    _Trans.RollbackTransaction()
                End If
            Else
                ret = ExecuteShopNonQuery(sql, _Trans)
            End If

            Return ret
        End Function
#End Region

#Region "UNIT Display"
        Private Shared Sub ClearUnitDisplay(ByVal CounterID As String, ByVal Close As Boolean, ByVal _Trans As TransactionDB)
            Dim UnitdisplayID As String = ""
            UnitdisplayID = GetUnitdisplayID(CounterID, _Trans)
            If UnitdisplayID = "" Then Exit Sub
            Dim sql As String = ""
            If Close = True Then
                sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
                sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','',4,1)"
                ExecuteShopNonQuery(sql, _Trans)
            Else
                sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
                sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','',2,1)"
                ExecuteShopNonQuery(sql, _Trans)
            End If
        End Sub

        Private Shared Sub PauseUnitDisplay(ByVal lPara As TabletLogonUserPara, ByVal _Trans As TransactionDB)
            Dim UnitdisplayID As String = ""
            UnitdisplayID = GetUnitdisplayID(lPara.COUNTER_ID, _Trans)
            If UnitdisplayID = "" Then Exit Sub
            Dim sql As String = ""
            sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','',3,1)"
            ExecuteShopNonQuery(sql, _Trans)
        End Sub

        Private Shared Sub CallUnitDisplay(ByVal CounterID As String, ByVal Queue As String, ByVal _Trans As TransactionDB)
            Dim UnitdisplayID As String = ""
            UnitdisplayID = GetUnitdisplayID(CounterID, _Trans)
            If UnitdisplayID = "" Then Exit Sub
            Dim sql As String = ""
            sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','" & Queue & "','',0,1)"
            ExecuteShopNonQuery(sql, _Trans)
        End Sub

        Private Shared Sub CallSpeaker(ByVal CounterID As String, ByVal QueueNo As String, ByVal sTrans As TransactionDB)
            Dim dt As New DataTable
            Dim sql As String = ""
            sql = "select queue_no from TB_speaker where datediff(d,getdate(),call_date)=0 and queue_no ='" & QueueNo & "' and counter_id ='" & CounterID & "' and status = '0'"
            dt = GetShopDataTable(sql, sTrans)
            If dt.Rows.Count = 0 Then
                Dim cDt As DataTable = GetShopDataTable("select counter_name from tb_counter where id='" & CounterID & "'", sTrans)
                If cDt.Rows.Count > 0 Then
                    sql = "insert into TB_speaker(id,queue_no, counter_id, counter_name, call_date, status, nationality) "
                    sql += " values((select isnull(MAX(id) + 1,1) as id from TB_speaker),'" & QueueNo & "','" & CounterID & "','" & cDt.Rows(0)("counter_name") & "',getdate(),'0','THAI')"
                    ExecuteShopNonQuery(sql, sTrans)
                End If
                cDt.Dispose()
            End If
            dt.Dispose()
        End Sub

        Private Shared Sub ServeUnitDisplay(ByVal CounterID As String, ByVal QueueNo As String, ByVal shTrans As TransactionDB)
            Dim UnitdisplayID As String = ""
            UnitdisplayID = GetUnitdisplayID(CounterID, shTrans)
            If UnitdisplayID = "" Then Exit Sub
            Dim sql As String = ""
            sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','" & QueueNo & "','',1,1)"
            ExecuteShopNonQuery(sql, shTrans)
        End Sub

        'Private Shared Sub ShowUserUnitDisplay(ByVal txt As String)
        '    Dim UnitdisplayID As String = ""
        '    UnitdisplayID = GetUnitdisplayID()
        '    If UnitdisplayID = "" Then Exit Sub
        '    Dim sql As String = ""
        '    sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
        '    sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','" & txt & "',5,1)"

        '    If CreateTransaction("QueueModule_ShowUserUnitDisplay") = True Then
        '        If executeSQL(sql, shTrans, True) = True Then
        '            CommitTransaction()
        '        Else
        '            RollbackTransaction()
        '        End If
        '    End If
        'End Sub

        Private Shared Function GetUnitdisplayID(ByVal CounterID As String, ByVal _Trans As TransactionDB) As String
            Dim ret As String = ""
            Dim sql As String = ""
            Dim dt As New DataTable
            If CounterID <> "" Then
                sql = "select unitdisplay from tb_counter where id = '" & CounterID & "' and active_status = 1"
                dt = GetShopDataTable(sql, _Trans)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("unitdisplay").ToString <> "0" Then
                        ret = dt.Rows(0).Item("unitdisplay").ToString
                    End If
                End If
            End If
            dt.Dispose()

            Return ret
        End Function
#End Region
    End Class
End Namespace

