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
    'Represents a transaction for TB_REP_CUST_NETWORK_DAY table CenLinqDB.
    '[Create by  on December, 20 2013]
    Public Class TbRepCustNetworkDayCenLinqDB
        Public sub TbRepCustNetworkDayCenLinqDB()

        End Sub 
        ' TB_REP_CUST_NETWORK_DAY
        Const _tableName As String = "TB_REP_CUST_NETWORK_DAY"
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
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _GSM_REGIS As Long = 0
        Dim _GSM_SERVE As Long = 0
        Dim _GSM_MISS_CALL As Long = 0
        Dim _GSM_CANCEL As Long = 0
        Dim _GSM_NOTCALL As Long = 0
        Dim _GSM_NOTCON As Long = 0
        Dim _GSM_NOTEND As Long = 0
        Dim _OTC_REGIS As Long = 0
        Dim _OTC_SERVE As Long = 0
        Dim _OTC_MISS_CALL As Long = 0
        Dim _OTC_CANCEL As Long = 0
        Dim _OTC_NOTCALL As Long = 0
        Dim _OTC_NOTCON As Long = 0
        Dim _OTC_NOTEND As Long = 0
        Dim _NON_REGIS As Long = 0
        Dim _NON_SERVE As Long = 0
        Dim _NON_MISS_CALL As Long = 0
        Dim _NON_CANCEL As Long = 0
        Dim _NON_NOTCALL As Long = 0
        Dim _NON_NOTCON As Long = 0
        Dim _NON_NOTEND As Long = 0
        Dim _AIS3G_REGIS As Long = 0
        Dim _AIS3G_SERVE As Long = 0
        Dim _AIS3G_MISS_CALL As Long = 0
        Dim _AIS3G_CANCEL As Long = 0
        Dim _AIS3G_NOTCALL As Long = 0
        Dim _AIS3G_NOTCON As Long = 0
        Dim _AIS3G_NOTEND As Long = 0
        Dim _OTC3G_REGIS As Long = 0
        Dim _OTC3G_SERVE As Long = 0
        Dim _OTC3G_MISS_CALL As Long = 0
        Dim _OTC3G_CANCEL As Long = 0
        Dim _OTC3G_NOTCALL As Long = 0
        Dim _OTC3G_NOTCON As Long = 0
        Dim _OTC3G_NOTEND As Long = 0

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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
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
        <Column(Storage:="_GSM_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_REGIS() As Long
            Get
                Return _GSM_REGIS
            End Get
            Set(ByVal value As Long)
               _GSM_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_GSM_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_SERVE() As Long
            Get
                Return _GSM_SERVE
            End Get
            Set(ByVal value As Long)
               _GSM_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_GSM_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_MISS_CALL() As Long
            Get
                Return _GSM_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _GSM_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_GSM_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_CANCEL() As Long
            Get
                Return _GSM_CANCEL
            End Get
            Set(ByVal value As Long)
               _GSM_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_GSM_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_NOTCALL() As Long
            Get
                Return _GSM_NOTCALL
            End Get
            Set(ByVal value As Long)
               _GSM_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_GSM_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_NOTCON() As Long
            Get
                Return _GSM_NOTCON
            End Get
            Set(ByVal value As Long)
               _GSM_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_GSM_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GSM_NOTEND() As Long
            Get
                Return _GSM_NOTEND
            End Get
            Set(ByVal value As Long)
               _GSM_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_OTC_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_REGIS() As Long
            Get
                Return _OTC_REGIS
            End Get
            Set(ByVal value As Long)
               _OTC_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_OTC_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_SERVE() As Long
            Get
                Return _OTC_SERVE
            End Get
            Set(ByVal value As Long)
               _OTC_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_OTC_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_MISS_CALL() As Long
            Get
                Return _OTC_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _OTC_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_CANCEL() As Long
            Get
                Return _OTC_CANCEL
            End Get
            Set(ByVal value As Long)
               _OTC_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_OTC_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_NOTCALL() As Long
            Get
                Return _OTC_NOTCALL
            End Get
            Set(ByVal value As Long)
               _OTC_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_NOTCON() As Long
            Get
                Return _OTC_NOTCON
            End Get
            Set(ByVal value As Long)
               _OTC_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_OTC_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC_NOTEND() As Long
            Get
                Return _OTC_NOTEND
            End Get
            Set(ByVal value As Long)
               _OTC_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_NON_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_REGIS() As Long
            Get
                Return _NON_REGIS
            End Get
            Set(ByVal value As Long)
               _NON_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_NON_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_SERVE() As Long
            Get
                Return _NON_SERVE
            End Get
            Set(ByVal value As Long)
               _NON_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_NON_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_MISS_CALL() As Long
            Get
                Return _NON_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _NON_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_CANCEL() As Long
            Get
                Return _NON_CANCEL
            End Get
            Set(ByVal value As Long)
               _NON_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOTCALL() As Long
            Get
                Return _NON_NOTCALL
            End Get
            Set(ByVal value As Long)
               _NON_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOTCON() As Long
            Get
                Return _NON_NOTCON
            End Get
            Set(ByVal value As Long)
               _NON_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOTEND() As Long
            Get
                Return _NON_NOTEND
            End Get
            Set(ByVal value As Long)
               _NON_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_REGIS() As Long
            Get
                Return _AIS3G_REGIS
            End Get
            Set(ByVal value As Long)
               _AIS3G_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_SERVE() As Long
            Get
                Return _AIS3G_SERVE
            End Get
            Set(ByVal value As Long)
               _AIS3G_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_MISS_CALL() As Long
            Get
                Return _AIS3G_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _AIS3G_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_CANCEL() As Long
            Get
                Return _AIS3G_CANCEL
            End Get
            Set(ByVal value As Long)
               _AIS3G_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_NOTCALL() As Long
            Get
                Return _AIS3G_NOTCALL
            End Get
            Set(ByVal value As Long)
               _AIS3G_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_NOTCON() As Long
            Get
                Return _AIS3G_NOTCON
            End Get
            Set(ByVal value As Long)
               _AIS3G_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_AIS3G_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS3G_NOTEND() As Long
            Get
                Return _AIS3G_NOTEND
            End Get
            Set(ByVal value As Long)
               _AIS3G_NOTEND = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_REGIS() As Long
            Get
                Return _OTC3G_REGIS
            End Get
            Set(ByVal value As Long)
               _OTC3G_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_SERVE() As Long
            Get
                Return _OTC3G_SERVE
            End Get
            Set(ByVal value As Long)
               _OTC3G_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_MISS_CALL() As Long
            Get
                Return _OTC3G_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _OTC3G_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_CANCEL() As Long
            Get
                Return _OTC3G_CANCEL
            End Get
            Set(ByVal value As Long)
               _OTC3G_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_NOTCALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_NOTCALL() As Long
            Get
                Return _OTC3G_NOTCALL
            End Get
            Set(ByVal value As Long)
               _OTC3G_NOTCALL = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_NOTCON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_NOTCON() As Long
            Get
                Return _OTC3G_NOTCON
            End Get
            Set(ByVal value As Long)
               _OTC3G_NOTCON = value
            End Set
        End Property 
        <Column(Storage:="_OTC3G_NOTEND", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property OTC3G_NOTEND() As Long
            Get
                Return _OTC3G_NOTEND
            End Get
            Set(ByVal value As Long)
               _OTC3G_NOTEND = value
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
            _SERVICE_DATE = New DateTime(1,1,1)
            _SERVICE_ID = 0
            _SERVICE_NAME_EN = ""
            _SERVICE_NAME_TH = ""
            _GSM_REGIS = 0
            _GSM_SERVE = 0
            _GSM_MISS_CALL = 0
            _GSM_CANCEL = 0
            _GSM_NOTCALL = 0
            _GSM_NOTCON = 0
            _GSM_NOTEND = 0
            _OTC_REGIS = 0
            _OTC_SERVE = 0
            _OTC_MISS_CALL = 0
            _OTC_CANCEL = 0
            _OTC_NOTCALL = 0
            _OTC_NOTCON = 0
            _OTC_NOTEND = 0
            _NON_REGIS = 0
            _NON_SERVE = 0
            _NON_MISS_CALL = 0
            _NON_CANCEL = 0
            _NON_NOTCALL = 0
            _NON_NOTCON = 0
            _NON_NOTEND = 0
            _AIS3G_REGIS = 0
            _AIS3G_SERVE = 0
            _AIS3G_MISS_CALL = 0
            _AIS3G_CANCEL = 0
            _AIS3G_NOTCALL = 0
            _AIS3G_NOTCON = 0
            _AIS3G_NOTEND = 0
            _OTC3G_REGIS = 0
            _OTC3G_SERVE = 0
            _OTC3G_MISS_CALL = 0
            _OTC3G_CANCEL = 0
            _OTC3G_NOTCALL = 0
            _OTC3G_NOTCON = 0
            _OTC3G_NOTEND = 0
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


        '/// Returns an indication whether the current data is inserted into TB_REP_CUST_NETWORK_DAY table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_NETWORK_DAY table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_NETWORK_DAY table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_CUST_NETWORK_DAY table successfully.
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


        '/// Returns an indication whether the record of TB_REP_CUST_NETWORK_DAY by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_CUST_NETWORK_DAY by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepCustNetworkDayCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_CUST_NETWORK_DAY by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepCustNetworkDayCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_NETWORK_DAY by specified SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_SHOP_ID>The SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE_SHOP_ID(cSERVICE_DATE As DateTime, cSHOP_ID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_CUST_NETWORK_DAY by specified SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_SHOP_ID>The SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE_SHOP_ID(cSERVICE_DATE As DateTime, cSHOP_ID As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_NETWORK_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_CUST_NETWORK_DAY table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_NETWORK_DAY table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_REP_CUST_NETWORK_DAY table successfully.
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


        '/// Returns an indication whether the record of TB_REP_CUST_NETWORK_DAY by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("gsm_regis")) = False Then _gsm_regis = Convert.ToInt32(Rdr("gsm_regis"))
                        If Convert.IsDBNull(Rdr("gsm_serve")) = False Then _gsm_serve = Convert.ToInt32(Rdr("gsm_serve"))
                        If Convert.IsDBNull(Rdr("gsm_miss_call")) = False Then _gsm_miss_call = Convert.ToInt32(Rdr("gsm_miss_call"))
                        If Convert.IsDBNull(Rdr("gsm_cancel")) = False Then _gsm_cancel = Convert.ToInt32(Rdr("gsm_cancel"))
                        If Convert.IsDBNull(Rdr("gsm_notcall")) = False Then _gsm_notcall = Convert.ToInt32(Rdr("gsm_notcall"))
                        If Convert.IsDBNull(Rdr("gsm_notcon")) = False Then _gsm_notcon = Convert.ToInt32(Rdr("gsm_notcon"))
                        If Convert.IsDBNull(Rdr("gsm_notend")) = False Then _gsm_notend = Convert.ToInt32(Rdr("gsm_notend"))
                        If Convert.IsDBNull(Rdr("otc_regis")) = False Then _otc_regis = Convert.ToInt32(Rdr("otc_regis"))
                        If Convert.IsDBNull(Rdr("otc_serve")) = False Then _otc_serve = Convert.ToInt32(Rdr("otc_serve"))
                        If Convert.IsDBNull(Rdr("otc_miss_call")) = False Then _otc_miss_call = Convert.ToInt32(Rdr("otc_miss_call"))
                        If Convert.IsDBNull(Rdr("otc_cancel")) = False Then _otc_cancel = Convert.ToInt32(Rdr("otc_cancel"))
                        If Convert.IsDBNull(Rdr("otc_notcall")) = False Then _otc_notcall = Convert.ToInt32(Rdr("otc_notcall"))
                        If Convert.IsDBNull(Rdr("otc_notcon")) = False Then _otc_notcon = Convert.ToInt32(Rdr("otc_notcon"))
                        If Convert.IsDBNull(Rdr("otc_notend")) = False Then _otc_notend = Convert.ToInt32(Rdr("otc_notend"))
                        If Convert.IsDBNull(Rdr("non_regis")) = False Then _non_regis = Convert.ToInt32(Rdr("non_regis"))
                        If Convert.IsDBNull(Rdr("non_serve")) = False Then _non_serve = Convert.ToInt32(Rdr("non_serve"))
                        If Convert.IsDBNull(Rdr("non_miss_call")) = False Then _non_miss_call = Convert.ToInt32(Rdr("non_miss_call"))
                        If Convert.IsDBNull(Rdr("non_cancel")) = False Then _non_cancel = Convert.ToInt32(Rdr("non_cancel"))
                        If Convert.IsDBNull(Rdr("non_notcall")) = False Then _non_notcall = Convert.ToInt32(Rdr("non_notcall"))
                        If Convert.IsDBNull(Rdr("non_notcon")) = False Then _non_notcon = Convert.ToInt32(Rdr("non_notcon"))
                        If Convert.IsDBNull(Rdr("non_notend")) = False Then _non_notend = Convert.ToInt32(Rdr("non_notend"))
                        If Convert.IsDBNull(Rdr("ais3g_regis")) = False Then _ais3g_regis = Convert.ToInt32(Rdr("ais3g_regis"))
                        If Convert.IsDBNull(Rdr("ais3g_serve")) = False Then _ais3g_serve = Convert.ToInt32(Rdr("ais3g_serve"))
                        If Convert.IsDBNull(Rdr("ais3g_miss_call")) = False Then _ais3g_miss_call = Convert.ToInt32(Rdr("ais3g_miss_call"))
                        If Convert.IsDBNull(Rdr("ais3g_cancel")) = False Then _ais3g_cancel = Convert.ToInt32(Rdr("ais3g_cancel"))
                        If Convert.IsDBNull(Rdr("ais3g_notcall")) = False Then _ais3g_notcall = Convert.ToInt32(Rdr("ais3g_notcall"))
                        If Convert.IsDBNull(Rdr("ais3g_notcon")) = False Then _ais3g_notcon = Convert.ToInt32(Rdr("ais3g_notcon"))
                        If Convert.IsDBNull(Rdr("ais3g_notend")) = False Then _ais3g_notend = Convert.ToInt32(Rdr("ais3g_notend"))
                        If Convert.IsDBNull(Rdr("otc3g_regis")) = False Then _otc3g_regis = Convert.ToInt32(Rdr("otc3g_regis"))
                        If Convert.IsDBNull(Rdr("otc3g_serve")) = False Then _otc3g_serve = Convert.ToInt32(Rdr("otc3g_serve"))
                        If Convert.IsDBNull(Rdr("otc3g_miss_call")) = False Then _otc3g_miss_call = Convert.ToInt32(Rdr("otc3g_miss_call"))
                        If Convert.IsDBNull(Rdr("otc3g_cancel")) = False Then _otc3g_cancel = Convert.ToInt32(Rdr("otc3g_cancel"))
                        If Convert.IsDBNull(Rdr("otc3g_notcall")) = False Then _otc3g_notcall = Convert.ToInt32(Rdr("otc3g_notcall"))
                        If Convert.IsDBNull(Rdr("otc3g_notcon")) = False Then _otc3g_notcon = Convert.ToInt32(Rdr("otc3g_notcon"))
                        If Convert.IsDBNull(Rdr("otc3g_notend")) = False Then _otc3g_notend = Convert.ToInt32(Rdr("otc3g_notend"))
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


        '/// Returns an indication whether the record of TB_REP_CUST_NETWORK_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepCustNetworkDayCenLinqDB
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("gsm_regis")) = False Then _gsm_regis = Convert.ToInt32(Rdr("gsm_regis"))
                        If Convert.IsDBNull(Rdr("gsm_serve")) = False Then _gsm_serve = Convert.ToInt32(Rdr("gsm_serve"))
                        If Convert.IsDBNull(Rdr("gsm_miss_call")) = False Then _gsm_miss_call = Convert.ToInt32(Rdr("gsm_miss_call"))
                        If Convert.IsDBNull(Rdr("gsm_cancel")) = False Then _gsm_cancel = Convert.ToInt32(Rdr("gsm_cancel"))
                        If Convert.IsDBNull(Rdr("gsm_notcall")) = False Then _gsm_notcall = Convert.ToInt32(Rdr("gsm_notcall"))
                        If Convert.IsDBNull(Rdr("gsm_notcon")) = False Then _gsm_notcon = Convert.ToInt32(Rdr("gsm_notcon"))
                        If Convert.IsDBNull(Rdr("gsm_notend")) = False Then _gsm_notend = Convert.ToInt32(Rdr("gsm_notend"))
                        If Convert.IsDBNull(Rdr("otc_regis")) = False Then _otc_regis = Convert.ToInt32(Rdr("otc_regis"))
                        If Convert.IsDBNull(Rdr("otc_serve")) = False Then _otc_serve = Convert.ToInt32(Rdr("otc_serve"))
                        If Convert.IsDBNull(Rdr("otc_miss_call")) = False Then _otc_miss_call = Convert.ToInt32(Rdr("otc_miss_call"))
                        If Convert.IsDBNull(Rdr("otc_cancel")) = False Then _otc_cancel = Convert.ToInt32(Rdr("otc_cancel"))
                        If Convert.IsDBNull(Rdr("otc_notcall")) = False Then _otc_notcall = Convert.ToInt32(Rdr("otc_notcall"))
                        If Convert.IsDBNull(Rdr("otc_notcon")) = False Then _otc_notcon = Convert.ToInt32(Rdr("otc_notcon"))
                        If Convert.IsDBNull(Rdr("otc_notend")) = False Then _otc_notend = Convert.ToInt32(Rdr("otc_notend"))
                        If Convert.IsDBNull(Rdr("non_regis")) = False Then _non_regis = Convert.ToInt32(Rdr("non_regis"))
                        If Convert.IsDBNull(Rdr("non_serve")) = False Then _non_serve = Convert.ToInt32(Rdr("non_serve"))
                        If Convert.IsDBNull(Rdr("non_miss_call")) = False Then _non_miss_call = Convert.ToInt32(Rdr("non_miss_call"))
                        If Convert.IsDBNull(Rdr("non_cancel")) = False Then _non_cancel = Convert.ToInt32(Rdr("non_cancel"))
                        If Convert.IsDBNull(Rdr("non_notcall")) = False Then _non_notcall = Convert.ToInt32(Rdr("non_notcall"))
                        If Convert.IsDBNull(Rdr("non_notcon")) = False Then _non_notcon = Convert.ToInt32(Rdr("non_notcon"))
                        If Convert.IsDBNull(Rdr("non_notend")) = False Then _non_notend = Convert.ToInt32(Rdr("non_notend"))
                        If Convert.IsDBNull(Rdr("ais3g_regis")) = False Then _ais3g_regis = Convert.ToInt32(Rdr("ais3g_regis"))
                        If Convert.IsDBNull(Rdr("ais3g_serve")) = False Then _ais3g_serve = Convert.ToInt32(Rdr("ais3g_serve"))
                        If Convert.IsDBNull(Rdr("ais3g_miss_call")) = False Then _ais3g_miss_call = Convert.ToInt32(Rdr("ais3g_miss_call"))
                        If Convert.IsDBNull(Rdr("ais3g_cancel")) = False Then _ais3g_cancel = Convert.ToInt32(Rdr("ais3g_cancel"))
                        If Convert.IsDBNull(Rdr("ais3g_notcall")) = False Then _ais3g_notcall = Convert.ToInt32(Rdr("ais3g_notcall"))
                        If Convert.IsDBNull(Rdr("ais3g_notcon")) = False Then _ais3g_notcon = Convert.ToInt32(Rdr("ais3g_notcon"))
                        If Convert.IsDBNull(Rdr("ais3g_notend")) = False Then _ais3g_notend = Convert.ToInt32(Rdr("ais3g_notend"))
                        If Convert.IsDBNull(Rdr("otc3g_regis")) = False Then _otc3g_regis = Convert.ToInt32(Rdr("otc3g_regis"))
                        If Convert.IsDBNull(Rdr("otc3g_serve")) = False Then _otc3g_serve = Convert.ToInt32(Rdr("otc3g_serve"))
                        If Convert.IsDBNull(Rdr("otc3g_miss_call")) = False Then _otc3g_miss_call = Convert.ToInt32(Rdr("otc3g_miss_call"))
                        If Convert.IsDBNull(Rdr("otc3g_cancel")) = False Then _otc3g_cancel = Convert.ToInt32(Rdr("otc3g_cancel"))
                        If Convert.IsDBNull(Rdr("otc3g_notcall")) = False Then _otc3g_notcall = Convert.ToInt32(Rdr("otc3g_notcall"))
                        If Convert.IsDBNull(Rdr("otc3g_notcon")) = False Then _otc3g_notcon = Convert.ToInt32(Rdr("otc3g_notcon"))
                        If Convert.IsDBNull(Rdr("otc3g_notend")) = False Then _otc3g_notend = Convert.ToInt32(Rdr("otc3g_notend"))
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


        '/// Returns an indication whether the Class Data of TB_REP_CUST_NETWORK_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepCustNetworkDayCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepCustNetworkDayCenParaDB
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
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then ret.service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then ret.service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("gsm_regis")) = False Then ret.gsm_regis = Convert.ToInt32(Rdr("gsm_regis"))
                        If Convert.IsDBNull(Rdr("gsm_serve")) = False Then ret.gsm_serve = Convert.ToInt32(Rdr("gsm_serve"))
                        If Convert.IsDBNull(Rdr("gsm_miss_call")) = False Then ret.gsm_miss_call = Convert.ToInt32(Rdr("gsm_miss_call"))
                        If Convert.IsDBNull(Rdr("gsm_cancel")) = False Then ret.gsm_cancel = Convert.ToInt32(Rdr("gsm_cancel"))
                        If Convert.IsDBNull(Rdr("gsm_notcall")) = False Then ret.gsm_notcall = Convert.ToInt32(Rdr("gsm_notcall"))
                        If Convert.IsDBNull(Rdr("gsm_notcon")) = False Then ret.gsm_notcon = Convert.ToInt32(Rdr("gsm_notcon"))
                        If Convert.IsDBNull(Rdr("gsm_notend")) = False Then ret.gsm_notend = Convert.ToInt32(Rdr("gsm_notend"))
                        If Convert.IsDBNull(Rdr("otc_regis")) = False Then ret.otc_regis = Convert.ToInt32(Rdr("otc_regis"))
                        If Convert.IsDBNull(Rdr("otc_serve")) = False Then ret.otc_serve = Convert.ToInt32(Rdr("otc_serve"))
                        If Convert.IsDBNull(Rdr("otc_miss_call")) = False Then ret.otc_miss_call = Convert.ToInt32(Rdr("otc_miss_call"))
                        If Convert.IsDBNull(Rdr("otc_cancel")) = False Then ret.otc_cancel = Convert.ToInt32(Rdr("otc_cancel"))
                        If Convert.IsDBNull(Rdr("otc_notcall")) = False Then ret.otc_notcall = Convert.ToInt32(Rdr("otc_notcall"))
                        If Convert.IsDBNull(Rdr("otc_notcon")) = False Then ret.otc_notcon = Convert.ToInt32(Rdr("otc_notcon"))
                        If Convert.IsDBNull(Rdr("otc_notend")) = False Then ret.otc_notend = Convert.ToInt32(Rdr("otc_notend"))
                        If Convert.IsDBNull(Rdr("non_regis")) = False Then ret.non_regis = Convert.ToInt32(Rdr("non_regis"))
                        If Convert.IsDBNull(Rdr("non_serve")) = False Then ret.non_serve = Convert.ToInt32(Rdr("non_serve"))
                        If Convert.IsDBNull(Rdr("non_miss_call")) = False Then ret.non_miss_call = Convert.ToInt32(Rdr("non_miss_call"))
                        If Convert.IsDBNull(Rdr("non_cancel")) = False Then ret.non_cancel = Convert.ToInt32(Rdr("non_cancel"))
                        If Convert.IsDBNull(Rdr("non_notcall")) = False Then ret.non_notcall = Convert.ToInt32(Rdr("non_notcall"))
                        If Convert.IsDBNull(Rdr("non_notcon")) = False Then ret.non_notcon = Convert.ToInt32(Rdr("non_notcon"))
                        If Convert.IsDBNull(Rdr("non_notend")) = False Then ret.non_notend = Convert.ToInt32(Rdr("non_notend"))
                        If Convert.IsDBNull(Rdr("ais3g_regis")) = False Then ret.ais3g_regis = Convert.ToInt32(Rdr("ais3g_regis"))
                        If Convert.IsDBNull(Rdr("ais3g_serve")) = False Then ret.ais3g_serve = Convert.ToInt32(Rdr("ais3g_serve"))
                        If Convert.IsDBNull(Rdr("ais3g_miss_call")) = False Then ret.ais3g_miss_call = Convert.ToInt32(Rdr("ais3g_miss_call"))
                        If Convert.IsDBNull(Rdr("ais3g_cancel")) = False Then ret.ais3g_cancel = Convert.ToInt32(Rdr("ais3g_cancel"))
                        If Convert.IsDBNull(Rdr("ais3g_notcall")) = False Then ret.ais3g_notcall = Convert.ToInt32(Rdr("ais3g_notcall"))
                        If Convert.IsDBNull(Rdr("ais3g_notcon")) = False Then ret.ais3g_notcon = Convert.ToInt32(Rdr("ais3g_notcon"))
                        If Convert.IsDBNull(Rdr("ais3g_notend")) = False Then ret.ais3g_notend = Convert.ToInt32(Rdr("ais3g_notend"))
                        If Convert.IsDBNull(Rdr("otc3g_regis")) = False Then ret.otc3g_regis = Convert.ToInt32(Rdr("otc3g_regis"))
                        If Convert.IsDBNull(Rdr("otc3g_serve")) = False Then ret.otc3g_serve = Convert.ToInt32(Rdr("otc3g_serve"))
                        If Convert.IsDBNull(Rdr("otc3g_miss_call")) = False Then ret.otc3g_miss_call = Convert.ToInt32(Rdr("otc3g_miss_call"))
                        If Convert.IsDBNull(Rdr("otc3g_cancel")) = False Then ret.otc3g_cancel = Convert.ToInt32(Rdr("otc3g_cancel"))
                        If Convert.IsDBNull(Rdr("otc3g_notcall")) = False Then ret.otc3g_notcall = Convert.ToInt32(Rdr("otc3g_notcall"))
                        If Convert.IsDBNull(Rdr("otc3g_notcon")) = False Then ret.otc3g_notcon = Convert.ToInt32(Rdr("otc3g_notcon"))
                        If Convert.IsDBNull(Rdr("otc3g_notend")) = False Then ret.otc3g_notend = Convert.ToInt32(Rdr("otc3g_notend"))

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


        'Get Insert Statement for table TB_REP_CUST_NETWORK_DAY
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SERVICE_DATE, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, GSM_REGIS, GSM_SERVE, GSM_MISS_CALL, GSM_CANCEL, GSM_NOTCALL, GSM_NOTCON, GSM_NOTEND, OTC_REGIS, OTC_SERVE, OTC_MISS_CALL, OTC_CANCEL, OTC_NOTCALL, OTC_NOTCON, OTC_NOTEND, NON_REGIS, NON_SERVE, NON_MISS_CALL, NON_CANCEL, NON_NOTCALL, NON_NOTCON, NON_NOTEND, AIS3G_REGIS, AIS3G_SERVE, AIS3G_MISS_CALL, AIS3G_CANCEL, AIS3G_NOTCALL, AIS3G_NOTCON, AIS3G_NOTEND, OTC3G_REGIS, OTC3G_SERVE, OTC3G_MISS_CALL, OTC3G_CANCEL, OTC3G_NOTCALL, OTC3G_NOTCON, OTC3G_NOTEND)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetDouble(_SERVICE_ID) & ", "
                sql += DB.SetString(_SERVICE_NAME_EN) & ", "
                sql += DB.SetString(_SERVICE_NAME_TH) & ", "
                sql += DB.SetDouble(_GSM_REGIS) & ", "
                sql += DB.SetDouble(_GSM_SERVE) & ", "
                sql += DB.SetDouble(_GSM_MISS_CALL) & ", "
                sql += DB.SetDouble(_GSM_CANCEL) & ", "
                sql += DB.SetDouble(_GSM_NOTCALL) & ", "
                sql += DB.SetDouble(_GSM_NOTCON) & ", "
                sql += DB.SetDouble(_GSM_NOTEND) & ", "
                sql += DB.SetDouble(_OTC_REGIS) & ", "
                sql += DB.SetDouble(_OTC_SERVE) & ", "
                sql += DB.SetDouble(_OTC_MISS_CALL) & ", "
                sql += DB.SetDouble(_OTC_CANCEL) & ", "
                sql += DB.SetDouble(_OTC_NOTCALL) & ", "
                sql += DB.SetDouble(_OTC_NOTCON) & ", "
                sql += DB.SetDouble(_OTC_NOTEND) & ", "
                sql += DB.SetDouble(_NON_REGIS) & ", "
                sql += DB.SetDouble(_NON_SERVE) & ", "
                sql += DB.SetDouble(_NON_MISS_CALL) & ", "
                sql += DB.SetDouble(_NON_CANCEL) & ", "
                sql += DB.SetDouble(_NON_NOTCALL) & ", "
                sql += DB.SetDouble(_NON_NOTCON) & ", "
                sql += DB.SetDouble(_NON_NOTEND) & ", "
                sql += DB.SetDouble(_AIS3G_REGIS) & ", "
                sql += DB.SetDouble(_AIS3G_SERVE) & ", "
                sql += DB.SetDouble(_AIS3G_MISS_CALL) & ", "
                sql += DB.SetDouble(_AIS3G_CANCEL) & ", "
                sql += DB.SetDouble(_AIS3G_NOTCALL) & ", "
                sql += DB.SetDouble(_AIS3G_NOTCON) & ", "
                sql += DB.SetDouble(_AIS3G_NOTEND) & ", "
                sql += DB.SetDouble(_OTC3G_REGIS) & ", "
                sql += DB.SetDouble(_OTC3G_SERVE) & ", "
                sql += DB.SetDouble(_OTC3G_MISS_CALL) & ", "
                sql += DB.SetDouble(_OTC3G_CANCEL) & ", "
                sql += DB.SetDouble(_OTC3G_NOTCALL) & ", "
                sql += DB.SetDouble(_OTC3G_NOTCON) & ", "
                sql += DB.SetDouble(_OTC3G_NOTEND)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_CUST_NETWORK_DAY
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
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "SERVICE_ID = " & DB.SetDouble(_SERVICE_ID) & ", "
                Sql += "SERVICE_NAME_EN = " & DB.SetString(_SERVICE_NAME_EN) & ", "
                Sql += "SERVICE_NAME_TH = " & DB.SetString(_SERVICE_NAME_TH) & ", "
                Sql += "GSM_REGIS = " & DB.SetDouble(_GSM_REGIS) & ", "
                Sql += "GSM_SERVE = " & DB.SetDouble(_GSM_SERVE) & ", "
                Sql += "GSM_MISS_CALL = " & DB.SetDouble(_GSM_MISS_CALL) & ", "
                Sql += "GSM_CANCEL = " & DB.SetDouble(_GSM_CANCEL) & ", "
                Sql += "GSM_NOTCALL = " & DB.SetDouble(_GSM_NOTCALL) & ", "
                Sql += "GSM_NOTCON = " & DB.SetDouble(_GSM_NOTCON) & ", "
                Sql += "GSM_NOTEND = " & DB.SetDouble(_GSM_NOTEND) & ", "
                Sql += "OTC_REGIS = " & DB.SetDouble(_OTC_REGIS) & ", "
                Sql += "OTC_SERVE = " & DB.SetDouble(_OTC_SERVE) & ", "
                Sql += "OTC_MISS_CALL = " & DB.SetDouble(_OTC_MISS_CALL) & ", "
                Sql += "OTC_CANCEL = " & DB.SetDouble(_OTC_CANCEL) & ", "
                Sql += "OTC_NOTCALL = " & DB.SetDouble(_OTC_NOTCALL) & ", "
                Sql += "OTC_NOTCON = " & DB.SetDouble(_OTC_NOTCON) & ", "
                Sql += "OTC_NOTEND = " & DB.SetDouble(_OTC_NOTEND) & ", "
                Sql += "NON_REGIS = " & DB.SetDouble(_NON_REGIS) & ", "
                Sql += "NON_SERVE = " & DB.SetDouble(_NON_SERVE) & ", "
                Sql += "NON_MISS_CALL = " & DB.SetDouble(_NON_MISS_CALL) & ", "
                Sql += "NON_CANCEL = " & DB.SetDouble(_NON_CANCEL) & ", "
                Sql += "NON_NOTCALL = " & DB.SetDouble(_NON_NOTCALL) & ", "
                Sql += "NON_NOTCON = " & DB.SetDouble(_NON_NOTCON) & ", "
                Sql += "NON_NOTEND = " & DB.SetDouble(_NON_NOTEND) & ", "
                Sql += "AIS3G_REGIS = " & DB.SetDouble(_AIS3G_REGIS) & ", "
                Sql += "AIS3G_SERVE = " & DB.SetDouble(_AIS3G_SERVE) & ", "
                Sql += "AIS3G_MISS_CALL = " & DB.SetDouble(_AIS3G_MISS_CALL) & ", "
                Sql += "AIS3G_CANCEL = " & DB.SetDouble(_AIS3G_CANCEL) & ", "
                Sql += "AIS3G_NOTCALL = " & DB.SetDouble(_AIS3G_NOTCALL) & ", "
                Sql += "AIS3G_NOTCON = " & DB.SetDouble(_AIS3G_NOTCON) & ", "
                Sql += "AIS3G_NOTEND = " & DB.SetDouble(_AIS3G_NOTEND) & ", "
                Sql += "OTC3G_REGIS = " & DB.SetDouble(_OTC3G_REGIS) & ", "
                Sql += "OTC3G_SERVE = " & DB.SetDouble(_OTC3G_SERVE) & ", "
                Sql += "OTC3G_MISS_CALL = " & DB.SetDouble(_OTC3G_MISS_CALL) & ", "
                Sql += "OTC3G_CANCEL = " & DB.SetDouble(_OTC3G_CANCEL) & ", "
                Sql += "OTC3G_NOTCALL = " & DB.SetDouble(_OTC3G_NOTCALL) & ", "
                Sql += "OTC3G_NOTCON = " & DB.SetDouble(_OTC3G_NOTCON) & ", "
                Sql += "OTC3G_NOTEND = " & DB.SetDouble(_OTC3G_NOTEND) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_CUST_NETWORK_DAY
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_CUST_NETWORK_DAY
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SERVICE_DATE, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, GSM_REGIS, GSM_SERVE, GSM_MISS_CALL, GSM_CANCEL, GSM_NOTCALL, GSM_NOTCON, GSM_NOTEND, OTC_REGIS, OTC_SERVE, OTC_MISS_CALL, OTC_CANCEL, OTC_NOTCALL, OTC_NOTCON, OTC_NOTEND, NON_REGIS, NON_SERVE, NON_MISS_CALL, NON_CANCEL, NON_NOTCALL, NON_NOTCON, NON_NOTEND, AIS3G_REGIS, AIS3G_SERVE, AIS3G_MISS_CALL, AIS3G_CANCEL, AIS3G_NOTCALL, AIS3G_NOTCON, AIS3G_NOTEND, OTC3G_REGIS, OTC3G_SERVE, OTC3G_MISS_CALL, OTC3G_CANCEL, OTC3G_NOTCALL, OTC3G_NOTCON, OTC3G_NOTEND FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
