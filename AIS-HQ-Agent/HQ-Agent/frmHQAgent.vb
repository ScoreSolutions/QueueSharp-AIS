Imports System.IO
Imports Engine
Imports HeartBeat
Imports System.Data

Public Class frmHQAgent
    Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

    Private Sub tmGenSiebel_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmGenSiebel.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkGenSiebel.Checked = True Then
            tmGenSiebel.Enabled = False
            Dim eng As New Engine.AppointmentGenSiebelENG
            eng.SetTextLog(txtLog)
            eng.UpdateSiebelActivity(lblTime)
            eng = Nothing
            tmGenSiebel.Enabled = True
        End If
        UpdateHB(patch, "GenerateSiebelActivity")
    End Sub

    Private Sub tmClearCustomerInfo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmClearCustomerInfo.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkClearCustInfo.Checked = True Then
            tmClearCustomerInfo.Enabled = False
            Dim eng As New Engine.ClearCustInfoENG
            eng.SetTextLog(txtLog)
            eng.ClearCustInfo()
            eng = Nothing
            tmClearCustomerInfo.Enabled = True
        End If
        'WriteHeartBeat("ClearCustomerInfo")
        UpdateHB(patch, "ClearCustomerInfo")
    End Sub


    Private Sub tmCutOffData_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCutOffData.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkCutOffData.Checked = True Then
            tmCutOffData.Enabled = False
            Dim eng As New Engine.CutOffDataENG
            eng.SetTextLog(txtLog)
            eng.CutOffData(lblTime, True)
            eng = Nothing
            tmCutOffData.Enabled = True
        End If
        'WriteHeartBeat("CutOffData")
        UpdateHB(patch, "CutOffData-ProcessAllReport")
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

    Private Sub tmBlacklist_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmBlacklist.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkBlackList.Checked = True Then
            tmBlacklist.Enabled = False
            Dim eng As New Engine.AppointmentBacklistENG
            eng.SetTextLog(txtLog)
            eng.SetBlacklist(lblTime)
            eng = Nothing
            tmBlacklist.Enabled = True
        End If
        'WriteHeartBeat("GenerateBlacklist")
        UpdateHB(patch, "GenerateBlacklist")
    End Sub

    Private Sub tmArchMaster_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmArchMaster.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkArchMaster.Checked = True Then
            tmArchMaster.Enabled = False
            Dim eng As New Engine.ArchMasterENG
            eng.SetTextLog(txtLog)
            eng.ArchiveData()
            eng = Nothing
            tmArchMaster.Enabled = True
        End If
        'WriteHeartBeat("ArchiveMaster")
        UpdateHB(patch, "ArchiveMaster")
    End Sub

    Private Sub tmSendAppointmentNotify_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmSendAppointmentNotify.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkSendAppointmentNotify.Checked = True Then
            Try
                tmSendAppointmentNotify.Enabled = False
                Dim eng As New Engine.AppointmentNotifyENG
                eng.SetTextLog(txtLog)
                eng.SendNotify(lblTime)
                eng = Nothing
                tmSendAppointmentNotify.Enabled = True
            Catch ex As Exception : End Try
        End If
        'WriteHeartBeat("SendAppointmentNotify")
        UpdateHB(patch, "SendAppointmentNotify")
    End Sub

    Private Sub tmCSISurvey_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCSISurvey.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkCSI.Checked = True Then
            tmCSISurvey.Enabled = False
            Dim eng As New CSISurveyENG
            eng.FilterData()
            eng.SendATSR()
            eng.GetATSRResult()
            eng.SendResultToShop()
            eng = Nothing
            tmCSISurvey.Enabled = True
        End If
        'WriteHeartBeat("CSISurvey")
        UpdateHB(patch, "CSISurvey")
    End Sub

    Private Sub tmWorkForce_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmWorkForce.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
        If chkWorkforce.Checked = True Then
            tmWorkForce.Enabled = False

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
            
            tmWorkForce.Enabled = True
        End If
        'WriteHeartBeat("WorkForce")
        UpdateHB(patch, "WorkForce")
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        SetAllEnable(False)

        Dim dt As New DataTable
        If cmbShop.SelectedValue = "0" Then
            dt = Engine.Common.FunctionEng.GetActiveShop
        Else
            dt = Engine.Common.FunctionEng.GetShopDtByID(cmbShop.SelectedValue)
        End If

        For Each dr As DataRow In dt.Rows
            Dim strDate As DateTime = dtpST.Value
            Dim endDate As DateTime = dtpET.Value
            Dim currDate As DateTime = strDate
            Do
                Dim ServiceDate As DateTime = currDate
                If cbSeg.Checked = True Then
                    '************************* Segment ****************************
                    Dim seENG As New Reports.ReportsCustBySegmentENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        seENG.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " Segment T " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    'If cbD.Checked Then
                    '    Application.DoEvents()
                    '    seENG.ProcReportByDate(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Segment D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbW.Checked Then
                    '    Application.DoEvents()
                    '    seENG.ProcReportByWeek(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Segment W " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbM.Checked Then
                    '    Application.DoEvents()
                    '    seENG.ProcReportByMonth(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Segment M " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbY.Checked Then
                    '    Application.DoEvents()
                    '    seENG.ProcReportByYear(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Segment Y " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    seENG = Nothing
                End If

                If cbNet.Checked = True Then
                    '************************* Network Type ****************************
                    Dim ntENG As New Reports.ReportsCustByNetworkTypeENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        ntENG.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " Network Type T " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    'If cbD.Checked Then
                    '    Application.DoEvents()
                    '    ntENG.ProcReportByDate(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Network Type D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbW.Checked Then
                    '    Application.DoEvents()
                    '    ntENG.ProcReportByWeek(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Network Type W " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbM.Checked Then
                    '    Application.DoEvents()
                    '    ntENG.ProcReportByMonth(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Network Type M " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbY.Checked Then
                    '    Application.DoEvents()
                    '    ntENG.ProcReportByYear(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " Network Type Y " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    ntENG = Nothing
                End If

                If cbWTsh.Checked = True Then
                    '************************* WT & HT Shop ****************************
                    Dim whsEng As New Reports.ReportsWaitingTimeHandlingTimeByShopENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        whsEng.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " WT & HT Shop T " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    'If cbD.Checked Then
                    '    Application.DoEvents()
                    '    whsEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                    '    TextBox1.Text = currDate.ToShortDateString & " WT & HT Shop D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbW.Checked Then
                    '    Application.DoEvents()
                    '    whsEng.ProcReportByWeek(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " WT & HT Shop W " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        whsEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " WT & HT Shop M " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    'If cbY.Checked Then
                    '    Application.DoEvents()
                    '    whsEng.ProcReportByYear(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " WT & HT Shop Y " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    whsEng = Nothing
                End If

                'If cbWTsk.Checked = True Then
                '    '********************* WT & HT Skill **********************
                '    Dim whskEng As New Reports.ReportsWaitingTimeHandlingTimeBySkillENG
                '    If cbD.Checked Then
                '        Application.DoEvents()
                '        whskEng.ProcReportByDate(ServiceDate)
                '        TextBox1.Text = currDate.ToShortDateString & " WT & HT Skill D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                '    End If
                'End If

                If cbWTst.Checked = True Then
                    '******************** WT & HT Agent ***********************
                    Dim wtaEng As New Reports.ReportsWaitingTimeHandlingTimeByAgentENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        wtaEng.ProcessReportByTime(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " WT & HT Agent D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    wtaEng = Nothing
                End If

                If cbKPIsh.Checked = True Then
                    '*********************** KPI Shop **************************
                    Dim whaEng As New Reports.RepAverageServiceTimeComparingWithKPIENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        whaEng.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " KPI Shop T " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    'If cbD.Checked Then
                    '    Application.DoEvents()
                    '    whaEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                    '    TextBox1.Text = currDate.ToShortDateString & " KPI Shop D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    'If cbW.Checked Then
                    '    Application.DoEvents()
                    '    whaEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                    '    TextBox1.Text = currDate.ToShortDateString & " KPI Shop W " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        whaEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " KPI Shop M " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    'If cbY.Checked Then
                    '    Application.DoEvents()
                    '    whaEng.ProcReportByYear(ServiceDate)
                    '    TextBox1.Text = currDate.ToShortDateString & " KPI Shop Y " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    'End If
                    whaEng = Nothing
                End If

                If cbKPIst.Checked = True Then
                    '*********************** KPI Staff **************************
                    Dim agksEng As New Reports.RepAverageServiceTimeComparingWithKPI_byStaffENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        agksEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " KPI Staff D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    ''agksEng.ProcReportByTime(ServiceDate)
                    ''agksEng.ProcReportByWeek(ServiceDate)
                    ''agksEng.ProcReportByMonth(ServiceDate)
                    ''agksEng.ProcReportByYear(ServiceDate)
                    agksEng = Nothing
                End If

                If cbPro.Checked = True Then
                    '********************** Productivity ************************
                    Dim pdEng As New Reports.ReportsProductivityENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        pdEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " Productivity D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    ''pdEng.ProcReportByWeek(ServiceDate)
                    ''pdEng.ProcReportByMonth(ServiceDate)
                    ''pdEng.ProcReportByYear(ServiceDate)
                    pdEng = Nothing
                End If

                'If cbCa.Checked = True Then
                '    '************************* Capacity ****************************
                '    Dim caEng As New Reports.ReportsCapacityReportByShopENG
                '    If cbD.Checked Then
                '        Application.DoEvents()
                '        caEng.ProcReportByDate(ServiceDate)
                '        TextBox1.Text = currDate.ToShortDateString & " Capacity D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                '    End If
                '    ''caEng.ProcReportByWeek(ServiceDate)
                '    ''caEng.ProcReportByMonth(ServiceDate)
                '    ''caEng.ProcReportByYear(ServiceDate)
                '    ''caEng.ProcReportByTime(ServiceDate)
                'End If

                If cbSt.Checked = True Then
                    '************************* Staff Attendance ****************************
                    Dim staEng As New Reports.ReportsStaffAttendanceENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        staEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        TextBox1.Text = currDate.ToShortDateString & " Staff Attendance D " & dr("shop_abb") & vbNewLine & TextBox1.Text
                    End If
                    staEng = Nothing
                End If

                currDate = DateAdd(DateInterval.Day, 1, currDate)
            Loop While currDate <= endDate
        Next

        MsgBox("Complete")

        SetAllEnable(True)
    End Sub

    Private Sub SetAllEnable(ByVal status As Boolean)
        btnTest.Enabled = status
        cmbShop.Enabled = status
        Button1.Enabled = status
        Button2.Enabled = status

        cbSeg.Enabled = status
        cbNet.Enabled = status
        cbWTsh.Enabled = status
        cbWTst.Enabled = status
        cbKPIsh.Enabled = status
        cbKPIst.Enabled = status
        cbPro.Enabled = status
        cbSt.Enabled = status

        cbT.Enabled = status
        cbD.Enabled = status
        cbW.Enabled = status
        cbM.Enabled = status
        cbY.Enabled = status
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        cbSeg.Checked = False
        cbNet.Checked = False
        cbWTsh.Checked = False
        cbWTsk.Checked = False
        cbWTst.Checked = False
        cbKPIsh.Checked = False
        cbKPIst.Checked = False
        cbCa.Checked = False
        cbPro.Checked = False
        cbSt.Checked = False
        cbT.Checked = False
        cbD.Checked = False
        cbW.Checked = False
        cbM.Checked = False
        cbY.Checked = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SetAllEnable(False)
        Dim eng As New Engine.WorkForceENG
        eng.SetTextLog(txtLog)

        Dim ServiceDate As Date = dtpST.Value
        Dim StartTime As String = "08:30"
        Dim EndTime As String = "09:00"
        For i As Int32 = 1 To 26
            StartTime = DateAdd(DateInterval.Minute, 30, CDate(StartTime)).ToShortTimeString.PadLeft(5, "0")
            EndTime = DateAdd(DateInterval.Minute, 30, CDate(EndTime)).ToShortTimeString.PadLeft(5, "0")
            eng.WFStartProcess(ServiceDate, StartTime, EndTime, lblTime)
        Next
        eng = Nothing

        MessageBox.Show("Add Workforce Complete!!!")
        SetAllEnable(True)
    End Sub

    Private Sub frmHQAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetActiveShop

        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "shop_name_en"
            dt = dt.DefaultView.ToTable

            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("shop_name_en") = "All Shop"
            dt.Rows.InsertAt(dr, 0)

            cmbShop.ValueMember = "id"
            cmbShop.DisplayMember = "shop_name_en"
            cmbShop.DataSource = dt
        End If


    End Sub
End Class