
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_UNITDISPLAY table Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="TB_UNITDISPLAY")>  _
    Public Class TbUnitdisplayShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _COUNTER_NO As  String  = ""
        Dim _QUEUE_NO As  String  = ""
        Dim _TXT As  String  = ""
        Dim _ACTION As  System.Nullable(Of Long)  = 0
        Dim _STATUS_CLS As  System.Nullable(Of Long)  = 0
        Dim _ROWGUID As String = ""

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
        <Column(Storage:="_COUNTER_NO", DbType:="VarChar(100)")>  _
        Public Property COUNTER_NO() As  String 
            Get
                Return _COUNTER_NO
            End Get
            Set(ByVal value As  String )
               _COUNTER_NO = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(100)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_TXT", DbType:="VarChar(100)")>  _
        Public Property TXT() As  String 
            Get
                Return _TXT
            End Get
            Set(ByVal value As  String )
               _TXT = value
            End Set
        End Property 
        <Column(Storage:="_ACTION", DbType:="Int")>  _
        Public Property ACTION() As  System.Nullable(Of Long) 
            Get
                Return _ACTION
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ACTION = value
            End Set
        End Property 
        <Column(Storage:="_STATUS_CLS", DbType:="Int")>  _
        Public Property STATUS_CLS() As  System.Nullable(Of Long) 
            Get
                Return _STATUS_CLS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _STATUS_CLS = value
            End Set
        End Property 
        <Column(Storage:="_ROWGUID", DbType:="UNIQUEIDENTIFIER NOT NULL ",CanBeNull:=false)>  _
        Public Property ROWGUID() As String
            Get
                Return _ROWGUID
            End Get
            Set(ByVal value As String)
               _ROWGUID = value
            End Set
        End Property 


    End Class
End Namespace
