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
    'Represents a transaction for TB_REP_WT_HT_STAFF_WEEK table CenLinqDB.
    '[Create by  on March, 18 2013]
    Public Class TbRepWtHtStaffWeekCenLinqDB
        Public sub TbRepWtHtStaffWeekCenLinqDB()

        End Sub 
        ' TB_REP_WT_HT_STAFF_WEEK
        Const _tableName As String = "TB_REP_WT_HT_STAFF_WEEK"
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
        Dim _WEEK_OF_YEAR As Long = 0
        Dim _SHOW_YEAR As Long = 0
        Dim _PERIOD_DATE As String = ""
        Dim _USER_CODE As String = ""
        Dim _USERNAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_TH As String = ""
        Dim _SERVICE_NAME_EN As String = ""
        Dim _NUMBER_OF_QUEUE As Long = 0
        Dim _REGIS As Long = 0
        Dim _SERVE As Long = 0
        Dim _MISS_CALL As Long = 0
        Dim _CANCEL As Long = 0
        Dim _NOT_CALL As Long = 0
        Dim _NOT_CON As Long = 0
        Dim _NOT_END As Long = 0
        Dim _SUM_WT As Long = 0
        Dim _COUNT_WT As Long = 0
        Dim _AVG_WT As Long = 0
        Dim _SUM_HT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _AVG_HT As Long = 0
        Dim _SUM_CONT As Long = 0
        Dim _COUNT_CONT As Long = 0
        Dim _AVG_CONT As Long = 0

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
        <Column(Storage:="_WEEK_OF_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEEK_OF_YEAR() As Long
            Get
                Return _WEEK_OF_YEAR
            End Get
            Set(ByVal value As Long)
               _WEEK_OF_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_YEAR() As Long
            Get
                Return _SHOW_YEAR
            End Get
            Set(ByVal value As Long)
               _SHOW_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_PERIOD_DATE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PERIOD_DATE() As String
            Get
                Return _PERIOD_DATE
            End Get
            Set(ByVal value As String)
               _PERIOD_DATE = value
            End Set
        End Property 
        <Column(Storage:="_USER_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_CODE() As String
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As String)
               _USER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
               _USERNAME = value
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
        <Column(Storage:="_SERVICE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As Long
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As Long)
               _SERVICE_ID = value
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
        <Column(Storage:="_SERVICE_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_EN() As String
            Get
                Return _SERVICE_NAME_EN
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_NUMBER_OF_QUEUE", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property NUMBER_OF_QUEUE() As Long
            Get
                Return _NUMBER_OF_QUEUE
            End Get
            Set(ByVal value As Long)
               _NUMBER_OF_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_REGIS", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
               _REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERVE", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE() As Long
            Get
                Return _SERVE
            End Get
            Set(ByVal value As Long)
               _SERVE = value
            End Set
        End Property 
        <Column(Storage:="_MISS_CALL", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property MISS_CALL() As Long
            Get
                Return _MISS_CALL
            End Get
            Set(ByVal value As Long)
               _MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_CANCEL", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCEL() As Long
            Get
                Return _CANCEL
            End Get
            Set(ByVal value As Long)
               _CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CALL", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CALL() As Long
            Get
                Return _NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CON", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CON() As Long
            Get
                Return _NOT_CON
            End Get
            Set(ByVal value As Long)
               _NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NOT_END", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_END() As Long
            Get
                Return _NOT_END
            End Get
            Set(ByVal value As Long)
               _NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_SUM_WT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_WT() As Long
            Get
                Return _SUM_WT
            End Get
            Set(ByVal value As Long)
               _SUM_WT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_WT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_WT() As Long
            Get
                Return _COUNT_WT
            End Get
            Set(ByVal value As Long)
               _COUNT_WT = value
            End Set
        End Property 
        <Column(Storage:="_AVG_WT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property AVG_WT() As Long
            Get
                Return _AVG_WT
            End Get
            Set(ByVal value As Long)
               _AVG_WT = value
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
        <Column(Storage:="_COUNT_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_HT() As Long
            Get
                Return _COUNT_HT
            End Get
            Set(ByVal value As Long)
               _COUNT_HT = value
            End Set
        End Property 
        <Column(Storage:="_AVG_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property AVG_HT() As Long
            Get
                Return _AVG_HT
            End Get
            Set(ByVal value As Long)
               _AVG_HT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_CONT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_CONT() As Long
            Get
                Return _SUM_CONT
            End Get
            Set(ByVal value As Long)
               _SUM_CONT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_CONT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_CONT() As Long
            Get
                Return _COUNT_CONT
            End Get
            Set(ByVal value As Long)
               _COUNT_CONT = value
            End Set
        End Property 
        <Column(Storage:="_AVG_CONT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property AVG_CONT() As Long
            Get
                Return _AVG_CONT
            End Get
            Set(ByVal value As Long)
               _AVG_CONT = value
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
            _WEEK_OF_YEAR = 0
            _SHOW_YEAR = 0
            _PERIOD_DATE = ""
            _USER_CODE = ""
            _USERNAME = ""
            _STAFF_NAME = ""
            _SERVICE_ID = 0
            _SERVICE_NAME_TH = ""
            _SERVICE_NAME_EN = ""
            _NUMBER_OF_QUEUE = 0
            _REGIS = 0
            _SERVE = 0
            _MISS_CALL = 0
            _CANCEL = 0
            _NOT_CALL = 0
            _NOT_CON = 0
            _NOT_END = 0
            _SUM_WT = 0
            _COUNT_WT = 0
            _AVG_WT = 0
            _SUM_HT = 0
            _COUNT_HT = 0
            _AVG_HT = 0
            _SUM_CONT = 0
            _COUNT_CONT = 0
            _AVG_CONT = 0
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


        '/// Returns an indication whether the current data is inserted into TB_REP_WT_HT_STAFF_WEEK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_STAFF_WEEK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_STAFF_WEEK table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_WT_HT_STAFF_WEEK table successfully.
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_STAFF_WEEK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_WT_HT_STAFF_WEEK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepWtHtStaffWeekCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_WT_HT_STAFF_WEEK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepWtHtStaffWeekCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_STAFF_WEEK by specified SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key is retrieved successfully.
        '/// <param name=cSHOP_ID_SHOW_YEAR_WEEK_OF_YEAR>The SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySHOP_ID_SHOW_YEAR_WEEK_OF_YEAR(cSHOP_ID As Integer, cSHOW_YEAR As Integer, cWEEK_OF_YEAR As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " AND WEEK_OF_YEAR = " & DB.SetDouble(cWEEK_OF_YEAR), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_WT_HT_STAFF_WEEK by specified SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key is retrieved successfully.
        '/// <param name=cSHOP_ID_SHOW_YEAR_WEEK_OF_YEAR>The SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySHOP_ID_SHOW_YEAR_WEEK_OF_YEAR(cSHOP_ID As Integer, cSHOW_YEAR As Integer, cWEEK_OF_YEAR As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " AND WEEK_OF_YEAR = " & DB.SetDouble(cWEEK_OF_YEAR) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_STAFF_WEEK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_WT_HT_STAFF_WEEK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_STAFF_WEEK table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_WT_HT_STAFF_WEEK table successfully.
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_STAFF_WEEK by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("week_of_year")) = False Then _week_of_year = Convert.ToInt32(Rdr("week_of_year"))
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("period_date")) = False Then _period_date = Rdr("period_date").ToString()
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("number_of_queue")) = False Then _number_of_queue = Convert.ToInt64(Rdr("number_of_queue"))
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt64(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then _serve = Convert.ToInt64(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("miss_call")) = False Then _miss_call = Convert.ToInt64(Rdr("miss_call"))
                        If Convert.IsDBNull(Rdr("cancel")) = False Then _cancel = Convert.ToInt64(Rdr("cancel"))
                        If Convert.IsDBNull(Rdr("not_call")) = False Then _not_call = Convert.ToInt64(Rdr("not_call"))
                        If Convert.IsDBNull(Rdr("not_con")) = False Then _not_con = Convert.ToInt64(Rdr("not_con"))
                        If Convert.IsDBNull(Rdr("not_end")) = False Then _not_end = Convert.ToInt64(Rdr("not_end"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then _sum_wt = Convert.ToInt64(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then _count_wt = Convert.ToInt64(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("avg_wt")) = False Then _avg_wt = Convert.ToInt64(Rdr("avg_wt"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt64(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt64(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("avg_ht")) = False Then _avg_ht = Convert.ToInt64(Rdr("avg_ht"))
                        If Convert.IsDBNull(Rdr("sum_cont")) = False Then _sum_cont = Convert.ToInt64(Rdr("sum_cont"))
                        If Convert.IsDBNull(Rdr("count_cont")) = False Then _count_cont = Convert.ToInt64(Rdr("count_cont"))
                        If Convert.IsDBNull(Rdr("avg_cont")) = False Then _avg_cont = Convert.ToInt64(Rdr("avg_cont"))
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_STAFF_WEEK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepWtHtStaffWeekCenLinqDB
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
                        If Convert.IsDBNull(Rdr("week_of_year")) = False Then _week_of_year = Convert.ToInt32(Rdr("week_of_year"))
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("period_date")) = False Then _period_date = Rdr("period_date").ToString()
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("number_of_queue")) = False Then _number_of_queue = Convert.ToInt64(Rdr("number_of_queue"))
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt64(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then _serve = Convert.ToInt64(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("miss_call")) = False Then _miss_call = Convert.ToInt64(Rdr("miss_call"))
                        If Convert.IsDBNull(Rdr("cancel")) = False Then _cancel = Convert.ToInt64(Rdr("cancel"))
                        If Convert.IsDBNull(Rdr("not_call")) = False Then _not_call = Convert.ToInt64(Rdr("not_call"))
                        If Convert.IsDBNull(Rdr("not_con")) = False Then _not_con = Convert.ToInt64(Rdr("not_con"))
                        If Convert.IsDBNull(Rdr("not_end")) = False Then _not_end = Convert.ToInt64(Rdr("not_end"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then _sum_wt = Convert.ToInt64(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then _count_wt = Convert.ToInt64(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("avg_wt")) = False Then _avg_wt = Convert.ToInt64(Rdr("avg_wt"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt64(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt64(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("avg_ht")) = False Then _avg_ht = Convert.ToInt64(Rdr("avg_ht"))
                        If Convert.IsDBNull(Rdr("sum_cont")) = False Then _sum_cont = Convert.ToInt64(Rdr("sum_cont"))
                        If Convert.IsDBNull(Rdr("count_cont")) = False Then _count_cont = Convert.ToInt64(Rdr("count_cont"))
                        If Convert.IsDBNull(Rdr("avg_cont")) = False Then _avg_cont = Convert.ToInt64(Rdr("avg_cont"))
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


        '/// Returns an indication whether the Class Data of TB_REP_WT_HT_STAFF_WEEK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepWtHtStaffWeekCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepWtHtStaffWeekCenParaDB
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
                        If Convert.IsDBNull(Rdr("week_of_year")) = False Then ret.week_of_year = Convert.ToInt32(Rdr("week_of_year"))
                        If Convert.IsDBNull(Rdr("show_year")) = False Then ret.show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("period_date")) = False Then ret.period_date = Rdr("period_date").ToString()
                        If Convert.IsDBNull(Rdr("user_code")) = False Then ret.user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then ret.username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then ret.staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then ret.service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then ret.service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("number_of_queue")) = False Then ret.number_of_queue = Convert.ToInt64(Rdr("number_of_queue"))
                        If Convert.IsDBNull(Rdr("regis")) = False Then ret.regis = Convert.ToInt64(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then ret.serve = Convert.ToInt64(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("miss_call")) = False Then ret.miss_call = Convert.ToInt64(Rdr("miss_call"))
                        If Convert.IsDBNull(Rdr("cancel")) = False Then ret.cancel = Convert.ToInt64(Rdr("cancel"))
                        If Convert.IsDBNull(Rdr("not_call")) = False Then ret.not_call = Convert.ToInt64(Rdr("not_call"))
                        If Convert.IsDBNull(Rdr("not_con")) = False Then ret.not_con = Convert.ToInt64(Rdr("not_con"))
                        If Convert.IsDBNull(Rdr("not_end")) = False Then ret.not_end = Convert.ToInt64(Rdr("not_end"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then ret.sum_wt = Convert.ToInt64(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then ret.count_wt = Convert.ToInt64(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("avg_wt")) = False Then ret.avg_wt = Convert.ToInt64(Rdr("avg_wt"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then ret.sum_ht = Convert.ToInt64(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then ret.count_ht = Convert.ToInt64(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("avg_ht")) = False Then ret.avg_ht = Convert.ToInt64(Rdr("avg_ht"))
                        If Convert.IsDBNull(Rdr("sum_cont")) = False Then ret.sum_cont = Convert.ToInt64(Rdr("sum_cont"))
                        If Convert.IsDBNull(Rdr("count_cont")) = False Then ret.count_cont = Convert.ToInt64(Rdr("count_cont"))
                        If Convert.IsDBNull(Rdr("avg_cont")) = False Then ret.avg_cont = Convert.ToInt64(Rdr("avg_cont"))

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


        'Get Insert Statement for table TB_REP_WT_HT_STAFF_WEEK
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, WEEK_OF_YEAR, SHOW_YEAR, PERIOD_DATE, USER_CODE, USERNAME, STAFF_NAME, SERVICE_ID, SERVICE_NAME_TH, SERVICE_NAME_EN, NUMBER_OF_QUEUE, REGIS, SERVE, MISS_CALL, CANCEL, NOT_CALL, NOT_CON, NOT_END, SUM_WT, COUNT_WT, AVG_WT, SUM_HT, COUNT_HT, AVG_HT, SUM_CONT, COUNT_CONT, AVG_CONT)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDouble(_WEEK_OF_YEAR) & ", "
                sql += DB.SetDouble(_SHOW_YEAR) & ", "
                sql += DB.SetString(_PERIOD_DATE) & ", "
                sql += DB.SetString(_USER_CODE) & ", "
                sql += DB.SetString(_USERNAME) & ", "
                sql += DB.SetString(_STAFF_NAME) & ", "
                sql += DB.SetDouble(_SERVICE_ID) & ", "
                sql += DB.SetString(_SERVICE_NAME_TH) & ", "
                sql += DB.SetString(_SERVICE_NAME_EN) & ", "
                sql += DB.SetDouble(_NUMBER_OF_QUEUE) & ", "
                sql += DB.SetDouble(_REGIS) & ", "
                sql += DB.SetDouble(_SERVE) & ", "
                sql += DB.SetDouble(_MISS_CALL) & ", "
                sql += DB.SetDouble(_CANCEL) & ", "
                sql += DB.SetDouble(_NOT_CALL) & ", "
                sql += DB.SetDouble(_NOT_CON) & ", "
                sql += DB.SetDouble(_NOT_END) & ", "
                sql += DB.SetDouble(_SUM_WT) & ", "
                sql += DB.SetDouble(_COUNT_WT) & ", "
                sql += DB.SetDouble(_AVG_WT) & ", "
                sql += DB.SetDouble(_SUM_HT) & ", "
                sql += DB.SetDouble(_COUNT_HT) & ", "
                sql += DB.SetDouble(_AVG_HT) & ", "
                sql += DB.SetDouble(_SUM_CONT) & ", "
                sql += DB.SetDouble(_COUNT_CONT) & ", "
                sql += DB.SetDouble(_AVG_CONT)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_WT_HT_STAFF_WEEK
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
                Sql += "WEEK_OF_YEAR = " & DB.SetDouble(_WEEK_OF_YEAR) & ", "
                Sql += "SHOW_YEAR = " & DB.SetDouble(_SHOW_YEAR) & ", "
                Sql += "PERIOD_DATE = " & DB.SetString(_PERIOD_DATE) & ", "
                Sql += "USER_CODE = " & DB.SetString(_USER_CODE) & ", "
                Sql += "USERNAME = " & DB.SetString(_USERNAME) & ", "
                Sql += "STAFF_NAME = " & DB.SetString(_STAFF_NAME) & ", "
                Sql += "SERVICE_ID = " & DB.SetDouble(_SERVICE_ID) & ", "
                Sql += "SERVICE_NAME_TH = " & DB.SetString(_SERVICE_NAME_TH) & ", "
                Sql += "SERVICE_NAME_EN = " & DB.SetString(_SERVICE_NAME_EN) & ", "
                Sql += "NUMBER_OF_QUEUE = " & DB.SetDouble(_NUMBER_OF_QUEUE) & ", "
                Sql += "REGIS = " & DB.SetDouble(_REGIS) & ", "
                Sql += "SERVE = " & DB.SetDouble(_SERVE) & ", "
                Sql += "MISS_CALL = " & DB.SetDouble(_MISS_CALL) & ", "
                Sql += "CANCEL = " & DB.SetDouble(_CANCEL) & ", "
                Sql += "NOT_CALL = " & DB.SetDouble(_NOT_CALL) & ", "
                Sql += "NOT_CON = " & DB.SetDouble(_NOT_CON) & ", "
                Sql += "NOT_END = " & DB.SetDouble(_NOT_END) & ", "
                Sql += "SUM_WT = " & DB.SetDouble(_SUM_WT) & ", "
                Sql += "COUNT_WT = " & DB.SetDouble(_COUNT_WT) & ", "
                Sql += "AVG_WT = " & DB.SetDouble(_AVG_WT) & ", "
                Sql += "SUM_HT = " & DB.SetDouble(_SUM_HT) & ", "
                Sql += "COUNT_HT = " & DB.SetDouble(_COUNT_HT) & ", "
                Sql += "AVG_HT = " & DB.SetDouble(_AVG_HT) & ", "
                Sql += "SUM_CONT = " & DB.SetDouble(_SUM_CONT) & ", "
                Sql += "COUNT_CONT = " & DB.SetDouble(_COUNT_CONT) & ", "
                Sql += "AVG_CONT = " & DB.SetDouble(_AVG_CONT) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_WT_HT_STAFF_WEEK
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_WT_HT_STAFF_WEEK
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, WEEK_OF_YEAR, SHOW_YEAR, PERIOD_DATE, USER_CODE, USERNAME, STAFF_NAME, SERVICE_ID, SERVICE_NAME_TH, SERVICE_NAME_EN, NUMBER_OF_QUEUE, REGIS, SERVE, MISS_CALL, CANCEL, NOT_CALL, NOT_CON, NOT_END, SUM_WT, COUNT_WT, AVG_WT, SUM_HT, COUNT_HT, AVG_HT, SUM_CONT, COUNT_CONT, AVG_CONT FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
