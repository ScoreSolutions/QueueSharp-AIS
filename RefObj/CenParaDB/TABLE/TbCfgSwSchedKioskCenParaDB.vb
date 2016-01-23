
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CFG_SW_SCHED_KIOSK table Parameter.
    '[Create by  on June, 26 2013]

    <Table(Name:="TB_CFG_SW_SCHED_KIOSK")>  _
    Public Class TbCfgSwSchedKioskCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _EVENT_DATE As DateTime = New DateTime(1,1,1)
        Dim _K_BEFORE As  System.Nullable(Of Long)  = 0
        Dim _K_LATE As  System.Nullable(Of Long)  = 0
        Dim _K_MAX_APPOINTMENT As  System.Nullable(Of Long)  = 0
        Dim _K_BEFORE_APP As  System.Nullable(Of Long)  = 0
        Dim _K_CANCEL As  System.Nullable(Of Long)  = 0
        Dim _K_DISABLE As  System.Nullable(Of Long)  = 0
        Dim _K_SERVE As  System.Nullable(Of Long)  = 0
        Dim _K_REFRESH As  System.Nullable(Of Long)  = 0
        Dim _K_VDO As  System.Nullable(Of Long)  = 0
        Dim _K_LEN As  System.Nullable(Of Long)  = 0
        Dim _K_MOBILE1 As  String  = ""
        Dim _K_MOBILE2 As  String  = ""
        Dim _K_MOBILE3 As  String  = ""
        Dim _K_MOBILE4 As  String  = ""
        Dim _K_RETARDATION_VDO As  System.Nullable(Of Long)  = 0
        Dim _EVENT_STATUS As Char = "1"

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
        <Column(Storage:="_EVENT_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property EVENT_DATE() As DateTime
            Get
                Return _EVENT_DATE
            End Get
            Set(ByVal value As DateTime)
               _EVENT_DATE = value
            End Set
        End Property 
        <Column(Storage:="_K_BEFORE", DbType:="Int")>  _
        Public Property K_BEFORE() As  System.Nullable(Of Long) 
            Get
                Return _K_BEFORE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_BEFORE = value
            End Set
        End Property 
        <Column(Storage:="_K_LATE", DbType:="Int")>  _
        Public Property K_LATE() As  System.Nullable(Of Long) 
            Get
                Return _K_LATE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_LATE = value
            End Set
        End Property 
        <Column(Storage:="_K_MAX_APPOINTMENT", DbType:="Int")>  _
        Public Property K_MAX_APPOINTMENT() As  System.Nullable(Of Long) 
            Get
                Return _K_MAX_APPOINTMENT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_MAX_APPOINTMENT = value
            End Set
        End Property 
        <Column(Storage:="_K_BEFORE_APP", DbType:="Int")>  _
        Public Property K_BEFORE_APP() As  System.Nullable(Of Long) 
            Get
                Return _K_BEFORE_APP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_BEFORE_APP = value
            End Set
        End Property 
        <Column(Storage:="_K_CANCEL", DbType:="Int")>  _
        Public Property K_CANCEL() As  System.Nullable(Of Long) 
            Get
                Return _K_CANCEL
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_K_DISABLE", DbType:="Int")>  _
        Public Property K_DISABLE() As  System.Nullable(Of Long) 
            Get
                Return _K_DISABLE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_DISABLE = value
            End Set
        End Property 
        <Column(Storage:="_K_SERVE", DbType:="Int")>  _
        Public Property K_SERVE() As  System.Nullable(Of Long) 
            Get
                Return _K_SERVE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_K_REFRESH", DbType:="Int")>  _
        Public Property K_REFRESH() As  System.Nullable(Of Long) 
            Get
                Return _K_REFRESH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_REFRESH = value
            End Set
        End Property 
        <Column(Storage:="_K_VDO", DbType:="Int")>  _
        Public Property K_VDO() As  System.Nullable(Of Long) 
            Get
                Return _K_VDO
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_VDO = value
            End Set
        End Property 
        <Column(Storage:="_K_LEN", DbType:="Int")>  _
        Public Property K_LEN() As  System.Nullable(Of Long) 
            Get
                Return _K_LEN
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_LEN = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE1", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE1() As  String 
            Get
                Return _K_MOBILE1
            End Get
            Set(ByVal value As  String )
               _K_MOBILE1 = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE2", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE2() As  String 
            Get
                Return _K_MOBILE2
            End Get
            Set(ByVal value As  String )
               _K_MOBILE2 = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE3", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE3() As  String 
            Get
                Return _K_MOBILE3
            End Get
            Set(ByVal value As  String )
               _K_MOBILE3 = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE4", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE4() As  String 
            Get
                Return _K_MOBILE4
            End Get
            Set(ByVal value As  String )
               _K_MOBILE4 = value
            End Set
        End Property 
        <Column(Storage:="_K_RETARDATION_VDO", DbType:="Int")>  _
        Public Property K_RETARDATION_VDO() As  System.Nullable(Of Long) 
            Get
                Return _K_RETARDATION_VDO
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_RETARDATION_VDO = value
            End Set
        End Property 
        <Column(Storage:="_EVENT_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property EVENT_STATUS() As Char
            Get
                Return _EVENT_STATUS
            End Get
            Set(ByVal value As Char)
               _EVENT_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
