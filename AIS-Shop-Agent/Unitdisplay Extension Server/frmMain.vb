Imports Unitdisplay_Extension_Server.Org.Mentalis.Files
Imports System.IO.Ports
Imports System.Data.SqlClient

Public Class frmMain
    Dim sqlstr As String

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click

        If cboComport.Text.Trim = "" Then
            MessageBox.Show("กรุณาเลือกช่องการเชื่อต่ออุปกรณ์ Unitdisplay", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"

        If txtServer.Text.Trim <> CStr(ini.ReadString("Server")) Or txtDatabase.Text.Trim <> CStr(ini.ReadString("Database")) Or txtUsername.Text.Trim <> CStr(ini.ReadString("Username")) Or txtPassword.Text.Trim <> CStr(Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("Password"))) Or NUD.Value.ToString <> CStr(ini.ReadString("Interval")) Or cboComport.Text.ToUpper.Replace("COM", "").Trim <> CStr(ini.ReadString("COM")) Or txtServer1.Text.Trim <> CStr(ini.ReadString("Server1")) Or txtDatabase1.Text.Trim <> CStr(ini.ReadString("Database1")) Or txtUsername1.Text.Trim <> CStr(ini.ReadString("Username1")) Or txtPassword1.Text.Trim <> CStr(Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("Password1"))) Then
            MessageBox.Show("กรุณาบันทึกข้อมูลก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ini.ReadString("Interval") = "" Then
            MessageBox.Show("กรุณาตั้งค่าข้อมูลก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        If OpenComUnitDisplay() Then
            sqlstr = "DELETE FROM TB_UNITDISPLAY"
            executeSQL("frmMain_btnStart.Click", sqlstr)
            ini.Write("Start", "T")
            Disable()
        Else
            MessageBox.Show("ไม่สามารถเชื่อมต่อกับป้าย Unitdisplay ได้", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If


    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Try
            Dim ini As New IniReader(INIFileName)
            ini.Section = "SETTING"
            ini.Write("Start", "F")
            Enable()
            CloseCom(hCom)
        Catch ex As Exception : End Try
    End Sub

    Sub Disable()
        TimerDisplay.Interval = NUD.Value * 1000
        TimerDisplay.Enabled = True
        btnStop.Enabled = True
        btnStart.Enabled = False
        gb.Enabled = False
        btnSave.Enabled = False
        PictureBox1.Visible = True
        PictureBox2.Visible = False
    End Sub

    Sub Enable()
        TimerDisplay.Enabled = False
        btnStop.Enabled = False
        btnStart.Enabled = True
        gb.Enabled = True
        btnSave.Enabled = True
        PictureBox1.Visible = False
        PictureBox2.Visible = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboComport.Text.Trim = "" Then
            MessageBox.Show("กรุณาเลือกช่องการเชื่อมต่ออุปกรณ์ Unitdisplay", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Dim txtErrorConnection As String = ""

        '************** Database Primary *******************
        If TestConnection(getConnectionStrTest) = False Then
            txtServer.Focus()
            txtErrorConnection = "ไม่สามารถเชื่อมต่อฐานข้อมูล Primary ได้"
            ini.Write("Server", "")
            ini.Write("Database", "")
            ini.Write("Username", "")
            ini.Write("Password", "")
        Else
            ini.Write("Server", txtServer.Text.Trim)
            ini.Write("Database", txtDatabase.Text.Trim)
            ini.Write("Username", txtUsername.Text.Trim)
            ini.Write("Password", Engine.Common.FunctionEng.EnCripPwd(txtPassword.Text.Trim))
        End If
        '***************************************************
        '************** Database Secondary *****************
        If TestConnection(getConnectionStrTest1) = False Then
            If txtErrorConnection = "" Then
                txtErrorConnection = "ไม่สามารถเชื่อมต่อฐานข้อมูล Secondary ได้"
            Else
                txtErrorConnection = txtErrorConnection & vbNewLine & "ไม่สามารถเชื่อมต่อฐานข้อมูล Secondary ได้"
            End If
            ini.Write("Server1", "")
            ini.Write("Database1", "")
            ini.Write("Username1", "")
            ini.Write("Password1", "")
        Else
            ini.Write("Server1", txtServer1.Text.Trim)
            ini.Write("Database1", txtDatabase1.Text.Trim)
            ini.Write("Username1", txtUsername1.Text.Trim)
            ini.Write("Password1", Engine.Common.FunctionEng.EnCripPwd(txtPassword1.Text.Trim))
        End If
        '***************************************************

        If txtErrorConnection = "" Then
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnStart.Enabled = True
        Else
            If InStr(txtErrorConnection, "Primary") > 0 And InStr(txtErrorConnection, "Secondary") > 0 Then
                MessageBox.Show(txtErrorConnection, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                If MessageBox.Show(txtErrorConnection & vbCrLf & "ต้องการทำรายการต่อหรือไม่ ?", "ผิดพลาด", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                Else
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnStart.Enabled = True
                End If
            End If
        End If

        ini.Write("Com", cboComport.SelectedItem.ToString.Replace("COM", ""))
        ini.Write("Interval", NUD.Value.ToString)
        btnStart.Enabled = True

    End Sub

    Function getConnectionStrTest() As String
        Return "Data Source=" & txtServer.Text & ";Initial Catalog=" & txtDatabase.Text & ";Persist Security Info=True;User ID=" & txtUsername.Text & ";Password=" & txtPassword.Text & ";"
    End Function

    Function getConnectionStrTest1() As String
        Return "Data Source=" & txtServer1.Text & ";Initial Catalog=" & txtDatabase1.Text & ";Persist Security Info=True;User ID=" & txtUsername1.Text & ";Password=" & txtPassword1.Text & ";"
    End Function

    Public Function TestConnection(ByVal ConnectionString As String) As Boolean
        Dim TestConn As New SqlConnection
        Try
            TestConn.ConnectionString = ConnectionString
            TestConn.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Application.DoEvents()

        UpdateVersion_Company()
        txtServer.Focus()
        Try
            Dim myComPort As New SerialPort
            For i As Int32 = 0 To My.Computer.Ports.SerialPortNames.Count - 1
                cboComport.Items.Add(My.Computer.Ports.SerialPortNames(i))
            Next
        Catch ex As Exception
            ''กรณีที่ไม่มี Comport ใดเปิดใช้งานอยู่เลย จึง Add Port Com1 เข้าไปก่อน
            'cboComport.Items.Add("COM1")
        End Try

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        txtServer.Text = CStr(ini.ReadString("Server"))
        txtDatabase.Text = CStr(ini.ReadString("Database"))
        txtUsername.Text = CStr(ini.ReadString("Username"))
        txtPassword.Text = CStr(Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("Password")))
        txtServer1.Text = CStr(ini.ReadString("Server1"))
        txtDatabase1.Text = CStr(ini.ReadString("Database1"))
        txtUsername1.Text = CStr(ini.ReadString("Username1"))
        txtPassword1.Text = CStr(Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("Password1")))

        cboComport.SelectedIndex = cboComport.FindString("COM" & ini.ReadString("Com"))
        If ini.ReadString("Interval") <> "" Then
            NUD.Value = CInt(ini.ReadString("Interval"))
        End If

        If ini.ReadString("Start") = "T" Then
            btnStart.Enabled = True
            btnStart.PerformClick()
        ElseIf ini.ReadString("Start") = "F" Then
            btnStop.Enabled = True
            btnStop.PerformClick()
        End If

        NotifyIcon.Visible = True
        Me.Hide()
    End Sub

    Private Sub TimerDisplay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerDisplay.Tick
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select * from TB_UNITDISPLAY order by id"
        dt = getDataTable("frmMain_TimerDiaplsy_Tick", sql)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("counter_no").ToString <> "" Then
                Select Case dt.Rows(0).Item("action")
                    Case 0
                        CallUnitDisplay(dt.Rows(0).Item("queue_no"), dt.Rows(0).Item("counter_no"))
                    Case 1
                        ServeUnitDisplay(dt.Rows(0).Item("queue_no"), dt.Rows(0).Item("counter_no"))
                    Case 2
                        ldlUnitDisplay(dt.Rows(0).Item("counter_no"))
                    Case 3
                        PauseUnitDisplay(dt.Rows(0).Item("counter_no"))
                    Case 4
                        ClearUnitDisplay(dt.Rows(0).Item("counter_no"))
                    Case 5
                        ShowTexe(dt.Rows(0).Item("txt"), dt.Rows(0).Item("counter_no"))
                End Select
            End If
            sql = "delete from TB_UNITDISPLAY where id = " & dt.Rows(0).Item("id")
            executeSQL("frmMain_TimerDisplay.Tick", sql)
        End If
        dt.Dispose()
    End Sub



    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon.Visible = True
        Else
            NotifyIcon.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon.Visible = False
    End Sub

    Private Sub TimerCheckConnection_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckConnection.Tick
        ''Try
        ''    If ConnecetionPrimaryDB = False Then
        ''        Dim ChkConn As New SqlClient.SqlConnection
        ''        Conn.ConnectionString = getConnectionString()
        ''        Conn.Open()
        ''        ConnecetionPrimaryDB = True
        ''    End If
        ''Catch ex As Exception : End Try
        'Engine.Common.ShopConnectDBENG.CheckCurrentActiveDB(INIFileName, Conn)

    End Sub
End Class
