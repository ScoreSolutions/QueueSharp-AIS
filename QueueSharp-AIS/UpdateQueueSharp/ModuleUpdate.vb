Module ModuleUpdate


    Public Sub KillProcess(ByVal ProcessName As String)
        Try
            Dim KillProcess As System.Diagnostics.Process = Process.GetProcessesByName(ProcessName)(0)
            KillProcess.Kill()
        Catch ex As Exception : End Try
    End Sub

End Module
