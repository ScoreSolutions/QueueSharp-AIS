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
    'Represents a transaction for TB_REP_APPOINTMENT_SHOP_WEEK table CenLinqDB.
    '[Create by  on March, 23 2013]
    Public Class TbRepAppointmentShopWeekCenLinqDB
        Public sub TbRepAppointmentShopWeekCenLinqDB()

        End Sub 
        ' TB_REP_APPOINTMENT_SHOP_WEEK
        Const _tableName As String = "TB_REP_APPOINTMENT_SHOP_WEEK"
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
        Dim _WEEK_OF_YEAR As Long = 0
        Dim _SHOW_YEAR As Long = 0
        Dim _PERIOD_DATE As String = ""
        Dim _TOTAL As Long = 0
        Dim _KIOSK_SHOW As Long = 0
        Dim _KIOSK_NOSHOW As Long = 0
        Dim _WEB_SHOW As Long = 0
        Dim _WEB_NOSHOW As Long = 0
        Dim _MOBILE_SHOW As Long = 0
        Dim _MOBILE_NOSHOW As Long = 0

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
        <Column(Storage:="_WEEK_OF_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEEK_OF_YEAR() As Long
            Get
                Return _WEEK_OF_YEAR
            End Get
            Set(ByVal value As Long)
               _WEEK_OF_YEAR = value
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
        <Column(Storage:="_PERIOD_DATE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property PERIOD_DATE() As String
            Get
                Return _PERIOD_DATE
            End Get
            Set(ByVal value As String)
               _PERIOD_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TOTAL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOTAL() As Long
            Get
                Return _TOTAL
            End Get
            Set(ByVal value As Long)
               _TOTAL = value
            End Set
        End Property 
        <Column(Storage:="_KIOSK_SHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KIOSK_SHOW() As Long
            Get
                Return _KIOSK_SHOW
            End Get
            Set(ByVal value As Long)
               _KIOSK_SHOW = value
            End Set
        End Property 
        <Column(Storage:="_KIOSK_NOSHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KIOSK_NOSHOW() As Long
            Get
                Return _KIOSK_NOSHOW
            End Get
            Set(ByVal value As Long)
               _KIOSK_NOSHOW = value
            End Set
        End Property 
        <Column(Storage:="_WEB_SHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEB_SHOW() As Long
            Get
                Return _WEB_SHOW
            End Get
            Set(ByVal value As Long)
               _WEB_SHOW = value
            End Set
        End Property 
        <Column(Storage:="_WEB_NOSHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property WEB_NOSHOW() As Long
            Get
                Return _WEB_NOSHOW
            End Get
            Set(ByVal value As Long)
               _WEB_NOSHOW = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_SHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_SHOW() As Long
            Get
                Return _MOBILE_SHOW
            End Get
            Set(ByVal value As Long)
               _MOBILE_SHOW = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NOSHOW", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NOSHOW() As Long
            Get
                Return _MOBILE_NOSHOW
            End Get
            Set(ByVal value As Long)
               _MOBILE_NOSHOW = value
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
            _WEEK_OF_YEAR = 0
            _SHOW_YEAR = 0
            _PERIOD_DATE = ""
            _TOTAL = 0
            _KIOSK_SHOW = 0
            _KIOSK_NOSHOW = 0
            _WEB_SHOW = 0
            _WEB_NOSHOW = 0
            _MOBILE_SHOW = 0
            _MOBILE_NOSHOW = 0
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


        '/// Returns an indication whether the current data is inserted into TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
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


        '/// Returns an indication whether the record of TB_REP_APPOINTMENT_SHOP_WEEK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_APPOINTMENT_SHOP_WEEK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepAppointmentShopWeekCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_APPOINTMENT_SHOP_WEEK by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepAppointmentShopWeekCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_APPOINTMENT_SHOP_WEEK by specified SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key is retrieved successfully.
        '/// <param name=cSHOP_ID_SHOW_YEAR_WEEK_OF_YEAR>The SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySHOP_ID_SHOW_YEAR_WEEK_OF_YEAR(cSHOP_ID As Integer, cSHOW_YEAR As Integer, cWEEK_OF_YEAR As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " AND WEEK_OF_YEAR = " & DB.SetDouble(cWEEK_OF_YEAR), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_APPOINTMENT_SHOP_WEEK by specified SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key is retrieved successfully.
        '/// <param name=cSHOP_ID_SHOW_YEAR_WEEK_OF_YEAR>The SHOP_ID_SHOW_YEAR_WEEK_OF_YEAR key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySHOP_ID_SHOW_YEAR_WEEK_OF_YEAR(cSHOP_ID As Integer, cSHOW_YEAR As Integer, cWEEK_OF_YEAR As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " AND SHOW_YEAR = " & DB.SetDouble(cSHOW_YEAR) & " AND WEEK_OF_YEAR = " & DB.SetDouble(cWEEK_OF_YEAR) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_APPOINTMENT_SHOP_WEEK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_APPOINTMENT_SHOP_WEEK table successfully.
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


        '/// Returns an indication whether the record of TB_REP_APPOINTMENT_SHOP_WEEK by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("week_of_year")) = False Then _week_of_year = Convert.ToInt32(Rdr("week_of_year"))
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("period_date")) = False Then _period_date = Rdr("period_date").ToString()
                        If Convert.IsDBNull(Rdr("total")) = False Then _total = Convert.ToInt32(Rdr("total"))
                        If Convert.IsDBNull(Rdr("kiosk_show")) = False Then _kiosk_show = Convert.ToInt32(Rdr("kiosk_show"))
                        If Convert.IsDBNull(Rdr("kiosk_noshow")) = False Then _kiosk_noshow = Convert.ToInt32(Rdr("kiosk_noshow"))
                        If Convert.IsDBNull(Rdr("web_show")) = False Then _web_show = Convert.ToInt32(Rdr("web_show"))
                        If Convert.IsDBNull(Rdr("web_noshow")) = False Then _web_noshow = Convert.ToInt32(Rdr("web_noshow"))
                        If Convert.IsDBNull(Rdr("mobile_show")) = False Then _mobile_show = Convert.ToInt32(Rdr("mobile_show"))
                        If Convert.IsDBNull(Rdr("mobile_noshow")) = False Then _mobile_noshow = Convert.ToInt32(Rdr("mobile_noshow"))
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


        '/// Returns an indication whether the record of TB_REP_APPOINTMENT_SHOP_WEEK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepAppointmentShopWeekCenLinqDB
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
                        If Convert.IsDBNull(Rdr("week_of_year")) = False Then _week_of_year = Convert.ToInt32(Rdr("week_of_year"))
                        If Convert.IsDBNull(Rdr("show_year")) = False Then _show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("period_date")) = False Then _period_date = Rdr("period_date").ToString()
                        If Convert.IsDBNull(Rdr("total")) = False Then _total = Convert.ToInt32(Rdr("total"))
                        If Convert.IsDBNull(Rdr("kiosk_show")) = False Then _kiosk_show = Convert.ToInt32(Rdr("kiosk_show"))
                        If Convert.IsDBNull(Rdr("kiosk_noshow")) = False Then _kiosk_noshow = Convert.ToInt32(Rdr("kiosk_noshow"))
                        If Convert.IsDBNull(Rdr("web_show")) = False Then _web_show = Convert.ToInt32(Rdr("web_show"))
                        If Convert.IsDBNull(Rdr("web_noshow")) = False Then _web_noshow = Convert.ToInt32(Rdr("web_noshow"))
                        If Convert.IsDBNull(Rdr("mobile_show")) = False Then _mobile_show = Convert.ToInt32(Rdr("mobile_show"))
                        If Convert.IsDBNull(Rdr("mobile_noshow")) = False Then _mobile_noshow = Convert.ToInt32(Rdr("mobile_noshow"))
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


        '/// Returns an indication whether the Class Data of TB_REP_APPOINTMENT_SHOP_WEEK by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepAppointmentShopWeekCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepAppointmentShopWeekCenParaDB
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
                        If Convert.IsDBNull(Rdr("week_of_year")) = False Then ret.week_of_year = Convert.ToInt32(Rdr("week_of_year"))
                        If Convert.IsDBNull(Rdr("show_year")) = False Then ret.show_year = Convert.ToInt32(Rdr("show_year"))
                        If Convert.IsDBNull(Rdr("period_date")) = False Then ret.period_date = Rdr("period_date").ToString()
                        If Convert.IsDBNull(Rdr("total")) = False Then ret.total = Convert.ToInt32(Rdr("total"))
                        If Convert.IsDBNull(Rdr("kiosk_show")) = False Then ret.kiosk_show = Convert.ToInt32(Rdr("kiosk_show"))
                        If Convert.IsDBNull(Rdr("kiosk_noshow")) = False Then ret.kiosk_noshow = Convert.ToInt32(Rdr("kiosk_noshow"))
                        If Convert.IsDBNull(Rdr("web_show")) = False Then ret.web_show = Convert.ToInt32(Rdr("web_show"))
                        If Convert.IsDBNull(Rdr("web_noshow")) = False Then ret.web_noshow = Convert.ToInt32(Rdr("web_noshow"))
                        If Convert.IsDBNull(Rdr("mobile_show")) = False Then ret.mobile_show = Convert.ToInt32(Rdr("mobile_show"))
                        If Convert.IsDBNull(Rdr("mobile_noshow")) = False Then ret.mobile_noshow = Convert.ToInt32(Rdr("mobile_noshow"))

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


        'Get Insert Statement for table TB_REP_APPOINTMENT_SHOP_WEEK
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, WEEK_OF_YEAR, SHOW_YEAR, PERIOD_DATE, TOTAL, KIOSK_SHOW, KIOSK_NOSHOW, WEB_SHOW, WEB_NOSHOW, MOBILE_SHOW, MOBILE_NOSHOW)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDouble(_WEEK_OF_YEAR) & ", "
                sql += DB.SetDouble(_SHOW_YEAR) & ", "
                sql += DB.SetString(_PERIOD_DATE) & ", "
                sql += DB.SetDouble(_TOTAL) & ", "
                sql += DB.SetDouble(_KIOSK_SHOW) & ", "
                sql += DB.SetDouble(_KIOSK_NOSHOW) & ", "
                sql += DB.SetDouble(_WEB_SHOW) & ", "
                sql += DB.SetDouble(_WEB_NOSHOW) & ", "
                sql += DB.SetDouble(_MOBILE_SHOW) & ", "
                sql += DB.SetDouble(_MOBILE_NOSHOW)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_APPOINTMENT_SHOP_WEEK
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
                Sql += "WEEK_OF_YEAR = " & DB.SetDouble(_WEEK_OF_YEAR) & ", "
                Sql += "SHOW_YEAR = " & DB.SetDouble(_SHOW_YEAR) & ", "
                Sql += "PERIOD_DATE = " & DB.SetString(_PERIOD_DATE) & ", "
                Sql += "TOTAL = " & DB.SetDouble(_TOTAL) & ", "
                Sql += "KIOSK_SHOW = " & DB.SetDouble(_KIOSK_SHOW) & ", "
                Sql += "KIOSK_NOSHOW = " & DB.SetDouble(_KIOSK_NOSHOW) & ", "
                Sql += "WEB_SHOW = " & DB.SetDouble(_WEB_SHOW) & ", "
                Sql += "WEB_NOSHOW = " & DB.SetDouble(_WEB_NOSHOW) & ", "
                Sql += "MOBILE_SHOW = " & DB.SetDouble(_MOBILE_SHOW) & ", "
                Sql += "MOBILE_NOSHOW = " & DB.SetDouble(_MOBILE_NOSHOW) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_APPOINTMENT_SHOP_WEEK
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_APPOINTMENT_SHOP_WEEK
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, WEEK_OF_YEAR, SHOW_YEAR, PERIOD_DATE, TOTAL, KIOSK_SHOW, KIOSK_NOSHOW, WEB_SHOW, WEB_NOSHOW, MOBILE_SHOW, MOBILE_NOSHOW FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
