Imports CenParaDB.ReportCriteria
Imports Engine.Common
Imports ShParaDB.Common.Utilities
Namespace Reports

    Public Class RepWaitingTimeHandlingTimeBySkillENG
        Public Function GetReportByTime(ByVal InputPara As CustByWaitingTimePara) As DataTable
            Dim ret As New DataTable
            'Create DataTable
            ret.Columns.Add("Date")
            ret.Columns.Add("Shop Name")
            ret.Columns.Add("Time")

            'วนลูปตาม Shop ก่อน
            Dim ShopID() As String = Split(InputPara.ShopID, ",")
            Dim cenTrans As New CenLinqDB.Common.Utilities.TransactionDB
            If cenTrans.Trans IsNot Nothing Then
                For Each shID As String In ShopID
                    If shID.Trim <> "" Then
                        Dim ShopLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(Convert.ToInt64(shID))
                        If ShopLnq.ID <> 0 Then
                            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                            shTrans = FunctionEng.GetShTransction(shTrans, cenTrans, ShopLnq)
                            If shTrans.Trans IsNot Nothing Then
                                'หา Service ของ Shop
                                Dim sLnq As New ShLinqDB.TABLE.TbSkillShLinqDB
                                Dim sDt As DataTable = sLnq.GetDataList("active_status='1' and appointment = 0 ", "id", shTrans.Trans)
                                If sDt.Rows.Count > 0 Then
                                    For Each sDr As DataRow In sDt.Rows
                                        If ret.Columns.Contains(sDr("skill") & " AWT") = False Then
                                            ret.Columns.Add(sDr("skill") & " AWT")
                                        End If
                                        If ret.Columns.Contains(sDr("skill") & " AHT") = False Then
                                            ret.Columns.Add(sDr("skill") & " AHT")
                                        End If
                                        If ret.Columns.Contains(sDr("skill") & " Total Regis") = False Then
                                            ret.Columns.Add(sDr("skill") & " Total Regis", GetType(Long))
                                        End If
                                        If ret.Columns.Contains(sDr("skill") & " Total Served") = False Then
                                            ret.Columns.Add(sDr("skill") & " Total Served", GetType(Long))
                                        End If
                                        If ret.Columns.Contains(sDr("skill") & " Total Misscall") = False Then
                                            ret.Columns.Add(sDr("skill") & " Total Misscall", GetType(Long))
                                        End If
                                        If ret.Columns.Contains(sDr("skill") & " Total Cancelled") = False Then
                                            ret.Columns.Add(sDr("skill") & " Total Cancelled", GetType(Long))
                                        End If
                                        If ret.Columns.Contains(sDr("skill") & " Total Incomplete") = False Then
                                            ret.Columns.Add(sDr("skill") & " Total Incomplete", GetType(Long))
                                        End If
                                    Next
                                End If
                                'จากนั้นก็ลูปตามวันที่
                                Dim CurrDate As Date = InputPara.DateFrom
                                Do
                                    Dim DateDisplay As String = CurrDate.ToString("dd/MM/yyyy", New System.Globalization.CultureInfo("en-US"))

                                    'หาหน่วยของเวลาด้วยนะ
                                    Dim sTime() As String = Split(InputPara.TimePeroidFrom, ":")
                                    Dim sHour As Integer = sTime(0)
                                    Dim sMin As Integer = sTime(1)
                                    Dim CurrTime As DateTime = New DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, sHour, sMin, 0)

                                    Dim eTime() As String = Split(InputPara.TimePeroidTo, ":")
                                    Dim eHour As Integer = eTime(0)
                                    Dim eMin As Integer = eTime(1)
                                    Dim EndTime As DateTime = New DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, eHour, eMin, 0)
                                    Do
                                        If shTrans.Trans IsNot Nothing Then
                                            Dim dr As DataRow = ret.NewRow
                                            Dim TimeTo As DateTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
                                            If TimeTo > EndTime Then
                                                TimeTo = EndTime
                                            End If

                                            dr("Date") = DateDisplay
                                            dr("Shop Name") = ShopLnq.SHOP_NAME_TH
                                            dr("Time") = CurrTime.ToString("HH:mm") & " - " & TimeTo.ToString("HH:mm")

                                            For Each sDr As DataRow In sDt.Rows
                                                dr(sDr("skill") & " AWT") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "W", shTrans)
                                                dr(sDr("skill") & " AHT") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "H", shTrans)
                                                dr(sDr("skill") & " Total Regis") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "R", shTrans)
                                                dr(sDr("skill") & " Total Served") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "S", shTrans)
                                                dr(sDr("skill") & " Total Misscall") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "M", shTrans)
                                                dr(sDr("skill") & " Total Cancelled") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "C", shTrans)
                                                dr(sDr("skill") & " Total Incomplete") = GetServiceTime(CurrTime, TimeTo, sDr("id"), "I", shTrans)
                                            Next
                                            ret.Rows.Add(dr)
                                        End If
                                        CurrTime = DateAdd(DateInterval.Minute, InputPara.IntervalMinute, CurrTime)
                                    Loop While CurrTime <= EndTime

                                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                                Loop While CurrDate <= InputPara.DateTo
                                shTrans.CommitTransaction()
                            Else
                                FunctionEng.SaveErrorLog("RepWaitingTimeHandlingTimeBySkill.GetReportByTime", shTrans.ErrorMessage & " Shop ID = " & ShopLnq.ID)
                            End If
                            shTrans.CommitTransaction()
                        End If
                    End If
                Next
                ret.TableName = "RepCustByWaitingTimeByTime"
                'ReportsENG.SetDataToTable(ret, InputPara.ProcessUser, cenTrans)
            End If
            cenTrans.CommitTransaction()
            Return ret
        End Function

        Private Function GetServiceTime(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime, ByVal ServiceID As Long, ByVal TimeType As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""
            'TimeType 
            '' W= Avg Waiting Time
            '' H= Avg Handling Time
            '' R= Total Regis
            '' S= Total Served
            Dim StrTimeFrom As String = TimeFrom.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim StrTimeTo As String = TimeTo.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US"))
            Dim sql As String = ""
            sql += "select count(cq.id) qty, AVG(DATEDIFF(SECOND, assign_time,start_time)) sumAWT, "
            sql += " AVG(DATEDIFF(SECOND, start_time,end_time)) sumAHT"
            sql += " from tb_counter_queue_history cq "
            sql += " where cq.item_id =" & ServiceID

            sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "

            If TimeType = "S" Then
                sql += " and status = 3 "
            End If

            'If TimeType = "W" Or TimeType = "R" Then
            '    sql += " and convert(varchar(16), cq.service_date, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.service_date, 120) < '" & StrTimeTo & "' "
            'ElseIf TimeType = "H" Or TimeType = "S" Then
            '    sql += " and convert(varchar(16), cq.start_time, 120) >= '" & StrTimeFrom & "' "
            '    sql += " and convert(varchar(16), cq.start_time, 120) < '" & StrTimeTo & "' "
            'End If

            Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                If TimeType = "R" Or TimeType = "S" Then
                    ret = Convert.ToInt64(dt.Rows(0)("qty"))
                Else
                    Dim tSec As Long = 0
                    If TimeType = "W" Then
                        If Convert.IsDBNull(dt.Rows(0)("sumAWT")) = False Then
                            tSec = Convert.ToInt64(dt.Rows(0)("sumAWT"))
                        End If
                    ElseIf TimeType = "H" Then
                        If Convert.IsDBNull(dt.Rows(0)("sumAHT")) = False Then
                            tSec = Convert.ToInt64(dt.Rows(0)("sumAHT"))
                        End If
                    End If
                    Dim cMin As Integer = Math.Floor(tSec / 60)
                    Dim cSec As Integer = tSec Mod 60

                    ret = cMin.ToString & ":" & cSec.ToString.PadLeft(2, "0")
                End If
            End If

            Return ret
        End Function




    End Class
End Namespace

