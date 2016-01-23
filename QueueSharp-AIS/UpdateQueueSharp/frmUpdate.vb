Imports QIS.Org.Mentalis.Files
Imports System.Net
Imports System.Net.NetworkInformation

Public Class frmUpdate

    Dim INIFile As String = Application.StartupPath & "\Myversion.ini"
    Dim ini As New IniReader(INIFile)
    Dim NewVersion As String = ""
    Dim MyVersion As String = ""
    Dim PathINI As String = ""
    Dim PathEXE As String = ""

    Private Sub frmUpdate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.Yes Then
            OpenQueueSharp()
        End If
    End Sub

    Private Sub frmUpdate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        ini.Section = "Setting"
        ini.Write("process", "F")
        txtStatus.Text = "... ตรวจสอบการเชื่อมต่อกับ เซิร์ฟเวอร์ ..." & vbNewLine
        PgLogo.Focus()
        Me.Hide()
        frmWait.Show()
        bw.RunWorkerAsync()
        timerCheckProcess.Enabled = True
    End Sub

    Private Sub lnlSetting_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnlSetting.LinkClicked
        Select Case Me.Height
            Case 190
                Me.Height = 290
            Case Else
                Me.Height = 190
        End Select
        gbProxy.Visible = Me.Height <> 190
    End Sub

    Private Sub btnINI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnINI.Click
        sfdINI.Filter = "Version.ini|Version.ini"
        sfdINI.ShowDialog()
        txtINI.Text = sfdINI.FileName
    End Sub

    Private Sub btnEXE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEXE.Click
        sfdEXE.Filter = "QueueSharp.exe|QueueSharp.exe"
        sfdEXE.ShowDialog()
        txtEXE.Text = sfdEXE.FileName
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        If txtINI.Text.Trim <> "" And txtEXE.Text.Trim <> "" Then
            ini.Write("ini", txtINI.Text.Trim)
            ini.Write("exe", txtEXE.Text.Trim)
            ini.Write("process", "F")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Application.Restart()
        Else
            MessageBox.Show("กรุณาตั้งค่าการเชื่อมต่อกับ เซิร์ฟเวอร์", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Function CompareVersion(ByVal OldVersion As String, ByVal NewVersion As String) As Boolean
        Dim _OldVersion() As String
        _OldVersion = Split(OldVersion, ".")
        Dim _NewVersion() As String
        _NewVersion = Split(NewVersion, ".")
        For i As Integer = 0 To _OldVersion.Length - 1
            If CInt(_OldVersion(i)) < CInt(_NewVersion(i)) Then Return True
        Next
        Return False
    End Function

    Sub OpenQueueSharp()
        Application.Exit()
        KillProcess("QueueSharp")
        Dim proc As New Process()
        proc.StartInfo.FileName = Application.StartupPath & "\QueueSharp.exe"
        proc.StartInfo.Arguments = ""
        proc.Start()
        KillProcess("UpdateQueueSharp")
    End Sub

    Private Sub timerStartProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerCheckProcess.Tick
        If CStr(ini.ReadString("process")) = "F" Then
            Exit Sub
        End If
        frmWait.Close()
        Me.Show()
        txtStatus.Text = "... ไม่สามารถเชื่อมต่อกับ เซิร์ฟเวอร์ ได้ ..." & vbNewLine
        timerCheckProcess.Enabled = False
    End Sub

    Private Sub bw_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw.DoWork
        Try
            txtINI.Text = CStr(ini.ReadString("ini"))
            txtEXE.Text = CStr(ini.ReadString("exe"))
            MyVersion = CStr(ini.ReadString("version"))
            If txtINI.Text.Trim <> "" Then
                PathINI = txtINI.Text
                PathEXE = txtEXE.Text
                Application.DoEvents()
                If System.IO.File.Exists(PathINI) = True And System.IO.File.Exists(PathEXE) = True Then
                    Dim iniVersion As New IniReader(PathINI)
                    iniVersion.Section = "Setting"
                    NewVersion = CStr(iniVersion.ReadString("version"))

                    If CompareVersion(MyVersion, NewVersion) Then
                        Dim NewCopy As String = Application.StartupPath & "\QueueSharp.tmp"
                        If System.IO.File.Exists(PathEXE) = True Then
                            If System.IO.File.Exists(NewCopy) = True Then
                                System.IO.File.Delete(NewCopy)
                            End If
                            System.IO.File.Copy(PathEXE, NewCopy)
                            If System.IO.File.Exists(Application.StartupPath & "\QueueSharp_" & MyVersion & ".tmp") = True Then
                                System.IO.File.Delete(Application.StartupPath & "\QueueSharp_" & MyVersion & ".tmp")
                            End If
                            My.Computer.FileSystem.RenameFile(Application.StartupPath & "\" & "QueueSharp.exe", "QueueSharp_" & MyVersion & ".tmp")
                            My.Computer.FileSystem.RenameFile(Application.StartupPath & "\" & "QueueSharp.tmp", "QueueSharp.exe")
                            ini.Section = "Setting"
                            ini.Write("version", NewVersion.Trim)
                            OpenQueueSharp()
                        End If
                    Else
                        OpenQueueSharp()
                    End If
                End If
            End If
        Catch ex As Exception : End Try
        ini.Write("process", "T")
    End Sub

End Class