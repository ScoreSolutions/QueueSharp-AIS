
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_CUST_SEGMENT_WEEK table Parameter.
    '[Create by  on March, 9 2013]

    <Table(Name:="TB_REP_CUST_SEGMENT_WEEK")>  _
    Public Class TbRepCustSegmentWeekCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SHOW_YEAR As Long = 0
        Dim _WEEK_OF_YEAR As Long = 0
        Dim _PERIOD_DATE As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _MASS_REGIS As Long = 0
        Dim _MASS_SERVE As Long = 0
        Dim _MASS_MISS_CALL As Long = 0
        Dim _MASS_CANCEL As Long = 0
        Dim _MASS_INCOMPLETE As Long = 0
        Dim _SERENADE_REGIS As Long = 0
        Dim _SERENADE_SERVE As Long = 0
        Dim _SERENADE_MISS_CALL As Long = 0
        Dim _SERENADE_CANCEL As Long = 0
        Dim _SERENADE_INCOMPLETE As Long = 0

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
        <Column(Storage:="_SHOP_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_TH = value
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
        <Column(Storage:="_SHOW_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_YEAR() As Long
            Get
                Return _SHOW_YEAR
            End Get
            Set(ByVal value As Long)
               _SHOW_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_WEEK_OF_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEEK_OF_YEAR() As Long
            Get
                Return _WEEK_OF_YEAR
            End Get
            Set(ByVal value As Long)
               _WEEK_OF_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_PERIOD_DATE", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property PERIOD_DATE() As String
            Get
                Return _PERIOD_DATE
            End Get
            Set(ByVal value As String)
               _PERIOD_DATE = value
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
        <Column(Storage:="_MASS_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_REGIS() As Long
            Get
                Return _MASS_REGIS
            End Get
            Set(ByVal value As Long)
               _MASS_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_MASS_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_SERVE() As Long
            Get
                Return _MASS_SERVE
            End Get
            Set(ByVal value As Long)
               _MASS_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_MASS_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_MISS_CALL() As Long
            Get
                Return _MASS_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _MASS_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_MASS_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_CANCEL() As Long
            Get
                Return _MASS_CANCEL
            End Get
            Set(ByVal value As Long)
               _MASS_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_MASS_INCOMPLETE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_INCOMPLETE() As Long
            Get
                Return _MASS_INCOMPLETE
            End Get
            Set(ByVal value As Long)
               _MASS_INCOMPLETE = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_REGIS() As Long
            Get
                Return _SERENADE_REGIS
            End Get
            Set(ByVal value As Long)
               _SERENADE_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_SERVE() As Long
            Get
                Return _SERENADE_SERVE
            End Get
            Set(ByVal value As Long)
               _SERENADE_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_MISS_CALL() As Long
            Get
                Return _SERENADE_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _SERENADE_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_CANCEL() As Long
            Get
                Return _SERENADE_CANCEL
            End Get
            Set(ByVal value As Long)
               _SERENADE_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_INCOMPLETE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_INCOMPLETE() As Long
            Get
                Return _SERENADE_INCOMPLETE
            End Get
            Set(ByVal value As Long)
               _SERENADE_INCOMPLETE = value
            End Set
        End Property 


    End Class
End Namespace
