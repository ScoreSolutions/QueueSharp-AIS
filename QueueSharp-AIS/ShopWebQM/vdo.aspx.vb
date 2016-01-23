Imports System.Data
Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Partial Class vdo
    Inherits System.Web.UI.Page
    Dim MyUser As utils.User
    Dim delFileName As String = ""
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyUser = CType(Session("MyUser"), utils.User)
        
        Engine.Common.FunctionEng.SaveQisDBTransLog(MyUser.login_history_id, "QM.vdo.aspx.Page_Load", "แสดง VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))
        LoadData()
        Title = Version
    End Sub
    Sub LoadData()
        Dim utl As New utils

        lblShop.Text = MyUser.ShopNameEN & IIf(MyUser.ShopNameTH <> "", " (" & MyUser.ShopNameTH & ")", "")

        Dim dt As New DataTable
        dt = utl.GetQueueInfo(Request("qid"), Request("fdate"))
        If dt.Rows.Count > 0 Then
            lblDate.Text = "Date : " & Convert.ToDateTime(dt.Rows(0)("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
        End If
        gvQ.DataSource = dt
        gvQ.DataBind()
        dt.Dispose()
        If Not IsPostBack Then
            ShowVDO()
        End If

    End Sub
    Sub ShowVDO()
        Dim fldPath As String = ConfigurationManager.AppSettings("TempVDOPath") & "/" & MyUser.username
        FP.File = ""
        Dim f As String = fldPath & "/" & Request("fdate") & Request("qid") & ".flv"
        FP.File = f
        'Engine.Common.FunctionEng.SaveQisDBTransLog(MyUser.login_history_id, "ShopWebQM.vdo.aspx.Page_Load", "เล่นไฟล์ VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))
        FP.Dispose()
    End Sub

End Class
