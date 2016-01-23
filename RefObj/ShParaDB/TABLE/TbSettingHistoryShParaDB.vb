
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_SETTING_HISTORY table Parameter.
    '[Create by  on March, 5 2012]

    <Table(Name:="TB_SETTING_HISTORY")>  _
    Public Class TbSettingHistoryShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CONFIG_NAME As  String  = ""
        Dim _CONFIG_VALUE As  String  = ""
        Dim _CONFIG_DESC As  String  = ""
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_SETTING_ID As Long = 0

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
        <Column(Storage:="_CONFIG_NAME", DbType:="VarChar(100)")>  _
        Public Property CONFIG_NAME() As  String 
            Get
                Return _CONFIG_NAME
            End Get
            Set(ByVal value As  String )
               _CONFIG_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CONFIG_VALUE", DbType:="VarChar(100)")>  _
        Public Property CONFIG_VALUE() As  String 
            Get
                Return _CONFIG_VALUE
            End Get
            Set(ByVal value As  String )
               _CONFIG_VALUE = value
            End Set
        End Property 
        <Column(Storage:="_CONFIG_DESC", DbType:="VarChar(255)")>  _
        Public Property CONFIG_DESC() As  String 
            Get
                Return _CONFIG_DESC
            End Get
            Set(ByVal value As  String )
               _CONFIG_DESC = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="Int")>  _
        Public Property CREATE_BY() As  System.Nullable(Of Long) 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
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
        <Column(Storage:="_UPDATE_BY", DbType:="Int")>  _
        Public Property UPDATE_BY() As  System.Nullable(Of Long) 
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
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
        <Column(Storage:="_TB_SETTING_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_SETTING_ID() As Long
            Get
                Return _TB_SETTING_ID
            End Get
            Set(ByVal value As Long)
               _TB_SETTING_ID = value
            End Set
        End Property 


    End Class
End Namespace
