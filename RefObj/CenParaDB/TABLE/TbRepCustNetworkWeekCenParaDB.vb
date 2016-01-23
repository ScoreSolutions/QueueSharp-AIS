
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_CUST_NETWORK_WEEK table Parameter.
    '[Create by  on December, 20 2013]

    <Table(Name:="TB_REP_CUST_NETWORK_WEEK")>  _
    Public Class TbRepCustNetworkWeekCenParaDB

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
        Dim _GSM_REGIS As Long = 0
        Dim _GSM_SERVE As Long = 0
        Dim _GSM_MISS_CALL As Long = 0
        Dim _GSM_CANCEL As Long = 0
        Dim _GSM_NOTCALL As Long = 0
        Dim _GSM_NOTCON As Long = 0
        Dim _GSM_NOTEND As Long = 0
        Dim _OTC_REGIS As Long = 0
        Dim _OTC_SERVE As Long = 0
        Dim _OTC_MISS_CALL As Long = 0
        Dim _OTC_CANCEL As Long = 0
        Dim _OTC_NOTCALL As Long = 0
        Dim _OTC_NOTCON As Long = 0
        Dim _OTC_NOTEND As Long = 0
        Dim _NON_REGIS As Long = 0
        Dim _NON_SERVE As Long = 0
        Dim _NON_MISS_CALL As Long = 0
        Dim _NON_CANCEL As Long = 0
        Dim _NON_NOTCALL As Long = 0
        Dim _NON_NOTCON As Long = 0
        Dim _NON_NOTEND As Long = 0
        Dim _AIS3G_REGIS As Long = 0
        Dim _AIS3G_SERVE As Long = 0
        Dim _AIS3G_MISS_CALL As Long = 0
        Dim _AIS3G_CANCEL As Long = 0
        Dim _AIS3G_NOTCALL As Long = 0
        Dim _AIS3G_NOTCON As Long = 0
        Dim _AIS3G_NOTEND As Long = 0
        Dim _OTC3G_REGIS As Long = 0
        Dim _OTC3G_SERVE As Long = 0
        Dim _OTC3G_MISS_CALL As Long = 0
        Dim _OTC3G_CANCEL As Long = 0
        Dim _OTC3G_NOTCALL As Long = 0
        Dim _OTC3G_NOTCON As Long = 0
        Dim _OTC3G_NOTEND As Long = 0

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
        <Column(Storage:="_GSM_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_REGIS() As Long
            Get
                Return _GSM_REGIS
            End Get
            Set(ByVal value As Long)
               _GSM_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_GSM_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_SERVE() As Long
            Get
                Return _GSM_SERVE
            End Get
            Set(ByVal value As Long)
               _GSM_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_GSM_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_MISS_CALL() As Long
            Get
                Return _GSM_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _GSM_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_GSM_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_CANCEL() As Long
            Get
                Return _GSM_CANCEL
            End Get
            Set(ByVal value As Long)
               _GSM_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_GSM_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_NOTCALL() As Long
            Get
                Return _GSM_NOTCALL
            End Get
            Set(ByVal value As Long)
               _GSM_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_GSM_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_NOTCON() As Long
            Get
                Return _GSM_NOTCON
            End Get
            Set(ByVal value As Long)
               _GSM_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_GSM_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_NOTEND() As Long
            Get
                Return _GSM_NOTEND
            End Get
            Set(ByVal value As Long)
               _GSM_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_OTC_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_REGIS() As Long
            Get
                Return _OTC_REGIS
            End Get
            Set(ByVal value As Long)
               _OTC_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_OTC_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_SERVE() As Long
            Get
                Return _OTC_SERVE
            End Get
            Set(ByVal value As Long)
               _OTC_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_OTC_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_MISS_CALL() As Long
            Get
                Return _OTC_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _OTC_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_CANCEL() As Long
            Get
                Return _OTC_CANCEL
            End Get
            Set(ByVal value As Long)
               _OTC_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_OTC_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_NOTCALL() As Long
            Get
                Return _OTC_NOTCALL
            End Get
            Set(ByVal value As Long)
               _OTC_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_NOTCON() As Long
            Get
                Return _OTC_NOTCON
            End Get
            Set(ByVal value As Long)
               _OTC_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_OTC_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_NOTEND() As Long
            Get
                Return _OTC_NOTEND
            End Get
            Set(ByVal value As Long)
               _OTC_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_NON_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_REGIS() As Long
            Get
                Return _NON_REGIS
            End Get
            Set(ByVal value As Long)
               _NON_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_NON_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_SERVE() As Long
            Get
                Return _NON_SERVE
            End Get
            Set(ByVal value As Long)
               _NON_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_NON_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_MISS_CALL() As Long
            Get
                Return _NON_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _NON_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_CANCEL() As Long
            Get
                Return _NON_CANCEL
            End Get
            Set(ByVal value As Long)
               _NON_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOTCALL() As Long
            Get
                Return _NON_NOTCALL
            End Get
            Set(ByVal value As Long)
               _NON_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOTCON() As Long
            Get
                Return _NON_NOTCON
            End Get
            Set(ByVal value As Long)
               _NON_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOTEND() As Long
            Get
                Return _NON_NOTEND
            End Get
            Set(ByVal value As Long)
               _NON_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_REGIS() As Long
            Get
                Return _AIS3G_REGIS
            End Get
            Set(ByVal value As Long)
               _AIS3G_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_SERVE() As Long
            Get
                Return _AIS3G_SERVE
            End Get
            Set(ByVal value As Long)
               _AIS3G_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_MISS_CALL() As Long
            Get
                Return _AIS3G_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _AIS3G_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_CANCEL() As Long
            Get
                Return _AIS3G_CANCEL
            End Get
            Set(ByVal value As Long)
               _AIS3G_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_NOTCALL() As Long
            Get
                Return _AIS3G_NOTCALL
            End Get
            Set(ByVal value As Long)
               _AIS3G_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_NOTCON() As Long
            Get
                Return _AIS3G_NOTCON
            End Get
            Set(ByVal value As Long)
               _AIS3G_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_NOTEND() As Long
            Get
                Return _AIS3G_NOTEND
            End Get
            Set(ByVal value As Long)
               _AIS3G_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_REGIS() As Long
            Get
                Return _OTC3G_REGIS
            End Get
            Set(ByVal value As Long)
               _OTC3G_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_SERVE() As Long
            Get
                Return _OTC3G_SERVE
            End Get
            Set(ByVal value As Long)
               _OTC3G_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_MISS_CALL() As Long
            Get
                Return _OTC3G_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _OTC3G_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_CANCEL() As Long
            Get
                Return _OTC3G_CANCEL
            End Get
            Set(ByVal value As Long)
               _OTC3G_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_NOTCALL() As Long
            Get
                Return _OTC3G_NOTCALL
            End Get
            Set(ByVal value As Long)
               _OTC3G_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_NOTCON() As Long
            Get
                Return _OTC3G_NOTCON
            End Get
            Set(ByVal value As Long)
               _OTC3G_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_NOTEND() As Long
            Get
                Return _OTC3G_NOTEND
            End Get
            Set(ByVal value As Long)
               _OTC3G_NOTEND = value
            End Set
        End Property 


    End Class
End Namespace
