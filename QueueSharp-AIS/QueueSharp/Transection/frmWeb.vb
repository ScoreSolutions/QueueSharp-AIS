Public Class frmWeb

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        WB.GoBack()
    End Sub

    Private Sub btnForword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForword.Click
        WB.GoForward()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        WB.Refresh()
    End Sub
End Class