
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_ITEM_GROUP table Parameter.
    '[Create by  on December, 13 2013]

    <Table(Name:="TB_ITEM_GROUP")>  _
    Public Class TbItemGroupCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ITEM_GROUP_CODE As String = ""
        Dim _ITEM_GROUP_NAME_EN As String = ""
        Dim _ITEM_GROUP_NAME_TH As String = ""
        Dim _ACTIVE_STATUS As Char = "Y"

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
        <Column(Storage:="_ITEM_GROUP_CODE", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_GROUP_CODE() As String
            Get
                Return _ITEM_GROUP_CODE
            End Get
            Set(ByVal value As String)
               _ITEM_GROUP_CODE = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_GROUP_NAME_EN", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_GROUP_NAME_EN() As String
            Get
                Return _ITEM_GROUP_NAME_EN
            End Get
            Set(ByVal value As String)
               _ITEM_GROUP_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_GROUP_NAME_TH", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_GROUP_NAME_TH() As String
            Get
                Return _ITEM_GROUP_NAME_TH
            End Get
            Set(ByVal value As String)
               _ITEM_GROUP_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTIVE_STATUS() As Char
            Get
                Return _ACTIVE_STATUS
            End Get
            Set(ByVal value As Char)
               _ACTIVE_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
