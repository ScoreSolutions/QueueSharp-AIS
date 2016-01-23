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
    'Represents a transaction for TB_REP_SUMMARY_REPORT_STAFF table CenLinqDB.
    '[Create by  on August, 23 2013]
    Public Class TbRepSummaryReportStaffCenLinqDB
        Public sub TbRepSummaryReportStaffCenLinqDB()

        End Sub 
        ' TB_REP_SUMMARY_REPORT_STAFF
        Const _tableName As String = "TB_REP_SUMMARY_REPORT_STAFF"
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
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SHOP_LOCATION_GROUP As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SHOW_DATE As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _EMP_ID As  String  = ""
        Dim _NETWORK_TYPE As String = ""
        Dim _SEGMENT_TYPE As String = ""
        Dim _ITEM_ID As Long = 0
        Dim _ITEM_NAME_EN As String = ""
        Dim _ITEM_NAME_TH As String = ""
        Dim _REGIS As Long = 0
        Dim _SERVE As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCLE As Long = 0
        Dim _WAIT_WITH_KPI As Long = 0
        Dim _SERVE_WITH_KPI As Long = 0
        Dim _P_HT As Long = 0
        Dim _AHT As Long = 0
        Dim _MAX_HT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _SUM_HT As Long = 0
        Dim _PRODUCTIVITY As String = ""
        Dim _P_PRODUCTIVITY As String = ""
        Dim _NON_PRODUCTIVITY As String = ""
        Dim _P_NON_PRODUCTIVITY As String = ""
        Dim _TOTAL_TIME As String = ""

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
        <Column(Storage:="_SHOP_LOCATION_GROUP", DbType:="VarChar(3) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_LOCATION_GROUP() As String
            Get
                Return _SHOP_LOCATION_GROUP
            End Get
            Set(ByVal value As String)
               _SHOP_LOCATION_GROUP = value
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
        <Column(Storage:="_SHOW_DATE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_DATE() As String
            Get
                Return _SHOW_DATE
            End Get
            Set(ByVal value As String)
               _SHOW_DATE = value
            End Set
        End Property 
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property STAFF_NAME() As String
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As String)
               _STAFF_NAME = value
            End Set
        End Property 
        <Column(Storage:="_EMP_ID", DbType:="VarChar(255)")>  _
        Public Property EMP_ID() As  String 
            Get
                Return _EMP_ID
            End Get
            Set(ByVal value As  String )
               _EMP_ID = value
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
        <Column(Storage:="_SEGMENT_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SEGMENT_TYPE() As String
            Get
                Return _SEGMENT_TYPE
            End Get
            Set(ByVal value As String)
               _SEGMENT_TYPE = value
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
        <Column(Storage:="_ITEM_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME_TH() As String
            Get
                Return _ITEM_NAME_TH
            End Get
            Set(ByVal value As String)
               _ITEM_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
               _REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE() As Long
            Get
                Return _SERVE
            End Get
            Set(ByVal value As Long)
               _SERVE = value
            End Set
        End Property 
        <Column(Storage:="_MISSED_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MISSED_CALL() As Long
            Get
                Return _MISSED_CALL
            End Get
            Set(ByVal value As Long)
               _MISSED_CALL = value
            End Set
        End Property 
        <Column(Storage:="_CANCLE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCLE() As Long
            Get
                Return _CANCLE
            End Get
            Set(ByVal value As Long)
               _CANCLE = value
            End Set
        End Property 
        <Column(Storage:="_WAIT_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WAIT_WITH_KPI() As Long
            Get
                Return _WAIT_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _WAIT_WITH_KPI = value
            End Set
        End Property 
        <Column(Storage:="_SERVE_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE_WITH_KPI() As Long
            Get
                Return _SERVE_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _SERVE_WITH_KPI = value
            End Set
        End Property 
        <Column(Storage:="_P_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property P_HT() As Long
            Get
                Return _P_HT
            End Get
            Set(ByVal value As Long)
               _P_HT = value
            End Set
        End Property 
        <Column(Storage:="_AHT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property AHT() As Long
            Get
                Return _AHT
            End Get
            Set(ByVal value As Long)
               _AHT = value
            End Set
        End Property 
        <Column(Storage:="_MAX_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_HT() As Long
            Get
                Return _MAX_HT
            End Get
            Set(ByVal value As Long)
               _MAX_HT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_HT() As Long
            Get
                Return _COUNT_HT
            End Get
            Set(ByVal value As Long)
               _COUNT_HT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_HT() As Long
            Get
                Return _SUM_HT
            End Get
            Set(ByVal value As Long)
               _SUM_HT = value
            End Set
        End Property 
        <Column(Storage:="_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PRODUCTIVITY() As String
            Get
                Return _PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_P_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property P_PRODUCTIVITY() As String
            Get
                Return _P_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _P_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_NON_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_PRODUCTIVITY() As String
            Get
                Return _NON_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_P_NON_PRODUCTIVITY", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property P_NON_PRODUCTIVITY() As String
            Get
                Return _P_NON_PRODUCTIVITY
            End Get
            Set(ByVal value As String)
               _P_NON_PRODUCTIVITY = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL_TIME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL_TIME() As String
            Get
                Return _TOTAL_TIME
            End Get
            Set(ByVal value As String)
               _TOTAL_TIME = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _SHOP_ID = 0
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _SHOP_LOCATION_GROUP = ""
            _SERVICE_DATE = New DateTime(1,1,1)
            _SHOW_DATE = ""
            _STAFF_NAME = ""
            _EMP_ID = ""
            _NETWORK_TYPE = ""
            _SEGMENT_TYPE = ""
            _ITEM_ID = 0
            _ITEM_NAME_EN = ""
            _ITEM_NAME_TH = ""
            _REGIS = 0
            _SERVE = 0
            _MISSED_CALL = 0
            _CANCLE = 0
            _WAIT_WITH_KPI = 0
            _SERVE_WITH_KPI = 0
            _P_HT = 0
            _AHT = 0
            _MAX_HT = 0
            _COUNT_HT = 0
            _SUM_HT = 0
            _PRODUCTIVITY = ""
            _P_PRODUCTIVITY = ""
            _NON_PRODUCTIVITY = ""
            _P_NON_PRODUCTIVITY = ""
            _TOTAL_TIME = ""
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


        '/// Returns an indication whether the current data is inserted into TB_REP_SUMMARY_REPORT_STAFF table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_SUMMARY_REPORT_STAFF table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_SUMMARY_REPORT_STAFF table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_SUMMARY_REPORT_STAFF table successfully.
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


        '/// Returns an indication whether the record of TB_REP_SUMMARY_REPORT_STAFF by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_SUMMARY_REPORT_STAFF by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepSummaryReportStaffCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_SUMMARY_REPORT_STAFF by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepSummaryReportStaffCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_SUMMARY_REPORT_STAFF by specified ITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID>The ITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID(cITEM_ID As Integer, cNETWORK_TYPE As String, cSEGMENT_TYPE As String, cSERVICE_DATE As DateTime, cSHOP_ID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("ITEM_ID = " & DB.SetDouble(cITEM_ID) & " AND NETWORK_TYPE = " & DB.SetString(cNETWORK_TYPE) & " AND SEGMENT_TYPE = " & DB.SetString(cSEGMENT_TYPE) & " AND SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_SUMMARY_REPORT_STAFF by specified ITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID>The ITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByITEM_ID_NETWORK_TYPE_SEGMENT_TYPE_SERVICE_DATE_SHOP_ID(cITEM_ID As Integer, cNETWORK_TYPE As String, cSEGMENT_TYPE As String, cSERVICE_DATE As DateTime, cSHOP_ID As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ITEM_ID = " & DB.SetDouble(cITEM_ID) & " AND NETWORK_TYPE = " & DB.SetString(cNETWORK_TYPE) & " AND SEGMENT_TYPE = " & DB.SetString(cSEGMENT_TYPE) & " AND SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_SUMMARY_REPORT_STAFF by specified SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_SHOP_ID>The SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE_SHOP_ID(cSERVICE_DATE As DateTime, cSHOP_ID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_SUMMARY_REPORT_STAFF by specified SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_SHOP_ID>The SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE_SHOP_ID(cSERVICE_DATE As DateTime, cSHOP_ID As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_SUMMARY_REPORT_STAFF by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_SUMMARY_REPORT_STAFF table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_SUMMARY_REPORT_STAFF table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_SUMMARY_REPORT_STAFF table successfully.
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


        '/// Returns an indication whether the record of TB_REP_SUMMARY_REPORT_STAFF by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("shop_location_group")) = False Then _shop_location_group = Rdr("shop_location_group").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then _show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("emp_id")) = False Then _emp_id = Rdr("emp_id").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("segment_type")) = False Then _segment_type = Rdr("segment_type").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_name_en")) = False Then _item_name_en = Rdr("item_name_en").ToString()
                        If Convert.IsDBNull(Rdr("item_name_th")) = False Then _item_name_th = Rdr("item_name_th").ToString()
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then _serve = Convert.ToInt32(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then _missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancle")) = False Then _cancle = Convert.ToInt32(Rdr("cancle"))
                        If Convert.IsDBNull(Rdr("wait_with_kpi")) = False Then _wait_with_kpi = Convert.ToInt32(Rdr("wait_with_kpi"))
                        If Convert.IsDBNull(Rdr("serve_with_kpi")) = False Then _serve_with_kpi = Convert.ToInt32(Rdr("serve_with_kpi"))
                        If Convert.IsDBNull(Rdr("p_ht")) = False Then _p_ht = Convert.ToInt32(Rdr("p_ht"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt64(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("max_ht")) = False Then _max_ht = Convert.ToInt64(Rdr("max_ht"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt64(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt64(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("productivity")) = False Then _productivity = Rdr("productivity").ToString()
                        If Convert.IsDBNull(Rdr("p_productivity")) = False Then _p_productivity = Rdr("p_productivity").ToString()
                        If Convert.IsDBNull(Rdr("non_productivity")) = False Then _non_productivity = Rdr("non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("p_non_productivity")) = False Then _p_non_productivity = Rdr("p_non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("total_time")) = False Then _total_time = Rdr("total_time").ToString()
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


        '/// Returns an indication whether the record of TB_REP_SUMMARY_REPORT_STAFF by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepSummaryReportStaffCenLinqDB
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
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("shop_location_group")) = False Then _shop_location_group = Rdr("shop_location_group").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then _show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("emp_id")) = False Then _emp_id = Rdr("emp_id").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("segment_type")) = False Then _segment_type = Rdr("segment_type").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_name_en")) = False Then _item_name_en = Rdr("item_name_en").ToString()
                        If Convert.IsDBNull(Rdr("item_name_th")) = False Then _item_name_th = Rdr("item_name_th").ToString()
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then _serve = Convert.ToInt32(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then _missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancle")) = False Then _cancle = Convert.ToInt32(Rdr("cancle"))
                        If Convert.IsDBNull(Rdr("wait_with_kpi")) = False Then _wait_with_kpi = Convert.ToInt32(Rdr("wait_with_kpi"))
                        If Convert.IsDBNull(Rdr("serve_with_kpi")) = False Then _serve_with_kpi = Convert.ToInt32(Rdr("serve_with_kpi"))
                        If Convert.IsDBNull(Rdr("p_ht")) = False Then _p_ht = Convert.ToInt32(Rdr("p_ht"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt64(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("max_ht")) = False Then _max_ht = Convert.ToInt64(Rdr("max_ht"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt64(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt64(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("productivity")) = False Then _productivity = Rdr("productivity").ToString()
                        If Convert.IsDBNull(Rdr("p_productivity")) = False Then _p_productivity = Rdr("p_productivity").ToString()
                        If Convert.IsDBNull(Rdr("non_productivity")) = False Then _non_productivity = Rdr("non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("p_non_productivity")) = False Then _p_non_productivity = Rdr("p_non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("total_time")) = False Then _total_time = Rdr("total_time").ToString()
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


        '/// Returns an indication whether the Class Data of TB_REP_SUMMARY_REPORT_STAFF by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepSummaryReportStaffCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepSummaryReportStaffCenParaDB
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
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then ret.shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then ret.shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then ret.shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("shop_location_group")) = False Then ret.shop_location_group = Rdr("shop_location_group").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then ret.show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then ret.staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("emp_id")) = False Then ret.emp_id = Rdr("emp_id").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("segment_type")) = False Then ret.segment_type = Rdr("segment_type").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then ret.item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_name_en")) = False Then ret.item_name_en = Rdr("item_name_en").ToString()
                        If Convert.IsDBNull(Rdr("item_name_th")) = False Then ret.item_name_th = Rdr("item_name_th").ToString()
                        If Convert.IsDBNull(Rdr("regis")) = False Then ret.regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then ret.serve = Convert.ToInt32(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then ret.missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancle")) = False Then ret.cancle = Convert.ToInt32(Rdr("cancle"))
                        If Convert.IsDBNull(Rdr("wait_with_kpi")) = False Then ret.wait_with_kpi = Convert.ToInt32(Rdr("wait_with_kpi"))
                        If Convert.IsDBNull(Rdr("serve_with_kpi")) = False Then ret.serve_with_kpi = Convert.ToInt32(Rdr("serve_with_kpi"))
                        If Convert.IsDBNull(Rdr("p_ht")) = False Then ret.p_ht = Convert.ToInt32(Rdr("p_ht"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then ret.aht = Convert.ToInt64(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("max_ht")) = False Then ret.max_ht = Convert.ToInt64(Rdr("max_ht"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then ret.count_ht = Convert.ToInt64(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then ret.sum_ht = Convert.ToInt64(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("productivity")) = False Then ret.productivity = Rdr("productivity").ToString()
                        If Convert.IsDBNull(Rdr("p_productivity")) = False Then ret.p_productivity = Rdr("p_productivity").ToString()
                        If Convert.IsDBNull(Rdr("non_productivity")) = False Then ret.non_productivity = Rdr("non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("p_non_productivity")) = False Then ret.p_non_productivity = Rdr("p_non_productivity").ToString()
                        If Convert.IsDBNull(Rdr("total_time")) = False Then ret.total_time = Rdr("total_time").ToString()

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


        'Get Insert Statement for table TB_REP_SUMMARY_REPORT_STAFF
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SHOP_LOCATION_GROUP, SERVICE_DATE, SHOW_DATE, STAFF_NAME, EMP_ID, NETWORK_TYPE, SEGMENT_TYPE, ITEM_ID, ITEM_NAME_EN, ITEM_NAME_TH, REGIS, SERVE, MISSED_CALL, CANCLE, WAIT_WITH_KPI, SERVE_WITH_KPI, P_HT, AHT, MAX_HT, COUNT_HT, SUM_HT, PRODUCTIVITY, P_PRODUCTIVITY, NON_PRODUCTIVITY, P_NON_PRODUCTIVITY, TOTAL_TIME)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetString(_SHOP_LOCATION_GROUP) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetString(_SHOW_DATE) & ", "
                sql += DB.SetString(_STAFF_NAME) & ", "
                sql += DB.SetString(_EMP_ID) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_SEGMENT_TYPE) & ", "
                sql += DB.SetDouble(_ITEM_ID) & ", "
                sql += DB.SetString(_ITEM_NAME_EN) & ", "
                sql += DB.SetString(_ITEM_NAME_TH) & ", "
                sql += DB.SetDouble(_REGIS) & ", "
                sql += DB.SetDouble(_SERVE) & ", "
                sql += DB.SetDouble(_MISSED_CALL) & ", "
                sql += DB.SetDouble(_CANCLE) & ", "
                sql += DB.SetDouble(_WAIT_WITH_KPI) & ", "
                sql += DB.SetDouble(_SERVE_WITH_KPI) & ", "
                sql += DB.SetDouble(_P_HT) & ", "
                sql += DB.SetDouble(_AHT) & ", "
                sql += DB.SetDouble(_MAX_HT) & ", "
                sql += DB.SetDouble(_COUNT_HT) & ", "
                sql += DB.SetDouble(_SUM_HT) & ", "
                sql += DB.SetString(_PRODUCTIVITY) & ", "
                sql += DB.SetString(_P_PRODUCTIVITY) & ", "
                sql += DB.SetString(_NON_PRODUCTIVITY) & ", "
                sql += DB.SetString(_P_NON_PRODUCTIVITY) & ", "
                sql += DB.SetString(_TOTAL_TIME)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_SUMMARY_REPORT_STAFF
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "SHOP_LOCATION_GROUP = " & DB.SetString(_SHOP_LOCATION_GROUP) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "SHOW_DATE = " & DB.SetString(_SHOW_DATE) & ", "
                Sql += "STAFF_NAME = " & DB.SetString(_STAFF_NAME) & ", "
                Sql += "EMP_ID = " & DB.SetString(_EMP_ID) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "SEGMENT_TYPE = " & DB.SetString(_SEGMENT_TYPE) & ", "
                Sql += "ITEM_ID = " & DB.SetDouble(_ITEM_ID) & ", "
                Sql += "ITEM_NAME_EN = " & DB.SetString(_ITEM_NAME_EN) & ", "
                Sql += "ITEM_NAME_TH = " & DB.SetString(_ITEM_NAME_TH) & ", "
                Sql += "REGIS = " & DB.SetDouble(_REGIS) & ", "
                Sql += "SERVE = " & DB.SetDouble(_SERVE) & ", "
                Sql += "MISSED_CALL = " & DB.SetDouble(_MISSED_CALL) & ", "
                Sql += "CANCLE = " & DB.SetDouble(_CANCLE) & ", "
                Sql += "WAIT_WITH_KPI = " & DB.SetDouble(_WAIT_WITH_KPI) & ", "
                Sql += "SERVE_WITH_KPI = " & DB.SetDouble(_SERVE_WITH_KPI) & ", "
                Sql += "P_HT = " & DB.SetDouble(_P_HT) & ", "
                Sql += "AHT = " & DB.SetDouble(_AHT) & ", "
                Sql += "MAX_HT = " & DB.SetDouble(_MAX_HT) & ", "
                Sql += "COUNT_HT = " & DB.SetDouble(_COUNT_HT) & ", "
                Sql += "SUM_HT = " & DB.SetDouble(_SUM_HT) & ", "
                Sql += "PRODUCTIVITY = " & DB.SetString(_PRODUCTIVITY) & ", "
                Sql += "P_PRODUCTIVITY = " & DB.SetString(_P_PRODUCTIVITY) & ", "
                Sql += "NON_PRODUCTIVITY = " & DB.SetString(_NON_PRODUCTIVITY) & ", "
                Sql += "P_NON_PRODUCTIVITY = " & DB.SetString(_P_NON_PRODUCTIVITY) & ", "
                Sql += "TOTAL_TIME = " & DB.SetString(_TOTAL_TIME) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_SUMMARY_REPORT_STAFF
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_SUMMARY_REPORT_STAFF
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SHOP_LOCATION_GROUP, SERVICE_DATE, SHOW_DATE, STAFF_NAME, EMP_ID, NETWORK_TYPE, SEGMENT_TYPE, ITEM_ID, ITEM_NAME_EN, ITEM_NAME_TH, REGIS, SERVE, MISSED_CALL, CANCLE, WAIT_WITH_KPI, SERVE_WITH_KPI, P_HT, AHT, MAX_HT, COUNT_HT, SUM_HT, PRODUCTIVITY, P_PRODUCTIVITY, NON_PRODUCTIVITY, P_NON_PRODUCTIVITY, TOTAL_TIME FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
