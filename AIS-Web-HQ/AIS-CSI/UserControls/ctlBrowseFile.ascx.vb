Imports System.IO
Imports CenParaDB.Common

Partial Class UserControls_ctlBrowseFile
    Inherits System.Web.UI.UserControl

    Public Event Upload(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Overrides Property ID() As String
        Get
            Return MyBase.ID
        End Get
        Set(ByVal value As String)
            MyBase.ID = value
        End Set
    End Property

    Public ReadOnly Property TmpFileUploadPara() As TmpFileUploadPara
        Get
            Dim TmpName As String = GetTempFileName()
            If File.Exists(TmpName) = True Then
                Dim FileProp As New FileInfo(TmpName)
                Dim fData As New TmpFileUploadPara
                fData.TmpFileByte = GetTempFileByte()
                fData.FileExtent = FileProp.Extension
                fData.FileName = Split(FileProp.Name, "__")(1)
                FileProp = Nothing
                Return fData
            Else
                Return Nothing
            End If
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

    Public ReadOnly Property HasFile() As Boolean
        Get
            Return File.Exists(GetTempFileName)
        End Get
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
            ClearFile()
            SaveTempFile(fData)
        End If
    End Sub

    Private Sub SaveTempFile(ByVal FileBrowse As AjaxControlToolkit.AsyncFileUpload, ByVal FileExtent As String)
        Dim TmpFileUpload As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempUpload"))
        If Directory.Exists(TmpFileUpload) = False Then
            Directory.CreateDirectory(TmpFileUpload)
        End If

        Dim ClientIP As String = Request.UserHostAddress
        If Directory.Exists(TmpFileUpload & "/" & ClientIP) = False Then
            Directory.CreateDirectory(TmpFileUpload & "/" & ClientIP)
        End If

        Dim PathFile As String = GetTempPath()
        Dim FileName As String = PathFile & "\" & Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "") & "_" & FileBrowse.ClientID & FileExtent
        If File.Exists(FileName) = True Then
            File.Delete(FileName)
        End If
        FileBrowse.SaveAs(FileName)
    End Sub
    Private Sub SaveTempFile(ByVal fData As TmpFileUploadPara)
        Dim TmpFileUpload As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempUpload"))
        If Directory.Exists(TmpFileUpload) = False Then
            Directory.CreateDirectory(TmpFileUpload)
        End If

        Dim ClientIP As String = Request.UserHostAddress
        If Directory.Exists(TmpFileUpload & "\" & ClientIP) = False Then
            Directory.CreateDirectory(TmpFileUpload & "\" & ClientIP)
        End If

        Dim fs As FileStream
        Dim byteData() As Byte
        byteData = fData.TmpFileByte

        Dim PathFile As String = GetTempPath()
        Dim FileName As String = PathFile & "\" & Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "").Replace("/", "_") & "_" & FileBrowse.ClientID & "__" & fData.FileName
        If File.Exists(FileName) = True Then
            File.Delete(FileName)
        End If

        fs = New FileStream(FileName, FileMode.CreateNew)
        fs.Write(byteData, 0, byteData.Length)
        fs.Close()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent Upload(sender, e)
    End Sub

    Public Sub ClearFile()
        Dim TempPath As String = GetTempPath()
        If Directory.Exists(TempPath) = True Then
            For Each f As String In Directory.GetFiles(TempPath)
                Try
                    File.SetAttributes(f, FileAttributes.Normal)
                    File.Delete(f)
                Catch ex As Exception

                End Try
            Next
        End If
        
    End Sub

    Private ReadOnly Property GetTempPath() As String
        Get
            Return Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempUpload")) & "\" & Request.UserHostAddress
        End Get
    End Property

    Private Function GetTempFileByte() As Byte()
        Dim TempName As String = Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "").Replace("/", "_") & "_" & FileBrowse.ClientID & "__"

        Dim f() As Byte
        If Directory.Exists(GetTempPath) = True Then
            For Each fle As String In Directory.GetFiles(GetTempPath, TempName & "*.*")
                If InStr(fle, TempName) > 0 Then
                    f = File.ReadAllBytes(fle)
                    Exit For
                End If
            Next
        End If

        Return f
    End Function

    Private Function GetTempFileName() As String
        Dim TempName As String = Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "").Replace("/", "_") & "_" & FileBrowse.ClientID & "__"

        Dim f As String = ""
        If Directory.Exists(GetTempPath) = True Then
            For Each fle As String In Directory.GetFiles(GetTempPath, TempName & "*.*")
                If InStr(fle, TempName) > 0 Then
                    f = fle
                    Exit For
                End If
            Next
        End If
        Return f
    End Function


    Public Sub SaveFile(ByVal PathFile As String, ByVal fileName As String)
        Dim TempName As String = GetTempFileName()
        If TempName <> "" Then
            'Dim fs As FileStream
            'Dim byteData() As Byte
            'Dim FileProp As New FileInfo(TempName)
            'Dim fData As New TmpFileUploadPara
            'fData.TmpFileByte = FileBrowse.FileBytes
            'fData.FileExtent = FileProp.Extension
            'fData.FileName = FileProp.Name

            'fs = New FileStream(PathFile & "\" & fileName, FileMode.CreateNew)
            'fs.Write(byteData, 0, byteData.Length)
            'fs.Close()

            File.Move(TempName, PathFile & "\" & fileName)
            'ClearFile()
        End If
    End Sub
End Class
