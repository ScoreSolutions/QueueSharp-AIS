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
    'Represents a transaction for TB_USER table CenLinqDB.
    '[Create by  on June, 25 2013]
    Public Class TbUserCenLinqDB
        Public sub TbUserCenLinqDB()

        End Sub 
        ' TB_USER
        Const _tableName As String = "TB_USER"
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
        Dim _TITLE_ID As  System.Nullable(Of Long)  = 0
        Dim _USER_CODE As  String  = ""
        Dim _FNAME As  String  = ""
        Dim _LNAME As  String  = ""
        Dim _POSITION As  String  = ""
        Dim _GROUP_ID As  System.Nullable(Of Long)  = 0
        Dim _USERNAME As  String  = ""
        Dim _PASSWORD As  String  = ""
        Dim _IS_ADMIN As System.Nullable(Of Char) = "0"
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _IP_ADDRESS As  String  = ""
        Dim _VIEW_OTHERS_VDO As  String  = ""
        Dim _REPORT_ACTIVE As Char = "1"

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
        <Column(Storage:="_TITLE_ID", DbType:="Int")>  _
        Public Property TITLE_ID() As  System.Nullable(Of Long) 
            Get
                Return _TITLE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TITLE_ID = value
            End Set
        End Property 
        <Column(Storage:="_USER_CODE", DbType:="VarChar(20)")>  _
        Public Property USER_CODE() As  String 
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As  String )
               _USER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_FNAME", DbType:="VarChar(50)")>  _
        Public Property FNAME() As  String 
            Get
                Return _FNAME
            End Get
            Set(ByVal value As  String )
               _FNAME = value
            End Set
        End Property 
        <Column(Storage:="_LNAME", DbType:="VarChar(50)")>  _
        Public Property LNAME() As  String 
            Get
                Return _LNAME
            End Get
            Set(ByVal value As  String )
               _LNAME = value
            End Set
        End Property 
        <Column(Storage:="_POSITION", DbType:="VarChar(50)")>  _
        Public Property POSITION() As  String 
            Get
                Return _POSITION
            End Get
            Set(ByVal value As  String )
               _POSITION = value
            End Set
        End Property 
        <Column(Storage:="_GROUP_ID", DbType:="Int")>  _
        Public Property GROUP_ID() As  System.Nullable(Of Long) 
            Get
                Return _GROUP_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _GROUP_ID = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(50)")>  _
        Public Property USERNAME() As  String 
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As  String )
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_PASSWORD", DbType:="VarChar(50)")>  _
        Public Property PASSWORD() As  String 
            Get
                Return _PASSWORD
            End Get
            Set(ByVal value As  String )
               _PASSWORD = value
            End Set
        End Property 
        <Column(Storage:="_IS_ADMIN", DbType:="Char(1)")>  _
        Public Property IS_ADMIN() As  System.Nullable(Of Char) 
            Get
                Return _IS_ADMIN
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _IS_ADMIN = value
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
        <Column(Storage:="_IP_ADDRESS", DbType:="VarChar(50)")>  _
        Public Property IP_ADDRESS() As  String 
            Get
                Return _IP_ADDRESS
            End Get
            Set(ByVal value As  String )
               _IP_ADDRESS = value
            End Set
        End Property 
        <Column(Storage:="_VIEW_OTHERS_VDO", DbType:="VarChar(5000)")>  _
        Public Property VIEW_OTHERS_VDO() As  String 
            Get
                Return _VIEW_OTHERS_VDO
            End Get
            Set(ByVal value As  String )
               _VIEW_OTHERS_VDO = value
            End Set
        End Property 
        <Column(Storage:="_REPORT_ACTIVE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property REPORT_ACTIVE() As Char
            Get
                Return _REPORT_ACTIVE
            End Get
            Set(ByVal value As Char)
               _REPORT_ACTIVE = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _TITLE_ID = 0
            _USER_CODE = ""
            _FNAME = ""
            _LNAME = ""
            _POSITION = ""
            _GROUP_ID = 0
            _USERNAME = ""
            _PASSWORD = ""
            _IS_ADMIN = ""
            _ACTIVE_STATUS = 0
            _IP_ADDRESS = ""
            _VIEW_OTHERS_VDO = ""
            _REPORT_ACTIVE = ""
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


        '/// Returns an indication whether the current data is inserted into TB_USER table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_USER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_USER table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_USER table successfully.
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


        '/// Returns an indication whether the record of TB_USER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_USER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Integer, trans As SQLTransaction) As TbUserCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_USER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Integer, trans As SQLTransaction) As TbUserCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_USER by specified USERNAME key is retrieved successfully.
        '/// <param name=cUSERNAME>The USERNAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByUSERNAME(cUSERNAME As String, trans As SQLTransaction) As Boolean
            Return doChkData("USERNAME = " & DB.SetString(cUSERNAME) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_USER by specified USERNAME key is retrieved successfully.
        '/// <param name=cUSERNAME>The USERNAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByUSERNAME(cUSERNAME As String, cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("USERNAME = " & DB.SetString(cUSERNAME) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_USER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_USER table successfully.
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
                        _ID = DB.GetLastID(_tableName, trans)
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


        '/// Returns an indication whether the current data is updated to TB_USER table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_USER table successfully.
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


        '/// Returns an indication whether the record of TB_USER by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("title_id")) = False Then _title_id = Convert.ToInt32(Rdr("title_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("fname")) = False Then _fname = Rdr("fname").ToString()
                        If Convert.IsDBNull(Rdr("lname")) = False Then _lname = Rdr("lname").ToString()
                        If Convert.IsDBNull(Rdr("position")) = False Then _position = Rdr("position").ToString()
                        If Convert.IsDBNull(Rdr("group_id")) = False Then _group_id = Convert.ToInt32(Rdr("group_id"))
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("password")) = False Then _password = Rdr("password").ToString()
                        If Convert.IsDBNull(Rdr("is_admin")) = False Then _is_admin = Rdr("is_admin").ToString()
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("ip_address")) = False Then _ip_address = Rdr("ip_address").ToString()
                        If Convert.IsDBNull(Rdr("view_others_vdo")) = False Then _view_others_vdo = Rdr("view_others_vdo").ToString()
                        If Convert.IsDBNull(Rdr("report_active")) = False Then _report_active = Rdr("report_active").ToString()
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


        '/// Returns an indication whether the record of TB_USER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbUserCenLinqDB
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
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("title_id")) = False Then _title_id = Convert.ToInt32(Rdr("title_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then _user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("fname")) = False Then _fname = Rdr("fname").ToString()
                        If Convert.IsDBNull(Rdr("lname")) = False Then _lname = Rdr("lname").ToString()
                        If Convert.IsDBNull(Rdr("position")) = False Then _position = Rdr("position").ToString()
                        If Convert.IsDBNull(Rdr("group_id")) = False Then _group_id = Convert.ToInt32(Rdr("group_id"))
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("password")) = False Then _password = Rdr("password").ToString()
                        If Convert.IsDBNull(Rdr("is_admin")) = False Then _is_admin = Rdr("is_admin").ToString()
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("ip_address")) = False Then _ip_address = Rdr("ip_address").ToString()
                        If Convert.IsDBNull(Rdr("view_others_vdo")) = False Then _view_others_vdo = Rdr("view_others_vdo").ToString()
                        If Convert.IsDBNull(Rdr("report_active")) = False Then _report_active = Rdr("report_active").ToString()
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


        '/// Returns an indication whether the Class Data of TB_USER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbUserCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbUserCenParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("title_id")) = False Then ret.title_id = Convert.ToInt32(Rdr("title_id"))
                        If Convert.IsDBNull(Rdr("user_code")) = False Then ret.user_code = Rdr("user_code").ToString()
                        If Convert.IsDBNull(Rdr("fname")) = False Then ret.fname = Rdr("fname").ToString()
                        If Convert.IsDBNull(Rdr("lname")) = False Then ret.lname = Rdr("lname").ToString()
                        If Convert.IsDBNull(Rdr("position")) = False Then ret.position = Rdr("position").ToString()
                        If Convert.IsDBNull(Rdr("group_id")) = False Then ret.group_id = Convert.ToInt32(Rdr("group_id"))
                        If Convert.IsDBNull(Rdr("username")) = False Then ret.username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("password")) = False Then ret.password = Rdr("password").ToString()
                        If Convert.IsDBNull(Rdr("is_admin")) = False Then ret.is_admin = Rdr("is_admin").ToString()
                        If Convert.IsDBNull(Rdr("active_status")) = False Then ret.active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("ip_address")) = False Then ret.ip_address = Rdr("ip_address").ToString()
                        If Convert.IsDBNull(Rdr("view_others_vdo")) = False Then ret.view_others_vdo = Rdr("view_others_vdo").ToString()
                        If Convert.IsDBNull(Rdr("report_active")) = False Then ret.report_active = Rdr("report_active").ToString()

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


        'Get Insert Statement for table TB_USER
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TITLE_ID, USER_CODE, FNAME, LNAME, POSITION, GROUP_ID, USERNAME, PASSWORD, IS_ADMIN, ACTIVE_STATUS, IP_ADDRESS, VIEW_OTHERS_VDO, REPORT_ACTIVE)"
                Sql += " VALUES("
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_TITLE_ID) & ", "
                sql += DB.SetString(_USER_CODE) & ", "
                sql += DB.SetString(_FNAME) & ", "
                sql += DB.SetString(_LNAME) & ", "
                sql += DB.SetString(_POSITION) & ", "
                sql += DB.SetDouble(_GROUP_ID) & ", "
                sql += DB.SetString(_USERNAME) & ", "
                sql += DB.SetString(_PASSWORD) & ", "
                sql += DB.SetString(_IS_ADMIN) & ", "
                sql += DB.SetDouble(_ACTIVE_STATUS) & ", "
                sql += DB.SetString(_IP_ADDRESS) & ", "
                sql += DB.SetString(_VIEW_OTHERS_VDO) & ", "
                sql += DB.SetString(_REPORT_ACTIVE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_USER
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "TITLE_ID = " & DB.SetDouble(_TITLE_ID) & ", "
                Sql += "USER_CODE = " & DB.SetString(_USER_CODE) & ", "
                Sql += "FNAME = " & DB.SetString(_FNAME) & ", "
                Sql += "LNAME = " & DB.SetString(_LNAME) & ", "
                Sql += "POSITION = " & DB.SetString(_POSITION) & ", "
                Sql += "GROUP_ID = " & DB.SetDouble(_GROUP_ID) & ", "
                Sql += "USERNAME = " & DB.SetString(_USERNAME) & ", "
                Sql += "PASSWORD = " & DB.SetString(_PASSWORD) & ", "
                Sql += "IS_ADMIN = " & DB.SetString(_IS_ADMIN) & ", "
                Sql += "ACTIVE_STATUS = " & DB.SetDouble(_ACTIVE_STATUS) & ", "
                Sql += "IP_ADDRESS = " & DB.SetString(_IP_ADDRESS) & ", "
                Sql += "VIEW_OTHERS_VDO = " & DB.SetString(_VIEW_OTHERS_VDO) & ", "
                Sql += "REPORT_ACTIVE = " & DB.SetString(_REPORT_ACTIVE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_USER
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_USER
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TITLE_ID, USER_CODE, FNAME, LNAME, POSITION, GROUP_ID, USERNAME, PASSWORD, IS_ADMIN, ACTIVE_STATUS, IP_ADDRESS, VIEW_OTHERS_VDO, REPORT_ACTIVE FROM " & tableName
                Return Sql
            End Get
        End Property


            'Define Child Table 

       Dim _TB_ROLE_USER_tb_user_id As DataTable
       Dim _TB_USER_SHOP_tb_user_id As DataTable

       Public Property CHILD_TB_ROLE_USER_tb_user_id() As DataTable
           Get
               'Child Table Name : TB_ROLE_USER Column :tb_user_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRoleUserItem As New TbRoleUserCenLinqDB
               _TB_ROLE_USER_tb_user_id = TbRoleUserItem.GetDataList("tb_user_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRoleUserItem = Nothing
               Return _TB_ROLE_USER_tb_user_id
           End Get
           Set(ByVal value As DataTable)
               _TB_ROLE_USER_tb_user_id = value
           End Set
       End Property
       Public Property CHILD_TB_USER_SHOP_tb_user_id() As DataTable
           Get
               'Child Table Name : TB_USER_SHOP Column :tb_user_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbUserShopItem As New TbUserShopCenLinqDB
               _TB_USER_SHOP_tb_user_id = TbUserShopItem.GetDataList("tb_user_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbUserShopItem = Nothing
               Return _TB_USER_SHOP_tb_user_id
           End Get
           Set(ByVal value As DataTable)
               _TB_USER_SHOP_tb_user_id = value
           End Set
       End Property
    End Class
End Namespace
