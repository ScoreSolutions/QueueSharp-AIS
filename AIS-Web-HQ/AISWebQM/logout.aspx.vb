
Partial Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim utl As New utils
        'Try
        '    utl.DeleteVDOofUser(CType(Session("MyUser"), utils.User).username, Server.MapPath(ConfigurationManager.AppSettings("tempvdo_path")))
        'Catch ex As Exception

        'End Try

        Session("MyUser") = Nothing
        Response.Redirect("login.aspx")
    End Sub
End Class
