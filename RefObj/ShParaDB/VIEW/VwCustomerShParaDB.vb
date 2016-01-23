
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_Customer view Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="VW_Customer")>  _
    Public Class VwCustomerShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _TITLE_NAME As  String  = ""
        Dim _F_NAME As  String  = ""
        Dim _L_NAME As  String  = ""
        Dim _MOBILE_NO As  String  = ""
        Dim _MOBILE_STATUS As  String  = ""
        Dim _EMAIL As  String  = ""
        Dim _SEGMENT_LEVEL As  String  = ""
        Dim _BIRTH_DATE As  String  = ""
        Dim _CATEGORY As  String  = ""
        Dim _ACCOUNT_BALANCE As  String  = ""
        Dim _CONTACT_CLASS As  String  = ""
        Dim _SERVICE_YEAR As  String  = ""
        Dim _CHUM_SCORE As  String  = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _CAMPAIGN_CODE As  String  = ""
        Dim _CAMPAIGN_NAME As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CREATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _CAMPAIGN_NAME_EN As  String  = ""
        Dim _CAMPAIGN_DESC As  String  = ""
        Dim _CAMPAIGN_DESC_TH2 As  String  = ""
        Dim _CAMPAIGN_DESC_EN2 As  String  = ""

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
        <Column(Storage:="_TITLE_NAME", DbType:="VarChar(100)")>  _
        Public Property TITLE_NAME() As  String 
            Get
                Return _TITLE_NAME
            End Get
            Set(ByVal value As  String )
               _TITLE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_F_NAME", DbType:="VarChar(200)")>  _
        Public Property F_NAME() As  String 
            Get
                Return _F_NAME
            End Get
            Set(ByVal value As  String )
               _F_NAME = value
            End Set
        End Property 
        <Column(Storage:="_L_NAME", DbType:="VarChar(500)")>  _
        Public Property L_NAME() As  String 
            Get
                Return _L_NAME
            End Get
            Set(ByVal value As  String )
               _L_NAME = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(20)")>  _
        Public Property MOBILE_NO() As  String 
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As  String )
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_STATUS", DbType:="VarChar(100)")>  _
        Public Property MOBILE_STATUS() As  String 
            Get
                Return _MOBILE_STATUS
            End Get
            Set(ByVal value As  String )
               _MOBILE_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL", DbType:="VarChar(200)")>  _
        Public Property EMAIL() As  String 
            Get
                Return _EMAIL
            End Get
            Set(ByVal value As  String )
               _EMAIL = value
            End Set
        End Property 
        <Column(Storage:="_SEGMENT_LEVEL", DbType:="VarChar(100)")>  _
        Public Property SEGMENT_LEVEL() As  String 
            Get
                Return _SEGMENT_LEVEL
            End Get
            Set(ByVal value As  String )
               _SEGMENT_LEVEL = value
            End Set
        End Property 
        <Column(Storage:="_BIRTH_DATE", DbType:="VarChar(100)")>  _
        Public Property BIRTH_DATE() As  String 
            Get
                Return _BIRTH_DATE
            End Get
            Set(ByVal value As  String )
               _BIRTH_DATE = value
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
        <Column(Storage:="_ACCOUNT_BALANCE", DbType:="VarChar(100)")>  _
        Public Property ACCOUNT_BALANCE() As  String 
            Get
                Return _ACCOUNT_BALANCE
            End Get
            Set(ByVal value As  String )
               _ACCOUNT_BALANCE = value
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
        <Column(Storage:="_SERVICE_YEAR", DbType:="VarChar(100)")>  _
        Public Property SERVICE_YEAR() As  String 
            Get
                Return _SERVICE_YEAR
            End Get
            Set(ByVal value As  String )
               _SERVICE_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_CHUM_SCORE", DbType:="VarChar(100)")>  _
        Public Property CHUM_SCORE() As  String 
            Get
                Return _CHUM_SCORE
            End Get
            Set(ByVal value As  String )
               _CHUM_SCORE = value
            End Set
        End Property 
        <Column(Storage:="_PREFER_LANG", DbType:="VarChar(50)")>  _
        Public Property PREFER_LANG() As  String 
            Get
                Return _PREFER_LANG
            End Get
            Set(ByVal value As  String )
               _PREFER_LANG = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_CODE", DbType:="VarChar(100)")>  _
        Public Property CAMPAIGN_CODE() As  String 
            Get
                Return _CAMPAIGN_CODE
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_CODE = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_NAME", DbType:="VarChar(255)")>  _
        Public Property CAMPAIGN_NAME() As  String 
            Get
                Return _CAMPAIGN_NAME
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_NAME = value
            End Set
        End Property 
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(50)")>  _
        Public Property NETWORK_TYPE() As  String 
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As  String )
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime")>  _
        Public Property CREATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CREATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50)")>  _
        Public Property CREATE_BY() As  String 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  String )
               _CREATE_BY = value
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(50)")>  _
        Public Property UPDATE_BY() As  String 
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As  String )
               _UPDATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_NAME_EN", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_NAME_EN() As  String 
            Get
                Return _CAMPAIGN_NAME_EN
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_DESC", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_DESC() As  String 
            Get
                Return _CAMPAIGN_DESC
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_DESC = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_DESC_TH2", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_DESC_TH2() As  String 
            Get
                Return _CAMPAIGN_DESC_TH2
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_DESC_TH2 = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_DESC_EN2", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_DESC_EN2() As  String 
            Get
                Return _CAMPAIGN_DESC_EN2
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_DESC_EN2 = value
            End Set
        End Property 


    End Class
End Namespace
