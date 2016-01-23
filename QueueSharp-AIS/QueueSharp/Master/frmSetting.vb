Public Class frmSetting

    Dim dt_data As New DataTable

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '[config_name]
        '[config_value]
        '[config_desc]
        Dim config_name As String = ""
        Dim config_value As String = ""
        Dim config_desc As String = ""
        Dim New_ As Boolean = False
        Dim sql As String = ""
        Dim dt As New DataTable
       
        For i As Int32 = 1 To 23
            Select Case i
                Case 1 'มาก่อนเวลา
                    config_name = "k_before"
                    config_value = nud_k_before.Value.ToString
                    config_desc = "(Kiosk) Minutes prior to the appointment"
                Case 2 'มาสาย
                    config_name = "k_late"
                    config_value = nud_k_late.Value.ToString
                    config_desc = "(Kiosk) Too late"
                Case 3 'จองก่อนเวลา
                    config_name = "k_before_app"
                    config_value = nud_k_before_app.Value.ToString
                    config_desc = "(Kiosk) Appointment ahead of time"
                Case 4 'ยกเลิกการนัด
                    config_name = "k_cancel"
                    config_value = nud_k_Cancel.Value.ToString
                    config_desc = "(Kiosk) Cancel ahead of time"
                Case 5 'ซ่อนเวลารอถ้ามากเกินไป
                    config_name = "k_disable"
                    config_value = nud_k_disable.Value.ToString
                    config_desc = "(Kiosk) Disable Waiting time is Over"
                Case 6 'ลูกค้ารับบริการได้กี่รายการ
                    config_name = "k_serve"
                    config_value = nud_k_serve.Value.ToString
                    config_desc = "(Kiosk) Maximun served"
                Case 7 'Refresh
                    config_name = "k_refresh"
                    config_value = nud_k_refresh.Value.ToString
                    config_desc = "(Kiosk) Refresh data every"
                Case 8 'Show VDO
                    config_name = "k_vdo"
                    config_value = nud_k_vdo.Value.ToString
                    config_desc = "(Kiosk) Show VDO every"
                Case 9 'Lenght
                    config_name = "k_len"
                    config_value = nud_k_len.Value.ToString
                    config_desc = "(Kiosk) Length Mobile"
                Case 12 'อักษรน้ำหน้าเบอร์โทรศัพท์ 1
                    config_name = "k_mobile1"
                    config_value = txtMobile1.Text
                    config_desc = "(Kiosk) Ahesd Mobile 1"
                Case 13 'อักษรน้ำหน้าเบอร์โทรศัพท์ 2
                    config_name = "k_mobile2"
                    config_value = txtMobile2.Text
                    config_desc = "(Kiosk) Ahesd Mobile 2"
                Case 14 'อักษรน้ำหน้าเบอร์โทรศัพท์ 3
                    config_name = "k_mobile3"
                    config_value = txtMobile3.Text
                    config_desc = "(Kiosk) Ahesd Mobile 3"
                Case 15 'อักษรน้ำหน้าเบอร์โทรศัพท์ 4
                    config_name = "k_mobile4"
                    config_value = txtMobile4.Text
                    config_desc = "(Kiosk) Ahesd Mobile 4"
                Case 23
                    config_name = "k_max_appointment"
                    config_value = nud_k_appointment.Value
                    config_desc = "(Koisk) Max Appointment"
            End Select

            sql = "select * from TB_SETTING where config_name = '" & config_name & "'"
            dt = getDataTable(sql)
            If dt.Rows.Count = 0 Then
                Dim id As Int32 = FindID("TB_SETTING")
                sql = "insert into TB_SETTING(id,config_name,config_value,config_desc,create_by,create_date) values(" & id & ",'" & config_name & "','" & config_value & "','" & config_desc & "'," & myUser.user_id & ",getdate())"
            Else
                sql = "update TB_SETTING set config_value = '" & config_value & "',update_by = " & myUser.user_id & ",update_date = getdate() where config_name = '" & config_name & "'"

            End If
            executeSQL(sql)
        Next
        MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub frmSetting_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select * from TB_SETTING"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Try
                    Select Case dt.Rows(i).Item("config_name").ToString.Trim
                        Case "k_before" 'มาก่อนเวลา
                            nud_k_before.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_late" 'มาสาย
                            nud_k_late.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_before_app" 'จองก่อนเวลา
                            nud_k_before_app.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_cancel" 'ยกเลิกการนัด
                            nud_k_Cancel.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_disable" 'ซ่อนเวลารอถ้ามากเกินไป
                            nud_k_disable.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_serve" 'ลูกค้ารับบริการได้กี่รายการ
                            nud_k_serve.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_refresh" 'Refresh
                            nud_k_refresh.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_vdo" 'Show VDO
                            nud_k_vdo.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_len" 'Lenght
                            nud_k_len.Value = dt.Rows(i).Item("config_value").ToString
                        Case "k_mobile1" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 1
                            txtMobile1.Text = dt.Rows(i).Item("config_value").ToString
                        Case "k_mobile2" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 2
                            txtMobile2.Text = dt.Rows(i).Item("config_value").ToString
                        Case "k_mobile3" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 3
                            txtMobile3.Text = dt.Rows(i).Item("config_value").ToString
                        Case "k_mobile4" 'อักษรน้ำหน้าเบอร์โทรศัพท์ 4
                            txtMobile4.Text = dt.Rows(i).Item("config_value").ToString
                        Case "k_max_appointment"
                            nud_k_appointment.Value = dt.Rows(i).Item("config_value").ToString
                    End Select
                Catch ex As Exception : End Try
            Next
        End If
    End Sub

    Private Sub Textmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile1.KeyPress, txtMobile2.KeyPress, txtMobile3.KeyPress, txtMobile4.KeyPress
        If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub nud_k_Cancel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nud_k_Cancel.KeyPress
        e.Handled = True
    End Sub

    Private Sub nud_k_before_app_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nud_k_before_app.KeyPress
        e.Handled = True
    End Sub

    Private Sub nud_k_before_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nud_k_before.KeyPress
        e.Handled = True
    End Sub
End Class