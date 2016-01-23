Imports System.Data
Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Data.SqlClient
Imports CenLinqDB.Common.Utilities
Imports CenLinqDB.TABLE
Imports System.IO

Partial Class Video
    Inherits System.Web.UI.Page
    Dim shopid As String
    Dim ShopUseQM As String = "N"
    Dim ShopQMUrl As String = ""
    Dim ShopAbb As String = ""
    Dim MyUser As utils.User
    Dim delFileName As String = ""
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")
    Protected strMediaDefault As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack() Then
            'If Not IsNothing(Request.QueryString("qid")) Then
            '    Session("shopid") = Request.QueryString("shopid") & ""
            '    Session("qid") = Request.QueryString("qid") & ""
            '    Session("fdate") = Request.QueryString("fdate") & ""
            '    Session("ip") = Request.QueryString("ip") & ""
            '    Response.Redirect("VideoOnly.aspx")
            'Else
            '    If Me.lbl_shopid.Text = "" Then
            '        Me.lbl_shopid.Text = Session("shopid")
            '    End If

            '    If Me.lbl_qid.Text = "" Then
            '        Me.lbl_qid.Text = Session("qid")
            '    End If

            '    If Me.lbl_fdate.Text = "" Then
            '        Me.lbl_fdate.Text = Session("fdate")
            '    End If

            '    If Me.lbl_ip.Text = "" Then
            '        Me.lbl_ip.Text = Session("ip")
            '    End If

            '    If Trim(Request("shopid")) <> "" Then
            '        shopid = Request("shopid")
            '    Else
            '        shopid = Session("shopid")
            '    End If
            '    Engine.Common.FunctionEng.SaveTransLog("ShopWebQM_Video.aspx.Page_Load", "แสดง VDO ที่ QID=" & lbl_qid.Text & ":วันที่ " & lbl_fdate.Text)
            '    LoadData()
            '    Title = Version
            'End If

            MyUser = CType(Session("MyUser"), utils.User)
            If Trim(Request("shopid")) <> "" Then
                shopid = Request("shopid")
            Else
                shopid = Session("shopid")
            End If
            Dim sh As New CenLinqDB.TABLE.TbShopCenLinqDB
            sh.GetDataByPK(shopid, Nothing)
            If sh.ID <> 0 Then
                ShopAbb = sh.SHOP_ABB
                ShopQMUrl = sh.SHOP_QM_URL
                ShopUseQM = sh.SHOP_USE_QM
                LoadData()
            End If

            sh = Nothing
            Title = Version
        End If


        ' FP.File = "http://localhost/QMVDO/13082695.flv"
    End Sub

    Private Sub LoadData()
        ShowVideoOnly()
        'ShowVideo()
    End Sub

    Sub ShowVideoOnly()
        Dim fileName As String = ShopAbb & "_" & Request("fdate") & Request("qid") & ".flv"
        Dim fldPath As String = ConfigurationManager.AppSettings("tempvdo_path") & Request("uid")
        FP.File = ""
        FP.File = fldPath & "/" & fileName
        FP.Dispose()
    End Sub

    Sub ShowVideo()
        Dim fileName As String = ShopAbb & "_" & Request("fdate") & Request("qid") & ".flv"
        Dim fldPath As String = ConfigurationManager.AppSettings("tempvdo_path") & Request("uid")
        Dim fldPathNotUser As String = ConfigurationManager.AppSettings("tempvdo_path")
        Dim fldPathCopy As String = ConfigurationManager.AppSettings("tempvdo_path_center")


        Dim fldPathVideo As String = ConfigurationManager.AppSettings("tempvdo_path_center") & Request("uid")
        If System.IO.Directory.Exists(fldPathVideo) = False Then
            System.IO.Directory.CreateDirectory(fldPathVideo)
        End If

        Dim sSource As String
        Dim sTarget As String
        Dim isExists As Boolean = False
   

        Dim SourcePath As String = fldPathCopy & fileName 'This is just an example string and could be anything, it maps to fileToCopy in your code.
        Dim SaveDirectory As String = fldPathCopy
        Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, System.IO.Path.GetFileName(SourcePath)) 'combines the saveDirectory and the filename to get a fully qualified path.
        If System.IO.File.Exists(SavePath) Then
            sSource = fldPathCopy & fileName '"C:\something.txt"
            sTarget = fldPathCopy & Request("uid") & "/" & fileName
            File.Copy(sSource, sTarget, True)
            isExists = True
        End If
        FP.File = ""
        'Dim sbVideoDefault As New StringBuilder
        If isExists = False Then
            Dim f As String = fldPath & "/" & fileName
            'Dim f As String = fldPath & "/" & Request("fdate") & Request("qid") & ".flv"
            If WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", f) = True Then
                sSource = fldPathCopy & Request("uid") & "/" & fileName
                sTarget = fldPathCopy & fileName '"C:\something.txt"
                File.Copy(sSource, sTarget, True)

                'sbVideoDefault.Append("<a href=""" & f & """ style=""display:block;width:760px;height:305px""  id=""player"">  </a>" & vbCrLf)
                'sbVideoDefault.Append("<script>" & vbCrLf)
                'sbVideoDefault.Append("flowplayer(""player"", """ & Page.ResolveUrl("~/video/flowplayer-3.2.7.swf") & """);" & vbCrLf)
                'sbVideoDefault.Append("</script>" & vbCrLf)
                'strMediaDefault = sbVideoDefault.ToString

                FP.File = f
                'Engine.Common.FunctionEng.SaveTransLog(MyUser.login_history_id, "QM.vdo.aspx.Page_Load", "เล่นไฟล์ VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))
            End If
        Else
            Dim fNotUser As String = fldPath & "/" & fileName
            'sbVideoDefault.Append("<a href=""" & fNotUser & """ style=""display:block;width:760px;height:305px""  id=""player"">  </a>" & vbCrLf)
            'sbVideoDefault.Append("<script>" & vbCrLf)
            'sbVideoDefault.Append("flowplayer(""player"", """ & Page.ResolveUrl("~/video/flowplayer-3.2.7.swf") & """);" & vbCrLf)
            'sbVideoDefault.Append("</script>" & vbCrLf)
            'strMediaDefault = sbVideoDefault.ToString

            FP.File = fNotUser
            'Engine.Common.FunctionEng.SaveTransLog(MyUser.login_history_id, "QM.vdo.aspx.Page_Load", "เล่นไฟล์ VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))

        End If
        FP.Dispose()
    End Sub


    Private Function WriteTempFTP(ByVal ShopID As String, ByVal QID As String, ByVal ftpfile As String, ByVal filename As String) As Boolean
        Dim ret As Boolean = False

        Dim utl As New utils
        Dim shopftp As utils.FtpInfo = utl.GetShopFTP(Request("shopid"))
        Using FTP As New AlexPilotti.FTPS.Client.FTPSClient
            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential(shopftp.User, shopftp.Password)
            Try
                If ConfigurationManager.AppSettings("ftp_ssl") = "1" Then   'USE FTP/SSL (FTPS connection)

                    FTP.Connect(shopftp.Host, 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                                New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                                New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 30, True)
                Else
                    FTP.Connect(shopftp.Host, credentials)
                End If
                FTP.GetFile(ftpfile, Server.MapPath(filename))
                ret = True
            Catch ex As Exception
                ret = False
            End Try
        End Using

        Return ret
    End Function

    Public Function ValidateCertificate(ByVal sender As Object, _
                                      ByVal certificate As X509Certificate, _
                                      ByVal chain As X509Chain, _
                                      ByVal sslPolicyErrors As Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function



    Sub showError(ByVal msg As String, Optional ByVal isError As Boolean = True)
        If isError Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg.Replace("'", "\'") & "',{ buttons: { Ok: false},prefix:'jqir',overlayspeed: 'fast',opacity: 0.7 });", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg.Replace("'", "\'") & "',{ buttons: { Ok: false},prefix:'jqi',overlayspeed: 'fast',opacity: 0.7 });", True)
        End If
    End Sub
End Class
