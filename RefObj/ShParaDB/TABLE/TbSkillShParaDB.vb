
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_SKILL table Parameter.
    '[Create by  on July, 2 2013]

    <Table(Name:="TB_SKILL")>  _
    Public Class TbSkillShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _SKILL As  String  = ""
        Dim _APPOINTMENT As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _MASTER_SKILLID As  String  = ""

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
        <Column(Storage:="_SKILL", DbType:="VarChar(100)")>  _
        Public Property SKILL() As  String 
            Get
                Return _SKILL
            End Get
            Set(ByVal value As  String )
               _SKILL = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT", DbType:="SmallInt")>  _
        Public Property APPOINTMENT() As  System.Nullable(Of Long) 
            Get
                Return _APPOINTMENT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _APPOINTMENT = value
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
        <Column(Storage:="_MASTER_SKILLID", DbType:="VarChar(50)")>  _
        Public Property MASTER_SKILLID() As  String 
            Get
                Return _MASTER_SKILLID
            End Get
            Set(ByVal value As  String )
               _MASTER_SKILLID = value
            End Set
        End Property 


    End Class
End Namespace