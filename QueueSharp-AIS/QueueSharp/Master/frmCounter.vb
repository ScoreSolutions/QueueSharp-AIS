Imports System.Data.SqlClient

Public Class frmCounter

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        cbActive.Checked = True
        cbQuick.Checked = False
        cbBackOffice.Checked = False
        cbCounterManager.Checked = False
        cbCounterTablet.Checked = False
        cbBackOffice.Enabled = True
        cbCounterManager.Enabled = True
        cbCounterTablet.Enabled = True
        txtCode.Enabled = True
        txtName.Enabled = True
        txtSearch.Enabled = False
        cbActive.Enabled = True
        cbQuick.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        cbSearch.Enabled = False
        cbSpeed.Enabled = True
        cbSpeed.Checked = False
        GridType.Enabled = True
        GridTypeAllow.Enabled = True
        nudUnitDisplay.Enabled = True
        nudUnitDisplay.Value = 1
        GridType.BackgroundColor = Color.White
        If GridType.Rows.Count > 0 Then
            For i As Int32 = 0 To GridType.Rows.Count - 1
                GridType.Rows(i).Cells("cb").Value = False
            Next
        End If

        GridTypeAllow.BackgroundColor = Color.White
        If GridTypeAllow.Rows.Count > 0 Then
            For i As Int32 = 0 To GridType.Rows.Count - 1
                GridTypeAllow.Rows(i).Cells("cb1").Value = False
            Next
        End If
    End Sub

    Sub Edit()
        txtCode.Enabled = True
        txtName.Enabled = True
        txtSearch.Enabled = False
        cbActive.Enabled = True
        cbQuick.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        cbSearch.Enabled = False
        cbSpeed.Enabled = True
        cbCounterManager.Enabled = True
        cbBackOffice.Enabled = True
        cbCounterTablet.Enabled = True
        GridType.Enabled = True
        GridTypeAllow.Enabled = True
        GridType.BackgroundColor = Color.White
        GridTypeAllow.BackgroundColor = Color.White
        nudUnitDisplay.Enabled = True
    End Sub

    Sub Clear()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtCode.Enabled = False
        txtName.Enabled = False
        txtSearch.Enabled = True
        cbActive.Checked = False
        cbActive.Enabled = False
        cbQuick.Checked = False
        cbQuick.Enabled = False
        cbSpeed.Checked = False
        cbSpeed.Enabled = False
        cbBackOffice.Checked = False
        cbBackOffice.Enabled = False
        cbCounterManager.Checked = False
        cbCounterManager.Enabled = False
        cbCounterTablet.Checked = False
        cbCounterTablet.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbSearch.Enabled = True
        nudUnitDisplay.Enabled = False
        nudUnitDisplay.Value = 1
        GridType.Enabled = False
        GridTypeAllow.Enabled = False
        GridType.BackgroundColor = Color.LightGray
        GridTypeAllow.BackgroundColor = Color.LightGray
    End Sub

    Sub ShowData()
        sql = "select id,counter_code,counter_name,active_status,quickservice,speed_lane,unitdisplay,back_office,counter_manager,case when active_status = 1 then 'Active' else 'Inactive' end as status, counter_tablet from TB_counter order by counter_name"
        dt_data = getDataTable(Sql)
        Grid.DataSource = dt_data
        SearchData()
    End Sub

    Sub ShowCustomerType()
        Dim dt As New DataTable
        sql = "select id,customertype_name  from TB_CUSTOMERTYPE where active_status = 1 order by customertype_name"
        dt = getDataTable(sql)
        GridType.DataSource = dt
        GridTypeAllow.DataSource = dt
    End Sub

    Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "counter_code like '%" & txtSearch.Text.Trim & "%' or counter_name like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "(counter_code like '%" & txtSearch.Text.Trim & "%' or counter_name like '%" & txtSearch.Text.Trim & "%') and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "(counter_code like '%" & txtSearch.Text.Trim & "%' or counter_name like '%" & txtSearch.Text.Trim & "%') and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Function Validation() As Boolean
        If txtCode.Text.Trim = "" Then
            MessageBox.Show("Please enter the Counter Code.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter the Counter Name.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If nudUnitDisplay.Text.Trim = "" Then
            MessageBox.Show("Please enter the Unit Display ID.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If


        If CheckDuplicate("TB_counter", "counter_code", txtCode.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Counter Code already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If CheckDuplicate("TB_counter", "counter_name", txtName.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Counter Name already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If nudUnitDisplay.Value > 0 Then
            If CheckDuplicate("TB_counter", "unitdisplay", nudUnitDisplay.Value.ToString, txtID.Text) = True Then
                MessageBox.Show("The Unit Display ID already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCode.Focus()
                Return False
            End If
        End If


        Dim chk As Int32 = 0
        For i As Int32 = 0 To GridType.Rows.Count - 1
            If GridType.Rows(i).Cells("cb").Value = True Then
                If GridTypeAllow.Rows(i).Cells("cb1").Value = True Then
                    MessageBox.Show("Customer Type Allow cannot be the same as the Customer Type.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
                chk = chk + 1
            End If
        Next
        If chk <> 1 Then
            MessageBox.Show("Please select only one Customer Type to save the record.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        cbSearch.SelectedIndex = 1
        ShowCustomerType()
        ShowData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then

            Dim Active As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If

            Dim quick As Int32 = 0
            If cbQuick.Checked Then
                quick = 1
            End If

            Dim Speed As Int32 = 0
            If cbSpeed.Checked Then
                Speed = 1
            End If

            Dim back_office As Int32 = 0
            If cbBackOffice.Checked Then
                back_office = 1
            End If

            Dim counter_manager As Int32 = 0
            If cbCounterManager.Checked Then
                counter_manager = 1
            End If

            Dim counter_tablet As Int32 = 0
            If cbCounterTablet.Checked Then
                counter_tablet = 1
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id_counter As String = FindID("TB_counter")
                sql = "insert into TB_counter(id,counter_code,counter_name,quickservice,active_status,unitdisplay,speed_lane,back_office,counter_manager,counter_tablet,create_by,create_date) values(" & id_counter & ",'" & FixDB(txtCode.Text) & "','" & FixDB(txtName.Text) & "'," & quick & "," & Active & ",'" & nudUnitDisplay.Value & "'," & Speed & "," & back_office & "," & counter_manager & "," & counter_tablet & "," & myUser.user_id & ",getdate())"
                executeSQL(sql)
                For i As Int32 = 0 To GridType.Rows.Count - 1
                    If GridType.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_counter_customertype")
                        sql = "insert into TB_counter_customertype(id,counter_id,customertype_id,create_by,create_date) values(" & id & "," & id_counter & "," & GridType.Rows(i).Cells("customertype_id").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next

                For i As Int32 = 0 To GridTypeAllow.Rows.Count - 1
                    If GridTypeAllow.Rows(i).Cells("cb1").Value = True Then
                        Dim id As String = FindID("TB_counter_customertype_allow")
                        sql = "insert into TB_counter_customertype_allow(id,counter_id,customertype_id,create_by,create_date) values(" & id & "," & id_counter & "," & GridTypeAllow.Rows(i).Cells("customertype_id1").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next
            Else
                'Update
                sql = "update TB_counter set counter_code = '" & FixDB(txtCode.Text) & "',counter_name = '" & FixDB(txtName.Text) & "',quickservice = '" & quick & "',active_status = " & Active & ",unitdisplay = '" & nudUnitDisplay.Value & "',speed_lane = " & Speed & ",back_office = " & back_office & ",counter_manager = " & counter_manager & ",counter_tablet=" & counter_tablet & ", update_by = " & myUser.user_id & ",update_date = getdate() where id = '" & txtID.Text & "'"
                executeSQL(sql)

                sql = "delete from TB_counter_customertype where counter_id = " & txtID.Text
                executeSQL(sql)

                For i As Int32 = 0 To GridType.Rows.Count - 1
                    If GridType.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_counter_customertype")
                        sql = "insert into TB_counter_customertype(id,counter_id,customertype_id,create_by,create_date) values(" & id & "," & txtID.Text & "," & GridType.Rows(i).Cells("customertype_id").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next

                sql = "delete from TB_counter_customertype_allow where counter_id = '" & txtID.Text & "'"
                executeSQL(sql)

                For i As Int32 = 0 To GridTypeAllow.Rows.Count - 1
                    If GridTypeAllow.Rows(i).Cells("cb1").Value = True Then
                        Dim id As String = FindID("TB_counter_customertype_allow")
                        sql = "insert into TB_counter_customertype_allow(id,counter_id,customertype_id,create_by,create_date) values(" & id & "," & txtID.Text & "," & GridTypeAllow.Rows(i).Cells("customertype_id1").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next

            End If

            MessageBox.Show("Your selected data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Clear()
            ShowData()
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            nudUnitDisplay.Focus()
        End If
    End Sub

    Private Sub nudUnitDisplay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nudUnitDisplay.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbQuick.Focus()
        End If
    End Sub

    Private Sub cbSpeed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbSpeed.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbActive.Focus()
        End If
    End Sub

    Private Sub cbActive_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbActive.KeyPress
        If Asc(e.KeyChar) = 13 Then
            GridType.Focus()
        End If
    End Sub

    Private Sub GridType_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GridType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            GridTypeAllow.Focus()
        End If
    End Sub

    Private Sub GridTypeAllow_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GridTypeAllow.KeyPress
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
            txtCode.Text = Grid.SelectedRows(0).Cells("counter_code").Value.ToString.Trim
            txtName.Text = Grid.SelectedRows(0).Cells("counter_name").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            nudUnitDisplay.Value = Grid.SelectedRows(0).Cells("unitdisplay").Value
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            cbQuick.Checked = Grid.SelectedRows(0).Cells("quickservice").Value
            cbSpeed.Checked = Grid.SelectedRows(0).Cells("speed_lane").Value
            cbBackOffice.Checked = Grid.SelectedRows(0).Cells("back_office").Value
            cbCounterManager.Checked = Grid.SelectedRows(0).Cells("counter_manager").Value
            cbCounterTablet.Checked = Grid.SelectedRows(0).Cells("counter_tablet").Value
            If GridType.Rows.Count > 0 Then
                For i As Int32 = 0 To GridType.Rows.Count - 1
                    GridType.Rows(i).Cells("cb").Value = False
                Next
            End If

            If GridTypeAllow.Rows.Count > 0 Then
                For i As Int32 = 0 To GridType.Rows.Count - 1
                    GridTypeAllow.Rows(i).Cells("cb1").Value = False
                Next
            End If


            Dim dt As New DataTable
            sql = "select * from TB_COUNTER_CUSTOMERTYPE where counter_id = " & txtID.Text
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    For j As Int32 = 0 To GridType.Rows.Count - 1
                        If Trim(dt.Rows(i).Item("customertype_id").ToString) = Trim(GridType.Rows(j).Cells("customertype_id").Value.ToString) Then
                            GridType.Rows(j).Cells("cb").Value = True
                        End If
                    Next
                Next
            End If

            If txtID.Text.Trim <> "" Then
                dt = New DataTable
                sql = "select * from TB_COUNTER_CUSTOMERTYPE_ALLOW where counter_id = " & txtID.Text
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        For j As Int32 = 0 To GridTypeAllow.Rows.Count - 1
                            If Trim(dt.Rows(i).Item("customertype_id").ToString) = Trim(GridTypeAllow.Rows(j).Cells("customertype_id1").Value.ToString) Then
                                GridTypeAllow.Rows(j).Cells("cb1").Value = True
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception : End Try

    End Sub

    Private Sub Grid_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseDoubleClick
        If Grid.RowCount > 0 Then
            Edit()
        End If
    End Sub

End Class