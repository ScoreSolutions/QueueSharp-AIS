Imports Engine.Common

Public Class frmSMSAlertAgent

    Dim dtSMSHis As New DataTable
    Dim dtOld As New DataTable
    Dim CurrDate As Date = DateTime.Now.Date

    Private Sub tmTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTime.Tick
        tmTime.Enabled = False
        lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", New Globalization.CultureInfo("en-US"))

        Dim tmpDate As String = lblTime.Text.Substring(0, 10)
        If CurrDate < New Date(Split(tmpDate, "/")(2), Split(tmpDate, "/")(1), Split(tmpDate, "/")(0)) Then
            CurrDate = DateTime.Now.Date
            SetAVGHT(CurrDate)

            If dtOld.Columns.Contains("item_id") = False Then
                dtOld.Columns.Add("item_id")
            End If
            If dtOld.Columns.Contains("status") = False Then
                dtOld.Columns.Add("status")
            End If

            dtSMSHis = New DataTable
            If dtSMSHis.Columns.Contains("mobile_no") = False Then
                dtSMSHis.Columns.Add("mobile_no")
            End If
            If dtSMSHis.Columns.Contains("queue_no") = False Then
                dtSMSHis.Columns.Add("queue_no")
            End If
            If dtSMSHis.Columns.Contains("service_date") = False Then
                dtSMSHis.Columns.Add("service_date")
            End If
        End If
        tmTime.Enabled = True
    End Sub

    Private Sub frmSMSAlertAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub frmSMSAlertAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "SMS Alert Agent V " & getMyVersion()
        Dim IsConnect As Boolean = False
        Do
            IsConnect = FunctionEng.CheckShopConnect()
        Loop Until IsConnect = True

        If IsConnect = True Then
            lblMinuteConfig.Text = FunctionEng.GetConfig("SMSAlertMinute")
            SetAVGHT(CurrDate)

            If dtOld.Columns.Contains("item_id") = False Then
                dtOld.Columns.Add("item_id")
            End If
            If dtOld.Columns.Contains("status") = False Then
                dtOld.Columns.Add("status")
            End If

            If dtSMSHis.Columns.Contains("mobile_no") = False Then
                dtSMSHis.Columns.Add("mobile_no")
            End If
            If dtSMSHis.Columns.Contains("queue_no") = False Then
                dtSMSHis.Columns.Add("queue_no")
            End If
            If dtSMSHis.Columns.Contains("service_date") = False Then
                dtSMSHis.Columns.Add("service_date")
            End If

            tmAlert.Enabled = True
            Me.WindowState = FormWindowState.Minimized
        End If
    End Sub
    Private Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub SetAVGHT(ByVal ServiceDateNow As DateTime)
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetAvgHT(ServiceDateNow)
        dt.Columns.Add("service_date")
        If dt.Rows.Count > 0 Then
            Dim LastMonth As DateTime = DateAdd(DateInterval.Month, -1, ServiceDateNow)
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("service_date") = LastMonth.ToString("MMMM yyyy", New Globalization.CultureInfo("en-US"))
            Next
            dgServiceList.DataSource = dt
        Else
            dgServiceList.DataSource = dt
        End If
        dt.Dispose()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        dt = dgServiceList.DataSource
        If dt.Rows.Count > 0 Then
            MessageBox.Show(dt.Rows.Count)
        End If
        dt.Dispose()
    End Sub

    Private Sub tmAlert_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmAlert.Tick
        tmAlert.Enabled = False
        Dim ConfigMin As String = lblMinuteConfig.Text
        If ConfigMin.Trim <> "" Then
            'หาจำนวนคิวและเวลาที่รอ และจำนวน Counter ที่เปิด

            'Avg HT ของเมื่อวาน
            Dim htDT As New DataTable
            htDT = dgServiceList.DataSource

            Dim dtM As New DataTable
            dtM = FunctionEng.GetQueueWaitTime()
            If dtM.Rows.Count > 0 Then
                ChkData(dtM, htDT, dtOld, ConfigMin)
                dtM.DefaultView.RowFilter = ""
                dtOld = dtM.Copy
            End If
            dtM.Dispose()

            htDT.DefaultView.RowFilter = ""
            htDT.Dispose()
        End If
        tmAlert.Enabled = True
    End Sub

    Private Sub ChkData(ByVal dt As DataTable, ByVal htDt As DataTable, ByVal dtOld As DataTable, ByVal ConfigMin As String)
        If dt.Rows.Count > 0 And (htDt IsNot Nothing Or htDt.Rows.Count > 0) Then
            For Each SerMin As String In Split(ConfigMin, "##")
                Dim ItemID As Integer = Split(SerMin, ":")(0)
                Dim ItemMin As Integer = Split(SerMin, ":")(1)

                'ถ้า AVG HT มีค่ามากกว่าค่า Config
                htDt.DefaultView.RowFilter = "item_id='" & ItemID & "' and avg_ht>" & ItemMin
                If htDt.DefaultView.Count > 0 Then
                    'ถ้าข้อมูลเดิมเป็น Serving แล้วข้อมูลไหม่เป็น End แล้ว
                    dtOld.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 2"
                    If dtOld.DefaultView.Count > 0 Then
                        For Each drOld As DataRowView In dtOld.DefaultView
                            dt.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 3 and queue_no = '" & drOld("queue_no") & "'"
                            'ถ้ามีข้อมูลแสดงว่าเป็นคิวที่เพิ่งจะ End ก็ให้ส่งค่าไป SMS โลด
                            If dt.DefaultView.Count > 0 Then
                                SendSMSAlert(ItemID, drOld("txt_queue"), drOld("customertype_id"))
                            End If
                        Next
                    End If

                    'ถ้าข้อมูลเดิมเป็น Calling แล้วข้อมูลใหม่เป็น Miss Call
                    dtOld.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 4"
                    If dtOld.DefaultView.Count > 0 Then
                        For Each drOld As DataRowView In dtOld.DefaultView
                            dt.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 8 and queue_no = '" & drOld("queue_no") & "' "
                            'ถ้ามีข้อมูลแสดงว่าเป็นคิวที่เพิ่งจะ Miss Call ก็ให้ส่งค่าไป SMS โลด
                            If dt.DefaultView.Count > 0 Then
                                SendSMSAlert(ItemID, drOld("txt_queue"), drOld("customertype_id"))
                            End If
                        Next
                    End If

                    'ถ้าข้อมูลเดิมเป็น Serving แล้วข้อมูลใหม่เป็น Cancel
                    dtOld.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 2"
                    If dtOld.DefaultView.Count > 0 Then
                        For Each drOld As DataRowView In dtOld.DefaultView
                            dt.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 5 and queue_no = '" & drOld("queue_no") & "'"
                            'ถ้ามีข้อมูลแสดงว่าเป็นคิวที่เพิ่งจะ Cancel ก็ให้ส่งค่าไป SMS โลด
                            If dt.DefaultView.Count > 0 Then
                                SendSMSAlert(ItemID, drOld("txt_queue"), drOld("customertype_id"))
                            End If
                        Next
                    End If

                    'ถ้าข้อมูลเดิมเป็น Waiting แล้วข้อมูลใหม่เป็น Cancel
                    dtOld.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 1"
                    If dtOld.DefaultView.Count > 0 Then
                        For Each drOld As DataRowView In dtOld.DefaultView
                            dt.DefaultView.RowFilter = "item_id='" & ItemID & "' and status = 5 and queue_no = '" & drOld("queue_no") & "'"
                            'ถ้ามีข้อมูลแสดงว่าเป็นคิวที่เพิ่งจะ Cancel ก็ให้ส่งค่าไป SMS โลด
                            If dt.DefaultView.Count > 0 Then
                                SendSMSAlert(ItemID, drOld("txt_queue"), drOld("customertype_id"))
                            End If
                        Next
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub SendSMSAlert(ByVal ItemID As Int32, ByVal txtQueue As String, ByVal CustomertypeID As Integer)
        Dim cAgent As Integer = FunctionEng.GetCurrentLogonAgent(ItemID, CustomertypeID)

        If cAgent > 0 Then
            Dim dt As New DataTable
            dt = FunctionEng.GetQueueForSendSMSNotify(ItemID, CustomertypeID, txtQueue, cAgent)
            If dt.Rows.Count > 0 Then
                If Convert.IsDBNull(dt.Rows(0)("customer_id")) = False Then
                    Dim MobileNo As String = dt.Rows(0)("customer_id")
                    Dim QueueNo As String = dt.Rows(0)("queue_no")

                    If MobileNo.Trim <> "" Then
                        'ลูกค้าที่เคยได้รับ SMS แล้ว ไม่ต้องส่งหาอีก กรณีจำนวน Counter มีการเปลี่ยนแปลงอาจจะทำให้ระบบคำนวณหาคิวที่จะได้รับ SMS แล้วไปเจอกับคิวที่เคยได้รับ SMS ไปแล้วในวันนั้น
                        Dim wh As String = "mobile_no='" & MobileNo & "'"
                        wh += " and queue_no='" & QueueNo & "'"
                        wh += " and service_date = '" & DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
                        dtSMSHis.DefaultView.RowFilter = wh

                        If dtSMSHis.DefaultView.Count = 0 Then
                            'ส่ง SMS หาคนที่  : (จำนวน Agent * 2)+1
                            Dim PreLang As String = ""
                            If Convert.IsDBNull(dt.Rows(0)("prefer_lang")) = False Then
                                PreLang = dt.Rows(0)("prefer_lang").ToString
                            End If

                            Dim txt As String = ""
                            If InStr(PreLang.ToUpper, "THAI") > 0 Then
                                Dim ThaiTxt As String = FunctionEng.GetConfig("SMSAlertWordingThai")
                                If ThaiTxt.Trim <> "" Then
                                    txt = String.Format(ThaiTxt, QueueNo, MobileNo)
                                End If
                            Else
                                Dim EngTxt As String = FunctionEng.GetConfig("SMSAlertWordingEng")
                                If EngTxt.Trim <> "" Then
                                    txt = String.Format(EngTxt, QueueNo, MobileNo)
                                End If
                            End If
                            If txt.Trim = "" Then
                                txt = "คิว " & dt.Rows(0)("queue_no")
                                txt += " หมายเลข " & MobileNo
                                txt += " ใกล้ถึงคิวรับบริการ มีคิวก่อนหน้าคุณ 2 ราย"
                            End If


                            Dim gw As New Engine.CallWebService.ShopWebServiceENG
                            Dim p As New ShParaDb.ShopWebService.SMSResponsePara
                            p = gw.SendSMS(MobileNo, txt)
                            Dim LogMsg As String = "Trans Log SMS : " & txt
                            If p.RESULT = False Then
                                LogMsg = "Error Log SMS : " & txt & " ### Error Msg :" & p.ERROR_RESPONSE
                            End If

                            Dim hDr As DataRow = dtSMSHis.NewRow
                            hDr("mobile_no") = MobileNo
                            hDr("queue_no") = dt.Rows(0)("queue_no")
                            hDr("service_date") = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                            dtSMSHis.Rows.Add(hDr)

                            FunctionEng.SaveShopErrorLog("frmSMSAlertAgent.SendSMSAlert", LogMsg)
                            p = Nothing
                            gw = Nothing
                        End If
                    End If
                End If
            End If
            dt.Dispose()
        End If
    End Sub

    Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        tmAlert.Enabled = False
        Dim f As New frmConfig
        f.ShowDialog()
        lblMinuteConfig.Text = FunctionEng.GetConfig("SMSAlertMinute")
        tmAlert.Enabled = True
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub
End Class