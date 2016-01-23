
Partial Class _Default
    Inherits System.Web.UI.Page

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    'ifrm.Attributes.Add("src", "http://localhost/e-Sevice/frm_login.aspx?mobileparam=HWWQ+8Mw8pQvsXVTAIBpHxSvBo12nzwsh3gAqi4qDw9HpxdR9PycgynfCnQsDizlahdpI+wLOizlQsy+TAm3RQ==")

    'End Sub

    'Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
    '    If txtPara.Text <> "" Then
    '        'Response.Redirect("frm_login.aspx?mobileparam=" & txtPara.Text)
    '        ifrm.Attributes.Add("src", Request.Url.AbsoluteUri.Replace("Default.aspx", "frm_login.aspx?mobileparam=" & txtPara.Text))

    '    Else
    '        'MsgBox("Please input parameter!", MsgBoxStyle.Critical)
    '        ViewState("PreviousPage") = Request.UrlReferrer
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Please Input Parameter !!!');", True)
    '        If ViewState("PreviousPage") IsNot Nothing Then Response.Redirect(ViewState("PreviousPage").ToString())
    '    End If
    'End Sub
End Class
