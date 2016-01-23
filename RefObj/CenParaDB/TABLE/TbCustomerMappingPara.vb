
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_CUSTOMER_MAPPING table Parameter.
    '[Create by  on July, 27 2012]

    <Table(Name:="TB_CUSTOMER_MAPPING")>  _
    Public Class TbCustomerMappingPara

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _FIELD_NAME As String = ""
        Dim _MAPPING_CODE As String = ""
        Dim _DISPLAY_VALUE As String = ""

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
        <Column(Storage:="_FIELD_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property FIELD_NAME() As String
            Get
                Return _FIELD_NAME
            End Get
            Set(ByVal value As String)
               _FIELD_NAME = value
            End Set
        End Property 
        <Column(Storage:="_MAPPING_CODE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property MAPPING_CODE() As String
            Get
                Return _MAPPING_CODE
            End Get
            Set(ByVal value As String)
               _MAPPING_CODE = value
            End Set
        End Property 
        <Column(Storage:="_DISPLAY_VALUE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property DISPLAY_VALUE() As String
            Get
                Return _DISPLAY_VALUE
            End Get
            Set(ByVal value As String)
               _DISPLAY_VALUE = value
            End Set
        End Property 


    End Class
End Namespace
