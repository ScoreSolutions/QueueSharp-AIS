
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_SHOP table Parameter.
    '[Create by  on December, 27 2012]

    <Table(Name:="TB_SHOP")>  _
    Public Class TbShopCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_CODE As String = ""
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As  String  = ""
        Dim _SHOP_ABB As  String  = ""
        Dim _REGION_ID As Long = 0
        Dim _BUILDING_ID As  System.Nullable(Of Long)  = 0
        Dim _OPEN_TIME As String = ""
        Dim _CLOSE_TIME As String = ""
        Dim _SHOP_DB_NAME As String = ""
        Dim _SHOP_DB_USERID As String = ""
        Dim _SHOP_DB_PWD As String = ""
        Dim _SHOP_DB_SERVER As String = ""
        Dim _SHOP_DR_NAME As String = ""
        Dim _SHOP_DR_USERID As String = ""
        Dim _SHOP_DR_PWD As String = ""
        Dim _SHOP_DR_SERVER As String = ""
        Dim _SHOP_USE_QM As Char = "Y"
        Dim _SHOP_QM_URL As String = ""
        Dim _ACTIVE As Char = "Y"
        Dim _QM_FTP_USERNAME As  String  = ""
        Dim _QM_FTP_PASSWORD As  String  = ""
        Dim _SHOP_SIZE As  System.Nullable(Of Char)  = ""
        Dim _MAIN_SERVERNAME As  String  = ""
        Dim _MAIN_DB_NAME As  String  = ""
        Dim _MAIN_DB_USERID As  String  = ""
        Dim _MAIN_DB_PWD As  String  = ""

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
        <Column(Storage:="_SHOP_CODE", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_CODE() As String
            Get
                Return _SHOP_CODE
            End Get
            Set(ByVal value As String)
               _SHOP_CODE = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255)")>  _
        Public Property SHOP_NAME_EN() As  String 
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As  String )
               _SHOP_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_ABB", DbType:="VarChar(50)")>  _
        Public Property SHOP_ABB() As  String 
            Get
                Return _SHOP_ABB
            End Get
            Set(ByVal value As  String )
               _SHOP_ABB = value
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
        <Column(Storage:="_BUILDING_ID", DbType:="Int")>  _
        Public Property BUILDING_ID() As  System.Nullable(Of Long) 
            Get
                Return _BUILDING_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BUILDING_ID = value
            End Set
        End Property 
        <Column(Storage:="_OPEN_TIME", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property OPEN_TIME() As String
            Get
                Return _OPEN_TIME
            End Get
            Set(ByVal value As String)
               _OPEN_TIME = value
            End Set
        End Property 
        <Column(Storage:="_CLOSE_TIME", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property CLOSE_TIME() As String
            Get
                Return _CLOSE_TIME
            End Get
            Set(ByVal value As String)
               _CLOSE_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_NAME", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_NAME() As String
            Get
                Return _SHOP_DB_NAME
            End Get
            Set(ByVal value As String)
               _SHOP_DB_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_USERID", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_USERID() As String
            Get
                Return _SHOP_DB_USERID
            End Get
            Set(ByVal value As String)
               _SHOP_DB_USERID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_PWD", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_PWD() As String
            Get
                Return _SHOP_DB_PWD
            End Get
            Set(ByVal value As String)
               _SHOP_DB_PWD = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_SERVER", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_SERVER() As String
            Get
                Return _SHOP_DB_SERVER
            End Get
            Set(ByVal value As String)
               _SHOP_DB_SERVER = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_NAME", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_NAME() As String
            Get
                Return _SHOP_DR_NAME
            End Get
            Set(ByVal value As String)
               _SHOP_DR_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_USERID", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_USERID() As String
            Get
                Return _SHOP_DR_USERID
            End Get
            Set(ByVal value As String)
               _SHOP_DR_USERID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_PWD", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_PWD() As String
            Get
                Return _SHOP_DR_PWD
            End Get
            Set(ByVal value As String)
               _SHOP_DR_PWD = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_SERVER", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_SERVER() As String
            Get
                Return _SHOP_DR_SERVER
            End Get
            Set(ByVal value As String)
               _SHOP_DR_SERVER = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_USE_QM", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_USE_QM() As Char
            Get
                Return _SHOP_USE_QM
            End Get
            Set(ByVal value As Char)
               _SHOP_USE_QM = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_QM_URL", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_QM_URL() As String
            Get
                Return _SHOP_QM_URL
            End Get
            Set(ByVal value As String)
               _SHOP_QM_URL = value
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
        <Column(Storage:="_QM_FTP_USERNAME", DbType:="VarChar(100)")>  _
        Public Property QM_FTP_USERNAME() As  String 
            Get
                Return _QM_FTP_USERNAME
            End Get
            Set(ByVal value As  String )
               _QM_FTP_USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_QM_FTP_PASSWORD", DbType:="VarChar(100)")>  _
        Public Property QM_FTP_PASSWORD() As  String 
            Get
                Return _QM_FTP_PASSWORD
            End Get
            Set(ByVal value As  String )
               _QM_FTP_PASSWORD = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_SIZE", DbType:="Char(1)")>  _
        Public Property SHOP_SIZE() As  System.Nullable(Of Char) 
            Get
                Return _SHOP_SIZE
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _SHOP_SIZE = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_SERVERNAME", DbType:="VarChar(100)")>  _
        Public Property MAIN_SERVERNAME() As  String 
            Get
                Return _MAIN_SERVERNAME
            End Get
            Set(ByVal value As  String )
               _MAIN_SERVERNAME = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_DB_NAME", DbType:="VarChar(100)")>  _
        Public Property MAIN_DB_NAME() As  String 
            Get
                Return _MAIN_DB_NAME
            End Get
            Set(ByVal value As  String )
               _MAIN_DB_NAME = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_DB_USERID", DbType:="VarChar(100)")>  _
        Public Property MAIN_DB_USERID() As  String 
            Get
                Return _MAIN_DB_USERID
            End Get
            Set(ByVal value As  String )
               _MAIN_DB_USERID = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_DB_PWD", DbType:="VarChar(500)")>  _
        Public Property MAIN_DB_PWD() As  String 
            Get
                Return _MAIN_DB_PWD
            End Get
            Set(ByVal value As  String )
               _MAIN_DB_PWD = value
            End Set
        End Property 


    End Class
End Namespace
