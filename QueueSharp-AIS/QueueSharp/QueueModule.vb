Imports System.Data
Imports System.Data.SqlClient
Imports PrinterClassDll
Imports QueueSharp.Org.Mentalis.Files
Imports System.IO
Imports Join.LeftJoin
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Engine.Common.ShopConnectDBENG

Module QueueModule

    Public INIFileName As String = Application.StartupPath & "\QM\QueueSharp.ini"  'Parth ที่ใช้เก็บไฟล์ .ini
    'Public ConfigQMini As String = "D:\Scoresolutions\ConfigQM.ini"
    Public ApplicationName As String = "QSHARP"
    Public myUser As User
    Public CounterFunction As CounterFC
    Public LastService As Last
    Public BlinkCount As Int16 = 30   'จำนวนครั้งของการกระพริบ บน Unit Display เมื่อ Q ถูกเรียก
    Public MsgShow As Boolean = False
    Public FormServiceShow As Boolean
    Public AutoForceQueue As Boolean = False
    'Const MAIN_SERVER As String = "MAIN"


    Public Structure User
        Dim user_id As String
        Dim user_code As String
        Dim username As String
        Dim fulllname As String
        Dim counter_id As String
        Dim counter_name As String
        Dim item_id As String
        Dim ip_address As String
        Dim assign_appointment As String
    End Structure

    Public Structure Last
        Dim LastQueue As String
        Dim LastService As String
        Dim LastRoom As String
    End Structure

    Public Structure CounterFC
        Dim Swap As Int32
        Dim Beep As Int32
        Dim QuickService As Int32
        Dim ReturnCase As Int32
        Dim SpeedLane As Int32
    End Structure

    Declare Function GetUserName Lib "advapi32.dll" Alias _
      "GetUserNameA" (ByVal lpBuffer As String, _
      ByRef nSize As Integer) As Integer

    Public Function GetUserName() As String
        Dim iReturn As Integer
        Dim userName As String
        userName = New String(CChar(" "), 50)
        iReturn = GetUserName(userName, 50)
        GetUserName = userName.Substring(0, userName.IndexOf(Chr(0)))
    End Function

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

    Public Function StringToUnicode(ByVal v As String) As Byte()
        Dim tmp() As Byte
        tmp = System.Text.Encoding.Unicode.GetBytes(v)
        Return tmp
    End Function

    Sub ClearUnitDisplay(ByVal Close As Boolean, Optional ByVal _Trans As SqlTransaction = Nothing)
        Dim UnitdisplayID As String = ""
        UnitdisplayID = GetUnitdisplayID(_Trans)
        If UnitdisplayID = "" Then Exit Sub
        Dim sql As String = ""
        'Dim ID As Int32
        'ID = FindID("TB_UNITDISPLAY")
        If Close = True Then
            sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','',4,1)"
            executeSQL(sql, _Trans, True)
        Else
            sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','',2,1)"
            executeSQL(sql, _Trans, True)
        End If
    End Sub

    Sub PauseUnitDisplay()
        Dim UnitdisplayID As String = ""
        UnitdisplayID = GetUnitdisplayID()
        If UnitdisplayID = "" Then Exit Sub
        Dim sql As String = ""
        'Dim ID As Int32
        'ID = FindID("TB_UNITDISPLAY")
        sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
        sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','',3,1)"
        executeSQL(sql)
    End Sub

    Sub CallUnitDisplay(ByVal Queue As String, Optional ByVal _Trans As SqlTransaction = Nothing)
        Dim UnitdisplayID As String = ""
        UnitdisplayID = GetUnitdisplayID(_Trans)
        If UnitdisplayID = "" Then Exit Sub
        Dim sql As String = ""
        'Dim ID As Int32
        'ID = FindID("TB_UNITDISPLAY")
        sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
        sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','" & Queue & "','',0,1)"
        executeSQL(sql, _Trans, True)
    End Sub

    Public Sub CallSpeaker(ByVal QueueNumber As String, ByVal sTrans As SqlTransaction)
        Dim dt As New DataTable
        Dim sql As String = ""
        sql = "select queue_no from TB_speaker where datediff(d,getdate(),call_date)=0 and queue_no ='" & FixDB(QueueNumber) & "' and counter_id ='" & FixDB(myUser.counter_id) & "' and status = '0'"
        dt = getDataTable(sql, sTrans)
        If dt.Rows.Count = 0 Then
            sql = "insert into TB_speaker(id,queue_no, counter_id, counter_name, call_date, status, nationality) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_speaker),'" & QueueNumber & "','" & FixDB(myUser.counter_id) & "','" & FixDB(myUser.counter_name) & "',getdate(),'0','THAI')"
            executeSQL(sql, sTrans, True)
        End If
        dt.Dispose()
    End Sub

    Sub ServeUnitDisplay(ByVal Queue As String)
        Dim UnitdisplayID As String = ""
        UnitdisplayID = GetUnitdisplayID()
        If UnitdisplayID = "" Then Exit Sub
        Dim sql As String = ""
        If CreateTransaction("QueueModule_ServeUnitDisplay") = True Then
            sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
            sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','" & Queue & "','',1,1)"
            If executeSQL(sql, shTrans, True) = True Then
                CommitTransaction()
            Else
                RollbackTransaction()
            End If
        End If
    End Sub

    Sub ShowUserUnitDisplay(ByVal txt As String)
        Dim UnitdisplayID As String = ""
        UnitdisplayID = GetUnitdisplayID()
        If UnitdisplayID = "" Then Exit Sub
        Dim sql As String = ""
        sql = "insert into TB_UNITDISPLAY(id,counter_no,queue_no,txt,action,status_cls) "
        sql += " values((select isnull(MAX(id) + 1,1) as id from TB_UNITDISPLAY),'" & UnitdisplayID & "','','" & txt & "',5,1)"

        If CreateTransaction("QueueModule_ShowUserUnitDisplay") = True Then
            If executeSQL(sql, shTrans, True) = True Then
                CommitTransaction()
            Else
                RollbackTransaction()
            End If
        End If
    End Sub

    Function GetUnitdisplayID(Optional ByVal _Trans As SqlTransaction = Nothing) As String
        Dim ret As String = ""
        Dim sql As String = ""
        Dim dt As New DataTable
        If myUser.counter_id <> "" Then
            sql = "select unitdisplay from tb_counter where id = " & myUser.counter_id & " and active_status = 1"
            dt = getDataTable(sql, _Trans)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("unitdisplay").ToString <> "0" Then
                    ret = dt.Rows(0).Item("unitdisplay").ToString
                End If
            End If
        End If
        dt.Dispose()

        Return ret
    End Function
