Public Class frmEmergencyText

    Dim dt As New DataTable

    Private Sub frmSchedulText_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        LoadFile()
    End Sub

    Sub LoadFile()
        Dim sql As String = ""
        sql = "select time_show,txt from TV_Emergency_txt"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            txt.Text = dt.Rows(0).Item("txt").ToString
            NUD.Value = dt.Rows(0).Item("time_show").ToString
        End If
    End Sub

    Sub SaveFile_M()
        Dim sql As String = ""
        If dt.Rows.Count = 0 Then
            sql = "insert into TV_Emergency_txt(id,time_show,time,txt) values(1," & NUD.Value & ",getdate(),'" & FixDB(txt.Text) & "')"
        Else
            sql = "update TV_Emergency_txt set time_show = " & NUD.Value & ",time = getdate(),txt = '" & FixDB(txt.Text) & "'"
        End If
        executeSQL(sql)
        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "เรียบร้อย", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnSave_M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_M.Click
        SaveFile_M()
        Me.Close()
    End Sub
End Class
