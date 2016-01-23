Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Partial Class FTPVDO

    Public Function WriteTempFTP(ByVal shopFtpHost As String, ByVal User As String, ByVal Password As String, ByVal Localfile As String, ByVal DestinationFile As String, ByVal FolderName As String, ByVal QMSplitFolder As String) As Boolean
        Using FTP As New AlexPilotti.FTPS.Client.FTPSClient
            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential(User, Password)
            Try
                FTP.Connect(shopFtpHost, 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                            New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 200, True)

                If QMSplitFolder = "Y" Then
                    CheckToDayDirectory(FTP, FolderName)
                End If

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

    Private Sub CheckToDayDirectory(ByVal FTP As AlexPilotti.FTPS.Client.FTPSClient, ByVal FolderName As String)
        Try
            FTP.GetDirectoryListUnparsed(FolderName)
        Catch ex As Exception
            FTP.MakeDir(FolderName)
        End Try
    End Sub
End Class
