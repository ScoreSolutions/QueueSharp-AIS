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
    'Represents a transaction for TB_QM_TRANSFER_LOG table ShLinqDB.
    '[Create by  on October, 10 2013]
    Public Class TbQmTransferLogShLinqDB
        Public sub TbQmTransferLogShLinqDB()

        End Sub 
        ' TB_QM_TRANSFER_LOG
        Const _tableName As String = "TB_QM_TRANSFER_LOG"
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
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _TRANSFER_DATE As DateTime = New DateTime(1,1,1)
        Dim _TB_COUNTER_QUEUE_ID As Long = 0
        Dim _QUEUE_NO As String = ""
        Dim _MOBILE_NO As  String  = ""
        Dim _ITEM_ID As Long = 0
        Dim _ITEM_NAME_EN As String = ""
        Dim _COUNTER_ID As Long = 0
        Dim _COUNTER_NAME As String = ""
        Dim _STATUS As Char = "1"

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(100)")>  _
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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TRANSFER_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TRANSFER_DATE() As DateTime
            Get
                Return _TRANSFER_DATE
            End Get
            Set(ByVal value As DateTime)
               _TRANSFER_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TB_COUNTER_QUEUE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_COUNTER_QUEUE_ID() As Long
            Get
                Return _TB_COUNTER_QUEUE_ID
            End Get
            Set(ByVal value As Long)
               _TB_COUNTER_QUEUE_ID = value
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
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50)")>  _
        Public Property MOBILE_NO() As  String 
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As  String )
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_ID() As Long
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As Long)
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_NAME_EN() As String
            Get
                Return _ITEM_NAME_EN
            End Get
            Set(ByVal value As String)
               _ITEM_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_ID() As Long
            Get
                Return _COUNTER_ID
            End Get
            Set(ByVal value As Long)
               _COUNTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_NAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNTER_NAME() As String
            Get
                Return _COUNTER_NAME
            End Get
            Set(ByVal value As String)
               _COUNTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property STATUS() As Char
            Get
                Return _STATUS
            End Get
            Set(ByVal value As Char)
               _STATUS = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _SERVICE_DATE = New DateTime(1,1,1)
            _TRANSFER_DATE = New DateTime(1,1,1)
            _TB_COUNTER_QUEUE_ID = 0
            _QUEUE_NO = ""
            _MOBILE_NO = ""
            _ITEM_ID = 0
            _ITEM_NAME_EN = ""
            _COUNTER_ID = 0
            _COUNTER_NAME = ""
            _STATUS = ""
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


        '/// Returns an indication whether the current data is inserted into TB_QM_TRANSFER_LOG table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_QM_TRANSFER_LOG table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_QM_TRANSFER_LOG table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_QM_TRANSFER_LOG table successfully.
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


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_QM_TRANSFER_LOG by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbQmTransferLogShLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_QM_TRANSFER_LOG by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbQmTransferLogShParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified SERVICE_DATE_TB_COUNTER_QUEUE_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_TB_COUNTER_QUEUE_ID>The SERVICE_DATE_TB_COUNTER_QUEUE_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE_TB_COUNTER_QUEUE_ID(cSERVICE_DATE As DateTime, cTB_COUNTER_QUEUE_ID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND TB_COUNTER_QUEUE_ID = " & DB.SetDouble(cTB_COUNTER_QUEUE_ID), trans)
        End Function

        '/// Returns an duplicate data record of TB_QM_TRANSFER_LOG by specified SERVICE_DATE_TB_COUNTER_QUEUE_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_TB_COUNTER_QUEUE_ID>The SERVICE_DATE_TB_COUNTER_QUEUE_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE_TB_COUNTER_QUEUE_ID(cSERVICE_DATE As DateTime, cTB_COUNTER_QUEUE_ID As Long, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND TB_COUNTER_QUEUE_ID = " & DB.SetDouble(cTB_COUNTER_QUEUE_ID) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified SERVICE_DATE_STATUS key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_STATUS>The SERVICE_DATE_STATUS key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE_STATUS(cSERVICE_DATE As DateTime, cSTATUS As String, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND STATUS = " & DB.SetString(cSTATUS), trans)
        End Function

        '/// Returns an duplicate data record of TB_QM_TRANSFER_LOG by specified SERVICE_DATE_STATUS key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_STATUS>The SERVICE_DATE_STATUS key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE_STATUS(cSERVICE_DATE As DateTime, cSTATUS As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND STATUS = " & DB.SetString(cSTATUS) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified TRANSFER_DATE key is retrieved successfully.
        '/// <param name=cTRANSFER_DATE>The TRANSFER_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTRANSFER_DATE(cTRANSFER_DATE As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("TRANSFER_DATE = " & DB.SetDateTime(cTRANSFER_DATE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_QM_TRANSFER_LOG by specified TRANSFER_DATE key is retrieved successfully.
        '/// <param name=cTRANSFER_DATE>The TRANSFER_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTRANSFER_DATE(cTRANSFER_DATE As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("TRANSFER_DATE = " & DB.SetDateTime(cTRANSFER_DATE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_QM_TRANSFER_LOG table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_QM_TRANSFER_LOG table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_QM_TRANSFER_LOG table successfully.
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


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("transfer_date")) = False Then _transfer_date = Convert.ToDateTime(Rdr("transfer_date"))
                        If Convert.IsDBNull(Rdr("tb_counter_queue_id")) = False Then _tb_counter_queue_id = Convert.ToInt64(Rdr("tb_counter_queue_id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_name_en")) = False Then _item_name_en = Rdr("item_name_en").ToString()
                        If Convert.IsDBNull(Rdr("counter_id")) = False Then _counter_id = Convert.ToInt32(Rdr("counter_id"))
                        If Convert.IsDBNull(Rdr("counter_name")) = False Then _counter_name = Rdr("counter_name").ToString()
                        If Convert.IsDBNull(Rdr("status")) = False Then _status = Rdr("status").ToString()
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


        '/// Returns an indication whether the record of TB_QM_TRANSFER_LOG by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbQmTransferLogShLinqDB
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("transfer_date")) = False Then _transfer_date = Convert.ToDateTime(Rdr("transfer_date"))
                        If Convert.IsDBNull(Rdr("tb_counter_queue_id")) = False Then _tb_counter_queue_id = Convert.ToInt64(Rdr("tb_counter_queue_id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_name_en")) = False Then _item_name_en = Rdr("item_name_en").ToString()
                        If Convert.IsDBNull(Rdr("counter_id")) = False Then _counter_id = Convert.ToInt32(Rdr("counter_id"))
                        If Convert.IsDBNull(Rdr("counter_name")) = False Then _counter_name = Rdr("counter_name").ToString()
                        If Convert.IsDBNull(Rdr("status")) = False Then _status = Rdr("status").ToString()
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


        '/// Returns an indication whether the Class Data of TB_QM_TRANSFER_LOG by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbQmTransferLogShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbQmTransferLogShParaDB
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("transfer_date")) = False Then ret.transfer_date = Convert.ToDateTime(Rdr("transfer_date"))
                        If Convert.IsDBNull(Rdr("tb_counter_queue_id")) = False Then ret.tb_counter_queue_id = Convert.ToInt64(Rdr("tb_counter_queue_id"))
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then ret.queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then ret.item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_name_en")) = False Then ret.item_name_en = Rdr("item_name_en").ToString()
                        If Convert.IsDBNull(Rdr("counter_id")) = False Then ret.counter_id = Convert.ToInt32(Rdr("counter_id"))
                        If Convert.IsDBNull(Rdr("counter_name")) = False Then ret.counter_name = Rdr("counter_name").ToString()
                        If Convert.IsDBNull(Rdr("status")) = False Then ret.status = Rdr("status").ToString()

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


        'Get Insert Statement for table TB_QM_TRANSFER_LOG
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SERVICE_DATE, TRANSFER_DATE, TB_COUNTER_QUEUE_ID, QUEUE_NO, MOBILE_NO, ITEM_ID, ITEM_NAME_EN, COUNTER_ID, COUNTER_NAME, STATUS)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetDateTime(_TRANSFER_DATE) & ", "
                sql += DB.SetDouble(_TB_COUNTER_QUEUE_ID) & ", "
                sql += DB.SetString(_QUEUE_NO) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetDouble(_ITEM_ID) & ", "
                sql += DB.SetString(_ITEM_NAME_EN) & ", "
                sql += DB.SetDouble(_COUNTER_ID) & ", "
                sql += DB.SetString(_COUNTER_NAME) & ", "
                sql += DB.SetString(_STATUS)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_QM_TRANSFER_LOG
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "TRANSFER_DATE = " & DB.SetDateTime(_TRANSFER_DATE) & ", "
                Sql += "TB_COUNTER_QUEUE_ID = " & DB.SetDouble(_TB_COUNTER_QUEUE_ID) & ", "
                Sql += "QUEUE_NO = " & DB.SetString(_QUEUE_NO) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "ITEM_ID = " & DB.SetDouble(_ITEM_ID) & ", "
                Sql += "ITEM_NAME_EN = " & DB.SetString(_ITEM_NAME_EN) & ", "
                Sql += "COUNTER_ID = " & DB.SetDouble(_COUNTER_ID) & ", "
                Sql += "COUNTER_NAME = " & DB.SetString(_COUNTER_NAME) & ", "
                Sql += "STATUS = " & DB.SetString(_STATUS) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_QM_TRANSFER_LOG
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_QM_TRANSFER_LOG
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SERVICE_DATE, TRANSFER_DATE, TB_COUNTER_QUEUE_ID, QUEUE_NO, MOBILE_NO, ITEM_ID, ITEM_NAME_EN, COUNTER_ID, COUNTER_NAME, STATUS FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
