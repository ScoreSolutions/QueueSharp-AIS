
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_STAFF_ATTENDANCE_MONTH table Parameter.
    '[Create by  on July, 30 2014]

    <Table(Name:="TB_REP_STAFF_ATTENDANCE_MONTH")>  _
    Public Class TbRepStaffAttendanceMonthCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _MONTH_NO As Long = 0
        Dim _SHOW_MONTH As String = ""
        Dim _SHOW_YEAR As Long = 0
        Dim _USER_ID As Long = 0
        Dim _USER_CODE As String = ""
        Dim _USERNAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _WORKING_DAY As Long = 0
        Dim _LOG_IN As  String  = ""
        Dim _LOG_OUT As  String  = ""
        Dim _TOTAL_TIME As  String  = ""
        Dim _SERVICE_TIME As String = ""
        Dim _PRODUCTIVITY As String = ""
        Dim _NON_PRODUCTIVITY As String = ""
        Dim _PROD_LEARNING As String = ""
        Dim _PROD_STAND_BY As String = ""
        Dim _PROD_BRIEF As String = ""
        Dim _PROD_WARP_UP As String = ""
        Dim _PROD_CONSULT As String = ""
        Dim _PROD_OTHER As String = ""
        Dim _TOTAL_PRODUCTIVITY As String = ""
        Dim _NPROD_LUNCH As String = ""
        Dim _NPROD_LEAVE As String = ""
        Dim _NPROD_CHANGE_COUNTER As String = ""
        Dim _NPROD_HOME As String = ""
        Dim _NPROD_MINI_BREAK As String = ""
        Dim _NPROD_RESTROOM As String = ""
        Dim _NPROD_OTHER As String = ""
        Dim _TOTAL_NON_PRODUCTIVITY As String = ""
        Dim _SUM_TOTAL_TIME As Long = 0
        Dim _SUM_SERVICE_TIME As Long = 0
        Dim _SUM_PRODUCTIVITY As Long = 0
        Dim _SUM_NON_PRODUCTIVITY As Long = 0
        Dim _SUM_EST_OT As Long = 0
        Dim _SUM_PROD_LEARNING As Long = 0
        Dim _SUM_PROD_STAND_BY As Long = 0
        Dim _SUM_PROD_BRIEF As Long = 0
        Dim _SUM_PROD_WARP_UP As Long = 0
        Dim _SUM_PROD_CONSULT As Long = 0
        Dim _SUM_PROD_OTHER As Long = 0
        Dim _SUM_NPROD_LUNCH As Long = 0
        Dim _SUM_NPROD_LEAVE As Long = 0
        Dim _SUM_NPROD_CHANGE_COUNTER As Long = 0
        Dim _SUM_NPROD_HOME As Long = 0
        Dim _SUM_NPROD_MINI_BREAK As Long = 0
        Dim _SUM_NPROD_RESTROOM As Long = 0
        Dim _SUM_NPROD_OTHER As Long = 0
        Dim _COUNT_PRODUCTIVITY As Long = 0
        Dim _COUNT_NON_PRODUCTIVITY As Long = 0
        Dim _COUNT_EST_OT As Long = 0
        Dim _COUNT_PROD_LEARNING As Long = 0
        Dim _COUNT_PROD_STAND_BY As Long = 0
        Dim _COUNT_PROD_BRIEF As Long = 0
        Dim _COUNT_PROD_WARP_UP As Long = 0
        Dim _COUNT_PROD_CONSULT As Long = 0
        Dim _COUNT_PROD_OTHER As Long = 0
        Dim _COUNT_NPROD_LUNCH As Long = 0
        Dim _COUNT_NPROD_LEAVE As Long = 0
        Dim _COUNT_NPROD_CHANGE_COUNTER As Long = 0
        Dim _COUNT_NPROD_HOME As Long = 0
        Dim _COUNT_NPROD_MINI_BREAK As Long = 0
        Dim _COUNT_NPROD_RESTROOM As Long = 0
        Dim _COUNT_NPROD_OTHER As Long = 0
        Dim _EST_OT As String = "00:00:00"

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
        <Column(Storage:="_MONTH_NO", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MONTH_NO() As Long
            Get
                Return _MONTH_NO
            End Get
            Set(ByVal value As Long)
               _MONTH_NO = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_MONTH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_MONTH() As String
            Get
                Return _SHOW_MONTH
            End Get
            Set(ByVal value As String)
               _SHOW_MONTH = value
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
        <Column(Storage:="_USERNAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
               _USERNAME = value
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
        <Column(Storage:="_WORKING_DAY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WORKING_DAY() As Long
            Get
                Return _WORKING_DAY
            End Get
            Set(ByVal value As Long)
               _WORKING_DAY = value
            End Set
        End Property 
        <Column(Storage:="_LOG_IN", DbType:="VarChar(50)")>  _
        Public Property LOG_IN() As  String 
            Get
                Return _LOG_IN
            End Get
            Set(ByVal value As  String )
               _LOG_IN = value
            End Set
        End Property 
        <Column(Storage:="_LOG_OUT", DbType:="VarChar(50)")>  _
        Public Property LOG_OUT() As  String 
            Get
                Return _LOG_OUT
            End Get
            Set(ByVal value As  String )
               _LOG_OUT = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_TIME", DbType:="VarChar(50)")>  _
        Public Property TOTAL_TIME() As  String 
            Get
                Return _TOTAL_TIME
            End Get
            Set(ByVal value As  String )
               _TOTAL_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_TIME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_TIME() As String
            Get
                Return _SERVICE_TIME
            End Get
            Set(ByVal value As String)
               _SERVICE_TIME = value
            End Set
        End Property 
        <Column(Storage:="_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PRODUCTIVITY() As String
            Get
                Return _PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_NON_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_PRODUCTIVITY() As String
            Get
                Return _NON_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_PROD_LEARNING", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROD_LEARNING() As String
            Get
                Return _PROD_LEARNING
            End Get
            Set(ByVal value As String)
               _PROD_LEARNING = value
            End Set
        End Property 
        <Column(Storage:="_PROD_STAND_BY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROD_STAND_BY() As String
            Get
                Return _PROD_STAND_BY
            End Get
            Set(ByVal value As String)
               _PROD_STAND_BY = value
            End Set
        End Property 
        <Column(Storage:="_PROD_BRIEF", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROD_BRIEF() As String
            Get
                Return _PROD_BRIEF
            End Get
            Set(ByVal value As String)
               _PROD_BRIEF = value
            End Set
        End Property 
        <Column(Storage:="_PROD_WARP_UP", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROD_WARP_UP() As String
            Get
                Return _PROD_WARP_UP
            End Get
            Set(ByVal value As String)
               _PROD_WARP_UP = value
            End Set
        End Property 
        <Column(Storage:="_PROD_CONSULT", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROD_CONSULT() As String
            Get
                Return _PROD_CONSULT
            End Get
            Set(ByVal value As String)
               _PROD_CONSULT = value
            End Set
        End Property 
        <Column(Storage:="_PROD_OTHER", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROD_OTHER() As String
            Get
                Return _PROD_OTHER
            End Get
            Set(ByVal value As String)
               _PROD_OTHER = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_PRODUCTIVITY() As String
            Get
                Return _TOTAL_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _TOTAL_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_LUNCH", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_LUNCH() As String
            Get
                Return _NPROD_LUNCH
            End Get
            Set(ByVal value As String)
               _NPROD_LUNCH = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_LEAVE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_LEAVE() As String
            Get
                Return _NPROD_LEAVE
            End Get
            Set(ByVal value As String)
               _NPROD_LEAVE = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_CHANGE_COUNTER", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_CHANGE_COUNTER() As String
            Get
                Return _NPROD_CHANGE_COUNTER
            End Get
            Set(ByVal value As String)
               _NPROD_CHANGE_COUNTER = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_HOME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_HOME() As String
            Get
                Return _NPROD_HOME
            End Get
            Set(ByVal value As String)
               _NPROD_HOME = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_MINI_BREAK", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_MINI_BREAK() As String
            Get
                Return _NPROD_MINI_BREAK
            End Get
            Set(ByVal value As String)
               _NPROD_MINI_BREAK = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_RESTROOM", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_RESTROOM() As String
            Get
                Return _NPROD_RESTROOM
            End Get
            Set(ByVal value As String)
               _NPROD_RESTROOM = value
            End Set
        End Property 
        <Column(Storage:="_NPROD_OTHER", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NPROD_OTHER() As String
            Get
                Return _NPROD_OTHER
            End Get
            Set(ByVal value As String)
               _NPROD_OTHER = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_NON_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_NON_PRODUCTIVITY() As String
            Get
                Return _TOTAL_NON_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _TOTAL_NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_SUM_TOTAL_TIME", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_TOTAL_TIME() As Long
            Get
                Return _SUM_TOTAL_TIME
            End Get
            Set(ByVal value As Long)
               _SUM_TOTAL_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SUM_SERVICE_TIME", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_SERVICE_TIME() As Long
            Get
                Return _SUM_SERVICE_TIME
            End Get
            Set(ByVal value As Long)
               _SUM_SERVICE_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PRODUCTIVITY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PRODUCTIVITY() As Long
            Get
                Return _SUM_PRODUCTIVITY
            End Get
            Set(ByVal value As Long)
               _SUM_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NON_PRODUCTIVITY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NON_PRODUCTIVITY() As Long
            Get
                Return _SUM_NON_PRODUCTIVITY
            End Get
            Set(ByVal value As Long)
               _SUM_NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_SUM_EST_OT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_EST_OT() As Long
            Get
                Return _SUM_EST_OT
            End Get
            Set(ByVal value As Long)
               _SUM_EST_OT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PROD_LEARNING", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PROD_LEARNING() As Long
            Get
                Return _SUM_PROD_LEARNING
            End Get
            Set(ByVal value As Long)
               _SUM_PROD_LEARNING = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PROD_STAND_BY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PROD_STAND_BY() As Long
            Get
                Return _SUM_PROD_STAND_BY
            End Get
            Set(ByVal value As Long)
               _SUM_PROD_STAND_BY = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PROD_BRIEF", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PROD_BRIEF() As Long
            Get
                Return _SUM_PROD_BRIEF
            End Get
            Set(ByVal value As Long)
               _SUM_PROD_BRIEF = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PROD_WARP_UP", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PROD_WARP_UP() As Long
            Get
                Return _SUM_PROD_WARP_UP
            End Get
            Set(ByVal value As Long)
               _SUM_PROD_WARP_UP = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PROD_CONSULT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PROD_CONSULT() As Long
            Get
                Return _SUM_PROD_CONSULT
            End Get
            Set(ByVal value As Long)
               _SUM_PROD_CONSULT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_PROD_OTHER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_PROD_OTHER() As Long
            Get
                Return _SUM_PROD_OTHER
            End Get
            Set(ByVal value As Long)
               _SUM_PROD_OTHER = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_LUNCH", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_LUNCH() As Long
            Get
                Return _SUM_NPROD_LUNCH
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_LUNCH = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_LEAVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_LEAVE() As Long
            Get
                Return _SUM_NPROD_LEAVE
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_LEAVE = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_CHANGE_COUNTER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_CHANGE_COUNTER() As Long
            Get
                Return _SUM_NPROD_CHANGE_COUNTER
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_CHANGE_COUNTER = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_HOME", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_HOME() As Long
            Get
                Return _SUM_NPROD_HOME
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_HOME = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_MINI_BREAK", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_MINI_BREAK() As Long
            Get
                Return _SUM_NPROD_MINI_BREAK
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_MINI_BREAK = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_RESTROOM", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_RESTROOM() As Long
            Get
                Return _SUM_NPROD_RESTROOM
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_RESTROOM = value
            End Set
        End Property 
        <Column(Storage:="_SUM_NPROD_OTHER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_NPROD_OTHER() As Long
            Get
                Return _SUM_NPROD_OTHER
            End Get
            Set(ByVal value As Long)
               _SUM_NPROD_OTHER = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PRODUCTIVITY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PRODUCTIVITY() As Long
            Get
                Return _COUNT_PRODUCTIVITY
            End Get
            Set(ByVal value As Long)
               _COUNT_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NON_PRODUCTIVITY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NON_PRODUCTIVITY() As Long
            Get
                Return _COUNT_NON_PRODUCTIVITY
            End Get
            Set(ByVal value As Long)
               _COUNT_NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_EST_OT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_EST_OT() As Long
            Get
                Return _COUNT_EST_OT
            End Get
            Set(ByVal value As Long)
               _COUNT_EST_OT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PROD_LEARNING", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PROD_LEARNING() As Long
            Get
                Return _COUNT_PROD_LEARNING
            End Get
            Set(ByVal value As Long)
               _COUNT_PROD_LEARNING = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PROD_STAND_BY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PROD_STAND_BY() As Long
            Get
                Return _COUNT_PROD_STAND_BY
            End Get
            Set(ByVal value As Long)
               _COUNT_PROD_STAND_BY = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PROD_BRIEF", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PROD_BRIEF() As Long
            Get
                Return _COUNT_PROD_BRIEF
            End Get
            Set(ByVal value As Long)
               _COUNT_PROD_BRIEF = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PROD_WARP_UP", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PROD_WARP_UP() As Long
            Get
                Return _COUNT_PROD_WARP_UP
            End Get
            Set(ByVal value As Long)
               _COUNT_PROD_WARP_UP = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PROD_CONSULT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PROD_CONSULT() As Long
            Get
                Return _COUNT_PROD_CONSULT
            End Get
            Set(ByVal value As Long)
               _COUNT_PROD_CONSULT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_PROD_OTHER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_PROD_OTHER() As Long
            Get
                Return _COUNT_PROD_OTHER
            End Get
            Set(ByVal value As Long)
               _COUNT_PROD_OTHER = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_LUNCH", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_LUNCH() As Long
            Get
                Return _COUNT_NPROD_LUNCH
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_LUNCH = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_LEAVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_LEAVE() As Long
            Get
                Return _COUNT_NPROD_LEAVE
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_LEAVE = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_CHANGE_COUNTER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_CHANGE_COUNTER() As Long
            Get
                Return _COUNT_NPROD_CHANGE_COUNTER
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_CHANGE_COUNTER = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_HOME", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_HOME() As Long
            Get
                Return _COUNT_NPROD_HOME
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_HOME = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_MINI_BREAK", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_MINI_BREAK() As Long
            Get
                Return _COUNT_NPROD_MINI_BREAK
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_MINI_BREAK = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_RESTROOM", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_RESTROOM() As Long
            Get
                Return _COUNT_NPROD_RESTROOM
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_RESTROOM = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_NPROD_OTHER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_NPROD_OTHER() As Long
            Get
                Return _COUNT_NPROD_OTHER
            End Get
            Set(ByVal value As Long)
               _COUNT_NPROD_OTHER = value
            End Set
        End Property 
        <Column(Storage:="_EST_OT", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property EST_OT() As String
            Get
                Return _EST_OT
            End Get
            Set(ByVal value As String)
               _EST_OT = value
            End Set
        End Property 


    End Class
End Namespace
