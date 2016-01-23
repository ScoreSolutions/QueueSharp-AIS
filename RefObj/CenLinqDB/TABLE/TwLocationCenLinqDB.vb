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
    'Represents a transaction for TW_LOCATION table CenLinqDB.
    '[Create by  on May, 29 2014]
    Public Class TwLocationCenLinqDB
        Public sub TwLocationCenLinqDB()

        End Sub 
        ' TW_LOCATION
        Const _tableName As String = "TW_LOCATION"
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
        Dim _LOCATION_CODE As String = ""
        Dim _LOCATION_NAME_TH As  String  = ""
        Dim _LOCATION_NAME_EN As  String  = ""
        Dim _LOCATION_ABB As  String  = ""
        Dim _LOCATION_SEGMENT As  String  = ""
        Dim _LOCATION_TYPE As  String  = ""
        Dim _PROVINCE_CODE As String = ""
        Dim _REGION_CODE As String = ""
        Dim _ACTIVE As Char = "Y"

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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
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
        <Column(Storage:="_LOCATION_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOCATION_CODE() As String
            Get
                Return _LOCATION_CODE
            End Get
            Set(ByVal value As String)
               _LOCATION_CODE = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_NAME_TH", DbType:="VarChar(255)")>  _
        Public Property LOCATION_NAME_TH() As  String 
            Get
                Return _LOCATION_NAME_TH
            End Get
            Set(ByVal value As  String )
               _LOCATION_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_NAME_EN", DbType:="VarChar(255)")>  _
        Public Property LOCATION_NAME_EN() As  String 
            Get
                Return _LOCATION_NAME_EN
            End Get
            Set(ByVal value As  String )
               _LOCATION_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_ABB", DbType:="VarChar(50)")>  _
        Public Property LOCATION_ABB() As  String 
            Get
                Return _LOCATION_ABB
            End Get
            Set(ByVal value As  String )
               _LOCATION_ABB = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_SEGMENT", DbType:="VarChar(255)")>  _
        Public Property LOCATION_SEGMENT() As  String 
            Get
                Return _LOCATION_SEGMENT
            End Get
            Set(ByVal value As  String )
               _LOCATION_SEGMENT = value
            End Set
        End Property 
        <Column(Storage:="_LOCATION_TYPE", DbType:="VarChar(50)")>  _
        Public Property LOCATION_TYPE() As  String 
            Get
                Return _LOCATION_TYPE
            End Get
            Set(ByVal value As  String )
               _LOCATION_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_PROVINCE_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PROVINCE_CODE() As String
            Get
                Return _PROVINCE_CODE
            End Get
            Set(ByVal value As String)
               _PROVINCE_CODE = value
            End Set
        End Property 
        <Column(Storage:="_REGION_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_CODE() As String
            Get
                Return _REGION_CODE
            End Get
            Set(ByVal value As String)
               _REGION_CODE = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACTIVE() As Char
            Get
                Return _ACTIVE
            End Get
            Set(ByVal value As Char)
               _ACTIVE = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _LOCATION_CODE = ""
            _LOCATION_NAME_TH = ""
            _LOCATION_NAME_EN = ""
            _LOCATION_ABB = ""
            _LOCATION_SEGMENT = ""
            _LOCATION_TYPE = ""
            _PROVINCE_CODE = ""
            _REGION_CODE = ""
            _ACTIVE = ""
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


        '/// Returns an indication whether the current data is inserted into TW_LOCATION table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_LOCATION table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_LOCATION table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TW_LOCATION table successfully.
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


        '/// Returns an indication whether the record of TW_LOCATION by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TW_LOCATION by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Integer, trans As SQLTransaction) As TwLocationCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TW_LOCATION by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Integer, trans As SQLTransaction) As TwLocationCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TW_LOCATION by specified LOCATION_CODE key is retrieved successfully.
        '/// <param name=cLOCATION_CODE>The LOCATION_CODE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByLOCATION_CODE(cLOCATION_CODE As String, trans As SQLTransaction) As Boolean
            Return doChkData("LOCATION_CODE = " & DB.SetString(cLOCATION_CODE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_LOCATION by specified LOCATION_CODE key is retrieved successfully.
        '/// <param name=cLOCATION_CODE>The LOCATION_CODE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByLOCATION_CODE(cLOCATION_CODE As String, cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("LOCATION_CODE = " & DB.SetString(cLOCATION_CODE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_LOCATION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TW_LOCATION table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_LOCATION table successfully.
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


        '/// Returns an indication whether the current data is deleted from TW_LOCATION table successfully.
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


        '/// Returns an indication whether the record of TW_LOCATION by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("location_code")) = False Then _location_code = Rdr("location_code").ToString()
                        If Convert.IsDBNull(Rdr("location_name_th")) = False Then _location_name_th = Rdr("location_name_th").ToString()
                        If Convert.IsDBNull(Rdr("location_name_en")) = False Then _location_name_en = Rdr("location_name_en").ToString()
                        If Convert.IsDBNull(Rdr("location_abb")) = False Then _location_abb = Rdr("location_abb").ToString()
                        If Convert.IsDBNull(Rdr("location_segment")) = False Then _location_segment = Rdr("location_segment").ToString()
                        If Convert.IsDBNull(Rdr("location_type")) = False Then _location_type = Rdr("location_type").ToString()
                        If Convert.IsDBNull(Rdr("province_code")) = False Then _province_code = Rdr("province_code").ToString()
                        If Convert.IsDBNull(Rdr("region_code")) = False Then _region_code = Rdr("region_code").ToString()
                        If Convert.IsDBNull(Rdr("active")) = False Then _active = Rdr("active").ToString()
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


        '/// Returns an indication whether the record of TW_LOCATION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TwLocationCenLinqDB
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
                        If Convert.IsDBNull(Rdr("location_code")) = False Then _location_code = Rdr("location_code").ToString()
                        If Convert.IsDBNull(Rdr("location_name_th")) = False Then _location_name_th = Rdr("location_name_th").ToString()
                        If Convert.IsDBNull(Rdr("location_name_en")) = False Then _location_name_en = Rdr("location_name_en").ToString()
                        If Convert.IsDBNull(Rdr("location_abb")) = False Then _location_abb = Rdr("location_abb").ToString()
                        If Convert.IsDBNull(Rdr("location_segment")) = False Then _location_segment = Rdr("location_segment").ToString()
                        If Convert.IsDBNull(Rdr("location_type")) = False Then _location_type = Rdr("location_type").ToString()
                        If Convert.IsDBNull(Rdr("province_code")) = False Then _province_code = Rdr("province_code").ToString()
                        If Convert.IsDBNull(Rdr("region_code")) = False Then _region_code = Rdr("region_code").ToString()
                        If Convert.IsDBNull(Rdr("active")) = False Then _active = Rdr("active").ToString()
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


        '/// Returns an indication whether the Class Data of TW_LOCATION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TwLocationCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TwLocationCenParaDB
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
                        If Convert.IsDBNull(Rdr("location_code")) = False Then ret.location_code = Rdr("location_code").ToString()
                        If Convert.IsDBNull(Rdr("location_name_th")) = False Then ret.location_name_th = Rdr("location_name_th").ToString()
                        If Convert.IsDBNull(Rdr("location_name_en")) = False Then ret.location_name_en = Rdr("location_name_en").ToString()
                        If Convert.IsDBNull(Rdr("location_abb")) = False Then ret.location_abb = Rdr("location_abb").ToString()
                        If Convert.IsDBNull(Rdr("location_segment")) = False Then ret.location_segment = Rdr("location_segment").ToString()
                        If Convert.IsDBNull(Rdr("location_type")) = False Then ret.location_type = Rdr("location_type").ToString()
                        If Convert.IsDBNull(Rdr("province_code")) = False Then ret.province_code = Rdr("province_code").ToString()
                        If Convert.IsDBNull(Rdr("region_code")) = False Then ret.region_code = Rdr("region_code").ToString()
                        If Convert.IsDBNull(Rdr("active")) = False Then ret.active = Rdr("active").ToString()

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


        'Get Insert Statement for table TW_LOCATION
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, LOCATION_CODE, LOCATION_NAME_TH, LOCATION_NAME_EN, LOCATION_ABB, LOCATION_SEGMENT, LOCATION_TYPE, PROVINCE_CODE, REGION_CODE, ACTIVE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_LOCATION_CODE) & ", "
                sql += DB.SetString(_LOCATION_NAME_TH) & ", "
                sql += DB.SetString(_LOCATION_NAME_EN) & ", "
                sql += DB.SetString(_LOCATION_ABB) & ", "
                sql += DB.SetString(_LOCATION_SEGMENT) & ", "
                sql += DB.SetString(_LOCATION_TYPE) & ", "
                sql += DB.SetString(_PROVINCE_CODE) & ", "
                sql += DB.SetString(_REGION_CODE) & ", "
                sql += DB.SetString(_ACTIVE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TW_LOCATION
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "LOCATION_CODE = " & DB.SetString(_LOCATION_CODE) & ", "
                Sql += "LOCATION_NAME_TH = " & DB.SetString(_LOCATION_NAME_TH) & ", "
                Sql += "LOCATION_NAME_EN = " & DB.SetString(_LOCATION_NAME_EN) & ", "
                Sql += "LOCATION_ABB = " & DB.SetString(_LOCATION_ABB) & ", "
                Sql += "LOCATION_SEGMENT = " & DB.SetString(_LOCATION_SEGMENT) & ", "
                Sql += "LOCATION_TYPE = " & DB.SetString(_LOCATION_TYPE) & ", "
                Sql += "PROVINCE_CODE = " & DB.SetString(_PROVINCE_CODE) & ", "
                Sql += "REGION_CODE = " & DB.SetString(_REGION_CODE) & ", "
                Sql += "ACTIVE = " & DB.SetString(_ACTIVE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TW_LOCATION
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TW_LOCATION
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, LOCATION_CODE, LOCATION_NAME_TH, LOCATION_NAME_EN, LOCATION_ABB, LOCATION_SEGMENT, LOCATION_TYPE, PROVINCE_CODE, REGION_CODE, ACTIVE FROM " & tableName
                Return Sql
            End Get
        End Property


            'Define Child Table 

       Dim _TW_FILTER_BRANCH_tw_location_id As DataTable
       Dim _TW_FILTER_DATA_tw_location_id As DataTable
       Dim _TW_FILTER_TEMP_TARGET_tw_location_id As DataTable

       Public Property CHILD_TW_FILTER_BRANCH_tw_location_id() As DataTable
           Get
               'Child Table Name : TW_FILTER_BRANCH Column :tw_location_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TwFilterBranchItem As New TwFilterBranchCenLinqDB
               _TW_FILTER_BRANCH_tw_location_id = TwFilterBranchItem.GetDataList("tw_location_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TwFilterBranchItem = Nothing
               Return _TW_FILTER_BRANCH_tw_location_id
           End Get
           Set(ByVal value As DataTable)
               _TW_FILTER_BRANCH_tw_location_id = value
           End Set
       End Property
       Public Property CHILD_TW_FILTER_DATA_tw_location_id() As DataTable
           Get
               'Child Table Name : TW_FILTER_DATA Column :tw_location_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TwFilterDataItem As New TwFilterDataCenLinqDB
               _TW_FILTER_DATA_tw_location_id = TwFilterDataItem.GetDataList("tw_location_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TwFilterDataItem = Nothing
               Return _TW_FILTER_DATA_tw_location_id
           End Get
           Set(ByVal value As DataTable)
               _TW_FILTER_DATA_tw_location_id = value
           End Set
       End Property
       Public Property CHILD_TW_FILTER_TEMP_TARGET_tw_location_id() As DataTable
           Get
               'Child Table Name : TW_FILTER_TEMP_TARGET Column :tw_location_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TwFilterTempTargetItem As New TwFilterTempTargetCenLinqDB
               _TW_FILTER_TEMP_TARGET_tw_location_id = TwFilterTempTargetItem.GetDataList("tw_location_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TwFilterTempTargetItem = Nothing
               Return _TW_FILTER_TEMP_TARGET_tw_location_id
           End Get
           Set(ByVal value As DataTable)
               _TW_FILTER_TEMP_TARGET_tw_location_id = value
           End Set
       End Property
    End Class
End Namespace
