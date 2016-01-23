Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities

Namespace Reports
    Public Class RepAverageServiceTimeComparingWithKPI_byStaff
        Public Function GetReport(ByVal InputPara As AverageServiceTimeWithKPI_byStaffPara) As DataTable
            Dim ret As New DataTable
            'Create DataTable
            ret.Columns.Add("Shop Name")
            ret.Columns.Add("Staff")
            ret.Columns.Add("Date")
            ret.Columns.Add("Service Type")
            ret.Columns.Add("Time")

            ret.Columns.Add("Registation")
            ret.Columns.Add("Serve")
            ret.Columns.Add("Missed Call")
            ret.Columns.Add("Average Waiting Time")
            ret.Columns.Add("Average Handling Time")
            ret.Columns.Add("Max HT (Min)")
            ret.Columns.Add("Min HT (Min)")
            ret.Columns.Add("Meet KPI")
            ret.Columns.Add("KPI (%)")

            Dim cenTrans As New CenLinqDB.Common.Utilities.TransactionDB
            If cenTrans.Trans IsNot Nothing Then
                'วนลูปตาม Shop ก่อน
                Dim ShopID() As String = Split(InputPara.ShopID, ",")
                For Each shID As String In ShopID
                    If shID.Trim <> "" Then
                        Dim ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(Convert.ToInt64(shID))
                        If ShopLnq.ID <> 0 Then
                            Dim Interval As Int32 = InputPara.IntervalMinute
                            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                            shTrans = FunctionEng.GetShTransction(shTrans, cenTrans, ShopLnq)
                            'หา Service ของ Shop
                            Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                            Dim sDt As DataTable = sLnq.GetDataList("active_status='1'", "item_order", shTrans.Trans)
                            Dim CurrentDate As Date = InputPara.DateFrom
                            Dim sql As String = ""
                            Dim dt_user As New DataTable
                            sql = "select TB_USER.id,title_name + ' ' + fname + ' ' + lname as Staff from TB_USER left join TB_TITLE on TB_USER.title_id = TB_TITLE.id where active_status = 1 and TB_USER.id in (select user_id from TB_COUNTER_QUEUE_HISTORY where convert(int,convert(varchar(8),service_date,112)) = " & FixDate(CurrentDate) & " group by user_id)"
                            dt_user = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
                            If dt_user.Rows.Count > 0 Then
                                ' Loop Agent
                                For x As Int32 = 0 To dt_user.Rows.Count - 1
                                    CurrentDate = InputPara.DateFrom
                                    Do ' Loop Date
                                        Dim ST As DateTime = InputPara.TimePeroidFrom
                                        Dim ET As DateTime = DateAdd(DateInterval.Minute, Interval, ST)

                                        For i As Int32 = 0 To sDt.Rows.Count - 1
                                            Dim dt As New DataTable
                                            Dim CurrTimeEnd As DateTime = InputPara.TimePeroidTo
                                            Do ' Loop Time Interval
                                                sql = "declare @Register as int; select @Register = (select COUNT(queue_no) from TB_COUNTER_QUEUE_HISTORY where convert(int,convert(varchar(8),service_date,112)) = " & FixDate(CurrentDate) & " and item_id = " & sDt.Rows(i).Item("id").ToString & " and convert(varchar(5),service_date,114) >= '" & ST.Hour.ToString.PadLeft(2, "0") & ":" & ST.Minute.ToString.PadLeft(2, "0") & "' and convert(varchar(5),service_date,114) < '" & ET.Hour.ToString.PadLeft(2, "0") & ":" & ET.Minute.ToString.PadLeft(2, "0") & "')" & vbNewLine
                                                sql &= "declare @Misscall as int; select @Misscall = (select COUNT(queue_no) from TB_COUNTER_QUEUE_HISTORY where convert(int,convert(varchar(8),service_date,112)) = " & FixDate(CurrentDate) & "  and user_id = " & dt_user.Rows(x).Item("id").ToString & " and status = 8 and item_id = " & sDt.Rows(i).Item("id").ToString & " and convert(varchar(5),service_date,114) >= '" & ST.Hour.ToString.PadLeft(2, "0") & ":" & ST.Minute.ToString.PadLeft(2, "0") & "' and convert(varchar(5),service_date,114) < '" & ET.Hour.ToString.PadLeft(2, "0") & ":" & ET.Minute.ToString.PadLeft(2, "0") & "')" & vbNewLine
                                                sql &= "select @Register as regis,COUNT(queue_no) as serve,@Misscall as miss_call,SUM(HT_WithInKPI) as serve_within_kpi " & vbNewLine
                                                sql &= ",convert(varchar(5),(SUM(HT_WithInKPI) * 100) / COUNT(queue_no)) + '%' as ht_within_kpi_per" & vbNewLine

                                                sql &= ",convert(varchar(10),MAX(HTime)/60) + ':' + right('0' + convert(varchar(10),MAX(HTime)%60),2) as max_ht" & vbNewLine
                                                sql &= ",convert(varchar(10),MIN(HTime)/60) + ':' + right('0' + convert(varchar(10),MIN(HTime)%60),2) as min_ht" & vbNewLine
                                                sql &= ",convert(varchar(10),AVG(WTime)/60) + ':' + right('0' + convert(varchar(10),AVG(WTime)%60),2) as avg_wt" & vbNewLine
                                                sql &= ",convert(varchar(10),AVG(HTime)/60) + ':' + right('0' + convert(varchar(10),AVG(HTime)%60),2) as avg_ht" & vbNewLine
                                                sql &= "from (" & vbNewLine
                                                sql &= "select" & vbNewLine
                                                sql &= "item_id,item_wait * 60 as item_wait,item_time * 60 as item_time,TB_ITEM.item_name,queue_no " & vbNewLine
                                                sql &= ",DATEDIFF(SECOND,assign_time,start_time) as WTime,DATEDIFF(SECOND,start_time,end_time) as HTime" & vbNewLine
                                                sql &= ",case when (item_wait * 60) > DATEDIFF(SECOND,start_time ,end_time) then 1 else 0 end as HT_WithInKPI" & vbNewLine

                                                sql &= "from TB_COUNTER_QUEUE_HISTORY left join TB_ITEM on TB_COUNTER_QUEUE_HISTORY.item_id = TB_ITEM.id " & vbNewLine
                                                sql &= "where convert(int,convert(varchar(8),service_date,112)) = " & FixDate(CurrentDate) & " and status = 3 and item_id = " & sDt.Rows(i).Item("id").ToString & " and convert(varchar(5),service_date,114) >= '" & ST.Hour.ToString.PadLeft(2, "0") & ":" & ST.Minute.ToString.PadLeft(2, "0") & "' and convert(varchar(5),service_date,114) < '" & ET.Hour.ToString.PadLeft(2, "0") & ":" & ET.Minute.ToString.PadLeft(2, "0") & "' and user_id = " & dt_user.Rows(x).Item("id").ToString & vbNewLine
                                                sql &= " ) as TB group by item_id " & vbNewLine


                                                dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
                                                If dt.Rows.Count > 0 Then
                                                    Dim dr As DataRow
                                                    dr = ret.NewRow
                                                    dr("Shop Name") = ShopLnq.SHOP_NAME_EN
                                                    dr("Staff") = dt_user.Rows(x).Item("Staff").ToString
                                                    dr("Date") = CDate(CurrentDate).ToString("dd/MM/yyyy", New System.Globalization.CultureInfo("en-US"))
                                                    dr("Service Type") = sDt.Rows(i).Item("item_name").ToString
                                                    dr("Time") = ST.Hour.ToString.PadLeft(2, "0") & ":" & ST.Minute.ToString.PadLeft(2, "0") & " - " & ET.Hour.ToString.PadLeft(2, "0") & ":" & ET.Minute.ToString.PadLeft(2, "0")
                                                    dr("Registation") = dt.Rows(0).Item("regis").ToString
                                                    dr("Serve") = dt.Rows(0).Item("serve").ToString
                                                    dr("Missed Call") = dt.Rows(0).Item("miss_call").ToString
                                                    dr("Average Waiting Time") = dt.Rows(0).Item("avg_wt").ToString
                                                    dr("Average Handling Time") = dt.Rows(0).Item("avg_ht").ToString
                                                    dr("Max HT (Min)") = dt.Rows(0).Item("max_ht").ToString
                                                    dr("Min HT (Min)") = dt.Rows(0).Item("min_ht").ToString

                                                    dr("Meet KPI") = dt.Rows(0).Item("serve_within_kpi").ToString
                                                    dr("KPI (%)") = dt.Rows(0).Item("ht_within_kpi_per").ToString
                                                    ret.Rows.Add(dr)
                                                End If


                                                ST = DateAdd(DateInterval.Minute, Interval, ST)
                                                ET = DateAdd(DateInterval.Minute, Interval, ET)
                                            Loop While ST < CurrTimeEnd
                                            ST = InputPara.TimePeroidFrom
                                            ET = DateAdd(DateInterval.Minute, Interval, ST)
                                        Next
                                        CurrentDate = DateAdd(DateInterval.Day, 1, CurrentDate)
                                    Loop While CurrentDate <= InputPara.DateTo
                                Next
                            End If

                        End If
                    End If
                Next
            End If
            ret.TableName = "RepAverageServiceTimeComparingWithKPI"
            Return ret
        End Function

        Public Function FixDate(ByVal StringDate As String) As String 'Convert วันที่ให้เป็น YYYYMMDD
            Dim d As String = ""
            Dim m As String = ""
            Dim y As String = ""
            If IsDate(StringDate) Then
                Dim dmy As Date = CDate(StringDate)
                d = dmy.Day
                m = dmy.Month
                y = dmy.Year
                If y > 2500 Then
                    y = y - 543
                End If
                Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
            Else
                Return ""
            End If
        End Function


        Public Function RenderReportByDate(ByVal InputPara As AverageServiceTimeWithKPI_byStaffPara) As DataTable
            Dim ret As String = ""

            Dim WhText As String = "shop_id in (" & InputPara.ShopID & ")"
            WhText += " and convert(varchar(10),service_date,120) >= '" & InputPara.DateFrom.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            WhText += " and convert(varchar(10),service_date,120) <= '" & InputPara.DateTo.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRepAvgServiceTimeKpiStaffDayCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(WhText, "shop_name_en,service_date", trans.Trans)
            trans.CommitTransaction()
            Return dt
        End Function
    End Class
End Namespace


