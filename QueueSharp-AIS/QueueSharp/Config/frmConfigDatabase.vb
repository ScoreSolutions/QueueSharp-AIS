Imports QueueSharp.Org.Mentalis.Files
Imports System.Data
Imports System.Data.SqlClient
Imports Engine.Common.ShopConnectDBENG

Public Class frmConfigDatabase

    Public BeginProgram As Boolean = False
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Primary As Boolean = True
        Dim Secondary As Boolean = True

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        If txtServer.Text.Trim = "" And txtDatabase.Text.Trim = "" And txtUsername.Text.Trim = "" And txtServer1.Text.Trim = "" And txtDatabase1.Text.Trim = "" And txtUsername1.Text.Trim = "" Then
            MessageBox.Show("All fields in the Primary & Secondary Database must be filled up first. " & Environment.NewLine & "- Server" & Environment.NewLine & "- Database" & Environment.NewLine & "- Username", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        ElseIf txtServer.Text.Trim = txtServer1.Text.Trim And txtDatabase.Text.Trim = txtDatabase1.Text.Trim And txtUsername.Text.Trim = txtUsername1.Text.Trim And txtPassword.Text.Trim = txtPassword1.Text.Trim Then
            MessageBox.Show("Primary & Secondary Database 's dupicate. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If

        '**************** Database Primary *******************
        'If PingServer(txtServer.Text.Trim) = True Then
        If TestConnection(getConnectionStrTest) = False Then
            Primary = False
        End If
        'Else
        'Primary = False
        'End If
        '******************************************************

        '*************** Database Secondary *******************
        'If PingServer(txtServer1.Text.Trim) = True Then
        If TestConnection(getConnectionStrTest1) = False Then
            Secondary = False
        End If
        'Else
        'Secondary = False
        'End If
        '******************************************************

        If Primary = False And Secondary = False Then
            MessageBox.Show("The Primary & Secondary Database cannot be connected." & vbCrLf & "Please check its connection.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtServer.Focus()
            Exit Sub
        End If

        If Primary = False And Secondary = True Then
            If MessageBox.Show("The Primary Database cannot be connected." & vbCrLf & "Please check its connection.", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If

        If Primary = True And Secondary = False Then
            If MessageBox.Show("The Secondary Database cannot be connected." & vbCrLf & "Please check its connection.", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If

        ini.Write("Server", txtServer.Text.Trim)
        ini.Write("Database", txtDatabase.Text.Trim)
        ini.Write("Username", txtUsername.Text.Trim)
        ini.Write("Password", Engine.Common.FunctionEng.EnCripPwd(txtPassword.Text.Trim))
        ini.Write("Server1", txtServer1.Text.Trim)
        ini.Write("Database1", txtDatabase1.Text.Trim)
        ini.Write("Username1", txtUsername1.Text.Trim)
        ini.Write("Password1", Engine.Common.FunctionEng.EnCripPwd(txtPassword1.Text.Trim))
        ini = Nothing
        frmMain.TimerCheckStatus.Enabled = False
        MessageBox.Show("Your input data is successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Application.Restart()
    End Sub

    Private Sub frmDatabaseConfig_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Dir(INIFileName) = "" Then
            Windows.Forms.Application.Restart()
        End If
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub frmDatabaseConfig_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtServer.Focus()
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
        ini = Nothing
    End Sub

    Function getConnectionStrTest() As String
        Return "Data Source=" & txtServer.Text & ";Initial Catalog=" & txtDatabase.Text & ";User ID=" & txtUsername.Text & ";Password=" & txtPassword.Text & ";Connect Timeout=1;"
    End Function

    Function getConnectionStrTest1() As String
        Return "Data Source=" & txtServer1.Text & ";Initial Catalog=" & txtDatabase1.Text & ";User ID=" & txtUsername1.Text & ";Password=" & txtPassword1.Text & ";Connect Timeout=1;"
    End Function

    Private Sub txtServer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtServer.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtDatabase.Focus()
        End If
    End Sub

    Private Sub txtDatabasr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDatabase.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtUsername.Focus()
        End If
    End Sub

    Private Sub txtUsername_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsername.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnSave.Focus()
        End If
    End Sub

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

    
End Class