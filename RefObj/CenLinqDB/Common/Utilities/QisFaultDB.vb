Imports System.Windows.Forms
Imports System.Data.SqlClient

Namespace Common.Utilities
    Public Class QisFaultDB
        Private Structure QisFaultDBConfig
            Dim FaultServerName As String
            Dim FaultDbUserID As String
            Dim FaultDbPwd As String
            Dim FaultDbName As String
        End Structure
        Private Shared ReadOnly Property GetQisFaultDBConfig(ByVal INIFile As String) As QisFaultDBConfig
            Get
                'Application.StartupPath = C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0
                If INIFile = "" Then
                    INIFile = Application.StartupPath & "\Config.ini"
                End If

                Dim ini As New IniReader(INIFile)
                ini.Section = "QisFaultDB"

                Dim cnf As New QisFaultDBConfig
                cnf.FaultServerName = ini.ReadString("FaultServerName")
                cnf.FaultDbUserID = ini.ReadString("FaultDbUserID")
                cnf.FaultDbPwd = SqlDB.DeCripPwd(ini.ReadString("FaultDbPwd"))
                cnf.FaultDbName = ini.ReadString("FaultDbName")
                ini = Nothing
                Return cnf
            End Get
        End Property

        Private Shared ReadOnly Property GetFaultConnectionString(ByVal INIFile As String) As String
            Get
                Dim ret As String = ""
                Try
                    Dim cnf As New QisFaultDBConfig
                    cnf = GetQisFaultDBConfig(INIFile)
                    ret = "Data Source=" & cnf.FaultServerName & ";Initial Catalog=" & cnf.FaultDbName & ";User ID=" & cnf.FaultDbUserID & ";Password=" & cnf.FaultDbPwd
                    cnf = Nothing
                Catch ex As Exception
                    'Throw New ApplicationException(ErrorConnectionString, ex)
                End Try
                Return ret
            End Get
        End Property

        Public Shared Function ExecuteNonQuery(ByVal sql As String, ByVal INIFile As String) As Integer
            Dim ret As Integer = 0
            Dim conn As New SqlConnection(GetFaultConnectionString(INIFile))
            Try
                conn.Open()

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 240
                ret = cmd.ExecuteNonQuery
            Catch ex As Exception
            Finally
                conn.Close()
            End Try
            Return ret
        End Function

        Public Shared Function GetDataTable(ByVal sql As String, ByVal INIFile As String) As DataTable
            Dim ret As New DataTable
            Dim conn As New SqlConnection(GetFaultConnectionString(INIFile))
            Try
                conn.Open()
                Dim da As New SqlDataAdapter(sql, conn)
                da.Fill(ret)
                da.Dispose()
            Catch ex As Exception
            Finally
                conn.Close()
            End Try
            Return ret
        End Function

        Public Shared Function GetLastID(ByVal tbName As String, ByVal INIFile As String) As Long
            Dim ret As Long
            Dim Sql As String = "select IDENT_CURRENT('" & tbName & "') lastID "
            Dim dt As DataTable = GetDataTable(Sql, INIFile)
            If Convert.IsDBNull(dt.Rows(0)("lastID")) = False Then
                ret = Convert.ToInt64(dt.Rows(0)("lastID").ToString())
            Else
                ret = 1
            End If

            Return ret
        End Function
    End Class
End Namespace

