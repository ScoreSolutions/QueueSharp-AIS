Imports System.Windows.Forms
Imports System.IO
Namespace Reports
    Public Class ReportsENG

        Protected Structure ReasonData
            Dim SumReasonTime As Integer
            Dim CountReasonTime As Integer
        End Structure

        Protected Function SaveProcessLog(ByVal ReportName As String, ByVal ServiceDate As DateTime) As Long
            Dim ret As Long = 0
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbReportProcessLogCenLinqDB
                lnq.REPORT_NAME = ReportName
                lnq.SERVICE_DATE = ServiceDate
                lnq.PROC_START_DATE = DateTime.Now

                Dim tmp As Boolean = lnq.InsertData("ReportENG.SaveProcessLOG", trans.Trans)
                If tmp = True Then
                    ret = lnq.ID
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If
            Return ret
        End Function

        Protected Function UpdateProcessTime(ByVal vID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbReportProcessLogCenLinqDB
                lnq.GetDataByPK(vID, trans.Trans)
                lnq.PROC_END_DATE = DateTime.Now
                ret = lnq.UpdateByPK("ReportENG.UpdateProcessTime", trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If
            Return ret
        End Function

        Protected Function UpdateProcessError(ByVal vID As Long, ByVal ErrMsg As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbReportProcessLogCenLinqDB
                lnq.GetDataByPK(vID, trans.Trans)
                lnq.ERR_MSG += vbNewLine & ErrMsg
                ret = lnq.UpdateByPK("ReportENG.UpdateProcessError", trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If
            Return ret
        End Function

        Public Shared Function GetFormatTimeFromSec(ByVal TimeSec As Integer) As String
            'แปลงเวลาจากวินาทีไปเป็น HH:mm:ss
            Dim tHour As Integer = 0
            Dim tMin As Integer = 0
            Dim tSec As Integer = 0
            If TimeSec >= 3600 Then
                tHour = Math.Floor(TimeSec / 3600)
                tMin = Math.Floor((TimeSec - (tHour * 3600)) / 60)
                tSec = (TimeSec - (tHour * 3600)) Mod 60
            Else
                tMin = Math.Floor(TimeSec / 60)
                tSec = TimeSec Mod 60
            End If

            Return tHour.ToString.PadLeft(2, "0") & ":" & tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
        End Function

        Public Shared Function GetSecFromTimeFormat(ByVal TimeFormat As String) As Integer
            'แปลงเวลาในรูปแบบ HH:mm:ss ไปเป็นวินาที

            Dim ret As Int32 = 0
            If TimeFormat.Trim <> "" Then
                Dim tmp() As String = Split(TimeFormat, ":")
                Dim TimeSec As Integer = 0
                If Convert.ToInt64(tmp(0)) > 0 Then
                    TimeSec += (Convert.ToInt64(tmp(0)) * 60 * 60)
                End If
                If Convert.ToInt64(tmp(1)) > 0 Then
                    TimeSec += (Convert.ToInt64(tmp(1)) * 60)
                End If
                ret = TimeSec + Convert.ToInt32(tmp(2))
            End If
            Return ret
        End Function

        Public Shared Function GetUserByQTrans(ByVal ServiceDate As DateTime, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim ret As New DataTable
            Dim StrDate As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsENG.GetUserByQTrans")
            If shTrans.Trans IsNot Nothing Then
                Dim wh As String = " id in(select distinct [user_id] from tb_counter_queue_history where convert(varchar(8),service_date,112) between '" & StrDate & "' and '" & StrDate & "')"
                wh += " and upper(isnull(username,'')) <> 'ADMIN' "
                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                ret = lnq.GetDataList(wh, "fname, lname", shTrans.Trans)
                lnq = Nothing
            End If

            Return ret
        End Function

        Public Shared Function GetFirstDayOfWeek(ByVal ServiceDate As Date) As Date
            Dim CalDate As Date = DateAdd(DateInterval.Day, -1, ServiceDate)
            Dim FirstDay As Date = DateAdd(DateInterval.Day, 1 - CalDate.DayOfWeek, CalDate)
            If FirstDay < New Date(ServiceDate.Year, 1, 1) Then
                FirstDay = New Date(ServiceDate.Year, 1, 1)
            End If
            Return FirstDay
        End Function

        Public Shared Function GetLastDayOfWeek(ByVal ServiceDate As Date) As Date
            Dim CalDate As Date = DateAdd(DateInterval.Day, -1, ServiceDate)
            Dim LastDay As Date = DateAdd(DateInterval.Day, 7 - CalDate.DayOfWeek, CalDate)
            If LastDay > New Date(ServiceDate.Year, 12, 31) Then
                LastDay = New Date(ServiceDate.Year, 12, 31)
            End If
            Return LastDay
        End Function

#Region "Productivity"

        Protected Function GetSumReasonByDate(ByVal ServiceDate As DateTime, ByVal UserID As Integer, ByVal ReasonName As String, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer
            Dim dt As New DataTable
            Dim sql As String = ""
            sql += " declare @Date as varchar(10); select @Date = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "';" & vbNewLine
            sql += " select user_id,SUM([Time]) as [Time]  " & vbNewLine
            sql += " from (" & vbNewLine
            sql += "    select user_id,sum(DATEDIFF(SECOND,ST,ET)) as [Time]  " & vbNewLine
            sql += "    from (" & vbNewLine
            sql += "        select user_id,reason_id,name as reason_name,TB_LOGOUT_REASON.productive,service_date as ST," & vbNewLine
            sql += "            (Select min(service_date)" & vbNewLine
            sql += "            from TB_LOG_LOGIN" & vbNewLine
            sql += "            where CONVERT(varchar(10),service_date,120) = @Date and reason_id = 0 and action = 1" & vbNewLine
            sql += "            and user_id = Main.user_id and service_date > Main.service_date) as ET" & vbNewLine
            sql += "        from TB_LOG_LOGIN as Main " & vbNewLine
            sql += "        left join TB_LOGOUT_REASON on Main.reason_id = TB_LOGOUT_REASON.id" & vbNewLine
            sql += "        where CONVERT(varchar(10),service_date,120) = @Date and reason_id > 0 ) TB" & vbNewLine
            sql += "    where ET is not null and productive = " & Productive & " and user_id = " & UserID & vbNewLine
            If ReasonName <> "" Then
                sql += "    and upper(reason_name) = '" & ReasonName.ToUpper & "'"
            End If
            sql += "    group by user_id" & vbNewLine
            sql += "    union all" & vbNewLine
            sql += "    select user_id,sum(DATEDIFF(SECOND,ST,ET)) as [Time]  " & vbNewLine
            sql += "    from (" & vbNewLine
            sql += "        select user_id,reason_id,name as reason_name,TB_HOLD_REASON.productive,service_date as ST," & vbNewLine
            sql += "            (Select min(service_date)" & vbNewLine
            sql += "            from TB_LOG_HOLD " & vbNewLine
            sql += "            where CONVERT(varchar(10),service_date,120) = @Date and reason_id = 0 and action =2" & vbNewLine
            sql += "            and user_id = Main.user_id and service_date > Main.service_date ) as ET" & vbNewLine
            sql += "        from TB_LOG_HOLD as Main" & vbNewLine
            sql += "        left join TB_HOLD_REASON on Main.reason_id = TB_HOLD_REASON.id" & vbNewLine
            sql += "        where CONVERT(varchar(10),service_date,120) = @Date and reason_id > 0) TB " & vbNewLine
            sql += "    where ET is not null and productive = " & Productive & " and user_id = " & UserID & vbNewLine
            If ReasonName <> "" Then
                sql += "    and upper(reason_name) = '" & ReasonName.ToUpper & "'"
            End If
            sql += "    group by user_id ) as TB" & vbNewLine
            sql += " group by user_id" & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbLogHoldShLinqDB
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Time")
            End If
            Return 0
        End Function

        Protected Function GetSumOtherReasonByDate(ByVal ServiceDate As DateTime, ByVal UserID As Integer, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer

            Dim OtherReason As String = ""
            If Productive = "1" Then
                OtherReason = "'LEARNING','STAND BY','BRIEF','WARP UP','CONSULT'"
            Else
                OtherReason = "'LUNCH','LEAVE','CHANGE COUNTER','HOME','MINI BREAK','REST ROOM'"
            End If

            Dim dt As New DataTable
            Dim sql As String = ""
            sql += " declare @Date as varchar(10); select @Date = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "';" & vbNewLine
            sql += " select user_id,SUM([Time]) as [Time]  " & vbNewLine
            sql += " from (" & vbNewLine
            sql += "    select user_id,sum(DATEDIFF(SECOND,ST,ET)) as [Time]  " & vbNewLine
            sql += "    from (" & vbNewLine
            sql += "        select user_id,reason_id,name as reason_name,TB_LOGOUT_REASON.productive,service_date as ST," & vbNewLine
            sql += "            (Select min(service_date)" & vbNewLine
            sql += "            from TB_LOG_LOGIN" & vbNewLine
            sql += "            where CONVERT(varchar(10),service_date,120) = @Date and reason_id = 0 and action = 1" & vbNewLine
            sql += "            and user_id = Main.user_id and service_date > Main.service_date) as ET" & vbNewLine
            sql += "        from TB_LOG_LOGIN as Main " & vbNewLine
            sql += "        left join TB_LOGOUT_REASON on Main.reason_id = TB_LOGOUT_REASON.id" & vbNewLine
            sql += "        where CONVERT(varchar(10),service_date,120) = @Date and reason_id > 0 ) TB" & vbNewLine
            sql += "    where ET is not null and productive = " & Productive & " and user_id = " & UserID & vbNewLine
            sql += "    and upper(reason_name) not in (" & OtherReason & ") "
            sql += "    group by user_id" & vbNewLine

            sql += "    union all" & vbNewLine

            sql += "    select user_id,sum(DATEDIFF(SECOND,ST,ET)) as [Time]  " & vbNewLine
            sql += "    from (" & vbNewLine
            sql += "        select user_id,reason_id,name as reason_name,TB_HOLD_REASON.productive,service_date as ST," & vbNewLine
            sql += "            (Select min(service_date)" & vbNewLine
            sql += "            from TB_LOG_HOLD " & vbNewLine
            sql += "            where CONVERT(varchar(10),service_date,120) = @Date and reason_id = 0 and action =2" & vbNewLine
            sql += "            and user_id = Main.user_id and service_date > Main.service_date ) as ET" & vbNewLine
            sql += "        from TB_LOG_HOLD as Main" & vbNewLine
            sql += "        left join TB_HOLD_REASON on Main.reason_id = TB_HOLD_REASON.id" & vbNewLine
            sql += "        where CONVERT(varchar(10),service_date,120) = @Date and reason_id > 0) TB " & vbNewLine
            sql += "    where ET is not null and productive = " & Productive & " and user_id = " & UserID & vbNewLine
            sql += "    and upper(reason_name) not in (" & OtherReason & ") "
            sql += "    group by user_id ) as TB" & vbNewLine
            sql += " group by user_id" & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbLogHoldShLinqDB
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Time")
            End If
            Return 0
        End Function

        Protected Function GetSumReasonByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As Integer, ByVal ReasonName As String, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer
            Dim ret As Integer = 0
            Dim CurrDate As DateTime = FirstDay
            Dim SumItemTime As Long = 0
            Do
                Dim ItemTime As Long = GetSumReasonByDate(CurrDate, UserID, ReasonName, Productive, shTrans)
                If ItemTime > 0 Then
                    SumItemTime += ItemTime
                End If

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")

            Return Convert.ToInt32(SumItemTime)
        End Function

        Protected Function GetReasonDataByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As Integer, ByVal ReasonName As String, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ReasonData
            Dim ret As ReasonData
            Dim CurrDate As DateTime = FirstDay
            Dim SumItemTime As Integer = 0
            Dim CountTime As Integer = 0
            Do

                Dim ItemTime As Long = GetSumReasonByDate(CurrDate, UserID, ReasonName, Productive, shTrans)
                If ItemTime > 0 Then
                    SumItemTime += ItemTime
                    CountTime += 1
                End If

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")
            ret.SumReasonTime = SumItemTime
            ret.CountReasonTime = CountTime
            Return ret
        End Function

        Protected Function GetOtherReasonDataByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As Integer, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ReasonData
            Dim ret As ReasonData
            Dim CurrDate As DateTime = FirstDay
            Dim SumItemTime As Integer = 0
            Dim CountTime As Integer = 0
            Do
                Dim ItemTime As Long = GetSumOtherReasonByDate(CurrDate, UserID, Productive, shTrans)
                If ItemTime > 0 Then
                    SumItemTime += ItemTime
                    CountTime += 1
                End If

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")
            ret.SumReasonTime = SumItemTime
            ret.CountReasonTime = CountTime
            Return ret
        End Function
#End Region

        Protected Sub CreateLogFile(ByVal ClassName As String, ByVal FuncationName As String, ByVal TextMsg As String)
            Try
                Dim LogDir As String = Application.StartupPath & "\LoadCubeLog\"
                If Directory.Exists(LogDir) = False Then
                    Directory.CreateDirectory(LogDir)
                End If

                Dim FILE_NAME As String = LogDir & DateTime.Now.ToString("yyyyMMddHH") & ".log"
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & ClassName & "." & FuncationName & " : " & TextMsg & Chr(13) & Chr(13))
                objWriter.Close()
            Catch ex As Exception

            End Try
        End Sub

    End Class
End Namespace

