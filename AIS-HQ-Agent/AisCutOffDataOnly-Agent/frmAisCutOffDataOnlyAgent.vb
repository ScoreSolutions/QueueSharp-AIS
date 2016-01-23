Imports System.IO
Imports Engine
Imports HeartBeat
Imports System.Data

Public Class frmAisCutOffDataOnlyAgent
    Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

    Private Sub tmCutOffData_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCutOffData.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()

        tmCutOffData.Enabled = False
        Dim eng As New Engine.CutOffDataENG
        eng.SetTextLog(txtLog)
        eng.CutOffData(lblTime, False)
        eng = Nothing

        Application.Exit()
    End Sub

    Private Sub frmHQAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtLog.Text = ""
    End Sub

    Private Sub frmAisCutOffDataAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Application.CommandLineArgs.Count > 0 Then
            tmCutOffData.Enabled = True
        End If
    End Sub

    Private Sub frmAisCutOffDataAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS Cut off Data Agent V" & getMyVersion()
    End Sub
    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub
End Class