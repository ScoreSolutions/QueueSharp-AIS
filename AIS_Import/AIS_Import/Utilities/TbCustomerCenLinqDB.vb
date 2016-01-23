Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports DB = AIS_Import.Utilities.SqlDB
'Imports CenParaDB.TABLE
'Imports CenParaDB.Common.Utilities

Namespace Utilities
    'Represents a transaction for TB_CUSTOMER table CenLinqDB.
    '[Create by  on January, 18 2012]
    Public Class TbCustomerCenLinqDB
        Public Sub TbCustomerCenLinqDB()

        End Sub
        ' TB_CUSTOMER
        Const _tableName As String = "TB_CUSTOMER"
        Dim _deletedRow As Int16 = 0

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage() As String
            Get
                Return _information
            End Get
        End Property
        Public ReadOnly Property HaveData() As Boolean
            Get
                Return _haveData
            End Get
        End Property


        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _UPDATE_BY As String = ""
        Dim _UPDATE_DATE As System.Nullable(Of DateTime) = New DateTime(1, 1, 1)
        Dim _MOBILE_NO As String = ""
        Dim _TITLE_NAME As String = ""
        Dim _F_NAME As String = ""
        Dim _L_NAME As String = ""
        Dim _EMAIL As String = ""
        Dim _PREFER_LANG As String = ""
        Dim _SEGMENT_LEVEL As String = ""
        Dim _BIRTH_DATE As String = ""
        Dim _MOBILE_STATUS As String = ""
        Dim _CATEGORY As String = ""
        Dim _ACCOUNT_BALANCE As String = ""
        Dim _CONTACT_CLASS As String = ""
        Dim _SERVICE_YEAR As String = ""
        Dim _CHUM_SCORE As String = ""
        Dim _CAMPAIGN_CODE As String = ""
        Dim _CAMPAIGN_NAME As String = ""
        Dim _NETWORK_TYPE As String = ""
        Dim _CAMPAIGN_NAME_EN As String = ""
        Dim _CAMPAIGN_DESC As String = ""
        Dim _CAMPAIGN_DESC_TH2 As String = ""
        Dim _CAMPAIGN_DESC_EN2 As String = ""

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ", CanBeNull:=False)> _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
                _ID = value
            End Set
        End Property
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20) NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
                _CREATE_BY = value
            End Set
        End Property
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(20)")> _
        Public Property UPDATE_BY() As String
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As String)
                _UPDATE_BY = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime")> _
        Public Property UPDATE_DATE() As System.Nullable(Of DateTime)
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _UPDATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(20)")> _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
                _MOBILE_NO = value
            End Set
        End Property
        <Column(Storage:="_TITLE_NAME", DbType:="VarChar(100)")> _
        Public Property TITLE_NAME() As String
            Get
                Return _TITLE_NAME
            End Get
            Set(ByVal value As String)
                _TITLE_NAME = value
            End Set
        End Property
        <Column(Storage:="_F_NAME", DbType:="VarChar(200)")> _
        Public Property F_NAME() As String
            Get
                Return _F_NAME
            End Get
            Set(ByVal value As String)
                _F_NAME = value
            End Set
        End Property
        <Column(Storage:="_L_NAME", DbType:="VarChar(500)")> _
        Public Property L_NAME() As String
            Get
                Return _L_NAME
            End Get
            Set(ByVal value As String)
                _L_NAME = value
            End Set
        End Property
        <Column(Storage:="_EMAIL", DbType:="VarChar(200)")> _
        Public Property EMAIL() As String
            Get
                Return _EMAIL
            End Get
            Set(ByVal value As String)
                _EMAIL = value
            End Set
        End Property
        <Column(Storage:="_PREFER_LANG", DbType:="VarChar(100)")> _
        Public Property PREFER_LANG() As String
            Get
                Return _PREFER_LANG
            End Get
            Set(ByVal value As String)
                _PREFER_LANG = value
            End Set
        End Property
        <Column(Storage:="_SEGMENT_LEVEL", DbType:="VarChar(100)")> _
        Public Property SEGMENT_LEVEL() As String
            Get
                Return _SEGMENT_LEVEL
            End Get
            Set(ByVal value As String)
                _SEGMENT_LEVEL = value
            End Set
        End Property
        <Column(Storage:="_BIRTH_DATE", DbType:="VarChar(100)")> _
        Public Property BIRTH_DATE() As String
            Get
                Return _BIRTH_DATE
            End Get
            Set(ByVal value As String)
                _BIRTH_DATE = value
            End Set
        End Property
        <Column(Storage:="_MOBILE_STATUS", DbType:="VarChar(100)")> _
        Public Property MOBILE_STATUS() As String
            Get
                Return _MOBILE_STATUS
            End Get
            Set(ByVal value As String)
                _MOBILE_STATUS = value
            End Set
        End Property
        <Column(Storage:="_CATEGORY", DbType:="VarChar(100)")> _
        Public Property CATEGORY() As String
            Get
                Return _CATEGORY
            End Get
            Set(ByVal value As String)
                _CATEGORY = value
            End Set
        End Property
        <Column(Storage:="_ACCOUNT_BALANCE", DbType:="VarChar(100)")> _
        Public Property ACCOUNT_BALANCE() As String
            Get
                Return _ACCOUNT_BALANCE
            End Get
            Set(ByVal value As String)
                _ACCOUNT_BALANCE = value
            End Set
        End Property
        <Column(Storage:="_CONTACT_CLASS", DbType:="VarChar(100)")> _
        Public Property CONTACT_CLASS() As String
            Get
                Return _CONTACT_CLASS
            End Get
            Set(ByVal value As String)
                _CONTACT_CLASS = value
            End Set
        End Property
        <Column(Storage:="_SERVICE_YEAR", DbType:="VarChar(100)")> _
        Public Property SERVICE_YEAR() As String
            Get
                Return _SERVICE_YEAR
            End Get
            Set(ByVal value As String)
                _SERVICE_YEAR = value
            End Set
        End Property
        <Column(Storage:="_CHUM_SCORE", DbType:="VarChar(100)")> _
        Public Property CHUM_SCORE() As String
            Get
                Return _CHUM_SCORE
            End Get
            Set(ByVal value As String)
                _CHUM_SCORE = value
            End Set
        End Property
        <Column(Storage:="_CAMPAIGN_CODE", DbType:="VarChar(100)")> _
        Public Property CAMPAIGN_CODE() As String
            Get
                Return _CAMPAIGN_CODE
            End Get
            Set(ByVal value As String)
                _CAMPAIGN_CODE = value
            End Set
        End Property
        <Column(Storage:="_CAMPAIGN_NAME", DbType:="VarChar(200)")> _
        Public Property CAMPAIGN_NAME() As String
            Get
                Return _CAMPAIGN_NAME
            End Get
            Set(ByVal value As String)
                _CAMPAIGN_NAME = value
            End Set
        End Property
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(100)")> _
        Public Property NETWORK_TYPE() As String
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As String)
                _NETWORK_TYPE = value
            End Set
        End Property

        Public Property CAMPAIGN_NAME_EN() As String
            Get
                Return _CAMPAIGN_NAME_EN
            End Get
            Set(ByVal value As String)
                _CAMPAIGN_NAME_EN = value
            End Set
        End Property

        Public Property CAMPAIGN_DESC() As String
            Get
                Return _CAMPAIGN_DESC
            End Get
            Set(ByVal value As String)
                _CAMPAIGN_DESC = value
            End Set
        End Property

        Public Property CAMPAIGN_DESC_TH2() As String
            Get
                Return _CAMPAIGN_DESC_TH2
            End Get
            Set(ByVal value As String)
                _CAMPAIGN_DESC_TH2 = value
            End Set
        End Property

        Public Property CAMPAIGN_DESC_EN2() As String
            Get
                Return _CAMPAIGN_DESC_EN2
            End Get
            Set(ByVal value As String)
                _CAMPAIGN_DESC_EN2 = value
            End Set
        End Property

        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1, 1, 1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1, 1, 1)
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
        End Sub

        'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(ByVal whClause As String, ByVal orderBy As String, ByVal trans As SqlTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIf(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(ByVal Sql As String, ByVal trans As SqlTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into TB_CUSTOMER table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(ByVal LoginName As String, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                _ID = DB.GetNextID("id", TableName, trans)
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
        Public Function UpdateByPK(ByVal LoginName As String, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("id = " & DB.SetDouble(_ID) & " ", trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(ByVal Sql As String, ByVal trans As SqlTransaction) As Boolean
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
        Public Function DeleteByPK(ByVal cPK As Long, ByVal trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                Return doDelete("id = " & cPK, trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the record of TB_CUSTOMER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(ByVal cid As Long, ByVal trans As SqlTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_CUSTOMER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(ByVal cid As Long, ByVal trans As SqlTransaction) As TbCustomerCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function

        Public Function GetDataByMobileNo(ByVal cMObileNo As String, ByVal trans As SqlTransaction) As TbCustomerCenLinqDB
            Return doGetData("mobile_no = " & DB.SetString(cMObileNo) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_CUSTOMER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        'Public Function GetParameter(ByVal cid As Long, ByVal trans As SqlTransaction) As TbCustomerCenParaDB
        '    Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        'End Function


        '/// Returns an indication whether the record of TB_CUSTOMER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_CUSTOMER table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = False Then
                Try
                    'DB.CreateTextFile(SqlInsert)
                    ret = (DB.ExecuteNonQuery(SqlInsert, trans) > 0)
                    If ret = False Then
                        _error = MessageResources.MSGEN001
                    Else
                        _haveData = True
                    End If
                    _information = MessageResources.MSGIN001
                Catch ex As ApplicationException
                    ret = False
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


        '/// Returns an indication whether the current data is updated to TB_CUSTOMER table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " Where " & whText
                Try

                    ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans) > 0)
                    If ret = False Then
                        _error = MessageResources.MSGEU001
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
            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TB_CUSTOMER table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> "" Then
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


        '/// Returns an indication whether the record of TB_CUSTOMER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(ByVal whText As String, ByVal trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            ClearData()
            _haveData = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SqlDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _ID = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _CREATE_BY = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _CREATE_DATE = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _UPDATE_BY = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _UPDATE_DATE = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _MOBILE_NO = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("title_name")) = False Then _TITLE_NAME = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then _F_NAME = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then _L_NAME = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then _EMAIL = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _PREFER_LANG = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then _SEGMENT_LEVEL = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then _BIRTH_DATE = Convert.ToString(Rdr("birth_date"))
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then _MOBILE_STATUS = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _CATEGORY = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then _ACCOUNT_BALANCE = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _CONTACT_CLASS = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then _SERVICE_YEAR = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then _CHUM_SCORE = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then _CAMPAIGN_CODE = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then _CAMPAIGN_NAME = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _NETWORK_TYPE = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then _CAMPAIGN_NAME_EN = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then _CAMPAIGN_DESC = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then _CAMPAIGN_DESC_TH2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then _CAMPAIGN_DESC_EN2 = Rdr("campaign_desc_en2").ToString()
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed = False Then
                        Rdr.Close()
                    End If
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
        Private Function doGetData(ByVal whText As String, ByVal trans As SqlTransaction) As TbCustomerCenLinqDB
            ClearData()
            _haveData = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SqlDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _ID = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _CREATE_BY = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _CREATE_DATE = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _UPDATE_BY = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _UPDATE_DATE = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _MOBILE_NO = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("title_name")) = False Then _TITLE_NAME = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then _F_NAME = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then _L_NAME = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then _EMAIL = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _PREFER_LANG = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then _SEGMENT_LEVEL = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then _BIRTH_DATE = Rdr("birth_date")
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then _MOBILE_STATUS = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _CATEGORY = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then _ACCOUNT_BALANCE = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _CONTACT_CLASS = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then _SERVICE_YEAR = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then _CHUM_SCORE = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then _CAMPAIGN_CODE = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then _CAMPAIGN_NAME = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _NETWORK_TYPE = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then _CAMPAIGN_NAME_EN = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then _CAMPAIGN_DESC = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then _CAMPAIGN_DESC_TH2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then _CAMPAIGN_DESC_EN2 = Rdr("campaign_desc_en2").ToString()

                        'Generate Item For Child Table
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104
                    If Rdr IsNot Nothing And Rdr.IsClosed = False Then
                        Rdr.Close()
                    End If
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function

        ' SQL Statements


        'Get Insert Statement for table TB_CUSTOMER
        Private ReadOnly Property SqlInsert() As String
            Get
                Dim Sql As String = ""
                Sql += "INSERT INTO " & TableName & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, MOBILE_NO, TITLE_NAME, F_NAME, L_NAME, EMAIL, PREFER_LANG, SEGMENT_LEVEL, BIRTH_DATE, MOBILE_STATUS, CATEGORY, ACCOUNT_BALANCE, CONTACT_CLASS, SERVICE_YEAR, CHUM_SCORE, CAMPAIGN_CODE, CAMPAIGN_NAME, NETWORK_TYPE, CAMPAIGN_NAME_EN, CAMPAIGN_DESC, CAMPAIGN_DESC_TH2, CAMPAIGN_DESC_EN2)"
                Sql += " VALUES("
                Sql += DB.SetDouble(_ID) & ", "
                Sql += DB.SetString(_CREATE_BY) & ", "
                Sql += DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += DB.SetString(_UPDATE_BY) & ", "
                Sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += DB.SetString(_MOBILE_NO) & ", "
                Sql += DB.SetString(_TITLE_NAME) & ", "
                Sql += DB.SetString(_F_NAME) & ", "
                Sql += DB.SetString(_L_NAME) & ", "
                Sql += DB.SetString(_EMAIL) & ", "
                Sql += DB.SetString(_PREFER_LANG) & ", "
                Sql += DB.SetString(_SEGMENT_LEVEL) & ", "
                Sql += DB.SetString(_BIRTH_DATE) & ", "
                Sql += DB.SetString(_MOBILE_STATUS) & ", "
                Sql += DB.SetString(_CATEGORY) & ", "
                Sql += DB.SetString(_ACCOUNT_BALANCE) & ", "
                Sql += DB.SetString(_CONTACT_CLASS) & ", "
                Sql += DB.SetString(_SERVICE_YEAR) & ", "
                Sql += DB.SetString(_CHUM_SCORE) & ", "
                Sql += DB.SetString(_CAMPAIGN_CODE) & ", "
                Sql += DB.SetString(_CAMPAIGN_NAME) & ", "
                Sql += DB.SetString(_NETWORK_TYPE) & ", "
                Sql += DB.SetString(_CAMPAIGN_NAME_EN) & ", "
                Sql += DB.SetString(_CAMPAIGN_DESC) & ", "
                Sql += DB.SetString(_CAMPAIGN_DESC_TH2) & ", "
                Sql += DB.SetString(_CAMPAIGN_DESC_EN2) & ""
                Sql += ")"
                Return Sql
            End Get
        End Property


        'Get update statement form table TB_CUSTOMER
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & TableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
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
                Sql += "CAMPAIGN_DESC_EN2 = " & DB.SetString(_CAMPAIGN_DESC_EN2) & ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_CUSTOMER
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & TableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_CUSTOMER
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & TableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace

