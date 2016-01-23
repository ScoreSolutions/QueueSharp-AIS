Imports System.Data.SqlClient
Imports System.Data
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    'Public Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("QisDBConn").ConnectionString)
    Public Conn As SqlConnection = CenLinqDB.Common.Utilities.SqlDB.GetConnection()
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Session("MyUser") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''        hideError()
        'Dim tmp(1) As String
        'Try
        '    tmp(0) = Conn.DataSource
        '    If Conn.State <> ConnectionState.Open Then
        '        Conn.Open()
        '    End If

        '    Session("DBTYPE") = "DB"
        '    lblConnInfo.Text = Conn.DataSource

        'Catch ex As Exception
        '    Try
        '        Conn.ConnectionString = ConfigurationManager.ConnectionStrings("QisDBConn2").ConnectionString
        '        tmp(1) = Conn.DataSource
        '        Conn.Open()
        '        Session("DBTYPE") = "DR"
        '        lblConnInfo.Text = Conn.DataSource
        '    Catch ex2 As Exception
        '        showError("Attempt1[" & tmp(0) & "]: <br/>" & ex.Message & "<br/><br/>Attempt2[" & tmp(0) & "]: <br/>" & ex2.Message)
        '    End Try

        'End Try
        Try
            Dim usr As utils.User= CType(Session("MyUser"), utils.User)

            lblUser.Text = "&nbsp;&nbsp;&nbsp;" & usr.username & "&nbsp;&nbsp;&nbsp;"
            If Len(Trim(Request("shopid"))) > 0 Then
                Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
                shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(Trim(Request("shopid")))
                If shLnq.ID <> 0 Then
                    lblShopName.Text = shLnq.SHOP_NAME_EN
                End If
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
        Engine.Common.LoginENG.LogOut(DirectCast(Session("MyUser"), utils.User).login_history_id)

        Session.RemoveAll()
        Response.Redirect("logout.aspx")
    End Sub

End Class

