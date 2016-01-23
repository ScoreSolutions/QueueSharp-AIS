Public Class frmChannel

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        cbInActive.Checked = False
        txtCode.Enabled = True
        txtName.Enabled = True
        txtSearch.Enabled = False
        cbInActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtName.Focus()
        cbSearch.Enabled = False
    End Sub

    Sub Edit()
        txtSearch.Text = ""
        txtCode.Enabled = True
        txtName.Enabled = True
        txtSearch.Enabled = False
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbInActive.Enabled = True
        cbSearch.Enabled = False
        txtName.Focus()
    End Sub

    Sub Clear()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtCode.Enabled = False
        txtName.Enabled = False
        txtSearch.Enabled = True
        cbInActive.Checked = False
        cbInActive.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbSearch.Enabled = True
    End Sub

    Private Sub ShowData()
        sql = "select id,name,channel,active_status from TV_Channel order by name"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data
    End Sub

    Private Sub SearchData()
        Select Case cbSearch.SelectedIndex
            Case 0
                dt_data.DefaultView.RowFilter = "name like '%" & txtSearch.Text.Trim & "%'"
            Case 1
                dt_data.DefaultView.RowFilter = "name like '%" & txtSearch.Text.Trim & "%' and active_status = 1"
            Case 2
                dt_data.DefaultView.RowFilter = "name like '%" & txtSearch.Text.Trim & "%' and active_status = 0"
        End Select
    End Sub

    Private Function Validation() As Boolean
        

        If txtName.Text.Trim = "" Then
            MessageBox.Show("กรุณากรอกชื่อช่องสัญญาณ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If txtCode.Text.Trim = "" Then
            MessageBox.Show("กรุณากรอกรหัสช่องสัญญาณ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If CheckDuplicate("TV_Channel", "name", txtCode.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("ชื่อช่องสัญญาณซ้ำกับข้อมูลในระบบ !!!", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Add()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Clear()
        ShowData()
    End Sub

    Private Sub frmCustomerType_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        cbSearch.SelectedIndex = 1
        ShowData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim active_status As String = "Y"
            If cbInActive.Checked Then
                active_status = "N"
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id As String = FindID("TV_Channel")
                sql = "insert into TV_Channel(id,name,channel,active_status,update_by,update_date) values('" & id & "','" & FixDB(txtName.Text) & "','" & FixDB(txtCode.Text) & "','" & active_status & "','" & FixDB(myUser.user_id) & "',getdate())"
            Else
                'Update
                sql = "update TV_Channel set channel = '" & FixDB(txtCode.Text) & "',name = '" & FixDB(txtName.Text) & "',active_status = '" & active_status & "',update_by = '" & FixDB(myUser.user_id) & "',update_date = getdate() where id = '" & txtID.Text & "'"
            End If
            executeSQL(sql)
            MsgBox("บันทึกข้อมูลเรียบร้อย")
            Clear()
            ShowData()
            SearchData()
        End If
    End Sub

    Private Sub GridDepartment_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseDoubleClick
        If Grid.RowCount > 0 Then
            Edit()
        End If
    End Sub

    Private Sub txtDepartmentCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbInActive.Focus()
        End If
    End Sub

    Private Sub txtDepartmentName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtCode.Focus()
        End If
    End Sub

    Private Sub cbInActive_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbInActive.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        SearchData()
    End Sub

    Private Sub cbSearch_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSearch.SelectionChangeCommitted
        SearchData()
    End Sub

    Private Sub Grid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.SelectionChanged
        Try
            txtCode.Text = Grid.SelectedRows(0).Cells("channel").Value.ToString.Trim
            txtName.Text = Grid.SelectedRows(0).Cells("name").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            If Grid.SelectedRows(0).Cells("active_status").Value.ToString = "Y" Then
                cbInActive.Checked = False
            Else
                cbInActive.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Textmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

End Class