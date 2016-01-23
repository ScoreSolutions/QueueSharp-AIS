
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_SPEAKER table Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="TB_SPEAKER")>  _
    Public Class TbSpeakerShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _QUEUE_NO As  String  = ""
        Dim _COUNTER_ID As  System.Nullable(Of Long)  = 0
        Dim _COUNTER_NAME As  String  = ""
        Dim _NATIONALITY As  String  = ""
        Dim _CALL_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _STATUS As  System.Nullable(Of Char)  = ""

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
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(20)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
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
        <Column(Storage:="_COUNTER_NAME", DbType:="VarChar(50)")>  _
        Public Property COUNTER_NAME() As  String 
            Get
                Return _COUNTER_NAME
            End Get
            Set(ByVal value As  String )
               _COUNTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_NATIONALITY", DbType:="VarChar(4)")>  _
        Public Property NATIONALITY() As  String 
            Get
                Return _NATIONALITY
            End Get
            Set(ByVal value As  String )
               _NATIONALITY = value
            End Set
        End Property 
        <Column(Storage:="_CALL_DATE", DbType:="DateTime")>  _
        Public Property CALL_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CALL_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CALL_DATE = value
            End Set
        End Property 
        <Column(Storage:="_STATUS", DbType:="Char(1)")>  _
        Public Property STATUS() As  System.Nullable(Of Char) 
            Get
                Return _STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _STATUS = value
            End Set
        End Property 


    End Class
End Namespace
