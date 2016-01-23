Public Class frmReason
    Public Reason As Int32
    'Reason = 1   Hold Room
    'Reason = 2   Log Out

    Dim dt_Reason As New DataTable


    Private Sub frmReason_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes Then Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cbReason_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cbReason.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnOK.Focus()
        End If
    End Sub


    Private Sub frmReason_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""

        Dim TB As String = ""

        Select Case Reason
            Case 1
                TB = "TB_HOLD_REASON"
                Me.Text = "HOLD REASON"
            Case 2
                TB = "TB_LOGOUT_REASON"
                Me.Text = "LOGOUT REASON"
        End Select

        sql = "select id,name,productive from " & TB & " where active_status = 1 order by name"
        dt_Reason = getDataTable(sql)
        dt_Reason.PrimaryKey = New DataColumn() {dt_Reason.Columns("id")}

        With cbReason
            .DataSource = dt_Reason
            .DisplayMember = "name"
            .ValueMember = "id"
            If dt_Reason.Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
        

    End Sub

    Private Sub btnComfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If dt_Reason.Rows.Count > 0 Then
            Dim productive As Int32
            Dim s As String = cbReason.SelectedValue
            Dim foundRow As DataRow = dt_Reason.Rows.Find(s)
            If foundRow IsNot Nothing Then
                productive = CInt(foundRow(2).ToString)
            End If

            Dim TB As String = ""
            Select Case Reason
                Case 1
                    LogHoldRoom(1, cbReason.SelectedValue, productive)
                Case 2
                    LogLogin(2, cbReason.SelectedValue, productive)
            End Select

            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Hide()
            Me.Close()
            Threading.Thread.Sleep(500)
            Application.DoEvents()
        End If
    End Sub
End Class