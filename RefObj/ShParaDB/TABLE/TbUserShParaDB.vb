
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_USER table Parameter.
    '[Create by  on December, 11 2012]

    <Table(Name:="TB_USER")>  _
    Public Class TbUserShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TITLE_ID As  System.Nullable(Of Long)  = 0
        Dim _USER_CODE As  String  = ""
        Dim _FNAME As  String  = ""
        Dim _LNAME As  String  = ""
        Dim _POSITION As  String  = ""
        Dim _GROUP_ID As  System.Nullable(Of Long)  = 0
        Dim _USERNAME As  String  = ""
        Dim _PASSWORD As  String  = ""
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _COUNTER_ID As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _IP_ADDRESS As  String  = ""
        Dim _CHECK_UPDATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

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
        <Column(Storage:="_TITLE_ID", DbType:="Int")>  _
        Public Property TITLE_ID() As  System.Nullable(Of Long) 
            Get
                Return _TITLE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TITLE_ID = value
            End Set
        End Property 
        <Column(Storage:="_USER_CODE", DbType:="VarChar(20)")>  _
        Public Property USER_CODE() As  String 
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As  String )
               _USER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_FNAME", DbType:="VarChar(50)")>  _
        Public Property FNAME() As  String 
            Get
                Return _FNAME
            End Get
            Set(ByVal value As  String )
               _FNAME = value
            End Set
        End Property 
        <Column(Storage:="_LNAME", DbType:="VarChar(50)")>  _
        Public Property LNAME() As  String 
            Get
                Return _LNAME
            End Get
            Set(ByVal value As  String )
               _LNAME = value
            End Set
        End Property 
        <Column(Storage:="_POSITION", DbType:="VarChar(50)")>  _
        Public Property POSITION() As  String 
            Get
                Return _POSITION
            End Get
            Set(ByVal value As  String )
               _POSITION = value
            End Set
        End Property 
        <Column(Storage:="_GROUP_ID", DbType:="Int")>  _
        Public Property GROUP_ID() As  System.Nullable(Of Long) 
            Get
                Return _GROUP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _GROUP_ID = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(20)")>  _
        Public Property USERNAME() As  String 
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As  String )
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_PASSWORD", DbType:="VarChar(200)")>  _
        Public Property PASSWORD() As  String 
            Get
                Return _PASSWORD
            End Get
            Set(ByVal value As  String )
               _PASSWORD = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE_STATUS", DbType:="SmallInt")>  _
        Public Property ACTIVE_STATUS() As  System.Nullable(Of Long) 
            Get
                Return _ACTIVE_STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ACTIVE_STATUS = value
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
        <Column(Storage:="_ITEM_ID", DbType:="Int")>  _
        Public Property ITEM_ID() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_IP_ADDRESS", DbType:="VarChar(50)")>  _
        Public Property IP_ADDRESS() As  String 
            Get
                Return _IP_ADDRESS
            End Get
            Set(ByVal value As  String )
               _IP_ADDRESS = value
            End Set
        End Property 
        <Column(Storage:="_CHECK_UPDATE", DbType:="DateTime")>  _
        Public Property CHECK_UPDATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CHECK_UPDATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CHECK_UPDATE = value
            End Set
        End Property 


    End Class
End Namespace
