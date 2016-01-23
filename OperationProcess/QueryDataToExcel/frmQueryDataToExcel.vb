Imports Engine.Common
Imports OfficeOpenXml
Imports System.IO

Public Class frmQueryDataToExcel

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dt As DataTable = FunctionEng.GetActiveShop()
        If dt.Rows.Count > 0 Then
            'Dim IsGenColumn As Boolean = False
            Dim tmp As New DataTable
            For Each dr As DataRow In dt.Rows
                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(dr("id"), "frmQueryDataToExcel.Button1_Click")
                Dim tmpSh As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(txtQuery.Text, shTrans.Trans)

                If tmpSh.Rows.Count > 0 Then
                    For Each cl As DataColumn In tmpSh.Columns
                        If tmp.Columns.Contains(cl.ColumnName) = False Then
                            tmp.Columns.Add(cl.ColumnName)
                        End If
                    Next


                    For Each tmpShDr As DataRow In tmpSh.Rows
                        Dim tmpDr As DataRow = tmp.NewRow
                        For Each cl As DataColumn In tmpSh.Columns
                            tmpDr(cl.ColumnName) = tmpShDr(cl.ColumnName)
                        Next
                        tmp.Rows.Add(tmpDr)
                    Next
                End If
                tmpSh.Dispose()
            Next

            If tmp.Rows.Count > 0 Then
                Using ep As New ExcelPackage
                    Dim sh As ExcelWorksheet = ep.Workbook.Worksheets.Add("Sheet1")

                    'Column Header
                    Dim c As Integer = 1
                    For Each tmpCl As DataColumn In tmp.Columns
                        sh.Cells(1, c).Value = tmpCl.ColumnName
                        c += 1
                    Next

                    'Data row start at row 2
                    Dim i As Integer = 2
                    For Each tmpDr As DataRow In tmp.Rows
                        c = 1
                        For Each tmpCl As DataColumn In tmp.Columns
                            If Convert.IsDBNull(tmpDr(tmpCl.ColumnName)) = False Then
                                sh.Cells(i, c).Value = tmpDr(tmpCl.ColumnName)
                            End If
                            c += 1
                        Next
                        i += 1
                    Next

                    Dim FilePath As String = Application.StartupPath & "\ExcelFile"
                    If Directory.Exists(FilePath) = False Then
                        Directory.CreateDirectory(FilePath)
                    End If

                    Dim FileName As String = FilePath & "\" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".xlsx"
                    If File.Exists(FileName) = True Then
                        File.Delete(FileName)
                    End If

                    Dim b() As Byte = ep.GetAsByteArray
                    File.WriteAllBytes(FileName, b)
                End Using
            End If
            tmp.Dispose()
        End If
        dt.Dispose()
    End Sub
End Class
