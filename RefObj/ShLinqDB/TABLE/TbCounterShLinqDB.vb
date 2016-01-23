Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = ShLinqDB.Common.Utilities.SQLDB
Imports ShParaDB.TABLE
Imports ShParaDB.Common.Utilities

Namespace TABLE
    'Represents a transaction for TB_COUNTER table ShLinqDB.
    '[Create by  on Febuary, 27 2012]
    Public Class TbCounterShLinqDB
        Public sub TbCounterShLinqDB()

        End Sub 
        ' TB_COUNTER
        Const _tableName As String = "TB_COUNTER"
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
        Dim _COUNTER_CODE As  String  = ""
        Dim _COUNTER_NAME As  String  = ""
        Dim _COUNTER_STATUS As  System.Nullable(Of Long)  = 0
        Dim _QUICKSERVICE As  System.Nullable(Of Long)  = 0
        Dim _BEEP As  System.Nullable(Of Long)  = 0
        Dim _RETURN_CASE As  System.Nullable(Of Long)  = 0
        Dim _AUTO_SWAP As  System.Nullable(Of Long)  = 0
        Dim _SPEED_LANE As  System.Nullable(Of Long)  = 0
        Dim _UNITDISPLAY As  String  = ""
        Dim _AVAILABLE As  System.Nullable(Of Long)  = 0
        Dim _BACK_OFFICE As  System.Nullable(Of Long)  = 0
        Dim _COUNTER_MANAGER As  System.Nullable(Of Long)  = 0
        Dim _ACTIVE_STATUS As  System.Nullable(Of Long)  = 0
        Dim _CREATE_BY As  String  = ""
        Dim _CREATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

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
        <Column(Storage:="_COUNTER_CODE", DbType:="VarChar(20)")>  _
        Public Property COUNTER_CODE() As  String 
            Get
                Return _COUNTER_CODE
            End Get
            Set(ByVal value As  String )
               _COUNTER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_NAME", DbType:="VarChar(50)")>  _
        Public Property COUNTER_NAME() As  String 
            Get
                Return _COUNTER_NAME
            End Get
            Set(ByVal value As  String )
               _COUNTER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_STATUS", DbType:="SmallInt")>  _
        Public Property COUNTER_STATUS() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_STATUS
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_QUICKSERVICE", DbType:="SmallInt")>  _
        Public Property QUICKSERVICE() As  System.Nullable(Of Long) 
            Get
                Return _QUICKSERVICE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _QUICKSERVICE = value
            End Set
        End Property 
        <Column(Storage:="_BEEP", DbType:="SmallInt")>  _
        Public Property BEEP() As  System.Nullable(Of Long) 
            Get
                Return _BEEP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BEEP = value
            End Set
        End Property 
        <Column(Storage:="_RETURN_CASE", DbType:="SmallInt")>  _
        Public Property RETURN_CASE() As  System.Nullable(Of Long) 
            Get
                Return _RETURN_CASE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _RETURN_CASE = value
            End Set
        End Property 
        <Column(Storage:="_AUTO_SWAP", DbType:="SmallInt")>  _
        Public Property AUTO_SWAP() As  System.Nullable(Of Long) 
            Get
                Return _AUTO_SWAP
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _AUTO_SWAP = value
            End Set
        End Property 
        <Column(Storage:="_SPEED_LANE", DbType:="SmallInt")>  _
        Public Property SPEED_LANE() As  System.Nullable(Of Long) 
            Get
                Return _SPEED_LANE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _SPEED_LANE = value
            End Set
        End Property 
        <Column(Storage:="_UNITDISPLAY", DbType:="VarChar(5)")>  _
        Public Property UNITDISPLAY() As  String 
            Get
                Return _UNITDISPLAY
            End Get
            Set(ByVal value As  String )
               _UNITDISPLAY = value
            End Set
        End Property 
        <Column(Storage:="_AVAILABLE", DbType:="SmallInt")>  _
        Public Property AVAILABLE() As  System.Nullable(Of Long) 
            Get
                Return _AVAILABLE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _AVAILABLE = value
            End Set
        End Property 
        <Column(Storage:="_BACK_OFFICE", DbType:="SmallInt")>  _
        Public Property BACK_OFFICE() As  System.Nullable(Of Long) 
            Get
                Return _BACK_OFFICE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _BACK_OFFICE = value
            End Set
        End Property 
        <Column(Storage:="_COUNTER_MANAGER", DbType:="SmallInt")>  _
        Public Property COUNTER_MANAGER() As  System.Nullable(Of Long) 
            Get
                Return _COUNTER_MANAGER
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _COUNTER_MANAGER = value
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
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20)")>  _
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


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _COUNTER_CODE = ""
            _COUNTER_NAME = ""
            _COUNTER_STATUS = 0
            _QUICKSERVICE = 0
            _BEEP = 0
            _RETURN_CASE = 0
            _AUTO_SWAP = 0
            _SPEED_LANE = 0
            _UNITDISPLAY = ""
            _AVAILABLE = 0
            _BACK_OFFICE = 0
            _COUNTER_MANAGER = 0
            _ACTIVE_STATUS = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
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


        '/// Returns an indication whether the current data is inserted into TB_COUNTER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_COUNTER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_COUNTER table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_COUNTER table successfully.
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


        '/// Returns an indication whether the record of TB_COUNTER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_COUNTER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Integer, trans As SQLTransaction) As TbCounterShLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_COUNTER by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Integer, trans As SQLTransaction) As TbCounterShParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_COUNTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_COUNTER table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_COUNTER table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = True Then
                If whText.Trim() <> ""
                    Dim tmpWhere As String = " Where " & whText
                    Try

                        ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = DB.ErrorMessage
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
            Else
                ret = False
                _error = MessageResources.MSGEU002
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TB_COUNTER table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> ""
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


        '/// Returns an indication whether the record of TB_COUNTER by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("counter_code")) = False Then _counter_code = Rdr("counter_code").ToString()
                        If Convert.IsDBNull(Rdr("counter_name")) = False Then _counter_name = Rdr("counter_name").ToString()
                        If Convert.IsDBNull(Rdr("counter_status")) = False Then _counter_status = Convert.ToInt16(Rdr("counter_status"))
                        If Convert.IsDBNull(Rdr("quickservice")) = False Then _quickservice = Convert.ToInt16(Rdr("quickservice"))
                        If Convert.IsDBNull(Rdr("beep")) = False Then _beep = Convert.ToInt16(Rdr("beep"))
                        If Convert.IsDBNull(Rdr("return_case")) = False Then _return_case = Convert.ToInt16(Rdr("return_case"))
                        If Convert.IsDBNull(Rdr("auto_swap")) = False Then _auto_swap = Convert.ToInt16(Rdr("auto_swap"))
                        If Convert.IsDBNull(Rdr("speed_lane")) = False Then _speed_lane = Convert.ToInt16(Rdr("speed_lane"))
                        If Convert.IsDBNull(Rdr("unitdisplay")) = False Then _unitdisplay = Rdr("unitdisplay").ToString()
                        If Convert.IsDBNull(Rdr("available")) = False Then _available = Convert.ToInt16(Rdr("available"))
                        If Convert.IsDBNull(Rdr("back_office")) = False Then _back_office = Convert.ToInt16(Rdr("back_office"))
                        If Convert.IsDBNull(Rdr("counter_manager")) = False Then _counter_manager = Convert.ToInt16(Rdr("counter_manager"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
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


        '/// Returns an indication whether the record of TB_COUNTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbCounterShLinqDB
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
                        If Convert.IsDBNull(Rdr("counter_code")) = False Then _counter_code = Rdr("counter_code").ToString()
                        If Convert.IsDBNull(Rdr("counter_name")) = False Then _counter_name = Rdr("counter_name").ToString()
                        If Convert.IsDBNull(Rdr("counter_status")) = False Then _counter_status = Convert.ToInt16(Rdr("counter_status"))
                        If Convert.IsDBNull(Rdr("quickservice")) = False Then _quickservice = Convert.ToInt16(Rdr("quickservice"))
                        If Convert.IsDBNull(Rdr("beep")) = False Then _beep = Convert.ToInt16(Rdr("beep"))
                        If Convert.IsDBNull(Rdr("return_case")) = False Then _return_case = Convert.ToInt16(Rdr("return_case"))
                        If Convert.IsDBNull(Rdr("auto_swap")) = False Then _auto_swap = Convert.ToInt16(Rdr("auto_swap"))
                        If Convert.IsDBNull(Rdr("speed_lane")) = False Then _speed_lane = Convert.ToInt16(Rdr("speed_lane"))
                        If Convert.IsDBNull(Rdr("unitdisplay")) = False Then _unitdisplay = Rdr("unitdisplay").ToString()
                        If Convert.IsDBNull(Rdr("available")) = False Then _available = Convert.ToInt16(Rdr("available"))
                        If Convert.IsDBNull(Rdr("back_office")) = False Then _back_office = Convert.ToInt16(Rdr("back_office"))
                        If Convert.IsDBNull(Rdr("counter_manager")) = False Then _counter_manager = Convert.ToInt16(Rdr("counter_manager"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then _active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))

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


        '/// Returns an indication whether the Class Data of TB_COUNTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbCounterShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbCounterShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt32(Rdr("id"))
                        If Convert.IsDBNull(Rdr("counter_code")) = False Then ret.counter_code = Rdr("counter_code").ToString()
                        If Convert.IsDBNull(Rdr("counter_name")) = False Then ret.counter_name = Rdr("counter_name").ToString()
                        If Convert.IsDBNull(Rdr("counter_status")) = False Then ret.counter_status = Convert.ToInt16(Rdr("counter_status"))
                        If Convert.IsDBNull(Rdr("quickservice")) = False Then ret.quickservice = Convert.ToInt16(Rdr("quickservice"))
                        If Convert.IsDBNull(Rdr("beep")) = False Then ret.beep = Convert.ToInt16(Rdr("beep"))
                        If Convert.IsDBNull(Rdr("return_case")) = False Then ret.return_case = Convert.ToInt16(Rdr("return_case"))
                        If Convert.IsDBNull(Rdr("auto_swap")) = False Then ret.auto_swap = Convert.ToInt16(Rdr("auto_swap"))
                        If Convert.IsDBNull(Rdr("speed_lane")) = False Then ret.speed_lane = Convert.ToInt16(Rdr("speed_lane"))
                        If Convert.IsDBNull(Rdr("unitdisplay")) = False Then ret.unitdisplay = Rdr("unitdisplay").ToString()
                        If Convert.IsDBNull(Rdr("available")) = False Then ret.available = Convert.ToInt16(Rdr("available"))
                        If Convert.IsDBNull(Rdr("back_office")) = False Then ret.back_office = Convert.ToInt16(Rdr("back_office"))
                        If Convert.IsDBNull(Rdr("counter_manager")) = False Then ret.counter_manager = Convert.ToInt16(Rdr("counter_manager"))
                        If Convert.IsDBNull(Rdr("active_status")) = False Then ret.active_status = Convert.ToInt16(Rdr("active_status"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))

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


        'Get Insert Statement for table TB_COUNTER
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, COUNTER_CODE, COUNTER_NAME, COUNTER_STATUS, QUICKSERVICE, BEEP, RETURN_CASE, AUTO_SWAP, SPEED_LANE, UNITDISPLAY, AVAILABLE, BACK_OFFICE, COUNTER_MANAGER, ACTIVE_STATUS, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_COUNTER_CODE) & ", "
                sql += DB.SetString(_COUNTER_NAME) & ", "
                sql += DB.SetDouble(_COUNTER_STATUS) & ", "
                sql += DB.SetDouble(_QUICKSERVICE) & ", "
                sql += DB.SetDouble(_BEEP) & ", "
                sql += DB.SetDouble(_RETURN_CASE) & ", "
                sql += DB.SetDouble(_AUTO_SWAP) & ", "
                sql += DB.SetDouble(_SPEED_LANE) & ", "
                sql += DB.SetString(_UNITDISPLAY) & ", "
                sql += DB.SetDouble(_AVAILABLE) & ", "
                sql += DB.SetDouble(_BACK_OFFICE) & ", "
                sql += DB.SetDouble(_COUNTER_MANAGER) & ", "
                sql += DB.SetDouble(_ACTIVE_STATUS) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_COUNTER
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "COUNTER_CODE = " & DB.SetString(_COUNTER_CODE) & ", "
                Sql += "COUNTER_NAME = " & DB.SetString(_COUNTER_NAME) & ", "
                Sql += "COUNTER_STATUS = " & DB.SetDouble(_COUNTER_STATUS) & ", "
                Sql += "QUICKSERVICE = " & DB.SetDouble(_QUICKSERVICE) & ", "
                Sql += "BEEP = " & DB.SetDouble(_BEEP) & ", "
                Sql += "RETURN_CASE = " & DB.SetDouble(_RETURN_CASE) & ", "
                Sql += "AUTO_SWAP = " & DB.SetDouble(_AUTO_SWAP) & ", "
                Sql += "SPEED_LANE = " & DB.SetDouble(_SPEED_LANE) & ", "
                Sql += "UNITDISPLAY = " & DB.SetString(_UNITDISPLAY) & ", "
                Sql += "AVAILABLE = " & DB.SetDouble(_AVAILABLE) & ", "
                Sql += "BACK_OFFICE = " & DB.SetDouble(_BACK_OFFICE) & ", "
                Sql += "COUNTER_MANAGER = " & DB.SetDouble(_COUNTER_MANAGER) & ", "
                Sql += "ACTIVE_STATUS = " & DB.SetDouble(_ACTIVE_STATUS) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_COUNTER
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_COUNTER
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
