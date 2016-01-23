Imports System.IO
Imports System.Text
Imports exc = Microsoft.Office.Interop.Excel

Public Class frmCustomer
    Dim err As String = ""
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Timer1.Enabled = True
        lblTime.Text = "00:00"
        Application.DoEvents()
        Dim dt As DataTable = ReadToDT()
        If dt.Rows.Count > 0 Then
            DataGridView1.DataSource = dt
        End If
        Timer1.Enabled = False
    End Sub

    Public Function ByteToString(ByVal Buffer() As Byte, Optional ByVal Encoding As Integer = EncodeType._DEFAULT) As String
        System.Text.Encoding.Default.GetString(Buffer, 0, Buffer.Length)
        Select Case Encoding
            Case EncodeType._ASCII
                Return System.Text.Encoding.ASCII.GetString(Buffer, 0, Buffer.Length)
            Case EncodeType._UNICODE
                Return System.Text.Encoding.Unicode.GetString(Buffer, 0, Buffer.Length)
            Case EncodeType._UTF32
                Return System.Text.Encoding.UTF32.GetString(Buffer, 0, Buffer.Length)
            Case EncodeType._UTF7
                Return System.Text.Encoding.UTF7.GetString(Buffer, 0, Buffer.Length)
            Case EncodeType._UTF8
                Return System.Text.Encoding.UTF8.GetString(Buffer, 0, Buffer.Length)
            Case Else
                Return System.Text.Encoding.Default.GetString(Buffer, 0, Buffer.Length)
        End Select
    End Function
    Public Function StreamToByte(ByVal Stream As System.IO.Stream) As Byte() ' Convert Specify Stream To Byte
        Dim Result() As Byte
        ReDim Result(Stream.Length - 1)
        Stream.Read(Result, 0, Stream.Length)
        StreamToByte = Result.Clone
    End Function

    Public Function ByteToStream(ByVal Buffer() As Byte) As System.IO.MemoryStream ' Convert Byte To Stream
        ByteToStream = New System.IO.MemoryStream(Buffer)
    End Function

    Private Function ReadTextFile(ByVal fleName As String) As String()
        Dim strRec As New StringBuilder
        'strRec.Append(System.IO.File.ReadAllText(fleName))
        If File.Exists(fleName) Then
            Dim Stream As New StreamReader(fleName, System.Text.UnicodeEncoding.Default)
            While Stream.Peek <> -1
                strRec.Append(Stream.ReadToEnd)
            End While
            Stream.Close()
            Stream = Nothing




            'Dim Stream As New FileStream(fleName, FileMode.Open, FileAccess.Read)
            'strRec = Split(ByteToString(StreamToByte(Stream), EncodeType._DEFAULT), Chr(10))

        End If
        Return Split(strRec.ToString, Chr(10))
    End Function

    Private Function ReadToDT() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("first_name")
        dt.Columns.Add("last_name")
        dt.Columns.Add("title_name")
        dt.Columns.Add("email")
        dt.Columns.Add("mobile_no")
        dt.Columns.Add("mobile_status")
        dt.Columns.Add("birth_date")
        dt.Columns.Add("segment_level")
        dt.Columns.Add("category")
        dt.Columns.Add("account_balance")
        dt.Columns.Add("contact_class")
        dt.Columns.Add("service_year")
        dt.Columns.Add("chum_score")
        dt.Columns.Add("campaign_code")
        dt.Columns.Add("campaign_name")
        dt.Columns.Add("prefer_lang")
        dt.Columns.Add("network_type")

        Dim err As String = ""
        If File.Exists(txtTextFileName.Text) Then
            Dim strRec() As String = ReadTextFile(txtTextFileName.Text) 'Split(ByteToString(StreamToByte(Stream), EncodeType._DEFAULT), Chr(10))
            Dim i As Integer = 0
            If strRec.Length > 1 Then
                ProgressBar1.Maximum = strRec.Length - 1    'แถวแรกจะเป็นชื่อคอลัมน์
                For Each str As String In strRec
                    If i > 0 Then
                        If str.Trim <> "" Then
                            Dim strFld As String() = Split(str, "|")
                            Dim dr As DataRow = dt.NewRow
                            dr("first_name") = strFld(0)
                            dr("last_name") = strFld(1)
                            dr("title_name") = strFld(2)
                            dr("email") = strFld(3)
                            dr("mobile_no") = strFld(4)
                            dr("mobile_status") = strFld(5)
                            dr("birth_date") = GetBirthDate(strFld(6))
                            dr("segment_level") = strFld(7)
                            dr("category") = strFld(8)
                            dr("account_balance") = strFld(9)
                            dr("contact_class") = strFld(10)
                            dr("service_year") = strFld(11)
                            dr("chum_score") = strFld(12)
                            dr("campaign_code") = strFld(13)
                            dr("campaign_name") = strFld(14)
                            dr("prefer_lang") = strFld(15)
                            dr("network_type") = strFld(16)
                            dt.Rows.Add(dr)
                            ProgressBar1.Value = i + 1
                            lblStatus.Text = "Row " & i & " of " & (strRec.Length - 2) & " Rows" & vbNewLine
                            lblStatus.Text += Math.Round(((i * 100) / (strRec.Length - 2)), 2) & "%"
                            Application.DoEvents()
                        End If
                    End If
                    i += 1
                Next
            End If
        End If
        If err <> "" Then
            MessageBox.Show(err)
        End If
        Return dt
    End Function

    Private Function SaveToTable(ByVal strRec() As String) As Boolean
        
        Dim ret As Boolean = False
        Dim i As Integer = 0

        Dim iTrans As New Utilities.TransactionDB
        Utilities.SqlDB.ExecuteNonQuery("delete from tb_customer", iTrans.Trans)
        iTrans.CommitTransaction()

        ProgressBar1.Maximum = strRec.Length - 1    'แถวแรกจะเป็นชื่อคอลัมน์
        For Each str As String In strRec
            If i > 0 Then
                If str.Trim <> "" Then
                    Dim strFld As String() = Split(str, "|")
                    Dim lnq As New Utilities.TbCustomerCenLinqDB
                    lnq.F_NAME = strFld(0)
                    lnq.L_NAME = strFld(1)
                    lnq.TITLE_NAME = strFld(2)
                    lnq.EMAIL = strFld(3)
                    lnq.MOBILE_NO = strFld(4)
                    lnq.MOBILE_STATUS = strFld(5)
                    lnq.BIRTH_DATE = GetBirthDate(strFld(6))
                    lnq.SEGMENT_LEVEL = strFld(7)
                    lnq.CATEGORY = strFld(8)
                    lnq.ACCOUNT_BALANCE = strFld(9)
                    lnq.CONTACT_CLASS = strFld(10)
                    lnq.SERVICE_YEAR = strFld(11)
                    lnq.CHUM_SCORE = strFld(12)
                    lnq.CAMPAIGN_CODE = strFld(13)
                    lnq.CAMPAIGN_NAME = strFld(14)
                    lnq.PREFER_LANG = strFld(15)
                    lnq.NETWORK_TYPE = strFld(16)

                    Dim trans As New Utilities.TransactionDB
                    ret = lnq.InsertData("1", trans.Trans)
                    If ret = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                        err += lnq.ErrorMessage
                    End If
                    ProgressBar1.Value = i + 1
                    lblStatus.Text = "Row " & i & " of " & (strRec.Length - 2) & " Rows" & vbNewLine
                    lblStatus.Text += Math.Round(((i * 100) / (strRec.Length - 2)), 2) & "%"
                    Application.DoEvents()
                End If
            End If
            i += 1
        Next
        Return ret
    End Function

    Private Function GetBirthDate(ByVal bDate As String) As Date
        Dim ret As New Date(1, 1, 1)
        If bDate.Trim <> "" Then
            Dim bYear As String = (Val(bDate.Substring(6, 4)) + 543)
            Dim bMonth As String = bDate.Substring(3, 2)
            Dim bDay As String = bDate.Substring(0, 2)
            ret = DateSerial(bYear, bMonth, bDay)
        End If
        Return ret
    End Function

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        lblTime.Text = "00:00"
        Timer1.Enabled = True
        Application.DoEvents()
        Dim ret As Boolean = False
        'SaveToTable(ReadTextFile(txtTextFileName.Text))
        SaveExcelToTable(txtTextFileName.Text)
        If err.Trim <> "" Then
            MessageBox.Show(err)
        End If
        Timer1.Enabled = False
    End Sub

    Private Function UpdateBirthDate(ByVal ExcelFile As String) As Boolean
        Dim ret As Boolean = False
        Dim dt As DataTable = GetExcelData(0, txtTextFileName.Text)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                If dr("BDATE").ToString.Trim <> "" Then
                    Dim trans As New Utilities.TransactionDB
                    Dim lnq As New Utilities.TbCustomerCenLinqDB
                    If dr("MOBILE_NO").ToString.Trim <> "" Then
                        lnq.GetDataByMobileNo(dr("MOBILE_NO"), trans.Trans)
                        If lnq.HaveData = True Then
                            lnq.BIRTH_DATE = dr("BDATE").ToString
                            ret = lnq.UpdateByPK("UPDATE", trans.Trans)
                            If ret = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                                err += lnq.ErrorMessage
                            End If
                        End If
                    End If
                End If

            Next
        End If
    End Function

    Private Function SaveExcelToTable(ByVal ExcelFile As String) As Boolean
        Dim ret As Boolean = True
        Dim dt As DataTable = GetExcelData(0, txtTextFileName.Text)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                If dr("MOBILE_NO").ToString.Trim() <> "" Then
                    Dim lnq As New Utilities.TbCustomerCenLinqDB
                    lnq.F_NAME = dr("FST_NAME").ToString
                    lnq.L_NAME = dr("LAST_NAME").ToString
                    lnq.TITLE_NAME = dr("X_TITLE").ToString
                    lnq.EMAIL = dr("EMAIL_ADDR").ToString
                    lnq.MOBILE_NO = dr("MOBILE_NO").ToString
                    lnq.MOBILE_STATUS = dr("STATUS").ToString
                    lnq.BIRTH_DATE = dr("BDATE").ToString
                    lnq.SEGMENT_LEVEL = dr("SEGMENT").ToString
                    lnq.CATEGORY = dr("CATEGORY").ToString
                    lnq.ACCOUNT_BALANCE = dr("Acc_Balance").ToString
                    lnq.CONTACT_CLASS = dr("CONTACT_CLASS").ToString
                    lnq.SERVICE_YEAR = dr("Service_Year").ToString
                    lnq.CHUM_SCORE = dr("CHURN_SCORE").ToString
                    lnq.CAMPAIGN_CODE = dr("CampName_TH").ToString
                    lnq.CAMPAIGN_NAME = dr("CampDesc_TH").ToString
                    lnq.PREFER_LANG = dr("PRE_LANG").ToString
                    lnq.NETWORK_TYPE = dr("NETWORK_TPYE").ToString
                    lnq.CAMPAIGN_NAME_EN = dr("CampName_Eng").ToString
                    lnq.CAMPAIGN_DESC = dr("CampDesc_Eng").ToString
                    'lnq.CAMPAIGN_DESC_TH2 = dr("CampDesc_TH")
                    'lnq.CAMPAIGN_DESC_EN2 = dr("CampDesc_Eng")

                    Dim trans As New Utilities.TransactionDB
                    ret = lnq.InsertData("INITIAL", trans.Trans)
                    If ret = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                        err += lnq.ErrorMessage
                    End If
                    ProgressBar1.Value = i + 1
                    lblStatus.Text = "Row " & (i + 1) & " of " & dt.Rows.Count & " Rows" & vbNewLine
                    lblStatus.Text += Math.Round((((i + 1) * 100) / dt.Rows.Count), 2) & "%"
                    Application.DoEvents()
                End If
            Next

        End If
    End Function

    Private Function GetExcelData(ByVal Sh As Integer, ByVal ExcelFile As String) As DataTable 'Sheet แรก =0
        Dim f As New FileInfo(ExcelFile)
        Dim stream As FileStream = File.Open(ExcelFile, FileMode.Open, FileAccess.Read)
        Dim excelReader As Excel.IExcelDataReader = Nothing
        excelReader = Excel.ExcelReaderFactory.CreateBinaryReader(stream) 'Excel 2003/97 Format
        'excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream) 'Excel 2007 Format
        excelReader.IsFirstRowAsColumnNames = True
        Dim result As DataSet = excelReader.AsDataSet()
        Return result.Tables(Sh)
    End Function

    'public static DataTable getExcelData(int sh)  //ลำดับของ Sheet (Sheet แรก=0)
    '    {
    '        FileInfo f = new FileInfo(Constant.ExcelFile.excelFile);
    '        FileStream stream = File.Open(Constant.ExcelFile.excelFile, FileMode.Open, FileAccess.Read);
    '        Excel.IExcelDataReader excelReader = null;
    '        //excelReader = Excel.ExcelReaderFactory.CreateBinaryReader(stream); //Excel 2003/97 Format
    '        excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream); //Excel 2007
    '        excelReader.IsFirstRowAsColumnNames = true;//First Row As Column Names 

    'DataSet result = excelReader.AsDataSet();

    '        return result.Tables[sh];

    Private Sub SimulateData()
        lblTime.Text = "00:00"
        Timer1.Enabled = True
        Application.DoEvents()
        'Dim iTrans As New Utilities.TransactionDB
        'Utilities.SqlDB.ExecuteNonQuery("delete from tb_customer", iTrans.Trans)
        'iTrans.CommitTransaction()

        Utilities.SqlDB.CreateTextFile("02|FST_NAME|LAST_NAME|X_TITLE|EMAIL_ADDR|MOBILE_NO|STATUS|BDATE|SEGMENT|CATEGORY|Acc_Balance|CONTACT_CLASS|Service_Year|CHURN_SCORE|Camp_Code|Camp_Name|CAMP_NAME_EN|CAMP_DESC|PRE_LANG|Network Type")
        Dim endReg As Long = 40000000
        ProgressBar1.Maximum = endReg
        For i As Integer = 0 To endReg - 1
            Dim lnq As New Utilities.TbCustomerCenLinqDB
            lnq.F_NAME = "FIRST_NAME " & (i + 1)
            lnq.L_NAME = "LAST_NAME " & (i + 1)
            lnq.TITLE_NAME = "TITLE_NAME" & (i + 1)
            lnq.EMAIL = "EMAIL" & (i + 1)
            lnq.MOBILE_NO = (i + 1).ToString.PadLeft(10, "0")
            lnq.MOBILE_STATUS = "MOBILE_STATUS " & (i + 1)
            lnq.BIRTH_DATE = DateTime.Now
            lnq.SEGMENT_LEVEL = "SEGMENT_LEVEL " & (i + 1)
            lnq.CATEGORY = "CATEGORY" & (i + 1)
            lnq.ACCOUNT_BALANCE = "ACCOUNT_BALANCE" & (i + 1)
            lnq.CONTACT_CLASS = "CONTACT_CLASS" & (i + 1)
            lnq.SERVICE_YEAR = "SERVICE_YEAR" & (i + 1)
            lnq.CHUM_SCORE = "CHUM_SCORE" & (i + 1)
            lnq.CAMPAIGN_CODE = "CAMPAIGN_CODE" & (i + 1)
            lnq.CAMPAIGN_NAME = "CAMPAIGN_NAME" & (i + 1)
            lnq.CAMPAIGN_NAME_EN = "CAMPAIGN_NAME_EN" & (i + 1)
            lnq.CAMPAIGN_DESC = "CAMPAIGN_DESC" & (i + 1)
            lnq.PREFER_LANG = "PREFER_LANG" & (i + 1)
            lnq.NETWORK_TYPE = "NETWORK_TYPE" & (i + 1)

            Dim txtData As String = "02|" & lnq.F_NAME
            txtData += "|" & lnq.L_NAME
            txtData += "|" & lnq.TITLE_NAME
            txtData += "|" & lnq.EMAIL
            txtData += "|" & lnq.MOBILE_NO
            txtData += "|" & lnq.MOBILE_STATUS
            txtData += "|" & lnq.BIRTH_DATE
            txtData += "|" & lnq.SEGMENT_LEVEL
            txtData += "|" & lnq.CATEGORY
            txtData += "|" & lnq.ACCOUNT_BALANCE
            txtData += "|" & lnq.CONTACT_CLASS
            txtData += "|" & lnq.SERVICE_YEAR
            txtData += "|" & lnq.CHUM_SCORE
            txtData += "|" & lnq.CAMPAIGN_CODE
            txtData += "|" & lnq.CAMPAIGN_NAME
            txtData += "|" & lnq.CAMPAIGN_NAME_EN
            txtData += "|" & lnq.CAMPAIGN_DESC
            txtData += "|" & lnq.PREFER_LANG
            txtData += "|" & lnq.NETWORK_TYPE
            Utilities.SqlDB.CreateTextFile(txtData)

            'Dim trans As New Utilities.TransactionDB
            'Dim ret As Boolean = lnq.InsertData("SYSTEM", trans.Trans)
            'If ret = True Then
            '    trans.CommitTransaction()
            'Else
            '    trans.RollbackTransaction()
            '    err += lnq.ErrorMessage
            'End If
            ProgressBar1.Value = i + 1
            lblStatus.Text = "Row " & i & " of " & (endReg) & " Rows" & vbNewLine
            lblStatus.Text += Math.Round(((i * 100) / endReg), 2) & "%"
            Application.DoEvents()
        Next
        Timer1.Enabled = False
    End Sub

    Private Sub btnSimulateData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimulateData.Click
        SimulateData()
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim strTime() As String = Split(lblTime.Text, ":")
        Dim strMin As Integer = Val(strTime(1))
        Dim strHour As Integer = Val(strTime(0))

        If strMin = 59 Then
            strMin = 0
            strHour += 1
        Else
            strMin += 1
        End If

        lblTime.Text = strHour.ToString.PadLeft(2, "0") & ":" & strMin.ToString.PadLeft(2, "0")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'RichTextBox1.Text = System.IO.File.ReadAllText(txtTextFileName.Text)
        RichTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
    End Sub

    Private Sub btnUpBirthDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpBirthDate.Click
        UpdateBirthDate(txtTextFileName.Text)
    End Sub
End Class

Public Enum EncodeType
    _DEFAULT = 0
    _ASCII = 1
    _UNICODE = 2
    _UTF32 = 3
    _UTF7 = 4
    _UTF8 = 5
End Enum