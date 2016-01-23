Imports System.Data
Partial Class CSIWebApp_tmpTestReadExcelFile
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        Dim eng As New Engine.CSI.FilterTemplateENG
        dt = eng.ReadMobileNoFromExcel(TextBox1.Text)
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub
End Class
