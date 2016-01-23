Imports System.IO
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Engine.Common
Imports System.Data
Imports CenLinqDB.Common.Utilities

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class FaultManagementService
     Inherits System.Web.Services.WebService

    '<WebMethod()> _
    'Public Function HelloWorld() As String
    '    Return "Hello World"
    'End Function

    <WebMethod()> _
    Public Function SendFileToDC(ByVal FileByte() As Byte, ByVal FileName As String, ByVal MachineName As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim fs As FileStream
            If File.Exists(FunctionEng.GetFaultMngUploadPath & FileName) = True Then
                File.Delete(FunctionEng.GetFaultMngUploadPath & FileName)
            End If

            fs = New FileStream(FunctionEng.GetFaultMngUploadPath & FileName, FileMode.CreateNew)
            fs.Write(FileByte, 0, FileByte.Length)
            fs.Close()
            ret = True
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

    <WebMethod()> _
    Public Function LoadFileFromDC(ByVal FileName As String) As Byte()
        Dim FileByte() As Byte = Nothing
        If File.Exists(FunctionEng.GetFaultMngUploadPath & FileName) = True Then
            FileByte = File.ReadAllBytes(FunctionEng.GetFaultMngUploadPath & FileName)
        End If
        Return FileByte
    End Function

    <WebMethod()> _
    Public Function SendMonitorFileToDC(ByVal FileByte() As Byte, ByVal FileName As String, ByVal MachineName As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim fs As FileStream
            If File.Exists(FunctionEng.GetFaultMngMonitorPath & FileName) = True Then
                File.Delete(FunctionEng.GetFaultMngMonitorPath & FileName)
            End If

            fs = New FileStream(FunctionEng.GetFaultMngMonitorPath & FileName, FileMode.CreateNew)
            fs.Write(FileByte, 0, FileByte.Length)
            fs.Close()
            ret = True
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

    <WebMethod()> _
    Public Function LoadMonitorFileFromDC(ByVal ServerName As String) As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("FileName")
        ret.Columns.Add("FileByte", GetType(Byte()))

        For Each f As String In Directory.GetFiles(FunctionEng.GetFaultMngMonitorPath, ServerName & "*.xml")
            If File.Exists(f) = True Then
                Try
                    Dim fInfo As New FileInfo(f)
                    Dim FileByte() As Byte = Nothing
                    FileByte = File.ReadAllBytes(f)

                    Dim dr As DataRow = ret.NewRow
                    dr("FileName") = fInfo.Name
                    dr("FileByte") = FileByte
                    ret.Rows.Add(dr)
                    fInfo = Nothing
                Catch ex As Exception
                End Try
            End If
        Next

        ret.TableName = "LoadMonitorFileFromDC"
        Return ret
    End Function

    <WebMethod()> _
    Public Function SendConfigFileToDC(ByVal FileByte() As Byte, ByVal FileName As String, ByVal MachineName As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim fs As FileStream
            If File.Exists(FunctionEng.GetFaultMngConfigPath & FileName) = True Then
                File.Delete(FunctionEng.GetFaultMngConfigPath & FileName)
            End If

            fs = New FileStream(FunctionEng.GetFaultMngConfigPath & FileName, FileMode.CreateNew)
            fs.Write(FileByte, 0, FileByte.Length)
            fs.Close()
            ret = True
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

    <WebMethod()> _
    Public Function LoadConfigFileFromDC(ByVal ServerName As String) As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("FileName")
        ret.Columns.Add("FileByte", GetType(Byte()))

        For Each f As String In Directory.GetFiles(FunctionEng.GetFaultMngConfigPath, ServerName & "*.xml")
            If File.Exists(f) = True Then
                Dim fInfo As New FileInfo(f)
                Dim FileByte() As Byte = Nothing
                FileByte = File.ReadAllBytes(f)

                Dim dr As DataRow = ret.NewRow
                dr("FileName") = fInfo.Name
                dr("FileByte") = FileByte
                ret.Rows.Add(dr)

                fInfo = Nothing
            End If
        Next
        ret.TableName = "LoadConfigFileFromDC"
        Return ret
    End Function

    <WebMethod()> _
    Public Function DeleteConfigFile(ByVal XMLConfigFileName As String, ByVal RepQty As Integer) As Boolean
        Dim ret As Boolean = False
        Try
            File.Delete(FunctionEng.GetFaultMngConfigPath & XMLConfigFileName)
            ret = True
        Catch ex As Exception
            System.Threading.Thread.Sleep(1000)
            If RepQty <= 10 Then
                ret = DeleteConfigFile(XMLConfigFileName, RepQty + 1)
            End If
        End Try

        Return ret
    End Function

    <WebMethod()> _
    Public Function GetAlarmURL() As String
        Dim ret As String = ""
        ret = FunctionEng.GetQisDBConfig("SNMP_ALARM_URL1") & "###" & FunctionEng.GetQisDBConfig("SNMP_ALARM_URL2")
        Return ret
    End Function

    <WebMethod()> _
    Public Function SendAlarm(ByVal SysLocation As String, ByVal HostIP As String, ByVal HostName As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal Desc As String, ByVal FlagClear As String, ByVal AlarmMethod As String) As Boolean
        Dim ret As Boolean = CreateAlarmLog(SysLocation, HostIP, HostName, AlarmType, AlarmName, Severity, AlarmValue, Desc, FlagClear, AlarmMethod)
        Return ret
    End Function

    Private Function CreateAlarmLog(ByVal SysLocation As String, ByVal HostIP As String, ByVal HostName As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal Desc As String, ByVal FlagClear As String, ByVal AlarmMethod As String) As Boolean
        Dim ret As Boolean = False

        Dim fmDir As String = "D:\FaultManagementAlarmLog\"
        If Directory.Exists(fmDir) = False Then
            Directory.CreateDirectory(fmDir)
        End If
        Try
            Dim TxtLog As String = ""
            TxtLog += "SysLocation=" & SysLocation & vbNewLine
            TxtLog += "HostIP=" & HostIP & vbNewLine
            TxtLog += "HostName=" & HostName & vbNewLine
            TxtLog += "AlarmType=" & AlarmType & vbNewLine
            TxtLog += "AlarmName=" & AlarmName & vbNewLine
            TxtLog += "Severity=" & Severity & vbNewLine
            TxtLog += "AlarmValue=" & AlarmValue & vbNewLine
            TxtLog += "Desc=" & Desc & vbNewLine
            TxtLog += "FlagClear=" & FlagClear & vbNewLine
            TxtLog += "AlarmMethod=" & AlarmMethod

            Dim FileName As String = "AlarmLog_" & DateTime.Now.ToString("yyyyMMdd_HH_mm_ss_fff") & ".txt"
            Dim objWriter As New System.IO.StreamWriter(fmDir & FileName, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & vbNewLine & TxtLog & vbNewLine)
            objWriter.Close()
            ret = True
        Catch ex As Exception
            ret = False
        End Try

        Return ret
    End Function

    <WebMethod()> _
    Public Function GetAlarmByServerName(ByVal ServerName As String) As DataTable
        Dim sql As String = "select * from TB_ALARM_WAITING_CLEAR where ServerName='" & ServerName & "' and FlagAlarm = 'Alarm' order by UpdateDate desc"
        Dim dt As New DataTable
        dt = QisFaultDB.GetDataTable(sql, Server.MapPath("Config.ini"))
        dt.TableName = "GetAlarmByServerName"
        Return dt
    End Function

    <WebMethod()> _
    Public Function InserAlarmWaitingClear(ByVal sql As String) As Long
        Dim ret As Long = 0
        If QisFaultDB.ExecuteNonQuery(sql, Server.MapPath("Config.ini")) > 0 Then

            ret = QisFaultDB.GetLastID("TB_ALARM_WAITING_CLEAR", Server.MapPath("Config.ini"))

            Dim AlmDT As New DataTable
            AlmDT = QisFaultDB.GetDataTable("select * from TB_ALARM_WAITING_CLEAR where id=" & ret, Server.MapPath("Config.ini"))
            If AlmDT.Rows.Count > 0 Then
                Dim dr As DataRow = AlmDT.Rows(0)
                Dim ServerName As String = dr("ServerName")
                Dim IPAddress As String = dr("HostIP")
                Dim AlarmActivity As String = dr("AlarmActivity")
                Dim Severity As String = dr("Severity")
                Dim AlarmValue As String = dr("AlarmValue")
                Dim AlarmMethod As String = dr("AlarmMethod")
                Dim AlarmDesc As String = dr("SpecificProblem")
                InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Alarm", AlarmDesc, AlarmDesc, ret)
            End If
        End If
        Return ret
    End Function

    Private Sub InsertAlarmLog(ByVal ServerName As String, ByVal HostIP As String, ByVal AlarmActivity As String, ByVal Severity As String, ByVal CurrentValue As String, ByVal AlarmMethod As String, ByVal FlagAlarm As String, ByVal AlarmDesc As String, ByVal SpecificPloblem As String, ByVal AlarmWaitingClearID As Integer)
        Dim sql As String = "insert into TB_ALARM_LOG (CreateDate,ServerName,HostIP,AlarmActivity,Severity,"
        sql += " CurrentValue,AlarmMethod,FlagAlarm,AlarmDesc,SpecificPloblem,AlarmWaitingClearID)"
        sql += " values(getdate(),'" & ServerName & "','" & HostIP & "','" & AlarmActivity & "','" & Severity & "',"
        sql += " '" & CurrentValue & "','" & AlarmMethod & "','" & FlagAlarm & "','" & AlarmDesc & "','" & SpecificPloblem & "','" & AlarmWaitingClearID & "')"

        QisFaultDB.ExecuteNonQuery(sql, Server.MapPath("Config.ini"))
    End Sub

    <WebMethod()> _
    Public Function UpdateAlarmWaitingClear(ByVal DCAlarmWaitingClearID As Long) As Boolean
        Dim sql As String = "update TB_ALARM_WAITING_CLEAR set AlarmQty = AlarmQty + 1,UpdateDate=getdate() where id=" & DCAlarmWaitingClearID
        Dim ret As Boolean = False
        If QisFaultDB.ExecuteNonQuery(sql, Server.MapPath("Config.ini")) > 0 Then
            Dim AlmDT As New DataTable
            AlmDT = QisFaultDB.GetDataTable("select * from TB_ALARM_WAITING_CLEAR where id=" & DCAlarmWaitingClearID, Server.MapPath("Config.ini"))
            If AlmDT.Rows.Count > 0 Then
                Dim dr As DataRow = AlmDT.Rows(0)
                Dim ServerName As String = dr("ServerName")
                Dim IPAddress As String = dr("HostIP")
                Dim AlarmActivity As String = dr("AlarmActivity")
                Dim Severity As String = dr("Severity")
                Dim AlarmValue As String = dr("AlarmValue")
                Dim AlarmMethod As String = dr("AlarmMethod")
                Dim AlarmDesc As String = dr("SpecificProblem")
                InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Alarm", AlarmDesc, AlarmDesc, DCAlarmWaitingClearID)
                ret = True
            End If
        End If
        Return ret
    End Function

    <WebMethod()> _
    Public Function SendClearAlarm(ByVal DCAlarmWaitingClearID As Long) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = "update TB_ALARM_WAITING_CLEAR set FlagAlarm='Clear', ClearDate=getdate() where id=" & DCAlarmWaitingClearID
        If QisFaultDB.ExecuteNonQuery(sql, Server.MapPath("Config.ini")) > 0 Then
            Dim AlmDT As New DataTable
            AlmDT = QisFaultDB.GetDataTable("select * from TB_ALARM_WAITING_CLEAR where id=" & DCAlarmWaitingClearID, Server.MapPath("Config.ini"))
            If AlmDT.Rows.Count > 0 Then
                Dim dr As DataRow = AlmDT.Rows(0)
                Dim ServerName As String = dr("ServerName")
                Dim IPAddress As String = dr("HostIP")
                Dim AlarmActivity As String = dr("AlarmActivity")
                Dim Severity As String = dr("Severity")
                Dim AlarmValue As String = dr("AlarmValue")
                Dim AlarmMethod As String = dr("AlarmMethod")
                Dim AlarmDesc As String = dr("SpecificProblem")
                InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Clear", AlarmDesc, "", DCAlarmWaitingClearID)
                ret = True
            End If
        End If
        Return ret
    End Function

    <WebMethod()> _
    Public Function AddConfigPort(ByVal ServerName As String, ByVal IPAddress As String, ByVal PortNumber As Integer, ByVal ChkSun As String, ByVal ChkMon As String, ByVal ChkTue As String, ByVal ChkWed As String, ByVal ChkThu As String, ByVal ChkFri As String, ByVal ChkSat As String, ByVal ChkAllDay As String, ByVal AlarmTimeFrom As String, ByVal AlarmTimeTo As String) As Boolean
        Dim sql As String = "insert into TB_CONFIG_PORT_LIST (HostIP,HostName,PortNumber,"
        sql += " AlarmSun, AlarmMon,AlarmTue,AlarmWed,AlarmThu,AlarmFri,AlarmSat,AllDayEvent,AlarmTimeFrom,AlarmTimeTo)"
        sql += " values('" & IPAddress & "','" & ServerName & "','" & PortNumber & "',"
        sql += " '" & ChkSun & "','" & ChkMon & "','" & ChkTue & "','" & ChkWed & "','" & ChkThu & "','" & ChkFri & "','" & ChkSat & "','" & ChkAllDay & "','" & AlarmTimeFrom & "','" & AlarmTimeTo & "')"
        Return (QisFaultDB.ExecuteNonQuery(sql, Server.MapPath("Config.ini")) > 0)
    End Function

    <WebMethod()> _
    Public Function GetConfigPortList(ByVal whText As String) As DataTable
        Dim sql As String = " select * from TB_CONFIG_PORT_LIST where 1=1 " & whText
        Dim dt As New DataTable
        dt = QisFaultDB.GetDataTable(sql, Server.MapPath("Config.ini"))
        dt.TableName = "GetConfigPortList"
        Return dt
    End Function

    <WebMethod()> _
    Public Function DeleteConfigPortList(ByVal id As Long) As Boolean
        Dim sql As String = "delete from TB_CONFIG_PORT_LIST where id=" & id
        Return (QisFaultDB.ExecuteNonQuery(sql, Server.MapPath("Config.ini")) > 0)
    End Function

    <WebMethod()> _
    Public Function SendImAlive(ByVal ServerName As String, ByVal ServerIP As String, ByVal cfgIntervalMinute As Integer, ByVal cfgStartTime As String, ByVal cfgEndTime As String, ByVal AliveTime As DateTime) As Boolean
        Dim ret As Boolean = False

        Dim NextAliveTime As DateTime = DateAdd(DateInterval.Minute, cfgIntervalMinute, AliveTime)
        Dim EndTime As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Split(cfgEndTime, ":")(0), Split(cfgEndTime, ":")(1), 0)

        Dim INIFileName As String = Server.MapPath("Config.ini")
        Dim sql As String = "select id from TB_IAM_ALIVE where hostip='" & ServerIP & "'"
        Dim dt As New DataTable
        dt = QisFaultDB.GetDataTable(sql, INIFileName)
        If dt.Rows.Count > 0 Then
            sql = "update TB_IAM_ALIVE"
            sql += " set ServerName='" & ServerName & "', HostIP='" & ServerIP & "',"
            sql += " cfg_monitor_time_start='" & cfgStartTime & "', cfg_monitor_time_end='" & cfgEndTime & "', cfg_interval='" & cfgIntervalMinute & "',"
            sql += " alive_time='" & AliveTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "' "
            If NextAliveTime <= EndTime Then
                sql += ", next_alive_time='" & NextAliveTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
            Else
                sql += ", next_alive_time=null"
            End If
            sql += " where id='" & dt.Rows(0)("id") & "'"
        Else
            sql = "insert into TB_IAM_ALIVE(CreateDate,ServerName,HostIP,"
            sql += " cfg_monitor_time_start, cfg_monitor_time_end,cfg_interval,"
            sql += " alive_time,next_alive_time)"
            sql += " values(getdate(),'" & ServerName & "','" & ServerIP & "',"
            sql += " '" & cfgStartTime & "','" & cfgEndTime & "','" & cfgIntervalMinute & "',"
            sql += " '" & AliveTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
            If NextAliveTime <= EndTime Then
                sql += ", '" & NextAliveTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
            Else
                sql += ", null"
            End If
            sql += ")"
        End If
        dt.Dispose()

        ret = (QisFaultDB.ExecuteNonQuery(sql, INIFileName) > 0)
        Return ret
    End Function

    '<WebMethod()> _
    'Public Function GetCurrentPath() As String
    '    Return Server.MapPath("Config.ini")
    'End Function
End Class

