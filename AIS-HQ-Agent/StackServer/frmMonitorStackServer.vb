Imports System.Management

Public Class frmMonitorStackServer

    Private Sub frmMonitorStackServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetServerInfo()
        SetEmailGridview()
        SetSMSGridview()
        
    End Sub

    Private Sub SetEmailGridview()
        dgEmail.AutoGenerateColumns = False
        Dim eng As New Engine.MonitorStackServerENG
        dgEmail.DataSource = eng.GetStackEmail(txtIPAddress.Text)
    End Sub
    Private Sub SetSMSGridview()
        dgSMS.AutoGenerateColumns = False
        Dim eng As New Engine.MonitorStackServerENG
        dgSMS.DataSource = eng.GetStackSMS(txtIPAddress.Text)
    End Sub

    Private Sub GetServerInfo()
        Dim eng As New Engine.Common.IniConfigEng

        txtServerName.Text = Environment.MachineName
        txtLocation.Text = eng.GetServerConfig("Location")
        txtIPAddress.Text = Engine.Common.FunctionEng.GetIPAddress()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim ret As String = "Stak Time :" & DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") & vbNewLine
        ret += GetCPU()
        ret += GetRam()
        ret += GetHDD()

        txtDesc.Text = ret

        Dim para As New CenParaDB.TABLE.TbStackServerLogCenParaDB
        para.SERVER_NAME = txtServerName.Text
        para.IP_ADDRESS = txtIPAddress.Text
        para.LOCATION = txtLocation.Text
        para.STACK_DESC = txtDesc.Text
        Dim eng As New Engine.MonitorStackServerENG
        eng.SaveStackServerLog(para)
    End Sub

    Private Function GetCPU() As String
        Dim ret As String = ""
        Dim moReturn As Management.ManagementObjectCollection
        Dim moSearch As Management.ManagementObjectSearcher
        Dim mo As Management.ManagementObject
        moSearch = New Management.ManagementObjectSearcher("Select * from Win32_Processor")
        moReturn = moSearch.Get
        For Each mo In moReturn
            Dim ProcessName As String = mo("Name")
            Dim PercentUsage As String = mo("LoadPercentage")
            ret += "CPU Usage : " & PercentUsage & " %" & vbNewLine
        Next
        moSearch.Dispose()
        moReturn.Dispose()
        Return ret
    End Function

    Private Function GetRam() As String
        Dim ret As String = ""
        Dim ComInfo As New Devices.ComputerInfo
        ret = "Available Physical Memory :" & (ComInfo.AvailablePhysicalMemory / 1024 / 1024 / 1024).ToString("#,##0.00") & " GB" & vbNewLine
        Return ret
    End Function

    Private Function GetHDD() As String
        Dim desc As String = " HDD Info :" & vbNewLine
        Dim drives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives
        For Each dri As System.IO.DriveInfo In drives
            If dri.IsReady = True Then
                desc += "Drive Name : " & dri.Name & vbNewLine
                desc += "Volume Label : " & dri.VolumeLabel & vbNewLine
                desc += "Total Free Space : " & (dri.TotalFreeSpace / (1024 ^ 3)).ToString("#,##0.00") & " GB" & vbNewLine
                desc += "TotalSize : " & (dri.TotalSize / (1024 ^ 3)).ToString("#,##0.00") & " GB" & vbNewLine & vbNewLine
            End If
        Next

        Return desc
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim eng As New Engine.Common.IniConfigEng
        eng.SetServerConfig("Location", txtLocation.Text)
        MessageBox.Show("Success!!!")
    End Sub

    Private Sub btnEmailAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailAdd.Click
        Dim eng As New Engine.MonitorStackServerENG
        Dim para As New CenParaDB.TABLE.TbStackServerEmailCenParaDB
        para.SERVER_NAME = txtServerName.Text
        para.IP_ADDRESS = txtIPAddress.Text
        para.LOCATION = txtLocation.Text
        para.EMAIL_TO = txtEmailTo.Text

        If eng.SaveStackEmail(para) = True Then
            txtEmailTo.Text = ""
            SetEmailGridview()
            MessageBox.Show("Save Complete")
        Else
            MessageBox.Show(eng.ErrorMessage)
        End If
    End Sub

    Private Sub btnEmailDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailDelete.Click
        For Each dg As DataGridViewRow In dgEmail.Rows
            If dg.Cells("email_to").Selected = True Then
                Dim eng As New Engine.MonitorStackServerENG
                eng.DeleteStackEmail(dg.Cells("id").Value)
            End If
        Next
        SetEmailGridview()
    End Sub

    Private Sub btnSmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmsAdd.Click
        Dim eng As New Engine.MonitorStackServerENG
        Dim para As New CenParaDB.TABLE.TbStackServerSmsCenParaDB
        para.SERVER_NAME = txtServerName.Text
        para.IP_ADDRESS = txtIPAddress.Text
        para.LOCATION = txtLocation.Text
        para.SMS_TO = txtMobileNo.Text

        If eng.SaveStackSMS(para) = True Then
            txtMobileNo.Text = ""
            SetSMSGridview()
            MessageBox.Show("Save Complete")
        Else
            MessageBox.Show(eng.ErrorMessage)
        End If
    End Sub

    Private Sub btnSmsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmsDelete.Click
        For Each dg As DataGridViewRow In dgSMS.Rows
            If dg.Cells("sms_to").Selected = True Then
                Dim eng As New Engine.MonitorStackServerENG
                eng.DeleteStackSms(dg.Cells("sms_id").Value)
            End If
        Next
        SetSMSGridview()
    End Sub
End Class