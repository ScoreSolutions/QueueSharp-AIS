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
    'Represents a transaction for TB_COUNTER_QUEUE table ShLinqDB.
    '[Create by  on Febuary, 23 2012]
    Public Class TbCounterQueueShLinqDB
        Public sub TbCounterQueueShLinqDB()

        End Sub 
        ' TB_COUNTER_QUEUE
        Const _tableName As String = "TB_COUNTER_QUEUE"
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
        Dim _QUEUE_NO As  String  = ""
        Dim _CUSTOMER_ID As  String  = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _SEGMENT As  String  = ""
        Dim _CUSTOMERTYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _COUNTER_ID As  System.Nullable(Of Long)  = 0
        Dim _USER_ID As  System.Nullable(Of Long)  = 0
        Dim _SERVICE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ASSIGN_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CALL_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _START_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _STATUS As  System.Nullable(Of Long)  = 0
        Dim _HOLD As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _COMBO_ITEM_ALL As  String  = ""
        Dim _COMBO_ITEM_END As  String  = ""
        Dim _FLAG As  String  = ""
        Dim _MSREPL_TRAN_VERSION As String = ""
        Dim _ROWGUID As String = ""

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
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(20)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
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
        <Column(Storage:="_CUSTOMER_NAME", DbType:="VarChar(250)")>  _
        Public Property CUSTOMER_NAME() As  String 
            Get
                Return _CUSTOMER_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SEGMENT", DbType:="VarChar(100)")>  _
        Public Property SEGMENT() As  String 
            Get
                Return _SEGMENT
            End Get
            Set(ByVal value As  String )
               _SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_ID", DbType:="Int")>  _
        Public Property CUSTOMERTYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _CUSTOMERTYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CUSTOMERTYPE_ID = value
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
        <Column(Storage:="_COUNTER_ID", DbType:="Int")>  _
        Public Property COUNTER_ID() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_USER_ID", DbType:="Int")>  _
        Public Property USER_ID() As  System.Nullable(Of Long) 
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _USER_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime")>  _
        Public Property SERVICE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_ASSIGN_TIME", DbType:="DateTime")>  _
        Public Property ASSIGN_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ASSIGN_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ASSIGN_TIME = value
            End Set
        End Property 
        <Column(Storage:="_CALL_TIME", DbType:="DateTime")>  _
        Public Property CALL_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _CALL_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CALL_TIME = value
            End Set
        End Property 
        <Column(Storage:="_START_TIME", DbType:="DateTime")>  _
        Public Property START_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _START_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _START_TIME = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME", DbType:="DateTime")>  _
        Public Property END_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _END_TIME = value
            End Set
        End Property 
        <Column(Storage:="_STATUS", DbType:="SmallInt")>  _
        Public Property STATUS() As  System.Nullable(Of Long) 
            Get
                Return _STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _STATUS = value
            End Set
        End Property 
        <Column(Storage:="_HOLD", DbType:="DateTime")>  _
        Public Property HOLD() As  System.Nullable(Of DateTime) 
            Get
                Return _HOLD
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _HOLD = value
            End Set
        End Property 
        <Column(Storage:="_COMBO_ITEM_ALL", DbType:="NVarChar(100)")>  _
        Public Property COMBO_ITEM_ALL() As  String 
            Get
                Return _COMBO_ITEM_ALL
            End Get
            Set(ByVal value As  String )
               _COMBO_ITEM_ALL = value
            End Set
        End Property 
        <Column(Storage:="_COMBO_ITEM_END", DbType:="NVarChar(100)")>  _
        Public Property COMBO_ITEM_END() As  String 
            Get
                Return _COMBO_ITEM_END
            End Get
            Set(ByVal value As  String )
               _COMBO_ITEM_END = value
            End Set
        End Property 
        <Column(Storage:="_FLAG", DbType:="VarChar(200)")>  _
        Public Property FLAG() As  String 
            Get
                Return _FLAG
            End Get
            Set(ByVal value As  String )
               _FLAG = value
            End Set
        End Property 
        <Column(Storage:="_MSREPL_TRAN_VERSION", DbType:="UNIQUEIDENTIFIER NOT NULL ",CanBeNull:=false)>  _
        Public Property MSREPL_TRAN_VERSION() As String
            Get
                Return _MSREPL_TRAN_VERSION
            End Get
            Set(ByVal value As String)
               _MSREPL_TRAN_VERSION = value
            End Set
        End Property 
        <Column(Storage:="_ROWGUID", DbType:="UNIQUEIDENTIFIER NOT NULL ",CanBeNull:=false)>  _
        Public Property ROWGUID() As String
            Get
                Return _ROWGUID
            End Get
            Set(ByVal value As String)
               _ROWGUID = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _QUEUE_NO = ""
            _CUSTOMER_ID = ""
            _CUSTOMER_NAME = ""
            _SEGMENT = ""
            _CUSTOMERTYPE_ID = 0
            _ITEM_ID = 0
            _COUNTER_ID = 0
            _USER_ID = 0
            _SERVICE_DATE = New DateTime(1,1,1)
            _ASSIGN_TIME = New DateTime(1,1,1)
            _CALL_TIME = New DateTime(1,1,1)
            _START_TIME = New DateTime(1,1,1)
            _END_TIME = New DateTime(1,1,1)
            _STATUS = 0
            _HOLD = New DateTime(1,1,1)
            _COMBO_ITEM_ALL = ""
            _COMBO_ITEM_END = ""
            _FLAG = ""
             _MSREPL_TRAN_VERSION = ""
             _ROWGUID = ""
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


        '/// Returns an indication whether the current data is inserted into TB_COUNTER_QUEUE table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _id = DB.GetNextID("id",tableName, trans)
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_COUNTER_QUEUE table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return doUpdate("id = " & DB.SetDouble(_id) & " ", trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_COUNTER_QUEUE table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_COUNTER_QUEUE table successfully.
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


        '/// Returns an indication whether the record of TB_COUNTER_QUEUE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_COUNTER_QUEUE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Integer, trans As SQLTransaction) As TbCounterQueueShLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_COUNTER_QUEUE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Integer, trans As SQLTransaction) As TbCounterQueueShParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_COUNTER_QUEUE by specified ROWGUID key is retrieved successfully.
        '/// <param name=cROWGUID>The ROWGUID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByROWGUID(cROWGUID As String, trans As SQLTransaction) As Boolean
            Return doChkData("ROWGUID = " & DB.SetString(cROWGUID) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_COUNTER_QUEUE by specified ROWGUID key is retrieved successfully.
        '/// <param name=cROWGUID>The ROWGUID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByROWGUID(cROWGUID As String, cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("ROWGUID = " & DB.SetString(cROWGUID) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_COUNTER_QUEUE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_COUNTER_QUEUE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_COUNTER_QUEUE table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_COUNTER_QUEUE table successfully.
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


        '/// Returns an indication whether the record of TB_COUNTER_QUEUE by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_id")) = False Then _customer_id = Rdr("customer_id").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then _segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("customertype_id")) = False Then _customertype_id = Convert.ToInt32(Rdr("customertype_id"))
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("counter_id")) = False Then _counter_id = Convert.ToInt32(Rdr("counter_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("assign_time")) = False Then _assign_time = Convert.ToDateTime(Rdr("assign_time"))
                        If Convert.IsDBNull(Rdr("call_time")) = False Then _call_time = Convert.ToDateTime(Rdr("call_time"))
                        If Convert.IsDBNull(Rdr("start_time")) = False Then _start_time = Convert.ToDateTime(Rdr("start_time"))
                        If Convert.IsDBNull(Rdr("end_time")) = False Then _end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("status")) = False Then _status = Convert.ToInt16(Rdr("status"))
                        If Convert.IsDBNull(Rdr("hold")) = False Then _hold = Convert.ToDateTime(Rdr("hold"))
                        If Convert.IsDBNull(Rdr("combo_item_all")) = False Then _combo_item_all = Rdr("combo_item_all").ToString()
                        If Convert.IsDBNull(Rdr("combo_item_end")) = False Then _combo_item_end = Rdr("combo_item_end").ToString()
                        If Convert.IsDBNull(Rdr("flag")) = False Then _flag = Rdr("flag").ToString()
                        If Convert.IsDBNull(Rdr("msrepl_tran_version")) = False Then _msrepl_tran_version = Rdr("msrepl_tran_version").ToString()
                        If Convert.IsDBNull(Rdr("rowguid")) = False Then _rowguid = Rdr("rowguid").ToString()
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


        '/// Returns an indication whether the record of TB_COUNTER_QUEUE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbCounterQueueShLinqDB
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
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_id")) = False Then _customer_id = Rdr("customer_id").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then _segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("customertype_id")) = False Then _customertype_id = Convert.ToInt32(Rdr("customertype_id"))
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("counter_id")) = False Then _counter_id = Convert.ToInt32(Rdr("counter_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("assign_time")) = False Then _assign_time = Convert.ToDateTime(Rdr("assign_time"))
                        If Convert.IsDBNull(Rdr("call_time")) = False Then _call_time = Convert.ToDateTime(Rdr("call_time"))
                        If Convert.IsDBNull(Rdr("start_time")) = False Then _start_time = Convert.ToDateTime(Rdr("start_time"))
                        If Convert.IsDBNull(Rdr("end_time")) = False Then _end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("status")) = False Then _status = Convert.ToInt16(Rdr("status"))
                        If Convert.IsDBNull(Rdr("hold")) = False Then _hold = Convert.ToDateTime(Rdr("hold"))
                        If Convert.IsDBNull(Rdr("combo_item_all")) = False Then _combo_item_all = Rdr("combo_item_all").ToString()
                        If Convert.IsDBNull(Rdr("combo_item_end")) = False Then _combo_item_end = Rdr("combo_item_end").ToString()
                        If Convert.IsDBNull(Rdr("flag")) = False Then _flag = Rdr("flag").ToString()
                        If Convert.IsDBNull(Rdr("msrepl_tran_version")) = False Then _msrepl_tran_version = Rdr("msrepl_tran_version").ToString()
                        If Convert.IsDBNull(Rdr("rowguid")) = False Then _rowguid = Rdr("rowguid").ToString()

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


        '/// Returns an indication whether the Class Data of TB_COUNTER_QUEUE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbCounterQueueShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbCounterQueueShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then ret.queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_id")) = False Then ret.customer_id = Rdr("customer_id").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then ret.customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then ret.segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("customertype_id")) = False Then ret.customertype_id = Convert.ToInt32(Rdr("customertype_id"))
                        If Convert.IsDBNull(Rdr("item_id")) = False Then ret.item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("counter_id")) = False Then ret.counter_id = Convert.ToInt32(Rdr("counter_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then ret.user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("assign_time")) = False Then ret.assign_time = Convert.ToDateTime(Rdr("assign_time"))
                        If Convert.IsDBNull(Rdr("call_time")) = False Then ret.call_time = Convert.ToDateTime(Rdr("call_time"))
                        If Convert.IsDBNull(Rdr("start_time")) = False Then ret.start_time = Convert.ToDateTime(Rdr("start_time"))
                        If Convert.IsDBNull(Rdr("end_time")) = False Then ret.end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("status")) = False Then ret.status = Convert.ToInt16(Rdr("status"))
                        If Convert.IsDBNull(Rdr("hold")) = False Then ret.hold = Convert.ToDateTime(Rdr("hold"))
                        If Convert.IsDBNull(Rdr("combo_item_all")) = False Then ret.combo_item_all = Rdr("combo_item_all").ToString()
                        If Convert.IsDBNull(Rdr("combo_item_end")) = False Then ret.combo_item_end = Rdr("combo_item_end").ToString()
                        If Convert.IsDBNull(Rdr("flag")) = False Then ret.flag = Rdr("flag").ToString()
                        If Convert.IsDBNull(Rdr("msrepl_tran_version")) = False Then ret.msrepl_tran_version = Rdr("msrepl_tran_version").ToString()
                        If Convert.IsDBNull(Rdr("rowguid")) = False Then ret.rowguid = Rdr("rowguid").ToString()

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


        'Get Insert Statement for table TB_COUNTER_QUEUE
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, QUEUE_NO, CUSTOMER_ID, CUSTOMER_NAME, SEGMENT, CUSTOMERTYPE_ID, ITEM_ID, COUNTER_ID, USER_ID, SERVICE_DATE, ASSIGN_TIME, CALL_TIME, START_TIME, END_TIME, STATUS, HOLD, COMBO_ITEM_ALL, COMBO_ITEM_END, FLAG, ROWGUID)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_QUEUE_NO) & ", "
                sql += DB.SetString(_CUSTOMER_ID) & ", "
                sql += DB.SetString(_CUSTOMER_NAME) & ", "
                sql += DB.SetString(_SEGMENT) & ", "
                sql += DB.SetDouble(_CUSTOMERTYPE_ID) & ", "
                sql += DB.SetDouble(_ITEM_ID) & ", "
                sql += DB.SetDouble(_COUNTER_ID) & ", "
                sql += DB.SetDouble(_USER_ID) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetDateTime(_ASSIGN_TIME) & ", "
                sql += DB.SetDateTime(_CALL_TIME) & ", "
                sql += DB.SetDateTime(_START_TIME) & ", "
                sql += DB.SetDateTime(_END_TIME) & ", "
                sql += DB.SetDouble(_STATUS) & ", "
                sql += DB.SetDateTime(_HOLD) & ", "
                sql += DB.SetString(_COMBO_ITEM_ALL) & ", "
                sql += DB.SetString(_COMBO_ITEM_END) & ", "
                sql += DB.SetString(_FLAG) & ", "
                sql += DB.SetString(_ROWGUID)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_COUNTER_QUEUE
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "QUEUE_NO = " & DB.SetString(_QUEUE_NO) & ", "
                Sql += "CUSTOMER_ID = " & DB.SetString(_CUSTOMER_ID) & ", "
                Sql += "CUSTOMER_NAME = " & DB.SetString(_CUSTOMER_NAME) & ", "
                Sql += "SEGMENT = " & DB.SetString(_SEGMENT) & ", "
                Sql += "CUSTOMERTYPE_ID = " & DB.SetDouble(_CUSTOMERTYPE_ID) & ", "
                Sql += "ITEM_ID = " & DB.SetDouble(_ITEM_ID) & ", "
                Sql += "COUNTER_ID = " & DB.SetDouble(_COUNTER_ID) & ", "
                Sql += "USER_ID = " & DB.SetDouble(_USER_ID) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "ASSIGN_TIME = " & DB.SetDateTime(_ASSIGN_TIME) & ", "
                Sql += "CALL_TIME = " & DB.SetDateTime(_CALL_TIME) & ", "
                Sql += "START_TIME = " & DB.SetDateTime(_START_TIME) & ", "
                Sql += "END_TIME = " & DB.SetDateTime(_END_TIME) & ", "
                Sql += "STATUS = " & DB.SetDouble(_STATUS) & ", "
                Sql += "HOLD = " & DB.SetDateTime(_HOLD) & ", "
                Sql += "COMBO_ITEM_ALL = " & DB.SetString(_COMBO_ITEM_ALL) & ", "
                Sql += "COMBO_ITEM_END = " & DB.SetString(_COMBO_ITEM_END) & ", "
                Sql += "FLAG = " & DB.SetString(_FLAG) & ", "
                Sql += "ROWGUID = " & DB.SetString(_ROWGUID) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_COUNTER_QUEUE
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_COUNTER_QUEUE
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
