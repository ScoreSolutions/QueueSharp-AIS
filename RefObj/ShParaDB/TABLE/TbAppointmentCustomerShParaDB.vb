
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_APPOINTMENT_CUSTOMER table Parameter.
    '[Create by  on April, 25 2013]

    <Table(Name:="TB_APPOINTMENT_CUSTOMER")>  _
    Public Class TbAppointmentCustomerShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _APP_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CAPACITY As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _CUSTOMER_ID As  String  = ""
        Dim _START_SLOT As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_SLOT As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _APPOINTMENT_CHANNEL As  System.Nullable(Of Char)  = "((1))"
        Dim _SIEBEL_ACTIVITY_ID As  String  = ""
        Dim _SIEBEL_STATUS As  String  = ""
        Dim _SIEBEL_DESC As  String  = ""
        Dim _CREATE_BY As  System.Nullable(Of Long)  = 0
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  System.Nullable(Of Long)  = 0
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CUSTOMER_EMAIL As  String  = ""
        Dim _QUEUE_NO As  String  = ""
        Dim _SERVICE As  String  = ""
        Dim _APPOINTMENT_JOB_ID As  System.Nullable(Of Long)  = 0

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
        <Column(Storage:="_APP_DATE", DbType:="DateTime")>  _
        Public Property APP_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_CAPACITY", DbType:="Int")>  _
        Public Property CAPACITY() As  System.Nullable(Of Long) 
            Get
                Return _CAPACITY
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CAPACITY = value
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
        <Column(Storage:="_CUSTOMER_ID", DbType:="VarChar(20)")>  _
        Public Property CUSTOMER_ID() As  String 
            Get
                Return _CUSTOMER_ID
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_ID = value
            End Set
        End Property 
        <Column(Storage:="_START_SLOT", DbType:="DateTime")>  _
        Public Property START_SLOT() As  System.Nullable(Of DateTime) 
            Get
                Return _START_SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _START_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_END_SLOT", DbType:="DateTime")>  _
        Public Property END_SLOT() As  System.Nullable(Of DateTime) 
            Get
                Return _END_SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _END_SLOT = value
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
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(1)")>  _
        Public Property APPOINTMENT_CHANNEL() As  System.Nullable(Of Char) 
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _APPOINTMENT_CHANNEL = value
            End Set
        End Property 
        <Column(Storage:="_SIEBEL_ACTIVITY_ID", DbType:="VarChar(50)")>  _
        Public Property SIEBEL_ACTIVITY_ID() As  String 
            Get
                Return _SIEBEL_ACTIVITY_ID
            End Get
            Set(ByVal value As  String )
               _SIEBEL_ACTIVITY_ID = value
            End Set
        End Property 
        <Column(Storage:="_SIEBEL_STATUS", DbType:="VarChar(50)")>  _
        Public Property SIEBEL_STATUS() As  String 
            Get
                Return _SIEBEL_STATUS
            End Get
            Set(ByVal value As  String )
               _SIEBEL_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_SIEBEL_DESC", DbType:="VarChar(500)")>  _
        Public Property SIEBEL_DESC() As  String 
            Get
                Return _SIEBEL_DESC
            End Get
            Set(ByVal value As  String )
               _SIEBEL_DESC = value
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
        <Column(Storage:="_CUSTOMER_EMAIL", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_EMAIL() As  String 
            Get
                Return _CUSTOMER_EMAIL
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_EMAIL = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(20)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE", DbType:="VarChar(100)")>  _
        Public Property SERVICE() As  String 
            Get
                Return _SERVICE
            End Get
            Set(ByVal value As  String )
               _SERVICE = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_JOB_ID", DbType:="BigInt")>  _
        Public Property APPOINTMENT_JOB_ID() As  System.Nullable(Of Long) 
            Get
                Return _APPOINTMENT_JOB_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _APPOINTMENT_JOB_ID = value
            End Set
        End Property 


    End Class
End Namespace
