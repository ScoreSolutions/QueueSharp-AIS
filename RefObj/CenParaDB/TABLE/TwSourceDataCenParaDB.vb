
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TW_SOURCE_DATA table Parameter.
    '[Create by  on November, 29 2013]

    <Table(Name:="TW_SOURCE_DATA")>  _
    Public Class TwSourceDataCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TW_SOURCE_TEXTFILE_ID As Long = 0
        Dim _ORDER_TYPE As String = ""
        Dim _COMPLETE_DATE As DateTime = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _NATIONALITY As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _MOBILE_SEGMENT As  String  = ""
        Dim _LOCATION_CODE As String = ""
        Dim _LOCATION_NAME As String = ""
        Dim _PROVINCE_CODE As String = ""
        Dim _REGION_CODE As String = ""

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
        <Column(Storage:="_TW_SOURCE_TEXTFILE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_SOURCE_TEXTFILE_ID() As Long
            Get
                Return _TW_SOURCE_TEXTFILE_ID
            End Get
            Set(ByVal value As Long)
               _TW_SOURCE_TEXTFILE_ID = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_TYPE() As String
            Get
                Return _ORDER_TYPE
            End Get
            Set(ByVal value As String)
               _ORDER_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_COMPLETE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property COMPLETE_DATE() As DateTime
            Get
                Return _COMPLETE_DATE
            End Get
            Set(ByVal value As DateTime)
               _COMPLETE_DATE = value
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
        <Column(Storage:="_NATIONALITY", DbType:="VarChar(50)")>  _
        Public Property NATIONALITY() As  String 
            Get
                Return _NATIONALITY
            End Get
            Set(ByVal value As  String )
               _NATIONALITY = value
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
        <Column(Storage:="_MOBILE_SEGMENT", DbType:="VarChar(50)")>  _
        Public Property MOBILE_SEGMENT() As  String 
            Get
                Return _MOBILE_SEGMENT
            End Get
            Set(ByVal value As  String )
               _MOBILE_SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOCATION_CODE() As String
            Get
                Return _LOCATION_CODE
            End Get
            Set(ByVal value As String)
               _LOCATION_CODE = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOCATION_NAME() As String
            Get
                Return _LOCATION_NAME
            End Get
            Set(ByVal value As String)
               _LOCATION_NAME = value
            End Set
        End Property 
        <Column(Storage:="_PROVINCE_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROVINCE_CODE() As String
            Get
                Return _PROVINCE_CODE
            End Get
            Set(ByVal value As String)
               _PROVINCE_CODE = value
            End Set
        End Property 
        <Column(Storage:="_REGION_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_CODE() As String
            Get
                Return _REGION_CODE
            End Get
            Set(ByVal value As String)
               _REGION_CODE = value
            End Set
        End Property 


    End Class
End Namespace
