Imports QueueSharp.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG
Imports System.IO
Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class frmMain
    Public CheckCam As Boolean = False
    Sub CloseAllChildForm()
        'Dim Form As Form
        'For Each Form In Me.MdiChildren
        '    If Form.Visible Then
        '        Form.Close()
        '    End If
        'Next
        For i As Integer = My.Application.OpenForms.Count - 1 To 0 Step -1
            If My.Application.OpenForms.Item(i) IsNot Me Then
                My.Application.OpenForms.Item(i).Close()
            End If
        Next i
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            UpdateCounterStatus(myUser.counter_id, False)
            'Close Com Unitdisplay
            ClearUnitDisplay(False)
            Dim sql As String = ""
            If myUser.user_id <> "" Then
                sql = "update TB_user set counter_id = 0,item_id = 0, ip_address=null where id =" & myUser.user_id
                executeSQL(sql)
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Function CheckServerIP() As Boolean
        'ตรวจสอบว่า IP ของเครือง MainServer กับ DisplayServer จะต้องไม่ใช่ IP เดียวกัน
        Dim ret As Boolean = False
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        If ini.ReadString("Server") = ini.ReadString("Server1") Then
            ret = True
        End If
        ini = Nothing
        Return ret
    End Function

    Private Sub CheckFileList()
        If File.Exists(Application.StartupPath & "\QM\QueueSharp.ini") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\QueueSharp.ini", True)
        End If

        Dim ini As New IniReader(Application.StartupPath & "\QM\QueueSharp.ini")
        ini.Section = "Setting"
        Dim qQM As String = GetShopConfig(INIFileName, "q_qm", "QueueSharp", "frmMain_CheckFileList")
        If qQM.Trim = "Y" Then
            If File.Exists(ini.ReadString("CamConfig")) = False Then
                showFormError("ไม่พบไฟล์ " & ini.ReadString("CamConfig"), False)
                'If File.Exists(Application.StartupPath & "\QM\Configuration\ConfigInput.exe") = True Then
                '    Shell(Application.StartupPath & "\QM\Configuration\ConfigInput.exe", AppWinStyle.NormalFocus, True)
                'End If
            End If
        End If
        ini = Nothing

        If File.Exists(Application.StartupPath & "\QM\AndreaElectronicsUSBAudio.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\AndreaElectronicsUSBAudio.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\Avisynth_258.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\Avisynth_258.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\avs2avi.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\avs2avi.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\avs2avi.rar") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\avs2avi.rar", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\ffmpeg.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\ffmpeg.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\ffmpeg.rar") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\ffmpeg.rar", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\QM.txt") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\QM.txt", True)
        End If

        'If File.Exists(Application.StartupPath & "\QM\QMLaunch.exe") = False Then
        '    showFormError("ไม่พบไฟล์ QMLaunch.exe", True)
        'End If

        If File.Exists(Application.StartupPath & "\QM\QualityManagement.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\QualityManagement.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\TextQueueNo.txt") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\TextQueueNo.txt", True)
        End If

        If File.Exists(Application.StartupPath & "\QM\Xvid-1.3.2-20110601.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QM\Xvid-1.3.2-20110601.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\alert.png") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\alert.png", True)
        End If

        If File.Exists(Application.StartupPath & "\QMPreview.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QMPreview.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QMSchedule.exe") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QMSchedule.exe", True)
        End If

        If File.Exists(Application.StartupPath & "\QSharp.ico") = False Then
            showFormError("ไม่พบไฟล์ " & Application.StartupPath & "\QSharp.ico", True)
        End If
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If CheckServerIP() = True Then
            showFormError("การตั้งค่าการเชื่อมต่อฐานข้อมูลไม่ถูกต้อง" & vbCrLf & "กรุณาตรวจสอบการตั้งค่าก่อนการใช้งาน", False)
        End If

        Dim ChkDB As String = CheckConnDbServer(INIFileName)
        If ChkDB.Trim <> "" Then
            showFormError(ChkDB, False)
        End If

        'Dim CurrDB As String = GetShopConfig(INIFileName, "CurrentDB", "QueueSharp", "frmMain_Shown")
        'If CurrDB.Trim = "" Then
        '    showFormError("ไม่พบการตั้งค่า  CurrentDB ใน TB_SETTING", True)
        '    Application.Exit()
        'End If


        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Dim MainServerIP As String = ini.ReadString("Server")
        Dim DisplayServerIP As String = ini.ReadString("Server1")
        ini.Section = "QMFtp"
        Dim FtpHost As String = ini.ReadString("FtpHost")
        If DisplayServerIP <> FtpHost Then
            showFormError("การตั้งค่า  QMFtpHost ใน QueueSharp.ini ไม่ถูกต้อง อาจมีปัญหาทำให้ File VDO ถูก Transfer ไปเก็บยัง Shop อื่น", True)
            ini = Nothing
            Application.Exit()
        End If
        ini = Nothing

        CheckFileList()

        mRibbon.RibbonControl.ColorScheme = mRibbon.ColorScheme.Blue
        UpdateVersion_Company()

        ' ''************** Lincese ************
        'Dim ini As New IniReader(INIFileName)
        'ini.Section = "SETTING"
        'Dim score As New ScoreLicense.ScoreLicense(ApplicationName)
        'If Not score.IsValidLicense(ini.ReadString("License")) Then
        '    Me.Refresh()
        '    Dim f As New frmLicense
        '    If f.ShowDialog() = Windows.Forms.DialogResult.No Then
        '        Application.Exit()
        '    End If
        'End If
        ''***********************************

        If CheckConn("frmMain_Shown", False, "") = False Then
            TimerCheckStatus.Enabled = False
            TimerCheckConnection.Enabled = False
            timerCheckForce.Enabled = False
            timerForce.Enabled = False
            Dim f As New frmConfigDatabase
            f.BeginProgram = True
            If f.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Application.Exit()
            End If
        End If

        Dim sql As String = ""
        Dim dt As New DataTable
        sql = ""
        Dim ShopName As String = ""
        sql = "select config_name,config_value from TB_SETTING where config_name = 's_name_en'"
        dt = getDataTable(sql)
        ShopName = dt.Rows(0).Item("config_value").ToString
        Me.Text = Me.Text & "  ( " & ShopName & " ) Main IP:" & MainServerIP & " Display IP:" & DisplayServerIP
        dt.Dispose()

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        'แสดงหน้าจอ Login
        Dim ff As New frmLogin
        ff.ExitApp = True
        If ff.ShowDialog() = Windows.Forms.DialogResult.Yes Then
            TimerChkLogon.Enabled = True
            'If ff.Serve = True Then
            Dim f As New frmServiceQueue
            f.MdiParent = Me
            f.Show()

            If CheckCam = True Then
                TimerCheckCamera.Enabled = True
            End If
        End If
        ff.Close()
    End Sub

    Private Sub TimerCheckCamera_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckCamera.Tick
        'Dim ChkCamAgent As Boolean = False
        'Dim ChkCamCustomer As Boolean = False
        Try
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"
            If File.Exists(ini.ReadString("CamConfig")) = True Then
                Dim qQM As String = GetShopConfig(INIFileName, "q_qm", "QueueSharp", "frmMain_CheckFileList")
                If qQM.Trim = "Y" Then
                    'ตรวจสอบการทำงานของกล้อง
                    Dim ConfigQM As New IniReader(ini.ReadString("CamConfig"))

                    Dim AgentInputDevice As String = ConfigQM.ReadString("Agent", "AgentCameraInput")
                    Dim CustomerInputDevice As String = ConfigQM.ReadString("Customer", "CustomerCameraInput")

                    If AgentInputDevice.Trim <> "" And CustomerInputDevice.Trim <> "" Then
                        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

                        If IO.Directory.Exists(Application.StartupPath & "\QM\Temp") = False Then
                            IO.Directory.CreateDirectory(Application.StartupPath & "\QM\Temp")
                        End If

                        Dim CheckCustomerCam As Boolean = False
                        Dim CheckAgentCam As Boolean = False
                        Dim AgentCam As New HostWebcam
                        Dim CustomerCam As New GuestWebcam

                        Try
                            CheckCustomerCam = CustomerCam.InitializeGuestWebcam(CustomerInputDevice, QM.CheckQid)
                        Catch ex As Exception
                            CheckCustomerCam = False
                        End Try
                        Try
                            CheckAgentCam = AgentCam.InitializeHostWebcam(AgentInputDevice, QM.CheckQid)
                        Catch ex As Exception
                            CheckAgentCam = False
                        End Try

                        If CheckAgentCam = False And CheckCustomerCam = False Then
                            showNotify("QM", "กรุณาตรวจสอบการทำงานของกล้องพนักงานและกล้องลูกค้า")
                            SaveQueryErrorLog("QueueSharp.frmMain.TimerCheckCamera.Tick กรุณาตรวจสอบการทำงานของกล้องพนักงานและกล้องลูกค้า", "")
                        Else
                            If CheckAgentCam = False Then
                                showNotify("QM", "กรุณาตรวจสอบการเชื่อมต่อของกล้องพนักงาน")
                                SaveQueryErrorLog("QueueSharp.frmMain.TimerCheckCamera.Tick กรุณาตรวจสอบการเชื่อมต่อของกล้องพนักงาน", "")
                            Else
                                AgentCam.HostStart()
                            End If

                            If CheckCustomerCam = False Then
                                showNotify("QM", "กรุณาตรวจสอบการเชื่อมต่อของกล้องลูกค้า")
                                SaveQueryErrorLog("QueueSharp.frmMain.TimerCheckCamera.Tick กรุณาตรวจสอบการเชื่อมต่อของกล้องลูกค้า", "")
                            Else
                                CustomerCam.GuestStart()
                            End If
                        End If

                        If AgentCam.CheckHost = True Then
                            AgentCam.HostStop()
                            AgentCam.HostDispose()
                        End If
                        If CustomerCam.CheckGuest = True Then
                            CustomerCam.GuestStop()
                            CustomerCam.GuestDispose()
                        End If

                        AgentCam = Nothing
                        CustomerCam = Nothing

                        If File.Exists(Application.StartupPath & "\QM\Temp\" & QM.CheckQid & "A.avi") = True Then
                            File.SetAttributes(Application.StartupPath & "\QM\Temp\" & QM.CheckQid & "A.avi", FileAttributes.Normal)
                            File.Delete(Application.StartupPath & "\QM\Temp\" & QM.CheckQid & "A.avi")
                        End If
                        If File.Exists(Application.StartupPath & "\QM\Temp\" & QM.CheckQid & "B.avi") = True Then
                            File.SetAttributes(Application.StartupPath & "\QM\Temp\" & QM.CheckQid & "B.avi", FileAttributes.Normal)
                            File.Delete(Application.StartupPath & "\QM\Temp\" & QM.CheckQid & "B.avi")
                        End If
                    End If
                End If
            End If
            ini = Nothing
        Catch ex As Exception

        End Try
        TimerCheckCamera.Enabled = False
    End Sub

    Private Sub RbAboutBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAbout.Click
        frmAboutBox.ShowDialog()
    End Sub

    Private Sub tButLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLogout.Click
        timerCheckForce.Enabled = False
        timerForce.Enabled = False
        TimerCheckStatus.Enabled = False
        TimerCheckBackupQM.Enabled = False
        Dim f As New frmReason
        f.Reason = 2
        If f.ShowDialog = Windows.Forms.DialogResult.Yes Then
            Dim sql As String = ""
            sql = "update TB_user set counter_id = 0 ,item_id = 0, ip_address=null where id =" & myUser.user_id
            executeSQL(sql)
            CloseAllChildForm()
            UpdateCounterStatus(myUser.counter_id, False)
            Me.Text = Me.Text.Replace(myUser.fulllname, "-")

            QM.CloseQMSchedule()

            Dim ff As New frmLogin
            ff.ExitApp = True
            If ff.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                Dim fff As New frmServiceQueue
                fff.MdiParent = Me
                fff.Show()

                If CheckCam = True Then
                    TimerCheckCamera.Enabled = True
                End If
            End If
            ff.Close()
        Else
            timerCheckForce.Enabled = True
        End If

    End Sub

    Private Sub RbDbCfg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDbConfig.Click
        CloseAllChildForm()
        Dim f As New frmConfigDatabase
        f.ShowDialog()
    End Sub

    Private Sub RibbonButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrinterConfig.Click
        Dim f As New frmConfigPrinter
        f.ShowDialog()
    End Sub

    Private Sub BtnCusType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCusType.Click
        CloseAllChildForm()
        Dim f As New frmCustomerType
        f.ShowDialog()
    End Sub

    Private Sub BtnItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnItem.Click
        CloseAllChildForm()
        Dim f As New frmService
        f.ShowDialog()
    End Sub

    Private Sub BtnRoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCounter.Click
        CloseAllChildForm()
        Dim f As New frmCounter
        f.ShowDialog()
    End Sub

    Private Sub BtnPriority_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPriorityItem.Click
        CloseAllChildForm()
        Dim f As New frmPriority
        f.ShowDialog()
    End Sub



    Private Sub btnItemType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CloseAllChildForm()
        Dim f As New frmServiceType
        f.ShowDialog()
    End Sub

    Private Sub BtnServe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnServe.Click
        CloseAllChildForm()
        Dim f As New frmServiceQueue
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub btnPriorityCustomerType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPriorityCustomerType.Click
        CloseAllChildForm()
        Dim f As New frmItemOrder
        f.ShowDialog()
    End Sub

    Private Sub btnNotifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotifier.Click
        If LastService.LastQueue <> "" Then
            If LastService.LastRoom <> "" Then
                showNotify("หมายเลขคิว : " & LastService.LastQueue, "บริการต่อไป : " & LastService.LastService & vbCrLf & LastService.LastRoom)
            Else
                showNotify("หมายเลขคิว : " & LastService.LastQueue, "จบการบริการทั้งหมดของระบบ")
            End If
        End If
    End Sub

    Private Sub BtnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHistory.Click
        CloseAllChildForm()
        Dim f As New frmDailyHistory
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BtnAwaiting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAwaiting.Click
        CloseAllChildForm()
        Dim f As New frmAwaitingCustomer
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BtnRoomStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCounterStatus.Click
        CloseAllChildForm()
        Dim f As New frmRoomStatus
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BtnCusInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCusInfo.Click
        CloseAllChildForm()
        Dim f As New frmCustomerInfo
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BtnGroupUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGroupUser.Click
        CloseAllChildForm()
        Dim f As New frmGroupUser
        f.ShowDialog()
    End Sub

    Private Sub BtnUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUser.Click
        CloseAllChildForm()
        Dim f As New frmUser
        f.ShowDialog()
    End Sub

    Private Sub btnCustomerTypeService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWorkFlow.Click
        CloseAllChildForm()
        Dim f As New frmWorkFlow
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub btnCusEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCusEdit.Click
        CloseAllChildForm()
        Dim f As New frmEditServiceCustomer
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub btnUnitDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitDisplay.Click
        Dim f As New frmConfigUnitDisplay
        f.ShowDialog()
    End Sub

    Private Sub TimerBeep_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Console.Beep(2000, 500)
    End Sub

    Private Sub btnCusAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCusAdd.Click
        Dim f As New frmAddServiceCustomer
        f.ShowDialog()
    End Sub

    Private Sub btnSchedulText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedulText.Click
        CloseAllChildForm()
        Dim f As New frmSchedulText
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub btnEmergency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmergency.Click
        CloseAllChildForm()
        Dim f As New frmEmergencyText
        f.ShowDialog()
    End Sub

    Private Sub btnRemote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemote.Click
        CloseAllChildForm()
        Dim f As New frmChannelSelect
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub btnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowReport.Click
        If System.IO.File.Exists(Windows.Forms.Application.StartupPath & "\Report.exe") Then
            Dim proc As New Process()
            proc.StartInfo.FileName = Windows.Forms.Application.StartupPath & "\Report.exe"
            proc.StartInfo.Arguments = ""
            proc.Start()
        Else
            MessageBox.Show("ไม่พบข้อมูลรายงาน !!!" & vbCrLf & Windows.Forms.Application.StartupPath & "\Report.exe", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnLogoutReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogoutReason.Click
        CloseAllChildForm()
        Dim f As New frmLogoutReason
        f.ShowDialog()
    End Sub

    Private Sub btnHoldReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHoldReason.Click
        CloseAllChildForm()
        Dim f As New frmHoldReason
        f.ShowDialog()
    End Sub

    Private Sub btnSkill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkill.Click
        CloseAllChildForm()
        Dim f As New frmSkill
        f.ShowDialog()
    End Sub

    Private Sub btnSegment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSegment.Click
        CloseAllChildForm()
        Dim f As New frmSegment
        f.ShowDialog()
    End Sub

    Private Sub TimerChkLogon_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerChkLogon.Tick
        Try
            Dim sql As String = ""
            sql = "update TB_USER set check_update = GETDATE() where id = " & myUser.user_id
            executeSQL(sql, False)
        Catch ex As Exception : End Try
    End Sub

    Private Sub btnSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetting.Click
        CloseAllChildForm()
        Dim f As New frmSetting
        f.ShowDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Minimized
        Rbbn.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
        Rbbn.Focus()
    End Sub

    Private Sub TimerCheckHoldRoom_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckHoldRoom.Tick
        TimerCheckHoldRoom.Enabled = False
        Dim f As New frmLogin
        f.ExitApp = True
        f.cbCounter.Enabled = False
        f.txtUserName.Enabled = False
        f.CheckCounter.Enabled = False
        If f.ShowDialog() = Windows.Forms.DialogResult.Yes Then
            TimerChkLogon.Enabled = True
            Dim ff As New frmServiceQueue
            ff.MdiParent = Me
            ff.Show()
        End If
        f.Close()
    End Sub

    Private Sub btnAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppointment.Click
        CloseAllChildForm()
        Dim f As New frmAppointment
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub TimerCheckConnection_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckConnection.Tick
        'Engine.Common.ShopConnectDBENG.CheckCurrentActiveDB(INIFileName, Conn, "QueueSharp", "frmMain_TimerCheckConnection_Tick")
    End Sub

    Private Sub btnForce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
        CloseAllChildForm()
        Dim f As New frmForce
        f.ShowDialog()
    End Sub

    Private Sub TimerCheckStatus_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckStatus.Tick
        Try
            If myUser.user_id > 0 Then
                Dim sql As String = ""
                Dim dt As New DataTable
                sql = "select counter_id from tb_user where id = " & myUser.user_id & " and counter_id = 0"
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    CloseAllChildForm()
                    Dim f As New frmLogin
                    f.ExitApp = True
                    If f.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Dim ff As New frmServiceQueue
                        ff.MdiParent = Me
                        ff.Show()
                    End If
                    f.Close()
                End If
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub BtnAppointmentCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAppointmentCustomer.Click
        CloseAllChildForm()
        Dim f As New frmAppointmentCustomer
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub timerCheckForce_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerCheckForce.Tick
        timerCheckForce.Enabled = False
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select wait from TB_FORCE_SCHEDULE where DATEDIFF(D,force_date,GETDATE()) = 0 and GETDATE() between start_time and dateadd(minute,slot,end_time)"
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                If timerForce.Enabled = False Then
                    timerForce.Interval = dt.Rows(0).Item("wait") * 1000
                    timerForce.Enabled = True
                End If
            Else
                timerForce.Enabled = False
            End If
            dt.Dispose()
        Catch ex As Exception

        End Try
        timerCheckForce.Enabled = True
    End Sub

    Private Sub timerForce_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerForce.Tick
        timerForce.Enabled = False
        Dim sql As String = ""
        Dim dt As New DataTable
        If myUser.counter_id > 0 And myUser.user_id > 0 Then
            'ห้องที่เป็น back office หรือ counter manager
            sql = "select * from tb_counter where id = " & myUser.counter_id & " and active_status = 1 and (back_office = 1 or counter_manager = 1)"
            dt = getDataTable(sql)
            If dt.Rows.Count = 0 Then
                'ถ้าไม่ใช่ห้องที่เป็น back office หรือ counter manager
                sql = "select isnull(counter_id,0) as counter_id from tb_user where id = " & myUser.user_id
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    If CInt(dt.Rows(0).Item("counter_id")) > 0 Then
                        sql = "select * from tb_counter_queue where datediff(d,getdate(),service_date)=0 and status in (2,4) and user_id = " & myUser.user_id & " and counter_id = " & myUser.counter_id
                        dt = getDataTable(sql)
                        If dt.Rows.Count = 0 Then
                            sql = "declare @Custype as int; select @Custype = (select customertype_id from TB_COUNTER_CUSTOMERTYPE where counter_id = " & myUser.counter_id & "); select * from TB_CUSTOMERTYPE where app = 1 and id = @Custype"
                            dt = getDataTable(sql)
                            If dt.Rows.Count > 0 Then
                                'Appointment
                                sql = "declare @Custype as int;"
                                sql += " select @Custype = (select customertype_id from TB_COUNTER_CUSTOMERTYPE where counter_id = " & myUser.counter_id & ");"
                                sql += " select * "
                                sql += " from tb_counter_queue "
                                sql += " where datediff(d,getdate(),service_date)=0 and status = 1 "
                                sql += " and customertype_id = @Custype and hold <= GETDATE()"
                            Else
                                sql = "declare @Custype as int;"
                                sql += " select @Custype = (select customertype_id from TB_COUNTER_CUSTOMERTYPE where counter_id = " & myUser.counter_id & ");"
                                sql += " select * "
                                sql += " from tb_counter_queue "
                                sql += " where datediff(d,getdate(),service_date)=0 and status = 1 "
                                sql += " and customertype_id = @Custype"

                                Dim pItemID As Int32 = GetPrimaryService(myUser.user_id)
                                If pItemID > 0 Then
                                    sql += " and item_id = " & pItemID
                                End If
                            End If

                            dt = getDataTable(sql)
                            If dt.Rows.Count > 0 Then
                                CloseAllChildForm()
                                AutoForceQueue = True
                                Dim f As New frmServiceQueue
                                f.MdiParent = Me
                                f.Show()
                            End If
                        End If
                    End If
                End If
            End If
        End If
        timerForce.Enabled = True

        'End If
    End Sub

    Private Sub btnMDSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CloseAllChildForm()
        Dim f As New frmMDSetting
        f.ShowDialog()
    End Sub

    Private Sub RibbonButton80_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQSetting.Click
        CloseAllChildForm()
        Dim f As New frmQSetting
        f.ShowDialog()
    End Sub

    Dim QMScheduleTransferBackup As String = ""
    Dim LastQmScheculeRefreshTime As DateTime = DateTime.Now
    Private Sub TimerCheckBackupQM_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckBackupQM.Tick
        TimerCheckBackupQM.Enabled = False
        QMScheduleTransferBackup = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "QMScheduleTransferBackup", "QueueSharp", "frmScheduleQM_Load")
        If QMScheduleTransferBackup.Trim = "" Then
            QMScheduleTransferBackup = "15"
        End If
        If DateAdd(DateInterval.Minute, Convert.ToDouble(QMScheduleTransferBackup), LastQmScheculeRefreshTime) < DateTime.Now Then
            QM.StartQMSchedule()

            QMScheduleTransferBackup = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "QMScheduleTransferBackup", "QueueSharp", "QMSchedule.frmScheduleQM.TimerStartTransferFileQM")
            LastQmScheculeRefreshTime = DateTime.Now
        End If


        TimerCheckBackupQM.Enabled = True
    End Sub
End Class
