
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_CAPACITY_SHOP_DAY table Parameter.
    '[Create by  on March, 25 2013]

    <Table(Name:="TB_REP_CAPACITY_SHOP_DAY")>  _
    Public Class TbRepCapacityShopDayCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_EN As String = ""
        Dim _SHOP_NAME_TH As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SHOW_DATE As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _WORKING_HOUR As String = ""
        Dim _KPI As Long = 0
        Dim _NO_COUNTER As Long = 0
        Dim _CAPACITY_TRANS As Long = 0
        Dim _APPOINTMENT As Long = 0
        Dim _EXPECTED_WALK_IN As Long = 0
        Dim _TOTAL_TO_BE_SERVE As Long = 0
        Dim _EXPECTED_CAPACITY_USED As Long = 0
        Dim _EXPECTED_OPEN_COUNTER As Long = 0

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
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_EN() As String
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_EN = value
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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_DATE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_DATE() As String
            Get
                Return _SHOW_DATE
            End Get
            Set(ByVal value As String)
               _SHOW_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As Long
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As Long)
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_EN() As String
            Get
                Return _SERVICE_NAME_EN
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_TH() As String
            Get
                Return _SERVICE_NAME_TH
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_WORKING_HOUR", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property WORKING_HOUR() As String
            Get
                Return _WORKING_HOUR
            End Get
            Set(ByVal value As String)
               _WORKING_HOUR = value
            End Set
        End Property 
        <Column(Storage:="_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KPI() As Long
            Get
                Return _KPI
            End Get
            Set(ByVal value As Long)
               _KPI = value
            End Set
        End Property 
        <Column(Storage:="_NO_COUNTER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_COUNTER() As Long
            Get
                Return _NO_COUNTER
            End Get
            Set(ByVal value As Long)
               _NO_COUNTER = value
            End Set
        End Property 
        <Column(Storage:="_CAPACITY_TRANS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CAPACITY_TRANS() As Long
            Get
                Return _CAPACITY_TRANS
            End Get
            Set(ByVal value As Long)
               _CAPACITY_TRANS = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT() As Long
            Get
                Return _APPOINTMENT
            End Get
            Set(ByVal value As Long)
               _APPOINTMENT = value
            End Set
        End Property 
        <Column(Storage:="_EXPECTED_WALK_IN", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EXPECTED_WALK_IN() As Long
            Get
                Return _EXPECTED_WALK_IN
            End Get
            Set(ByVal value As Long)
               _EXPECTED_WALK_IN = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_TO_BE_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_TO_BE_SERVE() As Long
            Get
                Return _TOTAL_TO_BE_SERVE
            End Get
            Set(ByVal value As Long)
               _TOTAL_TO_BE_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_EXPECTED_CAPACITY_USED", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EXPECTED_CAPACITY_USED() As Long
            Get
                Return _EXPECTED_CAPACITY_USED
            End Get
            Set(ByVal value As Long)
               _EXPECTED_CAPACITY_USED = value
            End Set
        End Property 
        <Column(Storage:="_EXPECTED_OPEN_COUNTER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EXPECTED_OPEN_COUNTER() As Long
            Get
                Return _EXPECTED_OPEN_COUNTER
            End Get
            Set(ByVal value As Long)
               _EXPECTED_OPEN_COUNTER = value
            End Set
        End Property 


    End Class
End Namespace
