Imports System.IO
Imports Engine
Imports HeartBeat
Imports System.Data

Public Class frmAisWorkForceAgent
    Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

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

    Private Sub tmWorkForce_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmWorkForce.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()

        Dim eng As New Engine.WorkForceENG
        eng.SetTextLog(txtLog)
        Dim StartTime As String = "08:30"
        Dim EndTime As String = "09:00"
        Dim CurrTime As DateTime = DateTime.Now
        For i As Int32 = 1 To 26
            StartTime = DateAdd(DateInterval.Minute, 30, CDate(StartTime)).ToShortTimeString.PadLeft(5, "0")
            EndTime = DateAdd(DateInterval.Minute, 30, CDate(EndTime)).ToShortTimeString.PadLeft(5, "0")
            eng.WFStartProcess(DateAdd(DateInterval.Day, -1, DateTime.Now), StartTime, EndTime, lblTime)
        Next
        UpdateHB(patch, "WorkForce")

        End
    End Sub


    Private Sub SetAllEnable(ByVal status As Boolean)
        cmbShop.Enabled = status
        Button2.Enabled = status
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SetAllEnable(False)

        Dim DateFrom As DateTime = dtpST.Value
        Dim DateTo As DateTime = dtpET.Value
        Dim CurrDate As DateTime = DateFrom
        Do
            Dim eng As New Engine.WorkForceENG
            eng.SetTextLog(txtLog)

            Dim ServiceDate As Date = CurrDate
            Dim StartTime As String = "08:30"
            Dim EndTime As String = "09:00"
            For i As Int32 = 1 To 26
                StartTime = DateAdd(DateInterval.Minute, 30, CDate(StartTime)).ToShortTimeString.PadLeft(5, "0")
                EndTime = DateAdd(DateInterval.Minute, 30, CDate(EndTime)).ToShortTimeString.PadLeft(5, "0")
                eng.WFStartProcess(ServiceDate, StartTime, EndTime, lblTime)
            Next
            eng = Nothing

            CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
        Loop While CurrDate <= DateTo
        MessageBox.Show("Add Workforce Complete!!!")
        SetAllEnable(True)
    End Sub

    Private Sub frmHQAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Application.CommandLineArgs.Count > 0 Then
            tmWorkForce.Enabled = True
        End If
    End Sub
End Class