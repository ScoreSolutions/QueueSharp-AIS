Imports System.Data
Imports System.Data.SqlClient
Imports PrinterClassDll
Imports Kiosk.Org.Mentalis.Files
Imports System.IO
Imports System
Imports System.Runtime.InteropServices
Imports System.Resources

Module QueueModule

    Public INIFileName As String = Application.StartupPath & "\QueueSharp.ini"  'Parth ที่ใช้เก็บไฟล์ .ini
    Public CountVDO = 0
    Public MaxCountVDO = 180
    Public Mobile As String


#Region "Database Connection"
    Public Conn As New SqlConnection

    Function getConnectionString() As String
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Return "Data Source=" & ini.ReadString("Server") & ";Initial Catalog=" & ini.ReadString("Database") & ";Persist Security Info=True;User ID=" & ini.ReadString("Username") & ";Password=" & ini.ReadString("Password") & ";"
    End Function

    Public Function CheckConn(ByVal ConnectionString As String, ByVal ShowMessageError As Boolean) As Boolean
        Try
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Conn.ConnectionString = ConnectionString
            Conn.Open()
            Return True
        Catch ex As Exception
            If ShowMessageError Then
                showFormError(ex.Message)
            End If
            Return False
        End Try
    End Function

#End Region
#Region "Convert Data"

    Public Function FixDB(ByVal TXT As String) As String 'Replace text จาก ' เป็น ''
        If IsDBNull(TXT) = True Then
            Return ""
        ElseIf TXT = Nothing Then
            Return ""
        Else
            Return Trim(TXT.ToString.Replace("'", "''"))
        End If
    End Function

    Function FixMoney(ByVal MyMoney As Double) As String 'Convert ตัวเลข เป็น เลขทศนิยม 2 ตำแหน่ง
        Dim Money As String = ""
        Money = Format(MyMoney, "#,###.##")
        Return Money
    End Function

    Public Function FixDate(ByVal StringDate As String) As String 'Convert วันที่ให้เป็น YYYYMMDD
        Dim d As String = ""
        Dim m As String = ""
        Dim y As String = ""
        If IsDate(StringDate) Then
            Dim dmy As Date = CDate(StringDate)
            d = dmy.Day
            m = dmy.Month
            y = dmy.Year
            If y > 2500 Then
                y = y - 543
            End If
            Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
        Else
            Return ""
        End If
    End Function

#End Region
#Region "Manage Query"

    Public Function getDataTable(ByVal SQL As String) As DataTable
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            CheckConn(getConnectionString, False)
            da = New SqlDataAdapter(SQL, Conn)
            da.Fill(dt)
            Return dt
            Conn.Close()
        Catch ex As Exception
            showFormError(ex.Message & Environment.NewLine & Environment.NewLine & SQL)
            Return dt
        End Try
        Return New DataTable
    End Function

    Public Sub executeSQL(ByVal sql As String, Optional ByVal forceExit As Boolean = True)
        If sql.Trim <> "" Then
            Dim cmd As New SqlCommand(sql)
            cmd.Connection = Conn
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                showFormError(ex.Message & Environment.NewLine & Environment.NewLine & sql)
            End Try
        End If
    End Sub

#End Region
#Region "All Error"

    Sub showFormError(ByVal Message As String)
        Dim f As New frmErrorMessage
        f.txtMassage.Text = Message
        f.ShowDialog()
        f.Dispose()
    End Sub

