
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_FILTER_TEMP_DATA table Parameter.
    '[Create by  on October, 26 2015]

    <Table(Name:="TB_FILTER_TEMP_DATA")>  _
    Public Class TbFilterTempDataCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_SHOP_ID As Long = 0
        Dim _TB_FILTER_ID As Long = 0
        Dim _QUEUE_NO As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _END_TIME As DateTime = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _MASTER_ITEMID As Long = 0
        Dim _ITEM_NAME As String = ""
        Dim _CATEGORY As  String  = ""
        Dim _CONTACT_CLASS As  String  = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _SEGMENT As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _USERNAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _SH_TEMPLATE_CODE As  String  = ""

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
        <Column(Storage:="_TB_SHOP_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_SHOP_ID() As Long
            Get
                Return _TB_SHOP_ID
            End Get
            Set(ByVal value As Long)
               _TB_SHOP_ID = value
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
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property QUEUE_NO() As String
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As String)
               _QUEUE_NO = value
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
        <Column(Storage:="_END_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property END_TIME() As DateTime
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As DateTime)
               _END_TIME = value
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
        <Column(Storage:="_MASTER_ITEMID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property MASTER_ITEMID() As Long
            Get
                Return _MASTER_ITEMID
            End Get
            Set(ByVal value As Long)
               _MASTER_ITEMID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME() As String
            Get
                Return _ITEM_NAME
            End Get
            Set(ByVal value As String)
               _ITEM_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CATEGORY", DbType:="VarChar(100)")>  _
        Public Property CATEGORY() As  String 
            Get
                Return _CATEGORY
            End Get
            Set(ByVal value As  String )
               _CATEGORY = value
            End Set
        End Property 
        <Column(Storage:="_CONTACT_CLASS", DbType:="VarChar(100)")>  _
        Public Property CONTACT_CLASS() As  String 
            Get
                Return _CONTACT_CLASS
            End Get
            Set(ByVal value As  String )
               _CONTACT_CLASS = value
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
        <Column(Storage:="_SEGMENT", DbType:="VarChar(100)")>  _
        Public Property SEGMENT() As  String 
            Get
                Return _SEGMENT
            End Get
            Set(ByVal value As  String )
               _SEGMENT = value
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
        <Column(Storage:="_USERNAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property STAFF_NAME() As String
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As String)
               _STAFF_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SH_TEMPLATE_CODE", DbType:="VarChar(50)")>  _
        Public Property SH_TEMPLATE_CODE() As  String 
            Get
                Return _SH_TEMPLATE_CODE
            End Get
            Set(ByVal value As  String )
               _SH_TEMPLATE_CODE = value
            End Set
        End Property 


    End Class
End Namespace
