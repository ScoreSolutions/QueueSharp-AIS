
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for LOGIN_HISTORY table Parameter.
    '[Create by  on October, 15 2013]

    <Table(Name:="LOGIN_HISTORY")>  _
    Public Class LoginHistoryCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CUSTOMER_ID As String = ""
        Dim _LOGIN_TIME As DateTime = New DateTime(1,1,1)
        Dim _LOGOUT_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _IP_ADDRESS As String = ""
        Dim _SESSION_ID As String = ""
        Dim _BROWSER As String = ""
        Dim _LOGIN_MODULE As  String  = ""
        Dim _SERVER_URL As  String  = ""
        Dim _USER_SHOP As  String  = ""

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
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime2 NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime2")>  _
        Public Property UPDATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _UPDATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_ID", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property CUSTOMER_ID() As String
            Get
                Return _CUSTOMER_ID
            End Get
            Set(ByVal value As String)
               _CUSTOMER_ID = value
            End Set
        End Property 
        <Column(Storage:="_LOGIN_TIME", DbType:="DateTime2 NOT NULL ",CanBeNull:=false)>  _
        Public Property LOGIN_TIME() As DateTime
            Get
                Return _LOGIN_TIME
            End Get
            Set(ByVal value As DateTime)
               _LOGIN_TIME = value
            End Set
        End Property 
        <Column(Storage:="_LOGOUT_TIME", DbType:="DateTime2")>  _
        Public Property LOGOUT_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _LOGOUT_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _LOGOUT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_IP_ADDRESS", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property IP_ADDRESS() As String
            Get
                Return _IP_ADDRESS
            End Get
            Set(ByVal value As String)
               _IP_ADDRESS = value
            End Set
        End Property 
        <Column(Storage:="_SESSION_ID", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SESSION_ID() As String
            Get
                Return _SESSION_ID
            End Get
            Set(ByVal value As String)
               _SESSION_ID = value
            End Set
        End Property 
        <Column(Storage:="_BROWSER", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property BROWSER() As String
            Get
                Return _BROWSER
            End Get
            Set(ByVal value As String)
               _BROWSER = value
            End Set
        End Property 
        <Column(Storage:="_LOGIN_MODULE", DbType:="VarChar(255)")>  _
        Public Property LOGIN_MODULE() As  String 
            Get
                Return _LOGIN_MODULE
            End Get
            Set(ByVal value As  String )
               _LOGIN_MODULE = value
            End Set
        End Property 
        <Column(Storage:="_SERVER_URL", DbType:="VarChar(1000)")>  _
        Public Property SERVER_URL() As  String 
            Get
                Return _SERVER_URL
            End Get
            Set(ByVal value As  String )
               _SERVER_URL = value
            End Set
        End Property 
        <Column(Storage:="_USER_SHOP", DbType:="VarChar(50)")>  _
        Public Property USER_SHOP() As  String 
            Get
                Return _USER_SHOP
            End Get
            Set(ByVal value As  String )
               _USER_SHOP = value
            End Set
        End Property 


    End Class
End Namespace
