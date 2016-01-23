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
    'Represents a transaction for TW_FILTER_DATA table CenLinqDB.
    '[Create by  on April, 27 2015]
    Public Class TwFilterDataCenLinqDB
        Public sub TwFilterDataCenLinqDB()

        End Sub 
        ' TW_FILTER_DATA
        Const _tableName As String = "TW_FILTER_DATA"
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
        Dim _TW_LOCATION_ID As Long = 0
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _ORDER_NO As  String  = ""
        Dim _ORDER_TYPE_NAME As String = ""
        Dim _ORDER_UNIQUE_ID As String = ""
        Dim _MOBILE_NO As String = ""
        Dim _CUSTOMER_NAME As  String  = ""
        Dim _CUSTOMER_SEGMENT As  String  = ""
        Dim _NETWORK_TYPE As  String  = ""
        Dim _USERNAME As  String  = ""
        Dim _TW_FILTER_ID As Long = 0
        Dim _TW_SFF_ORDER_TYPE_ID As  System.Nullable(Of Long)  = 0
        Dim _TEMPLATE_CODE As String = ""
        Dim _END_TIME As DateTime = New DateTime(1,1,1)
        Dim _FILTER_TIME As DateTime = New DateTime(1,1,1)
        Dim _SEND_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _ATSR_CALL_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _RESULT_TIME As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _RESULT_STATUS As Char = "0"
        Dim _RESULT As  String  = ""
        Dim _NPS_SCORE As Long = 0
        Dim _ACTIVITY_ID_SURVEY As  String  = ""
        Dim _ACTIVITY_ID_LEAVE_VOICE As  String  = ""
        Dim _TW_SOURCE_DATE_ID As Long = 0
        Dim _TW_SOURCE_DATA_ID As Long = 0

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
        <Column(Storage:="_TW_LOCATION_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_LOCATION_ID() As Long
            Get
                Return _TW_LOCATION_ID
            End Get
            Set(ByVal value As Long)
               _TW_LOCATION_ID = value
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
        <Column(Storage:="_ORDER_NO", DbType:="VarChar(50)")>  _
        Public Property ORDER_NO() As  String 
            Get
                Return _ORDER_NO
            End Get
            Set(ByVal value As  String )
               _ORDER_NO = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_TYPE_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_TYPE_NAME() As String
            Get
                Return _ORDER_TYPE_NAME
            End Get
            Set(ByVal value As String)
               _ORDER_TYPE_NAME = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_UNIQUE_ID", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_UNIQUE_ID() As String
            Get
                Return _ORDER_UNIQUE_ID
            End Get
            Set(ByVal value As String)
               _ORDER_UNIQUE_ID = value
            End Set
        End Property 
        <Column(Storage:="_MOBILE_NO", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
               _MOBILE_NO = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_NAME", DbType:="VarChar(255)")>  _
        Public Property CUSTOMER_NAME() As  String 
            Get
                Return _CUSTOMER_NAME
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_NAME = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_SEGMENT", DbType:="VarChar(100)")>  _
        Public Property CUSTOMER_SEGMENT() As  String 
            Get
                Return _CUSTOMER_SEGMENT
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_SEGMENT = value
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
        <Column(Storage:="_USERNAME", DbType:="VarChar(50)")>  _
        Public Property USERNAME() As  String 
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As  String )
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_TW_FILTER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_FILTER_ID() As Long
            Get
                Return _TW_FILTER_ID
            End Get
            Set(ByVal value As Long)
               _TW_FILTER_ID = value
            End Set
        End Property 
        <Column(Storage:="_TW_SFF_ORDER_TYPE_ID", DbType:="Int")>  _
        Public Property TW_SFF_ORDER_TYPE_ID() As  System.Nullable(Of Long) 
            Get
                Return _TW_SFF_ORDER_TYPE_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _TW_SFF_ORDER_TYPE_ID = value
            End Set
        End Property 
        <Column(Storage:="_TEMPLATE_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property TEMPLATE_CODE() As String
            Get
                Return _TEMPLATE_CODE
            End Get
            Set(ByVal value As String)
               _TEMPLATE_CODE = value
            End Set
        End Property 
        <Column(Storage:="_END_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property END_TIME() As DateTime
            Get
                Return _END_TIME
            End Get
            Set(ByVal value As DateTime)
               _END_TIME = value
            End Set
        End Property 
        <Column(Storage:="_FILTER_TIME", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property FILTER_TIME() As DateTime
            Get
                Return _FILTER_TIME
            End Get
            Set(ByVal value As DateTime)
               _FILTER_TIME = value
            End Set
        End Property 
        <Column(Storage:="_SEND_TIME", DbType:="DateTime")>  _
        Public Property SEND_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _SEND_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _SEND_TIME = value
            End Set
        End Property 
        <Column(Storage:="_ATSR_CALL_TIME", DbType:="DateTime")>  _
        Public Property ATSR_CALL_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _ATSR_CALL_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _ATSR_CALL_TIME = value
            End Set
        End Property 
        <Column(Storage:="_RESULT_TIME", DbType:="DateTime")>  _
        Public Property RESULT_TIME() As  System.Nullable(Of DateTime) 
            Get
                Return _RESULT_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _RESULT_TIME = value
            End Set
        End Property 
        <Column(Storage:="_RESULT_STATUS", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property RESULT_STATUS() As Char
            Get
                Return _RESULT_STATUS
            End Get
            Set(ByVal value As Char)
               _RESULT_STATUS = value
            End Set
        End Property 
        <Column(Storage:="_RESULT", DbType:="VarChar(255)")>  _
        Public Property RESULT() As  String 
            Get
                Return _RESULT
            End Get
            Set(ByVal value As  String )
               _RESULT = value
            End Set
        End Property 
        <Column(Storage:="_NPS_SCORE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NPS_SCORE() As Long
            Get
                Return _NPS_SCORE
            End Get
            Set(ByVal value As Long)
               _NPS_SCORE = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVITY_ID_SURVEY", DbType:="VarChar(50)")>  _
        Public Property ACTIVITY_ID_SURVEY() As  String 
            Get
                Return _ACTIVITY_ID_SURVEY
            End Get
            Set(ByVal value As  String )
               _ACTIVITY_ID_SURVEY = value
            End Set
        End Property 
        <Column(Storage:="_ACTIVITY_ID_LEAVE_VOICE", DbType:="VarChar(50)")>  _
        Public Property ACTIVITY_ID_LEAVE_VOICE() As  String 
            Get
                Return _ACTIVITY_ID_LEAVE_VOICE
            End Get
            Set(ByVal value As  String )
               _ACTIVITY_ID_LEAVE_VOICE = value
            End Set
        End Property 
        <Column(Storage:="_TW_SOURCE_DATE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_SOURCE_DATE_ID() As Long
            Get
                Return _TW_SOURCE_DATE_ID
            End Get
            Set(ByVal value As Long)
               _TW_SOURCE_DATE_ID = value
            End Set
        End Property 
        <Column(Storage:="_TW_SOURCE_DATA_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TW_SOURCE_DATA_ID() As Long
            Get
                Return _TW_SOURCE_DATA_ID
            End Get
            Set(ByVal value As Long)
               _TW_SOURCE_DATA_ID = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _TW_LOCATION_ID = 0
            _SERVICE_DATE = New DateTime(1,1,1)
            _ORDER_NO = ""
            _ORDER_TYPE_NAME = ""
            _ORDER_UNIQUE_ID = ""
            _MOBILE_NO = ""
            _CUSTOMER_NAME = ""
            _CUSTOMER_SEGMENT = ""
            _NETWORK_TYPE = ""
            _USERNAME = ""
            _TW_FILTER_ID = 0
            _TW_SFF_ORDER_TYPE_ID = 0
            _TEMPLATE_CODE = ""
            _END_TIME = New DateTime(1,1,1)
            _FILTER_TIME = New DateTime(1,1,1)
            _SEND_TIME = New DateTime(1,1,1)
            _ATSR_CALL_TIME = New DateTime(1,1,1)
            _RESULT_TIME = New DateTime(1,1,1)
            _RESULT_STATUS = ""
            _RESULT = ""
            _NPS_SCORE = 0
            _ACTIVITY_ID_SURVEY = ""
            _ACTIVITY_ID_LEAVE_VOICE = ""
            _TW_SOURCE_DATE_ID = 0
            _TW_SOURCE_DATA_ID = 0
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


        '/// Returns an indication whether the current data is inserted into TW_FILTER_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_FILTER_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_FILTER_DATA table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TW_FILTER_DATA table successfully.
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


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TW_FILTER_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cid As Long, trans As SQLTransaction) As TwFilterDataCenLinqDB
            Return doGetData("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TW_FILTER_DATA by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cid As Long, trans As SQLTransaction) As TwFilterDataCenParaDB
            Return doMappingParameter("id = " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified TW_LOCATION_ID key is retrieved successfully.
        '/// <param name=cTW_LOCATION_ID>The TW_LOCATION_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTW_LOCATION_ID(cTW_LOCATION_ID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("TW_LOCATION_ID = " & DB.SetDouble(cTW_LOCATION_ID) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_FILTER_DATA by specified TW_LOCATION_ID key is retrieved successfully.
        '/// <param name=cTW_LOCATION_ID>The TW_LOCATION_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTW_LOCATION_ID(cTW_LOCATION_ID As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("TW_LOCATION_ID = " & DB.SetDouble(cTW_LOCATION_ID) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified SERVICE_DATE key is retrieved successfully.
        '/// <param name=cSERVICE_DATE>The SERVICE_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE(cSERVICE_DATE As DateTime, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_FILTER_DATA by specified SERVICE_DATE key is retrieved successfully.
        '/// <param name=cSERVICE_DATE>The SERVICE_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE(cSERVICE_DATE As DateTime, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByMOBILE_NO(cMOBILE_NO As String, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_FILTER_DATA by specified MOBILE_NO key is retrieved successfully.
        '/// <param name=cMOBILE_NO>The MOBILE_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByMOBILE_NO(cMOBILE_NO As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("MOBILE_NO = " & DB.SetString(cMOBILE_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified TEMPLATE_CODE key is retrieved successfully.
        '/// <param name=cTEMPLATE_CODE>The TEMPLATE_CODE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTEMPLATE_CODE(cTEMPLATE_CODE As String, trans As SQLTransaction) As Boolean
            Return doChkData("TEMPLATE_CODE = " & DB.SetString(cTEMPLATE_CODE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_FILTER_DATA by specified TEMPLATE_CODE key is retrieved successfully.
        '/// <param name=cTEMPLATE_CODE>The TEMPLATE_CODE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTEMPLATE_CODE(cTEMPLATE_CODE As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("TEMPLATE_CODE = " & DB.SetString(cTEMPLATE_CODE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified ORDER_UNIQUE_ID key is retrieved successfully.
        '/// <param name=cORDER_UNIQUE_ID>The ORDER_UNIQUE_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByORDER_UNIQUE_ID(cORDER_UNIQUE_ID As String, trans As SQLTransaction) As Boolean
            Return doChkData("ORDER_UNIQUE_ID = " & DB.SetString(cORDER_UNIQUE_ID) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TW_FILTER_DATA by specified ORDER_UNIQUE_ID key is retrieved successfully.
        '/// <param name=cORDER_UNIQUE_ID>The ORDER_UNIQUE_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByORDER_UNIQUE_ID(cORDER_UNIQUE_ID As String, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ORDER_UNIQUE_ID = " & DB.SetString(cORDER_UNIQUE_ID) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TW_FILTER_DATA table successfully.
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


        '/// Returns an indication whether the current data is updated to TW_FILTER_DATA table successfully.
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


        '/// Returns an indication whether the current data is deleted from TW_FILTER_DATA table successfully.
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


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("tw_location_id")) = False Then _tw_location_id = Convert.ToInt32(Rdr("tw_location_id"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("order_no")) = False Then _order_no = Rdr("order_no").ToString()
                        If Convert.IsDBNull(Rdr("order_type_name")) = False Then _order_type_name = Rdr("order_type_name").ToString()
                        If Convert.IsDBNull(Rdr("order_unique_id")) = False Then _order_unique_id = Rdr("order_unique_id").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_segment")) = False Then _customer_segment = Rdr("customer_segment").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("tw_filter_id")) = False Then _tw_filter_id = Convert.ToInt64(Rdr("tw_filter_id"))
                        If Convert.IsDBNull(Rdr("tw_sff_order_type_id")) = False Then _tw_sff_order_type_id = Convert.ToInt32(Rdr("tw_sff_order_type_id"))
                        If Convert.IsDBNull(Rdr("template_code")) = False Then _template_code = Rdr("template_code").ToString()
                        If Convert.IsDBNull(Rdr("end_time")) = False Then _end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("filter_time")) = False Then _filter_time = Convert.ToDateTime(Rdr("filter_time"))
                        If Convert.IsDBNull(Rdr("send_time")) = False Then _send_time = Convert.ToDateTime(Rdr("send_time"))
                        If Convert.IsDBNull(Rdr("atsr_call_time")) = False Then _atsr_call_time = Convert.ToDateTime(Rdr("atsr_call_time"))
                        If Convert.IsDBNull(Rdr("result_time")) = False Then _result_time = Convert.ToDateTime(Rdr("result_time"))
                        If Convert.IsDBNull(Rdr("result_status")) = False Then _result_status = Rdr("result_status").ToString()
                        If Convert.IsDBNull(Rdr("result")) = False Then _result = Rdr("result").ToString()
                        If Convert.IsDBNull(Rdr("nps_score")) = False Then _nps_score = Convert.ToInt32(Rdr("nps_score"))
                        If Convert.IsDBNull(Rdr("activity_id_survey")) = False Then _activity_id_survey = Rdr("activity_id_survey").ToString()
                        If Convert.IsDBNull(Rdr("activity_id_leave_voice")) = False Then _activity_id_leave_voice = Rdr("activity_id_leave_voice").ToString()
                        If Convert.IsDBNull(Rdr("tw_source_date_id")) = False Then _tw_source_date_id = Convert.ToInt64(Rdr("tw_source_date_id"))
                        If Convert.IsDBNull(Rdr("tw_source_data_id")) = False Then _tw_source_data_id = Convert.ToInt64(Rdr("tw_source_data_id"))
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


        '/// Returns an indication whether the record of TW_FILTER_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TwFilterDataCenLinqDB
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
                        If Convert.IsDBNull(Rdr("tw_location_id")) = False Then _tw_location_id = Convert.ToInt32(Rdr("tw_location_id"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("order_no")) = False Then _order_no = Rdr("order_no").ToString()
                        If Convert.IsDBNull(Rdr("order_type_name")) = False Then _order_type_name = Rdr("order_type_name").ToString()
                        If Convert.IsDBNull(Rdr("order_unique_id")) = False Then _order_unique_id = Rdr("order_unique_id").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then _mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then _customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_segment")) = False Then _customer_segment = Rdr("customer_segment").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then _network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("tw_filter_id")) = False Then _tw_filter_id = Convert.ToInt64(Rdr("tw_filter_id"))
                        If Convert.IsDBNull(Rdr("tw_sff_order_type_id")) = False Then _tw_sff_order_type_id = Convert.ToInt32(Rdr("tw_sff_order_type_id"))
                        If Convert.IsDBNull(Rdr("template_code")) = False Then _template_code = Rdr("template_code").ToString()
                        If Convert.IsDBNull(Rdr("end_time")) = False Then _end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("filter_time")) = False Then _filter_time = Convert.ToDateTime(Rdr("filter_time"))
                        If Convert.IsDBNull(Rdr("send_time")) = False Then _send_time = Convert.ToDateTime(Rdr("send_time"))
                        If Convert.IsDBNull(Rdr("atsr_call_time")) = False Then _atsr_call_time = Convert.ToDateTime(Rdr("atsr_call_time"))
                        If Convert.IsDBNull(Rdr("result_time")) = False Then _result_time = Convert.ToDateTime(Rdr("result_time"))
                        If Convert.IsDBNull(Rdr("result_status")) = False Then _result_status = Rdr("result_status").ToString()
                        If Convert.IsDBNull(Rdr("result")) = False Then _result = Rdr("result").ToString()
                        If Convert.IsDBNull(Rdr("nps_score")) = False Then _nps_score = Convert.ToInt32(Rdr("nps_score"))
                        If Convert.IsDBNull(Rdr("activity_id_survey")) = False Then _activity_id_survey = Rdr("activity_id_survey").ToString()
                        If Convert.IsDBNull(Rdr("activity_id_leave_voice")) = False Then _activity_id_leave_voice = Rdr("activity_id_leave_voice").ToString()
                        If Convert.IsDBNull(Rdr("tw_source_date_id")) = False Then _tw_source_date_id = Convert.ToInt64(Rdr("tw_source_date_id"))
                        If Convert.IsDBNull(Rdr("tw_source_data_id")) = False Then _tw_source_data_id = Convert.ToInt64(Rdr("tw_source_data_id"))
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


        '/// Returns an indication whether the Class Data of TW_FILTER_DATA by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TwFilterDataCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TwFilterDataCenParaDB
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
                        If Convert.IsDBNull(Rdr("tw_location_id")) = False Then ret.tw_location_id = Convert.ToInt32(Rdr("tw_location_id"))
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("order_no")) = False Then ret.order_no = Rdr("order_no").ToString()
                        If Convert.IsDBNull(Rdr("order_type_name")) = False Then ret.order_type_name = Rdr("order_type_name").ToString()
                        If Convert.IsDBNull(Rdr("order_unique_id")) = False Then ret.order_unique_id = Rdr("order_unique_id").ToString()
                        If Convert.IsDBNull(Rdr("mobile_no")) = False Then ret.mobile_no = Rdr("mobile_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_name")) = False Then ret.customer_name = Rdr("customer_name").ToString()
                        If Convert.IsDBNull(Rdr("customer_segment")) = False Then ret.customer_segment = Rdr("customer_segment").ToString()
                        If Convert.IsDBNull(Rdr("network_type")) = False Then ret.network_type = Rdr("network_type").ToString()
                        If Convert.IsDBNull(Rdr("username")) = False Then ret.username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("tw_filter_id")) = False Then ret.tw_filter_id = Convert.ToInt64(Rdr("tw_filter_id"))
                        If Convert.IsDBNull(Rdr("tw_sff_order_type_id")) = False Then ret.tw_sff_order_type_id = Convert.ToInt32(Rdr("tw_sff_order_type_id"))
                        If Convert.IsDBNull(Rdr("template_code")) = False Then ret.template_code = Rdr("template_code").ToString()
                        If Convert.IsDBNull(Rdr("end_time")) = False Then ret.end_time = Convert.ToDateTime(Rdr("end_time"))
                        If Convert.IsDBNull(Rdr("filter_time")) = False Then ret.filter_time = Convert.ToDateTime(Rdr("filter_time"))
                        If Convert.IsDBNull(Rdr("send_time")) = False Then ret.send_time = Convert.ToDateTime(Rdr("send_time"))
                        If Convert.IsDBNull(Rdr("atsr_call_time")) = False Then ret.atsr_call_time = Convert.ToDateTime(Rdr("atsr_call_time"))
                        If Convert.IsDBNull(Rdr("result_time")) = False Then ret.result_time = Convert.ToDateTime(Rdr("result_time"))
                        If Convert.IsDBNull(Rdr("result_status")) = False Then ret.result_status = Rdr("result_status").ToString()
                        If Convert.IsDBNull(Rdr("result")) = False Then ret.result = Rdr("result").ToString()
                        If Convert.IsDBNull(Rdr("nps_score")) = False Then ret.nps_score = Convert.ToInt32(Rdr("nps_score"))
                        If Convert.IsDBNull(Rdr("activity_id_survey")) = False Then ret.activity_id_survey = Rdr("activity_id_survey").ToString()
                        If Convert.IsDBNull(Rdr("activity_id_leave_voice")) = False Then ret.activity_id_leave_voice = Rdr("activity_id_leave_voice").ToString()
                        If Convert.IsDBNull(Rdr("tw_source_date_id")) = False Then ret.tw_source_date_id = Convert.ToInt64(Rdr("tw_source_date_id"))
                        If Convert.IsDBNull(Rdr("tw_source_data_id")) = False Then ret.tw_source_data_id = Convert.ToInt64(Rdr("tw_source_data_id"))

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


        'Get Insert Statement for table TW_FILTER_DATA
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TW_LOCATION_ID, SERVICE_DATE, ORDER_NO, ORDER_TYPE_NAME, ORDER_UNIQUE_ID, MOBILE_NO, CUSTOMER_NAME, CUSTOMER_SEGMENT, NETWORK_TYPE, USERNAME, TW_FILTER_ID, TW_SFF_ORDER_TYPE_ID, TEMPLATE_CODE, END_TIME, FILTER_TIME, SEND_TIME, ATSR_CALL_TIME, RESULT_TIME, RESULT_STATUS, RESULT, NPS_SCORE, ACTIVITY_ID_SURVEY, ACTIVITY_ID_LEAVE_VOICE, TW_SOURCE_DATE_ID, TW_SOURCE_DATA_ID)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_TW_LOCATION_ID) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetString(_ORDER_NO) & ", "
                sql += DB.SetString(_ORDER_TYPE_NAME) & ", "
                sql += DB.SetString(_ORDER_UNIQUE_ID) & ", "
                sql += DB.SetString(_MOBILE_NO) & ", "
                sql += DB.SetString(_CUSTOMER_NAME) & ", "
                sql += DB.SetString(_CUSTOMER_SEGMENT) & ", "
                sql += DB.SetString(_NETWORK_TYPE) & ", "
                sql += DB.SetString(_USERNAME) & ", "
                sql += DB.SetDouble(_TW_FILTER_ID) & ", "
                sql += DB.SetDouble(_TW_SFF_ORDER_TYPE_ID) & ", "
                sql += DB.SetString(_TEMPLATE_CODE) & ", "
                sql += DB.SetDateTime(_END_TIME) & ", "
                sql += DB.SetDateTime(_FILTER_TIME) & ", "
                sql += DB.SetDateTime(_SEND_TIME) & ", "
                sql += DB.SetDateTime(_ATSR_CALL_TIME) & ", "
                sql += DB.SetDateTime(_RESULT_TIME) & ", "
                sql += DB.SetString(_RESULT_STATUS) & ", "
                sql += DB.SetString(_RESULT) & ", "
                sql += DB.SetDouble(_NPS_SCORE) & ", "
                sql += DB.SetString(_ACTIVITY_ID_SURVEY) & ", "
                sql += DB.SetString(_ACTIVITY_ID_LEAVE_VOICE) & ", "
                sql += DB.SetDouble(_TW_SOURCE_DATE_ID) & ", "
                sql += DB.SetDouble(_TW_SOURCE_DATA_ID)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TW_FILTER_DATA
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "ID = " & DB.SetDouble(_ID) & ", "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "TW_LOCATION_ID = " & DB.SetDouble(_TW_LOCATION_ID) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "ORDER_NO = " & DB.SetString(_ORDER_NO) & ", "
                Sql += "ORDER_TYPE_NAME = " & DB.SetString(_ORDER_TYPE_NAME) & ", "
                Sql += "ORDER_UNIQUE_ID = " & DB.SetString(_ORDER_UNIQUE_ID) & ", "
                Sql += "MOBILE_NO = " & DB.SetString(_MOBILE_NO) & ", "
                Sql += "CUSTOMER_NAME = " & DB.SetString(_CUSTOMER_NAME) & ", "
                Sql += "CUSTOMER_SEGMENT = " & DB.SetString(_CUSTOMER_SEGMENT) & ", "
                Sql += "NETWORK_TYPE = " & DB.SetString(_NETWORK_TYPE) & ", "
                Sql += "USERNAME = " & DB.SetString(_USERNAME) & ", "
                Sql += "TW_FILTER_ID = " & DB.SetDouble(_TW_FILTER_ID) & ", "
                Sql += "TW_SFF_ORDER_TYPE_ID = " & DB.SetDouble(_TW_SFF_ORDER_TYPE_ID) & ", "
                Sql += "TEMPLATE_CODE = " & DB.SetString(_TEMPLATE_CODE) & ", "
                Sql += "END_TIME = " & DB.SetDateTime(_END_TIME) & ", "
                Sql += "FILTER_TIME = " & DB.SetDateTime(_FILTER_TIME) & ", "
                Sql += "SEND_TIME = " & DB.SetDateTime(_SEND_TIME) & ", "
                Sql += "ATSR_CALL_TIME = " & DB.SetDateTime(_ATSR_CALL_TIME) & ", "
                Sql += "RESULT_TIME = " & DB.SetDateTime(_RESULT_TIME) & ", "
                Sql += "RESULT_STATUS = " & DB.SetString(_RESULT_STATUS) & ", "
                Sql += "RESULT = " & DB.SetString(_RESULT) & ", "
                Sql += "NPS_SCORE = " & DB.SetDouble(_NPS_SCORE) & ", "
                Sql += "ACTIVITY_ID_SURVEY = " & DB.SetString(_ACTIVITY_ID_SURVEY) & ", "
                Sql += "ACTIVITY_ID_LEAVE_VOICE = " & DB.SetString(_ACTIVITY_ID_LEAVE_VOICE) & ", "
                Sql += "TW_SOURCE_DATE_ID = " & DB.SetDouble(_TW_SOURCE_DATE_ID) & ", "
                Sql += "TW_SOURCE_DATA_ID = " & DB.SetDouble(_TW_SOURCE_DATA_ID) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TW_FILTER_DATA
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TW_FILTER_DATA
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
