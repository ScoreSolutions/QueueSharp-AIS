Public Class frmTestForm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox2.Text = Engine.Common.FunctionEng.EnCripPwd(TextBox1.Text)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = Engine.Common.FunctionEng.DeCripPwd(TextBox2.Text)
    End Sub
End Class