Namespace CSI
    Public Class VerifyDataENG
        Public Function GetVerifyDataList(ByVal vFilterID As Long, ByVal ShopID As Long, ByVal ServDate As String) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
            fLnq.GetDataByPK(vFilterID, trans.Trans)

            Dim DayQty As Integer = 0
            Dim HourQty As Integer = CInt(Split(fLnq.PREIOD_TIMETO, ":")(0)) - CInt(Split(fLnq.PREIOD_TIMEFROM, ":")(0))

            If fLnq.SCHEDULETYPEDAY = "0" Then
                DayQty = DateDiff(DateInterval.Day, fLnq.PREIOD_DATEFROM.Value, fLnq.PREIOD_DATETO.Value) + 1
            ElseIf fLnq.SCHEDULETYPEDAY = "1" Then
                Dim CurrDate As DateTime = fLnq.PREIOD_DATEFROM.Value
                Do
                    If fLnq.SCHEDULESUNDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Sunday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEMONDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Monday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULETUEDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Tuesday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEWEDDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Wednesday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULETHUDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Thursday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEFRIDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Friday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULESATDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Saturday Then
                        DayQty += 1
                    End If

                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                Loop While CurrDate <= fLnq.PREIOD_DATETO.Value
            End If
            '## ตั้มเพิ่ม Colums regis,no target โดยกรองจาก Shop,Service,วันที่
            Dim sql As String = " Select Table1.* "
            sql += " ,(select sum(regis) from TB_REP_WT_HT_SHOP_DAY T where T.shop_id=" & ShopID & " and T.service_id=Table1.item_id"
            ' If ServDate.Trim <> "" Then
            sql += " and convert(varchar(8), t.service_date,112) = '" & ServDate & "'"
            ' End If
            sql += " ) as regis "
            sql += " ,(select sum(target) from TB_FILTER_TEMP_TARGET T where T.tb_shop_id=" & ShopID & " and T.tb_item_id=Table1.item_id"
            'If ServDate.Trim <> "" Then
            sql += " and convert(varchar(8), t.service_date,112) = '" & ServDate & "'"
            'End If
            sql += " ) as notarget "
            sql += " From ("
            sql += " select fd.tb_filter_id, s.id shop_id, s.shop_name_en, ti.id item_id, ti.item_name, "
            sql += " f.[target], fs.target_percent, ((f.[target]*fs.target_percent)/100)/CONVERT(float,(" & (DayQty * HourQty) & ")) no_target,"
            sql += " count(fd.mobile_no) data_for_send, sum(case when fd.send_time is not null then 1 else 0 end) send_complete, "
            sql += " sum(case when fd.result_status= 2  then 1 else 0 end) return_result, f.target_unlimited"
            sql += " from TB_FILTER f "
            sql += " inner join TB_FILTER_DATA fd on f.id=fd.tb_filter_id"
            sql += " inner join TB_FILTER_SERVICE fs on f.id= fs.tb_filter_id and fd.tb_item_id=fs.tb_item_id "
            sql += " inner join TB_ITEM ti on ti.id=fs.tb_item_id"
            sql += " inner join TB_SHOP s on s.id=fd.shop_id"
            sql += " where f.id=" & vFilterID & " and s.id= " & ShopID
            ''sql += " where 1=1"
            If ServDate.Trim <> "" Then
                sql += " and convert(varchar(8), fd.service_date,112) = '" & ServDate & "'"
            End If
            sql += " group by fd.tb_filter_id, s.id, s.shop_name_en, ti.id , "
            sql += " ti.item_name,  f.[target], fs.target_percent, f.[target],fs.target_percent,f.target_unlimited "
            sql += " ) as Table1"
            sql += " order by Table1.shop_name_en, Table1.item_name"

            'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim dt As New DataTable
            Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetNoResultData(ByVal ServiceDate As String, ByVal ShopID As Long, ByVal FilterID As Long, ByVal ServiceID As Long) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim sql As String = " select fd.id, fd.mobile_no, fd.customer_name, fd.username, f.filter_name, ti.item_code,"
            sql += " fd.template_code, fd.send_time, fd.result_time, fd.result"
            sql += " from tb_filter_data fd"
            sql += " inner join TB_SHOP s on s.id=fd.shop_id"
            sql += " inner join TB_FILTER f on f.id=fd.tb_filter_id"
            sql += " inner join TB_ITEM ti on ti.id=fd.tb_item_id"
            sql += " where convert(varchar(8),service_date,112) ='" & ServiceDate & "' "
            sql += " and fd.shop_id = " & ShopID
            sql += " and fd.tb_filter_id = " & FilterID
            sql += " and fd.tb_item_id =" & ServiceID
            sql += " and fd.result_status= 2"
            sql += " order by fd.mobile_no"
            Dim dt As DataTable = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function


