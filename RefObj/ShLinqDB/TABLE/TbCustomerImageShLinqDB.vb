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
    'Represents a transaction for TB_CUSTOMER_IMAGE table ShLinqDB.
    '[Create by  on September, 16 2013]
    Public Class TbCustomerImageShLinqDB
        Public sub TbCustomerImageShLinqDB()

        End Sub 
        ' TB_CUSTOMER_IMAGE
        Const _tableName As String = "TB_CUSTOMER_IMAGE"
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
        Dim _CAPTURE_TIME As DateTime = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _ASSIGN_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _QUEUE_NO As  String  = ""
        Dim _CONTACTID As  String  = ""
        Dim _CUSTOMER_FIRST_NAME As  String  = ""
        Dim _CUSTOMER_LAST_NAME As  String  = ""
        Dim _CUSTOMER_STATUS As  String  = ""
        Dim _LAST_CAPTURE_DATE As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _ACCOUNT_NO As  String  = ""
        Dim _WEBSERVICE_HOST_IP As String = ""
        Dim _APPOINTMENT_JOB_ID As Long = 0

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
        <Column(Storage:="_CAPTURE_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property CAPTURE_TIME() As DateTime
            Get
                Return _CAPTURE_TIME
            End Get
            Set(ByVal value As DateTime)
               _CAPTURE_TIME = value
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
        <Column(Storage:="_ASSIGN_TIME", DbType:="DateTime")>  _
        Public Property ASSIGN_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ASSIGN_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ASSIGN_TIME = value
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
        <Column(Storage:="_CONTACTID", DbType:="VarChar(100)")>  _
        Public Property CONTACTID() As  String 
            Get
                Return _CONTACTID
            End Get
            Set(ByVal value As  String )
               _CONTACTID = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_FIRST_NAME", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_FIRST_NAME() As  String 
            Get
                Return _CUSTOMER_FIRST_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_FIRST_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_LAST_NAME", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_LAST_NAME() As  String 
            Get
                Return _CUSTOMER_LAST_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_LAST_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_STATUS", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_STATUS() As  String 
            Get
                Return _CUSTOMER_STATUS
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_LAST_CAPTURE_DATE", DbType:="VarChar(50)")>  _
        Public Property LAST_CAPTURE_DATE() As  String 
            Get
                Return _LAST_CAPTURE_DATE
            End Get
            Set(ByVal value As  String )
               _LAST_CAPTURE_DATE = value
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
        <Column(Storage:="_ACCOUNT_NO", DbType:="VarChar(100)")>  _
        Public Property ACCOUNT_NO() As  String 
            Get
                Return _ACCOUNT_NO
            End Get
            Set(ByVal value As  String )
               _ACCOUNT_NO = value
            End Set
        End Property 
        <Column(Storage:="_WEBSERVICE_HOST_IP", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property WEBSERVICE_HOST_IP() As String
            Get
                Return _WEBSERVICE_HOST_IP
            End Get
            Set(ByVal value As String)
               _WEBSERVICE_HOST_IP = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_JOB_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_JOB_ID() As Long
            Get
                Return _APPOINTMENT_JOB_ID
            End Get
            Set(ByVal value As Long)
               _APPOINTMENT_JOB_ID = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _CAPTURE_TIME = New DateTime(1,1,1)
            _MOBILE_NO = ""
            _ASSIGN_TIME = New DateTime(1,1,1)
            _QUEUE_NO = ""
            _CONTACTID = ""
            _CUSTOMER_FIRST_NAME = ""
            _CUSTOMER_LAST_NAME = ""
            _CUSTOMER_STATUS = ""
            _LAST_CAPTURE_DATE = ""
            _NETWORK_TYPE = ""
            _ACCOUNT_NO = ""
            _WEBSERVICE_HOST_IP = ""
            _APPOINTMENT_JOB_ID = 0
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


        '/// Returns an indication whether the current data is inserted into TB_CUSTOMER_IMAGE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER_IMAGE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER_IMAGE table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMER_IMAGE table successfully.
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


        '/// Returns an indication whether the record of TB_CUSTOMER_IMAGE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_CUSTOMER_IMAGE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbCustomerImageShLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_CUSTOMER_IMAGE by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbCustomerImageShParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMER_IMAGE by specified CAPTURE_TIME key is retrieved successfully.
        '/// <param name=cCAPTURE_TIME>The CAPTURE_TIME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByCAPTURE_TIME(cCAPTURE_TIME As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("CAPTURE_TIME = " & DB.SetDateTime(cCAPTURE_TIME) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_CUSTOMER_IMAGE by specified CAPTURE_TIME key is retrieved successfully.
        '/// <param name=cCAPTURE_TIME>The CAPTURE_TIME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByCAPTURE_TIME(cCAPTURE_TIME As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("CAPTURE_TIME = " & DB.SetDateTime(cCAPTURE_TIME) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMER_IMAGE by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMOBILE_NO(cMOBILE_NO As String, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_CUSTOMER_IMAGE by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMOBILE_NO(cMOBILE_NO As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMER_IMAGE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_CUSTOMER_IMAGE table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER_IMAGE table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMER_IMAGE table successfully.
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


        '/// Returns an indication whether the record of TB_CUSTOMER_IMAGE by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("capture_time")) = False Then _capture_time = Convert.ToDateTime(Rdr("capture_time"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("assign_time")) = False Then _assign_time = Convert.ToDateTime(Rdr("assign_time"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("ContactID")) = False Then _ContactID = Rdr("ContactID").ToString()
                        If Convert.IsDBNull(Rdr("customer_first_name")) = False Then _customer_first_name = Rdr("customer_first_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_last_name")) = False Then _customer_last_name = Rdr("customer_last_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_status")) = False Then _customer_status = Rdr("customer_status").ToString()
                        If Convert.IsDBNull(Rdr("last_capture_date")) = False Then _last_capture_date = Rdr("last_capture_date").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("account_no")) = False Then _account_no = Rdr("account_no").ToString()
                        If Convert.IsDBNull(Rdr("webservice_host_ip")) = False Then _webservice_host_ip = Rdr("webservice_host_ip").ToString()
                        If Convert.IsDBNull(Rdr("appointment_job_id")) = False Then _appointment_job_id = Convert.ToInt64(Rdr("appointment_job_id"))
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


        '/// Returns an indication whether the record of TB_CUSTOMER_IMAGE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbCustomerImageShLinqDB
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
                        If Convert.IsDBNull(Rdr("capture_time")) = False Then _capture_time = Convert.ToDateTime(Rdr("capture_time"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("assign_time")) = False Then _assign_time = Convert.ToDateTime(Rdr("assign_time"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("ContactID")) = False Then _ContactID = Rdr("ContactID").ToString()
                        If Convert.IsDBNull(Rdr("customer_first_name")) = False Then _customer_first_name = Rdr("customer_first_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_last_name")) = False Then _customer_last_name = Rdr("customer_last_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_status")) = False Then _customer_status = Rdr("customer_status").ToString()
                        If Convert.IsDBNull(Rdr("last_capture_date")) = False Then _last_capture_date = Rdr("last_capture_date").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("account_no")) = False Then _account_no = Rdr("account_no").ToString()
                        If Convert.IsDBNull(Rdr("webservice_host_ip")) = False Then _webservice_host_ip = Rdr("webservice_host_ip").ToString()
                        If Convert.IsDBNull(Rdr("appointment_job_id")) = False Then _appointment_job_id = Convert.ToInt64(Rdr("appointment_job_id"))
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


        '/// Returns an indication whether the Class Data of TB_CUSTOMER_IMAGE by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbCustomerImageShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbCustomerImageShParaDB
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
                        If Convert.IsDBNull(Rdr("capture_time")) = False Then ret.capture_time = Convert.ToDateTime(Rdr("capture_time"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("assign_time")) = False Then ret.assign_time = Convert.ToDateTime(Rdr("assign_time"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then ret.queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("ContactID")) = False Then ret.ContactID = Rdr("ContactID").ToString()
                        If Convert.IsDBNull(Rdr("customer_first_name")) = False Then ret.customer_first_name = Rdr("customer_first_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_last_name")) = False Then ret.customer_last_name = Rdr("customer_last_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_status")) = False Then ret.customer_status = Rdr("customer_status").ToString()
                        If Convert.IsDBNull(Rdr("last_capture_date")) = False Then ret.last_capture_date = Rdr("last_capture_date").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("account_no")) = False Then ret.account_no = Rdr("account_no").ToString()
                        If Convert.IsDBNull(Rdr("webservice_host_ip")) = False Then ret.webservice_host_ip = Rdr("webservice_host_ip").ToString()
                        If Convert.IsDBNull(Rdr("appointment_job_id")) = False Then ret.appointment_job_id = Convert.ToInt64(Rdr("appointment_job_id"))

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


        'Get Insert Statement for table TB_CUSTOMER_IMAGE
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, CAPTURE_TIME, MOBILE_NO, ASSIGN_TIME, QUEUE_NO, CONTACTID, CUSTOMER_FIRST_NAME, CUSTOMER_LAST_NAME, CUSTOMER_STATUS, LAST_CAPTURE_DATE, NETWORK_TYPE, ACCOUNT_NO, WEBSERVICE_HOST_IP, APPOINTMENT_JOB_ID)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDateTime(_CAPTURE_TIME) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetDateTime(_ASSIGN_TIME) & ", "
                sql += DB.SetString(_QUEUE_NO) & ", "
                sql += DB.SetString(_CONTACTID) & ", "
                sql += DB.SetString(_CUSTOMER_FIRST_NAME) & ", "
                sql += DB.SetString(_CUSTOMER_LAST_NAME) & ", "
                sql += DB.SetString(_CUSTOMER_STATUS) & ", "
                sql += DB.SetString(_LAST_CAPTURE_DATE) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_ACCOUNT_NO) & ", "
                sql += DB.SetString(_WEBSERVICE_HOST_IP) & ", "
                sql += DB.SetDouble(_APPOINTMENT_JOB_ID)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_CUSTOMER_IMAGE
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "CAPTURE_TIME = " & DB.SetDateTime(_CAPTURE_TIME) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "ASSIGN_TIME = " & DB.SetDateTime(_ASSIGN_TIME) & ", "
                Sql += "QUEUE_NO = " & DB.SetString(_QUEUE_NO) & ", "
                Sql += "CONTACTID = " & DB.SetString(_CONTACTID) & ", "
                Sql += "CUSTOMER_FIRST_NAME = " & DB.SetString(_CUSTOMER_FIRST_NAME) & ", "
                Sql += "CUSTOMER_LAST_NAME = " & DB.SetString(_CUSTOMER_LAST_NAME) & ", "
                Sql += "CUSTOMER_STATUS = " & DB.SetString(_CUSTOMER_STATUS) & ", "
                Sql += "LAST_CAPTURE_DATE = " & DB.SetString(_LAST_CAPTURE_DATE) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "ACCOUNT_NO = " & DB.SetString(_ACCOUNT_NO) & ", "
                Sql += "WEBSERVICE_HOST_IP = " & DB.SetString(_WEBSERVICE_HOST_IP) & ", "
                Sql += "APPOINTMENT_JOB_ID = " & DB.SetDouble(_APPOINTMENT_JOB_ID) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_CUSTOMER_IMAGE
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_CUSTOMER_IMAGE
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, CAPTURE_TIME, MOBILE_NO, ASSIGN_TIME, QUEUE_NO, CONTACTID, CUSTOMER_FIRST_NAME, CUSTOMER_LAST_NAME, CUSTOMER_STATUS, LAST_CAPTURE_DATE, NETWORK_TYPE, ACCOUNT_NO, WEBSERVICE_HOST_IP, APPOINTMENT_JOB_ID FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
