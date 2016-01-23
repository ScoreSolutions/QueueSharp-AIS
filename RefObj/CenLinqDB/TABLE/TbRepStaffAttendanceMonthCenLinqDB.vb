Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = CenLinqDB.Common.Utilities.SQLDB
Imports CenParaDB.TABLE
Imports CenParaDB.Common.Utilities

Namespace TABLE
    'Represents a transaction for TB_REP_STAFF_ATTENDANCE_MONTH table CenLinqDB.
    '[Create by  on July, 30 2014]
    Public Class TbRepStaffAttendanceMonthCenLinqDB
        Public sub TbRepStaffAttendanceMonthCenLinqDB()

        End Sub 
        ' TB_REP_STAFF_ATTENDANCE_MONTH
        Const _tableName As String = "TB_REP_STAFF_ATTENDANCE_MONTH"
        Dim _deletedRow As Int16 = 0

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage As String
            Get
                Return _information
            End Get
        End Property
        Public ReadOnly Property HaveData As Boolean
            Get
                Return _haveData
            End Get
        End Property


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


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _SHOP_ID = 0
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _MONTH_NO = 0
            _SHOW_MONTH = ""
            _SHOW_YEAR = 0
            _USER_ID = 0
            _USER_CODE = ""
            _USERNAME = ""
            _STAFF_NAME = ""
            _WORKING_DAY = 0
            _LOG_IN = ""
            _LOG_OUT = ""
            _TOTAL_TIME = ""
            _SERVICE_TIME = ""
            _PRODUCTIVITY = ""
            _NON_PRODUCTIVITY = ""
            _PROD_LEARNING = ""
            _PROD_STAND_BY = ""
            _PROD_BRIEF = ""
            _PROD_WARP_UP = ""
            _PROD_CONSULT = ""
            _PROD_OTHER = ""
            _TOTAL_PRODUCTIVITY = ""
            _NPROD_LUNCH = ""
            _NPROD_LEAVE = ""
            _NPROD_CHANGE_COUNTER = ""
            _NPROD_HOME = ""
            _NPROD_MINI_BREAK = ""
            _NPROD_RESTROOM = ""
            _NPROD_OTHER = ""
            _TOTAL_NON_PRODUCTIVITY = ""
            _SUM_TOTAL_TIME = 0
            _SUM_SERVICE_TIME = 0
            _SUM_PRODUCTIVITY = 0
            _SUM_NON_PRODUCTIVITY = 0
            _SUM_EST_OT = 0
            _SUM_PROD_LEARNING = 0
            _SUM_PROD_STAND_BY = 0
            _SUM_PROD_BRIEF = 0
            _SUM_PROD_WARP_UP = 0
            _SUM_PROD_CONSULT = 0
            _SUM_PROD_OTHER = 0
            _SUM_NPROD_LUNCH = 0
            _SUM_NPROD_LEAVE = 0
            _SUM_NPROD_CHANGE_COUNTER = 0
            _SUM_NPROD_HOME = 0
            _SUM_NPROD_MINI_BREAK = 0
            _SUM_NPROD_RESTROOM = 0
            _SUM_NPROD_OTHER = 0
            _COUNT_PRODUCTIVITY = 0
            _COUNT_NON_PRODUCTIVITY = 0
            _COUNT_EST_OT = 0
            _COUNT_PROD_LEARNING = 0
            _COUNT_PROD_STAND_BY = 0
            _COUNT_PROD_BRIEF = 0
            _COUNT_PROD_WARP_UP = 0
            _COUNT_PROD_CONSULT = 0
            _COUNT_PROD_OTHER = 0
            _COUNT_NPROD_LUNCH = 0
            _COUNT_NPROD_LEAVE = 0
            _COUNT_NPROD_CHANGE_COUNTER = 0
            _COUNT_NPROD_HOME = 0
            _COUNT_NPROD_MINI_BREAK = 0
            _COUNT_NPROD_RESTROOM = 0
            _COUNT_NPROD_OTHER = 0
            _EST_OT = ""
        End Sub

       'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(whClause As String, orderBy As String, trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIF(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(Sql As String, trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _ID = DB.GetNextID("ID",tableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("ID = " & DB.SetDouble(_ID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cID As Long, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return doDelete("ID = " & DB.SetDouble(cID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the record of TB_REP_STAFF_ATTENDANCE_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_STAFF_ATTENDANCE_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepStaffAttendanceMonthCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_STAFF_ATTENDANCE_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepStaffAttendanceMonthCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_STAFF_ATTENDANCE_MONTH by specified MONTH_NO_SHOP_ID_SHOW_YEAR key is retrieved successfully.
        '/// <param name=cMONTH_NO_SHOP_ID_SHOW_YEAR>The MONTH_NO_SHOP_ID_SHOW_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMONTH_NO_SHOP_ID_SHOW_YEAR(cMONTH_NO As Integer, cSHOP_ID As Integer, cSHOW_YEAR As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("MONTH_NO = " & DB.SetDouble(cMONTH_NO) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_STAFF_ATTENDANCE_MONTH by specified MONTH_NO_SHOP_ID_SHOW_YEAR key is retrieved successfully.
        '/// <param name=cMONTH_NO_SHOP_ID_SHOW_YEAR>The MONTH_NO_SHOP_ID_SHOW_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMONTH_NO_SHOP_ID_SHOW_YEAR(cMONTH_NO As Integer, cSHOP_ID As Integer, cSHOW_YEAR As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MONTH_NO = " & DB.SetDouble(cMONTH_NO) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_STAFF_ATTENDANCE_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = False Then
                Try

                    ret = (DB.ExecuteNonQuery(SqlInsert, trans) > 0)
                    If ret = False Then
                        _error = DB.ErrorMessage
                    Else
                        _haveData = True
                    End If
                    _information = MessageResources.MSGIN001
                Catch ex As ApplicationException
                    ret = false
                    _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlInsert
                Catch ex As Exception
                    ret = False
                    _error = MessageResources.MSGEC101 & " Exception :" & ex.ToString() & "### SQL: " & SqlInsert
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEN002 & "### SQL: " & SqlInsert
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If _haveData = True Then
                If whText.Trim() <> ""
                    Try

                        ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = DB.ErrorMessage
                        End If
                        _information = MessageResources.MSGIU001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlUpdate & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC102 & " Exception :" & ex.ToString() & "### SQL: " & SqlUpdate & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGEU003 & "### SQL: " & SqlUpdate & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlUpdate & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_STAFF_ATTENDANCE_MONTH table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> ""
                    Try
                        ret = (DB.ExecuteNonQuery(SqlDelete & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = MessageResources.MSGED001
                        End If
                        _information = MessageResources.MSGID001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlDelete & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC103 & " Exception :" & ex.ToString() & "### SQL: " & SqlDelete & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGED003 & "### SQL: " & SqlDelete & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlDelete & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_REP_STAFF_ATTENDANCE_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " WHERE " & whText
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("month_no")) = False Then _month_no = Convert.ToInt32(Rdr("month_no"))
                        If Convert.IsDBNull(Rdr("show_month")) = False Then _show_month = Rdr("show_month").ToString()
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("working_day")) = False Then _working_day = Convert.ToInt32(Rdr("working_day"))
                        If Convert.IsDBNull(Rdr("log_in")) = False Then _log_in = Rdr("log_in").ToString()
                        If Convert.IsDBNull(Rdr("log_out")) = False Then _log_out = Rdr("log_out").ToString()
                        If Convert.IsDBNull(Rdr("total_time")) = False Then _total_time = Rdr("total_time").ToString()
                        If Convert.IsDBNull(Rdr("service_time")) = False Then _service_time = Rdr("service_time").ToString()
                        If Convert.IsDBNull(Rdr("productivity")) = False Then _productivity = Rdr("productivity").ToString()
                        If Convert.IsDBNull(Rdr("non_productivity")) = False Then _non_productivity = Rdr("non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("prod_learning")) = False Then _prod_learning = Rdr("prod_learning").ToString()
                        If Convert.IsDBNull(Rdr("prod_stand_by")) = False Then _prod_stand_by = Rdr("prod_stand_by").ToString()
                        If Convert.IsDBNull(Rdr("prod_brief")) = False Then _prod_brief = Rdr("prod_brief").ToString()
                        If Convert.IsDBNull(Rdr("prod_warp_up")) = False Then _prod_warp_up = Rdr("prod_warp_up").ToString()
                        If Convert.IsDBNull(Rdr("prod_consult")) = False Then _prod_consult = Rdr("prod_consult").ToString()
                        If Convert.IsDBNull(Rdr("prod_other")) = False Then _prod_other = Rdr("prod_other").ToString()
                        If Convert.IsDBNull(Rdr("total_productivity")) = False Then _total_productivity = Rdr("total_productivity").ToString()
                        If Convert.IsDBNull(Rdr("nprod_lunch")) = False Then _nprod_lunch = Rdr("nprod_lunch").ToString()
                        If Convert.IsDBNull(Rdr("nprod_leave")) = False Then _nprod_leave = Rdr("nprod_leave").ToString()
                        If Convert.IsDBNull(Rdr("nprod_change_counter")) = False Then _nprod_change_counter = Rdr("nprod_change_counter").ToString()
                        If Convert.IsDBNull(Rdr("nprod_home")) = False Then _nprod_home = Rdr("nprod_home").ToString()
                        If Convert.IsDBNull(Rdr("nprod_mini_break")) = False Then _nprod_mini_break = Rdr("nprod_mini_break").ToString()
                        If Convert.IsDBNull(Rdr("nprod_restroom")) = False Then _nprod_restroom = Rdr("nprod_restroom").ToString()
                        If Convert.IsDBNull(Rdr("nprod_other")) = False Then _nprod_other = Rdr("nprod_other").ToString()
                        If Convert.IsDBNull(Rdr("total_non_productivity")) = False Then _total_non_productivity = Rdr("total_non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("sum_total_time")) = False Then _sum_total_time = Convert.ToInt32(Rdr("sum_total_time"))
                        If Convert.IsDBNull(Rdr("sum_service_time")) = False Then _sum_service_time = Convert.ToInt32(Rdr("sum_service_time"))
                        If Convert.IsDBNull(Rdr("sum_productivity")) = False Then _sum_productivity = Convert.ToInt32(Rdr("sum_productivity"))
                        If Convert.IsDBNull(Rdr("sum_non_productivity")) = False Then _sum_non_productivity = Convert.ToInt32(Rdr("sum_non_productivity"))
                        If Convert.IsDBNull(Rdr("sum_est_ot")) = False Then _sum_est_ot = Convert.ToInt32(Rdr("sum_est_ot"))
                        If Convert.IsDBNull(Rdr("sum_prod_learning")) = False Then _sum_prod_learning = Convert.ToInt32(Rdr("sum_prod_learning"))
                        If Convert.IsDBNull(Rdr("sum_prod_stand_by")) = False Then _sum_prod_stand_by = Convert.ToInt32(Rdr("sum_prod_stand_by"))
                        If Convert.IsDBNull(Rdr("sum_prod_brief")) = False Then _sum_prod_brief = Convert.ToInt32(Rdr("sum_prod_brief"))
                        If Convert.IsDBNull(Rdr("sum_prod_warp_up")) = False Then _sum_prod_warp_up = Convert.ToInt32(Rdr("sum_prod_warp_up"))
                        If Convert.IsDBNull(Rdr("sum_prod_consult")) = False Then _sum_prod_consult = Convert.ToInt32(Rdr("sum_prod_consult"))
                        If Convert.IsDBNull(Rdr("sum_prod_other")) = False Then _sum_prod_other = Convert.ToInt32(Rdr("sum_prod_other"))
                        If Convert.IsDBNull(Rdr("sum_nprod_lunch")) = False Then _sum_nprod_lunch = Convert.ToInt32(Rdr("sum_nprod_lunch"))
                        If Convert.IsDBNull(Rdr("sum_nprod_leave")) = False Then _sum_nprod_leave = Convert.ToInt32(Rdr("sum_nprod_leave"))
                        If Convert.IsDBNull(Rdr("sum_nprod_change_counter")) = False Then _sum_nprod_change_counter = Convert.ToInt32(Rdr("sum_nprod_change_counter"))
                        If Convert.IsDBNull(Rdr("sum_nprod_home")) = False Then _sum_nprod_home = Convert.ToInt32(Rdr("sum_nprod_home"))
                        If Convert.IsDBNull(Rdr("sum_nprod_mini_break")) = False Then _sum_nprod_mini_break = Convert.ToInt32(Rdr("sum_nprod_mini_break"))
                        If Convert.IsDBNull(Rdr("sum_nprod_restroom")) = False Then _sum_nprod_restroom = Convert.ToInt32(Rdr("sum_nprod_restroom"))
                        If Convert.IsDBNull(Rdr("sum_nprod_other")) = False Then _sum_nprod_other = Convert.ToInt32(Rdr("sum_nprod_other"))
                        If Convert.IsDBNull(Rdr("count_productivity")) = False Then _count_productivity = Convert.ToInt32(Rdr("count_productivity"))
                        If Convert.IsDBNull(Rdr("count_non_productivity")) = False Then _count_non_productivity = Convert.ToInt32(Rdr("count_non_productivity"))
                        If Convert.IsDBNull(Rdr("count_est_ot")) = False Then _count_est_ot = Convert.ToInt32(Rdr("count_est_ot"))
                        If Convert.IsDBNull(Rdr("count_prod_learning")) = False Then _count_prod_learning = Convert.ToInt32(Rdr("count_prod_learning"))
                        If Convert.IsDBNull(Rdr("count_prod_stand_by")) = False Then _count_prod_stand_by = Convert.ToInt32(Rdr("count_prod_stand_by"))
                        If Convert.IsDBNull(Rdr("count_prod_brief")) = False Then _count_prod_brief = Convert.ToInt32(Rdr("count_prod_brief"))
                        If Convert.IsDBNull(Rdr("count_prod_warp_up")) = False Then _count_prod_warp_up = Convert.ToInt32(Rdr("count_prod_warp_up"))
                        If Convert.IsDBNull(Rdr("count_prod_consult")) = False Then _count_prod_consult = Convert.ToInt32(Rdr("count_prod_consult"))
                        If Convert.IsDBNull(Rdr("count_prod_other")) = False Then _count_prod_other = Convert.ToInt32(Rdr("count_prod_other"))
                        If Convert.IsDBNull(Rdr("count_nprod_lunch")) = False Then _count_nprod_lunch = Convert.ToInt32(Rdr("count_nprod_lunch"))
                        If Convert.IsDBNull(Rdr("count_nprod_leave")) = False Then _count_nprod_leave = Convert.ToInt32(Rdr("count_nprod_leave"))
                        If Convert.IsDBNull(Rdr("count_nprod_change_counter")) = False Then _count_nprod_change_counter = Convert.ToInt32(Rdr("count_nprod_change_counter"))
                        If Convert.IsDBNull(Rdr("count_nprod_home")) = False Then _count_nprod_home = Convert.ToInt32(Rdr("count_nprod_home"))
                        If Convert.IsDBNull(Rdr("count_nprod_mini_break")) = False Then _count_nprod_mini_break = Convert.ToInt32(Rdr("count_nprod_mini_break"))
                        If Convert.IsDBNull(Rdr("count_nprod_restroom")) = False Then _count_nprod_restroom = Convert.ToInt32(Rdr("count_nprod_restroom"))
                        If Convert.IsDBNull(Rdr("count_nprod_other")) = False Then _count_nprod_other = Convert.ToInt32(Rdr("count_nprod_other"))
                        If Convert.IsDBNull(Rdr("est_ot")) = False Then _est_ot = Rdr("est_ot").ToString()
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_REP_STAFF_ATTENDANCE_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepStaffAttendanceMonthCenLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("month_no")) = False Then _month_no = Convert.ToInt32(Rdr("month_no"))
                        If Convert.IsDBNull(Rdr("show_month")) = False Then _show_month = Rdr("show_month").ToString()
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("working_day")) = False Then _working_day = Convert.ToInt32(Rdr("working_day"))
                        If Convert.IsDBNull(Rdr("log_in")) = False Then _log_in = Rdr("log_in").ToString()
                        If Convert.IsDBNull(Rdr("log_out")) = False Then _log_out = Rdr("log_out").ToString()
                        If Convert.IsDBNull(Rdr("total_time")) = False Then _total_time = Rdr("total_time").ToString()
                        If Convert.IsDBNull(Rdr("service_time")) = False Then _service_time = Rdr("service_time").ToString()
                        If Convert.IsDBNull(Rdr("productivity")) = False Then _productivity = Rdr("productivity").ToString()
                        If Convert.IsDBNull(Rdr("non_productivity")) = False Then _non_productivity = Rdr("non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("prod_learning")) = False Then _prod_learning = Rdr("prod_learning").ToString()
                        If Convert.IsDBNull(Rdr("prod_stand_by")) = False Then _prod_stand_by = Rdr("prod_stand_by").ToString()
                        If Convert.IsDBNull(Rdr("prod_brief")) = False Then _prod_brief = Rdr("prod_brief").ToString()
                        If Convert.IsDBNull(Rdr("prod_warp_up")) = False Then _prod_warp_up = Rdr("prod_warp_up").ToString()
                        If Convert.IsDBNull(Rdr("prod_consult")) = False Then _prod_consult = Rdr("prod_consult").ToString()
                        If Convert.IsDBNull(Rdr("prod_other")) = False Then _prod_other = Rdr("prod_other").ToString()
                        If Convert.IsDBNull(Rdr("total_productivity")) = False Then _total_productivity = Rdr("total_productivity").ToString()
                        If Convert.IsDBNull(Rdr("nprod_lunch")) = False Then _nprod_lunch = Rdr("nprod_lunch").ToString()
                        If Convert.IsDBNull(Rdr("nprod_leave")) = False Then _nprod_leave = Rdr("nprod_leave").ToString()
                        If Convert.IsDBNull(Rdr("nprod_change_counter")) = False Then _nprod_change_counter = Rdr("nprod_change_counter").ToString()
                        If Convert.IsDBNull(Rdr("nprod_home")) = False Then _nprod_home = Rdr("nprod_home").ToString()
                        If Convert.IsDBNull(Rdr("nprod_mini_break")) = False Then _nprod_mini_break = Rdr("nprod_mini_break").ToString()
                        If Convert.IsDBNull(Rdr("nprod_restroom")) = False Then _nprod_restroom = Rdr("nprod_restroom").ToString()
                        If Convert.IsDBNull(Rdr("nprod_other")) = False Then _nprod_other = Rdr("nprod_other").ToString()
                        If Convert.IsDBNull(Rdr("total_non_productivity")) = False Then _total_non_productivity = Rdr("total_non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("sum_total_time")) = False Then _sum_total_time = Convert.ToInt32(Rdr("sum_total_time"))
                        If Convert.IsDBNull(Rdr("sum_service_time")) = False Then _sum_service_time = Convert.ToInt32(Rdr("sum_service_time"))
                        If Convert.IsDBNull(Rdr("sum_productivity")) = False Then _sum_productivity = Convert.ToInt32(Rdr("sum_productivity"))
                        If Convert.IsDBNull(Rdr("sum_non_productivity")) = False Then _sum_non_productivity = Convert.ToInt32(Rdr("sum_non_productivity"))
                        If Convert.IsDBNull(Rdr("sum_est_ot")) = False Then _sum_est_ot = Convert.ToInt32(Rdr("sum_est_ot"))
                        If Convert.IsDBNull(Rdr("sum_prod_learning")) = False Then _sum_prod_learning = Convert.ToInt32(Rdr("sum_prod_learning"))
                        If Convert.IsDBNull(Rdr("sum_prod_stand_by")) = False Then _sum_prod_stand_by = Convert.ToInt32(Rdr("sum_prod_stand_by"))
                        If Convert.IsDBNull(Rdr("sum_prod_brief")) = False Then _sum_prod_brief = Convert.ToInt32(Rdr("sum_prod_brief"))
                        If Convert.IsDBNull(Rdr("sum_prod_warp_up")) = False Then _sum_prod_warp_up = Convert.ToInt32(Rdr("sum_prod_warp_up"))
                        If Convert.IsDBNull(Rdr("sum_prod_consult")) = False Then _sum_prod_consult = Convert.ToInt32(Rdr("sum_prod_consult"))
                        If Convert.IsDBNull(Rdr("sum_prod_other")) = False Then _sum_prod_other = Convert.ToInt32(Rdr("sum_prod_other"))
                        If Convert.IsDBNull(Rdr("sum_nprod_lunch")) = False Then _sum_nprod_lunch = Convert.ToInt32(Rdr("sum_nprod_lunch"))
                        If Convert.IsDBNull(Rdr("sum_nprod_leave")) = False Then _sum_nprod_leave = Convert.ToInt32(Rdr("sum_nprod_leave"))
                        If Convert.IsDBNull(Rdr("sum_nprod_change_counter")) = False Then _sum_nprod_change_counter = Convert.ToInt32(Rdr("sum_nprod_change_counter"))
                        If Convert.IsDBNull(Rdr("sum_nprod_home")) = False Then _sum_nprod_home = Convert.ToInt32(Rdr("sum_nprod_home"))
                        If Convert.IsDBNull(Rdr("sum_nprod_mini_break")) = False Then _sum_nprod_mini_break = Convert.ToInt32(Rdr("sum_nprod_mini_break"))
                        If Convert.IsDBNull(Rdr("sum_nprod_restroom")) = False Then _sum_nprod_restroom = Convert.ToInt32(Rdr("sum_nprod_restroom"))
                        If Convert.IsDBNull(Rdr("sum_nprod_other")) = False Then _sum_nprod_other = Convert.ToInt32(Rdr("sum_nprod_other"))
                        If Convert.IsDBNull(Rdr("count_productivity")) = False Then _count_productivity = Convert.ToInt32(Rdr("count_productivity"))
                        If Convert.IsDBNull(Rdr("count_non_productivity")) = False Then _count_non_productivity = Convert.ToInt32(Rdr("count_non_productivity"))
                        If Convert.IsDBNull(Rdr("count_est_ot")) = False Then _count_est_ot = Convert.ToInt32(Rdr("count_est_ot"))
                        If Convert.IsDBNull(Rdr("count_prod_learning")) = False Then _count_prod_learning = Convert.ToInt32(Rdr("count_prod_learning"))
                        If Convert.IsDBNull(Rdr("count_prod_stand_by")) = False Then _count_prod_stand_by = Convert.ToInt32(Rdr("count_prod_stand_by"))
                        If Convert.IsDBNull(Rdr("count_prod_brief")) = False Then _count_prod_brief = Convert.ToInt32(Rdr("count_prod_brief"))
                        If Convert.IsDBNull(Rdr("count_prod_warp_up")) = False Then _count_prod_warp_up = Convert.ToInt32(Rdr("count_prod_warp_up"))
                        If Convert.IsDBNull(Rdr("count_prod_consult")) = False Then _count_prod_consult = Convert.ToInt32(Rdr("count_prod_consult"))
                        If Convert.IsDBNull(Rdr("count_prod_other")) = False Then _count_prod_other = Convert.ToInt32(Rdr("count_prod_other"))
                        If Convert.IsDBNull(Rdr("count_nprod_lunch")) = False Then _count_nprod_lunch = Convert.ToInt32(Rdr("count_nprod_lunch"))
                        If Convert.IsDBNull(Rdr("count_nprod_leave")) = False Then _count_nprod_leave = Convert.ToInt32(Rdr("count_nprod_leave"))
                        If Convert.IsDBNull(Rdr("count_nprod_change_counter")) = False Then _count_nprod_change_counter = Convert.ToInt32(Rdr("count_nprod_change_counter"))
                        If Convert.IsDBNull(Rdr("count_nprod_home")) = False Then _count_nprod_home = Convert.ToInt32(Rdr("count_nprod_home"))
                        If Convert.IsDBNull(Rdr("count_nprod_mini_break")) = False Then _count_nprod_mini_break = Convert.ToInt32(Rdr("count_nprod_mini_break"))
                        If Convert.IsDBNull(Rdr("count_nprod_restroom")) = False Then _count_nprod_restroom = Convert.ToInt32(Rdr("count_nprod_restroom"))
                        If Convert.IsDBNull(Rdr("count_nprod_other")) = False Then _count_nprod_other = Convert.ToInt32(Rdr("count_nprod_other"))
                        If Convert.IsDBNull(Rdr("est_ot")) = False Then _est_ot = Rdr("est_ot").ToString()
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function


        '/// Returns an indication whether the Class Data of TB_REP_STAFF_ATTENDANCE_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepStaffAttendanceMonthCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepStaffAttendanceMonthCenParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then ret.shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then ret.shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then ret.shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("month_no")) = False Then ret.month_no = Convert.ToInt32(Rdr("month_no"))
                        If Convert.IsDBNull(Rdr("show_month")) = False Then ret.show_month = Rdr("show_month").ToString()
                        If Convert.IsDBNull(Rdr("show_year")) = False Then ret.show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then ret.user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then ret.user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then ret.username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then ret.staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("working_day")) = False Then ret.working_day = Convert.ToInt32(Rdr("working_day"))
                        If Convert.IsDBNull(Rdr("log_in")) = False Then ret.log_in = Rdr("log_in").ToString()
                        If Convert.IsDBNull(Rdr("log_out")) = False Then ret.log_out = Rdr("log_out").ToString()
                        If Convert.IsDBNull(Rdr("total_time")) = False Then ret.total_time = Rdr("total_time").ToString()
                        If Convert.IsDBNull(Rdr("service_time")) = False Then ret.service_time = Rdr("service_time").ToString()
                        If Convert.IsDBNull(Rdr("productivity")) = False Then ret.productivity = Rdr("productivity").ToString()
                        If Convert.IsDBNull(Rdr("non_productivity")) = False Then ret.non_productivity = Rdr("non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("prod_learning")) = False Then ret.prod_learning = Rdr("prod_learning").ToString()
                        If Convert.IsDBNull(Rdr("prod_stand_by")) = False Then ret.prod_stand_by = Rdr("prod_stand_by").ToString()
                        If Convert.IsDBNull(Rdr("prod_brief")) = False Then ret.prod_brief = Rdr("prod_brief").ToString()
                        If Convert.IsDBNull(Rdr("prod_warp_up")) = False Then ret.prod_warp_up = Rdr("prod_warp_up").ToString()
                        If Convert.IsDBNull(Rdr("prod_consult")) = False Then ret.prod_consult = Rdr("prod_consult").ToString()
                        If Convert.IsDBNull(Rdr("prod_other")) = False Then ret.prod_other = Rdr("prod_other").ToString()
                        If Convert.IsDBNull(Rdr("total_productivity")) = False Then ret.total_productivity = Rdr("total_productivity").ToString()
                        If Convert.IsDBNull(Rdr("nprod_lunch")) = False Then ret.nprod_lunch = Rdr("nprod_lunch").ToString()
                        If Convert.IsDBNull(Rdr("nprod_leave")) = False Then ret.nprod_leave = Rdr("nprod_leave").ToString()
                        If Convert.IsDBNull(Rdr("nprod_change_counter")) = False Then ret.nprod_change_counter = Rdr("nprod_change_counter").ToString()
                        If Convert.IsDBNull(Rdr("nprod_home")) = False Then ret.nprod_home = Rdr("nprod_home").ToString()
                        If Convert.IsDBNull(Rdr("nprod_mini_break")) = False Then ret.nprod_mini_break = Rdr("nprod_mini_break").ToString()
                        If Convert.IsDBNull(Rdr("nprod_restroom")) = False Then ret.nprod_restroom = Rdr("nprod_restroom").ToString()
                        If Convert.IsDBNull(Rdr("nprod_other")) = False Then ret.nprod_other = Rdr("nprod_other").ToString()
                        If Convert.IsDBNull(Rdr("total_non_productivity")) = False Then ret.total_non_productivity = Rdr("total_non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("sum_total_time")) = False Then ret.sum_total_time = Convert.ToInt32(Rdr("sum_total_time"))
                        If Convert.IsDBNull(Rdr("sum_service_time")) = False Then ret.sum_service_time = Convert.ToInt32(Rdr("sum_service_time"))
                        If Convert.IsDBNull(Rdr("sum_productivity")) = False Then ret.sum_productivity = Convert.ToInt32(Rdr("sum_productivity"))
                        If Convert.IsDBNull(Rdr("sum_non_productivity")) = False Then ret.sum_non_productivity = Convert.ToInt32(Rdr("sum_non_productivity"))
                        If Convert.IsDBNull(Rdr("sum_est_ot")) = False Then ret.sum_est_ot = Convert.ToInt32(Rdr("sum_est_ot"))
                        If Convert.IsDBNull(Rdr("sum_prod_learning")) = False Then ret.sum_prod_learning = Convert.ToInt32(Rdr("sum_prod_learning"))
                        If Convert.IsDBNull(Rdr("sum_prod_stand_by")) = False Then ret.sum_prod_stand_by = Convert.ToInt32(Rdr("sum_prod_stand_by"))
                        If Convert.IsDBNull(Rdr("sum_prod_brief")) = False Then ret.sum_prod_brief = Convert.ToInt32(Rdr("sum_prod_brief"))
                        If Convert.IsDBNull(Rdr("sum_prod_warp_up")) = False Then ret.sum_prod_warp_up = Convert.ToInt32(Rdr("sum_prod_warp_up"))
                        If Convert.IsDBNull(Rdr("sum_prod_consult")) = False Then ret.sum_prod_consult = Convert.ToInt32(Rdr("sum_prod_consult"))
                        If Convert.IsDBNull(Rdr("sum_prod_other")) = False Then ret.sum_prod_other = Convert.ToInt32(Rdr("sum_prod_other"))
                        If Convert.IsDBNull(Rdr("sum_nprod_lunch")) = False Then ret.sum_nprod_lunch = Convert.ToInt32(Rdr("sum_nprod_lunch"))
                        If Convert.IsDBNull(Rdr("sum_nprod_leave")) = False Then ret.sum_nprod_leave = Convert.ToInt32(Rdr("sum_nprod_leave"))
                        If Convert.IsDBNull(Rdr("sum_nprod_change_counter")) = False Then ret.sum_nprod_change_counter = Convert.ToInt32(Rdr("sum_nprod_change_counter"))
                        If Convert.IsDBNull(Rdr("sum_nprod_home")) = False Then ret.sum_nprod_home = Convert.ToInt32(Rdr("sum_nprod_home"))
                        If Convert.IsDBNull(Rdr("sum_nprod_mini_break")) = False Then ret.sum_nprod_mini_break = Convert.ToInt32(Rdr("sum_nprod_mini_break"))
                        If Convert.IsDBNull(Rdr("sum_nprod_restroom")) = False Then ret.sum_nprod_restroom = Convert.ToInt32(Rdr("sum_nprod_restroom"))
                        If Convert.IsDBNull(Rdr("sum_nprod_other")) = False Then ret.sum_nprod_other = Convert.ToInt32(Rdr("sum_nprod_other"))
                        If Convert.IsDBNull(Rdr("count_productivity")) = False Then ret.count_productivity = Convert.ToInt32(Rdr("count_productivity"))
                        If Convert.IsDBNull(Rdr("count_non_productivity")) = False Then ret.count_non_productivity = Convert.ToInt32(Rdr("count_non_productivity"))
                        If Convert.IsDBNull(Rdr("count_est_ot")) = False Then ret.count_est_ot = Convert.ToInt32(Rdr("count_est_ot"))
                        If Convert.IsDBNull(Rdr("count_prod_learning")) = False Then ret.count_prod_learning = Convert.ToInt32(Rdr("count_prod_learning"))
                        If Convert.IsDBNull(Rdr("count_prod_stand_by")) = False Then ret.count_prod_stand_by = Convert.ToInt32(Rdr("count_prod_stand_by"))
                        If Convert.IsDBNull(Rdr("count_prod_brief")) = False Then ret.count_prod_brief = Convert.ToInt32(Rdr("count_prod_brief"))
                        If Convert.IsDBNull(Rdr("count_prod_warp_up")) = False Then ret.count_prod_warp_up = Convert.ToInt32(Rdr("count_prod_warp_up"))
                        If Convert.IsDBNull(Rdr("count_prod_consult")) = False Then ret.count_prod_consult = Convert.ToInt32(Rdr("count_prod_consult"))
                        If Convert.IsDBNull(Rdr("count_prod_other")) = False Then ret.count_prod_other = Convert.ToInt32(Rdr("count_prod_other"))
                        If Convert.IsDBNull(Rdr("count_nprod_lunch")) = False Then ret.count_nprod_lunch = Convert.ToInt32(Rdr("count_nprod_lunch"))
                        If Convert.IsDBNull(Rdr("count_nprod_leave")) = False Then ret.count_nprod_leave = Convert.ToInt32(Rdr("count_nprod_leave"))
                        If Convert.IsDBNull(Rdr("count_nprod_change_counter")) = False Then ret.count_nprod_change_counter = Convert.ToInt32(Rdr("count_nprod_change_counter"))
                        If Convert.IsDBNull(Rdr("count_nprod_home")) = False Then ret.count_nprod_home = Convert.ToInt32(Rdr("count_nprod_home"))
                        If Convert.IsDBNull(Rdr("count_nprod_mini_break")) = False Then ret.count_nprod_mini_break = Convert.ToInt32(Rdr("count_nprod_mini_break"))
                        If Convert.IsDBNull(Rdr("count_nprod_restroom")) = False Then ret.count_nprod_restroom = Convert.ToInt32(Rdr("count_nprod_restroom"))
                        If Convert.IsDBNull(Rdr("count_nprod_other")) = False Then ret.count_nprod_other = Convert.ToInt32(Rdr("count_nprod_other"))
                        If Convert.IsDBNull(Rdr("est_ot")) = False Then ret.est_ot = Rdr("est_ot").ToString()

                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return ret
        End Function

        ' SQL Statements


        'Get Insert Statement for table TB_REP_STAFF_ATTENDANCE_MONTH
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, MONTH_NO, SHOW_MONTH, SHOW_YEAR, USER_ID, USER_CODE, USERNAME, STAFF_NAME, WORKING_DAY, LOG_IN, LOG_OUT, TOTAL_TIME, SERVICE_TIME, PRODUCTIVITY, NON_PRODUCTIVITY, PROD_LEARNING, PROD_STAND_BY, PROD_BRIEF, PROD_WARP_UP, PROD_CONSULT, PROD_OTHER, TOTAL_PRODUCTIVITY, NPROD_LUNCH, NPROD_LEAVE, NPROD_CHANGE_COUNTER, NPROD_HOME, NPROD_MINI_BREAK, NPROD_RESTROOM, NPROD_OTHER, TOTAL_NON_PRODUCTIVITY, SUM_TOTAL_TIME, SUM_SERVICE_TIME, SUM_PRODUCTIVITY, SUM_NON_PRODUCTIVITY, SUM_EST_OT, SUM_PROD_LEARNING, SUM_PROD_STAND_BY, SUM_PROD_BRIEF, SUM_PROD_WARP_UP, SUM_PROD_CONSULT, SUM_PROD_OTHER, SUM_NPROD_LUNCH, SUM_NPROD_LEAVE, SUM_NPROD_CHANGE_COUNTER, SUM_NPROD_HOME, SUM_NPROD_MINI_BREAK, SUM_NPROD_RESTROOM, SUM_NPROD_OTHER, COUNT_PRODUCTIVITY, COUNT_NON_PRODUCTIVITY, COUNT_EST_OT, COUNT_PROD_LEARNING, COUNT_PROD_STAND_BY, COUNT_PROD_BRIEF, COUNT_PROD_WARP_UP, COUNT_PROD_CONSULT, COUNT_PROD_OTHER, COUNT_NPROD_LUNCH, COUNT_NPROD_LEAVE, COUNT_NPROD_CHANGE_COUNTER, COUNT_NPROD_HOME, COUNT_NPROD_MINI_BREAK, COUNT_NPROD_RESTROOM, COUNT_NPROD_OTHER, EST_OT)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDouble(_MONTH_NO) & ", "
                sql += DB.SetString(_SHOW_MONTH) & ", "
                sql += DB.SetDouble(_SHOW_YEAR) & ", "
                sql += DB.SetDouble(_USER_ID) & ", "
                sql += DB.SetString(_USER_CODE) & ", "
                sql += DB.SetString(_USERNAME) & ", "
                sql += DB.SetString(_STAFF_NAME) & ", "
                sql += DB.SetDouble(_WORKING_DAY) & ", "
                sql += DB.SetString(_LOG_IN) & ", "
                sql += DB.SetString(_LOG_OUT) & ", "
                sql += DB.SetString(_TOTAL_TIME) & ", "
                sql += DB.SetString(_SERVICE_TIME) & ", "
                sql += DB.SetString(_PRODUCTIVITY) & ", "
                sql += DB.SetString(_NON_PRODUCTIVITY) & ", "
                sql += DB.SetString(_PROD_LEARNING) & ", "
                sql += DB.SetString(_PROD_STAND_BY) & ", "
                sql += DB.SetString(_PROD_BRIEF) & ", "
                sql += DB.SetString(_PROD_WARP_UP) & ", "
                sql += DB.SetString(_PROD_CONSULT) & ", "
                sql += DB.SetString(_PROD_OTHER) & ", "
                sql += DB.SetString(_TOTAL_PRODUCTIVITY) & ", "
                sql += DB.SetString(_NPROD_LUNCH) & ", "
                sql += DB.SetString(_NPROD_LEAVE) & ", "
                sql += DB.SetString(_NPROD_CHANGE_COUNTER) & ", "
                sql += DB.SetString(_NPROD_HOME) & ", "
                sql += DB.SetString(_NPROD_MINI_BREAK) & ", "
                sql += DB.SetString(_NPROD_RESTROOM) & ", "
                sql += DB.SetString(_NPROD_OTHER) & ", "
                sql += DB.SetString(_TOTAL_NON_PRODUCTIVITY) & ", "
                sql += DB.SetDouble(_SUM_TOTAL_TIME) & ", "
                sql += DB.SetDouble(_SUM_SERVICE_TIME) & ", "
                sql += DB.SetDouble(_SUM_PRODUCTIVITY) & ", "
                sql += DB.SetDouble(_SUM_NON_PRODUCTIVITY) & ", "
                sql += DB.SetDouble(_SUM_EST_OT) & ", "
                sql += DB.SetDouble(_SUM_PROD_LEARNING) & ", "
                sql += DB.SetDouble(_SUM_PROD_STAND_BY) & ", "
                sql += DB.SetDouble(_SUM_PROD_BRIEF) & ", "
                sql += DB.SetDouble(_SUM_PROD_WARP_UP) & ", "
                sql += DB.SetDouble(_SUM_PROD_CONSULT) & ", "
                sql += DB.SetDouble(_SUM_PROD_OTHER) & ", "
                sql += DB.SetDouble(_SUM_NPROD_LUNCH) & ", "
                sql += DB.SetDouble(_SUM_NPROD_LEAVE) & ", "
                sql += DB.SetDouble(_SUM_NPROD_CHANGE_COUNTER) & ", "
                sql += DB.SetDouble(_SUM_NPROD_HOME) & ", "
                sql += DB.SetDouble(_SUM_NPROD_MINI_BREAK) & ", "
                sql += DB.SetDouble(_SUM_NPROD_RESTROOM) & ", "
                sql += DB.SetDouble(_SUM_NPROD_OTHER) & ", "
                sql += DB.SetDouble(_COUNT_PRODUCTIVITY) & ", "
                sql += DB.SetDouble(_COUNT_NON_PRODUCTIVITY) & ", "
                sql += DB.SetDouble(_COUNT_EST_OT) & ", "
                sql += DB.SetDouble(_COUNT_PROD_LEARNING) & ", "
                sql += DB.SetDouble(_COUNT_PROD_STAND_BY) & ", "
                sql += DB.SetDouble(_COUNT_PROD_BRIEF) & ", "
                sql += DB.SetDouble(_COUNT_PROD_WARP_UP) & ", "
                sql += DB.SetDouble(_COUNT_PROD_CONSULT) & ", "
                sql += DB.SetDouble(_COUNT_PROD_OTHER) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_LUNCH) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_LEAVE) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_CHANGE_COUNTER) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_HOME) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_MINI_BREAK) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_RESTROOM) & ", "
                sql += DB.SetDouble(_COUNT_NPROD_OTHER) & ", "
                sql += DB.SetString(_EST_OT)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_STAFF_ATTENDANCE_MONTH
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "MONTH_NO = " & DB.SetDouble(_MONTH_NO) & ", "
                Sql += "SHOW_MONTH = " & DB.SetString(_SHOW_MONTH) & ", "
                Sql += "SHOW_YEAR = " & DB.SetDouble(_SHOW_YEAR) & ", "
                Sql += "USER_ID = " & DB.SetDouble(_USER_ID) & ", "
                Sql += "USER_CODE = " & DB.SetString(_USER_CODE) & ", "
                Sql += "USERNAME = " & DB.SetString(_USERNAME) & ", "
                Sql += "STAFF_NAME = " & DB.SetString(_STAFF_NAME) & ", "
                Sql += "WORKING_DAY = " & DB.SetDouble(_WORKING_DAY) & ", "
                Sql += "LOG_IN = " & DB.SetString(_LOG_IN) & ", "
                Sql += "LOG_OUT = " & DB.SetString(_LOG_OUT) & ", "
                Sql += "TOTAL_TIME = " & DB.SetString(_TOTAL_TIME) & ", "
                Sql += "SERVICE_TIME = " & DB.SetString(_SERVICE_TIME) & ", "
                Sql += "PRODUCTIVITY = " & DB.SetString(_PRODUCTIVITY) & ", "
                Sql += "NON_PRODUCTIVITY = " & DB.SetString(_NON_PRODUCTIVITY) & ", "
                Sql += "PROD_LEARNING = " & DB.SetString(_PROD_LEARNING) & ", "
                Sql += "PROD_STAND_BY = " & DB.SetString(_PROD_STAND_BY) & ", "
                Sql += "PROD_BRIEF = " & DB.SetString(_PROD_BRIEF) & ", "
                Sql += "PROD_WARP_UP = " & DB.SetString(_PROD_WARP_UP) & ", "
                Sql += "PROD_CONSULT = " & DB.SetString(_PROD_CONSULT) & ", "
                Sql += "PROD_OTHER = " & DB.SetString(_PROD_OTHER) & ", "
                Sql += "TOTAL_PRODUCTIVITY = " & DB.SetString(_TOTAL_PRODUCTIVITY) & ", "
                Sql += "NPROD_LUNCH = " & DB.SetString(_NPROD_LUNCH) & ", "
                Sql += "NPROD_LEAVE = " & DB.SetString(_NPROD_LEAVE) & ", "
                Sql += "NPROD_CHANGE_COUNTER = " & DB.SetString(_NPROD_CHANGE_COUNTER) & ", "
                Sql += "NPROD_HOME = " & DB.SetString(_NPROD_HOME) & ", "
                Sql += "NPROD_MINI_BREAK = " & DB.SetString(_NPROD_MINI_BREAK) & ", "
                Sql += "NPROD_RESTROOM = " & DB.SetString(_NPROD_RESTROOM) & ", "
                Sql += "NPROD_OTHER = " & DB.SetString(_NPROD_OTHER) & ", "
                Sql += "TOTAL_NON_PRODUCTIVITY = " & DB.SetString(_TOTAL_NON_PRODUCTIVITY) & ", "
                Sql += "SUM_TOTAL_TIME = " & DB.SetDouble(_SUM_TOTAL_TIME) & ", "
                Sql += "SUM_SERVICE_TIME = " & DB.SetDouble(_SUM_SERVICE_TIME) & ", "
                Sql += "SUM_PRODUCTIVITY = " & DB.SetDouble(_SUM_PRODUCTIVITY) & ", "
                Sql += "SUM_NON_PRODUCTIVITY = " & DB.SetDouble(_SUM_NON_PRODUCTIVITY) & ", "
                Sql += "SUM_EST_OT = " & DB.SetDouble(_SUM_EST_OT) & ", "
                Sql += "SUM_PROD_LEARNING = " & DB.SetDouble(_SUM_PROD_LEARNING) & ", "
                Sql += "SUM_PROD_STAND_BY = " & DB.SetDouble(_SUM_PROD_STAND_BY) & ", "
                Sql += "SUM_PROD_BRIEF = " & DB.SetDouble(_SUM_PROD_BRIEF) & ", "
                Sql += "SUM_PROD_WARP_UP = " & DB.SetDouble(_SUM_PROD_WARP_UP) & ", "
                Sql += "SUM_PROD_CONSULT = " & DB.SetDouble(_SUM_PROD_CONSULT) & ", "
                Sql += "SUM_PROD_OTHER = " & DB.SetDouble(_SUM_PROD_OTHER) & ", "
                Sql += "SUM_NPROD_LUNCH = " & DB.SetDouble(_SUM_NPROD_LUNCH) & ", "
                Sql += "SUM_NPROD_LEAVE = " & DB.SetDouble(_SUM_NPROD_LEAVE) & ", "
                Sql += "SUM_NPROD_CHANGE_COUNTER = " & DB.SetDouble(_SUM_NPROD_CHANGE_COUNTER) & ", "
                Sql += "SUM_NPROD_HOME = " & DB.SetDouble(_SUM_NPROD_HOME) & ", "
                Sql += "SUM_NPROD_MINI_BREAK = " & DB.SetDouble(_SUM_NPROD_MINI_BREAK) & ", "
                Sql += "SUM_NPROD_RESTROOM = " & DB.SetDouble(_SUM_NPROD_RESTROOM) & ", "
                Sql += "SUM_NPROD_OTHER = " & DB.SetDouble(_SUM_NPROD_OTHER) & ", "
                Sql += "COUNT_PRODUCTIVITY = " & DB.SetDouble(_COUNT_PRODUCTIVITY) & ", "
                Sql += "COUNT_NON_PRODUCTIVITY = " & DB.SetDouble(_COUNT_NON_PRODUCTIVITY) & ", "
                Sql += "COUNT_EST_OT = " & DB.SetDouble(_COUNT_EST_OT) & ", "
                Sql += "COUNT_PROD_LEARNING = " & DB.SetDouble(_COUNT_PROD_LEARNING) & ", "
                Sql += "COUNT_PROD_STAND_BY = " & DB.SetDouble(_COUNT_PROD_STAND_BY) & ", "
                Sql += "COUNT_PROD_BRIEF = " & DB.SetDouble(_COUNT_PROD_BRIEF) & ", "
                Sql += "COUNT_PROD_WARP_UP = " & DB.SetDouble(_COUNT_PROD_WARP_UP) & ", "
                Sql += "COUNT_PROD_CONSULT = " & DB.SetDouble(_COUNT_PROD_CONSULT) & ", "
                Sql += "COUNT_PROD_OTHER = " & DB.SetDouble(_COUNT_PROD_OTHER) & ", "
                Sql += "COUNT_NPROD_LUNCH = " & DB.SetDouble(_COUNT_NPROD_LUNCH) & ", "
                Sql += "COUNT_NPROD_LEAVE = " & DB.SetDouble(_COUNT_NPROD_LEAVE) & ", "
                Sql += "COUNT_NPROD_CHANGE_COUNTER = " & DB.SetDouble(_COUNT_NPROD_CHANGE_COUNTER) & ", "
                Sql += "COUNT_NPROD_HOME = " & DB.SetDouble(_COUNT_NPROD_HOME) & ", "
                Sql += "COUNT_NPROD_MINI_BREAK = " & DB.SetDouble(_COUNT_NPROD_MINI_BREAK) & ", "
                Sql += "COUNT_NPROD_RESTROOM = " & DB.SetDouble(_COUNT_NPROD_RESTROOM) & ", "
                Sql += "COUNT_NPROD_OTHER = " & DB.SetDouble(_COUNT_NPROD_OTHER) & ", "
                Sql += "EST_OT = " & DB.SetString(_EST_OT) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_STAFF_ATTENDANCE_MONTH
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_STAFF_ATTENDANCE_MONTH
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, MONTH_NO, SHOW_MONTH, SHOW_YEAR, USER_ID, USER_CODE, USERNAME, STAFF_NAME, WORKING_DAY, LOG_IN, LOG_OUT, TOTAL_TIME, SERVICE_TIME, PRODUCTIVITY, NON_PRODUCTIVITY, PROD_LEARNING, PROD_STAND_BY, PROD_BRIEF, PROD_WARP_UP, PROD_CONSULT, PROD_OTHER, TOTAL_PRODUCTIVITY, NPROD_LUNCH, NPROD_LEAVE, NPROD_CHANGE_COUNTER, NPROD_HOME, NPROD_MINI_BREAK, NPROD_RESTROOM, NPROD_OTHER, TOTAL_NON_PRODUCTIVITY, SUM_TOTAL_TIME, SUM_SERVICE_TIME, SUM_PRODUCTIVITY, SUM_NON_PRODUCTIVITY, SUM_EST_OT, SUM_PROD_LEARNING, SUM_PROD_STAND_BY, SUM_PROD_BRIEF, SUM_PROD_WARP_UP, SUM_PROD_CONSULT, SUM_PROD_OTHER, SUM_NPROD_LUNCH, SUM_NPROD_LEAVE, SUM_NPROD_CHANGE_COUNTER, SUM_NPROD_HOME, SUM_NPROD_MINI_BREAK, SUM_NPROD_RESTROOM, SUM_NPROD_OTHER, COUNT_PRODUCTIVITY, COUNT_NON_PRODUCTIVITY, COUNT_EST_OT, COUNT_PROD_LEARNING, COUNT_PROD_STAND_BY, COUNT_PROD_BRIEF, COUNT_PROD_WARP_UP, COUNT_PROD_CONSULT, COUNT_PROD_OTHER, COUNT_NPROD_LUNCH, COUNT_NPROD_LEAVE, COUNT_NPROD_CHANGE_COUNTER, COUNT_NPROD_HOME, COUNT_NPROD_MINI_BREAK, COUNT_NPROD_RESTROOM, COUNT_NPROD_OTHER, EST_OT FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
