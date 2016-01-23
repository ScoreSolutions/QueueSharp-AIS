Imports System.IO

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'Dim eng As New Engine.Config.CPUConfigENG
            'Dim Mi As Engine.Config.CPUConfigENG.AlarmSeverity
            'Mi.AlarmCode = "ABC"
            'Mi.AlarmMethod = "SNMP"
            'Mi.Severity = "Minor"
            'Mi.ValueOver = 40

            'eng.SaveCPUConfig(3, Mi, Mi, Mi, 5, "Y").Save("D:\CPUConfig.xml")

            'Dim eng As New Engine.Config.CPUConfigENG
            'eng.GetConfigFromXML("D:\MyProject\QueueSharp-AIS\AIS-FaultManagement\FaultManagement\bin\Debug\Config\AKKARAWATP_CONFIG_CPU_PROCESS.xml")





            'Dim eng As New Engine.Config.RamConfigENG
            'Dim Mi As Engine.Config.RamConfigENG.AlarmSeverity
            'Mi.AlarmCode = "ABC"
            'Mi.AlarmMethod = "SNMP"
            'Mi.Severity = "Minor"
            'Mi.ValueOver = 40

            'eng.SaveRamConfig(3, Mi, Mi, Mi, 5, "Y").Save("D:\MyProject\QueueSharp-AIS\AIS-FaultManagement\FaultManagement\bin\Debug\Config\AKKARAWATP_CONFIG_RAM_PROCESS.xml")




            'If File.Exists(fileName) = True Then
            '    Dim fInfo As New FileInfo(fileName)
            '    dr("FileInfo") = fInfo
            '    dr("file_binary") = File.ReadAllBytes(fileName)
            'End If


            'Dim fm As New FaultMngService.FaultManagementService
            'fm.Url = "http://10.13.181.99/DCWebServiceAPI/FaultManagementService.asmx"
            'fm.SendFileToDC(File.ReadAllBytes("D:\Tmp\QIS_CAMP_POST_20120313.txt"), "QIS_CAMP_POST_20120313.txt", "localhost")

            'Dim fm As New FaultMngService.FaultManagementService
            'fm.Url = "http://localhost:49929/WebServiceAPI/FaultManagementService.asmx"
            'File.WriteAllBytes("D:\aaa.text", fm.LoadFileFromDC("QIS_CAMP_POST_20120313.txt"))
            'fs = 
            'fm.SendFileToDC(File.ReadAllBytes("D:\Tmp\QIS_CAMP_POST_20120313.txt"), "QIS_CAMP_POST_20120313.txt", "localhost")



        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub btnSaveHDDConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveHDDConfig.Click
        Dim eng As New Engine.Config.HDDConfigENG
        Dim inf As New Engine.Info.DriveInfoENG
        Dim dt As DataTable = inf.GetDriveInfoToDT()
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("MinorSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))
            dt.Columns.Add("MajorSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))
            dt.Columns.Add("CriticalSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim MinorSeverity As Engine.Config.ConfigENG.AlarmSeverity
                MinorSeverity.AlarmCode = "ABC"
                MinorSeverity.AlarmMethod = "SNMP"
                MinorSeverity.Severity = "Minor"
                MinorSeverity.ValueOver = 40
                dt.Rows(i)("MinorSeverity") = MinorSeverity

                Dim MajorSeverity As Engine.Config.ConfigENG.AlarmSeverity
                MajorSeverity.AlarmCode = "ABC"
                MajorSeverity.AlarmMethod = "SNMP"
                MajorSeverity.Severity = "Major"
                MajorSeverity.ValueOver = 40
                dt.Rows(i)("MajorSeverity") = MajorSeverity

                Dim CriticalSeverity As Engine.Config.ConfigENG.AlarmSeverity
                CriticalSeverity.AlarmCode = "ABC"
                CriticalSeverity.AlarmMethod = "SNMP"
                CriticalSeverity.Severity = "Critical"
                CriticalSeverity.ValueOver = 40
                dt.Rows(i)("CriticalSeverity") = CriticalSeverity
            Next

            eng.SaveHDDConfig(dt, 3, 5, "Y", "Y").Save("D:\HDDConfig.xml")
            
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim eng As New Engine.Info.WindowsServiceInfoENG("OracleVssWriterORCL1125")
        MessageBox.Show(eng.ServiceStatus)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim eng As New Engine.Info.WindowsProcessInfoENG("abc123")

    End Sub
End Class