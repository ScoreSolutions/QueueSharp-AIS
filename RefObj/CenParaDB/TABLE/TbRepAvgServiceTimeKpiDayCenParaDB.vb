
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_AVG_SERVICE_TIME_KPI_DAY table Parameter.
    '[Create by  on March, 8 2013]

    <Table(Name:="TB_REP_AVG_SERVICE_TIME_KPI_DAY")>  _
    Public Class TbRepAvgServiceTimeKpiDayCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _REGIS As Long = 0
        Dim _SERVED As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCEL As Long = 0
        Dim _SERVE_WITH_KPI As Long = 0
        Dim _WAIT_WITH_KPI As Long = 0
        Dim _NOT_CALL As Long = 0
        Dim _NOT_CONFIRM As Long = 0
        Dim _NOT_END As Long = 0
        Dim _MAX_WT As Long = 0
        Dim _MAX_HT As  System.Nullable(Of Long)  = 0

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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
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
        <Column(Storage:="_SERVE_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE_WITH_KPI() As Long
            Get
                Return _SERVE_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _SERVE_WITH_KPI = value
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
        <Column(Storage:="_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CALL() As Long
            Get
                Return _NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CONFIRM", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CONFIRM() As Long
            Get
                Return _NOT_CONFIRM
            End Get
            Set(ByVal value As Long)
               _NOT_CONFIRM = value
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
        <Column(Storage:="_MAX_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_WT() As Long
            Get
                Return _MAX_WT
            End Get
            Set(ByVal value As Long)
               _MAX_WT = value
            End Set
        End Property 
        <Column(Storage:="_MAX_HT", DbType:="Int")>  _
        Public Property MAX_HT() As  System.Nullable(Of Long) 
            Get
                Return _MAX_HT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _MAX_HT = value
            End Set
        End Property 


    End Class
End Namespace
