Imports System.IO
Public Class frmTestFile

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ff As New FileInfo("D:\MyProject\QueueSharp-AIS\QualityManagement\QM\bin\Debug\TextQueueNo.txt")
        MessageBox.Show(ff.Name)
    End Sub
End Class