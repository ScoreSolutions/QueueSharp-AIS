
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TW_SOURCE_TEXTFILE table Parameter.
    '[Create by  on November, 7 2013]

    <Table(Name:="TW_SOURCE_TEXTFILE")>  _
    Public Class TwSourceTextfileCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SOURCE_TYPE As String = ""
        Dim _IMPORT_TIME As DateTime = New DateTime(1,1,1)
        Dim _FILE_NAME As String = ""
        Dim _RECORD_COUNT As Long = 0

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
        <Column(Storage:="_SOURCE_TYPE", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property SOURCE_TYPE() As String
            Get
                Return _SOURCE_TYPE
            End Get
            Set(ByVal value As String)
               _SOURCE_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_IMPORT_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property IMPORT_TIME() As DateTime
            Get
                Return _IMPORT_TIME
            End Get
            Set(ByVal value As DateTime)
               _IMPORT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_FILE_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property FILE_NAME() As String
            Get
                Return _FILE_NAME
            End Get
            Set(ByVal value As String)
               _FILE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_RECORD_COUNT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property RECORD_COUNT() As Long
            Get
                Return _RECORD_COUNT
            End Get
            Set(ByVal value As Long)
               _RECORD_COUNT = value
            End Set
        End Property 


    End Class
End Namespace
