Imports System.IO
Imports Engine
Imports HeartBeat
Imports System.Data

Public Class frmAisAppointmentAgent
    Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

    Private Sub frmHQAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "AIS Appointment Agent V" & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub


    Private Sub tmSendAppointmentNotify_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmSendAppointmentNotify.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        Try
            tmSendAppointmentNotify.Enabled = False
            Dim eng As New Engine.AppointmentNotifyENG
            eng.SetTextLog(txtLog)
            eng.SendNotify(lblTime)
            eng = Nothing
            tmSendAppointmentNotify.Enabled = True
        Catch ex As Exception : End Try

    End Sub

    Private Sub tmGenSiebel_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmGenSiebel.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        Try
            tmGenSiebel.Enabled = False
            Dim eng As New Engine.AppointmentGenSiebelENG
            eng.SetTextLog(txtLog)
            eng.UpdateSiebelActivity(lblTime)
            eng = Nothing
            tmGenSiebel.Enabled = True
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub tmBlacklist_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmBlacklist.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        Try
            tmBlacklist.Enabled = False
            Dim eng As New Engine.AppointmentBacklistENG
            eng.SetTextLog(txtLog)
            eng.SetBlacklist(lblTime)
            eng = Nothing
            tmBlacklist.Enabled = True
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub frmAisAppointmentAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS Appointment Agent V" & getMyVersion()
    End Sub

    Dim LastUpdateOn As DateTime = DateTime.Now
    Dim RefreshTime As Integer = 1
    Private Sub tmUpdateShopService_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmUpdateShopService.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        Try
            If DateAdd(DateInterval.Minute, RefreshTime, LastUpdateOn) < DateTime.Now Then
                tmUpdateShopService.Enabled = False
                Dim eng As New Engine.AppointmentUpdateShopServiceENG
                eng.UpdateShopAppointmentService(lblTime)
                eng = Nothing
                tmUpdateShopService.Enabled = True

                LastUpdateOn = DateTime.Now
                RefreshTime = Engine.Common.FunctionEng.GetQisDBConfig("RefreshShopAppointService")
            End If
        Catch ex As Exception

        End Try
        
    End Sub
End Class