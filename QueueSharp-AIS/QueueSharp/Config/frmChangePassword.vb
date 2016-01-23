
Public Class frmChangePassword

    Private Function ValidateData() As Boolean
        Dim ret As Boolean = True
        If txtPwd.Text.Trim = "" Then
            ret = False
            MessageBox.Show("Please enter your password.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPwd.Focus()
        ElseIf txtPwd.Text.Trim <> txtConfPwd.Text.Trim Then
            ret = False
            MessageBox.Show("Your password does not match with our database." & vbNewLine & "Please re-enter your password.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfPwd.Focus()
        End If
        Return ret
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ValidateData() = True Then
            Dim sql As String = ""
            sql = "update TB_user set [password] = '" & FixDB(txtPwd.Text.Trim) & "' where id = " & myUser.user_id
            executeSQL(sql)
            MessageBox.Show("Your password has been changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        End If
    End Sub

    Private Sub frmChangePassword_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmChangePassword_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtUsername.Text = myUser.username
        txtPwd.Focus()
    End Sub

    Private Sub txtPwd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPwd.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtConfPwd.Focus()
        End If
    End Sub

    Private Sub txtConfPwd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConfPwd.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnSave.PerformClick()
        End If
    End Sub
End Class