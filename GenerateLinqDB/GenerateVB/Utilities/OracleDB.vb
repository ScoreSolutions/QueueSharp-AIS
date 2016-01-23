Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.OracleClient
Imports System.Text

Namespace Utilities
    Public Class OracleDB
        Protected Shared ErrorConnectionString As String = MessageResources.MSGEC001
        Protected Shared ErrorConnection As String = MessageResources.MSGEC002
        Protected Shared ErrorSetCommandConnection As String = MessageResources.MSGEC003
        Protected Shared ErrorInvalidCommandType As String = MessageResources.MSGEC004
        Protected Shared ErrorDuplicateParameter As String = MessageResources.MSGEC006
        Protected Shared ErrorNullParameter As String = MessageResources.MSGEC005
        Protected Shared ErrorExecuteNonQuery As String = MessageResources.MSGEC010
        Protected Shared ErrorExecuteReader As String = MessageResources.MSGEC011
        Protected Shared ErrorExecuteTable As String = MessageResources.MSGEC012
        Protected Shared ErrorExecuteScalar As String = MessageResources.MSGEC013
        Protected Shared ErrorDatabaseOther As String = MessageResources.MSGEC901
        Protected Shared ErrorUndefined As String = MessageResources.MSGEC902

        Public Shared Function SetDouble(ByVal number As Double) As String
            Return number.ToString()
        End Function
        Public Shared Function SetDecimal(ByVal number As Decimal) As String
            Return number.ToString()
        End Function
        Public Shared Function SetString(ByVal str As String) As String
            Dim ret As String = ""
            If str.Trim() = "" Then
                ret = "NULL"
            Else
                ret = Chr(39) & str.Trim().Replace("'", "''") & Chr(39)
            End If
            Return ret
        End Function
        Public Shared Function SetDate() As String
            Return SetDateTime(DateTime.Today)
        End Function
        Public Function SetDate(ByVal DateIn As DateTime) As String
            Return SetDateTime(DateIn)
        End Function
        Public Shared Function SetDateTime() As String
            Return SetDateTime(DateTime.Today)
        End Function
        Public Shared Function SetDateTime(ByVal DateIn As DateTime) As String
            Dim ret As String = ""
            If DateIn.Year = 1 Then
                ret = "NULL"
            Else
                ret = "'" & DateIn.Year.ToString("yyyy-MM-dd HH24:mi:ss") & "'"
            End If
            Return ret
        End Function

        '/// <summary>
        '/// Set order clause corresponding to Thai dictionary (Oracle Only).
        '/// </summary>
        '/// <param name="orderBy">The fields for sort data.</param>
        '/// <returns>The order clause.</returns>
        Public Shared Function SetSortString(ByVal orderBy As String) As String
            Dim ret As String = ""

            If orderBy.Trim() <> "" Then
                Dim field() As String = orderBy.Split(",")
                For i As Integer = 0 To field.Length - 1
                    Dim sort() As String = field(i).Trim().Split(" ")
                    Dim fieldName As String = sort(0).Trim().ToUpper
                    ret += IIf(ret = "", "", ", ") & "NLSSORT(UPPER(" & fieldName & "), 'NLS_SORT = THAI_DICTIONARY') "
                    If sort.Length = 2 Then ret += sort(1)
                Next
            End If

            Return ret

        End Function

        Public Shared Function GetExceptionMessage(ByVal ex As OracleException) As String
            Return String.Format(ErrorDatabaseOther, ex.ErrorCode.ToString(), ex.Message)
        End Function
        Protected Shared ReadOnly Property ConnectionString() As String
            Get
                Try
                    Return "Data Source=ORCL;Persist Security Info=True;User ID=excise_law;Password=password;Unicode=True"
                Catch ex As Exception
                    Throw New ApplicationException(ErrorConnectionString, ex)
                End Try
            End Get
        End Property

        Public Shared Function GetConnection() As OracleConnection
            Dim conn As OracleConnection
            Try
                conn = New OracleConnection(ConnectionString)
                conn.Open()
                Return conn
            Catch ex As Exception
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As ApplicationException
                Throw ex
            Catch ex As OracleException
                Throw New ApplicationException(ErrorConnection, ex)
            End Try
        End Function

        Public Shared Function GetConnection(ByVal connString As String) As OracleConnection
            Dim conn As OracleConnection
            Try
                conn = New OracleConnection(connString)
                conn.Open()
                Return conn
            Catch ex As Exception
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As ApplicationException
                Throw ex
            Catch ex As OracleException
                Throw New ApplicationException(ErrorConnection, ex)
            End Try
        End Function

        Public Shared Function GetNextID(ByVal seqName As String, ByVal trans As OracleTransaction) As Long
            Dim Sql As String = "select " & seqName & ".nextval maxID from dual"
            Dim dt As DataTable = ExecuteTable(Sql)

            Return Convert.ToInt64(dt.Rows(0)("maxID").ToString())
        End Function

        Public Shared Function ExecuteTable(ByVal sql As String) As DataTable
            Return ExecuteTable(sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal conn As OracleConnection) As DataTable
            Return ExecuteTable(sql, conn, Nothing)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal trans As OracleTransaction) As DataTable
            Return ExecuteTable(sql, Nothing, trans)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal conn As OracleConnection, ByVal trans As OracleTransaction) As DataTable
            Dim cmd As New OracleCommand
            Dim adapter As New OracleDataAdapter
            adapter.SelectCommand = cmd

            Dim dt As New DataTable

            Try
                If conn Is Nothing And trans Is Nothing Then
                    conn = New OracleConnection(ConnectionString)
                ElseIf trans IsNot Nothing And conn Is Nothing Then
                    conn = trans.Connection
                End If

                BuildCommand(cmd, conn, trans, CommandType.Text, sql, Nothing)
                adapter.Fill(dt)
                adapter.Dispose()
            Catch ex As ApplicationException
                adapter.Dispose()
                Throw ex
            Catch ex As OracleException
                adapter.Dispose()
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                adapter.Dispose()
                Throw New ApplicationException(ErrorExecuteTable, ex)
            End Try

            Return dt
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String) As OracleDataReader
            Return ExecuteReader(Sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal trans As OracleTransaction) As OracleDataReader
            Return ExecuteReader(Sql, Nothing, trans)
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal conn As OracleConnection) As OracleDataReader
            Return ExecuteReader(Sql, conn, Nothing)
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal conn As OracleConnection, ByVal trans As OracleTransaction) As OracleDataReader
            Dim command As New OracleCommand()
            Dim reader As OracleDataReader
            Dim LetClose As Boolean = False

            Try
                If trans Is Nothing Then
                    conn = New OracleConnection(ConnectionString)
                    LetClose = True
                Else
                    If conn Is Nothing Then
                        conn = trans.Connection
                    End If

                End If

                BuildCommand(command, conn, trans, CommandType.Text, Sql, Nothing)
                reader = IIf(LetClose = True, command.ExecuteReader(CommandBehavior.CloseConnection), command.ExecuteReader())
            Catch ex As ApplicationException
                Throw ex
            Catch ex As OracleException
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                Throw New ApplicationException(ErrorExecuteReader, ex)
            End Try

            Return reader
        End Function


        Public Shared Function ExecuteNonQuery(ByVal Sql As String) As Integer
            Return ExecuteNonQuery(Sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal conn As OracleConnection) As Integer
            Return ExecuteNonQuery(Sql, conn, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal trans As OracleTransaction) As Integer
            Return ExecuteNonQuery(Sql, Nothing, trans)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal conn As OracleConnection, ByVal trans As OracleTransaction) As Integer
            Dim retval As Integer
            Dim command As New OracleCommand

            Try
                If trans Is Nothing Then
                    conn = trans.Connection
                    BuildCommand(command, conn, trans, CommandType.Text, Sql, Nothing)
                    retval = command.ExecuteNonQuery
                Else
                    If conn Is Nothing Then
                        conn = trans.Connection
                    End If

                    BuildCommand(command, trans.Connection, trans, CommandType.Text, Sql, Nothing)
                    retval = command.ExecuteNonQuery
                End If
            Catch ex As ApplicationException
                Throw ex
            Catch ex As OracleException
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                Throw New ApplicationException(ErrorExecuteNonQuery, ex)
            End Try

            Return retval
        End Function

        Public Function ExecuteScarlar(ByVal Sql As String) As Object
            Return ExecuteScalar(Sql, Nothing, Nothing)
        End Function
        Public Function ExecuteScalar(ByVal Sql As String, ByVal conn As OracleConnection) As Object
            Return ExecuteScalar(Sql, conn, Nothing)
        End Function
        Public Function ExecuteScalar(ByVal Sql As String, ByVal trans As OracleTransaction) As Object
            Return ExecuteScalar(Sql, Nothing, trans)
        End Function
        Public Function ExecuteScalar(ByVal Sql As String, ByVal conn As OracleConnection, ByVal trans As OracleTransaction) As Object
            Dim command As New OracleCommand
            Dim ret As Object
            Try
                If trans Is Nothing And conn Is Nothing Then
                    conn = New OracleConnection(ConnectionString)
                    BuildCommand(command, conn, Nothing, CommandType.Text, Sql, Nothing)
                ElseIf trans IsNot Nothing And conn Is Nothing Then
                    conn = trans.Connection
                    BuildCommand(command, conn, trans, CommandType.Text, Sql, Nothing)
                End If

                ret = command.ExecuteOracleScalar

            Catch ex As ApplicationException
                Throw ex
            Catch ex As OracleException
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                Throw New ApplicationException(ErrorExecuteScalar, ex)
            End Try

            Return ret
        End Function

        Private Shared Sub BuildCommand(ByVal cmd As OracleCommand, ByVal conn As OracleConnection, ByVal trans As OracleTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As OracleParameter)
            If conn.State <> ConnectionState.Open Then
                Try
                    conn.Open()
                Catch ex As OracleException
                    Throw New ApplicationException(GetExceptionMessage(ex), ex)
                Catch ex As ApplicationException
                    Throw (ex)
                Catch ex As Exception
                    Throw New ApplicationException(ErrorConnection, ex)
                End Try
            End If

            Try
                cmd.Connection = conn
            Catch ex As Exception
                Throw New ApplicationException(ErrorSetCommandConnection, ex)
            End Try
            cmd.CommandText = cmdText

            If trans IsNot Nothing Then
                cmd.Transaction = trans
            End If

            Try
                cmd.CommandType = cmdType
                cmd.CommandTimeout = 120
            Catch ex As ArgumentException
                Throw New ApplicationException(ErrorInvalidCommandType, ex)
            End Try

            If cmdParms IsNot Nothing Then
                For Each parm As OracleParameter In cmdParms
                    Try
                        cmd.Parameters.Add(parm)
                    Catch ex As ArgumentNullException
                        Throw New ApplicationException(ErrorNullParameter, ex)
                    Catch ex As ArgumentException
                        Throw New ApplicationException(ErrorDuplicateParameter, ex)
                    End Try
                Next
            End If
        End Sub
    End Class
End Namespace

