Namespace CSI
    Public Class CSIReportsENG
        Public Function GetDetailDataList(ByVal whText As String) As DataTable
            Dim sql As String = ""
            sql += " select fd.tb_filter_id, f.filter_name, s.id shop_id,s.shop_code, s.shop_name_en, ti.id item_id, ti.item_name, "
            sql += " fd.end_time, fd.filter_time, fd.send_time, fd.atsr_call_time, fd.mobile_no, fd.result,  fd.customer_name, "
            sql += " fd.username, fd.nps_score, fd.network_type,isnull(fd.staff_name,'') staff_name"
            sql += " from  TB_FILTER_DATA fd"
            sql += " inner join TB_FILTER_SERVICE fs on fd.tb_filter_id= fs.tb_filter_id and fd.tb_item_id=fs.tb_item_id "
            sql += " inner join TB_FILTER f on f.id=fd.tb_filter_id"
            sql += " inner join TB_ITEM ti on ti.id=fs.tb_item_id"
            sql += " inner join TB_SHOP s on s.id=fd.shop_id "
            sql += " where " & whText
            sql += " order by s.shop_name_en, ti.item_name, fd.send_time"

            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetCSIDataList(ByVal WhText As String) As DataTable
            Dim sql As String = ""
            sql += " select datename(MONTH, fd.send_time) + ' ' + LTRIM(RTRIM(str(year(fd.send_time)))) month_name," & vbNewLine
            sql += " s.shop_code, s.shop_name_en, ti.id item_id, ti.item_name," & vbNewLine
            sql += " sum(case fd.result when 'ดีมาก' then 1 else 0 end) result_very_good," & vbNewLine
            sql += " sum(case fd.result when 'ดี' then 1 else 0 end) result_good," & vbNewLine
            sql += " sum(case fd.result when 'ควรปรับปรุง' then 1 else 0 end) result_adjust" & vbNewLine
            sql += " from  TB_FILTER_DATA fd" & vbNewLine
            sql += " inner join TB_FILTER_SERVICE fs on fd.tb_filter_id= fs.tb_filter_id and fd.tb_item_id=fs.tb_item_id " & vbNewLine
            sql += " inner join TB_ITEM ti on ti.id=fs.tb_item_id" & vbNewLine
            sql += " inner join TB_SHOP s on s.id=fd.shop_id" & vbNewLine
            sql += " where " & WhText & vbNewLine
            sql += " group by s.shop_code, s.shop_name_en, ti.id, ti.item_name," & vbNewLine
            sql += " datename(MONTH, fd.send_time),year(fd.send_time)" & vbNewLine
            sql += " order by s.shop_name_en, ti.item_name" & vbNewLine

            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetCSIByAgentDataList(ByVal WhText As String) As DataTable
            Dim sql As String = ""
            sql += " select datename(MONTH, fd.send_time) + ' ' + LTRIM(RTRIM(str(year(fd.send_time)))) month_name," & vbNewLine
            sql += " s.shop_code, s.shop_name_en, ti.id item_id, ti.item_name,username,staff_name," & vbNewLine
            sql += " sum(case fd.result when 'ดีมาก' then 1 else 0 end) result_very_good," & vbNewLine
            sql += " sum(case fd.result when 'ดี' then 1 else 0 end) result_good," & vbNewLine
            sql += " sum(case fd.result when 'ควรปรับปรุง' then 1 else 0 end) result_adjust," & vbNewLine
            sql += " fd.segment " & vbNewLine
            sql += " from  TB_FILTER_DATA fd" & vbNewLine
            sql += " inner join TB_FILTER_SERVICE fs on fd.tb_filter_id= fs.tb_filter_id and fd.tb_item_id=fs.tb_item_id " & vbNewLine
            sql += " inner join TB_ITEM ti on ti.id=fs.tb_item_id" & vbNewLine
            sql += " inner join TB_SHOP s on s.id=fd.shop_id" & vbNewLine
            'sql += " where fd.shop_id=1"
            sql += " where " & WhText & vbNewLine
            sql += " group by s.shop_code, s.shop_name_en, ti.id, ti.item_name,username,staff_name," & vbNewLine
            sql += " datename(MONTH, fd.send_time),year(fd.send_time), fd.segment" & vbNewLine
            sql += " order by s.shop_name_en, ti.item_name" & vbNewLine

            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetUserCSIByAgentList(ByVal WhText As String) As DataTable

            Dim dt As New DataTable
            dt = GetCSIByAgentDataList(WhText)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "username,staff_name,segment"
                dt = dt.DefaultView.ToTable(True, "username", "staff_name", "segment")
            End If

            Return dt
        End Function

        Public Function GetNPSSCOREDataList(ByVal whText As String, ByVal shopID As Long) As DataTable
            Dim sql As String = ""
            sql += " select isnull(a.shop_id,b.shop_id) shop_id,isnull(a.region_id,b.region_id) region_id," & vbNewLine
            sql += " isnull(a.shop_code,b.shop_code) shop_code,isnull(a.shop_name_en,b.shop_name_en) shop_name_en, " & vbNewLine
            sql += " isnull(a.region_code,b.region_code) region_code,isnull(a.region_name_en,b.region_name_en) region_name_en," & vbNewLine
            sql += " isnull(a.score_0,0) score_0,isnull(a.score_1,0) score_1 ,isnull(a.score_2,0) score_2," & vbNewLine
            sql += " isnull(a.score_3,0) score_3,isnull(a.score_4,0) score_4,isnull(a.score_5,0) score_5," & vbNewLine
            sql += " isnull(a.score_6,0) score_6,isnull(a.score_7,0) score_7,isnull(a.score_8,0) score_8," & vbNewLine
            sql += " isnull(a.score_9,0) score_9,isnull(a.score_10,0) score_10,isnull(a.GrandTo,0) GrandTo" & vbNewLine
            sql += " from " & vbNewLine
            sql += " (  select s.id as shop_id,r.id as region_id, s.shop_code,s.shop_name_en,r.region_code,r.region_name_en" & vbNewLine
            sql += "    from tb_shop s" & vbNewLine
            sql += "    inner join TB_REGION r on r.id=s.region_id" & vbNewLine
            sql += "    where s.active='Y'" & vbNewLine
            sql += " ) b " & vbNewLine
            sql += " full outer join " & vbNewLine
            sql += " (" & vbNewLine
            sql += "    select count(fd.id) GrandTo,s.id as shop_id, s.id ,r.id as region_id," & vbNewLine
            sql += "    s.shop_code,s.shop_name_en,  r.region_code,r.region_name_en ," & vbNewLine
            sql += "    SUM(case fd.nps_score when 0 then 1 else 0 end) score_0," & vbNewLine
            sql += "    SUM(case fd.nps_score when 1 then 1 else 0 end) score_1," & vbNewLine
            sql += "    SUM(case fd.nps_score when 2 then 1 else 0 end) score_2," & vbNewLine
            sql += "    SUM(case fd.nps_score when 3 then 1 else 0 end) score_3," & vbNewLine
            sql += "    SUM(case fd.nps_score when 4 then 1 else 0 end) score_4," & vbNewLine
            sql += "    SUM(case fd.nps_score when 5 then 1 else 0 end) score_5," & vbNewLine
            sql += "    SUM(case fd.nps_score when 6 then 1 else 0 end) score_6," & vbNewLine
            sql += "    SUM(case fd.nps_score when 7 then 1 else 0 end) score_7," & vbNewLine
            sql += "    SUM(case fd.nps_score when 8 then 1 else 0 end) score_8," & vbNewLine
            sql += "    SUM(case fd.nps_score when 9 then 1 else 0 end) score_9," & vbNewLine
            sql += "    SUM(case fd.nps_score when 10 then 1 else 0 end) score_10" & vbNewLine
            sql += "    from TB_FILTER_DATA fd  " & vbNewLine
            sql += "    inner join TB_SHOP S on fd.shop_id=S.id  " & vbNewLine
            sql += "    inner join TB_REGION R on s.region_id = r.id" & vbNewLine
            sql += "    where result_status =2 and nps_score <> -1" & vbNewLine
            sql += "    and " & whText
            sql += "    group by s.id  ,r.id, s.shop_code, s.shop_name_en, r.region_code, r.region_name_en " & vbNewLine
            sql += "    ) a on a.shop_id=b.shop_id"
            'sql += " order by r.region_code,s.shop_code, s.shop_name_en"
            If shopID > 0 Then
                sql += " where isnull(a.shop_id,b.shop_id)='" & shopID & "'"
            End If

            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetNPSSCOREByAgentDataList(ByVal whText As String) As DataTable
            Dim sql As String = ""
            sql += " select count(fd.id) GrandTo,shop_id ,r.id as region_id," & _
             " s.shop_code,s.shop_name_en,  r.region_code,r.region_name_en ,fd.username,fd.staff_name," & _
             " SUM(case fd.nps_score when 0 then 1 else 0 end) score_0," & _
             " SUM(case fd.nps_score when 1 then 1 else 0 end) score_1," & _
             " SUM(case fd.nps_score when 2 then 1 else 0 end) score_2," & _
             " SUM(case fd.nps_score when 3 then 1 else 0 end) score_3," & _
             " SUM(case fd.nps_score when 4 then 1 else 0 end) score_4," & _
             " SUM(case fd.nps_score when 5 then 1 else 0 end) score_5," & _
             " SUM(case fd.nps_score when 6 then 1 else 0 end) score_6," & _
             " SUM(case fd.nps_score when 7 then 1 else 0 end) score_7," & _
             " SUM(case fd.nps_score when 8 then 1 else 0 end) score_8," & _
             " SUM(case fd.nps_score when 9 then 1 else 0 end) score_9," & _
             " SUM(case fd.nps_score when 10 then 1 else 0 end) score_10" & _
             " from TB_FILTER_DATA fd  " & _
             " left join TB_SHOP S on fd.shop_id=S.id  " & _
             " left join TB_REGION R on s.region_id = r.id" & _
             " where result_status =2 and nps_score <> -1"
            sql += " and " & whText
            sql += " group by shop_id ,r.id, s.shop_code, s.shop_name_en, r.region_code, r.region_name_en, fd.username ,fd.staff_name"



            '** แสดงทุก Shop ที่ Active

            ' sql += " select isnull(GrandTo,'') GrandTo,isnull(shop.shop_id,'') shop_id,isnull(shop.region_id,'') region_id," & _
            '" isnull(shop.shop_code,'') shop_code ,isnull(shop.shop_name_en,'') shop_name_en,isnull(shop.region_code,'') region_code," & _
            '" isnull(shop.region_name_en,'') region_name_en,isnull(username,'') username,isnull(staff_name,'') staff_name," & _
            '" isnull(score_0,'0') score_0,isnull(score_1,'0') score_1,isnull(score_2,'0') score_2,isnull(score_3,'0') score_3," & _
            '" isnull(score_4,'0') score_4,isnull(score_5,'0') score_5,isnull(score_6,'0') score_6,isnull(score_7,'0') score_7," & _
            '" isnull(score_8,'0') score_8,isnull(score_9,'0') score_9,isnull(score_10,'0') score_10 from (" & _
            '" (select s.id as shop_id,shop_code,shop_name_en,region_code,r.id as region_id,region_name_en from TB_SHOP s " & _
            '" left join TB_REGION R on s.region_id = r.id  where s.active='Y') SHOP" & _
            '" Left Join (" & _
            '" select count(fd.id) GrandTo,shop_id ,r.id as region_id," & _
            ' " s.shop_code,s.shop_name_en,  r.region_code,r.region_name_en ,fd.username,fd.staff_name," & _
            ' " SUM(case fd.nps_score when 0 then 1 else 0 end) score_0," & _
            ' " SUM(case fd.nps_score when 1 then 1 else 0 end) score_1," & _
            ' " SUM(case fd.nps_score when 2 then 1 else 0 end) score_2," & _
            ' " SUM(case fd.nps_score when 3 then 1 else 0 end) score_3," & _
            ' " SUM(case fd.nps_score when 4 then 1 else 0 end) score_4," & _
            ' " SUM(case fd.nps_score when 5 then 1 else 0 end) score_5," & _
            ' " SUM(case fd.nps_score when 6 then 1 else 0 end) score_6," & _
            ' " SUM(case fd.nps_score when 7 then 1 else 0 end) score_7," & _
            ' " SUM(case fd.nps_score when 8 then 1 else 0 end) score_8," & _
            ' " SUM(case fd.nps_score when 9 then 1 else 0 end) score_9," & _
            ' " SUM(case fd.nps_score when 10 then 1 else 0 end) score_10" & _
            ' " from TB_FILTER_DATA fd  " & _
            ' " left join TB_SHOP S on fd.shop_id=S.id  " & _
            ' " left join TB_REGION R on s.region_id = r.id" & _
            ' " where result_status =2 and nps_score <> -1"
            ' sql += " and " & whText
            ' sql += " group by shop_id ,r.id, s.shop_code, s.shop_name_en, r.region_code, r.region_name_en, fd.username ,fd.staff_name"
            ' sql += " ) NPS on SHOP.shop_id=NPS.shop_id)"

            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetTWDetailDataList(ByVal whText As String) As DataTable
            Dim sql As String = ""
            sql += " select fd.tw_filter_id,F.filter_name,Fd.tw_location_id,L.location_code,l.region_code,l.province_code,"
            sql += " L.location_name_th ,fd.order_type_name,fd.end_time,fd.filter_time,"
            sql += " fd.send_time, fd.atsr_call_time, fd.mobile_no, fd.result, fd.customer_name, fd.network_type,fd.nps_score"
            sql += " from  TW_FILTER_DATA fd"
            sql += " inner join TW_FILTER F on F.id=fd.tw_filter_id"
            sql += " inner join TW_LOCATION L on L.id=fd.tw_location_id"
            sql += " where " & whText
            sql += " order by l.region_code,l.province_code,L.location_name_th, fd.order_type_name, fd.send_time"

            Dim lnq As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetTWCSIDataList(ByVal WhText As String) As DataTable
            Dim sql As String = ""
            sql += " select L.location_code,L.location_name_th, L.province_code,L.region_code,L.location_segment,location_type," & vbNewLine
            sql += " sum(case fd.result when 'ดีมาก' then 1 else 0 end) result_very_good," & vbNewLine
            sql += " sum(case fd.result when 'ดี' then 1 else 0 end) result_good," & vbNewLine
            sql += " sum(case fd.result when 'ควรปรับปรุง' then 1 else 0 end) result_adjust" & vbNewLine
            sql += " from  TW_FILTER_DATA fd" & vbNewLine
            sql += " inner join TW_FILTER F on F.id=fd.tw_filter_id" & vbNewLine
            sql += " inner join TW_LOCATION L on L.id=fd.tw_location_id" & vbNewLine
            sql += " where  " & WhText & vbNewLine
            'sql += " and fd.result_status='2'"
            sql += " group by L.location_code, L.location_name_th, L.province_code,L.region_code,L.location_segment,location_type" & vbNewLine
            sql += " order by L.region_code,L.province_code, L.location_name_th"

            Dim lnq As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function

        Public Function GetTWNPSSCOREDataList(ByVal whText As String) As DataTable
            Dim sql As String = ""
            sql += " select count(fd.id) GrandTo,fd.tw_location_id shop_id ,"
            sql += " L.location_code shop_code,L.location_name_th shop_name_en, region_code ,L.province_code,L.location_type,L.location_segment,"
            sql += " SUM(case fd.nps_score when 0 then 1 else 0 end) score_0,"
            sql += " SUM(case fd.nps_score when 1 then 1 else 0 end) score_1,"
            sql += " SUM(case fd.nps_score when 2 then 1 else 0 end) score_2,"
            sql += " SUM(case fd.nps_score when 3 then 1 else 0 end) score_3,"
            sql += " SUM(case fd.nps_score when 4 then 1 else 0 end) score_4,"
            sql += " SUM(case fd.nps_score when 5 then 1 else 0 end) score_5,"
            sql += " SUM(case fd.nps_score when 6 then 1 else 0 end) score_6,"
            sql += " SUM(case fd.nps_score when 7 then 1 else 0 end) score_7,"
            sql += " SUM(case fd.nps_score when 8 then 1 else 0 end) score_8,"
            sql += " SUM(case fd.nps_score when 9 then 1 else 0 end) score_9,"
            sql += " SUM(case fd.nps_score when 10 then 1 else 0 end) score_10"
            sql += " from TW_FILTER_DATA fd  "
            sql += " left join TW_LOCATION L on fd.tw_location_id=L.ID  "
            sql += " where result_status = 2 And nps_score <> -1 "
            sql += " and " & whText
            sql += " group by fd.tw_location_id ,L.location_code,L.location_name_th,region_code ,L.province_code,L.location_type,L.location_segment"
            sql += " order by region_code,L.location_code, L.location_name_th "

            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()

            Return dt
        End Function
    End Class
End Namespace

