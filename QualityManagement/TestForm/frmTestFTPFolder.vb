Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class frmTestFTPFolder

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Using FTP As New AlexPilotti.FTPS.Client.FTPSClient
            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential("vdo", "vdo1234")
            Try
                'FTP.Connect(shopFtpHost, 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                '            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                '            New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 200, True)
                'FTP.PutFile(Localfile, DestinationFile) 'eg. "/jddddd.flv"

                FTP.Connect("172.16.59.149", 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                            New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 200, True)

                CheckToDayDirectory(FTP)
                FTP.PutFile("D:\Movie\GI_JOE_RETALIATION_2013.iso", "/20130911/GI_JOE_RETALIATION_2013.iso")
                'Return True
            Catch ex As Exception
                'Return False
            End Try
        End Using
    End Sub

    Private Sub CheckToDayDirectory(ByVal FTP As AlexPilotti.FTPS.Client.FTPSClient)
        Dim DirName As String = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        Try
            FTP.GetDirectoryListUnparsed(DirName)
        Catch ex As Exception
            FTP.MakeDir(DirName)
        End Try
    End Sub

    Public Function ValidateCertificate(ByVal sender As Object, _
                                      ByVal certificate As X509Certificate, _
                                      ByVal chain As X509Chain, _
                                      ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function
End Class