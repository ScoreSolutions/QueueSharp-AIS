Imports Engine.Common
Imports System.Windows.Forms
Imports System.IO

Public Class WorkForceENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox
    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    Public Sub SetTextLog(ByVal txtLog As TextBox)
        _TXT_LOG = txtLog
    End Sub

    Dim _OldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If _OldLog <> LogDesc Then
            _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
        End If
    End Sub

    Public Sub WFStartProcess(ByVal ServiceDate As Date, ByVal StartTime As String, ByVal EndTime As String, ByVal lblTime As Label)
        Dim DateNow As DateTime = DateAdd(DateInterval.Day, -1, DateTime.Now)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim txt As String = ""
            txt = "Split,Calls_Offered,ASA,Avg_Talk,Avg_ACW,Calls_Aband,Pos_Staffed,%_Svc_Level,Avg_Talk_w/Hold,Calls_Offrd_II,%_Svc_Level_II,Backup_Calls,ACD_Calls,ABN_Calls1,Busy_Calls,Disc_Calls,ACC_Calls,ACD_Time,ACW_Time,ANS_Time,Hold_Time" & vbCrLf

            Dim shDt As DataTable = FunctionEng.GetActiveShop()
            Dim ShopCode As String = ""
            For Each shDr As DataRow In shDt.Rows
                If shDr("shop_abb") = "PK" Or shDr("shop_abb") = "LDP" Then
                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                    Application.DoEvents()

                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(shDr("id"))
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "WorkForceENG.WFStartProcess")
                    If shDr("shop_abb") = "PK" Then
                        ShopCode = "PK"
                    Else
                        ShopCode = "LDP"
                    End If
                    txt += GenerateTextFile(ServiceDate, StartTime, EndTime, ShopCode, shTrans, lblTime)
                End If
            Next

            If Directory.Exists("D:\POC_WFM") = False Then
                Directory.CreateDirectory("D:\POC_WFM")
            End If
            Dim FileName As String = "QIS_WFM_" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "_" & StartTime.Replace(":", "")
            CreateTextfile("D:\POC_WFM\" & FileName & ".txt", txt)

            trans.CommitTransaction()
        End If
        'End If
    End Sub

    Private Function GenerateTextFile(ByVal ServiceDate As Date, ByVal StartTime As String, ByVal EndTime As String, ByVal ShopCode As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal lblTime As Label) As String
        Dim txt As String = ""
        Dim ItemCode As String = ""
        Dim shItem As New DataTable
        Dim i As Int32 = 0
        shItem = FunctionEng.GetShopService(shTrans)
        If shItem.Rows.Count > 0 Then
            For Each stDr As DataRow In shItem.Rows
                If ShopCode = "PK" Then
                    Select Case CInt(shItem.Rows(i).Item("id"))
                        Case 1
                            ItemCode = "11"
                        Case 2
                            ItemCode = "12"
                        Case 3
                            ItemCode = "13"
                        Case 4
                            ItemCode = "14"
                    End Select
                Else
                    Select Case CInt(shItem.Rows(i).Item("id"))
                        Case 1
                            ItemCode = "15"
                        Case 2
                            ItemCode = "16"
                        Case 3
                            ItemCode = "17"
                        Case 4
                            ItemCode = "18"
                    End Select
                End If

                'If Convert.IsDBNull(stDr("workfource_display_id")) = False Then
                Dim dt_Data As New DataTable
                Dim dt_Login As New DataTable
                Dim ServiceConfig As String = "18" & ItemCode
                Dim Regis As String = "0"
                Dim Serve As String = "0"
                Dim AWT As String = ":00"
                Dim AHT As String = ":00"
                Dim ACT As String = ":00"
                Dim Abandon As String = "0"
                Dim ALogin As String = ":00"
                Dim Per_WT_within_kpi As String = "0.00"
                Dim Cus_within_kpi As String = "0"
                Dim SHT As String = ":00"
                Dim SWT As String = ":00"
                dt_Data = QueryDataBy30Minute(ServiceDate, StartTime, EndTime, ShopCode, stDr("id"), shTrans)
                dt_Login = QueryLoginBy30Minute(ServiceDate, StartTime, EndTime, ShopCode, stDr("id"), ItemCode, shTrans)

                If dt_Data.Rows.Count > 0 Then
                    ServiceConfig = "18" & ItemCode
                    Regis = dt_Data.Rows(0).Item("regis").ToString
                    Serve = dt_Data.Rows(0).Item("serve").ToString
                    AWT = GetFormatTimeFromSec(dt_Data.Rows(0).Item("awt"))
                    AHT = GetFormatTimeFromSec(dt_Data.Rows(0).Item("aht"))
                    ACT = GetFormatTimeFromSec(dt_Data.Rows(0).Item("act"))
                    Abandon = dt_Data.Rows(0).Item("aban")
                    Per_WT_within_kpi = dt_Data.Rows(0).Item("per_wt_with_kpi").ToString.Substring(0, dt_Data.Rows(0).Item("per_wt_with_kpi").ToString.Length - 2)
                    Cus_within_kpi = dt_Data.Rows(0).Item("wt_with_kpi").ToString
                    SHT = GetFormatTimeFromSec(dt_Data.Rows(0).Item("ht"))
                    SWT = GetFormatTimeFromSec(dt_Data.Rows(0).Item("wt"))
                End If

                If dt_Login.Rows.Count > 0 Then
                    ALogin = GetFormatTimeFromSec(dt_Login.Rows(0).Item("TT"))
                End If
                txt += ServiceConfig & "," & Regis & "," & AWT & "," & AHT & "," & ACT & "," & Abandon & "," & ALogin & "," & Per_WT_within_kpi & "," & AHT & "," & Regis & "," & Per_WT_within_kpi & "," & "0" & "," & Serve & "," & "0" & "," & "0" & "," & "0" & "," & Cus_within_kpi & "," & SHT & "," & "0" & "," & SWT & "," & "0" & vbCrLf
                'End If

                i += 1

                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                Application.DoEvents()
            Next
            shItem.Dispose()
            shItem = Nothing
        End If

        Return txt
    End Function

    Private Sub CreateTextfile(ByVal patch As String, ByVal ValTxt As String)
        'Try
        '    Dim oWrite As StreamWriter
        '    oWrite = File.CreateText(patch)
        '    oWrite.WriteLine(ValTxt)
        '    oWrite.Close()
        'Catch ex As Exception : End Try
        Try
            If ValTxt = "" Then
                ValTxt = " "
            End If

            Dim txtFile As New StreamWriter(patch, False, System.Text.Encoding.UTF8, ValTxt.Length)
            With txtFile
                .Write(ValTxt)
                .Close()
            End With
        Catch ex As Exception : End Try
    End Sub

    Private Function QueryDataBy30Minute(ByVal ServiceDate As Date, ByVal StTime As String, ByVal EdTime As String, ByVal ShopServiceID As String, ByVal ServiceId As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        Dim sql As String = ""
        Dim dt As New DataTable

        sql += " declare @ST as datetime; select @ST = '" & StTime & "';" & vbNewLine
        sql += " declare @ET as datetime; select @ET = '" & EdTime & "';" & vbNewLine
        'sql += " declare @SUMINTERVAL as int; select @SUMINTERVAL = 30;" & vbNewLine   'คำนวณทุกๆ 30 นาที
        sql += " select  isnull(SUM(regis),0) as regis " & vbNewLine
        sql += " ,isnull(SUM(serve),0) as serve " & vbNewLine
        sql += " ,isnull(sum(cancel),0) + isnull(sum(misscall),0) + isnull(sum(notcall),0) + isnull(sum(notcon),0) + isnull(sum(notend),0)  as aban " & vbNewLine
        sql += " ,isnull(SUM(case when status = 3 then wt else 0 end),0) as wt " & vbNewLine
        sql += " ,isnull(SUM(case when status = 3 then ht else 0 end),0) as ht " & vbNewLine
        sql += " ,isnull(SUM(case when status = 3 then ct else 0 end),0) as ct " & vbNewLine
        sql += " ,isnull(round(SUM(case when status = 3 then wt else 0 end)/SUM(serve),0),0) as awt " & vbNewLine
        sql += " ,isnull(round(SUM(case when status = 3 then ht else 0 end)/SUM(serve),0),0) as aht " & vbNewLine
        sql += " ,isnull(round(SUM(case when status = 3 then ct else 0 end)/SUM(serve),0),0) as act " & vbNewLine
        sql += " ,isnull(sum(case when status = 3 then case when wt <= std_wt then 1 else 0 end else 0 end),0) as wt_with_kpi " & vbNewLine
        sql += " ,isnull(sum(case when status = 3 then case when wt > std_wt then 1 else 0 end else 0 end),0) as wt_over_kpi " & vbNewLine
        sql += " ,isnull(sum(case when status = 3 then case when ht <= std_ht then 1 else 0 end else 0 end),0) as ht_with_kpi " & vbNewLine
        sql += " ,isnull(sum(case when status = 3 then case when ht > std_ht then 1 else 0 end else 0 end),0) as ht_over_kpi " & vbNewLine
        sql += " ,isnull(cast(round((sum(case when status = 3 then case when wt <= std_wt then 1 else 0 end else 0 end)/cast(SUM(regis) as money)) * 100,2) as money),0) as per_wt_with_kpi " & vbNewLine
        sql += " ,isnull(100 - cast(round((sum(case when status = 3 then case when wt <= std_wt then 1 else 0 end else 0 end)/cast(SUM(regis) as money)) * 100,2) as money),0) as per_wt_over_kpi " & vbNewLine
        sql += " ,isnull(cast(round((sum(case when status = 3 then case when ht <= std_ht then 1 else 0 end else 0 end)/cast(SUM(serve) as money)) * 100,2) as money),0) as per_ht_with_kpi" & vbNewLine
        sql += " ,isnull(100 - cast(round((sum(case when status = 3 then case when ht <= std_ht then 1 else 0 end else 0 end)/cast(SUM(serve) as money)) * 100,2) as money),0) as per_ht_over_kpi" & vbNewLine
        sql += " from VW_Report " & vbNewLine
        sql += " where YY = " & ServiceDate.Year & " and MM = " & ServiceDate.Month & " and DD = " & ServiceDate.Day & vbNewLine
        sql += " and [time] >= @ST and [time] < @ET and item_id = " & ServiceId & vbNewLine

        dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        Return dt
    End Function

    Private Function QueryLoginBy30Minute(ByVal ServiceDate As Date, ByVal StTime As String, ByVal EdTime As String, ByVal ShopServiceID As String, ByVal ServiceId As String, ByVal WFDisplayID As Int16, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        Dim sql As String = ""
        Dim dt As New DataTable

        Dim DD As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        sql += " declare @Date as varchar(10); select @Date = '" & DD & "';"
        sql += " declare @ST as varchar(10); select @ST = '" & StTime & ":00" & "';"
        sql += " declare @ET as varchar(10); select @ET = '" & EdTime & ":00" & "';"
        sql += " select isnull(AVG(TT),0) as TT from ("
        sql += " Select user_id"
        sql += " ,min(ST) as ST,ET"
        sql += " ,case when min(ST) <= @ST and ET >= @ET then 1800  "
        sql += " when ET > @ST and min(ST) < @ST then DATEDIFF(SECOND,@ST,ET) "
        sql += " when min(ST) < @ET and ET > @ET then DATEDIFF(SECOND,min(ST),@ET) "
        sql += " else 0 end as TT"
        sql += " from"
        sql += " ("
        sql += " select user_id,reason_id,productive,CONVERT(varchar(8),service_date,114) as ST,"
        sql += " ("
        sql += " Select min(Convert(varchar(8), service_date, 114))"
        sql += " from TB_LOG_LOGIN "
        sql += " where CONVERT(varchar(10),service_date,112) = @Date and reason_id = 0 and action = 2"
        sql += " and user_id = Main.user_id and service_date > Main.service_date"
        sql += " ) as ET"
        sql += " from TB_LOG_LOGIN as Main "
        sql += " where CONVERT(varchar(10),service_date,112) = @Date and action = 1 and reason_id = 0"
        sql += " ) as Logout "
        sql += " where ET Is Not null"
        sql += " group by user_id,ET"
        sql += " ) AS TB where TT > 0 and user_id in (select user_id from TB_USER_SERVICE_SCHEDULE where CONVERT(varchar(10),service_date,112) = @Date and priority = 1 and item_id = " & ServiceId & ")"
        dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        Return dt
    End Function

    Public Function GetFormatTimeFromSec(ByVal TimeSec As Integer) As String
        'แปลงเวลาจากวินาทีไปเป็น HH:mm:ss
        Dim tHour As Integer = 0
        Dim tMin As Integer = 0
        Dim tSec As Integer = 0

        If TimeSec >= 3600 Then
            tHour = Math.Floor(TimeSec / 3600) 'ชม.
            tMin = Math.Floor((TimeSec - (tHour * 3600)) / 60) ' นาที
            tSec = (TimeSec - (tHour * 3600)) Mod 60
        Else
            tMin = Math.Floor(TimeSec / 60)
            tSec = TimeSec Mod 60
        End If

        If TimeSec >= 3600 Then
            Return tHour.ToString.PadLeft(2, "0") & ":" & tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
        ElseIf TimeSec < 60 Then
            Return ":" & tSec.ToString.PadLeft(2, "0")
        Else
            Return tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
        End If
    End Function


End Class
