Imports System.IO
Imports CenParaDB.Common

Partial Class UserControls_ctlBrowseFile
    Inherits System.Web.UI.UserControl

    Public Event Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AutoUpload(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

    Public ReadOnly Property TmpFileUploadPara() As TmpFileUploadPara
        Get
            Return Session("TmpFileData")
        End Get
    End Property

    Public ReadOnly Property HasFile() As Boolean
        Get
            Return (Session("TmpFileData") IsNot Nothing)
        End Get
    End Property

    Public Property Width() As Unit
        Get
            Return FileBrowse.Width
        End Get
        Set(ByVal value As Unit)
            FileBrowse.Width = value
        End Set
    End Property

    Public Property VisibleUploadButton() As Boolean
        Get
            Return Button1.Visible
        End Get
        Set(ByVal value As Boolean)
            Button1.Visible = value
        End Set
    End Property

    Protected Sub FileBrowse_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles FileBrowse.UploadedComplete
        If e.state = AjaxControlToolkit.AsyncFileUploadState.Success Then
            Dim FileProp As New FileInfo(FileBrowse.FileName)
            Dim fData As New TmpFileUploadPara
            fData.TmpFileByte = FileBrowse.FileBytes
            fData.FileExtent = FileProp.Extension
            fData.FileName = FileProp.Name
            Session("TmpFileData") = fData
            RaiseEvent AutoUpload(sender, e)
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent Upload_Click(sender, e)
    End Sub

    'Public Sub SaveFile(ByVal fileName As String)
    '    If Session("TmpFileData") IsNot Nothing Then
    '        Dim fs As FileStream
    '        Dim byteData() As Byte
    '        Dim fData As New TmpFileUploadPara
    '        fData = Session("TmpFileData")
    '        byteData = fData.TmpFileByte

    '        If File.Exists(Config.GetUploadPath & fileName) = True Then
    '            File.Delete(Config.GetUploadPath & fileName)
    '        End If

    '        fs = New FileStream(Config.GetUploadPath & fileName, FileMode.CreateNew)
    '        fs.Write(byteData, 0, byteData.Length)
    '        fs.Close()
    '        Session.Contents.Remove("TmpFileData")
    '    End If
    'End Sub

    'Public Function GetFile(ByVal fileName As String) As TmpFileUploadPara
    '    Dim fData As New TmpFileUploadPara
    '    If File.Exists(Config.GetUploadPath & fileName) = True Then
    '        Dim fs As New FileInfo(Config.GetUploadPath & fileName)
    '        fData.TmpFileByte = File.ReadAllBytes(Config.GetUploadPath & fileName)
    '        fData.FileExtent = fs.Extension
    '        Session("TmpFileData") = fData
    '    End If

    '    Return fData
    'End Function

    'Public Function GetFileInSession() As TmpFileUploadPara
    '    Dim fData As New TmpFileUploadPara
    '    If Session("TmpFileData") IsNot Nothing Then
    '        fData = Session("TmpFileData")
    '    End If
    '    Return fData
    'End Function


    Public Sub ClearFile()
        Session.Contents.Remove("TmpFileData")
    End Sub
End Class
