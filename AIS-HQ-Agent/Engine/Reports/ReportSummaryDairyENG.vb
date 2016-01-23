Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportSummaryDairyENG : Inherits ReportsENG

        Dim _ServiceDate As New DateTime(1, 1, 1)
        Dim _ShopID As Long = 0
        Dim _lblTime As Label
        Public WriteOnly Property ServiceDate()
            Set(ByVal value)
                _ServiceDate = value
            End Set
        End Property
        Public WriteOnly Property ShopID() As Long
            Set(ByVal value As Long)
                _ShopID = value
            End Set
        End Property
        Public WriteOnly Property lblTime() As Label
            Set(ByVal value As Label)
                _lblTime = value
            End Set
        End Property

        Public Sub SummaryDailyProcessAllReport()
            ProcReport(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReport(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Try
                ServiceDate = New Date(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day)

                Dim shSql As String = "select s.id,s.shop_db_name,s.shop_db_userid, s.shop_db_server, s.shop_db_pwd, "
                shSql += " s.shop_name_en, s.shop_name_th, r.location_group"
                shSql += " from TB_SHOP s"
                shSql += " inner join TB_REGION r on r.id=s.region_id"
                shSql += " where s.id='" & ShopID & "'"
                shSql += " order by r.location_group, s.shop_name_en"


                Dim shDt As New System.Data.DataTable
                shDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(shSql)
                If shDt.Rows.Count = 0 Then
                    Exit Sub
                End If

                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
                Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.SummaryDaily, ServiceDate)

                If ProcID <> 0 Then
                    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_SUMMARY_DAILY_REPORT where convert(varchar(10), service_date,120) = '" & _
                                                                     ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                    dTrans.CommitTransaction()

                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    If trans.Trans IsNot Nothing Then

                        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportSummaryDailyENG.ProcReport")
                        '==shTran1
                        If shTrans.Trans IsNot Nothing Then

                            Dim dt As New DataTable
                            dt = GetDataFromQueueHistory(ServiceDate, shTrans)
                            shTrans.CommitTransaction()
                            '===shTran2
                            If dt.Rows.Count > 0 Then
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportSummaryDailyENG.ProcReport")
                                If shTrans.Trans IsNot Nothing Then

                                    For Each dr As DataRow In dt.Rows

                                        '====== Start
                                        Dim lnq As New CenLinqDB.TABLE.TbRepSummaryDailyReportCenLinqDB
                                        With lnq
                                            .SHOP_ID = shDt.Rows(0).Item("ID") & ""
                                            .SHOP_NAME_EN = shDt.Rows(0).Item("SHOP_NAME_EN") & ""
                                            .SHOP_NAME_TH = shDt.Rows(0).Item("SHOP_NAME_TH") & ""
                                            .SHOP_LOCATION_GROUP = shDt.Rows(0).Item("location_group") & ""
                                            .SERVICE_DATE = ServiceDate
                                            .SHOW_DATE = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                            .SHOW_DAY = ServiceDate.DayOfWeek.ToString
                                            .ITEM_ID = dr.Item("ITEM_ID") & ""
                                            .ITEM_NAME_EN = dr.Item("ITEM_NAME") & ""
                                            .ITEM_NAME_TH = dr.Item("ITEM_NAME_TH") & ""
                                            .AHT = dr.Item("AHT") & ""
                                            .AWT = dr.Item("AWT") & ""
                                            .CANCLE = dr.Item("CANCEL") & ""
                                            .COUNT_HT = dr.Item("cht") & ""
                                            .COUNT_WT = dr.Item("cwt") & ""
                                            '.ID = ""
                                            .MISSED_CALL = dr.Item("misscall") & ""
                                            .NETWORK_TYPE = dr.Item("NetworkType") & ""
                                            .REGIS = dr.Item("REGIS") & ""
                                            .SEGMENT_TYPE = dr.Item("customertype_name") & ""
                                            .SERVE = dr.Item("SERVE") & ""
                                            .SUM_HT = dr.Item("SUM_HT") & ""
                                            .SUM_WT = dr.Item("SUM_WT") & ""
                                            .SERVE_WITH_KPI = dr.Item("ht_with_kpi") & ""
                                            .WAIT_WITH_KPI = dr.Item("wt_with_kpi") & ""
                                        End With
                                        '====== End

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("ReportSummaryDairyENG.ProcReport", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsSummaryDairyENG")
                                        End If

                                        Try
                                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                            Application.DoEvents()
                                        Catch ex As Exception

                                        End Try
                                    Next


                                    shTrans.CommitTransaction()
                                    dt.Dispose()
                                Else '===End shTrans2
                                    UpdateProcessError(ProcID, shTrans.ErrorMessage)
                                End If

                            End If '===End DT_DataFromHistort

                            '===End shTrans1
                        Else
                            '===shTrans1
                            UpdateProcessError(ProcID, shTrans.ErrorMessage)
                        End If


                    End If ' trans
                    UpdateProcessTime(ProcID)
                End If
                shLnq = Nothing
            Catch ex As Exception
                CreateLogFile("ReportSummaryDairyENG", "ProcReport", ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End Sub

        Private Function GetDataFromQueueHistory(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            'sql = "Select NetworkType,customertype_name,tbi.master_itemid item_id, " & _
            '" TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall," & _
            '" sum(cancel) cancel,sum(wt_with_kpi) wt_with_kpi,sum(ht_with_kpi) ht_with_kpi," & _
            '" case when SUM(cwt) = 0 then 0 else SUM(wt)/SUM(cwt) end awt," & _
            '" case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht , " & _
            '" sum(cwt) cwt,sum(wt) sum_wt,sum(cht) cht ,sum(ht) sum_ht" & _
            '" From VW_Report VW" & _
            '" Left Join TB_ITEM TBI On VW.item_id=TBI.id " & _
            '" Where convert(varchar(8), service_date,112) = " & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & _
            '" and UPPER(isnull(staff,'')) not like '%ADMIN%'" & _
            '" group by NetworkType,customertype_name,tbi.master_itemid, " & _
            '" TBI.item_name, TBI.item_name_th"

            sql += " Select 	case when NetworkType IS null then 'Other' " & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G' " & vbNewLine
            sql += " else '2G' end end NetworkType," & vbNewLine
            sql += " customertype_name,tbi.master_itemid item_id," & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall, " & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when SUM(cwt) = 0 then 0 else SUM(wt)/SUM(cwt) end awt, " & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht , " & vbNewLine
            sql += " sum(cwt) cwt,sum(wt) sum_wt,sum(cht) cht ,sum(ht) sum_ht " & vbNewLine
            sql += " From VW_Report VW " & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id " & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) =" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " and VW.item_id is not null" & vbNewLine
            sql += " group by case when NetworkType IS null then 'Other' " & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G' " & vbNewLine
            sql += " else '2G' end end,customertype_name,tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th " & vbNewLine

            sql += "  union " & vbNewLine

            sql += " Select 'All' NetworkType,'All' customertype_name,tbi.master_itemid item_id, " & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall, " & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when SUM(cwt) = 0 then 0 else SUM(wt)/SUM(cwt) end awt, " & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht , " & vbNewLine
            sql += " sum(cwt) cwt,sum(wt) sum_wt,sum(cht) cht ,sum(ht) sum_ht " & vbNewLine
            sql += " From VW_Report VW " & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id " & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) =" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " and VW.item_id is not null" & vbNewLine
            sql += " group by tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th " & vbNewLine

            sql += " union " & vbNewLine

            sql += " Select 'All' NetworkType,customertype_name,tbi.master_itemid item_id, " & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall, " & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when SUM(cwt) = 0 then 0 else SUM(wt)/SUM(cwt) end awt, " & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht , " & vbNewLine
            sql += " sum(cwt) cwt,sum(wt) sum_wt,sum(cht) cht ,sum(ht) sum_ht " & vbNewLine
            sql += " From VW_Report VW " & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id " & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) =" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " and VW.item_id is not null" & vbNewLine
            sql += " group by customertype_name,tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th " & vbNewLine

            sql += " union " & vbNewLine

            sql += " Select case when NetworkType IS null then 'Other' " & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G' " & vbNewLine
            sql += " else '2G' end end NetworkType, " & vbNewLine
            sql += " 'All' customertype_name,tbi.master_itemid item_id, " & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall, " & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when SUM(cwt) = 0 then 0 else SUM(wt)/SUM(cwt) end awt, " & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht , " & vbNewLine
            sql += " sum(cwt) cwt,sum(wt) sum_wt,sum(cht) cht ,sum(ht) sum_ht " & vbNewLine
            sql += " From VW_Report VW " & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id " & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) =" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' " & vbNewLine
            sql += " and VW.item_id is not null" & vbNewLine
            sql += " group by case when NetworkType IS null then 'Other' " & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G' " & vbNewLine
            sql += " else '2G' end end ,tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th"


            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)

            Return dt
        End Function
    End Class
End Namespace
