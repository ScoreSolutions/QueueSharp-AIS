Public Class frmDialogYesNo

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.No
    End Sub
    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Private Sub frmDialogTesNo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
    End Sub
End Class