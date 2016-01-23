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
    'Represents a transaction for TB_REP_CUST_SEGMENT_MONTH table CenLinqDB.
    '[Create by  on March, 6 2013]
    Public Class TbRepCustSegmentMonthCenLinqDB
        Public sub TbRepCustSegmentMonthCenLinqDB()

        End Sub 
        ' TB_REP_CUST_SEGMENT_MONTH
        Const _tableName As String = "TB_REP_CUST_SEGMENT_MONTH"
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
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _MONTH_NO As Long = 0
        Dim _SHOW_MONTH As String = ""
        Dim _SHOW_YEAR As Long = 0
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _MASS_REGIS As Long = 0
        Dim _MASS_SERVE As Long = 0
        Dim _MASS_MISS_CALL As Long = 0
        Dim _MASS_CANCEL As Long = 0
        Dim _MASS_INCOMPLETE As Long = 0
        Dim _SERENADE_REGIS As Long = 0
        Dim _SERENADE_SERVE As Long = 0
        Dim _SERENADE_MISS_CALL As Long = 0
        Dim _SERENADE_CANCEL As Long = 0
        Dim _SERENADE_INCOMPLETE As Long = 0

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
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
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
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_EN() As String
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_MONTH_NO", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MONTH_NO() As Long
            Get
                Return _MONTH_NO
            End Get
            Set(ByVal value As Long)
               _MONTH_NO = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_MONTH", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_MONTH() As String
            Get
                Return _SHOW_MONTH
            End Get
            Set(ByVal value As String)
               _SHOW_MONTH = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_YEAR() As Long
            Get
                Return _SHOW_YEAR
            End Get
            Set(ByVal value As Long)
               _SHOW_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As Long
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As Long)
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_EN() As String
            Get
                Return _SERVICE_NAME_EN
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_TH() As String
            Get
                Return _SERVICE_NAME_TH
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_MASS_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_REGIS() As Long
            Get
                Return _MASS_REGIS
            End Get
            Set(ByVal value As Long)
               _MASS_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_MASS_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_SERVE() As Long
            Get
                Return _MASS_SERVE
            End Get
            Set(ByVal value As Long)
               _MASS_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_MASS_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_MISS_CALL() As Long
            Get
                Return _MASS_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _MASS_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_MASS_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_CANCEL() As Long
            Get
                Return _MASS_CANCEL
            End Get
            Set(ByVal value As Long)
               _MASS_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_MASS_INCOMPLETE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MASS_INCOMPLETE() As Long
            Get
                Return _MASS_INCOMPLETE
            End Get
            Set(ByVal value As Long)
               _MASS_INCOMPLETE = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_REGIS() As Long
            Get
                Return _SERENADE_REGIS
            End Get
            Set(ByVal value As Long)
               _SERENADE_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_SERVE() As Long
            Get
                Return _SERENADE_SERVE
            End Get
            Set(ByVal value As Long)
               _SERENADE_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_MISS_CALL() As Long
            Get
                Return _SERENADE_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _SERENADE_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_CANCEL() As Long
            Get
                Return _SERENADE_CANCEL
            End Get
            Set(ByVal value As Long)
               _SERENADE_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_SERENADE_INCOMPLETE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERENADE_INCOMPLETE() As Long
            Get
                Return _SERENADE_INCOMPLETE
            End Get
            Set(ByVal value As Long)
               _SERENADE_INCOMPLETE = value
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
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _MONTH_NO = 0
            _SHOW_MONTH = ""
            _SHOW_YEAR = 0
            _SERVICE_ID = 0
            _SERVICE_NAME_EN = ""
            _SERVICE_NAME_TH = ""
            _MASS_REGIS = 0
            _MASS_SERVE = 0
            _MASS_MISS_CALL = 0
            _MASS_CANCEL = 0
            _MASS_INCOMPLETE = 0
            _SERENADE_REGIS = 0
            _SERENADE_SERVE = 0
            _SERENADE_MISS_CALL = 0
            _SERENADE_CANCEL = 0
            _SERENADE_INCOMPLETE = 0
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


        '/// Returns an indication whether the current data is inserted into TB_REP_CUST_SEGMENT_MONTH table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_SEGMENT_MONTH table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_SEGMENT_MONTH table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_CUST_SEGMENT_MONTH table successfully.
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


        '/// Returns an indication whether the record of TB_REP_CUST_SEGMENT_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_CUST_SEGMENT_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepCustSegmentMonthCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_CUST_SEGMENT_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepCustSegmentMonthCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_SEGMENT_MONTH by specified SHOP_ID_SHOW_MONTH_SHOW_YEAR key is retrieved successfully.
        '/// <param name=cSHOP_ID_SHOW_MONTH_SHOW_YEAR>The SHOP_ID_SHOW_MONTH_SHOW_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySHOP_ID_SHOW_MONTH_SHOW_YEAR(cSHOP_ID As Integer, cSHOW_MONTH As String, cSHOW_YEAR As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_MONTH = " & DB.SetString(cSHOW_MONTH) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_CUST_SEGMENT_MONTH by specified SHOP_ID_SHOW_MONTH_SHOW_YEAR key is retrieved successfully.
        '/// <param name=cSHOP_ID_SHOW_MONTH_SHOW_YEAR>The SHOP_ID_SHOW_MONTH_SHOW_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySHOP_ID_SHOW_MONTH_SHOW_YEAR(cSHOP_ID As Integer, cSHOW_MONTH As String, cSHOW_YEAR As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_MONTH = " & DB.SetString(cSHOW_MONTH) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_SEGMENT_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_CUST_SEGMENT_MONTH table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_SEGMENT_MONTH table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_CUST_SEGMENT_MONTH table successfully.
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


        '/// Returns an indication whether the record of TB_REP_CUST_SEGMENT_MONTH by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("month_no")) = False Then _month_no = Convert.ToInt32(Rdr("month_no"))
                        If Convert.IsDBNull(Rdr("show_month")) = False Then _show_month = Rdr("show_month").ToString()
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("mass_regis")) = False Then _mass_regis = Convert.ToInt32(Rdr("mass_regis"))
                        If Convert.IsDBNull(Rdr("mass_serve")) = False Then _mass_serve = Convert.ToInt32(Rdr("mass_serve"))
                        If Convert.IsDBNull(Rdr("mass_miss_call")) = False Then _mass_miss_call = Convert.ToInt32(Rdr("mass_miss_call"))
                        If Convert.IsDBNull(Rdr("mass_cancel")) = False Then _mass_cancel = Convert.ToInt32(Rdr("mass_cancel"))
                        If Convert.IsDBNull(Rdr("mass_incomplete")) = False Then _mass_incomplete = Convert.ToInt32(Rdr("mass_incomplete"))
                        If Convert.IsDBNull(Rdr("serenade_regis")) = False Then _serenade_regis = Convert.ToInt32(Rdr("serenade_regis"))
                        If Convert.IsDBNull(Rdr("serenade_serve")) = False Then _serenade_serve = Convert.ToInt32(Rdr("serenade_serve"))
                        If Convert.IsDBNull(Rdr("serenade_miss_call")) = False Then _serenade_miss_call = Convert.ToInt32(Rdr("serenade_miss_call"))
                        If Convert.IsDBNull(Rdr("serenade_cancel")) = False Then _serenade_cancel = Convert.ToInt32(Rdr("serenade_cancel"))
                        If Convert.IsDBNull(Rdr("serenade_incomplete")) = False Then _serenade_incomplete = Convert.ToInt32(Rdr("serenade_incomplete"))
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


        '/// Returns an indication whether the record of TB_REP_CUST_SEGMENT_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepCustSegmentMonthCenLinqDB
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
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("month_no")) = False Then _month_no = Convert.ToInt32(Rdr("month_no"))
                        If Convert.IsDBNull(Rdr("show_month")) = False Then _show_month = Rdr("show_month").ToString()
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("mass_regis")) = False Then _mass_regis = Convert.ToInt32(Rdr("mass_regis"))
                        If Convert.IsDBNull(Rdr("mass_serve")) = False Then _mass_serve = Convert.ToInt32(Rdr("mass_serve"))
                        If Convert.IsDBNull(Rdr("mass_miss_call")) = False Then _mass_miss_call = Convert.ToInt32(Rdr("mass_miss_call"))
                        If Convert.IsDBNull(Rdr("mass_cancel")) = False Then _mass_cancel = Convert.ToInt32(Rdr("mass_cancel"))
                        If Convert.IsDBNull(Rdr("mass_incomplete")) = False Then _mass_incomplete = Convert.ToInt32(Rdr("mass_incomplete"))
                        If Convert.IsDBNull(Rdr("serenade_regis")) = False Then _serenade_regis = Convert.ToInt32(Rdr("serenade_regis"))
                        If Convert.IsDBNull(Rdr("serenade_serve")) = False Then _serenade_serve = Convert.ToInt32(Rdr("serenade_serve"))
                        If Convert.IsDBNull(Rdr("serenade_miss_call")) = False Then _serenade_miss_call = Convert.ToInt32(Rdr("serenade_miss_call"))
                        If Convert.IsDBNull(Rdr("serenade_cancel")) = False Then _serenade_cancel = Convert.ToInt32(Rdr("serenade_cancel"))
                        If Convert.IsDBNull(Rdr("serenade_incomplete")) = False Then _serenade_incomplete = Convert.ToInt32(Rdr("serenade_incomplete"))
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


        '/// Returns an indication whether the Class Data of TB_REP_CUST_SEGMENT_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepCustSegmentMonthCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepCustSegmentMonthCenParaDB
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
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then ret.shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then ret.shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("month_no")) = False Then ret.month_no = Convert.ToInt32(Rdr("month_no"))
                        If Convert.IsDBNull(Rdr("show_month")) = False Then ret.show_month = Rdr("show_month").ToString()
                        If Convert.IsDBNull(Rdr("show_year")) = False Then ret.show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then ret.service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then ret.service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("mass_regis")) = False Then ret.mass_regis = Convert.ToInt32(Rdr("mass_regis"))
                        If Convert.IsDBNull(Rdr("mass_serve")) = False Then ret.mass_serve = Convert.ToInt32(Rdr("mass_serve"))
                        If Convert.IsDBNull(Rdr("mass_miss_call")) = False Then ret.mass_miss_call = Convert.ToInt32(Rdr("mass_miss_call"))
                        If Convert.IsDBNull(Rdr("mass_cancel")) = False Then ret.mass_cancel = Convert.ToInt32(Rdr("mass_cancel"))
                        If Convert.IsDBNull(Rdr("mass_incomplete")) = False Then ret.mass_incomplete = Convert.ToInt32(Rdr("mass_incomplete"))
                        If Convert.IsDBNull(Rdr("serenade_regis")) = False Then ret.serenade_regis = Convert.ToInt32(Rdr("serenade_regis"))
                        If Convert.IsDBNull(Rdr("serenade_serve")) = False Then ret.serenade_serve = Convert.ToInt32(Rdr("serenade_serve"))
                        If Convert.IsDBNull(Rdr("serenade_miss_call")) = False Then ret.serenade_miss_call = Convert.ToInt32(Rdr("serenade_miss_call"))
                        If Convert.IsDBNull(Rdr("serenade_cancel")) = False Then ret.serenade_cancel = Convert.ToInt32(Rdr("serenade_cancel"))
                        If Convert.IsDBNull(Rdr("serenade_incomplete")) = False Then ret.serenade_incomplete = Convert.ToInt32(Rdr("serenade_incomplete"))

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


        'Get Insert Statement for table TB_REP_CUST_SEGMENT_MONTH
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, MONTH_NO, SHOW_MONTH, SHOW_YEAR, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, MASS_REGIS, MASS_SERVE, MASS_MISS_CALL, MASS_CANCEL, MASS_INCOMPLETE, SERENADE_REGIS, SERENADE_SERVE, SERENADE_MISS_CALL, SERENADE_CANCEL, SERENADE_INCOMPLETE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDouble(_MONTH_NO) & ", "
                sql += DB.SetString(_SHOW_MONTH) & ", "
                sql += DB.SetDouble(_SHOW_YEAR) & ", "
                sql += DB.SetDouble(_SERVICE_ID) & ", "
                sql += DB.SetString(_SERVICE_NAME_EN) & ", "
                sql += DB.SetString(_SERVICE_NAME_TH) & ", "
                sql += DB.SetDouble(_MASS_REGIS) & ", "
                sql += DB.SetDouble(_MASS_SERVE) & ", "
                sql += DB.SetDouble(_MASS_MISS_CALL) & ", "
                sql += DB.SetDouble(_MASS_CANCEL) & ", "
                sql += DB.SetDouble(_MASS_INCOMPLETE) & ", "
                sql += DB.SetDouble(_SERENADE_REGIS) & ", "
                sql += DB.SetDouble(_SERENADE_SERVE) & ", "
                sql += DB.SetDouble(_SERENADE_MISS_CALL) & ", "
                sql += DB.SetDouble(_SERENADE_CANCEL) & ", "
                sql += DB.SetDouble(_SERENADE_INCOMPLETE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_CUST_SEGMENT_MONTH
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "MONTH_NO = " & DB.SetDouble(_MONTH_NO) & ", "
                Sql += "SHOW_MONTH = " & DB.SetString(_SHOW_MONTH) & ", "
                Sql += "SHOW_YEAR = " & DB.SetDouble(_SHOW_YEAR) & ", "
                Sql += "SERVICE_ID = " & DB.SetDouble(_SERVICE_ID) & ", "
                Sql += "SERVICE_NAME_EN = " & DB.SetString(_SERVICE_NAME_EN) & ", "
                Sql += "SERVICE_NAME_TH = " & DB.SetString(_SERVICE_NAME_TH) & ", "
                Sql += "MASS_REGIS = " & DB.SetDouble(_MASS_REGIS) & ", "
                Sql += "MASS_SERVE = " & DB.SetDouble(_MASS_SERVE) & ", "
                Sql += "MASS_MISS_CALL = " & DB.SetDouble(_MASS_MISS_CALL) & ", "
                Sql += "MASS_CANCEL = " & DB.SetDouble(_MASS_CANCEL) & ", "
                Sql += "MASS_INCOMPLETE = " & DB.SetDouble(_MASS_INCOMPLETE) & ", "
                Sql += "SERENADE_REGIS = " & DB.SetDouble(_SERENADE_REGIS) & ", "
                Sql += "SERENADE_SERVE = " & DB.SetDouble(_SERENADE_SERVE) & ", "
                Sql += "SERENADE_MISS_CALL = " & DB.SetDouble(_SERENADE_MISS_CALL) & ", "
                Sql += "SERENADE_CANCEL = " & DB.SetDouble(_SERENADE_CANCEL) & ", "
                Sql += "SERENADE_INCOMPLETE = " & DB.SetDouble(_SERENADE_INCOMPLETE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_CUST_SEGMENT_MONTH
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_CUST_SEGMENT_MONTH
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, MONTH_NO, SHOW_MONTH, SHOW_YEAR, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, MASS_REGIS, MASS_SERVE, MASS_MISS_CALL, MASS_CANCEL, MASS_INCOMPLETE, SERENADE_REGIS, SERENADE_SERVE, SERENADE_MISS_CALL, SERENADE_CANCEL, SERENADE_INCOMPLETE FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
