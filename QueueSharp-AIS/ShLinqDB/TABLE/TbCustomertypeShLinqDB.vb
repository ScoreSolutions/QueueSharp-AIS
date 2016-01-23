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
    'Represents a transaction for TB_CUSTOMERTYPE table ShLinqDB.
    '[Create by  on Febuary, 23 2012]
    Public Class TbCustomertypeShLinqDB
        Public sub TbCustomertypeShLinqDB()

        End Sub 
        ' TB_CUSTOMERTYPE
        Const _tableName As String = "TB_CUSTOMERTYPE"
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
        Dim _CUSTOMERTYPE_CODE As  String  = ""
        Dim _CUSTOMERTYPE_NAME As  String  = ""
        Dim _TXT_QUEUE As  System.Nullable(Of Char)  = ""
        Dim _MIN_QUEUE As  String  = ""
        Dim _MAX_QUEUE As  String  = ""
        Dim _PRIORITY_VALUE As  System.Nullable(Of Long)  = 0
        Dim _COLOR As  System.Nullable(Of Long)  = 0
        Dim _APP As  System.Nullable(Of Long)  = 0
        Dim _DEF As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
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
        <Column(Storage:="_CUSTOMERTYPE_CODE", DbType:="VarChar(20)")>  _
        Public Property CUSTOMERTYPE_CODE() As  String 
            Get
                Return _CUSTOMERTYPE_CODE
            End Get
            Set(ByVal value As  String )
               _CUSTOMERTYPE_CODE = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_NAME", DbType:="VarChar(100)")>  _
        Public Property CUSTOMERTYPE_NAME() As  String 
            Get
                Return _CUSTOMERTYPE_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMERTYPE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_TXT_QUEUE", DbType:="Char(1)")>  _
        Public Property TXT_QUEUE() As  System.Nullable(Of Char) 
            Get
                Return _TXT_QUEUE
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _TXT_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_MIN_QUEUE", DbType:="VarChar(10)")>  _
        Public Property MIN_QUEUE() As  String 
            Get
                Return _MIN_QUEUE
            End Get
            Set(ByVal value As  String )
               _MIN_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_MAX_QUEUE", DbType:="VarChar(10)")>  _
        Public Property MAX_QUEUE() As  String 
            Get
                Return _MAX_QUEUE
            End Get
            Set(ByVal value As  String )
               _MAX_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_PRIORITY_VALUE", DbType:="SmallInt")>  _
        Public Property PRIORITY_VALUE() As  System.Nullable(Of Long) 
            Get
                Return _PRIORITY_VALUE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _PRIORITY_VALUE = value
            End Set
        End Property 
        <Column(Storage:="_COLOR", DbType:="Int")>  _
        Public Property COLOR() As  System.Nullable(Of Long) 
            Get
                Return _COLOR
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COLOR = value
            End Set
        End Property 
        <Column(Storage:="_APP", DbType:="SmallInt")>  _
        Public Property APP() As  System.Nullable(Of Long) 
            Get
                Return _APP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _APP = value
            End Set
        End Property 
        <Column(Storage:="_DEF", DbType:="SmallInt")>  _
        Public Property DEF() As  System.Nullable(Of Long) 
            Get
                Return _DEF
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _DEF = value
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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20)")>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(20)")>  _
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
            _CUSTOMERTYPE_CODE = ""
            _CUSTOMERTYPE_NAME = ""
            _TXT_QUEUE = ""
            _MIN_QUEUE = ""
            _MAX_QUEUE = ""
            _PRIORITY_VALUE = 0
            _COLOR = 0
            _APP = 0
            _DEF = 0
            _ACTIVE_STATUS = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
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


        '/// Returns an indication whether the current data is inserted into TB_CUSTOMERTYPE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMERTYPE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMERTYPE table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMERTYPE table successfully.
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


        '/// Returns an indication whether the record of TB_CUSTOMERTYPE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_CUSTOMERTYPE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Integer, trans As SQLTransaction) As TbCustomertypeShLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_CUSTOMERTYPE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Integer, trans As SQLTransaction) As TbCustomertypeShParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMERTYPE by specified ROWGUID key is retrieved successfully.
        '/// <param name=cROWGUID>The ROWGUID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByROWGUID(cROWGUID As String, trans As SQLTransaction) As Boolean
            Return doChkData("ROWGUID = " & DB.SetString(cROWGUID) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_CUSTOMERTYPE by specified ROWGUID key is retrieved successfully.
        '/// <param name=cROWGUID>The ROWGUID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByROWGUID(cROWGUID As String, cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("ROWGUID = " & DB.SetString(cROWGUID) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMERTYPE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_CUSTOMERTYPE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMERTYPE table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMERTYPE table successfully.
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


        '/// Returns an indication whether the record of TB_CUSTOMERTYPE by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("customertype_code")) = False Then _customertype_code = Rdr("customertype_code").ToString()
                        If Convert.IsDBNull(Rdr("customertype_name")) = False Then _customertype_name = Rdr("customertype_name").ToString()
                        If Convert.IsDBNull(Rdr("txt_queue")) = False Then _txt_queue = Rdr("txt_queue").ToString()
                        If Convert.IsDBNull(Rdr("min_queue")) = False Then _min_queue = Rdr("min_queue").ToString()
                        If Convert.IsDBNull(Rdr("max_queue")) = False Then _max_queue = Rdr("max_queue").ToString()
                        If Convert.IsDBNull(Rdr("priority_value")) = False Then _priority_value = Convert.ToInt16(Rdr("priority_value"))
                        If Convert.IsDBNull(Rdr("color")) = False Then _color = Convert.ToInt32(Rdr("color"))
                        If Convert.IsDBNull(Rdr("app")) = False Then _app = Convert.ToInt16(Rdr("app"))
                        If Convert.IsDBNull(Rdr("def")) = False Then _def = Convert.ToInt16(Rdr("def"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
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


        '/// Returns an indication whether the record of TB_CUSTOMERTYPE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbCustomertypeShLinqDB
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
                        If Convert.IsDBNull(Rdr("customertype_code")) = False Then _customertype_code = Rdr("customertype_code").ToString()
                        If Convert.IsDBNull(Rdr("customertype_name")) = False Then _customertype_name = Rdr("customertype_name").ToString()
                        If Convert.IsDBNull(Rdr("txt_queue")) = False Then _txt_queue = Rdr("txt_queue").ToString()
                        If Convert.IsDBNull(Rdr("min_queue")) = False Then _min_queue = Rdr("min_queue").ToString()
                        If Convert.IsDBNull(Rdr("max_queue")) = False Then _max_queue = Rdr("max_queue").ToString()
                        If Convert.IsDBNull(Rdr("priority_value")) = False Then _priority_value = Convert.ToInt16(Rdr("priority_value"))
                        If Convert.IsDBNull(Rdr("color")) = False Then _color = Convert.ToInt32(Rdr("color"))
                        If Convert.IsDBNull(Rdr("app")) = False Then _app = Convert.ToInt16(Rdr("app"))
                        If Convert.IsDBNull(Rdr("def")) = False Then _def = Convert.ToInt16(Rdr("def"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
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


        '/// Returns an indication whether the Class Data of TB_CUSTOMERTYPE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbCustomertypeShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbCustomertypeShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("customertype_code")) = False Then ret.customertype_code = Rdr("customertype_code").ToString()
                        If Convert.IsDBNull(Rdr("customertype_name")) = False Then ret.customertype_name = Rdr("customertype_name").ToString()
                        If Convert.IsDBNull(Rdr("txt_queue")) = False Then ret.txt_queue = Rdr("txt_queue").ToString()
                        If Convert.IsDBNull(Rdr("min_queue")) = False Then ret.min_queue = Rdr("min_queue").ToString()
                        If Convert.IsDBNull(Rdr("max_queue")) = False Then ret.max_queue = Rdr("max_queue").ToString()
                        If Convert.IsDBNull(Rdr("priority_value")) = False Then ret.priority_value = Convert.ToInt16(Rdr("priority_value"))
                        If Convert.IsDBNull(Rdr("color")) = False Then ret.color = Convert.ToInt32(Rdr("color"))
                        If Convert.IsDBNull(Rdr("app")) = False Then ret.app = Convert.ToInt16(Rdr("app"))
                        If Convert.IsDBNull(Rdr("def")) = False Then ret.def = Convert.ToInt16(Rdr("def"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then ret.active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
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


        'Get Insert Statement for table TB_CUSTOMERTYPE
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CUSTOMERTYPE_CODE, CUSTOMERTYPE_NAME, TXT_QUEUE, MIN_QUEUE, MAX_QUEUE, PRIORITY_VALUE, COLOR, APP, DEF, ACTIVE_STATUS, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, ROWGUID)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CUSTOMERTYPE_CODE) & ", "
                sql += DB.SetString(_CUSTOMERTYPE_NAME) & ", "
                sql += DB.SetString(_TXT_QUEUE) & ", "
                sql += DB.SetString(_MIN_QUEUE) & ", "
                sql += DB.SetString(_MAX_QUEUE) & ", "
                sql += DB.SetDouble(_PRIORITY_VALUE) & ", "
                sql += DB.SetDouble(_COLOR) & ", "
                sql += DB.SetDouble(_APP) & ", "
                sql += DB.SetDouble(_DEF) & ", "
                sql += DB.SetDouble(_ACTIVE_STATUS) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_ROWGUID)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_CUSTOMERTYPE
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "CUSTOMERTYPE_CODE = " & DB.SetString(_CUSTOMERTYPE_CODE) & ", "
                Sql += "CUSTOMERTYPE_NAME = " & DB.SetString(_CUSTOMERTYPE_NAME) & ", "
                Sql += "TXT_QUEUE = " & DB.SetString(_TXT_QUEUE) & ", "
                Sql += "MIN_QUEUE = " & DB.SetString(_MIN_QUEUE) & ", "
                Sql += "MAX_QUEUE = " & DB.SetString(_MAX_QUEUE) & ", "
                Sql += "PRIORITY_VALUE = " & DB.SetDouble(_PRIORITY_VALUE) & ", "
                Sql += "COLOR = " & DB.SetDouble(_COLOR) & ", "
                Sql += "APP = " & DB.SetDouble(_APP) & ", "
                Sql += "DEF = " & DB.SetDouble(_DEF) & ", "
                Sql += "ACTIVE_STATUS = " & DB.SetDouble(_ACTIVE_STATUS) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "ROWGUID = " & DB.SetString(_ROWGUID) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_CUSTOMERTYPE
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_CUSTOMERTYPE
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
