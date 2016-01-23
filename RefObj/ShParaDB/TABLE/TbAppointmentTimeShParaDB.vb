
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT_TIME table Parameter.
    '[Create by  on January, 27 2012]

    <Table(Name:="TB_APPOINTMENT_TIME")>  _
    Public Class TbAppointmentTimeShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _APP_ID As  System.Nullable(Of Long)  = 0
        Dim _START_TIME_H As  System.Nullable(Of Long)  = 0
        Dim _START_TIME_M As  System.Nullable(Of Long)  = 0
        Dim _END_TIME_H As  System.Nullable(Of Long)  = 0
        Dim _END_TIME_M As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
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
        <Column(Storage:="_APP_ID", DbType:="Int")>  _
        Public Property APP_ID() As  System.Nullable(Of Long) 
            Get
                Return _APP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _APP_ID = value
            End Set
        End Property 
        <Column(Storage:="_START_TIME_H", DbType:="SmallInt")>  _
        Public Property START_TIME_H() As  System.Nullable(Of Long) 
            Get
                Return _START_TIME_H
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _START_TIME_H = value
            End Set
        End Property 
        <Column(Storage:="_START_TIME_M", DbType:="SmallInt")>  _
        Public Property START_TIME_M() As  System.Nullable(Of Long) 
            Get
                Return _START_TIME_M
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _START_TIME_M = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME_H", DbType:="SmallInt")>  _
        Public Property END_TIME_H() As  System.Nullable(Of Long) 
            Get
                Return _END_TIME_H
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _END_TIME_H = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME_M", DbType:="SmallInt")>  _
        Public Property END_TIME_M() As  System.Nullable(Of Long) 
            Get
                Return _END_TIME_M
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _END_TIME_M = value
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


    End Class
End Namespace
