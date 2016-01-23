Public Class frmSegment

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        txtName.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        txtName.Enabled = True
        txtSearch.Enabled = False
        cbActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbSearch.Enabled = False
        cbActive.Checked = True
        txtName.Focus()
    End Sub

    Sub Edit()
        txtSearch.Text = ""
        txtName.Enabled = True
        txtSearch.Enabled = False
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbActive.Enabled = True
        cbSearch.Enabled = False
        txtName.Focus()
    End Sub

    Sub Clear()
        txtName.Text = ""
        txtID.Text = ""
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
        If cbCustomerType.SelectedIndex >= 0 Then
            sql = "select id,segment,active_status,customertype_id,case when active_status = 1 then 'Active' else 'Inactive' end as status from tb_segment where customertype_id = " & cbCustomerType.SelectedValue & " order by segment"
            dt_data = getDataTable(sql)
            Grid.DataSource = dt_data
            SearchData()
        End If
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "segment like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "segment like '%" & txtSearch.Text.Trim & "%' and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "segment like '%" & txtSearch.Text.Trim & "%' and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Private Function Validation() As Boolean
        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter the Segment.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If CheckDuplicate("tb_segment", "segment", txtName.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Segment already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
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
        Dim dt As New DataTable
        sql = "select id,customertype_name from TB_CUSTOMERTYPE where active_status = 1 and app = 0 and def = 0 order by customertype_name"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            With cbCustomerType
                .DataSource = dt
                .ValueMember = "id"
                .DisplayMember = "customertype_name"
                .SelectedIndex = 0
            End With
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
                Dim id As String = FindID("TB_SEGMENT")
                sql = "insert into TB_SEGMENT(id,segment,customertype_id,active_status,create_by,create_date) values('" & id & "','" & FixDB(txtName.Text) & "'," & cbCustomerType.SelectedValue.ToString & "," & Active & ",'" & FixDB(myUser.user_id) & "',getdate())"
            Else
                'Update
                sql = "update TB_SEGMENT set segment = '" & FixDB(txtName.Text) & "',active_status = " & Active & ",customertype_id = " & cbCustomerType.SelectedValue.ToString & ",update_by = '" & FixDB(myUser.user_id) & "',update_date = getdate() where id = '" & txtID.Text & "'"
            End If
            executeSQL(sql)
            MessageBox.Show("Your input data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Clear()
            ShowData()
        End If
    End Sub

    Private Sub GridDepartment_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseDoubleClick
        If Grid.RowCount > 0 Then
            Edit()
        End If
    End Sub

    Private Sub txtDepartmentName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbCustomerType.Focus()
        End If
    End Sub

    Private Sub cbCustomerType_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbCustomerType.KeyPress
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
            txtName.Text = Grid.SelectedRows(0).Cells("segment").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
        Catch ex As Exception : End Try
    End Sub

    Private Sub cbCustomerType_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCustomerType.SelectionChangeCommitted
        ShowData()
    End Sub
End Class