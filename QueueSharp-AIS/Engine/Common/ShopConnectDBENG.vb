Imports System.Data.SqlClient
Imports Engine.Common
Imports System.Windows.Forms
Imports System.Web
Imports System.Net
Imports System.Text
Imports System.IO

Namespace Common
    Public Class ShopConnectDBENG

        'Public Shared Function PingServer(ByVal ServerIPAddress As String) As Boolean
        '    Dim ret As Boolean = False
        '    Try
        '        If My.Computer.Network.Ping(ServerIPAddress, 1000) = True Then
        '            ret = True
        '        End If
        '    Catch ex As System.Net.NetworkInformation.PingException
        '    Catch ex As Exception
        '    End Try

        '    Return ret
        'End Function

        Public Shared Function getMainConnectionString(ByVal INIFileName As String) As String
            Dim ret As String = ""
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"
            ret = "Data Source=" & ini.ReadString("Server") & ";Initial Catalog=" & ini.ReadString("Database") & ";User ID=" & ini.ReadString("Username") & ";Password=" & FunctionEng.DeCripPwd(ini.ReadString("Password")) & ";Connection Timeout=3;"
            ini = Nothing
            Return ret
        End Function

        Public Shared Function getDisplayConnectionString(ByVal INIFileName As String) As String
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"
            Dim ret As String = "Data Source=" & ini.ReadString("Server1") & ";Initial Catalog=" & ini.ReadString("Database1") & ";User ID=" & ini.ReadString("Username1") & ";Password=" & FunctionEng.DeCripPwd(ini.ReadString("Password1")) & ";Connection Timeout=3;"
            ini = Nothing
            Return ret
        End Function

        Public Shared Function CheckTransQueue(ByVal INIFileName As String) As Boolean
            Dim ret As Boolean = False
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            Dim dtM As New DataTable
            Dim dtD As New DataTable

            Try
                Dim ConnM As New SqlConnection(getMainConnectionString(INIFileName))
                ConnM.Open()
                Dim da As New SqlDataAdapter("select count(id) qty from tb_counter_queue", ConnM)
                da.Fill(dtM)
                ConnM.Close()
            Catch ex As SqlException
                Return False
            End Try

            Try
                Dim ConnD As New SqlConnection(getDisplayConnectionString(INIFileName))
                ConnD.Open()
                Dim da As New SqlDataAdapter("select count(id) qty from tb_counter_queue", ConnD)
                da.Fill(dtD)
                ConnD.Close()
            Catch ex As SqlException
                Return False
            End Try

            If Convert.ToInt64(dtM.Rows(0)("qty")) = Convert.ToInt64(dtD.Rows(0)("qty")) Then
                ret = True
            End If
            ini = Nothing

            Return ret
        End Function

        Private Shared Function ConnectToMainServer(ByVal Conn As SqlConnection, ByVal INIFileName As String) As Boolean
            Dim ret As Boolean = False
            Try
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
                Conn.ConnectionString = getMainConnectionString(INIFileName)
                Conn.Open()
                ret = True
            Catch ex As SqlException

            End Try
            Return ret
        End Function

        Public Shared Function CheckDBConn(ByVal INIFileName As String) As Boolean
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            If CheckCurrentDB(INIFileName) = "MAIN" Then
                Dim Conn As New SqlConnection
                Dim ret As Boolean = False
                For i As Integer = 1 To 1   'ถ้าไม่ได้ให้ Retry 3 ครั้ง
                    ret = ConnectToMainServer(Conn, INIFileName)
                    If ret = True Then
                        Exit For
                    End If
                Next
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If

                Return ret
            End If
            ini = Nothing

            Return False
        End Function

        Public Shared Function CheckConnDbServer(ByVal INIFileName As String) As String
            Dim ret As String = ""

            Dim conn As New SqlConnection
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            If ConnectToMainServer(conn, INIFileName) = False Then
                ret = "ไม่สามารถเชื่อมต่อฐานข้อมูลที่เครื่อง Main Server ได้"
            End If

            If ret.Trim = "" Then
                Try
                    conn = New SqlConnection
                    conn.ConnectionString = getDisplayConnectionString(INIFileName)
                    conn.Open()
                Catch ex1 As Exception
                    ret = "ไม่สามารถเชื่อมต่อฐานข้อมูลที่เครื่อง Display Server ได้"
                End Try
            End If

            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            ini = Nothing

            Return ret
        End Function

        Public Shared Function CheckConCreateTrans(ByVal Conn As SqlConnection, ByVal INIFileName As String) As Boolean
            Dim ret As Boolean = False
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"
            Try
                For i As Integer = 1 To 3   'ถ้าไม่ได้ให้ Retry 3 ครั้ง
                    ret = ConnectToMainServer(Conn, INIFileName)
                    If ret = True Then
                        Exit For
                    End If
                Next
            Catch ex As Exception

            End Try
            ini = Nothing
            Return ret
        End Function

        Public Shared Function CheckDBConn(ByVal Conn As SqlConnection, ByVal INIFileName As String, ByVal SoftWareName As String, ByVal FunctionName As String, ByVal SQL As String) As Boolean
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            If CheckCurrentDB(INIFileName) = "MAIN" Then
                Dim ret As Boolean = False
                Dim RetryConnectDB As Integer = ini.ReadInteger("RetryConnectDB")
                For i As Integer = 1 To RetryConnectDB   'ถ้าไม่ได้ให้ Retry 3 ครั้ง
                    ret = ConnectToMainServer(Conn, INIFileName)
                    If ret = True Then
                        Exit For
                    End If
                Next
                'If ret = False Then
                '    Try
                '        'ถ้า Connect Main ไม่ได้ ก็ให้ Swich มาที่ Display DB เลย
                '        If SetActiveDB(INIFileName, SoftWareName, FunctionName, "DISPLAY", SQL) = True Then
                '            Conn.ConnectionString = getDisplayConnectionString(INIFileName)
                '            Conn.Open()
                '            ret = True
                '        Else
                '            ret = False
                '        End If
                '    Catch ex1 As Exception
                '        ret = False
                '    End Try
                'End If

                Return ret
            Else
                Try
                    If Conn.State = ConnectionState.Open Then
                        Conn.Close()
                    End If
                    Conn.ConnectionString = getDisplayConnectionString(INIFileName)
                    Conn.Open()
                    Return True
                Catch ex As SqlException
                    Return False
                End Try
            End If
            ini = Nothing

            Return False
        End Function

        Public Shared Function AgentCheckDBConn(ByVal Conn As SqlConnection, ByVal INIFileName As String, ByVal SoftWareName As String, ByVal FunctionName As String, ByVal SQL As String) As Boolean
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            If CheckCurrentDB(INIFileName) = "MAIN" Then
                Dim RetryConnectDB As Integer = ini.ReadInteger("RetryConnectDB")
                Dim ret As Boolean = False
                For i As Integer = 1 To RetryConnectDB   'ถ้าไม่ได้ให้ Retry 3 ครั้ง
                    ret = ConnectToMainServer(Conn, INIFileName)
                    If ret = True Then
                        Exit For
                    End If
                Next
                If ret = False Then
                    Try
                        Conn.ConnectionString = getDisplayConnectionString(INIFileName)
                        Conn.Open()
                        ret = True
                    Catch ex1 As Exception
                        ret = False
                    End Try
                End If

                Return ret
            Else
                Try
                    If Conn.State = ConnectionState.Open Then
                        Conn.Close()
                    End If
                    Conn.ConnectionString = getDisplayConnectionString(INIFileName)
                    Conn.Open()
                    Return True
                Catch ex As SqlException
                    Return False
                End Try
            End If

            Return False
        End Function

        Public Shared Function CheckCurrentDB(ByVal IniFilename As String) As String
            Dim ret As String = "MAIN"

            'Dim ini As New IniReader(IniFilename)
            'ini.Section = "Setting"

            'Try
            '    Dim dt As New DataTable
            '    Dim ConnD As New SqlConnection(getDisplayConnectionString(IniFilename))
            '    ConnD.Open()
            '    Dim da As New SqlDataAdapter("select config_value from tb_setting where config_name='CurrentDB'", ConnD)
            '    da.Fill(dt)
            '    ConnD.Close()

            '    If dt.Rows.Count > 0 Then
            '        ret = dt.Rows(0)("config_value").ToString
            '    End If
            '    dt.Dispose()
            'Catch ex As SqlException
            '    ret = "MAIN"
            'End Try

            'ini = Nothing

            Return ret
        End Function

        Public Shared Function SetActiveDB(ByVal IniFilename As String, ByVal SoftWareName As String, ByVal FunctionName As String, ByVal ActiveDB As String, ByVal SQL As String) As Boolean
            Dim ini As New IniReader(IniFilename)
            ini.Section = "Setting"
            Dim ret As Boolean = False
            Try
                Dim dt As New DataTable
                Dim ConnD As New SqlConnection(getDisplayConnectionString(ini.Filename))
                ConnD.Open()

                Dim cmd As New SqlCommand("update tb_setting set config_value='" & ActiveDB & "' where config_name='CurrentDB'", ConnD)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                ConnD.Close()

                Try
                    'กรณีที่มีการ FailOver ให้เก็บ Log ที่เครื่อง Display

                    'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                      Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
                      chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                      sslerror As System.Net.Security.SslPolicyErrors) True

                    Dim FailURL As String = "http://" & ini.ReadString("Server1") & "/ShopWebService/frmFailOverLog.aspx?"
                    Dim InputPara As String = "ClientIP=" & FunctionEng.GetIPAddress
                    InputPara += "&SoftwareName=" & SoftWareName
                    InputPara += "&FunctionName=" & FunctionName
                    InputPara += "&ActiveDB=" & ActiveDB
                    InputPara += "&SQL=" & SQL

                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(FailURL & InputPara)
                    webRequest.ContentType = "application/x-www-form-urlencoded"
                    webRequest.Method = "POST"
                    Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
                    Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
                    Dim os As Stream = Nothing

                    webRequest.ContentLength = bytes.Length
                    os = webRequest.GetRequestStream()
                    os.Write(bytes, 0, bytes.Length)
                    webresponse = webRequest.GetResponse()
                    Dim Stream As New StreamReader(webresponse.GetResponseStream())
                    Dim tmp As String = ""
                    If Stream.Peek <> -1 Then
                        tmp = Stream.ReadLine()
                    End If
                    Stream.Close()
                    Stream = Nothing
                Catch ex As WebException

                End Try

                ret = True
            Catch ex As SqlException
                ret = False
            End Try

            Return ret
        End Function

        Public Shared Sub CheckCurrentActiveDB(ByVal INIFileName As String, ByVal Conn As SqlConnection, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim ini As New IniReader(INIFileName)
            ini.Section = "Setting"

            Dim CurrDB As String = CheckCurrentDB(INIFileName)
            If CurrDB = "DISPLAY" Then
                If CheckTransQueue(INIFileName) = True Then
                    If Conn.State = ConnectionState.Open Then
                        Conn.Close()
                    End If

                    Conn.ConnectionString = getMainConnectionString(INIFileName)
                    Conn.Open()
                    SetActiveDB(INIFileName, SoftwareName, FunctionName, "MAIN", "")
                End If
            End If
            ini = Nothing
        End Sub

        Public Shared Function ExecuteTable(ByVal Sql As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim dt As New DataTable
            Try
                Dim conn As New SqlConnection
                If AgentCheckDBConn(conn, INIFileName, SoftwareName, FunctionName, Sql) = True Then
                    Dim cmd As New SqlCommand(Sql, conn)
                    cmd.CommandType = CommandType.Text
                    Dim da As New SqlDataAdapter(Sql, conn)
                    da.Fill(dt)
                    conn.Close()
                    conn.Dispose()
                    da.Dispose()
                End If
            Catch ex As Exception

            End Try

            Return dt
        End Function

        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Boolean
            Dim ret As Boolean = False
            Try
                Dim conn As New SqlConnection
                If AgentCheckDBConn(conn, INIFileName, SoftwareName, FunctionName, Sql) = True Then
                    Dim cmd As New SqlCommand(Sql, conn)
                    cmd.CommandType = CommandType.Text
                    If cmd.ExecuteNonQuery() > 0 Then
                        ret = True
                    End If
                End If
            Catch ex As Exception

            End Try

            Return ret
        End Function

        Public Shared Function GetShopConfig(ByVal INIFileName As String, ByVal ConfigName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As String
            Dim ret As String = ""
            Dim sql As String = "select config_value from tb_setting where config_name='" & ConfigName & "'"
            Dim dt As New DataTable
            dt = ExecuteTable(sql, INIFileName, SoftwareName, FunctionName)
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0)("config_value")
            End If
            dt.Dispose()

            Return ret
        End Function

        Public Shared Function UpdateShopConfig(ByVal INIFileName As String, ByVal ConfigName As String, ByVal ConfigValue As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Boolean
            Dim ret As Boolean = False
            Dim sql As String = "update tb_setting set config_value ='" & ConfigValue & "' where config_name='" & ConfigName & "'"
            ret = ExecuteNonQuery(sql, INIFileName, SoftwareName, FunctionName)
         
            Return ret
        End Function
    End Class
End Namespace

