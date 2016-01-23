
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_SHOP_CONFIG table Parameter.
    '[Create by  on April, 12 2012]

    <Table(Name:="TB_SHOP_CONFIG")>  _
    Public Class TbShopConfigCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CONFIG_PAGE As String = ""
        Dim _CONFIG_NAME As String = ""
        Dim _CONFIG_VALUE As String = ""
        Dim _SHOP_ID As Long = 0
        Dim _EVENT_DATE As DateTime = New DateTime(1,1,1)
        Dim _ACTIVE As Char = ""

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
        <Column(Storage:="_CONFIG_PAGE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property CONFIG_PAGE() As String
            Get
                Return _CONFIG_PAGE
            End Get
            Set(ByVal value As String)
               _CONFIG_PAGE = value
            End Set
        End Property 
        <Column(Storage:="_CONFIG_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property CONFIG_NAME() As String
            Get
                Return _CONFIG_NAME
            End Get
            Set(ByVal value As String)
               _CONFIG_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CONFIG_VALUE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property CONFIG_VALUE() As String
            Get
                Return _CONFIG_VALUE
            End Get
            Set(ByVal value As String)
               _CONFIG_VALUE = value
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
        <Column(Storage:="_EVENT_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property EVENT_DATE() As DateTime
            Get
                Return _EVENT_DATE
            End Get
            Set(ByVal value As DateTime)
               _EVENT_DATE = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTIVE() As Char
            Get
                Return _ACTIVE
            End Get
            Set(ByVal value As Char)
               _ACTIVE = value
            End Set
        End Property 


    End Class
End Namespace
