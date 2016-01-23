Public Class frmAppointmentCustomer

    Private Sub frmDailyHistory_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Sub ShowData()
        Dim dt As New DataTable
        Dim sql As String = ""
        'sql = "exec SP_DailyHistory " & UserID
        sql = "SP_AppointmentCustomer"

        dt = getDataTable(sql)
        Grid.DataSource = dt

    End Sub

    Private Sub frmDailyHistory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Grid.AutoGenerateColumns = False
        ShowData
    End Sub
End Class