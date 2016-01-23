
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CFG_COUNTER_CUSTTYPE_ALLOW table Parameter.
    '[Create by  on July, 16 2013]

    <Table(Name:="TB_CFG_COUNTER_CUSTTYPE_ALLOW")>  _
    Public Class TbCfgCounterCusttypeAllowCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_CFG_COUNTER_ID As Long = 0
        Dim _TB_CUSTOMERTYPE_ID As Long = 0

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
        <Column(Storage:="_TB_CFG_COUNTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_CFG_COUNTER_ID() As Long
            Get
                Return _TB_CFG_COUNTER_ID
            End Get
            Set(ByVal value As Long)
               _TB_CFG_COUNTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_TB_CUSTOMERTYPE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_CUSTOMERTYPE_ID() As Long
            Get
                Return _TB_CUSTOMERTYPE_ID
            End Get
            Set(ByVal value As Long)
               _TB_CUSTOMERTYPE_ID = value
            End Set
        End Property 


    End Class
End Namespace
