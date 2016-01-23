Imports System.Data
Imports Engine
Imports System.IO
Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class frmAisUpdateMasterAgent
    Dim _ErrorMsg As String
    Dim _strDateNow As String

#Region "Convert Image"
    Public Function ConvertImageFromDB() As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean = eng.ConvertImageFromDB
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing

        Return ret
    End Function

    Public Function TransferMainDisplayVDOFile() As Boolean
        Dim ret As Boolean = False
        Dim eng As New UpdateMasterENG
        Dim dt As DataTable = eng.GetMainDisplayVDOFile
        _ErrorMsg = eng.ErrorMessage
        If _ErrorMsg.Trim = "" Then
            ret = True
        End If
        eng = Nothing

        If ret = True Then
            If dt.Rows.Count > 0 Then
                Dim ini As New CenLinqDB.Common.Utilities.IniReader(Application.StartupPath & "\Config.ini")
                ini.Section = "MainDisplay"

                Dim ShopFTPUserID As String = ini.ReadString("ShopFTPUserID")
                Dim ShopFTPPassword As String = ini.ReadString("ShopFTPPassword")
                Dim VDOFileName As String = ini.ReadString("VDOFileName")
                For Each dr As DataRow In dt.Rows
                    If Convert.IsDBNull(dr("shop_qm_url")) = False Then
                        Dim LocalFile As String = dr("folder_name") & "\" & dr("file_name")
                        If File.Exists(LocalFile) = True Then
                            ret = WriteTempFTP(dr("shop_qm_url"), ShopFTPUserID, ShopFTPPassword, LocalFile, "/" & VDOFileName)
                            If ret = False Then
                                Exit For
                            Else
                                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                                If trans.Trans IsNot Nothing Then
                                    Dim lnq As New CenLinqDB.TABLE.TbShopFileScheduleCenLinqDB
                                    lnq.GetDataByPK(dr("id"), trans.Trans)

                                    If lnq.ID > 0 Then
                                        lnq.TRANSFER_STATUS = "2"c
                                        If lnq.UpdateByPK("frmAisUpdateMasterAgent.TransferMainDisplayVDOFile", trans.Trans) = True Then
                                            trans.CommitTransaction()
                                        Else
                                            trans.RollbackTransaction()
                                        End If
                                    Else
                                        trans.RollbackTransaction()
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                ini = Nothing
            End If
            dt.Dispose()
        End If

        Return ret
    End Function

    Private Function WriteTempFTP(ByVal shopFtpHost As String, ByVal User As String, ByVal Password As String, ByVal Localfile As String, ByVal DestinationFile As String) As Boolean
        Using FTP As New AlexPilotti.FTPS.Client.FTPSClient
            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential(User, Password)
            Try
                FTP.Connect(shopFtpHost, 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                            New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 200, True)

                FTP.PutFile(Localfile, DestinationFile) 'eg. "/jddddd.flv"
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using

    End Function

    Public Function ValidateCertificate(ByVal sender As Object, _
                                      ByVal certificate As X509Certificate, _
                                      ByVal chain As X509Chain, _
                                      ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function
#End Region

#Region "Update Master Data at Schedult"
    Private Function UpdateMaster(ByVal strDateNow As String, ByVal TableName As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean
        ret = eng.UpdateMaster(strDateNow, TableName)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing
        Return ret
    End Function

    Private Function UpdateItem(ByVal strDateNow As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean
        ret = eng.UpdateItem(strDateNow)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing
        Return ret
    End Function

    Private Function UpdateHoldReason(ByVal strDateNow As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean
        ret = eng.UpdateHoldReason(strDateNow)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing
        Return ret
    End Function

    Private Function UpdateLogoutReason(ByVal strDateNow As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean
        ret = eng.UpdateLogoutReason(strDateNow)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing
        Return ret
    End Function

    Private Function UpdateSkill(ByVal strDateNow As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean
        ret = eng.UpdateSkill(strDateNow)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing
        Return ret
    End Function

    Private Function UpdateUser(ByVal vDateNow As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean = eng.UpdateUser(vDateNow)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing

        Return ret
    End Function

    Private Function UpdateCounter(ByVal vDateNow As String) As Boolean
        Dim eng As New UpdateMasterENG
        Dim ret As Boolean = eng.UpdateCounter(vDateNow)
        If ret = False Then
            _ErrorMsg = eng.ErrorMessage
        End If
        eng = Nothing

        Return ret
    End Function
#End Region

#Region "Form Control"
    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub frmAisUpdateMasterAgent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub frmAisUpdateMasterAgent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = "AIS Update Mater Agent V" & getMyVersion()
        NotifyIcon1.Text = "AIS Update Mater Agent V" & getMyVersion()
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function
#End Region


    Dim LastUpdateTime As DateTime = DateTime.Now
    Dim RefreshTime As Integer = Convert.ToInt16(Engine.Common.FunctionEng.GetQisDBConfig("RefreshUpdateMasterAgent"))
    Private Sub tmrUpdateMaster_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdateMaster.Tick
        If DateAdd(DateInterval.Minute, RefreshTime, LastUpdateTime) < DateTime.Now Then
            tmrUpdateMaster.Enabled = False
            _strDateNow = DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US"))
            Dim err As String = ""
            Dim ret As Boolean
            ret = UpdateMaster(_strDateNow, "TB_CFG_SW_SCHED_KIOSK")
            If ret = False Then
                err = "KIOSK :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateMaster(_strDateNow, "TB_CFG_SW_SCHED_MAINDISPLAY")
            If ret = False Then
                err &= "MAINDISPLAY :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateMaster(_strDateNow, "TB_CFG_SW_SCHED_QSHARP")
            If ret = False Then
                err &= "QSHARP :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateItem(_strDateNow)
            If ret = False Then
                err &= "ITEM :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateHoldReason(_strDateNow)
            If ret = False Then
                err &= "HOLD REASON :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateLogoutReason(_strDateNow)
            If ret = False Then
                err &= "LOGOUT REASON :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateSkill(_strDateNow)
            If ret = False Then
                err &= "SKILL :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateUser(_strDateNow)
            If ret = False Then
                err += "USER :: " & _ErrorMsg & vbCrLf
            End If

            ret = UpdateCounter(_strDateNow)
            If ret = False Then
                err += "COUNTER :: " & _ErrorMsg & vbCrLf
            End If

            ret = ConvertImageFromDB()
            If ret = False Then
                err += "CONVERT IMAGE FROM DB :: " & _ErrorMsg & vbCrLf
            End If

            'ret = TransferMainDisplayVDOFile()
            'If ret = False Then
            '    err += "TRANSFER MAIN DISPLAY VDO FILE :: " & _ErrorMsg & vbCrLf
            'End If

            If err <> "" Then
                txtLog.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & "  " & err
            End If
            tmrUpdateMaster.Enabled = True
        End If
    End Sub

    Dim CurrDate As DateTime = DateTime.Now
    Private Sub tmTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTime.Tick
        tmTime.Enabled = False
        Dim vDateNow As DateTime = DateTime.Now
        lblTime.Text = vDateNow.ToString("dd/MM/yyyy HH:mm:ss", New Globalization.CultureInfo("en-US"))
        If CurrDate < New Date(vDateNow.Year, vDateNow.Month, vDateNow.Day, vDateNow.Hour, 0, 0) Then
            CurrDate = DateTime.Now
            CreateLogFile()
        End If

        tmTime.Enabled = True
    End Sub

    Private Sub CreateLogFile()
        If txtLog.Text.Trim <> "" Then
            Dim LogPath As String = Application.StartupPath & "\Log"
            If Directory.Exists(LogPath) = False Then
                Directory.CreateDirectory(LogPath)
            End If

            Try
                Dim FILE_NAME As String = LogPath & "\" & DateTime.Now.ToString("yyyyMMddHH") & ".log"
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter.WriteLine(txtLog.Text)
                objWriter.Close()

                txtLog.Text = ""
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtLog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLog.KeyPress
        e.Handled = True
    End Sub
End Class
