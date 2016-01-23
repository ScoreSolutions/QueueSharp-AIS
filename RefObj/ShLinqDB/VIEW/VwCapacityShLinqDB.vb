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
    'Represents a transaction for VW_CAPACITY view ShLinqDB.
    '[Create by  on Febuary, 27 2012]
    Public Class VwCapacityShLinqDB
        Public sub VwCapacityShLinqDB()

        End Sub 
        ' VW_CAPACITY
        Const _viewName As String = "VW_CAPACITY"

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
        Dim _APP_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _START_SLOT As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _END_SLOT As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)

        'Generate Field Property 
        <Column(Storage:="_APP_DATE", DbType:="DateTime")>  _
        Public Property APP_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _APP_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _APP_DATE = value
            End Set
        End Property 
        <Column(Storage:="_START_SLOT", DbType:="DateTime")>  _
        Public Property START_SLOT() As  System.Nullable(Of DateTime) 
            Get
                Return _START_SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _START_SLOT = value
            End Set
        End Property 
        <Column(Storage:="_END_SLOT", DbType:="DateTime")>  _
        Public Property END_SLOT() As  System.Nullable(Of DateTime) 
            Get
                Return _END_SLOT
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _END_SLOT = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _APP_DATE = New DateTime(1,1,1)
            _START_SLOT = New DateTime(1,1,1)
            _END_SLOT = New DateTime(1,1,1)
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


        '/// Returns an indication whether the record of VW_CAPACITY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the record of VW_CAPACITY by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("app_date")) = False Then _app_date = Convert.ToDateTime(Rdr("app_date"))
                        If Convert.IsDBNull(Rdr("start_slot")) = False Then _start_slot = Convert.ToDateTime(Rdr("start_slot"))
                        If Convert.IsDBNull(Rdr("end_slot")) = False Then _end_slot = Convert.ToDateTime(Rdr("end_slot"))
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


        '/// Returns an indication whether the record of VW_CAPACITY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As VwCapacityShLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("app_date")) = False Then _app_date = Convert.ToDateTime(Rdr("app_date"))
                        If Convert.IsDBNull(Rdr("start_slot")) = False Then _start_slot = Convert.ToDateTime(Rdr("start_slot"))
                        If Convert.IsDBNull(Rdr("end_slot")) = False Then _end_slot = Convert.ToDateTime(Rdr("end_slot"))

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


        '/// Returns an indication whether the Class Data of VW_CAPACITY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As VwCapacityShParaDB
            ClearData()
            _haveData  = False
            Dim ret As New VwCapacityShParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("app_date")) = False Then ret.app_date = Convert.ToDateTime(Rdr("app_date"))
                        If Convert.IsDBNull(Rdr("start_slot")) = False Then ret.start_slot = Convert.ToDateTime(Rdr("start_slot"))
                        If Convert.IsDBNull(Rdr("end_slot")) = False Then ret.end_slot = Convert.ToDateTime(Rdr("end_slot"))

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


        'Get Select Statement for table VW_CAPACITY
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT * FROM " & viewName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
