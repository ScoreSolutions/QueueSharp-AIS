
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblWelcome.Text = "Welcome " & CType(Session("MyUser"), utils.User).fulllname
        Title = Version
    End Sub
End Class
