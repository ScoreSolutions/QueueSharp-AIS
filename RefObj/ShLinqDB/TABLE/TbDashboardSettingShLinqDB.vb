Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = ShLinqDB.Common.Utilities.SQLDB
Imports ShParaDB.TABLE
Imports ShParaDB.Common.Utilities

Namespace TABLE
    'Represents a transaction for TB_DASHBOARD_SETTING table ShLinqDB.
    '[Create by  on June, 23 2012]
    Public Class TbDashboardSettingShLinqDB
        Public sub TbDashboardSettingShLinqDB()

        End Sub 
        ' TB_DASHBOARD_SETTING
        Const _tableName As String = "TB_DASHBOARD_SETTING"
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
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _CSI_HIGH As  System.Nullable(Of Long)  = 0
        Dim _CSI_LOW As  System.Nullable(Of Long)  = 0
        Dim _KPI_HIGH As  System.Nullable(Of Long)  = 0
        Dim _KPI_LOW As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

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
        <Column(Storage:="_ITEM_ID", DbType:="Int")>  _
        Public Property ITEM_ID() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_CSI_HIGH", DbType:="SmallInt")>  _
        Public Property CSI_HIGH() As  System.Nullable(Of Long) 
            Get
                Return _CSI_HIGH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CSI_HIGH = value
            End Set
        End Property 
        <Column(Storage:="_CSI_LOW", DbType:="SmallInt")>  _
        Public Property CSI_LOW() As  System.Nullable(Of Long) 
            Get
                Return _CSI_LOW
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CSI_LOW = value
            End Set
        End Property 
        <Column(Storage:="_KPI_HIGH", DbType:="SmallInt")>  _
        Public Property KPI_HIGH() As  System.Nullable(Of Long) 
            Get
                Return _KPI_HIGH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _KPI_HIGH = value
            End Set
        End Property 
        <Column(Storage:="_KPI_LOW", DbType:="SmallInt")>  _
        Public Property KPI_LOW() As  System.Nullable(Of Long) 
            Get
                Return _KPI_LOW
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _KPI_LOW = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50)")>  _
        Public Property CREATE_BY() As  String 
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As  String )
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


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _ITEM_ID = 0
            _CSI_HIGH = 0
            _CSI_LOW = 0
            _KPI_HIGH = 0
            _KPI_LOW = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
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


        '/// Returns an indication whether the current data is inserted into TB_DASHBOARD_SETTING table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _id = DB.GetNextID("id",tableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_DASHBOARD_SETTING table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("id = " & DB.SetDouble(_id) & " ", trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_DASHBOARD_SETTING table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_DASHBOARD_SETTING table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cPK As Long, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return doDelete("id = " & cPK, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the record of TB_DASHBOARD_SETTING by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_DASHBOARD_SETTING by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Integer, trans As SQLTransaction) As TbDashboardSettingShLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_DASHBOARD_SETTING by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Integer, trans As SQLTransaction) As TbDashboardSettingShParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_DASHBOARD_SETTING by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_DASHBOARD_SETTING table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_DASHBOARD_SETTING table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = True Then
                If whText.Trim() <> ""
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


        '/// Returns an indication whether the current data is deleted from TB_DASHBOARD_SETTING table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> ""
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


        '/// Returns an indication whether the record of TB_DASHBOARD_SETTING by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("csi_high")) = False Then _csi_high = Convert.ToInt16(Rdr("csi_high"))
                        If Convert.IsDBNull(Rdr("csi_low")) = False Then _csi_low = Convert.ToInt16(Rdr("csi_low"))
                        If Convert.IsDBNull(Rdr("kpi_high")) = False Then _kpi_high = Convert.ToInt16(Rdr("kpi_high"))
                        If Convert.IsDBNull(Rdr("kpi_low")) = False Then _kpi_low = Convert.ToInt16(Rdr("kpi_low"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed=False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_DASHBOARD_SETTING by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbDashboardSettingShLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("csi_high")) = False Then _csi_high = Convert.ToInt16(Rdr("csi_high"))
                        If Convert.IsDBNull(Rdr("csi_low")) = False Then _csi_low = Convert.ToInt16(Rdr("csi_low"))
                        If Convert.IsDBNull(Rdr("kpi_high")) = False Then _kpi_high = Convert.ToInt16(Rdr("kpi_high"))
                        If Convert.IsDBNull(Rdr("kpi_low")) = False Then _kpi_low = Convert.ToInt16(Rdr("kpi_low"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))

                        'Generate Item For Child Table
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed=False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function


        '/// Returns an indication whether the Class Data of TB_DASHBOARD_SETTING by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbDashboardSettingShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbDashboardSettingShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("item_id")) = False Then ret.item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("csi_high")) = False Then ret.csi_high = Convert.ToInt16(Rdr("csi_high"))
                        If Convert.IsDBNull(Rdr("csi_low")) = False Then ret.csi_low = Convert.ToInt16(Rdr("csi_low"))
                        If Convert.IsDBNull(Rdr("kpi_high")) = False Then ret.kpi_high = Convert.ToInt16(Rdr("kpi_high"))
                        If Convert.IsDBNull(Rdr("kpi_low")) = False Then ret.kpi_low = Convert.ToInt16(Rdr("kpi_low"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))

                        'Generate Item For Child Table

                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed=False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return ret
        End Function

        ' SQL Statements


        'Get Insert Statement for table TB_DASHBOARD_SETTING
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, ITEM_ID, CSI_HIGH, CSI_LOW, KPI_HIGH, KPI_LOW, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetDouble(_ITEM_ID) & ", "
                sql += DB.SetDouble(_CSI_HIGH) & ", "
                sql += DB.SetDouble(_CSI_LOW) & ", "
                sql += DB.SetDouble(_KPI_HIGH) & ", "
                sql += DB.SetDouble(_KPI_LOW) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_DASHBOARD_SETTING
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "ITEM_ID = " & DB.SetDouble(_ITEM_ID) & ", "
                Sql += "CSI_HIGH = " & DB.SetDouble(_CSI_HIGH) & ", "
                Sql += "CSI_LOW = " & DB.SetDouble(_CSI_LOW) & ", "
                Sql += "KPI_HIGH = " & DB.SetDouble(_KPI_HIGH) & ", "
                Sql += "KPI_LOW = " & DB.SetDouble(_KPI_LOW) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_DASHBOARD_SETTING
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_DASHBOARD_SETTING
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace