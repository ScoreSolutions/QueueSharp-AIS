
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_Q_BEFORE_AFTER view Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="VW_Q_BEFORE_AFTER")>  _
    Public Class VwQBeforeAfterShParaDB

        'Generate Field List
        Dim _QUEUE_NO As  String  = ""
        Dim _CUSTOMER_ID As  String  = ""
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _ITEM_BEFORE As  System.Nullable(Of Long)  = 0
        Dim _ITEM_BEFORE_STATUS As Long = 0

        'Generate Field Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(20)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_ID", DbType:="VarChar(20)")>  _
        Public Property CUSTOMER_ID() As  String 
            Get
                Return _CUSTOMER_ID
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_ID = value
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
        <Column(Storage:="_ITEM_BEFORE", DbType:="Int")>  _
        Public Property ITEM_BEFORE() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_BEFORE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_BEFORE = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_BEFORE_STATUS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_BEFORE_STATUS() As Long
            Get
                Return _ITEM_BEFORE_STATUS
            End Get
            Set(ByVal value As Long)
               _ITEM_BEFORE_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
