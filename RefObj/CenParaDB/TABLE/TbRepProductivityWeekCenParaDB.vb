
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_PRODUCTIVITY_WEEK table Parameter.
    '[Create by  on March, 11 2013]

    <Table(Name:="TB_REP_PRODUCTIVITY_WEEK")>  _
    Public Class TbRepProductivityWeekCenParaDB

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
        Dim _WEEK_OF_YEAR As Long = 0
        Dim _SHOW_YEAR As Long = 0
        Dim _PERIOD_DATE As String = ""
        Dim _TOTAL_TARGET As Long = 0
        Dim _TOTAL_ACTUAL As Long = 0
        Dim _TOTAL_PER_ACHIEVE_TARGET As Long = 0
        Dim _TOTAL_AHT As Long = 0
        Dim _DATA_VALUE As String = ""

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
        <Column(Storage:="_PERIOD_DATE", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property PERIOD_DATE() As String
            Get
                Return _PERIOD_DATE
            End Get
            Set(ByVal value As String)
               _PERIOD_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_TARGET", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_TARGET() As Long
            Get
                Return _TOTAL_TARGET
            End Get
            Set(ByVal value As Long)
               _TOTAL_TARGET = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_ACTUAL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_ACTUAL() As Long
            Get
                Return _TOTAL_ACTUAL
            End Get
            Set(ByVal value As Long)
               _TOTAL_ACTUAL = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_PER_ACHIEVE_TARGET", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_PER_ACHIEVE_TARGET() As Long
            Get
                Return _TOTAL_PER_ACHIEVE_TARGET
            End Get
            Set(ByVal value As Long)
               _TOTAL_PER_ACHIEVE_TARGET = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_AHT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_AHT() As Long
            Get
                Return _TOTAL_AHT
            End Get
            Set(ByVal value As Long)
               _TOTAL_AHT = value
            End Set
        End Property 
        <Column(Storage:="_DATA_VALUE", DbType:="Text NOT NULL ",CanBeNull:=false)>  _
        Public Property DATA_VALUE() As String
            Get
                Return _DATA_VALUE
            End Get
            Set(ByVal value As String)
               _DATA_VALUE = value
            End Set
        End Property 


    End Class
End Namespace