#End Region


    '#Region "Prin"
    '    Public Sub printTicket(ByVal QueueNo As String, ByVal CustomerID As String, ByVal NumQueue As String, ByVal WaitingTime As String, Optional ByVal printerName As String = "EPSON BA-T500 Full cut")
    '        Try
    '            Dim prn As New PrinterClassDll.Win32Print
    '            Dim ini As New IniReader(INIFileName)
    '            ini.Section = "SETTING"
    '            prn.SetPrinterName(printerName) '(SimpleDecrypt(ini.ReadString("PrinterName")))
    '            prn.SetDeviceFont(9.5, "FontControl", False, False)
    '            'prn.PrintText("x")
    '            'prn.PrintText("r")
    '            prn.PrintImage("logo.gif")
    '            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
    '            prn.PrintText("Date: " & IIf(Now.Date.Year > 2500, Now.Date.Year - 543, Now.Date.Year) & Now.Date.ToString("-MM-dd") & " Time: " & Now.ToString("hh:mm:ss tt"))
    '            prn.SetDeviceFont(6, "FontA1x1", False, False)
    '            prn.PrintText(" ")
    '            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
    '            prn.PrintText("Tel. " & CustomerID)
    '            prn.SetDeviceFont(9.5, "FontA1x1", False, False)
    '            prn.PrintText(" ")

    '            Dim sql As String = ""
    '            Dim dt As New DataTable
    '            sql = "select item_id,item_name from tb_counter_queue x left join tb_item y on x.item_id = y.id where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(Mobile) & "' and status = 1 and queue_no = '" & FixDB(QueueNo) & "' order by flag desc"
    '            dt = getDataTable(sql)
    '            For i As Int32 = 0 To dt.Rows.Count - 1
    '                prn.PrintText((i + 1).ToString & ". " & dt.Rows(i).Item("item_name").ToString)
    '            Next

    '            'If QueueNo.Length > 0 Then
    '            '    prn.SetDeviceFont(50, "Free 3 of 9 Extended", False, True)
    '            '    'prn.SetDeviceFont(50, "Code128", False, True)
    '            '    prn.PrintText("*" & QueueNo & "*")
    '            '    'prn.PrintText(hn & vbLf)
    '            'End If
    '            prn.SetDeviceFont(6, "FontA1x1", True, True)
    '            prn.PrintText(" ")
    '            prn.SetDeviceFont(14, "FontA2x2", True, True)
    '            prn.PrintText("Q# " & QueueNo)
    '            prn.SetDeviceFont(6, "FontA1x1", True, True)
    '            prn.PrintText(" ")
    '            prn.SetDeviceFont(10, "Angsana UPC", False, True)
    '            prn.PrintText("มีคิวรอก่อนหน้าคุณ " & NumQueue & " ราย")
    '            prn.PrintText("เวลาที่รอรับบริการประมาณ " & WaitingTime & " นาที")
    '            prn.SetDeviceFont(6, "FontA1x1", True, True)
    '            prn.PrintText(" ")
    '            prn.PrintText(" ")
    '            prn.PrintText(" ")
    '            prn.PrintText(" ")
    '            prn.SetDeviceFont(9.5, "FontControl", False, False)
    '            prn.PrintImage("footer.gif")

    '            prn.EndDoc()
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try


    '    End Sub
    '#End Region