#End Region
#Region "Database Connection"
    Public Conn As New SqlConnection
    Public shTrans As SqlTransaction

    Public Function CheckConn(ByVal FunctionName As String, ByVal Logout As Boolean, ByVal SQL As String) As Boolean
        If CheckDBConn(Conn, INIFileName, "QueueSharp", FunctionName, SQL) = False Then
            If Logout = True Then
                LogoutIfErrorConnection()
            End If
            Return False
        End If
        Return True
    End Function


    ' <summary>
    ' Commit the SQL database transaction and close the connection to the database.
    ' </summary>
    ' <returns>Return true when process operation successfully, otherwise false.</returns>
    Public Function CommitTransaction() As Boolean
        Dim ret As Boolean = True
        Try
            If shTrans IsNot Nothing Then shTrans.Commit()
            If Conn IsNot Nothing Then Conn.Close()

        Catch ex As SqlException
            ret = False
            '_error = SqlDB.GetExceptionMessage(ex)
        Catch
            ret = False
            '_error = errorCommitTransaction
        End Try

        Return ret
    End Function


    ' <summary>
    ' Rolls back a transaction from a pending state and close the connection to the database.
    ' </summary>
    ' <returns>Return true when process operation successfully, otherwise false.</returns>
    Public Function RollbackTransaction() As Boolean
        Dim ret As Boolean = True
        Try
            If shTrans IsNot Nothing Then shTrans.Rollback()
            If Conn IsNot Nothing Then Conn.Close()
        Catch ex As SqlException
            ret = False
            '_error = SqlDB.GetExceptionMessage(ex)
        Catch
            ret = False
            '_error = errorRollbackTransaction
        End Try

        Return ret
    End Function

    Public Function CreateLogInTrans() As Boolean
        Dim ret As Boolean = False
        Try
            If CheckConCreateTrans(Conn, INIFileName) = True Then
                shTrans = Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted)
                ret = True
            Else
                shTrans = Nothing
            End If
        Catch ex As Exception

        End Try
        Return ret
    End Function

    Public Function CreateTransaction(ByVal FunctionName As String) As Boolean
        Dim ret As Boolean = True
        Try
            If CheckDBConn(Conn, INIFileName, "QueueSharp", FunctionName, "") = True Then
                shTrans = Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted)
            Else
                shTrans = Nothing
                ret = False
            End If
        Catch ex As SqlException
            ret = False
            '_error = ex.Message
        Catch ex As Exception
            ret = False
            '_error = ex.Message 'SqlDB.GetExceptionMessage(ex)
        End Try
        Return ret
    End Function

#End Region


#Region "Convert Data"

    Public Function FixDB(ByVal TXT As String) As String 'Replace text จาก ' เป็น ''
        If IsDBNull(TXT) = True Then
            Return ""
        ElseIf TXT = Nothing Then
            Return ""
        Else
            Return Trim(TXT.ToString.Replace("'", "''"))
        End If
    End Function

    Function FixMoney(ByVal MyMoney As Double) As String 'Convert ตัวเลข เป็น เลขทศนิยม 2 ตำแหน่ง
        Dim Money As String = ""
        Money = Format(MyMoney, "#,###.##")
        Return Money
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
    Public Function FixDateTime(ByVal DateTimeIn As DateTime) As String 'Convert จาก DataRow Datetime ให้เป็น yyyy-MM-dd HH:mm:ss.fff  ใช้สำหรับ Insert หรือ Update ลงฐานข้อมูล
        Dim ret As String = ""
        ret = "'" & DateTimeIn.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
        Return ret
    End Function

#End Region

#Region "Manage Query"

    Public Function getDataTable(ByVal SQL As String) As DataTable
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            If CheckConn("QueueModule_GetDatatable", True, SQL) = True Then
                da = New SqlDataAdapter(SQL, Conn)
                da.Fill(dt)
                Conn.Close()
                Return dt
            Else
                SaveQueryErrorLog("Database connection error !!!", SQL)
                'showFormError("Database connection error !!!", False)
            End If
        Catch ex As SqlException
            If CheckConn("QueueModule_GetDatatable", True, SQL) = True Then
                Try
                    da = New SqlDataAdapter(SQL, Conn)
                    da.Fill(dt)
                    Conn.Close()
                    Return dt
                Catch ex1 As SqlException
                    SaveQueryErrorLog(ex1.Message, SQL)
                    'showFormError(ex1.Message & Environment.NewLine & Environment.NewLine & SQL, False)
                    Return dt
                End Try
            Else
                SaveQueryErrorLog(ex.Message, SQL)
                'showFormError(ex.Message & Environment.NewLine & Environment.NewLine & SQL, False)
                Return dt
            End If
        End Try
        Return New DataTable
    End Function

    Public Function executeSQL(ByVal SQL As String, Optional ByVal forceExit As Boolean = True) As Boolean
        Dim ret As Boolean = False
        If SQL.Trim <> "" Then
            Dim cmd As New SqlCommand(SQL)
            Try
                If CheckConn("QueueModule_executeSQL", True, SQL) = True Then
                    cmd.Connection = Conn
                    cmd.ExecuteNonQuery()
                    Conn.Close()
                    ret = True
                Else
                    'showFormError("Database connection error !!!", False)
                    SaveQueryErrorLog("Database connection error !!!", SQL)
                    ret = False
                End If
            Catch ex As Exception
                SaveQueryErrorLog(ex.Message, SQL)
                ret = False
                If forceExit Then
                    'showFormError(ex.Message & Environment.NewLine & Environment.NewLine & SQL, False)
                End If
            End Try
        End If
        Return ret
    End Function

    Public Function getDataTable(ByVal SQL As String, ByVal shTrans As SqlTransaction) As DataTable
        Dim dt As New DataTable
        Try
            If shTrans IsNot Nothing Then
                dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(SQL, shTrans)
            Else
                dt = getDataTable(SQL)
            End If
            Return dt
        Catch ex As SqlException
            'showFormError(ex.Message & " Database connection error !!!", False)
            SaveQueryErrorLog("Database connection error !!!", SQL)
        End Try
        Return New DataTable
    End Function


    Public Function executeSQL(ByVal SQL As String, ByVal shTrans As SqlTransaction, ByVal forceExit As Boolean) As Boolean
        Dim ret As Boolean = False
        If SQL.Trim <> "" Then
            Try
                If shTrans IsNot Nothing Then
                    If ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(SQL, shTrans) > 0 Then
                        ret = True
                    End If
                Else
                    ret = executeSQL(SQL, forceExit)
                End If
            Catch ex As Exception
                SaveQueryErrorLog(ex.Message, SQL)
                If forceExit Then
                    'showFormError(ex.Message & Environment.NewLine & Environment.NewLine & SQL, False)
                End If
            End Try
        End If

        Return ret
    End Function
