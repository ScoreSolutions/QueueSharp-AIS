Imports OfficeOpenXml
Imports System.IO

Public Class Form1
    Dim IniFileName As String = Application.StartupPath & "\Config.ini"
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                Dim vDateNow As DateTime = DateTime.Now
                Dim eng As New Engine.CSISurveyENG
                Dim dt As New DataTable
                dt = eng.GetData2GCustomerToday(vDateNow)
                If dt.Rows.Count > 0 Then
                    If Directory.Exists(Application.StartupPath & "\Temp\") = False Then
                        Directory.CreateDirectory(Application.StartupPath & "\Temp\")
                    End If

                    Dim vMonth As String = DateAdd(DateInterval.Month, -1, vDateNow).ToString("MM_yyyy", New Globalization.CultureInfo("en-US"))
                    Dim FileName As String = Application.StartupPath & "\Temp\CSI_AIS_" & vMonth & ".xlsx"
                    Using exPke As New ExcelPackage(New FileInfo(FileName))
                        Dim sh As ExcelWorksheet = exPke.Workbook.Worksheets.Add(vMonth)
                        sh.Cells("A1").Value = "Mobile No"
                        sh.Cells("A2").LoadFromDataTable(dt, False)

                        exPke.Save()

                        Dim ini As New IniReader(IniFileName)
                        ini.Section = "MailSetting"
                        Dim MailSubject As String = ini.ReadString("MailSubject")
                        MailSubject = Replace(MailSubject, "{Month}", DateAdd(DateInterval.Month, -1, vDateNow).ToString("MM"))
                        MailSubject = Replace(MailSubject, "{Year}", DateAdd(DateInterval.Month, -1, vDateNow).ToString("yyyy", New Globalization.CultureInfo("en-US")))

                        Dim MailTo As String = ini.ReadString("MailTo")
                        Dim MailCC As String = ini.ReadString("MailCC")
                        ini = Nothing

                        'If File.Exists(FileName) = True Then
                        '    File.Move(FileName, "D:\" & exPke.File.Name)
                        'End If
                        ExecuteSendEmail(MailSubject, MailTo, MailCC, CreateMailContent(vMonth), FileName)
                    End Using
                End If
                
                dt.Dispose()
            End If
        Catch ex As Exception
            Engine.Common.FunctionEng.SaveErrorLog("Form1.Shown", "Exception :" & ex.Message & vbNewLine & ex.StackTrace, "\ErrorLog\", "AisCSI-Export2GData")
        End Try
        Application.Exit()
    End Sub

    Private Function CreateMailContent(ByVal vMonth As String) As String
        Dim ret As String = "Dear All " & vbNewLine
        ret += "ข้อมูลลูกค้า 2G ทุกสถานะ ประจำเดือน " & vMonth.Replace("_", " ") & vbNewLine
        ret += "[Auto Mail]"
        Return ret
    End Function

    Private Function ExecuteSendEmail(ByVal MailSubject As String, ByVal MailTo As String, ByVal MailCC As String, ByVal MailContent As String, ByVal MailAttFile As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim sql As String = ""
            sql = " declare @p_subject as varchar(300); set @p_subject='" & MailSubject.Trim & "'"
            sql += " declare @p_recipients as varchar(300);set @p_recipients='" & MailTo.Trim & "'"
            sql += " declare @p_cc as varchar(300);set @p_cc='" & MailCC.Trim & "'"
            sql += " declare @MailAttFile varchar(255); set @MailAttFile = '" & MailAttFile & "';"
            sql += " EXEC msdb.dbo.sp_send_dbmail @profile_name='QIS System',"
            sql += " @recipients=@p_recipients,@copy_recipients=@p_cc,"
            sql += " @subject=@p_subject,@body='" & MailContent.Trim & "',@file_attachments = @MailAttFile"

            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql)
            ret = True
        Catch ex As Exception
            ret = False
        End Try

        Return ret
    End Function
    

    
End Class
