
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_SUMMARY_REPORT_STAFF table Parameter.
    '[Create by  on August, 23 2013]

    <Table(Name:="TB_REP_SUMMARY_REPORT_STAFF")>  _
    Public Class TbRepSummaryReportStaffCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SHOP_LOCATION_GROUP As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SHOW_DATE As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _EMP_ID As  String  = ""
        Dim _NETWORK_TYPE As String = ""
        Dim _SEGMENT_TYPE As String = ""
        Dim _ITEM_ID As Long = 0
        Dim _ITEM_NAME_EN As String = ""
        Dim _ITEM_NAME_TH As String = ""
        Dim _REGIS As Long = 0
        Dim _SERVE As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCLE As Long = 0
        Dim _WAIT_WITH_KPI As Long = 0
        Dim _SERVE_WITH_KPI As Long = 0
        Dim _P_HT As Long = 0
        Dim _AHT As Long = 0
        Dim _MAX_HT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _SUM_HT As Long = 0
        Dim _PRODUCTIVITY As String = ""
        Dim _P_PRODUCTIVITY As String = ""
        Dim _NON_PRODUCTIVITY As String = ""
        Dim _P_NON_PRODUCTIVITY As String = ""
        Dim _TOTAL_TIME As String = ""

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(50)")>  _
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
        <Column(Storage:="_SHOP_LOCATION_GROUP", DbType:="VarChar(3) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_LOCATION_GROUP() As String
            Get
                Return _SHOP_LOCATION_GROUP
            End Get
            Set(ByVal value As String)
               _SHOP_LOCATION_GROUP = value
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
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property STAFF_NAME() As String
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As String)
               _STAFF_NAME = value
            End Set
        End Property 
        <Column(Storage:="_EMP_ID", DbType:="VarChar(255)")>  _
        Public Property EMP_ID() As  String 
            Get
                Return _EMP_ID
            End Get
            Set(ByVal value As  String )
               _EMP_ID = value
            End Set
        End Property 
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NETWORK_TYPE() As String
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As String)
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_SEGMENT_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SEGMENT_TYPE() As String
            Get
                Return _SEGMENT_TYPE
            End Get
            Set(ByVal value As String)
               _SEGMENT_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_ID() As Long
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As Long)
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME_EN() As String
            Get
                Return _ITEM_NAME_EN
            End Get
            Set(ByVal value As String)
               _ITEM_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME_TH() As String
            Get
                Return _ITEM_NAME_TH
            End Get
            Set(ByVal value As String)
               _ITEM_NAME_TH = value
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
        <Column(Storage:="_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE() As Long
            Get
                Return _SERVE
            End Get
            Set(ByVal value As Long)
               _SERVE = value
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
        <Column(Storage:="_CANCLE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCLE() As Long
            Get
                Return _CANCLE
            End Get
            Set(ByVal value As Long)
               _CANCLE = value
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
        <Column(Storage:="_P_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property P_HT() As Long
            Get
                Return _P_HT
            End Get
            Set(ByVal value As Long)
               _P_HT = value
            End Set
        End Property 
        <Column(Storage:="_AHT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property AHT() As Long
            Get
                Return _AHT
            End Get
            Set(ByVal value As Long)
               _AHT = value
            End Set
        End Property 
        <Column(Storage:="_MAX_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_HT() As Long
            Get
                Return _MAX_HT
            End Get
            Set(ByVal value As Long)
               _MAX_HT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_HT() As Long
            Get
                Return _COUNT_HT
            End Get
            Set(ByVal value As Long)
               _COUNT_HT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_HT() As Long
            Get
                Return _SUM_HT
            End Get
            Set(ByVal value As Long)
               _SUM_HT = value
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
        <Column(Storage:="_P_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property P_PRODUCTIVITY() As String
            Get
                Return _P_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _P_PRODUCTIVITY = value
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
        <Column(Storage:="_P_NON_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property P_NON_PRODUCTIVITY() As String
            Get
                Return _P_NON_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _P_NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_TIME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_TIME() As String
            Get
                Return _TOTAL_TIME
            End Get
            Set(ByVal value As String)
               _TOTAL_TIME = value
            End Set
        End Property 


    End Class
End Namespace
