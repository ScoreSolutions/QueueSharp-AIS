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
    'Represents a transaction for TW_SOURCE_DATA table CenLinqDB.
    '[Create by  on November, 29 2013]
    Public Class TwSourceDataCenLinqDB
        Public sub TwSourceDataCenLinqDB()

        End Sub 
        ' TW_SOURCE_DATA
        Const _tableName As String = "TW_SOURCE_DATA"
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
        Dim _TW_SOURCE_TEXTFILE_ID As Long = 0
        Dim _ORDER_TYPE As String = ""
        Dim _COMPLETE_DATE As DateTime = New DateTime(1,1,1)
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _NATIONALITY As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _MOBILE_SEGMENT As  String  = ""
        Dim _LOCATION_CODE As String = ""
        Dim _LOCATION_NAME As String = ""
        Dim _PROVINCE_CODE As String = ""
        Dim _REGION_CODE As String = ""

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
        <Column(Storage:="_TW_SOURCE_TEXTFILE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_SOURCE_TEXTFILE_ID() As Long
            Get
                Return _TW_SOURCE_TEXTFILE_ID
            End Get
            Set(ByVal value As Long)
               _TW_SOURCE_TEXTFILE_ID = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_TYPE() As String
            Get
                Return _ORDER_TYPE
            End Get
            Set(ByVal value As String)
               _ORDER_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_COMPLETE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property COMPLETE_DATE() As DateTime
            Get
                Return _COMPLETE_DATE
            End Get
            Set(ByVal value As DateTime)
               _COMPLETE_DATE = value
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
        <Column(Storage:="_NATIONALITY", DbType:="VarChar(50)")>  _
        Public Property NATIONALITY() As  String 
            Get
                Return _NATIONALITY
            End Get
            Set(ByVal value As  String )
               _NATIONALITY = value
            End Set
        End Property 
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(50)")>  _
        Public Property NETWORK_TYPE() As  String 
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As  String )
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_SEGMENT", DbType:="VarChar(50)")>  _
        Public Property MOBILE_SEGMENT() As  String 
            Get
                Return _MOBILE_SEGMENT
            End Get
            Set(ByVal value As  String )
               _MOBILE_SEGMENT = value
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
        <Column(Storage:="_LOCATION_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOCATION_NAME() As String
            Get
                Return _LOCATION_NAME
            End Get
            Set(ByVal value As String)
               _LOCATION_NAME = value
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


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _TW_SOURCE_TEXTFILE_ID = 0
            _ORDER_TYPE = ""
            _COMPLETE_DATE = New DateTime(1,1,1)
            _MOBILE_NO = ""
            _CUSTOMER_NAME = ""
            _NATIONALITY = ""
            _NETWORK_TYPE = ""
            _MOBILE_SEGMENT = ""
            _LOCATION_CODE = ""
            _LOCATION_NAME = ""
            _PROVINCE_CODE = ""
            _REGION_CODE = ""
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


        '/// Returns an indication whether the current data is inserted into TW_SOURCE_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_SOURCE_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_SOURCE_DATA table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TW_SOURCE_DATA table successfully.
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


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TW_SOURCE_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TwSourceDataCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TW_SOURCE_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TwSourceDataCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified ORDER_TYPE key is retrieved successfully.
        '/// <param name=cORDER_TYPE>The ORDER_TYPE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByORDER_TYPE(cORDER_TYPE As String, trans As SQLTransaction) As Boolean
            Return doChkData("ORDER_TYPE = " & DB.SetString(cORDER_TYPE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_SOURCE_DATA by specified ORDER_TYPE key is retrieved successfully.
        '/// <param name=cORDER_TYPE>The ORDER_TYPE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByORDER_TYPE(cORDER_TYPE As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ORDER_TYPE = " & DB.SetString(cORDER_TYPE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified COMPLETE_DATE key is retrieved successfully.
        '/// <param name=cCOMPLETE_DATE>The COMPLETE_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByCOMPLETE_DATE(cCOMPLETE_DATE As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("COMPLETE_DATE = " & DB.SetDateTime(cCOMPLETE_DATE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_SOURCE_DATA by specified COMPLETE_DATE key is retrieved successfully.
        '/// <param name=cCOMPLETE_DATE>The COMPLETE_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByCOMPLETE_DATE(cCOMPLETE_DATE As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("COMPLETE_DATE = " & DB.SetDateTime(cCOMPLETE_DATE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMOBILE_NO(cMOBILE_NO As String, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_SOURCE_DATA by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMOBILE_NO(cMOBILE_NO As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified LOCATION_CODE key is retrieved successfully.
        '/// <param name=cLOCATION_CODE>The LOCATION_CODE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByLOCATION_CODE(cLOCATION_CODE As String, trans As SQLTransaction) As Boolean
            Return doChkData("LOCATION_CODE = " & DB.SetString(cLOCATION_CODE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_SOURCE_DATA by specified LOCATION_CODE key is retrieved successfully.
        '/// <param name=cLOCATION_CODE>The LOCATION_CODE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByLOCATION_CODE(cLOCATION_CODE As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("LOCATION_CODE = " & DB.SetString(cLOCATION_CODE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TW_SOURCE_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_SOURCE_DATA table successfully.
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


        '/// Returns an indication whether the current data is deleted from TW_SOURCE_DATA table successfully.
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


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("tw_source_textfile_id")) = False Then _tw_source_textfile_id = Convert.ToInt64(Rdr("tw_source_textfile_id"))
                        If Convert.IsDBNull(Rdr("order_type")) = False Then _order_type = Rdr("order_type").ToString()
                        If Convert.IsDBNull(Rdr("complete_date")) = False Then _complete_date = Convert.ToDateTime(Rdr("complete_date"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("nationality")) = False Then _nationality = Rdr("nationality").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("mobile_segment")) = False Then _mobile_segment = Rdr("mobile_segment").ToString()
                        If Convert.IsDBNull(Rdr("location_code")) = False Then _location_code = Rdr("location_code").ToString()
                        If Convert.IsDBNull(Rdr("location_name")) = False Then _location_name = Rdr("location_name").ToString()
                        If Convert.IsDBNull(Rdr("province_code")) = False Then _province_code = Rdr("province_code").ToString()
                        If Convert.IsDBNull(Rdr("region_code")) = False Then _region_code = Rdr("region_code").ToString()
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


        '/// Returns an indication whether the record of TW_SOURCE_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TwSourceDataCenLinqDB
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
                        If Convert.IsDBNull(Rdr("tw_source_textfile_id")) = False Then _tw_source_textfile_id = Convert.ToInt64(Rdr("tw_source_textfile_id"))
                        If Convert.IsDBNull(Rdr("order_type")) = False Then _order_type = Rdr("order_type").ToString()
                        If Convert.IsDBNull(Rdr("complete_date")) = False Then _complete_date = Convert.ToDateTime(Rdr("complete_date"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("nationality")) = False Then _nationality = Rdr("nationality").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("mobile_segment")) = False Then _mobile_segment = Rdr("mobile_segment").ToString()
                        If Convert.IsDBNull(Rdr("location_code")) = False Then _location_code = Rdr("location_code").ToString()
                        If Convert.IsDBNull(Rdr("location_name")) = False Then _location_name = Rdr("location_name").ToString()
                        If Convert.IsDBNull(Rdr("province_code")) = False Then _province_code = Rdr("province_code").ToString()
                        If Convert.IsDBNull(Rdr("region_code")) = False Then _region_code = Rdr("region_code").ToString()
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


        '/// Returns an indication whether the Class Data of TW_SOURCE_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TwSourceDataCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TwSourceDataCenParaDB
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
                        If Convert.IsDBNull(Rdr("tw_source_textfile_id")) = False Then ret.tw_source_textfile_id = Convert.ToInt64(Rdr("tw_source_textfile_id"))
                        If Convert.IsDBNull(Rdr("order_type")) = False Then ret.order_type = Rdr("order_type").ToString()
                        If Convert.IsDBNull(Rdr("complete_date")) = False Then ret.complete_date = Convert.ToDateTime(Rdr("complete_date"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then ret.customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("nationality")) = False Then ret.nationality = Rdr("nationality").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("mobile_segment")) = False Then ret.mobile_segment = Rdr("mobile_segment").ToString()
                        If Convert.IsDBNull(Rdr("location_code")) = False Then ret.location_code = Rdr("location_code").ToString()
                        If Convert.IsDBNull(Rdr("location_name")) = False Then ret.location_name = Rdr("location_name").ToString()
                        If Convert.IsDBNull(Rdr("province_code")) = False Then ret.province_code = Rdr("province_code").ToString()
                        If Convert.IsDBNull(Rdr("region_code")) = False Then ret.region_code = Rdr("region_code").ToString()

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


        'Get Insert Statement for table TW_SOURCE_DATA
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TW_SOURCE_TEXTFILE_ID, ORDER_TYPE, COMPLETE_DATE, MOBILE_NO, CUSTOMER_NAME, NATIONALITY, NETWORK_TYPE, MOBILE_SEGMENT, LOCATION_CODE, LOCATION_NAME, PROVINCE_CODE, REGION_CODE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_TW_SOURCE_TEXTFILE_ID) & ", "
                sql += DB.SetString(_ORDER_TYPE) & ", "
                sql += DB.SetDateTime(_COMPLETE_DATE) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetString(_CUSTOMER_NAME) & ", "
                sql += DB.SetString(_NATIONALITY) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_MOBILE_SEGMENT) & ", "
                sql += DB.SetString(_LOCATION_CODE) & ", "
                sql += DB.SetString(_LOCATION_NAME) & ", "
                sql += DB.SetString(_PROVINCE_CODE) & ", "
                sql += DB.SetString(_REGION_CODE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TW_SOURCE_DATA
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "TW_SOURCE_TEXTFILE_ID = " & DB.SetDouble(_TW_SOURCE_TEXTFILE_ID) & ", "
                Sql += "ORDER_TYPE = " & DB.SetString(_ORDER_TYPE) & ", "
                Sql += "COMPLETE_DATE = " & DB.SetDateTime(_COMPLETE_DATE) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "CUSTOMER_NAME = " & DB.SetString(_CUSTOMER_NAME) & ", "
                Sql += "NATIONALITY = " & DB.SetString(_NATIONALITY) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "MOBILE_SEGMENT = " & DB.SetString(_MOBILE_SEGMENT) & ", "
                Sql += "LOCATION_CODE = " & DB.SetString(_LOCATION_CODE) & ", "
                Sql += "LOCATION_NAME = " & DB.SetString(_LOCATION_NAME) & ", "
                Sql += "PROVINCE_CODE = " & DB.SetString(_PROVINCE_CODE) & ", "
                Sql += "REGION_CODE = " & DB.SetString(_REGION_CODE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TW_SOURCE_DATA
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TW_SOURCE_DATA
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TW_SOURCE_TEXTFILE_ID, ORDER_TYPE, COMPLETE_DATE, MOBILE_NO, CUSTOMER_NAME, NATIONALITY, NETWORK_TYPE, MOBILE_SEGMENT, LOCATION_CODE, LOCATION_NAME, PROVINCE_CODE, REGION_CODE FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
