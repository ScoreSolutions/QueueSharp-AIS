
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_FILTER_TEMP_TARGET table Parameter.
    '[Create by  on December, 28 2012]

    <Table(Name:="TB_FILTER_TEMP_TARGET")>  _
    Public Class TbFilterTempTargetCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_FILTER_ID As Long = 0
        Dim _TB_SHOP_ID As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _END_TIME_FROM As String = ""
        Dim _END_TIME_TO As String = ""
        Dim _TB_ITEM_ID As Long = 0
        Dim _USERNAME As  String  = ""
        Dim _TARGET As Long = 0

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
        <Column(Storage:="_TB_FILTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_FILTER_ID() As Long
            Get
                Return _TB_FILTER_ID
            End Get
            Set(ByVal value As Long)
               _TB_FILTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_TB_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_SHOP_ID() As Long
            Get
                Return _TB_SHOP_ID
            End Get
            Set(ByVal value As Long)
               _TB_SHOP_ID = value
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
        <Column(Storage:="_END_TIME_FROM", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property END_TIME_FROM() As String
            Get
                Return _END_TIME_FROM
            End Get
            Set(ByVal value As String)
               _END_TIME_FROM = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME_TO", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property END_TIME_TO() As String
            Get
                Return _END_TIME_TO
            End Get
            Set(ByVal value As String)
               _END_TIME_TO = value
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
        <Column(Storage:="_USERNAME", DbType:="Text")>  _
        Public Property USERNAME() As  String 
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As  String )
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_TARGET", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TARGET() As Long
            Get
                Return _TARGET
            End Get
            Set(ByVal value As Long)
               _TARGET = value
            End Set
        End Property 


    End Class
End Namespace
