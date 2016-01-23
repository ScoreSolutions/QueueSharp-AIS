Imports OfficeOpenXml
Imports System.IO
Imports System.Data.SqlClient

Public Class frmSupportServerInfo

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim StartTime As DateTime = DateTime.Now
        PrintLog("Start Time")

        SetEnable(False)
        Try
            Dim FileName As String = Application.StartupPath & "\ServerInfo_" & txtServerName.Text & ".xlsx"
            If File.Exists(FileName) = True Then
                Try
                    File.SetAttributes(FileName, FileAttributes.Normal)
                    File.Delete(FileName)
                Catch ex As Exception

                End Try
            End If


            Using exPke As New ExcelPackage(New FileInfo(FileName))
                SetSheetServerInfo(exPke, StartTime)
                SetSheetDatabaseInfo(exPke, StartTime)
                exPke.Save()

                MessageBox.Show("Complete")

                Process.Start(FileName)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & ex.StackTrace)
        End Try

        SetEnable(True)
    End Sub

    Private Sub PrintLog(ByVal LogMsg As String)
        txtLog.Text = txtLog.Text & vbNewLine & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " " & LogMsg
    End Sub


    Private Function GetConnection() As SqlConnection
        Dim ConnStr As String = "Data Source=" & txtDbServerName.Text.Trim & ";Initial Catalog=" & txtDbName.Text.Trim & ";User ID=" & txtDbUserID.Text.Trim & ";Password=" & txtDbPassword.Text.Trim
        Dim conn As New SqlConnection(ConnStr)
        Try
            conn.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return conn
    End Function

    Private Sub SetSheetDatabaseInfo(ByVal exPke As ExcelPackage, ByVal StartTime As DateTime)
        Dim conn As SqlConnection = GetConnection()
        If conn.State = ConnectionState.Open Then
            Dim sh As ExcelWorksheet = exPke.Workbook.Worksheets.Add("DatabaseInfo")
            sh.Cells("A1").Value = "Database Information"
            sh.Cells("A2").Value = "Start Time"
            sh.Cells("B2").Value = StartTime.ToString("dd/MM/yyyy HH:mm:ss.fff")

            sh.Cells("A4").Value = "Database Name"
            sh.Cells("B4").Value = txtDbName.Text
            PrintLog("Database Name=" & txtDbName.Text)

            Dim sql As String = " SELECT database_name = DB_NAME(database_id), physical_name,type_desc"
            sql += " , file_size_mb = CAST((size) * 8. / 1024 AS DECIMAL(8,2)), "
            sql += " case max_size when -1 then 'Unlimited' else  str((max_size*8.0)/1024) end max_size_mb"
            sql += " FROM sys.master_files WITH(NOWAIT)"
            sql += " where DB_NAME(database_id)='" & txtDbName.Text.Trim & "'"
            sql += " order by database_name, type_desc desc"

            Dim dt As DataTable = GetDataTable(sql, conn)
            If dt.Rows.Count > 0 Then
                sh.Cells("A6").Value = "Database File"

                sh.Cells("A7").Value = "Database Name"
                sh.Cells("B7").Value = "File Name"
                sh.Cells("C7").Value = "Data File Type"
                sh.Cells("D7").Value = "File Size(MB)"
                sh.Cells("E7").Value = "Max File Size(MB)"

                Dim Row As Int16 = 8
                For Each dr As DataRow In dt.Rows
                    sh.Cells("A" & Row).Value = dr("database_name")
                    sh.Cells("B" & Row).Value = dr("physical_name")
                    sh.Cells("C" & Row).Value = dr("type_desc")
                    sh.Cells("D" & Row).Value = dr("file_size_mb")
                    sh.Cells("E" & Row).Value = dr("max_size_mb")
                    Row += 1

                    PrintLog("Database File=" & dr("physical_name"))
                Next
            End If
            dt.Dispose()
        End If
        conn.Close()
    End Sub


    Private Function GetDataTable(ByVal sql As String, ByVal conn As SqlConnection) As DataTable
        Dim dt As New DataTable
        Try
            If conn.State = ConnectionState.Open Then
                Dim da As New SqlDataAdapter(sql, conn)
                da.Fill(dt)
                dt.Dispose()
            End If
        Catch ex As Exception
            dt = New DataTable
        End Try
        Return dt
    End Function

    Private Sub SetSheetServerInfo(ByVal exPke As ExcelPackage, ByVal StartTime As DateTime)
        'Worksheet ServerInfo
        Dim sh As ExcelWorksheet = exPke.Workbook.Worksheets.Add("ServerInfo")
        sh.Cells("A1").Value = "Server Information"
        sh.Cells("A2").Value = "Start Time"
        sh.Cells("B2").Value = StartTime.ToString("dd/MM/yyyy HH:mm:ss.fff")

        sh.Cells("A4").Value = "Server Name"
        sh.Cells("B4").Value = txtServerName.Text
        PrintLog("Server Name=" & txtServerName.Text)

        sh.Cells("A5").Value = "IP Address"
        sh.Cells("B5").Value = txtServerIPAddress.Text
        PrintLog("IP Address=" & txtServerIPAddress.Text)

        sh.Cells("A6").Value = "CPU"
        sh.Cells("B6").Value = GetCPUInfo() & " %"
        PrintLog("CPU=" & sh.Cells("B6").Value)

        Dim RamPara As New RamInfoPara
        RamPara = GetRAMInfo()
        sh.Cells("A8").Value = "RAM"
        sh.Cells("B8").Value = "Usage"
        sh.Cells("C8").Value = RamPara.PercentUsageGB & " %"
        PrintLog("RAM Usage=" & RamPara.PercentUsageGB & " %")

        sh.Cells("B9").Value = "Total"
        sh.Cells("C9").Value = RamPara.TotalPhysicalMemoryGB & " GB"
        PrintLog("RAM Total=" & RamPara.TotalPhysicalMemoryGB & " GB")

        sh.Cells("B10").Value = "Available"
        sh.Cells("C10").Value = RamPara.AvailablePhysicalMemoryGB & " GB"
        PrintLog("RAM Available=" & RamPara.AvailablePhysicalMemoryGB & " GB")
        RamPara = Nothing


        Dim Row As Int16 = 12
        sh.Cells("A" & Row).Value = "Drive"
        sh.Cells("B" & Row).Value = "% Usage"
        sh.Cells("C" & Row).Value = "Total Size"
        sh.Cells("D" & Row).Value = "Free Space"
        Row += 1

        Dim HDDPara As New HDDInfoPara
        For Each dr As DataRow In HDDPara.GetDriveInfoToDT.Rows
            sh.Cells("A" & Row).Value = dr("DriveLetter")
            sh.Cells("B" & Row).Value = dr("PercentUsage") & " %"
            sh.Cells("C" & Row).Value = dr("TotalSizeGB") & " GB"
            sh.Cells("D" & Row).Value = dr("FreeSpaceGB") & " GB"
            PrintLog("Drive " & dr("DriveLetter") & ": Usage=" & dr("PercentUsage") & " %    Total Size=" & dr("TotalSizeGB") & " GB      Free Space=" & dr("FreeSpaceGB") & " GB")
            Row += 1
        Next
        HDDPara = Nothing
    End Sub