#Region "Prin"
    Public Sub printTicket(ByVal QueueNo As String, ByVal CustomerID As String, ByVal NumQueue As String, ByVal WaitingTime As String, Optional ByVal printerName As String = "EPSON BA-T500 Full cut")
        Try
            Dim prn As New PrinterClassDll.Win32Print
            Dim ini As New IniReader(INIFileName)
            Dim txtprint As String
            Dim spacestr As String
            Dim Getspace As Integer
            ini.Section = "SETTING"
            prn.SetPrinterName(printerName) '(SimpleDecrypt(ini.ReadString("PrinterName")))
            prn.SetDeviceFont(9.5, "FontControl", False, False)
            'prn.PrintText("x")
            'prn.PrintText("r")
            '----------------------------------------------------
            prn.PrintImage("logo.gif")
            '----------------------------------------------------
            prn.SetDeviceFont(12, "Angsana UPC", True, True)
            txtprint = "ยินดีต้อนรับ"
            Getspace = GetSpacestr(Len(txtprint), "Angsana UPC", 12)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "     " & txtprint)
            '----------------------------------------------------
            prn.SetDeviceFont(9.5, "FontA1x1", False, False)
            prn.PrintText(" ")
            '----------------------------------------------------
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            prn.PrintText("       Date: " & Now.Date.ToString("dd-MM-") & IIf(Now.Date.Year > 2500, Now.Date.Year - 543, Now.Date.Year) & " Time: " & Now.ToString("HH:mm ") & "น.")
            '----------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", False, False)
            prn.PrintText(" ")
            '----------------------------------------------------
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            txtprint = CustomerID.ToString
            Getspace = GetSpacestr(Len(txtprint), "FontA1x1", 9.5)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "   " & CustomerID)
            '----------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", False, False)
            prn.PrintText(" ")
            '----------------------------------------------------
            prn.SetDeviceFont(10, "Angsana UPC", False, True)

            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select item_id,item_name from tb_counter_queue x left join tb_item y on x.item_id = y.id where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(Mobile) & "' and status = 1 and queue_no = '" & FixDB(QueueNo) & "' order by flag desc"
            dt = getDataTable(sql)
            For i As Int32 = 0 To dt.Rows.Count - 1
                txtprint = Trim(dt.Rows(i).Item("item_name").ToString)
                Getspace = GetSpacestr(Len(txtprint), "Angsana UPC", 10)
                spacestr = returnStrpadding(Getspace)
                prn.PrintText("                      " & txtprint)
            Next
            '-----------------------------------------------------
            'If QueueNo.Length > 0 Then
            '    prn.SetDeviceFont(50, "Free 3 of 9 Extended", False, True)
            '    'prn.SetDeviceFont(50, "Code128", False, True)
            '    prn.PrintText("*" & QueueNo & "*")
            '    'prn.PrintText(hn & vbLf)
            'End If
            '---------------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", True, True)
            prn.PrintText(" ")
            prn.PrintText(" ")
            prn.SetDeviceFont(10, "Angsana UPC", True, True)
            txtprint = "หมายเลข"
            Getspace = GetSpacestr(Len(txtprint), "Angsana UPC", 10)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "   " & txtprint)
            '--------------------------------------------------------
            prn.SetDeviceFont(14, "FontA2x2", True, True)
            txtprint = QueueNo
            Getspace = GetSpacestr(Len(txtprint), "FontA2x2", 14)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "   " & QueueNo)
            '--------------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", True, True)
            prn.PrintText(" ")
            prn.PrintText(" ")
            '--------------------------------------------------------
            prn.SetDeviceFont(10, "Angsana UPC", False, True)
            txtprint = "มีคิวรอก่อนหน้าคุณ " & NumQueue & " ราย"
            Getspace = GetSpacestr(Len(txtprint), "Angsana UPC", 10)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "       " & txtprint)
            '--------------------------------------------------------
            txtprint = "เวลาที่รับบริการประมาณ " & WaitingTime & " นาที"
            Getspace = GetSpacestr(Len(txtprint), "Angsana UPC", 10)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "       " & txtprint)
            '--------------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", True, True)
            prn.PrintText(" ")
            '--------------------------------------------------------
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            txtprint = "CRM Campaign :"
            Getspace = GetSpacestr(Len(txtprint), "FontA1x1", 9.5)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "    " & txtprint)
            '--------------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", True, True)
            prn.PrintText(" ")
            '--------------------------------------------------------
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            txtprint = "----------------"
            Getspace = GetSpacestr(Len(txtprint), "FontA1x1", 9.5)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "        " & txtprint)
            '---------------------------------------------------------
            prn.SetDeviceFont(9.5, "FontA1x1", True, True)
            txtprint = "----------------"
            Getspace = GetSpacestr(Len(txtprint), "FontA1x1", 9.5)
            spacestr = returnStrpadding(Getspace)
            prn.PrintText(spacestr & "        " & txtprint)
            '---------------------------------------------------------
            prn.SetDeviceFont(6, "FontA1x1", True, True)
            prn.PrintText(" ")
            prn.PrintText(" ")
            prn.SetDeviceFont(9.5, "FontControl", False, False)
            prn.PrintImage("footer.gif")
            prn.EndDoc()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Private Function returnStrpadding(ByVal total As Integer) As String
        Dim spacestr As String
        For i As Integer = 1 To total
            spacestr = spacestr & "  "
        Next
        Return spacestr
    End Function
    Private Function GetSpacestr(ByVal Lentxt As Integer, ByVal font As String, ByVal fontsize As Double) As Integer
        Dim formatlen As Integer
        Dim padding As Integer
        If font = "FontA1x1" Then
            If fontsize = 9.5 Then
                formatlen = 31 / 2
                Lentxt = Lentxt / 2
                padding = (formatlen - Lentxt)
                Return padding
            End If
        ElseIf font = "FontA2x2" Then
            If fontsize = 14 Then
                formatlen = 20 / 2
                Lentxt = Lentxt / 2
                padding = (formatlen - Lentxt)
                Return padding
            End If
        ElseIf font = "Angsana UPC" Then
            If fontsize = 10 Then
                formatlen = 32 / 2
                Lentxt = Lentxt / 2
                padding = (formatlen - Lentxt)
                Return padding
            ElseIf fontsize = 12 Then
                formatlen = 30 / 2
                Lentxt = Lentxt / 2
                padding = (formatlen - Lentxt)
                Return padding
            End If
        End If

    End Function
