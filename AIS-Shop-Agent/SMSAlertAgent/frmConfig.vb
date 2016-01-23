Imports System.Data.SqlClient
Imports Engine.Common.FunctionEng

Public Class frmConfig


    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim ini As New IniReader(Application.StartupPath & "\Dashboard.ini")
        'ini.Section = "Setting"
        'txtMainServer.Text = ini.ReadString("Server")
        'txtMainDB.Text = ini.ReadString("Database")
        'txtMainUser.Text = ini.ReadString("Username")
        'txtMainPass.Text = DeCripPwd(ini.ReadString("Password"))
        'txtDisplayServer.Text = ini.ReadString("Server1")
        'txtDisplayDBName.Text = ini.ReadString("Database1")
        'txtDisplayUser.Text = ini.ReadString("Username1")
        'txtDisplayPass.Text = DeCripPwd(ini.ReadString("Password1"))

        'ini.Section = "XML Flash"
        'txtXMLFolder.Text = ini.ReadString("XMLFolder")
        'txtFlashFile.Text = ini.ReadString("FlashFile")
        'Dim RefreshEvery As String = Engine.Common.ShopConnectDBENG.GetShopConfig(Application.StartupPath & "\Dashboard.ini", "DashboardRefreshMinute")
        'numRefleshEvery.Value = IIf(RefreshEvery.Trim = "", 10, RefreshEvery)

        'ini = Nothing

        AddListParameter()
        txtSMSThai.Text = Engine.Common.FunctionEng.GetConfig("SMSAlertWordingThai")
        txtSMSEng.Text = Engine.Common.FunctionEng.GetConfig("SMSAlertWordingEng")

        If txtSMSThai.Text.Trim = "" Then
            txtSMSThai.Text = "คิว {QueueNo} หมายเลข {MobileNo} ใกล้ถึงคิวรับบริการ มีคิวก่อนหน้าคุณ 2 ราย"
        Else
            txtSMSThai.Text = Replace(Replace(txtSMSThai.Text, "{0}", "{QueueNo}"), "{1}", "{MobileNo}")
        End If
        If txtSMSEng.Text.Trim = "" Then
            txtSMSEng.Text = "Queue {QueueNo} Mobile {MobileNo} Near the queue service. You had 2 previous owners."
        Else
            txtSMSEng.Text = Replace(Replace(txtSMSEng.Text, "{0}", "{QueueNo}"), "{1}", "{MobileNo}")
        End If
    End Sub

    Private Sub AddListParameter()
        Dim dt As New DataTable
        dt.Columns.Add("Value")
        dt.Columns.Add("Text")

        Dim dr As DataRow = dt.NewRow
        dr("value") = "{QueueNo}"
        dr("Text") = "Queue No"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("value") = "{MobileNo}"
        dr("Text") = "Mobile No"
        dt.Rows.Add(dr)

        'Dim lItem As New lis
        lstSMSParam.DisplayMember = "Text"
        lstSMSParam.ValueMember = "value"
        lstSMSParam.DataSource = dt


    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ConfigValue As String = ""
        For Each grv As DataGridViewRow In DataGridView1.Rows
            Dim WaitingSec As Integer = 0
            If grv.Cells("colWaitingSec").Value.ToString.Trim <> "" Then
                WaitingSec = Convert.ToInt32(grv.Cells("colWaitingSec").Value)
            End If
            If WaitingSec > 0 Then
                If ConfigValue.Trim = "" Then
                    ConfigValue = grv.Cells("colid").Value & ":" & WaitingSec
                Else
                    ConfigValue += "##" & grv.Cells("colid").Value & ":" & WaitingSec
                End If
            End If
        Next

        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans.CreateTransaction()
        Dim sql As String = "update tb_setting set config_value='" & ConfigValue & "' where config_name='SMSAlertMinute'"
        Dim shLnq As New ShLinqDB.TABLE.TbSettingShLinqDB
        If shLnq.UpdateBySql(sql, shTrans.Trans) = True Then
            shTrans.CommitTransaction()
            MessageBox.Show("Save Complete!", "Save Config", MessageBoxButtons.OK)
            SetGridview()
        Else
            shTrans.RollbackTransaction()
            MessageBox.Show("Save Error! " & shTrans.ErrorMessage, "Save Config", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub SetGridview()
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetShopServiceList()
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("waiting_sec", GetType(Int16))

            Dim ConfigMin As String = Engine.Common.FunctionEng.GetConfig("SMSAlertMinute")
            If ConfigMin.Trim <> "" Then
                For i As Int16 = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("waiting_sec") = 0
                    For Each SerMin As String In Split(ConfigMin, "##")
                        If dt.Rows(i)("id") = Convert.ToInt32(Split(SerMin, ":")(0)) Then
                            dt.Rows(i)("waiting_sec") = Convert.ToInt16(Split(SerMin, ":")(1))
                        End If
                    Next
                Next
            End If

            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource = dt
        End If
        dt.Dispose()
    End Sub

    Private Sub frmConfig_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        SetGridview()
    End Sub

    Private Sub btnSaveSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSMS.Click
        If SaveSMSValidate() = True Then
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans.CreateTransaction()
            Dim SMSThai As String = Replace(Replace(txtSMSThai.Text.Trim, "{QueueNo}", "{0}"), "{MobileNo}", "{1}")
            Dim sql As String = "update tb_setting set config_value='" & SMSThai & "' where config_name='SMSAlertWordingThai'"
            Dim shLnq As New ShLinqDB.TABLE.TbSettingShLinqDB
            If shLnq.UpdateBySql(sql, shTrans.Trans) = True Then

                Dim SMSEng As String = Replace(Replace(txtSMSEng.Text.Trim, "{QueueNo}", "{0}"), "{MobileNo}", "{1}")
                sql = "update tb_setting set config_value='" & SMSEng & "' where config_name='SMSAlertWordingEng'"
                If shLnq.UpdateBySql(sql, shTrans.Trans) = True Then
                    shTrans.CommitTransaction()
                    MessageBox.Show("Save Complete!", "Save Config", MessageBoxButtons.OK)
                Else
                    shTrans.RollbackTransaction()
                    MessageBox.Show("Save Error! " & shTrans.ErrorMessage, "Save Config SMSAlertWordingEng", MessageBoxButtons.OK)
                End If
            Else
                shTrans.RollbackTransaction()
                MessageBox.Show("Save Error! " & shTrans.ErrorMessage, "Save Config SMSAlertWordingThai", MessageBoxButtons.OK)
            End If
            shLnq = Nothing
        End If
    End Sub

    Private Function SaveSMSValidate() As Boolean
        Dim ret As Boolean = True
        If txtSMSThai.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ SMS ภาษาไทย")
            txtSMSThai.Focus()
            Return False
        End If
        If txtSMSEng.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ SMS ภาษาอังกฤษ")
            txtSMSEng.Focus()
            Return False
        End If
        If InStr(txtSMSThai.Text, "{QueueNo}") = 0 Then
            MessageBox.Show("กรุณาระบุพารามิเตอร์ QueueNo")
            txtSMSThai.Focus()
            Return False
        End If
        If InStr(txtSMSThai.Text, "{MobileNo}") = 0 Then
            MessageBox.Show("กรุณาระบุพารามิเตอร์ MobileNo")
            txtSMSThai.Focus()
            Return False
        End If
        If InStr(txtSMSEng.Text, "{QueueNo}") = 0 Then
            MessageBox.Show("กรุณาระบุพารามิเตอร์ QueueNo")
            txtSMSEng.Focus()
            Return False
        End If
        If InStr(txtSMSEng.Text, "{MobileNo}") = 0 Then
            MessageBox.Show("กรุณาระบุพารามิเตอร์ MobileNo")
            txtSMSEng.Focus()
            Return False
        End If

        Return ret
    End Function
    

    Private Sub lstSMSParam_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstSMSParam.MouseDown
        DoDragDrop(lstSMSParam.SelectedValue, DragDropEffects.Move)
    End Sub
End Class