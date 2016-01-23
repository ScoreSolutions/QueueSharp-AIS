Imports System.Data.SqlClient
Imports System.Data
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    'Public Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("QisDBConn").ConnectionString)
    'Public Conn As SqlConnection = CenLinqDB.Common.Utilities.SqlDB.GetConnection()
    Public WriteOnly Property SetLblTempVDOPath() As String
        Set(ByVal value As String)
            txtTempVDOPath.Text = value
        End Set
    End Property
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Session("MyUser") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Try
            Dim usr As utils.User = CType(Session("MyUser"), utils.User)
            lblUser.Text = "&nbsp;&nbsp;&nbsp;" & usr.username & "&nbsp;&nbsp;&nbsp;"
            If Len(usr.ShopNameEN) > 0 Then
                lblShopName.Text = usr.ShopNameEN
            Else
                lblShopName.Visible = False
            End If
        Catch ex As Exception
        End Try

    End Sub

    Sub showError(ByVal msg As String, Optional ByVal isError As Boolean = True)
        If isError Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg.Replace("'", "\'") & "',{ buttons: { Ok: false},prefix:'jqir',overlayspeed: 'fast',opacity: 0.7 });", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg.Replace("'", "\'") & "',{ buttons: { Ok: false},prefix:'jqi',overlayspeed: 'fast',opacity: 0.7 });", True)
        End If
    End Sub


    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Engine.Common.FunctionEng.SaveQisDBLogOut(DirectCast(Session("MyUser"), utils.User).login_history_id)
        Response.Redirect("logout.aspx")
    End Sub

End Class

