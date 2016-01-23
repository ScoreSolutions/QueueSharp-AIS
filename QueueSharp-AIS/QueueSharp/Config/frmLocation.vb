Imports System.Data
Imports System.Data.SqlClient

Public Class frmLocation

    Dim sql As String = ""
    Dim NewLocation As Boolean

    Private Sub frmLocation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtId.Focus()
        Dim dt As New DataTable
        sql = "select * from TB_location"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            txtId.Text = dt.Rows(0)("location_code").ToString()
            txtName.Text = dt.Rows(0)("location_name").ToString()
            NewLocation = False
        Else
            NewLocation = True
        End If
    End Sub

    Private Sub ButSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() = True Then
            If NewLocation = True Then
                sql = "insert into TB_location(location_code,location_name) values('" & FixDB(txtId.Text.ToUpper) & "','" & FixDB(txtName.Text.ToUpper) & "')"
            Else
                sql = "update TB_location set location_code = '" & FixDB(txtId.Text) & "',location_name = '" & FixDB(txtName.Text.ToUpper) & "'"
            End If
            executeSQL(sql)
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub

    Private Function Validation() As Boolean
        If txtId.Text = "" Then
            MessageBox.Show("กรุณากรอกข้อมูล รหัสสถานที่ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtId.Focus()
            Return False
        End If
        If txtName.Text = "" Then
            MessageBox.Show("กรุณากรอกข้อมูล ชื่อสถานที่ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub txtId_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtId.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnSave.PerformClick()
        End If
    End Sub

    
End Class