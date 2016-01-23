Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim popupScript = "<script language='javascript'> " & _
        "window.open('../Document_Import/LW10-00004.pdf?#page=2&search=" & TextBox1.Text & "','','left=250px, top=245px, width=700px, height=450px, scrollbars=yes, status=yes, resizable=yes'); " & _
        " </script>"
        Me.RegisterStartupScript("Google", popupScript)

    End Sub
End Class