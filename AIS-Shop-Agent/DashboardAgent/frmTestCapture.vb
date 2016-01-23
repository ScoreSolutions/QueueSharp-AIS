Imports System.Runtime.InteropServices

Public Class frmTestCapture
    Private Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer
    Dim bm As Bitmap
    Private Sub btnCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCapture.Click
        Dim gr As Graphics
        bm = New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, Drawing.Imaging.PixelFormat.Format32bppArgb)
        gr = Graphics.FromImage(bm)
        gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))

        bm.Save(Application.StartupPath & "\TestCapture.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        bm.Dispose()

        SystemParametersInfo(20, 0&, Application.StartupPath & "\TestCapture.jpg", &H2 Or &H1)
    End Sub

    'Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal IpClassName As String, ByVal IpWindowName As String) As IntPtr
    Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As IntPtr
    Private Const SW_HIDE As Integer = 0
    Private Const SW_RESTORE As Integer = 9

    Private Sub ShowDesktopIcon()
        Dim hwnd As IntPtr
        hwnd = FindWindow(vbNullString, "Program Manager")
        If hwnd <> 0 Then
            ShowWindow(hwnd, SW_RESTORE)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'HideDesktopIcon()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ShowDesktopIcon()
    End Sub


    <DllImport("user32.dll", EntryPoint:="FindWindow", SetLastError:=True)> _
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function
    

    

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        
        'System.Threading.Thread.Sleep(2000)
        'SendMessage(lHwnd, WM_COMMAND, MIN_ALL_UNDO, IntPtr.Zero)
    End Sub
End Class