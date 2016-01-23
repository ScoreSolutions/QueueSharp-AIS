
Partial Class Template_ContentMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Title = Config.getReportVersion()
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        'Session.Abandon()
        Engine.Common.LoginENG.LogOut(Config.GetLoginHistoryID)
        Response.Redirect("../frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub

    
    Protected Sub btnGoBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoBack.Click
        Config.SaveTransLog("คลิกปุ่ม Go Back", "AisReports.Template_ContentMasterPage.btnGoBack_Click", Config.GetLoginHistoryID)
        Response.Redirect("../ReportApp/frmWelcomePage.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub

    Protected Sub btnMain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMain.Click
        Config.SaveTransLog("คลิกปุ่ม Main", "AisReports.Template_ContentMasterPage.btnGoBack_Click", Config.GetLoginHistoryID)
        Response.Redirect("../ReportApp/frmWelcomePage.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub
End Class

