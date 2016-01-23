Public Class frmErrorMessage
    Dim buttonClick As Boolean = False

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        buttonClick = True
        Me.Close()
    End Sub

    Private Sub frmConnectionError_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If buttonClick = False Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

End Class