Public Class frmAisEmpProfileAgent

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

    Private Sub frmAisEmpProfileAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            Dim eng As New Engine.EmployeeProfileENG
            eng.SetTextLog(txtLog)
            eng.UpdateEmployeeProfileAllShop()
            eng = Nothing
            If txtLog.Text.Trim = "" Then
                dt = Nothing
                End
            End If
        End If
        dt = Nothing


    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub btnUpdateData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateData.Click
        txtLog.Text = ""
        Dim eng As New Engine.EmployeeProfileENG
        eng.SetTextLog(txtLog)
        If cmbShop.SelectedValue <> "0" Then
            eng.UpdateEmployeeProfileByShop(cmbShop.SelectedValue)
        Else
            eng.UpdateEmployeeProfileAllShop()
        End If
        eng = Nothing
        If txtLog.Text.Trim = "" Then
            MessageBox.Show("Update Employee Profile Complete!!!")
        Else
            MessageBox.Show(txtLog.Text)
        End If

    End Sub

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub

    Private Sub frmAisEmpProfileAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS Employee Profile Agent V" & getMyVersion()
    End Sub
End Class
