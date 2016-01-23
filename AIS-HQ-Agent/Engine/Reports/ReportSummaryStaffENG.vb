Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportSummaryStaffENG : Inherits ReportsENG

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

        Public Sub SummaryStaffProcessAllReport()
            ProcReport(_ServiceDate, _ShopID, _lblTime)
        End Sub
        Public Sub ProcReport(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
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
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.SummaryStaff, ServiceDate)

            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_SUMMARY_REPORT_STAFF where convert(varchar(10), service_date,120) = '" & _
                                                                 ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then

                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportSummaryStaffENG.ProcReport")
                    '==shTran1
                    If shTrans.Trans IsNot Nothing Then

                        Dim dtUser As DataTable = GetUserData(ServiceDate, shTrans)
                        shTrans.CommitTransaction()
                        If dtUser.Rows.Count = 0 Then
                            Exit Sub
                        End If

                        For Each drUser As DataRow In dtUser.Rows
                            Dim dt As New DataTable
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportSummaryStaffENG.ProcReport")
                            dt = GetDataFromQueueHistory(ServiceDate, drUser("user_code") & "", shTrans)
                            shTrans.CommitTransaction()
                            '===shTran2
                            If dt.Rows.Count > 0 Then
                                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportSummaryStaffENG.ProcReport")
                                If shTrans.Trans IsNot Nothing Then
                                    For Each dr As DataRow In dt.Rows
                                        '====== Start
                                        Dim lnq As New CenLinqDB.TABLE.TbRepSummaryReportStaffCenLinqDB
                                        With lnq
                                            .SHOP_ID = shDt.Rows(0).Item("ID") & ""
                                            .SHOP_NAME_EN = shDt.Rows(0).Item("SHOP_NAME_EN") & ""
                                            .SHOP_NAME_TH = shDt.Rows(0).Item("SHOP_NAME_TH") & ""
                                            .SHOP_LOCATION_GROUP = shDt.Rows(0).Item("location_group") & ""
                                            .SHOW_DATE = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                                            .SERVICE_DATE = ServiceDate

                                            If Convert.IsDBNull(drUser("staff_name")) = False Then
                                                .STAFF_NAME = drUser("staff_name") & ""
                                                .EMP_ID = drUser("user_code") & ""
                                            Else
                                                .STAFF_NAME = "#"
                                                .EMP_ID = "#"
                                            End If

                                            .ITEM_ID = dr.Item("ITEM_ID") & ""
                                            .ITEM_NAME_EN = dr.Item("ITEM_NAME") & ""
                                            .ITEM_NAME_TH = dr.Item("ITEM_NAME_TH") & ""
                                            .AHT = dr.Item("AHT") & ""
                                            .CANCLE = dr.Item("CANCEL") & ""
                                            .COUNT_HT = dr.Item("cht") & ""
                                            .MAX_HT = dr.Item("MAX_HT") & ""
                                            .MISSED_CALL = dr.Item("misscall") & ""
                                            .NETWORK_TYPE = dr.Item("NetworkType") & ""
                                            .P_HT = dr.Item("P_HT") & ""
                                            .REGIS = dr.Item("REGIS") & ""
                                            .SEGMENT_TYPE = dr.Item("customertype_name") & ""
                                            .SERVE = dr.Item("SERVE") & ""
                                            .SUM_HT = dr.Item("SUM_HT") & ""

                                            Dim vTotProd As Integer = GetSumReasonByDate(ServiceDate, drUser("user_id") & "", "", "1", shTrans)
                                            Dim vTotNonProd As Integer = GetSumReasonByDate(ServiceDate, drUser("user_id") & "", "", "0", shTrans)
                                            Dim Prod As String = GetFormatTimeFromSec(vTotProd)
                                            Dim NonProd As String = GetFormatTimeFromSec(vTotNonProd)
                                            Dim TOTAL_TIME As String = GetFormatTimeFromSec(GetSecFromTimeFormat(drUser("ET") & "") - GetSecFromTimeFormat(drUser("ST") & ""))
                                            Dim SERVICE_TIME As String
                                            If Prod <> "" And NonProd <> "" Then
                                                SERVICE_TIME = GetFormatTimeFromSec(GetSecFromTimeFormat(TOTAL_TIME) - (GetSecFromTimeFormat(Prod) + GetSecFromTimeFormat(NonProd)))
                                            Else
                                                SERVICE_TIME = GetFormatTimeFromSec(0)
                                            End If

                                            .PRODUCTIVITY = GetFormatTimeFromSec(GetSecFromTimeFormat(SERVICE_TIME) + GetSecFromTimeFormat(Prod))
                                            .NON_PRODUCTIVITY = NonProd
                                            .P_PRODUCTIVITY = (GetSecFromTimeFormat(.PRODUCTIVITY) * 100) / GetSecFromTimeFormat(TOTAL_TIME)
                                            .P_NON_PRODUCTIVITY = (GetSecFromTimeFormat(.NON_PRODUCTIVITY) * 100) / GetSecFromTimeFormat(TOTAL_TIME)
                                            .TOTAL_TIME = TOTAL_TIME
                                            .WAIT_WITH_KPI = dr.Item("wt_with_kpi") & ""
                                            .SERVE_WITH_KPI = dr.Item("ht_with_kpi") & ""
                                        End With
                                        '====== End

                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim ret As Boolean = False
                                        ret = lnq.InsertData("ProcessReports", trans.Trans)
                                        If ret = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("ReportSummaryStaffENG.ProcReport", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsSummaryStaffENG")
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


                        Next 'dtUser
                       


                    Else '===End shTrans1
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If


                End If ' trans
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub

        Private Function GetDataFromQueueHistory(ByVal ServiceDate As DateTime, ByVal UserCode As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""

            sql += " Select 	case when NetworkType IS null then 'Other'" & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G'" & vbNewLine
            sql += " else '2G' end end NetworkType," & vbNewLine
            sql += " customertype_name,tbi.master_itemid item_id," & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall," & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when sum(serve) = 0 then 0 else ((sum(ht_with_kpi)*100)/ sum(serve)) end p_ht," & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht," & vbNewLine
            sql += " MAX(ht) max_ht,sum(cht) cht ,sum(ht) sum_ht" & vbNewLine
            sql += " From VW_Report VW" & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id" & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) = " & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' and user_code='" & UserCode & "' " & vbNewLine
            sql += " and VW.item_id is not null " & vbNewLine
            sql += " group by case when NetworkType IS null then 'Other'" & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G'" & vbNewLine
            sql += " else '2G' end end,customertype_name,tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th" & vbNewLine

            sql += " union " & vbNewLine

            sql += " Select 'All' NetworkType,'All' customertype_name,tbi.master_itemid item_id," & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall," & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when sum(serve) = 0 then 0 else ((sum(ht_with_kpi)*100)/ sum(serve)) end p_ht," & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht," & vbNewLine
            sql += " MAX(ht) max_ht,sum(cht) cht ,sum(ht) sum_ht" & vbNewLine
            sql += " From VW_Report VW" & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id" & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) = " & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' and user_code='" & UserCode & "' " & vbNewLine
            sql += " and VW.item_id is not null " & vbNewLine
            sql += " group by tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th" & vbNewLine

            sql += " union" & vbNewLine

            sql += " Select 'All' NetworkType,customertype_name,tbi.master_itemid item_id," & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall," & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when sum(serve) = 0 then 0 else ((sum(ht_with_kpi)*100)/ sum(serve)) end p_ht," & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht," & vbNewLine
            sql += " MAX(ht) max_ht,sum(cht) cht ,sum(ht) sum_ht" & vbNewLine
            sql += " From VW_Report VW" & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id" & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) = " & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' and user_code='" & UserCode & "' " & vbNewLine
            sql += " and VW.item_id is not null " & vbNewLine
            sql += " group by customertype_name,tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th" & vbNewLine

            sql += " union" & vbNewLine

            sql += " Select case when NetworkType IS null then 'Other'" & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G'" & vbNewLine
            sql += " else '2G' end end NetworkType," & vbNewLine
            sql += " 'All' customertype_name,tbi.master_itemid item_id," & vbNewLine
            sql += " TBI.item_name,TBI.item_name_th,sum(regis) regis,sum(serve) serve,sum(misscall) misscall," & vbNewLine
            sql += " sum(cancel) cancel, " & vbNewLine
            sql += " sum(case when status = 3 then case when assign_time is null then 0 else case when DATEDIFF(SECOND,assign_time,call_time) <= item_wait * 60 then 1 else 0 end end else 0 end) wt_with_kpi," & vbNewLine
            sql += " sum(ht_with_kpi) ht_with_kpi, " & vbNewLine
            sql += " case when sum(serve) = 0 then 0 else ((sum(ht_with_kpi)*100)/ sum(serve)) end p_ht," & vbNewLine
            sql += " case when SUM(cht) = 0 then 0 else SUM(ht)/SUM(cht) end aht," & vbNewLine
            sql += " MAX(ht) max_ht,sum(cht) cht ,sum(ht) sum_ht" & vbNewLine
            sql += " From VW_Report VW" & vbNewLine
            sql += " Left Join TB_ITEM TBI On VW.item_id=TBI.id" & vbNewLine
            sql += " Where convert(varchar(8), service_date,112) =" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & vbNewLine
            sql += " and CONVERT(varchar(5),service_date,114) >= '08:00'"
            sql += " and CONVERT(varchar(5),service_date,114) < '22:00'"
            sql += " and UPPER(isnull(staff,'')) not like '%ADMIN%' and user_code='" & UserCode & "' " & vbNewLine
            sql += " and VW.item_id is not null " & vbNewLine
            sql += " group by case when NetworkType IS null then 'Other'" & vbNewLine
            sql += " else case when CHARINDEX('3G',NetworkType)>0 then '3G'" & vbNewLine
            sql += " else '2G' end end ,tbi.master_itemid, " & vbNewLine
            sql += " TBI.item_name, TBI.item_name_th"

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)

            Return dt
        End Function

        Private Function GetUserData(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(GetQuery(ServiceDate), shTrans.Trans)
            lnq = Nothing
            Return dt
        End Function

        Private Function GetQuery(ByVal ServiceDate As DateTime) As String
            Dim sql As String = ""
            sql += " declare @Date as varchar(8); select @Date = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "';" & vbNewLine
            sql += " select main.user_id,username,user_code,fname + ' ' + lname as staff_name"
            sql += " ,CONVERT(varchar(8),min(service_date),114) as ST"
            sql += " ,CONVERT(varchar(8),max(service_date),114) as ET "
            sql += " from TB_LOG_LOGIN as Main "
            sql += " left join TB_USER on Main.user_id = TB_USER.id"
            sql += " where CONVERT(varchar(8),service_date,112) = @Date"
            sql += " and upper(isnull(username,'')) <> 'ADMIN' "
            sql += " group by user_id,username,fname,lname,user_code order by staff_name"
            Return sql
        End Function

    End Class

End Namespace
