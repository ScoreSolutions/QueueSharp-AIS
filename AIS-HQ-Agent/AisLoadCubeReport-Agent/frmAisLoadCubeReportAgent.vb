Imports System.IO
Imports Engine
Imports System.Data

Public Class frmAisLoadCubeReportAgent
    'Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

    Private Sub tmCutOffData_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCutOffData.Tick
        tmCutOffData.Enabled = False
        Dim eng As New Engine.CutOffDataENG
        eng.SetTextLog(txtLog)
        eng.CutOffData(lblTime, True)
        eng = Nothing

        tmCutOffData.Enabled = True
        'Application.Exit()
    End Sub

    Private Sub frmHQAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "AIS Cut off Data Agent V" & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        SetAllEnable(False)

        Dim dt As New DataTable
        If lstShop.SelectedItems.Count = 0 Then
            dt = Engine.Common.FunctionEng.GetActiveShop
            dt.DefaultView.Sort = "shop_name_en"
            dt = dt.DefaultView.ToTable
        Else
            'dt.Columns.Add("id")
            For Each lst As DataRowView In lstShop.SelectedItems
                Dim tmpDt As New DataTable
                tmpDt = Engine.Common.FunctionEng.GetShopDtByID(lst("id"))
                dt.Merge(tmpDt)
                tmpDt.Dispose()
            Next
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
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Segment T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        seENG.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Segment D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        seENG.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Segment W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        seENG.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Segment M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    seENG = Nothing
                End If

                If cbNet.Checked = True Then
                    '************************* Network Type ****************************
                    Dim ntENG As New Reports.ReportsCustByNetworkTypeENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        ntENG.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Network Type T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        ntENG.ProcReportByDay(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Network Type D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        ntENG.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Network Type W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        ntENG.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Network Type M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    ntENG = Nothing
                End If

                If cbCat.Checked = True Then
                    '************************* Category ****************************
                    Dim csENG As New Reports.ReportsCustByCategoryENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        csENG.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Cat/Sub Cate T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        csENG.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Cat/Sub Cate D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        csENG.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Cat/Sub Cate W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        csENG.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Cat/Sub Cate M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    csENG = Nothing
                End If

                If cbWTsh.Checked = True Then
                    '************************* WT & HT Shop ****************************
                    Dim whsEng As New Reports.ReportsWaitingTimeHandlingTimeByShopENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        whsEng.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Shop T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        whsEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Shop D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        whsEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Shop W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        whsEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Shop M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    whsEng = Nothing
                End If

                If cbWTsk.Checked = True Then
                    '********************* WT & HT Skill **********************
                    Dim whskEng As New Reports.ReportsWaitingTimeHandlingTimeBySkillENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        whskEng.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Skill T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        whskEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Skill D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        whskEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Skill W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        whskEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Skill M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    whskEng = Nothing
                End If

                If cbWTst.Checked = True Then
                    '******************** WT & HT Agent ***********************
                    Dim wtaEng As New Reports.ReportsWaitingTimeHandlingTimeByAgentENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        wtaEng.ProcessReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " WT & HT Staff T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        wtaEng.ProcessReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & "WT & HT Staff D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        wtaEng.ProcessReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & "WT & HT Staff W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        wtaEng.ProcessReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & "WT & HT Staff M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If

                    wtaEng = Nothing
                End If

                If cbKPIsh.Checked = True Then
                    '*********************** KPI Shop **************************
                    Dim whaEng As New Reports.RepAverageServiceTimeComparingWithKPIENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        whaEng.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Shop T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        whaEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Shop D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        whaEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Shop W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        whaEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Shop M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    whaEng = Nothing
                End If

                If cbKPIst.Checked = True Then
                    '*********************** KPI Staff **************************
                    Dim agksEng As New Reports.RepAverageServiceTimeComparingWithKPI_byStaffENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        agksEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Staff D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        agksEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Staff W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        agksEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " KPI Staff M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    agksEng = Nothing
                End If

                If cbPro.Checked = True Then
                    '********************** Productivity ************************
                    Dim pdEng As New Reports.ReportsProductivityENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        pdEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Productivity D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked = True Then
                        Application.DoEvents()
                        pdEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Productivity W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        pdEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Productivity M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    pdEng = Nothing
                End If

                If cbCa.Checked = True Then
                    '************************* Capacity ****************************
                    Dim caEng As New Reports.ReportsCapacityReportByShopENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        caEng.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Capacity T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        caEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Capacity D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        caEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Capacity W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        caEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Capacity M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    caEng = Nothing
                End If

                If cbSt.Checked = True Then
                    '************************* Staff Attendance ****************************
                    Dim staEng As New Reports.ReportsStaffAttendanceENG
                    If cbD.Checked Then
                        Application.DoEvents()
                        staEng.ProcReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Staff Attendance D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        staEng.ProcReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Staff Attendance W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        staEng.ProcReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Staff Attendance M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    staEng = Nothing
                End If

                If cbAppointShop.Checked = True Then
                    '************************* Appointment By Shop ****************************
                    Dim cAppSh As New Reports.ReportsAppointmentByShopENG
                    If cbT.Checked Then
                        Application.DoEvents()
                        cAppSh.ProcessReportByTime(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Appointment By Shop T " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbD.Checked Then
                        Application.DoEvents()
                        cAppSh.ProcessReportByDate(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Appointment By Shop D " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbW.Checked Then
                        Application.DoEvents()
                        cAppSh.ProcessReportByWeek(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Appointment By Shop W " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If
                    If cbM.Checked Then
                        Application.DoEvents()
                        cAppSh.ProcessReportByMonth(ServiceDate, dr("id"), lblTime)
                        txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Appointment By Shop M " & dr("shop_abb") & vbNewLine & txtLog.Text
                    End If

                    cAppSh = Nothing
                End If


                If cbIntervalPerformance.Checked Then
                    '************************* Interval Performance ****************************
                    Dim sumIntervalENG As New Reports.ReportsIntervalPerformanceENG
                    Application.DoEvents()
                    sumIntervalENG.ProcReportByTime(ServiceDate, dr("id"), lblTime)
                    txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Summary Interval " & dr("shop_abb") & vbNewLine & txtLog.Text
                    sumIntervalENG = Nothing
                End If


                'If cbSumDairy.Checked Then
                '    '************************* Summary Dairy ****************************
                '    Dim sumDairyENG As New Reports.ReportSummaryDairyENG
                '    Application.DoEvents()
                '    sumDairyENG.ProcReport(ServiceDate, dr("id"), lblTime)
                '    txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Summary Daily " & dr("shop_abb") & vbNewLine & txtLog.Text
                '    sumDairyENG = Nothing
                'End If



                'If cbSumStaff.Checked Then
                '    '************************* Summary Staff ****************************
                '    Dim sumStaffENG As New Reports.ReportSummaryStaffENG
                '    Application.DoEvents()
                '    sumStaffENG.ProcReport(ServiceDate, dr("id"), lblTime)
                '    txtLog.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " #### " & currDate.ToShortDateString & " Summary Staff " & dr("shop_abb") & vbNewLine & txtLog.Text
                '    sumStaffENG = Nothing
                'End If

                currDate = DateAdd(DateInterval.Day, 1, currDate)
            Loop While currDate <= endDate
        Next

        MsgBox("Complete")

        SetAllEnable(True)
    End Sub

    Private Sub SetAllEnable(ByVal status As Boolean)
        btnTest.Enabled = status
        lstShop.Enabled = status
        Button1.Enabled = status

        cbSeg.Enabled = status
        cbNet.Enabled = status
        cbCat.Enabled = status
        cbWTsh.Enabled = status
        cbWTst.Enabled = status
        cbWTsk.Enabled = status
        cbKPIsh.Enabled = status
        cbKPIst.Enabled = status
        cbPro.Enabled = status
        cbSt.Enabled = status
        cbCa.Enabled = status
        cbAppointShop.Enabled = status
        cbSumDairy.Enabled = status
        cbIntervalPerformance.Enabled = status
        cbSumStaff.Enabled = status

        cbT.Enabled = status
        cbD.Enabled = status
        cbW.Enabled = status
        cbM.Enabled = status
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtLog.Text = ""
    End Sub

    Private Sub frmAisCutOffDataAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetActiveShop

        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "shop_name_en"
            dt = dt.DefaultView.ToTable

            lstShop.ValueMember = "id"
            lstShop.DisplayMember = "shop_name_en"
            lstShop.DataSource = dt
            lstShop.ClearSelected()
        End If

        tmCutOffData.Enabled = True
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmAisCutOffDataAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS Cut off Data Agent V" & getMyVersion()
        NotifyIcon1.Text = "AIS Cut off Data Agent V" & getMyVersion()
    End Sub
    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub

    Private Sub tmTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTime.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
    End Sub
End Class