Imports Kiosk.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG
Imports Engine.Kiosk.KioskModule
Imports Engine.Kiosk
Imports System.IO

Public Class frmAppointment
    Dim ClickBefore As String
    Public ChooseService As String = ""
    Dim Slot As Int32 = 0
    Dim Gap As Int32 = 0
    Dim Capacity As Int32 = 0
    Dim date_now As String
    Dim ServeTime As Int32


    Private Sub frmAppointment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetLogo()
    End Sub

    Sub SetLogo()
        Try
            Dim logopath As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "LogoKioskPath", SoftwareName, "frmAppointment.SetLogo")
            If File.Exists(logopath) = False Then
                logopath = Application.StartupPath & "\Images\logo_kiosk.png"
            End If
            PictureBox1.BackgroundImage = Image.FromFile(logopath)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmRegister.Show()
        Me.Close()
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.Exit()
    End Sub

    Private Sub PictureBox_(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        CountVDO = 0
    End Sub

    Private Sub btnP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnP.Click
        If Language = "TH" Then
            frmChooseService.btnTH.PerformClick()
        Else
            frmChooseService.btnEN.PerformClick()
        End If
        frmChooseService.RenderObject()
        frmChooseService.RenderService()
        Me.Close()
    End Sub

    Private Sub btnMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMain.Click
        Me.Close()
        'frmChooseService.Close()
        frmRegister.BringToFront()
    End Sub

    Function InsertAppointment(ByVal Time As String) As Boolean
        If ShowDialogBox(msgAvailBookTime & Time & vbCrLf & msgSureToBook, msgConfirm, True) = True Then
            'Dim sql As String = ""
            'Dim item As String() = ChooseService.Split(",")
            'For i As Int32 = 0 To item.Length - 1
            '    Dim Id As Int32 = FindID("TB_APPOINTMENT_CUSTOMER", INIFileName, SoftwareName, "frmAppointment_InsertAppointment")
            '    sql = "INSERT INTO TB_APPOINTMENT_CUSTOMER (id, app_date,capacity,item_id, customer_id,start_slot,active_status,create_by,create_date) VALUES(" & Id & ", getdate()," & Capacity & ", " & item(i) & ",'" & Mobile & "', '" & FixDate(FindDateNow(INIFileName, SoftwareName, "frmAppointment_InsertAppointment")) & " " & Time & "',1,0,getdate())"
            '    executeSQL(sql, INIFileName, SoftwareName, "frmAppointment_InsertAppointment")
            'Next
            'Return True

            Dim eng As New KioskAppointmentENG
            Dim ret As Boolean = eng.InsertAppointment(Capacity, ChooseService, Time, INIFileName, SoftwareName, "frmAppointment_InsertAppointment")
            eng = Nothing

            Return ret
        End If
        Return False
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Cursor = Cursors.WaitCursor
        Dim StartSlotTime As String = ""
        Dim ret As Boolean = False
        For x As Int32 = 0 To FLP.Controls.Count - 1
            If FLP.Controls(x).BackColor = Color.DarkRed Then
                StartSlotTime = FLP.Controls(x).Name
                ret = True
                Exit For
            End If
        Next
        If ret = False Then
            Me.Cursor = Cursors.Default
            ShowDialogBox(msgChooseAppTime, msgWarning)
            Exit Sub
        End If

        Dim sql As String = ""
        Dim dt As New DataTable
        Dim AllService() As String = Split(ChooseService, ",")
        Dim CountService As Int32 = AllService.Length
        Dim eng As New KioskAppointmentENG '
        Dim AppointmentJobID As Long = 0

        'sql = "select id,in_use from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,GETDATE()) = 0 and in_use < capacity and CONVERT(varchar(5),slot_time,114) between '" & StartSlotTime & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, CDate(StartSlotTime)).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, CDate(StartSlotTime)).Minute.ToString.PadLeft(2, "0") & "'"
        'dt = getDataTable(sql, INIFileName, SoftwareName, "frmAppointment_btnOK_Click")
        dt = eng.GetAppointmentSlotInUse(INIFileName, SoftwareName, "frmAppointment_btnOK_Click", StartSlotTime, Slot, CountService)
        If dt.Rows.Count = CountService Then
            Dim DateSend As String = ""
            Dim Item As String = ""
            Dim Service() As String = ChooseService.Split(",")

            'Dim dt_temp As New DataTable
            'sql = "select item_name,item_name_th,item_order from tb_item where id in (" & ChooseService & ") order by item_wait,item_time,item_order"
            'dt_temp = getDataTable(sql, INIFileName, SoftwareName, "frmAppointment_btnOK_Click")

            Dim dt_temp As New DataTable
            dt_temp = eng.GetSelectedItem(ChooseService, INIFileName, SoftwareName, "frmAppointment_btnOK_Click")
            If dt_temp.Rows.Count > 0 Then
                For i As Int32 = 0 To dt_temp.Rows.Count - 1
                    If Item = "" Then
                        If dt_temp.Rows.Count > 1 Then
                            If Language = "TH" Then
                                Item = dt_temp.Rows(i).Item(itemname_field).ToString
                            Else
                                Item = dt_temp.Rows(i).Item(itemname_field).ToString
                            End If
                        Else
                            Item = dt_temp.Rows(i).Item(itemname_field).ToString
                        End If
                    Else
                        Item = Item & vbCrLf & dt_temp.Rows(i).Item(itemname_field).ToString
                    End If
                Next
            End If

            If Language = "TH" Then
                lblMsg.Text = Mobile & vbCrLf & vbCrLf & msgHaveAppointment & vbCrLf & Item & vbCrLf & msgAppTimeAt & StartSlotTime & msgAtclock
                lblFooter.Text = msgPleasArrive & k_before & msgMinuteBeforeApptime & vbCrLf & msgAutomaticCancelApp
            Else
                lblMsg.Text = Mobile & vbCrLf & vbCrLf & msgHaveAppointment & vbCrLf & Item & vbCrLf & vbCrLf & msgAppTimeAt & StartSlotTime
                lblFooter.Text = msgPleasArrive & k_before & msgMinuteBeforeApptime & vbCrLf & msgLateArriveThan & vbCrLf & msgAutomaticCancelApp
            End If

            '********* Insert ตารางนัด ********
            If InsertAppointment(StartSlotTime) = True Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    'sql = "update TB_APPOINTMENT_SLOT set in_use = " & CInt(dt.Rows(i).Item("in_use")) + 1 & " where id = " & dt.Rows(i).Item("id")
                    'executeSQL(sql, INIFileName, SoftwareName, "frmAppointment_btnOK.Click")
                    eng.UpdateUseAppointmentSlot(CInt(dt.Rows(i).Item("in_use")) + 1, dt.Rows(i).Item("id"), INIFileName, SoftwareName, "frmAppointment_btnOK.Click")
                Next
            Else
                dt_temp.Dispose()
                dt.Dispose()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            PanelMsg.Visible = True
            TimerEnd.Enabled = True
            Application.DoEvents()

            '********************************
            Try
                SendCaptureImageToCaptureAppointment(Mobile)
                DateSend = FindDateNow(INIFileName, SoftwareName, "frmAppointment_btnOK.Click").ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("th-TH")) & " " & StartSlotTime & ":00"
                SendSMS(Mobile, ChooseService, DateSend)

                Item = ""
                dt_temp.DefaultView.Sort = " item_wait,item_time,item_order"
                For Each dr As DataRowView In dt_temp.DefaultView
                    If Item.Trim = "" Then
                        Item = dr(itemname_field)
                    Else
                        Item = Item & "," & dr(itemname_field)
                    End If
                Next
                AppointmentJobID = SiebelCreateActivity(Mobile, DateSend, Item)
            Catch ex As Exception
                Dim sqlLog As String = ""
                sqlLog = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
                sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmAppointment_btnOK.Click") & ", 1, getdate(),null,null,"
                sqlLog += " getdate(), '" & ex.Message & "', null, '" & SoftwareName & "', '" & frmRegister.getMyVersion & "')"
                executeSQL(sqlLog, INIFileName, SoftwareName, "frmAppointment_btnOK.Click")
            End Try
            dt_temp.Dispose()
            dt.Dispose()
        Else
            Dim f As New frmDialogMsg
            f.lblText.Text = msgAppTime
            f.btnConfirm.Visible = False
            f.btnCancel.Visible = False
            f.btnOK.Visible = False

            If Language = "TH" Then
                f.btnOK.Text = "ตกลง"
                f.btnPrevious.Text = "ก่อนหน้า"
                f.btnMain.Text = "หน้าหลัก"
            Else
                f.btnOK.Text = "OK"
                f.btnPrevious.Text = "Previous"
                f.btnMain.Text = "Main Menu"
            End If

            f.btnOK.Visible = True
            f.btnPrevious.Visible = True
            f.btnMain.Visible = True

            Select Case f.ShowDialog
                Case Windows.Forms.DialogResult.Yes
                    Me.Cursor = Cursors.Default
                    btnP.PerformClick()
                    Exit Sub
                Case Windows.Forms.DialogResult.OK
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Case Windows.Forms.DialogResult.Cancel
                    Me.Cursor = Cursors.Default
                    btnMain.PerformClick()
                    Exit Sub
            End Select
        End If

        If CustomerImageID <> 0 Then
            'Update TB_CustomerImage AppointmentJiob_ID =AppointmentJobID  where ID = CustomerImageID
            Dim sqlUpdate As String = "Update TB_CUSTOMER_IMAGE set Appointment_Job_ID = '" & AppointmentJobID & _
            "' Where ID='" & CustomerImageID & "'"
            executeSQL(sqlUpdate, INIFileName, SoftwareName, "frmAppointment_btnOK.Click")
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function SiebelCreateActivity(ByVal MobileNo As String, ByVal StartSlot As String, ByVal ItemName As String) As Long
        Dim ret As Long = 0
        Try
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmAppointment_SiebelCreateActivity")
            Dim rJob As New ShopWebServiceMain.SiebelResponsePara
            rJob = ws.SiebelCreateActivity(MobileNo, StartSlot, ItemName, AppointmentChannel)
            ret = rJob.APPOINTMENT_JOB_ID
            rJob = Nothing
            ws = Nothing
        Catch ex As Exception
            Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
            sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmAppointment_SiebelCreateActivity") & ", 1, getdate(),null,null,"
            sqlLog += " getdate(), 'Don''t Create Siebel Activity Mobile No. " & FixDB(Mobile) & "### Exception :" & ex.Message & " ', null, '" & SoftwareName & "', '" & frmRegister.getMyVersion & "')"
            executeSQL(sqlLog, INIFileName, SoftwareName, "frmAppointment_SiebelCreateActivity")

            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Timeout = 20000

            Dim rJob As New ShopWebServiceDisplay.SiebelResponsePara
            ws.Url = GetWebServiceURL(False, INIFileName, SoftwareName, "frmAppointment_SiebelCreateActivity")
            rJob = ws.SiebelCreateActivity(MobileNo, StartSlot, ItemName, AppointmentChannel)
            ret = rJob.APPOINTMENT_JOB_ID
            rJob = Nothing
            ws = Nothing
        End Try
        Return ret
    End Function

    Private Sub SendCaptureImageToCaptureAppointment(ByVal MobileNo As String)
        Dim eng As New KioskBaseENG
        Dim _file As String = eng.GetCapturePath & MobileNo & ".jpg"
        If IO.File.Exists(_file) = True Then
            IO.File.Move(_file, eng.GetCaptureAppointmentPath & MobileNo & ".jpg")
        End If
        eng = Nothing
    End Sub

    Private Sub SendSMSFromDisplay(ByVal Mobile As String, ByVal ServiceID As String, ByVal AppointmentTile As String)
        Try
            Dim smsResBackup As ShopWebServiceDisplay.SMSResponsePara
            Dim sendSMSBackup As New ShopWebServiceDisplay.WebServiceAPI
            sendSMSBackup.Url = d_webservice_url
            smsResBackup = sendSMSBackup.ShopSendSMS(Mobile, ServiceID, AppointmentTile)
            If smsResBackup.RESULT = False Then
                Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
                sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmAppointment_SendSMSFromDisplay") & ", 1, getdate(),null,null,"
                sqlLog += " getdate(), 'Don''t Send SMS Mobile No. " & FixDB(Mobile) & "', null, '" & SoftwareName & "', '" & frmRegister.getMyVersion & "')"
                executeSQL(sqlLog, INIFileName, SoftwareName, "frmAppointment_SendSMSFromDisplay")
            End If

        Catch ey As Exception : End Try
    End Sub

    Sub SendSMS(ByVal Mobile As String, ByVal ServiceID As String, ByVal AppointmentTile As String)
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try
            Dim smsRes As ShopWebServiceMain.SMSResponsePara
            Dim sendSMS As New ShopWebServiceMain.WebServiceAPI
            sendSMS.Url = m_webservice_url
            smsRes = sendSMS.ShopSendSMS(Mobile, ServiceID, AppointmentTile)
            If smsRes.RESULT = False Then
                Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
                sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmAppointment_SendSMS") & ", 1, getdate(),null,null,"
                sqlLog += " getdate(), 'Don''t Send SMS Mobile No. " & FixDB(Mobile) & "', null, '" & SoftwareName & "', '" & frmRegister.getMyVersion & "')"
                executeSQL(sqlLog, INIFileName, SoftwareName, "frmAppointment_SendSMS")
            End If
        Catch ex As Exception
            SendSMSFromDisplay(Mobile, ServiceID, AppointmentTile)
        End Try

        ini = Nothing
    End Sub

    Private Sub frmAppointment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ChangeLanguage()
        Dim sql As String = ""
        Dim dt As New DataTable
        '******************* Create Slot *********************
        'sql = "declare @EndSlot as datetime; select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,GETDATE()) = 0);select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & k_before_close & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & k_before_close & ") * -1 ,end_slot) end as end_time ,slot from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,GETDATE(),app_date)=0"
        'dt = getDataTable(sql, INIFileName, SoftwareName, "frmAppointment_Shown")

        'Gen Slot
        Dim eng As New KioskAppointmentENG
        dt = eng.GetTimeSlot(k_before_close, INIFileName, SoftwareName, "frmAppointment_Shown")
        If dt.Rows.Count > 0 Then
            Dim vDateNow As DateTime = FindDateNow(INIFileName, SoftwareName, "frmAppointment_Shown")
            date_now = vDateNow
            'Loop in dataTabel For add slot
            Dim StartTime As Date = CDate(dt.Rows(0).Item("start_time").ToString).ToShortTimeString
            Dim EndTime As Date = CDate(dt.Rows(0).Item("end_time").ToString).ToShortTimeString
            Dim SlotTime As Date = StartTime
            Slot = dt.Rows(0).Item("slot")
            'Add slot
            Do
                If CDate(CDate(SlotTime).ToShortTimeString) >= CDate(CDate(vDateNow).ToShortTimeString) Then
                    AddSlotTime(CDate(SlotTime).Hour.ToString.PadLeft(2, "0") & ":" & CDate(SlotTime).Minute.ToString.PadLeft(2, "0"))
                End If
                SlotTime = DateAdd(DateInterval.Minute, Slot, SlotTime)
            Loop While CDate(SlotTime) <= CDate(EndTime)
        End If
        dt.Dispose()

        'Dim dtT As New DataTable
        'sql = "select convert(varchar(5),slot_time,114) as time,case when in_use = capacity then 1 else 0 end as status from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,GETDATE()) = 0 and slot_time > DATEADD(MINUTE," & k_before_app & ",GETDATE())"
        'dtT = getDataTable(sql, INIFileName, SoftwareName, "frmAppointment_Shown")

        Dim dtT As New DataTable
        dtT = eng.GetSlotStatus(k_before_app, INIFileName, SoftwareName, "frmAppointment_Shown")
        If dtT.Rows.Count > 0 Then
            For i As Int32 = 0 To dtT.Rows.Count - 1
                If dtT.Rows(i).Item("status") = 0 Then
                    FLP.Controls(dtT.Rows(i).Item("time")).Text = IIf(Language = "TH", "ว่าง", "available")
                    FLP.Controls(dtT.Rows(i).Item("time")).BackColor = Color.FromArgb(&HA8, &HCB, &H67)
                    FLP.Controls(dtT.Rows(i).Item("time")).ForeColor = Color.White
                Else
                    FLP.Controls(dtT.Rows(i).Item("time")).BackColor = Color.DimGray
                End If
            Next
        End If
        dtT.Dispose()
        eng = Nothing
    End Sub

    Sub AddSlotTime(ByVal Time As String)
        Dim ret As Boolean = False
        For i As Int32 = 0 To FLP.Controls.Count - 1
            If FLP.Controls(i).Name = Time Then
                ret = True
            End If
        Next
        If ret = True Then Exit Sub

        Dim lbl1 As New Label
        Dim lbl2 As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 40, FontStyle.Bold, GraphicsUnit.Pixel)
        With lbl1
            .AutoSize = False
            .Width = 430
            .Height = 50
            .Name = "T_" & Time
            .Font = Font
            .Text = Time & IIf(Language = "TH", " น.", "")
            .BackColor = Color.White
            .ForeColor = Color.Green
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
        End With
        FLP.Controls.Add(lbl1)
        With lbl2
            .Font = Font
            .AutoSize = False
            .Width = 200
            .Height = 50
            .Name = Time
            .Tag = "btn"
            .BackColor = Color.LightGray
            .TextAlign = ContentAlignment.MiddleCenter
            .BorderStyle = BorderStyle.FixedSingle
        End With
        FLP.Controls.Add(lbl2)
        AddHandler lbl2.Click, AddressOf CheckStatus
    End Sub

    Private Sub CheckStatus(ByVal Sender As Object, ByVal e As EventArgs)
        Dim btn As Label = Sender
        If btn.Text <> "" Then
            For x As Int32 = 0 To FLP.Controls.Count - 1
                If FLP.Controls(x).BackColor = Color.DarkRed Then
                    FLP.Controls(x).BackColor = Color.FromArgb(&HA8, &HCB, &H67)
                    FLP.Controls(x).Text = IIf(Language = "TH", "ว่าง", "available")
                End If
            Next

            btn.BackColor = Color.DarkRed
            btn.ForeColor = Color.White
        End If

    End Sub

    Private Sub TimerEnd_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerEnd.Tick
        TimerEnd.Enabled = False
        frmRegister.BringToFront()
        Me.Close()
    End Sub

    Private Sub btnTH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Language = "TH"
        ChangeLanguage()
        FLP.Controls.Clear()
        frmAppointment_Shown(Nothing, Nothing)
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Language = "EN"
    '    ChangeLanguage()
    '    FLP.Controls.Clear()
    '    frmAppointment_Shown(Nothing, Nothing)
    'End Sub

  
End Class