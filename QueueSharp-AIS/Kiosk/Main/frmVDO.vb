Public Class frmVDO

    Private Sub frmVDO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        VDO.settings.setMode("loop", True)
        VDO.URL = Application.StartupPath & "\AIS.wmv"
        Try
            VDO.uiMode = "none"
            VDO.stretchToFit = True
            VDO.Ctlcontrols.play()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmVDO_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick, pb.MouseClick, pb1.MouseClick
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub


   
End Class