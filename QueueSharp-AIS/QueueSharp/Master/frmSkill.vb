Imports QueueSharp.Org.Mentalis.Files
Public Class frmSkill

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()

        txtName.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        'txtName.Enabled = True
        txtSearch.Enabled = False
        'cbActive.Enabled = True
        'cbApp.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbSearch.Enabled = False
        cbActive.Checked = True
        'txtName.Focus()
        GridItem.Enabled = True
        If GridItem.Rows.Count > 0 Then
            For i As Int32 = 0 To GridItem.Rows.Count - 1
                GridItem.Rows(i).Cells("cb").Value = False
            Next
        End If
        cmbMasterSkill.Enabled = True
        cmbMasterSkill.SelectedValue = "0"
    End Sub

    Sub Edit()
        txtSearch.Text = ""
        'txtName.Enabled = True
        txtSearch.Enabled = False
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        'cbActive.Enabled = True
        'cbApp.Enabled = True
        cbSearch.Enabled = False
        'txtName.Focus()
        GridItem.Enabled = True
        cmbMasterSkill.Enabled = True
    End Sub

    Sub Clear()
        txtName.Text = ""
        txtID.Text = ""
        txtName.Enabled = False
        txtSearch.Enabled = True
        cbActive.Checked = False
        cbActive.Enabled = False
        cbApp.Checked = False
        cbApp.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbSearch.Enabled = True
        GridItem.Enabled = False
        If GridItem.Rows.Count > 0 Then
            For i As Int32 = 0 To GridItem.Rows.Count - 1
                GridItem.Rows(i).Cells("cb").Value = False
            Next
        End If
        cmbMasterSkill.Enabled = False
    End Sub

    Private Sub ShowData()
        sql = "select id,skill,appointment,active_status,case when active_status = 1 then 'Active' else 'Inactive' end as status,master_skillid from TB_SKILL order by skill"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data
        SearchData()
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "skill like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "skill like '%" & txtSearch.Text.Trim & "%' and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "skill like '%" & txtSearch.Text.Trim & "%' and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Private Function Validation() As Boolean
        If cmbMasterSkill.SelectedValue = "0" Then
            MessageBox.Show("Please select Master Skill", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbMasterSkill.Focus()
            Return False
        End If
        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter the Skill.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If CheckDuplicate("TB_SKILL", "skill", txtName.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Skill already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If cbApp.Checked Then
            If CheckDuplicate("TB_SKILL", "appointment", "1", txtID.Text) = True Then
                MessageBox.Show("The Skill Appointment already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cbApp.Focus()
                Return False
            End If
        End If

        Dim chk As Boolean = False
        For i As Int32 = 0 To GridItem.Rows.Count - 1
            If GridItem.Rows(i).Cells("cb").Value = True Then
                chk = True
                Exit For
            End If
        Next
        If chk = False Then
            MessageBox.Show("Please select the service.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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


        Dim sql As String = ""
        Dim dt As New DataTable
        dt = New DataTable
        sql = "select id,item_name from TB_ITEM order by item_order asc"
        dt = getDataTable(sql)
        GridItem.DataSource = dt

        SetRefMasterSkill()
        ShowData()
    End Sub

    Private Sub SetRefMasterSkill()
        Dim dt As New DataTable
        dt = GetMasterSkillList()
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("skill") = ""
            dt.Rows.InsertAt(dr, 0)

            cmbMasterSkill.DataSource = dt
            cmbMasterSkill.ValueMember = "id"
            cmbMasterSkill.DisplayMember = "skill"
            'dt.Dispose()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim Active As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If
            Dim App As Int32 = 0
            If cbApp.Checked Then
                App = 1
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id As String = FindID("TB_SKILL")
                sql = "insert into TB_SKILL(id,skill,appointment,active_status,create_by,create_date,master_skillid) values('" & id & "','" & FixDB(txtName.Text) & "'," & App & "," & Active & ",'" & FixDB(myUser.user_id) & "',getdate(),'" & cmbMasterSkill.SelectedValue & "')"
                executeSQL(sql)

                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    If GridItem.Rows(i).Cells("cb").Value = True Then
                        Dim id_skill As String = FindID("TB_SKILL_ITEM")
                        sql = "insert into TB_SKILL_ITEM(id,skill_id,item_id,create_by,create_date) values(" & id_skill & "," & id & "," & GridItem.Rows(i).Cells("item_id").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next
            Else
                'Update
                sql = "update TB_SKILL set skill = '" & FixDB(txtName.Text) & "',appointment = " & App & ",active_status = " & Active & ",update_by = '" & FixDB(myUser.user_id) & "',update_date = getdate(),master_skillid='" & cmbMasterSkill.SelectedValue & "' where id = '" & txtID.Text & "'"
                executeSQL(sql)
                sql = "delete from TB_SKILL_ITEM where skill_id = " & txtID.Text
                executeSQL(sql)

                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    If GridItem.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_SKILL_ITEM")
                        sql = "insert into TB_SKILL_ITEM(id,skill_id,item_id,create_by,create_date) values(" & id & "," & txtID.Text & "," & GridItem.Rows(i).Cells("item_id").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next
            End If

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

    Private Sub txtDepartmentName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
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
            txtName.Text = Grid.SelectedRows(0).Cells("skill").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbApp.Checked = Grid.SelectedRows(0).Cells("appointment").Value
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            If Convert.IsDBNull(Grid.SelectedRows(0).Cells("master_skillid").Value) = False Then
                If Grid.SelectedRows(0).Cells("master_skillid").Value.ToString.Trim = "" Then
                    cmbMasterSkill.SelectedValue = "0"
                Else
                    cmbMasterSkill.SelectedValue = Grid.SelectedRows(0).Cells("master_skillid").Value.ToString.Trim
                End If
            Else
                cmbMasterSkill.SelectedValue = "0"
            End If

            If GridItem.Rows.Count > 0 Then
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = False
                Next
            End If

            If txtID.Text.Trim <> "" Then
                Dim dt As New DataTable
                sql = "select * from TB_SKILL_ITEM where skill_id = " & txtID.Text
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        For j As Int32 = 0 To GridItem.Rows.Count - 1
                            If Trim(dt.Rows(i).Item("item_id").ToString) = Trim(GridItem.Rows(j).Cells("item_id").Value.ToString) Then
                                GridItem.Rows(j).Cells("cb").Value = True
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub cmbMasterSkill_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMasterSkill.SelectionChangeCommitted
        If cmbMasterSkill.SelectedValue <> "0" Then
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            Try
                Dim ws As New ShopWebServiceMain.WebServiceAPI
                Dim para As New ShopWebServiceMain.TbSkillCenParaDB
                ws.Url = GetWebServiceURL(True)
                para = ws.GetMasterSkillByID(cmbMasterSkill.SelectedValue)
                txtName.Text = para.SKILL
                cbApp.Checked = para.APPOINTMENT
                cbActive.Checked = para.ACTIVE_STATUS

                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = False
                Next

                Dim dt As New DataTable
                dt = ws.GetMasterSkillItemList(cmbMasterSkill.SelectedValue)
                For Each dr As DataRow In dt.Rows
                    Dim sql As String = "select id from tb_item where master_itemid='" & dr("master_itemid") & "'"
                    Dim tmpDt As New DataTable
                    tmpDt = getDataTable(sql)
                    If tmpDt.Rows.Count > 0 Then
                        For j As Int32 = 0 To GridItem.Rows.Count - 1
                            If Trim(tmpDt.Rows(0).Item("id").ToString) = Trim(GridItem.Rows(j).Cells("item_id").Value.ToString) Then
                                GridItem.Rows(j).Cells("cb").Value = True
                            End If
                        Next
                    End If
                    tmpDt.Dispose()
                Next
                dt.Dispose()
                para = Nothing
                ws = Nothing
            Catch ex As Exception

            End Try
            ini = Nothing
        Else
            txtName.Text = ""
            txtID.Text = ""
            cbApp.Checked = False
            cbActive.Checked = False
            If GridItem.Rows.Count > 0 Then
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = False
                Next
            End If
        End If
    End Sub
End Class