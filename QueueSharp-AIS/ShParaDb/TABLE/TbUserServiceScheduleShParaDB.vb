
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_USER_SERVICE_SCHEDULE table Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="TB_USER_SERVICE_SCHEDULE")>  _
    Public Class TbUserServiceScheduleShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _SERVICE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _USER_ID As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _PRIORITY As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _MSREPL_TRAN_VERSION As String = ""
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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime")>  _
        Public Property SERVICE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_USER_ID", DbType:="Int")>  _
        Public Property USER_ID() As  System.Nullable(Of Long) 
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _USER_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ID", DbType:="Int")>  _
        Public Property ITEM_ID() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_PRIORITY", DbType:="SmallInt")>  _
        Public Property PRIORITY() As  System.Nullable(Of Long) 
            Get
                Return _PRIORITY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _PRIORITY = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE_STATUS", DbType:="SmallInt")>  _
        Public Property ACTIVE_STATUS() As  System.Nullable(Of Long) 
            Get
                Return _ACTIVE_STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ACTIVE_STATUS = value
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
        <Column(Storage:="_MSREPL_TRAN_VERSION", DbType:="UNIQUEIDENTIFIER NOT NULL ",CanBeNull:=false)>  _
        Public Property MSREPL_TRAN_VERSION() As String
            Get
                Return _MSREPL_TRAN_VERSION
            End Get
            Set(ByVal value As String)
               _MSREPL_TRAN_VERSION = value
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
