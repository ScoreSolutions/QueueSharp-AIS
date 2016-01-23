Imports Engine.Common
Public Class frmEncryptDecryptPwd

    Private Sub btnEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEncrypt.Click
        If txtEncrypt.Text.Trim <> "" Then
            txtDecrypt.Text = FunctionEng.EnCripPwd(txtEncrypt.Text.Trim)
        End If
    End Sub

    Private Sub btnDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecrypt.Click
        If txtDecrypt.Text.Trim <> "" Then
            txtEncrypt.Text = FunctionEng.DeCripPwd(txtDecrypt.Text.Trim)
        End If
    End Sub
End Class