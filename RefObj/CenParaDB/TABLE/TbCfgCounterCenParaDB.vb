
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CFG_COUNTER table Parameter.
    '[Create by  on October, 19 2014]

    <Table(Name:="TB_CFG_COUNTER")>  _
    Public Class TbCfgCounterCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _EVENT_DATE As DateTime = New DateTime(1,1,1)
        Dim _COUNTER_CODE As String = ""
        Dim _COUNTER_NAME As String = ""
        Dim _QUICKSERVICE As Long = 0
        Dim _UNITDISPLAY As String = ""
        Dim _SPEED_LANE As Long = 0
        Dim _BACK_OFFICE As Long = 0
        Dim _COUNTER_MANAGER As Long = 0
        Dim _ACTIVE_STATUS As Long = 0
        Dim _EVENT_STATUS As Char = "1"
        Dim _REF_SHOP_COUNTER_ID As Long = 0

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
        <Column(Storage:="_EVENT_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property EVENT_DATE() As DateTime
            Get
                Return _EVENT_DATE
            End Get
            Set(ByVal value As DateTime)
               _EVENT_DATE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_CODE", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_CODE() As String
            Get
                Return _COUNTER_CODE
            End Get
            Set(ByVal value As String)
               _COUNTER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_NAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_NAME() As String
            Get
                Return _COUNTER_NAME
            End Get
            Set(ByVal value As String)
               _COUNTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_QUICKSERVICE", DbType:="SmallInt NOT NULL ",CanBeNull:=false)>  _
        Public Property QUICKSERVICE() As Long
            Get
                Return _QUICKSERVICE
            End Get
            Set(ByVal value As Long)
               _QUICKSERVICE = value
            End Set
        End Property 
        <Column(Storage:="_UNITDISPLAY", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property UNITDISPLAY() As String
            Get
                Return _UNITDISPLAY
            End Get
            Set(ByVal value As String)
               _UNITDISPLAY = value
            End Set
        End Property 
        <Column(Storage:="_SPEED_LANE", DbType:="SmallInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SPEED_LANE() As Long
            Get
                Return _SPEED_LANE
            End Get
            Set(ByVal value As Long)
               _SPEED_LANE = value
            End Set
        End Property 
        <Column(Storage:="_BACK_OFFICE", DbType:="SmallInt NOT NULL ",CanBeNull:=false)>  _
        Public Property BACK_OFFICE() As Long
            Get
                Return _BACK_OFFICE
            End Get
            Set(ByVal value As Long)
               _BACK_OFFICE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_MANAGER", DbType:="SmallInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_MANAGER() As Long
            Get
                Return _COUNTER_MANAGER
            End Get
            Set(ByVal value As Long)
               _COUNTER_MANAGER = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE_STATUS", DbType:="SmallInt NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTIVE_STATUS() As Long
            Get
                Return _ACTIVE_STATUS
            End Get
            Set(ByVal value As Long)
               _ACTIVE_STATUS = value
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
        <Column(Storage:="_REF_SHOP_COUNTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property REF_SHOP_COUNTER_ID() As Long
            Get
                Return _REF_SHOP_COUNTER_ID
            End Get
            Set(ByVal value As Long)
               _REF_SHOP_COUNTER_ID = value
            End Set
        End Property 


    End Class
End Namespace
