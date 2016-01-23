Imports System.IO.Ports
Imports QueueSharp.Org.Mentalis.Files

Public Class frmConfigDeviceQueue

    Public ButtonClick As Boolean = False

    Private Sub frmDeviceQueueConfig_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Dim myComPort As New SerialPort
            For i As Int32 = 0 To My.Computer.Ports.SerialPortNames.Count - 1
                cbComport.Items.Add(My.Computer.Ports.SerialPortNames(i))
            Next
        Catch ex As Exception
            ''กรณีที่ไม่มี Comport ใดเปิดใช้งานอยู่เลย จึง Add Port Com1 เข้าไปก่อน
            'cbComport.Items.Add("COM1")
        End Try

        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        If Dir(INIFileName) <> "" Then
            cbComport.SelectedIndex = cbComport.FindString("COM" & ini.ReadString("devicequeue"))
        Else
            cbComport.SelectedIndex = 0
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("devicequeue", cbComport.SelectedItem.ToString.Replace("COM", ""))
        MsgBox("Your input data is successfully been saved.", "Complete", MessageBoxButtons.OK & MessageBoxIcon.Information)
        ButtonClick = True
        Me.Close()
    End Sub

End Class