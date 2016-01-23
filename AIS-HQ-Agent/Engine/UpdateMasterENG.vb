Imports CenLnqTable = CenLinqDB.TABLE
Imports ShLnqTable = ShLinqDB.TABLE
Imports System.Data
Imports CenLnqCommon = CenLinqDB.Common
Imports ShLnqCommon = ShLinqDB.Common
Imports System.IO

Public Class UpdateMasterENG
    Dim _err As String = ""

    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    Public Function GetMasterData(ByVal EventDate As String, ByVal TableName As String) As DataTable
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        Select Case TableName
            Case "TB_CFG_SW_SCHED_KIOSK"
                Dim lnq As New CenLnqTable.TbCfgSwSchedKioskCenLinqDB
                dt = lnq.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' And event_status = 1", "", trans.Trans)
                lnq = Nothing
            Case "TB_CFG_SW_SCHED_MAINDISPLAY"
                Dim lnq As New CenLnqTable.TbCfgSwSchedMaindisplayCenLinqDB
                dt = lnq.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' And event_status = 1 ", "", trans.Trans)
                lnq = Nothing
            Case "TB_CFG_SW_SCHED_QSHARP"
                Dim lnq As New CenLnqTable.TbCfgSwSchedQsharpCenLinqDB
                dt = lnq.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' And event_status = 1 ", "", trans.Trans)
                lnq = Nothing
        End Select

        trans.CommitTransaction()
        Return dt
    End Function

    Private Function GetCenColumnName(ByVal TableName) As DataTable
        Dim trans As New cenlnqcommon.Utilities.TransactionDB
        Dim dt As New DataTable
        Dim lnq As New CenLnqTable.TbCfgSwSchedKioskCenLinqDB
        Dim sql As String = "select column_name from information_schema.columns" & _
        " where table_name = '" & TableName & "' "
        dt = lnq.GetListBySql(sql, trans.Trans)
        trans.CommitTransaction()
        Return dt
    End Function

    Private Function GetShSETTING(ByVal shopID As String) As DataTable
        Dim trans As New shlnqcommon.Utilities.TransactionDB
        trans = Common.FunctionEng.GetShTransction(shopID, "UpdateMasterENG.getShSETTING")
        Dim dt As New DataTable
        Dim lnq As New ShLnqTable.TbSettingShLinqDB
        Dim sql As String = "select config_name from dbo.TB_SETTING"
        dt = lnq.GetListBySql(sql, trans.Trans)
        trans.CommitTransaction()
        Return dt
    End Function

    Private Function GetColumnUpdate(ByVal dtCen As DataTable, ByVal dtShop As DataTable) As ArrayList
        Dim arrList As New ArrayList

        For cnt As Integer = 0 To dtCen.Rows.Count - 1
            Dim tempDR() As DataRow
            tempDR = dtShop.Select("config_name = '" & dtCen.Rows(cnt).Item("column_name").ToString & "'")
            If tempDR.Length > 0 Then
                arrList.Add(dtCen.Rows(cnt).Item("column_name").ToString)
            End If
        Next

        Return arrList
    End Function

    Public Function UpdateStatus(ByVal ID As String, ByVal TableName As String) As Boolean

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim ret As Boolean
        Try
            Dim lnq As New CenLnqTable.TbCfgSwSchedKioskCenLinqDB
            ret = lnq.UpdateBySql("Update " & TableName & " Set event_status='2' Where ID='" & ID & "'", trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
        Catch ex As Exception
            _err = ex.Message.ToString
            trans.RollbackTransaction()
            ret = False
        End Try

        Return ret
    End Function

    Private Function UpdateShopSetting(ByVal shopID As String, ByVal arrRowUpdate As ArrayList, ByVal drCen As DataRow) As Boolean
        Dim ret As Boolean = False
        Dim trans As ShLinqDB.Common.Utilities.TransactionDB = Common.FunctionEng.GetShTransction(shopID, "UpdateMasterENG.UpdateShopSetting")
        Try
            Dim lnq As New ShLnqTable.TbSettingShLinqDB
            For cntArr As Integer = 0 To arrRowUpdate.Count - 1
                Dim key As String = arrRowUpdate(cntArr)
                Dim value As String = drCen.Item(key).ToString
                Dim sql As String = "Update TB_SETTING SET Config_Value ='" & value & "' where Config_Name='" & key & "'"
                ret = lnq.UpdateBySql(sql, trans.Trans)
                If ret = False Then
                    _err = lnq.ErrorMessage
                    trans.RollbackTransaction()
                    Return False
                End If
            Next
            ret = True
            trans.CommitTransaction()

        Catch ex As Exception
            _err = ex.Message.ToString
            trans.RollbackTransaction()
        End Try
        Return ret
    End Function

    Public Function UpdateMaster(ByVal EventDate As String, ByVal TableName As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            Dim dtCen As New DataTable
            dtCen = GetMasterData(EventDate, TableName)
            If dtCen.Rows.Count = 0 Then
                Return True
            End If

            Dim dtCenColumn As New DataTable
            dtCenColumn = GetCenColumnName(TableName)

            For cnt As Integer = 0 To dtCen.Rows.Count - 1
                Dim id As String = dtCen.Rows(cnt).Item("id").ToString
                Dim shop_id As String = dtCen.Rows(cnt).Item("shop_id").ToString
                Dim dtShop As New DataTable
                dtShop = GetShSETTING(shop_id)

                Dim arrRowUpdate As New ArrayList
                arrRowUpdate = GetColumnUpdate(dtCenColumn, dtShop)

                ret = UpdateShopSetting(shop_id, arrRowUpdate, dtCen.Rows(cnt))
                If ret = False Then Return False

                ret = UpdateStatus(id, TableName)
                If ret = False Then Return False
            Next
            ret = True
        Catch ex As Exception
            _err = ex.Message.ToString
            ret = False
        End Try
        Return ret
    End Function

#Region "__CFG_ITEM"

    Public Function UpdateItem(ByVal EventDate As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            'get master_item where eventdate = now and status = 1
            Dim trans As New CenLnqCommon.Utilities.TransactionDB
            Dim dtCen As New DataTable
            Dim lnqCen As New CenLnqTable.TbCfgItemCenLinqDB
            dtCen = lnqCen.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' And event_status = 1", "", trans.Trans)
            trans.CommitTransaction()
            If dtCen.Rows.Count = 0 Then
                Return True
            End If

            'update new item's data to shops
            For cnt As Integer = 0 To dtCen.Rows.Count - 1
                Dim id As String = dtCen.Rows(cnt).Item("id") & ""
                Dim shop_id As String = dtCen.Rows(cnt).Item("shop_id") & ""
                Dim master_item_id As String = dtCen.Rows(cnt).Item("master_item_id") & ""

                trans = New CenLinqDB.Common.Utilities.TransactionDB
                Dim lnqMaster As New CenLnqTable.TbItemCenLinqDB
                Dim paraMaster As New CenParaDB.TABLE.TbItemCenParaDB
                paraMaster = lnqMaster.GetParameter(master_item_id, trans.Trans)
                trans.CommitTransaction()

                If paraMaster IsNot Nothing Then
                    Dim shTrans As ShLnqCommon.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shop_id, "UpdateMasterENG.UpdateItem")
                    Dim lnqSh As New ShLnqTable.TbItemShLinqDB
                    Dim dtSh As New DataTable
                    dtSh = lnqSh.GetDataList(" MASTER_ITEMID = '" & paraMaster.ID & "'", "", shTrans.Trans)

                    If dtSh.Rows.Count <> 0 Then
                        lnqSh.GetDataByPK(dtSh.Rows(0).Item("id") & "", shTrans.Trans)
                    End If
                    With lnqSh
                        .ACTIVE_STATUS = paraMaster.ACTIVE_STATUS
                        .APP_MAX_QUEUE = paraMaster.APP_MAX_QUEUE
                        .APP_MIN_QUEUE = paraMaster.APP_MIN_QUEUE
                        .COLOR = paraMaster.COLOR
                        .ITEM_CODE = paraMaster.ITEM_CODE
                        .ITEM_NAME = paraMaster.ITEM_NAME
                        .ITEM_NAME_TH = paraMaster.ITEM_NAME_TH
                        .ITEM_ORDER = paraMaster.ITEM_ORDER
                        .ITEM_TIME = paraMaster.ITEM_TIME
                        .ITEM_WAIT = paraMaster.ITEM_WAIT
                        .Q_TYPE_ID = paraMaster.Q_TYPE_ID
                        .TXT_QUEUE = paraMaster.TXT_QUEUE
                        .TB_ITEM_GROUP_ID = paraMaster.TB_ITEM_GROUP_ID
                        .MASTER_ITEMID = paraMaster.ID
                    End With

                    If dtSh.Rows.Count = 0 Then
                        ret = lnqSh.InsertData(0, shTrans.Trans)
                    Else
                        ret = lnqSh.UpdateByPK(0, shTrans.Trans)
                    End If

                    If ret = True Then
                        shTrans.CommitTransaction()

                        'update master_status = 2
                        UpdateStatus(id, "TB_CFG_ITEM")

                    Else
                        _err = lnqSh.ErrorMessage
                        shTrans.RollbackTransaction()
                        ret = False
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            _err = ex.Message
            ret = False
        End Try
        Return ret
    End Function


#End Region '__CFG_ITEM

#Region "__CFG_HOLD_REASON"
    Public Function UpdateHoldReason(ByVal EventDate As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            'get master_item where eventdate = now and status = 1
            Dim trans As New CenLnqCommon.Utilities.TransactionDB
            Dim dtCen As New DataTable
            Dim lnqCen As New CenLnqTable.TbCfgHoldReasonCenLinqDB
            dtCen = lnqCen.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' and event_status='1'", "", trans.Trans)
            trans.CommitTransaction()
            If dtCen.Rows.Count = 0 Then
                Return True
            End If

            'update new item's data to shops
            For cnt As Integer = 0 To dtCen.Rows.Count - 1
                Dim id As String = dtCen.Rows(cnt).Item("id") & ""
                Dim shop_id As String = dtCen.Rows(cnt).Item("shop_id") & ""
                Dim master_holdreason_id As String = dtCen.Rows(cnt).Item("master_holdreason_id") & ""
                Dim hold_reason_name As String = dtCen.Rows(cnt)("name")
                Dim productive As String = dtCen.Rows(cnt)("productive")
                Dim active_status As String = dtCen.Rows(cnt)("active_status")

                Dim shTrans As ShLnqCommon.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shop_id, "UpdateMasterENG.UpdateHoldReason")
                Dim lnqSh As New ShLnqTable.TbHoldReasonShLinqDB
                Dim dtSh As New DataTable
                dtSh = lnqSh.GetDataList(" master_holdreasonid = '" & master_holdreason_id & "'", "", shTrans.Trans)

                If dtSh.Rows.Count <> 0 Then
                    lnqSh.GetDataByPK(dtSh.Rows(0).Item("id") & "", shTrans.Trans)
                End If
                With lnqSh
                    .ACTIVE_STATUS = active_status
                    .NAME = hold_reason_name
                    .PRODUCTIVE = productive
                    .MASTER_HOLDREASONID = master_holdreason_id
                End With

                If dtSh.Rows.Count = 0 Then
                    ret = lnqSh.InsertData(0, shTrans.Trans)
                Else
                    ret = lnqSh.UpdateByPK(0, shTrans.Trans)
                End If

                If ret = True Then
                    shTrans.CommitTransaction()

                    'update master_status = 2
                    UpdateStatus(id, "TB_CFG_HOLD_REASON")
                Else
                    _err = lnqSh.ErrorMessage
                    shTrans.RollbackTransaction()
                    ret = False
                    Exit For
                End If
            Next

        Catch ex As Exception
            _err = ex.Message
            ret = False
        End Try
        Return ret
    End Function
#End Region '__CFG_HOLD_REASON

#Region "__CFG_LOGOUT_REASON"
    Public Function UpdateLogoutReason(ByVal EventDate As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            'get master_item where eventdate = now and status = 1
            Dim trans As New CenLnqCommon.Utilities.TransactionDB
            Dim dtCen As New DataTable
            Dim lnqCen As New CenLnqTable.TbCfgLogoutReasonCenLinqDB
            dtCen = lnqCen.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' and event_status='1'", "", trans.Trans)
            trans.CommitTransaction()
            If dtCen.Rows.Count = 0 Then
                Return True
            End If

            'update new item's data to shops
            For cnt As Integer = 0 To dtCen.Rows.Count - 1
                Dim id As String = dtCen.Rows(cnt).Item("id") & ""
                Dim shop_id As String = dtCen.Rows(cnt).Item("shop_id") & ""
                Dim master_logoutreason_id As String = dtCen.Rows(cnt).Item("master_logoutreason_id") & ""
                Dim logout_reason_name As String = dtCen.Rows(cnt)("name")
                Dim productive As String = dtCen.Rows(cnt)("productive")
                Dim active_status As String = dtCen.Rows(cnt)("active_status")

                Dim shTrans As ShLnqCommon.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shop_id, "UpdateMasterENG.UpdateLogoutReason")
                Dim lnqSh As New ShLnqTable.TbLogoutReasonShLinqDB
                Dim dtSh As New DataTable
                dtSh = lnqSh.GetDataList(" master_logoutreasonid = '" & master_logoutreason_id & "'", "", shTrans.Trans)

                If dtSh.Rows.Count <> 0 Then
                    lnqSh.GetDataByPK(dtSh.Rows(0).Item("id") & "", shTrans.Trans)
                End If
                With lnqSh
                    .ACTIVE_STATUS = active_status
                    .NAME = logout_reason_name
                    .PRODUCTIVE = productive
                    .MASTER_LOGOUTREASONID = master_logoutreason_id
                End With

                If dtSh.Rows.Count = 0 Then
                    ret = lnqSh.InsertData(0, shTrans.Trans)
                Else
                    ret = lnqSh.UpdateByPK(0, shTrans.Trans)
                End If

                If ret = True Then
                    shTrans.CommitTransaction()

                    'update master_status = 2
                    UpdateStatus(id, "TB_CFG_LOGOUT_REASON")
                Else
                    _err = lnqSh.ErrorMessage
                    shTrans.RollbackTransaction()
                    ret = False
                    Exit For
                End If
            Next

        Catch ex As Exception
            _err = ex.Message
            ret = False
        End Try
        Return ret
    End Function
#End Region '__CFG_LOGOUT_REASON

#Region "__CFG_COUNTER"
    Public Function UpdateCounter(ByVal EventDate As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            'get cfg_master_item where eventdate = now and status = 1
            Dim trans As New CenLnqCommon.Utilities.TransactionDB
            Dim dtCen As New DataTable
            Dim lnqCen As New CenLnqTable.TbCfgCounterCenLinqDB
            dtCen = lnqCen.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' and event_status='1'", "isnull(update_date,create_date)", trans.Trans)
            trans.CommitTransaction()
            If dtCen.Rows.Count = 0 Then
                dtCen.Dispose()
                lnqCen = Nothing
                Return True
            End If

            For Each dr As DataRow In dtCen.Rows
                trans = New CenLinqDB.Common.Utilities.TransactionDB
                lnqCen.GetDataByPK(dr("id"), trans.Trans)
                trans.CommitTransaction()

                If lnqCen.ID <> 0 Then
                    Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(lnqCen.SHOP_ID, "UpdateMasterENG.UpdateCounter")
                    If shTrans.Trans IsNot Nothing Then
                        Dim lnq As New ShLinqDB.TABLE.TbCounterShLinqDB
                        If lnqCen.REF_SHOP_COUNTER_ID > 0 Then
                            lnq.GetDataByPK(lnqCen.REF_SHOP_COUNTER_ID, shTrans.Trans)
                        End If
                        If lnq.ID = 0 Then
                            lnq.ChkDataByWhere("counter_code = '" & lnqCen.COUNTER_CODE & "'", shTrans.Trans)
                        End If
                        If lnq.ID = 0 Then
                            lnq.ChkDataByWhere("counter_name = '" & lnqCen.COUNTER_NAME & "'", shTrans.Trans)
                        End If
                        If lnq.ID = 0 Then
                            lnq.ChkDataByWhere("unitdisplay='" & lnqCen.UNITDISPLAY & "'", shTrans.Trans)
                        End If

                        lnq.COUNTER_CODE = lnqCen.COUNTER_CODE
                        lnq.COUNTER_NAME = lnqCen.COUNTER_NAME
                        lnq.QUICKSERVICE = lnqCen.QUICKSERVICE
                        lnq.SPEED_LANE = lnqCen.SPEED_LANE
                        lnq.UNITDISPLAY = lnqCen.UNITDISPLAY
                        lnq.BACK_OFFICE = lnqCen.BACK_OFFICE
                        lnq.COUNTER_MANAGER = lnqCen.COUNTER_MANAGER
                        lnq.ACTIVE_STATUS = lnqCen.ACTIVE_STATUS

                        If lnq.ID <> 0 Then
                            ret = lnq.UpdateByPK(lnqCen.CREATE_BY, shTrans.Trans)
                        Else
                            ret = lnq.InsertData(lnqCen.CREATE_BY, shTrans.Trans)
                        End If
                        If ret = True Then
                            'Counter Customer Type
                            Dim dSql As String = "delete from TB_COUNTER_CUSTOMERTYPE where counter_id='" & lnq.ID & "'"
                            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, shTrans.Trans)

                            Dim CntCtDT As New DataTable
                            CntCtDT = lnqCen.CHILD_TB_CFG_COUNTER_CUSTOMER_TYPE_tb_cfg_counter_id
                            If CntCtDT.Rows.Count > 0 Then
                                For Each CntCtDR As DataRow In CntCtDT.Rows
                                    Dim CntCtLnq As New ShLinqDB.TABLE.TbCounterCustomertypeShLinqDB
                                    CntCtLnq.COUNTER_ID = lnq.ID
                                    CntCtLnq.CUSTOMERTYPE_ID = Convert.ToInt64(CntCtDR("tb_customertype_id"))

                                    ret = CntCtLnq.InsertData(CntCtDR("CREATE_BY"), shTrans.Trans)
                                    If ret = False Then
                                        shTrans.RollbackTransaction()
                                        _err = CntCtLnq.ErrorMessage
                                        Exit Function
                                    End If
                                Next
                            End If
                            CntCtDT.Dispose()


                            'Counter Customer Type Allow
                            dSql = "delete from TB_COUNTER_CUSTOMERTYPE_ALLOW where counter_id='" & lnq.ID & "'"
                            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, shTrans.Trans)

                            Dim CntAlDT As New DataTable
                            CntAlDT = lnqCen.CHILD_TB_CFG_COUNTER_CUSTTYPE_ALLOW_tb_cfg_counter_id
                            If CntAlDT.Rows.Count > 0 Then
                                For Each CntAlDR As DataRow In CntAlDT.Rows
                                    Dim CntAlLnq As New ShLinqDB.TABLE.TbCounterCustomertypeAllowShLinqDB
                                    CntAlLnq.COUNTER_ID = lnq.ID
                                    CntAlLnq.CUSTOMERTYPE_ID = Convert.ToInt64(CntAlDR("tb_customertype_id"))
                                    ret = CntAlLnq.InsertData(CntAlDR("CREATE_BY"), shTrans.Trans)
                                    If ret = False Then
                                        shTrans.RollbackTransaction()
                                        _err = CntAlLnq.ErrorMessage
                                        Exit Function
                                    End If
                                Next
                            End If
                            CntAlDT.Dispose()
                        Else
                            shTrans.RollbackTransaction()
                            _err = lnq.ErrorMessage
                            ret = False
                            Exit Function
                        End If

                        If ret = True Then
                            lnqCen.EVENT_STATUS = "2"
                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                            ret = lnqCen.UpdateByPK("AisUpdateMasterAgent", trans.Trans)
                            If ret = True Then
                                trans.CommitTransaction()
                                shTrans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                shTrans.RollbackTransaction()
                                _err = lnqCen.ErrorMessage
                            End If
                        Else
                            shTrans.RollbackTransaction()
                        End If
                    Else
                        ret = False
                        _err = shTrans.ErrorMessage
                        Exit Function
                    End If
                End If
            Next
            dtCen.Dispose()
            lnqCen = Nothing

        Catch ex As Exception
            _err = "UpdateMasterENG.UpdateCounter Exception :" & ex.Message
            ret = False
        End Try

        Return ret
    End Function
#End Region '__CFG_COUNTER

#Region "__CFG_USER"
    Public Function UpdateUser(ByVal EventDate As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            'get cfg_master_item where eventdate = now and status = 1

            Dim dtCen As New DataTable
            Dim lnqCen As New CenLnqTable.TbCfgUserCenLinqDB
            dtCen = lnqCen.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' and event_status='1'", "", Nothing)
            If dtCen.Rows.Count = 0 Then
                dtCen.Dispose()
                lnqCen = Nothing
                Return True
            End If

            For Each dr As DataRow In dtCen.Rows
                lnqCen.GetDataByPK(dr("id"), Nothing)

                If lnqCen.ID <> 0 Then
                    Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(lnqCen.SHOP_ID, "UpdateMasterENG.UpdateUser")
                    If shTrans.Trans IsNot Nothing Then
                        Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                        lnq.ChkDataByUSERNAME(lnqCen.USERNAME, shTrans.Trans)

                        lnq.TITLE_ID = lnqCen.TITLE_ID
                        lnq.USER_CODE = lnqCen.USER_CODE
                        lnq.FNAME = lnqCen.FNAME
                        lnq.LNAME = lnqCen.LNAME
                        lnq.POSITION = lnqCen.POSITION
                        lnq.GROUP_ID = lnqCen.GROUP_ID
                        lnq.USERNAME = lnqCen.USERNAME
                        lnq.PASSWORD = lnqCen.PASSWORD
                        lnq.ACTIVE_STATUS = lnqCen.ACTIVE_STATUS

                        If lnq.ID <> 0 Then
                            ret = lnq.UpdateByPK(lnqCen.CREATE_BY, shTrans.Trans)
                        Else
                            ret = lnq.InsertData(lnqCen.CREATE_BY, shTrans.Trans)
                        End If

                        If ret = True Then
                            Dim dSql As String = "delete from tb_user_skill where user_id='" & lnq.ID & "'"
                            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, shTrans.Trans)

                            Dim cfgUserSkillDt As New DataTable
                            cfgUserSkillDt = lnqCen.GetListBySql("select * from TB_CFG_USER_SKILL where tb_cfg_user_id='" & lnqCen.ID & "'", Nothing)
                            If cfgUserSkillDt.Rows.Count > 0 Then
                                For Each usDr As DataRow In cfgUserSkillDt.Rows
                                    Try
                                        Dim skLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                                        skLnq.ChkDataByWhere("master_skillid=" & Convert.ToInt16(usDr("skill_id")), shTrans.Trans)
                                        If skLnq.ID <> 0 Then
                                            Dim usLnq As New ShLinqDB.TABLE.TbUserSkillShLinqDB
                                            usLnq.USER_ID = lnq.ID
                                            usLnq.SKILL_ID = skLnq.ID

                                            ret = usLnq.InsertData(usDr("create_by"), shTrans.Trans)
                                            If ret = False Then
                                                shTrans.RollbackTransaction()
                                                _err = lnq.ErrorMessage
                                                Exit Function
                                            End If
                                        End If
                                        skLnq = Nothing
                                    Catch ex As Exception
                                        shTrans.RollbackTransaction()
                                        _err = ex.Message
                                        Exit Function
                                    End Try
                                Next
                            End If
                            cfgUserSkillDt.Dispose()
                        Else
                            shTrans.RollbackTransaction()
                            _err = lnq.ErrorMessage
                            ret = False
                            Exit Function
                        End If

                        If ret = True Then
                            Dim trans As New CenLnqCommon.Utilities.TransactionDB
                            lnqCen.EVENT_STATUS = "2"
                            ret = lnqCen.UpdateByPK("AisUpdateMasterAgent", trans.Trans)
                            If ret = True Then
                                trans.CommitTransaction()
                                shTrans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                shTrans.RollbackTransaction()
                                _err = lnqCen.ErrorMessage
                            End If
                        Else
                            shTrans.RollbackTransaction()
                        End If
                    Else
                        _err = shTrans.ErrorMessage
                        ret = False
                        Exit Function
                    End If
                End If
            Next
            dtCen.Dispose()
            lnqCen = Nothing
        Catch ex As Exception
            _err = "UpdateMasterENG.UpdateUser Exception :" & ex.Message
            ret = False
        End Try

        Return ret
    End Function
#End Region  '__CFG_USER



#Region "__CFG_SKILL"
    Public Function UpdateSkill(ByVal EventDate As String) As Boolean
        _err = ""
        Dim ret As Boolean = False
        Try
            'get cfg_master_item where eventdate = now and status = 1
            Dim trans As New CenLnqCommon.Utilities.TransactionDB
            Dim dtCen As New DataTable
            Dim lnqCen As New CenLnqTable.TbCfgSkillCenLinqDB
            dtCen = lnqCen.GetDataList("convert(varchar(10),event_date,120) = '" & EventDate & "' and event_status='1'", "", trans.Trans)
            trans.CommitTransaction()
            If dtCen.Rows.Count = 0 Then
                Return True
            End If

            'if exist data for update to shops
            For cnt As Integer = 0 To dtCen.Rows.Count - 1
                Dim id As String = dtCen.Rows(cnt).Item("id") & ""
                Dim shop_id As String = dtCen.Rows(cnt).Item("shop_id") & ""
                Dim master_skill_id As String = dtCen.Rows(cnt).Item("master_skill_id") & ""

                'get skill&skill_item for update to shop
                trans = New CenLinqDB.Common.Utilities.TransactionDB
                Dim lnqMaster As New CenLnqTable.TbSkillCenLinqDB
                Dim paraMaster As New CenParaDB.TABLE.TbSkillCenParaDB
                paraMaster = lnqMaster.GetParameter(master_skill_id, trans.Trans)

                Dim lnqSkillItem As New CenLnqTable.TbSkillItemCenLinqDB
                Dim dtSkillItem As New DataTable
                If paraMaster IsNot Nothing Then
                    dtSkillItem = lnqSkillItem.GetDataList("skill_id = '" & paraMaster.ID & "'", "", trans.Trans)
                End If
                trans.CommitTransaction()

                'if exist data
                If paraMaster IsNot Nothing Then
                    Dim shTrans As ShLnqCommon.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shop_id, "UpdateMasterENG.UpdateSkill")
                    Dim lnqSh As New ShLnqTable.TbSkillShLinqDB
                    Dim dtSh As New DataTable
                    dtSh = lnqSh.GetDataList(" master_skillid = '" & paraMaster.ID & "'", "", shTrans.Trans)

                    'insert/update header
                    If dtSh.Rows.Count <> 0 Then
                        lnqSh.GetDataByPK(dtSh.Rows(0).Item("id") & "", shTrans.Trans)
                    End If
                    With lnqSh
                        .ACTIVE_STATUS = paraMaster.ACTIVE_STATUS
                        .SKILL = paraMaster.SKILL
                        .APPOINTMENT = paraMaster.APPOINTMENT
                        .MASTER_SKILLID = paraMaster.ID
                    End With

                    If dtSh.Rows.Count = 0 Then
                        ret = lnqSh.InsertData(0, shTrans.Trans)
                    Else
                        ret = lnqSh.UpdateByPK(0, shTrans.Trans)
                    End If

                    'delete&insert detail
                    Try
                        Dim uSql As String = "delete from tb_skill_item "
                        uSql += " where skill_id = '" & lnqSh.ID & "' "
                        ShLnqCommon.Utilities.SqlDB.ExecuteNonQuery(uSql, shTrans.Trans)
                    Catch ex As Exception
                        _err = ex.Message.ToString
                        shTrans.RollbackTransaction()
                        Return False
                    End Try


                    If dtSkillItem.Rows.Count > 0 Then
                        For cntItem As Integer = 0 To dtSkillItem.Rows.Count - 1
                            Dim lnqShItem As New ShLnqTable.TbSkillItemShLinqDB
                            Dim Item_ID As Integer = dtSkillItem.Rows(cntItem).Item("ITEM_ID")
                            With lnqShItem
                                .ID = 0
                                .SKILL_ID = lnqSh.ID
                                .ITEM_ID = Item_ID
                            End With
                            ret = lnqShItem.InsertData(0, shTrans.Trans)

                            If ret = False Then
                                _err = lnqShItem.ErrorMessage
                                shTrans.RollbackTransaction()
                                Return False
                            End If

                        Next
                    End If


                    If ret = True Then
                        'if everthing's success
                        shTrans.CommitTransaction()

                        'update master_status = 2
                        If UpdateStatus(id, "TB_CFG_SKILL") = False Then
                            Return False
                        End If

                    Else
                        _err = lnqSh.ErrorMessage
                        shTrans.RollbackTransaction()
                        Return False
                    End If

                End If
            Next

        Catch ex As Exception
            _err = ex.Message
            Return False
        End Try
        Return ret
    End Function


