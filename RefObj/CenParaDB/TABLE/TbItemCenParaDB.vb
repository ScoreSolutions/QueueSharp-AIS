
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_ITEM table Parameter.
    '[Create by  on August, 15 2014]

    <Table(Name:="TB_ITEM")>  _
    Public Class TbItemCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _ITEM_CODE As  String  = ""
        Dim _ITEM_NAME As  String  = ""
        Dim _ITEM_NAME_TH As  String  = ""
        Dim _ITEM_TIME As  System.Nullable(Of Long)  = 0
        Dim _ITEM_WAIT As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ORDER As  System.Nullable(Of Long)  = 0
        Dim _TXT_QUEUE As  System.Nullable(Of Char)  = ""
        Dim _COLOR As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _Q_TYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _APP_MIN_QUEUE As  String  = ""
        Dim _APP_MAX_QUEUE As  String  = ""
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_ITEM_GROUP_ID As  System.Nullable(Of Long)  = 0
        Dim _BRAND_NAME As  String  = ""

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
        <Column(Storage:="_ITEM_CODE", DbType:="VarChar(20)")>  _
        Public Property ITEM_CODE() As  String 
            Get
                Return _ITEM_CODE
            End Get
            Set(ByVal value As  String )
               _ITEM_CODE = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME", DbType:="VarChar(50)")>  _
        Public Property ITEM_NAME() As  String 
            Get
                Return _ITEM_NAME
            End Get
            Set(ByVal value As  String )
               _ITEM_NAME = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME_TH", DbType:="VarChar(50)")>  _
        Public Property ITEM_NAME_TH() As  String 
            Get
                Return _ITEM_NAME_TH
            End Get
            Set(ByVal value As  String )
               _ITEM_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_TIME", DbType:="Int")>  _
        Public Property ITEM_TIME() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_TIME = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_WAIT", DbType:="Int")>  _
        Public Property ITEM_WAIT() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_WAIT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_WAIT = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ORDER", DbType:="SmallInt")>  _
        Public Property ITEM_ORDER() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ORDER
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ORDER = value
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
        <Column(Storage:="_COLOR", DbType:="Int")>  _
        Public Property COLOR() As  System.Nullable(Of Long) 
            Get
                Return _COLOR
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COLOR = value
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
        <Column(Storage:="_Q_TYPE_ID", DbType:="SmallInt")>  _
        Public Property Q_TYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _Q_TYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _Q_TYPE_ID = value
            End Set
        End Property 
        <Column(Storage:="_APP_MIN_QUEUE", DbType:="VarChar(10)")>  _
        Public Property APP_MIN_QUEUE() As  String 
            Get
                Return _APP_MIN_QUEUE
            End Get
            Set(ByVal value As  String )
               _APP_MIN_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_APP_MAX_QUEUE", DbType:="VarChar(10)")>  _
        Public Property APP_MAX_QUEUE() As  String 
            Get
                Return _APP_MAX_QUEUE
            End Get
            Set(ByVal value As  String )
               _APP_MAX_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50)")>  _
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
        <Column(Storage:="_TB_ITEM_GROUP_ID", DbType:="Int")>  _
        Public Property TB_ITEM_GROUP_ID() As  System.Nullable(Of Long) 
            Get
                Return _TB_ITEM_GROUP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TB_ITEM_GROUP_ID = value
            End Set
        End Property 
        <Column(Storage:="_BRAND_NAME", DbType:="VarChar(100)")>  _
        Public Property BRAND_NAME() As  String 
            Get
                Return _BRAND_NAME
            End Get
            Set(ByVal value As  String )
               _BRAND_NAME = value
            End Set
        End Property 


    End Class
End Namespace
