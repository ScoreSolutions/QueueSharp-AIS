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
    'Represents a transaction for TB_FILTER_TEMP_DATA table CenLinqDB.
    '[Create by  on October, 26 2015]
    Public Class TbFilterTempDataCenLinqDB
        Public sub TbFilterTempDataCenLinqDB()

        End Sub 
        ' TB_FILTER_TEMP_DATA
        Const _tableName As String = "TB_FILTER_TEMP_DATA"
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
        Dim _TB_SHOP_ID As Long = 0
        Dim _TB_FILTER_ID As Long = 0
        Dim _QUEUE_NO As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _END_TIME As DateTime = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _MASTER_ITEMID As Long = 0
        Dim _ITEM_NAME As String = ""
        Dim _CATEGORY As  String  = ""
        Dim _CONTACT_CLASS As  String  = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _SEGMENT As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _USERNAME As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _SH_TEMPLATE_CODE As  String  = ""

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(100)")>  _
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
        <Column(Storage:="_TB_SHOP_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_SHOP_ID() As Long
            Get
                Return _TB_SHOP_ID
            End Get
            Set(ByVal value As Long)
               _TB_SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_TB_FILTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_FILTER_ID() As Long
            Get
                Return _TB_FILTER_ID
            End Get
            Set(ByVal value As Long)
               _TB_FILTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property QUEUE_NO() As String
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As String)
               _QUEUE_NO = value
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
        <Column(Storage:="_END_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property END_TIME() As DateTime
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As DateTime)
               _END_TIME = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_NAME", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_NAME() As  String 
            Get
                Return _CUSTOMER_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_MASTER_ITEMID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property MASTER_ITEMID() As Long
            Get
                Return _MASTER_ITEMID
            End Get
            Set(ByVal value As Long)
               _MASTER_ITEMID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME() As String
            Get
                Return _ITEM_NAME
            End Get
            Set(ByVal value As String)
               _ITEM_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CATEGORY", DbType:="VarChar(100)")>  _
        Public Property CATEGORY() As  String 
            Get
                Return _CATEGORY
            End Get
            Set(ByVal value As  String )
               _CATEGORY = value
            End Set
        End Property 
        <Column(Storage:="_CONTACT_CLASS", DbType:="VarChar(100)")>  _
        Public Property CONTACT_CLASS() As  String 
            Get
                Return _CONTACT_CLASS
            End Get
            Set(ByVal value As  String )
               _CONTACT_CLASS = value
            End Set
        End Property 
        <Column(Storage:="_PREFER_LANG", DbType:="VarChar(100)")>  _
        Public Property PREFER_LANG() As  String 
            Get
                Return _PREFER_LANG
            End Get
            Set(ByVal value As  String )
               _PREFER_LANG = value
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
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(100)")>  _
        Public Property NETWORK_TYPE() As  String 
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As  String )
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_SH_TEMPLATE_CODE", DbType:="VarChar(50)")>  _
        Public Property SH_TEMPLATE_CODE() As  String 
            Get
                Return _SH_TEMPLATE_CODE
            End Get
            Set(ByVal value As  String )
               _SH_TEMPLATE_CODE = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _TB_SHOP_ID = 0
            _TB_FILTER_ID = 0
            _QUEUE_NO = ""
            _SERVICE_DATE = New DateTime(1,1,1)
            _END_TIME = New DateTime(1,1,1)
            _MOBILE_NO = ""
            _CUSTOMER_NAME = ""
            _MASTER_ITEMID = 0
            _ITEM_NAME = ""
            _CATEGORY = ""
            _CONTACT_CLASS = ""
            _PREFER_LANG = ""
            _SEGMENT = ""
            _NETWORK_TYPE = ""
            _USERNAME = ""
            _STAFF_NAME = ""
            _SH_TEMPLATE_CODE = ""
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


        '/// Returns an indication whether the current data is inserted into TB_FILTER_TEMP_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_FILTER_TEMP_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_FILTER_TEMP_DATA table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_FILTER_TEMP_DATA table successfully.
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


        '/// Returns an indication whether the record of TB_FILTER_TEMP_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_FILTER_TEMP_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbFilterTempDataCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_FILTER_TEMP_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbFilterTempDataCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_FILTER_TEMP_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_FILTER_TEMP_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_FILTER_TEMP_DATA table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_FILTER_TEMP_DATA table successfully.
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


        '/// Returns an indication whether the record of TB_FILTER_TEMP_DATA by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("tb_shop_id")) = False Then _tb_shop_id = Convert.ToInt64(Rdr("tb_shop_id"))
                        If Convert.IsDBNull(Rdr("tb_filter_id")) = False Then _tb_filter_id = Convert.ToInt64(Rdr("tb_filter_id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("end_time")) = False Then _end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("master_itemid")) = False Then _master_itemid = Convert.ToInt64(Rdr("master_itemid"))
                        If Convert.IsDBNull(Rdr("item_name")) = False Then _item_name = Rdr("item_name").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then _segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("sh_template_code")) = False Then _sh_template_code = Rdr("sh_template_code").ToString()
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


        '/// Returns an indication whether the record of TB_FILTER_TEMP_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbFilterTempDataCenLinqDB
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
                        If Convert.IsDBNull(Rdr("tb_shop_id")) = False Then _tb_shop_id = Convert.ToInt64(Rdr("tb_shop_id"))
                        If Convert.IsDBNull(Rdr("tb_filter_id")) = False Then _tb_filter_id = Convert.ToInt64(Rdr("tb_filter_id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("end_time")) = False Then _end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("master_itemid")) = False Then _master_itemid = Convert.ToInt64(Rdr("master_itemid"))
                        If Convert.IsDBNull(Rdr("item_name")) = False Then _item_name = Rdr("item_name").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then _segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then _staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("sh_template_code")) = False Then _sh_template_code = Rdr("sh_template_code").ToString()
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


        '/// Returns an indication whether the Class Data of TB_FILTER_TEMP_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbFilterTempDataCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbFilterTempDataCenParaDB
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
                        If Convert.IsDBNull(Rdr("tb_shop_id")) = False Then ret.tb_shop_id = Convert.ToInt64(Rdr("tb_shop_id"))
                        If Convert.IsDBNull(Rdr("tb_filter_id")) = False Then ret.tb_filter_id = Convert.ToInt64(Rdr("tb_filter_id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then ret.queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("end_time")) = False Then ret.end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then ret.customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("master_itemid")) = False Then ret.master_itemid = Convert.ToInt64(Rdr("master_itemid"))
                        If Convert.IsDBNull(Rdr("item_name")) = False Then ret.item_name = Rdr("item_name").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then ret.category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then ret.contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then ret.prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment")) = False Then ret.segment = Rdr("segment").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then ret.username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("staff_name")) = False Then ret.staff_name = Rdr("staff_name").ToString()
                        If Convert.IsDBNull(Rdr("sh_template_code")) = False Then ret.sh_template_code = Rdr("sh_template_code").ToString()

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


        'Get Insert Statement for table TB_FILTER_TEMP_DATA
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TB_SHOP_ID, TB_FILTER_ID, QUEUE_NO, SERVICE_DATE, END_TIME, MOBILE_NO, CUSTOMER_NAME, MASTER_ITEMID, ITEM_NAME, CATEGORY, CONTACT_CLASS, PREFER_LANG, SEGMENT, NETWORK_TYPE, USERNAME, STAFF_NAME, SH_TEMPLATE_CODE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_TB_SHOP_ID) & ", "
                sql += DB.SetDouble(_TB_FILTER_ID) & ", "
                sql += DB.SetString(_QUEUE_NO) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetDateTime(_END_TIME) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetString(_CUSTOMER_NAME) & ", "
                sql += DB.SetDouble(_MASTER_ITEMID) & ", "
                sql += DB.SetString(_ITEM_NAME) & ", "
                sql += DB.SetString(_CATEGORY) & ", "
                sql += DB.SetString(_CONTACT_CLASS) & ", "
                sql += DB.SetString(_PREFER_LANG) & ", "
                sql += DB.SetString(_SEGMENT) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_USERNAME) & ", "
                sql += DB.SetString(_STAFF_NAME) & ", "
                sql += DB.SetString(_SH_TEMPLATE_CODE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_FILTER_TEMP_DATA
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "TB_SHOP_ID = " & DB.SetDouble(_TB_SHOP_ID) & ", "
                Sql += "TB_FILTER_ID = " & DB.SetDouble(_TB_FILTER_ID) & ", "
                Sql += "QUEUE_NO = " & DB.SetString(_QUEUE_NO) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "END_TIME = " & DB.SetDateTime(_END_TIME) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "CUSTOMER_NAME = " & DB.SetString(_CUSTOMER_NAME) & ", "
                Sql += "MASTER_ITEMID = " & DB.SetDouble(_MASTER_ITEMID) & ", "
                Sql += "ITEM_NAME = " & DB.SetString(_ITEM_NAME) & ", "
                Sql += "CATEGORY = " & DB.SetString(_CATEGORY) & ", "
                Sql += "CONTACT_CLASS = " & DB.SetString(_CONTACT_CLASS) & ", "
                Sql += "PREFER_LANG = " & DB.SetString(_PREFER_LANG) & ", "
                Sql += "SEGMENT = " & DB.SetString(_SEGMENT) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "USERNAME = " & DB.SetString(_USERNAME) & ", "
                Sql += "STAFF_NAME = " & DB.SetString(_STAFF_NAME) & ", "
                Sql += "SH_TEMPLATE_CODE = " & DB.SetString(_SH_TEMPLATE_CODE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_FILTER_TEMP_DATA
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_FILTER_TEMP_DATA
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TB_SHOP_ID, TB_FILTER_ID, QUEUE_NO, SERVICE_DATE, END_TIME, MOBILE_NO, CUSTOMER_NAME, MASTER_ITEMID, ITEM_NAME, CATEGORY, CONTACT_CLASS, PREFER_LANG, SEGMENT, NETWORK_TYPE, USERNAME, STAFF_NAME, SH_TEMPLATE_CODE FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
