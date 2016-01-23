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
    'Represents a transaction for TB_REP_WT_HT_SHOP_MONTH table CenLinqDB.
    '[Create by  on March, 12 2013]
    Public Class TbRepWtHtShopMonthCenLinqDB
        Public sub TbRepWtHtShopMonthCenLinqDB()

        End Sub 
        ' TB_REP_WT_HT_SHOP_MONTH
        Const _tableName As String = "TB_REP_WT_HT_SHOP_MONTH"
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
        Dim _REGIS As Long = 0
        Dim _SERVE As Long = 0
        Dim _MISSED_CALL As Long = 0
        Dim _CANCLE As Long = 0
        Dim _INCOMPLETE As Long = 0
        Dim _AWT As Long = 0
        Dim _AHT As Long = 0
        Dim _ACT As Long = 0
        Dim _COUNT_WT As Long = 0
        Dim _SUM_WT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _SUM_HT As Long = 0
        Dim _COUNT_CT As Long = 0
        Dim _SUM_CT As Long = 0

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
        <Column(Storage:="_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
               _REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE() As Long
            Get
                Return _SERVE
            End Get
            Set(ByVal value As Long)
               _SERVE = value
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
        <Column(Storage:="_CANCLE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCLE() As Long
            Get
                Return _CANCLE
            End Get
            Set(ByVal value As Long)
               _CANCLE = value
            End Set
        End Property 
        <Column(Storage:="_INCOMPLETE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property INCOMPLETE() As Long
            Get
                Return _INCOMPLETE
            End Get
            Set(ByVal value As Long)
               _INCOMPLETE = value
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
        <Column(Storage:="_ACT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ACT() As Long
            Get
                Return _ACT
            End Get
            Set(ByVal value As Long)
               _ACT = value
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
        <Column(Storage:="_COUNT_CT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_CT() As Long
            Get
                Return _COUNT_CT
            End Get
            Set(ByVal value As Long)
               _COUNT_CT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_CT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_CT() As Long
            Get
                Return _SUM_CT
            End Get
            Set(ByVal value As Long)
               _SUM_CT = value
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
            _REGIS = 0
            _SERVE = 0
            _MISSED_CALL = 0
            _CANCLE = 0
            _INCOMPLETE = 0
            _AWT = 0
            _AHT = 0
            _ACT = 0
            _COUNT_WT = 0
            _SUM_WT = 0
            _COUNT_HT = 0
            _SUM_HT = 0
            _COUNT_CT = 0
            _SUM_CT = 0
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


        '/// Returns an indication whether the current data is inserted into TB_REP_WT_HT_SHOP_MONTH table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_SHOP_MONTH table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_SHOP_MONTH table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_WT_HT_SHOP_MONTH table successfully.
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_SHOP_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_WT_HT_SHOP_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepWtHtShopMonthCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_WT_HT_SHOP_MONTH by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepWtHtShopMonthCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_SHOP_MONTH by specified MONTH_NO_SHOP_ID_SHOW_YEAR key is retrieved successfully.
        '/// <param name=cMONTH_NO_SHOP_ID_SHOW_YEAR>The MONTH_NO_SHOP_ID_SHOW_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMONTH_NO_SHOP_ID_SHOW_YEAR(cMONTH_NO As Integer, cSHOP_ID As Integer, cSHOW_YEAR As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("MONTH_NO = " & DB.SetDouble(cMONTH_NO) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_WT_HT_SHOP_MONTH by specified MONTH_NO_SHOP_ID_SHOW_YEAR key is retrieved successfully.
        '/// <param name=cMONTH_NO_SHOP_ID_SHOW_YEAR>The MONTH_NO_SHOP_ID_SHOW_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMONTH_NO_SHOP_ID_SHOW_YEAR(cMONTH_NO As Integer, cSHOP_ID As Integer, cSHOW_YEAR As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MONTH_NO = " & DB.SetDouble(cMONTH_NO) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_WT_HT_SHOP_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_WT_HT_SHOP_MONTH table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_WT_HT_SHOP_MONTH table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_WT_HT_SHOP_MONTH table successfully.
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_SHOP_MONTH by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then _serve = Convert.ToInt32(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then _missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancle")) = False Then _cancle = Convert.ToInt32(Rdr("cancle"))
                        If Convert.IsDBNull(Rdr("incomplete")) = False Then _incomplete = Convert.ToInt32(Rdr("incomplete"))
                        If Convert.IsDBNull(Rdr("awt")) = False Then _awt = Convert.ToInt32(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt32(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("act")) = False Then _act = Convert.ToInt32(Rdr("act"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then _count_wt = Convert.ToInt32(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then _sum_wt = Convert.ToInt32(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt32(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt32(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("count_ct")) = False Then _count_ct = Convert.ToInt32(Rdr("count_ct"))
                        If Convert.IsDBNull(Rdr("sum_ct")) = False Then _sum_ct = Convert.ToInt32(Rdr("sum_ct"))
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


        '/// Returns an indication whether the record of TB_REP_WT_HT_SHOP_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepWtHtShopMonthCenLinqDB
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
                        If Convert.IsDBNull(Rdr("regis")) = False Then _regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then _serve = Convert.ToInt32(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then _missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancle")) = False Then _cancle = Convert.ToInt32(Rdr("cancle"))
                        If Convert.IsDBNull(Rdr("incomplete")) = False Then _incomplete = Convert.ToInt32(Rdr("incomplete"))
                        If Convert.IsDBNull(Rdr("awt")) = False Then _awt = Convert.ToInt32(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then _aht = Convert.ToInt32(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("act")) = False Then _act = Convert.ToInt32(Rdr("act"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then _count_wt = Convert.ToInt32(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then _sum_wt = Convert.ToInt32(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then _count_ht = Convert.ToInt32(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then _sum_ht = Convert.ToInt32(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("count_ct")) = False Then _count_ct = Convert.ToInt32(Rdr("count_ct"))
                        If Convert.IsDBNull(Rdr("sum_ct")) = False Then _sum_ct = Convert.ToInt32(Rdr("sum_ct"))
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


        '/// Returns an indication whether the Class Data of TB_REP_WT_HT_SHOP_MONTH by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepWtHtShopMonthCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepWtHtShopMonthCenParaDB
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
                        If Convert.IsDBNull(Rdr("regis")) = False Then ret.regis = Convert.ToInt32(Rdr("regis"))
                        If Convert.IsDBNull(Rdr("serve")) = False Then ret.serve = Convert.ToInt32(Rdr("serve"))
                        If Convert.IsDBNull(Rdr("missed_call")) = False Then ret.missed_call = Convert.ToInt32(Rdr("missed_call"))
                        If Convert.IsDBNull(Rdr("cancle")) = False Then ret.cancle = Convert.ToInt32(Rdr("cancle"))
                        If Convert.IsDBNull(Rdr("incomplete")) = False Then ret.incomplete = Convert.ToInt32(Rdr("incomplete"))
                        If Convert.IsDBNull(Rdr("awt")) = False Then ret.awt = Convert.ToInt32(Rdr("awt"))
                        If Convert.IsDBNull(Rdr("aht")) = False Then ret.aht = Convert.ToInt32(Rdr("aht"))
                        If Convert.IsDBNull(Rdr("act")) = False Then ret.act = Convert.ToInt32(Rdr("act"))
                        If Convert.IsDBNull(Rdr("count_wt")) = False Then ret.count_wt = Convert.ToInt32(Rdr("count_wt"))
                        If Convert.IsDBNull(Rdr("sum_wt")) = False Then ret.sum_wt = Convert.ToInt32(Rdr("sum_wt"))
                        If Convert.IsDBNull(Rdr("count_ht")) = False Then ret.count_ht = Convert.ToInt32(Rdr("count_ht"))
                        If Convert.IsDBNull(Rdr("sum_ht")) = False Then ret.sum_ht = Convert.ToInt32(Rdr("sum_ht"))
                        If Convert.IsDBNull(Rdr("count_ct")) = False Then ret.count_ct = Convert.ToInt32(Rdr("count_ct"))
                        If Convert.IsDBNull(Rdr("sum_ct")) = False Then ret.sum_ct = Convert.ToInt32(Rdr("sum_ct"))

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


        'Get Insert Statement for table TB_REP_WT_HT_SHOP_MONTH
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, MONTH_NO, SHOW_MONTH, SHOW_YEAR, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, REGIS, SERVE, MISSED_CALL, CANCLE, INCOMPLETE, AWT, AHT, ACT, COUNT_WT, SUM_WT, COUNT_HT, SUM_HT, COUNT_CT, SUM_CT)"
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
                sql += DB.SetDouble(_REGIS) & ", "
                sql += DB.SetDouble(_SERVE) & ", "
                sql += DB.SetDouble(_MISSED_CALL) & ", "
                sql += DB.SetDouble(_CANCLE) & ", "
                sql += DB.SetDouble(_INCOMPLETE) & ", "
                sql += DB.SetDouble(_AWT) & ", "
                sql += DB.SetDouble(_AHT) & ", "
                sql += DB.SetDouble(_ACT) & ", "
                sql += DB.SetDouble(_COUNT_WT) & ", "
                sql += DB.SetDouble(_SUM_WT) & ", "
                sql += DB.SetDouble(_COUNT_HT) & ", "
                sql += DB.SetDouble(_SUM_HT) & ", "
                sql += DB.SetDouble(_COUNT_CT) & ", "
                sql += DB.SetDouble(_SUM_CT)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_WT_HT_SHOP_MONTH
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
                Sql += "REGIS = " & DB.SetDouble(_REGIS) & ", "
                Sql += "SERVE = " & DB.SetDouble(_SERVE) & ", "
                Sql += "MISSED_CALL = " & DB.SetDouble(_MISSED_CALL) & ", "
                Sql += "CANCLE = " & DB.SetDouble(_CANCLE) & ", "
                Sql += "INCOMPLETE = " & DB.SetDouble(_INCOMPLETE) & ", "
                Sql += "AWT = " & DB.SetDouble(_AWT) & ", "
                Sql += "AHT = " & DB.SetDouble(_AHT) & ", "
                Sql += "ACT = " & DB.SetDouble(_ACT) & ", "
                Sql += "COUNT_WT = " & DB.SetDouble(_COUNT_WT) & ", "
                Sql += "SUM_WT = " & DB.SetDouble(_SUM_WT) & ", "
                Sql += "COUNT_HT = " & DB.SetDouble(_COUNT_HT) & ", "
                Sql += "SUM_HT = " & DB.SetDouble(_SUM_HT) & ", "
                Sql += "COUNT_CT = " & DB.SetDouble(_COUNT_CT) & ", "
                Sql += "SUM_CT = " & DB.SetDouble(_SUM_CT) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_WT_HT_SHOP_MONTH
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_WT_HT_SHOP_MONTH
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, MONTH_NO, SHOW_MONTH, SHOW_YEAR, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, REGIS, SERVE, MISSED_CALL, CANCLE, INCOMPLETE, AWT, AHT, ACT, COUNT_WT, SUM_WT, COUNT_HT, SUM_HT, COUNT_CT, SUM_CT FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
