Imports System.Data.SqlClient
Imports System.Data
Partial Class MasterLogin
    Inherits System.Web.UI.MasterPage
    'Public Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("QisDBConn").ConnectionString)
    Public Conn As SqlConnection = CenLinqDB.Common.Utilities.SqlDB.GetConnection()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'hideError()

        Dim tmp As String() = {"", ""}
        Session("DBTYPE") = ""
        Try
            'If Session("dbconnection") IsNot Nothing Then
            '    Conn = Session("dbconnection")
            'End If
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
                'Session("dbconnection") = Conn
            End If

            Session("DBTYPE") = "DB"
        Catch ex As Exception
            tmp(0) = Conn.DataSource
            Try
                Conn.ConnectionString = ConfigurationManager.ConnectionStrings("QisDBConn2").ConnectionString
                If Conn.State <> ConnectionState.Open Then
                    Conn.Open()
                    'Session("dbconnection") = Conn
                End If
                Session("DBTYPE") = "DR"
            Catch ex2 As Exception
                tmp(1) = Conn.DataSource
                showError("Attempt1:[" & tmp(0) & "] <br/>" & ex.Message & "<br/><br/>Attempt2:[" & tmp(1) & "] <br/>" & ex2.Message)
            End Try
        End Try
    End Sub

    Sub showError(ByVal msg As String, Optional ByVal isError As Boolean = True)
        If isError Then
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg & "',{ buttons: { Ok: false},prefix:'jqir',overlayspeed: 'fast',opacity: 0.7 });", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "alert('" & msg & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg & "',{ buttons: { Ok: false},prefix:'jqi',overlayspeed: 'fast',opacity: 0.7 });", True)
        End If

        'lblError.Text = msg
        'lblError.Visible = True
    End Sub

End Class

