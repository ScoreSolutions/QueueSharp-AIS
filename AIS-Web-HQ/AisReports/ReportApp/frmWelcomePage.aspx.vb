Imports System.Data

Partial Class ReportApp_frmWelcomePage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
        If uPara.LOGIN_HISTORY_ID <> 0 Then
            lblWelcome.Text = "Welcome : " & uPara.USER_PARA.FNAME & " " & uPara.USER_PARA.LNAME
            CreateReportMenuList(uPara)

            Dim mas As MasterPage = Me.Master
            Dim btn As Button = mas.FindControl("btnGoBack")
            btn.Visible = False
            uPara = Nothing
        Else
            Response.Redirect("../frmLogin.aspx")
        End If
    End Sub

    Private Sub CreateReportMenuList(ByVal uPara As CenParaDB.Common.UserProfilePara)
        Dim ret As String = "<table border='0' cellpadding='0' cellspacing='0' width='100%'>"
        ret += "    <tr>"
        ret += "        <td width='10%'>&nbsp;</td>"
        ret += "        <td width='90%'>&nbsp;</td>"
        ret += "    </tr>"
        Dim dt As New DataTable
        If uPara.USERNAME.ToUpper <> "ADMIN" Then
            dt = Engine.Common.MenuENG.GetMenuList(1, uPara.USERNAME)
        Else
            dt = Engine.Common.MenuENG.GetMenuList(1)
        End If

        If dt.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each dr As DataRow In dt.Rows
                ret += "    <tr>"
                ret += "        <td >" & (i + 1) & ".</td>"
                ret += "        <td align='left' ><a href='" & dr("menu_url") & "?rnd=" & DateTime.Now.Millisecond & "' onClick=""SaveTransLog(" & Chr(39) & "คลิกเมนู : " & dr("menu_name_en") & Chr(39) & ",'" & uPara.LOGIN_HISTORY_ID & "')"" >" & dr("menu_name_en") & "</a></td>"
                ret += "    </tr>"
                i += 1
            Next
        End If
        ret += "</table>"
        dt.Dispose()

        lblReportList.Text = ret
    End Sub

    Protected Sub btnTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.Click
        'Dim para As New CenParaDB.ReportCriteria.AverageServiceTimeWithKPIPara
        'para.ShopID = 1
        'para.IntervalMinute = 30
        'para.DateFrom = New Date(2012, 3, 14)
        'para.DateTo = New Date(2012, 4, 11)
        'para.TimePeroidFrom = "11:00"
        'para.TimePeroidTo = "20:00"

        'Dim eng As New Engine.Reports.RepAverageServiceTimeComparingWithKPI
        'eng.GetReport(para)

        'Dim para As New CenParaDB.ReportCriteria.CapacityReportByShopPara
        'para.ShopID = 1
        'para.IntervalMinute = 30
        'para.DateFrom = New Date(2012, 3, 14)
        'para.DateTo = New Date(2012, 4, 11)
        'para.TimePeroidFrom = "11:00"
        'para.TimePeroidTo = "20:00"
        'Dim eng As New Engine.Reports.RepCapacityByShopENG

        'eng.GetReports(para)

    End Sub
End Class
