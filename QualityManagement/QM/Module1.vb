Imports System.Runtime.InteropServices
Module Module1

    Private Const WM_CLOSE = &H10
    Const VK_F As Integer = &H46
    Const VK_CTRL As Integer = &H11
    Const VK_ALT As Integer = &H12
    Const VK_PAUSE As Integer = &H13
    Const WM_SYSKEYDOWN As Integer = &H100
    Const WM_SYSKEYUP As Integer = &H101
    <DllImport("user32.dll")> _
    Public Function FindWindow(ByVal sClassName As String, ByVal sAppName As String) As IntPtr
    End Function
    <DllImport("User32.Dll", EntryPoint:="PostMessageA")> _
    Private Function PostMessage(ByVal hWnd As IntPtr, ByVal msg As UInteger, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
    End Function
    Sub CloseQM()
        Dim hWindow As Long = FindWindow(vbNullString, "QM")
        '"Controller" is the caption of the application running
        PostMessage(hWindow, WM_SYSKEYDOWN, VK_PAUSE, 0)
        PostMessage(hWindow, WM_SYSKEYUP, VK_PAUSE, 0)
    End Sub

    Partial Class QmTransferStatus
        Public Const FileIsMerging As String = "1"
        Public Const FileIsAlreadyMerged As String = "2"
        Public Const FileIsTransferringFromTempFolder As String = "3"
        Public Const TransferComplete As String = "4"
        Public Const FileMovingToBackupFolder As String = "5"
        Public Const FileIsTransferringFromBackupFolder As String = "6"
        Public Const FileNotFound As String = "7"
    End Class
End Module
