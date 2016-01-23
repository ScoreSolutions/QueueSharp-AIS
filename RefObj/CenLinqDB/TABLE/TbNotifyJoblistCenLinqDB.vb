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
    'Represents a transaction for TB_NOTIFY_JOBLIST table CenLinqDB.
    '[Create by  on April, 12 2012]
    Public Class TbNotifyJoblistCenLinqDB
        Public sub TbNotifyJoblistCenLinqDB()

        End Sub 
        ' TB_NOTIFY_JOBLIST
        Const _tableName As String = "TB_NOTIFY_JOBLIST"
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
        Dim _SHOP_ID As  System.Nullable(Of Long)  = 0
        Dim _MOBILE_NO As String = ""
        Dim _APPOINTMENT_CHANNEL As Char = ""
        Dim _APPOINTMENT_TIME As DateTime = New DateTime(1,1,1)
        Dim _SMS_TIME1 As DateTime = New DateTime(1,1,1)
        Dim _SMS_ALERT1 As Char = ""
        Dim _SMS_MSG1 As String = ""
        Dim _SMS_TIME2 As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SMS_ALERT2 As  System.Nullable(Of Char)  = ""
        Dim _SMS_MSG2 As  String  = ""
        Dim _CUSTOMER_EMAIL As  String  = ""
        Dim _EMAIL_TIME1 As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _EMAIL_ALERT1 As  System.Nullable(Of Char)  = ""
        Dim _EMAIL_MSG1 As  String  = ""
        Dim _EMAIL_TIME2 As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _EMAIL_ALERT2 As  System.Nullable(Of Char)  = ""
        Dim _EMAIL_MSG2 As  String  = ""

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
        <Column(Storage:="_SHOP_ID", DbType:="Int")>  _
        Public Property SHOP_ID() As  System.Nullable(Of Long) 
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SHOP_ID = value
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
        <Column(Storage:="_APPOINTMENT_CHANNEL", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_CHANNEL() As Char
            Get
                Return _APPOINTMENT_CHANNEL
            End Get
            Set(ByVal value As Char)
               _APPOINTMENT_CHANNEL = value
            End Set
        End Property 
        <Column(Storage:="_APPOINTMENT_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property APPOINTMENT_TIME() As DateTime
            Get
                Return _APPOINTMENT_TIME
            End Get
            Set(ByVal value As DateTime)
               _APPOINTMENT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SMS_TIME1", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SMS_TIME1() As DateTime
            Get
                Return _SMS_TIME1
            End Get
            Set(ByVal value As DateTime)
               _SMS_TIME1 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_ALERT1", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SMS_ALERT1() As Char
            Get
                Return _SMS_ALERT1
            End Get
            Set(ByVal value As Char)
               _SMS_ALERT1 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_MSG1", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property SMS_MSG1() As String
            Get
                Return _SMS_MSG1
            End Get
            Set(ByVal value As String)
               _SMS_MSG1 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_TIME2", DbType:="DateTime")>  _
        Public Property SMS_TIME2() As  System.Nullable(Of DateTime) 
            Get
                Return _SMS_TIME2
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SMS_TIME2 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_ALERT2", DbType:="Char(1)")>  _
        Public Property SMS_ALERT2() As  System.Nullable(Of Char) 
            Get
                Return _SMS_ALERT2
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _SMS_ALERT2 = value
            End Set
        End Property 
        <Column(Storage:="_SMS_MSG2", DbType:="VarChar(500)")>  _
        Public Property SMS_MSG2() As  String 
            Get
                Return _SMS_MSG2
            End Get
            Set(ByVal value As  String )
               _SMS_MSG2 = value
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
        <Column(Storage:="_EMAIL_TIME1", DbType:="DateTime")>  _
        Public Property EMAIL_TIME1() As  System.Nullable(Of DateTime) 
            Get
                Return _EMAIL_TIME1
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _EMAIL_TIME1 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_ALERT1", DbType:="Char(1)")>  _
        Public Property EMAIL_ALERT1() As  System.Nullable(Of Char) 
            Get
                Return _EMAIL_ALERT1
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _EMAIL_ALERT1 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_MSG1", DbType:="VarChar(500)")>  _
        Public Property EMAIL_MSG1() As  String 
            Get
                Return _EMAIL_MSG1
            End Get
            Set(ByVal value As  String )
               _EMAIL_MSG1 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_TIME2", DbType:="DateTime")>  _
        Public Property EMAIL_TIME2() As  System.Nullable(Of DateTime) 
            Get
                Return _EMAIL_TIME2
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _EMAIL_TIME2 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_ALERT2", DbType:="Char(1)")>  _
        Public Property EMAIL_ALERT2() As  System.Nullable(Of Char) 
            Get
                Return _EMAIL_ALERT2
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _EMAIL_ALERT2 = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL_MSG2", DbType:="VarChar(500)")>  _
        Public Property EMAIL_MSG2() As  String 
            Get
                Return _EMAIL_MSG2
            End Get
            Set(ByVal value As  String )
               _EMAIL_MSG2 = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _SHOP_ID = 0
            _MOBILE_NO = ""
            _APPOINTMENT_CHANNEL = ""
            _APPOINTMENT_TIME = New DateTime(1,1,1)
            _SMS_TIME1 = New DateTime(1,1,1)
            _SMS_ALERT1 = ""
            _SMS_MSG1 = ""
            _SMS_TIME2 = New DateTime(1,1,1)
            _SMS_ALERT2 = ""
            _SMS_MSG2 = ""
            _CUSTOMER_EMAIL = ""
            _EMAIL_TIME1 = New DateTime(1,1,1)
            _EMAIL_ALERT1 = ""
            _EMAIL_MSG1 = ""
            _EMAIL_TIME2 = New DateTime(1,1,1)
            _EMAIL_ALERT2 = ""
            _EMAIL_MSG2 = ""
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


        '/// Returns an indication whether the current data is inserted into TB_NOTIFY_JOBLIST table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_NOTIFY_JOBLIST table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_NOTIFY_JOBLIST table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_NOTIFY_JOBLIST table successfully.
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


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_NOTIFY_JOBLIST by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Long, trans As SQLTransaction) As TbNotifyJoblistCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_NOTIFY_JOBLIST by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Long, trans As SQLTransaction) As TbNotifyJoblistCenParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMOBILE_NO(cMOBILE_NO As String, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_NOTIFY_JOBLIST by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMOBILE_NO(cMOBILE_NO As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified SMS_TIME1 key is retrieved successfully.
        '/// <param name=cSMS_TIME1>The SMS_TIME1 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySMS_TIME1(cSMS_TIME1 As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("SMS_TIME1 = " & DB.SetDateTime(cSMS_TIME1) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_NOTIFY_JOBLIST by specified SMS_TIME1 key is retrieved successfully.
        '/// <param name=cSMS_TIME1>The SMS_TIME1 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySMS_TIME1(cSMS_TIME1 As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SMS_TIME1 = " & DB.SetDateTime(cSMS_TIME1) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified SMS_TIME2 key is retrieved successfully.
        '/// <param name=cSMS_TIME2>The SMS_TIME2 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySMS_TIME2(cSMS_TIME2 As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("SMS_TIME2 = " & DB.SetDateTime(cSMS_TIME2) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_NOTIFY_JOBLIST by specified SMS_TIME2 key is retrieved successfully.
        '/// <param name=cSMS_TIME2>The SMS_TIME2 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySMS_TIME2(cSMS_TIME2 As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SMS_TIME2 = " & DB.SetDateTime(cSMS_TIME2) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified EMAIL_TIME1 key is retrieved successfully.
        '/// <param name=cEMAIL_TIME1>The EMAIL_TIME1 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByEMAIL_TIME1(cEMAIL_TIME1 As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("EMAIL_TIME1 = " & DB.SetDateTime(cEMAIL_TIME1) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_NOTIFY_JOBLIST by specified EMAIL_TIME1 key is retrieved successfully.
        '/// <param name=cEMAIL_TIME1>The EMAIL_TIME1 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByEMAIL_TIME1(cEMAIL_TIME1 As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("EMAIL_TIME1 = " & DB.SetDateTime(cEMAIL_TIME1) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified EMAIL_TIME2 key is retrieved successfully.
        '/// <param name=cEMAIL_TIME2>The EMAIL_TIME2 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByEMAIL_TIME2(cEMAIL_TIME2 As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("EMAIL_TIME2 = " & DB.SetDateTime(cEMAIL_TIME2) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_NOTIFY_JOBLIST by specified EMAIL_TIME2 key is retrieved successfully.
        '/// <param name=cEMAIL_TIME2>The EMAIL_TIME2 key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByEMAIL_TIME2(cEMAIL_TIME2 As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("EMAIL_TIME2 = " & DB.SetDateTime(cEMAIL_TIME2) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_NOTIFY_JOBLIST table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_NOTIFY_JOBLIST table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_NOTIFY_JOBLIST table successfully.
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


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("appointment_channel")) = False Then _appointment_channel = Rdr("appointment_channel").ToString()
                        If Convert.IsDBNull(Rdr("appointment_time")) = False Then _appointment_time = Convert.ToDateTime(Rdr("appointment_time"))
                        If Convert.IsDBNull(Rdr("sms_time1")) = False Then _sms_time1 = Convert.ToDateTime(Rdr("sms_time1"))
                        If Convert.IsDBNull(Rdr("sms_alert1")) = False Then _sms_alert1 = Rdr("sms_alert1").ToString()
                        If Convert.IsDBNull(Rdr("sms_msg1")) = False Then _sms_msg1 = Rdr("sms_msg1").ToString()
                        If Convert.IsDBNull(Rdr("sms_time2")) = False Then _sms_time2 = Convert.ToDateTime(Rdr("sms_time2"))
                        If Convert.IsDBNull(Rdr("sms_alert2")) = False Then _sms_alert2 = Rdr("sms_alert2").ToString()
                        If Convert.IsDBNull(Rdr("sms_msg2")) = False Then _sms_msg2 = Rdr("sms_msg2").ToString()
                        If Convert.IsDBNull(Rdr("customer_email")) = False Then _customer_email = Rdr("customer_email").ToString()
                        If Convert.IsDBNull(Rdr("email_time1")) = False Then _email_time1 = Convert.ToDateTime(Rdr("email_time1"))
                        If Convert.IsDBNull(Rdr("email_alert1")) = False Then _email_alert1 = Rdr("email_alert1").ToString()
                        If Convert.IsDBNull(Rdr("email_msg1")) = False Then _email_msg1 = Rdr("email_msg1").ToString()
                        If Convert.IsDBNull(Rdr("email_time2")) = False Then _email_time2 = Convert.ToDateTime(Rdr("email_time2"))
                        If Convert.IsDBNull(Rdr("email_alert2")) = False Then _email_alert2 = Rdr("email_alert2").ToString()
                        If Convert.IsDBNull(Rdr("email_msg2")) = False Then _email_msg2 = Rdr("email_msg2").ToString()
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


        '/// Returns an indication whether the record of TB_NOTIFY_JOBLIST by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbNotifyJoblistCenLinqDB
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
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("appointment_channel")) = False Then _appointment_channel = Rdr("appointment_channel").ToString()
                        If Convert.IsDBNull(Rdr("appointment_time")) = False Then _appointment_time = Convert.ToDateTime(Rdr("appointment_time"))
                        If Convert.IsDBNull(Rdr("sms_time1")) = False Then _sms_time1 = Convert.ToDateTime(Rdr("sms_time1"))
                        If Convert.IsDBNull(Rdr("sms_alert1")) = False Then _sms_alert1 = Rdr("sms_alert1").ToString()
                        If Convert.IsDBNull(Rdr("sms_msg1")) = False Then _sms_msg1 = Rdr("sms_msg1").ToString()
                        If Convert.IsDBNull(Rdr("sms_time2")) = False Then _sms_time2 = Convert.ToDateTime(Rdr("sms_time2"))
                        If Convert.IsDBNull(Rdr("sms_alert2")) = False Then _sms_alert2 = Rdr("sms_alert2").ToString()
                        If Convert.IsDBNull(Rdr("sms_msg2")) = False Then _sms_msg2 = Rdr("sms_msg2").ToString()
                        If Convert.IsDBNull(Rdr("customer_email")) = False Then _customer_email = Rdr("customer_email").ToString()
                        If Convert.IsDBNull(Rdr("email_time1")) = False Then _email_time1 = Convert.ToDateTime(Rdr("email_time1"))
                        If Convert.IsDBNull(Rdr("email_alert1")) = False Then _email_alert1 = Rdr("email_alert1").ToString()
                        If Convert.IsDBNull(Rdr("email_msg1")) = False Then _email_msg1 = Rdr("email_msg1").ToString()
                        If Convert.IsDBNull(Rdr("email_time2")) = False Then _email_time2 = Convert.ToDateTime(Rdr("email_time2"))
                        If Convert.IsDBNull(Rdr("email_alert2")) = False Then _email_alert2 = Rdr("email_alert2").ToString()
                        If Convert.IsDBNull(Rdr("email_msg2")) = False Then _email_msg2 = Rdr("email_msg2").ToString()

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


        '/// Returns an indication whether the Class Data of TB_NOTIFY_JOBLIST by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbNotifyJoblistCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbNotifyJoblistCenParaDB
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
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("appointment_channel")) = False Then ret.appointment_channel = Rdr("appointment_channel").ToString()
                        If Convert.IsDBNull(Rdr("appointment_time")) = False Then ret.appointment_time = Convert.ToDateTime(Rdr("appointment_time"))
                        If Convert.IsDBNull(Rdr("sms_time1")) = False Then ret.sms_time1 = Convert.ToDateTime(Rdr("sms_time1"))
                        If Convert.IsDBNull(Rdr("sms_alert1")) = False Then ret.sms_alert1 = Rdr("sms_alert1").ToString()
                        If Convert.IsDBNull(Rdr("sms_msg1")) = False Then ret.sms_msg1 = Rdr("sms_msg1").ToString()
                        If Convert.IsDBNull(Rdr("sms_time2")) = False Then ret.sms_time2 = Convert.ToDateTime(Rdr("sms_time2"))
                        If Convert.IsDBNull(Rdr("sms_alert2")) = False Then ret.sms_alert2 = Rdr("sms_alert2").ToString()
                        If Convert.IsDBNull(Rdr("sms_msg2")) = False Then ret.sms_msg2 = Rdr("sms_msg2").ToString()
                        If Convert.IsDBNull(Rdr("customer_email")) = False Then ret.customer_email = Rdr("customer_email").ToString()
                        If Convert.IsDBNull(Rdr("email_time1")) = False Then ret.email_time1 = Convert.ToDateTime(Rdr("email_time1"))
                        If Convert.IsDBNull(Rdr("email_alert1")) = False Then ret.email_alert1 = Rdr("email_alert1").ToString()
                        If Convert.IsDBNull(Rdr("email_msg1")) = False Then ret.email_msg1 = Rdr("email_msg1").ToString()
                        If Convert.IsDBNull(Rdr("email_time2")) = False Then ret.email_time2 = Convert.ToDateTime(Rdr("email_time2"))
                        If Convert.IsDBNull(Rdr("email_alert2")) = False Then ret.email_alert2 = Rdr("email_alert2").ToString()
                        If Convert.IsDBNull(Rdr("email_msg2")) = False Then ret.email_msg2 = Rdr("email_msg2").ToString()

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


        'Get Insert Statement for table TB_NOTIFY_JOBLIST
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, MOBILE_NO, APPOINTMENT_CHANNEL, APPOINTMENT_TIME, SMS_TIME1, SMS_ALERT1, SMS_MSG1, SMS_TIME2, SMS_ALERT2, SMS_MSG2, CUSTOMER_EMAIL, EMAIL_TIME1, EMAIL_ALERT1, EMAIL_MSG1, EMAIL_TIME2, EMAIL_ALERT2, EMAIL_MSG2)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetString(_APPOINTMENT_CHANNEL) & ", "
                sql += DB.SetDateTime(_APPOINTMENT_TIME) & ", "
                sql += DB.SetDateTime(_SMS_TIME1) & ", "
                sql += DB.SetString(_SMS_ALERT1) & ", "
                sql += DB.SetString(_SMS_MSG1) & ", "
                sql += DB.SetDateTime(_SMS_TIME2) & ", "
                sql += DB.SetString(_SMS_ALERT2) & ", "
                sql += DB.SetString(_SMS_MSG2) & ", "
                sql += DB.SetString(_CUSTOMER_EMAIL) & ", "
                sql += DB.SetDateTime(_EMAIL_TIME1) & ", "
                sql += DB.SetString(_EMAIL_ALERT1) & ", "
                sql += DB.SetString(_EMAIL_MSG1) & ", "
                sql += DB.SetDateTime(_EMAIL_TIME2) & ", "
                sql += DB.SetString(_EMAIL_ALERT2) & ", "
                sql += DB.SetString(_EMAIL_MSG2)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_NOTIFY_JOBLIST
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
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "APPOINTMENT_CHANNEL = " & DB.SetString(_APPOINTMENT_CHANNEL) & ", "
                Sql += "APPOINTMENT_TIME = " & DB.SetDateTime(_APPOINTMENT_TIME) & ", "
                Sql += "SMS_TIME1 = " & DB.SetDateTime(_SMS_TIME1) & ", "
                Sql += "SMS_ALERT1 = " & DB.SetString(_SMS_ALERT1) & ", "
                Sql += "SMS_MSG1 = " & DB.SetString(_SMS_MSG1) & ", "
                Sql += "SMS_TIME2 = " & DB.SetDateTime(_SMS_TIME2) & ", "
                Sql += "SMS_ALERT2 = " & DB.SetString(_SMS_ALERT2) & ", "
                Sql += "SMS_MSG2 = " & DB.SetString(_SMS_MSG2) & ", "
                Sql += "CUSTOMER_EMAIL = " & DB.SetString(_CUSTOMER_EMAIL) & ", "
                Sql += "EMAIL_TIME1 = " & DB.SetDateTime(_EMAIL_TIME1) & ", "
                Sql += "EMAIL_ALERT1 = " & DB.SetString(_EMAIL_ALERT1) & ", "
                Sql += "EMAIL_MSG1 = " & DB.SetString(_EMAIL_MSG1) & ", "
                Sql += "EMAIL_TIME2 = " & DB.SetDateTime(_EMAIL_TIME2) & ", "
                Sql += "EMAIL_ALERT2 = " & DB.SetString(_EMAIL_ALERT2) & ", "
                Sql += "EMAIL_MSG2 = " & DB.SetString(_EMAIL_MSG2) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_NOTIFY_JOBLIST
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_NOTIFY_JOBLIST
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
