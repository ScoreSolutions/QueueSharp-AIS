Imports System.Data.SqlClient
Imports Dashboard.Org.Mentalis.Files
Imports Engine.Common.FunctionEng
Public Class frmConfig
    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ini As New IniReader(Application.StartupPath & "\Dashboard.ini")
        ini.Section = "Setting"
        txtMainServer.Text = ini.ReadString("Server")
        txtMainDB.Text = ini.ReadString("Database")
        txtMainUser.Text = ini.ReadString("Username")
        txtMainPass.Text = DeCripPwd(ini.ReadString("Password"))
        txtDisplayServer.Text = ini.ReadString("Server1")
        txtDisplayDBName.Text = ini.ReadString("Database1")
        txtDisplayUser.Text = ini.ReadString("Username1")
        txtDisplayPass.Text = DeCripPwd(ini.ReadString("Password1"))

        Dim RefreshEvery As String = Engine.Common.ShopConnectDBENG.GetShopConfig(Application.StartupPath & "\Dashboard.ini", "DashboardRefreshMinute", "Dashboard", "frmComfig_Load")
        numRefleshEvery.Value = IIf(RefreshEvery.Trim = "", 10, RefreshEvery)

        ini = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ini As New IniReader(Application.StartupPath & "\Dashboard.ini")
        Try
            ini.Section = "Setting"
            ini.Write("Server", txtMainServer.Text)
            ini.Write("Database", txtMainDB.Text)
            ini.Write("Username", txtMainUser.Text)
            ini.Write("Password", EnCripPwd(txtMainPass.Text))
            ini.Write("Server1", txtDisplayServer.Text)
            ini.Write("Database1", txtDisplayDBName.Text)
            ini.Write("Username1", txtDisplayUser.Text)
            ini.Write("Password1", EnCripPwd(txtDisplayPass.Text))
            ini = Nothing

            Dim sql As String = "update tb_setting set config_value='" & numRefleshEvery.Value & "' where config_name='DashboardRefreshMinute'"
            If Engine.Common.ShopConnectDBENG.ExecuteNonQuery(sql, Application.StartupPath & "\Dashboard.ini", "Dashboard", "frmConfig_btnSave.Click") = True Then
                MessageBox.Show("Save Success!!!", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.Yes
                Me.Close()
            Else
                MessageBox.Show("Cannon Update Config", "Save Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Save Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btmMainTestConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmMainTestConnect.Click
        TestConnect(txtMainServer.Text, "Data Source=" & txtMainServer.Text & ";Initial Catalog=" & txtMainDB.Text & ";Persist Security Info=True;User ID=" & txtMainUser.Text & ";Password=" & txtMainPass.Text)
    End Sub

    Private Sub btnDisplayTestConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplayTestConnect.Click
        TestConnect(txtDisplayServer.Text, "Data Source=" & txtDisplayServer.Text & ";Initial Catalog=" & txtDisplayDBName.Text & ";Persist Security Info=True;User ID=" & txtDisplayUser.Text & ";Password=" & txtDisplayPass.Text)
    End Sub

    Private Sub TestConnect(ByVal IPAddress As String, ByVal StrConn As String)
        Try
            Using sqlConnection As SqlConnection = New SqlConnection(StrConn)
                sqlConnection.Open()
                MessageBox.Show("Test Connection Success!!!", "Test Connection Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As SqlException
            MessageBox.Show(ex.Message, "Test Connection Fail", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    
    Private Sub BrowseFile(ByVal txtFlashService As TextBox)
        'สร้าง object OpenFileDialog.
        Dim objOpenFile = New OpenFileDialog()

        'Title ของ Dialog.
        objOpenFile.Title = "Open File Dailog"
        If txtFlashService.Text = "" Then
            objOpenFile.InitialDirectory = Application.StartupPath
        Else
            objOpenFile.InitialDirectory = txtFlashService.Text
        End If

        'กรองนามสกุลไฟล์
        objOpenFile.Filter = "Text Files|*.exe|ALL Files|*.*"
        'Open Dialog, ทำงานเมื่อกดปุ่ม OK
        If objOpenFile.ShowDialog() = DialogResult.OK Then
            'นำ Path ที่ได้ไปแสดงบน TextBox
            txtFlashService.Text = objOpenFile.FileName
        End If
    End Sub

End Class