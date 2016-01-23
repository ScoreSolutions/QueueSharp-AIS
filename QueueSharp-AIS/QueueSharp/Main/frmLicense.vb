Imports ScoreLicense
Imports QueueSharp.Org.Mentalis.Files

Public Class frmLicense

    Dim ButtonClick As Boolean = False

    Private Sub frmLicense_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If ButtonClick = False Then
            e.Cancel = True
            Exit Sub
        End If

    End Sub


    Private Sub ButOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButOK.Click

        If txtLicense.Text = "" Then
            MessageBox.Show("Please Enter License.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtLicense.Focus()
            Exit Sub
        End If
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim score As New ScoreLicense.ScoreLicense(ApplicationName)
        If Not score.IsValidLicense(txtLicense.Text) Then
            MessageBox.Show("Invalid License Key.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            ini.Write("license", txtLicense.Text)
        End If
        ButtonClick = True
        Me.Close()
    End Sub

    Private Sub ButCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButCancel.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        Dim score As New ScoreLicense.ScoreLicense(ApplicationName)
        If Not score.IsValidLicense(ini.ReadString("License")) Then
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
        ButtonClick = True
        Me.Close()
    End Sub
End Class