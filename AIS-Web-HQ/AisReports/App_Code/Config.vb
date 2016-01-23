Imports Microsoft.VisualBasic
Imports CenParaDB.Common
Imports System.Web
Imports System.Web.Security
Imports System.Web.HttpContext
Imports System.Web.UI
Imports System.Xml.Serialization
Imports System.IO

Public Class Config
    Public Shared Function getReportVersion() As String
        Return ConfigurationManager.AppSettings("ReportVersion").ToString
    End Function
    Public Shared Sub SaveTransLog(ByVal TransDesc As String, ByVal ClassName As String, ByVal LoginHisID As Long)
        Engine.Common.FunctionEng.SaveTransLog(LoginHisID, ClassName, TransDesc)
    End Sub

    Public Shared Function GetLoginHistoryID() As Long
        Dim tmp As LoginSessionPara
        tmp = Engine.Common.LoginENG.GetLoginSessionPara()
        Dim ret As Long = tmp.LOGIN_HISTORY_ID
        tmp = Nothing
        Return ret
    End Function

    Public Shared Function GetLogOnUser() As UserProfilePara
        Dim ret As New UserProfilePara
        ret = Engine.Common.LoginENG.GetLogOnUser
        Return ret
    End Function
End Class
