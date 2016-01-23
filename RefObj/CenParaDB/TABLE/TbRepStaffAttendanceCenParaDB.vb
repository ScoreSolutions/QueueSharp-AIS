
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_STAFF_ATTENDANCE table Parameter.
    '[Create by  on November, 14 2012]

    <Table(Name:="TB_REP_STAFF_ATTENDANCE")>  _
    Public Class TbRepStaffAttendanceCenParaDB

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
        Dim _USER_ID As Long = 0
        Dim _USER_CODE As  String  = ""
        Dim _USERNAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _LOG_IN As String = ""
        Dim _LOG_OUT As  String  = ""
        Dim _TOTAL_TIME As  String  = ""
        Dim _SERVICE_TIME As  String  = ""
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
        <Column(Storage:="_SHOW_DATE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_DATE() As String
            Get
                Return _SHOW_DATE
            End Get
            Set(ByVal value As String)
               _SHOW_DATE = value
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
        <Column(Storage:="_USER_CODE", DbType:="VarChar(255)")>  _
        Public Property USER_CODE() As  String 
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As  String )
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
        <Column(Storage:="_LOG_IN", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOG_IN() As String
            Get
                Return _LOG_IN
            End Get
            Set(ByVal value As String)
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
        <Column(Storage:="_SERVICE_TIME", DbType:="VarChar(50)")>  _
        Public Property SERVICE_TIME() As  String 
            Get
                Return _SERVICE_TIME
            End Get
            Set(ByVal value As  String )
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


    End Class
End Namespace
