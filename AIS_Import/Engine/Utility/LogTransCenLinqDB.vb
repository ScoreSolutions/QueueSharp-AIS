Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = Engine.Utilities.SqlDB
Imports Engine.Utilities

Namespace Utilities
    'Represents a transaction for LOG_TRANS table CenLinqDB.
    '[Create by  on Febuary, 2 2012]
    Public Class LogTransCenLinqDB
        Public Sub LogTransCenLinqDB()

        End Sub
        ' LOG_TRANS
        Const _tableName As String = "LOG_TRANS"
        Dim _deletedRow As Int16 = 0

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage() As String
            Get
                Return _information
            End Get
        End Property
        Public ReadOnly Property HaveData() As Boolean
            Get
                Return _haveData
            End Get
        End Property


        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _UPDATE_BY As String = ""
        Dim _UPDATE_DATE As System.Nullable(Of DateTime) = New DateTime(1, 1, 1)
        Dim _LOGIN_HIS_ID As Long = 0
        Dim _TRANS_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _TRANS_DESC As String = ""

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ", CanBeNull:=False)> _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
                _ID = value
            End Set
        End Property
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50) NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
                _CREATE_BY = value
            End Set
        End Property
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime2 NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(50)")> _
        Public Property UPDATE_BY() As String
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As String)
                _UPDATE_BY = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime2")> _
        Public Property UPDATE_DATE() As System.Nullable(Of DateTime)
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _UPDATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_LOGIN_HIS_ID", DbType:="BigInt NOT NULL ", CanBeNull:=False)> _
        Public Property LOGIN_HIS_ID() As Long
            Get
                Return _LOGIN_HIS_ID
            End Get
            Set(ByVal value As Long)
                _LOGIN_HIS_ID = value
            End Set
        End Property
        <Column(Storage:="_TRANS_DATE", DbType:="DateTime2 NOT NULL ", CanBeNull:=False)> _
        Public Property TRANS_DATE() As DateTime
            Get
                Return _TRANS_DATE
            End Get
            Set(ByVal value As DateTime)
                _TRANS_DATE = value
            End Set
        End Property
        <Column(Storage:="_TRANS_DESC", DbType:="VarChar(100) NOT NULL ", CanBeNull:=False)> _
        Public Property TRANS_DESC() As String
            Get
                Return _TRANS_DESC
            End Get
            Set(ByVal value As String)
                _TRANS_DESC = value
            End Set
        End Property


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1, 1, 1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1, 1, 1)
            _LOGIN_HIS_ID = 0
            _TRANS_DATE = New DateTime(1, 1, 1)
            _TRANS_DESC = ""
        End Sub

        'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(ByVal whClause As String, ByVal orderBy As String, ByVal trans As SqlTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIf(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(ByVal Sql As String, ByVal trans As SqlTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into LOG_TRANS table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(ByVal LoginName As String, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                _ID = DB.GetNextID("id", TableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to LOG_TRANS table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(ByVal LoginName As String, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("id = " & DB.SetDouble(_ID) & " ", trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to LOG_TRANS table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(ByVal Sql As String, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                Return DB.ExecuteNonQuery(Sql, trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is deleted from LOG_TRANS table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(ByVal cPK As Long, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                Return doDelete("id = " & cPK, trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the record of LOG_TRANS by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(ByVal cid As Long, ByVal trans As SqlTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of LOG_TRANS by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(ByVal cid As Long, ByVal trans As SqlTransaction) As LogTransCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of LOG_TRANS by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(ByVal cid As Long, ByVal trans As SqlTransaction) As LogTransCenParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of LOG_TRANS by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into LOG_TRANS table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(ByVal trans As SqlTransaction) As Boolean
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
                    ret = False
                    _error = ex.Message
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC101
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEN002
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is updated to LOG_TRANS table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = True Then
                If whText.Trim() <> "" Then
                    Dim tmpWhere As String = " Where " & whText
                    Try

                        ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = DB.ErrorMessage
                        End If
                        _information = MessageResources.MSGIU001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message
                    Catch ex As Exception
                        ex.ToString()
                        ret = False
                        _error = MessageResources.MSGEC102
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGEU003
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from LOG_TRANS table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> "" Then
                    Dim tmpWhere As String = " Where " & whText
                    Try
                        ret = (DB.ExecuteNonQuery(SqlDelete & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = MessageResources.MSGED001
                        End If
                        _information = MessageResources.MSGID001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message
                    Catch ex As Exception
                        ex.ToString()
                        ret = False
                        _error = MessageResources.MSGEC103
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGED003
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of LOG_TRANS by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            ClearData()
            _haveData = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SqlDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _ID = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _CREATE_BY = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _CREATE_DATE = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _UPDATE_BY = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _UPDATE_DATE = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("login_his_id")) = False Then _LOGIN_HIS_ID = Convert.ToInt64(Rdr("login_his_id"))
                        If Convert.IsDBNull(Rdr("trans_date")) = False Then _TRANS_DATE = Convert.ToDateTime(Rdr("trans_date"))
                        If Convert.IsDBNull(Rdr("trans_desc")) = False Then _TRANS_DESC = Rdr("trans_desc").ToString()
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed = False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of LOG_TRANS by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(ByVal whText As String, ByVal trans As SqlTransaction) As LogTransCenLinqDB
            ClearData()
            _haveData = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SqlDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _ID = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _CREATE_BY = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _CREATE_DATE = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _UPDATE_BY = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _UPDATE_DATE = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("login_his_id")) = False Then _LOGIN_HIS_ID = Convert.ToInt64(Rdr("login_his_id"))
                        If Convert.IsDBNull(Rdr("trans_date")) = False Then _TRANS_DATE = Convert.ToDateTime(Rdr("trans_date"))
                        If Convert.IsDBNull(Rdr("trans_desc")) = False Then _TRANS_DESC = Rdr("trans_desc").ToString()

                        'Generate Item For Child Table
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed = False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function


        '/// Returns an indication whether the Class Data of LOG_TRANS by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(ByVal whText As String, ByVal trans As SqlTransaction) As LogTransCenParaDB
            ClearData()
            _haveData = False
            Dim ret As New LogTransCenParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SqlDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.ID = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.CREATE_BY = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.CREATE_DATE = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.UPDATE_BY = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.UPDATE_DATE = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("login_his_id")) = False Then ret.LOGIN_HIS_ID = Convert.ToInt64(Rdr("login_his_id"))
                        If Convert.IsDBNull(Rdr("trans_date")) = False Then ret.TRANS_DATE = Convert.ToDateTime(Rdr("trans_date"))
                        If Convert.IsDBNull(Rdr("trans_desc")) = False Then ret.TRANS_DESC = Rdr("trans_desc").ToString()

                        'Generate Item For Child Table

                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed = False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return ret
        End Function

        ' SQL Statements


        'Get Insert Statement for table LOG_TRANS
        Private ReadOnly Property SqlInsert() As String
            Get
                Dim Sql As String = ""
                Sql += "INSERT INTO " & TableName & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, LOGIN_HIS_ID, TRANS_DATE, TRANS_DESC)"
                Sql += " VALUES("
                Sql += DB.SetDouble(_ID) & ", "
                Sql += DB.SetString(_CREATE_BY) & ", "
                Sql += DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += DB.SetString(_UPDATE_BY) & ", "
                Sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += DB.SetDouble(_LOGIN_HIS_ID) & ", "
                Sql += DB.SetDateTime(_TRANS_DATE) & ", "
                Sql += DB.SetString(_TRANS_DESC)
                Sql += ")"
                Return Sql
            End Get
        End Property


        'Get update statement form table LOG_TRANS
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & TableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "LOGIN_HIS_ID = " & DB.SetDouble(_LOGIN_HIS_ID) & ", "
                Sql += "TRANS_DATE = " & DB.SetDateTime(_TRANS_DATE) & ", "
                Sql += "TRANS_DESC = " & DB.SetString(_TRANS_DESC) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table LOG_TRANS
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & TableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table LOG_TRANS
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & TableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace