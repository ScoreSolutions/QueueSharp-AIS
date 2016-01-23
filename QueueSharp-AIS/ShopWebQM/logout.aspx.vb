
Partial Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim usr As utils.User = DirectCast(Session("MyUser"), utils.User)
            Dim utl As New utils
            utl.DeleteVDOofUser(usr.TempVDOPath)
            Session.RemoveAll()
        Catch ex As Exception

        End Try
        Response.Redirect("login.aspx")
    End Sub
End Class
