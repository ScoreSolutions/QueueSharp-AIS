Imports Engine.Utilities
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Net
Imports Microsoft.SqlServer

Public Class frmDailyImportCustomer
    Dim _err As String = ""
    'Dim _rowCount As Long = 0
    Const _ClassName As String = "frmDailyImportCustomer"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label3.Text = DateTime.Now.ToString("HH:mm:ss")
        Timer1.Enabled = False
        tmTime.Enabled = True

        'ตรวจสอบรายการที่จะต้องเริ่ม Import ใหม่
        Dim sql As String = "select * from TB_LOG_CUSTOMER_DAILY where start_time is null and import_msg = 'Waiting' order by create_date "
        Dim trans As New TransactionDB
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, trans.Trans)
        trans.CommitTransaction()
        If dt.Rows.Count > 0 Then
            Dim CustInfoDailyImportPath As String = FunctionENG.GetQisDBConfig("CustInfoDailyImportPath")
            If Directory.Exists(CustInfoDailyImportPath) = False Then
                Directory.CreateDirectory(CustInfoDailyImportPath)
            End If
            For j As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(j)

                'Copy Text File From Gateway
                If File.Exists(CustInfoDailyImportPath & dr("file_name")) = False Then
                    'ถ้ายังไม่มี Text File
                    If CopyFile(dr("file_name"), dr("file_size"), CustInfoDailyImportPath) = False Then
                        SetImportLogStatus(dr("id"), "File Not Found")
                        Continue For
                    End If
                Else
                    'ถ้ามี Text File อยู่แล้วให้ตรวจสอบว่าขนาดไฟล์เท่ากันหรือไม่
                    Dim fle As New FileInfo(CustInfoDailyImportPath & dr("file_name"))
                    If fle.Length <> Convert.ToInt64(dr("file_size")) Then
                        If CopyFile(dr("file_name"), dr("file_size"), CustInfoDailyImportPath) = False Then
                            SetImportLogStatus(dr("id"), "File Not Found")
                            Continue For
                        End If
                    End If
                End If

                If File.Exists(CustInfoDailyImportPath & dr("file_name")) = True Then
                    'ไฟล์มาแล้ว ก็เริ่ม Import ได้
                    ProgressBar1.Value = 0
                    lblStatus.Text = ""
                    ProgressBar2.Value = 0
                    lblUpdateStatus.Text = ""
                    PrintLog("Start Import File :" & dr("file_name"))
                    trans = New TransactionDB(False)
                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                    Dim sqlStart As String = "update TB_LOG_CUSTOMER_DAILY "
                    sqlStart += " set start_time = " & SqlDB.SetDateTime(DateTime.Now) & ", "
                    sqlStart += " import_msg = 'Importing'"
                    sqlStart += " where id = '" & dr("id") & "'"
                    If SqlDB.ExecuteNonQuery(sqlStart, trans.Trans) > 0 Then
                        trans.CommitTransaction()
                        Dim ret As Boolean = False
                        SetImportLogStatus(dr("id"), "Create Process File")

                        TextBox1.Text = CustInfoDailyImportPath & dr("file_name")
                        If dr("file_type") = "1" Then
                            If File.Exists(CustInfoDailyImportPath & "QIS_CUSINFO_PROCESS.dat") = True Then
                                File.Delete(CustInfoDailyImportPath & "QIS_CUSINFO_PROCESS.dat")
                            End If
                            File.Copy(CustInfoDailyImportPath & dr("file_name"), CustInfoDailyImportPath & "QIS_CUSINFO_PROCESS.dat")

                            ret = ImportCustInfoToTemp(CustInfoDailyImportPath & dr("file_name"), dr("id"))
                            If ret = True Then
                                UpdateCustInfo(CustInfoDailyImportPath & dr("file_name"), dr("id"))
                                InsertCustInfo(CustInfoDailyImportPath & dr("file_name"), dr("id"))
                            End If
                        ElseIf dr("file_type") = "2" Then
                            If File.Exists(CustInfoDailyImportPath & "QIS_CUSINFO_PROCESS.dat") = True Then
                                File.Delete(CustInfoDailyImportPath & "QIS_CUSINFO_PROCESS.dat")
                            End If
                            File.Copy(CustInfoDailyImportPath & dr("file_name"), CustInfoDailyImportPath & "QIS_CUSINFO_PROCESS.dat")

                            ret = ReplaceCustInfo(CustInfoDailyImportPath & dr("file_name"), dr("id"))
                        ElseIf dr("file_type") = "3" Then

                            If File.Exists(CustInfoDailyImportPath & "QIS_CAMP_PROCESS.dat") = True Then
                                File.Delete(CustInfoDailyImportPath & "QIS_CAMP_PROCESS.dat")
                            End If
                            File.Copy(CustInfoDailyImportPath & dr("file_name"), CustInfoDailyImportPath & "QIS_CAMP_PROCESS.dat")

                            If dr("file_name").ToString.IndexOf("PRE") > -1 Then
                                ret = ImportCampaign(CustInfoDailyImportPath & dr("file_name"), dr("id"))
                            Else
                                ret = ImportCampaign(CustInfoDailyImportPath & dr("file_name"), dr("id"))
                            End If
                        End If

                        If ret = True Then
                            PrintLog("Update Complete Status")
                            trans = New TransactionDB
                            Dim sqlEnd As String = "update TB_LOG_CUSTOMER_DAILY "
                            sqlEnd += " set end_time = " & SqlDB.SetDateTime(DateTime.Now) & ", "
                            sqlEnd += " import_msg = 'Complete' "
                            sqlEnd += " where id = '" & dr("id") & "'"
                            If SqlDB.ExecuteNonQuery(sqlEnd, trans.Trans) > 0 Then
                                trans.CommitTransaction()
                                UpdateGWComplete(dr("file_name"), "12")

                                CreateLogFile(txtLog.Text, dr("file_name"), CustInfoDailyImportPath)
                            Else
                                trans.RollbackTransaction()
                                FunctionENG.SaveErrorLog("Timer1_Tick.frmDailyImportCustomer", SqlDB.ErrorMessage)
                                PrintLog("Update Error :" & SqlDB.ErrorMessage)
                            End If
                        Else
                            FunctionENG.SaveErrorLog("Timer1_Tick.frmDailyImportCustomer", _err)
                            FunctionENG.SendErrorToSMS("File Name : " & dr("file_name") & ": " & _err)
                            PrintLog("Update Error :" & "File Name : " & dr("file_name") & ": " & _err)
                        End If
                    Else
                        trans.RollbackTransaction()
                        FunctionENG.SaveErrorLog("Timer1_Tick.frmDailyImportCustomer", SqlDB.ErrorMessage)
                        FunctionENG.SendErrorToSMS("File Name : " & dr("file_name") & ": " & SqlDB.ErrorMessage)
                        PrintLog("Update Error :" & "File Name : " & dr("file_name") & ": " & SqlDB.ErrorMessage)
                    End If
                End If
            Next
            dt.Dispose()
            dt = Nothing
        End If
        FlushMemory()
        Timer1.Enabled = True
        'End If
    End Sub

    Private Declare Function SetProcessWorkingSetSize Lib "kernel32" (ByVal hProcess As Long, ByVal dwMinimumWorkingSetSize As Long, ByVal dwMaximumWorkingSetSize As Long) As Boolean

    Private Sub FlushMemory()
        GC.Collect(1, GCCollectionMode.Forced)
        GC.WaitForPendingFinalizers()
        SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)

        'Dim myProcess As Process() = Process.GetProcessesByName("sqlservr")
        'For Each proc As Process In myProcess
        '    SetProcessWorkingSetSize(proc.Handle, -1, -1)
        'Next
    End Sub

    Private Sub CreateLogFile(ByVal LogText As String, ByVal FileName As String, ByVal CustInfoDailyImportPath As String)
        Dim FILE_NAME As String = CustInfoDailyImportPath & FileName.Replace(".dat", "") & ".txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        objWriter.WriteLine(LogText)
        objWriter.Close()

        txtLog.Text = ""
        TextBox1.Text = ""
        ProgressBar1.Value = 0
        ProgressBar2.Value = 0
        lblUpdateStatus.Text = ""
        lblStatus.Text = ""
    End Sub

    Private Sub UpdateGWComplete(ByVal FileName As String, ByVal ImpStatus As String)
        ' http://10.13.181.100/cgi-bin/procupdateimportfile.pl?filename=QIS_CUSINFO_20120214.dat&fstatus=12
        PrintLog("Start Update to Gateway")
        Dim InputPara As String = "filename=" & FileName & "&fstatus=" & ImpStatus
        Dim SMS_URL1 As String = FunctionENG.GetQisDBConfig("ImportFinishURL1") & InputPara
        Dim webRequest As WebRequest
        Dim webresponse As WebResponse
        webRequest = webRequest.Create(SMS_URL1)
        If SetStatusMsg(webRequest, webresponse, InputPara) = False Then
            Dim SMS_URL2 As String = FunctionENG.GetQisDBConfig("ImportFinishURL2") & InputPara
            webRequest = webRequest.Create(SMS_URL2)
            SetStatusMsg(webRequest, webresponse, InputPara)
        End If
    End Sub

    Private Function SetStatusMsg(ByVal webRequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String) As Boolean
        Dim ret As Boolean = False
        webRequest.ContentType = "application/x-www-form-urlencoded"
        webRequest.Method = "POST"
        Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
        Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
        Dim os As Stream = Nothing

        Try
            webRequest.ContentLength = bytes.Length
            os = webRequest.GetRequestStream()
            os.Write(bytes, 0, bytes.Length)
            webresponse = webRequest.GetResponse()
            Dim Stream As New StreamReader(webresponse.GetResponseStream())
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp <> "DONE" Then
                    _err = tmp
                    ret = False
                    PrintLog("Error Update To Gateway")
                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.SetStatusMsg", "Error Update To Gateway : " & tmp)
                Else
                    PrintLog("Complete Update To Gateway")
                    ret = True
                End If
            End If
            Stream.Close()
            Stream = Nothing

        Catch ex As WebException
            _err = ex.Message
            PrintLog("Error Update To Gateway : " & ex.Message)
        Finally
            If os IsNot Nothing Then
                os.Close()
            End If
        End Try

        Return ret
    End Function

    Private Function ImportCampaignToTemp(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        'Import Campaign To TB_CUSTOMER_TEMP
        Dim ret As Boolean = False
        Try
            PrintLog("Start Insert Campaign TextFile To TB_CUSTOMER_TEMP :" & fleName)
            FunctionENG.SaveTransLog("frmDailyImportCustomer.ImportCampaignToTemp", "Import Campaign Data To TB_CUSTOMER_TEMP :" & fleName)
            If UpdateTotalRowCount(fleName, ImpLogID) = True Then
                If SetImportLogStatus(ImpLogID, "Execute SSIS CampaignTextFileToCustomerTemp") = True Then
                    'Execute SSIS CampaignTextFileToCustomerTemp
                    PrintLog("Execute SSIS CampaignTextFileToCustomerTemp")
                    Dim app As New Microsoft.SqlServer.Dts.Runtime.Application
                    Dim pkg As Dts.Runtime.Package = app.LoadFromSqlServer("CampaignTextFileToCustomerTemp", SqlDB.HQMainServer, SqlDB.HQMainDbUserID, SqlDB.HQMainDbPwd, Nothing)
                    Dim pkgResults As Dts.Runtime.DTSExecResult = pkg.Execute()

                    If pkgResults.ToString = "Success" Then
                        ret = True
                        PrintLog("Finish Execute SSIS CampaignTextFileToCustomerTemp :" & fleName)
                    Else
                        ret = False
                        Dim _err As String = ""
                        For Each Err As Dts.Runtime.DtsError In pkg.Errors
                            If _err = "" Then
                                _err = Err.ErrorCode & " :: " & Err.Description
                            Else
                                _err += vbNewLine & Err.ErrorCode & " :: " & Err.Description
                            End If
                        Next
                        PrintLog("Error Execute SSIS CampaignTextFileToCustomerTemp :" & fleName & vbNewLine & _err)
                        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfoToTemp", "Error Insert Campaign TextFile To TB_CUSTOMER_TEMP :" & fleName & vbNewLine & _err)
                    End If






                    'Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
                    'Dim ErrCount As Long = 0
                    'Dim i As Long = 0
                    'While Stream.Peek <> -1
                    '    Dim str As String = Stream.ReadLine
                    '    If str.Trim <> "" Then
                    '        Dim strFld As String() = Split(str, "|")
                    '        Try
                    '            If strFld(0).PadLeft(2, "0") = "01" Then
                    '                ProgressBar1.Maximum = strFld(2)
                    '                i += 1
                    '                Continue While
                    '            End If

                    '            Dim lnq As New TbCustomerTempCenLinqDB
                    '            lnq.MOBILE_NO = strFld(1)
                    '            lnq.CAMPAIGN_CODE = strFld(2).Trim().Replace("'", "''")
                    '            lnq.CAMPAIGN_NAME = strFld(3).Trim().Replace("'", "''")
                    '            lnq.CAMPAIGN_DESC_TH2 = strFld(4).Trim().Replace("'", "''")
                    '            lnq.CAMPAIGN_NAME_EN = strFld(5).Trim().Replace("'", "''")
                    '            lnq.CAMPAIGN_DESC = strFld(6).Trim().Replace("'", "''")
                    '            lnq.CAMPAIGN_DESC_EN2 = strFld(7).Trim().Replace("'", "''")

                    '            Dim trans As New TransactionDB(False)
                    '            trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                    '            ret = lnq.InsertData("InsertDaily", trans.Trans)
                    '            If ret = True Then
                    '                PrintLog("Inserting Data")
                    '                If UpdateImportedRow(ImpLogID, "row_count", trans) = True Then
                    '                    trans.CommitTransaction()
                    '                Else
                    '                    trans.RollbackTransaction()
                    '                End If
                    '            Else
                    '                trans.RollbackTransaction()
                    '                ErrCount += 1
                    '                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCampaignToTemp", "Mobile No :" & lnq.MOBILE_NO & " " & lnq.ErrorMessage)
                    '                PrintLog("Mobile No :" & lnq.MOBILE_NO & " " & lnq.ErrorMessage)
                    '            End If
                    '            If ProgressBar1.Value + 1 <= ProgressBar1.Maximum Then
                    '                ProgressBar1.Value = i + 1
                    '                lblStatus.Text = "Row " & (i + 1) & " of " & ProgressBar1.Maximum & " Rows" & vbNewLine
                    '                If ProgressBar1.Maximum > 0 Then
                    '                    lblStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar1.Maximum), 2) & "%"
                    '                Else
                    '                    lblStatus.Text += 0 & "%"
                    '                End If
                    '                Application.DoEvents()
                    '            End If
                    '        Catch ex As IndexOutOfRangeException
                    '            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCampaignToTemp", "MobileNo : " & strFld(1) & ex.Message)
                    '            ErrCount += 1
                    '            UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                    '            UpdateGWComplete(fleName, "99")   'หยุดการ Import
                    '            FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                    '            Exit While
                    '        End Try
                    '    End If
                    '    i += 1
                    'End While
                    'Stream.Close()
                    'Stream = Nothing
                    'If ErrCount >= (ProgressBar1.Maximum / 100) Then
                    '    FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                    'End If
                End If
                PrintLog("Finish Insert Campaign Data Temp")
                FunctionENG.SaveTransLog("frmDailyImportCustomer.ImportCampaignToTemp", "Finish Insert Campaign Data Temp")
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCampaignToTemp", "OutOfMemoryException " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCampaignToTemp", "Exception " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        End Try
        Return ret
    End Function

    Private Function ImportCustInfoToTemp(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        'Import CustInfo To TB_CUSTOMER_TEMP
        Dim ret As Boolean = False
        Try
            PrintLog("Start Import TextFile To TB_CUSTOMER_TEMP :" & fleName)
            FunctionENG.SaveTransLog(_ClassName & ".ImportCustInfoToTemp", "Import TextFile To TB_CUSTOMER_TEMP :" & fleName)
            If UpdateTotalRowCount(fleName, ImpLogID) = True Then
                If SetImportLogStatus(ImpLogID, "Import TextFile to TB_CUSTOMER_TEMP") = True Then
                    'Execute SSIS TextFileToBulkInsert
                    PrintLog("Execute SSIS TextFileToBulkInsert")
                    Dim app As New Microsoft.SqlServer.Dts.Runtime.Application
                    Dim pkg As Dts.Runtime.Package = app.LoadFromSqlServer("TextFileToCustomerTemp", SqlDB.HQMainServer, SqlDB.HQMainDbUserID, SqlDB.HQMainDbPwd, Nothing)
                    Dim pkgResults As Dts.Runtime.DTSExecResult = pkg.Execute()

                    If pkgResults.ToString = "Success" Then
                        ret = True
                        PrintLog("Finish Import TextFile To TB_CUSTOMER_TEMP :" & fleName)
                    Else
                        ret = False
                        Dim _err As String = ""
                        For Each Err As Dts.Runtime.DtsError In pkg.Errors
                            If _err = "" Then
                                _err = Err.ErrorCode & " :: " & Err.Description
                            Else
                                _err += vbNewLine & Err.ErrorCode & " :: " & Err.Description
                            End If
                        Next
                        PrintLog("Error Import TextFile To TB_CUSTOMER_TEMP :" & fleName & vbNewLine & _err)
                        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfoToTemp", "Error Import TextFile To TB_CUSTOMER_TEMP :" & fleName & vbNewLine & _err)
                    End If
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfoToTemp", "OutOfMemoryException " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfoToTemp", "Exception " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        End Try
        Return ret
    End Function

    Private Sub InsertCustInfo(ByVal fleName As String, ByVal ImpLogID As Long)
        Dim ret As Boolean = False
        Try
            PrintLog("Start Insert CustInfo :" & fleName)
            FunctionENG.SaveTransLog(_ClassName & ".InsertCustInfo", "Start Insert CustInfo :" & fleName)
            If File.Exists(fleName) = True Then
                Dim i As Long = 0
                Dim ErrCount As Long = 0

                Dim trans As New TransactionDB(False)
                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                If trans.Trans IsNot Nothing Then
                    Dim lnqT As New TbCustomerTempCenLinqDB
                    PrintLog("Query Data before insert")
                    Dim dt As DataTable = lnqT.GetDataList("mobile_no not in (select mobile_no from tb_customer)", "", trans.Trans)
                    trans.CommitTransaction()
                    If dt.Rows.Count > 0 Then
                        ProgressBar1.Value = 0
                        ProgressBar1.Maximum = dt.Rows.Count
                        PrintLog("Finish Query Data (" & dt.Rows.Count & ")")
                        If SetImportLogStatus(ImpLogID, "Inserting Data") = True Then
                            For Each drT As DataRow In dt.Rows
                                Try
                                    trans = New TransactionDB(False)
                                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                    Dim lnq As New TbCustomerCenLinqDB
                                    lnq.F_NAME = IIf(Convert.IsDBNull(drT("F_NAME")), "", drT("F_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.L_NAME = IIf(Convert.IsDBNull(drT("L_NAME")), "", drT("L_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.TITLE_NAME = IIf(Convert.IsDBNull(drT("TITLE_NAME")), "", drT("TITLE_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.EMAIL = IIf(Convert.IsDBNull(drT("EMAIL")), "", drT("EMAIL").ToString.Trim().Replace("'", "''"))
                                    lnq.MOBILE_NO = IIf(Convert.IsDBNull(drT("MOBILE_NO")), "", drT("MOBILE_NO").ToString.Trim().Replace("'", "''"))
                                    lnq.MOBILE_STATUS = IIf(Convert.IsDBNull(drT("MOBILE_STATUS")), "", drT("MOBILE_STATUS").ToString.Trim().Replace("'", "''"))
                                    lnq.BIRTH_DATE = IIf(Convert.IsDBNull(drT("BIRTH_DATE")), "", drT("BIRTH_DATE").ToString.Trim().Replace("'", "''"))
                                    lnq.SEGMENT_LEVEL = IIf(Convert.IsDBNull(drT("SEGMENT_LEVEL")), "", drT("SEGMENT_LEVEL").ToString.Trim().Replace("'", "''"))
                                    lnq.CATEGORY = IIf(Convert.IsDBNull(drT("CATEGORY")), "", drT("CATEGORY").ToString.Trim().Replace("'", "''"))
                                    lnq.ACCOUNT_BALANCE = IIf(Convert.IsDBNull(drT("ACCOUNT_BALANCE")), "", drT("ACCOUNT_BALANCE").ToString.Trim().Replace("'", "''"))
                                    lnq.CONTACT_CLASS = IIf(Convert.IsDBNull(drT("CONTACT_CLASS")), "", drT("CONTACT_CLASS").ToString.Trim().Replace("'", "''"))
                                    lnq.SERVICE_YEAR = IIf(Convert.IsDBNull(drT("SERVICE_YEAR")), "", drT("SERVICE_YEAR").ToString.Trim().Replace("'", "''"))
                                    lnq.CHUM_SCORE = IIf(Convert.IsDBNull(drT("CHUM_SCORE")), "", drT("CHUM_SCORE").ToString.Trim().Replace("'", "''"))
                                    lnq.CAMPAIGN_CODE = IIf(Convert.IsDBNull(drT("CAMPAIGN_CODE")), "", drT("CAMPAIGN_CODE").ToString.Trim().Replace("'", "''"))
                                    lnq.CAMPAIGN_NAME = IIf(Convert.IsDBNull(drT("CAMPAIGN_NAME")), "", drT("CAMPAIGN_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.PREFER_LANG = IIf(Convert.IsDBNull(drT("PREFER_LANG")), "", drT("PREFER_LANG").ToString.Trim().Replace("'", "''"))
                                    lnq.NETWORK_TYPE = IIf(Convert.IsDBNull(drT("NETWORK_TYPE")), "", drT("NETWORK_TYPE").ToString.Trim().Replace("'", "''"))

                                    trans = New TransactionDB(False)
                                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                    ret = lnq.InsertData("DailyInsert", trans.Trans)
                                    If ret = True Then
                                        PrintLog("Inserting Data")
                                        If UpdateImportedRow(ImpLogID, "row_count_insert", trans) = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                        End If
                                    Else
                                        trans.RollbackTransaction()

                                        ErrCount += 1
                                        Dim errMsg As String = "Insert Error : " & lnq.ErrorMessage & " MobileNo:" & lnq.MOBILE_NO & "  ConnectionString:" & trans.conn.ConnectionString
                                        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", errMsg)
                                        PrintLog(errMsg)
                                    End If

                                    If ProgressBar1.Value + 1 <= ProgressBar1.Maximum Then
                                        ProgressBar1.Value = i + 1
                                        lblStatus.Text = "Row " & (i + 1) & " of " & ProgressBar1.Maximum & " Rows" & vbNewLine
                                        If ProgressBar1.Maximum > 0 Then
                                            lblStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar1.Maximum), 2) & "%"
                                        Else
                                            lblStatus.Text += 0 & "%"
                                        End If
                                    End If
                                Catch ex As IndexOutOfRangeException
                                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "MobileNo : " & drT("MOBILE_NO") & ex.Message)
                                    ErrCount += 1
                                    PrintLog("Import Error Stop Process " & ex.Message)
                                    UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                                    UpdateGWComplete(fleName, "99")   'หยุดการ Import
                                    FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                                    Exit For
                                End Try
                                i += 1
                            Next

                            If ErrCount >= (ProgressBar1.Maximum / 100) Then
                                FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                                PrintLog("Import Error " & "File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                            End If
                        End If
                        dt.Dispose()
                        dt = Nothing
                        PrintLog("Finish Insert CustInfo :" & fleName)
                        FunctionENG.SaveTransLog(_ClassName & ".InsertCustInfo", "Finish Insert CustInfo :" & fleName)
                    End If
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "OutOfMemoryException " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "Exception " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        End Try
    End Sub

    Private Delegate Sub DegUpdateCustInfo(ByVal fleName As String, ByVal ImpLogID As Long)
    Private Sub UpdateCustInfo(ByVal fleName As String, ByVal ImpLogID As Long)
        Dim ret As Boolean = False
        Try
            PrintLog("Start Update Current Data : " & fleName)
            FunctionENG.SaveTransLog(_ClassName & ".UpdateCustInfo", "Start Update Current Data : " & fleName)
            If SetImportLogStatus(ImpLogID, "Update Current Data") = True Then
                Dim i As Long = 0
                Dim ErrCount As Long = 0

                Dim trans As New TransactionDB(False)
                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                If trans.Trans IsNot Nothing Then
                    PrintLog("Query Data before update")
                    Dim lnqT As New TbCustomerTempCenLinqDB
                    Dim dt As DataTable = lnqT.GetDataList("mobile_no in (select mobile_no from tb_customer)", "", trans.Trans)
                    trans.CommitTransaction()
                    If dt.Rows.Count > 0 Then
                        PrintLog("Finish Query Data before update (" & dt.Rows.Count & " Rows)")
                        ProgressBar2.Maximum = dt.Rows.Count
                        If SetImportLogStatus(ImpLogID, "Updating Data") = True Then
                            For Each drT As DataRow In dt.Rows
                                Try
                                    trans = New TransactionDB(False)
                                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                    Dim lnq As New TbCustomerCenLinqDB
                                    lnq.F_NAME = IIf(Convert.IsDBNull(drT("F_NAME")), "", drT("F_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.L_NAME = IIf(Convert.IsDBNull(drT("L_NAME")), "", drT("L_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.TITLE_NAME = IIf(Convert.IsDBNull(drT("TITLE_NAME")), "", drT("TITLE_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.EMAIL = IIf(Convert.IsDBNull(drT("EMAIL")), "", drT("EMAIL").ToString.Trim().Replace("'", "''"))
                                    lnq.MOBILE_NO = IIf(Convert.IsDBNull(drT("MOBILE_NO")), "", drT("MOBILE_NO").ToString.Trim().Replace("'", "''"))
                                    lnq.MOBILE_STATUS = IIf(Convert.IsDBNull(drT("MOBILE_STATUS")), "", drT("MOBILE_STATUS").ToString.Trim().Replace("'", "''"))
                                    lnq.BIRTH_DATE = IIf(Convert.IsDBNull(drT("BIRTH_DATE")), "", drT("BIRTH_DATE").ToString.Trim().Replace("'", "''"))
                                    lnq.SEGMENT_LEVEL = IIf(Convert.IsDBNull(drT("SEGMENT_LEVEL")), "", drT("SEGMENT_LEVEL").ToString.Trim().Replace("'", "''"))
                                    lnq.CATEGORY = IIf(Convert.IsDBNull(drT("CATEGORY")), "", drT("CATEGORY").ToString.Trim().Replace("'", "''"))
                                    lnq.ACCOUNT_BALANCE = IIf(Convert.IsDBNull(drT("ACCOUNT_BALANCE")), "", drT("ACCOUNT_BALANCE").ToString.Trim().Replace("'", "''"))
                                    lnq.CONTACT_CLASS = IIf(Convert.IsDBNull(drT("CONTACT_CLASS")), "", drT("CONTACT_CLASS").ToString.Trim().Replace("'", "''"))
                                    lnq.SERVICE_YEAR = IIf(Convert.IsDBNull(drT("SERVICE_YEAR")), "", drT("SERVICE_YEAR").ToString.Trim().Replace("'", "''"))
                                    lnq.CHUM_SCORE = IIf(Convert.IsDBNull(drT("CHUM_SCORE")), "", drT("CHUM_SCORE").ToString.Trim().Replace("'", "''"))
                                    lnq.CAMPAIGN_CODE = IIf(Convert.IsDBNull(drT("CAMPAIGN_CODE")), "", drT("CAMPAIGN_CODE").ToString.Trim().Replace("'", "''"))
                                    lnq.CAMPAIGN_NAME = IIf(Convert.IsDBNull(drT("CAMPAIGN_NAME")), "", drT("CAMPAIGN_NAME").ToString.Trim().Replace("'", "''"))
                                    lnq.PREFER_LANG = IIf(Convert.IsDBNull(drT("PREFER_LANG")), "", drT("PREFER_LANG").ToString.Trim().Replace("'", "''"))
                                    lnq.NETWORK_TYPE = IIf(Convert.IsDBNull(drT("NETWORK_TYPE")), "", drT("NETWORK_TYPE").ToString.Trim().Replace("'", "''"))

                                    Dim uSql As String = "UPDATE TB_CUSTOMER SET "
                                    uSql += " UPDATE_BY = 'DailyUpdate', UPDATE_DATE = getdate(), "
                                    uSql += " TITLE_NAME = '" & lnq.TITLE_NAME & "', "
                                    uSql += " F_NAME = '" & lnq.F_NAME & "', L_NAME = '" & lnq.L_NAME & "', EMAIL = '" & lnq.EMAIL & "', "
                                    uSql += " PREFER_LANG = '" & lnq.PREFER_LANG & "', SEGMENT_LEVEL = '" & lnq.SEGMENT_LEVEL & "', BIRTH_DATE = '" & lnq.BIRTH_DATE & "', MOBILE_STATUS = '" & lnq.MOBILE_STATUS & "', "
                                    uSql += " CATEGORY = '" & lnq.CATEGORY & "', ACCOUNT_BALANCE = '" & lnq.ACCOUNT_BALANCE & "', CONTACT_CLASS = '" & lnq.CONTACT_CLASS & "', SERVICE_YEAR = '" & lnq.SERVICE_YEAR & "', "
                                    uSql += " CHUM_SCORE = '" & lnq.CHUM_SCORE & "', NETWORK_TYPE = '" & lnq.NETWORK_TYPE & "'"
                                    uSql += " Where MOBILE_NO = '" & lnq.MOBILE_NO & "' "

                                    ret = lnq.UpdateBySql(uSql, trans.Trans)
                                    If ret = True Then
                                        PrintLog("Updating Data")
                                        If UpdateImportedRow(ImpLogID, "row_count_update", trans) = True Then
                                            trans.CommitTransaction()
                                        Else
                                            ErrCount += 1
                                            trans.RollbackTransaction()
                                        End If
                                    Else
                                        ErrCount += 1
                                        trans.RollbackTransaction()
                                        PrintLog("Mobile No:" & lnq.MOBILE_NO & " Sql:" & uSql & " Err :" & lnq.ErrorMessage)
                                    End If

                                    If ProgressBar2.Value + 1 <= ProgressBar2.Maximum Then
                                        ProgressBar2.Value = i + 1
                                        lblUpdateStatus.Text = "Update Status Row " & (i + 1) & " of " & ProgressBar2.Maximum & " Rows" & vbNewLine
                                        If ProgressBar2.Maximum > 0 Then
                                            lblUpdateStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar2.Maximum), 2) & "%"
                                        Else
                                            lblUpdateStatus.Text += 0 & "%"
                                        End If
                                        Application.DoEvents()
                                    End If
                                Catch ex As IndexOutOfRangeException
                                    FunctionENG.SaveErrorLog(_ClassName & ".ImportCustInfo", "MobileNo : " & drT("MOBILE_NO") & ex.Message)
                                    ErrCount += 1
                                    PrintLog("Import Error Stop Process " & ex.Message)
                                    UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                                    UpdateGWComplete(fleName, "99")   'หยุดการ Import
                                    FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                                    Exit For
                                End Try
                                i += 1
                            Next
                            If ErrCount >= (ProgressBar2.Maximum / 100) Then
                                FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                                PrintLog("Import Error " & "File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                            End If
                        End If
                        dt.Dispose()
                        dt = Nothing
                        PrintLog("Finish Update CustInfo : " & fleName)
                        FunctionENG.SaveTransLog(_ClassName & ".UpdateCustInfo", "Finish Update CustInfo : " & fleName)
                    End If
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "OutOfMemoryException " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "Exception " & ex.Message)
            PrintLog("Import Error " & ex.Message)
        End Try
    End Sub

    'Private Function ImportCustInfo(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
    '    Dim ret As Boolean = False

    '    Try
    '        PrintLog(fleName)
    '        If File.Exists(fleName) = True Then
    '            PrintLog("Exists : " & fleName)
    '            Dim i As Long = 0
    '            Dim ErrCount As Long = 0
    '            Dim CompleteRows As Long = 0
    '            Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
    '            While Stream.Peek <> -1
    '                Dim str As String = Stream.ReadLine
    '                If str.Trim <> "" Then
    '                    Dim strFld As String() = Split(str, "|")
    '                    Try

    '                        If strFld(0) = "01" Then
    '                            ProgressBar1.Maximum = strFld(2)
    '                            Continue While
    '                        End If

    '                        Dim lnq As New TbCustomerCenLinqDB
    '                        lnq.F_NAME = strFld(1)
    '                        lnq.L_NAME = strFld(2)
    '                        lnq.TITLE_NAME = strFld(3)
    '                        lnq.EMAIL = strFld(4)
    '                        lnq.MOBILE_NO = strFld(5)
    '                        lnq.MOBILE_STATUS = strFld(6)
    '                        lnq.BIRTH_DATE = strFld(7)
    '                        lnq.SEGMENT_LEVEL = strFld(8)
    '                        lnq.CATEGORY = strFld(9)
    '                        lnq.ACCOUNT_BALANCE = strFld(10)
    '                        lnq.CONTACT_CLASS = strFld(11)
    '                        lnq.SERVICE_YEAR = strFld(12)
    '                        lnq.CHUM_SCORE = strFld(13)
    '                        lnq.CAMPAIGN_CODE = strFld(14)
    '                        lnq.CAMPAIGN_NAME = strFld(15)
    '                        'lnq.CAMPAIGN_NAME_EN = strFld(16)
    '                        'lnq.CAMPAIGN_DESC = strFld(17)
    '                        lnq.PREFER_LANG = strFld(16)
    '                        lnq.NETWORK_TYPE = strFld(17)

    '                        Dim uSql As String = "UPDATE TB_CUSTOMER SET "
    '                        uSql += " UPDATE_BY = 'DailyUpdate', UPDATE_DATE = getdate(), "
    '                        uSql += " TITLE_NAME = '" & lnq.TITLE_NAME & "', "
    '                        uSql += " F_NAME = '" & lnq.F_NAME & "', L_NAME = '" & lnq.L_NAME & "', EMAIL = '" & lnq.EMAIL & "', "
    '                        uSql += " PREFER_LANG = '" & lnq.PREFER_LANG & "', SEGMENT_LEVEL = '" & lnq.SEGMENT_LEVEL & "', BIRTH_DATE = '" & lnq.BIRTH_DATE & "', MOBILE_STATUS = '" & lnq.MOBILE_STATUS & "', "
    '                        uSql += " CATEGORY = '" & lnq.CATEGORY & "', ACCOUNT_BALANCE = '" & lnq.ACCOUNT_BALANCE & "', CONTACT_CLASS = '" & lnq.CONTACT_CLASS & "', SERVICE_YEAR = '" & lnq.SERVICE_YEAR & "', "
    '                        uSql += " CHUM_SCORE = '" & lnq.CHUM_SCORE & "', NETWORK_TYPE = '" & lnq.NETWORK_TYPE & "'"
    '                        uSql += " Where MOBILE_NO = '" & lnq.MOBILE_NO & "' "

    '                        Dim trans As New TransactionDB(False)
    '                        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
    '                        ret = lnq.UpdateBySql(uSql, trans.Trans)
    '                        If ret = False Then
    '                            'Dim iSql As String = ""
    '                            'iSql += "insert into tb_customer (create_by, create_on, "
    '                            'iSql += "mobile_no,title_name,f_name,l_name, email, prefer_lang, segment_level, birth_date,mobile_status,"
    '                            'iSql += "category, account_balance, contact_class, service_year, chum_score, campaign_code, "
    '                            'iSql += "campaign_name, network_type, campaign_name_en, campaign_desc, campaign_desc_th2, campaign_desc_en2) "
    '                            'iSql += " values('DailyInsert',getdate(), '" & lnq.MOBILE_NO & "', '" & lnq.TITLE_NAME & "')"
    '                            ret = lnq.InsertData("DailyInsert", trans.Trans)
    '                            If ret = True Then
    '                                PrintLog("Inserting Data")
    '                                CompleteRows += 1

    '                                If UpdateImportedRow(ImpLogID, trans) = True Then
    '                                    trans.CommitTransaction()
    '                                Else
    '                                    trans.RollbackTransaction()
    '                                End If
    '                            Else
    '                                trans.RollbackTransaction()

    '                                ErrCount += 1
    '                                Dim errMsg As String = "Insert Error : " & lnq.ErrorMessage & " MobileNo:" & lnq.MOBILE_NO & "  ConnectionString:" & trans.conn.ConnectionString
    '                                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", errMsg)
    '                                PrintLog(errMsg)
    '                            End If
    '                        Else
    '                            PrintLog("Update Data")
    '                            CompleteRows += 1
    '                            If UpdateImportedRow(ImpLogID, trans) = True Then
    '                                trans.CommitTransaction()
    '                            Else
    '                                trans.RollbackTransaction()
    '                            End If
    '                        End If

    '                        If ProgressBar1.Value + 1 <= ProgressBar1.Maximum Then
    '                            ProgressBar1.Value = i + 1
    '                            lblStatus.Text = "Row " & (i + 1) & " of " & ProgressBar1.Maximum & " Rows" & vbNewLine
    '                            If ProgressBar1.Maximum > 0 Then
    '                                lblStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar1.Maximum), 2) & "%"
    '                            Else
    '                                lblStatus.Text += 0 & "%"
    '                            End If
    '                            Application.DoEvents()
    '                        End If
    '                    Catch ex As IndexOutOfRangeException
    '                        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "MobileNo : " & strFld(1) & ex.Message)
    '                        ErrCount += 1
    '                        PrintLog("Import Error Stop Process " & ex.Message)
    '                        UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
    '                        UpdateGWComplete(fleName, "99")   'หยุดการ Import
    '                        FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
    '                        Exit While
    '                    End Try
    '                End If
    '                i += 1
    '            End While
    '            Stream.Close()
    '            Stream = Nothing
    '            If ErrCount >= (100 / ProgressBar1.Maximum) Then
    '                FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
    '                PrintLog("Import Error " & "File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
    '            End If
    '            _rowCount = CompleteRows
    '        End If
    '    Catch ex As OutOfMemoryException
    '        _err = ex.Message
    '        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "OutOfMemoryException " & ex.Message)
    '        PrintLog("Import Error " & ex.Message)
    '    Catch ex As Exception
    '        _err = ex.Message
    '        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCustInfo", "Exception " & ex.Message)
    '        PrintLog("Import Error " & ex.Message)
    '    End Try

    '    Return ret
    'End Function
    

    Private Function UpdateImportedRow(ByVal cID As Long, ByVal FldName As String, ByVal Trans As TransactionDB) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        sql += " update TB_LOG_CUSTOMER_DAILY "
        sql += " set " & FldName & " = isnull(" & FldName & ",0)+1 "
        sql += " where id = '" & cID & "'"
        Try
            If SqlDB.ExecuteNonQuery(sql, Trans.Trans) > 0 Then
                ret = True
            End If
        Catch ex As Exception
            PrintLog("frmDailyImportCustomer.UpdateImportedRow " & ex.Message)
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.UpdateImportedRow", ex.Message)
        End Try

        Return ret
    End Function

    Private Function SetImportLogStatus(ByVal cID As Long, ByVal StatusText As String) As Boolean
        Dim ret As Boolean = False
        Dim trans As New TransactionDB(False)
        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
        Dim sqlStart As String = "update TB_LOG_CUSTOMER_DAILY "
        sqlStart += " set import_msg = '" & StatusText & "'"
        sqlStart += " where id = '" & cID & "'"
        If SqlDB.ExecuteNonQuery(sqlStart, trans.Trans) > 0 Then
            trans.CommitTransaction()
            ret = True
        Else
            trans.RollbackTransaction()
        End If
        Return ret
    End Function

    Private Function ClearCustomerTemp() As Boolean
        Dim ret As Boolean = False
        Dim trans As New TransactionDB(False)
        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
        If SqlDB.ExecuteNonQuery("truncate table TB_CUSTOMER_TEMP", trans.Trans) = True Then
            trans.CommitTransaction()
            ret = True
        Else
            trans.RollbackTransaction()
            ret = False
        End If
        Return ret
    End Function

    Private Function CreateBulkCustomerTable() As Boolean
        SqlDB.ExecuteNonQuery("drop table TB_BULK_INSERT_CUSTOMER")

        Dim ret As Boolean = False
        Dim sql As String = ""
        sql += " create table TB_BULK_INSERT_CUSTOMER(" & vbNewLine
        sql += " ROW_STATUS varchar(10), "
        sql += " F_NAME varchar(200)," & vbNewLine
        sql += " L_NAME varchar(200)," & vbNewLine
        sql += " TITLE_NAME varchar(200)," & vbNewLine
        sql += " EMAIL varchar(200)," & vbNewLine
        sql += " MOBILE_NO varchar(200)," & vbNewLine
        sql += " MOBILE_STATUS varchar(200)," & vbNewLine
        sql += " BIRTH_DATE varchar(200)," & vbNewLine
        sql += " SEGMENT_LEVEL varchar(200)," & vbNewLine
        sql += " CATEGORY varchar(200)," & vbNewLine
        sql += " ACCOUNT_BALANCE varchar(200)," & vbNewLine
        sql += " CONTACT_CLASS varchar(200)," & vbNewLine
        sql += " SERVICE_YEAR varchar(200)," & vbNewLine
        sql += " CHUM_SCORE varchar(200)," & vbNewLine
        sql += " CAMPAIGN_CODE varchar(200)," & vbNewLine
        sql += " CAMPAIGN_NAME varchar(200)," & vbNewLine
        sql += " PREFER_LANG varchar(200)," & vbNewLine
        sql += " NETWORK_TYPE varchar(200)" & vbNewLine
        sql += " ) "

        sql += " CREATE UNIQUE NONCLUSTERED INDEX [Index_1] ON [TB_BULK_INSERT_CUSTOMER] ("
        sql += " MOBILE_NO ASC )"
        sql += " WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, "
        sql += " IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, "
        sql += " ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] "

        Dim trans As New TransactionDB()
        If SqlDB.ExecuteNonQuery(sql, trans.Trans) = True Then
            ret = True
            trans.CommitTransaction()
        Else
            trans.RollbackTransaction()
        End If
        Return ret
    End Function

    Private Function ReplaceCustInfo1(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            If File.Exists(fleName) Then
                PrintLog("Start ReplaceCustInfo File Name : " & fleName)
                FunctionENG.SaveTransLog("frmDailyImportCustomer.ReplaceCustInfo", "Start ReplaceCustInfo File Name : " & fleName)

                If CreateBulkCustomerTable() = True Then
                    Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
                    While Stream.Peek <> -1
                        Dim str As String = Stream.ReadLine
                        If str.Trim <> "" Then
                            Dim strFld As String() = Split(str, "|")
                            Try
                                If strFld(0) = "01" Then
                                    Dim trans As New TransactionDB(False)
                                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                    Dim sqlEnd As String = "update TB_LOG_CUSTOMER_DAILY "
                                    sqlEnd += " set end_time = " & SqlDB.SetDateTime(DateTime.Now) & ", "
                                    sqlEnd += " row_count = '" & strFld(2) & "'"
                                    sqlEnd += " where id = '" & ImpLogID & "'"
                                    If SqlDB.ExecuteNonQuery(sqlEnd, trans.Trans) > 0 Then
                                        trans.CommitTransaction()
                                        ret = True
                                    Else
                                        trans.RollbackTransaction()
                                        ret = False
                                    End If
                                    Exit While
                                End If
                            Catch ex As IndexOutOfRangeException
                                ret = False
                                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "MobileNo : " & strFld(1) & ex.Message)
                                UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                                UpdateGWComplete(fleName, "99")   'หยุดการ Import
                                FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                                Exit While
                            End Try
                        End If
                    End While
                    Stream.Close()
                    Stream = Nothing











                    If ret = True Then
                        Dim cmd As String = "bcp.exe "
                        Dim args As String = Chr(34) & SqlDB.HQMainDbName & ".dbo.TB_BULK_INSERT_CUSTOMER" & Chr(34) & " in " & Chr(34) & fleName & Chr(34)
                        args += " -c -t " & Chr(34) & "|" & Chr(34) & " -r " & Chr(34) & "\n" & Chr(34) & " -T -F 2"
                        args += " -S " & SqlDB.HQMainServer & " -U " & SqlDB.HQMainDbUserID & " -P " & SqlDB.HQMainDbPwd '& " -L 10000001 "

                        Dim proc As New ProcessStartInfo(cmd, args)
                        Dim pr As Process = Process.Start(proc)
                        SetImportLogStatus(ImpLogID, "Copy From Text File")
                        PrintLog("Copy From Text File :" & fleName)
                        Application.DoEvents()
                        System.Threading.Thread.Sleep(10000)
                        Do
                            System.Threading.Thread.Sleep(10000)
                            Try
                                Dim pp As Process = Process.GetProcessById(pr.Id)
                            Catch ex As Exception
                                ret = True
                                pr.Close()
                                Exit Do
                            End Try
                        Loop While 1 = 1   'ลูปไปเรื่อยๆ จนกว่าโปรแกรม bcp จะปิดไปเอง

                        If ret = True Then
                            Dim trans As New TransactionDB(False)
                            trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                            PrintLog("Backup Data and Move From TEMP TO Production")
                            Application.DoEvents()

                            'TB_CUSTOMER_BACKUP
                            Dim sqlB As String = ""
                            sqlB += " select top 1 id from tb_customer "
                            Dim dtB As DataTable = SqlDB.ExecuteTable(sqlB, trans.Trans)
                            If dtB.Rows.Count > 0 Then
                                'ถ้ามีข้อมูลใน TB_CUSTOMER ก็ทำการ BACKUP ก่อน
                                sqlB = "select MIN(mobile_no) min_no, MAX(mobile_no) max_no from tb_customer"
                                dtB = SqlDB.ExecuteTable(sqlB, trans.Trans)
                                trans.CommitTransaction()
                                If dtB.Rows.Count > 0 Then
                                    SetImportLogStatus(ImpLogID, "Backup Customer Profile")

                                    Dim LoopCount As Long = 1000
                                    Dim MinNo As Long = Convert.ToInt64(dtB.Rows(0)("min_no"))
                                    Dim MaxNo As Long = Convert.ToInt64(dtB.Rows(0)("max_no"))
                                    Dim CurrNo As Long = MinNo
                                    Dim LoopNo As Long = (MaxNo - MinNo) / LoopCount

                                    For i As Integer = 1 To LoopCount
                                        trans = New TransactionDB(False)
                                        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                        sqlB = ""
                                        sqlB += " INSERT INTO [TB_CUSTOMER_BACKUP]([id],[create_by],[create_date],[update_by],[update_date]"
                                        sqlB += " ,[mobile_no],[title_name],[f_name],[l_name],[email]"
                                        sqlB += " ,[prefer_lang],[segment_level],[birth_date],[mobile_status],[category],[account_balance]"
                                        sqlB += " ,[contact_class],[service_year],[chum_score],[campaign_code],[campaign_name],[network_type]"
                                        sqlB += " ,[campaign_name_en],[campaign_desc],[campaign_desc_th2],[campaign_desc_en2],[backup_date])"

                                        sqlB += " SELECT [id],[create_by],[create_date],[update_by],[update_date]"
                                        sqlB += " ,[mobile_no],[title_name],[f_name],[l_name],[email]"
                                        sqlB += " ,[prefer_lang],[segment_level],[birth_date],[mobile_status],[category],[account_balance]"
                                        sqlB += " ,[contact_class],[service_year],[chum_score],[campaign_code],[campaign_name],[network_type]"
                                        sqlB += " ,[campaign_name_en],[campaign_desc],[campaign_desc_th2],[campaign_desc_en2], GETDATE()"
                                        sqlB += " FROM [TB_CUSTOMER] "
                                        sqlB += " WHERE convert(bigint, mobile_no) between " & CurrNo & " and " & (CurrNo + LoopNo)

                                        If SqlDB.ExecuteNonQuery(sqlB, trans.Trans) > 0 Then
                                            trans.CommitTransaction()
                                            PrintLog(i & "Backup Customer Mobile No: 0" & CurrNo.ToString & " to 0" & (CurrNo + LoopNo).ToString)
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error Backup table TB_CUSTOMER " & SqlDB.ErrorMessage & vbNewLine & sqlB)
                                            PrintLog(i & "Error Backup table TB_CUSTOMER " & SqlDB.ErrorMessage & "$$$$ " & sqlB)
                                        End If
                                        CurrNo += LoopNo + 1
                                    Next
                                    dtB.Dispose()
                                    dtB = Nothing
                                Else
                                    trans.CommitTransaction()
                                End If
                                PrintLog("Finish Backup Customer Profile to TB_CUSTOMER_BACKUP")
                            Else
                                trans.CommitTransaction()
                            End If

                            'Move Data จาก TB_BULK_INSERT_CUSTOMER ไปยัง TB_CUSTOMER
                            PrintLog("Move Data From TEMP TO Production")
                            SetImportLogStatus(ImpLogID, "Move Data TO Production")
                            trans = New TransactionDB(False)
                            trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                            If SqlDB.ExecuteNonQuery("truncate table TB_CUSTOMER", trans.Trans) = True Then
                                trans.CommitTransaction()

                                trans = New TransactionDB(False)
                                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                Dim sqlM As String = ""
                                sqlM = "select MIN(mobile_no) min_no, MAX(mobile_no) max_no from TB_BULK_INSERT_CUSTOMER"
                                Dim dtM As DataTable = SqlDB.ExecuteTable(sqlM, trans.Trans)
                                trans.CommitTransaction()
                                If dtM.Rows.Count > 0 Then
                                    Dim LoopCount As Long = 2000
                                    Dim MinNo As Long = Convert.ToInt64(dtM.Rows(0)("min_no"))
                                    Dim MaxNo As Long = Convert.ToInt64(dtM.Rows(0)("max_no"))
                                    Dim CurrNo As Long = MinNo
                                    Dim LoopNo As Long = (MaxNo - MinNo) / LoopCount

                                    For i As Integer = 1 To LoopCount
                                        sqlM = ""
                                        sqlM += " INSERT INTO [TB_CUSTOMER] ([create_by],[create_date],"
                                        sqlM += " [mobile_no],[title_name],[f_name],[l_name],[email],[prefer_lang],[segment_level],[birth_date],"
                                        sqlM += " [mobile_status],[category],[account_balance],[contact_class],[service_year],[chum_score],"
                                        sqlM += " [campaign_code],[campaign_name],[network_type])"

                                        sqlM += " select 'REPLACE',GETDATE(),"
                                        sqlM += " MOBILE_NO,TITLE_NAME,F_NAME,L_NAME,EMAIL,PREFER_LANG,SEGMENT_LEVEL,BIRTH_DATE,"
                                        sqlM += " MOBILE_STATUS,CATEGORY,ACCOUNT_BALANCE,CONTACT_CLASS,SERVICE_YEAR,CHUM_SCORE,"
                                        sqlM += " CAMPAIGN_CODE, CAMPAIGN_NAME, NETWORK_TYPE"
                                        sqlM += " from TB_BULK_INSERT_CUSTOMER "
                                        sqlM += " WHERE convert(bigint, mobile_no) between " & CurrNo & " and " & (CurrNo + LoopNo)

                                        trans = New TransactionDB(False)
                                        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                        Dim insRow As Long = SqlDB.ExecuteNonQuery(sqlM, trans.Trans)
                                        If insRow > 0 Then
                                            PrintLog(i & " Move Data to Production Mobile No: 0" & CurrNo.ToString & " to 0" & (CurrNo + LoopNo).ToString)

                                            Dim sqlEnd As String = "update TB_LOG_CUSTOMER_DAILY "
                                            sqlEnd += " set end_time = " & SqlDB.SetDateTime(DateTime.Now) & ", "
                                            sqlEnd += " row_count_insert = isnull(row_count_insert,0) + " & insRow
                                            sqlEnd += " where id = '" & ImpLogID & "'"
                                            If SqlDB.ExecuteNonQuery(sqlEnd, trans.Trans) > 0 Then
                                                trans.CommitTransaction()
                                            Else
                                                trans.RollbackTransaction()
                                                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error Move Data : " & sqlEnd & " " & SqlDB.ErrorMessage)
                                                PrintLog(i & " Error Move Data : " & sqlEnd & " " & SqlDB.ErrorMessage)
                                            End If
                                        Else
                                            trans.RollbackTransaction()
                                            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error Move Data : " & sqlM & " " & SqlDB.ErrorMessage)
                                            PrintLog(i & " Error Move Data : " & sqlM & " " & SqlDB.ErrorMessage)
                                        End If
                                        CurrNo += LoopNo + 1
                                    Next

                                    PrintLog("Finish Move Data From TEMP TO Production")
                                    dtM.Dispose()
                                    dtM = Nothing
                                End If
                            Else
                                ret = False
                                trans.RollbackTransaction()
                                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error truncate table TB_CUSTOMER " & SqlDB.ErrorMessage)
                                PrintLog("Error truncate table TB_CUSTOMER " & SqlDB.ErrorMessage)
                            End If
                            PrintLog("Finish ReplaceCustInfo File Name : " & fleName)
                            FunctionENG.SaveTransLog("frmDailyImportCustomer.ReplaceCustInfo", "Finish ReplaceCustInfo File Name : " & fleName)

                        End If
                    End If
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        End Try

        Return ret
    End Function

    Private Function ReplaceCustInfo2(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            If File.Exists(fleName) Then
                PrintLog("Start ReplaceCustInfo File Name : " & fleName)
                FunctionENG.SaveTransLog("frmDailyImportCustomer.ReplaceCustInfo", "Start ReplaceCustInfo File Name : " & fleName)
                Dim trans As New TransactionDB(False)
                PrintLog("Clear TB_CUSTOMER_TEMP")
                If ClearCustomerTemp() = True Then
                    Dim i As Long = 0
                    Dim ErrCount As Long = 0
                    Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
                    While Stream.Peek <> -1
                        Dim str As String = Stream.ReadLine
                        If str.Trim <> "" Then
                            Dim strFld As String() = Split(str, "|")
                            Try
                                If strFld(0) = "01" Then
                                    ProgressBar1.Maximum = strFld(2)
                                    Continue While
                                End If
                                Dim lnq As New TbCustomerTempCenLinqDB
                                lnq.F_NAME = strFld(1)
                                lnq.L_NAME = strFld(2)
                                lnq.TITLE_NAME = strFld(3)
                                lnq.EMAIL = strFld(4)
                                lnq.MOBILE_NO = strFld(5)
                                lnq.MOBILE_STATUS = strFld(6)
                                lnq.BIRTH_DATE = strFld(7)
                                lnq.SEGMENT_LEVEL = strFld(8)
                                lnq.CATEGORY = strFld(9)
                                lnq.ACCOUNT_BALANCE = strFld(10)
                                lnq.CONTACT_CLASS = strFld(11)
                                lnq.SERVICE_YEAR = strFld(12)
                                lnq.CHUM_SCORE = strFld(13)
                                lnq.CAMPAIGN_CODE = strFld(14)
                                lnq.CAMPAIGN_NAME = strFld(15)
                                'lnq.CAMPAIGN_NAME_EN = strFld(16)
                                'lnq.CAMPAIGN_DESC = strFld(17)
                                lnq.PREFER_LANG = strFld(16)
                                lnq.NETWORK_TYPE = strFld(17)

                                trans = New TransactionDB(False)
                                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                ret = lnq.InsertData("ReplaceInsert", trans.Trans)
                                If ret = True Then
                                    PrintLog("Inserting To Temp")
                                    If UpdateImportedRow(ImpLogID, "row_count", trans) = True Then
                                        trans.CommitTransaction()
                                    Else
                                        trans.RollbackTransaction()
                                    End If
                                Else
                                    trans.RollbackTransaction()
                                    ErrCount += 1
                                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", lnq.ErrorMessage)
                                End If

                                If ProgressBar1.Value + 1 <= ProgressBar1.Maximum Then
                                    ProgressBar1.Value = i + 1
                                    lblStatus.Text = "Row " & (i + 1) & " of " & ProgressBar1.Maximum & " Rows" & vbNewLine
                                    If ProgressBar1.Maximum > 0 Then
                                        lblStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar1.Maximum), 2) & "%"
                                    Else
                                        lblStatus.Text += 0 & "%"
                                    End If
                                    Application.DoEvents()
                                End If
                            Catch ex As IndexOutOfRangeException
                                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "MobileNo : " & strFld(1) & ex.Message)
                                ErrCount += 1
                                UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                                UpdateGWComplete(fleName, "99")   'หยุดการ Import
                                FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                                Exit While
                            End Try
                        End If
                        i += 1
                    End While
                    Stream.Close()
                    Stream = Nothing
                    If ErrCount >= (ProgressBar1.Maximum / 100) Then
                        FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                    End If


                    'TB_CUSTOMER_BACKUP
                    Dim sqlB As String = ""
                    sqlB += " INSERT INTO [TB_CUSTOMER_BACKUP]([id],[create_by],[create_date],[update_by],[update_date]"
                    sqlB += " ,[mobile_no],[title_name],[f_name],[l_name],[email]"
                    sqlB += " ,[prefer_lang],[segment_level],[birth_date],[mobile_status],[category],[account_balance]"
                    sqlB += " ,[contact_class],[service_year],[chum_score],[campaign_code],[campaign_name],[network_type]"
                    sqlB += " ,[campaign_name_en],[campaign_desc],[campaign_desc_th2],[campaign_desc_en2],[backup_date])"

                    sqlB += " SELECT [id],[create_by],[create_date],[update_by],[update_date]"
                    sqlB += " ,[mobile_no],[title_name],[f_name],[l_name],[email]"
                    sqlB += " ,[prefer_lang],[segment_level],[birth_date],[mobile_status],[category],[account_balance]"
                    sqlB += " ,[contact_class],[service_year],[chum_score],[campaign_code],[campaign_name],[network_type]"
                    sqlB += " ,[campaign_name_en],[campaign_desc],[campaign_desc_th2],[campaign_desc_en2], GETDATE()"
                    sqlB += " FROM [TB_CUSTOMER]"
                    trans = New TransactionDB(False)
                    PrintLog("Backup Customer Profile to TB_CUSTOMER_BACKUP")
                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                    If SqlDB.ExecuteNonQuery(sqlB, trans.Trans) > 0 Then
                        trans.CommitTransaction()

                        'Move Data จาก TB_CUSTOMER_TEMP ไปยัง TB_CUSTOMER
                        trans = New TransactionDB(False)
                        PrintLog("Move Data From TEMP TO Production")
                        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                        If SqlDB.ExecuteNonQuery("truncate table TB_CUSTOMER", trans.Trans) = True Then
                            trans.CommitTransaction()

                            Dim sqlM As String = "INSERT INTO [TB_CUSTOMER] ([create_by],[create_date],[update_by],[update_date],[mobile_no],[title_name],[f_name],[l_name],[email],[prefer_lang],[segment_level],[birth_date],[mobile_status],[category],[account_balance],[contact_class],[service_year],[chum_score],[campaign_code],[campaign_name],[network_type],[campaign_name_en],[campaign_desc]) "
                            sqlM += " SELECT [create_by],[create_date],[update_by],[update_date],[mobile_no],[title_name],[f_name],[l_name],[email],[prefer_lang],[segment_level],[birth_date],[mobile_status],[category],[account_balance],[contact_class],[service_year],[chum_score],[campaign_code],[campaign_name],[network_type],[campaign_name_en],[campaign_desc] FROM [TB_CUSTOMER_TEMP]"
                            trans = New TransactionDB(False)
                            trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                            If SqlDB.ExecuteNonQuery(sqlM, trans.Trans) > 0 Then
                                trans.CommitTransaction()
                                If ClearCustomerTemp() = False Then
                                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error truncate table TB_CUSTOMER_TEMP" & SqlDB.ErrorMessage)
                                    PrintLog("Error truncate table TB_CUSTOMER_TEMP" & SqlDB.ErrorMessage)
                                End If
                            Else
                                trans.RollbackTransaction()
                                FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error Move Data : " & sqlM & " " & SqlDB.ErrorMessage)
                                PrintLog("Error Move Data : " & sqlM & " " & SqlDB.ErrorMessage)
                            End If
                        Else
                            trans.RollbackTransaction()
                            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error truncate table TB_CUSTOMER " & SqlDB.ErrorMessage)
                            PrintLog("Error truncate table TB_CUSTOMER " & SqlDB.ErrorMessage)
                        End If
                        PrintLog("Finish ReplaceCustInfo File Name : " & fleName)
                        FunctionENG.SaveTransLog("frmDailyImportCustomer.ReplaceCustInfo", "Finish ReplaceCustInfo File Name : " & fleName)
                    Else
                        trans.RollbackTransaction()
                        FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error BACKUP to table TB_CUSTOMER_BACKUP " & SqlDB.ErrorMessage)
                        PrintLog("Error BACKUP to table TB_CUSTOMER_BACKUP " & SqlDB.ErrorMessage)
                    End If
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        End Try

        Return ret
    End Function

    Private Function UpdateTotalRowCount(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        Dim ret As Boolean = False
        Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
        While Stream.Peek <> -1
            Dim str As String = Stream.ReadLine
            If str.Trim <> "" Then
                Dim strFld As String() = Split(str, "|")
                Try
                    If strFld(0) = "01" Then
                        Dim trans As New TransactionDB(False)
                        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                        Dim sqlEnd As String = "update TB_LOG_CUSTOMER_DAILY "
                        sqlEnd += " set row_count = '" & strFld(2) & "'"
                        sqlEnd += " where id = '" & ImpLogID & "'"
                        If SqlDB.ExecuteNonQuery(sqlEnd, trans.Trans) > 0 Then
                            trans.CommitTransaction()
                            ret = True
                        Else
                            trans.RollbackTransaction()
                            ret = False
                        End If
                        Exit While
                    End If
                Catch ex As IndexOutOfRangeException
                    ret = False
                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "MobileNo : " & strFld(1) & ex.Message)
                    UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                    UpdateGWComplete(fleName, "99")   'หยุดการ Import
                    FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                    Exit While
                End Try
            End If
        End While
        Stream.Close()
        Stream = Nothing

        Return ret
    End Function

    Private Function ImportProcessTextFileToBulkInsert(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean

        Dim ret As Boolean = False

        'Execute SSIS ImportProcessTextFileToBulkInsert
        PrintLog("Execute SSIS TextFileToBulkInsertForReplace")
        SetImportLogStatus(ImpLogID, "Execute SSIS TextFileToBulkInsertForReplace")
        Dim app As New Microsoft.SqlServer.Dts.Runtime.Application
        Dim pkg As Dts.Runtime.Package = app.LoadFromSqlServer("TextFileToBulkInsertForReplace", SqlDB.HQMainServer, SqlDB.HQMainDbUserID, SqlDB.HQMainDbPwd, Nothing)
        Dim pkgResults As Dts.Runtime.DTSExecResult = pkg.Execute()
        If pkgResults.ToString = "Success" Then
            ret = True
            PrintLog("Finish Execute SSIS TextFileToBulkInsertForReplace : " & fleName)
        Else
            ret = False
            Dim _err As String = ""
            For Each Err As Dts.Runtime.DtsError In pkg.Errors
                If _err = "" Then
                    _err = Err.ErrorCode & " :: " & Err.Description
                Else
                    _err += vbNewLine & Err.ErrorCode & " :: " & Err.Description
                End If
            Next
            PrintLog("Error Execute SSIS TextFileToBulkInsertForReplace : " & fleName & vbNewLine & _err)
            FunctionENG.SaveErrorLog(_ClassName & ".ImportCustInfoToTemp", "Error Execute SSIS TextFileToBulkInsertForReplace : " & fleName & vbNewLine & _err)
        End If

        Return ret
    End Function

    Private Function ReplaceCustInfo(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            If File.Exists(fleName) Then
                PrintLog("Start ReplaceCustInfo File Name : " & fleName)
                FunctionENG.SaveTransLog("frmDailyImportCustomer.ReplaceCustInfo", "Start ReplaceCustInfo File Name : " & fleName)

                If CreateBulkCustomerTable() = True Then
                    ret = UpdateTotalRowCount(fleName, ImpLogID)
                    If ret = True Then
                        ret = ImportProcessTextFileToBulkInsert(fleName, ImpLogID)
                        If ret = True Then
                            Dim trans As New TransactionDB(False)
                            trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)

                            'TB_CUSTOMER_BACKUP
                            Dim sqlB As String = ""
                            sqlB += " select top 1 id from tb_customer "
                            Dim dtB As DataTable = SqlDB.ExecuteTable(sqlB, trans.Trans)
                            trans.CommitTransaction()
                            If dtB.Rows.Count > 0 Then
                                'ถ้ามีข้อมูลใน TB_CUSTOMER ก็ทำการ BACKUP ก่อน
                                PrintLog("Backup Data to TB_CUSTOMER_BACKUP")
                                SetImportLogStatus(ImpLogID, "Backup Customer Profile")

                                'Execute SSIS CustomerToBackup
                                PrintLog("Execute SSIS CustomerToBackup : " & fleName)
                                Dim app As New Microsoft.SqlServer.Dts.Runtime.Application
                                Dim pkg As Dts.Runtime.Package = app.LoadFromSqlServer("CustomerToBackup", SqlDB.HQMainServer, SqlDB.HQMainDbUserID, SqlDB.HQMainDbPwd, Nothing)
                                Dim pkgResults As Dts.Runtime.DTSExecResult = pkg.Execute()

                                If pkgResults.ToString = "Success" Then
                                    ret = True
                                    PrintLog("Finish Execute SSIS CustomerToBackup : " & fleName)
                                Else
                                    ret = False
                                    Dim _err As String = ""
                                    For Each Err As Dts.Runtime.DtsError In pkg.Errors
                                        If _err = "" Then
                                            _err = Err.ErrorCode & " :: " & Err.Description
                                        Else
                                            _err += vbNewLine & Err.ErrorCode & " :: " & Err.Description
                                        End If
                                    Next
                                    PrintLog("Error Execute SSIS CustomerToBackup : " & fleName & vbNewLine & _err)
                                    FunctionENG.SaveErrorLog(_ClassName & ".ImportCustInfoToTemp", "Error Execute SSIS CustomerToBackup : " & fleName & vbNewLine & _err)
                                End If

                                dtB.Dispose()
                                dtB = Nothing
                            End If

                            'Move Data จาก TB_BULK_INSERT_CUSTOMER ไปยัง TB_CUSTOMER
                            trans = New TransactionDB(False)
                            trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                            Dim sqlM As String = ""
                            sqlM = "select top 1 mobile_no from TB_BULK_INSERT_CUSTOMER"
                            Dim dtM As DataTable = SqlDB.ExecuteTable(sqlM, trans.Trans)
                            trans.CommitTransaction()
                            If dtM.Rows.Count > 0 Then

                                PrintLog("Truncate TB_CUSTOMER")
                                trans = New TransactionDB(False)
                                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                If SqlDB.ExecuteNonQuery("truncate table TB_CUSTOMER", trans.Trans) = True Then
                                    trans.CommitTransaction()

                                    PrintLog("Move Data From TB_BULK_INSERT_CUSTOMER TO TB_CUSTOMER")
                                    SetImportLogStatus(ImpLogID, "Move Data From TB_BULK_INSERT_CUSTOMER TO TB_CUSTOMER")

                                    'Execute SSIS BulkToCustomer
                                    PrintLog("Execute SSIS BulkToCustomer : " & fleName)
                                    Dim app As New Microsoft.SqlServer.Dts.Runtime.Application
                                    Dim pkg As Dts.Runtime.Package = app.LoadFromSqlServer("BulkToCustomer", SqlDB.HQMainServer, SqlDB.HQMainDbUserID, SqlDB.HQMainDbPwd, Nothing)
                                    Dim pkgResults As Dts.Runtime.DTSExecResult = pkg.Execute()

                                    If pkgResults.ToString = "Success" Then
                                        Dim sqlEnd As String = "update TB_LOG_CUSTOMER_DAILY "
                                        sqlEnd += " set row_count_insert = (select count(id) from tb_customer)"
                                        sqlEnd += " where id = '" & ImpLogID & "'"

                                        trans = New TransactionDB(False)
                                        trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                        If SqlDB.ExecuteNonQuery(sqlEnd, trans.Trans) > 0 Then
                                            trans.CommitTransaction()
                                            PrintLog("Finish Move Data From TEMP TO Production")
                                        Else
                                            trans.RollbackTransaction()
                                        End If
                                    Else
                                        ret = False
                                        Dim _err As String = ""
                                        For Each Err As Dts.Runtime.DtsError In pkg.Errors
                                            If _err = "" Then
                                                _err = Err.ErrorCode & " :: " & Err.Description
                                            Else
                                                _err += vbNewLine & Err.ErrorCode & " :: " & Err.Description
                                            End If
                                        Next
                                        PrintLog("Error Execute SSIS BulkToCustomer : " & fleName & vbNewLine & _err)
                                        FunctionENG.SaveErrorLog(_ClassName & ".ImportCustInfoToTemp", "Error Execute SSIS BulkToCustomer : " & fleName & vbNewLine & _err)
                                    End If
                                Else
                                    ret = False
                                    trans.RollbackTransaction()
                                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "Error truncate table TB_CUSTOMER " & SqlDB.ErrorMessage)
                                    PrintLog("Error truncate table TB_CUSTOMER " & SqlDB.ErrorMessage)
                                End If
                                dtM.Dispose()
                                dtM = Nothing
                            End If
                            PrintLog("Finish ReplaceCustInfo File Name : " & fleName)
                            FunctionENG.SaveTransLog("frmDailyImportCustomer.ReplaceCustInfo", "Finish ReplaceCustInfo File Name : " & fleName)
                        End If
                    End If
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        End Try

        Return ret
    End Function

    Private Function CopyFile(ByVal FileName As String, ByVal FileSize As Long, ByVal DestPath As String) As Boolean
        Dim ret As Boolean = False
        Try
            PrintLog("Copy File " & FileName)
            'TEST Server
            'Dim copyCmd As String = "D:\APPS\putty_0.61\pscp.exe"  
            'Dim args As String = "-2 -q -C -i D:\APPS\putty_0.61\rsa-key-2048-20120214.ppk qisdb@10.13.181.100:data.qiscustinfo.local/" & FileName & " " & DestPath & FileName


            ' PRODUCTION
            Dim copyCmd As String = "D:\Putty\PSCP.EXE "
            Dim args As String = "-q -i D:\Putty\id_rsa.ppk qisdb@10.13.174.95:data.qiscustinfo.local/" & FileName & " " & DestPath & FileName
            Dim proc As New ProcessStartInfo(copyCmd, args)
            Dim pr As Process = Process.Start(proc)

            pr.Close()
            System.Threading.Thread.Sleep(10000)
            Do
                Try
                    Dim Stream As New StreamReader(DestPath & FileName, System.Text.UnicodeEncoding.Default)
                    Stream.Close()
                    Stream = Nothing
                    ret = True
                    Exit Do
                Catch ex As Exception
                    'SqlDB.CreateLogFile("Inner Loop" & DateTime.Now.ToString("HH:mm:ss") & " " & ex.Message)
                End Try
            Loop While File.Exists(DestPath & FileName)
            PrintLog("Copy File Finish")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            _err = ex.Message
            FunctionENG.SendErrorToSMS("File Name : " & FileName & ": " & ex.Message)
            ret = False
        End Try

        Return ret
    End Function

    Private Function ImportCampaign(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        Dim ret As Boolean = False

        Try
            'Start Import Campaign To Temp
            If ImportCampaignToTemp(fleName, ImpLogID) = True Then
                PrintLog("Start Update Campaign")
                FunctionENG.SaveTransLog(_ClassName & ".ImportCampaign", "Start Update Campaign")
                
                Dim trans As New TransactionDB(False)
                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                If trans.Trans IsNot Nothing Then
                    Dim lnqT As New TbCustomerTempCenLinqDB
                    PrintLog("Start Query Data")
                    SetImportLogStatus(ImpLogID, "Query Data")
                    Dim sql As String = "select MOBILE_NO, CAMPAIGN_CODE, CAMPAIGN_NAME,CAMPAIGN_DESC_TH2,CAMPAIGN_NAME_EN,CAMPAIGN_DESC,CAMPAIGN_DESC_EN2 "
                    sql += " from TB_CUSTOMER_TEMP "
                    sql += " where mobile_no in (select mobile_no from tb_customer)"

                    Dim dt As DataTable = lnqT.GetListBySql(sql, trans.Trans) 'lnqT.GetDataList("mobile_no in (select mobile_no from tb_customer)", "", trans.Trans)
                    trans.CommitTransaction()
                    If dt.Rows.Count > 0 Then
                        ProgressBar2.Value = 0
                        ProgressBar2.Maximum = dt.Rows.Count
                        PrintLog("Finish Query Data (" & dt.Rows.Count & " Rows)")
                        If SetImportLogStatus(ImpLogID, "Updating Data") = True Then
                            Dim i As Long = 0
                            Dim ErrCount As Long = 0
                            Dim CompleteCount As Long = 0
                            For Each drT As DataRow In dt.Rows
                                Try
                                    Dim uSql As String = ""
                                    Dim lnq As New TbCustomerCenLinqDB
                                    lnq.MOBILE_NO = drT("MOBILE_NO")
                                    lnq.CAMPAIGN_CODE = drT("CAMPAIGN_CODE").Trim().Replace("'", "''")
                                    lnq.CAMPAIGN_NAME = drT("CAMPAIGN_NAME").Trim().Replace("'", "''")
                                    lnq.CAMPAIGN_DESC_TH2 = drT("CAMPAIGN_DESC_TH2").Trim().Replace("'", "''")
                                    lnq.CAMPAIGN_NAME_EN = drT("CAMPAIGN_NAME_EN").Trim().Replace("'", "''")
                                    lnq.CAMPAIGN_DESC = drT("CAMPAIGN_DESC").Trim().Replace("'", "''")
                                    lnq.CAMPAIGN_DESC_EN2 = drT("CAMPAIGN_DESC_EN2").Trim().Replace("'", "''")

                                    uSql += "UPDATE TB_CUSTOMER SET "
                                    uSql += " UPDATE_BY = 'UpdateCampaign', UPDATE_DATE = getdate(), "
                                    uSql += " CAMPAIGN_CODE = '" & lnq.CAMPAIGN_CODE & "', CAMPAIGN_NAME = '" & lnq.CAMPAIGN_NAME & "', "
                                    uSql += " CAMPAIGN_NAME_EN = '" & lnq.CAMPAIGN_NAME_EN & "', CAMPAIGN_DESC = '" & lnq.CAMPAIGN_DESC & "', "
                                    uSql += " CAMPAIGN_DESC_TH2 = '" & lnq.CAMPAIGN_DESC_TH2 & "', CAMPAIGN_DESC_EN2 = '" & lnq.CAMPAIGN_DESC_EN2 & "'"
                                    uSql += " Where MOBILE_NO = '" & lnq.MOBILE_NO & "' "

                                    trans = New TransactionDB(False)
                                    trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                    ret = lnq.UpdateBySql(uSql, trans.Trans)
                                    If ret = True Then
                                        PrintLog("Updating Data")
                                        trans.CommitTransaction()
                                        CompleteCount += 1
                                    Else
                                        trans.RollbackTransaction()
                                        ErrCount += 1
                                        FunctionENG.SaveErrorLog(_ClassName & ".ImportCampaignToTemp", "Mobile No :" & lnq.MOBILE_NO & " " & uSql)
                                        PrintLog("Mobile No :" & lnq.MOBILE_NO & " " & uSql)
                                    End If
                                    If ProgressBar2.Value + 1 <= ProgressBar2.Maximum Then
                                        ProgressBar2.Value = i + 1
                                        lblUpdateStatus.Text = "Update Status Row " & (i + 1) & " of " & ProgressBar2.Maximum & " Rows" & vbNewLine
                                        If ProgressBar2.Maximum > 0 Then
                                            lblUpdateStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar2.Maximum), 2) & "%"
                                        Else
                                            lblUpdateStatus.Text += 0 & "%"
                                        End If
                                        Application.DoEvents()
                                    Else
                                        PrintLog("ProgressBar2.Value + 1 <= ProgressBar2.Maximum")
                                    End If
                                Catch ex As IndexOutOfRangeException
                                    FunctionENG.SaveErrorLog(_ClassName & ".ImportCampaign", "MobileNo : " & drT("MOBILE_NO") & ex.Message)
                                    ErrCount += 1
                                    PrintLog("Import Error Stop Process " & ex.Message)
                                    UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                                    UpdateGWComplete(fleName, "99")   'หยุดการ Import
                                    FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                                    Exit For
                                End Try
                                i += 1
                            Next

                            'Update จำนวน Record ที่ Update
                            If CompleteCount > 0 Then
                                Dim cSql As String = "update tb_log_customer_daily "
                                cSql += " set row_count_update = " & CompleteCount
                                cSql += " where id= " & ImpLogID
                                trans = New TransactionDB(False)
                                trans.CreateTransaction("Data Source=" & SqlDB.HQMainServer & ";Initial Catalog=" & SqlDB.HQMainDbName & ";User ID=" & SqlDB.HQMainDbUserID & ";Password=" & SqlDB.HQMainDbPwd)
                                If SqlDB.ExecuteNonQuery(cSql, trans.Trans) > 0 Then
                                    trans.CommitTransaction()
                                Else
                                    trans.RollbackTransaction()
                                    PrintLog("Error Update Total Row : " & cSql & " " & SqlDB.ErrorMessage)
                                    FunctionENG.SaveErrorLog("frmDailyImportCustomer.ImportCampaign", "Error Update Total Row : " & cSql & " " & SqlDB.ErrorMessage)
                                End If
                            End If

                            If ErrCount >= (ProgressBar2.Maximum / 100) Then
                                FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                                PrintLog("Import Error " & "File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                            End If
                        End If
                        dt.Dispose()
                        dt = Nothing
                    End If
                    PrintLog("Finish Update Campaign")
                    FunctionENG.SaveTransLog(_ClassName & ".ImportCampaign", "Finish Update Campaign")
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            PrintLog(_err)
            FunctionENG.SaveErrorLog(_ClassName & ".ImportCampaign", "OutOfMemoryException File Name : " & fleName & " " & ex.Message)
        Catch ex As Exception
            _err = ex.Message
            PrintLog(_err)
            FunctionENG.SaveErrorLog(_ClassName & ".ImportCampaign", "Exception File Name : " & fleName & " " & ex.Message)
        End Try

        Return ret
    End Function

    Dim _oldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If LogDesc <> _oldLog Then
            txtLog.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & " " & LogDesc & vbNewLine & txtLog.Text
            _oldLog = LogDesc
        End If
        Application.DoEvents()
    End Sub

    Private Sub btnTestCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCopy.Click
        ''QIS_CUSINFO_20120312.dat
        'CopyFile("QIS_CUSINFO_20120312.dat", 40844027, FunctionENG.GetQisDBConfig("CustInfoDailyImportPath"))
        'MessageBox.Show("Copy Complete")

        'ImportCampaign("K:\CustInfoTemp\QIS_CAMP_POST_20120324.dat", 25)


        'ImportCustInfo("D:\My Documents\AIS-Q\Source\CustInfo\QIS_CUSINFO_20120312.dat")

        'InsertCustInfo("K:\CustInfoTemp\QIS_CUSINFO_20120325.dat", 26)

        'CopyToTempTextFile("D:\My Documents\AIS-Q\Source\CustInfo\QIS_CUSINFO_REPLACE_20120307.dat", 22)
        'ReplaceCustInfo(FunctionENG.GetQisDBConfig("CustInfoDailyImportPath") & "QIS_CUSINFO_REPLACE_20120601.dat", 119)

        'If File.Exists(FunctionENG.GetQisDBConfig("CustInfoDailyImportPath") & "QIS_CUSINFO_PROCESS.dat") = True Then
        '    File.Delete(FunctionENG.GetQisDBConfig("CustInfoDailyImportPath") & "QIS_CUSINFO_PROCESS.dat")
        'End If

        'File.Copy(FunctionENG.GetQisDBConfig("CustInfoDailyImportPath") & "QIS_CUSINFO_REPLACE_20120601.dat", FunctionENG.GetQisDBConfig("CustInfoDailyImportPath") & "QIS_CUSINFO_PROCESS.dat")

        CopyFile("QIS_CUSINFO_20120830.dat", 2941374454, FunctionENG.GetQisDBConfig("CustInfoDailyImportPath"))
        'MessageBox.Show("Copy Complete")
    End Sub

    Private Sub frmDailyImportCustomer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Function CopyToTempTextFile(ByVal fleName As String, ByVal ImpLogID As Long) As Boolean
        Dim ret As Boolean = False
        Try
            If File.Exists(fleName) Then
                Dim i As Long = 0
                Dim ErrCount As Long = 0
                Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
                While Stream.Peek <> -1
                    Dim str As String = Stream.ReadLine
                    If str.Trim <> "" Then
                        Dim strFld As String() = Split(str, "|")
                        Try
                            If strFld(0) = "01" Then
                                ProgressBar1.Maximum = strFld(2)
                                Continue While
                            End If

                            Dim FILE_NAME As String = "D:\TempTextFile.txt"
                            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                            objWriter.WriteLine(str)
                            objWriter.Close()

                            If ProgressBar1.Value + 1 <= ProgressBar1.Maximum Then
                                ProgressBar1.Value = i + 1
                                lblStatus.Text = "Row " & (i + 1) & " of " & ProgressBar1.Maximum & " Rows" & vbNewLine
                                If ProgressBar1.Maximum > 0 Then
                                    lblStatus.Text += Math.Round((((i + 1) * 100) / ProgressBar1.Maximum), 2) & "%"
                                Else
                                    lblStatus.Text += 0 & "%"
                                End If
                                Application.DoEvents()
                            End If
                        Catch ex As IndexOutOfRangeException
                            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", "MobileNo : " & strFld(1) & ex.Message)
                            ErrCount += 1
                            UpdateGWComplete(fleName, "9")  'ไฟล์ผิด Format
                            UpdateGWComplete(fleName, "99")   'หยุดการ Import
                            FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data File Format Invalid")
                            Exit While
                        End Try
                    End If
                    i += 1
                End While
                Stream.Close()
                Stream = Nothing
                If ErrCount >= (ProgressBar1.Maximum / 100) Then
                    FunctionENG.SendErrorToSMS("File Name : " & fleName & ": " & "Error Import Data  มากกว่า 1% ของข้อมูล")
                End If
            End If
        Catch ex As OutOfMemoryException
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        Catch ex As Exception
            _err = ex.Message
            FunctionENG.SaveErrorLog("frmDailyImportCustomer.ReplaceCustInfo", ex.Message)
        End Try

        Return ret
    End Function


    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub tmTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTime.Tick
        Label3.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
    End Sub

End Class