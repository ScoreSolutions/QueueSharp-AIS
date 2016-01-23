Imports System.Data

Public Class frmItemOrder

    Sub showItem()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,item_name,item_order from TB_ITEM where active_status = 1 order by item_order asc"
        dt = getDataTable(sql)
        Grid.DataSource = dt
    End Sub

    Private Sub frmItemOrder_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub frmPriority_CusType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showItem()
    End Sub

    Private Sub ButSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Grid.RowCount > 0 Then
            Dim sql As String = ""
            For i As Integer = 0 To Grid.Rows.Count - 1
                sql = "update TB_ITEM set item_order ='" & Grid.Rows(i).Cells("item_order").Value.ToString & "' where id = '" & Grid.Rows(i).Cells("id").Value.ToString & "'"
                executeSQL(sql)
            Next
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        End If
    End Sub

End Class