Imports System.Drawing.Printing
Imports Kiosk.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG
Imports Engine.Kiosk
Imports Engine.Kiosk.KioskModule
Imports System.IO

Public Class frmChooseService

    Dim dt_displayservice As New DataTable

    Dim PrintQueue As KioskChooseServiceENG.Print



    Sub SetLogo()
        Try
            Dim logopath As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "LogoKioskPath", SoftwareName, "frmChooseService.SetLogo")
            If File.Exists(logopath) = False Then
                logopath = Application.StartupPath & "\Images\logo_kiosk.png"
            End If
            PictureBox4.BackgroundImage = Image.FromFile(logopath)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMain.Click
        CountVDO = 0
        'Me.Close()
        frmRegister.BringToFront()
    End Sub

    Private Sub TimerVDO_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CountVDO < MaxCountVDO Then
            CountVDO = CountVDO + 1
        Else
            If frmVDO.ShowDialog = Windows.Forms.DialogResult.Yes Then
                frmVDO.Close()
            End If
        End If
    End Sub

    Private Sub frmChooseService_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetLogo()
        Dim dt As New DataTable
        'Try
        '    Dim EventDate As String = DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
        '    Dim ws As New ShopWebServiceMain.WebServiceAPI
        '    ws.Timeout = 2400000
        '    dt = ws.LoadImageFromWebConfig(EventDate, 4)
        '    ws = Nothing
        'Catch ex As Exception

        'End Try

        Dim ChooseServiceFooterBanner As String = Application.StartupPath & "\ChooseServiceFooterBanner.png"
        If dt.Rows.Count > 0 Then
            If Convert.IsDBNull(dt.Rows(0)("Files")) = False Then
                If IO.File.Exists(ChooseServiceFooterBanner) = True Then
                    IO.File.SetAttributes(ChooseServiceFooterBanner, IO.FileAttributes.Normal)
                    IO.File.Delete(ChooseServiceFooterBanner)
                End If

                Dim FileByte() As Byte = DirectCast(dt.Rows(0)("Files"), Byte())
                Dim fs As New IO.FileStream(Application.StartupPath & "\ChooseServiceFooterBanner.png", IO.FileMode.CreateNew)
                fs.Write(FileByte, 0, FileByte.Length)
                fs.Close()

                PictureBox1.Image = Image.FromFile(Application.StartupPath & "\ChooseServiceFooterBanner.png")
                FileByte = Nothing
            Else
                PictureBox1.Image = Image.FromFile(Application.StartupPath & "\ChooseServiceFooterBanner.png")
            End If
        Else
            PictureBox1.Image = Image.FromFile(Application.StartupPath & "\ChooseServiceFooterBanner.png")
        End If

    End Sub

    Public Sub RenderService()
        FLP.Hide()
        FLP.Controls.Clear()
        Dim sql As String = ""
        sql = "exec SP_DisplayService " & CustomerTypeID
        dt_displayservice = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_RenderService")

        If CustomerBrandName = "YOU" Then
            dt_displayservice.DefaultView.RowFilter = "brand_name='Y'"
            dt_displayservice = dt_displayservice.DefaultView.ToTable
        End If

        If dt_displayservice.Rows.Count > 0 Then
            'msgDebug += "Query : " & DateTime.Now.ToString("hh:mm:ss.fff") & vbCrLf
            For i As Int32 = 0 To dt_displayservice.Rows.Count - 1
                AddItem1(dt_displayservice.Rows(i).Item("id").ToString, dt_displayservice.Rows(i).Item(itemname_field).ToString, dt_displayservice.Rows(i).Item("count_agent").ToString)
                additem2(dt_displayservice.Rows(i).Item("id").ToString, dt_displayservice.Rows(i).Item("count_queue").ToString)
                additem3(dt_displayservice.Rows(i).Item("id").ToString, dt_displayservice.Rows(i).Item("wait_time").ToString)
                additem4(dt_displayservice.Rows(i).Item("id").ToString, dt_displayservice.Rows(i).Item("app_queue").ToString)
            Next
            'msgDebug += "Render : " & DateTime.Now.ToString("hh:mm:ss.fff") & vbCrLf
        End If
        dt_displayservice.Dispose()
        FLP.Show()
        'MessageBox.Show(msgDebug)
    End Sub

    Public Sub RenderObject()
        PrintQueue.CountQueue = ""
        PrintQueue.ListService = ""
        PrintQueue.Mobile = ""
        PrintQueue.WaitingTime = ""
        PrintQueue.QueueNo = ""

        btnAppointment.Visible = True

        Dim eng As New KioskChooseServiceENG
        'Dim sql As String = ""
        'Dim dt As New DataTable
        'sql = "select top 1 id from TB_APPOINTMENT_SCHEDULE where datediff(d,app_date,GETDATE()) = 0"
        'dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_RenderObject")

        Dim dt As New DataTable
        dt = eng.CheckAppointmentSchedule(INIFileName, SoftwareName, "frmChooseService_RenderObject")
        If dt.Rows.Count = 0 Then
            btnAppointment.Visible = False
        Else
            If CustomerApp = False Then
                btnAppointment.Visible = False
            Else
                'sql = "select top 1 id from tb_counter_queue where datediff(d,getdate(),service_date)=0 and status in (1,2,4) and customer_id = '" & FixDB(Mobile) & "'"
                'dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_RenderObject")
                dt = eng.CheckQueueCustomerInShop(Mobile, INIFileName, SoftwareName, "frmChooseService_RenderObject")
                If dt.Rows.Count > 0 Then
                    btnAppointment.Visible = False
                End If
            End If
        End If
        dt.Dispose()

        'ดึงข้อมูล Campaign
        If CustomerAIS = True Then
            TimerCampaign.Enabled = True
        End If

        'Hard Code โลด
        lblCategory.Text = ""
        If SMECorporateType = "SME" Then
            lblCategory.Text = SMECorporateType
        End If
        If InStr(NetworkType, "3G") > 0 Then
            lblCategory.Text += "     3G"
        End If
        If Trim(CustomerBrandName) <> "" Then
            lblCategory.Text = CustomerBrandName & "!"
        End If
        Dim clr As Color = ColorTranslator.FromHtml("#a8cb67")
        lblCategory.BackColor = clr
        'Application.DoEvents()
    End Sub

    Private Sub frmChooseService_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'RenderService()
    End Sub

    Sub AddItem1(ByVal id As Integer, ByVal Item As String, ByVal agent As Integer)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 25, FontStyle.Bold, GraphicsUnit.Pixel)
        With lbl
            .Width = 320
            .Height = 45
            .Name = id
            .Text = Item
            .Font = Font
            .AutoSize = False
            .FlatStyle = FlatStyle.Flat
            .BackColor = Color.White
            .Enabled = True
            .TextAlign = ContentAlignment.MiddleLeft
        End With
        FLP.Controls.Add(lbl)
        AddHandler lbl.MouseClick, AddressOf CheckItem
    End Sub

    Sub additem2(ByVal id As Integer, ByVal item As String)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 25, FontStyle.Bold, GraphicsUnit.Pixel)
        With lbl
            .Width = 80
            .Height = 45
            .Name = id
            .Text = item
            .Font = Font
            .AutoSize = False
            .FlatStyle = FlatStyle.Flat
            .TextAlign = ContentAlignment.MiddleRight
        End With
        FLP.Controls.Add(lbl)
    End Sub

    Sub additem3(ByVal id As Integer, ByVal item As String)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 25, FontStyle.Bold, GraphicsUnit.Pixel)
        With lbl
            .Width = 115
            .Height = 45
            .Font = Font
            .TextAlign = ContentAlignment.MiddleRight
            .Text = item
            .AutoSize = False
            .FlatStyle = FlatStyle.Flat
        End With
        FLP.Controls.Add(lbl)
    End Sub

    Sub additem4(ByVal id As Integer, ByVal item As String)
        Dim lbl As New Label
        Dim Font As Font = New Font("Microsoft Sans Serif", 25, FontStyle.Bold, GraphicsUnit.Pixel)
        With lbl
            .Width = 80
            .Height = 45
            .Name = id
            .Text = item
            .Font = Font
            .TextAlign = ContentAlignment.MiddleRight
            .AutoSize = False
            .FlatStyle = FlatStyle.Flat
        End With
        FLP.Controls.Add(lbl)
    End Sub

    Private Sub CheckItem(ByVal Sender As Object, ByVal e As EventArgs)
        CountVDO = 0
        Dim lbl As Label = Sender
        If lbl.BackColor = Color.White Then
            Dim num As Int32 = 0
            For i As Int32 = 0 To FLP.Controls.Count - 1
                If FLP.Controls(i).BackColor = Color.LightSeaGreen Then
                    num = num + 1
                End If
            Next
            '************************ Config
            If num = k_serve Then
                ShowDialogBox(msgMaximum & k_serve & msgService, msgWarning)
                Exit Sub
            End If

            lbl.BackColor = Color.LightSeaGreen
            lbl.ForeColor = Color.White
        Else
            lbl.BackColor = Color.White
            lbl.ForeColor = Color.Black
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Cursor = Cursors.WaitCursor
        CountVDO = 0
        If Validation() = False Then
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim dDateNow As DateTime = FindDateNow(INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
        Dim vDateNow As String = FixDateTime(dDateNow)
        'Dim DeBug As String = DateTime.Now.ToString("HH:mm:ss.fff") & " : Start" & vbNewLine
        Dim WaitingTime As String = ""
        Dim sql As String = ""
        Dim AllService As String = ""
        For i As Int32 = 0 To FLP.Controls.Count - 1
            If FLP.Controls(i).BackColor = Color.LightSeaGreen Then
                If AllService = "" Then
                    AllService = FLP.Controls(i).Name
                Else
                    AllService = AllService & "," & FLP.Controls(i).Name
                End If
            End If
        Next

        Dim ItemUser As String = ""
        Dim dt As New DataTable

        Dim dt_temp As New DataTable
        dt_displayservice.DefaultView.RowFilter = "id in (" & AllService & ")"
        dt_displayservice.DefaultView.Sort = " wait_time,item_wait,item_time,item_order"
        dt_temp = dt_displayservice.DefaultView.ToTable
        dt_displayservice.DefaultView.RowFilter = ""

        Dim ItemID As String = ""
        Dim ItemName As String = ""
        Dim ItemTime As String = ""
        Dim CountQueue As String = ""
        Dim HideWaitTime As String = ""

        Dim eng As New KioskChooseServiceENG
        Dim Queue As String = ""
        Queue = eng.FindQueueNo(Mobile, dt_temp.Rows(0).Item("id").ToString, CustomerTypeID, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
        If Queue <> "" Then
            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " : Before Insert Service" & vbNewLine
            Dim CurrDB As String = CheckCurrentDB(INIFileName)
            For j As Int32 = 0 To dt_temp.Rows.Count - 1
                If ItemUser = "" Then
                    ItemUser = dt_temp.Rows(j).Item("id").ToString
                Else
                    ItemUser = ItemUser & "," & dt_temp.Rows(j).Item("id").ToString
                End If
                InsertService(Mobile, CustomerTypeID, dt_temp.Rows(j).Item("id").ToString, CustomerName, Segment, Queue, 0, NetworkType, vDateNow, CurrDB, INIFileName, SoftwareName, "frmChooseService_btnOK.Click")
            Next
            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " : End Insert Service" & vbNewLine

            ItemID = dt_temp.Rows(0).Item("id").ToString
            ItemName = dt_temp.Rows(0).Item(itemname_field).ToString
            If CInt(dt_temp.Rows(0).Item("time")) <= CInt(k_disable) Then
                ItemTime = dt_temp.Rows(0).Item("time").ToString
            End If
            CountQueue = dt_temp.Rows(0).Item("count_queue").ToString
            HideWaitTime = dt_temp.Rows(0)("disable_ticket_waiting_time").ToString
            '***************************************************

            eng.UpdateQueueNo(INIFileName, SoftwareName, "frmChooseService_btnOK.Click", vDateNow, CurrDB, Mobile, AllService, Segment, Queue, ItemID)
        Else
            ShowDialogBox(msgPlsChkQSetting & vbCrLf & msgOfService & ItemName, msgWarning)
            sql = "delete from TB_COUNTER_QUEUE where datediff(d,getdate(),service_date)=0 "
            executeSQL(sql, INIFileName, SoftwareName, "frmChooseService.Click")
            Me.Cursor = Cursors.Default
            eng = Nothing
            Exit Sub
        End If

        Dim Item() As String = ItemUser.Split(",")
        Dim ListItem As String = ""
        For i As Int32 = 0 To Item.Length - 1
            dt = New DataTable
            dt_displayservice.DefaultView.RowFilter = "id = " & Item(i)
            dt = dt_displayservice.DefaultView.ToTable
            If ListItem = "" Then
                ListItem = dt.Rows(0).Item(itemname_field).ToString
            Else
                ListItem = ListItem & "," & dt.Rows(0).Item(itemname_field).ToString
            End If
        Next
        dt_displayservice.DefaultView.RowFilter = ""
        btnMain.Enabled = False
        btnOK.Enabled = False
        btnAppointment.Enabled = False
        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " : Befor Update Waiting Time" & vbNewLine
        If Language = "TH" Then
            If dt_temp.Rows.Count > 1 Then
                lblMsg.Text = lblMsg.Text.Replace("[H]", msgPleastaketicket)
                'ถ้าเลือกมาหลาย service
                lblMsg.Text = lblMsg.Text.Replace("[Y]", msgYouwillbeservefirstwith)
                lblMsg.Text = lblMsg.Text.Replace("[S]", dt_temp.Rows(0).Item(itemname_field).ToString & msgBefore)
                If HideWaitTime = "0" Then
                    If ItemTime <> "" Then
                        lblMsg.Text = lblMsg.Text.Replace("[X]", msgPlsWaitForAbout & ItemTime & msgMinute)
                    Else
                        lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                    End If
                Else
                    lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                End If
            Else
                'ถ้าเลือกมา service เดียว
                lblMsg.Text = lblMsg.Text.Replace("[H]", msgPleastaketicket)
                lblMsg.Text = lblMsg.Text.Replace("[Y]", msgYouwillbeservefirstwith)
                lblMsg.Text = lblMsg.Text.Replace("[S]", dt_temp.Rows(0).Item(itemname_field).ToString)
                If HideWaitTime = "0" Then
                    If ItemTime <> "" Then
                        lblMsg.Text = lblMsg.Text.Replace("[X]", msgPlsWaitForAbout & ItemTime & msgMinute)
                    Else
                        lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                    End If
                Else
                    lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                End If
            End If
        Else
            lblMsg.Text = lblMsg.Text.Replace("[H]", msgPleastaketicket)
            If dt_temp.Rows.Count > 1 Then
                'ถ้าเลือกมาหลาย service
                lblMsg.Text = lblMsg.Text.Replace("[Y]", msgYouwillbeservefirstwith)
                lblMsg.Text = lblMsg.Text.Replace("[S]", dt_temp.Rows(0).Item(itemname_field).ToString)

                If HideWaitTime = "0" Then
                    If ItemTime <> "" Then
                        lblMsg.Text = lblMsg.Text.Replace("[X]", msgWithinabout & ItemTime & msgMinute)
                    Else
                        lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                    End If
                Else
                    lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                End If
            Else
                lblMsg.Text = lblMsg.Text.Replace("[Y]", msgYouwillservewith)
                lblMsg.Text = lblMsg.Text.Replace("[S]", dt_temp.Rows(0).Item(itemname_field).ToString)
                If HideWaitTime = "0" Then
                    If ItemTime <> "" Then
                        lblMsg.Text = lblMsg.Text.Replace("[X]", msgAbout & ItemTime & msgMinute)
                    Else
                        lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                    End If
                Else
                    lblMsg.Text = lblMsg.Text.Replace("[X]", "")
                End If
            End If
        End If

        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " : Befor Dialog Print" & vbNewLine
        'MessageBox.Show(DeBug)

        'New Version V0.0.24.0
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
            PanelMsg.Visible = True
            '************** Print ***************
            PrintQueue.CountQueue = CountQueue
            PrintQueue.ListService = ListItem
            PrintQueue.Mobile = Mobile
            PrintQueue.WaitingTime = ItemTime
            PrintQueue.QueueNo = Queue
            PrintQueue.HideWaiting = HideWaitTime
            PrintQueue.PrintTicketCoupon = PrintTicketCoupon
            'printTicket(Queue, Mobile, CountQueue, ItemTime, ListItem)
            Dim p As New PrintDocument
            p.PrintController = New Printing.StandardPrintController
            AddHandler p.PrintPage, AddressOf pd_PrintPage
            p.Print()
            'lastPrintY = 0
            '************************************
        End If
        Me.Cursor = Cursors.Default
        TimerEnd.Enabled = True
        'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " : After Print" & vbNewLine

        '=== Start Update TB_Customer_Image
        UpdateCustomerImageFile(CustomerImageID, Queue, dDateNow)

        If IsSMS = True Then
            'Send SMS
            Dim msg As String = ""
            If Language = "TH" Then
                msg = "คิว " & Queue & " | ประเภท " & ListItem
                msg += " | มี " & CountQueue & " คิวรอรับบริการก่อนหน้าคุณ"
            Else
                msg = "Queue No." & Queue
                msg += " | " & ListItem
                msg += " | " & CountQueue & " Queue(s) ahead of you"
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

            'DeBug += DateTime.Now.ToString("HH:mm:ss.fff") & " : After SMS" & vbNewLine
        End If
    End Sub

    Private Function Validation() As Boolean
        '*************** เช็คการกรอกข้อมูล ***************
        Dim chk As Boolean = False
        Dim num As Int32 = 0
        For i As Int32 = 0 To FLP.Controls.Count - 1
            If FLP.Controls(i).BackColor = Color.LightSeaGreen Then
                chk = True
                num = num + 1
                Exit For
            End If
        Next

        If chk = False Then
            ShowDialogBox(msgPleaseChooseOneService, msgWarning)

            'PanelMsg.Visible = True
            'lblMsg.Text = lblMsg.Text.Replace("[H]", "")
            'lblMsg.Text = lblMsg.Text.Replace("[Y]", msgPleaseChooseOneService)
            'lblMsg.Text = lblMsg.Text.Replace("[S]", "")
            'lblMsg.Text = lblMsg.Text.Replace("[X]", "")
            'frmDialogMsg.Close()

            'PanelMsg.Visible = False
            Return False

        End If

        Return True
    End Function

    Private Sub btnAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppointment.Click
        Dim sql As String = ""


        CountVDO = 0
        If Mobile = "" Then
            ShowDialogBox(msgCannotBBooked, msgWarning)
            Exit Sub
        End If

        '*************** เช็คการกรอกข้อมูล ***************
        Dim chk As Boolean = False
        Dim num As Int32 = 0
        For i As Int32 = 0 To FLP.Controls.Count - 1
            If FLP.Controls(i).BackColor = Color.LightSeaGreen Then
                chk = True
                num = num + 1
            End If
        Next

        If chk = False Then
            ShowDialogBox(msgPleaseChooseOneService, msgWarning)
            Exit Sub
        End If

        If num > k_max_appointment Then
            If Language = "TH" Then
                ShowDialogBox(msgMaxServiceApp & k_max_appointment & msgService, msgWarning)
                Exit Sub
            Else
                ShowDialogBox(msgMaxServiceApp & k_max_appointment & msgService, msgWarning)
                Exit Sub
            End If
        End If

        Dim serviceId As String = ""
        For i As Integer = 0 To FLP.Controls.Count - 1
            If FLP.Controls.Item(i).BackColor = Color.LightSeaGreen Then
                If serviceId = "" Then
                    serviceId = FLP.Controls.Item(i).Name
                Else
                    serviceId = FLP.Controls.Item(i).Name & "," & serviceId
                End If
            End If
        Next

        Dim eng As New KioskChooseServiceENG
        Dim dt As New DataTable
        'sql = "select item_name,item_name_th  from TB_ITEM where active_status = 1 and id in (" & serviceId & ") and id not in (select item_id from TB_APPOINTMENT_ITEM where DATEDIFF(D,GETDATE(),app_date)=0)"
        'dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_btnAppointment_Click")
        dt = eng.CheckServiceAppointment(serviceId, INIFileName, SoftwareName, "frmChooseService_btnAppointment_Click")
        If dt.Rows.Count > 0 Then
            Dim ListItem As String = ""
            For i As Int32 = 0 To dt.Rows.Count - 1
                If ListItem = "" Then
                    ListItem = dt.Rows(i).Item(itemname_field).ToString
                Else
                    ListItem = ListItem & vbCrLf & dt.Rows(i).Item(itemname_field).ToString
                End If
            Next
            dt.Dispose()
            eng = Nothing
            ShowDialogBox(msgYouChooseTheService & vbCrLf & vbCrLf & ListItem & vbCrLf & vbCrLf & msgBookingNotAllowed, msgWarning)
            Exit Sub
        End If
        eng = Nothing

        'กรณีจองแล้วไม่มาตามนัด(NoShow) x ครั้ง ภายใน X วัน ก็จะติด Backlist ไปนาน x วัน
        Dim f As New frmDialogMsg
        Try
            Dim wsBl As New ShopWebServiceMain.WebServiceAPI
            wsBl.Url = m_webservice_url
            Dim p As New ShopWebServiceMain.AppointmentCheckBacklistResultPara
            p = wsBl.CheckBlackList(Mobile)
            If p.IsBackList = True Then
                If Language = "TH" Then
                    f.lblText.Text = Mobile & vbCrLf & vbCrLf & msgCannotAppoinment & vbCrLf & vbCrLf & msgPleasmakeanew & vbCrLf & msgAppointmentAgain & p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                Else
                    f.lblText.Text = Mobile & vbCrLf & vbCrLf & msgCannotAppoinment & vbCrLf & msgPleasmakeanew & vbCrLf & msgAppointmentAgain & vbCrLf & p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & msgOnward
                End If

                f.btnConfirm.Visible = False
                f.btnCancel.Visible = False
                f.btnOK.Visible = False

                If Language = "TH" Then
                    f.btnMain.Text = "หน้าหลัก"
                Else
                    f.btnMain.Text = "Main Menu"
                End If
                p = Nothing
                wsBl = Nothing

                f.btnMain.Visible = True
                f.ShowDialog()
                btnMain.PerformClick()
                Exit Sub
            End If
            p = Nothing
            wsBl = Nothing
        Catch ex As Exception
            Try
                Dim sqlLog As String = "insert into tb_error_log ([id],[create_by],[create_date],[update_by],[update_date],[log_date],[error_message],[sql_command],[client_ip],[version])"
                sqlLog += " values(" & FindID("tb_error_log", INIFileName, SoftwareName, "frmChooseService_btnAppointment.Click") & ", 1, getdate(),null,null,"
                sqlLog += " getdate(), 'btnAppointment.btnAppointment_Click " & ex.Message & "', null, '" & Engine.Common.FunctionEng.GetIPAddress & "', '" & frmRegister.getMyVersion & "')"
                executeSQL(sqlLog, INIFileName, SoftwareName, "frmChooseService_btnAppointment.Click")
                Dim wsBl As New ShopWebServiceDisplay.WebServiceAPI
                wsBl.Url = d_webservice_url
                Dim p As New ShopWebServiceDisplay.AppointmentCheckBacklistResultPara
                p = wsBl.CheckBlackList(Mobile)
                If p.IsBackList = True Then
                    f.btnMain.Visible = True
                    f.btnPrevious.Visible = False
                    f.btnOK.Visible = False
                    If Language = "TH" Then
                        f.lblText.Text = Mobile & vbCrLf & vbCrLf & msgCannotAppoinment & vbCrLf & vbCrLf & msgPleasmakeanew & vbCrLf & msgAppointmentAgain & p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
                    Else
                        'f.lblText.Text = Mobile & vbCrLf & vbCrLf & msgCannotAppoinment & vbCrLf & vbCrLf & msgPleasmakeanew & vbCrLf & msgAppointmentAgain & vbCrLf & p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & msgOnward
                        f.lblText.Text = Mobile & vbCrLf & vbCrLf & msgCannotAppoinment & vbCrLf & msgPleasmakeanew & vbCrLf & msgAppointmentAgain & vbCrLf & p.NewAppointmentDate.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US")) & msgOnward
                    End If

                    f.btnConfirm.Visible = False
                    f.btnCancel.Visible = False
                    f.btnOK.Visible = False

                    If Language = "TH" Then
                        f.btnMain.Text = "หน้าหลัก"
                    Else
                        f.btnMain.Text = "Main Menu"
                    End If
                    p = Nothing
                    wsBl = Nothing

                    f.btnMain.Visible = True
                    f.ShowDialog()
                    btnMain.PerformClick()
                    Exit Sub
                End If
                p = Nothing
                wsBl = Nothing
            Catch ey As Exception : End Try

        End Try

        frmAppointment.Show()
        frmAppointment.ChooseService = serviceId
    End Sub

    Private Sub pb_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb.MouseMove, FLP.MouseMove
        CountVDO = 0
    End Sub


    Private Sub TimerEnd_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerEnd.Tick
        TimerEnd.Enabled = False
        'Me.Close()
        lblMsg.Text = "[H]" & vbNewLine & vbNewLine
        lblMsg.Text += "[Y]" & vbNewLine
        lblMsg.Text += "[S]" & vbNewLine & vbNewLine
        lblMsg.Text += "[X]"

        PanelMsg.Visible = False
        btnMain.Enabled = True
        btnOK.Enabled = True
        btnAppointment.Enabled = True
        frmRegister.BringToFront()
    End Sub

    Private Sub btnTH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTH.Click
        Application.DoEvents()
        Language = "TH"
        ChangeLanguage()
        RenderService()
    End Sub

    Private Sub btnEN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEN.Click
        Application.DoEvents()
        Language = "EN"
        ChangeLanguage()
        RenderService()
    End Sub


#Region "Print"

    Private Sub pd_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pd.PrintPage
        Dim eng As New KioskChooseServiceENG
        eng.PrintQueueWalkInTicket(e, INIFileName, SoftwareName, "frmChooseService_pd.PrintPage", PrintQueue)
        eng = Nothing
        e.HasMorePages = False

        CheckNotify()
    End Sub
#End Region

    'Function FindQueueNo(ByVal mobile As String, ByVal ItemID As Int32, ByVal txtQueue As String, ByVal TypeID As Int32)
    '    Dim sql As String = ""
    '    Dim dt As New DataTable
    '    sql = "declare @Min as int; select @Min = (select min_queue from TB_CUSTOMERTYPE  where id = " & TypeID & ");"
    '    sql &= "declare @Max as int; select @Max = (select max_queue from TB_CUSTOMERTYPE  where id = " & TypeID & ");"
    '    sql &= "select top 1 @Min as MinQ,@Max as MaxQ ,CONVERT(int,RIGHT(queue_no,3)) as Q "
    '    sql &= " from TB_COUNTER_QUEUE left join TB_ITEM on TB_COUNTER_QUEUE.item_id = TB_ITEM.id "
    '    sql &= " where(DateDiff(D, GETDATE(), service_date) = 0 And customertype_id = " & TypeID & ")"
    '    sql &= " and item_id = " & ItemID & " and LEFT(queue_no,1) = '" & txtQueue & "' and CONVERT(int,RIGHT(queue_no,3)) >= @Min and CONVERT(int,RIGHT(queue_no,3)) <=  @Max"
    '    sql &= " order by service_date desc"

    '    dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_FindQueueNo")
    '    If dt.Rows.Count > 0 Then
    '        If CInt(dt.Rows(0).Item("Q")) + 1 <= dt.Rows(0).Item("MaxQ") Then
    '            Return txtQueue & CInt(dt.Rows(0).Item("Q") + 1).ToString.PadLeft(3, "0")
    '        Else
    '            Return txtQueue & dt.Rows(0).Item("MinQ").ToString.PadLeft(3, "0")
    '        End If
    '    End If

    '    dt = New DataTable
    '    sql = "select min_queue from TB_CUSTOMERTYPE  where id = " & TypeID
    '    dt = getDataTable(sql, INIFileName, SoftwareName, "frmChooseService_FindQueueNo")
    '    Return txtQueue & dt.Rows(0).Item("min_queue").ToString.PadLeft(3, "0")
    'End Function

#Region "Call ShopWebService for Update Campaign and AR Barlance"
    Sub CheckCustomerCampange()
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Dim eng As New KioskBaseENG
        Dim DeBug As String = "Start " & DateTime.Now.ToString("HH:mm:ss.fff")
        Try
            'MessageBox.Show("เริ่ม Connect Gateway ตัวที่ 1")
            Dim FindCustomerInfo As New ShopWebServiceMain.WebServiceAPI
            Dim CustomerInfo As New ShopWebServiceMain.TbCustomerShParaDB
            FindCustomerInfo.Timeout = 5000
            FindCustomerInfo.Url = m_webservice_url
            CustomerInfo = FindCustomerInfo.GetCampaign(Mobile, AssetID, NetworkType)
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
                CustomerInfo = FindCustomerInfo.GetCampaign(Mobile, AssetID, NetworkType)
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
        DeBug += "End " & DateTime.Now.ToString("HH:mm:ss.fff")
        'MessageBox.Show(DeBug)
    End Sub

    'Sub UpdateCP(ByVal CAMPAIGN_CODE As String, ByVal CAMPAIGN_NAME As String, ByVal CAMPAIGN_NAME_EN As String, ByVal CAMPAIGN_DESC As String, ByVal CAMPAIGN_DESC_TH2 As String, ByVal CAMPAIGN_DESC_EN2 As String, ByVal MobileNo As String)
    '    Try
    '        Dim sql As String = ""
    '        sql = "update tb_customer set campaign_code = '" & FixDB(CAMPAIGN_CODE) & "',campaign_name = '" & FixDB(CAMPAIGN_NAME) & "',campaign_name_en = '" & FixDB(CAMPAIGN_NAME_EN) & "',campaign_desc = '" & FixDB(CAMPAIGN_DESC) & "',campaign_desc_th2 = '" & FixDB(CAMPAIGN_DESC_TH2) & "',campaign_desc_en2 = '" & FixDB(CAMPAIGN_DESC_EN2) & "' where mobile_no = '" & MobileNo & "'"
    '        executeSQL(sql, INIFileName, SoftwareName, "frmChooseService_UpdateCP")
    '    Catch ex As Exception : End Try
    'End Sub

    Sub CheckCustomerAccountBalance()
        If Mobile <> "" Then
            Dim eng As New KioskBaseENG
            Try
                Dim FindCustomerInfo As New ShopWebServiceMain.WebServiceAPI
                Dim CustomerInfo As New ShopWebServiceMain.TbCustomerShParaDB
                FindCustomerInfo.Timeout = 5000
                FindCustomerInfo.Url = m_webservice_url
                CustomerInfo = FindCustomerInfo.GetArBalance(Mobile)

                Dim sql As String = ""
                If CustomerInfo.ACCOUNT_BALANCE <> "" Then
                    eng.UpdateAR(CustomerInfo.ACCOUNT_BALANCE, Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerAccountBalance")
                Else
                    eng.UpdateAR("", Mobile, INIFileName, SoftwareName, "frmChooseService_CheckCustomerAccountBalance")
                End If
            Catch ex As Exception
                CheckCustomerAccountBalanceFromDisplay()
            End Try
            eng = Nothing
        End If
    End Sub

    Sub CheckCustomerAccountBalanceFromDisplay()
        If Mobile <> "" Then
            Dim eng As New KioskBaseENG
            Try
                Dim FindCustomerInfo As New ShopWebServiceDisplay.WebServiceAPI
                Dim CustomerInfo As New ShopWebServiceDisplay.TbCustomerShParaDB
                FindCustomerInfo.Url = d_webservice_url
                FindCustomerInfo.Timeout = 5000
                CustomerInfo = FindCustomerInfo.GetArBalance(Mobile)
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
#End Region

    Private Sub TimerCampaign_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerCampaign.Tick
        If Mobile <> "" Then
            CheckCustomerCampange()
            CheckCustomerAccountBalance()
        End If
        TimerCampaign.Enabled = False
    End Sub

    Sub UpdateCustomerImageFile(ByVal CustomerImageID As Long, ByVal QueueNo As String, ByVal AssignTime As DateTime)
        If CustomerImageID <> 0 Then
            Try
                Dim sql As String = "update tb_customer_image"
                sql += " set queue_no='" & QueueNo & "',"
                sql += " assign_time='" & AssignTime.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
                sql += " where id=" & CustomerImageID
                If ExecuteNonQuery(sql, INIFileName, SoftwareName, "frmChooseService_UpdateCustomerImageFile") = True Then
                    Dim ws As New ShopWebServiceMain.WebServiceAPI
                    ws.Url = GetWebServiceURL(True, INIFileName, SoftwareName, "frmChooseService_UpdateCustomerImageFile")
                    ws.UpdateCustCaptureIndexFile()
                    ws = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class