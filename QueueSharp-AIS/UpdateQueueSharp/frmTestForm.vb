Imports DirectX.Capture
Imports System.Collections.Specialized

Public Class frmTestForm

    Public Structure Active
        Dim Camera As Filter
        Dim CaptureInfo As Capture
        Dim Counter As Integer
        Dim CounterFrames As Integer
        Dim PathVideo As String
    End Structure

    Public AgentCapture As New Active
    Public Dispositive As New Filters
    Dim FlagCam As Boolean = False
    Dim fName As New ArrayList

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try

            If FlagCam = True Then
                AgentCapture.CaptureInfo.Stop()
                AgentCapture.CaptureInfo.DisposeCapture()
            Else
                FlagCam = True
            End If
            Try
                AgentCapture.Camera = Dispositive.VideoInputDevices(1)
                AgentCapture.CaptureInfo = New Capture(AgentCapture.Camera, Nothing)
                AgentCapture.CaptureInfo.PreviewWindow = Me.PictureBox1
                fName.Add(AgentCapture.CaptureInfo.Filename)
                AgentCapture.CaptureInfo.Start()



            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmTestForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            AgentCapture.CaptureInfo.Stop()
            For Each fFile As String In fName
                System.IO.File.Delete(fFile)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCapture.Click
        'AgentCapture.CaptureInfo.DrawToBitmap()
        'PictureBox1.Image.Save("D:\TestPict.png", Imaging.ImageFormat.Png)

        Dim bm As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        AgentCapture.CaptureInfo.DrawToBitmap(bm, New Rectangle(PictureBox1.Left, PictureBox1.Top, PictureBox1.Width, PictureBox1.Height))
        'bm.Save("D:\TestPict.png", Imaging.ImageFormat.Png)
        PictureBox1.Image = bm
        PictureBox1.Image.Save("D:\TestPict.png", Imaging.ImageFormat.Png)
    End Sub

    'Public Sub RefreshImage(ByVal Frame As System.Windows.Forms.PictureBox)
    '    Dim s() As String
    '    s = AgentCapture.PathVideo.Split(".")
    '    Me.PictureBox1.Image = Frame.Image
    '    'Me.PictureBox1.Image.Save(s(0) + CStr(AgentCapture.CounterFrames) + ".png", Imaging.ImageFormat.Png)
    '    AgentCapture.CounterFrames += 1
    '    Me.PictureBox1.Refresh()
    'End Sub

    Private Sub frmTestForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler AgentCapture.CaptureInfo.FrameCaptureComplete, AddressOf RefreshImage

        'AgentCapture.Counter = 1
        'AgentCapture.CounterFrames = 1
    End Sub
End Class