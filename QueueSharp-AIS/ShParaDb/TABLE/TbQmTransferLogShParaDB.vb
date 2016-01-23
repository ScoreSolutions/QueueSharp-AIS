
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_QM_TRANSFER_LOG table Parameter.
    '[Create by  on October, 10 2013]

    <Table(Name:="TB_QM_TRANSFER_LOG")>  _
    Public Class TbQmTransferLogShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _TRANSFER_DATE As DateTime = New DateTime(1,1,1)
        Dim _TB_COUNTER_QUEUE_ID As Long = 0
        Dim _QUEUE_NO As String = ""
        Dim _MOBILE_NO As  String  = ""
        Dim _ITEM_ID As Long = 0
        Dim _ITEM_NAME_EN As String = ""
        Dim _COUNTER_ID As Long = 0
        Dim _COUNTER_NAME As String = ""
        Dim _STATUS As Char = "1"

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(100)")>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(100)")>  _
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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TRANSFER_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TRANSFER_DATE() As DateTime
            Get
                Return _TRANSFER_DATE
            End Get
            Set(ByVal value As DateTime)
               _TRANSFER_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TB_COUNTER_QUEUE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_COUNTER_QUEUE_ID() As Long
            Get
                Return _TB_COUNTER_QUEUE_ID
            End Get
            Set(ByVal value As Long)
               _TB_COUNTER_QUEUE_ID = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property QUEUE_NO() As String
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As String)
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50)")>  _
        Public Property MOBILE_NO() As  String 
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As  String )
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_ID() As Long
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As Long)
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME_EN() As String
            Get
                Return _ITEM_NAME_EN
            End Get
            Set(ByVal value As String)
               _ITEM_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_ID() As Long
            Get
                Return _COUNTER_ID
            End Get
            Set(ByVal value As Long)
               _COUNTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_NAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_NAME() As String
            Get
                Return _COUNTER_NAME
            End Get
            Set(ByVal value As String)
               _COUNTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property STATUS() As Char
            Get
                Return _STATUS
            End Get
            Set(ByVal value As Char)
               _STATUS = value
            End Set
        End Property 


    End Class
End Namespace
