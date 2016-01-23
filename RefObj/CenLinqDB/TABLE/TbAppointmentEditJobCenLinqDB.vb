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
    'Represents a transaction for TB_APPOINTMENT_EDIT_JOB table CenLinqDB.
    '[Create by  on September, 30 2014]
    Public Class TbAppointmentEditJobCenLinqDB
        Public sub TbAppointmentEditJobCenLinqDB()

        End Sub 
        ' TB_APPOINTMENT_EDIT_JOB
        Const _tableName As String = "TB_APPOINTMENT_EDIT_JOB"
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
        Dim _TB_APPOINTMENT_JOB_ID_OLD As Long = 0
        Dim _MOBILE_NO As String = ""
        Dim _TB_SHOP_ID As Long = 0
        Dim _SERVICE_ID As String = ""
        Dim _APP_DATE As DateTime = New DateTime(1,1,1)
        Dim _START_SLOT As DateTime = New DateTime(1,1,1)
        Dim _APPOINTMENT_CHANNEL As Char = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _CUSTOMER_EMAIL As  String  = ""
        Dim _ISDELETEOLDDATA As  System.Nullable(Of Char)  = "N"

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
        <Column(Storage:="_TB_APPOINTMENT_JOB_ID_OLD", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_APPOINTMENT_JOB_ID_OLD() As Long
            Get
                Return _TB_APPOINTMENT_JOB_ID_OLD
            End Get
            Set(ByVal value As Long)
               _TB_APPOINTMENT_JOB_ID_OLD = value
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
        <Column(Storage:="_TB_SHOP_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_SHOP_ID() As Long
            Get
                Return _TB_SHOP_ID
            End Get
            Set(ByVal value As Long)
               _TB_SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_ID", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As String
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As String)
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_APP_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property APP_DATE() As DateTime
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As DateTime)
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_START_SLOT", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property START_SLOT() As DateTime
            Get
                Return _START_SLOT
            End Get
            Set(ByVal value As DateTime)
               _START_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_CHANNEL() As Char
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As Char)
               _APPOINTMENT_CHANNEL = value
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
        <Column(Storage:="_CUSTOMER_EMAIL", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_EMAIL() As  String 
            Get
                Return _CUSTOMER_EMAIL
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_EMAIL = value
            End Set
        End Property 
        <Column(Storage:="_ISDELETEOLDDATA", DbType:="Char(1)")>  _
        Public Property ISDELETEOLDDATA() As  System.Nullable(Of Char) 
            Get
                Return _ISDELETEOLDDATA
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _ISDELETEOLDDATA = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _TB_APPOINTMENT_JOB_ID_OLD = 0
            _MOBILE_NO = ""
            _TB_SHOP_ID = 0
            _SERVICE_ID = ""
            _APP_DATE = New DateTime(1,1,1)
            _START_SLOT = New DateTime(1,1,1)
            _APPOINTMENT_CHANNEL = ""
            _PREFER_LANG = ""
            _CUSTOMER_EMAIL = ""
            _ISDELETEOLDDATA = ""
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


        '/// Returns an indication whether the current data is inserted into TB_APPOINTMENT_EDIT_JOB table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_APPOINTMENT_EDIT_JOB table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_APPOINTMENT_EDIT_JOB table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_APPOINTMENT_EDIT_JOB table successfully.
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


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_APPOINTMENT_EDIT_JOB by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbAppointmentEditJobCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_APPOINTMENT_EDIT_JOB by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbAppointmentEditJobCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMOBILE_NO(cMOBILE_NO As String, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_APPOINTMENT_EDIT_JOB by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMOBILE_NO(cMOBILE_NO As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified TB_SHOP_ID key is retrieved successfully.
        '/// <param name=cTB_SHOP_ID>The TB_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTB_SHOP_ID(cTB_SHOP_ID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("TB_SHOP_ID = " & DB.SetDouble(cTB_SHOP_ID) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_APPOINTMENT_EDIT_JOB by specified TB_SHOP_ID key is retrieved successfully.
        '/// <param name=cTB_SHOP_ID>The TB_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTB_SHOP_ID(cTB_SHOP_ID As Long, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("TB_SHOP_ID = " & DB.SetDouble(cTB_SHOP_ID) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified START_SLOT key is retrieved successfully.
        '/// <param name=cSTART_SLOT>The START_SLOT key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySTART_SLOT(cSTART_SLOT As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("START_SLOT = " & DB.SetDateTime(cSTART_SLOT) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_APPOINTMENT_EDIT_JOB by specified START_SLOT key is retrieved successfully.
        '/// <param name=cSTART_SLOT>The START_SLOT key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySTART_SLOT(cSTART_SLOT As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("START_SLOT = " & DB.SetDateTime(cSTART_SLOT) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_APPOINTMENT_EDIT_JOB table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_APPOINTMENT_EDIT_JOB table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_APPOINTMENT_EDIT_JOB table successfully.
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


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("tb_appointment_job_id_old")) = False Then _tb_appointment_job_id_old = Convert.ToInt64(Rdr("tb_appointment_job_id_old"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("tb_shop_id")) = False Then _tb_shop_id = Convert.ToInt64(Rdr("tb_shop_id"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Rdr("service_id").ToString()
                        If Convert.IsDBNull(Rdr("app_date")) = False Then _app_date = Convert.ToDateTime(Rdr("app_date"))
                        If Convert.IsDBNull(Rdr("start_slot")) = False Then _start_slot = Convert.ToDateTime(Rdr("start_slot"))
                        If Convert.IsDBNull(Rdr("appointment_channel")) = False Then _appointment_channel = Rdr("appointment_channel").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("customer_email")) = False Then _customer_email = Rdr("customer_email").ToString()
                        If Convert.IsDBNull(Rdr("IsDeleteOldData")) = False Then _IsDeleteOldData = Rdr("IsDeleteOldData").ToString()
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


        '/// Returns an indication whether the record of TB_APPOINTMENT_EDIT_JOB by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbAppointmentEditJobCenLinqDB
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
                        If Convert.IsDBNull(Rdr("tb_appointment_job_id_old")) = False Then _tb_appointment_job_id_old = Convert.ToInt64(Rdr("tb_appointment_job_id_old"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("tb_shop_id")) = False Then _tb_shop_id = Convert.ToInt64(Rdr("tb_shop_id"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Rdr("service_id").ToString()
                        If Convert.IsDBNull(Rdr("app_date")) = False Then _app_date = Convert.ToDateTime(Rdr("app_date"))
                        If Convert.IsDBNull(Rdr("start_slot")) = False Then _start_slot = Convert.ToDateTime(Rdr("start_slot"))
                        If Convert.IsDBNull(Rdr("appointment_channel")) = False Then _appointment_channel = Rdr("appointment_channel").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("customer_email")) = False Then _customer_email = Rdr("customer_email").ToString()
                        If Convert.IsDBNull(Rdr("IsDeleteOldData")) = False Then _IsDeleteOldData = Rdr("IsDeleteOldData").ToString()
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


        '/// Returns an indication whether the Class Data of TB_APPOINTMENT_EDIT_JOB by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbAppointmentEditJobCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbAppointmentEditJobCenParaDB
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
                        If Convert.IsDBNull(Rdr("tb_appointment_job_id_old")) = False Then ret.tb_appointment_job_id_old = Convert.ToInt64(Rdr("tb_appointment_job_id_old"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("tb_shop_id")) = False Then ret.tb_shop_id = Convert.ToInt64(Rdr("tb_shop_id"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Rdr("service_id").ToString()
                        If Convert.IsDBNull(Rdr("app_date")) = False Then ret.app_date = Convert.ToDateTime(Rdr("app_date"))
                        If Convert.IsDBNull(Rdr("start_slot")) = False Then ret.start_slot = Convert.ToDateTime(Rdr("start_slot"))
                        If Convert.IsDBNull(Rdr("appointment_channel")) = False Then ret.appointment_channel = Rdr("appointment_channel").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then ret.prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("customer_email")) = False Then ret.customer_email = Rdr("customer_email").ToString()
                        If Convert.IsDBNull(Rdr("IsDeleteOldData")) = False Then ret.IsDeleteOldData = Rdr("IsDeleteOldData").ToString()

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


        'Get Insert Statement for table TB_APPOINTMENT_EDIT_JOB
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TB_APPOINTMENT_JOB_ID_OLD, MOBILE_NO, TB_SHOP_ID, SERVICE_ID, APP_DATE, START_SLOT, APPOINTMENT_CHANNEL, PREFER_LANG, CUSTOMER_EMAIL, ISDELETEOLDDATA)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_TB_APPOINTMENT_JOB_ID_OLD) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetDouble(_TB_SHOP_ID) & ", "
                sql += DB.SetString(_SERVICE_ID) & ", "
                sql += DB.SetDateTime(_APP_DATE) & ", "
                sql += DB.SetDateTime(_START_SLOT) & ", "
                sql += DB.SetString(_APPOINTMENT_CHANNEL) & ", "
                sql += DB.SetString(_PREFER_LANG) & ", "
                sql += DB.SetString(_CUSTOMER_EMAIL) & ", "
                sql += DB.SetString(_ISDELETEOLDDATA)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_APPOINTMENT_EDIT_JOB
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "TB_APPOINTMENT_JOB_ID_OLD = " & DB.SetDouble(_TB_APPOINTMENT_JOB_ID_OLD) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "TB_SHOP_ID = " & DB.SetDouble(_TB_SHOP_ID) & ", "
                Sql += "SERVICE_ID = " & DB.SetString(_SERVICE_ID) & ", "
                Sql += "APP_DATE = " & DB.SetDateTime(_APP_DATE) & ", "
                Sql += "START_SLOT = " & DB.SetDateTime(_START_SLOT) & ", "
                Sql += "APPOINTMENT_CHANNEL = " & DB.SetString(_APPOINTMENT_CHANNEL) & ", "
                Sql += "PREFER_LANG = " & DB.SetString(_PREFER_LANG) & ", "
                Sql += "CUSTOMER_EMAIL = " & DB.SetString(_CUSTOMER_EMAIL) & ", "
                Sql += "ISDELETEOLDDATA = " & DB.SetString(_ISDELETEOLDDATA) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_APPOINTMENT_EDIT_JOB
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_APPOINTMENT_EDIT_JOB
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TB_APPOINTMENT_JOB_ID_OLD, MOBILE_NO, TB_SHOP_ID, SERVICE_ID, APP_DATE, START_SLOT, APPOINTMENT_CHANNEL, PREFER_LANG, CUSTOMER_EMAIL, ISDELETEOLDDATA FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
