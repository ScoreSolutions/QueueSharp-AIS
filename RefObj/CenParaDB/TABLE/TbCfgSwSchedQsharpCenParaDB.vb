
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CFG_SW_SCHED_QSHARP table Parameter.
    '[Create by  on June, 26 2013]

    <Table(Name:="TB_CFG_SW_SCHED_QSHARP")>  _
    Public Class TbCfgSwSchedQsharpCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As  System.Nullable(Of Long)  = 0
        Dim _EVENT_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _Q_REFRESH As  System.Nullable(Of Long)  = 0
        Dim _Q_CON_LDAP As  System.Nullable(Of Char)  = "1"
        Dim _EVENT_STATUS As  System.Nullable(Of Char)  = "1"

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
        <Column(Storage:="_SHOP_ID", DbType:="Int")>  _
        Public Property SHOP_ID() As  System.Nullable(Of Long) 
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_EVENT_DATE", DbType:="DateTime")>  _
        Public Property EVENT_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _EVENT_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _EVENT_DATE = value
            End Set
        End Property 
        <Column(Storage:="_Q_REFRESH", DbType:="Int")>  _
        Public Property Q_REFRESH() As  System.Nullable(Of Long) 
            Get
                Return _Q_REFRESH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _Q_REFRESH = value
            End Set
        End Property 
        <Column(Storage:="_Q_CON_LDAP", DbType:="Char(1)")>  _
        Public Property Q_CON_LDAP() As  System.Nullable(Of Char) 
            Get
                Return _Q_CON_LDAP
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _Q_CON_LDAP = value
            End Set
        End Property 
        <Column(Storage:="_EVENT_STATUS", DbType:="Char(1)")>  _
        Public Property EVENT_STATUS() As  System.Nullable(Of Char) 
            Get
                Return _EVENT_STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _EVENT_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