#End Region
#Region "All Error"

    Sub showFormError(ByVal Message As String, Optional ByVal forceExit As Boolean = True)
        If FormOpen(frmErrorMessage) = False Then
            frmErrorMessage.txtMassage.Text = Message
            If forceExit = False Then frmErrorMessage.btnClose.Text = "Close" Else frmErrorMessage.btnClose.Text = "Exit Queue Sharp™"
            frmErrorMessage.ShowDialog()
            frmErrorMessage.Dispose()
            If forceExit Then
                System.Environment.Exit(0)
            End If
        End If
    End Sub

#End Region

#Region "Version"
    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
        'Dim company As String = Application.CompanyName
    End Function

    Public Function getCompany() As String
        Dim company As String = Application.CompanyName
        If company.Trim = "" Then
            company = "None"
        End If
        Return company
    End Function

    Public Sub UpdateVersion_Company()
        Dim v As String = getMyVersion()
        Dim c As String = getCompany()
        frmMain.Text = Replace(frmMain.Text, "[%V%]", v)
        frmMain.Text = Replace(frmMain.Text, "[%C%]", c)
        frmMain.lblScore.Text = Replace(frmMain.lblScore.Text, "[%V%]", v)
        frmAboutBox.lblVersion.Text = Replace(frmAboutBox.lblVersion.Text, "[%V%]", v)

    End Sub

