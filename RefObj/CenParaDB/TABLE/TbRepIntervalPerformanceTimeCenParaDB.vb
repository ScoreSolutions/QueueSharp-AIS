
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_INTERVAL_PERFORMANCE_TIME table Parameter.
    '[Create by  on January, 28 2015]

    <Table(Name:="TB_REP_INTERVAL_PERFORMANCE_TIME")>  _
    Public Class TbRepIntervalPerformanceTimeCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _REGION_NAME As String = ""
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _NETWORK_TYPE As String = ""
        Dim _CUSTOMERTYPE_NAME As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME As String = ""
        Dim _INTERVAL_MINUTE As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _TIME_PRIOD_FROM As DateTime = New DateTime(1,1,1)
        Dim _TIME_PRIOD_TO As DateTime = New DateTime(1,1,1)
        Dim _SHOW_TIME As String = ""
        Dim _REGIS As Long = 0
        Dim _SERVED As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCEL As Long = 0
        Dim _NOT_CALL As Long = 0
        Dim _NOT_CON As Long = 0
        Dim _NOT_END As Long = 0
        Dim _WAIT_WITH_KPI As Long = 0
        Dim _SERVE_WITH_KPI As Long = 0
        Dim _MAX_WT As Long = 0
        Dim _MAX_HT As Long = 0
        Dim _AWT As Long = 0
        Dim _AHT As Long = 0
        Dim _COUNT_WT As Long = 0
        Dim _SUM_WT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _SUM_HT As Long = 0

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
        <Column(Storage:="_REGION_NAME", DbType:="VarChar(3) NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_NAME() As String
            Get
                Return _REGION_NAME
            End Get
            Set(ByVal value As String)
               _REGION_NAME = value
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
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property NETWORK_TYPE() As String
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As String)
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_NAME", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property CUSTOMERTYPE_NAME() As String
            Get
                Return _CUSTOMERTYPE_NAME
            End Get
            Set(ByVal value As String)
               _CUSTOMERTYPE_NAME = value
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
        <Column(Storage:="_SERVICE_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME() As String
            Get
                Return _SERVICE_NAME
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME = value
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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
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
        <Column(Storage:="_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
               _REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERVED", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVED() As Long
            Get
                Return _SERVED
            End Get
            Set(ByVal value As Long)
               _SERVED = value
            End Set
        End Property 
        <Column(Storage:="_MISSED_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MISSED_CALL() As Long
            Get
                Return _MISSED_CALL
            End Get
            Set(ByVal value As Long)
               _MISSED_CALL = value
            End Set
        End Property 
        <Column(Storage:="_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCEL() As Long
            Get
                Return _CANCEL
            End Get
            Set(ByVal value As Long)
               _CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CALL() As Long
            Get
                Return _NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CON() As Long
            Get
                Return _NOT_CON
            End Get
            Set(ByVal value As Long)
               _NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_END() As Long
            Get
                Return _NOT_END
            End Get
            Set(ByVal value As Long)
               _NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_WAIT_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WAIT_WITH_KPI() As Long
            Get
                Return _WAIT_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _WAIT_WITH_KPI = value
            End Set
        End Property 
        <Column(Storage:="_SERVE_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE_WITH_KPI() As Long
            Get
                Return _SERVE_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _SERVE_WITH_KPI = value
            End Set
        End Property 
        <Column(Storage:="_MAX_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_WT() As Long
            Get
                Return _MAX_WT
            End Get
            Set(ByVal value As Long)
               _MAX_WT = value
            End Set
        End Property 
        <Column(Storage:="_MAX_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_HT() As Long
            Get
                Return _MAX_HT
            End Get
            Set(ByVal value As Long)
               _MAX_HT = value
            End Set
        End Property 
        <Column(Storage:="_AWT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AWT() As Long
            Get
                Return _AWT
            End Get
            Set(ByVal value As Long)
               _AWT = value
            End Set
        End Property 
        <Column(Storage:="_AHT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AHT() As Long
            Get
                Return _AHT
            End Get
            Set(ByVal value As Long)
               _AHT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_WT() As Long
            Get
                Return _COUNT_WT
            End Get
            Set(ByVal value As Long)
               _COUNT_WT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_WT() As Long
            Get
                Return _SUM_WT
            End Get
            Set(ByVal value As Long)
               _SUM_WT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_HT() As Long
            Get
                Return _COUNT_HT
            End Get
            Set(ByVal value As Long)
               _COUNT_HT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_HT() As Long
            Get
                Return _SUM_HT
            End Get
            Set(ByVal value As Long)
               _SUM_HT = value
            End Set
        End Property 


    End Class
End Namespace
