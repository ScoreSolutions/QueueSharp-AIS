Imports System.IO
Imports QMDeleteTempFileAgent.Org.Mentalis.Files
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                Dim ini As New IniReader(Application.StartupPath & "\ConfigPath.ini")
                ini.Section = "Setting"
                Dim QMTempPath As String = ini.ReadString("TempVDOPath")
                If Directory.Exists(QMTempPath) = True Then
                    For Each d As String In Directory.GetDirectories(QMTempPath)
                        For Each f As String In Directory.GetFiles(d)
                            Dim fInfo As New FileInfo(f)
                            If DateAdd(DateInterval.Day, 7, fInfo.LastAccessTime) < DateTime.Today Then
                                Try
                                    File.SetAttributes(f, FileAttributes.Normal)
                                    File.Delete(f)
                                Catch ex As Exception

                                End Try
                            End If
                            fInfo = Nothing
                        Next

                        'ถ้าไม่มีไฟล์เหลือใน Folder แล้วให้ลบ Folder ไปเลย
                        If Directory.GetFiles(d).Length = 0 Then
                            Try
                                Directory.Delete(d)
                            Catch ex As Exception

                            End Try
                        End If
                    Next

                    For Each f As String In Directory.GetFiles(QMTempPath)
                        Dim fInfo As New FileInfo(f)
                        If DateAdd(DateInterval.Day, 7, fInfo.LastAccessTime) < DateTime.Today Then
                            Try
                                File.SetAttributes(f, FileAttributes.Normal)
                                File.Delete(f)
                            Catch ex As Exception

                            End Try
                        End If
                        fInfo = Nothing
                    Next
                End If


                Dim QMVdoPath As String = ini.ReadString("VDOPath")
                If Directory.Exists(QMVdoPath) = True Then
                    Dim QMLogMonth As String = Engine.Common.FunctionEng.GetConfig("QMLogMonth")
                    If QMLogMonth.Trim = "" Then
                        QMLogMonth = "2"
                    End If
                    Dim DateCal As String = DateAdd(DateInterval.Month, 0 - Convert.ToInt16(QMLogMonth), DateTime.Today).ToString("yyMMdd", New Globalization.CultureInfo("en-US"))

                    'Delete file In sub - directory
                    For Each d As String In Directory.GetDirectories(QMVdoPath)
                        For Each f As String In Directory.GetFiles(d, "*.flv")
                            Dim fInfo As New FileInfo(f)
                            Dim FileDate As String = fInfo.Name.Substring(0, 6)
                            If FileDate < DateCal Then
                                Dim Qid As Integer = CInt(fInfo.Name.Substring(6, fInfo.Name.LastIndexOf(".") - 6))

                                Dim sql As String = "update tb_qm_transfer_log"
                                sql += " set status='7', update_by='QMDeleteTempFile', update_date=getdate()"
                                sql += " where tb_counter_queue_id='" & Qid & "'"
                                sql += " and convert(varchar(8),service_date,112)='20" & FileDate & "'"
                                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                                If Engine.Common.FunctionEng.ExecuteShopNonQuery(sql, shTrans) = True Then
                                    Try
                                        File.SetAttributes(f, FileAttributes.Normal)
                                        File.Delete(f)
                                        shTrans.CommitTransaction()
                                    Catch ex As Exception
                                        shTrans.RollbackTransaction()
                                    End Try
                                Else
                                    shTrans.RollbackTransaction()

                                    'File ไม่มี Log และอยู่นานเกิน 2 เดือนแล้ว
                                    Try
                                        File.SetAttributes(f, FileAttributes.Normal)
                                        File.Delete(f)
                                    Catch ex As Exception

                                    End Try
                                End If
                            End If
                        Next

                        'ถ้าไม่มีไฟล์เหลือใน Folder แล้วให้ลบ Folder ไปเลย
                        If Directory.GetFiles(d).Length = 0 Then
                            Try
                                Directory.Delete(d)
                            Catch ex As Exception

                            End Try
                        End If
                    Next

                    'Delete File in VDO Directory
                    For Each f As String In Directory.GetFiles(QMVdoPath, "*.flv")
                        Dim fInfo As New FileInfo(f)
                        Dim FileDate As String = fInfo.Name.Substring(0, 6)
                        If FileDate < DateCal Then
                            Dim Qid As Integer = CInt(fInfo.Name.Substring(6, fInfo.Name.LastIndexOf(".") - 6))

                            Dim sql As String = "update tb_qm_transfer_log"
                            sql += " set status='7', update_by='QMDeleteTempFile', update_date=getdate()"
                            sql += " where tb_counter_queue_id='" & Qid & "'"
                            sql += " and convert(varchar(8),service_date,112)='20" & FileDate & "'"

                            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                            If Engine.Common.FunctionEng.ExecuteShopNonQuery(sql, shTrans) = True Then
                                Try
                                    File.SetAttributes(f, FileAttributes.Normal)
                                    File.Delete(f)
                                    shTrans.CommitTransaction()
                                Catch ex As Exception
                                    shTrans.RollbackTransaction()
                                End Try
                            Else
                                shTrans.RollbackTransaction()

                                'File ไม่มี Log และอยู่นานเกิน 2 เดือนแล้ว
                                Try
                                    File.SetAttributes(f, FileAttributes.Normal)
                                    File.Delete(f)
                                Catch ex As Exception

                                End Try
                            End If
                        End If
                    Next
                End If
                ini = Nothing
            End If
        Catch ex As Exception

        End Try
        Application.Exit()
    End Sub
End Class
