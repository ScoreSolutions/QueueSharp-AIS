
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_FILTER_SERVICE table Parameter.
    '[Create by  on December, 27 2012]

    <Table(Name:="TB_FILTER_SERVICE")>  _
    Public Class TbFilterServiceCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_FILTER_ID As Long = 0
        Dim _TB_ITEM_ID As Long = 0
        Dim _TARGET_PERCENT As  System.Nullable(Of Long)  = 0
        Dim _CHK As Char = "N"
        Dim _TARGET_PERHOUR As Long = 0

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
        <Column(Storage:="_TB_FILTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_FILTER_ID() As Long
            Get
                Return _TB_FILTER_ID
            End Get
            Set(ByVal value As Long)
               _TB_FILTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_TB_ITEM_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_ITEM_ID() As Long
            Get
                Return _TB_ITEM_ID
            End Get
            Set(ByVal value As Long)
               _TB_ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_TARGET_PERCENT", DbType:="Int")>  _
        Public Property TARGET_PERCENT() As  System.Nullable(Of Long) 
            Get
                Return _TARGET_PERCENT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TARGET_PERCENT = value
            End Set
        End Property 
        <Column(Storage:="_CHK", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property CHK() As Char
            Get
                Return _CHK
            End Get
            Set(ByVal value As Char)
               _CHK = value
            End Set
        End Property 
        <Column(Storage:="_TARGET_PERHOUR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TARGET_PERHOUR() As Long
            Get
                Return _TARGET_PERHOUR
            End Get
            Set(ByVal value As Long)
               _TARGET_PERHOUR = value
            End Set
        End Property 


    End Class
End Namespace
