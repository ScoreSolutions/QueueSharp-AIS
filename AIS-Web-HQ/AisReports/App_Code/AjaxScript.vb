﻿Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AjaxScript
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Sub SaveTransLog(ByVal TransDesc As String, ByVal LoginHisID As Long)
        Config.SaveTransLog(TransDesc, "AisReports.AjaxScript.SaveTransLog", LoginHisID)
    End Sub

End Class