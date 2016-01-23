
Partial Class Gsm
    Inherits System.Web.UI.MasterPage
    Dim version As String = System.Configuration.ConfigurationManager.AppSettings("version")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim cPara As CenParaDB.Appointment.CustomerWebAppointmentPara = globalFunction.GetCustomerLogin
        'If cPara.PreferLang = "Thai" Then
        '    Panel2.BackImageUrl = "~/image/5.png"
        'Else
        '    Panel2.BackImageUrl = "~/image/2.png"
        'End If

        'If Session("form") Is Nothing Then
        '    lbl_form.Text = ""
        'End If
        Page.Title = version
        'cPara = Nothing
    End Sub
End Class

