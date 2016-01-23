Public Class frmCustomerInfo
    Dim dt As New DataTable

    Private Sub AwaitingCustomer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Sub ShowData(ByVal arg As String)
        Dim sql As String = ""
        sql = "exec SP_CustomerInfo"
        dt = getDataTable(sql)
        Grid.AutoGenerateColumns = False
        Grid.DataSource = dt
        dt.DefaultView.RowFilter = "queue_no =''"
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        If txtSearch.Text.Trim = "" Then
            dt.DefaultView.RowFilter = "queue_no =''"
        ElseIf dt.Rows.Count > 0 Then
            dt.DefaultView.RowFilter = "queue_no like '%" & txtSearch.Text & "%' or customer_id like '%" & txtSearch.Text & "%'"
        End If

    End Sub

    Private Sub frmCustomerInfo_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ShowData("")
    End Sub

    'Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
    '    ShowData(FixDB(txtSearch.Text))
    'End Sub
End Class