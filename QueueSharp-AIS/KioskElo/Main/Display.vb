Imports KioskElo.Org.Mentalis.Files

Public Class Display

    Dim clickX As Integer = 0
    Dim clickY As Integer = 0

    Private Sub Display_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Me.Left = ini.ReadInteger("LEFT")
        Me.Top = ini.ReadInteger("TOP")
        Me.Height = 25

        If ini.ReadInteger("Ticket") = 0 Then
            ini.Write("Ticket", 0)
        End If

        If ini.ReadInteger("MaxQ") = 0 Then
            ini.Write("MaxQ", 560)
        End If

        CountTicket.Enabled = True

    End Sub

#Region "Location"
    Private Sub frmFloat_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, lblCount.MouseDown, lblPer.MouseDown
        clickX = e.X
        clickY = e.Y
    End Sub

    Private Sub frmFloat_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, lblCount.MouseMove, lblPer.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left = Me.Left + (e.X - clickX)
            Me.Top = Me.Top + (e.Y - clickY)
        End If
    End Sub

    Private Sub frmFloat_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, lblCount.MouseUp, lblPer.MouseUp
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("LEFT", Me.Left)
        ini.Write("TOP", Me.Top)
    End Sub
#End Region

    Private Sub CountTicket_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CountTicket.Tick
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"

        Dim MaxQ As Integer = ini.ReadInteger("MaxQ")
        Dim Ticket As Integer = ini.ReadInteger("Ticket")
        lblCount.Text = Ticket

        Dim Per As Integer = 100 - (((Ticket) / MaxQ) * 100)
        lblPer.Text = CStr(Per) & "%"
        If Per <= 10 Then
            lblPer.ForeColor = Color.Red
        ElseIf Per <= 60 Then
            lblPer.ForeColor = Color.Yellow
        Else
            lblPer.ForeColor = Color.Lime
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If MessageBox.Show("คุณต้องการจะเคลีย์ข้อมูล ใช่หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim ini As New IniReader(INIFileName)
            ini.Section = "SETTING"
            ini.Write("Ticket", 0)
            lblCount.Text = 0
            lblPer.Text = "100%"
            lblPer.ForeColor = Color.Lime
        End If
    End Sub

End Class
