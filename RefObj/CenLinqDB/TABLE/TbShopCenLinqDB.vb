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
    'Represents a transaction for TB_SHOP table CenLinqDB.
    '[Create by  on December, 27 2012]
    Public Class TbShopCenLinqDB
        Public sub TbShopCenLinqDB()

        End Sub 
        ' TB_SHOP
        Const _tableName As String = "TB_SHOP"
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
        Dim _SHOP_CODE As String = ""
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As  String  = ""
        Dim _SHOP_ABB As  String  = ""
        Dim _REGION_ID As Long = 0
        Dim _BUILDING_ID As  System.Nullable(Of Long)  = 0
        Dim _OPEN_TIME As String = ""
        Dim _CLOSE_TIME As String = ""
        Dim _SHOP_DB_NAME As String = ""
        Dim _SHOP_DB_USERID As String = ""
        Dim _SHOP_DB_PWD As String = ""
        Dim _SHOP_DB_SERVER As String = ""
        Dim _SHOP_DR_NAME As String = ""
        Dim _SHOP_DR_USERID As String = ""
        Dim _SHOP_DR_PWD As String = ""
        Dim _SHOP_DR_SERVER As String = ""
        Dim _SHOP_USE_QM As Char = "Y"
        Dim _SHOP_QM_URL As String = ""
        Dim _ACTIVE As Char = "Y"
        Dim _QM_FTP_USERNAME As  String  = ""
        Dim _QM_FTP_PASSWORD As  String  = ""
        Dim _SHOP_SIZE As  System.Nullable(Of Char)  = ""
        Dim _MAIN_SERVERNAME As  String  = ""
        Dim _MAIN_DB_NAME As  String  = ""
        Dim _MAIN_DB_USERID As  String  = ""
        Dim _MAIN_DB_PWD As  String  = ""

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
        <Column(Storage:="_SHOP_CODE", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_CODE() As String
            Get
                Return _SHOP_CODE
            End Get
            Set(ByVal value As String)
               _SHOP_CODE = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255)")>  _
        Public Property SHOP_NAME_EN() As  String 
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As  String )
               _SHOP_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_ABB", DbType:="VarChar(50)")>  _
        Public Property SHOP_ABB() As  String 
            Get
                Return _SHOP_ABB
            End Get
            Set(ByVal value As  String )
               _SHOP_ABB = value
            End Set
        End Property 
        <Column(Storage:="_REGION_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_ID() As Long
            Get
                Return _REGION_ID
            End Get
            Set(ByVal value As Long)
               _REGION_ID = value
            End Set
        End Property 
        <Column(Storage:="_BUILDING_ID", DbType:="Int")>  _
        Public Property BUILDING_ID() As  System.Nullable(Of Long) 
            Get
                Return _BUILDING_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BUILDING_ID = value
            End Set
        End Property 
        <Column(Storage:="_OPEN_TIME", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property OPEN_TIME() As String
            Get
                Return _OPEN_TIME
            End Get
            Set(ByVal value As String)
               _OPEN_TIME = value
            End Set
        End Property 
        <Column(Storage:="_CLOSE_TIME", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property CLOSE_TIME() As String
            Get
                Return _CLOSE_TIME
            End Get
            Set(ByVal value As String)
               _CLOSE_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_NAME", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_NAME() As String
            Get
                Return _SHOP_DB_NAME
            End Get
            Set(ByVal value As String)
               _SHOP_DB_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_USERID", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_USERID() As String
            Get
                Return _SHOP_DB_USERID
            End Get
            Set(ByVal value As String)
               _SHOP_DB_USERID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_PWD", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_PWD() As String
            Get
                Return _SHOP_DB_PWD
            End Get
            Set(ByVal value As String)
               _SHOP_DB_PWD = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DB_SERVER", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DB_SERVER() As String
            Get
                Return _SHOP_DB_SERVER
            End Get
            Set(ByVal value As String)
               _SHOP_DB_SERVER = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_NAME", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_NAME() As String
            Get
                Return _SHOP_DR_NAME
            End Get
            Set(ByVal value As String)
               _SHOP_DR_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_USERID", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_USERID() As String
            Get
                Return _SHOP_DR_USERID
            End Get
            Set(ByVal value As String)
               _SHOP_DR_USERID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_PWD", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_PWD() As String
            Get
                Return _SHOP_DR_PWD
            End Get
            Set(ByVal value As String)
               _SHOP_DR_PWD = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_DR_SERVER", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_DR_SERVER() As String
            Get
                Return _SHOP_DR_SERVER
            End Get
            Set(ByVal value As String)
               _SHOP_DR_SERVER = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_USE_QM", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_USE_QM() As Char
            Get
                Return _SHOP_USE_QM
            End Get
            Set(ByVal value As Char)
               _SHOP_USE_QM = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_QM_URL", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_QM_URL() As String
            Get
                Return _SHOP_QM_URL
            End Get
            Set(ByVal value As String)
               _SHOP_QM_URL = value
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
        <Column(Storage:="_QM_FTP_USERNAME", DbType:="VarChar(100)")>  _
        Public Property QM_FTP_USERNAME() As  String 
            Get
                Return _QM_FTP_USERNAME
            End Get
            Set(ByVal value As  String )
               _QM_FTP_USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_QM_FTP_PASSWORD", DbType:="VarChar(100)")>  _
        Public Property QM_FTP_PASSWORD() As  String 
            Get
                Return _QM_FTP_PASSWORD
            End Get
            Set(ByVal value As  String )
               _QM_FTP_PASSWORD = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_SIZE", DbType:="Char(1)")>  _
        Public Property SHOP_SIZE() As  System.Nullable(Of Char) 
            Get
                Return _SHOP_SIZE
            End Get
            Set(ByVal value As  System.Nullable(Of Char) )
               _SHOP_SIZE = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_SERVERNAME", DbType:="VarChar(100)")>  _
        Public Property MAIN_SERVERNAME() As  String 
            Get
                Return _MAIN_SERVERNAME
            End Get
            Set(ByVal value As  String )
               _MAIN_SERVERNAME = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_DB_NAME", DbType:="VarChar(100)")>  _
        Public Property MAIN_DB_NAME() As  String 
            Get
                Return _MAIN_DB_NAME
            End Get
            Set(ByVal value As  String )
               _MAIN_DB_NAME = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_DB_USERID", DbType:="VarChar(100)")>  _
        Public Property MAIN_DB_USERID() As  String 
            Get
                Return _MAIN_DB_USERID
            End Get
            Set(ByVal value As  String )
               _MAIN_DB_USERID = value
            End Set
        End Property 
        <Column(Storage:="_MAIN_DB_PWD", DbType:="VarChar(500)")>  _
        Public Property MAIN_DB_PWD() As  String 
            Get
                Return _MAIN_DB_PWD
            End Get
            Set(ByVal value As  String )
               _MAIN_DB_PWD = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _SHOP_CODE = ""
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _SHOP_ABB = ""
            _REGION_ID = 0
            _BUILDING_ID = 0
            _OPEN_TIME = ""
            _CLOSE_TIME = ""
            _SHOP_DB_NAME = ""
            _SHOP_DB_USERID = ""
            _SHOP_DB_PWD = ""
            _SHOP_DB_SERVER = ""
            _SHOP_DR_NAME = ""
            _SHOP_DR_USERID = ""
            _SHOP_DR_PWD = ""
            _SHOP_DR_SERVER = ""
            _SHOP_USE_QM = ""
            _SHOP_QM_URL = ""
            _ACTIVE = ""
            _QM_FTP_USERNAME = ""
            _QM_FTP_PASSWORD = ""
            _SHOP_SIZE = ""
            _MAIN_SERVERNAME = ""
            _MAIN_DB_NAME = ""
            _MAIN_DB_USERID = ""
            _MAIN_DB_PWD = ""
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


        '/// Returns an indication whether the current data is inserted into TB_SHOP table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_SHOP table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_SHOP table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_SHOP table successfully.
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


        '/// Returns an indication whether the record of TB_SHOP by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_SHOP by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Integer, trans As SQLTransaction) As TbShopCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_SHOP by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Integer, trans As SQLTransaction) As TbShopCenParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_SHOP by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_SHOP table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_SHOP table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_SHOP table successfully.
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


        '/// Returns an indication whether the record of TB_SHOP by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("shop_code")) = False Then _shop_code = Rdr("shop_code").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("shop_abb")) = False Then _shop_abb = Rdr("shop_abb").ToString()
                        If Convert.IsDBNull(Rdr("region_id")) = False Then _region_id = Convert.ToInt32(Rdr("region_id"))
                        If Convert.IsDBNull(Rdr("building_id")) = False Then _building_id = Convert.ToInt32(Rdr("building_id"))
                        If Convert.IsDBNull(Rdr("open_time")) = False Then _open_time = Rdr("open_time").ToString()
                        If Convert.IsDBNull(Rdr("close_time")) = False Then _close_time = Rdr("close_time").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_name")) = False Then _shop_db_name = Rdr("shop_db_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_userid")) = False Then _shop_db_userid = Rdr("shop_db_userid").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_pwd")) = False Then _shop_db_pwd = Rdr("shop_db_pwd").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_server")) = False Then _shop_db_server = Rdr("shop_db_server").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_name")) = False Then _shop_dr_name = Rdr("shop_dr_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_userid")) = False Then _shop_dr_userid = Rdr("shop_dr_userid").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_pwd")) = False Then _shop_dr_pwd = Rdr("shop_dr_pwd").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_server")) = False Then _shop_dr_server = Rdr("shop_dr_server").ToString()
                        If Convert.IsDBNull(Rdr("shop_use_qm")) = False Then _shop_use_qm = Rdr("shop_use_qm").ToString()
                        If Convert.IsDBNull(Rdr("shop_qm_url")) = False Then _shop_qm_url = Rdr("shop_qm_url").ToString()
                        If Convert.IsDBNull(Rdr("active")) = False Then _active = Rdr("active").ToString()
                        If Convert.IsDBNull(Rdr("qm_ftp_username")) = False Then _qm_ftp_username = Rdr("qm_ftp_username").ToString()
                        If Convert.IsDBNull(Rdr("qm_ftp_password")) = False Then _qm_ftp_password = Rdr("qm_ftp_password").ToString()
                        If Convert.IsDBNull(Rdr("shop_size")) = False Then _shop_size = Rdr("shop_size").ToString()
                        If Convert.IsDBNull(Rdr("main_servername")) = False Then _main_servername = Rdr("main_servername").ToString()
                        If Convert.IsDBNull(Rdr("main_db_name")) = False Then _main_db_name = Rdr("main_db_name").ToString()
                        If Convert.IsDBNull(Rdr("main_db_userid")) = False Then _main_db_userid = Rdr("main_db_userid").ToString()
                        If Convert.IsDBNull(Rdr("main_db_pwd")) = False Then _main_db_pwd = Rdr("main_db_pwd").ToString()
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


        '/// Returns an indication whether the record of TB_SHOP by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbShopCenLinqDB
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
                        If Convert.IsDBNull(Rdr("shop_code")) = False Then _shop_code = Rdr("shop_code").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("shop_abb")) = False Then _shop_abb = Rdr("shop_abb").ToString()
                        If Convert.IsDBNull(Rdr("region_id")) = False Then _region_id = Convert.ToInt32(Rdr("region_id"))
                        If Convert.IsDBNull(Rdr("building_id")) = False Then _building_id = Convert.ToInt32(Rdr("building_id"))
                        If Convert.IsDBNull(Rdr("open_time")) = False Then _open_time = Rdr("open_time").ToString()
                        If Convert.IsDBNull(Rdr("close_time")) = False Then _close_time = Rdr("close_time").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_name")) = False Then _shop_db_name = Rdr("shop_db_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_userid")) = False Then _shop_db_userid = Rdr("shop_db_userid").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_pwd")) = False Then _shop_db_pwd = Rdr("shop_db_pwd").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_server")) = False Then _shop_db_server = Rdr("shop_db_server").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_name")) = False Then _shop_dr_name = Rdr("shop_dr_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_userid")) = False Then _shop_dr_userid = Rdr("shop_dr_userid").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_pwd")) = False Then _shop_dr_pwd = Rdr("shop_dr_pwd").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_server")) = False Then _shop_dr_server = Rdr("shop_dr_server").ToString()
                        If Convert.IsDBNull(Rdr("shop_use_qm")) = False Then _shop_use_qm = Rdr("shop_use_qm").ToString()
                        If Convert.IsDBNull(Rdr("shop_qm_url")) = False Then _shop_qm_url = Rdr("shop_qm_url").ToString()
                        If Convert.IsDBNull(Rdr("active")) = False Then _active = Rdr("active").ToString()
                        If Convert.IsDBNull(Rdr("qm_ftp_username")) = False Then _qm_ftp_username = Rdr("qm_ftp_username").ToString()
                        If Convert.IsDBNull(Rdr("qm_ftp_password")) = False Then _qm_ftp_password = Rdr("qm_ftp_password").ToString()
                        If Convert.IsDBNull(Rdr("shop_size")) = False Then _shop_size = Rdr("shop_size").ToString()
                        If Convert.IsDBNull(Rdr("main_servername")) = False Then _main_servername = Rdr("main_servername").ToString()
                        If Convert.IsDBNull(Rdr("main_db_name")) = False Then _main_db_name = Rdr("main_db_name").ToString()
                        If Convert.IsDBNull(Rdr("main_db_userid")) = False Then _main_db_userid = Rdr("main_db_userid").ToString()
                        If Convert.IsDBNull(Rdr("main_db_pwd")) = False Then _main_db_pwd = Rdr("main_db_pwd").ToString()
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


        '/// Returns an indication whether the Class Data of TB_SHOP by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbShopCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbShopCenParaDB
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
                        If Convert.IsDBNull(Rdr("shop_code")) = False Then ret.shop_code = Rdr("shop_code").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then ret.shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then ret.shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("shop_abb")) = False Then ret.shop_abb = Rdr("shop_abb").ToString()
                        If Convert.IsDBNull(Rdr("region_id")) = False Then ret.region_id = Convert.ToInt32(Rdr("region_id"))
                        If Convert.IsDBNull(Rdr("building_id")) = False Then ret.building_id = Convert.ToInt32(Rdr("building_id"))
                        If Convert.IsDBNull(Rdr("open_time")) = False Then ret.open_time = Rdr("open_time").ToString()
                        If Convert.IsDBNull(Rdr("close_time")) = False Then ret.close_time = Rdr("close_time").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_name")) = False Then ret.shop_db_name = Rdr("shop_db_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_userid")) = False Then ret.shop_db_userid = Rdr("shop_db_userid").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_pwd")) = False Then ret.shop_db_pwd = Rdr("shop_db_pwd").ToString()
                        If Convert.IsDBNull(Rdr("shop_db_server")) = False Then ret.shop_db_server = Rdr("shop_db_server").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_name")) = False Then ret.shop_dr_name = Rdr("shop_dr_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_userid")) = False Then ret.shop_dr_userid = Rdr("shop_dr_userid").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_pwd")) = False Then ret.shop_dr_pwd = Rdr("shop_dr_pwd").ToString()
                        If Convert.IsDBNull(Rdr("shop_dr_server")) = False Then ret.shop_dr_server = Rdr("shop_dr_server").ToString()
                        If Convert.IsDBNull(Rdr("shop_use_qm")) = False Then ret.shop_use_qm = Rdr("shop_use_qm").ToString()
                        If Convert.IsDBNull(Rdr("shop_qm_url")) = False Then ret.shop_qm_url = Rdr("shop_qm_url").ToString()
                        If Convert.IsDBNull(Rdr("active")) = False Then ret.active = Rdr("active").ToString()
                        If Convert.IsDBNull(Rdr("qm_ftp_username")) = False Then ret.qm_ftp_username = Rdr("qm_ftp_username").ToString()
                        If Convert.IsDBNull(Rdr("qm_ftp_password")) = False Then ret.qm_ftp_password = Rdr("qm_ftp_password").ToString()
                        If Convert.IsDBNull(Rdr("shop_size")) = False Then ret.shop_size = Rdr("shop_size").ToString()
                        If Convert.IsDBNull(Rdr("main_servername")) = False Then ret.main_servername = Rdr("main_servername").ToString()
                        If Convert.IsDBNull(Rdr("main_db_name")) = False Then ret.main_db_name = Rdr("main_db_name").ToString()
                        If Convert.IsDBNull(Rdr("main_db_userid")) = False Then ret.main_db_userid = Rdr("main_db_userid").ToString()
                        If Convert.IsDBNull(Rdr("main_db_pwd")) = False Then ret.main_db_pwd = Rdr("main_db_pwd").ToString()

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


        'Get Insert Statement for table TB_SHOP
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_CODE, SHOP_NAME_TH, SHOP_NAME_EN, SHOP_ABB, REGION_ID, BUILDING_ID, OPEN_TIME, CLOSE_TIME, SHOP_DB_NAME, SHOP_DB_USERID, SHOP_DB_PWD, SHOP_DB_SERVER, SHOP_DR_NAME, SHOP_DR_USERID, SHOP_DR_PWD, SHOP_DR_SERVER, SHOP_USE_QM, SHOP_QM_URL, ACTIVE, QM_FTP_USERNAME, QM_FTP_PASSWORD, SHOP_SIZE, MAIN_SERVERNAME, MAIN_DB_NAME, MAIN_DB_USERID, MAIN_DB_PWD)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_SHOP_CODE) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetString(_SHOP_ABB) & ", "
                sql += DB.SetDouble(_REGION_ID) & ", "
                sql += DB.SetDouble(_BUILDING_ID) & ", "
                sql += DB.SetString(_OPEN_TIME) & ", "
                sql += DB.SetString(_CLOSE_TIME) & ", "
                sql += DB.SetString(_SHOP_DB_NAME) & ", "
                sql += DB.SetString(_SHOP_DB_USERID) & ", "
                sql += DB.SetString(_SHOP_DB_PWD) & ", "
                sql += DB.SetString(_SHOP_DB_SERVER) & ", "
                sql += DB.SetString(_SHOP_DR_NAME) & ", "
                sql += DB.SetString(_SHOP_DR_USERID) & ", "
                sql += DB.SetString(_SHOP_DR_PWD) & ", "
                sql += DB.SetString(_SHOP_DR_SERVER) & ", "
                sql += DB.SetString(_SHOP_USE_QM) & ", "
                sql += DB.SetString(_SHOP_QM_URL) & ", "
                sql += DB.SetString(_ACTIVE) & ", "
                sql += DB.SetString(_QM_FTP_USERNAME) & ", "
                sql += DB.SetString(_QM_FTP_PASSWORD) & ", "
                sql += DB.SetString(_SHOP_SIZE) & ", "
                sql += DB.SetString(_MAIN_SERVERNAME) & ", "
                sql += DB.SetString(_MAIN_DB_NAME) & ", "
                sql += DB.SetString(_MAIN_DB_USERID) & ", "
                sql += DB.SetString(_MAIN_DB_PWD)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_SHOP
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_CODE = " & DB.SetString(_SHOP_CODE) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "SHOP_ABB = " & DB.SetString(_SHOP_ABB) & ", "
                Sql += "REGION_ID = " & DB.SetDouble(_REGION_ID) & ", "
                Sql += "BUILDING_ID = " & DB.SetDouble(_BUILDING_ID) & ", "
                Sql += "OPEN_TIME = " & DB.SetString(_OPEN_TIME) & ", "
                Sql += "CLOSE_TIME = " & DB.SetString(_CLOSE_TIME) & ", "
                Sql += "SHOP_DB_NAME = " & DB.SetString(_SHOP_DB_NAME) & ", "
                Sql += "SHOP_DB_USERID = " & DB.SetString(_SHOP_DB_USERID) & ", "
                Sql += "SHOP_DB_PWD = " & DB.SetString(_SHOP_DB_PWD) & ", "
                Sql += "SHOP_DB_SERVER = " & DB.SetString(_SHOP_DB_SERVER) & ", "
                Sql += "SHOP_DR_NAME = " & DB.SetString(_SHOP_DR_NAME) & ", "
                Sql += "SHOP_DR_USERID = " & DB.SetString(_SHOP_DR_USERID) & ", "
                Sql += "SHOP_DR_PWD = " & DB.SetString(_SHOP_DR_PWD) & ", "
                Sql += "SHOP_DR_SERVER = " & DB.SetString(_SHOP_DR_SERVER) & ", "
                Sql += "SHOP_USE_QM = " & DB.SetString(_SHOP_USE_QM) & ", "
                Sql += "SHOP_QM_URL = " & DB.SetString(_SHOP_QM_URL) & ", "
                Sql += "ACTIVE = " & DB.SetString(_ACTIVE) & ", "
                Sql += "QM_FTP_USERNAME = " & DB.SetString(_QM_FTP_USERNAME) & ", "
                Sql += "QM_FTP_PASSWORD = " & DB.SetString(_QM_FTP_PASSWORD) & ", "
                Sql += "SHOP_SIZE = " & DB.SetString(_SHOP_SIZE) & ", "
                Sql += "MAIN_SERVERNAME = " & DB.SetString(_MAIN_SERVERNAME) & ", "
                Sql += "MAIN_DB_NAME = " & DB.SetString(_MAIN_DB_NAME) & ", "
                Sql += "MAIN_DB_USERID = " & DB.SetString(_MAIN_DB_USERID) & ", "
                Sql += "MAIN_DB_PWD = " & DB.SetString(_MAIN_DB_PWD) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_SHOP
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_SHOP
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


            'Define Child Table 

       Dim _TB_FILTER_DATA_shop_id As DataTable
       Dim _TB_FILTER_SHOP_tb_shop_id As DataTable
       Dim _TB_FILTER_STAFF_shop_id As DataTable
       Dim _TB_FILTER_TEMP_TARGET_tb_shop_id As DataTable
       Dim _TB_NOTIFY_JOBLIST_shop_id As DataTable
       Dim _TB_REP_AVG_SERVICE_TIME_KPI_MONTH_shop_id As DataTable
       Dim _TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY_shop_id As DataTable
       Dim _TB_REP_AVG_SERVICE_TIME_KPI_TIME_shop_id As DataTable
       Dim _TB_REP_CAPACITY_SHOP_MONTH_shop_id As DataTable
       Dim _TB_REP_CUST_NETWORK_TIME_shop_id As DataTable
       Dim _TB_REP_CUST_SEGMENT_TIME_shop_id As DataTable
       Dim _TB_REP_PRODUCTIVITY_DAY_shop_id As DataTable
       Dim _TB_REP_STAFF_ATTENDANCE_shop_id As DataTable
       Dim _TB_REP_WT_HT_AGENT_shop_id As DataTable
       Dim _TB_REP_WT_HT_SHOP_MONTH_shop_id As DataTable
       Dim _TB_REP_WT_HT_SHOP_TIME_shop_id As DataTable
       Dim _TB_SHOP_CONFIG_shop_id As DataTable
       Dim _TB_TEMP_COMPARE_TRANSACTION_shop_id As DataTable

       Public Property CHILD_TB_FILTER_DATA_shop_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_DATA Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterDataItem As New TbFilterDataCenLinqDB
               _TB_FILTER_DATA_shop_id = TbFilterDataItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterDataItem = Nothing
               Return _TB_FILTER_DATA_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_DATA_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_FILTER_SHOP_tb_shop_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_SHOP Column :tb_shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterShopItem As New TbFilterShopCenLinqDB
               _TB_FILTER_SHOP_tb_shop_id = TbFilterShopItem.GetDataList("tb_shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterShopItem = Nothing
               Return _TB_FILTER_SHOP_tb_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_SHOP_tb_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_FILTER_STAFF_shop_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_STAFF Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterStaffItem As New TbFilterStaffCenLinqDB
               _TB_FILTER_STAFF_shop_id = TbFilterStaffItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterStaffItem = Nothing
               Return _TB_FILTER_STAFF_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_STAFF_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_FILTER_TEMP_TARGET_tb_shop_id() As DataTable
           Get
               'Child Table Name : TB_FILTER_TEMP_TARGET Column :tb_shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbFilterTempTargetItem As New TbFilterTempTargetCenLinqDB
               _TB_FILTER_TEMP_TARGET_tb_shop_id = TbFilterTempTargetItem.GetDataList("tb_shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbFilterTempTargetItem = Nothing
               Return _TB_FILTER_TEMP_TARGET_tb_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_FILTER_TEMP_TARGET_tb_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_NOTIFY_JOBLIST_shop_id() As DataTable
           Get
               'Child Table Name : TB_NOTIFY_JOBLIST Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbNotifyJoblistItem As New TbNotifyJoblistCenLinqDB
               _TB_NOTIFY_JOBLIST_shop_id = TbNotifyJoblistItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbNotifyJoblistItem = Nothing
               Return _TB_NOTIFY_JOBLIST_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_NOTIFY_JOBLIST_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_AVG_SERVICE_TIME_KPI_MONTH_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_AVG_SERVICE_TIME_KPI_MONTH Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepAvgServiceTimeKpiMonthItem As New TbRepAvgServiceTimeKpiMonthCenLinqDB
               _TB_REP_AVG_SERVICE_TIME_KPI_MONTH_shop_id = TbRepAvgServiceTimeKpiMonthItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepAvgServiceTimeKpiMonthItem = Nothing
               Return _TB_REP_AVG_SERVICE_TIME_KPI_MONTH_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_AVG_SERVICE_TIME_KPI_MONTH_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepAvgServiceTimeKpiStaffDayItem As New TbRepAvgServiceTimeKpiStaffDayCenLinqDB
               _TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY_shop_id = TbRepAvgServiceTimeKpiStaffDayItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepAvgServiceTimeKpiStaffDayItem = Nothing
               Return _TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_AVG_SERVICE_TIME_KPI_STAFF_DAY_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_AVG_SERVICE_TIME_KPI_TIME_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_AVG_SERVICE_TIME_KPI_TIME Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepAvgServiceTimeKpiTimeItem As New TbRepAvgServiceTimeKpiTimeCenLinqDB
               _TB_REP_AVG_SERVICE_TIME_KPI_TIME_shop_id = TbRepAvgServiceTimeKpiTimeItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepAvgServiceTimeKpiTimeItem = Nothing
               Return _TB_REP_AVG_SERVICE_TIME_KPI_TIME_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_AVG_SERVICE_TIME_KPI_TIME_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_CAPACITY_SHOP_MONTH_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_CAPACITY_SHOP_MONTH Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepCapacityShopMonthItem As New TbRepCapacityShopMonthCenLinqDB
               _TB_REP_CAPACITY_SHOP_MONTH_shop_id = TbRepCapacityShopMonthItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepCapacityShopMonthItem = Nothing
               Return _TB_REP_CAPACITY_SHOP_MONTH_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_CAPACITY_SHOP_MONTH_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_CUST_NETWORK_TIME_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_CUST_NETWORK_TIME Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepCustNetworkTimeItem As New TbRepCustNetworkTimeCenLinqDB
               _TB_REP_CUST_NETWORK_TIME_shop_id = TbRepCustNetworkTimeItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepCustNetworkTimeItem = Nothing
               Return _TB_REP_CUST_NETWORK_TIME_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_CUST_NETWORK_TIME_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_CUST_SEGMENT_TIME_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_CUST_SEGMENT_TIME Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepCustSegmentTimeItem As New TbRepCustSegmentTimeCenLinqDB
               _TB_REP_CUST_SEGMENT_TIME_shop_id = TbRepCustSegmentTimeItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepCustSegmentTimeItem = Nothing
               Return _TB_REP_CUST_SEGMENT_TIME_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_CUST_SEGMENT_TIME_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_PRODUCTIVITY_DAY_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_PRODUCTIVITY_DAY Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepProductivityDayItem As New TbRepProductivityDayCenLinqDB
               _TB_REP_PRODUCTIVITY_DAY_shop_id = TbRepProductivityDayItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepProductivityDayItem = Nothing
               Return _TB_REP_PRODUCTIVITY_DAY_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_PRODUCTIVITY_DAY_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_STAFF_ATTENDANCE_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_STAFF_ATTENDANCE Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepStaffAttendanceItem As New TbRepStaffAttendanceCenLinqDB
               _TB_REP_STAFF_ATTENDANCE_shop_id = TbRepStaffAttendanceItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepStaffAttendanceItem = Nothing
               Return _TB_REP_STAFF_ATTENDANCE_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_STAFF_ATTENDANCE_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_WT_HT_AGENT_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_WT_HT_AGENT Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepWtHtAgentItem As New TbRepWtHtAgentCenLinqDB
               _TB_REP_WT_HT_AGENT_shop_id = TbRepWtHtAgentItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepWtHtAgentItem = Nothing
               Return _TB_REP_WT_HT_AGENT_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_WT_HT_AGENT_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_WT_HT_SHOP_MONTH_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_WT_HT_SHOP_MONTH Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepWtHtShopMonthItem As New TbRepWtHtShopMonthCenLinqDB
               _TB_REP_WT_HT_SHOP_MONTH_shop_id = TbRepWtHtShopMonthItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepWtHtShopMonthItem = Nothing
               Return _TB_REP_WT_HT_SHOP_MONTH_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_WT_HT_SHOP_MONTH_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_REP_WT_HT_SHOP_TIME_shop_id() As DataTable
           Get
               'Child Table Name : TB_REP_WT_HT_SHOP_TIME Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbRepWtHtShopTimeItem As New TbRepWtHtShopTimeCenLinqDB
               _TB_REP_WT_HT_SHOP_TIME_shop_id = TbRepWtHtShopTimeItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbRepWtHtShopTimeItem = Nothing
               Return _TB_REP_WT_HT_SHOP_TIME_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_REP_WT_HT_SHOP_TIME_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_SHOP_CONFIG_shop_id() As DataTable
           Get
               'Child Table Name : TB_SHOP_CONFIG Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbShopConfigItem As New TbShopConfigCenLinqDB
               _TB_SHOP_CONFIG_shop_id = TbShopConfigItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbShopConfigItem = Nothing
               Return _TB_SHOP_CONFIG_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_SHOP_CONFIG_shop_id = value
           End Set
       End Property
       Public Property CHILD_TB_TEMP_COMPARE_TRANSACTION_shop_id() As DataTable
           Get
               'Child Table Name : TB_TEMP_COMPARE_TRANSACTION Column :shop_id
               Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
               Dim TbTempCompareTransactionItem As New TbTempCompareTransactionCenLinqDB
               _TB_TEMP_COMPARE_TRANSACTION_shop_id = TbTempCompareTransactionItem.GetDataList("shop_id = " & _ID, "", trans.Trans)
               trans.CommitTransaction()
               TbTempCompareTransactionItem = Nothing
               Return _TB_TEMP_COMPARE_TRANSACTION_shop_id
           End Get
           Set(ByVal value As DataTable)
               _TB_TEMP_COMPARE_TRANSACTION_shop_id = value
           End Set
       End Property
    End Class
End Namespace
