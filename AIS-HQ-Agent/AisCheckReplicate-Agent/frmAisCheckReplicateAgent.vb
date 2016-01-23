Public Class frmAisCheckReplicateAgent

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

    Private Sub frmAisCheckReplicateAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            If dt.Rows.Count > 0 Then
                Dim eng As New Engine.CompareDataENG
                For Each dr As DataRow In dt.Rows
                    eng.CompareBeforeCutOffByShopID(dr("id"), DateTime.Now)
                Next
                If eng.MailMessage().Trim <> "" Then
                    txtLog.Text = eng.MailMessage
                    Dim MailSjb As String = "Queue transaction Replicate จำนวนไม่เท่ากันวันที่ " & DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                    eng.SendAlertEmail(MailSjb)
                End If
                eng = Nothing
            End If
            dt = Nothing

            End
        End If

        dt = Nothing
    End Sub

    Private Sub btnCheckData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckData.Click
        Dim eng As New Engine.CompareDataENG
        If cmbShop.SelectedValue <> "0" Then
            eng.CompareBeforeCutOffByShopID(cmbShop.SelectedValue, DateTime.Now)
        Else
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.GetActiveShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    eng.CompareBeforeCutOffByShopID(dr("id"), DateTime.Now)
                Next
            End If
            dt = Nothing
        End If

        txtLog.Text = eng.MailMessage().Replace("<br />", vbCrLf)
        If txtLog.Text.Trim = "" Then
            MessageBox.Show("Check Data Complete!!!")
        Else
            Dim MailSjb As String = "Queue transaction Replicate จำนวนไม่เท่ากันวันที่ " & DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
            eng.SendAlertEmail(MailSjb)
            MessageBox.Show(txtLog.Text)
        End If
        eng = Nothing
    End Sub
    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub
End Class
