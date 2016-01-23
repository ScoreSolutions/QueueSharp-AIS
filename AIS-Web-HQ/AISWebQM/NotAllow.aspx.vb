Imports System.Data
Partial Class NotAllow
    Inherits System.Web.UI.Page
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Master.showError("You are NOT ALLOWED to use the function.")
    End Sub
End Class
