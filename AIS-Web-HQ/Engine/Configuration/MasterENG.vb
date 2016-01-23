Imports CenParaDB.TABLE
Imports CenLinqDB.TABLE
Imports CenLinqDB.Common.Utilities
Imports System.Data.SqlClient
Imports Engine.Common
Imports ShLinqDB.TABLE

Namespace Configuration
    Public Class MasterENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function GetServiceActiveList(ByVal Whtext As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbItemCenLinqDB
            Dim sql As String = "select *, "
            sql += " 'ดีมาก' as very_good,'ดี' as good, 'ควรปรับปรุง' as improve"
            sql += " from tb_item where " & Whtext & " and active_status='1' order by item_order"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans) 'lnq.GetDataList(Whtext & " and active_status='1'", "item_order", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetServiceByAppointMent() As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim sql As String = "select distinct TB_ITEM.* from TB_ITEM " & _
            " Left Join TB_SKILL_ITEM on TB_ITEM.id=TB_SKILL_ITEM.item_id " & _
            " Left Join TB_SKILL on TB_SKILL.id=TB_SKILL_ITEM.skill_id " & _
            " where appointment = 1  " & _
            " and TB_ITEM.active_status='1'"
            Dim dt As New DataTable
            dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetServiceAllList(ByVal Whtext As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbItemCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(Whtext, "item_order", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetCustomerTypeList(ByVal Whtext As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbCustomertypeCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(Whtext, "customertype_name", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetServicePara(ByVal ServiceID As Integer) As TbItemCenParaDB
            Dim p As New TbItemCenParaDB
            Dim lnq As New TbItemCenLinqDB
            p = lnq.GetParameter(ServiceID, Nothing)
            lnq = Nothing

            Return p
        End Function

        Public Function GetCustomerTypePara(ByVal ID As Integer) As TbCustomertypeCenParaDB
            Dim p As New TbCustomertypeCenParaDB
            Dim lnq As New TbCustomertypeCenLinqDB
            p = lnq.GetParameter(ID, Nothing)
            lnq = Nothing

            Return p
        End Function

        Public Function GetMasterTitleList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbTitleCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(WhText, "title_name", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetCategoryList() As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim sql As String = "select mapping_code value, display_value display from TB_CUSTOMER_MAPPING where field_name='GROUP_CODE' order by id "
            Dim dt As New DataTable
            dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetContactClass() As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim sql As String = "select mapping_code value, display_value display from TB_CUSTOMER_MAPPING where field_name='CUST_CLASS' order by mapping_code "
            Dim dt As New DataTable
            dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetSegmentList() As DataTable
            _err = ""
            Dim sql As String = "select segment_name value, segment_name display from tb_segment order by segment_name"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
        Public Function GetNationalityList() As DataTable
            _err = ""
            Dim sql As String = "select nationality_name value, nationality_name display from tb_nationality  order by nationality_name"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function


        Public Function GetUserList(ByVal WhText As String, ByVal MasterItemID As String, ByVal ShopList As String) As DataTable
            _err = ""
            Dim ret As New DataTable
            ret.Columns.Add("shop_id")
            ret.Columns.Add("shop_name")
            ret.Columns.Add("shop_code")
            ret.Columns.Add("user_id")
            ret.Columns.Add("username")
            ret.Columns.Add("staff_name")
            ret.Columns.Add("fname")
            ret.Columns.Add("lname")
            ret.Columns.Add("master_itemid")
            ret.Columns.Add("UQID")

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans IsNot Nothing Then
                For Each sh As String In Split(ShopList, ",")
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(sh)
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq)

                    Dim sql As String = ""
                    sql += " select distinct u.id, isnull(t.title_name ,'') + u.fname + ' ' + u.lname staff_name, u.fname, u.lname, u.username "
                    sql += " from tb_user u"
                    sql += " left join TB_TITLE t on t.id=u.title_id"
                    sql += " inner join TB_USER_SKILL us on u.id=us.user_id"
                    sql += " inner join TB_SKILL s on s.id=us.skill_id"
                    sql += " inner join TB_SKILL_ITEM si on s.id=si.skill_id"
                    sql += " inner join TB_ITEM i on i.id=si.item_id"
                    sql += " where u.active_status = '1' and s.active_status='1' "
                    If MasterItemID.Trim <> "" Then
                        sql += " and i.master_itemid in (" & MasterItemID & ")"
                    End If

                    Dim uDt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
                    shTrans.CommitTransaction()
                    If uDt.Rows.Count > 0 Then
                        For Each uDr As DataRow In uDt.Rows
                            Dim drR As DataRow = ret.NewRow
                            drR("shop_id") = shLnq.ID
                            drR("shop_name") = shLnq.SHOP_NAME_EN
                            drR("shop_code") = shLnq.SHOP_CODE
                            drR("user_id") = uDr("id")
                            drR("username") = uDr("username")
                            drR("staff_name") = uDr("staff_name")
                            drR("fname") = uDr("fname")
                            drR("lname") = uDr("lname")
                            drR("UQID") = shLnq.ID & uDr("id").ToString
                            ret.Rows.Add(drR)
                        Next
                        uDt.Dispose()
                        uDt = Nothing
                        trans.CommitTransaction()
                    End If
                Next
            End If

            If ret.Rows.Count > 0 Then
                If WhText.Trim <> "" Then
                    ret.DefaultView.RowFilter = WhText
                End If

                ret.DefaultView.Sort = "shop_name,fname,lname"
                ret = ret.DefaultView.ToTable
            End If

            Return ret
        End Function

        Public Function GetShopPara(ByVal ShopID As Long) As CenParaDB.TABLE.TbShopCenParaDB
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim para As New CenParaDB.TABLE.TbShopCenParaDB
            para = lnq.GetParameter(ShopID, trans.Trans)
            trans.CommitTransaction()
            Return para
        End Function

        Public Function GetTWLocationPara(ByVal ShopID As Long) As CenParaDB.TABLE.TwLocationCenParaDB
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            Dim para As New CenParaDB.TABLE.TwLocationCenParaDB
            para = lnq.GetParameter(ShopID, trans.Trans)
            trans.CommitTransaction()
            Return para
        End Function

        Public Function GetBuildingList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbBuildingCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(WhText, "building_name_en", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function

        Public Function GetShopList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim sql As String = "select sh.id,sh.shop_code,sh.shop_name_en,"
            sql += " rg.id region_id, rg.region_name_en,sh.shop_size,rg.region_code "
            sql += " from TB_SHOP sh"
            sql += " inner join TB_REGION rg on rg.id=sh.region_id"
            sql += " where " & WhText & " and sh.active='Y'"
            sql += " order by sh.shop_name_en"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetShopListByUser(ByVal WhText As String, ByVal UserName As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim dt As New DataTable
            If UserName.ToUpper <> "ADMIN" Then
                Dim sql As String = "select sh.id,sh.shop_code,sh.shop_name_en,"
                sql += " rg.id region_id, rg.region_name_en,sh.shop_size "
                sql += " from TB_SHOP sh"
                sql += " inner join TB_REGION rg on rg.id=sh.region_id"
                sql += " inner join tb_user_shop us on sh.id=us.tb_shop_id"
                sql += " inner join tb_user u on u.id=us.tb_user_id"
                sql += " where " & WhText & " and u.username = '" & UserName & "' and sh.active='Y'"
                sql += " order by sh.shop_name_en"
                dt = lnq.GetListBySql(sql, trans.Trans)
            Else
                dt = FunctionEng.GetActiveShop()
            End If

            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetMasterServicePara(ByVal ServiceID As Long) As TbItemCenParaDB
            Dim p As New TbItemCenParaDB
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            p = lnq.GetParameter(ServiceID, trans.Trans)
            lnq = Nothing
            Return p
        End Function

        Public Function SaveMasterService(ByVal UserName As String, ByVal p As TbItemCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If

            lnq.ITEM_CODE = p.ITEM_CODE
            lnq.ITEM_NAME = p.ITEM_NAME
            lnq.ITEM_NAME_TH = p.ITEM_NAME_TH
            lnq.ITEM_TIME = p.ITEM_TIME
            lnq.ITEM_WAIT = p.ITEM_WAIT
            lnq.ITEM_ORDER = p.ITEM_ORDER
            lnq.TXT_QUEUE = p.TXT_QUEUE
            lnq.COLOR = p.COLOR
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.Q_TYPE_ID = p.Q_TYPE_ID
            lnq.APP_MIN_QUEUE = p.APP_MIN_QUEUE
            lnq.APP_MAX_QUEUE = p.APP_MAX_QUEUE
            lnq.BRAND_NAME = p.BRAND_NAME

            Dim isInsert As Boolean = False
            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
                isInsert = True
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
                Return ret
            Else
                trans.CommitTransaction()
            End If

            If ret = True And isInsert = False Then


                Dim shDT As New DataTable
                shDT = FunctionEng.GetActiveShopDDL
                If shDT.Rows.Count > 0 Then
                    For i As Integer = 0 To shDT.Rows.Count - 1
                        Dim _shopid As String = shDT.Rows(i).Item("ID").ToString
                        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(_shopid)
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = Engine.Common.FunctionEng.GetShTransction(_shopid, "MasterENG.SaveShopItem")
                        If shTrans.Trans IsNot Nothing Then
                            Dim shItemLnq As New TbItemShLinqDB
                            shItemLnq.ChkDataByWhere("master_itemid='" & p.ID & "'", shTrans.Trans)
                            If shItemLnq.ID > 0 Then
                                shItemLnq.MASTER_ITEMID = p.ID
                                shItemLnq.ITEM_CODE = p.ITEM_CODE
                                shItemLnq.ITEM_NAME = p.ITEM_NAME
                                shItemLnq.ITEM_NAME_TH = p.ITEM_NAME_TH
                                shItemLnq.ITEM_TIME = p.ITEM_TIME
                                shItemLnq.ITEM_WAIT = p.ITEM_WAIT
                                shItemLnq.TXT_QUEUE = p.TXT_QUEUE
                                shItemLnq.COLOR = p.COLOR
                                shItemLnq.ACTIVE_STATUS = p.ACTIVE_STATUS
                                shItemLnq.APP_MIN_QUEUE = p.APP_MIN_QUEUE
                                shItemLnq.APP_MAX_QUEUE = p.APP_MAX_QUEUE
                                shItemLnq.TB_ITEM_GROUP_ID = p.TB_ITEM_GROUP_ID
                                shItemLnq.BRAND_NAME = p.BRAND_NAME
                                ret = shItemLnq.UpdateByPK(UserName, shTrans.Trans)
                            End If

                            If ret = False Then
                                shTrans.RollbackTransaction()
                                _err = "MasterENG.SaveShopItem :" & shLnq.SHOP_ABB & " ### " & shItemLnq.ErrorMessage
                                'Exit For
                            Else
                                shTrans.CommitTransaction()
                            End If
                        Else
                            _err = shLnq.SHOP_ABB & " ### " & shTrans.ErrorMessage
                            ret = False
                            'Exit For
                        End If
                    Next
                End If
            End If

            Return ret
        End Function

        Public Function CheckDuplicateMasterServiceByItemCode(ByVal ServiceID As Long, ByVal ItemCode As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            ret = lnq.ChkDataByWhere("item_code = '" & ItemCode & "'and id<>'" & ServiceID & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function

        Public Function CheckDuplicateMasterServiceByItemNameEng(ByVal ServiceID As Long, ByVal ItemNameEng As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            ret = lnq.ChkDataByWhere("item_name = '" & ItemNameEng & "'and id<>'" & ServiceID & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function

        Public Function CheckDuplicateMasterServiceByItemNameTH(ByVal ServiceID As Long, ByVal ItemNameTH As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            ret = lnq.ChkDataByWhere("item_name_th = '" & ItemNameTH & "'and id<>'" & ServiceID & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function

        Public Function CheckDuplicateMasterServiceByTextQueue(ByVal ServiceID As Long, ByVal TextQueue As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            ret = lnq.ChkDataByWhere("txt_queue = '" & TextQueue & "'and id<>'" & ServiceID & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function

        Public Function CheckDuplicateMasterServiceByItemOrder(ByVal ServiceID As Long, ByVal ItemOrder As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbItemCenLinqDB
            ret = lnq.ChkDataByWhere("item_order = '" & ItemOrder & "'and id<>'" & ServiceID & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function


        Public Sub New()
            _err = ""
        End Sub
#Region "Skill"
        Public Function GetSkillPara(ByVal SkillID As Long) As TbSkillCenParaDB
            Dim p As New TbSkillCenParaDB
            Dim trans As New TransactionDB
            Dim lnq As New TbSkillCenLinqDB
            p = lnq.GetParameter(SkillID, trans.Trans)
            lnq = Nothing
            Return p
        End Function

        Public Function GetSkillList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbSkillCenLinqDB
            Dim sql As String = "select id,skill,appointment,"
            sql += " active_status "
            sql += " from TB_SKILL "
            sql += " where " & WhText & " "
            sql += " order by skill"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetSkillItem(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim sql As String = "select s.id,s.skill_id,s.item_id,t.item_name, t.item_code"
            sql += " from TB_SKILL_ITEM s"
            sql += " inner join TB_ITEM t on t.id=s.item_id"
            sql += " where " & WhText & " "
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetSkillItem(ByVal SkillID As Long) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim sql As String = "select s.id,s.skill_id,s.item_id,t.item_name, t.item_code,t.id master_itemid"
            sql += " from TB_ITEM t "
            sql += " left join TB_SKILL_ITEM s on t.id=s.item_id"
            sql += " where skill_id='" & SkillID & "' or t.id is null "
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function SaveSkill(ByVal UserName As String, ByVal p As TbSkillCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbSkillCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If

            lnq.ID = p.ID
            lnq.SKILL = p.SKILL
            lnq.APPOINTMENT = p.APPOINTMENT
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                p.ID = lnq.ID
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function SaveSkillItem(ByVal UserName As String, ByVal p As TbSkillItemCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbSkillItemCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If
            lnq.SKILL_ID = p.SKILL_ID
            lnq.ITEM_ID = p.ITEM_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function DeleteSkillItem(ByVal skill_id As String)
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Try
                Dim uSql As String = "delete from tb_skill_item "
                uSql += " where skill_id = '" & skill_id & "' "
                SqlDB.ExecuteNonQuery(uSql, trans.Trans)
                trans.CommitTransaction()
                ret = True
            Catch ex As Exception
                _err = ex.Message.ToString
                trans.RollbackTransaction()
                ret = False
            End Try

            Return ret
        End Function

        Public Function CheckDuplicateSkill(ByVal id As Long, ByVal Skill As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbSkillCenLinqDB
            ret = lnq.ChkDataByWhere("skill = '" & Skill & "'and id <>'" & id & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function
#End Region '__Skill


#Region "__HoldReason"
        Public Function GetHoldReasonPara(ByVal HoldReasonID As Long) As TbHoldReasonCenParaDB
            Dim p As New TbHoldReasonCenParaDB
            Dim trans As New TransactionDB
            Dim lnq As New TbHoldReasonCenLinqDB
            p = lnq.GetParameter(HoldReasonID, trans.Trans)
            lnq = Nothing
            Return p
        End Function

        Public Function GetHoldReasonList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbHoldReasonCenLinqDB
            Dim sql As String = "select id,name,productive,"
            sql += " active_status "
            sql += " from TB_HOLD_REASON "
            sql += " where " & WhText & " "
            sql += " order by name"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function SaveHoldReason(ByVal UserName As String, ByVal p As TbHoldReasonCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbHoldReasonCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If
            lnq.NAME = p.NAME
            lnq.PRODUCTIVE = p.PRODUCTIVE
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                p.ID = lnq.ID
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function CheckDuplicateHoldReason(ByVal id As Long, ByVal Reason As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbHoldReasonCenLinqDB
            ret = lnq.ChkDataByWhere(" NAME = '" & Reason & "' and id <>'" & id & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function
#End Region '__HoldReason


#Region "__LogoutReason"
        Public Function GetLogoutReasonPara(ByVal LogoutReasonID As Long) As TbLogoutReasonCenParaDB
            Dim p As New TbLogoutReasonCenParaDB
            Dim trans As New TransactionDB
            Dim lnq As New TbLogoutReasonCenLinqDB
            p = lnq.GetParameter(LogoutReasonID, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return p
        End Function

        Public Function GetLogoutReasonList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbLogoutReasonCenLinqDB
            Dim sql As String = "select id,name,productive,"
            sql += " active_status "
            sql += " from TB_LOGOUT_REASON "
            sql += " where " & WhText & " "
            sql += " order by name"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function SaveLogoutReason(ByVal UserName As String, ByVal p As TbLogoutReasonCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbLogoutReasonCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If
            lnq.NAME = p.NAME
            lnq.PRODUCTIVE = p.PRODUCTIVE
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                p.ID = lnq.ID
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function CheckDuplicateLogoutReason(ByVal id As Long, ByVal Reason As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbLogoutReasonCenLinqDB
            ret = lnq.ChkDataByWhere(" NAME = '" & Reason & "' and id <>'" & id & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function
#End Region '__LogoutReason

#Region "Master User"
        Public Function GetMasterUserPara(ByVal MasterUserID As Long) As TbUserCenParaDB
            Dim p As New TbUserCenParaDB
            Dim trans As New TransactionDB
            Dim lnq As New TbUserCenLinqDB
            p = lnq.GetParameter(MasterUserID, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return p
        End Function

        Public Function GetMasterUserList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbUserCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList(WhText, "fname,lname", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function SaveMasterUser(ByVal UserName As String, ByVal p As TbUserCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbUserCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If

            lnq.TITLE_ID = p.TITLE_ID
            lnq.USER_CODE = p.USER_CODE
            lnq.FNAME = p.FNAME
            lnq.LNAME = p.LNAME
            lnq.POSITION = p.POSITION
            lnq.USERNAME = p.USERNAME
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                p.ID = lnq.ID
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function CheckDuplicateUsername(ByVal id As Long, ByVal UserName As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbUserCenLinqDB
            ret = lnq.ChkDuplicateByUSERNAME(UserName, id, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function
#End Region  '__Master User

#Region "Master Shop Menu"
        Public Function GetMasterShopMenu() As DataTable
            'order by menu_type,menu_name
            Dim lnq As New TbMasterShopMenuCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList("1=1", "menu_type,menu_name", Nothing)
            lnq = Nothing
            Return dt
        End Function
#End Region

#Region "TB_REGION"
        Public Function GetRegionAllList(ByVal OrderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New TbRegionCenLinqDB
            Dim sql As String = "select id, region_name_th, region_name_en from tb_region where active='Y' order by " & OrderBy
            dt = lnq.GetListBySql(sql, trans.Trans)
            dt.TableName = "RegionAllList"
            trans.CommitTransaction()
            lnq = Nothing
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If
        End Function
#End Region

#Region "TW"

        Public Function GetCategory(ByVal module_name As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New TwLocationCenLinqDB
            Dim sql As String = "select distinct category_name as id,category_name from TB_SIEBEL_ACTIVITY_CATEGORY  "
            sql += " where active_status='Y' and module_name = '" & module_name & "' order by category_name"
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetSubCategory(ByVal catagory_name As String, ByVal module_name As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New TwLocationCenLinqDB
            Dim sql As String = "select distinct subcategory_name as id,subcategory_name from TB_SIEBEL_ACTIVITY_SUBCATEGORY"
            sql += " where active_status='Y' and module_name ='" & module_name & "' and category_name ='" & catagory_name & "' order by subcategory_name"
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetTWRegionAllList() As DataTable
            Dim dt As New DataTable
            Dim trans As New TransactionDB
            Dim lnq As New TwLocationCenLinqDB
            Dim sql As String = "select distinct region_code as code,region_code as name from TW_LOCATION order by region_code"
            dt = lnq.GetListBySql(sql, trans.Trans)
            dt.TableName = "RegionAllList"
            trans.CommitTransaction()
            lnq = Nothing
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If
        End Function

        Public Function GetTWShopList(ByVal WhText As String, ByVal LocationName As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            Dim sql As String = "select id,location_code,isnull(location_name_en,location_name_th) location_name_en, "
            sql += " region_code,province_code,location_type,location_segment, "
            sql += " location_code + ' ' + isnull(location_name_en,location_name_th) location_name_ddl"
            sql += " from TW_LOCATION"
            sql += " where 1=1 " & WhText & " and active='Y' "
            If LocationName.Trim.Length = 1 Then
                sql += " and (left(location_name_th,1)='" & LocationName & "' or convert(varchar,location_name_th) like '%" & LocationName & "%' "
                sql += " or left(location_name_en,1)='" & LocationName & "' or convert(varchar,location_name_en) like '%" & LocationName & "%' )"
            Else
                sql += " and (location_name_th like '%" & LocationName & "%' or location_name_en like '%" & LocationName & "%')"
            End If
            sql += " order by region_code,province_code,location_name_en"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetTWProvinceList(ByVal RegionCode As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            Dim sql As String = "select distinct province_code as code,province_code as name from TW_LOCATION where region_code='" & RegionCode & "' order by province_code"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetTWLocationTypeList() As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            Dim sql As String = "select distinct location_type as code,location_type as name from TW_LOCATION order by location_type"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetTWSegmentList() As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            Dim sql As String = "select distinct upper(location_segment) as code,upper(location_segment) as name from TW_LOCATION order by upper(location_segment)"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetTWSffOrderTypeList(ByVal wh As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            Dim sql As String = "select id ,order_type_name from TW_SFF_ORDER_TYPE  where active ='Y' and " & wh & " order by order_type_name"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function


#End Region 'TW


    End Class
End Namespace

