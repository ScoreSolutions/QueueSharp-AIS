Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports QueueSharp.Org.Mentalis.Files

Public Class frmRegister

    Dim dt_service As New DataTable 'เก็บข้อมูลของประเภทบริการ
    Dim LastQueue As String = ""
    Dim LastPay As Boolean = False
    Dim LastIn As Boolean = False

    Public Sub Clear()
        'เคลียร์ค่าใน Textbox
        txtType.Text = ""
        txtCustomerID.Text = ""
        txtTypeId.Text = ""
        'frmMain.lblRoomName.Text = ""
        txtCustomerID.Focus()

        For i As Int32 = 0 To FLPType.Controls.Count - 1
            FLPType.Controls(i).BackColor = Color.LightSeaGreen
        Next

        For i As Int32 = 0 To FLPService.Controls.Count - 1
            FLPService.Controls(i).BackColor = Color.LightSeaGreen
        Next

    End Sub

    Private Function Validation() As Boolean
        '*************** เช็คการกรอกข้อมูล ***************
        If txtCustomerID.Text = "" Then
            MessageBox.Show("Please enter mobile number !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If txtType.Text = "" Then
            MessageBox.Show("Please select customer type !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim chk As Boolean = False
        For i As Int32 = 0 To FLPService.Controls.Count - 1
            If FLPService.Controls(i).BackColor = Color.Orange Then
                chk = True
                Exit For
            End If
        Next
        If chk = False Then
            MessageBox.Show("Please select service !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub ButOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButOK.Click
        If Validation() = False Then Exit Sub

        '************** เช็คการตั้งค่าหมายเลขคิวของประเภทลูกค้า *************
        '        'QueueNo = GenQueueNo(txtTypeId.Text)
        'If QueueNo(0) = "" Then
        '    MessageBox.Show("Queue No has not support customer typy !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        '*********** เช็คว่ามีบริการค้างอยู่ในระบบหรือไม่ **********
        Dim sql As String = ""
        Dim dt As New DataTable

        sql = "exec SP_RemainAllService '" & FixDB(txtCustomerID.Text) & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            If MessageBox.Show("This customer is already in queue !!!" & vbCrLf & "Do you want cancel all service ?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    UpdateQueueStatus(5, 0, 0, dt.Rows(i).Item("queue_no"), dt.Rows(i).Item("customer_id"), dt.Rows(i).Item("item_id"))
                Next
            Else
                Clear()
                Exit Sub
            End If
        End If


        Dim vDateNow As String = FixDateTime(FindDateNow)
        For i As Int32 = 0 To FLPService.Controls.Count - 1
            If FLPService.Controls(i).BackColor = Color.Orange Then
                InsertService(txtCustomerID.Text, txtTypeId.Text, FLPService.Controls(i).Name, "", 0, vDateNow)
            End If
        Next

        Dim Service() As String
        Service = FindService(txtCustomerID.Text)
        Dim Queue As String = ""
        Queue = GenQueueNo(Service(0), txtTypeId.Text)

        If Queue <> "" Then
            'Update Queue No.
            sql = "update TB_COUNTER_QUEUE set queue_no = '" & FixDB(Queue) & "' where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(txtCustomerID.Text) & "' and status = 1"
            executeSQL(sql)

            'Update Service
            sql = "update TB_COUNTER_QUEUE set flag = '1',assign_time = getdate() where datediff(d,getdate(),service_date)=0 and customer_id = '" & FixDB(txtCustomerID.Text) & "' and queue_no = '" & FixDB(Queue) & "' and item_id = " & Service(0)
            executeSQL(sql)

            txtMessage.Text = "Queue No : " & Queue & vbCrLf & "Mobile No : " & txtCustomerID.Text & vbCrLf & "Waiting Time : " & Service(2) & vbCrLf & "Next Services : " & Service(3)

        Else
            MessageBox.Show("Please check config Queue No length !!!" & vbCrLf & "This service " & Service(1), "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Exit Sub
        End If

        'Print(QueueNo(0), txtCustomerID.Text, txtType.Text)
        'Dim txt As String = ""
        'txt &= "Queue No : " & QueueNo(0) & vbCrLf
        'txt &= "Customer type :" & txtType.Text & vbCrLf
        'txt &= "Register completed"
        'txtMessage.Text = txt

        Clear()
    End Sub

#Region "Add Button"
    Sub LoadBtnType()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,customertype_name,txt_queue from TB_customertype where active_status = 1 order by customertype_code"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim btn As New Button
                With btn
                    .Width = 242
                    .Height = 35
                    .Name = dt.Rows(i).Item("id").ToString
                    .Text = dt.Rows(i).Item("customertype_name").ToString
                    .Tag = dt.Rows(i).Item("txt_queue").ToString
                    .AutoSize = True
                    .FlatStyle = FlatStyle.Flat
                    .BackColor = Color.LightSeaGreen
                    .FlatAppearance.BorderSize = 1
                    .FlatAppearance.BorderColor = Color.Black
                End With
                FLPType.Controls.Add(btn)
                AddHandler btn.Click, AddressOf CheckType
            Next
        End If
    End Sub

    Private Sub CheckType(ByVal Sender As Object, ByVal e As EventArgs)
        For i As Int32 = 0 To FLPType.Controls.Count - 1
            FLPType.Controls(i).BackColor = Color.LightSeaGreen
            FLPType.Controls(i).ForeColor = Color.Black
        Next
        Dim btn As Button = Sender
        btn.BackColor = Color.Orange
        txtType.Text = btn.Text
        txtTypeId.Text = btn.Name
    End Sub

    Sub LoadBtnService()
        Dim sql As String = ""
        Dim dt As New DataTable
        dt = New DataTable
        sql = "select id,item_name from TB_ITEM where active_status = 1 order by item_name"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim btn As New Button
                With btn
                    .Width = 242
                    .Height = 35
                    .Name = dt.Rows(i).Item("id").ToString
                    .Text = dt.Rows(i).Item("item_name").ToString
                    .AutoSize = True
                    .FlatStyle = FlatStyle.Flat
                    .BackColor = Color.LightSeaGreen
                    .FlatAppearance.BorderSize = 1
                    .FlatAppearance.BorderColor = Color.Black
                End With
                FLPService.Controls.Add(btn)
                AddHandler btn.Click, AddressOf CheckService
            Next
        End If
    End Sub
    Private Sub CheckService(ByVal Sender As Object, ByVal e As EventArgs)
        Dim btn As Button = Sender
        If btn.BackColor = Color.LightSeaGreen Then
            btn.BackColor = Color.Orange
        Else
            btn.BackColor = Color.LightSeaGreen
        End If
    End Sub
#End Region

#Region "Printter"
    Private Sub CheckPrint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckPrint.CheckedChanged
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        If CheckPrint.Checked = True Then
            ini.Write("Print", "1")
        Else
            ini.Write("Print", "0")
        End If
    End Sub

    Private Sub ButRePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButRePrint.Click
        Dim f As New frmReprint
        f.ShowDialog()
    End Sub

    Sub Print(ByVal QueueNo As String, ByVal CustomerID As String, ByVal CustomerType As String)

        'อ่านค่าจาก File .ini
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim PrinterName As String = ini.ReadString("PrinterName")
        Dim copy As Int32 = 0
        '******************

        If CInt(ini.ReadString("Print")) = 1 Then
            '********Print********
            If PrinterName <> "" Then
                copy = CInt(ini.ReadString("Copy"))
            End If
            For j As Int32 = 1 To copy
                printTicket(QueueNo, CustomerID, CustomerType, PrinterName)
            Next
            '*********************
        End If


        'Try
        '    'อ่านค่าจาก File .ini
        '    Dim ini As New IniReader(INIFileName)
        '    ini.Section = "SETTING"
        '    Dim pnt As String = ini.ReadString("Print")
        '    Dim PrinterName As String = ini.ReadString("Printer")
        '    Dim copy As Int32 = 0
        '    '*********************

        '    '********Print********
        '    If pnt = "1" Then
        '        If PrinterName <> "" Then
        '            copy = CInt(ini.ReadString("Copy"))
        '        End If
        '        For j As Int32 = 1 To copy
        '            printTicket(Queue, PrinterName)
        '        Next
        '    End If
        '    '*********************
        'Catch ex As Exception : End Try
    End Sub

#End Region

#Region "Timer Tab View"
    Sub showcustomer_register() 'แสดงข้อมูลลูกค้าที่ Register เข้าระบบไปแล้ว
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "exec SP_RegisterToDay"
        dt = getDataTable(sql)
        GridCustomer.DataSource = dt
    End Sub

    Private Sub ButRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButRefresh.Click
        showcustomer_register()
    End Sub


    Private Sub CheckTimerView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckTimerView.CheckedChanged
        checkAutoRefresh()
    End Sub

    Function checkAutoRefresh() As Boolean
        If CheckTimerView.Checked Then
            timerRefreshView.Interval = CInt(DomainUp.Value) * 1000
            timerRefreshView.Enabled = True
            DomainUp.Enabled = True
            ButRefresh.Enabled = False
        Else
            timerRefreshView.Enabled = False
            DomainUp.Enabled = False
            ButRefresh.Enabled = True
        End If
    End Function

    Private Sub TabRegister_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabRegister.SelectedIndexChanged
        txtMessage.Text = ""
        Select Case TabRegister.SelectedTab.Name.ToUpper
            Case "TabQueue".ToUpper
                CheckTimerView.Checked = False
                timerRefreshView.Enabled = False
            Case "TabView".ToUpper
                timerRefreshView.Enabled = True
                showcustomer_register()
                CheckTimerView.Checked = True
                timerRefreshView.Enabled = True
        End Select
    End Sub

    Private Sub timerRefreshView_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefreshView.Tick
        If TabRegister.SelectedIndex <> 1 Then Exit Sub
        showcustomer_register()
    End Sub
#End Region

    Private Sub frmQueue_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            Me.ControlBox = False
        Else
            Me.ControlBox = True
        End If
    End Sub

    Private Sub frmQueue_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'ตรวจสอบดูว่าตั้งค้าให้ Print Queue หรือไม่
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim pnt As String = ini.ReadString("Print")
        Dim printname As String = ini.ReadString("PrinterName")
        If printname = "" Then
            CheckPrint.Enabled = False
            CheckPrint.Checked = False
        Else
            If pnt = "1" Then
                CheckPrint.Checked = True
            Else
                CheckPrint.Checked = False
            End If
        End If
        txtCustomerID.Focus()
        LoadBtnType()
        LoadBtnService()
    End Sub



    Private Sub txtCustomerID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerID.KeyPress
        If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub ButtonClear_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        Clear()
    End Sub

    Private Sub pgPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pgPrint.Click
        If CheckPrint.Checked = True Then
            CheckPrint.Checked = False
        Else
            CheckPrint.Checked = True
        End If
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        If CheckPrint.Checked = True Then
            ini.Write("Print", "1")
        Else
            ini.Write("Print", "0")
        End If
    End Sub

End Class