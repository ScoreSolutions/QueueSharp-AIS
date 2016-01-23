
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT_SLOT table Parameter.
    '[Create by  on July, 18 2013]

    <Table(Name:="TB_APPOINTMENT_SLOT")>  _
    Public Class TbAppointmentSlotShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _APP_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CAPACITY As  System.Nullable(Of Long)  = 0
        Dim _SLOT_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _IN_USE As  System.Nullable(Of Long)  = 0
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
        <Column(Storage:="_APP_DATE", DbType:="DateTime")>  _
        Public Property APP_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_CAPACITY", DbType:="SmallInt")>  _
        Public Property CAPACITY() As  System.Nullable(Of Long) 
            Get
                Return _CAPACITY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CAPACITY = value
            End Set
        End Property 
        <Column(Storage:="_SLOT_TIME", DbType:="DateTime")>  _
        Public Property SLOT_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _SLOT_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SLOT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_IN_USE", DbType:="SmallInt")>  _
        Public Property IN_USE() As  System.Nullable(Of Long) 
            Get
                Return _IN_USE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _IN_USE = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50)")>  _
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


    End Class
End Namespace
