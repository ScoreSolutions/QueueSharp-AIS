Imports System.IO

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
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
End Class