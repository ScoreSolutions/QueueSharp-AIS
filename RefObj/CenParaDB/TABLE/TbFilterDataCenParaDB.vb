
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_FILTER_DATA table Parameter.
    '[Create by  on October, 28 2015]

    <Table(Name:="TB_FILTER_DATA")>  _
    Public Class TbFilterDataCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _QUEUE_NO As String = ""
        Dim _QUEUE_UNIQUE_ID As String = ""
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _USERNAME As String = ""
        Dim _TB_FILTER_ID As Long = 0
        Dim _TB_ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _TEMPLATE_CODE As String = ""
        Dim _FILTER_TIME As DateTime = New DateTime(1,1,1)
        Dim _SEND_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _RESULT_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _RESULT_STATUS As Char = "0"
        Dim _RESULT As  String  = ""
        Dim _SEND_TO_SHOP_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ATSR_CALL_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _NETWORK_TYPE As String = "2G"
        Dim _NPS_SCORE As Long = 0
        Dim _ACTIVITY_ID_SURVEY As  String  = ""
        Dim _ACTIVITY_ID_LEAVE_VOICE As  String  = ""
        Dim _STAFF_NAME As  String  = ""
        Dim _SEGMENT As  String  = ""

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
               _CREATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
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
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property QUEUE_NO() As String
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As String)
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_UNIQUE_ID", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property QUEUE_UNIQUE_ID() As String
            Get
                Return _QUEUE_UNIQUE_ID
            End Get
            Set(ByVal value As String)
               _QUEUE_UNIQUE_ID = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_NAME", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_NAME() As  String 
            Get
                Return _CUSTOMER_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_TB_FILTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_FILTER_ID() As Long
            Get
                Return _TB_FILTER_ID
            End Get
            Set(ByVal value As Long)
               _TB_FILTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_TB_ITEM_ID", DbType:="Int")>  _
        Public Property TB_ITEM_ID() As  System.Nullable(Of Long) 
            Get
                Return _TB_ITEM_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TB_ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_TEMPLATE_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TEMPLATE_CODE() As String
            Get
                Return _TEMPLATE_CODE
            End Get
            Set(ByVal value As String)
               _TEMPLATE_CODE = value
            End Set
        End Property 
        <Column(Storage:="_FILTER_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property FILTER_TIME() As DateTime
            Get
                Return _FILTER_TIME
            End Get
            Set(ByVal value As DateTime)
               _FILTER_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SEND_TIME", DbType:="DateTime")>  _
        Public Property SEND_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _SEND_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SEND_TIME = value
            End Set
        End Property 
        <Column(Storage:="_RESULT_TIME", DbType:="DateTime")>  _
        Public Property RESULT_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _RESULT_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _RESULT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_RESULT_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property RESULT_STATUS() As Char
            Get
                Return _RESULT_STATUS
            End Get
            Set(ByVal value As Char)
               _RESULT_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_RESULT", DbType:="VarChar(255)")>  _
        Public Property RESULT() As  String 
            Get
                Return _RESULT
            End Get
            Set(ByVal value As  String )
               _RESULT = value
            End Set
        End Property 
        <Column(Storage:="_SEND_TO_SHOP_TIME", DbType:="DateTime")>  _
        Public Property SEND_TO_SHOP_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _SEND_TO_SHOP_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SEND_TO_SHOP_TIME = value
            End Set
        End Property 
        <Column(Storage:="_ATSR_CALL_TIME", DbType:="DateTime")>  _
        Public Property ATSR_CALL_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ATSR_CALL_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ATSR_CALL_TIME = value
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
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NETWORK_TYPE() As String
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As String)
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_NPS_SCORE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NPS_SCORE() As Long
            Get
                Return _NPS_SCORE
            End Get
            Set(ByVal value As Long)
               _NPS_SCORE = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVITY_ID_SURVEY", DbType:="VarChar(50)")>  _
        Public Property ACTIVITY_ID_SURVEY() As  String 
            Get
                Return _ACTIVITY_ID_SURVEY
            End Get
            Set(ByVal value As  String )
               _ACTIVITY_ID_SURVEY = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVITY_ID_LEAVE_VOICE", DbType:="VarChar(50)")>  _
        Public Property ACTIVITY_ID_LEAVE_VOICE() As  String 
            Get
                Return _ACTIVITY_ID_LEAVE_VOICE
            End Get
            Set(ByVal value As  String )
               _ACTIVITY_ID_LEAVE_VOICE = value
            End Set
        End Property 
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255)")>  _
        Public Property STAFF_NAME() As  String 
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As  String )
               _STAFF_NAME = value
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


    End Class
End Namespace
