Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = CenLinqDB.Common.Utilities.SQLDB
Imports CenParaDB.TABLE
Imports CenParaDB.Common.Utilities

Namespace TABLE
    'Represents a transaction for TB_FILTER table CenLinqDB.
    '[Create by  on October, 19 2014]
    Public Class TbFilterCenLinqDB
        Public sub TbFilterCenLinqDB()

        End Sub 
        ' TB_FILTER
        Const _tableName As String = "TB_FILTER"
        Dim _deletedRow As Int16 = 0

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage As String
            Get
                Return _information
            End Get
        End Property
        Public ReadOnly Property HaveData As Boolean
            Get
                Return _haveData
            End Get
        End Property


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


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _FILTER_NAME = ""
            _CATEGORY = ""
            _SEGMENT = ""
            _PREIOD_DATEFROM = New DateTime(1,1,1)
            _PREIOD_DATETO = New DateTime(1,1,1)
            _PREIOD_TIMEFROM = ""
            _PREIOD_TIMETO = ""
            _SCHEDULETYPEDAY = ""
            _SCHEDULEMONDAY = ""
            _SCHEDULETUEDAY = ""
            _SCHEDULEWEDDAY = ""
            _SCHEDULETHUDAY = ""
            _SCHEDULEFRIDAY = ""
            _SCHEDULESATDAY = ""
            _SCHEDULESUNDAY = ""
            _TARGET = 0
            _TARGET_UNLIMITED = ""
            _TEMPLATE_CODE = ""
            _DURATION_TYPE = ""
            _DURATION_AFTER_MIN = 0
            _DURATION_EVERY_MIN = 0
            _ACTIVE_STATUS = ""
            _PROCESS_STATUS = ""
            _LAST_PROC_TIME = New DateTime(1,1,1)
            _CONTACT_CLASS = ""
            _NATIONALITY = ""
            _LAST_SAVE_FILTER = New DateTime(1,1,1)
            _CAL_TARGET = ""
            _NETWORK_TYPE = ""
            _BLANK_CONTACT_CLASS = ""
            _BLANK_CATEGORY = ""
            _SURVEY_ALL_SHOP = ""
            _GEN_ACT_ALL_SURVEY = ""
            _CATEGORY_GEN_ACT_ALL_SURVEY = ""
            _SUBCATEGORY_GEN_ACT_ALL_SURVEY = ""
            _CAT_GEN_ACT_ALL_SURVEY_UPC = ""
            _SUBCAT_GEN_ACT_ALL_SURVEY_UPC = ""
            _OWNER_GEN_ACT_ALL_SURVEY = ""
            _GEN_ACT_RESULT3 = ""
            _CATEGORY_GEN_ACT_RESULT3 = ""
            _SUBCATEGORY_GEN_ACT_RESULT3 = ""
            _CAT_GEN_ACT_RESULT3_UPC = ""
            _SUBCAT_GEN_ACT_RESULT3_UPC = ""
            _OWNER_GEN_ACT_RESULT3 = ""
        End Sub

       'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(whClause As String, orderBy As String, trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIF(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(Sql As String, trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into TB_FILTER table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _ID = DB.GetNextID("ID",tableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_FILTER table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("ID = " & DB.SetDouble(_ID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_FILTER table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_FILTER table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cID As Long, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return doDelete("ID = " & DB.SetDouble(cID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the record of TB_FILTER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_FILTER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbFilterCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_FILTER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbFilterCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_FILTER by specified FILTER_NAME key is retrieved successfully.
        '/// <param name=cFILTER_NAME>The FILTER_NAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByFILTER_NAME(cFILTER_NAME As String, trans As SQLTransaction) As Boolean
            Return doChkData("FILTER_NAME = " & DB.SetString(cFILTER_NAME) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_FILTER by specified FILTER_NAME key is retrieved successfully.
        '/// <param name=cFILTER_NAME>The FILTER_NAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByFILTER_NAME(cFILTER_NAME As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("FILTER_NAME = " & DB.SetString(cFILTER_NAME) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_FILTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_FILTER table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = False Then
                Try

                    ret = (DB.ExecuteNonQuery(SqlInsert, trans) > 0)
                    If ret = False Then
                        _error = DB.ErrorMessage
                    Else
                        _haveData = True
                    End If
                    _information = MessageResources.MSGIN001
                Catch ex As ApplicationException
                    ret = false
                    _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlInsert
                Catch ex As Exception
                    ret = False
                    _error = MessageResources.MSGEC101 & " Exception :" & ex.ToString() & "### SQL: " & SqlInsert
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEN002 & "### SQL: " & SqlInsert
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is updated to TB_FILTER table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If _haveData = True Then
                If whText.Trim() <> ""
                    Try

                        ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = DB.ErrorMessage
                        End If
                        _information = MessageResources.MSGIU001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlUpdate & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC102 & " Exception :" & ex.ToString() & "### SQL: " & SqlUpdate & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGEU003 & "### SQL: " & SqlUpdate & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlUpdate & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TB_FILTER table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> ""
                    Try
                        ret = (DB.ExecuteNonQuery(SqlDelete & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = MessageResources.MSGED001
                        End If
                        _information = MessageResources.MSGID001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlDelete & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC103 & " Exception :" & ex.ToString() & "### SQL: " & SqlDelete & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGED003 & "### SQL: " & SqlDelete & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlDelete & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_FILTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " WHERE " & whText
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("filter_name")) = False Then _filter_name = Rdr("filter_name").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then _segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("preiod_datefrom")) = False Then _preiod_datefrom = Convert.ToDateTime(Rdr("preiod_datefrom"))
                        If Convert.IsDBNull(Rdr("preiod_dateto")) = False Then _preiod_dateto = Convert.ToDateTime(Rdr("preiod_dateto"))
                        If Convert.IsDBNull(Rdr("preiod_timefrom")) = False Then _preiod_timefrom = Rdr("preiod_timefrom").ToString()
                        If Convert.IsDBNull(Rdr("preiod_timeto")) = False Then _preiod_timeto = Rdr("preiod_timeto").ToString()
                        If Convert.IsDBNull(Rdr("scheduletypeday")) = False Then _scheduletypeday = Rdr("scheduletypeday").ToString()
                        If Convert.IsDBNull(Rdr("schedulemonday")) = False Then _schedulemonday = Rdr("schedulemonday").ToString()
                        If Convert.IsDBNull(Rdr("scheduletueday")) = False Then _scheduletueday = Rdr("scheduletueday").ToString()
                        If Convert.IsDBNull(Rdr("schedulewedday")) = False Then _schedulewedday = Rdr("schedulewedday").ToString()
                        If Convert.IsDBNull(Rdr("schedulethuday")) = False Then _schedulethuday = Rdr("schedulethuday").ToString()
                        If Convert.IsDBNull(Rdr("schedulefriday")) = False Then _schedulefriday = Rdr("schedulefriday").ToString()
                        If Convert.IsDBNull(Rdr("schedulesatday")) = False Then _schedulesatday = Rdr("schedulesatday").ToString()
                        If Convert.IsDBNull(Rdr("schedulesunday")) = False Then _schedulesunday = Rdr("schedulesunday").ToString()
                        If Convert.IsDBNull(Rdr("target")) = False Then _target = Convert.ToInt32(Rdr("target"))
                        If Convert.IsDBNull(Rdr("target_unlimited")) = False Then _target_unlimited = Rdr("target_unlimited").ToString()
                        If Convert.IsDBNull(Rdr("template_code")) = False Then _template_code = Rdr("template_code").ToString()
                        If Convert.IsDBNull(Rdr("duration_type")) = False Then _duration_type = Rdr("duration_type").ToString()
                        If Convert.IsDBNull(Rdr("duration_after_min")) = False Then _duration_after_min = Convert.ToInt32(Rdr("duration_after_min"))
                        If Convert.IsDBNull(Rdr("duration_every_min")) = False Then _duration_every_min = Convert.ToInt32(Rdr("duration_every_min"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Rdr("active_status").ToString()
                        If Convert.IsDBNull(Rdr("process_status")) = False Then _process_status = Rdr("process_status").ToString()
                        If Convert.IsDBNull(Rdr("last_proc_time")) = False Then _last_proc_time = Convert.ToDateTime(Rdr("last_proc_time"))
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("nationality")) = False Then _nationality = Rdr("nationality").ToString()
                        If Convert.IsDBNull(Rdr("last_save_filter")) = False Then _last_save_filter = Convert.ToDateTime(Rdr("last_save_filter"))
                        If Convert.IsDBNull(Rdr("cal_target")) = False Then _cal_target = Rdr("cal_target").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("blank_contact_class")) = False Then _blank_contact_class = Rdr("blank_contact_class").ToString()
                        If Convert.IsDBNull(Rdr("blank_category")) = False Then _blank_category = Rdr("blank_category").ToString()
                        If Convert.IsDBNull(Rdr("survey_all_shop")) = False Then _survey_all_shop = Rdr("survey_all_shop").ToString()
                        If Convert.IsDBNull(Rdr("gen_act_all_survey")) = False Then _gen_act_all_survey = Rdr("gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("category_gen_act_all_survey")) = False Then _category_gen_act_all_survey = Rdr("category_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("subcategory_gen_act_all_survey")) = False Then _subcategory_gen_act_all_survey = Rdr("subcategory_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("cat_gen_act_all_survey_upc")) = False Then _cat_gen_act_all_survey_upc = Rdr("cat_gen_act_all_survey_upc").ToString()
                        If Convert.IsDBNull(Rdr("subcat_gen_act_all_survey_upc")) = False Then _subcat_gen_act_all_survey_upc = Rdr("subcat_gen_act_all_survey_upc").ToString()
                        If Convert.IsDBNull(Rdr("owner_gen_act_all_survey")) = False Then _owner_gen_act_all_survey = Rdr("owner_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("gen_act_result3")) = False Then _gen_act_result3 = Rdr("gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("category_gen_act_result3")) = False Then _category_gen_act_result3 = Rdr("category_gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("subcategory_gen_act_result3")) = False Then _subcategory_gen_act_result3 = Rdr("subcategory_gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("cat_gen_act_result3_upc")) = False Then _cat_gen_act_result3_upc = Rdr("cat_gen_act_result3_upc").ToString()
                        If Convert.IsDBNull(Rdr("subcat_gen_act_result3_upc")) = False Then _subcat_gen_act_result3_upc = Rdr("subcat_gen_act_result3_upc").ToString()
                        If Convert.IsDBNull(Rdr("owner_gen_act_result3")) = False Then _owner_gen_act_result3 = Rdr("owner_gen_act_result3").ToString()
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_FILTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbFilterCenLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("filter_name")) = False Then _filter_name = Rdr("filter_name").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then _segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("preiod_datefrom")) = False Then _preiod_datefrom = Convert.ToDateTime(Rdr("preiod_datefrom"))
                        If Convert.IsDBNull(Rdr("preiod_dateto")) = False Then _preiod_dateto = Convert.ToDateTime(Rdr("preiod_dateto"))
                        If Convert.IsDBNull(Rdr("preiod_timefrom")) = False Then _preiod_timefrom = Rdr("preiod_timefrom").ToString()
                        If Convert.IsDBNull(Rdr("preiod_timeto")) = False Then _preiod_timeto = Rdr("preiod_timeto").ToString()
                        If Convert.IsDBNull(Rdr("scheduletypeday")) = False Then _scheduletypeday = Rdr("scheduletypeday").ToString()
                        If Convert.IsDBNull(Rdr("schedulemonday")) = False Then _schedulemonday = Rdr("schedulemonday").ToString()
                        If Convert.IsDBNull(Rdr("scheduletueday")) = False Then _scheduletueday = Rdr("scheduletueday").ToString()
                        If Convert.IsDBNull(Rdr("schedulewedday")) = False Then _schedulewedday = Rdr("schedulewedday").ToString()
                        If Convert.IsDBNull(Rdr("schedulethuday")) = False Then _schedulethuday = Rdr("schedulethuday").ToString()
                        If Convert.IsDBNull(Rdr("schedulefriday")) = False Then _schedulefriday = Rdr("schedulefriday").ToString()
                        If Convert.IsDBNull(Rdr("schedulesatday")) = False Then _schedulesatday = Rdr("schedulesatday").ToString()
                        If Convert.IsDBNull(Rdr("schedulesunday")) = False Then _schedulesunday = Rdr("schedulesunday").ToString()
                        If Convert.IsDBNull(Rdr("target")) = False Then _target = Convert.ToInt32(Rdr("target"))
                        If Convert.IsDBNull(Rdr("target_unlimited")) = False Then _target_unlimited = Rdr("target_unlimited").ToString()
                        If Convert.IsDBNull(Rdr("template_code")) = False Then _template_code = Rdr("template_code").ToString()
                        If Convert.IsDBNull(Rdr("duration_type")) = False Then _duration_type = Rdr("duration_type").ToString()
                        If Convert.IsDBNull(Rdr("duration_after_min")) = False Then _duration_after_min = Convert.ToInt32(Rdr("duration_after_min"))
                        If Convert.IsDBNull(Rdr("duration_every_min")) = False Then _duration_every_min = Convert.ToInt32(Rdr("duration_every_min"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Rdr("active_status").ToString()
                        If Convert.IsDBNull(Rdr("process_status")) = False Then _process_status = Rdr("process_status").ToString()
                        If Convert.IsDBNull(Rdr("last_proc_time")) = False Then _last_proc_time = Convert.ToDateTime(Rdr("last_proc_time"))
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("nationality")) = False Then _nationality = Rdr("nationality").ToString()
                        If Convert.IsDBNull(Rdr("last_save_filter")) = False Then _last_save_filter = Convert.ToDateTime(Rdr("last_save_filter"))
                        If Convert.IsDBNull(Rdr("cal_target")) = False Then _cal_target = Rdr("cal_target").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("blank_contact_class")) = False Then _blank_contact_class = Rdr("blank_contact_class").ToString()
                        If Convert.IsDBNull(Rdr("blank_category")) = False Then _blank_category = Rdr("blank_category").ToString()
                        If Convert.IsDBNull(Rdr("survey_all_shop")) = False Then _survey_all_shop = Rdr("survey_all_shop").ToString()
                        If Convert.IsDBNull(Rdr("gen_act_all_survey")) = False Then _gen_act_all_survey = Rdr("gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("category_gen_act_all_survey")) = False Then _category_gen_act_all_survey = Rdr("category_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("subcategory_gen_act_all_survey")) = False Then _subcategory_gen_act_all_survey = Rdr("subcategory_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("cat_gen_act_all_survey_upc")) = False Then _cat_gen_act_all_survey_upc = Rdr("cat_gen_act_all_survey_upc").ToString()
                        If Convert.IsDBNull(Rdr("subcat_gen_act_all_survey_upc")) = False Then _subcat_gen_act_all_survey_upc = Rdr("subcat_gen_act_all_survey_upc").ToString()
                        If Convert.IsDBNull(Rdr("owner_gen_act_all_survey")) = False Then _owner_gen_act_all_survey = Rdr("owner_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("gen_act_result3")) = False Then _gen_act_result3 = Rdr("gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("category_gen_act_result3")) = False Then _category_gen_act_result3 = Rdr("category_gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("subcategory_gen_act_result3")) = False Then _subcategory_gen_act_result3 = Rdr("subcategory_gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("cat_gen_act_result3_upc")) = False Then _cat_gen_act_result3_upc = Rdr("cat_gen_act_result3_upc").ToString()
                        If Convert.IsDBNull(Rdr("subcat_gen_act_result3_upc")) = False Then _subcat_gen_act_result3_upc = Rdr("subcat_gen_act_result3_upc").ToString()
                        If Convert.IsDBNull(Rdr("owner_gen_act_result3")) = False Then _owner_gen_act_result3 = Rdr("owner_gen_act_result3").ToString()
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function


        '/// Returns an indication whether the Class Data of TB_FILTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbFilterCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbFilterCenParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("filter_name")) = False Then ret.filter_name = Rdr("filter_name").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then ret.category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then ret.segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("preiod_datefrom")) = False Then ret.preiod_datefrom = Convert.ToDateTime(Rdr("preiod_datefrom"))
                        If Convert.IsDBNull(Rdr("preiod_dateto")) = False Then ret.preiod_dateto = Convert.ToDateTime(Rdr("preiod_dateto"))
                        If Convert.IsDBNull(Rdr("preiod_timefrom")) = False Then ret.preiod_timefrom = Rdr("preiod_timefrom").ToString()
                        If Convert.IsDBNull(Rdr("preiod_timeto")) = False Then ret.preiod_timeto = Rdr("preiod_timeto").ToString()
                        If Convert.IsDBNull(Rdr("scheduletypeday")) = False Then ret.scheduletypeday = Rdr("scheduletypeday").ToString()
                        If Convert.IsDBNull(Rdr("schedulemonday")) = False Then ret.schedulemonday = Rdr("schedulemonday").ToString()
                        If Convert.IsDBNull(Rdr("scheduletueday")) = False Then ret.scheduletueday = Rdr("scheduletueday").ToString()
                        If Convert.IsDBNull(Rdr("schedulewedday")) = False Then ret.schedulewedday = Rdr("schedulewedday").ToString()
                        If Convert.IsDBNull(Rdr("schedulethuday")) = False Then ret.schedulethuday = Rdr("schedulethuday").ToString()
                        If Convert.IsDBNull(Rdr("schedulefriday")) = False Then ret.schedulefriday = Rdr("schedulefriday").ToString()
                        If Convert.IsDBNull(Rdr("schedulesatday")) = False Then ret.schedulesatday = Rdr("schedulesatday").ToString()
                        If Convert.IsDBNull(Rdr("schedulesunday")) = False Then ret.schedulesunday = Rdr("schedulesunday").ToString()
                        If Convert.IsDBNull(Rdr("target")) = False Then ret.target = Convert.ToInt32(Rdr("target"))
                        If Convert.IsDBNull(Rdr("target_unlimited")) = False Then ret.target_unlimited = Rdr("target_unlimited").ToString()
                        If Convert.IsDBNull(Rdr("template_code")) = False Then ret.template_code = Rdr("template_code").ToString()
                        If Convert.IsDBNull(Rdr("duration_type")) = False Then ret.duration_type = Rdr("duration_type").ToString()
                        If Convert.IsDBNull(Rdr("duration_after_min")) = False Then ret.duration_after_min = Convert.ToInt32(Rdr("duration_after_min"))
                        If Convert.IsDBNull(Rdr("duration_every_min")) = False Then ret.duration_every_min = Convert.ToInt32(Rdr("duration_every_min"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then ret.active_status = Rdr("active_status").ToString()
                        If Convert.IsDBNull(Rdr("process_status")) = False Then ret.process_status = Rdr("process_status").ToString()
                        If Convert.IsDBNull(Rdr("last_proc_time")) = False Then ret.last_proc_time = Convert.ToDateTime(Rdr("last_proc_time"))
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then ret.contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("nationality")) = False Then ret.nationality = Rdr("nationality").ToString()
                        If Convert.IsDBNull(Rdr("last_save_filter")) = False Then ret.last_save_filter = Convert.ToDateTime(Rdr("last_save_filter"))
                        If Convert.IsDBNull(Rdr("cal_target")) = False Then ret.cal_target = Rdr("cal_target").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("blank_contact_class")) = False Then ret.blank_contact_class = Rdr("blank_contact_class").ToString()
                        If Convert.IsDBNull(Rdr("blank_category")) = False Then ret.blank_category = Rdr("blank_category").ToString()
                        If Convert.IsDBNull(Rdr("survey_all_shop")) = False Then ret.survey_all_shop = Rdr("survey_all_shop").ToString()
                        If Convert.IsDBNull(Rdr("gen_act_all_survey")) = False Then ret.gen_act_all_survey = Rdr("gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("category_gen_act_all_survey")) = False Then ret.category_gen_act_all_survey = Rdr("category_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("subcategory_gen_act_all_survey")) = False Then ret.subcategory_gen_act_all_survey = Rdr("subcategory_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("cat_gen_act_all_survey_upc")) = False Then ret.cat_gen_act_all_survey_upc = Rdr("cat_gen_act_all_survey_upc").ToString()
                        If Convert.IsDBNull(Rdr("subcat_gen_act_all_survey_upc")) = False Then ret.subcat_gen_act_all_survey_upc = Rdr("subcat_gen_act_all_survey_upc").ToString()
                        If Convert.IsDBNull(Rdr("owner_gen_act_all_survey")) = False Then ret.owner_gen_act_all_survey = Rdr("owner_gen_act_all_survey").ToString()
                        If Convert.IsDBNull(Rdr("gen_act_result3")) = False Then ret.gen_act_result3 = Rdr("gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("category_gen_act_result3")) = False Then ret.category_gen_act_result3 = Rdr("category_gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("subcategory_gen_act_result3")) = False Then ret.subcategory_gen_act_result3 = Rdr("subcategory_gen_act_result3").ToString()
                        If Convert.IsDBNull(Rdr("cat_gen_act_result3_upc")) = False Then ret.cat_gen_act_result3_upc = Rdr("cat_gen_act_result3_upc").ToString()
                        If Convert.IsDBNull(Rdr("subcat_gen_act_result3_upc")) = False Then ret.subcat_gen_act_result3_upc = Rdr("subcat_gen_act_result3_upc").ToString()
                        If Convert.IsDBNull(Rdr("owner_gen_act_result3")) = False Then ret.owner_gen_act_result3 = Rdr("owner_gen_act_result3").ToString()

                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return ret
        End Function

        ' SQL Statements


        'Get Insert Statement for table TB_FILTER
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, FILTER_NAME, CATEGORY, SEGMENT, PREIOD_DATEFROM, PREIOD_DATETO, PREIOD_TIMEFROM, PREIOD_TIMETO, SCHEDULETYPEDAY, SCHEDULEMONDAY, SCHEDULETUEDAY, SCHEDULEWEDDAY, SCHEDULETHUDAY, SCHEDULEFRIDAY, SCHEDULESATDAY, SCHEDULESUNDAY, TARGET, TARGET_UNLIMITED, TEMPLATE_CODE, DURATION_TYPE, DURATION_AFTER_MIN, DURATION_EVERY_MIN, ACTIVE_STATUS, PROCESS_STATUS, LAST_PROC_TIME, CONTACT_CLASS, NATIONALITY, LAST_SAVE_FILTER, CAL_TARGET, NETWORK_TYPE, BLANK_CONTACT_CLASS, BLANK_CATEGORY, SURVEY_ALL_SHOP, GEN_ACT_ALL_SURVEY, CATEGORY_GEN_ACT_ALL_SURVEY, SUBCATEGORY_GEN_ACT_ALL_SURVEY, CAT_GEN_ACT_ALL_SURVEY_UPC, SUBCAT_GEN_ACT_ALL_SURVEY_UPC, OWNER_GEN_ACT_ALL_SURVEY, GEN_ACT_RESULT3, CATEGORY_GEN_ACT_RESULT3, SUBCATEGORY_GEN_ACT_RESULT3, CAT_GEN_ACT_RESULT3_UPC, SUBCAT_GEN_ACT_RESULT3_UPC, OWNER_GEN_ACT_RESULT3)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_FILTER_NAME) & ", "
                sql += DB.SetString(_CATEGORY) & ", "
                sql += DB.SetString(_SEGMENT) & ", "
                sql += DB.SetDateTime(_PREIOD_DATEFROM) & ", "
                sql += DB.SetDateTime(_PREIOD_DATETO) & ", "
                sql += DB.SetString(_PREIOD_TIMEFROM) & ", "
                sql += DB.SetString(_PREIOD_TIMETO) & ", "
                sql += DB.SetString(_SCHEDULETYPEDAY) & ", "
                sql += DB.SetString(_SCHEDULEMONDAY) & ", "
                sql += DB.SetString(_SCHEDULETUEDAY) & ", "
                sql += DB.SetString(_SCHEDULEWEDDAY) & ", "
                sql += DB.SetString(_SCHEDULETHUDAY) & ", "
                sql += DB.SetString(_SCHEDULEFRIDAY) & ", "
                sql += DB.SetString(_SCHEDULESATDAY) & ", "
                sql += DB.SetString(_SCHEDULESUNDAY) & ", "
                sql += DB.SetDouble(_TARGET) & ", "
                sql += DB.SetString(_TARGET_UNLIMITED) & ", "
                sql += DB.SetString(_TEMPLATE_CODE) & ", "
                sql += DB.SetString(_DURATION_TYPE) & ", "
                sql += DB.SetDouble(_DURATION_AFTER_MIN) & ", "
                sql += DB.SetDouble(_DURATION_EVERY_MIN) & ", "
                sql += DB.SetString(_ACTIVE_STATUS) & ", "
                sql += DB.SetString(_PROCESS_STATUS) & ", "
                sql += DB.SetDateTime(_LAST_PROC_TIME) & ", "
                sql += DB.SetString(_CONTACT_CLASS) & ", "
                sql += DB.SetString(_NATIONALITY) & ", "
                sql += DB.SetDateTime(_LAST_SAVE_FILTER) & ", "
                sql += DB.SetString(_CAL_TARGET) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_BLANK_CONTACT_CLASS) & ", "
                sql += DB.SetString(_BLANK_CATEGORY) & ", "
                sql += DB.SetString(_SURVEY_ALL_SHOP) & ", "
                sql += DB.SetString(_GEN_ACT_ALL_SURVEY) & ", "
                sql += DB.SetString(_CATEGORY_GEN_ACT_ALL_SURVEY) & ", "
                sql += DB.SetString(_SUBCATEGORY_GEN_ACT_ALL_SURVEY) & ", "
                sql += DB.SetString(_CAT_GEN_ACT_ALL_SURVEY_UPC) & ", "
                sql += DB.SetString(_SUBCAT_GEN_ACT_ALL_SURVEY_UPC) & ", "
                sql += DB.SetString(_OWNER_GEN_ACT_ALL_SURVEY) & ", "
                sql += DB.SetString(_GEN_ACT_RESULT3) & ", "
                sql += DB.SetString(_CATEGORY_GEN_ACT_RESULT3) & ", "
                sql += DB.SetString(_SUBCATEGORY_GEN_ACT_RESULT3) & ", "
                sql += DB.SetString(_CAT_GEN_ACT_RESULT3_UPC) & ", "
                sql += DB.SetString(_SUBCAT_GEN_ACT_RESULT3_UPC) & ", "
                sql += DB.SetString(_OWNER_GEN_ACT_RESULT3)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_FILTER
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "FILTER_NAME = " & DB.SetString(_FILTER_NAME) & ", "
                Sql += "CATEGORY = " & DB.SetString(_CATEGORY) & ", "
                Sql += "SEGMENT = " & DB.SetString(_SEGMENT) & ", "
                Sql += "PREIOD_DATEFROM = " & DB.SetDateTime(_PREIOD_DATEFROM) & ", "
                Sql += "PREIOD_DATETO = " & DB.SetDateTime(_PREIOD_DATETO) & ", "
                Sql += "PREIOD_TIMEFROM = " & DB.SetString(_PREIOD_TIMEFROM) & ", "
                Sql += "PREIOD_TIMETO = " & DB.SetString(_PREIOD_TIMETO) & ", "
                Sql += "SCHEDULETYPEDAY = " & DB.SetString(_SCHEDULETYPEDAY) & ", "
                Sql += "SCHEDULEMONDAY = " & DB.SetString(_SCHEDULEMONDAY) & ", "
                Sql += "SCHEDULETUEDAY = " & DB.SetString(_SCHEDULETUEDAY) & ", "
                Sql += "SCHEDULEWEDDAY = " & DB.SetString(_SCHEDULEWEDDAY) & ", "
                Sql += "SCHEDULETHUDAY = " & DB.SetString(_SCHEDULETHUDAY) & ", "
                Sql += "SCHEDULEFRIDAY = " & DB.SetString(_SCHEDULEFRIDAY) & ", "
                Sql += "SCHEDULESATDAY = " & DB.SetString(_SCHEDULESATDAY) & ", "
                Sql += "SCHEDULESUNDAY = " & DB.SetString(_SCHEDULESUNDAY) & ", "
                Sql += "TARGET = " & DB.SetDouble(_TARGET) & ", "
                Sql += "TARGET_UNLIMITED = " & DB.SetString(_TARGET_UNLIMITED) & ", "
                Sql += "TEMPLATE_CODE = " & DB.SetString(_TEMPLATE_CODE) & ", "
                Sql += "DURATION_TYPE = " & DB.SetString(_DURATION_TYPE) & ", "
                Sql += "DURATION_AFTER_MIN = " & DB.SetDouble(_DURATION_AFTER_MIN) & ", "
                Sql += "DURATION_EVERY_MIN = " & DB.SetDouble(_DURATION_EVERY_MIN) & ", "
                Sql += "ACTIVE_STATUS = " & DB.SetString(_ACTIVE_STATUS) & ", "
                Sql += "PROCESS_STATUS = " & DB.SetString(_PROCESS_STATUS) & ", "
                Sql += "LAST_PROC_TIME = " & DB.SetDateTime(_LAST_PROC_TIME) & ", "
                Sql += "CONTACT_CLASS = " & DB.SetString(_CONTACT_CLASS) & ", "
                Sql += "NATIONALITY = " & DB.SetString(_NATIONALITY) & ", "
                Sql += "LAST_SAVE_FILTER = " & DB.SetDateTime(_LAST_SAVE_FILTER) & ", "
                Sql += "CAL_TARGET = " & DB.SetString(_CAL_TARGET) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "BLANK_CONTACT_CLASS = " & DB.SetString(_BLANK_CONTACT_CLASS) & ", "
                Sql += "BLANK_CATEGORY = " & DB.SetString(_BLANK_CATEGORY) & ", "
                Sql += "SURVEY_ALL_SHOP = " & DB.SetString(_SURVEY_ALL_SHOP) & ", "
                Sql += "GEN_ACT_ALL_SURVEY = " & DB.SetString(_GEN_ACT_ALL_SURVEY) & ", "
                Sql += "CATEGORY_GEN_ACT_ALL_SURVEY = " & DB.SetString(_CATEGORY_GEN_ACT_ALL_SURVEY) & ", "
                Sql += "SUBCATEGORY_GEN_ACT_ALL_SURVEY = " & DB.SetString(_SUBCATEGORY_GEN_ACT_ALL_SURVEY) & ", "
                Sql += "CAT_GEN_ACT_ALL_SURVEY_UPC = " & DB.SetString(_CAT_GEN_ACT_ALL_SURVEY_UPC) & ", "
                Sql += "SUBCAT_GEN_ACT_ALL_SURVEY_UPC = " & DB.SetString(_SUBCAT_GEN_ACT_ALL_SURVEY_UPC) & ", "
                Sql += "OWNER_GEN_ACT_ALL_SURVEY = " & DB.SetString(_OWNER_GEN_ACT_ALL_SURVEY) & ", "
                Sql += "GEN_ACT_RESULT3 = " & DB.SetString(_GEN_ACT_RESULT3) & ", "
                Sql += "CATEGORY_GEN_ACT_RESULT3 = " & DB.SetString(_CATEGORY_GEN_ACT_RESULT3) & ", "
                Sql += "SUBCATEGORY_GEN_ACT_RESULT3 = " & DB.SetString(_SUBCATEGORY_GEN_ACT_RESULT3) & ", "
                Sql += "CAT_GEN_ACT_RESULT3_UPC = " & DB.SetString(_CAT_GEN_ACT_RESULT3_UPC) & ", "
                Sql += "SUBCAT_GEN_ACT_RESULT3_UPC = " & DB.SetString(_SUBCAT_GEN_ACT_RESULT3_UPC) & ", "
                Sql += "OWNER_GEN_ACT_RESULT3 = " & DB.SetString(_OWNER_GEN_ACT_RESULT3) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_FILTER
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_FILTER
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, FILTER_NAME, CATEGORY, SEGMENT, PREIOD_DATEFROM, PREIOD_DATETO, PREIOD_TIMEFROM, PREIOD_TIMETO, SCHEDULETYPEDAY, SCHEDULEMONDAY, SCHEDULETUEDAY, SCHEDULEWEDDAY, SCHEDULETHUDAY, SCHEDULEFRIDAY, SCHEDULESATDAY, SCHEDULESUNDAY, TARGET, TARGET_UNLIMITED, TEMPLATE_CODE, DURATION_TYPE, DURATION_AFTER_MIN, DURATION_EVERY_MIN, ACTIVE_STATUS, PROCESS_STATUS, LAST_PROC_TIME, CONTACT_CLASS, NATIONALITY, LAST_SAVE_FILTER, CAL_TARGET, NETWORK_TYPE, BLANK_CONTACT_CLASS, BLANK_CATEGORY, SURVEY_ALL_SHOP, GEN_ACT_ALL_SURVEY, CATEGORY_GEN_ACT_ALL_SURVEY, SUBCATEGORY_GEN_ACT_ALL_SURVEY, CAT_GEN_ACT_ALL_SURVEY_UPC, SUBCAT_GEN_ACT_ALL_SURVEY_UPC, OWNER_GEN_ACT_ALL_SURVEY, GEN_ACT_RESULT3, CATEGORY_GEN_ACT_RESULT3, SUBCATEGORY_GEN_ACT_RESULT3, CAT_GEN_ACT_RESULT3_UPC, SUBCAT_GEN_ACT_RESULT3_UPC, OWNER_GEN_ACT_RESULT3 FROM " & tableName
                Return Sql
            End Get
        End Property


            'Define Child Table 

       Dim _TB_FILTER_SERVICE_tb_filter_id As DataTable
       Dim _TB_FILTER_SHOP_tb_filter_id As DataTable
       Dim _TB_FILTER_STAFF_tb_filter_id As DataTable
       Dim _TB_FILTER_TEMP_TARGET_tb_filter_id As DataTable

       Public Property CHILD_TB_FILTER_SERVICE_tb_filter_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_SERVICE Column :tb_filter_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterServiceItem As New TbFilterServiceCenLinqDB
               _TB_FILTER_SERVICE_tb_filter_id = TbFilterServiceItem.GetDataList("tb_filter_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterServiceItem = Nothing
               Return _TB_FILTER_SERVICE_tb_filter_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_SERVICE_tb_filter_id = value
           End Set
       End Property
       Public Property CHILD_TB_FILTER_SHOP_tb_filter_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_SHOP Column :tb_filter_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterShopItem As New TbFilterShopCenLinqDB
               _TB_FILTER_SHOP_tb_filter_id = TbFilterShopItem.GetDataList("tb_filter_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterShopItem = Nothing
               Return _TB_FILTER_SHOP_tb_filter_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_SHOP_tb_filter_id = value
           End Set
       End Property
       Public Property CHILD_TB_FILTER_STAFF_tb_filter_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_STAFF Column :tb_filter_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterStaffItem As New TbFilterStaffCenLinqDB
               _TB_FILTER_STAFF_tb_filter_id = TbFilterStaffItem.GetDataList("tb_filter_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterStaffItem = Nothing
               Return _TB_FILTER_STAFF_tb_filter_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_STAFF_tb_filter_id = value
           End Set
       End Property
       Public Property CHILD_TB_FILTER_TEMP_TARGET_tb_filter_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_TEMP_TARGET Column :tb_filter_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterTempTargetItem As New TbFilterTempTargetCenLinqDB
               _TB_FILTER_TEMP_TARGET_tb_filter_id = TbFilterTempTargetItem.GetDataList("tb_filter_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterTempTargetItem = Nothing
               Return _TB_FILTER_TEMP_TARGET_tb_filter_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_TEMP_TARGET_tb_filter_id = value
           End Set
       End Property
    End Class
End Namespace
