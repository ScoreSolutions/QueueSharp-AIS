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
    'Represents a transaction for VW_Q_BEFORE_AFTER view ShLinqDB.
    '[Create by  on Febuary, 23 2012]
    Public Class VwQBeforeAfterShLinqDB
        Public sub VwQBeforeAfterShLinqDB()

        End Sub 
        ' VW_Q_BEFORE_AFTER
        Const _viewName As String = "VW_Q_BEFORE_AFTER"

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
        Dim _QUEUE_NO As  String  = ""
        Dim _CUSTOMER_ID As  String  = ""
        Dim _ITEM_ID As  System.Nullable(Of Long)  = 0
        Dim _ITEM_BEFORE As  System.Nullable(Of Long)  = 0
        Dim _ITEM_BEFORE_STATUS As Long = 0

        'Generate Field Property 
        <Column(Storage:="_QUEUE_NO", DbType:="VarChar(20)")>  _
        Public Property QUEUE_NO() As  String 
            Get
                Return _QUEUE_NO
            End Get
            Set(ByVal value As  String )
               _QUEUE_NO = value
            End Set
        End Property 
        <Column(Storage:="_CUSTOMER_ID", DbType:="VarChar(20)")>  _
        Public Property CUSTOMER_ID() As  String 
            Get
                Return _CUSTOMER_ID
            End Get
            Set(ByVal value As  String )
               _CUSTOMER_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ID", DbType:="Int")>  _
        Public Property ITEM_ID() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_BEFORE", DbType:="Int")>  _
        Public Property ITEM_BEFORE() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_BEFORE
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_BEFORE = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_BEFORE_STATUS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_BEFORE_STATUS() As Long
            Get
                Return _ITEM_BEFORE_STATUS
            End Get
            Set(ByVal value As Long)
               _ITEM_BEFORE_STATUS = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _QUEUE_NO = ""
            _CUSTOMER_ID = ""
            _ITEM_ID = 0
            _ITEM_BEFORE = 0
            _ITEM_BEFORE_STATUS = 0
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


        '/// Returns an indication whether the record of VW_Q_BEFORE_AFTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the record of VW_Q_BEFORE_AFTER by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_id")) = False Then _customer_id = Rdr("customer_id").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_before")) = False Then _item_before = Convert.ToInt32(Rdr("item_before"))
                        If Convert.IsDBNull(Rdr("item_before_status")) = False Then _item_before_status = Convert.ToInt32(Rdr("item_before_status"))
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


        '/// Returns an indication whether the record of VW_Q_BEFORE_AFTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As VwQBeforeAfterShLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then _queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_id")) = False Then _customer_id = Rdr("customer_id").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then _item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_before")) = False Then _item_before = Convert.ToInt32(Rdr("item_before"))
                        If Convert.IsDBNull(Rdr("item_before_status")) = False Then _item_before_status = Convert.ToInt32(Rdr("item_before_status"))

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


        '/// Returns an indication whether the Class Data of VW_Q_BEFORE_AFTER by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As VwQBeforeAfterShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New VwQBeforeAfterShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("queue_no")) = False Then ret.queue_no = Rdr("queue_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_id")) = False Then ret.customer_id = Rdr("customer_id").ToString()
                        If Convert.IsDBNull(Rdr("item_id")) = False Then ret.item_id = Convert.ToInt32(Rdr("item_id"))
                        If Convert.IsDBNull(Rdr("item_before")) = False Then ret.item_before = Convert.ToInt32(Rdr("item_before"))
                        If Convert.IsDBNull(Rdr("item_before_status")) = False Then ret.item_before_status = Convert.ToInt32(Rdr("item_before_status"))

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


        'Get Select Statement for table VW_Q_BEFORE_AFTER
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & viewName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
