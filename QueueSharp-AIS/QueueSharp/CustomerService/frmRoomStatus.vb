Public Class frmRoomStatus

    Private Sub AwaitingCustomer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ControlBox = False
    End Sub

    Sub ShowData()
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "exec SP_RoomStatus"
        dt = getDataTable(sql)
        Grid.DataSource = dt
        FormatGridWait()
    End Sub

    Private Sub FormatGridWait()
        For i As Integer = 0 To Grid.Rows.Count - 1
            With Grid.Rows(i).DefaultCellStyle
                If Grid.Rows(i).Cells("counter_status").Value.ToString = "Open" Then
                    .ForeColor = Drawing.Color.White
                    .SelectionForeColor = Drawing.Color.White
                    .BackColor = Drawing.Color.ForestGreen
                    .SelectionBackColor = Drawing.Color.ForestGreen
                    Grid.Item("ClearStatus", i).Style.BackColor = Drawing.Color.MistyRose
                    Grid.Item("ClearStatus", i).Style.SelectionBackColor = Drawing.Color.MistyRose
                    Grid.Item("ClearStatus", i).Style.ForeColor = Drawing.Color.Black
                    Grid.Item("ClearStatus", i).Style.SelectionForeColor = Drawing.Color.Black
                    Grid.Item("ClearStatus", i).Value = "Clear Status"
                ElseIf Grid.Rows(i).Cells("counter_status").Value.ToString = "Hold" Then
                    .ForeColor = Drawing.Color.White
                    .SelectionForeColor = Drawing.Color.White
                    .BackColor = Drawing.Color.Orange
                    .SelectionBackColor = Drawing.Color.Orange
                    Grid.Item("ClearStatus", i).Style.BackColor = Drawing.Color.MistyRose
                    Grid.Item("ClearStatus", i).Style.SelectionBackColor = Drawing.Color.MistyRose
                    Grid.Item("ClearStatus", i).Style.ForeColor = Drawing.Color.Black
                    Grid.Item("ClearStatus", i).Style.SelectionForeColor = Drawing.Color.Black
                    Grid.Item("ClearStatus", i).Value = "Clear Status"
                Else
                    .ForeColor = Drawing.Color.White
                    .SelectionForeColor = Drawing.Color.White
                    .BackColor = Drawing.Color.DarkRed
                    .SelectionBackColor = Drawing.Color.DarkRed
                    Grid.Item("ClearStatus", i).Style.BackColor = Drawing.Color.LightGray
                    Grid.Item("ClearStatus", i).Style.SelectionBackColor = Drawing.Color.LightGray
                    Grid.Item("ClearStatus", i).Style.ForeColor = Drawing.Color.LightGray
                    Grid.Item("ClearStatus", i).Style.SelectionForeColor = Drawing.Color.LightGray
                    Grid.Item("ClearStatus", i).Value = " "
                End If
            End With
        Next
    End Sub

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick
        ShowData()
    End Sub

    Private Sub frmAwaitingCustomer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ShowData()
        timerRefresh.Interval = AutoRefresh()
    End Sub

    Private Sub Grid_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseClick
        If Grid.Rows.Count > 0 Then
            If Grid.SelectedRows(0).Cells(e.ColumnIndex).Value.ToString.ToUpper = "CLEAR STATUS" Then
                timerRefresh.Enabled = False
                If Grid.SelectedRows(0).Cells("id").Value.ToString <> myUser.counter_id Then
                    If MessageBox.Show("Do you want to comfirm" & vbCrLf & "Clear " & Grid.SelectedRows(0).Cells(2).Value.ToString & " is status?", "Comfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        Dim sql As String = ""
                        sql = "update TB_USER set counter_id = 0 ,item_id = 0,ip_address = null,check_update = getdate() where counter_id = " & Grid.SelectedRows(0).Cells("id").Value.ToString
                        executeSQL(sql)
                        MessageBox.Show("Your selected data has successfully been saved.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ShowData()
                    End If
                Else
                    MessageBox.Show("You cannot clear your counter status." & vbCrLf & "Because you are login now.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                timerRefresh.Enabled = True
            End If
        End If
    End Sub


    Private Sub Grid_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles Grid.RowStateChanged
        FormatGridWait()
    End Sub
End Class