Imports CenLinqDB.TABLE
Imports CenParaDB.TABLE

Namespace Configuration
    Public Class ShopEventScheduleSettingENG
        Dim _err As String = ""

        Public ReadOnly Property ErrMessage() As String
            Get
                Return _err.Trim
            End Get
        End Property

        Public Function GetSysconfigDT() As DataTable
            Dim dt As New DataTable
            Dim lnq As New SysconfigCenLinqDB
            dt = lnq.GetDataList("1=1", "", Nothing)
            lnq = Nothing
            Return dt
        End Function

        Public Function SaveSysconfig(ByVal ConfigValue As String, ByVal ConfigName As String, ByVal UpdateBy As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim sLnq As New SysconfigCenLinqDB
                Dim sql As String = "update sysconfig "
                sql += " set config_value = '" & ConfigValue & "',"
                sql += " update_date=getdate(), update_by='" & UpdateBy & "'"
                sql += " where config_name = '" & ConfigName & "'"
                ret = sLnq.UpdateBySql(sql, trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                    _err = "ShopEventScheduleSettingENG.SaveSysconfig : " & sLnq.ErrorMessage
                End If
                sLnq = Nothing
            End If

            Return ret
        End Function

        Public Function SaveCfgSchedMainDisplay(ByVal p As CenParaDB.TABLE.TbCfgSwSchedMainDisplayCenParaDB, ByVal UserName As String) As Boolean
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

            Dim ret As Boolean = False
            Dim lnq As New CenLinqDB.TABLE.TbCfgSwSchedMaindisplayCenLinqDB
            lnq.ChkDataByEVENT_DATE_SHOP_ID(p.EVENT_DATE, p.SHOP_ID, trans.Trans)

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.M_RETARDATION_VDO = p.M_RETARDATION_VDO
            lnq.EVENT_STATUS = p.EVENT_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SaveCfgSchedHoldReason(ByVal p As CenParaDB.TABLE.TbCfgHoldReasonCenParaDB, ByVal UserName As String) As Boolean
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim ret As Boolean = False

            Dim lnq As New CenLinqDB.TABLE.TbCfgHoldReasonCenLinqDB
            lnq.ChkDataByEVENT_DATE_MASTER_HOLDREASON_ID_SHOP_ID(p.EVENT_DATE, p.MASTER_HOLDREASON_ID, p.SHOP_ID, trans.Trans)

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.NAME = p.NAME
            lnq.PRODUCTIVE = p.PRODUCTIVE
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.MASTER_HOLDREASON_ID = p.MASTER_HOLDREASON_ID
            lnq.EVENT_STATUS = p.EVENT_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SaveCfgSchedLogoutReason(ByVal p As CenParaDB.TABLE.TbCfgLogoutReasonCenParaDB, ByVal UserName As String) As Boolean
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim ret As Boolean = False

            Dim lnq As New CenLinqDB.TABLE.TbCfgLogoutReasonCenLinqDB
            lnq.ChkDataByEVENT_DATE_MASTER_LOGOUTREASON_ID_SHOP_ID(p.EVENT_DATE, p.MASTER_LOGOUTREASON_ID, p.SHOP_ID, trans.Trans)

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.NAME = p.NAME
            lnq.PRODUCTIVE = p.PRODUCTIVE
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.MASTER_LOGOUTREASON_ID = p.MASTER_LOGOUTREASON_ID
            lnq.EVENT_STATUS = p.EVENT_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SaveShopFileByte(ByVal p As TbShopFileByteCenParaDB, ByVal LoginName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0
            Dim lnq As New TbShopFileByteCenLinqDB
            With lnq
                .TARGET_TYPE = p.TARGET_TYPE
                .FOLDER_NAME = p.FOLDER_NAME
                .FILE_NAME = p.FILE_NAME
                .FILE_BYTE = p.FILE_BYTE
                .CONVERT_STATUS = p.CONVERT_STATUS

                If .InsertData(LoginName, trans.Trans) = False Then
                    _err = .ErrorMessage
                    ret = 0
                Else
                    ret = lnq.ID
                End If
            End With
            Return ret
        End Function

        Public Function SaveShopFileSchedule(ByVal p As TbShopFileScheduleCenParaDB, ByVal LoginName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbShopFileScheduleCenLinqDB
            With lnq
                .EVENT_DATE = p.EVENT_DATE 'Engine.Common.FunctionEng.cStrToDate(EventDate)
                .SHOP_ID = p.SHOP_ID 'arrShopID(cntShop)
                .TRANSFER_STATUS = p.TRANSFER_STATUS
                .TB_SHOP_FILE_BYTE_ID = p.TB_SHOP_FILE_BYTE_ID
                ret = .InsertData(LoginName, trans.Trans)
                If ret = False Then
                    _err = .ErrorMessage
                    ret = False
                End If
            End With
            Return ret
        End Function

        Public Function UpdateFileScheduleTransferStatus(ByVal FileScheduleID As Long, ByVal TransferStatus As String) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New TbShopFileScheduleCenLinqDB
                lnq.GetDataByPK(FileScheduleID, trans.Trans)
                If lnq.ID <> 0 Then
                    lnq.TRANSFER_STATUS = TransferStatus
                    ret = lnq.UpdateByPK("UpdateFileScheduleTransferStatus", trans.Trans)
                    If ret = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                        _err = "UpdateFileScheduleTransferStatus : " & lnq.ErrorMessage
                    End If
                Else
                    trans.RollbackTransaction()
                    _err = "UpdateFileScheduleTransferStatus : " & lnq.ErrorMessage
                End If
                lnq = Nothing
            Else
                _err = "UpdateFileScheduleTransferStatus : " & trans.ErrorMessage
            End If
            Return ret
        End Function

        Public Function SaveCfgSchedQSharp(ByVal p As CenParaDB.TABLE.TbCfgSwSchedQsharpCenParaDB, ByVal UserName As String) As Boolean
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

            Dim ret As Boolean = False
            Dim lnq As New CenLinqDB.TABLE.TbCfgSwSchedQsharpCenLinqDB
            lnq.ChkDataByEVENT_DATE_SHOP_ID(p.EVENT_DATE, p.SHOP_ID, trans.Trans)

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.Q_REFRESH = p.Q_REFRESH
            lnq.Q_CON_LDAP = p.Q_CON_LDAP
            lnq.EVENT_STATUS = p.EVENT_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SaveCfgSchedKiosk(ByVal p As CenParaDB.TABLE.TbCfgSwSchedKioskCenParaDB, ByVal UserName As String) As Boolean
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

            Dim ret As Boolean = False
            Dim lnq As New CenLinqDB.TABLE.TbCfgSwSchedKioskCenLinqDB
            lnq.ChkDataByEVENT_DATE_SHOP_ID(p.EVENT_DATE, p.SHOP_ID, trans.Trans)

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.K_BEFORE = p.K_BEFORE
            lnq.K_LATE = p.K_LATE
            lnq.K_MAX_APPOINTMENT = p.K_MAX_APPOINTMENT
            lnq.K_BEFORE_APP = p.K_BEFORE_APP
            lnq.K_CANCEL = p.K_CANCEL
            lnq.K_DISABLE = p.K_DISABLE
            lnq.K_SERVE = p.K_SERVE
            lnq.K_REFRESH = p.K_REFRESH
            lnq.K_VDO = p.K_VDO
            lnq.K_LEN = p.K_LEN
            lnq.K_MOBILE1 = p.K_MOBILE1
            lnq.K_MOBILE2 = p.K_MOBILE2
            lnq.K_MOBILE3 = p.K_MOBILE3
            lnq.K_MOBILE4 = p.K_MOBILE4
            lnq.K_RETARDATION_VDO = p.K_RETARDATION_VDO
            lnq.EVENT_STATUS = p.EVENT_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
                Common.FunctionEng.SaveErrorLog("ShopEventScheduleSetttingENG.SaveCfgSchedKios", _err)
            End If
            lnq = Nothing

            Return ret
        End Function

#Region "Config User"
        Public Function SaveCfgUser(ByVal p As CenParaDB.TABLE.TbCfgUserCenParaDB, ByVal UserName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbCfgUserCenLinqDB
            lnq.ChkDataByEVENT_STATUS_SHOP_ID_USERNAME(p.EVENT_STATUS, p.SHOP_ID, UserName, trans.Trans)

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.TITLE_ID = p.TITLE_ID
            lnq.USER_CODE = p.USER_CODE
            lnq.FNAME = p.FNAME
            lnq.LNAME = p.LNAME
            lnq.POSITION = p.POSITION
            lnq.GROUP_ID = p.GROUP_ID
            lnq.USERNAME = p.USERNAME
            lnq.PASSWORD = p.PASSWORD
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.EVENT_STATUS = p.EVENT_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = True Then
                p.ID = lnq.ID
            End If
            lnq = Nothing

            Return ret
        End Function


        Public Function DeleteCfgUserSkill(ByVal CftUserID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_USER_SKILL where tb_cfg_user_id='" & CftUserID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try
            Return ret
        End Function


        Public Function SaveCfgUserSkill(ByVal p As CenParaDB.TABLE.TbCfgUserSkillCenParaDB, ByVal UserName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbCfgUserSkillCenLinqDB
            lnq.ChkDataBySKILL_ID_TB_CFG_USER_ID(p.SKILL_ID, p.TB_CFG_USER_ID, trans.Trans)
            lnq.TB_CFG_USER_ID = p.TB_CFG_USER_ID
            lnq.SKILL_ID = p.SKILL_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If

            If ret = False Then
                _err = lnq.ErrorMessage
            Else
                p.ID = lnq.ID
            End If
            lnq = Nothing

            Return ret
        End Function
#End Region

#Region "Config Counter"
        Public Function SaveCfgCounter(ByVal p As TbCfgCounterCenParaDB, ByVal UserName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0
            Dim lnq As New TbCfgCounterCenLinqDB
            lnq.ChkDataByCOUNTER_CODE_EVENT_DATE_SHOP_ID(p.COUNTER_CODE, p.EVENT_DATE, p.SHOP_ID, trans.Trans)
            If lnq.ID = 0 Then
                lnq.ChkDataByCOUNTER_NAME_EVENT_DATE_SHOP_ID(p.COUNTER_NAME, p.EVENT_DATE, p.SHOP_ID, trans.Trans)
                If lnq.ID = 0 Then
                    lnq.ChkDataByEVENT_DATE_SHOP_ID_UNITDISPLAY(p.EVENT_DATE, p.SHOP_ID, p.UNITDISPLAY, trans.Trans)
                End If
            End If

            lnq.SHOP_ID = p.SHOP_ID
            lnq.EVENT_DATE = p.EVENT_DATE
            lnq.COUNTER_CODE = p.COUNTER_CODE
            lnq.COUNTER_NAME = p.COUNTER_NAME
            lnq.QUICKSERVICE = p.QUICKSERVICE
            lnq.UNITDISPLAY = p.UNITDISPLAY
            lnq.SPEED_LANE = p.SPEED_LANE
            lnq.BACK_OFFICE = p.BACK_OFFICE
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.EVENT_STATUS = p.EVENT_STATUS
            lnq.COUNTER_MANAGER = p.COUNTER_MANAGER
            lnq.REF_SHOP_COUNTER_ID = p.REF_SHOP_COUNTER_ID

            Dim re As Boolean = False
            If lnq.ID <> 0 Then
                re = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                re = lnq.InsertData(UserName, trans.Trans)
            End If
            If re = False Then
                _err = lnq.ErrorMessage
            Else
                ret = lnq.ID
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SaveCfgCounterCustomertype(ByVal p As TbCfgCounterCustomerTypeCenParaDB, ByVal UserName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim sql As String = "delete from TB_CFG_COUNTER_CUSTOMER_TYPE where tb_cfg_counter_id='" & p.TB_CFG_COUNTER_ID & "'"
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)

            Dim lnq As New TbCfgCounterCustomerTypeCenLinqDB
            lnq.ChkDataByTB_CFG_COUNTER_ID_TB_CUSTOMERTYPE_ID(p.TB_CFG_COUNTER_ID, p.TB_CUSTOMERTYPE_ID, trans.Trans)

            lnq.TB_CFG_COUNTER_ID = p.TB_CFG_COUNTER_ID
            lnq.TB_CUSTOMERTYPE_ID = p.TB_CUSTOMERTYPE_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If

            If ret = False Then
                _err = lnq.ErrorMessage
            End If

            Return ret
        End Function

        Public Function DeleteCfgCounterCustomerAllow(ByVal CfgCounterID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_COUNTER_CUSTTYPE_ALLOW where tb_cfg_counter_id='" & CfgCounterID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgCounterCustomerType(ByVal CfgCounterID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_COUNTER_CUSTOMER_TYPE where tb_cfg_counter_id='" & CfgCounterID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgCounter(ByVal CfgCounterID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_COUNTER where id='" & CfgCounterID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgHoldReason(ByVal CfgHoldReasonID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_HOLD_REASON where id='" & CfgHoldReasonID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgLogoutReason(ByVal CfgLogoutReasonID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_LOGOUT_REASON where id='" & CfgLogoutReasonID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgUser(ByVal CfgUserID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_USER where id='" & CfgUserID & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgMainDisplay(ByVal shopid As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_SW_SCHED_MAINDISPLAY where EVENT_STATUS=1 AND Shop_ID='" & shopid & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgShopFileScheduleKiosk(ByVal shopid As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_SHOP_FILE_SCHEDULE " & _
                " where tb_shop_file_byte_id in (select id from TB_SHOP_FILE_BYTE where Target_Type in ('2','3') ) " & _
                " and Transfer_Status=1 AND Shop_ID ='" & shopid & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgShopFileScheduleMainDisplay(ByVal shopid As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_SHOP_FILE_SCHEDULE " & _
                " where tb_shop_file_byte_id in (select id from TB_SHOP_FILE_BYTE where Target_Type ='1') " & _
                " and Transfer_Status=1 AND Shop_ID ='" & shopid & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteCfgQSharpSchedule(ByVal shopid As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_SW_SCHED_QSHARP where EVENT_STATUS=1 AND Shop_ID='" & shopid & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function


        Public Function DeleteCfgKioskSchedule(ByVal shopid As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Try
                Dim sql As String = "delete from TB_CFG_SW_SCHED_KIOSK where EVENT_STATUS=1 AND Shop_ID='" & shopid & "'"
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, trans.Trans)
                ret = True
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function

        Public Function SaveCfgCounterCusttypeAllow(ByVal p As TbCfgCounterCusttypeAllowCenParaDB, ByVal UserName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False

            Dim lnq As New TbCfgCounterCusttypeAllowCenLinqDB
            lnq.ChkDataByTB_CFG_COUNTER_ID_TB_CUSTOMERTYPE_ID(p.TB_CFG_COUNTER_ID, p.TB_CUSTOMERTYPE_ID, trans.Trans)

            lnq.TB_CFG_COUNTER_ID = p.TB_CFG_COUNTER_ID
            lnq.TB_CUSTOMERTYPE_ID = p.TB_CUSTOMERTYPE_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If

            If ret = False Then
                _err = lnq.ErrorMessage
            End If

            Return ret
        End Function
#End Region

#Region "Search Data"
        Public Function SearchReason(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String, ByVal ReasonType As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("name")
                    dt.Columns.Add("productive")
                    dt.Columns.Add("active_status")

                    Dim whText As String = " name like '%" & wh & "%'"
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchReason")
                        If shTrans.Trans IsNot Nothing Then
                            Dim tmp As New DataTable
                            If ReasonType = "HoldReason" Then
                                Dim lnq As New ShLinqDB.TABLE.TbHoldReasonShLinqDB
                                tmp = lnq.GetDataList(whText, "name", shTrans.Trans)
                                lnq = Nothing
                            ElseIf ReasonType = "LogoutReason" Then
                                Dim lnq As New ShLinqDB.TABLE.TbLogoutReasonShLinqDB
                                tmp = lnq.GetDataList(whText, "name", shTrans.Trans)
                                lnq = Nothing
                            End If
                            shTrans.CommitTransaction()
                            BindReason(dt, tmp, shDr("shop_name_en"))
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Private Sub BindReason(ByVal dt As DataTable, ByVal tmp As DataTable, ByVal ShopNameEN As String)
            If tmp.Rows.Count > 0 Then
                For Each tmpDr As DataRow In tmp.Rows
                    Dim dr As DataRow = dt.NewRow
                    dr("shop_name_en") = ShopNameEN
                    dr("name") = tmpDr("name")
                    dr("productive") = IIf(Convert.ToInt16(tmpDr("productive")) = 1, "Yes", "No")
                    dr("active_status") = tmpDr("active_status")
                    dt.Rows.Add(dr)
                Next
            End If
            dt.Dispose()

        End Sub

        Public Function SearchService(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("item_code")
                    dt.Columns.Add("item_name")
                    dt.Columns.Add("item_order")
                    dt.Columns.Add("item_time")
                    dt.Columns.Add("item_wait")
                    dt.Columns.Add("txt_queue")
                    dt.Columns.Add("active_status")
                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchService")
                        If shTrans.Trans IsNot Nothing Then
                            Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetDataList(wh, "item_order", shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("id") = tmpDr("id")
                                    dr("item_code") = tmpDr("item_code")
                                    dr("item_name") = tmpDr("item_name")
                                    dr("item_order") = tmpDr("item_order")
                                    dr("item_time") = tmpDr("item_time")
                                    dr("item_wait") = tmpDr("item_wait")
                                    dr("txt_queue") = tmpDr("txt_queue")
                                    dr("active_status") = tmpDr("active_status")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function SearchSegment(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("segment")
                    dt.Columns.Add("active_status")
                    dt.Columns.Add("active")

                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchSegment")
                        If shTrans.Trans IsNot Nothing Then
                            Dim lnq As New ShLinqDB.TABLE.TbSegmentShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetDataList(wh, "segment", shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("segment") = tmpDr("segment")
                                    dr("active_status") = tmpDr("active_status")
                                    dr("active") = IIf(tmpDr("active_status") = 1, "Active", "No Active")
                                    dr("id") = tmpDr("id")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function SearchCustomerType(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("customertype_code")
                    dt.Columns.Add("customertype_name")
                    dt.Columns.Add("active_status")
                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchCustomerType")
                        If shTrans.Trans IsNot Nothing Then
                            Dim lnq As New ShLinqDB.TABLE.TbCustomertypeShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetDataList(wh, "customertype_code", shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("id") = tmpDr("id")
                                    dr("customertype_code") = tmpDr("customertype_code")
                                    dr("customertype_name") = tmpDr("customertype_name")
                                    dr("active_status") = tmpDr("active_status")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function SearchUser(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("group_name")
                    dt.Columns.Add("user_code")
                    dt.Columns.Add("position")
                    dt.Columns.Add("User_Name")
                    dt.Columns.Add("active_status")

                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchUser")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sql As String = " select u.id, u.user_code,u.fname + ' ' + u.lname user_name, u.position,g.group_name,u.active_status "
                            sql += " from tb_user u"
                            sql += " left join TB_GROUPUSER g on g.id=u.group_id"
                            sql += " where " & wh
                            sql += " order by u.fname, u.lname"

                            Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetListBySql(sql, shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("id") = tmpDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("group_name") = tmpDr("group_name")
                                    dr("user_code") = tmpDr("user_code")
                                    dr("position") = tmpDr("position")
                                    dr("User_Name") = tmpDr("user_name")
                                    dr("active_status") = tmpDr("active_status")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function SearchCounter(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("counter_code")
                    dt.Columns.Add("counter_name")
                    dt.Columns.Add("active_status")

                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchCounter")
                        If shTrans.Trans IsNot Nothing Then
                            Dim sql As String = " select c.id, c.counter_code,c.counter_name,c.active_status "
                            sql += " from tb_counter c"
                            sql += " where " & wh
                            sql += " order by c.counter_name"

                            Dim lnq As New ShLinqDB.TABLE.TbCounterShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetListBySql(sql, shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("id") = tmpDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("counter_code") = tmpDr("counter_code")
                                    dr("counter_name") = tmpDr("counter_name")
                                    dr("active_status") = tmpDr("active_status")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function SearchSkill(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("skill")
                    dt.Columns.Add("appointment")
                    dt.Columns.Add("active_status")
                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchSkill")
                        If shTrans.Trans IsNot Nothing Then
                            Dim lnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetDataList(wh, "skill", shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("id") = tmpDr("id")
                                    dr("skill") = tmpDr("skill")
                                    dr("appointment") = tmpDr("appointment")
                                    dr("active_status") = tmpDr("active_status")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function SearchGroupUser(ByVal ShopID As Long, ByVal wh As String, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(UserName, True)
            If shDt.Rows.Count > 0 Then
                If ShopID <> 0 Then
                    shDt.DefaultView.RowFilter = "id=" & ShopID
                End If
                If shDt.DefaultView.Count > 0 Then
                    dt.Columns.Add("shop_id")
                    dt.Columns.Add("shop_name_en")
                    dt.Columns.Add("id")
                    dt.Columns.Add("group_code")
                    dt.Columns.Add("group_name")
                    dt.Columns.Add("active_status")
                    If wh.Trim = "" Then
                        wh = "1=1"
                    End If
                    For Each shDr As DataRowView In shDt.DefaultView
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Common.FunctionEng.GetShTransction(shDr("id"), "ShopEventScheduleSettingENG.SearchGroupUser")
                        If shTrans.Trans IsNot Nothing Then
                            Dim lnq As New ShLinqDB.TABLE.TbGroupuserShLinqDB
                            Dim tmpDt As New DataTable
                            tmpDt = lnq.GetDataList(wh, "group_code", shTrans.Trans)
                            shTrans.CommitTransaction()
                            lnq = Nothing
                            If tmpDt.Rows.Count > 0 Then
                                For Each tmpDr As DataRow In tmpDt.Rows
                                    Dim dr As DataRow = dt.NewRow
                                    dr("shop_id") = shDr("id")
                                    dr("shop_name_en") = shDr("shop_name_en")
                                    dr("id") = tmpDr("id")
                                    dr("group_code") = tmpDr("group_code")
                                    dr("group_name") = tmpDr("group_name")
                                    dr("active_status") = tmpDr("active_status")
                                    dt.Rows.Add(dr)
                                Next
                            End If
                            tmpDt.Dispose()
                        End If
                    Next
                End If
            End If
            shDt.Dispose()
            lEng = Nothing

            Return dt
        End Function

        Public Function GetShopGroupUserPara(ByVal vID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShParaDB.TABLE.TbGroupuserShParaDB
            Dim p As New ShParaDB.TABLE.TbGroupuserShParaDB

            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbGroupuserShLinqDB
                p = lnq.GetParameter(vID, shTrans.Trans)
                lnq = Nothing
            End If

            Return p
        End Function
        Public Function GetShopGroupUserDup(ByVal ShopID As Long, ByVal GroupUserCode As String, ByVal GroupUserName As String, ByVal vID As Long) As Boolean
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Common.FunctionEng.GetShTransction(ShopID, "ShopEventScheduleSettingENG.SearchGroupUser")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbGroupuserShLinqDB
                If lnq.GetDataList("group_code='" & GroupUserCode & "' and id<>'" & vID & "'", "", shTrans.Trans).Rows.Count > 0 Then
                    ret = True
                    _err = "Duplicate Group Code"
                End If

                If ret = False Then
                    If lnq.GetDataList("group_name='" & GroupUserName & "' and id<>'" & vID & "'", "", shTrans.Trans).Rows.Count > 0 Then
                        ret = True
                        _err = "Duplicate Group Name"
                    End If
                End If
                lnq = Nothing
            End If
            shTrans.CommitTransaction()

            Return ret
        End Function

        Public Function GetShopSegmentDup(ByVal ShopID As Long, ByVal Segment As String, ByVal CustomerTypeID As Long, ByVal vID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Common.FunctionEng.GetShTransction(shTrans, trans, shLnq)
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbSegmentShLinqDB
                If lnq.GetDataList("segment='" & Segment & "' and customertype_id='" & CustomerTypeID & "' and id<>'" & vID & "'", "", shTrans.Trans).Rows.Count > 0 Then
                    ret = True
                    _err = "Duplicate Segment at " & shLnq.SHOP_NAME_EN
                End If

                lnq = Nothing
            End If
            shTrans.CommitTransaction()
            trans.CommitTransaction()
            shLnq = Nothing


            Return ret
        End Function
#End Region
    End Class
End Namespace

