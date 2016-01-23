Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
'Imports CheckLDAP
'Namespace AISQsharp
Public Class utils
    Inherits System.Web.UI.Page

    Public Structure FtpInfo
        Dim Host As String
        Dim User As String
        Dim Password As String
    End Structure
    Public Structure FunctionReturn
        Dim Result As Boolean
        Dim Msg As String
    End Structure
    Public Shadows Structure User
        Dim user_id As String
        Dim user_code As String
        Dim username As String
        Dim fulllname As String
        Dim counter_id As String
        Dim counter_name As String
        Dim item_id As String
        Dim ip_address As String
        Dim is_admin As Boolean
        Dim view_others_vdo As String
        Dim login_history_id As Long
    End Structure

    Public myUser As User = Nothing

    Public Shared Function GetVersion() As String
        Return System.Configuration.ConfigurationManager.AppSettings("Version")
    End Function

    Public Function GetShopList() As DataTable
        Dim Sql As String = "Select id as shop_id, "
        Sql += " ISNULL(shop_name_en,'')+(case when shop_name_th IS  null then '' else ' ('+shop_name_th+')' end) as shop_name,"
        Sql += " * from TB_SHOP where active = 'Y' "
        Sql += " order by shop_abb"

        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.ExecuteDataTable(Sql)
        Return dt
    End Function

    Public Function GetShopListByUser(ByVal conn As SqlConnection, ByVal view_others_vdo As String, ByVal UserID As String) As DataTable
        Dim Sql As String = "Select id as shop_id, "
        Sql += " ISNULL(shop_name_en,'')+(case when shop_name_th IS  null then '' else ' ('+shop_name_th+')' end) as shop_name,"
        Sql += " * from TB_SHOP where active = 'Y' "
        If view_others_vdo.Trim <> "" Then
            Sql += " and id in(" & view_others_vdo & ") "
        Else
            Sql += " and id = 0 "
        End If
        Sql += " union "
        Sql += " select tb_shop_id shop_id,"
        Sql += " ISNULL(s.shop_name_en,'')+(case when s.shop_name_th IS  null then '' else ' ('+s.shop_name_th+')' end) as shop_name,"
        Sql += " s.*"
        Sql += " from tb_user_shop us"
        Sql += " inner join tb_shop s on s.id=us.tb_shop_id"
        Sql += " where s.active = 'Y' and us.tb_user_id = '" & UserID & "'"
        Sql += " order by shop_abb"

        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(Sql, conn)
        Try
            da.Fill(dt)
        Catch ex As Exception
            dt = New DataTable
        End Try
        Return dt
    End Function

    Function GetMenuList(ByVal conn As SqlConnection) As DataTable
        Dim Sql As String = "use " & conn.Database & ";Select id as menu_id,menu_type,menu_name from TB_menu order by menu_name"
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(Sql, conn)
        Try
            da.Fill(dt)
        Catch ex As Exception
            dt = New DataTable
        End Try
        Return dt
    End Function

    'Function GetShopList(ByVal conn As SqlConnection, ByVal shopID As String) As DataTable
    '    Dim Sql As String = "use " & conn.Database & ";Select id as shop_id,ISNULL(shop_name_en,'')+(case when shop_name_th IS  null then '' else ' ('+shop_name_th+')' end) as shop_name,* from TB_SHOP where id='" & shopID & "' "
    '    Dim dt As New DataTable
    '    Dim da As New SqlDataAdapter(Sql, conn)
    '    Try
    '        da.Fill(dt)
    '    Catch ex As Exception
    '        dt = New DataTable
    '    End Try
    '    Return dt
    'End Function
    'Function GetServiceLogByShopAndDate(ByVal ShopID As String, ByVal D As Date, ByVal D2 As Date, ByVal conn As SqlConnection) As DataTable
    '    Dim dt1 As New DataTable
    '    dt1 = GetShopList(conn, ShopID)
    '    Dim tmpDate As String = ""
    '    tmpDate = "and service_date between '" & IIf(D.Year > 2500, D.Year - 543, D.Year) & D.Month.ToString.PadLeft(2, "0") & D.Day.ToString.PadLeft(2, "0") & "' and dateadd(s,86399,'" & IIf(D2.Year > 2500, D2.Year - 543, D2.Year) & D2.Month.ToString.PadLeft(2, "0") & D2.Day.ToString.PadLeft(2, "0") & "')"
    '    Dim Sql As String = ""
    '    If dt1.Rows.Count = 0 Then
    '        Return New DataTable
    '    End If

    '    Sql &= "use " & dt1.Rows(0)("shop_db_name") & "; select TB_COUNTER_QUEUE.id,service_date,user_id,isnull(title_name,'') + ' ' + isnull(fname,'') + ' ' + isnull(lname,'') as agent"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 9 and datepart(MINUTE,start_time) between 0 and 59 "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T9_10"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 10 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T10_11"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 11 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T11_12"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 12 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T12_13"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 13 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T13_14"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 14 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T14_15"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 15 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T15_16"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 16 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T16_17"
    '    Sql &= " ,case when datepart(HOUR,start_time) = 17 and datepart(MINUTE,start_time) between 0 and 59  "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T17_18"
    '    Sql &= " ,case when datepart(HOUR,start_time) >= 18 "
    '    Sql &= " then item_code + '/' + queue_no else '' end as T18 "
    '    Sql &= " "
    '    Sql &= " from TB_COUNTER_QUEUE left join TB_USER on TB_COUNTER_QUEUE.user_id = TB_USER.id left join TB_TITLE on TB_USER.title_id = TB_TITLE.id left join tb_item on TB_COUNTER_QUEUE.item_id = tb_item.id inner join TB_CAPTURE_LOG on TB_COUNTER_QUEUE.id = TB_CAPTURE_LOG.QueueId  and datediff(D,TB_CAPTURE_LOG.create_date,TB_COUNTER_QUEUE.service_date)=0 "
    '    Sql &= " where status = 3 " & tmpDate
    '    dt1.Dispose()

    '    Dim dt As New DataTable
    '    Dim da As New SqlDataAdapter(Sql, conn)
    '    Try
    '        da.Fill(dt)
    '    Catch ex As Exception
    '        dt = New DataTable
    '    End Try
    '    Return dt
    'End Function

    Function GetServiceLog(ByVal ShopID As String, ByVal Agent As String, ByVal Service As String, ByVal Q As String, ByVal mob As String, ByVal TimeFrom As String, ByVal TimeTo As String, ByVal dfrom As String, ByVal dto As String, ByVal CounterID As String) As DataTable
        Dim whLog As String = ""
        Dim Sql As String = ""
        Dim tmpDate As String = ""
        tmpDate = " and convert(varchar(8),q.service_date,112) between '" & dfrom & "' and '" & dto & "'" & vbNewLine

        whLog = "วันที่ระหว่าง " & dfrom & " ถึง " & dto
        Dim tmpSql As String = ""
        If Agent.Trim <> "" Then
            tmpSql += " and q.user_id = '" & Agent & "'" & vbNewLine
            whLog += ":USER_ID= '" & Agent & "'"
        End If
        If Q.Trim <> "" Then
            tmpSql += "  and q.queue_no like '%" & Q & "%' " & vbNewLine
            whLog += ":คิวที่ " & Q
        End If
        If Service.Trim <> "" Then
            tmpSql += "  and tb_item.id = '" & Service & "' " & vbNewLine
            whLog += ":TB_ITEM.id=" & Service
        End If
        If mob.Trim <> "" Then
            tmpSql += " and ISNULL(q.customer_id,'') like '%" & mob & "%' " & vbNewLine
            whLog += ":MobileNo=" & mob
        End If
        If TimeFrom <> "0" Then
            tmpSql += " and convert(varchar(5),q.start_time,114) >= '" & TimeFrom & "' " & vbNewLine
            whLog += ":เวลาตั้งแต่ " & TimeFrom
        End If
        If TimeTo <> "0" Then
            tmpSql += " and convert(varchar(5),q.start_time,114) <= '" & TimeTo & "'" & vbNewLine
            whLog += ":ถึงเวลา " & TimeTo
        End If
        If CounterID.Trim <> "" Then
            tmpSql += " and q.counter_id = '" & CounterID & "'" & vbNewLine
            whLog += ":TB_COUNTER.id=" & CounterID
        End If

        Dim LoginHisID As Long = DirectCast(Session("MyUser"), utils.User).login_history_id
        Engine.Common.FunctionEng.SaveTransLog(LoginHisID, "QM.utils.GetServiceLog", "ค้นหาด้วยเงื่อนไข " & whLog)

        Dim dt As New DataTable

        'Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
        'shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
        'If shLnq.ID <> 0 Then
        Sql = " select q.id as [id],q.service_date,convert(varchar,q.service_date,12) as file_date,"
        Sql += " q.user_id,isnull(fname,'') + ' ' + isnull(lname,'') as agent,tb_item.id as iid,q.customer_id as MOBILE_NO" & vbNewLine
        Sql &= " ,convert(varchar(8),q.start_time,114) + ' - ' + convert(varchar(8),q.end_time,114) as time" & vbNewLine
        Sql &= " ,q.queue_no,TB_item.item_name , tb_counter.counter_name , LTRIM(str(q.id))+'###'+right(CONVERT(varchar(8),q.service_date,112),6) command_argument, " & vbNewLine
        Sql += " isnull(qm.status,'7') qm_status,case qm.status when '1' then 'File is merging' " & vbNewLine
        Sql += "                when '2' then 'File is already merged' " & vbNewLine
        Sql += "                when '3' then 'File is transferring from Temp Folder' " & vbNewLine
        Sql += "                when '4' then 'File is already exist' " & vbNewLine
        Sql += "                when '5' then 'File is moving to Backup Folder' " & vbNewLine
        Sql += "                when '6' then 'File is transferring from Backup Folder' " & vbNewLine
        Sql += "                when '7' then 'File not found' " & vbNewLine
        Sql += "                else '' " & vbNewLine
        Sql += " end as qm_status_name " & vbNewLine
        Sql &= " from TB_COUNTER_QUEUE q " & vbNewLine
        Sql += " left join TB_USER on q.user_id = TB_USER.id " & vbNewLine
        Sql += " left join TB_TITLE on TB_USER.title_id = TB_TITLE.id " & vbNewLine
        Sql += " left join tb_item on q.item_id = tb_item.id " & vbNewLine
        Sql += " left join tb_counter on q.counter_id=tb_counter.id" & vbNewLine
        Sql += " left join tb_qm_transfer_log qm on CONVERT(varchar(19),qm.service_date,120)=CONVERT(varchar(19),q.service_date,120) " & vbNewLine
        Sql += "        and qm.tb_counter_queue_id=q.id " & vbNewLine
        Sql &= " where q.status = 3 " & tmpDate
        Sql &= tmpSql
        Sql += "UNION ALL" & vbNewLine
        Sql += " select q.tb_counter_queue_id as [id],q.service_date,convert(varchar,q.service_date,12) as file_date,"
        Sql += " q.user_id,isnull(fname,'') + ' ' + isnull(lname,'') as agent,tb_item.id as iid,q.customer_id as MOBILE_NO" & vbNewLine
        Sql &= " ,convert(varchar(8),q.start_time,114) + ' - ' + convert(varchar(8),q.end_time,114) as time" & vbNewLine
        Sql &= " ,q.queue_no,TB_item.item_name , tb_counter.counter_name ,LTRIM(str(q.tb_counter_queue_id))+'###'+right(CONVERT(varchar(8),q.service_date,112),6) command_argument," & vbNewLine
        Sql += " isnull(qm.status,'7') qm_status,case qm.status when '1' then 'File is merging' " & vbNewLine
        Sql += "                when '2' then 'File is already merged' " & vbNewLine
        Sql += "                when '3' then 'File is transferring from Temp Folder' " & vbNewLine
        Sql += "                when '4' then 'File is already exist' " & vbNewLine
        Sql += "                when '5' then 'File is moving to Backup Folder' " & vbNewLine
        Sql += "                when '6' then 'File is transferring from Backup Folder' " & vbNewLine
        Sql += "                when '7' then 'File not found' " & vbNewLine
        Sql += "                else '' " & vbNewLine
        Sql += " end as qm_status_name " & vbNewLine
        Sql &= " from TB_COUNTER_QUEUE_HISTORY q " & vbNewLine
        Sql += " left join TB_USER on q.user_id = TB_USER.id " & vbNewLine
        Sql += " left join TB_TITLE on TB_USER.title_id = TB_TITLE.id " & vbNewLine
        Sql += " left join tb_item on q.item_id = tb_item.id " & vbNewLine
        Sql += " left join tb_counter on q.counter_id=tb_counter.id" & vbNewLine
        Sql += " left join tb_qm_transfer_log qm on CONVERT(varchar(19),qm.service_date,120)=CONVERT(varchar(19),q.service_date,120) " & vbNewLine
        Sql += "        and qm.tb_counter_queue_id=q.tb_counter_queue_id " & vbNewLine
        Sql &= " where q.status = 3 " & tmpDate
        Sql &= tmpSql

        dt = Engine.Common.FunctionEng.ExecuteShopSQL(Sql, ShopID, "WebQM_App_code_utils.GetServiceLog")

        Return dt
    End Function



    'Function GetServiceLogAllShop(ByVal Agent As String, ByVal Service As String, ByVal Q As String, ByVal mob As String, ByVal conn As SqlConnection, ByVal dfrom As String, ByVal dto As String, ByVal ST As String, ByVal ET As String) As DataTable
    '    Dim dt1 As New DataTable
    '    dt1 = GetShopList(conn)
    '    Dim Sql As String = ""
    '    If dt1.Rows.Count = 0 Then
    '        Return New DataTable
    '    End If
    '    Dim tmpDate As String = ""
    '    tmpDate = " and convert(varchar(8),service_date,112) between '" & dfrom & "' and '" & dto & "'"
    '    Dim tmpSql As String = ""
    '    If Agent.Trim <> "" Then
    '        tmpSql += " and user_id = '" & Agent & "'"
    '    End If
    '    If Q.Trim <> "" Then
    '        tmpSql += "  and TB_COUNTER_QUEUE.queue_no like '%" & Q & "%' "
    '    End If
    '    If Service.Trim <> "" Then
    '        tmpSql += "  and tb_item.id = '" & Service & "' "
    '    End If
    '    If mob.Trim <> "" Then
    '        tmpSql += " and ISNULL(customer_id,'') like '%" & mob & "%' "
    '    End If
    '    If ST.Trim <> "" And ET.Trim <> "" Then
    '        tmpSql += " and convert(varchar(5),start_time,114) >= '" & ST & "' and convert(varchar(5),start_time,114) < '" & ET & "'"
    '    End If

    '    Dim dt As New DataTable
    '    Dim dtRet As New DataTable
    '    Dim tmp As String = "shop_" & Session("DBTYPE") & "_name"

    '    For i As Integer = 0 To dt1.Rows.Count - 1  'Loop All Shop
    '        Sql = "use " & dt1.Rows(i)(tmp) & _
    '        "; select '" & dt1.Rows(i)("id") & "' as shop_id,'" & dt1.Rows(i)("shop_name_en").ToString & IIf(dt1.Rows(i)("shop_name_th").ToString <> "", " (" & dt1.Rows(i)("shop_name_en").ToString & ")", "") & "' as shop_name,queue_no,TB_COUNTER_QUEUE_history.tb_counter_queue_id,convert(varchar,service_date,101) as service_date,user_id,isnull(fname,'') + ' ' + isnull(lname,'') as agent,tb_item.id as iid,customer_id,item_code,convert(varchar,start_time,108) as start_time,convert(varchar,end_time,108) as end_time,convert(varchar,service_date,12) as fdate "

    '        Sql &= " from TB_COUNTER_QUEUE_HISTORY left join TB_USER on TB_COUNTER_QUEUE_HISTORY.user_id = TB_USER.id left join tb_item on TB_COUNTER_QUEUE_HISTORY.item_id = tb_item.id inner join TB_CAPTURE_LOG on TB_COUNTER_QUEUE_history.tb_counter_queue_id = TB_CAPTURE_LOG.QueueId and datediff(D,TB_CAPTURE_LOG.create_date,TB_COUNTER_QUEUE_HISTORY.service_date)=0 left join TB_Customer on TB_COUNTER_QUEUE_HISTORY.customer_id=TB_Customer.mobile_no "
    '        Sql &= " where status =3 " & tmpDate
    '        Sql &= tmpSql
    '        Dim da As New SqlDataAdapter(Sql, conn)
    '        Try
    '            dt = New DataTable
    '            da.Fill(dt)
    '            dtRet.Merge(dt)
    '        Catch ex As Exception
    '            'dt = New DataTable
    '        End Try
    '    Next

    '    Return dtRet
    'End Function

    'Function GetVDObyQID(ByVal ShopID As String, ByVal QID As String, ByVal Conn As SqlConnection) As DataTable
    '    Dim dt1 As New DataTable
    '    dt1 = GetShopList(Conn, ShopID)
    '    Dim dt As New DataTable

    '    If dt1.Rows.Count > 0 Then
    '        Dim sql As String = SelectDB(ShopID, Conn) & " select capture from TB_CAPTURE_LOG where QueueId='" & QID & "' "
    '        Dim da As New SqlDataAdapter(sql, Conn)

    '        Try
    '            da.Fill(dt)
    '        Catch ex As Exception

    '        End Try
    '    Else
    '        dt = New DataTable
    '    End If
    '    dt1.Dispose()

    '    Return dt
    'End Function

    Function GetQueueInfo(ByVal ShopID As String, ByVal Qid As String, ByVal fdate As String, ByVal Conn As SqlConnection) As DataTable
        Dim dt As New DataTable
        Dim sql As String = ""
        sql &= " select TB_USER.username,"
        sql &= " convert(varchar,service_date,103) as service_date,queue_no,"
        sql &= " isnull(title_name,'') + ' ' + isnull(fname,'') + ' ' + isnull(lname,'') as agent,"
        sql &= " tb_item.item_name, "
        sql &= " convert(varchar(8),start_time,114) as start,convert(varchar(8),end_time,114) as [end],customer_id from TB_COUNTER_QUEUE left join TB_USER on TB_COUNTER_QUEUE.user_id = TB_USER.id left join TB_TITLE on TB_USER.title_id = TB_TITLE.id left join tb_item on TB_COUNTER_QUEUE.item_id = tb_item.id"
        sql &= " where TB_COUNTER_QUEUE.id='" & Qid & "' and convert(varchar,service_date,12)='" & fdate & "' "

        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "WebQM_utile.GetQueueInfo")

        Dim dt1 As New DataTable
        Try
            dt1 = New DataTable
            sql = Strings.Replace(sql, "TB_COUNTER_QUEUE", "TB_COUNTER_QUEUE_HISTORY", , , CompareMethod.Text)
            sql = Strings.Replace(sql, "TB_COUNTER_QUEUE_HISTORY.id", "TB_COUNTER_QUEUE_HISTORY.tb_counter_queue_id", , , CompareMethod.Text)
            dt1 = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "WebQM_utile.GetQueueInfo")

            dt.Merge(dt1)

        Catch ex As Exception
            dt = New DataTable
        End Try

        Return dt
    End Function

    Function GetQueueInfo(ByVal ShopID As String, ByVal Qid As String, ByVal fdate As String) As DataTable
        'Dim dt1 As New DataTable
        'dt1 = GetShopList(Conn, ShopID)

        Dim dt As New DataTable
        'If dt1.Rows.Count > 0 Then
        Dim sql As String = ""
        sql &= " select TB_USER.username,"
        sql &= " service_date,queue_no,"
        sql &= " isnull(title_name,'') + ' ' + isnull(fname,'') + ' ' + isnull(lname,'') as agent,"
        sql &= " tb_item.item_name, "
        sql &= " convert(varchar(8),start_time,114) as start,convert(varchar(8),end_time,114) as [end],customer_id from TB_COUNTER_QUEUE left join TB_USER on TB_COUNTER_QUEUE.user_id = TB_USER.id left join TB_TITLE on TB_USER.title_id = TB_TITLE.id left join tb_item on TB_COUNTER_QUEUE.item_id = tb_item.id"
        sql &= " where TB_COUNTER_QUEUE.id='" & Qid & "' and convert(varchar,service_date,12)='" & fdate & "' "
        Try
            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "WebQM.utils.GetQueueInfo")

            Dim dt1 As New DataTable
            sql = Strings.Replace(sql, "TB_COUNTER_QUEUE", "TB_COUNTER_QUEUE_HISTORY", , , CompareMethod.Text)
            sql = Strings.Replace(sql, "TB_COUNTER_QUEUE_HISTORY.id", "TB_COUNTER_QUEUE_HISTORY.tb_counter_queue_id", , , CompareMethod.Text)
            dt1 = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "WebQM.utils.GetQueueInfo")

            If dt1.Rows.Count > 0 Then
                dt.Merge(dt1)
            End If
        Catch ex As Exception
            dt = New DataTable
        End Try

        Return dt
    End Function

    Function GetAgentList(ByVal ShopID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select isnull(fname,' ')+' '+isnull(lname,'') as username, [id] as userid from tb_user where active_status = 1 order by username"
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "WebQM_util_GetAgentList")
        Return dt
    End Function


    Function GetUserInfo(ByVal username As String, ByVal Conn As SqlConnection) As DataTable

        Dim dt As New DataTable

        Dim sql As String = " use " & Conn.Database & "; select * from tb_user where username='" & username & "' "

        Dim da As New SqlDataAdapter(sql, Conn)
        Try
            da.Fill(dt)
        Catch ex As Exception
            dt = New DataTable
        End Try
        Return dt
    End Function

    Public Function GetCounterList(ByVal ShopID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select id, counter_name from TB_COUNTER where active_status='1' order by counter_name "
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "QM_utils_GetCounterList")
        Return dt
    End Function

    Function GetServiceList(ByVal ShopID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select item_name,ISNULL(item_name,'')+(case when item_name_th IS  null then '' else ' ('+item_name_th+')' end) as item_name_enth, [id] as item_id from tb_item order by item_name"
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "QM_utils_GetServiceList")
        Return dt
    End Function

    Function GetSkillList(ByVal ShopID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select skill, [id] as skill_id from tb_skill order by skill"
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "QM_utils_GetSkillList")
        Return dt
    End Function

    Function GetUserGroupList(ByVal ShopID As String, ByVal Conn As SqlConnection) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select * from tb_groupuser order by group_name"
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "QM_utils_GetUserGroupList")
        'Dim da As New SqlDataAdapter(sql, Conn)
        'Try
        '    da.Fill(dt)
        'Catch ex As Exception
        '    dt = New DataTable
        'End Try
        Return dt
    End Function

    Function GetTitleList(ByVal ShopID As String, ByVal Conn As SqlConnection) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select * from tb_title order by [id]"
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "QM_utils_GetTitleList")
        'Dim da As New SqlDataAdapter(sql, Conn)
        'Try
        '    da.Fill(dt)
        'Catch ex As Exception
        '    dt = New DataTable
        'End Try
        Return dt
    End Function

    Function GetUserList(ByVal ShopID As String, ByVal Conn As SqlConnection) As DataTable
        Dim dt As New DataTable
        Dim sql As String = " select * from tb_user order by [id]"
        dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "QM_utils_GetUserList")
        'Dim da As New SqlDataAdapter(sql, Conn)
        'Try
        '    da.Fill(dt)
        'Catch ex As Exception
        '    dt = New DataTable
        'End Try
        Return dt
    End Function

    'Public Function CheckDuplicate(ByVal TableName As String, ByVal FieldName As String, ByVal Value_Duplicate As String, ByVal id As String, ByVal ShopID As String, ByVal conn As SqlConnection) As Boolean
    '    Try
    '        Dim sql As String = ""
    '        Dim dt As New DataTable
    '        Dim tmpSql As String = SelectDB(ShopID, conn)
    '        sql = tmpSql & "select * from " & FixDB(TableName) & " where " & FixDB(FieldName) & " = '" & FixDB(Value_Duplicate) & "' and id <> '" & id & "' and active_status = 1"
    '        dt = getDataTable(sql, conn)
    '        If dt.Rows.Count > 0 Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Return False
    '    End Try
    '    Return False
    'End Function

    Public Function GetDatatable(ByVal SQL As String) As DataTable
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetDatatable(SQL)

        Return dt
    End Function

    Public Function getDataTable(ByVal SQL As String, ByVal Conn As SqlConnection) As DataTable
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            da = New SqlDataAdapter(SQL, Conn)
            da.Fill(dt)
            'Conn.Close()
            Return dt

        Catch ex As Exception
            Return New DataTable
        End Try
        Return New DataTable
    End Function

    Public Function FixDB(ByVal TXT As String) As String 'Replace text จาก ' เป็น ''
        If IsDBNull(TXT) = True Then
            Return ""
        ElseIf TXT = Nothing Then
            Return ""
        Else
            Return Trim(TXT.ToString.Replace("'", "''"))
        End If
    End Function

    'Function SelectDB(ByVal ShopID As String, ByVal Conn As SqlConnection) As String
    '    Dim dt1 As New DataTable
    '    Dim tmp As String = "shop_" & Session("DBTYPE") & "_name"

    '    dt1 = GetShopList(Conn, ShopID)
    '    'Dim a = Session("DBTYPE")
    '    'If Conn.Database.EndsWith("_DR", StringComparison.OrdinalIgnoreCase) Then
    '    '    tmp = "shop_dr_name"
    '    'Else
    '    '    tmp = "shop_db_name"
    '    'End If
    '    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
    '        Return "use " & dt1.Rows(0)(tmp) & ";"
    '    Else
    '        Return ""
    '    End If
    'End Function

    'หา rowID ของ Table
    Public Function FindID(ByVal TableName As String, ByVal Conn As SqlConnection) As String
        Dim id As String = ""
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select isnull(MAX(id + 1),1) as id from " & FixDB(TableName)
            dt = getDataTable(sql, Conn)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0).Item("id").ToString
            End If
            Return id
        Catch ex As Exception
            'showFormError(ex.Message & Environment.NewLine & Environment.NewLine)
        End Try
        Return id
    End Function

    Public Sub executeSQL(ByVal SQL As String, ByVal conn As SqlConnection, Optional ByVal forceExit As Boolean = True)
        If SQL.Trim <> "" Then
            Dim cmd As New SqlCommand(SQL)
            Try
                'If CheckConn(getConnectionString, False) = True Then
                cmd.Connection = conn
                cmd.ExecuteNonQuery()
                'conn.Close()
                'If conn.DataSource <> MAIN_SERVER Then
                '    'Create Login File
                'End If
                'Else
                'showFormError("Database connection error ", False)
                'End If
            Catch ex As Exception
                'SaveQueryErrorLog(ex.Message, SQL)
            End Try
        End If
    End Sub

    Public Function GetPostbackControlName(ByVal p As Page) As String
        Dim controlName As String = p.Request.Params("__EVENTTARGET")
        Return IIf(controlName = Nothing, "", controlName)
    End Function

    Public Function GetShopFTP(ByVal shopid As String) As FtpInfo
        Dim ret As New FtpInfo
        Try
            Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(shopid)
            If shLnq.ID <> 0 Then
                With ret
                    .Host = shLnq.SHOP_QM_URL
                    .User = shLnq.QM_FTP_USERNAME
                    .Password = shLnq.QM_FTP_PASSWORD
                End With
            End If
        Catch ex As Exception

        End Try

        Return ret
    End Function
    Function GetFTPFileNameFromQueueID(ByVal shopid As String, ByVal qid As String, ByVal conn As SqlConnection) As String
        'Return qid & ".flv"
        Dim fn As String = ""
        Dim dt As New DataTable
        Dim d As Date
        Dim y As String
        dt = Engine.Common.FunctionEng.ExecuteShopSQL("select create_date from tb_capture_log where queue_id='" & qid & "' ", shopid, "WebQM_utils_GetFTPFileNameFromQueueID")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            d = dt.Rows(0)("create_date")
            If d.Year > 2500 Then
                y = d.Year - 543
            Else
                y = d.Year
            End If
            fn = Strings.Right(y, 2) & d.ToString("mm") & d.ToString("dd") & qid & ".flv"
        Else
            fn = "$error$"
        End If
        Return fn
    End Function


    Function CheckLDAPLogin(ByVal u As String, ByVal p As String) As CenParaDB.GateWay.LDAPResponsePara
        Dim LDAP As New CenParaDB.GateWay.LDAPResponsePara
        Try
            Dim eng As New Engine.GeteWay.GateWayServiceENG
            LDAP = eng.LDAPAuth(u, p)
            eng = Nothing
        Catch ex As Exception
            LDAP.RESULT = False
            LDAP.RESPONSE_MSG = ex.Message
        End Try
        Return LDAP
    End Function

    Public Function CurrentUserIsAdmin(ByRef obj As User) As Boolean
        Try
            Return obj.is_admin = "1"
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub DeleteVDOofUser(ByVal username As String, ByVal path As String)
        Dim fld As String = Server.MapPath(path & username)

        'If System.IO.Directory.Exists(Server.MapPath(path)) = False Then
        '    System.IO.Directory.CreateDirectory(Server.MapPath(path))
        'End If
        If System.IO.Directory.Exists(fld) = False Then
            System.IO.Directory.CreateDirectory(fld)
        End If

        For Each f As String In IO.Directory.GetFiles(fld)
            Try
                IO.File.Delete(f)
            Catch ex As Exception

            End Try
        Next


        'For Each f As String In IO.Directory.GetFiles(path)
        '    Try
        '        IO.File.Delete(f)
        '    Catch ex As Exception

        '    End Try
        'Next
    End Sub

    Public Function SaveConfigMultiShop(ByVal cfgPage As String, ByVal cfgGroupID As String, ByVal ShopList() As String, ByVal VarName As String, ByVal Value As String, ByVal ActiveDate As Date, ByVal Conn As SqlConnection, Optional ByVal OverrideOldConfig As Boolean = False) As FunctionReturn
        Dim ret As FunctionReturn
        ret.Result = True
        ret.Msg = ""
        Dim sql As String = ""
        Try
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If
            Dim usr As String = CType(Session("MyUser"), utils.User).username

            For i As Int16 = 0 To ShopList.Length - 1

                '****** Drop the recent config
                If OverrideOldConfig Then
                    executeSQL("use " & Conn.Database & "; update tb_shop_config set update_by='" & usr & "',update_date=getdate(),active='0' where config_page='" & cfgPage & "' and shop_id='" & ShopList(i) & "' and config_name='" & VarName & "'", Conn)
                End If

                '****** Save the new config
                sql = "use " & Conn.Database & ";declare @nwid int;select @nwid=isnull(max([id]),0)+1 from TB_SHOP_CONFIG;"
                sql &= " INSERT INTO [TB_SHOP_CONFIG] " & _
                    "            ([id] " & _
                    "            ,[create_by] " & _
                    "            ,[create_date] " & _
                    "            ,[config_page] " & _
                    "            ,[config_group_id] " & _
                    "            ,[config_name] " & _
                    "            ,[config_value] " & _
                    "            ,[shop_id] " & _
                    "            ,[event_date] " & _
                    "            ,[active]) " & _
                    "      VALUES " & _
                    "            (@nwid " & _
                    "            ,'" & usr & "' " & _
                    "            ,getdate() " & _
                    "            ,'" & cfgPage & "' " & _
                    "            ,'" & cfgGroupID & "' " & _
                    "            ,'" & VarName & "' " & _
                    "            ,'" & Value & "' " & _
                    "            ,'" & ShopList(i) & "' " & _
                    "            ,'" & ActiveDate.ToString("yyyyMMdd") & "' " & _
                    "            ,'1'); "
                executeSQL(sql, Conn)
            Next
        Catch ex As Exception
            ret.Result = False
            ret.Msg = ex.Message
        End Try
        Return ret
    End Function

    Public Function GetConfigTable(ByVal PageName As String, ByVal configFieldList As String, ByVal additionalFilter As String, ByVal Conn As SqlConnection) As DataTable
        Dim dt As New DataTable

        If Conn.State <> ConnectionState.Open Then
            Conn.Open()
        End If

        Dim da As New SqlDataAdapter("use " & Conn.Database & "; select ISNULL(shop_name_en,'')+(case when shop_name_th IS  null then '' else ' ('+shop_name_th+')' end) as shop_name,config_name,config_value,event_date from tb_shop_config a inner join tb_shop b on a.shop_id=b.id where config_page='" & PageName & "' and config_name  in(" & configFieldList & ") and a.active='1' " & additionalFilter & " Order by shop_name_en,config_group_id", Conn)
        da.Fill(dt)

        Return dt
    End Function

    Public Function GetNewCfgGroupID() As String
        Return Guid.NewGuid.ToString
    End Function

    '#### ตั้ม เพิ่ม function #####
    Public Function GetShopDetail(ByVal Shop_id As String, ByVal conn As SqlConnection) As DataTable
        Dim Sql As String = "Select * "
        Sql += " from TB_SHOP where active = 'Y' "
        Sql += " and id=" & Shop_id

        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(Sql, conn)
        Try
            da.Fill(dt)
        Catch ex As Exception
            dt = New DataTable
        End Try
        Return dt
    End Function
    '########################
End Class
'End Namespace

