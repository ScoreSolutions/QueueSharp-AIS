Public Class frmDialogAppointment
    Public Msg As String = ""

    Private Sub frmDialogAppointment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblMsg.Text = Msg
    End Sub

    Private Sub btnAppOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btnAppCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub btnAppNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppNew.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAppBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppBack.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class