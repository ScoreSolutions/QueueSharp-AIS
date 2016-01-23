
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_TEMP_COMPARE_TRANSACTION table Parameter.
    '[Create by  on January, 1 2013]

    <Table(Name:="TB_TEMP_COMPARE_TRANSACTION")>  _
    Public Class TbTempCompareTransactionCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _COUNT_BEFORE As Long = 0
        Dim _COUNT_AFTER As Long = 0
        Dim _STATUS As Char = ""
        Dim _LAST_HISTORY_ID As Long = 0

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
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
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
        <Column(Storage:="_COUNT_BEFORE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_BEFORE() As Long
            Get
                Return _COUNT_BEFORE
            End Get
            Set(ByVal value As Long)
               _COUNT_BEFORE = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_AFTER", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_AFTER() As Long
            Get
                Return _COUNT_AFTER
            End Get
            Set(ByVal value As Long)
               _COUNT_AFTER = value
            End Set
        End Property 
        <Column(Storage:="_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property STATUS() As Char
            Get
                Return _STATUS
            End Get
            Set(ByVal value As Char)
               _STATUS = value
            End Set
        End Property 
        <Column(Storage:="_LAST_HISTORY_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property LAST_HISTORY_ID() As Long
            Get
                Return _LAST_HISTORY_ID
            End Get
            Set(ByVal value As Long)
               _LAST_HISTORY_ID = value
            End Set
        End Property 


    End Class
End Namespace
