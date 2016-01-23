
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_COUNTER table Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="TB_COUNTER")>  _
    Public Class TbCounterShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _COUNTER_CODE As  String  = ""
        Dim _COUNTER_NAME As  String  = ""
        Dim _COUNTER_STATUS As  System.Nullable(Of Long)  = 0
        Dim _QUICKSERVICE As  System.Nullable(Of Long)  = 0
        Dim _BEEP As  System.Nullable(Of Long)  = 0
        Dim _RETURN_CASE As  System.Nullable(Of Long)  = 0
        Dim _AUTO_SWAP As  System.Nullable(Of Long)  = 0
        Dim _SPEED_LANE As  System.Nullable(Of Long)  = 0
        Dim _UNITDISPLAY As  String  = ""
        Dim _AVAILABLE As  System.Nullable(Of Long)  = 0
        Dim _BACK_OFFICE As  System.Nullable(Of Long)  = 0
        Dim _COUNTER_MANAGER As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_CODE", DbType:="VarChar(20)")>  _
        Public Property COUNTER_CODE() As  String 
            Get
                Return _COUNTER_CODE
            End Get
            Set(ByVal value As  String )
               _COUNTER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_NAME", DbType:="VarChar(50)")>  _
        Public Property COUNTER_NAME() As  String 
            Get
                Return _COUNTER_NAME
            End Get
            Set(ByVal value As  String )
               _COUNTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_STATUS", DbType:="SmallInt")>  _
        Public Property COUNTER_STATUS() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_QUICKSERVICE", DbType:="SmallInt")>  _
        Public Property QUICKSERVICE() As  System.Nullable(Of Long) 
            Get
                Return _QUICKSERVICE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _QUICKSERVICE = value
            End Set
        End Property 
        <Column(Storage:="_BEEP", DbType:="SmallInt")>  _
        Public Property BEEP() As  System.Nullable(Of Long) 
            Get
                Return _BEEP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BEEP = value
            End Set
        End Property 
        <Column(Storage:="_RETURN_CASE", DbType:="SmallInt")>  _
        Public Property RETURN_CASE() As  System.Nullable(Of Long) 
            Get
                Return _RETURN_CASE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _RETURN_CASE = value
            End Set
        End Property 
        <Column(Storage:="_AUTO_SWAP", DbType:="SmallInt")>  _
        Public Property AUTO_SWAP() As  System.Nullable(Of Long) 
            Get
                Return _AUTO_SWAP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _AUTO_SWAP = value
            End Set
        End Property 
        <Column(Storage:="_SPEED_LANE", DbType:="SmallInt")>  _
        Public Property SPEED_LANE() As  System.Nullable(Of Long) 
            Get
                Return _SPEED_LANE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SPEED_LANE = value
            End Set
        End Property 
        <Column(Storage:="_UNITDISPLAY", DbType:="VarChar(5)")>  _
        Public Property UNITDISPLAY() As  String 
            Get
                Return _UNITDISPLAY
            End Get
            Set(ByVal value As  String )
               _UNITDISPLAY = value
            End Set
        End Property 
        <Column(Storage:="_AVAILABLE", DbType:="SmallInt")>  _
        Public Property AVAILABLE() As  System.Nullable(Of Long) 
            Get
                Return _AVAILABLE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _AVAILABLE = value
            End Set
        End Property 
        <Column(Storage:="_BACK_OFFICE", DbType:="SmallInt")>  _
        Public Property BACK_OFFICE() As  System.Nullable(Of Long) 
            Get
                Return _BACK_OFFICE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BACK_OFFICE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_MANAGER", DbType:="SmallInt")>  _
        Public Property COUNTER_MANAGER() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_MANAGER
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_MANAGER = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE_STATUS", DbType:="SmallInt")>  _
        Public Property ACTIVE_STATUS() As  System.Nullable(Of Long) 
            Get
                Return _ACTIVE_STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ACTIVE_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20)")>  _
        Public Property CREATE_BY() As  String 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  String )
               _CREATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime")>  _
        Public Property CREATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
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


    End Class
End Namespace
