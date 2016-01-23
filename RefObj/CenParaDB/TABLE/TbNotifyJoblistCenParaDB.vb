
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_NOTIFY_JOBLIST table Parameter.
    '[Create by  on April, 12 2012]

    <Table(Name:="TB_NOTIFY_JOBLIST")>  _
    Public Class TbNotifyJoblistCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As  System.Nullable(Of Long)  = 0
        Dim _MOBILE_NO As String = ""
        Dim _APPOINTMENT_CHANNEL As Char = ""
        Dim _APPOINTMENT_TIME As DateTime = New DateTime(1,1,1)
        Dim _SMS_TIME1 As DateTime = New DateTime(1,1,1)
        Dim _SMS_ALERT1 As Char = ""
        Dim _SMS_MSG1 As String = ""
        Dim _SMS_TIME2 As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SMS_ALERT2 As  System.Nullable(Of Char)  = ""
        Dim _SMS_MSG2 As  String  = ""
        Dim _CUSTOMER_EMAIL As  String  = ""
        Dim _EMAIL_TIME1 As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _EMAIL_ALERT1 As  System.Nullable(Of Char)  = ""
        Dim _EMAIL_MSG1 As  String  = ""
        Dim _EMAIL_TIME2 As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _EMAIL_ALERT2 As  System.Nullable(Of Char)  = ""
        Dim _EMAIL_MSG2 As  String  = ""

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
        <Column(Storage:="_SHOP_ID", DbType:="Int")>  _
        Public Property SHOP_ID() As  System.Nullable(Of Long) 
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SHOP_ID = value
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
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_CHANNEL() As Char
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As Char)
               _APPOINTMENT_CHANNEL = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_TIME() As DateTime
            Get
                Return _APPOINTMENT_TIME
            End Get
            Set(ByVal value As DateTime)
               _APPOINTMENT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SMS_TIME1", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SMS_TIME1() As DateTime
            Get
                Return _SMS_TIME1
            End Get
            Set(ByVal value As DateTime)
               _SMS_TIME1 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_ALERT1", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SMS_ALERT1() As Char
            Get
                Return _SMS_ALERT1
            End Get
            Set(ByVal value As Char)
               _SMS_ALERT1 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_MSG1", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property SMS_MSG1() As String
            Get
                Return _SMS_MSG1
            End Get
            Set(ByVal value As String)
               _SMS_MSG1 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_TIME2", DbType:="DateTime")>  _
        Public Property SMS_TIME2() As  System.Nullable(Of DateTime) 
            Get
                Return _SMS_TIME2
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SMS_TIME2 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_ALERT2", DbType:="Char(1)")>  _
        Public Property SMS_ALERT2() As  System.Nullable(Of Char) 
            Get
                Return _SMS_ALERT2
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _SMS_ALERT2 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_MSG2", DbType:="VarChar(500)")>  _
        Public Property SMS_MSG2() As  String 
            Get
                Return _SMS_MSG2
            End Get
            Set(ByVal value As  String )
               _SMS_MSG2 = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_EMAIL", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_EMAIL() As  String 
            Get
                Return _CUSTOMER_EMAIL
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_EMAIL = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_TIME1", DbType:="DateTime")>  _
        Public Property EMAIL_TIME1() As  System.Nullable(Of DateTime) 
            Get
                Return _EMAIL_TIME1
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _EMAIL_TIME1 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_ALERT1", DbType:="Char(1)")>  _
        Public Property EMAIL_ALERT1() As  System.Nullable(Of Char) 
            Get
                Return _EMAIL_ALERT1
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _EMAIL_ALERT1 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_MSG1", DbType:="VarChar(500)")>  _
        Public Property EMAIL_MSG1() As  String 
            Get
                Return _EMAIL_MSG1
            End Get
            Set(ByVal value As  String )
               _EMAIL_MSG1 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_TIME2", DbType:="DateTime")>  _
        Public Property EMAIL_TIME2() As  System.Nullable(Of DateTime) 
            Get
                Return _EMAIL_TIME2
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _EMAIL_TIME2 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_ALERT2", DbType:="Char(1)")>  _
        Public Property EMAIL_ALERT2() As  System.Nullable(Of Char) 
            Get
                Return _EMAIL_ALERT2
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _EMAIL_ALERT2 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_MSG2", DbType:="VarChar(500)")>  _
        Public Property EMAIL_MSG2() As  String 
            Get
                Return _EMAIL_MSG2
            End Get
            Set(ByVal value As  String )
               _EMAIL_MSG2 = value
            End Set
        End Property 


    End Class
End Namespace
