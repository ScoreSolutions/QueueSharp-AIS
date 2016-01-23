Imports QueueSharp.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG

Public Class frmHoldReason

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        txtName.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        'txtName.Enabled = True
        txtSearch.Enabled = False
        'cbActive.Enabled = True
        'cbProductive.Enabled = True
        cbProductive.Checked = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbSearch.Enabled = False
        cbActive.Checked = True
        cmbMasterHoldReason.Enabled = True
        cmbMasterHoldReason.SelectedValue = "0"
        'txtName.Focus()
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
        'cbProductive.Enabled = True
        cbSearch.Enabled = False
        cmbMasterHoldReason.Enabled = True
        'txtName.Focus()
    End Sub

    Sub Clear()
        txtName.Text = ""
        txtID.Text = ""
        'txtName.Enabled = False
        txtSearch.Enabled = True
        cbActive.Checked = False
        'cbActive.Enabled = False
        cbProductive.Checked = False
        'cbProductive.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbSearch.Enabled = True
        cmbMasterHoldReason.Enabled = False
    End Sub

    Private Sub ShowData()
        sql = "select id,name,productive,active_status,case when productive = 1 then 'Productive' else 'Non Productive' end as pro,case when active_status = 1 then 'Active' else 'Inactive' end as status,master_holdreasonid from TB_HOLD_REASON order by name"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data

        SearchData()
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "name like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "name like '%" & txtSearch.Text.Trim & "%' and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "name like '%" & txtSearch.Text.Trim & "%' and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Private Function Validation() As Boolean
        If cmbMasterHoldReason.SelectedValue = "0" Then
            MessageBox.Show("Please select Master Hold Reason.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbMasterHoldReason.Focus()
            Return False
        End If

        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter Reason.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If CheckDuplicate("TB_HOLD_REASON", "master_holdreasonid", cmbMasterHoldReason.SelectedValue, txtID.Text) = True Then
            MessageBox.Show("The Master Hold Reason already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbMasterHoldReason.Focus()
            Return False
        End If

        If CheckDuplicate("TB_HOLD_REASON", "name", txtName.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Reason already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        cbSearch.SelectedIndex = 1
        SetDDLMasterHoldReason()
        ShowData()
    End Sub

    Private Sub SetDDLMasterHoldReason()
        Dim dt As New DataTable
        dt = GetMasterHoldReasonList()
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("name") = ""
            dt.Rows.InsertAt(dr, 0)

            cmbMasterHoldReason.DataSource = dt
            cmbMasterHoldReason.DisplayMember = "name"
            cmbMasterHoldReason.ValueMember = "id"
            dt = Nothing
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim Productive As Int32 = 0
            If cbProductive.Checked Then
                Productive = 1
            End If
            Dim Active As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id As String = FindID("TB_HOLD_REASON")
                sql = "insert into TB_HOLD_REASON(id,name,productive,active_status,create_by,create_date,master_holdreasonid) values('" & id & "','" & FixDB(txtName.Text) & "'," & Productive & "," & Active & ",'" & FixDB(myUser.user_id) & "',getdate(),'" & cmbMasterHoldReason.SelectedValue & "')"
            Else
                'Update
                sql = "update TB_HOLD_REASON set name = '" & FixDB(txtName.Text) & "',productive = " & Productive & ",active_status = " & Active & ",update_by = '" & FixDB(myUser.user_id) & "',update_date = getdate(), master_holdreasonid='" & cmbMasterHoldReason.SelectedValue & "' where id = '" & txtID.Text & "'"
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
            cbProductive.Focus()
        End If
    End Sub

    Private Sub cbProductive_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbProductive.KeyPress
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
            txtName.Text = Grid.SelectedRows(0).Cells("Reason").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbProductive.Checked = Grid.SelectedRows(0).Cells("productive").Value
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            cmbMasterHoldReason.SelectedValue = IIf(Convert.IsDBNull(Grid.SelectedRows(0).Cells("master_holdreasonid").Value) = False, Grid.SelectedRows(0).Cells("master_holdreasonid").Value, "0")
        Catch ex As Exception : End Try
    End Sub

    Private Sub cmbMasterHoldReason_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMasterHoldReason.SelectionChangeCommitted
        If cmbMasterHoldReason.SelectedValue <> "0" Then
            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            Try
                'If PingServer(ini.ReadString("Server")) = True Then
                Dim ws As New ShopWebServiceMain.WebServiceAPI
                Dim para As New ShopWebServiceMain.TbHoldReasonCenParaDB
                ws.Url = GetWebServiceURL(True)
                para = ws.GetMasterHoldReasonByID(cmbMasterHoldReason.SelectedValue)
                txtName.Text = para.NAME
                cbProductive.Checked = (para.PRODUCTIVE.Value = 1)
                cbActive.Checked = (para.ACTIVE_STATUS.Value = 1)
                para = Nothing
                ws = Nothing
                'Else
                ''If PingServer(ini.ReadString("Server1")) = True Then
                ''    Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                ''    Dim para As New ShopWebServiceDisplay.TbHoldReasonCenParaDB
                ''    ws.Url = GetWebServiceURL(False)
                ''    para = ws.GetMasterHoldReasonByID(cmbMasterHoldReason.SelectedValue)
                ''    txtName.Text = para.NAME
                ''    cbProductive.Checked = (para.PRODUCTIVE.Value = 1)
                ''    cbActive.Checked = (para.ACTIVE_STATUS.Value = 1)
                ''    para = Nothing
                ''    ws = Nothing
                ''Else
                ''    txtName.Text = ""
                ''    cbProductive.Checked = False
                ''    cbActive.Checked = False
                ''End If
                'End If
            Catch ex As Exception
                'If PingServer(ini.ReadString("Server1")) = True Then
                Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                Dim para As New ShopWebServiceDisplay.TbHoldReasonCenParaDB
                ws.Url = GetWebServiceURL(False)
                para = ws.GetMasterHoldReasonByID(cmbMasterHoldReason.SelectedValue)
                txtName.Text = para.NAME
                cbProductive.Checked = (para.PRODUCTIVE.Value = 1)
                cbActive.Checked = (para.ACTIVE_STATUS.Value = 1)
                para = Nothing
                ws = Nothing
                'Else
                'txtName.Text = ""
                'cbProductive.Checked = False
                'cbActive.Checked = False
                'End If
            End Try
            ini = Nothing
        Else
            txtName.Text = ""
            cbProductive.Checked = False
            cbActive.Checked = False
        End If
    End Sub
End Class