
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_ERROR_LOG table Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="TB_ERROR_LOG")>  _
    Public Class TbErrorLogShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _LOG_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ERROR_MESSAGE As String = ""
        Dim _SQL_COMMAND As  String  = ""
        Dim _CLIENT_IP As  String  = ""
        Dim _VERSION As  String  = ""

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
        <Column(Storage:="_CREATE_BY", DbType:="Int")>  _
        Public Property CREATE_BY() As  System.Nullable(Of Long) 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CREATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime")>  _
        Public Property CREATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CREATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_UPDATE_BY", DbType:="Int")>  _
        Public Property UPDATE_BY() As  System.Nullable(Of Long) 
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
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
        <Column(Storage:="_LOG_DATE", DbType:="DateTime")>  _
        Public Property LOG_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _LOG_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _LOG_DATE = value
            End Set
        End Property 
        <Column(Storage:="_ERROR_MESSAGE", DbType:="Text NOT NULL ",CanBeNull:=false)>  _
        Public Property ERROR_MESSAGE() As String
            Get
                Return _ERROR_MESSAGE
            End Get
            Set(ByVal value As String)
               _ERROR_MESSAGE = value
            End Set
        End Property 
        <Column(Storage:="_SQL_COMMAND", DbType:="Text")>  _
        Public Property SQL_COMMAND() As  String 
            Get
                Return _SQL_COMMAND
            End Get
            Set(ByVal value As  String )
               _SQL_COMMAND = value
            End Set
        End Property 
        <Column(Storage:="_CLIENT_IP", DbType:="VarChar(15)")>  _
        Public Property CLIENT_IP() As  String 
            Get
                Return _CLIENT_IP
            End Get
            Set(ByVal value As  String )
               _CLIENT_IP = value
            End Set
        End Property 
        <Column(Storage:="_VERSION", DbType:="VarChar(20)")>  _
        Public Property VERSION() As  String 
            Get
                Return _VERSION
            End Get
            Set(ByVal value As  String )
               _VERSION = value
            End Set
        End Property 
        


    End Class
End Namespace
