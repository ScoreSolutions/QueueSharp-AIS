Imports Update_Agent_Extension_Server.Org.Mentalis.Files
Imports System.IO.Ports
Imports System.Data.SqlClient

Public Class frmMain

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"

        If txtServer.Text.Trim <> CStr(ini.ReadString("Server")) Or txtDatabase.Text.Trim <> CStr(ini.ReadString("Database")) Or txtUsername.Text.Trim <> CStr(ini.ReadString("Username")) Or txtPassword.Text.Trim <> CStr(ini.ReadString("Password")) Or NUD.Value.ToString <> CStr(ini.ReadString("Interval")) Or txtServer1.Text.Trim <> CStr(ini.ReadString("Server1")) Or txtDatabase1.Text.Trim <> CStr(ini.ReadString("Database1")) Or txtUsername1.Text.Trim <> CStr(ini.ReadString("Username1")) Or txtPassword1.Text.Trim <> CStr(ini.ReadString("Password1")) Then
            MessageBox.Show("กรุณาบันทึกข้อมูลก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If CStr(ini.ReadString("Server")) = "" And CStr(ini.ReadString("Server")) = "" Then
            MessageBox.Show("กรุณาตั่งค่าการเชื่อมต่อ", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ini.ReadString("Interval") = "" Then
            MessageBox.Show("กรุณาตั้งค่าข้อมูลก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ini.Write("Start", "T")
        Disable()

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

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("Start", "F")
        Enable()
    End Sub

    Sub Disable()
        TimerDisplay.Interval = NUD.Value * 1000
        TimerDisplay.Enabled = True
        btnStop.Enabled = True
        btnStart.Enabled = False
        GB.Enabled = False
        btnSave.Enabled = False
        PictureBox1.Visible = True
        PictureBox2.Visible = False
    End Sub

    Sub Enable()
        TimerDisplay.Enabled = False
        btnStop.Enabled = False
        btnStart.Enabled = True
        GB.Enabled = True
        btnSave.Enabled = True
        PictureBox1.Visible = False
        PictureBox2.Visible = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim txtErrorConnection As String = ""
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

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
            ini.Write("Password", txtPassword.Text.Trim)
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
            ini.Write("Password1", txtPassword1.Text.Trim)
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

        ini.Write("Interval", NUD.Value.ToString)

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Application.DoEvents()

        UpdateVersion_Company()
        txtServer.Focus()

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        txtServer.Text = CStr(ini.ReadString("Server"))
        txtDatabase.Text = CStr(ini.ReadString("Database"))
        txtUsername.Text = CStr(ini.ReadString("Username"))
        txtPassword.Text = CStr(ini.ReadString("Password"))
        txtServer1.Text = CStr(ini.ReadString("Server1"))
        txtDatabase1.Text = CStr(ini.ReadString("Database1"))
        txtUsername1.Text = CStr(ini.ReadString("Username1"))
        txtPassword1.Text = CStr(ini.ReadString("Password1"))
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
        sql = "update TB_USER set counter_id = 0 ,item_id = 0,ip_address = null,check_update = getdate() where DATEDIFF(ss,check_update,GETDATE()) > " & NUD.Value.ToString
        executeSQL(sql)
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
        Try
            If ConnecetionPrimaryDB = False Then
                Dim ChkConn As New SqlClient.SqlConnection
                Conn.ConnectionString = getConnectionString()
                Conn.Open()
                ConnecetionPrimaryDB = True
            End If
        Catch ex As Exception : End Try
    End Sub

End Class
