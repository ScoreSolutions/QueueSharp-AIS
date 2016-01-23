Imports System.Data.SqlClient
Imports QueueSharp.Org.Mentalis.Files
Imports System.IO
Imports System.Net


Public Class frmServiceConfirm

    Public CustomerID As String = ""
    Public CustomerName As String = ""
    Public CustomerTypeID As String = ""
    Public CustomerTypeName As String = ""
    Public QueueNo As String = ""

    Private Sub frmServiceConfirm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then
            If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
            Dim sql As String = ""
            sql = "delete from TB_speaker where datediff(d,getdate(),call_date)=0 and queue_no ='" & FixDB(QueueNo) & "' and counter_id = " & myUser.counter_id
            executeSQL(sql)
        End If

        Try
            frmUnitDisplay.Close()
        Catch ex As Exception
            'Do nothing
        End Try
    End Sub

    Private Sub frmServiceComfirm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblQ.Text = QueueNo
        lbltype.Text = CustomerTypeName
        lblCustomerID.Text = CustomerID
        lblName.Text = CustomerName
        Dim sql As String = ""
        Dim dt As New DataTable

        sql = "exec SP_FindCustomerInfo '" & CustomerID.Replace("-", "") & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            lblName.Text = dt.Rows(0).Item("Name").ToString
            lblEmail.Text = dt.Rows(0).Item("Email").ToString
            lblMobile_status.Text = dt.Rows(0).Item("Mobile_status").ToString
            lblBirth_date.Text = dt.Rows(0).Item("Birth_date").ToString
            lblCategory.Text = dt.Rows(0).Item("Category").ToString
            lblAcc.Text = dt.Rows(0).Item("Acc").ToString
            lblCon_class.Text = dt.Rows(0).Item("Con_class").ToString
            lblService_year.Text = dt.Rows(0).Item("Service_year").ToString
            lblChurn.Text = dt.Rows(0).Item("Churn").ToString
            lblNetwork_type.Text = dt.Rows(0).Item("Network_type").ToString
            lblSegment.Text = dt.Rows(0).Item("Segment_Level").ToString
            lblPre_lang.Text = dt.Rows(0).Item("Pre_lang").ToString
            lblBillingSystem.Text = IIf(dt.Rows(0).Item("Billing_System").ToString = "BOS", "Yes", "No")
            lblBrand_Name.Text = dt.Rows(0).Item("Brand_Name").ToString
            If InStr(dt.Rows(0).Item("Pre_lang").ToString.ToUpper, "THAI") > 0 Then
                If dt.Rows(0).Item("Camp_name").ToString.Trim = "" And dt.Rows(0).Item("campaign_desc_th2").ToString.Trim = "" Then
                    lblCamp_code.Text = ""
                    lblCamp_name.Text = ""
                    lblCamp_name1.Text = ""
                Else
                    lblCamp_code.Text = dt.Rows(0).Item("Camp_code").ToString
                    lblCamp_name.Text = dt.Rows(0).Item("Camp_name").ToString
                    lblCamp_name1.Text = dt.Rows(0).Item("campaign_desc_th2").ToString
                End If
            Else
                If dt.Rows(0).Item("campaign_desc").ToString.Trim = "" And dt.Rows(0).Item("campaign_desc_en2").ToString.Trim = "" Then
                    lblCamp_code.Text = ""
                    lblCamp_name.Text = ""
                    lblCamp_name1.Text = ""
                Else
                    lblCamp_code.Text = dt.Rows(0).Item("campaign_name_en").ToString
                    lblCamp_name.Text = dt.Rows(0).Item("campaign_desc").ToString
                    lblCamp_name1.Text = dt.Rows(0).Item("campaign_desc_en2").ToString
                End If
            End If
            Dim ImageUrl As String = dt.Rows(0).Item("url_capture").ToString
            DisplayImage(CustomerID.Replace("-", ""), ImageUrl)
        Else
            lblName.Text = "-"
            lblEmail.Text = "-"
            lblMobile_status.Text = "-"
            lblBirth_date.Text = "-"
            lblCategory.Text = "-"
            lblAcc.Text = "-"
            lblCon_class.Text = "-"
            lblService_year.Text = "-"
            lblChurn.Text = "-"
            lblCamp_code.Text = "-"
            lblCamp_name.Text = "-"
            lblPre_lang.Text = "-"
            lblNetwork_type.Text = "-"
            lblSegment.Text = "-"
            lblCamp_name1.Text = "-"
            lblBillingSystem.Text = "-"
            lblBrand_Name.Text = "-"
        End If
        '***** ShowUnitDisplay *****
        frmUnitDisplay.Owner = Me
        frmUnitDisplay.QueueNo = QueueNo
        frmUnitDisplay.Show()
        '***************************

        btnComfirm.Focus()

    End Sub

    Sub DisplayImage(ByVal MobileNo As String, ByVal ImageUrl As String)
        Try
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Url = GetWebServiceURL(True)
            Dim p As New ShopWebServiceMain.CaptureImageResponsePara
            p = ws.LoadCaptureImageFile(MobileNo)
            If p.CaptureResult Then
                Dim bipimag As New MemoryStream(p.CaptureImage)
                Dim imag As Image = New Bitmap(bipimag)
                pbDisplayImage.Image = imag
            Else
                If ImageUrl <> "" Then
                    'pbDisplayImage.ImageLocation = ImageUrl
                    pbDisplayImage.Image = GetImageFromURL(ImageUrl)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetImageFromURL(ByVal ImageUrl As String) As Image
        Dim ret As Image

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        Dim dt As New DataTable
        dt = getDataTable("select password from tb_user where username='" & myUser.username & "'")
        If dt.Rows.Count > 0 Then
            Dim req As HttpWebRequest = WebRequest.Create(ImageUrl)
            req.Method = "GET"
            req.ContentType = "image/jpeg"
            req.UserAgent = "Mozilla/4.0+(compatible;+MSIE+5.01;+Windows+NT+5.0"
            req.Credentials = New NetworkCredential(myUser.username, Engine.Common.FunctionEng.DeCripPwd(dt.Rows(0)("password")))

            Dim res As WebResponse = req.GetResponse
            ret = System.Drawing.Image.FromStream(res.GetResponseStream())
        End If
        dt.Dispose()

        Return ret
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHold5.Click
        If CheckCustomerStatus(QueueNo, CustomerID, 4) = True Then
            If CheckHold(QueueNo, CustomerID) = False Then
                UpdateQueueStatus(6, myUser.counter_id, 5, QueueNo, CustomerID)
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            'MessageBox.Show("Information has updated changed by another terminal !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHold10.Click
        If CheckCustomerStatus(QueueNo, CustomerID, 4) = True Then
            If CheckHold(QueueNo, CustomerID) = False Then
                UpdateQueueStatus(6, myUser.counter_id, 10, QueueNo, CustomerID)
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            'MessageBox.Show("Information has updated changed by another terminal !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Me.Close()
    End Sub

    Private Sub btnHold15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHold15.Click
        If CheckCustomerStatus(QueueNo, CustomerID, 4) = True Then
            If CheckHold(QueueNo, CustomerID) = False Then
                UpdateQueueStatus(6, myUser.counter_id, 15, QueueNo, CustomerID)
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            'MessageBox.Show("Information has updated changed by another terminal !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Me.Close()
    End Sub

    Private Sub btnComfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComfirm.Click
        If CheckCustomerStatus(QueueNo, CustomerID, 4) = True Then
            If CreateTransaction("brnConfirm_Click") = True Then
                If UpdateQueueStatus(2, myUser.counter_id, 0, QueueNo, CustomerID, , , shTrans) = True Then
                    CommitTransaction()
                    Me.DialogResult = Windows.Forms.DialogResult.Yes
                    Me.Close()

                    Try
                        If Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "UseSiebelPop", "QueueSharp", "frmServiceConfigm_btnConfirm.Click") = "Y" Then
                            Dim SiebelPopPath As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "SiebelPopPath", "QueueSharp", "frmServiceConfigm_btnConfirm.Click")
                            If SiebelPopPath.Trim <> "" Then
                                If Replace(Trim(lblNetwork_type.Text), "-", "") <> "" Then
                                    Dim strPathExe As String = SiebelPopPath & "SiebelPop.exe "
                                    Dim process As New Process
                                    process.StartInfo.FileName = strPathExe
                                    process.StartInfo.Arguments = Trim(lblCustomerID.Text) & ",Walk In"
                                    process.Start()
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    RollbackTransaction()
                    MessageBox.Show("Database Connection Fail!!! Please try again.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Database Connection Fail!!! Please try again.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub btnMisscall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMisscall.Click
        If CheckCustomerStatus(QueueNo, CustomerID, 4) = True Then
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select item_id,customertype_id,getdate() datenow "
            sql += " from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 "
            sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' "
            sql += " and status = 4 "
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                Dim vDateNow As String = FixDateTime(Convert.ToDateTime(dt.Rows(0)("datenow")))
                Dim CurrDB As String = Engine.Common.ShopConnectDBENG.CheckCurrentDB(INIFileName)

                sql = "update tb_counter_queue "
                sql += " set status = 8,call_time = assign_time,start_time = assign_time,end_time = assign_time,"
                sql += " user_id = " & myUser.user_id & ",counter_id = " & myUser.counter_id
                sql += " where datediff(d,getdate(),service_date)=0 "
                sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' "
                sql += " and status = 4"
                Dim ret As Boolean = executeSQL(sql)
                If ret = True Then
                    'If CurrDB = "MAIN" Then
                    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                    'End If

                    InsertLog(QueueNo, CustomerID, myUser.user_id, 0, myUser.counter_id, 8, "", Nothing, vDateNow, CurrDB)

                    If dt.Rows(0)("customertype_id") = "3" Then
                        'ถ้าเป็นคิวจอง
                        'คืน Slot
                        UpdateAppointmentSlot(CustomerID, QueueNo)

                        sql = "select appointment_job_id,customer_id,queue_no "
                        sql += " from TB_APPOINTMENT_CUSTOMER "
                        sql += " where DATEDIFF(D,GETDATE(),start_slot)=0 and active_status = 2 "
                        sql += " and customer_id = '" & FixDB(CustomerID) & "' "
                        sql += " and queue_no='" & FixDB(QueueNo) & "' "
                        Dim aDt As New DataTable
                        aDt = getDataTable(sql)
                        If aDt.Rows.Count > 0 Then
                            'Register แล้ว MissCall
                            sql = "update TB_APPOINTMENT_CUSTOMER "
                            sql += " set active_status = 4 "
                            sql += " where DATEDIFF(D,GETDATE(),app_date)=0 and active_status = 2 "
                            sql += " and customer_id = '" & FixDB(CustomerID) & "' and queue_no='" & QueueNo & "' "
                            executeSQL(sql)

                            'Update Appointment Job To DC
                            'ถ้าต้องการให้ระบบนำไปคิด Backlist ให้กำหนด status=5 แต่ถ้าไม่คิด Backlist ให้กำหนดเป็น 4
                            UpdateAppointmentJob(aDt.Rows(0)("appointment_job_id"), "4")

                            'Update Siebel when Missed Call
                            Dim dtS As DataTable = GetSiebelData(CustomerID, "4", "Registered")
                            If dtS.Rows.Count > 0 Then
                                Dim StartSlot As String = Convert.ToDateTime(dtS.Rows(0)("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))
                                Dim CancelDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("th-TH"))

                                Try
                                    Dim ws As New ShopWebServiceMain.WebServiceAPI
                                    ws.Url = GetWebServiceURL()
                                    ws.SiebelUpdateToMissed(CustomerID, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                                    ws = Nothing
                                Catch ex As Exception
                                    Dim sqlLog As String = "insert into tb_error_log "
                                    sqlLog += " values(" & FindID("tb_error_log") & ", 1, getdate(),null,null,"
                                    sqlLog += " getdate(), 'Don''t  Update Activity to Missed Call Mobile No. " & FixDB(CustomerID) & "', null, '" & Engine.Common.FunctionEng.GetIPAddress & "', '" & getMyVersion() & "', newid())"
                                    executeSQL(sqlLog)

                                    Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                                    ws.Url = GetWebServiceURL(False)
                                    ws.SiebelUpdateToMissed(CustomerID, StartSlot, CancelDate, dtS.Rows(0)("siebel_activity_id"), dtS.Rows(0)("siebel_desc"))
                                    ws = Nothing
                                End Try
                            End If
                        End If
                        aDt.Dispose()
                    End If

                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If


                'For i As Int32 = 0 To dt.Rows.Count - 1
                '    sql = "update tb_counter_queue "
                '    sql += " set status = 8,call_time = assign_time,start_time = assign_time,end_time = assign_time,"
                '    sql += " user_id = " & myUser.user_id & ",counter_id = " & myUser.counter_id
                '    sql += " where datediff(d,getdate(),service_date)=0 "
                '    sql += " and queue_no = '" & FixDB(QueueNo) & "' and customer_id = '" & FixDB(CustomerID) & "' "
                '    sql += " and item_id = " & dt.Rows(i).Item("item_id").ToString & " and status = 4"
                '    executeSQL(sql)
                '    'If CurrDB = "MAIN" Then
                '    '    Engine.Common.QueueSharpENG.ExecuteSqlToDisplay(sql, INIFileName)
                '    'End If

                '    InsertLog(QueueNo, CustomerID, myUser.user_id, dt.Rows(i).Item("item_id").ToString, myUser.counter_id, 8, "", Nothing, vDateNow, CurrDB)
                'Next
            End If
        End If
        Me.Close()
    End Sub

End Class