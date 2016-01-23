Imports System.Data.SqlClient

Public Class frmItemIndex

    Public dt_item As New DataTable

    Private Sub frmItemIndex_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Grid.DataSource = dt_item
        For i As Int32 = 0 To Grid.Rows.Count - 1
            Grid.Rows(i).Cells("cb").Value = True
        Next
        cbAll.Checked = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmItemIndex_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim dt_ItemCheck As New DataTable
        Dim dr As DataRow
        dt_ItemCheck.Columns.Add("id", GetType(System.String))

        For i As Int32 = 0 To dt_item.Rows.Count - 1
            If Grid.Rows(i).Cells("cb").Value = True Then
                dr = dt_ItemCheck.NewRow
                dr("id") = Grid.Rows(i).Cells("id").Value.ToString
                dt_ItemCheck.Rows.Add(dr)
            End If
        Next

        If dt_ItemCheck.Rows.Count = 0 Then
            MessageBox.Show("กรุณาเลือกบริการของลูกค้า !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        dt_item = dt_ItemCheck
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAll.CheckedChanged
        If cbAll.Checked Then
            For i As Int32 = 0 To Grid.Rows.Count - 1
                Grid.Rows(i).Cells("cb").Value = True
            Next
        Else
            For i As Int32 = 0 To Grid.Rows.Count - 1
                Grid.Rows(i).Cells("cb").Value = False
            Next
        End If
    End Sub
End Class