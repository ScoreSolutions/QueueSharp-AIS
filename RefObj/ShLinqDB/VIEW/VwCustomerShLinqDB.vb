Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = ShLinqDB.Common.Utilities.SQLDB
Imports ShParaDB.VIEW
Imports ShParaDB.Common.Utilities

Namespace VIEW
    'Represents a transaction for VW_Customer view ShLinqDB.
    '[Create by  on March, 5 2012]
    Public Class VwCustomerShLinqDB
        Public sub VwCustomerShLinqDB()

        End Sub 
        ' VW_Customer
        Const _viewName As String = "VW_Customer"

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property ViewName As String
            Get
                Return _viewName
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
        Dim _TITLE_NAME As  String  = ""
        Dim _F_NAME As  String  = ""
        Dim _L_NAME As  String  = ""
        Dim _MOBILE_NO As  String  = ""
        Dim _MOBILE_STATUS As  String  = ""
        Dim _EMAIL As  String  = ""
        Dim _SEGMENT_LEVEL As  String  = ""
        Dim _BIRTH_DATE As  String  = ""
        Dim _CATEGORY As  String  = ""
        Dim _ACCOUNT_BALANCE As  String  = ""
        Dim _CONTACT_CLASS As  String  = ""
        Dim _SERVICE_YEAR As  String  = ""
        Dim _CHUM_SCORE As  String  = ""
        Dim _PREFER_LANG As  String  = ""
        Dim _CAMPAIGN_CODE As  String  = ""
        Dim _CAMPAIGN_NAME As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _CREATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _CAMPAIGN_NAME_EN As  String  = ""
        Dim _CAMPAIGN_DESC As  String  = ""
        Dim _CAMPAIGN_DESC_TH2 As  String  = ""
        Dim _CAMPAIGN_DESC_EN2 As  String  = ""

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
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(20)")>  _
        Public Property MOBILE_NO() As  String 
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As  String )
               _MOBILE_NO = value
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
        <Column(Storage:="_EMAIL", DbType:="VarChar(200)")>  _
        Public Property EMAIL() As  String 
            Get
                Return _EMAIL
            End Get
            Set(ByVal value As  String )
               _EMAIL = value
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
        <Column(Storage:="_PREFER_LANG", DbType:="VarChar(50)")>  _
        Public Property PREFER_LANG() As  String 
            Get
                Return _PREFER_LANG
            End Get
            Set(ByVal value As  String )
               _PREFER_LANG = value
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
        <Column(Storage:="_CAMPAIGN_NAME", DbType:="VarChar(255)")>  _
        Public Property CAMPAIGN_NAME() As  String 
            Get
                Return _CAMPAIGN_NAME
            End Get
            Set(ByVal value As  String )
               _CAMPAIGN_NAME = value
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
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime")>  _
        Public Property CREATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CREATE_DATE = value
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
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime")>  _
        Public Property UPDATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _UPDATE_DATE = value
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


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _TITLE_NAME = ""
            _F_NAME = ""
            _L_NAME = ""
            _MOBILE_NO = ""
            _MOBILE_STATUS = ""
            _EMAIL = ""
            _SEGMENT_LEVEL = ""
            _BIRTH_DATE = ""
            _CATEGORY = ""
            _ACCOUNT_BALANCE = ""
            _CONTACT_CLASS = ""
            _SERVICE_YEAR = ""
            _CHUM_SCORE = ""
            _PREFER_LANG = ""
            _CAMPAIGN_CODE = ""
            _CAMPAIGN_NAME = ""
            _NETWORK_TYPE = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _CREATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
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


        '/// Returns an indication whether the record of VW_Customer by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the record of VW_Customer by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("title_name")) = False Then _title_name = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then _f_name = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then _l_name = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then _mobile_status = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then _email = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then _segment_level = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then _birth_date = Rdr("birth_date").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then _account_balance = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then _service_year = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then _chum_score = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then _campaign_code = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then _campaign_name = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then _campaign_name_en = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then _campaign_desc = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then _campaign_desc_th2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then _campaign_desc_en2 = Rdr("campaign_desc_en2").ToString()
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


        '/// Returns an indication whether the record of VW_Customer by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As VwCustomerShLinqDB
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
                        If Convert.IsDBNull(Rdr("title_name")) = False Then _title_name = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then _f_name = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then _l_name = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then _mobile_status = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then _email = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then _segment_level = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then _birth_date = Rdr("birth_date").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then _category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then _account_balance = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then _contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then _service_year = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then _chum_score = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then _prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then _campaign_code = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then _campaign_name = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then _campaign_name_en = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then _campaign_desc = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then _campaign_desc_th2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then _campaign_desc_en2 = Rdr("campaign_desc_en2").ToString()

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


        '/// Returns an indication whether the Class Data of VW_Customer by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As VwCustomerShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New VwCustomerShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("title_name")) = False Then ret.title_name = Rdr("title_name").ToString()
                        If Convert.IsDBNull(Rdr("f_name")) = False Then ret.f_name = Rdr("f_name").ToString()
                        If Convert.IsDBNull(Rdr("l_name")) = False Then ret.l_name = Rdr("l_name").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("mobile_status")) = False Then ret.mobile_status = Rdr("mobile_status").ToString()
                        If Convert.IsDBNull(Rdr("email")) = False Then ret.email = Rdr("email").ToString()
                        If Convert.IsDBNull(Rdr("segment_level")) = False Then ret.segment_level = Rdr("segment_level").ToString()
                        If Convert.IsDBNull(Rdr("birth_date")) = False Then ret.birth_date = Rdr("birth_date").ToString()
                        If Convert.IsDBNull(Rdr("category")) = False Then ret.category = Rdr("category").ToString()
                        If Convert.IsDBNull(Rdr("account_balance")) = False Then ret.account_balance = Rdr("account_balance").ToString()
                        If Convert.IsDBNull(Rdr("contact_class")) = False Then ret.contact_class = Rdr("contact_class").ToString()
                        If Convert.IsDBNull(Rdr("service_year")) = False Then ret.service_year = Rdr("service_year").ToString()
                        If Convert.IsDBNull(Rdr("chum_score")) = False Then ret.chum_score = Rdr("chum_score").ToString()
                        If Convert.IsDBNull(Rdr("prefer_lang")) = False Then ret.prefer_lang = Rdr("prefer_lang").ToString()
                        If Convert.IsDBNull(Rdr("campaign_code")) = False Then ret.campaign_code = Rdr("campaign_code").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name")) = False Then ret.campaign_name = Rdr("campaign_name").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("campaign_name_en")) = False Then ret.campaign_name_en = Rdr("campaign_name_en").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc")) = False Then ret.campaign_desc = Rdr("campaign_desc").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_th2")) = False Then ret.campaign_desc_th2 = Rdr("campaign_desc_th2").ToString()
                        If Convert.IsDBNull(Rdr("campaign_desc_en2")) = False Then ret.campaign_desc_en2 = Rdr("campaign_desc_en2").ToString()

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


        'Get Select Statement for table VW_Customer
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & viewName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
