Public Class frmMessageCounter

    Dim buttonClick As Boolean = False

    Private Sub frmMessage_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If buttonClick = False Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub frmMessage_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select x.id,message,x.create_date,y.counter_name from TB_MESSAGE_COUNTER x left join TB_COUNTER y on x.counter_send = y.id where message_status = 1 and DATEDIFF(D,GETDATE(),x.create_date)=0 and counter_id = " & myUser.counter_id & " order by x.create_date asc"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            TextMessage.Text = dt.Rows(0).Item("message")
            Label3.Text = dt.Rows(0).Item("counter_name")
            sql = "update TB_MESSAGE_COUNTER set message_status = 2 where id = " & dt.Rows(0).Item("id")
            executeSQL(sql)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        buttonClick = True
        MsgShow = False
        Me.Close()
    End Sub
End Class