
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_APPOINTMENT_SHOP_WEEK table Parameter.
    '[Create by  on March, 23 2013]

    <Table(Name:="TB_REP_APPOINTMENT_SHOP_WEEK")>  _
    Public Class TbRepAppointmentShopWeekCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _WEEK_OF_YEAR As Long = 0
        Dim _SHOW_YEAR As Long = 0
        Dim _PERIOD_DATE As String = ""
        Dim _TOTAL As Long = 0
        Dim _KIOSK_SHOW As Long = 0
        Dim _KIOSK_NOSHOW As Long = 0
        Dim _WEB_SHOW As Long = 0
        Dim _WEB_NOSHOW As Long = 0
        Dim _MOBILE_SHOW As Long = 0
        Dim _MOBILE_NOSHOW As Long = 0

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
        <Column(Storage:="_WEEK_OF_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEEK_OF_YEAR() As Long
            Get
                Return _WEEK_OF_YEAR
            End Get
            Set(ByVal value As Long)
               _WEEK_OF_YEAR = value
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
        <Column(Storage:="_PERIOD_DATE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PERIOD_DATE() As String
            Get
                Return _PERIOD_DATE
            End Get
            Set(ByVal value As String)
               _PERIOD_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL() As Long
            Get
                Return _TOTAL
            End Get
            Set(ByVal value As Long)
               _TOTAL = value
            End Set
        End Property 
        <Column(Storage:="_KIOSK_SHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KIOSK_SHOW() As Long
            Get
                Return _KIOSK_SHOW
            End Get
            Set(ByVal value As Long)
               _KIOSK_SHOW = value
            End Set
        End Property 
        <Column(Storage:="_KIOSK_NOSHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KIOSK_NOSHOW() As Long
            Get
                Return _KIOSK_NOSHOW
            End Get
            Set(ByVal value As Long)
               _KIOSK_NOSHOW = value
            End Set
        End Property 
        <Column(Storage:="_WEB_SHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEB_SHOW() As Long
            Get
                Return _WEB_SHOW
            End Get
            Set(ByVal value As Long)
               _WEB_SHOW = value
            End Set
        End Property 
        <Column(Storage:="_WEB_NOSHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEB_NOSHOW() As Long
            Get
                Return _WEB_NOSHOW
            End Get
            Set(ByVal value As Long)
               _WEB_NOSHOW = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_SHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_SHOW() As Long
            Get
                Return _MOBILE_SHOW
            End Get
            Set(ByVal value As Long)
               _MOBILE_SHOW = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NOSHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NOSHOW() As Long
            Get
                Return _MOBILE_NOSHOW
            End Get
            Set(ByVal value As Long)
               _MOBILE_NOSHOW = value
            End Set
        End Property 


    End Class
End Namespace
