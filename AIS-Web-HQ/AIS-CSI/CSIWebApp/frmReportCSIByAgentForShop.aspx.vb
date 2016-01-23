
Partial Class CSIWebApp_frmReportCSIByAgentForShop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim UserName As String = Config.GetLogOnUser.USERNAME
            If UserName <> "" Then
                If Request("MenuID") IsNot Nothing Then
                    If UserName <> "admin" Then
                        If Engine.Common.MenuENG.CheckMenuByUserName(Request("MenuID"), Request.AppRelativeCurrentExecutionFilePath, UserName) = False Then
                            Response.Redirect("../CSIWebApp/frmWelcomePage.aspx?NoAuthen=Y")
                        Else
                            Response.Redirect("../CSIWebApp/frmReportCSIByAgent.aspx?ForShop=Y&MenuID=" & Request("MenuID"))
                        End If
                    Else
                        Response.Redirect("../CSIWebApp/frmReportCSIByAgent.aspx?ForShop=Y&MenuID=" & Request("MenuID"))
                    End If
                Else
                    Response.Redirect("../CSIWebApp/frmWelcomePage.aspx?NoAuthen=Y")
                End If
            Else
                Session.RemoveAll()
                Session.Abandon()
                Response.Redirect("../frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
            End If
        End If
    End Sub
End Class
