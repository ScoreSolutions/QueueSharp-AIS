Imports System.Drawing.Printing
Imports Kiosk.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG
Imports Engine.Kiosk
Imports Engine.Kiosk.KioskModule
Imports System.IO


Public Class frmRegister

    Dim PrintQueue As KioskRegisterENG.Print
    'Public Structure Print
    '    Dim QueueNo As String
    '    Dim Mobile As String
    '    Dim WaitingTime As String
    '    Dim ListService As String
    '    Dim CountQueue As String
    '    Dim AppTime As String
    'End Structure


    Private Sub frmRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetLogo()
    End Sub

    Sub SetLogo()
        Try
            Dim logopath As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "LogoKioskPath", SoftwareName, "frmRegister.SetLogo")
            If File.Exists(logopath) = False Then
                logopath = Application.StartupPath & "\Images\logo_kiosk.png"
            End If

            PictureBox1.BackgroundImage = Image.FromFile(logopath)
        Catch ex As Exception
            'MessageBox.Show(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "1"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "1")
        eng = Nothing
    End Sub
    

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "2"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "2")
        eng = Nothing
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "3"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "3")
        eng = Nothing
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "4"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "4")
        eng = Nothing
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "5"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "5")
        eng = Nothing
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "6"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "6")
        eng = Nothing
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "7"
        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "7")
        eng = Nothing
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "8"
        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "8")
        eng = Nothing
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "9"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "9")
        eng = Nothing
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click
        'CountVDO = 0
        'If txtMobile.Text.Length = k_len Then Exit Sub
        'txtMobile.Text = txtMobile.Text & "0"

        Dim eng As New KioskRegisterENG
        eng.ClickNumber(txtMobile, "0")
        eng = Nothing
    End Sub


    Private Function CheckTodayAppointment(ByVal Mobile As String) As Boolean
        Dim ret As Boolean = False
        '************** ตรวจสอบข้อมูลการนัดของวันนี้ ***************
        'Appointment
        Dim Late As Boolean = False
        Dim MsgFooter As String = ""
        Dim eng As New KioskRegisterENG
        Dim dt As New DataTable
        dt = eng.GetTodayAppointment(Mobile, k_late, INIFileName, SoftwareName, "frmRegister_CheckTodayAppointment")
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "active_status, app_time desc"
            Dim StartSlot As String = dt.DefaultView(0)("app_time").ToString   'เวลาที่ทำรายการจองมาครั้งล่าสุด

            ''ถ้าวันนี้มีการจองแล้วสถานะเป็น No Show แล้ว ให้แสดง Popup แจ้งว่าคิวได้ถูกยกเลิกแล้ว แค่ครั้งแรกเท่านั้น
            ''ครั้งต่อๆ ไปในวันนี้ถ้ามีกดอีก ก็ไม่ต้องแสดง Popup นี้อีก
            Dim qDt As New DataTable
            qDt = eng.GetQueueToday(StartSlot, Mobile, INIFileName, SoftwareName, "frmRegister_CheckTodayAppointment")
            If qDt.Rows.Count > 0 Then
                qDt.Dispose()
                dt.Dispose()
                ret = False
            Else
                dt.DefaultView.RowFilter = "regis_time < 0 and app_time = '" & StartSlot & "'"   'ถ้าจองแล้วมาสาย
                If dt.DefaultView.Count > 0 Then
                    Late = True
                    MsgFooter = msgQCancelled
                Else
                    dt.DefaultView.RowFilter = "active_status='1'" 'ถ้าสถานะเป็น confirm
                    If dt.DefaultView.Count > 0 Then
                        MsgFooter = msgWantToBeServedNow
                    End If
                End If

                Dim ListItem As String = ""
                dt.DefaultView.RowFilter = "app_time = '" & StartSlot & "'"
                For Each dr As DataRowView In dt.DefaultView
                    If ListItem = "" Then
                        ListItem = dr(itemname_field).ToString
                    Else
                        ListItem = ListItem & vbCrLf & dr(itemname_field).ToString
                    End If
                Next

                '****************************************************************
                PanelMsg.Visible = True
                btnMain.Enabled = False
                btnOK.Enabled = False
                btnEdit.Enabled = False

                Dim vDateNow As DateTime = FindDateNow(INIFileName, SoftwareName, "frmRegister_CheckTodayAppointment")
                If Language = "TH" Then
                    lblMsg.Text = Mobile & vbCrLf & vbCrLf & msgHasAppointment & vbCrLf & vbCrLf & ListItem & vbCrLf & vbCrLf & msgDateon & vDateNow.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH")) & msgTimeAt & dt.Rows(0).Item("app_time").ToString & msgAtclock & vbCrLf & vbCrLf & MsgFooter
                Else
                    lblMsg.Text = Mobile & vbCrLf & vbCrLf & msgHasAppointment & vbCrLf & ListItem & vbCrLf & vbCrLf & msgDateon & vDateNow.ToString("dd/MM/yy", New System.Globalization.CultureInfo("en-US")) & msgTimeAt & CDate(dt.Rows(0).Item("app_time")).ToString("hh:mm tt") & vbCrLf & vbCrLf & MsgFooter
                End If
                btnAppBack.Width = 90
                If Late = True Then
                    btnAppBack.Width = 120
                    btnAppNew.Visible = True
                    btnMain.Enabled = True
                    btnAppCancel.Visible = False
                    btnAppOK.Visible = False
                Else
                    btnAppNew.Visible = False
                    If k_cancel > (dt.Rows(0).Item("regis_time") - k_late) Then   'ตอนที่คิวรี่มามีบวกเวลา Late ไว้ด้วย ถึงตอนนี้ต้องลบเวลา Late ออก
                        btnAppCancel.ForeColor = Color.DarkGray
                        btnAppCancel.Enabled = False
                        btnAppCancel.Visible = True
                        btnAppOK.Visible = True
                    Else
                        btnAppNew.Visible = False
                        btnAppCancel.Visible = True
                        btnAppOK.Visible = True
                        btnAppCancel.ForeColor = Color.Black
                        btnAppCancel.Enabled = True
                    End If
                End If

                CheckCustomerProfile(Mobile) 'ทำการ Update Customer Profile
                CheckCustomerAccountBalance(Mobile)
                CheckCustomerCampange(Mobile, AssetID, NetworkType)
                ret = True
                '**************************************************
            End If
        End If
        '*********************************************

        Return ret
    End Function

    Sub CheckCustomerCampange(ByVal vMobile As String, ByVal vAssetID As String, ByVal vNetworkType As String)
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Dim eng As New KioskBaseENG
        Try
            Dim FindCustomerInfo As New ShopWebServiceMain.WebServiceAPI
            Dim CustomerInfo As New ShopWebServiceMain.TbCustomerShParaDB
            FindCustomerInfo.Timeout = 5000
            FindCustomerInfo.Url = m_webservice_url
            CustomerInfo = FindCustomerInfo.GetCampaign(vMobile, vAssetID, vNetworkType)
            Campaign_TH = CustomerInfo.CAMPAIGN_CODE.ToString
            Campaign_Desc1_TH = CustomerInfo.CAMPAIGN_NAME.ToString
            Campaign_Desc2_TH = CustomerInfo.CAMPAIGN_DESC_TH2.ToString
            Campaign_EN = CustomerInfo.CAMPAIGN_NAME_EN.ToString
            Campaign_Desc1_EN = CustomerInfo.CAMPAIGN_DESC.ToString
            Campaign_Desc2_EN = CustomerInfo.CAMPAIGN_DESC_EN2.ToString

            eng.UpdateCP(CustomerInfo.CAMPAIGN_CODE, CustomerInfo.CAMPAIGN_NAME, CustomerInfo.CAMPAIGN_NAME_EN, CustomerInfo.CAMPAIGN_DESC, CustomerInfo.CAMPAIGN_DESC_TH2, CustomerInfo.CAMPAIGN_DESC_EN2, Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerCampange")

        Catch ex As Exception
            Try
                Dim FindCustomerInfo As New ShopWebServiceDisplay.WebServiceAPI
                Dim CustomerInfo As New ShopWebServiceDisplay.TbCustomerShParaDB
                FindCustomerInfo.Timeout = 5000
                FindCustomerInfo.Url = d_webservice_url
                CustomerInfo = FindCustomerInfo.GetCampaign(vMobile, vAssetID, vNetworkType)
                Campaign_TH = CustomerInfo.CAMPAIGN_CODE.ToString
                Campaign_Desc1_TH = CustomerInfo.CAMPAIGN_NAME.ToString
                Campaign_Desc2_TH = CustomerInfo.CAMPAIGN_DESC_TH2.ToString
                Campaign_EN = CustomerInfo.CAMPAIGN_NAME_EN.ToString
                Campaign_Desc1_EN = CustomerInfo.CAMPAIGN_DESC.ToString
                Campaign_Desc2_EN = CustomerInfo.CAMPAIGN_DESC_EN2.ToString

                eng.UpdateCP(CustomerInfo.CAMPAIGN_CODE, CustomerInfo.CAMPAIGN_NAME, CustomerInfo.CAMPAIGN_NAME_EN, CustomerInfo.CAMPAIGN_DESC, CustomerInfo.CAMPAIGN_DESC_TH2, CustomerInfo.CAMPAIGN_DESC_EN2, Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerCampange")
            Catch ey As Exception : End Try
        End Try
        eng = Nothing
        ini = Nothing
    End Sub

    Sub CheckCustomerAccountBalance(ByVal vMobileNo As String)
        If Mobile <> "" Then
            Dim eng As New KioskBaseENG
            Try
                Dim FindCustomerInfo As New ShopWebServiceMain.WebServiceAPI
                Dim CustomerInfo As New ShopWebServiceMain.TbCustomerShParaDB
                FindCustomerInfo.Timeout = 5000
                FindCustomerInfo.Url = m_webservice_url
                CustomerInfo = FindCustomerInfo.GetArBalance(vMobileNo)

                Dim sql As String = ""
                If CustomerInfo.ACCOUNT_BALANCE <> "" Then
                    eng.UpdateAR(CustomerInfo.ACCOUNT_BALANCE, Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerAccountBalance")
                Else
                    eng.UpdateAR("", Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerAccountBalance")
                End If
            Catch ex As Exception
                CheckCustomerAccountBalanceFromDisplay(vMobileNo)
            End Try
            eng = Nothing
        End If
    End Sub

    Sub CheckCustomerAccountBalanceFromDisplay(ByVal vMobileNo As String)
        If Mobile <> "" Then
            Dim eng As New KioskBaseENG
            Try
                Dim FindCustomerInfo As New ShopWebServiceDisplay.WebServiceAPI
                Dim CustomerInfo As New ShopWebServiceDisplay.TbCustomerShParaDB
                FindCustomerInfo.Url = d_webservice_url
                FindCustomerInfo.Timeout = 5000
                CustomerInfo = FindCustomerInfo.GetArBalance(vMobileNo)
                Dim sql As String = ""
                If CustomerInfo.ACCOUNT_BALANCE <> "" Then
                    eng.UpdateAR(CustomerInfo.ACCOUNT_BALANCE, Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerAccountBalanceFromDisplay")
                Else
                    eng.UpdateAR("", Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerAccountBalanceFromDisplay")
                End If
            Catch ey As Exception : End Try
            eng = Nothing
        End If
    End Sub

    Private Sub RegisterOKClick(ByVal IsCheckAppointment As Boolean)
        Dim sql As String = ""
        Dim dt As New DataTable
        CountVDO = 0
        CustomerApp = False
        CustomerAIS = False
        Segment = ""
        Campaign_TH = ""
        Campaign_EN = ""
        Campaign_Desc1_TH = ""
        Campaign_Desc2_TH = ""
        Campaign_Desc1_EN = ""
        Campaign_Desc2_EN = ""
        SMECorporateType = ""
        AssetID = ""
        NetworkType = ""
        CustomerPAGroup = ""
        CustomerURLCapture = ""
        CustomerURLCaptureDate = ""
        CustomerBOS = ""
        CustomerMobileStatus = ""
        CustomerCategory = ""
        CustomerImageID = 0
        CustomerBrandName = ""

        Mobile = txtMobile.Text
        'Dim DeBug As String = DateTime.Now.ToString("HH:mm:ss.fff") & " Start" & vbNewLine
        If txtMobile.Text.Length <> k_len Then
            If txtMobile.Text <> "" Then
                ShowDialogBox(msgVerifyYourNumber, msgWarning)
                btnOK.Enabled = True
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            Dim ret As Boolean = False
            If k_mobile1 <> "" Or k_mobile2 <> "" Or k_mobile3 <> "" Or k_mobile4 <> "" Then
                Select Case StringFromLeft(txtMobile.Text, 2)
                    Case k_mobile1
                        ret = True
                    Case k_mobile2
                        ret = True
                    Case k_mobile3
                        ret = True
                    Case k_mobile4
                        ret = True
                    Case ""
                        ret = True
                End Select
            End If
            If ret = False Then
                ShowDialogBox(msgWrongNumberFormat, msgWarning)
                Exit Sub
            End If
        End If
        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before CheckUpdateConfig" & vbNewLine
        CheckUpdateConfig(INIFileName, SoftwareName, "frmRegister_RegisterOKClick")

        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " End CheckUpdateConfig" & vbNewLine
        If Mobile <> "" Then
            If IsCheckAppointment = True Then
                If CheckTodayAppointment(Mobile) = True Then
                    btnOK.Enabled = True
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            Dim eng As New KioskRegisterENG
            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before CheckCustomerProfile" & vbNewLine
            If CheckCustomerProfile(Mobile) = True Then
                CustomerApp = True
                CustomerAIS = True
                CustomerTypeID = eng.FindCustomerType(Segment, INIFileName, SoftwareName, "frmRegister_RegisterOKClick")
                'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " End CheckCustomerProfile" & vbNewLine
            Else
                'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before SP_FindCustomerInfo" & vbNewLine

                'Connect webservice ไม่ได้ หาข้อมูลใน Database ของ Shop
                sql = "SP_FindCustomerInfo '" & Mobile & "'"
                dt = getDataTable(sql, INIFileName, SoftwareName, "frmRegister_RegisterOKClick")
                If dt.Rows.Count > 0 Then
                    'MessageBox.Show("หาข้อมูลพบว่าเป็นลูกค้าที่เคยมารับบริการ")
                    CustomerApp = True
                    'ถ้ามีข้อมูลเบอร์โทรศัพท์
                    Segment = dt.Rows(0).Item("segment_level").ToString.Trim
                    Campang = dt.Rows(0).Item("Camp_name").ToString

                    Campaign_TH = dt.Rows(0).Item("Camp_code").ToString
                    Campaign_Desc1_TH = dt.Rows(0).Item("Camp_name").ToString
                    Campaign_Desc2_TH = dt.Rows(0).Item("CAMPAIGN_DESC_TH2").ToString
                    Campaign_EN = dt.Rows(0).Item("CAMPAIGN_NAME_EN").ToString
                    Campaign_Desc1_EN = dt.Rows(0).Item("CAMPAIGN_DESC").ToString
                    Campaign_Desc2_EN = dt.Rows(0).Item("CAMPAIGN_DESC_EN2").ToString
                    Contact_ID = dt.Rows(0).Item("contact_id").ToString
                    CustomerTypeID = eng.FindCustomerType(Segment, INIFileName, SoftwareName, "frmRegister_RegisterOKClick")
                    If Convert.IsDBNull(dt.Rows(0)("pa_group")) = False Then CustomerPAGroup = dt.Rows(0)("pa_group").ToString
                    If Convert.IsDBNull(dt.Rows(0)("url_capture")) = False Then CustomerURLCapture = dt.Rows(0)("url_capture").ToString
                    If Convert.IsDBNull(dt.Rows(0)("billing_system")) = False Then CustomerBOS = IIf(dt.Rows(0)("billing_system").ToString = "BOS", "Yes", "No")
                    If Convert.IsDBNull(dt.Rows(0)("mobile_status")) = False Then CustomerMobileStatus = dt.Rows(0)("mobile_status")
                    If Convert.IsDBNull(dt.Rows(0)("category")) = False Then CustomerCategory = dt.Rows(0)("category")
                    If Convert.IsDBNull(dt.Rows(0)("brand_name")) = False Then CustomerBrandName = dt.Rows(0)("brand_name")
                Else
                    'MessageBox.Show("ไม่พบข้อมูล")
                    CustomerApp = False
                    CustomerTypeID = DefaultCustomerTypeID
                End If

                'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " End SP_FindCustomerInfo" & vbNewLine
            End If

            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before CheckCurrentAppointment" & vbNewLine
            'ตรวจสอบว่าลูกค้ามีรายการจองอยู่แล้วที่ Shop อื่นๆ หรือไม่
            If CheckCurrentAppointment(Mobile) = True Then
                CustomerApp = False
            ElseIf IsCheckAppointment = False Then
                CustomerApp = True
            End If
            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " End CheckCurrentAppointment" & vbNewLine

            If ShowCustomerCapture = "Y" Then
                If eng.CheckCapturePaGroup(CustomerPAGroup, CustomerURLCapture, INIFileName, SoftwareName, "frmRegister_RegisterOKClick") = True Then
                    If CustomerAIS = True And Contact_ID <> "" Then
                        If eng.CheckDetectCamera() = True Then
                            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before Show Customer Capture" & vbNewLine

                            'สำหรับการเรียก Form ที่ใช้ในการ Capture รูปภาพลูกค้า
                            frmKiosCapture.MobileNumber = Mobile
                            frmKiosCapture.ShowDialog()
                        End If
                    End If
                End If

            End If
            eng = Nothing
        Else
            CustomerTypeID = DefaultCustomerTypeID
        End If

        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before ChangeLanguage" & vbNewLine
        ChangeLanguage()
        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " End ChangeLanguage" & vbNewLine
        'MessageBox.Show(DeBug)

        frmChooseService.RenderObject()
        frmChooseService.RenderService()
        frmChooseService.BringToFront()
        CountVDO = 0
        txtMobile.Text = ""
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOK.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        RegisterOKClick(True)
        btnOK.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Function CheckCurrentAppointment(ByVal MobileNo As String) As Boolean
        Dim ret As Boolean = False

        Try
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
            ret = ws.CheckActiveAppointment(MobileNo)
            ws = Nothing
        Catch ex As Exception
            Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
            sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmRegister_CheckCurrentAppointment") & ", 1, getdate(),null,null,"
            sqlLog += " getdate(), 'Kiosk.frmRegister.CheckCurrentAppointment : Don''t Check Active Appointment Mobile No. " & FixDB(Mobile) & " ### Exception : " & ex.Message & "', null,'" & Engine.Common.FunctionEng.GetIPAddress & "' , '" & getMyVersion() & "')"
            executeSQL(sqlLog, INIFileName, SoftwareName, "frmRegister_CheckCurrentAppointment")

            Try
                Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                ws.Url = GetWebServiceURL(False, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
                ret = ws.CheckActiveAppointment(MobileNo)
                ws = Nothing
            Catch ey As Exception

            End Try
        End Try

        Return ret
    End Function

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        CountVDO = 0
        If txtMobile.Text.Trim <> "" Then
            If StringFromRight(txtMobile.Text, 1) = "-" Then
                txtMobile.Text = txtMobile.Text.Substring(0, (txtMobile.Text.Length - 2))
            Else
                txtMobile.Text = txtMobile.Text.Substring(0, (txtMobile.Text.Length - 1))
            End If

        End If
    End Sub

    Private Sub TimerVDO_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerVDO.Tick
        Try
            If CountVDO < MaxCountVDO Then
                CountVDO = CountVDO + 1
            Else
                TimerVDO.Enabled = False
                frmDialogMsg.Hide()
                txtMobile.Text = ""
                If frmVDO.ShowDialog = Windows.Forms.DialogResult.Yes Then
                    frmDialogMsg.Close()
                    frmVDO.Close()
                    CountVDO = 0
                    TimerVDO.Enabled = True
                End If
            End If
        Catch ex As Exception : End Try

    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If PanelMsg.Visible = True Then
            Exit Sub
        End If
        Application.Exit()
    End Sub

    Private Sub btnMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMain.Click
        txtMobile.Text = ""
        TimerVDO.Enabled = False
        If frmVDO.ShowDialog = Windows.Forms.DialogResult.Yes Then
            frmVDO.Close()
            CountVDO = 0
            TimerVDO.Enabled = True
        End If
    End Sub

    Private Sub pb_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb.MouseMove
        CountVDO = 0
    End Sub

    

    Private Sub frmRegister_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim Debug As String = "Start Debug : " & DateTime.Now.ToString("HH:mm:ss.fff") & vbNewLine
        Application.DoEvents()

        Debug += "Before LoadConfig Debug : " & DateTime.Now.ToString("HH:mm:ss.fff") & vbNewLine
        LoadConfig()
        Debug += "After LoadConfig Debug : " & DateTime.Now.ToString("HH:mm:ss.fff") & vbNewLine

        MaxCountVDO = k_vdo * 60
        Dim f As New Display
        f.Show()

        Debug += "Before DeleteQueueIfNoQueue Debug : " & DateTime.Now.ToString("HH:mm:ss.fff") & vbNewLine
        Dim eng As New KioskRegisterENG
        eng.DeleteQueueIfNoQueue(INIFileName, SoftwareName, "frmRegister_Shown")
        Debug += "After DeleteQueueIfNoQueue Debug : " & DateTime.Now.ToString("HH:mm:ss.fff") & vbNewLine

        Me.BringToFront()
        frmChooseService.Show()
        Me.BringToFront()

        Debug += "After frmChooseService Debug : " & DateTime.Now.ToString("HH:mm:ss.fff") & vbNewLine

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        'MessageBox.Show(Debug)
    End Sub


    Private Sub btnAppBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppBack.Click
        CountVDO = 0
        PanelMsg.Visible = False
        btnMain.Enabled = True
        btnOK.Enabled = True
        btnEdit.Enabled = True
    End Sub

    Private Sub btnAppNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppNew.Click
        CountVDO = 0
        PanelMsg.Visible = False
        btnMain.Enabled = True
        btnOK.Enabled = True
        btnEdit.Enabled = True
        RegisterOKClick(False)
    End Sub

    Private Sub btnAppCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppCancel.Click
        Me.Cursor = Cursors.WaitCursor
        CountVDO = 0
        PanelMsg.Visible = False

        Dim f As New frmDialogMsg
        f.lblText.Text = Mobile & vbCrLf & vbCrLf & msgCancelAppointment
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

        Dim eng As New KioskRegisterENG
        Select Case f.ShowDialog
            Case Windows.Forms.DialogResult.Yes
                CountVDO = 0
                msgMobile = Mobile
                PanelMsg.Visible = False
                Dim ff As New frmDialogBeforeClose
                ff.Message = msgCancelAppointmentCompleted1 & Mobile & msgCancelAppointmentCompleted2
                ff.ShowDialog()
                Dim sql As String = ""
                Dim dt As New DataTable
                Dim rec As Int32 = 1
                Dim AppSlot As String = ""
                'sql = "select count(1) as rec from TB_APPOINTMENT_CUSTOMER where DATEDIFF(D,start_slot,GETDATE()) = 0 and customer_id = '" & Mobile & "' and active_status = 1 group by start_slot"
                'dt = getDataTable(sql, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")
                dt = eng.CheckAppointmentCustomer(Mobile, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")
                If dt.Rows.Count > 0 Then
                    rec = CInt(dt.Rows(0).Item("rec"))
                    AppSlot = Convert.ToDateTime(dt.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US"))
                End If
                'dt = New DataTable
                'sql = "declare @time as datetime;select @time = (select top 1 start_slot from TB_APPOINTMENT_CUSTOMER where DATEDIFF(D,app_date,GETDATE()) = 0 and customer_id = '" & Mobile & "' and active_status = 1 group by start_slot) select top " & rec & " id,in_use from TB_APPOINTMENT_SLOT where DATEDIFF(D,app_date,GETDATE()) = 0 and slot_time >= @time"
                'dt = getDataTable(sql, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")

                dt = New DataTable
                dt = eng.CheckAppointmentSlot(Mobile, rec, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")
                If dt.Rows.Count > 0 Then
                    Dim aEng As New KioskAppointmentENG
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        If CInt(dt.Rows(i).Item("in_use")) > 0 Then
                            'sql = "update TB_APPOINTMENT_SLOT set in_use = " & CInt(dt.Rows(i).Item("in_use")) - 1 & " where id = " & dt.Rows(i).Item("id")
                            'executeSQL(sql, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")

                            aEng.UpdateUseAppointmentSlot(CInt(dt.Rows(i).Item("in_use")) - 1, dt.Rows(i).Item("id"), INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")
                        End If
                    Next
                    aEng = Nothing
                End If

                'sql = "select appointment_job_id "
                'sql += "from TB_APPOINTMENT_CUSTOMER "
                'sql += " where DATEDIFF(D,start_slot,GETDATE()) = 0 and customer_id = '" & Mobile & "' "
                'sql += " and active_status = 1"
                'dt = getDataTable(sql, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")

                dt = eng.GetAppointmentJobID(Mobile, INIFileName, SoftwareName, "frmRegister_btnAppCancel.Click")
                If dt.Rows.Count > 0 Then
                    'sql = "update TB_APPOINTMENT_CUSTOMER set active_status = 6 where datediff(d,getdate(),app_date)=0 and customer_id = '" & Mobile & "' and active_status = 1"
                    'executeSQL(sql, INIFileName, SoftwareName, "frmRegister_btnAppCancel_Click")
                    eng.UpdateStatusAppointmentCustomer(Mobile, 1, 6, INIFileName, SoftwareName, "frmRegister_btnAppCancel.Click")

                    'Update Appointment Job To DC จาก 1(Confirm) ให้เป็น 6 (Cancel)
                    UpdateAppointmentJob(dt.Rows(0)("appointment_job_id"), "6")   'Cancel = 6
                End If
                dt.Dispose()

                btnMain.Enabled = True
                btnOK.Enabled = True
                btnEdit.Enabled = True

                DeleteFileFromCaptureAppointment(Mobile)
                DeleteSMSCancelAppointment(Mobile, AppSlot, "frmRegister_btnAppCancel.Click")

                Dim dtS As DataTable = eng.GetSiebelData(Mobile, "6", "Open", INIFileName, SoftwareName, "frmRegister_btnAppCancel.Click")
                If dtS.Rows.Count > 0 Then
                    Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                    Dim CancelDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                    Try
                        Dim ws As New ShopWebServiceMain.WebServiceAPI
                        ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
                        ws.SiebelUpdateToCancel(Mobile, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                        ws = Nothing
                    Catch ex As Exception
                        Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
                        sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmRegister_btnAppCancel.Click") & ", 1, getdate(),null,null,"
                        sqlLog += " getdate(), 'Kiosk.frmRegister.btnAppCancel_Click : Don''t Update Siebel Activity to Cancel Mobile No. " & FixDB(Mobile) & "', null, '" & SoftwareName & "', '" & getMyVersion() & "')"
                        executeSQL(sqlLog, INIFileName, SoftwareName, "frmRegister_btnAppCancel.Click")

                        Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                        ws.Url = GetWebServiceURL(False, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
                        ws.SiebelUpdateToCancel(Mobile, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                        ws = Nothing
                    End Try
                End If
                dtS.Dispose()

            Case Windows.Forms.DialogResult.OK
                CountVDO = 0
                PanelMsg.Visible = True
            Case Windows.Forms.DialogResult.Cancel
                CountVDO = 0
                PanelMsg.Visible = False
                btnMain.Enabled = True
                btnOK.Enabled = True
                btnEdit.Enabled = True
        End Select
        eng = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DeleteFileFromCaptureAppointment(ByVal MobileNo As String)
        Dim eng As New KioskBaseENG
        Dim Path As String = eng.GetCaptureAppointmentPath
        If File.Exists(Path & MobileNo & ".jpg") = True Then
            File.Delete(Path & MobileNo & ".jpg")
        End If
        eng = Nothing
    End Sub

    'Private Function GetSiebelData(ByVal MobileNo As String, ByVal AppointmentStatus As String, ByVal SiebelStatus As String) As DataTable
    '    'Update Siebel Activity to Cancelled (ไม่รู้จะทำไปทำไม เสียเวลาเปล่า)
    '    Dim sqlS As String = "select top 1 start_slot,CONVERT(varchar(5),start_slot,114) as app_time,item_id,item_name,item_name_th,siebel_activity_id, siebel_desc "
    '    sqlS += " from TB_APPOINTMENT_CUSTOMER "
    '    sqlS += " inner join TB_ITEM on TB_APPOINTMENT_CUSTOMER.item_id = TB_ITEM.id  "
    '    sqlS += " where DateDiff(D, GETDATE(), start_slot) = 0 And TB_ITEM.active_status = '1' "
    '    sqlS += " and TB_APPOINTMENT_CUSTOMER.active_status = " & AppointmentStatus & " "
    '    sqlS += " and customer_id = '" & MobileNo & "' "
    '    sqlS += " and siebel_status = '" & SiebelStatus & "'"
    '    sqlS += " order by item_order,item_name"
    '    Dim dtS As New DataTable
    '    dtS = getDataTable(sqlS, INIFileName, SoftwareName, "frmRegister_GetSiebelData")

    '    Return dtS
    'End Function

    Private Sub UpdateAppointmentJob(ByVal AppointJobID As Long, ByVal ActiveStatus As String)
        Try
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmRegister_UpdateAppointmentJob")
            ws.UpdateAppointmentJobStatus(AppointJobID, ActiveStatus)
            ws = Nothing
        Catch ex As Exception
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Url = GetWebServiceURL(False, INIFileName, SoftwareName, "frmRegister_UpdateAppointmentJob")
            ws.UpdateAppointmentJobStatus(AppointJobID, ActiveStatus)
            ws = Nothing
        End Try
    End Sub
    Private Sub DeleteSMSCancelAppointment(ByVal MobileNo As String, ByVal StartSlot As String, ByVal ButtonAction As String)
        'ลบข้อมูลการส่ง SMS Alert ที่ DC ผ่าน Web Service
        Try
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmRegister_DeleteSMSCancelAppointment")
            ws.DeleteSMSCancelAppointment(MobileNo, StartSlot)
            ws = Nothing
        Catch ex As Exception
            Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
            sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, ButtonAction) & ", 1, getdate(),null,null,"
            sqlLog += " getdate(), 'Kiosk." & ButtonAction & " : Don''t SMS to Cancel Mobile No. " & FixDB(Mobile) & "', null, '" & SoftwareName & "', '" & getMyVersion() & "')"
            executeSQL(sqlLog, INIFileName, SoftwareName, ButtonAction)

            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Url = GetWebServiceURL(False, INIFileName, SoftwareName, "frmRegister_DeleteSMSCancelAppointment")
            ws.DeleteSMSCancelAppointment(MobileNo, StartSlot)
            ws = Nothing
        End Try
    End Sub

    Private Sub btnAppOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppOK.Click

        CountVDO = 0
        PanelMsg.Visible = False

        PrintQueue.CountQueue = ""
        PrintQueue.ListService = ""
        PrintQueue.Mobile = ""
        PrintQueue.QueueNo = ""
        PrintQueue.WaitingTime = ""
        PrintQueue.AppTime = ""
        PrintQueue.AppDateNow = New Date(1, 1, 1)

        Dim eng As New KioskRegisterENG
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim txtQueue As String = ""
        dt = eng.GetTextQueueFromCustomerType(INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
        If dt.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            CustomerTypeID = dt.Rows(0).Item("id").ToString
            txtQueue = dt.Rows(0).Item("txt_queue").ToString
            dt = New DataTable
            dt = eng.GetDataAppointmentCustomer(Mobile, INIFileName, SoftwareName, "frmRegister_btnAppOK_Click")
            If dt.Rows.Count > 0 Then
                Dim AppointmentJobID As Long = Convert.ToInt64(dt.Rows(0)("appointment_job_id"))
                PrintQueue.WaitingTime = dt.Rows(0).Item("time").ToString
                PrintQueue.AppTime = dt.Rows(0).Item("app_time").ToString
                PrintQueue.HideWaitingTime = dt.Rows(0)("disable_ticket_waiting_time").ToString
                Dim AppSlot As String = Convert.ToDateTime(dt.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm", New Globalization.CultureInfo("en-US"))

                Dim Queue As String = ""
                Dim AllService As String = ""
                Dim ListItem As String = ""
                Queue = eng.FindQueueNoAppointment(Mobile, dt.Rows(0).Item("item_id").ToString, txtQueue, CustomerTypeID, INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
                Dim CurrDB As String = CheckCurrentDB(INIFileName)
                Dim vDateNow As String = "'" & Convert.ToDateTime(dt.Rows(0)("date_now")).ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'" 'FindDateNow(INIFileName, SoftwareName, "frmRegister_btnAppOK.Click").ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) 
                PrintQueue.AppDateNow = Convert.ToDateTime(dt.Rows(0)("date_now"))
                For i As Int32 = 0 To dt.Rows.Count - 1
                    InsertServiceAppointment(PrintQueue.AppTime, Mobile, CustomerTypeID, dt.Rows(i).Item("item_id").ToString, CustomerName, Segment, vDateNow, Queue, 0, NetworkType, CurrDB, INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
                    If ListItem = "" Then
                        ListItem = dt.Rows(i).Item(itemname_field).ToString
                    Else
                        ListItem = ListItem & "," & dt.Rows(i).Item(itemname_field).ToString
                    End If

                    If AllService = "" Then
                        AllService = dt.Rows(i).Item("item_id").ToString
                    Else
                        AllService = AllService & "," & dt.Rows(i).Item("item_id").ToString
                    End If
                Next


                dt = New DataTable
                Dim CountApp As String = "0"
                dt = eng.GetAppServiceQty(Mobile, Queue, INIFileName, SoftwareName, "frmRegister_btnAppOK_Click")
                If dt.Rows.Count > 0 Then
                    CountApp = dt.Rows(0).Item("Count_app").ToString
                End If

                eng.UpdateCounterQueueAppointment(AllService, Mobile, Queue, Segment, CurrDB, INIFileName, SoftwareName, "frmRegister_btnAppOK.Click", ListItem)

                'Update Appointment Job To DC
                UpdateAppointmentJob(AppointmentJobID, "2")   'Register = 2
                'Debug += DateTime.Now.ToString("HH:mm:ss.fff") & " After UpdateAppointmentJob" & vbNewLine

                DeleteSMSCancelAppointment(Mobile, AppSlot, "frmRegister_btnAppOK.Click")

                'Update Siebel To Register
                Dim dtS As DataTable = eng.GetSiebelData(Mobile, "2", "Open", INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
                If dtS.Rows.Count > 0 Then
                    'Debug += DateTime.Now.ToString("HH:mm:ss.fff") & " Before SiebelUpdateToRegister" & vbNewLine
                    Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                    Dim CancelDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                    Try
                        Dim ws As New ShopWebServiceMain.WebServiceAPI
                        ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
                        ws.SiebelUpdateToRegister(Mobile, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                        ws = Nothing
                    Catch ex As Exception
                        Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
                        sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmRegister_btnAppOK.Click") & ", 1, getdate(),null,null,"
                        sqlLog += " getdate(), 'Don''t Update Siebel Activity to Register Mobile No. " & FixDB(Mobile) & "', null, '" & SoftwareName & "', '" & getMyVersion() & "')"
                        executeSQL(sqlLog, INIFileName, SoftwareName, "frmRegister_btnAppOK_Click")

                        Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                        ws.Url = GetWebServiceURL(False, INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
                        ws.SiebelUpdateToRegister(Mobile, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                        ws = Nothing
                    End Try
                    'Debug += DateTime.Now.ToString("HH:mm:ss.fff") & " After SiebelUpdateToRegister" & vbNewLine
                End If
                'MessageBox.Show(Debug)

                Me.Cursor = Cursors.Default

                Dim IsPrint As Boolean = False
                Dim IsSMS As Boolean = False
                If Mobile.Trim <> "" Then
                    Dim fd As New frmDialogConfirmPrint
                    If Language = "TH" Then
                        fd.Msg = "คุณต้องการพิมพ์บัตรคิวหรือไม่ ?"
                    Else
                        fd.Msg = "Do you want to printed queue ticket?"
                    End If
                    Dim diResult As Windows.Forms.DialogResult = fd.ShowDialog
                    If diResult = Windows.Forms.DialogResult.OK Then
                        IsPrint = True
                    ElseIf diResult = Windows.Forms.DialogResult.Yes Then
                        IsSMS = True
                    Else
                        If diResult = Windows.Forms.DialogResult.Cancel Then
                            IsPrint = True
                            IsSMS = True
                        End If
                    End If
                Else
                    IsPrint = True
                End If

                If IsPrint = True Then
                    PrintQueue.CountQueue = CountApp
                    PrintQueue.ListService = ListItem
                    PrintQueue.Mobile = Mobile
                    PrintQueue.QueueNo = Queue
                    Dim p As New PrintDocument
                    p.PrintController = New Printing.StandardPrintController
                    AddHandler p.PrintPage, AddressOf pd_PrintPage
                    p.Print()
                    Dim f As New frmDialogBeforeClose
                    f.Message = msgGetTicketAndWaitForAppointment & PrintQueue.WaitingTime & msgMinute
                    f.ShowDialog()
                End If

                'ส่งไฟล์ Capture ไปยัง MainServer
                UpdateCaptureAppointment(Mobile, Queue, PrintQueue.AppDateNow, AppointmentJobID)

                If IsSMS = True Then
                    'Send SMS
                    Dim msg As String = ""
                    If Language = "TH" Then
                        msg = "คิว " & Queue & " | ประเภท " & ListItem
                        msg += " | มี " & CountApp & " คิวรอรับบริการก่อนหน้าคุณ"
                    Else
                        msg = "Queue No." & Queue
                        msg += " | " & ListItem
                        msg += " | " & CountApp & " Queue(s) ahead of you"
                    End If

                    Try
                        Dim p As New ShopWebServiceMain.SMSResponsePara
                        Dim ws As New ShopWebServiceMain.WebServiceAPI
                        ws.Url = m_webservice_url
                        p = ws.SendSMS(msg, Mobile)
                        ws = Nothing

                        If p.RESULT = False Then
                            Dim wsd As New ShopWebServiceDisplay.WebServiceAPI
                            wsd.Url = d_webservice_url
                            wsd.SendSMS(msg, Mobile)
                            wsd = Nothing
                        End If
                    Catch ex As Exception
                        Dim wsd As New ShopWebServiceDisplay.WebServiceAPI
                        wsd.Url = d_webservice_url
                        wsd.SendSMS(msg, Mobile)
                        wsd = Nothing
                    End Try
                End If
                txtMobile.Text = ""
            Else
                dt = eng.GetDataAppointmentNoShow(Mobile, INIFileName, SoftwareName, "frmRegister_btnAppOK_Click")
                Dim fldItemName As String = ""
                Dim ListItem As String = ""
                If Language = "TH" Then
                    fldItemName = "item_name_th"
                Else
                    fldItemName = "item_name"
                End If

                For Each dr As DataRow In dt.Rows
                    If ListItem.Trim = "" Then
                        ListItem = dr(fldItemName)
                    Else
                        ListItem += vbCrLf & dr(fldItemName)
                    End If
                Next
                PanelMsg.Visible = True
                btnMain.Enabled = False
                btnOK.Enabled = False
                btnEdit.Enabled = False

                Dim vDateNow As DateTime = FindDateNow(INIFileName, SoftwareName, "frmRegister_btnAppOK.Click")
                If Language = "TH" Then
                    lblMsg.Text = Mobile & vbCrLf & vbCrLf & msgHasAppointment & vbCrLf & vbCrLf & ListItem & vbCrLf & vbCrLf & msgDateon & vDateNow.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH")) & msgTimeAt & Convert.ToDateTime(dt.Rows(0).Item("start_slot")).ToString("HH:mm", New Globalization.CultureInfo("th-TH")) & msgAtclock & vbCrLf & vbCrLf & msgQCancelled
                Else
                    lblMsg.Text = Mobile & vbCrLf & vbCrLf & msgHasAppointment & vbCrLf & ListItem & vbCrLf & vbCrLf & msgDateon & vDateNow.ToString("dd/MM/yy", New System.Globalization.CultureInfo("en-US")) & msgTimeAt & CDate(dt.Rows(0).Item("start_slot")).ToString("hh:mm tt") & vbCrLf & vbCrLf & msgQCancelled
                End If

                btnAppBack.Width = 120
                btnAppNew.Visible = True
                btnMain.Enabled = True
                btnAppCancel.Visible = False
                btnAppOK.Visible = False

                CheckCustomerProfile(Mobile)
            End If
            dt.Dispose()
            btnMain.Enabled = True
            btnOK.Enabled = True
            btnEdit.Enabled = True
        Else
            Me.Cursor = Cursors.Default
            ShowDialogBox(msgAppointmentNotSupported, msgWarning)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UpdateCaptureAppointment(ByVal MobileNo As String, ByVal QueueNo As String, ByVal AssignTime As DateTime, ByVal appointment_job_id As Long)
        Try
            Dim sql As String = "update tb_customer_image "
            sql += " set queue_no='" & QueueNo & "',"
            sql += " assign_time='" & AssignTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
            sql += " where mobile_no='" & MobileNo & "' and datediff(d,getdate(),capture_time)=0 "
            sql += " and assign_time is null and queue_no is null and appointment_job_id='" & appointment_job_id & "'"
            If ExecuteNonQuery(sql, INIFileName, SoftwareName, "frmRegister_UpdateCaptureAppointment") = True Then
                Dim ws As New ShopWebServiceMain.WebServiceAPI
                ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmRegister_UpdateCaptureAppointment")
                ws.UpdateCustCaptureIndexFile()
                ws = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnTH_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTH.Click
        If PanelMsg.Visible = True Then
            Exit Sub
        End If
        Language = "TH"
        ChangeLanguage()
    End Sub

    Private Sub btnEN_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEN.Click
        If PanelMsg.Visible = True Then
            Exit Sub
        End If
        Language = "EN"
        ChangeLanguage()
    End Sub

#Region "Print"
    Private Sub pd_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pd.PrintPage
        Dim eng As New KioskRegisterENG
        eng.PrintQueueAppointTicket(e, INIFileName, SoftwareName, "frmRegister_pd.PrintPage", PrintQueue)
        eng = Nothing

        CheckNotify()
    End Sub

#End Region

#Region "Call ShopWebService for getCustomerProfile"
    Function CheckCustomerProfile(ByVal MobileNo As String) As Boolean
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Try
            Dim DeBug As String = " Start Shown : " & DateTime.Now.ToString("mm:ss.fff") & vbCrLf
            Dim FindCustomerInfo As New ShopWebServiceMain.WebServiceAPI
            Dim CustomerInfo As New ShopWebServiceMain.TbCustomerShParaDB
            FindCustomerInfo.Url = m_webservice_url
            'FindCustomerInfo.Timeout = 10000
            DeBug += " Before GetCustomerProfile : " & DateTime.Now.ToString("mm:ss.fff") & vbCrLf
            CustomerInfo = FindCustomerInfo.GetCustomerProfile(MobileNo)
            DeBug += " After GetCustomerProfile : " & DateTime.Now.ToString("mm:ss.fff") & vbCrLf
            Segment = CustomerInfo.SEGMENT_LEVEL
            AssetID = CustomerInfo.ASSET_ID
            NetworkType = CustomerInfo.NETWORK_TYPE
            SMECorporateType = CustomerInfo.CORPORATE_TYPE
            CustomerPAGroup = CustomerInfo.PA_GROUP
            CustomerURLCapture = CustomerInfo.URL_CAPTURE
            CustomerURLCaptureDate = CustomerInfo.URL_CAPTURE_DATE
            Contact_ID = CustomerInfo.CONTACT_ID
            CustomerBOS = IIf(CustomerInfo.BILLING_SYSTEM.Trim = "BOS", "Yes", "No")
            CustomerMobileStatus = CustomerInfo.MOBILE_STATUS
            CustomerCategory = CustomerInfo.CATEGORY
            CustomerBrandName = CustomerInfo.BRAND_NAME
            If CustomerInfo.NETWORK_TYPE.Trim = "" Then
                Return False
            End If
            ''MessageBox.Show(DeBug)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Return CheckCustomerProfileFromDisplay(MobileNo)
        End Try
        ini = Nothing

        Return True
    End Function

    Private Function CheckCustomerProfileFromDisplay(ByVal MobileNo As String) As Boolean
        Try
            Dim FindCustomerInfo As New ShopWebServiceDisplay.WebServiceAPI
            Dim CustomerInfo As New ShopWebServiceDisplay.TbCustomerShParaDB
            FindCustomerInfo.Url = d_webservice_url
            'FindCustomerInfo.Timeout = 10000
            CustomerInfo = FindCustomerInfo.GetCustomerProfile(MobileNo)
            Segment = CustomerInfo.SEGMENT_LEVEL
            AssetID = CustomerInfo.ASSET_ID
            NetworkType = CustomerInfo.NETWORK_TYPE
            SMECorporateType = CustomerInfo.CORPORATE_TYPE
            CustomerPAGroup = CustomerInfo.PA_GROUP
            CustomerURLCapture = CustomerInfo.URL_CAPTURE
            CustomerURLCaptureDate = CustomerInfo.URL_CAPTURE_DATE
            Contact_ID = CustomerInfo.CONTACT_ID
            CustomerBOS = IIf(CustomerInfo.BILLING_SYSTEM.Trim = "BOS", "Yes", "No")
            CustomerMobileStatus = CustomerInfo.MOBILE_STATUS
            CustomerCategory = CustomerInfo.CATEGORY
            CustomerBrandName = CustomerInfo.BRAND_NAME
            If CustomerInfo.NETWORK_TYPE.Trim = "" Then
                Return False
            End If
            FindCustomerInfo = Nothing
            CustomerInfo = Nothing
        Catch ey As Exception
            Return False
        End Try

        Return True
    End Function



#End Region

    Private Sub TimerCheckCaptureFile_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckCaptureFile.Tick
        TimerCheckCaptureFile.Enabled = False
        Try
            Dim eng As New KioskCaptureENG
            Dim BackupPath As String = eng.GetCaptureBackupPath
            Dim txtFile As String = BackupPath & "BackupCaptureFile.txt"
            If File.Exists(txtFile) = True Then
                Dim ws As New ShopWebServiceMain.WebServiceAPI
                ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmRegister_TimerCheckCaptureFile.Tick")
                Dim Stream As New StreamReader(txtFile, System.Text.UnicodeEncoding.Default)
                While Stream.Peek <> -1
                    Dim str As String = Stream.ReadLine
                    If str.Trim <> "" Then
                        Dim strFld As String() = Split(str, "#")
                        Try
                            Dim _file As String = BackupPath & strFld(1) & ".jpg"
                            If File.Exists(_file) = True Then
                                Dim SendID As Long = 0
                                Dim ff As New FileInfo(_file)
                                SendID = ws.SendCaptureImageFile(File.ReadAllBytes(_file), Split(ff.Name, ".")(0), strFld(2), GetStrToDateTime(strFld(0)), ff.CreationTime)
                                If SendID > 0 Then
                                    File.Delete(_file)
                                End If
                                ff = Nothing
                            End If
                        Catch ex As IndexOutOfRangeException

                        End Try
                    End If
                End While
                Stream.Close()
                Stream = Nothing
                ws = Nothing

                If Directory.GetFiles(BackupPath, "*.jpg").Length = 0 Then
                    File.Delete(txtFile)
                End If
            End If
            eng = Nothing
        Catch ex As Exception

        End Try
        TimerCheckCaptureFile.Enabled = True
    End Sub

    
End Class