#Region "Server Information"
    Private Function GetRAMInfo() As RamInfoPara
        Dim ret As New RamInfoPara
        Dim ComInfo As New Devices.ComputerInfo
        ret.AvailablePhysicalMemoryGB = Math.Round((ComInfo.AvailablePhysicalMemory / (1024 ^ 3)), 2)
        ret.TotalPhysicalMemoryGB = Math.Round((ComInfo.TotalPhysicalMemory / (1024 ^ 3)), 2)
        ret.PercentUsageGB = Math.Round(((ComInfo.TotalPhysicalMemory - ComInfo.AvailablePhysicalMemory) / ComInfo.TotalPhysicalMemory) * 100, 2)

        Return ret
    End Function

    Private Function GetCPUInfo() As Double
        Dim ret As Double = 0
        Dim moReturn As Management.ManagementObjectCollection
        Dim moSearch As Management.ManagementObjectSearcher
        Dim mo As Management.ManagementObject
        moSearch = New Management.ManagementObjectSearcher("Select * from Win32_Processor")
        moReturn = moSearch.Get
        For Each mo In moReturn
            ret += Convert.ToDouble(mo("LoadPercentage"))
        Next
        moSearch.Dispose()
        moReturn.Dispose()

        Return ret
    End Function

    Public Shared Function GetIPAddress() As String
        Dim oAddr As System.Net.IPAddress
        Dim sAddr As String
        With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
            oAddr = New System.Net.IPAddress(.AddressList(0).Address)
            sAddr = oAddr.ToString
        End With
        GetIPAddress = sAddr
    End Function
#End Region

    

    Private Sub SetEnable(ByVal vEnable As Boolean)
        txtDbName.Enabled = vEnable
        txtDbUserID.Enabled = vEnable
        txtDbPassword.Enabled = vEnable
    End Sub

    Private Sub frmSupportServerInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtServerName.Text = Environment.MachineName
        txtServerIPAddress.Text = GetIPAddress()
    End Sub



End Class