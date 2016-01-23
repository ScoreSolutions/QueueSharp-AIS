Public Class frmTestSetDateModify

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim f As String = "D:\AISWebQM\bin\App_Code.dll"
        'Try
        '    IO.File.SetCreationTime(f, New DateTime(2013, 10, 24, 12, 35, 53))
        '    IO.File.SetLastAccessTime(f, New DateTime(2013, 10, 24, 12, 35, 53))
        '    IO.File.SetLastWriteTime(f, New DateTime(2013, 10, 24, 12, 35, 53))
        'Catch ex As Exception

        'End Try


        Dim yy As Integer = txtDateTime.Text.Substring(0, 4)
        Dim mm As Integer = txtDateTime.Text.Substring(5, 2)
        Dim dd As Integer = txtDateTime.Text.Substring(8, 2)
        Dim hh As Integer = txtDateTime.Text.Substring(11, 2)
        Dim mi As Integer = txtDateTime.Text.Substring(14, 2)
        Dim ss As Integer = txtDateTime.Text.Substring(17, 2)

        For Each f As DataRowView In lstFile.SelectedItems
            Dim FileName As String = f("FileList")
            Try
                IO.File.SetCreationTime(f("FileList"), New DateTime(yy, mm, dd, hh, mi, ss))
                IO.File.SetLastAccessTime(f("FileList"), New DateTime(yy, mm, dd, hh, mi, ss))
                IO.File.SetLastWriteTime(f("FileList"), New DateTime(yy, mm, dd, hh, mi, ss))
            Catch ex As Exception

            End Try
        Next

    End Sub

    Private Sub frmTestSetDateModify_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fld As String = "D:\Tmp"

        lstFile.ValueMember = "FileList"
        lstFile.DisplayMember = "FileName"
        lstFile.DataSource = GetFileList(fld)

    End Sub

    Private Function GetFileList(ByVal fld As String) As System.Data.DataTable
        Dim dt As New System.Data.DataTable
        dt.Columns.Add("FileName")
        dt.Columns.Add("FileList")

        For Each f As String In IO.Directory.GetFiles(fld)
            Dim dr As DataRow = dt.NewRow
            dr("FileName") = f
            dr("FileList") = f
            dt.Rows.Add(dr)
        Next

        For Each d As String In IO.Directory.GetDirectories(fld)
            dt.Merge(GetFileList(d))
        Next

        Return dt
    End Function
End Class