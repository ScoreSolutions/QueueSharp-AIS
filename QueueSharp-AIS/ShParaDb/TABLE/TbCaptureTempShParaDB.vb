
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CAPTURE_TEMP table Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="TB_CAPTURE_TEMP")>  _
    Public Class TbCaptureTempShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _QUEUE_NO As  String  = ""
        Dim _QUEUE_ID As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE As  System.Nullable(Of Long)  = 0
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
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(20)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_ID", DbType:="Int")>  _
        Public Property QUEUE_ID() As  System.Nullable(Of Long) 
            Get
                Return _QUEUE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _QUEUE_ID = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE", DbType:="SmallInt")>  _
        Public Property ACTIVE() As  System.Nullable(Of Long) 
            Get
                Return _ACTIVE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ACTIVE = value
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
