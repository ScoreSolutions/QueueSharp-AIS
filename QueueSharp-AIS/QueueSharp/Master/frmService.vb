Imports System.Data.SqlClient
Imports System.IO
Imports QueueSharp.Org.Mentalis.Files
Imports Engine.Common.ShopConnectDBENG

Public Class frmService

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        txtCode.Text = ""
        txtName.Text = ""
        txtName_th.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        txtMin.Text = ""
        txtMax.Text = ""
        txtMin.Enabled = True
        txtMax.Enabled = True
        cbInActive.Checked = True
        'txtCode.Enabled = True
        'txtName.Enabled = True
        'txtName_th.Enabled = True
        cbQueue.Enabled = True
        'nudTime.Enabled = True
        'nudWait.Enabled = True
        txtSearch.Enabled = False
        cbInActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        btnClearIcon.Enabled = True
        cbSearch.Enabled = False
        txtQueue.Text = ""
        txtQueue.Enabled = True
        lblColor.Enabled = True
        nudTime.Value = 10
        nudWait.Value = 10
        lblColor.BackColor = Drawing.Color.White
        txtItemOrder.Enabled = True
        txtUrl.Enabled = True
        txtItemOrder.Text = dt_data.Rows.Count
        btnOrder.Enabled = False
        cmbRefMasterItem.Enabled = True
        cmbRefMasterItem.SelectedValue = "0"
    End Sub

    Sub Edit()
        txtSearch.Text = ""
        'txtCode.Enabled = True
        'txtName.Enabled = True
        'txtName_th.Enabled = True
        cbQueue.Enabled = True
        'nudTime.Enabled = True
        'nudWait.Enabled = True
        txtSearch.Enabled = False
        cbInActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        btnClearIcon.Enabled = True
        cbSearch.Enabled = False
        txtQueue.Enabled = True
        lblColor.Enabled = True
        txtMin.Enabled = True
        txtMax.Enabled = True
        txtItemOrder.Enabled = True
        txtUrl.Enabled = True
        btnOrder.Enabled = False
        cmbRefMasterItem.Enabled = True
    End Sub

    Sub Clear()
        txtCode.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtQueue.Text = ""
        txtName_th.Text = ""
        txtMin.Text = ""
        txtMax.Text = ""
        txtCode.Enabled = False
        txtName.Enabled = False
        cbQueue.Enabled = False
        nudTime.Enabled = False
        nudWait.Enabled = False
        txtSearch.Enabled = True
        cbInActive.Checked = False
        cbInActive.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnClearIcon.Enabled = False
        cbSearch.Enabled = True
        txtQueue.Enabled = False
        lblColor.Enabled = False
        txtName_th.Enabled = False
        txtMin.Enabled = False
        txtMax.Enabled = False
        lblColor.BackColor = Drawing.Color.White
        txtItemOrder.Text = ""
        txtItemOrder.Enabled = False
        txtUrl.Text = ""
        txtUrl.Enabled = False
        btnOrder.Enabled = True
        cmbRefMasterItem.Enabled = False
    End Sub

    Private Sub ShowData()
        sql = "select x.id,item_code,item_name,item_time,item_wait,q_type_id,x.active_status,"
        sql += " y.name as TB_type_name,txt_queue,item_order,item_name_th,app_min_queue,app_max_queue,color,"
        sql += " case when x.active_status = 1 then 'Active' else 'Inactive' end as status,item_wait,item_url, "
        sql += " x.master_itemid, x.brand_name"
        sql += " from TB_item x "
        sql += " left join TB_type y on x.q_type_id = y.id "
        sql += " order by item_order,item_name"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data
        SearchData()
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "item_code like '%" & txtSearch.Text.Trim & "%' or item_name like '%" & txtSearch.Text.Trim & "%' or item_name_th like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "(item_code like '%" & txtSearch.Text.Trim & "%' or item_name like '%" & txtSearch.Text.Trim & "%' or item_name_th like '%" & txtSearch.Text.Trim & "%') and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "(item_code like '%" & txtSearch.Text.Trim & "%' or item_name like '%" & txtSearch.Text.Trim & "%' or item_name_th like '%" & txtSearch.Text.Trim & "%') and active_status = 0"
            End Select
        Catch ex As Exception : End Try

    End Sub

    Private Function Validation() As Boolean
        If cmbRefMasterItem.SelectedValue = "0" Then
            MessageBox.Show("Please select the Master Item", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbRefMasterItem.Focus()
            Return False
        End If

        If txtCode.Text.Trim = "" Then
            MessageBox.Show("Please enter the Item Code.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter the Item Name In Engilsh.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If txtName_th.Text.Trim = "" Then
            MessageBox.Show("Please enter the Item Name In Thai.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName_th.Focus()
            Return False
        End If

        If txtQueue.Text.Trim = "" Then
            MessageBox.Show("Please enter the Text Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemOrder.Focus()
            Return False
        End If

        If txtItemOrder.Text.Trim = "" Then
            MessageBox.Show("Please enter the Item Order.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemOrder.Focus()
            Return False
        End If

        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select * from tb_item"
        dt = getDataTable(sql)
        Dim Ord As Int32 = 0
        If txtID.Text = "" Then
            Ord = dt.Rows.Count + 1
        Else
            Ord = dt.Rows.Count
        End If

        If CInt(txtItemOrder.Text) > Ord Then
            MessageBox.Show("This Max Item Order is " & Ord.ToString, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemOrder.Focus()
            Return False
        End If

        If txtMin.Text.Trim = "" Then
            MessageBox.Show("Please enter the Min Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMin.Focus()
            Return False
        End If

        If txtMax.Text.Trim = "" Then
            MessageBox.Show("Please enter the Max Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMax.Focus()
            Return False
        End If

        If CInt(txtMin.Text) >= CInt(txtMax.Text) Then
            MessageBox.Show("Min Queue > Max Queue.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMax.Focus()
            Exit Function
        End If

        If CheckDuplicate("TB_ITEM", "MASTER_ITEMID", cmbRefMasterItem.SelectedValue, txtID.Text) = True Then
            MessageBox.Show("Master Item already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbRefMasterItem.Focus()
            Return False
        End If

        If CheckDuplicate("TB_item", "item_code", txtCode.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("Item Code already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If CheckDuplicate("TB_item", "item_name", txtName.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("Item Name In Engilsh already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        If CheckDuplicate("TB_item", "item_name_th", txtName_th.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("Item Name In Thai already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName_th.Focus()
            Return False
        End If

        If CheckDuplicate("TB_item", "txt_queue", txtQueue.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("Text Queue already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQueue.Focus()
            Return False
        End If
        If txtQueue.Text.Length > 0 Then
            If Asc(txtQueue.Text) < 65 Or Asc(txtQueue.Text) > 90 Then
                MessageBox.Show("You must enter English Alphabets only!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQueue.Focus()
                Return False
            End If
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

    Private Sub frmService_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        Grid.AutoGenerateColumns = False

        dt = New DataTable
        sql = "select id,name from TB_type"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            With cbQueue
                .DataSource = dt
                .ValueMember = "id"
                .DisplayMember = "name"
                .SelectedIndex = 0
            End With
        End If

        SetRefMasterItem()

        cbSearch.SelectedIndex = 1
        ShowData()
    End Sub

    Private Sub SetRefMasterItem()
        Dim dt As New DataTable
        dt = GetMasterItemList()
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("item_name") = ""
            dt.Rows.InsertAt(dr, 0)

            cmbRefMasterItem.DataSource = dt
            cmbRefMasterItem.ValueMember = "id"
            cmbRefMasterItem.DisplayMember = "item_name"
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim Active As Int32 = 0
            If cbInActive.Checked Then
                Active = 1
            End If

            Dim tmpColor As String = ""
            If lblColor.BackColor = Drawing.Color.White Then
                tmpColor = "NULL"
            Else
                tmpColor = lblColor.BackColor.ToArgb.ToString()
            End If

            Dim Order As Int32 = 0
            Dim dt As New DataTable

            If txtID.Text = "" Then
                'Insert
                Order = txtItemOrder.Text
                Dim id_item As String = FindID("TB_item")
                sql = "insert into TB_item(id,item_code,item_name,item_name_th,q_type_id,item_time,item_wait,color,txt_queue,app_min_queue,app_max_queue,active_status,create_by,create_date,item_order,item_url, master_itemid) values('" & id_item & "','" & FixDB(txtCode.Text) & "','" & FixDB(txtName.Text) & "','" & FixDB(txtName_th.Text) & "',1," & nudTime.Value.ToString & "," & nudWait.Value.ToString & "," & tmpColor & ",'" & FixDB(txtQueue.Text) & "','" & txtMin.Text & "','" & txtMax.Text & "'," & Active & "," & myUser.user_id & ",getdate()," & txtItemOrder.Text & ",'" & FixDB(txtUrl.Text.Trim) & "','" & cmbRefMasterItem.SelectedValue & "')"
                executeSQL(sql)
            Else
                'Update
                sql = "select item_order from TB_item where id = " & txtID.Text
                dt = getDataTable(sql)
                Order = dt.Rows(0).Item("item_order")

                sql = "update TB_item set item_order = " & CInt(txtItemOrder.Text) - 1 & " where item_order = " & txtItemOrder.Text
                executeSQL(sql)

                sql = "update TB_item set item_code = '" & FixDB(txtCode.Text) & "',item_name = '" & FixDB(txtName.Text) & "',item_name_th = '" & FixDB(txtName_th.Text) & "',q_type_id = 1,item_time = " & nudTime.Value.ToString & ",item_wait = " & nudWait.Value.ToString & ",color = " & tmpColor & ",txt_queue = '" & FixDB(txtQueue.Text) & "',app_min_queue = '" & txtMin.Text & "',app_max_queue = '" & txtMax.Text & "',active_status = " & Active & ",update_by = " & myUser.user_id & ",update_date = getdate(),item_order = " & txtItemOrder.Text & ",item_url = '" & FixDB(txtUrl.Text.Trim) & "', master_itemid='" & cmbRefMasterItem.SelectedValue & "' where id = " & txtID.Text
                executeSQL(sql)

            End If

            '********* ReOrder ********
            If CInt(txtItemOrder.Text) > Order Then
                sql = "select *  from TB_ITEM  where item_order <= " & txtItemOrder.Text & "  order by item_order asc"
                dt = New DataTable
                dt = getDataTable(sql)
                For i As Int32 = 0 To dt.Rows.Count - 1
                    sql = "update TB_ITEM set item_order = " & i + 1 & " where id = " & dt.Rows(i).Item("id")
                    executeSQL(sql)
                Next
            Else
                sql = "select *  from TB_ITEM  where item_order < " & txtItemOrder.Text & "  order by item_order asc"
                dt = New DataTable
                dt = getDataTable(sql)
                For i As Int32 = 0 To dt.Rows.Count - 1
                    sql = "update TB_ITEM set item_order = " & i + 1 & " where id = " & dt.Rows(i).Item("id")
                    executeSQL(sql)
                Next

                sql = "select *  from TB_ITEM  where item_order >= " & txtItemOrder.Text & " and item_code <> '" & FixDB(txtCode.Text) & "' order by item_order asc"
                dt = New DataTable
                dt = getDataTable(sql)
                For i As Int32 = 0 To dt.Rows.Count - 1
                    sql = "update TB_ITEM set item_order = " & txtItemOrder.Text + i + 1 & " where id = " & dt.Rows(i).Item("id")
                    executeSQL(sql)
                Next
            End If

            '**************************
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

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtName.Focus()
        End If
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtName_th.Focus()
        End If
    End Sub

    Private Sub txtName_th_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName_th.KeyPress
        If Asc(e.KeyChar) = 13 Then
            nudTime.Focus()
        End If
    End Sub

    Private Sub nudTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nudTime.KeyPress
        If Asc(e.KeyChar) = 13 Then
            nudWait.Focus()
        End If
    End Sub

    Private Sub nudWait_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nudWait.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtItemOrder.Focus()
        End If
    End Sub

    Private Sub txtItemOrder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemOrder.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtQueue.Focus()
        End If
    End Sub

    Private Sub txtQueue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQueue.KeyPress
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
            cbInActive.Focus()
        End If
    End Sub

    Private Sub cbInActive_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbInActive.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub Textmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMin.KeyPress, txtMax.KeyPress, txtItemOrder.KeyPress
        If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
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
            txtCode.Text = Grid.SelectedRows(0).Cells("item_code").Value.ToString.Trim
            txtName.Text = Grid.SelectedRows(0).Cells("item_name").Value.ToString.Trim
            txtName_th.Text = Grid.SelectedRows(0).Cells("item_name_th").Value.ToString.Trim
            cbQueue.SelectedValue = Grid.SelectedRows(0).Cells("q_type_id").Value.ToString.Trim
            nudTime.Value = Grid.SelectedRows(0).Cells("item_time").Value.ToString.Trim
            nudWait.Value = Grid.SelectedRows(0).Cells("item_wait").Value.ToString.Trim
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim
            cbInActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            txtQueue.Text = Grid.SelectedRows(0).Cells("txt_queue").Value.ToString.Trim
            txtMin.Text = Grid.SelectedRows(0).Cells("app_min_queue").Value.ToString.Trim
            txtMax.Text = Grid.SelectedRows(0).Cells("app_max_queue").Value.ToString.Trim
            txtItemOrder.Text = Grid.SelectedRows(0).Cells("item_order").Value.ToString.Trim
            txtUrl.Text = Grid.SelectedRows(0).Cells("item_url").Value.ToString.Trim
            If Convert.IsDBNull(Grid.SelectedRows(0).Cells("master_itemid").Value) = False Then
                If Grid.SelectedRows(0).Cells("master_itemid").Value.ToString.Trim = "" Then
                    cmbRefMasterItem.SelectedValue = "0"
                Else
                    cmbRefMasterItem.SelectedValue = Grid.SelectedRows(0).Cells("master_itemid").Value.ToString.Trim
                End If
            Else
                cmbRefMasterItem.SelectedValue = "0"
            End If
            If Grid.SelectedRows(0).Cells("color").Value.ToString <> "" And IsNumeric(Grid.SelectedRows(0).Cells("color").Value.ToString) Then
                lblColor.BackColor = Drawing.Color.FromArgb(CInt(Grid.SelectedRows(0).Cells("color").Value.ToString))
            Else
                lblColor.BackColor = Drawing.Color.White
            End If

        Catch ex As Exception : End Try
    End Sub

    'Public Shared Function StrToByteArray(ByVal str As String) As Byte()
    '    Dim encoding As New System.Text.UTF8Encoding()
    '    Return encoding.GetBytes(str)
    'End Function 


    'Private Sub pg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pg.Click
    '    ofd.Filter = "PNG Images (*.png)|"
    '    If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        pg.ImageLocation = ofd.FileName
    '    End If
    'End Sub

    'Private Sub btnClearIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearIcon.Click
    '    pg.Image = Nothing
    '    pg.ImageLocation = Nothing
    'End Sub

    'Private Sub Textmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMin.KeyPress, txtMax.KeyPress
    '    If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub txtQueue_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQueue.KeyUp
        txtQueue.Text = txtQueue.Text.ToUpper
        txtQueue.SelectAll()
    End Sub

    Private Sub lblColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblColor.Click
        If btnSave.Enabled = True Then
            ColorPicker.Color = lblColor.BackColor
            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then
                lblColor.BackColor = ColorPicker.Color
            End If
        End If
    End Sub

    Private Sub btnOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Dim sql As String = ""
        Dim dt As New DataTable
        Dim countActive As Int32 = 0
        sql = "select id,item_order  from tb_item where active_status = 1 order by item_order asc"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                sql = "update tb_item set item_order = " & i + 1 & " where id = " & dt.Rows(i).Item("id")
                executeSQL(sql)
            Next
        End If

        countActive = dt.Rows.Count
        dt = New DataTable
        sql = "select id,item_order  from tb_item where active_status = 0 order by item_order asc"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                sql = "update tb_item set item_order = " & countActive + i + 1 & " where id = " & dt.Rows(i).Item("id")
                executeSQL(sql)
            Next
        End If

        ShowData()
    End Sub

 
    Private Sub txtQueue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQueue.TextChanged

    End Sub

    Private Sub cmbRefMasterItem_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRefMasterItem.SelectionChangeCommitted
        If cmbRefMasterItem.SelectedValue <> "0" Then
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
                Dim para As New ShopWebServiceMain.TbItemCenParaDB
                ws.Url = GetWebServiceURL(True)
                para = ws.GetMasterItemByID(cmbRefMasterItem.SelectedValue)
                txtCode.Text = para.ITEM_CODE
                txtName.Text = para.ITEM_NAME
                txtName_th.Text = para.ITEM_NAME_TH
                nudTime.Value = para.ITEM_TIME.Value
                nudWait.Value = para.ITEM_WAIT.Value
                txtMin.Text = para.APP_MIN_QUEUE
                txtMax.Text = para.APP_MAX_QUEUE
                txtQueue.Text = para.TXT_QUEUE
                If para.COLOR.Value <> 0 And IsNumeric(para.COLOR.Value) Then
                    lblColor.BackColor = Drawing.Color.FromArgb(para.COLOR.Value)
                Else
                    lblColor.BackColor = Drawing.Color.White
                End If

                para = Nothing
                ws = Nothing
                'Else
                'If PingServer(ini.ReadString("Server1")) = True Then
                '    Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                '    Dim para As New ShopWebServiceDisplay.TbItemCenParaDB
                '    ws.Url = GetWebServiceURL(False)
                '    para = ws.GetMasterItemByID(cmbRefMasterItem.SelectedValue)
                '    txtCode.Text = para.ITEM_CODE
                '    txtName.Text = para.ITEM_NAME
                '    txtName_th.Text = para.ITEM_NAME_TH
                '    para = Nothing
                '    ws = Nothing
                'Else
                '    txtCode.Text = ""
                '    txtName.Text = ""
                '    txtName_th.Text = ""
                'End If
                'End If
            Catch ex As Exception
                'If PingServer(ini.ReadString("Server1")) = True Then
                Dim ws As New ShopWebServiceDisplay.WebServiceAPI
                Dim para As New ShopWebServiceDisplay.TbItemCenParaDB
                ws.Url = GetWebServiceURL(False)
                para = ws.GetMasterItemByID(cmbRefMasterItem.SelectedValue)
                txtCode.Text = para.ITEM_CODE
                txtName.Text = para.ITEM_NAME
                txtName_th.Text = para.ITEM_NAME_TH
                nudTime.Value = para.ITEM_TIME.Value
                nudWait.Value = para.ITEM_WAIT.Value
                txtMin.Text = para.APP_MIN_QUEUE
                txtMax.Text = para.APP_MAX_QUEUE
                txtQueue.Text = para.TXT_QUEUE
                If para.COLOR.Value <> 0 And IsNumeric(para.COLOR.Value) Then
                    lblColor.BackColor = Drawing.Color.FromArgb(para.COLOR.Value)
                Else
                    lblColor.BackColor = Drawing.Color.White
                End If
                para = Nothing
                ws = Nothing
                'Else
                '    txtCode.Text = ""
                '    txtName.Text = ""
                '    txtName_th.Text = ""
                'End If
            End Try
            ini = Nothing
        Else
            txtCode.Text = ""
            txtName.Text = ""
            txtName_th.Text = ""
        End If
    End Sub
End Class