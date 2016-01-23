Imports Engine.CallWebService
Imports Engine.Common

Partial Public Class frmFailOverLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClientIP As String = Request("ClientIP")
        Dim SoftwareName As String = Request("SoftwareName")
        Dim FunctionName As String = Request("FunctionName")
        Dim ActiveDB As String = Request("ActiveDB")
        Dim SQL As String = Request("SQL")

        Dim ret As String = CreateFailOverDBLog(ClientIP, SoftwareName, FunctionName, ActiveDB, SQL)
        If ret.Trim = "" Then
            Response.Write("SUCCESS")
        Else
            Response.Write(ret)
            FunctionEng.SaveShopErrorLog("frmFailOverLog_Page_Load", ret)
        End If
    End Sub


    Private Function CreateFailOverDBLog(ByVal ClinetIP As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal ActiveDB As String, ByVal SQL As String) As String
        Dim ret As String = ""
        Dim eng As New ShopWebServiceENG
        ret = eng.CreateFailOverDBLog(ClinetIP, SoftwareName, FunctionName, ActiveDB, SQL)
        eng = Nothing

        Return ret
    End Function

End Class