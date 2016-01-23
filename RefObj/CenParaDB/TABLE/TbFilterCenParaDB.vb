
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_FILTER table Parameter.
    '[Create by  on October, 19 2014]

    <Table(Name:="TB_FILTER")>  _
    Public Class TbFilterCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _FILTER_NAME As String = ""
        Dim _CATEGORY As String = ""
        Dim _SEGMENT As  String  = ""
        Dim _PREIOD_DATEFROM As  System.Nullable(Of Date)  = New DateTime(1,1,1)
        Dim _PREIOD_DATETO As  System.Nullable(Of Date)  = New DateTime(1,1,1)
        Dim _PREIOD_TIMEFROM As  String  = ""
        Dim _PREIOD_TIMETO As  String  = ""
        Dim _SCHEDULETYPEDAY As Char = "0"
        Dim _SCHEDULEMONDAY As Char = "N"
        Dim _SCHEDULETUEDAY As Char = "N"
        Dim _SCHEDULEWEDDAY As Char = "N"
        Dim _SCHEDULETHUDAY As Char = "N"
        Dim _SCHEDULEFRIDAY As Char = "N"
        Dim _SCHEDULESATDAY As Char = "N"
        Dim _SCHEDULESUNDAY As Char = "N"
        Dim _TARGET As  System.Nullable(Of Long)  = 0
        Dim _TARGET_UNLIMITED As Char = "N"
        Dim _TEMPLATE_CODE As String = ""
        Dim _DURATION_TYPE As Char = "0"
        Dim _DURATION_AFTER_MIN As  System.Nullable(Of Long)  = 0
        Dim _DURATION_EVERY_MIN As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As Char = "Y"
        Dim _PROCESS_STATUS As Char = "Y"
        Dim _LAST_PROC_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CONTACT_CLASS As  String  = ""
        Dim _NATIONALITY As  String  = ""
        Dim _LAST_SAVE_FILTER As DateTime = New DateTime(1,1,1)
        Dim _CAL_TARGET As Char = "Y"
        Dim _NETWORK_TYPE As String = "ALL"
        Dim _BLANK_CONTACT_CLASS As Char = "N"
        Dim _BLANK_CATEGORY As Char = "N"
        Dim _SURVEY_ALL_SHOP As Char = "N"
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
        <Column(Storage:="_CATEGORY", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property CATEGORY() As String
            Get
                Return _CATEGORY
            End Get
            Set(ByVal value As String)
               _CATEGORY = value
            End Set
        End Property 
        <Column(Storage:="_SEGMENT", DbType:="Text")>  _
        Public Property SEGMENT() As  String 
            Get
                Return _SEGMENT
            End Get
            Set(ByVal value As  String )
               _SEGMENT = value
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
        <Column(Storage:="_DURATION_TYPE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property DURATION_TYPE() As Char
            Get
                Return _DURATION_TYPE
            End Get
            Set(ByVal value As Char)
               _DURATION_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_DURATION_AFTER_MIN", DbType:="Int")>  _
        Public Property DURATION_AFTER_MIN() As  System.Nullable(Of Long) 
            Get
                Return _DURATION_AFTER_MIN
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _DURATION_AFTER_MIN = value
            End Set
        End Property 
        <Column(Storage:="_DURATION_EVERY_MIN", DbType:="Int")>  _
        Public Property DURATION_EVERY_MIN() As  System.Nullable(Of Long) 
            Get
                Return _DURATION_EVERY_MIN
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _DURATION_EVERY_MIN = value
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
        <Column(Storage:="_LAST_PROC_TIME", DbType:="DateTime")>  _
        Public Property LAST_PROC_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _LAST_PROC_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _LAST_PROC_TIME = value
            End Set
        End Property 
        <Column(Storage:="_CONTACT_CLASS", DbType:="VarChar(50)")>  _
        Public Property CONTACT_CLASS() As  String 
            Get
                Return _CONTACT_CLASS
            End Get
            Set(ByVal value As  String )
               _CONTACT_CLASS = value
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
        <Column(Storage:="_LAST_SAVE_FILTER", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property LAST_SAVE_FILTER() As DateTime
            Get
                Return _LAST_SAVE_FILTER
            End Get
            Set(ByVal value As DateTime)
               _LAST_SAVE_FILTER = value
            End Set
        End Property 
        <Column(Storage:="_CAL_TARGET", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property CAL_TARGET() As Char
            Get
                Return _CAL_TARGET
            End Get
            Set(ByVal value As Char)
               _CAL_TARGET = value
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
        <Column(Storage:="_BLANK_CONTACT_CLASS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property BLANK_CONTACT_CLASS() As Char
            Get
                Return _BLANK_CONTACT_CLASS
            End Get
            Set(ByVal value As Char)
               _BLANK_CONTACT_CLASS = value
            End Set
        End Property 
        <Column(Storage:="_BLANK_CATEGORY", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property BLANK_CATEGORY() As Char
            Get
                Return _BLANK_CATEGORY
            End Get
            Set(ByVal value As Char)
               _BLANK_CATEGORY = value
            End Set
        End Property 
        <Column(Storage:="_SURVEY_ALL_SHOP", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SURVEY_ALL_SHOP() As Char
            Get
                Return _SURVEY_ALL_SHOP
            End Get
            Set(ByVal value As Char)
               _SURVEY_ALL_SHOP = value
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
