Imports Engine.Common
Imports Cen = CenLinqDB.TABLE
Imports System.Windows.Forms
Imports System.Globalization
Imports System.IO
Imports CenLinqDB.Common.Utilities

Public Class CSITWSurveyENG
    Dim _err As String = ""

    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property



#Region "_ReadTextFile"
    Public Function ReadAndSaveDataFromTextFile() As Integer
        Dim ret As Integer = 0
        Dim INIFlie As String = Application.StartupPath & "\config.ini"
        Dim ini As New IniReader(INIFlie)
        ini.Section = "Setting"
        Dim path As String = ini.ReadString("path")
        If path <> "" Then
            Try
                Dim dateNow As String = DateTime.Now.ToString("ddMMyyyy", New CultureInfo("en-US")) '"15072013" 
                For Each files As String In System.IO.Directory.GetFiles(path, "*" & dateNow & "*.DAT")
                    Dim arrFileName1() = files.Split("\")
                    Dim FileNames() As String = arrFileName1(arrFileName1.Length - 1).Split(".")
                    Dim FileName As String = ""
                    If FileNames.Length > 0 Then
                        FileName = FileNames(0)
                    End If

                    Dim _file() As String = System.IO.Directory.GetFiles(path, FileName & "*.sync")
                    If _file.Length = 1 Then
                        If IsSave(FileName) = False Then
                            Dim re As Boolean = SaveDataFromTextFile(FileName, files)
                            If re = True Then
                                ret += 1
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadAndSaveDataFromTextFile", "Exception : " & ex.Message & vbNewLine & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
            End Try
        End If
        Return ret
    End Function

    Public Function IsSave(ByVal fileName As String) As Boolean
        Dim ret As Boolean = False
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        Dim lnq As New Cen.TwSourceTextfileCenLinqDB
        Dim sql As String = "select 'y' from TW_SOURCE_TEXTFILE " & _
        " where file_name = '" & fileName & "' "
        dt = lnq.GetListBySql(sql, trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            ret = True
        Else
            ret = False
        End If
        dt.Dispose()
        lnq = Nothing

        Return ret

    End Function

    Private Function SaveDataFromTextFile(ByVal fileName As String, ByVal filePath As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

            '=== บันทึก TW_SOURCE_TEXTFILE
            Dim lnq As New Cen.TwSourceTextfileCenLinqDB
            'lnq.ID = 0
            lnq.FILE_NAME = fileName
            lnq.IMPORT_TIME = DateTime.Now 'DateTime.Now.ToString("yyyy-MM-dd", New CultureInfo("en-US"))

            Dim strFileType As String = fileName.Substring(0, 7)
            If strFileType = "TWX_PAY" Then
                lnq.SOURCE_TYPE = "PAYMENT"
            ElseIf strFileType = "TWX_SFF" Then
                lnq.SOURCE_TYPE = "SFF"
            End If
            lnq.RECORD_COUNT = 0

            ret = lnq.InsertData("CSITWSurveyAgent", trans.Trans)
            If ret = False Then
                trans.RollbackTransaction()
                FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadDataFromTextFile", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                Return False
            Else
                trans.CommitTransaction()
            End If
            '=== END บันทึก TW_SOURCE_TEXTFILE

            Dim SourceTxtID As String = lnq.ID
            '=== อ่าน TextFile บันทึก Data
            Dim recordCount As Integer = 0
            Dim Stream As New StreamReader(filePath, System.Text.UnicodeEncoding.Default)
            While Stream.Peek <> -1
                Dim str As String = Stream.ReadLine
                If str.Trim <> "" Then
                    Dim strFld As String() = Split(str, "|")
                    If strFld(0) = "01" Or strFld(0) = "09" Then
                        Continue While
                    End If

                    Try
                        Dim ORDER_TYPE As String = ""
                        Dim COMPLETE_DATE As New DateTime(1, 1, 1)
                        Dim MOBILE_NO As String = ""
                        Dim CUSOMER_NAME As String = ""
                        Dim NATIONALITY As String = ""
                        Dim NETWORK_TYPE As String = ""
                        Dim MOBILE_SEGMENT As String = ""
                        Dim LOCATION_CODE As String = ""
                        Dim LOCATION_NAME As String = ""
                        Dim REGION_CODE As String = ""
                        Dim PROVINCE_CODE As String = ""
                        If strFileType = "TWX_PAY" Then
                            '14/27/2013 16:25:20|0800739888|วิษณุ อโนทัยสินทวี|2G|THAILAND|Gold|1101|สาขา Flag Ship
                            ORDER_TYPE = "PAYMENT"
                            COMPLETE_DATE = New DateTime(strFld(1).Substring(6, 4), strFld(1).Substring(3, 2), _
                                                     strFld(1).Substring(0, 2), CInt(strFld(1).Substring(11, 2)), _
                                                     CInt(strFld(1).Substring(14, 2)), CInt(strFld(1).Substring(17, 2)))
                            MOBILE_NO = strFld(2)
                            CUSOMER_NAME = strFld(3)
                            NETWORK_TYPE = strFld(4)
                            NATIONALITY = strFld(5)
                            MOBILE_SEGMENT = strFld(6)
                            LOCATION_CODE = strFld(7)
                            LOCATION_NAME = strFld(8)

                        ElseIf strFileType = "TWX_SFF" Then
                            'Change Promotion|14/27/2013 16:25:20|0800739888|วิษณุ อโนทัยสินทวี|2G|THAILAND|Gold|1101|สาขา Flag Ship
                            ORDER_TYPE = strFld(1)
                            COMPLETE_DATE = New DateTime(strFld(2).Substring(6, 4), strFld(2).Substring(3, 2), _
                                                      strFld(2).Substring(0, 2), CInt(strFld(2).Substring(11, 2)), _
                                                      CInt(strFld(2).Substring(14, 2)), CInt(strFld(2).Substring(17, 2)))
                            MOBILE_NO = strFld(3)
                            CUSOMER_NAME = strFld(4)
                            NETWORK_TYPE = strFld(5)
                            NATIONALITY = strFld(6)
                            MOBILE_SEGMENT = strFld(7)
                            LOCATION_CODE = strFld(8)
                            LOCATION_NAME = strFld(9)
                        End If

                        If LOCATION_CODE <> "" Then
                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                            Dim lLnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                            Dim lDt As New DataTable
                            lDt = lLnq.GetDataList("location_code='" & LOCATION_CODE & "'", "", trans.Trans)
                            If lDt.Rows.Count > 0 Then
                                REGION_CODE = lDt.Rows(0).Item("region_code") & ""
                                PROVINCE_CODE = lDt.Rows(0).Item("province_code") & ""

                                Dim lnqData As New Cen.TwSourceDataCenLinqDB
                                With lnqData
                                    .TW_SOURCE_TEXTFILE_ID = SourceTxtID
                                    .ORDER_TYPE = ORDER_TYPE
                                    .COMPLETE_DATE = COMPLETE_DATE
                                    .MOBILE_NO = MOBILE_NO
                                    .CUSTOMER_NAME = CUSOMER_NAME
                                    .NATIONALITY = NATIONALITY
                                    .NETWORK_TYPE = NETWORK_TYPE
                                    .MOBILE_SEGMENT = MOBILE_SEGMENT
                                    .LOCATION_CODE = LOCATION_CODE
                                    .LOCATION_NAME = LOCATION_NAME
                                    .REGION_CODE = REGION_CODE
                                    .PROVINCE_CODE = PROVINCE_CODE
                                End With

                                ret = lnqData.InsertData("CSITWSurveyAgent", trans.Trans)
                                If ret = False Then
                                    trans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadDataFromTextFile", lnqData.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                Else
                                    trans.CommitTransaction()
                                    recordCount += 1
                                End If
                            Else
                                trans.RollbackTransaction()
                            End If
                            lLnq = Nothing
                            lDt.Dispose()
                        End If
                    Catch ex As IndexOutOfRangeException
                        FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadDataFromTextFile", "1. IndexOutOfRangeException FileName : " & fileName & ex.Message, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                    End Try
                End If
            End While

            '=== อัพเดท Record Count
            If recordCount > 0 Then
                trans = New CenLinqDB.Common.Utilities.TransactionDB
                If UpdateRecordNumber(SourceTxtID, recordCount, trans) = True Then
                    trans.CommitTransaction()
                    ret = True
                Else
                    trans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadDataFromTextFile", "Update record_count not complete.", Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                End If
            Else
                'trans.RollbackTransaction()
                FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadDataFromTextFile", "No data", Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
            End If
            Stream.Close()
            Stream = Nothing
        Catch ex As Exception
            ret = False
            FunctionEng.SaveErrorLog("frmAisCSITWAgent.ReadDataFromTextFile", "2. Exception FileName : " & fileName & ex.Message, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
        End Try

        Return ret
    End Function

    Public Function UpdateRecordNumber(ByVal ID As String, ByVal CountRec As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean
        Try
            Dim lnq As New Cen.TwSourceTextfileCenLinqDB
            ret = lnq.UpdateBySql("UPDATE TW_SOURCE_TEXTFILE SET RECORD_COUNT=" & CountRec & " Where ID='" & ID & "'", trans.Trans)

        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

#End Region '_ReadTextFile



#Region "_FilterData"
    Public Sub FilterData()
        Try
            SetFilterActiveStatus()

            Dim fLnq As New Cen.TwFilterCenLinqDB
            Dim fSql As String = "process_status='Y' and active_status = 'Y' and cal_target='Y'"
            Dim fDt As DataTable = fLnq.GetDataList(fSql, "", Nothing)
            If fDt.Rows.Count > 0 Then
                For Each fDr As DataRow In fDt.Rows
                    Dim FilterLnq As New Cen.TwFilterCenLinqDB
                    FilterLnq.GetDataByPK(fDr("id"), Nothing)
                    If FilterLnq.ID <> 0 Then
                        'หา Location ของ Template ที่ทำงาน
                        Dim lSql As String = "select l.location_code "
                        lSql += " from tw_location l "
                        lSql += " inner join TW_FILTER_BRANCH fb on l.id=fb.tw_location_id"
                        lSql += " where fb.tw_filter_id = '" & fDr("id") & "'"

                        Dim FilterPayment As Boolean = False
                        Dim WhText As String = ""
                        Dim Query As String = ""
                        If FilterLnq.ORDER_PAYMENT_PER > 0 Then
                            'payment คิวรี่ย้อนหลัง 1 วัน
                            WhText = " convert(varchar(8),sd.complete_date,112)=convert(varchar(8), DATEADD(day,-1,getdate()),112)"
                            WhText += " and sd.location_code in (" & lSql & ") "
                            WhText += " and sd.id not in (select tw_source_data_id from tw_filter_data)"
                            Query += GetQueryDataAllLocation(WhText, "PAYMENT")
                            FilterPayment = True
                        End If

                        If FilterLnq.ORDER_PAYMENT_PER < 100 Then
                            If FilterPayment = True Then
                                Query += " UNION ALL" & vbNewLine
                            End If


                            'sff คืวรี่วันนี้ทั้งหมด
                            WhText = " convert(varchar(8),sd.complete_date,112)=convert(varchar(8), getdate(),112)"
                            WhText += " and sd.location_code in (" & lSql & ") "
                            WhText += " and sd.id not in (select tw_source_data_id from tw_filter_data)"

                            Query += GetQueryDataAllLocation(WhText, "SFF")
                        End If

                        Dim dDt As DataTable = SqlDB.ExecuteTable(Query)
                        If dDt.Rows.Count > 0 Then
                            'หา Shop ที่จะต้องทำการสำรวจโดย Filter นี้
                            Dim shLnq As New Cen.TwFilterBranchCenLinqDB
                            Dim shDt As DataTable = shLnq.GetDataList("tw_filter_id = " & fDr("id"), "", Nothing)
                            If shDt.Rows.Count > 0 Then
                                '###########Filter By Template##############
                                WhText = " 1=1 "
                                If FilterLnq.NATIONALITY.Trim <> "" Then
                                    'Nationality
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
                                            vNation += " and isnull(nationality,'') not in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
                                        Else
                                            If IsCheckThai = False Then
                                                vNation += " and isnull(nationality,'') not in ('THAILAND','THAI')" & vbNewLine
                                            End If
                                            If IsCheckEng = False Then
                                                vNation += " and isnull(nationality,'') not in ('ENGLAND','ENG','United Kingdom')" & vbNewLine
                                            End If
                                        End If
                                    Else
                                        If IsCheckThai = True And IsCheckEng = True Then
                                            vNation += " and isnull(nationality,'') in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
                                        Else
                                            If IsCheckThai = True And IsCheckEng = False Then
                                                vNation += " and isnull(nationality,'') in ('THAILAND','THAI')" & vbNewLine
                                            ElseIf IsCheckThai = False And IsCheckEng = True Then
                                                vNation += " and isnull(nationality,'') in ('ENGLAND','ENG','United Kingdom')" & vbNewLine
                                            ElseIf IsCheckThai = False And IsCheckEng = False Then
                                                vNation += " and isnull(nationality,'') not in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
                                            End If
                                        End If
                                    End If

                                    If IsCheckBlank = False Then
                                        vNation += " and isnull(nationality,'')<>''" & vbNewLine
                                    Else
                                        'ถ้า Tick Checkbox Nationality ทั้ง 4 ค่าเลย ก็ไม่ต้อง ตรวจสอบ Nationality
                                        If IsCheckThai = True And IsCheckEng = True And IsCheckOther = True Then

                                        Else
                                            If IsCheckThai = False And IsCheckEng = False And IsCheckOther = False Then
                                                'ถ้า Tick Blank อย่างเดียว
                                                vNation = " and isnull(nationality,'')=''"
                                            Else
                                                vNation = " and (" & vNation.Substring(4) & " or isnull(nationality,'')='')" & vbNewLine
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
                                    WhText += " and mobile_segment in (" & tmp & ") " & vbNewLine
                                End If

                                'หา Service ของ Filter
                                Dim oLnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
                                Dim sDt As New DataTable
                                sDt = oLnq.GetDataList("tw_filter_id='" & FilterLnq.ID & "'", "", Nothing)
                                If FilterLnq.CHK_ORDER_SFF = "Y" Then
                                    sDt = CalPercentTarget(FilterLnq)
                                End If

                                If sDt.Rows.Count > 0 Then
                                    sDt.DefaultView.RowFilter = "target_percent > 0"
                                    If sDt.DefaultView.Count > 0 Then
                                        Dim OrderTypeName As String = ""
                                        For Each sDr As DataRowView In sDt.DefaultView
                                            Dim sTmp As New Cen.TwSffOrderTypeCenLinqDB
                                            sTmp.GetDataByPK(sDr("tw_sff_order_type_id"), Nothing)
                                            If OrderTypeName = "" Then
                                                OrderTypeName = "'" & sTmp.ORDER_TYPE_NAME & "'"
                                            Else
                                                OrderTypeName += "," & "'" & sTmp.ORDER_TYPE_NAME & "'"
                                            End If
                                            sTmp = Nothing
                                        Next
                                        WhText += " and order_type in (" & OrderTypeName & ")" & vbNewLine
                                    End If
                                    sDt.Dispose()
                                End If
                                oLnq = Nothing

                                If FilterLnq.NETWORK_TYPE.ToUpper <> "ALL" Then
                                    WhText += " and network_type = '" & FilterLnq.NETWORK_TYPE & "'" & vbNewLine
                                End If
                                '###########Filter By Template##############
                                dDt.DefaultView.RowFilter = WhText
                                If dDt.DefaultView.Count > 0 Then
                                    For Each shDr As DataRow In shDt.Rows
                                        Dim ShopLnq As Cen.TwLocationCenLinqDB = FunctionEng.GetTwLocationCenLinqDB(shDr("tw_location_id"))
                                        If ShopLnq.ACTIVE = "Y" Then
                                            If IsCompletedTarget(FilterLnq, ShopLnq.ID) Then
                                                SaveFilterData(FilterLnq, ShopLnq, dDt.DefaultView.ToTable.Copy, "PAYMENT")
                                                SaveFilterData(FilterLnq, ShopLnq, dDt.DefaultView.ToTable.Copy, "SFF")
                                            End If
                                        End If
                                        ShopLnq = Nothing
                                    Next
                                End If
                                dDt.DefaultView.RowFilter = ""
                                shDt.Dispose()
                            End If
                        Else
                            FunctionEng.SaveErrorLog("CSITWSurveyENG.FilterData : ", "Query Expire " & fDr("filter_name"), Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                        End If
                        dDt.Dispose()
                    End If
                Next
            End If
            fDt.Dispose()
        Catch ex As Exception
            CreateLogFile("CSITWSurveyENG.FilterData : " & ex.Message & ex.StackTrace)
        End Try

    End Sub

    Private Sub SaveFilterData(ByVal FilterLnq As Cen.TwFilterCenLinqDB, ByVal ShopLnq As Cen.TwLocationCenLinqDB, ByVal dt As DataTable, ByVal TYPE As String)
        Try
            'Dim dt As New DataTable
            Dim WhText As String = ""
            If TYPE = "PAYMENT" Then
                WhText = " str_complete_date > '" & FilterLnq.LAST_SAVE_FILTER.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "'"
            Else
                WhText = " str_complete_date > '" & FilterLnq.LAST_SAVE_FILTER.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "'"
            End If
            WhText += " and source_type = '" & TYPE & "'"
            WhText += " and location_code='" & ShopLnq.LOCATION_CODE.Trim() & "'"
            dt.CaseSensitive = False
            dt.DefaultView.RowFilter = WhText
            dt = dt.DefaultView.ToTable

            'เพิ่มข้อมูลลงใน TW_FILTER_DATA
            If dt.Rows.Count > 0 Then
                Dim fsLnq As New Cen.TwFilterOrderTypeCenLinqDB
                Dim fSV As New DataTable
                Dim fsSql As String = ""
                If FilterLnq.CHK_ORDER_SFF = "N" Then
                    fsSql = "select fot.id,fot.tw_sff_order_type_id,o.order_type_name,fot.target_percent "
                    fsSql += " from tw_sff_order_type o "
                    fsSql += " inner join tw_filter_order_type fot on o.id=fot.tw_sff_order_type_id"
                    fsSql += " where fot.tw_filter_id='" & FilterLnq.ID & "' "
                    fSV = fsLnq.GetListBySql(fsSql, Nothing)
                Else
                    fSV = CalPercentTarget(FilterLnq)
                End If

                Dim vDateNow As DateTime = DateTime.Now
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    Dim OrderType As String = IIf(Convert.IsDBNull(dt.Rows(i)("order_type")) = False, dt.Rows(i)("order_type"), "")
                    If FilterLnq.TARGET_UNLIMITED = "N" Then
                        fSV.DefaultView.RowFilter = "order_type_name = '" & OrderType & "' and target_percent > 0"
                    End If
                    If fSV.DefaultView.Count > 0 Then
                        'ตรวจสอบ Target ถ้าได้ผลลัพธ์ตามจำนวน Target ที่ต้องการแล้วก็ไม่ต้องส่งอีก
                        If ChkTargetPerHour(FilterLnq, ShopLnq.ID, OrderType, Convert.ToDateTime(dt.Rows(i)("complete_date")), TYPE) = False Then
                            If ChkJustInsert(dt.Rows(i)("mobile_no"), Convert.ToInt64(dt.Rows(i)("id"))) = False Then
                                Dim trans As New TransactionDB
                                'Queue_unique_id Format = QueueNo + ShopAbb+yyyyMMddHHmmss  EX. A104PK25550326172423
                                Dim OrderQuiqueID As String = ShopLnq.LOCATION_CODE & dt.Rows(i).Item("mobile_no") & Convert.ToDateTime(dt.Rows(i)("complete_date")).ToString("yyyyMMddHHmmss", New CultureInfo("en-US"))
                                Dim lnq As New Cen.TwFilterDataCenLinqDB
                                If lnq.ChkDuplicateByORDER_UNIQUE_ID(OrderQuiqueID, lnq.ID, Nothing) = False Then
                                    lnq.TW_LOCATION_ID = ShopLnq.ID
                                    lnq.ORDER_TYPE_NAME = OrderType
                                    lnq.SERVICE_DATE = dt.Rows(i)("complete_date")
                                    lnq.ORDER_UNIQUE_ID = OrderQuiqueID
                                    lnq.MOBILE_NO = dt.Rows(i)("mobile_no")
                                    If Convert.IsDBNull(dt.Rows(i)("customer_name")) = False Then lnq.CUSTOMER_NAME = dt.Rows(i)("customer_name")
                                    If Convert.IsDBNull(dt.Rows(i)("mobile_segment")) = False Then lnq.CUSTOMER_SEGMENT = dt.Rows(i)("mobile_segment")
                                    lnq.TW_FILTER_ID = FilterLnq.ID

                                    Dim sffLnq As New CenLinqDB.TABLE.TwSffOrderTypeCenLinqDB
                                    Dim sffDt As DataTable = sffLnq.GetDataList("order_type_name='" & OrderType & "'", "", trans.Trans)
                                    If sffDt.Rows.Count > 0 Then
                                        lnq.TW_SFF_ORDER_TYPE_ID = Convert.ToInt64(sffDt.Rows(0)("id"))
                                    End If
                                    sffLnq = Nothing
                                    sffDt.Dispose()

                                    lnq.TEMPLATE_CODE = FilterLnq.TEMPLATE_CODE
                                    lnq.FILTER_TIME = vDateNow
                                    lnq.RESULT_STATUS = "0"
                                    lnq.END_TIME = dt.Rows(i)("complete_date")
                                    If Convert.IsDBNull(dt.Rows(i)("network_type")) = False Then lnq.NETWORK_TYPE = dt.Rows(i)("network_type")
                                    lnq.NPS_SCORE = -1
                                    lnq.TW_SOURCE_DATA_ID = Convert.ToInt64(dt.Rows(i)("id"))

                                    If lnq.InsertData("CSITWSurverENG.SaveFilterData", trans.Trans) Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("CSITWSurverENG.SaveFilterData", ShopLnq.LOCATION_NAME_TH & " " & lnq.MOBILE_NO & " " & lnq.SERVICE_DATE & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                        'PrintLog("CSITWSurverENG.SaveFilterData : " & ShopLnq.LOCATION_NAME_TH & " " & lnq.MOBILE_NO & " " & lnq.SERVICE_DATE & lnq.ErrorMessage)
                                    End If
                                End If
                                lnq = Nothing
                            End If
                        End If
                    End If
                    fSV.DefaultView.RowFilter = ""
                Next
                fSV.Dispose()
            End If
        Catch ex As Exception
            CreateLogFile("CSITWSurveyENG.SaveFilterData : " & ex.Message & ex.StackTrace)
        End Try
    End Sub

    'Sub SaveFilterData(ByVal FilterLnq As Cen.TwFilterCenLinqDB, ByVal ShopLnq As Cen.TwLocationCenLinqDB, ByVal lblTime As Label, ByVal TYPE As String)
    '    Dim dt As New DataTable
    '    Dim WhText As String
    '    If TYPE = "PAYMENT" Then
    '        'payment คิวรี่ย้อนหลังทั้งหมด
    '        WhText = " convert(varchar(8),sd.complete_date,112)=convert(varchar(8), DATEADD(day,-1,getdate()),112)"
    '        WhText += " and location_code='" & ShopLnq.LOCATION_CODE & "'"
    '        WhText += " and convert(varchar(19),sd.complete_date,120)>'" & FilterLnq.LAST_SAVE_FILTER.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "'"
    '        dt = GetQData(FilterLnq, WhText, "PAYMENT")
    '    Else
    '        'sff คืวรี่วันนี้ทั้งหมด
    '        WhText = " convert(varchar(8),sd.complete_date,112)=convert(varchar(8), getdate(),112)"
    '        WhText += " and location_code='" & ShopLnq.LOCATION_CODE & "'"
    '        WhText += " and convert(varchar(19),sd.complete_date,120)>'" & FilterLnq.LAST_SAVE_FILTER.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US")) & "'"
    '        dt = GetQData(FilterLnq, WhText, "SFF")
    '    End If

    '    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
    '    Application.DoEvents()

    '    'เพิ่มข้อมูลลงใน TW_FILTER_DATA
    '    If dt.Rows.Count > 0 Then
    '        dt = GetFilterMobileNo2Month(dt)
    '        If dt.Rows.Count > 0 Then
    '            lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
    '            Application.DoEvents()

    '            Dim fsLnq As New Cen.TwFilterOrderTypeCenLinqDB
    '            Dim fSV As New DataTable
    '            Dim fsSql As String = ""
    '            If FilterLnq.CHK_ORDER_SFF = "N" Then
    '                fsSql = "select fot.id,fot.tw_sff_order_type_id,o.order_type_name,fot.target_percent "
    '                fsSql += " from tw_sff_order_type o "
    '                fsSql += " inner join tw_filter_order_type fot on o.id=fot.tw_sff_order_type_id"
    '                fsSql += " where fot.tw_filter_id='" & FilterLnq.ID & "' "
    '                fSV = fsLnq.GetListBySql(fsSql, Nothing)
    '            Else
    '                fSV = CalPercentTarget(FilterLnq)
    '            End If

    '            Dim vDateNow As DateTime = DateTime.Now
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                Dim OrderType As String = IIf(Convert.IsDBNull(dt.Rows(i)("order_type")) = False, dt.Rows(i)("order_type"), "")
    '                If FilterLnq.TARGET_UNLIMITED = "N" Then
    '                    fSV.DefaultView.RowFilter = "order_type_name = '" & OrderType & "' and target_percent > 0"
    '                End If
    '                If fSV.DefaultView.Count > 0 Then
    '                    'ตรวจสอบ Target ถ้าได้ผลลัพธ์ตามจำนวน Target ที่ต้องการแล้วก็ไม่ต้องส่งอีก
    '                    If ChkTarget(FilterLnq, ShopLnq.ID, OrderType, Convert.ToDateTime(dt.Rows(i)("complete_date")), TYPE) = False Then
    '                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
    '                        Application.DoEvents()
    '                        If ChkJustInsert(dt.Rows(i)("mobile_no")) = False Then
    '                            Dim trans As New TransactionDB
    '                            'Queue_unique_id Format = QueueNo + ShopAbb+yyyyMMddHHmmss  EX. A104PK25550326172423
    '                            Dim lnq As New Cen.TwFilterDataCenLinqDB
    '                            lnq.TW_LOCATION_ID = ShopLnq.ID
    '                            lnq.ORDER_TYPE_NAME = OrderType
    '                            lnq.SERVICE_DATE = dt.Rows(i)("complete_date")
    '                            lnq.ORDER_UNIQUE_ID = ShopLnq.LOCATION_CODE & dt.Rows(i).Item("mobile_no") & Convert.ToDateTime(dt.Rows(i)("complete_date")).ToString("yyyyMMddHHmmss", New CultureInfo("en-US"))
    '                            lnq.MOBILE_NO = dt.Rows(i)("mobile_no")
    '                            If Convert.IsDBNull(dt.Rows(i)("customer_name")) = False Then lnq.CUSTOMER_NAME = dt.Rows(i)("customer_name")
    '                            If Convert.IsDBNull(dt.Rows(i)("mobile_segment")) = False Then lnq.CUSTOMER_SEGMENT = dt.Rows(i)("mobile_segment")
    '                            lnq.TW_FILTER_ID = FilterLnq.ID

    '                            Dim sffLnq As New CenLinqDB.TABLE.TwSffOrderTypeCenLinqDB
    '                            Dim sffDt As DataTable = sffLnq.GetDataList("order_type_name='" & OrderType & "'", "", Nothing)
    '                            If sffDt.Rows.Count > 0 Then
    '                                lnq.TW_SFF_ORDER_TYPE_ID = Convert.ToInt64(sffDt.Rows(0)("id"))
    '                            End If
    '                            sffLnq = Nothing
    '                            sffDt.Dispose()

    '                            lnq.TEMPLATE_CODE = FilterLnq.TEMPLATE_CODE
    '                            lnq.FILTER_TIME = vDateNow
    '                            lnq.RESULT_STATUS = "0"
    '                            lnq.END_TIME = dt.Rows(i)("complete_date")
    '                            If Convert.IsDBNull(dt.Rows(i)("network_type")) = False Then lnq.NETWORK_TYPE = dt.Rows(i)("network_type")
    '                            lnq.NPS_SCORE = -1

    '                            If lnq.InsertData("CSITWSurverENG.SendCSI", trans.Trans) Then
    '                                trans.CommitTransaction()
    '                            Else
    '                                trans.RollbackTransaction()
    '                                FunctionEng.SaveErrorLog("CSITWSurverENG.SendCSI", ShopLnq.LOCATION_NAME_TH & " " & lnq.MOBILE_NO & " " & lnq.SERVICE_DATE & lnq.ErrorMessage)
    '                                PrintLog("CSITWSurverENG.SendCSI : " & ShopLnq.LOCATION_NAME_TH & " " & lnq.MOBILE_NO & " " & lnq.SERVICE_DATE & lnq.ErrorMessage)
    '                            End If
    '                            lnq = Nothing
    '                        End If
    '                    End If
    '                End If
    '                lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
    '                Application.DoEvents()
    '                fSV.DefaultView.RowFilter = ""
    '            Next
    '            fSV.Dispose()
    '        End If
    '        dt.Dispose()
    '    End If
    'End Sub

    Function IsCompletedTarget(ByVal filterlnq As CenLinqDB.TABLE.TwFilterCenLinqDB, ByVal shop_id As String) As Boolean
        If filterlnq.TARGET_UNLIMITED = "Y" Then
            Return True
        End If

        Dim _acsualsurvey As Integer = 0
        Dim sql As String = "select count(ID) cnt from TW_FILTER_DATA where tw_filter_id = '" & filterlnq.ID & _
        "' and tw_location_id ='" & shop_id & "'  and result_status =2"
        Dim dt As New DataTable
        Dim fLnq As New Cen.TwFilterDataCenLinqDB
        dt = fLnq.GetListBySql(sql, Nothing)
        If dt.Rows.Count > 0 Then
            _acsualsurvey = CInt(dt.Rows(0)("cnt"))
        End If

        If _acsualsurvey >= filterlnq.TARGET Then
            Return False
        End If
        Return True
    End Function

    'Private Function GetQDataAllLocation(ByVal whText As String, ByVal SourceType As String) As DataTable
    '    Dim sql As String = ""
    '    sql += " SELECT sd.id, sd.order_type,sd.complete_date,sd.mobile_no,sd.customer_name,sd.mobile_segment, " & vbNewLine
    '    sql += " st.source_type, convert(varchar(19),sd.complete_date,120) str_complete_date, sd.network_type, " & vbNewLine
    '    sql += " sd.nationality,sd.location_code "
    '    sql += " FROM tw_source_data sd " & vbNewLine
    '    sql += " inner join TW_Source_Textfile st on sd.TW_Source_Textfile_id = st.id" & vbNewLine
    '    sql += " Where " & whText & vbNewLine
    '    sql += " and st.source_type = '" & SourceType & "'" & vbNewLine
    '    sql += " and sd.mobile_no not in (select mobile_no from TB_FILTER_ATT_BACKLIST_MOBILE)"

    '    Dim dt As New DataTable
    '    Dim Qlnq As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
    '    dt = Qlnq.GetListBySql(sql, Nothing)
    '    Qlnq = Nothing

    '    Return dt
    'End Function


    Private Function GetQueryDataAllLocation(ByVal whText As String, ByVal SourceType As String) As String
        Dim sql As String = ""
        sql += " SELECT sd.id, sd.order_type,sd.complete_date,sd.mobile_no,sd.customer_name,sd.mobile_segment, " & vbNewLine
        sql += " st.source_type, convert(varchar(19),sd.complete_date,120) str_complete_date, sd.network_type, " & vbNewLine
        sql += " sd.nationality,sd.location_code "
        sql += " FROM tw_source_data sd " & vbNewLine
        sql += " inner join TW_Source_Textfile st on sd.TW_Source_Textfile_id = st.id" & vbNewLine
        sql += " Where " & whText & vbNewLine
        sql += " and st.source_type = '" & SourceType & "'" & vbNewLine
        sql += " and sd.mobile_no not in (select mobile_no from TB_FILTER_ATT_BACKLIST_MOBILE)" & vbNewLine
        sql += " and sd.mobile_no not in (" & vbNewLine
        sql += "            select mobile_no" & vbNewLine
        sql += "            from TW_FILTER_DATA " & vbNewLine
        sql += "            where result_status >= '1'" & vbNewLine
        sql += "            and convert(varchar(10),send_time,120) >= convert(varchar(10),DATEADD(DAY, 0-(select top 1 config_value from sysconfig where config_name='CSITW_FILTER_LASTDAY') , getdate()),120) " & vbNewLine
        sql += " )" & vbNewLine

        Return sql
    End Function


    'Private Function GetQData(ByVal FilterLnq As Cen.TwFilterCenLinqDB, ByVal whText As String, ByVal SourceType As String) As DataTable
    '    Dim sql As String = ""
    '    sql += " SELECT  * FROM tw_source_data sd " & vbNewLine
    '    sql += " inner join TW_Source_Textfile st on sd.TW_Source_Textfile_id = st.id" & vbNewLine
    '    sql += " Where " & whText & vbNewLine
    '    sql += " and st.source_type = '" & SourceType & "'" & vbNewLine

    '    If FilterLnq.NATIONALITY.Trim <> "" Then
    '        'Nationality
    '        Dim vNation As String = ""
    '        Dim IsCheckThai As Boolean = False
    '        Dim IsCheckEng As Boolean = False
    '        Dim IsCheckOther As Boolean = False
    '        Dim IsCheckBlank As Boolean = False

    '        If FilterLnq.NATIONALITY.Trim.IndexOf("THAI") > -1 Then
    '            IsCheckThai = True
    '        End If
    '        If FilterLnq.NATIONALITY.Trim.IndexOf("ENG") > -1 Then
    '            IsCheckEng = True
    '        End If
    '        If FilterLnq.NATIONALITY.Trim.IndexOf("OTHER") > -1 Then
    '            IsCheckOther = True
    '        End If
    '        If FilterLnq.NATIONALITY.Trim.IndexOf("BLANK") > -1 Then
    '            IsCheckBlank = True
    '        End If

    '        If IsCheckOther = True Then
    '            If IsCheckThai = False And IsCheckEng = False Then
    '                vNation += " and UPPER(sd.nationality) not in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
    '            Else
    '                If IsCheckThai = False Then
    '                    vNation += " and UPPER(sd.nationality) not in ('THAILAND','THAI')" & vbNewLine
    '                End If
    '                If IsCheckEng = False Then
    '                    vNation += " and UPPER(sd.nationality) not in ('ENGLAND','ENG','United Kingdom')" & vbNewLine
    '                End If
    '            End If
    '        Else
    '            If IsCheckThai = True And IsCheckEng = True Then
    '                vNation += " and UPPER(sd.nationality) in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
    '            Else
    '                If IsCheckThai = True And IsCheckEng = False Then
    '                    vNation += " and UPPER(sd.nationality) in ('THAILAND','THAI')" & vbNewLine
    '                ElseIf IsCheckThai = False And IsCheckEng = True Then
    '                    vNation += " and UPPER(sd.nationality) in ('ENGLAND','ENG','United Kingdom')" & vbNewLine
    '                ElseIf IsCheckThai = False And IsCheckEng = False Then
    '                    vNation += " and UPPER(sd.nationality) not in ('THAILAND','THAI','ENGLAND','ENG','United Kingdom')" & vbNewLine
    '                End If
    '            End If
    '        End If
    '        If IsCheckBlank = False Then
    '            vNation += " and isnull(sd.nationality,'')<>''" & vbNewLine
    '        Else
    '            'ถ้า Tick Checkbox Nationality ทั้ง 4 ค่าเลย ก็ไม่ต้อง ตรวจสอบ Nationality
    '            If IsCheckThai = True And IsCheckEng = True And IsCheckOther = True Then

    '            Else
    '                vNation = " and (" & vNation.Substring(4) & " or isnull(sd.nationality,'')='')" & vbNewLine
    '            End If
    '        End If
    '        sql += vNation
    '    End If

    '    If FilterLnq.SEGMENT <> "0" Then
    '        Dim tmp As String = ""
    '        For Each seg As String In Split(FilterLnq.SEGMENT, ",")
    '            If tmp = "" Then
    '                tmp = "'" & seg & "'"
    '            Else
    '                tmp += "," & "'" & seg & "'"
    '            End If
    '        Next
    '        sql += " and sd.mobile_segment in (" & tmp & ") " & vbNewLine
    '    End If

    '    'หา Service ของ Filter
    '    Dim oLnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
    '    Dim sDt As New DataTable
    '    sDt = oLnq.GetDataList("tw_filter_id='" & FilterLnq.ID & "'", "", Nothing)
    '    If FilterLnq.CHK_ORDER_SFF = "Y" Then
    '        sDt = CalPercentTarget(FilterLnq)
    '    End If

    '    If sDt.Rows.Count > 0 Then
    '        sDt.DefaultView.RowFilter = "target_percent > 0"
    '        If sDt.DefaultView.Count > 0 Then
    '            Dim OrderTypeName As String = ""
    '            For Each sDr As DataRowView In sDt.DefaultView
    '                Dim sTmp As New Cen.TwSffOrderTypeCenLinqDB
    '                sTmp.GetDataByPK(sDr("tw_sff_order_type_id"), Nothing)
    '                If OrderTypeName = "" Then
    '                    OrderTypeName = "'" & sTmp.ORDER_TYPE_NAME & "'"
    '                Else
    '                    OrderTypeName += "," & "'" & sTmp.ORDER_TYPE_NAME & "'"
    '                End If
    '                sTmp = Nothing
    '            Next
    '            sql += " and sd.order_type in (" & OrderTypeName & ")" & vbNewLine
    '        End If
    '        sDt.Dispose()
    '    End If
    '    oLnq = Nothing

    '    If FilterLnq.NETWORK_TYPE.ToUpper <> "ALL" Then
    '        sql += " and sd.network_type = '" & FilterLnq.NETWORK_TYPE & "'" & vbNewLine
    '    End If

    '    Dim dt As New DataTable
    '    Dim Qlnq As New CenLinqDB.TABLE.TwFilterDataCenLinqDB
    '    dt = Qlnq.GetListBySql(sql, Nothing)
    '    Qlnq = Nothing

    '    If dt.Rows.Count > 0 Then
    '        Dim bDt As New DataTable
    '        Dim bSql As String = "select mobile_no "
    '        bSql += " from TB_FILTER_ATT_BACKLIST_MOBILE bm "
    '        bDt = FilterLnq.GetListBySql(bSql, Nothing)
    '        If bDt.Rows.Count > 0 Then
    '            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
    '                bDt.DefaultView.RowFilter = "mobile_no = '" & dt.Rows(i)("mobile_no").ToString & "'"
    '                If bDt.DefaultView.Count > 0 Then
    '                    dt.Rows.RemoveAt(i)
    '                End If
    '                bDt.DefaultView.RowFilter = ""
    '            Next
    '        End If
    '        bDt.Dispose()
    '    End If

    '    Return dt
    'End Function

    Private Function CalPercentTarget(ByVal FilterLnq As Cen.TwFilterCenLinqDB) As DataTable
        Dim sDt As New DataTable
        Dim oLnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
        Dim sql As String = " select fot.id,fot.target_percent,fot.target_perhour,fot.tw_sff_order_type_id,st.order_type_name"
        sql += " from TW_FILTER_ORDER_TYPE fot"
        sql += " inner join TW_SFF_ORDER_TYPE st on st.id=fot.tw_sff_order_type_id "
        sql += " where fot.tw_filter_id='" & FilterLnq.ID & "'"

        sDt = oLnq.GetListBySql(sql, Nothing)
        If sDt.Rows.Count > 0 Then
            sDt.DefaultView.RowFilter = "tw_sff_order_type_id<>'1'"   ' 1=PAYMENT
            Dim TargetPercent As Integer = FilterLnq.ORDER_SFF_PER / sDt.DefaultView.Count
            sDt.DefaultView.RowFilter = ""
            For i As Integer = 0 To sDt.Rows.Count - 1
                sDt.Rows(i)("target_percent") = TargetPercent

                If sDt.Rows(i)("tw_sff_order_type_id").ToString.ToUpper = "1" Then  'PAYMENT
                    sDt.Rows(i)("target_percent") = FilterLnq.ORDER_PAYMENT_PER
                End If
            Next
        End If
        Return sDt
    End Function

    Private Function GetFilterMobileNo2Month(ByVal dt As DataTable) As DataTable
        Dim FilterLastDay As String = FunctionEng.GetQisDBConfig("CSITW_FILTER_LASTDAY")
        If FilterLastDay.Trim = "" Then
            FilterLastDay = "60"
        End If

        Dim lnq As New Cen.TwFilterDataCenLinqDB
        Dim WhText As String = "result_status >= '1' "
        WhText += " and  convert(varchar(10),send_time,120) >= convert(varchar(10),DATEADD(DAY, " & (0 - Convert.ToInt16(FilterLastDay)) & " , getdate()),120) "

        Dim fDt As DataTable = lnq.GetDataList(WhText, "", Nothing)
        If fDt.Rows.Count > 0 Then
            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                fDt.DefaultView.RowFilter = "mobile_no = '" & dt.Rows(i)("mobile_no").ToString & "'"
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
    Private Function GetFilterBackList(ByVal dt As DataTable) As DataTable
        Dim bDt As New DataTable
        Dim bSql As String = "select mobile_no "
        bSql += " from TB_FILTER_ATT_BACKLIST_MOBILE bm "
        bDt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(bSql)
        If bDt.Rows.Count > 0 Then
            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                bDt.DefaultView.RowFilter = "mobile_no = '" & dt.Rows(i)("mobile_no").ToString & "'"
                If bDt.DefaultView.Count > 0 Then
                    dt.Rows.RemoveAt(i)
                End If
                bDt.DefaultView.RowFilter = ""
            Next
        End If
        bDt.Dispose()

        Return dt
    End Function

    Private Sub SetFilterActiveStatus()
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim fLnq As New Cen.TwFilterCenLinqDB
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
                                Dim ChkTime As DateTime
                                ChkTime = DateAdd(DateInterval.Minute, 0, DateTime.Now)
                                If ChkTime < fLnq.LAST_SAVE_FILTER Then
                                    fLnq.PROCESS_STATUS = "N"
                                Else
                                    fLnq.PROCESS_STATUS = "Y"
                                End If
                            Else
                                fLnq.PROCESS_STATUS = "N"
                            End If

                            If fLnq.UpdateByPK("CSITWSurveyENG.SetFilterActiveStatus", trans.Trans) = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("CSITWSurveyENG.SetFilterActiveStatus", "FilterName : " & fLnq.FILTER_NAME & " " & fLnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                            End If
                        End If
                    End If
                Next
                fDt = Nothing
            End If
            fLnq = Nothing
        Else
            FunctionEng.SaveErrorLog("CSITWSurveyENG.SetFilterActiveStatus", trans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
        End If
    End Sub

    Private Function GetFilterAllDay(ByVal fLnq As Cen.TwFilterCenLinqDB, ByVal DateNow As DateTime) As Boolean
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

    Private Function GetFilterByDay(ByVal fLnq As Cen.TwFilterCenLinqDB, ByVal DateNow As DateTime) As Boolean
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

    Private Function ChkTargetPerHour(ByVal FilterLnq As Cen.TwFilterCenLinqDB, ByVal LocationID As Long, ByVal OrderType As String, ByVal CompleteDateTime As DateTime, ByVal type As String) As Boolean
        Dim ret As Boolean = False
        If FilterLnq.TARGET_UNLIMITED = "N" Then
            Dim sql As String = "select [target],end_time_from,end_time_to "
            sql += " from TW_FILTER_TEMP_TARGET ftt"
            sql += " inner join TW_FILTER_ORDER_TYPE fot on fot.tw_sff_order_type_id=ftt.tw_sff_order_type_id"
            sql += " inner join TW_SFF_ORDER_TYPE st on st.id=fot.tw_sff_order_type_id "
            sql += " where ftt.tw_filter_id = '" & FilterLnq.ID & "'"
            sql += " and st.order_type_name = '" & OrderType & "'"
            sql += " and ftt.tw_location_id = '" & LocationID & "'"
            If type = "PAYMENT" Then
                sql += " and convert(varchar(8),service_date,112)='" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
            Else
                sql += " and convert(varchar(8),service_date,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
            End If
            sql += " and '" & CompleteDateTime.ToString("HH:mm") & "' between end_time_from and end_time_to"

            Dim tDt As New DataTable
            Dim tLnq As New Cen.TwFilterTempTargetCenLinqDB
            tDt = tLnq.GetListBySql(sql, Nothing)
            If tDt.Rows.Count > 0 Then
                sql = "select count(fd.id) filter"
                sql += " from tw_filter_data fd"
                sql += " where fd.tw_filter_id='" & FilterLnq.ID & "'"
                sql += " and fd.order_type_name='" & OrderType & "'"
                sql += " and fd.tw_location_id = '" & LocationID & "'"
                sql += " and fd.result_status='2' "
                If type = "PAYMENT" Then
                    sql += " and convert(varchar(8),fd.end_time,112)='" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
                    sql += " and CONVERT(varchar(5),fd.end_time,114) between '" & Convert.ToDateTime(tDt.Rows(0)("end_time_from")).ToString("HH:mm") & "' and '" & Convert.ToDateTime(tDt.Rows(0)("end_time_to")).ToString("HH:mm") & "'"
                Else
                    sql += " and convert(varchar(8),fd.end_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
                    sql += " and CONVERT(varchar(5),fd.end_time,114) between '" & Convert.ToDateTime(tDt.Rows(0)("end_time_from")).ToString("HH:mm") & "' and '" & Convert.ToDateTime(tDt.Rows(0)("end_time_to")).ToString("HH:mm") & "'"
                End If

                Dim dt As New DataTable
                Dim lnq As New Cen.TwFilterDataCenLinqDB
                dt = lnq.GetListBySql(sql, Nothing)
                If dt.Rows.Count > 0 Then
                    If Convert.ToDouble(dt.Rows(0)("filter")) >= Convert.ToDouble(tDt.Rows(0)("target")) Then
                        ret = True
                    End If
                    dt.Dispose()
                End If
                lnq = Nothing
            End If
            tDt.Dispose()
            tLnq = Nothing
        End If

        Return ret
    End Function

    'Private Function ChkTargetPerHour(ByVal FilterLnq As Cen.TwFilterCenLinqDB, ByVal LocationID As Long, ByVal OrderType As String, ByVal CompleteDateTime As DateTime, ByVal type As String) As Boolean
    '    Dim ret As Boolean = False
    '    If FilterLnq.TARGET_UNLIMITED = "N" Then
    '        Dim sql As String = "select [target],end_time_from,end_time_to "
    '        sql += " from TW_FILTER_TEMP_TARGET ftt"
    '        sql += " inner join TW_FILTER_ORDER_TYPE fot on fot.tw_sff_order_type_id=ftt.tw_sff_order_type_id"
    '        sql += " inner join TW_SFF_ORDER_TYPE st on st.id=fot.tw_sff_order_type_id "
    '        sql += " where ftt.tw_filter_id = '" & FilterLnq.ID & "'"
    '        sql += " and st.order_type_name = '" & OrderType & "'"
    '        sql += " and ftt.tw_location_id = '" & LocationID & "'"
    '        If type = "PAYMENT" Then
    '            sql += " and convert(varchar(8),service_date,112)='" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
    '        Else
    '            sql += " and convert(varchar(8),service_date,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
    '        End If
    '        sql += " and '" & CompleteDateTime.ToString("HH:mm") & "' between end_time_from and end_time_to"

    '        Dim tDt As New DataTable
    '        Dim tLnq As New Cen.TwFilterTempTargetCenLinqDB
    '        tDt = tLnq.GetListBySql(sql, Nothing)
    '        If tDt.Rows.Count > 0 Then

    '            sql = "select count(fd.id) filter"
    '            sql += " from tw_filter_data fd"
    '            sql += " where fd.tw_filter_id='" & FilterLnq.ID & "'"
    '            sql += " and fd.order_type_name='" & OrderType & "'"
    '            sql += " and fd.tw_location_id = '" & LocationID & "'"
    '            sql += " and fd.result_status='2' "
    '            If type = "PAYMENT" Then
    '                sql += " and convert(varchar(8),fd.end_time,112)='" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
    '                sql += " and CONVERT(varchar(5),fd.end_time,114) between '" & Convert.ToDateTime(tDt.Rows(0)("end_time_from")).ToString("HH:mm") & "' and '" & Convert.ToDateTime(tDt.Rows(0)("end_time_to")).ToString("HH:mm") & "'"
    '            Else
    '                sql += " and convert(varchar(8),fd.end_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'"
    '                sql += " and CONVERT(varchar(5),fd.end_time,114) between '" & Convert.ToDateTime(tDt.Rows(0)("end_time_from")).ToString("HH:mm") & "' and '" & Convert.ToDateTime(tDt.Rows(0)("end_time_to")).ToString("HH:mm") & "'"
    '            End If

    '            Dim dt As New DataTable
    '            Dim lnq As New Cen.TwFilterDataCenLinqDB
    '            dt = lnq.GetListBySql(sql, Nothing)
    '            If dt.Rows.Count > 0 Then
    '                If Convert.ToDouble(dt.Rows(0)("filter")) >= Convert.ToDouble(tDt.Rows(0)("target")) Then
    '                    ret = True
    '                End If
    '                dt.Dispose()
    '            End If
    '            lnq = Nothing
    '        End If
    '        tDt.Dispose()
    '        tLnq = Nothing
    '    End If

    '    Return ret
    'End Function

    Private Function ChkJustInsert(ByVal MobileNo As String, ByVal TwSourdeDataID As Long) As Boolean
        Dim ret As Boolean = False
        Dim lnq As New Cen.TwFilterDataCenLinqDB
        Dim dt As DataTable = lnq.GetDataList("mobile_no = '" & MobileNo & "' and result_status <> '2' and convert(varchar(8),filter_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "", Nothing)
        If dt.Rows.Count > 0 Then
            ret = True
        Else
            dt = New DataTable
            dt = lnq.GetDataList("tw_source_data_id='" & TwSourdeDataID & "'", "", Nothing)
            If dt.Rows.Count > 0 Then
                ret = True
            End If
        End If
        dt.Dispose()
        lnq = Nothing

        Return ret
    End Function
#End Region '_FilterData



#Region "_Sent ATSR"

    Public Sub CreateLogFile(ByVal TextMsg As String)
        Try
            Dim LogDir As String = Application.StartupPath & "\CSITWLog\"
            If Directory.Exists(LogDir) = False Then
                Directory.CreateDirectory(LogDir)
            End If

            Dim FILE_NAME As String = LogDir & DateTime.Now.ToString("yyyyMMddHH") & ".log"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & TextMsg & Chr(13) & Chr(13))
            objWriter.Close()
        Catch ex As Exception

        End Try
        
    End Sub

    Private Function CheckSendATSRbyOrderQniqueID(ByVal OrderUniqueID As String, ByVal vID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            'ถ้ามีข้อมูลว่าเคยส่งให้ ATSR ไปแล้วก็ไม่ต้องส่งซ้ำอีก
            Dim lnq As New Cen.TwFilterDataCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("order_unique_id='" & OrderUniqueID & "' and id<>'" & vID & "'", "", Nothing)
            If dt.Rows.Count > 0 Then
                ret = True
            End If
            dt.Dispose()
            lnq = Nothing
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSITWSurvey.CheckSendATSRbyOrderQniqueID", ex.Message & vbNewLine & ex.StackTrace, "\ErrorLog\", "CSITWSurveyENG")
        End Try
        Return ret
    End Function

    Public Sub SendATSR()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New Cen.TwFilterDataCenLinqDB
                Dim dt As DataTable = lnq.GetDataList("send_time is null and result_status='0' and convert(varchar(8),filter_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "", trans.Trans)
                trans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    Dim fDt As New DataTable
                    fDt = dt.DefaultView.ToTable(True, "tw_filter_id")
                    If fDt.Rows.Count > 0 Then
                        fDt.Columns.Add("fLnq", GetType(CenLinqDB.TABLE.TwFilterCenLinqDB))
                        For i As Integer = 0 To fDt.Rows.Count - 1
                            Dim fLnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
                            fLnq.GetDataByPK(fDt.Rows(i)("tw_filter_id"), Nothing)
                            fDt.Rows(i)("fLnq") = fLnq
                            fLnq = Nothing
                        Next
                    End If

                    For Each dr As DataRow In dt.Rows
                        Try
                            If CheckSendATSRbyOrderQniqueID(dr("order_unique_id"), dr("id")) = False Then
                                lnq.GetDataByPK(dr("id"), Nothing)
                                If lnq.ID > 0 Then
                                    Dim eng As New Engine.GateWay.GateWayServiceENG
                                    Dim shLnq As Cen.TwLocationCenLinqDB = FunctionEng.GetTwLocationCenLinqDB(dr("tw_location_id"))
                                    If eng.SendATSR(dr("mobile_no"), dr("order_unique_id"), shLnq.LOCATION_CODE, dr("template_code")) = True Then
                                        trans = New CenLinqDB.Common.Utilities.TransactionDB
                                        Dim iLnq As New Cen.TwFilterDataCenLinqDB
                                        iLnq.GetDataByPK(dr("id"), trans.Trans)
                                        iLnq.SEND_TIME = DateTime.Now
                                        iLnq.RESULT_STATUS = "1"

                                        If iLnq.UpdateByPK("CSITWSurverENG.SendATSR", trans.Trans) = True Then
                                            trans.CommitTransaction()

                                            fDt.DefaultView.RowFilter = "tw_filter_id='" & iLnq.TW_FILTER_ID & "'"
                                            If fDt.DefaultView.Count > 0 Then
                                                Dim fLnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
                                                fLnq = DirectCast(fDt.DefaultView(0)("fLnq"), CenLinqDB.TABLE.TwFilterCenLinqDB)

                                                If fLnq.GEN_ACT_ALL_SURVEY = "Y" Then
                                                    Dim shDt As New DataTable
                                                    shDt = GetLocationDetail(lnq.TW_LOCATION_ID, Nothing)
                                                    If shDt.Rows.Count > 0 Then
                                                        ' Gen Siebel Activity
                                                        Dim shDr As DataRow = shDt.Rows(0)
                                                        Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
                                                        sieRet = CreateSiebelActivityAllSurvey(shLnq.LOCATION_CODE, shLnq.LOCATION_NAME_TH, dr("mobile_no"), shDr("region_code"), fLnq)
                                                        If sieRet.RESULT = False Then
                                                            Dim _errDesc As String = "Error CreateSiebelActivity ID= " & lnq.ID & " MobileNO=" & dr("mobile_no") & " " & sieRet.ErrorMessage
                                                            FunctionEng.SaveErrorLog("CSITWSurverENG.SendATSR", _errDesc, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                                        Else
                                                            trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                            iLnq = New Cen.TwFilterDataCenLinqDB
                                                            iLnq.GetDataByPK(dr("id"), trans.Trans)
                                                            If iLnq.ID > 0 Then
                                                                iLnq.ACTIVITY_ID_SURVEY = sieRet.ACTIVITY_ID

                                                                If iLnq.UpdateByPK("CSITWSurverENG.SendATSR", trans.Trans) = True Then
                                                                    trans.CommitTransaction()
                                                                Else
                                                                    trans.RollbackTransaction()
                                                                    Dim _errDesc As String = "Error Update ACTIVITY_ID_SURVEY   ID= " & lnq.ID & " MobileNO=" & iLnq.MOBILE_NO & " " & iLnq.ErrorMessage
                                                                    CreateLogFile("SendATSR " & _errDesc)
                                                                    FunctionEng.SaveErrorLog("CSITWSurverENG.SendATSR", _errDesc, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                                                End If
                                                            Else
                                                                trans.RollbackTransaction()
                                                            End If
                                                        End If
                                                        sieRet = Nothing
                                                    End If
                                                    shDt.Dispose()
                                                End If
                                                fLnq = Nothing
                                            End If
                                            iLnq = Nothing
                                        Else
                                            trans.RollbackTransaction()
                                            Dim _errDesc As String = "ID= " & lnq.ID & " MobileNO=" & iLnq.MOBILE_NO & " " & iLnq.ErrorMessage
                                            CreateLogFile("SendATSR " & _errDesc)
                                            FunctionEng.SaveErrorLog("CSITWSurverENG.SendATSR", _errDesc, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                        End If
                                    End If
                                    eng = Nothing
                                    shLnq = Nothing
                                Else
                                    CreateLogFile("SendATSR " & lnq.ErrorMessage)
                                    FunctionEng.SaveErrorLog("CSITWSurverENG.SendATSR", lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                End If
                            End If
                        Catch ex As Exception
                            CreateLogFile("SendATSR " & ex.Message)
                            FunctionEng.SaveErrorLog("CSITWSurverENG.SendATSR", ex.Message, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                        End Try
                    Next
                    fDt.Dispose()
                    dt.Dispose()
                End If
            Else
                FunctionEng.SaveErrorLog("CSITWSurverENG.SendATSR ", trans.ErrorMessage, "\ErrorLog\", "CSITWSurveyENG")
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSITWSurvey.SendATSR", ex.Message & vbNewLine & ex.StackTrace, "\ErrorLog\", "CSITWSurveyENG")
        End Try
    End Sub
    Private Function CreateSiebelActivityResult3(ByVal ShopCode As String, ByVal ShopNameTh As String, ByVal MobileNo As String, ByVal RegionCode As String, ByVal fLnq As CenLinqDB.TABLE.TwFilterCenLinqDB) As CenParaDB.CSI.SiebelResponsePara
        Dim data As New CenParaDB.CSI.SiebelReqPara
        data.SIEBEL_TYPE = "Leave Voice Survey CSI"

        If RegionCode.ToUpper = "BKK" Then
            '    data.ACTIVITYCATEGORY = "ร้องเรียน-ชมพนักงาน TWZ / DEALER - กรุงเทพฯและปริมณฑล."
            data.ACTIVITYCATEGORY = fLnq.CATEGORY_GEN_ACT_RESULT3
            data.ACTIVITYSUBCATEGORY = fLnq.SUBCATEGORY_GEN_ACT_RESULT3
        Else
            '    data.ACTIVITYCATEGORY = "ร้องเรียน-ชมพนักงาน TWZ / DEALER - ต่างจังหวัด."
            data.ACTIVITYCATEGORY = fLnq.CAT_GEN_ACT_RESULT3_UPC
            data.ACTIVITYSUBCATEGORY = fLnq.SUBCAT_GEN_ACT_RESULT3_UPC
        End If
        data.SIEBEL_OWNERNAME = fLnq.OWNER_GEN_ACT_RESULT3 '"QAI"
        data.DESCRIPTION = "1. ข้อความที่ลูกค้าฝากไว้ ..." & vbNewLine & vbNewLine & _
        "2.Location / สาขา ที่ร้องเรียน    " & ShopCode & "/" & ShopNameTh & vbNewLine & _
        "3.QAI Team/ลูกค้า Leave Voice จากการสำรวจ CSI"
        data.STATUS = "Open"
        data.MOBILE_NO = MobileNo

        Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
        Dim gw As New Engine.GateWay.GateWayServiceENG
        sieRet = gw.CreateSiebelActivity(data)
        gw = Nothing
        data = Nothing

        Return sieRet
    End Function

    Private Function CreateSiebelActivityAllSurvey(ByVal ShopCode As String, ByVal ShopNameEn As String, ByVal MobileNo As String, ByVal RegionCode As String, ByVal fLnq As CenLinqDB.TABLE.TwFilterCenLinqDB) As CenParaDB.CSI.SiebelResponsePara
        Dim data As New CenParaDB.CSI.SiebelReqPara
        data.SIEBEL_TYPE = "Survey CSI"
        'data.ACTIVITYCATEGORY = "Survey"
        'data.ACTIVITYSUBCATEGORY = "CSI Survey Telewiz Shop"
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

#End Region '_Sent ATSR



#Region "_GetATSR_RESULT"
    Private Function GetLocationDetail(ByVal ShopID As Long, ByVal trans As SqlClient.SqlTransaction) As DataTable
        Dim ret As New DataTable
        Dim sql As String = "select location_code, isnull(location_name_en,location_name_th) location_name, region_code "
        sql += " from tw_location "
        sql += " where id='" & ShopID & "'"

        ret = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans)
        Return ret
    End Function

    Public Sub GetATSRResult()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New Cen.TwFilterDataCenLinqDB
                Dim dt As DataTable = lnq.GetDataList("result_time is null and result_status='1' and convert(varchar(8),send_time,112)='" & DateTime.Now.ToString("yyyyMMdd", New CultureInfo("en-US")) & "'", "", trans.Trans)
                trans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    Dim fDt As New DataTable
                    fDt = dt.DefaultView.ToTable(True, "tw_filter_id")
                    If fDt.Rows.Count > 0 Then
                        fDt.Columns.Add("fLnq", GetType(CenLinqDB.TABLE.TwFilterCenLinqDB))
                        For i As Integer = 0 To fDt.Rows.Count - 1
                            Dim fLnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
                            fLnq.GetDataByPK(fDt.Rows(i)("tw_filter_id"), Nothing)
                            fDt.Rows(i)("fLnq") = fLnq
                            fLnq = Nothing
                        Next
                    End If


                    For Each dr As DataRow In dt.Rows
                        lnq = New Cen.TwFilterDataCenLinqDB
                        Dim eng As New Engine.GateWay.GateWayServiceENG
                        Dim res As New CenParaDB.GateWay.ATSRResultPara
                        res = eng.GetATSRSurveyResult(dr("order_unique_id"))
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

                            If lnq.UpdateByPK("CSITWSurveyENG.GetATSRResult", trans.Trans) = True Then
                                trans.CommitTransaction()

                                If res.RESULT_VALUE.Trim = "ควรปรับปรุง" And res.HAVE_LEAVE_VOICE = True Then

                                    fDt.DefaultView.RowFilter = "tw_filter_id='" & lnq.TW_FILTER_ID & "'"
                                    If fDt.DefaultView.Count > 0 Then
                                        Dim fLnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
                                        fLnq = DirectCast(fDt.DefaultView(0)("fLnq"), CenLinqDB.TABLE.TwFilterCenLinqDB)

                                        If fLnq.GEN_ACT_RESULT3 = "Y" Then
                                            'If FunctionEng.GetQisDBConfig("CSITW_GEN_ACT_RESULT3") = "Y" Then
                                            'trans = New CenLinqDB.Common.Utilities.TransactionDB
                                            Dim shDt As New DataTable
                                            shDt = GetLocationDetail(lnq.TW_LOCATION_ID, Nothing)
                                            'trans.CommitTransaction()
                                            If shDt.Rows.Count > 0 Then
                                                Dim shDr As DataRow = shDt.Rows(0)
                                                Dim sieRet As New CenParaDB.CSI.SiebelResponsePara
                                                sieRet = CreateSiebelActivityResult3(shDr("location_code"), shDr("location_name"), lnq.MOBILE_NO, shDr("region_code"), fLnq)
                                                If sieRet.RESULT = False Then
                                                    FunctionEng.SaveErrorLog("CSITWSurveyENG.GetATSRResult", "Error CreateSiebelActivityResult3  QuqID=" & dr("order_unique_id") & " $$$ " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                                Else
                                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                                                    lnq = New Cen.TwFilterDataCenLinqDB
                                                    lnq.GetDataByPK(dr("id"), trans.Trans)
                                                    lnq.ACTIVITY_ID_LEAVE_VOICE = sieRet.ACTIVITY_ID

                                                    If lnq.UpdateByPK("CSITWSurveyENG.GetATSRResult", trans.Trans) = True Then
                                                        trans.CommitTransaction()
                                                    Else
                                                        FunctionEng.SaveErrorLog("CSITWSurveyENG.GetATSRResult", "Error UPDATE ACTIVITY_LEAVE_VOICE  QuqID=" & dr("order_unique_id"), Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                                    End If
                                                End If
                                                sieRet = Nothing
                                            Else
                                                FunctionEng.SaveErrorLog("CSITWSurveyENG.GetATSRResult", "Error GetShopDetail  QuqID=" & dr("order_unique_id"), Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                            End If
                                            shDt.Dispose()
                                            'End If

                                        End If
                                        fLnq = Nothing
                                    End If
                                End If

                            Else
                                trans.RollbackTransaction()
                                FunctionEng.SaveErrorLog("CSITWSurveyENG.GetATSRResult", "QuqID=" & dr("order_unique_id") & " $$$ " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
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

                                    If lnq.UpdateByPK("CSITWSurveyENG.GetATSRResult", trans.Trans) = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("CSITWSurveyENG.GetATSRResult", "orderID=" & dr("order_unique_id") & " $$$ " & lnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                                    End If
                            End Select
                        End If
                        eng = Nothing
                        res = Nothing
                        lnq = Nothing
                    Next
                    dt.Dispose()
                End If
            Else
                FunctionEng.SaveErrorLog("CSITWSurverENG.GetATSRResult", trans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
            End If
        Catch ex As Exception
            CreateLogFile("CSITWSurveyENG.GetATSRResult " & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

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
                End If
            End If
        End If

        Return ret
    End Function
#End Region



#Region "Calulate Filter Target"
    Public Sub CalTarget()
        Try
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim fLnq As New Cen.TwFilterCenLinqDB
                Dim fDt As DataTable = fLnq.GetDataList("cal_target = 'N' and active_status='Y'", "", trans.Trans)
                trans.CommitTransaction()
                If fDt.Rows.Count > 0 Then
                    For Each fDr As DataRow In fDt.Rows
                        If SaveFilterTempTarget(fDr("id"), "AisCSITWAgent") = False Then
                            FunctionEng.SaveErrorLog("CSITWSurveyENG.CalTarget", _err, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
                        End If
                    Next
                    fDt.Dispose()
                End If
            End If
        Catch ex As Exception
            FunctionEng.SaveErrorLog("CSITWSurveyENG.CalTarget", "Exception : " & ex.Message, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
        End Try
    End Sub

    Private Function GetTemplateDayQty(ByVal fLnq As CenLinqDB.TABLE.TwFilterCenLinqDB) As Integer
        Dim DayQty As Integer = 0
        Try
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
        Catch ex As Exception
            DayQty = 0
        End Try
        Return DayQty
    End Function

    Public Function SaveFilterTempTarget(ByVal FilterID As Long, ByVal LoginName As String) As Boolean
        'สูตรคำนวณ Target ต่อ Shop ต่อ Service = (Target/(จำนวนวัน * จำนวนชั่วโมง))* % ของแต่ละ Service
        Dim ret As Boolean = False
        Try
            Dim fLnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
            fLnq.GetDataByPK(FilterID, Nothing)
            If fLnq.ID > 0 Then
                Dim HourQty As Integer = CInt(Split(fLnq.PREIOD_TIMETO, ":")(0)) - CInt(Split(fLnq.PREIOD_TIMEFROM, ":")(0))
                Dim DayQty As Integer = GetTemplateDayQty(fLnq)
                If DayQty > 0 Then
                    Dim shDt As DataTable = fLnq.CHILD_TW_FILTER_BRANCH_tw_filter_id
                    If shDt.Rows.Count > 0 Then
                        Dim svLnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
                        Dim svDt As New DataTable
                        If fLnq.CHK_ORDER_SFF.Value = "N" Then
                            svDt = svLnq.GetDataList("tw_filter_id='" & fLnq.ID & "' and target_percent > 0", "", Nothing)
                        Else
                            svDt = svLnq.GetDataList("tw_filter_id='" & fLnq.ID & "'", "", Nothing)
                            If svDt.Rows.Count > 0 Then
                                svDt.DefaultView.RowFilter = "tw_sff_order_type_id<>'1'"   ' 1=PAYMENT
                                Dim TargetPercent As Integer = fLnq.ORDER_SFF_PER / svDt.DefaultView.Count
                                svDt.DefaultView.RowFilter = ""
                                For i As Integer = 0 To svDt.Rows.Count - 1
                                    svDt.Rows(i)("target_percent") = TargetPercent

                                    If svDt.Rows(i)("tw_sff_order_type_id").ToString.ToUpper = "1" Then  'PAYMENT
                                        svDt.Rows(i)("target_percent") = fLnq.ORDER_PAYMENT_PER
                                    End If
                                Next
                            End If
                        End If

                        If svDt.Rows.Count > 0 Then
                            For Each shDr As DataRow In shDt.Rows
                                'ทำ Transaction ภายใต้ Location
                                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                                For Each svDr As DataRow In svDt.Rows
                                    Dim tmp As Double = ((fLnq.TARGET * Convert.ToDouble(svDr("target_percent"))) / 100) / (DayQty * HourQty)
                                    Dim Target As Integer = Math.Ceiling(tmp)

                                    'Update Target Per Hour สำหรับแต่ละ Service
                                    Dim sLnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
                                    sLnq.GetDataByPK(svDr("id"), trans.Trans)
                                    sLnq.TARGET_PERHOUR = Target

                                    If sLnq.UpdateByPK(LoginName, trans.Trans) = True Then
                                        'Loop ตามวันที่
                                        Dim CurrDate As DateTime = fLnq.PREIOD_DATEFROM.Value
                                        Do
                                            If GetSelectedDateOfWeek(fLnq, CurrDate) = True Then
                                                For i As Integer = CInt(Split(fLnq.PREIOD_TIMEFROM, ":")(0)) To CInt(Split(fLnq.PREIOD_TIMETO, ":")(0)) - 1
                                                    Dim lnq As New CenLinqDB.TABLE.TwFilterTempTargetCenLinqDB
                                                    lnq.TW_FILTER_ID = fLnq.ID
                                                    lnq.TW_LOCATION_ID = Convert.ToInt64(shDr("tw_location_id"))
                                                    lnq.SERVICE_DATE = CurrDate
                                                    lnq.END_TIME_FROM = i.ToString.PadLeft(2, "0") & ":00"
                                                    lnq.END_TIME_TO = (i + 1).ToString.PadLeft(2, "0") & ":00"
                                                    lnq.TW_SFF_ORDER_TYPE_ID = Convert.ToInt64(svDr("tw_sff_order_type_id"))
                                                    ' lnq.USERNAME = GetFilterUserName(fLnq, Convert.ToInt64(svDr("tw_sff_order_type_id")), Convert.ToInt64(shDr("tb_shop_id")), trans)
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
                    Else
                        _err = "Location Not Found"
                    End If
                End If
            End If
        Catch ex As Exception
            ret = False
        End Try

        Return ret
    End Function

    Private Function GetSelectedDateOfWeek(ByVal fLnq As CenLinqDB.TABLE.TwFilterCenLinqDB, ByVal CurrDate As DateTime) As Boolean
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

#Region "Get CSI TWS Filter Data 2G Customer"
    Public Function GetData2GCustomerToday(ByVal vDate As DateTime) As DataTable
        Dim dt As New DataTable
        Try
            Dim LastMonth As DateTime = DateAdd(DateInterval.Month, -1, vDate)
            Dim vDateFrom As String = New Date(LastMonth.Year, LastMonth.Month, 1).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim vDateTo As String = New Date(LastMonth.Year, LastMonth.Month, DateTime.DaysInMonth(LastMonth.Year, LastMonth.Month)).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim sql As String = "select distinct mobile_no "
            sql += " from tw_filter_data "
            sql += " where network_type='2G'"
            sql += " and convert(varchar(8),service_date,112) between '" & vDateFrom & "' and '" & vDateTo & "'"

            Dim lnq As New Cen.TbFilterDataCenLinqDB
            dt = lnq.GetListBySql(sql, Nothing)
            lnq = Nothing
        Catch ex As Exception
            dt = New DataTable
            FunctionEng.SaveErrorLog("CSITWSurveyENG.GetData2GCustomerToday", "Exception : " & ex.Message, Application.StartupPath & "\ErrorLog\", "CSITWSurveyENG")
        End Try

        Return dt
    End Function
#End Region
End Class
