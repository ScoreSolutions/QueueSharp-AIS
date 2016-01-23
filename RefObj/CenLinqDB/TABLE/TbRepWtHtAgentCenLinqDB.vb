Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports DB = CenLinqDB.Common.Utilities.SqlDB
Imports CenParaDB.TABLE
Imports CenParaDB.Common.Utilities

Namespace TABLE
    'Represents a transaction for TB_REP_WT_HT_AGENT table CenLinqDB.
    '[Create by  on June, 8 2012]
    Public Class TbRepWtHtAgentCenLinqDB
        Public Sub TbRepWtHtAgentCenLinqDB()

        End Sub
        ' TB_REP_WT_HT_AGENT
        Const _tableName As String = "TB_REP_WT_HT_AGENT"
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
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _SHOW_DATE As String = ""
        Dim _USER_ID As Long = 0
        Dim _USER_CODE As String = ""
        Dim _USERNAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_TYPE As String = ""
        Dim _QUEUE_NO As String = ""
        Dim _MOBILE_NO As String = ""
        Dim _AWT As System.Nullable(Of Long) = 0
        Dim _AHT As System.Nullable(Of Long) = 0
        Dim _ACT As System.Nullable(Of Long) = 0
        Dim _CWT As System.Nullable(Of Long) = 0
        Dim _CHT As System.Nullable(Of Long) = 0
        Dim _CCT As System.Nullable(Of Long) = 0

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
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime NOT NULL ", CanBeNull:=False)> _
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
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime")> _
        Public Property UPDATE_DATE() As System.Nullable(Of DateTime)
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _UPDATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
                _SHOP_ID = value
            End Set
        End Property
        <Column(Storage:="_SHOP_NAME_TH", DbType:="VarChar(255) NOT NULL ", CanBeNull:=False)> _
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
                _SHOP_NAME_TH = value
            End Set
        End Property
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255) NOT NULL ", CanBeNull:=False)> _
        Public Property SHOP_NAME_EN() As String
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As String)
                _SHOP_NAME_EN = value
            End Set
        End Property
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ", CanBeNull:=False)> _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
                _SERVICE_DATE = value
            End Set
        End Property
        <Column(Storage:="_SHOW_DATE", DbType:="VarChar(50) NOT NULL ", CanBeNull:=False)> _
        Public Property SHOW_DATE() As String
            Get
                Return _SHOW_DATE
            End Get
            Set(ByVal value As String)
                _SHOW_DATE = value
            End Set
        End Property
        <Column(Storage:="_USER_ID", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property USER_ID() As Long
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As Long)
                _USER_ID = value
            End Set
        End Property
        <Column(Storage:="_USER_CODE", DbType:="VarChar(255)")> _
        Public Property USER_CODE() As String
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As String)
                _USER_CODE = value
            End Set
        End Property
        <Column(Storage:="_USERNAME", DbType:="VarChar(255) NOT NULL ", CanBeNull:=False)> _
        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
                _USERNAME = value
            End Set
        End Property
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255) NOT NULL ", CanBeNull:=False)> _
        Public Property STAFF_NAME() As String
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As String)
                _STAFF_NAME = value
            End Set
        End Property
        <Column(Storage:="_SERVICE_ID", DbType:="Int NOT NULL ", CanBeNull:=False)> _
        Public Property SERVICE_ID() As Long
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As Long)
                _SERVICE_ID = value
            End Set
        End Property
        <Column(Storage:="_SERVICE_TYPE", DbType:="VarChar(255) NOT NULL ", CanBeNull:=False)> _
        Public Property SERVICE_TYPE() As String
            Get
                Return _SERVICE_TYPE
            End Get
            Set(ByVal value As String)
                _SERVICE_TYPE = value
            End Set
        End Property
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(255)")> _
        Public Property QUEUE_NO() As String
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As String)
                _QUEUE_NO = value
            End Set
        End Property
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(255)")> _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
                _MOBILE_NO = value
            End Set
        End Property
        <Column(Storage:="_AWT", DbType:="BigInt")> _
        Public Property AWT() As System.Nullable(Of Long)
            Get
                Return _AWT
            End Get
            Set(ByVal value As System.Nullable(Of Long))
                _AWT = value
            End Set
        End Property
        <Column(Storage:="_AHT", DbType:="BigInt")> _
        Public Property AHT() As System.Nullable(Of Long)
            Get
                Return _AHT
            End Get
            Set(ByVal value As System.Nullable(Of Long))
                _AHT = value
            End Set
        End Property
        <Column(Storage:="_ACT", DbType:="BigInt")> _
        Public Property ACT() As System.Nullable(Of Long)
            Get
                Return _ACT
            End Get
            Set(ByVal value As System.Nullable(Of Long))
                _ACT = value
            End Set
        End Property
        <Column(Storage:="_CWT", DbType:="BigInt")> _
        Public Property CWT() As System.Nullable(Of Long)
            Get
                Return _CWT
            End Get
            Set(ByVal value As System.Nullable(Of Long))
                _CWT = value
            End Set
        End Property
        <Column(Storage:="_CHT", DbType:="BigInt")> _
        Public Property CHT() As System.Nullable(Of Long)
            Get
                Return _CHT
            End Get
            Set(ByVal value As System.Nullable(Of Long))
                _CHT = value
            End Set
        End Property
        <Column(Storage:="_CCT", DbType:="BigInt")> _
        Public Property CCT() As System.Nullable(Of Long)
            Get
                Return _CCT
            End Get
            Set(ByVal value As System.Nullable(Of Long))
                _CCT = value
            End Set
        End Property


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1, 1, 1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1, 1, 1)
            _SHOP_ID = 0
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _SERVICE_DATE = New DateTime(1, 1, 1)
            _SHOW_DATE = ""
            _USER_ID = 0
            _USER_CODE = ""
            _USERNAME = ""
            _STAFF_NAME = ""
            _SERVICE_ID = 0
            _SERVICE_TYPE = ""
            _QUEUE_NO = ""
            _MOBILE_NO = ""
            _AWT = 0
            _AHT = 0
            _ACT = 0
            _CHT = 0
            _CCT = 0
            _CCT = 0
        End Sub

        'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(ByVal whClause As String, ByVal orderBy As String, ByVal trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIF(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(ByVal Sql As String, ByVal trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into TB_REP_WT_HT_AGENT table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(ByVal LoginName As String, ByVal trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then
                _id = DB.GetNextID("id", tableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_AGENT table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(ByVal LoginName As String, ByVal trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("id = " & DB.SetDouble(_id) & " ", trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_AGENT table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(ByVal Sql As String, ByVal trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then
                Return DB.ExecuteNonQuery(Sql, trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_WT_HT_AGENT table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(ByVal cPK As Long, ByVal trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then
                Return doDelete("id = " & cPK, trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_AGENT by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(ByVal cid As Long, ByVal trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_WT_HT_AGENT by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(ByVal cid As Long, ByVal trans As SQLTransaction) As TbRepWtHtAgentCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_WT_HT_AGENT by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(ByVal cid As Long, ByVal trans As SQLTransaction) As TbRepWtHtAgentCenParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_AGENT by specified SERVICE_DATE key is retrieved successfully.
        '/// <param name=cSERVICE_DATE>The SERVICE_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE(ByVal cSERVICE_DATE As DateTime, ByVal trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_WT_HT_AGENT by specified SERVICE_DATE key is retrieved successfully.
        '/// <param name=cSERVICE_DATE>The SERVICE_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE(ByVal cSERVICE_DATE As DateTime, ByVal cid As Long, ByVal trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_AGENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(ByVal whText As String, ByVal trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_WT_HT_AGENT table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(ByVal trans As SQLTransaction) As Boolean
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_AGENT table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(ByVal whText As String, ByVal trans As SQLTransaction) As Boolean
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


        '/// Returns an indication whether the current data is deleted from TB_REP_WT_HT_AGENT table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(ByVal whText As String, ByVal trans As SQLTransaction) As Boolean
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_AGENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(ByVal whText As String, ByVal trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            ClearData()
            _haveData = False
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then _show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_type")) = False Then _service_type = Rdr("service_type").ToString()
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("awt")) = False Then _awt = Convert.ToInt64(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _AHT = Convert.ToInt64(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("act")) = False Then _AHT = Convert.ToInt64(Rdr("act"))
                        If Convert.IsDBNull(Rdr("cwt")) = False Then _AWT = Convert.ToInt64(Rdr("cwt"))
                        If Convert.IsDBNull(Rdr("cht")) = False Then _AHT = Convert.ToInt64(Rdr("cht"))
                        If Convert.IsDBNull(Rdr("cct")) = False Then _AHT = Convert.ToInt64(Rdr("cct"))
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_AGENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(ByVal whText As String, ByVal trans As SQLTransaction) As TbRepWtHtAgentCenLinqDB
            ClearData()
            _haveData = False
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then _show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_type")) = False Then _service_type = Rdr("service_type").ToString()
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("awt")) = False Then _awt = Convert.ToInt64(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt64(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("act")) = False Then _AHT = Convert.ToInt64(Rdr("act"))
                        If Convert.IsDBNull(Rdr("cwt")) = False Then _AWT = Convert.ToInt64(Rdr("cwt"))
                        If Convert.IsDBNull(Rdr("cht")) = False Then _AHT = Convert.ToInt64(Rdr("cht"))
                        If Convert.IsDBNull(Rdr("cct")) = False Then _AHT = Convert.ToInt64(Rdr("cct"))
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


        '/// Returns an indication whether the Class Data of TB_REP_WT_HT_AGENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(ByVal whText As String, ByVal trans As SQLTransaction) As TbRepWtHtAgentCenParaDB
            ClearData()
            _haveData = False
            Dim ret As New TbRepWtHtAgentCenParaDB
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then ret.show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("user_id")) = False Then ret.user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then ret.user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then ret.username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then ret.staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_type")) = False Then ret.service_type = Rdr("service_type").ToString()
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then ret.queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("awt")) = False Then ret.AWT = Convert.ToInt64(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then ret.AHT = Convert.ToInt64(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("act")) = False Then ret.ACT = Convert.ToInt64(Rdr("act"))
                        If Convert.IsDBNull(Rdr("cwt")) = False Then ret.CWT = Convert.ToInt64(Rdr("cwt"))
                        If Convert.IsDBNull(Rdr("cht")) = False Then ret.CHT = Convert.ToInt64(Rdr("cht"))
                        If Convert.IsDBNull(Rdr("cct")) = False Then ret.CHT = Convert.ToInt64(Rdr("cct"))
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


        'Get Insert Statement for table TB_REP_WT_HT_AGENT
        Private ReadOnly Property SqlInsert() As String
            Get
                Dim Sql As String = ""
                Sql += "INSERT INTO " & TableName & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SERVICE_DATE, SHOW_DATE, USER_ID, USER_CODE, USERNAME, STAFF_NAME, SERVICE_ID, SERVICE_TYPE, QUEUE_NO, MOBILE_NO, AWT, AHT, ACT, CWT, CHT, CCT)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetString(_SHOW_DATE) & ", "
                sql += DB.SetDouble(_USER_ID) & ", "
                sql += DB.SetString(_USER_CODE) & ", "
                sql += DB.SetString(_USERNAME) & ", "
                sql += DB.SetString(_STAFF_NAME) & ", "
                sql += DB.SetDouble(_SERVICE_ID) & ", "
                sql += DB.SetString(_SERVICE_TYPE) & ", "
                sql += DB.SetString(_QUEUE_NO) & ", "
                Sql += DB.SetString(_MOBILE_NO) & ", "
                Sql += DB.SetDouble(_AWT) & ", "
                Sql += DB.SetDouble(_AHT) & ", "
                Sql += DB.SetDouble(_ACT) & ", "
                Sql += DB.SetDouble(_CWT) & ", "
                Sql += DB.SetDouble(_CHT) & ", "
                Sql += DB.SetDouble(_CCT)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_WT_HT_AGENT
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "SHOW_DATE = " & DB.SetString(_SHOW_DATE) & ", "
                Sql += "USER_ID = " & DB.SetDouble(_USER_ID) & ", "
                Sql += "USER_CODE = " & DB.SetString(_USER_CODE) & ", "
                Sql += "USERNAME = " & DB.SetString(_USERNAME) & ", "
                Sql += "STAFF_NAME = " & DB.SetString(_STAFF_NAME) & ", "
                Sql += "SERVICE_ID = " & DB.SetDouble(_SERVICE_ID) & ", "
                Sql += "SERVICE_TYPE = " & DB.SetString(_SERVICE_TYPE) & ", "
                Sql += "QUEUE_NO = " & DB.SetString(_QUEUE_NO) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "AWT = " & DB.SetDouble(_AWT) & ", "
                Sql += "AHT = " & DB.SetDouble(_AHT) & ", "
                Sql += "ACT = " & DB.SetDouble(_ACT) & ", "
                Sql += "AWT = " & DB.SetDouble(_CWT) & ", "
                Sql += "AHT = " & DB.SetDouble(_CHT) & ", "
                Sql += "ACT = " & DB.SetDouble(_CCT) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_WT_HT_AGENT
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_WT_HT_AGENT
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
