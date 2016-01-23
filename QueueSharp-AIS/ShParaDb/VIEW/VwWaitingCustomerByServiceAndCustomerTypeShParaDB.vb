
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_Waiting_Customer_by_service_and_customer_type view Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="VW_Waiting_Customer_by_service_and_customer_type")>  _
    Public Class VwWaitingCustomerByServiceAndCustomerTypeShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _ITEM_NAME As  String  = ""
        Dim _ITEM_NAME_TH As  String  = ""
        Dim _ITEM_ORDER As  System.Nullable(Of Long)  = 0
        Dim _ITEM_TIME As  System.Nullable(Of Long)  = 0
        Dim _ITEM_WAIT As  System.Nullable(Of Long)  = 0
        Dim _COUNT_QUEUE As Long = 0
        Dim _APP_QUEUE As Long = 0
        Dim _COUNT_AGENT As Long = 0
        Dim _WAIT_TIME As  String  = ""
        Dim _TIME As  System.Nullable(Of Long)  = 0

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
        <Column(Storage:="_ITEM_ORDER", DbType:="SmallInt")>  _
        Public Property ITEM_ORDER() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ORDER
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ORDER = value
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
        <Column(Storage:="_COUNT_QUEUE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_QUEUE() As Long
            Get
                Return _COUNT_QUEUE
            End Get
            Set(ByVal value As Long)
               _COUNT_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_APP_QUEUE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property APP_QUEUE() As Long
            Get
                Return _APP_QUEUE
            End Get
            Set(ByVal value As Long)
               _APP_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_AGENT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_AGENT() As Long
            Get
                Return _COUNT_AGENT
            End Get
            Set(ByVal value As Long)
               _COUNT_AGENT = value
            End Set
        End Property 
        <Column(Storage:="_WAIT_TIME", DbType:="VarChar(5)")>  _
        Public Property WAIT_TIME() As  String 
            Get
                Return _WAIT_TIME
            End Get
            Set(ByVal value As  String )
               _WAIT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_TIME", DbType:="Int")>  _
        Public Property TIME() As  System.Nullable(Of Long) 
            Get
                Return _TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TIME = value
            End Set
        End Property 


    End Class
End Namespace
