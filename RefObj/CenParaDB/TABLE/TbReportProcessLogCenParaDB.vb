
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REPORT_PROCESS_LOG table Parameter.
    '[Create by  on April, 16 2012]

    <Table(Name:="TB_REPORT_PROCESS_LOG")>  _
    Public Class TbReportProcessLogCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _REPORT_NAME As String = ""
        Dim _PROC_START_DATE As DateTime = New DateTime(1,1,1)
        Dim _PROC_END_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ERR_MSG As  String  = ""

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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_REPORT_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property REPORT_NAME() As String
            Get
                Return _REPORT_NAME
            End Get
            Set(ByVal value As String)
               _REPORT_NAME = value
            End Set
        End Property 
        <Column(Storage:="_PROC_START_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property PROC_START_DATE() As DateTime
            Get
                Return _PROC_START_DATE
            End Get
            Set(ByVal value As DateTime)
               _PROC_START_DATE = value
            End Set
        End Property 
        <Column(Storage:="_PROC_END_DATE", DbType:="DateTime")>  _
        Public Property PROC_END_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _PROC_END_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _PROC_END_DATE = value
            End Set
        End Property 
        <Column(Storage:="_ERR_MSG", DbType:="Text")>  _
        Public Property ERR_MSG() As  String 
            Get
                Return _ERR_MSG
            End Get
            Set(ByVal value As  String )
               _ERR_MSG = value
            End Set
        End Property 


    End Class
End Namespace
