
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for SYSMENU table Parameter.
    '[Create by  on June, 24 2013]

    <Table(Name:="SYSMENU")>  _
    Public Class SysmenuCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SYSMODULE_ID As Long = 0
        Dim _MENU_NAME_TH As String = ""
        Dim _MENU_NAME_EN As  String  = ""
        Dim _MENU_DESC_TH As  String  = ""
        Dim _MENU_DESC_EN As  String  = ""
        Dim _REF_SYSMENU_ID As Long = 0
        Dim _ORDER_SEQ As Long = 0
        Dim _ACTIVE As Char = "Y"
        Dim _MENU_URL As  String  = ""

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
        <Column(Storage:="_SYSMODULE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SYSMODULE_ID() As Long
            Get
                Return _SYSMODULE_ID
            End Get
            Set(ByVal value As Long)
               _SYSMODULE_ID = value
            End Set
        End Property 
        <Column(Storage:="_MENU_NAME_TH", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property MENU_NAME_TH() As String
            Get
                Return _MENU_NAME_TH
            End Get
            Set(ByVal value As String)
               _MENU_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_MENU_NAME_EN", DbType:="VarChar(100)")>  _
        Public Property MENU_NAME_EN() As  String 
            Get
                Return _MENU_NAME_EN
            End Get
            Set(ByVal value As  String )
               _MENU_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_MENU_DESC_TH", DbType:="VarChar(255)")>  _
        Public Property MENU_DESC_TH() As  String 
            Get
                Return _MENU_DESC_TH
            End Get
            Set(ByVal value As  String )
               _MENU_DESC_TH = value
            End Set
        End Property 
        <Column(Storage:="_MENU_DESC_EN", DbType:="VarChar(255)")>  _
        Public Property MENU_DESC_EN() As  String 
            Get
                Return _MENU_DESC_EN
            End Get
            Set(ByVal value As  String )
               _MENU_DESC_EN = value
            End Set
        End Property 
        <Column(Storage:="_REF_SYSMENU_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REF_SYSMENU_ID() As Long
            Get
                Return _REF_SYSMENU_ID
            End Get
            Set(ByVal value As Long)
               _REF_SYSMENU_ID = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_SEQ", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_SEQ() As Long
            Get
                Return _ORDER_SEQ
            End Get
            Set(ByVal value As Long)
               _ORDER_SEQ = value
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
        <Column(Storage:="_MENU_URL", DbType:="VarChar(255)")>  _
        Public Property MENU_URL() As  String 
            Get
                Return _MENU_URL
            End Get
            Set(ByVal value As  String )
               _MENU_URL = value
            End Set
        End Property 


    End Class
End Namespace
