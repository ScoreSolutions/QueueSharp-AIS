
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT_JOB table Parameter.
    '[Create by  on April, 25 2013]

    <Table(Name:="TB_APPOINTMENT_JOB")>  _
    Public Class TbAppointmentJobCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ABB As String = ""
        Dim _MOBILE_NO As String = ""
        Dim _APP_DATE As DateTime = New DateTime(1,1,1)
        Dim _START_SLOT As DateTime = New DateTime(1,1,1)
        Dim _APPOINTMENT_CHANNEL As Char = ""
        Dim _ACTIVE_STATUS As Char = ""

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
        <Column(Storage:="_SHOP_ABB", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ABB() As String
            Get
                Return _SHOP_ABB
            End Get
            Set(ByVal value As String)
               _SHOP_ABB = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_APP_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property APP_DATE() As DateTime
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As DateTime)
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_START_SLOT", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property START_SLOT() As DateTime
            Get
                Return _START_SLOT
            End Get
            Set(ByVal value As DateTime)
               _START_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_CHANNEL() As Char
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As Char)
               _APPOINTMENT_CHANNEL = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTIVE_STATUS() As Char
            Get
                Return _ACTIVE_STATUS
            End Get
            Set(ByVal value As Char)
               _ACTIVE_STATUS = value
            End Set
        End Property 


    End Class
End Namespace
