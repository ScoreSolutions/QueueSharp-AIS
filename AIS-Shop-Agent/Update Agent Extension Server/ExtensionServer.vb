﻿Imports System.Data
Imports System.Data.SqlClient
Imports Update_Agent_Extension_Server.Org.Mentalis.Files
Imports System.IO

Module ExtensionServer

    Public INIFileName As String = Application.StartupPath & "\UpdateAgentExtensionServer.ini"
    Public INIErrorLog As String = Application.StartupPath & "\Logfile\" & Date.Now.Date.ToShortDateString & Date.Now.Date.ToShortTimeString & ".ini"
    Public Conn As New SqlConnection
    Public ConnecetionPrimaryDB As Boolean = True


    Function getConnectionString() As String
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Return "Data Source=" & ini.ReadString("Server") & ";Initial Catalog=" & ini.ReadString("Database") & ";Persist Security Info=True;User ID=" & ini.ReadString("Username") & ";Password=" & ini.ReadString("Password") & ";"
    End Function
    Function getBackupConnectionString() As String
        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Return "Data Source=" & ini.ReadString("Server1") & ";Initial Catalog=" & ini.ReadString("Database1") & ";Persist Security Info=True;User ID=" & ini.ReadString("Username1") & ";Password=" & ini.ReadString("Password1") & ";"
    End Function

    Public Function CheckConn(Optional ByVal msg As Boolean = True) As Boolean
        Try
            Dim ConnectionString As String = ""
            If ConnecetionPrimaryDB = True Then
                ConnectionString = getConnectionString()
            Else
                ConnectionString = getBackupConnectionString()
            End If

            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Conn.ConnectionString = ConnectionString
            Conn.Open()
            Return True
        Catch ex As Exception
            Try
                Dim ConnectionString As String = ""
                If ConnecetionPrimaryDB = True Then
                    ConnecetionPrimaryDB = False
                    ConnectionString = getBackupConnectionString()
                Else
                    ConnecetionPrimaryDB = True
                    ConnectionString = getConnectionString()
                End If
                Conn.ConnectionString = ConnectionString
                Conn.Open()
                Return True
            Catch ex1 As Exception
                Return False
            End Try
        End Try
        Return False
    End Function

    Public Function getDataTable(ByVal SQL As String) As DataTable
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            If CheckConn(False) = True Then
                da = New SqlDataAdapter(SQL, Conn)
                da.Fill(dt)
                Conn.Close()
                Return dt
            End If
        Catch ex As Exception : End Try
        Return New DataTable
    End Function

    Public Sub executeSQL(ByVal SQL As String, Optional ByVal forceExit As Boolean = True)
        If SQL.Trim <> "" Then
            Dim cmd As New SqlCommand(SQL)
            Try
                If CheckConn(False) = True Then
                    cmd.Connection = Conn
                    cmd.ExecuteNonQuery()
                    Conn.Close()
                End If
            Catch ex As Exception : End Try
        End If
    End Sub

    Public Sub WriteTextfile(ByVal ValTxt As String)
        Try
            If ValTxt = "" Then
                ValTxt = " "
            End If
            Dim txtFile As New StreamWriter(INIErrorLog, False, System.Text.Encoding.UTF8, ValTxt.Length)
            With txtFile
                .Write(ValTxt)
                .Close()
            End With
        Catch ex As Exception : End Try

    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Public Sub UpdateVersion_Company()
        Dim v As String = getMyVersion()
        frmMain.Text = Replace(frmMain.Text, "[%V%]", v)
    End Sub

End Module
