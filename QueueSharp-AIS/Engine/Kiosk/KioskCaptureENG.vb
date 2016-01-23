Imports System.IO
Imports AMS
Imports System.Windows.Forms
Imports System.Drawing


Namespace Kiosk
    Public Class KioskCaptureENG : Inherits KioskBaseENG
        Public Sub DeleteTempCaptureAllFile()
            Dim Path As String = GetCapturePath()
            For Each _file As String In System.IO.Directory.GetFiles(Path)
                File.Delete(_file)
            Next
        End Sub

        Public Sub DeleteTempCaptureFile(ByVal MobileNo As String)
            Dim Path As String = GetCapturePath()
            If File.Exists(Path & MobileNo & ".jpg") = True Then
                File.Delete(Path & MobileNo & ".jpg")
            End If
        End Sub

        Public Sub SaveImageCapture(ByVal ConfigINI As Profile.Ini, ByVal image As System.Drawing.Image, _
                                    ByVal pcCapture As PictureBox, ByVal btnDisplayImage As Button, ByVal frmDialogMsg As Form, ByVal MobileNo As String)

            '======= บันทึกรูปภาพแบบปกติ =====
            Dim FileName As String = "Temp.jpg"
            Dim Path As String = GetCapturePath 'ConfigINI.GetValue("CaptureImage", "Path", "")
            Dim Path1 As String = Path & FileName
            pcCapture.Image.Save(Path1, System.Drawing.Imaging.ImageFormat.Jpeg)
            '======= บันทึกรูปภาพแบบปกติ =====

            '======= save convert to 300 dpi =====
            Dim path2 As String = GetCapturePath 'ConfigINI.GetValue("CaptureImage", "Path", "")
            Dim Resolution As String = ConfigINI.GetValue("CaptureImage", "Resolution", "")
            path2 = path2 & MobileNo & ".jpg"
            If File.Exists(path2) Then
                File.Delete(path2)
            End If

            Using bitmap As Bitmap = DirectCast(image.FromFile(Path1), Bitmap)
                Using newBitmap As New Bitmap(bitmap)
                    newBitmap.SetResolution(Resolution, Resolution)
                    newBitmap.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg)
                End Using
            End Using

            If File.Exists(Path1) Then
                File.Delete(Path1)
            End If
            '======= save convert to 300 dpi =====

            Dim bipimag As New MemoryStream(File.ReadAllBytes(path2))
            Dim imag As Image = New Bitmap(bipimag)

            btnDisplayImage.BackgroundImageLayout = ImageLayout.Stretch
            btnDisplayImage.BackgroundImage = imag


        End Sub
    End Class
End Namespace

