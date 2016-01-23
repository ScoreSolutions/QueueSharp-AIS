Public Class frmAddWord

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    If txtWording.Text.Trim <> "" And frmMain.lbAddWord.Items.IndexOf(txtWording.Text.Trim) = -1 Then
    '        frmMain.lbAddWord.Items.Add(txtWording.Text.Trim)
    '    End If
    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmAddWord_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        ElseIf e.KeyCode = Keys.Escape Then
            Button2.PerformClick()
        End If
    End Sub

End Class