Imports Engine.Common
Imports System.Windows.Forms
Imports CenParaDB.Common.Utilities

Namespace Reports
    Public Class ReportsStaffAttendanceENG : Inherits ReportsENG


        Dim _ServiceDate As New DateTime(1, 1, 1)
        Dim _ShopID As Long = 0
        Dim _lblTime As Label

        Public WriteOnly Property ServiceDate()
            Set(ByVal value)
                _ServiceDate = value
            End Set
        End Property
        Public WriteOnly Property ShopID() As Long
            Set(ByVal value As Long)
                _ShopID = value
            End Set
        End Property
        Public WriteOnly Property lblTime() As Label
            Set(ByVal value As Label)
                _lblTime = value
            End Set
        End Property

        Public Sub StaffAttendanceProcessAllReport()
            ProcReportByDate(_ServiceDate, _ShopID, _lblTime)
            ProcReportByWeek(_ServiceDate, _ShopID, _lblTime)
            ProcReportByMonth(_ServiceDate, _ShopID, _lblTime)
        End Sub

        Public Sub ProcReportByDate(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : " & Constant.ReportName.ReportsStaffAttendanceReport, ServiceDate)
            If ProcID <> 0 Then
                Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_STAFF_ATTENDANCE where convert(varchar(10), service_date,120) = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                dTrans.CommitTransaction()

                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                If trans.Trans IsNot Nothing Then
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsStaffAttendanceENG.ProcReportByDate")
                    If shTrans.Trans IsNot Nothing Then
                        Dim dt As DataTable = GetReportDataByDate(ServiceDate, shTrans)
                        shTrans.CommitTransaction()
                        If dt.Rows.Count > 0 Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsStaffAttendanceENG.ProcReportByDate")
                            If shTrans.Trans IsNot Nothing Then
                                For Each dr As DataRow In dt.Rows
                                    Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceCenLinqDB
                                    lnq.SHOP_ID = shLnq.ID
                                    lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                                    lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                                    lnq.SERVICE_DATE = ServiceDate
                                    lnq.SHOW_DATE = ServiceDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                                    lnq.USER_ID = dr("user_id")
                                    If Convert.IsDBNull(dr("staff_name")) = False Then
                                        lnq.USERNAME = dr("username")
                                        lnq.USER_CODE = dr("user_code")
                                        lnq.STAFF_NAME = dr("staff_name")
                                    Else
                                        lnq.USERNAME = "#"
                                        lnq.USER_CODE = "#"
                                        lnq.STAFF_NAME = "#"
                                    End If
                                    lnq.LOG_IN = dr("ST").ToString
                                    If Convert.IsDBNull(dr("ST")) = False Then
                                        lnq.LOG_OUT = dr("ET").ToString
                                        lnq.TOTAL_TIME = GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.LOG_OUT) - GetSecFromTimeFormat(lnq.LOG_IN))
                                    End If

                                    '************** Service Time ***************
                                    Dim vTotProd As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "", "1", shTrans)
                                    Dim vTotNonProd As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "", "0", shTrans)

                                    Dim Prod As String = GetFormatTimeFromSec(vTotProd)
                                    Dim NonProd As String = GetFormatTimeFromSec(vTotNonProd)

                                    If lnq.TOTAL_TIME <> "00:00:00" Then
                                        If Prod <> "" And NonProd <> "" Then
                                            'lnq.SERVICE_TIME = GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.TOTAL_TIME) - (GetSecFromTimeFormat(Prod) + GetSecFromTimeFormat(NonProd)))
                                            lnq.SERVICE_TIME = GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.TOTAL_TIME) - (vTotProd + vTotNonProd))
                                        Else
                                            lnq.SERVICE_TIME = GetFormatTimeFromSec(0)
                                        End If
                                        lnq.PRODUCTIVITY = GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.SERVICE_TIME) + GetSecFromTimeFormat(Prod))
                                        lnq.NON_PRODUCTIVITY = NonProd
                                    Else
                                        lnq.SERVICE_TIME = "00:00:00"
                                        lnq.PRODUCTIVITY = "00:00:00"
                                        lnq.NON_PRODUCTIVITY = "00:00:00"
                                    End If

                                    Dim vProdLearning As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "LEARNING", "1", shTrans)
                                    Dim vProdStandBy As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "STAND BY", "1", shTrans)
                                    Dim vProdBrief As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "BRIEF", "1", shTrans)
                                    Dim vProdWarpUp As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "WARP UP", "1", shTrans)
                                    Dim vProdConsult As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "CONSULT", "1", shTrans)
                                    Dim vProdOther As Integer = vTotProd - (vProdLearning + vProdStandBy + vProdBrief + vProdWarpUp + vProdConsult)
                                    lnq.PROD_LEARNING = GetFormatTimeFromSec(vProdLearning)
                                    lnq.PROD_STAND_BY = GetFormatTimeFromSec(vProdStandBy)
                                    lnq.PROD_BRIEF = GetFormatTimeFromSec(vProdBrief)
                                    lnq.PROD_WARP_UP = GetFormatTimeFromSec(vProdWarpUp)
                                    lnq.PROD_CONSULT = GetFormatTimeFromSec(vProdConsult)
                                    lnq.PROD_OTHER = GetFormatTimeFromSec(vProdOther)
                                    lnq.TOTAL_PRODUCTIVITY = Prod

                                    'lnq.PRODUCTIVITY_DESC = GetProductivityDesc(ServiceDate, lnq.USER_ID, shTrans)
                                    'lnq.NON_PRODUCTIVITY_DESC = GetNonProductivityDesc(ServiceDate, lnq.USER_ID, shTrans)

                                    Dim nProdLunch As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "LUNCH", "0", shTrans)
                                    Dim nProdLeave As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "LEAVE", "0", shTrans)
                                    Dim nProdChangeCounter As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "CHANGE COUNTER", "0", shTrans)
                                    Dim nProdHome As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "HOME", "0", shTrans)
                                    Dim nProdMiniBreak As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "MINI BREAK", "0", shTrans)
                                    Dim nProdRestRoom As Integer = GetSumReasonByDate(ServiceDate, lnq.USER_ID, "REST ROOM", "0", shTrans)
                                    Dim nProdOther As Integer = vTotNonProd - (nProdLunch + nProdLeave + nProdChangeCounter + nProdHome + nProdMiniBreak + nProdRestRoom)
                                    lnq.NPROD_LUNCH = GetFormatTimeFromSec(nProdLunch)
                                    lnq.NPROD_LEAVE = GetFormatTimeFromSec(nProdLeave)
                                    lnq.NPROD_CHANGE_COUNTER = GetFormatTimeFromSec(nProdChangeCounter)
                                    lnq.NPROD_HOME = GetFormatTimeFromSec(nProdHome)
                                    lnq.NPROD_MINI_BREAK = GetFormatTimeFromSec(nProdMiniBreak)
                                    lnq.NPROD_RESTROOM = GetFormatTimeFromSec(nProdRestRoom)
                                    lnq.NPROD_OTHER = GetFormatTimeFromSec(nProdOther)
                                    lnq.TOTAL_NON_PRODUCTIVITY = NonProd

                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    Dim ret As Boolean = False
                                    ret = lnq.InsertData("ProcessReports", trans.Trans)
                                    If ret = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByDate", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsStaffAttendanceENG")
                                    End If

                                    Try
                                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                        Application.DoEvents()
                                    Catch ex As Exception

                                    End Try
                                Next
                                shTrans.CommitTransaction()
                                dt.Dispose()
                            Else
                                UpdateProcessError(ProcID, shTrans.ErrorMessage)
                            End If
                        End If
                    Else
                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
                    End If
                End If
                UpdateProcessTime(ProcID)
            End If
            shLnq = Nothing
        End Sub
        'Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
        '    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        '    Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : ReportsStaffAttendanceReportWeek", ServiceDate)
        '    If ProcID <> 0 Then
        '        Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '        Dim c As New Globalization.CultureInfo("en-US")
        '        Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
        '        CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_STAFF_ATTENDANCE_WEEK where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
        '        dTrans.CommitTransaction()

        '        Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
        '        Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)

        '        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '        If trans.Trans IsNot Nothing Then
        '            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsStaffAttendanceENG.ProcReportByWeek")
        '            If shTrans.Trans IsNot Nothing Then
        '                Dim dt As DataTable = GetReportDataByPeriodDate(FirstDay, LastDay, shLnq.ID)
        '                shTrans.CommitTransaction()
        '                If dt.Rows.Count > 0 Then
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsStaffAttendanceENG.ProcReportByWeek")
        '                    If shTrans.Trans IsNot Nothing Then
        '                        For Each dr As DataRow In dt.Rows
        '                            Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceWeekCenLinqDB
        '                            lnq.SHOP_ID = shLnq.ID
        '                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                            lnq.WEEK_OF_YEAR = WeekNo
        '                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
        '                            lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "-" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        '                            lnq.USER_ID = dr("user_id")
        '                            If Convert.IsDBNull(dr("staff_name")) = False Then
        '                                lnq.USERNAME = dr("username")
        '                                lnq.USER_CODE = dr("user_code")
        '                                lnq.STAFF_NAME = dr("staff_name")
        '                            Else
        '                                lnq.USERNAME = "-"
        '                                lnq.USER_CODE = "-"
        '                                lnq.STAFF_NAME = "-"
        '                            End If
        '                            lnq.LOG_IN = dr("ST").ToString

        '                            Dim vSumTotalTime As Integer = 0
        '                            If Convert.IsDBNull(dr("ST")) = False Then
        '                                _WorkingDayByPeriod = 0
        '                                vSumTotalTime = GetSecFromTimeFormat(GetSumTotalTimeByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans))
        '                                lnq.LOG_OUT = dr("ET").ToString
        '                                If _WorkingDayByPeriod > 0 Then
        '                                    lnq.TOTAL_TIME = GetFormatTimeFromSec(vSumTotalTime / _WorkingDayByPeriod) 'GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.LOG_OUT) - GetSecFromTimeFormat(lnq.LOG_IN))
        '                                Else
        '                                    lnq.TOTAL_TIME = "00:00:00"
        '                                End If
        '                            End If

        '                            '************** Service Time ***************
        '                            Dim vSumProd As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "", "1", shTrans)
        '                            Dim vSumNonProd As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "", "0", shTrans)
        '                            Dim vSumServiceTime As ReasonData = GetSumServiceTimeByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans) '(vSumTotalTime - (vSumProd.SumReasonTime + vSumNonProd.SumReasonTime))

        '                            Dim vAvgProd As Integer = 0
        '                            Dim vAvgNonProd As Integer = 0
        '                            If _WorkingDayByPeriod > 0 Then
        '                                vAvgProd = vSumProd.SumReasonTime / _WorkingDayByPeriod
        '                                vAvgNonProd = vSumNonProd.SumReasonTime / _WorkingDayByPeriod
        '                            End If

        '                            Dim Prod As String = GetFormatTimeFromSec(vAvgProd)
        '                            Dim NonProd As String = GetFormatTimeFromSec(vAvgNonProd)
        '                            If Prod <> "" And NonProd <> "" Then
        '                                If _WorkingDayByPeriod > 0 Then
        '                                    lnq.SERVICE_TIME = GetFormatTimeFromSec(vSumServiceTime.SumReasonTime / _WorkingDayByPeriod)
        '                                Else
        '                                    lnq.SERVICE_TIME = GetFormatTimeFromSec(0)
        '                                End If
        '                            Else
        '                                lnq.SERVICE_TIME = GetFormatTimeFromSec(0)
        '                            End If

        '                            Dim tmpProd As ReasonData = GetSumProdutivityByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans)
        '                            lnq.PRODUCTIVITY = GetFormatTimeFromSec(tmpProd.SumReasonTime / tmpProd.CountReasonTime) 'GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.SERVICE_TIME) + GetSecFromTimeFormat(Prod))
        '                            lnq.NON_PRODUCTIVITY = NonProd

        '                            Dim vSumEstOT As ReasonData = GetSumEstOTByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans)
        '                            If vSumEstOT.CountReasonTime > 0 Then
        '                                lnq.EST_OT = GetFormatTimeFromSec(vSumEstOT.SumReasonTime / vSumEstOT.CountReasonTime)
        '                            Else
        '                                lnq.EST_OT = "00:00:00"
        '                            End If

        '                            'Dim vProdLearning As Integer = 0
        '                            'Dim vProdStandBy As Integer = 0
        '                            'Dim vProdBrief As Integer = 0
        '                            'Dim vProdWarpUp As Integer = 0
        '                            'Dim vProdConsult As Integer = 0
        '                            'Dim vProdOther As Integer = 0
        '                            'vProdLearning = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEARNING", "1", shTrans)
        '                            'vProdStandBy = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "STAND BY", "1", shTrans)
        '                            'vProdBrief = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "BRIEF", "1", shTrans)
        '                            'vProdWarpUp = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "WARP UP", "1", shTrans)
        '                            'vProdConsult = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CONSULT", "1", shTrans)
        '                            'vProdOther = vSumProd - (vProdLearning + vProdStandBy + vProdBrief + vProdWarpUp + vProdConsult)
        '                            'If _WorkingDayByPeriod > 0 Then
        '                            '    lnq.PROD_LEARNING = GetFormatTimeFromSec(vProdLearning / _WorkingDayByPeriod)
        '                            '    lnq.PROD_STAND_BY = GetFormatTimeFromSec(vProdStandBy / _WorkingDayByPeriod)
        '                            '    lnq.PROD_BRIEF = GetFormatTimeFromSec(vProdBrief / _WorkingDayByPeriod)
        '                            '    lnq.PROD_WARP_UP = GetFormatTimeFromSec(vProdWarpUp / _WorkingDayByPeriod)
        '                            '    lnq.PROD_CONSULT = GetFormatTimeFromSec(vProdConsult / _WorkingDayByPeriod)
        '                            '    lnq.PROD_OTHER = GetFormatTimeFromSec(vProdOther / _WorkingDayByPeriod)
        '                            '    lnq.TOTAL_PRODUCTIVITY = Prod
        '                            'End If

        '                            lnq.PROD_LEARNING = "00:00:00"
        '                            lnq.PROD_STAND_BY = "00:00:00"
        '                            lnq.PROD_BRIEF = "00:00:00"
        '                            lnq.PROD_WARP_UP = "00:00:00"
        '                            lnq.PROD_CONSULT = "00:00:00"
        '                            lnq.PROD_OTHER = "00:00:00"

        '                            Dim vProdLearning As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEARNING", "1", shTrans)
        '                            Dim vProdStandBy As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "STAND BY", "1", shTrans)
        '                            Dim vProdBrief As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "BRIEF", "1", shTrans)
        '                            Dim vProdWarpUp As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "WARP UP", "1", shTrans)
        '                            Dim vProdConsult As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CONSULT", "1", shTrans)
        '                            Dim vProdOther As ReasonData = GetOtherReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "1", shTrans)

        '                            If vProdLearning.CountReasonTime > 0 Then
        '                                lnq.PROD_LEARNING = GetFormatTimeFromSec(vProdLearning.SumReasonTime / vProdLearning.CountReasonTime)
        '                            End If
        '                            If vProdStandBy.CountReasonTime > 0 Then
        '                                lnq.PROD_STAND_BY = GetFormatTimeFromSec(vProdStandBy.SumReasonTime / vProdStandBy.CountReasonTime)
        '                            End If
        '                            If vProdBrief.CountReasonTime > 0 Then
        '                                lnq.PROD_BRIEF = GetFormatTimeFromSec(vProdBrief.SumReasonTime / vProdBrief.CountReasonTime)
        '                            End If
        '                            If vProdWarpUp.CountReasonTime > 0 Then
        '                                lnq.PROD_WARP_UP = GetFormatTimeFromSec(vProdWarpUp.SumReasonTime / vProdWarpUp.CountReasonTime)
        '                            End If
        '                            If vProdConsult.CountReasonTime > 0 Then
        '                                lnq.PROD_CONSULT = GetFormatTimeFromSec(vProdConsult.SumReasonTime / vProdConsult.CountReasonTime)
        '                            End If
        '                            If vProdOther.CountReasonTime > 0 Then
        '                                lnq.PROD_OTHER = GetFormatTimeFromSec(vProdOther.SumReasonTime / vProdOther.CountReasonTime)
        '                            End If

        '                            lnq.TOTAL_PRODUCTIVITY = Prod

        '                            ''Non Productivity
        '                            'Dim nProdLunch As Integer = 0
        '                            'Dim nProdLeave As Integer = 0
        '                            'Dim nProdChangeCounter As Integer = 0
        '                            'Dim nProdHome As Integer = 0
        '                            'Dim nProdMiniBreak As Integer = 0
        '                            'Dim nProdRestRoom As Integer = 0
        '                            'Dim nProdOther As Integer = 0
        '                            'nProdLunch = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LUNCH", "0", shTrans)
        '                            'nProdLeave = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEAVE", "0", shTrans)
        '                            'nProdChangeCounter = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CHANGE COUNTER", "0", shTrans)
        '                            'nProdHome = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "HOME", "0", shTrans)
        '                            'nProdMiniBreak = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "MINI BREAK", "0", shTrans)
        '                            'nProdRestRoom = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "REST ROOM", "0", shTrans)
        '                            'nProdOther = vSumNonProd - (nProdLunch + nProdLeave + nProdChangeCounter + nProdHome + nProdMiniBreak + nProdRestRoom)

        '                            'If _WorkingDayByPeriod > 0 Then
        '                            '    lnq.NPROD_LUNCH = GetFormatTimeFromSec(nProdLunch / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_LEAVE = GetFormatTimeFromSec(nProdLeave / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_CHANGE_COUNTER = GetFormatTimeFromSec(nProdChangeCounter / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_HOME = GetFormatTimeFromSec(nProdHome / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_MINI_BREAK = GetFormatTimeFromSec(nProdMiniBreak / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_RESTROOM = GetFormatTimeFromSec(nProdRestRoom / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_OTHER = GetFormatTimeFromSec(nProdOther / _WorkingDayByPeriod)
        '                            '    lnq.TOTAL_NON_PRODUCTIVITY = NonProd
        '                            'End If

        '                            lnq.NPROD_LUNCH = "00:00:00"
        '                            lnq.NPROD_LEAVE = "00:00:00"
        '                            lnq.NPROD_CHANGE_COUNTER = "00:00:00"
        '                            lnq.NPROD_HOME = "00:00:00"
        '                            lnq.NPROD_MINI_BREAK = "00:00:00"
        '                            lnq.NPROD_RESTROOM = "00:00:00"
        '                            lnq.NPROD_OTHER = "00:00:00"

        '                            Dim nProdLunch As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LUNCH", "0", shTrans)
        '                            Dim nProdLeave As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEAVE", "0", shTrans)
        '                            Dim nProdChangeCounter As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CHANGE COUNTER", "0", shTrans)
        '                            Dim nProdHome As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "HOME", "0", shTrans)
        '                            Dim nProdMiniBreak As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "MINI BREAK", "0", shTrans)
        '                            Dim nProdRestRoom As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "REST ROOM", "0", shTrans)
        '                            Dim nProdOther As ReasonData = GetOtherReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "0", shTrans)

        '                            If nProdLunch.CountReasonTime > 0 Then
        '                                lnq.NPROD_LUNCH = GetFormatTimeFromSec(nProdLunch.SumReasonTime / nProdLunch.CountReasonTime)
        '                            End If
        '                            If nProdLeave.CountReasonTime > 0 Then
        '                                lnq.NPROD_LEAVE = GetFormatTimeFromSec(nProdLeave.SumReasonTime / nProdLeave.CountReasonTime)
        '                            End If
        '                            If nProdChangeCounter.CountReasonTime > 0 Then
        '                                lnq.NPROD_CHANGE_COUNTER = GetFormatTimeFromSec(nProdChangeCounter.SumReasonTime / nProdChangeCounter.CountReasonTime)
        '                            End If
        '                            If nProdHome.CountReasonTime > 0 Then
        '                                lnq.NPROD_HOME = GetFormatTimeFromSec(nProdHome.SumReasonTime / nProdHome.CountReasonTime)
        '                            End If
        '                            If nProdMiniBreak.CountReasonTime > 0 Then
        '                                lnq.NPROD_MINI_BREAK = GetFormatTimeFromSec(nProdMiniBreak.SumReasonTime / nProdMiniBreak.CountReasonTime)
        '                            End If
        '                            If nProdRestRoom.CountReasonTime > 0 Then
        '                                lnq.NPROD_RESTROOM = GetFormatTimeFromSec(nProdRestRoom.SumReasonTime / nProdRestRoom.CountReasonTime)
        '                            End If
        '                            If nProdOther.CountReasonTime > 0 Then
        '                                lnq.NPROD_OTHER = GetFormatTimeFromSec(nProdOther.SumReasonTime / nProdOther.CountReasonTime)
        '                            End If
        '                            lnq.TOTAL_NON_PRODUCTIVITY = NonProd

        '                            lnq.WORKING_DAY = _WorkingDayByPeriod
        '                            lnq.SUM_TOTAL_TIME = vSumTotalTime
        '                            lnq.SUM_SERVICE_TIME = vSumServiceTime.SumReasonTime
        '                            lnq.SUM_PRODUCTIVITY = tmpProd.SumReasonTime 'vSumServiceTime.SumReasonTime + vSumProd.SumReasonTime
        '                            lnq.SUM_NON_PRODUCTIVITY = vSumNonProd.SumReasonTime
        '                            lnq.SUM_EST_OT = vSumEstOT.SumReasonTime
        '                            'lnq.SUM_PROD_LEARNING = vProdLearning
        '                            'lnq.SUM_PROD_STAND_BY = vProdStandBy
        '                            'lnq.SUM_PROD_BRIEF = vProdBrief
        '                            'lnq.SUM_PROD_WARP_UP = vProdWarpUp
        '                            'lnq.SUM_PROD_CONSULT = vProdConsult
        '                            'lnq.SUM_PROD_OTHER = vProdOther
        '                            lnq.SUM_PROD_LEARNING = vProdLearning.SumReasonTime
        '                            lnq.SUM_PROD_STAND_BY = vProdStandBy.SumReasonTime
        '                            lnq.SUM_PROD_BRIEF = vProdBrief.SumReasonTime
        '                            lnq.SUM_PROD_WARP_UP = vProdWarpUp.SumReasonTime
        '                            lnq.SUM_PROD_CONSULT = vProdConsult.SumReasonTime
        '                            lnq.SUM_PROD_OTHER = vProdOther.SumReasonTime

        '                            'lnq.SUM_NPROD_LEAVE = nProdLeave
        '                            'lnq.SUM_NPROD_LUNCH = nProdLunch
        '                            'lnq.SUM_NPROD_CHANGE_COUNTER = nProdChangeCounter
        '                            'lnq.SUM_NPROD_HOME = nProdHome
        '                            'lnq.SUM_NPROD_MINI_BREAK = nProdMiniBreak
        '                            'lnq.SUM_NPROD_RESTROOM = nProdRestRoom
        '                            'lnq.SUM_NPROD_OTHER = nProdOther

        '                            lnq.SUM_NPROD_LEAVE = nProdLeave.SumReasonTime
        '                            lnq.SUM_NPROD_LUNCH = nProdLunch.SumReasonTime
        '                            lnq.SUM_NPROD_CHANGE_COUNTER = nProdChangeCounter.SumReasonTime
        '                            lnq.SUM_NPROD_HOME = nProdHome.SumReasonTime
        '                            lnq.SUM_NPROD_MINI_BREAK = nProdMiniBreak.SumReasonTime
        '                            lnq.SUM_NPROD_RESTROOM = nProdRestRoom.SumReasonTime
        '                            lnq.SUM_NPROD_OTHER = nProdOther.SumReasonTime

        '                            ' Count of Reason
        '                            lnq.COUNT_PRODUCTIVITY = tmpProd.CountReasonTime
        '                            lnq.COUNT_NON_PRODUCTIVITY = vSumNonProd.CountReasonTime
        '                            lnq.COUNT_EST_OT = vSumEstOT.CountReasonTime
        '                            lnq.COUNT_PROD_LEARNING = vProdLearning.CountReasonTime
        '                            lnq.COUNT_PROD_STAND_BY = vProdStandBy.CountReasonTime
        '                            lnq.COUNT_PROD_BRIEF = vProdBrief.CountReasonTime
        '                            lnq.COUNT_PROD_WARP_UP = vProdWarpUp.CountReasonTime
        '                            lnq.COUNT_PROD_CONSULT = vProdConsult.CountReasonTime
        '                            lnq.COUNT_PROD_OTHER = vProdOther.CountReasonTime
        '                            lnq.COUNT_NPROD_LEAVE = nProdLeave.CountReasonTime
        '                            lnq.COUNT_NPROD_LUNCH = nProdLunch.CountReasonTime
        '                            lnq.COUNT_NPROD_CHANGE_COUNTER = nProdChangeCounter.CountReasonTime
        '                            lnq.COUNT_NPROD_HOME = nProdHome.CountReasonTime
        '                            lnq.COUNT_NPROD_MINI_BREAK = nProdMiniBreak.CountReasonTime
        '                            lnq.COUNT_NPROD_RESTROOM = nProdRestRoom.CountReasonTime
        '                            lnq.COUNT_NPROD_OTHER = nProdOther.CountReasonTime

        '                            trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                            Dim ret As Boolean = False
        '                            ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                            If ret = True Then
        '                                trans.CommitTransaction()
        '                            Else
        '                                trans.RollbackTransaction()
        '                                FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage)
        '                            End If
        '                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        '                            Application.DoEvents()
        '                        Next
        '                        shTrans.CommitTransaction()
        '                        dt.Dispose()
        '                    Else
        '                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
        '                    End If
        '                End If
        '            Else
        '                UpdateProcessError(ProcID, shTrans.ErrorMessage)
        '            End If
        '        End If
        '        UpdateProcessTime(ProcID)
        '    End If
        '    shLnq = Nothing
        'End Sub
        Public Sub ProcReportByWeek(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : ReportsStaffAttendanceReportWeek", ServiceDate)
            If ProcID <> 0 Then
                Try

                
                    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                    Dim c As New Globalization.CultureInfo("en-US")
                    Dim WeekNo As Integer = c.Calendar.GetWeekOfYear(ServiceDate, c.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday)
                    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_STAFF_ATTENDANCE_WEEK where week_of_year = '" & WeekNo & "' and show_year='" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                    dTrans.CommitTransaction()

                    Dim FirstDay As Date = GetFirstDayOfWeek(ServiceDate)
                    Dim LastDay As Date = GetLastDayOfWeek(ServiceDate)

                    'Load Cube Report By Date
                    Dim CurrDate As DateTime = FirstDay
                    Do
                        ProcReportByDate(CurrDate, ShopID, lblTime)
                        CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                    Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")

                    Dim dt As DataTable = GetReportDataByPeriodDate(FirstDay, LastDay, shLnq.ID)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceWeekCenLinqDB
                            lnq.SHOP_ID = shLnq.ID
                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                            lnq.WEEK_OF_YEAR = WeekNo
                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                            lnq.PERIOD_DATE = FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "-" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                            lnq.USER_ID = dr("user_id")
                            If Convert.IsDBNull(dr("staff_name")) = False Then
                                lnq.USERNAME = dr("username")
                                lnq.USER_CODE = dr("user_code")
                                lnq.STAFF_NAME = dr("staff_name")
                            Else
                                lnq.USERNAME = "-"
                                lnq.USER_CODE = "-"
                                lnq.STAFF_NAME = "-"
                            End If

                            lnq.WORKING_DAY = Convert.ToInt32(dr("working_day"))
                            lnq.TOTAL_TIME = dr("total_time")
                            lnq.SERVICE_TIME = dr("service_time")
                            lnq.PRODUCTIVITY = dr("productivity")
                            lnq.NON_PRODUCTIVITY = dr("non_productivity")
                            lnq.EST_OT = dr("est_ot")

                            lnq.PROD_LEARNING = dr("prod_learning")
                            lnq.PROD_STAND_BY = dr("prod_stand_by")
                            lnq.PROD_BRIEF = dr("prod_brief")
                            lnq.PROD_WARP_UP = dr("prod_warp_up")
                            lnq.PROD_CONSULT = dr("prod_consult")
                            lnq.PROD_OTHER = dr("prod_other")
                            lnq.TOTAL_PRODUCTIVITY = dr("total_productivity")

                            'Non Productivity
                            lnq.NPROD_LUNCH = dr("nprod_lunch")
                            lnq.NPROD_LEAVE = dr("nprod_leave")
                            lnq.NPROD_CHANGE_COUNTER = dr("nprod_change_counter")
                            lnq.NPROD_HOME = dr("nprod_home")
                            lnq.NPROD_MINI_BREAK = dr("nprod_mini_break")
                            lnq.NPROD_RESTROOM = dr("nprod_restroom")
                            lnq.NPROD_OTHER = dr("nprod_other")
                            lnq.TOTAL_NON_PRODUCTIVITY = dr("total_non_productivity")

                            lnq.SUM_TOTAL_TIME = Convert.ToInt64(dr("sum_total_time"))
                            lnq.SUM_SERVICE_TIME = Convert.ToInt64(dr("sum_service_time"))
                            lnq.SUM_PRODUCTIVITY = Convert.ToInt64(dr("sum_productivity"))
                            lnq.SUM_NON_PRODUCTIVITY = Convert.ToInt64(dr("sum_non_productivity"))
                            lnq.SUM_EST_OT = Convert.ToInt64(dr("sum_est_ot"))

                            lnq.SUM_PROD_LEARNING = Convert.ToInt64(dr("sum_prod_learning"))
                            lnq.SUM_PROD_STAND_BY = Convert.ToInt64(dr("sum_prod_stand_by"))
                            lnq.SUM_PROD_BRIEF = Convert.ToInt64(dr("sum_prod_brief"))
                            lnq.SUM_PROD_WARP_UP = Convert.ToInt64(dr("sum_prod_warp_up"))
                            lnq.SUM_PROD_CONSULT = Convert.ToInt64(dr("sum_prod_consult"))
                            lnq.SUM_PROD_OTHER = Convert.ToInt64(dr("sum_prod_other"))

                            lnq.SUM_NPROD_LEAVE = Convert.ToInt64(dr("sum_nprod_leave"))
                            lnq.SUM_NPROD_LUNCH = Convert.ToInt64(dr("sum_nprod_lunch"))
                            lnq.SUM_NPROD_CHANGE_COUNTER = Convert.ToInt64(dr("sum_nprod_change_counter"))
                            lnq.SUM_NPROD_HOME = Convert.ToInt64(dr("sum_nprod_home"))
                            lnq.SUM_NPROD_MINI_BREAK = Convert.ToInt64(dr("sum_nprod_mini_break"))
                            lnq.SUM_NPROD_RESTROOM = Convert.ToInt64(dr("sum_nprod_restroom"))
                            lnq.SUM_NPROD_OTHER = Convert.ToInt64(dr("sum_nprod_other"))

                            ' Count of Reason
                            lnq.COUNT_PRODUCTIVITY = Convert.ToInt64(dr("count_productivity"))
                            lnq.COUNT_NON_PRODUCTIVITY = Convert.ToInt64(dr("count_non_productivity"))
                            lnq.COUNT_EST_OT = Convert.ToInt64(dr("count_est_ot"))
                            lnq.COUNT_PROD_LEARNING = Convert.ToInt64(dr("count_prod_learning"))
                            lnq.COUNT_PROD_STAND_BY = Convert.ToInt64(dr("count_prod_stand_by"))
                            lnq.COUNT_PROD_BRIEF = Convert.ToInt64(dr("count_prod_brief"))
                            lnq.COUNT_PROD_WARP_UP = Convert.ToInt64(dr("count_prod_warp_up"))
                            lnq.COUNT_PROD_CONSULT = Convert.ToInt64(dr("count_prod_consult"))
                            lnq.COUNT_PROD_OTHER = Convert.ToInt64(dr("count_prod_other"))
                            lnq.COUNT_NPROD_LEAVE = Convert.ToInt64(dr("count_nprod_leave"))
                            lnq.COUNT_NPROD_LUNCH = Convert.ToInt64(dr("count_nprod_lunch"))
                            lnq.COUNT_NPROD_CHANGE_COUNTER = Convert.ToInt64(dr("count_nprod_change_counter"))
                            lnq.COUNT_NPROD_HOME = Convert.ToInt64(dr("count_nprod_home"))
                            lnq.COUNT_NPROD_MINI_BREAK = Convert.ToInt64(dr("count_nprod_mini_break"))
                            lnq.COUNT_NPROD_RESTROOM = Convert.ToInt64(dr("count_nprod_restroom"))
                            lnq.COUNT_NPROD_OTHER = Convert.ToInt64(dr("count_nprod_other"))

                            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                            Dim ret As Boolean = False
                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                            If ret = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByWeek", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsStaffAttendanceENG")
                            End If

                            Try
                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                Application.DoEvents()
                            Catch ex As Exception

                            End Try
                        Next
                        dt.Dispose()
                    End If
                    UpdateProcessTime(ProcID)
                Catch ex As Exception
                    UpdateProcessError(ProcID, "Exception :" & shLnq.SHOP_ABB & " " & ex.Message & vbNewLine & ex.StackTrace)
                    FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByWeek", "Exception :" & shLnq.SHOP_ABB & " " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsStaffAttendanceENG")
                End Try
            End If
            shLnq = Nothing
        End Sub

        Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : ReportsStaffAttendanceReportMonth", ServiceDate)
            If ProcID <> 0 Then
                Try
                    Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
                    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_STAFF_ATTENDANCE_MONTH where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
                    dTrans.CommitTransaction()

                    Dim FirstDay As DateTime = New Date(ServiceDate.Year, ServiceDate.Month, 1)
                    Dim LastDay As DateTime = New Date(ServiceDate.Year, ServiceDate.Month, DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month))

                    'Load Cube Report By Date
                    Dim CurrDate As DateTime = FirstDay
                    Do
                        ProcReportByDate(CurrDate, ShopID, lblTime)
                        CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                    Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")


                    Dim dt As DataTable = GetReportDataByPeriodDate(FirstDay, LastDay, shLnq.ID)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceMonthCenLinqDB
                            lnq.SHOP_ID = shLnq.ID
                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
                            lnq.MONTH_NO = ServiceDate.Month
                            lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
                            lnq.USER_ID = dr("user_id")
                            If Convert.IsDBNull(dr("staff_name")) = False Then
                                lnq.USERNAME = dr("username")
                                lnq.USER_CODE = dr("user_code")
                                lnq.STAFF_NAME = dr("staff_name")
                            Else
                                lnq.USERNAME = "-"
                                lnq.USER_CODE = "-"
                                lnq.STAFF_NAME = "-"
                            End If
                            lnq.WORKING_DAY = Convert.ToInt32(dr("working_day"))
                            lnq.TOTAL_TIME = dr("total_time")
                            lnq.SERVICE_TIME = dr("service_time")
                            lnq.PRODUCTIVITY = dr("productivity")
                            lnq.NON_PRODUCTIVITY = dr("non_productivity")
                            lnq.EST_OT = dr("est_ot")

                            lnq.PROD_LEARNING = dr("prod_learning")
                            lnq.PROD_STAND_BY = dr("prod_stand_by")
                            lnq.PROD_BRIEF = dr("prod_brief")
                            lnq.PROD_WARP_UP = dr("prod_warp_up")
                            lnq.PROD_CONSULT = dr("prod_consult")
                            lnq.PROD_OTHER = dr("prod_other")
                            lnq.TOTAL_PRODUCTIVITY = dr("total_productivity")

                            'Non Productivity
                            lnq.NPROD_LUNCH = dr("nprod_lunch")
                            lnq.NPROD_LEAVE = dr("nprod_leave")
                            lnq.NPROD_CHANGE_COUNTER = dr("nprod_change_counter")
                            lnq.NPROD_HOME = dr("nprod_home")
                            lnq.NPROD_MINI_BREAK = dr("nprod_mini_break")
                            lnq.NPROD_RESTROOM = dr("nprod_restroom")
                            lnq.NPROD_OTHER = dr("nprod_other")
                            lnq.TOTAL_NON_PRODUCTIVITY = dr("total_non_productivity")

                            lnq.SUM_TOTAL_TIME = Convert.ToInt64(dr("sum_total_time"))
                            lnq.SUM_SERVICE_TIME = Convert.ToInt64(dr("sum_service_time"))
                            lnq.SUM_PRODUCTIVITY = Convert.ToInt64(dr("sum_productivity"))
                            lnq.SUM_NON_PRODUCTIVITY = Convert.ToInt64(dr("sum_non_productivity"))
                            lnq.SUM_EST_OT = Convert.ToInt64(dr("sum_est_ot"))

                            lnq.SUM_PROD_LEARNING = Convert.ToInt64(dr("sum_prod_learning"))
                            lnq.SUM_PROD_STAND_BY = Convert.ToInt64(dr("sum_prod_stand_by"))
                            lnq.SUM_PROD_BRIEF = Convert.ToInt64(dr("sum_prod_brief"))
                            lnq.SUM_PROD_WARP_UP = Convert.ToInt64(dr("sum_prod_warp_up"))
                            lnq.SUM_PROD_CONSULT = Convert.ToInt64(dr("sum_prod_consult"))
                            lnq.SUM_PROD_OTHER = Convert.ToInt64(dr("sum_prod_other"))

                            lnq.SUM_NPROD_LEAVE = Convert.ToInt64(dr("sum_nprod_leave"))
                            lnq.SUM_NPROD_LUNCH = Convert.ToInt64(dr("sum_nprod_lunch"))
                            lnq.SUM_NPROD_CHANGE_COUNTER = Convert.ToInt64(dr("sum_nprod_change_counter"))
                            lnq.SUM_NPROD_HOME = Convert.ToInt64(dr("sum_nprod_home"))
                            lnq.SUM_NPROD_MINI_BREAK = Convert.ToInt64(dr("sum_nprod_mini_break"))
                            lnq.SUM_NPROD_RESTROOM = Convert.ToInt64(dr("sum_nprod_restroom"))
                            lnq.SUM_NPROD_OTHER = Convert.ToInt64(dr("sum_nprod_other"))

                            ' Count of Reason
                            lnq.COUNT_PRODUCTIVITY = Convert.ToInt64(dr("count_productivity"))
                            lnq.COUNT_NON_PRODUCTIVITY = Convert.ToInt64(dr("count_non_productivity"))
                            lnq.COUNT_EST_OT = Convert.ToInt64(dr("count_est_ot"))
                            lnq.COUNT_PROD_LEARNING = Convert.ToInt64(dr("count_prod_learning"))
                            lnq.COUNT_PROD_STAND_BY = Convert.ToInt64(dr("count_prod_stand_by"))
                            lnq.COUNT_PROD_BRIEF = Convert.ToInt64(dr("count_prod_brief"))
                            lnq.COUNT_PROD_WARP_UP = Convert.ToInt64(dr("count_prod_warp_up"))
                            lnq.COUNT_PROD_CONSULT = Convert.ToInt64(dr("count_prod_consult"))
                            lnq.COUNT_PROD_OTHER = Convert.ToInt64(dr("count_prod_other"))
                            lnq.COUNT_NPROD_LEAVE = Convert.ToInt64(dr("count_nprod_leave"))
                            lnq.COUNT_NPROD_LUNCH = Convert.ToInt64(dr("count_nprod_lunch"))
                            lnq.COUNT_NPROD_CHANGE_COUNTER = Convert.ToInt64(dr("count_nprod_change_counter"))
                            lnq.COUNT_NPROD_HOME = Convert.ToInt64(dr("count_nprod_home"))
                            lnq.COUNT_NPROD_MINI_BREAK = Convert.ToInt64(dr("count_nprod_mini_break"))
                            lnq.COUNT_NPROD_RESTROOM = Convert.ToInt64(dr("count_nprod_restroom"))
                            lnq.COUNT_NPROD_OTHER = Convert.ToInt64(dr("count_nprod_other"))

                            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                            Dim ret As Boolean = False
                            ret = lnq.InsertData("ProcessReports", trans.Trans)
                            If ret = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ReportsStaffAttendanceENG")
                            End If

                            Try
                                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                                Application.DoEvents()
                            Catch ex As Exception

                            End Try
                        Next
                        dt.Dispose()
                    End If
                    UpdateProcessTime(ProcID)
                Catch ex As Exception
                    UpdateProcessError(ProcID, "Exception :" & shLnq.SHOP_ABB & " " & ex.Message & vbNewLine & ex.StackTrace)
                    FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByMonth", "Exception :" & shLnq.SHOP_ABB & " " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "ReportsStaffAttendanceENG")
                End Try
            End If
            shLnq = Nothing
        End Sub


        'Public Sub ProcReportByMonth(ByVal ServiceDate As DateTime, ByVal ShopID As Long, ByVal lblTime As Label)
        '    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        '    Dim ProcID As Long = SaveProcessLog(shLnq.SHOP_ABB & " : ReportsStaffAttendanceReportMonth", ServiceDate)
        '    If ProcID <> 0 Then
        '        Dim dTrans As New CenLinqDB.Common.Utilities.TransactionDB
        '        CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery("delete from TB_REP_STAFF_ATTENDANCE_MONTH where month_no = '" & ServiceDate.Month & "' and show_year='" & ServiceDate.Year & "' and shop_id='" & ShopID & "'", dTrans.Trans)
        '        dTrans.CommitTransaction()

        '        Dim FirstDay As DateTime = New Date(ServiceDate.Year, ServiceDate.Month, 1)
        '        Dim LastDay As DateTime = New Date(ServiceDate.Year, ServiceDate.Month, DateTime.DaysInMonth(ServiceDate.Year, ServiceDate.Month))

        '        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '        If trans.Trans IsNot Nothing Then
        '            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        '            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsStaffAttendanceENG.ProcReportByMonth")
        '            If shTrans.Trans IsNot Nothing Then
        '                Dim dt As DataTable = GetReportDataByPeriodDate(FirstDay, LastDay, shTrans)
        '                shTrans.CommitTransaction()
        '                If dt.Rows.Count > 0 Then
        '                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ReportsStaffAttendanceENG.ProcReportByMonth")
        '                    If shTrans.Trans IsNot Nothing Then
        '                        For Each dr As DataRow In dt.Rows
        '                            Dim lnq As New CenLinqDB.TABLE.TbRepStaffAttendanceMonthCenLinqDB
        '                            lnq.SHOP_ID = shLnq.ID
        '                            lnq.SHOP_NAME_TH = shLnq.SHOP_NAME_TH
        '                            lnq.SHOP_NAME_EN = shLnq.SHOP_NAME_EN
        '                            lnq.MONTH_NO = ServiceDate.Month
        '                            lnq.SHOW_MONTH = ServiceDate.ToString("MMMM", New Globalization.CultureInfo("en-US"))
        '                            lnq.SHOW_YEAR = ServiceDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
        '                            lnq.USER_ID = dr("user_id")
        '                            If Convert.IsDBNull(dr("staff_name")) = False Then
        '                                lnq.USERNAME = dr("username")
        '                                lnq.USER_CODE = dr("user_code")
        '                                lnq.STAFF_NAME = dr("staff_name")
        '                            Else
        '                                lnq.USERNAME = "-"
        '                                lnq.USER_CODE = "-"
        '                                lnq.STAFF_NAME = "-"
        '                            End If
        '                            lnq.LOG_IN = dr("ST").ToString

        '                            Dim vSumTotalTime As Integer = 0
        '                            If Convert.IsDBNull(dr("ST")) = False Then
        '                                _WorkingDayByPeriod = 0
        '                                vSumTotalTime = GetSecFromTimeFormat(GetSumTotalTimeByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans))

        '                                lnq.LOG_OUT = dr("ET").ToString
        '                                If _WorkingDayByPeriod > 0 Then
        '                                    lnq.TOTAL_TIME = GetFormatTimeFromSec(vSumTotalTime / _WorkingDayByPeriod) 'GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.LOG_OUT) - GetSecFromTimeFormat(lnq.LOG_IN))
        '                                Else
        '                                    lnq.TOTAL_TIME = "00:00:00"
        '                                End If
        '                            End If

        '                            '************** Service Time ***************
        '                            Dim vSumProd As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "", "1", shTrans)
        '                            Dim vSumNonProd As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "", "0", shTrans)
        '                            Dim vSumServiceTime As ReasonData = GetSumServiceTimeByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans) '(vSumTotalTime - (vSumProd.SumReasonTime + vSumNonProd.SumReasonTime))

        '                            Dim vAvgProd As Integer = 0
        '                            Dim vAvgNonProd As Integer = 0
        '                            If _WorkingDayByPeriod > 0 Then
        '                                vAvgProd = vSumProd.SumReasonTime / _WorkingDayByPeriod
        '                                vAvgNonProd = vSumNonProd.SumReasonTime / _WorkingDayByPeriod
        '                            End If

        '                            Dim Prod As String = GetFormatTimeFromSec(vAvgProd)
        '                            Dim NonProd As String = GetFormatTimeFromSec(vAvgNonProd)
        '                            If Prod <> "" And NonProd <> "" Then
        '                                If _WorkingDayByPeriod > 0 Then
        '                                    lnq.SERVICE_TIME = GetFormatTimeFromSec(vSumServiceTime.SumReasonTime / _WorkingDayByPeriod) 'GetFormatTimeFromSec(vSumServiceTime / _WorkingDayByPeriod)
        '                                Else
        '                                    lnq.SERVICE_TIME = GetFormatTimeFromSec(0)
        '                                End If
        '                            Else
        '                                lnq.SERVICE_TIME = GetFormatTimeFromSec(0)
        '                            End If
        '                            Dim tmpProd As ReasonData = GetSumProdutivityByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans)
        '                            lnq.PRODUCTIVITY = GetFormatTimeFromSec(tmpProd.SumReasonTime / tmpProd.CountReasonTime) 'GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.SERVICE_TIME) + GetSecFromTimeFormat(Prod))
        '                            lnq.NON_PRODUCTIVITY = NonProd

        '                            Dim vSumEstOT As ReasonData = GetSumEstOTByPeriodDate(FirstDay, LastDay, lnq.USER_ID, shTrans)
        '                            If vSumEstOT.CountReasonTime > 0 Then
        '                                lnq.EST_OT = GetFormatTimeFromSec(vSumEstOT.SumReasonTime / vSumEstOT.CountReasonTime)
        '                            Else
        '                                lnq.EST_OT = "00:00:00"
        '                            End If

        '                            'Dim vProdLearning As Integer = 0
        '                            'Dim vProdStandBy As Integer = 0
        '                            'Dim vProdBrief As Integer = 0
        '                            'Dim vProdWarpUp As Integer = 0
        '                            'Dim vProdConsult As Integer = 0
        '                            'Dim vProdOther As Integer = 0
        '                            'vProdLearning = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEARNING", "1", shTrans)
        '                            'vProdStandBy = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "STAND BY", "1", shTrans)
        '                            'vProdBrief = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "BRIEF", "1", shTrans)
        '                            'vProdWarpUp = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "WARP UP", "1", shTrans)
        '                            'vProdConsult = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CONSULT", "1", shTrans)
        '                            'vProdOther = vSumProd - (vProdLearning + vProdStandBy + vProdBrief + vProdWarpUp + vProdConsult)
        '                            'If _WorkingDayByPeriod > 0 Then
        '                            '    lnq.PROD_LEARNING = GetFormatTimeFromSec(vProdLearning / _WorkingDayByPeriod)
        '                            '    lnq.PROD_STAND_BY = GetFormatTimeFromSec(vProdStandBy / _WorkingDayByPeriod)
        '                            '    lnq.PROD_BRIEF = GetFormatTimeFromSec(vProdBrief / _WorkingDayByPeriod)
        '                            '    lnq.PROD_WARP_UP = GetFormatTimeFromSec(vProdWarpUp / _WorkingDayByPeriod)
        '                            '    lnq.PROD_CONSULT = GetFormatTimeFromSec(vProdConsult / _WorkingDayByPeriod)
        '                            '    lnq.PROD_OTHER = GetFormatTimeFromSec(vProdOther / _WorkingDayByPeriod)
        '                            '    lnq.TOTAL_PRODUCTIVITY = Prod
        '                            'End If

        '                            lnq.PROD_LEARNING = "00:00:00"
        '                            lnq.PROD_STAND_BY = "00:00:00"
        '                            lnq.PROD_BRIEF = "00:00:00"
        '                            lnq.PROD_WARP_UP = "00:00:00"
        '                            lnq.PROD_CONSULT = "00:00:00"
        '                            lnq.PROD_OTHER = "00:00:00"

        '                            Dim vProdLearning As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEARNING", "1", shTrans)
        '                            Dim vProdStandBy As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "STAND BY", "1", shTrans)
        '                            Dim vProdBrief As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "BRIEF", "1", shTrans)
        '                            Dim vProdWarpUp As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "WARP UP", "1", shTrans)
        '                            Dim vProdConsult As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CONSULT", "1", shTrans)
        '                            Dim vProdOther As ReasonData = GetOtherReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "1", shTrans)

        '                            If vProdLearning.CountReasonTime > 0 Then
        '                                lnq.PROD_LEARNING = GetFormatTimeFromSec(vProdLearning.SumReasonTime / vProdLearning.CountReasonTime)
        '                            End If
        '                            If vProdStandBy.CountReasonTime > 0 Then
        '                                lnq.PROD_STAND_BY = GetFormatTimeFromSec(vProdStandBy.SumReasonTime / vProdStandBy.CountReasonTime)
        '                            End If
        '                            If vProdBrief.CountReasonTime > 0 Then
        '                                lnq.PROD_BRIEF = GetFormatTimeFromSec(vProdBrief.SumReasonTime / vProdBrief.CountReasonTime)
        '                            End If
        '                            If vProdWarpUp.CountReasonTime > 0 Then
        '                                lnq.PROD_WARP_UP = GetFormatTimeFromSec(vProdWarpUp.SumReasonTime / vProdWarpUp.CountReasonTime)
        '                            End If
        '                            If vProdConsult.CountReasonTime > 0 Then
        '                                lnq.PROD_CONSULT = GetFormatTimeFromSec(vProdConsult.SumReasonTime / vProdConsult.CountReasonTime)
        '                            End If
        '                            If vProdOther.CountReasonTime > 0 Then
        '                                lnq.PROD_OTHER = GetFormatTimeFromSec(vProdOther.SumReasonTime / vProdOther.CountReasonTime)
        '                            End If

        '                            lnq.TOTAL_PRODUCTIVITY = Prod

        '                            'Dim nProdLunch As Integer = 0
        '                            'Dim nProdLeave As Integer = 0
        '                            'Dim nProdChangeCounter As Integer = 0
        '                            'Dim nProdHome As Integer = 0
        '                            'Dim nProdMiniBreak As Integer = 0
        '                            'Dim nProdRestRoom As Integer = 0
        '                            'Dim nProdOther As Integer = 0
        '                            'nProdLunch = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LUNCH", "0", shTrans)
        '                            'nProdLeave = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEAVE", "0", shTrans)
        '                            'nProdChangeCounter = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CHANGE COUNTER", "0", shTrans)
        '                            'nProdHome = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "HOME", "0", shTrans)
        '                            'nProdMiniBreak = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "MINI BREAK", "0", shTrans)
        '                            'nProdRestRoom = GetSumReasonByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "REST ROOM", "0", shTrans)
        '                            'nProdOther = vSumNonProd - (nProdLunch + nProdLeave + nProdChangeCounter + nProdHome + nProdMiniBreak + nProdRestRoom)

        '                            'If _WorkingDayByPeriod > 0 Then
        '                            '    lnq.NPROD_LUNCH = GetFormatTimeFromSec(nProdLunch / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_LEAVE = GetFormatTimeFromSec(nProdLeave / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_CHANGE_COUNTER = GetFormatTimeFromSec(nProdChangeCounter / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_HOME = GetFormatTimeFromSec(nProdHome / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_MINI_BREAK = GetFormatTimeFromSec(nProdMiniBreak / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_RESTROOM = GetFormatTimeFromSec(nProdRestRoom / _WorkingDayByPeriod)
        '                            '    lnq.NPROD_OTHER = GetFormatTimeFromSec(nProdOther / _WorkingDayByPeriod)
        '                            '    lnq.TOTAL_NON_PRODUCTIVITY = NonProd
        '                            'End If

        '                            lnq.NPROD_LUNCH = "00:00:00"
        '                            lnq.NPROD_LEAVE = "00:00:00"
        '                            lnq.NPROD_CHANGE_COUNTER = "00:00:00"
        '                            lnq.NPROD_HOME = "00:00:00"
        '                            lnq.NPROD_MINI_BREAK = "00:00:00"
        '                            lnq.NPROD_RESTROOM = "00:00:00"
        '                            lnq.NPROD_OTHER = "00:00:00"

        '                            Dim nProdLunch As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LUNCH", "0", shTrans)
        '                            Dim nProdLeave As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "LEAVE", "0", shTrans)
        '                            Dim nProdChangeCounter As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "CHANGE COUNTER", "0", shTrans)
        '                            Dim nProdHome As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "HOME", "0", shTrans)
        '                            Dim nProdMiniBreak As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "MINI BREAK", "0", shTrans)
        '                            Dim nProdRestRoom As ReasonData = GetReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "REST ROOM", "0", shTrans)
        '                            Dim nProdOther As ReasonData = GetOtherReasonDataByPeriodDate(FirstDay, LastDay, lnq.USER_ID, "0", shTrans)

        '                            If nProdLunch.CountReasonTime > 0 Then
        '                                lnq.NPROD_LUNCH = GetFormatTimeFromSec(nProdLunch.SumReasonTime / nProdLunch.CountReasonTime)
        '                            End If
        '                            If nProdLeave.CountReasonTime > 0 Then
        '                                lnq.NPROD_LEAVE = GetFormatTimeFromSec(nProdLeave.SumReasonTime / nProdLeave.CountReasonTime)
        '                            End If
        '                            If nProdChangeCounter.CountReasonTime > 0 Then
        '                                lnq.NPROD_CHANGE_COUNTER = GetFormatTimeFromSec(nProdChangeCounter.SumReasonTime / nProdChangeCounter.CountReasonTime)
        '                            End If
        '                            If nProdHome.CountReasonTime > 0 Then
        '                                lnq.NPROD_HOME = GetFormatTimeFromSec(nProdHome.SumReasonTime / nProdHome.CountReasonTime)
        '                            End If
        '                            If nProdMiniBreak.CountReasonTime > 0 Then
        '                                lnq.NPROD_MINI_BREAK = GetFormatTimeFromSec(nProdMiniBreak.SumReasonTime / nProdMiniBreak.CountReasonTime)
        '                            End If
        '                            If nProdRestRoom.CountReasonTime > 0 Then
        '                                lnq.NPROD_RESTROOM = GetFormatTimeFromSec(nProdRestRoom.SumReasonTime / nProdRestRoom.CountReasonTime)
        '                            End If
        '                            If nProdOther.CountReasonTime > 0 Then
        '                                lnq.NPROD_OTHER = GetFormatTimeFromSec(nProdOther.SumReasonTime / nProdOther.CountReasonTime)
        '                            End If
        '                            lnq.TOTAL_NON_PRODUCTIVITY = NonProd

        '                            lnq.WORKING_DAY = _WorkingDayByPeriod
        '                            lnq.SUM_TOTAL_TIME = vSumTotalTime
        '                            'lnq.SUM_SERVICE_TIME = vSumServiceTime
        '                            'lnq.SUM_PRODUCTIVITY = vSumServiceTime + vSumProd.SumReasonTime
        '                            lnq.SUM_SERVICE_TIME = vSumServiceTime.SumReasonTime
        '                            lnq.SUM_PRODUCTIVITY = tmpProd.SumReasonTime ' vSumServiceTime.SumReasonTime + vSumProd.SumReasonTime
        '                            lnq.SUM_NON_PRODUCTIVITY = vSumNonProd.SumReasonTime
        '                            lnq.SUM_EST_OT = vSumEstOT.SumReasonTime
        '                            'lnq.SUM_PROD_LEARNING = vProdLearning
        '                            'lnq.SUM_PROD_STAND_BY = vProdStandBy
        '                            'lnq.SUM_PROD_BRIEF = vProdBrief
        '                            'lnq.SUM_PROD_WARP_UP = vProdWarpUp
        '                            'lnq.SUM_PROD_CONSULT = vProdConsult
        '                            'lnq.SUM_PROD_OTHER = vProdOther
        '                            lnq.SUM_PROD_LEARNING = vProdLearning.SumReasonTime
        '                            lnq.SUM_PROD_STAND_BY = vProdStandBy.SumReasonTime
        '                            lnq.SUM_PROD_BRIEF = vProdBrief.SumReasonTime
        '                            lnq.SUM_PROD_WARP_UP = vProdWarpUp.SumReasonTime
        '                            lnq.SUM_PROD_CONSULT = vProdConsult.SumReasonTime
        '                            lnq.SUM_PROD_OTHER = vProdOther.SumReasonTime

        '                            'lnq.SUM_NPROD_LEAVE = nProdLeave
        '                            'lnq.SUM_NPROD_LUNCH = nProdLunch
        '                            'lnq.SUM_NPROD_CHANGE_COUNTER = nProdChangeCounter
        '                            'lnq.SUM_NPROD_HOME = nProdHome
        '                            'lnq.SUM_NPROD_MINI_BREAK = nProdMiniBreak
        '                            'lnq.SUM_NPROD_RESTROOM = nProdRestRoom
        '                            'lnq.SUM_NPROD_OTHER = nProdOther

        '                            lnq.SUM_NPROD_LEAVE = nProdLeave.SumReasonTime
        '                            lnq.SUM_NPROD_LUNCH = nProdLunch.SumReasonTime
        '                            lnq.SUM_NPROD_CHANGE_COUNTER = nProdChangeCounter.SumReasonTime
        '                            lnq.SUM_NPROD_HOME = nProdHome.SumReasonTime
        '                            lnq.SUM_NPROD_MINI_BREAK = nProdMiniBreak.SumReasonTime
        '                            lnq.SUM_NPROD_RESTROOM = nProdRestRoom.SumReasonTime
        '                            lnq.SUM_NPROD_OTHER = nProdOther.SumReasonTime

        '                            ' Count of Reason
        '                            lnq.COUNT_PRODUCTIVITY = tmpProd.CountReasonTime
        '                            lnq.COUNT_NON_PRODUCTIVITY = vSumNonProd.CountReasonTime
        '                            lnq.COUNT_EST_OT = vSumEstOT.CountReasonTime
        '                            lnq.COUNT_PROD_LEARNING = vProdLearning.CountReasonTime
        '                            lnq.COUNT_PROD_STAND_BY = vProdStandBy.CountReasonTime
        '                            lnq.COUNT_PROD_BRIEF = vProdBrief.CountReasonTime
        '                            lnq.COUNT_PROD_WARP_UP = vProdWarpUp.CountReasonTime
        '                            lnq.COUNT_PROD_CONSULT = vProdConsult.CountReasonTime
        '                            lnq.COUNT_PROD_OTHER = vProdOther.CountReasonTime
        '                            lnq.COUNT_NPROD_LEAVE = nProdLeave.CountReasonTime
        '                            lnq.COUNT_NPROD_LUNCH = nProdLunch.CountReasonTime
        '                            lnq.COUNT_NPROD_CHANGE_COUNTER = nProdChangeCounter.CountReasonTime
        '                            lnq.COUNT_NPROD_HOME = nProdHome.CountReasonTime
        '                            lnq.COUNT_NPROD_MINI_BREAK = nProdMiniBreak.CountReasonTime
        '                            lnq.COUNT_NPROD_RESTROOM = nProdRestRoom.CountReasonTime
        '                            lnq.COUNT_NPROD_OTHER = nProdOther.CountReasonTime

        '                            trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                            Dim ret As Boolean = False
        '                            ret = lnq.InsertData("ProcessReports", trans.Trans)
        '                            If ret = True Then
        '                                trans.CommitTransaction()
        '                            Else
        '                                trans.RollbackTransaction()
        '                                FunctionEng.SaveErrorLog("ReportsStaffAttendanceENG.ProcReportByMonth", shLnq.SHOP_ABB & " " & lnq.ErrorMessage)
        '                            End If
        '                            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        '                            Application.DoEvents()
        '                        Next
        '                        shTrans.CommitTransaction()
        '                        dt.Dispose()
        '                    Else
        '                        UpdateProcessError(ProcID, shTrans.ErrorMessage)
        '                    End If
        '                End If
        '            Else
        '                UpdateProcessError(ProcID, shTrans.ErrorMessage)
        '            End If
        '        End If
        '        UpdateProcessTime(ProcID)
        '    End If
        '    shLnq = Nothing
        'End Sub

        Private Function GetProductivityDesc(ByVal ServiceDate As DateTime, ByVal UserID As Integer, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""
            Dim rDt As DataTable = GetReasonDT("1", ServiceDate, shTrans)
            If rDt.Rows.Count > 0 Then
                For Each rdr As DataRow In rDt.Rows
                    If rdr("reason_type") = "LOGOUT" Then
                        Dim lDt As DataTable = GetLogoutDT(ServiceDate, UserID, "1", shTrans)
                        lDt.DefaultView.RowFilter = "reason_id = '" & rdr("id") & "' "
                        If lDt.DefaultView.Count > 0 Then
                            For Each dr As DataRowView In lDt.DefaultView
                                If ret = "" Then
                                    ret = dr("reason_name") & "|" & dr("Logout")
                                Else
                                    ret += "###" & dr("reason_name") & "|" & dr("Logout")
                                End If
                            Next
                        Else
                            If ret = "" Then
                                ret = rdr("name") & "|0"
                            Else
                                ret += "###" & rdr("name") & "|0"
                            End If
                        End If
                    ElseIf rdr("reason_type") = "HOLD" Then
                        Dim hDt As DataTable = GetHoldDT(ServiceDate, UserID, "1", shTrans)
                        hDt.DefaultView.RowFilter = "reason_id = '" & rdr("id") & "' "
                        If hDt.DefaultView.Count > 0 Then
                            For Each dr As DataRowView In hDt.DefaultView
                                If ret = "" Then
                                    ret = dr("reason_name") & "|" & dr("Hold")
                                Else
                                    ret += "###" & dr("reason_name") & "|" & dr("Hold")
                                End If
                            Next
                        Else
                            If ret = "" Then
                                ret = rdr("name") & "|0"
                            Else
                                ret += "###" & rdr("name") & "|0"
                            End If
                        End If
                    End If
                Next
                rDt.Dispose()
                rDt = Nothing
            End If

            Return ret
        End Function

        Private Function GetNonProductivityDesc(ByVal ServiceDate As DateTime, ByVal UserID As Integer, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""
            'Productive
            '1=Productive
            '0=Non Productive
            Dim rDt As DataTable = GetReasonDT("0", ServiceDate, shTrans)
            If rDt.Rows.Count > 0 Then
                For Each rdr As DataRow In rDt.Rows
                    If rdr("reason_type") = "LOGOUT" Then
                        Dim lDt As DataTable = GetLogoutDT(ServiceDate, UserID, "0", shTrans)
                        lDt.DefaultView.RowFilter = "reason_id = '" & rdr("id") & "' "
                        If lDt.DefaultView.Count > 0 Then
                            For Each dr As DataRowView In lDt.DefaultView
                                If ret = "" Then
                                    ret = dr("reason_name") & "|" & dr("Logout")
                                Else
                                    ret += "###" & dr("reason_name") & "|" & dr("Logout")
                                End If
                            Next
                        Else
                            If ret = "" Then
                                ret = rdr("name") & "|0"
                            Else
                                ret += "###" & rdr("name") & "|0"
                            End If
                        End If
                    ElseIf rdr("reason_type") = "HOLD" Then
                        Dim hDt As DataTable = GetHoldDT(ServiceDate, UserID, "0", shTrans)
                        hDt.DefaultView.RowFilter = "reason_id = '" & rdr("id") & "' "
                        If hDt.DefaultView.Count > 0 Then
                            For Each dr As DataRowView In hDt.DefaultView
                                If ret = "" Then
                                    ret = dr("reason_name") & "|" & dr("Hold")
                                Else
                                    ret += "###" & dr("reason_name") & "|" & dr("Hold")
                                End If
                            Next
                        Else
                            If ret = "" Then
                                ret = rdr("name") & "|0"
                            Else
                                ret += "###" & rdr("name") & "|0"
                            End If
                        End If
                    End If
                Next
                rDt.Dispose()
                rDt = Nothing
            End If

            Return ret
        End Function

        Private Function GetReasonDT(ByVal Productive As String, ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim StrDate As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim sql As String = ""
            sql += " select distinct lr.id, lr.name,lr.productive,'LOGOUT' reason_type " & vbNewLine
            sql += " from TB_LOGOUT_REASON lr" & vbNewLine
            sql += " where lr.productive = '" & Productive & "' "

            sql += " union " & vbNewLine

            sql += " select hr.id, hr.name, hr.productive,'HOLD' reason_type "
            sql += " from TB_HOLD_REASON hr" & vbNewLine
            sql += " where hr.productive = '" & Productive & "' "

            Return ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        End Function

        Private Function GetLogoutDT(ByVal ServiceDate As DateTime, ByVal UserID As Integer, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql += " declare @Date as varchar(10); select @Date = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "';" & vbNewLine
            sql += " select user_id,reason_id,reason_name,sum(DATEDIFF(SECOND,ST,ET)) as Logout  from (" & vbNewLine
            sql += " select user_id,reason_id,name as reason_name,Main.productive,service_date as ST," & vbNewLine
            sql += " (" & vbNewLine
            sql += " Select min(service_date)" & vbNewLine
            sql += " from TB_LOG_LOGIN" & vbNewLine
            sql += " where CONVERT(varchar(10),service_date,120) = @Date and reason_id = 0 and action = 1" & vbNewLine
            sql += " and user_id = Main.user_id and service_date > Main.service_date" & vbNewLine
            sql += " ) as ET" & vbNewLine
            sql += " from TB_LOG_LOGIN as Main " & vbNewLine
            sql += " left join TB_LOGOUT_REASON on Main.reason_id = TB_LOGOUT_REASON.id" & vbNewLine
            sql += " where CONVERT(varchar(10),service_date,120) = @Date and reason_id > 0 " & vbNewLine
            sql += " ) TB where ET is not null and productive = " & Productive & " and user_id = " & UserID & vbNewLine
            sql += " group by user_id,reason_id,reason_name" & vbNewLine

            Dim lnq As New ShLinqDB.TABLE.TbLogHoldShLinqDB
            Return lnq.GetListBySql(sql, shTrans.Trans)
        End Function

        Private Function GetHoldDT(ByVal ServiceDate As DateTime, ByVal UserID As Integer, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim sql As String = ""
            sql += " declare @Date as varchar(10); select @Date = '" & ServiceDate.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "';" & vbNewLine
            sql += " select user_id,reason_id,reason_name,sum(DATEDIFF(SECOND,ST,ET)) as Hold  from (" & vbNewLine
            sql += " select user_id,reason_id,name as reason_name,Main.productive,service_date as ST," & vbNewLine
            sql += " (" & vbNewLine
            sql += " Select min(service_date)" & vbNewLine
            sql += " from TB_LOG_HOLD" & vbNewLine
            sql += " where CONVERT(varchar(10),service_date,120) = @Date and reason_id = 0 and action =2" & vbNewLine
            sql += " and user_id = Main.user_id and service_date > Main.service_date" & vbNewLine
            sql += " ) as ET" & vbNewLine
            sql += " from TB_LOG_HOLD as Main" & vbNewLine
            sql += " left join TB_HOLD_REASON on Main.reason_id = TB_HOLD_REASON.id" & vbNewLine
            sql += " where CONVERT(varchar(10),service_date,120) = @Date and reason_id > 0 " & vbNewLine
            sql += " ) TB where ET is not null and productive = " & Productive & " and user_id = " & UserID & vbNewLine
            sql += " group by user_id,reason_id,reason_name"

            Dim lnq As New ShLinqDB.TABLE.TbLogLoginShLinqDB
            Return lnq.GetListBySql(sql, shTrans.Trans)
        End Function




        Private Function GetTotalTime(ByVal TimeFrom As DateTime, ByVal TimeTo As DateTime) As String
            Return GetFormatTimeFromSec(DateDiff(DateInterval.Second, TimeFrom, TimeTo))
        End Function

        Private Function GetReportDataByDate(ByVal ServiceDate As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
            Dim dt As DataTable = lnq.GetListBySql(GetQuery(ServiceDate), shTrans.Trans)
            lnq = Nothing
            Return dt
        End Function

        Private Function GetReportDataByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal ShopID As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("user_id")
            ret.Columns.Add("user_code")
            ret.Columns.Add("username")
            ret.Columns.Add("staff_name")
            ret.Columns.Add("working_day", GetType(Integer))
            ret.Columns.Add("total_time")
            ret.Columns.Add("service_time")
            ret.Columns.Add("productivity")
            ret.Columns.Add("non_productivity")
            ret.Columns.Add("est_ot")

            ret.Columns.Add("prod_learning")
            ret.Columns.Add("prod_stand_by")
            ret.Columns.Add("prod_brief")
            ret.Columns.Add("prod_warp_up")
            ret.Columns.Add("prod_consult")
            ret.Columns.Add("prod_other")
            ret.Columns.Add("total_productivity")

            ret.Columns.Add("nprod_lunch")
            ret.Columns.Add("nprod_leave")
            ret.Columns.Add("nprod_change_counter")
            ret.Columns.Add("nprod_mini_break")
            ret.Columns.Add("nprod_home")
            ret.Columns.Add("nprod_restroom")
            ret.Columns.Add("nprod_other")
            ret.Columns.Add("total_non_productivity")

            'Summary Column
            ret.Columns.Add("sum_total_time", GetType(Long))
            ret.Columns.Add("sum_service_time", GetType(Long))
            ret.Columns.Add("sum_productivity", GetType(Long))
            ret.Columns.Add("sum_non_productivity", GetType(Long))
            ret.Columns.Add("sum_est_ot", GetType(Long))
            ret.Columns.Add("sum_prod_learning", GetType(Long))
            ret.Columns.Add("sum_prod_stand_by", GetType(Long))
            ret.Columns.Add("sum_prod_brief", GetType(Long))
            ret.Columns.Add("sum_prod_warp_up", GetType(Long))
            ret.Columns.Add("sum_prod_consult", GetType(Long))
            ret.Columns.Add("sum_prod_other", GetType(Long))
            ret.Columns.Add("sum_nprod_lunch", GetType(Long))
            ret.Columns.Add("sum_nprod_leave", GetType(Long))
            ret.Columns.Add("sum_nprod_change_counter", GetType(Long))
            ret.Columns.Add("sum_nprod_home", GetType(Long))
            ret.Columns.Add("sum_nprod_mini_break", GetType(Long))
            ret.Columns.Add("sum_nprod_restroom", GetType(Long))
            ret.Columns.Add("sum_nprod_other", GetType(Long))

            'Count Column
            ret.Columns.Add("count_productivity", GetType(Long))
            ret.Columns.Add("count_non_productivity", GetType(Long))
            ret.Columns.Add("count_est_ot", GetType(Long))
            ret.Columns.Add("count_prod_learning", GetType(Long))
            ret.Columns.Add("count_prod_stand_by", GetType(Long))
            ret.Columns.Add("count_prod_brief", GetType(Long))
            ret.Columns.Add("count_prod_warp_up", GetType(Long))
            ret.Columns.Add("count_prod_consult", GetType(Long))
            ret.Columns.Add("count_prod_other", GetType(Long))
            ret.Columns.Add("count_nprod_lunch", GetType(Long))
            ret.Columns.Add("count_nprod_leave", GetType(Long))
            ret.Columns.Add("count_nprod_change_counter", GetType(Long))
            ret.Columns.Add("count_nprod_home", GetType(Long))
            ret.Columns.Add("count_nprod_mini_break", GetType(Long))
            ret.Columns.Add("count_nprod_restroom", GetType(Long))
            ret.Columns.Add("count_nprod_other", GetType(Long))

            Try
                Dim sql As String = "select sa.user_id, sa.user_code, sa.username, sa.staff_name, " & vbNewLine
                sql += " sa.total_time, sa.service_time, sa.productivity, sa.non_productivity, " & vbNewLine
                sql += " sa.prod_learning, sa.prod_stand_by, sa.prod_brief, sa.prod_warp_up,sa.prod_consult, sa.prod_other, sa.total_productivity," & vbNewLine
                sql += " sa.nprod_lunch, sa.nprod_leave, sa.nprod_change_counter,sa.nprod_home, sa.nprod_mini_break, sa.nprod_restroom,sa.nprod_other, sa.total_non_productivity " & vbNewLine
                sql += " from tb_rep_staff_attendance sa" & vbNewLine
                sql += " where convert(varchar(8),service_date,112) between '" & FirstDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and '" & LastDay.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'" & vbNewLine
                sql += " and sa.shop_id='" & ShopID & "'" & vbNewLine
                Dim dt As New DataTable
                dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql)
                If dt.Rows.Count > 0 Then
                    Dim uDt As New DataTable
                    uDt = dt.DefaultView.ToTable(True, "user_id", "user_code", "username", "staff_name").Copy
                    If uDt.Rows.Count > 0 Then
                        For Each uDr As DataRow In uDt.Rows
                            Dim tmp As DataRow = ret.NewRow
                            tmp("user_id") = uDr("user_id")
                            tmp("user_code") = uDr("user_code")
                            tmp("username") = uDr("username")
                            tmp("staff_name") = uDr("staff_name")

                            ''Default Value
                            For j As Integer = 0 To ret.Columns.Count - 1
                                If Convert.IsDBNull(tmp(j)) = True Then
                                    tmp(j) = 0
                                End If
                            Next

                            dt.DefaultView.RowFilter = "user_id='" & uDr("user_id") & "'"
                            For Each drv As DataRowView In dt.DefaultView
                                If ReportsENG.GetSecFromTimeFormat(drv("total_time")) > 0 Then
                                    tmp("working_day") += 1
                                    tmp("sum_total_time") += ReportsENG.GetSecFromTimeFormat(drv("total_time"))
                                    tmp("sum_service_time") += ReportsENG.GetSecFromTimeFormat(drv("service_time"))
                                End If
                                If ReportsENG.GetSecFromTimeFormat(drv("productivity")) > 0 Then
                                    tmp("sum_productivity") += ReportsENG.GetSecFromTimeFormat(drv("productivity"))
                                    tmp("count_productivity") += 1
                                End If
                                If ReportsENG.GetSecFromTimeFormat(drv("non_productivity")) > 0 Then
                                    tmp("sum_non_productivity") += ReportsENG.GetSecFromTimeFormat(drv("non_productivity"))
                                    tmp("count_non_productivity") += 1
                                End If
                                If ReportsENG.GetSecFromTimeFormat(drv("total_time")) > (450 * 60) Then
                                    tmp("sum_est_ot") += (ReportsENG.GetSecFromTimeFormat(drv("total_time")) - (450 * 60))
                                    tmp("count_est_ot") += 1
                                End If
                                '##### Productivity
                                If drv("prod_learning") <> "00:00:00" Then
                                    tmp("sum_prod_learning") += ReportsENG.GetSecFromTimeFormat(drv("prod_learning"))
                                    tmp("count_prod_learning") += 1
                                End If
                                If drv("prod_stand_by") <> "00:00:00" Then
                                    tmp("sum_prod_stand_by") += ReportsENG.GetSecFromTimeFormat(drv("prod_stand_by"))
                                    tmp("count_prod_stand_by") += 1
                                End If
                                If drv("prod_brief") <> "00:00:00" Then
                                    tmp("sum_prod_brief") += ReportsENG.GetSecFromTimeFormat(drv("prod_brief"))
                                    tmp("count_prod_brief") += 1
                                End If
                                If drv("prod_warp_up") <> "00:00:00" Then
                                    tmp("sum_prod_warp_up") += ReportsENG.GetSecFromTimeFormat(drv("prod_warp_up"))
                                    tmp("count_prod_warp_up") += 1
                                End If
                                If drv("prod_consult") <> "00:00:00" Then
                                    tmp("sum_prod_consult") += ReportsENG.GetSecFromTimeFormat(drv("prod_consult"))
                                    tmp("count_prod_consult") += 1
                                End If
                                If drv("prod_other") <> "00:00:00" Then
                                    tmp("sum_prod_other") += ReportsENG.GetSecFromTimeFormat(drv("prod_other"))
                                    tmp("count_prod_other") += 1
                                End If

                                '##### Non Productivity
                                If drv("nprod_lunch") <> "00:00:00" Then
                                    tmp("sum_nprod_lunch") += ReportsENG.GetSecFromTimeFormat(drv("nprod_lunch"))
                                    tmp("count_nprod_lunch") += 1
                                End If
                                If drv("nprod_leave") <> "00:00:00" Then
                                    tmp("sum_nprod_leave") += ReportsENG.GetSecFromTimeFormat(drv("nprod_leave"))
                                    tmp("count_nprod_leave") += 1
                                End If
                                If drv("nprod_change_counter") <> "00:00:00" Then
                                    tmp("sum_nprod_change_counter") += ReportsENG.GetSecFromTimeFormat(drv("nprod_change_counter"))
                                    tmp("count_nprod_change_counter") += 1
                                End If
                                If drv("nprod_home") <> "00:00:00" Then
                                    tmp("sum_nprod_home") += ReportsENG.GetSecFromTimeFormat(drv("nprod_home"))
                                    tmp("count_nprod_home") += 1
                                End If
                                If drv("nprod_mini_break") <> "00:00:00" Then
                                    tmp("sum_nprod_mini_break") += ReportsENG.GetSecFromTimeFormat(drv("nprod_mini_break"))
                                    tmp("count_nprod_mini_break") += 1
                                End If
                                If drv("nprod_restroom") <> "00:00:00" Then
                                    tmp("sum_nprod_restroom") += ReportsENG.GetSecFromTimeFormat(drv("nprod_restroom"))
                                    tmp("count_nprod_restroom") += 1
                                End If
                                If drv("nprod_other") <> "00:00:00" Then
                                    tmp("sum_nprod_other") += ReportsENG.GetSecFromTimeFormat(drv("nprod_other"))
                                    tmp("count_nprod_other") += 1
                                End If
                            Next
                            dt.DefaultView.RowFilter = ""

                            tmp("total_time") = "00:00:00"
                            tmp("service_time") = "00:00:00"
                            tmp("productivity") = "00:00:00"
                            tmp("non_productivity") = "00:00:00"
                            tmp("est_ot") = "00:00:00"

                            tmp("prod_learning") = "00:00:00"
                            tmp("prod_stand_by") = "00:00:00"
                            tmp("prod_brief") = "00:00:00"
                            tmp("prod_warp_up") = "00:00:00"
                            tmp("prod_consult") = "00:00:00"
                            tmp("prod_other") = "00:00:00"
                            tmp("total_productivity") = "00:00:00"

                            tmp("nprod_lunch") = "00:00:00"
                            tmp("nprod_leave") = "00:00:00"
                            tmp("nprod_change_counter") = "00:00:00"
                            tmp("nprod_home") = "00:00:00"
                            tmp("nprod_mini_break") = "00:00:00"
                            tmp("nprod_restroom") = "00:00:00"
                            tmp("nprod_other") = "00:00:00"
                            tmp("total_non_productivity") = "00:00:00"

                            If Convert.ToInt64(tmp("working_day")) > 0 Then
                                tmp("total_time") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_total_time")) / Convert.ToInt32(tmp("working_day")))
                                tmp("service_time") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_service_time")) / Convert.ToInt32(tmp("working_day")))
                            End If
                            If Convert.ToInt64(tmp("count_productivity")) > 0 Then
                                tmp("productivity") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_productivity")) / Convert.ToInt32(tmp("count_productivity")))
                            End If
                            If Convert.ToInt64(tmp("count_non_productivity")) > 0 Then
                                tmp("non_productivity") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_non_productivity")) / Convert.ToInt32(tmp("count_non_productivity")))
                            End If
                            If Convert.ToInt64(tmp("count_est_ot")) > 0 Then
                                tmp("est_ot") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_est_ot")) / Convert.ToInt32(tmp("count_est_ot")))
                            End If

                            '### Productivity
                            If Convert.ToInt64(tmp("count_prod_learning")) > 0 Then
                                tmp("prod_learning") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_prod_learning")) / Convert.ToInt32(tmp("count_prod_learning")))
                            End If
                            If Convert.ToInt64(tmp("count_prod_stand_by")) > 0 Then
                                tmp("prod_stand_by") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_prod_stand_by")) / Convert.ToInt32(tmp("count_prod_stand_by")))
                            End If
                            If Convert.ToInt64(tmp("count_prod_brief")) > 0 Then
                                tmp("prod_brief") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_prod_brief")) / Convert.ToInt32(tmp("count_prod_brief")))
                            End If
                            If Convert.ToInt64(tmp("count_prod_warp_up")) > 0 Then
                                tmp("prod_warp_up") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_prod_warp_up")) / Convert.ToInt32(tmp("count_prod_warp_up")))
                            End If
                            If Convert.ToInt64(tmp("count_prod_consult")) > 0 Then
                                tmp("prod_consult") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_prod_consult")) / Convert.ToInt32(tmp("count_prod_consult")))
                            End If
                            If Convert.ToInt64(tmp("count_prod_other")) > 0 Then
                                tmp("prod_other") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_prod_other")) / Convert.ToInt32(tmp("count_prod_other")))
                            End If

                            '### Non Productivity
                            If Convert.ToInt64(tmp("count_nprod_lunch")) > 0 Then
                                tmp("nprod_lunch") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_lunch")) / Convert.ToInt32(tmp("count_nprod_lunch")))
                            End If
                            If Convert.ToInt64(tmp("count_nprod_leave")) > 0 Then
                                tmp("nprod_leave") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_leave")) / Convert.ToInt32(tmp("count_nprod_leave")))
                            End If
                            If Convert.ToInt64(tmp("count_nprod_change_counter")) > 0 Then
                                tmp("nprod_change_counter") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_change_counter")) / Convert.ToInt32(tmp("count_nprod_change_counter")))
                            End If
                            If Convert.ToInt64(tmp("count_nprod_home")) > 0 Then
                                tmp("nprod_home") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_home")) / Convert.ToInt32(tmp("count_nprod_home")))
                            End If
                            If Convert.ToInt64(tmp("count_nprod_mini_break")) > 0 Then
                                tmp("nprod_mini_break") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_mini_break")) / Convert.ToInt32(tmp("count_nprod_mini_break")))
                            End If
                            If Convert.ToInt64(tmp("count_nprod_restroom")) > 0 Then
                                tmp("nprod_restroom") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_restroom")) / Convert.ToInt32(tmp("count_nprod_restroom")))
                            End If
                            If Convert.ToInt64(tmp("count_nprod_other")) > 0 Then
                                tmp("nprod_other") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(tmp("sum_nprod_other")) / Convert.ToInt32(tmp("count_nprod_other")))
                            End If
                            
                            ret.Rows.Add(tmp)
                        Next
                    End If
                    uDt.Dispose()

                End If
                dt.Dispose()

            Catch ex As Exception
                ret = New DataTable
                CreateLogFile("ReportsStaffAttendanceENG", "GetReportDataByPeriodDate", ex.Message & vbNewLine & ex.StackTrace)
            End Try

            Return ret
        End Function

        'Private Function GetReportDataByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        '    Dim ret As New DataTable
        '    ret.Columns.Add("user_id")
        '    ret.Columns.Add("username")
        '    ret.Columns.Add("user_code")
        '    ret.Columns.Add("staff_name")
        '    ret.Columns.Add("ST")
        '    ret.Columns.Add("ET")

        '    Dim SumST As Long = 0
        '    Dim CountST As Long = 0
        '    Dim SumET As Long = 0
        '    Dim CountET As Long = 0


        '    Dim tmpDt As New DataTable
        '    tmpDt.Columns.Add("user_id")
        '    tmpDt.Columns.Add("username")
        '    tmpDt.Columns.Add("user_code")
        '    tmpDt.Columns.Add("staff_name")
        '    tmpDt.Columns.Add("SUM_ST", GetType(Long))
        '    tmpDt.Columns.Add("SUM_ET", GetType(Long))
        '    tmpDt.Columns.Add("COUNT_ST", GetType(Long))
        '    tmpDt.Columns.Add("COUNT_ET", GetType(Long))

        '    Dim CurrDate As DateTime = FirstDay
        '    Do
        '        Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
        '        Dim dt As DataTable = lnq.GetListBySql(GetQuery(CurrDate), shTrans.Trans)
        '        If dt.Rows.Count > 0 Then
        '            Dim i As Integer = 0
        '            For i = 0 To dt.Rows.Count - 1
        '                Dim dr As DataRow = dt.Rows(i)
        '                tmpDt.DefaultView.RowFilter = "user_id='" & dr("user_id") & "' and username='" & dr("username") & "' and user_code='" & dr("user_code") & "' and staff_name = '" & dr("staff_name") & "'"
        '                If tmpDt.DefaultView.Count = 0 Then
        '                    Dim tmpDr As DataRow = tmpDt.NewRow
        '                    tmpDr("user_id") = dr("user_id")
        '                    tmpDr("username") = dr("username")
        '                    tmpDr("user_code") = dr("user_code")
        '                    tmpDr("staff_name") = dr("staff_name")
        '                    If dr("st").ToString.Trim <> "" Then
        '                        tmpDr("SUM_ST") = ReportsENG.GetSecFromTimeFormat(dr("ST"))
        '                        tmpDr("COUNT_ST") = 1
        '                    Else
        '                        tmpDr("COUNT_ST") = 0
        '                    End If
        '                    If dr("et").ToString.Trim <> "" Then
        '                        tmpDr("SUM_ET") = ReportsENG.GetSecFromTimeFormat(dr("ET"))
        '                        tmpDr("COUNT_ET") = 1
        '                    Else
        '                        tmpDr("COUNT_ET") = 0
        '                    End If
        '                    tmpDt.Rows.Add(tmpDr)
        '                Else
        '                    For j As Integer = 0 To tmpDt.Rows.Count - 1
        '                        Dim tmpDr As DataRow = tmpDt.Rows(j)
        '                        Dim vUserID As String = IIf(Convert.IsDBNull(tmpDr("user_id")) = False, tmpDr("user_id"), "")
        '                        Dim vUserName As String = IIf(Convert.IsDBNull(tmpDr("username")) = False, tmpDr("username"), "")
        '                        Dim vUserCode As String = IIf(Convert.IsDBNull(tmpDr("user_code")) = False, tmpDr("user_code"), "")
        '                        Dim vStaffName As String = IIf(Convert.IsDBNull(tmpDr("staff_name")) = False, tmpDr("staff_name"), "")

        '                        If vUserID = dr("user_id") And vUserName = dr("username") And vUserCode = dr("user_code") And vStaffName = dr("staff_name") Then
        '                            tmpDt.Rows(j)("user_id") = dr("user_id")
        '                            tmpDt.Rows(j)("username") = dr("username")
        '                            tmpDt.Rows(j)("user_code") = dr("user_code")
        '                            tmpDt.Rows(j)("staff_name") = dr("staff_name")
        '                            If dr("st").ToString.Trim <> "" Then
        '                                tmpDt.Rows(j)("SUM_ST") += ReportsENG.GetSecFromTimeFormat(dr("ST"))
        '                                tmpDt.Rows(j)("COUNT_ST") += 1
        '                            End If
        '                            If dr("et").ToString.Trim <> "" Then
        '                                tmpDt.Rows(j)("SUM_ET") += ReportsENG.GetSecFromTimeFormat(dr("ET"))
        '                                tmpDt.Rows(j)("COUNT_ET") += 1
        '                            End If
        '                        End If
        '                    Next
        '                End If
        '            Next
        '            tmpDt.Dispose()
        '        End If
        '        dt.Dispose()
        '        lnq = Nothing

        '        CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
        '    Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")

        '    For Each dr As DataRow In tmpDt.Rows
        '        Dim retDr As DataRow = ret.NewRow
        '        retDr("user_id") = dr("user_id")
        '        retDr("username") = dr("username")
        '        retDr("user_code") = dr("user_code")
        '        retDr("staff_name") = dr("staff_name")
        '        retDr("ST") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("sum_st") / dr("count_st")))
        '        retDr("ET") = ReportsENG.GetFormatTimeFromSec(Convert.ToInt64(dr("sum_et") / dr("count_et")))
        '        ret.Rows.Add(retDr)
        '    Next

        '    Return ret
        'End Function

        Dim _WorkingDayByPeriod As Integer = 0

        Private Function GetSumTotalTimeByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As String
            Dim ret As String = ""
            Dim CurrDate As DateTime = FirstDay
            Dim TotalTime As Long = 0
            'Dim i As Integer = 0
            Do
                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                Dim dt As DataTable = lnq.GetListBySql(GetQuery(CurrDate), shTrans.Trans)
                lnq = Nothing
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "user_id='" & UserID & "'"

                    If dt.DefaultView.Count > 0 Then
                        If Convert.IsDBNull(dt.DefaultView(0)("ST")) = False Then
                            Dim TmpTime As Long = GetSecFromTimeFormat(dt.DefaultView(0)("ET")) - GetSecFromTimeFormat(dt.DefaultView(0)("ST"))
                            TotalTime += TmpTime
                            If TmpTime > 0 Then _WorkingDayByPeriod += 1
                        End If
                    End If
                End If
                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")
            If TotalTime > 0 Then
                ret = ReportsENG.GetFormatTimeFromSec(TotalTime)
            End If

            Return ret
        End Function

        Private Function GetSumServiceTimeByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ReasonData
            Dim ret As ReasonData
            Dim CurrDate As DateTime = FirstDay
            Do
                'Dim vSumTotalTime As Integer=ge
                Dim vTotProd As Integer = GetSumReasonByDate(CurrDate, UserID, "", "1", shTrans)
                Dim vTotNonProd As Integer = GetSumReasonByDate(CurrDate, UserID, "", "0", shTrans)

                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                Dim dt As DataTable = lnq.GetListBySql(GetQuery(CurrDate), shTrans.Trans)
                lnq = Nothing
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "user_id='" & UserID & "'"

                    If dt.DefaultView.Count > 0 Then
                        If Convert.IsDBNull(dt.DefaultView(0)("ST")) = False Then
                            Dim TmpTime As Long = GetSecFromTimeFormat(dt.DefaultView(0)("ET")) - GetSecFromTimeFormat(dt.DefaultView(0)("ST"))
                            If TmpTime > 0 Then
                                ret.SumReasonTime += (TmpTime - (+vTotProd + vTotNonProd))
                                ret.CountReasonTime += 1

                                'lnq.SERVICE_TIME = GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.TOTAL_TIME) - (GetSecFromTimeFormat(Prod) + GetSecFromTimeFormat(NonProd)))
                            End If
                        End If
                    End If
                End If
                lnq = Nothing
                dt.Dispose()

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")

            Return ret
        End Function


        Private Function GetSumProdutivityByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ReasonData
            Dim ret As ReasonData
            Dim CurrDate As DateTime = FirstDay
            Do
                Dim vTotProd As Integer = GetSumReasonByDate(CurrDate, UserID, "", "1", shTrans)
                Dim vTotNonProd As Integer = GetSumReasonByDate(CurrDate, UserID, "", "0", shTrans)

                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                Dim dt As DataTable = lnq.GetListBySql(GetQuery(CurrDate), shTrans.Trans)
                lnq = Nothing
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "user_id='" & UserID & "'"

                    If dt.DefaultView.Count > 0 Then
                        If Convert.IsDBNull(dt.DefaultView(0)("ST")) = False Then
                            Dim TmpTime As Long = GetSecFromTimeFormat(dt.DefaultView(0)("ET")) - GetSecFromTimeFormat(dt.DefaultView(0)("ST"))
                            If TmpTime > 0 Then
                                Dim ServiceTime As Long = (TmpTime - (vTotProd + vTotNonProd))
                                ret.SumReasonTime += ServiceTime + vTotProd
                                ret.CountReasonTime += 1

                                'lnq.SERVICE_TIME = GetFormatTimeFromSec(GetSecFromTimeFormat(lnq.TOTAL_TIME) - (GetSecFromTimeFormat(Prod) + GetSecFromTimeFormat(NonProd)))
                            End If
                        End If
                    End If
                End If
                lnq = Nothing
                dt.Dispose()

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")

            Return ret
        End Function

        Private Function GetSumEstOTByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ReasonData
            Dim ret As ReasonData
            Dim CurrDate As DateTime = FirstDay
            Do
                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                Dim dt As DataTable = lnq.GetListBySql(GetQuery(CurrDate), shTrans.Trans)
                lnq = Nothing
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "user_id='" & UserID & "'"

                    If dt.DefaultView.Count > 0 Then
                        If Convert.IsDBNull(dt.DefaultView(0)("ST")) = False Then
                            Dim TmpTime As Long = GetSecFromTimeFormat(dt.DefaultView(0)("ET")) - GetSecFromTimeFormat(dt.DefaultView(0)("ST"))
                            If TmpTime - (450 * 60) > 0 Then
                                ret.SumReasonTime += (TmpTime - (450 * 60))
                                ret.CountReasonTime += 1
                            End If
                        End If
                    End If
                End If
                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")
            
            Return ret
        End Function

        Protected Function GetAvgReasonByPeriodDate(ByVal FirstDay As DateTime, ByVal LastDay As DateTime, ByVal UserID As Integer, ByVal ReasonName As String, ByVal Productive As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Integer
            Dim ret As Integer = 0
            Dim CurrDate As DateTime = FirstDay
            Dim SumItemTime As Long = 0
            Dim i As Integer = 0
            Do
                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                Dim dt As DataTable = lnq.GetListBySql(GetQuery(CurrDate), shTrans.Trans)
                lnq = Nothing
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.RowFilter = "user_id='" & UserID & "'"

                    If dt.DefaultView.Count > 0 Then
                        If Convert.IsDBNull(dt.DefaultView(0)("ST")) = False Then
                            Dim ItemTime As Long = GetSumReasonByDate(CurrDate, UserID, ReasonName, Productive, shTrans)
                            If ItemTime > 0 Then
                                SumItemTime += ItemTime
                            End If
                            i += 1
                        End If
                    End If
                End If

                CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
            Loop While CurrDate.ToString("yyyyMMdd") <= LastDay.ToString("yyyyMMdd")

            Return Convert.ToInt32(SumItemTime / i)
        End Function

        Private Function GetQuery(ByVal ServiceDate As DateTime) As String
            Dim sql As String = ""
            sql += " declare @Date as varchar(8); select @Date = '" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "';" & vbNewLine
            sql += " select main.user_id,username,user_code,fname + ' ' + lname as staff_name"
            sql += " ,CONVERT(varchar(8),min(service_date),114) as ST"
            sql += " ,CONVERT(varchar(8),max(service_date),114) as ET "
            sql += " from TB_LOG_LOGIN as Main "
            sql += " left join TB_USER on Main.user_id = TB_USER.id"
            sql += " where CONVERT(varchar(8),service_date,112) = @Date"
            sql += " and upper(isnull(username,'')) <> 'ADMIN' "
            sql += " group by user_id,username,fname,lname,user_code order by staff_name"
            Return sql
        End Function
    End Class
End Namespace

