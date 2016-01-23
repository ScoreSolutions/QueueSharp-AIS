
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT_SCHEDULE table Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="TB_APPOINTMENT_SCHEDULE")>  _
    Public Class TbAppointmentScheduleShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _APP_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CAPACITY As  System.Nullable(Of Long)  = 0
        Dim _START_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SLOT As  System.Nullable(Of Long)  = 0
        Dim _GAP As  System.Nullable(Of Long)  = 0
        Dim _START_SLOT As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_SLOT As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ROWGUID As String = ""

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
        <Column(Storage:="_APP_DATE", DbType:="DateTime")>  _
        Public Property APP_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_CAPACITY", DbType:="Int")>  _
        Public Property CAPACITY() As  System.Nullable(Of Long) 
            Get
                Return _CAPACITY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CAPACITY = value
            End Set
        End Property 
        <Column(Storage:="_START_TIME", DbType:="DateTime")>  _
        Public Property START_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _START_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _START_TIME = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME", DbType:="DateTime")>  _
        Public Property END_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _END_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SLOT", DbType:="SmallInt")>  _
        Public Property SLOT() As  System.Nullable(Of Long) 
            Get
                Return _SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SLOT = value
            End Set
        End Property 
        <Column(Storage:="_GAP", DbType:="SmallInt")>  _
        Public Property GAP() As  System.Nullable(Of Long) 
            Get
                Return _GAP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _GAP = value
            End Set
        End Property 
        <Column(Storage:="_START_SLOT", DbType:="DateTime")>  _
        Public Property START_SLOT() As  System.Nullable(Of DateTime) 
            Get
                Return _START_SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _START_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_END_SLOT", DbType:="DateTime")>  _
        Public Property END_SLOT() As  System.Nullable(Of DateTime) 
            Get
                Return _END_SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _END_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="Int")>  _
        Public Property CREATE_BY() As  System.Nullable(Of Long) 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
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
        <Column(Storage:="_UPDATE_BY", DbType:="Int")>  _
        Public Property UPDATE_BY() As  System.Nullable(Of Long) 
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
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
        <Column(Storage:="_ROWGUID", DbType:="UNIQUEIDENTIFIER NOT NULL ",CanBeNull:=false)>  _
        Public Property ROWGUID() As String
            Get
                Return _ROWGUID
            End Get
            Set(ByVal value As String)
               _ROWGUID = value
            End Set
        End Property 


    End Class
End Namespace
