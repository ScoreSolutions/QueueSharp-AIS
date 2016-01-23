'Imports Security
Imports Engine.Common

Public Class frmUser

    Dim sql As String = ""
    Dim dt_data As New DataTable

    Sub Add()
        GridItem.BackgroundColor = Color.White
        GridItem.Enabled = True
        If GridItem.Rows.Count > 0 Then
            For i As Int32 = 0 To GridItem.Rows.Count - 1
                GridItem.Rows(i).Cells("cb").Value = False
            Next
        End If
        txtCode.Text = ""
        txtFname.Text = ""
        txtID.Text = ""
        txtSearch.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtPoistion.Text = ""
        txtLname.Text = ""
        txtUsername.Enabled = True
        txtPassword.Enabled = True
        txtPoistion.Enabled = True
        txtLname.Enabled = True
        cbActive.Checked = False
        txtCode.Enabled = True
        txtFname.Enabled = True
        txtSearch.Enabled = False
        cbActive.Enabled = True
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        txtCode.Focus()
        cbTitel.Enabled = True
        cbGroup.Enabled = True
        cbSearch.Enabled = False
        cbActive.Checked = True
        btnLDAP.Enabled = True
        cbConLDAP.Checked = False
        cbConLDAP.Enabled = True
    End Sub

    Sub Edit()
        GridItem.BackgroundColor = Color.White
        GridItem.Enabled = True
        txtSearch.Text = ""
        txtUsername.Enabled = True
        txtPassword.Enabled = True
        txtPoistion.Enabled = True
        txtLname.Enabled = True
        txtCode.Enabled = True
        txtFname.Enabled = True
        txtSearch.Enabled = False
        Grid.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnAdd.Enabled = False
        cbActive.Enabled = True
        txtCode.Focus()
        cbTitel.Enabled = True
        cbGroup.Enabled = True
        cbSearch.Enabled = False
        btnLDAP.Enabled = True
        cbConLDAP.Checked = False
        cbConLDAP.Enabled = True
    End Sub

    Sub Clear()
        GridItem.BackgroundColor = Color.LightGray
        If GridItem.Rows.Count > 0 Then
            For i As Int32 = 0 To GridItem.Rows.Count - 1
                GridItem.Rows(i).Cells("cb").Value = False
            Next
        End If
        GridItem.Enabled = False
        txtCode.Text = ""
        txtFname.Text = ""
        txtID.Text = ""
        txtCode.Enabled = False
        txtFname.Enabled = False
        txtUsername.Enabled = False
        txtPassword.Enabled = False
        txtPoistion.Enabled = False
        txtLname.Enabled = False
        txtSearch.Enabled = True
        cbActive.Checked = False
        cbActive.Enabled = False
        Grid.Enabled = True
        btnAdd.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cbTitel.Enabled = False
        cbGroup.Enabled = False
        cbSearch.Enabled = True
        btnLDAP.Enabled = False
        cbConLDAP.Checked = False
        cbConLDAP.Enabled = False
    End Sub

    Private Sub ShowData()
        sql = "select x.id,title_id,user_code,fname,lname,position,group_id,username,[password],x.active_status,title_name + ' ' + fname + ' ' + lname as fullname,group_name,case when x.active_status = 1 then 'Active' else 'Inactive' end as status from TB_USER x left join TB_TITLE y on x.title_id = y.id left join TB_GROUPUSER z on x.group_id = z.id order by user_code"
        dt_data = getDataTable(sql)
        Grid.DataSource = dt_data
        SearchData()
    End Sub

    Private Sub SearchData()
        Try
            Select Case cbSearch.SelectedIndex
                Case 0
                    dt_data.DefaultView.RowFilter = "username like '%" & txtSearch.Text.Trim & "%' or fname like '%" & txtSearch.Text.Trim & "%' or lname like '%" & txtSearch.Text.Trim & "%' or position like '%" & txtSearch.Text.Trim & "%' or group_name like '%" & txtSearch.Text.Trim & "%'"
                Case 1
                    dt_data.DefaultView.RowFilter = "(username like '%" & txtSearch.Text.Trim & "%' or fname like '%" & txtSearch.Text.Trim & "%' or lname like '%" & txtSearch.Text.Trim & "%' or position like '%" & txtSearch.Text.Trim & "%' or group_name like '%" & txtSearch.Text.Trim & "%') and active_status = 1"
                Case 2
                    dt_data.DefaultView.RowFilter = "(username like '%" & txtSearch.Text.Trim & "%' or fname like '%" & txtSearch.Text.Trim & "%' or lname like '%" & txtSearch.Text.Trim & "%' or position like '%" & txtSearch.Text.Trim & "%' or group_name like '%" & txtSearch.Text.Trim & "%') and active_status = 0"
            End Select
        Catch ex As Exception : End Try
    End Sub

    Private Function Validation() As Boolean
        If txtCode.Text.Trim = "" Then
            MessageBox.Show("Please enter the User Code.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If txtFname.Text.Trim = "" Then
            MessageBox.Show("Please enter the Name.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFname.Focus()
            Return False
        End If

        If txtLname.Text.Trim = "" Then
            MessageBox.Show("Please enter the Surname.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLname.Focus()
            Return False
        End If

        If txtPoistion.Text.Trim = "" Then
            MessageBox.Show("Please enter the Position.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return False
        End If

        If txtUsername.Text.Trim = "" Then
            MessageBox.Show("Please enter Username.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return False
        End If

        If txtPassword.Text.Trim = "" Then
            MessageBox.Show("Please enter Password.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return False
        End If

        If cbConLDAP.Checked = True Then
            Try
                Dim FindData As New ShopWebServiceMain.WebServiceAPI
                Dim LDAP As New ShopWebServiceMain.LDAPResponsePara
                FindData.Url = GetWebServiceURL(True)
                LDAP = FindData.LDAPAuth(txtUsername.Text.Trim, txtPassword.Text.Trim)
                If LDAP.RESULT = True Then
                    If CheckDuplicate("TB_user", "username", txtUsername.Text.Trim, txtID.Text) = True Then
                        MessageBox.Show("The Username already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtUsername.Focus()
                        Return False
                    End If
                Else
                    Try
                        Dim FindData_Backup As New ShopWebServiceDisplay.WebServiceAPI
                        Dim LDAP_Backup As New ShopWebServiceDisplay.LDAPResponsePara
                        FindData.Url = GetWebServiceURL(False)
                        LDAP_Backup = FindData_Backup.LDAPAuth(txtUsername.Text.Trim, txtPassword.Text.Trim)
                        If LDAP_Backup.RESULT = True Then
                            If CheckDuplicate("TB_user", "username", txtUsername.Text.Trim, txtID.Text) = True Then
                                MessageBox.Show("The Username already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                txtUsername.Focus()
                                Return False
                            End If
                        Else
                            If InStr(LDAP.RESPONSE_MSG.ToUpper, "USER") > 0 Then
                                MessageBox.Show("User not found", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ElseIf InStr(LDAP.RESPONSE_MSG.ToUpper, "PASSWORD") > 0 Then
                                MessageBox.Show("Invalid password", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Else
                                MessageBox.Show("Your username or password is incorrect.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                            Return False
                        End If
                    Catch ey As Exception
                        MessageBox.Show("Cannot access LDAP.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End Try
                End If
            Catch ex As Exception
                MessageBox.Show("Cannot access LDAP.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End Try
        End If

        If CheckDuplicate("TB_user", "user_code", txtCode.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The User Code already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        If CheckDuplicate("TB_user", "username", txtUsername.Text.Trim, txtID.Text) = True Then
            MessageBox.Show("The Username already exists! Please re-enter the new one.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return False
        End If

        Dim chk As Boolean = False
        For i As Int32 = 0 To GridItem.Rows.Count - 1
            If GridItem.Rows(i).Cells("cb").Value = True Then
                chk = True
                Exit For
            End If
        Next
        If chk = False Then
            MessageBox.Show("Please select Skill.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        Grid.AutoGenerateColumns = False
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "select id,title_name from TB_title order by title_code"
        dt = getDataTable(sql)
        With cbTitel
            .DataSource = dt
            .DisplayMember = "title_name"
            .ValueMember = "id"
        End With
        dt = New DataTable
        sql = "select id,group_name from TB_groupuser where active_status = 1 order by group_code"
        dt = getDataTable(sql)
        With cbGroup
            .DataSource = dt
            .DisplayMember = "group_name"
            .ValueMember = "id"
        End With

        dt = New DataTable
        sql = "select id,skill from TB_SKILL where active_status = 1 order by skill"
        dt = getDataTable(sql)
        GridItem.DataSource = dt

        cbSearch.SelectedIndex = 1

        ShowData()
        SearchData()

        If QueueCheckLDAP() = False Then
            btnLDAP.Visible = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validation() Then
            Dim Active As Int32 = 0
            If cbActive.Checked Then
                Active = 1
            End If

            Dim PW As String = Engine.Common.FunctionEng.EnCripPwd(txtPassword.Text.Trim)

            If txtID.Text = "" Then
                'Insert
                Dim id_user As String = FindID("TB_user")
                sql = "insert into TB_user(id,title_id,user_code,fname,lname,position,group_id,username,[password],active_status,create_by,create_date,counter_id) "
                sql += " values(" & id_user & "," & cbTitel.SelectedValue & ",'" & FixDB(txtCode.Text) & "','" & FixDB(txtFname.Text) & "','" & FixDB(txtLname.Text) & "','" & FixDB(txtPoistion.Text) & "'," & cbGroup.SelectedValue & ",'" & FixDB(txtUsername.Text) & "','" & FixDB(PW) & "'," & Active & "," & myUser.user_id & ",getdate(),0)"
                executeSQL(sql)

                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    If GridItem.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_user_skill")
                        sql = "insert into TB_user_skill(id,user_id,skill_id,create_by,create_date) values(" & id & "," & id_user & "," & GridItem.Rows(i).Cells("skill_id").Value & "," & myUser.user_id & ",getdate())"
                        executeSQL(sql)
                    End If
                Next

            Else
                'Update
                sql = "update TB_user set title_id = " & cbTitel.SelectedValue & ",user_code = '" & FixDB(txtCode.Text) & "',fname = '" & FixDB(txtFname.Text) & "',lname = '" & FixDB(txtLname.Text) & "',position = '" & FixDB(txtPoistion.Text) & "',group_id = " & cbGroup.SelectedValue & ",username = '" & FixDB(txtUsername.Text) & "',[password] = '" & FixDB(PW) & "',active_status = " & Active & ",update_by = " & myUser.user_id & ",update_date = getdate() where id = " & txtID.Text
                executeSQL(sql)

                sql = "delete from TB_user_skill where user_id = " & txtID.Text
                executeSQL(sql)

                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    If GridItem.Rows(i).Cells("cb").Value = True Then
                        Dim id As String = FindID("TB_user_skill")
                        sql = "insert into TB_user_skill(id,user_id,skill_id,create_by,create_date) values(" & id & "," & txtID.Text & "," & GridItem.Rows(i).Cells("skill_id").Value & "," & myUser.user_id & ",getdate())"
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

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbTitel.Focus()
        End If
    End Sub

    Private Sub cbTitel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbTitel.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtFname.Focus()
        End If
    End Sub

    Private Sub txtFname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFname.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLname.Focus()
        End If
    End Sub

    Private Sub txtLname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLname.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbGroup.Focus()
        End If
    End Sub

    Private Sub cbGroup_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbGroup.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtUsername.Focus()
        End If
    End Sub

    Private Sub txtUsername_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsername.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPoistion.Focus()
        End If
    End Sub

    Private Sub txtCPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPoistion.KeyPress
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
            txtCode.Text = Grid.SelectedRows(0).Cells("user_code").Value.ToString.Trim
            txtFname.Text = Grid.SelectedRows(0).Cells("fname").Value.ToString.Trim
            txtLname.Text = Grid.SelectedRows(0).Cells("lname").Value.ToString.Trim
            txtPoistion.Text = Grid.SelectedRows(0).Cells("position").Value.ToString.Trim
            txtUsername.Text = Grid.SelectedRows(0).Cells("username").Value.ToString.Trim
            Dim PW As String = Engine.Common.FunctionEng.DeCripPwd(Grid.SelectedRows(0).Cells("password").Value.ToString.Trim)
            txtPassword.Text = PW
            cbTitel.SelectedValue = Grid.SelectedRows(0).Cells("title_id").Value.ToString
            cbGroup.SelectedValue = Grid.SelectedRows(0).Cells("group_id").Value.ToString
            cbActive.Checked = Grid.SelectedRows(0).Cells("active_status").Value
            txtID.Text = Grid.SelectedRows(0).Cells("id").Value.ToString.Trim

            If GridItem.Rows.Count > 0 Then
                For i As Int32 = 0 To GridItem.Rows.Count - 1
                    GridItem.Rows(i).Cells("cb").Value = False
                Next
            End If

            If txtID.Text.Trim <> "" Then
                Dim dt As New DataTable
                sql = "select * from TB_user_skill where user_id = " & txtID.Text
                dt = getDataTable(sql)
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        For j As Int32 = 0 To GridItem.Rows.Count - 1
                            If Trim(dt.Rows(i).Item("skill_id").ToString) = Trim(GridItem.Rows(j).Cells("skill_id").Value.ToString) Then
                                GridItem.Rows(j).Cells("cb").Value = True
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception : End Try
    End Sub

    Private Sub btnLDAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLDAP.Click
        If txtUsername.Text.Trim <> "" And txtPassword.Text.Trim <> "" Then
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"
            Try
                'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                  Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                  chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                  sslerror As System.Net.Security.SslPolicyErrors) True

                'If ShopConnectDBENG.PingServer(ini.ReadString("Server")) = True Then
                Dim FindData As New ShopWebServiceMain.WebServiceAPI
                Dim LDAP As New ShopWebServiceMain.LDAPResponsePara
                FindData.Url = GetWebServiceURL(True)
                LDAP = FindData.LDAPAuth(txtUsername.Text.Trim, txtPassword.Text.Trim)
                If LDAP.RESULT = True Then
                    MessageBox.Show("Congratulation! Your username and password have existed in the LDAP.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    If InStr(LDAP.RESPONSE_MSG.ToUpper, "USER") > 0 Then
                        MessageBox.Show("User not found", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ElseIf InStr(LDAP.RESPONSE_MSG.ToUpper, "PASSWORD") > 0 Then
                        MessageBox.Show("Invalid password", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        MessageBox.Show("Your username or password is incorrect.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
                'Else
                'CallLDAPFromDisplayServer(ini)
                'End If
            Catch ex As Exception
                'MessageBox.Show("Cannot access LDAP", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CallLDAPFromDisplayServer(ini)
                Exit Sub
            End Try

        End If
    End Sub

    Private Function CallLDAPFromDisplayServer(ByVal ini As IniReader) As ShopWebServiceDisplay.LDAPResponsePara
        'Call LDAP at Display Server
        Dim LDAP_Backup As New ShopWebServiceDisplay.LDAPResponsePara
        Try
            Dim FindData_Backup As New ShopWebServiceDisplay.WebServiceAPI
            'If ShopConnectDBENG.PingServer(ini.ReadString("Server1")) = True Then
            FindData_Backup.Url = GetWebServiceURL(False)
            LDAP_Backup = FindData_Backup.LDAPAuth(txtUsername.Text.Trim, txtPassword.Text.Trim)
            If LDAP_Backup.RESULT = True Then
                MessageBox.Show("Congratulation! Your username and password have existed in the LDAP.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                If InStr(LDAP_Backup.RESPONSE_MSG.ToUpper, "USER") > 0 Then
                    MessageBox.Show("User not found", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ElseIf InStr(LDAP_Backup.RESPONSE_MSG.ToUpper, "PASSWORD") > 0 Then
                    MessageBox.Show("Invalid password", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Your username or password is incorrect.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
            'Else
            'MessageBox.Show("Cannot access LDAP", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If
        Catch ey As Exception
            MessageBox.Show("Cannot access LDAP", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Return LDAP_Backup
    End Function

End Class