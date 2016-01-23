Public Class frmPriority

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        cbBefore.Enabled = True
        cbAfter.Enabled = True
        cbImm.Enabled = True
        txtSearch.Text = ""
        cbActive.Checked = False
        txtSearch.Enabled = False
        cbActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbBefore.Focus()
        txtID.Text = ""
        cbSearch.Enabled = False
        cbActive.Checked = True
        cbBefore.SelectedIndex = 0
        cbAfter.SelectedIndex = 0
    End Sub

    Sub Edit()
        cbBefore.Enabled = True
        cbAfter.Enabled = True
        cbImm.Enabled = True
        cbActive.Enabled = True
        txtSearch.Text = ""
        txtSearch.Enabled = False
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbBefore.Focus()
        cbSearch.Enabled = False
    End Sub

    Sub Clear()
        cbBefore.Enabled = False
        cbAfter.Enabled = False
        cbImm.Enabled = False
        cbBefore.SelectedIndex = 0
        cbAfter.SelectedIndex = 0
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
        sql = "select x.id,x.item_before,x.item_after,y.item_name as before,z.item_name as after,x.imm,x.active_status,case when x.active_status = 1 then 'Active' else 'Inactive' end as status from TB_item_priority x left join TB_item y on x.item_before = y.id left join TB_item z on x.item_after = z.id where y.active_status = 1 and z.active_status = 1 order by y.id"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "before like '%" & txtSearch.Text.Trim & "%' or after like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "(before like '%" & txtSearch.Text.Trim & "%' or after like '%" & txtSearch.Text.Trim & "%') and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "(before like '%" & txtSearch.Text.Trim & "%' or after like '%" & txtSearch.Text.Trim & "%') and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Private Function Validation() As Boolean
        If cbBefore.SelectedIndex = 0 Then
            MessageBox.Show("Please select Prerequisite.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbBefore.Focus()
            Return False
        End If

        If cbAfter.SelectedIndex = 0 Then
            MessageBox.Show("Please select Service Item.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbAfter.Focus()
            Return False
        End If

        If cbBefore.SelectedValue = cbAfter.SelectedValue Then
            MessageBox.Show("The Service Item is duplicate! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbBefore.Focus()
            Return False
        End If

        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select * from TB_item_priority where item_before = '" & cbBefore.SelectedValue.ToString & "' and item_after = '" & cbAfter.SelectedValue.ToString & "' and id <> '" & txtID.Text & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            MessageBox.Show("The Service Priority already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbBefore.Focus()
            Return False
        End If

        sql = "select * from TB_item_priority where item_before = '" & cbAfter.SelectedValue.ToString & "' and item_after = '" & cbBefore.SelectedValue.ToString & "' and id <> '" & txtID.Text & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            MessageBox.Show("Cannot set this condition due to the loop dependency.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbBefore.Focus()
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
        Dim sql As String = ""
        Dim dt_before As New DataTable
        Dim dt_after As New DataTable
        sql = "select 0 as id,'----- Select -----' as item_name union all select id,item_name from TB_item order by item_name asc"
        dt_before = getDataTable(sql)
        dt_after = getDataTable(sql)
        If dt_before.Rows.Count > 0 Then
            With cbBefore
                .DataSource = dt_before
                .ValueMember = "id"
                .DisplayMember = "item_name"
                .SelectedIndex = 0
            End With
            With cbAfter
                .DataSource = dt_after
                .ValueMember = "id"
                .DisplayMember = "item_name"
                .SelectedIndex = 0
            End With
        End If
        cbSearch.SelectedIndex = 1
        ShowData()
        SearchData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim Active As Int32 = 0
            Dim imm As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If
            If cbImm.Checked Then
                imm = 1
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id As String = FindID("TB_item_priority")
                sql = "insert into TB_item_priority(id,item_before,item_after,imm,active_status,create_by,create_date) values('" & id & "','" & cbBefore.SelectedValue.ToString & "','" & cbAfter.SelectedValue.ToString & "','" & imm & "','" & Active & "','" & FixDB(myUser.user_id) & "',getdate())"
            Else
                'Update
                sql = "update TB_item_priority set item_before = '" & cbBefore.SelectedValue.ToString & "',item_after = '" & cbAfter.SelectedValue.ToString & "',imm = '" & imm & "',active_status = '" & Active & "',update_by = '" & FixDB(myUser.user_id) & "',update_date = getdate() where id = '" & txtID.Text & "'"
            End If

            executeSQL(sql)
            MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        SearchData()
    End Sub

    Private Sub cbSearch_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSearch.SelectionChangeCommitted
        SearchData()
    End Sub

    Private Sub Grid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.SelectionChanged
        Try
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbBefore.SelectedValue = Grid.SelectedRows(0).Cells("item_before").Value.ToString.Trim
            cbAfter.SelectedValue = Grid.SelectedRows(0).Cells("item_after").Value.ToString.Trim
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            cbImm.Checked = Grid.SelectedRows(0).Cells("imm").Value
        Catch ex As Exception : End Try
    End Sub
End Class