Imports Engine.Common
Imports Cen = CenLinqDB.TABLE
Imports System.Windows.Forms
Imports System.Globalization
Imports System.IO

Public Class CSISurveyENG
    Dim _err As String = ""
    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

#Region "Filter Set"
    Private Function GetFilterTimeNoDate(ByVal DateNow As DateTime, ByVal TimeFrom As String, ByVal TimeTo As String) As Boolean
        'กรณี DateFrom และ DateTo ไม่เท่ากับค่าว่าง
        Dim ret As Boolean = False
        If TimeFrom <> "" And TimeTo <> "" Then
            If TimeFrom <= DateNow.ToString("HH:mm") And TimeTo >= DateNow.ToString("HH:mm") Then
                ret = True
            End If
        ElseIf TimeFrom <> "" And TimeTo = "" Then
            If TimeFrom <= DateNow.ToString("HH:mm") Then
                ret = True
            End If
        ElseIf TimeFrom = "" And TimeTo <> "" Then
            If TimeTo >= DateNow.ToString("HH:mm") Then
                ret = True
            End If
        Else
            ret = True
        End If

        Return ret
    End Function

    Private Function GetFilterAllDay(ByVal fLnq As Cen.TbFilterCenLinqDB, ByVal DateNow As DateTime) As Boolean
        Dim ret As Boolean = False
        Dim DateFrom As String = fLnq.PREIOD_DATEFROM.Value.ToString("yyyy-MM-dd", New CultureInfo("en-US"))
        Dim DateTo As String = fLnq.PREIOD_DATETO.Value.ToString("yyyy-MM-dd", New CultureInfo("en-US"))
        Dim TimeFrom As String = fLnq.PREIOD_TIMEFROM
        Dim TimeTo As String = fLnq.PREIOD_TIMETO

        If fLnq.PREIOD_DATEFROM.Value.Year <> 1 And fLnq.PREIOD_DATETO.Value.Year <> 1 Then
            If DateFrom <= DateNow.ToString("yyyy-MM-dd", New CultureInfo("en-US")) And DateTo >= DateNow.ToString("yyyy-MM-dd", New CultureInfo("en-US")) Then
                ret = GetFilterTimeNoDate(DateNow, TimeFrom, TimeTo)
            Else
                ret = False
            End If
        ElseIf fLnq.PREIOD_DATEFROM.Value.Year <> 1 And fLnq.PREIOD_DATETO.Value.Year = 1 Then
            If DateFrom <= DateNow.ToString("yyyy-MM-dd", New CultureInfo("en-US")) Then
                ret = GetFilterTimeNoDate(DateNow, TimeFrom, TimeTo)
            Else
                ret = False
            End If
        ElseIf fLnq.PREIOD_DATEFROM.Value.Year = 1 And fLnq.PREIOD_DATETO.Value.Year <> 1 Then
            If DateFrom <= DateNow.ToString("yyyy-MM-dd", New CultureInfo("en-US")) Then
                ret = GetFilterTimeNoDate(DateNow, TimeFrom, TimeTo)
            Else
                ret = False
            End If
        ElseIf fLnq.PREIOD_DATEFROM.Value.Year = 1 And fLnq.PREIOD_DATETO.Value.Year = 1 Then
            ret = GetFilterTimeNoDate(DateNow, TimeFrom, TimeTo)
        Else
            ret = True
        End If

        Return ret
    End Function

    Private Function GetFilterByDay(ByVal fLnq As Cen.TbFilterCenLinqDB, ByVal DateNow As DateTime) As Boolean
        Dim ret As Boolean = GetFilterAllDay(fLnq, DateNow)
        If ret = True Then
            ret = False
            Dim CaseDay As Integer = DatePart(DateInterval.Weekday, DateTime.Now)
            Select Case CaseDay
                Case 1
                    ret = (fLnq.SCHEDULESUNDAY = "Y")
                Case 2
                    ret = (fLnq.SCHEDULEMONDAY = "Y")
                Case 3
                    ret = (fLnq.SCHEDULETUEDAY = "Y")
                Case 4
                    ret = (fLnq.SCHEDULEWEDDAY = "Y")
                Case 5
                    ret = (fLnq.SCHEDULETHUDAY = "Y")
                Case 6
                    ret = (fLnq.SCHEDULEFRIDAY = "Y")
                Case 7
                    ret = (fLnq.SCHEDULESATDAY = "Y")
            End Select
        End If

        Return ret
    End Function

    Private Sub SetFilterActiveStatus()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim fLnq As New Cen.TbFilterCenLinqDB
                Dim fDt As DataTable = fLnq.GetDataList("active_status = 'Y'", "", trans.Trans)
                trans.CommitTransaction()
                If fDt.Rows.Count > 0 Then
                    For Each fDr As DataRow In fDt.Rows
                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                        If trans.Trans IsNot Nothing Then
                            Dim ret As Boolean = False
                            fLnq.GetDataByPK(fDr("id"), trans.Trans)
                            If fLnq.ID <> 0 Then
                                Dim DateNow As DateTime = DateTime.Now
                                Dim TimeNow As String = CenLinqDB.Common.Utilities.SqlDB.GetDateNow("T", trans.Trans)
                                If fLnq.SCHEDULETYPEDAY = "0" Then
                                    'สำรวจทุกวัน
                                    ret = GetFilterAllDay(fLnq, DateNow)
                                ElseIf fLnq.SCHEDULETYPEDAY = "1" Then
                                    'สำรวจตามวัน
                                    ret = GetFilterByDay(fLnq, DateNow)
                                End If

                                If ret = True Then
                                    'duration_type (ประเภทเวลาการ Filter)
                                    '0 = สำรวจหลังบริการเสร็จ
                                    '1 = สำรวจทุกๆ
                                    Dim ChkTime As DateTime
                                    Select Case fLnq.DURATION_TYPE
                                        Case "0"
                                            ChkTime = DateAdd(DateInterval.Minute, (0 - fLnq.DURATION_AFTER_MIN.Value), DateTime.Now)
                                        Case "1"
                                            ChkTime = DateAdd(DateInterval.Minute, (0 - fLnq.DURATION_EVERY_MIN.Value), DateTime.Now)
                                    End Select

                                    If ChkTime < fLnq.LAST_SAVE_FILTER Then
                                        fLnq.PROCESS_STATUS = "N"
                                    Else
                                        fLnq.PROCESS_STATUS = "Y"
                                    End If
                                Else
                                    fLnq.PROCESS_STATUS = "N"
                                End If

                                If fLnq.UpdateByPK("CSISurveyENG.SetFilterActiveStatus", trans.Trans) = True Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("CSISurveyENG.SetFilterActiveStatus", "FilterName : " & fLnq.FILTER_NAME & " " & fLnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                End If
                            End If
                        End If
                    Next
                    fDt = Nothing
                End If
                fLnq = Nothing
            Else
                FunctionEng.SaveErrorLog("CSISurveyENG.SetFilterActiveStatus", trans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                CreateLogFile("CSISurveyENG.SetFilterActiveStatus", trans.ErrorMessage)
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSISurveyENG.SetFilterActiveStatus", ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
            CreateLogFile("SetFilterActiveStatus", ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub UpdateLastProcTime(ByVal FilterLnq As Cen.TbFilterCenLinqDB)
        Dim fTrans As New CenLinqDB.Common.Utilities.TransactionDB
        If FilterLnq.UpdateBySql("update tb_filter set last_proc_time=getdate() where id = " & FilterLnq.ID, fTrans.Trans) = True Then
            fTrans.CommitTransaction()
        Else
            fTrans.RollbackTransaction()
            FunctionEng.SaveErrorLog("CSISurverENG.SendCSI", "update tb_filter set lase_proc_time=getdate() where id = " & FilterLnq.ID & " ###ErrorMsg=" & FilterLnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
        End If
    End Sub

    Public Sub CreateLogFile(ByVal FunctionName As String, ByVal TextMsg As String)
        Try
            Dim LogDir As String = Application.StartupPath & "\CSILog\"
            If Directory.Exists(LogDir) = False Then
                Directory.CreateDirectory(LogDir)
            End If

            Dim FILE_NAME As String = LogDir & DateTime.Now.ToString("yyyyMMddHH") & ".log"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & "CSISurverENG." & FunctionName & " : " & TextMsg & Chr(13) & Chr(13))
            objWriter.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteFilterTempData()
        Try
            Dim sql As String = "delete from TB_FILTER_TEMP_DATA"
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql)
        Catch ex As Exception

        End Try
    End Sub


    Public Sub FilterData()
        Try
            SetFilterActiveStatus()
            DeleteFilterTempData()

            Dim fLnq As New Cen.TbFilterCenLinqDB
            Dim fSql As String = "process_status='Y' and active_status = 'Y' and cal_target='Y'"
            Dim fDt As DataTable = fLnq.GetDataList(fSql, "", Nothing)
            If fDt.Rows.Count > 0 Then
                For Each fDr As DataRow In fDt.Rows
                    Dim FilterLnq As New Cen.TbFilterCenLinqDB
                    FilterLnq.GetDataByPK(fDr("id"), Nothing)
                    If FilterLnq.ID <> 0 Then
                        'Filter By Template
                        Dim WhText As String = ""
                        Dim vCategory As String = GetCategoryDesc(FilterLnq.CATEGORY)
                        Dim vContactClass As String = GetContactClassDesc(FilterLnq.CONTACT_CLASS)

                        'Category
                        If FilterLnq.BLANK_CATEGORY = "Y" Then
                            WhText += " and isnull(category,'') in ('" & vCategory & "','') " & vbNewLine
                        Else
                            WhText += " and category = '" & vCategory & "' " & vbNewLine
                        End If

                        'ContactClass
                        If FilterLnq.BLANK_CONTACT_CLASS = "Y" Then
                            WhText += " and isnull(contact_class,'') in ('" & vContactClass & "','') " & vbNewLine
                        Else
                            WhText += " and contact_class = '" & vContactClass & "' " & vbNewLine
                        End If

                        'Nationality
                        If FilterLnq.NATIONALITY.Trim <> "" Then
                            Dim vNation As String = ""
                            Dim IsCheckThai As Boolean = False
                            Dim IsCheckEng As Boolean = False
                            Dim IsCheckOther As Boolean = False
                            Dim IsCheckBlank As Boolean = False

                            If FilterLnq.NATIONALITY.Trim.IndexOf("THAI") > -1 Then
                                IsCheckThai = True
                            End If
                            If FilterLnq.NATIONALITY.Trim.IndexOf("ENG") > -1 Then
                                IsCheckEng = True
                            End If
                            If FilterLnq.NATIONALITY.Trim.IndexOf("OTHER") > -1 Then
                                IsCheckOther = True
                            End If
                            If FilterLnq.NATIONALITY.Trim.IndexOf("BLANK") > -1 Then
                                IsCheckBlank = True
                            End If

                            If IsCheckOther = True Then
                                If IsCheckThai = False And IsCheckEng = False Then
                                    vNation += " and UPPER(prefer_lang) not in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
                                Else
                                    If IsCheckThai = False Then
                                        vNation += " and UPPER(prefer_lang) not in ('THAILAND','THAI')" & vbNewLine
                                    End If
                                    If IsCheckEng = False Then
                                        vNation += " and UPPER(prefer_lang) not in ('ENGLAND','ENG','United Kingdom')" & vbNewLine
                                    End If
                                End If
                            Else
                                If IsCheckThai = True And IsCheckEng = True Then
                                    vNation += " and UPPER(prefer_lang) in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
                                Else
                                    If IsCheckThai = True And IsCheckEng = False Then
                                        vNation += " and UPPER(prefer_lang) in ('THAILAND','THAI')" & vbNewLine
                                    ElseIf IsCheckThai = False And IsCheckEng = True Then
                                        vNation += " and UPPER(prefer_lang) in ('ENGLAND','ENG','United Kingdom')"
                                    ElseIf IsCheckThai = False And IsCheckEng = False Then
                                        vNation += " and UPPER(prefer_lang) not in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')"
                                    End If
                                End If
                            End If
                            If IsCheckBlank = False Then
                                vNation += " and isnull(prefer_lang,'')<>''" & vbNewLine
                            Else
                                'ถ้า Tick Checkbox Nationality ทั้ง 4 ค่าเลย ก็ไม่ต้อง ตรวจสอบ Nationality
                                If IsCheckThai = True And IsCheckEng = True And IsCheckOther = True And IsCheckBlank = True Then

                                Else
                                    If IsCheckThai = False And IsCheckEng = False And IsCheckOther = False Then
                                        'ถ้า Tick Blank อย่างเดียว
                                        vNation = " and isnull(prefer_lang,'')=''"
                                    Else
                                        vNation = " and (" & vNation.Substring(4) & " or isnull(prefer_lang,'')='')" & vbNewLine
                                    End If
                                End If
                            End If
                            WhText += vNation
                        End If

                        If FilterLnq.SEGMENT <> "0" Then
                            Dim tmp As String = ""
                            For Each seg As String In Split(FilterLnq.SEGMENT, ",")
                                If tmp = "" Then
                                    tmp = "'" & seg & "'"
                                Else
                                    tmp += "," & "'" & seg & "'"
                                End If
                            Next
                            WhText += " and segment in (" & tmp & ") " & vbNewLine
                        End If

                        'หา Service ของ Filter
                        Dim sDt As New DataTable
                        sDt = FilterLnq.CHILD_TB_FILTER_SERVICE_tb_filter_id
                        If sDt.Rows.Count > 0 Then
                            sDt.DefaultView.RowFilter = "target_percent > 0"
                            If sDt.DefaultView.Count > 0 Then
                                Dim ServiceID As String = ""
                                For Each sDr As DataRowView In sDt.DefaultView
                                    If ServiceID = "" Then
                                        ServiceID = sDr("tb_item_id")
                                    Else
                                        ServiceID += "," & sDr("tb_item_id")
                                    End If
                                Next
                                WhText += " and master_itemid in (" & ServiceID & ")" & vbNewLine
                            End If
                            sDt.Dispose()
                        End If

                        If FilterLnq.NETWORK_TYPE.ToUpper <> "ALL" Then
                            WhText += " and network_type = '" & FilterLnq.NETWORK_TYPE & "'" & vbNewLine
                        End If

                        'หา Shop ที่จะต้องทำการสำรวจโดย Filter นี้
                        Dim shLnq As New Cen.TbFilterShopCenLinqDB
                        Dim shDt As DataTable
                        If FilterLnq.SURVEY_ALL_SHOP = "Y" Then
                            Dim shSql As String = "Select sh.id as tb_shop_id, "
                            shSql += " isnull(fs.sh_template_code,'" & FilterLnq.TEMPLATE_CODE & "') sh_template_code "
                            shSql += " from tb_shop sh "
                            shSql += " left join tb_filter_shop fs on sh.id=fs.tb_shop_id  and fs.tb_filter_id='" & FilterLnq.ID & "'"
                            shSql += " where sh.active = 'Y'"
                            shDt = shLnq.GetListBySql(shSql, Nothing)
                        Else
                            shDt = shLnq.GetDataList("tb_filter_id = " & fDr("id"), "", Nothing)
                        End If

                        If shDt.Rows.Count > 0 Then
                            Dim dt As New DataTable
                            dt = GetQDataAllShop(FilterLnq, shDt, WhText)
                            If dt.Rows.Count > 0 Then
                                Dim vDateNow As DateTime = DateTime.Now
                                For i As Integer = 0 To dt.Rows.Count - 1
                                    Dim ItemID As Long = IIf(Convert.IsDBNull(dt.Rows(i)("master_itemid")) = False, Convert.ToInt64(dt.Rows(i)("master_itemid")), 0)
                                    Dim fSV As DataView = FilterLnq.CHILD_TB_FILTER_SERVICE_tb_filter_id.DefaultView
                                    If FilterLnq.TARGET_UNLIMITED = "N" Then
                                        fSV.RowFilter = "tb_item_id = '" & ItemID & "' and target_percent > 0"
                                    End If
                                    If fSV.Count > 0 Then
                                        Dim ShopID As Long = Convert.ToInt64(dt.Rows(i)("tb_shop_id"))
                                        Dim ShopABB As String = dt.Rows(i)("shop_abb")
                                        Dim ShopNameEN As String = dt.Rows(i)("shop_name_en")
                                        'ตรวจสอบ Target ถ้าได้ผลลัพธ์ตามจำนวน Target ที่ต้องการแล้วก็ไม่ต้องส่งอีก
                                        If ChkTarget(FilterLnq, ShopID, ItemID, Convert.ToDateTime(dt.Rows(i)("end_time"))) = False Then
                                            If ChkJustInsert(dt.Rows(i)("mobile_no")) = False Then
                                                'Queue_unique_id Format = QueueNo + ShopAbb+yyyyMMddHHmmss  EX. A104PK25550326172423
                                                Dim QUniqueID As String = dt.Rows(i)("queue_no") & ShopABB & Convert.ToDateTime(dt.Rows(i)("service_date")).ToString("yyyyMMddHHmmss", New CultureInfo("en-US"))
                                                Dim lnq As New Cen.TbFilterDataCenLinqDB
                                                If lnq.ChkDuplicateByQUEUE_UNIQUE_ID(QUniqueID, lnq.ID, Nothing) = False Then
                                                    lnq.SHOP_ID = ShopID
                                                    lnq.SERVICE_DATE = dt.Rows(i)("service_date")
                                                    lnq.QUEUE_NO = dt.Rows(i)("queue_no")
                                                    lnq.QUEUE_UNIQUE_ID = QUniqueID
                                                    lnq.MOBILE_NO = dt.Rows(i)("mobile_no")
                                                    lnq.USERNAME = dt.Rows(i)("username")
                                                    lnq.STAFF_NAME = dt.Rows(i)("staff_name")
                                                    If Convert.IsDBNull(dt.Rows(i)("customer_name")) = False Then lnq.CUSTOMER_NAME = dt.Rows(i)("customer_name")
                                                    lnq.TB_FILTER_ID = FilterLnq.ID
                                                    lnq.TB_ITEM_ID = ItemID
                                                    lnq.TEMPLATE_CODE = dt.Rows(i)("sh_template_code")
                                                    lnq.FILTER_TIME = vDateNow
                                                    lnq.RESULT_STATUS = "0"
                                                    lnq.END_TIME = dt.Rows(i)("end_time")
                                                    If Convert.IsDBNull(dt.Rows(i)("network_type")) = False Then lnq.NETWORK_TYPE = dt.Rows(i)("network_type")
                                                    lnq.NPS_SCORE = -1
                                                    lnq.SEGMENT = dt.Rows(i)("segment")

                                                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                                                    If lnq.InsertData("CSISurverENG.SendCSI", trans.Trans) Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("CSISurverENG.SendCSI", ShopNameEN & " " & lnq.MOBILE_NO & " " & lnq.QUEUE_NO & " " & lnq.SERVICE_DATE & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                                    End If
                                                End If
                                                lnq = Nothing
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                            dt.Dispose()
                            DeleteFilterTempData()
                        End If
                        shDt.Dispose()
                        shLnq = Nothing
                    End If
                Next
                fDt.Dispose()
            End If
        Catch ex As Exception
            CreateLogFile("FilterData", ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Function IsCompletedTarget(ByVal FilterLnq As Cen.TbFilterCenLinqDB, ByVal shop_id As String) As Boolean
        If FilterLnq.TARGET_UNLIMITED = "Y" Then
            Return True
        End If


        Dim _acsualsurvey As Integer = 0
        Dim sql As String = "select count(ID) cnt from TB_FILTER_DATA where tb_filter_id = '" & FilterLnq.ID & _
        "' and shop_id ='" & shop_id & "'  and result_status =2"
        Dim dt As New DataTable
        Dim fLnq As New Cen.TbFilterDataCenLinqDB
        dt = fLnq.GetListBySql(sql, Nothing)
        If dt.Rows.Count > 0 Then
            _acsualsurvey = CInt(dt.Rows(0)("cnt"))
        End If

        If _acsualsurvey >= FilterLnq.TARGET.Value Then
            Return False
        End If
        Return True
    End Function

    Private Sub DeleteTempTarget(ByVal FilterTemplateID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dSql As String = "delete from tb_filter_temp_target where tb_filter_id=" & FilterTemplateID
        CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        trans.CommitTransaction()
    End Sub

    Private Function ChkTarget(ByVal FilterLnq As Cen.TbFilterCenLinqDB, ByVal ShopID As Long, ByVal ServiceID As Long, ByVal EndServiceTime As DateTime) As Boolean
        Dim ret As Boolean = False
        If FilterLnq.TARGET_UNLIMITED = "N" Then
            Dim sql As String = "select [target],[username],end_time_from,end_time_to "
            sql += " from TB_FILTER_TEMP_TARGET"
            sql += " where tb_filter_id = '" & FilterLnq.ID & "'"
            sql += " and tb_item_id = '" & ServiceID & "'"
            sql += " and tb_shop_id = '" & ShopID & "'"
            sql += " and convert(varchar(8),service_date,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
            sql += " and '" & EndServiceTime.ToString("HH:mm") & "' between end_time_from and end_time_to"

            Dim tDt As New DataTable
            Dim tLnq As New Cen.TbFilterTempTargetCenLinqDB
            tDt = tLnq.GetListBySql(sql, Nothing)
            If tDt.Rows.Count > 0 Then
                sql = "select count(fd.id) filter"
                sql += " from tb_filter_data fd"
                sql += " where fd.tb_filter_id='" & FilterLnq.ID & "'"
                sql += " and fd.tb_item_id='" & ServiceID & "'"
                sql += " and fd.shop_id = '" & ShopID & "'"
                sql += " and fd.result_status='2' "
                sql += " and convert(varchar(8),fd.end_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
                sql += " and CONVERT(varchar(5),fd.end_time,114) between '" & Convert.ToDateTime(tDt.Rows(0)("end_time_from")).ToString("HH:mm") & "' and '" & Convert.ToDateTime(tDt.Rows(0)("end_time_to")).ToString("HH:mm") & "'"
                If Convert.IsDBNull(tDt.Rows(0)("username")) = False Then
                    sql += " and fd.username in (" & tDt.Rows(0)("username").ToString & ")"
                End If

                Dim dt As New DataTable
                Dim lnq As New Cen.TbFilterDataCenLinqDB
                dt = lnq.GetListBySql(sql, Nothing)
                If dt.Rows.Count > 0 Then
                    If Convert.ToDouble(dt.Rows(0)("filter")) >= Convert.ToDouble(tDt.Rows(0)("target")) Then
                        ret = True
                    End If
                    dt.Dispose()
                End If
                lnq = Nothing
            Else
                'ถ้ากรณีไม่พบข้อมูลใน TB_FILTER_TEMP_TARGET เป็นไปได้ว่าอาจจะเป็น Shop ใหม่ที่เพิ่งเพิ่มเข้าไป ในกรณีนี้ให้ Cal Target ใหม่
                DeleteTempTarget(FilterLnq.ID)
                If SaveFilterTempTarget(FilterLnq.ID, "AisCSIAgent") = False Then
                    FunctionEng.SaveErrorLog("CSISurveyENG.CalTarget", _err, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                End If
                ret = ChkTarget(FilterLnq, ShopID, ServiceID, EndServiceTime)
            End If
            tDt.Dispose()
            tLnq = Nothing
        End If

        Return ret
    End Function

    Private Function ChkJustInsert(ByVal MobileNo As String) As Boolean
        Dim ret As Boolean = False
        Dim lnq As New Cen.TbFilterDataCenLinqDB
        Dim dt As DataTable = lnq.GetDataList("mobile_no = '" & MobileNo & "' and result_status <> '2' and convert(varchar(8),filter_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "", Nothing)
        If dt.Rows.Count > 0 Then
            ret = True
        End If
        dt.Dispose()
        lnq = Nothing

        Return ret
    End Function

    Private Function GetFilterMobileNo2Month(ByVal dt As DataTable) As DataTable
        Dim FilterLastDay As String = FunctionEng.GetQisDBConfig("CSI_FILTER_LASTDAY")
        If FilterLastDay.Trim = "" Then
            FilterLastDay = "60"
        End If

        Dim lnq As New Cen.TbFilterDataCenLinqDB
        Dim WhText As String = "result_status >= '1' "
        WhText += " and  convert(varchar(10),send_time,120) >= convert(varchar(10),DATEADD(DAY, " & (0 - Convert.ToInt16(FilterLastDay)) & " , getdate()),120) "

        Dim fDt As DataTable = lnq.GetDataList(WhText, "", Nothing)
        If fDt.Rows.Count > 0 Then
            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                fDt.DefaultView.RowFilter = "mobile_no = '" & dt.Rows(i)("customer_id").ToString & "'"
                If fDt.DefaultView.Count > 0 Then
                    dt.Rows.RemoveAt(i)
                End If
                fDt.DefaultView.RowFilter = ""
            Next
            fDt.Dispose()
        End If
        lnq = Nothing

        Return dt
    End Function

    Private Function GetCategoryDesc(ByVal CategoryCode As String) As String
        Dim ret As String = ""
        Dim lnq As New CenLinqDB.TABLE.TbCustomerMappingLinq
        Dim dt As New DataTable
        dt = lnq.GetDataList("mapping_code = '" & CategoryCode & "' and field_name = 'GROUP_CODE' ", "", Nothing)
        If dt.Rows.Count > 0 Then
            ret = dt.Rows(0)("display_value").ToString
        End If
        dt.Dispose()
        lnq = Nothing

        Return ret
    End Function

    Private Function GetContactClassDesc(ByVal ContactClassCode As String) As String
        Dim ret As String = ""
        Dim lnq As New CenLinqDB.TABLE.TbCustomerMappingLinq
        Dim dt As New DataTable
        dt = lnq.GetDataList("mapping_code = '" & ContactClassCode & "' and field_name = 'CUST_CLASS' ", "", Nothing)
        If dt.Rows.Count > 0 Then
            ret = dt.Rows(0)("display_value").ToString
        End If
        dt.Dispose()
        lnq = Nothing

        Return ret
    End Function

    Private Function GetQDataAllShop(ByVal FilterLnq As Cen.TbFilterCenLinqDB, ByVal shDt As DataTable, ByVal WhText As String) As DataTable
        Dim dt As New DataTable

        Try
            Dim uDt As New DataTable
            uDt = FilterLnq.CHILD_TB_FILTER_STAFF_tb_filter_id

            For Each shDr As DataRow In shDt.Rows
                Dim ShopLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(shDr("tb_shop_id"))
                Dim template_code As String = shDr("sh_template_code") & ""
                If ShopLnq.ACTIVE = "Y" Then
                    If IsCompletedTarget(FilterLnq, ShopLnq.ID) Then
                        If FilterLnq.DURATION_TYPE = "0" Then
                            'สำรวจหลัง End Queue ไปแล้ว X นาที

                            Dim wh As String = " and DATEADD(MINUTE, " & FilterLnq.DURATION_AFTER_MIN.Value & " ,end_time)<= GETDATE()"
                            wh += " and convert(varchar(8),service_date,112)=convert(varchar(8),getdate(),112)"
                            wh += " and CONVERT(varchar(19), DATEADD(MINUTE, " & (0 - FilterLnq.DURATION_AFTER_MIN.Value) & " , end_time),120) >= '" & FilterLnq.LAST_SAVE_FILTER.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "'"
                            dt.Merge(GetQDataByShop(FilterLnq, ShopLnq, wh + WhText, uDt, template_code))
                        ElseIf FilterLnq.DURATION_TYPE = "1" Then
                            'สำรวจทุกๆ X นาที
                            Dim LastTime As DateTime
                            If FilterLnq.LAST_PROC_TIME.Value.Year = 1 Then
                                FilterLnq.LAST_PROC_TIME = DateTime.Now
                                UpdateLastProcTime(FilterLnq)
                            Else
                                LastTime = FilterLnq.LAST_PROC_TIME
                            End If

                            If DateAdd(DateInterval.Minute, FilterLnq.DURATION_EVERY_MIN.Value, LastTime) <= DateTime.Now Then
                                Dim wh As String = " and convert(varchar(16),end_time,120) between '" & LastTime.ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US")) & "' and CONVERT(varchar(16),getdate(),120) "
                                whText += " and convert(varchar(8), service_date,112)=convert(varchar(8),getdate(),112) "
                                whText += " and CONVERT(varchar(19), DATEADD(MINUTE, " & (0 - FilterLnq.DURATION_EVERY_MIN.Value) & " , end_time),120) >= '" & FilterLnq.LAST_SAVE_FILTER.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "'"
                                dt.Merge(GetQDataByShop(FilterLnq, ShopLnq, wh + WhText, uDt, template_code))
                                UpdateLastProcTime(FilterLnq)
                            End If
                        End If
                    End If 'IsSuccessTarget

                    'GetATSRResult()
                    'SendResultToShop()

                End If
                ShopLnq = Nothing
            Next
            uDt.Dispose()

            If dt.Rows.Count > 0 Then
                Dim FilterLastDay As String = FunctionEng.GetQisDBConfig("CSI_FILTER_LASTDAY")
                If FilterLastDay.Trim = "" Then
                    FilterLastDay = "60"
                End If


                Dim sql As String = "select fd.*, sh.shop_abb, sh.shop_name_en,sh_template_code" & vbNewLine
                sql += " from tb_filter_temp_data fd" & vbNewLine
                sql += " inner join tb_shop sh on sh.id=fd.tb_shop_id"
                sql += " where fd.mobile_no not in (select mobile_no from TB_FILTER_ATT_BACKLIST_MOBILE)" & vbNewLine
                sql += " and fd.mobile_no not in (select mobile_no " & vbNewLine
                sql += "                        from tb_filter_data " & vbNewLine
                sql += "                        where result_status >= '1' " & vbNewLine
                sql += "                        and  convert(varchar(10),send_time,120) >= convert(varchar(10),DATEADD(DAY, " & (0 - Convert.ToInt16(FilterLastDay)) & " , getdate()),120) " & vbNewLine
                sql += " )" & vbNewLine
                Dim tLnq As New Cen.TbFilterTempDataCenLinqDB
                dt = tLnq.GetListBySql(sql, Nothing)
                tLnq = Nothing
            End If
        Catch ex As Exception
            dt = New DataTable
        End Try

        Return dt
    End Function

    Private Function GetQDataByShop(ByVal FilterLnq As Cen.TbFilterCenLinqDB, ByVal ShopLnq As Cen.TbShopCenLinqDB, ByVal whText As String, ByVal UserShopDT As DataTable, ByVal template_code As String, Optional ByVal TimeType As String = "H") As DataTable

        'หาข้อมูล Q ที่ End
        Dim sql As String = ""
        sql += " SELECT '" & ShopLnq.ID & "' shop_id, '" & ShopLnq.SHOP_ABB & "' shop_abb, '" & ShopLnq.SHOP_NAME_EN & "' shop_name_en,'" & template_code & "' sh_template_code," & vbNewLine
        sql += "  * FROM [v_csi_filter_data]" & vbNewLine
        sql += " Where 1=1 " & whText & vbNewLine
        If TimeType = "H" Then
            'เอาคิวที่ End มาแล้วในแต่ละชั่วโมง
            sql += " and end_time >= DATEADD(hour,-1,getdate())" & vbNewLine
            sql += " and convert(varchar(5),end_time,114) between '" & FilterLnq.PREIOD_TIMEFROM & "' and '" & FilterLnq.PREIOD_TIMETO & "'"
        ElseIf TimeType = "D" Then
            sql += " and convert(varchar(10),end_time,120)=convert(varchar(10),getdate(),120)" & vbNewLine
        End If

        Dim dt As New DataTable
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(ShopLnq.ID, "CSISurveyENG.GetQData")
        If shTrans.Trans IsNot Nothing Then
            Dim Qlnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            dt = Qlnq.GetListBySql(sql, shTrans.Trans)
            shTrans.CommitTransaction()
            Qlnq = Nothing
        End If


        'หา User ของ Filter
        If dt.Rows.Count > 0 Then
            If UserShopDT.Rows.Count > 0 Then
                UserShopDT.DefaultView.RowFilter = " shop_id = '" & ShopLnq.ID & "'"
                UserShopDT = UserShopDT.DefaultView.ToTable
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    UserShopDT.DefaultView.RowFilter = "username = '" & dt.Rows(i)("username") & "'"
                    If UserShopDT.DefaultView.Count = 0 Then
                        dt.Rows.RemoveAt(i)
                    End If
                    UserShopDT.DefaultView.RowFilter = ""
                Next
            End If
        End If

        If dt.Rows.Count > 0 Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim re As Boolean = False
            For Each dr As DataRow In dt.Rows
                Try
                    Dim lnq As New Cen.TbFilterTempDataCenLinqDB
                    lnq.TB_SHOP_ID = ShopLnq.ID
                    lnq.TB_FILTER_ID = FilterLnq.ID
                    If Convert.IsDBNull(dr("queue_no")) = False Then lnq.QUEUE_NO = dr("queue_no")
                    If Convert.IsDBNull(dr("service_date")) = False Then lnq.SERVICE_DATE = Convert.ToDateTime(dr("service_date"))
                    If Convert.IsDBNull(dr("end_time")) = False Then lnq.END_TIME = Convert.ToDateTime(dr("end_time"))
                    If Convert.IsDBNull(dr("customer_id")) = False Then lnq.MOBILE_NO = dr("customer_id")
                    If Convert.IsDBNull(dr("customer_name")) = False Then lnq.CUSTOMER_NAME = dr("customer_name")
                    If Convert.IsDBNull(dr("master_itemid")) = False Then lnq.MASTER_ITEMID = dr("master_itemid")
                    If Convert.IsDBNull(dr("item_name")) = False Then lnq.ITEM_NAME = dr("item_name")
                    If Convert.IsDBNull(dr("category")) = False Then lnq.CATEGORY = dr("category")
                    If Convert.IsDBNull(dr("contact_class")) = False Then lnq.CONTACT_CLASS = dr("contact_class")
                    If Convert.IsDBNull(dr("prefer_lang")) = False Then lnq.PREFER_LANG = dr("prefer_lang")
                    If Convert.IsDBNull(dr("segment")) = False Then lnq.SEGMENT = dr("segment")
                    If Convert.IsDBNull(dr("network_type")) = False Then lnq.NETWORK_TYPE = dr("network_type")
                    If Convert.IsDBNull(dr("username")) = False Then lnq.USERNAME = dr("username")
                    If Convert.IsDBNull(dr("staff_name")) = False Then lnq.STAFF_NAME = dr("staff_name")
                    If Convert.IsDBNull(dr("sh_template_code")) = False Then lnq.SH_TEMPLATE_CODE = dr("sh_template_code")

                    re = lnq.InsertData("CSISurveyENG.GetQDataByShop", trans.Trans)
                    If re = False Then
                        FunctionEng.SaveErrorLog("CSISurveyENG.GetQDataByShop", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                        Exit For
                    End If
                Catch ex As Exception
                    FunctionEng.SaveErrorLog("CSISurveyENG.GetQDataByShop", "Exception : " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                    Exit For
                End Try
            Next

            If re = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                dt = New DataTable
            End If
        End If

        Return dt
    End Function
#End Region


#Region "Sent ATSR"
    Private Function CheckSendATSRbyOrderQniqueID(ByVal QueueUniqueID As String, ByVal vID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            'ถ้ามีข้อมูลว่าเคยส่งให้ ATSR ไปแล้วก็ไม่ต้องส่งซ้ำอีก
            Dim lnq As New Cen.TbFilterDataCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("queue_unique_id='" & QueueUniqueID & "' and id<>'" & vID & "'", "", Nothing)
            If dt.Rows.Count > 0 Then
                ret = True
            End If
            dt.Dispose()
            lnq = Nothing
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSISurvey.CheckSendATSRbyOrderQniqueID", ex.Message & vbNewLine & ex.StackTrace, "\ErrorLog\", "CSITWSurveyENG")
        End Try
        Return ret
    End Function


    Public Sub SendATSR()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New Cen.TbFilterDataCenLinqDB
                Dim dt As DataTable = lnq.GetDataList("send_time is null and result_status='0' and convert(varchar(8),filter_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "", trans.Trans)
                trans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    Dim fDt As New DataTable
                    fDt = dt.DefaultView.ToTable(True, "tb_filter_id")
                    If fDt.Rows.Count > 0 Then
                        fDt.Columns.Add("fLnq", GetType(CenLinqDB.TABLE.TbFilterCenLinqDB))
                        For i As Integer = 0 To fDt.Rows.Count - 1
                            Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                            fLnq.GetDataByPK(fDt.Rows(i)("tb_filter_id"), Nothing)
                            fDt.Rows(i)("fLnq") = fLnq
                            fLnq = Nothing
                        Next
                    End If

                    For Each dr As DataRow In dt.Rows
                        Try
                            If CheckSendATSRbyOrderQniqueID(dr("queue_unique_id"), dr("id")) = False Then


                                lnq.GetDataByPK(dr("id"), Nothing)
                                If lnq.ID > 0 Then
                                    Dim eng As New Engine.GateWay.GateWayServiceENG
                                    Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("shop_id"))
                                    If eng.SendATSR(dr("mobile_no"), dr("queue_unique_id"), shLnq.SHOP_ABB, dr("template_code")) = True Then
                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim iLnq As New Cen.TbFilterDataCenLinqDB
                                        iLnq.GetDataByPK(dr("id"), trans.Trans)
                                        iLnq.SEND_TIME = DateTime.Now
                                        iLnq.RESULT_STATUS = "1"

                                        If iLnq.UpdateByPK("CSISurverENG.SendATSR", trans.Trans) = True Then
                                            trans.CommitTransaction()

                                            fDt.DefaultView.RowFilter = "tb_filter_id='" & iLnq.TB_FILTER_ID & "'"
                                            If fDt.DefaultView.Count > 0 Then
                                                Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                                                fLnq = DirectCast(fDt.DefaultView(0)("fLnq"), CenLinqDB.TABLE.TbFilterCenLinqDB)

                                                If fLnq.GEN_ACT_ALL_SURVEY = "Y" Then
                                                    Dim shDt As New DataTable
                                                    shDt = GetShopDetail(lnq.SHOP_ID, Nothing)
                                                    If shDt.Rows.Count > 0 Then
                                                        Dim shDr As DataRow = shDt.Rows(0)
                                                        Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
                                                        sieRet = CreateSiebelActivityAllSurvey(shLnq.SHOP_CODE, shLnq.SHOP_NAME_EN, shDr("region_code"), dr("mobile_no"), fLnq)
                                                        If sieRet.RESULT = False Then
                                                            Dim _errDesc As String = "Error CreateSiebelActivity ID= " & lnq.ID & " MobileNO=" & dr("mobile_no") & " " & sieRet.ErrorMessage
                                                            FunctionEng.SaveErrorLog("CSISurverENG.SendATSR", _errDesc, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                                        Else
                                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                            iLnq = New Cen.TbFilterDataCenLinqDB
                                                            iLnq.GetDataByPK(dr("id"), trans.Trans)
                                                            iLnq.ACTIVITY_ID_SURVEY = sieRet.ACTIVITY_ID

                                                            If iLnq.UpdateByPK("CSISurverENG.SendATSR", trans.Trans) = True Then
                                                                trans.CommitTransaction()
                                                            Else
                                                                trans.RollbackTransaction()
                                                                Dim _errDesc As String = "Error Update ACTIVITY_ID_SURVEY ID= " & lnq.ID & " MobileNO=" & iLnq.MOBILE_NO & " " & iLnq.ErrorMessage
                                                                FunctionEng.SaveErrorLog("CSISurverENG.SendATSR", _errDesc, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                                            End If
                                                        End If
                                                        sieRet = Nothing
                                                    End If
                                                    shDt.Dispose()
                                                    'End If
                                                End If
                                                fLnq = Nothing
                                            End If
                                            fDt.DefaultView.RowFilter = ""
                                        Else
                                            trans.RollbackTransaction()
                                            Dim _errDesc As String = "ID= " & lnq.ID & " MobileNO=" & iLnq.MOBILE_NO & " " & iLnq.ErrorMessage
                                            FunctionEng.SaveErrorLog("CSISurverENG.SendATSR", _errDesc, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                        End If
                                        iLnq = Nothing
                                    End If
                                    eng = Nothing
                                    shLnq = Nothing
                                Else
                                    FunctionEng.SaveErrorLog("CSISurveyENG.SendATSR", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                End If
                            End If
                        Catch ex As Exception
                            FunctionEng.SaveErrorLog("CSISurveyENG.SendATSR", "Exception 1 " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                        End Try
                    Next
                    fDt.Dispose()
                    dt.Dispose()
                End If
            Else
                FunctionEng.SaveErrorLog("CSISurverENG.SendATSR", "Error Trans :" & trans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSISurveyENG.SendATSR", "Exception 1 " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
        End Try
    End Sub

    Private Function CreateSiebelActivityResult3(ByVal ShopCode As String, ByVal ShopNameEn As String, ByVal MobileNo As String, ByVal RegionCode As String, ByVal fLnq As CenLinqDB.TABLE.TbFilterCenLinqDB) As CenParaDB.CSI.SiebelResponsePara
        Dim data As New CenParaDB.CSI.SiebelReqPara
        data.SIEBEL_TYPE = "Leave Voice Survey CSI"
        If RegionCode.ToUpper = "BKK" Then
            '    data.ACTIVITYCATEGORY = "ร้องเรียน-ชมพนักงาน / สำนักงาน AIS Outlet (กรุงเทพ)"
            data.ACTIVITYCATEGORY = fLnq.CATEGORY_GEN_ACT_RESULT3
            data.ACTIVITYSUBCATEGORY = fLnq.SUBCATEGORY_GEN_ACT_RESULT3
        Else
            '    data.ACTIVITYCATEGORY = "ร้องเรียน-ชมพนักงาน / สำนักงาน AIS Branch (ต่างจังหวัด)"
            data.ACTIVITYCATEGORY = fLnq.CAT_GEN_ACT_RESULT3_UPC
            data.ACTIVITYSUBCATEGORY = fLnq.SUBCAT_GEN_ACT_RESULT3_UPC
        End If
        'data.ACTIVITYSUBCATEGORY = "ข้อควรปรับปรุงจากการสำรวจ CSI"

        data.SIEBEL_OWNERNAME = fLnq.OWNER_GEN_ACT_RESULT3 '"QAI"
        data.DESCRIPTION = "1. ข้อความที่ลูกค้าฝากไว้ " & vbNewLine & vbNewLine & _
        "2.Location / สาขา ที่ร้องเรียน    " & ShopCode & "/" & ShopNameEn & vbNewLine & _
        "3.กรณี Verify เรียบร้อยแล้ว ระบุข้อมูลใน Resolution กรณีเรื่องที่ให้ปรับปรุงไม่ตรงกับ Sub-cat ให้ดำเนินการระบุใน Reason ด้วย" & vbNewLine & _
        "4.QAI Team/ลูกค้า Leave Voice จากการสำรวจ CSI"
        data.STATUS = "Open"
        data.MOBILE_NO = MobileNo

        Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
        Dim gw As New Engine.GateWay.GateWayServiceENG
        sieRet = gw.CreateSiebelActivity(data)
        gw = Nothing
        data = Nothing

        Return sieRet
    End Function

    Private Function CreateSiebelActivityAllSurvey(ByVal ShopCode As String, ByVal ShopNameEn As String, ByVal RegionCode As String, ByVal MobileNo As String, ByVal fLnq As CenLinqDB.TABLE.TbFilterCenLinqDB) As CenParaDB.CSI.SiebelResponsePara
        Dim data As New CenParaDB.CSI.SiebelReqPara
        data.SIEBEL_TYPE = "Survey CSI"

        'data.ACTIVITYCATEGORY = "Survey"
        'data.ACTIVITYSUBCATEGORY = "CSI Survey AIS Retail Shop"
        If RegionCode.ToUpper = "BKK" Then
            data.ACTIVITYCATEGORY = fLnq.CATEGORY_GEN_ACT_ALL_SURVEY
            data.ACTIVITYSUBCATEGORY = fLnq.SUBCATEGORY_GEN_ACT_ALL_SURVEY
        Else
            data.ACTIVITYCATEGORY = fLnq.CAT_GEN_ACT_ALL_SURVEY_UPC
            data.ACTIVITYSUBCATEGORY = fLnq.SUBCAT_GEN_ACT_ALL_SURVEY_UPC
        End If
        data.SIEBEL_OWNERNAME = fLnq.OWNER_GEN_ACT_ALL_SURVEY
        data.DESCRIPTION = ShopCode & "/" & ShopNameEn
        data.STATUS = "Done"
        data.MOBILE_NO = MobileNo

        Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
        Dim gw As New Engine.GateWay.GateWayServiceENG
        sieRet = gw.CreateSiebelActivity(data)
        gw = Nothing
        data = Nothing

        Return sieRet
    End Function

    Private Function GetATSRCallTime(ByVal DateIN As String) As DateTime
        Dim ret As New DateTime(1, 1, 1, 1, 1, 1)
        '07/01/2013 14:16:09:000

        If DateIN.Trim <> "" Then
            If DateIN.Substring(10, 1) = " " Then
                Dim vDT() As String = Split(DateIN, " ")
                Dim vDate() As String = Split(vDT(0), "/")
                Dim vTime() As String = Split(vDT(1), ":")

                If vDate.Length = 3 And vTime.Length = 4 Then
                    Dim yy As Integer = vDate(2)
                    Dim mm As Integer = vDate(1)
                    Dim dd As Integer = vDate(0)
                    Dim hh As Integer = vTime(0)
                    Dim mi As Integer = vTime(1)
                    Dim ss As Integer = vTime(2)
                    ret = New DateTime(yy, mm, dd, hh, mi, ss)

                    'System.IO.Directory.GetFiles("", "ABC*.txt")
                End If
            End If
        End If

        Return ret
    End Function

    Private Function GetShopDetail(ByVal ShopID As Long, ByVal trans As SqlClient.SqlTransaction) As DataTable
        Dim ret As New DataTable
        Dim sql As String = "select sh.shop_code, sh.shop_name_en, r.region_code "
        sql += " from tb_shop sh "
        sql += " inner join tb_region r on r.id=sh.region_id"
        sql += " where sh.id='" & ShopID & "'"

        ret = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans)
        Return ret
    End Function

    Public Sub GetATSRResult()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New Cen.TbFilterDataCenLinqDB
                Dim dt As DataTable = lnq.GetDataList("result_time is null and result_status='1' and convert(varchar(8),send_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "", trans.Trans)
                trans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    Dim fDt As New DataTable
                    fDt = dt.DefaultView.ToTable(True, "tb_filter_id")
                    If fDt.Rows.Count > 0 Then
                        fDt.Columns.Add("fLnq", GetType(CenLinqDB.TABLE.TbFilterCenLinqDB))
                        For i As Integer = 0 To fDt.Rows.Count - 1
                            Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                            fLnq.GetDataByPK(fDt.Rows(i)("tb_filter_id"), Nothing)
                            fDt.Rows(i)("fLnq") = fLnq
                            fLnq = Nothing
                        Next
                    End If

                    For Each dr As DataRow In dt.Rows
                        lnq = New Cen.TbFilterDataCenLinqDB
                        Dim eng As New Engine.GateWay.GateWayServiceENG
                        Dim res As New CenParaDB.GateWay.ATSRResultPara
                        res = eng.GetATSRSurveyResult(dr("queue_unique_id"))
                        If res.RESULT_VALUE.Trim <> "" Then
                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                            lnq.GetDataByPK(dr("id"), trans.Trans)
                            lnq.RESULT_TIME = DateTime.Now
                            lnq.RESULT_STATUS = "2"
                            lnq.RESULT = res.RESULT_VALUE
                            lnq.ATSR_CALL_TIME = GetATSRCallTime(res.ATSR_CALL_TIME)

                            If res.NPS_SCORE <> "" Then
                                lnq.NPS_SCORE = res.NPS_SCORE
                            Else
                                lnq.NPS_SCORE = -1
                            End If

                            If lnq.UpdateByPK("CSISurveyENG.GetATSRResult", trans.Trans) = True Then
                                trans.CommitTransaction()

                                If res.RESULT_VALUE.Trim = "ควรปรับปรุง" And res.HAVE_LEAVE_VOICE = True Then

                                    fDt.DefaultView.RowFilter = "tb_filter_id='" & lnq.TB_FILTER_ID & "'"
                                    If fDt.DefaultView.Count > 0 Then
                                        Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                                        fLnq = DirectCast(fDt.DefaultView(0)("fLnq"), CenLinqDB.TABLE.TbFilterCenLinqDB)

                                        If fLnq.GEN_ACT_RESULT3 = "Y" Then
                                            'If FunctionEng.GetQisDBConfig("CSI_GEN_ACT_RESULT3") = "Y" Then
                                            Dim shDt As New DataTable
                                            shDt = GetShopDetail(lnq.SHOP_ID, Nothing)
                                            If shDt.Rows.Count > 0 Then
                                                Dim shDr As DataRow = shDt.Rows(0)
                                                Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
                                                sieRet = CreateSiebelActivityResult3(shDr("shop_code"), shDr("shop_name_en"), lnq.MOBILE_NO, shDr("region_code"), fLnq)
                                                If sieRet.RESULT = False Then
                                                    FunctionEng.SaveErrorLog("CSISurveyENG.GetATSRResult", "Error CreateSiebelActivityResult3  QuqID=" & dr("queue_unique_id") & " $$$ " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                                Else
                                                    lnq = New Cen.TbFilterDataCenLinqDB
                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    lnq.GetDataByPK(dr("id"), trans.Trans)
                                                    lnq.ACTIVITY_ID_LEAVE_VOICE = sieRet.ACTIVITY_ID
                                                    If lnq.UpdateByPK("CSISurveyENG.GetATSRResult", trans.Trans) = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        trans.RollbackTransaction()
                                                        FunctionEng.SaveErrorLog("CSISurveyENG.GetATSRResult", "Error UPDATE ACTIVITY_ID_LEAVE_VOICE  QuqID=" & dr("queue_unique_id"), Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                                    End If
                                                End If
                                                sieRet = Nothing
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("CSISurveyENG.GetATSRResult", "Error GetShopDetail  QuqID=" & dr("queue_unique_id"), Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                            End If
                                            shDt.Dispose()
                                            'End If
                                        End If
                                        fLnq = Nothing
                                    End If
                                End If
                            Else
                                trans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("CSISurveyENG.GetATSRResult", "QuqID=" & dr("queue_unique_id") & " $$$ " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                            End If
                        Else
                            Select Case res.RESULT_STATE
                                Case "UNKNOWN", "UNSUCCESS"
                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                    lnq.GetDataByPK(dr("id"), trans.Trans)
                                    lnq.RESULT_TIME = DateTime.Now
                                    lnq.RESULT_STATUS = "3"
                                    lnq.RESULT = res.RESULT_STATE
                                    lnq.ATSR_CALL_TIME = GetATSRCallTime(res.ATSR_CALL_TIME)

                                    If lnq.UpdateByPK("CSISurveyENG.GetATSRResult", trans.Trans) = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        'PrintLog("CSISurveyENG.GetATSRResult : QuqID=" & dr("queue_unique_id") & " $$$ " & lnq.ErrorMessage)
                                        FunctionEng.SaveErrorLog("CSISurveyENG.GetATSRResult", "QuqID=" & dr("queue_unique_id") & " $$$ " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                                    End If
                            End Select
                        End If
                        eng = Nothing
                        res = Nothing
                        lnq = Nothing
                    Next
                    fDt.Dispose()
                    dt.Dispose()
                End If
            Else
                FunctionEng.SaveErrorLog("CSISurverENG.GetATSRResult", trans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSISurveyENG.GetATSRResult", "Exception : " & ex.Message, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
        End Try
    End Sub

    Public Sub SendResultToShop()
        Try
            Dim sDt As DataTable = FunctionEng.GetActiveShop()
            If sDt.Rows.Count > 0 Then
                For Each sDr As DataRow In sDt.Rows
                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    Dim fLnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB

                    Dim dt As DataTable = fLnq.GetDataList("send_to_shop_time is null and result_status = '2' and shop_id = '" & sDr("id") & "' and convert(varchar(8),result_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "result_time", trans.Trans)
                    trans.CommitTransaction()
                    fLnq = Nothing

                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            trans = New CenLinqDB.Common.Utilities.TransactionDB

                            Dim lnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
                            lnq.GetDataByPK(dr("id"), trans.Trans)

                            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, FunctionEng.GetTbShopCenLinqDB(sDr("id")), "CSISurveyENG.SendResultToShop")
                            If shTrans.Trans IsNot Nothing Then
                                Dim CsiLnq As New ShLinqDB.TABLE.TbCsiShLinqDB
                                CsiLnq.SERVICE_DATE = lnq.SERVICE_DATE
                                CsiLnq.ITEM_ID = lnq.TB_ITEM_ID
                                If lnq.RESULT = "ดีมาก" Then
                                    CsiLnq.RESULT_VALUE = 3
                                ElseIf lnq.RESULT = "ดี" Then
                                    CsiLnq.RESULT_VALUE = 2
                                ElseIf lnq.RESULT = "ควรปรับปรุง" Then
                                    CsiLnq.RESULT_VALUE = 1
                                ElseIf lnq.RESULT = "ตามคาดหวัง" Then
                                    CsiLnq.RESULT_VALUE = 1
                                ElseIf lnq.RESULT = "มากกว่าคาดหวัง" Then
                                    CsiLnq.RESULT_VALUE = 2
                                Else
                                    CsiLnq.RESULT_VALUE = 0
                                End If

                                Dim ret As Boolean = CsiLnq.InsertData("CSISurveyENG.SendResultToShop", shTrans.Trans)
                                If ret = True Then
                                    shTrans.CommitTransaction()

                                    lnq.SEND_TO_SHOP_TIME = DateTime.Now
                                    If lnq.UpdateByPK("CSISurveyENG.SendResultToShop", trans.Trans) = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                    End If
                                Else
                                    shTrans.RollbackTransaction()
                                End If
                                CsiLnq = Nothing
                            End If
                            trans.CommitTransaction()
                            lnq = Nothing
                        Next
                        dt.Dispose()
                    End If
                Next
                sDt.Dispose()
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSISurveyENG.SendResultToShop", "Exception : " & ex.Message, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
        End Try
    End Sub
#End Region

#Region "Send Email"
    Public Sub CSISendEmail(ByVal ServiceDate As DateTime, ByVal ShopID As Long)
        Dim shLinq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        Dim MailMsg As String = FilterDataForSendEmail(ServiceDate, shLinq)
        If MailMsg.Trim <> "" Then
            Dim gw As New GateWay.GateWayServiceENG
            gw.SendEmail("nattapol@scoresolutions.co.th;akkarawat@scoresolutions.co.th", "CSI Data at shop " & shLinq.SHOP_NAME_TH & " " & ServiceDate.ToString("dd/MM/yyyy"), MailMsg)
            shLinq = Nothing
        End If
        shLinq = Nothing
    End Sub
    Private Function FilterDataForSendEmail(ByVal ServiceDate As DateTime, ByVal shLinq As CenLinqDB.TABLE.TbShopCenLinqDB) As String
        Dim MailMsg As New System.Text.StringBuilder

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
        dt = fLnq.GetDataList("active_status='Y'", "", trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)

                trans = New CenLinqDB.Common.Utilities.TransactionDB
                Dim shLnq As New Cen.TbFilterShopCenLinqDB
                Dim shDt As DataTable = shLnq.GetDataList("tb_filter_id = " & dr("id") & " and tb_shop_id = '" & shLinq.ID & "'", "", trans.Trans)
                trans.CommitTransaction()
                If shDt.Rows.Count > 0 Then
                    For Each shDr As DataRow In shDt.Rows
                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                        If trans.Trans IsNot Nothing Then
                            fLnq.GetDataByPK(dr("id"), trans.Trans)
                            If fLnq.HaveData = True Then
                                Dim ret As Boolean = False
                                If fLnq.SCHEDULETYPEDAY = "0" Then
                                    'สำรวจทุกวัน
                                    ret = True
                                ElseIf fLnq.SCHEDULETYPEDAY = "1" Then
                                    'สำรวจตามวัน
                                    Dim CaseDay As Integer = DatePart(DateInterval.Weekday, DateTime.Now)
                                    Select Case CaseDay
                                        Case 1
                                            ret = (fLnq.SCHEDULESUNDAY = "Y")
                                        Case 2
                                            ret = (fLnq.SCHEDULEMONDAY = "Y")
                                        Case 3
                                            ret = (fLnq.SCHEDULETUEDAY = "Y")
                                        Case 4
                                            ret = (fLnq.SCHEDULEWEDDAY = "Y")
                                        Case 5
                                            ret = (fLnq.SCHEDULETHUDAY = "Y")
                                        Case 6
                                            ret = (fLnq.SCHEDULEFRIDAY = "Y")
                                        Case 7
                                            ret = (fLnq.SCHEDULESATDAY = "Y")
                                    End Select
                                End If

                                If ret = True Then
                                    Dim qDt As DataTable = GetQDataByShop(fLnq, shLinq, "1=1", fLnq.CHILD_TB_FILTER_SHOP_tb_filter_id, "D")
                                    If qDt.Rows.Count > 0 Then

                                        'ใช้ ชั่วคราวก่อน 2013-01-12
                                        Dim bSql As String = "select b.mobile_no"
                                        bSql += " from TB_FILTER_BACKLIST_FILE f "
                                        bSql += " inner join TB_FILTER_BACKLIST_MOBILE b on b.tb_filter_backlist_file_id=f.id"
                                        bSql += " where f.tb_filter_id='" & fLnq.ID & "'"
                                        Dim bDt As DataTable = fLnq.GetListBySql(bSql, trans.Trans)
                                        If bDt.Rows.Count > 0 Then
                                            For j As Integer = qDt.Rows.Count - 1 To 0 Step -1
                                                bDt.DefaultView.RowFilter = "mobile_no = '" & qDt.Rows(j)("customer_id").ToString & "'"
                                                If bDt.DefaultView.Count > 0 Then
                                                    qDt.Rows.RemoveAt(j)
                                                End If
                                                bDt.DefaultView.RowFilter = ""
                                            Next
                                        End If
                                        bDt = Nothing




                                        If qDt.Rows.Count > 0 Then
                                            MailMsg.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'> ")
                                            MailMsg.Append("    <tr>")
                                            MailMsg.Append("        <td>Template Name</td>")
                                            MailMsg.Append("        <td>Mobile No</td>")
                                            MailMsg.Append("        <td>Customer Name</td>")
                                            MailMsg.Append("        <td>End Service Time</td>")
                                            MailMsg.Append("        <td>Username</td>")
                                            MailMsg.Append("        <td>Service Name</td>")
                                            MailMsg.Append("    </tr>")
                                            Dim tmpMsg As String = ""
                                            For Each qDr As DataRow In qDt.Rows
                                                MailMsg.Append("    <tr>")
                                                MailMsg.Append("        <td>" & fLnq.FILTER_NAME & "</td>")
                                                MailMsg.Append("        <td>" & qDr("customer_id") & "</td>")
                                                If Convert.IsDBNull(qDr("customer_name")) = False Then
                                                    MailMsg.Append("        <td>" & qDr("customer_name") & "</td>")
                                                Else
                                                    MailMsg.Append("        <td>&nbsp;</td>")
                                                End If
                                                MailMsg.Append("        <td>" & Convert.ToDateTime(qDr("end_time")).ToString("dd/MM/yyyy HH:mm:ss", New Globalization.CultureInfo("th-TH")) & "</td>")
                                                MailMsg.Append("        <td>" & qDr("username") & "</td>")
                                                MailMsg.Append("        <td>" & qDr("item_name") & "</td>")
                                                MailMsg.Append("    </tr>")
                                            Next
                                            MailMsg.Append("</table>")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Next
            dt = Nothing
        End If
        fLnq = Nothing

        Return MailMsg.ToString
    End Function
#End Region

#Region "Calulate Filter Target"
    Public Sub CalTarget()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim fLnq As New Cen.TbFilterCenLinqDB
                Dim fDt As DataTable = fLnq.GetDataList("cal_target = 'N' and active_status='Y'", "", trans.Trans)
                trans.CommitTransaction()
                If fDt.Rows.Count > 0 Then
                    For Each fDr As DataRow In fDt.Rows
                        If SaveFilterTempTarget(fDr("id"), "AisCSIAgent") = False Then
                            FunctionEng.SaveErrorLog("CSISurveyENG.CalTarget", _err, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
                        End If
                    Next
                    fDt.Dispose()
                End If
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSISurveyENG.CalTarget", "Exception : " & ex.Message, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
        End Try
    End Sub

    Public Function SaveFilterTempTarget(ByVal FilterID As Long, ByVal LoginName As String) As Boolean
        Dim ret As Boolean = False
        Try
            'สูตรคำนวณ Target ต่อ Shop ต่อ Service = (Target/(จำนวนวัน * จำนวนชั่วโมง))* % ของแต่ละ Service

            Dim fLnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
            fLnq.GetDataByPK(FilterID, Nothing)

            Dim DayQty As Integer = 0
            Dim HourQty As Integer = CInt(Split(fLnq.PREIOD_TIMETO, ":")(0)) - CInt(Split(fLnq.PREIOD_TIMEFROM, ":")(0))

            If fLnq.SCHEDULETYPEDAY = "0" Then
                DayQty = DateDiff(DateInterval.Day, fLnq.PREIOD_DATEFROM.Value, fLnq.PREIOD_DATETO.Value) + 1
            ElseIf fLnq.SCHEDULETYPEDAY = "1" Then
                Dim CurrDate As DateTime = fLnq.PREIOD_DATEFROM.Value
                Do
                    If fLnq.SCHEDULESUNDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Sunday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEMONDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Monday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULETUEDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Tuesday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEWEDDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Wednesday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULETHUDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Thursday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULEFRIDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Friday Then
                        DayQty += 1
                    End If
                    If fLnq.SCHEDULESATDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Saturday Then
                        DayQty += 1
                    End If

                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                Loop While CurrDate <= fLnq.PREIOD_DATETO.Value
            End If

            Dim shDt As New DataTable
            If fLnq.SURVEY_ALL_SHOP = "Y" Then
                Dim ShSql As String = "select '" & fLnq.ID & "' tb_filter_id, id tb_shop_id "
                ShSql += " from tb_shop "
                ShSql += " where active='Y'"
                shDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(ShSql)
            Else
                shDt = fLnq.CHILD_TB_FILTER_SHOP_tb_filter_id
            End If

            'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If shDt.Rows.Count > 0 Then
                Dim svLnq As New CenLinqDB.TABLE.TbFilterServiceCenLinqDB
                Dim svDt As DataTable = svLnq.GetDataList("tb_filter_id='" & fLnq.ID & "' and target_percent > 0", "", Nothing)
                If svDt.Rows.Count > 0 Then
                    For Each shDr As DataRow In shDt.Rows
                        'ทำ Transaction ภายใต้ Shop
                        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                        For Each svDr As DataRow In svDt.Rows
                            Dim tmp As Double = ((fLnq.TARGET * Convert.ToDouble(svDr("target_percent"))) / 100) / (DayQty * HourQty)
                            Dim Target As Integer = Math.Ceiling(tmp)

                            'Update Target Per Hour สำหรับแต่ละ Service
                            Dim sLnq As New CenLinqDB.TABLE.TbFilterServiceCenLinqDB
                            sLnq.GetDataByPK(svDr("id"), trans.Trans)
                            sLnq.TARGET_PERHOUR = Target

                            If sLnq.UpdateByPK(LoginName, trans.Trans) = True Then
                                'Loop ตามวันที่
                                Dim CurrDate As DateTime = fLnq.PREIOD_DATEFROM.Value
                                Do
                                    If GetSelectedDateOfWeek(fLnq, CurrDate) = True Then
                                        For i As Integer = CInt(Split(fLnq.PREIOD_TIMEFROM, ":")(0)) To CInt(Split(fLnq.PREIOD_TIMETO, ":")(0)) - 1
                                            Dim lnq As New CenLinqDB.TABLE.TbFilterTempTargetCenLinqDB
                                            lnq.TB_FILTER_ID = fLnq.ID
                                            lnq.TB_SHOP_ID = Convert.ToInt64(shDr("tb_shop_id"))
                                            lnq.SERVICE_DATE = CurrDate
                                            lnq.END_TIME_FROM = i.ToString.PadLeft(2, "0") & ":00"
                                            lnq.END_TIME_TO = (i + 1).ToString.PadLeft(2, "0") & ":00"
                                            lnq.TB_ITEM_ID = Convert.ToInt64(svDr("tb_item_id"))
                                            lnq.USERNAME = GetFilterUserName(fLnq, Convert.ToInt64(svDr("tb_item_id")), Convert.ToInt64(shDr("tb_shop_id")), trans)
                                            lnq.TARGET = Target

                                            ret = lnq.InsertData(LoginName, trans.Trans)
                                            If ret = False Then
                                                _err = lnq.ErrorMessage
                                                Exit For
                                            End If
                                        Next
                                        If ret = False Then
                                            Exit Do
                                        End If
                                    End If

                                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                                Loop While CurrDate <= fLnq.PREIOD_DATETO.Value
                            Else
                                ret = False
                                _err = sLnq.ErrorMessage
                                Exit For
                            End If
                            If ret = False Then
                                Exit For
                            End If
                        Next
                        If ret = False Then
                            trans.RollbackTransaction()
                            Exit For
                        Else
                            trans.CommitTransaction()
                        End If
                    Next
                Else
                    _err = "Service Not Found"
                End If

                If ret = True Then
                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    fLnq.CAL_TARGET = "Y"
                    If fLnq.UpdateByPK(LoginName, trans.Trans) = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                        _err = fLnq.ErrorMessage
                    End If
                    'Else
                    '    trans.RollbackTransaction()
                End If
                shDt.Dispose()
            Else
                _err = "Shop Not Found"
            End If
        Catch ex As Exception
            CreateLogFile("SqveFilterTempTarget", ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return ret
    End Function

    Private Function GetSelectedDateOfWeek(ByVal fLnq As CenLinqDB.TABLE.TbFilterCenLinqDB, ByVal CurrDate As DateTime) As Boolean
        Dim ret As Boolean = False
        If fLnq.SCHEDULETYPEDAY = "0" Then
            ret = True
        ElseIf fLnq.SCHEDULETYPEDAY = "1" Then
            If fLnq.SCHEDULESUNDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Sunday Then
                ret = True
            ElseIf fLnq.SCHEDULEMONDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Monday Then
                ret = True
            ElseIf fLnq.SCHEDULETUEDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Tuesday Then
                ret = True
            ElseIf fLnq.SCHEDULEWEDDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Wednesday Then
                ret = True
            ElseIf fLnq.SCHEDULETHUDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Thursday Then
                ret = True
            ElseIf fLnq.SCHEDULEFRIDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Friday Then
                ret = True
            ElseIf fLnq.SCHEDULESATDAY = "Y" And CurrDate.DayOfWeek = DayOfWeek.Saturday Then
                ret = True
            End If
        End If

        Return ret
    End Function

    Private Function GetFilterUserName(ByVal fLnq As CenLinqDB.TABLE.TbFilterCenLinqDB, ByVal ServiceID As Long, ByVal ShopID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As String
        Dim ret As String = ""
        Dim fuDt As New DataTable
        fuDt = fLnq.CHILD_TB_FILTER_STAFF_tb_filter_id
        fuDt.DefaultView.RowFilter = "shop_id = '" & ShopID & "'"
        fuDt = fuDt.DefaultView.ToTable
        If fuDt.Rows.Count > 0 Then
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq, "CSISurveyENG.GetFilterUserName")

            Dim sql As String = ""
            sql += " select distinct  u.username "
            sql += " from tb_user u"
            sql += " left join TB_TITLE t on t.id=u.title_id"
            sql += " inner join TB_USER_SKILL us on u.id=us.user_id"
            sql += " inner join TB_SKILL s on s.id=us.skill_id"
            sql += " inner join TB_SKILL_ITEM si on s.id=si.skill_id"
            sql += " inner join TB_ITEM i on i.id=si.item_id"
            sql += " where u.active_status = '1' and s.active_status='1' "
            sql += " and i.master_itemid = '" & ServiceID & "'"

            Dim uDt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
            shTrans.CommitTransaction()
            If uDt.Rows.Count > 0 Then
                For i As Integer = fuDt.Rows.Count - 1 To 0 Step -1
                    uDt.DefaultView.RowFilter = "username = '" & fuDt.Rows(i)("username") & "'"
                    If uDt.DefaultView.Count > 0 Then
                        If ret.Trim = "" Then
                            ret = "'" & fuDt.Rows(i)("username") & "'"
                        Else
                            ret += ",'" & fuDt.Rows(i)("username") & "'"
                        End If
                    End If
                    uDt.DefaultView.RowFilter = ""
                Next
            End If
            uDt = Nothing
        End If
        fuDt = Nothing

        Return ret
    End Function
#End Region

#Region "Get CSI AIS Filter Data 2G Customer"
    Public Function GetData2GCustomerToday(ByVal vDate As DateTime) As DataTable
        Dim dt As New DataTable
        Try
            Dim LastMonth As DateTime = DateAdd(DateInterval.Month, -1, vDate)
            Dim vDateFrom As String = New Date(LastMonth.Year, LastMonth.Month, 1).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim vDateTo As String = New Date(LastMonth.Year, LastMonth.Month, DateTime.DaysInMonth(LastMonth.Year, LastMonth.Month)).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = "select distinct mobile_no "
            sql += " from tb_filter_data "
            sql += " where network_type='2G'"
            sql += " and convert(varchar(8),service_date,112) between '" & vDateFrom & "' and '" & vDateTo & "'"

            Dim lnq As New Cen.TbFilterDataCenLinqDB
            dt = lnq.GetListBySql(sql, Nothing)
            lnq = Nothing
        Catch ex As Exception
            dt = New DataTable
            FunctionEng.SaveErrorLog("CSISurveyENG.GetData2GCustomerToday", "Exception : " & ex.Message, Application.StartupPath & "\ErrorLog\", "CSISurveyENG")
        End Try

        Return dt
    End Function
#End Region


End Class
