
'*** IMPORTS ***
Imports SoundExtension.Org.Mentalis.Files
Imports System.Data.SqlClient
Imports System.Drawing
'***************
Public Class frmMain
    Dim SND_LOOP As String = ""
    'Dim SND_LANG As String = ""
    'Dim SND_DETECT As String = ""
    'Dim SND_SEQ As String = ""
    Dim SND_FREQ As String = ""
    'Dim SND_SUPPORTED_COUNTER As String = ""
    Dim STATE As String = ""
    'Dim SND_WORDING As String = ""


#Region "Configuration Manipulation"
    'Private Sub saveDefaultINI()
    '    Dim ini As New IniReader(iniFile)
    '    ini.Section = "SETTING"

    '    '*** Database Setting ***
    '    ini.Write("Server", "Server")
    '    ini.Write("Database", "Database Name")
    '    ini.Write("Username", "Username")
    '    ini.Write("Password", "Password")

    '    ini.Write("Server1", "Server")
    '    ini.Write("Database1", "Database Name")
    '    ini.Write("Username1", "Username")
    '    ini.Write("Password1", "Password")

    '    ini.Write("STATE", "0")

    '    '*** Other Setting ****
    '    ini.Write("SND_LOOP", "1")    '1 time looping
    '    ini.Write("SND_LANG", "T")    'Thai is default
    '    ini.Write("SND_DETECT", "0")  'Turn Nationality Detection Off
    '    ini.Write("SND_SEQ", "T-E")   'Thai First
    '    ini.Write("SND_FREQ", "1")    '1 Seconds Before Update Calling Queue List
    '    'ini.Write("SND_SUPPORTED_COUNTER", "") 'No Default Supported Counter
    '    'ini.Write("SND_WORDING", "")    'Empy Additional Wording
    'End Sub

    Function getINIValue(ByVal argKey As String) As String
        Dim ini As New IniReader(iniFile)
        ini.Section = "SETTING"
        If argKey <> "" Then
            Return ini.ReadString(argKey)
        Else
            Return ""
        End If
    End Function

    Sub getConfig()
        DB_SERVER = getINIValue("Server")
        DB_NAME = getINIValue("Database")
        DB_USER = getINIValue("Username")
        DB_PASS = Engine.Common.FunctionEng.DeCripPwd(getINIValue("Password"))
        DB_SERVER1 = getINIValue("Server1")
        DB_NAME1 = getINIValue("Database1")
        DB_USER1 = getINIValue("Username1")
        DB_PASS1 = Engine.Common.FunctionEng.DeCripPwd(getINIValue("Password1"))
        STATE = getINIValue("STATE")

        SND_LOOP = getINIValue("SND_LOOP")
        'SND_LANG = getINIValue("SND_LANG")
        'SND_DETECT = getINIValue("SND_DETECT")
        'SND_SEQ = getINIValue("SND_SEQ")
        SND_FREQ = getINIValue("SND_FREQ")
        'SND_SUPPORTED_COUNTER = getINIValue("SND_SUPPORTED_COUNTER")
        'SND_WORDING = getINIValue("SND_WORDING")
    End Sub

    Sub saveConfig()
        Dim ini As New IniReader(iniFile)
        ini.Section = "SETTING"
        'ini.Write("Server", DB_SERVER)
        'ini.Write("Database", DB_NAME)
        'ini.Write("Username", DB_USER)
        'ini.Write("Password", Engine.Common.FunctionEng.EnCripPwd(DB_PASS))
        'ini.Write("Server1", DB_SERVER1)
        'ini.Write("Database1", DB_NAME1)
        'ini.Write("Username1", DB_USER1)
        'ini.Write("Password1", Engine.Common.FunctionEng.EnCripPwd(DB_PASS1))
        'ini.Write("STATE", STATE)

        ini.Write("SND_LOOP", SND_LOOP)
        'ini.Write("SND_LANG", SND_LANG)
        'ini.Write("SND_DETECT", SND_DETECT)
        'ini.Write("SND_SEQ", SND_SEQ)
        ini.Write("SND_FREQ", SND_FREQ)
        'ini.Write("SND_SUPPORTED_COUNTER", SND_SUPPORTED_COUNTER)
        'ini.Write("SND_WORDING", SND_WORDING)
        ini = Nothing
    End Sub

    Sub setVarFromControl()
        ''*** Sound Language
        'If rdBoth.Checked Then
        '    SND_LANG = "B"
        'ElseIf rdEng.Checked Then
        '    SND_LANG = "E"
        'Else
        '    SND_LANG = "T"
        'End If
        ''*** Language Sequence
        'If rdEngFirst.Checked Then
        '    SND_SEQ = "E-T"
        'Else
        '    SND_SEQ = "T-E"
        'End If

        ''*** Auto Detect
        'SND_DETECT = IIf(chkDetect.Checked, "1", "0")
        '*** LOOP
        SND_LOOP = nudLoop.Value
        '*** Update Frequency
        SND_FREQ = nudFreq.Value

        ''*** Database
        'DB_SERVER = txtMainServer.Text
        'DB_NAME = txtMainDBName.Text
        'DB_USER = txtMainUserName.Text
        'DB_PASS = txtMainPass.Text

        'DB_SERVER1 = txtDisplayServer.Text
        'DB_NAME1 = txtDisplayDBName.Text
        'DB_USER1 = txtDisplayUserName.Text
        'DB_PASS1 = txtDisplayPass.Text

        'Dim a(lbAddWord.Items.Count - 1) As String
        'lbAddWord.Items.CopyTo(a, 0)
        'SND_WORDING = Join(a, ",")

    End Sub

    Sub setConfigToControl()
        txtMainServer.Text = DB_SERVER
        txtMainDBName.Text = DB_NAME
        txtMainUserName.Text = DB_USER
        txtMainPass.Text = DB_PASS

        txtDisplayServer.Text = DB_SERVER1
        txtDisplayDBName.Text = DB_NAME1
        txtDisplayUserName.Text = DB_USER1
        txtDisplayPass.Text = DB_PASS1

        ''*** Sound Language
        'Select Case SND_LANG.ToUpper
        '    Case "T" : rdThai.Checked = True
        '    Case "E" : rdEng.Checked = True
        '    Case "B" : rdBoth.Checked = True
        'End Select

        ''*** Language Sequence
        'rdThaiFirst.Checked = (SND_SEQ = "T-E") 'Is Thai be for Eng?
        'rdEngFirst.Checked = (SND_SEQ = "E-T") 'Is Eng be for Thai?

        ''*** Is Auto Detect? If So Enable Controls accordingly
        'If SND_DETECT = "1" Then
        '    enableThaiEng(False)
        '    enableSequence(False)
        '    chkDetect.Checked = True
        'Else
        '    enableThaiEng(True)
        '    enableSequence(SND_LANG = "B")
        '    chkDetect.Checked = False
        'End If

        '*** Looping
        If IsNumeric(SND_LOOP) Then
            If CInt(SND_LOOP) < nudLoop.Minimum Or CInt(SND_LOOP) > nudLoop.Maximum Then
                SND_LOOP = nudLoop.Minimum
            End If
        Else
            SND_LOOP = nudLoop.Minimum
        End If
        nudLoop.Value = SND_LOOP

        '*** DATABASE Checking Frquency for queue being Called
        If IsNumeric(SND_FREQ) Then
            If CInt(SND_FREQ) < nudFreq.Minimum Or CInt(SND_FREQ) > nudFreq.Maximum Then
                SND_FREQ = nudFreq.Minimum
            End If
        Else
            SND_FREQ = nudFreq.Minimum
        End If
        nudFreq.Value = SND_FREQ

        'Dim a() As String = Split(SND_WORDING, ",")
        'For i As Integer = 0 To UBound(a)
        '    lbAddWord.Items.Add(a(i))
        'Next


    End Sub

    'Private Function getStartupPath() As String
    '    Return Windows.Forms.Application.StartupPath & "\"
    'End Function

    'Sub enableSequence(ByVal argIsEnable As Boolean)
    '    Me.rdThaiFirst.Enabled = argIsEnable
    '    Me.rdEngFirst.Enabled = argIsEnable
    'End Sub

    'Sub enableThaiEng(ByVal argIsEnabled As Boolean)
    '    rdThai.Enabled = argIsEnabled
    '    rdEng.Enabled = argIsEnabled
    '    rdBoth.Enabled = argIsEnabled
    'End Sub

    Function INIExists() As Boolean
        If IO.File.Exists(iniFile) Then
            Return True
        Else
            Return False
        End If
    End Function

    'Private Sub Lang_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdBoth.Checked Then
    '        enableSequence(True)
    '    Else
    '        enableSequence(False)
    '    End If
    'End Sub

    'Private Sub chkDetect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If chkDetect.Checked Then
    '        enableThaiEng(False)
    '        enableSequence(False)
    '    Else
    '        enableThaiEng(True)
    '        enableSequence(rdBoth.Checked)
    '    End If
    'End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        setVarFromControl()
        saveConfig()
        Dim txt As String = "Configurations Saved Sucessfully"
        MessageBox.Show(txt, "Info.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        uplog(txt)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        getConfig()
        setConfigToControl()
    End Sub
    Sub saveState()
        Dim ini As New IniReader(iniFile)
        ini.Section = "SETTING"
        ini.Write("STATE", IIf(btnStart.Enabled, "0", "1"))
        STATE = ini.ReadString("STATE")
    End Sub
#End Region

#Region "Shared Functions"

    Function checkConnection() As Boolean
        Cursor.Current = Cursors.AppStarting
        Dim uc As New UpdateConResult
        uc = updateConn(Conn)
        If Not uc.IsOK Then
            uplog(uc.Message)
        End If
        Cursor.Current = Cursors.Default
        Return uc.IsOK
    End Function

    Sub uplog(ByVal argTXT As String)
        If argTXT Is Nothing Then
            Exit Sub
        End If
        If argTXT.Trim <> "" Then
            If txtLog.Text.Length > 20000 Then
                txtLog.Text = WriteLogFile(txtLog)
            End If
            txtLog.Text &= Now & vbTab & argTXT & vbCrLf
            txtLog.SelectionStart = Len(txtLog.Text)
            txtLog.ScrollToCaret()
        End If
    End Sub

#End Region




    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If Not INIExists() Then
        '    saveDefaultINI()
        'End If

        getConfig()
        setConfigToControl()

        btnStart.Enabled = True
        btnStop.Enabled = False
        If STATE = "1" Then
            btnStart.PerformClick()
            toggleStart(True)
        Else
            toggleStart(False)
        End If
        gbAbout.Text = "About " & My.Application.Info.AssemblyName
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Sub toggleStart(ByVal argIsStarted As Boolean)
        btnStart.Enabled = Not argIsStarted
        btnStop.Enabled = argIsStarted
        saveState()
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "AIS QSound Extionsion V " & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        'checkConnection()
    End Sub




#Region "Actions On Supported Counters' Tab"

    'Sub showAllCounter()
    '    If Not checkConnection() Then
    '        Exit Sub
    '    End If
    '    Cursor.Current = Cursors.AppStarting
    '    Dim sql As String = "select id as [ID],counter_name as [Counter Name], '' as [Counter Description], convert(bit,0) as [Select]  from tb_counter "
    '    Dim ds As New DataSet
    '    Dim da As New SqlDataAdapter(sql, Conn)
    '    Try
    '        da.Fill(ds)
    '        dgvSupp.DataSource = ds.Tables(0)
    '        dgvSupp.Columns(0).Visible = False
    '        dgvSupp.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    '        checkboxByConfig()

    '        Try
    '            Conn.Close()
    '        Catch ex As Exception
    '            uplog(ex.Message)
    '        End Try
    '    Catch ex As Exception
    '        uplog(ex.Message)
    '    End Try
    '    Cursor.Current = Cursors.Default
    'End Sub

    'Private Sub dgvSupp_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    If dgvSupp.RowCount > 0 Then
    '        If dgvSupp.Rows(e.RowIndex).Cells(3).Value = 0 Then
    '            dgvSupp.Rows(e.RowIndex).Cells(3).Value = 1
    '        Else
    '            dgvSupp.Rows(e.RowIndex).Cells(3).Value = 0
    '        End If
    '    End If
    'End Sub

    'Private Sub btnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    With dgvSupp
    '        If .RowCount > 0 Then
    '            For i As Integer = 0 To .RowCount - 1
    '                .Rows(i).Cells(3).Value = 1
    '            Next
    '        End If
    '    End With
    'End Sub

    'Private Sub btnSelNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    With dgvSupp
    '        If .RowCount > 0 Then
    '            For i As Integer = 0 To .RowCount - 1
    '                .Rows(i).Cells(3).Value = 0
    '            Next
    '        End If
    '    End With
    'End Sub

    'Sub checkboxByConfig()
    '    'If SND_SUPPORTED_COUNTER = "" Then
    '    '    btnSelNone.PerformClick()
    '    'Else
    '    '    Dim tmp() As String
    '    '    tmp = Split(SND_SUPPORTED_COUNTER, "::")
    '    '    Array.Sort(tmp)
    '    '    For i As Integer = 0 To dgvSupp.RowCount - 1
    '    '        If Array.IndexOf(tmp, dgvSupp.Rows(i).Cells(0).Value.ToString) >= 0 Then
    '    '            dgvSupp.Rows(i).Cells(3).Value = 1
    '    '        End If
    '    '    Next
    '    'End If
    'End Sub

    'Function getFormatSelectedCounterForINI() As String
    '    Dim tmp As String = ""
    '    With dgvSupp
    '        For i As Integer = 0 To .RowCount - 1
    '            If dgvSupp.Rows(i).Cells(3).Value Then 'If Cell(3) Checked
    '                If tmp = "" Then
    '                    tmp = .Rows(i).Cells(0).Value
    '                Else
    '                    tmp &= "::" & .Rows(i).Cells(0).Value   'Con cat each counter_id by
    '                End If
    '            End If
    '        Next
    '    End With
    '    Return tmp
    'End Function

    'Private Sub btnSaveCounter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'SND_SUPPORTED_COUNTER = getFormatSelectedCounterForINI()

    '    Dim ini As New IniReader(iniFile)
    '    ini.Section = "SETTING"
    '    ini.Write("SND_SUPPORTED_COUNTER", SND_SUPPORTED_COUNTER)
    '    Dim txt As String = "Supported Counters has been Saved Successfully"
    '    MessageBox.Show(txt, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    '    uplog(txt)
    '    ini = Nothing
    'End Sub
#End Region


    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        uplog(My.Application.Info.AssemblyName & " - Started")
        toggleStart(True)
        tmrUpdate.Interval = IIf(IsNumeric(SND_FREQ), SND_FREQ * 1000, 2000)
        tmrUpdate.Enabled = True
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Text = My.Application.Info.AssemblyName & " v" & myVersion()
        lblProductName.Text = My.Application.Info.AssemblyName & " v" & myVersion()
        lblProductName.Left = gbAbout.Width / 2 - lblProductName.Width / 2
        frmSplash.Show()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub tmrUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdate.Tick
        doStartPlaySoundBlink()
        tmrUpdate.Interval = IIf(IsNumeric(SND_FREQ), SND_FREQ * 1000, 2000)
    End Sub
    Sub doStartPlaySoundBlink()
        lblStatus.Text = "Checking for Queue..."
        tmrUpdate.Enabled = False
        btnState.ImageKey = "Green"
        If checkConnection() Then
            Dim sql As String = ""
            Dim ds As New DataSet
            sql = "select  s.id,queue_no, s.counter_name,s.nationality "
            sql += " from tb_Speaker s "
            sql += " inner join tb_counter c on c.id=s.counter_id"
            sql += " where s.status='0' and convert(varchar(8),s.call_date,112)=convert(varchar(8),getdate(),112) "
            sql += " and c.back_office=0 and c.counter_manager=0"
            sql += " order by s.call_date asc"
            Dim da As New SqlDataAdapter(sql, Conn)
            Try
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    For Each dr As DataRow In ds.Tables(0).Rows
                        lblID.Text = dr("id").ToString
                        lblQueue.Text = dr("queue_no").ToString
                        lblCounter.Text = dr("counter_name").ToString
                        lblNation.Text = dr("nationality").ToString

                        PlaySoundBlink()
                        Me.TableLayoutPanel1.Refresh()
                        Application.DoEvents()
                        Try
                            Dim cmd As New SqlCommand("update tb_Speaker set status='1' where id='" & lblID.Text & "' ", Conn)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            uplog(ex.Message)
                        End Try
                    Next
                End If
                ds.Dispose()
                da.Dispose()
            Catch ex As Exception
                uplog(ex.Message)
            End Try
        End If

        lblID.Text = ""
        lblQueue.Text = ""
        lblCounter.Text = ""
        lblNation.Text = ""

        btnState.ImageKey = "Grey"
        tmrUpdate.Enabled = CBool(STATE)
        lblStatus.Text = "Ready"

    End Sub


    'Sub speakRoomName(ByVal tmp As String, ByVal lang As String)
    '    'Dim regex As System.Text.RegularExpressions.Regex
    '    'Dim tmp2 As String = regex.IsMatch.Replace(tmp, "[0-9]", "")

    '    For i As Integer = 0 To lbAddWord.Items.Count - 1
    '        If tmp = "" Then Exit For
    '        If InStr(tmp, lbAddWord.Items(i).ToString) Then
    '            SpeakOut(lbAddWord.Items(i).ToString, lang)
    '            tmp = tmp.Replace(lbAddWord.Items(i).ToString, "").Trim
    '        End If
    '    Next
    '    If tmp <> "" Then
    '        SpeakOut(tmp, lang) 'หมายเลขห้อง
    '    End If
    'End Sub


#Region "Old Sourcecode"
    Sub doStartSpeakQueue()
        lblStatus.Text = "Checking for Queue..."
        tmrUpdate.Enabled = False
        btnState.ImageKey = "Green"
        If checkConnection() Then
            Dim sql As String = ""
            Dim ds As New DataSet
            sql = "select top 1 id,queue_no, counter_name,nationality from tb_Speaker where status='0' order by call_date asc"
            Dim da As New SqlDataAdapter(sql, Conn)
            Try
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).Rows(0)
                        lblID.Text = .Item("id").ToString
                        lblQueue.Text = .Item("queue_no").ToString
                        lblCounter.Text = .Item("counter_name").ToString
                        lblNation.Text = .Item("nationality").ToString
                    End With
                End If
                ds.Dispose()
                da.Dispose()
            Catch ex As Exception
                uplog(ex.Message)
            End Try
        End If

        'Dim Qno_ndx As Int16 = 1
        'Dim Room_ndx As Int16 = 2
        Dim tmp As String
        Me.TableLayoutPanel1.Refresh()
        Application.DoEvents()
        'For i As Integer = 0 To dgvQ.RowCount - 1

        If lblQueue.Text <> "" Then
            For j As Integer = 1 To IIf(IsNumeric(SND_LOOP), SND_LOOP, 1)
                tmp = lblCounter.Text
                'If SND_DETECT = "1" Then    'Language Auto Detection
                '    If Strings.Left(lblNation.Text, 4).ToUpper = "THAI" Then
                '        '**** THAI
                '        SpeakOut("^1", "th")    'เชิญ
                '        SpeakOut(lblQueue.Text, "th") 'หมายเลขคิว
                '        SpeakOut("^2", "th")    'ที่ห้อง
                '        'speakRoomName(tmp, "th")
                '        SpeakOut("^3", "")  'ค่ะ
                '    Else
                '        '*** ENGLISH
                '        SpeakOut("^1", "en")    'เชิญ
                '        SpeakOut(lblQueue.Text, "en") 'หมายเลขคิว
                '        SpeakOut("^2", "en")    'ที่ห้อง
                '        'speakRoomName(tmp, "en")
                '    End If
                'Else
                '    'If SND_LANG = "B" Then
                '    '    If SND_SEQ = "T-E" Then
                '    '        '**** THAI
                '    '        SpeakOut("^1", "th")    'เชิญ
                '    '        SpeakOut(lblQueue.Text, "th") 'หมายเลขคิว
                '    '        SpeakOut("^2", "th")    'ที่ห้อง
                '    '        'speakRoomName(tmp, "th")
                '    '        SpeakOut("^3", "")  'ค่ะ

                '    '        '*** ENGLISH
                '    '        SpeakOut("^1", "en")    'เชิญ
                '    '        SpeakOut(lblQueue.Text, "en") 'หมายเลขคิว
                '    '        SpeakOut("^2", "en")    'ที่ห้อง
                '    '        'speakRoomName(tmp, "en")
                '    '    Else

                '    '        '*** ENGLISH
                '    '        SpeakOut("^1", "en")    'เชิญ
                '    '        SpeakOut(lblQueue.Text, "en") 'หมายเลขคิว
                '    '        SpeakOut("^2", "en")    'ที่ห้อง
                '    '        'speakRoomName(tmp, "en")

                '    '        '**** THAI
                '    '        SpeakOut("^1", "th")    'เชิญ
                '    '        SpeakOut(lblQueue.Text, "th") 'หมายเลขคิว
                '    '        SpeakOut("^2", "th")    'ที่ห้อง
                '    '        'speakRoomName(tmp, "th")
                '    '        SpeakOut("^3", "")  'ค่ะ
                '    '    End If
                '    '    'ElseIf SND_LANG = "E" Then
                '    '    '    '*** ENGLISH
                '    '    '    SpeakOut("^1", "en")    'เชิญ
                '    '    '    SpeakOut(lblQueue.Text, "en") 'หมายเลขคิว
                '    '    '    SpeakOut("^2", "en")    'ที่ห้อง
                '    '    '    'speakRoomName(tmp, "en")
                '    'Else
                '    '    '**** THAI
                '    '    SpeakOut("^1", "th")    'เชิญ
                '    '    SpeakOut(lblQueue.Text, "th") 'หมายเลขคิว
                '    '    SpeakOut("^2", "th")    'ที่ห้อง
                '    '    'speakRoomName(tmp, "th")
                '    '    SpeakOut("^3", "")  'ค่ะ
                '    'End If
                'End If
                Application.DoEvents()
            Next
            Try
                Dim cmd As New SqlCommand("update q_Speaker set status='1' where id='" & lblID.Text & "' ", Conn)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                uplog(ex.Message)
            End Try
        End If
        lblID.Text = ""
        lblQueue.Text = ""
        lblCounter.Text = ""
        lblNation.Text = ""

        'Next
        btnState.ImageKey = "Grey"
        tmrUpdate.Enabled = CBool(STATE)
        lblStatus.Text = "Ready"

        '---------------Force Minimize---------------
        'If tmrUpdate.Enabled Then Me.WindowState = FormWindowState.Minimized

    End Sub
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MessageBox.Show("Are you sure?", "Confirm Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Application.Exit()
        End If
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        tmrUpdate.Enabled = False
        toggleStart(False)
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Me.tabCfg.Enabled = False
        Cursor.Current = Cursors.AppStarting
        Dim tmp As New UpdateConResult
        Dim strConn As String = formatConnectionString(txtMainServer.Text, txtMainDBName.Text, txtMainUserName.Text, txtMainPass.Text)
        tmp = CheckConn(Conn, strConn)
        If tmp.IsOK Then
            MessageBox.Show("Main Server Database Connection Testing Success.", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            MessageBox.Show("Main Server Database Connection Testing Failure!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            uplog(tmp.Message)
        End If
        Cursor.Current = Cursors.Default
        Me.tabCfg.Enabled = True
    End Sub


    'Private Sub tabCfg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCfg.SelectedIndexChanged
    '    If tabCfg.TabPages(tabCfg.SelectedIndex).Name.ToString.ToUpper = tbSppRoom.Name.ToUpper Then
    '        'showAllCounter()
    '    End If
    'End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblWeb.LinkClicked
        System.Diagnostics.Process.Start("http://www.scoresolutions.co.th")
    End Sub


    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmAddWord
        f.ShowDialog()
    End Sub

    'Private Sub lbAddWord_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If lbAddWord.SelectedItems.Count = 0 Or lbAddWord.Items.Count = 0 Then
    '        btnDel.Enabled = False
    '    Else
    '        btnDel.Enabled = True
    '    End If
    'End Sub

    'Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If lbAddWord.SelectedItems.Count > 0 Then
    '        lbAddWord.Items.RemoveAt(lbAddWord.SelectedIndex)
    '    End If
    'End Sub

    Private Sub TimerStrart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerStrart.Tick
        'Restart ทุก เที่ยงคืน
        Dim MidNight As DateTime = DateTime.FromOADate(Int(Now.ToOADate) + 1)
        If Math.Abs(DateDiff(DateInterval.Second, Now, MidNight)) < 20 Then
            Application.Restart()
        End If
    End Sub

    Private Sub btnDisplayDBTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplayDBTest.Click
        Me.tabCfg.Enabled = False
        Cursor.Current = Cursors.AppStarting
        Dim tmp As New UpdateConResult
        Dim strConn As String = formatConnectionString(txtDisplayServer.Text, txtDisplayDBName.Text, txtDisplayUserName.Text, txtDisplayPass.Text)
        tmp = CheckConn(Conn, strConn)
        If tmp.IsOK Then
            MessageBox.Show("Display Server Database Connection Testing Success.", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            MessageBox.Show("Display Server Database Connection Testing Failure!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            uplog(tmp.Message)
        End If
        Cursor.Current = Cursors.Default
        Me.tabCfg.Enabled = True
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub
End Class
