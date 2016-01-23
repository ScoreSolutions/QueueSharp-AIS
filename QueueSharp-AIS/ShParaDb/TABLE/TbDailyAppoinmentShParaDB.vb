
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_DAILY_APPOINMENT table Parameter.
    '[Create by  on January, 13 2012]

    <Table(Name:="TB_DAILY_APPOINMENT")>  _
    Public Class TbDailyAppoinmentShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _SERVICE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _QUEUE_NO As  String  = ""
        Dim _CUSTOMER_ID As  String  = ""
        Dim _SERVICE_TIME As  System.Nullable(Of Long)  = 0
        Dim _MSREPL_TRAN_VERSION As String = ""

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
        <Column(Storage:="_SERVICE_TIME", DbType:="Int")>  _
        Public Property SERVICE_TIME() As  System.Nullable(Of Long) 
            Get
                Return _SERVICE_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SERVICE_TIME = value
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


    End Class
End Namespace
