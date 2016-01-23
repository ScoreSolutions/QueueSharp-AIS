
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_COUNTER_CUSTOMERTYPE_ALLOW_HISTORY table Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="TB_COUNTER_CUSTOMERTYPE_ALLOW_HISTORY")>  _
    Public Class TbCounterCustomertypeAllowHistoryShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _COUNTER_ID As  System.Nullable(Of Long)  = 0
        Dim _CUSTOMERTYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_COUNTER_CUSTOMERTYPE_ALLOW_ID As Long = 0

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
        <Column(Storage:="_COUNTER_ID", DbType:="Int")>  _
        Public Property COUNTER_ID() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_ID", DbType:="Int")>  _
        Public Property CUSTOMERTYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _CUSTOMERTYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CUSTOMERTYPE_ID = value
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
        <Column(Storage:="_TB_COUNTER_CUSTOMERTYPE_ALLOW_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_COUNTER_CUSTOMERTYPE_ALLOW_ID() As Long
            Get
                Return _TB_COUNTER_CUSTOMERTYPE_ALLOW_ID
            End Get
            Set(ByVal value As Long)
               _TB_COUNTER_CUSTOMERTYPE_ALLOW_ID = value
            End Set
        End Property 


    End Class
End Namespace
