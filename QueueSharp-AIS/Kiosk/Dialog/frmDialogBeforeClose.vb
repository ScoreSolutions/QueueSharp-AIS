
Public Class frmDialogBeforeClose

    Public Message As String = ""

    Private Sub frmDialogBeforeClose_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblTxt.Text = Message
    End Sub

    Private Sub TimerEnd_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerEnd.Tick
        Me.Close()
    End Sub
End Class