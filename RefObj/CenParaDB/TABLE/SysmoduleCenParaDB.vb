
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for SYSMODULE table Parameter.
    '[Create by  on June, 24 2013]

    <Table(Name:="SYSMODULE")>  _
    Public Class SysmoduleCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SYSTEM_APP As String = ""
        Dim _MODULE_NAME_TH As String = ""
        Dim _MODULE_NAME_EN As  String  = ""
        Dim _MODULE_DESC_TH As  String  = ""
        Dim _MODULE_DESC_EN As  String  = ""
        Dim _MODULE_URL As  String  = ""
        Dim _ORDER_SEQ As Long = 0
        Dim _ACTIVE As Char = "Y"

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
        <Column(Storage:="_SYSTEM_APP", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SYSTEM_APP() As String
            Get
                Return _SYSTEM_APP
            End Get
            Set(ByVal value As String)
               _SYSTEM_APP = value
            End Set
        End Property 
        <Column(Storage:="_MODULE_NAME_TH", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property MODULE_NAME_TH() As String
            Get
                Return _MODULE_NAME_TH
            End Get
            Set(ByVal value As String)
               _MODULE_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_MODULE_NAME_EN", DbType:="VarChar(100)")>  _
        Public Property MODULE_NAME_EN() As  String 
            Get
                Return _MODULE_NAME_EN
            End Get
            Set(ByVal value As  String )
               _MODULE_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_MODULE_DESC_TH", DbType:="VarChar(255)")>  _
        Public Property MODULE_DESC_TH() As  String 
            Get
                Return _MODULE_DESC_TH
            End Get
            Set(ByVal value As  String )
               _MODULE_DESC_TH = value
            End Set
        End Property 
        <Column(Storage:="_MODULE_DESC_EN", DbType:="VarChar(255)")>  _
        Public Property MODULE_DESC_EN() As  String 
            Get
                Return _MODULE_DESC_EN
            End Get
            Set(ByVal value As  String )
               _MODULE_DESC_EN = value
            End Set
        End Property 
        <Column(Storage:="_MODULE_URL", DbType:="VarChar(255)")>  _
        Public Property MODULE_URL() As  String 
            Get
                Return _MODULE_URL
            End Get
            Set(ByVal value As  String )
               _MODULE_URL = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_SEQ", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_SEQ() As Long
            Get
                Return _ORDER_SEQ
            End Get
            Set(ByVal value As Long)
               _ORDER_SEQ = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTIVE() As Char
            Get
                Return _ACTIVE
            End Get
            Set(ByVal value As Char)
               _ACTIVE = value
            End Set
        End Property 


    End Class
End Namespace
