
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_Agent view Parameter.
    '[Create by  on March, 5 2012]

    <Table(Name:="VW_Agent")>  _
    Public Class VwAgentShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _QUEUE_NO As  String  = ""
        Dim _CUSTOMER_ID As  String  = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _SEGMENT As  String  = ""
        Dim _CUSTOMERTYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _COUNTER_ID As  System.Nullable(Of Long)  = 0
        Dim _USER_ID As  System.Nullable(Of Long)  = 0
        Dim _SERVICE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ASSIGN_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CALL_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _START_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _STATUS As  System.Nullable(Of Long)  = 0
        Dim _LOG_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

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
        <Column(Storage:="_CUSTOMER_NAME", DbType:="VarChar(250)")>  _
        Public Property CUSTOMER_NAME() As  String 
            Get
                Return _CUSTOMER_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SEGMENT", DbType:="VarChar(100)")>  _
        Public Property SEGMENT() As  String 
            Get
                Return _SEGMENT
            End Get
            Set(ByVal value As  String )
               _SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_ID", DbType:="Int")>  _
        Public Property CUSTOMERTYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _CUSTOMERTYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CUSTOMERTYPE_ID = value
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
        <Column(Storage:="_COUNTER_ID", DbType:="Int")>  _
        Public Property COUNTER_ID() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_ID = value
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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime")>  _
        Public Property SERVICE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_ASSIGN_TIME", DbType:="DateTime")>  _
        Public Property ASSIGN_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ASSIGN_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ASSIGN_TIME = value
            End Set
        End Property 
        <Column(Storage:="_CALL_TIME", DbType:="DateTime")>  _
        Public Property CALL_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _CALL_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CALL_TIME = value
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
        <Column(Storage:="_STATUS", DbType:="SmallInt")>  _
        Public Property STATUS() As  System.Nullable(Of Long) 
            Get
                Return _STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _STATUS = value
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


    End Class
End Namespace
