Public Class frmDialogConfirmPrint
    Public Msg As String = ""

    Private Sub frmDialogAppointment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblMsg.Text = Msg
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnSms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSms.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btnPrintAndSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintAndSMS.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class