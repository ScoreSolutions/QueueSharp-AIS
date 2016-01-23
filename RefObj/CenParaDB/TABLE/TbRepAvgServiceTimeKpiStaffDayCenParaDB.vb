
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY table Parameter.
    '[Create by  on June, 8 2012]

    <Table(Name:="TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY")>  _
    Public Class TbRepAvgServiceTimeKpiStaffDayCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _USER_ID As Long = 0
        Dim _USER_CODE As String = ""
        Dim _USER_NAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SHOW_DATE As String = ""
        Dim _SHOW_DAY As String = ""
        Dim _SHOW_WEEK As String = ""
        Dim _SHOW_YEAR As Long = 0
        Dim _REGIS As Long = 0
        Dim _SERVED As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCEL As Long = 0
        Dim _SERVE_WITH_KPI As Long = 0
        Dim _PER_AWT_WITH_KPI As Double = 0
        Dim _PER_AWT_OVER_KPI As Double = 0
        Dim _PER_AHT_WITH_KPI As Double = 0
        Dim _PER_AHT_OVER_KPI As Double = 0
        Dim _PER_MISSED_CALL As Double = 0
        Dim _PER_CANCEL As Double = 0
        Dim _NOTCALL As Long = 0
        Dim _NOTCON As Long = 0
        Dim _NOTEND As Long = 0
        Dim _WAIT_WITH_KPI As Long = 0
        Dim _MAX_WT As String = ""
        Dim _MAX_HT As String = ""
        Dim _ACONT As Long = 0
        Dim _SCONT As Long = 0
        Dim _CCONT As Long = 0

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
        <Column(Storage:="_USER_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_ID() As Long
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As Long)
               _USER_ID = value
            End Set
        End Property 
        <Column(Storage:="_USER_CODE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_CODE() As String
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As String)
               _USER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_USER_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_NAME() As String
            Get
                Return _USER_NAME
            End Get
            Set(ByVal value As String)
               _USER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property STAFF_NAME() As String
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As String)
               _STAFF_NAME = value
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
        <Column(Storage:="_SHOW_DAY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_DAY() As String
            Get
                Return _SHOW_DAY
            End Get
            Set(ByVal value As String)
               _SHOW_DAY = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_WEEK", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_WEEK() As String
            Get
                Return _SHOW_WEEK
            End Get
            Set(ByVal value As String)
               _SHOW_WEEK = value
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
        <Column(Storage:="_REGIS", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
                _REGIS = value
            End Set
        End Property
        <Column(Storage:="_SERVED", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property SERVED() As Long
            Get
                Return _SERVED
            End Get
            Set(ByVal value As Long)
                _SERVED = value
            End Set
        End Property
        <Column(Storage:="_MISSED_CALL", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property MISSED_CALL() As Long
            Get
                Return _MISSED_CALL
            End Get
            Set(ByVal value As Long)
                _MISSED_CALL = value
            End Set
        End Property
        <Column(Storage:="_CANCEL", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property CANCEL() As Long
            Get
                Return _CANCEL
            End Get
            Set(ByVal value As Long)
                _CANCEL = value
            End Set
        End Property
        <Column(Storage:="_SERVE_WITH_KPI", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property SERVE_WITH_KPI() As Long
            Get
                Return _SERVE_WITH_KPI
            End Get
            Set(ByVal value As Long)
                _SERVE_WITH_KPI = value
            End Set
        End Property
        <Column(Storage:="_PER_AWT_WITH_KPI", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property PER_AWT_WITH_KPI() As Double
            Get
                Return _PER_AWT_WITH_KPI
            End Get
            Set(ByVal value As Double)
                _PER_AWT_WITH_KPI = value
            End Set
        End Property
        <Column(Storage:="_PER_AWT_OVER_KPI", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property PER_AWT_OVER_KPI() As Double
            Get
                Return _PER_AWT_OVER_KPI
            End Get
            Set(ByVal value As Double)
                _PER_AWT_OVER_KPI = value
            End Set
        End Property
        <Column(Storage:="_PER_AHT_WITH_KPI", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property PER_AHT_WITH_KPI() As Double
            Get
                Return _PER_AHT_WITH_KPI
            End Get
            Set(ByVal value As Double)
                _PER_AHT_WITH_KPI = value
            End Set
        End Property
        <Column(Storage:="_PER_AHT_OVER_KPI", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property PER_AHT_OVER_KPI() As Double
            Get
                Return _PER_AHT_OVER_KPI
            End Get
            Set(ByVal value As Double)
                _PER_AHT_OVER_KPI = value
            End Set
        End Property
        <Column(Storage:="_PER_MISSED_CALL", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property PER_MISSED_CALL() As Double
            Get
                Return _PER_MISSED_CALL
            End Get
            Set(ByVal value As Double)
                _PER_MISSED_CALL = value
            End Set
        End Property
        <Column(Storage:="_PER_CANCEL", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property PER_CANCEL() As Double
            Get
                Return _PER_CANCEL
            End Get
            Set(ByVal value As Double)
                _PER_CANCEL = value
            End Set
        End Property
        <Column(Storage:="_NOTCALL", DbType:="Int NOT NULL ", CanBeNull:=False)> _
         Public Property NOTCALL() As Long
            Get
                Return _NOTCALL
            End Get
            Set(ByVal value As Long)
                _NOTCALL = value
            End Set
        End Property
        <Column(Storage:="_NOTCON", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property NOTCON() As Long
            Get
                Return _NOTCON
            End Get
            Set(ByVal value As Long)
                _NOTCON = value
            End Set
        End Property
        <Column(Storage:="_NOTEND", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property NOTEND() As Long
            Get
                Return _NOTEND
            End Get
            Set(ByVal value As Long)
                _NOTEND = value
            End Set
        End Property
        <Column(Storage:="_WAIT_WITH_KPI", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property WAIT_WITH_KPI() As Long
            Get
                Return _WAIT_WITH_KPI
            End Get
            Set(ByVal value As Long)
                _WAIT_WITH_KPI = value
            End Set
        End Property
        <Column(Storage:="_MAX_WT", DbType:="VarChar(50)")> _
        Public Property MAX_WT() As String
            Get
                Return _MAX_WT
            End Get
            Set(ByVal value As String)
                _MAX_WT = value
            End Set
        End Property
        <Column(Storage:="_MAX_HT", DbType:="VarChar(50)")> _
        Public Property MAX_HT() As String
            Get
                Return _MAX_HT
            End Get
            Set(ByVal value As String)
                _MAX_HT = value
            End Set
        End Property
        <Column(Storage:="_ACONT", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property ACONT() As Long
            Get
                Return _ACONT
            End Get
            Set(ByVal value As Long)
                _ACONT = value
            End Set
        End Property
        <Column(Storage:="_SCONT", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property SCONT() As Long
            Get
                Return _SCONT
            End Get
            Set(ByVal value As Long)
                _SCONT = value
            End Set
        End Property
        <Column(Storage:="_CCONT", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property CCONT() As Long
            Get
                Return _CCONT
            End Get
            Set(ByVal value As Long)
                _CCONT = value
            End Set
        End Property
    End Class
End Namespace
