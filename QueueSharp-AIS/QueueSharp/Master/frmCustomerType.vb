Imports QueueSharp.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG

Public Class frmCustomerType

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        txtQueue.Text = ""
        txtMin.Text = ""
        txtMax.Text = ""
        lblColor.BackColor = Drawing.Color.White
        txtQueue.Enabled = False
        lblQueue.Enabled = False
        txtMin.Enabled = True
        txtMax.Enabled = True
        lblColor.Enabled = True
        'txtCode.Enabled = True
        'txtName.Enabled = True
        txtSearch.Enabled = False
        'cbActive.Enabled = True
        'cbApp.Enabled = True
        'cbDef.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        cbSearch.Enabled = False
        cbDef.Checked = False
        cbApp.Checked = False
        cbActive.Checked = True
        cmbMasterCustomertype.Enabled = True
        cmbMasterCustomertype.SelectedValue = "0"
    End Sub

    Sub Edit()
        txtSearch.Text = ""
        txtMin.Enabled = True
        txtMax.Enabled = True
        'txtCode.Enabled = True
        'txtName.Enabled = True
        txtSearch.Enabled = False
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        'cbActive.Enabled = True
        'cbApp.Enabled = True
        'cbDef.Enabled = True
        txtCode.Focus()
        cbSearch.Enabled = False
        cmbMasterCustomertype.Enabled = True

        If cbApp.Checked Then
            lblQueue.Enabled = True
            'txtQueue.Enabled = True
            txtMin.Enabled = False
            txtMax.Enabled = False
        Else
            lblQueue.Enabled = False
            'txtQueue.Enabled = False
            txtQueue.Text = ""
            txtMin.Enabled = True
            txtMax.Enabled = True
        End If
    End Sub

    Sub Clear()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        cbApp.Checked = False
        cbApp.Enabled = False
        lblColor.BackColor = Drawing.Color.White
        txtCode.Enabled = False
        txtName.Enabled = False
        txtQueue.Enabled = False
        txtQueue.Text = ""
        lblQueue.Enabled = False
        txtMin.Enabled = False
        txtMax.Enabled = False
        txtSearch.Enabled = True
        cbActive.Checked = False
        cbActive.Enabled = False
        cbDef.Checked = False
        cbDef.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbSearch.Enabled = True
        cmbMasterCustomertype.Enabled = False
    End Sub

    Private Sub ShowData()
        sql = "select id,customertype_code,customertype_name,active_status,app,def,min_queue,max_queue,txt_queue,color,case when active_status = 1 then 'Active' else 'Inactive' end as status,master_customertypeid from TB_customertype order by customertype_code"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data
        SearchData()
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "customertype_code like '%" & txtSearch.Text.Trim & "%' or customertype_name like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "(customertype_code like '%" & txtSearch.Text.Trim & "%' or customertype_name like '%" & txtSearch.Text.Trim & "%') and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "(customertype_code like '%" & txtSearch.Text.Trim & "%' or customertype_name like '%" & txtSearch.Text.Trim & "%') and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Private Function Validation() As Boolean
        '
        If cmbMasterCustomertype.SelectedValue = "0" Then
            MessageBox.Show("Please select Master Customer Type.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbMasterCustomertype.Focus()
            Return False
        End If

        If txtCode.Text.Trim = "" Then
            MessageBox.Show("Please enter Customer Type Code.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter Customer Type Name.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If cbApp.Checked = False Then
            If txtMin.Text.Trim = "" Then
                MessageBox.Show("Please enter Min Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtMin.Focus()
                Return False
            End If

            If txtMax.Text.Trim = "" Then
                MessageBox.Show("Please enter Max Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtMax.Focus()
                Return False
            End If

            If CInt(txtMin.Text) >= CInt(txtMax.Text) Then
                MessageBox.Show("Incorrect input data.The Min Queue cannot be less than Max Queue." & vbNewLine & "Please re-enter the new input.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtMax.Focus()
                Exit Function
            End If
        End If

        If CheckDuplicate("TB_customertype", "master_customertypeid", cmbMasterCustomertype.SelectedValue, txtID.Text) = True Then
            MessageBox.Show("The Master Customer Type already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If CheckDuplicate("TB_customertype", "customertype_code", txtCode.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Customer Type Code already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If cbApp.Checked Then
            If CheckDuplicate("TB_customertype", "app", "1", txtID.Text) = True Then
                MessageBox.Show("The Customer Type Appointment already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCode.Focus()
                Return False
            Else
                If txtQueue.Text.Trim = "" Then
                    MessageBox.Show("Please enter Text Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtQueue.Focus()
                    Return False
                End If
            End If
        End If

        If cbDef.Checked Then
            If CheckDuplicate("TB_customertype", "def", "1", txtID.Text) = True Then
                MessageBox.Show("The Default Customer Type already exists for other Customer Type Code.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCode.Focus()
                Return False
            End If
        End If
        If txtQueue.Text.Length > 0 Then
            If Asc(txtQueue.Text) < 65 Or Asc(txtQueue.Text) > 90 Then
                MessageBox.Show("You must enter English Alphabets only!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQueue.Focus()
                Return False
            End If
        End If


        If cbApp.Checked Then
            txtMin.Text = ""
            txtMax.Text = ""
        Else
            txtQueue.Text = ""
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
        SetMasterCustomertype()
        ShowData()
    End Sub

    Private Sub SetMasterCustomertype()
        Dim dt As New DataTable
        dt = GetMasterCustomerTypeList()
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("customertype_name") = ""
            dt.Rows.InsertAt(dr, 0)

            cmbMasterCustomertype.DataSource = dt
            cmbMasterCustomertype.DisplayMember = "customertype_name"
            cmbMasterCustomertype.ValueMember = "id"
            dt = Nothing
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then

            Dim tmpColor As String = ""
            If lblColor.BackColor = Drawing.Color.White Then
                tmpColor = "NULL"
            Else
                tmpColor = lblColor.BackColor.ToArgb.ToString()
            End If

            Dim Active As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If

            Dim App As Int32 = 0
            If cbApp.Checked Then
                App = 1
            End If

            Dim Def As Int32 = 0
            If cbDef.Checked Then
                Def = 1
            End If

            If txtID.Text = "" Then
                'Insert
                Dim id As String = FindID("TB_customertype")
                sql = "insert into TB_customertype(id,customertype_code,customertype_name,color,active_status,app,def,create_by,create_date,min_queue,max_queue,txt_queue,priority_value,master_customertypeid) values('" & id & "','" & FixDB(txtCode.Text) & "','" & FixDB(txtName.Text) & "'," & tmpColor & "," & Active & "," & App & "," & Def & ",'" & FixDB(myUser.user_id) & "',getdate(),'" & txtMin.Text & "','" & txtMax.Text & "','" & FixDB(txtQueue.Text) & "',1,'" & cmbMasterCustomertype.SelectedValue & "')"
            Else
                'Update
                sql = "update TB_customertype set customertype_code = '" & FixDB(txtCode.Text) & "',customertype_name = '" & FixDB(txtName.Text) & "',color = " & tmpColor & ",active_status = " & Active & ",app = " & App & " ,def = " & Def & ",txt_queue = '" & FixDB(txtQueue.Text) & "',update_by = '" & FixDB(myUser.user_id) & "',update_date = getdate(),min_queue = '" & txtMin.Text & "',max_queue = '" & txtMax.Text & "',priority_value = 1,master_customertypeid='" & cmbMasterCustomertype.SelectedValue & "' where id = '" & txtID.Text & "'"
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

    Private Sub txtDepartmentCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtDepartmentName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtMin.Focus()
        End If
    End Sub

    Private Sub txtMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMin.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtMax.Focus()
        End If
    End Sub

    Private Sub txtMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMax.KeyPress
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
            txtCode.Text = Grid.SelectedRows(0).Cells("customertype_code").Value.ToString.Trim
            txtName.Text = Grid.SelectedRows(0).Cells("customertype_name").Value.ToString.Trim
            txtMin.Text = Grid.SelectedRows(0).Cells("min_queue").Value.ToString.Trim
            txtMax.Text = Grid.SelectedRows(0).Cells("max_queue").Value.ToString.Trim
            txtQueue.Text = Grid.SelectedRows(0).Cells("txt_queue").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            cbApp.Checked = Grid.SelectedRows(0).Cells("app").Value
            cbDef.Checked = Grid.SelectedRows(0).Cells("def").Value
            If Grid.SelectedRows(0).Cells("master_customertypeid").Value.ToString.Trim <> "" Then
                cmbMasterCustomertype.SelectedValue = Grid.SelectedRows(0).Cells("master_customertypeid").Value
            Else
                cmbMasterCustomertype.SelectedValue = "0"
            End If
            If Grid.SelectedRows(0).Cells("color").Value.ToString <> "" And IsNumeric(Grid.SelectedRows(0).Cells("color").Value.ToString) Then
                lblColor.BackColor = Drawing.Color.FromArgb(CInt(Grid.SelectedRows(0).Cells("color").Value.ToString))
            Else
                lblColor.BackColor = Drawing.Color.White
            End If

            If txtID.Text = "1" Or txtID.Text = "2" Or txtID.Text = "3" Then

            End If

        Catch ex As Exception : End Try
    End Sub

    Private Sub Textmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMin.KeyPress, txtMax.KeyPress
        If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub lblColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblColor.Click
        If btnSave.Enabled = True Then
            ColorPicker.Color = lblColor.BackColor
            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then
                lblColor.BackColor = ColorPicker.Color
            End If
        End If
    End Sub

    Private Sub cbApp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbApp.CheckedChanged
        If cbApp.Enabled = True Then
            If cbApp.Checked Then
                lblQueue.Enabled = True
                'txtQueue.Enabled = True
                txtMin.Enabled = False
                txtMax.Enabled = False
            Else
                lblQueue.Enabled = False
                'txtQueue.Enabled = False
                txtMin.Enabled = True
                txtMax.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtQueue_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQueue.KeyUp
        txtQueue.Text = txtQueue.Text.ToUpper
        txtQueue.SelectAll()
    End Sub

    Private Sub cmbMasterCustomertype_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMasterCustomertype.SelectionChangeCommitted
        If cmbMasterCustomertype.SelectedValue <> "0" Then
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
                    Dim para As New ShopWebServiceMain.TbCustomertypeCenParaDB
                    ws.Url = GetWebServiceURL(True)
                    para = ws.GetMasterCustomerTypeByID(cmbMasterCustomertype.SelectedValue)
                    txtCode.Text = para.CUSTOMERTYPE_CODE
                    txtName.Text = para.CUSTOMERTYPE_NAME
                    cbApp.Checked = (para.APP.Value = 1)
                    cbActive.Checked = (para.ACTIVE_STATUS.Value = 1)
                    cbDef.Checked = (para.DEF.Value = 1)
                    If cbApp.Checked = True Then
                        txtQueue.Text = para.TXT_QUEUE
                    End If
                    para = Nothing
                    ws = Nothing
                'Else
                '    'If PingServer(ini.ReadString("Server1")) = True Then
                '    '    Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                '    '    Dim para As New ShopWebServiceDisplay.TbCustomertypeCenParaDB
                '    '    ws.Url = GetWebServiceURL(False)
                '    '    para = ws.GetMasterCustomerTypeByID(cmbMasterCustomertype.SelectedValue)
                '    '    txtCode.Text = para.CUSTOMERTYPE_CODE
                '    '    txtName.Text = para.CUSTOMERTYPE_NAME
                '    '    cbApp.Checked = (para.APP.Value = 1)
                '    '    cbActive.Checked = (para.ACTIVE_STATUS.Value = 1)
                '    '    cbDef.Checked = (para.DEF.Value = 1)
                '    '    If cbApp.Checked = True Then
                '    '        txtQueue.Text = para.TXT_QUEUE
                '    '    End If
                '    '    para = Nothing
                '    '    ws = Nothing
                '    'Else
                '    '    txtCode.Text = ""
                '    '    txtName.Text = ""
                '    '    cbActive.Checked = False
                '    '    cbApp.Checked = False
                '    '    cbDef.Checked = False
                '    '    txtQueue.Text = ""
                '    'End If
                'End If
            Catch ex As Exception
                'If PingServer(ini.ReadString("Server1")) = True Then
                Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                Dim para As New ShopWebServiceDisplay.TbCustomertypeCenParaDB
                ws.Url = GetWebServiceURL(False)
                para = ws.GetMasterCustomerTypeByID(cmbMasterCustomertype.SelectedValue)
                txtCode.Text = para.CUSTOMERTYPE_CODE
                txtName.Text = para.CUSTOMERTYPE_NAME
                cbApp.Checked = (para.APP.Value = 1)
                cbActive.Checked = (para.ACTIVE_STATUS.Value = 1)
                cbDef.Checked = (para.DEF.Value = 1)
                If cbApp.Checked = True Then
                    txtQueue.Text = para.TXT_QUEUE
                End If
                para = Nothing
                ws = Nothing
                'Else
                'txtCode.Text = ""
                'txtName.Text = ""
                'cbActive.Checked = False
                'cbApp.Checked = False
                'cbDef.Checked = False
                'txtQueue.Text = ""
                'End If
            End Try
            ini = Nothing
        Else
            txtCode.Text = ""
            txtName.Text = ""
            cbActive.Checked = False
            cbApp.Checked = False
            cbDef.Checked = False
            txtQueue.Text = ""
        End If
    End Sub
End Class