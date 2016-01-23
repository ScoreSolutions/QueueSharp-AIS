Imports Cen = CenLinqDB.TABLE
Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities.Constant
Imports System.Threading

Public Class CutOffDataENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox
    Dim _RecCountAfter As Integer = 0

    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    Public Sub SetTextLog(ByVal txtLog As TextBox)
        _TXT_LOG = txtLog
    End Sub

    Dim _OldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If _OldLog <> LogDesc Then
            _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
            Application.DoEvents()
        End If
    End Sub


    Public Sub CutOffData(ByVal lblTime As Label, ByVal IsLoadCube As Boolean)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim dtDate As New DataTable   'Date for Process Reports
            dtDate.Columns.Add("service_date", GetType(DateTime))
            dtDate.Columns.Add("show_date")
            dtDate.Columns.Add("shop_id")

            Dim dt As DataTable = FunctionEng.GetActiveShop()
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "shop_abb"
                dt = dt.DefaultView.ToTable
                dt.Columns.Add("TbShopCenLinqDB", GetType(Cen.TbShopCenLinqDB))

                Dim cEng As New CompareDataENG
                '#########################################
                '###########  Cut off data ###############
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Application.DoEvents()
                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")

                    Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))
                    Application.DoEvents()
                    dt.Rows(i)("TbShopCenLinqDB") = shLnq

                    ProcessCutOffData(trans, shLnq, lblTime, dtDate)
                    cEng.SaveTempAfterCutoff(shLnq.ID, DateAdd(DateInterval.Day, -1, DateTime.Now), _RecCountAfter)
                    shLnq = Nothing
                Next
                '#########################################

                If IsLoadCube = True Then
                    LoadCubeAllReports(dt, dtDate, lblTime)
                End If


                '#########################################
                '###########  Cut off Logfile ###############
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim shLnq As Cen.TbShopCenLinqDB = DirectCast(dr("TbShopCenLinqDB"), Cen.TbShopCenLinqDB)
                    ProcessLogfileHistory(trans, shLnq, lblTime)
                    shLnq = Nothing
                Next
                '#########################################

                dtDate.Dispose()
                dt.Dispose()
            End If
            trans.CommitTransaction()

            If FunctionEng.GetQisDBConfig("IS_DELETE_TRANS_LOG") = "Y" Then
                DeleteTransLog()
            End If
        End If
    End Sub

    Public Sub LoadCubeAllReports(ByVal dt As DataTable, ByVal dtDate As DataTable, ByVal lblTime As Label)
        'Cut off data ให้เสร็จหมดทุก Shop ก่อน แล้วค่อยมา Load Cube Report
        If FunctionEng.GetQisDBConfig("HQ-AGENT_LOAD_ALL_REPORT") = "Y" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim shLnq As Cen.TbShopCenLinqDB = DirectCast(dr("TbShopCenLinqDB"), Cen.TbShopCenLinqDB) ' FunctionEng.GetTbShopCenLinqDB(dr("id"))

                dtDate.DefaultView.RowFilter = "shop_id = '" & shLnq.ID & "'"
                For Each drDate As DataRowView In dtDate.DefaultView
                    Dim drServiceDate As DateTime = Convert.ToDateTime(drDate("service_date"))
                    Dim tmpDate As DateTime = New DateTime(drServiceDate.Year, drServiceDate.Month, drServiceDate.Day)
                    ProcessAllReportByShop(shLnq, tmpDate, lblTime)
                Next
                dtDate.DefaultView.RowFilter = ""
                shLnq = Nothing
            Next


            ' ''###########################################
            ''ส่ง Mail Alert กรณี Load Cube ไม่ครบทุก Report
            'Dim dDate As DataTable = dtDate.DefaultView.ToTable(True, "service_date").Copy
            'If dDate.Rows.Count > 0 Then
            '    Dim CurrDate As New Date(1, 1, 1)
            '    For Each rDate As DataRow In dDate.Rows
            '        If CurrDate.ToString("yyyyMMdd") <> Convert.ToDateTime(rDate("service_date")).ToString("yyyyMMdd") Then
            '            CurrDate = New Date(Convert.ToDateTime(rDate("service_date")).Year, Convert.ToDateTime(rDate("service_date")).Month, Convert.ToDateTime(rDate("service_date")).Day)
            '            CheckCubeReportsProcessAllShop(CurrDate, dt)
            '        End If
            '    Next
            '    dDate.Dispose()
            'End If
            ' ''###########################################
        End If
    End Sub



    Private Sub DeleteTransLog()
        'Delete Trans Log from QisDB.LOG_TRANS
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim transAfter As String = DateAdd(DateInterval.Day, 0 - Convert.ToInt16(FunctionEng.GetQisDBConfig("DELETE_TRANS_LOG_AFTER_DAY")), DateTime.Now).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from LOG_TRANS where convert(varchar(8),trans_date,112) <= '" & transAfter & "'", trans.Trans)
        trans.CommitTransaction()
    End Sub

    Private Sub ProcessAllReportByShop(ByVal shLnq As Cen.TbShopCenLinqDB, ByVal ServiceDate As DateTime, ByVal lblTime As Label)

        'Dim seENG As New Reports.ReportsCustBySegmentENG
        'seENG.ServiceDate = ServiceDate
        'seENG.ShopID = shLnq.ID
        'seENG.lblTime = lblTime
        'Dim seThread As New Thread(AddressOf seENG.SegmentProcessAllReport)
        'seThread.Start()

        'Dim ntENG As New Reports.ReportsCustByNetworkTypeENG
        'ntENG.ServiceDate = ServiceDate
        'ntENG.ShopID = shLnq.ID
        'ntENG.lblTime = lblTime
        'Dim ntThread As New Thread(AddressOf ntENG.NetworkTypeProcessAllReport)
        'ntThread.Start()

        'Dim ctEng As New Reports.ReportsCustByCategoryENG
        'ctEng.ServiceDate = ServiceDate
        'ctEng.ShopID = shLnq.ID
        'ctEng.lblTime = lblTime
        'Dim ctThread As New Thread(AddressOf ctEng.CustByCategoryProcessAllReport)
        'ctThread.Start()

        'Dim whsEng As New Reports.ReportsWaitingTimeHandlingTimeByShopENG
        'whsEng.ServiceDate = ServiceDate
        'whsEng.ShopID = shLnq.ID
        'whsEng.lblTime = lblTime
        'Dim whsThread As New Thread(AddressOf whsEng.ShopWaitingTimeHandlingTimeProcessAllReport)
        'whsThread.Start()

        'Dim wtaEng As New Reports.ReportsWaitingTimeHandlingTimeByAgentENG
        'wtaEng.ServiceDate = ServiceDate
        'wtaEng.ShopID = shLnq.ID
        'wtaEng.lblTime = lblTime
        'Dim wtaThread As New Thread(AddressOf wtaEng.AgentWaitingTimeHandlingTimeProcessAllReport)
        'wtaThread.Start()

        'Dim whaEng As New Reports.RepAverageServiceTimeComparingWithKPIENG
        'whaEng.ServiceDate = ServiceDate
        'whaEng.ShopID = shLnq.ID
        'whaEng.lblTime = lblTime
        'Dim whaThread As New Thread(AddressOf whaEng.ShopAVGCompareKPIProcessAllReport)
        'whaThread.Start()

        'Dim agksEng As New Reports.RepAverageServiceTimeComparingWithKPI_byStaffENG
        'agksEng.ServiceDate = ServiceDate
        'agksEng.ShopID = shLnq.ID
        'agksEng.lblTime = lblTime
        'Dim agksThread As New Thread(AddressOf agksEng.AgentAVGCompareKPIProcessAllReport)
        'agksThread.Start()

        'Dim pdEng As New Reports.ReportsProductivityENG
        'pdEng.ServiceDate = ServiceDate
        'pdEng.ShopID = shLnq.ID
        'pdEng.lblTime = lblTime
        'Dim pdThread As New Thread(AddressOf pdEng.ProductivityProcessAllReport)
        'pdThread.Start()

        'Dim caEng As New Reports.ReportsCapacityReportByShopENG
        'caEng.ServiceDate = ServiceDate
        'caEng.ShopID = shLnq.ID
        'caEng.lblTime = lblTime
        'Dim caThread As New Thread(AddressOf caEng.CapacityReportProcessAllReport)
        'caThread.Start()

        'Dim staEng As New Reports.ReportsStaffAttendanceENG
        'staEng.ServiceDate = ServiceDate
        'staEng.ShopID = shLnq.ID
        'staEng.lblTime = lblTime
        'Dim staThread As New Thread(AddressOf staEng.StaffAttendanceProcessAllReport)
        'staThread.Start()

        'Dim appShEng As New Reports.ReportsAppointmentByShopENG
        'appShEng.ServiceDate = ServiceDate
        'appShEng.ShopID = shLnq.ID
        'appShEng.lblTime = lblTime
        'Dim appShThread As New Thread(AddressOf appShEng.ShopAppointmentProcessAllReport)
        'appShThread.Start()

        'Dim SumDairyEng As New Reports.ReportSummaryDairyENG
        'SumDairyEng.ServiceDate = ServiceDate
        'SumDairyEng.ShopID = shLnq.ID
        'SumDairyEng.lblTime = lblTime
        'Dim SumDairyThread As New Thread(AddressOf SumDairyEng.SummaryDailyProcessAllReport)
        'SumDairyThread.Start()

        'Dim SumIntervalENG As New Reports.ReportSummaryIntervalENG
        'SumIntervalENG.ServiceDate = ServiceDate
        'SumIntervalENG.ShopID = shLnq.ID
        'SumIntervalENG.lblTime = lblTime
        'Dim SumIntervalThread As New Thread(AddressOf SumIntervalENG.SummaryIntervalProcessAllReport)
        'SumIntervalThread.Start()

        'Dim SumStaffENG As New Reports.ReportSummaryStaffENG
        'SumStaffENG.ServiceDate = ServiceDate
        'SumStaffENG.ShopID = shLnq.ID
        'SumStaffENG.lblTime = lblTime
        'Dim SumStaffThread As New Thread(AddressOf SumStaffENG.SummaryStaffProcessAllReport)
        'SumStaffThread.Start()


        FunctionEng.SaveTransLog("CutOffDataENG.ProcessAllReport", "Shop : " & shLnq.SHOP_ABB & " : Start Process All Report Date " & ServiceDate.ToString("yyyy-MM-dd"))
        Dim seENG As New Reports.ReportsCustBySegmentENG
        seENG.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        seENG.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        seENG.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        seENG.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        seENG = Nothing

        Dim ntENG As New Reports.ReportsCustByNetworkTypeENG
        ntENG.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        ntENG.ProcReportByDay(ServiceDate, shLnq.ID, lblTime)
        ntENG.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        ntENG.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        ntENG = Nothing

        Dim ctEng As New Reports.ReportsCustByCategoryENG
        ctEng.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        ctEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        ctEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        ctEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        ctEng = Nothing

        Dim whsEng As New Reports.ReportsWaitingTimeHandlingTimeByShopENG
        whsEng.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        whsEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        whsEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        whsEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        whsEng = Nothing

        Dim wtaEng As New Reports.ReportsWaitingTimeHandlingTimeByAgentENG
        wtaEng.ProcessReportByTime(ServiceDate, shLnq.ID, lblTime)
        wtaEng.ProcessReportByDate(ServiceDate, shLnq.ID, lblTime)
        wtaEng.ProcessReportByWeek(ServiceDate, shLnq.ID, lblTime)
        wtaEng.ProcessReportByMonth(ServiceDate, shLnq.ID, lblTime)
        wtaEng = Nothing

        ''''########## Report By Skill ไม่มีแล้ว   ###########'
        'Dim whskEng As New Reports.ReportsWaitingTimeHandlingTimeBySkillENG
        'whskEng.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        'whskEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        'whskEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        'whskEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        'whskEng = Nothing

        Dim whaEng As New Reports.RepAverageServiceTimeComparingWithKPIENG
        whaEng.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        whaEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        whaEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        whaEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        whaEng = Nothing

        Dim agksEng As New Reports.RepAverageServiceTimeComparingWithKPI_byStaffENG
        agksEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        agksEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        agksEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        agksEng = Nothing

        Dim pdEng As New Reports.ReportsProductivityENG
        pdEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        pdEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        pdEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        pdEng = Nothing

        Dim caEng As New Reports.ReportsCapacityReportByShopENG
        caEng.ProcReportByTime(ServiceDate, shLnq.ID, lblTime)
        caEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        caEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        caEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        caEng = Nothing

        Dim staEng As New Reports.ReportsStaffAttendanceENG
        staEng.ProcReportByDate(ServiceDate, shLnq.ID, lblTime)
        staEng.ProcReportByWeek(ServiceDate, shLnq.ID, lblTime)
        staEng.ProcReportByMonth(ServiceDate, shLnq.ID, lblTime)
        staEng = Nothing

        Dim appShEng As New Reports.ReportsAppointmentByShopENG
        appShEng.ProcessReportByTime(ServiceDate, shLnq.ID, lblTime)
        appShEng.ProcessReportByDate(ServiceDate, shLnq.ID, lblTime)
        appShEng.ProcessReportByWeek(ServiceDate, shLnq.ID, lblTime)
        appShEng.ProcessReportByMonth(ServiceDate, shLnq.ID, lblTime)
        appShEng = Nothing

        Dim SumDairyEng As New Reports.ReportSummaryDairyENG
        SumDairyEng.ProcReport(ServiceDate, shLnq.ID, lblTime)
        SumDairyEng = Nothing

        Dim SumIntervalENG As New Reports.ReportSummaryIntervalENG
        SumIntervalENG.ProcReport(ServiceDate, shLnq.ID, lblTime)
        SumIntervalENG = Nothing

        Dim SumStaffENG As New Reports.ReportSummaryStaffENG
        SumStaffENG.ProcReport(ServiceDate, shLnq.ID, lblTime)
        SumStaffENG = Nothing

        FunctionEng.SaveTransLog("CutOffDataENG.ProcessAllReport", "Shop : " & shLnq.SHOP_ABB & " : Finish Process All Report Date " & ServiceDate.ToString("yyyy-MM-dd"))
    End Sub

    Private Sub ProcessAllReport(ByVal ServiceDate As DateTime, ByVal lblTime As Label)
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetActiveShop
        For Each dr As DataRow In dt.Rows
            Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))
            ProcessAllReportByShop(shLnq, ServiceDate, lblTime)
            shLnq = Nothing
        Next
    End Sub

#Region "Process Check Data"
    Public Sub CheckCubeReportsProcessByShopID(ByVal DateFrom As DateTime, ByVal DateTo As DateTime, ByVal ShopID As Long)
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        Dim CurrDate As DateTime = DateFrom
        Do
            _MailCubeReportProcess = ""
            CheckCubeReportsProcess(CurrDate, shLnq)

            If _MailCubeReportProcess.Trim <> "" Then
                SendMailErrorCubeReport(_MailCubeReportProcess, CurrDate)
            End If

            CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
        Loop While CurrDate <= DateTo
        shLnq = Nothing
    End Sub

    Private Function CheckCubeByReport(ByVal strTmp As String, ByVal pDt As DataTable, ByVal ShopAbb As String, ByVal ReportName As String) As String
        'Dim strTmp As String = ""

        pDt.DefaultView.RowFilter = "report_name = '" & ShopAbb & " : " & ReportName & "'"
        If pDt.DefaultView.Count > 0 Then
            If Convert.IsDBNull(pDt.DefaultView(0)("proc_end_date")) = True Then
                If strTmp.Trim = "" Then
                    strTmp = ReportName
                Else
                    strTmp += ", " & ReportName
                End If
            End If
        Else
            If strTmp.Trim = "" Then
                strTmp = ReportName
            Else
                strTmp += ", " & ReportName
            End If
        End If

        Return strTmp
    End Function

    Private Sub CheckCubeReportsProcess(ByVal ServiceDate As DateTime, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim pDt As New DataTable
        Dim pLnq As New CenLinqDB.TABLE.TbReportProcessLogCenLinqDB
        Dim pDate As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim wh As String = "SUBSTRING(report_name,0,CHARINDEX(':',report_name))='" & shLnq.SHOP_ABB & "' "
        wh += " and convert(varchar(8),service_date,112)='" & pDate & "'"
        pDt = pLnq.GetDataList(wh, "proc_start_date desc", trans.Trans)
        trans.CommitTransaction()

        Dim strTmp As String = ""
        If pDt.Rows.Count > 0 Then
            'ต้อง Check เป็นราย Reports
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.CustomerBySegmentByTime)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.CustomerByNetworkTypeByTime)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.AverageServiceTimeComparingWidthKPIStaffByDate)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.AverageServiceTimeComparingWidthKPIByTime)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.WaitingTimeAndHandlingTimeReportByAgentByTime)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.WaitingTimeHandlingTimeByShopByTime)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.ProductivityReportByDate)
            strTmp = CheckCubeByReport(strTmp, pDt, shLnq.SHOP_ABB, ReportName.ReportsStaffAttendanceReport)
        Else
            strTmp = "Cube report Unload at <b>" & shLnq.SHOP_ABB & "</b>"
        End If
        pDt = Nothing
        pLnq = Nothing

        If strTmp.Trim <> "" Then
            _MailCubeReportProcess += "<br />" & "Cube report Upload incompleted at <b>" & shLnq.SHOP_NAME_EN & "</b> (" & strTmp & ")"
        End If
    End Sub

    Public Sub CheckCubeReportsProcessAllShop(ByVal ServiceDate As DateTime, ByVal ShopListDT As DataTable)
        _MailCubeReportProcess = ""
        If ShopListDT.Rows.Count > 0 Then
            If ShopListDT.Columns.Contains("TbShopCenLinqDB") = False Then
                ShopListDT.Columns.Add("TbShopCenLinqDB", GetType(Cen.TbShopCenLinqDB))
            End If

            For Each dr As DataRow In ShopListDT.Rows
                Dim shLnq As New Cen.TbShopCenLinqDB
                If Convert.IsDBNull(dr("TbShopCenLinqDB")) = False Then
                    shLnq = DirectCast(dr("TbShopCenLinqDB"), Cen.TbShopCenLinqDB)
                Else
                    shLnq = FunctionEng.GetTbShopCenLinqDB(dr("id"))
                End If

                CheckCubeReportsProcess(ServiceDate, shLnq)
                shLnq = Nothing
            Next
        End If
        If _MailCubeReportProcess.Trim <> "" Then
            SendMailErrorCubeReport(_MailCubeReportProcess, ServiceDate)
        End If
    End Sub

    Dim _MailCubeReportProcess As String = ""
    Public ReadOnly Property MailCubeReportProcess() As String
        Get
            Return _MailCubeReportProcess.Trim()
        End Get
    End Property

    Private Sub SendMailErrorCubeReport(ByVal MailMsg As String, ByVal MailDate As DateTime)
        Dim dt As New DataTable
        Dim lnq As New CenLinqDB.TABLE.TbEmailBatchAlertCenLinqDB
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        dt = lnq.GetDataList("convert(varchar(8),getdate(),112) between convert(varchar(8),ef_date,112) and convert(varchar(8),isnull(ep_date,getdate()),112) ", "", trans.Trans)
        Dim MailSjb As String = "Cube Report Upload incompleted on " & MailDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        PrintLog(MailMsg.Replace("<br />", vbCrLf))

        If dt.Rows.Count > 0 Then
            Dim LoopStep As Integer = 50
            Dim LoopCount As Integer = Math.Ceiling(dt.Rows.Count / LoopStep)
            For i As Integer = 0 To LoopCount - 1
                Dim MailTo As String = ""
                For j As Integer = 0 To LoopStep - 1
                    Dim RowIndex As Integer = j + (LoopStep * i)
                    If RowIndex < dt.Rows.Count Then
                        Dim dr As DataRow = dt.Rows(RowIndex)
                        If MailTo = "" Then
                            MailTo = dr("email_addr").ToString
                        Else
                            MailTo += ";" & dr("email_addr").ToString
                        End If
                    End If
                Next

                Dim gw As New Engine.GateWay.GateWayServiceENG
                gw.SendEmail(MailTo, MailSjb, MailMsg)
                gw = Nothing
            Next
        End If
        dt = Nothing
        trans.CommitTransaction()
        lnq = Nothing
    End Sub
#End Region

    Private Sub ProcessLogfileHistory(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal lblTime As Label)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "CutOffDatENG.ProcessLogfileHistory")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetLogFileDT(shTrans)
            If dt.Rows.Count > 0 Then
                FunctionEng.SaveTransLog("CutOffDataENG.ProcessLogfileHistory", "Start Cut off Logfile on Shop " & shLnq.SHOP_ABB & "### " & dt.Rows.Count & " Record(s)")
                Dim recCnt As Integer = 0
                For i As Integer = 0 To dt.Rows.Count - 1
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "CutOffDatENG.ProcessLogfileHistory")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dr As DataRow = dt.Rows(i)
                        Dim src As New ShLinqDB.TABLE.TbLogfileShLinqDB

                        Dim dst As New ShLinqDB.TABLE.TbLogfileHistoryShLinqDB
                        If Convert.IsDBNull(dr("CQ_ID")) = False Then dst.CQ_ID = Convert.ToInt64(dr("CQ_ID"))
                        If Convert.IsDBNull(dr("LOG_DATE")) = False Then dst.LOG_DATE = Convert.ToDateTime(dr("LOG_DATE"))
                        If Convert.IsDBNull(dr("QUEUE_NO")) = False Then dst.QUEUE_NO = dr("QUEUE_NO")
                        If Convert.IsDBNull(dr("CUSTOMER_ID")) = False Then dst.CUSTOMER_ID = dr("CUSTOMER_ID")
                        If Convert.IsDBNull(dr("USER_ID")) = False Then dst.USER_ID = Convert.ToInt16(dr("USER_ID"))
                        If Convert.IsDBNull(dr("ITEM_ID")) = False Then dst.ITEM_ID = Convert.ToInt16(dr("ITEM_ID"))
                        If Convert.IsDBNull(dr("COUNTER_ID")) = False Then dst.COUNTER_ID = Convert.ToInt16(dr("COUNTER_ID"))
                        If Convert.IsDBNull(dr("FLAG")) = False Then dst.FLAG = dr("FLAG")
                        If Convert.IsDBNull(dr("STATUS")) = False Then dst.STATUS = Convert.ToInt16(dr("STATUS"))
                        If Convert.IsDBNull(dr("ID")) = False Then dst.TB_LOG_FILE_ID = Convert.ToInt64(dr("ID"))
                        If dst.InsertData("CutOffLogfile", shTrans.Trans) = True Then
                            If src.DeleteByPK(Convert.ToInt64(dr("ID")), shTrans.Trans) = True Then
                                shTrans.CommitTransaction()
                                recCnt += 1
                            Else
                                shTrans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("CutOffDataENG.ProcessLogfileHistory", dst.ErrorMessage & " TB_LOGFILE.id=" & Convert.ToInt64(dr("ID")) & " ServiceDate: " & Convert.ToDateTime(dr("LOG_DATE")).ToString("dd/MM/yyyy"), Application.StartupPath & "\ErrorLog\", "CutOffDataENG")
                            End If
                        Else
                            shTrans.RollbackTransaction()
                            FunctionEng.SaveErrorLog("CutOffDataENG.ProcessLogfileHistory", dst.ErrorMessage & " TB_LOGFILE.id=" & Convert.ToInt64(dr("ID")) & " log_date: " & Convert.ToDateTime(dr("LOG_DATE")).ToString("dd/MM/yyyy"), Application.StartupPath & "\ErrorLog\", "CutOffDataENG")
                        End If
                        src = Nothing
                        dst = Nothing

                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                        Application.DoEvents()
                    End If
                Next
                FunctionEng.SaveTransLog("CutOffDataENG.ProcessLogfileHistory", "End Cut off Log File on Shop " & shLnq.SHOP_ABB & "### " & recCnt & " Record(s)")
                dt.Dispose()
                dt = Nothing
            End If
        End If
    End Sub


    Private Sub ProcessCutOffData(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal lblTime As Label, ByVal ServiceDate As DataTable)
        _RecCountAfter = 0
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "CutOffDataENG.ProcessCutOffData")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_counter_queue", "service_date", shTrans)
            If dt.Rows.Count > 0 Then
                FunctionEng.SaveTransLog("CutOffDataENG.ProcessCutOffData", "Start Cut off data on Shop " & shLnq.SHOP_ABB & " ### " & dt.Rows.Count & " Record(s)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim src As New ShLinqDB.TABLE.TbCounterQueueShLinqDB

                    ServiceDate.DefaultView.RowFilter = "show_date = '" & Convert.ToDateTime(dr("service_date")).ToString("yyyy-MM-dd") & "' and shop_id = '" & shLnq.ID & "' "
                    If ServiceDate.DefaultView.Count > 0 Then
                    Else
                        Dim dr1 As DataRow = ServiceDate.NewRow
                        dr1("service_date") = Convert.ToDateTime(dr("service_date"))
                        dr1("show_date") = Convert.ToDateTime(dr("service_date")).ToString("yyyy-MM-dd")
                        dr1("shop_id") = shLnq.ID
                        ServiceDate.Rows.Add(dr1)
                    End If

                    Dim dst As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
                    dst.QUEUE_NO = dr("QUEUE_NO")
                    If Convert.IsDBNull(dr("CUSTOMER_ID")) = False Then dst.CUSTOMER_ID = dr("CUSTOMER_ID")
                    If Convert.IsDBNull(dr("CUSTOMER_NAME")) = False Then dst.CUSTOMER_NAME = dr("CUSTOMER_NAME")
                    If Convert.IsDBNull(dr("SEGMENT")) = False Then dst.SEGMENT = dr("SEGMENT")
                    If Convert.IsDBNull(dr("CUSTOMERTYPE_ID")) = False Then dst.CUSTOMERTYPE_ID = Convert.ToInt64(dr("CUSTOMERTYPE_ID"))
                    If Convert.IsDBNull(dr("CUSTOMERTYPE_NAME")) = False Then dst.CUSTOMERTYPE_NAME = dr("CUSTOMERTYPE_NAME")
                    If Convert.IsDBNull(dr("ITEM_ID")) = False Then dst.ITEM_ID = Convert.ToInt64(dr("ITEM_ID"))
                    If Convert.IsDBNull(dr("ITEM_NAME")) = False Then dst.ITEM_NAME = dr("ITEM_NAME")
                    If Convert.IsDBNull(dr("COUNTER_ID")) = False Then dst.COUNTER_ID = Convert.ToInt64(dr("COUNTER_ID"))
                    If Convert.IsDBNull(dr("COUNTER_NAME")) = False Then dst.COUNTER_NAME = dr("COUNTER_NAME")
                    If Convert.IsDBNull(dr("USER_ID")) = False Then dst.USER_ID = Convert.ToInt64(dr("USER_ID"))
                    If Convert.IsDBNull(dr("USERNAME")) = False Then dst.USER_NAME = dr("USERNAME")
                    If Convert.IsDBNull(dr("SERVICE_DATE")) = False Then dst.SERVICE_DATE = Convert.ToDateTime(dr("SERVICE_DATE"))
                    If Convert.IsDBNull(dr("ASSIGN_TIME")) = False Then dst.ASSIGN_TIME = Convert.ToDateTime(dr("ASSIGN_TIME"))
                    If Convert.IsDBNull(dr("CALL_TIME")) = False Then dst.CALL_TIME = Convert.ToDateTime(dr("CALL_TIME"))
                    If Convert.IsDBNull(dr("START_TIME")) = False Then dst.START_TIME = Convert.ToDateTime(dr("START_TIME"))
                    If Convert.IsDBNull(dr("END_TIME")) = False Then dst.END_TIME = Convert.ToDateTime(dr("END_TIME"))
                    If Convert.IsDBNull(dr("STATUS")) = False Then dst.STATUS = Convert.ToInt16(dr("STATUS"))
                    If Convert.IsDBNull(dr("HOLD")) = False Then dst.HOLD = dr("HOLD")
                    If Convert.IsDBNull(dr("COMBO_ITEM_ALL")) = False Then dst.COMBO_ITEM_ALL = dr("COMBO_ITEM_ALL")
                    If Convert.IsDBNull(dr("COMBO_ITEM_END")) = False Then dst.COMBO_ITEM_END = dr("COMBO_ITEM_END")
                    If Convert.IsDBNull(dr("FLAG")) = False Then dst.FLAG = dr("FLAG")
                    If Convert.IsDBNull(dr("NETWORK_TYPE")) = False Then dst.NETWORK_TYPE = dr("NETWORK_TYPE")
                    If Convert.IsDBNull(dr("ADD_SERVICE")) = False Then dst.ADD_SERVICE = dr("ADD_SERVICE")
                    If Convert.IsDBNull(dr("CALL_AUTO_FORCE")) = False Then dst.CALL_AUTO_FORCE = dr("CALL_AUTO_FORCE")
                    If Convert.IsDBNull(dr("ID")) = False Then dst.TB_COUNTER_QUEUE_ID = Convert.ToInt64(dr("ID"))

                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "CutOffDataENG.ProcessCutOffData")
                    If shTrans.Trans IsNot Nothing Then
                        If dst.InsertData("CutOffData", shTrans.Trans) = True Then
                            If src.DeleteByPK(dr("ID"), shTrans.Trans) = True Then
                                shTrans.CommitTransaction()
                                _RecCountAfter += 1
                            Else
                                shTrans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("CutOffDataENG.ProcessCutOffData", dst.ErrorMessage & " TB_COUNTER_QUEUE.id=" & src.ID & " ServiceDate: " & src.SERVICE_DATE.Value.ToString("dd/MM/yyyy"), Application.StartupPath & "\ErrorLog\", "CutOffDataENG")
                            End If
                        Else
                            shTrans.RollbackTransaction()
                            FunctionEng.SaveErrorLog("CutOffDataENG.ProcessCutOffData", dst.ErrorMessage & " TB_COUNTER_QUEUE.id=" & dr("ID") & " ServiceDate: " & Convert.ToDateTime(dr("SERVICE_DATE")).ToString("dd/MM/yyyy"), Application.StartupPath & "\ErrorLog\", "CutOffDataENG")
                        End If
                    Else
                        shTrans.RollbackTransaction()
                        FunctionEng.SaveErrorLog("CutOffDataENG.ProcessCutOffData", shTrans.ErrorMessage & " TB_COUNTER_QUEUE.id=" & dr("ID") & " ServiceDate: " & Convert.ToDateTime(dr("SERVICE_DATE")).ToString("dd/MM/yyyy"), Application.StartupPath & "\ErrorLog\", "CutOffDataENG")
                    End If
                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                    Application.DoEvents()
                Next
                FunctionEng.SaveTransLog("CutOffDataENG.ProcessCutOffData", "End Cut off data on Shop " & shLnq.SHOP_ABB & " ### " & _RecCountAfter & " Record(s)")
                dt.Dispose()
                dt = Nothing
            End If
        End If
    End Sub

    Private Function GetProcDT(ByVal TbName As String, ByVal DateField As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable

        Dim sql As String = "select q.id, q.queue_no, q.customer_id,q.customer_name,q.segment,"
        sql += " q.customertype_id,ct.customertype_name,q.item_id,t.item_name,"
        sql += " q.counter_id,c.counter_name,q.user_id,u.username,"
        sql += " q.service_date,q.assign_time,q.call_time,q.start_time,q.end_time,"
        sql += " q.status, q.hold, q.combo_item_all, q.combo_item_end, q.flag,"
        sql += " convert(varchar(10),service_date,120) show_date, isnull(cm.network_type,isnull(q.network_type,'')) network_type,"
        sql += " q.add_service, q.call_auto_force"
        sql += " from TB_COUNTER_QUEUE q"
        sql += " inner join TB_CUSTOMERTYPE ct on ct.id=q.customertype_id"
        sql += " inner join TB_ITEM t on t.id=q.item_id"
        sql += " left join TB_COUNTER c on c.id=q.counter_id"
        sql += " left join TB_USER u on u.id=q.user_id"
        sql += " left join TB_CUSTOMER cm on cm.mobile_no=q.customer_id"
        sql += " where convert(varchar(10),q.service_date,120) < '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "'"
        Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        shTrans.CommitTransaction()

        Return dt
    End Function

    Private Function GetLogFileDT(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        Dim sql As String = "select id, cq_id, log_date, queue_no, customer_id, [user_id], item_id, counter_id, flag, status from TB_LOGFILE"
        sql += " where convert(varchar(10),log_date,120) < '" & DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-US")) & "'"
        Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        shTrans.CommitTransaction()

        Return dt
    End Function
End Class
