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
    'Represents a transaction for TB_CUSTOMER table CenLinqDB.
    '[Create by  on March, 13 2013]
    Public Class TbCustomerCenLinqDB
        Public sub TbCustomerCenLinqDB()

        End Sub 
        ' TB_CUSTOMER
        Const _tableName As String = "TB_CUSTOMER"
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
        Dim _CREATE_BY As  String  = "INITIAL"
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _MOBILE_NO As  String  = ""
        Dim _TITLE_NAME As  String  = ""
        Dim _F_NAME As  String  = ""
        Dim _L_NAME As  String  = ""
        Dim _EMAIL As  String  = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _SEGMENT_LEVEL As  String  = ""
        Dim _BIRTH_DATE As  String  = ""
        Dim _MOBILE_STATUS As  String  = ""
        Dim _CATEGORY As  String  = ""
        Dim _ACCOUNT_BALANCE As  String  = ""
        Dim _CONTACT_CLASS As  String  = ""
        Dim _SERVICE_YEAR As  String  = ""
        Dim _CHUM_SCORE As  String  = ""
        Dim _CAMPAIGN_CODE As  String  = ""
        Dim _CAMPAIGN_NAME As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _CAMPAIGN_NAME_EN As  String  = ""
        Dim _CAMPAIGN_DESC As  String  = ""
        Dim _CAMPAIGN_DESC_TH2 As  String  = ""
        Dim _CAMPAIGN_DESC_EN2 As  String  = ""
        Dim _CORPORATE_TYPE As  String  = ""

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
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50)")>  _
        Public Property MOBILE_NO() As  String 
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As  String )
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_TITLE_NAME", DbType:="VarChar(100)")>  _
        Public Property TITLE_NAME() As  String 
            Get
                Return _TITLE_NAME
            End Get
            Set(ByVal value As  String )
               _TITLE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_F_NAME", DbType:="VarChar(200)")>  _
        Public Property F_NAME() As  String 
            Get
                Return _F_NAME
            End Get
            Set(ByVal value As  String )
               _F_NAME = value
            End Set
        End Property 
        <Column(Storage:="_L_NAME", DbType:="VarChar(500)")>  _
        Public Property L_NAME() As  String 
            Get
                Return _L_NAME
            End Get
            Set(ByVal value As  String )
               _L_NAME = value
            End Set
        End Property 
        <Column(Storage:="_EMAIL", DbType:="VarChar(200)")>  _
        Public Property EMAIL() As  String 
            Get
                Return _EMAIL
            End Get
            Set(ByVal value As  String )
               _EMAIL = value
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
        <Column(Storage:="_SEGMENT_LEVEL", DbType:="VarChar(100)")>  _
        Public Property SEGMENT_LEVEL() As  String 
            Get
                Return _SEGMENT_LEVEL
            End Get
            Set(ByVal value As  String )
               _SEGMENT_LEVEL = value
            End Set
        End Property 
        <Column(Storage:="_BIRTH_DATE", DbType:="VarChar(100)")>  _
        Public Property BIRTH_DATE() As  String 
            Get
                Return _BIRTH_DATE
            End Get
            Set(ByVal value As  String )
               _BIRTH_DATE = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_STATUS", DbType:="VarChar(100)")>  _
        Public Property MOBILE_STATUS() As  String 
            Get
                Return _MOBILE_STATUS
            End Get
            Set(ByVal value As  String )
               _MOBILE_STATUS = value
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
        <Column(Storage:="_ACCOUNT_BALANCE", DbType:="VarChar(100)")>  _
        Public Property ACCOUNT_BALANCE() As  String 
            Get
                Return _ACCOUNT_BALANCE
            End Get
            Set(ByVal value As  String )
               _ACCOUNT_BALANCE = value
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
        <Column(Storage:="_SERVICE_YEAR", DbType:="VarChar(100)")>  _
        Public Property SERVICE_YEAR() As  String 
            Get
                Return _SERVICE_YEAR
            End Get
            Set(ByVal value As  String )
               _SERVICE_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_CHUM_SCORE", DbType:="VarChar(100)")>  _
        Public Property CHUM_SCORE() As  String 
            Get
                Return _CHUM_SCORE
            End Get
            Set(ByVal value As  String )
               _CHUM_SCORE = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_CODE", DbType:="VarChar(100)")>  _
        Public Property CAMPAIGN_CODE() As  String 
            Get
                Return _CAMPAIGN_CODE
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_CODE = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_NAME", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_NAME() As  String 
            Get
                Return _CAMPAIGN_NAME
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_NAME = value
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
        <Column(Storage:="_CAMPAIGN_NAME_EN", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_NAME_EN() As  String 
            Get
                Return _CAMPAIGN_NAME_EN
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_DESC", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_DESC() As  String 
            Get
                Return _CAMPAIGN_DESC
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_DESC = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_DESC_TH2", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_DESC_TH2() As  String 
            Get
                Return _CAMPAIGN_DESC_TH2
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_DESC_TH2 = value
            End Set
        End Property 
        <Column(Storage:="_CAMPAIGN_DESC_EN2", DbType:="VarChar(500)")>  _
        Public Property CAMPAIGN_DESC_EN2() As  String 
            Get
                Return _CAMPAIGN_DESC_EN2
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_DESC_EN2 = value
            End Set
        End Property 
        <Column(Storage:="_CORPORATE_TYPE", DbType:="VarChar(255)")>  _
        Public Property CORPORATE_TYPE() As  String 
            Get
                Return _CORPORATE_TYPE
            End Get
            Set(ByVal value As  String )
               _CORPORATE_TYPE = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _MOBILE_NO = ""
            _TITLE_NAME = ""
            _F_NAME = ""
            _L_NAME = ""
            _EMAIL = ""
            _PREFER_LANG = ""
            _SEGMENT_LEVEL = ""
            _BIRTH_DATE = ""
            _MOBILE_STATUS = ""
            _CATEGORY = ""
            _ACCOUNT_BALANCE = ""
            _CONTACT_CLASS = ""
            _SERVICE_YEAR = ""
            _CHUM_SCORE = ""
            _CAMPAIGN_CODE = ""
            _CAMPAIGN_NAME = ""
            _NETWORK_TYPE = ""
            _CAMPAIGN_NAME_EN = ""
            _CAMPAIGN_DESC = ""
            _CAMPAIGN_DESC_TH2 = ""
            _CAMPAIGN_DESC_EN2 = ""
            _CORPORATE_TYPE = ""
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


        '/// Returns an indication whether the current data is inserted into TB_CUSTOMER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMER table successfully.
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


        '/// Returns an indication whether the record of TB_CUSTOMER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_CUSTOMER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbCustomerCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_CUSTOMER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbCustomerCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function

        Public Function GetParameterByMobileNo(ByVal cMobileNo As String, ByVal trans As SqlTransaction) As TbCustomerCenParaDB
            Return doMappingParameter("mobile_no = " & DB.SetString(cMobileNo), trans)
        End Function

        '/// Returns an indication whether the record of TB_CUSTOMER by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMOBILE_NO(cMOBILE_NO As String, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TB_CUSTOMER by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMOBILE_NO(cMOBILE_NO As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_CUSTOMER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMER table successfully.
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


        '/// Returns an indication whether the record of TB_CUSTOMER by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("title_name")) = False Then _title_name = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then _f_name = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then _l_name = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then _email = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then _segment_level = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then _birth_date = Rdr("birth_date").ToString()
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then _mobile_status = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then _account_balance = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then _service_year = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then _chum_score = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then _campaign_code = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then _campaign_name = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then _campaign_name_en = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then _campaign_desc = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then _campaign_desc_th2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then _campaign_desc_en2 = Rdr("campaign_desc_en2").ToString()
                        If Convert.IsDBNull(Rdr("corporate_type")) = False Then _corporate_type = Rdr("corporate_type").ToString()
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


        '/// Returns an indication whether the record of TB_CUSTOMER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbCustomerCenLinqDB
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
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("title_name")) = False Then _title_name = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then _f_name = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then _l_name = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then _email = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then _segment_level = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then _birth_date = Rdr("birth_date").ToString()
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then _mobile_status = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then _account_balance = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then _service_year = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then _chum_score = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then _campaign_code = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then _campaign_name = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then _campaign_name_en = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then _campaign_desc = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then _campaign_desc_th2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then _campaign_desc_en2 = Rdr("campaign_desc_en2").ToString()
                        If Convert.IsDBNull(Rdr("corporate_type")) = False Then _corporate_type = Rdr("corporate_type").ToString()
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


        '/// Returns an indication whether the Class Data of TB_CUSTOMER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbCustomerCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbCustomerCenParaDB
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
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("title_name")) = False Then ret.title_name = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then ret.f_name = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then ret.l_name = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then ret.email = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then ret.prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then ret.segment_level = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then ret.birth_date = Rdr("birth_date").ToString()
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then ret.mobile_status = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then ret.category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then ret.account_balance = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then ret.contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then ret.service_year = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then ret.chum_score = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then ret.campaign_code = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then ret.campaign_name = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then ret.campaign_name_en = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then ret.campaign_desc = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then ret.campaign_desc_th2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then ret.campaign_desc_en2 = Rdr("campaign_desc_en2").ToString()
                        If Convert.IsDBNull(Rdr("corporate_type")) = False Then ret.corporate_type = Rdr("corporate_type").ToString()

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


        'Get Insert Statement for table TB_CUSTOMER
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, MOBILE_NO, TITLE_NAME, F_NAME, L_NAME, EMAIL, PREFER_LANG, SEGMENT_LEVEL, BIRTH_DATE, MOBILE_STATUS, CATEGORY, ACCOUNT_BALANCE, CONTACT_CLASS, SERVICE_YEAR, CHUM_SCORE, CAMPAIGN_CODE, CAMPAIGN_NAME, NETWORK_TYPE, CAMPAIGN_NAME_EN, CAMPAIGN_DESC, CAMPAIGN_DESC_TH2, CAMPAIGN_DESC_EN2, CORPORATE_TYPE)"
                Sql += " VALUES("
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetString(_TITLE_NAME) & ", "
                sql += DB.SetString(_F_NAME) & ", "
                sql += DB.SetString(_L_NAME) & ", "
                sql += DB.SetString(_EMAIL) & ", "
                sql += DB.SetString(_PREFER_LANG) & ", "
                sql += DB.SetString(_SEGMENT_LEVEL) & ", "
                sql += DB.SetString(_BIRTH_DATE) & ", "
                sql += DB.SetString(_MOBILE_STATUS) & ", "
                sql += DB.SetString(_CATEGORY) & ", "
                sql += DB.SetString(_ACCOUNT_BALANCE) & ", "
                sql += DB.SetString(_CONTACT_CLASS) & ", "
                sql += DB.SetString(_SERVICE_YEAR) & ", "
                sql += DB.SetString(_CHUM_SCORE) & ", "
                sql += DB.SetString(_CAMPAIGN_CODE) & ", "
                sql += DB.SetString(_CAMPAIGN_NAME) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_CAMPAIGN_NAME_EN) & ", "
                sql += DB.SetString(_CAMPAIGN_DESC) & ", "
                sql += DB.SetString(_CAMPAIGN_DESC_TH2) & ", "
                sql += DB.SetString(_CAMPAIGN_DESC_EN2) & ", "
                sql += DB.SetString(_CORPORATE_TYPE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_CUSTOMER
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "TITLE_NAME = " & DB.SetString(_TITLE_NAME) & ", "
                Sql += "F_NAME = " & DB.SetString(_F_NAME) & ", "
                Sql += "L_NAME = " & DB.SetString(_L_NAME) & ", "
                Sql += "EMAIL = " & DB.SetString(_EMAIL) & ", "
                Sql += "PREFER_LANG = " & DB.SetString(_PREFER_LANG) & ", "
                Sql += "SEGMENT_LEVEL = " & DB.SetString(_SEGMENT_LEVEL) & ", "
                Sql += "BIRTH_DATE = " & DB.SetString(_BIRTH_DATE) & ", "
                Sql += "MOBILE_STATUS = " & DB.SetString(_MOBILE_STATUS) & ", "
                Sql += "CATEGORY = " & DB.SetString(_CATEGORY) & ", "
                Sql += "ACCOUNT_BALANCE = " & DB.SetString(_ACCOUNT_BALANCE) & ", "
                Sql += "CONTACT_CLASS = " & DB.SetString(_CONTACT_CLASS) & ", "
                Sql += "SERVICE_YEAR = " & DB.SetString(_SERVICE_YEAR) & ", "
                Sql += "CHUM_SCORE = " & DB.SetString(_CHUM_SCORE) & ", "
                Sql += "CAMPAIGN_CODE = " & DB.SetString(_CAMPAIGN_CODE) & ", "
                Sql += "CAMPAIGN_NAME = " & DB.SetString(_CAMPAIGN_NAME) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "CAMPAIGN_NAME_EN = " & DB.SetString(_CAMPAIGN_NAME_EN) & ", "
                Sql += "CAMPAIGN_DESC = " & DB.SetString(_CAMPAIGN_DESC) & ", "
                Sql += "CAMPAIGN_DESC_TH2 = " & DB.SetString(_CAMPAIGN_DESC_TH2) & ", "
                Sql += "CAMPAIGN_DESC_EN2 = " & DB.SetString(_CAMPAIGN_DESC_EN2) & ", "
                Sql += "CORPORATE_TYPE = " & DB.SetString(_CORPORATE_TYPE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_CUSTOMER
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_CUSTOMER
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID,CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, MOBILE_NO, TITLE_NAME, F_NAME, L_NAME, EMAIL, PREFER_LANG, SEGMENT_LEVEL, BIRTH_DATE, MOBILE_STATUS, CATEGORY, ACCOUNT_BALANCE, CONTACT_CLASS, SERVICE_YEAR, CHUM_SCORE, CAMPAIGN_CODE, CAMPAIGN_NAME, NETWORK_TYPE, CAMPAIGN_NAME_EN, CAMPAIGN_DESC, CAMPAIGN_DESC_TH2, CAMPAIGN_DESC_EN2, CORPORATE_TYPE FROM " & TableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
