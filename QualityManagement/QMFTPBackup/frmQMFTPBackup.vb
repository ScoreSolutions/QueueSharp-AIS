Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports QMFTPBackup.Org.Mentalis.Files
Imports System.IO

Public Class frmQMFTPBackup

    Function WriteTempFTP(ByVal shopFtpHost As String, ByVal User As String, ByVal Password As String, ByVal Localfile As String, ByVal DestinationFile As String) As Boolean
        Using FTP As New AlexPilotti.FTPS.Client.FTPSClient
            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential(User, Password)
            Try
                FTP.Connect(shopFtpHost, 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                            New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 2000, True)



                FTP.PutFile(Localfile, DestinationFile) 'eg. "/jddddd.flv"
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using

    End Function

    Function ValidateCertificate(ByVal sender As Object, _
                                  ByVal certificate As X509Certificate, _
                                  ByVal chain As X509Chain, _
                                  ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub btnSendVdo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendVdo.Click
        Dim INIFileName As String = Application.StartupPath & "\Config.ini"
        Dim Path As String
        Dim FtpHost As String
        Dim FtpUser As String
        Dim FtpPassword As String
        Dim ini As New IniReader(INIFileName)
        Path = ini.ReadString("Setting", "Path")
        FtpHost = ini.ReadString("Setting", "HostIP")
        FtpUser = ini.ReadString("Setting", "User")
        FtpPassword = ini.ReadString("Setting", "Password")

        If Directory.Exists(Path) Then
            Dim files() As String = IO.Directory.GetFiles(Path, "*.flv")
            If files.Length > 0 Then
                ProgressBar1.Maximum = files.Length
                ProgressBar1.Value = 0

                For Each f As String In IO.Directory.GetFiles(Path, "*.flv")
                    Dim file As New FileInfo(f)
                    Dim _filenames() As String = File.Name.Split(".")

                    If _filenames(0).EndsWith("_") = False Then
                        Dim ret As Boolean = WriteTempFTP(FtpHost, FtpUser, FtpPassword, f, "/" & file.Name)
                        If ret Then
                            'IO.File.SetAttributes(f, FileAttributes.Normal)
                            'IO.File.Delete(f)
                            My.Computer.FileSystem.RenameFile(f, _filenames(0) & "_." & _filenames(1))
                        End If
                    End If
                    file = Nothing
                    ProgressBar1.Value += 1
                    Application.DoEvents()
                    lblProcess.Text = "Processing.. " & ProgressBar1.Value & " of " & files.Length
                Next
            End If

        End If
        INIFileName = Nothing
        ini = Nothing

        MessageBox.Show("Success!!")
    End Sub
End Class
