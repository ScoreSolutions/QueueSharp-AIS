Imports QueueSharp.Org.Mentalis.Files

Public Class FormFloatAllservice
    Dim sql As String = ""
    Dim ds As New DataSet
    Public Mobile As String
   
    Private Sub FormFloatAllservice_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "exec SP_FindCustomerInfo '" & Mobile.Replace("-", "") & "'"
        dt = getDataTable(sql)
        If dt.Rows.Count > 0 Then
            lblName.Text = dt.Rows(0).Item("Name").ToString
            lblEmail.Text = dt.Rows(0).Item("Email").ToString
            lblMobile_status.Text = dt.Rows(0).Item("Mobile_status").ToString
            lblBirth_date.Text = dt.Rows(0).Item("Birth_date").ToString
            lblCategory.Text = dt.Rows(0).Item("Category").ToString
            lblAcc.Text = dt.Rows(0).Item("Acc").ToString
            lblCon_class.Text = dt.Rows(0).Item("Con_class").ToString
            lblService_year.Text = dt.Rows(0).Item("Service_year").ToString
            lblChurn.Text = dt.Rows(0).Item("Churn").ToString
            lblCamp_code.Text = dt.Rows(0).Item("Camp_code").ToString
            lblCamp_name.Text = dt.Rows(0).Item("Camp_name").ToString
            lblPre_lang.Text = dt.Rows(0).Item("Pre_lang").ToString
            lblNetwork_type.Text = dt.Rows(0).Item("Network_type").ToString
            lblSegment.Text = dt.Rows(0).Item("Segment_Level").ToString
        Else
            lblName.Text = ""
            lblEmail.Text = ""
            lblMobile_status.Text = ""
            lblBirth_date.Text = ""
            lblCategory.Text = ""
            lblAcc.Text = ""
            lblCon_class.Text = ""
            lblService_year.Text = ""
            lblChurn.Text = ""
            lblCamp_code.Text = ""
            lblCamp_name.Text = ""
            lblPre_lang.Text = ""
            lblNetwork_type.Text = ""
            lblSegment.Text = ""
        End If
    End Sub
End Class