#Region "_TW"
        Public Function GetTWVerifyDataList(ByVal vFilterID As Long, ByVal ServDate As String) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim fLnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
            fLnq.GetDataByPK(vFilterID, trans.Trans)

            Dim DayQty As Integer = 0
            Dim HourQty As Integer = CInt(Split(fLnq.PREIOD_TIMETO, ":")(0)) - CInt(Split(fLnq.PREIOD_TIMEFROM, ":")(0))

            If fLnq.SCHEDULETYPEDAY = "0" Then
                DayQty = DateDiff(DateInterval.Day, fLnq.PREIOD_DATEFROM.Value, fLnq.PREIOD_DATETO.Value) + 1
            ElseIf fLnq.SCHEDULETYPEDAY = "1" Then
                Dim CurrDate As DateTime = fLnq.PREIOD_DATEFROM.Value
                Do
                    If fLnq.SCHEDULESUNDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Sunday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEMONDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Monday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULETUEDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Tuesday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEWEDDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Wednesday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULETHUDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Thursday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEFRIDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Friday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULESATDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Saturday Then
                        DayQty += 1
                    End If

                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                Loop While CurrDate <= fLnq.PREIOD_DATETO.Value
            End If
            '## ตั้มเพิ่ม Colums regis,no target โดยกรองจาก Shop,Service,วันที่
            Dim sql As String = " Select Table1.*, " & vbNewLine
            sql += " (select COUNT(*) from TW_SOURCE_DATA SD Where SD.location_code=table1.location_code and SD.order_type=TABLE1.order_type_name" & vbNewLine
            sql += " and convert(varchar(8), SD.complete_date,112) ='" & ServDate & "'" & vbNewLine
            sql += " ) as regis " & vbNewLine
            sql += " ,(select SUM(target) from TW_FILTER_TEMP_TARGET T "
            sql += "   inner join TW_SFF_ORDER_TYPE st on st.id=t.tw_sff_order_type_id "
            sql += "   where T.tw_location_id=table1.location_id and st.order_type_name=TABLE1.order_type_name" & vbNewLine
            sql += "   and convert(varchar(8), T.service_date,112) = '" & ServDate & "'" & vbNewLine
            sql += " ) as notarget " & vbNewLine
            sql += " From (" & vbNewLine
            sql += " select fd.tw_filter_id, L.id location_id, L.location_name_th, ti.id order_type_id, ti.order_type_name, " & vbNewLine
            sql += " f.[target], fs.target_percent, ((f.[target]*fs.target_percent)/100)/CONVERT(float,(" & (DayQty * HourQty) & ")) no_target," & vbNewLine
            sql += " count(fd.mobile_no) data_for_send, sum(case when fd.send_time is not null then 1 else 0 end) send_complete, " & vbNewLine
            sql += " sum(case when fd.result_status= 2  then 1 else 0 end) return_result, f.target_unlimited,l.location_code" & vbNewLine
            sql += " from TW_FILTER f " & vbNewLine
            sql += " inner join TW_FILTER_DATA fd on f.id=fd.tw_filter_id" & vbNewLine
            sql += " inner join TW_FILTER_ORDER_TYPE fs on f.id= fs.tw_filter_id and fd.tw_sff_order_type_id=fs.tw_sff_order_type_id" & vbNewLine
            sql += " inner join TW_SFF_ORDER_TYPE ti on ti.id=fs.tw_sff_order_type_id" & vbNewLine
            sql += " inner join TW_LOCATION L on L.id=fd.tw_location_id" & vbNewLine
            sql += " where f.id=" & vFilterID & " " & vbNewLine
            If ServDate.Trim <> "" Then
                sql += " and convert(varchar(8), fd.service_date,112) = '" & ServDate & "'" & vbNewLine
            End If
            sql += " group by fd.tw_filter_id, L.id, L.location_name_th, ti.id , " & vbNewLine
            sql += " ti.order_type_name,  f.[target], fs.target_percent, f.[target],fs.target_percent,f.target_unlimited,l.location_code " & vbNewLine
            sql += " ) as Table1" & vbNewLine
            sql += " order by Table1.location_name_th, Table1.order_type_name" & vbNewLine

            Dim dt As New DataTable
            Dim lnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function

        Public Function GetTWNoResultData(ByVal CompleteDate As String, ByVal LocationID As Long, ByVal FilterID As Long, ByVal ServiceID As Long) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
            Dim sql As String = " select fd.id, fd.mobile_no, fd.customer_name, fd.username, f.filter_name, st.order_type_name,"
            sql += " fd.template_code, fd.send_time, fd.result_time, fd.result"
            sql += " from tw_filter_data fd"
            sql += " inner join TW_LOCATION l on l.id=fd.tw_location_id"
            sql += " inner join TW_FILTER f on f.id=fd.tw_filter_id"
            sql += " inner join TW_SFF_ORDER_TYPE st on st.id=fd.tw_sff_order_type_id"
            sql += " where convert(varchar(8),service_date,112) ='" & CompleteDate & "' "
            sql += " and fd.tw_location_id = " & LocationID
            sql += " and fd.tw_filter_id = " & FilterID
            sql += " and fd.tw_sff_order_type_id =" & ServiceID
            sql += " and fd.result_status= 2"
            sql += " order by fd.mobile_no"
            Dim dt As DataTable = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return dt
        End Function
#End Region '_TW

    End Class
End Namespace

