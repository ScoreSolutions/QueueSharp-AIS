Public Class frmHQUpdateCustomerCorrporate

    Private Sub frmHQUpdateCustomerCorrporate_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

    Private Sub frmHQUpdateCustomerCorrporate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        dt.Dispose()
    End Sub

    Private Sub btnUpdateData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateData.Click
        Dim eng As New Engine.UpdateCustomerCoorperateENG
        If cmbShop.SelectedValue <> "0" Then
            eng.ProcUpdateCustomerCoorperateTypeByShop(cmbShop.SelectedValue, lblTime, ProgressBar1, lblStatus)
        Else
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.GetActiveShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    eng.ProcUpdateCustomerCoorperateTypeByShop(dr("id"), lblTime, ProgressBar1, lblStatus)
                Next
            End If
            dt = Nothing
        End If
        eng = Nothing

        'MessageBox.Show("Complete")
    End Sub
    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub
End Class
