Imports Engine.Common.ShopConnectDBENG
Imports System.Data.SqlClient

Namespace Common
    Public Class QueueSharpENG
        Public Shared Function ExecuteSqlToDisplay(ByVal sql As String, ByVal IniFilename As String) As Boolean
            Dim ret As Boolean = False

            Dim ini As New IniReader(IniFilename)
            ini.Section = "Setting"
            Try
                If ini.ReadString("Server").Trim <> ini.ReadString("Server1") Then
                    Dim ConnD As New SqlConnection(getDisplayConnectionString(ini.Filename))
                    ConnD.Open()

                    Dim cmd As New SqlCommand(sql, ConnD)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    ConnD.Close()
                End If
                ret = True
            Catch ex As SqlException
                ret = False
            End Try

            Return ret
        End Function

        Public Shared Function GetDataTableFromDisplay(ByVal sql As String, ByVal IniFilename As String) As DataTable
            Dim ret As New DataTable

            Dim ini As New IniReader(IniFilename)
            ini.Section = "Setting"
            'If ini.ReadString("Server").Trim <> ini.ReadString("Server1") Then
            Try
                Dim ConnD As New SqlConnection(getDisplayConnectionString(ini.Filename))
                ConnD.Open()
                Dim da As New SqlDataAdapter(sql, ConnD)
                Dim dt As New DataTable
                da.Fill(dt)
                
                ConnD.Close()
                ret = dt
            Catch ex As SqlException

            End Try
            'Else
            'ret = True
            'End If

            Return ret
        End Function

        
    End Class
End Namespace

