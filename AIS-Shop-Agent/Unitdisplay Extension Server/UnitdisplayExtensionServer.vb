Imports System.Data
Imports System.Data.SqlClient
Imports Unitdisplay_Extension_Server.Org.Mentalis.Files
Imports System.IO

Module UnitdisplayExtensionServer

    Public INIFileName As String = Application.StartupPath & "\UnitdisplayExtensionServer.ini"
    Public INIErrorLog As String = Application.StartupPath & "\Logfile\" & Date.Now.Date.ToShortDateString & Date.Now.Date.ToShortTimeString & ".ini"
    Public Conn As New SqlConnection

    Public Function CheckConn(ByVal FunctionName As String, ByVal SQL As String) As Boolean
        Return Engine.Common.ShopConnectDBENG.AgentCheckDBConn(Conn, INIFileName, "UnitDisplayExtension", FunctionName, SQL)
    End Function

    Public Function getDataTable(ByVal FunctionName As String, ByVal SQL As String) As DataTable
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            If CheckConn(FunctionName, SQL) = True Then
                da = New SqlDataAdapter(SQL, Conn)
                da.Fill(dt)
                Conn.Close()
                Return dt
            End If
        Catch ex As Exception : End Try
        Return New DataTable
    End Function

    Public Sub executeSQL(ByVal FunctionName As String, ByVal SQL As String)
        If SQL.Trim <> "" Then
            Dim cmd As New SqlCommand(SQL)
            Try
                If CheckConn(FunctionName, SQL) = True Then
                    cmd.Connection = Conn
                    cmd.ExecuteNonQuery()
                    Conn.Close()
                End If
            Catch ex As Exception : End Try
        End If
    End Sub

    Public Sub WriteTextfile(ByVal ValTxt As String)
        Try
            If ValTxt = "" Then
                ValTxt = " "
            End If
            Dim txtFile As New StreamWriter(INIErrorLog, False, System.Text.Encoding.UTF8, ValTxt.Length)
            With txtFile
                .Write(ValTxt)
                .Close()
            End With
        Catch ex As Exception : End Try

    End Sub

