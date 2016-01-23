
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_CUST_CATEGORY_TIME table Parameter.
    '[Create by  on March, 27 2013]

    <Table(Name:="TB_REP_CUST_CATEGORY_TIME")>  _
    Public Class TbRepCustCategoryTimeCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SHOW_DATE As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _INTERVAL_MINUTE As Long = 0
        Dim _TIME_PRIOD_FROM As DateTime = New DateTime(1,1,1)
        Dim _TIME_PRIOD_TO As DateTime = New DateTime(1,1,1)
        Dim _SHOW_TIME As String = ""
        Dim _KEY_REGIS As Long = 0
        Dim _KEY_SERVE As Long = 0
        Dim _KEY_MISS_CALL As Long = 0
        Dim _KEY_CANCEL As Long = 0
        Dim _KEY_NOT_CALL As Long = 0
        Dim _KEY_NOT_CON As Long = 0
        Dim _KEY_NOT_END As Long = 0
        Dim _SME_REGIS As Long = 0
        Dim _SME_SERVE As Long = 0
        Dim _SME_MISS_CALL As Long = 0
        Dim _SME_CANCEL As Long = 0
        Dim _SME_NOT_CALL As Long = 0
        Dim _SME_NOT_CON As Long = 0
        Dim _SME_NOT_END As Long = 0
        Dim _ROY_REGIS As Long = 0
        Dim _ROY_SERVE As Long = 0
        Dim _ROY_MISS_CALL As Long = 0
        Dim _ROY_CANCEL As Long = 0
        Dim _ROY_NOT_CALL As Long = 0
        Dim _ROY_NOT_CON As Long = 0
        Dim _ROY_NOT_END As Long = 0
        Dim _TOT_REGIS As Long = 0
        Dim _TOT_SERVE As Long = 0
        Dim _TOT_MISS_CALL As Long = 0
        Dim _TOT_CANCEL As Long = 0
        Dim _TOT_NOT_CALL As Long = 0
        Dim _TOT_NOT_CON As Long = 0
        Dim _TOT_NOT_END As Long = 0
        Dim _GOV_REGIS As Long = 0
        Dim _GOV_SERVE As Long = 0
        Dim _GOV_MISS_CALL As Long = 0
        Dim _GOV_CANCEL As Long = 0
        Dim _GOV_NOT_CALL As Long = 0
        Dim _GOV_NOT_CON As Long = 0
        Dim _GOV_NOT_END As Long = 0
        Dim _EMB_REGIS As Long = 0
        Dim _EMB_SERVE As Long = 0
        Dim _EMB_MISS_CALL As Long = 0
        Dim _EMB_CANCEL As Long = 0
        Dim _EMB_NOT_CALL As Long = 0
        Dim _EMB_NOT_CON As Long = 0
        Dim _EMB_NOT_END As Long = 0
        Dim _NON_REGIS As Long = 0
        Dim _NON_SERVE As Long = 0
        Dim _NON_MISS_CALL As Long = 0
        Dim _NON_CANCEL As Long = 0
        Dim _NON_NOT_CALL As Long = 0
        Dim _NON_NOT_CON As Long = 0
        Dim _NON_NOT_END As Long = 0
        Dim _STA_REGIS As Long = 0
        Dim _STA_SERVE As Long = 0
        Dim _STA_MISS_CALL As Long = 0
        Dim _STA_CANCEL As Long = 0
        Dim _STA_NOT_CALL As Long = 0
        Dim _STA_NOT_CON As Long = 0
        Dim _STA_NOT_END As Long = 0
        Dim _AIS_REGIS As Long = 0
        Dim _AIS_SERVE As Long = 0
        Dim _AIS_MISS_CALL As Long = 0
        Dim _AIS_CANCEL As Long = 0
        Dim _AIS_NOT_CALL As Long = 0
        Dim _AIS_NOT_CON As Long = 0
        Dim _AIS_NOT_END As Long = 0
        Dim _PRE_REGIS As Long = 0
        Dim _PRE_SERVE As Long = 0
        Dim _PRE_MISS_CALL As Long = 0
        Dim _PRE_CANCEL As Long = 0
        Dim _PRE_NOT_CALL As Long = 0
        Dim _PRE_NOT_CON As Long = 0
        Dim _PRE_NOT_END As Long = 0
        Dim _FOR_REGIS As Long = 0
        Dim _FOR_SERVE As Long = 0
        Dim _FOR_MISS_CALL As Long = 0
        Dim _FOR_CANCEL As Long = 0
        Dim _FOR_NOT_CALL As Long = 0
        Dim _FOR_NOT_CON As Long = 0
        Dim _FOR_NOT_END As Long = 0
        Dim _THA_REGIS As Long = 0
        Dim _THA_SERVE As Long = 0
        Dim _THA_MISS_CALL As Long = 0
        Dim _THA_CANCEL As Long = 0
        Dim _THA_NOT_CALL As Long = 0
        Dim _THA_NOT_CON As Long = 0
        Dim _THA_NOT_END As Long = 0
        Dim _NO_REGIS As Long = 0
        Dim _NO_SERVE As Long = 0
        Dim _NO_MISS_CALL As Long = 0
        Dim _NO_CANCEL As Long = 0
        Dim _NO_NOT_CALL As Long = 0
        Dim _NO_NOT_CON As Long = 0
        Dim _NO_NOT_END As Long = 0

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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_DATE", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_INTERVAL_MINUTE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property INTERVAL_MINUTE() As Long
            Get
                Return _INTERVAL_MINUTE
            End Get
            Set(ByVal value As Long)
               _INTERVAL_MINUTE = value
            End Set
        End Property 
        <Column(Storage:="_TIME_PRIOD_FROM", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TIME_PRIOD_FROM() As DateTime
            Get
                Return _TIME_PRIOD_FROM
            End Get
            Set(ByVal value As DateTime)
               _TIME_PRIOD_FROM = value
            End Set
        End Property 
        <Column(Storage:="_TIME_PRIOD_TO", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TIME_PRIOD_TO() As DateTime
            Get
                Return _TIME_PRIOD_TO
            End Get
            Set(ByVal value As DateTime)
               _TIME_PRIOD_TO = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_TIME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_TIME() As String
            Get
                Return _SHOW_TIME
            End Get
            Set(ByVal value As String)
               _SHOW_TIME = value
            End Set
        End Property 
        <Column(Storage:="_KEY_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_REGIS() As Long
            Get
                Return _KEY_REGIS
            End Get
            Set(ByVal value As Long)
               _KEY_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_KEY_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_SERVE() As Long
            Get
                Return _KEY_SERVE
            End Get
            Set(ByVal value As Long)
               _KEY_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_KEY_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_MISS_CALL() As Long
            Get
                Return _KEY_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _KEY_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_KEY_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_CANCEL() As Long
            Get
                Return _KEY_CANCEL
            End Get
            Set(ByVal value As Long)
               _KEY_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_KEY_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_NOT_CALL() As Long
            Get
                Return _KEY_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _KEY_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_KEY_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_NOT_CON() As Long
            Get
                Return _KEY_NOT_CON
            End Get
            Set(ByVal value As Long)
               _KEY_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_KEY_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_NOT_END() As Long
            Get
                Return _KEY_NOT_END
            End Get
            Set(ByVal value As Long)
               _KEY_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_SME_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_REGIS() As Long
            Get
                Return _SME_REGIS
            End Get
            Set(ByVal value As Long)
               _SME_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SME_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_SERVE() As Long
            Get
                Return _SME_SERVE
            End Get
            Set(ByVal value As Long)
               _SME_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_SME_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_MISS_CALL() As Long
            Get
                Return _SME_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _SME_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_SME_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_CANCEL() As Long
            Get
                Return _SME_CANCEL
            End Get
            Set(ByVal value As Long)
               _SME_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_SME_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_NOT_CALL() As Long
            Get
                Return _SME_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _SME_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_SME_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_NOT_CON() As Long
            Get
                Return _SME_NOT_CON
            End Get
            Set(ByVal value As Long)
               _SME_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_SME_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_NOT_END() As Long
            Get
                Return _SME_NOT_END
            End Get
            Set(ByVal value As Long)
               _SME_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_ROY_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_REGIS() As Long
            Get
                Return _ROY_REGIS
            End Get
            Set(ByVal value As Long)
               _ROY_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_ROY_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_SERVE() As Long
            Get
                Return _ROY_SERVE
            End Get
            Set(ByVal value As Long)
               _ROY_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_ROY_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_MISS_CALL() As Long
            Get
                Return _ROY_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _ROY_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_ROY_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_CANCEL() As Long
            Get
                Return _ROY_CANCEL
            End Get
            Set(ByVal value As Long)
               _ROY_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_ROY_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_NOT_CALL() As Long
            Get
                Return _ROY_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _ROY_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_ROY_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_NOT_CON() As Long
            Get
                Return _ROY_NOT_CON
            End Get
            Set(ByVal value As Long)
               _ROY_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_ROY_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_NOT_END() As Long
            Get
                Return _ROY_NOT_END
            End Get
            Set(ByVal value As Long)
               _ROY_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_TOT_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_REGIS() As Long
            Get
                Return _TOT_REGIS
            End Get
            Set(ByVal value As Long)
               _TOT_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_TOT_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_SERVE() As Long
            Get
                Return _TOT_SERVE
            End Get
            Set(ByVal value As Long)
               _TOT_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_TOT_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_MISS_CALL() As Long
            Get
                Return _TOT_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _TOT_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_TOT_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_CANCEL() As Long
            Get
                Return _TOT_CANCEL
            End Get
            Set(ByVal value As Long)
               _TOT_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_TOT_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_NOT_CALL() As Long
            Get
                Return _TOT_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _TOT_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_TOT_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_NOT_CON() As Long
            Get
                Return _TOT_NOT_CON
            End Get
            Set(ByVal value As Long)
               _TOT_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_TOT_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_NOT_END() As Long
            Get
                Return _TOT_NOT_END
            End Get
            Set(ByVal value As Long)
               _TOT_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_GOV_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_REGIS() As Long
            Get
                Return _GOV_REGIS
            End Get
            Set(ByVal value As Long)
               _GOV_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_GOV_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_SERVE() As Long
            Get
                Return _GOV_SERVE
            End Get
            Set(ByVal value As Long)
               _GOV_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_GOV_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_MISS_CALL() As Long
            Get
                Return _GOV_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _GOV_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_GOV_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_CANCEL() As Long
            Get
                Return _GOV_CANCEL
            End Get
            Set(ByVal value As Long)
               _GOV_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_GOV_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_NOT_CALL() As Long
            Get
                Return _GOV_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _GOV_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_GOV_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_NOT_CON() As Long
            Get
                Return _GOV_NOT_CON
            End Get
            Set(ByVal value As Long)
               _GOV_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_GOV_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_NOT_END() As Long
            Get
                Return _GOV_NOT_END
            End Get
            Set(ByVal value As Long)
               _GOV_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_EMB_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_REGIS() As Long
            Get
                Return _EMB_REGIS
            End Get
            Set(ByVal value As Long)
               _EMB_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_EMB_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_SERVE() As Long
            Get
                Return _EMB_SERVE
            End Get
            Set(ByVal value As Long)
               _EMB_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_EMB_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_MISS_CALL() As Long
            Get
                Return _EMB_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _EMB_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_EMB_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_CANCEL() As Long
            Get
                Return _EMB_CANCEL
            End Get
            Set(ByVal value As Long)
               _EMB_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_EMB_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_NOT_CALL() As Long
            Get
                Return _EMB_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _EMB_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_EMB_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_NOT_CON() As Long
            Get
                Return _EMB_NOT_CON
            End Get
            Set(ByVal value As Long)
               _EMB_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_EMB_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_NOT_END() As Long
            Get
                Return _EMB_NOT_END
            End Get
            Set(ByVal value As Long)
               _EMB_NOT_END = value
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
        <Column(Storage:="_NON_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOT_CALL() As Long
            Get
                Return _NON_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NON_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOT_CON() As Long
            Get
                Return _NON_NOT_CON
            End Get
            Set(ByVal value As Long)
               _NON_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOT_END() As Long
            Get
                Return _NON_NOT_END
            End Get
            Set(ByVal value As Long)
               _NON_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_STA_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_REGIS() As Long
            Get
                Return _STA_REGIS
            End Get
            Set(ByVal value As Long)
               _STA_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_STA_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_SERVE() As Long
            Get
                Return _STA_SERVE
            End Get
            Set(ByVal value As Long)
               _STA_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_STA_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_MISS_CALL() As Long
            Get
                Return _STA_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _STA_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_STA_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_CANCEL() As Long
            Get
                Return _STA_CANCEL
            End Get
            Set(ByVal value As Long)
               _STA_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_STA_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_NOT_CALL() As Long
            Get
                Return _STA_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _STA_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_STA_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_NOT_CON() As Long
            Get
                Return _STA_NOT_CON
            End Get
            Set(ByVal value As Long)
               _STA_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_STA_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_NOT_END() As Long
            Get
                Return _STA_NOT_END
            End Get
            Set(ByVal value As Long)
               _STA_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_AIS_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_REGIS() As Long
            Get
                Return _AIS_REGIS
            End Get
            Set(ByVal value As Long)
               _AIS_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_AIS_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_SERVE() As Long
            Get
                Return _AIS_SERVE
            End Get
            Set(ByVal value As Long)
               _AIS_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_AIS_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_MISS_CALL() As Long
            Get
                Return _AIS_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _AIS_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_CANCEL() As Long
            Get
                Return _AIS_CANCEL
            End Get
            Set(ByVal value As Long)
               _AIS_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_AIS_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_NOT_CALL() As Long
            Get
                Return _AIS_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _AIS_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_NOT_CON() As Long
            Get
                Return _AIS_NOT_CON
            End Get
            Set(ByVal value As Long)
               _AIS_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_AIS_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_NOT_END() As Long
            Get
                Return _AIS_NOT_END
            End Get
            Set(ByVal value As Long)
               _AIS_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_PRE_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_REGIS() As Long
            Get
                Return _PRE_REGIS
            End Get
            Set(ByVal value As Long)
               _PRE_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_PRE_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_SERVE() As Long
            Get
                Return _PRE_SERVE
            End Get
            Set(ByVal value As Long)
               _PRE_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_PRE_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_MISS_CALL() As Long
            Get
                Return _PRE_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _PRE_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_PRE_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_CANCEL() As Long
            Get
                Return _PRE_CANCEL
            End Get
            Set(ByVal value As Long)
               _PRE_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_PRE_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_NOT_CALL() As Long
            Get
                Return _PRE_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _PRE_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_PRE_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_NOT_CON() As Long
            Get
                Return _PRE_NOT_CON
            End Get
            Set(ByVal value As Long)
               _PRE_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_PRE_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_NOT_END() As Long
            Get
                Return _PRE_NOT_END
            End Get
            Set(ByVal value As Long)
               _PRE_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_FOR_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_REGIS() As Long
            Get
                Return _FOR_REGIS
            End Get
            Set(ByVal value As Long)
               _FOR_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_FOR_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_SERVE() As Long
            Get
                Return _FOR_SERVE
            End Get
            Set(ByVal value As Long)
               _FOR_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_FOR_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_MISS_CALL() As Long
            Get
                Return _FOR_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _FOR_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_FOR_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_CANCEL() As Long
            Get
                Return _FOR_CANCEL
            End Get
            Set(ByVal value As Long)
               _FOR_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_FOR_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_NOT_CALL() As Long
            Get
                Return _FOR_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _FOR_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_FOR_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_NOT_CON() As Long
            Get
                Return _FOR_NOT_CON
            End Get
            Set(ByVal value As Long)
               _FOR_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_FOR_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_NOT_END() As Long
            Get
                Return _FOR_NOT_END
            End Get
            Set(ByVal value As Long)
               _FOR_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_THA_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_REGIS() As Long
            Get
                Return _THA_REGIS
            End Get
            Set(ByVal value As Long)
               _THA_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_THA_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_SERVE() As Long
            Get
                Return _THA_SERVE
            End Get
            Set(ByVal value As Long)
               _THA_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_THA_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_MISS_CALL() As Long
            Get
                Return _THA_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _THA_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_THA_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_CANCEL() As Long
            Get
                Return _THA_CANCEL
            End Get
            Set(ByVal value As Long)
               _THA_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_THA_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_NOT_CALL() As Long
            Get
                Return _THA_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _THA_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_THA_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_NOT_CON() As Long
            Get
                Return _THA_NOT_CON
            End Get
            Set(ByVal value As Long)
               _THA_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_THA_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_NOT_END() As Long
            Get
                Return _THA_NOT_END
            End Get
            Set(ByVal value As Long)
               _THA_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_NO_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_REGIS() As Long
            Get
                Return _NO_REGIS
            End Get
            Set(ByVal value As Long)
               _NO_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_NO_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_SERVE() As Long
            Get
                Return _NO_SERVE
            End Get
            Set(ByVal value As Long)
               _NO_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_NO_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_MISS_CALL() As Long
            Get
                Return _NO_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _NO_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NO_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_CANCEL() As Long
            Get
                Return _NO_CANCEL
            End Get
            Set(ByVal value As Long)
               _NO_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NO_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_NOT_CALL() As Long
            Get
                Return _NO_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NO_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NO_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_NOT_CON() As Long
            Get
                Return _NO_NOT_CON
            End Get
            Set(ByVal value As Long)
               _NO_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NO_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_NOT_END() As Long
            Get
                Return _NO_NOT_END
            End Get
            Set(ByVal value As Long)
               _NO_NOT_END = value
            End Set
        End Property 


    End Class
End Namespace
