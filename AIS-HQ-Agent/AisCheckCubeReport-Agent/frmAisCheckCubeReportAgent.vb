Imports System.IO
Imports Engine
Imports System.Data

Public Class frmAisCheckCubeReportAgent
    'Dim patch As String = "D:\HeartBeat\HeartBeat.txt"

    Private Sub tmTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTime.Tick
        lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        Application.DoEvents()
    End Sub

    Private Sub frmAisCheckCubeReportAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                Dim eng As New Engine.CutOffDataENG
                eng.SetTextLog(txtLog)
                Dim CurrDate As DateTime = DateAdd(DateInterval.Day, -1, DateTime.Now)
                eng.CheckCubeReportsProcessAllShop(CurrDate, dt)
                eng = Nothing
            End If
            dt = Nothing

            End

            'If txtLog.Text.Trim = "" Then
            '    dt = Nothing
            '    End
            'Else
            '    Dim dr As DataRow = dt.NewRow
            '    dr("id") = "0"
            '    dr("shop_name_en") = "All Shop"
            '    dt.Rows.InsertAt(dr, 0)

            '    cmbShop.ValueMember = "id"
            '    cmbShop.DisplayMember = "shop_name_en"
            '    cmbShop.DataSource = dt
            'End If
        End If
        dt = Nothing
    End Sub

    Private Sub frmAisCheckCubeReportAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

    Private Sub btnCheckReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckReport.Click
        txtLog.Text = ""
        Dim eng As New Engine.CutOffDataENG
        eng.SetTextLog(txtLog)
        If cmbShop.SelectedValue <> "0" Then
            eng.CheckCubeReportsProcessByShopID(dtpST.Value, dtpET.Value, cmbShop.SelectedValue)
        Else
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.GetActiveShop
            If dt.Rows.Count > 0 Then
                Dim CurrDate As Date = dtpST.Value
                Do
                    eng.CheckCubeReportsProcessAllShop(CurrDate, dt)
                    CurrDate = DateAdd(DateInterval.Day, 1, CurrDate)
                Loop While CurrDate <= dtpET.Value
            End If
            dt = Nothing
        End If
        eng = Nothing
        If txtLog.Text.Trim = "" Then
            MessageBox.Show("Check Reports Complete!!!")
        Else
            MessageBox.Show(txtLog.Text)
        End If
    End Sub
    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub
End Class