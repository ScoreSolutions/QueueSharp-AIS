Imports System.IO
Imports Engine
Imports HeartBeat
Imports System.Data

Public Class frmAisCSIAgent
    Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

    Private Sub frmHQAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "AIS CSI Agent V" & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub tmCSISurvey_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCSISurvey.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()

        tmCSISurvey.Enabled = False
        Dim eng As New CSISurveyENG
        Try
            eng.FilterData()
        Catch ex As Exception
            eng.CreateLogFile("tmCSISurvey_Tick", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        eng = Nothing
        tmCSISurvey.Enabled = True

    End Sub

    Private Sub frmAisCSIAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS CSI Agent V" & getMyVersion()

        tmCSISurvey.Enabled = False
        tmCSISendATSR.Enabled = False
        Dim eng As New CSISurveyENG
        Try
            eng.FilterData()
        Catch ex As Exception
            eng.CreateLogFile("frmAisCSIAgent_Show", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        eng = Nothing
        tmCSISurvey.Enabled = True
        tmCSISendATSR.Enabled = True
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub

    Private Sub tmCalTarget_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCalTarget.Tick
        tmCalTarget.Enabled = False
        Dim eng As New Engine.CSISurveyENG
        Try
            eng.CalTarget()
        Catch ex As Exception
            eng.CreateLogFile("tmCalTarget_Tick", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        eng = Nothing
        tmCalTarget.Enabled = True
    End Sub

    Private Sub tmCSISendATSR_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCSISendATSR.Tick
        tmCSISendATSR.Enabled = False
        Dim eng As New Engine.CSISurveyENG
        Try
            eng.SendATSR()
            eng.GetATSRResult()
            eng.SendResultToShop()
        Catch ex As Exception
            eng.CreateLogFile("tmCSISendATSR_Tick", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        eng = Nothing
        tmCSISendATSR.Enabled = True
    End Sub
End Class