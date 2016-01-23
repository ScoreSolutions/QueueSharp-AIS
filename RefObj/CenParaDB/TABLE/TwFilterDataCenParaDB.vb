
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TW_FILTER_DATA table Parameter.
    '[Create by  on April, 27 2015]

    <Table(Name:="TW_FILTER_DATA")>  _
    Public Class TwFilterDataCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TW_LOCATION_ID As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _ORDER_NO As  String  = ""
        Dim _ORDER_TYPE_NAME As String = ""
        Dim _ORDER_UNIQUE_ID As String = ""
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _CUSTOMER_SEGMENT As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _USERNAME As  String  = ""
        Dim _TW_FILTER_ID As Long = 0
        Dim _TW_SFF_ORDER_TYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _TEMPLATE_CODE As String = ""
        Dim _END_TIME As DateTime = New DateTime(1,1,1)
        Dim _FILTER_TIME As DateTime = New DateTime(1,1,1)
        Dim _SEND_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ATSR_CALL_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _RESULT_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _RESULT_STATUS As Char = "0"
        Dim _RESULT As  String  = ""
        Dim _NPS_SCORE As Long = 0
        Dim _ACTIVITY_ID_SURVEY As  String  = ""
        Dim _ACTIVITY_ID_LEAVE_VOICE As  String  = ""
        Dim _TW_SOURCE_DATE_ID As Long = 0
        Dim _TW_SOURCE_DATA_ID As Long = 0

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
        <Column(Storage:="_TW_LOCATION_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_LOCATION_ID() As Long
            Get
                Return _TW_LOCATION_ID
            End Get
            Set(ByVal value As Long)
               _TW_LOCATION_ID = value
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
        <Column(Storage:="_ORDER_NO", DbType:="VarChar(50)")>  _
        Public Property ORDER_NO() As  String 
            Get
                Return _ORDER_NO
            End Get
            Set(ByVal value As  String )
               _ORDER_NO = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_TYPE_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_TYPE_NAME() As String
            Get
                Return _ORDER_TYPE_NAME
            End Get
            Set(ByVal value As String)
               _ORDER_TYPE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_UNIQUE_ID", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_UNIQUE_ID() As String
            Get
                Return _ORDER_UNIQUE_ID
            End Get
            Set(ByVal value As String)
               _ORDER_UNIQUE_ID = value
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
        <Column(Storage:="_CUSTOMER_SEGMENT", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_SEGMENT() As  String 
            Get
                Return _CUSTOMER_SEGMENT
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(100)")>  _
        Public Property NETWORK_TYPE() As  String 
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As  String )
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(50)")>  _
        Public Property USERNAME() As  String 
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As  String )
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_TW_FILTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_FILTER_ID() As Long
            Get
                Return _TW_FILTER_ID
            End Get
            Set(ByVal value As Long)
               _TW_FILTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_TW_SFF_ORDER_TYPE_ID", DbType:="Int")>  _
        Public Property TW_SFF_ORDER_TYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _TW_SFF_ORDER_TYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TW_SFF_ORDER_TYPE_ID = value
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
        <Column(Storage:="_END_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property END_TIME() As DateTime
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As DateTime)
               _END_TIME = value
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
        <Column(Storage:="_ATSR_CALL_TIME", DbType:="DateTime")>  _
        Public Property ATSR_CALL_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ATSR_CALL_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ATSR_CALL_TIME = value
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
        <Column(Storage:="_TW_SOURCE_DATE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_SOURCE_DATE_ID() As Long
            Get
                Return _TW_SOURCE_DATE_ID
            End Get
            Set(ByVal value As Long)
               _TW_SOURCE_DATE_ID = value
            End Set
        End Property 
        <Column(Storage:="_TW_SOURCE_DATA_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_SOURCE_DATA_ID() As Long
            Get
                Return _TW_SOURCE_DATA_ID
            End Get
            Set(ByVal value As Long)
               _TW_SOURCE_DATA_ID = value
            End Set
        End Property 


    End Class
End Namespace
