Imports System.Data

Public Class frmWriteMessage

    Dim dt As New DataTable

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim chk As Boolean = False
        For i As Int32 = 0 To GridCounter.Rows.Count - 1
            If GridCounter.Rows(i).Cells("cb").Value = True Then
                chk = True
                Exit For
            End If
        Next
        If chk = False Then
            MessageBox.Show("Please select counter !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If txtMsg.Text.Trim = "" Then
            MessageBox.Show("Please enter text !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        For i As Int32 = 0 To GridCounter.Rows.Count - 1
            Dim sql As String = ""
            If GridCounter.Rows(i).Cells("cb").Value = True Then
                sql = "declare @ID as int; select @ID = (select isnull(MAX(id + 1),1) as id from TB_message_counter);insert into TB_message_counter(id,counter_id,counter_send,message,message_status,create_by,create_date) values(@ID," & GridCounter.Rows(i).Cells("counter_id").Value & "," & myUser.counter_id & ",'" & FixDB(txtMsg.Text) & "',1," & myUser.user_id & ",getdate())"
                executeSQL(sql)
            End If
        Next

        txtMsg.Text = ""
        MessageBox.Show("Send Success ", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()

    End Sub

    Private Sub frmWriteMessage_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,counter_name from TB_counter where active_status = '1'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            GridCounter.DataSource = dt
        End If

    End Sub


End Class