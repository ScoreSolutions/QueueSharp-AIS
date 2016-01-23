
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for SYSCONFIG table Parameter.
    '[Create by  on Febuary, 16 2012]

    <Table(Name:="SYSCONFIG")>  _
    Public Class SysconfigCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CONFIG_NAME As String = ""
        Dim _CONFIG_VALUE As String = ""
        Dim _DISPLAY_LABEL_TH As  String  = ""
        Dim _DISPLAY_LABEL_EN As  String  = ""
        Dim _DESC As  String  = ""
        Dim _ROWGUID As String = ""

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
        <Column(Storage:="_CONFIG_NAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property CONFIG_NAME() As String
            Get
                Return _CONFIG_NAME
            End Get
            Set(ByVal value As String)
               _CONFIG_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CONFIG_VALUE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property CONFIG_VALUE() As String
            Get
                Return _CONFIG_VALUE
            End Get
            Set(ByVal value As String)
               _CONFIG_VALUE = value
            End Set
        End Property 
        <Column(Storage:="_DISPLAY_LABEL_TH", DbType:="VarChar(255)")>  _
        Public Property DISPLAY_LABEL_TH() As  String 
            Get
                Return _DISPLAY_LABEL_TH
            End Get
            Set(ByVal value As  String )
               _DISPLAY_LABEL_TH = value
            End Set
        End Property 
        <Column(Storage:="_DISPLAY_LABEL_EN", DbType:="VarChar(255)")>  _
        Public Property DISPLAY_LABEL_EN() As  String 
            Get
                Return _DISPLAY_LABEL_EN
            End Get
            Set(ByVal value As  String )
               _DISPLAY_LABEL_EN = value
            End Set
        End Property 
        <Column(Storage:="_DESC", DbType:="VarChar(500)")>  _
        Public Property DESC() As  String 
            Get
                Return _DESC
            End Get
            Set(ByVal value As  String )
               _DESC = value
            End Set
        End Property 
        <Column(Storage:="_ROWGUID", DbType:="UNIQUEIDENTIFIER NOT NULL ",CanBeNull:=false)>  _
        Public Property ROWGUID() As String
            Get
                Return _ROWGUID
            End Get
            Set(ByVal value As String)
               _ROWGUID = value
            End Set
        End Property 


    End Class
End Namespace
