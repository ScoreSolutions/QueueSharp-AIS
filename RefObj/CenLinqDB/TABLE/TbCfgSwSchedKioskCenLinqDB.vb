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
    'Represents a transaction for TB_CFG_SW_SCHED_KIOSK table CenLinqDB.
    '[Create by  on June, 26 2013]
    Public Class TbCfgSwSchedKioskCenLinqDB
        Public sub TbCfgSwSchedKioskCenLinqDB()

        End Sub 
        ' TB_CFG_SW_SCHED_KIOSK
        Const _tableName As String = "TB_CFG_SW_SCHED_KIOSK"
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
        Dim _EVENT_DATE As DateTime = New DateTime(1,1,1)
        Dim _K_BEFORE As  System.Nullable(Of Long)  = 0
        Dim _K_LATE As  System.Nullable(Of Long)  = 0
        Dim _K_MAX_APPOINTMENT As  System.Nullable(Of Long)  = 0
        Dim _K_BEFORE_APP As  System.Nullable(Of Long)  = 0
        Dim _K_CANCEL As  System.Nullable(Of Long)  = 0
        Dim _K_DISABLE As  System.Nullable(Of Long)  = 0
        Dim _K_SERVE As  System.Nullable(Of Long)  = 0
        Dim _K_REFRESH As  System.Nullable(Of Long)  = 0
        Dim _K_VDO As  System.Nullable(Of Long)  = 0
        Dim _K_LEN As  System.Nullable(Of Long)  = 0
        Dim _K_MOBILE1 As  String  = ""
        Dim _K_MOBILE2 As  String  = ""
        Dim _K_MOBILE3 As  String  = ""
        Dim _K_MOBILE4 As  String  = ""
        Dim _K_RETARDATION_VDO As  System.Nullable(Of Long)  = 0
        Dim _EVENT_STATUS As Char = "1"

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
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_EVENT_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property EVENT_DATE() As DateTime
            Get
                Return _EVENT_DATE
            End Get
            Set(ByVal value As DateTime)
               _EVENT_DATE = value
            End Set
        End Property 
        <Column(Storage:="_K_BEFORE", DbType:="Int")>  _
        Public Property K_BEFORE() As  System.Nullable(Of Long) 
            Get
                Return _K_BEFORE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_BEFORE = value
            End Set
        End Property 
        <Column(Storage:="_K_LATE", DbType:="Int")>  _
        Public Property K_LATE() As  System.Nullable(Of Long) 
            Get
                Return _K_LATE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_LATE = value
            End Set
        End Property 
        <Column(Storage:="_K_MAX_APPOINTMENT", DbType:="Int")>  _
        Public Property K_MAX_APPOINTMENT() As  System.Nullable(Of Long) 
            Get
                Return _K_MAX_APPOINTMENT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_MAX_APPOINTMENT = value
            End Set
        End Property 
        <Column(Storage:="_K_BEFORE_APP", DbType:="Int")>  _
        Public Property K_BEFORE_APP() As  System.Nullable(Of Long) 
            Get
                Return _K_BEFORE_APP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_BEFORE_APP = value
            End Set
        End Property 
        <Column(Storage:="_K_CANCEL", DbType:="Int")>  _
        Public Property K_CANCEL() As  System.Nullable(Of Long) 
            Get
                Return _K_CANCEL
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_K_DISABLE", DbType:="Int")>  _
        Public Property K_DISABLE() As  System.Nullable(Of Long) 
            Get
                Return _K_DISABLE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_DISABLE = value
            End Set
        End Property 
        <Column(Storage:="_K_SERVE", DbType:="Int")>  _
        Public Property K_SERVE() As  System.Nullable(Of Long) 
            Get
                Return _K_SERVE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_K_REFRESH", DbType:="Int")>  _
        Public Property K_REFRESH() As  System.Nullable(Of Long) 
            Get
                Return _K_REFRESH
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_REFRESH = value
            End Set
        End Property 
        <Column(Storage:="_K_VDO", DbType:="Int")>  _
        Public Property K_VDO() As  System.Nullable(Of Long) 
            Get
                Return _K_VDO
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_VDO = value
            End Set
        End Property 
        <Column(Storage:="_K_LEN", DbType:="Int")>  _
        Public Property K_LEN() As  System.Nullable(Of Long) 
            Get
                Return _K_LEN
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_LEN = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE1", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE1() As  String 
            Get
                Return _K_MOBILE1
            End Get
            Set(ByVal value As  String )
               _K_MOBILE1 = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE2", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE2() As  String 
            Get
                Return _K_MOBILE2
            End Get
            Set(ByVal value As  String )
               _K_MOBILE2 = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE3", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE3() As  String 
            Get
                Return _K_MOBILE3
            End Get
            Set(ByVal value As  String )
               _K_MOBILE3 = value
            End Set
        End Property 
        <Column(Storage:="_K_MOBILE4", DbType:="VarChar(2)")>  _
        Public Property K_MOBILE4() As  String 
            Get
                Return _K_MOBILE4
            End Get
            Set(ByVal value As  String )
               _K_MOBILE4 = value
            End Set
        End Property 
        <Column(Storage:="_K_RETARDATION_VDO", DbType:="Int")>  _
        Public Property K_RETARDATION_VDO() As  System.Nullable(Of Long) 
            Get
                Return _K_RETARDATION_VDO
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _K_RETARDATION_VDO = value
            End Set
        End Property 
        <Column(Storage:="_EVENT_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property EVENT_STATUS() As Char
            Get
                Return _EVENT_STATUS
            End Get
            Set(ByVal value As Char)
               _EVENT_STATUS = value
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
            _EVENT_DATE = New DateTime(1,1,1)
            _K_BEFORE = 0
            _K_LATE = 0
            _K_MAX_APPOINTMENT = 0
            _K_BEFORE_APP = 0
            _K_CANCEL = 0
            _K_DISABLE = 0
            _K_SERVE = 0
            _K_REFRESH = 0
            _K_VDO = 0
            _K_LEN = 0
            _K_MOBILE1 = ""
            _K_MOBILE2 = ""
            _K_MOBILE3 = ""
            _K_MOBILE4 = ""
            _K_RETARDATION_VDO = 0
            _EVENT_STATUS = ""
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


        '/// Returns an indication whether the current data is inserted into TB_CFG_SW_SCHED_KIOSK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CFG_SW_SCHED_KIOSK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CFG_SW_SCHED_KIOSK table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_CFG_SW_SCHED_KIOSK table successfully.
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


        '/// Returns an indication whether the record of TB_CFG_SW_SCHED_KIOSK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_CFG_SW_SCHED_KIOSK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbCfgSwSchedKioskCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_CFG_SW_SCHED_KIOSK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbCfgSwSchedKioskCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_CFG_SW_SCHED_KIOSK by specified EVENT_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cEVENT_DATE_SHOP_ID>The EVENT_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByEVENT_DATE_SHOP_ID(cEVENT_DATE As DateTime, cSHOP_ID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("EVENT_DATE = " & DB.SetDateTime(cEVENT_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID), trans)
        End Function

        '/// Returns an duplicate data record of TB_CFG_SW_SCHED_KIOSK by specified EVENT_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cEVENT_DATE_SHOP_ID>The EVENT_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByEVENT_DATE_SHOP_ID(cEVENT_DATE As DateTime, cSHOP_ID As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("EVENT_DATE = " & DB.SetDateTime(cEVENT_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_CFG_SW_SCHED_KIOSK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_CFG_SW_SCHED_KIOSK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_CFG_SW_SCHED_KIOSK table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_CFG_SW_SCHED_KIOSK table successfully.
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


        '/// Returns an indication whether the record of TB_CFG_SW_SCHED_KIOSK by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("event_date")) = False Then _event_date = Convert.ToDateTime(Rdr("event_date"))
                        If Convert.IsDBNull(Rdr("k_before")) = False Then _k_before = Convert.ToInt32(Rdr("k_before"))
                        If Convert.IsDBNull(Rdr("k_late")) = False Then _k_late = Convert.ToInt32(Rdr("k_late"))
                        If Convert.IsDBNull(Rdr("k_max_appointment")) = False Then _k_max_appointment = Convert.ToInt32(Rdr("k_max_appointment"))
                        If Convert.IsDBNull(Rdr("k_before_app")) = False Then _k_before_app = Convert.ToInt32(Rdr("k_before_app"))
                        If Convert.IsDBNull(Rdr("k_cancel")) = False Then _k_cancel = Convert.ToInt32(Rdr("k_cancel"))
                        If Convert.IsDBNull(Rdr("k_disable")) = False Then _k_disable = Convert.ToInt32(Rdr("k_disable"))
                        If Convert.IsDBNull(Rdr("k_serve")) = False Then _k_serve = Convert.ToInt32(Rdr("k_serve"))
                        If Convert.IsDBNull(Rdr("k_refresh")) = False Then _k_refresh = Convert.ToInt32(Rdr("k_refresh"))
                        If Convert.IsDBNull(Rdr("k_vdo")) = False Then _k_vdo = Convert.ToInt32(Rdr("k_vdo"))
                        If Convert.IsDBNull(Rdr("k_len")) = False Then _k_len = Convert.ToInt32(Rdr("k_len"))
                        If Convert.IsDBNull(Rdr("k_mobile1")) = False Then _k_mobile1 = Rdr("k_mobile1").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile2")) = False Then _k_mobile2 = Rdr("k_mobile2").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile3")) = False Then _k_mobile3 = Rdr("k_mobile3").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile4")) = False Then _k_mobile4 = Rdr("k_mobile4").ToString()
                        If Convert.IsDBNull(Rdr("k_retardation_vdo")) = False Then _k_retardation_vdo = Convert.ToInt32(Rdr("k_retardation_vdo"))
                        If Convert.IsDBNull(Rdr("event_status")) = False Then _event_status = Rdr("event_status").ToString()
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


        '/// Returns an indication whether the record of TB_CFG_SW_SCHED_KIOSK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbCfgSwSchedKioskCenLinqDB
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
                        If Convert.IsDBNull(Rdr("event_date")) = False Then _event_date = Convert.ToDateTime(Rdr("event_date"))
                        If Convert.IsDBNull(Rdr("k_before")) = False Then _k_before = Convert.ToInt32(Rdr("k_before"))
                        If Convert.IsDBNull(Rdr("k_late")) = False Then _k_late = Convert.ToInt32(Rdr("k_late"))
                        If Convert.IsDBNull(Rdr("k_max_appointment")) = False Then _k_max_appointment = Convert.ToInt32(Rdr("k_max_appointment"))
                        If Convert.IsDBNull(Rdr("k_before_app")) = False Then _k_before_app = Convert.ToInt32(Rdr("k_before_app"))
                        If Convert.IsDBNull(Rdr("k_cancel")) = False Then _k_cancel = Convert.ToInt32(Rdr("k_cancel"))
                        If Convert.IsDBNull(Rdr("k_disable")) = False Then _k_disable = Convert.ToInt32(Rdr("k_disable"))
                        If Convert.IsDBNull(Rdr("k_serve")) = False Then _k_serve = Convert.ToInt32(Rdr("k_serve"))
                        If Convert.IsDBNull(Rdr("k_refresh")) = False Then _k_refresh = Convert.ToInt32(Rdr("k_refresh"))
                        If Convert.IsDBNull(Rdr("k_vdo")) = False Then _k_vdo = Convert.ToInt32(Rdr("k_vdo"))
                        If Convert.IsDBNull(Rdr("k_len")) = False Then _k_len = Convert.ToInt32(Rdr("k_len"))
                        If Convert.IsDBNull(Rdr("k_mobile1")) = False Then _k_mobile1 = Rdr("k_mobile1").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile2")) = False Then _k_mobile2 = Rdr("k_mobile2").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile3")) = False Then _k_mobile3 = Rdr("k_mobile3").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile4")) = False Then _k_mobile4 = Rdr("k_mobile4").ToString()
                        If Convert.IsDBNull(Rdr("k_retardation_vdo")) = False Then _k_retardation_vdo = Convert.ToInt32(Rdr("k_retardation_vdo"))
                        If Convert.IsDBNull(Rdr("event_status")) = False Then _event_status = Rdr("event_status").ToString()
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


        '/// Returns an indication whether the Class Data of TB_CFG_SW_SCHED_KIOSK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbCfgSwSchedKioskCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbCfgSwSchedKioskCenParaDB
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
                        If Convert.IsDBNull(Rdr("event_date")) = False Then ret.event_date = Convert.ToDateTime(Rdr("event_date"))
                        If Convert.IsDBNull(Rdr("k_before")) = False Then ret.k_before = Convert.ToInt32(Rdr("k_before"))
                        If Convert.IsDBNull(Rdr("k_late")) = False Then ret.k_late = Convert.ToInt32(Rdr("k_late"))
                        If Convert.IsDBNull(Rdr("k_max_appointment")) = False Then ret.k_max_appointment = Convert.ToInt32(Rdr("k_max_appointment"))
                        If Convert.IsDBNull(Rdr("k_before_app")) = False Then ret.k_before_app = Convert.ToInt32(Rdr("k_before_app"))
                        If Convert.IsDBNull(Rdr("k_cancel")) = False Then ret.k_cancel = Convert.ToInt32(Rdr("k_cancel"))
                        If Convert.IsDBNull(Rdr("k_disable")) = False Then ret.k_disable = Convert.ToInt32(Rdr("k_disable"))
                        If Convert.IsDBNull(Rdr("k_serve")) = False Then ret.k_serve = Convert.ToInt32(Rdr("k_serve"))
                        If Convert.IsDBNull(Rdr("k_refresh")) = False Then ret.k_refresh = Convert.ToInt32(Rdr("k_refresh"))
                        If Convert.IsDBNull(Rdr("k_vdo")) = False Then ret.k_vdo = Convert.ToInt32(Rdr("k_vdo"))
                        If Convert.IsDBNull(Rdr("k_len")) = False Then ret.k_len = Convert.ToInt32(Rdr("k_len"))
                        If Convert.IsDBNull(Rdr("k_mobile1")) = False Then ret.k_mobile1 = Rdr("k_mobile1").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile2")) = False Then ret.k_mobile2 = Rdr("k_mobile2").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile3")) = False Then ret.k_mobile3 = Rdr("k_mobile3").ToString()
                        If Convert.IsDBNull(Rdr("k_mobile4")) = False Then ret.k_mobile4 = Rdr("k_mobile4").ToString()
                        If Convert.IsDBNull(Rdr("k_retardation_vdo")) = False Then ret.k_retardation_vdo = Convert.ToInt32(Rdr("k_retardation_vdo"))
                        If Convert.IsDBNull(Rdr("event_status")) = False Then ret.event_status = Rdr("event_status").ToString()

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


        'Get Insert Statement for table TB_CFG_SW_SCHED_KIOSK
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, EVENT_DATE, K_BEFORE, K_LATE, K_MAX_APPOINTMENT, K_BEFORE_APP, K_CANCEL, K_DISABLE, K_SERVE, K_REFRESH, K_VDO, K_LEN, K_MOBILE1, K_MOBILE2, K_MOBILE3, K_MOBILE4, K_RETARDATION_VDO, EVENT_STATUS)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetDateTime(_EVENT_DATE) & ", "
                sql += DB.SetDouble(_K_BEFORE) & ", "
                sql += DB.SetDouble(_K_LATE) & ", "
                sql += DB.SetDouble(_K_MAX_APPOINTMENT) & ", "
                sql += DB.SetDouble(_K_BEFORE_APP) & ", "
                sql += DB.SetDouble(_K_CANCEL) & ", "
                sql += DB.SetDouble(_K_DISABLE) & ", "
                sql += DB.SetDouble(_K_SERVE) & ", "
                sql += DB.SetDouble(_K_REFRESH) & ", "
                sql += DB.SetDouble(_K_VDO) & ", "
                sql += DB.SetDouble(_K_LEN) & ", "
                sql += DB.SetString(_K_MOBILE1) & ", "
                sql += DB.SetString(_K_MOBILE2) & ", "
                sql += DB.SetString(_K_MOBILE3) & ", "
                sql += DB.SetString(_K_MOBILE4) & ", "
                sql += DB.SetDouble(_K_RETARDATION_VDO) & ", "
                sql += DB.SetString(_EVENT_STATUS)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_CFG_SW_SCHED_KIOSK
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "EVENT_DATE = " & DB.SetDateTime(_EVENT_DATE) & ", "
                Sql += "K_BEFORE = " & DB.SetDouble(_K_BEFORE) & ", "
                Sql += "K_LATE = " & DB.SetDouble(_K_LATE) & ", "
                Sql += "K_MAX_APPOINTMENT = " & DB.SetDouble(_K_MAX_APPOINTMENT) & ", "
                Sql += "K_BEFORE_APP = " & DB.SetDouble(_K_BEFORE_APP) & ", "
                Sql += "K_CANCEL = " & DB.SetDouble(_K_CANCEL) & ", "
                Sql += "K_DISABLE = " & DB.SetDouble(_K_DISABLE) & ", "
                Sql += "K_SERVE = " & DB.SetDouble(_K_SERVE) & ", "
                Sql += "K_REFRESH = " & DB.SetDouble(_K_REFRESH) & ", "
                Sql += "K_VDO = " & DB.SetDouble(_K_VDO) & ", "
                Sql += "K_LEN = " & DB.SetDouble(_K_LEN) & ", "
                Sql += "K_MOBILE1 = " & DB.SetString(_K_MOBILE1) & ", "
                Sql += "K_MOBILE2 = " & DB.SetString(_K_MOBILE2) & ", "
                Sql += "K_MOBILE3 = " & DB.SetString(_K_MOBILE3) & ", "
                Sql += "K_MOBILE4 = " & DB.SetString(_K_MOBILE4) & ", "
                Sql += "K_RETARDATION_VDO = " & DB.SetDouble(_K_RETARDATION_VDO) & ", "
                Sql += "EVENT_STATUS = " & DB.SetString(_EVENT_STATUS) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_CFG_SW_SCHED_KIOSK
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_CFG_SW_SCHED_KIOSK
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, EVENT_DATE, K_BEFORE, K_LATE, K_MAX_APPOINTMENT, K_BEFORE_APP, K_CANCEL, K_DISABLE, K_SERVE, K_REFRESH, K_VDO, K_LEN, K_MOBILE1, K_MOBILE2, K_MOBILE3, K_MOBILE4, K_RETARDATION_VDO, EVENT_STATUS FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