#End Region


    Sub InsertService(ByVal CustomerID As String, ByVal CustomerTypeID As Integer, ByVal ItemID As Integer, Optional ByVal QueueNo As String = "", Optional ByVal UserID As Int32 = 0)
        '@queue_no as varchar(20)
        '@customertype_id as int
        '@item_id as int
        '@txt_queue as char(1)
        '@no_queue as int
        Dim sql As String = ""
        sql = "exec SP_InsertService '" & CustomerID & "'," & CustomerTypeID & "," & ItemID & ",'" & QueueNo & "'," & UserID
        executeSQL(sql)
        InsertLog(QueueNo, CustomerID, UserID, ItemID, 0, 1)
    End Sub

    Function FindService(ByVal argCustomerID As String) As String()
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim service(3) As String
        sql = "exec SP_FindService '" & FixDB(argCustomerID) & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            service(0) = dt.Rows(0)("item_id").ToString
            service(1) = dt.Rows(0)("item_name").ToString

            Dim txtTime As String = ""
            If CInt(dt.Rows(0)("TotalTime")) = "1000000" Then
                txtTime = "-"
            Else
                txtTime = CStr((CInt(dt.Rows(0)("TotalTime")) / 60) + 1) & " Minute"
            End If
            service(2) = txtTime

            For i As Int32 = 0 To dt.Rows.Count - 1
                If service(3) = "" Then
                    service(3) = dt.Rows(i)("item_name").ToString
                Else
                    service(3) = service(3) & ", " & dt.Rows(i)("item_name").ToString
                End If
            Next

            Return service
        End If
        Return service
    End Function

    'Insert Log กรณีที่มีการเปลี่ยนแปลงสถานะของลูกค้า
    Sub InsertLog(ByVal QueueNo As String, ByVal CustomerID As String, ByVal UserID As Integer, ByVal ItemID As Integer, ByVal CounterID As Integer, ByVal Status As Integer, Optional ByVal Flag As String = "")
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim RowID As Int32 = 0
        Dim Item As Int32 = 0
        sql = "select id from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and customer_id = '" & CustomerID & "' and queue_no = '" & QueueNo & "' and item_id = " & ItemID
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            Item = dt.Rows(0).Item("id")
        End If
        '@cq_id as int,
        '@queue_no as varchar(20),
        '@customer_id as varchar(20),
        '@member_id as int,
        '@item_id as int,
        '@counter_id as int,
        '@status as smallint

        sql = "exec SP_InsertLog " & Item & ",'" & FixDB(QueueNo) & "','" & FixDB(CustomerID) & "'," & UserID & "," & ItemID & "," & CounterID & "," & Status & ",'" & FixDB(Flag) & "'"
        executeSQL(sql)

    End Sub

    Function GenQueueNo(ByVal ItemId As String, ByVal CustomerTypeID As String) As String
        Dim QueueNo As String = ""
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "declare @Item as int; select @Item = " & ItemId & ";declare @Min as varChar(3); select @Min = (select min_queue from TB_CUSTOMERTYPE where id = " & CustomerTypeID & ");declare @Max as varChar(3); select @Max = (select max_queue from TB_CUSTOMERTYPE where id = " & CustomerTypeID & ");declare @Q as Char;select @Q = (select txt_queue from TB_ITEM where id = @Item); select MAX(queue_no) as queue_no,@Q as txt_queue,@Min as min_queue,@Max as max_queue from (select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 and item_id = @Item and queue_no <> '' and convert(int,(right(queue_no,3))) between @Min and @Max and left(queue_no,1) = @Q group by queue_no) as xxx "
        dt = getDataTable(sql)
        If dt.Rows(0).Item("queue_no").ToString = "" Then
            Return dt.Rows(0).Item("txt_queue").ToString & dt.Rows(0).Item("min_queue").ToString.PadLeft(3, "0")
        Else
            Dim Q As Int32 = CInt(StringFromRight(dt.Rows(0).Item("queue_no").ToString, 3)) + 1
            Return dt.Rows(0).Item("txt_queue").ToString & Q.ToString.PadLeft(3, "0")
        End If
        Return QueueNo
    End Function

    Public Function StringFromLeft(ByVal strTmp As String, ByVal strLength As Integer) As String
        If (strLength > 0 And strTmp.Length >= strLength) Then
            Return strTmp.Substring(0, strLength)
        Else
            Return strTmp
        End If
    End Function

    Public Function StringFromRight(ByVal strTmp As String, ByVal strLength As Integer) As String
        If (strLength > 0 And strTmp.Length >= strLength) Then
            Return strTmp.Substring(strTmp.Length - strLength, strLength)
        Else
            Return strTmp
        End If
    End Function

    Sub PrintQueue(ByVal QueueNo As String, ByVal CustomerID As String, ByVal CustomerType As String)

        'อ่านค่าจาก File .ini
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim PrinterName As String = ini.ReadString("PrinterName")
        Dim copy As Int32 = 0
        '******************
        printTicket(QueueNo, CustomerID, "3", "10", PrinterName)

    End Sub

    Function ShowDialogBox(ByVal Text As String, ByVal HeadText As String, Optional ByVal btnYesNo As Boolean = False) As Boolean
        Dim f As New frmDialogMsg
        If btnYesNo = True Then
            f.btnOK.Visible = False
        Else
            f.btnConfirm.Visible = False
            f.btnCancel.Visible = False
        End If
        f.Text = HeadText
        f.lblText.Text = Text
        If f.ShowDialog() = Windows.Forms.DialogResult.Yes Then
            Return True
        End If
        Return False
    End Function

End Module
