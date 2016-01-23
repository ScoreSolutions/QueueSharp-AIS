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
    'Represents a transaction for TB_ITEM table ShLinqDB.
    '[Create by  on August, 15 2014]
    Public Class TbItemShLinqDB
        Public sub TbItemShLinqDB()

        End Sub 
        ' TB_ITEM
        Const _tableName As String = "TB_ITEM"
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
        Dim _ITEM_CODE As  String  = ""
        Dim _ITEM_NAME As  String  = ""
        Dim _ITEM_NAME_TH As  String  = ""
        Dim _ITEM_TIME As  System.Nullable(Of Long)  = 0
        Dim _ITEM_WAIT As  System.Nullable(Of Long)  = 0
        Dim _ITEM_ORDER As  System.Nullable(Of Long)  = 0
        Dim _TXT_QUEUE As  System.Nullable(Of Char)  = ""
        Dim _COLOR As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _Q_TYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _APP_MIN_QUEUE As  String  = ""
        Dim _APP_MAX_QUEUE As  String  = ""
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ITEM_URL As  String  = ""
        Dim _MASTER_ITEMID As Long = 0
        Dim _TB_ITEM_GROUP_ID As  System.Nullable(Of Long)  = 0
        Dim _COUNT_QUEUE_BY_SEGMENT As Char = "0"
        Dim _DISABLE_TICKET_WAITING_TIME As Char = "0"
        Dim _BRAND_NAME As Char = "N"

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
        <Column(Storage:="_ITEM_CODE", DbType:="VarChar(20)")>  _
        Public Property ITEM_CODE() As  String 
            Get
                Return _ITEM_CODE
            End Get
            Set(ByVal value As  String )
               _ITEM_CODE = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME", DbType:="VarChar(50)")>  _
        Public Property ITEM_NAME() As  String 
            Get
                Return _ITEM_NAME
            End Get
            Set(ByVal value As  String )
               _ITEM_NAME = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME_TH", DbType:="VarChar(50)")>  _
        Public Property ITEM_NAME_TH() As  String 
            Get
                Return _ITEM_NAME_TH
            End Get
            Set(ByVal value As  String )
               _ITEM_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_TIME", DbType:="Int")>  _
        Public Property ITEM_TIME() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_TIME = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_WAIT", DbType:="Int")>  _
        Public Property ITEM_WAIT() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_WAIT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_WAIT = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ORDER", DbType:="SmallInt")>  _
        Public Property ITEM_ORDER() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ORDER
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ORDER = value
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
        <Column(Storage:="_COLOR", DbType:="Int")>  _
        Public Property COLOR() As  System.Nullable(Of Long) 
            Get
                Return _COLOR
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COLOR = value
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
        <Column(Storage:="_Q_TYPE_ID", DbType:="SmallInt")>  _
        Public Property Q_TYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _Q_TYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _Q_TYPE_ID = value
            End Set
        End Property 
        <Column(Storage:="_APP_MIN_QUEUE", DbType:="VarChar(10)")>  _
        Public Property APP_MIN_QUEUE() As  String 
            Get
                Return _APP_MIN_QUEUE
            End Get
            Set(ByVal value As  String )
               _APP_MIN_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_APP_MAX_QUEUE", DbType:="VarChar(10)")>  _
        Public Property APP_MAX_QUEUE() As  String 
            Get
                Return _APP_MAX_QUEUE
            End Get
            Set(ByVal value As  String )
               _APP_MAX_QUEUE = value
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
        <Column(Storage:="_ITEM_URL", DbType:="VarChar(500)")>  _
        Public Property ITEM_URL() As  String 
            Get
                Return _ITEM_URL
            End Get
            Set(ByVal value As  String )
               _ITEM_URL = value
            End Set
        End Property 
        <Column(Storage:="_MASTER_ITEMID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASTER_ITEMID() As Long
            Get
                Return _MASTER_ITEMID
            End Get
            Set(ByVal value As Long)
               _MASTER_ITEMID = value
            End Set
        End Property 
        <Column(Storage:="_TB_ITEM_GROUP_ID", DbType:="Int")>  _
        Public Property TB_ITEM_GROUP_ID() As  System.Nullable(Of Long) 
            Get
                Return _TB_ITEM_GROUP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TB_ITEM_GROUP_ID = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_QUEUE_BY_SEGMENT", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_QUEUE_BY_SEGMENT() As Char
            Get
                Return _COUNT_QUEUE_BY_SEGMENT
            End Get
            Set(ByVal value As Char)
               _COUNT_QUEUE_BY_SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_DISABLE_TICKET_WAITING_TIME", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property DISABLE_TICKET_WAITING_TIME() As Char
            Get
                Return _DISABLE_TICKET_WAITING_TIME
            End Get
            Set(ByVal value As Char)
               _DISABLE_TICKET_WAITING_TIME = value
            End Set
        End Property 
        <Column(Storage:="_BRAND_NAME", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property BRAND_NAME() As Char
            Get
                Return _BRAND_NAME
            End Get
            Set(ByVal value As Char)
               _BRAND_NAME = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _ITEM_CODE = ""
            _ITEM_NAME = ""
            _ITEM_NAME_TH = ""
            _ITEM_TIME = 0
            _ITEM_WAIT = 0
            _ITEM_ORDER = 0
            _TXT_QUEUE = ""
            _COLOR = 0
            _ACTIVE_STATUS = 0
            _Q_TYPE_ID = 0
            _APP_MIN_QUEUE = ""
            _APP_MAX_QUEUE = ""
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _ITEM_URL = ""
            _MASTER_ITEMID = 0
            _TB_ITEM_GROUP_ID = 0
            _COUNT_QUEUE_BY_SEGMENT = ""
            _DISABLE_TICKET_WAITING_TIME = ""
            _BRAND_NAME = ""
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


        '/// Returns an indication whether the current data is inserted into TB_ITEM table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_ITEM table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_ITEM table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_ITEM table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cID As Integer, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return doDelete("ID = " & DB.SetDouble(cID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the record of TB_ITEM by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_ITEM by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Integer, trans As SQLTransaction) As TbItemShLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_ITEM by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Integer, trans As SQLTransaction) As TbItemShParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_ITEM by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_ITEM table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_ITEM table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_ITEM table successfully.
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


        '/// Returns an indication whether the record of TB_ITEM by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("item_code")) = False Then _item_code = Rdr("item_code").ToString()
                        If Convert.IsDBNull(Rdr("item_name")) = False Then _item_name = Rdr("item_name").ToString()
                        If Convert.IsDBNull(Rdr("item_name_th")) = False Then _item_name_th = Rdr("item_name_th").ToString()
                        If Convert.IsDBNull(Rdr("item_time")) = False Then _item_time = Convert.ToInt32(Rdr("item_time"))
                        If Convert.IsDBNull(Rdr("item_wait")) = False Then _item_wait = Convert.ToInt32(Rdr("item_wait"))
                        If Convert.IsDBNull(Rdr("item_order")) = False Then _item_order = Convert.ToInt16(Rdr("item_order"))
                        If Convert.IsDBNull(Rdr("txt_queue")) = False Then _txt_queue = Rdr("txt_queue").ToString()
                        If Convert.IsDBNull(Rdr("color")) = False Then _color = Convert.ToInt32(Rdr("color"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("q_type_id")) = False Then _q_type_id = Convert.ToInt16(Rdr("q_type_id"))
                        If Convert.IsDBNull(Rdr("app_min_queue")) = False Then _app_min_queue = Rdr("app_min_queue").ToString()
                        If Convert.IsDBNull(Rdr("app_max_queue")) = False Then _app_max_queue = Rdr("app_max_queue").ToString()
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("item_url")) = False Then _item_url = Rdr("item_url").ToString()
                        If Convert.IsDBNull(Rdr("master_itemid")) = False Then _master_itemid = Convert.ToInt32(Rdr("master_itemid"))
                        If Convert.IsDBNull(Rdr("tb_item_group_id")) = False Then _tb_item_group_id = Convert.ToInt32(Rdr("tb_item_group_id"))
                        If Convert.IsDBNull(Rdr("count_queue_by_segment")) = False Then _count_queue_by_segment = Rdr("count_queue_by_segment").ToString()
                        If Convert.IsDBNull(Rdr("disable_ticket_waiting_time")) = False Then _disable_ticket_waiting_time = Rdr("disable_ticket_waiting_time").ToString()
                        If Convert.IsDBNull(Rdr("brand_name")) = False Then _brand_name = Rdr("brand_name").ToString()
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


        '/// Returns an indication whether the record of TB_ITEM by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbItemShLinqDB
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
                        If Convert.IsDBNull(Rdr("item_code")) = False Then _item_code = Rdr("item_code").ToString()
                        If Convert.IsDBNull(Rdr("item_name")) = False Then _item_name = Rdr("item_name").ToString()
                        If Convert.IsDBNull(Rdr("item_name_th")) = False Then _item_name_th = Rdr("item_name_th").ToString()
                        If Convert.IsDBNull(Rdr("item_time")) = False Then _item_time = Convert.ToInt32(Rdr("item_time"))
                        If Convert.IsDBNull(Rdr("item_wait")) = False Then _item_wait = Convert.ToInt32(Rdr("item_wait"))
                        If Convert.IsDBNull(Rdr("item_order")) = False Then _item_order = Convert.ToInt16(Rdr("item_order"))
                        If Convert.IsDBNull(Rdr("txt_queue")) = False Then _txt_queue = Rdr("txt_queue").ToString()
                        If Convert.IsDBNull(Rdr("color")) = False Then _color = Convert.ToInt32(Rdr("color"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("q_type_id")) = False Then _q_type_id = Convert.ToInt16(Rdr("q_type_id"))
                        If Convert.IsDBNull(Rdr("app_min_queue")) = False Then _app_min_queue = Rdr("app_min_queue").ToString()
                        If Convert.IsDBNull(Rdr("app_max_queue")) = False Then _app_max_queue = Rdr("app_max_queue").ToString()
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("item_url")) = False Then _item_url = Rdr("item_url").ToString()
                        If Convert.IsDBNull(Rdr("master_itemid")) = False Then _master_itemid = Convert.ToInt32(Rdr("master_itemid"))
                        If Convert.IsDBNull(Rdr("tb_item_group_id")) = False Then _tb_item_group_id = Convert.ToInt32(Rdr("tb_item_group_id"))
                        If Convert.IsDBNull(Rdr("count_queue_by_segment")) = False Then _count_queue_by_segment = Rdr("count_queue_by_segment").ToString()
                        If Convert.IsDBNull(Rdr("disable_ticket_waiting_time")) = False Then _disable_ticket_waiting_time = Rdr("disable_ticket_waiting_time").ToString()
                        If Convert.IsDBNull(Rdr("brand_name")) = False Then _brand_name = Rdr("brand_name").ToString()
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


        '/// Returns an indication whether the Class Data of TB_ITEM by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbItemShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbItemShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("item_code")) = False Then ret.item_code = Rdr("item_code").ToString()
                        If Convert.IsDBNull(Rdr("item_name")) = False Then ret.item_name = Rdr("item_name").ToString()
                        If Convert.IsDBNull(Rdr("item_name_th")) = False Then ret.item_name_th = Rdr("item_name_th").ToString()
                        If Convert.IsDBNull(Rdr("item_time")) = False Then ret.item_time = Convert.ToInt32(Rdr("item_time"))
                        If Convert.IsDBNull(Rdr("item_wait")) = False Then ret.item_wait = Convert.ToInt32(Rdr("item_wait"))
                        If Convert.IsDBNull(Rdr("item_order")) = False Then ret.item_order = Convert.ToInt16(Rdr("item_order"))
                        If Convert.IsDBNull(Rdr("txt_queue")) = False Then ret.txt_queue = Rdr("txt_queue").ToString()
                        If Convert.IsDBNull(Rdr("color")) = False Then ret.color = Convert.ToInt32(Rdr("color"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then ret.active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("q_type_id")) = False Then ret.q_type_id = Convert.ToInt16(Rdr("q_type_id"))
                        If Convert.IsDBNull(Rdr("app_min_queue")) = False Then ret.app_min_queue = Rdr("app_min_queue").ToString()
                        If Convert.IsDBNull(Rdr("app_max_queue")) = False Then ret.app_max_queue = Rdr("app_max_queue").ToString()
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("item_url")) = False Then ret.item_url = Rdr("item_url").ToString()
                        If Convert.IsDBNull(Rdr("master_itemid")) = False Then ret.master_itemid = Convert.ToInt32(Rdr("master_itemid"))
                        If Convert.IsDBNull(Rdr("tb_item_group_id")) = False Then ret.tb_item_group_id = Convert.ToInt32(Rdr("tb_item_group_id"))
                        If Convert.IsDBNull(Rdr("count_queue_by_segment")) = False Then ret.count_queue_by_segment = Rdr("count_queue_by_segment").ToString()
                        If Convert.IsDBNull(Rdr("disable_ticket_waiting_time")) = False Then ret.disable_ticket_waiting_time = Rdr("disable_ticket_waiting_time").ToString()
                        If Convert.IsDBNull(Rdr("brand_name")) = False Then ret.brand_name = Rdr("brand_name").ToString()

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


        'Get Insert Statement for table TB_ITEM
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, ITEM_CODE, ITEM_NAME, ITEM_NAME_TH, ITEM_TIME, ITEM_WAIT, ITEM_ORDER, TXT_QUEUE, COLOR, ACTIVE_STATUS, Q_TYPE_ID, APP_MIN_QUEUE, APP_MAX_QUEUE, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, ITEM_URL, MASTER_ITEMID, TB_ITEM_GROUP_ID, COUNT_QUEUE_BY_SEGMENT, DISABLE_TICKET_WAITING_TIME, BRAND_NAME)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_ITEM_CODE) & ", "
                sql += DB.SetString(_ITEM_NAME) & ", "
                sql += DB.SetString(_ITEM_NAME_TH) & ", "
                sql += DB.SetDouble(_ITEM_TIME) & ", "
                sql += DB.SetDouble(_ITEM_WAIT) & ", "
                sql += DB.SetDouble(_ITEM_ORDER) & ", "
                sql += DB.SetString(_TXT_QUEUE) & ", "
                sql += DB.SetDouble(_COLOR) & ", "
                sql += DB.SetDouble(_ACTIVE_STATUS) & ", "
                sql += DB.SetDouble(_Q_TYPE_ID) & ", "
                sql += DB.SetString(_APP_MIN_QUEUE) & ", "
                sql += DB.SetString(_APP_MAX_QUEUE) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_ITEM_URL) & ", "
                sql += DB.SetDouble(_MASTER_ITEMID) & ", "
                sql += DB.SetDouble(_TB_ITEM_GROUP_ID) & ", "
                sql += DB.SetString(_COUNT_QUEUE_BY_SEGMENT) & ", "
                sql += DB.SetString(_DISABLE_TICKET_WAITING_TIME) & ", "
                sql += DB.SetString(_BRAND_NAME)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_ITEM
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ITEM_CODE = " & DB.SetString(_ITEM_CODE) & ", "
                Sql += "ITEM_NAME = " & DB.SetString(_ITEM_NAME) & ", "
                Sql += "ITEM_NAME_TH = " & DB.SetString(_ITEM_NAME_TH) & ", "
                Sql += "ITEM_TIME = " & DB.SetDouble(_ITEM_TIME) & ", "
                Sql += "ITEM_WAIT = " & DB.SetDouble(_ITEM_WAIT) & ", "
                Sql += "ITEM_ORDER = " & DB.SetDouble(_ITEM_ORDER) & ", "
                Sql += "TXT_QUEUE = " & DB.SetString(_TXT_QUEUE) & ", "
                Sql += "COLOR = " & DB.SetDouble(_COLOR) & ", "
                Sql += "ACTIVE_STATUS = " & DB.SetDouble(_ACTIVE_STATUS) & ", "
                Sql += "Q_TYPE_ID = " & DB.SetDouble(_Q_TYPE_ID) & ", "
                Sql += "APP_MIN_QUEUE = " & DB.SetString(_APP_MIN_QUEUE) & ", "
                Sql += "APP_MAX_QUEUE = " & DB.SetString(_APP_MAX_QUEUE) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "ITEM_URL = " & DB.SetString(_ITEM_URL) & ", "
                Sql += "MASTER_ITEMID = " & DB.SetDouble(_MASTER_ITEMID) & ", "
                Sql += "TB_ITEM_GROUP_ID = " & DB.SetDouble(_TB_ITEM_GROUP_ID) & ", "
                Sql += "COUNT_QUEUE_BY_SEGMENT = " & DB.SetString(_COUNT_QUEUE_BY_SEGMENT) & ", "
                Sql += "DISABLE_TICKET_WAITING_TIME = " & DB.SetString(_DISABLE_TICKET_WAITING_TIME) & ", "
                Sql += "BRAND_NAME = " & DB.SetString(_BRAND_NAME) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_ITEM
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_ITEM
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & TableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