#End Region
#Region "Prin"
    Public Sub printTicket(ByVal QueueNo As String, ByVal CustomerID As String, ByVal CustomerType As String, Optional ByVal printerName As String = "BIXOLON SAMSUNG SRP-350plus")
        'On Error Resume Next
        'Dim ini As New IniReader(INIFileName)
        'Dim prn As New PrinterClassDll.Win32Print
        'If printerName = "" Then
        '    printerName = "BIXOLON SAMSUNG SRP-350plus"
        'End If
        'ini.Section = "SETTING"
        'prn.SetPrinterName(printerName) '(SimpleDecrypt(ini.ReadString("PrinterName")))
        'prn.SetDeviceFont(9.5, "FontControl", False, False)
        'prn.PrintText("x")
        'prn.PrintText("r")
        'prn.PrintImage("logo.gif")
        'prn.SetDeviceFont(9.5, "FontA1x1", True, True)
        ''prn.SetDeviceFont(9.5, "FontB2x1", True, True)
        ''prn.PrintText("โรงพยาบาล ทหารผ่านศึก")
        ''prn.SetDeviceFont(9.5, "FontA1x1", True, True)
        ''prn.PrintText(" ")
        'prn.PrintText("Date: " & Now.Date.Year & Now.Date.ToString("-MM-dd") & " Time: " & Now.ToString("hh:mm:ss tt"))
        'prn.SetDeviceFont(9.5, "FontA1x1", True, True)
        'prn.PrintText("VN # " & CustomerID)
        'prn.PrintText("--------------------------")
        ''prn.SetDeviceFont(9.5, "FontA2x2", True, True)
        ''prn.PrintText(" ")
        'prn.SetDeviceFont(18, "FontA1x1", True, True)
        'prn.PrintText("Queue # " & QueueNo)
        'prn.SetDeviceFont(6, "FontA1x1", True, True)
        'If QueueNo.Length > 0 Then
        '    prn.SetDeviceFont(16, "Code128", False, True)
        '    prn.PrintText(QueueNo & vbLf)
        '    prn.SetDeviceFont(9.5, "FontA2x2", True, True)
        '    prn.PrintText(" ")
        'End If
        'prn.EndDoc()

        Try
            Dim prn As New PrinterClassDll.Win32Print
            Dim ini As New IniReader(INIFileName)
            ini.Section = "SETTING"
            prn.SetPrinterName(printerName) '(SimpleDecrypt(ini.ReadString("PrinterName")))
            prn.SetDeviceFont(9.5, "FontControl", False, False)
            prn.PrintText("x")
            'prn.PrintText("r")
            prn.PrintImage("logo.gif")
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            'prn.PrintText(" ")
            prn.PrintText("Date: " & IIf(Now.Date.Year > 2500, Now.Date.Year - 543, Now.Date.Year) & Now.Date.ToString("-MM-dd") & " Time: " & Now.ToString("hh:mm:ss tt"))
            'prn.PrintText(" ")
            prn.SetDeviceFont(9.5, "FontA2x1", True, True)
            prn.PrintText("VN # " & CustomerID)
            prn.PrintText(CustomerType)
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            prn.PrintText(" ")
            prn.SetDeviceFont(9.5, "FontA2x2", True, True)
            prn.PrintText("Queue # " & QueueNo)
            prn.SetDeviceFont(6, "FontA1x1", True, True)
            prn.PrintText(" ")
            If QueueNo.Length > 0 Then
                prn.SetDeviceFont(50, "Free 3 of 9 Extended", False, True)
                'prn.SetDeviceFont(50, "Code128", False, True)
                prn.PrintText(QueueNo)
                'prn.PrintText(hn & vbLf)
                prn.SetDeviceFont(9.5, "FontA2x1", True, True)
                prn.PrintText(" ")
            End If

            prn.EndDoc()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#Region "suggestNext Service & Room"

    'หาบริการและห้องที่ลูกค้าจะต้องไปทำต่อ
    Public Function suggestNextRoom(ByVal CustomerID As String, ByVal CustomerTypeID As String) As String()
        Dim ret(4) As String
        'ret(0) = item_id
        'ret(1) = item_name
        'ret(2) = counter_id
        'ret(3) = counter_name
        Dim service(1) As String
        'service(0) = item_id
        'service(1) = item_name
        Dim counter(1) As String
        'counter(0) = counter_id
        'counter(1) = counter_name

        service = FindService(CustomerID)

        If service(0) <> "" Then
            counter = FindRoom(service(0), CustomerTypeID)
            If counter(0) = "" Then
                'ถ้าผิดปกติ หาห้องไม่ได้ 
                MessageBox.Show("บริการต่อไปของลูกค้า คือ " & service(1) & vbCrLf & "แต่ไม่มีช่องบริการที่รองรับกับการทำงานของประเภทลูกค้า" & vbCrLf & "กรุณากลับไปตรวจสอบการตั้งค่าช่องบริการของระบบ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ret
            End If
            ret(0) = service(0)
            ret(1) = service(1)
            ret(2) = counter(0)
            ret(3) = counter(1)
            Return ret
        Else
            'ถ้าผิดปกติ หาไม่เจอเลย 
            Return ret
        End If

        '********************************************************************
    End Function

    'หาบริการที่ลูกค้าจะต้องไปทำต่อ
    Function FindService(ByVal argCustomerID As String) As String()
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim service(3) As String
        sql = "exec SP_FindService '" & FixDB(argCustomerID) & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            service(0) = dt.Rows(0)("item_id").ToString
            service(1) = dt.Rows(0)("item_name").ToString

            Dim txtTime As String = ""
            If CInt(dt.Rows(0)("TotalTime")) = "1000000" Then
                txtTime = "-"
            Else
                txtTime = CStr((CInt(dt.Rows(0)("TotalTime")) / 60) + 1) & " Minute"
            End If
            service(2) = txtTime

            For i As Int32 = 0 To dt.Rows.Count - 1
                If service(3) = "" Then
                    service(3) = dt.Rows(i)("item_name").ToString
                Else
                    service(3) = service(3) & ", " & dt.Rows(i)("item_name").ToString
                End If
            Next

            Return service
        End If
        Return service
    End Function

    'หาห้องที่ลูกค้าจะต้องไปของบริการนั้นๆ
    Public Function FindRoom(ByVal ItemID As String, ByVal CustomerTypeID As String) As String()
        Dim Counter(1) As String
        Dim sql As String = ""
        Dim dt As New DataTable

        Dim RoomFifo As String = ""
        sql = "exec SP_FindRoomFIFO " & ItemID & "," & CustomerTypeID
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                If RoomFifo = "" Then
                    RoomFifo = dt.Rows(i)("counter_name").ToString
                Else
                    RoomFifo = RoomFifo + ", " + dt.Rows(i)("counter_name").ToString
                End If
            Next
            Counter(0) = "-1"
            Counter(1) = RoomFifo
            Return Counter
        Else
            Return Counter
        End If
    End Function

#End Region

    'เช็คข้อมูลซ้ำ
    Public Function CheckDuplicate(ByVal TableName As String, ByVal FieldName As String, ByVal Value_Duplicate As String, ByVal id As String) As Boolean
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select * from " & FixDB(TableName) & " where " & FixDB(FieldName) & " = '" & FixDB(Value_Duplicate) & "' and id <> '" & id & "' "
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            showFormError(ex.Message & Environment.NewLine & Environment.NewLine)
        End Try
        Return False
    End Function

    'หา rowID ของ Tabel
    Public Function FindID(ByVal TableName As String) As String
        Dim id As String = ""
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select isnull(MAX(id + 1),1) as id from " & FixDB(TableName)
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0).Item("id").ToString
            End If
            Return id
        Catch ex As Exception
            showFormError(ex.Message & Environment.NewLine & Environment.NewLine)
        End Try
        Return id
    End Function

    'หา rowID ของ Tabel
    Public Function FindID(ByVal TableName As String, ByVal _Trans As SqlTransaction) As String
        Dim id As String = ""
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select isnull(MAX(id + 1),1) as id from " & FixDB(TableName)
            dt = getDataTable(sql, _Trans)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0).Item("id").ToString
            End If
            Return id
        Catch ex As Exception
            showFormError(ex.Message & Environment.NewLine & Environment.NewLine)
        End Try
        Return id
    End Function

    'หาหมายเลขคิวใหม่ของลูกค้าประเภทนั้นๆ
    Function GenQueueNo(ByVal ItemId As String, ByVal CustomerTypeID As String) As String
        Dim QueueNo As String = ""
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "declare @Item as int; select @Item = " & ItemId & ";declare @Min as varChar(3); select @Min = (select min_queue from TB_CUSTOMERTYPE where id = " & CustomerTypeID & ");declare @Max as varChar(3); select @Max = (select max_queue from TB_CUSTOMERTYPE where id = " & CustomerTypeID & ");declare @Q as Char;select @Q = (select txt_queue from TB_ITEM where id = @Item); select MAX(queue_no) as queue_no,@Q as txt_queue,@Min as min_queue,@Max as max_queue from (select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and item_id = @Item and queue_no <> '' and convert(int,(right(queue_no,3))) between @Min and @Max and left(queue_no,1) = @Q group by queue_no) as xxx "
        dt = getDataTable(sql)
        If dt.Rows(0).Item("queue_no").ToString = "" Then
            Return dt.Rows(0).Item("txt_queue").ToString & dt.Rows(0).Item("min_queue").ToString.PadLeft(3, "0")
        Else
            Dim Q As Int32 = CInt(StringFromRight(dt.Rows(0).Item("queue_no").ToString, 3)) + 1
            Return dt.Rows(0).Item("txt_queue").ToString & Q.ToString.PadLeft(3, "0")
        End If
        Return QueueNo
    End Function

    Public Sub ChangeInputLang(Optional ByVal txt As String = "ENGL")
        Dim tmp As String = ""
        If Not InputLanguage.CurrentInputLanguage.Culture.EnglishName.ToUpper.StartsWith(txt) Then
            For i As Integer = 0 To InputLanguage.InstalledInputLanguages.Count - 1
                tmp = InputLanguage.InstalledInputLanguages.Item(i).Culture.EnglishName.ToString
                If tmp.ToUpper.StartsWith(txt) Then
                    InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages.Item(i)
                    Exit For
                End If
            Next
        End If
    End Sub

    'เช็คสถานะของลูกค้าว่าถูก Update ไปก่อนหน้านี้แล้วหรือยัง
    Function CheckCustomerStatus(ByVal QueueNo As String, ByVal CustomerID As String, ByVal Status As Integer, Optional ByVal shTrans As SqlTransaction = Nothing) As Boolean
        Dim sql As String = ""
        Dim dt As New DataTable
        '@QueueNo as int,
        '@CustomerID as varchar,
        '@Status as int
        sql = "exec SP_CheckCustomerStatus '" & QueueNo & "','" & CustomerID & "'," & Status
        dt = getDataTable(sql, shTrans)
        If dt.Rows.Count = 0 Then
            MessageBox.Show("The information has already been updated by the other user.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    ''Insert บริการของลูกค้าลงฐานข้อมูล
    'Sub InsertService(ByVal CustomerID As String, ByVal CustomerTypeID As Integer, ByVal ItemID As Integer, ByVal QueueNo As String, ByVal UserID As Int32, ByVal vDateNow As String)
    '    Dim sql As String = ""
    '    sql = "exec SP_InsertService '" & CustomerID & "'," & CustomerTypeID & "," & ItemID & ",'" & QueueNo & "'," & UserID
    '    executeSQL(sql)
    '    InsertLog(QueueNo, CustomerID, UserID, ItemID, 0, 1, "", Nothing, vDateNow)
    'End Sub

    Public Function ClearMainDisplay(ByVal CounterID As Integer, ByVal _Trans As SqlTransaction) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = "delete from TB_MAINDISPLAY "
        sql += " where DATEDIFF(D,GETDATE(),order_time)=0 "
        sql += " and counter_code=(select counter_code from tb_counter where id='" & CounterID & "')"
        executeSQL(sql, _Trans, True)
    End Function

    Private Function InsertMainDisplay(ByVal QueueNo As String, ByVal CounterID As Integer, ByVal StartTime As String, ByVal StatusID As Integer, ByVal _Trans As SqlTransaction) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        ClearMainDisplay(CounterID, _Trans)

        sql = "insert into TB_MAINDISPLAY(create_by,create_date,queue_no,counter_code,order_time,status_id) "
        sql += " values ('" & myUser.user_id & "',getdate(),'" & QueueNo & "',"
        sql += "(select counter_code from tb_counter where id='" & CounterID & "'),'" & StartTime & "','" & StatusID & "')"
        If _Trans Is Nothing Then
            If CreateTransaction("QueueModule_InsertMainDisplay") = True Then
                _Trans = shTrans
                ret = executeSQL(sql, _Trans, True)
                If ret = True Then
                    CommitTransaction()
                Else
                    RollbackTransaction()
                End If
            End If
        Else
            ret = executeSQL(sql, _Trans, True)
        End If

        Return ret
    End Function


    'Update Status ของลูกค้า
    Public Function UpdateQueueStatus(ByVal Status As Integer, ByVal CounterID As Integer, ByVal TimeHold As Integer, ByVal QueueNo As String, ByVal CustomerID As String, Optional ByVal ItemID As Int32 = 0, Optional ByVal Flag As String = "", Optional ByVal _Trans As SqlTransaction = Nothing) As Boolean
        Dim ret As Boolean = False
        Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)
        Dim sql As String = ""
        Dim DateNow As String = FindDateNow(_Trans).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))

        Select Case Status
            Case 1
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "' ,counter_id = 0,[user_id] = 0"
                sql += " ,call_time = null,start_time = Null,call_auto_force='0'"
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (2,4,6,8)"
            Case 2
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "', start_time = '" & DateNow & "',[user_id] = '" & myUser.user_id & "',counter_id = '" & CounterID & "'"
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (1,4)"
            Case 3
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "',end_time = '" & DateNow & "',[user_id] = '" & myUser.user_id & "',counter_id = '" & CounterID & "'"
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = '" & ItemID & "'"
            Case 4
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "',call_time = '" & DateNow & "',[user_id] = '" & myUser.user_id & "',counter_id = '" & CounterID & "' "
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (1)"
            Case 5
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "',end_time = '" & DateNow & "',[user_id] = '" & myUser.user_id & "',counter_id = '" & CounterID & "'"
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and item_id = '" & ItemID & "'"
            Case 6
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "',hold = DATEADD(minute," & TimeHold & ",GETDATE()),[user_id] = '" & myUser.user_id & "',counter_id = '" & CounterID & "'"
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (1,2,4,8) "
            Case 8
                sql = "update TB_COUNTER_QUEUE "
                sql += " set status = '" & Status & "',start_time = '" & DateNow & "',[user_id] = '" & myUser.user_id & "',hold = null "
                sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' and customer_id = '" & CustomerID & "' and status in (4)"
        End Select
        ret = executeSQL(sql, _Trans, True)

        If ret = True Then
            'If CurrDB = "MAIN" Then
            '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
            'End If

            If Status = 1 Then
                ClearMainDisplay(myUser.counter_id, _Trans)

                If myUser.item_id = 0 Then
                    sql = "update tb_counter_queue set flag = '' where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = 1"
                Else
                    sql = "update tb_counter_queue set flag = '' where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & myUser.item_id
                End If
                ret = executeSQL(sql, _Trans, True)
                If ret = True Then
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If
                End If
            ElseIf Status = 2 Or Status = 4 Then
                ret = InsertMainDisplay(QueueNo, myUser.counter_id, DateNow, Status, _Trans)
                If ret = True Then
                    If myUser.item_id = 0 Then
                        'Appointment
                        Dim dt As New DataTable
                        sql = "select top 1 TB_COUNTER_QUEUE.item_id from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = '" & Status & "' order by item_wait,item_time,item_order"
                        dt = getDataTable(sql, _Trans)
                        If dt.Rows.Count > 0 Then
                            sql = "update tb_counter_queue set flag = 'MainDisplay' where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & dt.Rows(0).Item("item_id").ToString
                            ret = executeSQL(sql, _Trans, True)
                            If ret = True Then
                                'If CurrDB = "MAIN" Then
                                '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                                'End If
                            End If
                        End If
                        dt.Dispose()
                    Else
                        sql = "update tb_counter_queue set flag = 'MainDisplay' where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and item_id = " & myUser.item_id
                        ret = executeSQL(sql, _Trans, True)
                        If ret = True Then
                            'If CurrDB = "MAIN" Then
                            '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                            'End If
                        End If
                    End If
                End If
            End If
        End If
        If ret = True Then
            InsertLog(QueueNo, CustomerID, myUser.user_id, 0, CounterID, Status, Flag, _Trans, "'" & DateNow & "'", CurrDB)
        End If

        Return ret
    End Function

    Public Function UpdateCallStatus(ByVal CounterID As Integer, ByVal QueueNo As String, ByVal MobileNo As String, ByVal DateNow As String, ByVal shTrans As SqlTransaction) As Boolean
        Dim ret As Boolean = False
        Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)

        Dim Status As Integer = 4
        Dim Sql As String = "update TB_COUNTER_QUEUE "
        Sql += " set status = " & Status & ",call_time = '" & DateNow & "',[user_id] = '" & myUser.user_id & "',counter_id = '" & CounterID & "' "
        If AutoForceQueue = True Then
            Sql += ",call_auto_force='1'"
        End If
        Sql += " where datediff(D,GETDATE(),service_date)=0 and queue_no = '" & QueueNo & "' "
        Sql += " and customer_id = '" & MobileNo & "' and status = 1"
        ret = executeSQL(Sql, shTrans, True)

        If ret = True Then
            ret = InsertMainDisplay(QueueNo, myUser.counter_id, DateNow, Status, shTrans)
            If ret = True Then
                'If CurrDB = "MAIN" Then
                '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(Sql, INIFileName)
                'End If

                InsertLog(QueueNo, MobileNo, myUser.user_id, 0, CounterID, Status, "", shTrans, "'" & DateNow & "'", CurrDB)
                If myUser.item_id = 0 Then
                    'Appointment 
                    Dim dt As New DataTable
                    Sql = "select top 1 q.item_id "
                    Sql += " from TB_COUNTER_QUEUE q "
                    Sql += " left join TB_ITEM t on q.item_id = t.id "
                    Sql += " where datediff(d,getdate(),q.service_date)=0 "
                    Sql += " and q.queue_no = '" & FixDB(QueueNo) & "' and q.customer_id = '" & FixDB(MobileNo) & "' "
                    Sql += " and q.status = " & Status
                    Sql += " order by t.item_wait,t.item_time,t.item_order"
                    dt = getDataTable(Sql, shTrans)
                    If dt.Rows.Count > 0 Then
                        Sql = "update tb_counter_queue "
                        Sql += " set flag = 'MainDisplay' "
                        Sql += " where datediff(d,getdate(),service_date)=0 "
                        Sql += " and queue_no = '" & FixDB(QueueNo) & "' "
                        Sql += " and customer_id = '" & FixDB(MobileNo) & "' "
                        Sql += " and item_id = " & dt.Rows(0).Item("item_id").ToString
                        Sql += " and status = " & Status
                        ret = executeSQL(Sql, shTrans, True)

                        'If CurrDB = "MAIN" Then
                        '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(Sql, INIFileName)
                        'End If
                    End If
                    dt.Dispose()
                Else
                    Sql = "update tb_counter_queue "
                    Sql += " set flag = 'MainDisplay' where datediff(d,getdate(),service_date)=0 "
                    Sql += " and queue_no = '" & FixDB(QueueNo) & "' "
                    Sql += " and customer_id = '" & FixDB(MobileNo) & "' "
                    Sql += " and item_id = " & myUser.item_id
                    Sql += " and status = " & Status
                    ret = executeSQL(Sql, shTrans, True)

                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(Sql, INIFileName)
                    'End If
                End If
            End If
        End If

        Return ret
    End Function

    'Insert Log กรณีที่มีการเปลี่ยนแปลงสถานะของลูกค้า
    Sub InsertLog(ByVal QueueNo As String, ByVal CustomerID As String, ByVal UserID As Integer, ByVal ItemID As Integer, ByVal CounterID As Integer, ByVal Status As Integer, ByVal Flag As String, ByVal _Trans As SqlTransaction, ByVal vDateNow As String, ByVal CurrDB As String)
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim RowID As Int32 = 0
        Dim vQID As Int32 = 0
        sql = "select top 1 id from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and customer_id = '" & CustomerID & "' and queue_no = '" & QueueNo & "' and item_id = " & ItemID
        dt = getDataTable(sql, _Trans)
        If dt.Rows.Count > 0 Then
            vQID = dt.Rows(0).Item("id")
        End If
        dt.Dispose()

        'sql = "exec SP_InsertLog " & Item & ",'" & FixDB(QueueNo) & "','" & FixDB(CustomerID) & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & FixDB(Flag) & "'"
        'executeSQL(sql, _Trans, True)

        Dim vID As Long = FindID("TB_LOGFILE", _Trans)
        sql = " insert into TB_LOGFILE(id,cq_id,log_date,queue_no,customer_id,user_id,item_id,counter_id,status,flag) "
        sql += " values('" & vID & "','" & vQID & "'," & vDateNow & ",'" & FixDB(QueueNo) & "','" & FixDB(CustomerID) & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & FixDB(Flag) & "')"
        executeSQL(sql, _Trans, True)

        'If CurrDB = "MAIN" Then
        '    Dim Ldt As New DataTable
        '    Ldt = getDataTable("select top 1 id, rowguid from TB_LOGFILE where queue_no='" & QueueNo & "' and customer_id='" & CustomerID & "' and item_id='" & ItemID & "' and counter_id='" & CounterID & "' and status='" & Status & "' and DATEDIFF(D,GETDATE(),log_date) = 0 order by id desc", _Trans)
        '    If Ldt.Rows.Count > 0 Then
        '        Dim dr As DataRow = Ldt.Rows(0)
        '        sql = " insert into TB_LOGFILE(id,cq_id,log_date,queue_no,customer_id,user_id,item_id,counter_id,status,flag,rowguid) "
        '        sql += " values('" & vID & "','" & vQID & "'," & vDateNow & ",'" & FixDB(QueueNo) & "','" & FixDB(CustomerID) & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & FixDB(Flag) & "','" & dr("rowguid").ToString & "')"

        '        Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
        '    End If
        '    Ldt.Dispose()
        'End If
    End Sub

    'Insert Log การใช้งานของ User
    Sub InsertUserAction(ByVal Action As Integer)
        If myUser.counter_id <> 0 Then
            'Action  1 = Login   2 = Logout   3 = Holdroom   4 = UnHoldroom
            Dim sql As String = ""
            sql = "exec SP_InsertUserAction " & myUser.user_id & "," & myUser.counter_id & "," & Action
            executeSQL(sql)

            Select Case Action
                Case 1
                    sql = "update TB_user set counter_id = " & myUser.counter_id & " where id =" & myUser.user_id
                    executeSQL(sql)
                Case 2
                    sql = "update TB_user set counter_id = 0 ,item_id = 0 where id =" & myUser.user_id
                    executeSQL(sql)
            End Select
        End If
    End Sub

    'Log Hold Room
    Sub LogHoldRoom(ByVal Action As Int32, Optional ByVal ReasonID As Int32 = 0, Optional ByVal Productive As Int32 = 0)
        If myUser.counter_id <> 0 Then
            'Action  1 = Holdroom   2 = UnHoldroom
            '@user_id as varchar(20),
            '@counter_id as int,
            '@reason_id as int,
            '@productive as int,
            '@action as int
            Dim sql As String = ""
            sql = "exec SP_HoldRoom " & myUser.user_id & "," & myUser.counter_id & "," & ReasonID & "," & Productive & "," & Action
            executeSQL(sql)
        End If
    End Sub

    'Log Login
    Sub LogLogin(ByVal Action As Int32, Optional ByVal ReasonID As Int32 = 0, Optional ByVal Productive As Int32 = 0)
        If myUser.counter_id <> 0 Then
            'Action  1 = Login   2 = Logout
            '@user_id as varchar(20),
            '@counter_id as int,
            '@reason_id as int,
            '@productive as int,
            '@action as int
            Dim sql As String = ""
            sql = "exec SP_LOGIN " & myUser.user_id & "," & myUser.counter_id & "," & ReasonID & "," & Productive & "," & Action & "," & myUser.item_id & ", '" & myUser.ip_address & "', '" & getMyVersion() & "'"
            'sql = "exec SP_LOGIN " & myUser.user_id & "," & myUser.counter_id & "," & ReasonID & "," & Productive & "," & Action & "," & myUser.item_id & ", '" & myUser.ip_address & "'"
            executeSQL(sql)
        End If
    End Sub

    'หาบริการทั้งหมดของลูกค้าที่ยังไม่ได้รับบริการ
    Public Function GetRemainALLService(ByVal CustomerID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String = "exec SP_RemainAllService '" & FixDB(CustomerID) & "'"
        dt = getDataTable(sql)
        Return dt
    End Function

    'เช็คว่ามีบริการของลูกค้าที่ต้องทำต่อหรือไม่
    Public Function GetRemainService(ByVal Queue As String, ByVal QCTB_ID As Integer) As Boolean
        Dim dt As New DataTable
        Dim sql As String = "exec SP_RemainService '" & FixDB(Queue) & "'," & QCTB_ID
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    'โชว์ Dialog ตอนจบบริการ
    Public Sub showNotify(ByVal Title As String, ByVal text As String)
        Dim ntfy As New TaskBarNotifier
        With ntfy
            .CloseButtonClickEnabled = True
            .TitleClickEnabled = False
            .TextClickEnabled = False
            .DrawTextFocusRect = True
            .KeepVisibleOnMouseOver = True
            .ReShowOnMouseOver = True
            .TransparencyKey = Color.PaleTurquoise

            Dim Image As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\alert.png")
            Dim Image1 As Image = System.Drawing.Image.FromFile(Application.StartupPath & "\close.bmp")
            .SetBackgroundBitmap(Image, Color.FromArgb(255, 0, 255))
            .SetCloseBitmap(Image1, Color.FromArgb(255, 0, 255), New Point(350, 34))
            .TitleRectangle = New Rectangle(80, 25, 300, 30)
            .TextRectangle = New Rectangle(-10, -20, 400, 300)
            .Show(Title, text, 500, 5000, 1000)
        End With
    End Sub

    'Update สถานะของห้องว่า มีการใช้งานหรือไม่
    Sub UpdateCounterStatus(ByVal CounterID As Integer, ByVal Status As Boolean)
        If CounterID <> 0 Then
            Dim available As Int32 = 0
            If Status = True Then
                available = 1
            End If
            Dim sql As String = ""
            sql = "update TB_counter set available = " & available & " where id = " & CounterID
            executeSQL(sql)
        End If
    End Sub

    Function ImageToStream(ByVal fileName As String) As Byte()
        Dim stream As New MemoryStream()
tryagain:
        Try
            Dim image As New Bitmap(fileName)
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
        Catch ex As Exception
            GoTo tryagain
        End Try
        Return stream.ToArray()
    End Function

    Public Sub WriteTextfile(ByVal patch As String, ByVal ValTxt As String)
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

    Function CheckHold(ByVal QueueNo As String, ByVal CustomerID As String) As Boolean
        'เช็คว่าเป็นลูกค้าที่ Hold ไว้หรือไม่ ถ้าใช้ก็ให้ปรับสถานะเป็น Misscall ไปเลย
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select hold from TB_counter_queue where datediff(d,getdate(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and hold is not null"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            UpdateQueueStatus(8, 0, 0, QueueNo, CustomerID)
            Return True
        End If
        Return False
    End Function

    Public Function StringFromLeft(ByVal strTmp As String, ByVal strLength As Integer) As String
        If (strLength > 0 And strTmp.Length >= strLength) Then
            Return strTmp.Substring(0, strLength)
        Else
            Return strTmp
        End If
    End Function

    Public Function StringFromRight(ByVal strTmp As String, ByVal strLength As Integer) As String
        If (strLength > 0 And strTmp.Length >= strLength) Then
            Return strTmp.Substring(strTmp.Length - strLength, strLength)
        Else
            Return strTmp
        End If
    End Function

    Public Function GetIPAddress() As String
        Dim oAddr As System.Net.IPAddress
        Dim sAddr As String
        With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
            oAddr = New System.Net.IPAddress(.AddressList(0).Address)
            sAddr = oAddr.ToString
        End With
        GetIPAddress = sAddr
    End Function

    Public Sub SaveQueryErrorLog(ByVal error_message As String, ByVal argSQL As String)
        Try
            Dim id As String = FindID("TB_Error_Log")
            Dim sql As String = ""
            sql = "Insert Into TB_Error_Log (id,client_ip, log_date,error_message, sql_command,version) Values (" & id & ",'" & GetIPAddress() & "',GetDate(),'" & FixDB(error_message) & "','" & FixDB(argSQL) & "','" & getMyVersion() & "') "
            executeSQL(sql)
        Catch ex As Exception
            'showFormError("Can't save attention log ." & vbNewLine & vbNewLine & ex.Message, False)
        End Try
    End Sub

    Public Function CompareDataTableNew(ByVal Dt1 As System.Data.DataTable, ByVal Dt2 As System.Data.DataTable, ByVal argKey1 As String, ByVal argKey2 As String) As System.Data.DataTable
        Dim joinDt As New DataTable
        If Dt1.Rows.Count > 0 And Dt2.Rows.Count > 0 Then
            joinDt = Join.LeftJoin.Join(Dt1, Dt2, Dt1.Columns(argKey1), Dt2.Columns(argKey2))
        End If
        Return joinDt
    End Function

    Function QueueCheckLDAP() As Boolean
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select config_name,config_value from TB_SETTING where config_name = 'q_con_ldap'"
        dt = getDataTable(sql)
        If dt.Rows(0).Item("config_value").ToString = "1" Then
            Return True
        End If
        Return False
    End Function

    Function AutoRefresh() As Int32
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select config_name,config_value from TB_SETTING where config_name = 'q_refresh'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("config_value") * 1000
        Else
            Return 10 * 1000
        End If
    End Function

    Sub LogoutIfErrorConnection()
        frmMain.CloseAllChildForm()
        If FormOpen(frmLogin) = False Then
            frmLogin.ExitApp = True
            frmLogin.ShowDialog()
        End If
    End Sub

    Public Function FormOpen(ByRef thisForm As Form) As Boolean
        Return thisForm.Visible
    End Function

    Function FindDateNow() As Date
        Dim ret As New DateTime
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select GETDATE() as DateNow"
        dt = getDataTable(sql)
        ret = dt.Rows(0).Item("DateNow")
        dt.Dispose()
        Return ret
    End Function

    Function FindDateNow(ByVal _trans As SqlTransaction) As Date
        Dim ret As New DateTime
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select GETDATE() as DateNow"
        dt = getDataTable(sql, _trans)
        If dt.Rows.Count > 0 Then
            ret = dt.Rows(0).Item("DateNow")
        Else
            dt = Engine.Common.QueueSharpENG.GetDataTableFromDisplay(sql, INIFileName)
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0).Item("DateNow")
            Else
                ret = DateTime.Now
            End If
        End If

        dt.Dispose()
        Return ret
    End Function

    Function GetServiceAssignment(Optional ByVal QueueNo As String = "", Optional ByVal CustomerID As String = "", Optional ByVal Status As Int32 = 0, Optional ByVal ServiceID As Int32 = 0) As DataTable
        Dim dt As New DataTable
        Dim sql As String = ""
        Try
            If ServiceID > 0 Then
                sql = "select item_id,item_name from TB_USER_SERVICE_SCHEDULE left join TB_ITEM on TB_USER_SERVICE_SCHEDULE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and TB_USER_SERVICE_SCHEDULE.active_status = 1 and user_id = " & myUser.user_id & " and item_id = " & ServiceID
            Else
                sql = "select item_id,item_name from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id where DATEDIFF(D,GETDATE(),service_date)=0 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' and status = " & Status & " and item_id in (select item_id from TB_USER_SERVICE_SCHEDULE where DATEDIFF(D,GETDATE(),service_date)=0 and active_status = 1 and user_id = " & myUser.user_id & ")"
            End If
            dt = getDataTable(sql)
        Catch ex As Exception : End Try

        Return dt

    End Function

    Function GetPrimaryService(ByVal UserID As Long) As Int32
        Dim ret As Int32 = 0
        'Get Primary Service 
        Dim sqlS As String = "select t.id,item_name "
        sqlS += " from TB_USER_SERVICE_SCHEDULE ss "
        sqlS += " left join tb_item t on ss.item_id = t.id  "
        sqlS += " where datediff(D,GETDATE(),ss.service_date)=0 "
        sqlS += " and ss.user_id = " & UserID & " and ss.active_status = 1 "
        sqlS += " and t.id in (select item_id from TB_USER_SKILL left join TB_SKILL_ITEM on TB_USER_SKILL.skill_id = TB_SKILL_ITEM.skill_id where user_id = " & UserID & ") "
        sqlS += " and ss.priority = 1"
        Dim dtS As New DataTable
        dtS = getDataTable(sqlS)
        If dtS.Rows.Count > 0 Then
            ret = dtS.Rows(0)("id")
        End If
        dtS.Dispose()
        Return ret
    End Function

    Sub UpdateAppointmentSlot(ByVal CustomerID As String, ByVal QueueNo As String)
        Dim sql As String = ""
        Dim dt As New DataTable
        Try
            sql = "select hold from TB_COUNTER_QUEUE left join TB_CUSTOMERTYPE on TB_COUNTER_QUEUE.customertype_id = TB_CUSTOMERTYPE.id where DATEDIFF(D,GETDATE(),service_date) = 0 and TB_CUSTOMERTYPE.app = 1 and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "'"
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                Dim Hold As DateTime = Convert.ToDateTime(dt.Rows(0).Item("hold"))
                sql = "declare @Slot as int; " & vbNewLine
                sql += " select @Slot = (select top 1 slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,GETDATE(),start_slot)=0); " & vbNewLine
                sql += " select *,@Slot as slot  " & vbNewLine
                sql += " from TB_APPOINTMENT_CUSTOMER " & vbNewLine
                sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 " & vbNewLine
                sql += " and customer_id = '" & FixDB(CustomerID) & "'"
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 1 To dt.Rows.Count
                        If i > 1 Then
                            Hold = DateAdd(DateInterval.Minute, CInt(dt.Rows(i - 1).Item("slot")), Hold)
                        End If
                        sql = "update TB_APPOINTMENT_SLOT "
                        sql += " set in_use = in_use - 1 "
                        sql += " where DATEDIFF(D,GETDATE(),app_date)=0 "
                        sql += " and CONVERT(varchar(5),slot_time,114) = '" & Hold.ToString("HH:mm") & "'"
                        executeSQL(sql)
                    Next
                End If
            End If
            dt.Dispose()
        Catch ex As Exception : End Try

        dt.Dispose()
    End Sub

    'Public Function GetShopConfig(ByVal ConfigName As String) As String
    '    Dim Sql = "select config_value from tb_setting where config_name = 'ConfigName'"
    '    Dim dt As New DataTable
    '    dt = getDataTable(Sql)
    '    Dim ret As String = ""
    '    If dt.Rows.Count > 0 Then
    '        ret = dt.Rows(0)("config_value").ToString
    '    End If
    '    Return ret
    'End Function

    Function GetWebServiceURL(Optional ByVal MainServer As Boolean = True) As String
        Dim ret As String = ""
        Dim sql As String = ""
        Dim dt As New DataTable

        If MainServer = True Then
            sql = "select config_value from tb_setting where config_name = 'm_webservice_url'"
        Else
            sql = "select config_value from tb_setting where config_name = 'd_webservice_url'"
        End If
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            ret = dt.Rows(0).Item("config_value").ToString
        End If
        Return ret
    End Function

#Region "Update Siebel Activity"
    Public Function GetSiebelData(ByVal MobileNo As String, ByVal AppointmentStatus As String, ByVal SiebelStatus As String) As DataTable
        'Update Siebel Activity  (ไม่รู้จะทำไปทำไม เสียเวลาเปล่า)
        Dim sqlS As String = "select top 1 start_slot,CONVERT(varchar(5),start_slot,114) as app_time,item_id,item_name,item_name_th,siebel_activity_id, siebel_desc "
        sqlS += " from TB_APPOINTMENT_CUSTOMER "
        sqlS += " inner join TB_ITEM on TB_APPOINTMENT_CUSTOMER.item_id = TB_ITEM.id  "
        sqlS += " where DateDiff(D, GETDATE(), app_date) = 0 And TB_ITEM.active_status = '1' "
        sqlS += " and TB_APPOINTMENT_CUSTOMER.active_status = " & AppointmentStatus & " "
        sqlS += " and customer_id = '" & MobileNo & "' "
        sqlS += " and siebel_status = '" & SiebelStatus & "'"
        sqlS += " order by item_wait,item_time,item_order"
        Dim dtS As New DataTable
        dtS = getDataTable(sqlS)

        Return dtS
    End Function

    Public Sub SiebelUpdateToCancel(ByVal MobileNo As String, ByVal CustomerTypeID As String)
        'Update Siebel To Cancel กรณีเป็นคิวจอง
        If MobileNo.Trim <> "" And CustomerTypeID = "3" Then
            Dim dtS As DataTable = GetSiebelData(MobileNo, "6", "Registered")
            If dtS.Rows.Count > 0 Then

                'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                  Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                  chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                  sslerror As System.Net.Security.SslPolicyErrors) True

                Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                Dim CancelDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                Try
                    Dim ini As New IniReader(INIFileName)
                    ini.Section = "Setting"
                    Dim ws As New ShopWebServiceMain.WebServiceAPI
                    ws.Url = GetWebServiceURL(True)
                    ws.SiebelUpdateToCancel(MobileNo, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                    ws = Nothing
                    ini = Nothing
                Catch ex As Exception
                    Dim sqlLog As String = "insert into tb_error_log "
                    sqlLog += " values(" & FindID("tb_error_log") & ", 1, getdate(),null,null,"
                    sqlLog += " getdate(), 'QueurSharp.frmAwaitingCustomer.GridWait_CellMouseClick : Don''t Update Siebel Activity To Cancel Mobile No. " & FixDB(MobileNo) & "', null, 'Kiosk', null, newid())"
                    executeSQL(sqlLog)

                    Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                    ws.Url = GetWebServiceURL(False)
                    ws.SiebelUpdateToCancel(MobileNo, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                    ws = Nothing
                End Try
            End If
            dtS.Dispose()
        End If
    End Sub

    Public Sub UpdateAppointmentJob(ByVal AppointJobID As Long, ByVal ActiveStatus As String)
        Try
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Url = GetWebServiceURL(True)
            ws.Timeout = 20000
            ws.UpdateAppointmentJobStatus(AppointJobID, ActiveStatus)
            ws = Nothing
        Catch ex As Exception
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Url = GetWebServiceURL(False)
            ws.Timeout = 20000
            ws.UpdateAppointmentJobStatus(AppointJobID, ActiveStatus)
            ws = Nothing
        End Try
    End Sub
#End Region
End Module
