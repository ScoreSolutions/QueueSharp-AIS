Imports System.IO
Namespace MainDisplay
    Public Class MainDisplayENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property
        Public Function RestartMainDisplay(ByVal MainDisplayPath As String) As Boolean

            Dim ret As Boolean = KillMainDisplayProcess()
            If ret = True Then
                Try
                    Process.Start(MainDisplayPath & "MainDisplayFlash.exe")
                Catch ex As Exception
                    _err = ex.Message & vbNewLine & ex.StackTrace
                    Engine.Common.FunctionEng.SaveShopErrorLog("MainDisplayENG.RestartMainDisplay", _err)
                End Try

            End If
            Return ret
        End Function

        Public Function KillMainDisplayProcess() As Boolean
            Try
                Dim prc() As Process = Process.GetProcessesByName("MainDisplayFlash")
                If prc.Length > 0 Then
                    For Each p As Process In prc
                        p.Kill()
                    Next
                End If

                prc = Process.GetProcessesByName("run_flash_main")
                If prc.Length > 0 Then
                    For Each p As Process In prc
                        p.Kill()
                    Next
                End If
                prc = Process.GetProcessesByName("run_flash_serenade_club")
                If prc.Length > 0 Then
                    For Each p As Process In prc
                        p.Kill()
                    Next
                End If
                prc = Process.GetProcessesByName("run_flash_2nd")
                If prc.Length > 0 Then
                    For Each p As Process In prc
                        p.Kill()
                    Next
                End If

                System.Threading.Thread.Sleep(5000)
            Catch ex As Exception
                Engine.Common.FunctionEng.SaveShopErrorLog("MainDisplayENG.KillMainDisplayProcess", ex.Message & vbNewLine & ex.StackTrace)
            End Try
            Return True
        End Function
    End Class
End Namespace

