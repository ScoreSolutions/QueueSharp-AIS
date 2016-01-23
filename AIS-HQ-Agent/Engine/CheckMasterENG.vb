Imports CenLinqDB.TABLE
Imports Engine.Common
Imports System.Windows.Forms

Public Class CheckMasterENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox

    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    'Public Sub SetTextLog(ByVal txtLog As TextBox)
    '    _TXT_LOG = txtLog
    'End Sub

    'Dim _OldLog As String = ""
    'Private Sub PrintLog(ByVal LogDesc As String)
    '    If _OldLog <> LogDesc Then
    '        _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
    '        _OldLog = LogDesc
    '        Application.DoEvents()
    '    End If
    'End Sub

    Public Function CheckConflictMasterData(ByVal ShopID As Long) As String
        Dim ret As String = ""
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        If shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME) = True Then
            Dim Tmp As String = "<font color='#0000CC' size='5px' ><b>Master Setup มีการเปลี่ยนแปลงที่ Shop " & shLnq.SHOP_NAME_EN & "</b></font> ที่ "

            Dim MsgItem As String = ShopItem(shTrans, shLnq)
            If MsgItem.Trim <> "" Then
                ret += "<br />Table <font color='blue'><b>" & shLnq.SHOP_DB_NAME & ".TB_ITEM</b></font>"
                ret += MsgItem
                ret += "<br />"
            End If

            Dim MsgHold As String = ShopHoldReason(shTrans, shLnq)
            If MsgHold.Trim <> "" Then
                ret += "<br />Table <font color='blue'><b>" & shLnq.SHOP_DB_NAME & ".TB_HOLD_REASON</b></font>"
                ret += MsgHold
                ret += "<br />"
            End If

            Dim MsgLogout As String = ShopLogoutReason(shTrans, shLnq)
            If MsgLogout.Trim <> "" Then
                ret += "<br />Table <font color='blue'><b>" & shLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON</b></font>"
                ret += MsgLogout
                ret += "<br />"
            End If

            'Dim MsgGroupUser As String = ShopGroupuser(shTrans, shLnq)
            'If MsgGroupUser.Trim <> "" Then
            '    ret += "<br />Table <font color='blue'><b>" & shLnq.SHOP_DB_NAME & ".TB_GROUPUSER</b></font>"
            '    ret += MsgGroupUser
            '    ret += "<br />"
            'End If

            'Dim MsgCustomerType As String = ShopCustomertype(shTrans, shLnq)
            'If MsgCustomerType.Trim <> "" Then
            '    ret += "<br />Table <font color='blue'><b>" & shLnq.SHOP_DB_NAME & ".TB_CUSTOMERTYPE</b></font>"
            '    ret += MsgCustomerType
            '    ret += "<br />"
            'End If

            'Dim MsgSkill As String = ShopSkill(shTrans, shLnq)
            'If MsgSkill.Trim <> "" Then
            '    ret += "<br />Table <font color='blue'><b>" & shLnq.SHOP_DB_NAME & ".TB_SKILL</b></font>"
            '    ret += MsgSkill
            '    ret += "<br />"
            'End If

            shTrans.CommitTransaction()
            If ret.Trim <> "" Then
                ret = Tmp & ret
                ret += "<hr /><br /><br />"  'เว้นระหว่าง Shop
            End If
        End If
        shLnq = Nothing

        Return ret
    End Function

    Private Function ShopLogoutReason(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim mLnq As New CenLinqDB.TABLE.TbLogoutReasonCenLinqDB
            Dim mDt As New DataTable
            mDt = mLnq.GetDataList("1=1", "", trans.Trans)
            If mDt.Rows.Count > 0 Then
                Dim shDt As New DataTable
                Dim shLnq As New ShLinqDB.TABLE.TbLogoutReasonShLinqDB
                shDt = shLnq.GetDataList("1=1", "", shTrans.Trans)
                shLnq = Nothing
                If shDt.Rows.Count > 0 Then
                    'If shDt.Columns.Contains("master_logoutreasonid") = True Then
                    '    For Each shDr As DataRow In shDt.Rows
                    '        If Convert.IsDBNull(shDr("master_logoutreasonid")) = False Then
                    '            mDt.DefaultView.RowFilter = "id = '" & shDr("master_logoutreasonid") & "'"
                    '            If mDt.DefaultView.Count = 0 Then
                    '                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.master_logoutreasonid: <b>" & shDr("master_logoutreasonid").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_LOGOUT_REASON.id"
                    '            End If

                    '            mDt.DefaultView.RowFilter = "name = '" & shDr("name") & "'"
                    '            If mDt.DefaultView.Count = 0 Then
                    '                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.name: <b>" & shDr("name").ToString & " not found in QisDB.TB_LOGOUT_REASON.name"
                    '            End If

                    '            mDt.DefaultView.RowFilter = "id = '" & shDr("master_logoutreasonid") & "' and productive = '" & shDr("productive") & "'"
                    '            If mDt.DefaultView.Count = 0 Then
                    '                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.name: <b>" & shDr("name").ToString & "</b> ฟิลด์ productive ไม่ตรงกันกับ QisDB.TB_LOGOUT_REASON.productive"
                    '            End If
                    '        Else
                    '            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.name: <b>" & shDr("name").ToString & "</b> ไม่ได้อ้างอิงกับ to QisDB.TB_LOGOUT_REASON"
                    '        End If
                    '    Next
                    'Else
                    '    ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON" & " ไม่มีฟิลด์ master_logoutreasonid"
                    'End If


                    'ใช้ชั่วคราวไปก่อน
                    For Each shDr As DataRow In shDt.Rows
                        mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "'"
                        If mDt.DefaultView.Count = 0 Then
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.id: <b>" & shDr("id").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_LOGOUT_REASON.id"
                        End If

                        mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and trim(name) = '" & shDr("name").ToString.Trim & "'"
                        If mDt.DefaultView.Count = 0 Then
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.name: <b>" & shDr("name").ToString & " ไม่มีอยู่ใน QisDB.TB_LOGOUT_REASON.name"
                        End If

                        mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and productive = '" & shDr("productive") & "'"
                        If mDt.DefaultView.Count = 0 Then
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_LOGOUT_REASON.name: <b>" & shDr("name").ToString & "</b> ฟิลด์ productive ไม่ตรงกันกับ QisDB.TB_LOGOUT_REASON.productive"
                        End If
                    Next
                End If
                shDt = Nothing
            Else
                ret = "Not found data in QisDB.TB_LOGOUT_REASON"
            End If
            mDt = Nothing
            mLnq = Nothing
        End If
        trans.CommitTransaction()

        Return ret
    End Function

    Private Function ShopHoldReason(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim mLnq As New CenLinqDB.TABLE.TbHoldReasonCenLinqDB
            Dim mDt As New DataTable
            mDt = mLnq.GetDataList("1=1", "", trans.Trans)
            If mDt.Rows.Count > 0 Then
                Dim shDt As New DataTable
                Dim shLnq As New ShLinqDB.TABLE.TbHoldReasonShLinqDB
                shDt = shLnq.GetDataList("1=1", "", shTrans.Trans)
                shLnq = Nothing
                If shDt.Rows.Count > 0 Then
                    'If shDt.Columns.Contains("master_holdreasonid") = True Then
                    '    For Each shDr As DataRow In shDt.Rows
                    '        If Convert.IsDBNull(shDr("master_holdreasonid")) = False Then
                    '            mDt.DefaultView.RowFilter = "id = '" & shDr("master_holdreasonid") & "'"
                    '            If mDt.DefaultView.Count = 0 Then
                    '                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.master_holdreasonid: <b>" & shDr("master_holdreasonid").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_HOLD_REASON.id"
                    '            End If

                    '            mDt.DefaultView.RowFilter = "name = '" & shDr("name") & "'"
                    '            If mDt.DefaultView.Count = 0 Then
                    '                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.name: <b>" & shDr("name").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_HOLD_REASON.name"
                    '            End If

                    '            mDt.DefaultView.RowFilter = "id = '" & shDr("master_holdreasonid") & "' and productive = '" & shDr("productive") & "'"
                    '            If mDt.DefaultView.Count = 0 Then
                    '                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.name: <b>" & shDr("name").ToString & "</b> ฟิลด์ productive ไม่ตรงกันกับ QisDB.TB_HOLD_REASON.productive"
                    '            End If
                    '        Else
                    '            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.name: <b>" & shDr("name").ToString & "</b> ไม่ได้อ้างอิงกับ QisDB.TB_HOLD_REASON"
                    '        End If
                    '    Next
                    'Else
                    '    ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON" & " ไม่มีฟิลด์ master_holdreasonid"
                    'End If


                    'ใช้ชั่วคราวไปก่อน
                    For Each shDr As DataRow In shDt.Rows
                        mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "'"
                        If mDt.DefaultView.Count = 0 Then
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.id: <b>" & shDr("id").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_HOLD_REASON.id"
                        End If

                        mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and trim(name) = '" & shDr("name").ToString.Trim & "'"
                        If mDt.DefaultView.Count = 0 Then
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.name: <b>" & shDr("name").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_HOLD_REASON.name"
                        End If

                        mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and productive = '" & shDr("productive") & "'"
                        If mDt.DefaultView.Count = 0 Then
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_HOLD_REASON.name: <b>" & shDr("name").ToString & "</b> ฟิลด์ productive ไม่ตรงกันกับ QisDB.TB_HOLD_REASON.productive"
                        End If
                    Next
                End If
                shDt = Nothing
            Else
                ret = "Not found data in QisDB.TB_HOLD_REASON"
            End If
            mDt = Nothing
            mLnq = Nothing
        End If
        trans.CommitTransaction()

        Return ret
    End Function

    Private Function ShopSkill(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim mLnq As New CenLinqDB.TABLE.TbSkillCenLinqDB
            Dim mDt As New DataTable
            mDt = mLnq.GetDataList("1=1", "", trans.Trans)
            If mDt.Rows.Count > 0 Then
                Dim shDt As New DataTable
                Dim shLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                shDt = shLnq.GetDataList("1=1", "", shTrans.Trans)
                shLnq = Nothing
                If shDt.Rows.Count > 0 Then
                    For Each shDr As DataRow In shDt.Rows
                        If Convert.IsDBNull(shDr("master_skillid")) = False Then
                            mDt.DefaultView.RowFilter = "id = '" & shDr("master_skillid") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_SKILL.master_skillid: <b>" & shDr("master_skillid").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_SKILL.id"
                            End If

                            mDt.DefaultView.RowFilter = "skill = '" & shDr("skill") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_SKILL.skill: <b>" & shDr("skill").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_SKILL.skill"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("master_skillid") & "' and active_status = '" & shDr("active_status") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_SKILL.skill: <b>" & shDr("skill").ToString & "</b> ฟิลด์ active_status ไม่ตรงกันกับ QisDB.TB_SKILL.active_status"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("master_skillid") & "' and appointment = '" & shDr("appointment") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_SKILL.skill: <b>" & shDr("skill").ToString & "</b> ฟิลด์ appointment ไม่ตรงกันกับ QisDB.TB_SKILL.appointment"
                            End If
                        Else
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_SKILL.skill: <b>" & shDr("skill").ToString & "</b> ไม่ได้อ้างอิงกับ QisDB.TB_SKILL"
                        End If
                    Next
                End If
                shDt = Nothing
            Else
                ret = "Not found data in QisDB.TB_SKILL"
            End If
            mDt = Nothing
            mLnq = Nothing
        End If
        trans.CommitTransaction()

        Return ret
    End Function

    Private Function ShopGroupuser(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim mLnq As New CenLinqDB.TABLE.TbGroupuserCenLinqDB
            Dim mDt As New DataTable
            mDt = mLnq.GetDataList("1=1", "", trans.Trans)
            If mDt.Rows.Count > 0 Then
                Dim shDt As New DataTable
                Dim shLnq As New ShLinqDB.TABLE.TbGroupuserShLinqDB
                shDt = shLnq.GetDataList("1=1", "", shTrans.Trans)
                shLnq = Nothing
                If shDt.Rows.Count > 0 Then
                    For Each shDr As DataRow In shDt.Rows
                        If Convert.IsDBNull(shDr("master_groupuserid")) = False Then
                            mDt.DefaultView.RowFilter = "id = '" & shDr("master_groupuserid") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_GROUPUSER.master_groupuserid: <b>" & shDr("master_groupuserid").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_GROUPUSER.id"
                            End If

                            mDt.DefaultView.RowFilter = "group_code = '" & shDr("group_code") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_GROUPUSER.group_code: <b>" & shDr("group_code").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_GROUPUSER.group_code"
                            End If

                            mDt.DefaultView.RowFilter = "group_name = '" & shDr("group_name") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_GROUPUSER.group_name: <b>" & shDr("group_name").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_GROUPUSER.group_name"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("master_groupuserid") & "' and active_status = '" & shDr("active_status") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_GROUPUSER.group_name: <b>" & shDr("group_name").ToString & "</b> ฟิลด์ active_status ไม่ตรงกันกับ QisDB.TB_GROUPUSER.active_status"
                            End If
                        Else
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_GROUPUSER.group_name: <b>" & shDr("group_name").ToString & "</b> ไม่ได้อ้างอิงกับ QisDB.TB_GROUPUSER"
                        End If
                    Next
                End If
                shDt = Nothing
            Else
                ret = "Not found data in QisDB.TB_GROUPUSER"
            End If
            mDt = Nothing
            mLnq = Nothing
        End If
        trans.CommitTransaction()

        Return ret
    End Function

    Private Function ShopItem(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim mLnq As New CenLinqDB.TABLE.TbItemCenLinqDB
            Dim mDt As New DataTable
            mDt = mLnq.GetDataList("1=1", "", trans.Trans)
            If mDt.Rows.Count > 0 Then
                Dim shDt As New DataTable
                Dim shLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                shDt = shLnq.GetDataList("1=1", "", shTrans.Trans)
                shLnq = Nothing
                If shDt.Rows.Count > 0 Then
                    For Each shDr As DataRow In shDt.Rows
                        'If Convert.IsDBNull(shDr("master_itemid")) = False Then
                        '    mDt.DefaultView.RowFilter = "id = '" & shDr("master_itemid") & "'"
                        '    If mDt.DefaultView.Count = 0 Then
                        '        ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.master_itemid: <b>" & shDr("master_itemid").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_ITEM.id"
                        '    End If

                        '    mDt.DefaultView.RowFilter = "item_code = '" & shDr("item_code") & "'"
                        '    If mDt.DefaultView.Count = 0 Then
                        '        ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_code: <b>" & shDr("item_code").ToString & "</b> ไม่มีอยู่ใน  QisDB.TB_ITEM.item_code"
                        '    End If

                        '    mDt.DefaultView.RowFilter = "item_name = '" & shDr("item_name") & "'"
                        '    If mDt.DefaultView.Count = 0 Then
                        '        ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString & "</b> ไม่มีอยู่ใน  QisDB.TB_ITEM.item_name"
                        '    End If

                        'Else
                        '    ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString & "</b> ไม่ได้อ้างอิงกับ QisDB.TB_ITEM"
                        'End If





                        'ใช้ชั่วคราวไปก่อน
                        If Convert.IsDBNull(shDr("id")) = False Then
                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.id: <b>" & shDr("id").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_ITEM.id"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and trim(item_code) = '" & shDr("item_code").ToString.Trim & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString.Trim & "</b> ฟิลด์ item_code: <b>" & shDr("item_code").ToString & "</b> ไม่ตรงกันกับ  QisDB.TB_ITEM.item_code"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and trim(item_name) = '" & shDr("item_name").ToString.Trim & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString & "</b> ไม่มีอยู่ใน  QisDB.TB_ITEM.item_name"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and trim(item_name_th) = '" & shDr("item_name_th").ToString.Trim & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString.Trim & "</b> ฟิลด์ item_name_th: <b>" & shDr("item_name_th").ToString & "</b> ไม่ตรงกันกับ  QisDB.TB_ITEM.item_name_th"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and item_time = '" & shDr("item_time") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString.Trim & "</b> ฟิลด์ item_time: <b>" & shDr("item_time").ToString & "</b> ไม่ตรงกันกับ  QisDB.TB_ITEM.item_time"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and item_wait = '" & shDr("item_wait") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString.Trim & "</b> ฟิลด์ item_wait: <b>" & shDr("item_wait").ToString & "</b> ไม่ตรงกันกับ  QisDB.TB_ITEM.item_wait"
                            End If

                            mDt.DefaultView.RowFilter = "id = '" & shDr("id") & "' and trim(txt_queue) = '" & shDr("txt_queue").ToString & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString.Trim & "</b> ฟิลด์ txt_queue:" & shDr("txt_queue").ToString & "</b> ไม่ตรงกันกับ  QisDB.TB_ITEM.txt_queue"
                            End If
                        Else
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_ITEM.item_name: <b>" & shDr("item_name").ToString & "</b> ไม่ได้อ้างอิงกับ QisDB.TB_ITEM"
                        End If
                    Next
                End If
                shDt = Nothing
            Else
                ret = "Not found data in QisDB.TB_ITEM"
            End If
            mDt = Nothing
            mLnq = Nothing
        End If
        trans.CommitTransaction()

        Return ret
    End Function

    Private Function ShopCustomertype(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim ret As String = ""

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim mLnq As New CenLinqDB.TABLE.TbCustomertypeCenLinqDB
            Dim mDt As New DataTable
            mDt = mLnq.GetDataList("1=1", "", trans.Trans)
            If mDt.Rows.Count > 0 Then
                Dim shDt As New DataTable
                Dim shLnq As New ShLinqDB.TABLE.TbCustomertypeShLinqDB
                shDt = shLnq.GetDataList("1=1", "", shTrans.Trans)
                shLnq = Nothing
                If shDt.Rows.Count > 0 Then
                    For Each shDr As DataRow In shDt.Rows
                        If Convert.IsDBNull(shDr("master_customertypeid")) = False Then
                            mDt.DefaultView.RowFilter = "id = '" & shDr("master_customertypeid") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_CUSTOMERTYPE.master_customertypeid: <b>" & shDr("master_customertypeid").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_CUSTOMERTYPE.id"
                            End If

                            mDt.DefaultView.RowFilter = "customertype_code = '" & shDr("customertype_code") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_CUSTOMERTYPE.customertype_code: <b>" & shDr("customertype_code").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_CUSTOMERTYPE.customertype_code"
                            End If

                            mDt.DefaultView.RowFilter = "customertype_name = '" & shDr("customertype_name") & "'"
                            If mDt.DefaultView.Count = 0 Then
                                ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_CUSTOMERTYPE.customertype_name: <b>" & shDr("customertype_name").ToString & "</b> ไม่มีอยู่ใน QisDB.TB_CUSTOMERTYPE.customertype_name"
                            End If
                        Else
                            ret += "<br />" & ShopLnq.SHOP_DB_NAME & ".TB_CUSTOMERTYPE.customertype_name: <b>" & shDr("customertype_name").ToString & "</b> ไม่ได้อ้างอิงกับ QisDB.TB_CUSTOMERTYPE"
                        End If
                    Next
                End If
                shDt = Nothing
            Else
                ret = "Not found data in QisDB.TB_CUSTOMERTYPE"
            End If
            mDt = Nothing
            mLnq = Nothing
        End If
        trans.CommitTransaction()

        Return ret
    End Function

    Public Sub SendMailErrorCompareMasterData(ByVal MailMsg As String)
        Dim dt As New DataTable
        Dim lnq As New CenLinqDB.TABLE.TbEmailBatchAlertCenLinqDB
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        dt = lnq.GetDataList("convert(varchar(8),getdate(),112) between convert(varchar(8),ef_date,112) and convert(varchar(8),isnull(ep_date,getdate()),112) ", "", trans.Trans)
        Dim MailSjb As String = "Master setup was changed on " & DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))

        If dt.Rows.Count > 0 Then
            Dim LoopStep As Integer = 50
            Dim LoopCount As Integer = Math.Ceiling(dt.Rows.Count / LoopStep)
            For i As Integer = 0 To LoopCount - 1
                Dim MailTo As String = ""
                For j As Integer = 0 To LoopStep - 1
                    Dim RowIndex As Integer = j + (LoopStep * i)
                    If RowIndex < dt.Rows.Count Then
                        Dim dr As DataRow = dt.Rows(RowIndex)
                        If MailTo = "" Then
                            MailTo = dr("email_addr").ToString
                        Else
                            MailTo += ";" & dr("email_addr").ToString
                        End If
                    End If
                Next

                Dim gw As New Engine.GateWay.GateWayServiceENG
                gw.SendEmail(MailTo, MailSjb, MailMsg)
                gw = Nothing
            Next
        End If
        dt = Nothing
        trans.CommitTransaction()
        lnq = Nothing
    End Sub
End Class
