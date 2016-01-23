Imports System.Net
Imports System.IO

Public Class frmTestPictureHTTPS
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim UserName As String = "narumolo"
            Dim Pwd As String = "Kumi*115"
            Dim txtUrl As String = "https://acc3.ais.co.th/cmsx/AIS/ACC/Serenade%20PA/info/PublishingImages/PA19/arunee19.jpg"

            'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
            System.Net.ServicePointManager.ServerCertificateValidationCallback = _
              Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
              chain As System.Security.Cryptography.X509Certificates.X509Chain, _
              sslerror As System.Net.Security.SslPolicyErrors) True

            Dim req As HttpWebRequest = WebRequest.Create(txtUrl)
            req.Method = "GET"
            req.ContentType = "image/jpeg"
            req.UserAgent = "Mozilla/4.0+(compatible;+MSIE+5.01;+Windows+NT+5.0"
            req.Credentials = New NetworkCredential(UserName, Pwd)

            Dim res As WebResponse = req.GetResponse
            Dim theImage As System.Drawing.Image = System.Drawing.Image.FromStream(res.GetResponseStream())
            PictureBox1.Image = theImage
        Catch ex As Exception

        End Try
        
    End Sub
End Class
