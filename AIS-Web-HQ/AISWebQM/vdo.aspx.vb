Imports System.Data
Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports CenLinqDB.TABLE
Imports System.IO

Partial Class vdo
    Inherits System.Web.UI.Page
    Dim shopid As String
    Dim shopNameEn As String
    Dim MyUser As utils.User
    'Dim delFileName As String = ""
    Dim Version As String = System.Configuration.ConfigurationManager.AppSettings("Version")

    Dim _err As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyUser = CType(Session("MyUser"), utils.User)
        If Trim(Request("shopid")) <> "" Then
            shopid = Request("shopid")
        Else
            shopid = Session("shopid")
        End If
        Engine.Common.FunctionEng.SaveTransLog(MyUser.login_history_id, "QM.vdo.aspx.Page_Load", "แสดง VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))
        LoadData()
        Title = Version
    End Sub
    Sub LoadData()
        Dim utl As New utils
        Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
        shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(shopid)
        If shLnq.ID > 0 Then
            lblShop.Text = shLnq.SHOP_NAME_EN & "(" & shLnq.SHOP_NAME_TH & ")"
            shopNameEn = shLnq.SHOP_NAME_EN
        End If

        Dim dt As New DataTable
        dt = utl.GetQueueInfo(shopid, Request("qid"), Request("fdate"))
        If dt.Rows.Count > 0 Then
            lblDate.Text = "Date : " & Convert.ToDateTime(dt.Rows(0)("service_date")).ToString("dd/MM/yyyy", New Globalization.CultureInfo("th-TH"))
            'If UCase(MyUser.username) <> dt.Rows(0)("username").ToString.ToUpper AndAlso ("," & MyUser.view_others_vdo & ",").Contains(shopid) = False Then 'ถ้าไม่ใช่ vdo ของตัวเอง จะต้องได้รับอณุญาตให้ดูของคนอื่นได้ จากหน้า Manage QM user
            '    Master.showError("You are not authorized to view this Video.")
            '    shLnq = Nothing
            '    Exit Sub
            'End If
        End If
        gvQ.DataSource = dt
        gvQ.DataBind()

        If Not IsPostBack Then
            dvExternal.Style.Add("display", "none")
            dvInternal.Style.Add("display", "")
            ShowVDO_Internal(shLnq)
        End If
        shLnq = Nothing
    
    End Sub

    Sub ShowVDO_Internal(ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        'Dim fldTempVdo As String = ConfigurationManager.AppSettings("tempvdo_path")
        Dim fldPathCenter As String = ConfigurationManager.AppSettings("tempvdo_path_center")
        Dim fldPathUser As String = ConfigurationManager.AppSettings("tempvdo_path_center") & MyUser.username & "/"

        If System.IO.Directory.Exists(fldPathCenter) = False Then
            System.IO.Directory.CreateDirectory(fldPathCenter)
        End If
        If System.IO.Directory.Exists(fldPathUser) = False Then
            System.IO.Directory.CreateDirectory(fldPathUser)
        End If
        
        'Dim strShopid As String = Request("shopid")
        Dim strqid As String = Request("qid")
        Dim fdate As String = Request("fdate")
        Dim rnd As String = DateTime.Now.Millisecond
        Dim strHostName As String
        strHostName = System.Net.Dns.GetHostName()
        Dim strip As String = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()
        

        Dim IsOpenFile As Boolean = False
        Dim descFile As String = shLnq.SHOP_ABB & "_" & Request("fdate") & Request("qid") & ".flv"
        'ถ้า User เคยเปิดไฟล์นี้มาก่อนอยู่แล้ว ก็ไม่ต้องไปดึงมาใหม่
        If File.Exists(fldPathUser & descFile) = False Then
            'ถ้าไฟล์นี้เคยถูกเปิดโดย User อื่นๆ มาก่อน ก็ให้ไป Copy มาเลย
            If File.Exists(fldPathCenter & descFile) = False Then

                If WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", fldPathCenter & descFile, "20" & Request("fdate")) = True Then
                    'ถ้าไฟล์นี้ไม่เคยถูกเปิดเลย
                    Try
                        File.Copy(fldPathCenter & descFile, fldPathUser & descFile)
                        IsOpenFile = True
                    Catch ex As Exception
                        'Master.showError(ex.Message)
                        _err = ex.Message
                    End Try
                End If
            Else
                Try
                    File.Copy(fldPathCenter & descFile, fldPathUser & descFile)
                    IsOpenFile = True
                Catch ex As Exception
                    _err = ex.Message
                End Try
            End If
        Else
            IsOpenFile = True
        End If

        If IsOpenFile = True Then
            Dim strUrlParameter As String = "http://" & strip & "/AISWebQMVideo/VideoOnly.aspx?qid=" & strqid & _
                      "&fdate=" & fdate & _
                      "&shopid=" & shLnq.ID & _
                      "&uid=" & MyUser.username & _
                      "&rnd=" & rnd
            frame1.Attributes.Add("src", strUrlParameter)
        Else
            Master.showError(_err)
        End If
    End Sub
    'Sub ShowVDO_Transfer()


    '    Dim fileName As String = shopNameEn & Request("fdate") & Request("qid") & ".flv"
    '    Dim fldPath As String = ConfigurationManager.AppSettings("tempvdo_path")
    '    Dim fldPathSelf As String = ConfigurationManager.AppSettings("tempvdo_path_self")
    '    Dim fldPathCenter As String = ConfigurationManager.AppSettings("tempvdo_path_center") & MyUser.username
    '    Dim fldPathSub As String = ConfigurationManager.AppSettings("tempvdo_path_center")
    '    ' Dim fldPathSelf As String = ConfigurationManager.AppSettings("tempvdo_path_self")

    '    If System.IO.Directory.Exists(fldPathSelf) = False Then
    '        System.IO.Directory.CreateDirectory(fldPathSelf)
    '    End If
    '    If System.IO.Directory.Exists(fldPathCenter) = False Then
    '        System.IO.Directory.CreateDirectory(fldPathCenter)
    '    End If
    '    If System.IO.Directory.Exists(fldPathSub) = False Then
    '        System.IO.Directory.CreateDirectory(fldPathSub)
    '    End If

    '    Dim isExists As Boolean
    '    Dim sSource As String
    '    Dim sTarget As String

    '    Dim SourcePath As String = fldPathSelf & fileName 'This is just an example string and could be anything, it maps to fileToCopy in your code.
    '    Dim SaveDirectory As String = fldPathSelf
    '    Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, System.IO.Path.GetFileName(SourcePath)) 'combines the saveDirectory and the filename to get a fully qualified path.
    '    If System.IO.File.Exists(SavePath) Then
    '        sSource = fldPathSelf & fileName '"C:\something.txt"
    '        sTarget = fldPathCenter & "/" & fileName
    '        File.Copy(sSource, sTarget, True)
    '        isExists = True
    '    End If

    '    Dim f As String = fldPathSelf & "/" & fileName
    '    If isExists = False Then
    '        If WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", f) = True Then
    '            sSource = fldPathSelf & fileName '"C:\something.txt"
    '            sTarget = fldPathCenter & "/" & fileName
    '            File.Copy(sSource, sTarget, True)
    '        End If
    '    End If


    '    'http://localhost:2859/AISWebQMVideo/VideoOnlyFTP2.aspx

    '    Dim shLnq As New TbShopCenLinqDB
    '    Dim strShopid As String = Request("shopid")
    '    Dim strqid As String = Request("qid")
    '    Dim fdate As String = Request("fdate")
    '    Dim rnd As String = DateTime.Now.Millisecond
    '    Dim strHostName As String
    '    strHostName = System.Net.Dns.GetHostName()
    '    Dim strip As String = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()
    '    'shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(shopid)
    '    'If shLnq.ID <> 0 Then
    '    '    strip = shLnq.SHOP_QM_URL
    '    'End If
    '    'shLnq = Nothing
    '    ' ?qid=1662&fdate=131014&shopid=1&uid=Admin
    '    Dim strUrlParameter As String = "http://" & strip & "/AISWebQMVideo/VideoOnly.aspx?qid=" & strqid & _
    '                  "&fdate=" & fdate & _
    '                  "&shopid=" & strShopid & _
    '                  "&uid=" & MyUser.username

    '    frame1.Attributes.Add("src", strUrlParameter)
    'End Sub

    'Sub ShowVDO()
    '    Dim fldPath As String = ConfigurationManager.AppSettings("tempvdo_path") & MyUser.username

    '    Dim utl As New utils
    '    If System.IO.Directory.Exists(Server.MapPath(fldPath)) = False Then
    '        System.IO.Directory.CreateDirectory(Server.MapPath(fldPath))
    '    End If



    '    FP.File = ""
    '    Dim f As String = fldPath & "/" & Request("qid") & "_" & Session.SessionID & ".flv"
    '    'Dim f As String = fldPath & "/" & Request("fdate") & Request("qid") & ".flv"
    '    If WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", f) = True Then
    '        FP.File = f
    '        Engine.Common.FunctionEng.SaveTransLog(MyUser.login_history_id, "QM.vdo.aspx.Page_Load", "เล่นไฟล์ VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))
    '    End If
    '    FP.Dispose()



    '    'Dim fldPath As String = ConfigurationManager.AppSettings("QMTempFolder") & MyUser.username
    '    'Dim utl As New utils
    '    'If System.IO.Directory.Exists(fldPath) = False Then
    '    '    System.IO.Directory.CreateDirectory(fldPath)
    '    'End If
    '    'FP.File = ""
    '    'Dim f As String = fldPath & "/" & Session.SessionID & ".flv"
    '    'WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", f)
    '    'FP.File = f
    'End Sub

    'Sub ShowVDO2()
    '    Dim fileName As String = Request("fdate") & Request("qid") & ".flv"
    '    Dim fldPath As String = ConfigurationManager.AppSettings("tempvdo_path") & MyUser.username
    '    Dim f As String = fldPath & "/" & fileName
    '    Dim fldPathCopy As String = ConfigurationManager.AppSettings("tempvdo_path")
    '    Dim fCopy As String = fldPathCopy & "/" & fileName
    '    Dim utl As New utils
    '    If System.IO.Directory.Exists(fldPath) = False Then
    '        System.IO.Directory.CreateDirectory(fldPath)
    '    End If

    '    Dim isExists As Boolean = False
    '    'If System.IO.Directory.Exists(fldPathCopy & fileName) = True Then
    '    '    '  System.IO.Directory.CreateDirectory(Server.MapPath(fldPath))
    '    '    Dim sSource As String = fldPathCopy & fileName '"C:\something.txt"
    '    '    Dim sTarget As String = fldPath & "/" & fileName
    '    '    File.Copy(sSource, sTarget, True)
    '    '    ' File.Copy(sSource, Path.Combine(MyUser.username, Path.GetFileName(sSource)), True)
    '    '    isExists = True
    '    'End If


    '    FP.File = ""
    '    ' Dim f As String = fldPath & "/" & Request("qid") & "_" & Session.SessionID & ".flv"
    '    'Dim f As String = fldPath & "/" & Request("fdate") & Request("qid") & ".flv"
    '    If isExists = False Then
    '        If WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", f) = True Then
    '            FP.File = f
    '            Engine.Common.FunctionEng.SaveTransLog(MyUser.login_history_id, "QM.vdo.aspx.Page_Load", "เล่นไฟล์ VDO ที่ QID=" & Request("qid") & ":วันที่ " & Request("fdate"))
    '        End If
    '    Else
    '        FP.File = f
    '    End If
    '    FP.Dispose()



    '    'Dim fldPath As String = ConfigurationManager.AppSettings("QMTempFolder") & MyUser.username
    '    'Dim utl As New utils
    '    'If System.IO.Directory.Exists(fldPath) = False Then
    '    '    System.IO.Directory.CreateDirectory(fldPath)
    '    'End If
    '    'FP.File = ""
    '    'Dim f As String = fldPath & "/" & Session.SessionID & ".flv"
    '    'WriteTempFTP(shopid, Request("qid"), Request("fdate") & Request("qid") & ".flv", f)
    '    'FP.File = f
    'End Sub

    Private Function WriteTempFTP(ByVal ShopID As String, ByVal QID As String, ByVal ftpfile As String, ByVal filename As String, ByVal FileDate As String) As Boolean
        Dim ret As Boolean = False

        Dim utl As New utils
        Dim shopftp As utils.FtpInfo = utl.GetShopFTP(ShopID)
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

                Try
                    FTP.GetFile(FileDate & "/" & ftpfile, filename)
                    ret = True
                Catch ex As Exception
                    If ex.Message.Trim = "File not found" Then
                        FTP.GetFile(ftpfile, filename)
                        ret = True
                    Else
                        _err = ex.Message
                    End If
                End Try
            Catch ex As Exception
                ret = False
                _err = ex.Message & " $$$$ " & vbNewLine & ex.StackTrace
                Master.showError(ex.Message & " $$$$ " & vbNewLine & ex.StackTrace)
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

    'Sub WriteTemp(ByVal ShopID As String, ByVal QID As String, ByVal filename As String)
    '    Dim utl As New utils

    '    Dim dt As New System.Data.DataTable
    '    dt = utl.GetVDObyQID(ShopID, QID, Master.Conn)
    '    If dt.Rows.Count > 0 Then
    '        Dim buffer As Byte() = dt.Rows(0)("capture")
    '        FileIO.FileSystem.WriteAllBytes(Server.MapPath(filename), buffer, False)
    '    End If
    'End Sub
End Class
