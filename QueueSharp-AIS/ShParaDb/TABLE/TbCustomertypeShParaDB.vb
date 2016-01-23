
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CUSTOMERTYPE table Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="TB_CUSTOMERTYPE")>  _
    Public Class TbCustomertypeShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CUSTOMERTYPE_CODE As  String  = ""
        Dim _CUSTOMERTYPE_NAME As  String  = ""
        Dim _TXT_QUEUE As  System.Nullable(Of Char)  = ""
        Dim _MIN_QUEUE As  String  = ""
        Dim _MAX_QUEUE As  String  = ""
        Dim _PRIORITY_VALUE As  System.Nullable(Of Long)  = 0
        Dim _COLOR As  System.Nullable(Of Long)  = 0
        Dim _APP As  System.Nullable(Of Long)  = 0
        Dim _DEF As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
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
        <Column(Storage:="_CUSTOMERTYPE_CODE", DbType:="VarChar(20)")>  _
        Public Property CUSTOMERTYPE_CODE() As  String 
            Get
                Return _CUSTOMERTYPE_CODE
            End Get
            Set(ByVal value As  String )
               _CUSTOMERTYPE_CODE = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_NAME", DbType:="VarChar(100)")>  _
        Public Property CUSTOMERTYPE_NAME() As  String 
            Get
                Return _CUSTOMERTYPE_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMERTYPE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_TXT_QUEUE", DbType:="Char(1)")>  _
        Public Property TXT_QUEUE() As  System.Nullable(Of Char) 
            Get
                Return _TXT_QUEUE
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _TXT_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_MIN_QUEUE", DbType:="VarChar(10)")>  _
        Public Property MIN_QUEUE() As  String 
            Get
                Return _MIN_QUEUE
            End Get
            Set(ByVal value As  String )
               _MIN_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_MAX_QUEUE", DbType:="VarChar(10)")>  _
        Public Property MAX_QUEUE() As  String 
            Get
                Return _MAX_QUEUE
            End Get
            Set(ByVal value As  String )
               _MAX_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_PRIORITY_VALUE", DbType:="SmallInt")>  _
        Public Property PRIORITY_VALUE() As  System.Nullable(Of Long) 
            Get
                Return _PRIORITY_VALUE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _PRIORITY_VALUE = value
            End Set
        End Property 
        <Column(Storage:="_COLOR", DbType:="Int")>  _
        Public Property COLOR() As  System.Nullable(Of Long) 
            Get
                Return _COLOR
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COLOR = value
            End Set
        End Property 
        <Column(Storage:="_APP", DbType:="SmallInt")>  _
        Public Property APP() As  System.Nullable(Of Long) 
            Get
                Return _APP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _APP = value
            End Set
        End Property 
        <Column(Storage:="_DEF", DbType:="SmallInt")>  _
        Public Property DEF() As  System.Nullable(Of Long) 
            Get
                Return _DEF
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _DEF = value
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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20)")>  _
        Public Property CREATE_BY() As  String 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  String )
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
