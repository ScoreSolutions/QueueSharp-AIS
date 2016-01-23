
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("Login.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub
End Class
