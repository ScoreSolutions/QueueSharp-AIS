Public Class frmRestartMainDisplay

    Private Sub frmRestartMainDisplay_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Minimized
        Try
            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim MainDisplayPath As String = ini.ReadString("MainDisplayPath")

            Dim mEng As New Engine.MainDisplay.MainDisplayENG
            mEng.RestartMainDisplay(MainDisplayPath)
            mEng = Nothing
        Catch ex As Exception

        End Try

        Application.Exit()
    End Sub
End Class