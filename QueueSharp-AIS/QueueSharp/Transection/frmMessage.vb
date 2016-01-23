Public Class frmMessage

    Dim buttonClick As Boolean = False
    Public Message As String = ""
    Public ID As String = ""

    Private Sub frmMessage_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If buttonClick = False Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub frmMessage_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.ButSnooze.Focus()
        TextMessage.Text = Message
        ComboSnooze.Items.Add(5)
        ComboSnooze.Items.Add(10)
        ComboSnooze.Items.Add(15)
        ComboSnooze.Items.Add(20)
        ComboSnooze.Items.Add(25)
        ComboSnooze.Items.Add(30)
        ComboSnooze.SelectedIndex = ComboSnooze.FindString("5")
    End Sub

    Private Sub ButDismiss_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButDismiss.Click
        Dim sql As String = ""
        sql = "update TB_message set message_status = '3' where id =" & ID
        executeSQL(sql)
        buttonClick = True
        Me.Close()
    End Sub

    Private Sub ButSnooze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButSnooze.Click
        Dim sql As String = ""
        sql = "update TB_message set message_date = dateadd(n, " & ComboSnooze.SelectedItem.ToString & ",getdate()), message_status = 1,update_by = " & myUser.user_id & ",update_date = getdate() where id =" & ID
        executeSQL(sql)
        buttonClick = True
        Me.Close()
    End Sub

End Class