
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT_EDIT_JOB table Parameter.
    '[Create by  on September, 30 2014]

    <Table(Name:="TB_APPOINTMENT_EDIT_JOB")>  _
    Public Class TbAppointmentEditJobCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_APPOINTMENT_JOB_ID_OLD As Long = 0
        Dim _MOBILE_NO As String = ""
        Dim _TB_SHOP_ID As Long = 0
        Dim _SERVICE_ID As String = ""
        Dim _APP_DATE As DateTime = New DateTime(1,1,1)
        Dim _START_SLOT As DateTime = New DateTime(1,1,1)
        Dim _APPOINTMENT_CHANNEL As Char = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _CUSTOMER_EMAIL As  String  = ""
        Dim _ISDELETEOLDDATA As  System.Nullable(Of Char)  = "N"

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(100)")>  _
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
        <Column(Storage:="_TB_APPOINTMENT_JOB_ID_OLD", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_APPOINTMENT_JOB_ID_OLD() As Long
            Get
                Return _TB_APPOINTMENT_JOB_ID_OLD
            End Get
            Set(ByVal value As Long)
               _TB_APPOINTMENT_JOB_ID_OLD = value
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
        <Column(Storage:="_TB_SHOP_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_SHOP_ID() As Long
            Get
                Return _TB_SHOP_ID
            End Get
            Set(ByVal value As Long)
               _TB_SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_ID", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As String
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As String)
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_APP_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property APP_DATE() As DateTime
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As DateTime)
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_START_SLOT", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property START_SLOT() As DateTime
            Get
                Return _START_SLOT
            End Get
            Set(ByVal value As DateTime)
               _START_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_CHANNEL() As Char
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As Char)
               _APPOINTMENT_CHANNEL = value
            End Set
        End Property 
        <Column(Storage:="_PREFER_LANG", DbType:="VarChar(100)")>  _
        Public Property PREFER_LANG() As  String 
            Get
                Return _PREFER_LANG
            End Get
            Set(ByVal value As  String )
               _PREFER_LANG = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_EMAIL", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_EMAIL() As  String 
            Get
                Return _CUSTOMER_EMAIL
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_EMAIL = value
            End Set
        End Property 
        <Column(Storage:="_ISDELETEOLDDATA", DbType:="Char(1)")>  _
        Public Property ISDELETEOLDDATA() As  System.Nullable(Of Char) 
            Get
                Return _ISDELETEOLDDATA
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _ISDELETEOLDDATA = value
            End Set
        End Property 


    End Class
End Namespace