#End Region '__CFG_SKILL



#Region "CONVERT IMAGE FROM DB"
    Public Function ConvertImageFromDB() As Boolean
        Dim ret As Boolean = True
        _err = ""
        Dim lnq As New CenLnqTable.TbShopFileByteCenLinqDB
        Dim sql As String = "select id from TB_SHOP_FILE_BYTE where convert_status='N'"
        Dim dt As New DataTable
        dt = lnq.GetListBySql(sql, Nothing)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim trans As New CenLnqCommon.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    lnq = New CenLnqTable.TbShopFileByteCenLinqDB
                    lnq.GetDataByPK(dr("id"), trans.Trans)
                    If lnq.ID <> 0 Then
                        Try
                            If Directory.Exists(lnq.FOLDER_NAME) = False Then
                                Directory.CreateDirectory(lnq.FOLDER_NAME)
                            End If

                            Dim fs As FileStream
                            If File.Exists(lnq.FOLDER_NAME & "\" & lnq.FILE_NAME) = True Then
                                File.Delete(lnq.FOLDER_NAME & "\" & lnq.FILE_NAME)
                            End If

                            fs = New FileStream(lnq.FOLDER_NAME & "\" & lnq.FILE_NAME, FileMode.CreateNew)
                            fs.Write(lnq.FILE_BYTE, 0, lnq.FILE_BYTE.Length)
                            fs.Close()

                            lnq.FILE_BYTE = Nothing
                            lnq.CONVERT_STATUS = "Y"
                            ret = lnq.UpdateByPK("UpdateMasterENG.ConvertImageFromDB", trans.Trans)
                            If ret = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                _err = "UpdateMasterENG.ConvertImageFromDB : " & lnq.ErrorMessage
                            End If
                        Catch ex As Exception
                            trans.RollbackTransaction()
                            _err = "UpdateMasterENG.ConvertImageFromDB : Exception : " & ex.Message
                            ret = False
                        End Try
                    Else
                        trans.RollbackTransaction()
                        ret = False
                        _err = "UpdateMasterENG.ConvertImageFromDB : " & lnq.ErrorMessage
                    End If
                End If
            Next
        End If
        lnq = Nothing
        dt.Dispose()

        Return ret
    End Function

    Public Function GetMainDisplayVDOFile() As DataTable
        Dim ret As New DataTable
        _err = ""
        Dim lnq As New CenLnqTable.TbShopFileByteCenLinqDB
        Dim sql As String = " select ss.id,sf.folder_name,sf.file_name,ss.shop_id,s.shop_qm_url "
        sql += " from TB_SHOP_FILE_SCHEDULE ss"
        sql += " inner join TB_SHOP_FILE_BYTE sf on sf.id=ss.tb_shop_file_byte_id "
        sql += " inner join TB_SHOP s on s.id=ss.shop_id"
        sql += " where ss.transfer_status='1' and sf.target_type='1' and DATEDIFF(d,getdate(),event_date)=0 "
        ret = lnq.GetListBySql(sql, Nothing)
        _err = lnq.ErrorMessage
        lnq = Nothing
        Return ret
    End Function

#End Region '__CONVERT IMAGE FROM DB



End Class
