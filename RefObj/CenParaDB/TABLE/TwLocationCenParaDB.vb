
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TW_LOCATION table Parameter.
    '[Create by  on May, 29 2014]

    <Table(Name:="TW_LOCATION")>  _
    Public Class TwLocationCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _LOCATION_CODE As String = ""
        Dim _LOCATION_NAME_TH As  String  = ""
        Dim _LOCATION_NAME_EN As  String  = ""
        Dim _LOCATION_ABB As  String  = ""
        Dim _LOCATION_SEGMENT As  String  = ""
        Dim _LOCATION_TYPE As  String  = ""
        Dim _PROVINCE_CODE As String = ""
        Dim _REGION_CODE As String = ""
        Dim _ACTIVE As Char = "Y"

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_LOCATION_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOCATION_CODE() As String
            Get
                Return _LOCATION_CODE
            End Get
            Set(ByVal value As String)
               _LOCATION_CODE = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_NAME_TH", DbType:="VarChar(255)")>  _
        Public Property LOCATION_NAME_TH() As  String 
            Get
                Return _LOCATION_NAME_TH
            End Get
            Set(ByVal value As  String )
               _LOCATION_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_NAME_EN", DbType:="VarChar(255)")>  _
        Public Property LOCATION_NAME_EN() As  String 
            Get
                Return _LOCATION_NAME_EN
            End Get
            Set(ByVal value As  String )
               _LOCATION_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_ABB", DbType:="VarChar(50)")>  _
        Public Property LOCATION_ABB() As  String 
            Get
                Return _LOCATION_ABB
            End Get
            Set(ByVal value As  String )
               _LOCATION_ABB = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_SEGMENT", DbType:="VarChar(255)")>  _
        Public Property LOCATION_SEGMENT() As  String 
            Get
                Return _LOCATION_SEGMENT
            End Get
            Set(ByVal value As  String )
               _LOCATION_SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_TYPE", DbType:="VarChar(50)")>  _
        Public Property LOCATION_TYPE() As  String 
            Get
                Return _LOCATION_TYPE
            End Get
            Set(ByVal value As  String )
               _LOCATION_TYPE = value
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
