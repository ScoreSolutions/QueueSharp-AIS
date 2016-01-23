Imports System.IO
Namespace Dashboard
    Public Class DashboardENG

        Public Function KillDashboardProcess() As Boolean
            Try
                Dim prc() As Process = Process.GetProcessesByName("AIS-Dashboard")
                If prc.Length > 0 Then
                    For Each p As Process In prc
                        p.Kill()
                    Next
                End If

                prc = Process.GetProcessesByName("Dashboard")
                If prc.Length > 0 Then
                    For Each p As Process In prc
                        p.Kill()
                    Next
                End If

                System.Threading.Thread.Sleep(5000)
            Catch ex As Exception
                Engine.Common.FunctionEng.SaveShopErrorLog("DashboardENG.KillDashboardProcess", ex.Message & vbNewLine & ex.StackTrace)
            End Try
            Return True
        End Function
    End Class
End Namespace

