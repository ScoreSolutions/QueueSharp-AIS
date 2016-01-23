
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_APPOINTMENT_SERVICE_DATE table Parameter.
    '[Create by  on March, 25 2013]

    <Table(Name:="TB_REP_APPOINTMENT_SERVICE_DATE")>  _
    Public Class TbRepAppointmentServiceDateCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _TRANSACTION_DATE As DateTime = New DateTime(1,1,1)
        Dim _APPOINTMENT_DATE As DateTime = New DateTime(1,1,1)
        Dim _APPOINTMENT_CHANNEL As  System.Nullable(Of Char)  = ""
        Dim _MOBILE_NO As String = ""
        Dim _SEGMENT As String = ""
        Dim _USER_CODE As  String  = ""
        Dim _STAFF_NAME As  String  = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _APPOINTMENT_STATUS As String = ""
        Dim _REMARKS As  String  = ""

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
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_EN() As String
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_TRANSACTION_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TRANSACTION_DATE() As DateTime
            Get
                Return _TRANSACTION_DATE
            End Get
            Set(ByVal value As DateTime)
               _TRANSACTION_DATE = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_DATE() As DateTime
            Get
                Return _APPOINTMENT_DATE
            End Get
            Set(ByVal value As DateTime)
               _APPOINTMENT_DATE = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(10)")>  _
        Public Property APPOINTMENT_CHANNEL() As  System.Nullable(Of Char) 
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _APPOINTMENT_CHANNEL = value
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
        <Column(Storage:="_SEGMENT", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SEGMENT() As String
            Get
                Return _SEGMENT
            End Get
            Set(ByVal value As String)
               _SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_USER_CODE", DbType:="VarChar(50)")>  _
        Public Property USER_CODE() As  String 
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As  String )
               _USER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255)")>  _
        Public Property STAFF_NAME() As  String 
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As  String )
               _STAFF_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As Long
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As Long)
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_EN() As String
            Get
                Return _SERVICE_NAME_EN
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_TH() As String
            Get
                Return _SERVICE_NAME_TH
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_STATUS", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_STATUS() As String
            Get
                Return _APPOINTMENT_STATUS
            End Get
            Set(ByVal value As String)
               _APPOINTMENT_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_REMARKS", DbType:="VarChar(255)")>  _
        Public Property REMARKS() As  String 
            Get
                Return _REMARKS
            End Get
            Set(ByVal value As  String )
               _REMARKS = value
            End Set
        End Property 


    End Class
End Namespace
