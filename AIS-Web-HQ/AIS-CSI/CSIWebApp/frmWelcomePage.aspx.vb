﻿Imports System.Data

Partial Class ReportApp_frmWelcomePage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
        If uPara.LOGIN_HISTORY_ID <> 0 Then
            lblUser.Text = "ยินดีต้อนรับคุณ " & uPara.USER_PARA.FNAME & " " & uPara.USER_PARA.LNAME & " เข้าสู่ระบบ"
            uPara = Nothing

            If Request("NoAuthen") IsNot Nothing Then
                lblUser.Text = "คุณไม่ได้รับสิทธิ์ให้ดำเนินการ"
                lblUser.ForeColor = Drawing.Color.Red
            End If
        Else
            Response.Redirect("../frmLogin.aspx")
        End If
    End Sub
End Class