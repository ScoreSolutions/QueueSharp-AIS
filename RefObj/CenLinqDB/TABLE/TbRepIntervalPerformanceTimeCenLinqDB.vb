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
    'Represents a transaction for TB_REP_INTERVAL_PERFORMANCE_TIME table CenLinqDB.
    '[Create by  on January, 28 2015]
    Public Class TbRepIntervalPerformanceTimeCenLinqDB
        Public sub TbRepIntervalPerformanceTimeCenLinqDB()

        End Sub 
        ' TB_REP_INTERVAL_PERFORMANCE_TIME
        Const _tableName As String = "TB_REP_INTERVAL_PERFORMANCE_TIME"
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
        Dim _REGION_NAME As String = ""
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _NETWORK_TYPE As String = ""
        Dim _CUSTOMERTYPE_NAME As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME As String = ""
        Dim _INTERVAL_MINUTE As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _TIME_PRIOD_FROM As DateTime = New DateTime(1,1,1)
        Dim _TIME_PRIOD_TO As DateTime = New DateTime(1,1,1)
        Dim _SHOW_TIME As String = ""
        Dim _REGIS As Long = 0
        Dim _SERVED As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCEL As Long = 0
        Dim _NOT_CALL As Long = 0
        Dim _NOT_CON As Long = 0
        Dim _NOT_END As Long = 0
        Dim _WAIT_WITH_KPI As Long = 0
        Dim _SERVE_WITH_KPI As Long = 0
        Dim _MAX_WT As Long = 0
        Dim _MAX_HT As Long = 0
        Dim _AWT As Long = 0
        Dim _AHT As Long = 0
        Dim _COUNT_WT As Long = 0
        Dim _SUM_WT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _SUM_HT As Long = 0

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
        <Column(Storage:="_REGION_NAME", DbType:="VarChar(3) NOT NULL ",CanBeNull:=false)>  _
        Public Property REGION_NAME() As String
            Get
                Return _REGION_NAME
            End Get
            Set(ByVal value As String)
               _REGION_NAME = value
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
        <Column(Storage:="_NETWORK_TYPE", DbType:="VarChar(5) NOT NULL ",CanBeNull:=false)>  _
        Public Property NETWORK_TYPE() As String
            Get
                Return _NETWORK_TYPE
            End Get
            Set(ByVal value As String)
               _NETWORK_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMERTYPE_NAME", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property CUSTOMERTYPE_NAME() As String
            Get
                Return _CUSTOMERTYPE_NAME
            End Get
            Set(ByVal value As String)
               _CUSTOMERTYPE_NAME = value
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
        <Column(Storage:="_SERVICE_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME() As String
            Get
                Return _SERVICE_NAME
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_INTERVAL_MINUTE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property INTERVAL_MINUTE() As Long
            Get
                Return _INTERVAL_MINUTE
            End Get
            Set(ByVal value As Long)
               _INTERVAL_MINUTE = value
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
        <Column(Storage:="_TIME_PRIOD_FROM", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TIME_PRIOD_FROM() As DateTime
            Get
                Return _TIME_PRIOD_FROM
            End Get
            Set(ByVal value As DateTime)
               _TIME_PRIOD_FROM = value
            End Set
        End Property 
        <Column(Storage:="_TIME_PRIOD_TO", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property TIME_PRIOD_TO() As DateTime
            Get
                Return _TIME_PRIOD_TO
            End Get
            Set(ByVal value As DateTime)
               _TIME_PRIOD_TO = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_TIME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_TIME() As String
            Get
                Return _SHOW_TIME
            End Get
            Set(ByVal value As String)
               _SHOW_TIME = value
            End Set
        End Property 
        <Column(Storage:="_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
               _REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERVED", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVED() As Long
            Get
                Return _SERVED
            End Get
            Set(ByVal value As Long)
               _SERVED = value
            End Set
        End Property 
        <Column(Storage:="_MISSED_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MISSED_CALL() As Long
            Get
                Return _MISSED_CALL
            End Get
            Set(ByVal value As Long)
               _MISSED_CALL = value
            End Set
        End Property 
        <Column(Storage:="_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCEL() As Long
            Get
                Return _CANCEL
            End Get
            Set(ByVal value As Long)
               _CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CALL() As Long
            Get
                Return _NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CON() As Long
            Get
                Return _NOT_CON
            End Get
            Set(ByVal value As Long)
               _NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_END() As Long
            Get
                Return _NOT_END
            End Get
            Set(ByVal value As Long)
               _NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_WAIT_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WAIT_WITH_KPI() As Long
            Get
                Return _WAIT_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _WAIT_WITH_KPI = value
            End Set
        End Property 
        <Column(Storage:="_SERVE_WITH_KPI", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE_WITH_KPI() As Long
            Get
                Return _SERVE_WITH_KPI
            End Get
            Set(ByVal value As Long)
               _SERVE_WITH_KPI = value
            End Set
        End Property 
        <Column(Storage:="_MAX_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_WT() As Long
            Get
                Return _MAX_WT
            End Get
            Set(ByVal value As Long)
               _MAX_WT = value
            End Set
        End Property 
        <Column(Storage:="_MAX_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MAX_HT() As Long
            Get
                Return _MAX_HT
            End Get
            Set(ByVal value As Long)
               _MAX_HT = value
            End Set
        End Property 
        <Column(Storage:="_AWT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AWT() As Long
            Get
                Return _AWT
            End Get
            Set(ByVal value As Long)
               _AWT = value
            End Set
        End Property 
        <Column(Storage:="_AHT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AHT() As Long
            Get
                Return _AHT
            End Get
            Set(ByVal value As Long)
               _AHT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_WT() As Long
            Get
                Return _COUNT_WT
            End Get
            Set(ByVal value As Long)
               _COUNT_WT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_WT() As Long
            Get
                Return _SUM_WT
            End Get
            Set(ByVal value As Long)
               _SUM_WT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_HT() As Long
            Get
                Return _COUNT_HT
            End Get
            Set(ByVal value As Long)
               _COUNT_HT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_HT() As Long
            Get
                Return _SUM_HT
            End Get
            Set(ByVal value As Long)
               _SUM_HT = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _REGION_NAME = ""
            _SHOP_ID = 0
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _NETWORK_TYPE = ""
            _CUSTOMERTYPE_NAME = ""
            _SERVICE_ID = 0
            _SERVICE_NAME = ""
            _INTERVAL_MINUTE = 0
            _SERVICE_DATE = New DateTime(1,1,1)
            _TIME_PRIOD_FROM = New DateTime(1,1,1)
            _TIME_PRIOD_TO = New DateTime(1,1,1)
            _SHOW_TIME = ""
            _REGIS = 0
            _SERVED = 0
            _MISSED_CALL = 0
            _CANCEL = 0
            _NOT_CALL = 0
            _NOT_CON = 0
            _NOT_END = 0
            _WAIT_WITH_KPI = 0
            _SERVE_WITH_KPI = 0
            _MAX_WT = 0
            _MAX_HT = 0
            _AWT = 0
            _AHT = 0
            _COUNT_WT = 0
            _SUM_WT = 0
            _COUNT_HT = 0
            _SUM_HT = 0
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


        '/// Returns an indication whether the current data is inserted into TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
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


        '/// Returns an indication whether the record of TB_REP_INTERVAL_PERFORMANCE_TIME by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_INTERVAL_PERFORMANCE_TIME by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Integer, trans As SQLTransaction) As TbRepIntervalPerformanceTimeCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_INTERVAL_PERFORMANCE_TIME by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Integer, trans As SQLTransaction) As TbRepIntervalPerformanceTimeCenParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_INTERVAL_PERFORMANCE_TIME by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_INTERVAL_PERFORMANCE_TIME table successfully.
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


        '/// Returns an indication whether the record of TB_REP_INTERVAL_PERFORMANCE_TIME by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("region_name")) = False Then _region_name = Rdr("region_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("customertype_name")) = False Then _customertype_name = Rdr("customertype_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name")) = False Then _service_name = Rdr("service_name").ToString()
                        If Convert.IsDBNull(Rdr("interval_minute")) = False Then _interval_minute = Convert.ToInt32(Rdr("interval_minute"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("time_priod_from")) = False Then _time_priod_from = Convert.ToDateTime(Rdr("time_priod_from"))
                        If Convert.IsDBNull(Rdr("time_priod_to")) = False Then _time_priod_to = Convert.ToDateTime(Rdr("time_priod_to"))
                        If Convert.IsDBNull(Rdr("show_time")) = False Then _show_time = Rdr("show_time").ToString()
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("served")) = False Then _served = Convert.ToInt32(Rdr("served"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then _missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancel")) = False Then _cancel = Convert.ToInt32(Rdr("cancel"))
                        If Convert.IsDBNull(Rdr("not_call")) = False Then _not_call = Convert.ToInt32(Rdr("not_call"))
                        If Convert.IsDBNull(Rdr("not_con")) = False Then _not_con = Convert.ToInt32(Rdr("not_con"))
                        If Convert.IsDBNull(Rdr("not_end")) = False Then _not_end = Convert.ToInt32(Rdr("not_end"))
                        If Convert.IsDBNull(Rdr("wait_with_kpi")) = False Then _wait_with_kpi = Convert.ToInt32(Rdr("wait_with_kpi"))
                        If Convert.IsDBNull(Rdr("serve_with_kpi")) = False Then _serve_with_kpi = Convert.ToInt32(Rdr("serve_with_kpi"))
                        If Convert.IsDBNull(Rdr("max_wt")) = False Then _max_wt = Convert.ToInt32(Rdr("max_wt"))
                        If Convert.IsDBNull(Rdr("max_ht")) = False Then _max_ht = Convert.ToInt32(Rdr("max_ht"))
                        If Convert.IsDBNull(Rdr("awt")) = False Then _awt = Convert.ToInt32(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt32(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then _count_wt = Convert.ToInt32(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then _sum_wt = Convert.ToInt32(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt32(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt32(Rdr("sum_ht"))
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


        '/// Returns an indication whether the record of TB_REP_INTERVAL_PERFORMANCE_TIME by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepIntervalPerformanceTimeCenLinqDB
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
                        If Convert.IsDBNull(Rdr("region_name")) = False Then _region_name = Rdr("region_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("customertype_name")) = False Then _customertype_name = Rdr("customertype_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name")) = False Then _service_name = Rdr("service_name").ToString()
                        If Convert.IsDBNull(Rdr("interval_minute")) = False Then _interval_minute = Convert.ToInt32(Rdr("interval_minute"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("time_priod_from")) = False Then _time_priod_from = Convert.ToDateTime(Rdr("time_priod_from"))
                        If Convert.IsDBNull(Rdr("time_priod_to")) = False Then _time_priod_to = Convert.ToDateTime(Rdr("time_priod_to"))
                        If Convert.IsDBNull(Rdr("show_time")) = False Then _show_time = Rdr("show_time").ToString()
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("served")) = False Then _served = Convert.ToInt32(Rdr("served"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then _missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancel")) = False Then _cancel = Convert.ToInt32(Rdr("cancel"))
                        If Convert.IsDBNull(Rdr("not_call")) = False Then _not_call = Convert.ToInt32(Rdr("not_call"))
                        If Convert.IsDBNull(Rdr("not_con")) = False Then _not_con = Convert.ToInt32(Rdr("not_con"))
                        If Convert.IsDBNull(Rdr("not_end")) = False Then _not_end = Convert.ToInt32(Rdr("not_end"))
                        If Convert.IsDBNull(Rdr("wait_with_kpi")) = False Then _wait_with_kpi = Convert.ToInt32(Rdr("wait_with_kpi"))
                        If Convert.IsDBNull(Rdr("serve_with_kpi")) = False Then _serve_with_kpi = Convert.ToInt32(Rdr("serve_with_kpi"))
                        If Convert.IsDBNull(Rdr("max_wt")) = False Then _max_wt = Convert.ToInt32(Rdr("max_wt"))
                        If Convert.IsDBNull(Rdr("max_ht")) = False Then _max_ht = Convert.ToInt32(Rdr("max_ht"))
                        If Convert.IsDBNull(Rdr("awt")) = False Then _awt = Convert.ToInt32(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt32(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then _count_wt = Convert.ToInt32(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then _sum_wt = Convert.ToInt32(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt32(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt32(Rdr("sum_ht"))
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


        '/// Returns an indication whether the Class Data of TB_REP_INTERVAL_PERFORMANCE_TIME by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepIntervalPerformanceTimeCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepIntervalPerformanceTimeCenParaDB
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
                        If Convert.IsDBNull(Rdr("region_name")) = False Then ret.region_name = Rdr("region_name").ToString()
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then ret.shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then ret.shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then ret.shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("customertype_name")) = False Then ret.customertype_name = Rdr("customertype_name").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name")) = False Then ret.service_name = Rdr("service_name").ToString()
                        If Convert.IsDBNull(Rdr("interval_minute")) = False Then ret.interval_minute = Convert.ToInt32(Rdr("interval_minute"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("time_priod_from")) = False Then ret.time_priod_from = Convert.ToDateTime(Rdr("time_priod_from"))
                        If Convert.IsDBNull(Rdr("time_priod_to")) = False Then ret.time_priod_to = Convert.ToDateTime(Rdr("time_priod_to"))
                        If Convert.IsDBNull(Rdr("show_time")) = False Then ret.show_time = Rdr("show_time").ToString()
                        If Convert.IsDBNull(Rdr("regis")) = False Then ret.regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("served")) = False Then ret.served = Convert.ToInt32(Rdr("served"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then ret.missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancel")) = False Then ret.cancel = Convert.ToInt32(Rdr("cancel"))
                        If Convert.IsDBNull(Rdr("not_call")) = False Then ret.not_call = Convert.ToInt32(Rdr("not_call"))
                        If Convert.IsDBNull(Rdr("not_con")) = False Then ret.not_con = Convert.ToInt32(Rdr("not_con"))
                        If Convert.IsDBNull(Rdr("not_end")) = False Then ret.not_end = Convert.ToInt32(Rdr("not_end"))
                        If Convert.IsDBNull(Rdr("wait_with_kpi")) = False Then ret.wait_with_kpi = Convert.ToInt32(Rdr("wait_with_kpi"))
                        If Convert.IsDBNull(Rdr("serve_with_kpi")) = False Then ret.serve_with_kpi = Convert.ToInt32(Rdr("serve_with_kpi"))
                        If Convert.IsDBNull(Rdr("max_wt")) = False Then ret.max_wt = Convert.ToInt32(Rdr("max_wt"))
                        If Convert.IsDBNull(Rdr("max_ht")) = False Then ret.max_ht = Convert.ToInt32(Rdr("max_ht"))
                        If Convert.IsDBNull(Rdr("awt")) = False Then ret.awt = Convert.ToInt32(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then ret.aht = Convert.ToInt32(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then ret.count_wt = Convert.ToInt32(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then ret.sum_wt = Convert.ToInt32(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then ret.count_ht = Convert.ToInt32(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then ret.sum_ht = Convert.ToInt32(Rdr("sum_ht"))

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


        'Get Insert Statement for table TB_REP_INTERVAL_PERFORMANCE_TIME
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, REGION_NAME, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, NETWORK_TYPE, CUSTOMERTYPE_NAME, SERVICE_ID, SERVICE_NAME, INTERVAL_MINUTE, SERVICE_DATE, TIME_PRIOD_FROM, TIME_PRIOD_TO, SHOW_TIME, REGIS, SERVED, MISSED_CALL, CANCEL, NOT_CALL, NOT_CON, NOT_END, WAIT_WITH_KPI, SERVE_WITH_KPI, MAX_WT, MAX_HT, AWT, AHT, COUNT_WT, SUM_WT, COUNT_HT, SUM_HT)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_REGION_NAME) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_CUSTOMERTYPE_NAME) & ", "
                sql += DB.SetDouble(_SERVICE_ID) & ", "
                sql += DB.SetString(_SERVICE_NAME) & ", "
                sql += DB.SetDouble(_INTERVAL_MINUTE) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetDateTime(_TIME_PRIOD_FROM) & ", "
                sql += DB.SetDateTime(_TIME_PRIOD_TO) & ", "
                sql += DB.SetString(_SHOW_TIME) & ", "
                sql += DB.SetDouble(_REGIS) & ", "
                sql += DB.SetDouble(_SERVED) & ", "
                sql += DB.SetDouble(_MISSED_CALL) & ", "
                sql += DB.SetDouble(_CANCEL) & ", "
                sql += DB.SetDouble(_NOT_CALL) & ", "
                sql += DB.SetDouble(_NOT_CON) & ", "
                sql += DB.SetDouble(_NOT_END) & ", "
                sql += DB.SetDouble(_WAIT_WITH_KPI) & ", "
                sql += DB.SetDouble(_SERVE_WITH_KPI) & ", "
                sql += DB.SetDouble(_MAX_WT) & ", "
                sql += DB.SetDouble(_MAX_HT) & ", "
                sql += DB.SetDouble(_AWT) & ", "
                sql += DB.SetDouble(_AHT) & ", "
                sql += DB.SetDouble(_COUNT_WT) & ", "
                sql += DB.SetDouble(_SUM_WT) & ", "
                sql += DB.SetDouble(_COUNT_HT) & ", "
                sql += DB.SetDouble(_SUM_HT)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_INTERVAL_PERFORMANCE_TIME
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "REGION_NAME = " & DB.SetString(_REGION_NAME) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "CUSTOMERTYPE_NAME = " & DB.SetString(_CUSTOMERTYPE_NAME) & ", "
                Sql += "SERVICE_ID = " & DB.SetDouble(_SERVICE_ID) & ", "
                Sql += "SERVICE_NAME = " & DB.SetString(_SERVICE_NAME) & ", "
                Sql += "INTERVAL_MINUTE = " & DB.SetDouble(_INTERVAL_MINUTE) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "TIME_PRIOD_FROM = " & DB.SetDateTime(_TIME_PRIOD_FROM) & ", "
                Sql += "TIME_PRIOD_TO = " & DB.SetDateTime(_TIME_PRIOD_TO) & ", "
                Sql += "SHOW_TIME = " & DB.SetString(_SHOW_TIME) & ", "
                Sql += "REGIS = " & DB.SetDouble(_REGIS) & ", "
                Sql += "SERVED = " & DB.SetDouble(_SERVED) & ", "
                Sql += "MISSED_CALL = " & DB.SetDouble(_MISSED_CALL) & ", "
                Sql += "CANCEL = " & DB.SetDouble(_CANCEL) & ", "
                Sql += "NOT_CALL = " & DB.SetDouble(_NOT_CALL) & ", "
                Sql += "NOT_CON = " & DB.SetDouble(_NOT_CON) & ", "
                Sql += "NOT_END = " & DB.SetDouble(_NOT_END) & ", "
                Sql += "WAIT_WITH_KPI = " & DB.SetDouble(_WAIT_WITH_KPI) & ", "
                Sql += "SERVE_WITH_KPI = " & DB.SetDouble(_SERVE_WITH_KPI) & ", "
                Sql += "MAX_WT = " & DB.SetDouble(_MAX_WT) & ", "
                Sql += "MAX_HT = " & DB.SetDouble(_MAX_HT) & ", "
                Sql += "AWT = " & DB.SetDouble(_AWT) & ", "
                Sql += "AHT = " & DB.SetDouble(_AHT) & ", "
                Sql += "COUNT_WT = " & DB.SetDouble(_COUNT_WT) & ", "
                Sql += "SUM_WT = " & DB.SetDouble(_SUM_WT) & ", "
                Sql += "COUNT_HT = " & DB.SetDouble(_COUNT_HT) & ", "
                Sql += "SUM_HT = " & DB.SetDouble(_SUM_HT) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_INTERVAL_PERFORMANCE_TIME
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_INTERVAL_PERFORMANCE_TIME
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