#Region "UnitDisPlay"
    Public hCom As Int32
    Private Declare Function OpenCom Lib "leddisplay.dll" (ByVal pCom As Byte(), ByVal uBaudRate As Integer) As Int32

    Public Declare Function LED_Test Lib "leddisplay.dll" (ByVal hCom As Long, ByVal ID As Byte, ByVal reciveWaitTime As Long) As Long
    Public Declare Function CloseCom Lib "leddisplay.dll" (ByVal hCom As Long) As Long
    Private Declare Function LEDS_ShowASCIIString Lib "leddisplay.dll" (ByVal hCom As Long, _
    ByVal id As Byte, ByVal showMode As Byte, ByVal pStr As String, _
    ByVal pos As Integer, ByVal pFixStr As String, _
    ByVal reciveWaitTime As Long) As Long
    'Public Declare Function LEDM_EneterASCIIMode Lib "leddisplay.dll" (ByVal hCom As Long, _
    'ByVal id As Byte, ByVal reciveWaitTime As Long) As Long
    Public Declare Function LED_WriteValue Lib "leddisplay.dll" (ByVal hCom As Int32, ByVal ID As Byte, ByVal stringID As Byte, ByVal pCallNum As Byte(), ByVal pCounterNum As Byte(), ByVal reciveWaitTime As Int32) As Int32
    Public Declare Function LEDM_ShowASCIIInfo Lib "leddisplay.dll" (ByVal hCom As Long, _
        ByVal ID As Byte, ByVal pStr As Int32, ByVal strLength As Byte, ByVal reciveWaitTime As Long) As Long
    Public Declare Function LEDM_EneterASCIIMode Lib "leddisplay" (ByVal hCom As Long, ByVal ID As Byte, ByVal reciveWaitTime As Long) As Long
    Public Declare Function LEDS_ShowASCIIString Lib "leddisplay.dll" (ByVal hCom As Int32, ByVal ID As Byte, ByVal showMode As Byte, ByRef pStr As Byte, ByVal stringLength As Byte, ByVal pos As Int32, ByRef pFixStr As Byte, ByVal fixStrLength As Byte, ByVal reciveWaitTime As Int32) As Int32
    Public Declare Function CloseDevice Lib "leddisplay.dll" (ByVal hCom As Int32, ByVal ID As Byte, ByVal reciveTime As Int16) As Boolean


    Public Function OpenCom(ByVal com As String) As Int32
        Dim tmp() As Byte
        tmp = StringToUnicode(com)
        OpenCom = OpenCom(tmp, 9600)
    End Function

    Public Function StringToUnicode(ByVal v As String) As Byte()
        Dim tmp() As Byte
        tmp = System.Text.Encoding.Unicode.GetBytes(v)
        Return tmp
    End Function

    Function OpenComUnitDisplay() As Boolean
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        If Dir(INIFileName) <> "" Then
            If ini.ReadString("Com") <> "" Then
                hCom = OpenCom("COM" & ini.ReadString("Com"))
                If hCom > 0 Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function


    Sub ClearUnitDisplay(ByVal unitdisplay As String)
        Dim ID As Byte
        ID = CByte(unitdisplay)
        CloseDevice(hCom, ID, 0)
    End Sub

    Sub PauseUnitDisplay(ByVal unitdisplay As String)
        Dim stringID(0 To 3) As Byte
        stringID(0) = 3
        Dim ID As Byte
        ID = CByte(unitdisplay)
        Dim B_callNum() As Byte
        B_callNum = StringToUnicode("0001")
        Dim B_counterNum() As Byte
        B_counterNum = StringToUnicode("11")
        LED_WriteValue(hCom, ID, stringID(0), B_callNum, B_counterNum, 0)
    End Sub

    Sub ldlUnitDisplay(ByVal unitdisplay As String)
        Dim stringID(0 To 3) As Byte
        stringID(0) = 2
        Dim ID As Byte
        ID = CByte(unitdisplay)
        Dim B_callNum() As Byte
        B_callNum = StringToUnicode("0001")
        Dim B_counterNum() As Byte
        B_counterNum = StringToUnicode("11")
        LED_WriteValue(hCom, ID, stringID(0), B_callNum, B_counterNum, 0)
    End Sub

    Sub CallUnitDisplay(ByVal Queue As String, ByVal unitdisplay As String)
        Dim stringID(0 To 3) As Byte
        stringID(0) = 0
        Dim ID As Byte
        ID = CByte(unitdisplay)
        Dim B_callNum() As Byte
        B_callNum = StringToUnicode(Queue)
        Dim B_counterNum() As Byte
        B_counterNum = StringToUnicode("11")
        LED_WriteValue(hCom, ID, stringID(0), B_callNum, B_counterNum, 0)
    End Sub

    Sub ServeUnitDisplay(ByVal Queue As String, ByVal unitdisplay As String)
        Dim stringID(0 To 3) As Byte
        stringID(0) = 1
        Dim ID As Byte
        ID = CByte(unitdisplay)
        Dim B_callNum() As Byte
        B_callNum = StringToUnicode(Queue)
        Dim B_counterNum() As Byte
        B_counterNum = StringToUnicode("11")
        LED_WriteValue(hCom, ID, stringID(0), B_callNum, B_counterNum, 0)
    End Sub

    Sub ShowTexe(ByVal txt As String, ByVal unitdisplay As String)
        Dim ID As Byte
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        If Dir(INIFileName) <> "" Then
            Try
                ID = CByte(unitdisplay)
                Dim pStr() As Byte
                Dim pFixStr() As Byte
                pStr = System.Text.Encoding.ASCII.GetBytes(txt)
                pFixStr = System.Text.Encoding.ASCII.GetBytes("12")
                LEDS_ShowASCIIString(hCom, ID, 2, pStr(0), CByte(pStr.Length), 0, pFixStr(0), CByte(pFixStr.Length), 0)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

#End Region

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Public Sub UpdateVersion_Company()
        Dim v As String = getMyVersion()
        frmMain.Text = Replace(frmMain.Text, "[%V%]", v)
    End Sub

End Module

