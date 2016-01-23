
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_MOBILE_APPOINT_ACT table Parameter.
    '[Create by  on October, 18 2013]

    <Table(Name:="TB_MOBILE_APPOINT_ACT")>  _
    Public Class TbMobileAppointActCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _APP_DATE As DateTime = New DateTime(1,1,1)
        Dim _SERVICE_ID As  String  = ""
        Dim _SHOP_ID As  System.Nullable(Of Long)  = 0
        Dim _APPOINTMENT_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ACTION_STATUS As Char = "1"

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
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
               _MOBILE_NO = value
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
        <Column(Storage:="_SERVICE_ID", DbType:="VarChar(50)")>  _
        Public Property SERVICE_ID() As  String 
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As  String )
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_ID", DbType:="Int")>  _
        Public Property SHOP_ID() As  System.Nullable(Of Long) 
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_TIME", DbType:="DateTime")>  _
        Public Property APPOINTMENT_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _APPOINTMENT_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _APPOINTMENT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_ACTION_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTION_STATUS() As Char
            Get
                Return _ACTION_STATUS
            End Get
            Set(ByVal value As Char)
               _ACTION_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
