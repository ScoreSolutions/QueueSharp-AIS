
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_LOG_CUSTOMER_DAILY table Parameter.
    '[Create by  on April, 12 2012]

    <Table(Name:="TB_LOG_CUSTOMER_DAILY")>  _
    Public Class TbLogCustomerDailyCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _FILE_SIZE As  System.Nullable(Of Long)  = 0
        Dim _FILE_NAME As  String  = ""
        Dim _FILE_TYPE As  System.Nullable(Of Char)  = ""
        Dim _START_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _IMPORT_MSG As  String  = ""
        Dim _ROW_COUNT As  System.Nullable(Of Long)  = 0
        Dim _ROW_COUNT_INSERT As  System.Nullable(Of Long)  = 0
        Dim _ROW_COUNT_UPDATE As  System.Nullable(Of Long)  = 0

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(20)")>  _
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
        <Column(Storage:="_FILE_SIZE", DbType:="BigInt")>  _
        Public Property FILE_SIZE() As  System.Nullable(Of Long) 
            Get
                Return _FILE_SIZE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _FILE_SIZE = value
            End Set
        End Property 
        <Column(Storage:="_FILE_NAME", DbType:="VarChar(255)")>  _
        Public Property FILE_NAME() As  String 
            Get
                Return _FILE_NAME
            End Get
            Set(ByVal value As  String )
               _FILE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_FILE_TYPE", DbType:="Char(1)")>  _
        Public Property FILE_TYPE() As  System.Nullable(Of Char) 
            Get
                Return _FILE_TYPE
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _FILE_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_START_TIME", DbType:="DateTime")>  _
        Public Property START_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _START_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _START_TIME = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME", DbType:="DateTime")>  _
        Public Property END_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _END_TIME = value
            End Set
        End Property 
        <Column(Storage:="_IMPORT_MSG", DbType:="VarChar(500)")>  _
        Public Property IMPORT_MSG() As  String 
            Get
                Return _IMPORT_MSG
            End Get
            Set(ByVal value As  String )
               _IMPORT_MSG = value
            End Set
        End Property 
        <Column(Storage:="_ROW_COUNT", DbType:="BigInt")>  _
        Public Property ROW_COUNT() As  System.Nullable(Of Long) 
            Get
                Return _ROW_COUNT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ROW_COUNT = value
            End Set
        End Property 
        <Column(Storage:="_ROW_COUNT_INSERT", DbType:="BigInt")>  _
        Public Property ROW_COUNT_INSERT() As  System.Nullable(Of Long) 
            Get
                Return _ROW_COUNT_INSERT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ROW_COUNT_INSERT = value
            End Set
        End Property 
        <Column(Storage:="_ROW_COUNT_UPDATE", DbType:="BigInt")>  _
        Public Property ROW_COUNT_UPDATE() As  System.Nullable(Of Long) 
            Get
                Return _ROW_COUNT_UPDATE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ROW_COUNT_UPDATE = value
            End Set
        End Property 


    End Class
End Namespace
