
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TW_FILTER table Parameter.
    '[Create by  on October, 19 2014]

    <Table(Name:="TW_FILTER")>  _
    Public Class TwFilterCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _FILTER_NAME As String = ""
        Dim _NATIONALITY As  String  = ""
        Dim _NETWORK_TYPE As String = ""
        Dim _SEGMENT As String = ""
        Dim _ORDER_PAYMENT_PER As Double = 0
        Dim _ORDER_SFF_PER As Double = 0
        Dim _CHK_ORDER_SFF As  System.Nullable(Of Char)  = ""
        Dim _PREIOD_DATEFROM As  System.Nullable(Of Date)  = New DateTime(1,1,1)
        Dim _PREIOD_DATETO As  System.Nullable(Of Date)  = New DateTime(1,1,1)
        Dim _PREIOD_TIMEFROM As  String  = ""
        Dim _PREIOD_TIMETO As  String  = ""
        Dim _SCHEDULETYPEDAY As Char = ""
        Dim _SCHEDULEMONDAY As Char = ""
        Dim _SCHEDULETUEDAY As Char = ""
        Dim _SCHEDULEWEDDAY As Char = ""
        Dim _SCHEDULETHUDAY As Char = ""
        Dim _SCHEDULEFRIDAY As Char = ""
        Dim _SCHEDULESATDAY As Char = ""
        Dim _SCHEDULESUNDAY As Char = ""
        Dim _TARGET As  System.Nullable(Of Long)  = 0
        Dim _TARGET_UNLIMITED As Char = ""
        Dim _TEMPLATE_CODE As String = ""
        Dim _ACTIVE_STATUS As Char = ""
        Dim _PROCESS_STATUS As Char = ""
        Dim _LAST_SAVE_FILTER As DateTime = New DateTime(1,1,1)
        Dim _CAL_TARGET As  System.Nullable(Of Char)  = ""
        Dim _GEN_ACT_ALL_SURVEY As Char = "N"
        Dim _CATEGORY_GEN_ACT_ALL_SURVEY As  String  = ""
        Dim _SUBCATEGORY_GEN_ACT_ALL_SURVEY As  String  = ""
        Dim _CAT_GEN_ACT_ALL_SURVEY_UPC As  String  = ""
        Dim _SUBCAT_GEN_ACT_ALL_SURVEY_UPC As  String  = ""
        Dim _OWNER_GEN_ACT_ALL_SURVEY As String = "QISSYS"
        Dim _GEN_ACT_RESULT3 As Char = "N"
        Dim _CATEGORY_GEN_ACT_RESULT3 As  String  = ""
        Dim _SUBCATEGORY_GEN_ACT_RESULT3 As  String  = ""
        Dim _CAT_GEN_ACT_RESULT3_UPC As  String  = ""
        Dim _SUBCAT_GEN_ACT_RESULT3_UPC As  String  = ""
        Dim _OWNER_GEN_ACT_RESULT3 As String = "QAI"

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
        <Column(Storage:="_FILTER_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property FILTER_NAME() As String
            Get
                Return _FILTER_NAME
            End Get
            Set(ByVal value As String)
               _FILTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_NATIONALITY", DbType:="VarChar(100)")>  _
        Public Property NATIONALITY() As  String 
            Get
                Return _NATIONALITY
            End Get
            Set(ByVal value As  String )
               _NATIONALITY = value
            End Set
        End Property 
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NETWORK_TYPE() As String
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As String)
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_SEGMENT", DbType:="Text NOT NULL ",CanBeNull:=false)>  _
        Public Property SEGMENT() As String
            Get
                Return _SEGMENT
            End Get
            Set(ByVal value As String)
               _SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_PAYMENT_PER", DbType:="Double NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_PAYMENT_PER() As Double
            Get
                Return _ORDER_PAYMENT_PER
            End Get
            Set(ByVal value As Double)
               _ORDER_PAYMENT_PER = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_SFF_PER", DbType:="Double NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_SFF_PER() As Double
            Get
                Return _ORDER_SFF_PER
            End Get
            Set(ByVal value As Double)
               _ORDER_SFF_PER = value
            End Set
        End Property 
        <Column(Storage:="_CHK_ORDER_SFF", DbType:="Char(1)")>  _
        Public Property CHK_ORDER_SFF() As  System.Nullable(Of Char) 
            Get
                Return _CHK_ORDER_SFF
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _CHK_ORDER_SFF = value
            End Set
        End Property 
        <Column(Storage:="_PREIOD_DATEFROM", DbType:="Date")>  _
        Public Property PREIOD_DATEFROM() As  System.Nullable(Of Date) 
            Get
                Return _PREIOD_DATEFROM
            End Get
            Set(ByVal value As  System.Nullable(Of Date) )
               _PREIOD_DATEFROM = value
            End Set
        End Property 
        <Column(Storage:="_PREIOD_DATETO", DbType:="Date")>  _
        Public Property PREIOD_DATETO() As  System.Nullable(Of Date) 
            Get
                Return _PREIOD_DATETO
            End Get
            Set(ByVal value As  System.Nullable(Of Date) )
               _PREIOD_DATETO = value
            End Set
        End Property 
        <Column(Storage:="_PREIOD_TIMEFROM", DbType:="VarChar(5)")>  _
        Public Property PREIOD_TIMEFROM() As  String 
            Get
                Return _PREIOD_TIMEFROM
            End Get
            Set(ByVal value As  String )
               _PREIOD_TIMEFROM = value
            End Set
        End Property 
        <Column(Storage:="_PREIOD_TIMETO", DbType:="VarChar(5)")>  _
        Public Property PREIOD_TIMETO() As  String 
            Get
                Return _PREIOD_TIMETO
            End Get
            Set(ByVal value As  String )
               _PREIOD_TIMETO = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULETYPEDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULETYPEDAY() As Char
            Get
                Return _SCHEDULETYPEDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULETYPEDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULEMONDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULEMONDAY() As Char
            Get
                Return _SCHEDULEMONDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULEMONDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULETUEDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULETUEDAY() As Char
            Get
                Return _SCHEDULETUEDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULETUEDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULEWEDDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULEWEDDAY() As Char
            Get
                Return _SCHEDULEWEDDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULEWEDDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULETHUDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULETHUDAY() As Char
            Get
                Return _SCHEDULETHUDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULETHUDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULEFRIDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULEFRIDAY() As Char
            Get
                Return _SCHEDULEFRIDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULEFRIDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULESATDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULESATDAY() As Char
            Get
                Return _SCHEDULESATDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULESATDAY = value
            End Set
        End Property 
        <Column(Storage:="_SCHEDULESUNDAY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SCHEDULESUNDAY() As Char
            Get
                Return _SCHEDULESUNDAY
            End Get
            Set(ByVal value As Char)
               _SCHEDULESUNDAY = value
            End Set
        End Property 
        <Column(Storage:="_TARGET", DbType:="Int")>  _
        Public Property TARGET() As  System.Nullable(Of Long) 
            Get
                Return _TARGET
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TARGET = value
            End Set
        End Property 
        <Column(Storage:="_TARGET_UNLIMITED", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property TARGET_UNLIMITED() As Char
            Get
                Return _TARGET_UNLIMITED
            End Get
            Set(ByVal value As Char)
               _TARGET_UNLIMITED = value
            End Set
        End Property 
        <Column(Storage:="_TEMPLATE_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TEMPLATE_CODE() As String
            Get
                Return _TEMPLATE_CODE
            End Get
            Set(ByVal value As String)
               _TEMPLATE_CODE = value
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
        <Column(Storage:="_PROCESS_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROCESS_STATUS() As Char
            Get
                Return _PROCESS_STATUS
            End Get
            Set(ByVal value As Char)
               _PROCESS_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_LAST_SAVE_FILTER", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property LAST_SAVE_FILTER() As DateTime
            Get
                Return _LAST_SAVE_FILTER
            End Get
            Set(ByVal value As DateTime)
               _LAST_SAVE_FILTER = value
            End Set
        End Property 
        <Column(Storage:="_CAL_TARGET", DbType:="Char(1)")>  _
        Public Property CAL_TARGET() As  System.Nullable(Of Char) 
            Get
                Return _CAL_TARGET
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _CAL_TARGET = value
            End Set
        End Property 
        <Column(Storage:="_GEN_ACT_ALL_SURVEY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property GEN_ACT_ALL_SURVEY() As Char
            Get
                Return _GEN_ACT_ALL_SURVEY
            End Get
            Set(ByVal value As Char)
               _GEN_ACT_ALL_SURVEY = value
            End Set
        End Property 
        <Column(Storage:="_CATEGORY_GEN_ACT_ALL_SURVEY", DbType:="VarChar(255)")>  _
        Public Property CATEGORY_GEN_ACT_ALL_SURVEY() As  String 
            Get
                Return _CATEGORY_GEN_ACT_ALL_SURVEY
            End Get
            Set(ByVal value As  String )
               _CATEGORY_GEN_ACT_ALL_SURVEY = value
            End Set
        End Property 
        <Column(Storage:="_SUBCATEGORY_GEN_ACT_ALL_SURVEY", DbType:="VarChar(255)")>  _
        Public Property SUBCATEGORY_GEN_ACT_ALL_SURVEY() As  String 
            Get
                Return _SUBCATEGORY_GEN_ACT_ALL_SURVEY
            End Get
            Set(ByVal value As  String )
               _SUBCATEGORY_GEN_ACT_ALL_SURVEY = value
            End Set
        End Property 
        <Column(Storage:="_CAT_GEN_ACT_ALL_SURVEY_UPC", DbType:="VarChar(255)")>  _
        Public Property CAT_GEN_ACT_ALL_SURVEY_UPC() As  String 
            Get
                Return _CAT_GEN_ACT_ALL_SURVEY_UPC
            End Get
            Set(ByVal value As  String )
               _CAT_GEN_ACT_ALL_SURVEY_UPC = value
            End Set
        End Property 
        <Column(Storage:="_SUBCAT_GEN_ACT_ALL_SURVEY_UPC", DbType:="VarChar(255)")>  _
        Public Property SUBCAT_GEN_ACT_ALL_SURVEY_UPC() As  String 
            Get
                Return _SUBCAT_GEN_ACT_ALL_SURVEY_UPC
            End Get
            Set(ByVal value As  String )
               _SUBCAT_GEN_ACT_ALL_SURVEY_UPC = value
            End Set
        End Property 
        <Column(Storage:="_OWNER_GEN_ACT_ALL_SURVEY", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property OWNER_GEN_ACT_ALL_SURVEY() As String
            Get
                Return _OWNER_GEN_ACT_ALL_SURVEY
            End Get
            Set(ByVal value As String)
               _OWNER_GEN_ACT_ALL_SURVEY = value
            End Set
        End Property 
        <Column(Storage:="_GEN_ACT_RESULT3", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property GEN_ACT_RESULT3() As Char
            Get
                Return _GEN_ACT_RESULT3
            End Get
            Set(ByVal value As Char)
               _GEN_ACT_RESULT3 = value
            End Set
        End Property 
        <Column(Storage:="_CATEGORY_GEN_ACT_RESULT3", DbType:="VarChar(255)")>  _
        Public Property CATEGORY_GEN_ACT_RESULT3() As  String 
            Get
                Return _CATEGORY_GEN_ACT_RESULT3
            End Get
            Set(ByVal value As  String )
               _CATEGORY_GEN_ACT_RESULT3 = value
            End Set
        End Property 
        <Column(Storage:="_SUBCATEGORY_GEN_ACT_RESULT3", DbType:="VarChar(255)")>  _
        Public Property SUBCATEGORY_GEN_ACT_RESULT3() As  String 
            Get
                Return _SUBCATEGORY_GEN_ACT_RESULT3
            End Get
            Set(ByVal value As  String )
               _SUBCATEGORY_GEN_ACT_RESULT3 = value
            End Set
        End Property 
        <Column(Storage:="_CAT_GEN_ACT_RESULT3_UPC", DbType:="VarChar(255)")>  _
        Public Property CAT_GEN_ACT_RESULT3_UPC() As  String 
            Get
                Return _CAT_GEN_ACT_RESULT3_UPC
            End Get
            Set(ByVal value As  String )
               _CAT_GEN_ACT_RESULT3_UPC = value
            End Set
        End Property 
        <Column(Storage:="_SUBCAT_GEN_ACT_RESULT3_UPC", DbType:="VarChar(255)")>  _
        Public Property SUBCAT_GEN_ACT_RESULT3_UPC() As  String 
            Get
                Return _SUBCAT_GEN_ACT_RESULT3_UPC
            End Get
            Set(ByVal value As  String )
               _SUBCAT_GEN_ACT_RESULT3_UPC = value
            End Set
        End Property 
        <Column(Storage:="_OWNER_GEN_ACT_RESULT3", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property OWNER_GEN_ACT_RESULT3() As String
            Get
                Return _OWNER_GEN_ACT_RESULT3
            End Get
            Set(ByVal value As String)
               _OWNER_GEN_ACT_RESULT3 = value
            End Set
        End Property 


    End Class
End Namespace
