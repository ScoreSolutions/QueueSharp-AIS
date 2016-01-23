Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE
Imports CenLinqDB.TABLE
Imports CenParaDB.TABLE
Imports Engine.Common
Imports System.Data

Namespace Configuration
    Public Class ShopSettingENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function GetTbSetting(ByVal ShopID As String) As DataTable
            Dim dt As New DataTable
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.GetTbSetting")
            If shTrans.Trans IsNot Nothing Then
                Dim sLnq As New TbSettingShLinqDB
                dt = sLnq.GetDataList("1=1", "config_name", shTrans.Trans)
                sLnq = Nothing
            End If

            Return dt
        End Function

        Public Function SaveShopTbSetting(ByVal ShopID As String, ByVal ConfigValue As String, ByVal ConfigName As String) As Boolean
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.SaveShopTbSetting")
            If shTrans.Trans IsNot Nothing Then
                Dim sLnq As New TbSettingShLinqDB
                Dim sql As String = "update tb_setting "
                sql += " set config_value = '" & ConfigValue & "',"
                sql += " update_date=getdate()"
                sql += " where config_name = '" & ConfigName & "'"
                ret = sLnq.UpdateBySql(sql, shTrans.Trans)

                If ret = True Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                    _err = "ShopSettingEng.SaveShopTbSetting : " & sLnq.ErrorMessage
                End If
                sLnq = Nothing
            End If

            Return ret
        End Function

        Public Function SaveShopHoldReason(ByVal p As ShParaDB.TABLE.TbHoldReasonShParaDB, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New ShLinqDB.TABLE.TbHoldReasonShLinqDB
            If p.MASTER_HOLDREASONID <> 0 Then
                lnq.ChkDataByWhere("master_holdreasonid='" & p.MASTER_HOLDREASONID & "'", shTrans.Trans)
            End If
            If lnq.ID = 0 Then
                lnq.ChkDataByWhere("name='" & p.NAME.Trim & "'", shTrans.Trans)
            End If
            lnq.MASTER_HOLDREASONID = p.MASTER_HOLDREASONID
            lnq.NAME = p.NAME
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.PRODUCTIVE = p.PRODUCTIVE

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                ret = lnq.InsertData(LoginName, shTrans.Trans)
            End If
            If ret = False Then
                _err = "ShopSettingENG.SaveShopHoldReason : " & lnq.ErrorMessage
            End If

            Return ret
        End Function

        Public Function SaveShopLogoutReason(ByVal p As ShParaDB.TABLE.TbLogoutReasonShParaDB, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New ShLinqDB.TABLE.TbLogoutReasonShLinqDB
            If p.MASTER_LOGOUTREASONID <> 0 Then
                lnq.ChkDataByWhere("master_holdreasonid='" & p.MASTER_LOGOUTREASONID & "'", shTrans.Trans)
            End If
            If lnq.ID = 0 Then
                lnq.ChkDataByWhere("name='" & p.NAME.Trim & "'", shTrans.Trans)
            End If
            lnq.MASTER_LOGOUTREASONID = p.MASTER_LOGOUTREASONID
            lnq.NAME = p.NAME
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.PRODUCTIVE = p.PRODUCTIVE

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                ret = lnq.InsertData(LoginName, shTrans.Trans)
            End If
            If ret = False Then
                _err = "ShopSettingENG.SaveShopLogoutReason : " & lnq.ErrorMessage
            End If

            Return ret
        End Function

        Public Function SaveShopUser(ByVal p As ShParaDB.TABLE.TbUserShParaDB, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If
            lnq.TITLE_ID = p.TITLE_ID
            lnq.USER_CODE = p.USER_CODE
            lnq.FNAME = p.FNAME
            lnq.LNAME = p.LNAME
            lnq.POSITION = p.POSITION
            lnq.GROUP_ID = p.GROUP_ID
            lnq.USERNAME = p.USERNAME
            lnq.PASSWORD = p.PASSWORD
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.COUNTER_ID = 0
            lnq.ITEM_ID = 0

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                ret = lnq.InsertData(LoginName, shTrans.Trans)
            End If
            If ret = True Then
                p.ID = lnq.ID
            Else
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function SaveShopUserSkill(ByVal p As ShParaDB.TABLE.TbUserSkillShParaDB, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New ShLinqDB.TABLE.TbUserSkillShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If

            lnq.USER_ID = p.USER_ID
            lnq.SKILL_ID = p.SKILL_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                ret = lnq.InsertData(LoginName, shTrans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function GetShopUserService(ByVal ShopID As Long) As DataTable
            Dim sql As String = "select id, item_code, item_code code,item_name name"
            sql += " from tb_item "
            sql += " where active_status='1'"
            sql += " order by item_order"
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopSettingENG.GetShopUserService")
            Return dt
        End Function

        Public Function GetShopUserSkill(ByVal ShopID As Long) As DataTable
            Dim sql As String = ""
            sql = "select a.user_id,b.item_id,d.item_code "
            sql += " from TB_USER_SKILL a "
            sql += " left join TB_SKILL_ITEM b on a.skill_id = b.skill_id "
            sql += " left join TB_SKILL c on a.skill_id = c.id "
            sql += " left join TB_ITEM d on b.item_id = d.id "
            sql += " where c.active_status = 1 and d.active_status = 1 "
            sql += " group by a.user_id,b.item_id,d.item_code"

            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopSettingENG.GetShopUserSkill")
            Return dt
        End Function

        Public Function GetShopUserByDate(ByVal ShopID As Long, ByVal vDate As Date) As DataTable
            'Dim sql As String = "select uss.user_id, uss.item_id,t.item_code, uss.priority, u.id ,ti.title_name + u.fname + ' ' + u.lname as fullname"
            'sql += " from tb_user_service_schedule uss"
            'sql += " inner join tb_item t on t.id=uss.item_id"
            'sql += " inner join tb_user u on u.id=uss.user_id"
            'sql += " left join tb_title ti on ti.id=u.title_id"
            'sql += " where t.active_status=1 and uss.active_status=1"
            'sql += " and convert(varchar(8),uss.service_date,112)='" & vDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"

            Dim sql As String = "select user_id, item_id,item_code, priority, id ,title_name + fname + ' ' + lname as fullname from ("
            sql += " select  u.id  ,ti.title_name ,fname,lname from tb_user u"
            sql += " left join tb_title ti on u.title_id=ti.id "
            sql += " where active_status='1') TBU "
            sql += "  Left Join "
            sql += " (select uss.user_id, uss.item_id,t.item_code, uss.priority"
            sql += " from  tb_user_service_schedule uss "
            sql += " left join tb_item t on t.id=uss.item_id "
            sql += " where t.active_status=1 and uss.active_status=1 "
            sql += " and convert(varchar(8),uss.service_date,112)='" & vDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            sql += ") TBUS ON TBU.id=TBUS.user_id order by fname,lname"
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopSettingENG.GetShopUserByDate")
            Return dt
        End Function

        Public Function GetShopUserSkillAppoint(ByVal ShopID As Long) As DataTable
            Dim sql As String = "select user_id"
            sql += vbCrLf & " from TB_USER_SKILL "
            sql += vbCrLf & " left join TB_SKILL on TB_USER_SKILL.skill_id = TB_SKILL.id"
            sql += vbCrLf & " where TB_SKILL.active_status = 1 and TB_SKILL.appointment = 1 "

            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopSettingENG.GetShopUserSkillAppoint")
            Return dt
        End Function

        Public Function GetShopUserAppointByDate(ByVal ShopID As Long, ByVal vDate As Date) As DataTable
            Dim sql As String = "select user_id"
            sql += vbCrLf & " from TB_APPOINTMENT_USER "
            sql += " where convert(varchar(8),app_date,112)='" & vDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"

            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopSettingENG.GetShopUserAppointByDate")
            Return dt
        End Function

        Public Sub DeleteShopAssignment(ByVal DateFrom As String, ByVal DateTo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
            Dim Sql As String = "delete from TB_USER_SERVICE_SCHEDULE where convert(varchar(8),service_date,112) between '" & DateFrom & "' and '" & DateTo & "'"
            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(Sql, shTrans.Trans)

            Sql = "delete from TB_APPOINTMENT_USER where convert(varchar(8),app_date,112) between '" & DateFrom & "' and '" & DateTo & "'"
            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(Sql, shTrans.Trans)
        End Sub

        Public Function SaveShopUserServiceSchedule(ByVal p As TbUserServiceScheduleShParaDB, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Try
                Dim ussLnq As New TbUserServiceScheduleShLinqDB
                ussLnq.SERVICE_DATE = p.SERVICE_DATE
                ussLnq.USER_ID = p.USER_ID
                ussLnq.ITEM_ID = p.ITEM_ID
                ussLnq.PRIORITY = p.PRIORITY
                ussLnq.ACTIVE_STATUS = p.ACTIVE_STATUS

                ret = ussLnq.InsertData(UserID, shTrans.Trans)
                If ret = False Then
                    _err = ussLnq.ErrorMessage
                End If
                ussLnq = Nothing
            Catch ex As Exception
                _err = ex.Message
            End Try

            Return ret
        End Function

        Public Function SaveShopAppointmentUser(ByVal p As TbAppointmentUserShParaDB, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Try
                Dim auLnq As New TbAppointmentUserShLinqDB
                auLnq.APP_DATE = p.APP_DATE
                auLnq.USER_ID = p.USER_ID
                ret = auLnq.InsertData(UserID, shTrans.Trans)
                If ret = False Then
                    _err = auLnq.ErrorMessage
                End If
            Catch ex As Exception
                _err = ex.Message
            End Try

            Return ret
        End Function

        Public Sub New()
            _err = ""
        End Sub

        Public Function GetTbSettingOpenAndCloseTime(ByVal ShopID As String) As DataTable
            Dim dt As New DataTable
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.GetTbSettingOpenAndCloseTime")
            If shTrans.Trans IsNot Nothing Then
                Dim sLnq As New TbSettingShLinqDB
                dt = sLnq.GetDataList(" config_name in ('s_open','s_close') ", "config_name desc", shTrans.Trans)
                sLnq = Nothing
            End If

            Return dt
        End Function

        Public Function SaveAutoForce(ByVal ShopID As String, ByVal UserName As String, ByVal p As TbForceScheduleShParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.SaveAutoForce")
            Dim lnq As New TbForceScheduleShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If

            lnq.FORCE_DATE = p.FORCE_DATE
            lnq.START_TIME = p.START_TIME
            lnq.END_TIME = p.END_TIME
            lnq.START_SLOT = p.START_SLOT
            lnq.END_SLOT = p.END_SLOT
            lnq.SLOT = p.SLOT
            lnq.WAIT = p.WAIT

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, shTrans.Trans)
            Else
                ret = lnq.InsertData(UserName, shTrans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                shTrans.RollbackTransaction()
            Else
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function DeleteAutoForce(ByVal ForceDateF As String, ByVal ForceDateT As String, ByVal ShopID As String)
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.DeleteAutoForce")
            Try
                Dim uSql As String = "Delete From TB_FORCE_SCHEDULE "
                uSql += " Where convert(varchar(8),Force_Date,112)  Between '" & ForceDateF & "' AND '" & ForceDateT & "'"
                ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(uSql, shTrans.Trans)
                shTrans.CommitTransaction()
                ret = True
            Catch ex As Exception
                _err = ex.Message.ToString
                shTrans.RollbackTransaction()
                ret = False
            End Try

            Return ret
        End Function

        Public Sub DeleteAppointmentSchedule(ByVal AppDateFrom As String, ByVal AppDateTo As String, ByVal ShopID As Long)
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.DeleteAppointmentSchedule")
            Try
                Dim uSql As String = "Delete From TB_APPOINTMENT_SCHEDULE "
                uSql += " Where convert(varchar(8),app_date,112)  Between '" & AppDateFrom & "' AND '" & AppDateFrom & "'"
                ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(uSql, shTrans.Trans)
                shTrans.CommitTransaction()
            Catch ex As Exception
                _err = ex.Message.ToString
                shTrans.RollbackTransaction()
            End Try
        End Sub

        Public Function SaveAppointmentSchedule(ByVal p As TbAppointmentScheduleShParaDB, ByVal UserName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbAppointmentScheduleShLinqDB
            lnq.APP_DATE = p.APP_DATE
            lnq.CAPACITY = p.CAPACITY
            lnq.START_TIME = p.START_TIME
            lnq.END_TIME = p.END_TIME
            lnq.GAP = p.GAP
            lnq.START_SLOT = p.START_SLOT
            lnq.END_SLOT = p.END_SLOT
            lnq.SLOT = p.SLOT

            ret = lnq.InsertData(UserName, shTrans.Trans)
            If ret = False Then
                _err = lnq.ErrorMessage
            End If

            Return ret
        End Function

        Public Function DeleteAppointmentService(ByVal vDate As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Try
                Dim dSql As String = "delete from TB_APPOINTMENT_ITEM where convert(varchar(8),app_date,112) = '" & vDate & "'"
                ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, shTrans.Trans)
                ret = True
            Catch ex As Exception
                ret = False
                _err = ex.Message
            End Try

            Return ret
        End Function

        Public Function SaveAppointmentService(ByVal p As ShParaDB.TABLE.TbAppointmentItemShParaDB, ByVal UserID As Integer, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New ShLinqDB.TABLE.TbAppointmentItemShLinqDB
            lnq.APP_DATE = p.APP_DATE
            lnq.ITEM_ID = p.ITEM_ID

            ret = lnq.InsertData(UserID, shTrans.Trans)
            If ret = False Then
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function


        Public Function DeleteAppointmentSlot(ByVal vDate As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Try
                Dim dSql As String = "delete from TB_APPOINTMENT_SLOT where CONVERT(varchar(8),app_date,112)  = '" & vDate & "'"
                ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, shTrans.Trans)
                ret = True
            Catch ex As Exception
                ret = False
                _err = ex.Message
            End Try

            Return ret
        End Function

        Public Function DeleteUserSkill(ByVal UserID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Try
                Dim dSql As String = "delete TB_USER_SKILL where user_id='" & UserID & "'"
                ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, shTrans.Trans)
                ret = True
            Catch ex As Exception
                ret = False
                _err = ex.Message
            End Try

            Return ret
        End Function

        Public Function SaveAppointmentSlot(ByVal p As ShParaDB.TABLE.TbAppointmentSlotShParaDB, ByVal UserName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbAppointmentSlotShLinqDB
            lnq.APP_DATE = p.APP_DATE
            lnq.CAPACITY = p.CAPACITY
            lnq.SLOT_TIME = p.SLOT_TIME
            lnq.IN_USE = p.IN_USE

            ret = lnq.InsertData(UserName, shTrans.Trans)
            If ret = False Then
                _err = lnq.ErrorMessage
            End If

            Return ret
        End Function

        Public Function GetShopSkill(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim lnq As New TbSkillShLinqDB
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.GetShopSkill")
            dt = lnq.GetListBySql("select id,skill from TB_SKILL where active_status = 1 order by skill", shTrans.Trans)
            lnq = Nothing
            shTrans.CommitTransaction()

            Return dt
        End Function

        'เช็คข้อมูลซ้ำ
        Public Function CheckShopDataDuplicate(ByVal ShopID As Long, ByVal TableName As String, ByVal FieldName As String, ByVal Value_Duplicate As String, ByVal id As String) As Boolean
            Try
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select * from " & TableName & " where " & FieldName & " = '" & Value_Duplicate & "' and id <> '" & id & "' "
                dt = FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopSettingENG.CheckShopDataDuplicate")
                If dt.Rows.Count > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
            End Try
            Return False
        End Function
    End Class
End Namespace

