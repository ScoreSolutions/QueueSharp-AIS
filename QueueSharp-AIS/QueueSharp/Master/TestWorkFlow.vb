Public Class TestWorkFlow


    Private Sub monthView1_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.Calendar.DateRangeChangedEventArgs) Handles monthView1.SelectionChanged
        'calendar1.SetViewRange(e.Start, e.[End])
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        frmMain.Show()
        frmMain.WindowState = FormWindowState.Maximized
    End Sub

   
End Class