Public Class frmAisCompareMasterDataAgent

    Private Sub frmAisCheckReplicateAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub tmTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTime.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
    End Sub

    Private Sub frmAisCompareMasterDataAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        dt = Engine.Common.FunctionEng.GetActiveShop
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "shop_name_en"
            dt = dt.DefaultView.ToTable

            Dim dr As DataRow = dt.NewRow
            dr("id") = "0"
            dr("shop_name_en") = "All Shop"
            dt.Rows.InsertAt(dr, 0)

            cmbShop.ValueMember = "id"
            cmbShop.DisplayMember = "shop_name_en"
            cmbShop.DataSource = dt
        End If

        If My.Application.CommandLineArgs.Count > 0 Then
            dt.DefaultView.RowFilter = "id not in (0)"
            dt = dt.DefaultView.ToTable

            Dim eng As New Engine.CheckMasterENG
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtLog.Text += eng.CheckConflictMasterData(dr("id"))
                Next
            End If
            If txtLog.Text.Trim = "" Then
                dt = Nothing
            Else
                eng.SendMailErrorCompareMasterData(txtLog.Text)
                txtLog.Text = txtLog.Text.Replace("<br />", vbCrLf)
            End If
            eng = Nothing
            End
        End If
        dt = Nothing
    End Sub

    Private Sub btnCheckData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckData.Click
        txtLog.Text = ""
        Dim eng As New Engine.CheckMasterENG
        If cmbShop.SelectedValue <> "0" Then
            txtLog.Text = eng.CheckConflictMasterData(cmbShop.SelectedValue)
        Else
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.GetActiveShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtLog.Text += eng.CheckConflictMasterData(dr("id"))
                Next
            End If
            dt = Nothing
        End If
        If txtLog.Text.Trim = "" Then
            MessageBox.Show("Compare Master Data Complete!!!")
        Else
            eng.SendMailErrorCompareMasterData(txtLog.Text)
            txtLog.Text = txtLog.Text.Replace("<br />", vbCrLf)
            'MessageBox.Show(txtLog.Text)
        End If
        eng = Nothing
    End Sub

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub
End Class
