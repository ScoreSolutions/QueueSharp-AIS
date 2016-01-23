Imports System.IO

Namespace Common
    Public Class TmpFileUploadPara
        Dim _TmpFileByte() As Byte
        Dim _FileExtent As String = ""
        Dim _FileName As String = ""

        Public Property TmpFileByte() As Byte()
            Get
                Return _TmpFileByte
            End Get
            Set(ByVal value() As Byte)
                _TmpFileByte = value
            End Set
        End Property
        Public Property FileExtent() As String
            Get
                Return _FileExtent
            End Get
            Set(ByVal value As String)
                _FileExtent = value
            End Set
        End Property
        Public Property FileName() As String
            Get
                Return _FileName.Trim
            End Get
            Set(ByVal value As String)
                _FileName = value
            End Set
        End Property
    End Class
End Namespace

