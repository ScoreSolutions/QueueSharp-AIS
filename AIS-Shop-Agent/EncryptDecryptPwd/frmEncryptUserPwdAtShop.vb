Imports System.Data.SqlClient
Public Class frmEncryptUserPwdAtShop

    Private Sub btnDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecrypt.Click
        Dim connStr As String = "Data Source=" & txtServerIP.Text.Trim & ";Initial Catalog=" & txtDBName.Text.Trim & ";User ID=" & txtUserName.Text.Trim & ";Password=" & txtPassword.Text.Trim
        Dim conn As New SqlConnection(connStr)
        Try
            conn.Open()
            Dim sql As String = "select fname + ' ' + lname staff_name, position, username, password, "
            sql += " case active_status when '1' then 'Active' else 'In Active' end status "
            sql += " from tb_user "
            sql += " order by fname"
            Dim da As New SqlDataAdapter(sql, conn)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("password") = Engine.Common.FunctionEng.DeCripPwd(dt.Rows(i)("password"))
                Next
                DataGridView1.AutoGenerateColumns = False
                DataGridView1.DataSource = dt
            End If
            dt.Dispose()
            da.Dispose()
        Catch ex As SqlException
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub CreateLogFile(ByVal LogText As String, ByVal FileName As String)
        Dim objWriter As New System.IO.StreamWriter(FileName, True)
        objWriter.WriteLine(LogText)
        objWriter.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If DataGridView1.RowCount > 0 Then
            Dim txt As New System.Text.StringBuilder
            txt.AppendLine("Staff Name" & vbTab & vbTab & vbTab & vbTab & "Position" & vbTab & vbTab & "User Name" & vbTab & vbTab & "Password")
            For Each dr As DataGridViewRow In DataGridView1.Rows
                txt.AppendLine(dr.Cells("colStaffName").Value & vbTab & vbTab & vbTab & vbTab & dr.Cells("colPosition").Value & vbTab & vbTab & dr.Cells("colUsername").Value & vbTab & vbTab & dr.Cells("colPassword").Value)
            Next
            Dim ssd As New System.Windows.Forms.SaveFileDialog
            ssd.Filter = "*.txt|*.txt"
            If ssd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CreateLogFile(txt.ToString, ssd.FileName)
            End If
            txt = Nothing
        End If
        
    End Sub

    Private Sub btnByUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnByUser.Click
        Dim frm As New frmEncryptDecryptPwd
        frm.ShowDialog()
    End Sub
End Class