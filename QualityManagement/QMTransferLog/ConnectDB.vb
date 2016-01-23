Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports AMS
Imports ShLinqDB.Common.Utilities


Public Class ConnectDB
    'Private Conn As SqlDB
    Dim configINI As New Profile.Ini("C:\Windows" & "\QueueSharp.ini")
    Dim pServer As String = configINI.GetValue("Setting", "MainServer")
    Dim pDBName As String = configINI.GetValue("Setting", "MainDatabase")
    Dim pUsername As String = configINI.GetValue("Setting", "MainUsername")
    Dim pPassword As String = Engine.Common.FunctionEng.DeCripPwd(configINI.GetValue("Setting", "MainPassword"))
   
    'SQL Server
    Public Function SqlStrCon() As SqlConnection
        Return New SqlConnection("Data Source=" & pServer & ";Initial Catalog=" & pDBName & ";Persist Security Info=True;User ID=" & pUsername & ";Password=" & pPassword & ";Connect Timeout=3;")
    End Function

    Function getConnectionString() As String
        Return "Data Source=" & pServer & ";Initial Catalog=" & pDBName & ";Persist Security Info=True;User ID=" & pUsername & ";Password=" & pPassword & ";Connect Timeout=3;"
    End Function

    Public Function GetConn(Optional ByVal msg As Boolean = True) As SqlConnection
        Dim conn As New SqlConnection
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.ConnectionString = New ConnectDB().getConnectionString
            conn.Open()
            Return conn
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function
End Class
Public Class DBClass
 
#Region ""

    Public Function SqlGet(ByVal sql As String, ByVal tblName As String) As DataSet
        Dim conn As New SqlConnection
        conn = New ConnectDB().GetConn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If

        Dim da As New SqlDataAdapter(sql, conn)
        Dim ds As New DataSet
        da.Fill(ds, tblName)
        Return ds
    End Function
    Public Function SqlGet(ByVal sql As String, ByVal tblName As String, ByVal parameters As SqlParameterCollection) As DataSet
        Dim conn As SqlConnection
        conn = New ConnectDB().GetConn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        Dim da As New SqlDataAdapter(sql, conn)
        Dim ds As New DataSet
        For Each param As SqlParameter In parameters
            da.SelectCommand.Parameters.AddWithValue(param.ParameterName, param.SqlDbType).Value = param.Value
        Next
        da.Fill(ds, tblName)
        Return ds
    End Function
    Public Function SqlExecuteScalar(ByVal sql As String) As Integer
        Dim i As Integer
        Dim conn As SqlConnection
        conn = New ConnectDB().GetConn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        Dim cmd As New SqlCommand(sql, conn)

        Try
            conn.Open()
            i = cmd.ExecuteScalar
        Catch ex As Exception
            i = 0
        End Try
        conn.Close()
        Return i
    End Function
    Public Function SqlExecute(ByVal sql As String) As Integer
        Dim i As Integer
        Dim conn As SqlConnection
        Try
            conn = New ConnectDB().GetConn
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Dim cmd As New SqlCommand(sql, conn)

            Try
                conn.Open()
                i = cmd.ExecuteNonQuery()
            Catch ex As Exception
                i = 0
            End Try
            conn.Close()
            Return i
        Catch ex As Exception
            Return 0
        End Try

    End Function
    Public Function SqlExecute(ByVal sql As String, ByVal parameters As SqlParameterCollection) As Integer
        Dim i As Integer
        Dim conn As SqlConnection
        conn = New ConnectDB().GetConn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        Dim cmd As New SqlCommand(sql, conn)
        For Each param As SqlParameter In parameters
            cmd.Parameters.AddWithValue(param.ParameterName, param.SqlDbType).Value = param.Value
        Next

        Try
            conn.Open()
            i = cmd.ExecuteNonQuery()
        Catch ex As Exception
            i = 0
        End Try

        conn.Close()
        Return i
    End Function
    Public Function SqlExcSto(ByVal stpName As String, ByVal tblName As String, ByVal parameters As SqlParameterCollection) As DataSet
        Dim conn As SqlConnection
        conn = New ConnectDB().GetConn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        Dim cmd As New SqlCommand()
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = stpName
        For Each param As SqlParameter In parameters
            cmd.Parameters.AddWithValue(param.ParameterName, param.SqlDbType).Value = param.Value
        Next
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds, tblName)
        Return ds
    End Function
#End Region

  
End Class