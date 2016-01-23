
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT table Parameter.
    '[Create by  on January, 22 2012]

    <Table(Name:="TB_APPOINTMENT")>  _
    Public Class TbAppointmentShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _BEFORE_TIME As  System.Nullable(Of Long)  = 0
        Dim _LATE_TIME As  System.Nullable(Of Long)  = 0
        Dim _BEFORE_APP_TIME As  System.Nullable(Of Long)  = 0
        Dim _START_TIME_H As  System.Nullable(Of Long)  = 0
        Dim _START_TIME_M As  System.Nullable(Of Long)  = 0
        Dim _END_TIME_H As  System.Nullable(Of Long)  = 0
        Dim _END_TIME_M As  System.Nullable(Of Long)  = 0
        Dim _SLOT As  System.Nullable(Of Long)  = 0

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
        <Column(Storage:="_BEFORE_TIME", DbType:="SmallInt")>  _
        Public Property BEFORE_TIME() As  System.Nullable(Of Long) 
            Get
                Return _BEFORE_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BEFORE_TIME = value
            End Set
        End Property 
        <Column(Storage:="_LATE_TIME", DbType:="SmallInt")>  _
        Public Property LATE_TIME() As  System.Nullable(Of Long) 
            Get
                Return _LATE_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _LATE_TIME = value
            End Set
        End Property 
        <Column(Storage:="_BEFORE_APP_TIME", DbType:="SmallInt")>  _
        Public Property BEFORE_APP_TIME() As  System.Nullable(Of Long) 
            Get
                Return _BEFORE_APP_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BEFORE_APP_TIME = value
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
        <Column(Storage:="_SLOT", DbType:="SmallInt")>  _
        Public Property SLOT() As  System.Nullable(Of Long) 
            Get
                Return _SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SLOT = value
            End Set
        End Property 


    End Class
End Namespace
