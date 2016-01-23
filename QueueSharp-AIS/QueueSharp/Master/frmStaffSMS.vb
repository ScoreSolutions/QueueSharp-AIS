

Public Class frmStaffSMS

    '    Dim ds As New DataSet
    '    Dim sql As String = ""

    '    Private Sub frmStaffSMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        showdata()
    '    End Sub

    '    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
    '        If txtSMS.Text = "" Or txtSMS.Text.Length <> 10 Then
    '            MessageBox.Show("Invalid Mobile Number !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            txtSMS.Focus()
    '            Exit Sub
    '        End If

    '        sql = "select mobile_no from TB_staff_sms_config where mobile_no ='" & FixDB(txtSMS.Text) & "' and counter_id = '" & FixDB(lblcounter.Text) & "' and multiply = '" & NUP.Value & "'"
    '        ds = getDataset(sql)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            MessageBox.Show("Duplicate Phone Number Dectected !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Exit Sub
    '        End If
    '        sql = "insert into TB_staff_sms_config(counter_id, multiply, mobile_no) values('" & FixDB(lblcounter.Text) & "','" & NUP.Value & "','" & FixDB(txtSMS.Text) & "')"
    '        executeSQL(sql)
    '        txtSMS.Text = ""
    '        showdata()
    '    End Sub

    '    Sub showdata()
    '        gridSMS.Columns.Clear()
    '        sql = "select row_id,mobile_no,multiply from TB_staff_sms_config where counter_id = '" & FixDB(lblcounter.Text) & "' order by multiply asc"
    '        ds = getDataset(sql)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            With gridSMS
    '                .DataSource = ds.Tables(0)
    '                Dim cs As New DataGridViewCellStyle
    '                cs.Font = New Font("Ms Sans Serif", 8, FontStyle.Bold)
    '                .ColumnHeadersDefaultCellStyle = cs
    '                .Columns(0).Visible = False
    '                .Columns(1).HeaderText = "Mobile No."
    '                .Columns(2).HeaderText = "Multiply"
    '                .Columns(1).ReadOnly = True
    '                .Columns(2).ReadOnly = True
    '                .Columns(1).Width = 90
    '                .Columns(2).Width = 60
    '                .AllowUserToAddRows = False
    '                Dim buttons As New DataGridViewButtonColumn()
    '                With buttons
    '                    .Width = 50
    '                    .Text = "Edit"
    '                    .UseColumnTextForButtonValue = True
    '                    .FlatStyle = FlatStyle.Standard
    '                    .CellTemplate.Style.BackColor = Color.Honeydew
    '                End With
    '                .Columns.Add(buttons)
    '                Dim buttons1 As New DataGridViewButtonColumn()
    '                With buttons1
    '                    .Width = 50
    '                    .Text = "Delete"
    '                    .UseColumnTextForButtonValue = True
    '                    .FlatStyle = FlatStyle.Standard
    '                    .CellTemplate.Style.BackColor = Color.Honeydew
    '                End With
    '                .Columns.Add(buttons1)
    '            End With
    '        Else
    '            gridSMS.DataSource = Nothing
    '        End If
    '    End Sub

    '    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
    '        ButtonAdd.Visible = True
    '        ButtonUpdate.Visible = False
    '        ButtonCancel.Visible = False
    '        lblrow_id.Text = ""
    '        txtSMS.Text = ""
    '        NUP.Value = 2
    '        gridSMS.Enabled = True
    '    End Sub

    '    Private Sub TextSMS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSMS.KeyPress
    '        If (e.KeyChar < "0" Or e.KeyChar > "9") And Asc(e.KeyChar) <> 8 Then
    '            e.Handled = True
    '        End If
    '    End Sub

    '    Private Sub ButtonUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click
    '        If txtSMS.Text = "" Then
    '            MessageBox.Show("Please specify Mobile Number !!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            txtSMS.Focus()
    '            Exit Sub
    '        End If
    '        sql = "update TB_staff_sms_config set multiply ='" & NUP.Value & "', mobile_no ='" & FixDB(txtSMS.Text) & "' where row_id = '" & FixDB(lblrow_id.Text) & "'"
    '        executeSQL(sql)
    '        txtSMS.Text = ""
    '        NUP.Value = 2
    '        ButtonAdd.Visible = True
    '        ButtonUpdate.Visible = False
    '        ButtonCancel.Visible = False
    '        lblrow_id.Text = ""
    '        gridSMS.Enabled = True
    '        showdata()
    '    End Sub

    '    Private Sub gridSMS_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles gridSMS.CellMouseClick
    '        If e.RowIndex >= 0 Then
    '            If e.ColumnIndex = 3 Then
    '                sql = "select * from TB_staff_sms_config where row_id ='" & gridSMS.CurrentRow.Cells(0).Value.ToString & "'"
    '                ds = getDataset(sql)
    '                NUP.Value = ds.Tables(0).Rows(0)("multiply").ToString
    '                txtSMS.Text = ds.Tables(0).Rows(0)("mobile_no").ToString
    '                lblrow_id.Text = ds.Tables(0).Rows(0)("row_id").ToString
    '                ButtonAdd.Visible = False
    '                ButtonUpdate.Visible = True
    '                ButtonCancel.Visible = True
    '                gridSMS.Enabled = False
    '            End If

    '            If e.ColumnIndex = 4 Then
    '                If (MessageBox.Show("Are you sure want to delete?", "Comfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
    '                    executeSQL("delete from TB_staff_sms_config where row_id ='" & gridSMS.CurrentRow.Cells(0).Value.ToString & "'")
    '                    showdata()
    '                End If
    '            End If
    '        End If
    '    End Sub

    '    Private Sub gridSMS_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles gridSMS.CellMouseDoubleClick
    '        If e.RowIndex >= 0 Then
    '            If e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
    '                sql = "select * from TB_staff_sms_config where row_id ='" & gridSMS.CurrentRow.Cells(0).Value.ToString & "'"
    '                ds = getDataset(sql)
    '                NUP.Value = ds.Tables(0).Rows(0)("multiply").ToString
    '                txtSMS.Text = ds.Tables(0).Rows(0)("mobile_no").ToString
    '                lblrow_id.Text = ds.Tables(0).Rows(0)("row_id").ToString
    '                ButtonAdd.Visible = False
    '                ButtonUpdate.Visible = True
    '                ButtonCancel.Visible = True
    '                gridSMS.Enabled = False
    '            End If
    '        End If
    '    End Sub

    '    Sub Enable()
    '        NUP.Enabled = True
    '        txtSMS.Enabled = True
    '    End Sub

    '    Sub Clear()
    '        NUP.Enabled = True
    '        txtSMS.Enabled = True
    '        NUP.Value = 2
    '        txtSMS.Text = ""
    '    End Sub

    '    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    '        Enable()
    '    End Sub

    '    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '        Clear()
    '    End Sub
End Class