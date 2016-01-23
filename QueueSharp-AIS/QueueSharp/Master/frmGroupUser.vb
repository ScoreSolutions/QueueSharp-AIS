
Imports System.Data.SqlClient

Public Class frmGroupUser

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        GridMenu.BackgroundColor = Drawing.Color.White
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        GridMenu.Enabled = True
        If GridMenu.Rows.Count > 0 Then
            For i As Int32 = 0 To GridMenu.Rows.Count - 1
                GridMenu.Rows(i).Cells("cb").Value = False
            Next
        End If
        cbActive.Checked = False
        txtCode.Enabled = True
        txtName.Enabled = True
        txtSearch.Enabled = False
        cbActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        cbSearch.Enabled = False
        cbActive.Checked = True
    End Sub

    Sub Edit()
        GridMenu.BackgroundColor = Drawing.Color.White
        txtSearch.Text = ""
        GridMenu.Enabled = True
        txtCode.Enabled = True
        txtName.Enabled = True
        txtSearch.Enabled = False
        cbActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        cbSearch.Enabled = False
    End Sub

    Sub Clear()
        GridMenu.BackgroundColor = Drawing.Color.LightGray
        If GridMenu.Rows.Count > 0 Then
            For i As Int32 = 0 To GridMenu.Rows.Count - 1
                GridMenu.Rows(i).Cells("cb").Value = False
            Next
        End If
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        GridMenu.Enabled = False
        txtCode.Enabled = False
        txtName.Enabled = False
        txtSearch.Enabled = True
        cbActive.Checked = False
        cbActive.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbSearch.Enabled = True
    End Sub

    Private Sub ShowData()
        sql = "select id,group_code,group_name,active_status,case when active_status = 1 then 'Active' else 'Inactive' end as status from TB_groupuser order by group_code"
        dt_data = getDataTable(Sql)
        Grid.DataSource = dt_data
        SearchData()
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "group_code like '%" & txtSearch.Text.Trim & "%' or group_name like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "(group_code like '%" & txtSearch.Text.Trim & "%' or group_name like '%" & txtSearch.Text.Trim & "%') and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "(group_code like '%" & txtSearch.Text.Trim & "%' or group_name like '%" & txtSearch.Text.Trim & "%') and active_status = 0"
            End Select
        Catch ex As Exception : End Try

    End Sub

    Private Function Validation() As Boolean
        If txtCode.Text.Trim = "" Then
            MessageBox.Show("Please enter Group Code", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter Group Name", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If CheckDuplicate("TB_groupuser", "group_code", txtCode.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("Group Code already exists!Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If CheckDuplicate("TB_groupuser", "group_name", txtName.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("Group Name already exists!Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        Dim chk As Boolean = False
        For i As Int32 = 0 To GridMenu.Rows.Count - 1
            If GridMenu.Rows(i).Cells("cb").Value = True Then
                chk = True
                Exit For
            End If
        Next
        If chk = False Then
            MessageBox.Show("Please select the menu.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Sub frmCounter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,menu_type,menu_name from TB_menu order by menu_type,menu_name"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            GridMenu.DataSource = dt
        End If

        cbSearch.SelectedIndex = 1
        ShowData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim Active As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id_group As String = FindID("TB_groupuser")
                sql = "insert into TB_groupuser(id,group_code,group_name,active_status,create_by,create_date) values('" & id_group & "','" & FixDB(txtCode.Text) & "','" & FixDB(txtName.Text) & "'," & Active & "," & myUser.user_id & ",getdate())"
                executeSQL(sql)

                For i As Int32 = 0 To GridMenu.Rows.Count - 1
                    If GridMenu.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_groupuser_menu")
                        sql = "insert into TB_groupuser_menu(id,group_id,menu_id,create_by,create_date) values('" & id & "','" & FixDB(id_group) & "','" & FixDB(GridMenu.Rows(i).Cells("menu_id").Value) & "','" & myUser.user_id & "',getdate())"
                        executeSQL(sql)
                    End If
                Next
            Else
                'Update
                sql = "update TB_groupuser set group_code = '" & FixDB(txtCode.Text) & "',group_name = '" & FixDB(txtName.Text) & "',active_status = " & Active & ",update_by = " & myUser.user_id & ",update_date = getdate() where id = " & txtID.Text
                executeSQL(sql)

                sql = "delete from TB_groupuser_menu where group_id = " & txtID.Text
                executeSQL(sql)
                For i As Int32 = 0 To GridMenu.Rows.Count - 1
                    If GridMenu.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_groupuser_menu")
                        sql = "insert into TB_groupuser_menu(id,group_id,menu_id,update_by,update_date) values('" & id & "'," & txtID.Text & ",'" & FixDB(GridMenu.Rows(i).Cells("menu_id").Value) & "'," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next
            End If

            MessageBox.Show("You input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Clear()
            ShowData()
        End If
    End Sub

    Private Sub GridDepartment_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseDoubleClick
        If Grid.RowCount > 0 Then
            Edit()
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbActive.Focus()
        End If
    End Sub

    Private Sub cbInActive_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbActive.KeyPress
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
            txtCode.Text = Grid.SelectedRows(0).Cells("group_code").Value.ToString.Trim
            txtName.Text = Grid.SelectedRows(0).Cells("group_name").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            ShowMenu(Grid.SelectedRows(0).Cells("id").Value.ToString)
        Catch ex As Exception : End Try
    End Sub

    Private Sub GridMenu_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridMenu.CellContentClick
        Try
            If GridMenu.Rows(e.RowIndex).Cells("cb").Value = True Then
                GridMenu.Rows(e.RowIndex).Cells("cb").Value = False
            Else
                GridMenu.Rows(e.RowIndex).Cells("cb").Value = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub ShowMenu(ByVal Group_id As String)
        Dim dt As New DataTable
        sql = "select menu_id from TB_groupuser_menu where group_id = '" & FixDB(Group_id) & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To GridMenu.Rows.Count - 1
                GridMenu.Rows(i).Cells("cb").Value = False
            Next

            For i As Int32 = 0 To dt.Rows.Count - 1
                For j As Int32 = 0 To GridMenu.Rows.Count - 1
                    If Trim(dt.Rows(i).Item("menu_id").ToString) = Trim(GridMenu.Rows(j).Cells("menu_id").Value.ToString) Then
                        GridMenu.Rows(j).Cells("cb").Value = True
                    End If
                Next
            Next
        End If
    End Sub

End Class