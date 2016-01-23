Imports System.Windows.Forms
Imports Engine.Common
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportsIntervalPerformanceENG : Inherits ReportsENG
        Public Sub ProcReportByTime(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.IntervalPerformanceReportByTime, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_INTERVAL_PERFORMANCE_TIME where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    'หา Region
                    Dim rgLnq As New CenLinqDB.TABLE.TbRegionCenLinqDB
                    rgLnq.GetDataByPK(shLnq.REGION_ID, Nothing)
                    Dim RegionName As String = IIf(rgLnq.LOCATION_GROUP = "RO", "UPC", rgLnq.LOCATION_GROUP)

                    'หา Interval Time
                    Dim lnqI As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                    Dim dtI As DataTable = lnqI.GetDataList("active='Y'", "", Nothing)
                    trans.CommitTransaction()
                    If dtI.Rows.Count > 0 Then
                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = FunctionEng.GetShTransction(shLnq.ID, "ReportIntervalPerformanceENG.ProcReportByTime")

                        Dim sLnq As New ShLinqDB.TABLE.TbItemShLinqDB
                        Dim sSql As String = "select id,item_name,item_name_th from tb_item where active_status='1' order by id"
                        Dim sDt As DataTable = sLnq.GetListBySql(sSql, shTrans.Trans)
                        If sDt.Rows.Count > 0 Then
                            For Each drI As DataRow In dtI.Rows
                                Dim IntervalMinute As Int64 = Convert.ToInt64(drI("interval_time"))
                                'Loop ตามเวลาที่ เปิด ปิด Shop
                                Dim StartTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 6, 0, 0)
                                Dim EndTime As New DateTime(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day, 22, 0, 0)
                                Dim CurrTime As DateTime = StartTime
                                Do
                                    If CurrTime < EndTime Then
                                        Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                        If TimeTo > EndTime Then
                                            TimeTo = EndTime
                                        End If

                                        shTrans = FunctionEng.GetShTransction(shLnq.ID, "ReportIntervalPerformanceENG.ProcReportByTime")
                                        Dim dt As DataTable = GetDataTableTime(ServiceDate, CurrTime, TimeTo, shTrans)
                                        If dt.Rows.Count > 0 Then
                                            dt.DefaultView.Sort = "networktype,customertype_name,master_itemid"
                                            dt = dt.DefaultView.ToTable

                                            For Each dr As DataRow In dt.Rows
                                                Try
                                                    Dim lnq As New CenLinqDB.TABLE.TbRepIntervalPerformanceTimeCenLinqDB
                                                    lnq.REGION_NAME = RegionName
                                                    lnq.SHOP_ID = shLnq.ID
                                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                                    lnq.NETWORK_TYPE = dr("networktype")
                                                    lnq.CUSTOMERTYPE_NAME = dr("customertype_name")
                                                    lnq.SERVICE_ID = dr("master_itemid")
                                                    lnq.SERVICE_NAME = dr("item_name")
                                                    lnq.INTERVAL_MINUTE = IntervalMinute
                                                    lnq.SERVICE_DATE = New Date(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day)
                                                    lnq.TIME_PRIOD_FROM = CurrTime
                                                    lnq.TIME_PRIOD_TO = TimeTo
                                                    lnq.SHOW_TIME = lnq.TIME_PRIOD_FROM.ToString("HH:mm") & "-" & lnq.TIME_PRIOD_TO.ToString("HH:mm")
                                                    lnq.REGIS = dr("regis")
                                                    lnq.SERVED = dr("serve")
                                                    lnq.MISSED_CALL = dr("misscall")
                                                    lnq.CANCEL = dr("cancel")
                                                    lnq.NOT_CALL = dr("notcall")
                                                    lnq.NOT_CON = dr("notcon")
                                                    lnq.NOT_END = dr("notend")
                                                    lnq.WAIT_WITH_KPI = dr("wt_with_kpi")
                                                    lnq.SERVE_WITH_KPI = dr("ht_with_kpi")
                                                    lnq.MAX_WT = dr("max_wt")
                                                    lnq.MAX_HT = dr("max_ht")
                                                    lnq.AHT = dr("AHT")
                                                    lnq.AWT = dr("AWT")
                                                    lnq.COUNT_WT = dr("cwt")
                                                    lnq.COUNT_HT = dr("cht")
                                                    lnq.SUM_WT = dr("swt")
                                                    lnq.SUM_HT = dr("sht")

                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    Dim ret As Boolean = False
                                                    ret = lnq.InsertData("ProReportByTime", trans.Trans)
                                                    If ret = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("ReportsIntervalPerformanceENG.ProReportByTime", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsIntervalPerformanceENG")
                                                    End If
                                                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                                    Application.DoEvents()
                                                    lnq = Nothing
                                                Catch ex As Exception
                                                    FunctionEng.SaveErrorLog("ReportsIntervalPerformanceENG.ProReportByTime", "Exception : " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsIntervalPerformanceENG")
                                                End Try
                                            Next
                                        End If
                                        dt.Dispose()
                                    End If

                                    CurrTime = DateAdd(DateInterval.Minute, IntervalMinute, CurrTime)
                                Loop While CurrTime <= EndTime
                            Next
                        End If
                        sLnq = Nothing
                        sDt.Dispose()
                    End If
                    lnqI = Nothing
                    dtI.Dispose()
                    rgLnq = Nothing
                End If



                UpdateProcessTime(ProcID)
            End If
        End Sub
        Private Function GetQuery(ByVal IsAllNetworkType As Boolean, ByVal IsAllService As Boolean) As String
            Dim sql As String = ""
            sql = "select customertype_name," & vbNewLine
            If IsAllService = False Then
                sql += " master_itemid, item_name, " & vbNewLine
            Else
                sql += " 0 master_itemid, 'All' item_name," & vbNewLine
            End If
            If IsAllNetworkType = False Then
                sql += " case when networktype IS null then 'Other' " & vbNewLine
                sql += "      else" & vbNewLine
                sql += "        case when CHARINDEX('3G',networktype)>0 then '3G'  else '2G'  end" & vbNewLine
                sql += " end networktype," & vbNewLine
            Else
                sql += " 'All' networktype," & vbNewLine
            End If
            sql += " isnull(sum(regis),0) as regis,isnull(sum(serve),0) as serve, " & vbNewLine
            sql += " isnull(sum(misscall),0) as misscall,isnull(sum(cancel),0) as cancel," & vbNewLine
            sql += " isnull(sum(notcall),0) as notcall,isnull(sum(notcon),0) as notcon," & vbNewLine
            sql += " isnull(sum(notend),0) as notend,isnull(sum(wt_with_kpi),0) as wt_with_kpi," & vbNewLine
            sql += " isnull(sum(ht_with_kpi),0) as ht_with_kpi,isnull(max(wt),0) as max_wt," & vbNewLine
            sql += " isnull(max(ht),0) as max_ht, isnull(AVG(wt),0) as AWT,isnull(AVG(ht),0) as AHT," & vbNewLine
            sql += " isnull(SUM(cwt),0) as CWT,isnull(SUM(wt),0) as SWT," & vbNewLine
            sql += " isnull(SUM(cht),0) as CHT,isnull(SUM(ht),0) as SHT " & vbNewLine
            sql += " from (select r.staff,r.service_date,i.master_itemid,r.item_name,r.regis,r.serve," & vbNewLine
            sql += "        r.misscall,r.cancel,r.notcall,r.notcon,r.notend  ,r.networktype, r.customertype_name," & vbNewLine
            sql += "        case when r.serve = 1 then r.wt_with_kpi else 0 end as wt_with_kpi," & vbNewLine
            sql += "        case when r.serve = 1 then r.ht_with_kpi else 0 end as ht_with_kpi," & vbNewLine
            sql += "        case when r.serve = 1 then r.wt else 0 end as wt," & vbNewLine
            sql += "        case when r.serve = 1 then r.ht else 0 end as ht," & vbNewLine
            sql += "        r.cwt,r.cht"
            sql += "        from vw_report r"
            sql += "        inner join tb_item i on i.id=r.item_id) as TB " & vbNewLine
            sql += " where 1=1 " & vbNewLine
            Return sql
        End Function

        Private Function GetDataTableTime(ByVal ServiceDate As DateTime, ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable

            Dim StrTimeFrom As String = TimeFrom.ToString("HH:mm")
            Dim StrTimeTo As String = DateAdd(DateInterval.Minute, -1, TimeTo).ToString("HH:mm")
            Dim StrDate As String = ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))

            'By Network Type By Service
            Dim sql As String = GetQuery(False, False)
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'" & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by master_itemid, item_name, customertype_name, " & vbNewLine
            sql += " case when networktype IS null then 'Other' " & vbNewLine
            sql += "      else" & vbNewLine
            sql += "        case when CHARINDEX('3G',networktype)>0 then '3G'  else '2G'  end" & vbNewLine
            sql += " end " & vbNewLine

            sql += " UNION ALL" & vbNewLine

            'All Network Type By Service
            sql += GetQuery(True, False)
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'" & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by master_itemid, item_name, customertype_name " & vbNewLine

            sql += " UNION ALL" & vbNewLine

            'By Network Type All Service
            sql += GetQuery(False, True)
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'" & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by  customertype_name, " & vbNewLine
            sql += " case when networktype IS null then 'Other' " & vbNewLine
            sql += "      else" & vbNewLine
            sql += "        case when CHARINDEX('3G',networktype)>0 then '3G'  else '2G'  end" & vbNewLine
            sql += " end " & vbNewLine

            sql += " UNION ALL" & vbNewLine

            'All Network Type All Service
            sql += GetQuery(True, True)
            sql += " and CONVERT(varchar(16),service_date,120) between '" & StrDate & " " & StrTimeFrom & "' and '" & StrDate & " " & StrTimeTo & "'" & vbNewLine
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " group by  customertype_name " & vbNewLine
            
            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function

        
    End Class
End Namespace

