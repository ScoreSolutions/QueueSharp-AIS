Imports System.IO
Imports QMSchedule.Org.Mentalis.Files
Imports AlexPilotti.FTPS.Client
Imports AlexPilotti.FTPS.Common
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Data.SqlClient
Imports Engine.Common.ShopConnectDBENG

Public Class frmScheduleQM
    Dim INIFileName As String = Application.StartupPath & "\QM\QueueSharp.ini"
    Dim FtpHost As String
    Dim FtpUser As String
    Dim FtpPassword As String

    Private Const FileIsMerging As String = "1"
    Private Const FileIsAlreadyMerged As String = "2"
    Private Const FileIsTransferringFromTempFolder As String = "3"
    Private Const TransferComplete As String = "4"
    Private Const FileMovingToBackupFolder As String = "5"
    Private Const FileIsTransferringFromBackupFolder As String = "6"
    Private Const FileNotFound As String = "7"

    Private Sub frmScheduleQM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ini As New IniReader(INIFileName)
        FtpHost = ini.ReadString("QMFtp", "FtpHost")
        FtpUser = ini.ReadString("QMFtp", "FtpUser")
        FtpPassword = Engine.Common.FunctionEng.DeCripPwd(ini.ReadString("QMFtp", "FtpPassword"))
        ini = Nothing

        If My.Application.CommandLineArgs.Count > 0 Then
            If My.Application.CommandLineArgs(0).ToString = "6" Then
                TransferFileStatus6()
            End If

            DeleteQMTempFile()
        Else
            TransferFileFromBackup()
        End If
        Application.Exit()
    End Sub


    Public Function WriteTempFTP(ByVal shopFtpHost As String, ByVal User As String, ByVal Password As String, ByVal Localfile As String, ByVal DestinationFile As String, ByVal FolderName As String, ByVal QMSplitFolder As String) As Boolean
        Using FTP As New AlexPilotti.FTPS.Client.FTPSClient
            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential(User, Password)
            Try
                FTP.Connect(shopFtpHost, 990, credentials, AlexPilotti.FTPS.Client.ESSLSupportMode.Implicit, _
                            New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidateCertificate), _
                            New System.Security.Cryptography.X509Certificates.X509Certificate(), 0, 0, 0, 2000, True)

                If QMSplitFolder = "Y" Then
                    CheckToDayDirectory(FTP, FolderName)
                End If

                FTP.PutFile(Localfile, DestinationFile) 'eg. "/jddddd.flv"
                Return True
            Catch ex As Exception
                SaveQueryErrorLog("frmScheduleQM_WriteTempFTP : Exception :" & ex.Message & " ### Cannot Transfer File Name" & Localfile, "")
                Return False
            End Try
        End Using

    End Function
    Public Function ValidateCertificate(ByVal sender As Object, _
                                      ByVal certificate As X509Certificate, _
                                      ByVal chain As X509Chain, _
                                      ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub CheckToDayDirectory(ByVal FTP As AlexPilotti.FTPS.Client.FTPSClient, ByVal FolderName As String)
        Try
            FTP.GetDirectoryListUnparsed(FolderName)
        Catch ex As Exception
            FTP.MakeDir(FolderName)
        End Try
    End Sub

    Private Function UpdateQmTransferLog(ByVal vStatus As String, ByVal Qid As String, ByVal ServiceDate As String) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        sql += "select username "
        sql += " from TB_USER "

        If ServiceDate = DateTime.Now.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) Then
            sql += " where id = (select top 1 USER_ID "
            sql += "            from TB_COUNTER_QUEUE where id='" & Qid & "'"
            sql += "            and DATEDIFF(d,service_date,getdate())=0)"
        Else
            sql += " where id = (select USER_ID "
            sql += "            from TB_COUNTER_QUEUE_HISTORY "
            sql += "            where tb_counter_queue_id = '" & Qid & "'"
            sql += "            and CONVERT(varchar(8),service_date,112)='" & ServiceDate & "')"
        End If

        

        Dim dt As DataTable = getDataTable(sql, Nothing)
        If dt.Rows.Count > 0 Then
            Try
                sql = "update TB_QM_TRANSFER_LOG"
                sql += " set update_date=getdate(), update_by='" & dt.Rows(0)("username") & "', status='" & vStatus & "'"
                If vStatus = TransferComplete Then
                    sql += ",transfer_date=getdate() "
                End If
                sql += " where tb_counter_queue_id='" & Qid & "' and convert(varchar(8),service_date,112)='" & ServiceDate & "'"
                ret = executeSQL(sql)
                If ret = False Then
                    SaveQueryErrorLog("frmScheduleQM_UpdateQmTransferLog Error Update TB_QM_TRANSFER_LOG", sql)
                End If
            Catch ex As Exception
                SaveQueryErrorLog("frmScheduleQM_UpdateQmTransferLog Exception :" & ex.Message, sql)
            End Try
        End If
        dt.Dispose()

        Return ret
    End Function

    Private Function GetQueueDataByDate(ByVal vServiceDate As String, ByVal QueueID As Integer) As DataTable
        Dim sql As String = "select q.id ,q.service_date,q.queue_no,q.customer_id mobile_no,q.item_id,i.item_name"
        sql += " from TB_COUNTER_QUEUE q"
        sql += " inner join TB_ITEM i on i.id=q.item_id"
        sql += " where convert(varchar(8),service_date,112)='" & vServiceDate & "' and q.id=" & QueueID
        sql += " union all "
        sql += " select q.tb_counter_queue_id id ,q.service_date,q.queue_no,q.customer_id mobile_no,q.item_id,q.item_name"
        sql += " from TB_COUNTER_QUEUE_HISTORY q"
        sql += " where convert(varchar(8),service_date,112)='" & vServiceDate & "' and q.id=" & QueueID

        Dim dt As New DataTable
        dt = getDataTable(sql)
        Return dt
    End Function


    Private Sub DeleteQMTempFile()
        'MessageBox.Show("DeleteQMTempFile")
        Dim TempFolder As String = Application.StartupPath & "\QM\Temp\"
        Dim BackupFolder As String = Application.StartupPath & "\QM\Backup\"

        If IO.Directory.Exists(TempFolder) = True Then
            For Each f As String In IO.Directory.GetFiles(TempFolder)
                'ถ้าเป็นไฟล์นามสกุลอื่นๆ ก็ให้ลบเลย
                Try
                    IO.File.SetAttributes(f, FileAttributes.Normal)
                    IO.File.Delete(f)
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub

    Private Sub TransferFileStatus6()
        Dim BackupFolder As String = Application.StartupPath & "\QM\Backup"
        If Directory.Exists(BackupFolder) = False Then
            Exit Sub
        End If

        If Directory.GetFiles(BackupFolder).Length = 0 Then
            Exit Sub
        End If

        Dim QMSplitFolder As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "QMSplitFolder", "QueueSharp", "QMSchedule.frmScheduleQM.TransferFileStatus6")
        If QMSplitFolder.Trim = "" Then
            QMSplitFolder = "N"
        End If

        Dim dt As New DataTable
        dt = getDataTable("select id, service_date,tb_counter_queue_id from TB_QM_TRANSFER_LOG where status='" & FileIsTransferringFromBackupFolder & "' and ip_address='" & GetIPAddress() & "'")
        For Each dr As DataRow In dt.Rows
            Dim FileDate As String = ""
            Dim Qid As Integer = 0
            Try
                FileDate = Convert.ToDateTime(dr("service_date")).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                Dim fInfo As New FileInfo(BackupFolder & "\" & FileDate.Substring(2, 6) & dr("tb_counter_queue_id") & ".flv") 'Ex. 13080199.flv
                Dim f As String = fInfo.FullName

                Dim FtpDestfile As String = ""
                If QMSplitFolder = "Y" Then
                    FtpDestfile = "/" & FileDate & "/" & fInfo.Name
                Else
                    FtpDestfile = "/" & fInfo.Name
                End If

                If fInfo.Name.LastIndexOf(".") > 0 Then
                    Qid = CInt(fInfo.Name.Substring(6, fInfo.Name.LastIndexOf(".") - 6))
                Else
                    Qid = CInt(fInfo.Name.Substring(6))
                End If

                Dim ret As Boolean = WriteTempFTP(FtpHost, FtpUser, FtpPassword, f, FtpDestfile, FileDate, QMSplitFolder)
                If ret = True Then
                    If UpdateQmTransferLog(TransferComplete, Qid, FileDate) = True Then
                        Try
                            File.Delete(f)
                        Catch ex As Exception
                            UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                            SaveQueryErrorLog("QMSchedule.frmScheduleQM.TransferFileFromBackup : Cannot Delete File From Backup QID=" & Qid & "  QDate=" & FileDate & "   File=" & fInfo.Name & " $$$ Exception=" & ex.Message, "")
                        End Try
                    Else
                        UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                    End If
                Else
                    UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                    SaveQueryErrorLog("QMSchedule.frmScheduleQM.TransferFileFromBackup : Cannot Transfer File From Backup Folder File=" & fInfo.Name, "")
                End If
                fInfo = Nothing
            Catch ex As Exception
                UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                SaveQueryErrorLog("QMSchedule.frmScheduleQM.TransferFileFromBackup Exception :" & ex.Message, "")
            End Try
        Next
    End Sub

    Private Sub TransferFileFromBackup()
        'Application.DoEvents()
        Dim BackupFolder As String = Application.StartupPath & "\QM\Backup"
        If Directory.Exists(BackupFolder) = False Then
            Exit Sub
        End If

        If Directory.GetFiles(BackupFolder).Length = 0 Then
            Exit Sub
        End If

        Dim dt As New DataTable
        dt = getDataTable("select id, service_date,tb_counter_queue_id from TB_QM_TRANSFER_LOG where status='" & FileMovingToBackupFolder & "' and ip_address='" & GetIPAddress() & "'")
        If dt.Rows.Count > 0 Then
            Dim QMSplitFolder As String = Engine.Common.ShopConnectDBENG.GetShopConfig(INIFileName, "QMSplitFolder", "QueueSharp", "QMSchedule.frmScheduleQM.TransferFileFromBackup")
            For Each dr As DataRow In dt.Rows
                Dim FileDate As String = ""
                Dim Qid As Integer = 0

                Dim uSql As String = "update TB_QM_TRANSFER_LOG "
                uSql += " set status='" & FileIsTransferringFromBackupFolder & "' "
                uSql += " where id='" & dr("id") & "' and status='" & FileMovingToBackupFolder & "' "

                If executeSQL(uSql) = True Then
                    Try
                        FileDate = Convert.ToDateTime(dr("service_date")).ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
                        Dim fInfo As New FileInfo(BackupFolder & "\" & FileDate.Substring(2, 6) & dr("tb_counter_queue_id") & ".flv") 'Ex. 13080199.flv
                        Dim f As String = fInfo.FullName

                        Dim FtpDestfile As String = ""
                        If QMSplitFolder = "Y" Then
                            FtpDestfile = "/" & FileDate & "/" & fInfo.Name
                        Else
                            FtpDestfile = "/" & fInfo.Name
                        End If

                        If fInfo.Name.LastIndexOf(".") > 0 Then
                            Qid = CInt(fInfo.Name.Substring(6, fInfo.Name.LastIndexOf(".") - 6))
                        Else
                            Qid = CInt(fInfo.Name.Substring(6))
                        End If

                        Dim ret As Boolean = WriteTempFTP(FtpHost, FtpUser, FtpPassword, f, FtpDestfile, FileDate, QMSplitFolder)
                        If ret = True Then
                            If UpdateQmTransferLog(TransferComplete, Qid, FileDate) = True Then
                                Try
                                    File.Delete(f)
                                Catch ex As Exception
                                    UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                                    SaveQueryErrorLog("QMSchedule.frmScheduleQM.TransferFileFromBackup : Cannot Delete File From Backup QID=" & Qid & "  QDate=" & FileDate & "   File=" & fInfo.Name & " $$$ Exception=" & ex.Message, "")
                                End Try
                            Else
                                UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                            End If
                        Else
                            UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                            SaveQueryErrorLog("QMSchedule.frmScheduleQM.TransferFileFromBackup : Cannot Transfer File From Backup Folder File=" & fInfo.Name, "")
                        End If
                        fInfo = Nothing
                    Catch ex As Exception
                        UpdateQmTransferLog(FileMovingToBackupFolder, Qid, FileDate)
                        SaveQueryErrorLog("QMSchedule.frmScheduleQM.TransferFileFromBackup Exception :" & ex.Message, "")
                    End Try
                End If
            Next
        End If
        dt.Dispose()
    End Sub

#Region "Database Connection"
    Public Conn As New SqlConnection

    Public Sub SaveQueryErrorLog(ByVal error_message As String, ByVal argSQL As String)
        Try
            Dim id As String = FindID("TB_Error_Log")
            Dim sql As String = ""
            sql = "Insert Into TB_Error_Log (id,client_ip, log_date,error_message, sql_command,version) Values (" & id & ",'" & GetIPAddress() & "',GetDate(),'" & FixDB(error_message) & "','" & FixDB(argSQL) & "','" & getMyVersion() & "') "
            executeSQL(sql)
        Catch ex As Exception

        End Try
    End Sub

    Public Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
        'Dim company As String = Application.CompanyName
    End Function

    Public Function executeSQL(ByVal SQL As String) As Boolean
        Dim ret As Boolean = False
        If SQL.Trim <> "" Then
            Dim cmd As New SqlCommand(SQL)
            Try
                If CheckConn("QMSchedule.frmShcedule.executeSQL", SQL) = True Then
                    cmd.Connection = Conn
                    cmd.ExecuteNonQuery()
                    Conn.Close()
                    ret = True
                Else
                    SaveQueryErrorLog("QMSchedule.frmShcedule.executeSQL : Database connection error !!!", SQL)
                    ret = False
                End If
            Catch ex As Exception
                SaveQueryErrorLog("QMSchedule.frmShcedule.executeSQL : " & ex.Message, SQL)
                ret = False
            End Try
        End If
        Return ret
    End Function

    Public Function getDataTable(ByVal SQL As String) As DataTable
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            If CheckConn("QMSchedule.frmShcedule.getDataTable", SQL) = True Then
                da = New SqlDataAdapter(SQL, Conn)
                da.Fill(dt)
                Conn.Close()
                Return dt
            Else
                SaveQueryErrorLog("QMSchedule.frmShcedule.getDataTable : Database connection error !!!", SQL)
            End If
        Catch ex As SqlException
            If CheckConn("QMSchedule.frmShcedule.getDataTable", SQL) = True Then
                Try
                    da = New SqlDataAdapter(SQL, Conn)
                    da.Fill(dt)
                    Conn.Close()
                    Return dt
                Catch ex1 As SqlException
                    SaveQueryErrorLog("QMSchedule.frmShcedule.getDataTable : " & ex1.Message, SQL)
                    Return dt
                End Try
            Else
                SaveQueryErrorLog("QMSchedule.frmShcedule.getDataTable : " & ex.Message, SQL)
                Return dt
            End If
        End Try
        Return New DataTable
    End Function

    Public Function getDataTable(ByVal SQL As String, ByVal shTrans As SqlTransaction) As DataTable
        Dim dt As New DataTable
        Try
            If shTrans IsNot Nothing Then
                dt = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(SQL, shTrans)
            Else
                dt = getDataTable(SQL)
            End If
            Return dt
        Catch ex As SqlException
            SaveQueryErrorLog("QMSchedule.frmShcedule.getDataTable : Database connection error !!!", SQL)
        End Try
        Return New DataTable
    End Function
    Public Function FindID(ByVal TableName As String) As String
        Dim id As String = ""
        Try
            Dim sql As String = ""
            Dim dt As New DataTable
            sql = "select isnull(MAX(id + 1),1) as id from " & FixDB(TableName)
            dt = getDataTable(sql)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0).Item("id").ToString
            End If
            Return id
        Catch ex As Exception

        End Try
        Return id
    End Function

    Public Function FixDB(ByVal TXT As String) As String 'Replace text จาก ' เป็น ''
        If IsDBNull(TXT) = True Then
            Return ""
        ElseIf TXT = Nothing Then
            Return ""
        Else
            Return Trim(TXT.ToString.Replace("'", "''"))
        End If
    End Function
    Public Function CheckConn(ByVal FunctionName As String, ByVal SQL As String) As Boolean
        If CheckDBConn(Conn, INIFileName, "QueueSharp", FunctionName, SQL) = False Then
            Return False
        End If
        Return True
    End Function

    Public Function GetIPAddress() As String
        Dim oAddr As System.Net.IPAddress
        Dim sAddr As String
        With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
            oAddr = New System.Net.IPAddress(.AddressList(0).Address)
            sAddr = oAddr.ToString
        End With
        GetIPAddress = sAddr
    End Function
#End Region
End Class