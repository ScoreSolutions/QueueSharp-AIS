
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_SHOP_FILE_BYTE table Parameter.
    '[Create by  on July, 4 2013]

    <Table(Name:="TB_SHOP_FILE_BYTE")>  _
    Public Class TbShopFileByteCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _FOLDER_NAME As String = ""
        Dim _FILE_NAME As String = ""
        Dim _TARGET_TYPE As Char = ""
        Dim _FILE_BYTE() As Byte
        Dim _CONVERT_STATUS As Char = "N"

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
               _CREATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
               _CREATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(50)")>  _
        Public Property UPDATE_BY() As  String 
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As  String )
               _UPDATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime")>  _
        Public Property UPDATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _UPDATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_FOLDER_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property FOLDER_NAME() As String
            Get
                Return _FOLDER_NAME
            End Get
            Set(ByVal value As String)
               _FOLDER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_FILE_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property FILE_NAME() As String
            Get
                Return _FILE_NAME
            End Get
            Set(ByVal value As String)
               _FILE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_TARGET_TYPE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property TARGET_TYPE() As Char
            Get
                Return _TARGET_TYPE
            End Get
            Set(ByVal value As Char)
               _TARGET_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_FILE_BYTE", DbType:="IMAGE")>  _
        Public Property FILE_BYTE() As  Byte() 
            Get
                Return _FILE_BYTE
            End Get
            Set(ByVal value() As Byte)
               _FILE_BYTE = value
            End Set
        End Property 
        <Column(Storage:="_CONVERT_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property CONVERT_STATUS() As Char
            Get
                Return _CONVERT_STATUS
            End Get
            Set(ByVal value As Char)
               _CONVERT_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
