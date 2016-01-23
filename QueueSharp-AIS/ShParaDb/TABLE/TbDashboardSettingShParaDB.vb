
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_DASHBOARD_SETTING table Parameter.
    '[Create by  on June, 23 2012]

    <Table(Name:="TB_DASHBOARD_SETTING")>  _
    Public Class TbDashboardSettingShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _CSI_HIGH As  System.Nullable(Of Long)  = 0
        Dim _CSI_LOW As  System.Nullable(Of Long)  = 0
        Dim _KPI_HIGH As  System.Nullable(Of Long)  = 0
        Dim _KPI_LOW As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

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
        <Column(Storage:="_ITEM_ID", DbType:="Int")>  _
        Public Property ITEM_ID() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_CSI_HIGH", DbType:="SmallInt")>  _
        Public Property CSI_HIGH() As  System.Nullable(Of Long) 
            Get
                Return _CSI_HIGH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CSI_HIGH = value
            End Set
        End Property 
        <Column(Storage:="_CSI_LOW", DbType:="SmallInt")>  _
        Public Property CSI_LOW() As  System.Nullable(Of Long) 
            Get
                Return _CSI_LOW
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CSI_LOW = value
            End Set
        End Property 
        <Column(Storage:="_KPI_HIGH", DbType:="SmallInt")>  _
        Public Property KPI_HIGH() As  System.Nullable(Of Long) 
            Get
                Return _KPI_HIGH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _KPI_HIGH = value
            End Set
        End Property 
        <Column(Storage:="_KPI_LOW", DbType:="SmallInt")>  _
        Public Property KPI_LOW() As  System.Nullable(Of Long) 
            Get
                Return _KPI_LOW
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _KPI_LOW = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50)")>  _
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


    End Class
End Namespace
