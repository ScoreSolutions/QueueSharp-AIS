Imports Engine.Common.ShopConnectDBENG
Imports System.Data.SqlClient

Namespace Common
    Public Class KioskENG
        Private Shared _err As String = ""
        Public Shared ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property
        Public Shared Function ExecuteSqlToDisplay(ByVal sql As String, ByVal IniFilename As String) As Boolean
            Dim ret As Boolean = False
            _err = ""

            Dim ini As New IniReader(IniFilename)
            ini.Section = "Setting"
            Try
                If ini.ReadString("Server") <> ini.ReadString("Server1") Then
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
                _err = ex.Message & " ### " & sql
            End Try

            Return ret
        End Function
    End Class
End Namespace

