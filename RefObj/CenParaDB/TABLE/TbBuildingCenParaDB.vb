
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_BUILDING table Parameter.
    '[Create by  on April, 12 2012]

    <Table(Name:="TB_BUILDING")>  _
    Public Class TbBuildingCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _REGION_ID As Long = 0
        Dim _BUILDING_NAME_TH As String = ""
        Dim _BUILDING_NAME_EN As  String  = ""
        Dim _ACTIVE As Char = ""

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
        <Column(Storage:="_REGION_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_ID() As Long
            Get
                Return _REGION_ID
            End Get
            Set(ByVal value As Long)
               _REGION_ID = value
            End Set
        End Property 
        <Column(Storage:="_BUILDING_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property BUILDING_NAME_TH() As String
            Get
                Return _BUILDING_NAME_TH
            End Get
            Set(ByVal value As String)
               _BUILDING_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_BUILDING_NAME_EN", DbType:="VarChar(255)")>  _
        Public Property BUILDING_NAME_EN() As  String 
            Get
                Return _BUILDING_NAME_EN
            End Get
            Set(ByVal value As  String )
               _BUILDING_NAME_EN = value
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


            'Define Child Table 

       Dim _TB_SHOP_building_id As DataTable

       Public Property CHILD_TB_SHOP_building_id() As DataTable
           Get
               Return _TB_SHOP_building_id
           End Get
           Set(ByVal value As DataTable)
               _TB_SHOP_building_id = value
           End Set
       End Property
    End Class
End Namespace
