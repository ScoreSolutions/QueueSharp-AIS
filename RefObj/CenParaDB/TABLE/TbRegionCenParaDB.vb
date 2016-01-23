
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REGION table Parameter.
    '[Create by  on April, 12 2012]

    <Table(Name:="TB_REGION")>  _
    Public Class TbRegionCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _REGION_CODE As  String  = ""
        Dim _REGION_NAME_TH As String = ""
        Dim _REGION_NAME_EN As  String  = ""
        Dim _ACTIVE As Char = ""
        Dim _LOCATION_GROUP As  String  = ""

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
        <Column(Storage:="_REGION_CODE", DbType:="VarChar(50)")>  _
        Public Property REGION_CODE() As  String 
            Get
                Return _REGION_CODE
            End Get
            Set(ByVal value As  String )
               _REGION_CODE = value
            End Set
        End Property 
        <Column(Storage:="_REGION_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_NAME_TH() As String
            Get
                Return _REGION_NAME_TH
            End Get
            Set(ByVal value As String)
               _REGION_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_REGION_NAME_EN", DbType:="VarChar(255)")>  _
        Public Property REGION_NAME_EN() As  String 
            Get
                Return _REGION_NAME_EN
            End Get
            Set(ByVal value As  String )
               _REGION_NAME_EN = value
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
        <Column(Storage:="_LOCATION_GROUP", DbType:="VarChar(50)")>  _
        Public Property LOCATION_GROUP() As  String 
            Get
                Return _LOCATION_GROUP
            End Get
            Set(ByVal value As  String )
               _LOCATION_GROUP = value
            End Set
        End Property 


            'Define Child Table 

       Dim _TB_BUILDING_region_id As DataTable
       Dim _TB_SHOP_region_id As DataTable

       Public Property CHILD_TB_BUILDING_region_id() As DataTable
           Get
               Return _TB_BUILDING_region_id
           End Get
           Set(ByVal value As DataTable)
               _TB_BUILDING_region_id = value
           End Set
       End Property
       Public Property CHILD_TB_SHOP_region_id() As DataTable
           Get
               Return _TB_SHOP_region_id
           End Get
           Set(ByVal value As DataTable)
               _TB_SHOP_region_id = value
           End Set
       End Property
    End Class
End Namespace
