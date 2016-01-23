
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_EMAIL_BATCH_ALERT table Parameter.
    '[Create by  on December, 5 2012]

    <Table(Name:="TB_EMAIL_BATCH_ALERT")>  _
    Public Class TbEmailBatchAlertCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _EMAIL_ADDR As String = ""
        Dim _EF_DATE As DateTime = New DateTime(1,1,1)
        Dim _EP_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

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
        <Column(Storage:="_EMAIL_ADDR", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property EMAIL_ADDR() As String
            Get
                Return _EMAIL_ADDR
            End Get
            Set(ByVal value As String)
               _EMAIL_ADDR = value
            End Set
        End Property 
        <Column(Storage:="_EF_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property EF_DATE() As DateTime
            Get
                Return _EF_DATE
            End Get
            Set(ByVal value As DateTime)
               _EF_DATE = value
            End Set
        End Property 
        <Column(Storage:="_EP_DATE", DbType:="DateTime")>  _
        Public Property EP_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _EP_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _EP_DATE = value
            End Set
        End Property 


    End Class
End Namespace
