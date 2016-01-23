Imports Agent_Conductor.Org.Mentalis.Files

Public Class frmMain

    Dim INIFileName As String = Application.StartupPath & "\AgentConductor.ini"

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon.Visible = True
        Else
            NotifyIcon.Visible = False
        End If
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Application.DoEvents()

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        txtP1.Text = CStr(ini.ReadString("P1"))
        txtP2.Text = CStr(ini.ReadString("P2"))
        txtP3.Text = CStr(ini.ReadString("P3"))
        txtP4.Text = CStr(ini.ReadString("P4"))
        txtP5.Text = CStr(ini.ReadString("P5"))
        NUD.Value = ini.ReadString("Interval")

        If ini.ReadString("Start") = "T" Then
            btnStart.Enabled = True
            btnStart.PerformClick()
        Else
            ini.Write("Start", "F")
            btnStop.Enabled = True
            btnStop.PerformClick()
        End If

        'NotifyIcon.Visible = True
        'Me.Hide()
    End Sub

    Private Sub NotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon.Visible = False
    End Sub

    Sub Disable()
        btnStop.Enabled = True
        btnStart.Enabled = False
        btnStart.BackColor = Color.Gray
        btnStop.BackColor = Color.SteelBlue
        GB.Enabled = False
        btnSave.Enabled = False
        PictureBox1.Visible = True
        PictureBox2.Visible = False

        If txtP1.Text <> "" Then
            T1.Interval = NUD.Value * 1000
            T1.Enabled = True
        End If
        If txtP2.Text <> "" Then
            T2.Interval = NUD.Value * 1000
            T2.Enabled = True
        End If
        If txtP3.Text <> "" Then
            T3.Interval = NUD.Value * 1000
            T3.Enabled = True
        End If
        If txtP4.Text <> "" Then
            T4.Interval = NUD.Value * 1000
            T4.Enabled = True
        End If
        If txtP5.Text <> "" Then
            T5.Interval = NUD.Value * 1000
            T5.Enabled = True
        End If

    End Sub

    Sub Enable()
        T1.Enabled = False
        T2.Enabled = False
        T3.Enabled = False
        T4.Enabled = False
        T5.Enabled = False
        btnStop.Enabled = False
        btnStart.Enabled = True
        btnStop.BackColor = Color.Gray
        btnStart.BackColor = Color.SteelBlue
        GB.Enabled = True
        btnSave.Enabled = True
        PictureBox1.Visible = False
        PictureBox2.Visible = True
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"

        If txtP1.Text.Trim <> CStr(ini.ReadString("P1")) Or txtP2.Text.Trim <> CStr(ini.ReadString("P2")) Or txtP3.Text.Trim <> CStr(ini.ReadString("P3")) Or txtP4.Text.Trim <> CStr(ini.ReadString("P4")) Or txtP5.Text.Trim <> CStr(ini.ReadString("P5")) Or NUD.Value.ToString <> CStr(ini.ReadString("Interval")) Then
            MessageBox.Show("กรุณาบันทึกข้อมูลก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ini.ReadString("Interval") = "" Then
            MessageBox.Show("กรุณาตั้งค่าข้อมูลก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ini.Write("Start", "T")
        Disable()
        txtDisplay.Text = "Start Program..."
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("Start", "F")
        Enable()
        txtDisplay.Text = "Stop Program..."
    End Sub

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        SFD1.Filter = "(*.exe)|*.exe"
        SFD1.ShowDialog()
        txtP1.Text = SFD1.FileName
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        SFD2.Filter = "(*.exe)|*.exe"
        SFD2.ShowDialog()
        txtP2.Text = SFD2.FileName
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        SFD3.Filter = "(*.exe)|*.exe"
        SFD3.ShowDialog()
        txtP3.Text = SFD3.FileName
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        SFD4.Filter = "(*.exe)|*.exe"
        SFD4.ShowDialog()
        txtP4.Text = SFD4.FileName
    End Sub

    Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        SFD5.Filter = "(*.exe)|*.exe"
        SFD5.ShowDialog()
        txtP5.Text = SFD5.FileName
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        ini.Write("P1", txtP1.Text)
        ini.Write("P2", txtP2.Text)
        ini.Write("P3", txtP3.Text)
        ini.Write("P4", txtP4.Text)
        ini.Write("P5", txtP5.Text)

        If txtP1.Text <> "" Then
            ini.Write("P1_App", FindApplicationName(txtP1.Text))
        End If

        If txtP2.Text <> "" Then
            ini.Write("P2_App", FindApplicationName(txtP2.Text))
        End If

        If txtP3.Text <> "" Then
            ini.Write("P3_App", FindApplicationName(txtP3.Text))
        End If

        If txtP4.Text <> "" Then
            ini.Write("P4_App", FindApplicationName(txtP4.Text))
        End If

        If txtP5.Text <> "" Then
            ini.Write("P5_App", FindApplicationName(txtP5.Text))
        End If

        ini.Write("Interval", NUD.Value.ToString)
        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Function FindApplicationName(ByRef PathName As String) As String
        Dim App As String = ""
        Try
            Dim Program() As String = PathName.Split("\")
            App = Program(Program.Length - 1).Replace(".exe", "")
        Catch ex As Exception : End Try
        Return App
    End Function

    Public Function ProcessesResponse(ByVal ProcessName As String) As Boolean
        Dim p As Process() = Process.GetProcessesByName(ProcessName)
        Try
            If p IsNot Nothing AndAlso p(0).Responding Then
                Return True
            Else
                p(0).Kill()
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Function ProcessesRunning(ByVal ProcessName As String) As Integer
        Try
            If Process.GetProcessesByName(ProcessName).GetUpperBound(0) + 1 > 0 Then
                Return True
            End If
        Catch : End Try
        Return False
    End Function

    Sub OpenProgram(ByVal PathProgram As String)
        If System.IO.File.Exists(PathProgram) Then
            Dim proc As New Process()
            proc.StartInfo.FileName = PathProgram
            proc.StartInfo.Arguments = ""
            proc.Start()
        End If
    End Sub
   
    Private Sub T1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T1.Tick
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try

            If ProcessesRunning(ini.ReadString("P1_APP")) = False AndAlso ProcessesResponse(ini.ReadString("P1_APP")) = False Then
                txtDisplay.Text = txtDisplay.Text & vbCrLf & Date.Now.ToShortTimeString & "  Restart Program " & ini.ReadString("P1_APP")
                OpenProgram(ini.ReadString("P1"))
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub T2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T2.Tick
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try
            If ProcessesRunning(ini.ReadString("P2_APP")) = False AndAlso ProcessesResponse(ini.ReadString("P2_APP")) = False Then
                OpenProgram(ini.ReadString("P2"))
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub T3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T3.Tick
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try
            If ProcessesRunning(ini.ReadString("P3_APP")) = 0 Then
                If System.IO.File.Exists(ini.ReadString("P3")) Then
                    Dim proc As New Process()
                    proc.StartInfo.FileName = ini.ReadString("P3")
                    proc.StartInfo.Arguments = ""
                    proc.Start()
                End If
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub T4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T4.Tick
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try
            If ProcessesRunning(ini.ReadString("P4_APP")) = 0 Then
                If System.IO.File.Exists(ini.ReadString("P4")) Then
                    Dim proc As New Process()
                    proc.StartInfo.FileName = ini.ReadString("P4")
                    proc.StartInfo.Arguments = ""
                    proc.Start()
                End If
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub T5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T5.Tick
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try
            If ProcessesRunning(ini.ReadString("P5_APP")) = 0 Then
                If System.IO.File.Exists(ini.ReadString("P5")) Then
                    Dim proc As New Process()
                    proc.StartInfo.FileName = ini.ReadString("P5")
                    proc.StartInfo.Arguments = ""
                    proc.Start()
                End If
            End If
        Catch ex As Exception : End Try
    End Sub
End Class
