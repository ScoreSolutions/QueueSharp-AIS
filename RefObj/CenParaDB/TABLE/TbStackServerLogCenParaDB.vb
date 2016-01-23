
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_STACK_SERVER_LOG table Parameter.
    '[Create by  on May, 27 2012]

    <Table(Name:="TB_STACK_SERVER_LOG")>  _
    Public Class TbStackServerLogCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _LOG_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SERVER_NAME As String = ""
        Dim _IP_ADDRESS As String = ""
        Dim _LOCATION As String = ""
        Dim _STACK_DESC As String = ""
        Dim _SEND_EMAIL As Char = ""
        Dim _SEND_SMS As Char = ""

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(20)")>  _
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
        <Column(Storage:="_LOG_DATE", DbType:="DateTime")>  _
        Public Property LOG_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _LOG_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _LOG_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SERVER_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVER_NAME() As String
            Get
                Return _SERVER_NAME
            End Get
            Set(ByVal value As String)
               _SERVER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_IP_ADDRESS", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property IP_ADDRESS() As String
            Get
                Return _IP_ADDRESS
            End Get
            Set(ByVal value As String)
               _IP_ADDRESS = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOCATION() As String
            Get
                Return _LOCATION
            End Get
            Set(ByVal value As String)
               _LOCATION = value
            End Set
        End Property 
        <Column(Storage:="_STACK_DESC", DbType:="Text NOT NULL ",CanBeNull:=false)>  _
        Public Property STACK_DESC() As String
            Get
                Return _STACK_DESC
            End Get
            Set(ByVal value As String)
               _STACK_DESC = value
            End Set
        End Property 
        <Column(Storage:="_SEND_EMAIL", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SEND_EMAIL() As Char
            Get
                Return _SEND_EMAIL
            End Get
            Set(ByVal value As Char)
               _SEND_EMAIL = value
            End Set
        End Property 
        <Column(Storage:="_SEND_SMS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SEND_SMS() As Char
            Get
                Return _SEND_SMS
            End Get
            Set(ByVal value As Char)
               _SEND_SMS = value
            End Set
        End Property 


    End Class
End Namespace
