Public Class frmDailyHistory

    Private Sub frmDailyHistory_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Sub ShowUser()
        Dim dt As New DataTable
        Dim sql As String = ""

        sql += " select 0 as id,'----- Show All -----' as FullName,0 as ord"
        sql += " union all"
        sql += " select id,FullName,1 as ord"
        sql += " from ("
        sql += " select TB_USER.id,isnull(title_name,'') + ' ' + isnull(fname,'') + ' ' + isnull(lname,'') as FullName from TB_USER left join TB_TITLE on TB_USER.title_id = TB_TITLE.id where TB_USER.id in (select user_id from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date)=0 group by user_id)) as TB1 "
        sql += " order by ord,FullName"

        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            cbUser.DataSource = dt
            cbUser.DisplayMember = "FullName"
            cbUser.ValueMember = "id"
            dt = New DataTable
        End If
    End Sub

    Sub showcustomerend(ByVal UserID As Integer)
        Dim dt As New DataTable
        Dim sql As String = ""
        'sql = "exec SP_DailyHistory " & UserID
        sql = "exec SP_ShowCustomerEnd " & UserID
        dt = getDataTable(sql)
        Grid.DataSource = dt

        dt = New DataTable
        If UserID = 0 Then
            sql = "select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and status in (3,5,8) group by queue_no"
        Else
            sql = "select queue_no from TB_COUNTER_QUEUE where DATEDIFF(D,GETDATE(),service_date) = 0 and status in (3,5,8) and TB_COUNTER_QUEUE.user_id = " & UserID & " group by queue_no"
        End If

        dt = getDataTable(sql)
        lblServe.Text = dt.Rows.Count.ToString
    End Sub

    Private Sub cbCounter_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbUser.SelectionChangeCommitted
        showcustomerend(cbUser.SelectedValue)
    End Sub

    Private Sub frmDailyHistory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Grid.AutoGenerateColumns = False
        ShowUser()
        showcustomerend(cbUser.SelectedValue)
    End Sub
End Class