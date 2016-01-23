﻿Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Windows.Forms
Imports System.Web
Imports AIS_Import.Common.Utilities

Namespace Utilities
    Public Class SqlDB
        Public Shared Function SetDouble(ByVal number As System.Nullable(Of Double)) As String
            Dim ret As String
            If Convert.IsDBNull(number) Then
                ret = "NULL"
            ElseIf number Is Nothing Then
                ret = "NULL"
            Else
                ret = number.ToString()
            End If
            Return ret
        End Function
        Public Shared Function SetDecimal(ByVal number As System.Nullable(Of Decimal)) As String
            Dim ret As String
            If Convert.IsDBNull(number) Then
                ret = "NULL"
            ElseIf number Is Nothing Then
                ret = "NULL"
            Else
                ret = number.ToString()
            End If
            Return ret
        End Function
        Public Shared Function SetString(ByVal str As String) As String
            Dim ret As String = ""
            If str Is Nothing Or str.Trim() = "" Then
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
            If DateIn.Year = 1 Or Convert.IsDBNull(DateIn) Then
                ret = "NULL"
            ElseIf DateIn.Year > 2500 Then
                Dim vYear As String = DateIn.Year - 543
                ret = "'" & vYear & "-" & DateIn.ToString("MM-dd HH:mm:ss") & "'"
            Else
                ret = "'" & DateIn.Year & "-" & DateIn.ToString("MM-dd HH:mm:ss") & "'"
            End If
            Return ret
        End Function

        Public Shared Function GetExceptionMessage(ByVal ex As SqlException) As String
            'Return String.Format(ErrorDatabaseOther, ex.ErrorCode.ToString(), ex.Message)
        End Function

        Private Shared ReadOnly Property INIFileName() As IniReader
            Get
                'Application.StartupPath = C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0
                Dim INIFlie As String = "C:\Windows\QueueSharp.ini"
                Dim ini As New IniReader(INIFlie)
                ini.Section = "SETTING"
                Return ini
            End Get
        End Property

        Public Shared Function GetStartupPath() As String
            Return Application.StartupPath
        End Function

        Public Shared ReadOnly Property MainServer() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("HQMainServer")
            End Get
        End Property
        Public Shared ReadOnly Property MainDbName() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("HQMainDatabase")
            End Get
        End Property

        Public Shared ReadOnly Property MainDbUserID() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("HQMainUsername")
            End Get
        End Property

        Public Shared ReadOnly Property MainDbPwd() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("HQMainPassword")
            End Get
        End Property

        Public Shared ReadOnly Property DrServer() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("BackupServer")
            End Get
        End Property
        Public Shared ReadOnly Property DrDbName() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("BackupDatabase")
            End Get
        End Property

        Public Shared ReadOnly Property DrDbUserID() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("BackupUsername")
            End Get
        End Property

        Public Shared ReadOnly Property DrDbPwd() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("BackupPassword")
            End Get
        End Property

        Protected Shared ReadOnly Property MainConnectionString() As String
            Get
                Try
                    Return "Data Source=" & MainServer() & ";Initial Catalog=" & MainDbName() & ";User ID=" & MainDbUserID() & ";Password=" & MainDbPwd()
                Catch ex As Exception
                    '                    Throw New ApplicationException(ErrorConnectionString, ex)
                End Try
            End Get
        End Property

        Protected Shared ReadOnly Property DrConnectionString() As String
            Get
                Try
                    Return "Data Source=" & DrServer() & ";Initial Catalog=" & DrDbName() & ";User ID=" & DrDbUserID() & ";Password=" & DrDbPwd()
                Catch ex As Exception
                    'Throw New ApplicationException(ErrorConnectionString, ex)
                End Try
            End Get
        End Property

        'Protected Shared ReadOnly Property MainConnectionString() As String
        '    Get
        '        Try
        '            Return "Data Source=user-pc;Initial Catalog=AIS-SHOP;User ID=score;Password=score"
        '        Catch ex As Exception
        '            '                    Throw New ApplicationException(ErrorConnectionString, ex)
        '        End Try
        '    End Get
        'End Property

        'Protected Shared ReadOnly Property DrConnectionString() As String
        '    Get
        '        Try
        '            Return "Data Source=localhost;Initial Catalog=AIS_CENTRAL;User ID=score;Password=score"
        '        Catch ex As Exception
        '            'Throw New ApplicationException(ErrorConnectionString, ex)
        '        End Try
        '    End Get
        'End Property

        Public Shared Sub CreateTextFile(ByVal sqlText As String)
            Dim FILE_NAME As String = "D:\Test.txt"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(sqlText)
            objWriter.Close()
        End Sub

        Public Shared Sub CreateLogFile(ByVal sqlText As String)
            Dim FILE_NAME As String = "D:\CustomerImportLog.txt"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(sqlText)
            objWriter.Close()
        End Sub


        Public Shared Function GetConnection() As SqlConnection
            Dim conn As SqlConnection
            Try
                conn = New SqlConnection(MainConnectionString)
                conn.Open()
                Return conn
            Catch ex As ApplicationException
                Throw ex
            Catch ex As SqlException
                conn = New SqlConnection(DrConnectionString)
                conn.Open()
                Return conn
                'Throw New ApplicationException(ErrorConnection, ex)
            Catch ex As Exception
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            End Try
        End Function

        Public Shared Function GetConnection(ByVal connString As String) As SqlConnection
            Dim conn As SqlConnection
            Try
                conn = New SqlConnection(connString)
                conn.Open()
                Return conn
            Catch ex As ApplicationException
                Throw ex
            Catch ex As SqlException

                'Throw New ApplicationException(ErrorConnection, ex)
            Catch ex As Exception
                Throw New ApplicationException(GetExceptionMessage(ex), ex)
            End Try
        End Function


        Public Shared Function ChkConnection() As Boolean
            Dim ret As Boolean = False
            Dim conn As SqlConnection
            Try
                conn = New SqlConnection(MainConnectionString)
                conn.Open()

                ret = True
            Catch ex As ApplicationException
                'Throw ex
                ret = False
            Catch ex As SqlException
                'Throw New ApplicationException(ErrorConnection, ex)
                ret = False
            Catch ex As Exception
                'Throw New ApplicationException(GetExceptionMessage(ex), ex)
                ret = False
            End Try

            Return ret
        End Function
        Public Shared Function GetNextID(ByVal fldName As String, ByVal tbName As String, ByVal trans As SqlTransaction) As Long
            Dim ret As Long
            Dim Sql As String = "select max(" & fldName & ") maxID from " & tbName
            Dim dt As DataTable = ExecuteTable(Sql, trans)
            If Convert.IsDBNull(dt.Rows(0)("maxID")) = False Then
                ret = Convert.ToInt64(dt.Rows(0)("maxID").ToString()) + 1
            Else
                ret = 1
            End If

            Return ret
        End Function

        Public Shared Function GetDateNow(ByVal rtType As String, ByVal trans As SqlTransaction) As String
            'rtType = D  ให้คืนค่าเป็นวันที่ในรูปแบบ yyyy-MM-dd
            'rtType = T  ให้คืนค่าเป็นเวลาปัจจุบัน hh:mm
            Dim ret As String
            Dim dt As DataTable = ExecuteTable("select convert(varchar(10),getdate(),120) date_now, CONVERT(varchar(5),getdate(),108) time_now", trans)
            If rtType = "D" Then
                ret = dt.Rows(0)("date_now")
            Else
                ret = dt.Rows(0)("time_now")
            End If

            Return ret

        End Function

        Public Shared Function ExecuteTable(ByVal sql As String) As DataTable
            Return ExecuteTable(sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal conn As SqlConnection) As DataTable
            Return ExecuteTable(sql, conn, Nothing)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal trans As SqlTransaction) As DataTable
            Return ExecuteTable(sql, Nothing, trans)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As DataTable
            Dim cmd As New SqlCommand
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd

            Dim dt As New DataTable

            Try
                If conn Is Nothing And trans Is Nothing Then
                    conn = New SqlConnection(MainConnectionString)
                ElseIf trans IsNot Nothing And conn Is Nothing Then
                    conn = trans.Connection
                End If

                BuildCommand(cmd, conn, trans, CommandType.Text, sql, Nothing)
                adapter.Fill(dt)
                adapter.Dispose()
                'conn.Close()
            Catch ex As ApplicationException
                adapter.Dispose()
                'Throw ex
            Catch ex As SqlException
                adapter.Dispose()
                'Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                adapter.Dispose()
                'Throw New ApplicationException(ErrorExecuteTable, ex)
            End Try

            Return dt
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String) As SqlDataReader
            Return ExecuteReader(Sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal trans As SqlTransaction) As SqlDataReader
            Return ExecuteReader(Sql, Nothing, trans)
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal conn As SqlConnection) As SqlDataReader
            Return ExecuteReader(Sql, conn, Nothing)
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As SqlDataReader
            Dim command As New SqlCommand()
            Dim reader As SqlDataReader
            Dim LetClose As Boolean = False

            Try
                If trans IsNot Nothing And conn Is Nothing Then
                    conn = trans.Connection
                ElseIf conn Is Nothing Then
                    conn = GetConnection()
                    LetClose = True
                End If

                BuildCommand(command, conn, trans, CommandType.Text, Sql, Nothing)
                'reader = IIf(LetClose = True, command.ExecuteReader(CommandBehavior.CloseConnection), command.ExecuteReader())
                reader = command.ExecuteReader()
                'conn.Close()
            Catch ex As ApplicationException
                'Throw ex
            Catch ex As SqlException
                'Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                'Throw New ApplicationException(ErrorExecuteReader, ex)
            End Try

            Return reader
        End Function


        Public Shared Function ExecuteNonQuery(ByVal Sql As String) As Integer
            Return ExecuteNonQuery(Sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal conn As SqlConnection) As Integer
            Return ExecuteNonQuery(Sql, conn, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal trans As SqlTransaction) As Integer
            Return ExecuteNonQuery(Sql, Nothing, trans, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal trans As SqlTransaction, ByVal cmdParms() As SqlParameter) As Integer
            Return ExecuteNonQuery(Sql, Nothing, trans, cmdParms)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdParms() As SqlParameter) As Integer
            Dim retval As Integer
            Dim command As New SqlCommand

            Try
                If trans Is Nothing Then
                    conn = New SqlConnection(MainConnectionString)
                    BuildCommand(command, conn, trans, CommandType.Text, Sql, cmdParms)
                    retval = command.ExecuteNonQuery
                Else
                    If trans IsNot Nothing And conn Is Nothing Then
                        conn = trans.Connection
                    End If

                    BuildCommand(command, trans.Connection, trans, CommandType.Text, Sql, cmdParms)
                    retval = command.ExecuteNonQuery
                End If
            Catch ex As ApplicationException
                'Throw ex
                CreateLogFile(Sql & Chr(13) & ex.Message & Chr(13) & Chr(13))
            Catch ex As SqlException
                CreateLogFile(Sql & Chr(13) & ex.Message & Chr(13) & Chr(13))
                'Throw New ApplicationException(GetExceptionMessage(ex), ex)
            Catch ex As Exception
                CreateLogFile(Sql & Chr(13) & ex.Message & Chr(13) & Chr(13))
                'Throw New ApplicationException(ErrorExecuteNonQuery, ex)
            End Try

            Return retval
        End Function

        Private Shared Sub BuildCommand(ByVal cmd As SqlCommand, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As SqlParameter)
            If conn.State <> ConnectionState.Open Then
                Try
                    conn.Open()
                Catch ex As SqlException
                    Throw New ApplicationException(GetExceptionMessage(ex), ex)
                Catch ex As ApplicationException
                    Throw (ex)
                Catch ex As Exception
                    'Throw New ApplicationException(ErrorConnection, ex)
                End Try
            End If

            Try
                cmd.Connection = conn
            Catch ex As Exception
                'Throw New ApplicationException(ErrorSetCommandConnection, ex)
            End Try
            cmd.CommandText = cmdText

            If trans IsNot Nothing Then
                cmd.Transaction = trans
            End If

            Try
                cmd.CommandType = cmdType
                cmd.CommandTimeout = 120
            Catch ex As ArgumentException
                'Throw New ApplicationException(ErrorInvalidCommandType, ex)
            End Try

            If cmdParms IsNot Nothing Then
                For Each parm As SqlParameter In cmdParms
                    Try
                        cmd.Parameters.Add(parm)
                    Catch ex As ArgumentNullException
                        'Throw New ApplicationException(ErrorNullParameter, ex)
                    Catch ex As ArgumentException
                        'Throw New ApplicationException(ErrorDuplicateParameter, ex)
                    End Try
                Next
            End If
        End Sub
    End Class
End Namespace

