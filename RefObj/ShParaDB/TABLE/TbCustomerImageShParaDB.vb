
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CUSTOMER_IMAGE table Parameter.
    '[Create by  on September, 16 2013]

    <Table(Name:="TB_CUSTOMER_IMAGE")>  _
    Public Class TbCustomerImageShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CAPTURE_TIME As DateTime = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _ASSIGN_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _QUEUE_NO As  String  = ""
        Dim _CONTACTID As  String  = ""
        Dim _CUSTOMER_FIRST_NAME As  String  = ""
        Dim _CUSTOMER_LAST_NAME As  String  = ""
        Dim _CUSTOMER_STATUS As  String  = ""
        Dim _LAST_CAPTURE_DATE As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _ACCOUNT_NO As  String  = ""
        Dim _WEBSERVICE_HOST_IP As String = ""
        Dim _APPOINTMENT_JOB_ID As Long = 0

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
        <Column(Storage:="_CAPTURE_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property CAPTURE_TIME() As DateTime
            Get
                Return _CAPTURE_TIME
            End Get
            Set(ByVal value As DateTime)
               _CAPTURE_TIME = value
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
        <Column(Storage:="_ASSIGN_TIME", DbType:="DateTime")>  _
        Public Property ASSIGN_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ASSIGN_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ASSIGN_TIME = value
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
        <Column(Storage:="_CONTACTID", DbType:="VarChar(100)")>  _
        Public Property CONTACTID() As  String 
            Get
                Return _CONTACTID
            End Get
            Set(ByVal value As  String )
               _CONTACTID = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_FIRST_NAME", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_FIRST_NAME() As  String 
            Get
                Return _CUSTOMER_FIRST_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_FIRST_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_LAST_NAME", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_LAST_NAME() As  String 
            Get
                Return _CUSTOMER_LAST_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_LAST_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_STATUS", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_STATUS() As  String 
            Get
                Return _CUSTOMER_STATUS
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_LAST_CAPTURE_DATE", DbType:="VarChar(50)")>  _
        Public Property LAST_CAPTURE_DATE() As  String 
            Get
                Return _LAST_CAPTURE_DATE
            End Get
            Set(ByVal value As  String )
               _LAST_CAPTURE_DATE = value
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
        <Column(Storage:="_ACCOUNT_NO", DbType:="VarChar(100)")>  _
        Public Property ACCOUNT_NO() As  String 
            Get
                Return _ACCOUNT_NO
            End Get
            Set(ByVal value As  String )
               _ACCOUNT_NO = value
            End Set
        End Property 
        <Column(Storage:="_WEBSERVICE_HOST_IP", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property WEBSERVICE_HOST_IP() As String
            Get
                Return _WEBSERVICE_HOST_IP
            End Get
            Set(ByVal value As String)
               _WEBSERVICE_HOST_IP = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_JOB_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_JOB_ID() As Long
            Get
                Return _APPOINTMENT_JOB_ID
            End Get
            Set(ByVal value As Long)
               _APPOINTMENT_JOB_ID = value
            End Set
        End Property 


    End Class
End Namespace
